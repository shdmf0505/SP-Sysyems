#region using

using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
	/// <summary>
	/// 프 로 그 램 명  : 공정관리 > 투입관리 > LOT 투입 등록
	/// 업  무  설  명  : PO 기준으로 생성된 LOT을 현장에 투입한다.
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-06-24
	/// 수  정  이  력  : 
	///					1. 예상 생산 완료일 Valid 적용 필요!
	///					2. lot print 공통으로 뺀 후 적용
	/// 
	/// </summary>
	public partial class InputLot : SmartConditionManualBaseForm
	{
		#region Local Variables

		// TODO : 화면에서 사용할 내부 변수 추가
		#endregion

		#region 생성자

		public InputLot()
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
		}

		/// <summary>        
		/// 코드그룹 리스트 그리드를 초기화한다.
		/// </summary>
		private void InitializeGrid()
		{
			// TODO : 그리드 초기화 로직 추가
			//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			//품목 그리드 초기화
			//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			grdProductList.GridButtonItem = GridButtonItem.None;
			grdProductList.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
			grdProductList.View.SetIsReadOnly();

            if(UserInfo.Current.Enterprise.Equals(Constants.EnterPrise_YoungPoong))
                grdProductList.View.AddTextBoxColumn("CALDATE", 120);
            else
                grdProductList.View.AddTextBoxColumn("CALDATE", 120).SetIsHidden();
            //수주오더
            grdProductList.View.AddTextBoxColumn("PRODUCTIONORDERID", 100);
			//수주라인
			grdProductList.View.AddTextBoxColumn("LINENO", 50);
			// 품목코드
			grdProductList.View.AddTextBoxColumn("PRODUCTDEFID", 200);
			//품목 버전
			grdProductList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
			// 품목명
			grdProductList.View.AddTextBoxColumn("PRODUCTDEFNAME", 250);
			//차수
			grdProductList.View.AddTextBoxColumn("INPUTSEQUENCE", 80);
			//UOM
			grdProductList.View.AddTextBoxColumn("UNIT", 80).SetLabel("UOM").SetTextAlignment(TextAlignment.Center);
			//기준소요량
			grdProductList.View.AddSpinEditColumn("JOINTQTY", 100).SetLabel("STANDARDCOSTQTY").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false);

			grdProductList.View.PopulateColumns();

			//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			//LOT 그리드 초기화
			//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			grdLotList.GridButtonItem = GridButtonItem.None;
			grdLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdLotList.View.EnableRowStateStyle = false;

			//품목코드
			grdLotList.View.AddTextBoxColumn("PRODUCTDEFID",120).SetIsHidden();
			//품목버전
			grdLotList.View.AddTextBoxColumn("PRODUCTDEFVERSION").SetIsHidden();
			//LOT ID
			grdLotList.View.AddTextBoxColumn("LOTID", 250).SetIsReadOnly();
			//단위
			grdLotList.View.AddTextBoxColumn("UNIT", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            //최초생성 PNL수량
            grdLotList.View.AddSpinEditColumn("CREATEDPANELQTY", 120).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetIsReadOnly().SetLabel("CREATEDPNLQTY");
            //PNL수량
            grdLotList.View.AddSpinEditColumn("PANELQTY", 100).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetIsReadOnly().SetLabel("PNLQTY");
            //투입수량(PNL)
            grdLotList.View.AddSpinEditColumn("INPUTPNLQTY", 100).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetLabel("INPUTPNLQTY");
            //투입수량
            grdLotList.View.AddSpinEditColumn("QTY", 100).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right).SetIsReadOnly().SetLabel("INPUTQTY");
            // TODO : Resource
            //투입작업장
            grdLotList.View.AddComboBoxColumn("RESOURCEID", 100, new SqlQueryAdapter(), "RESOURCENAME", "RESOURCEID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false);
            
            //작업장
            grdLotList.View.AddTextBoxColumn("INPUTAREA", 100)
                .SetIsReadOnly()
                .SetIsHidden();
            grdLotList.View.AddTextBoxColumn("PROCESSSEGMENTNAME",150).SetIsReadOnly();
            //Panel 당 PCS 수
            grdLotList.View.AddTextBoxColumn("PANELPERQTY").SetIsHidden();
            //납기일
            grdLotList.View.AddTextBoxColumn("DUEDATE").SetIsHidden();
			//리드타임
			grdLotList.View.AddTextBoxColumn("LEADTIME").SetIsHidden();

            grdLotList.View.PopulateColumns();
            InitializationSummaryRow();
        }
        /// <summary>
        /// Footer 추가 Panel, PCS 합계 표시 2019.08.01
        /// 이동우 차장 요청
        /// </summary>
        private void InitializationSummaryRow()
        {
            grdLotList.View.Columns["LOTID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdLotList.View.Columns["LOTID"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
            grdLotList.View.Columns["PANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdLotList.View.Columns["PANELQTY"].SummaryItem.DisplayFormat = " ";
            grdLotList.View.Columns["QTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdLotList.View.Columns["QTY"].SummaryItem.DisplayFormat = " ";

            grdLotList.View.OptionsView.ShowFooter = true;
            grdLotList.ShowStatusBar = false;
        }
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
		{
			// TODO : 화면에서 사용할 이벤트 추가
			spFrom.KeyDown += Range_KeyDown;
			spTo.KeyDown += Range_KeyDown;
			btnApply.Click += BtnApply_Click;
			btnInput.Click += BtnInput_Click;
            
			grdProductList.View.FocusedRowChanged += View_FocusedRowChanged1;
			grdLotList.View.FocusedRowChanged += View_FocusedRowChanged;
            grdLotList.View.CustomDrawFooterCell += View_CustomDrawFooterCell;
            // TODO : Resource
            grdLotList.View.CellValueChanged += LotListView_CellValueChanged;
            grdLotList.View.RowCellStyle += View_RowCellStyle;
            grdLotList.View.ValidatingEditor += View_ValidatingEditor;
            grdLotList.View.InvalidValueException += View_InvalidValueException;
        }

		/// <summary>
		/// 범위 적용 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Range_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				CheckLotIdToRange();
			}
		}

		/// <summary>
		/// 적용 버튼 클릭 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnApply_Click(object sender, EventArgs e)
		{
			CheckLotIdToRange();
		}

		/// <summary>
		/// 지정된 범위로 Lot List 체크 
		/// </summary>
		private void CheckLotIdToRange()
		{
			DataTable dt = grdLotList.DataSource as DataTable;
			if (dt.Rows.Count < 1) return;

			grdLotList.View.CheckedAll(false);

			int from = Format.GetInteger(spFrom.EditValue);
			int to = Format.GetInteger(spTo.EditValue);
			if (to < from) return;

			int lastSeq = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["LOTID"].ToString().Substring(18, 3));
			if (lastSeq < to) return;

			int firstSeq = Convert.ToInt32(dt.Rows[0]["LOTID"].ToString().Substring(18, 3));
			if (firstSeq <= from)
			{
				List<int> lotList = dt.AsEnumerable().Select(r => Convert.ToInt32(r["LOTID"].ToString().Substring(18, 3))).ToList();
				int focusRow = lotList.IndexOf(from);

				int checkRowCnt = to - from + 1;

				for (int i = focusRow; i < (focusRow + checkRowCnt); i++)
				{
					grdLotList.View.CheckRow(i, true);
				}
			}
		}

		private void View_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            ColumnView view = sender as ColumnView;
            GridColumn column = (e as EditFormValidateEditorEventArgs)?.Column ?? view.FocusedColumn;
            if (column.FieldName != "INPUTPNLQTY")
            {
                return;
            }
            DataRow poRow = grdProductList.View.GetFocusedDataRow();
            int lotInputPnlQty = int.Parse(poRow["LOTINPUTPNLQTY"].ToString());
            int inputPnlQty = int.Parse(e.Value.ToString());
            if (inputPnlQty < lotInputPnlQty)
            {
                e.Valid = false;
                // LOT투입 PNL수량({0})보다 작은값을 입력할 수 없습니다.
                ShowMessage("LessThanLotInputPnlQty", lotInputPnlQty.ToString());
            }
        }

        private void View_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            ColumnView view = sender as ColumnView;
            if (view == null)
            {
                return;
            }
            e.ExceptionMode = ExceptionMode.Ignore;
        }

        private void LotListView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "RESOURCEID")
            {
                RepositoryItemLookUpEdit edit = e.Column.ColumnEdit as RepositoryItemLookUpEdit;

                string areaId = Format.GetString(edit.GetDataSourceValue("AREAID", edit.GetDataSourceRowIndex("RESOURCEID", e.Value)));

                grdLotList.View.SetFocusedRowCellValue("INPUTAREA", areaId);
            }
            else if(e.Column.FieldName == "INPUTPNLQTY")
            {
                int value = int.Parse(e.Value.ToString());
                if (value > int.Parse(grdLotList.View.GetRowCellValue(e.RowHandle, "PANELQTY").ToString()))
                {
                    // LOT 수량보다 큰 수량을 투입할 수 없습니다.
                    ShowMessage("CantInputLagerThanLotQty");
                    grdLotList.View.CellValueChanged -= LotListView_CellValueChanged;
                    value = int.Parse(grdLotList.View.GetRowCellValue(e.RowHandle, "PANELQTY").ToString());
                    grdLotList.View.SetRowCellValue(e.RowHandle, "INPUTPNLQTY", value);
                    grdLotList.View.CellValueChanged += LotListView_CellValueChanged;
                }
                int panelPerQty = int.Parse(grdLotList.View.GetRowCellValue(e.RowHandle, "PANELPERQTY").ToString());
                grdLotList.View.SetRowCellValue(e.RowHandle, "QTY", value * panelPerQty);
            }
        }

        private void View_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            DataTable dt = grdLotList.View.GetCheckedRows();

            if (dt.Rows.Count > 0)
            {
                int PanelSum = 0;
                int qtySum = 0;
                dt.Rows.Cast<DataRow>().ForEach(row =>
                {
                    PanelSum += Format.GetInteger(row["PANELQTY"]);
                    qtySum += Format.GetInteger(row["QTY"]);
                });
                
                if (e.Column.FieldName == "PANELQTY")
                {
                    e.Info.DisplayText = Format.GetString(PanelSum);
                }
                if (e.Column.FieldName == "QTY")
                {
                    e.Info.DisplayText = Format.GetString(qtySum);
                }
            }
            else
            {
                grdLotList.View.Columns["PANELQTY"].SummaryItem.DisplayFormat ="  ";
                grdLotList.View.Columns["QTY"].SummaryItem.DisplayFormat = "  ";
            }
        }

        private void View_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if(e.Column.FieldName == "INPUTPNLQTY")
            {
                e.Appearance.ForeColor = Color.Black;
                e.Appearance.BackColor = Color.Yellow;
            }
        }

        /// <summary>
        /// PO 선택 시 이벤트 - LOT 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			FocusedDataBind();
		}

		/// <summary>
		/// Lot List Row 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			DataRow row = grdLotList.View.GetFocusedDataRow();

			if(row != null)
			{
				//포커스 Row의 납기일, 예상 생산 완료일 설정
				SetOptionDateTime(row);
			}	
		}

		/// <summary>
		/// LOT 투입 시작 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnInput_Click(object sender, EventArgs e)
		{
			base.OnValidateContent();

            // 재공실사 진행 여부 체크
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);

            DataTable isWipSurveyResult = SqlExecuter.Query("GetPlantIsWipSurvey", "10001", param);

            if (isWipSurveyResult.Rows.Count > 0)
            {
                DataRow row = isWipSurveyResult.AsEnumerable().FirstOrDefault();

                string isWipSurvey = Format.GetString(row["ISWIPSURVEY"]);

                if (isWipSurvey == "Y")
                {
                    // 재공실사가 진행 중 입니다. {0}을 진행할 수 없습니다.
                    ShowMessage("PLANTINWIPSURVEY", Language.Get(string.Join("_", "MENU", MenuId)));

                    return;
                }
            }

            DataTable dtChecked = grdLotList.View.GetCheckedRows();

			if (dtChecked.Rows.Count == 0)
			{
				//투입할 LOT이 존재하지 않습니다. 
				throw MessageException.Create("NotExistInputLot");
			}


            DataTable itemList = grdProductList.DataSource as DataTable;

            foreach (DataRow dr in itemList.Rows)
            {

                param.Add("P_PRODUCTDEFID", dr["PRODUCTDEFID"]);
                param.Add("P_PRODUCTDEFVERSION", dr["PRODUCTDEFVERSION"]);
                DataTable lineNoInfo = SqlExecuter.Query("GetRoutingOperationList", "10001", param);

                if (!Format.GetString(lineNoInfo.Rows[0]["USERSEQUENCE"]).Equals("10") && Format.GetString(lineNoInfo.Rows[0]["ASSEMBLYITEMCLASS"]).Equals("Product"))
                {
                    throw MessageException.Create("NoLotTen");

                }
                param.Remove("P_PRODUCTDEFID");
                param.Remove("P_PRODUCTDEFVERSION");


            }


            //투입 작업장 Validation
            foreach (DataRow row in dtChecked.Rows)
			{
				string areaId = Format.GetString(row["INPUTAREA"], "");
				if (string.IsNullOrEmpty(areaId) || areaId.Equals("*"))
				{
					// 투입 작업장을 선택하여 주시오.
					throw MessageException.Create("NoInputArea");
				}
			}

			DateTime nowDateTime = DateTime.Now;
            //DateTime completeDate = Convert.ToDateTime(txtCompleteDate.Text);

            ////0보다 크면 nowDateTime 큰 것
            ////0보다 작으면 completeDate 큰 것
            ////예상 생산 완료일 > 현재 날짜
            //int result = DateTime.Compare(nowDateTime, completeDate);
            //if(result > 0)
            //{
            //	//예상 생산 완료일은 현재 날짜보다 클 수 없습니다.
            //	throw MessageException.Create("NotExistInputLot");
            //}

            DataTable dtFocusedProductList = (grdProductList.View.GetFocusedDataRow().Table) as DataTable;
			DataTable dtClone = dtFocusedProductList.Clone();

			dtClone.ImportRow(grdProductList.View.GetFocusedDataRow());
			dtClone.AcceptChanges();

			DataSet sendDataSet = new DataSet();
			dtChecked.TableName = "lotList";
			dtClone.TableName = "productList";

			sendDataSet.Tables.Add(dtChecked);
			sendDataSet.Tables.Add(dtClone);

            // todo : 결과 반환받기
            MessageWorker worker = new MessageWorker("InputLot");
            worker.SetBody(new MessageBody()
            {
                { "PlantId", UserInfo.Current.Plant },
                { "ExpectedCompleteDate", dtpCompleteDate.DateTime },
                { "LotList", dtChecked },
                { "ProductList", dtClone }
            });

            //DataTable result = ExecuteRule<DataTable>("InputLot", sendDataSet);
            var executeResult = worker.Execute<DataTable>();
            DataTable result = executeResult.GetResultSet();

            //LOT CARD 출력
            if (chkPrintLotCard.Checked)
			{
                string lotId = string.Join(",", result.AsEnumerable().Select(row => Format.GetString(row["LOTID"])));
                CommonFunction.PrintLotCard_Ver2(lotId, LotCardType.Normal);

                //foreach(DataRow row in result.Rows)
                //{
                //	string lotId = row["LOTID"].ToString();
                //	CommonFunction.PrintLotCard_Ver2(lotId, LotCardType.Normal);
                //}
            }

            ShowMessage("SuccedSave");
			View_FocusedRowChanged1(null, null);
		}

		/// <summary>
		/// Report 파일의 컨트롤 중 Tag(FieldName) 값이 있는 컨트롤에 DataBinding(Text)를 추가한다.
		/// </summary>
		/// <param name="controls"></param>
		/// <param name="dataSource"></param>
		private void SetReportControlDataBinding(XRControlCollection controls, DataTable dataSource)
		{
			if (controls.Count > 0)
			{
				foreach (XRControl control in controls)
				{
					if (!string.IsNullOrWhiteSpace(control.Tag.ToString()))
						control.DataBindings.Add("Text", dataSource, control.Tag.ToString());

					SetReportControlDataBinding(control.Controls, dataSource);
				}
			}
		}

		#endregion

		#region 툴바

		/// <summary>
		/// 저장 버튼을 클릭하면 호출한다.
		/// </summary>
		protected override void OnToolbarSaveClick()
		{
			base.OnToolbarSaveClick();
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
			spFrom.EditValue = 0;
			spTo.EditValue = 0;
			ClearGridLotList();
			ClearBottomControls();

			var values = Conditions.GetValues();
			values.Add("PlantId", UserInfo.Current.Plant);
			values["P_PRODUCTIONORDERID"] = Format.GetString(values["P_PRODUCTIONORDERID"]).TrimEnd(',');
			values["P_PRODUCTDEFID"] = Format.GetString(values["P_PRODUCTDEFID"]).TrimEnd(',');

			//수주별 품목 리스트 조회
			DataTable dtProductDefList = await QueryAsync("SelectNotInputProductDefId", "10001", values);
			
			if (dtProductDefList.Rows.Count < 1)
			{
				// 조회할 데이터가 없습니다.
				ShowMessage("NoSelectData"); 
			}

			grdProductList.DataSource = dtProductDefList;
			FocusedDataBind();//포커스 row 조회 및 바인드
		}

		/// <summary>
		/// 조회조건 항목을 추가한다.
		/// </summary>
		protected override void InitializeCondition()
		{
			base.InitializeCondition();

			// TODO : 조회조건 추가 구성이 필요한 경우 사용
			
			//PO
			InitializeConditionProductOrderId_Popup();

			// 품목
			InitializeConditionProductDefId_Popup();
            //CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 2, true, Conditions);

            Conditions.GetCondition<ConditionItemComboBox>("P_PRODUCTDEFTYPE").SetDefault("*");
		}

		/// <summary>
		/// 팝업형 조회조건 생성 - 품목
		/// </summary>
		private void InitializeConditionProductDefId_Popup()
		{
			// SelectPopup 항목 추가
			var conditionProductId = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductionOrderIdListOfLotInput", "10001", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEF", "PRODUCTDEF")
				.SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
				.SetPopupLayoutForm(1000, 800)
				.SetPopupResultCount(0)
				.SetPopupAutoFillColumns("PRODUCTDEFNAME")
				.SetLabel("PRODUCTDEF")
				.SetPosition(1.2)
				.SetPopupApplySelection((selectedRows, dataGridRows) =>
				{
					string orderId = "";

					selectedRows.Cast<DataRow>().ForEach(row =>
					{
						orderId += row["PRODUCTIONORDER"].ToString() + ",";
					});
					orderId.TrimEnd(',');
					Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTIONORDERID").SetValue(orderId);
				});


			// 팝업에서 사용되는 검색조건 (품목코드/명)
			conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetEmptyItem();
			conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");

			// 팝업 그리드에서 보여줄 컬럼 정의
			//수주번호
			conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTIONORDERID", 100);
			//수주라인
			conditionProductId.GridColumns.AddTextBoxColumn("LINENO", 50);
			//수주량
			conditionProductId.GridColumns.AddSpinEditColumn("PLANQTY", 90);
			// 품목코드
			conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
			// 품목명
			conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
			// 품목버전
			conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);

		}

		/// <summary>
		/// 팝업형 조회조건 생성 - PO
		/// </summary>
		private void InitializeConditionProductOrderId_Popup()
		{
			var conditionProductionOrderId = Conditions.AddSelectPopup("P_PRODUCTIONORDERID", new SqlQuery("GetProductionOrderIdListOfLotInput", "10001", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTIONORDER", "PRODUCTIONORDER")
			   //.SetMultiGrid(true)
			   .SetPopupLayout("SELECTPRODUCTIONORDER", PopupButtonStyles.Ok_Cancel, true, false)
			   .SetPopupResultCount(0)
			   .SetPopupLayoutForm(1000, 800)
			   .SetPopupAutoFillColumns("PRODUCTDEFNAME")
			   .SetLabel("PRODUCTIONORDERID")
			   .SetPosition(1.1)
			   .SetPopupApplySelection((selectedRows, dataGridRows) =>
			   {
				   string productDefId = "";
				   selectedRows.Cast<DataRow>().ForEach(row =>
				   {
					   productDefId += row["PRODUCTDEF"].ToString() + ",";
				   });
				   productDefId.TrimEnd(',');
				   Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(productDefId);
			   });


			// 팝업에서 사용되는 검색조건 (P/O번호)
			conditionProductionOrderId.Conditions.AddTextBox("TXTPRODUCTIONORDERID");

			// 팝업 그리드에서 보여줄 컬럼 정의
			// P/O번호
			conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTIONORDERID", 150)
				.SetValidationKeyColumn();
			//수주라인
			conditionProductionOrderId.GridColumns.AddTextBoxColumn("LINENO", 50);
			// 수주량
			conditionProductionOrderId.GridColumns.AddSpinEditColumn("PLANQTY", 90);
			// 품목코드
			conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
			// 품목명
			conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
			// 품목버전
			conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
		}

		/// <summary>
		/// 조회조건의 컨트롤을 추가한다.
		/// </summary>
		protected override void InitializeConditionControls()
		{
			base.InitializeConditionControls();

			// TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
		}

		#endregion

		#region 유효성 검사

		/// <summary>
		/// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
		/// </summary>
		protected override void OnValidateContent()
		{
			base.OnValidateContent();

			// TODO : 유효성 로직 변경
		}

		#endregion

		#region Private Function

		// TODO : 화면에서 사용할 내부 함수 추가

		/// <summary>
		/// 품목 선택 시 포커스 된 Row의 투입 LOT조회
		/// </summary>
		private void FocusedDataBind()
		{
			DataRow selectedRow = grdProductList.View.GetFocusedDataRow();
			if (selectedRow == null) return;

			string productionOrderId = selectedRow["PRODUCTIONORDERID"].ToString();
			string productDefId = selectedRow["PRODUCTDEFID"].ToString();
			string productDefVersion = selectedRow["PRODUCTDEFVERSION"].ToString();
			string lineNo = selectedRow["LINENO"].ToString();
            string rtrSht = selectedRow["RTRSHT"].ToString();

            Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("PlantId", UserInfo.Current.Plant);
			param.Add("LanguageType", UserInfo.Current.LanguageType);
			param.Add("ProductionOrderId", productionOrderId);
			param.Add("ProductDefId", productDefId);
			param.Add("ProductDefVersion", productDefVersion);
			param.Add("LineNo", lineNo);

            // TODO : Resource
            //투입작업장 Combo조회
            //grdLotList.View.RefreshComboBoxDataSource("INPUTAREA", new SqlQuery("GetLotInputAreaList", "10001", param));
            grdLotList.View.RefreshComboBoxDataSource("RESOURCEID", new SqlQuery("GetLotInputAreaList", "10031", param));

            //LOT LIST 조회
            //DataTable dtLotList = SqlExecuter.Query("SelectNotInputLotListByProductDefId", "10001", param);
            DataTable dtLotList = SqlExecuter.Query("SelectNotInputLotListByProductDefId", "10031", param);

            if (dtLotList.Rows.Count < 1)
			{
				ClearGridLotList();
				ClearBottomControls();
			}
			else
			{
				grdLotList.DataSource = dtLotList;

				int focusedRow = 0;
				DataRow row = grdLotList.View.GetDataRow(focusedRow);

				if (row == null) return;

                //납기일, 예상 생산 완료일 설정 
                DataTable dt = SqlExecuter.Query("SelectExpectedCompleteDate", "10001", param);

                //납기일
                if (!string.IsNullOrEmpty(row["DUEDATE"].ToString()))
                {
                    DateTime dueDate = Convert.ToDateTime(row["DUEDATE"].ToString());
                    txtDueDate.Text = string.Format("{0:yyyy-MM-dd}", dueDate);
                }

                if (dt.Rows.Count >0 &&  !String.IsNullOrWhiteSpace(Format.GetFullTrimString(dt.Rows[0]["EXPCECTDATE"])))
                {
                    DateTime expectDay = Convert.ToDateTime(Format.GetFullTrimString(dt.Rows[0]["EXPCECTDATE"]));
                    dtpCompleteDate.DateTime = new DateTime(expectDay.Year, expectDay.Month, expectDay.Day);
                }
                else
                {
                    dtpCompleteDate.EditValue = null;
                }
                
                
			}

            if(rtrSht == "RTR")
            {
                grdLotList.View.Columns["INPUTPNLQTY"].OwnerBand.Visible = true;
                grdLotList.View.Columns["INPUTPNLQTY"].Visible = true;
            }
            else
            {
                grdLotList.View.Columns["INPUTPNLQTY"].OwnerBand.Visible = false;
                grdLotList.View.Columns["INPUTPNLQTY"].Visible = false;
            }
		}

		/// <summary>
		/// Controls 클리어
		/// </summary>
		private void ClearBottomControls()
		{
			txtDueDate.Text = "";
            //txtCompleteDate.Text = "";
            dtpCompleteDate.EditValue = null;

            chkPrintLotCard.Checked = false;
		}

		/// <summary>
		/// Lot List 클리어
		/// </summary>
		private void ClearGridLotList()
		{
			grdLotList.View.ClearDatas();
		}

		/// <summary>
		/// 납기일, 예상 생산 완료일 설정
		/// </summary>
		/// <param name="row"></param>
		private void SetOptionDateTime(DataRow row)
		{
			//납기일
			if (!string.IsNullOrEmpty(row["DUEDATE"].ToString()))
			{
				DateTime dueDate = Convert.ToDateTime(row["DUEDATE"].ToString());
				txtDueDate.Text = string.Format("{0:yyyy-MM-dd}", dueDate);
			}

            //예상 생산 완료일
            DateTime today = DateTime.Now.AddDays(24);
            dtpCompleteDate.DateTime = new DateTime(today.Year, today.Month, today.Day);

            if (!string.IsNullOrEmpty(row["LEADTIME"].ToString()))
			{
				double leadTime = Format.GetDouble(row["LEADTIME"], 0);
				DateTime dateTime = DateTime.Now.AddSeconds(leadTime);
                //txtCompleteDate.Text = string.Format("{0:yyyy-MM-dd}", dateTime);
            }
		}
		#endregion
	}
}