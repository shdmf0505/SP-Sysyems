#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using Micube.SmartMES.Commons.Controls;

using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.BandedGrid;

using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질 관리 > Defect Map > 품목 Defect Map 조회
    /// 업  무  설  명  : 품목 별로 Defect Map을 조회한다.
    ///                  설비에서 보낸 log Data의 내용만 조회 된다.
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-05-01
    /// 수  정  이  력  : 
    /// 2019.10.09 배선용 using Micube.Framework 추가
    ///            배선용 재공조회에서 화면이동시 이벤트 처리 override LoadForm
    /// </summary>
    public partial class DefectMapByItem : SmartConditionManualBaseForm
    {
        #region Local Variables

        /// <summary>
        /// Chart에서 선택한 포인트의 key 값
        /// </summary>
        private string _key = string.Empty;

        /// <summary>
        /// Chart에서 선택한 포인트의 Equipment Type
        /// </summary>
        private EquipmentType _equipmentType = EquipmentType.EQUIPMENTTYPE_AOI;

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public DefectMapByItem()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();
            InitializeGrid();
            InitializeControl();
            InitializeLanguageKey();
        }

        /// <summary>
        /// Grid 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region Main Grid

            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            var group = grdMain.View.AddGroupColumn("");
            group.AddTextBoxColumn("KEY", 150).SetIsHidden();
            group.AddTextBoxColumn("MODELID", 120).SetLabel("PRODUCTDEF").SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("MODELVERSION", 100).SetLabel("PRODUCTDEFVERSION").SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("EVENTTIME", 180).SetTextAlignment(TextAlignment.Left);

            group = grdMain.View.AddGroupColumn("AOIDEFECTRATEBYLAYER");
            group.AddTextBoxColumn("AOITOTALRATE", 100).SetLabel("TOTALRATE").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);
            group.AddTextBoxColumn("AOICS", 60).SetLabel("CS").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);
            group.AddTextBoxColumn("AOIONELAYER", 60).SetLabel("ONELAYER").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);
            group.AddTextBoxColumn("AOITWOLAYER", 60).SetLabel("TWOLAYER").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);
            group.AddTextBoxColumn("AOITHREELAYER", 60).SetLabel("THREELAYER").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);
            group.AddTextBoxColumn("AOIFOURLAYER", 60).SetLabel("FOURLAYER").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);
            group.AddTextBoxColumn("AOIFIVELAYER", 60).SetLabel("FIVELAYER").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);
            group.AddTextBoxColumn("AOISIXLAYER", 60).SetLabel("SIXLAYER").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);
            group.AddTextBoxColumn("AOISEVENLAYER", 60).SetLabel("SEVENLAYER").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);
            group.AddTextBoxColumn("AOIEIGHTLAYER", 60).SetLabel("EIGHTLAYER").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);
            group.AddTextBoxColumn("AOININELAYER", 60).SetLabel("NINELAYER").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);
            group.AddTextBoxColumn("AOITENLAYER", 60).SetLabel("TENLAYER").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);
            group.AddTextBoxColumn("AOIELEVENLAYER", 60).SetLabel("ELEVENLAYER").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);
            group.AddTextBoxColumn("AOITWELVELAYER", 60).SetLabel("TWELVELAYER").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);
            group.AddTextBoxColumn("AOISS", 60).SetLabel("SS").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);

            group = grdMain.View.AddGroupColumn("");
            group.AddTextBoxColumn("BBTDEFECTRATE", 120).SetDisplayFormat("{0:f2}%", MaskTypes.Custom); ;

            group = grdMain.View.AddGroupColumn("HOLEDEFECTRATEBYLAYER");
            group.AddTextBoxColumn("HOLETOTALRATE", 100).SetLabel("TOTALRATE").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);
            group.AddTextBoxColumn("HOLECS", 60).SetLabel("CS").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);
            group.AddTextBoxColumn("HOLEONELAYER", 60).SetLabel("ONELAYER").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);
            group.AddTextBoxColumn("HOLETWOLAYER", 60).SetLabel("TWOLAYER").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);
            group.AddTextBoxColumn("HOLETHREELAYER", 60).SetLabel("THREELAYER").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);
            group.AddTextBoxColumn("HOLEFOURLAYER", 60).SetLabel("FOURLAYER").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);
            group.AddTextBoxColumn("HOLEFIVELAYER", 60).SetLabel("FIVELAYER").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);
            group.AddTextBoxColumn("HOLESIXLAYER", 60).SetLabel("SIXLAYER").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);
            group.AddTextBoxColumn("HOLESEVENLAYER", 60).SetLabel("SEVENLAYER").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);
            group.AddTextBoxColumn("HOLEEIGHTLAYER", 60).SetLabel("EIGHTLAYER").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);
            group.AddTextBoxColumn("HOLENINELAYER", 60).SetLabel("NINELAYER").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);
            group.AddTextBoxColumn("HOLETENLAYER", 60).SetLabel("TENLAYER").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);
            group.AddTextBoxColumn("HOLEELEVENLAYER", 60).SetLabel("ELEVENLAYER").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);
            group.AddTextBoxColumn("HOLETWELVELAYER", 60).SetLabel("TWELVELAYER").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);
            group.AddTextBoxColumn("HOLESS", 60).SetLabel("SS").SetDisplayFormat("{0:f2}%", MaskTypes.Custom);

            grdMain.View.PopulateColumns();
            grdMain.View.BestFitColumns();
            grdMain.View.SetIsReadOnly();

            SetFooterSummary();

            grdMain.View.OptionsCustomization.AllowColumnMoving = false;
            grdMain.View.OptionsView.ShowFooter = true;
            grdMain.View.FooterPanelHeight = 10;
            grdMain.ShowStatusBar = false;

            grdMain.GridButtonItem = GridButtonItem.Export;

            #endregion

            #region Raw Data

            grdRowData.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            DefectMapHelper.SetGridColumnByRowData(grdRowData);

            grdRowData.View.SetIsReadOnly();
            grdRowData.View.PopulateColumns();
            grdRowData.View.BestFitColumns();

            grdRowData.ShowStatusBar = true;
            grdRowData.GridButtonItem = GridButtonItem.Export;

            #endregion
        }

        /// <summary>
        /// Controler 초기화
        /// </summary>
        private void InitializeControl()
        {
            // AOI
            tabDefectMap.Controls.Add(new ucDefectMapBasicForm()
            {
                Dock = DockStyle.Fill
            });

            // Comparison
            tabComparison.Controls.Add(new ucTypeComparison()
            {
                Dock = DockStyle.Fill
            });
        }

        /// <summary>
        /// Control Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            grdMain.LanguageKey = "GRIDLOTLIST";
            btnInterpretation.LanguageKey = "INTERPRETATION";
            tabMain.SetLanguageKey(tnpLotList, "TABCURRENTBYITEM");
            tabMain.SetLanguageKey(tabDefectMap, "TABDEFECTMAP");
            tabMain.SetLanguageKey(tabComparison, "TABCOMPARISON");
            tabMain.SetLanguageKey(tabRawData, "TABROWDATA");
        }

        #endregion

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {
            //! 해석 버튼 클릭 이벤트
            btnInterpretation.Click += (s, e) => ShowDefectMap();

            //! 첫번째 Tab에서만 '해석'버튼을 보여준다.
            tabMain.SelectedPageChanged += (s, e) => btnInterpretation.Visible = tabMain.SelectedTabPageIndex.Equals(0);

            //! Chart 마우스 클릭 이벤트
            chartMain.MouseClick += (s, e) =>
            {
                if (e.Button.Equals(MouseButtons.Right))
                {
                    ChartHitInfo hi = chartMain.CalcHitInfo(e.Location);
                    if (DefectMapHelper.IsNull(hi.SeriesPoint))
                    {
                        return;
                    }

                    _key = hi.SeriesPoint.Argument;
                    _equipmentType = (EquipmentType)hi.SeriesPoint.Tag;
                    chartMain.ContextMenu = new ContextMenu(new MenuItem[] { new MenuItem("Defect Map View", SetDefectMapAnalysis) });
                }
            };

            //! Grid Merge 이벤트
            grdMain.View.CellMerge += (s, e) =>
            {
                if (!(s is DevExpress.XtraGrid.Views.Grid.GridView view))
                {
                    return;
                }

                if (e.Column.FieldName.Equals("MODELID") || e.Column.FieldName.Equals("MODELVERSION"))
                {
                    string str1 = view.GetRowCellDisplayText(e.RowHandle1, e.Column);
                    string str2 = view.GetRowCellDisplayText(e.RowHandle2, e.Column);
                    e.Merge = str1 == str2;
                }
                else
                {
                    e.Merge = false;
                }

                e.Handled = true;
            };

            //! 조회조건에 P_PRODUCTDEFID가 존재시, SelectedPopup의 X버튼 클릭 이벤트
            if (Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID") is var control)
            {
                control.Properties.ButtonClick += (s, e) =>
                {
                    if ((s as DevExpress.XtraEditors.ButtonEdit).Properties.Buttons.IndexOf(e.Button).Equals(1))
                    {
                        Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").Text = string.Empty;
                    }
                };
            }
        }

        #region override Event

        /// <summary>
        /// 2019.10.09 배선용
        /// 재공조회에서 화면이동시 이벤트 처리
        /// </summary>
        /// <param name="parameters"></param>
        public async override void LoadForm(Dictionary<string, object> parameters)
        {
            base.LoadForm(parameters);

            if (!DefectMapHelper.IsNull(parameters))
            {
                Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(Format.GetString(parameters["PRODUCTDEFID"]));
                Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").Text = Format.GetString(parameters["PRODUCTREVISION"]);

                await OnSearchAsync();
            }
        }

        #endregion

        #endregion

        #region 검색

        /// <summary>
        /// 조회조건 검색 버튼 이벤트
        /// </summary>
        /// <returns></returns>
        protected async override Task OnSearchAsync()
        {
            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);
                btnInterpretation.IsBusy = true;
                this.tabMain.SelectedTabPageIndex = 0;
                SetClear();

                await base.OnSearchAsync();

                if (await SqlExecuter.QueryAsync("GetDefectMapRateByItem", "10001",
                                                 DefectMapHelper.AddLanguageTypeToConditions(Conditions.GetValues())) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData");
                        return;
                    }

                    DefectMapHelper.DrawingLineChartByItem(chartMain, dt);
                    grdMain.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                throw MessageException.Create(Format.GetString(ex));
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);
                btnInterpretation.IsBusy = false;
            }
        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // 최종 오픈 때 아래 코드로 교체
            //CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 0, false, Conditions);

            var productDefID = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetDefectMapProductList", "10001"))
                                         .SetLabel("PRODUCTDEFID")
                                         .SetPopupLayout("GRIDPRODUCTLIST", PopupButtonStyles.Ok_Cancel, true, false)
                                         .SetPopupLayoutForm(600, 400, FormBorderStyle.FixedToolWindow)
                                         .SetPosition(1)
                                         .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                         {
                                             selectedRow.ForEach(row =>
                                             {
                                                 Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").Text = DefectMapHelper.StringByDataRowObejct(row, "P_PRODUCTDEFVERSION");
                                             });
                                         });

            productDefID.Conditions.AddTextBox("P_PRODUCTDEFID").SetLabel("PRODUCTDEFID");

            productDefID.GridColumns.AddTextBoxColumn("P_PRODUCTDEFID", 120).SetLabel("PRODUCTDEFID");
            productDefID.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 150);
            productDefID.GridColumns.AddTextBoxColumn("P_PRODUCTDEFVERSION", 80).SetLabel("PRODUCTDEFVERSION");

            #region 품목명

            Conditions.AddTextBox("P_PRODUCTDEFVERSION").SetLabel("PRODUCTDEFVERSION").SetPosition(1.1);
            Conditions.GetCondition<ConditionItemTextBox>("P_PRODUCTDEFVERSION").SetIsReadOnly();

            #endregion
        }

        #endregion

        #region Private Function

        /// <summary>
        /// Chart에서 선택한 Point Sheet에서 선택하기
        /// </summary>
        private void SetDefectMapAnalysis(object sender, EventArgs e)
        {
            grdMain.View.ActiveFilterString = "";

            if (grdMain.View.GetRowHandleByValue("KEY", _key) is int rowHandle)
            {
                //grdMain.View.SetRowCellValue(rowHandle, grdMain.View.Columns[35].FieldName, true);
                //grdMain.View.CheckMarkSelection.InvertRowSelection(rowHandle); // check 유무만 
                //grdMain.View.CheckMarkSelection.InvertRowSelectionManually(rowHandle); // check 후 OnCheckStateChanged 이벤트 진행
                grdMain.View.CheckMarkSelection.ClearSelection();
                grdMain.View.CheckMarkSelection.SelectRow(rowHandle, true);
                grdMain.View.FocusedRowHandle = rowHandle;
                ShowDefectMap();
            }
        }

        /// <summary>
        /// Defect Map 보여주기 - 해석 버튼 클릭 이벤트
        /// </summary>
        private void ShowDefectMap()
        {
            try
            {
                if (grdMain.View.GetCheckedRows() is DataTable dt)
                {
                    if (dt.Rows.Count < 1)
                    {
                        ShowMessage("GridNoChecked");
                        ((ucDefectMapBasicForm)tabDefectMap.Controls[0]).SetClose();
                        ((ucTypeComparison)tabComparison.Controls[0]).SetClose();
                        return;
                    }

                    DialogManager.ShowWaitArea(this.pnlContent);
                    btnInterpretation.IsBusy = true;

                    Dictionary<string, object> value = Conditions.GetValues();
                    value["P_PRODUCTDEFID"] = DefectMapHelper.StringByDataRowObejct(dt.Rows[0], "MODELID");
                    value["P_PRODUCTDEFVERSION"] = DefectMapHelper.StringByDataRowObejct(dt.Rows[0], "MODELVERSION");
                    value.Add("P_LOTID", string.Join(",", dt.AsEnumerable().Select(x => x.Field<string>("LOTID")).ToList()));

                    if (SqlExecuter.Query("GetDefectMapList", "10002",
                                          DefectMapHelper.AddLanguageTypeToConditions(value)) is DataTable lotData)
                    {
                        ((ucDefectMapBasicForm)tabDefectMap.Controls[0]).SetData(lotData, _equipmentType, true);
                        ((ucTypeComparison)tabComparison.Controls[0]).SetData(lotData);
                        grdRowData.DataSource = lotData;
                    }

                    this.tabMain.SelectedTabPageIndex = 1;
                }
            }
            catch (Exception ex)
            {
                throw MessageException.Create(Format.GetString(ex));
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);
                btnInterpretation.IsBusy = false;
            }
        }

        /// <summary>
        /// Main Grid에 Layer에 Footer에 Summary 처리
        /// </summary>
        private void SetFooterSummary()
        {
            foreach (BandedGridColumn column in grdMain.View.Columns)
            {
                if (column.FieldName.Equals("MODELID") || column.FieldName.Equals("MODELVERSION") ||
                    column.FieldName.Equals("LOTID") || column.FieldName.Equals("EVENTTIME") ||
                    column.FieldName.Equals("AOITOTALRATE") || column.FieldName.Equals("HOLETOTALRATE") ||
                    column.FieldName.Equals("KEY") || column.FieldName.Equals("_INTERNAL_CHECKMARK_SELECTION_"))
                {
                    continue;
                }

                grdMain.View.Columns[column.FieldName].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
                grdMain.View.Columns[column.FieldName].SummaryItem.DisplayFormat = "{0:f2} %";
            }
        }

        /// <summary>
        /// 초기화
        /// </summary>
        private void SetClear()
        {
            if (!DefectMapHelper.IsNull(grdMain))
            {
                grdMain.DataSource = null;
            }

            if (!DefectMapHelper.IsNull(grdRowData))
            {
                grdRowData.DataSource = null;
            }

            chartMain.ClearSeries();

            ((ucDefectMapBasicForm)tabDefectMap.Controls[0]).SetClose();
            ((ucTypeComparison)tabComparison.Controls[0]).SetClose();
        }

        #endregion
    }
}
