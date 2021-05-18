#region using

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
    /// 프 로 그 램 명  : 품질 > 레칭레이트 > 에칭레이트 측정 값 등록
    /// 업  무  설  명  : 에칭레이트 측정 값 및 이상발생 관리한다.
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-06-24
    /// 수  정  이  력  : 2019-07-17 강유라
    /// 
    /// 
    /// </summary>
    public partial class EtchingRateSpcStatus : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        DataRow _addMeasure = null;
        DataRow _addReMeasure = null;
        private DataTable _grdMeasureTable = null;

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

        public EtchingRateSpcStatus()
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
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region grdRawData 초기화

            grdRawData.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdRawData.View.SetIsReadOnly();
            grdRawData.View.SetSortOrder("MEASUREDATE", DevExpress.Data.ColumnSortOrder.Descending);
            //grdMeasure.View.SetSortOrder("MEASUREDEGREE", DevExpress.Data.ColumnSortOrder.Descending);

            grdRawData.View.AddTextBoxColumn("MEASUREDATE", 150);

            grdRawData.View.AddTextBoxColumn("MEASUREDEGREE", 150);

            grdRawData.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 150)
                .SetLabel("TOPPROCESSSEGMENTCLASS");

            grdRawData.View.AddComboBoxColumn("AREAID", 150, new SqlQuery("GetAreaListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"P_PLANTID={UserInfo.Current.Plant}"), "AREANAME", "AREAID")
                .SetLabel("AREA");

            grdRawData.View.AddTextBoxColumn("EQUIPMENTNAME", 150)
                .SetLabel("EQUIPMENT");

            grdRawData.View.AddTextBoxColumn("CHILDEQUIPMENTNAME", 150)
                .SetLabel("CHILDEQUIPMENT");

            grdRawData.View.AddTextBoxColumn("WORKTYPE", 150)
                .SetLabel("TYPECONDITION");

            grdRawData.View.AddTextBoxColumn("SPECRANGE", 150);

            grdRawData.View.AddTextBoxColumn("CONTROLRANGE", 150);

            grdRawData.View.AddTextBoxColumn("ETCHRATEVALUE", 150);

            grdRawData.View.AddTextBoxColumn("ETCHRATEBEFOREVALUE", 150)
                .SetIsHidden();

            grdRawData.View.AddTextBoxColumn("ETCHRATEAFTERVALUE", 150)
                .SetIsHidden();

            grdRawData.View.AddTextBoxColumn("EQPTSPEED", 150);

            grdRawData.View.AddTextBoxColumn("MEASURER", 150);

            grdRawData.View.AddComboBoxColumn("RESULT", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OKNG", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdRawData.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();

            grdRawData.View.AddTextBoxColumn("PLANTID", 150)
                .SetIsHidden();

            grdRawData.View.AddTextBoxColumn("SPECSEQUENCE", 150)
                .SetIsHidden();

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

            grdOverRules.View.AddTextBoxColumn("PLANTID", 120);
            grdOverRules.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 120);
            grdOverRules.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 120);
            grdOverRules.View.AddTextBoxColumn("PROCESSSEGMENTID", 120);
            grdOverRules.View.AddTextBoxColumn("AREAID", 120);
            grdOverRules.View.AddTextBoxColumn("EQUIPMENTID", 120);
            grdOverRules.View.AddTextBoxColumn("EQUIPMENTNAME", 120);
            grdOverRules.View.AddTextBoxColumn("CHILDEQUIPMENTID", 120);
            grdOverRules.View.AddTextBoxColumn("CHILDEQUIPMENTNAME", 120);
            grdOverRules.View.AddTextBoxColumn("PRODUCTDEFID", 120);
            grdOverRules.View.AddTextBoxColumn("INSPITEMID", 120);
            grdOverRules.View.AddTextBoxColumn("WORKTYPE", 120);

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
            //화면 로드시 스펙def 조회 이벤트
            //Load += (s, e) =>
            //{
            //    SearchSpecDef();
            //};

            #region 첫번째 탭 이벤트
            ////grdMeasure 그리드에 새Row가 추가 될 때 이벤트
            //grdRawData.View.AddingNewRow += (s, e) =>
            //{
            //    BindingMeasure(e.NewRow);
            //};
            #endregion


        }
        /// <summary>
        /// 폼 Load 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EtchingRateSpcStatus_Load(object sender, EventArgs e)
        {
            this.tabAnalysis.PageVisible = false;
            SpcClass.SpcDictionaryDataSetting();
            this.ucXBarFrame1.cboChartTypeSetting(3);
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
            //this.ucXBarFrame1.SetChartTypeCboSelectIndex(3);
            //this.ucXBarFrame1.cboLeftChartType.ItemIndex
            SpcClass.SpcDictionaryDataSetting();

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
        private void EtchingRateSpcStatus_Resize(object sender, EventArgs e)
        {
            this.FormResize();
        }

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

        #region 첫번째 탭 이벤트
        #endregion

        #region 두번째 탭 이벤트
        #endregion

        #endregion

        #region 툴바
        #endregion

        #region 검색

        /// <summary>
        /// Main자료조회 - 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            try
            {
                await base.OnSearchAsync();

                // TODO : 조회 SP 변경
                var values = Conditions.GetValues();
                values.Add("P_LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);

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
                InputRawMaxCount = await SqlExecuter.QueryAsync("GetSpcEtchingRateRawMaxCount", "10001", values);
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
                    _AnalysisParameter.dtInputRawData = await SqlExecuter.QueryAsync("GetSpcEtchingRateRaw", "10001", values);
                }
                else
                {
                    await this.InputDataMerge(dateStart, dateEnd, values);
                }

                #region SPEC 조회 - Linq형
                //Spec 입력자료 구성.
                if (_AnalysisParameter.dtInputRawData != null && _AnalysisParameter.dtInputRawData.Rows.Count < 1)
                {
                    this.ucXBarFrame1.ClearChartControlsXBAR();
                    this.ucCpkFrame1.ClearChartControlsCpk();
                    // 조회할 데이터가 없습니다.
                    ShowMessage("NoSelectData");
                    return;
                }

             try
                {
                    //A.LSL
                    //A.SL
                    //A.USL
                    //A.LCL
                    //A.CL
                    //A.UCL
                    //A.LOL
                    //A.UOL
                    var dtSpecData = _AnalysisParameter.dtInputRawData.AsEnumerable()
                                                           .GroupBy(m => new { SUBGROUP = m.Field<string>("SUBGROUP") })
                                                           .Select(grp => new
                                                           {
                                                               sSUBGROUP = grp.Key.SUBGROUP.ToSafeString(),
                                                               sCTYPE = grp.Max(x => x.Field<object>("CONTROLTYPE").ToSafeString()),
                                                               nUSL = grp.Max(x => x.Field<object>("USL").ToSafeDoubleStaMax()),
                                                               nTSL = grp.Max(x => x.Field<object>("SL").ToSafeDoubleStaMax()),
                                                               nLSL = grp.Min(x => x.Field<object>("LSL").ToSafeDoubleStaMin()),
                                                               nUCL = grp.Max(x => x.Field<object>("UCL").ToSafeDoubleStaMax()),
                                                               nTCL = grp.Max(x => x.Field<object>("CL").ToSafeDoubleStaMax()),
                                                               nLCL = grp.Min(x => x.Field<object>("LCL").ToSafeDoubleStaMin()),
                                                               nUOL = grp.Max(x => x.Field<object>("UOL").ToSafeDoubleStaMax()),
                                                               nLOL = grp.Min(x => x.Field<object>("LOL").ToSafeDoubleStaMin())

                                                           });
                    foreach (var s in dtSpecData)
                    {
                        DataRow dataRawSpec = tempInputSpec.NewRow();

                        SpcFunction.IsDbNckStringWrite(dataRawSpec, "SUBGROUP", s.sSUBGROUP);
                        SpcFunction.IsDbNckStringWrite(dataRawSpec, "CTYPE", s.sCTYPE);

                        SpcFunction.IsDbNckDoubleWrite(dataRawSpec, "USL", s.nUSL);
                        SpcFunction.IsDbNckDoubleWrite(dataRawSpec, "SL", s.nTSL);
                        SpcFunction.IsDbNckDoubleWrite(dataRawSpec, "LSL", s.nLSL);

                        SpcFunction.IsDbNckDoubleWrite(dataRawSpec, "UCL", s.nUCL);
                        SpcFunction.IsDbNckDoubleWrite(dataRawSpec, "CL", s.nTCL);
                        SpcFunction.IsDbNckDoubleWrite(dataRawSpec, "LCL", s.nLCL);

                        SpcFunction.IsDbNckDoubleWrite(dataRawSpec, "UOL", s.nUOL);
                        SpcFunction.IsDbNckDoubleWrite(dataRawSpec, "LOL", s.nLOL);
                        tempInputSpec.Rows.Add(dataRawSpec);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine(tempInputSpec.Rows.Count);
                _AnalysisParameter.dtInputSpecData = tempInputSpec;

                #endregion SPEC 조회 - Linq형

                //Chart 구분
                string chartType = ucXBarFrame1.cboLeftChartType.GetDataValue().ToSafeString().ToUpper().Replace("_", "");
                _AnalysisParameter.spcOption.specDefaultChartType = "I";
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

                string subgroupType = "DATE";
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
                                break;
                        }

                        break;
                }

                this.ChartAnalysisExecution();

                grdRawData.DataSource = _AnalysisParameter.dtInputRawData;
                grdRawData.ShowStatusBar = true;
                #endregion

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

            // TODO : 조회조건 추가 구성이 필요한 경우 사용

            InitializeConditionPopup_ProcessSegment();
            InitializeConditionPopup_Equipment();
            InitializeConditionPopup_ChildEquipment();
            InitializeConditionPopup_Area();
        }

        #region 조회조건 팝업 초기화

        /// <summary>
        /// 공정조회 조건 팝업
        /// </summary>
        private void InitializeConditionPopup_ProcessSegment()
        {
            var ProcessSegmentClassId = Conditions.AddSelectPopup("P_PROCESSSEGMENTCLASSID", new SqlQuery("GetProcessSegmentClass", "10001", "PROCESSSEGMENTCLASSTYPE=TopProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                                               .SetPopupLayout("TOPPROCESSSEGMENTCLASS", PopupButtonStyles.Ok_Cancel, true, true)
                                               .SetPopupAutoFillColumns("PROCESSSEGMENTCLASSNAME")
                                               .SetPopupResultCount(1)
                                               .SetPosition(3)
                                               .SetLabel("TOPPROCESSSEGMENTCLASS")
                                               .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow);


            ProcessSegmentClassId.Conditions.AddTextBox("PROCESSSEGMENTCLASS")
                .SetLabel("LARGEPROCESSSEGMENTIDNAME");


            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 150)
                .SetLabel("TOPPROCESSSEGMENTCLASSID");
            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200)
                .SetLabel("TOPPROCESSSEGMENTCLASSNAME");
        }

        /// <summary>
        /// 작업장 조회조건
        /// </summary>
        private void InitializeConditionPopup_Area()
        {
            // 팝업 컬럼설정
            var areaPopup = Conditions.AddSelectPopup("P_AREAID", new SqlQuery("GetAreaListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID")
               .SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(400, 600)
               .SetLabel("AREA")
               .SetPopupResultCount(1)
               .SetPosition(3.5)
               .SetRelationIds("P_PLANTID")
               .SetDefault(Framework.UserInfo.Current.Area, Framework.UserInfo.Current.Area);

            // 팝업 조회조건
            areaPopup.Conditions.AddTextBox("AREAIDNAME")
                .SetLabel("AREAIDNAME");

            // 팝업 그리드
            areaPopup.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetValidationKeyColumn();
            areaPopup.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }

        /// <summary>
        /// 설비 조회조건 팝업
        /// </summary>
        private void InitializeConditionPopup_Equipment()
        {
            var equipmentId = Conditions.AddSelectPopup("P_EQUIPMENTID", new SqlQuery("GetEquipmentByClassHierarchy", "10001", "DETAILEQUIPMENTTYPE=Main", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTNAME", "EQUIPMENTID")
                         .SetPopupLayout("EQUIPMENT", PopupButtonStyles.Ok_Cancel, true, false)
                         .SetPopupResultCount(1)
                         .SetPosition(4)
                         .SetLabel("EQUIPMENT")
                         .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                         .SetPopupAutoFillColumns("EQUIPMENTNAME");

            equipmentId.Conditions.AddComboBox("PARENTEQUIPMENTCLASSID", new SqlQuery("GetEquipmentClassHierarchyAndType", "10001", "HIERARCHY=TopEquipment", "EQUIPMENTCLASSTYPE=Production", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTCLASSNAME", "EQUIPMENTCLASSID")
                                  .SetValidationKeyColumn()
                                  .SetLabel("TOPEQUIPMENTCLASS");

            equipmentId.Conditions.AddComboBox("MIDDLEEQUIPMENTCLASSID", new SqlQuery("GetEquipmentClassHierarchyAndType", "10001", "HIERARCHY=MiddleEquipment", "EQUIPMENTCLASSTYPE=Production", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTCLASSNAME", "EQUIPMENTCLASSID")
                                  .SetValidationKeyColumn()
                                  .SetRelationIds("PARENTEQUIPMENTCLASSID")
                                  .SetLabel("MIDDLEEQUIPMENTCLASS");

            equipmentId.Conditions.AddTextBox("EQUIPMENT")
                .SetLabel("EQUIPMENTIDNAME");

            // 팝업 그리드
            equipmentId.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSNAME", 150);
            equipmentId.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150);
            equipmentId.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200);

        }

        /// <summary>
        /// 설비단 조회조건 팝업
        /// </summary>
        private void InitializeConditionPopup_ChildEquipment()
        {
            var childEquipementId = Conditions.AddSelectPopup("P_CHILDEQUIPMENTID", new SqlQuery("GetEquipmentListByDetailType", "10001", "EQUIPMENTCLASSTYPE=Production", "DETAILEQUIPMENTTYPE=Sub", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTNAME", "EQUIPMENTID")
                                               .SetPopupLayout("CHILDEQUIPMENT", PopupButtonStyles.Ok_Cancel, true, false)
                                               .SetPopupResultCount(1)
                                               .SetPosition(5)
                                               .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                                               .SetPopupAutoFillColumns("EQUIPMENTNAME")
                                               .SetLabel("CHILDEQUIPMENT")
                                               .SetRelationIds("P_EQUIPMENTID");

            childEquipementId.Conditions.AddTextBox("EQUIPMENT")
                .SetLabel("CHILDEQUIPMENTIDNAME");

            // 팝업 그리드
            childEquipementId.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150)
                .SetLabel("CHILDEQUIPMENTID");
            childEquipementId.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200)
                .SetLabel("CHILDEQUIPMENTNAME");
        }

        #endregion

        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
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
                    dtInputPartData = await SqlExecuter.QueryAsync("GetSpcEtchingRateRaw", "10001", param);
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

                    dtInputFirstData = await SqlExecuter.QueryAsync("GetSpcEtchingRateRaw", "10001", param);
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
            dt.Columns.Add("EQUIPMENTID", typeof(string));
            dt.Columns.Add("EQUIPMENTNAME", typeof(string));
            dt.Columns.Add("CHILDEQUIPMENTID", typeof(string));
            dt.Columns.Add("CHILDEQUIPMENTNAME", typeof(string));
            dt.Columns.Add("PROCESSSEGMENTCLASSID", typeof(string));
            dt.Columns.Add("PROCESSSEGMENTCLASSNAME", typeof(string));
            dt.Columns.Add("INSPITEMID", typeof(string));
            dt.Columns.Add("CHEMICALNAME", typeof(string));
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

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            DataTable changed = null;
         
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
                //throw Framework.MessageException.Create(ex.ToString());
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
                                                                   sPLANTID = grp.Where(x => x.Field<object>("PLANTID") != null).Max(x => x.Field<object>("PLANTID").ToSafeString()),
                                                                   sPROCESSSEGMENTCLASSID = grp.Where(x => x.Field<object>("PROCESSSEGMENTCLASSID") != null).Max(x => x.Field<object>("PROCESSSEGMENTCLASSID").ToSafeString()),
                                                                   sPROCESSSEGMENTCLASSNAME = grp.Where(x => x.Field<object>("PROCESSSEGMENTCLASSNAME") != null).Max(x => x.Field<object>("PROCESSSEGMENTCLASSNAME").ToSafeString()),
                                                                   //sPROCESSSEGMENTID = grp.Where(x => x.Field<object>("PROCESSSEGMENTID") != null).Max(x => x.Field<object>("PROCESSSEGMENTID").ToSafeString()),
                                                                   sAREAID = grp.Where(x => x.Field<object>("AREAID") != null).Max(x => x.Field<object>("AREAID").ToSafeString()),
                                                                   sEQUIPMENTID = grp.Where(x => x.Field<object>("EQUIPMENTID") != null).Max(x => x.Field<object>("EQUIPMENTID").ToSafeString()),
                                                                   sEQUIPMENTNAME = grp.Where(x => x.Field<object>("EQUIPMENTNAME") != null).Max(x => x.Field<object>("EQUIPMENTNAME").ToSafeString()),
                                                                   sCHILDEQUIPMENTID = grp.Where(x => x.Field<object>("CHILDEQUIPMENTID") != null).Max(x => x.Field<object>("CHILDEQUIPMENTID").ToSafeString()),
                                                                   sCHILDEQUIPMENTNAME = grp.Where(x => x.Field<object>("CHILDEQUIPMENTNAME") != null).Max(x => x.Field<object>("CHILDEQUIPMENTNAME").ToSafeString())
                                                                   //sPRODUCTDEFID = grp.Where(x => x.Field<object>("PRODUCTDEFID") != null).Max(x => x.Field<object>("PRODUCTDEFID").ToSafeString()),
                                                                   //sINSPITEMID = grp.Where(x => x.Field<object>("INSPITEMID") != null).Max(x => x.Field<object>("INSPITEMID").ToSafeString()),
                                                                   //sWORKTYPE = grp.Where(x => x.Field<object>("WORKTYPE") != null).Max(x => x.Field<object>("WORKTYPE").ToSafeString())
                                                               });
                        foreach (var s in dtGroupData)
                        {
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "PLANTID", s.sPLANTID.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "PROCESSSEGMENTCLASSID", s.sPROCESSSEGMENTCLASSID.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "PROCESSSEGMENTCLASSNAME", s.sPROCESSSEGMENTCLASSNAME.ToSafeString());
                            //SpcFunction.IsDbNckStringWrite(dataRowOverRule, "PROCESSSEGMENTID", s.sPROCESSSEGMENTID.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "AREAID", s.sAREAID.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "EQUIPMENTID", s.sEQUIPMENTID.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "EQUIPMENTNAME", s.sEQUIPMENTNAME.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "CHILDEQUIPMENTID", s.sCHILDEQUIPMENTID.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "CHILDEQUIPMENTNAME", s.sCHILDEQUIPMENTNAME.ToSafeString());
                            //SpcFunction.IsDbNckStringWrite(dataRowOverRule, "PRODUCTDEFID", s.sPRODUCTDEFID.ToSafeString());
                            //SpcFunction.IsDbNckStringWrite(dataRowOverRule, "INSPITEMID", s.sINSPITEMID.ToSafeString());
                            //SpcFunction.IsDbNckStringWrite(dataRowOverRule, "WORKTYPE", s.sWORKTYPE.ToSafeString());
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

        //----------------------------------------------------------------------------------------
        /// <summary>
        /// specDef를 조회하는 함수
        /// </summary>
        private void SearchSpecDef()
        {
            //Dictionary<string, object> values = new Dictionary<string, object>();
            //values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);

            //grdRawData.DataSource = SqlExecuter.Query("SelectEtchingRateSpecEnable", "10001", values);
        }

        /// <summary>
        /// 저장 할 데이터가 있는지 확인하는 함수
        /// </summary>
        /// <param name="table"></param>
        private void CheckChange(DataTable table)
        {
            if (table.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        /// <summary>
        /// 조회한 데이터가 있는지 확인하는 함수
        /// </summary>
        /// <param name="table"></param>
        private void CheckData(DataTable table)
        {
            if (table.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
        }

        /// <summary>
        /// 첫번째 탭의 팝업결과를 바인딩 해주는 함수
        /// </summary>
        /// <param name="newRow"></param>
        private void BindingMeasure(DataRow newRow)
        {
            newRow["SPECSEQUENCE"] = _addMeasure["SPECSEQUENCE"];
            newRow["MEASUREDATE"] = _addMeasure["MEASUREDATE"];
            newRow["PROCESSSEGMENTCLASSNAME"] = _addMeasure["PROCESSSEGMENTCLASSNAME"];
            newRow["EQUIPMENTNAME"] = _addMeasure["EQUIPMENTNAME"];
            newRow["CHILDEQUIPMENTNAME"] = _addMeasure["CHILDEQUIPMENTNAME"];
            newRow["WORKTYPE"] = _addMeasure["WORKTYPE"];
            newRow["SPECRANGE"] = _addMeasure["SPECRANGE"];
            newRow["CONTROLRANGE"] = _addMeasure["CONTROLRANGE"];
            newRow["ETCHRATEVALUE"] = _addMeasure["ETCHRATEVALUE"];
            newRow["ETCHRATEBEFOREVALUE"] = _addMeasure["ETCHRATEBEFOREVALUE"];
            newRow["ETCHRATEAFTERVALUE"] = _addMeasure["ETCHRATEAFTERVALUE"];
            newRow["EQPTSPEED"] = _addMeasure["EQPTSPEED"];
            newRow["MEASURER"] = _addMeasure["MEASURER"];
            newRow["RESULT"] = _addMeasure["RESULT"];
            newRow["ENTERPRISEID"] = _addMeasure["ENTERPRISEID"];
            newRow["PLANTID"] = _addMeasure["PLANTID"];
            newRow["AREAID"] = _addMeasure["AREAID"];
        }

        /// <summary>
        /// grdAbnormal를 조회하는 함수
        /// </summary>
        /// <param name="flag"></param>
        private DataTable SearchAbnormal()
        {
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);

            DataTable dt = SqlExecuter.Query("SelectEtchingRateAbnormal", "10001", values);

            return dt;
        }

        /// <summary>
        /// grdReMeasure 그리드를 조회하는 함수
        /// </summary>
        private void SeartchReMeasure()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            //values.Add("SPECSEQUENCE", row["SPECSEQUENCE"]);
            //values.Add("MEASUREDATE", row["MEASUREDATE"]);
            //values.Add("MEASUREDEGREE", row["MEASUREDEGREE"]);
            //values.Add("ENTERPRISEID", row["ENTERPRISEID"]);
            //values.Add("PLANTID", row["PLANTID"]);
            //values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);

            DataTable dt = SqlExecuter.Query("SelectEtchingRateReMeasure", "10001", values);
            grdRawData.DataSource = dt;
        }



        /// <summary>
        /// 두번째 탭의 팝업결과를 grdReMeasure그리드에(하단) 바인딩 해주는 함수
        /// </summary>
        /// <param name="row"></param>
        private void BindingReMeasure(DataRow newRow)
        {
            newRow["SPECSEQUENCE"] = _addReMeasure["SPECSEQUENCE"];
            newRow["MEASUREDATE"] = _addReMeasure["MEASUREDATE"];
            newRow["MEASUREDEGREE"] = _addReMeasure["MEASUREDEGREE"];
            newRow["PROCESSSEGMENTCLASSNAME"] = _addReMeasure["PROCESSSEGMENTCLASSNAME"];
            newRow["EQUIPMENTNAME"] = _addReMeasure["EQUIPMENTNAME"];
            newRow["CHILDEQUIPMENTNAME"] = _addReMeasure["CHILDEQUIPMENTNAME"];
            newRow["WORKTYPE"] = _addReMeasure["WORKTYPE"];
            newRow["SPECRANGE"] = _addReMeasure["SPECRANGE"];
            newRow["CONTROLRANGE"] = _addReMeasure["CONTROLRANGE"];
            newRow["REMEASUREDATE"] = _addReMeasure["REMEASUREDATE"];
            newRow["REETCHRATEVALUE"] = _addReMeasure["REETCHRATEVALUE"];
            newRow["REETCHRATEBEFOREVALUE"] = _addReMeasure["REETCHRATEBEFOREVALUE"];
            newRow["REETCHRATEAFTERVALUE"] = _addReMeasure["REETCHRATEAFTERVALUE"];
            newRow["REMEASURER"] = _addReMeasure["REMEASURER"];
            newRow["RERESULT"] = _addReMeasure["RERESULT"];
            newRow["ACTIONRESULT"] = _addReMeasure["ACTIONRESULT"];
            newRow["ENTERPRISEID"] = _addReMeasure["ENTERPRISEID"];
            newRow["PLANTID"] = _addReMeasure["PLANTID"];
            //파일
            newRow["FILEID"] = _addReMeasure["FILEID"];
            newRow["FILENAME"] = _addReMeasure["FILENAME"];
            newRow["FILESIZE"] = _addReMeasure["FILESIZE"];
            newRow["FILEEXT"] = _addReMeasure["FILEEXT"];
            newRow["FILEPATH"] = _addReMeasure["FILEPATH"];
            newRow["SAFEFILENAME"] = _addReMeasure["SAFEFILENAME"];
            newRow["LOCALFILEPATH"] = _addReMeasure["LOCALFILEPATH"];
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
