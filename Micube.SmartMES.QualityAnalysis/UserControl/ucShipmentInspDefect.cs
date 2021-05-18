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
using DevExpress.Utils;
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
	public partial class ucShipmentInspDefect : UserControl
	{

        #region Local Variables
        public DataRow CurrentDataRow = null;
        private DataTable _fileDt;
        //Selected Lot ID
        private string _selectedLotId = string.Empty;
        public SmartPopupBaseForm parentForm;
        bool _inputAll = true;
        double _defectQtySum = 0.0;
        double _defectQtyPNLSum = 0.0;
        bool autoChange = false;
        DataTable _tempSave;
        bool _isQueryInitialize = false;
        #endregion

       
        #region 생성자
        public ucShipmentInspDefect()
		{
			InitializeComponent();

            if (!smartSpliterContainer1.IsDesignMode())
			{
				InitializeGrid();
                InitializeEvent();
                InitializationSummaryRow();
			}
		}
		#endregion

		#region 컨텐츠 영역 초기화
		/// <summary>
		/// 그리드 초기화
		/// </summary>
		private void InitializeGrid()
		{
            #region LOT LIST 그리드 초기화
            grdChildLot.View.SetIsReadOnly();

            grdChildLot.View.AddTextBoxColumn("RESOURCEID",200)
                .SetLabel("CHILDLOTID");

            grdChildLot.View.AddTextBoxColumn("LOTID", 200)
                .SetIsHidden();

            grdChildLot.View.AddTextBoxColumn("DEGREE",80);

            //전체수량
            var allQty = grdChildLot.View.AddGroupColumn("ALL");
            allQty.AddTextBoxColumn("ALLQTYPNL", 100)
                .SetLabel("PNL");
            allQty.AddTextBoxColumn("ALLQTYPCS", 100)
                .SetLabel("PCS");

            grdChildLot.View.PopulateColumns();
            #endregion

            #region 불량 그리드 초기화
            grdInspectionList.View.ClearColumns();

            grdInspectionList.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
            
            //구분 등 검사항목
            var item = grdInspectionList.View.AddGroupColumn("");

            //LOT ID
            item.AddTextBoxColumn("RESOURCEID",200)
				//.SetIsHidden()
				.SetIsReadOnly();

            item.AddTextBoxColumn("LOTID", 200)
                .SetIsHidden();

            item.AddTextBoxColumn("DEGREE", 80)
                .SetIsHidden();

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

            grdInspectionList.View.AddSpinEditColumn("INSPECTIONQTY", 150)
                .SetIsHidden();

            grdInspectionList.View.AddSpinEditColumn("INSPECTIONQTYPNL", 150)
                .SetIsHidden();


            grdInspectionList.View.AddSpinEditColumn("DEFECTQTYSUM", 150)
                .SetIsHidden();

            grdInspectionList.View.AddSpinEditColumn("DEFECTQTYPNLSUM", 150)
                .SetIsHidden();

            grdInspectionList.View.AddSpinEditColumn("QTY", 150)
                .SetIsHidden();

            grdInspectionList.View.AddSpinEditColumn("ISDELETE", 150)
                .SetIsHidden();

            //불량수량
            var groupSpecOutQty = grdInspectionList.View.AddGroupColumn("SPECOUTQTY");
            //PCS
            groupSpecOutQty.AddSpinEditColumn("DEFECTQTY", 100)
                .SetLabel("PCS")
                .SetDisplayFormat("#,##0", MaskTypes.Numeric, false)
                .SetTextAlignment(TextAlignment.Right);
            //PNL
            groupSpecOutQty.AddSpinEditColumn("DEFECTQTYPNL", 100)
                .SetLabel("PNL")
                .SetIsReadOnly()
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

            grdInspectionList.View.AddTextBoxColumn("FILEINSPECTIONTYPE", 150)
                .SetIsHidden();

            grdInspectionList.View.AddTextBoxColumn("FILERESOURCEID", 150)
                .SetIsHidden();

            grdInspectionList.View.AddTextBoxColumn("PROCESSRELNO", 150)
                .SetIsHidden();

            grdInspectionList.View.AddTextBoxColumn("FILENAME1", 150)
                .SetIsHidden();

            grdInspectionList.View.AddTextBoxColumn("FILEFULLPATH1", 150)
                .SetIsHidden();

            grdInspectionList.View.AddTextBoxColumn("FILEID1", 150)
                .SetIsHidden();

            grdInspectionList.View.AddTextBoxColumn("FILEDATA1", 150)
                .SetIsHidden();

            grdInspectionList.View.AddTextBoxColumn("FILECOMMENTS1", 150)
                .SetIsHidden();

            grdInspectionList.View.AddSpinEditColumn("FILESIZE1", 150)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric, false)
                .SetIsHidden();

            grdInspectionList.View.AddTextBoxColumn("FILEEXT1", 150)
                .SetIsHidden();

            grdInspectionList.View.AddTextBoxColumn("FILENAME2", 150)
                .SetIsHidden();

            grdInspectionList.View.AddTextBoxColumn("FILEFULLPATH2", 150)
                .SetIsHidden();

            grdInspectionList.View.AddTextBoxColumn("FILEID2", 150)
                .SetIsHidden();

            grdInspectionList.View.AddTextBoxColumn("FILEDATA2", 150)
                .SetIsHidden();

            grdInspectionList.View.AddTextBoxColumn("FILECOMMENTS2", 150)
                .SetIsHidden();

            grdInspectionList.View.AddSpinEditColumn("FILESIZE2", 150)
                .SetDisplayFormat("#,###", MaskTypes.Numeric, false)
                .SetIsHidden();

            grdInspectionList.View.AddTextBoxColumn("FILEEXT2", 150)
                .SetIsHidden();

            grdInspectionList.View.PopulateColumns();
            #endregion
        }

        #region 그리드 팝업 초기화
        /*
        /// <summary>
        /// 원인 품목 팝업
        /// </summary>
        private void InitializeGrid_CauseProductIdPopup()
        {
            var causeProductIdColumn = grdInspectionList.View.AddSelectPopupColumn("CONSUMABLEDEFNAME", 250, new SqlQuery("GetCauseMaterialList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("SELECTREASONMATERIAL", PopupButtonStyles.Ok_Cancel, false, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.SizableToolWindow)
                .SetLabel("REASONPRODUCT")
                .SetPopupQueryPopup((DataRow currentRow) =>
                {
                    currentRow["PARAMLOTID"] = CurrentDataRow["RESOURCEID"];
                    return true;
                })
                .SetPopupApplySelection((selectedRows, gridRows) =>
                {
                    DataRow row = selectedRows.FirstOrDefault();

                    if (row != null)
                    {
                        string hasRouting = Format.GetString(row["HASROUTING"]);
                        string materialLotId = Format.GetString(row["MATERIALLOTID"]);

                        if (hasRouting.Equals("Y"))
                        {
                            gridRows["PARAMLOTID"] = materialLotId;
                            gridRows["REASONPRODUCTDEFID"] = Format.GetString(row["CONSUMABLEDEFID"]);
                            gridRows["REASONPRODUCTDEFVERSION"] = Format.GetString(row["CONSUMABLEDEFVERSION"]);
                        }


                        gridRows["REASONCONSUMABLELOTID"] = materialLotId;//자재LOTID아님 
                        gridRows["REASONCONSUMABLEDEFID"] = Format.GetString(row["CONSUMABLEDEFID"]);
                        gridRows["REASONCONSUMABLEDEFVERSION"] = Format.GetString(row["CONSUMABLEDEFVERSION"]);

                        gridRows["MATERIALLOTID"] = materialLotId;
                        gridRows["HASROUTING"] = hasRouting;
                        gridRows["CONSUMABLEDEFID"] = Format.GetString(row["CONSUMABLEDEFID"]);
                        gridRows["CONSUMABLEDEFVERSION"] = Format.GetString(row["CONSUMABLEDEFVERSION"]);
                        gridRows["REASONAREAID"] = string.Empty;
                        gridRows["REASONAREANAME"] = string.Empty;
                    }
                    else
                    {
                        gridRows["REASONPRODUCTDEFID"] = string.Empty;
                        gridRows["REASONPRODUCTDEFVERSION"] = string.Empty;
                        gridRows["REASONCONSUMABLELOTID"] = string.Empty;
                        gridRows["HASROUTING"] = string.Empty;
                        gridRows["CONSUMABLEDEFID"] = string.Empty;
                        gridRows["CONSUMABLEDEFVERSION"] = string.Empty;
                        gridRows["REASONSEGMENTID"] = string.Empty;
                        gridRows["REASONSEGMENTNAME"] = string.Empty;
                        gridRows["REASONAREAID"] = string.Empty;
                        gridRows["REASONAREANAME"] = string.Empty;
                    }

                });

            causeProductIdColumn.Conditions.AddTextBox("LOTID")
                .SetPopupDefaultByGridColumnId("PARAMLOTID")
                .SetIsHidden();
            causeProductIdColumn.Conditions.AddTextBox("PROCESSSEGMENTID")
                .SetPopupDefaultByGridColumnId("REASONSEGMENTID")
                .SetIsHidden();
            causeProductIdColumn.Conditions.AddTextBox("PROCESSSEGMENTVERSION")
                .SetPopupDefaultByGridColumnId("REASONSEGMENTVERSION")
                .SetIsHidden();

            causeProductIdColumn.GridColumns.AddTextBoxColumn("CONSUMABLEDEFID", 150);
            causeProductIdColumn.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 250);
            causeProductIdColumn.GridColumns.AddTextBoxColumn("CONSUMABLEDEFVERSION", 80);
            causeProductIdColumn.GridColumns.AddTextBoxColumn("LOTID", 100)
                .SetIsHidden();
            causeProductIdColumn.GridColumns.AddTextBoxColumn("HASROUTING", 100)
                .SetIsHidden();
            causeProductIdColumn.GridColumns.AddTextBoxColumn("PRODUCT", 100)
                .SetIsHidden();
        }

        /// <summary>
        /// 원인 공정 팝업
        /// </summary>
        private void InitializeGrid_CauseSegmentIdPopup()
        {
            var causeSegmentIdColumn = grdInspectionList.View.AddSelectPopupColumn("REASONSEGMENTNAME", 200, new SqlQuery("GetCauseProcessList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("SELECTREASONSEGMENT", PopupButtonStyles.Ok_Cancel, false, true)
                .SetPopupResultCount(1)
                .SetPopupResultMapping("REASONSEGMENTID", "PROCESSSEGMENTID")
                .SetPopupResultMapping("REASONSEGMENTNAME", "PROCESSSEGMENTNAME")
                .SetPopupLayoutForm(900, 600, FormBorderStyle.SizableToolWindow)
                .SetLabel("CAUSEPROCESS")
                .SetPopupApplySelection((selectedRows, gridRows) => {
                    DataRow row = selectedRows.FirstOrDefault();

                    if (row != null)
                    {
                        gridRows["REASONSEGMENTID"] = Format.GetString(row["PROCESSSEGMENTID"]);
                        gridRows["REASONSEGMENTVERSION"] = Format.GetString(row["PROCESSSEGMENTVERSION"]);
                        gridRows["REASONAREAID"] = Format.GetString(row["AREAID"]);
                        gridRows["REASONAREANAME"] = Format.GetString(row["AREANAME"]);
                    }
                    else
                    {
                        gridRows["REASONSEGMENTID"] = string.Empty;
                        gridRows["REASONSEGMENTVERSION"] = string.Empty;
                        gridRows["REASONAREAID"] = string.Empty;
                        gridRows["REASONAREANAME"] = string.Empty;
                    }

                });

            //조회조건
            causeSegmentIdColumn.Conditions.AddTextBox("LOTID")
                .SetPopupDefaultByGridColumnId("PARAMLOTID")
                .SetIsHidden();
            causeSegmentIdColumn.Conditions.AddTextBox("HASROUTING")
                .SetPopupDefaultByGridColumnId("HASROUTING")
                .SetIsHidden();
            causeSegmentIdColumn.Conditions.AddTextBox("CONSUMABLEDEFID")
                .SetPopupDefaultByGridColumnId("CONSUMABLEDEFID")
                .SetIsHidden();
            causeSegmentIdColumn.Conditions.AddTextBox("CONSUMABLEDEFVERSION")
                .SetPopupDefaultByGridColumnId("CONSUMABLEDEFVERSION")
                .SetIsHidden();
            causeSegmentIdColumn.Conditions.AddTextBox("LANGUAGETYPE")
                .SetDefault(UserInfo.Current.LanguageType)
                .SetIsHidden();
            causeSegmentIdColumn.Conditions.AddTextBox("MATERIALLOTID")
                .SetPopupDefaultByGridColumnId("MATERIALLOTID")
                .SetIsHidden();

            causeSegmentIdColumn.GridColumns.AddTextBoxColumn("USERSEQUENCE", 80);
            causeSegmentIdColumn.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            causeSegmentIdColumn.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            causeSegmentIdColumn.GridColumns.AddTextBoxColumn("PROCESSSEGMENTVERSION", 80);
            causeSegmentIdColumn.GridColumns.AddTextBoxColumn("AREAID", 100);
            causeSegmentIdColumn.GridColumns.AddTextBoxColumn("AREANAME", 150);
            causeSegmentIdColumn.GridColumns.AddTextBoxColumn("PROCESSDEFID", 100)
                .SetIsHidden();
            causeSegmentIdColumn.GridColumns.AddTextBoxColumn("PROCESSDEFVERSION", 100)
                .SetIsHidden();
        }
        */
        #endregion



        /// <summary>
        /// 합계 Row 초기화
        /// </summary>
        private void InitializationSummaryRow()
		{
			grdInspectionList.View.Columns["DEFECTCODENAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;			
			grdInspectionList.View.Columns["DEFECTCODENAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
			grdInspectionList.View.Columns["INSPECTIONQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Max;
			grdInspectionList.View.Columns["INSPECTIONQTY"].SummaryItem.DisplayFormat = "{0}";
            grdInspectionList.View.Columns["INSPECTIONQTYPNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Max;
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
            var defectCodePopupColumn = item.AddSelectPopupColumn("DEFECTCODENAME", 150, new SqlQuery("GetDefectCodeList", "10004", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
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

			defectCodePopupColumn.Conditions.AddComboBox("DEFECTCODECLASSID", new SqlQuery("GetDefectCodeClassList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "DEFECTCODECLASSNAME", "DEFECTCODECLASSID");
			defectCodePopupColumn.Conditions.AddTextBox("TXTDEFECTCODENAME");

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
		private void InitializeEvent()
		{
            Load += ucShipmentInspDefect_Load;
            //LotList 그리드 포커스 체인지 이벤트
            grdChildLot.View.FocusedRowChanged += View_FocusedRowChanged_CheckSave;

            grdInspectionList.View.AddingNewRow += View_AddingNewRow;
            grdInspectionList.ToolbarDeletingRow += (s, e) =>
            {
                DataRow deleteRow = grdInspectionList.View.GetFocusedDataRow();
                if (deleteRow == null) return;
                if (!deleteRow.RowState.Equals(DataRowState.Added))
                {
                    deleteRow["ISDELETE"] = "Y";

                    picBox.Image = null;
                    picBox2.Image = null;
                    
                    e.Cancel = true;
                }


                if ((grdInspectionList.DataSource as DataTable).Rows.Count == 1)
                {
                    picBox.Image = null;
                    picBox2.Image = null;
                }            
            };
            grdInspectionList.View.CellValueChanged += GrdDefectQTY_CellValueChanged;
            grdInspectionList.View.CustomSummaryCalculate += View_CustomSummaryCalculate;

            grdInspectionList.View.FocusedRowChanged += FocusedRowChangedBeforeSave;
            //이미지 추가 버튼 클릭 이벤트
            btnAddImageMeasure.Click += BtnAddImage_Click;
            //이미지 삭제 이벤트
            picBox.KeyDown += PicDefect_KeyDown;
            picBox2.KeyDown += PicDefect_KeyDown;

            //이미지 tooltip 이벤트
            picBox.MouseEnter += PicBox_MouseEnter;
            picBox2.MouseEnter += PicBox_MouseEnter;

            //임시저장 클릭시 이벤트
            btnTempSave.Click += BtnTempSave_Click;
        }

        /// <summary>
        /// Load 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucShipmentInspDefect_Load(object sender, EventArgs e)
        {
            picBox.Properties.ShowMenu = false;
            picBox2.Properties.ShowMenu = false;

        }

        /// <summary>
        /// tooltip이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicBox_MouseEnter(object sender, EventArgs e)
        {
            SmartPictureEdit pic = sender as SmartPictureEdit;

            SuperToolTip tooltip = new SuperToolTip();
            SuperToolTipSetupArgs args = new SuperToolTipSetupArgs();

            Image image = null;
            image = pic.Image;
            pic.SuperTip = null;
            args.Contents.Image = null;

            if (image == null) return;

            args.Contents.Image = image;
            tooltip.Setup(args);

            pic.SuperTip = tooltip;
        }


        /// <summary>
        /// 불량코드 그리드 add버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            args.NewRow["RESOURCEID"] = CurrentDataRow["RESOURCEID"];
            args.NewRow["LOTID"] = CurrentDataRow["RESOURCEID"];
            args.NewRow["DEGREE"] = CurrentDataRow["DEGREE"];
            args.NewRow["INSPECTIONQTY"] = CurrentDataRow["ALLQTYPCS"].ToSafeDoubleZero();
            args.NewRow["INSPECTIONQTYPNL"] = CurrentDataRow["ALLQTYPNL"].ToSafeDoubleZero();
            args.NewRow["DEFECTQTY"] = "0";
            args.NewRow["DEFECTQTYPNL"] = "0";
            args.NewRow["DEFECTRATE"] = "0%";

            string lotid = grdInspectionList.View.GetFocusedRowCellValue("RESOURCEID").ToString();

            SetConsumableDefComboBox(lotid);
            

        }

        /// <summary>
        /// grdChildLot 포커스 된 row가 바뀔때 임시 저장한(_tempSave) Data를 grdChildLotDetail에 보여 주는 함수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged_CheckSave(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (autoChange == true)
            {
                autoChange = false;
                return;
            }

            DataRow beforeRow = grdChildLot.View.GetDataRow(e.PrevFocusedRowHandle);

            if (beforeRow == null) return;

            int tempCount = 0;
            if (_tempSave != null && e.PrevFocusedRowHandle >= 0)
            {
                tempCount = _tempSave.AsEnumerable().
                Where(r => r["RESOURCEID"].ToString().Equals(beforeRow["RESOURCEID"].ToString())
                && r["DEGREE"].ToString().Equals(beforeRow["DEGREE"].ToString()))
                .ToList().Count;
            }

            if ((grdInspectionList.DataSource as DataTable).Rows.Count > 0 && tempCount == 0)
            {
                DialogResult result = System.Windows.Forms.DialogResult.No;
             
                result = MSGBox.Show(MessageBoxType.Information, "HasDefectData",MessageBoxButtons.YesNo);//임시저장하지 않은 불량정보가 있습니다. 임시저장하지 않은 데이터는 저장되지않습니다. 선택한 LOT을 바꾸시겠습니까?

                if (result == System.Windows.Forms.DialogResult.No)
                {
                    autoChange = true;
                    grdChildLot.View.FocusedRowHandle = e.PrevFocusedRowHandle;
                    return;
                }

            }

            CurrentDataRow = grdChildLot.View.GetDataRow(e.FocusedRowHandle);

            picBox.Image = null;
            picBox2.Image = null;

            txtChildLotId.Editor.EditValue = CurrentDataRow["RESOURCEID"].ToString();

            SearchDefectByLotId();
        }

        /// <summary>
        /// 임시저장 클릭시 grdInspectionList 데이터를 저장하는
        /// 함수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTempSave_Click(object sender, EventArgs e)
        {
            if (CurrentDataRow == null) return;
            //임시 저장 할 그리드의 데이터
            DataTable defectData = grdInspectionList.DataSource as DataTable;

            bool hasDefectCode = true;

            _defectQtySum = 0.0;
            _defectQtyPNLSum = 0.0;

            foreach (DataRow row in defectData.Rows)
            {

                if (!row["ISDELETE"].ToString().Equals("Y"))
                {
                    _defectQtySum += row["DEFECTQTY"].ToSafeDoubleNaN();

                    //DEFECTCODE가 입력 되지 않거나 이미지가 하나도 등록 되지않은 경우
                    if (string.IsNullOrWhiteSpace(row["DEFECTCODE"].ToString()) ||
                        string.IsNullOrWhiteSpace(row["DEFECTQTY"].ToString()) ||
                        Format.GetInteger(row["DEFECTQTY"]) == 0 ||
                        (string.IsNullOrWhiteSpace(row["FILEFULLPATH1"].ToString()) &&
                        string.IsNullOrWhiteSpace(row["FILEFULLPATH2"].ToString())))
                    { 
                        if (string.IsNullOrWhiteSpace(Format.GetString(row["DEFECTCODE"])))
                        {
                            MSGBox.Show(MessageBoxType.Information, "DefectCodeIsRequired");//불량코드를 먼저 입력하세요.
                            hasDefectCode = false;
                            break;
                        }
                        else if (string.IsNullOrWhiteSpace(Format.GetString(row["DEFECTQTY"])))
                        {
                            MSGBox.Show(MessageBoxType.Information, "DefectQtyRequired");//불량수량을 입력해야 합니다.  
                            hasDefectCode = false;
                            break;
                        }
                        else if (Format.GetInteger(row["DEFECTQTY"]) == 0)
                        {
                            MSGBox.Show(MessageBoxType.Information, "DefectQtyInputZero");//불량수량은 0을 입력할수 없습니다.
                            hasDefectCode = false;
                            break;
                        }
                        else
                        {
                            if (UserInfo.Current.Enterprise.Equals("INTERFLEX"))
                            {
                                MSGBox.Show(MessageBoxType.Information, "DefectCodeAndLeastOneImageRequired");//불량코드와 적어도 하나의 이미지를 입력해야 합니다.
                                hasDefectCode = false;
                                break;
                            }
                        }
                    }
                }
            }

            _defectQtyPNLSum = Math.Ceiling(_defectQtySum / CurrentDataRow["PANELPERQTY"].ToSafeDoubleNaN());

            if (hasDefectCode == false)
                return;

            if (defectData.Rows.Count > 0)
            {
                if (_tempSave == null)
                {//임시테이블 DataTable null 일때 -> grdChildLotDetail DataSource 
                    _tempSave = defectData.Clone();

                    //모든 ROW 저장
                    AddAllRowTempSaveTable(defectData);
                }
                else
                {
                    var tempSaved = _tempSave.Rows.Cast<DataRow>()
                            .Where(r => r["RESOURCEID"].ToString().Equals(CurrentDataRow["RESOURCEID"].ToString()) &&
                           r["DEGREE"].ToString().Equals(CurrentDataRow["DEGREE"].ToString()))
                           .ToList();

                    if (tempSaved.Count == 0)
                    {
                        //모든 ROW 저장
                        AddAllRowTempSaveTable(defectData);
                    }
                    else
                    {
                        AddAllRowTempSaveAfterDelete(defectData);
                    }

                    SearchDefectByLotId();

                    /*
                    foreach (DataRow childDetailRow in defectData.Rows)
                    {
                        //임시저장 테이블에 저장하려는 데이터 있는지 체크
                        var alreadyTempSaved = _tempSave.Rows.Cast<DataRow>()
                            .Where(r => r["RESOURCEID"].ToString().Equals(childDetailRow["RESOURCEID"].ToString()) &&
                           r["DEGREE"].ToString().Equals(childDetailRow["DEGREE"].ToString()) &&
                           r["DEFECTCODE"].ToString().Equals(childDetailRow["DEFECTCODE"].ToString()) &&
                           r["QCSEGMENTID"].ToString().Equals(childDetailRow["QCSEGMENTID"].ToString()))
                           .ToList();

                        if (alreadyTempSaved.Count == 0)
                        {//없을때
                            AddRowTempSaveTable(childDetailRow);
                        }
                        else
                        {//있을때

                            if (childDetailRow["ISDELETE"].Equals("Y"))
                            {//row 상태 delete
                                DeleteTempSaveTable(childDetailRow);
                            }
                            else
                            {
                                DataTable toUpdateData = alreadyTempSaved.CopyToDataTable();
                                DataRow toUpdateRow = toUpdateData.Rows[0];
                                //기존 있는 Row에 Data update
                                UpdateTempSaveTable(childDetailRow);
                            }
                        }
                    }*/
                }
            }
            else
            {//현재 그리드에 데이터 없을 때
                if (_tempSave != null && _tempSave.Rows.Count > 0)
                {//임시저장 데이터에 해당 LotID의 데이터있는지 확인
                    var tempCount = _tempSave.AsEnumerable().
                        Where(r => r["RESOURCEID"].ToString().Equals(CurrentDataRow["RESOURCEID"].ToString())
                        && r["DEGREE"].ToString().Equals(CurrentDataRow["DEGREE"].ToString()))
                        .ToList().Count;

                    //임시저장한 데이터 있을 때 -> 임시저장한 데이터 삭제
                    if (tempCount > 0)
                    {
                        DeleteTempSaveByLotId(CurrentDataRow);
                    }
                    else
                    {
                        MSGBox.Show(MessageBoxType.Information, "NoDataToTempSave");//임시 저장 할 데이터가 없습니다.
                    }
                }
                else
                {
                    MSGBox.Show(MessageBoxType.Information, "NoDataToTempSave");//임시 저장 할 데이터가 없습니다.
                } 
               
            }

        }

        /// <summary>
        /// 이미등록된 결과가 있을 때 셀 ReadOnly 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_ShowingEditor(object sender, CancelEventArgs e)
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
            if (CurrentDataRow == null) return;
            if((grdInspectionList.DataSource as DataTable).Rows.Count == 0) return;

            if (e.IsTotalSummary)
			{
				GridSummaryItem item = e.Item as GridSummaryItem;
				if (item.FieldName == "DEFECTRATE")
				{
					double inspectionQty = 0;
					double defectQty = 0;
					if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
					{
                        if (CurrentDataRow == null) return;
                        //inspectionQty = Convert.ToDouble((sender as GridView).Columns["INSPECTIONQTY"].SummaryItem.SummaryValue);
                        inspectionQty = CurrentDataRow["ALLQTYPCS"].ToSafeDoubleNaN();
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

            #region 불량수량
            if (e.Column.FieldName == "DEFECTQTY")
            {
                //음수를 입력했을 때 0으로 바꿔줌
                if (e.Value.ToSafeInt32() < 0)
                    grdInspectionList.View.SetFocusedRowCellValue(e.Column.FieldName, 0);

                if (CurrentDataRow["ALLQTYPCS"].ToSafeInt32() < e.Value.ToSafeInt32())
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

                    row["FILEINSPECTIONTYPE"] = "ShipmentInspection";
                    row["FILERESOURCEID"] = row["RESOURCEID"] + row["DEGREE"].ToString() + row["DEFECTCODE"].ToString() + row["QCSEGMENTID"].ToString() + "D";
                    row["PROCESSRELNO"] = "*";

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
                            row["FILECOMMENTS1"] = "InspectionResult/ShipmentInspection";
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
                            row["FILECOMMENTS2"] = "InspectionResult/ShipmentInspection";
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
            if (e.KeyCode == Keys.Delete)
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
            DataRow row = grdInspectionList.View.GetDataRow(e.FocusedRowHandle);

            SearchImage(row);
        }

        #endregion

        #region Public Function
        /// <summary>
        /// 그리드의 데이터를 클리어 하는 함수
        /// </summary>
        public void ClearUserControlGrd()
        {
            grdChildLot.View.ClearDatas();
            grdInspectionList.View.ClearDatas();

            picBox.Image = null;
            picBox2.Image = null;

            _tempSave = null;
            txtChildLotId.Editor.EditValue = string.Empty;

        }
        /// <summary>
        /// Get Grid Datasource
        /// </summary>
        /// <returns></returns>
        public DataTable GetGridDataSource()
        {
            if ((grdInspectionList.DataSource as DataTable).Rows.Count > 0 && (_tempSave == null || _tempSave.Rows.Count == 0))
            { 
                // 임시저장 한 불량 폐기처리 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveDefectDisposalData");
            }
            DataTable changedDetail = null;
            if (_tempSave == null)
            {
                changedDetail = (grdInspectionList.DataSource as DataTable).Copy();
            }
            else
            {
                changedDetail = _tempSave.Copy();
            }
            return changedDetail;
        }
  
        public void SetLotIdAndData(DataTable lotDt)
        {
            grdChildLot.DataSource = lotDt;
            CurrentDataRow = grdChildLot.View.GetFocusedDataRow();
            _selectedLotId = CurrentDataRow["RESOURCEID"].ToString();
            txtChildLotId.Editor.EditValue = _selectedLotId;
            //SearchImage(grdInspectionList.View.GetFocusedDataRow());
        }

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

        #endregion

        #region Private Function

        /// <summary>
        /// 임시저장 Dt에 모든Row를 Add하는 함수
        /// </summary>
        /// <param name="toSaveRow"></param>
        private void AddAllRowTempSaveTable(DataTable toSaveDt)
        {
            foreach (DataRow toSaveRow in toSaveDt.Rows)
            {
                if (!toSaveRow["ISDELETE"].ToString().Equals("Y"))
                {
                    DataRow addRow = _tempSave.NewRow();
                    addRow.ItemArray = toSaveRow.ItemArray.Clone() as object[];
                    addRow["DEFECTQTYSUM"] = _defectQtySum;
                    addRow["DEFECTQTYPNLSUM"] = _defectQtyPNLSum;
                    //addRow["_STATE_"] = "added";
                    _tempSave.Rows.Add(addRow);
                }
            }
        }

        /// <summary>
        /// 임시저장 Dt에 Row를 Add하는 함수
        /// </summary>
        /// <param name="toSaveRow"></param>
        private void AddRowTempSaveTable(DataRow toSaveRow)
        {
            DataRow addRow = _tempSave.NewRow();
            addRow.ItemArray = toSaveRow.ItemArray.Clone() as object[];
            addRow["DEFECTQTYSUM"] = _defectQtySum;
            addRow["DEFECTQTYPNLSUM"] = _defectQtyPNLSum;
            //addRow["_STATE_"] = "added";
            _tempSave.Rows.Add(addRow);
        }

        /// <summary>
        /// 임시저장 Dt에 Row를 Delete하는 함수
        /// </summary>
        /// <param name="toSaveRow"></param>
        private void DeleteTempSaveTable(DataRow sourceRow)
        {
            string expression = "RESOURCEID=" + "'" + sourceRow["RESOURCEID"].ToString() + "'" + " AND DEGREE =" + "'" + sourceRow["DEGREE"].ToString() + "'" +
                 " AND DEFECTCODE=" + "'" + sourceRow["DEFECTCODE"].ToString() + "'"
                 + " AND QCSEGMENTID=" + "'" + sourceRow["QCSEGMENTID"].ToString() + "'";
            DataRow[] rows = _tempSave.Select(expression);
            DataRow deleteRow = rows[0];

            _tempSave.Rows.Remove(deleteRow);

            _tempSave.AcceptChanges();
        }


        /// <summary>
        /// grd에 데이터 없을때 입시저장 버튼을 눌렀을때 (임시저장한 해당Lot의 데이터 삭제)
        /// </summary>
        /// <param name="sourceRow"></param>
        private void DeleteTempSaveByLotId(DataRow sourceRow)
        {
            string expression = "RESOURCEID=" + "'" + sourceRow["RESOURCEID"].ToString() + "'" + " AND DEGREE =" + "'" + sourceRow["DEGREE"].ToString() + "'";
            DataRow[] rows = _tempSave.Select(expression);

            foreach (DataRow deleteRow in rows)
            {      
                _tempSave.Rows.Remove(deleteRow);
            }

            _tempSave.AcceptChanges();

        }
        /// <summary>
        /// 임시저장 Dt에 Row를 Update하는 함수
        /// </summary>
        /// <param name="sourceRow"></param>
        private void UpdateTempSaveTable(DataRow sourceRow)
        {
            string expression = "RESOURCEID=" + "'" + sourceRow["RESOURCEID"].ToString() + "'" + " AND DEGREE =" + "'" + sourceRow["DEGREE"].ToString() + "'" +
                 " AND DEFECTCODE=" + "'" + sourceRow["DEFECTCODE"].ToString() + "'"
                 + " AND QCSEGMENTID=" + "'" + sourceRow["QCSEGMENTID"].ToString() + "'";
            DataRow[] rows = _tempSave.Select(expression);
            DataRow targetRow = rows[0];
            
            targetRow["DEFECTQTY"] = sourceRow["DEFECTQTY"];

            targetRow["FILENAME1"] = sourceRow["FILENAME1"];
            targetRow["FILEDATA1"] = sourceRow["FILEDATA1"];
            targetRow["FILECOMMENTS1"] = sourceRow["FILECOMMENTS1"];
            targetRow["FILESIZE1"] = sourceRow["FILESIZE1"];
            targetRow["FILEEXT1"] = sourceRow["FILEEXT1"];


            targetRow["FILENAME2"] = sourceRow["FILENAME2"];
            targetRow["FILEDATA2"] = sourceRow["FILEDATA2"];
            targetRow["FILECOMMENTS2"] = sourceRow["FILECOMMENTS2"];
            targetRow["FILESIZE2"] = sourceRow["FILESIZE2"];
            targetRow["FILEEXT2"] = sourceRow["FILEEXT2"];

            targetRow["DEFECTQTYSUM"] = _defectQtySum;
            targetRow["DEFECTQTYPNLSUM"] = _defectQtyPNLSum;

            _tempSave.AcceptChanges();
        }

        /// <summary>
        /// 해당 Lot, degree에 해당하는 불량 정보가 있을때 먼저 삭제후 임시저장 하는 함수
        /// </summary>
        /// <param name="toSaveDt"></param>
        private void AddAllRowTempSaveAfterDelete(DataTable toSaveDt)
        {
            string expression = "RESOURCEID=" + "'" + CurrentDataRow["RESOURCEID"].ToString() + "'" + " AND DEGREE =" + "'" + CurrentDataRow["DEGREE"].ToString() + "'";
            DataRow[] rows = _tempSave.Select(expression);

            foreach (DataRow toDeleteRow in rows)
            {
                _tempSave.Rows.Remove(toDeleteRow);
            }

            //모든 ROW 저장
            AddAllRowTempSaveTable(toSaveDt);

            _tempSave.AcceptChanges();

        }

        /// <summary>
        /// 선택된 LotId가 바뀔때 임시저장된 데이터로 재 바인딩 하는 함수
        /// </summary>
        private void SearchDefectByLotId()
        {
            //Focused 된 grdChildLot Data에 해당하는 detail 정보가 임시저장 테이블에 있는지 확인
            if (_tempSave != null)
            {
                var bindingData = _tempSave.Rows.Cast<DataRow>()
                    .Where(r => r["RESOURCEID"].ToString().Equals(CurrentDataRow["RESOURCEID"].ToString()) &&
                    r["DEGREE"].ToString().Equals(CurrentDataRow["DEGREE"].ToString())).ToList();

                if (bindingData.Count == 0)
                {//임시저장 dt에 데이터 없을때
                    //.DataSource = CreateDataSource();
                    grdInspectionList.View.ClearDatas();
                }
                else
                {//임시저장 dt에 데이터 있을때
                    //grdInspectionList.DataSource = CreateDataSource();
                    grdInspectionList.View.ClearDatas();
                    grdInspectionList.DataSource = bindingData.CopyToDataTable().Copy();
                    DataRow row = grdInspectionList.View.GetFocusedDataRow();
                    SearchImage(row);
                }
            }
            else
            {
                //grdInspectionList.DataSource = CreateDataSource();
                grdInspectionList.View.ClearDatas();
            }
        }

        /// <summary>
        /// row 바귈때 이미지 바인딩하는 함수
        /// </summary>
        /// <param name="row"></param>
        private void SearchImage(DataRow row)
        {
            if (row == null) return;

            picBox.Image = null;
            picBox2.Image = null;

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
        #endregion
    }
}
