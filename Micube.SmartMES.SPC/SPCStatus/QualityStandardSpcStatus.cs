#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.SPCLibrary;
using Micube.SmartMES.SPC.UserControl;

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
    /// 프 로 그 램 명  : ex> SPC > 퓸질규격 종합 SPC 현황
    /// 업  무  설  명  : 품질규격 측정값을 분석하여 관리도 및 공정능력분석함.
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-08-07
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class QualityStandardSpcStatus : SmartConditionManualBaseForm
    {
        #region Local Variables



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
        /// XBarFrame 버튼 Checvk 유무
        /// </summary>
        private bool _isAgainAnalysisButtonXBar = false;
        /// <summary>
        /// XBarFrame 버튼 Checvk 유무 - Cpk
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
        /// <summary>
        /// Chart 분석용 Group 표시형, RawData 표시.
        /// </summary>
        private bool _isChartAnalysisRawDataMode = false;
        /// <summary>
        /// Sub Group 구분
        /// </summary>
        string _SubGroupType = "";
        #endregion

        private string _PRODUCTDEFVERSION_Data = "";
        #endregion

        #region 생성자

        public QualityStandardSpcStatus()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();
            InitializeControls();
            InitializeLanguageKey();
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
            #region RowData 
            
            grdRawData.GridButtonItem = GridButtonItem.Export;
            grdRawData.View.SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("SUBGROUP", 120).SetLabel("SPCRAWDATAVIEWSUBGROUP");
            grdRawData.View.AddTextBoxColumn("SUBGROUPNAME", 120).SetLabel("SPCRAWDATAVIEWSUBGROUPNAME");
            grdRawData.View.AddTextBoxColumn("SAMPLING", 120).SetLabel("SPCRAWDATAVIEWSAMPLING");
            grdRawData.View.AddTextBoxColumn("SAMPLINGNAME", 120).SetLabel("SPCRAWDATAVIEWSAMPLINGNAME");
            grdRawData.View.AddTextBoxColumn("SAMPLINGLOTID", 120).SetLabel("SPCRAWDATAVIEWSAMPLINGLOTID");
            grdRawData.View.AddTextBoxColumn("SAMPLINGNAMELOTID", 120).SetLabel("SPCRAWDATAVIEWSAMPLINGNAMELOTID");
            grdRawData.View.AddTextBoxColumn("SAMPLINGIMR", 120).SetLabel("SPCRAWDATAVIEWSAMPLINGIMR");
            grdRawData.View.AddTextBoxColumn("SAMPLINGIMRNAME", 120).SetLabel("SPCRAWDATAVIEWSAMPLINGIMRNAME");
            grdRawData.View.AddTextBoxColumn("SAMPLINGIMRLOTID", 120).SetLabel("SPCRAWDATAVIEWSAMPLINGIMRLOTID");
            grdRawData.View.AddTextBoxColumn("SAMPLINGIMRNAMELOTID", 120).SetLabel("SPCRAWDATAVIEWSAMPLINGIMRNAMELOTID");

            grdRawData.View.AddTextBoxColumn("MEASUREDATETIMEVIEWDATE", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("RESOURCETYPE", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("LOTID", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("PLANTID", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("AREAID", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("AREANAME", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("PROCESSSEGMENTID", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("EQUIPMENTID", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("EQUIPMENTNAME", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("MEASURER", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("CUSTOMERID", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("PRODUCTDEFID", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("PRODUCTDEFIDNAME", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("PRODUCTDEFVERSION", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("LOTTYPE", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("DAITEMID", 120).SetLabel("SPCDAITEMID").SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("INSPITEMNAME", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("SPECRANGE", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("INSPECTIONRESULT", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("MAXVALUE", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("MINVALUE", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("DAPOINTID", 120).SetLabel("SPCDAPOINTID").SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("NVALUE", 120).SetLabel("SPCNVALUE").SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("CONTROLTYPE", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("TARGET", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("USL", 120).SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("LSL", 120).SetIsReadOnly();

            grdRawData.View.PopulateColumns();

            grdRawData.View.Columns["SUBGROUP"].OwnerBand.Visible = _isChartAnalysisRawDataMode;
            grdRawData.View.Columns["SUBGROUPNAME"].OwnerBand.Visible = _isChartAnalysisRawDataMode;
            grdRawData.View.Columns["SAMPLING"].OwnerBand.Visible = _isChartAnalysisRawDataMode;
            grdRawData.View.Columns["SAMPLINGNAME"].OwnerBand.Visible = _isChartAnalysisRawDataMode;
            grdRawData.View.Columns["SAMPLINGLOTID"].OwnerBand.Visible = _isChartAnalysisRawDataMode;
            grdRawData.View.Columns["SAMPLINGNAMELOTID"].OwnerBand.Visible = _isChartAnalysisRawDataMode;
            grdRawData.View.Columns["SAMPLINGIMR"].OwnerBand.Visible = _isChartAnalysisRawDataMode;
            grdRawData.View.Columns["SAMPLINGIMRNAME"].OwnerBand.Visible = _isChartAnalysisRawDataMode;
            grdRawData.View.Columns["SAMPLINGIMRLOTID"].OwnerBand.Visible = _isChartAnalysisRawDataMode;
            grdRawData.View.Columns["SAMPLINGIMRNAMELOTID"].OwnerBand.Visible = _isChartAnalysisRawDataMode;

            #endregion

            #region Over Rule
            grdOverRules.GridButtonItem = GridButtonItem.Export;
            grdOverRules.View.SetIsReadOnly();
            //grdOverRules.View.AddTextBoxColumn("TEMPID", 120);
            //grdOverRules.View.AddTextBoxColumn("GROUPID", 120);
            //grdOverRules.View.AddTextBoxColumn("SUBGROUP", 120);
            grdOverRules.View.AddTextBoxColumn("SUBGROUPNAME", 120).SetLabel("SPCOVERDATAVIEWSUBGROUPNAME");
            //grdOverRules.View.AddTextBoxColumn("SAMPLING", 120);
            grdOverRules.View.AddTextBoxColumn("SAMPLINGNAME", 120).SetLabel("SPCOVERDATAVIEWSAMPLINGNAMEXAXIS");
            grdOverRules.View.AddTextBoxColumn("PRODUCTDEFID", 120);
            grdOverRules.View.AddTextBoxColumn("PRODUCTDEFIDNAME", 120);
            grdOverRules.View.AddTextBoxColumn("PROCESSSEGMENTID", 120);
            grdOverRules.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120);
            grdOverRules.View.AddTextBoxColumn("AREAID", 120);
            grdOverRules.View.AddTextBoxColumn("AREANAME", 120);
            grdOverRules.View.AddTextBoxColumn("EQUIPMENTID", 120);
            grdOverRules.View.AddTextBoxColumn("EQUIPMENTNAME", 120);
            grdOverRules.View.AddTextBoxColumn("DAITEMID", 120).SetLabel("SPCDAITEMID");
            grdOverRules.View.AddTextBoxColumn("INSPITEMNAME", 120).SetIsReadOnly();
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

        /// <summary>
        /// Control Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
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
            
        }
        /// <summary>
        /// 품 Load 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QualityStandardSpcStatus_Load(object sender, EventArgs e)
        {
            this.tabAnalysis.PageVisible = true;
            SpcClass.SpcDictionaryDataSetting();
            //XBar
            (tabControlChart.Controls[0] as ucXBarFrame).SpcCpkChartEnterEventHandler += QualityStandardSpcStatus_SpcCpkChartEnterEventHandler;
            (tabControlChart.Controls[0] as ucXBarFrame).ucXBarGrid01.ucXBar1.SpcChartDirectMessageUserEventHandler += UcXBar1_SpcChartDirectMessageUserEventHandler;
            (tabControlChart.Controls[0] as ucXBarFrame).ucXBarGrid01.ucXBar2.SpcChartDirectMessageUserEventHandler += UcXBar2_SpcChartDirectMessageUserEventHandler;
            (tabControlChart.Controls[0] as ucXBarFrame).ucXBarGrid01.ucXBar3.SpcChartDirectMessageUserEventHandler += UcXBar3_SpcChartDirectMessageUserEventHandler;
            (tabControlChart.Controls[0] as ucXBarFrame).ucXBarGrid01.ucXBar4.SpcChartDirectMessageUserEventHandler += UcXBar4_SpcChartDirectMessageUserEventHandler;
            (tabControlChart.Controls[0] as ucXBarFrame).cboChartTypeSetting(1);
            (tabControlChart.Controls[0] as ucXBarFrame).SpcChartXBarFrameDirectMessageUserEventHandler += QualityStandardSpcStatus_SpcChartXBarFrameDirectMessageUserEventHandler;

            //Wait
            (tabControlChart.Controls[0] as ucXBarFrame).SpcVtChartShowWaitAreaEventHandler += QualityStandardSpcStatus_SpcVtChartShowWaitAreaEventHandler;
            //XBar
            (tabControlChart.Controls[0] as ucXBarFrame).ucXBarGrid01.SpcVtChartShowWaitAreaEventHandler += UcXBarGrid01_SpcVtChartShowWaitAreaEventHandler;
            //Cpk
            (tabProcess.Controls[0] as ucCpkFrame).ucCpkGrid01.SpcVtChartShowWaitAreaEventHandler += UcCpkGrid01_SpcVtChartShowWaitAreaEventHandler;

            this.ucAnalysisPlot1.SpcVtChartShowWaitAreaEventHandler += UcAnalysisPlot1_SpcVtChartShowWaitAreaEventHandler;
            this.tabMain.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tabMain_SelectedPageChanged);
            InitializeGrid();
            FormResize();
        }





        #region Chart Wait 이벤트
        /// <summary>
        /// XBarFrame Chart Change Wait
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="se"></param>
        private void QualityStandardSpcStatus_SpcVtChartShowWaitAreaEventHandler(object sender, EventArgs e, SpcShowWaitAreaOption se)
        {
            se.CheckValue = se.ChartTypeChange;
            UcChartGridShowWaitArea(sender, e, se);
            this.ChartAnalysisRawDataView(false);
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
        /// 품 Resize 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QualityStandardSpcStatus_Resize(object sender, EventArgs e)
        {
            FormResize();
        }
        /// <summary>
        /// Chart Option 변경.
        /// </summary>
        /// <param name="f"></param>
        private void QualityStandardSpcStatus_SpcCpkChartEnterEventHandler(Commons.SPCLibrary.SpcFrameChangeData f)
        {
            _isAgainAnalysisXBar = f.isAgainAnalysisXBar;
            _isAgainAnalysisCpk = f.isAgainAnalysisCpk;
            _isAgainAnalysisPlot = f.isAgainAnalysis;
            _isAgainAnalysisButtonXBar = f.isAgainAnalysisButtonXBar;
            _isAgainAnalysisButtonCpk = f.isAgainAnalysisButtonCpk;
            _AnalysisParameter.spcOption = f.SPCOption;
        }
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
        /// <summary>
        /// XBarFrame Message 이벤트
        /// </summary>
        /// <param name="msg"></param>
        private void QualityStandardSpcStatus_SpcChartXBarFrameDirectMessageUserEventHandler(SpcEventsChartMessage msg)
        {
            ShowMessage(msg.mainMessage);
        }


        #endregion

        #region 검색
        /// <summary>
        /// Main자료조회 - 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            
            string subgroupNameFiled = "";

            //조회 Skip...
            if (_isCancelOnSearchAsync) return;

            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);
                await base.OnSearchAsync();

                var values = Conditions.GetValues();
                values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
                Dictionary<string, object> param = values;
                _SubGroupType = param["P_SPCAXISTYPE"].ToSafeString();
                Console.WriteLine(values.Count());

                string isSymbolDefault = @" || '@#' || ";
                string isSymbol = "";
                string productDefind = "";
                string equipmentId = "";
                string processsegmentId = "";

                productDefind = param["P_PRODUCTDEFID"].ToSafeString();
                equipmentId = param["P_EQUIPMENTID"].ToSafeString();
                processsegmentId = param["P_PROCESSSEGMENTID"].ToSafeString();

                string dateStart = param["P_PERIOD_PERIODFR"].ToSafeString();
                string dateEnd = param["P_PERIOD_PERIODTO"].ToSafeString();

                subgroupNameFiled = "";
                if (productDefind != "")
                {
                    subgroupNameFiled += string.Format(@"{0}coalesce(A.PRODUCTDEFID, A.PRODUCTDEFID, 'NULL')", isSymbol);
                    isSymbol = isSymbolDefault;
                }

                if (equipmentId != "")
                {
                    subgroupNameFiled += string.Format(@"{0}coalesce(A.EQUIPMENTID, A.EQUIPMENTID, 'NULL')", isSymbol);
                    isSymbol = isSymbolDefault;
                }

                if (processsegmentId != "")
                {

                    subgroupNameFiled += string.Format(@"{0}coalesce(A.PROCESSSEGMENTID, A.PROCESSSEGMENTID, 'NULL')", isSymbol);
                    isSymbol = isSymbolDefault;
                }

                if (subgroupNameFiled != "")
                {
                    subgroupNameFiled += string.Format(@"{0}coalesce(A.DAITEMID, A.DAITEMID, 'NULL')", isSymbol);
                }
                else
                {
                    subgroupNameFiled = "*";
                }

                values.Add("P_SUBGROUPNAMEFILED", subgroupNameFiled);//미사용.

                //품목이 없을시 품목 버전 초기화
                if (productDefind == "")
                {
                    _PRODUCTDEFVERSION_Data = "";
                }

                values.Add("P_PRODUCTDEFVERSION", _PRODUCTDEFVERSION_Data);

                //sia작업 : SPC품질규격 - 메인조회.
                #region 5.통계용 입력자료 DB 조회
                
                DataTable InputRawMaxCount;
                DataTable tempInputSpec;
                tempInputSpec = SpcClass.SpcCreateMainRawSpecTable();
                this.grdRawData.DataSource = null;
                this.grdRawData.View.ClearDatas();
                this.ChartAnalysisRawDataView(false);
                this.grdOverRules.DataSource = null;
                this.grdOverRules.View.ClearDatas();

                //전체 조회 Count 조회.
                InputRawMaxCount = await SqlExecuter.QueryAsync("GetSpcQualitySpecificationRawMaxCount", "10001", values);
                if (InputRawMaxCount != null && InputRawMaxCount.Rows.Count < 1)
                {
                    // 조회할 데이터가 없습니다.
                    (tabControlChart.Controls[0] as ucXBarFrame).ClearChartControlsXBAR();
                    (tabProcess.Controls[0] as ucCpkFrame).ClearChartControlsCpk();
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
                    _AnalysisParameter.dtInputRawData = await SqlExecuter.QueryAsync("GetSpcQualitySpecificationRaw", "10001", values);
                }
                else
                {
                    //values.Add("P_PRODUCTDEFVERSION", _PRODUCTDEFVERSION_Data);
                    await this.InputDataMerge(dateStart, dateEnd, values);
                }

                #region SPEC 조회 - Linq형
                //Spec 입력자료 구성.
                if (_AnalysisParameter.dtInputRawData != null && _AnalysisParameter.dtInputRawData.Rows.Count < 1)
                {
                    (tabControlChart.Controls[0] as ucXBarFrame).ClearChartControlsXBAR();
                    (tabProcess.Controls[0] as ucCpkFrame).ClearChartControlsCpk();
                    // 조회할 데이터가 없습니다.
                    ShowMessage("NoSelectData");
                    return;
                }

                try
                {
                    //.GroupBy(x => new { x.Age, x.Sex })
                    //var grouped = myTable.AsEnumerable().GroupBy(r => new { pp1 = r.Field<int>("col1"), pp2 = r.Field<int>("col2") });
                    var dtSpecData = _AnalysisParameter.dtInputRawData.AsEnumerable()
                                                           //.GroupBy(m => m.Field<string>("SUBGROUP"))
                                                           //.GroupBy(m => new { SUBGROUP = m.Field<string>("SUBGROUP"), CTYPE = m.Field<string>("CONTROLTYPE") })
                                                           .GroupBy(m => new { SUBGROUP = m.Field<string>("SUBGROUP") })
                                                           .Select(grp => new
                                                           {
                                                               sSUBGROUP = grp.Key.SUBGROUP.ToSafeString(),
                                                               //sCTYPE = grp.Key.CTYPE.ToSafeString(),
                                                               //nUSL = grp.Where(x => x.Field<object>("USL") != null).Max(x => x.Field<object>("USL").ToSafeDoubleZero()),
                                                               //nTSL = grp.Where(x => x.Field<object>("TARGET") != null).Max(x => x.Field<object>("TARGET").ToSafeDoubleZero()),
                                                               //nLSL = grp.Where(x => x.Field<object>("LSL") != null).Min(x => x.Field<object>("LSL").ToSafeDoubleZero())
                                                               nUSL = grp.Max(x => x.Field<object>("USL").ToSafeDoubleStaMax()),
                                                               nTSL = grp.Max(x => x.Field<object>("TARGET").ToSafeDoubleStaMax()),
                                                               nLSL = grp.Min(x => x.Field<object>("LSL").ToSafeDoubleStaMin())
                                                           });
                    foreach (var s in dtSpecData)
                    {
                        DataRow dataRawSpec = tempInputSpec.NewRow();

                        SpcFunction.IsDbNckStringWrite(dataRawSpec, "SUBGROUP", s.sSUBGROUP);
                        //SpcFunction.IsDbNckStringWrite(dataRawSpec, "CTYPE", s.sCTYPE);
                        SpcFunction.IsDbNckStringWrite(dataRawSpec, "CTYPE", "XBARR");
                        //SpcFunction.IsDbNckStringWrite(dataRawSpec, "SPECTYPE", s.nUSL);

                        SpcFunction.IsDbNckDoubleWrite(dataRawSpec, "USL", s.nUSL);
                        SpcFunction.IsDbNckDoubleWrite(dataRawSpec, "SL", s.nTSL);
                        SpcFunction.IsDbNckDoubleWrite(dataRawSpec, "LSL", s.nLSL);
                        tempInputSpec.Rows.Add(dataRawSpec);
                        //SpcFunction.IsDbNckDoubleWrite(dataRawSpec, "LCL", s.nLCL);
                        //SpcFunction.IsDbNckDoubleWrite(dataRawSpec, "CL", s.nTCL);
                        //SpcFunction.IsDbNckDoubleWrite(dataRawSpec, "UCL", s.nUCL);

                        //SpcFunction.IsDbNckDoubleWrite(dataRawSpec, "UOL", s.nUOL);
                        //SpcFunction.IsDbNckDoubleWrite(dataRawSpec, "LOL", s.nUOL);

                    }
                    //}).ToList();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine(tempInputSpec.Rows.Count);
                _AnalysisParameter.dtInputSpecData = tempInputSpec;
                #endregion SPEC 조회 - Linq형

                //_AnalysisParameter.dtInputSpecData = await SqlExecuter.QueryAsync("GetSpcQualitySpecificationSpec", "10001", values);
                //if (_AnalysisParameter.dtInputSpecData != null && _AnalysisParameter.dtInputSpecData.Rows.Count < 1)
                //{
                //    // 조회할 데이터가 없습니다.
                //    ShowMessage("NoSelectData - Spec Data");
                //}

                //Chart 구분
                string chartType = (tabControlChart.Controls[0] as ucXBarFrame).cboLeftChartType.GetDataValue().ToSafeString().ToUpper().Replace("_", "");
                _AnalysisParameter.spcOption.specDefaultChartType = "XBARR";
                _AnalysisParameter.spcOption.chartName.xBarChartType = chartType;
                _AnalysisParameter.spcOption.chartName.xCpkChartType = chartType;
                _AnalysisParameter.spcOption.ChartTypeSetting(chartType, ref _AnalysisParameter.spcOption);
                _AnalysisParameter.spcOption.sigmaType = (tabControlChart.Controls[0] as ucXBarFrame).chkLeftEstimate.Checked ? SigmaType.Yes : SigmaType.No;//추정치 사용 유무
                _AnalysisParameter.AgainAnalysisSigmaType = _AnalysisParameter.spcOption.sigmaType;

                //*통계 분석 실행
                _isAgainAnalysisExe = true;
                _isAgainAnalysisXBar = true;
                _isAgainAnalysisCpk = true;
                _isAgainAnalysisPlot = true;
                _isAgainAnalysisButtonXBar = true;
                _isAgainAnalysisButtonCpk = true;
                _isAgainAnalysisRawData = true;
                _isAgainAnalysisOverRules = true;
                //Subgroup Filed명 재설정.
                //_AnalysisParameter.dtInputRawMainFieldName.subgroupNameFiled = "SUBGROUPIMR";
                //_AnalysisParameter.dtInputRawMainFieldName.subgroupNameFiledName = "RELIASUBGROUPNAME";

                switch (chartType.ToUpper())
                {
                    case "I":
                    case "MR":
                    case "IMR":
                        switch (_SubGroupType)
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
                        switch (_SubGroupType)
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
                                break;
                        }

                        break;
                }

                //통계 분석
                this.ChartAnalysisExecution();


                #endregion

                //Grid에 Raw Data 표시.
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



        /// <summary>
        /// 조회 조건 설정
        /// 검색조건 초기화. 
        /// 조회조건 정보, 메뉴 - 조회조건 매핑 화면에 등록된 정보를 기준으로 구성됩니다.
        /// DB에 등록한 정보를 제외한 추가 조회조건 구성이 필요한 경우 사용합니다.
        /// </summary>
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
            //Chart Subgroup 구분
            this.Conditions.AddComboBox("p_SpcAxisType", new SqlQuery("GetSpcSearchComboCom", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"P_CODECLASSID={"SpcAxisType"}"), "CODENAME", "CODEID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetDefault("DATE")
                .SetValidationIsRequired()
                .SetLabel("조회조건")
                .SetPosition(1.1); // Subgroup 조회조건 
            //품목(제품, 모델) Popup
            InitializeConditionPopup_Product();//4
            //ProductPopup();
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
        /// <summary>
        /// 조건 Controls 설정.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
        }

        /// <summary>
        /// 품목 조회조건
        /// </summary>
        private void InitializeConditionPopup_Product()
        {
            _PRODUCTDEFVERSION_Data = "";
            // 팝업 컬럼설정
            var productPopup = Conditions.AddSelectPopup("p_productdefId", new SqlQuery("GetSpcSearchProductandVersion", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFNAME", "PRODUCTDEFID")
               .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(750, 600)
               .SetLabel("PRODUCT")
               //.SetDefault("1","2","3")
               .SetPopupResultCount(1)
               .SetPosition(2.4)
               //.SetValidationIsRequired()
               .SetRelationIds("p_plantId")
               .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                {
                    foreach (DataRow row in selectedRow)
                    {
                        _PRODUCTDEFVERSION_Data = row["PRODUCTDEFVERSION"].ToString();
                    }
                });
            // 팝업 조회조건
            productPopup.Conditions.AddTextBox("PRODUCTDEFIDNAME");
            //productPopup.Conditions.AddTextBox("P_PRODUCTDEFVERSION");
            // 팝업 그리드
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetValidationKeyColumn();
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 300);
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            
            //productPopup.DefaultDisplayValue = "*";
            //productPopup.DefaultValue = "*";
            //Conditions.items[3].Control.ForeColor = Color.Red;
            //new System.Collections.Generic.Mscorlib_CollectionDebugView<Micube.Framework.SmartControls.ConditionCollectionItem<System.Windows.Forms.Control>>(Conditions._items).Items[0]
        }
        //sia조건 : 품질규격 공정조건
        /// <summary>
        /// 표준공정(원인공정) 조회조건
        /// </summary>
        private void InitializeConditionPopup_ReasonSegment()
        {
            // 팝업 컬럼설정
            var discoverySegment = Conditions.AddSelectPopup("P_PROCESSSEGMENTID", new SqlQuery("GetSpcSearchSmallProcesssegment", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"),  "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
               .SetPopupLayout("REASONSEGMENT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(600, 600)
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
               .SetPopupResultCount(1)
               .SetPosition(2.8);
            // .SetRelationIds("p_processsegmentclassId", "p_equipmentId", "p_childEquipmentId");

            // 팝업 조회조건
            inspItemPopupColumn.Conditions.AddTextBox("INSPITEMIDNAME")
                .SetLabel("INSPITEMIDNAME");

            // 팝업 그리드
            inspItemPopupColumn.GridColumns.AddTextBoxColumn("INSPITEMID", 100)
                .SetValidationKeyColumn();
            inspItemPopupColumn.GridColumns.AddTextBoxColumn("INSPITEMNAME", 150);
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

        /// <summary>
        /// 품목 팝업
        /// </summary>
        /// <returns></returns>
        private ConditionItemSelectPopup ProductPopup()
        {
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();

            popup.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("PRODUCTLIST", PopupButtonStyles.Ok_Cancel, true, true);
            popup.Id = "PRODUCT";
            popup.SearchQuery = new SqlQuery("GetProductListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "PRODUCTDEFNAME";
            popup.ValueFieldName = "PRODUCTDEFID";
            popup.LanguageKey = "PRODUCT";
            popup.SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
            {
                foreach (DataRow row in selectedRow)
                {
                    //popupProduct.Tag = row["PRODUCTDEFVERSION"].ToString();
                }
            });

            popup.Conditions.AddTextBox("PRODUCTDEFIDNAME");

            popup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            popup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            popup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100)
                .SetIsHidden();

            return popup;
        }

        /// <summary>
        /// Lot 팝업
        /// </summary>
        /// <returns></returns>
        private ConditionItemSelectPopup LotPopup()
        {
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();

            popup.SetPopupLayoutForm(800, 800, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("LOTLIST", PopupButtonStyles.Ok_Cancel, true, true);
            popup.Id = "LOT";
            popup.SearchQuery = new SqlQuery("GetLotIdListByReliabilityVerificationNonRegularRequest", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "LOTID";
            popup.ValueFieldName = "LOTID";

            popup.Conditions.AddTextBox("LOTID");

            popup.GridColumns.AddTextBoxColumn("LOTID", 180)
                .SetLabel("Lot No"); // Lot ID
            popup.GridColumns.AddComboBoxColumn("LOTTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("PRODUCTIONTYPE")
                .SetTextAlignment(TextAlignment.Center); // 양산구분
            popup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150); // 품목코드
            popup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetTextAlignment(TextAlignment.Center); // 품목버전         
            popup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200); // 품목명    
            popup.GridColumns.AddTextBoxColumn("PROCESSDEFID", 150); // 라우팅
            popup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150); // 공정 ID
            popup.GridColumns.AddTextBoxColumn("USERSEQUENCE", 80)
                .SetTextAlignment(TextAlignment.Center); // 순서
            popup.GridColumns.AddTextBoxColumn("PLANTID", 120); // Site ID
            popup.GridColumns.AddTextBoxColumn("AREAID", 120); // 작업장 ID
            popup.GridColumns.AddTextBoxColumn("RTRSHT", 100); // Roll/Sheet           
            popup.GridColumns.AddTextBoxColumn("UNIT", 80)
                .SetTextAlignment(TextAlignment.Center); // 단위
            popup.GridColumns.AddSpinEditColumn("QTY", 90); // 수량
            popup.GridColumns.AddSpinEditColumn("PCSQTY", 90); // PCS 수량
            popup.GridColumns.AddSpinEditColumn("PANELQTY", 90);  // PNL 수량
            popup.GridColumns.AddSpinEditColumn("M2QTY", 90); // M2 수량

            return popup;
        }

        /// <summary>
        /// 고객사 팝업
        /// </summary>
        /// <returns></returns>
        private ConditionItemSelectPopup VendorPopup()
        {
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();

            popup.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("VENDORLIST", PopupButtonStyles.Ok_Cancel, true, true);
            popup.Id = "VENDOR";
            popup.SearchQuery = new SqlQuery("GetVendorListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "VENDORNAME";
            popup.ValueFieldName = "VENDORID";

            popup.Conditions.AddTextBox("VENDORIDNAME");

            popup.GridColumns.AddTextBoxColumn("VENDORID", 150);
            popup.GridColumns.AddTextBoxColumn("VENDORNAME", 200);

            return popup;
        }

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
            List<SpcConditionDateDays> rtnlstHour;
            bool isHour = false;
            DataTable dtInputFirstData = new DataTable();
            DataTable dtInputHour = new DataTable();
            //var values = Conditions.GetValues();
            //string startDate = "2020-02-01 08:30:00";
            //string endDate = "2020-03-01 08:30:00";
            rtnlstDay = SpcClass.SpcConditionList(startDate, endDate);
            
            //await base.OnSearchAsync();
            int niCount = 0;
            foreach (SpcConditionDateDays days in rtnlstDay)
            {
                niCount ++;
                param["P_PERIOD_PERIODFR"] = days.DateStart;
                param["P_PERIOD_PERIODTO"] = days.DateEnd;
                Console.WriteLine(string.Format("{0} : {1} ~ {2}", niCount,  days.DateStart, days.DateEnd));
                if (niCount != 1)
                {
                    DataTable dtInputPartData = null;

                    try
                    {
                        isHour = false;
                        dtInputPartData = await SqlExecuter.QueryAsync("GetSpcQualitySpecificationRaw", "10001", param);
                    }
                    catch (Exception ex1)
                    {
                        isHour = true;
                        Console.WriteLine(ex1.Message);
                    }

                    #region 시간별 조회
                    if (isHour)
                    {
                        rtnlstHour = SpcClass.SpcConditionListHour(days.DateStart, days.DateEnd);
                        foreach (SpcConditionDateDays hours in rtnlstHour)
                        {
                            dtInputHour = null;
                            param["P_PERIOD_PERIODFR"] = hours.DateStart;
                            param["P_PERIOD_PERIODTO"] = hours.DateEnd;
                            try
                            {
                                dtInputHour = await SqlExecuter.QueryAsync("GetSpcQualitySpecificationRaw", "10001", param);

                                for (int i = 0; i < dtInputHour.Rows.Count; i++)
                                {
                                    DataRow rowData = dtInputHour.Rows[i];
                                    dtInputFirstData.ImportRow(rowData);
                                }
                            }
                            catch (Exception exh)
                            {
                                Logger.Error(exh.Message);
                            }

                        }
                    }
                    #endregion 시간별 조회

                    if (dtInputPartData == null)
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
                    try
                    {
                        isHour = false;
                        dtInputFirstData = await SqlExecuter.QueryAsync("GetSpcQualitySpecificationRaw", "10001", param);
                    }
                    catch (Exception ex1)
                    {
                        isHour = true;
                        Console.WriteLine(ex1.Message);
                    }

                    #region 시간별 조회
                    if (isHour)
                    {
                        rtnlstHour = SpcClass.SpcConditionListHour(days.DateStart, days.DateEnd);
                        foreach (SpcConditionDateDays hours in rtnlstHour)
                        {
                            param["P_PERIOD_PERIODFR"] = hours.DateStart;
                            param["P_PERIOD_PERIODTO"] = hours.DateEnd;
                            try
                            {
                                if (dtInputFirstData != null)
                                {
                                    if (dtInputFirstData.Rows.Count <= 0)
                                    {
                                        dtInputFirstData = await SqlExecuter.QueryAsync("GetSpcQualitySpecificationRaw", "10001", param);
                                    }
                                    else
                                    {
                                        dtInputHour = await SqlExecuter.QueryAsync("GetSpcQualitySpecificationRaw", "10001", param);

                                        for (int i = 0; i < dtInputHour.Rows.Count; i++)
                                        {
                                            DataRow rowData = dtInputHour.Rows[i];
                                            dtInputFirstData.ImportRow(rowData);
                                        }
                                    }
                                }
                            }
                            catch (Exception exh)
                            {
                                Logger.Error(exh.Message);
                            }

                        }
                    }
                    #endregion 시간별 조회

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

            dt.Columns.Add("PRODUCTDEFID", typeof(string));
            dt.Columns.Add("PRODUCTDEFIDNAME", typeof(string));
            dt.Columns.Add("PROCESSSEGMENTID", typeof(string));
            dt.Columns.Add("PROCESSSEGMENTNAME", typeof(string));
            dt.Columns.Add("AREAID", typeof(string));
            dt.Columns.Add("AREANAME", typeof(string));
            dt.Columns.Add("EQUIPMENTID", typeof(string));
            dt.Columns.Add("EQUIPMENTNAME", typeof(string));
            dt.Columns.Add("DAITEMID", typeof(string));

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

        #region 4. 통계용 함수
        /// <summary>
        /// Chart 통계 분석 실행.
        /// </summary>
        private void ChartAnalysisExecution()
        {
            string subGroupName = "";

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
                        subGroupName = (tabControlChart.Controls[0] as ucXBarFrame).ucXBarGrid01.grp01.Text.ToSafeString();
                        if (_isAgainAnalysisXBar)
                        {
                            (tabControlChart.Controls[0] as ucXBarFrame).AnalysisExecution(_AnalysisParameter);
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
                    subGroupName = (tabProcess.Controls[0] as ucCpkFrame).ucCpkGrid01.grp01.Text.ToSafeString();
                    if (_isAgainAnalysisCpk)
                    {
                        _AnalysisParameter.spcOption.sigmaType = _AnalysisParameter.AgainAnalysisSigmaType;
                        (tabProcess.Controls[0] as ucCpkFrame).AnalysisExecution(_AnalysisParameter);
                        _isAgainAnalysisCpk = false;
                        _AnalysisParameter.isAgainAnalysisCpk = false;
                    }
                }
                else if (this.tabMain.SelectedTabPageIndex == 2)
                {
                    //Plot Check
                    if (_isAgainAnalysisPlot)
                    {
                        DialogManager.ShowWaitArea(this.pnlContent);
                        this.ucAnalysisPlot1.SubGroupIdSetting((tabControlChart.Controls[0] as ucXBarFrame)._spcPara, ref _isAgainAnalysisPlot);
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
                        DialogManager.ShowWaitArea(this.pnlContent);
                        this.GridOverRules();
                        _isAgainAnalysisOverRules = false;
                    }
                }
            }
            catch (Exception ex)
            {
                //throw Framework.MessageException.Create(ex.ToString());
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (_isAgainAnalysisExe != true)
                {
                    DialogManager.CloseWaitArea(this.pnlContent);
                    //DialogManager.CloseWaitArea((tabControlChart.Controls[0] as ucXBarFrame));
                    //DialogManager.CloseWaitArea((tabProcess.Controls[0] as ucCpkFrame));
                }
            }
            
        }

        ///// <summary>
        ///// 통계 분석 실행 - 공정능력 실행, 관리도 (Tab 이벤트에서 실행함)
        ///// </summary>
        //private void ChartAnalysisExecutionEventMode()
        //{

        //    string subGroupName = "";
        //    if (this.tabMain.SelectedTabPageIndex == 0)
        //    {
        //        this.FormResize();
        //        subGroupName = (tabControlChart.Controls[0] as ucXBarFrame).ucXBarGrid01.grp01.Text.ToSafeString();
        //        if (_isAgainAnalysisXBar)
        //        {
        //            _isAgainAnalysisXBar = false;
        //            (tabControlChart.Controls[0] as ucXBarFrame).AnalysisExecution(_AnalysisParameter);
        //        }
        //    }
        //    else if (this.tabMain.SelectedTabPageIndex == 1)
        //    {
        //        this.FormResize();
        //        subGroupName = (tabProcess.Controls[0] as ucCpkFrame).ucCpkGrid01.grp01.Text.ToSafeString();
        //        //_isAgainAnalysis = true;//sia작업 : 공정능력 실행.,
        //        if (_isAgainAnalysisCpk)
        //        {
        //            _isAgainAnalysisCpk = false;
        //            (tabProcess.Controls[0] as ucCpkFrame).AnalysisExecution(_AnalysisParameter);
        //        }

        //    }
        //}
        #endregion

        #region 5. Over Rules Check 및 Grid 표시.
        /// <summary>
        /// Over Rules Check Data Grid에 표시.
        /// </summary>
        private void GridOverRules()
        {
            //OverRules Grid Data 편집 2/6
            #region OverRules Grid Data 편집
            this.grdOverRules.DataSource = null;
            this.grdOverRules.View.ClearDatas();
            DataTable dtOverRulesTable = null;
            DataTable staChartData = null;
            DataTable staCpkData = null;
            dtOverRulesTable = CreateOverRulesTable();
            staChartData = (tabControlChart.Controls[0] as ucXBarFrame).ucXBarGrid01.rtnChartData;
            staCpkData = (tabControlChart.Controls[0] as ucXBarFrame).ucXBarGrid01.rtnCpkData;
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
                            SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "USL", item["USL"].ToSafeDoubleStaMin());
                            SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "CSL", item["CSL"].ToSafeDoubleStaMin());
                            SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "LSL", item["LSL"].ToSafeDoubleStaMin());
                        }

                        var dtGroupData = _AnalysisParameter.dtInputRawData.AsEnumerable()
                                                               .Where(x => x.Field<string>("SUBGROUP") == subGroupId)
                                                               .GroupBy(m => new { SUBGROUP = m.Field<string>("SUBGROUP") })
                                                               .Select(grp => new
                                                               {
                                                                   sSUBGROUP = grp.Key.SUBGROUP.ToSafeString(),
                                                                   nUSL = grp.Where(x => x.Field<object>("USL") != null).Max(x => x.Field<object>("USL").ToSafeDoubleZero()),
                                                                   nTSL = grp.Where(x => x.Field<object>("TARGET") != null).Max(x => x.Field<object>("TARGET").ToSafeDoubleZero()),
                                                                   nLSL = grp.Where(x => x.Field<object>("LSL") != null).Min(x => x.Field<object>("LSL").ToSafeDoubleZero()),
                                                                   sPRODUCTDEFID = grp.Where(x => x.Field<object>("PRODUCTDEFID") != null).Max(x => x.Field<object>("PRODUCTDEFID").ToSafeString()),
                                                                   sPRODUCTDEFIDNAME = grp.Where(x => x.Field<object>("PRODUCTDEFIDNAME") != null).Max(x => x.Field<object>("PRODUCTDEFIDNAME").ToSafeString()),
                                                                   sPROCESSSEGMENTID = grp.Where(x => x.Field<object>("PROCESSSEGMENTID") != null).Max(x => x.Field<object>("PROCESSSEGMENTID").ToSafeString()),
                                                                   sPROCESSSEGMENTNAME = grp.Where(x => x.Field<object>("PROCESSSEGMENTNAME") != null).Max(x => x.Field<object>("PROCESSSEGMENTNAME").ToSafeString()),
                                                                   sAREAID = grp.Where(x => x.Field<object>("AREAID") != null).Max(x => x.Field<object>("AREAID").ToSafeString()),
                                                                   sAREANAME = grp.Where(x => x.Field<object>("AREANAME") != null).Max(x => x.Field<object>("AREANAME").ToSafeString()),
                                                                   sEQUIPMENTID = grp.Where(x => x.Field<object>("EQUIPMENTID") != null).Max(x => x.Field<object>("EQUIPMENTID").ToSafeString()),
                                                                   sEQUIPMENTNAME = grp.Where(x => x.Field<object>("EQUIPMENTNAME") != null).Max(x => x.Field<object>("EQUIPMENTNAME").ToSafeString()),
                                                                   sDAITEMID = grp.Where(x => x.Field<object>("DAITEMID") != null).Max(x => x.Field<object>("DAITEMID").ToSafeString())
                                                               });
                        foreach (var s in dtGroupData)
                        {
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "PRODUCTDEFID", s.sPRODUCTDEFID.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "PRODUCTDEFIDNAME", s.sPRODUCTDEFIDNAME.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "PROCESSSEGMENTID", s.sPROCESSSEGMENTID.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "PROCESSSEGMENTNAME", s.sPROCESSSEGMENTNAME.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "AREAID", s.sAREAID.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "AREANAME", s.sAREANAME.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "EQUIPMENTID", s.sEQUIPMENTID.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "EQUIPMENTNAME", s.sEQUIPMENTNAME.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "DAITEMID", s.sDAITEMID.ToSafeString());
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


            grdOverRules.DataSource = dtOverRulesTable;
            grdOverRules.View.BestFitColumns();
            grdOverRules.ShowStatusBar = true;

            //Console.WriteLine("^^");
            #endregion OverRules Grid Data 편집
        }

        #endregion 5. Over Rules Check 및 Grid 표시.

        #region 6. Chart 분석용 Group 표시형, RawData 표시.
        /// <summary>
        /// Chart 분석용 Group 표시형, RawData 표시. - 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChartAnalysisRawData_Click(object sender, EventArgs e)
        {
            //293, 4
            try
            {
                if (_isChartAnalysisRawDataMode)
                {
                    ChartAnalysisRawDataView(false);
                }
                else
                {
                    ChartAnalysisRawDataView(true);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

        }
        /// <summary>
        /// Chart 분석용 Group 표시형, RawData 표시.
        /// </summary>
        /// <param name="isValue"></param>
        private void ChartAnalysisRawDataView(bool isValue)
        {
            if (_isChartAnalysisRawDataMode == isValue) return;

            _isChartAnalysisRawDataMode = isValue;
            string chartType = _AnalysisParameter.spcOption.chartName.xBarChartType.ToSafeString();
            grdRawData.View.Columns["SUBGROUP"].OwnerBand.Visible = _isChartAnalysisRawDataMode;
            grdRawData.View.Columns["SUBGROUPNAME"].OwnerBand.Visible = _isChartAnalysisRawDataMode;

            if (!isValue)
            {
                grdRawData.View.Columns["SAMPLING"].OwnerBand.Visible = _isChartAnalysisRawDataMode;
                grdRawData.View.Columns["SAMPLINGNAME"].OwnerBand.Visible = _isChartAnalysisRawDataMode;
                grdRawData.View.Columns["SAMPLINGLOTID"].OwnerBand.Visible = _isChartAnalysisRawDataMode;
                grdRawData.View.Columns["SAMPLINGNAMELOTID"].OwnerBand.Visible = _isChartAnalysisRawDataMode;
                grdRawData.View.Columns["SAMPLINGIMR"].OwnerBand.Visible = _isChartAnalysisRawDataMode;
                grdRawData.View.Columns["SAMPLINGIMRNAME"].OwnerBand.Visible = _isChartAnalysisRawDataMode;
                grdRawData.View.Columns["SAMPLINGIMRLOTID"].OwnerBand.Visible = _isChartAnalysisRawDataMode;
                grdRawData.View.Columns["SAMPLINGIMRNAMELOTID"].OwnerBand.Visible = _isChartAnalysisRawDataMode;
            }

            switch (chartType.ToUpper())
            {
                case "I":
                case "MR":
                case "IMR":
                    switch (_SubGroupType)
                    {
                        case "LOTID":
                            grdRawData.View.Columns["SAMPLINGIMRLOTID"].OwnerBand.Visible = _isChartAnalysisRawDataMode;
                            grdRawData.View.Columns["SAMPLINGIMRNAMELOTID"].OwnerBand.Visible = _isChartAnalysisRawDataMode;
                            break;
                        case "DATE":
                        default:
                            grdRawData.View.Columns["SAMPLINGIMR"].OwnerBand.Visible = _isChartAnalysisRawDataMode;
                            grdRawData.View.Columns["SAMPLINGIMRNAME"].OwnerBand.Visible = _isChartAnalysisRawDataMode;
                            break;
                    }
                    break;
                default:
                    switch (_SubGroupType)
                    {
                        case "LOTID":
                            grdRawData.View.Columns["SAMPLINGLOTID"].OwnerBand.Visible = _isChartAnalysisRawDataMode;
                            grdRawData.View.Columns["SAMPLINGNAMELOTID"].OwnerBand.Visible = _isChartAnalysisRawDataMode;
                            break;
                        case "DATE":
                        default:
                            grdRawData.View.Columns["SAMPLING"].OwnerBand.Visible = _isChartAnalysisRawDataMode;
                            grdRawData.View.Columns["SAMPLINGNAME"].OwnerBand.Visible = _isChartAnalysisRawDataMode;
                            break;
                    }

                    break;
            }

            grdRawData.Refresh();
        }
        #endregion 6. Chart 분석용 Group 표시형, RawData 표시.

        #region Private Function

        /// <summary>
        /// Form Resize
        /// </summary>
        private void FormResize()
        {
            try
            {
                int position = 190;
                int rate = 4;
                (tabControlChart.Controls[0] as ucXBarFrame).splMain.SplitterPosition = position;
                (tabControlChart.Controls[0] as ucXBarFrame).splMainSub.SplitterPosition = this.Height - (this.Height / rate);
                
                (tabProcess.Controls[0] as ucCpkFrame).splMain.SplitterPosition = position;
                (tabProcess.Controls[0] as ucCpkFrame).splMainSub.SplitterPosition = this.Height - (this.Height / rate);
            }
            catch (Exception)
            {
                //throw;
            }
        }


        #endregion

        
    }


}
