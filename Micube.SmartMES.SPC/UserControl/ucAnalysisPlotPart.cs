#region using

using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.SPCLibrary;
using static Micube.SmartMES.Commons.SPCLibrary.DataSets.SpcDataSet;

using DevExpress.XtraCharts;
using DevExpress.XtraLayout.Utils;

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Linq;

#endregion

namespace Micube.SmartMES.SPC.UserControl
{
    /// <summary>
    /// 프 로 그 램 명  : SPC User Control XBar Chart
    /// 업  무  설  명  : SPC 통계에서 사용되는 XBar-R & S Chart
    /// 생    성    자  : 이승우
    /// 생    성    일  : 2019-07-16
    /// 수  정  이  력  : 
    /// 2019-07-16  Chart Data 초기화 추가
    /// 2019-07-11  최초작성
    /// 
    /// </summary>
    /// 
    public partial class ucAnalysisPlotPart : DevExpress.XtraEditors.XtraUserControl
    {
        #region Local Variables

        public delegate void SpcChartEnterEventHandler(object sender, EventArgs e, SpcEventsOption se);
        public virtual event SpcChartEnterEventHandler SpcCpkChartEnterEventHandler;
        public SpcEventsOption spcEop = new SpcEventsOption();
        //public bool IsTestMode = false;
        public bool IsDetailPopupLock = false;

        private RtnControlDataTable _dtControlData = new RtnControlDataTable();
        private ControlSpec _controlSpec = new ControlSpec();
        /// <summary>
        /// Subgroup ID 입력
        /// </summary>
        private string _SUBGROUP = "";

        /// <summary>
        /// Subgroup Name 입력
        /// </summary>
        private string _SUBGROUPNAME = "";

        private SPCPara _spcPara = new SPCPara();
        private bool _IsOverRule = false;
        /// <summary>
        /// Paint 이벤트 사용 유무
        /// </summary>
        private bool _IsPaint = false;
        /// <summary>
        /// 해석용 관리선 표시 유무
        /// </summary>
        private IsInterpretation _isInterpretation = IsInterpretation.Create();
        private int _x = 0;
        private int _y = 0;
        public RtnControlDataTable rtnDirectChartData = new RtnControlDataTable();
        private SPCTestData _spcTestData = new SPCTestData();
        private ChartWholeValue _wholeRangeXBar = ChartWholeValue.Create();
        private ChartWholeValue _wholeRangeRS = ChartWholeValue.Create();
        private IsSpecCancel _isCancelXBar = IsSpecCancel.Create();
        private IsSpecCancel _isCancelXR = IsSpecCancel.Create();
        /// <summary>
        /// 직접입력 WholeRange 설정. 8/21
        /// </summary>
        private ChartWholeRangeDirectValue _originalWholeRange = ChartWholeRangeDirectValue.Create();
        private bool _isControlEventsStop = false;

        /// <summary>
        /// 콤보박스 Chart Type DataTable
        /// </summary>
        private DataTable _CboChartTypeDataTable = null;
        /// <summary>
        /// 콤복박스 Chart Subgroup DataTable
        /// </summary>
        private DataTable _CboChartSubgroupDataTable = null;
        #endregion

        #region 생성자

        /// <summary>
        /// User Control XBar Chart 생성자
        /// </summary>
        public ucAnalysisPlotPart()
        {

            InitializeComponent();

            ChartMessage.MessageSetting();
            ControlChartDataMappingClear();

            //if (IsTestMode == true)
            //{
            //    ControlChartDataMappingTest();
            //    _IsPaint = true;
            //}
            //else
            //{
            //    //ControlChartDataMappingClear();
            //}


            SpcViewOption.AttDefaultSetting();

            InitializeContent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        private void InitializeContent()
        {
            InitializeEvent();
        }

        /// <summary>
        /// 콤보박스 설정 - Chart Type 
        /// </summary>
        public void CoboBoxSettingChartType()
        {
            _CboChartTypeDataTable = CreateDataTableAnalysisChartType();
            this.cboChartType.DataSource = _CboChartTypeDataTable;
            this.cboChartType.DisplayMember = "Label";
            this.cboChartType.ValueMember = "nValue";
            this.cboChartType.Columns[1].Visible = false;
            this.cboChartType.ShowHeader = false;
        }
        /// <summary>
        /// 콤보박스 설정 - Subgroup ID
        /// </summary>
        public void CoboBoxSettingSubGroupId()
        {
            this.cboOrderMode.DataSource = null;
            _CboChartSubgroupDataTable = CreateDataTableAnalysisSubgroupID();
            if (_CboChartSubgroupDataTable != null && _CboChartSubgroupDataTable.Rows.Count > 0)
            {
                this.cboOrderMode.DataSource = _CboChartSubgroupDataTable;
                this.cboOrderMode.DisplayMember = "Label";
                this.cboOrderMode.ValueMember = "nValue";
                this.cboOrderMode.Columns[1].Visible = false;
                this.cboOrderMode.ShowHeader = false;
                this.cboOrderMode.ItemIndex = 0;
                this.cboChartType.ItemIndex = 0;
            }
        }
        /// <summary>
        /// 콤보박스 입력 iTem 생성 함수. (Chart Type)
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable CreateDataTableAnalysisChartType(string tableName = "dtCboChartType")
        {
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            //Filed
            dt.Columns.Add("Label", typeof(string));
            dt.Columns.Add("nValue", typeof(string));
            
            DataRow row;
            row = dt.NewRow(); row["Label"] = ""; row["nValue"] = AnalysisChartType.None.ToString(); dt.Rows.Add(row);
            row = dt.NewRow(); row["Label"] = "비교분석 Polt"; row["nValue"] = AnalysisChartType.AnalysisPolt.ToString(); dt.Rows.Add(row);
            row = dt.NewRow(); row["Label"] = "비교분석 Line"; row["nValue"] = AnalysisChartType.AnalysisLine.ToString(); dt.Rows.Add(row);
            row = dt.NewRow(); row["Label"] = "Box Plot"; row["nValue"] = AnalysisChartType.BoxPlot.ToString(); dt.Rows.Add(row);
            row = dt.NewRow(); row["Label"] = "산점도"; row["nValue"] = AnalysisChartType.ThreePointDiagram.ToString(); dt.Rows.Add(row);
            row = dt.NewRow(); row["Label"] = "시계열도"; row["nValue"] = AnalysisChartType.TimeSeries.ToString(); dt.Rows.Add(row);

            return dt;
        }

        public DataTable CreateDataTableAnalysisSubgroupID(string tableName = "dtCboSubgroupID")
        {
            DataRow row;
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            //Filed
            dt.Columns.Add("Label", typeof(string));
            dt.Columns.Add("nValue", typeof(string));

            try
            {
                row = dt.NewRow();
                row["Label"] = "";
                row["nValue"] = "None";
                dt.Rows.Add(row);

                if (_spcPara != null && _spcPara.InputData != null)
                {
                    if (_spcPara.InputData.Rows.Count > 0)
                    {
                        var rowSrc = _spcPara.InputData.AsEnumerable();
                        var cs = rowSrc
                            .GroupBy(g => new
                            {
                                g.SUBGROUP
                            })
                            .Select(s => new
                            {
                                sSUBGROUP = s.Key.SUBGROUP,
                                sSUBGROUPNAME = s.Max(ss => ss.SUBGROUPNAME),
                            });
                        foreach (var item in cs)
                        {
                            row = dt.NewRow();
                            row["Label"] = item.sSUBGROUPNAME.Replace("@#", @"/ ");
                            row["nValue"] = item.sSUBGROUP;
                            dt.Rows.Add(row);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return dt;
        }
        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            //ChrXbarR.Click += (s, e) =>
            //{
            //    _IsPaint = false;
            //    ChartHitInfo hitInfo = ((s as SmartChart).CalcHitInfo((e as MouseEventArgs).Location));
            //    SeriesPoint sp = hitInfo.SeriesPoint;
            //    _IsPaint = true;
            //};

            //Chart Paint 이벤트 - 사용자 Line 적용.
            //this.ChrPoltA.Paint += (s, e) =>
            //{
            //    if (_IsPaint == false)
            //    {
            //        //ControlLinePaint(s, e);
            //    }
            //    else
            //    {
            //        _IsPaint = false;
            //        //ChartHitInfo hitInfo = ((sender as SmartChart).CalcHitInfo((e as MouseEventArgs).Location));
            //        //SeriesPoint sp = hitInfo.SeriesPoint;

            //        // Obtain hit information under the test point. 
            //        ChartHitInfo hi = ChrPoltA.CalcHitInfo(_x, _y);

            //        // Obtain the series point under the test point. 
            //        SeriesPoint point = hi.SeriesPoint;

            //        // Check whether the series point was clicked or not. 
            //        if (point != null)
            //        {
            //            this.PopupRawDataEdit(point);
            //            //this.txtPointView.Text = point.Argument.ToString();
            //            //string pointInfo = string.Format("asixs 라벨: {0},  값: {1}"
            //            //    , point.Argument.ToString()
            //            //    , point.Values[0].ToSafeString());
            //            //MessageBox.Show(pointInfo);
            //        }
            //    }
            //};

            //Control Chart Click 이벤트
            //this.ChrPoltA.MouseClick += (s, e) =>
            //{
            //    if (e.Button != MouseButtons.Left)
            //    {
            //        _IsPaint = false;
            //        return;
            //    }

            //    //sia확인 : Chart 팝업 - ChrXbarR_Click
            //    ChartHitInfo hitInfo = ((s as SmartChart).CalcHitInfo((e as MouseEventArgs).Location));
            //    SeriesPoint sp = hitInfo.SeriesPoint;
            //    _x = hitInfo.HitPoint.X;
            //    _y = hitInfo.HitPoint.Y;
            //    _IsPaint = true;
            //};

            ////Control Chart Enter Focus 이벤트
            //this.ChrPoltA.Enter += (s, e) =>
            //{
            //    spcEop.groupName = "groupName";
            //    spcEop.subName = "subName";
            //    spcEop.ChartName = ChrPoltA.Name.ToSafeString();
            //    SpcCpkChartEnterEventHandler?.Invoke(s, e, spcEop);
            //};

            //XBar-R 차트 더블클릭 이벤트
            //this.ChrPoltA.DoubleClick += (s, e) =>
            //{
            //    if (!IsDetailPopupLock)
            //    {
            //        if (_controlSpec != null && _controlSpec != null && _spcPara != null)
            //        {
            //            if (_spcPara.InputData != null)
            //            {
            //                //TestSpcBaseChart0301Raw frm = new TestSpcBaseChart0301Raw();//Test
            //                SpcStatusDetailChartPopup frm = new SpcStatusDetailChartPopup();
            //                frm.ucChartDetail1.SpcDetailPopupInitialize(_controlSpec);
            //                frm.ucChartDetail1.ucCpk1.IsDetailPopupLock = true;
            //                frm.ucChartDetail1.ucCpk1.ControlChartDataMapping(_dtControlData, _controlSpec, _spcPara);
            //                frm.ucChartDetail1.ucXBar1.IsDetailPopupLock = true;
            //                frm.ucChartDetail1.ucXBar1.ControlChartDataMapping(_dtControlData, _controlSpec, _spcPara);
            //                frm.ShowDialog();
            //            }
            //        }
            //    }
            //};

            // 옵션 테스트 버튼 클릭 이벤트
            this.btnOptionTest.Click += (s, e) =>
            {

            };

            #region Spec Control limit Option 처리

            //Chart sigma3 표시 유무
            chkSigma3.CheckedChanged += (s, e) =>
            {
                ((XYDiagram)ChrPoltA.Diagram).AxisY.Strips["Strip03Sigma"].Visible = chkSigma3.Checked;
            };

            //Chart sigma2 표시 유무
            chkSigma2.CheckedChanged += (s, e) =>
            {
                ((XYDiagram)ChrPoltA.Diagram).AxisY.Strips["Strip02Sigma"].Visible = chkSigma2.Checked;
            };

            //Chart sigma1 표시 유무
            chkSigma1.CheckedChanged += (s, e) =>
            {
                ((XYDiagram)ChrPoltA.Diagram).AxisY.Strips["Strip01Sigma"].Visible = chkSigma1.Checked;
            };

            //Chart USL 표시 유무
            chkUSL.CheckedChanged += (s, e) =>
            {
                if (_isControlEventsStop != false) return;
                ((XYDiagram)ChrPoltA.Diagram).AxisY.ConstantLines["ConstantLineUSL"].Visible = chkUSL.Checked;
                ChrPoltA.Series["Series06OverRuleUSL"].Visible = chkUSL.Checked;
                _isCancelXBar.usl = !chkUSL.Checked;
                ChartWholeRangeChange(_isCancelXBar, 0);
            };

            //Chart CSL 표시 유무
            chkCSL.CheckedChanged += (s, e) =>
            {
                if (_isControlEventsStop != false) return;
                ((XYDiagram)ChrPoltA.Diagram).AxisY.ConstantLines["ConstantLineTarget"].Visible = chkCSL.Checked;
                //ChrXbarR.Series["Series04OverRuleUSL"].Visible = chkCSL.Checked;
                _isCancelXBar.csl = !chkCSL.Checked;
                ChartWholeRangeChange(_isCancelXBar, 0);
            };

            //Chart LSL 표시 유무
            chkLSL.CheckedChanged += (s, e) =>
            {
                if (_isControlEventsStop != false) return;
                ((XYDiagram)ChrPoltA.Diagram).AxisY.ConstantLines["ConstantLineLSL"].Visible = chkLSL.Checked;
                ChrPoltA.Series["Series07OverRuleLSL"].Visible = chkLSL.Checked;
                _isCancelXBar.lsl = !chkLSL.Checked;
                ChartWholeRangeChange(_isCancelXBar, 0);
            };

            //Chart UCL 표시 유무
            chkUCL.CheckedChanged += (s, e) =>
            {
                if (_isControlEventsStop != false) return;
                ((XYDiagram)ChrPoltA.Diagram).AxisY.ConstantLines["ConstantLineUCL"].Visible = chkUCL.Checked;
                ChrPoltA.Series["Series04OverRuleUCL"].Visible = chkUCL.Checked;
                _isCancelXBar.ucl = !chkUCL.Checked;
                ChartWholeRangeChange(_isCancelXBar, 0);
            };

            //Chart CCL 표시 유무
            chkCCL.CheckedChanged += (s, e) =>
            {
                if (_isControlEventsStop != false) return;
                ((XYDiagram)ChrPoltA.Diagram).AxisY.ConstantLines["ConstantLineXBAR"].Visible = chkCCL.Checked;
                _isCancelXBar.ccl = !chkCCL.Checked;
                ChartWholeRangeChange(_isCancelXBar, 0);
            };

            //Chart LCL 표시 유무
            chkLCL.CheckedChanged += (s, e) =>
            {
                if (_isControlEventsStop != false) return;
                ((XYDiagram)ChrPoltA.Diagram).AxisY.ConstantLines["ConstantLineLCL"].Visible = chkLCL.Checked;
                ChrPoltA.Series["Series05OverRuleLCL"].Visible = chkLCL.Checked;
                _isCancelXBar.lcl = !chkLCL.Checked;
                ChartWholeRangeChange(_isCancelXBar, 0);
            };

            //Chart RUCL 표시 유무
            chkRUCL.CheckedChanged += (s, e) =>
            {
                if (_isControlEventsStop != false) return;
                ((XYDiagram)ChrPoltA.Diagram).SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRUCL"].Visible = chkRUCL.Checked;
                ChrPoltA.Series["Series14OverRuleRUCL"].Visible = chkRUCL.Checked;
                _isCancelXR.ucl = !chkRUCL.Checked;
                ChartWholeRangeChange(_isCancelXR, 1);
            };

            //Chart RCCL 표시 유무
            chkRCCL.CheckedChanged += (s, e) =>
            {
                if (_isControlEventsStop != false) return;
                ((XYDiagram)ChrPoltA.Diagram).SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRCCL"].Visible = chkRCCL.Checked;
                _isCancelXR.ccl = !chkRCCL.Checked;
                ChartWholeRangeChange(_isCancelXR, 1);
            };

            //Chart RLCL 표시 유무
            chkRLCL.CheckedChanged += (s, e) =>
            {
                if (_isControlEventsStop != false) return;
                ((XYDiagram)ChrPoltA.Diagram).SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRLCL"].Visible = chkRLCL.Checked;
                ChrPoltA.Series["Series15OverRuleRLCL"].Visible = chkRLCL.Checked;
                _isCancelXR.lcl = !chkRLCL.Checked;
                ChartWholeRangeChange(_isCancelXR, 1);
            };

            //Chart UOL 표시 유무
            chkUOL.CheckedChanged += (s, e) =>
            {
                if (_isControlEventsStop != false) return;
                ((XYDiagram)ChrPoltA.Diagram).AxisY.ConstantLines["ConstantLineUOL"].Visible = chkUOL.Checked;
                ChrPoltA.Series["Series08OverRuleUOL"].Visible = chkUOL.Checked;
                _isCancelXBar.uol = !chkUOL.Checked;
                ChartWholeRangeChange(_isCancelXBar, 0);
            };

            //Chart LOL 표시 유무
            chkLOL.CheckedChanged += (s, e) =>
            {
                if (_isControlEventsStop != false) return;
                ((XYDiagram)ChrPoltA.Diagram).AxisY.ConstantLines["ConstantLineLOL"].Visible = chkLOL.Checked;
                ChrPoltA.Series["Series09OverRuleLOL"].Visible = chkLOL.Checked;
                _isCancelXBar.lol = !chkLOL.Checked;
                ChartWholeRangeChange(_isCancelXBar, 0);
            };

            #endregion
        }

        /// <summary>
        /// User Control Load 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucAnalysisPlot_Load(object sender, EventArgs e)
        {
            this.ControlDefaultSetting();

            this.CoboBoxSettingChartType();//Chart Type

            //this.CoboBoxSettingSubGroupId();//Subgroup ID


            //this.layoutItemTemp01.HideToCustomization();
            //this.layoutItemTemp01Value.HideToCustomization();

            //sia확인 : UCTR-01.Load 이벤트
            //ChrXbarR.CrosshairEnabled = DefaultBoolean.False;
            //ChrXbarR.RuntimeHitTesting = true;
        }

        public void SubGroupIdSetting(SPCPara spcPara, ref bool isAgainAnalysisPlot)
        {
            if (isAgainAnalysisPlot)
            {
                _spcPara = spcPara;
                this.CoboBoxSettingSubGroupId();//Subgroup ID
                isAgainAnalysisPlot = false;
            }
        }

        //Chart USL 표시 유무
        private void chkUSLCheckedChanged()
        {
            if (_isControlEventsStop != false) return;
            ((XYDiagram)ChrPoltA.Diagram).AxisY.ConstantLines["ConstantLineUSL"].Visible = chkUSL.Checked;
            ChrPoltA.Series["Series06OverRuleUSL"].Visible = chkUSL.Checked;
            _isCancelXBar.usl = !chkUSL.Checked;
            ChartWholeRangeChange(_isCancelXBar, 0);
        }
        /// <summary>
        /// 분석용 SubgroupID 선택 TextChanged 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboOrderMode_TextChanged(object sender, EventArgs e)
        {
            string chartType = this.cboChartType.GetDataValue().ToSafeString();
            if (chartType.ToUpper() != "NONE" && chartType.ToUpper() != "") 
            {
                this.AnalysisExcuteCharts();
            }
        }
        /// <summary>
        /// 분석용 Chart Type 선택 TextChanged 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboChartType_TextChanged(object sender, EventArgs e)
        {
            this.AnalysisExcuteCharts();
            
            //Console.WriteLine(this.cboChartType.GetDataValue());
        }

        private void cboChartType_EditValueChanged(object sender, EventArgs e)
        {

        }




        #endregion

        #region public Function
        /// <summary>
        /// 분석 Chart 표시
        /// </summary>
        public void AnalysisExcuteCharts()
        {
            string legendText = "";
            string parSubgoupID = "";
            string cboChartType = "";

            this.ChrPoltA.Series["Series03Value"].Visible = false;
            this.ChrPoltA.Series["Series04OverRuleUCL"].Visible = false;
            this.ChrPoltA.Series["Series05OverRuleLCL"].Visible = false;
            this.ChrPoltA.Series["Series11AUCL"].Visible = false;
            this.ChrPoltA.Legends["LegendPane01"].Title.Text = legendText;

            parSubgoupID = this.cboOrderMode.GetDataValue().ToSafeString();
            cboChartType = this.cboChartType.GetDataValue().ToSafeString();
            legendText = this.cboChartType.Text.ToSafeString();
            if (parSubgoupID == null || parSubgoupID == "" || parSubgoupID == "None")
            {
                //MessageBox("선택된 SubgroupID가 없습니다.");
                return;
            }

            if (cboChartType == null || cboChartType == "" || cboChartType == "None")
            {
                return;
            }

            AnalysisChartType analysisType = new AnalysisChartType();
            
            switch (cboChartType)
            {
                case "None":              //미선택
                    analysisType = AnalysisChartType.None;
                    break;
                case "AnalysisPolt":      //비교분석 Polt
                    analysisType = AnalysisChartType.AnalysisPolt;
                    break;
                case "AnalysisLine":      //비교분석 Line
                    analysisType = AnalysisChartType.AnalysisLine;
                    break;
                case "BoxPlot":            //Box Plot
                    analysisType = AnalysisChartType.BoxPlot;
                    break;
                case "ThreePointDiagram":  //산점도
                    analysisType = AnalysisChartType.ThreePointDiagram;
                    break;
                case "TimeSeries":        //시계열도
                    analysisType = AnalysisChartType.TimeSeries;
                    break;
                default:
                    analysisType = AnalysisChartType.None;
                    break;
            }

            this.ChrPoltA.Legends["LegendPane01"].Title.Text = legendText;

            SpcParameter parPlotData = SpcParameter.Create();
            var rowSrc = _spcPara.InputData.AsEnumerable();
            var cs = rowSrc
                .Where(w => w.SUBGROUP == parSubgoupID)
                .GroupBy(g => new
                {
                    g.SUBGROUP,
                    g.SAMPLING
                })
                .Select(s => new
                {
                    sSUBGROUP = s.Key.SUBGROUP,
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
                DataRow row = parPlotData.SpcDataAnalysisTable.NewRow();
                row["SUBGROUP"] = item.sSUBGROUP;
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

                parPlotData.SpcDataAnalysisTable.Rows.Add(row);
            }



            //DB Binding Clear
            this.ChrPoltA.DataSource = null;

            //BoxPlot

            this.ChrPoltA.Series["Series03Value"].ValueDataMembers.Clear();
            this.ChrPoltA.Series["Series03Value"].Points.Clear();


            //Line
            
            this.ChrPoltA.Series["Series04OverRuleUCL"].ValueDataMembers.Clear();
            this.ChrPoltA.Series["Series04OverRuleUCL"].Points.Clear();

            //산점도
            
            this.ChrPoltA.Series["Series05OverRuleLCL"].ValueDataMembers.Clear();
            this.ChrPoltA.Series["Series05OverRuleLCL"].Points.Clear();

            //시계열도
            
            this.ChrPoltA.Series["Series11AUCL"].ValueDataMembers.Clear();
            this.ChrPoltA.Series["Series11AUCL"].Points.Clear();

            double whMin=0, whMax=0;
            try
            {
                var wholeDatas = parPlotData.SpcDataAnalysisTable.AsEnumerable();
                var samplingData = wholeDatas
                                          .GroupBy(g => g.Field<string>("SUBGROUP"))
                                          .Select(s => new
                                          {
                                              s.Key,
                                              wMin = s.Min(g => g.Field<double>("n01LowMin")),
                                              wMax = s.Max(g => g.Field<double>("n02HightMax")),
                                              Count = s.Count()
                                          });

                foreach (var item in samplingData)
                {
                    whMax = item.wMax.ToSafeDoubleZero();
                    whMin = item.wMin.ToSafeDoubleZero();
                }

                this.ChartWholeRangeChange(whMax, whMin);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            switch (analysisType)
            {
                case AnalysisChartType.AnalysisPolt:
                case AnalysisChartType.BoxPlot:
                    ChartBoxPlot(ref parPlotData, analysisType, ViewType.CandleStick);
                    break;
                case AnalysisChartType.AnalysisLine:
                    ChartBoxPlot(ref parPlotData, analysisType, ViewType.RangeBar);
                    break;
                default:
                    break;
            }

            switch (analysisType)
            {
                case AnalysisChartType.AnalysisPolt:
                case AnalysisChartType.ThreePointDiagram:
                    ChartThreePointDiagram(ref parPlotData, analysisType, parSubgoupID);
                    break;
                case AnalysisChartType.TimeSeries:
                    ChartTimeSeries(ref parPlotData, analysisType, parSubgoupID);
                    break;
                default:
                    break;
            }

            Console.WriteLine(parPlotData.SpcDataAnalysisTable.ToString());

        }

        /// <summary>
        /// Chart Box Polt Chart
        /// </summary>
        /// <param name="parPlotData"></param>
        public void ChartBoxPlot(ref SpcParameter parPlotData, AnalysisChartType chartType, ViewType chartViewType)
        {
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
            this.ChrPoltA.Series["Series03Value"].ValueDataMembers.Clear();
            this.ChrPoltA.Series["Series03Value"].Points.Clear();
            this.ChrPoltA.Series["Series03Value"].CrosshairLabelPattern = labelPattern;
            this.ChrPoltA.Series["Series03Value"].ChangeView(chartViewType);
            
            //Point Line
            this.ChrPoltA.Series["Series04OverRuleUCL"].ValueDataMembers.Clear();
            this.ChrPoltA.Series["Series04OverRuleUCL"].Points.Clear();
            this.ChrPoltA.Series["Series04OverRuleUCL"].CrosshairLabelPattern = "평균: {V:F3}";

            if (parPlotData.SpcDataAnalysisTable != null && parPlotData.SpcDataAnalysisTable.Rows.Count > 0)
            {
                rowMax = parPlotData.SpcDataAnalysisTable.Rows.Count;
                for (int i = 0; i < rowMax; i++)
                {
                    DataRow row = parPlotData.SpcDataAnalysisTable.Rows[i];
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

                    var recDatax = _spcPara.InputData.AsEnumerable()
                        .Where(w => w.SUBGROUP == subgroupId && w.SAMPLING == samplingId)
                        .Select(s => new { s.NVALUE })
                        .OrderBy(r1 => r1.NVALUE);
                    foreach (var item in recDatax)
                    {
                        p++;
                        dValue[p] = item.NVALUE.ToSafeDoubleStaMin();
                    }

                    nQ1 = dValue[npQ1];
                    nQ2 = dValue[npQ2];
                    nQ3 = dValue[npQ3];
                    nQ4 = dValue[npQ4];
                    nMedian = dValue[npMedian];
                    
                    row["n03CloseQ1"] = nQ1;
                    row["n06OpenQ3"] = nQ3;

                    row["n04Median"] = nMedian;
                    row["n21Qp1"] = npQ1;
                    row["n22Qp2"] = npQ2;
                    row["n23Qp3"] = npQ3;
                    row["n24Qp4"] = npQ4;
                    row["n25Q1"] = nQ1;
                    row["n26Q2"] = nQ2;
                    row["n27Q3"] = nQ3;
                    row["n28Q4"] = nQ4;
                    row["n31UiQR"] = nQ4;
                    row["n32LiQR"] = nQ4;

                    //BoxPlot
                    SeriesPoint seriesPoint = new SeriesPoint();
                    seriesPoint.Argument = samplingName;
                    //string argument = samplingName;
                    //double low = nMin;
                    //double high = nMax;
                    //double open = nQ3;
                    //double close = nQ1;
                    double[] val = new double[4];
                    val[0] = nMin;
                    val[1] = nMax;
                    val[2] = nQ3;
                    val[3] = nQ1;
                    seriesPoint.Values = val;
                    if (chartType == AnalysisChartType.AnalysisLine)
                    {
                        seriesPoint.Color = Color.OrangeRed;
                    }
                    else
                    {
                        seriesPoint.Color = Color.DarkGray;
                    }
                        
                    //this.ChrPoltA.Series["Series03Value"].Points.AddFinancialPoint( argument, low, high, open, close);
                    this.ChrPoltA.Series["Series03Value"].Points.Add(seriesPoint);

                    if (chartType == AnalysisChartType.AnalysisLine)
                    {
                        //BoxPlot
                        SeriesPoint seriesPointLine = new SeriesPoint();
                        seriesPointLine.Argument = samplingName;
                        double[] valLine = new double[1];
                        valLine[0] = nAvg;
                        seriesPointLine.Values = valLine;
                        seriesPointLine.Color = Color.Black;
                        this.ChrPoltA.Series["Series04OverRuleUCL"].Points.Add(seriesPointLine);
                        
                    }

                }
            }


            this.ChrPoltA.Series["Series03Value"].Visible = true;

            switch (chartType)
            {
                case AnalysisChartType.AnalysisLine:
                    this.ChrPoltA.Series["Series04OverRuleUCL"].Visible = true;
                    break;
                default:
                    this.ChrPoltA.Series["Series04OverRuleUCL"].Visible = false;
                    break;
            }

            //this.ChrPoltA.Series["Series03Value"].CrosshairLabelPattern = "{A}\nMin: {LV:F3}\nMax: {HV:F3}\nQ1: {CV:F3}\nQ3: {OV:F3}";
        }
        /// <summary>
        /// Chart Box Polt Bainding 적용
        /// </summary>
        /// <param name="parPlotData"></param>
        public void ChartBoxPlotBainding(ref SpcParameter parPlotData, AnalysisChartType chartType, string parSubgoupID)
        {
            this.ChrPoltA.DataSource = parPlotData.SpcDataAnalysisTable;
            //Plot Chart
            this.ChrPoltA.Series["Series03Value"].ValueDataMembers.Clear();
            this.ChrPoltA.Series["Series03Value"].ChangeView(ViewType.CandleStick);
            //this.ChrPoltA.Series["Series03Value"].ChangeView(ViewType.RangeBar);
            this.ChrPoltA.Series["Series03Value"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series03Value"].ValueDataMembers.AddRange(new string[] { "n01LowMin", "n02HightMax", "n06OpenQ3", "n03CloseQ1" });
            this.ChrPoltA.Series["Series03Value"].CrosshairLabelPattern = "{A}\nMin: {LV:F3}\nMax: {HV:F3}\nQ1: {CV:F3}\nQ3: {OV:F3}";
            this.ChrPoltA.Series["Series03Value"].Visible = true;
            this.ChrPoltA.Series["Series03Value"].View.Color = Color.DarkGray;
        }
        /// <summary>
        /// Chart 산점동
        /// </summary>
        /// <param name="parPlotData"></param>
        /// <param name="parSubgoupID"></param>
        public void ChartThreePointDiagram(ref SpcParameter parPlotData, AnalysisChartType chartType, string parSubgoupID)
        {
            //LCL
            this.ChrPoltA.Series["Series05OverRuleLCL"].ValueDataMembers.Clear();
            this.ChrPoltA.Series["Series05OverRuleLCL"].Points.Clear();
            var dataPoint = _spcPara.InputData.AsEnumerable();
            var partPoint = dataPoint
                .Where(w => w.SUBGROUP == parSubgoupID)
                .Select(s => new
                {
                    sSUBGROUP = s.SUBGROUP,
                    sSAMPLING = s.SAMPLING,
                    sSUBGROUPNAME = s.SUBGROUPNAME,
                    sSAMPLINGNAME = s.SAMPLINGNAME,
                    nNVALUE = s.NVALUE
                })
                .OrderBy(r => r.sSAMPLING);
            foreach (var item in partPoint)
            {
                SeriesPoint seriesPoint = new SeriesPoint();
                seriesPoint.Argument = item.sSAMPLINGNAME;
                double[] val = new double[1];
                val[0] = item.nNVALUE;
                seriesPoint.Values = val;
                seriesPoint.Color = Color.Blue;
                this.ChrPoltA.Series["Series05OverRuleLCL"].Points.Add(seriesPoint);
            }

            this.ChrPoltA.Series["Series05OverRuleLCL"].View.Color = Color.Blue;
            if (chartType != AnalysisChartType.ThreePointDiagram)
            {
                this.ChrPoltA.Series["Series05OverRuleLCL"].CrosshairLabelPattern = "측정값: {V:F3}";
            }
            else
            {
                this.ChrPoltA.Series["Series05OverRuleLCL"].CrosshairLabelPattern = "{A}\n측정값: {V:F3}";
            }
            this.ChrPoltA.Series["Series05OverRuleLCL"].Visible = true;
        }
        /// <summary>
        /// Chart 시계열동
        /// </summary>
        /// <param name="parPlotData"></param>
        /// <param name="parSubgoupID"></param>
        public void ChartTimeSeries(ref SpcParameter parPlotData, AnalysisChartType chartType, string parSubgoupID)
        {
            int nSeq = 0;
            //LCL
            this.ChrPoltA.Series["Series11AUCL"].ValueDataMembers.Clear();
            this.ChrPoltA.Series["Series11AUCL"].Points.Clear();

            var dataPoint = _spcPara.InputData.AsEnumerable();
            var partPoint = dataPoint
                .Where(w => w.SUBGROUP == parSubgoupID)
                .Select(s => new
                {
                    sSUBGROUP = s.SUBGROUP,
                    sSAMPLING = s.SAMPLING,
                    sSUBGROUPNAME = s.SUBGROUPNAME,
                    sSAMPLINGNAME = s.SAMPLINGNAME,
                    nNVALUE = s.NVALUE
                })
                .OrderBy(r => r.sSAMPLING);
            foreach (var item in partPoint)
            {
                nSeq++;
                SeriesPoint seriesPoint = new SeriesPoint();
                seriesPoint.Argument = nSeq.ToString();
                double[] val = new double[1];
                val[0] = item.nNVALUE;
                seriesPoint.Values = val;
                //seriesPoint.Color = Color.Blue;
                this.ChrPoltA.Series["Series11AUCL"].Points.Add(seriesPoint);
            }
            
            this.ChrPoltA.Series["Series11AUCL"].View.Color = Color.Blue;
            this.ChrPoltA.Series["Series11AUCL"].CrosshairLabelPattern = "{A}\n측정값: {V:F3}";
            this.ChrPoltA.Series["Series11AUCL"].Visible = true;
        }

        public void ChartTemp()
        {
            //DataTable 일경우 AsEnumerable 변환 후 처리함.
            _spcPara.AnalysisInputData = new ParPIDataTable();
            var rowDatas = _spcPara.InputData.AsEnumerable()
                //.Where(w => w.SAMPLING == "LOTYYYYMMDD02")
                //.Select(s => new { s.NVALUE })
                //.GroupBy(x => new { x.SUBGROUP, x.SAMPLING })
                .OrderBy(r1 => r1.SUBGROUP)
                .ThenBy(r2 => r2.SAMPLING)
                .ThenBy(r3 => r3.NVALUE);
            foreach (DataRow item in rowDatas)
            {
                _spcPara.AnalysisInputData.ImportRow(item);
            }

            var recData = _spcPara.AnalysisInputData.AsEnumerable()
                .Where(w => w.SAMPLING == "LOTYYYYMMDD02")
                .Select(s => new { s.NVALUE })
                .OrderBy(r1 => r1.NVALUE);




            //DataTable 일경우 AsEnumerable 변환 후 처리함.

            var query = from r in rowDatas
                            //where r.Field<string>("ctype") == "XBARR"
                        group r by r.Field<string>("SUBGROUP")
                        //, r.Field<string>("SAMPLING")
                        into g
                        select new
                        {
                            Style = g.Key,
                            //AverageListPrice = g.Average(product => product.Field<Decimal>("ListPrice"))
                            nCount = g.Count()
                        };
            foreach (var rw in query)
            {
                //rowMaxInputData = rw.nCount;
                Console.WriteLine("Product style: {0} nCount: {1}",
                    rw.Style, rw.nCount);
            }

            //double[] nitem = new double[rowDatas.Length];
        }

        //화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 관리도 Chart Test Data 입력.
        /// </summary>
        public void ControlChartDataMapping(RtnControlDataTable dtControlData, ControlSpec controlSpec, SPCPara spcPara)
        {
            //입력자료 전이
            _dtControlData = dtControlData;
            _controlSpec = controlSpec;
            _SUBGROUP = controlSpec.sigmaResult.subGroup;
            _SUBGROUPNAME = controlSpec.sigmaResult.subGroupName;
            _spcPara = spcPara;

            _controlSpec.spcOption.chartName.mainCSL = "";
            _controlSpec.spcOption.chartName.subCCL = "";
            switch (_controlSpec.spcOption.chartType)
            {
                case ControlChartType.XBar_R:
                    _controlSpec.spcOption.chartName.mainCSL = "XBAR";
                    _controlSpec.spcOption.chartName.subCCL = "R";
                    break;
                case ControlChartType.XBar_S:
                    _controlSpec.spcOption.chartName.mainCSL = "XBAR";
                    _controlSpec.spcOption.chartName.subCCL = "S";
                    break;
                case ControlChartType.I_MR:
                    _controlSpec.spcOption.chartName.mainCSL = "I";
                    _controlSpec.spcOption.chartName.subCCL = "MR";
                    break;
                case ControlChartType.Merger:
                    _controlSpec.spcOption.chartName.mainCSL = "XBAR";
                    _controlSpec.spcOption.chartName.subCCL = "PM";
                    break;
                case ControlChartType.np:
                    _controlSpec.spcOption.chartName.mainCSL = "NP";
                    break;
                case ControlChartType.p:
                    _controlSpec.spcOption.chartName.mainCSL = "P";
                    break;
                case ControlChartType.c:
                    _controlSpec.spcOption.chartName.mainCSL = "C";
                    break;
                case ControlChartType.u:
                    _controlSpec.spcOption.chartName.mainCSL = "U";
                    break;
                default:
                    break;
            }

            switch (_controlSpec.spcOption.chartType)
            {
                case ControlChartType.XBar_R:
                case ControlChartType.XBar_S:
                case ControlChartType.I_MR:
                case ControlChartType.Merger:
                    this.ControlChartDataMappingXBAR();
                    break;
                case ControlChartType.np:
                case ControlChartType.p:
                case ControlChartType.c:
                case ControlChartType.u:
                    this.ControlChartDataMappingPCU();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 변경 Spec명
        /// </summary>
        /// <param name="specName"></param>
        public void ChartWholeRangeChange(double nMax, double nMin)
        {
            XYDiagram diagram = (XYDiagram)ChrPoltA.Diagram;
            XYDiagramDefaultPane defaultDiagPane = diagram.DefaultPane;
            XYDiagramPane secondDiagPane = diagram.Panes["SecondPane"];
            ControlSpec tempControlSpec = new ControlSpec();
            ChartWholeValue tempWholeRangeXBar = ChartWholeValue.Create();
            ChartWholeValue tempWholeRangeRS = ChartWholeValue.Create();

            diagram.AxisY.WholeRange.MaxValue = nMax;
            diagram.AxisY.WholeRange.MinValue = nMin;

            ////Chart Whole Range 
            //tempControlSpec = _controlSpec;
            //if (isChart != 1)
            //{
            //    tempWholeRangeXBar = SpcFunction.chartWholeRange(tempControlSpec.nXbar, tempControlSpec.spcOption, tempIsCancel);
            //    diagram.AxisY.WholeRange.MaxValue = tempWholeRangeXBar.max;
            //    diagram.AxisY.WholeRange.MinValue = tempWholeRangeXBar.min;
            //}
            //else
            //{
            //    tempWholeRangeRS = SpcFunction.chartWholeRange(tempControlSpec.nR, tempControlSpec.spcOption, tempIsCancel);
            //    diagram.SecondaryAxesY["SecondaryAxisY01"].WholeRange.MaxValue = tempWholeRangeRS.max;
            //    diagram.SecondaryAxesY["SecondaryAxisY01"].WholeRange.MinValue = tempWholeRangeRS.min;
            //}

        }

        /// <summary>
        /// 변경 Spec명
        /// </summary>
        /// <param name="specName"></param>
        public void ChartWholeRangeChange(IsSpecCancel tempIsCancel, int isChart)
        {
            XYDiagram diagram = (XYDiagram)ChrPoltA.Diagram;
            XYDiagramDefaultPane defaultDiagPane = diagram.DefaultPane;
            XYDiagramPane secondDiagPane = diagram.Panes["SecondPane"];
            ControlSpec tempControlSpec = new ControlSpec();
            ChartWholeValue tempWholeRangeXBar = ChartWholeValue.Create();
            ChartWholeValue tempWholeRangeRS = ChartWholeValue.Create();

            //Chart Whole Range 
            tempControlSpec = _controlSpec;
            if (isChart != 1)
            {
                tempWholeRangeXBar = SpcFunction.chartWholeRange(tempControlSpec.nXbar, tempControlSpec.spcOption, tempIsCancel);
                diagram.AxisY.WholeRange.MaxValue = tempWholeRangeXBar.max;
                diagram.AxisY.WholeRange.MinValue = tempWholeRangeXBar.min;
            }
            else
            {
                tempWholeRangeRS = SpcFunction.chartWholeRange(tempControlSpec.nR, tempControlSpec.spcOption, tempIsCancel);
                diagram.SecondaryAxesY["SecondaryAxisY01"].WholeRange.MaxValue = tempWholeRangeRS.max;
                diagram.SecondaryAxesY["SecondaryAxisY01"].WholeRange.MinValue = tempWholeRangeRS.min;
            }

        }

        /// <summary>
        /// Chart Whole Range 직접 설정.
        /// </summary>
        /// <param name="wholeRange"></param>
        public void ChartWholeRangeDirectChange(ChartWholeRangeDirectValue wholeRange)
        {
            XYDiagram diagram = (XYDiagram)ChrPoltA.Diagram;
            XYDiagramDefaultPane defaultDiagPane = diagram.DefaultPane;
            XYDiagramPane secondDiagPane = diagram.Panes["SecondPane"];

            if (wholeRange.WholeRangeP1Max != null)
            {
                diagram.AxisY.WholeRange.MaxValue = wholeRange.WholeRangeP1Max;
            }
            if (wholeRange.WholeRangeP1Min != null)
            {
                diagram.AxisY.WholeRange.MinValue = wholeRange.WholeRangeP1Min;
            }

            if (wholeRange.WholeRangeP2Max != null)
            {
                diagram.SecondaryAxesY["SecondaryAxisY01"].WholeRange.MaxValue = wholeRange.WholeRangeP2Max;
            }
            if (wholeRange.WholeRangeP2Min != null)
            {
                diagram.SecondaryAxesY["SecondaryAxisY01"].WholeRange.MinValue = wholeRange.WholeRangeP2Min;
            }
        }
        /// <summary>
        /// Chart Whole Range 원본 복원.
        /// </summary>
        public void ChartWholeRangeOrginalChange()
        {
            XYDiagram diagram = (XYDiagram)ChrPoltA.Diagram;
            XYDiagramDefaultPane defaultDiagPane = diagram.DefaultPane;
            XYDiagramPane secondDiagPane = diagram.Panes["SecondPane"];

            if (_originalWholeRange.WholeRangeP1Max != null)
            {
                diagram.AxisY.WholeRange.MaxValue = _originalWholeRange.WholeRangeP1Max;
            }
            if (_originalWholeRange.WholeRangeP1Min != null)
            {
                diagram.AxisY.WholeRange.MinValue = _originalWholeRange.WholeRangeP1Min;
            }

            if (_originalWholeRange.WholeRangeP2Max != null)
            {
                diagram.SecondaryAxesY["SecondaryAxisY01"].WholeRange.MaxValue = _originalWholeRange.WholeRangeP2Max;
            }
            if (_originalWholeRange.WholeRangeP2Min != null)
            {
                diagram.SecondaryAxesY["SecondaryAxisY01"].WholeRange.MinValue = _originalWholeRange.WholeRangeP2Min;
            }
        }
        /// <summary>
        /// 관리도 XBAR-R, S, 합동, I-MR 처리
        /// </summary>
        /// <param name="dtControlData"></param>
        /// <param name="controlSpec"></param>
        /// <param name="spcPara"></param>
        public void ControlChartDataMappingXBAR()
        {
            _isControlEventsStop = true;
            //sia확인 : 02.관리도 Chart XBAR Data 입력 및 Mapping [ControlChartDataMappingXBAR].
            SpcParameter xbarData = SpcParameter.Create();
            //ControlSpec controlSpec = new ControlSpec();
            this.DetailChartControlClearPCU(LayoutVisibility.Always);
            DataRow rowSpcData;
            XYDiagram diagram = (XYDiagram)ChrPoltA.Diagram;
            XYDiagramDefaultPane defaultDiagPane = diagram.DefaultPane;
            XYDiagramPane secondDiagPane = diagram.Panes["SecondPane"];

            this.ChrPoltA.Series["Series03Value"].Label.TextPattern = "Value {A}:{V:#0.000}";
            this.ChrPoltA.Series["Series06OverRuleUSL"].Label.TextPattern = "{A}:{V:#0.000}";
            this.ChrPoltA.Series["Series07OverRuleLSL"].Label.TextPattern = "{A}:{V:#0.000}";

            if (_dtControlData != null)
            {
                foreach (DataRow r in _dtControlData)
                {
                    rowSpcData = xbarData.SpcData.NewRow();
                    //Xbar
                    rowSpcData["Label"] = r["SAMPLING"].ToSafeString();
                    rowSpcData["nValue"] = r["BAR"];
                    //_controlSpec.nXbar.XDBar = r["BAR"].ToSafeDoubleStaMax();
                    //if (nXbarValue == SpcLimit.MAX)
                    //{
                    //    nXbarValue = r["XBAR"].ToSafeDoubleStaMin();
                    //    _controlSpec.nXbar.XDBar = nXbarValue;
                    //}

                    rowSpcData["AUCL"] = r["UCL"];
                    rowSpcData["ALCL"] = r["LCL"];

                    //R
                    rowSpcData["nRValue"] = r["R"];
                    rowSpcData["RACCL"] = r["RBAR"];
                    //_controlSpec.nXbar.XDRar = r["RBAR"].ToSafeDoubleStaMax();
                    //if (nXRValue == SpcLimit.MAX)
                    //{
                    //    nXRValue = r["R"].ToSafeDoubleStaMin();
                    //    _controlSpec.nR.XDRar = nXRValue;
                    //}

                    rowSpcData["RAUCL"] = r["RUCL"];
                    rowSpcData["RALCL"] = r["RLCL"];
                    xbarData.SpcData.Rows.Add(rowSpcData);
                }
            }
            else
            {
                return;
            }

            //DataTable 일경우 AsEnumerable 변환 후 처리함.
            SpcSpec tempSpecXBar = SpcSpec.Create();
            SpcSpec tempSpecXR = SpcSpec.Create();
            try
            {
                var rowDatas = _dtControlData.AsEnumerable();
                var query = from r in rowDatas
                                //where r.Field<string>("ctype") == "XBARR"
                            group r by r.Field<string>("SUBGROUP") into g
                            select new
                            {
                                Style = g.Key,
                                //AverageListPrice = g.Average(product => product.Field<Decimal>("ListPrice"))
                                nBARMAX = g.Max(s => s.Field<double>("BAR")).ToNullOrDouble(),
                                nBARMIN = g.Min(s => s.Field<double>("BAR")).ToNullOrDouble(),
                                nRMAX = g.Max(s => s.Field<double>("R")).ToNullOrDouble(),
                                nRMIN = g.Min(s => s.Field<double>("R")).ToNullOrDouble(),
                                //해석용 값
                                nUCL = g.Max(s => s.Field<double>("UCL")).ToNullOrDouble(),
                                nCCL = g.Max(s => s.Field<double>("CL")).ToNullOrDouble(),
                                nLCL = g.Min(s => s.Field<double>("LCL")).ToNullOrDouble(),

                                nRUCL = g.Max(s => s.Field<double>("RUCL")).ToNullOrDouble(),
                                nRCCL = g.Max(s => s.Field<double>("RCL")).ToNullOrDouble(),
                                nRLCL = g.Min(s => s.Field<double>("RLCL")).ToNullOrDouble(),

                                nXBAR = g.Max(s => s.Field<double>("XBAR")).ToNullOrDouble(),
                                nRBAR = g.Max(s => s.Field<double>("RBAR")).ToNullOrDouble(),
                                nCount = g.Count()
                            };
                foreach (var rw in query)
                {
                    Console.WriteLine("nCount: {0}", rw.nCount);
                    //XBAR
                    tempSpecXBar.BarMax = rw.nBARMAX.ToSafeDoubleStaMax();
                    tempSpecXBar.BarMin = rw.nBARMIN.ToSafeDoubleStaMax();
                    tempSpecXBar.XDBar = rw.nXBAR.ToSafeDoubleStaMax();
                    tempSpecXBar.ucl = rw.nUCL.ToSafeDoubleStaMax();
                    tempSpecXBar.ccl = rw.nCCL.ToSafeDoubleStaMax();
                    tempSpecXBar.lcl = rw.nLCL.ToSafeDoubleStaMin();

                    //R
                    tempSpecXR.RMax = rw.nRMAX.ToSafeDoubleStaMax();
                    tempSpecXR.RMin = rw.nRMIN.ToSafeDoubleStaMax();
                    tempSpecXR.XDRar = rw.nRBAR.ToSafeDoubleStaMax();
                    tempSpecXR.ucl = rw.nRUCL.ToSafeDoubleStaMax();
                    tempSpecXR.ccl = rw.nRCCL.ToSafeDoubleStaMax();
                    tempSpecXR.lcl = rw.nRLCL.ToSafeDoubleStaMin();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //해석용 구분
            _IsPaint = false;
            switch (_controlSpec.spcOption.limitType)
            {
                case LimitType.Interpretation:
                    _controlSpec.nXbar.ucl = tempSpecXBar.ucl;
                    //_controlSpec.nXbar.ccl = tempSpecXBar.ccl;
                    _controlSpec.nXbar.lcl = tempSpecXBar.lcl;
                    _controlSpec.nR.ucl = tempSpecXR.ucl;
                    _controlSpec.nR.ccl = tempSpecXR.ccl;
                    _controlSpec.nR.lcl = tempSpecXR.lcl;
                    break;
                case LimitType.Management:
                    break;
                case LimitType.Direct:
                    break;
                default:
                    break;
            }

            //Chart Whole Range 
            _controlSpec.nXbar.XDBar = tempSpecXBar.XDBar;
            _controlSpec.nR.XDRar = tempSpecXR.XDRar;

            _controlSpec.nXbar.BarMax = tempSpecXBar.BarMax.ToSafeDoubleStaMax();
            _controlSpec.nXbar.BarMin = tempSpecXBar.BarMin.ToSafeDoubleStaMax();
            _controlSpec.nR.RMax = tempSpecXR.RMax.ToSafeDoubleStaMax();
            _controlSpec.nR.RMin = tempSpecXR.RMin.ToSafeDoubleStaMax();

            IsSpecCancel.Clear(ref _isCancelXBar);
            IsSpecCancel.Clear(ref _isCancelXR);
            _wholeRangeXBar = SpcFunction.chartWholeRange(_controlSpec.nXbar, _controlSpec.spcOption, _isCancelXBar);
            _wholeRangeRS = SpcFunction.chartWholeRange(_controlSpec.nR, _controlSpec.spcOption, _isCancelXR);

            diagram.AxisY.WholeRange.MaxValue = _wholeRangeXBar.max;
            diagram.AxisY.WholeRange.MinValue = _wholeRangeXBar.min;
            diagram.SecondaryAxesY["SecondaryAxisY01"].WholeRange.MaxValue = _wholeRangeRS.max;
            diagram.SecondaryAxesY["SecondaryAxisY01"].WholeRange.MinValue = _wholeRangeRS.min;

            _originalWholeRange.WholeRangeP1Max = _wholeRangeXBar.max;
            _originalWholeRange.WholeRangeP1Min = _wholeRangeXBar.min;
            _originalWholeRange.WholeRangeP2Max = _wholeRangeRS.max;
            _originalWholeRange.WholeRangeP2Min = _wholeRangeRS.min;

            //OverRules Check --------------------------------------------
            xbarData.SpecCheckXBar(_controlSpec, ref xbarData);
            //OverRules Check --------------------------------------------

            switch (_controlSpec.spcOption.chartType)
            {
                case ControlChartType.I_MR:
                    xbarData.SpcData.Rows[0]["RAUCL"] = DBNull.Value;
                    xbarData.SpcData.Rows[0]["nRValue"] = DBNull.Value;
                    xbarData.SpcData.Rows[0]["RALCL"] = DBNull.Value;

                    xbarData.SpcData.Rows[0]["nROverRuleUCL"] = DBNull.Value;
                    xbarData.SpcData.Rows[0]["nROverRuleLCL"] = DBNull.Value;
                    break;
                default:
                    break;
            }

            this.ChrPoltA.DataSource = xbarData.SpcData;

            #region Chart Series Mapping
            // Create a series, and add it to the chart. 
            //Series series1 = new Series("My Series", ViewType.Bar);
            this.ChrPoltA.Series["Series01AUCL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series01AUCL"].ValueDataMembers.AddRange(new string[] { "AUCL" });
            this.ChrPoltA.Series["Series02ALCL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series02ALCL"].ValueDataMembers.AddRange(new string[] { "ALCL" });

            this.ChrPoltA.Series["Series03Value"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series03Value"].ValueDataMembers.AddRange(new string[] { "nValue" });
            //this.ChrXbarR.Series["Series03Value"].ValueDataMembers.AddRange(new string[] { "nValue", "ALCL" });


            switch (_controlSpec.spcOption.chartType)
            {
                case ControlChartType.XBar_R:
                case ControlChartType.XBar_S:
                case ControlChartType.I_MR:
                case ControlChartType.Merger:
                    this.ChrPoltA.Series["Series11AUCL"].ArgumentDataMember = "Label";
                    this.ChrPoltA.Series["Series11AUCL"].ValueDataMembers.AddRange(new string[] { "RAUCL" });
                    this.ChrPoltA.Series["Series12ALCL"].ArgumentDataMember = "Label";
                    this.ChrPoltA.Series["Series12ALCL"].ValueDataMembers.AddRange(new string[] { "RALCL" });
                    this.ChrPoltA.Series["Series13Value"].ArgumentDataMember = "Label";
                    this.ChrPoltA.Series["Series13Value"].ValueDataMembers.AddRange(new string[] { "nRValue" });
                    break;
                case ControlChartType.np:
                case ControlChartType.p:
                case ControlChartType.c:
                case ControlChartType.u:
                    break;
                default:
                    break;
            }

            //double? nRValue = 0;
            //string rValueLabel = "";
            //long nRlength = xbarData.SpcData.Rows.Count;
            //this.ChrXbarR.Series["Series13Value"].ArgumentDataMember = "";
            //this.ChrXbarR.Series["Series13Value"].ValueDataMembers.RemoveAt(0);
            //for (int i = 0; i < nRlength; i++)
            //{
            //    DataRow dataRaw = xbarData.SpcData.Rows[i];
            //    nRValue = SpcFunction.IsDbNckDoubleMax(dataRaw, "nRValue");
            //    rValueLabel = SpcFunction.IsDbNck(dataRaw, "Label");
            //    SeriesPoint seriesPoint = new SeriesPoint();
            //    seriesPoint.Argument = rValueLabel;
            //    double[] val = new double[1];
            //    val[0] = nRValue.ToSafeDoubleNaN();
            //    seriesPoint.Values = val;
            //    this.ChrXbarR.Series["Series13Value"].Points.Add(seriesPoint);
            //}
            //this.ChrXbarR.Series["Series13Value"].ArgumentDataMember = "";
            //this.ChrXbarR.Series["Series13Value"].ValueDataMembers.RemoveAt(0);


            ////this.ChrXbarR.Series["Series13Value"].Points[0].Argument 
            //double[] val = new double[1];
            ////val[0] = axisY;
            //this.ChrXbarR.Series["Series13Value"].Points[0].Values = val;

            //UCL OverRules - Control Limit
            this.ChrPoltA.Series["Series04OverRuleUCL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series04OverRuleUCL"].ValueDataMembers.AddRange(new string[] { "nOverRuleUCL" });
            //LCL
            this.ChrPoltA.Series["Series05OverRuleLCL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series05OverRuleLCL"].ValueDataMembers.AddRange(new string[] { "nOverRuleLCL" });

            //USL OverRules - Spec
            this.ChrPoltA.Series["Series06OverRuleUSL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series06OverRuleUSL"].ValueDataMembers.AddRange(new string[] { "nOverRuleUSL" });
            //LSL
            this.ChrPoltA.Series["Series07OverRuleLSL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series07OverRuleLSL"].ValueDataMembers.AddRange(new string[] { "nOverRuleLSL" });

            //UOL OverRules - Outlier
            this.ChrPoltA.Series["Series08OverRuleUOL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series08OverRuleUOL"].ValueDataMembers.AddRange(new string[] { "nOverRuleUOL" });
            //LOL
            this.ChrPoltA.Series["Series09OverRuleLOL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series09OverRuleLOL"].ValueDataMembers.AddRange(new string[] { "nOverRuleLOL" });

            //LSL OverRules
            this.ChrPoltA.Series["Series14OverRuleRUCL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series14OverRuleRUCL"].ValueDataMembers.AddRange(new string[] { "nROverRuleUCL" });

            this.ChrPoltA.Series["Series15OverRuleRLCL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series15OverRuleRLCL"].ValueDataMembers.AddRange(new string[] { "nROverRuleLCL" });



            #endregion

            pnlMain.Dock = DockStyle.Fill;

            #region Option - Text View (Chart)

            string textView = "";
            textView = Math.Round(_controlSpec.sigmaResult.cpkResult.nCP, 6).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblCp.ForeColor = Color.Black;
                this.lblCpValue.Text = textView;
                this.lblCpValue.ForeColor = Color.Black;
            }
            else
            {
                this.lblCp.ForeColor = Color.Gray;
                this.lblCpValue.Text = SpcLimit.NONEDATA;
                this.lblCpValue.ForeColor = Color.Gray;
            }

            textView = Math.Round(_controlSpec.sigmaResult.cpkResult.nCPK, 6).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblCpk.ForeColor = Color.Black;
                this.lblCpkValue.Text = textView;
                this.lblCpkValue.ForeColor = Color.Black;
            }
            else
            {
                this.lblCpk.ForeColor = Color.Gray;
                this.lblCpkValue.Text = SpcLimit.NONEDATA;
                this.lblCpkValue.ForeColor = Color.Gray;
            }

            textView = Math.Round(_controlSpec.sigmaResult.cpkResult.nPP, 6).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblPp.ForeColor = Color.Black;
                this.lblPpValue.Text = textView;
                this.lblPpValue.ForeColor = Color.Black;
            }
            else
            {
                this.lblPp.ForeColor = Color.Gray;
                this.lblPpValue.Text = SpcLimit.NONEDATA;
                this.lblPpValue.ForeColor = Color.Gray;
            }

            textView = Math.Round(_controlSpec.sigmaResult.cpkResult.nPPK, 6).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblPpk.ForeColor = Color.Black;
                this.lblPpkValue.Text = textView;
                this.lblPpkValue.ForeColor = Color.Black;
            }
            else
            {
                this.lblPpk.ForeColor = Color.Gray;
                this.lblPpkValue.Text = SpcLimit.NONEDATA;
                this.lblPpkValue.ForeColor = Color.Gray;
            }


            //UCL
            textView = Math.Round(_controlSpec.nXbar.ucl, 3).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblUCLValue.Text = textView;
                this.lblUCLValue.ForeColor = Color.Black;
                this.chkUCL.ForeColor = Color.Blue;
                this.chkUCL.Checked = true;
                this.chkUCL.Enabled = true;
            }
            else
            {
                this.lblUCLValue.Text = SpcLimit.NONEDATA;
                this.lblUCLValue.ForeColor = Color.Gray;
                this.chkUCL.ForeColor = Color.Gray;
                this.chkUCL.Checked = false;
                this.chkUCL.Enabled = false;
            }

            //XBAR or CCL
            textView = Math.Round(_controlSpec.nXbar.XDBar, 3).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblCCLValue.Text = textView;
                this.lblCCLValue.ForeColor = Color.Black;
                this.chkCCL.ForeColor = Color.Green;
                this.chkCCL.Checked = true;
                this.chkCCL.Enabled = true;
                this.chkCCL.Text = _controlSpec.spcOption.chartName.mainCSL;//XBAR, I
            }
            else
            {
                this.lblCCLValue.Text = SpcLimit.NONEDATA;
                this.lblCCLValue.ForeColor = Color.Gray;
                this.chkCCL.ForeColor = Color.Gray;
                this.chkCCL.Checked = false;
                this.chkCCL.Enabled = false;
                this.chkCCL.Text = "XBAR";
            }

            //LCL
            textView = Math.Round(_controlSpec.nXbar.lcl, 3).ToSafeString();
            if (textView != SpcLimit.MINTXT && textView != SpcLimit.MINTXT)
            {
                this.lblLCLValue.Text = textView;
                this.lblLCLValue.ForeColor = Color.Black;
                this.chkLCL.ForeColor = Color.Blue;
                this.chkLCL.Checked = true;
                this.chkLCL.Enabled = true;
            }
            else
            {
                this.lblLCLValue.Text = SpcLimit.NONEDATA;
                this.lblLCLValue.ForeColor = Color.Gray;
                this.chkLCL.ForeColor = Color.Gray;
                this.chkLCL.Checked = false;
                this.chkLCL.Enabled = false;
            }

            //USL
            textView = Math.Round(_controlSpec.nXbar.usl, 3).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblUSLValue.Text = textView;
                this.lblUSLValue.ForeColor = Color.Black;
                this.chkUSL.ForeColor = Color.Red;
                this.chkUSL.Checked = true;
                this.chkUSL.Enabled = true;
            }
            else
            {
                this.lblUSLValue.Text = SpcLimit.NONEDATA;
                this.lblUSLValue.ForeColor = Color.Gray;
                this.chkUSL.ForeColor = Color.Gray;
                this.chkUSL.Checked = false;
                this.chkUSL.Enabled = false;
            }

            //CSL
            textView = Math.Round(_controlSpec.nXbar.csl, 3).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblCSLValue.Text = textView;
                this.lblCSLValue.ForeColor = Color.Black;
                this.chkCSL.ForeColor = Color.OrangeRed;
                this.chkCSL.Checked = true;
                this.chkCSL.Enabled = true;
                this.chkCSL.Text = "Target";
            }
            else
            {
                this.lblCSLValue.Text = SpcLimit.NONEDATA;
                this.lblCSLValue.ForeColor = Color.Gray;
                this.chkCSL.ForeColor = Color.Gray;
                this.chkCSL.Checked = false;
                this.chkCSL.Enabled = false;
                this.chkCSL.Text = "Target";
            }

            //LSL
            textView = Math.Round(_controlSpec.nXbar.lsl, 3).ToSafeString();
            if (textView != SpcLimit.MINTXT && textView != SpcLimit.MINTXT)
            {
                this.lblLSLValue.Text = textView;
                this.lblLSLValue.ForeColor = Color.Black;
                this.chkLSL.ForeColor = Color.Red;
                this.chkLSL.Checked = true;
                this.chkLSL.Enabled = true;
            }
            else
            {
                this.lblLSLValue.Text = SpcLimit.NONEDATA;
                this.lblLSLValue.ForeColor = Color.Gray;
                this.chkLSL.ForeColor = Color.Gray;
                this.chkLSL.Checked = false;
                this.chkLSL.Enabled = false;
            }

            //UOL
            textView = Math.Round(_controlSpec.nXbar.uol, 3).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblUOLValue.Text = textView;
                this.lblUOLValue.ForeColor = Color.Black;
                this.chkUOL.ForeColor = Color.Black;
                this.chkUOL.Checked = true;
                this.chkUOL.Enabled = true;
            }
            else
            {
                this.lblUOLValue.Text = SpcLimit.NONEDATA;
                this.lblUOLValue.ForeColor = Color.Gray;
                this.chkUOL.ForeColor = Color.Gray;
                this.chkUOL.Checked = false;
                this.chkUOL.Enabled = false;
            }

            //LOL
            textView = Math.Round(_controlSpec.nXbar.lol, 3).ToSafeString();
            if (textView != SpcLimit.MINTXT && textView != SpcLimit.MINTXT)
            {
                this.lblLOLValue.Text = textView;
                this.lblLOLValue.ForeColor = Color.Black;
                this.chkLOL.ForeColor = Color.Black;
                this.chkLOL.Checked = true;
                this.chkLOL.Enabled = true;
            }
            else
            {
                this.lblLOLValue.Text = SpcLimit.NONEDATA;
                this.lblLOLValue.ForeColor = Color.Gray;
                this.chkLOL.ForeColor = Color.Gray;
                this.chkLOL.Checked = false;
                this.chkLOL.Enabled = false;
            }

            //RUCL
            textView = Math.Round(_controlSpec.nR.ucl, 3).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblRUCLValue.Text = textView;
                this.lblRUCLValue.ForeColor = Color.Black;
                this.chkRUCL.ForeColor = Color.Blue;
                this.chkRUCL.Checked = true;
                this.chkRUCL.Enabled = true;
            }
            else
            {
                this.lblRUCLValue.Text = SpcLimit.NONEDATA;
                this.lblRUCLValue.ForeColor = Color.Gray;
                this.chkRUCL.ForeColor = Color.Gray;
                this.chkRUCL.Checked = false;
                this.chkRUCL.Enabled = false;
            }

            //RBAR or RCCL
            textView = Math.Round(_controlSpec.nR.XDRar, 3).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblRCCLValue.Text = textView;
                this.lblRCCLValue.ForeColor = Color.Black;
                this.chkRCCL.ForeColor = Color.Green;
                this.chkRCCL.Checked = true;
                this.chkRCCL.Enabled = true;
                this.chkRCCL.Text = _controlSpec.spcOption.chartName.subCCL;//R, S, PM
            }
            else
            {
                this.lblRCCLValue.Text = SpcLimit.NONEDATA;
                this.lblRCCLValue.ForeColor = Color.Gray;
                this.chkRCCL.ForeColor = Color.Gray;
                this.chkRCCL.Checked = false;
                this.chkRCCL.Enabled = false;
                this.chkRCCL.Text = "R";
            }
            //RLCL
            textView = Math.Round(_controlSpec.nR.lcl, 3).ToSafeString();
            if (textView != SpcLimit.MINTXT && textView != SpcLimit.MINTXT)
            {
                this.lblRLCLValue.Text = textView;
                this.lblRLCLValue.ForeColor = Color.Black;
                this.chkRLCL.ForeColor = Color.Blue;
                this.chkRLCL.Checked = true;
                this.chkRLCL.Enabled = true;
            }
            else
            {
                this.lblRLCLValue.Text = SpcLimit.NONEDATA;
                this.lblRLCLValue.ForeColor = Color.Gray;
                this.chkRLCL.ForeColor = Color.Gray;
                this.chkRLCL.Checked = false;
                this.chkRLCL.Enabled = false;
            }
            #endregion



            diagram.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.False;
            switch (_controlSpec.spcOption.chartType)
            {
                case ControlChartType.XBar_R:
                case ControlChartType.XBar_S:
                case ControlChartType.I_MR:
                case ControlChartType.Merger:
                    secondDiagPane.Visible = true;
                    break;
                case ControlChartType.np:
                case ControlChartType.p:
                case ControlChartType.c:
                case ControlChartType.u:
                    secondDiagPane.Visible = false;
                    break;
                default:
                    break;
            }


            //if (defaultDiagPane != null)
            //{
            //    secondDiagPane.
            //    defaultDiagPane.Title.Visibility = DefaultBoolean.True;
            //}

            #region Chart Option - Sigma Text View
            //Sigma 1
            textView = _controlSpec.sigmaResult.nSigma1.ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblSigma1Value.Text = textView;
                this.lblSigma1Value.ForeColor = Color.Black;
                this.chkSigma1.ForeColor = SpcViewOption.att.sigma1.checkBox.ForeColor;
                this.chkSigma1.Checked = false;
                this.chkSigma1.Enabled = true;
                //Strip01Sigma.
                diagram.AxisY.Strips["Strip01Sigma"].Visible = false;
                diagram.AxisY.Strips["Strip01Sigma"].MinLimit.AxisValue = 0;
                diagram.AxisY.Strips["Strip01Sigma"].MaxLimit.AxisValue = _controlSpec.sigmaResult.nSigma1Max;
                diagram.AxisY.Strips["Strip01Sigma"].MinLimit.AxisValue = _controlSpec.sigmaResult.nSigma1Min;
            }
            else
            {
                this.lblSigma1Value.Text = SpcLimit.NONEDATA;
                this.lblSigma1Value.ForeColor = Color.Gray;
                this.chkSigma1.ForeColor = Color.Gray;
                this.chkSigma1.Checked = false;
                this.chkSigma1.Enabled = false;
                diagram.AxisY.Strips["Strip01Sigma"].Visible = false;
            }

            //sia확인 : Chart에 Data Mapping, ControlChartDataMapping
            //Sigma 2
            textView = _controlSpec.sigmaResult.nSigma2.ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblSigma2Value.Text = textView;
                this.lblSigma2Value.ForeColor = Color.Black;
                this.chkSigma2.ForeColor = SpcViewOption.att.sigma2.checkBox.ForeColor;
                this.chkSigma2.Checked = false;
                this.chkSigma2.Enabled = true;
                //Strip02Sigma.
                diagram.AxisY.Strips["Strip02Sigma"].Visible = false;
                diagram.AxisY.Strips["Strip02Sigma"].MinLimit.AxisValue = 0;
                diagram.AxisY.Strips["Strip02Sigma"].MaxLimit.AxisValue = _controlSpec.sigmaResult.nSigma2Max;
                diagram.AxisY.Strips["Strip02Sigma"].MinLimit.AxisValue = _controlSpec.sigmaResult.nSigma2Min;
            }
            else
            {
                this.lblSigma2Value.Text = SpcLimit.NONEDATA;
                this.lblSigma2Value.ForeColor = Color.Gray;
                this.chkSigma2.ForeColor = Color.Gray;
                this.chkSigma2.Checked = false;
                this.chkSigma2.Enabled = false;
            }
            //Sigma 3
            textView = _controlSpec.sigmaResult.nSigma3.ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblSigma3Value.Text = textView;
                this.lblSigma3Value.ForeColor = Color.Black;
                this.chkSigma3.ForeColor = SpcViewOption.att.sigma3.checkBox.ForeColor;
                this.chkSigma3.Checked = false;
                this.chkSigma3.Enabled = true;
                //Strip03Sigma.
                diagram.AxisY.Strips["Strip03Sigma"].Visible = false;
                diagram.AxisY.Strips["Strip03Sigma"].MinLimit.AxisValue = 0;
                diagram.AxisY.Strips["Strip03Sigma"].MaxLimit.AxisValue = _controlSpec.sigmaResult.nSigma3Max;
                diagram.AxisY.Strips["Strip03Sigma"].MinLimit.AxisValue = _controlSpec.sigmaResult.nSigma3Min;
            }
            else
            {
                this.lblSigma3Value.Text = SpcLimit.NONEDATA;
                this.lblSigma3Value.ForeColor = Color.Gray;
                this.chkSigma3.ForeColor = Color.Gray;
                this.chkSigma3.Checked = false;
                this.chkSigma3.Enabled = false;
            }

            #endregion

            //XBAR
            if (_controlSpec.nXbar.XDBar != SpcLimit.MAX && _controlSpec.nXbar.XDBar != SpcLimit.MIN)
            {
                diagram.AxisY.ConstantLines["ConstantLineXBAR"].Title.Text = _controlSpec.spcOption.chartName.mainCSL;
                diagram.AxisY.ConstantLines["ConstantLineXBAR"].AxisValue = _controlSpec.nXbar.XDBar;
                diagram.AxisY.ConstantLines["ConstantLineXBAR"].Visible = true;
            }
            else
            {
                diagram.AxisY.ConstantLines["ConstantLineXBAR"].AxisValue = "";
                diagram.AxisY.ConstantLines["ConstantLineXBAR"].Visible = false;
            }

            //UCL
            if (_controlSpec.spcOption.limitType != LimitType.Interpretation)
            {
                _isInterpretation.UCL = false;
                //관리용.
                if (_controlSpec.nXbar.ucl != SpcLimit.MAX && _controlSpec.nXbar.ucl != SpcLimit.MIN)
                {
                    diagram.AxisY.ConstantLines["ConstantLineUCL"].AxisValue = _controlSpec.nXbar.ucl;
                    diagram.AxisY.ConstantLines["ConstantLineUCL"].Visible = true;
                }
                else
                {
                    diagram.AxisY.ConstantLines["ConstantLineUCL"].AxisValue = "";
                    diagram.AxisY.ConstantLines["ConstantLineUCL"].Visible = false;
                }
            }
            else
            {
                //해석용
                _isInterpretation.UCL = true;
                diagram.AxisY.ConstantLines["ConstantLineUCL"].AxisValue = "";
                diagram.AxisY.ConstantLines["ConstantLineUCL"].Visible = false;
            }

            //LCL
            if (_controlSpec.spcOption.limitType != LimitType.Interpretation)
            {
                _isInterpretation.LCL = false;
                //관리용.
                if (_controlSpec.nXbar.lcl != SpcLimit.MAX && _controlSpec.nXbar.lcl != SpcLimit.MIN)
                {
                    diagram.AxisY.ConstantLines["ConstantLineLCL"].AxisValue = _controlSpec.nXbar.lcl;
                    diagram.AxisY.ConstantLines["ConstantLineLCL"].Visible = true;
                }
                else
                {
                    diagram.AxisY.ConstantLines["ConstantLineLCL"].AxisValue = "";
                    diagram.AxisY.ConstantLines["ConstantLineLCL"].Visible = false;
                }
            }
            else
            {
                //해석용
                _isInterpretation.LCL = true;
                diagram.AxisY.ConstantLines["ConstantLineLCL"].AxisValue = "";
                diagram.AxisY.ConstantLines["ConstantLineLCL"].Visible = false;
            }

            //USL
            if (_controlSpec.nXbar.usl != SpcLimit.MAX && _controlSpec.nXbar.usl != SpcLimit.MIN)
            {
                diagram.AxisY.ConstantLines["ConstantLineUSL"].AxisValue = _controlSpec.nXbar.usl;
                diagram.AxisY.ConstantLines["ConstantLineUSL"].Visible = true;
            }
            else
            {
                diagram.AxisY.ConstantLines["ConstantLineUSL"].AxisValue = "";
                diagram.AxisY.ConstantLines["ConstantLineUSL"].Visible = false;
            }

            //Target or CSL 
            if (_controlSpec.nXbar.csl != SpcLimit.MAX && _controlSpec.nXbar.csl != SpcLimit.MIN)
            {
                diagram.AxisY.ConstantLines["ConstantLineTarget"].AxisValue = _controlSpec.nXbar.csl;
                diagram.AxisY.ConstantLines["ConstantLineTarget"].Visible = true;
            }
            else
            {
                diagram.AxisY.ConstantLines["ConstantLineUSL"].AxisValue = "";
                diagram.AxisY.ConstantLines["ConstantLineUSL"].Visible = false;
            }

            //LSL
            if (_controlSpec.nXbar.lsl != SpcLimit.MAX && _controlSpec.nXbar.lsl != SpcLimit.MIN)
            {
                diagram.AxisY.ConstantLines["ConstantLineLSL"].AxisValue = _controlSpec.nXbar.lsl;
                diagram.AxisY.ConstantLines["ConstantLineLSL"].Visible = true;
            }
            else
            {
                diagram.AxisY.ConstantLines["ConstantLineLSL"].AxisValue = "";
                diagram.AxisY.ConstantLines["ConstantLineLSL"].Visible = false;
            }

            //UOL
            if (_controlSpec.nXbar.uol != SpcLimit.MAX && _controlSpec.nXbar.uol != SpcLimit.MIN)
            {
                diagram.AxisY.ConstantLines["ConstantLineUOL"].AxisValue = _controlSpec.nXbar.uol;
                diagram.AxisY.ConstantLines["ConstantLineUOL"].Visible = true;
            }
            else
            {
                diagram.AxisY.ConstantLines["ConstantLineUOL"].AxisValue = "";
                diagram.AxisY.ConstantLines["ConstantLineUOL"].Visible = false;
            }

            //LOL
            if (_controlSpec.nXbar.lol != SpcLimit.MAX && _controlSpec.nXbar.lol != SpcLimit.MIN)
            {
                diagram.AxisY.ConstantLines["ConstantLineLOL"].AxisValue = _controlSpec.nXbar.lol;
                diagram.AxisY.ConstantLines["ConstantLineLOL"].Visible = true;
            }
            else
            {
                diagram.AxisY.ConstantLines["ConstantLineLOL"].AxisValue = "";
                diagram.AxisY.ConstantLines["ConstantLineLOL"].Visible = false;
            }

            //Pans 2 처리.
            if (secondDiagPane.Visible == true)
            {
                //RXBAR or RCCL
                if (_controlSpec.nR.XDRar != SpcLimit.MAX && _controlSpec.nR.XDRar != SpcLimit.MIN)
                {
                    diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRCCL"].Title.Text = _controlSpec.spcOption.chartName.subCCL;
                    diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRCCL"].AxisValue = _controlSpec.nR.XDRar;
                    diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRCCL"].Visible = true;
                }
                else
                {
                    diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRCCL"].AxisValue = "";
                    diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRCCL"].Visible = false;
                }

                //RUCL
                if (_controlSpec.spcOption.limitType != LimitType.Interpretation)
                {
                    _isInterpretation.RUCL = false;
                    //관리용.
                    if (_controlSpec.nR.ucl != SpcLimit.MAX && _controlSpec.nR.ucl != SpcLimit.MIN)
                    {
                        diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRUCL"].AxisValue = _controlSpec.nR.ucl;
                        diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRUCL"].Visible = true;
                    }
                    else
                    {
                        diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRUCL"].AxisValue = "";
                        diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRUCL"].Visible = false;
                    }
                }
                else
                {
                    //해석용
                    _isInterpretation.RUCL = true;
                    diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRUCL"].AxisValue = "";
                    diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRUCL"].Visible = false;
                }
                ////RCCL
                //if (_controlSpec.nR.ccl != SpcLimit.MAX && _controlSpec.nR.ccl != SpcLimit.MIN)
                //{
                //    diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRCCL"].Title.Text = _controlSpec.spcOption.chartName.subCCL;
                //    diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRCCL"].AxisValue = _controlSpec.nR.ccl;
                //    diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRCCL"].Visible = true;
                //}
                //else
                //{
                //    diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRCCL"].AxisValue = "";
                //    diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRCCL"].Visible = false;
                //}



                //RLCL
                if (_controlSpec.spcOption.limitType != LimitType.Interpretation)
                {
                    _isInterpretation.RLCL = false;
                    //관리용.

                    if (_controlSpec.nR.lcl != SpcLimit.MAX && _controlSpec.nR.lcl != SpcLimit.MIN)
                    {
                        diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRLCL"].AxisValue = _controlSpec.nR.lcl;
                        diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRLCL"].Visible = true;
                    }
                    else
                    {
                        diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRLCL"].AxisValue = "";
                        diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRLCL"].Visible = false;
                    }
                }
                else
                {
                    //해석용
                    _isInterpretation.RLCL = true;
                    diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRLCL"].AxisValue = "";
                    diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRLCL"].Visible = false;
                }

            }

            this.ChrPoltA.Refresh();
            Application.DoEvents();
            _isControlEventsStop = false;
        }

        /// <summary>
        /// 관리도 P, NP, C, U
        /// </summary>
        /// <param name="dtControlData"></param>
        /// <param name="controlSpec"></param>
        /// <param name="spcPara"></param>
        public void ControlChartDataMappingPCU()
        {
            //sia확인 : 02.관리도 Chart Data 입력 및 Mapping [ControlChartDataMappingPCU].
            SpcParameter xbarData = SpcParameter.Create();

            DataRow rowSpcData;
            double nXbarValue = SpcLimit.MAX;
            //double nXRValue = SpcLimit.MAX;
            xbarData.SpcData.Rows.Clear();
            if (_dtControlData != null)
            {
                foreach (DataRow r in _dtControlData)
                {
                    rowSpcData = xbarData.SpcData.NewRow();
                    //Xbar
                    rowSpcData["Label"] = r["SAMPLING"].ToSafeString();
                    rowSpcData["nValue"] = r["BAR"];
                    if (nXbarValue == SpcLimit.MAX)
                    {
                        nXbarValue = r["XBAR"].ToSafeDoubleStaMin();
                    }

                    rowSpcData["AUCL"] = r["UCL"];
                    rowSpcData["ALCL"] = r["LCL"];

                    //Console.Write(_controlSpec.nR.ccl);

                    xbarData.SpcData.Rows.Add(rowSpcData);
                }
            }
            else
            {
                return;
            }

            //해석용 구분
            _IsPaint = false;

            //OverRules Check
            //xbarData.SpecCheckXBar(_controlSpec, ref xbarData);

            this.ChrPoltA.DataSource = xbarData.SpcData;

            #region Chart Series Mapping
            // Create a series, and add it to the chart. 
            //Series series1 = new Series("My Series", ViewType.Bar);
            this.ChrPoltA.Series["Series01AUCL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series01AUCL"].ValueDataMembers.AddRange(new string[] { "AUCL" });
            this.ChrPoltA.Series["Series02ALCL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series02ALCL"].ValueDataMembers.AddRange(new string[] { "ALCL" });

            this.ChrPoltA.Series["Series03Value"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series03Value"].ValueDataMembers.AddRange(new string[] { "nValue" });

            //this.ChrXbarR.Series["Series11AUCL"].Visible = false;
            //this.ChrXbarR.Series["Series12ALCL"].Visible = false;
            //this.ChrXbarR.Series["Series13Value"].Visible = false;

            //UCL OverRules
            this.ChrPoltA.Series["Series04OverRuleUCL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series04OverRuleUCL"].ValueDataMembers.AddRange(new string[] { "nOverRuleUCL" });
            //LCL OverRules
            this.ChrPoltA.Series["Series05OverRuleLCL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series05OverRuleLCL"].ValueDataMembers.AddRange(new string[] { "nOverRuleLCL" });

            //USL OverRules
            this.ChrPoltA.Series["Series06OverRuleUSL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series06OverRuleUSL"].ValueDataMembers.AddRange(new string[] { "nOverRuleUSL" });
            //LSL OverRules
            this.ChrPoltA.Series["Series07OverRuleLSL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series07OverRuleLSL"].ValueDataMembers.AddRange(new string[] { "nOverRuleLSL" });

            //UOL OverRules
            this.ChrPoltA.Series["Series08OverRuleUOL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series08OverRuleUOL"].ValueDataMembers.AddRange(new string[] { "nOverRuleUOL" });
            //LOL OverRules
            this.ChrPoltA.Series["Series09OverRuleLOL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series09OverRuleLOL"].ValueDataMembers.AddRange(new string[] { "nOverRuleLOL" });

            ////RUCL OverRules
            //this.ChrXbarR.Series["Series14OverRuleRUCL"].Visible = false;
            ////RLCL OverRules
            //this.ChrXbarR.Series["Series15OverRuleRLCL"].Visible = false;

            #endregion

            pnlMain.Dock = DockStyle.Fill;

            #region Option - Text View (Chart)

            string textView = "";
            textView = Math.Round(_controlSpec.sigmaResult.cpkResult.nCP, 6).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblCp.ForeColor = Color.Black;
                this.lblCpValue.Text = textView;
                this.lblCpValue.ForeColor = Color.Black;
            }
            else
            {
                this.lblCp.ForeColor = Color.Gray;
                this.lblCpValue.Text = SpcLimit.NONEDATA;
                this.lblCpValue.ForeColor = Color.Gray;
            }

            textView = Math.Round(_controlSpec.sigmaResult.cpkResult.nCPK, 6).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblCpk.ForeColor = Color.Black;
                this.lblCpkValue.Text = textView;
                this.lblCpkValue.ForeColor = Color.Black;
            }
            else
            {
                this.lblCpk.ForeColor = Color.Gray;
                this.lblCpkValue.Text = SpcLimit.NONEDATA;
                this.lblCpkValue.ForeColor = Color.Gray;
            }

            textView = Math.Round(_controlSpec.sigmaResult.cpkResult.nPP, 6).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblPp.ForeColor = Color.Black;
                this.lblPpValue.Text = textView;
                this.lblPpValue.ForeColor = Color.Black;
            }
            else
            {
                this.lblPp.ForeColor = Color.Gray;
                this.lblPpValue.Text = SpcLimit.NONEDATA;
                this.lblPpValue.ForeColor = Color.Gray;
            }

            textView = Math.Round(_controlSpec.sigmaResult.cpkResult.nPPK, 6).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblPpk.ForeColor = Color.Black;
                this.lblPpkValue.Text = textView;
                this.lblPpkValue.ForeColor = Color.Black;
            }
            else
            {
                this.lblPpk.ForeColor = Color.Gray;
                this.lblPpkValue.Text = SpcLimit.NONEDATA;
                this.lblPpkValue.ForeColor = Color.Gray;
            }


            //UCL
            textView = Math.Round(_controlSpec.nXbar.ucl, 3).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblUCLValue.Text = textView;
                this.lblUCLValue.ForeColor = Color.Black;
                this.chkUCL.ForeColor = Color.Blue;
                this.chkUCL.Checked = true;
                this.chkUCL.Enabled = true;
            }
            else
            {
                this.lblUCLValue.Text = SpcLimit.NONEDATA;
                this.lblUCLValue.ForeColor = Color.Gray;
                this.chkUCL.ForeColor = Color.Gray;
                this.chkUCL.Checked = false;
                this.chkUCL.Enabled = false;
            }

            //CCL
            textView = Math.Round(_controlSpec.nXbar.ccl, 3).ToSafeString();
            if (textView != SpcLimit.MINTXT && textView != SpcLimit.MINTXT)
            {
                this.lblCCLValue.Text = textView;
                this.lblCCLValue.ForeColor = Color.Black;
                this.chkCCL.ForeColor = Color.Blue;
                this.chkCCL.Checked = true;
                this.chkCCL.Enabled = true;
            }
            else
            {
                this.lblCCLValue.Text = SpcLimit.NONEDATA;
                this.lblCCLValue.ForeColor = Color.Gray;
                this.chkCCL.ForeColor = Color.Gray;
                this.chkCCL.Checked = false;
                this.chkCCL.Enabled = false;
            }

            //LCL
            textView = Math.Round(_controlSpec.nXbar.lcl, 3).ToSafeString();
            if (textView != SpcLimit.MINTXT && textView != SpcLimit.MINTXT)
            {
                this.lblLCLValue.Text = textView;
                this.lblLCLValue.ForeColor = Color.Black;
                this.chkLCL.ForeColor = Color.Blue;
                this.chkLCL.Checked = true;
                this.chkLCL.Enabled = true;
            }
            else
            {
                this.lblLCLValue.Text = SpcLimit.NONEDATA;
                this.lblLCLValue.ForeColor = Color.Gray;
                this.chkLCL.ForeColor = Color.Gray;
                this.chkLCL.Checked = false;
                this.chkLCL.Enabled = false;
            }

            //USL
            textView = Math.Round(_controlSpec.nXbar.usl, 3).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblUSLValue.Text = textView;
                this.lblUSLValue.ForeColor = Color.Black;
                this.chkUSL.ForeColor = Color.Red;
                this.chkUSL.Checked = true;
                this.chkUSL.Enabled = true;
            }
            else
            {
                this.lblUSLValue.Text = SpcLimit.NONEDATA;
                this.lblUSLValue.ForeColor = Color.Gray;
                this.chkUSL.ForeColor = Color.Gray;
                this.chkUSL.Checked = false;
                this.chkUSL.Enabled = false;
            }
            //CSL
            textView = Math.Round(nXbarValue, 3).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblCSLValue.Text = textView;
                this.lblCSLValue.ForeColor = Color.Black;
                this.chkCSL.ForeColor = Color.Green;
                this.chkCSL.Checked = true;
                this.chkCSL.Enabled = true;
                this.chkCSL.Text = _controlSpec.spcOption.chartName.mainCSL;//P, NP, C, U
            }
            else
            {
                this.lblCSLValue.Text = SpcLimit.NONEDATA;
                this.lblCSLValue.ForeColor = Color.Gray;
                this.chkCSL.ForeColor = Color.Gray;
                this.chkCSL.Checked = false;
                this.chkCSL.Enabled = false;
                this.chkCSL.Text = _controlSpec.spcOption.chartName.mainCSL;
            }
            //LSL
            textView = Math.Round(_controlSpec.nXbar.lsl, 3).ToSafeString();
            if (textView != SpcLimit.MINTXT && textView != SpcLimit.MINTXT)
            {
                this.lblLSLValue.Text = textView;
                this.lblLSLValue.ForeColor = Color.Black;
                this.chkLSL.ForeColor = Color.Red;
                this.chkLSL.Checked = true;
                this.chkLSL.Enabled = true;
            }
            else
            {
                this.lblLSLValue.Text = SpcLimit.NONEDATA;
                this.lblLSLValue.ForeColor = Color.Gray;
                this.chkLSL.ForeColor = Color.Gray;
                this.chkLSL.Checked = false;
                this.chkLSL.Enabled = false;
            }

            //UOL
            textView = Math.Round(_controlSpec.nXbar.uol, 3).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblUOLValue.Text = textView;
                this.lblUOLValue.ForeColor = Color.Black;
                this.chkUOL.ForeColor = Color.Black;
                this.chkUOL.Checked = true;
                this.chkUOL.Enabled = true;
            }
            else
            {
                this.lblUOLValue.Text = SpcLimit.NONEDATA;
                this.lblUOLValue.ForeColor = Color.Gray;
                this.chkUOL.ForeColor = Color.Gray;
                this.chkUOL.Checked = false;
                this.chkUOL.Enabled = false;
            }

            //LOL
            textView = Math.Round(_controlSpec.nXbar.lol, 3).ToSafeString();
            if (textView != SpcLimit.MINTXT && textView != SpcLimit.MINTXT)
            {
                this.lblLOLValue.Text = textView;
                this.lblLOLValue.ForeColor = Color.Black;
                this.chkLOL.ForeColor = Color.Black;
                this.chkLOL.Checked = true;
                this.chkLOL.Enabled = true;
            }
            else
            {
                this.lblLOLValue.Text = SpcLimit.NONEDATA;
                this.lblLOLValue.ForeColor = Color.Gray;
                this.chkLOL.ForeColor = Color.Gray;
                this.chkLOL.Checked = false;
                this.chkLOL.Enabled = false;
            }

            //RUCL
            textView = Math.Round(_controlSpec.nR.ucl, 3).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblRUCLValue.Text = textView;
                this.lblRUCLValue.ForeColor = Color.Black;
                this.chkRUCL.ForeColor = Color.Blue;
                this.chkRUCL.Checked = true;
                this.chkRUCL.Enabled = true;
            }
            else
            {
                this.lblRUCLValue.Text = SpcLimit.NONEDATA;
                this.lblRUCLValue.ForeColor = Color.Gray;
                this.chkRUCL.ForeColor = Color.Gray;
                this.chkRUCL.Checked = false;
                this.chkRUCL.Enabled = false;
            }
            //RCCL
            textView = Math.Round(_controlSpec.nR.ccl, 3).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblRCCLValue.Text = textView;
                this.lblRCCLValue.ForeColor = Color.Black;
                this.chkRCCL.ForeColor = Color.Green;
                this.chkRCCL.Checked = true;
                this.chkRCCL.Enabled = true;
                this.chkRCCL.Text = "R";
            }
            else
            {
                this.lblRCCLValue.Text = SpcLimit.NONEDATA;
                this.lblRCCLValue.ForeColor = Color.Gray;
                this.chkRCCL.ForeColor = Color.Gray;
                this.chkRCCL.Checked = false;
                this.chkRCCL.Enabled = false;
                this.chkRCCL.Text = "R";
            }
            //RLCL
            textView = Math.Round(_controlSpec.nR.lcl, 3).ToSafeString();
            if (textView != SpcLimit.MINTXT && textView != SpcLimit.MINTXT)
            {
                this.lblRLCLValue.Text = textView;
                this.lblRLCLValue.ForeColor = Color.Black;
                this.chkRLCL.ForeColor = Color.Blue;
                this.chkRLCL.Checked = true;
                this.chkRLCL.Enabled = true;
            }
            else
            {
                this.lblRLCLValue.Text = SpcLimit.NONEDATA;
                this.lblRLCLValue.ForeColor = Color.Gray;
                this.chkRLCL.ForeColor = Color.Gray;
                this.chkRLCL.Checked = false;
                this.chkRLCL.Enabled = false;
            }
            #endregion

            XYDiagram diagram = (XYDiagram)ChrPoltA.Diagram;
            XYDiagramDefaultPane defaultDiagPane = diagram.DefaultPane;
            XYDiagramPane secondDiagPane = diagram.Panes["SecondPane"];

            diagram.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.True;
            switch (_controlSpec.spcOption.chartType)
            {
                case ControlChartType.XBar_R:
                case ControlChartType.XBar_S:
                case ControlChartType.I_MR:
                case ControlChartType.Merger:
                    secondDiagPane.Visible = true;
                    break;
                case ControlChartType.np:
                case ControlChartType.p:
                case ControlChartType.c:
                case ControlChartType.u:
                    secondDiagPane.Visible = false;
                    break;
                default:
                    break;
            }


            //if (defaultDiagPane != null)
            //{
            //    secondDiagPane.
            //    defaultDiagPane.Title.Visibility = DefaultBoolean.True;
            //}

            #region Chart Option - Sigma Text View
            //Sigma 1
            textView = _controlSpec.sigmaResult.nSigma1.ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblSigma1Value.Text = textView;
                this.lblSigma1Value.ForeColor = Color.Black;
                this.chkSigma1.ForeColor = SpcViewOption.att.sigma1.checkBox.ForeColor;
                this.chkSigma1.Checked = false;
                this.chkSigma1.Enabled = true;
                //Strip01Sigma.
                diagram.AxisY.Strips["Strip01Sigma"].Visible = false;
                diagram.AxisY.Strips["Strip01Sigma"].MinLimit.AxisValue = 0;
                diagram.AxisY.Strips["Strip01Sigma"].MaxLimit.AxisValue = _controlSpec.sigmaResult.nSigma1Max;
                diagram.AxisY.Strips["Strip01Sigma"].MinLimit.AxisValue = _controlSpec.sigmaResult.nSigma1Min;
            }
            else
            {
                this.lblSigma1Value.Text = SpcLimit.NONEDATA;
                this.lblSigma1Value.ForeColor = Color.Gray;
                this.chkSigma1.ForeColor = Color.Gray;
                this.chkSigma1.Checked = false;
                this.chkSigma1.Enabled = false;
                diagram.AxisY.Strips["Strip01Sigma"].Visible = false;
            }

            //sia확인 : Chart에 Data Mapping, ControlChartDataMapping
            //Sigma 2
            textView = _controlSpec.sigmaResult.nSigma2.ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblSigma2Value.Text = textView;
                this.lblSigma2Value.ForeColor = Color.Black;
                this.chkSigma2.ForeColor = SpcViewOption.att.sigma2.checkBox.ForeColor;
                this.chkSigma2.Checked = false;
                this.chkSigma2.Enabled = true;
                //Strip02Sigma.
                diagram.AxisY.Strips["Strip02Sigma"].Visible = false;
                diagram.AxisY.Strips["Strip02Sigma"].MinLimit.AxisValue = 0;
                diagram.AxisY.Strips["Strip02Sigma"].MaxLimit.AxisValue = _controlSpec.sigmaResult.nSigma2Max;
                diagram.AxisY.Strips["Strip02Sigma"].MinLimit.AxisValue = _controlSpec.sigmaResult.nSigma2Min;
            }
            else
            {
                this.lblSigma2Value.Text = SpcLimit.NONEDATA;
                this.lblSigma2Value.ForeColor = Color.Gray;
                this.chkSigma2.ForeColor = Color.Gray;
                this.chkSigma2.Checked = false;
                this.chkSigma2.Enabled = false;
            }
            //Sigma 3
            textView = _controlSpec.sigmaResult.nSigma3.ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblSigma3Value.Text = textView;
                this.lblSigma3Value.ForeColor = Color.Black;
                this.chkSigma3.ForeColor = SpcViewOption.att.sigma3.checkBox.ForeColor;
                this.chkSigma3.Checked = false;
                this.chkSigma3.Enabled = true;
                //Strip03Sigma.
                diagram.AxisY.Strips["Strip03Sigma"].Visible = false;
                diagram.AxisY.Strips["Strip03Sigma"].MinLimit.AxisValue = 0;
                diagram.AxisY.Strips["Strip03Sigma"].MaxLimit.AxisValue = _controlSpec.sigmaResult.nSigma3Max;
                diagram.AxisY.Strips["Strip03Sigma"].MinLimit.AxisValue = _controlSpec.sigmaResult.nSigma3Min;
            }
            else
            {
                this.lblSigma3Value.Text = SpcLimit.NONEDATA;
                this.lblSigma3Value.ForeColor = Color.Gray;
                this.chkSigma3.ForeColor = Color.Gray;
                this.chkSigma3.Checked = false;
                this.chkSigma3.Enabled = false;
            }

            #endregion

            //nXbarValue
            if (nXbarValue != SpcLimit.MAX && nXbarValue != SpcLimit.MAX)
            {
                diagram.AxisY.ConstantLines["ConstantLineXBAR"].Title.Text = _controlSpec.spcOption.chartName.mainCSL;
                diagram.AxisY.ConstantLines["ConstantLineXBAR"].AxisValue = nXbarValue;
                diagram.AxisY.ConstantLines["ConstantLineXBAR"].Visible = true;
            }
            else
            {
                diagram.AxisY.ConstantLines["ConstantLineXBAR"].AxisValue = "";
                diagram.AxisY.ConstantLines["ConstantLineXBAR"].Visible = false;
            }

            //UCL
            if (_controlSpec.nXbar.ucl != SpcLimit.MAX && _controlSpec.nXbar.ucl != SpcLimit.MAX)
            {
                diagram.AxisY.ConstantLines["ConstantLineUCL"].AxisValue = _controlSpec.nXbar.ucl;
                diagram.AxisY.ConstantLines["ConstantLineUCL"].Visible = true;
            }
            else
            {
                diagram.AxisY.ConstantLines["ConstantLineUCL"].AxisValue = "";
                diagram.AxisY.ConstantLines["ConstantLineUCL"].Visible = false;
            }

            //LCL
            if (_controlSpec.nXbar.lcl != SpcLimit.MAX && _controlSpec.nXbar.lcl != SpcLimit.MAX)
            {
                diagram.AxisY.ConstantLines["ConstantLineLCL"].AxisValue = _controlSpec.nXbar.lcl;
                diagram.AxisY.ConstantLines["ConstantLineLCL"].Visible = true;
            }
            else
            {
                diagram.AxisY.ConstantLines["ConstantLineLCL"].AxisValue = "";
                diagram.AxisY.ConstantLines["ConstantLineLCL"].Visible = false;
            }

            //USL
            if (_controlSpec.nXbar.usl != SpcLimit.MAX && _controlSpec.nXbar.usl != SpcLimit.MAX)
            {
                diagram.AxisY.ConstantLines["ConstantLineUSL"].AxisValue = _controlSpec.nXbar.usl;
                diagram.AxisY.ConstantLines["ConstantLineUSL"].Visible = true;
            }
            else
            {
                diagram.AxisY.ConstantLines["ConstantLineUSL"].AxisValue = "";
                diagram.AxisY.ConstantLines["ConstantLineUSL"].Visible = false;
            }

            //LSL
            if (_controlSpec.nXbar.lsl != SpcLimit.MAX && _controlSpec.nXbar.lsl != SpcLimit.MAX)
            {
                diagram.AxisY.ConstantLines["ConstantLineLSL"].AxisValue = _controlSpec.nXbar.lsl;
                diagram.AxisY.ConstantLines["ConstantLineLSL"].Visible = true;
            }
            else
            {
                diagram.AxisY.ConstantLines["ConstantLineLSL"].AxisValue = "";
                diagram.AxisY.ConstantLines["ConstantLineLSL"].Visible = false;
            }

            //UOL
            if (_controlSpec.nXbar.uol != SpcLimit.MAX && _controlSpec.nXbar.uol != SpcLimit.MAX)
            {
                diagram.AxisY.ConstantLines["ConstantLineUOL"].AxisValue = _controlSpec.nXbar.uol;
                diagram.AxisY.ConstantLines["ConstantLineUOL"].Visible = true;
            }
            else
            {
                diagram.AxisY.ConstantLines["ConstantLineUOL"].AxisValue = "";
                diagram.AxisY.ConstantLines["ConstantLineUOL"].Visible = false;
            }

            //LOL
            if (_controlSpec.nXbar.lol != SpcLimit.MAX && _controlSpec.nXbar.lol != SpcLimit.MAX)
            {
                diagram.AxisY.ConstantLines["ConstantLineLOL"].AxisValue = _controlSpec.nXbar.lol;
                diagram.AxisY.ConstantLines["ConstantLineLOL"].Visible = true;
            }
            else
            {
                diagram.AxisY.ConstantLines["ConstantLineLOL"].AxisValue = "";
                diagram.AxisY.ConstantLines["ConstantLineLOL"].Visible = false;
            }

            //Pans 2 처리.
            if (secondDiagPane.Visible == true)
            {
                //RUCL
                if (_controlSpec.nR.ucl != SpcLimit.MAX && _controlSpec.nR.ucl != SpcLimit.MAX)
                {
                    diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRUCL"].AxisValue = _controlSpec.nR.ucl;
                    diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRUCL"].Visible = true;
                }
                else
                {
                    diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRUCL"].AxisValue = "";
                    diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRUCL"].Visible = false;
                }

                //RCCL
                if (_controlSpec.nR.ccl != SpcLimit.MAX && _controlSpec.nR.ccl != SpcLimit.MAX)
                {
                    diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRCCL"].AxisValue = _controlSpec.nR.ccl;
                    diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRCCL"].Visible = true;
                }
                else
                {
                    diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRCCL"].AxisValue = "";
                    diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRCCL"].Visible = false;
                }

                //RLCL
                if (_controlSpec.nR.lcl != SpcLimit.MAX && _controlSpec.nR.lcl != SpcLimit.MAX)
                {
                    diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRLCL"].AxisValue = _controlSpec.nR.lcl;
                    diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRLCL"].Visible = true;
                }
                else
                {
                    diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRLCL"].AxisValue = "";
                    diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRLCL"].Visible = false;
                }
            }

            this.DetailChartControlClearPCU(LayoutVisibility.Never);
        }

        /// <summary>
        /// Chart Control 초기화
        /// </summary>
        public void ControlChartDataMappingClear()
        {
            this.pnlTop.Visible = true;
            this.pnlRight.Visible = false;
            this.pnlRightTop.Visible = false;
            this.layoutItemScrollOption.Visibility = LayoutVisibility.Never;
            
            _spcPara = null;
            SpcParameter xbarData = SpcParameter.Create();
            ControlSpec controlSpec = new ControlSpec();

            ControlChartSerialCaption serialCaption = null;
            //serialCaption = controlSpec.spcOption.chartName.SerialCaption;
            //serialCaption = SpcFunction.ControlChartToolsNameSetting(serialCaption);

            xbarData.SpcData = xbarData.ClearDataSpcXBar(out controlSpec);
            this.ChrPoltA.DataSource = xbarData.SpcData;

            this.ChrPoltA.Legends["LegendPane01"].Title.TextColor = Color.Gray;
            this.ChrPoltA.Legends["LegendPane02"].Title.TextColor = Color.Gray;

            this.ChrPoltA.RuntimeHitTesting = true;

            string forAnalysisMessage = SpcLibMessage.common.comCpk1031;//해석용
            string overRuleMessage = SpcLibMessage.common.comCpk1032;//Over Rule
            string avgValueMessage = SpcLibMessage.common.comCpk1033;//값(Value)

            // Create a series, and add it to the chart. 
            //Series series1 = new Series("My Series", ViewType.Bar);
            this.ChrPoltA.Series["Series01AUCL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series01AUCL"].ValueDataMembers.AddRange(new string[] { "AUCL" });
            this.ChrPoltA.Series["Series01AUCL"].CrosshairLabelPattern = forAnalysisMessage + " UCL : {V:F3}";
            //this.ChrPoltA.Series["Series01AUCL"].Visible = false;
            //this.ChrXbarR.Series["Series01AUCL"].ValueDataMembersSerializable = "Min;Max";

            this.ChrPoltA.Series["Series02ALCL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series02ALCL"].ValueDataMembers.AddRange(new string[] { "ALCL" });
            this.ChrPoltA.Series["Series02ALCL"].CrosshairLabelPattern = forAnalysisMessage + " LCL : {V:F3}";
            this.ChrPoltA.Series["Series02ALCL"].Visible = false;

            //OverlappedRangeBarSeriesView overlappedRangeBarSeriesView1 = new OverlappedRangeBarSeriesView();
            // RangeBarSeriesLabel rangeValueSeries03Value = new RangeBarSeriesLabel();
            //rangeBarSeriesLabel2.Indent = 2;
            //rangeValueSeries03Value.TextPattern = "{V:F2}";

            //sia필수확인 : Series Type별 Point Format 설정.
            this.ChrPoltA.Series["Series03Value"].ArgumentDataMember = "Label";
            //this.ChrPoltA.Series["Series03Value"].ValueDataMembers.AddRange(new string[] { "nValue" });
            //this.ChrPoltA.Series["Series03Value"].Visible = false;
            //this.ChrPoltA.Series["Series03Value"].CrosshairLabelPattern = "Value : {V:F3}";
            //this.ChrXbarR.Series["Series03Value"].CrosshairLabelPattern = "Value : {V:F3}\n해석용 LSL : {V:F3}";
            //this.ChrXbarR.Series["Series03Value"].ValueDataMembersSerializable = "nValue;ALCL";
            //this.ChrXbarR.Series["Series03Value"].View = overlappedRangeBarSeriesView1;
            //this.ChrXbarR.Series["Series03Value"].ValueDataMembersSerializable = "nValue";

            this.ChrPoltA.Series["Series11AUCL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series11AUCL"].ValueDataMembers.AddRange(new string[] { "RAUCL" });
            this.ChrPoltA.Series["Series11AUCL"].CrosshairLabelPattern = forAnalysisMessage + " UCL : {V:F3}";
            this.ChrPoltA.Series["Series11AUCL"].Visible = false;
            
            this.ChrPoltA.Series["Series12ALCL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series12ALCL"].ValueDataMembers.AddRange(new string[] { "RALCL" });
            this.ChrPoltA.Series["Series12ALCL"].CrosshairLabelPattern = forAnalysisMessage + " LCL : {V:F3}";
            this.ChrPoltA.Series["Series12ALCL"].Visible = false;

            this.ChrPoltA.Series["Series13Value"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series13Value"].ValueDataMembers.AddRange(new string[] { "nRValue" });
            this.ChrPoltA.Series["Series13Value"].CrosshairLabelPattern = avgValueMessage + " : {V:F3}";
            this.ChrPoltA.Series["Series13Value"].Visible = false;

            //LSL OverRules
            this.ChrPoltA.Series["Series04OverRuleUCL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series04OverRuleUCL"].ValueDataMembers.AddRange(new string[] { "nOverRuleUCL" });
            this.ChrPoltA.Series["Series04OverRuleUCL"].CrosshairLabelPattern = overRuleMessage +" UCL : {V:F3}";
            this.ChrPoltA.Series["Series04OverRuleUCL"].Visible = false;

            this.ChrPoltA.Series["Series05OverRuleLCL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series05OverRuleLCL"].ValueDataMembers.AddRange(new string[] { "nOverRuleLCL" });
            this.ChrPoltA.Series["Series05OverRuleLCL"].CrosshairLabelPattern = overRuleMessage + " LCL : {V:F3}";
            this.ChrPoltA.Series["Series05OverRuleLCL"].Visible = false;

            //USL OverRules
            this.ChrPoltA.Series["Series06OverRuleUSL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series06OverRuleUSL"].ValueDataMembers.AddRange(new string[] { "nOverRuleUSL" });
            this.ChrPoltA.Series["Series06OverRuleUSL"].CrosshairLabelPattern = overRuleMessage + " USL : {V:F3}";
            this.ChrPoltA.Series["Series06OverRuleUSL"].Visible = false;

            this.ChrPoltA.Series["Series07OverRuleLSL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series07OverRuleLSL"].ValueDataMembers.AddRange(new string[] { "nOverRuleLSL" });
            this.ChrPoltA.Series["Series07OverRuleLSL"].CrosshairLabelPattern = overRuleMessage + " LSL : {V:F3}";
            this.ChrPoltA.Series["Series07OverRuleLSL"].Visible = false;


            //USL OverRules
            this.ChrPoltA.Series["Series08OverRuleUOL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series08OverRuleUOL"].ValueDataMembers.AddRange(new string[] { "nOverRuleUOL" });
            this.ChrPoltA.Series["Series08OverRuleUOL"].CrosshairLabelPattern = overRuleMessage + " UOL : {V:F3}";
            this.ChrPoltA.Series["Series08OverRuleUOL"].Visible = false;

            this.ChrPoltA.Series["Series09OverRuleLOL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series09OverRuleLOL"].ValueDataMembers.AddRange(new string[] { "nOverRuleLOL" });
            this.ChrPoltA.Series["Series09OverRuleLOL"].CrosshairLabelPattern = overRuleMessage + " LOL : {V:F3}";
            this.ChrPoltA.Series["Series09OverRuleLOL"].Visible = false;

            //LSL OverRules
            this.ChrPoltA.Series["Series14OverRuleRUCL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series14OverRuleRUCL"].ValueDataMembers.AddRange(new string[] { "nROverRuleUCL" });
            this.ChrPoltA.Series["Series14OverRuleRUCL"].CrosshairLabelPattern = overRuleMessage + " UCL : {V:F3}";
            this.ChrPoltA.Series["Series14OverRuleRUCL"].Visible = false;

            this.ChrPoltA.Series["Series15OverRuleRLCL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series15OverRuleRLCL"].ValueDataMembers.AddRange(new string[] { "nROverRuleLCL" });
            this.ChrPoltA.Series["Series15OverRuleRLCL"].CrosshairLabelPattern = overRuleMessage + " LCL : {V:F3}";
            this.ChrPoltA.Series["Series15OverRuleRLCL"].Visible = false;

            pnlMain.Dock = DockStyle.Fill;

            //공정능력 값
            this.lblCp.ForeColor = Color.Gray;
            this.lblCpk.ForeColor = Color.Gray;
            this.lblPp.ForeColor = Color.Gray;
            this.lblPpk.ForeColor = Color.Gray;

            this.lblCpValue.Text = SpcLimit.NONEDATA;
            this.lblCpkValue.Text = SpcLimit.NONEDATA;
            this.lblPpValue.Text = SpcLimit.NONEDATA;
            this.lblPpkValue.Text = SpcLimit.NONEDATA;
            this.lblCpValue.ForeColor = Color.Gray;
            this.lblCpkValue.ForeColor = Color.Gray;
            this.lblPpValue.ForeColor = Color.Gray;
            this.lblPpkValue.ForeColor = Color.Gray;

            this.chkSigma3.Enabled = false;
            this.chkSigma2.Enabled = false;
            this.chkSigma1.Enabled = false;

            this.chkSigma3.ForeColor = Color.Gray;
            this.chkSigma2.ForeColor = Color.Gray;
            this.chkSigma1.ForeColor = Color.Gray;

            this.lblSigma3Value.Text = SpcLimit.NONEDATA;
            this.lblSigma2Value.Text = SpcLimit.NONEDATA;
            this.lblSigma1Value.Text = SpcLimit.NONEDATA;

            this.lblSigma3Value.ForeColor = Color.Gray;
            this.lblSigma2Value.ForeColor = Color.Gray;
            this.lblSigma1Value.ForeColor = Color.Gray;

            //Chart Option
            //Spec
            this.lblUSLValue.Text = SpcLimit.NONEDATA;
            this.lblCSLValue.Text = SpcLimit.NONEDATA;
            this.lblLSLValue.Text = SpcLimit.NONEDATA;
            this.lblUSLValue.ForeColor = Color.Gray;
            this.lblCSLValue.ForeColor = Color.Gray;
            this.lblLSLValue.ForeColor = Color.Gray;

            this.chkCSL.Text = "XBAR";
            this.chkUSL.Enabled = false;
            this.chkCSL.Enabled = false;
            this.chkLSL.Enabled = false;
            this.chkUSL.ForeColor = Color.Gray;
            this.chkCSL.ForeColor = Color.Gray;
            this.chkLSL.ForeColor = Color.Gray;
            this.chkUSL.Checked = false;
            this.chkCSL.Checked = false;
            this.chkLSL.Checked = false;

            this.chkUCL.Enabled = false;
            this.chkCCL.Enabled = false;
            this.chkLCL.Enabled = false;
            this.chkRUCL.Enabled = false;
            this.chkRCCL.Enabled = false;
            this.chkRLCL.Enabled = false;

            this.chkUCL.ForeColor = Color.Gray;
            this.chkCCL.ForeColor = Color.Gray;
            this.chkLCL.ForeColor = Color.Gray;
            this.chkRUCL.ForeColor = Color.Gray;
            this.chkRCCL.ForeColor = Color.Gray;
            this.chkRLCL.ForeColor = Color.Gray;

            this.chkUCL.Enabled = false;
            this.chkCCL.Enabled = false;
            this.chkLCL.Enabled = false;
            this.chkRUCL.Enabled = false;
            this.chkRCCL.Enabled = false;
            this.chkRLCL.Enabled = false;

            //Control Value
            this.lblUCLValue.Text = SpcLimit.NONEDATA;
            this.lblCCLValue.Text = SpcLimit.NONEDATA;
            this.lblLCLValue.Text = SpcLimit.NONEDATA;

            this.lblUCLValue.ForeColor = Color.Gray;
            this.lblCCLValue.ForeColor = Color.Gray;
            this.lblLCLValue.ForeColor = Color.Gray;

            //Outlier
            this.chkUOL.Enabled = false;
            this.chkLOL.Enabled = false;
            this.chkUOL.ForeColor = Color.Gray;
            this.chkLOL.ForeColor = Color.Gray;
            this.lblUOLValue.Text = SpcLimit.NONEDATA;
            this.lblLOLValue.Text = SpcLimit.NONEDATA;
            this.lblUOLValue.ForeColor = Color.Gray;
            this.lblLOLValue.ForeColor = Color.Gray;

            //RS
            this.chkRCCL.Text = "R";
            this.chkRCCL.ForeColor = Color.Gray;
            this.lblRLCLValue.Text = SpcLimit.NONEDATA;
            this.lblRCCLValue.Text = SpcLimit.NONEDATA;
            this.lblRUCLValue.Text = SpcLimit.NONEDATA;
            this.lblRLCLValue.ForeColor = Color.Gray;
            this.lblRCCLValue.ForeColor = Color.Gray;
            this.lblRUCLValue.ForeColor = Color.Gray;

            XYDiagram diagram = (XYDiagram)ChrPoltA.Diagram;
            XYDiagramPane secondDiagPane = diagram.Panes["SecondPane"];

            //this.ChrPoltA.Series["Series03Value"].Label.TextPattern = "Value {A}:{V:#0.000}";
            //this.ChrPoltA.Series["Series06OverRuleUSL"].Label.TextPattern = "Over Rule USL {A}:{V:#0.000}";
            //this.ChrPoltA.Series["Series07OverRuleLSL"].Label.TextPattern = "Over Rule LSL {A}:{V:#0.000}";


            //XYDiagram diagram = (XYDiagram)ChrXbarR.Diagram;
            diagram.AxisY.ConstantLines["ConstantLineUCL"].AxisValue = controlSpec.nXbar.ucl;
            diagram.AxisY.ConstantLines["ConstantLineLCL"].AxisValue = controlSpec.nXbar.lcl;

            diagram.AxisY.ConstantLines["ConstantLineUSL"].AxisValue = controlSpec.nXbar.usl;
            diagram.AxisY.ConstantLines["ConstantLineTarget"].AxisValue = controlSpec.nXbar.csl;
            diagram.AxisY.ConstantLines["ConstantLineXBAR"].AxisValue = controlSpec.nXbar.XBar;
            diagram.AxisY.ConstantLines["ConstantLineLSL"].AxisValue = controlSpec.nXbar.lsl;

            diagram.AxisY.ConstantLines["ConstantLineUOL"].AxisValue = controlSpec.nXbar.uol;
            diagram.AxisY.ConstantLines["ConstantLineLOL"].AxisValue = controlSpec.nXbar.lol;

            diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRUCL"].AxisValue = controlSpec.nR.ucl;
            diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRCCL"].AxisValue = controlSpec.nR.ccl;
            diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRLCL"].AxisValue = controlSpec.nR.lcl;
        }

        /// <summary>
        /// 상세 Popup Chart 폼 사용시 미사용 항목 Visible
        /// </summary>
        /// <param name="value"></param>
        public void DetialChartControlClear(LayoutVisibility value = LayoutVisibility.Never)
        {
            this.layoutItemTemp01.Visibility = value;
            this.layoutItemTemp02.Visibility = value;
            this.layoutItemTemp03.Visibility = value;
            this.layoutItemTemp04.Visibility = value;
            this.layoutItemTemp01Value.Visibility = value;
            this.layoutItemTemp02Value.Visibility = value;
            this.layoutItemTemp03Value.Visibility = value;
            this.layoutItemTemp04Value.Visibility = value;
            this.layoutItemScrollOption.Visibility = value;
        }
        /// <summary>
        /// Chart Legends Title 변경.
        /// </summary>
        /// <param name="value"></param>
        public void ChartLegendsTitleChange(string chartTitle)
        {
            this.ControlChartDataMappingClear();

            this.ChrPoltA.Legends["LegendPane01"].Title.TextColor = Color.Black;
            this.ChrPoltA.Legends["LegendPane02"].Title.TextColor = Color.Black;

            switch (chartTitle.ToUpper())
            {
                case "XBARR":
                case "R":
                    this.ChrPoltA.Legends["LegendPane01"].Title.Text = "XBAR";
                    this.ChrPoltA.Legends["LegendPane02"].Title.Text = "R";
                    break;
                case "XBARS":
                case "S":
                    this.ChrPoltA.Legends["LegendPane01"].Title.Text = "XBAR";
                    this.ChrPoltA.Legends["LegendPane02"].Title.Text = "S";
                    break;
                case "XBARP":
                case "MERGER":
                case "PM":
                    this.ChrPoltA.Legends["LegendPane01"].Title.Text = "XBAR";
                    this.ChrPoltA.Legends["LegendPane02"].Title.Text = "PM";
                    break;
                case "IMR":
                case "I":
                    this.ChrPoltA.Legends["LegendPane01"].Title.Text = "I";
                    this.ChrPoltA.Legends["LegendPane02"].Title.Text = "MR";
                    break;
                case "P":
                    this.ChrPoltA.Legends["LegendPane01"].Title.Text = "P";
                    this.ChrPoltA.Legends["LegendPane02"].Title.Text = "";
                    break;
                case "NP":
                    this.ChrPoltA.Legends["LegendPane01"].Title.Text = "NP";
                    this.ChrPoltA.Legends["LegendPane02"].Title.Text = "";
                    break;
                case "C":
                    this.ChrPoltA.Legends["LegendPane01"].Title.Text = "C";
                    this.ChrPoltA.Legends["LegendPane02"].Title.Text = "";
                    break;
                case "U":
                    this.ChrPoltA.Legends["LegendPane01"].Title.Text = "U";
                    this.ChrPoltA.Legends["LegendPane02"].Title.Text = "";
                    break;
                default:
                    this.ChrPoltA.Legends["LegendPane01"].Title.Text = "";
                    this.ChrPoltA.Legends["LegendPane02"].Title.Text = "";
                    break;
            }
        }
        /// <summary>
        /// 관리도별 Chart Option Control 표시 및 여부 처리.
        /// </summary>
        /// <param name="value"></param>
        public void DetailChartControlOptionVisibleAndTitleClear(string chartTitle, LayoutVisibility value = LayoutVisibility.Never)
        {
            //LegendPane02
            string dd = this.ChrPoltA.Legends["LegendPane01"].Title.Text.ToString();
            this.DetailChartControlClearPCU(value);
        }
        /// <summary>
        /// 관리도별 Chart Option Control 표시여부 처리.
        /// </summary>
        /// <param name="value"></param>
        public void DetailChartControlClearPCU(LayoutVisibility value = LayoutVisibility.Never)
        {
            XYDiagram diagram = (XYDiagram)ChrPoltA.Diagram;
            XYDiagramDefaultPane defaultDiagPane = diagram.DefaultPane;
            XYDiagramPane secondDiagPane = diagram.Panes["SecondPane"];
            if (value != LayoutVisibility.Never)
            {
                diagram.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.False;
                secondDiagPane.Visible = true;
            }
            else
            {
                diagram.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.True;
                secondDiagPane.Visible = false;
            }

            if (this.layoutItemRCCLValue.Visibility != value)
            {
                this.layoutItemSpace01.Visibility = value;
                this.layoutItemSigma3.Visibility = value;
                this.layoutItemSigma3Value.Visibility = value;
                this.layoutItemSigma2.Visibility = value;
                this.layoutItemSigma2Value.Visibility = value;
                this.layoutItemSigma1.Visibility = value;
                this.layoutItemSigma1Value.Visibility = value;

                this.layoutItemCCL.Visibility = value;
                this.layoutItemCCLValue.Visibility = value;
                this.layoutItemSpace04.Visibility = value;
                this.layoutItemRUCL.Visibility = value;
                this.layoutItemRUCLValue.Visibility = value;
                this.layoutItemRCCL.Visibility = value;
                this.layoutItemRCCLValue.Visibility = value;
                this.layoutItemRLCL.Visibility = value;
                this.layoutItemRLCLValue.Visibility = value;
            }
        }



        #endregion

        #region Private Function
        /// <summary>
        /// Xbar-R Chart 직접 실행.
        /// </summary>
        public void DirectXBarRExcute(SPCPara spcParaData)
        {
            ControlSpec controlSpec = ControlSpec.Create();
            SPCLibs spcLib = new SPCLibs();
            SPCOption spcOption = SPCOption.Create();
            SPCOutData spcOutData = SPCOutData.Create();

            //입력 측정 자료.
            SPCPara specEdit = spcParaData;
            //GROUPID" />
            //SAMPLEID" />
            //SUBGROUP" />
            //SAMPLING" />
            //NVALUE" />
            int i = 0;
            int j = 0;
            string firstSubgroup = "";
            //추정치
            spcOption.sigmaType = specEdit.isUnbiasing;

            //차트 타입 //통계분석
            string chartMainType = specEdit.ChartTypeMain();

            //Start - Raw Data 입력 Part.......................................................
            var rowDatas = specEdit.tableRawData.AsEnumerable();
            specEdit.InputData = new ParPIDataTable();
            var lstRow = rowDatas.AsParallel()
                    .OrderBy(or => or.Field<string>("SUBGROUP"))
                    .ThenBy(or => or.Field<string>("SAMPLING"));
            foreach (DataRow row in lstRow)
            {
                DataRow dr = specEdit.InputData.NewRow();
                if (firstSubgroup == "")
                {
                    firstSubgroup = SpcFunction.IsDbNck(row, "SUBGROUP");
                }
                dr["GROUPID"] = 1;
                dr["SAMPLEID"] = 1;//자동증가
                dr["SUBGROUP"] = SpcFunction.IsDbNck(row, "SUBGROUP");
                dr["SUBGROUPNAME"] = SpcFunction.IsDbNck(row, "SUBGROUPNAME");
                dr["SAMPLING"] = SpcFunction.IsDbNck(row, "SAMPLING");//sia추가 : sampling Name 필드 추가 필요.
                dr["SAMPLINGNAME"] = SpcFunction.IsDbNck(row, "SAMPLINGNAME");
                dr["NVALUE"] = SpcFunction.IsDbNckDoubleMin(row, "NVALUE");
                dr["NSUBVALUE"] = SpcFunction.IsDbNckDoubleMin(row, "NSUBVALUE");
                specEdit.InputData.Rows.Add(dr);
            }

            Console.WriteLine(specEdit.InputData.Rows.Count);
            //end - Raw Data 입력 Part .......................................................

            //Start - Spec Data 입력 Part.......................................................
            i = 0;
            int nLength = 0;
            specEdit.InputSpec = new ParPISpecDataTable();

            if (specEdit.tableSubgroupSpec != null && specEdit.tableSubgroupSpec.Rows.Count > 0)
            {
                nLength = specEdit.tableSubgroupSpec.Rows.Count;
                for (i = 0; i < nLength; i++)
                {
                    DataRow rowSpec = specEdit.tableSubgroupSpec.Rows[i];
                    DataRow drs = specEdit.InputSpec.NewRow();
                    drs["TEMP"] = i;
                    drs["SUBGROUP"] = SpcFunction.IsDbNck(rowSpec, "SUBGROUP");
                    drs["CHARTTYPE"] = SpcFunction.IsDbNck(rowSpec, "CTYPE");
                    drs["USL"] = SpcFunction.IsDbNckDoubleMin(rowSpec, "USL");
                    drs["CSL"] = SpcFunction.IsDbNckDoubleMin(rowSpec, "SL");
                    drs["LSL"] = SpcFunction.IsDbNckDoubleMin(rowSpec, "LSL");
                    drs["UCL"] = SpcFunction.IsDbNckDoubleMin(rowSpec, "UCL");
                    drs["CCL"] = SpcFunction.IsDbNckDoubleMin(rowSpec, "SL");
                    drs["LCL"] = SpcFunction.IsDbNckDoubleMin(rowSpec, "LCL");
                    drs["UOL"] = SpcFunction.IsDbNckDoubleMin(rowSpec, "UOL");
                    drs["LOL"] = SpcFunction.IsDbNckDoubleMin(rowSpec, "LOL");
                    specEdit.InputSpec.Rows.Add(drs);
                }
            }
            Console.WriteLine(specEdit.InputSpec.Rows.Count);
            //End - Spec Data 입력 Part.......................................................

            switch (chartMainType)
            {
                case "XBARR":
                case "R":
                    spcOption.chartType = ControlChartType.XBar_R;
                    rtnDirectChartData = SPCLibs.SpcLibXBarR(specEdit, spcOption, ref spcOutData);
                    break;
                case "XBARS":
                case "S":
                    spcOption.chartType = ControlChartType.XBar_S;
                    rtnDirectChartData = SPCLibs.SpcLibXBarS(specEdit, spcOption, ref spcOutData);
                    break;
                case "IMR":
                case "I":
                    spcOption.chartType = ControlChartType.I_MR;
                    break;
                case "XBARP":
                case "PL":
                    spcOption.chartType = ControlChartType.Merger;
                    break;
                case "P":
                case "NP":
                case "C":
                case "U":
                    spcOption.chartType = ControlChartType.p;
                    rtnDirectChartData = SPCLibs.SpcLibP(specEdit, spcOption, ref spcOutData);
                    break;
                default:
                    break;
            }
            controlSpec.spcOption = spcOption;

            if (rtnDirectChartData != null && rtnDirectChartData.Rows.Count > 0)
            {
                this.ControlChartDataMapping(rtnDirectChartData, controlSpec, specEdit);
            }

        }
        /// <summary>
        /// Series의 값 반환.
        /// </summary>
        /// <param name="chart"></param>
        /// <param name="seriesName"></param>
        /// <param name="pointsIndex"></param>
        /// <returns></returns>
        private double GetSeriesValue(SmartChart chart, string seriesName, int pointsIndex)
        {
            double returnValue = double.NaN;
            try
            {
                returnValue = (double)chart.Series[seriesName].Points[pointsIndex].Values[0];
            }
            catch (Exception)
            {
                returnValue = double.NaN;
            }
            return returnValue;
        }

        /// <summary>
        /// Chart RawData 구성.
        /// </summary>
        /// <param name="point"></param>
        private void PopupRawDataEdit(SeriesPoint point)
        {

            string avgValueMessage = SpcLibMessage.common.comCpk1033;//값(Value)
            string asixsLabelMessage = SpcLibMessage.common.comCpk1034;//asixs 라벨:

            string pointInfo = string.Format("{0} {1},  {2}: {3}", asixsLabelMessage, point.Argument.ToString(), avgValueMessage, point.Values[0].ToSafeString());
            string subGroupId = string.Empty;
            string subGroupName = string.Empty;

            subGroupId = _SUBGROUP;//sia필수확인 : 8/8 SubGroup이 변경 되지 않음.
            subGroupName = _SUBGROUPNAME;
            Console.WriteLine(_controlSpec.sigmaResult.cpkResult.SUBGROUP);
            Console.WriteLine(_spcPara.InputData.Rows.Count);
            Console.WriteLine(_spcPara.InputData.Rows.Count);

            string samplingName = string.Empty;
            string samplingRow = string.Empty;
            ParPIDataTable ChartRawData = new ParPIDataTable();
            var subGroupData = _spcPara.InputData.Where(x => x.SUBGROUP == subGroupId)
                                                  .OrderBy(r1 => r1.SAMPLING).ThenByDescending(r2 => r2.NVALUE);

            foreach (DataRow item in subGroupData)
            {
                DataRow newRow = ChartRawData.NewRow();
                newRow["GROUPID"] = SpcFunction.IsDbNckInt64(item, "GROUPID");
                newRow["SAMPLEID"] = SpcFunction.IsDbNckInt64(item, "SAMPLEID");
                newRow["SUBGROUP"] = SpcFunction.IsDbNck(item, "SUBGROUP");
                newRow["SAMPLING"] = SpcFunction.IsDbNck(item, "SAMPLING");
                newRow["NVALUE"] = SpcFunction.IsDbNckDoubleMin(item, "NVALUE");
                ChartRawData.Rows.Add(newRow);
            }

            SpcStatusRawDataPopup frm = new SpcStatusRawDataPopup
            {
                RawData = ChartRawData,
                subgroupId = subGroupId,
                gridTitle = string.Format("{0}", subGroupName)
            };

            frm.ShowDialog();

        }

        /// <summary>
        /// Chart Paint - 해석용 관리선 처리.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlLinePaint(object sender, PaintEventArgs e)
        {
            if (_isInterpretation.LCL != true && _isInterpretation.UCL != true
                && _isInterpretation.RLCL != true && _isInterpretation.RUCL != true)
            {
                return;
            }

            try
            {

                //sia확인 : 03.Chart Paint

                //if (_IsOverRule == false)
                //{
                //    _IsOverRule = true;
                //    ChrXbarR.Series["Series03Value"].Points[4].Color = Color.Red;
                //}
                XYDiagram diag = (XYDiagram)ChrPoltA.Diagram;
                //XYDiagramPane secondDiagPaneCreate = new XYDiagramPane("SecondPane");
                //XYDiagramDefaultPane defaultDiagPane = diag.DefaultPane;
                XYDiagramPane secondDiagPane = diag.Panes["SecondPane"];

                //Axis2D axis2DX = ((XYDiagram)ChrXbarR.Diagram).SecondaryAxesX[0];
                Axis2D axis2DX = diag.SecondaryAxesX["SecondaryAxisX01"];
                Axis2D axis2DY = diag.SecondaryAxesY["SecondaryAxisY01"];
                //secondaryAxisX.
                //SecondaryAxisY sAxisY1 = (SecondaryAxisX)ChrXbarR.SecondaryAxisY();
                ControlCoordinates ccmin = diag.DiagramToPoint(diag.AxisX.VisualRange.MinValue.ToString(), (double)diag.AxisY.VisualRange.MinValue);
                ControlCoordinates ccmax = diag.DiagramToPoint(diag.AxisX.VisualRange.MinValue.ToString(), (double)diag.AxisY.VisualRange.MaxValue);

                //ControlCoordinates ccmax = diag.DiagramToPoint(diag.AxisX.VisualRange.MinValue.ToString(), (double)diag.AxisY.VisualRange.MaxValue);

                //txtPointView.Text = string.Format("X: {0},  Y: {1}", ccmin.Point.X, ccmin.Point.Y);

                //ControlCoordinates ccmax1 = diag.DiagramToPoint("Lot-02",(double)2.5);

                int height = ccmin.Point.Y - ccmax.Point.Y;
                object op = ChrPoltA.Series[0].Points[0].Values;
                Diagram uDiagram1 = ChrPoltA.Diagram;
                XYDiagram diagram = ChrPoltA.Diagram as XYDiagram;
                Type chartType = ChrPoltA.Series[0].GetType();

                //diagram.DefaultPane.Weight
                string axisXLabel = "";
                double axisXValueAUCL = double.NaN;
                double axisXValueALCL = double.NaN;
                double axisXValueBefore = double.NaN;

                double axisXValueSubAUCL = double.NaN;
                double axisXValueSubALCL = double.NaN;
                double axisXValueSubBefore = double.NaN;

                int nlen = ChrPoltA.Series["Series03Value"].Points.Count;
                int nlenSub = ChrPoltA.Series["Series13Value"].Points.Count;
                ControlCoordinates ccBase;
                ControlCoordinates ccBefore;
                ControlCoordinates ccObjectAUCL = null;
                ControlCoordinates ccBeforeALCL = null;
                ControlCoordinates ccObjectALCL = null;
                //ccBase = diag.DiagramToPoint("",);

                ControlCoordinates ccBeforeSub = null;
                ControlCoordinates ccObjectSubAUCL = null;
                ControlCoordinates ccBeforeSubALCL = null;
                ControlCoordinates ccObjectSubALCL = null;

                float xCap = 0f;
                float yCap = float.NaN;

                float xCapSub = 0f;
                float yCapSub = float.NaN;

                int i = 0;
                //xCap
                for (i = 0; i < nlen; i++)
                {
                    axisXValueBefore = axisXValueAUCL;
                    axisXValueSubBefore = axisXValueSubAUCL;

                    axisXLabel = ChrPoltA.Series["Series01AUCL"].Points[i].Argument.ToString();

                    axisXValueAUCL = ChrPoltA.Series["Series01AUCL"].Points[i].Values[0];
                    axisXValueALCL = ChrPoltA.Series["Series02ALCL"].Points[i].Values[0];

                    ccBefore = ccObjectAUCL;
                    ccObjectAUCL = diag.DiagramToPoint(axisXLabel, axisXValueAUCL);
                    ccBeforeALCL = ccObjectALCL;
                    ccObjectALCL = diag.DiagramToPoint(axisXLabel, axisXValueALCL);

                    if (i == 1)
                    {
                        txtPointView.Text = string.Format("X: {0},  Y: {1}", ccObjectAUCL.Point.X, ccObjectAUCL.Point.Y);
                    }

                    if (xCap == 0f)
                    {
                        if (ccBefore != null && ccObjectAUCL != null)
                        {
                            xCap = ((ccObjectAUCL.Point.X - ccBefore.Point.X) / 2) + 1.4f;
                            break;
                        }
                    }
                }

                if (xCap == 0f)
                {
                    xCap = (ccObjectAUCL.Point.X / 5) + 1.4f;
                }

                //XCapSub Check
                bool isSubChart = false;
                switch (_controlSpec.spcOption.chartType)
                {
                    case ControlChartType.XBar_R:
                    case ControlChartType.XBar_S:
                    case ControlChartType.I_MR:
                    case ControlChartType.Merger:
                        isSubChart = true;
                        break;
                    case ControlChartType.np:
                    case ControlChartType.p:
                    case ControlChartType.c:
                    case ControlChartType.u:
                        isSubChart = false;
                        break;
                    default:
                        break;
                }
                //XCapSub
                if (isSubChart != false)
                {
                    for (i = 0; i < nlenSub; i++)
                    {
                        //axisXValueSubAUCL = (double)ChrXbarR.Series["Series11AUCL"].Points[i].Values[0];
                        //axisXValueSubALCL = (double)ChrXbarR.Series["Series12ALCL"].Points[i].Values[0].ToSafeDoubleNaN();

                        axisXLabel = ChrPoltA.Series["Series11AUCL"].Points[i].Argument.ToString();
                        axisXValueSubAUCL = GetSeriesValue(ChrPoltA, "Series11AUCL", i);
                        axisXValueSubALCL = GetSeriesValue(ChrPoltA, "Series12ALCL", i);

                        //sia확인 : 해석용 Line, {(second Pane)} ucXbar Paint 

                        //Axis2D axis2DXx = diag.SecondaryAxesX[0];
                        //Axis2D axis2DYx = diag.SecondaryAxesY[0];
                        ccBeforeSub = ccObjectSubAUCL;
                        //ccObjectSubAUCL = diag.DiagramToPoint(axisXLabel, axisXValueSubAUCL, axis2DX, axis2DY, secondDiagPane);
                        ccObjectSubAUCL = diag.DiagramToPoint(axisXLabel, axisXValueSubAUCL, axis2DX, axis2DY, secondDiagPane);
                        ccBeforeSubALCL = ccObjectSubALCL;
                        ccObjectSubALCL = diag.DiagramToPoint(axisXLabel, axisXValueSubALCL, axis2DX, axis2DY, secondDiagPane);

                        if (xCapSub == 0f)
                        {
                            if (ccBeforeSub != null && ccObjectSubAUCL != null)
                            {
                                xCapSub = ((ccObjectSubAUCL.Point.X - ccBeforeSub.Point.X) / 2) + 1.4f;
                                break;
                            }
                        }
                    }
                }

                if (xCapSub == 0f)
                {
                    xCapSub = (ccObjectSubAUCL.Point.X / 5) + 1.4f;
                }

                bool firstLineCheck = false;
                bool firstLineSubCheck = false;
                for (i = 0; i < nlen; i++)
                {
                    axisXValueBefore = axisXValueAUCL;
                    axisXLabel = ChrPoltA.Series["Series01AUCL"].Points[i].Argument.ToString();
                    axisXValueAUCL = (double)ChrPoltA.Series["Series01AUCL"].Points[i].Values[0];
                    axisXValueALCL = (double)ChrPoltA.Series["Series02ALCL"].Points[i].Values[0];

#if DEBUG
                    Console.Write(axisXLabel + " : ");
                    Console.WriteLine(axisXValueAUCL);
#endif

                    axisXValueSubBefore = axisXValueSubAUCL;
                    axisXValueSubAUCL = GetSeriesValue(ChrPoltA, "Series11AUCL", i);
                    axisXValueSubALCL = GetSeriesValue(ChrPoltA, "Series12ALCL", i);

                    ccBefore = ccObjectAUCL;
                    ccObjectAUCL = diag.DiagramToPoint(axisXLabel, axisXValueAUCL);
                    ccBeforeALCL = ccObjectALCL;
                    ccObjectALCL = diag.DiagramToPoint(axisXLabel, axisXValueALCL);

                    if (isSubChart != false)
                    {
                        ccBeforeSub = ccObjectSubAUCL;
                        //ccObjectSubAUCL = diag.DiagramToPoint(axisXLabel, axisXValueSubAUCL);
                        ccObjectSubAUCL = diag.DiagramToPoint(axisXLabel, axisXValueSubAUCL, axis2DX, axis2DY, secondDiagPane);
                        ccBeforeSubALCL = ccObjectSubALCL;
                        //ccObjectSubALCL = diag.DiagramToPoint(axisXLabel, axisXValueSubALCL);
                        ccObjectSubALCL = diag.DiagramToPoint(axisXLabel, axisXValueSubALCL, axis2DX, axis2DY, secondDiagPane);
                    }

                    if (axisXValueAUCL != 0f)
                    {
                        #region Default Pane
                        float x1;
                        float x2;
                        x1 = ccObjectAUCL.Point.X - xCap;
                        x2 = ccObjectAUCL.Point.X + xCap;

                        //UCL 좌표값 입력.
                        float y1 = ccObjectAUCL.Point.Y;
                        float y2 = ccObjectAUCL.Point.Y;
                        Pen uslPenH = new Pen(Color.FromArgb(0, 0, 255), 0.1f);
                        uslPenH.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                        if (_isInterpretation.UCL != false)
                        {
                            e.Graphics.DrawLine(uslPenH, x1, y1, x2, y2);
                        }
                        //LCL 좌표값 입력.
                        float yL1 = ccObjectALCL.Point.Y;
                        float yL2 = ccObjectALCL.Point.Y;
                        //Pen uslPenHALCL = new Pen(Color.FromArgb(0, 0, 255), 0.1f);
                        //uslPenH.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                        if (_isInterpretation.LCL != false)
                        {
                            e.Graphics.DrawLine(uslPenH, x1, yL1, x2, yL2);
                        }

                        if (ccBefore != null && axisXValueBefore != 0f)
                        {
                            float xH1 = x1;
                            float xH2 = x1;
                            //UCL 좌표값 입력.
                            float yH1 = y2;
                            float yH2 = ccBefore.Point.Y;

                            float yH1ALCL = yL2;
                            float yH2ALCL = ccBeforeALCL.Point.Y;

                            Pen uslPenV = new Pen(Color.FromArgb(0, 0, 255), 0.1f);
                            uslPenV.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                            if (firstLineCheck != false)
                            {
                                if (_isInterpretation.UCL != false)
                                {
                                    e.Graphics.DrawLine(uslPenV, xH1, yH1, xH2, yH2);
                                }
                                if (_isInterpretation.LCL != false)
                                {
                                    e.Graphics.DrawLine(uslPenV, xH1, yH1ALCL, xH2, yH2ALCL);
                                }
                            }
                            else
                            {
                                firstLineCheck = true;
                            }
                        }
                        #endregion


                        #region Second Pane
                        if (isSubChart != false)
                        {
                            float x11;
                            float x12;
                            x11 = ccObjectSubAUCL.Point.X - xCapSub;
                            x12 = ccObjectSubAUCL.Point.X + xCapSub;

                            //RUCL 좌표값 입력.
                            float y11 = ccObjectSubAUCL.Point.Y;
                            float y12 = ccObjectSubAUCL.Point.Y;
                            Pen uslPenHSub = new Pen(Color.FromArgb(0, 0, 255), 0.1f);
                            uslPenHSub.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                            if (_isInterpretation.RUCL != false)
                            {
                                e.Graphics.DrawLine(uslPenHSub, x11, y11, x12, y12);
                            }

                            //RLCL 좌표값 입력.
                            float yL11 = ccObjectSubALCL.Point.Y;
                            float yL12 = ccObjectSubALCL.Point.Y;
                            //Pen uslPenHALCL = new Pen(Color.FromArgb(0, 0, 255), 0.1f);
                            //uslPenH.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                            if (_isInterpretation.RLCL != false)
                            {
                                e.Graphics.DrawLine(uslPenHSub, x11, yL11, x12, yL12);
                            }

                            if (ccBeforeSub != null && axisXValueSubBefore != 0f)
                            {
                                float xH11 = x11;
                                float xH12 = x11;
                                //UCL 좌표값 입력.
                                float yH11 = y12;
                                float yH12 = ccBeforeSub.Point.Y;

                                float yH11ALCL = yL12;
                                float yH12ALCL = ccBeforeSubALCL.Point.Y;

                                Pen uslPenVSub = new Pen(Color.FromArgb(0, 0, 255), 0.1f);
                                uslPenVSub.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                                if (firstLineSubCheck != false)
                                {
                                    if (_isInterpretation.RUCL != false)
                                    {
                                        e.Graphics.DrawLine(uslPenVSub, xH11, yH11, xH12, yH12);
                                    }
                                    if (_isInterpretation.RLCL != false)
                                    {
                                        e.Graphics.DrawLine(uslPenVSub, xH11, yH11ALCL, xH12, yH12ALCL);
                                    }
                                }
                                else
                                {
                                    firstLineSubCheck = true;
                                }
                            }
                        }
                        #endregion
                    }
                }
            }
            catch (Exception)
            {
                _IsPaint = false;
                //throw;
            }

            //e.Graphics.DrawLine(uslPen, x1,y1,x2,y2);
        }

        /// <summary>
        /// Control 기본값 설정
        /// </summary>
        private void ControlDefaultSetting()
        {
            //layoutItemTemp01.Visibility = LayoutVisibility.Never;
            //layoutItemTemp02.Visibility = LayoutVisibility.Never;
            //layoutItemTemp03.Visibility = LayoutVisibility.Never;
            //layoutItemTemp01Value.Visibility = LayoutVisibility.Never;
            //layoutItemTemp02Value.Visibility = LayoutVisibility.Never;
            //layoutItemTemp03Value.Visibility = LayoutVisibility.Never;
            lblSpace01.Text = "";
            lblSpace02.Text = "";
            lblSpace03.Text = "";
            lblSpace04.Text = "";
            lblSpace05.Text = "";

            ((XYDiagram)ChrPoltA.Diagram).AxisY.ConstantLines["ConstantLineUOL"].Visible = false;
            ((XYDiagram)ChrPoltA.Diagram).AxisY.ConstantLines["ConstantLineLOL"].Visible = false;
        }


        #endregion

        #region Test 함수
        public void TestSubGroupIdSetting()
        {
            this.TestDataCreate_InputData(ref _spcPara);//parameter Test Data Load
            this.CoboBoxSettingSubGroupId();//Subgroup ID
        }
        /// <summary>
                                            /// Test Data 생성 - 최초 Input Data 생성 함수 (자체 Test용)
                                            /// </summary>
                                            /// <param name="spcPara"></param>
            public void TestDataCreate_InputData(ref SPCPara spcPara)
        {
            string firstSubgroup = "";
            int i = 0;
            if (spcPara == null)
            {
                spcPara = new SPCPara();
                spcPara.spcOption = SPCOption.Create();
            }
            //Test Part.......................................................
            //string[,] pdata = _spcTestData.CTR01();//단일
            //string[,] pdata = _spcTestData.CTR_P01();//P Chart 단일
            //string[,] pdata = _spcTestData.CTR02_PL();//단일 합동
            //string[,] pdata = _spcTestData.CTR02_IMR();//단일
            string[,] pdata = _spcTestData.CTR02Group();//그룹 /R, S Test Data
            //string[,] pdata = _spcTestData.CTR03Group();//그룹 / R, S Test Data
            //string[,] pdata = _spcTestData.BoxPlot01_Single_TestData();//단일
            spcPara.InputData = null;
            spcPara.InputSpec = null;
            spcPara.InputData = new ParPIDataTable();
            spcPara.InputSpec = new ParPISpecDataTable();
            for (i = 1; i < pdata.GetLength(1); i++)
            {
                DataRow dr = spcPara.InputData.NewRow();
                dr["GROUPID"] = pdata[1, i];
                dr["SAMPLEID"] = 1;//자동증가
                dr["SUBGROUP"] = pdata[2, i];
                dr["SUBGROUPNAME"] = string.Format("그룹명{0}{1}", SpcSym.subgroupSep11, pdata[2, i]);
                dr["SAMPLING"] = pdata[3, i];
                dr["SAMPLINGNAME"] = string.Format("샘플명{0}{1}", SpcSym.subgroupSep11, pdata[3, i]); ;
                dr["NVALUE"] = pdata[4, i];
                dr["NSUBVALUE"] = pdata[4, i] + 1000;
                //if (chartType == "P")
                //{
                //    dr["NSUBVALUE"] = pdata[5, i];
                //}
                //else
                //{
                //    dr["NSUBVALUE"] = 0f;
                //}
                spcPara.InputData.Rows.Add(dr);

                if (firstSubgroup == "")
                {
                    firstSubgroup = pdata[2, i];
                }
            }

            //End Test Part....................................................+

            //Start - Spec Data 입력 Part.......................................................
            i = 0;
            int nLength = 0;
            string specChartType = "";
            spcPara.InputSpec = new ParPISpecDataTable();

            if (spcPara.InputData != null && spcPara.InputData.Rows.Count > 0)
            {

                var query = from r in spcPara.InputData
                                //where r.Field<string>("ctype") == "XBARR"
                            group r by r.SUBGROUP into g
                            select new
                            {
                                nSubgroup = g.Key,
                                //AverageListPrice = g.Average(product => product.Field<Decimal>("ListPrice"))
                                nCount = g.Count()
                            };
                foreach (var rw in query)
                {
                    nLength = 11;
                    for (i = 0; i < nLength; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                specChartType = "XBARR";
                                break;
                            case 1:
                                specChartType = "R";
                                break;
                            case 2:
                                specChartType = "XBARS";
                                break;
                            case 3:
                                specChartType = "S";
                                break;
                            case 4:
                                specChartType = "XBARP";
                                break;
                            case 5:
                                specChartType = "PL";
                                break;
                            case 6:
                                specChartType = "I";
                                break;
                            case 7:
                                specChartType = "MR";
                                break;
                            case 8:
                                specChartType = "NP";
                                break;
                            case 9:
                                specChartType = "P";
                                break;
                            case 10:
                                specChartType = "C";
                                break;
                            case 11:
                                specChartType = "U";
                                break;
                            default:
                                break;
                        }

                        DataRow drs = spcPara.InputSpec.NewRow();
                        drs["TEMP"] = i;
                        drs["SUBGROUP"] = rw.nSubgroup;
                        drs["CHARTTYPE"] = specChartType;
                        drs["USL"] = 135f;
                        drs["CSL"] = 75f;
                        drs["LSL"] = 35f;
                        drs["UCL"] = 95f;
                        drs["CCL"] = 63f;
                        drs["LCL"] = 25f;
                        drs["UOL"] = 165f;
                        drs["LOL"] = 5f;
                        spcPara.InputSpec.Rows.Add(drs);
                    }
                }
            }

            switch (spcPara.spcOption.limitType)
            {
                case LimitType.Interpretation:
                    break;
                case LimitType.Management:
                    if (spcPara.InputSpec != null && spcPara.InputSpec.Rows.Count > 0 && firstSubgroup != "")
                    {
                        string chartMainType = spcPara.ChartTypeMain();
                        var lstSpecMain = spcPara.InputSpec
                                            .Where(w => w.SUBGROUP == firstSubgroup && w.CHARTTYPE == chartMainType)
                                            .OrderBy(or => or.SUBGROUP);
                        foreach (var item in lstSpecMain)
                        {
                            Console.WriteLine(item.SUBGROUP);
                            spcPara.USL = item.USL;
                            spcPara.CSL = item.CSL;
                            spcPara.LSL = item.LSL;

                            spcPara.UCL = item.UCL;
                            spcPara.CCL = item.CCL;
                            spcPara.LCL = item.LCL;

                            spcPara.UOL = item.UOL;
                            spcPara.LOL = item.LOL;
                        }
                        //this.txtAllUclValue.Text = specEdit.UCL.ToSafeString();
                        //this.txtAllCclValue.Text = specEdit.CCL.ToSafeString();
                        //this.txtAllLclValue.Text = specEdit.LCL.ToSafeString();

                        string chartSubType = spcPara.ChartTypeSub();
                        if (chartSubType != "")
                        {
                            var lstSpecSub = spcPara.InputSpec
                            .Where(w => w.SUBGROUP == firstSubgroup && w.CHARTTYPE == chartSubType)
                            .OrderBy(or => or.SUBGROUP);
                            foreach (var item in lstSpecSub)
                            {
                                Console.WriteLine(item.SUBGROUP);

                                spcPara.RUCL = item.UCL;
                                spcPara.RCCL = item.CCL;
                                spcPara.RLCL = item.LCL;

                                spcPara.RUOL = item.UOL;
                                spcPara.RLOL = item.LOL;

                            }
                            //this.txtAllRUclValue.Text = specEdit.RUCL.ToSafeString();
                            //this.txtAllRCclValue.Text = specEdit.RCCL.ToSafeString();
                            //this.txtAllRLclValue.Text = specEdit.RLCL.ToSafeString();
                        }
                    }
                    break;
                case LimitType.Direct:
                    break;
                default:
                    break;
            }

            Console.WriteLine(spcPara.InputData.Count);
        }
        /// <summary>
        /// 관리도 Chart Test Data 입력.
        /// </summary>
        private void ControlChartDataMappingTest()
        {
            //sia확인 : 02.관리도 Chart Data 입력 및 Mapping.
            SpcParameter xbarData = SpcParameter.Create();
            ControlSpec controlSpec = new ControlSpec();
            xbarData.SpcData = xbarData.TestDataSpcXBar(out controlSpec);
            this.ChrPoltA.DataSource = null;// xbarData.SpcData;

            // Create a series, and add it to the chart. 
            //Series series1 = new Series("My Series", ViewType.Bar);
            this.ChrPoltA.Series["Series01AUCL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series01AUCL"].ValueDataMembers.AddRange(new string[] { "AUCL" });
            this.ChrPoltA.Series["Series02ALCL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series02ALCL"].ValueDataMembers.AddRange(new string[] { "ALCL" });

            this.ChrPoltA.Series["Series03Value"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series03Value"].ValueDataMembers.AddRange(new string[] { "nValue" });

            this.ChrPoltA.Series["Series11AUCL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series11AUCL"].ValueDataMembers.AddRange(new string[] { "RAUCL" });
            this.ChrPoltA.Series["Series12ALCL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series12ALCL"].ValueDataMembers.AddRange(new string[] { "RALCL" });

            this.ChrPoltA.Series["Series13Value"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series13Value"].ValueDataMembers.AddRange(new string[] { "nRValue" });

            //LSL OverRules
            this.ChrPoltA.Series["Series04OverRuleUCL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series04OverRuleUCL"].ValueDataMembers.AddRange(new string[] { "nOverRuleUCL" });

            this.ChrPoltA.Series["Series05OverRuleLCL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series05OverRuleLCL"].ValueDataMembers.AddRange(new string[] { "nOverRuleLCL" });

            //USL OverRules
            this.ChrPoltA.Series["Series06OverRuleUSL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series06OverRuleUSL"].ValueDataMembers.AddRange(new string[] { "nOverRuleUSL" });

            this.ChrPoltA.Series["Series07OverRuleLSL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series07OverRuleLSL"].ValueDataMembers.AddRange(new string[] { "nOverRuleLSL" });

            //USL OverRules
            this.ChrPoltA.Series["Series08OverRuleUOL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series08OverRuleUOL"].ValueDataMembers.AddRange(new string[] { "nOverRuleUOL" });

            this.ChrPoltA.Series["Series09OverRuleLOL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series09OverRuleLOL"].ValueDataMembers.AddRange(new string[] { "nOverRuleLOL" });

            //LSL OverRules
            this.ChrPoltA.Series["Series14OverRuleRUCL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series14OverRuleRUCL"].ValueDataMembers.AddRange(new string[] { "nROverRuleUCL" });

            this.ChrPoltA.Series["Series15OverRuleRLCL"].ArgumentDataMember = "Label";
            this.ChrPoltA.Series["Series15OverRuleRLCL"].ValueDataMembers.AddRange(new string[] { "nROverRuleLCL" });

            pnlMain.Dock = DockStyle.Fill;

            //Chart Option
            this.lblUCLValue.Text = Math.Round(controlSpec.nXbar.ucl, 3).ToSafeString();
            this.lblLCLValue.Text = Math.Round(controlSpec.nXbar.lcl, 3).ToSafeString();

            this.lblUSLValue.Text = Math.Round(controlSpec.nXbar.usl, 3).ToSafeString();
            this.lblCSLValue.Text = Math.Round(controlSpec.nXbar.csl, 3).ToSafeString();
            this.chkCSL.Text = "XBAR";
            this.chkCSL.ForeColor = Color.Green;
            this.lblLSLValue.Text = Math.Round(controlSpec.nXbar.lsl, 3).ToSafeString();

            this.lblUOLValue.Text = Math.Round(controlSpec.nXbar.uol, 3).ToSafeString();
            this.lblLOLValue.Text = Math.Round(controlSpec.nXbar.lol, 3).ToSafeString();

            this.lblRUCLValue.Text = Math.Round(controlSpec.nR.ucl, 3).ToSafeString();
            this.lblRCCLValue.Text = Math.Round(controlSpec.nR.ccl, 3).ToSafeString();
            this.chkRCCL.Text = "R";
            this.chkRCCL.ForeColor = Color.Green;
            this.lblRLCLValue.Text = Math.Round(controlSpec.nR.lcl, 3).ToSafeString();

            XYDiagram diagram = (XYDiagram)ChrPoltA.Diagram;
            diagram.AxisY.ConstantLines["ConstantLineUCL"].AxisValue = controlSpec.nXbar.ucl;
            diagram.AxisY.ConstantLines["ConstantLineLCL"].AxisValue = controlSpec.nXbar.lcl;

            diagram.AxisY.ConstantLines["ConstantLineUSL"].AxisValue = controlSpec.nXbar.usl;
            diagram.AxisY.ConstantLines["ConstantLineLSL"].AxisValue = controlSpec.nXbar.lsl;

            diagram.AxisY.ConstantLines["ConstantLineUOL"].AxisValue = controlSpec.nXbar.uol;
            diagram.AxisY.ConstantLines["ConstantLineLOL"].AxisValue = controlSpec.nXbar.lol;

            diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRUCL"].AxisValue = controlSpec.nR.ucl;
            diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRCCL"].AxisValue = controlSpec.nR.ccl;
            diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRLCL"].AxisValue = controlSpec.nR.lcl;
        }
        /// <summary>
        /// Xbar-R Chart 직접 실행 - Test.
        /// </summary>
        public void DirectXBarRExcuteTest(SPCPara spcParaData)
        {
            ControlSpec controlSpec = ControlSpec.Create();
            SPCOption spcOption = SPCOption.Create();
            SPCOutData spcOutData = SPCOutData.Create();
            SPCPara spcPara = spcParaData;
            spcPara.InputData = new ParPIDataTable();//입력 측정 자료.
            int i = 0;
            string chartType = spcPara.ChartTypeMain();

            //Test Part.......................................................
            string[,] pdata = _spcTestData.CTR_P01();//P Chart 단일
            //string[,] pdata = _spcTestData.CTR01();//단일
            //string[,] pdata = _spcTestData.CTR02Group();//그룹
            //string[,] pdata = _spcTestData.CTR03Group();//그룹
            for (i = 1; i < pdata.GetLength(1); i++)
            {
                DataRow dr = spcPara.InputData.NewRow();
                dr["GROUPID"] = pdata[1, i];
                dr["SAMPLEID"] = 1;//자동증가
                dr["SUBGROUP"] = pdata[2, i];
                dr["SUBGROUPNAME"] = string.Format("GroupName-{0}", pdata[2, i]);
                dr["SAMPLING"] = pdata[3, i];
                dr["SAMPLINGNAME"] = string.Format("SampleName-{0}", pdata[3, i]); ;
                dr["NVALUE"] = pdata[4, i];
                if (chartType == "P")
                {
                    dr["NSUBVALUE"] = pdata[5, i];
                }
                else
                {
                    dr["NSUBVALUE"] = 0f;
                }
                spcPara.InputData.Rows.Add(dr);
            }

            //spcPara.USL = 61.5;
            //spcPara.CSL = 35.7;
            //spcPara.LSL = 25.5;

            //End Test Part....................................................+

            //Start - Spec Data 입력 Part.......................................................
            i = 0;
            int nLength = 0;
            string specChartType = "";
            spcPara.InputSpec = new ParPISpecDataTable();

            if (spcPara.InputData != null && spcPara.InputData.Rows.Count > 0)
            {

                var query = from r in spcPara.InputData
                                //where r.Field<string>("ctype") == "XBARR"
                            group r by r.SUBGROUP into g
                            select new
                            {
                                nSubgroup = g.Key,
                                //AverageListPrice = g.Average(product => product.Field<Decimal>("ListPrice"))
                                nCount = g.Count()
                            };
                foreach (var rw in query)
                {
                    nLength = 11;
                    for (i = 0; i < nLength; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                specChartType = "XBARR";
                                break;
                            case 1:
                                specChartType = "R";
                                break;
                            case 2:
                                specChartType = "XBARS";
                                break;
                            case 3:
                                specChartType = "S";
                                break;
                            case 4:
                                specChartType = "XBARP";
                                break;
                            case 5:
                                specChartType = "PM";
                                break;
                            case 6:
                                specChartType = "I";
                                break;
                            case 7:
                                specChartType = "MR";
                                break;
                            case 8:
                                specChartType = "NP";
                                break;
                            case 9:
                                specChartType = "P";
                                break;
                            case 10:
                                specChartType = "C";
                                break;
                            case 11:
                                specChartType = "U";
                                break;
                            default:
                                break;
                        }

                        DataRow drs = spcPara.InputSpec.NewRow();
                        drs["TEMP"] = i;
                        drs["SUBGROUP"] = rw.nSubgroup;
                        drs["CHARTTYPE"] = specChartType;
                        drs["USL"] = 135f;
                        drs["CSL"] = 75f;
                        drs["LSL"] = 35f;
                        drs["UCL"] = 95f;
                        drs["CCL"] = 63f;
                        drs["LCL"] = 25f;
                        drs["UOL"] = 165f;
                        drs["LOL"] = 5f;
                        spcPara.InputSpec.Rows.Add(drs);
                    }
                }
            }

            Console.WriteLine(spcPara.InputSpec.Rows.Count);
            //End - Spec Data 입력 Part.......................................................

            //string sigmaType = this.comboBoxType.Text.ToString();
            spcOption.sigmaType = spcPara.isUnbiasing;

            //차트 타입 //통계분석
            controlSpec.spcOption = spcOption;
            chartType = spcPara.ChartTypeMain();
            switch (chartType)
            {
                case "XBARR":
                case "R":
                    spcOption.chartType = ControlChartType.XBar_R;
                    rtnDirectChartData = SPCLibs.SpcLibXBarR(spcPara, spcOption, ref spcOutData);
                    break;
                case "XBARS":
                case "S":
                    spcOption.chartType = ControlChartType.XBar_S;
                    rtnDirectChartData = SPCLibs.SpcLibXBarS(spcPara, spcOption, ref spcOutData);
                    break;
                case "IMR":
                case "I":
                    spcOption.chartType = ControlChartType.I_MR;
                    break;
                case "XBARP":
                case "PL":
                    spcOption.chartType = ControlChartType.Merger;
                    break;
                case "P":
                case "NP":
                case "C":
                case "U":
                    spcOption.chartType = ControlChartType.p;
                    rtnDirectChartData = SPCLibs.SpcLibP(spcPara, spcOption, ref spcOutData);
                    break;
                default:
                    break;
            }


            if (rtnDirectChartData != null && rtnDirectChartData.Rows.Count > 0)
            {
                this.ControlChartDataMapping(rtnDirectChartData, controlSpec, spcPara);
            }

        }




        #endregion


    }//end class
}//end namespace
