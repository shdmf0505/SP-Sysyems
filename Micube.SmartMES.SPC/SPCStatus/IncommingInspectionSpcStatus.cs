#region using

using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using Micube.SmartMES.Commons.SPCLibrary;
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
using Micube.Framework.Log;

#endregion

namespace Micube.SmartMES.SPC
{
    /// <summary>
    /// 프 로 그 램 명  : SPC > SPC 현황 > 수입검사 SPC 현황.
    /// 업  무  설  명  : 수입검사 측정값을 분석하여 관리도 및 공정능력분석함.
    /// 생    성    자  : 이승우
    /// 생    성    일  : 2019-07-26
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class IncommingInspectionSpcStatus : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

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
        #endregion

        #endregion

        #region 생성자

        public IncommingInspectionSpcStatus()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            //InitializeRawMaterialGrid();
            //InitializeRawAssyGrid();
            //InitializeDefectHistoryGrid();
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region RowData
            grdRawData.GridButtonItem = GridButtonItem.Export;
            grdRawData.View.AddTextBoxColumn("INSPECTIONDATETIMEVIEWDATE", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("RESOURCETYPE", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("LOTID", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("INSPITEMID", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("INSPITEMVERSION", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("INSPITEMIDNAME", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("PROCESSRELNO", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("AREAID", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("PRODUCTDEFID", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("PRODUCTDEFVERSION", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("PRODUCTDEFNAME", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("PROCESSSEGMENTID", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("EQUIPMENTID", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("EQUIPMENTNAME", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("VENDORNAME", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("INSPECTIONDEFID", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("ENTERPRISEID", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("PLANTID", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("LSL", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("USL", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("SL", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("LCL", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("UCL", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("LOL", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("UOL", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("NVALUE", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("NSUBVALUE", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("SAMPLEQTY", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("SPECOUTQTY", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("INSPECTIONRESULT", 120).SetIsReadOnly();

            grdRawData.View.PopulateColumns();
            
            #endregion

            #region Over Rule
            grdOverRules.GridButtonItem = GridButtonItem.Export;
            grdOverRules.View.SetIsReadOnly();
            //grdOverRules.View.AddTextBoxColumn("TEMPID", 120);
            //grdOverRules.View.AddTextBoxColumn("GROUPID", 120);
            //grdOverRules.View.AddTextBoxColumn("SUBGROUP", 120);
            grdOverRules.View.AddTextBoxColumn("SUBGROUPNAME", 120).SetLabel("SUBGROUPNAMEVIEW");
            grdOverRules.View.AddTextBoxColumn("SAMPLINGNAME", 120).SetLabel("SAMPLINGXAXIS");

            grdOverRules.View.AddTextBoxColumn("RESOURCETYPE", 120).SetIsReadOnly();
            grdOverRules.View.AddTextBoxColumn("INSPITEMID", 120).SetIsReadOnly();
            grdOverRules.View.AddTextBoxColumn("INSPITEMVERSION", 120).SetIsReadOnly();
            grdOverRules.View.AddTextBoxColumn("INSPITEMIDNAME", 120).SetIsReadOnly();
            grdOverRules.View.AddTextBoxColumn("PROCESSRELNO", 120).SetIsReadOnly();
            grdOverRules.View.AddTextBoxColumn("AREAID", 120).SetIsReadOnly();
            grdOverRules.View.AddTextBoxColumn("PRODUCTDEFID", 120).SetIsReadOnly();
            grdOverRules.View.AddTextBoxColumn("PRODUCTDEFVERSION", 120).SetIsReadOnly();
            grdOverRules.View.AddTextBoxColumn("PRODUCTDEFNAME", 120).SetIsReadOnly();
            grdOverRules.View.AddTextBoxColumn("PROCESSSEGMENTID", 120).SetIsReadOnly();
            grdOverRules.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 120).SetIsReadOnly();
            grdOverRules.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120).SetIsReadOnly();
            grdOverRules.View.AddTextBoxColumn("EQUIPMENTID", 120).SetIsReadOnly();
            grdOverRules.View.AddTextBoxColumn("EQUIPMENTNAME", 120).SetIsReadOnly();
            //grdOverRules.View.AddTextBoxColumn("VENDORNAME", 120).SetIsReadOnly();
            grdOverRules.View.AddTextBoxColumn("INSPECTIONDEFID", 120).SetIsReadOnly();
            grdOverRules.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 120).SetIsReadOnly();
            grdOverRules.View.AddTextBoxColumn("ENTERPRISEID", 120).SetIsReadOnly();
            grdOverRules.View.AddTextBoxColumn("PLANTID", 120).SetIsReadOnly();

            //grdOverRules.View.AddTextBoxColumn("MAX", 120);
            //grdOverRules.View.AddTextBoxColumn("MIN", 120);
            grdOverRules.View.AddTextBoxColumn("R", 120);
            grdOverRules.View.AddTextBoxColumn("RUCL", 120);
            grdOverRules.View.AddTextBoxColumn("RLCL", 120);
            grdOverRules.View.AddTextBoxColumn("RCL", 120);
            grdOverRules.View.AddTextBoxColumn("BAR", 120);

            grdOverRules.View.AddTextBoxColumn("USL", 120);
            grdOverRules.View.AddTextBoxColumn("LSL", 120);
            grdOverRules.View.AddTextBoxColumn("CSL", 120);

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

        #region 원자재 수입검사 결과 탭 초기화
        private void InitializeRawMaterialGrid()
        {
            #region 상단 그리드
            // TODO : 그리드 초기화 로직 추가
            grdRawData.View.SetIsReadOnly();
            grdRawData.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdRawData.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdRawData.View.SetSortOrder("ORDERNUMBER");
            grdRawData.View.SetSortOrder("DEGREE");

            grdRawData.View.AddTextBoxColumn("ORDERNUMBER", 150)
                .SetTextAlignment(TextAlignment.Center);

            grdRawData.View.AddTextBoxColumn("STORENO", 150);

            grdRawData.View.AddTextBoxColumn("ENTRYEXITDATE", 200)
                .SetDisplayFormat("yyyy-MM-dd")
                .SetTextAlignment(TextAlignment.Center);

            grdRawData.View.AddTextBoxColumn("ACCEPTDATE", 200)
                .SetDisplayFormat("yyyy-MM-dd")
                .SetLabel("RECEPTIONDATE")
                .SetTextAlignment(TextAlignment.Center);

            grdRawData.View.AddTextBoxColumn("INSPECTIONDATE", 200)
                .SetDisplayFormat("yyyy-MM-dd")
                .SetTextAlignment(TextAlignment.Center);

            grdRawData.View.AddTextBoxColumn("DEGREE", 150)
                .SetLabel("SEQ")
                .SetTextAlignment(TextAlignment.Center)
                .SetIsHidden();

            grdRawData.View.AddTextBoxColumn("VENDORNAME", 150)
                .SetTextAlignment(TextAlignment.Center);

            grdRawData.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 150)
                .SetTextAlignment(TextAlignment.Center);

            grdRawData.View.AddTextBoxColumn("CONSUMABLEDEFID", 150)
                .SetIsHidden();

            grdRawData.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 150)
                .SetIsHidden();

            grdRawData.View.AddTextBoxColumn("MATERIALLOTID", 150)
                .SetIsHidden();

            grdRawData.View.AddTextBoxColumn("QTY", 150)
                .SetTextAlignment(TextAlignment.Center);

            grdRawData.View.AddTextBoxColumn("UNIT", 150)
                .SetTextAlignment(TextAlignment.Center);

            grdRawData.View.AddTextBoxColumn("ISINSPECTION", 150)
                .SetTextAlignment(TextAlignment.Center);

            grdRawData.View.AddComboBoxColumn("INSPECTIONRESULT", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OKNG", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center);

            grdRawData.View.AddTextBoxColumn("HASREPORTFILE", 150)
                .SetTextAlignment(TextAlignment.Center);

            grdRawData.View.AddTextBoxColumn("AREAID", 150)
                .SetIsHidden();

            grdRawData.View.AddTextBoxColumn("DESCRIPTION", 150)
                .SetTextAlignment(TextAlignment.Center);

            grdRawData.View.AddTextBoxColumn("TXNHISTKEY", 150)
                .SetIsHidden();

            grdRawData.View.PopulateColumns();
            #endregion

        }
        #endregion



        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.tabMain.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tabMain_SelectedPageChanged);
        }

        #region 2. 폼 이벤트
        /// <summary>
        /// 폼 Load 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IncommingInspectionSpcStatus_Load(object sender, EventArgs e)
        {
            this.tabAnalysis.PageVisible = false;
            SpcClass.SpcDictionaryDataSetting();
            this.ucXBarFrame1.SpcCpkChartEnterEventHandler += UcXBarFrame1_SpcCpkChartEnterEventHandler;
            this.ucXBarFrame1.ucXBarGrid01.ucXBar1.SpcChartDirectMessageUserEventHandler += UcXBar1_SpcChartDirectMessageUserEventHandler;
            this.ucXBarFrame1.ucXBarGrid01.ucXBar2.SpcChartDirectMessageUserEventHandler += UcXBar2_SpcChartDirectMessageUserEventHandler;
            this.ucXBarFrame1.ucXBarGrid01.ucXBar3.SpcChartDirectMessageUserEventHandler += UcXBar3_SpcChartDirectMessageUserEventHandler;
            this.ucXBarFrame1.ucXBarGrid01.ucXBar4.SpcChartDirectMessageUserEventHandler += UcXBar4_SpcChartDirectMessageUserEventHandler;
            this.ucXBarFrame1.SpcChartXBarFrameDirectMessageUserEventHandler += UcXBarFrame1_SpcChartXBarFrameDirectMessageUserEventHandler;

            this.ucXBarFrame1.SpcVtChartShowWaitAreaEventHandler += UcXBarFrame1_SpcVtChartShowWaitAreaEventHandler;
            this.ucXBarFrame1.ucXBarGrid01.SpcVtChartShowWaitAreaEventHandler += UcXBarGrid01_SpcVtChartShowWaitAreaEventHandler;
            this.ucCpkFrame1.ucCpkGrid01.SpcVtChartShowWaitAreaEventHandler += UcCpkGrid01_SpcVtChartShowWaitAreaEventHandler;

            this.ucXBarFrame1.cboChartTypeSetting(1);
            InitializeGrid();
            this.FormResize();
        }

        #region Chart Wait 이벤트
        /// <summary>
        /// XBarFrame Chart Change Wait
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="se"></param>
        private void UcXBarFrame1_SpcVtChartShowWaitAreaEventHandler(object sender, EventArgs e, SpcShowWaitAreaOption se)
        {
            se.CheckValue = se.ChartTypeChange;
            UcChartGridShowWaitArea(sender, e, se);
        }
        /// <summary>
        /// XBar Grid Wait 이벤트 실행.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="se"></param>
        private void UcXBarGrid01_SpcVtChartShowWaitAreaEventHandler(object sender, EventArgs e, SpcShowWaitAreaOption se)
        {
            if (_isAgainAnalysisButtonXBar != true)
            {
                UcChartGridShowWaitArea(sender, e, se);
            }
            _isAgainAnalysisButtonXBar = false;
        }
        /// <summary>
        /// Cpk Grid Wait 이벤트 실행.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="se"></param>
        private void UcCpkGrid01_SpcVtChartShowWaitAreaEventHandler(object sender, EventArgs e, SpcShowWaitAreaOption se)
        {
            if (_isAgainAnalysisButtonCpk != true)
            {
                UcChartGridShowWaitArea(sender, e, se);
            }
            _isAgainAnalysisButtonCpk = false;
        }
        /// <summary>
        /// 분석 Chart Waite 이벤트 실행.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="se"></param>
        private void UcAnalysisPlot1_SpcVtChartShowWaitAreaEventHandler(object sender, EventArgs e, SpcShowWaitAreaOption se)
        {
            UcChartGridShowWaitArea(sender, e, se);
        }
        /// <summary>
        /// Chart Grid Waite 실행.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="se"></param>
        private void UcChartGridShowWaitArea(object sender, EventArgs e, SpcShowWaitAreaOption se)
        {
            try
            {
                if (se.CheckValue)
                {
                    _isCancelOnSearchAsync = true;
                    DialogManager.ShowWaitArea(this.pnlContent);
                }
                else
                {
                    DialogManager.CloseWaitArea(this.pnlContent);
                    _isCancelOnSearchAsync = false;
                }
            }
            catch (Exception ex)
            {
                DialogManager.CloseWaitArea(this.pnlContent);
                _isCancelOnSearchAsync = false;
                Logger.Error(ex.Message);
            }
        }
        #endregion Chart Wait 이벤트

        /// <summary>
        /// XBarFrame Message 이벤트
        /// </summary>
        /// <param name="msg"></param>
        private void UcXBarFrame1_SpcChartXBarFrameDirectMessageUserEventHandler(SpcEventsChartMessage msg)
        {
            ShowMessage(msg.mainMessage);
        }

        private void IncommingInspectionSpcStatus_Resize(object sender, EventArgs e)
        {
            this.FormResize();
        }
        /// <summary>
        /// Chart Option 변경.
        /// </summary>
        /// <param name="f"></param>
        private void UcXBarFrame1_SpcCpkChartEnterEventHandler(SpcFrameChangeData f)
        {
            _isAgainAnalysisXBar = f.isAgainAnalysisXBar;
            _isAgainAnalysisCpk = f.isAgainAnalysisCpk;
            _isAgainAnalysisPlot = f.isAgainAnalysis;
            _isAgainAnalysisButtonXBar = f.isAgainAnalysisButtonXBar;
            _isAgainAnalysisButtonCpk = f.isAgainAnalysisButtonCpk;
            _isAgainAnalysisOverRules = f.isAgainAnalysisOverRules;
            _AnalysisParameter.spcOption = f.SPCOption;
        }

        #endregion

        #region Chart Message 이벤트
        /// <summary>
        /// Chart1 Message 이벤트
        /// </summary>
        /// <param name="msg"></param>
        private void UcXBar1_SpcChartDirectMessageUserEventHandler(SpcEventsChartMessage msg)
        {
            ShowMessage(msg.mainMessage);
        }
        /// <summary>
        /// Chart2 Message 이벤트
        /// </summary>
        /// <param name="msg"></param>
        private void UcXBar2_SpcChartDirectMessageUserEventHandler(SpcEventsChartMessage msg)
        {
            ShowMessage(msg.mainMessage);
        }
        /// <summary>
        /// Chart3 Message 이벤트
        /// </summary>
        /// <param name="msg"></param>
        private void UcXBar3_SpcChartDirectMessageUserEventHandler(SpcEventsChartMessage msg)
        {
            ShowMessage(msg.mainMessage);
        }
        /// <summary>
        /// Chart4 Message 이벤트
        /// </summary>
        /// <param name="msg"></param>
        private void UcXBar4_SpcChartDirectMessageUserEventHandler(SpcEventsChartMessage msg)
        {
            ShowMessage(msg.mainMessage);
        }
        #endregion Chart Message 이벤트

        #region 3. 통계 Chart 관련 이벤트
        /// <summary>
        /// Tab Page 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            this.ChartAnalysisExecution();
        }
        #endregion

        /// <summary>
        /// grdRawAssyInspectionResult 포커스 row가 변경될 때 그에 해당하는 불량을 조회하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdRawAssy_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SearchDefectList();
        }

        /// <summary>
        /// grdMaterialInspectionResult그리드 ROW를 더블 클릭 했을 때 검사결과를 입력하는 팝업이 뜨는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdRawAssy_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            SmartBandedGridView view = sender as SmartBandedGridView;
            GridHitInfo info = view.CalcHitInfo(ea.Location);

            DataRow row = null; // grdRawAssyInspectionResult.View.GetDataRow(info.RowHandle);

            if (row == null) return;

            DialogManager.ShowWaitArea(pnlContent);

            //RawAssyImportInspectionResultPopup rawAssypopup = new RawAssyImportInspectionResultPopup();
            //rawAssypopup.StartPosition = FormStartPosition.CenterParent;
            //rawAssypopup.Owner = this;
            //rawAssypopup.CurrentDataRow = grdRawAssyInspectionResult.View.GetFocusedDataRow();
            //rawAssypopup.UserPopup();
            //rawAssypopup.OnSearch();
            //DialogManager.CloseWaitArea(pnlContent);

            //rawAssypopup.ShowDialog();
            //if (rawAssypopup.DialogResult == DialogResult.OK)
            //{
            //    Popup_FormClosed();
            //}
        }


        /// <summary>
        /// grdMaterialInspectionResult그리드 ROW를 더블 클릭 했을 때 검사결과를 입력하는 팝업이 뜨는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdRawMaterial_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            SmartBandedGridView view = sender as SmartBandedGridView;
            GridHitInfo info = view.CalcHitInfo(ea.Location);

            DataRow row = grdRawData.View.GetDataRow(info.RowHandle);

            if (row == null) return;

            DialogManager.ShowWaitArea(pnlContent);

            //RawMaterialImportInspectionResultPopup rawMaterialpopup = new RawMaterialImportInspectionResultPopup();
            //rawMaterialpopup.StartPosition = FormStartPosition.CenterParent;
            //rawMaterialpopup.Owner = this;
            //rawMaterialpopup.CurrentDataRow = grdMaterialInspectionResult.View.GetFocusedDataRow();
            //rawMaterialpopup.UserPopup();
            //rawMaterialpopup.OnSearch();
            //DialogManager.CloseWaitArea(pnlContent);

            //rawMaterialpopup.ShowDialog();
            //if (rawMaterialpopup.DialogResult == DialogResult.OK)
            //{
            //    Popup_FormClosed();
            //}
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            //DataTable changed = grdCodeClass.GetChangedRows();

            //ExecuteRule("SaveCodeClass", changed);
        }

        #endregion

        #region 검색

        /// <summary>
        /// Main자료조회 - 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            string subgroupType = "";

            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);
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

                subgroupType = param["P_SPCAXISTYPE"].ToSafeString();

                Console.WriteLine(values.Count());
                //string productDefind = "";
                //string equipmentId = "";
                string processsegmentId = "";
                string sourceType = "";

                //productDefind = param["P_PRODUCTDEFID"].ToSafeString();
                // equipmentId = param["P_EQUIPMENTID"].ToSafeString();
                sourceType = param["P_INSPECTIONRESULTRESOUCETYPE"].ToSafeString();
                processsegmentId = param["P_PROCESSSEGMENTID"].ToSafeString();

                //Dictionary<string, object> dicParam = new Dictionary<string, object>();

                #region 5.통계용 입력자료 DB 조회
                //초기화
                DataTable InputRawMaxCount;
                DataTable tempInputSpec;
                tempInputSpec = SpcClass.SpcCreateMainRawSpecTable();
                this.grdRawData.DataSource = null;
                this.grdRawData.View.ClearDatas();
                this.grdOverRules.DataSource = null;
                this.grdOverRules.View.ClearDatas();

                //전체 조회 Count 조회.
                InputRawMaxCount = await SqlExecuter.QueryAsync("GetSpcInspectionResultCommonRawMaxCount", "10001", values);
                if (InputRawMaxCount != null && InputRawMaxCount.Rows.Count < 1)
                {
                    this.ucXBarFrame1.ClearChartControlsXBAR();
                    this.ucCpkFrame1.ClearChartControlsCpk();
                    // 조회할 데이터가 없습니다.
                    ShowMessage("NoSelectData");
                    return;
                }

                //조회 자료 전체 건수 확인.
                long? nQueryMaxCount = InputRawMaxCount.Rows[0]["RECORDMAXCOUNT"].ToNullOrInt64();
                Console.WriteLine(nQueryMaxCount);

                //DB Data 부분 수집 여부 Check
                _AnalysisParameter.dtInputRawData = null;
                _AnalysisParameter.dtInputSpecData = null;
                 if (nQueryMaxCount < SpcFlag.nSpcQueryMaxCount)
                {
                    _AnalysisParameter.dtInputRawData = await SqlExecuter.QueryAsync("GetSpcInspectionResultCommonRaw", "10001", values);
                }
                else
                {
                    await this.InputDataMerge(dateStart, dateEnd, values);
                }

                _AnalysisParameter.dtInputSpecData = await SqlExecuter.QueryAsync("GetSpcInspectionResultCommonSpec", "10001", values);
                if (_AnalysisParameter.dtInputSpecData != null && _AnalysisParameter.dtInputSpecData.Rows.Count < 1)
                {
                    // 조회할 데이터가 없습니다.
                    ShowMessage("NoSelectData - Spec Data");
                }

                //Chart 구분
                string chartType = ucXBarFrame1.cboLeftChartType.GetDataValue().ToSafeString().ToUpper().Replace("_", "");
                _AnalysisParameter.spcOption.specDefaultChartType = "XBARR";
                _AnalysisParameter.spcOption.chartName.xBarChartType = chartType;
                _AnalysisParameter.spcOption.chartName.xCpkChartType = chartType;
                _AnalysisParameter.spcOption.ChartTypeSetting(chartType, ref _AnalysisParameter.spcOption);
                _AnalysisParameter.spcOption.sigmaType = ucXBarFrame1.chkLeftEstimate.Checked ? SigmaType.Yes : SigmaType.No;//추정치 사용 유무

                //*통계 분석 실행
                _isAgainAnalysisExe = true;
                _isAgainAnalysisXBar = true;
                _isAgainAnalysisCpk = true;
                _isAgainAnalysisPlot = true;
                _isAgainAnalysisButtonXBar = true;
                _isAgainAnalysisButtonCpk = true;
                _isAgainAnalysisRawData = true;
                _isAgainAnalysisOverRules = true;

                switch (sourceType)
                {
                    case "MaterialInspection":
                    case "RawMaterialInspection":
                        _AnalysisParameter.dtInputRawMainFieldName.subgroupNameFiledName = "INCOMSUBGROUPNAME01";
                        break;
                    case "ProcessInspection":
                    default:
                        _AnalysisParameter.dtInputRawMainFieldName.subgroupNameFiledName = "INCOMSUBGROUPNAME02";
                        break;
                }
                
                switch (chartType.ToUpper())
                {
                    case "I":
                    case "MR":
                    case "IMR":
                        //_AnalysisParameter.dtInputRawMainFieldName.subgroupNameFiled = "SUBGROUPIMR";
                        //_AnalysisParameter.dtInputRawMainFieldName.subgroupNameFiled = "SUBGROUPIMRNAME";
                        switch (subgroupType)
                        {
                            case "LOTID":
                                _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiled = "SAMPLINGIMRLOTID";
                                _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiledName = "SAMPLINGIMRNAMELOTID";
                                _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiled01 = "SAMPLING";
                                _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiledName01 = "SAMPLINGNAME";
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
                                switch (sourceType)
                                {
                                    case "MaterialInspection":
                                    case "RawMaterialInspection":
                                        _AnalysisParameter.dtInputRawMainFieldName.subgroupNameFiledName = "INCOMSUBGROUPNAME01";
                                        break;
                                    case "ProcessInspection":
                                    default:
                                        _AnalysisParameter.dtInputRawMainFieldName.subgroupNameFiledName = "INCOMSUBGROUPNAME02";
                                        break;
                                }
                                break;
                        }

                        break;
                }

                this.ChartAnalysisExecution();

                //this.ucXBarFrame1.AnalysisExecution(_AnalysisParameter);
                //this.ucXBarFrame1.AnalysisExecutionTest(ref _AnalysisParameter);
                #endregion

                grdRawData.DataSource = _AnalysisParameter.dtInputRawData;
                grdRawData.ShowStatusBar = true;

            }
            catch (Exception ex)
            {
                throw Framework.MessageException.Create(ex.ToString());
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);
                _isAgainAnalysisExe = false;
            }
        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            //Chart Subgroup 구분
            this.Conditions.AddComboBox("P_INSPECTIONRESULTRESOUCETYPE", new SqlQuery("GetSpcSearchComboCom", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"P_CODECLASSID={"SpcInspectionResultResouceType"}"), "CODENAME", "CODEID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetDefault("RawInspection")
                .SetValidationIsRequired()
                .SetLabel("검사구분")
                .SetPosition(2.1); // Subgroup 조회조건 

            this.Conditions.AddComboBox("p_SpcAxisType", new SqlQuery("GetSpcSearchComboCom", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"P_CODECLASSID={"SpcAxisType"}"), "CODENAME", "CODEID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetDefault("DATE")
                .SetValidationIsRequired()
                .SetLabel("조회조건")
                .SetPosition(2.2); // Subgroup 조회조건 


            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            //공급사
            InitializeConditionPopup_Vendor();
            //표준(원인) 공정
            InitializeConditionProcessSegmentId_Popup();
            //
            InitializeConditionPopup_Consumable();
            //작업장
            InitializeConditionPopup_Area();//7
            //검사항목
            InitializeConditionPopup_InspItem();//8
        }

        #region 조회조건 팝업
        private void InitializeConditionPopup_Vendor()
        {
            // 팝업 컬럼 설정
            var vendorPopup = Conditions.AddSelectPopup("P_VENDORID", new SqlQuery("GetVendorList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", "VENDORTYPE=Supplier"), "VENDORNAME", "VENDORID")
                                        .SetPopupLayout("VENDOR", PopupButtonStyles.Ok_Cancel, true, false)
                                        .SetPopupLayoutForm(400, 600)
                                        .SetPopupResultCount(1)
                                        .SetPosition(3.1)
                                        .SetLabel("VENDOR")
                                        .SetPopupAutoFillColumns("VENDORNAME");

            // 팝업 조회조건
            vendorPopup.Conditions.AddTextBox("VENDORID")
                       .SetLabel("VENDOR");

            // 팝업 그리드
            vendorPopup.GridColumns.AddTextBoxColumn("VENDORID", 150);

            vendorPopup.GridColumns.AddTextBoxColumn("VENDORNAME", 200);
        }
        /// <summary>
        /// 공정
        /// </summary>
        private void InitializeConditionProcessSegmentId_Popup()
        {
            var processSegmentIdPopup = this.Conditions.AddSelectPopup("P_PROCESSSEGMENTID", new SqlQuery("GetProcessSegmentList", "10002", $"PLANTID={UserInfo.Current.Plant}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                .SetPopupLayout(Language.Get("SELECTPROCESSSEGMENTID"), PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(950, 800, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("PROCESSSEGMENTNAME")
                .SetLabel("PROCESSSEGMENT")
                .SetPosition(3.2);

            processSegmentIdPopup.Conditions.AddComboBox("PROCESSSEGMENTCLASSID_MIDDLE", new SqlQuery("GetProcessSegmentClassByType", "10001", $"PLANTID={UserInfo.Current.Plant}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                .SetLabel("MIDDLEPROCESSSEGMENT");
            processSegmentIdPopup.Conditions.AddTextBox("PROCESSSEGMENT");


            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_TOP", 150)
                .SetLabel("LARGEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_MIDDLE", 150)
                .SetLabel("MIDDLEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
        }
        private void InitializeConditionPopup_Consumable()
        {//CONSUMABLECLASSID  OR CONSUMABLETYPE = SubAssembly 쿼리 조회조건으로 추가?
            var productPopup = Conditions.AddSelectPopup("P_CONSUMABLEDEFID", new SqlQuery("GetConsumableDefList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CONSUMABLEDEFNAME", "CONSUMABLEDEFID")
                                         .SetPopupLayout("CONSUMABLEDEF", PopupButtonStyles.Ok_Cancel, true, false)
                                         .SetPopupLayoutForm(600, 600)
                                         .SetPopupResultCount(1)
                                         .SetPosition(3.3)
                                         .SetLabel("CONSUMABLEDEF");

            productPopup.Conditions.AddTextBox("CONSUMABLEDEF");

            productPopup.GridColumns.AddTextBoxColumn("CONSUMABLEDEFID", 150);
            productPopup.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
            productPopup.GridColumns.AddTextBoxColumn("CONSUMABLEDEFVERSION", 200);
        }

        /// <summary>
        /// 작업장 조회조건
        /// </summary>
        private void InitializeConditionPopup_Area()
        {
            // 팝업 컬럼설정
            var areaPopup = Conditions.AddSelectPopup("p_areaId", new SqlQuery("GetAreaListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID")
               .SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(600, 600)
               .SetLabel("AREA")
               .SetPopupResultCount(1)
               .SetPosition(3.4)
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
        /// 검사항목 조회조건
        /// </summary>
        private void InitializeConditionPopup_InspItem()
        {
            // 팝업 컬럼설정
            var inspItemPopupColumn = Conditions.AddSelectPopup("P_INSPITEMID_IN", new SqlQuery("GetSpcSearchInspitem", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPITEMNAME", "INSPITEMID")
               //.SetPopupLayout("CHEMICAL", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(600, 600)
               .SetLabel("INSPITEMID")
               .SetPopupResultCount(2)
               .SetPosition(3.5);
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
        #endregion

        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
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
                    dtInputPartData = await SqlExecuter.QueryAsync("GetSpcInspectionResultCommonRaw", "10001", param);
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

                    dtInputFirstData = await SqlExecuter.QueryAsync("GetSpcInspectionResultCommonRaw", "10001", param);
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
            dt.Columns.Add("RESOURCETYPE", typeof(string));
            dt.Columns.Add("INSPITEMID", typeof(string));
            dt.Columns.Add("INSPITEMVERSION", typeof(string));
            dt.Columns.Add("INSPITEMIDNAME", typeof(string));
            dt.Columns.Add("PROCESSRELNO", typeof(string));
            dt.Columns.Add("AREAID", typeof(string));
            dt.Columns.Add("PRODUCTDEFID", typeof(string));
            dt.Columns.Add("PRODUCTDEFVERSION", typeof(string));
            dt.Columns.Add("PRODUCTDEFNAME", typeof(string));
            dt.Columns.Add("PROCESSSEGMENTID", typeof(string));
            dt.Columns.Add("PROCESSSEGMENTVERSION", typeof(string));
            dt.Columns.Add("PROCESSSEGMENTNAME", typeof(string));
            dt.Columns.Add("EQUIPMENTID", typeof(string));
            dt.Columns.Add("EQUIPMENTNAME", typeof(string));
            dt.Columns.Add("VENDORNAME", typeof(string));
            dt.Columns.Add("INSPECTIONDEFID", typeof(string));
            dt.Columns.Add("INSPECTIONDEFVERSION", typeof(string));
            dt.Columns.Add("ENTERPRISEID", typeof(string));
            dt.Columns.Add("PLANTID", typeof(string));

            //업무별 수정 End ----------------------------------

            dt.Columns.Add("MAX", typeof(double));
            dt.Columns.Add("MIN", typeof(double));
            dt.Columns.Add("R", typeof(double));
            dt.Columns.Add("RUCL", typeof(double));
            dt.Columns.Add("RLCL", typeof(double));
            dt.Columns.Add("RCL", typeof(double));
            dt.Columns.Add("BAR", typeof(double));

            dt.Columns.Add("USL", typeof(double));
            dt.Columns.Add("LSL", typeof(double));
            dt.Columns.Add("CSL", typeof(double));

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
        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            //grdCodeClass.View.CheckValidation();

            //DataTable changed = grdCodeClass.GetChangedRows();

            //if (changed.Rows.Count == 0)
            //{
            //    // 저장할 데이터가 존재하지 않습니다.
            //    throw MessageException.Create("NoSaveData");
            //}
        }

        #endregion

        #region Private Function
        // TODO : 화면에서 사용할 내부 함수 추가
        #region 4. 통계용 함수
        /// <summary>
        /// 통계 분석 실행 - 공정능력 실행, 관리도 (Tab 이벤트에서 실행함)
        /// </summary>
        private void ChartAnalysisExecution()
        {
            string subGroupName = "";
            //if (!_isAgainAnalysisCpk)
            //{
            //    _isAgainAnalysisCpk = _AnalysisParameter.isAgainAnalysisCpk;
            //}

            try
            {
                switch (this.tabMain.SelectedTabPageIndex)
                {
                    case 0:
                    case 2:
                    case 3:
                    case 4:
                        if (_isAgainAnalysisExe != true && _isAgainAnalysisXBar == true)
                        {
                            DialogManager.ShowWaitArea(this.pnlContent);
                        }
                        this.FormResize();
                        subGroupName = this.ucXBarFrame1.ucXBarGrid01.grp01.Text.ToSafeString();
                        if (_isAgainAnalysisXBar)
                        {
                            this.ucXBarFrame1.AnalysisExecution(_AnalysisParameter);
                            _isAgainAnalysisXBar = false;
                        }
                        break;
                    default:
                        break;
                }

                if (this.tabMain.SelectedTabPageIndex == 1)
                {
                    if (_isAgainAnalysisExe != true && _isAgainAnalysisCpk == true)
                    {
                        DialogManager.ShowWaitArea(this.pnlContent);
                    }
                    this.FormResize();
                    subGroupName = this.ucCpkFrame1.ucCpkGrid01.grp01.Text.ToSafeString();
                    if (_isAgainAnalysisCpk)
                    {
                        this.ucCpkFrame1.AnalysisExecution(_AnalysisParameter);
                        _isAgainAnalysisCpk = false;
                    }

                }
                else if (this.tabMain.SelectedTabPageIndex == 2)
                {
                    //Plot Check
                    if (_isAgainAnalysisPlot)
                    {
                        this.ucAnalysisPlot1.SubGroupIdSetting(this.ucXBarFrame1._spcPara, ref _isAgainAnalysisPlot);
                        _isAgainAnalysisPlot = false;
                    }
                }
                else if (this.tabMain.SelectedTabPageIndex == 3)
                {
                    //Raw Data
                    if (_isAgainAnalysisRawData)
                    {
                        DialogManager.ShowWaitArea(this.pnlContent);
                        grdRawData.View.BestFitColumns();
                        _isAgainAnalysisRawData = false;
                    }
                }
                else if (this.tabMain.SelectedTabPageIndex == 4)
                {
                    //Over Rules
                    if (_isAgainAnalysisOverRules)
                    {
                        this.GridOverRules();
                        _isAgainAnalysisOverRules = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (_isAgainAnalysisExe != true)
                {
                    DialogManager.CloseWaitArea(this.pnlContent);
                }
            }

        }
        #endregion

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
            staChartData = this.ucXBarFrame1.ucXBarGrid01.rtnChartData;
            staCpkData = this.ucXBarFrame1.ucXBarGrid01.rtnCpkData;
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
                        SpcFunction.IsDbNckStringWrite(dataRowOverRule, "SUBGROUPNAME", srcData, "", SpcClass.SubGroupSymbolDb, SpcClass.SubGroupSymbolView);
                        SpcFunction.IsDbNckStringWrite(dataRowOverRule, "SAMPLING", srcData);
                        SpcFunction.IsDbNckStringWrite(dataRowOverRule, "SAMPLINGNAME", srcData);

                        SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "BAR", srcData);
                        SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "R", srcData);

                        SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "UCL", srcData);
                        SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "LCL", srcData);
                        SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "CL", srcData);

                        SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "RUCL", srcData);
                        SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "RLCL", srcData);
                        SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "RCL", srcData);

                        string subGroupId = srcData["SUBGROUP"].ToSafeString();
                        var staCpkRaw = staCpkData.AsEnumerable().Where(x => x.Field<string>("SUBGROUP") == subGroupId);

                        foreach (var item in staCpkRaw)
                        {
                            SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "USL", item["USL"].ToSafeDoubleStaMin(), true);
                            SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "CSL", item["CSL"].ToSafeDoubleStaMin(), true);
                            SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "LSL", item["LSL"].ToSafeDoubleStaMin(), true);
                        }

                        var dtGroupData = _AnalysisParameter.dtInputRawData.AsEnumerable()
                                                               .Where(x => x.Field<string>("SUBGROUP") == subGroupId)
                                                               .GroupBy(m => new { SUBGROUP = m.Field<string>("SUBGROUP") })
                                                               .Select(grp => new
                                                               {
                                                                   sSUBGROUP = grp.Key.SUBGROUP.ToSafeString(),
                                                                   //nUSL = grp.Where(x => x.Field<object>("USL") != null).Max(x => x.Field<object>("USL").ToSafeDoubleZero()),
                                                                   //nTSL = grp.Where(x => x.Field<object>("TARGET") != null).Max(x => x.Field<object>("TARGET").ToSafeDoubleZero()),
                                                                   //nLSL = grp.Where(x => x.Field<object>("LSL") != null).Min(x => x.Field<object>("LSL").ToSafeDoubleZero()),
                                                                   sRESOURCETYPE = grp.Where(x => x.Field<object>("RESOURCETYPE") != null).Max(x => x.Field<object>("RESOURCETYPE").ToSafeString()),
                                                                   sINSPITEMID = grp.Where(x => x.Field<object>("INSPITEMID") != null).Max(x => x.Field<object>("INSPITEMID").ToSafeString()),
                                                                   sINSPITEMVERSION = grp.Where(x => x.Field<object>("INSPITEMVERSION") != null).Max(x => x.Field<object>("INSPITEMVERSION").ToSafeString()),
                                                                   sINSPITEMIDNAME = grp.Where(x => x.Field<object>("INSPITEMIDNAME") != null).Max(x => x.Field<object>("INSPITEMIDNAME").ToSafeString()),
                                                                   sPROCESSRELNO = grp.Where(x => x.Field<object>("PROCESSRELNO") != null).Max(x => x.Field<object>("PROCESSRELNO").ToSafeString()),
                                                                   sAREAID = grp.Where(x => x.Field<object>("AREAID") != null).Max(x => x.Field<object>("AREAID").ToSafeString()),
                                                                   sPRODUCTDEFID = grp.Where(x => x.Field<object>("PRODUCTDEFID") != null).Max(x => x.Field<object>("PRODUCTDEFID").ToSafeString()),
                                                                   sPRODUCTDEFVERSION = grp.Where(x => x.Field<object>("PRODUCTDEFVERSION") != null).Max(x => x.Field<object>("PRODUCTDEFVERSION").ToSafeString()),
                                                                   //sPRODUCTDEFNAME = grp.Where(x => x.Field<object>("PRODUCTDEFNAME") != null).Max(x => x.Field<object>("PRODUCTDEFNAME").ToSafeString()),
                                                                   sPROCESSSEGMENTID = grp.Where(x => x.Field<object>("PROCESSSEGMENTID") != null).Max(x => x.Field<object>("PROCESSSEGMENTID").ToSafeString()),
                                                                   sPROCESSSEGMENTVERSION = grp.Where(x => x.Field<object>("PROCESSSEGMENTVERSION") != null).Max(x => x.Field<object>("PROCESSSEGMENTVERSION").ToSafeString()),
                                                                   sPROCESSSEGMENTNAME = grp.Where(x => x.Field<object>("PROCESSSEGMENTNAME") != null).Max(x => x.Field<object>("PROCESSSEGMENTNAME").ToSafeString()),
                                                                   sEQUIPMENTID = grp.Where(x => x.Field<object>("EQUIPMENTID") != null).Max(x => x.Field<object>("EQUIPMENTID").ToSafeString()),
                                                                   sEQUIPMENTNAME = grp.Where(x => x.Field<object>("EQUIPMENTNAME") != null).Max(x => x.Field<object>("EQUIPMENTNAME").ToSafeString()),
                                                                   //sVENDORNAME = grp.Where(x => x.Field<object>("VENDORNAME") != null).Max(x => x.Field<object>("VENDORNAME").ToSafeString()),
                                                                   sINSPECTIONDEFID = grp.Where(x => x.Field<object>("INSPECTIONDEFID") != null).Max(x => x.Field<object>("INSPECTIONDEFID").ToSafeString()),
                                                                   sINSPECTIONDEFVERSION = grp.Where(x => x.Field<object>("INSPECTIONDEFVERSION") != null).Max(x => x.Field<object>("INSPECTIONDEFVERSION").ToSafeString()),
                                                                   sENTERPRISEID = grp.Where(x => x.Field<object>("ENTERPRISEID") != null).Max(x => x.Field<object>("ENTERPRISEID").ToSafeString()),
                                                                   sPLANTID = grp.Where(x => x.Field<object>("PLANTID") != null).Max(x => x.Field<object>("PLANTID").ToSafeString())
                                                               });
                        foreach (var s in dtGroupData)
                        {
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "RESOURCETYPE", s.sRESOURCETYPE.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "INSPITEMID", s.sINSPITEMID.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "INSPITEMVERSION", s.sINSPITEMVERSION.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "INSPITEMIDNAME", s.sINSPITEMIDNAME.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "PROCESSRELNO", s.sPROCESSRELNO.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "AREAID", s.sAREAID.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "PRODUCTDEFID", s.sPRODUCTDEFID.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "PRODUCTDEFVERSION", s.sPRODUCTDEFVERSION.ToSafeString());
                            //SpcFunction.IsDbNckStringWrite(dataRowOverRule, "PRODUCTDEFNAME", s.sPRODUCTDEFNAME.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "PROCESSSEGMENTID", s.sPROCESSSEGMENTID.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "PROCESSSEGMENTVERSION", s.sPROCESSSEGMENTVERSION.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "PROCESSSEGMENTNAME", s.sPROCESSSEGMENTNAME.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "EQUIPMENTID", s.sEQUIPMENTID.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "EQUIPMENTNAME", s.sEQUIPMENTNAME.ToSafeString());
                            //SpcFunction.IsDbNckStringWrite(dataRowOverRule, "VENDORNAME", s.sVENDORNAME.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "INSPECTIONDEFID", s.sINSPECTIONDEFID.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "INSPECTIONDEFVERSION", s.sINSPECTIONDEFVERSION.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "ENTERPRISEID", s.sENTERPRISEID.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "PLANTID", s.sPLANTID.ToSafeString());

                        }


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

        private void CheckData(DataTable table)
        {
            if (table.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
        }

        private void SearchDefectList()
        {
            DataRow row = null;// grdRawAssyInspectionResult.View.GetFocusedDataRow();
            if (row == null)
            {
                ///grdDefectHistory.DataSource = null;
                return;
            } 

            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                {"RESOURCEID", row["MATERIALLOTID"]},
                {"RESOURCETYPE", "MaterialLot"},
                {"TXNGROUPHISTKEY", row["TXNGROUPHISTKEY"]},
                {"LANGUAGETYPE", Framework.UserInfo.Current.LanguageType}
            };

            ///grdDefectHistory.DataSource = SqlExecuter.Query("SelectRawAssyDefectRate", "10001", values);
        }

        //popup이 닫힐때 그리드를 재조회 하는 이벤트 (팝업결과 저장 후 재조회)
        private async void Popup_FormClosed()
        {
            await OnSearchAsync();
        }
        /// <summary>
        /// Form Resize
        /// </summary>
        private void FormResize()
        {
            try
            {
                int position = 190;
                int rate = 4;
                this.ucXBarFrame1.splMain.SplitterPosition = position;
                this.ucXBarFrame1.splMainSub.SplitterPosition = this.Height - (this.Height / rate);
                this.ucCpkFrame1.splMain.SplitterPosition = position;
                this.ucCpkFrame1.splMainSub.SplitterPosition = this.Height - (this.Height / rate);
            }
            catch (Exception)
            {
                //throw;
            }
        }
        #endregion


    }
}
