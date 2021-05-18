#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
using Micube.SmartMES.Commons.SPCLibrary;
using Micube.Framework.Log;

using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace Micube.SmartMES.SPC
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 신뢰성검증 > 신뢰성 검증 의뢰(정기)
    /// 업  무  설  명  : 신뢰성 정기 의뢰를 하는 화면이다. 동도금 정기적으로 신뢰성 검증 하는 의뢰를 한다. 계측 값 등록 시 자동으로 신뢰성 의뢰가 등록 됨.  
    /// 생    성    자  : 이승우
    /// 생    성    일  : 2019-07-10
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ReliaVerifiSpcStatus : SmartConditionManualBaseForm
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

        /// <summary>
        /// 편집한 Raw Data : 검사항목 및 값 추가.
        /// </summary>
        private DataTable _dtDbRawData;
        /// <summary>
        /// 검사항목 기준정보 저장.
        /// </summary>
        private DataTable _dtItemIdData;

        /// <summary>
        /// Chart 분석용 Group 표시형, RawData 표시.
        /// </summary>
        private bool _isChartAnalysisRawDataMode = false;
        /// <summary>
        /// Sub Group 구분
        /// </summary>
        string _SubGroupType = "";


        #endregion

        #endregion Local Variables

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public ReliaVerifiSpcStatus()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();
        }


        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region RowData
            grdRawData.GridButtonItem = GridButtonItem.Export;

            grdRawData.View.SetIsReadOnly();
            grdRawData.View.SetSortOrder("SUBGROUP");
            grdRawData.View.SetSortOrder("SAMPLING");
            grdRawData.View.SetSortOrder("SAMPLERECEIVEDATEVIEW");

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


            grdRawData.View
                .AddTextBoxColumn("REQUESTDEPT", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 의뢰부서(공정)

            grdRawData.View
                .AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 품목코드

            grdRawData.View
                .AddTextBoxColumn("PRODUCTDEFNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 품목명

            grdRawData.View
                .AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 품목Version

            grdRawData.View
                .AddTextBoxColumn("LOTID", 170)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // LOT NO

            //var manufatureInfo = grdRawData.View.AddGroupColumn("MANUFACTURINFO");

            grdRawData.View
                .AddTextBoxColumn("PROCESSSEGMENTNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 표준공정

            grdRawData.View
                .AddTextBoxColumn("AREANAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 작업장

            grdRawData.View
                .AddTextBoxColumn("EQUIPMENTNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 설비(호기)

            grdRawData.View
                .AddTextBoxColumn("ITEMID", 150)
                .SetLabel("SPCDAPOINTID")
                .SetTextAlignment(TextAlignment.Center); // 검사항목 ID

            grdRawData.View
                .AddTextBoxColumn("ITEMNAME", 150)
                .SetLabel("SPCDAPOINTNAME")
                .SetTextAlignment(TextAlignment.Center); // 검사항목 명

            grdRawData.View
                .AddTextBoxColumn("NVALUE", 150)
                .SetLabel("SPCNVALUE")
                .SetTextAlignment(TextAlignment.Right); // 측정값
            //-------------------------------------------------------------------------------------

            grdRawData.View
                .AddTextBoxColumn("SAMPLERECEIVEDATEVIEW", 180)
                .SetLabel("SAMPLERECEIVEDATE")
                .SetTextAlignment(TextAlignment.Center); // 시료접수일시

            grdRawData.View
                .AddTextBoxColumn("VERIFICOMPDATEVIEW", 180)
                .SetLabel("VERIFICOMPDATE")
                .SetTextAlignment(TextAlignment.Center); // 검증완료일시

            grdRawData.View
                .AddTextBoxColumn("TRANSITIONDATEVIEW", 180)
                .SetLabel("TRANSITIONDATE")
                .SetTextAlignment(TextAlignment.Right); // 경과일

            grdRawData.View
                .AddTextBoxColumn("TRACKOUTTIME", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 작업종료시간

            grdRawData.View
                .AddTextBoxColumn("INSPECTIONRESULT", 150)
                .SetDefault(false)
                .SetTextAlignment(TextAlignment.Center); // 판정결과

            grdRawData.View
                .AddTextBoxColumn("ISNCRPUBLISH", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // NCR발행여부

            grdRawData.View
                .AddTextBoxColumn("DEFECTCODENAME", 150)
                .SetLabel("DEFECTNAME")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 불량명

            grdRawData.View.AddTextBoxColumn("QCSEGMENTNAME", 150);

            grdRawData.View
                .AddTextBoxColumn("ISCOMPLETION", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 완료여부

            grdRawData.View
                .AddTextBoxColumn("ISREREQUEST", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 재의뢰여부

            //grdRawData.View.AddTextBoxColumn("INSPECTIONDATETIMEVIEWDATE", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("RESOURCETYPE", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("LOTID", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("INSPITEMID", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("INSPITEMVERSION", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("INSPITEMIDNAME", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("PROCESSRELNO", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("AREAID", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("PRODUCTDEFID", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("PRODUCTDEFVERSION", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("PRODUCTDEFNAME", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("PROCESSSEGMENTID", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("EQUIPMENTID", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("EQUIPMENTNAME", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("VENDORNAME", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("INSPECTIONDEFID", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("ENTERPRISEID", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("PLANTID", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("LSL", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("USL", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("SL", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("LCL", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("UCL", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("LOL", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("UOL", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("NVALUE", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("NSUBVALUE", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("SAMPLEQTY", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("SPECOUTQTY", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("INSPECTIONRESULT", 120).SetIsReadOnly();


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
        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            //this.tabMain.SelectedPageChanged += TabReliabilityVerificationRequestRegular_SelectedPageChanged;
        }
        /// <summary>
        /// 폼 Load 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReliaVerifiSpcStatus_Load(object sender, EventArgs e)
        {
            this.tabAnalysis.PageVisible = false;
            SpcClass.SpcDictionaryDataSetting();
            this.ucXBarFrame1.cboChartTypeSetting(1);
            this.ucXBarFrame1.SpcCpkChartEnterEventHandler += UcXBarFrame1_SpcCpkChartEnterEventHandler;
            this.ucXBarFrame1.ucXBarGrid01.ucXBar1.SpcChartDirectMessageUserEventHandler += UcXBar1_SpcChartDirectMessageUserEventHandler;
            this.ucXBarFrame1.ucXBarGrid01.ucXBar2.SpcChartDirectMessageUserEventHandler += UcXBar2_SpcChartDirectMessageUserEventHandler;
            this.ucXBarFrame1.ucXBarGrid01.ucXBar3.SpcChartDirectMessageUserEventHandler += UcXBar3_SpcChartDirectMessageUserEventHandler;
            this.ucXBarFrame1.ucXBarGrid01.ucXBar4.SpcChartDirectMessageUserEventHandler += UcXBar4_SpcChartDirectMessageUserEventHandler;
            this.ucXBarFrame1.SpcChartXBarFrameDirectMessageUserEventHandler += UcXBarFrame1_SpcChartXBarFrameDirectMessageUserEventHandler;
            this.tabMain.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tabMain_SelectedPageChanged);

            this.ucXBarFrame1.SpcVtChartShowWaitAreaEventHandler += UcXBarFrame1_SpcVtChartShowWaitAreaEventHandler;
            this.ucXBarFrame1.ucXBarGrid01.SpcVtChartShowWaitAreaEventHandler += UcXBarGrid01_SpcVtChartShowWaitAreaEventHandler;
            this.ucCpkFrame1.ucCpkGrid01.SpcVtChartShowWaitAreaEventHandler += UcCpkGrid01_SpcVtChartShowWaitAreaEventHandler;

            InitializeLanguageKey();
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

        /// <summary>
        /// Control Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            tabMain.SetLanguageKey(xtraTabPage1, "CONTROLCHART");
            tabMain.SetLanguageKey(xtraTabPage2, "PROCESSCAPABILITY");
            tabMain.SetLanguageKey(xtraTabPage3, "TABROWDATA");
            tabMain.SetLanguageKey(xtraTabPage4, "OVERRULES");
        }

        /// <summary>
        /// XBarFrame Message 이벤트
        /// </summary>
        /// <param name="msg"></param>
        private void UcXBarFrame1_SpcChartXBarFrameDirectMessageUserEventHandler(SpcEventsChartMessage msg)
        {
            ShowMessage(msg.mainMessage);
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
        /// <summary>
        /// 폼 Resize
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReliaVerifiSpcStatus_Resize(object sender, EventArgs e)
        {
            FormResize();
        }

        /// <summary>        
        /// 탭이 변경 되었을때 호출
        /// </summary>
        private void TabReliabilityVerificationRequestRegular_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            SmartTabControl tab = sender as SmartTabControl;

            if (tab.SelectedTabPageIndex == 0) // 의로서 접수 조회
            {
                SearchReliaVerifiResultRegular();
            }
            else if (tab.SelectedTabPageIndex == 1) // 의뢰서 재접수 조회
            {
                FormResize();
                SearchReReliaVerifiResultRegular();
                
            }
        }



        #endregion Event

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
            string processsegmentId = "";

            _isAgainAnalysisExe = true;
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

                _SubGroupType = param["P_SPCAXISTYPE"].ToSafeString();
                processsegmentId = param["P_PROCESSSEGMENTID"].ToSafeString();


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
                InputRawMaxCount = await SqlExecuter.QueryAsync("GetSpcReliaVerifiResultRegularRawMaxCount", "10001", values);
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
                if (nQueryMaxCount <= 0)
                {
                    this.ucXBarFrame1.ClearChartControlsXBAR();
                    this.ucCpkFrame1.ClearChartControlsCpk();
                    // 조회할 데이터가 없습니다.
                    ShowMessage("NoSelectData");
                    return;
                }
                Console.WriteLine(nQueryMaxCount);
                
                //sia작업 : 4/9 ~ 
                //DB Data 부분 수집 여부 Check
                _AnalysisParameter.dtInputRawData = null;
                _AnalysisParameter.dtInputSpecData = null;

                //검사항목 기준정보 조회
                Dictionary<string, object> InspItemValues = new Dictionary<string, object>
                {
                    { "LANGUAGETYPE", UserInfo.Current.LanguageType }
                };


                //검사항목 기준 정보 조회.
                _dtItemIdData = await SqlExecuter.QueryAsync("GetSpcSearchInspitem", "10001", InspItemValues);


                if (nQueryMaxCount < SpcFlag.nSpcQueryMaxCount)
                {
                    //_AnalysisParameter.dtInputRawData = await SqlExecuter.QueryAsync("GetSpcInspectionResultCommonRaw", "10001", values);
                    _dtDbRawData = await SqlExecuter.QueryAsync("GetSpcReliaVerifiResultRegularRaw", "10001", values);
                    
                }
                else
                {
                    await this.InputDataMerge(dateStart, dateEnd, values);

                }

                //입력자료 검사항목별로 편집.
                if (_dtDbRawData != null && _dtDbRawData.Rows.Count > 0)
                {
                    this.InputRawDataEdit();
                }


                //_AnalysisParameter.dtInputRawData = await SqlExecuter.QueryAsync("GetSpcInspectionResultCommonRaw", "10001", values);
                //if (_AnalysisParameter.dtInputRawData != null && _AnalysisParameter.dtInputRawData.Rows.Count < 1)
                //{
                //    ucXBarFrame1.ClearChartControlsXBAR();
                //    ucCpkFrame1.ClearChartControlsCpk();
                //    this.grdOverRules.DataSource = null;
                //    // 조회할 데이터가 없습니다.
                //    ShowMessage("NoSelectData");
                //    return;
                //}

                //grdRawData.DataSource = _AnalysisParameter.dtInputRawData;

                //_AnalysisParameter.dtInputSpecData = await SqlExecuter.QueryAsync("GetSpcInspectionResultCommonSpec", "10001", values);
                //if (_AnalysisParameter.dtInputSpecData != null && _AnalysisParameter.dtInputSpecData.Rows.Count < 1)
                //{
                //    // 조회할 데이터가 없습니다.
                //    ShowMessage("NoSelectData - Spec Data");
                //}

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

                //SourceType
                _AnalysisParameter.dtInputRawMainFieldName.subgroupNameFiledName = "SUBGROUPNAME";

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
                                _AnalysisParameter.dtInputRawMainFieldName.subgroupNameFiledName = "SUBGROUPNAME";
                                break;
                        }

                        break;
                }

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
        /// 검사항목 명 조회.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string GetInspItemName(string id)
        {
            string itemName = "*";

            try
            {
                var data = _dtItemIdData.AsEnumerable();
                var lstRow = data.AsParallel().Where(w => w.Field<string>("INSPITEMID") == id);
                foreach (DataRow row in lstRow)
                {
                    itemName = SpcFunction.IsDbNck(row, "INSPITEMNAME");
                }
                Console.WriteLine(string.Format("검사항목 ID: {0}, 명: {1}",id, itemName));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return itemName;
        }
        /// <summary>
        /// 입력자료 검사항목별 편집
        /// </summary>
        /// <returns></returns>
        private int InputRawDataEdit()
        {
            int result = 1;
            int i;
            string filedNameItemData;
            string filedNameitemDataValue;
            string itemData;
            string itemDataName;
            double? itemDataValue;
            try
            {
                _AnalysisParameter.dtInputRawData = CreateDataTableMeasureitem();
                int nMaxCount = _dtDbRawData.Rows.Count;
                string inputRowSubGroupID = "";
                string inputRowSubGroupName = "";
                for (i = 0; i < nMaxCount; i++)
                {
                    DataRow dbRow = _dtDbRawData.Rows[i];
                    DataRow inputRow = _AnalysisParameter.dtInputRawData.NewRow();

                    //SpcFunction.IsDbNckStringWrite(inputRow, "SUBGROUP", dbRow);
                    //SpcFunction.IsDbNckStringWrite(inputRow, "SUBGROUPNAME", dbRow, "", SpcClass.SubGroupSymbolDb, SpcClass.SubGroupSymbolView);
                    //SpcFunction.IsDbNckStringWrite(inputRow, "SAMPLING", dbRow);
                    //SpcFunction.IsDbNckStringWrite(inputRow, "SAMPLINGNAME", dbRow);

                    SpcFunction.IsDbNckStringWrite(inputRow, "SUBGROUP", dbRow);
                    inputRowSubGroupID = inputRow["SUBGROUP"].ToString();
                    //SpcFunction.IsDbNckStringWrite(inputRow, "Subgroupname", dbRow, "", SpcClass.SubGroupSymbolDb, SpcClass.SubGroupSymbolView);
                    SpcFunction.IsDbNckStringWrite(inputRow, "SUBGROUPNAME", dbRow);
                    inputRowSubGroupName = inputRow["SUBGROUPNAME"].ToString();
                    SpcFunction.IsDbNckStringWrite(inputRow, "SAMPLING", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "SAMPLINGNAME", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "SAMPLINGLOTID", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "SAMPLINGNAMELOTID", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "REQUESTNO", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "ENTERPRISEID", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "PLANTID", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Reliabilitytype", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Requesttype", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Requestdate", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Verificompdate", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Verificompdateview", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Verificompdateviewdate", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Samplereceivedate", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Samplereceivedateview", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Samplereceivedateviewdate", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Transitiondate", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Issamplereceive", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Ispostprocess", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Requestor", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Requestdept", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Requestorjobposition", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Requestextensionno", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Requestmobileno", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Purpose", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Details", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Parentrequestno", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Description", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Validstate", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Lotid", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Outputdate", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Outputdateview", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Outputdateviewdate", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Productdefid", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Productdefversion", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Productdefname", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Processsegmentid", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Processsegmentversion", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Processsegmentname", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Areaid", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Areaname", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Equipmentid", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Equipmentname", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Trackouttime", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Isreceipt", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Isrerequest", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Inspectionresult", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Isncrpublish", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Defectcode", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Defectcodename", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Iscompletion", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Usersequence", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Qcsegmentid", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Qcsegmentname", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Inspitemid", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Inspitemversion", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Inspitemname", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Isoutputdate", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Processdefid", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Processdefversion", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Workcount", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Inspector", dbRow);
                    SpcFunction.IsDbNckStringWrite(inputRow, "Mmode", dbRow);

                    for (int j = 1; j < 11; j++)
                    {
                        //filedNameItemData = string.Format("measureitemid{0}", j.ToString());
                        filedNameItemData = string.Format("measureitemid");
                        filedNameitemDataValue = string.Format("measurevalue{0}", j.ToString());
                        itemData = SpcFunction.IsDbNck(dbRow, filedNameItemData);

                        itemDataValue = SpcFunction.IsDbNckDoubleMax(dbRow, filedNameitemDataValue);
                        if (itemData !=null && itemData !="" && itemDataValue != null && itemDataValue != SpcLimit.MAX)
                        {
                            if (j == 1)
                            {
                                itemDataName = GetInspItemName(itemData);
                                inputRow["ITEMNAME"] = itemDataName;
                                inputRow["SUBGROUP"] = string.Format("{0}{1}", inputRowSubGroupID, itemData);
                                inputRow["SUBGROUPNAME"] = string.Format("{0}{1}", inputRowSubGroupName, itemDataName);
                                inputRow["ITEMID"] = itemData;
                                inputRow["NVALUE"] = itemDataValue;
                                _AnalysisParameter.dtInputRawData.Rows.Add(inputRow);
                            }
                            else
                            {
                                DataRow inputRowSub = _AnalysisParameter.dtInputRawData.NewRow();
                                for (int r = 0; r < 65; r++)
                                {
                                    inputRowSub[r] = inputRow[r];
                                }

                                itemDataName = GetInspItemName(itemData);
                                inputRowSub["ITEMNAME"] = itemDataName;
                                inputRowSub["SUBGROUP"] = string.Format("{0}{1}", inputRowSubGroupID, itemData);
                                inputRowSub["SUBGROUPNAME"] = string.Format("{0}{1}", inputRowSubGroupName, itemDataName);
                                inputRowSub["ITEMID"] = itemData;
                                inputRowSub["NVALUE"] = itemDataValue;

                                _AnalysisParameter.dtInputRawData.Rows.Add(inputRowSub);
                            }
                        }
                    }

                    //Next Sentence...

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 기준정보 - 검사항목 Id 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private DataTable CreateDataTableBasicInspItem(string tableName = "dtBasicInspItem")
        {
            DataTable dt = new DataTable
            {
                TableName = tableName
            };
            //Filed
            dt.Columns.Add("INSPITEMID", typeof(string));
            dt.Columns.Add("INSPITEMNAME", typeof(string));
            return dt;
        }

        /// <summary>
        /// 측적값 편집 임시 Table
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private DataTable CreateDataTableMeasureitem(string tableName = "dtMeasureitem")
        {
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            //Filed
            dt.Columns.Add("SUBGROUP", typeof(string));
            dt.Columns.Add("SUBGROUPNAME", typeof(string));
            dt.Columns.Add("SAMPLING", typeof(string));
            dt.Columns.Add("SAMPLINGNAME", typeof(string));
            dt.Columns.Add("SAMPLINGLOTID", typeof(string));
            dt.Columns.Add("SAMPLINGNAMELOTID", typeof(string));
            dt.Columns.Add("REQUESTNO", typeof(string));
            dt.Columns.Add("ENTERPRISEID", typeof(string));
            dt.Columns.Add("PLANTID", typeof(string));
            dt.Columns.Add("RELIABILITYTYPE", typeof(string));
            dt.Columns.Add("REQUESTTYPE", typeof(string));
            dt.Columns.Add("REQUESTDATE", typeof(string));
            dt.Columns.Add("VERIFICOMPDATE", typeof(string));
            dt.Columns.Add("VERIFICOMPDATEVIEW", typeof(string));
            dt.Columns.Add("VERIFICOMPDATEVIEWDATE", typeof(string));
            dt.Columns.Add("SAMPLERECEIVEDATE", typeof(string));
            dt.Columns.Add("SAMPLERECEIVEDATEVIEW", typeof(string));
            dt.Columns.Add("SAMPLERECEIVEDATEVIEWDATE", typeof(string));
            dt.Columns.Add("TRANSITIONDATE", typeof(string));
            dt.Columns.Add("ISSAMPLERECEIVE", typeof(string));
            dt.Columns.Add("ISPOSTPROCESS", typeof(string));
            dt.Columns.Add("REQUESTOR", typeof(string));
            dt.Columns.Add("REQUESTDEPT", typeof(string));
            dt.Columns.Add("REQUESTORJOBPOSITION", typeof(string));
            dt.Columns.Add("REQUESTEXTENSIONNO", typeof(string));
            dt.Columns.Add("REQUESTMOBILENO", typeof(string));
            dt.Columns.Add("PURPOSE", typeof(string));
            dt.Columns.Add("DETAILS", typeof(string));
            dt.Columns.Add("PARENTREQUESTNO", typeof(string));
            dt.Columns.Add("DESCRIPTION", typeof(string));
            dt.Columns.Add("VALIDSTATE", typeof(string));
            dt.Columns.Add("LOTID", typeof(string));
            dt.Columns.Add("OUTPUTDATE", typeof(string));
            dt.Columns.Add("OUTPUTDATEVIEW", typeof(string));
            dt.Columns.Add("OUTPUTDATEVIEWDATE", typeof(string));
            dt.Columns.Add("PRODUCTDEFID", typeof(string));
            dt.Columns.Add("PRODUCTDEFVERSION", typeof(string));
            dt.Columns.Add("PRODUCTDEFNAME", typeof(string));
            dt.Columns.Add("PROCESSSEGMENTID", typeof(string));
            dt.Columns.Add("PROCESSSEGMENTVERSION", typeof(string));
            dt.Columns.Add("PROCESSSEGMENTNAME", typeof(string));
            dt.Columns.Add("AREAID", typeof(string));
            dt.Columns.Add("AREANAME", typeof(string));
            dt.Columns.Add("EQUIPMENTID", typeof(string));
            dt.Columns.Add("EQUIPMENTNAME", typeof(string));
            dt.Columns.Add("TRACKOUTTIME", typeof(string));
            dt.Columns.Add("ISRECEIPT", typeof(string));
            dt.Columns.Add("ISREREQUEST", typeof(string));
            dt.Columns.Add("INSPECTIONRESULT", typeof(string));
            dt.Columns.Add("ISNCRPUBLISH", typeof(string));
            dt.Columns.Add("DEFECTCODE", typeof(string));
            dt.Columns.Add("DEFECTCODENAME", typeof(string));
            dt.Columns.Add("ISCOMPLETION", typeof(string));
            dt.Columns.Add("USERSEQUENCE", typeof(string));
            dt.Columns.Add("QCSEGMENTID", typeof(string));
            dt.Columns.Add("QCSEGMENTNAME", typeof(string));
            dt.Columns.Add("INSPITEMID", typeof(string));
            dt.Columns.Add("INSPITEMVERSION", typeof(string));
            dt.Columns.Add("INSPITEMNAME", typeof(string));
            dt.Columns.Add("ISOUTPUTDATE", typeof(string));
            dt.Columns.Add("PROCESSDEFID", typeof(string));
            dt.Columns.Add("PROCESSDEFVERSION", typeof(string));
            dt.Columns.Add("WORKCOUNT", typeof(string));
            dt.Columns.Add("INSPECTOR", typeof(string));
            dt.Columns.Add("MMODE", typeof(string));
            dt.Columns.Add("ITEMID", typeof(string));
            dt.Columns.Add("ITEMNAME", typeof(string));
            dt.Columns.Add("NVALUE", typeof(double));
            
            return dt;
        }
        #endregion

        #region 조회조건 설정

        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            //Site 공장
            this.Conditions.AddComboBox("p_plantId", new SqlQuery("GetPlantListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "PLANTNAME", "PLANTID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetDefault(UserInfo.Current.Plant)
                .SetValidationIsRequired()
                .SetLabel("PLANT")
                .SetPosition(2.1); //표시순서 지정
            //Chart Subgroup 구분
            this.Conditions.AddComboBox("P_INSPECTIONRESULTRESOUCETYPE", new SqlQuery("GetSpcSearchComboCom", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"P_CODECLASSID={"SpcReliabilityInspectionType"}"), "CODENAME", "CODEID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetDefault("Regular")
                .SetValidationIsRequired()
                .SetLabel("검사구분")
                .SetPosition(2.2); // Subgroup 조회조건 
            //Chart Subgroup 구분
            this.Conditions.AddComboBox("p_SpcAxisType", new SqlQuery("GetSpcSearchComboCom", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"P_CODECLASSID={"SpcAxisType"}"), "CODENAME", "CODEID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetDefault("DATE")
                .SetValidationIsRequired()
                .SetLabel("조회조건")
                .SetPosition(2.3); // Subgroup 조회조건 
            // 품목
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 3.3, true, Conditions);

            //InitializeConditionEquipment_Popup();//3.1
            //Lot Id
            InitializeConditionPopup_LotId();//3.2
            //표준공정(원인공정)
            InitializeConditionPopup_ReasonSegment();//3.3
            //작업장
            InitializeConditionAreaId_Popup(); //3.4
            //검사항목
            InitializeConditionPopup_InspItem();//3.5
            //설비
            InitializeConditionPopup_Equipment();//3.6

            //InitializeConditionAreaId_Popup();
            //InitializeConditionProcessSegmentId_Popup();
        }

        /// <summary>
        /// 팝업형 조회조건 생성 - 작업장
        /// </summary>
        private void InitializeConditionAreaId_Popup()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("AreaType", "Area");
            param.Add("PlantId", UserInfo.Current.Plant);
            param.Add("LanguageType", UserInfo.Current.LanguageType);

            var areaIdPopup = this.Conditions.AddSelectPopup("p_areaId", new SqlQuery("GetAreaList", "10003", param), "AREANAME", "AREAID")
                .SetPopupLayout(Language.Get("SELECTAREAID"), PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("AREANAME")
                .SetLabel("AREA")
                .SetPosition(3.4);

            areaIdPopup.Conditions.AddTextBox("AREA")
                .SetLabel("TXTAREA");

            areaIdPopup.GridColumns.AddTextBoxColumn("AREAID", 150);
            areaIdPopup.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }

        /// <summary>
        /// 팝업형 조회조건 생성 - 공정
        /// </summary>
        private void InitializeConditionProcessSegmentId_Popup()
        {
            var processSegmentIdPopup = this.Conditions.AddSelectPopup("P_ProcessSegmentId", new SqlQuery("GetProcessSegmentList", "10002", $"PLANTID={UserInfo.Current.Plant}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                .SetPopupLayout(Language.Get("SELECTPROCESSSEGMENTID"), PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
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

        /// <summary>
        /// 팝업형 조회조건 생성 - 설비
        /// </summary>
        private void InitializeConditionEquipment_Popup()
        {
            // 팝업 컬럼설정
            var equipmentPopupColumn = Conditions.AddSelectPopup("p_equipmentId", new SqlQuery("GetEquipmentListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTNAME", "EQUIPMENTID")
               .SetPopupLayout("EQUIPMENT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(400, 600)
               .SetPopupAutoFillColumns("EQUIPMENTNAME")
               .SetLabel("EQUIPMENT")
               .SetPopupResultCount(1)
               .SetPosition(3.1)
               .SetRelationIds("p_plantId");

            // 팝업 조회조건
            equipmentPopupColumn.Conditions.AddTextBox("EQUIPMENTIDNAME")
                .SetLabel("EQUIPMENTIDNAME");

            // 팝업 그리드
            equipmentPopupColumn.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150)
                .SetValidationKeyColumn();
            equipmentPopupColumn.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200);
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
               .SetPosition(3.3);

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
               .SetPosition(3.6)
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
               .SetPosition(3.2)
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
            DataTable dtInputFirstData;
            dtInputFirstData = new DataTable();
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
                    dtInputPartData = await SqlExecuter.QueryAsync("GetSpcReliaVerifiResultRegularRaw", "10001", param);
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

                    dtInputFirstData = await SqlExecuter.QueryAsync("GetSpcReliaVerifiResultRegularRaw", "10001", param);
                    if (dtInputFirstData != null && dtInputFirstData.Rows.Count < 1)
                    {
                        niCount = 0;
                    }
                }
            }
            //17010

            this._dtDbRawData = dtInputFirstData;

            //if (dtInputFirstData != null && dtInputFirstData.Rows.Count > 0)
            //{
            //    _AnalysisParameter.dtInputRawData = dtInputFirstData;
            //}

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

        /// <summary>
        /// OverRules Check
        /// </summary>
        /// <param name="dataRowOverRule"></param>
        /// <returns></returns>
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

        #region Private Function
        #region 3. 통계 Chart 관련 이벤트
        //this.tabMain.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tabMain_SelectedPageChanged);
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
        /// 통계 분석 실행 - 공정능력 실행, 관리도 (Tab 이벤트에서 실행함)
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

        /// <summary>
        /// 의뢰 조회
        /// </summary>
        private void SearchReliaVerifiResultRegular()
        {
            try
            {
                this.ShowWaitArea();

                var values = Conditions.GetValues();
                values.Add("P_ISSECOND", "R"); // 의뢰
                values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
                DataTable dt = SqlExecuter.Query("GetReliaVerifiResultRegularlist", "10001", values);

                if (dt.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData");
                }

                grdRawData.DataSource = dt;
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
            }
        }

        /// <summary>
        /// 재의뢰 조회
        /// </summary>
        private void SearchReReliaVerifiResultRegular()
        {
            try
            {
                this.ShowWaitArea();
                var values = Conditions.GetValues();
                values.Add("P_ISSECOND", "S"); // 재의뢰
                values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
                DataTable dt = SqlExecuter.Query("GetReliaVerifiResultRegularlist", "10001", values);

                if (dt.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData");
                }

                //grdReReliaVerifiResultRegular.DataSource = dt;
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
            }
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

        #region Global Function

        /// <summary>
        /// Popup 닫혔을때 재검색하기 위한 함수
        /// </summary>
        public void Search()
        {
            OnSearchAsync();
        }



        #endregion

        
    }
}
