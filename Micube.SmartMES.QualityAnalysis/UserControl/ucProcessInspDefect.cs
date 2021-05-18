#region using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.Framework.Net;
using Micube.Framework;
using Micube.Framework.SmartControls;
using System.Text.RegularExpressions;
using Micube.Framework.SmartControls.Validations;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.IO;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.Repository;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using Micube.Framework.SmartControls.Grid.Conditions;
using Micube.SmartMES.QualityAnalysis.Helper;
using Micube.SmartMES.Commons;
#endregion

namespace Micube.SmartMES.QualityAnalysis
{
	/// <summary>
	/// 프로그램명 :  품질관리 > 수입검사 > 공정수입검사
	/// 업 무 설명 :  불량 리스트 UserControl
	/// 생  성  자 :  강유라
	/// 생  성  일 :  2019-09-09
	/// 수정 이 력 : 
	/// 
	/// 
	/// </summary>
	public partial class ucProcessInspDefect : UserControl
	{

        #region Local Variables
        DataRow CurrentDataRow = null;
        DataRow LotDataRow = null;
        private DataTable _fileDt;
        //Selected Lot ID
        private string _selectedLotId = string.Empty;
        //popupType
        public string _type = string.Empty;
        public SmartPopupBaseForm parentForm;
        bool _inputAll = true;
        bool _isQueryInitialize = false;
        double _defectQtySum = 0.0;
        double _defectQtyPNLSum = 0.0;

        int _selectedIndex = 0;
        #endregion

        #region 생성자
        public ucProcessInspDefect()
		{
			InitializeComponent();

            if (!smartSplitTableLayoutPanel1.IsDesignMode())
			{
				//InitializeGrid();
                //InitializeEvent();
                //InitializationSummaryRow();
			}
		}
		#endregion

		#region 컨텐츠 영역 초기화
		/// <summary>
		/// 그리드 초기화
		/// </summary>
		public void InitializeGrid()
		{
            grdInspectionList.View.ClearColumns();

            if (_type.Equals("updateData"))
            {
                grdInspectionList.GridButtonItem = GridButtonItem.None;
            }
            else
            {
                grdInspectionList.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
            }
            //구분 등 검사항목
            var item = grdInspectionList.View.AddGroupColumn("");

            //LOT ID
            item.AddTextBoxColumn("LOTID",150)
				.SetIsHidden()
				.SetIsReadOnly();

            item.AddTextBoxColumn("RESOURCEID", 150)
                .SetIsHidden()
                .SetIsReadOnly();

            //불량코드
            item.AddTextBoxColumn("DEFECTCODE",150)
				.SetIsHidden();

            //불량코드명
            InitializeGrid_DefectCodeListPopup(item);

            //품질공정
            item.AddTextBoxColumn("QCSEGMENTID",100)
				.SetIsHidden();
            item.AddTextBoxColumn("QCSEGMENTNAME", 150)
				.SetIsReadOnly();
            /*
            //검사 수량
            var groupInspQty = grdInspectionList.View.AddGroupColumn("INSPECTIONQTY");
            //PCS
            groupInspQty.AddSpinEditColumn("INSPECTIONQTY", 150)
                .SetLabel("PCS")
                .SetDisplayFormat("#,##0", MaskTypes.Numeric, false)
                .SetTextAlignment(TextAlignment.Right);
            //PNL
            groupInspQty.AddSpinEditColumn("INSPECTIONQTYPNL", 150)
                .SetLabel("PNL")
                .SetIsReadOnly()
                .SetDisplayFormat("#,##0", MaskTypes.Numeric, false)
                .SetTextAlignment(TextAlignment.Right);
                */


            grdInspectionList.View.AddSpinEditColumn("INSPECTIONQTY", 150)
                .SetIsHidden();
            grdInspectionList.View.AddSpinEditColumn("INSPECTIONQTYPNL", 150)
                .SetIsHidden();
            grdInspectionList.View.AddSpinEditColumn("QTY", 150)
                .SetIsHidden();

            //불량수량
            var groupSpecOutQty = grdInspectionList.View.AddGroupColumn("SPECOUTQTY");
            //PCS
            groupSpecOutQty.AddSpinEditColumn("DEFECTQTY", 100)
                .SetLabel("PCS")
                .SetIsReadOnly()
                .SetDisplayFormat("#,##0", MaskTypes.Numeric, false)
                .SetTextAlignment(TextAlignment.Right);
            //PNL
            groupSpecOutQty.AddSpinEditColumn("DEFECTQTYPNL", 100)
                .SetLabel("PNL")                
                .SetDisplayFormat("#,##0", MaskTypes.Numeric, false)
                .SetTextAlignment(TextAlignment.Right);

            //결과
            var result = grdInspectionList.View.AddGroupColumn("");

            //불량률
            result.AddSpinEditColumn("DEFECTRATE", 100)
				.SetIsReadOnly()
				.SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true)
				.SetTextAlignment(TextAlignment.Right);


            // 원인품목
            result.AddComboBoxColumn("REASONCONSUMABLEDEFIDVERSION", 200, new SqlQuery("GetReasonConsumableList", "10002"), "CONSUMABLEDEFNAME", "SPLITCONSUMABLEDEFIDVERSION")
                            .SetLabel("REASONCONSUMABLEDEFID")
                            .SetMultiColumns(ComboBoxColumnShowType.Custom, true)
                            .SetRelationIds("LOTID")
                            .SetPopupWidth(600)
                            .SetVisibleColumns("CONSUMABLEDEFID", "CONSUMABLEDEFVERSION", "CONSUMABLEDEFNAME", "MATERIALTYPE")
                            .SetVisibleColumnsWidth(90, 70, 200, 80);

            result.AddTextBoxColumn("SPLITCONSUMABLEDEFIDVERSION", 100).SetIsReadOnly().SetIsHidden();
            result.AddTextBoxColumn("REASONCONSUMABLEDEFID", 100).SetIsReadOnly().SetIsHidden();
            result.AddTextBoxColumn("REASONCONSUMABLEDEFVERSION", 100).SetIsReadOnly().SetIsHidden();
            // 원인자재LOT
            result.AddComboBoxColumn("REASONCONSUMABLELOTID", 180, new SqlQuery("GetDefectReasonConsumableLot", "10002"), "CONSUMABLELOTID", "CONSUMABLELOTID")
                            .SetRelationIds("LOTID", "SPLITCONSUMABLEDEFIDVERSION");

            // 원인공정
            result.AddComboBoxColumn("REASONSEGMENTID", 200, new SqlQuery("GetDefectReasonProcesssegment", "10002"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                            .SetMultiColumns(ComboBoxColumnShowType.Custom, true)
                            .SetRelationIds("SPLITCONSUMABLEDEFIDVERSION", "REASONCONSUMABLELOTID")
                            .SetPopupWidth(600)
                            .SetLabel("REASONPROCESSSEGMENTID")
                            .SetVisibleColumns("PROCESSSEGMENTID", "PROCESSSEGMENTNAME", "USERSEQUENCE", "AREANAME")
                            .SetVisibleColumnsWidth(90, 150, 70, 100);

            // 원인작업장
            result.AddTextBoxColumn("REASONAREAID", 100).SetIsReadOnly().SetIsHidden();
            result.AddTextBoxColumn("REASONAREANAME", 150).SetIsReadOnly().SetLabel("REASONAREAID");

            grdInspectionList.View.AddSpinEditColumn("FILEINSPECTIONTYPE", 150)
                .SetIsHidden();

            grdInspectionList.View.AddSpinEditColumn("FILERESOURCEID", 150)
                .SetIsHidden();

            grdInspectionList.View.AddSpinEditColumn("PROCESSRELNO", 150)
                .SetIsHidden();

            grdInspectionList.View.AddSpinEditColumn("FILENAME1", 150)
                .SetIsHidden();

            grdInspectionList.View.AddSpinEditColumn("FILEDATA1", 150)
                .SetIsHidden();

            grdInspectionList.View.AddSpinEditColumn("FILECOMMENTS1", 150)
                .SetIsHidden();

            grdInspectionList.View.AddSpinEditColumn("FILESIZE1", 150)
                .SetIsHidden();

            grdInspectionList.View.AddSpinEditColumn("FILEEXT1", 150)
                .SetIsHidden();

            grdInspectionList.View.AddSpinEditColumn("FILENAME2", 150)
                .SetIsHidden();

            grdInspectionList.View.AddSpinEditColumn("FILEDATA2", 150)
                .SetIsHidden();

            grdInspectionList.View.AddSpinEditColumn("FILECOMMENTS2", 150)
                .SetIsHidden();

            grdInspectionList.View.AddSpinEditColumn("FILESIZE2", 150)
                .SetIsHidden();

            grdInspectionList.View.AddSpinEditColumn("FILEEXT2", 150)
                .SetIsHidden();

            grdInspectionList.View.PopulateColumns();
		}


        /// <summary>
        /// 합계 Row 초기화
        /// </summary>
        public void InitializationSummaryRow()
		{
			grdInspectionList.View.Columns["DEFECTCODENAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;			
			grdInspectionList.View.Columns["DEFECTCODENAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
			grdInspectionList.View.Columns["INSPECTIONQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdInspectionList.View.Columns["INSPECTIONQTY"].SummaryItem.DisplayFormat = "{0}";
            grdInspectionList.View.Columns["INSPECTIONQTYPNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInspectionList.View.Columns["INSPECTIONQTYPNL"].SummaryItem.DisplayFormat = "{0}";
            grdInspectionList.View.Columns["DEFECTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdInspectionList.View.Columns["DEFECTQTY"].SummaryItem.DisplayFormat = "{0}";
            grdInspectionList.View.Columns["DEFECTQTYPNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            grdInspectionList.View.Columns["DEFECTQTYPNL"].SummaryItem.DisplayFormat = "{0}";
            grdInspectionList.View.Columns["DEFECTRATE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;//***불량율 합계는 100넘을 수있음 다시계산?***
            grdInspectionList.View.Columns["DEFECTRATE"].SummaryItem.DisplayFormat = "{0:f2} %";

            grdInspectionList.View.OptionsView.ShowFooter = true;
			grdInspectionList.ShowStatusBar = false;
		}
    

		/// <summary>
		/// 불량 코드 팝업
		/// </summary>
        private void InitializeGrid_DefectCodeListPopup(ConditionItemGroup item)
        {
            var defectCodePopupColumn = item.AddSelectPopupColumn("DEFECTCODENAME", 250, new SqlQuery("GetDefectCodeByProcess", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetPopupLayout("SELECTDEFECTCODEID", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupResultCount(1)
               .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
               .SetPopupAutoFillColumns("DEFECTCODENAME")
               .SetLabel("DEFECTCODENAME")
               .SetPopupApplySelection((selectedRows, dataGridRows) =>
               {
                    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow


                   DataTable dt = grdInspectionList.DataSource as DataTable;
                   DataRow dr = selectedRows.FirstOrDefault();

                   if (dr != null)
                   {
                       int icnt = dt.Select("DEFECTCODE = '" + dr["DEFECTCODE"].ToString() + "'").Count();
                       int icntQCId = dt.Select("QCSEGMENTID = '" + dr["QCSEGMENTID"].ToString() + "'").Count();
                       if (icnt > 0 && icntQCId > 0)

                       {
                           throw MessageException.Create("DuplicationDefectCode");
                       }

                       dataGridRows["DEFECTCODE"] = dr["DEFECTCODE"].ToString();
                       dataGridRows["QCSEGMENTID"] = dr["QCSEGMENTID"].ToString();
                       dataGridRows["QCSEGMENTNAME"] = dr["QCSEGMENTNAME"].ToString();        
                   }

                    /*
                    foreach (DataRow row in selectedRows)
                    {
                        dataGridRows["DEFECTCODE"] = row["DEFECTCODE"].ToString();
                        dataGridRows["QCSEGMENTID"] = row["QCSEGMENTID"].ToString();
                        dataGridRows["QCSEGMENTNAME"] = row["QCSEGMENTNAME"].ToString();
                    }
                    */

               });

            // 팝업의 검색조건 항목 추가 (불량코드/명)
            defectCodePopupColumn.Conditions.AddTextBox("TXTDEFECTCODE").
                SetLabel("DEFECTCODE");
            // 팝업의 검색조건 항목 추가 (품질공정id/명)
            defectCodePopupColumn.Conditions.AddTextBox("TXTQCSEGMENT")
                .SetLabel("QCSEGMENT");

            defectCodePopupColumn.GridColumns.AddTextBoxColumn("DEFECTCODE", 150)
                .SetIsReadOnly();
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("DEFECTCODENAME", 200)
                .SetIsReadOnly();
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("QCSEGMENTID", 150)
                .SetIsReadOnly();
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("QCSEGMENTNAME", 200)
                .SetIsReadOnly();
        }
		#endregion

		#region Event

		/// <summary>
		/// 이벤트 초기화
		/// </summary>
		public void InitializeEvent()
		{
            //PNL , PCS 수량 변경 이벤트 등록
            //_quantity = new Quantity();
            //_quantity.ChangeQtyValue += ChangePcsQty;
            //_quantity.ChangeQtyValue += ChangePnlQty;

            Load += UcProcessInspDefect_Load;

            grdInspectionList.ToolbarAddingRow += GrdInspectionList_ToolbarAddingRow;
            //grdInspectionList.View.AddingNewRow += View_AddingNewRow;
           
            grdInspectionList.View.ShowingEditor += View_ShowingEditor;
            grdInspectionList.View.CellValueChanged += GrdDefectQTY_CellValueChanged;
            //****grdInspectionList.View.CustomDrawFooterCell += View_CustomDrawFooterCell;
            grdInspectionList.View.CustomSummaryCalculate += View_CustomSummaryCalculate;

            //이미지 추가 버튼 클릭 이벤트
            btnAddImageMeasure.Click += BtnAddImage_Click;
            //이미지 삭제 이벤트
            picBox.KeyDown += PicDefect_KeyDown;
            picBox2.KeyDown += PicDefect_KeyDown;
        }

        private void GrdInspectionList_ToolbarAddingRow(object sender, CancelEventArgs e)
        {
            if (_selectedIndex == 1)
            {//2020-03-23 강유라 무검사 선택시 입력 불가
                MSGBox.Show( MessageBoxType.Information,"CantInputDefectQtyNoInspection"); //인계처리(무검사) 선택시 검사결과를 입력 할 수 없습니다.
                e.Cancel = true;
            }
            else
            { 

                if (!string.IsNullOrEmpty(_selectedLotId))
                {
                    e.Cancel = true;
                    grdInspectionList.View.AddNewRow();
                    DataRow newRow = grdInspectionList.View.GetFocusedDataRow();
                    newRow["LOTID"] = _selectedLotId;
                    newRow["RESOURCEID"] = _selectedLotId;
                    newRow["INSPECTIONQTY"] = LotDataRow["PCSQTY"];
                    newRow["INSPECTIONQTYPNL"] = LotDataRow["PNLQTY"];
                    newRow["DEFECTQTY"] = "0";
                    newRow["DEFECTQTYPNL"] = "0";
                    newRow["DEFECTRATE"] = "0%";

              
                    SetConsumableDefComboBox(_selectedLotId);
                }
            }
        }

        private void UcProcessInspDefect_Load(object sender, EventArgs e)
        {
            picBox.Properties.ShowMenu = false;
            picBox2.Properties.ShowMenu = false;


        }


        /// <summary>
        /// 그리드 Row 추가 시 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {     
            args.NewRow["LOTID"] = _selectedLotId;
            args.NewRow["RESOURCEID"] = _selectedLotId;
            args.NewRow["INSPECTIONQTY"] = LotDataRow["PCSQTY"];
            args.NewRow["INSPECTIONQTYPNL"] = LotDataRow["PNLQTY"];
            args.NewRow["DEFECTQTY"] = "0";
            args.NewRow["DEFECTQTYPNL"] = "0";
            args.NewRow["DEFECTRATE"] = "0%";
        }


        /// <summary>
        /// 이미등록된 결과가 있을 때 셀 ReadOnly 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_ShowingEditor(object sender, CancelEventArgs e)
		{
            if (_type.Equals("updateData"))
            {
                e.Cancel = true;
            }
            else
            {
                if (grdInspectionList.View.FocusedColumn.FieldName.Equals("INSPECTIONQTY")
                || grdInspectionList.View.FocusedColumn.FieldName.Equals("DEFECTQTY"))
                {
                    if (string.IsNullOrWhiteSpace(grdInspectionList.View.GetFocusedDataRow()["DEFECTCODE"].ToString()))
                    {
                        MSGBox.Show(MessageBoxType.Information, "InputDefectCodeFirst");//불량코드를 우선 입력하세요   
                        e.Cancel = true;
                    }
                }
            }
        }


        /// <summary>
        /// PNL관련 올림 처리 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "INSPECTIONQTYPNL" || e.Column.FieldName == "DEFECTQTYPNL")
            {

                e.Info.DisplayText = Math.Ceiling(Format.GetDouble(e.Info.Value, 0.00)).ToString();
            }
        }

        /// <summary>
        /// 불량률 계산
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
		{
			if (e.IsTotalSummary)
			{
				GridSummaryItem item = e.Item as GridSummaryItem;
				if (item.FieldName == "DEFECTRATE")
				{
					double inspectionQty = 0;
					double defectQty = 0;
					if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
					{
                        //inspectionQty = Convert.ToDouble((sender as GridView).Columns["INSPECTIONQTY"].SummaryItem.SummaryValue);
                        inspectionQty = LotDataRow["PCSQTY"].ToSafeDoubleNaN();
                        defectQty = Convert.ToDouble((sender as GridView).Columns["DEFECTQTY"].SummaryItem.SummaryValue);
						if(inspectionQty != 0 && defectQty !=0)
						{
                            decimal defectRate = Math.Round((defectQty / inspectionQty * 100).ToSafeDecimal(), 1);
                            e.TotalValue = defectRate;
                        }
                    }
				}
                
                if (item.FieldName == "DEFECTQTYPNL")
                {
                    double inspectionQty = 0;
                    decimal defectQty = 0;
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        //inspectionQty = Convert.ToDouble((sender as GridView).Columns["INSPECTIONQTY"].SummaryItem.SummaryValue);
                        defectQty = Convert.ToDecimal((sender as GridView).Columns["DEFECTQTY"].SummaryItem.SummaryValue);
                        if (inspectionQty != 0 && defectQty != 0)
                        {
                            decimal defecQtyPnl = Math.Ceiling(defectQty / CurrentDataRow["PANELPERQTY"].ToSafeDecimal());
                            e.TotalValue = defecQtyPnl;
                        }
                    }
                }
            }
		}

        /// <summary>
        /// 검사수량, 불량수량을 입력 했을때 불량율을 계산해준다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdDefectQTY_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow row = grdInspectionList.View.GetFocusedDataRow();

            #region 불량 수량 및 불량률
            /*
            if (e.Column.FieldName == "INSPECTIONQTY" || e.Column.FieldName == "DEFECTQTY")
            {
                //음수를 입력했을 때 0으로 바꿔줌
                if (e.Value.ToSafeInt32() < 0)
                    grdInspectionList.View.SetFocusedRowCellValue(e.Column.FieldName, 0);

                if (row["INSPECTIONQTY"].ToString().Equals("0"))
                {//검사수량이 0일때 return (0으로 나눌수 없음)
                    MSGBox.Show(MessageBoxType.Information, "InspectionQtyCount");//검사수량은 0이 될 수 없습니다.
                    return;
                }

                if (row["INSPECTIONQTY"].ToSafeInt32() < row["DEFECTQTY"].ToSafeInt32())
                {//검사 수량보다 불량수가 많을 때
                    MSGBox.Show(MessageBoxType.Information, "PcsQtyRangeOver");//검사수량보다 불량수량이 초과하였습니다.
                    grdInspectionList.View.SetFocusedRowCellValue("DEFECTQTY", 0);
                    return;
                }

                if (string.IsNullOrWhiteSpace(row["DEFECTQTY"].ToString()))
                {
                    grdInspectionList.View.SetFocusedRowCellValue("DEFECTRATE", null);
                }
                else
                {   //NSPECTIONQTYPNL = INSPECTIONQTY/PANELPERQTY
                    double inspectionQtyPNL = Math.Ceiling(row["INSPECTIONQTY"].ToSafeDoubleZero() / CurrentDataRow["PANELPERQTY"].ToSafeDoubleZero());
                    grdInspectionList.View.SetFocusedRowCellValue("INSPECTIONQTYPNL", inspectionQtyPNL);
                    //DEFECTQTYPNL = DEFECTQTY/PANELPERQTY
                    double defectQtyPNL = Math.Ceiling(row["DEFECTQTY"].ToSafeDoubleZero() / CurrentDataRow["PANELPERQTY"].ToSafeDoubleZero());
                    grdInspectionList.View.SetFocusedRowCellValue("DEFECTQTYPNL", defectQtyPNL);

                    //불량율계산
                    decimal defectRate = Math.Round((row["DEFECTQTY"].ToSafeDecimal() / row["INSPECTIONQTY"].ToSafeDecimal() * 100).ToSafeDecimal(), 1);
                    string per = defectRate.ToString() + "%";
                    grdInspectionList.View.SetFocusedRowCellValue("DEFECTRATE", per);
                    grdInspectionList.View.SetFocusedRowCellValue("QTY", e.Value);
                }
            }*/

            //2020-01-08 PNL수량만 바꿀수 있게 수정
            if (e.Column.FieldName == "DEFECTQTYPNL")
            {
                //음수를 입력했을 때 0으로 바꿔줌
                if (e.Value.ToSafeInt32() < 0)
                    grdInspectionList.View.SetFocusedRowCellValue(e.Column.FieldName, 0);

                var allDefectPNLQty = (grdInspectionList.DataSource as DataTable).AsEnumerable()
                    .Sum(r => r["DEFECTQTYPNL"].ToSafeInt32());

                if (row["INSPECTIONQTYPNL"].ToSafeInt32() < e.Value.ToSafeInt32() || row["INSPECTIONQTYPNL"].ToSafeInt32() < allDefectPNLQty)
                {//검사 수량보다 불량수가 많을 때
                    MSGBox.Show(MessageBoxType.Information, "PcsQtyRangeOver");//검사수량보다 불량수량이 초과하였습니다.
                    grdInspectionList.View.SetFocusedRowCellValue(e.Column.FieldName, 0);
                    return;
                }

                //DEFECTQTY = DEFECTQTYPNL * PANELPERQTY
                double defectQty = e.Value.ToSafeInt32() * CurrentDataRow["PANELPERQTY"].ToSafeDoubleZero();
                grdInspectionList.View.SetFocusedRowCellValue("DEFECTQTY", defectQty);

                //불량율계산
                decimal defectRate = Math.Round((defectQty.ToSafeDecimal() / row["INSPECTIONQTY"].ToSafeDecimal() * 100).ToSafeDecimal(), 1);
                string per = defectRate.ToString() + "%";
                grdInspectionList.View.SetFocusedRowCellValue("DEFECTRATE", per);
                //불량 처리시 API 컬럼명 (DEFECTQTY)
                grdInspectionList.View.SetFocusedRowCellValue("QTY", defectQty);               
            }

            #endregion

            #region - 원인 품목 / 공정 / 자재등
            if (e.Column.FieldName == "REASONCONSUMABLEDEFIDVERSION")
            {
                grdInspectionList.View.CellValueChanged -= GrdDefectQTY_CellValueChanged;

                RepositoryItemLookUpEdit edit = e.Column.ColumnEdit as RepositoryItemLookUpEdit;

                string consumableDefId = Format.GetString(edit.GetDataSourceValue("CONSUMABLEDEFID", edit.GetDataSourceRowIndex("SPLITCONSUMABLEDEFIDVERSION", e.Value)));
                string consumableDefVersion = Format.GetString(edit.GetDataSourceValue("CONSUMABLEDEFVERSION", edit.GetDataSourceRowIndex("SPLITCONSUMABLEDEFIDVERSION", e.Value)));

                grdInspectionList.View.SetFocusedRowCellValue("SPLITCONSUMABLEDEFIDVERSION", e.Value);
                grdInspectionList.View.SetFocusedRowCellValue("REASONCONSUMABLEDEFID", consumableDefId);
                grdInspectionList.View.SetFocusedRowCellValue("REASONCONSUMABLEDEFVERSION", consumableDefVersion);

                grdInspectionList.View.CellValueChanged += GrdDefectQTY_CellValueChanged;
            }

            if (e.Column.FieldName == "REASONSEGMENTID")
            {
                grdInspectionList.View.CellValueChanged -= GrdDefectQTY_CellValueChanged;

                RepositoryItemLookUpEdit edit = e.Column.ColumnEdit as RepositoryItemLookUpEdit;

                string areaId = Format.GetString(edit.GetDataSourceValue("AREAID", edit.GetDataSourceRowIndex("PROCESSSEGMENTID", e.Value)));
                string areaName = Format.GetString(edit.GetDataSourceValue("AREANAME", edit.GetDataSourceRowIndex("PROCESSSEGMENTID", e.Value)));

                grdInspectionList.View.SetFocusedRowCellValue("REASONAREAID", areaId);
                grdInspectionList.View.SetFocusedRowCellValue("REASONAREANAME", areaName);

                grdInspectionList.View.CellValueChanged += GrdDefectQTY_CellValueChanged;
            }
            #endregion
        }

        /// <summary>
        /// 이미지 추가버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddImage_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row = grdInspectionList.View.GetFocusedDataRow();
                if (row == null) return;

                if (_type.Equals("updateData"))
                {
                    MSGBox.Show(MessageBoxType.Information, "CanAddImageBeforeResultSave");//검사 결과가 저장된 후에는 이미지를 추가 할 수 없습니다.                   
                    return;
                }

                DialogManager.ShowWaitArea(this);

                string imageFile = string.Empty;

                OpenFileDialog dialog = new OpenFileDialog
                {
                    Multiselect = false,
                    Filter = "Image Files (*.bmp, *.jpg, *.jpeg, *.png)|*.BMP;*.JPG;*.JPEG;*.PNG",
                    FilterIndex = 0
                };

                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    row["FILEINSPECTIONTYPE"] = "ProcessInspection";
                    row["FILERESOURCEID"] = CurrentDataRow["LOTID"] + row["DEFECTCODE"].ToString() + row["QCSEGMENTID"].ToString() + "D" + CurrentDataRow["DEGREE"].ToString();
                    row["PROCESSRELNO"] = CurrentDataRow["LOTHISTKEY"] + CurrentDataRow["DEGREE"].ToString();

                    imageFile = dialog.FileName;
                    FileInfo fileInfo = new FileInfo(dialog.FileName);
                    using (FileStream fs = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        byte[] data = new byte[fileInfo.Length];
                        fs.Read(data, 0, (int)fileInfo.Length);

                        MemoryStream ms = new MemoryStream(Convert.FromBase64String(Convert.ToBase64String(data).ToString()));
                        if (picBox.Image == null)
                        {
                            row["FILENAME1"] = dialog.SafeFileName;
                            //2. 파일을 읽어들일때 File Binary를 읽어오던 부분을 경로를 저장하는 것으로 변경
                            //=========================================================================================================================
                            //YJKIM : binary파일을 저장하지 않고 File을 Upload하는 형태로 변경
                            //File Data를 저장할 필요가 없음
                            //row["FILEDATA1"] = (byte[])new ImageConverter().ConvertTo(new Bitmap(dialog.FileName), typeof(byte[]));
                            row["FILEFULLPATH1"] = dialog.FileName; //파일의 전체경로 저장
                            row["FILEID1"] = "FILE-" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                            //row["FILEDATA1"] = (byte[])new ImageConverter().ConvertTo(new Bitmap(dialog.FileName), typeof(byte[]));
                            //-------------------------------------------------------------------------------------------------------------------------
                            row["FILECOMMENTS1"] = "InspectionResult/ProcessInspection";
                            row["FILESIZE1"] = Math.Round(Convert.ToDouble(fileInfo.Length) / 1024);
                            row["FILEEXT1"] = fileInfo.Extension.Substring(1);

                            picBox.Image = Image.FromStream(ms);
                        }
                        else
                        {
                            row["FILENAME2"] = dialog.SafeFileName;
                            //2. 파일을 읽어들일때 File Binary를 읽어오던 부분을 경로를 저장하는 것으로 변경
                            //=========================================================================================================================
                            //YJKIM : binary파일을 저장하지 않고 File을 Upload하는 형태로 변경
                            //File Data를 저장할 필요가 없음
                            //row["FILEDATA1"] = (byte[])new ImageConverter().ConvertTo(new Bitmap(dialog.FileName), typeof(byte[]));
                            row["FILEFULLPATH2"] = dialog.FileName; //파일의 전체경로 저장
                            row["FILEID2"] = "FILE-" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                            //row["FILEDATA2"] = (byte[])new ImageConverter().ConvertTo(new Bitmap(dialog.FileName), typeof(byte[]));
                            //-------------------------------------------------------------------------------------------------------------------------
                            row["FILECOMMENTS2"] = "InspectionResult/ProcessInspection";
                            row["FILESIZE2"] = Math.Round(Convert.ToDouble(fileInfo.Length) / 1024);
                            row["FILEEXT2"] = fileInfo.Extension.Substring(1);
                            picBox2.Image = Image.FromStream(ms);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw Framework.MessageException.Create(ex.ToString());
            }
            finally
            {
                DialogManager.CloseWaitArea(this);
            }
        }

        /// <summary>
        /// PictureBox에 delete버튼 클릭시 사진지우는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicDefect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && _type.Equals("insertData"))
            {
                DataRow row = grdInspectionList.View.GetFocusedDataRow();
                if (row == null) return;
                SmartPictureEdit picBox = sender as SmartPictureEdit;
                picBox.Image = null;

                if (picBox.Name.Equals(picBox.Name))
                {
                    row["FILEDATA1"] = null;
                    row["FILECOMMENTS1"] = null;
                    row["FILESIZE1"] = DBNull.Value;
                    row["FILEEXT1"] = null;
                    row["FILENAME1"] = null;
                    //===========================================================================
                    //YJKIM : File의 FullPath를 저장할 필드를 설정
                    row["FILEFULLPATH1"] = null;
                    row["FILEID1"] = null;
                    //---------------------------------------------------------------------------

                }
                else if (picBox.Name.Equals(picBox2.Name))
                {
                    row["FILEDATA2"] = null;
                    row["FILECOMMENTS2"] = null;
                    row["FILESIZE2"] = DBNull.Value;
                    row["FILEEXT2"] = null;
                    row["FILENAME2"] = null;
                    //===========================================================================
                    //YJKIM : File의 FullPath를 저장할 필드를 설정
                    row["FILEFULLPATH2"] = null;
                    row["FILEID2"] = null;
                    //----------------------------------------------------------------------------
                }
            }
        }

        /// <summary>
        /// 포커스된 row에 등록된 이미지를 보여주는 이벤트(저장전)
        /// inspItem dt에 파일정보 저장하여 이미지 보여주기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FocusedRowChangedBeforeSave(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SmartBandedGridView view = sender as SmartBandedGridView;
            DataRow row = view.GetDataRow(e.FocusedRowHandle);
            if (row == null) return;
   
            picBox.Image = null;
            picBox2.Image = null;
            //======================================================================================================================================
            //YJKIM : 파일저장전 로컬경로의 이미지를 보여줄 때 사용
            if (row["FILEFULLPATH1"].ToString() == string.Empty && row["FILENAME1"].ToString() == string.Empty
                    && row["FILEFULLPATH2"].ToString() == string.Empty && row["FILENAME2"].ToString() == string.Empty)
            {
                return;
            }

            ImageConverter converter = new ImageConverter();

            if (!string.IsNullOrWhiteSpace(row["FILEFULLPATH1"].ToString()))
            {
                picBox.Image = QcmImageHelper.GetImageFromFile(row["FILEFULLPATH1"].ToString());//(Image)converter.ConvertFrom(row["FILEFULLPATH1"]);
                
            }

            if (!string.IsNullOrWhiteSpace(row["FILEFULLPATH2"].ToString()))
            {
                picBox2.Image = QcmImageHelper.GetImageFromFile(row["FILEFULLPATH2"].ToString());//(Image)converter.ConvertFrom(row["FILEFULLPATH2"]);
            }
            
        }

        /// <summary>
        /// 포커스된 row에 등록된 이미지를 보여주는 이벤트(저장후)
        /// lotid+inspItem 으로 파일정보 셀렉트하여 이미지 보여주기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SmartBandedGridView view = sender as SmartBandedGridView;
            DataRow row = view.GetDataRow(e.FocusedRowHandle);
            SearchImage(row);
        }

        #endregion

        #region Public Function
        /// <summary>
        /// REASONCONSUMABLEDEFID 콤보초기화
        /// </summary>
        public void SetConsumableDefComboBox(string lotId)
        {
            if (!string.IsNullOrEmpty(lotId))
            {
                grdInspectionList.View.RefreshComboBoxDataSource("REASONCONSUMABLEDEFIDVERSION", new SqlQuery("GetReasonConsumableList", "10002", $"LOTID={lotId}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
                grdInspectionList.View.RefreshComboBoxDataSource("REASONCONSUMABLELOTID", new SqlQuery("GetDefectReasonConsumableLot", "10002", $"LOTID={lotId}"));
                grdInspectionList.View.RefreshComboBoxDataSource("REASONSEGMENTID", new SqlQuery("GetDefectReasonProcesssegment", "10002", $"LOTID={lotId}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            }

        }

        /// <summary>
        /// 그리드의 데이터를 클리어 하는 함수
        /// </summary>
        public void ClearUserControlGrd()
        {
            grdInspectionList.View.ClearDatas();

            picBox.Image = null;
            picBox2.Image = null;

        }
        /// <summary>
        /// Get Grid Datasource
        /// </summary>
        /// <returns></returns>
        public DataTable GetGridDataSource()
        {
            DataTable dt = grdInspectionList.DataSource as DataTable;

            if (dt.Rows.Count == 0)
            {
                return dt;
            }
            else
            {
                CheckAllInput(dt);

                if (_inputAll == false)
                {
                    throw MessageException.Create("NeedToInputAllDataOSPInsp");//불량(폐기) 처리의 모든 항목을 입력해주세요.
                }
                return dt;
            }

            
        }
  
        public void SetLotIdAndData(DataRow row, DataRow lotRow, DataTable dt, string type)
        {
            CurrentDataRow = row;
            LotDataRow = lotRow;
            _selectedLotId = CurrentDataRow["LOTID"].ToString();
            _type = type;

            grdInspectionList.DataSource = dt;
            SearchImage(grdInspectionList.View.GetFocusedDataRow());

            if (_type.Equals("insertData"))
            {
                grdInspectionList.View.FocusedRowChanged += FocusedRowChangedBeforeSave;
            }
            else if (_type.Equals("updateData"))
            {
                grdInspectionList.View.FocusedRowChanged += View_FocusedRowChanged;
            }

        }

        /// <summary>
        /// 팝업에 _defectQtySum 전달
        /// </summary>
        /// <returns></returns>
        public double SetDefectQtySum()
        {
            return _defectQtySum;
        }


        /// <summary>
        /// 팝업에 _defectQtyPNLSum 전달
        /// </summary>
        /// <returns></returns>
        public double SetDefectQtyPNLSum()
        {
            return _defectQtyPNLSum;
        }

        public void SetType(string type)
        {
            _type = type;
        }

        /// <summary>
        /// 라디오 버튼으로 선택한 값 설정 함수
        /// </summary>
        /// <param name="selecteIndex"></param>
        public void SetSelectedIndex(int selecteIndex)
        {
            _selectedIndex = selecteIndex;
        }
        #endregion

        #region Private Function
        /// <summary>
        /// 모든 정보가 입려되었는지 체크하고
        /// 불량수량 합을 구하는 함수
        /// </summary>
        /// <param name="dt"></param>
        private void CheckAllInput(DataTable dt)
        {
            _inputAll = true;

            _defectQtySum = 0.0;
            _defectQtyPNLSum = 0.0;

            foreach (DataRow row in dt.Rows)
            {
                _defectQtySum += row["DEFECTQTY"].ToSafeDoubleNaN();
                //_defectQtyPNLSum += row["DEFECTQTYPNL"].ToSafeDoubleNaN();

                if (string.IsNullOrWhiteSpace(row["DEFECTCODE"].ToString()) || string.IsNullOrWhiteSpace(row["INSPECTIONQTY"].ToString())
                    || string.IsNullOrWhiteSpace(row["DEFECTQTY"].ToString()))
                {
                    _inputAll = false;
                    break;
                }

            }
            _defectQtyPNLSum = Math.Ceiling(_defectQtySum / CurrentDataRow["PANELPERQTY"].ToSafeDoubleNaN());
            //_defectQtyPNLSum = Math.Ceiling(_defectQtySum / CurrentDataRow["PANELPERQTY"].ToSafeDoubleNaN());
        }

        /// <summary>
        /// focuse된 row로 이미지 찾는 함수
        /// </summary>
        /// <param name="row"></param>
        private void SearchImage(DataRow row)
        {
            if (row == null) return;

            picBox.Image = null;
            picBox2.Image = null;

            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                {"RESOURCETYPE","ProcessInspection" },
                {"RESOURCEID",CurrentDataRow["LOTID"] + row["DEFECTCODE"].ToString() + row["QCSEGMENTID"].ToString()+ "D" +CurrentDataRow["DEGREE"].ToString()},
                {"RESOURCEVERSION", "*"}

            };

            _fileDt = SqlExecuter.Query("GetFileHttpPathFromObjectFileByStandardInfo", "10001", values);

            foreach (DataRow fileRow in _fileDt.Rows)
            {
                string filenameAndExt = fileRow.GetString("FILENAME") + "." + fileRow.GetString("FILEEXT");

                if (picBox.Image == null)
                {   
                    //2020-01-28 파일 경로변경
                    picBox.Image = CommonFunction.GetFtpImageFileToBitmap(fileRow.GetString("FILEPATH"), filenameAndExt);
                }
                else
                {
                    picBox2.Image = CommonFunction.GetFtpImageFileToBitmap(fileRow.GetString("FILEPATH"), filenameAndExt);
                }

            }
        }

        /*
        /// <summary>
        /// REASONCONSUMABLEDEFID 콤보초기화
        /// </summary>
        public void SetConsumableDefComboBox(String lotId)
        {
            string lotid = "";
            if (lotId == null)
            {
                lotid = grdInspectionList.View.GetFocusedRowCellValue("LOTID").ToString();

            }
            else
            {
                lotid = lotId;
            }

            grdInspectionList.View.RefreshComboBoxDataSource("REASONCONSUMABLEDEFID", new SqlQuery("GetReasonConsumableList", "10001", $"LOTID={lotid}"));
            _isQueryInitialize = true;
        }

        public void SetReasonCombo(String lotId)
        {
            grdInspectionList.View.RefreshComboBoxDataSource("REASONCONSUMABLELOTID", new SqlQuery("GetDefectReasonConsumableLotDisplay", "10001"
                , $"LOTID={lotId}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdInspectionList.View.RefreshComboBoxDataSource("REASONSEGMENTID", new SqlQuery("GetDefectReasonProcesssegmentDisplay", "10001"
                                , $"LOTID={lotId}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdInspectionList.View.RefreshComboBoxDataSource("REASONAREAID", new SqlQuery("GetDefectReasonAreaDisplay", "10001", $"LOTID={lotId}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
        }*/
        #endregion

    }
}
