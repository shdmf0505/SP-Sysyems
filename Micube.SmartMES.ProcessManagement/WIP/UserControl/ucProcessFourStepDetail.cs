#region using

using DevExpress.Data;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using DevExpress.Utils;

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid;
using Micube.Framework.SmartControls.Grid.BandedGrid;

using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
using Micube.Framework.SmartControls.Forms;


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정 4-Step 상세 정보 User Control
    /// 업  무  설  명  : 공정 4-Step 화면에서 사용되는 상세 정보를 보여주는 사용자 컨트롤을 생성한다.
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2019-07-02
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ucProcessFourStepDetail : UserControl
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        private DataTable _lotInfo;
        private string _lotId = "";
        private decimal _panelPerQty = 0;
        private decimal _panelQty = 0;
        private decimal _qty = 0;
        private DataTable _consumableDefectList;
        private string _queryVersion = "";

        private DataTable _dtDurableSet;

        private bool _isRepairProcess = false;
        private bool _isSendStep = false;

        private Dictionary<string, decimal> _consumableQty;

        #endregion

        #region Events
        public event EventHandler OnStartLoadingDefect = delegate { };
        public event EventHandler OnEndLoadingDefect = delegate { };
        #endregion

        #region 생성자

        public ucProcessFourStepDetail()
        {
            InitializeComponent();

            InitializeEvent();

            //InitializeTabPageVisible();

            if (!tabProcessFourStepDetail.IsDesignMode())
            {
                InitializeControls();
            }
        }

        #endregion

        #region 컨트롤 초기화

        /// <summary>
        /// 화면의 컨트롤을 초기화한다.
        /// </summary>
        private void InitializeControls()
        {
            InitializeEquipmentPage();
            InitializeCommentPage();
            InitializeProcessSpecPage();
            InitializeDefectPage();
            InitializeConsumableStartPage();
            InitializeConsumableCompletePage();
            InitializeDurableStartPage();
            InitializeDurableCompletePage();
            InitializeAOIDefectPage();
            InitializeBBTHOLEDefectPage();
            InitializeEquipmentUseStatusPage();
            InitializePostProcessEquipmentWipPage();
            InitializeEquipmentRecipePage();
            InitializeMessagePage();

            grdDefect.InitializeEvent();
        }

        private void InitializeEquipmentPage()
        {
            if (!tpgEquipment.PageVisible)
                return;

            ConditionItemSelectPopup equipmentCondition = new ConditionItemSelectPopup();
            equipmentCondition.Id = "EQUIPMENT";
            equipmentCondition.SearchQuery = new SqlQueryAdapter();
            equipmentCondition.ValueFieldName = "EQUIPMENTID";
            equipmentCondition.DisplayFieldName = "EQUIPMENTNAME";
            equipmentCondition.SetPopupLayout("SELECTEQUIPMENT", PopupButtonStyles.Ok_Cancel, true, false);
            equipmentCondition.SetPopupResultCount(1);
            equipmentCondition.SetPopupLayoutForm(400, 600, FormBorderStyle.SizableToolWindow);
            equipmentCondition.SetPopupAutoFillColumns("EQUIPMENTNAME");
            equipmentCondition.SetPopupApplySelection((selectedRows, dataGridRow) =>
            {
                if (selectedRows.Count() > 0)
                {
                    DataRow row = selectedRows.FirstOrDefault();

                    grdEquipment.View.CheckRow(grdEquipment.View.LocateByValue("EQUIPMENTID", row["EQUIPMENTID"]), true);

                    txtEquipment.Editor.SetValue(null);
                    txtEquipment.Editor.Text = "";
                }
            });

            equipmentCondition.Conditions.AddTextBox("EQUIPMENT");

            equipmentCondition.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150);
            equipmentCondition.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200);

            txtEquipment.Editor.SelectPopupCondition = equipmentCondition;


            grdEquipment.GridButtonItem = GridButtonItem.None;
            grdEquipment.ShowButtonBar = false;
            grdEquipment.ShowStatusBar = false;

            grdEquipment.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdEquipment.View.EnableRowStateStyle = false;
            grdEquipment.View.CheckMarkSelection.ShowCheckBoxHeader = false;
            grdEquipment.View.SetSortOrder("EQUIPMENTID");

            // 설비ID
            grdEquipment.View.AddTextBoxColumn("EQUIPMENTID", 150)
                .SetIsReadOnly();
            // 설비명
            grdEquipment.View.AddTextBoxColumn("EQUIPMENTNAME", 200)
                .SetIsReadOnly();
            // 작업 시작 일시
            var colTrackinTime = grdEquipment.View.AddTextBoxColumn("TRACKINTIME", 130)
                .SetIsReadOnly();
            // 작업 종료 일시
            var colTrackoutTime = grdEquipment.View.AddTextBoxColumn("TRACKOUTTIME", 130)
                .SetIsReadOnly();
            // PCS 수량
            grdEquipment.View.AddSpinEditColumn("PCSQTY", 80)
                .SetValidationIsRequired();
            // PLN 수량
            grdEquipment.View.AddSpinEditColumn("PNLQTY", 80)
                .SetValidationIsRequired();
            // Recipe
            //grdEquipment.View.AddComboBoxColumn("RECIPEID", new SqlQueryAdapter(), "RECIPENAME", "RECIPEID")
            //    .SetRelationIds("EQUIPMENTID");
            InitializeRecipeIdPopup();
            // Recipe Version
            grdEquipment.View.AddTextBoxColumn("RECIPEVERSION", 80)
                .SetIsReadOnly();
            // Recipe Type
            grdEquipment.View.AddComboBoxColumn("RECIPETYPE", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=RecipeType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();


            grdEquipment.View.PopulateColumns();


            grdEquipment.View.OptionsView.ShowIndicator = false;
        }

        private void InitializeRecipeIdPopup()
        {
            var recipeIdColumn = grdEquipment.View.AddSelectPopupColumn("RECIPEID", 120, new SqlQuery("GetEquipmentRecipeList", "10001"), "RECIPEID")
                .SetPopupLayout("SELECTRECIPE", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(600, 800)
                .SetPopupAutoFillColumns("RECIPETYPE")
                .SetPopupApplySelection((selectedRows, gridRow) =>
                {
                    DataRow row = selectedRows.FirstOrDefault();

                    if (row == null)
                    {
                        gridRow["RECIPEVERSION"] = string.Empty;
                        gridRow["RECIPETYPE"] = string.Empty;
                    }
                    else
                    {
                        gridRow["RECIPEVERSION"] = Format.GetString(row["RECIPEVERSION"]);
                        gridRow["RECIPETYPE"] = Format.GetString(row["RECIPETYPE"]);
                    }
                });

            recipeIdColumn.Conditions.AddTextBox("EQUIPMENTID")
                .SetPopupDefaultByGridColumnId("EQUIPMENTID")
                .SetIsReadOnly();

            recipeIdColumn.GridColumns.AddTextBoxColumn("EQUIPMENTID", 80).SetIsHidden();
            recipeIdColumn.GridColumns.AddTextBoxColumn("RECIPEID", 100);
            recipeIdColumn.GridColumns.AddTextBoxColumn("RECIPEVERSION", 80);
            recipeIdColumn.GridColumns.AddTextBoxColumn("RECIPENAME", 150);
            //recipeIdColumn.GridColumns.AddTextBoxColumn("RECIPETYPE", 100);
            recipeIdColumn.GridColumns.AddComboBoxColumn("RECIPETYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=RecipeType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
        }

        private void InitializeCommentPage()
        {
            if (!tpgComment.PageVisible)
                return;

            grdComment.GridButtonItem = GridButtonItem.None;
            grdComment.ShowButtonBar = false;
            grdComment.ShowStatusBar = false;

            grdComment.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdComment.View.SetIsReadOnly();

            // 공정수순ID
            grdComment.View.AddTextBoxColumn("PROCESSPATHID", 150)
                .SetValidationKeyColumn()
                .SetIsHidden();
            // 공정수순
            grdComment.View.AddTextBoxColumn("USERSEQUENCE", 80);
            // 공정ID
            grdComment.View.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetIsHidden();
            // 공정명
            grdComment.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            // 특기사항
            grdComment.View.AddTextBoxColumn("DESCRIPTION", 500)
                .SetLabel("REMARKS");
            // 현재공정여부
            grdComment.View.AddTextBoxColumn("ISCURRENTPROCESS", 70)
                .SetIsHidden();

            grdComment.View.PopulateColumns();


            grdComment.View.OptionsView.ShowIndicator = false;
            grdComment.View.OptionsView.AllowCellMerge = true;

            grdComment.View.Columns["PROCESSPATHID"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdComment.View.Columns["USERSEQUENCE"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdComment.View.Columns["PROCESSSEGMENTID"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdComment.View.Columns["PROCESSSEGMENTNAME"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdComment.View.Columns["DESCRIPTION"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdComment.View.Columns["ISCURRENTPROCESS"].OptionsColumn.AllowMerge = DefaultBoolean.False;
        }

        private void InitializeProcessSpecPage()
        {
            if (!tpgProcessSpec.PageVisible)
                return;

            grdProcessSpec.GridButtonItem = GridButtonItem.None;
            grdProcessSpec.ShowButtonBar = false;
            grdProcessSpec.ShowStatusBar = false;

            grdProcessSpec.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdProcessSpec.View.SetIsReadOnly();

            // 공정수순ID
            grdProcessSpec.View.AddTextBoxColumn("PROCESSPATHID", 150)
                .SetValidationKeyColumn()
                .SetIsHidden();
            // 공정수순
            grdProcessSpec.View.AddTextBoxColumn("USERSEQUENCE", 80);
            // 공정ID
            grdProcessSpec.View.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetIsHidden();
            // 공정명
            grdProcessSpec.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            // 항목
            grdProcessSpec.View.AddTextBoxColumn("SPECCLASSNAME", 120)
                .SetLabel("SPECITEM");
            // 하한값
            grdProcessSpec.View.AddSpinEditColumn("LSL", 90)
                .SetDisplayFormat("#,##0.00");
            // 중간값
            grdProcessSpec.View.AddSpinEditColumn("SL", 90)
                .SetDisplayFormat("#,##0.00");
            // 상한값
            grdProcessSpec.View.AddSpinEditColumn("USL", 90)
                .SetDisplayFormat("#,##0.00");
            // 현재공정여부
            grdProcessSpec.View.AddTextBoxColumn("ISCURRENTPROCESS", 70)
                .SetIsHidden();

            grdProcessSpec.View.PopulateColumns();


            grdProcessSpec.View.OptionsView.ShowIndicator = false;
            grdProcessSpec.View.OptionsView.AllowCellMerge = true;

            grdProcessSpec.View.Columns["SPECCLASSNAME"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdProcessSpec.View.Columns["LSL"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdProcessSpec.View.Columns["SL"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdProcessSpec.View.Columns["USL"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdProcessSpec.View.Columns["ISCURRENTPROCESS"].OptionsColumn.AllowMerge = DefaultBoolean.False;
        }

        private void InitializeDefectPage()
        {
            if (!tpgDefect.PageVisible)
                return;

            grdDefect.VisibleLotId = false;
            grdDefect.VisibleFileUpLoad = false;

            grdDefect.InitializeControls();

            /*
            //tabProcessFourStepDetail.
            grdDefect.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
            grdDefect.ShowStatusBar = false;

            grdDefect.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            // Lot No.
            grdDefect.View.AddTextBoxColumn("LOTID", 150)
                .SetIsReadOnly()
                .SetIsHidden();
            // 불량코드
            InitializeDefectCodePopup();
            // 불량명
            grdDefect.View.AddTextBoxColumn("DEFECTCODENAME", 200)
                .SetIsReadOnly();
            // 중공정ID
            //grdDefectList.View.AddComboBoxColumn("MIDDLESEGMENTCLASSID", 120, new SqlQuery("GetProcessSegmentClassByProcess", "10001", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass", $"PLANTID={UserInfo.Current.Plant}"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID");
            grdDefect.View.AddTextBoxColumn("QCSEGMENTID", 120)
                .SetIsReadOnly()
                .SetIsHidden();
            // 중공정명
            grdDefect.View.AddTextBoxColumn("QCSEGMENTNAME", 200)
                .SetIsReadOnly()
                .SetLabel("QCSEGMENT");
            // PNL
            grdDefect.View.AddSpinEditColumn("PNLQTY", 80)
                .SetDisplayFormat("#,##0.00")
                .SetValidationIsRequired();
            // 수량
            grdDefect.View.AddSpinEditColumn("QTY", 90)
                .SetValidationIsRequired();
            // 불량율
            grdDefect.View.AddSpinEditColumn("DEFECTRATE", 80)
                .SetDisplayFormat("#,##0.0000")
                .SetIsReadOnly()
                .SetIsHidden();
            // 원인품목
            grdDefect.View.AddComboBoxColumn("REASONCONSUMABLELOTID", 150, new SqlQuery("GetCauseMaterialList", "10001", $"PLANTID={UserInfo.Current.Plant}"), "REASONCONSUMABLELOTID", "REASONCONSUMABLELOTID")
                .SetRelationIds("LOTID")
                .SetLabel("REASONPRODUCT");
            // 원인공정
            grdDefect.View.AddComboBoxColumn("REASONSEGMENTID", 150, new SqlQuery("GetCauseProcessList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                .SetRelationIds("LOTID", "REASONCONSUMABLELOTID")
                .SetLabel("CAUSEPROCESS");
            InitializeGrid_CauseProductIdPopup();
            InitializeGrid_CauseSegmentIdPopup();
            // ReadOnly여부
            grdDefect.View.AddTextBoxColumn("ISREADONLY")
                .SetIsReadOnly()
                .SetIsHidden()
                .SetDefault("N");

            grdDefect.View.AddTextBoxColumn("HASROUTING").SetIsHidden().SetDefault("N");

            grdDefect.View.AddTextBoxColumn("PROCESSSEGMENTID").SetIsHidden();
            grdDefect.View.AddTextBoxColumn("PROCESSSEGMENTVERSION").SetIsHidden();

            grdDefect.View.AddTextBoxColumn("CONSUMABLEDEFID").SetIsHidden();
            grdDefect.View.AddTextBoxColumn("CONSUMABLEDEFVERSION").SetIsHidden();

            grdDefect.View.AddTextBoxColumn("REASONSEGMENTID").SetIsHidden();
            grdDefect.View.AddTextBoxColumn("REASONCONSUMABLELOTID").SetIsHidden();

            grdDefect.View.PopulateColumns();

            grdDefect.View.OptionsView.ShowFooter = true;
            grdDefect.View.OptionsView.ShowIndicator = false;

            grdDefect.View.Columns["DEFECTCODE"].SummaryItem.SummaryType = SummaryItemType.Custom;
            grdDefect.View.Columns["DEFECTCODE"].SummaryItem.DisplayFormat = Language.Get("SUM");
            grdDefect.View.Columns["PNLQTY"].SummaryItem.SummaryType = SummaryItemType.Sum;
            grdDefect.View.Columns["PNLQTY"].SummaryItem.DisplayFormat = "{0:n0}";
            grdDefect.View.Columns["QTY"].SummaryItem.SummaryType = SummaryItemType.Sum;
            grdDefect.View.Columns["QTY"].SummaryItem.DisplayFormat = "{0:n0}";
            grdDefect.View.Columns["DEFECTRATE"].SummaryItem.SummaryType = SummaryItemType.Sum;
            grdDefect.View.Columns["DEFECTRATE"].SummaryItem.DisplayFormat = "{0:n4}%";
            */
        }

        /// <summary>
        /// 원인 품목 팝업
        /// </summary>
        private void InitializeGrid_CauseProductIdPopup()
        {
            var causeProductIdColumn = grdDefect.View.AddSelectPopupColumn("CONSUMABLEDEFNAME", 200, new SqlQuery("GetCauseMaterialList", "10001"))
                .SetPopupLayout("SELECTREASONMATERIAL", PopupButtonStyles.Ok_Cancel, false, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(800, 800, FormBorderStyle.SizableToolWindow)
                .SetPopupAutoFillColumns("CONSUMABLEDEFNAME")
                .SetLabel("REASONPRODUCT")
                .SetPopupQueryPopup((DataRow currentRow) => {
                    currentRow["LOTID"] = _lotId;
                    return true;
                })
                .SetPopupApplySelection((selectedRows, gridRow) => {
                    DataRow row = selectedRows.FirstOrDefault();

                    if (row != null)
                    {
                        string hasRouting = Format.GetString(row["HASROUTING"]);    // 반제품 여부

                        gridRow["HASROUTING"] = Format.GetString(row["HASROUTING"]);
                        gridRow["CONSUMABLEDEFID"] = Format.GetString(row["CONSUMABLEDEFID"]);
                        gridRow["CONSUMABLEDEFVERSION"] = Format.GetString(row["CONSUMABLEDEFVERSION"]);
                        gridRow["REASONCONSUMABLELOTID"] = Format.GetString(row["CONSUMABLEDEFID"]);
                    }
                    else
                    {
                        gridRow["HASROUTING"] = string.Empty;
                        gridRow["CONSUMABLEDEFID"] = string.Empty;
                        gridRow["CONSUMABLEDEFVERSION"] = string.Empty;
                        gridRow["REASONCONSUMABLELOTID"] = string.Empty;
                    }
                });

            causeProductIdColumn.Conditions.AddTextBox("LOTID").SetPopupDefaultByGridColumnId("LOTID").SetIsHidden();
            causeProductIdColumn.Conditions.AddTextBox("PROCESSSEGMENTID").SetPopupDefaultByGridColumnId("PROCESSSEGMENTID").SetIsHidden();
            causeProductIdColumn.Conditions.AddTextBox("PROCESSSEGMENTVERSION").SetPopupDefaultByGridColumnId("PROCESSSEGMENTVERSION").SetIsHidden();

            causeProductIdColumn.GridColumns.AddTextBoxColumn("CONSUMABLEDEFID", 150);
            causeProductIdColumn.GridColumns.AddTextBoxColumn("CONSUMABLEDEFVERSION", 80);
            causeProductIdColumn.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 250);
            causeProductIdColumn.GridColumns.AddTextBoxColumn("LOTID", 10).SetIsHidden();
            causeProductIdColumn.GridColumns.AddTextBoxColumn("HASROUTING", 10).SetIsHidden();
            causeProductIdColumn.GridColumns.AddTextBoxColumn("PRODUCT", 10).SetIsHidden();
        }

        /// <summary>
        /// 원인 공정 팝업
        /// </summary>
        private void InitializeGrid_CauseSegmentIdPopup()
        {
            var causeSegmentIdColumn = grdDefect.View.AddSelectPopupColumn("PROCESSSEGMENTNAME", 150, new SqlQuery("GetCauseProcessList", "10003", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("SELECTREASONSEGMENT", PopupButtonStyles.Ok_Cancel, false, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(800, 800, FormBorderStyle.SizableToolWindow)
                .SetPopupAutoFillColumns("PROCESSSEGMENTNAME")
                .SetLabel("CAUSEPROCESS")
                .SetPopupApplySelection((selectedRows, gridRow) => {
                    DataRow row = selectedRows.FirstOrDefault();
                    if (row != null)
                    {
                        gridRow["PROCESSSEGMENTID"] = Format.GetString(row["PROCESSSEGMENTID"]);
                        gridRow["PROCESSSEGMENTVERSION"] = Format.GetString(row["PROCESSSEGMENTVERSION"]);
                        gridRow["REASONSEGMENTID"] = Format.GetString(row["PROCESSSEGMENTID"]);
                    }
                    else
                    {
                        gridRow["PROCESSSEGMENTID"] = string.Empty;
                        gridRow["PROCESSSEGMENTVERSION"] = string.Empty;
                        gridRow["REASONSEGMENTID"] = string.Empty;
                    }
                });

            //조회조건
            causeSegmentIdColumn.Conditions.AddTextBox("LOTID").SetPopupDefaultByGridColumnId("LOTID").SetIsHidden();
            causeSegmentIdColumn.Conditions.AddTextBox("HASROUTING").SetPopupDefaultByGridColumnId("HASROUTING").SetIsHidden();
            causeSegmentIdColumn.Conditions.AddTextBox("CONSUMABLEDEFID").SetPopupDefaultByGridColumnId("CONSUMABLEDEFID").SetIsHidden();
            causeSegmentIdColumn.Conditions.AddTextBox("CONSUMABLEDEFVERSION").SetPopupDefaultByGridColumnId("CONSUMABLEDEFVERSION").SetIsHidden();

            causeSegmentIdColumn.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            causeSegmentIdColumn.GridColumns.AddTextBoxColumn("PROCESSSEGMENTVERSION", 80);
            causeSegmentIdColumn.GridColumns.AddTextBoxColumn("USERSEQUENCE", 80);
            causeSegmentIdColumn.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            causeSegmentIdColumn.GridColumns.AddTextBoxColumn("PROCESSDEFID", 10).SetIsHidden();
            causeSegmentIdColumn.GridColumns.AddTextBoxColumn("PROCESSDEFVERSION", 10).SetIsHidden();
        }

        private void InitializeDefectCodePopup()
        {
            var defectCodePopupColumn = grdDefect.View.AddSelectPopupColumn("DEFECTCODE", 120, new SqlQuery("GetDefectCodeByProcess", "10002", "RESOURCETYPE=QCSegmentID", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "DEFECTCODE")
                .SetPopupLayout("SELECTDEFECTCODE", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("QCSEGMENTNAME")
                .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                {
                    if (selectedRow.Count() > 0)
                    {
                        grdDefect.View.SetFocusedRowCellValue("DEFECTCODENAME", selectedRow.FirstOrDefault()["DEFECTCODENAME"]);
                        grdDefect.View.SetFocusedRowCellValue("QCSEGMENTID", selectedRow.FirstOrDefault()["QCSEGMENTID"]);
                        grdDefect.View.SetFocusedRowCellValue("QCSEGMENTNAME", selectedRow.FirstOrDefault()["QCSEGMENTNAME"]);
                    }
                    else
                    {
                        grdDefect.View.SetFocusedRowCellValue("DEFECTCODENAME", "");
                        grdDefect.View.SetFocusedRowCellValue("QCSEGMENTID", "");
                        grdDefect.View.SetFocusedRowCellValue("QCSEGMENTNAME", "");
                    }
                });

            // 팝업의 검색조건 항목 추가 (불량코드/명)
            defectCodePopupColumn.Conditions.AddTextBox("TXTDEFECTCODENAME");

            // 팝업의 그리드에 컬럼 추가
            // 불량코드
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("DEFECTCODE", 80);
            // 불량코드명
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("DEFECTCODENAME", 200);
            // 중공정ID
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("QCSEGMENTID", 80);
            // 중공정명
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("QCSEGMENTNAME", 200);
        }

        private void InitializeConsumableStartPage()
        {
            if (!tpgConsumableStart.PageVisible)
                return;

            grdConsumableStart.GridButtonItem = GridButtonItem.Delete;
            grdConsumableStart.ShowStatusBar = false;

            grdConsumableStart.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdConsumableStart.View.SetIsReadOnly();

            // Area Id
            grdConsumableStart.View.AddTextBoxColumn("AREAID", 80)
                .SetIsHidden();
            // Warehouse Id
            grdConsumableStart.View.AddTextBoxColumn("WAREHOUSEID", 80)
                .SetIsHidden();
            // Key
            grdConsumableStart.View.AddTextBoxColumn("KEYCOLUMN", 100)
                .SetIsHidden();
            // 품목코드
            grdConsumableStart.View.AddTextBoxColumn("CONSUMABLEDEFID", 150);
            // 품목버전
            grdConsumableStart.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 80)
                .SetIsHidden();
            // 품목명
            grdConsumableStart.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
            // 자재 LOT ID
            grdConsumableStart.View.AddTextBoxColumn("CONSUMABLELOTID", 150);
            // 재고수량
            grdConsumableStart.View.AddSpinEditColumn("STOCKQTY", 100)
                .SetDisplayFormat("#,##0.#########");
            // 가용수량
            grdConsumableStart.View.AddSpinEditColumn("AVAILABLEQTY", 100)
                .SetDisplayFormat("#,##0.#########");
            // 투입수량
            grdConsumableStart.View.AddSpinEditColumn("INPUTQTY", 100)
                .SetDisplayFormat("#,##0.#########");
            // 실투입량
            grdConsumableStart.View.AddSpinEditColumn("ORGINPUTQTY", 100)
                .SetDisplayFormat("#,##0.#########")
                .SetIsHidden();
            // 불량수량
            grdConsumableStart.View.AddSpinEditColumn("DEFECTQTY", 100)
                .SetDisplayFormat("#,##0.#########")
                .SetIsHidden();
            // Lot사용수량
            grdConsumableStart.View.AddSpinEditColumn("LOTUSINGQTY", 100)
                .SetDisplayFormat("#,##0.#########");
            // 자재불량
            grdConsumableStart.View.AddButtonColumn("CONSUMABLEDEFECT", 100)
                .SetIsHidden();
            // 상태
            grdConsumableStart.View.AddTextBoxColumn("CONSUMABLESTATE", 80)
                .SetIsHidden();

            grdConsumableStart.View.PopulateColumns();


            grdConsumableStart.View.OptionsView.ShowIndicator = false;


            InitializeStandardRequirementGrid(grdStandardRequirementStart);
        }

        private void InitializeConsumableCompletePage()
        {
            if (!tpgConsumableComplete.PageVisible)
                return;

            grdConsumableComplete.GridButtonItem = GridButtonItem.Delete;
            grdConsumableComplete.ShowStatusBar = false;

            grdConsumableComplete.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdConsumableComplete.View.EnableRowStateStyle = false;

            // Area Id
            grdConsumableComplete.View.AddTextBoxColumn("AREAID", 80)
                .SetIsReadOnly()
                .SetIsHidden();
            // Warehouse Id
            grdConsumableStart.View.AddTextBoxColumn("WAREHOUSEID", 80)
                .SetIsReadOnly()
                .SetIsHidden();
            // Key
            grdConsumableComplete.View.AddTextBoxColumn("KEYCOLUMN", 100)
                .SetIsReadOnly()
                .SetIsHidden();
            // 품목코드
            grdConsumableComplete.View.AddTextBoxColumn("CONSUMABLEDEFID", 150)
                .SetIsReadOnly();
            // 품목버전
            grdConsumableComplete.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 80)
                .SetIsReadOnly()
                .SetIsHidden();
            // 품목명
            grdConsumableComplete.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 200)
                .SetIsReadOnly();
            // 자재 LOT ID
            grdConsumableComplete.View.AddTextBoxColumn("CONSUMABLELOTID", 150)
                .SetIsReadOnly();
            // 재고수량
            grdConsumableComplete.View.AddSpinEditColumn("STOCKQTY", 100)
                .SetDisplayFormat("#,##0.#########")
                .SetIsReadOnly();
            // 가용수량
            grdConsumableComplete.View.AddSpinEditColumn("AVAILABLEQTY", 100)
                .SetDisplayFormat("#,##0.#########")
                .SetIsReadOnly();
            // 투입수량
            grdConsumableComplete.View.AddSpinEditColumn("INPUTQTY", 100)
                .SetDisplayFormat("#,##0.#########")
                .SetValidationIsRequired();
            // 실투입량
            grdConsumableComplete.View.AddSpinEditColumn("ORGINPUTQTY", 100)
                .SetDisplayFormat("#,##0.#########")
                .SetIsReadOnly()
                .SetIsHidden();
            // 불량수량
            grdConsumableComplete.View.AddSpinEditColumn("DEFECTQTY", 100)
                .SetDisplayFormat("#,##0.#########")
                .SetIsReadOnly();
            // Lot사용수량
            grdConsumableComplete.View.AddSpinEditColumn("LOTUSINGQTY", 100)
                .SetDisplayFormat("#,##0.#########")
                .SetIsReadOnly();
            // 자재불량
            grdConsumableComplete.View.AddButtonColumn("CONSUMABLEDEFECT", 100);
            // 상태
            grdConsumableComplete.View.AddTextBoxColumn("CONSUMABLESTATE", 80)
                .SetIsReadOnly()
                .SetIsHidden();

            grdConsumableComplete.View.PopulateColumns();


            grdConsumableComplete.View.OptionsView.ShowIndicator = false;
            grdConsumableComplete.View.OptionsBehavior.EditorShowMode = EditorShowMode.MouseDown;


            InitializeStandardRequirementGrid(grdStandardRequirementComplete);

            _consumableDefectList = new DataTable();
            _consumableDefectList.Columns.Add("CONSUMABLELOTID", typeof(string));
            _consumableDefectList.Columns.Add("DEFECTQTY", typeof(decimal));
            _consumableDefectList.Columns.Add("REASONCODE", typeof(string));
        }

        private void InitializeDurableStartPage()
        {
            if (!tpgDurableStart.PageVisible)
                return;

            grdDurableStart.GridButtonItem = GridButtonItem.None;
            grdDurableStart.ShowStatusBar = false;

            grdDurableStart.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdDurableStart.View.EnableRowStateStyle = false;
            grdDurableStart.View.SetSortOrder("DURABLEDEFID");
            grdDurableStart.View.SetSortOrder("DURABLELOTID");

            // 품목코드
            grdDurableStart.View.AddTextBoxColumn("DURABLEDEFID", 150)
                .SetIsReadOnly();
            // 품목버전
            grdDurableStart.View.AddTextBoxColumn("DURABLEDEFVERSION", 80)
                .SetIsReadOnly()
                .SetIsHidden();
            // 품목명
            grdDurableStart.View.AddTextBoxColumn("DURABLEDEFNAME", 200)
                .SetIsReadOnly();
            // 치공구 LOT ID
            grdDurableStart.View.AddTextBoxColumn("DURABLELOTID", 150)
                .SetIsReadOnly();
            // 사용타수
            grdDurableStart.View.AddSpinEditColumn("USEDCOUNT", 90)
                .SetIsReadOnly();
            // 누적타수
            grdDurableStart.View.AddSpinEditColumn("TOTALUSEDCOUNT", 90)
                .SetIsReadOnly();
            // 보증타수
            grdDurableStart.View.AddSpinEditColumn("USEDLIMIT", 90)
                .SetIsReadOnly();
            // 연마기준타수
            grdDurableStart.View.AddSpinEditColumn("CLEANLIMIT", 90)
                .SetIsReadOnly();
            // 사용횟수
            grdDurableStart.View.AddSpinEditColumn("USINGQTY", 90)
                .SetIsReadOnly();
            // 설비
            DataTable equipmentList = new DataTable();
            equipmentList.Columns.Add("EQUIPMENTID", typeof(string));
            equipmentList.Columns.Add("EQUIPMENTNAME", typeof(string));
            grdDurableStart.View.AddComboBoxColumn("EQUIPMENTID", 150, new SqlQueryAdapter(equipmentList), "EQUIPMENTNAME", "EQUIPMENTID")
                .SetLabel("EQUIPMENT")
                .SetValidationIsRequired();

            grdDurableStart.View.PopulateColumns();


            grdDurableStart.View.OptionsView.ShowIndicator = false;
            grdDurableStart.View.OptionsView.AllowCellMerge = true;

            grdDurableStart.View.Columns["DURABLELOTID"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdDurableStart.View.Columns["USEDCOUNT"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdDurableStart.View.Columns["TOTALUSEDCOUNT"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdDurableStart.View.Columns["USEDLIMIT"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdDurableStart.View.Columns["CLEANLIMIT"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdDurableStart.View.Columns["USINGQTY"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdDurableStart.View.Columns["EQUIPMENTID"].OptionsColumn.AllowMerge = DefaultBoolean.False;


            grdBORDurableStart.GridButtonItem = GridButtonItem.None;
            grdBORDurableStart.ShowStatusBar = false;
            grdBORDurableStart.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdBORDurableStart.View.SetIsReadOnly();

            // 치공구 ID
            grdBORDurableStart.View.AddTextBoxColumn("DURABLEDEFID", 150);
            // 치공구 버전
            grdBORDurableStart.View.AddTextBoxColumn("DURABLEDEFVERSION", 80)
                .SetIsHidden();
            // 치공구 명
            grdBORDurableStart.View.AddTextBoxColumn("DURABLEDEFNAME", 300);
            // 치공구 그룹 ID
            grdBORDurableStart.View.AddTextBoxColumn("DURABLECLASSID", 100)
                .SetIsHidden();
            // 치공구 유형
            grdBORDurableStart.View.AddTextBoxColumn("DURABLECLASSNAME", 100)
                .SetLabel("DURABLETYPE");
            // 보증타수
            grdBORDurableStart.View.AddSpinEditColumn("USEDLIMIT", 90);
            // 연마기준타수
            grdBORDurableStart.View.AddSpinEditColumn("CLEANLIMIT", 90);

            grdBORDurableStart.View.PopulateColumns();


            grdBORDurableStart.View.OptionsView.ShowIndicator = false;
        }

        private void InitializeDurableCompletePage()
        {
            if (!tpgDurableComplete.PageVisible)
                return;

            grdDurableComplete.GridButtonItem = GridButtonItem.None;
            grdDurableComplete.ShowStatusBar = false;

            grdDurableComplete.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdDurableComplete.View.EnableRowStateStyle = false;

            // Transaction History Key
            grdDurableComplete.View.AddTextBoxColumn("TXNHISTKEY", 100)
                .SetIsReadOnly()
                .SetIsHidden();
            // 품목코드
            grdDurableComplete.View.AddTextBoxColumn("DURABLEDEFID", 150)
                .SetIsReadOnly();
            // 품목버전
            grdDurableComplete.View.AddTextBoxColumn("DURABLEDEFVERSION", 80)
                .SetIsReadOnly()
                .SetIsHidden();
            // 품목명
            grdDurableComplete.View.AddTextBoxColumn("DURABLEDEFNAME", 200)
                .SetIsReadOnly();
            // 치공구 LOT ID
            grdDurableComplete.View.AddTextBoxColumn("DURABLELOTID", 150)
                .SetIsReadOnly();
            // 작업시작시간
            grdDurableComplete.View.AddTextBoxColumn("WORKSTARTTIME", 130)
                .SetIsReadOnly();
            // 작업완료시간
            grdDurableComplete.View.AddTextBoxColumn("WORKENDTIME", 130)
                .SetIsReadOnly();
            // 사용타수
            grdDurableComplete.View.AddSpinEditColumn("USEDCOUNT", 90)
                .SetIsReadOnly();
            // 누적타수
            grdDurableComplete.View.AddSpinEditColumn("TOTALUSEDCOUNT", 90)
                .SetIsReadOnly();
            // 보증타수
            grdDurableComplete.View.AddSpinEditColumn("USEDLIMIT", 90)
                .SetIsReadOnly();
            // 연마기준타수
            grdDurableComplete.View.AddSpinEditColumn("CLEANLIMIT", 90)
                .SetIsReadOnly();
            // 사용횟수
            grdDurableComplete.View.AddSpinEditColumn("USINGQTY", 90)
                .SetValidationIsRequired();
            // 설비
            DataTable equipmentList = new DataTable();
            equipmentList.Columns.Add("EQUIPMENTID", typeof(string));
            equipmentList.Columns.Add("EQUIPMENTNAME", typeof(string));
            grdDurableComplete.View.AddComboBoxColumn("EQUIPMENTID", 150, new SqlQueryAdapter(equipmentList), "EQUIPMENTNAME", "EQUIPMENTID")
                .SetLabel("EQUIPMENT")
                .SetIsReadOnly();
            // 마지막 치공구 여부
            grdDurableComplete.View.AddTextBoxColumn("ISLASTDURABLE", 70)
                .SetIsReadOnly()
                .SetIsHidden();

            grdDurableComplete.View.PopulateColumns();


            grdDurableComplete.View.OptionsView.ShowIndicator = false;


            grdBORDurableComplete.GridButtonItem = GridButtonItem.None;
            grdBORDurableComplete.ShowStatusBar = false;
            grdBORDurableComplete.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdBORDurableComplete.View.SetIsReadOnly();

            // 치공구 ID
            grdBORDurableComplete.View.AddTextBoxColumn("DURABLEDEFID", 150);
            // 치공구 버전
            grdBORDurableComplete.View.AddTextBoxColumn("DURABLEDEFVERSION", 80)
                .SetIsHidden();
            // 치공구 명
            grdBORDurableComplete.View.AddTextBoxColumn("DURABLEDEFNAME", 300);
            // 치공구 그룹 ID
            grdBORDurableComplete.View.AddTextBoxColumn("DURABLECLASSID", 100)
                .SetIsHidden();
            // 치공구 유형
            grdBORDurableComplete.View.AddTextBoxColumn("DURABLECLASSNAME", 100)
                .SetLabel("DURABLETYPE");
            // 보증타수
            grdBORDurableComplete.View.AddSpinEditColumn("USEDLIMIT", 90);
            // 연마기준타수
            grdBORDurableComplete.View.AddSpinEditColumn("CLEANLIMIT", 90);

            grdBORDurableComplete.View.PopulateColumns();


            grdBORDurableComplete.View.OptionsView.ShowIndicator = false;
        }

        private void InitializeAOIDefectPage()
        {
            if (!tpgAOIDefect.PageVisible)
                return;

            grpAOIDefect.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;

            // 불량 그리드
            grdAOIDefect.GridButtonItem = GridButtonItem.None;
            grdAOIDefect.ShowButtonBar = false;
            grdAOIDefect.ShowStatusBar = false;

            grdAOIDefect.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdAOIDefect.View.EnableRowStateStyle = false;
            grdAOIDefect.View.SetSortOrder("SEQUENCE");
            //grdAOIDefect.View.SetSortOrder("DEFECTCODEGROUPID");
            //grdAOIDefect.View.SetSortOrder("DEFECTCODE");

            // 컬럼 초기화
            grdAOIDefect.View.ClearColumns();

            // 불량코드그룹ID
            grdAOIDefect.View.AddComboBoxColumn("DEFECTCODEGROUPID", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DefectMapDefectGroup", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODEID", "CODEID")
                .SetValidationIsRequired()
                .SetMultiColumns(ComboBoxColumnShowType.All);
            // 불량그룹
            grdAOIDefect.View.AddTextBoxColumn("DEFECTCODEGROUPNAME", 100)
                .SetIsReadOnly()
                .SetLabel("DEFECTGROUP");
            // 불량코드
            grdAOIDefect.View.AddComboBoxColumn("DEFECTCODE", 90, new SqlQuery("GetAoiDefectCodeByDefectCodeGroup", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODEID", "CODEID")
                .SetValidationIsRequired()
                .SetMultiColumns(ComboBoxColumnShowType.All)
                .SetRelationIds("DEFECTCODEGROUPID");
            // 불량항목
            grdAOIDefect.View.AddTextBoxColumn("DEFECTCODENAME", 120)
                .SetLabel("DEFECTITEM")
                .SetIsReadOnly();
            // Layer
            grdAOIDefect.View.AddComboBoxColumn("LAYER", 100, new SqlQueryAdapter(), "LAYERNAME", "LAYERID")
                .SetValidationIsRequired();
            // 수량(PCS)
            grdAOIDefect.View.AddSpinEditColumn("PCSQTY", 90)
                .SetLabel("QCMFINALINSPECTSPECOUT")
                .SetValidationIsRequired();
            // 분석(PCS)
            grdAOIDefect.View.AddSpinEditColumn("ANALYSISQTY", 90)
                .SetLabel("ANALYSISTARGET");
            // 분석(PNL)
            grdAOIDefect.View.AddSpinEditColumn("REPAIRTARGETPNLQTY", 90)
                .SetLabel("ANALYSISTARGETPNL");
            // 분석양품
            grdAOIDefect.View.AddSpinEditColumn("ANALYSISGOODQTY", 90);
            // 분석양품(PNL)
            grdAOIDefect.View.AddSpinEditColumn("ANALYSISGOODPNLQTY", 100);
            // 최종불량
            grdAOIDefect.View.AddSpinEditColumn("FINALDEFECTQTY", 90)
                .SetIsReadOnly();
            // 수정 가능 여부
            grdAOIDefect.View.AddTextBoxColumn("ISCHANGE", 90)
                .SetIsReadOnly()
                .SetIsHidden();
            // 표시순서
            grdAOIDefect.View.AddSpinEditColumn("SEQUENCE", 70)
                .SetIsReadOnly()
                .SetIsHidden();


            grdAOIDefect.View.PopulateColumns();

            grdAOIDefect.View.OptionsView.ShowIndicator = false;
            grdAOIDefect.View.OptionsView.ShowFooter = true;


            grdAOIDefect.View.Columns["DEFECTCODEGROUPID"].SummaryItem.SummaryType = SummaryItemType.Custom;
            grdAOIDefect.View.Columns["DEFECTCODEGROUPID"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
            grdAOIDefect.View.Columns["PCSQTY"].SummaryItem.SummaryType = SummaryItemType.Sum;
            grdAOIDefect.View.Columns["PCSQTY"].SummaryItem.DisplayFormat = "{0:n0}";
            grdAOIDefect.View.Columns["ANALYSISQTY"].SummaryItem.SummaryType = SummaryItemType.Sum;
            grdAOIDefect.View.Columns["ANALYSISQTY"].SummaryItem.DisplayFormat = "{0:n0}";
            grdAOIDefect.View.Columns["REPAIRTARGETPNLQTY"].SummaryItem.SummaryType = SummaryItemType.Sum;
            grdAOIDefect.View.Columns["REPAIRTARGETPNLQTY"].SummaryItem.DisplayFormat = "{0:n0}";
            grdAOIDefect.View.Columns["ANALYSISGOODQTY"].SummaryItem.SummaryType = SummaryItemType.Sum;
            grdAOIDefect.View.Columns["ANALYSISGOODQTY"].SummaryItem.DisplayFormat = "{0:n0}";
            grdAOIDefect.View.Columns["ANALYSISGOODPNLQTY"].SummaryItem.SummaryType = SummaryItemType.Sum;
            grdAOIDefect.View.Columns["ANALYSISGOODPNLQTY"].SummaryItem.DisplayFormat = "{0:n0}";
            grdAOIDefect.View.Columns["FINALDEFECTQTY"].SummaryItem.SummaryType = SummaryItemType.Sum;
            grdAOIDefect.View.Columns["FINALDEFECTQTY"].SummaryItem.DisplayFormat = "{0:n0}";


            grpAOIDefectInfo.GridButtonItem = GridButtonItem.None;

            // 설비 Defect Data 그리드
            grdAOIDefectInfo.GridButtonItem = GridButtonItem.None;
            grdAOIDefectInfo.ShowButtonBar = false;
            grdAOIDefectInfo.ShowStatusBar = false;

            grdAOIDefectInfo.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdAOIDefectInfo.View.SetIsReadOnly();

            // 컬럼 초기화
            grdAOIDefectInfo.View.ClearColumns();

            // 불량코드그룹ID
            grdAOIDefectInfo.View.AddTextBoxColumn("DEFECTCODEGROUPID", 80)
                .SetIsHidden();
            // 불량그룹
            grdAOIDefectInfo.View.AddTextBoxColumn("DEFECTCODEGROUPNAME", 100)
                .SetLabel("DEFECTGROUP");
            // 불량코드
            grdAOIDefectInfo.View.AddTextBoxColumn("DEFECTCODE", 90);
            // 불량항목
            grdAOIDefectInfo.View.AddTextBoxColumn("DEFECTCODENAME", 120)
                .SetLabel("DEFECTITEM");
            // Layer
            grdAOIDefectInfo.View.AddTextBoxColumn("LAYERID", 100)
                .SetLabel("LAYER");
            // 검사수
            grdAOIDefectInfo.View.AddSpinEditColumn("INSPECTQTY", 90)
                .SetIsHidden();
            // 불량수량
            grdAOIDefectInfo.View.AddSpinEditColumn("DEFECTQTY", 90);
            // 불량률
            grdAOIDefectInfo.View.AddSpinEditColumn("DEFECTRATE", 90)
                .SetDisplayFormat("#,##0.#####");


            grdAOIDefectInfo.View.PopulateColumns();

            grdAOIDefectInfo.View.OptionsView.ShowIndicator = false;
            grdAOIDefectInfo.View.OptionsView.ShowFooter = true;


            grdAOIDefectInfo.View.Columns["DEFECTCODEGROUPNAME"].SummaryItem.SummaryType = SummaryItemType.Custom;
            grdAOIDefectInfo.View.Columns["DEFECTCODEGROUPNAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
            grdAOIDefectInfo.View.Columns["DEFECTQTY"].SummaryItem.SummaryType = SummaryItemType.Sum;
            grdAOIDefectInfo.View.Columns["DEFECTQTY"].SummaryItem.DisplayFormat = "{0:n0}";
            grdAOIDefectInfo.View.Columns["DEFECTRATE"].SummaryItem.SummaryType = SummaryItemType.Sum;
            grdAOIDefectInfo.View.Columns["DEFECTRATE"].SummaryItem.DisplayFormat = "{0:n5}";
        }

        private void InitializeBBTHOLEDefectPage()
        {
            if (!tpgBBTHOLEDefect.PageVisible)
                return;

            grpBBTHOLEDefect.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;

            // 불량 그리드
            grdBBTHOLEDefect.GridButtonItem = GridButtonItem.None;
            grdBBTHOLEDefect.ShowButtonBar = false;
            grdBBTHOLEDefect.ShowStatusBar = false;

            grdBBTHOLEDefect.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdBBTHOLEDefect.View.EnableRowStateStyle = false;

            // 불량코드그룹ID
            grdBBTHOLEDefect.View.AddTextBoxColumn("DEFECTCODEGROUPID", 80)
                .SetIsReadOnly()
                .SetIsHidden();
            // 불량그룹
            grdBBTHOLEDefect.View.AddTextBoxColumn("DEFECTCODEGROUPNAME", 100)
                .SetLabel("DEFECTGROUP")
                .SetIsReadOnly()
                .SetIsHidden();
            // 불량코드
            grdBBTHOLEDefect.View.AddComboBoxColumn("DEFECTCODE", 90, new SqlQueryAdapter())
                .SetValidationIsRequired()
                .SetMultiColumns(ComboBoxColumnShowType.All);
            // 불량항목
            grdBBTHOLEDefect.View.AddTextBoxColumn("DEFECTCODENAME", 120)
                .SetLabel("DEFECTITEM")
                .SetIsReadOnly();
            // Layer
            grdBBTHOLEDefect.View.AddTextBoxColumn("LAYER", 100)
                .SetIsReadOnly()
                .SetIsHidden();
            // 수량(PCS)
            grdBBTHOLEDefect.View.AddSpinEditColumn("PCSQTY", 90)
                .SetValidationIsRequired();

            grdBBTHOLEDefect.View.PopulateColumns();

            grdBBTHOLEDefect.View.OptionsView.ShowIndicator = false;
            grdBBTHOLEDefect.View.OptionsView.ShowFooter = true;


            grdBBTHOLEDefect.View.Columns["DEFECTCODE"].SummaryItem.SummaryType = SummaryItemType.Custom;
            grdBBTHOLEDefect.View.Columns["DEFECTCODE"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
            grdBBTHOLEDefect.View.Columns["PCSQTY"].SummaryItem.SummaryType = SummaryItemType.Sum;
            grdBBTHOLEDefect.View.Columns["PCSQTY"].SummaryItem.DisplayFormat = "{0:n0}";


            grpBBTHOLEDefectInfo.GridButtonItem = GridButtonItem.None;

            // 설비 Defect Data 그리드
            grdBBTHOLEDefectInfo.GridButtonItem = GridButtonItem.None;
            grdBBTHOLEDefectInfo.ShowButtonBar = false;
            grdBBTHOLEDefectInfo.ShowStatusBar = false;

            grdBBTHOLEDefectInfo.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdBBTHOLEDefectInfo.View.SetIsReadOnly();

            // 불량그룹코드
            grdBBTHOLEDefectInfo.View.AddTextBoxColumn("DEFECTCODEGROUPID", 80)
                .SetIsHidden();
            // 불량그룹명
            grdBBTHOLEDefectInfo.View.AddTextBoxColumn("DEFECTCODEGROUPNAME", 100)
                .SetLabel("DEFECTGROUP")
                .SetIsHidden();
            // 불량코드
            grdBBTHOLEDefectInfo.View.AddTextBoxColumn("DEFECTCODE", 90);
            // 불량항목
            grdBBTHOLEDefectInfo.View.AddTextBoxColumn("DEFECTCODENAME", 120)
                .SetLabel("DEFECTITEM");
            // 검사수
            grdBBTHOLEDefectInfo.View.AddSpinEditColumn("INSPECTQTY", 90);
            // 불량수량
            grdBBTHOLEDefectInfo.View.AddSpinEditColumn("DEFECTQTY", 90);
            // 불량률
            grdBBTHOLEDefectInfo.View.AddSpinEditColumn("DEFECTRATE", 90)
                .SetDisplayFormat("#,##0.#####");


            grdBBTHOLEDefectInfo.View.PopulateColumns();


            grdBBTHOLEDefectInfo.View.OptionsView.ShowIndicator = false;
            grdBBTHOLEDefectInfo.View.OptionsView.ShowFooter = true;

            grdBBTHOLEDefectInfo.View.Columns["DEFECTCODE"].SummaryItem.SummaryType = SummaryItemType.Custom;
            grdBBTHOLEDefectInfo.View.Columns["DEFECTCODE"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
            grdBBTHOLEDefectInfo.View.Columns["DEFECTQTY"].SummaryItem.SummaryType = SummaryItemType.Sum;
            grdBBTHOLEDefectInfo.View.Columns["DEFECTQTY"].SummaryItem.DisplayFormat = "{0:n0}";
        }

        private void InitializeEquipmentUseStatusPage()
        {
            if (!tpgEquipmentUseStatus.PageVisible)
                return;

            grdEquipmentUseStatus.GridButtonItem = GridButtonItem.None;
            grdEquipmentUseStatus.ShowButtonBar = false;
            grdEquipmentUseStatus.ShowStatusBar = false;

            grdEquipmentUseStatus.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdEquipmentUseStatus.View.SetIsReadOnly();

            // 설비코드
            grdEquipmentUseStatus.View.AddTextBoxColumn("EQUIPMENTID", 150);
            // 설비명
            grdEquipmentUseStatus.View.AddTextBoxColumn("EQUIPMENTNAME", 200);
            // Lot 수
            grdEquipmentUseStatus.View.AddSpinEditColumn("LOTCNT", 80);
            // 설비 상태
            grdEquipmentUseStatus.View.AddComboBoxColumn("STATE", 100, new SqlQuery("GetState", "10001", "STATEMODELID=EquipmentState"), "STATENAME", "STATEID")
                .SetLabel("EQUIPMENTSTATE");

            grdEquipmentUseStatus.View.PopulateColumns();
        }

        private void InitializePostProcessEquipmentWipPage()
        {
            //if (!tpgPostProcessEquipmentWip.PageVisible)
            //    return;

            grdPostProcessEquipmentWip.GridButtonItem = GridButtonItem.None;
            grdPostProcessEquipmentWip.ShowButtonBar = false;
            grdPostProcessEquipmentWip.ShowStatusBar = false;

            grdPostProcessEquipmentWip.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdPostProcessEquipmentWip.View.SetIsReadOnly();

            // 작업장 ID
            grdPostProcessEquipmentWip.View.AddTextBoxColumn("AREAID", 150)
                .SetIsHidden();
            // 작업장명
            grdPostProcessEquipmentWip.View.AddTextBoxColumn("AREANAME", 200)
                .SetLabel("AREA");
            // 설비 코드
            grdPostProcessEquipmentWip.View.AddTextBoxColumn("EQUIPMENTID", 150);
            // 설비명
            grdPostProcessEquipmentWip.View.AddTextBoxColumn("EQUIPMENTNAME", 200);
            // Lot 수
            grdPostProcessEquipmentWip.View.AddSpinEditColumn("LOTCNT", 80);
            // 설비 상태
            grdPostProcessEquipmentWip.View.AddComboBoxColumn("STATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=EquipmentState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("EQUIPMENTSTATE");

            grdPostProcessEquipmentWip.View.PopulateColumns();
        }

        private void InitializeEquipmentRecipePage()
        {
            if (!tpgEquipmentRecipe.PageVisible)
                return;

            grdEquipmentRecipe.GridButtonItem = GridButtonItem.None;
            grdEquipmentRecipe.ShowButtonBar = false;
            grdEquipmentRecipe.ShowStatusBar = false;

            grdEquipmentRecipe.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            //grdEquipmentRecipe.View.SetIsReadOnly();

            // 설비 코드
            grdEquipmentRecipe.View.AddTextBoxColumn("EQUIPMENTID", 150)
                .SetIsReadOnly();
            // 설비명
            grdEquipmentRecipe.View.AddTextBoxColumn("EQUIPMENTNAME", 200)
                .SetIsReadOnly();
            // Recipe Id
            grdEquipmentRecipe.View.AddTextBoxColumn("RECIPEID", 120)
                .SetIsReadOnly();
            // Recipe Version
            grdEquipmentRecipe.View.AddTextBoxColumn("RECIPEVERSION", 70)
                .SetIsReadOnly();
            // Recipe Name
            grdEquipmentRecipe.View.AddTextBoxColumn("RECIPENAME", 150)
                .SetIsReadOnly();
            // Parameter Id
            grdEquipmentRecipe.View.AddTextBoxColumn("PARAMETERID", 120)
                .SetIsReadOnly();
            // Parameter Name
            grdEquipmentRecipe.View.AddTextBoxColumn("PARAMETERNAME", 150)
                .SetIsReadOnly();
            // 하한값
            grdEquipmentRecipe.View.AddSpinEditColumn("LSL", 80)
                .SetIsReadOnly();
            // 목표값
            grdEquipmentRecipe.View.AddSpinEditColumn("TARGET", 80)
                .SetIsReadOnly();
            // 상한값
            grdEquipmentRecipe.View.AddSpinEditColumn("USL", 80)
                .SetIsReadOnly();
            // Validation Type
            grdEquipmentRecipe.View.AddTextBoxColumn("VALIDATIONTYPE", 120)
                .SetIsReadOnly();
            // Data Type
            grdEquipmentRecipe.View.AddComboBoxColumn("DATATYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=RecipeParameterDataType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            // Value
            grdEquipmentRecipe.View.AddTextBoxColumn("VALUE", 100);


            grdEquipmentRecipe.View.PopulateColumns();


            grdEquipmentRecipe.View.OptionsView.ShowIndicator = false;
        }

        private void InitializeMessagePage()
        {
            if (!tpgMessage.PageVisible)
                return;

            grdMessage.GridButtonItem = GridButtonItem.None;
            grdMessage.ShowButtonBar = false;
            grdMessage.ShowStatusBar = false;

            grdMessage.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdMessage.View.SetIsReadOnly();

            grdMessage.View.AddTextBoxColumn("LOTID", 180).SetIsHidden();
            grdMessage.View.AddTextBoxColumn("TXNHISTKEY", 180).SetIsHidden();
            grdMessage.View.AddTextBoxColumn("PATHSEQUENCE", 70).SetIsHidden();
            grdMessage.View.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center);
            grdMessage.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            grdMessage.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            grdMessage.View.AddTextBoxColumn("WRITEPROCESSSEGMENT");//입력공정
            grdMessage.View.AddTextBoxColumn("AREANAME", 150);
            grdMessage.View.AddTextBoxColumn("MESSAGETYPE", 140).SetTextAlignment(TextAlignment.Center);
            grdMessage.View.AddTextBoxColumn("WRITER", 60).SetTextAlignment(TextAlignment.Center);
            grdMessage.View.AddTextBoxColumn("WRITEDATE", 140).SetTextAlignment(TextAlignment.Center);

            grdMessage.View.PopulateColumns();

            grdMessage.View.OptionsView.ShowIndicator = false;

            txtTitle.ReadOnly = true;
            rtbMessage.ReadOnly = true;
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        private void InitializeEvent()
        {
            txtEquipmentId.Editor.KeyDown += TxtEquipmentId_KeyDown;
            grdEquipment.View.CheckStateChanged += EquipmentView_CheckStateChanged;
            grdEquipment.View.CellValueChanged += EquipmentView_CellValueChanged;
            grdEquipment.View.RowStyle += EquipmentView_RowStyle;


            grdComment.View.RowStyle += CommentView_RowStyle;
            grdProcessSpec.View.RowStyle += ProcessSpecView_RowStyle;

            grdDefect.Grid.ToolbarDeleteRow += GrdDefect_ToolbarDeleteRow;
            grdDefect.View.AddingNewRow += DefectView_AddingNewRow;
            grdDefect.DefectQtyChanged += GrdDefect_DefectQtyChanged;
            //grdDefect.View.CellValueChanged += DefectView_CellValueChanged;

            txtConsumableLotIdStart.Editor.KeyDown += TxtConsumableLotIdStart_KeyDown;
            txtConsumableLotIdStart.Editor.PreviewKeyDown += TxtConsumableLotIdStart_PreviewKeyDown;
            grdConsumableStart.ToolbarDeleteRow += GrdConsumableStart_ToolbarDeleteRow;

            txtConsumableLotIdComplete.Editor.KeyDown += TxtConsumableLotIdComplete_KeyDown;
            txtConsumableLotIdComplete.Editor.PreviewKeyDown += TxtConsumableLotIdComplete_PreviewKeyDown;
            grdConsumableComplete.ToolbarDeleteRow += GrdConsumableComplete_ToolbarDeleteRow;
            grdConsumableComplete.View.CellValueChanged += ConsumableCompleteView_CellValueChanged;
            grdConsumableComplete.View.GridCellButtonClickEvent += ConsumableCompleteView_GridCellButtonClickEvent;

            txtDurableLotIdStart.Editor.KeyDown += TxtDurableLotIdStart_KeyDown;
            txtDurableLotIdStart.Editor.PreviewKeyDown += TxtDurableLotIdStart_PreviewKeyDown;
            grdDurableStart.ToolbarDeleteRow += GrdDurableStart_ToolbarDeleteRow;
            grdDurableStart.View.CheckStateChanged += DurableStartView_CheckStateChanged;

            //txtDurableLotIdComplete.Editor.KeyDown += TxtDurableLotIdComplete_KeyDown;
            //txtDurableLotIdComplete.Editor.PreviewKeyDown += TxtDurableLotIdComplete_PreviewKeyDown;
            grdDurableComplete.ToolbarDeleteRow += GrdDurableComplete_ToolbarDeleteRow;

            grpAOIDefect.HeaderButtonClickEvent += GrpAOIDefect_HeaderButtonClickEvent;
            grdAOIDefect.View.AddingNewRow += AOIDefectView_AddingNewRow;
            grdAOIDefect.View.ShowingEditor += AOIDefetView_ShowingEditor;
            grdAOIDefect.View.CellValueChanged += AOIDefectView_CellValueChanged;
            grdAOIDefect.View.CustomDrawFooterCell += AOIDefectView_CustomDrawFooterCell;
            grdAOIDefectInfo.View.CustomDrawFooterCell += AOIDefectInfoView_CustomDrawFooterCell;

            grpBBTHOLEDefect.HeaderButtonClickEvent += GrpBBTHOLEDefect_HeaderButtonClickEvent;
            grdBBTHOLEDefect.View.CellValueChanged += BBTHOLEDefectView_CellValueChanged;
            grdBBTHOLEDefect.View.CustomDrawFooterCell += BBTHOLEDefectView_CustomDrawFooterCell;
            grdBBTHOLEDefectInfo.View.CustomDrawFooterCell += BBTHOLEDefectInfoView_CustomDrawFooterCell;

            btnAOIDefectMapLink.Click += BtnDefectMapLink_Click;
            btnBBTHOLEDefectMapLink.Click += BtnDefectMapLink_Click;

            grdMessage.View.FocusedRowChanged += MessageView_FocusedRowChanged;

            grdDefect.OnStartLoadingComboboxData += new EventHandler(delegate(object s, EventArgs e) {
                    this.OnStartLoadingDefect(s, e);
                });
            grdDefect.OnEndLoadingComboboxData += new EventHandler(delegate(object s, EventArgs e) {
                    this.OnEndLoadingDefect(s, e);
                });
        }

        private void TxtEquipmentId_KeyDown(object sender, KeyEventArgs e)
        {
            if (ProcessType != ProcessType.StartWork)
                return;

            if (e.KeyCode == Keys.Enter)
            {
                string equipmentId = txtEquipmentId.Text;

                int rowHandle = grdEquipment.View.LocateByValue("EQUIPMENTID", equipmentId);

                if (rowHandle > -1)
                    grdEquipment.View.CheckRow(rowHandle, true);


                txtEquipmentId.Text = "";
            }
        }

        private void EquipmentView_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            string isEquipmentid = grdEquipment.View.GetRowCellValue(e.RowHandle, "EQUIPMENTID").ToString();

            DataTable dt = grdEquipmentRecipe.DataSource as DataTable;

            if (dt == null) return;

            int cnt = dt.AsEnumerable().Where(c => Format.GetTrimString(c["EQUIPMENTID"]).Equals(isEquipmentid)).Count();

            if (cnt > 0)
            {
                e.Appearance.BackColor = Color.Green;
                e.HighPriority = true;
            }
        }

        private void EquipmentView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "PCSQTY")
            {
                grdEquipment.View.CellValueChanged -= EquipmentView_CellValueChanged;

                decimal panelPerQty = Format.GetDecimal(_lotInfo.Rows[0]["PANELPERQTY"]);
                grdEquipment.View.SetFocusedRowCellValue("PNLQTY", Math.Ceiling(Format.GetDecimal(e.Value) / panelPerQty));

                grdEquipment.View.CellValueChanged += EquipmentView_CellValueChanged;
            }
            else if (e.Column.FieldName == "PNLQTY")
            {
                grdEquipment.View.CellValueChanged -= EquipmentView_CellValueChanged;

                int panelPerQty = Format.GetInteger(_lotInfo.Rows[0]["PANELPERQTY"]);
                grdEquipment.View.SetFocusedRowCellValue("PCSQTY", Format.GetInteger(e.Value) * panelPerQty);

                grdEquipment.View.CellValueChanged += EquipmentView_CellValueChanged;
            }
        }

        private void EquipmentView_CheckStateChanged(object sender, EventArgs e)
        {
            //if (ProcessType == ProcessType.WorkCompletion)
            //{
            //    grdEquipment.View.CheckStateChanged -= EquipmentView_CheckStateChanged;

            //    int rowHandle = grdEquipment.View.FocusedRowHandle;
            //    bool checkState = grdEquipment.View.CheckMarkSelection.IsRowSelected(rowHandle);

            //    grdEquipment.View.CheckRow(rowHandle, !checkState);

            //    grdEquipment.View.CheckStateChanged += EquipmentView_CheckStateChanged;


            //    return;
            //}

            if (ProcessType != ProcessType.StartWork)
                return;

            DataTable dataSource = grdEquipment.View.GetCheckedRows();

            SetDurableLotEquipmentDataSource(dataSource);
        }

        private void CommentView_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            string isCurrentProcess = grdComment.View.GetRowCellValue(e.RowHandle, "ISCURRENTPROCESS").ToString();

            if (isCurrentProcess == "Y")
            {
                e.Appearance.BackColor = Color.FromArgb(254, 254, 16);
                e.HighPriority = true;
            }
        }

        private void ProcessSpecView_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            string isCurrentProcess = grdProcessSpec.View.GetRowCellValue(e.RowHandle, "ISCURRENTPROCESS").ToString();

            if (isCurrentProcess == "Y")
            {
                e.Appearance.BackColor = Color.FromArgb(254, 254, 16);
                e.HighPriority = true;
            }
        }

        private void GrdDefect_ToolbarDeleteRow(object sender, EventArgs e)
        {
            //grdDefect.View.CellValueChanged -= DefectView_CellValueChanged;

            DefectQtyChanged?.Invoke(null, null);

            //grdDefect.View.CellValueChanged += DefectView_CellValueChanged;
        }

        private void DefectView_AddingNewRow(SmartBandedGridView sender, AddNewRowArgs args)
        {
            DataRow row = _lotInfo.AsEnumerable().FirstOrDefault();
            
            if (row == null)
                return;

            //공정 uom별로계산 로직 변경
            //grdDefect.SetInfo(Format.GetInteger(row["PANELPERQTY"].ToString()), Format.GetInteger(row["PCSQTY"].ToString()));
            string processUOM = Format.GetTrimString(row["PROCESSUOM"]);
            decimal pcsArry = Format.GetDecimal(row["PCSARY"].ToString());
            decimal pcsPnl = Format.GetDecimal(row["PCSPNL"].ToString());

            if (pcsArry.Equals(0))
            {
               // grdDefect.View.RemoveRow(grdDefect.View.RowCount-1);
                
                MSGBox.Show(MessageBoxType.Information,Language.GetMessage("NotInputPNLBKL").Message);
                args.IsCancel = true;
                return;
            }

            if (pcsPnl.Equals(0))
            {
                MSGBox.Show(MessageBoxType.Information, Language.GetMessage("NotInputPCSPNL").Message);
                args.IsCancel = true;
                return;
            }

            decimal CalQty = processUOM == "BLK" ? pcsPnl/ pcsArry : Format.GetInteger(row["PANELPERQTY"].ToString());

            grdDefect.SetInfo(CalQty, Format.GetInteger(row["PCSQTY"].ToString()));
            //grdDefect.SetInfo(Format.GetInteger(row["PANELPERQTY"].ToString()), Format.GetInteger(row["PCSQTY"].ToString()));
            grdDefect.View.SetFocusedRowCellValue("LOTID", _lotId);

            //grdDefect.SetConsumableDefComboBox();
        }

        private void GrdDefect_DefectQtyChanged(object sender, CellValueChangedEventArgs e)
        {
            DefectQtyChanged?.Invoke(sender, e);
        }

        private void DefectView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "PNLQTY")
            {
                grdDefect.View.CellValueChanged -= DefectView_CellValueChanged;

                decimal pnlQty = Format.GetDecimal(e.Value);

                decimal qty = _panelPerQty * pnlQty;

                grdDefect.View.SetRowCellValue(e.RowHandle, "QTY", qty);
                grdDefect.View.SetRowCellValue(e.RowHandle, "DEFECTRATE", (qty / _qty) * 100);

                DefectQtyChanged?.Invoke(sender, e);

                grdDefect.View.CellValueChanged += DefectView_CellValueChanged;
            }

            if (e.Column.FieldName == "QTY")
            {
                grdDefect.View.CellValueChanged -= DefectView_CellValueChanged;

                decimal qty = Format.GetDecimal(e.Value);

                decimal pnlQty = 0;
                if (_panelPerQty > 0)
                    pnlQty = qty / _panelPerQty;

                grdDefect.View.SetRowCellValue(e.RowHandle, "PNLQTY", pnlQty);
                grdDefect.View.SetRowCellValue(e.RowHandle, "DEFECTRATE", (qty / _qty) * 100);

                DefectQtyChanged?.Invoke(sender, e);

                grdDefect.View.CellValueChanged += DefectView_CellValueChanged;
            }
        }

        private void TxtConsumableLotIdStart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!CheckAlreadyInputLot(txtConsumableLotIdStart.Text, "CONSUMABLELOTID", grdConsumableStart))
                {
                    txtConsumableLotIdStart.Text = "";
                    return;
                }

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                param.Add("PLANTID", UserInfo.Current.Plant);
                param.Add("LOTID", _lotId);
                param.Add("CONSUMABLELOTID", txtConsumableLotIdStart.Text);

                txtConsumableLotIdStart.Text = "";


                DataTable consumableList = new DataTable();

                if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                {
                    if (Format.GetString(_lotInfo.AsEnumerable().FirstOrDefault()["DESCRIPTION"]) == "MIG")
                        consumableList = SqlExecuter.Query("GetConsumableLotByProcess_MIG", _queryVersion, param);
                    else
                        consumableList = SqlExecuter.Query("GetConsumableLotByProcess_YP", _queryVersion, param);
                }
                else
                    consumableList = SqlExecuter.Query("GetConsumableLotByProcess", _queryVersion, param);

                if (consumableList.Rows.Count > 0)
                {
                    DataRow consumableRow = consumableList.Rows.Cast<DataRow>().FirstOrDefault();
                    DataRow lotRow = _lotInfo.Rows.Cast<DataRow>().FirstOrDefault();

                    string currentAreaId = Format.GetString(lotRow["AREAID"]);
                    string consumableAreaId = Format.GetString(consumableRow["AREAID"]);

                    string currentWarehouseId = Format.GetString(lotRow["WAREHOUSEID"]);
                    string consumableWarehouseId = Format.GetString(consumableRow["WAREHOUSEID"]);

                    string consumableState = Format.GetString(consumableRow["CONSUMABLESTATE"]);

                    if (currentWarehouseId != consumableWarehouseId)
                    {
                        // 현재 창고에서 보유하고 있는 자재가 아닙니다. 현재 창고 ID : {0}, 소유 창고 ID : {1}
                        throw MessageException.Create("ConsumableNotInWarehouse", currentWarehouseId, consumableWarehouseId);
                    }

                    /*
                    if (currentAreaId != consumableAreaId)
                    {
                        // 현재 작업장에서 보유하고 있는 자재가 아닙니다. 현재 작업장 ID : {0}, 소유 작업장 ID : {1}
                        throw MessageException.Create("ConsumableNotInArea", currentAreaId, consumableAreaId);
                    }
                    */

                    if (consumableState == "Consumed")
                    {
                        // 해당 자재 Lot은 사용이 완료 된 Lot 입니다. Lot Id : {0}
                        throw MessageException.Create("ConsumedConsumableLot", _lotId);
                    }

                    if (consumableState == "Scrapped")
                    {
                        // 해당 자재 Lot은 폐기 처리 된 Lot 입니다. Lot Id : {0}
                        throw MessageException.Create("ScrappedConsumableLot", _lotId);
                    }

                    //decimal orgQty = Format.GetDecimal(consumableRow["ORGQTY"]);
                    //decimal requirementQty = Format.GetDecimal(consumableRow["QTY"]);

                    //if (orgQty < requirementQty)
                    //{
                    //    // 해당 자재 Lot의 재고 수량이 BOM 소요량 보다 적습니다. 자재 Lot Id : {0}
                    //    throw MessageException.Create("StockQtyLessThanRequirementQty", Format.GetString(consumableRow["CONSUMABLELOTID"]));
                    //}

                    //DataTable checkList = SqlExecuter.Query("GetInputMaterialLot", "10002", param);

                    //if (checkList.Rows.Count > 0)
                    //{
                    //    DataRow row = checkList.Rows.Cast<DataRow>().FirstOrDefault();



                    // 해당 자재 Lot은 다른 Lot에 투입 되었습니다. Lot Id : {0}
                    //throw MessageException.Create("ConsumableLotAlreadyInput", checkList.Rows.Cast<DataRow>().FirstOrDefault()["LOTID"].ToString());
                    //}

                    decimal inputQty = Format.GetDecimal(consumableRow["INPUTQTY"]);

                    if (inputQty <= 0)
                    {
                        decimal stockQty = Format.GetDecimal(consumableRow["STOCKQTY"]);
                        decimal lotUsingQty = Format.GetDecimal(consumableRow["LOTUSINGQTY"]);

                        // 해당 자재 Lot은 다른 Lot에서 모두 사용되었습니다. 재고수량 : {0}, Lot 사용수량 : {1}
                        throw MessageException.Create("ConsumableLotIsAlreadyUsingInLot", stockQty.ToString("#,##0.#####"), lotUsingQty.ToString("#,##0.#####"));
                    }


                    string consumableLotId = Format.GetString(consumableRow["CONSUMABLELOTID"]);
                    string consumableDefId = Format.GetString(consumableRow["CONSUMABLEDEFID"]);
                    string consumableDefVersion = Format.GetString(consumableRow["CONSUMABLEDEFVERSION"]);


                    DataTable dataSource = grdConsumableStart.DataSource as DataTable;

                    var query = from consumable in dataSource.Rows.Cast<DataRow>()
                                group consumable by consumable["KEYCOLUMN"].ToString() into g
                                select new
                                {
                                    ConsumableId = g.Key,
                                    InputQty = g.Sum(qty => Format.GetDecimal(qty["INPUTQTY"]))
                                };

                    string key = Format.GetString(consumableRow["KEYCOLUMN"]);

                    Dictionary<string, decimal> inputQtyList = query.ToDictionary(value => value.ConsumableId, value => value.InputQty);

                    inputQtyList.ForEach(value =>
                    {
                        if (key == value.Key)
                        {
                            decimal reqQty = Format.GetDecimal(grdStandardRequirementStart.View.GetRowCellValue(grdStandardRequirementStart.View.LocateByValue("KEYCOLUMN", value.Key), "REQUIREMENTQTY"));

                            if (reqQty - value.Value <= 0)
                            {
                                // {0} 자재는 이미 BOM 소요량만큼 투입되었습니다.
                                throw MessageException.Create("ConsumableIsAlreadyInputEqualRequirement", value.Key.Split('|')[0]);
                            }

                            if (Format.GetDecimal(consumableRow["INPUTQTY"]) > reqQty - value.Value)
                                consumableRow["INPUTQTY"] = Format.GetDecimal(reqQty - value.Value);

                            if (Format.GetDecimal(consumableRow["ORGINPUTQTY"]) > reqQty - value.Value)
                                consumableRow["ORGINPUTQTY"] = Format.GetDecimal(reqQty - value.Value);
                        }
                    });


                    Dictionary<string, object> checkParam = new Dictionary<string, object>();
                    checkParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    checkParam.Add("PLANTID", UserInfo.Current.Plant);
                    checkParam.Add("AREAID", Format.GetString(lotRow["AREAID"]));
                    checkParam.Add("CONSUMABLELOTID", consumableLotId);
                    checkParam.Add("CONSUMABLEDEFID", consumableDefId);
                    checkParam.Add("CONSUMABLEDEFVERSION", consumableDefVersion);

                    DataTable priorityConsumableList = SqlExecuter.Query("GetPriorityConsumableLotByProcess", "10001", checkParam);

                    if (priorityConsumableList.Rows.Count > 0)
                    {
                        // 해당 자재 보다 먼저 입고된 자재 Lot이 있습니다. 그래도 진행 하겠습니까?
                        if (MSGBox.Show(MessageBoxType.Question, "ExistsFirstStockLot", MessageBoxButtons.YesNo) == DialogResult.No)
                            return;
                    }


                    consumableList.Rows.Cast<DataRow>().ForEach(row =>
                    {
                        DataRow newRow = dataSource.NewRow();
                        newRow.ItemArray = row.ItemArray.Clone() as object[];

                        dataSource.Rows.Add(newRow);
                    });

                    dataSource.AcceptChanges();


                    return;
                }


                DataTable alternativeList = SqlExecuter.Query("GetConsumableAlternativeLotByProcess", _queryVersion, param);

                if (alternativeList.Rows.Count > 0)
                {
                    DataRow consumableRow = alternativeList.Rows.Cast<DataRow>().FirstOrDefault();

                    DataRow lotRow = _lotInfo.Rows.Cast<DataRow>().FirstOrDefault();

                    string currentAreaId = Format.GetString(lotRow["AREAID"]);
                    string consumableAreaId = Format.GetString(consumableRow["AREAID"]);

                    string currentWarehouseId = Format.GetString(lotRow["WAREHOUSEID"]);
                    string consumableWarehouseId = Format.GetString(consumableRow["WAREHOUSEID"]);

                    if (currentWarehouseId != consumableWarehouseId)
                    {
                        // 현재 창고에서 보유하고 있는 자재가 아닙니다. 현재 창고 ID : {0}, 소유 창고 ID : {1}
                        throw MessageException.Create("ConsumableNotInWarehouse", currentWarehouseId, consumableWarehouseId);
                    }

                    /*
                    if (currentAreaId != consumableAreaId)
                    {
                        // 현재 작업장에서 보유하고 있는 자재가 아닙니다. 현재 작업장 ID : {0}, 소유 작업장 ID : {1}
                        throw MessageException.Create("ConsumableNotInArea", currentAreaId, consumableAreaId);
                    }
                    */

                    DataTable dataSource = grdConsumableStart.DataSource as DataTable;

                    var query = from consumable in dataSource.Rows.Cast<DataRow>()
                                group consumable by consumable["KEYCOLUMN"].ToString() into g
                                select new
                                {
                                    ConsumableId = g.Key,
                                    InputQty = g.Sum(qty => Format.GetDecimal(qty["INPUTQTY"]))
                                };

                    string key = Format.GetString(consumableRow["KEYCOLUMN"]);

                    Dictionary<string, decimal> inputQtyList = query.ToDictionary(value => value.ConsumableId, value => value.InputQty);

                    inputQtyList.ForEach(value =>
                    {
                        if (key == value.Key)
                        {
                            decimal reqQty = Format.GetDecimal(grdStandardRequirementStart.View.GetRowCellValue(grdStandardRequirementStart.View.LocateByValue("KEYCOLUMN", value.Key), "REQUIREMENTQTY"));

                            if (reqQty - value.Value <= 0)
                            {
                                // {0} 자재는 이미 BOM 소요량만큼 투입되었습니다.
                                throw MessageException.Create("ConsumableIsAlreadyInputEqualRequirement", value.Key.Split('|')[0]);
                            }

                            if (Format.GetDecimal(consumableRow["INPUTQTY"]) > reqQty - value.Value)
                                consumableRow["INPUTQTY"] = reqQty - value.Value;
                        }
                    });

                    alternativeList.Rows.Cast<DataRow>().ForEach(row =>
                    {
                        DataRow newRow = dataSource.NewRow();
                        newRow.ItemArray = row.ItemArray.Clone() as object[];

                        dataSource.Rows.Add(newRow);
                    });

                    dataSource.AcceptChanges();


                    return;
                }

                // 해당 자재 Lot의 품목은 BOM에 등록 되지 않은 자재 입니다.
                MSGBox.Show(MessageBoxType.Information, "NotExistsConsumableInBOM");
            }
        }

        private void TxtConsumableLotIdStart_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                KeyEventArgs args = new KeyEventArgs(Keys.Enter);
                TxtConsumableLotIdStart_KeyDown(sender, args);
            }
        }

        private void GrdConsumableStart_ToolbarDeleteRow(object sender, EventArgs e)
        {
            if (grdConsumableStart.View.FocusedRowHandle < 0)
                return;

            //grdConsumableStart.View.DeleteRow(grdConsumableStart.View.FocusedRowHandle);
            (grdConsumableStart.View.DataSource as DataView).Delete(grdConsumableStart.View.FocusedRowHandle);
            (grdConsumableStart.View.DataSource as DataView).Table.AcceptChanges();
        }

        private void TxtConsumableLotIdComplete_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!CheckAlreadyInputLot(txtConsumableLotIdComplete.Text, "CONSUMABLELOTID", grdConsumableComplete))
                {
                    txtConsumableLotIdComplete.Text = "";
                    return;
                }

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                param.Add("PLANTID", UserInfo.Current.Plant);
                param.Add("LOTID", _lotId);
                param.Add("CONSUMABLELOTID", txtConsumableLotIdComplete.Text);

                txtConsumableLotIdComplete.Text = "";


                DataTable consumableList = new DataTable();

                if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                {
                    if (Format.GetString(_lotInfo.AsEnumerable().FirstOrDefault()["DESCRIPTION"]) == "MIG")
                        consumableList = SqlExecuter.Query("GetConsumableLotByProcess_MIG", _queryVersion, param);
                    else
                        consumableList = SqlExecuter.Query("GetConsumableLotByProcess_YP", _queryVersion, param);
                }
                else
                    consumableList = SqlExecuter.Query("GetConsumableLotByProcess", _queryVersion, param);

                if (consumableList.Rows.Count > 0)
                {
                    DataRow consumableRow = consumableList.Rows.Cast<DataRow>().FirstOrDefault();
                    DataRow lotRow = _lotInfo.Rows.Cast<DataRow>().FirstOrDefault();

                    string lotType = Format.GetString(lotRow["LOTTYPE"]);

                    string currentAreaId = Format.GetString(lotRow["AREAID"]);
                    string consumableAreaId = Format.GetString(consumableRow["AREAID"]);

                    string currentWarehouseId = Format.GetString(lotRow["WAREHOUSEID"]);
                    string consumableWarehouseId = Format.GetString(consumableRow["WAREHOUSEID"]);

                    string consumableState = Format.GetString(consumableRow["CONSUMABLESTATE"]);

                    if (currentWarehouseId != consumableWarehouseId)
                    {
                        // 현재 창고에서 보유하고 있는 자재가 아닙니다. 현재 창고 ID : {0}, 소유 창고 ID : {1}
                        throw MessageException.Create("ConsumableNotInWarehouse", currentWarehouseId, consumableWarehouseId);
                    }

                    /*
                    if (lotType != "Sample" && currentAreaId != consumableAreaId)
                    {
                        // 현재 작업장에서 보유하고 있는 자재가 아닙니다. 현재 작업장 ID : {0}, 소유 작업장 ID : {1}
                        throw MessageException.Create("ConsumableNotInArea", currentAreaId, consumableAreaId);
                    }
                    */

                    if (consumableState == "Consumed")
                    {
                        // 해당 자재 Lot은 사용이 완료 된 Lot 입니다. Lot Id : {0}
                        throw MessageException.Create("ConsumedConsumableLot", _lotId);
                    }

                    if (consumableState == "Scrapped")
                    {
                        // 해당 자재 Lot은 폐기 처리 된 Lot 입니다. Lot Id : {0}
                        throw MessageException.Create("ScrappedConsumableLot", _lotId);
                    }

                    decimal inputQty = Format.GetDecimal(consumableRow["INPUTQTY"]);

                    if (inputQty <= 0)
                    {
                        decimal stockQty = Format.GetDecimal(consumableRow["STOCKQTY"]);
                        decimal lotUsingQty = Format.GetDecimal(consumableRow["LOTUSINGQTY"]);

                        // 해당 자재 Lot은 다른 Lot에서 모두 사용되었습니다. 재고수량 : {0}, Lot 사용수량 : {1}
                        throw MessageException.Create("ConsumableLotIsAlreadyUsingInLot", stockQty.ToString("#,##0.#####"), lotUsingQty.ToString("#,##0.#####"));
                    }

                    string consumableLotId = Format.GetString(consumableRow["CONSUMABLELOTID"]);
                    string consumableDefId = Format.GetString(consumableRow["CONSUMABLEDEFID"]);
                    string consumableDefVersion = Format.GetString(consumableRow["CONSUMABLEDEFVERSION"]);

                    Dictionary<string, object> checkParam = new Dictionary<string, object>();
                    checkParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    checkParam.Add("PLANTID", UserInfo.Current.Plant);
                    checkParam.Add("AREAID", Format.GetString(lotRow["AREAID"]));
                    checkParam.Add("CONSUMABLELOTID", consumableLotId);
                    checkParam.Add("CONSUMABLEDEFID", consumableDefId);
                    checkParam.Add("CONSUMABLEDEFVERSION", consumableDefVersion);

                    DataTable priorityConsumableList = SqlExecuter.Query("GetPriorityConsumableLotByProcess", "10001", checkParam);

                    if (priorityConsumableList.Rows.Count > 0)
                    {
                        // 해당 자재 보다 먼저 입고된 자재 Lot이 있습니다. 그래도 진행 하겠습니까?
                        if (MSGBox.Show(MessageBoxType.Question, "ExistsFirstStockLot", MessageBoxButtons.YesNo) == DialogResult.No)
                            return;
                    }

                    DataTable dataSource = grdConsumableComplete.DataSource as DataTable;

                    var query = from consumable in dataSource.Rows.Cast<DataRow>()
                                group consumable by consumable["KEYCOLUMN"].ToString() into g
                                select new
                                {
                                    ConsumableId = g.Key,
                                    InputQty = g.Sum(qty => Format.GetDecimal(qty["ORGINPUTQTY"]))
                                };

                    string key = Format.GetString(consumableRow["KEYCOLUMN"]);

                    Dictionary<string, decimal> inputQtyList = query.ToDictionary(value => value.ConsumableId, value => value.InputQty);

                    inputQtyList.ForEach(value =>
                    {
                        if (key == value.Key)
                        {
                            decimal reqQty = Format.GetDecimal(grdStandardRequirementComplete.View.GetRowCellValue(grdStandardRequirementComplete.View.LocateByValue("KEYCOLUMN", value.Key), "REQUIREMENTQTY"));

                            if (reqQty - value.Value <= 0)
                            {
                                consumableRow["INPUTQTY"] = 0;
                                consumableRow["ORGINPUTQTY"] = 0;
                            }
                            else
                            {
                                if (Format.GetDecimal(consumableRow["INPUTQTY"]) > reqQty - value.Value)
                                {
                                    consumableRow["INPUTQTY"] = reqQty - value.Value;
                                    consumableRow["ORGINPUTQTY"] = reqQty - value.Value;
                                }
                            }
                        }
                    });

                    consumableList.Rows.Cast<DataRow>().ForEach(row =>
                    {
                        DataRow newRow = dataSource.NewRow();
                        newRow.ItemArray = row.ItemArray.Clone() as object[];

                        dataSource.Rows.Add(newRow);


                        string consumableLot = Format.GetString(row["CONSUMABLELOTID"]);
                        if (_consumableQty.Keys.Contains(consumableLot))
                        {
                            _consumableQty[consumableLot] = _consumableQty[consumableLot] + Format.GetDecimal(row["ORGINPUTQTY"]);
                        }
                        else
                        {
                            _consumableQty.Add(consumableLot, Format.GetDecimal(row["ORGINPUTQTY"]));
                        }
                    });

                    dataSource.AcceptChanges();


                    return;
                }


                DataTable alternativeList = SqlExecuter.Query("GetConsumableAlternativeLotByProcess", _queryVersion, param);

                if (alternativeList.Rows.Count > 0)
                {
                    DataRow consumableRow = alternativeList.Rows.Cast<DataRow>().FirstOrDefault();

                    DataRow lotRow = _lotInfo.Rows.Cast<DataRow>().FirstOrDefault();

                    string lotType = Format.GetString(lotRow["LOTTYPE"]);

                    string currentAreaId = Format.GetString(lotRow["AREAID"]);
                    string consumableAreaId = Format.GetString(consumableRow["AREAID"]);

                    string currentWarehouseId = Format.GetString(lotRow["WAREHOUSEID"]);
                    string consumableWarehouseId = Format.GetString(consumableRow["WAREHOUSEID"]);

                    if (currentWarehouseId != consumableWarehouseId)
                    {
                        // 현재 창고에서 보유하고 있는 자재가 아닙니다. 현재 창고 ID : {0}, 소유 창고 ID : {1}
                        throw MessageException.Create("ConsumableNotInWarehouse", currentWarehouseId, consumableWarehouseId);
                    }

                    /*
                    if (lotType != "Sample" && currentAreaId != consumableAreaId)
                    {
                        // 현재 작업장에서 보유하고 있는 자재가 아닙니다. 현재 작업장 ID : {0}, 소유 작업장 ID : {1}
                        throw MessageException.Create("ConsumableNotInArea", currentAreaId, consumableAreaId);
                    }
                    */

                    DataTable dataSource = grdConsumableComplete.DataSource as DataTable;

                    var query = from consumable in dataSource.Rows.Cast<DataRow>()
                                group consumable by consumable["KEYCOLUMN"].ToString() into g
                                select new
                                {
                                    ConsumableId = g.Key,
                                    InputQty = g.Sum(qty => Format.GetDecimal(qty["ORGINPUTQTY"]))
                                };

                    string key = Format.GetString(consumableRow["KEYCOLUMN"]);

                    Dictionary<string, decimal> inputQtyList = query.ToDictionary(value => value.ConsumableId, value => value.InputQty);

                    inputQtyList.ForEach(value =>
                    {
                        if (key == value.Key)
                        {
                            decimal reqQty = Format.GetDecimal(grdStandardRequirementComplete.View.GetRowCellValue(grdStandardRequirementComplete.View.LocateByValue("KEYCOLUMN", value.Key), "REQUIREMENTQTY"));

                            if (reqQty - value.Value < 0)
                            {
                                consumableRow["INPUTQTY"] = 0;
                                consumableRow["ORGINPUTQTY"] = 0;
                            }
                            else
                            {
                                if (Format.GetDecimal(consumableRow["INPUTQTY"]) > reqQty - value.Value)
                                {
                                    consumableRow["INPUTQTY"] = reqQty - value.Value;
                                    consumableRow["ORGINPUTQTY"] = reqQty - value.Value;
                                }
                            }
                        }
                    });

                    alternativeList.Rows.Cast<DataRow>().ForEach(row =>
                    {
                        DataRow newRow = dataSource.NewRow();
                        newRow.ItemArray = row.ItemArray.Clone() as object[];

                        dataSource.Rows.Add(newRow);


                        string consumableLot = Format.GetString(row["CONSUMABLELOTID"]);
                        if (_consumableQty.Keys.Contains(consumableLot))
                        {
                            _consumableQty[consumableLot] = _consumableQty[consumableLot] + Format.GetDecimal(row["ORGINPUTQTY"]);
                        }
                        else
                        {
                            _consumableQty.Add(consumableLot, Format.GetDecimal(row["ORGINPUTQTY"]));
                        }
                    });

                    dataSource.AcceptChanges();


                    return;
                }

                // 해당 자재 Lot의 품목은 BOM에 등록 되지 않은 자재 입니다.
                MSGBox.Show(MessageBoxType.Information, "NotExistsConsumableInBOM");
            }
        }

        private void TxtConsumableLotIdComplete_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                KeyEventArgs args = new KeyEventArgs(Keys.Enter);
                TxtConsumableLotIdComplete_KeyDown(sender, args);
            }
        }

        private void GrdConsumableComplete_ToolbarDeleteRow(object sender, EventArgs e)
        {
            if (grdConsumableComplete.View.FocusedRowHandle < 0)
                return;

            //grdConsumableComplete.View.DeleteRow(grdConsumableComplete.View.FocusedRowHandle);
            (grdConsumableComplete.View.DataSource as DataView).Delete(grdConsumableComplete.View.FocusedRowHandle);
            (grdConsumableComplete.View.DataSource as DataView).Table.AcceptChanges();
        }

        private void ConsumableCompleteView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "INPUTQTY")
            {
                grdConsumableComplete.View.CellValueChanged -= ConsumableCompleteView_CellValueChanged;

                decimal stockQty = Format.GetDecimal(grdConsumableComplete.View.GetFocusedRowCellValue("STOCKQTY"));
                decimal inputQty = Format.GetDecimal(e.Value);
                decimal defectQty = Format.GetDecimal(grdConsumableComplete.View.GetFocusedRowCellValue("DEFECTQTY"));
                decimal lotUsingQty = Format.GetDecimal(grdConsumableComplete.View.GetFocusedRowCellValue("LOTUSINGQTY"));

                if (inputQty > stockQty - lotUsingQty)
                {
                    // 투입수량은 가용수량(재고수량 - Lot 사용수량)을 초과할 수 없습니다. 가용수량 = {0}
                    MSGBox.Show(MessageBoxType.Warning, "InputQtyGreaterThanAvailableQty", (stockQty - lotUsingQty).ToString("#,##0.#####"));

                    grdConsumableComplete.View.SetFocusedRowCellValue("INPUTQTY", stockQty - lotUsingQty);

                    grdConsumableComplete.View.CellValueChanged += ConsumableCompleteView_CellValueChanged;

                    return;
                }

                decimal originalInputQty = inputQty - defectQty;


                grdConsumableComplete.View.SetFocusedRowCellValue("ORGINPUTQTY", originalInputQty);

                grdConsumableComplete.View.CellValueChanged += ConsumableCompleteView_CellValueChanged;
            }
        }

        private void ConsumableCompleteView_GridCellButtonClickEvent(SmartBandedGridView sender, GridCellButtonClickEventArgs e)
        {
            if (e.FieldName == "CONSUMABLEDEFECT")
            {
                string consumableLotId = grdConsumableComplete.View.GetFocusedRowCellValue("CONSUMABLELOTID").ToString();
                //decimal currentQty = Format.GetDecimal(grdConsumableComplete.View.GetFocusedRowCellValue("QTY"));
                //decimal originalQty = Format.GetDecimal(grdConsumableComplete.View.GetFocusedRowCellValue("ORGQTY"));
                decimal stockQty = Format.GetDecimal(grdConsumableComplete.View.GetFocusedRowCellValue("STOCKQTY"));
                decimal availableQty = Format.GetDecimal(grdConsumableComplete.View.GetFocusedRowCellValue("AVAILABLEQTY"));
                decimal inputQty = Format.GetDecimal(grdConsumableComplete.View.GetFocusedRowCellValue("INPUTQTY"));
                decimal originalInputQty = Format.GetDecimal(grdConsumableComplete.View.GetFocusedRowCellValue("ORGINPUTQTY"));
                decimal defectQty = Format.GetDecimal(grdConsumableComplete.View.GetFocusedRowCellValue("DEFECTQTY"));



                ConsumableDefectProcessPopup popup = new ConsumableDefectProcessPopup(consumableLotId, availableQty, _consumableDefectList);
                popup.ShowDialog();

                //_consumableDefectList = popup.GetConsumableDefectList();

                //DataRow[] row = _consumableDefectList.Select("CONSUMABLELOTID = '" + consumableLotId + "'");

                //if (row.Count() > 0)
                //{
                //    int changeQty = originalQty - Format.GetInteger(row[0]["DEFECTQTY"]);

                //    grdConsumableComplete.View.SetFocusedRowCellValue("QTY", changeQty);

                //    if (changeQty < inputQty)
                //        grdConsumableComplete.View.SetFocusedRowCellValue("INPUTQTY", changeQty);
                //}

                if (popup.DialogResult == DialogResult.OK)
                {
                    decimal inputDefectQty = popup.GetDefectQty();

                    grdConsumableComplete.View.SetFocusedRowCellValue("DEFECTQTY", inputDefectQty);

                    if (inputDefectQty + _consumableQty[consumableLotId] > availableQty)
                    {
                        grdConsumableComplete.View.SetFocusedRowCellValue("INPUTQTY", availableQty);
                        grdConsumableComplete.View.SetFocusedRowCellValue("ORGINPUTQTY", availableQty - inputDefectQty);
                    }
                    else
                    {
                        grdConsumableComplete.View.SetFocusedRowCellValue("INPUTQTY", inputDefectQty + _consumableQty[consumableLotId]);
                        grdConsumableComplete.View.SetFocusedRowCellValue("ORGINPUTQTY", _consumableQty[consumableLotId]);
                    }
                }

                //if (inputQty < inputDefectQty)
                //{
                //    // 불량수량은 투입수량을 초과 할 수 없습니다. 불량수량 : {0}, 투입수량 : {1}
                //    MSGBox.Show(MessageBoxType.Warning, "DefectQtyIsNotLagerThanInputQty", new string[] { inputDefectQty.ToString("#,##0.#####"), inputQty.ToString("#,##0.#####") });
                //    return;
                //}

                //grdConsumableComplete.View.SetFocusedRowCellValue("ORGINPUTQTY", inputQty - inputDefectQty);
                //grdConsumableComplete.View.SetFocusedRowCellValue("DEFECTQTY", inputDefectQty);
            }
        }

        private void TxtDurableLotIdStart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string durableLotId = txtDurableLotIdStart.Text;

                int rowHandle = grdDurableStart.View.LocateByValue("DURABLELOTID", durableLotId);

                if (rowHandle < 0)
                {
                    // 현재 작업에서 사용할 수 없는 치공구 Lot 입니다.
                    MSGBox.Show(MessageBoxType.Information, "NotAvailableDurableLotProcess");
                    txtDurableLotIdStart.Text = "";

                    return;
                }

                grdDurableStart.View.CheckRow(rowHandle, true);

                txtDurableLotIdStart.Text = "";

                //if (!CheckAlreadyInputLot(txtDurableLotIdStart.Text, "DURABLELOTID", grdDurableStart))
                //{
                //    txtDurableLotIdStart.Text = "";
                //    return;
                //}

                //Dictionary<string, object> param = new Dictionary<string, object>();
                //param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                //param.Add("PLANTID", UserInfo.Current.Plant);
                //param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                //param.Add("LOTID", _lotId);
                //param.Add("DURABLELOTID", txtDurableLotIdStart.Text);

                //txtDurableLotIdStart.Text = "";


                //DataTable durableList = SqlExecuter.Query("GetDurableLotByProcess", _queryVersion, param);

                //if (durableList.Rows.Count > 0)
                //{
                //    int usedCount = Format.GetInteger(durableList.Rows.Cast<DataRow>().FirstOrDefault()["USEDCOUNT"]);
                //    int usedLimit = Format.GetInteger(durableList.Rows.Cast<DataRow>().FirstOrDefault()["USEDLIMIT"]);

                //    if (usedCount >= usedLimit)
                //    {
                //        // 치공구의 현재 사용횟수가 한계 사용횟수를 초과하였습니다. 그래도 진행 하겠습니까?
                //        if (MSGBox.Show(MessageBoxType.Question, "UsedCountLargerThanUsedLimit", MessageBoxButtons.YesNo) == DialogResult.No)
                //            return;
                //    }

                //    DataTable dataSource = grdDurableStart.DataSource as DataTable;

                //    durableList.Rows.Cast<DataRow>().ForEach(row =>
                //    {
                //        DataRow newRow = dataSource.NewRow();
                //        newRow.ItemArray = row.ItemArray.Clone() as object[];

                //        dataSource.Rows.Add(newRow);
                //    });

                //    dataSource.AcceptChanges();


                //    return;
                //}

                //// 해당 치공구 Lot의 품목은 BOR에 등록되지 않은 자재 입니다.
                //MSGBox.Show(MessageBoxType.Information, "NotExistsDurableInBOR");
            }
        }

        private void TxtDurableLotIdStart_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                KeyEventArgs args = new KeyEventArgs(Keys.Enter);
                TxtDurableLotIdStart_KeyDown(sender, args);
            }
        }

        private void GrdDurableStart_ToolbarDeleteRow(object sender, EventArgs e)
        {
            (grdDurableStart.View.DataSource as DataView).Delete(grdDurableStart.View.FocusedRowHandle);
            (grdDurableStart.View.DataSource as DataView).Table.AcceptChanges();
        }

        private void TxtDurableLotIdComplete_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!CheckAlreadyInputLot(txtDurableLotIdComplete.Text, "DURABLELOTID", grdDurableComplete))
                {
                    txtDurableLotIdComplete.Text = "";
                    return;
                }

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                param.Add("PLANTID", UserInfo.Current.Plant);
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                param.Add("LOTID", _lotId);
                param.Add("DURABLELOTID", txtDurableLotIdComplete.Text);

                txtDurableLotIdComplete.Text = "";


                DataTable durableList = SqlExecuter.Query("GetDurableLotByProcess", _queryVersion, param);

                if (durableList.Rows.Count > 0)
                {
                    int usedCount = Format.GetInteger(durableList.Rows.Cast<DataRow>().FirstOrDefault()["USEDCOUNT"]);
                    int usedLimit = Format.GetInteger(durableList.Rows.Cast<DataRow>().FirstOrDefault()["USEDLIMIT"]);

                    if (usedCount >= usedLimit)
                    {
                        // 치공구의 현재 사용횟수가 한계 사용횟수를 초과하였습니다. 그래도 진행 하겠습니까?
                        if (MSGBox.Show(MessageBoxType.Question, "UsedCountLargerThanUsedLimit", MessageBoxButtons.YesNo) == DialogResult.No)
                            return;
                    }

                    DataTable dataSource = grdDurableComplete.DataSource as DataTable;

                    durableList.Rows.Cast<DataRow>().ForEach(row =>
                    {
                        DataRow newRow = dataSource.NewRow();
                        newRow.ItemArray = row.ItemArray.Clone() as object[];

                        dataSource.Rows.Add(newRow);
                    });

                    dataSource.AcceptChanges();


                    return;
                }

                // 해당 치공구 Lot의 품목은 BOR에 등록되지 않은 자재 입니다.
                MSGBox.Show(MessageBoxType.Information, "NotExistsDurableInBOR");
            }
        }

        private void TxtDurableLotIdComplete_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                KeyEventArgs args = new KeyEventArgs(Keys.Enter);
                TxtDurableLotIdComplete_KeyDown(sender, args);
            }
        }

        private void DurableStartView_CheckStateChanged(object sender, EventArgs e)
        {
            var checkedRowHandles = grdDurableStart.View.GetCheckedRowsHandle();

            DataTable equipmentList = grdEquipment.DataSource as DataTable;
            DataTable durableList = grdDurableStart.DataSource as DataTable;

            durableList.AsEnumerable().ForEach(r =>
            {
                r["EQUIPMENTID"] = null;
            });

            if (equipmentList.Rows.Count == 1 && checkedRowHandles.Count() == 1)
            {
                string equipmentId = Format.GetString(equipmentList.AsEnumerable().First()["EQUIPMENTID"]);

                grdDurableStart.View.SetRowCellValue(checkedRowHandles.First(), "EQUIPMENTID", equipmentId);
            }
        }

        private void GrdDurableComplete_ToolbarDeleteRow(object sender, EventArgs e)
        {
            (grdDurableComplete.View.DataSource as DataView).Delete(grdDurableComplete.View.FocusedRowHandle);
            (grdDurableComplete.View.DataSource as DataView).Table.AcceptChanges();
        }

        private void GrpAOIDefect_HeaderButtonClickEvent(object sender, HeaderButtonClickArgs args)
        {
            if (args.ClickItem == GridButtonItem.Add)
            {
                grdAOIDefect.View.AddNewRow();
            }
            else if (args.ClickItem == GridButtonItem.Delete)
            {
                grdAOIDefect.View.DeleteRow(grdAOIDefect.View.FocusedRowHandle);
            }
        }

        private void AOIDefectView_AddingNewRow(SmartBandedGridView sender, AddNewRowArgs args)
        {
            args.NewRow["ISCHANGE"] = "Y";
        }

        private void AOIDefetView_ShowingEditor(object sender, CancelEventArgs e)
        {
            string focusedFieldName = grdAOIDefect.View.FocusedColumn.FieldName;
            string isChangeRow = Format.GetString(grdAOIDefect.View.GetFocusedRowCellValue("ISCHANGE"));

            bool isChangeCell = false;

            if (isChangeRow == "N")
            {
                if (UserInfo.Current.Enterprise == Constants.EnterPrise_InterFlex)
                {
                    string defectCodeGroupId = Format.GetString(grdAOIDefect.View.GetFocusedRowCellValue("DEFECTCODEGROUPID"));

                    if (_isRepairProcess || _isSendStep)
                    {
                        if ((focusedFieldName == "ANALYSISGOODQTY" || focusedFieldName == "ANALYSISGOODPNLQTY") && defectCodeGroupId == "005")
                            isChangeCell = true;
                    }
                    else
                    {
                        if (focusedFieldName == "LAYER")
                            isChangeCell = true;

                        if (focusedFieldName == "PCSQTY" && defectCodeGroupId != "005")
                            isChangeCell = true;
                    }
                }
                else if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                {
                    string defectCode = Format.GetString(grdAOIDefect.View.GetFocusedRowCellValue("DEFECTCODE"));

                    if (_isRepairProcess || _isSendStep)
                    {
                        if ((focusedFieldName == "ANALYSISGOODQTY" || focusedFieldName == "ANALYSISGOODPNLQTY")/* && (defectCode == "1001" || defectCode == "2001")*/)
                        {
                            string analysisTarget = Format.GetString(grdAOIDefect.View.GetFocusedRowCellValue("ANALYSISQTY")); //분석대상수량
                            string goodQty = Format.GetString(grdAOIDefect.View.GetFocusedRowCellValue("ANALYSISGOODQTY")); //분석양품수량

                            if (analysisTarget == "0" || string.IsNullOrEmpty(analysisTarget) || goodQty == "0" || string.IsNullOrEmpty(goodQty))
                            {
                                e.Cancel = true;
                            }
                            else
                            {
                                isChangeCell = true;
                            }
                        }  
                        //if ((defectCode != "1001" && defectCode != "2001") || focusedFieldName != "ANALYSISGOODQTY")
                        //    e.Cancel = true;
                    }
                    else
                    {
                        if (focusedFieldName != "LAYER")
                            isChangeCell = true;

                        if (focusedFieldName == "PCSQTY"/* && defectCode != "1001" && defectCode != "2001"*/)
                            isChangeCell = true;

                        if (focusedFieldName == "ANALYSISQTY"/* && (defectCode == "1001" || defectCode == "2001")*/)
                            isChangeCell = true;
                    }
                }
            }
            else
            {
                string defectCode = Format.GetString(grdAOIDefect.View.GetFocusedRowCellValue("DEFECTCODE"));

                if (!_isRepairProcess && !_isSendStep && UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong/* && (defectCode == "1001" || defectCode == "2001")*/)
                {
                    if (focusedFieldName != "ANALYSISGOODQTY" && focusedFieldName != "ANALYSISGOODPNLQTY")
                        isChangeCell = true;
                }
                else
                {
                    if (focusedFieldName != "ANALYSISQTY" && focusedFieldName != "ANALYSISGOODQTY" && focusedFieldName != "ANALYSISGOODPNLQTY")
                        isChangeCell = true;
                }
            }

            e.Cancel = !isChangeCell;
        }

        private void AOIDefectView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "DEFECTCODEGROUPID")
            {
                RepositoryItemLookUpEdit edit = e.Column.ColumnEdit as RepositoryItemLookUpEdit;

                string defectCodeGroupName = Format.GetString(edit.GetDataSourceValue("CODENAME", edit.GetDataSourceRowIndex("CODEID", e.Value)));

                grdAOIDefect.View.SetFocusedRowCellValue("DEFECTCODEGROUPNAME", defectCodeGroupName);
            }
            else if (e.Column.FieldName == "DEFECTCODE" || e.Column.FieldName == "LAYER")
            {
                string defectCode = Format.GetString(grdAOIDefect.View.GetFocusedRowCellValue("DEFECTCODE"));
                string layerId = Format.GetString(grdAOIDefect.View.GetFocusedRowCellValue("LAYER"));

                DataTable dataSource = grdAOIDefect.DataSource as DataTable;

                if (dataSource.Select("DEFECTCODE = '" + defectCode + "' AND LAYER = '" + layerId + "'").Count() > 0)
                {
                    // 이미 등록된 불량 항목입니다.
                    MSGBox.Show(MessageBoxType.Information, "AlreadyInputDefectItem");

                    grdAOIDefect.View.CellValueChanged -= AOIDefectView_CellValueChanged;

                    grdAOIDefect.View.SetFocusedRowCellValue(e.Column, null);

                    grdAOIDefect.View.CellValueChanged += AOIDefectView_CellValueChanged;

                    return;
                }

                if (e.Column.FieldName == "DEFECTCODE")
                {
                    RepositoryItemLookUpEdit edit = e.Column.ColumnEdit as RepositoryItemLookUpEdit;

                    string defectCodeName = Format.GetString(edit.GetDataSourceValue("CODENAME", edit.GetDataSourceRowIndex("CODEID", e.Value)));

                    grdAOIDefect.View.SetFocusedRowCellValue("DEFECTCODENAME", defectCodeName);
                }
            }
            else if (e.Column.FieldName == "PCSQTY")
            {
                string defectCodeGroupId = Format.GetString(grdAOIDefect.View.GetFocusedRowCellValue("DEFECTCODEGROUPID"));
                string defectCode = Format.GetString(grdAOIDefect.View.GetFocusedRowCellValue("DEFECTCODE"));

                if (string.IsNullOrEmpty(defectCodeGroupId) || string.IsNullOrEmpty(defectCode))
                {
                    // 불량 그룹, 불량 항목을 선택하시기 바랍니다.
                    MSGBox.Show(MessageBoxType.Information, "SelectDefectGroupAndDefectItem");

                    grdAOIDefect.View.CellValueChanged -= AOIDefectView_CellValueChanged;

                    grdAOIDefect.View.SetFocusedRowCellValue(e.Column, null);

                    grdAOIDefect.View.CellValueChanged += AOIDefectView_CellValueChanged;

                    return;
                }

                if (UserInfo.Current.Enterprise == Constants.EnterPrise_InterFlex)
                {
                    if (defectCodeGroupId == "005")
                        grdAOIDefect.View.SetFocusedRowCellValue("ANALYSISQTY", e.Value);
                }
                else if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                {
                    //if (defectCode == "1001" || defectCode == "2001")
                    //    grdAOIDefect.View.SetFocusedRowCellValue("ANALYSISQTY", e.Value);
                }
            }
            else if (e.Column.FieldName == "ANALYSISQTY")
            {
                int defectQty = Format.GetInteger(grdAOIDefect.View.GetFocusedRowCellValue("PCSQTY"));
                int analysisTarget = Format.GetInteger(e.Value);

                if (analysisTarget > defectQty)
                {
                    // 분석대상 수량은 불량 수량을 초과할 수 없습니다.
                    MSGBox.Show(MessageBoxType.Information, "TargetQtyLessThanDefectQty");

                    grdAOIDefect.View.CellValueChanged -= AOIDefectView_CellValueChanged;

                    grdAOIDefect.View.SetFocusedRowCellValue("ANALYSISQTY", defectQty);

                    grdAOIDefect.View.CellValueChanged += AOIDefectView_CellValueChanged;

                    return;
                }
            }
            else if (e.Column.FieldName == "ANALYSISGOODQTY")
            {
                int defectQty = Format.GetInteger(grdAOIDefect.View.GetFocusedRowCellValue("PCSQTY"));
                int analysisTarget = Format.GetInteger(grdAOIDefect.View.GetFocusedRowCellValue("ANALYSISQTY")); //분석대상수량
                int goodQty = Format.GetInteger(e.Value); //분석양품수량

                if (goodQty > analysisTarget)
                {
                    // 분석양품 수량은 분석대상 수량을 초과할 수 없습니다.
                    MSGBox.Show(MessageBoxType.Information, "GoodQtyLessThanTargetQty");

                    grdAOIDefect.View.CellValueChanged -= AOIDefectView_CellValueChanged;

                    grdAOIDefect.View.SetFocusedRowCellValue("ANALYSISGOODQTY", analysisTarget);
                    grdAOIDefect.View.SetFocusedRowCellValue("FINALDEFECTQTY", defectQty - analysisTarget);

                    grdAOIDefect.View.CellValueChanged += AOIDefectView_CellValueChanged;

                    return;
                }

                grdAOIDefect.View.SetFocusedRowCellValue("FINALDEFECTQTY", defectQty - goodQty);
            }
        }

        private void AOIDefectView_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            Font ft = new Font("Mangul Gothic", 10F, FontStyle.Bold);
            e.Appearance.BackColor = Color.LightBlue;
            e.Appearance.FillRectangle(e.Cache, e.Bounds);
            e.Info.AllowDrawBackground = false;
            e.Appearance.Font = ft;
        }

        private void AOIDefectInfoView_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            Font ft = new Font("Mangul Gothic", 10F, FontStyle.Bold);
            e.Appearance.BackColor = Color.LightBlue;
            e.Appearance.FillRectangle(e.Cache, e.Bounds);
            e.Info.AllowDrawBackground = false;
            e.Appearance.Font = ft;
        }

        private void GrpBBTHOLEDefect_HeaderButtonClickEvent(object sender, HeaderButtonClickArgs args)
        {
            if (args.ClickItem == GridButtonItem.Add)
            {
                grdBBTHOLEDefect.View.AddNewRow();
            }
            else if (args.ClickItem == GridButtonItem.Delete)
            {
                grdBBTHOLEDefect.View.DeleteRow(grdBBTHOLEDefect.View.FocusedRowHandle);
            }
        }

        private void BBTHOLEDefectView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "DEFECTCODE")
            {
                string defectCode = Format.GetString(grdBBTHOLEDefect.View.GetFocusedRowCellValue("DEFECTCODE"));

                DataTable dataSource = grdBBTHOLEDefect.DataSource as DataTable;

                if (dataSource.Select("DEFECTCODE = '" + defectCode + "'").Count() > 0)
                {
                    // 이미 등록된 불량 항목입니다.
                    MSGBox.Show(MessageBoxType.Information, "AlreadyInputDefectItem");

                    grdBBTHOLEDefect.View.CellValueChanged -= BBTHOLEDefectView_CellValueChanged;

                    grdBBTHOLEDefect.View.SetFocusedRowCellValue(e.Column, null);

                    grdBBTHOLEDefect.View.CellValueChanged += BBTHOLEDefectView_CellValueChanged;

                    return;
                }

                RepositoryItemLookUpEdit edit = e.Column.ColumnEdit as RepositoryItemLookUpEdit;

                string defectCodeName = Format.GetString(edit.GetDataSourceValue("CODENAME", edit.GetDataSourceRowIndex("CODEID", e.Value)));

                grdBBTHOLEDefect.View.SetFocusedRowCellValue("DEFECTCODENAME", defectCodeName);
            }
        }

        private void BBTHOLEDefectView_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            Font ft = new Font("Mangul Gothic", 10F, FontStyle.Bold);
            e.Appearance.BackColor = Color.LightBlue;
            e.Appearance.FillRectangle(e.Cache, e.Bounds);
            e.Info.AllowDrawBackground = false;
            e.Appearance.Font = ft;
        }

        private void BBTHOLEDefectInfoView_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            Font ft = new Font("Mangul Gothic", 10F, FontStyle.Bold);
            e.Appearance.BackColor = Color.LightBlue;
            e.Appearance.FillRectangle(e.Cache, e.Bounds);
            e.Info.AllowDrawBackground = false;
            e.Appearance.Font = ft;
        }

        private void BtnDefectMapLink_Click(object sender, EventArgs e)
        {
            DefectMapLinkClick?.Invoke(sender, e);
        }

        private void MessageView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
                return;

            DataRow dr = this.grdMessage.View.GetFocusedDataRow();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("P_TXNHISTKEY", dr["TXNHISTKEY"].ToString());
            param.Add("P_LOTID", dr["LOTID"].ToString());
            param.Add("P_PROCESSSEGMENTID", dr["PROCESSSEGMENTID"].ToString());
            param.Add("P_USERSEQUENCE", dr["USERSEQUENCE"].ToString());

            DataTable dt = SqlExecuter.Query("SelectLotMessage", "10001", param);

            if (dt.Rows.Count < 1)
            {
                this.txtTitle.Text = string.Empty;
                this.rtbMessage.Rtf = null;
            }
            else
            {
                this.txtTitle.Text = dt.Rows[0]["TITLE"].ToString();
                this.rtbMessage.Rtf = dt.Rows[0]["MESSAGE"].ToString();
            }
        }

        #endregion

        #region Property

        /// <summary>
        /// User Control이 사용되는 화면 구분
        /// </summary>
        public ProcessType ProcessType { get; set; }


        public DataTable DurableDefDataSource
        {
            get
            {
                return _dtDurableSet;
            }
            set
            {
                _dtDurableSet = value;

                if (ProcessType == ProcessType.StartWork)
                    grdBORDurableStart.DataSource = _dtDurableSet;
                else if (ProcessType == ProcessType.WorkCompletion)
                    grdBORDurableComplete.DataSource = _dtDurableSet;

                if (_dtDurableSet != null)
                    SetDurableGridColumnVisible();
            }
        }

        public object EquipmentDataSource
        {
            get
            {
                return grdEquipment.DataSource;
            }
        }

        public object CommentDataSource
        {
            get
            {
                return grdComment.DataSource;
            }
            set
            {
                grdComment.DataSource = value;

                grdComment.View.FocusedRowHandle = grdComment.View.LocateByValue("ISCURRENTPROCESS", "Y");
                grdComment.View.FocusedColumn = grdComment.View.Columns["DESCRIPTION"];
            }
        }

        public object ProcessSpecDataSource
        {
            get
            {
                return grdProcessSpec.DataSource;
            }
            set
            {
                grdProcessSpec.DataSource = value;
            }
        }

        public object DefectDataSource
        {
            get
            {
                return grdDefect.DataSource;
            }
        }

        public object ConsumableStartDataSource
        {
            get
            {
                return grdConsumableStart.DataSource;
            }
        }

        public object StandardRequirementStartDataSource
        {
            get
            {
                return grdStandardRequirementStart.DataSource;
            }
            set
            {
                grdStandardRequirementStart.DataSource = value;
            }
        }

        public object ConsumableCompleteDataSource
        {
            get
            {
                return grdConsumableComplete.DataSource;
            }
        }

        public object StandardRequirementCompleteDataSource
        {
            get
            {
                return grdStandardRequirementComplete.DataSource;
            }
            set
            {
                grdStandardRequirementComplete.DataSource = value;
            }
        }

        public object DurableStartDataSource
        {
            get
            {
                return grdDurableStart.DataSource;
            }
        }

        public object DurableCompleteDataSource
        {
            get
            {
                return grdDurableComplete.DataSource;
            }
        }

        public object AOIDefectDataSource
        {
            get
            {
                if (tpgAOIDefect.PageVisible)
                {
                    DataTable dataSource = grdAOIDefect.DataSource as DataTable;

                    foreach (DataRow row in dataSource.Rows)
                    {
                        if (row["LAYER"] == DBNull.Value)
                        {
                            string defectCode = Format.GetString(row["DEFECTCODE"]);

                            // 불량이 발생한 Layer를 선택하시기 바랍니다. Defect Code : {0}
                            throw MessageException.Create("SelectLayer", defectCode);
                        }

                        if (row["PCSQTY"] == DBNull.Value)
                        {
                            string defectCode = Format.GetString(row["DEFECTCODE"]);

                            // 불량 수량을 입력하시기 바랍니다. Defect Code : {0}
                            throw MessageException.Create("InputDefectQty", defectCode);
                        }

                        if (_isRepairProcess)
                        {
                            string defectCodeGroupId = Format.GetString(row["DEFECTCODEGROUPID"]);
                            string defectCode = Format.GetString(row["DEFECTCODE"]);
                            int analysisQty = Format.GetInteger(row["ANALYSISQTY"]);

                            if ((UserInfo.Current.Enterprise == Constants.EnterPrise_InterFlex && defectCodeGroupId == "005") ||
                                (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong && analysisQty > 0/* && (defectCode == "1001" || defectCode == "2001")*/))
                            {
                                if (row["ANALYSISGOODQTY"] == DBNull.Value)
                                {
                                    // 분석 양품 수량을 입력하시기 바랍니다. Defect Code : {0}
                                    throw MessageException.Create("InputAnalysisGoodQty", defectCode);
                                }
                            }
                        }
                    }
                }

                if (UserInfo.Current.Enterprise == Constants.EnterPrise_InterFlex && !_isRepairProcess && !_isSendStep)
                {
                    DataTable dataSource = grdAOIDefect.DataSource as DataTable;
                    if (dataSource.Select("DEFECTCODEGROUPID = '005' AND ANALYSISQTY > 0").Count() > 0)
                    {
                        chkReworkPublish.Checked = true;
                    }
                }

                if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong && !_isRepairProcess && !_isSendStep)
                {
                    DataRow row = _lotInfo.AsEnumerable().FirstOrDefault();
                    string nextProcessSegmentType = Format.GetString(row["NEXTPROCESSSEGMENTTYPE"]);

                    DataTable dataSource = grdAOIDefect.DataSource as DataTable;
                    if (nextProcessSegmentType != "VRS" &&
                        dataSource.Select("ANALYSISQTY > 0").Count() > 0)
                    //dataSource.Select("(DEFECTCODE = '1001' OR DEFECTCODE = '2001') AND ANALYSISQTY > 0").Count() > 0)
                    {
                        chkReworkPublish.Checked = true;
                    }
                }

                return grdAOIDefect.DataSource;
            }
        }

        public object BBTHOLEDefectDataSource
        {
            get
            {
                if (tpgBBTHOLEDefect.PageVisible)
                    grdBBTHOLEDefect.View.CheckValidation();

                return grdBBTHOLEDefect.DataSource;
            }
        }

        public object EquipmentUseStatusDataSource
        {
            get
            {
                return grdEquipmentUseStatus.DataSource;
            }
            set
            {
                grdEquipmentUseStatus.DataSource = value;
            }
        }

        public object EquipmentRecipeDataSource
        {
            get
            {
                return grdEquipmentRecipe.DataSource;
            }
            set
            {
                if (ProcessType == ProcessType.StartWork)
                {
                    grdEquipmentRecipe.View.Columns["DATATYPE"].OwnerBand.Visible = false;
                    grdEquipmentRecipe.View.Columns["VALUE"].OwnerBand.Visible = false;
                }
                else if (ProcessType == ProcessType.WorkCompletion)
                {
                    grdEquipmentRecipe.View.Columns["DATATYPE"].OwnerBand.Visible = true;
                    grdEquipmentRecipe.View.Columns["VALUE"].OwnerBand.Visible = true;
                }

                grdEquipmentRecipe.DataSource = value;
            }
        }

        public object PostProcessEquipmentWipDataSource
        {
            get
            {
                return grdPostProcessEquipmentWip.DataSource;
            }
            set
            {
                grdPostProcessEquipmentWip.DataSource = value;
            }
        }

        public object MessageDataSource
        {
            get
            {
                return grdMessage.DataSource;
            }
            set
            {
                grdMessage.DataSource = value;

                if (value != null)
                {
                    bool isRealAllMessage = true;

                    foreach (DataRow row in (value as DataTable).Rows)
                    {
                        if (row["ISREAD"].ToString() == "N")
                        {
                            isRealAllMessage = false;
                            break;
                        }
                    }

                    if (!isRealAllMessage)
                    {
                        DataRow row = _lotInfo.Rows.Cast<DataRow>().FirstOrDefault();

                        DataTable lotMessageInfo = new DataTable();
                        lotMessageInfo.Columns.Add("LOTID", typeof(string));
                        lotMessageInfo.Columns.Add("PRODUCTDEFID", typeof(string));
                        lotMessageInfo.Columns.Add("PRODUCTDEFVERSION", typeof(string));
                        lotMessageInfo.Columns.Add("PRODUCTDEFNAME", typeof(string));
                        lotMessageInfo.Columns.Add("PROCESSSEGMENTID", typeof(string));
                        lotMessageInfo.Columns.Add("PROCESSSEGMENTVERSION", typeof(string));
                        lotMessageInfo.Columns.Add("PROCESSSEGMENTNAME", typeof(string));
                        lotMessageInfo.Columns.Add("USERSEQUENCE", typeof(string));

                        DataRow newRow = lotMessageInfo.NewRow();

                        foreach (DataColumn column in lotMessageInfo.Columns)
                        {
                            newRow[column.ColumnName] = row[column.ColumnName];
                        }

                        lotMessageInfo.Rows.Add(newRow);

                        lotMessageInfo.AcceptChanges();


                        //메시지 정보 조회
                        string lotId = lotMessageInfo.AsEnumerable().Select(r => r.Field<string>("LOTID")).Distinct().FirstOrDefault().ToString();
                        string productDefId = lotMessageInfo.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFID")).Distinct().FirstOrDefault().ToString();
                        string productDefVersion = lotMessageInfo.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFVERSION")).Distinct().FirstOrDefault().ToString();
                        string processSegmentId = lotMessageInfo.AsEnumerable().Select(r => r.Field<string>("PROCESSSEGMENTID")).Distinct().FirstOrDefault().ToString();

                        Dictionary<string, object> param = new Dictionary<string, object>();
                        param.Add("LOTID", lotId);
                        param.Add("PRODUCTDEFID", productDefId);
                        param.Add("PRODUCTDEFVERSION", productDefVersion);
                        param.Add("PROCESSSEGMENTID", processSegmentId);
                        param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                        param.Add("SHOWTYPE", "Y");

                        DataTable messageList = SqlExecuter.Query("GetLotMessageList", "10002", param);
                        if (messageList.Rows.Count > 0)
                        {
                            LotMessagePopupView popup = new LotMessagePopupView(lotMessageInfo);
                            popup.MessageDataSource = messageList;
                            popup.ShowDialog();

                            DataTable readList = popup.GetChangedRows();

                            MessageWorker worker = new MessageWorker("SaveLotMessageRead");
                            worker.SetBody(new MessageBody()
                            {
                                { "LotId", _lotId },
                                { "ReadMessageList", readList }
                            });

                            worker.Execute();
                        }


                    }
                }
            }
        }

        public bool IsReworkPublishChecked
        {
            get
            {
                return chkReworkPublish.Checked;
            }
        }

        public bool AOIDefectPageVisible
        {
            get
            {
                return tpgAOIDefect.PageVisible;
            }
        }

        #endregion

        #region Event Handler

        public event CellValueChangedEventHandler DefectQtyChanged;

        public event EventHandler DefectMapLinkClick;

        #endregion

        #region Public Function

        /// <summary>
        /// Process Type에 따라 Tab Page Visible 설정 변경
        /// </summary>
        public void InitializeTabPageVisible()
        {
            List<XtraTabPage> visiblePages = new List<XtraTabPage>();

            visiblePages.Add(tpgComment);
            visiblePages.Add(tpgProcessSpec);
            visiblePages.Add(tpgMessage);
            visiblePages.Add(tpgDefect);

            switch (ProcessType)
            {
                case ProcessType.LotAccept:
                    visiblePages.Remove(tpgDefect);
                    visiblePages.Add(tpgEquipmentUseStatus);
                    break;
                case ProcessType.StartWork:
                    visiblePages.Remove(tpgDefect);
                    visiblePages.Add(tpgEquipment);
                    visiblePages.Add(tpgConsumableStart);
                    visiblePages.Add(tpgDurableStart);
                    visiblePages.Add(tpgEquipmentRecipe);
                    break;
                case ProcessType.WorkCompletion:
                    visiblePages.Add(tpgEquipment);
                    visiblePages.Add(tpgConsumableComplete);
                    visiblePages.Add(tpgDurableComplete);
                    //visiblePages.Add(tpgAOIDefect);
                    visiblePages.Add(tpgEquipmentRecipe);
                    //visiblePages.Add(tpgPostProcessEquipmentWip);
                    break;
                case ProcessType.TransitRegist:
                    visiblePages.Add(tpgPostProcessEquipmentWip);
                    break;
            }


            tabProcessFourStepDetail.TabPages.ForEach(tabPage =>
            {
                if (visiblePages.Contains(tabPage))
                    tabPage.PageVisible = true;
                else
                    tabPage.PageVisible = false;
            });

            if (visiblePages.Contains(tpgEquipment))
                tabProcessFourStepDetail.SelectedTabPage = tpgEquipment;
            else
                tabProcessFourStepDetail.SelectedTabPage = tpgComment;
        }

        public void ClearData()
        {
            tabProcessFourStepDetail.TabPages.ForEach(tabPage =>
            {
                if (tabPage.PageVisible)
                {
                    tabPage.Controls.Find<SmartBandedGrid>(true).ForEach(grid =>
                    {
                        if (grid.DataSource != null)
                            grid.View.ClearDatas();
                    });

                    tabPage.Controls.Find<SmartRichTextBox>(true).ForEach(richText =>
                    {
                        richText.Text = "";
                    });
                }
            });

            if (tpgPostProcessEquipmentWip.PageVisible && ProcessType == ProcessType.WorkCompletion)
                tpgPostProcessEquipmentWip.PageVisible = false;

            if (tpgEquipment.PageVisible)
                tabProcessFourStepDetail.SelectedTabPage = tpgEquipment;
            else
                tabProcessFourStepDetail.SelectedTabPage = tpgComment;

            if (tpgConsumableComplete.PageVisible)
                _consumableDefectList.Rows.Clear();

            if (tpgAOIDefect.PageVisible)
                tpgAOIDefect.PageVisible = false;

            if (tpgBBTHOLEDefect.PageVisible)
                tpgBBTHOLEDefect.PageVisible = false;
        }

        public void ClearStartWorkData()
        {
            grdDurableStart.View.RefreshComboBoxDataSource("EQUIPMENTID", new SqlQueryAdapter());
            //grdConsumableStart.View.ClearDatas();
            //grdDurableStart.View.ClearDatas();

            //grdDefect.View.ClearDatas();
        }

        public void ClearCompleteWorkData()
        {
            grdConsumableComplete.View.ClearDatas();
            grdDurableComplete.View.ClearDatas();

            grdBBTHOLEDefect.View.ClearDatas();

            grdDefect.View.ClearDatas();

            _consumableDefectList.Rows.Clear();
            _consumableDefectList.AcceptChanges();

            if (tpgPostProcessEquipmentWip.PageVisible)
                tpgPostProcessEquipmentWip.PageVisible = false;

            if (tpgAOIDefect.PageVisible)
            {
                grdAOIDefect.View.ClearDatas();
                tpgAOIDefect.PageVisible = false;
            }
        }

        public void SetEquipmentDataSource(object dataSource, List<string> lotEquipment = null)
        {
            (txtEquipment.Editor.SelectPopupCondition as ConditionItemSelectPopup).SearchQuery = new SqlQueryAdapter(dataSource as DataTable);
            grdEquipment.DataSource = dataSource;

            if (ProcessType == ProcessType.StartWork)
            {
                DataTable equipmentList = dataSource as DataTable;
                if (equipmentList.Rows.Count == 1)
                {
                    grdEquipment.View.CheckedAll();
                    SetDurableLotEquipmentDataSource(dataSource);
                }
            }

            if (ProcessType == ProcessType.WorkCompletion)
                SetDurableLotEquipmentDataSource(dataSource);

            //if (ProcessType == ProcessType.WorkCompletion)
            //{
            //    //grdEquipment.View.CheckedAll();
            //    grdEquipment.View.CheckStateChanged -= EquipmentView_CheckStateChanged;

            //    for (int i = 0; i < (dataSource as DataTable).Rows.Count; i++)
            //    {
            //        DataRow row = (dataSource as DataTable).Rows[i];

            //        if (lotEquipment.Contains(Format.GetString(row["EQUIPMENTID"])))
            //            grdEquipment.View.CheckRow(i, true);
            //    }

            //    SetDurableLotEquipmentDataSource(grdEquipment.View.GetCheckedRows());

            //    grdEquipment.View.CheckStateChanged += EquipmentView_CheckStateChanged;
            //}
        }

        public void SetConsumableWorkStartDataSource(object dataSource)
        {
            grdConsumableStart.DataSource = dataSource;
        }

        public void SetConsumableWorkCompleteDataSource(object dataSource)
        {
            _consumableQty = new Dictionary<string, decimal>();

            DataTable data = dataSource as DataTable;

            data.AsEnumerable().ForEach(row =>
            {
                string consumableLotId = Format.GetString(row["CONSUMABLELOTID"]);

                if (_consumableQty.Keys.Contains(consumableLotId))
                {
                    _consumableQty[consumableLotId] = _consumableQty[consumableLotId] + Format.GetDecimal(row["ORGINPUTQTY"]);
                }
                else
                {
                    _consumableQty.Add(consumableLotId, Format.GetDecimal(row["ORGINPUTQTY"]));
                }
            });

            grdConsumableComplete.DataSource = dataSource;
        }

        public void SetDurableWorkStartDataSource(object dataSource)
        {
            grdDurableStart.DataSource = dataSource;
        }

        public void SetDurableWorkCompleteDataSource(object dataSource)
        {
            grdDurableComplete.DataSource = dataSource;
        }

        public DataTable GetCheckedDurableLotList()
        {
            return grdDurableStart.View.GetCheckedRows();
        }

        public void SetRecipeComboDataSource(object dataSource, string queryVersion = "10001")
        {
            //grdEquipment.View.RefreshComboBoxDataSource("RECIPEID", new SqlQueryAdapter(dataSource as DataTable));
            if (ProcessType == ProcessType.StartWork)
            {
                //grdEquipment.View.GetConditions().GetCondition<ConditionItemSelectPopup>("RECIPEID").SetIsReadOnly(false).SetSearchButtonReadOnly(false).SetClearButtonReadOnly(false);
                grdEquipment.View.Columns["RECIPEID"].OptionsColumn.AllowEdit = true;

                Dictionary<string, object> param = dataSource as Dictionary<string, object>;

                string recipeFieldName = Format.GetString(param["RECIPENAME"]);
                string productId = Format.GetString(param["PRODUCTID"]);
                string productVersion = Format.GetString(param["PRODUCTVERSION"]);
                string processDefId = Format.GetString(param["PROCESSDEFID"]);
                string processDefVersion = Format.GetString(param["PROCESSDEFVERSION"]);
                string segmentId = Format.GetString(param["SEGMENTID"]);
                string resourceId = Format.GetString(param["RESOURCEID"]);
                string lotId = Format.GetString(param["LOTID"]);

                ConditionItemSelectPopup conditionItem = grdEquipment.View.GetConditions().GetCondition<ConditionItemSelectPopup>("RECIPEID");
                conditionItem.SearchQuery = new SqlQuery("GetEquipmentRecipeList", queryVersion, $"RECIPENAME={recipeFieldName}", $"PRODUCTID={productId}", $"PRODUCTVERSION={productVersion}", $"PROCESSDEFID={processDefId}", $"PROCESSDEFVERSION={processDefVersion}", $"SEGMENTID={segmentId}", $"RESOURCEID={resourceId}", $"LOTID={lotId}");
            }
            else if (ProcessType == ProcessType.WorkCompletion)
            {
                //grdEquipment.View.GetConditions().GetCondition<ConditionItemSelectPopup>("RECIPEID").SetIsReadOnly().SetSearchButtonReadOnly().SetClearButtonReadOnly();
                grdEquipment.View.Columns["RECIPEID"].OptionsColumn.AllowEdit = false;
            }

        }

        public DataTable GetCheckedEquipmentList()
        {
            return grdEquipment.View.GetCheckedRows();
        }

        public string GetCheckedEquipmentIdUseStatus()
        {
            string result = "";

            DataTable checkedData = grdEquipmentUseStatus.View.GetCheckedRows();

            checkedData.Rows.Cast<DataRow>().ForEach(row =>
            {
                result += row["EQUIPMENTID"].ToString() + ",";
            });

            if (result.Length > 0)
                result = result.Substring(0, result.Length - 1);

            return result;
        }

        public void SetDurableEquipmentDataSource(DataView view)
        {
            RepositoryItemLookUpEdit equipmentId = grdDurableStart.View.Columns["EQUIPMENTID"].ColumnEdit as RepositoryItemLookUpEdit;

            equipmentId.DataSource = view.ToTable();
        }

        public void SetLotInfo(string lotId, decimal panelPerQty, decimal panelQty, decimal qty, string queryVersion = "10001", DataTable lotInfo = null)
        {
            _lotId = lotId;
            _panelPerQty = panelPerQty;
            _panelQty = panelQty;
            _qty = qty;
            _lotInfo = lotInfo;
            /*
            if (_lotInfo != null && _lotInfo.Rows.Count > 0)
            {
                DataRow row = _lotInfo.AsEnumerable().FirstOrDefault();

                string materialClass = Format.GetString(row["MATERIALCLASS"]);

                if (materialClass == "FG")
                {
                    grdDefect.View.Columns["QTY"].OptionsColumn.ReadOnly = false;
                    grdDefect.View.Columns["QTY"].OptionsColumn.AllowEdit = true;
                }
                else
                {
                    if (_panelPerQty == 0)
                    {
                        grdDefect.View.Columns["PNLQTY"].OptionsColumn.ReadOnly = true;
                        grdDefect.View.Columns["PNLQTY"].OptionsColumn.AllowEdit = false;
                        grdDefect.View.Columns["QTY"].OptionsColumn.ReadOnly = false;
                        grdDefect.View.Columns["QTY"].OptionsColumn.AllowEdit = true;
                    }
                    else
                    {
                        grdDefect.View.Columns["PNLQTY"].OptionsColumn.ReadOnly = false;
                        grdDefect.View.Columns["PNLQTY"].OptionsColumn.AllowEdit = true;
                        grdDefect.View.Columns["QTY"].OptionsColumn.ReadOnly = true;
                        grdDefect.View.Columns["QTY"].OptionsColumn.AllowEdit = false;
                    }
                }
            }
            */

            _queryVersion = queryVersion;
        }
        public void setQtyColText()
        {

            DataRow row = _lotInfo.AsEnumerable().FirstOrDefault();

            string Uom = Format.GetTrimString(row["PROCESSUOM"]);

            Color copyColor = grdDefect.View.Columns["DECISIONDEGREENAME"].AppearanceHeader.ForeColor;

            if (!Uom.Equals("PCS"))
            {
                grdDefect.PcsSetting = true;
                grdDefect.View.GetConditions().GetCondition("PNLQTY").IsRequired = true;
                grdDefect.View.GetConditions().GetCondition("QTY").IsRequired = false;
                grdDefect.View.Columns["PNLQTY"].AppearanceHeader.ForeColor = Color.Red;
                grdDefect.View.Columns["QTY"].AppearanceHeader.ForeColor = copyColor;
                //
            }
            else
            {
                grdDefect.PcsSetting = false;
                grdDefect.View.GetConditions().GetCondition("PNLQTY").IsRequired = false;
                grdDefect.View.GetConditions().GetCondition("QTY").IsRequired = true;
                grdDefect.View.Columns["QTY"].AppearanceHeader.ForeColor = Color.Red;
                grdDefect.View.Columns["PNLQTY"].AppearanceHeader.ForeColor = copyColor;
            }

            string ColCaption = Uom == "BLK" ? Language.Get("QTY") + "(" + "BLK" + ")" : Language.Get("QTY").ToString() + "(" + "PNL" + ")";
            grdDefect.View.Columns["PNLQTY"].Caption = ColCaption;
            grdDefect.View.Columns["PNLQTY"].ToolTip = ColCaption;

            grdDefect.DefectUOM = Uom;

        }
        public void DisplayTabRequire()
        {
            for (int i = 0; i < tabProcessFourStepDetail.TabPages.Count; i++)
            {
                if (tabProcessFourStepDetail.TabPages[i].PageVisible)
                {
                    switch (tabProcessFourStepDetail.TabPages[i].Name)
                    {
                        case "tpgDefect":
                            tabProcessFourStepDetail.TabPages[i].Appearance.Header.BackColor = Color.HotPink;
                            break;
                        case "tpgConsumableStart":
                            DataTable dtStart = grdStandardRequirementStart.DataSource as DataTable;
                            if (dtStart.Rows.Count > 0)
                            {
                                tabProcessFourStepDetail.TabPages[i].Appearance.Header.BackColor = Color.Yellow;
                            }
                            else
                            {
                                tabProcessFourStepDetail.TabPages[i].Appearance.Header.BackColor = Color.Transparent;
                            }
                            break;
                        case "tpgConsumableComplete":
                            DataTable dtComp = grdStandardRequirementComplete.DataSource as DataTable;

                            if (dtComp.Rows.Count > 0)
                            {
                                tabProcessFourStepDetail.TabPages[i].Appearance.Header.BackColor = Color.Yellow;
                            }
                            else
                            {
                                tabProcessFourStepDetail.TabPages[i].Appearance.Header.BackColor = Color.Transparent;
                            }
                            break;
                        case "tpgDurableStart":
                            if (_dtDurableSet.Rows.Count > 0)
                            {
                                tabProcessFourStepDetail.TabPages[i].Appearance.Header.BackColor = Color.Yellow;
                            }
                            else
                            {
                                tabProcessFourStepDetail.TabPages[i].Appearance.Header.BackColor = Color.Transparent;
                            }
                            break;
                        case "tpgDurableComplete":
                            if (_dtDurableSet.Rows.Count > 0)
                            {
                                tabProcessFourStepDetail.TabPages[i].Appearance.Header.BackColor = Color.Yellow;
                            }
                            else
                            {
                                tabProcessFourStepDetail.TabPages[i].Appearance.Header.BackColor = Color.Transparent;
                            }
                            break;
                        case "tpgAOIDefect":
                            if (tpgAOIDefect.PageVisible)
                            {
                                tabProcessFourStepDetail.TabPages[i].Appearance.Header.BackColor = Color.Yellow;
                            }
                            else
                            {
                                tabProcessFourStepDetail.TabPages[i].Appearance.Header.BackColor = Color.Transparent;
                            }
                            break;
                        case "tpgBBTHOLEDefect":
                            if (tpgBBTHOLEDefect.PageVisible)
                            {
                                tabProcessFourStepDetail.TabPages[i].Appearance.Header.BackColor = Color.Yellow;
                            }
                            else
                            {
                                tabProcessFourStepDetail.TabPages[i].Appearance.Header.BackColor = Color.Transparent;
                            }
                            break;
                    }
                }
            }
        }

        public void ClearTabPageHeaderColor()
        {
            tabProcessFourStepDetail.TabPages.ForEach(tabPage =>
            {
                if (tabPage.PageVisible)
                    tabPage.Appearance.Header.BackColor = Color.Transparent;
            });
        }

        public void SetPostProcessEquipmentWipVisible(bool visible = true)
        {
            tpgPostProcessEquipmentWip.PageVisible = visible;
        }

        public void ClearPostProcessEquipmentWipData()
        {
            if (grdPostProcessEquipmentWip.View.DataSource != null)
                grdPostProcessEquipmentWip.View.ClearDatas();
        }

        public DataTable GetConsumableDefectList()
        {
            return _consumableDefectList;
        }

        public void SetReadOnly(bool isReadOnly)
        {
            tabProcessFourStepDetail.TabPages.ForEach(tabPage =>
            {
                if (tabPage.PageVisible)
                {
                    SetReadOnlyControl(isReadOnly, tabPage);
                }
            });
        }
        
        public void SetEquipmentWorkTimeColumnHidden()
        {
            //grdEquipment.View.Columns["TRACKINTIME"].Visible = false;
            //grdEquipment.View.Columns["TRACKOUTTIME"].Visible = false;

            if (ProcessType == ProcessType.StartWork)
            {
                grdEquipment.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

                grdEquipment.View.GetConditions().GetCondition<ConditionItemTextBox>("TRACKINTIME").SetIsHidden();
                grdEquipment.View.GetConditions().GetCondition<ConditionItemTextBox>("TRACKOUTTIME").SetIsHidden();
                grdEquipment.View.GetConditions().GetCondition<ConditionItemSpinEdit>("PCSQTY").SetIsHidden();
                grdEquipment.View.GetConditions().GetCondition<ConditionItemSpinEdit>("PNLQTY").SetIsHidden();
            }

            if (ProcessType == ProcessType.WorkCompletion)
            {
                grdEquipment.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

                if (UserInfo.Current.Enterprise == Constants.EnterPrise_InterFlex)
                {
                    grdEquipment.View.GetConditions().GetCondition<ConditionItemSpinEdit>("PCSQTY").SetIsHidden();
                    grdEquipment.View.GetConditions().GetCondition<ConditionItemSpinEdit>("PNLQTY").SetIsHidden();
                }
            }

            grdEquipment.View.PopulateColumns();
        }

        public void SetAOIDefectPageVisible(bool isRepairProcess = false, bool isSendStep = false)
        {
            tpgAOIDefect.PageVisible = true;

            tabProcessFourStepDetail.SelectedTabPage = tpgAOIDefect;

            _isRepairProcess = isRepairProcess;
            _isSendStep = isSendStep;

            //InitializeAOIDefectPage();


            DataRow row = _lotInfo.AsEnumerable().FirstOrDefault();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("LOTID", _lotId);
            param.Add("PRODUCTDEFID", Format.GetString(row["PRODUCTDEFID"]));
            param.Add("PRODUCTDEFVERSION", Format.GetString(row["PRODUCTDEFVERSION"]));
            param.Add("EQUIPMENTTYPE", "AOI");


            DataTable defectInfoData = SqlExecuter.Query("SelectDefectDataByEquipment", "10001", param);

            grdAOIDefectInfo.DataSource = defectInfoData;


            if (isRepairProcess || isSendStep)
            {
                chkReworkPublish.Checked = false;
                chkReworkPublish.Visible = false;

                grpAOIDefect.GridButtonItem = GridButtonItem.None;

                Dictionary<string, object> defectParam = new Dictionary<string, object>();
                defectParam.Add("LOTID", _lotId);
                defectParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                DataTable defectList = new DataTable();
                if (isRepairProcess)
                    defectList = SqlExecuter.Query("SelectDefectMapDataByWorkComplete", "10001", defectParam);
                else
                    defectList = SqlExecuter.Query("SelectDefectMapDataForSend", "10001", defectParam);


                grdAOIDefect.DataSource = defectList;

                if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                {
                    grdAOIDefect.View.RefreshComboBoxDataSource("LAYER", new SqlQuery("GetLayerListForAoiBbtDefect_YP", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
                }
                else
                {
                    DataTable layerList = new DataTable();
                    layerList.Columns.Add("LAYERID", typeof(string));

                    List<string> layer = defectList.AsEnumerable().Select(r => Format.GetString(r["LAYER"])).Distinct().ToList();

                    layer.ForEach(c => layerList.Rows.Add(c));

                    layerList.AcceptChanges();

                    grdAOIDefect.View.RefreshComboBoxDataSource("LAYER", new SqlQueryAdapter(layerList));
                }
            }
            else
            {
                chkReworkPublish.Visible = true;

                DataTable layerList = new DataTable();

                if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                {
                    Dictionary<string, object> layerParam = new Dictionary<string, object>();
                    layerParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    layerList = SqlExecuter.Query("GetLayerListForAoiBbtDefect_YP", "10001", layerParam);

                    grdAOIDefect.View.RefreshComboBoxDataSource("LAYER", new SqlQueryAdapter(layerList));
                }
                else
                {
                    grdAOIDefect.View.RefreshComboBoxDataSource("LAYER", new SqlQuery("GetLayerListForAoiBbtDefect", "10001", $"LOTID={_lotId}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
                }

                grpAOIDefect.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;

                DataTable defectData = (grdAOIDefect.DataSource as DataTable).Clone();
                defectData.Columns["PCSQTY"].DataType = typeof(int);
                defectData.Columns["ANALYSISQTY"].DataType = typeof(int);
                defectData.Columns["REPAIRTARGETPNLQTY"].DataType = typeof(int);

                bool isReworkPublish = false;


                defectInfoData.AsEnumerable().ForEach(r =>
                {
                    DataRow newRow = defectData.NewRow();
                    newRow["DEFECTCODEGROUPID"] = r["DEFECTCODEGROUPID"];
                    newRow["DEFECTCODEGROUPNAME"] = r["DEFECTCODEGROUPNAME"];
                    newRow["DEFECTCODE"] = r["DEFECTCODE"];
                    newRow["DEFECTCODENAME"] = r["DEFECTCODENAME"];

                    if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                    {
                        var layerRowList = layerList.AsEnumerable().Where(lr => Format.GetString(lr["LAYERNAME"]) == Format.GetString(r["LAYERID"]));
                        if (layerRowList.Count() > 0)
                        {
                            DataRow layerRow = layerRowList.FirstOrDefault();
                            string layerId = Format.GetString(layerRow["LAYERID"]);

                            newRow["LAYER"] = layerId;
                        }
                    }
                    else
                    {
                        newRow["LAYER"] = r["LAYERID"];
                    }
                    newRow["PCSQTY"] = r["DEFECTQTY"];

                    if (UserInfo.Current.Enterprise == Constants.EnterPrise_InterFlex)
                    {
                        if (Format.GetString(r["DEFECTCODEGROUPID"]) == "005")
                        {
                            newRow["ANALYSISQTY"] = r["DEFECTQTY"];
                            newRow["SEQUENCE"] = 1;

                            isReworkPublish = true;
                        }
                        else
                        {
                            newRow["SEQUENCE"] = 11;
                        }
                    }
                    else if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                    {
                        newRow["SEQUENCE"] = 1;
                        //if (Format.GetString(r["DEFECTCODE"]) == "1001" || Format.GetString(r["DEFECTCODE"]) == "2001")
                        //{
                        //    //newRow["ANALYSISQTY"] = r["DEFECTQTY"];
                        //    newRow["SEQUENCE"] = 1;
                        //}
                        //else
                        //{
                        //    newRow["SEQUENCE"] = 11;
                        //}
                    }

                    newRow["ISCHANGE"] = "N";

                    defectData.Rows.Add(newRow);
                });

                defectData.AcceptChanges();

                grdAOIDefect.DataSource = defectData;


                if (UserInfo.Current.Enterprise == Constants.EnterPrise_InterFlex)
                {
                    chkReworkPublish.ReadOnly = true;
                    chkReworkPublish.Checked = isReworkPublish;
                }
                else if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                {
                    chkReworkPublish.ReadOnly = false;
                    chkReworkPublish.Checked = false;
                }
            }
        }

        public void SetBBTHOLEDefectPageVisible(string defectType)
        {
            tpgBBTHOLEDefect.PageVisible = true;

            tabProcessFourStepDetail.SelectedTabPage = tpgBBTHOLEDefect;

            //InitializeBBTHOLEDefectPage();

            tpgBBTHOLEDefect.Text = Language.Get(string.Concat(defectType, "DEFECT"));

            DataRow row = _lotInfo.AsEnumerable().FirstOrDefault();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("LOTID", _lotId);
            param.Add("PRODUCTDEFID", Format.GetString(row["PRODUCTDEFID"]));
            param.Add("PRODUCTDEFVERSION", Format.GetString(row["PRODUCTDEFVERSION"]));
            param.Add("EQUIPMENTTYPE", defectType);

            DataTable defectInfoData = SqlExecuter.Query("SelectDefectDataByEquipment", "10001", param);

            grdBBTHOLEDefectInfo.DataSource = defectInfoData;


            if (defectType == "BBT")
            {
                grdBBTHOLEDefect.View.RefreshComboBoxDataSource("DEFECTCODE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=DefectMapBBTDefectCode", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

                if (UserInfo.Current.Enterprise == Constants.EnterPrise_InterFlex)
                {
                    grpBBTHOLEDefect.GridButtonItem = GridButtonItem.None;

                    grdBBTHOLEDefect.View.Columns["DEFECTCODE"].OptionsColumn.AllowEdit = false;

                    Dictionary<string, object> defectParam = new Dictionary<string, object>();
                    defectParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    defectParam.Add("CODECLASSID", "DefectMapBBTDefectCode");
                    defectParam.Add("VALIDSTATE", "Valid");

                    DataTable bbtDefectList = SqlExecuter.Query("GetBBTDefectList", "10001", defectParam);

                    grdBBTHOLEDefect.DataSource = bbtDefectList;
                }
                else if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                {
                    grdBBTHOLEDefect.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;

                    grdBBTHOLEDefect.View.Columns["DEFECTCODE"].OptionsColumn.AllowEdit = true;

                    DataTable defectData = (grdBBTHOLEDefect.DataSource as DataTable).Clone();
                    defectData.Columns["PCSQTY"].DataType = typeof(int);


                    defectInfoData.AsEnumerable().ForEach(r =>
                    {
                        DataRow newRow = defectData.NewRow();
                        newRow["DEFECTCODEGROUPID"] = r["DEFECTCODEGROUPID"];
                        newRow["DEFECTCODEGROUPNAME"] = r["DEFECTCODEGROUPNAME"];
                        newRow["DEFECTCODE"] = r["DEFECTCODE"];
                        newRow["DEFECTCODENAME"] = r["DEFECTCODENAME"];
                        newRow["PCSQTY"] = r["DEFECTQTY"];

                        defectData.Rows.Add(newRow);
                    });

                    defectData.AcceptChanges();

                    grdBBTHOLEDefect.DataSource = defectData;
                }
            }
            else if (defectType == "HOLE")
            {
                grdBBTHOLEDefect.View.RefreshComboBoxDataSource("DEFECTCODE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=DefectMapHoleDefectCode", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

                grpBBTHOLEDefect.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;

                grdBBTHOLEDefect.View.Columns["DEFECTCODE"].OptionsColumn.AllowEdit = true;

                DataTable defectData = (grdBBTHOLEDefect.DataSource as DataTable).Clone();
                defectData.Columns["PCSQTY"].DataType = typeof(int);


                defectInfoData.AsEnumerable().ForEach(r =>
                {
                    DataRow newRow = defectData.NewRow();
                    newRow["DEFECTCODEGROUPID"] = r["DEFECTCODEGROUPID"];
                    newRow["DEFECTCODEGROUPNAME"] = r["DEFECTCODEGROUPNAME"];
                    newRow["DEFECTCODE"] = r["DEFECTCODE"];
                    newRow["DEFECTCODENAME"] = r["DEFECTCODENAME"];
                    newRow["PCSQTY"] = r["DEFECTQTY"];

                    defectData.Rows.Add(newRow);
                });

                defectData.AcceptChanges();

                grdBBTHOLEDefect.DataSource = defectData;
            }
        }

        public void SelectAOIDefectTabPage()
        {
            tabProcessFourStepDetail.SelectedTabPage = tpgAOIDefect;
        }

        public void SetDefectGridComboData(string lotId)
        {
            grdDefect.SetConsumableDefComboBox(lotId);
        }

        public bool GetDurableLimitColumnVisible()
        {
            SmartBandedGrid durableGrid = new SmartBandedGrid();

            if (ProcessType == ProcessType.StartWork)
                durableGrid = grdDurableStart;
            else if (ProcessType == ProcessType.WorkCompletion)
                durableGrid = grdDurableComplete;

            if (durableGrid.View.Columns["USEDLIMIT"] == null)
                return false;

            return durableGrid.View.Columns["USEDLIMIT"].OwnerBand.Visible;
        }

        public void ValidateAOIGrid()
        {
            if (tpgAOIDefect.PageVisible)
            {


                grdAOIDefect.View.CheckValidation();
            }
        }

        public void ValidateDefectGrid()
        {
            grdDefect.ValidateData();
        }

        #endregion

        #region Private Function

        private void InitializeStandardRequirementGrid(SmartBandedGrid grid)
        {
            grid.GridButtonItem = GridButtonItem.None;
            grid.ShowStatusBar = false;

            grid.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grid.View.SetIsReadOnly();

            // Key
            grid.View.AddTextBoxColumn("KEYCOLUMN", 100)
                .SetIsHidden();
            // 품목코드
            grid.View.AddTextBoxColumn("CONSUMABLEDEFID", 150);
            // 품목버전
            grid.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 80)
                .SetIsHidden();
            // 품목명
            grid.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
            // 기준소요량
            grid.View.AddSpinEditColumn("STANDARDREQUIREMENTQTY", 110)
                .SetDisplayFormat("#,##0.#########");
            // 소요량
            grid.View.AddSpinEditColumn("REQUIREMENTQTY", 90)
                .SetDisplayFormat("#,##0.#########");
            // 순번
            grid.View.AddTextBoxColumn("SEQUENCE", 50)
                .SetIsHidden();
            // 총투입량
            //grid.View.AddSpinEditColumn("TOTALINPUTQTY", 80);

            grid.View.PopulateColumns();


            grid.View.OptionsView.ShowIndicator = false;
        }

        private bool CheckAlreadyInputLot(string lotId, string fieldName, SmartBandedGrid grid)
        {
            var result = (grid.DataSource as DataTable).Select(fieldName + " = '" + lotId + "'");

            if (result.Count() > 0)
            {
                // 이미 등록된 Lot Id 입니다. Lot Id = {0}
                MSGBox.Show(MessageBoxType.Warning, "AlreadyInputLot", MessageBoxButtons.OK, lotId);
                grid.View.FocusedRowHandle = grid.View.LocateByValue(fieldName, lotId);
                grid.View.FocusedColumn = grid.View.Columns[fieldName];
                return false;
            }

            return true;
        }

        private void SetReadOnlyControl(bool isReadOnly, Control control)
        {
            if (control.Controls.Count > 0)
            {
                foreach (Control ctrl in control.Controls)
                {
                    if (ctrl is SmartLabelSelectPopupEdit smartLabelSelectPopup)
                        smartLabelSelectPopup.Editor.ReadOnly = isReadOnly;
                    else if (ctrl is SmartLabelTextBox smartLabelText)
                        smartLabelText.Editor.ReadOnly = isReadOnly;
                    else if (ctrl is SmartBandedGrid smartBandedGrid)
                        smartBandedGrid.Enabled = !isReadOnly;

                    SetReadOnlyControl(isReadOnly, ctrl);
                }
            }
        }

        private void SetDurableLotEquipmentDataSource(object dataSource)
        {
            if (ProcessType == ProcessType.StartWork)
                grdDurableStart.View.RefreshComboBoxDataSource("EQUIPMENTID", new SqlQueryAdapter(dataSource as DataTable));
            else if (ProcessType == ProcessType.WorkCompletion)
                grdDurableComplete.View.RefreshComboBoxDataSource("EQUIPMENTID", new SqlQueryAdapter(dataSource as DataTable));
        }

        private void SetDurableGridColumnVisible()
        {
            if (_dtDurableSet.Rows.Count > 0)
            {
                DataRow row = _dtDurableSet.AsEnumerable().FirstOrDefault();

                string durableClassId = Format.GetString(row["DURABLECLASSID"]);

                SmartBandedGrid durableGrid = new SmartBandedGrid();

                if (ProcessType == ProcessType.StartWork)
                    durableGrid = grdDurableStart;
                else if (ProcessType == ProcessType.WorkCompletion)
                    durableGrid = grdDurableComplete;

                if (durableClassId == "ToolType1")
                {
                    durableGrid.View.Columns["USEDLIMIT"].OwnerBand.Visible = true;
                    durableGrid.View.Columns["CLEANLIMIT"].OwnerBand.Visible = true;
                }
                else if (durableClassId == "ToolType5" || durableClassId == "ToolType7" || durableClassId == "ToolTypeA")
                {
                    durableGrid.View.Columns["USEDLIMIT"].OwnerBand.Visible = false;
                    durableGrid.View.Columns["CLEANLIMIT"].OwnerBand.Visible = false;
                }
            }
        }

        #endregion
    }
}