#region using

using DevExpress.XtraEditors;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.SPCLibrary;
using Micube.SmartMES.SPC.UserControl;

using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.SPC
{
    /// <summary>
    /// 프 로 그 램 명  : SPC > SPC 현황 > 품질분석 SPC 현황
    /// 업  무  설  명  : 품질분석 측정값을 분석하여 관리도 및 공정능력분석함.
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-07-29
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class QualityAnalysisSpcStatus : SmartConditionManualBaseForm
    {
        #region Local Variables
        private bool _isFirstEvents = false;
        /// <summary>
        /// 조회 조건 From 중 yyyy-MM-dd으로 자른 값
        /// </summary>
        private DateTime _date;

        /// <summary>
        /// 전 월
        /// </summary>
        private string _previousMonth = string.Empty;

        /// <summary>
        /// 금 월
        /// </summary>
        private string _month = string.Empty;

        /// <summary>
        /// Trand Row Data List
        /// </summary>
        private List<TrendClass> _trendData;

        #region 1. 통계 Chart 지역변수
        /// <summary>
        /// 통계분석 Parameter
        /// </summary>
        private AnalysisExecutionParameter _AnalysisParameter = AnalysisExecutionParameter.Create();

        /// <summary>
        /// DB 조회 구분 : true - 재분석 실행. false - 분석함.
        /// </summary>
        private bool _isAgainAnalysisExe = false;
        /// <summary>
        /// XBar 재분석 구분 : true - 재분석 실행. false - 분석함.
        /// </summary>
        private bool _isAgainAnalysisXBar = false;
        /// <summary>
        /// Cpk 재분석 구분 : true - 재분석 실행. false - 분석함.
        /// </summary>
        private bool _isAgainAnalysisCpk = false;
        /// <summary>
        /// Plot 재분석 구분 : true - 재분석 실행. false - 분석함.
        /// </summary>
        private bool _isAgainAnalysisPlot = false;
        /// <summary>
        /// XBarFrame 버튼 Check 유무
        /// </summary>
        private bool _isAgainAnalysisButtonXBar = false;
        /// <summary>
        /// XBarFrame 버튼 Check 유무 - Cpk
        /// </summary>
        private bool _isAgainAnalysisButtonCpk = false;
        /// <summary>
        /// XBarFrame 버튼 Check 유무 및 조회시.
        /// </summary>
        private bool _isAgainAnalysisRawData = false;
        /// <summary>
        /// XBarFrame 버튼 Check 유무 및 조회시.
        /// </summary>
        private bool _isAgainAnalysisOverRules = false;
        /// <summary>
        /// 조회 버튼 미실행 구분 : true-미실행, false-실행
        /// </summary>
        private bool _isCancelOnSearchAsync = false;
        #endregion 1. 통계 Chart 지역변수

        private SpcLibraryHelper _spcLibHelper = new SpcLibraryHelper();
        private DataTable _dtDefactDetailSum;
        private DataTable _dtDefactDetailDefect;
        private DataTable _dtDefactDetailProduct;
        
        #endregion

        #region 생성자

        public QualityAnalysisSpcStatus()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeLanguageKey();
            InitializeControls();
            InitializeEvent();
        }

        /// <summary>
        /// 화면 컨트롤 설정
        /// </summary>
        private void InitializeControls()
        {
            tabControlChart.Controls.Add(new ucXBarFrame()
            {
                Dock = DockStyle.Fill
            });

            tabProcess.Controls.Add(new ucCpkFrame()
            {
                Dock = DockStyle.Fill
            });
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            string defectiveWorst = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCUIDEFECTIVEWORST");//WORST

            #region Trend Summary Table

            gridTotal.View.ClearColumns();
            this.gridTotal.DataSource = null;
            this.gridDefect.DataSource = null;
            this.gridProduct.DataSource = null;


            gridTotal.GridButtonItem = GridButtonItem.Export;
            gridTotal.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            gridTotal.View.AddTextBoxColumn("VALUETYPE", 70) // 구분
                            .SetTextAlignment(TextAlignment.Left)
                            .SetLabel("구분");
            gridTotal.View.AddTextBoxColumn("LASTMONTH", 100) // 전월
                            .SetTextAlignment(TextAlignment.Right)
                            .SetLabel("전월");

            gridTotal.View.AddTextBoxColumn("DEFECTTOTAL", 100) // Summary
                            .SetTextAlignment(TextAlignment.Right)
                            .SetLabel("합계");

            gridTotal.View.AddTextBoxColumn("D01", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D02", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D03", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D04", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D05", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D06", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D07", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D08", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D09", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D10", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D11", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D12", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D13", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D14", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D15", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D16", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D17", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D18", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D19", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D20", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D21", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D22", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D23", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D24", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D25", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D26", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D27", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D28", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D29", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D30", 100).SetTextAlignment(TextAlignment.Right);
            gridTotal.View.AddTextBoxColumn("D31", 100).SetTextAlignment(TextAlignment.Right);

            gridTotal.View.PopulateColumns();
            gridTotal.View.BestFitColumns();

            gridTotal.ShowStatusBar = false;
            gridTotal.View.OptionsView.ShowGroupPanel = false;
            gridTotal.View.OptionsBehavior.Editable = false;

            gridTotal.Caption = string.Concat(Language.Get("INSERTDATE"), " : ", _date.Year, " / ", string.Format("{0:MM}", _date));

            #endregion

            #region Trend Table

            gridDefect.View.ClearColumns();
            gridDefect.GridButtonItem = GridButtonItem.Export;
            gridDefect.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            gridDefect.View.AddTextBoxColumn("SEQID", 70).SetLabel(defectiveWorst);
            gridDefect.View.AddTextBoxColumn("DEFECTNAME", 170);
            gridDefect.View.AddTextBoxColumn("TOTVALUEVIEW", 130).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D01", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D02", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D03", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D04", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D05", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D06", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D07", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D08", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D09", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D10", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D11", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D12", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D13", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D14", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D15", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D16", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D17", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D18", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D19", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D20", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D21", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D22", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D23", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D24", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D25", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D26", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D27", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D28", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D29", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D30", 100).SetTextAlignment(TextAlignment.Right);
            gridDefect.View.AddTextBoxColumn("D31", 100).SetTextAlignment(TextAlignment.Right);

            gridDefect.View.PopulateColumns();
            gridDefect.View.BestFitColumns();
            gridDefect.ShowStatusBar = false;
            gridDefect.View.OptionsView.ShowGroupPanel = false;
            gridDefect.View.OptionsBehavior.Editable = false;
            //gridDefect.Caption = "불량구분";
            //gridTotal.Caption = string.Concat(Language.Get("INSERTDATE"), " : ", _date.Year, " / ", string.Format("{0:MM}", _date));
            
            #endregion

            #region Prouct Table

            gridProduct.View.ClearColumns();
            gridProduct.GridButtonItem = GridButtonItem.Export;
            gridProduct.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            gridProduct.View.AddTextBoxColumn("SEQID", 70).SetLabel(defectiveWorst); ;
            gridProduct.View.AddTextBoxColumn("DEFECTNAME", 170);
            gridProduct.View.AddTextBoxColumn("TOTVALUEVIEW", 130).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D01", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D02", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D03", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D04", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D05", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D06", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D07", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D08", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D09", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D10", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D11", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D12", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D13", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D14", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D15", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D16", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D17", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D18", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D19", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D20", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D21", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D22", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D23", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D24", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D25", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D26", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D27", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D28", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D29", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D30", 100).SetTextAlignment(TextAlignment.Right);
            gridProduct.View.AddTextBoxColumn("D31", 100).SetTextAlignment(TextAlignment.Right);

            gridProduct.View.PopulateColumns();
            gridProduct.View.BestFitColumns();
            gridProduct.ShowStatusBar = false;
            gridProduct.View.OptionsView.ShowGroupPanel = false;
            gridProduct.View.OptionsBehavior.Editable = false;
            //gridProduct.Caption = "불량구분";
            //gridTotal.Caption = string.Concat(Language.Get("INSERTDATE"), " : ", _date.Year, " / ", string.Format("{0:MM}", _date));

            #endregion
            defectiveWorst = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCUIDEFECTIVEWORST");//WORST
            string defectiveName = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCUIDEFECTIVENAME");//불량명
            string defectiveCount = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCUIDEFECTIVECOUNT");//불량수량
            string defectiveProductName = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCUIDEFECTIVEPRODUCTNAME");//모델명

            this.gridDefect.View.Columns[0].Caption = defectiveWorst;//WORST
            this.gridDefect.View.Columns[1].Caption = defectiveName;//불량명
            this.gridDefect.View.Columns[2].Caption = defectiveCount;//불량수량

            this.gridProduct.View.Columns[0].Caption = defectiveWorst;//WORST
            this.gridProduct.View.Columns[1].Caption = defectiveProductName;//모델명
            this.gridProduct.View.Columns[2].Caption = defectiveCount;//불량수량

            int colIndexDefault = 2;
            int colIndexAdd = 0;
            for (int i = 1; i < 32; i++)
            {
                colIndexAdd = colIndexDefault + i;
                this.gridTotal.View.Columns[colIndexAdd].Caption = i.ToString();
                this.gridDefect.View.Columns[colIndexAdd].Caption = i.ToString();
                this.gridProduct.View.Columns[colIndexAdd].Caption = i.ToString();
            }


            #region RowData
            grdRowData.View.ClearColumns();
            grdRowData.GridButtonItem = GridButtonItem.Export;
            grdRowData.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdRowData.View.AddTextBoxColumn("INSPECTIONDATETIMEVIEWDATE", 100);
            grdRowData.View.AddTextBoxColumn("TXNHISTKEY", 100);
            grdRowData.View.AddTextBoxColumn("RESOURCETYPE", 100);
            grdRowData.View.AddTextBoxColumn("INSPECTIONTYPE", 100);
            grdRowData.View.AddTextBoxColumn("LOTID", 100);
            grdRowData.View.AddTextBoxColumn("INSPITEMID", 100);
            grdRowData.View.AddTextBoxColumn("INSPITEMVERSION", 100);
            grdRowData.View.AddTextBoxColumn("INSPITEMIDNAME", 100);
            grdRowData.View.AddTextBoxColumn("PROCESSRELNO", 100);
            grdRowData.View.AddTextBoxColumn("AREAID", 100);
            grdRowData.View.AddTextBoxColumn("PRODUCTDEFID", 100);
            grdRowData.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            grdRowData.View.AddTextBoxColumn("PRODUCTDEFIDNAME", 100);
            grdRowData.View.AddTextBoxColumn("PROCESSSEGMENTID", 100);
            grdRowData.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 100);
            grdRowData.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 100);
            grdRowData.View.AddTextBoxColumn("EQUIPMENTID", 100);
            grdRowData.View.AddTextBoxColumn("EQUIPMENTNAME", 100);
            grdRowData.View.AddTextBoxColumn("INSPECTIONDEFID", 100);
            grdRowData.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 100);
            grdRowData.View.AddTextBoxColumn("DEFECTCODE", 100);
            grdRowData.View.AddTextBoxColumn("DEFECTNAME", 100);
            grdRowData.View.AddTextBoxColumn("ENTERPRISEID", 100);
            grdRowData.View.AddTextBoxColumn("PLANTID", 100).SetLabel("SITE");
            grdRowData.View.AddTextBoxColumn("NVALUE", 100).SetLabel("DEFECTCOUNT");
            grdRowData.View.AddTextBoxColumn("NSUBVALUE", 100).SetLabel("NCRINSPECTIONQTY");
            grdRowData.View.AddTextBoxColumn("SAMPLEQTY", 100);

            grdRowData.View.PopulateColumns();
            grdRowData.View.BestFitColumns();
            grdRowData.ShowStatusBar = false;
            grdRowData.View.OptionsView.ShowGroupPanel = false;
            grdRowData.View.OptionsBehavior.Editable = false;

            #endregion


            #region Over Rule
            grdOverRules.GridButtonItem = GridButtonItem.Export;
            grdOverRules.View.SetIsReadOnly();
            //grdOverRules.View.AddTextBoxColumn("TEMPID", 120);
            //grdOverRules.View.AddTextBoxColumn("GROUPID", 120);
            //grdOverRules.View.AddTextBoxColumn("SUBGROUP", 120);
            grdOverRules.View.AddTextBoxColumn("SUBGROUPNAME", 120).SetLabel("SUBGROUPNAMEVIEW");
            grdOverRules.View.AddTextBoxColumn("SAMPLINGNAME", 120).SetLabel("SAMPLINGXAXIS");

            grdOverRules.View.AddTextBoxColumn("INSPECTIONDATETIMEVIEWDATE", 100);
            grdOverRules.View.AddTextBoxColumn("TXNHISTKEY", 100);
            grdOverRules.View.AddTextBoxColumn("RESOURCETYPE", 100);
            grdOverRules.View.AddTextBoxColumn("INSPECTIONTYPE", 100);
            grdOverRules.View.AddTextBoxColumn("LOTID", 100);
            grdOverRules.View.AddTextBoxColumn("INSPITEMID", 100);
            grdOverRules.View.AddTextBoxColumn("INSPITEMVERSION", 100);
            grdOverRules.View.AddTextBoxColumn("INSPITEMIDNAME", 100);
            grdOverRules.View.AddTextBoxColumn("PROCESSRELNO", 100);
            grdOverRules.View.AddTextBoxColumn("AREAID", 100);
            grdOverRules.View.AddTextBoxColumn("PRODUCTDEFID", 100);
            grdOverRules.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            grdOverRules.View.AddTextBoxColumn("PRODUCTDEFIDNAME", 100);
            grdOverRules.View.AddTextBoxColumn("PROCESSSEGMENTID", 100);
            grdOverRules.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 100);
            grdOverRules.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 100);
            grdOverRules.View.AddTextBoxColumn("EQUIPMENTID", 100);
            grdOverRules.View.AddTextBoxColumn("EQUIPMENTNAME", 100);
            grdOverRules.View.AddTextBoxColumn("INSPECTIONDEFID", 100);
            grdOverRules.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 100);
            grdOverRules.View.AddTextBoxColumn("DEFECTCODE", 100);
            grdOverRules.View.AddTextBoxColumn("DEFECTNAME", 100);
            grdOverRules.View.AddTextBoxColumn("ENTERPRISEID", 100);
            grdOverRules.View.AddTextBoxColumn("PLANTID", 100).SetLabel("SITE");
            grdOverRules.View.AddTextBoxColumn("NVALUE", 100).SetLabel("DEFECTCOUNT");
            grdOverRules.View.AddTextBoxColumn("NSUBVALUE", 100).SetLabel("NCRINSPECTIONQTY");
            grdOverRules.View.AddTextBoxColumn("SAMPLEQTY", 100);

            //grdOverRules.View.AddTextBoxColumn("MAX", 120);
            //grdOverRules.View.AddTextBoxColumn("MIN", 120);
            //grdOverRules.View.AddTextBoxColumn("R", 120);
            //grdOverRules.View.AddTextBoxColumn("RUCL", 120);
            //grdOverRules.View.AddTextBoxColumn("RLCL", 120);
            //grdOverRules.View.AddTextBoxColumn("RCL", 120);
            grdOverRules.View.AddTextBoxColumn("BAR", 120).SetLabel("VALUE");

            //grdOverRules.View.AddTextBoxColumn("USL", 120);
            //grdOverRules.View.AddTextBoxColumn("LSL", 120);
            //grdOverRules.View.AddTextBoxColumn("CSL", 120);

            grdOverRules.View.AddTextBoxColumn("UCL", 120);
            grdOverRules.View.AddTextBoxColumn("LCL", 120);
            grdOverRules.View.AddTextBoxColumn("CL", 120);
            //grdOverRules.View.AddTextBoxColumn("XBAR", 120);
            //grdOverRules.View.AddTextBoxColumn("RBAR", 120);
            //grdOverRules.View.AddTextBoxColumn("SUMVALUE", 120);
            //grdOverRules.View.AddTextBoxColumn("SUMSUBVALUE", 120);
            //grdOverRules.View.AddTextBoxColumn("TOTSUM", 120);
            //grdOverRules.View.AddTextBoxColumn("TOTAVGVALUE", 120);
            //grdOverRules.View.AddTextBoxColumn("SAMESIGMA", 120);
            //grdOverRules.View.AddTextBoxColumn("ISSAME", 120);
            //grdOverRules.View.AddTextBoxColumn("STDEVALUE", 120);
            //grdOverRules.View.AddTextBoxColumn("NN", 120);
            //grdOverRules.View.AddTextBoxColumn("SNN", 120);
            //grdOverRules.View.AddTextBoxColumn("TOTNN", 120);
            //grdOverRules.View.AddTextBoxColumn("GROUPNN", 120);

            grdOverRules.View.PopulateColumns();

            #endregion

        }

        /// <summary>
        /// Control Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            tabMain.SetLanguageKey(tabTrend, "TREND");
            tabMain.SetLanguageKey(tabControlChart, "CONTROLCHART");
            tabMain.SetLanguageKey(tabProcess, "PROCESSCAPABILITY");
            tabMain.SetLanguageKey(tabRowData, "TABROWDATA");
            tabMain.SetLanguageKey(tabOverRules, "OVERRULES");
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.smartSpliterContainer1.PanelVisibility = SplitPanelVisibility.Panel1;
            //gropup box 1 확대
            this.lblExtension1.Click += (s, e) =>
            {
                this.splMain.PanelVisibility = SplitPanelVisibility.Panel1;
            };

            //gropup box 1 축소
            this.lblReduction1.Click += (s, e) =>
            {
                this.splMain.PanelVisibility = SplitPanelVisibility.Both;
            };

            // 화면 Resize
            this.Resize += (s, e) =>
            {
                try
                {
                    int position = 190;
                    int rate = 3;
                    (tabControlChart.Controls[0] as ucXBarFrame).splMain.SplitterPosition = position;
                    (tabControlChart.Controls[0] as ucXBarFrame).splMainSub.SplitterPosition = this.Height - (this.Height / rate);
                    (tabProcess.Controls[0] as ucCpkFrame).splMain.SplitterPosition = position;
                    (tabProcess.Controls[0] as ucCpkFrame).splMainSub.SplitterPosition = this.Height - (this.Height / rate);
                }
                catch (Exception)
                {
                    //throw;
                }
            };

            // 품목 별 Trend Merge
            gridDefect.View.CellMerge += (s, e) =>
            {
                if (!(s is DevExpress.XtraGrid.Views.Grid.GridView view))
                {
                    return;
                }

                if (e.Column.FieldName == "ITEMCODE")
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

            // 관리도 Cpk Chart Enter 이벤트
            (tabControlChart.Controls[0] as ucXBarFrame).SpcCpkChartEnterEventHandler += (f) =>
            {
                _isAgainAnalysisXBar = f.isAgainAnalysis;
                _isAgainAnalysisCpk = f.isAgainAnalysis;
                _isAgainAnalysisPlot = f.isAgainAnalysis;
                _AnalysisParameter.spcOption = f.SPCOption;
            };

            // Tab 변경시 이벤트
            tabMain.SelectedPageChanged += (s, e) =>
            {
                ChartAnalysiSExecution((s as SmartTabControl).SelectedTabPageIndex);
            };
        }
        /// <summary>
        /// 폼 Load 이베튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QualityAnalysisSpcStatus_Load(object sender, EventArgs e)
        {
            //this.tabAnalysis.PageVisible = false;
            //(tabControlChart.Controls[0] as ucXBarFrame).SpcCpkChartEnterEventHandler += QualityAnalysisSpcStatus_SpcCpkChartEnterEventHandler;
            //ucXBar.SpcCpkChartEnterEventHandler += UcXBar_SpcCpkChartEnterEventHandler;
            SpcClass.SpcDictionaryDataSetting();
            this.ucXBar.ChartLegendsTitleChange("P");
            this.ucXBar.SpcChartDirectMessageUserEventHandler += UcXBar_SpcChartDirectMessageUserEventHandler;
            this.ucXBar.SpcChartVtPChartTypeChangeEventHandler += UcXBar_SpcChartVtPChartTypeChangeEventHandler;
            InitializeGrid();
            CoboBoxSettingValueType();
            FormResize();
            _isFirstEvents = true;
        }

        /// <summary>
        /// P Chart ComboBox Change Event시 OverRule 재처리 이벤트 발생.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="se"></param>
        private void UcXBar_SpcChartVtPChartTypeChangeEventHandler(object sender, EventArgs e, SpcEventsOption se)
        {
            _isAgainAnalysisOverRules = se.isPcChartType;
        }



        /// <summary>
        /// Chart Message 이벤트
        /// </summary>
        /// <param name="msg"></param>
        private void UcXBar_SpcChartDirectMessageUserEventHandler(SpcEventsChartMessage msg)
        {
            ShowMessage(msg.mainMessage);
        }

        /// <summary>
        /// Chart Option 변경.
        /// </summary>
        /// <param name="f"></param>
        private void QualityAnalysisSpcStatus_SpcCpkChartEnterEventHandler(SpcFrameChangeData f)
        {
            _isAgainAnalysisXBar = f.isAgainAnalysis;
            _isAgainAnalysisCpk = f.isAgainAnalysis;
            _isAgainAnalysisPlot = f.isAgainAnalysis;
            _AnalysisParameter.spcOption = f.SPCOption;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboDefect_TextChanged(object sender, EventArgs e)
        {
            DefectRateListChnage(0);
        }
        private void cboProduct_TextChanged(object sender, EventArgs e)
        {
            DefectRateListChnage(1);
        }
        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            try
            {
                

                await base.OnSearchAsync();

                var values = Conditions.GetValues();
                values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

                //사용자 편집.
                Dictionary<string, object> param = values;
                string dateStart = param["P_PERIOD_PERIODFR"].ToSafeString();
                DateTime dtStart = Convert.ToDateTime(dateStart);
                dateStart = dtStart.ToString("yyyy-MM-dd HH:mm:ss");
                string dateEnd = param["P_PERIOD_PERIODTO"].ToSafeString();
                DateTime dtEnd = Convert.ToDateTime(dateEnd);
                dateEnd = dtEnd.ToString("yyyy-MM-dd HH:mm:ss");
                values["P_PERIOD_PERIODFR"] = dateStart;
                values["P_PERIOD_PERIODTO"] = dateEnd;

                string subgroupType = param["P_SPCAXISTYPE"].ToSafeString();
                string inspectionResultResouceType = param["P_INSPECTIONRESULTRESOUCETYPE"].ToSafeString();
                _date = Convert.ToDateTime((Conditions.GetValue("P_PERIOD") as Dictionary<string, object>)["P_PERIOD_PERIODFR"]);
                _previousMonth = _date.AddMonths(-1).ToString("yyyy-MM");

                values.Add("P_FROM", string.Concat(_previousMonth, "\', \'", _date.ToString("yyyy-MM")));


                if (inspectionResultResouceType != null && inspectionResultResouceType != "")
                {
                }
                else
                {
                    // 조회할 데이터가 없습니다.
                    //다국어 처리.
                    ShowMessage("검사구분 코드는 필수 선택 항목입니다.");
                    return;
                }

                DialogManager.ShowWaitArea(this.pnlContent);

                //초기화
                DataTable InputRawMaxCount;
                DataTable tempInputSpec;
                tempInputSpec = SpcClass.SpcCreateMainRawSpecTable();
                this.grdRawData.DataSource = null;
                this.grdRawData.View.ClearDatas();
                this.grdOverRules.DataSource = null;
                this.grdOverRules.View.ClearDatas();
                this.gridTotal.DataSource = null;
                this.gridDefect.DataSource = null;
                this.gridProduct.DataSource = null;
                this.gridTotal.View.ClearDatas();
                this.gridDefect.View.ClearDatas();
                this.gridProduct.View.ClearDatas();
                _dtDefactDetailDefect = null;
                _dtDefactDetailProduct = null;

                this.ucXBar.ChartLegendsTitleChange("P");

                if (_AnalysisParameter.dtInputRawData != null)
                {
                    _AnalysisParameter.dtInputRawData.Clear();
                }

                //전체 조회 Count 조회.
                InputRawMaxCount = await SqlExecuter.QueryAsync("GetSpcFinalInspectionRawMaxCount", "10001", values);
                if (InputRawMaxCount != null && InputRawMaxCount.Rows.Count < 1)
                {
                    // 조회할 데이터가 없습니다.
                    ShowMessage("NoSelectData");
                    return;
                }

                //조회 자료 전체 건수 확인.
                long? nQueryMaxCount = InputRawMaxCount.Rows[0]["RECORDMAXCOUNT"].ToNullOrInt64();
                if (nQueryMaxCount <= 0)
                {
                    // 조회할 데이터가 없습니다.
                    ShowMessage("NoSelectData");
                    return;
                }

                //DB Data 부분 수집 여부 Check
                if (nQueryMaxCount < SpcFlag.nSpcQueryMaxCount)
                {
                    _AnalysisParameter.dtInputRawData = await SqlExecuter.QueryAsync("GetSpcFinalInspectionRaw", "10001", values);
                }
                else
                {
                    await this.InputDataMerge(dateStart, dateEnd, values);
                }

                //자료 조회    
                //if (await SqlExecuter.QueryAsync("GetSpcFinalInspectionRaw", "10001", values) is DataTable dt)
                if(_AnalysisParameter.dtInputRawData != null)
                {
                    if (_AnalysisParameter.dtInputRawData.Rows.Count < 0)
                    {
                        this.InitializeGrid();
                        ShowMessage("NoSelectData");
                        return;
                    }

                    #region Trend Chart

                    //var list = dt.AsEnumerable().Where(x => x.Field<string>("SUBGROUP") == _date.ToString("yyyy-MM").Replace("-", "/"));

                    //if (list != null && list.Count() != 0)
                    //{
                    //    SetPChart(list.CopyToDataTable());
                    //}

                    //_previousMonth = Convert.ToDateTime(_previousMonth).ToString("yyyy/M").Replace("-", "/");
                    //_month = _date.ToString("yyyy/M").Replace("-", "/");

                    //_trendData = TrendClass.Create(dt);

                    //InitializeGrid();
                    //SetTrandData();
                    //SetTrandProduct();

                    //(gridTotal.DataSource as DataTable).AcceptChanges();
                    //(gridDefect.DataSource as DataTable).AcceptChanges();

                    #endregion


                    #region 5.통계용 입력자료 DB 조회
                    
                    //Chart 구분
                    string chartType = "";
                    //chartType = ucXBarFrame1.cboLeftChartType.GetDataValue().ToSafeString().ToUpper().Replace("_", "");
                    chartType = "P";
                    _AnalysisParameter.spcOption.specDefaultChartType = "P";
                    _AnalysisParameter.spcOption.chartName.xBarChartType = chartType;
                    _AnalysisParameter.spcOption.chartName.xCpkChartType = chartType;
                    _AnalysisParameter.spcOption.ChartTypeSetting(chartType, ref _AnalysisParameter.spcOption);
                    _AnalysisParameter.spcOption.sigmaType = SigmaType.Yes;//추정치 사용 유무

                    //*통계 분석 실행
                    _isAgainAnalysisXBar = true;
                    _isAgainAnalysisCpk = true;
                    _isAgainAnalysisPlot = true;

                    _AnalysisParameter.dtInputRawMainFieldName.subgroupNameFiledName = "RELIASUBGROUPNAME";

                    switch (chartType.ToUpper())
                    {
                        case "P":
                        case "NP":
                        case "C":
                        case "U":
                        case "I":
                        case "MR":
                            _AnalysisParameter.dtInputRawMainFieldName.subgroupNameFiled = "SUBGROUP";
                            _AnalysisParameter.dtInputRawMainFieldName.subgroupNameFiledName = "SUBGROUPNAMEDEFECT";
                            _AnalysisParameter.dtInputRawMainFieldName.subgroupNameFiledName01 = "SUBGROUPNAMEPRODUCT";
                            switch (subgroupType)
                            {
                                case "LOTID":
                                    _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiled = "SAMPLINGIMRLOTID";
                                    _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiledName = "SAMPLINGIMRNAMELOTID";
                                    _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiled01 = "SAMPLINGIMRLOTID";
                                    _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiledName01 = "SAMPLINGIMRNAMELOTID";
                                    break;
                                case "DATE":
                                default:
                                    _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiled = "SAMPLINGIMR";
                                    _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiledName = "SAMPLINGIMRNAME";
                                    _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiled01 = "SAMPLING";
                                    _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiledName01 = "SAMPLINGNAME";
                                    break;
                            }
                            break;
                        default:
                            switch (subgroupType)
                            {
                                case "LOTID":
                                    _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiled = "SAMPLINGLOTID";
                                    _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiledName = "SAMPLINGNAMELOTID";
                                    _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiled01 = "";
                                    _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiledName01 = "";
                                    break;
                                case "DATE":
                                default:
                                    _AnalysisParameter.dtInputRawMainFieldName.Clear(ref _AnalysisParameter.dtInputRawMainFieldName);
                                    _AnalysisParameter.dtInputRawMainFieldName.subgroupNameFiledName = "RELIASUBGROUPNAME";
                                    break;
                            }

                            break;
                    }

                    //Chart Data 생성.
                    SPCPara spcPara = new SPCPara();
                    spcPara.spcOption = SPCOption.Create();
                    spcPara.spcOption.limitType = LimitType.Interpretation;
                    spcPara.tableRawData = _AnalysisParameter.dtInputRawData;
                    spcPara.tableSubgroupSpec = null;
                    spcPara.ChartTypeMain("P");

                    //통계실행.
                    this.ucXBar.DirectXBarRExcute(ref spcPara, _AnalysisParameter);

                    _isAgainAnalysisOverRules = true;

                    //불량률 계산 및 표시
                    this.DefectRateListView(spcPara);

                    this.grdRowData.DataSource = _AnalysisParameter.dtInputRawData;

                    #endregion


                }
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

        #region 검색 Data 부분 조회후 Merge 처리 부분.
        /// <summary>
        /// 입력자료 Merge 함수
        /// </summary>
        /// <param name="startDate">시작일자</param>
        /// <param name="endDate">종료일자</param>
        /// <param name="param">검색조건</param>
        /// <returns></returns>
        private async Task InputDataMerge(string startDate, string endDate, Dictionary<string, object> param)
        {
            List<SpcConditionDateDays> rtnlstDay;
            DataTable dtInputFirstData = new DataTable();
            //var values = Conditions.GetValues();
            //string startDate = "2020-02-01 08:30:00";
            //string endDate = "2020-03-01 08:30:00";
            rtnlstDay = SpcClass.SpcConditionList(startDate, endDate);
            //await base.OnSearchAsync();
            int niCount = 0;
            foreach (SpcConditionDateDays days in rtnlstDay)
            {
                niCount++;
                param["P_PERIOD_PERIODFR"] = days.DateStart;
                param["P_PERIOD_PERIODTO"] = days.DateEnd;
                Console.WriteLine(string.Format("{0} : {1} ~ {2}", niCount, days.DateStart, days.DateEnd));
                if (niCount != 1)
                {
                    DataTable dtInputPartData;
                    dtInputPartData = await SqlExecuter.QueryAsync("GetSpcFinalInspectionRaw", "10001", param);
                    if (dtInputPartData != null && dtInputPartData.Rows.Count < 1)
                    {
                        //Next Sentence...
                    }
                    else
                    {
                        for (int i = 0; i < dtInputPartData.Rows.Count; i++)
                        {
                            DataRow rowData = dtInputPartData.Rows[i];
                            dtInputFirstData.ImportRow(rowData);
                        }
                    }

                    Console.WriteLine(dtInputFirstData.Rows.Count);
                }
                else
                {

                    dtInputFirstData = await SqlExecuter.QueryAsync("GetSpcFinalInspectionRaw", "10001", param);
                    if (dtInputFirstData != null && dtInputFirstData.Rows.Count < 1)
                    {
                        niCount = 0;
                    }
                }
            }
            //17010

            if (dtInputFirstData != null && dtInputFirstData.Rows.Count > 0)
            {
                _AnalysisParameter.dtInputRawData = dtInputFirstData;
            }

            Console.WriteLine("1");
        }

        ///// <summary>
        ///// Spec Data - DataTable
        ///// </summary>
        ///// <param name="tableName"></param>
        ///// <returns></returns>
        //public DataTable CreateSpecTable(string tableName = "dtTempSpecData")
        //{
        //    DataTable dt = new DataTable();
        //    dt.TableName = tableName;
        //    //Filed
        //    dt.Columns.Add("SUBGROUP", typeof(string));
        //    dt.Columns.Add("CTYPE", typeof(string));
        //    dt.Columns.Add("SPECTYPE", typeof(string));
        //    dt.Columns.Add("USL", typeof(double));
        //    dt.Columns.Add("SL", typeof(double));
        //    dt.Columns.Add("LSL", typeof(double));
        //    dt.Columns.Add("LCL", typeof(double));
        //    dt.Columns.Add("CL", typeof(double));
        //    dt.Columns.Add("UOL", typeof(double));
        //    dt.Columns.Add("LOL", typeof(double));
        //    return dt;
        //}
        /// <summary>
        /// 합계 Data - DataTable
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable CreateChartSumTable(string tableName = "dtChartSumTable")
        {
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            //Filed
            dt.Columns.Add("TEMPID", typeof(long));
            dt.Columns.Add("GROUPID", typeof(long));
            dt.Columns.Add("SUBGROUP", typeof(string));
            dt.Columns.Add("XBAR", typeof(double));
            dt.Columns.Add("RBAR", typeof(double));
            dt.Columns.Add("SUMSTDEV", typeof(double));
            dt.Columns.Add("SUMSTDEVBAR", typeof(double));
            dt.Columns.Add("SUMSTDEVP", typeof(double));
            dt.Columns.Add("SUMSTDEVPBAR", typeof(double));
            dt.Columns.Add("SUMVA", typeof(double));
            dt.Columns.Add("TOTSUMVALUE", typeof(double));
            dt.Columns.Add("TOTSUMSUBVALUE", typeof(double));
            dt.Columns.Add("SUMTOTDEVSQU", typeof(double));
            dt.Columns.Add("AVGVALUE", typeof(double));
            dt.Columns.Add("TOTAVGVALUE", typeof(double));
            dt.Columns.Add("SAMESIGMA", typeof(double));
            dt.Columns.Add("ISSAME", typeof(bool));
            dt.Columns.Add("STDEVALUE", typeof(double));
            dt.Columns.Add("NN", typeof(long));
            dt.Columns.Add("SNN", typeof(ulong));
            dt.Columns.Add("TOTNN", typeof(ulong));
            dt.Columns.Add("GROUPNN", typeof(long));

            return dt;
        }

        /// <summary>
        /// 품질규격 Over Rules Grid DataTable
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable CreateOverRulesTable(string tableName = "dtOverRulesTable")
        {
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            //Filed
            dt.Columns.Add("TEMPID", typeof(long));
            dt.Columns.Add("GROUPID", typeof(long));
            dt.Columns.Add("SUBGROUP", typeof(string));
            dt.Columns.Add("SUBGROUPNAME", typeof(string));
            dt.Columns.Add("SAMPLING", typeof(string));
            dt.Columns.Add("SAMPLINGNAME", typeof(string));

            //업무별 수정 Start ----------------------------------
            dt.Columns.Add("INSPECTIONDATETIMEVIEWDATE", typeof(string));
            dt.Columns.Add("TXNHISTKEY", typeof(string));
            dt.Columns.Add("RESOURCETYPE", typeof(string));
            dt.Columns.Add("INSPECTIONTYPE", typeof(string));
            dt.Columns.Add("LOTID", typeof(string));
            dt.Columns.Add("INSPITEMID", typeof(string));
            dt.Columns.Add("INSPITEMVERSION", typeof(string));
            dt.Columns.Add("INSPITEMIDNAME", typeof(string));
            dt.Columns.Add("PROCESSRELNO", typeof(string));
            dt.Columns.Add("AREAID", typeof(string));
            dt.Columns.Add("PRODUCTDEFID", typeof(string));
            dt.Columns.Add("PRODUCTDEFVERSION", typeof(string));
            dt.Columns.Add("PRODUCTDEFIDNAME", typeof(string));
            dt.Columns.Add("PROCESSSEGMENTID", typeof(string));
            dt.Columns.Add("PROCESSSEGMENTVERSION", typeof(string));
            dt.Columns.Add("PROCESSSEGMENTNAME", typeof(string));
            dt.Columns.Add("EQUIPMENTID", typeof(string));
            dt.Columns.Add("EQUIPMENTNAME", typeof(string));
            dt.Columns.Add("INSPECTIONDEFID", typeof(string));
            dt.Columns.Add("INSPECTIONDEFVERSION", typeof(string));
            dt.Columns.Add("DEFECTCODE", typeof(string));
            dt.Columns.Add("DEFECTNAME", typeof(string));
            dt.Columns.Add("ENTERPRISEID", typeof(string));
            dt.Columns.Add("PLANTID", typeof(string));
            dt.Columns.Add("NVALUE", typeof(double));
            dt.Columns.Add("NSUBVALUE", typeof(double));
            dt.Columns.Add("SAMPLEQTY", typeof(double));
            //업무별 수정 End ----------------------------------

            dt.Columns.Add("MAX", typeof(double));
            dt.Columns.Add("MIN", typeof(double));
            dt.Columns.Add("R", typeof(double));
            dt.Columns.Add("RUCL", typeof(double));
            dt.Columns.Add("RLCL", typeof(double));
            dt.Columns.Add("RCL", typeof(double));
            dt.Columns.Add("BAR", typeof(double));

            dt.Columns.Add("USL", typeof(double));//추가
            dt.Columns.Add("LSL", typeof(double));//추가
            dt.Columns.Add("CSL", typeof(double));//추가

            dt.Columns.Add("UCL", typeof(double));
            dt.Columns.Add("LCL", typeof(double));
            dt.Columns.Add("CL", typeof(double));
            dt.Columns.Add("XBAR", typeof(double));
            dt.Columns.Add("RBAR", typeof(double));
            dt.Columns.Add("SUMVALUE", typeof(double));
            dt.Columns.Add("SUMSUBVALUE", typeof(double));
            dt.Columns.Add("TOTSUM", typeof(double));
            dt.Columns.Add("TOTAVGVALUE", typeof(double));
            dt.Columns.Add("SAMESIGMA", typeof(double));
            dt.Columns.Add("ISSAME", typeof(bool));
            dt.Columns.Add("STDEVALUE", typeof(double));
            dt.Columns.Add("NN", typeof(long));
            dt.Columns.Add("SNN", typeof(ulong));
            dt.Columns.Add("TOTNN", typeof(ulong));
            dt.Columns.Add("GROUPNN", typeof(long));
            return dt;
        }


        public bool OverRulesDataCheck(DataRow dataRowOverRule)
        {
            bool returnValue = false;
            SpcSpec overRuleCheckData = SpcSpec.Create();

            try
            {
                overRuleCheckData.XBar = dataRowOverRule["BAR"].ToSafeDoubleStaMin();
                overRuleCheckData.XRar = dataRowOverRule["R"].ToSafeDoubleStaMin();

                overRuleCheckData.usl = dataRowOverRule["USL"].ToSafeDoubleStaMax();
                overRuleCheckData.lsl = dataRowOverRule["LSL"].ToSafeDoubleStaMin();

                overRuleCheckData.ucl = dataRowOverRule["UCL"].ToSafeDoubleStaMax();
                overRuleCheckData.lcl = dataRowOverRule["LCL"].ToSafeDoubleStaMin();
                overRuleCheckData.ccl = dataRowOverRule["CL"].ToSafeDoubleStaMax();

                overRuleCheckData.rUcl = dataRowOverRule["RUCL"].ToSafeDoubleStaMax();
                overRuleCheckData.rLcl = dataRowOverRule["RLCL"].ToSafeDoubleStaMin();
                //overRuleCheckData.r = dataRowOverRule["RCL"].ToSafeDoubleZero();

                //Bar
                if (overRuleCheckData.XBar != SpcLimit.MIN)
                {
                    //Control
                    if (overRuleCheckData.XBar > overRuleCheckData.ucl)
                    {
                        returnValue = true;
                    }

                    if (overRuleCheckData.XBar < overRuleCheckData.lcl)
                    {
                        returnValue = true;
                    }
                    //Spec
                    if (overRuleCheckData.XBar > overRuleCheckData.usl)
                    {
                        returnValue = true;
                    }

                    if (overRuleCheckData.XBar < overRuleCheckData.lsl)
                    {
                        returnValue = true;
                    }

                }

                //R
                if (overRuleCheckData.XRar != SpcLimit.MIN)
                {
                    //Control
                    if (overRuleCheckData.XRar > overRuleCheckData.rUcl)
                    {
                        returnValue = true;
                    }

                    if (overRuleCheckData.XRar < overRuleCheckData.rLcl)
                    {
                        returnValue = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return returnValue;
        }


        #endregion


        /// <summary>
        /// 불량코드별 불량률 표시.
        /// </summary>
        /// <param name="spcPara"></param>
        private void DefectRateListView(SPCPara spcPara)
        {
            #region 불량률 계산
            DataTable dtDefactTotalSum;
            DataTable dtDefactTotal01;
            DataTable dtDefactTotal02;
            DataTable dtDefactView01;
            DataTable dtDefactView02;
            DataTable dtDefactViewSum;
            DataTable dtDefactDetailSum;
            DataTable dtDefactDetail01;
            DataTable dtDefactDetail02;
            string[] gridCaption;
            string statusMessage;
            string valueType01 = this.cboDefect.GetDataValue().ToString();
            string valueType02 = this.cboProduct.GetDataValue().ToString();

            _dtDefactDetailSum = _spcLibHelper.DefectRateList(spcPara, out dtDefactTotalSum, out dtDefactDetailSum, out gridCaption, out statusMessage);
            _dtDefactDetailDefect = _spcLibHelper.DefectRateList(spcPara, out dtDefactTotal01, out  dtDefactDetail01, out gridCaption, out statusMessage);
            _dtDefactDetailProduct = _spcLibHelper.DefectRateList(spcPara, out dtDefactTotal02, out dtDefactDetail02, out gridCaption, out statusMessage, "SUBGROUPNAME01");

            dtDefactViewSum = _spcLibHelper.DefectRateViewSum(dtDefactDetailSum, out statusMessage);
            dtDefactView01 = _spcLibHelper.DefectRateView(_dtDefactDetailDefect, valueType01, out statusMessage);
            dtDefactView02 = _spcLibHelper.DefectRateView(_dtDefactDetailProduct, valueType02, out statusMessage);

            int colIndexDefault = 2;
            int colIndexAdd = 0;

            string defectiveWorst = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCUIDEFECTIVEWORST");//WORST
            string defectiveName = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCUIDEFECTIVENAME");//불량명
            string defectiveCount = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCUIDEFECTIVECOUNT");//불량수량
            string defectiveProductName = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCUIDEFECTIVEPRODUCTNAME");//모델명

            this.gridDefect.View.Columns[0].Caption = defectiveWorst;//WORST
            this.gridDefect.View.Columns[1].Caption = defectiveName;//불량명
            this.gridDefect.View.Columns[2].Caption = defectiveCount;//불량수량

            this.gridProduct.View.Columns[0].Caption = defectiveWorst;//WORST
            this.gridProduct.View.Columns[1].Caption = defectiveProductName;//모델명
            this.gridProduct.View.Columns[2].Caption = defectiveCount;//불량수량

            if (gridCaption != null && gridCaption.Length > 0 && gridCaption[1] != "")
            {
                for (int i = 1; i < 32; i++)
                {
                    colIndexAdd = colIndexDefault + i;
                    this.gridTotal.View.Columns[colIndexAdd].Caption = gridCaption[i];
                    this.gridDefect.View.Columns[colIndexAdd].Caption = gridCaption[i];
                    this.gridProduct.View.Columns[colIndexAdd].Caption = gridCaption[i];
                }
            }
            this.gridTotal.DataSource = null;
            this.gridTotal.DataSource = dtDefactViewSum;

            this.gridDefect.DataSource = null;
            this.gridDefect.DataSource = dtDefactView01;
            this.gridProduct.DataSource = null;
            this.gridProduct.DataSource = dtDefactView02;

            #endregion//불량률 계산
        }

        /// <summary>
        /// 불량코드, 제품별 구분하여 화면 표시.
        /// </summary>
        /// <param name="option">0-Defect, 1-Product</param>
        private void DefectRateListChnage(int option = 0)
        {
            if (!_isFirstEvents) return;

            #region 불량률 계산 
            //string[] gridCaption;
            string statusMessage;
            string valueType01 = this.cboDefect.GetDataValue().ToString();
            string valueType02 = this.cboProduct.GetDataValue().ToString();
            string dataModeCaption = "";

            if (option == 0)
            {
                dataModeCaption = this.cboDefect.GetDisplayText();
                this.gridDefect.View.Columns[2].Caption = dataModeCaption;
                this.gridDefect.DataSource = null;
                if (_dtDefactDetailDefect != null && _dtDefactDetailDefect.Rows.Count > 0)
                {
                    DataTable dtDefactView01 = _spcLibHelper.DefectRateView(_dtDefactDetailDefect, valueType01, out statusMessage);
                    this.gridDefect.DataSource = dtDefactView01;
                }
            }
            else
            {
                dataModeCaption = this.cboProduct.GetDisplayText();
                this.gridProduct.View.Columns[2].Caption = dataModeCaption;
                this.gridProduct.DataSource = null;
                if (_dtDefactDetailProduct != null && _dtDefactDetailProduct.Rows.Count > 0)
                {
                    DataTable dtDefactView02 = _spcLibHelper.DefectRateView(_dtDefactDetailProduct, valueType02, out statusMessage);
                    this.gridProduct.DataSource = dtDefactView02;
                }
            }
            this.gridDefect.View.BestFitColumns();
            this.gridProduct.View.BestFitColumns();
            #endregion//불량률 계산
        }

        /// <summary>
        /// 콤보박스 설정 - Subgroup ID
        /// </summary>
        public void CoboBoxSettingValueType()
        {
            this.cboDefect.DataSource = null;
            DataTable _CboDefectValueType = CreateDataTableDefectValueType("cboDefect");
            DataTable _CboProductValueType = CreateDataTableDefectValueType("cboProduct");
            if (_CboDefectValueType != null && _CboDefectValueType.Rows.Count > 0)
            {
                this.cboDefect.DataSource = _CboDefectValueType;
                this.cboDefect.DisplayMember = "Label";
                this.cboDefect.ValueMember = "nValue";
                this.cboDefect.Columns[1].Visible = false;
                this.cboDefect.ShowHeader = false;
                this.cboDefect.ItemIndex = 0;
                this.cboDefect.ItemIndex = 0;

                this.cboProduct.DataSource = _CboProductValueType;
                this.cboProduct.DisplayMember = "Label";
                this.cboProduct.ValueMember = "nValue";
                this.cboProduct.Columns[1].Visible = false;
                this.cboProduct.ShowHeader = false;
                this.cboProduct.ItemIndex = 0;
                this.cboProduct.ItemIndex = 0;
            }
        }

        /// <summary>
        /// 불량율 값 구분 자료
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable CreateDataTableDefectValueType(string tableName = "cboDefect")
        {
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            //Filed
            dt.Columns.Add("Label", typeof(string));
            dt.Columns.Add("nValue", typeof(string));

            string defectiveCount = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCUIDEFECTIVECOUNT");//불량수량
            string defectiveRate = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCUIDEFECTIVERATE");//불량률
            string defectiveCountRate = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCUIDEFECTIVECOUNTRATE");//불량수량(불량률)

            DataRow row;
            row = dt.NewRow(); row["Label"] = defectiveCount; row["nValue"] = DefectValueType.value.ToString(); dt.Rows.Add(row);
            row = dt.NewRow(); row["Label"] = defectiveRate; row["nValue"] = DefectValueType.rate.ToString(); dt.Rows.Add(row);
            row = dt.NewRow(); row["Label"] = defectiveCountRate; row["nValue"] = DefectValueType.valueAndRate.ToString(); dt.Rows.Add(row);

            return dt;
        }
        /// <summary>
        /// 백업 비동기 override 모델
        /// </summary>
        protected void OnSearchAsync_B201901010()
        {
            DataTable dt = new DataTable();
            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);

                //await base.OnSearchAsync();

                var value = Conditions.GetValues();
                _date = Convert.ToDateTime((Conditions.GetValue("P_PERIOD") as Dictionary<string, object>)["P_PERIOD_PERIODFR"]);
                _previousMonth = _date.AddMonths(-1).ToString("yyyy-MM");

                value.Add("P_FROM", string.Concat(_previousMonth, "\', \'", _date.ToString("yyyy-MM")));
                
                //if (await SqlExecuter.QueryAsync("GetQualityAnalysisTrend", "10002", value) is DataTable dt)
                if(true)
                {
                    
                    if (dt == null || dt.Rows.Count < 1)
                    {
                        ShowMessage("NoSelectData");
                        return;
                    }

                    ucXBar.ControlChartDataMappingClear();

                    gridTotal.View.ClearColumns();
                    gridTotal.DataSource = null;

                    gridDefect.View.ClearColumns();
                    gridDefect.DataSource = null;

                    #region Trend Chart

                    var list = dt.AsEnumerable().Where(x => x.Field<string>("SUBGROUP") == _date.ToString("yyyy-MM").Replace("-", "/"));

                    if (list != null && list.Count() != 0)
                    {
                        SetPChart(list.CopyToDataTable());
                    }

                    _previousMonth = Convert.ToDateTime(_previousMonth).ToString("yyyy/M").Replace("-", "/");
                    _month = _date.ToString("yyyy/M").Replace("-", "/");

                    _trendData = TrendClass.Create(dt);

                    InitializeGrid();
                    SetTrandData();
                    SetTrandProduct();

                    (gridTotal.DataSource as DataTable).AcceptChanges();
                    (gridDefect.DataSource as DataTable).AcceptChanges();

                    #endregion

                    #region SPC

                    _AnalysisParameter.dtInputRawData = dt;

                    //if(await SqlExecuter.QueryAsync("", "10001", value) is DataTable spcDt)
                    //{
                    //    if (spcDt == null || spcDt.Rows.Count < 1)
                    //    {
                    //        ShowMessage("NoSelectSpc");
                    //        return;
                    //    }

                    //_AnalysisParameter.dtInputSpecData = spcDt;
                    _AnalysisParameter.dtInputSpecData = null;

                    ucXBarFrame frame = tabControlChart.Controls[0] as ucXBarFrame;

                    string chartType = frame.cboLeftChartType.GetDataValue().ToSafeString().ToUpper().Replace("_", "");
                    _AnalysisParameter.spcOption.chartName.xBarChartType = chartType;
                    _AnalysisParameter.spcOption.chartName.xCpkChartType = chartType;
                    _AnalysisParameter.spcOption.sigmaType = frame.chkLeftEstimate.Checked ? SigmaType.Yes : SigmaType.No;//추정치 사용 유무

                    //*통계 분석 실행
                    _isAgainAnalysisExe = true;
                    _isAgainAnalysisXBar = true;
                    _isAgainAnalysisCpk = true;
                    _isAgainAnalysisPlot = true;


                    frame.AnalysisExecution(_AnalysisParameter);
                    //}

                    #endregion
                }
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

        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            //Site 공장
            this.Conditions.AddComboBox("p_plantId", new SqlQuery("GetPlantListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "PLANTNAME", "PLANTID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetDefault(UserInfo.Current.Plant)
                .SetValidationIsRequired()
                .SetLabel("PLANT")
                .SetPosition(1.1); //표시순서 지정
            //업무 구분
            this.Conditions.AddComboBox("P_INSPECTIONRESULTRESOUCETYPE", new SqlQuery("GetSpcSearchComboCom", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"P_CODECLASSID={"SpcFinalAndSelfResouceType"}"), "CODENAME", "CODEID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                //.SetDefault("FinishInspection")//20191211 코드 기준.
                .SetValidationIsRequired()
                .SetLabel("INSEPCTIONDEFCATGNAME")//"검사구분"
                .SetPosition(2.1); // Subgroup 조회조건 
            //Chart Subgroup 구분
            this.Conditions.AddComboBox("p_SpcAxisType", new SqlQuery("GetSpcSearchComboCom", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"P_CODECLASSID={"SpcAxisType"}"), "CODENAME", "CODEID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetDefault("DATE")
                .SetValidationIsRequired()
                .SetLabel("SPCSEARCHCONDITION")//조회조건 
                .SetPosition(1.1); // Subgroup 조회조건 

            // 품목p_productdefId
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 2.4, false, Conditions);

            ////품목(제품, 모델) Popup
            //InitializeConditionPopup_Product();//4
            //Lot Id
            InitializeConditionPopup_LotId();//5
            //표준공정(원인공정)
            InitializeConditionPopup_ReasonSegment();//6
            //작업장
            InitializeConditionPopup_Area();//7
            //검사항목
            InitializeConditionPopup_InspItem();//8
            //설비
            InitializeConditionPopup_Equipment();//9
            //설비호기
            //InitializeConditionPopup_EquipmentStage();//10
        }

        #region 조회조건 팝업

        /// <summary>
        /// 품목 팝업
        /// </summary>
        /// <param name="id">검색조건 항목에 지정할 ID</param>
        /// <param name="position">검색조건을 추가할 순서</param>
        /// <param name="isMultiSelect">항목 복수 선택 여부</param>
        /// <param name="conditions">화면에서 사용되는 검색조건 컬렉션</param>
        /// <returns></returns>
        public static ConditionCollection AddConditionProductPopup_Sample(string id, double position, bool isMultiSelect, ConditionCollection conditions)
        {
            // SelectPopup 항목 추가
            var conditionProductId = conditions.AddSelectPopup(id, new SqlQuery("GetProductDefinitionList", "10001", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEFNAME", "PRODUCTDEFID")
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("UNIT")
                .SetLabel("PRODUCTDEFID")
                .SetPosition(position);

            // 복수 선택 여부에 따른 Result Count 지정
            if (isMultiSelect)
                conditionProductId.SetPopupResultCount(0);
            else
                conditionProductId.SetPopupResultCount(1);

            // 팝업에서 사용되는 검색조건 (품목코드/명)
            conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
            //제품 구분 추가 2019.08.14 배선용, 노석안 대리 요청
            conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                                        .SetDefault("Product");

            // 팝업 그리드에서 보여줄 컬럼 정의
            // 품목코드
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            // 품목명
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // 품목버전
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            // 품목유형구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 생산구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTIONTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 단위
            conditionProductId.GridColumns.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID");



            return conditions;
        }

        /// <summary>
        /// 품목 조회조건
        /// </summary>
        private void InitializeConditionPopup_Product()
        {
            // 팝업 컬럼설정
            var productPopup = Conditions.AddSelectPopup("p_productdefId", new SqlQuery("GetSpcSearchProduct", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFNAME", "PRODUCTDEFID")
               .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(400, 600)
               .SetLabel("PRODUCT")
               //.SetDefault("1","2","3")
               .SetPopupResultCount(1)
               .SetPosition(2.4)
               //.SetValidationIsRequired()
               .SetRelationIds("p_plantId");
            // 팝업 조회조건
            productPopup.Conditions.AddTextBox("PRODUCTDEFIDNAME");
            // 팝업 그리드
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetValidationKeyColumn();
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            //productPopup.DefaultDisplayValue = "*";
            //productPopup.DefaultValue = "*";
            //Conditions.items[3].Control.ForeColor = Color.Red;
            //new System.Collections.Generic.Mscorlib_CollectionDebugView<Micube.Framework.SmartControls.ConditionCollectionItem<System.Windows.Forms.Control>>(Conditions._items).Items[0]
        }
        /// <summary>
        /// 표준공정(원인공정) 조회조건
        /// </summary>
        private void InitializeConditionPopup_ReasonSegment()
        {
            // 팝업 컬럼설정
            var discoverySegment = Conditions.AddSelectPopup("P_PROCESSSEGMENTID", new SqlQuery("GetSpcSearchSmallProcesssegment", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
               .SetPopupLayout("REASONSEGMENT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(400, 600)
               .SetLabel("STANDARDSEGMENT")
               //.SetValidationIsRequired()
               .SetPopupResultCount(1)
               .SetPosition(2.6);

            // 팝업 조회조건
            discoverySegment.Conditions.AddTextBox("PROCESSSEGMENTIDNAME");
            // 팝업 그리드
            discoverySegment.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetValidationKeyColumn();
            discoverySegment.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            //discoverySegment.DefaultValue = "*";
        }
        /// <summary>
        /// 작업장 조회조건
        /// </summary>
        private void InitializeConditionPopup_Area()
        {
            // 팝업 컬럼설정
            var areaPopup = Conditions.AddSelectPopup("p_areaId", new SqlQuery("GetAreaListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID")
               .SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(400, 600)
               .SetLabel("AREA")
               .SetPopupResultCount(1)
               .SetPosition(2.7)
               .SetRelationIds("p_plantId");

            // 팝업 조회조건
            areaPopup.Conditions.AddTextBox("AREAIDNAME")
                .SetLabel("AREAIDNAME");

            // 팝업 그리드
            areaPopup.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetValidationKeyColumn();
            areaPopup.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }
        /// <summary>
        /// 설비 조회조건
        /// </summary>
        private void InitializeConditionPopup_Equipment()
        {
            // 팝업 컬럼설정
            var equipmentPopupColumn = Conditions.AddSelectPopup("p_equipmentId", new SqlQuery("GetSpcSearchEquipment", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTNAME", "EQUIPMENTID")
               .SetPopupLayout("EQUIPMENT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(400, 600)
               .SetLabel("EQUIPMENT")//(설비)
                                     //.SetLabel("EQUIPMENTUNIT")//설비(호기)
               .SetPopupResultCount(1)
               //.SetValidationIsRequired()
               .SetPosition(2.9)
               .SetRelationIds("p_plantId");
            //.SetRelationIds("p_plantId", "p_processsegmentclassId");

            // 팝업 조회조건
            equipmentPopupColumn.Conditions.AddTextBox("EQUIPMENTIDNAME")
                .SetLabel("EQUIPMENTIDNAME");
            // 팝업 그리드
            equipmentPopupColumn.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150)
                .SetValidationKeyColumn();
            equipmentPopupColumn.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200);
            //equipmentPopupColumn.DefaultValue = "*";
        }

        /// <summary>
        /// 설비단 조회조건
        /// </summary>
        private void InitializeConditionPopup_EquipmentStage()
        {
            // 팝업 컬럼설정
            var equipmentStagePopupColumn = Conditions.AddSelectPopup("p_childEquipmentId", new SqlQuery("GetSpcSearchChildEquipment", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CHILDEQUIPMENTNAME", "CHILDEQUIPMENTID")
               .SetPopupLayout("CHILDEQUIPMENT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(400, 600)
               .SetLabel("CHILDEQUIPMENT")
               .SetPopupResultCount(1)
               .SetPosition(2.9)
               .SetRelationIds("p_plantId");

            //.SetRelationIds("p_plantId", "p_equipmentId");

            // 팝업 조회조건
            equipmentStagePopupColumn.Conditions.AddTextBox("CHILDEQUIPMENTIDNAME")
                .SetLabel("CHILDEQUIPMENTIDNAME");

            // 팝업 그리드
            equipmentStagePopupColumn.GridColumns.AddTextBoxColumn("CHILDEQUIPMENTID", 150)
                .SetValidationKeyColumn();
            equipmentStagePopupColumn.GridColumns.AddTextBoxColumn("CHILDEQUIPMENTNAME", 200);
        }
        /// <summary>
        /// 검사항목 조회조건
        /// </summary>
        private void InitializeConditionPopup_InspItem()
        {
            // 팝업 컬럼설정
            var inspItemPopupColumn = Conditions.AddSelectPopup("P_INSPITEMID_IN", new SqlQuery("GetSpcSearchInspitem", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPITEMNAME", "INSPITEMID")
               .SetPopupLayout("CHEMICAL", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(400, 600)
               .SetLabel("INSPITEMID")
               .SetPopupResultCount(2)
               .SetPosition(2.8);
            // .SetRelationIds("p_processsegmentclassId", "p_equipmentId", "p_childEquipmentId");

            // 팝업 조회조건
            inspItemPopupColumn.Conditions.AddTextBox("INSPITEMIDNAME")
                .SetLabel("INSPITEMIDNAME");

            // 팝업 그리드
            inspItemPopupColumn.GridColumns.AddTextBoxColumn("INSPITEMID", 150)
                .SetValidationKeyColumn();
            inspItemPopupColumn.GridColumns.AddTextBoxColumn("INSPITEMNAME", 200);
            inspItemPopupColumn.GridColumns.AddTextBoxColumn("INSPECTIONTYPE", 200);
        }

        /// <summary>
        /// LOTID 조회조건
        /// </summary>
        private void InitializeConditionPopup_LotId()
        {
            // 팝업 컬럼설정
            var inspItemPopupColumn = Conditions.AddSelectPopup("p_LotId_In", new SqlQuery("GetSpcSearchLotId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "LOTID", "LOTID")
               .SetPopupLayout("CHEMICAL", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(400, 600)
               .SetLabel("LOTID")
               .SetPopupResultCount(2)
               .SetPosition(2.5)
               .SetRelationIds("p_plantId");
            // .SetRelationIds("p_processsegmentclassId", "p_equipmentId", "p_childEquipmentId");

            // 팝업 조회조건
            inspItemPopupColumn.Conditions.AddTextBox("LOTID")
                .SetLabel("LOTID");

            // 팝업 그리드
            inspItemPopupColumn.GridColumns.AddTextBoxColumn("LOTID", 150)
                .SetValidationKeyColumn();
            inspItemPopupColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
        }

        #endregion

        #endregion

        #region Private Function

        /// <summary>
        /// Grid에 조회된 월에 일별 동적 컬럼생성
        /// </summary>
        /// <param name="grid"></param>
        private void SetGridColumn(SmartBandedGrid grid)
        {
            int lastDay = DateTime.DaysInMonth(_date.Year, _date.Month);

            for (int i = 0; i < lastDay; i++)
            {
                grid.View.AddTextBoxColumn(string.Concat(_date.Month, "/", (i + 1)), 60)
                         .SetTextAlignment(TextAlignment.Right);
            }
        }

        /// <summary>
        /// P-Chart를 그려준다
        /// </summary>
        /// <param name="dt"></param>
        private void SetPChart(DataTable dt)
        {
            SPCPara spcParaEdit = new SPCPara()
            {
                tableRawData = dt
            };
            spcParaEdit.ChartTypeMain("P");

            ucXBar.DirectXBarRExcute(ref spcParaEdit, _AnalysisParameter);
        }

        /// <summary>
        /// 통계 분석 실행 - 공정능력 실행, 관리도 (Tab 이벤트에서 실행함)
        /// </summary>
        private void ChartAnalysiSExecution(int index)
        {
            string subGroupName = "";

            if (index == 1)
            {
                ucXBarFrame frame = tabControlChart.Controls[0] as ucXBarFrame;

                subGroupName = frame.ucXBarGrid01.grp01.Text.ToSafeString();
                if (_isAgainAnalysisXBar)
                {
                    _isAgainAnalysisXBar = false;
                    frame.AnalysisExecution(_AnalysisParameter);
                }
            }
            else if (index == 2)
            {
                ucCpkFrame frame = tabProcess.Controls[0] as ucCpkFrame;

                subGroupName = frame.ucCpkGrid01.grp01.Text.ToSafeString();
                if (_isAgainAnalysisCpk)
                {
                    _isAgainAnalysisCpk = false;
                    frame.AnalysisExecution(_AnalysisParameter);
                }
            }
            else if (index == 4)
            {
                //Over Rules
                if (_isAgainAnalysisOverRules)
                {
                    this.GridOverRules();
                    _isAgainAnalysisOverRules = false;
                }

            }
        }

        /// <summary>
        /// Trand Summary Data Setting
        /// </summary>
        /// <param name="dt"></param>
        private void SetTrandData()
        {
            SetTrendSum(_previousMonth, "LASTMONTH");
            SetTrendSum(_month, "SUMMARY");

            string day = string.Empty;

            List<TrendClass> daysDt = (from main in _trendData
                                       where main.Month == _month
                                       group main by main.CreatedTime into gr
                                       select new TrendClass
                                       {
                                           Month = gr.Key,
                                           PcsQty = gr.Sum(x => x.PcsQty),
                                           DefectQty = gr.Sum(x => x.DefectQty),
                                           Rate = DefectRate(gr.Sum(x => x.DefectQty), gr.Sum(x => x.PcsQty))
                                       }).ToList();

            if (daysDt == null || daysDt.Count == 0)
            {
                return;
            }

            for (int i = 0; i < DateTime.DaysInMonth(_date.Year, _date.Month); i++)
            {
                day = string.Concat(_date.Month, "/", i + 1);

                TrendClass trendClass = daysDt.Find(x => x.Month == day);

                if (trendClass != null)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        DataRow dr = ((DataRowView)gridTotal.View.GetRow(j)).Row;

                        if (j == 0)
                        {
                            dr[day] = trendClass.PcsQty.ToNullOrDouble(); // 전체 건수
                        }
                        else if (j == 1)
                        {
                            dr[day] = trendClass.DefectQty.ToNullOrDouble(); // 불량 건수
                        }
                        else
                        {
                            dr[day] = trendClass.Rate.ToNullOrDouble(); // 불량율(%)
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Grid Trend Summary에 전월 당월 Data 입력
        /// </summary>
        /// <param name="trendData"></param>
        /// <param name="month"></param>
        /// <param name="column"></param>
        private void SetTrendSum(string month, string column)
        {
            List<TrendClass> MonthDt = (from main in _trendData
                                        where main.Month == month
                                        group main by main.Month into gr
                                        select new TrendClass
                                        {
                                            Month = gr.Key,
                                            PcsQty = gr.Sum(x => x.PcsQty),
                                            DefectQty = gr.Sum(x => x.DefectQty),
                                            Rate = DefectRate(gr.Sum(x => x.DefectQty), gr.Sum(x => x.PcsQty))
                                        }).ToList();

            if (MonthDt.Count > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (gridTotal.View.RowCount < 3)
                    {
                        gridTotal.View.AddNewRow();
                    }

                    DataRow dr = ((DataRowView)gridTotal.View.GetRow(i)).Row;

                    if (i == 0)
                    {
                        dr["INSPTYPE"] = Language.Get("INPUTPCSQTY"); // 전체 건수
                        dr[column] = MonthDt[0].PcsQty.ToNullOrDouble();
                    }
                    else if (i == 1)
                    {
                        dr["INSPTYPE"] = Language.Get("DEFECTQTY"); // 불량 건수
                        dr[column] = MonthDt[0].DefectQty.ToNullOrDouble();
                    }
                    else
                    {
                        dr["INSPTYPE"] = Language.Get("DEFECTRATE"); // 불량율(%)
                        dr[column] = MonthDt[0].Rate.ToNullOrDouble();
                    }
                }
            }
        }

        /// <summary>
        /// 품목 Trand data Setting
        /// </summary>
        private void SetTrandProduct()
        {
            List<TrendClass> productDt = (from main in _trendData
                                          where main.Month == _month
                                          group main by main.ProductDefId into gr
                                          orderby gr.Key
                                          select new TrendClass
                                          {
                                              ProductDefId = gr.Key,
                                              PcsQty = gr.Sum(x => x.PcsQty),
                                              DefectQty = gr.Sum(x => x.DefectQty),
                                              Rate = DefectRate(gr.Sum(x => x.DefectQty), gr.Sum(x => x.PcsQty))
                                          }).ToList();

            if (productDt == null || productDt.Count == 0)
            {
                return;
            }

            foreach (TrendClass tc in productDt)
            {
                int row = 0;

                List<TrendClass> items = SetTrandProductItem(tc.ProductDefId);

                for (int i = 0; i < 3; i++)
                {
                    gridDefect.View.AddNewRow();
                    DataRow dr = ((DataRowView)gridDefect.View.GetRow(gridDefect.View.FocusedRowHandle)).Row;

                    dr["ITEMCODE"] = tc.ProductDefId;

                    if (row == 0)
                    {
                        dr["INSPTYPE"] = Language.Get("INPUTPCSQTY"); // 전체 건수
                        dr["SUMMARY"] = tc.PcsQty.ToNullOrDouble();
                    }
                    else if (row == 1)
                    {
                        dr["INSPTYPE"] = Language.Get("DEFECTQTY"); // 불량 건수
                        dr["SUMMARY"] = tc.DefectQty.ToNullOrDouble();
                    }
                    else
                    {
                        dr["INSPTYPE"] = Language.Get("DEFECTRATE"); // 불량율(%)
                        dr["SUMMARY"] = tc.Rate.ToNullOrDouble();
                    }

                    if (items != null || items.Count != 0)
                    {
                        foreach (TrendClass item in items)
                        {
                            if (row == 0)
                            {
                                dr[item.CreatedTime] = item.PcsQty.ToNullOrDouble();
                            }
                            else if (row == 1)
                            {
                                dr[item.CreatedTime] = item.DefectQty.ToNullOrDouble();
                            }
                            else
                            {
                                dr[item.CreatedTime] = item.Rate.ToNullOrDouble();
                            }
                        }
                    }

                    row++;
                }
            }
        }

        /// <summary>
        /// 품목의 일별 Data 조회
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private List<TrendClass> SetTrandProductItem(string value) => (from main in _trendData
                                                                       where main.ProductDefId == value
                                                                          && main.Month == _month
                                                                       group main by new { main.CreatedTime, main.ProductDefId } into gr
                                                                       orderby gr.Key.ProductDefId
                                                                       select new TrendClass
                                                                       {
                                                                           CreatedTime = gr.Key.CreatedTime,
                                                                           ProductDefId = gr.Key.ProductDefId,
                                                                           PcsQty = gr.Sum(x => x.PcsQty),
                                                                           DefectQty = gr.Sum(x => x.DefectQty),
                                                                           Rate = DefectRate(gr.Sum(x => x.DefectQty), gr.Sum(x => x.PcsQty))
                                                                       }).ToList();

        /// <summary>
        /// 불량률 계산
        /// </summary>
        /// <param name="value"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        private static double DefectRate(object value, object total)
        {
            double rate = Math.Round((value.ToSafeDoubleZero() / total.ToSafeDoubleZero()) * 100, 2);

            return double.IsInfinity(rate) ? -1 : rate;
        }

        /// <summary>
        /// resize
        /// </summary>
        private void FormResize()
        {
            try
            {
                int position = 140;
                int rate = 2;
                this.splMain.SplitterPosition = this.Height - (this.Height / rate);
                this.splMainSub01.SplitterPosition = position;
                //this.splMainSub0101.SplitterPosition = this.splMainSub0101.Height - (this.splMainSub0101.Height / 2);
                //this.splMainSub0101.SplitterPosition = 50;
            }
            catch (Exception)
            {
                //throw;
            }
        }


        #region 5. Over Rules Check 및 Grid 표시.

        /// <summary>
        /// Over Rules Check Data Grid에 표시.
        /// </summary>
        private void GridOverRules()
        {
            //OverRules Grid Data 편집.
            #region OverRules Grid Data 편집

            this.grdOverRules.DataSource = null;
            this.grdOverRules.View.ClearDatas();

            DataTable dtOverRulesTable = null;
            DataTable staChartData = null;
            DataTable staCpkData = null;
            dtOverRulesTable = this.CreateOverRulesTable();

            //통계 Data 전이
            staChartData = this.ucXBar.rtnDirectChartData;

            if (staChartData != null && staChartData.Rows.Count > 0)
            {
                try
                {
                    for (int i = 0; i < staChartData.Rows.Count; i++)
                    {
                        DataRow srcData = staChartData.Rows[i];
                        DataRow dataRowOverRule = dtOverRulesTable.NewRow();
                        SpcFunction.IsDbNckInt64Write(dataRowOverRule, "TEMPID", srcData);
                        SpcFunction.IsDbNckStringWrite(dataRowOverRule, "SUBGROUP", srcData);
                        //SpcFunction.IsDbNckStringWrite(dataRowOverRule, "SUBGROUPNAME", srcData, "", SpcClass.SubGroupSymbolDb, SpcClass.SubGroupSymbolView);
                        SpcFunction.IsDbNckStringWrite(dataRowOverRule, "SAMPLING", srcData);
                        srcData["SAMPLINGNAME"] = srcData["SAMPLING"].ToSafeString();
                        SpcFunction.IsDbNckStringWrite(dataRowOverRule, "SAMPLINGNAME", srcData);

                        SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "BAR", srcData);
                        SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "R", srcData);

                        SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "UCL", srcData);
                        SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "LCL", srcData);
                        SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "CL", srcData);

                        //SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "RUCL", srcData);
                        //SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "RLCL", srcData);
                        //SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "RCL", srcData);

                        string subGroupId = srcData["SUBGROUP"].ToSafeString();
                        //var staCpkRaw = staChartData.AsEnumerable().Where(x => x.Field<string>("SUBGROUP") == subGroupId);

                        //foreach (var item in staCpkRaw)
                        //{
                        //    SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "UCL", item["UCL"].ToSafeDoubleStaMin());
                        //    SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "CL", item["CL"].ToSafeDoubleStaMin());
                        //    SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "LCL", item["LCL"].ToSafeDoubleStaMin());
                        //}

                        var dtGroupData = _AnalysisParameter.dtInputRawData.AsEnumerable()
                                                               .Where(x => x.Field<string>("SUBGROUP") == subGroupId)
                                                               .GroupBy(m => new { SUBGROUP = m.Field<string>("SUBGROUP") })
                                                               .Select(grp => new
                                                               {
                                                                   sSUBGROUP = grp.Key.SUBGROUP.ToSafeString(),
                                                                   sSUBGROUPNAME = grp.Where(x => x.Field<object>("SUBGROUPNAME") != null).Max(x => x.Field<object>("SUBGROUPNAME").ToSafeString()),
                                                                   sINSPECTIONDATETIMEVIEWDATE = grp.Where(x => x.Field<object>("INSPECTIONDATETIMEVIEWDATE") != null).Max(x => x.Field<object>("INSPECTIONDATETIMEVIEWDATE").ToSafeString()),
                                                                   sTXNHISTKEY = grp.Where(x => x.Field<object>("TXNHISTKEY") != null).Max(x => x.Field<object>("TXNHISTKEY").ToSafeString()),
                                                                   sRESOURCETYPE = grp.Where(x => x.Field<object>("RESOURCETYPE") != null).Max(x => x.Field<object>("RESOURCETYPE").ToSafeString()),
                                                                   sINSPECTIONTYPE = grp.Where(x => x.Field<object>("INSPECTIONTYPE") != null).Max(x => x.Field<object>("INSPECTIONTYPE").ToSafeString()),
                                                                   sLOTID = grp.Where(x => x.Field<object>("LOTID") != null).Max(x => x.Field<object>("LOTID").ToSafeString()),
                                                                   sINSPITEMID = grp.Where(x => x.Field<object>("INSPITEMID") != null).Max(x => x.Field<object>("INSPITEMID").ToSafeString()),
                                                                   sINSPITEMVERSION = grp.Where(x => x.Field<object>("INSPITEMVERSION") != null).Max(x => x.Field<object>("INSPITEMVERSION").ToSafeString()),
                                                                   sINSPITEMIDNAME = grp.Where(x => x.Field<object>("INSPITEMIDNAME") != null).Max(x => x.Field<object>("INSPITEMIDNAME").ToSafeString()),
                                                                   sPROCESSRELNO = grp.Where(x => x.Field<object>("PROCESSRELNO") != null).Max(x => x.Field<object>("PROCESSRELNO").ToSafeString()),
                                                                   sAREAID = grp.Where(x => x.Field<object>("AREAID") != null).Max(x => x.Field<object>("AREAID").ToSafeString()),
                                                                   sPRODUCTDEFID = grp.Where(x => x.Field<object>("PRODUCTDEFID") != null).Max(x => x.Field<object>("PRODUCTDEFID").ToSafeString()),
                                                                   sPRODUCTDEFVERSION = grp.Where(x => x.Field<object>("PRODUCTDEFVERSION") != null).Max(x => x.Field<object>("PRODUCTDEFVERSION").ToSafeString()),
                                                                   sPRODUCTDEFIDNAME = grp.Where(x => x.Field<object>("PRODUCTDEFIDNAME") != null).Max(x => x.Field<object>("PRODUCTDEFIDNAME").ToSafeString()),
                                                                   sPROCESSSEGMENTID = grp.Where(x => x.Field<object>("PROCESSSEGMENTID") != null).Max(x => x.Field<object>("PROCESSSEGMENTID").ToSafeString()),
                                                                   sPROCESSSEGMENTVERSION = grp.Where(x => x.Field<object>("PROCESSSEGMENTVERSION") != null).Max(x => x.Field<object>("PROCESSSEGMENTVERSION").ToSafeString()),
                                                                   sPROCESSSEGMENTNAME = grp.Where(x => x.Field<object>("PROCESSSEGMENTNAME") != null).Max(x => x.Field<object>("PROCESSSEGMENTNAME").ToSafeString()),
                                                                   sEQUIPMENTID = grp.Where(x => x.Field<object>("EQUIPMENTID") != null).Max(x => x.Field<object>("EQUIPMENTID").ToSafeString()),
                                                                   sEQUIPMENTNAME = grp.Where(x => x.Field<object>("EQUIPMENTNAME") != null).Max(x => x.Field<object>("EQUIPMENTNAME").ToSafeString()),
                                                                   sINSPECTIONDEFID = grp.Where(x => x.Field<object>("INSPECTIONDEFID") != null).Max(x => x.Field<object>("INSPECTIONDEFID").ToSafeString()),
                                                                   sINSPECTIONDEFVERSION = grp.Where(x => x.Field<object>("INSPECTIONDEFVERSION") != null).Max(x => x.Field<object>("INSPECTIONDEFVERSION").ToSafeString()),
                                                                   sDEFECTCODE = grp.Where(x => x.Field<object>("DEFECTCODE") != null).Max(x => x.Field<object>("DEFECTCODE").ToSafeString()),
                                                                   sDEFECTNAME = grp.Where(x => x.Field<object>("DEFECTNAME") != null).Max(x => x.Field<object>("DEFECTNAME").ToSafeString()),
                                                                   sENTERPRISEID = grp.Where(x => x.Field<object>("ENTERPRISEID") != null).Max(x => x.Field<object>("ENTERPRISEID").ToSafeString()),
                                                                   sPLANTID = grp.Where(x => x.Field<object>("PLANTID") != null).Max(x => x.Field<object>("PLANTID").ToSafeString()),
                                                                   sNVALUE = grp.Where(x => x.Field<object>("NVALUE") != null).Sum(x => x.Field<object>("NVALUE").ToSafeDoubleStaMin()),
                                                                   sNSUBVALUE = grp.Where(x => x.Field<object>("NSUBVALUE") != null).Sum(x => x.Field<object>("NSUBVALUE").ToSafeDoubleStaMin()),
                                                                   sSAMPLEQTY = grp.Where(x => x.Field<object>("SAMPLEQTY") != null).Sum(x => x.Field<object>("SAMPLEQTY").ToSafeDoubleStaMin())


                                                               });
                        foreach (var s in dtGroupData)
                        {
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "SUBGROUP", s.sSUBGROUP.ToSafeString());
                            string tempSUBGROUPNAME = s.sSUBGROUPNAME.ToSafeString().Replace(SpcClass.SubGroupSymbolDb, SpcClass.SubGroupSymbolView);
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "SUBGROUPNAME", tempSUBGROUPNAME);
                            //SpcFunction.IsDbNckStringWrite(dataRowOverRule, "SUBGROUPNAME", srcData, "", SpcClass.SubGroupSymbolDb, SpcClass.SubGroupSymbolView);
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "INSPECTIONDATETIMEVIEWDATE", s.sINSPECTIONDATETIMEVIEWDATE.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "TXNHISTKEY", s.sTXNHISTKEY.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "RESOURCETYPE", s.sRESOURCETYPE.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "INSPECTIONTYPE", s.sINSPECTIONTYPE.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "LOTID", s.sLOTID.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "INSPITEMID", s.sINSPITEMID.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "INSPITEMVERSION", s.sINSPITEMVERSION.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "INSPITEMIDNAME", s.sINSPITEMIDNAME.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "PROCESSRELNO", s.sPROCESSRELNO.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "AREAID", s.sAREAID.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "PRODUCTDEFID", s.sPRODUCTDEFID.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "PRODUCTDEFVERSION", s.sPRODUCTDEFVERSION.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "PRODUCTDEFIDNAME", s.sPRODUCTDEFIDNAME.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "PROCESSSEGMENTID", s.sPROCESSSEGMENTID.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "PROCESSSEGMENTVERSION", s.sPROCESSSEGMENTVERSION.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "PROCESSSEGMENTNAME", s.sPROCESSSEGMENTNAME.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "EQUIPMENTID", s.sEQUIPMENTID.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "EQUIPMENTNAME", s.sEQUIPMENTNAME.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "INSPECTIONDEFID", s.sINSPECTIONDEFID.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "INSPECTIONDEFVERSION", s.sINSPECTIONDEFVERSION.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "DEFECTCODE", s.sDEFECTCODE.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "DEFECTNAME", s.sDEFECTNAME.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "ENTERPRISEID", s.sENTERPRISEID.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "PLANTID", s.sPLANTID.ToSafeString());
                            SpcFunction.IsDbNckDoubleWriteDBNull(dataRowOverRule, "NVALUE", s.sNVALUE.ToSafeDoubleStaMin());
                            SpcFunction.IsDbNckDoubleWriteDBNull(dataRowOverRule, "NSUBVALUE", s.sNSUBVALUE.ToSafeDoubleStaMin());
                            SpcFunction.IsDbNckDoubleWriteDBNull(dataRowOverRule, "SAMPLEQTY", s.sSAMPLEQTY.ToSafeDoubleStaMin());
                        }

                        //OverRule 체크
                        bool overRuleCheck = OverRulesDataCheck(dataRowOverRule);

                        if (overRuleCheck)
                        {
                            dtOverRulesTable.Rows.Add(dataRowOverRule);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                //Console.WriteLine(staChartData.Rows.Count);
                //Subgroup 조회 or Join
                //Spec 조회 or Join
            }

            //Grid에 Raw Data 표시.

            grdOverRules.DataSource = dtOverRulesTable;
            grdOverRules.View.BestFitColumns();
            grdOverRules.ShowStatusBar = true;

            //Console.WriteLine("^^");
            #endregion OverRules Grid Data 편집
        }

        /// <summary>
        /// Spec Check 모듈
        /// </summary>
        private void SpecCheck(DataTable specDt)
        {
            DataRow focusRow = grdRawData.View.GetFocusedDataRow();
            DataRow row = specDt.Rows.Cast<DataRow>()
                                     .Where(r => r["INSPITEMID"].Equals(focusRow["INSPITEMID"].ToString()) && r["CONTROLTYPE"].Equals(r["DEFAULTCHARTTYPE"]))
                                     .First();

            //입력 Parameter 입력
            SpcLibraryHelper spcHelper = new SpcLibraryHelper();
            SpcRules spcRules = new SpcRules();
            spcRules.xbarr.uol = Convert.ToDouble(row["UOL"]);
            spcRules.xbarr.usl = Convert.ToDouble(row["USL"]);
            spcRules.xbarr.ucl = Convert.ToDouble(row["UCL"]);
            spcRules.xbarr.lcl = Convert.ToDouble(row["LCL"]);
            spcRules.xbarr.lsl = Convert.ToDouble(row["LSL"]);
            spcRules.xbarr.lol = Convert.ToDouble(row["LOL"]);
            spcRules.nValue = Convert.ToDouble(grdRawData.View.GetRowCellValue(grdRawData.View.FocusedRowHandle, "TITRATIONQTY"));

            spcRules.defaultChartType = SpcChartType.xbarr;

            //Spec Check
            SpcRulesOver returnRulesOver = new SpcRulesOver();
            returnRulesOver = spcHelper.SpcSpecRuleCheck(spcRules);

            //결과 표시
            if (returnRulesOver.isResult == true)
            {
                grdRawData.View.SetRowCellValue(grdRawData.View.FocusedRowHandle, "ISSPECOUT", "Y");

                // Spec Out입니다. 메일을 보내시겠습니까?
                if (this.ShowMessageBox(returnRulesOver.message.value, "Caption", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    //SendMailPopup popup = new SendMailPopup();
                    //popup.Owner = this;
                    //popup.CurrentDataRow = grdChemicalRegistration.View.GetFocusedDataRow();
                    //popup.ShowDialog();
                }
            }
            else
            {
                grdRawData.View.SetRowCellValue(grdRawData.View.FocusedRowHandle, "ISSPECOUT", "N");

                if (spcRules.nValue.IsDouble())
                {
                    this.ShowMessageBox("정상 - SPEC Check", "OO");
                }
                else
                {
                    this.ShowMessageBox("측정값이 없습니다.", "OO");
                }
            }
        }
        #endregion 5. Over Rules Check 및 Grid 표시.

        #endregion


    }

}
