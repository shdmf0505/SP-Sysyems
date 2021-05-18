#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using System;
using Micube.SmartMES.Commons.Controls;
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
	/// 프 로 그 램 명  : 공정관리 > 포장관리 > 재포장 등록
	/// 업  무  설  명  : 이미 Box포장된 Box번호를 기준으로 데이터 조회 후 조회된 LOT을 다시 신규 Box번호로 재포장하는 화면
	///						 Box번호가 없을 경우 신규로 생성을 눌러 Box포장 실적만 저장
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-08-02
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class RePackingResult : SmartConditionManualBaseForm
	{
		#region Local Variables

		#endregion

		#region 생성자

		public RePackingResult()
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

			InitializeGrid();
			InitializePopupArea();

			//버튼 Enable
			ChangeEnableAllButtons(false);
		}

		/// <summary>        
		/// 그리드 초기화
		/// </summary>
		private void InitializeGrid()
		{
			InitializeGridPervPackingLot();
			InitializeGridRePackingLot();

			this.ucDataUpDownBtn.SourceGrid = this.grdPrevPackingLotList;
			this.ucDataUpDownBtn.TargetGrid = this.grdRePackingLotList;
		}

		/// <summary>
		/// 이전 포장 리스트 그리드 초기화
		/// </summary>
		private void InitializeGridPervPackingLot()
		{
			grdPrevPackingLotList.GridButtonItem = GridButtonItem.None;
			grdPrevPackingLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
			grdPrevPackingLotList.View.SetIsReadOnly(true);

			//BOX NO
			grdPrevPackingLotList.View.AddTextBoxColumn("BOXNO")
				.SetIsHidden();
			//작업자
			grdPrevPackingLotList.View.AddTextBoxColumn("WORKER")
				.SetIsHidden();
			//품목코드
			grdPrevPackingLotList.View.AddTextBoxColumn("PRODUCTDEFID", 150);
			//품목명
			grdPrevPackingLotList.View.AddTextBoxColumn("PRODUCTDEFNAME", 250);
			//Rev.
			grdPrevPackingLotList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
			//LOT ID
			grdPrevPackingLotList.View.AddTextBoxColumn("LOTID", 250);
			//수량
			grdPrevPackingLotList.View.AddTextBoxColumn("QTY", 100);
			//양품수량
			grdPrevPackingLotList.View.AddTextBoxColumn("GOODQTY", 100);
			//불량수량
			grdPrevPackingLotList.View.AddTextBoxColumn("DEFECTQTY", 100);
			//작업장
			grdPrevPackingLotList.View.AddTextBoxColumn("AREAID")
				.SetIsHidden();
			//작업장명
			grdPrevPackingLotList.View.AddTextBoxColumn("AREANAME")
				.SetIsHidden();

			grdPrevPackingLotList.View.PopulateColumns();
		}

		/// <summary>
		/// 재포장 리스트 그리드 초기화
		/// </summary>
		private void InitializeGridRePackingLot()
		{
			grdRePackingLotList.GridButtonItem = GridButtonItem.None;
			grdRePackingLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
			grdRePackingLotList.View.SetIsReadOnly(true);

			//BOX NO
			grdRePackingLotList.View.AddTextBoxColumn("BOXNO")
				.SetIsHidden();
			//작업자
			grdRePackingLotList.View.AddTextBoxColumn("WORKER")
				.SetIsHidden();
			//품목코드
			grdRePackingLotList.View.AddTextBoxColumn("PRODUCTDEFID", 150);
			//품목명
			grdRePackingLotList.View.AddTextBoxColumn("PRODUCTDEFNAME", 250);
			//Rev.
			grdRePackingLotList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
			//LOT ID
			grdRePackingLotList.View.AddTextBoxColumn("LOTID", 250);
			//수량
			grdRePackingLotList.View.AddTextBoxColumn("QTY", 100);
			//양품수량
			grdRePackingLotList.View.AddTextBoxColumn("GOODQTY", 100);
			//불량수량
			grdRePackingLotList.View.AddTextBoxColumn("DEFECTQTY", 100);
			//작업장
			grdRePackingLotList.View.AddTextBoxColumn("AREAID")
				.SetIsHidden();
			//작업장명
			grdRePackingLotList.View.AddTextBoxColumn("AREANAME")
				.SetIsHidden();

			grdRePackingLotList.View.PopulateColumns();
		}

		/// <summary>
		/// 작업장 팝업 초기화 및 작업자 초기화
		/// </summary>
		private void InitializePopupArea()
		{
			ConditionItemSelectPopup areaCondition = new ConditionItemSelectPopup();
			areaCondition.Id = "AREA";
            areaCondition.SearchQuery = new SqlQuery("GetAreaListByAuthority", "10001", $"PLANTID={UserInfo.Current.Plant}", "ISMODIFY=Y", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
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
		}
		#endregion

		#region Event

		/// <summary>
		/// 화면에서 사용되는 이벤트를 초기화한다.
		/// </summary>
		public void InitializeEvent()
		{
			txtPrevBoxNo.KeyDown += TxtPrevBoxNo_KeyDown;
			btnCreateBoxNo.Click += BtnCreateBoxNo_Click;
			btnPrintLabel.Click += BtnPrintLabel_Click;
			btnStartRePacking.Click += BtnStartRePacking_Click;
		}

		/// <summary>
		/// (재)포장 작업 완료
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnStartRePacking_Click(object sender, EventArgs e)
		{
			DataTable rePackingLotList = grdRePackingLotList.DataSource as DataTable;
			if(rePackingLotList.Rows.Count == 0)
			{
				// 저장할 데이터가 존재하지 않습니다.
				throw MessageException.Create("NoSaveData");
			}

			if (string.IsNullOrEmpty(txtNewBoxNo.Text))
			{
				//박스번호가 생성되지 않았습니다.
				throw MessageException.Create("NotCreateBoxNo");
			}

			MessageWorker worker = new MessageWorker("SaveBoxRePacking");
			worker.SetBody(new MessageBody()
			{
				//{ "EnterpriseId", UserInfo.Current.Enterprise },
				//{ "PlantId", UserInfo.Current.Plant },
				{ "Worker", cboUser.GetDataValue() },
				{ "BoxNo", txtNewBoxNo.Text },
				{ "rePackingLotList", rePackingLotList }
			});

			worker.Execute();
			ShowMessage("SuccedSave");

			//라벨 출력
			string packingLabelLotId = rePackingLotList.Rows[0]["LOTID"].ToString();
			SmartMES.Commons.CommonFunction.printPackingLabel(packingLabelLotId);

			//데이터 초기화
			ClearDatasAfterRePacking();
		}

		/// <summary>
		/// 라벨 출력
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnPrintLabel_Click(object sender, EventArgs e)
		{
			
		}

		/// <summary>
		/// 신규 Box 라벨 생성
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnCreateBoxNo_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(txtNewBoxNo.Text))
			{
				//박스번호가 존재합니다.
				ShowMessage("ExistBoxNo");
				return;
			}

			string area = Format.GetString(popArea.GetValue(), "");
			if(string.IsNullOrEmpty(area))
			{
				//작업장을 선택하세요.
				ShowMessage("NoAreaSelected");
				return;
			}

			string worker = Format.GetString(cboUser.GetDataValue(), "");
			if (string.IsNullOrEmpty(worker))
			{
				//작업자를 선택하여 주십시오.
				ShowMessage("SelectUser");
				return;
			}

			//박스No 채번
			MessageWorker messageWorker = new MessageWorker("IdCreate");
			messageWorker.SetBody(new MessageBody()
			{
				{ "IDCLASSID", "BoxNoSequence" }
			});
			var dtBoxNo = messageWorker.Execute<DataTable>().GetResultSet();
			string boxId = dtBoxNo.AsEnumerable().First().Field<string>("ID");

			//존재 여부 확인
			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("BOXNO", boxId);

			DataTable dtExistBoxNo = SqlExecuter.Query("GetBoxNo", "10001", param);
			if (dtExistBoxNo.Rows.Count > 1)
			{
				//해당 Box 번호는 이미 존재합니다.
				ShowMessage("ExistBoxNo", string.Format("Box No : {0}", txtNewBoxNo.Text));
				return;
			}

			txtNewBoxNo.Text = boxId;

			btnPrintLabel.Enabled = true;
			btnStartRePacking.Enabled = true;
			btnCreateBoxNo.Enabled = false;
			
			txtNewBoxNo.ReadOnly = true;
			txtPrevBoxNo.ReadOnly = true;
		}

		/// <summary>
		/// 이전 포장 리스트 조회
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TxtPrevBoxNo_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter && string.IsNullOrEmpty(txtNewBoxNo.Text))
			{
				Dictionary<string, object> param = new Dictionary<string, object>();
				param.Add("BoxNo", txtPrevBoxNo.Text);
				param.Add("LanguageType", UserInfo.Current.LanguageType);

				DataTable rePackingList = SqlExecuter.Query("SelectRePackingLotList", "10001", param);
				if(rePackingList.Rows.Count < 1)
				{
					//해당 박스에 재포장 할 LOT 리스트가 없습니다.
					ShowMessage("NotExistRePackingLot");
					return;
				}

				grdPrevPackingLotList.DataSource = rePackingList;

				//작업장, 작업자 자동 바인드
				string areaId = Format.GetString(rePackingList.Rows[0]["AREAID"], "");
				popArea.SetValue(areaId);
				popArea.Text = Format.GetString(rePackingList.Rows[0]["AREANAME"], "");

				//작업자 조회
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


				//버튼 사용 처리
				btnCreateBoxNo.Enabled = true;
			}
		}


		#endregion

		#region Private Function

		/// <summary>
		/// 포장작업 완료 후 초기화 작업
		/// </summary>
		private void ClearDatasAfterRePacking()
		{
			grdRePackingLotList.View.ClearDatas();
			txtNewBoxNo.Text = "";
			txtNewBoxNo.ReadOnly = false;
			btnCreateBoxNo.Enabled = true;
			btnStartRePacking.Enabled = false;
			btnPrintLabel.Enabled = false;

			DataTable dt = grdPrevPackingLotList.DataSource as DataTable;
			if(dt.Rows.Count == 0)
			{
				txtPrevBoxNo.Text = "";
				txtPrevBoxNo.ReadOnly = false;
				btnCreateBoxNo.Enabled = false;
			}
		}

		/// <summary>
		/// 버튼 Enable
		/// </summary>
		/// <param name="isEnable"></param>
		private void ChangeEnableAllButtons(bool isEnable)
		{
			btnCreateBoxNo.Enabled = isEnable;
			btnPrintLabel.Enabled = isEnable;
			btnStartRePacking.Enabled = isEnable;
		}

		#endregion
	}
}
