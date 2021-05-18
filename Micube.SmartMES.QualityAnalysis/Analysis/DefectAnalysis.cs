#region using

using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;
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

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : ex> 시스템관리 > 코드 관리 > 코드그룹 정보
    /// 업  무  설  명  : ex> 시스템에서 공통으로 사용되는 코드그룹 정보를 관리한다.
    /// 생    성    자  : 홍길동
    /// 생    성    일  : 2019-05-14
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class DefectAnalysis : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        string _Text;                                       // 설명
        SmartBandedGrid grdList = new SmartBandedGrid();    // Main Grid

        /// <summary>
        /// 선택된 불량코드 DataTable
        /// </summary>
        private DataTable _DefectList;
        
        /// <summary>
        /// 조회 조건 품목에서 찾은 정보 Row
        /// </summary>
        private DataRow _selectedRow;

        private DataTable dtAreaEqp;

        private bool isDoubleClickAction = false;

        #endregion

        #region 생성자

        public DefectAnalysis()
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

            if (Conditions.GetCondition("P_PRODUCTDEFID").IsRequired == false)
            {
                //Conditions.GetCondition("P_PRODUCTDEFID").IsRequired = true;
                Conditions.GetCondition("P_PRODUCTDEFID").SetValidationIsRequired();
            }

            
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            #region 공정작업장 그리드

            grdSegmentByTapProcArea.GridButtonItem = GridButtonItem.None;
            grdSegmentByTapProcArea.View.SetIsReadOnly();
            
            grdSegmentByTapProcArea.View.AddTextBoxColumn("NO", 100);
            grdSegmentByTapProcArea.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 100);

            grdSegmentByTapProcArea.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdSegmentByTapProcArea.View.PopulateColumns();

            grdAreaEquipmentByTapProcArea.GridButtonItem = GridButtonItem.None;
            grdAreaEquipmentByTapProcArea.View.SetIsReadOnly();

            grdAreaEquipmentByTapProcArea.View.AddTextBoxColumn("AREANAME", 150);
            //grdAreaEquipmentByTapProcArea.View
            //    .AddCheckBoxColumn("EQPCHECK", 80)
            //    .SetLabel("")
            //    .SetDefault(false)
            //    .SetTextAlignment(TextAlignment.Center);
            grdAreaEquipmentByTapProcArea.View.AddTextBoxColumn("EQUIPMENTNAME", 150);
            grdAreaEquipmentByTapProcArea.View.AddTextBoxColumn("EQPLOTCOUNT", 150).SetLabel("LOTCNT")
                                              .SetTextAlignment(TextAlignment.Right)
                                              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdAreaEquipmentByTapProcArea.View.AddTextBoxColumn("WORKPCSQTY", 150).SetLabel("RESULTPCSQTY")
                                              .SetTextAlignment(TextAlignment.Right)
                                              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdAreaEquipmentByTapProcArea.View.AddTextBoxColumn("WORKPANELQTY", 150).SetLabel("PANELQTY")
                                              .SetTextAlignment(TextAlignment.Right)
                                              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdAreaEquipmentByTapProcArea.View.AddTextBoxColumn("DEFECTRATE", 150)
                                              .SetTextAlignment(TextAlignment.Center)
                                              .SetDisplayFormat("##0.#0 %", MaskTypes.Numeric);

            grdAreaEquipmentByTapProcArea.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdAreaEquipmentByTapProcArea.View.PopulateColumns();

            grdAreaEquipmentByTapProcArea.View.OptionsView.AllowCellMerge = true;

            #endregion 공정작업장 그리드

            #region 작업장 설비 그리드

            grdSegmentByTapAreaEqp.GridButtonItem = GridButtonItem.None;
            grdSegmentByTapAreaEqp.View.SetIsReadOnly();

            grdSegmentByTapAreaEqp.View.AddTextBoxColumn("NO", 100);
            grdSegmentByTapAreaEqp.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 100);

            grdSegmentByTapAreaEqp.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdSegmentByTapAreaEqp.View.PopulateColumns();

            grdAreaEquipmentByTapAreaEqp.GridButtonItem = GridButtonItem.None;
            grdAreaEquipmentByTapAreaEqp.View.SetIsReadOnly();

            grdAreaEquipmentByTapAreaEqp.View.AddTextBoxColumn("AREANAME", 150);
            //grdAreaEquipmentByTapAreaEqp.View
            //    .AddCheckBoxColumn("EQPCHECK", 80)
            //    .SetLabel("")
            //    .SetDefault(false)
            //    .SetTextAlignment(TextAlignment.Center);
            grdAreaEquipmentByTapAreaEqp.View.AddTextBoxColumn("EQUIPMENTNAME", 150);
            grdAreaEquipmentByTapAreaEqp.View.AddTextBoxColumn("EQPLOTCOUNT", 150).SetLabel("LOTCNT")
                                             .SetTextAlignment(TextAlignment.Right)
                                             .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdAreaEquipmentByTapAreaEqp.View.AddTextBoxColumn("WORKPCSQTY", 150).SetLabel("RESULTPCSQTY")
                                             .SetTextAlignment(TextAlignment.Right)
                                             .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdAreaEquipmentByTapAreaEqp.View.AddTextBoxColumn("WORKPANELQTY", 150).SetLabel("PANELQTY")
                                             .SetTextAlignment(TextAlignment.Right)
                                             .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdAreaEquipmentByTapAreaEqp.View.AddTextBoxColumn("DEFECTRATE", 150)
                                             .SetTextAlignment(TextAlignment.Center)
                                             .SetDisplayFormat("##0.#0 %", MaskTypes.Numeric);

            grdAreaEquipmentByTapAreaEqp.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdAreaEquipmentByTapAreaEqp.View.PopulateColumns();

            grdAreaEquipmentByTapAreaEqp.View.OptionsView.AllowCellMerge = true;

            #endregion 작업장 설비 그리드

            #region 설비 LOT 그리드

            grdSegmentByTapEqpLOT.GridButtonItem = GridButtonItem.None;
            grdSegmentByTapEqpLOT.View.SetIsReadOnly();

            grdSegmentByTapEqpLOT.View.AddTextBoxColumn("NO", 100);
            grdSegmentByTapEqpLOT.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 100);

            grdSegmentByTapEqpLOT.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdSegmentByTapEqpLOT.View.PopulateColumns();

            grdAreaEquipmentByTapEqpLOT.GridButtonItem = GridButtonItem.None;
            grdAreaEquipmentByTapEqpLOT.View.SetIsReadOnly();

            grdAreaEquipmentByTapEqpLOT.View.AddTextBoxColumn("AREANAME", 150);
            //grdAreaEquipmentByTapEqpLOT.View
            //    .AddCheckBoxColumn("EQPCHECK", 80)
            //    .SetDefault(false)
            //    .SetLabel("")
            //    .SetTextAlignment(TextAlignment.Center);
            grdAreaEquipmentByTapEqpLOT.View.AddTextBoxColumn("EQUIPMENTNAME", 150);
            grdAreaEquipmentByTapEqpLOT.View.AddTextBoxColumn("EQPLOTCOUNT", 150).SetLabel("LOTCNT")
                                            .SetTextAlignment(TextAlignment.Right)
                                            .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdAreaEquipmentByTapEqpLOT.View.AddTextBoxColumn("WORKPCSQTY", 150).SetLabel("RESULTPCSQTY")
                                            .SetTextAlignment(TextAlignment.Right)
                                            .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdAreaEquipmentByTapEqpLOT.View.AddTextBoxColumn("WORKPANELQTY", 150).SetLabel("PANELQTY")
                                            .SetTextAlignment(TextAlignment.Right)
                                            .SetDisplayFormat("#,##0", MaskTypes.Numeric); ;
            grdAreaEquipmentByTapEqpLOT.View.AddTextBoxColumn("DEFECTRATE", 150)
                                            .SetTextAlignment(TextAlignment.Center)
                                            .SetDisplayFormat("##0.#0 %", MaskTypes.Numeric); ;

            grdAreaEquipmentByTapEqpLOT.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            //grdAreaEquipmentByTapEqpLOT.View.AddCheckBoxColumn("EQPCHECK").SetLabel("");

            grdAreaEquipmentByTapEqpLOT.View.PopulateColumns();

            grdAreaEquipmentByTapEqpLOT.View.OptionsView.AllowCellMerge = true;

            #endregion 설비 LOT 그리드
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            #region 공정-작업장

            grdAreaEquipmentByTapProcArea.View.DoubleClick += View_DoubleClick;

            grdAreaEquipmentByTapProcArea.View.CellMerge += grdAreaEquipment1_CellMerge;
            grdAreaEquipmentByTapProcArea.View.CellValueChanged += grdAreaView_CellValueChanged;
            grdAreaEquipmentByTapProcArea.View.CustomRowFilter += grdAreaEqpView_CustomRowFilter;

            grdSegmentByTapProcArea.View.CheckStateChanged += View1_CheckStateChanged;

            gbProcessArea1.ExpandEvent += (s, e) =>
            {
                this.splitProcAreaBodyChart.PanelVisibility = SplitPanelVisibility.Panel1;
                this.splitBodyMain.PanelVisibility = SplitPanelVisibility.Panel2;
            };

            gbProcessArea1.RestoreEvent += (s, e) =>
            {
                this.splitProcAreaBodyChart.PanelVisibility = SplitPanelVisibility.Both;
                this.splitBodyMain.PanelVisibility = SplitPanelVisibility.Both;
            };

            gbProcessArea2.ExpandEvent += (s, e) =>
            {
                this.splitProcAreaBodyChart.PanelVisibility = SplitPanelVisibility.Panel2;
                this.splitBodyMain.PanelVisibility = SplitPanelVisibility.Panel2;
            };
            gbProcessArea2.RestoreEvent += (s, e) =>
            {
                this.splitProcAreaBodyChart.PanelVisibility = SplitPanelVisibility.Both;
                this.splitBodyMain.PanelVisibility = SplitPanelVisibility.Both;
            };

            chartProcessAreaInTapProcArea.MouseDoubleClick += ChartProcessAreaInTapProcArea_MouseDoubleClick;
            #endregion 공정-작업장

            #region 작업장-설비

            grdAreaEquipmentByTapAreaEqp.View.DoubleClick += View_DoubleClick;

            grdAreaEquipmentByTapAreaEqp.View.CellMerge += grdAreaEquipment2_CellMerge;
            grdAreaEquipmentByTapAreaEqp.View.CellValueChanged += grdAreaView_CellValueChanged;
            grdAreaEquipmentByTapAreaEqp.View.CustomRowFilter += grdAreaEqpView_CustomRowFilter;

            grdSegmentByTapAreaEqp.View.CheckStateChanged += View2_CheckStateChanged;

            gbAreaEqp1.ExpandEvent += (s, e) =>
            {
                this.splitAreaEquipBodyChart.PanelVisibility = SplitPanelVisibility.Panel1;
                this.spltAreaEqpBody.PanelVisibility = SplitPanelVisibility.Panel2;
            };

            gbAreaEqp1.RestoreEvent += (s, e) =>
            {
                this.splitAreaEquipBodyChart.PanelVisibility = SplitPanelVisibility.Both;
                this.spltAreaEqpBody.PanelVisibility = SplitPanelVisibility.Both;
            };

            gbAreaEqp2.ExpandEvent += (s, e) =>
            {
                this.splitAreaEquipBodyChart.PanelVisibility = SplitPanelVisibility.Panel2;
                this.spltAreaEqpBody.PanelVisibility = SplitPanelVisibility.Panel2;
            };
            gbAreaEqp2.RestoreEvent += (s, e) =>
            {
                this.splitAreaEquipBodyChart.PanelVisibility = SplitPanelVisibility.Both;
                this.spltAreaEqpBody.PanelVisibility = SplitPanelVisibility.Both;
            };

            chartProcessAreaInTapAreaEqp.MouseDoubleClick += ChartProcessAreaInTapAreaEqp_MouseDoubleClick;

            #endregion 작업장-설비

            #region 설비-LOT

            grdAreaEquipmentByTapEqpLOT.View.DoubleClick += View_DoubleClick;

            grdAreaEquipmentByTapEqpLOT.View.CellMerge += grdAreaEquipment3_CellMerge;
            grdAreaEquipmentByTapEqpLOT.View.CellValueChanged += grdAreaView_CellValueChanged;
            grdAreaEquipmentByTapEqpLOT.View.CustomRowFilter += grdAreaEqpView_CustomRowFilter;

            grdSegmentByTapEqpLOT.View.CheckStateChanged += View3_CheckStateChanged;

            gbEqpLOT.ExpandEvent += (s, e) =>
            {
                this.splitEqpLOTMainBody.PanelVisibility = SplitPanelVisibility.Panel2;
            };

            gbEqpLOT.RestoreEvent += (s, e) =>
            {
                this.splitEqpLOTMainBody.PanelVisibility = SplitPanelVisibility.Both;
            };

            #endregion 설비-LOT

            #region 각 탭별 조회 버튼 이벤트

            this.btnLOTViewProcArea.Click += BtnLOTView_Click;
            this.btnLOTViewAreaEqp.Click += BtnLOTView_Click;
            this.btnLOTViewEqpLot.Click += BtnLOTView_Click;

            #endregion

            tabCtrlDefectAnalysis.SelectedPageChanged += TabCtrlDefectAnalysis_SelectedPageChanged;
        }

        private void TabCtrlDefectAnalysis_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if(isDoubleClickAction)
            {
                if(tabCtrlDefectAnalysis.SelectedTabPageIndex == 1)
                    btnSearchAreaEqpChart.PerformClick();
                else if(tabCtrlDefectAnalysis.SelectedTabPageIndex == 2)
                    btnSearchEqpLOTChart.PerformClick();
            }

            isDoubleClickAction = false;
        }

        private void ChartProcessAreaInTapAreaEqp_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ChartControl chart = sender as ChartControl;

            ChartHitInfo hi = chart.CalcHitInfo(e.Location);
            SeriesPoint pt = hi.SeriesPoint;

            if (!string.IsNullOrEmpty(pt.Argument))
            {
                if (tabCtrlDefectAnalysis.SelectedTabPageIndex == 1)
                {
                    grdAreaEquipmentByTapEqpLOT.DataSource = grdAreaEquipmentByTapAreaEqp.DataSource;
                    grdSegmentByTapEqpLOT.DataSource = grdSegmentByTapAreaEqp.DataSource;

                    grdAreaEquipmentByTapEqpLOT.View.BestFitColumns();
                    grdSegmentByTapEqpLOT.View.BestFitColumns();

                    List<int> lstProcCheck = grdSegmentByTapAreaEqp.View.GetCheckedRowsHandle().ToList();
                    foreach (int i in lstProcCheck)
                    {
                        grdSegmentByTapEqpLOT.View.SetRowCellValue(i, "_INTERNAL_CHECKMARK_SELECTION_", true);
                    }

                    //grdAreaEquipmentByTapEqpLOT.View.SelectRow(info.RowHandle + 1);
                    grdAreaEquipmentByTapEqpLOT.View.SelectRow(1);
                    isDoubleClickAction = true;

                    tabCtrlDefectAnalysis.SelectedTabPageIndex = 2;
                }
            }
        }

        private void ChartProcessAreaInTapProcArea_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ChartControl chart = sender as ChartControl;
            
            ChartHitInfo hi = chart.CalcHitInfo(e.Location);
            SeriesPoint pt = hi.SeriesPoint;

            if (!string.IsNullOrEmpty(pt.Argument))
            {
                if (tabCtrlDefectAnalysis.SelectedTabPageIndex == 0)
                {
                    grdAreaEquipmentByTapAreaEqp.DataSource = grdAreaEquipmentByTapProcArea.DataSource;
                    grdSegmentByTapAreaEqp.DataSource = grdSegmentByTapProcArea.DataSource;

                    grdAreaEquipmentByTapAreaEqp.View.BestFitColumns();
                    grdSegmentByTapAreaEqp.View.BestFitColumns();

                    List<int> lstProcCheck = grdSegmentByTapProcArea.View.GetCheckedRowsHandle().ToList();
                    foreach (int i in lstProcCheck)
                    {
                        grdSegmentByTapAreaEqp.View.SetRowCellValue(i, "_INTERNAL_CHECKMARK_SELECTION_", true);
                    }

                    // 작업장 - 설비 그리드 체크 모두 해제
                    grdAreaEquipmentByTapProcArea.View.UncheckedAll();

                    for (int ih = 0; ih < grdAreaEquipmentByTapProcArea.View.RowCount; ih++)
                    {
                        string compObj = grdAreaEquipmentByTapProcArea.View.GetRowCellValue(ih, "AREANAME").ToString();
                        if (compObj.Equals(pt.Argument))
                            grdAreaEquipmentByTapAreaEqp.View.SetRowCellValue(ih, "_INTERNAL_CHECKMARK_SELECTION_", true);
                    }

                    List<int> lstAreaCheck = grdAreaEquipmentByTapProcArea.View.GetCheckedRowsHandle().ToList();
                    foreach (int i in lstAreaCheck)
                    {
                        grdAreaEquipmentByTapAreaEqp.View.SetRowCellValue(i, "_INTERNAL_CHECKMARK_SELECTION_", true);
                    }

                    isDoubleClickAction = true;
                    tabCtrlDefectAnalysis.SelectedTabPageIndex = 1;
                }
            }
        }

        private void BtnLOTView_Click(object sender, EventArgs e)
        {
            LotViewPopup popup = new LotViewPopup();

            popup.StartPosition = FormStartPosition.CenterParent;

            popup.Conditions = Conditions.GetValues();

            popup.ShowDialog();
            if (popup.DialogResult == DialogResult.OK)
            {
                DataTable dt = popup._checkTable;
                //popup.Conditions;

                OpenMenu("PG-QC-0554", popup.Conditions);
            }
        }

        private void GbProcessArea_ExpandEvent(object sender, EventArgs e)
        {
            SmartGroupBox sgb = sender as SmartGroupBox;

            //this.splitBodyMain.PanelVisibility = SplitPanelVisibility.Panel1;

            this.splitProcAreaBodyChart.PanelVisibility = SplitPanelVisibility.Panel1;

            //this.splMainPanel1.PanelVisibility = SplitPanelVisibility.Panel1;
            //this.splMain.PanelVisibility = SplitPanelVisibility.Panel1;
            
        }

        private void View_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(ea.Location);
            
            if (info.InRow || info.InRowCell)
            {
                string colField = info.Column == null ? "N/A" : info.Column.FieldName;
                if (colField.Equals("AREANAME"))
                {
                    string strAreaValue = view.GetRowCellValue(info.RowHandle, "AREANAME").ToString(); 
                    if (tabCtrlDefectAnalysis.SelectedTabPageIndex == 0)
                    {
                        grdAreaEquipmentByTapAreaEqp.DataSource = grdAreaEquipmentByTapProcArea.DataSource;
                        grdSegmentByTapAreaEqp.DataSource = grdSegmentByTapProcArea.DataSource;

                        grdAreaEquipmentByTapAreaEqp.View.BestFitColumns();
                        grdSegmentByTapAreaEqp.View.BestFitColumns();

                        List<int> lstProcCheck = grdSegmentByTapProcArea.View.GetCheckedRowsHandle().ToList();
                        foreach (int i in lstProcCheck)
                        {
                            grdSegmentByTapAreaEqp.View.SetRowCellValue(i, "_INTERNAL_CHECKMARK_SELECTION_", true);
                        }

                        // 작업장 - 설비 그리드 체크 모두 해제
                        grdAreaEquipmentByTapProcArea.View.UncheckedAll();

                        for (int ih = 0; ih < grdAreaEquipmentByTapProcArea.View.RowCount; ih++)
                        {
                            string compObj = grdAreaEquipmentByTapProcArea.View.GetRowCellValue(ih, "AREANAME").ToString();
                            if(compObj.Equals(strAreaValue))
                                grdAreaEquipmentByTapAreaEqp.View.SetRowCellValue(ih, "_INTERNAL_CHECKMARK_SELECTION_", true);
                        }

                        List<int> lstAreaCheck = grdAreaEquipmentByTapProcArea.View.GetCheckedRowsHandle().ToList();
                        foreach (int i in lstAreaCheck)
                        {
                            grdAreaEquipmentByTapAreaEqp.View.SetRowCellValue(i, "_INTERNAL_CHECKMARK_SELECTION_", true);
                        }

                        isDoubleClickAction = true;
                        tabCtrlDefectAnalysis.SelectedTabPageIndex = 1;
                    }
                    else if (tabCtrlDefectAnalysis.SelectedTabPageIndex == 2)
                    {
                        tabCtrlDefectAnalysis.SelectedTabPageIndex = 1;
                        grdAreaEquipmentByTapAreaEqp.DataSource = grdAreaEquipmentByTapEqpLOT.DataSource;
                        grdSegmentByTapAreaEqp.DataSource = grdSegmentByTapEqpLOT.DataSource;

                        grdAreaEquipmentByTapAreaEqp.View.BestFitColumns();
                        grdSegmentByTapAreaEqp.View.BestFitColumns();

                        List<int> lstProcCheck = grdSegmentByTapEqpLOT.View.GetCheckedRowsHandle().ToList();
                        foreach (int i in lstProcCheck)
                        {
                            grdSegmentByTapAreaEqp.View.SetRowCellValue(i, "_INTERNAL_CHECKMARK_SELECTION_", true);
                        }

                        // 작업장 - 설비 그리드 체크 모두 해제
                        grdAreaEquipmentByTapProcArea.View.UncheckedAll();

                        for (int ih = 0; ih < grdAreaEquipmentByTapProcArea.View.RowCount; ih++)
                        {
                            string compObj = grdAreaEquipmentByTapProcArea.View.GetRowCellValue(ih, "AREANAME").ToString();
                            if (compObj.Equals(strAreaValue))
                                grdAreaEquipmentByTapAreaEqp.View.SetRowCellValue(ih, "_INTERNAL_CHECKMARK_SELECTION_", true);
                        }

                        List<int> lstAreaCheck = grdAreaEquipmentByTapProcArea.View.GetCheckedRowsHandle().ToList();
                        foreach (int i in lstAreaCheck)
                        {
                            grdAreaEquipmentByTapAreaEqp.View.SetRowCellValue(i, "_INTERNAL_CHECKMARK_SELECTION_", true);
                        }

                        isDoubleClickAction = true;

                        tabCtrlDefectAnalysis.SelectedTabPageIndex = 1;
                        //btnSearchAreaEqpChart.PerformClick();
                    }
                }
                else if (colField.Equals("EQUIPMENTNAME"))
                {
                    if (tabCtrlDefectAnalysis.SelectedTabPageIndex == 1)
                    {
                        grdAreaEquipmentByTapEqpLOT.DataSource = grdAreaEquipmentByTapAreaEqp.DataSource;
                        grdSegmentByTapEqpLOT.DataSource = grdSegmentByTapAreaEqp.DataSource;

                        grdAreaEquipmentByTapEqpLOT.View.BestFitColumns();
                        grdSegmentByTapEqpLOT.View.BestFitColumns();

                        List<int> lstProcCheck = grdSegmentByTapAreaEqp.View.GetCheckedRowsHandle().ToList();
                        foreach (int i in lstProcCheck)
                        {
                            grdSegmentByTapEqpLOT.View.SetRowCellValue(i, "_INTERNAL_CHECKMARK_SELECTION_", true);
                        }

                        grdAreaEquipmentByTapEqpLOT.View.SelectRow(info.RowHandle+1);
                        //List<int> lstAreaCheck = grdAreaEquipmentByTapAreaEqp.View.GetCheckedRowsHandle().ToList();
                        //foreach (int i in lstAreaCheck)
                        //{
                        //    grdAreaEquipmentByTapEqpLOT.View.SetRowCellValue(i, "_INTERNAL_CHECKMARK_SELECTION_", true);
                        //}

                        isDoubleClickAction = true;

                        tabCtrlDefectAnalysis.SelectedTabPageIndex = 2;

                        //btnSearchEqpLOTChart.PerformClick();
                    }
                }
                    

                //MessageBox.Show(string.Format("DoubleClick on row: {0}, column: {1}.", info.RowHandle, colCaption));
            }
            //grdAreaEquipmentByTapProcArea;
            //grdSegmentByTapProcArea;
        }

        private void View1_CheckStateChanged(object sender, EventArgs e)
        {
            DataView dv = dtAreaEqp.DefaultView;
            DataTable dt = grdSegmentByTapProcArea.View.GetCheckedRows();

            if (dt.Rows.Count > 0)
            {
                List<string> lstSeqNo = dt.AsEnumerable().Select(g => g.Field<string>("NO")).ToList<string>();
                string joined = "'" + string.Join("','", lstSeqNo) + "'";
                string strFilter = string.Format("NO IN ({0})", joined);

                dv.RowFilter = strFilter;

                grdAreaEquipmentByTapProcArea.DataSource = dv.ToTable();
            }
            else
                grdAreaEquipmentByTapProcArea.View.ClearDatas();

            grdAreaEquipmentByTapProcArea.View.RefreshData();
            grdAreaEquipmentByTapProcArea.View.BestFitColumns();
        }

        private void View2_CheckStateChanged(object sender, EventArgs e)
        {
            DataView dv = dtAreaEqp.DefaultView;
            DataTable dt = grdSegmentByTapAreaEqp.View.GetCheckedRows();

            if (dt.Rows.Count > 0)
            {
                List<string> lstSeqNo = dt.AsEnumerable().Select(g => g.Field<string>("NO")).ToList<string>();
                string joined = "'" + string.Join("','", lstSeqNo) + "'";
                string strFilter = string.Format("NO IN ({0})", joined);

                dv.RowFilter = strFilter;

                grdAreaEquipmentByTapAreaEqp.DataSource = dv.ToTable();
            }
            else
                grdAreaEquipmentByTapAreaEqp.View.ClearDatas();

            grdAreaEquipmentByTapAreaEqp.View.RefreshData();
            grdAreaEquipmentByTapAreaEqp.View.BestFitColumns();
        }

        private void View3_CheckStateChanged(object sender, EventArgs e)
        {
            DataView dv = dtAreaEqp.DefaultView;
            DataTable dt = grdSegmentByTapEqpLOT.View.GetCheckedRows();

            if (dt.Rows.Count > 0)
            {
                List<string> lstSeqNo = dt.AsEnumerable().Select(g => g.Field<string>("NO")).ToList<string>();
                string joined = "'" + string.Join("','", lstSeqNo) + "'";
                string strFilter = string.Format("NO IN ({0})", joined);

                dv.RowFilter = strFilter;

                grdAreaEquipmentByTapEqpLOT.DataSource = dv.ToTable();
            }
            else
                grdAreaEquipmentByTapEqpLOT.View.ClearDatas();

            grdAreaEquipmentByTapEqpLOT.View.RefreshData();
            grdAreaEquipmentByTapEqpLOT.View.BestFitColumns();
        }

        private void grdAreaEqpView_CustomRowFilter(object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
        {
            SmartBandedGrid grid;
            if (this.tabCtrlDefectAnalysis.SelectedTabPage == tabProcArea)
            {
                grid = grdAreaEquipmentByTapProcArea;
            }
            else if (this.tabCtrlDefectAnalysis.SelectedTabPage == tabAreaEqp)
            {
                grid = grdAreaEquipmentByTapAreaEqp;
            }
            else
            {
                grid = grdAreaEquipmentByTapAreaEqp;
            }

            SmartBandedGridView view = grid.View as SmartBandedGridView;
            int[] rowHandles = view.GetSelectedRows();
        }


        private void grdAreaView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow row = grdAreaEquipmentByTapProcArea.View.GetFocusedDataRow();

            SmartBandedGrid grid;
            if (this.tabCtrlDefectAnalysis.SelectedTabPage == tabProcArea)
            {
                grid = grdAreaEquipmentByTapProcArea;
            }
            else if (this.tabCtrlDefectAnalysis.SelectedTabPage == tabAreaEqp)
            {
                grid = grdAreaEquipmentByTapAreaEqp;
            }
            else
            {
                grid = grdAreaEquipmentByTapAreaEqp;
            }

            grid.View.CellValueChanged -= grdAreaView_CellValueChanged;

            grid.View.CellValueChanged += grdAreaView_CellValueChanged;

                //string area = Format.GetString(row["AREANAME"]);
                //throw MessageException.Create("NoMatchingAreaUser", area);// 작업장에 대한 권한이 없습니다.

        }

        #region Grid Cell Merge Event

        private void grdAreaEquipment1_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            if (e.Column.FieldName.Equals("AREANAME"))
            {
                var dr1 = grdAreaEquipmentByTapProcArea.View.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = grdAreaEquipmentByTapProcArea.View.GetDataRow(e.RowHandle2); //아래 행 정보

                //비교하는 이유 그래야 정상적으로 나옴.
                e.Merge = dr1["AREANAME"].ToString().Equals(dr2["AREANAME"].ToString());
            }
            else
                e.Merge = false;

            e.Handled = true;
        }

        private void grdAreaEquipment2_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            if (e.Column.FieldName.Equals("AREANAME"))
            {
                var dr1 = grdAreaEquipmentByTapAreaEqp.View.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = grdAreaEquipmentByTapAreaEqp.View.GetDataRow(e.RowHandle2); //아래 행 정보

                //비교하는 이유 그래야 정상적으로 나옴.
                e.Merge = dr1["AREANAME"].ToString().Equals(dr2["AREANAME"].ToString());
            }
            else
                e.Merge = false;

            e.Handled = true;
        }

        private void grdAreaEquipment3_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            if (e.Column.FieldName.Equals("AREANAME"))
            {
                var dr1 = grdAreaEquipmentByTapEqpLOT.View.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = grdAreaEquipmentByTapEqpLOT.View.GetDataRow(e.RowHandle2); //아래 행 정보

                //비교하는 이유 그래야 정상적으로 나옴.
                e.Merge = dr1["AREANAME"].ToString().Equals(dr2["AREANAME"].ToString());
            }
            else
                e.Merge = false;

            e.Handled = true;
        }

        #endregion

        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            DataTable changed = grdList.GetChangedRows();

            ExecuteRule("SaveCodeClass", changed);
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
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataRow dr = _selectedRow;
            if(dr != null)
                values.Add("PRODUCTDEFIDVERSION", dr["PRODUCTDEFIDVERSION"].ToString());

            string qryYPE = string.Empty;
            if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
                qryYPE = "YPE";

            if (this.tabCtrlDefectAnalysis.SelectedTabPage == tabProcArea)
            {
                grdAreaEquipmentByTapProcArea.View.ClearDatas();

                chartProcessAreaInTapProcArea.Series.Clear();
                chartTimeAreaSeriesInTapProcArea.Series.Clear();

                if (await SqlExecuter.QueryAsync("GetProcessSegmentByDefectAnalysis", "10001", values) is DataTable dt2)
                {
                    grdSegmentByTapProcArea.DataSource = dt2;
                    grdSegmentByTapProcArea.View.RefreshData();
                    grdSegmentByTapProcArea.View.BestFitColumns();
                }

                if (await SqlExecuter.QueryAsync("GetAreaEquipmentByDefectAnalysis" + qryYPE, "10001", values) is DataTable dt3)
                {
                    dtAreaEqp = dt3;
                    grdAreaEquipmentByTapProcArea.View.RefreshData();
                    grdAreaEquipmentByTapProcArea.View.BestFitColumns();
                    //DataTable dt = grdAreaEquipmentByTapProcArea.View.GetCheckedRows();
                }
            }
            else if (this.tabCtrlDefectAnalysis.SelectedTabPage == tabAreaEqp)
            {
                grdAreaEquipmentByTapAreaEqp.View.ClearDatas();

                chartProcessAreaInTapAreaEqp.Series.Clear();
                chartTimeAreaSeriesInTapAreaEqp.Series.Clear();

                if (await SqlExecuter.QueryAsync("GetProcessSegmentByDefectAnalysis", "10001", values) is DataTable dt2)
                {
                    grdSegmentByTapAreaEqp.DataSource = dt2;
                    grdSegmentByTapAreaEqp.View.BestFitColumns();
                }

                if (await SqlExecuter.QueryAsync("GetAreaEquipmentByDefectAnalysis" + qryYPE, "10001", values) is DataTable dt3)
                {
                    dtAreaEqp = dt3;
                    grdAreaEquipmentByTapAreaEqp.View.RefreshData();
                    grdAreaEquipmentByTapAreaEqp.View.BestFitColumns();
                    //grdAreaEquipmentByTapAreaEqp.DataSource = dt3;
                }
            }
            else
            {
                grdAreaEquipmentByTapEqpLOT.View.ClearDatas();

                chartTimeAreaSeriesInTapEqpLOT.Series.Clear();

                if (await SqlExecuter.QueryAsync("GetProcessSegmentByDefectAnalysis", "10001", values) is DataTable dt2)
                {
                    grdSegmentByTapEqpLOT.DataSource = dt2;
                    grdSegmentByTapEqpLOT.View.BestFitColumns();
                }

                if (await SqlExecuter.QueryAsync("GetAreaEquipmentByDefectAnalysis" + qryYPE, "10001", values) is DataTable dt3)
                {
                    dtAreaEqp = dt3;
                    grdAreaEquipmentByTapEqpLOT.View.RefreshData();
                    grdAreaEquipmentByTapEqpLOT.View.BestFitColumns();
                    //grdAreaEquipmentByTapEqpLOT.DataSource = dt3;
                }
            }

            //if (dtCodeClass.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            //{
            //    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            //}
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            //base.InitializeCondition();

            // 초기 Tab 선택
            this.tabCtrlDefectAnalysis.SelectedTabPageIndex = 0;

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            #region 품목 

            var productDefID = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductListByYieldStatus", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFIDVERSION", "PRODUCTDEFIDVERSION")
                                         .SetLabel("PRODUCTDEFID")
                                         .SetPopupLayout("GRIDPRODUCTLIST", PopupButtonStyles.Ok_Cancel, true, false)
                                         .SetPopupLayoutForm(600, 400, FormBorderStyle.FixedToolWindow)
                                         .SetPosition(1.1)
                                         .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                         {
                                             foreach (DataRow row in selectedRow)
                                             {
                                                 SetCondition(row);
                                             }
                                         });

            SmartTextBox tbProductID = Conditions.GetControl<SmartTextBox>("PRODUCTDEFID");

            productDefID.Conditions.AddTextBox("PRODUCTDEFID")
                                   .SetLabel("PRODUCTDEFID");

            //productDefID.Conditions.GetControl<SmartTextBox>("PRODUCTDEFID").EditValue = tbProductID.EditValue;

            productDefID.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 120)
                                    .SetLabel("PRODUCTDEFID");

            productDefID.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 250)
                                    .SetLabel("PRODUCTDEFNAME");

            productDefID.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100)
                                    .SetLabel("PRODUCTDEFVERSION");


            #endregion

            #region 품목명

            Conditions.GetCondition<ConditionItemTextBox>("P_PRODUCTNAME").SetIsReadOnly();

            #endregion

            #region 불량선택
            // DefectSelectPopup
            //DefectSelectPopup defectPopup = new DefectSelectPopup();
            //var defectCodePopup = Conditions.AddSelectPopup("P_DEFECTCODE", (ISmartCustomPopup)defectPopup, "DEFECTSEGNAME", "DEFECTSEGNAME")
            //     .SetPopupLayoutForm(500, 600)
            //     .SetLabel("DEFECTCODE")
            //     .SetPopupCustomParameter((popup, dataRow) =>
            //     {
            //         (popup as DefectSelectPopup).SelectedDefectHandlerEvent += (dt) => { _DefectList = dt; };
            //     });
            ;


            var defectCodePopup = Conditions.AddSelectPopup("P_DEFECTCODE", new SqlQuery("GetDefectCodeByYieldStatus", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "DEFECTSEGNAME", "DEFECTSEGID")
               .SetPopupLayout("DEFECTCODE", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("DEFECTCODE")
               .SetPopupResultCount(1)
               .SetPosition(5.1)
               .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
               {

                   //foreach (DataRow row in selectedRow)
                   //{
                   //    Conditions.GetControl<SmartSelectPopupEdit>("P_DEFECTCODE").SetValue(row["DEFECTSEGNAME"].ToString());
                   //    Conditions.GetControl<SmartTextBox>("P_DEFECTNAME").EditValue = row["DEFECTSEGNAME"].ToString();
                   //}

                   #region Commented

                   //string codeName = "";
                   //string codeID = "";

                   //selectedRow.Cast<DataRow>().ForEach(row =>
                   //{
                   //    codeName += row["DEFECTNAME"].ToString() + ",";
                   //    codeID += row["DEFECTCODE"].ToString() + ",";

                   //});
                   //codeName = codeName.TrimEnd(',');
                   //codeID = codeID.TrimEnd(',');
                   //Conditions.GetControl<SmartSelectPopupEdit>("DEFECTCODE").SetValue(codeName);
                   //Conditions.GetControl<SmartTextBox>("DEFECTNAME").Text = codeName;

                   #endregion
               });

            // 팝업 조회조건
            defectCodePopup.Conditions.AddTextBox("P_DEFECTCODE").SetLabel("DEFECTNAME");

            // 팝업 그리드
            defectCodePopup.GridColumns.AddTextBoxColumn("DEFECTNAME", 150);
            defectCodePopup.GridColumns.AddTextBoxColumn("QCSEGMENT", 200);


            #endregion

            #region 불량명

            //var defectName = Conditions.AddTextBox("P_DEFECTNAME").SetLabel("DEFECTNAME").SetPosition(5.2).SetIsReadOnly();

            #endregion
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
            grdList.View.CheckValidation();

            DataTable changed = grdList.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        private void SetCondition(DataRow dr)
        {

            if (!DefectMapHelper.IsNull(dr.GetObject("DEFECTCODE")))
            {
                Conditions.GetControl<SmartSelectPopupEdit>("P_DEFECTCODE").SetValue(dr["DEFECTCODE"]);

                //Conditions.GetControl<SmartTextBox>("DEFECTNAME").Text = dr["DEFECTNAME"].ToString();
            }
            else
            {
                Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(dr["PRODUCTDEFID"]);

                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = dr["PRODUCTDEFNAME"].ToString();
            }
            _selectedRow = dr;
        }

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 마스터 정보를 조회한다.
        /// </summary>
        private void LoadData()
        {

        }

        #endregion

        public override void LoadForm(Dictionary<string, object> parameters)
        {
            base.LoadForm(parameters);

            if (parameters != null)
            {
                Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(parameters["P_PRODUCTDEFID"].ToString());
                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = parameters["P_PRODUCTNAME"].ToString();
                Conditions.GetControl<SmartComboBox>("P_PLANTID").EditValue = parameters["P_PLANTID"].ToString();
                SmartPeriodEdit period = Conditions.GetControl<SmartPeriodEdit>("P_PERIOD");
                period.datePeriodFr.EditValue = parameters["P_PERIOD_PERIODFR"];
                period.datePeriodTo.EditValue = parameters["P_PERIOD_PERIODTO"];

                //if (parameters.ContainsKey("P_LOTS"))
                //{
                //    ;
                //    //arrLot = parameters["P_LOTS"].ToString().Split(',');
                //    //setLOTComboData(arrLot);
                //}

                OnSearchAsync();
            }
        }

        private void SearchProcData()
        {
            this.OnSearchAsync();
        }

        public void SetChartData(DataTable dt1, DataTable dt2, string strChartType)
        {
            if (dt1 == null || dt1.Rows.Count == 0) return;

            DataTable chartData = new DataTable("ChartData");
            chartData.Columns.Add("SeriesData", typeof(string));
            chartData.Columns.Add("ArgumentData", typeof(string));
            chartData.Columns.Add("ValueData", typeof(double));

            if (strChartType.Equals("1"))
            {
                //chartProcessAreaInTapProcArea.Series.Clear();
                chartTimeAreaSeriesInTapProcArea.Series.Clear();

                //chartProcessAreaInTapProcArea.SeriesSelectionMode = SeriesSelectionMode.Point;
                //chartProcessAreaInTapProcArea.SelectionMode = ElementSelectionMode.Single;

                //// Specify the crosshair group header pattern. 
                //chartProcessAreaInTapProcArea.CrosshairOptions.GroupHeaderPattern = "{A}";

                ////chartProcessAreaInTapProcArea.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.True;

                #region 공정 시계열 - 작업장 불량 박스 플롯 차트

                this.chartProcessAreaInTapProcArea.Series.Clear();

                this.chartProcessAreaInTapProcArea.Series.Add(new Series("BoxPlot", ViewType.CandleStick));
                this.chartProcessAreaInTapProcArea.Series.Add(new Series("Median", ViewType.Point));
                this.chartProcessAreaInTapProcArea.Series.Add(new Series("Average", ViewType.Point));

                this.chartProcessAreaInTapProcArea.Series["BoxPlot"].Visible = false;
                this.chartProcessAreaInTapProcArea.Series["Median"].Visible = false;
                this.chartProcessAreaInTapProcArea.Series["Average"].Visible = false;
                YieldParameter parPlotData = YieldParameter.Create();


                var boxBase = dt2.AsEnumerable();
                if (boxBase.Count() > 0)
                {
                    var rowSrc = from dr in boxBase
                                 select new
                                 {
                                     SAMPLING = dr.Field<string>("AREAID"),
                                     SUBGROUP = "SUBGROUP",
                                     SUBGROUPNAME = dr.Field<string>("LOTID"),
                                     SAMPLINGNAME = dr.Field<string>("AREANAME"),
                                     NVALUE = dr.Field<decimal>("RATE")
                                 };

                    var cs = rowSrc
                    //.Where(w => w.SUBGROUP == parSubgoupID)    
                    .GroupBy(g => new
                    {
                        //g.SUBGROUP,
                        g.SAMPLING
                    })
                    .Select(s => new
                    {
                        //sSUBGROUP = s.Key.SUBGROUP,
                        sSUBGROUP = s.Max(ss => ss.SUBGROUP),
                        sSAMPLING = s.Key.SAMPLING,
                        sSUBGROUPNAME = s.Max(ss => ss.SUBGROUPNAME),
                        sSAMPLINGNAME = s.Max(ss => ss.SAMPLINGNAME),
                        nMin = s.Min(ss => ss.NVALUE),
                        nMax = s.Max(ss => ss.NVALUE),
                        nTot = s.Sum(ss => ss.NVALUE),
                        nAvg = s.Average(ss => ss.NVALUE),
                        nSampingCount = s.Count()
                    });

                    foreach (var item in cs)
                    {
                        DataRow row = parPlotData.YieldDataAnalysisTable.NewRow();
                        row["SUBGROUP"] = "SUBGROUP"; //item.sSUBGROUP;
                        row["SAMPLING"] = item.sSAMPLING;
                        row["SUBGROUPNAME"] = item.sSUBGROUPNAME;
                        row["SAMPLINGNAME"] = item.sSAMPLINGNAME;
                        row["Label"] = item.sSAMPLINGNAME;

                        row["n01LowMin"] = item.nMin;
                        row["n02HightMax"] = item.nMax;
                        row["n05Mean"] = item.nAvg;

                        row["n41MIN"] = item.nMin;
                        row["n42MAX"] = item.nMax;

                        row["n33SamplingCount"] = item.nSampingCount;

                        parPlotData.YieldDataAnalysisTable.Rows.Add(row);
                    };

                    //DB Binding Clear
                    this.chartProcessAreaInTapProcArea.DataSource = null;

                    //BoxPlot
                    this.chartProcessAreaInTapProcArea.Series["BoxPlot"].ValueDataMembers.Clear();
                    this.chartProcessAreaInTapProcArea.Series["BoxPlot"].Points.Clear();

                    //평균
                    this.chartProcessAreaInTapProcArea.Series["Average"].ValueDataMembers.Clear();
                    this.chartProcessAreaInTapProcArea.Series["Average"].Points.Clear();

                    //중앙값
                    this.chartProcessAreaInTapProcArea.Series["Median"].ValueDataMembers.Clear();
                    this.chartProcessAreaInTapProcArea.Series["Median"].Points.Clear();

                    ChartBoxPlot(ref chartProcessAreaInTapProcArea, ref parPlotData, AnalysisChartType.AnalysisPolt, ViewType.CandleStick, strChartType, dt2);
                }

                #endregion

                #region 공정 시계열 - 작업장 불량 포인트 차트

                var baseLinq = dt1.AsEnumerable();
                {
                    var vItems = from dr in baseLinq
                                 where !dr.IsNull("AREAID") || !dr.IsNull("LOTID") || !dr.IsNull("DEFECTQTY")
                                 select new
                                 {
                                     AREA = dr.Field<string>("AREANAME"),
                                     LOTID = dr.Field<string>("LOTID"),
                                     //DEFECTNAME = dr.Field<string>("DEFECTNAME") + "-" + dr.Field<string>("QCSEGMENTNAME"),
                                     //DEFECTCODE = dr.Field<string>("DEFECTCODE") + "-" + dr.Field<string>("QCSEGMENTID"),
                                     DEFECTQTY = dr.Field<decimal>("DEFECTQTY"),
                                     INPUTQTY = dr.Field<decimal>("INPUTQTY"),
                                 };

                    var vAreaLot = from it in vItems
                                   group it by new
                                   {
                                       AreaName = it.AREA,
                                       LotNo = it.LOTID
                                   } into g
                                   select new
                                   {
                                       AREANM = g.Key.AreaName,
                                       LOTID = g.Key.LotNo
                                   };

                    var vLotDefectQty = from it in vItems
                                        group it by new
                                        {
                                            AreaName = it.AREA,
                                            LotNo = it.LOTID,
                                            //DefectName = it.DEFECTNAME,
                                            InputQty = it.INPUTQTY
                                        } into g
                                        select new
                                        {
                                            AREANAME = g.Key.AreaName,
                                            LOTID = g.Key.LotNo,
                                            //DEFECTNAME = g.Key.DefectName,
                                            INPUTQTY = g.Key.InputQty,
                                            DEFECTQTY = g.Max(x => x.DEFECTQTY)
                                        };

                    var vLotQty = from it in vLotDefectQty
                                  group it by new
                                  {
                                      AreaName = it.AREANAME,
                                      LotNo = it.LOTID,
                                      InputQty = it.INPUTQTY
                                  } into g
                                  select new
                                  {
                                      AREANAME = g.Key.AreaName,
                                      LOTID = g.Key.LotNo,
                                      INPUTQTY = g.Key.InputQty,
                                      DEFECTQTY = g.Sum(x => x.DEFECTQTY)
                                  };

                    var lotRate = from it in vLotQty
                                  select new
                                  {
                                      it.AREANAME,
                                      it.LOTID,
                                      RATE = Math.Round(100 * ((double)(it.DEFECTQTY) / (double)it.INPUTQTY), 2)
                                  };

                    Series ptSeries;
                    List<Series> lstAreaLotPtSeries = new List<Series>();
                    foreach (var it in vAreaLot)
                    {
                        ptSeries = new Series(it.LOTID.ToString(), ViewType.Point);
                        //ptSeries.ValueDataMembers = it.LOTNO.ToString();
                        ptSeries.ArgumentScaleType = ScaleType.Qualitative;

                        ptSeries.CrosshairLabelPattern = "{S} : {V}";

                        foreach (var val in lotRate)
                        {
                            if (it.LOTID.ToString().Equals(val.LOTID.ToString()) && it.AREANM.ToString().Equals(val.AREANAME.ToString()))
                            {
                                SeriesPoint pt = new SeriesPoint(val.AREANAME.ToString(), val.RATE);

                                ptSeries.Points.Add(pt);
                            }
                        }

                        lstAreaLotPtSeries.Add(ptSeries);
                    }

                    chartProcessAreaInTapProcArea.DataSource = chartData;
                    chartProcessAreaInTapProcArea.Series.AddRange(lstAreaLotPtSeries.ToArray());

                    // Hide the legend (if necessary). 
                    chartProcessAreaInTapProcArea.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

                }

                #endregion

                #region 공정 작업장 시계열 - 작업장 차트

                chartTimeAreaSeriesInTapProcArea.Series.Clear();

                chartTimeAreaSeriesInTapProcArea.SeriesSelectionMode = SeriesSelectionMode.Point;
                chartTimeAreaSeriesInTapProcArea.SelectionMode = ElementSelectionMode.Single;
                //chartTimeAreaSeriesInTapProcArea.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.True;
                chartTimeAreaSeriesInTapProcArea.SeriesTemplate.ArgumentScaleType = ScaleType.Auto;
                // Specify the crosshair group header pattern. 
                chartTimeAreaSeriesInTapProcArea.CrosshairOptions.GroupHeaderPattern = "{A}";

                // 작업장 시계열
                {
                    var vItems = from dr in baseLinq
                                 where !dr.IsNull("AREAID") || !dr.IsNull("LOTID") || !dr.IsNull("DEFECTQTY")
                                 select new
                                 {
                                     AREA = dr.Field<string>("AREANAME"),
                                     LOTID = dr.Field<string>("LOTID"),
                                     //DEFECTNAME = dr.Field<string>("DEFECTNAME") + "-" + dr.Field<string>("QCSEGMENTNAME"),
                                     //DEFECTCODE = dr.Field<string>("DEFECTCODE") + "-" + dr.Field<string>("QCSEGMENTID"),
                                     DEFECTQTY = dr.Field<decimal>("DEFECTQTY"),
                                     INPUTQTY = dr.Field<decimal>("INPUTQTY"),
                                     SUMMARYDATE = dr.Field<string>("SUMMARYDATE")
                                 };

                    var gItems = from it in vItems
                                 group it by new
                                 {
                                     LotNo = it.LOTID,
                                     AreaNm = it.AREA,
                                     //DefectName = it.DEFECTNAME,
                                     SummaryDate = it.SUMMARYDATE
                                 } into g
                                 select new
                                 {
                                     LOTID = g.Key.LotNo,
                                     AREANM = g.Key.AreaNm,
                                     //DEFECTNAME = g.Key.DefectName,
                                     SUMMARYDATE = g.Key.SummaryDate,
                                     INPUTQTY = g.Max(x => x.INPUTQTY),
                                     DEFECTQTY = g.Max(x => x.DEFECTQTY)
                                 };
                    var ggItems = from it in gItems
                                  group it by new
                                  {
                                    LotNo = it.LOTID,
                                    AreaNm = it.AREANM,
                                    SummaryDate = it.SUMMARYDATE
                                  } into g
                                  select new
                                  {
                                        LOTID = g.Key.LotNo,
                                        AREANM = g.Key.AreaNm,
                                        SUMMARYDATE = g.Key.SummaryDate,
                                        INPUTQTY = g.Max(x => x.INPUTQTY),
                                        DEFECTQTY = g.Sum(x => x.DEFECTQTY)
                                  };

                    var areaLotRate = from it in ggItems
                                      select new
                                      {
                                          it.AREANM,
                                          it.LOTID,
                                          it.SUMMARYDATE,
                                          RATE = Math.Round(100 * ((double)(it.DEFECTQTY) / (double)it.INPUTQTY), 2)
                                      };

                    var lots = from it in areaLotRate
                               group it by new
                               {
                                   it.AREANM,
                                   it.LOTID,
                               } into g
                               select new
                               {
                                   AREANAME = g.Key.AREANM,
                                   LOTID = g.Key.LOTID,
                               };

                    Series ptSeries;
                    List<Series> lstAreaLotPtSeries = new List<Series>();
                    foreach (var it in lots)
                    {
                        ptSeries = new Series(it.AREANAME.ToString() + "-" + it.LOTID.ToString(), ViewType.Point);
                        //ptSeries.ValueDataMembers = it.LOTNO.ToString();
                        ptSeries.ArgumentScaleType = ScaleType.Auto;
                        ptSeries.CrosshairLabelPattern = "{S} : {V}";

                        foreach (var val in areaLotRate)
                        {
                            if (it.LOTID.ToString().Equals(val.LOTID.ToString()))
                            {
                                SeriesPoint pt = new SeriesPoint(val.SUMMARYDATE, val.RATE);

                                ptSeries.Points.Add(pt);
                                //ptSeries.Points.Add(new SeriesPoint(val.AREAID.ToString(), val.RATE));
                            }
                        }

                        lstAreaLotPtSeries.Add(ptSeries);
                    }


                    chartTimeAreaSeriesInTapProcArea.Series.AddRange(lstAreaLotPtSeries.ToArray());

                    // Access the type-specific options of the diagram. 
                    XYDiagram diagram = (XYDiagram)chartTimeAreaSeriesInTapProcArea.Diagram;

                    diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Second;
                    diagram.AxisX.Label.TextPattern = "{A:yyyy-MM-dd HH}";

                    // Hide the legend (if necessary). 
                    chartTimeAreaSeriesInTapProcArea.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                    chartTimeAreaSeriesInTapProcArea.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Left;
                    chartTimeAreaSeriesInTapProcArea.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
                    chartTimeAreaSeriesInTapProcArea.Legend.Direction = LegendDirection.LeftToRight;

                    #endregion
                }
            }
            else if (strChartType.Equals("2"))
            {
                //chartProcessAreaInTapAreaEqp.Series.Clear();
                chartTimeAreaSeriesInTapAreaEqp.Series.Clear();

                //chartProcessAreaInTapAreaEqp.SeriesSelectionMode = SeriesSelectionMode.Point;
                //chartProcessAreaInTapAreaEqp.SelectionMode = ElementSelectionMode.Single;
                chartProcessAreaInTapAreaEqp.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.True;
                // Specify the crosshair group header pattern. 
                //chartProcessAreaInTapAreaEqp.CrosshairOptions.GroupHeaderPattern = "{A}";

                #region 작업장 설비- 작업장-설비 불량 박스 플롯 차트

                this.chartProcessAreaInTapAreaEqp.Series.Clear();

                this.chartProcessAreaInTapAreaEqp.Series.Add(new Series("BoxPlot", ViewType.CandleStick));
                this.chartProcessAreaInTapAreaEqp.Series.Add(new Series("Median", ViewType.Point));
                this.chartProcessAreaInTapAreaEqp.Series.Add(new Series("Average", ViewType.Point));

                this.chartProcessAreaInTapAreaEqp.Series["BoxPlot"].Visible = false;
                this.chartProcessAreaInTapAreaEqp.Series["Median"].Visible = false;
                this.chartProcessAreaInTapAreaEqp.Series["Average"].Visible = false;
                YieldParameter parPlotData = YieldParameter.Create();

                var boxBase = dt2.AsEnumerable();
                if (boxBase.Count() > 0)
                {
                    var rowSrc = from dr in boxBase
                                 select new
                                 {
                                     SAMPLING = dr.Field<string>("AREAEQPID"),
                                     SUBGROUP = "SUBGROUP",
                                     SUBGROUPNAME = dr.Field<string>("LOTID"),
                                     SAMPLINGNAME = dr.Field<string>("AREAEQPNAME"),
                                     NVALUE = dr.Field<decimal>("RATE")
                                 };

                    var cs = rowSrc
                    //.Where(w => w.SUBGROUP == parSubgoupID)    
                    .GroupBy(g => new
                    {
                        //g.SUBGROUP,
                        g.SAMPLING
                    })
                    .Select(s => new
                    {
                        //sSUBGROUP = s.Key.SUBGROUP,
                        sSUBGROUP = s.Max(ss => ss.SUBGROUP),
                        sSAMPLING = s.Key.SAMPLING,
                        sSUBGROUPNAME = s.Max(ss => ss.SUBGROUPNAME),
                        sSAMPLINGNAME = s.Max(ss => ss.SAMPLINGNAME),
                        nMin = s.Min(ss => ss.NVALUE),
                        nMax = s.Max(ss => ss.NVALUE),
                        nTot = s.Sum(ss => ss.NVALUE),
                        nAvg = s.Average(ss => ss.NVALUE),
                        nSampingCount = s.Count()
                    });

                    foreach (var item in cs)
                    {
                        DataRow row = parPlotData.YieldDataAnalysisTable.NewRow();
                        row["SUBGROUP"] = "SUBGROUP"; //item.sSUBGROUP;
                        row["SAMPLING"] = item.sSAMPLING;
                        row["SUBGROUPNAME"] = item.sSUBGROUPNAME;
                        row["SAMPLINGNAME"] = item.sSAMPLINGNAME;
                        row["Label"] = item.sSAMPLINGNAME;

                        row["n01LowMin"] = item.nMin;
                        row["n02HightMax"] = item.nMax;
                        row["n05Mean"] = item.nAvg;

                        row["n41MIN"] = item.nMin;
                        row["n42MAX"] = item.nMax;

                        row["n33SamplingCount"] = item.nSampingCount;

                        parPlotData.YieldDataAnalysisTable.Rows.Add(row);
                    };

                    //DB Binding Clear
                    this.chartProcessAreaInTapAreaEqp.DataSource = null;

                    //BoxPlot
                    this.chartProcessAreaInTapAreaEqp.Series["BoxPlot"].ValueDataMembers.Clear();
                    this.chartProcessAreaInTapAreaEqp.Series["BoxPlot"].Points.Clear();

                    //평균
                    this.chartProcessAreaInTapAreaEqp.Series["Average"].ValueDataMembers.Clear();
                    this.chartProcessAreaInTapAreaEqp.Series["Average"].Points.Clear();

                    //중앙값
                    this.chartProcessAreaInTapAreaEqp.Series["Median"].ValueDataMembers.Clear();
                    this.chartProcessAreaInTapAreaEqp.Series["Median"].Points.Clear();

                    ChartBoxPlot(ref chartProcessAreaInTapAreaEqp, ref parPlotData, AnalysisChartType.AnalysisPolt, ViewType.CandleStick, strChartType, dt2);
                }

                #endregion

                #region 공정 시계열 - 작업장-설비 포인트 차트

                var baseLinq = dt1.AsEnumerable();
                {
                    var vItems = from dr in baseLinq
                                 where !dr.IsNull("AREAID") || !dr.IsNull("LOTID") || !dr.IsNull("DEFECTQTY")
                                 select new
                                 {
                                     AREAEQP = dr.Field<string>("AREANAME") + "-" + dr.Field<string>("EQUIPMENTNAME"),
                                     LOTID = dr.Field<string>("LOTID"),
                                     //DEFECTNAME = dr.Field<string>("DEFECTNAME"),
                                     //DEFECTCODE = dr.Field<string>("DEFECTCODE"),
                                     DEFECTQTY = dr.Field<decimal>("DEFECTQTY"),
                                     INPUTQTY = dr.Field<decimal>("INPUTQTY"),
                                     SUMMARYDATE = dr.Field<string>("SUMMARYDATE")
                                 };



                    var gItems = from it in vItems
                                 group it by new
                                 {
                                     AreaEQP = it.AREAEQP,
                                     LotNo = it.LOTID,
                                     InputQty = it.INPUTQTY
                                 } into g
                                 select new
                                 {
                                     AREAEQP = g.Key.AreaEQP,
                                     LOTID = g.Key.LotNo,
                                     INPUTQTY = g.Key.InputQty,
                                     DEFECTQTY = g.Sum(x => x.DEFECTQTY)
                                 };

                    var areaLotRate = from it in gItems
                                      select new
                                      {
                                          it.AREAEQP,
                                          it.LOTID,
                                          RATE = Math.Round(100 * ((double)(it.DEFECTQTY) / (double)it.INPUTQTY), 2)
                                      };

                    var lots = from it in areaLotRate
                               group it by new
                               {
                                   it.AREAEQP,
                                   it.LOTID,
                               } into g
                               select new
                               {
                                   AreaEqp = g.Key.AREAEQP,
                                   LOTNO = g.Key.LOTID,
                               };

                    Series ptSeries;
                    List<Series> lstAreaLotPtSeries = new List<Series>();
                    foreach (var it in lots)
                    {
                        ptSeries = new Series(it.LOTNO.ToString(), ViewType.Point);
                        //ptSeries.ValueDataMembers = it.LOTNO.ToString();
                        ptSeries.ArgumentScaleType = ScaleType.Qualitative;
                        ptSeries.CrosshairLabelPattern = "{S} : {V}";

                        foreach (var val in areaLotRate)
                        {
                            if (it.LOTNO.ToString().Equals(val.LOTID.ToString()) && it.AreaEqp.ToString().Equals(val.AREAEQP.ToString()))
                            {
                                SeriesPoint pt = new SeriesPoint(val.AREAEQP.ToString(), val.RATE);

                                ptSeries.Points.Add(pt);
                                //ptSeries.Points.Add(new SeriesPoint(val.AREAID.ToString(), val.RATE));
                            }
                        }

                        lstAreaLotPtSeries.Add(ptSeries);
                    }


                    chartProcessAreaInTapAreaEqp.Series.AddRange(lstAreaLotPtSeries.ToArray());

                    // Hide the legend (if necessary). 
                    chartProcessAreaInTapAreaEqp.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

                }

                #endregion

                chartTimeAreaSeriesInTapAreaEqp.Series.Clear();

                chartTimeAreaSeriesInTapAreaEqp.SeriesSelectionMode = SeriesSelectionMode.Point;
                chartTimeAreaSeriesInTapAreaEqp.SelectionMode = ElementSelectionMode.Single;
                chartTimeAreaSeriesInTapAreaEqp.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.True;
                // Specify the crosshair group header pattern. 
                chartTimeAreaSeriesInTapAreaEqp.CrosshairOptions.GroupHeaderPattern = "{A}";

                // 작업장 시계열
                {
                    var vItems = from dr in baseLinq
                                 where !dr.IsNull("AREAID") || !dr.IsNull("LOTID") || !dr.IsNull("DEFECTQTY")
                                 select new
                                 {
                                     AREA = dr.Field<string>("AREAID"),
                                     AREANAME = dr.Field<string>("AREANAME"),
                                     EQP = dr.Field<string>("EQUIPMENTNAME"),
                                     LOTID = dr.Field<string>("LOTID"),
                                     //DEFECTNAME = dr.Field<string>("DEFECTNAME"),
                                     //DEFECTCODE = dr.Field<string>("DEFECTCODE"),
                                     DEFECTQTY = dr.Field<decimal>("DEFECTQTY"),
                                     INPUTQTY = dr.Field<decimal>("INPUTQTY"),
                                     SUMMARYDATE = dr.Field<string>("SUMMARYDATE")
                                 };

                    var gItems = from it in vItems
                                 group it by new
                                 {
                                     AreaID = it.AREA,
                                     AreaName = it.AREANAME,
                                     EqpID = it.EQP,
                                     LotNo = it.LOTID,
                                     InputQty = it.INPUTQTY,
                                     SummaryDate = it.SUMMARYDATE
                                 } into g
                                 select new
                                 {
                                     AREAID = g.Key.AreaID,
                                     AREANAME = g.Key.AreaName,
                                     EQPID = g.Key.EqpID,
                                     LOTID = g.Key.LotNo,
                                     SUMMARYDATE = g.Key.SummaryDate,
                                     INPUTQTY = g.Key.InputQty,
                                     DEFECTQTY = g.Sum(x => x.DEFECTQTY)
                                 };

                    var areaDefectInfo = from it in gItems
                                         group it by new
                                         {
                                             AreaName = it.AREANAME,
                                             AreaID = it.AREAID,
                                             EqpID = it.EQPID,
                                             LotID = it.LOTID,
                                             SummaryDate = it.SUMMARYDATE
                                         } into g
                                         select new
                                         {
                                             AREAID = g.Key.AreaID,
                                             AREANAME = g.Key.AreaName,
                                             EQPID = g.Key.EqpID,
                                             LOTNO = g.Key.LotID,
                                             SUMMARYDATE = g.Key.SummaryDate,
                                             INPUTQTY = g.Sum(x => x.INPUTQTY),
                                             DEFECTQTY = g.Sum(x => x.DEFECTQTY)
                                         };

                    var areaLotRate = from it in areaDefectInfo
                                      select new
                                      {
                                          it.AREAID,
                                          it.AREANAME,
                                          it.EQPID,
                                          it.LOTNO,
                                          it.SUMMARYDATE,
                                          RATE = Math.Round(100 * ((double)(it.DEFECTQTY) / (double)it.INPUTQTY), 2)
                                      };

                    var lots = from it in areaLotRate
                               group it by new
                               {
                                   it.EQPID,
                                   it.LOTNO,
                               } into g
                               select new
                               {
                                   EQPNM = g.Key.EQPID,
                                   LOTID = g.Key.LOTNO, 
                                   //SUMDATE = g.Key.SUMMARYDATE
                               };

                    Series ptSeries;
                    List<Series> lstAreaLotPtSeries = new List<Series>();
                    foreach (var it in lots)
                    {
                        ptSeries = new Series(it.EQPNM+"-"+it.LOTID, ViewType.Point);
                        //ptSeries.ValueDataMembers = it.LOTNO.ToString();
                        ptSeries.ArgumentScaleType = ScaleType.Auto;
                        ptSeries.CrosshairLabelPattern = "{S} : {V}";

                        foreach (var val in areaLotRate)
                        {
                            if (it.LOTID.ToString().Equals(val.LOTNO.ToString()))
                            {
                                SeriesPoint pt = new SeriesPoint(val.SUMMARYDATE, val.RATE);

                                ptSeries.Points.Add(pt);
                                //ptSeries.Points.Add(new SeriesPoint(val.AREAID.ToString(), val.RATE));
                            }
                        }

                        lstAreaLotPtSeries.Add(ptSeries);
                    }


                    chartTimeAreaSeriesInTapAreaEqp.Series.AddRange(lstAreaLotPtSeries.ToArray());

                    // Access the type-specific options of the diagram. 
                    XYDiagram diagram = (XYDiagram)chartTimeAreaSeriesInTapAreaEqp.Diagram;

                    diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Second;
                    //diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Hour;
                    diagram.AxisX.Label.TextPattern = "{A:yyyy-MM-dd HH}";

                    // Hide the legend (if necessary). 
                    chartTimeAreaSeriesInTapAreaEqp.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                    chartTimeAreaSeriesInTapAreaEqp.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Left;
                    chartTimeAreaSeriesInTapAreaEqp.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
                    chartTimeAreaSeriesInTapAreaEqp.Legend.Direction = LegendDirection.LeftToRight;
                }
            }
            else
            {
                //chartTimeAreaSeriesInTapEqpLOT
                chartTimeAreaSeriesInTapEqpLOT.Series.Clear();

                chartTimeAreaSeriesInTapEqpLOT.SeriesSelectionMode = SeriesSelectionMode.Point;
                chartTimeAreaSeriesInTapEqpLOT.SelectionMode = ElementSelectionMode.Single;
                chartTimeAreaSeriesInTapEqpLOT.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.True;
                // Specify the crosshair group header pattern. 
                chartTimeAreaSeriesInTapEqpLOT.CrosshairOptions.GroupHeaderPattern = "{A}";

                var baseLinq = dt1.AsEnumerable();
                // 작업장 시계열
                {
                    var vItems = from dr in baseLinq
                                 where !dr.IsNull("AREAID") || !dr.IsNull("LOTID") || !dr.IsNull("DEFECTQTY")
                                 select new
                                 {
                                     AREA = dr.Field<string>("AREAID"),
                                     AREANAME = dr.Field<string>("AREANAME"),
                                     EQP = dr.Field<string>("EQUIPMENTNAME"),
                                     LOTID = dr.Field<string>("LOTID"),
                                     //DEFECTNAME = dr.Field<string>("DEFECTNAME"),
                                     //DEFECTCODE = dr.Field<string>("DEFECTCODE"),
                                     DEFECTQTY = dr.Field<decimal>("DEFECTQTY"),
                                     INPUTQTY = dr.Field<decimal>("INPUTQTY"),
                                     SUMMARYDATE = dr.Field<string>("SUMMARYDATE")
                                 };



                    var gItems = from it in vItems
                                 group it by new
                                 {
                                     AreaID = it.AREA,
                                     AreaName = it.AREANAME,
                                     EqpID = it.EQP,
                                     LotNo = it.LOTID,
                                     InputQty = it.INPUTQTY,
                                     SummaryDate = it.SUMMARYDATE
                                 } into g
                                 select new
                                 {
                                     AREAID = g.Key.AreaID,
                                     AREANAME = g.Key.AreaName,
                                     EQPID = g.Key.EqpID,
                                     LOTID = g.Key.LotNo,
                                     INPUTQTY = g.Key.InputQty,
                                     SUMMARYDATE = g.Key.SummaryDate,
                                     DEFECTQTY = g.Sum(x => x.DEFECTQTY)
                                 };

                    var areaLotRate = from it in gItems
                                      select new
                                      {
                                          it.AREANAME,
                                          it.EQPID,
                                          it.LOTID,
                                          it.SUMMARYDATE,
                                          RATE = Math.Round(100 * ((double)(it.DEFECTQTY) / (double)it.INPUTQTY), 2)
                                      };

                    var lots = from it in areaLotRate
                               group it by new
                               {
                                   it.AREANAME,
                                   it.EQPID,
                                   it.LOTID,
                                   //it.SUMMARYDATE
                               } into g
                               select new
                               {
                                   AREA = g.Key.AREANAME,
                                   EQP = g.Key.EQPID,
                                   LOTNO = g.Key.LOTID,
                                   //SUMDATE = g.Key.SUMMARYDATE
                               };

                    Series ptSeries;
                    List<Series> lstAreaLotPtSeries = new List<Series>();
                    foreach (var it in lots)
                    {
                        ptSeries = new Series(it.EQP.ToString() + "-" + it.LOTNO.ToString(), ViewType.Point);
                        //ptSeries.ValueDataMembers = it.LOTNO.ToString();
                        ptSeries.ArgumentScaleType = ScaleType.Auto;
                        ptSeries.CrosshairLabelPattern = "{S} : {V}";

                        foreach (var val in areaLotRate)
                        {
                            if (it.LOTNO.ToString().Equals(val.LOTID.ToString()) && it.EQP.ToString().Equals(val.EQPID.ToString()))
                            {
                                SeriesPoint pt = new SeriesPoint(val.SUMMARYDATE, val.RATE);

                                ptSeries.Points.Add(pt);
                                //ptSeries.Points.Add(new SeriesPoint(val.AREAID.ToString(), val.RATE));
                            }
                        }

                        lstAreaLotPtSeries.Add(ptSeries);
                    }


                    chartTimeAreaSeriesInTapEqpLOT.Series.AddRange(lstAreaLotPtSeries.ToArray());

                    // Access the type-specific options of the diagram. 
                    XYDiagram diagram = (XYDiagram)chartTimeAreaSeriesInTapEqpLOT.Diagram;

                    diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Second;
                    //diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Hour;
                    diagram.AxisX.Label.TextPattern = "{A:yyyy-MM-dd HH}";

                    // Hide the legend (if necessary). 
                    chartTimeAreaSeriesInTapEqpLOT.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                    chartTimeAreaSeriesInTapEqpLOT.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Left;
                    chartTimeAreaSeriesInTapEqpLOT.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
                    chartTimeAreaSeriesInTapEqpLOT.Legend.Direction = LegendDirection.LeftToRight;

                    #region Chart Drawing Test - Not Effective

                    SmartChart chart = chartTimeAreaSeriesInTapEqpLOT;
                    // case 1
                    //XYDiagram diagram = (XYDiagram)chart.Diagram;
                    //Graphics graph = chart.CreateGraphics();
                    //Point coords = diagram.DiagramToPoint(0, 0).Point;
                    //Pen pen = new Pen(Color.Red, 2);
                    //graph.DrawRectangle(pen, coords.X, coords.Y, 100, 100);


                    // case 2
                    // Cast the chart's diagram to the XYDiagram type, to access its axes. 
                    //DevExpress.XtraCharts.XYDiagram2D diagram2 = (DevExpress.XtraCharts.XYDiagram)chartControl1.Diagram;
                    //Point coords = diagram2.DiagramToPoint(0, 0).Point;

                    //Pen pen = new Pen(Color.Red, 2);

                    //e.Graphics.DrawRectangle(pen, coords.X, coords.Y, 10, 10);
                    
                    //    // Create a constant line. 
                    //    ConstantLine constantLine1 = new ConstantLine("Constant Line 1");
                    //    diagram.AxisX.ConstantLines.Add(constantLine1);
                    //    // Define its axis value. 
                    //    constantLine1.AxisValue = Convert.ToDateTime("2020-01-15 00:00:00");

                    //    // Customize the behavior of the constant line. 
                    //    constantLine1.Visible = true;
                    //    constantLine1.ShowInLegend = false;
                    //    constantLine1.LegendText = "Some Threshold";
                    //    constantLine1.ShowBehind = false;

                    #endregion
                }

            }
        }

        /// <summary>
        /// Box Plot Chart
        /// </summary>
        /// <param name="parPlotData"></param>
        /// <param name="chartType"></param>
        /// <param name="chartViewType"></param>
        /// <param name="dt"></param>
        private void ChartBoxPlot(ref SmartChart chart, ref YieldParameter parPlotData, AnalysisChartType chartType, ViewType chartViewType, string strChartType, DataTable dt = null)
        {
            if (dt == null) return;

            int rowMax = 0;
            string subgroupId = "";
            string samplingId = "";
            string samplingName = "";
            int npQ1, npQ2, npQ3, npQ4, npMedian;
            double nQ1, nQ2, nQ3, nQ4, nMedian;
            int nSCount = 0, nisResidue;
            double nRange = 0;
            double nIQR = 0;
            double nMin = 0, nMax = 0, nAvg = 0;
            string labelPattern = "";
            switch (chartType)
            {
                case AnalysisChartType.AnalysisPolt:
                case AnalysisChartType.BoxPlot:
                    labelPattern = "{A}\nMin: {LV:F3}\nMax: {HV:F3}\nQ1: {CV:F3}\nQ3: {OV:F3}";
                    break;
                case AnalysisChartType.AnalysisLine:
                    labelPattern = "{A}\nMin: {LV:F3}\nMax: {HV:F3}";
                    break;
            }

            //Box Plot
            chart.Series["BoxPlot"].ValueDataMembers.Clear();
            chart.Series["BoxPlot"].Points.Clear();
            chart.Series["BoxPlot"].CrosshairLabelPattern = labelPattern;
            chart.Series["BoxPlot"].ChangeView(chartViewType);

            //평균 Line
            chart.Series["Average"].ValueDataMembers.Clear();
            chart.Series["Average"].Points.Clear();
            chart.Series["Average"].CrosshairLabelPattern = "평균: {V:F3}";

            //중앙값 Line
            chart.Series["Median"].ValueDataMembers.Clear();
            chart.Series["Median"].Points.Clear();
            chart.Series["Median"].CrosshairLabelPattern = "중앙값: {V:F3}";

            if (parPlotData.YieldDataAnalysisTable != null && parPlotData.YieldDataAnalysisTable.Rows.Count > 0)
            {
                rowMax = parPlotData.YieldDataAnalysisTable.Rows.Count;
                for (int i = 0; i < rowMax; i++)
                {
                    DataRow row = parPlotData.YieldDataAnalysisTable.Rows[i];
                    subgroupId = row["SUBGROUP"].ToSafeString();
                    samplingId = row["SAMPLING"].ToSafeString();
                    samplingName = row["SAMPLINGNAME"].ToSafeString();
                    nAvg = row["n05Mean"].ToSafeDoubleZero();
                    nMin = row["n41MIN"].ToSafeDoubleZero();
                    nMax = row["n42MAX"].ToSafeDoubleZero();
                    nSCount = (int)row["n33SamplingCount"];

                    npQ1 = ((nSCount + 1) * 1) / 4;
                    npQ2 = ((nSCount + 1) * 2) / 4;
                    npQ3 = ((nSCount + 1) * 3) / 4;
                    npQ4 = ((nSCount + 1) * 4) / 4;

                    nisResidue = nSCount % 2;
                    if (nisResidue != 0)
                    {
                        npMedian = (nSCount + 1) / 2;
                    }
                    else
                    {
                        npMedian = nSCount / 2;
                    }



                    int p = 0;
                    double[] dValue = new double[nSCount + 2];
                    int recCount = 0;

                    if (strChartType.Equals("1"))
                    {
                        var recDatax = dt.AsEnumerable()
                            //.Where(w => w.SUBGROUP == subgroupId && w.SAMPLING == samplingId)
                            .Where(w => w.Field<string>("AREAID") == samplingId)
                            .Select(s => new { NVALUE = s.Field<decimal>("RATE") })
                            .OrderBy(r1 => r1.NVALUE);

                        recCount = recDatax.Count();

                        foreach (var item in recDatax)
                        {
                            p++;
                            dValue[p] = item.NVALUE.ToSafeDoubleStaMin();
                        }
                    }
                    else
                    {
                        var recDatax = dt.AsEnumerable()
                            //.Where(w => w.SUBGROUP == subgroupId && w.SAMPLING == samplingId)
                            .Where(w => w.Field<string>("AREAEQPID") == samplingId)
                            .Select(s => new { NVALUE = s.Field<decimal>("RATE") })
                            .OrderBy(r1 => r1.NVALUE);

                        recCount = recDatax.Count();

                        foreach (var item in recDatax)
                        {
                            p++;
                            dValue[p] = item.NVALUE.ToSafeDoubleStaMin();
                        }
                    }

                    if (recCount == 1)
                    {
                        nQ1 = dValue[1];
                        nQ2 = dValue[1];
                        nQ3 = dValue[1];
                        nQ4 = dValue[1];
                    }
                    else if (recCount == 2)
                    {
                        nQ1 = dValue[1];
                        nQ2 = dValue[1];
                        nQ3 = dValue[2];
                        nQ4 = dValue[2];
                    }
                    else
                    {
                        nQ1 = dValue[npQ1];
                        nQ2 = dValue[npQ2];
                        nQ3 = dValue[npQ3];
                        nQ4 = dValue[npQ4];
                    }
                    nMedian = dValue[npMedian];

                    row["n03CloseQ1"] = nQ1;
                    row["n06OpenQ3"] = nQ3;

                    row["n04Median"] = nMedian;
                    //row["n21Qp1"] = npQ1;
                    //row["n22Qp2"] = npQ2;
                    //row["n23Qp3"] = npQ3;
                    //row["n24Qp4"] = npQ4;
                    //row["n25Q1"] = nQ1;
                    //row["n26Q2"] = nQ2;
                    //row["n27Q3"] = nQ3;
                    //row["n28Q4"] = nQ4;
                    //row["n31UiQR"] = nQ4;
                    //row["n32LiQR"] = nQ4;

                    //BoxPlot
                    SeriesPoint seriesPoint = new SeriesPoint();
                    seriesPoint.Argument = samplingName;
                    double[] val = new double[4];
                    val[0] = nMin;
                    val[1] = nMax;
                    val[2] = nQ3;
                    val[3] = nQ1;
                    seriesPoint.Values = val;

                    seriesPoint.Color = Color.FromArgb(127, Color.DarkGray);

                    //nMedian 중앙값
                    SeriesPoint seriesMedianLine = new SeriesPoint();
                    seriesMedianLine.Argument = samplingName;
                    double[] valMedianLine = new double[1];
                    valMedianLine[0] = nMedian;
                    seriesMedianLine.Values = valMedianLine;
                    seriesMedianLine.Color = Color.LightGreen;
                    chart.Series["Median"].Points.Add(seriesMedianLine);
                    chart.Series["Median"].View.Color = Color.LightGreen;


                    chart.Series["BoxPlot"].Points.Add(seriesPoint);

                    switch (chartType)
                    {
                        case AnalysisChartType.AnalysisLine:
                        case AnalysisChartType.AnalysisPolt:
                            //BoxPlot
                            SeriesPoint seriesPointLine = new SeriesPoint();
                            seriesPointLine.Argument = samplingName;
                            double[] valLine = new double[1];
                            valLine[0] = nAvg;
                            seriesPointLine.Values = valLine;
                            seriesPointLine.Color = Color.Black;
                            chart.Series["Average"].Points.Add(seriesPointLine);
                            chart.Series["Average"].View.Color = Color.Black;
                            break;
                    }

                }

                ((FinancialSeriesViewBase)chart.Series["BoxPlot"].View).ReductionOptions.Color = Color.FromArgb(127, Color.DarkGray);

                ((PointSeriesView)chart.Series["Median"].View).PointMarkerOptions.Kind = MarkerKind.Triangle;
                ((PointSeriesView)chart.Series["Average"].View).PointMarkerOptions.Kind = MarkerKind.Pentagon;

                chart.Series["BoxPlot"].Visible = true;
                chart.Series["Median"].Visible = true;
                chart.Series["Average"].Visible = true;
            }
        }

        private async void btnSearchProcAreaChart_Click(object sender, EventArgs e)
        {
            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);

                await base.OnSearchAsync();

                var values = Conditions.GetValues();
                ltbPeriodProcArea.EditValue = values["P_PERIOD_PERIODFR"].ToString().Substring(0, 10) + " ~ " + values["P_PERIOD_PERIODTO"].ToString().Substring(0, 10);
                ltbItemProcArea.EditValue = values["P_PRODUCTNAME"].ToString();

                DataRow dr = _selectedRow;
                //values.Add("PRODUCTDEFIDVERSION", dr["PRODUCTDEFIDVERSION"].ToString());

                if (string.IsNullOrEmpty(values["P_PRODUCTDEFID"].ToString()))
                {
                    ShowMessage("ToolRequestProductCodeValidation");
                    return;
                }

                DataTable chkTable = grdSegmentByTapProcArea.View.GetCheckedRows();

                if (chkTable.Rows.Count == 0)
                    return;

                DataTable areaEqpGridSrc = grdAreaEquipmentByTapProcArea.DataSource as DataTable;
                if (areaEqpGridSrc.Rows.Count == 0) return;

                DataTable chkAreaTable = grdAreaEquipmentByTapProcArea.View.GetCheckedRows();
                
                List<string> lstSeq = chkTable.AsEnumerable().Select(s => s.Field<string>("NO")).Distinct().ToList();
                List<string> lstArea = chkAreaTable.AsEnumerable().Select(s => s.Field<string>("AREAID")).Distinct().ToList();
                List<string> lstEQP = chkAreaTable.AsEnumerable().Select(s => s.Field<string>("EQUIPMENTID")).Distinct().ToList();

                string strProcessSeq = string.Join(",", lstSeq);
                string strArea = string.Join(",", lstArea);
                string strEqp = string.Join(",", lstEQP);

                values.Add("P_USERSEQUENCE", strProcessSeq);
                values.Add("P_AREA", strArea);
                values.Add("P_EQUIPMENTID", strEqp);
                values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                string qryYPE = string.Empty;
                if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
                    qryYPE = "YPE";

                DataSet dataSet = new DataSet();
                if (await SqlExecuter.QueryAsync("SelectDefectAnalysysData" + qryYPE, "10001", values) is DataTable dt1)
                {
                    dt1.TableName = "Table1";
                    dataSet.Tables.Add(dt1.Copy());
                }

                if (await SqlExecuter.QueryAsync("SelectDefectRateOpenCloseByDefect" + qryYPE, "10001", values) is DataTable dt2)
                {
                    dt2.TableName = "Table2";
                    dataSet.Tables.Add(dt2.Copy());
                }

                SetChartData(dataSet.Tables[0], dataSet.Tables[1], "1");

                XYDiagram diagram;
                diagram = ((XYDiagram)chartTimeAreaSeriesInTapProcArea.Diagram);
                diagram.EnableAxisXZooming = true;
                diagram.EnableAxisYZooming = true;
                
            }
            catch (Exception ex)
            {
                throw Framework.MessageException.Create(ex.ToString());
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);
            }
        }


        private async void btnSearchAreaEqpChart_Click(object sender, EventArgs e)
        {
            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);

                await base.OnSearchAsync();

                var values = Conditions.GetValues();
                ltbPeriodAreaEqp.EditValue = values["P_PERIOD_PERIODFR"].ToString().Substring(0, 10) + " ~ " + values["P_PERIOD_PERIODTO"].ToString().Substring(0, 10);
                ltbItemAreaEqp.EditValue = values["P_PRODUCTNAME"].ToString();

                if (string.IsNullOrEmpty(values["P_PRODUCTDEFID"].ToString()))
                {
                    ShowMessage("ToolRequestProductCodeValidation");
                    return;
                }

                DataRow dr = _selectedRow;
                //values.Add("PRODUCTDEFIDVERSION", dr["PRODUCTDEFIDVERSION"].ToString());

                DataTable chkTable = grdSegmentByTapAreaEqp.View.GetCheckedRows();
                if (chkTable.Rows.Count == 0)
                    return;

                DataTable areaEqpGridSrc = grdAreaEquipmentByTapAreaEqp.DataSource as DataTable;
                if (areaEqpGridSrc.Rows.Count == 0) return;

                DataTable chkAreaTable = grdAreaEquipmentByTapAreaEqp.View.GetCheckedRows();

                List<string> lstSeq = chkTable.AsEnumerable().Select(s => s.Field<string>("NO")).Distinct().ToList();
                List<string> lstArea = chkAreaTable.AsEnumerable().Select(s => s.Field<string>("AREAID")).Distinct().ToList();
                List<string> lstEQP = chkAreaTable.AsEnumerable().Select(s => s.Field<string>("EQUIPMENTID")).Distinct().ToList();

                string strProcessSeq = string.Join(",", lstSeq);
                string strArea = string.Join(",", lstArea);
                string strEqp = string.Join(",", lstEQP);

                values.Add("P_USERSEQUENCE", strProcessSeq);
                values.Add("P_AREA", strArea);
                values.Add("P_EQUIPMENTID", strEqp);
                values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                string qryYPE = string.Empty;
                if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
                    qryYPE = "YPE";

                DataSet dataSet = new DataSet();
                if (await SqlExecuter.QueryAsync("SelectDefectAnalysysData" + qryYPE, "10001", values) is DataTable dt1)
                {
                    dt1.TableName = "Table1";
                    dataSet.Tables.Add(dt1.Copy());
                }

                if (await SqlExecuter.QueryAsync("SelectDefectRateOpenCloseByDefect" + qryYPE, "10002", values) is DataTable dt2)
                {
                    dt2.TableName = "Table2";
                    dataSet.Tables.Add(dt2.Copy());
                }

                SetChartData(dataSet.Tables[0], dataSet.Tables[1], "2");

                XYDiagram diagram;
                diagram = ((XYDiagram)chartTimeAreaSeriesInTapAreaEqp.Diagram);
                diagram.EnableAxisXZooming = true;
                diagram.EnableAxisYZooming = true;
                
            }
            catch (Exception ex)
            {
                throw Framework.MessageException.Create(ex.ToString());
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);
            }
        }

        private async void btnSearchEqpLOTChart_Click(object sender, EventArgs e)
        {
            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);

                await base.OnSearchAsync();

                var values = Conditions.GetValues();
                ltbPeriodEqpLOT.EditValue = values["P_PERIOD_PERIODFR"].ToString().Substring(0, 10) + " ~ " + values["P_PERIOD_PERIODTO"].ToString().Substring(0, 10);
                ltbItemEqpLOT.EditValue = values["P_PRODUCTNAME"].ToString();

                if (string.IsNullOrEmpty(values["P_PRODUCTDEFID"].ToString()))
                {
                    ShowMessage("ToolRequestProductCodeValidation");
                    return;
                }

                DataRow dr = _selectedRow;
                //values.Add("PRODUCTDEFIDVERSION", dr["PRODUCTDEFIDVERSION"].ToString());

                DataTable chkTable = grdSegmentByTapEqpLOT.View.GetCheckedRows();
                if (chkTable.Rows.Count == 0)
                    return;

                DataTable areaEqpGridSrc = grdAreaEquipmentByTapEqpLOT.DataSource as DataTable;
                if (areaEqpGridSrc.Rows.Count == 0) return;

                //DataTable chkAreaTable = grdAreaEquipmentByTapEqpLOT.View.GetCheckedRows();

                //if (chkAreaTable.Rows.Count > 1 || chkAreaTable.Rows.Count == 0)
                //{
                //    this.ShowMessage("SelectOnlyOneEquipment");

                //    return;
                //}

                List<string> lstSeq = chkTable.AsEnumerable().Select(s => s.Field<string>("NO")).Distinct().ToList();
                //List<string> lstArea = chkAreaTable.AsEnumerable().Select(s => s.Field<string>("AREAID")).Distinct().ToList();
                //List<string> lstEQP = chkAreaTable.AsEnumerable().Select(s => s.Field<string>("EQUIPMENTID")).Distinct().ToList();

                string strProcessSeq = string.Join(",", lstSeq);
                //string strArea = string.Join(",", lstArea);
                //string strEqp = string.Join(",", lstEQP);

                string strArea = grdAreaEquipmentByTapEqpLOT.View.GetFocusedRowCellValue("AREAID").ToString();
                string strEqp = grdAreaEquipmentByTapEqpLOT.View.GetFocusedRowCellValue("EQUIPMENTID").ToString();

                values.Add("P_USERSEQUENCE", strProcessSeq);
                values.Add("P_AREA", strArea);
                values.Add("P_EQUIPMENTID", strEqp);
                values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                string qryYPE = string.Empty;
                if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
                    qryYPE = "YPE";

                DataSet dataSet = new DataSet();
                if (await SqlExecuter.QueryAsync("SelectDefectAnalysysData" + qryYPE, "10001", values) is DataTable dt1)
                {
                    dt1.TableName = "Table1";
                    dataSet.Tables.Add(dt1.Copy());
                }

                SetChartData(dataSet.Tables[0], null, "3");
                XYDiagram diagram;
                diagram = ((XYDiagram)chartTimeAreaSeriesInTapEqpLOT.Diagram);
                diagram.EnableAxisXZooming = true;
                diagram.EnableAxisYZooming = true;
            }
            catch (Exception ex)
            {
                throw Framework.MessageException.Create(ex.ToString());
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);
            }
        }
    }
}
