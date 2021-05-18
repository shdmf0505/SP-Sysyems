 #region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
	/// <summary>
	/// 프 로 그 램 명  : 공정관리 > 자재관리 > 자재불출요청
	/// 업  무  설  명  : LOT 투입별 / 각 공정별로 자재불출요청을 할 수 있다.
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-06-28
	/// 수  정  이  력  : 2019-11-27 정승원 CT_CONSUMEMATERIALLOT_TEMP에 소모된 자재 제외 후 청구
	/// 
	/// </summary>
	public partial class RequestConsumableRelease : SmartConditionManualBaseForm
	{
		#region Local Variables

		#endregion

		#region 생성자
		public RequestConsumableRelease()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 다른 화면에서 파라미터 넘길 때
		/// </summary>
		/// <param name="parameters"></param>
		public override void LoadForm(Dictionary<string, object> parameters)
		{
			base.LoadForm(parameters);

			if (parameters != null)
			{
				string areaId = Format.GetString(parameters["AREAID"]);
                string inputdate = DateTime.Parse(Format.GetString(parameters["INPUTDATE"])).ToString("yyyy-MM-dd");

				if(!string.IsNullOrWhiteSpace(areaId) && !string.IsNullOrWhiteSpace(inputdate))
				{
					Conditions.GetControl<SmartSelectPopupEdit>("P_AREAID").SetValue(areaId);
					Conditions.GetControl<SmartSelectPopupEdit>("P_AREAID").Text = Format.GetString(parameters["AREANAME"]);
                    Conditions.GetControl<SmartPeriodEdit>("P_PERIOD").datePeriodFr.EditValue = inputdate;

                    OnSearchAsync();
				}
			}
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
		}

		/// <summary>        
		/// 그리드를 초기화한다.
		/// </summary>
		private void InitializeGrid()
		{
			// TODO : 그리드 초기화 로직 추가

			InitGridInputList();

			InitGridLotList1();
			InitGridLotList2();

			InitGridComsumeList1();
			InitGridComsumeList2();

			InitSummaryRow1();
			InitSummaryRow2();

			//헤더 숨김
			tabLotList.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
		}


		/// <summary>
		/// 작업장별 - 투입현황 그리드 초기화
		/// </summary>
		private void InitGridInputList()
		{
			grdRequestList.GridButtonItem = GridButtonItem.Export;
			grdRequestList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
			grdRequestList.View.SetIsReadOnly();

			//청구여부
			grdRequestList.View.AddComboBoxColumn("ISREQUEST", 60, new SqlQuery("GetTypeList", "10001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
				.SetTextAlignment(TextAlignment.Center).SetLabel("ISPRINTCHIT");
            //청구번호
            grdRequestList.View.AddTextBoxColumn("REQUESTNO", 100).SetTextAlignment(TextAlignment.Center).SetLabel("REQUESTID");
            //양산구분
            grdRequestList.View.AddTextBoxColumn("LOTTYPE", 80).SetTextAlignment(TextAlignment.Center);
			//품목코드
			grdRequestList.View.AddTextBoxColumn("PRODUCTDEFID", 130);
			//품목버전
			grdRequestList.View.AddTextBoxColumn("PRODUCTDEFVERSION").SetIsHidden();
			//품목명
			grdRequestList.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);
			//공정순서
			grdRequestList.View.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center);
			//공정
			grdRequestList.View.AddTextBoxColumn("PROCESSSEGMENTID").SetIsHidden();
			//공정버전
			grdRequestList.View.AddTextBoxColumn("PROCESSSEGMENTVERSION").SetIsHidden();
			//공정명
			grdRequestList.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 130);
			//작업장
			grdRequestList.View.AddTextBoxColumn("AREAID", 130).SetIsHidden();
			grdRequestList.View.AddTextBoxColumn("AREANAME", 130).SetLabel("AREA");
			//단위
			grdRequestList.View.AddTextBoxColumn("UNIT", 50).SetLabel("UOM").SetTextAlignment(TextAlignment.Center);
			//LOT 수
			grdRequestList.View.AddSpinEditColumn("LOTCNT", 50).SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
			//투입수량(PNL)
			grdRequestList.View.AddSpinEditColumn("PANELQTY", 100).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetLabel("INPUTPNL");
			//투입수량(PCS)
			grdRequestList.View.AddSpinEditColumn("QTY", 100).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("INPUTPCS");
			//투입수량(MM)
			grdRequestList.View.AddSpinEditColumn("MM", 100).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetLabel("INPUTMM");

			grdRequestList.View.PopulateColumns();
		}

		/// <summary>
		/// 작업장별 - LOT 리스트 그리드 초기화
		/// </summary>
		private void InitGridLotList1()
		{
			grdLotList1.GridButtonItem = GridButtonItem.Export;
			grdLotList1.ShowStatusBar = false;
			grdLotList1.View.SetIsReadOnly();

			//품목코드
			grdLotList1.View.AddTextBoxColumn("PRODUCTDEFID", 130);
			//품목명
			grdLotList1.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);
			//LOTID
			grdLotList1.View.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);
			//소요량
			grdLotList1.View.AddSpinEditColumn("BOMQTY", 100).SetLabel("REQUIREMENTQTY").SetDisplayFormat("#,##0.000000000", MaskTypes.Numeric, true);
			//PNL
			grdLotList1.View.AddSpinEditColumn("PANELQTY", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric);
			//PCS
			grdLotList1.View.AddSpinEditColumn("QTY", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric);
            //자재코드
            grdLotList1.View.AddTextBoxColumn("MATERIALDEFID", 130);
            //자재명
            grdLotList1.View.AddTextBoxColumn("MATERIALDEFNAME", 200);
            //현재고량
            grdLotList1.View.AddSpinEditColumn("STOCKQTY", 100).SetLabel("NOWSTOCKQTY").SetDisplayFormat("#,##0.000000000", MaskTypes.Numeric, true);
			//청구수량
			grdLotList1.View.AddSpinEditColumn("CHARGEQTY", 100).SetDisplayFormat("#,##0.000000000", MaskTypes.Numeric, true);
			//투입일시
			grdLotList1.View.AddSpinEditColumn("INPUTDATE", 130).SetTextAlignment(TextAlignment.Center);
			//작업장
			grdLotList1.View.AddTextBoxColumn("AREAID").SetIsHidden();
			//자재
			grdLotList1.View.AddSpinEditColumn("CONSUMEDQTY").SetIsHidden();

			grdLotList1.View.PopulateColumns();
		}

		/// <summary>
		/// 작업장별 - LOT 리스트 그리드 초기화
		/// </summary>
		private void InitGridLotList2()
		{
			grdLotList2.GridButtonItem = GridButtonItem.Export;
			grdLotList2.ShowStatusBar = false;
			grdLotList2.View.SetIsReadOnly();

			//품목코드
			grdLotList2.View.AddTextBoxColumn("PRODUCTDEFID", 130);
			//품목명
			grdLotList2.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);
			//LOTID
			grdLotList2.View.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);
			//소요량
			grdLotList2.View.AddSpinEditColumn("BOMQTY", 100).SetLabel("REQUIREMENTQTY").SetDisplayFormat("#,##0.000000000", MaskTypes.Numeric, true);
			//PNL
			grdLotList2.View.AddSpinEditColumn("PANELQTY", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric);
			//PCS
			grdLotList2.View.AddSpinEditColumn("QTY", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric);
			//현재고량
			grdLotList2.View.AddSpinEditColumn("STOCKQTY", 100).SetLabel("NOWSTOCKQTY").SetDisplayFormat("#,##0.000000000", MaskTypes.Numeric, true);
			//청구수량
			grdLotList2.View.AddSpinEditColumn("CHARGEQTY", 100).SetDisplayFormat("#,##0.000000000", MaskTypes.Numeric, true);
			//투입일시
			grdLotList2.View.AddSpinEditColumn("INPUTDATE", 130).SetTextAlignment(TextAlignment.Center);
			//작업장
			grdLotList2.View.AddTextBoxColumn("AREAID").SetIsHidden();
			//자재
			grdLotList2.View.AddTextBoxColumn("MATERIALDEFID").SetIsHidden();
			grdLotList2.View.AddTextBoxColumn("MATERIALDEFVERSION").SetIsHidden();
			grdLotList2.View.AddSpinEditColumn("CONSUMEDQTY").SetIsHidden();

			grdLotList2.View.PopulateColumns();
		}

		/// <summary>
		/// 자재DEFID별 현 재고량 기준테이블
		/// </summary>
		private void InitGridComsumeList1()
		{
			grdConsumeList1.GridButtonItem = GridButtonItem.None;
			grdConsumeList1.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

			grdConsumeList1.View.SetIsReadOnly();
			grdConsumeList1.ShowStatusBar = false;
			grdConsumeList1.View.AddTextBoxColumn("CONSUMABLEDEFID", 100);
			grdConsumeList1.View.AddTextBoxColumn("CONSUMABLEDEFVERSION").SetIsHidden();
			grdConsumeList1.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 150);
			grdConsumeList1.View.AddTextBoxColumn("UOM", 50).SetTextAlignment(TextAlignment.Center);
			grdConsumeList1.View.AddSpinEditColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.000000000", MaskTypes.Numeric, true);

			grdConsumeList1.View.PopulateColumns();
		}

		/// <summary>
		/// 자재DEFID별 현 재고량 기준테이블
		/// </summary>
		private void InitGridComsumeList2()
		{
			grdConsumeList2.GridButtonItem = GridButtonItem.None;
			grdConsumeList2.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

			grdConsumeList2.View.SetIsReadOnly();
			grdConsumeList2.ShowStatusBar = false;
			grdConsumeList2.View.AddTextBoxColumn("CONSUMABLEDEFID", 100);
			grdConsumeList2.View.AddTextBoxColumn("CONSUMABLEDEFVERSION").SetIsHidden();
			grdConsumeList2.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 150);
			grdConsumeList2.View.AddTextBoxColumn("UOM", 50).SetTextAlignment(TextAlignment.Center);
			grdConsumeList2.View.AddSpinEditColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.000000000", MaskTypes.Numeric, true);

			grdConsumeList2.View.PopulateColumns();
		}

		/// <summary>
		/// 투입자재 - 합계 Row 초기화
		/// </summary>
		private void InitSummaryRow1()
		{
			grdLotList1.View.Columns["PRODUCTDEFNAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
			grdLotList1.View.Columns["PRODUCTDEFNAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("LOTCNT"));

			grdLotList1.View.Columns["LOTID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
			grdLotList1.View.Columns["LOTID"].SummaryItem.DisplayFormat = "{0:#,##0.##}";

			grdLotList1.View.Columns["STOCKQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdLotList1.View.Columns["STOCKQTY"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));

			grdLotList1.View.Columns["CHARGEQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdLotList1.View.Columns["CHARGEQTY"].SummaryItem.DisplayFormat = "{0:#,##0.000000000}";

			grdLotList1.View.OptionsView.ShowFooter = true;
			grdLotList1.ShowStatusBar = false;
		}

		/// <summary>
		/// 투입자재 - 합계 Row 초기화
		/// </summary>
		private void InitSummaryRow2()
		{
			grdLotList2.View.Columns["PRODUCTDEFNAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
			grdLotList2.View.Columns["PRODUCTDEFNAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("LOTCNT"));

			grdLotList2.View.Columns["LOTID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
			grdLotList1.View.Columns["LOTID"].SummaryItem.DisplayFormat = "{0:#,##0.##}";

			grdLotList2.View.Columns["STOCKQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdLotList2.View.Columns["STOCKQTY"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));

			grdLotList2.View.Columns["CHARGEQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdLotList2.View.Columns["CHARGEQTY"].SummaryItem.DisplayFormat = "{0:#,##0.000000000}";

			grdLotList2.View.OptionsView.ShowFooter = true;
			grdLotList2.ShowStatusBar = false;
		}

		#endregion

		#region Event

		/// <summary>
		/// 화면에서 사용되는 이벤트를 초기화한다.
		/// </summary>
		public void InitializeEvent()
		{
			// TODO : 화면에서 사용할 이벤트 추가
			btnPrint.Click += BtnPrint_Click;
			grdRequestList.View.FocusedRowChanged += View_FocusedRowChanged;
			grdRequestList.View.CheckStateChanged += View_CheckStateChanged1;

			//single
			chkIsStockBase1.CheckedChanged += ChkIsStockBaseInProcess_CheckedChanged;
			grdConsumeList1.View.FocusedRowChanged += View_FocusedRowChanged1;

			//multi
			chkIsStockBase2.CheckedChanged += ChkIsStockBase2_CheckedChanged;
			grdConsumeList2.View.CheckStateChanged += View_CheckStateChanged;
			
		}

		#region 메인 그리드 Row 선택 시 이벤트

		/// <summary>
		/// 메인 그리드 체크 시 이벤트 -> 자재 목록 조회
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_CheckStateChanged1(object sender, EventArgs e)
		{
			ClearGridDatasOfBottomGrid();

			DataTable dt = grdRequestList.DataSource as DataTable;
			DataTable dtCopy = dt.Clone();

			for(int i = 0; i < dt.Rows.Count; i++)
			{
				bool isChecked = grdRequestList.View.IsRowChecked(i);
				if(isChecked)
				{
					DataRow r = grdRequestList.View.GetDataRow(i);
					DataRow newRow = dtCopy.NewRow();
					newRow.ItemArray = r.ItemArray;

					dtCopy.Rows.Add(newRow);
				}
			}

			if(dtCopy.Rows.Count > 0)
			{
				FocusedDataBindConsumList(true, dtCopy);
			}
			else
			{
				FocusedDataBindConsumList();
			}
			
		}

		/// <summary>
		/// 메인 그리드 Row 선택 시 이벤트 -> 자재 목록 조회
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			if (grdRequestList.View.FocusedRowHandle < 0) return;

			ClearGridDatasOfBottomGrid();

			FocusedDataBindConsumList();
		}

		/// <summary>
		/// 자재 목록 조회
		/// </summary>
		private void FocusedDataBindConsumList(bool isChecked = false, DataTable dt = null)
		{
            var values = Conditions.GetValues();

            Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("P_ISREQUEST", values["P_ISREQUEST"]);

            if (!isChecked)
			{
				tabLotList.SelectedTabPage = singlePage;

				grdConsumeList1.View.ClearDatas();
				grdLotList1.View.ClearDatas();

				DataRow selectRow = grdRequestList.View.GetFocusedDataRow();
				if (selectRow == null) return;

				param.Add("TYPE", "SINGLE");
				param.Add("PRODUCTDEFID", selectRow["PRODUCTDEFID"]);
				param.Add("PRODUCTDEFVERSION", selectRow["PRODUCTDEFVERSION"]);
				param.Add("PROCESSSEGMENTID", selectRow["PROCESSSEGMENTID"]);
				param.Add("AREAID", selectRow["AREAID"]);

				grdConsumeList1.DataSource = SqlExecuter.Query("SelectConsumListOfBom", "10001", param);
			}
			else
			{
				tabLotList.SelectedTabPage = multiPage;

				grdConsumeList2.View.ClearDatas();
				grdLotList2.View.ClearDatas();

				string product = string.Empty;
				if(dt.Rows.Count <= 0) return;

				foreach(DataRow row in dt.Rows)
				{
					product += row["PRODUCTDEFID"] + "|" + row["PRODUCTDEFVERSION"] + "|" + row["PROCESSSEGMENTID"] + ",";
				}

				param.Add("PRODUCTDEFID", product.TrimEnd(','));
				param.Add("AREAID", dt.Rows[0]["AREAID"]);

				grdConsumeList2.DataSource = SqlExecuter.Query("SelectConsumListOfBom", "10001", param);

				grdConsumeList2.View.CheckedAll(true);
			}
			
		}

		#endregion

		#region 자재 선택 시 불출요청 할 LOT 리스트 조회

		/// <summary>
		/// (싱글)자재 선택 시 LOT LIST 조회
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_FocusedRowChanged1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			FocusedDataBind();
		}

		/// <summary>
		/// (멀티)자재 선택 시 LOT LIST 조회
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_CheckStateChanged(object sender, EventArgs e)
		{
			FocusedDataBindMulti();
		}

		/// <summary>
		/// (싱글)자재 선택 시 LOT LIST 조회
		/// </summary>
		private void FocusedDataBind()
		{
			grdLotList1.View.ClearDatas();

			DataRow selectRow = grdRequestList.View.GetFocusedDataRow();
			if (selectRow == null) return;

            var values = Conditions.GetValues();
            
            Dictionary<string, object> param = new Dictionary<string, object>();
            
			//string product = selectRow["PRODUCTDEFID"].ToString() + "|" + selectRow["PRODUCTDEFVERSION"].ToString() + "|" + selectRow["PROCESSSEGMENTID"].ToString();
			//param.Add("PRODUCTDEFID", product.TrimEnd(','));

			DataRow selectRowConsum = grdConsumeList1.View.GetFocusedDataRow();
			if(selectRowConsum == null) return;

			//string consumable = selectRowConsum["CONSUMABLEDEFID"] + "|" + selectRowConsum["CONSUMABLEDEFVERSION"].ToString();
			//param.Add("CONSUMABLEDEFID", consumable);
			
			param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
			param.Add("PLANTID", UserInfo.Current.Plant);
			param.Add("AREAID", selectRow["AREAID"]);
			param.Add("YEARMONTH", string.Format("{0:yyyy-MM}", DateTime.Now));
            param.Add("PRODUCTDEFID", Format.GetString(selectRow["PRODUCTDEFID"]));
            param.Add("PRODUCTDEFVERSION", Format.GetString(selectRow["PRODUCTDEFVERSION"]));
            param.Add("PROCESSSEGMENTID", Format.GetString(selectRow["PROCESSSEGMENTID"]));
            param.Add("CONSUMABLEDEFID", Format.GetString(selectRowConsum["CONSUMABLEDEFID"]));
            param.Add("CONSUMABLEDEFVERSION", Format.GetString(selectRowConsum["CONSUMABLEDEFVERSION"]));
            param.Add("P_ISREQUEST", values["P_ISREQUEST"]);


            //불출 요청할 lot 리스트 조회
            grdLotList1.DataSource = SqlExecuter.Query("SelectLotListByRequest", "10001", param);
		}


		/// <summary>
		/// (멀티)자재 선택 시 LOT LIST 조회
		/// </summary>
		private void FocusedDataBindMulti()
		{
			grdLotList2.View.ClearDatas();

			DataTable dt = grdRequestList.View.GetCheckedRows();
			if (dt.Rows.Count <= 0)
			{
				grdConsumeList2.View.ClearDatas();
				return;
			}

			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
			param.Add("PLANTID", UserInfo.Current.Plant);
			param.Add("AREAID", dt.Rows[0]["AREAID"]);
			param.Add("YEARMONTH", string.Format("{0:yyyy-MM}", DateTime.Now));

			string product = string.Empty;
			dt.AsEnumerable().ForEach(r => {
				product += r["PRODUCTDEFID"].ToString() + "|" + r["PRODUCTDEFVERSION"].ToString() + "|" + r["PROCESSSEGMENTID"].ToString() + ",";
			});
			param.Add("PRODUCTDEFID", product.TrimEnd(','));

			DataTable dtChecked = grdConsumeList2.View.GetCheckedRows();
			if(dtChecked.Rows.Count <= 0) return;


			string consumable = string.Empty;
			dtChecked.AsEnumerable().ForEach(r => {
				consumable += r["CONSUMABLEDEFID"].ToString() + "|" + r["CONSUMABLEDEFVERSION"].ToString() + ",";
			});
			param.Add("CONSUMABLEDEFID", consumable.TrimEnd(','));


			//불출 요청할 lot 리스트 조회
			grdLotList2.DataSource = SqlExecuter.Query("SelectLotListByRequest", "10001", param);
		}
		#endregion


		#region 재고량기준 체크

		/// <summary>
		/// (싱글)재고량기준 청구 체크 시 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ChkIsStockBaseInProcess_CheckedChanged(object sender, EventArgs e)
		{
			if (chkIsStockBase1.Checked)
			{
				DataTable dtCurrentList = grdLotList1.DataSource as DataTable;

				double consumedQty = Format.GetDouble(dtCurrentList.Rows[0]["CONSUMEDQTY"], 0);
				double stdQty = Format.GetDouble(dtCurrentList.Rows[0]["STOCKQTY"], 0);
				//2019-11-27 CT_CONSUMEMATERIALLOT_TEMP에 소모된 자재 제외 후 청구
				stdQty -= consumedQty;
				if (stdQty < 0) stdQty = 0;

				for (int i = 0; i < dtCurrentList.Rows.Count; i++)
				{
					DataRow r = dtCurrentList.Rows[i];
					double chargeQty = Format.GetDouble(r["CHARGEQTY"], 0);
					double resultQty = stdQty - chargeQty;

					if (resultQty < 0)
					{
						r["CHARGEQTY"] = resultQty * (-1);
						break;
					}
					else
					{
						r["CHARGEQTY"] = 0;
					}

					stdQty = resultQty;
				}

				dtCurrentList.AcceptChanges();
			}
			else
			{
				FocusedDataBind();
			}
		}

		/// <summary>
		/// (멀티)재고량기준 청구 체크 시 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ChkIsStockBase2_CheckedChanged(object sender, EventArgs e)
		{
			if (chkIsStockBase2.Checked)
			{
				DataTable dtConsume = grdConsumeList2.View.GetCheckedRows();
				DataTable dtCurrentList = grdLotList2.DataSource as DataTable;

				for (int i = 0; i < dtConsume.Rows.Count; i++)
				{
					DataRow[] rows = dtCurrentList.Select("MATERIALDEFID = '" + dtConsume.Rows[i]["CONSUMABLEDEFID"].ToString() +
														"'AND MATERIALDEFVERSION = '" + dtConsume.Rows[i]["CONSUMABLEDEFVERSION"].ToString() + "'");

					double consumedQty = Format.GetDouble(rows[0]["CONSUMEDQTY"], 0);
					double stdQty = Format.GetDouble(rows[0]["STOCKQTY"], 0);
					//2019-11-27 CT_CONSUMEMATERIALLOT_TEMP에 소모된 자재 제외 후 청구
					stdQty -= consumedQty;
					if (stdQty < 0) stdQty = 0;

					for (int k = 0; k < rows.Length; k++)
					{
						DataRow r = rows[k];
						double chargeQty = Format.GetDouble(r["CHARGEQTY"], 0);
						double resultQty = stdQty - chargeQty;

						if (resultQty < 0)
						{
							rows[k]["CHARGEQTY"] = resultQty * (-1);
							break;
						}
						else
						{
							rows[k]["CHARGEQTY"] = 0;
						}

						stdQty = resultQty;
					}//for
				}//for
				dtCurrentList.AcceptChanges();
			}
			else
			{
				FocusedDataBindMulti();
			}
		}

		#endregion





		/// <summary>
		/// 전표 출력 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnPrint_Click(object sender, EventArgs e)
		{
			int index = tabLotList.SelectedTabPageIndex;

			string requestNo = string.Empty;
			switch (index)
			{
				case 0://싱글

					DataRow selectRow = grdRequestList.View.GetFocusedDataRow();
					if(selectRow == null) return;

					requestNo = Format.GetString(selectRow["REQUESTNO"]);
					if (selectRow["ISREQUEST"].ToString().Equals("Y") && !string.IsNullOrEmpty(requestNo))
					{
						printChit(requestNo);
					}

					break;
				case 1://멀티
                    List<string> printed = new List<string>();
					DataTable dtChecked = grdRequestList.View.GetCheckedRows();
					for (int i = 0; i < dtChecked.Rows.Count; i++)
					{
						DataRow row = dtChecked.Rows[i];
						requestNo = Format.GetString(row["REQUESTNO"]);
						if (row["ISREQUEST"].ToString().Equals("Y") && !string.IsNullOrEmpty(requestNo))
						{
                            if (!printed.Contains(requestNo))
                            {
                                printed.Add(requestNo);
                                printChit(requestNo);
                            }
                        }
					}
					break;
			}//switch
		}
		#endregion

		#region 툴바
		/// <summary>
		/// 저장 버튼을 클릭하면 호출한다.
		/// </summary>
		protected override void OnToolbarSaveClick()
		{
			base.OnToolbarSaveClick();

			int index = tabLotList.SelectedTabPageIndex;
			SmartBandedGrid grid = index == 0 ? grdLotList1 : grdLotList2;

			if ((grid.DataSource as DataTable).Rows.Count == 0)
			{
				//저장 할 데이터가 없습니다.
				throw MessageException.Create("NoSaveData");
			}

			MessageWorker worker = new MessageWorker("RequestConsumableRelease");
			worker.SetBody(new MessageBody()
			{
				{ "enterpriseId", UserInfo.Current.Enterprise },
				{ "plantId", UserInfo.Current.Plant },
				{ "materialList", grid.DataSource },
			});

			var result =  worker.Execute<DataTable>();
			DataTable dtRequestNo = result.GetResultSet();

			//전표출력
			string requestNo = Format.GetString(dtRequestNo.AsEnumerable().FirstOrDefault()["REQUESTNO"]);
			printChit(requestNo);
		}
		#endregion

		#region 검색
		/// <summary>
		/// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
		/// </summary>
		protected async override Task OnSearchAsync()
		{
			await base.OnSearchAsync();

			// TODO : 조회 SP 변경
			var values = Conditions.GetValues();
			values.Add("PlantId", UserInfo.Current.Plant);
			values.Add("LanguageType", UserInfo.Current.LanguageType);

			//검색 눌러서 조회 시 무조건 싱글
			ClearGridDatasOfBottomGrid();

			DataTable dtListByArea = await QueryAsync("SelectLotListWipByProcessSeg", "10001", values);

			if (dtListByArea.Rows.Count < 1)
			{
				// 조회할 데이터가 없습니다.
				ShowMessage("NoSelectData");
			}

			grdRequestList.DataSource = dtListByArea;
			FocusedDataBindConsumList();
		}

		/// <summary>
		/// 조회조건 항목을 추가한다.
		/// </summary>
		protected override void InitializeCondition()
		{
			base.InitializeCondition();

			// TODO : 조회조건 추가 구성이 필요한 경우 사용

			//품목
			InitializeConditionProductdefId_Popup();
			//공정
			CommonFunction.AddConditionProcessSegmentPopup("P_PROCESSSEGMENTID", 2.1, true, Conditions);
			//작업장
			CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 2.2, false, Conditions, true, true);
		}

		/// <summary>
		/// 조회조건의 컨트롤을 추가한다.
		/// </summary>
		protected override void InitializeConditionControls()
		{
			base.InitializeConditionControls();

			// TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
		}

		/// <summary>
		/// 팝업형 조회조건 생성 - 품목
		/// </summary>
		private void InitializeConditionProductdefId_Popup()
		{
			var conditionProductId = this.Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefinitionList", "10001", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEFNAME", "PRODUCTDEFID")
				.SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
				.SetPopupLayoutForm(800, 800)
				.SetPopupAutoFillColumns("UNIT")
				.SetLabel("PRODUCTDEF")
				.SetPosition(1.1)
				.SetPopupResultCount(0)
				.SetPopupAutoFillColumns("PRODUCTDEFNAME");


			// 팝업에서 사용되는 검색조건 (품목코드/명)
			conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");

			// 팝업 그리드에서 보여줄 컬럼 정의
			// 품목코드
			conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 200);
			// 품목명
			conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
			// 품목버전
			conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 120);
		}

		#endregion

		#region 유효성 검사

		/// <summary>
		/// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
		/// </summary>
		protected override void OnValidateContent()
		{
			base.OnValidateContent();
		}

		#endregion

		#region Private Function

		/// <summary>
		/// 전표출력
		/// </summary>
		private void printChit(string requestNo)
		{
			if (string.IsNullOrEmpty(requestNo))
			{
				ShowMessage("NotExistRequestNo");
				return;
			}

			Assembly assembly = Assembly.GetAssembly(this.GetType());
			Stream stream = assembly.GetManifestResourceStream("Micube.SmartMES.ProcessManagement.Report.RequestConsumableRelease.repx");
			CommonFunction.PrintRequestConsumableRelease(requestNo, stream);
		}

		/// <summary>
		/// 현 재고량 계산
		/// </summary>
		/// <param name="targetGrid"></param>
		/// <param name="stdGrid"></param>
		private void CalculateStockQty(DataRow currentRow, SmartBandedGrid targetGrid, SmartBandedGrid stdGrid)
		{
			DataTable dtTarget = (targetGrid.DataSource as DataTable).Clone();
			DataTable dtStd = stdGrid.DataSource as DataTable;

			DataRow newRow = dtTarget.NewRow();
			newRow.ItemArray = currentRow.ItemArray;
			dtTarget.Rows.Add(newRow);

			var result = from consumeRows in dtTarget.AsEnumerable()
						 join stdRows in dtStd.AsEnumerable()
						 on new { A = consumeRows["MATERIALDEFID"], B = consumeRows["MATERIALDEFVERSION"] }
						 equals new { A = stdRows["CONSUMABLEDEFID"], B = stdRows["CONSUMABLEDEFVERSION"] }
						 select new
						 {
							 MATERIALDEFID = consumeRows["MATERIALDEFID"],
							 STOCKQTY = Format.GetDecimal(stdRows["STOCKQTY"]) - Format.GetDecimal(consumeRows["CHARGEQTY"]),
							 LOTID = consumeRows["LOTID"]
						 };

			var dl = result.ToList();
			for (int i = 0; i < dl.Count; i++)
			{
				targetGrid.View.SetRowCellValue(targetGrid.View.LocateByValue("LOTID", dl[i].LOTID.ToString()), "STOCKQTY", Format.GetDecimal(dl[i].STOCKQTY));
			}

			(targetGrid.DataSource as DataTable).AcceptChanges();
		}

		/// <summary>
		/// 투입 기준 자재 데이터 바인드
		/// </summary>
		/// <param name="dt"></param>
		private void StockStdBind(DataTable dt, SmartBandedGrid grid, bool isDataBind = false)
		{
			grid.View.ClearDatas();
			DataTable dtConsume = grid.DataSource as DataTable;
			if (isDataBind || dtConsume.Rows.Count == 0)
			{
				List<string> consumeDefList = dt.AsEnumerable().Select(r => Format.GetString(r["MATERIALDEFID"])).Distinct().ToList();
				for (int i = 0; i < consumeDefList.Count; i++)
				{
					decimal stockQty = dt.AsEnumerable().Where(r => Format.GetString(r["MATERIALDEFID"]).Equals(consumeDefList[i])).Select(r => Format.GetDecimal(r["STOCKQTY"])).ToList()[0];
					string materialVer = dt.AsEnumerable().Where(r => Format.GetString(r["MATERIALDEFID"]).Equals(consumeDefList[i])).Select(r => Format.GetString(r["MATERIALDEFVERSION"])).ToList()[0];
					DataRow newRow = dtConsume.NewRow();
					newRow["CONSUMABLEDEFID"] = consumeDefList[i];
					newRow["CONSUMABLEDEFVERSION"] = materialVer;
					newRow["STOCKQTY"] = stockQty;

					dtConsume.Rows.Add(newRow);
				}
				dtConsume.AcceptChanges();
			}
		}

		/// <summary>
		/// 하단 그리드 데이터 클리어
		/// </summary>
		private void ClearGridDatasOfBottomGrid()
		{
			grdConsumeList1.View.ClearDatas();
			grdConsumeList2.View.ClearDatas();
			grdLotList1.View.ClearDatas();
			grdLotList2.View.ClearDatas();
			chkIsStockBase1.Checked = false;
			chkIsStockBase2.Checked = false;
		}
		#endregion
	}
}