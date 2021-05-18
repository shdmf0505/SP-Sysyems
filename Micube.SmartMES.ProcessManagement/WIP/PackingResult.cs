#region using

using DevExpress.Utils;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
	/// <summary>
	/// 프 로 그 램 명  : 공정관리 > 포장관리 > 포장 실적 등록
	/// 업  무  설  명  : 포장BOX별로 LOT의 포장작업 진행, BOX번호 생성 후 LOT정보 매핑
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-07-12
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class PackingResult : SmartConditionBaseForm
	{
		#region Local Variables

		//현재 LOT의 양품 수량
		private double _currentGoodQty = 0;

		//품목코드 비교
		private string _productDefId = string.Empty;
		private string _productDefVersion = string.Empty;

		//포장 작업 알림
		private bool _isDoPackingStart = false;

		//포장 타입
		private string _packageType = string.Empty;
		#endregion

		#region 생성자

		public PackingResult()
		{
			InitializeComponent();
		}

		#endregion

		#region 컨텐츠 영역 초기화

		/// <summary>
		/// 화면의 컨텐츠 영역을 초기화한다.
		/// </summary>
		protected override void InitializeContent()
		{
			base.InitializeContent();

			InitializeEvent();

			// TODO : 컨트롤 초기화 로직 구성
			InitializeGrid();
			InitializeLotInfoGrid();
			InitializePopupArea();

			//버튼 Enable
			ChangeEnableAllButtons(false);
		}

		/// <summary>        
		/// 포장 작업 그리드 초기화
		/// </summary>
		private void InitializeGrid()
		{
			grdWaitPackingLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
			grdWaitPackingLotList.GridButtonItem = GridButtonItem.Delete;
			grdWaitPackingLotList.View.SetIsReadOnly();
			grdWaitPackingLotList.View.OptionsSelection.EnableAppearanceFocusedRow = false;
			grdWaitPackingLotList.View.OptionsSelection.EnableAppearanceFocusedCell = false;

			//BOX No
			grdWaitPackingLotList.View.AddTextBoxColumn("BOXNO")
				.SetIsHidden();
			//품목코드
			grdWaitPackingLotList.View.AddTextBoxColumn("PRODUCTDEFID", 150);
			//품목명
			grdWaitPackingLotList.View.AddTextBoxColumn("PRODUCTDEFNAME", 250);
			//Rev.
			grdWaitPackingLotList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
			//LOT ID
			grdWaitPackingLotList.View.AddTextBoxColumn("LOTID", 250);
			//양품수량
			grdWaitPackingLotList.View.AddSpinEditColumn("QTY", 80)
				.SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
			//불량수량
			grdWaitPackingLotList.View.AddSpinEditColumn("DEFECTQTY", 80)
				.SetDisplayFormat("#,##0", MaskTypes.Numeric, false)
				.SetDefault(0);
			//작업자
			grdWaitPackingLotList.View.AddTextBoxColumn("USERID")
				.SetIsHidden();
			grdWaitPackingLotList.View.AddTextBoxColumn("USERNAME", 80);
			//작업시간
			grdWaitPackingLotList.View.AddTextBoxColumn("TXNTIME", 130);

			grdWaitPackingLotList.View.PopulateColumns();
		}

		/// <summary>
		/// LOT Info Grid 초기화
		/// </summary>
		private void InitializeLotInfoGrid()
		{
			grdLotInfo.ColumnCount = 5;
			grdLotInfo.LabelWidthWeight = "40%";
			grdLotInfo.SetInvisibleFields("CUSTOMERID", "ISHOLD", "LOTSTATE", "DEFECTQTY", "PROCESSSTATE", "PROCESSPATHID", "PROCESSDEFID", 
				"PROCESSDEFVERSION", "PROCESSSEGMENTCLASSID", "PROCESSSEGMENTID", "PROCESSSEGMENTVERSION", "PRODUCTDEFVERSION",
				"PRODUCTTYPE", "DEFECTUNIT", "PCSPNL", "PROCESSSEGMENTTYPE", "AREAID");
		}

		/// <summary>
		/// 작업장 팝업 초기화 및 작업자 초기화
		/// </summary>
		private void InitializePopupArea()
		{
			ConditionItemSelectPopup areaCondition = new ConditionItemSelectPopup();
			areaCondition.Id = "AREA";
			areaCondition.SearchQuery = new SqlQuery("GetAreaList", "10003", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", "AREATYPE=Area", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
			areaCondition.ValueFieldName = "AREAID";
			areaCondition.DisplayFieldName = "AREANAME";
			areaCondition.SetPopupLayout("SELECTAREA", PopupButtonStyles.Ok_Cancel, true, true);
			areaCondition.SetPopupResultCount(1);
			areaCondition.SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow);
			areaCondition.SetPopupAutoFillColumns("AREANAME");
			areaCondition.SetPopupApplySelection((selectedRows, dataGridRows) =>
			{
				//작업장 변경 시 작업자 조회
				string areaId = string.Empty;
				selectedRows.Cast<DataRow>().ForEach(row =>
				{
					areaId = Format.GetString(row["AREAID"], ""); 
				});

				if (string.IsNullOrEmpty(areaId)) return;

				Dictionary<string, object> param = new Dictionary<string, object>();
				param.Add("AreaId", areaId);
				param.Add("EnterpriseId", UserInfo.Current.Enterprise);
				param.Add("PlantId", UserInfo.Current.Plant);

				cboUser.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
				cboUser.ValueMember = "USERID";
				cboUser.DisplayMember = "WORKERNAME";
				cboUser.DataSource = SqlExecuter.Query("GetAreaworkerList", "10001", param);
				cboUser.ShowHeader = false;

				DataTable dtuser = cboUser.DataSource as DataTable;
				if (dtuser.Rows.Count == 0)
				{
					//현 작업장에 작업자가 없습니다. 작업자 등록 후 사용 가능합니다.
					ShowMessage("UserEmptyInArea");
					return;
				}

				List<DataRow> list = dtuser.AsEnumerable().Where(r => r["USERID"].Equals(UserInfo.Current.Id)).ToList();
				cboUser.EditValue = (list.Count > 0) ? list[0]["USERID"].ToString() : dtuser.Rows[0]["USERID"];
				
			});


			areaCondition.Conditions.AddTextBox("AREA");

			areaCondition.GridColumns.AddTextBoxColumn("AREAID", 150);
			areaCondition.GridColumns.AddTextBoxColumn("AREANAME", 200);

			popArea.SelectPopupCondition = areaCondition;
			popArea.SearchButtonReadOnly = false;
		}
		#endregion

		#region Event

		/// <summary>
		/// 화면에서 사용되는 이벤트를 초기화한다.
		/// </summary>
		public void InitializeEvent()
		{
			defectListForPacking.EditValueChange += DefectListForPacking_EditValueChange;
			txtLotId.KeyDown += TxtLotId_KeyDown;
			btnSaveLot.Click += BtnSaveLot_Click;
			btnStartPacking.Click += BtnStartPacking_Click;
			btnCreateBoxNo.Click += BtnCreateBoxNo_Click;
			btnPrintLabel.Click += BtnPrintLabel_Click;
		}

		/// <summary>
		/// 라벨 프린트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnPrintLabel_Click(object sender, EventArgs e)
		{
			DataTable dt = grdWaitPackingLotList.DataSource as DataTable;
			if(dt.Rows.Count < 1)
			{
				ShowMessage("NotExistLot");
				return;
			}

			string packingLabelLotId = dt.Rows[0]["LOTID"].ToString();
			SmartMES.Commons.CommonFunction.printPackingLabel(packingLabelLotId);
		}

		/// <summary>
		/// 불량 LOT 저장
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSaveLot_Click(object sender, EventArgs e)
		{
			/*
			decimal currentQty = Format.GetDecimal(numDefectQty.EditValue);
			if(!currentQty.Equals(0))
			{
				defectListForPacking.StorageDefectLotTempTable();
			}

			DataTable dtDefectLot = defectListForPacking.GetDefectLotSaveData();
			if (dtDefectLot.Rows.Count == 0)
			{
				// 저장할 데이터가 존재하지 않습니다.
				throw MessageException.Create("NoSaveData");
			}

			DataTable dtCopy = dtDefectLot.Copy();

			dtCopy.TableName = "lotDefectList";
			DataSet dataSet = new DataSet();
			dataSet.Tables.Add(dtCopy);

			ExecuteRule("SaveBoxPackingDefect", dataSet);
			ShowMessage("SuccedSave");

			dataSet.Clear();
			defectListForPacking.ClearDatas();
			defectListForPacking.ClearDefectLotTemp();
			*/
		}

		/// <summary>
		/// 박스 번호 생성
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnCreateBoxNo_Click(object sender, EventArgs e)
		{
			if(!string.IsNullOrEmpty(txtBoxNo.Text))
			{
				//박스번호가 존재합니다.
				ShowMessage("ExistBoxNo");
				return;
			}

			//박스No 채번
			MessageWorker worker = new MessageWorker("IdCreate");
			worker.SetBody(new MessageBody()
			{
				{ "IDCLASSID", "BoxNoSequence" }
			});
			var dtBoxNo = worker.Execute<DataTable>().GetResultSet();
			string boxId = dtBoxNo.AsEnumerable().First().Field<string>("ID");

			//존재 여부 확인
			Dictionary<string , object> param = new Dictionary<string, object>();
			param.Add("BOXNO", boxId);

			DataTable dtExistBoxNo = SqlExecuter.Query("GetBoxNo", "10001", param);
			if(dtExistBoxNo.Rows.Count > 1)
			{
				//해당 Box 번호는 이미 존재합니다.
				ShowMessage("ExistBoxNo", string.Format("Box No : {0}", txtBoxNo.Text));
				return;
			}

			txtBoxNo.Text = boxId;
			btnPrintLabel.Enabled = true;
			txtBoxNo.ReadOnly = true;
		}

		/// <summary>
		/// 포장작업완료 클릭 시 이벤트 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnStartPacking_Click(object sender, EventArgs e)
		{
			DataTable packingLotList = grdWaitPackingLotList.DataSource as DataTable;
			if(packingLotList.Rows.Count == 0)
			{
				// 저장할 데이터가 존재하지 않습니다.
				throw MessageException.Create("NoSaveData");
			}

			if(string.IsNullOrEmpty(txtBoxNo.Text))
			{
				//박스번호가 생성되지 않았습니다.
				throw MessageException.Create("NotCreateBoxNo");
			}

			//현재 LOT의 불량 추가 등록
			decimal currentQty = Format.GetDecimal(numDefectQty.EditValue);
			if (!currentQty.Equals(0))
			{
				defectListForPacking.StorageDefectLotTempTable();
			}

			DataTable dtDefectLot = defectListForPacking.GetDefectLotSaveData();

			//포장 작업 시작
			MessageWorker worker = new MessageWorker("SaveBoxPacking");
			worker.SetBody(new MessageBody()
			{
				{ "EnterpriseId", UserInfo.Current.Enterprise },
				{ "PlantId", UserInfo.Current.Plant },
				{ "EquipmentClassId", "PPP1" },
				{ "EquipmentId", "PPP1-TEST" },
				{ "BoxNo", txtBoxNo.Text },
				{ "packageType", _packageType },
				{ "lotDefectList", dtDefectLot },
				{ "packingLotList", packingLotList }
			});

			worker.Execute();
			ShowMessage("SuccedSave");

			//라벨 출력
			string packingLabelLotId = packingLotList.Rows[0]["LOTID"].ToString();
			SmartMES.Commons.CommonFunction.printPackingLabel(packingLabelLotId);

			//포장 작업 알림 해제
			_isDoPackingStart = false;

			txtLotId.Text = "";
			txtBoxNo.ReadOnly = false;
			ClearDetailInfo();
			ChangeEnableAllButtons(false);

			defectListForPacking.ClearDatas();
			defectListForPacking.ClearDefectLotTemp();

			//포장타입 초기화
			_packageType = string.Empty;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TxtLotId_KeyDown(object sender, KeyEventArgs e)
		{
			btnPrintLabel.Enabled = false;

			if (e.KeyCode == Keys.Enter)
			{
				if (_isDoPackingStart)
				{
					//포장 대기중인 LOT이 존재합니다. 포장 작업을 완료해주십시요.
					ShowMessage("WaitingPackingLotList");
					txtLotId.Text = "";
					return;
				}

				DataTable currentDefectLotList = defectListForPacking.GetDefectListData();
				if(currentDefectLotList.Rows.Count > 0)
				{
					//불량 LOT 임시 저장
					defectListForPacking.StorageDefectLotTempTable();
				}

				Dictionary<string, object> param = new Dictionary<string, object>();
				param.Add("LotId", txtLotId.Text);
				param.Add("LanguageType", UserInfo.Current.LanguageType);
				param.Add("EnterpriseId", UserInfo.Current.Enterprise);
				param.Add("PlantId", UserInfo.Current.Plant);
				param.Add("ProcessState", "WaitForReceive,Wait,Run");

				DataTable dtlotInfo = SqlExecuter.Query("SelectLotInfoByProcessForPacking", "10001", param);
				if(dtlotInfo.Rows.Count < 1)
				{
					// 조회할 데이터가 없습니다.
					ShowMessage("NoSelectData");
					txtLotId.Text = "";
					//ClearDetailInfo();
					return;
				}

				//DataTable to Dictionary
				Dictionary<string, string> pairs = GetDictionary(dtlotInfo);

				//LOT 작업장 비교
				bool isSuccess = CheckLotArea(pairs["AREAID"]);
				if (!isSuccess)
				{
					txtLotId.Text = "";
					return;
				}
					

				//포장 가능 상태 체크
				isSuccess = AvailablePacking(dtlotInfo);
				if( !isSuccess  )
				{
					txtLotId.Text = "";
					return;
				}

				//같은 품목 체크
				isSuccess = CheckSameProductDef(pairs["PRODUCTDEFID"], pairs["PRODUCTDEFVERSION"]);
				if (!isSuccess)
				{
					txtLotId.Text = "";
					return;
				} 


				//Box 사양 체크
				param.Clear();
				param.Add("CustomerId", pairs["CUSTOMERID"]);
				param.Add("ProductDefId", pairs["PRODUCTDEFID"]);
				param.Add("ProductDefVersion", pairs["PRODUCTDEFVERSION"]);

				double lotQty = Format.GetDouble(pairs["QTY"], 0);
				DataTable dtBoxSpce = SqlExecuter.Query("GetPackingQty", "10001", param);
				if (dtBoxSpce.Rows.Count < 1)
				{
					//해당 제품의 박스 사양이 등록되어 있지않습니다. 
					ShowMessage("NotExistPackingProduct");
					txtLotId.Text = "";
					return;
				}

				_packageType = dtBoxSpce.AsEnumerable().Select(r => Format.GetString(r["PACKAGETYPE"])).Distinct().FirstOrDefault();
				double packingQty = dtBoxSpce.AsEnumerable().Select(r => Format.GetDouble(r["PACKINGQTY"], 0)).Distinct().FirstOrDefault();
				double summaryQty = (grdWaitPackingLotList.DataSource as DataTable).AsEnumerable().Sum(r=>r.Field<double>("QTY"));
				if(grdWaitPackingLotList.View.RowCount > 0) summaryQty += lotQty;

				if (packingQty == 0)
				{
					//포장 수량은(는) 0일수 없습니다. 수량을 확인하여 주십시오.
					ShowMessage("CheckQty", Language.Get("PACKINGQTY"), string.Format("Packing Qty : {0}", packingQty));
					txtLotId.Text = "";
					return;
				}
				else if (lotQty == 0)
				{
					//LOT 수량은(는) 0일수 없습니다. 수량을 확인하여 주십시오.
					ShowMessage("CheckQty", Language.Get("LOTQTY"), string.Format("LOT Qty : {0}", lotQty));
					txtLotId.Text = "";
					return;
				}
				else if(summaryQty > packingQty)
				{
					//박스 수량을(를) 초과하였습니다.
					ShowMessage("OverQty", Language.Get("BOXQTY"));

					//포장 작업 알림 설정
					//_isDoPackingStart = true면 포장 작업 수행 해야 됨!
					_isDoPackingStart = true;
					txtLotId.Text = "";
					return;
				}
				else if (packingQty < lotQty)
				{
					Dictionary<string, object> options = new Dictionary<string, object>();
					options.Add("LOTQTY", lotQty);
					options.Add("PACKINGQTY", packingQty);

					//분할 팝업
					SplitLotForPacking splitLotPacking = new SplitLotForPacking(dtlotInfo, options);
					DialogResult result = splitLotPacking.ShowDialog();
					if (result.Equals(DialogResult.Cancel))
					{
						txtLotId.Text = "";
						tabPartition.Enabled = false;
					}
				}
				else
				{
					//현재 LOT의 양품 수량
					_currentGoodQty = Format.GetDouble(pairs["QTY"], 0);

					////작업자 조회
					//string areaId = pairs["AREAID"].ToString();
					//bool isEmptyUser = false;
					//InitializeWorkerCombo(out isEmptyUser, areaId);
					//if (isEmptyUser) return;

					//포장작업
					isSuccess = SetPackingGridData(dtlotInfo);
					if (!isSuccess) return;

					//중앙 controls
					SetDataBindCenterControls(dtlotInfo);

					//불량
					SetDefectListData(dtlotInfo);
					defectListForPacking.SetDictionaryPairs(pairs);

					//LOT Info 바인드
					grdLotInfo.DataSource = dtlotInfo;

					//LOT ID 초기화, Enabled = true
					txtLotId.Text = "";
					tabPartition.Enabled = true;
					btnCreateBoxNo.Enabled = true;
					btnSaveLot.Enabled = true;
					btnStartPacking.Enabled = true;
				}

			}
		}

		/// <summary>
		/// 불량수량 변경 이벤트
		/// </summary>
		/// <param name="pcsQty"></param>
		private void DefectListForPacking_EditValueChange(double pcsQty, out CancelEventArgs e)
		{
			e = new CancelEventArgs();
		
			if(_currentGoodQty < pcsQty)
			{
				//불량 수량은(는) 양품수량보다 많을 수 없습니다.
				ShowMessage("CanNotToMuch", Language.Get("DEFECTQTY"), Language.Get("GOODQTY"));
				e.Cancel = true;
				return;
			}
			
			double goodQty = _currentGoodQty;
			goodQty -= pcsQty;

			numGoodQty.Text = goodQty.ToString();
			numDefectQty.Text = pcsQty.ToString();

			//포장작업 LOT 수량 변경
			DataTable lotInfo = grdLotInfo.DataSource as DataTable;
			string lotId = lotInfo.Rows[0]["LOTID"].ToString();
			DataTable lotList = grdWaitPackingLotList.DataSource as DataTable;
			foreach(DataRow r in lotList.Rows)
			if(r["LOTID"].Equals(lotId))
			{
				r["DEFECTQTY"] = numDefectQty.EditValue;
				r["QTY"] = numGoodQty.EditValue;
				break;
			}

		}

		#endregion

		#region Private Function

		/// <summary>
		/// 포장 가능 상태 체크
		/// </summary>
		/// <param name="dt"></param>
		private bool AvailablePacking(DataTable dt)
		{
			string lotId = dt.Rows[0]["LOTID"].ToString();

			if (dt.Rows[0]["ISLOCKING"].Equals("Y"))
			{
				//해당 LOT은 Locking 상태입니다.
				ShowMessage("InLockingState", string.Format("LOT ID : {0}", lotId));
				return false;
			}

			if (dt.Rows[0]["ISHOLD"].Equals("Y"))
			{
				//해당 LOT은 Hold 상태입니다.
				ShowMessage("InHoldState", string.Format("LOT ID : {0}", lotId));
				return false;
			}

			if (!dt.Rows[0]["LOTSTATE"].Equals("InProduction"))
			{
				//해당 LOT은 양산 타입이 아닙니다.
				ShowMessage("");
				return false;
			}

			//공정 스텝 확인
			string stepType = "";
			string processsegmentId = dt.Rows[0]["PROCESSSEGMENTID"].ToString();
			string processsegmentVersion = dt.Rows[0]["PROCESSSEGMENTVERSION"].ToString();
			string[] types = GetProcessStepType(processsegmentId, processsegmentVersion, out stepType);

			string processState = dt.Rows[0]["PROCESSSTATE"].ToString();
			bool isSuccess = false;

			switch(types.Length)
			{
				case 1:
					if(processState.Equals("WaitForReceive") && types[0].Equals("Run"))
					{ isSuccess = true; }
					break;
				case 2:
					if((processState.Equals("WaitForReceive") && types[0].Equals("Run") && types[1].Equals("WaitForSend"))
					|| (processState.Equals("Wait") && types[0].Equals("WaitForReceive") && types[1].Equals("Run"))
					|| (processState.Equals("Run") && types[0].Equals("Wait") && types[1].Equals("Run")))
					{ isSuccess = true; }
					break;
				case 3:
					if((processState.Equals("Wait") && types[0].Equals("WaitForReceive") && types[1].Equals("Run") && types[2].Equals("WaitForSend"))
					|| (processState.Equals("Run") && types[0].Equals("Wait") && types[1].Equals("Run") && types[2].Equals("WaitForSend"))
					|| (processState.Equals("Run") && types[0].Equals("WaitForReceive") && types[1].Equals("Run") && types[2].Equals("WaitForSend")))
					{ isSuccess = true; }
					break;
				case 4:
					if(processState.Equals("Run") && types[0].Equals("WaitForReceive") && types[1].Equals("Wait") && types[2].Equals("Run") && types[3].Equals("WaitForSend"))
					{ isSuccess = true; }
					break;
				default:
					isSuccess = false;
					break;
			}

			if (!isSuccess)
			{
				//포장을 진행할 수 없는 공정입니다. 공정 스텝을 확인하여 주십시오.
				ShowMessage("NotAvailablePacking", string.Format("Step : {0}  LotState : {1}", stepType, processState));
			}

			return isSuccess;
		}

		private string[] GetProcessStepType(string processsegmentId, string processsegmentVersion, out string stepType)
		{
			Dictionary<string, object> values = new Dictionary<string, object>();
			values.Add("ProcesssegmentId", processsegmentId);
			values.Add("ProcesssegmentVersion", processsegmentVersion);
			DataTable dtStepType = SqlExecuter.Query("GetStepTypeByProcesSeg", "10001", values);

			stepType = dtStepType.Rows[0]["STEPTYPE"].ToString();
			string[] stepTypes = stepType.Split(',');

			return stepTypes;
		}

		/// <summary>
		/// 같은 품목 체크
		/// </summary>
		/// <param name="productDefId"></param>
		/// <param name="productDefVersion"></param>
		/// <returns></returns>
		private bool CheckSameProductDef(string productDefId, string productDefVersion)
		{
			bool isSuccess = false;
			DataTable dtPackingList = grdWaitPackingLotList.DataSource as DataTable;
			if (dtPackingList.Rows.Count == 0 ||
				(string.IsNullOrEmpty(_productDefId) && string.IsNullOrEmpty(_productDefVersion)))
			{
				_productDefId = productDefId;
				_productDefVersion = productDefVersion;
				isSuccess = true;
			}
			else if ( !_productDefId.Equals(productDefId) && _productDefVersion.Equals(productDefVersion) )
			{
				//품목코드가 일치하지 않습니다. 품목코드를 확인하여 주십시오. {0}
				ShowMessage("CheckProductDefId", string.Format("Product Code : {0}, Rev. : {1}", productDefId, productDefVersion));
				isSuccess = false;
			}
			else
			{ isSuccess = true; }

			return isSuccess;
		}

		/// <summary>
		/// 사용 변수 DataTable to Dictionary
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		private Dictionary<string, string> GetDictionary(DataTable dt)
		{
			Dictionary<string, string> pairs = new Dictionary<string, string>();
			for (int i = 0; i < dt.Columns.Count; i++)
			{
				pairs.Add(dt.Columns[i].ColumnName, dt.Rows.Cast<DataRow>().Select(k => Convert.ToString(k[dt.Columns[i]])).First());
			}
			return pairs;
		}

		/// <summary>
		/// LOT 작업장 체크
		/// </summary>
		/// <param name="areaId"></param>
		/// <returns></returns>
		private bool CheckLotArea(string lotAreaId)
		{
			string currentAreaId = Format.GetString(popArea.GetValue(), "");
			if(string.IsNullOrEmpty(currentAreaId))
			{
				//작업장을 선택하세요.
				ShowMessage("NoAreaSelected");
				return false;
			}

			string currentUserId = Format.GetString(cboUser.GetDataValue(), "");
			if(string.IsNullOrEmpty(currentUserId))
			{
				//작업자를 선택하세요.
				ShowMessage("NoSelectWorker");
				return false;
			}

			if(!lotAreaId.Equals(currentAreaId))
			{
				//현재 작업장과 LOT 작업장이 일치하지 않습니다.
				ShowMessage("NotEqualLotArea");
				return false;
			}

			return true;
		}

		/// <summary>
		/// 불량 탭 그리드 데이터 바인드 및 탭 활성화
		/// </summary>
		/// <param name="dt"></param>
		private void SetDefectListData(DataTable dt)
		{
			string lotId = dt.AsEnumerable().First().Field<string>("LOTID");

			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("LotId", lotId);
			param.Add("EnterpriseId", UserInfo.Current.Enterprise);
			param.Add("PlantId", UserInfo.Current.Plant);
			param.Add("ProcessSegmentClassType", "MiddleProcessSegmentClass");
			param.Add("ResourceType", "QCSegmentID");

			DataTable dtDefectList = SqlExecuter.Query("SelectDefectByProcess", "10001", param);

			defectListForPacking.SetDefectListData(dtDefectList);

			//탭 활성화
			DevExpress.XtraTab.XtraTabPageCollection pageCollection = tabPartition.TabPages;
			if(tabPartition.SelectedTabPage.Name != "DefectList")
			{
				tabPartition.SelectedTabPage = (pageCollection[1]);
			}
			
		}

		/// <summary>
		/// 포장작업 탭 그리드 데이터 바인드
		/// </summary>
		/// <param name="dt"></param>
		private bool SetPackingGridData(DataTable dt)
		{
			if(string.IsNullOrEmpty(cboUser.EditValue.ToString()))
			{
				//작업자를 선택하여 주십시오.
				ShowMessage("SelectUser");
				return false;
			}

			DataTable dataTable = dt.Copy();
			dataTable.Columns.Add("USERID", typeof(string));
			dataTable.Columns.Add("USERNAME", typeof(string));
			dataTable.Columns.Add("TXNTIME", typeof(string));
			dataTable.Rows[0]["USERID"] = cboUser.GetDataValue();
			dataTable.Rows[0]["USERNAME"] = cboUser.Text;
			dataTable.Rows[0]["TXNTIME"] = string.Format("{0:yyyy-MM-dd}", DateTime.Now); 

			DataTable lotList = grdWaitPackingLotList.DataSource as DataTable;
			if(lotList.Rows.Count >= 1)
			{
				string lotId = dataTable.Rows[0]["LOTID"].ToString();
				var list = lotList.AsEnumerable().Where(r => r.Field<string>("LOTID").Equals(lotId)).ToList();
				if(list.Count > 0)
				{
					//이미 추가 된 LOT 입니다.
					ShowMessage("ExistLot", string.Format("LOT ID : {0}", lotId));
					return false;
				}

				lotList.ImportRow(dataTable.Rows[0]);
				return true;
			}
			else
			{
				grdWaitPackingLotList.DataSource = dataTable;
				return true;
			}

			//lotList.AcceptChanges();
		}

		/// <summary>
		/// Panel Controls 데이터 바인드 
		/// </summary>
		/// <param name="dt"></param>
		private void SetDataBindCenterControls(DataTable dt)
		{
			string unit = dt.AsEnumerable().First().Field<string>("UNIT");
			double qty = dt.AsEnumerable().First().Field<double>("QTY");

			txtUnit.Text = unit;
			numGoodQty.EditValue = qty;
			numDefectQty.EditValue = 0;
		}

		/// <summary>
		/// 작업자 콤보 초기화
		/// </summary>
		private void InitializeWorkerCombo(out bool isEmptyUser, string areaId = "")
		{
			isEmptyUser = false;
			if (cboUser.DataSource != null) return;

			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("AreaId", areaId);
			param.Add("EnterpriseId", UserInfo.Current.Enterprise);
			param.Add("PlantId", UserInfo.Current.Plant);

			cboUser.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
			cboUser.ValueMember = "USERID";
			cboUser.DisplayMember = "WORKERNAME";
			cboUser.DataSource = SqlExecuter.Query("GetAreaworkerList", "10001", param);
			cboUser.ShowHeader = false;

			DataTable dtuser = cboUser.DataSource as DataTable;
			if(dtuser.Rows.Count == 0)
			{
				//현 작업장에 작업자가 없습니다. 작업자 등록 후 사용 가능합니다.
				ShowMessage("UserEmptyInArea");
				isEmptyUser = true;
				return;
			}

			List<DataRow> list = dtuser.AsEnumerable().Where(r => r["USERID"].Equals(UserInfo.Current.Id)).ToList();
			cboUser.EditValue = (list.Count > 0) ? list[0]["USERID"].ToString() : dtuser.Rows[0]["USERID"];
		}

		/// <summary>
		/// 데이터 초기화
		/// </summary>
		private void ClearDetailInfo()
		{
			grdLotInfo.ClearData();
			grdWaitPackingLotList.View.ClearDatas();
			txtBoxNo.Text = "";
			defectListForPacking.ClearDatas();
			ClearPanelControls();
		}

		/// <summary>
		/// 데이터 초기화
		/// </summary>
		private void ClearPanelControls()
		{
			txtUnit.Text = "";
			numGoodQty.Text = "0";
			numDefectQty.Text = "0";
			txtComment.Text = "";
		}

		/// <summary>
		/// 버튼 Enable
		/// </summary>
		/// <param name="isEnable"></param>
		private void ChangeEnableAllButtons(bool isEnable)
		{
			btnCreateBoxNo.Enabled = isEnable;
			btnPrintLabel.Enabled = isEnable;
			btnStartPacking.Enabled = isEnable;
			btnSaveLot.Enabled = isEnable;
		}
		#endregion
	}
}