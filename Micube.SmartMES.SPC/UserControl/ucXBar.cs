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
using System.Collections.Generic;
using Micube.Framework;

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
    public partial class ucXBar : DevExpress.XtraEditors.XtraUserControl
    {
        #region Local Variables
        /// <summary>
        /// SPC Option 재정의 이벤트 - delegate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="se"></param>
        public delegate void SpcChartEnterEventHandler(object sender, EventArgs e, SpcEventsOption se);
        /// <summary>
        /// SPC Option 재정의 이벤트 
        /// </summary>
        public virtual event SpcChartEnterEventHandler SpcCpkChartEnterEventHandler;
        /// <summary>
        /// SPC P Chart Type Change 재정의 이벤트 - delegate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="se"></param>
        public delegate void SpcChartPChartTypeChangeEventHandler(object sender, EventArgs e, SpcEventsOption se);
        /// <summary>
        /// SPC P Chart Type Change 재정의 이벤트 
        /// </summary>
        public virtual event SpcChartPChartTypeChangeEventHandler SpcChartVtPChartTypeChangeEventHandler;


        /// <summary>
        /// Chart Double Click 재정의 이벤트 - delegate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="se"></param>
        public delegate void SpcChartDoubleClickEventHandler(object sender, EventArgs e, SpcEventsOption se);
        /// <summary>
        /// Chart Double Click 재정의 이벤트
        /// </summary>
        public virtual event SpcChartDoubleClickEventHandler SpcChartDoubleClickUserEventHandler;
        /// <summary>
        /// Chart 내부 메세지 이벤트 - delegate
        /// </summary>
        /// <param name="msg"></param>
        public delegate void SpcChartDirectMessageEventHandler(SpcEventsChartMessage msg);
        /// <summary>
        /// Chart 내부 메세지 이벤트
        /// </summary>
        public virtual event SpcChartDirectMessageEventHandler SpcChartDirectMessageUserEventHandler;
        /// <summary>
        /// SpcChartEnterEventHandler 이벤트 추가 파라메타
        /// </summary>
        public SpcEventsOption spcEop = new SpcEventsOption();
        /// <summary>
        /// 내부 Message 입력 Parameter
        /// </summary>
        public SpcEventsChartMessage spcMsg = new SpcEventsChartMessage();
        /// <summary>
        /// Test Mode 구분
        /// </summary>
        //public bool IsTestMode = false;
        /// <summary>
        /// 상세 Popup 표시 중지 구분
        /// </summary>
        public bool IsDetailPopupLock = false;
        /// <summary>
        /// 차트명
        /// </summary>
        private string _nChartName = "";
        /// <summary>
        /// 결과 자료.
        /// </summary>
        private RtnControlDataTable _dtControlData = new RtnControlDataTable();
        /// <summary>
        /// 결과 자료 - 직접 처리용.
        /// </summary>
        public RtnControlDataTable rtnDirectChartData = new RtnControlDataTable();
        /// <summary>
        /// SPEC 정보
        /// </summary>
        private ControlSpec _controlSpec = new ControlSpec();
        /// <summary>
        /// Subgroup ID 입력
        /// </summary>
        private string _SUBGROUP = "";

        /// <summary>
        /// Subgroup Name 입력
        /// </summary>
        private string _SUBGROUPNAME = "";
        /// <summary>
        /// 입력 Parameter
        /// </summary>
        private SPCPara _spcPara = new SPCPara();
        /// <summary>
        /// Over Rules 구분
        /// </summary>
        private bool _IsOverRule = false;
        /// <summary>
        /// 직접 Chart 구분 
        /// </summary>
        private bool _IsThisCboChartTypeTextChanged = false;
        /// <summary>
        /// Paint 이벤트 사용 유무
        /// </summary>
        private bool _IsPaint = false;
        /// <summary>
        /// 해석용 관리선 표시 유무
        /// </summary>
        private IsInterpretation _isInterpretation = IsInterpretation.Create();
        /// <summary>
        /// Chart X 좌표
        /// </summary>
        private int _x = 0;
        /// <summary>
        /// Chart Y 좌표
        /// </summary>
        private int _y = 0;
        /// <summary>
        /// Test Data
        /// </summary>
        private SPCTestData _spcTestData = new SPCTestData();
        /// <summary>
        /// Chart Whole XBar 값
        /// </summary>
        private ChartWholeValue _wholeRangeXBar = ChartWholeValue.Create();
        /// <summary>
        /// Chart Whole RS 값
        /// </summary>
        private ChartWholeValue _wholeRangeRS = ChartWholeValue.Create();
        /// <summary>
        /// XBAR 취소 Flag
        /// </summary>
        private IsSpecCancel _isCancelXBar = IsSpecCancel.Create();
        /// <summary>
        /// R 취소 Flag
        /// </summary>
        private IsSpecCancel _isCancelXR = IsSpecCancel.Create();
        /// <summary>
        /// 직접입력 WholeRange 설정. 8/21
        /// </summary>
        private ChartWholeRangeDirectValue _originalWholeRange = ChartWholeRangeDirectValue.Create();

        /// <summary>
        /// 컨트론 이벤트 일시 정지 구분 : 정지-true
        /// </summary>
        private bool _isControlEventsStop = false;

        /// <summary>
        /// 콤복박스 Chart Subgroup DataTable
        /// </summary>
        private DataTable _CboChartSubgroupDataTable = null;

        /// <summary>
        /// 공정능력 결과 Table
        /// </summary>
        public RtnPPDataTable rtnCpkData = new RtnPPDataTable();
        /// <summary>
        /// SPC 통계용 Form 공통 사용 함수 정의
        /// </summary>
        private SpcClass _spcClass = new SpcClass();
        #endregion

        #region 생성자

        /// <summary>
        /// User Control XBar Chart 생성자
        /// </summary>
        public ucXBar()
        {
            InitializeComponent();
            ChartMessage.MessageSetting();

            ControlChartDataMappingClear();

            //if (SpcFlag.isTestMode > 0)
            //{
            //    //ControlChartDataMappingTest();
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

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            //폼 로드 이벤트
            this.Load += (s, e) =>
            {
                
                ControlDefaultSetting();

                if (this.btnRulesCheck.Width > 176)
                {
                    this.btnRulesCheck.Text = "Rules Check";
                    this.btnRulesCheck.Width = 176;
                }
            };

            //Chart Paint 이벤트 - 사용자 Line 적용.
            this.ChrXbarR.Paint += (s, e) =>
            {
                if (_IsThisCboChartTypeTextChanged)
                {
                    _IsThisCboChartTypeTextChanged = false;
                    _IsPaint = false;
                }

                if (_IsPaint == false)
                {
                    ControlLinePaint(s, e);
                }
                else
                {

                    //sia확인 : ucXBar> ChrXbarR_Paint 처리.
                    if (_isControlEventsStop != false) return;


                    _IsPaint = false;
                    //ChartHitInfo hitInfo = ((sender as SmartChart).CalcHitInfo((e as MouseEventArgs).Location));
                    //SeriesPoint sp = hitInfo.SeriesPoint;

                    // Obtain hit information under the test point. 
                    ChartHitInfo hi = ChrXbarR.CalcHitInfo(_x, _y);

                    // Obtain the series point under the test point. 
                    SeriesPoint point = hi.SeriesPoint;

                    // Check whether the series point was clicked or not. 
                    if (point != null)
                    {
                        this.PopupRawDataEdit(point);
                        ControlLinePaint(s, e);
                        //this.txtPointView.Text = point.Argument.ToString();
                        //string pointInfo = string.Format("asixs 라벨: {0},  값: {1}"
                        //    , point.Argument.ToString()
                        //    , point.Values[0].ToSafeString());
                        //MessageBox.Show(pointInfo);
                    }
                }
            };

            //Control Chart Click 이벤트
            this.ChrXbarR.MouseClick += (s, e) =>
            {
                if (e.Button != MouseButtons.Left)
                {
                    _IsPaint = false;
                    return;
                }

                //sia확인 : Chart 팝업 - ChrXbarR_Click
                ChartHitInfo hitInfo = ((s as SmartChart).CalcHitInfo((e as MouseEventArgs).Location));
                SeriesPoint sp = hitInfo.SeriesPoint;
                _x = hitInfo.HitPoint.X;
                _y = hitInfo.HitPoint.Y;
                _IsPaint = true;
            };

            //Control Chart Enter Focus 이벤트
            this.ChrXbarR.Enter += (s, e) =>
            {
                spcEop.groupName = "groupName";
                spcEop.subName = "subName";
                spcEop.ChartName = ChrXbarR.Name.ToSafeString();
                spcEop.isPcChartType = false;
                SpcCpkChartEnterEventHandler?.Invoke(s, e, spcEop);
            };

            //XBar-R 차트 더블클릭 이벤트
            this.ChrXbarR.DoubleClick += (s, e) =>
            {
                if (_controlSpec != null && _controlSpec.spcOption != null)
                {
                    switch (_controlSpec.spcOption.chartType)
                    {
                        case ControlChartType.XBar_R:
                        case ControlChartType.XBar_S:
                        case ControlChartType.I_MR:
                        case ControlChartType.Merger:
                            break;
                        case ControlChartType.np:
                        case ControlChartType.p:
                        case ControlChartType.c:
                        case ControlChartType.u:
                        default:
                            IsDetailPopupLock = true;
                            break;
                    }
                }

                if (!IsDetailPopupLock)
                {
                    if (_controlSpec != null && _controlSpec != null && _spcPara != null)
                    {
                        if (_spcPara.InputData != null)
                        {
                            //TestSpcBaseChart0301Raw frm = new TestSpcBaseChart0301Raw();//Test
                            SpcStatusDetailChartPopup frm = new SpcStatusDetailChartPopup();
                            frm.ucChartDetail1.SpcDetailPopupInitialize(_controlSpec);
                            frm.ucChartDetail1.ucCpk1.IsDetailPopupLock = true;
                            frm.ucChartDetail1.ucCpk1.ControlChartDataMapping(_dtControlData, _controlSpec, _spcPara);
                            frm.ucChartDetail1.ucXBar1.IsDetailPopupLock = true;
                            frm.ucChartDetail1.ucXBar1.ControlChartDataMapping(_dtControlData, _controlSpec, _spcPara);
                            frm.ShowDialog();
                        }
                    }
                }
            };

            #region Spec Control limit Option 처리

            //Chart sigma3 표시 유무
            chkSigma3.CheckedChanged += (s, e) =>
            {
                ((XYDiagram)ChrXbarR.Diagram).AxisY.Strips["Strip03Sigma"].Visible = chkSigma3.Checked;
            };

            //Chart sigma2 표시 유무
            chkSigma2.CheckedChanged += (s, e) =>
            {
                ((XYDiagram)ChrXbarR.Diagram).AxisY.Strips["Strip02Sigma"].Visible = chkSigma2.Checked;
            };

            //Chart sigma1 표시 유무
            chkSigma1.CheckedChanged += (s, e) =>
            {
                ((XYDiagram)ChrXbarR.Diagram).AxisY.Strips["Strip01Sigma"].Visible = chkSigma1.Checked;
            };

            //Chart USL 표시 유무
            chkUSL.CheckedChanged += (s, e) =>
            {
                if (_isControlEventsStop != false) return;
                ((XYDiagram)ChrXbarR.Diagram).AxisY.ConstantLines["ConstantLineUSL"].Visible = chkUSL.Checked;
                ChrXbarR.Series["Series06OverRuleUSL"].Visible = chkUSL.Checked;
                _isCancelXBar.usl = !chkUSL.Checked;
                _controlSpec.nXbar.usl = lblUSLValue.Text.ToSafeDoubleStaMax();
                ChartWholeRangeChange(_isCancelXBar, 0);
            };

            //Chart CSL 표시 유무
            chkCSL.CheckedChanged += (s, e) =>
            {
                if (_isControlEventsStop != false) return;
                ((XYDiagram)ChrXbarR.Diagram).AxisY.ConstantLines["ConstantLineTarget"].Visible = chkCSL.Checked;
                //ChrXbarR.Series["Series04OverRuleUSL"].Visible = chkCSL.Checked;
                _isCancelXBar.csl = !chkCSL.Checked;
                ChartWholeRangeChange(_isCancelXBar, 0);
            };

            //Chart LSL 표시 유무
            chkLSL.CheckedChanged += (s, e) =>
            {
                if (_isControlEventsStop != false) return;
                ((XYDiagram)ChrXbarR.Diagram).AxisY.ConstantLines["ConstantLineLSL"].Visible = chkLSL.Checked;
                ChrXbarR.Series["Series07OverRuleLSL"].Visible = chkLSL.Checked;
                _isCancelXBar.lsl = !chkLSL.Checked;
                _controlSpec.nXbar.lsl = lblLSLValue.Text.ToSafeDoubleStaMax();
                ChartWholeRangeChange(_isCancelXBar, 0);
            };

            //Chart UCL 표시 유무
            chkUCL.CheckedChanged += (s, e) =>
            {
                if (_isControlEventsStop != false) return;
                switch (_controlSpec.spcOption.limitType)
                {
                    case LimitType.Interpretation:
                        ((XYDiagram)ChrXbarR.Diagram).AxisY.ConstantLines["ConstantLineUCL"].Visible = false;
                        break;
                    case LimitType.Management:
                    case LimitType.Direct:
                    default:
                        ((XYDiagram)ChrXbarR.Diagram).AxisY.ConstantLines["ConstantLineUCL"].Visible = chkUCL.Checked;
                        break;
                }

                ChrXbarR.Series["Series04OverRuleUCL"].Visible = chkUCL.Checked;
                _isCancelXBar.ucl = !chkUCL.Checked;
                ChartWholeRangeChange(_isCancelXBar, 0);
            };

            //Chart CCL 표시 유무
            chkCCL.CheckedChanged += (s, e) =>
            {
                if (_isControlEventsStop != false) return;
                ((XYDiagram)ChrXbarR.Diagram).AxisY.ConstantLines["ConstantLineXBAR"].Visible = chkCCL.Checked;
                _isCancelXBar.ccl = !chkCCL.Checked;
                ChartWholeRangeChange(_isCancelXBar, 0);
            };

            //Chart LCL 표시 유무
            chkLCL.CheckedChanged += (s, e) =>
            {
                if (_isControlEventsStop != false) return;
                switch (_controlSpec.spcOption.limitType)
                {
                    case LimitType.Interpretation:
                        ((XYDiagram)ChrXbarR.Diagram).AxisY.ConstantLines["ConstantLineLCL"].Visible = false;
                        break;
                    case LimitType.Management:
                    case LimitType.Direct:
                    default:
                        ((XYDiagram)ChrXbarR.Diagram).AxisY.ConstantLines["ConstantLineLCL"].Visible = chkLCL.Checked;
                        break;
                }

                ChrXbarR.Series["Series05OverRuleLCL"].Visible = chkLCL.Checked;
                _isCancelXBar.lcl = !chkLCL.Checked;
                ChartWholeRangeChange(_isCancelXBar, 0);
            };

            //Chart RUCL 표시 유무
            chkRUCL.CheckedChanged += (s, e) =>
            {
                if (_isControlEventsStop != false) return;
                ((XYDiagram)ChrXbarR.Diagram).SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRUCL"].Visible = chkRUCL.Checked;
                ChrXbarR.Series["Series14OverRuleRUCL"].Visible = chkRUCL.Checked;
                _isCancelXR.ucl = !chkRUCL.Checked;
                ChartWholeRangeChange(_isCancelXR, 1);
            };

            //Chart RCCL 표시 유무
            chkRCCL.CheckedChanged += (s, e) =>
            {
                if (_isControlEventsStop != false) return;
                ((XYDiagram)ChrXbarR.Diagram).SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRCCL"].Visible = chkRCCL.Checked;
                _isCancelXR.ccl = !chkRCCL.Checked;
                ChartWholeRangeChange(_isCancelXR, 1);
            };

            //Chart RLCL 표시 유무
            chkRLCL.CheckedChanged += (s, e) =>
            {
                if (_isControlEventsStop != false) return;
                ((XYDiagram)ChrXbarR.Diagram).SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRLCL"].Visible = chkRLCL.Checked;
                ChrXbarR.Series["Series15OverRuleRLCL"].Visible = chkRLCL.Checked;
                _isCancelXR.lcl = !chkRLCL.Checked;
                ChartWholeRangeChange(_isCancelXR, 1);
            };

            //Chart UOL 표시 유무
            chkUOL.CheckedChanged += (s, e) =>
            {
                if (_isControlEventsStop != false) return;
                ((XYDiagram)ChrXbarR.Diagram).AxisY.ConstantLines["ConstantLineUOL"].Visible = chkUOL.Checked;
                ChrXbarR.Series["Series08OverRuleUOL"].Visible = chkUOL.Checked;
                _isCancelXBar.uol = !chkUOL.Checked;
                ChartWholeRangeChange(_isCancelXBar, 0);
            };

            //Chart LOL 표시 유무
            chkLOL.CheckedChanged += (s, e) =>
            {
                if (_isControlEventsStop != false) return;
                ((XYDiagram)ChrXbarR.Diagram).AxisY.ConstantLines["ConstantLineLOL"].Visible = chkLOL.Checked;
                ChrXbarR.Series["Series09OverRuleLOL"].Visible = chkLOL.Checked;
                _isCancelXBar.lol = !chkLOL.Checked;
                ChartWholeRangeChange(_isCancelXBar, 0);
            };

            #endregion
        }

        /// <summary>
        ///Chart USL 표시 유무 
        /// </summary>
        private void chkUSLCheckedChanged()
        {
            if (_isControlEventsStop != false) return;
            ((XYDiagram)ChrXbarR.Diagram).AxisY.ConstantLines["ConstantLineUSL"].Visible = chkUSL.Checked;
            ChrXbarR.Series["Series06OverRuleUSL"].Visible = chkUSL.Checked;
            _isCancelXBar.usl = !chkUSL.Checked;
            ChartWholeRangeChange(_isCancelXBar, 0);
        }

        /// <summary>
        /// 콤보박스 설정 - Subgroup ID
        /// </summary>
        public void CoboBoxSettingSubGroupId(int itemIndex = 0)
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
                this.cboOrderMode.ItemIndex = itemIndex;
            }

            if (this.cboOrderMode.DataSource != null)
            {
                if (_CboChartSubgroupDataTable.Rows.Count > 2)
                {
                    this.lblOrderMode.Visible = true;
                    this.cboOrderMode.Visible = true;
                    this.pnlTop.Visible = true;
                }
                else
                {
                    this.lblOrderMode.Visible = false;
                    this.cboOrderMode.Visible = false;
                    this.pnlTop.Visible = false;
                }

                this.lblSubgroupId.Tag = this.cboOrderMode.GetDataValue();
                this.lblSubgroupId.Text = this.cboOrderMode.GetDisplayText();
            }

        }
        /// <summary>
        /// 서브그룹 값 등록.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
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
                            string labelData = "";
                            labelData = item.sSUBGROUPNAME.Replace("@#", @"/ ");
                            if (labelData != null && labelData != "")
                            {
                                row["Label"] = labelData;
                            }
                            else
                            {
                                row["Label"] = item.sSUBGROUP.Replace("@#", @"/ ");
                            }

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
        private void cboOrderMode_EditValueChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// option 기능 : subgroup id 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboOrderMode_TextChanged(object sender, EventArgs e)
        {
            if (_isControlEventsStop != false) return;

            if (rtnDirectChartData != null && rtnDirectChartData.Rows.Count > 0)
            {
                _controlSpec.sigmaResult.subGroup = this.cboOrderMode.GetDataValue().ToSafeString();
                _controlSpec.sigmaResult.subGroupName = this.cboOrderMode.GetDisplayText().ToSafeString();
                if (_controlSpec.sigmaResult.subGroup != null && _controlSpec.sigmaResult.subGroup != "")
                {
                    this.ControlChartDataMapping(rtnDirectChartData, _controlSpec, _spcPara);
                }
                else
                {
                    Console.WriteLine("선택 자료 없음.");
                    // 조회할 데이터가 없습니다.
                    //ShowMessage("NoSelectData");
                }
            }
        }
        private void cboChartType_EditValueChanged(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// Chart 분석 타입 구분 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboChartType_TextChanged(object sender, EventArgs e)
        {
            if (_isControlEventsStop != false) return;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _isControlEventsStop = true;
                string chartType = this.cboChartType.GetDataValue().ToSafeString();
                this.ChartLegendsTitleChange(chartType, false);
                this.DirectAnalysisExcute(chartType);

                spcEop.groupName = "groupName";
                spcEop.subName = "subName";
                spcEop.ChartName = ChrXbarR.Name.ToSafeString();
                spcEop.isPcChartType = true;
                SpcChartVtPChartTypeChangeEventHandler?.Invoke(sender, e, spcEop);
            }
            catch (Exception ex)
            {
                _isControlEventsStop = false;
                Console.WriteLine(ex.Message);
                this.Cursor = Cursors.Default;
            }
            _isControlEventsStop = false;
            _IsThisCboChartTypeTextChanged = true;
            this.Cursor = Cursors.Default;
        }
        /// <summary>
        /// Rules Check 버튼 Click 이벤트.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRulesCheck_Click(object sender, EventArgs e)
        {
            if (_spcPara != null && _spcPara.spcOption !=null && _dtControlData != null && _dtControlData.Count > 0)
            {
                RulesCheck rulesCheckData = RulesCheck.Create();
                //sia확인 : Rules 설정 - ucXBar>btnRulesCheck_Click.
                SpcControlRulesSettingPopup frmPopup = new SpcControlRulesSettingPopup();
                //Console.WriteLine(_spcPara.spcOption.chartType)
                frmPopup.spcOption = _spcPara.spcOption;
                frmPopup.RulesModeCheck();
                frmPopup.ShowDialog();
                if (frmPopup.DialogResult == DialogResult.OK)
                {
                    rulesCheckData = frmPopup.RulesCheckData;
                    Console.Write(rulesCheckData.lstRulesPara.Count);
                    if (rulesCheckData.lstRulesPara.Count > 0)
                    {
                        rulesCheckData = RulesChecking(rulesCheckData);
                    }
                }
            }
            else
            {
                this.MessageChartInternal(SpcLibMessage.common.com1001);//"분석 자료가 없습니다."
            }
        }


        #region Chart Tool Strip Menu Item 이벤트
        /// <summary>
        /// Image 복사
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspMnuItemImageCopy_Click(object sender, EventArgs e)
        {
            SpcClass.ChartImageCopy(this.ChrXbarR);
        }
        /// <summary>
        /// Image 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspMnuItemImageSave_Click(object sender, EventArgs e)
        {
            string defaultFileName = "";
            if (_SUBGROUPNAME != "")
            {
                defaultFileName = string.Format("_{0}", _SUBGROUPNAME.Replace('/', '_').Replace('*', ' ').Trim());
            }
            defaultFileName = string.Format("{0}{1}", SpcLibMessage.common.com1011, defaultFileName);
            SpcClass.ChartImageSave(this.ChrXbarR, defaultFileName, SpcLibMessage.common.com1007);
        }
        #endregion Chart Tool Strip Menu Item 이벤트

        #endregion

        #region Private Function
        /// <summary>
        /// 내부 Message 처리.
        /// </summary>
        /// <param name="message"></param>
        private void MessageChartInternal(string message)
        {
            try
            {
                spcMsg.mainMessage = message;
                SpcChartDirectMessageUserEventHandler?.Invoke(spcMsg);
            }
            catch (Exception ex)
            {
                message = string.Format("{0} - {1}", message, ex.Message);
                MessageBox.Show(message);
            }
        }
        /// <summary>
        /// InputData만 편집하여 반환함.
        /// </summary>
        /// <param name="spcPara"></param>
        /// <param name="analysisParameter"></param>
        public void DirectInputDataRead(ref SPCPara spcPara, AnalysisExecutionParameter analysisParameter)
        {
            string firstSubgroup = "";
            //Start - Raw Data 입력 Part.......................................................
            var rowDatas = spcPara.tableRawData.AsEnumerable();
            spcPara.InputData = new ParPIDataTable();
            var lstRow = rowDatas.AsParallel()
                    .OrderBy(or => or.Field<string>(analysisParameter.dtInputRawMainFieldName.subgroupNameFiled))
                    .ThenBy(or => or.Field<string>(analysisParameter.dtInputRawMainFieldName.samplingNameFiled));
            foreach (DataRow row in lstRow)
            {
                DataRow dr = spcPara.InputData.NewRow();
                if (firstSubgroup == "")
                {
                    firstSubgroup = SpcFunction.IsDbNck(row, analysisParameter.dtInputRawMainFieldName.subgroupNameFiled);
                }
                dr["GROUPID"] = 1;
                dr["SAMPLEID"] = 1;//자동증가
                dr["SUBGROUP"] = SpcFunction.IsDbNck(row, analysisParameter.dtInputRawMainFieldName.subgroupNameFiled);
                dr["SUBGROUPNAME"] = SpcFunction.IsDbNck(row, analysisParameter.dtInputRawMainFieldName.subgroupNameFiledName);
                if (analysisParameter.dtInputRawMainFieldName.subgroupNameFiledName01 != null
                    && analysisParameter.dtInputRawMainFieldName.subgroupNameFiledName01 != "")
                {
                    dr["SUBGROUPNAME01"] = SpcFunction.IsDbNck(row, analysisParameter.dtInputRawMainFieldName.subgroupNameFiledName01);
                }

                dr["SAMPLING"] = SpcFunction.IsDbNck(row, analysisParameter.dtInputRawMainFieldName.samplingNameFiled);
                dr["SAMPLINGNAME"] = SpcFunction.IsDbNck(row, analysisParameter.dtInputRawMainFieldName.samplingNameFiledName);
                if (analysisParameter.dtInputRawMainFieldName.samplingNameFiled01 != null
                        && analysisParameter.dtInputRawMainFieldName.samplingNameFiled01 != "")
                {
                    dr["SAMPLING01"] = SpcFunction.IsDbNck(row, analysisParameter.dtInputRawMainFieldName.samplingNameFiled01);
                }
                if (analysisParameter.dtInputRawMainFieldName.samplingNameFiledName01 != null
                        && analysisParameter.dtInputRawMainFieldName.samplingNameFiledName01 != "")
                {
                    dr["SAMPLINGNAME01"] = SpcFunction.IsDbNck(row, analysisParameter.dtInputRawMainFieldName.samplingNameFiledName01);
                }

                if (analysisParameter.dtInputRawMainFieldName.samplingNameFiled02 != null
                        && analysisParameter.dtInputRawMainFieldName.samplingNameFiled02 != "")
                {
                    dr["SAMPLING02"] = SpcFunction.IsDbNck(row, analysisParameter.dtInputRawMainFieldName.samplingNameFiled02);
                }
                if (analysisParameter.dtInputRawMainFieldName.samplingNameFiledName02 != null
                        && analysisParameter.dtInputRawMainFieldName.samplingNameFiledName02 != "")
                {
                    dr["SAMPLINGNAME02"] = SpcFunction.IsDbNck(row, analysisParameter.dtInputRawMainFieldName.samplingNameFiledName02);
                }
                dr["NVALUE"] = SpcFunction.IsDbNckDoubleMin(row, analysisParameter.dtInputRawMainFieldName.nValue);
                dr["NSUBVALUE"] = SpcFunction.IsDbNckDoubleMin(row, analysisParameter.dtInputRawMainFieldName.nSubValue);

                spcPara.InputData.Rows.Add(dr);
            }

            Console.WriteLine(spcPara.InputData.Rows.Count);
            //end - Raw Data 입력 Part .......................................................
        }

        /// <summary>
        /// Xbar-R Chart 직접 실행.
        /// </summary>
        public void DirectXBarRExcute(ref SPCPara spcPara, AnalysisExecutionParameter analysisParameter)
        {
            _isControlEventsStop = true;
            SPCOutData spcOutData = SPCOutData.Create();
            ControlSpec controlSpec = ControlSpec.Create();
            SPCLibs spcLib = new SPCLibs();

            SPCOption spcOption = spcPara.spcOption;

            //입력 측정 자료.
            //GROUPID" />
            //SAMPLEID" />
            //SUBGROUP" />
            //SAMPLING" />
            //NVALUE" />
            int i = 0;
            int j = 0;
            string firstSubgroup = "";
            //추정치
            spcOption.sigmaType = spcPara.isUnbiasing;

            //siaTest : ucXBar > DirectXBarRExcute
            //차트 타입 //통계분석
            string chartMainType = spcPara.ChartTypeMain();

            //Start - Raw Data 입력 Part.......................................................
            var rowDatas = spcPara.tableRawData.AsEnumerable();
            spcPara.InputData = new ParPIDataTable();
            var lstRow = rowDatas.AsParallel()
                    .OrderBy(or => or.Field<string>(analysisParameter.dtInputRawMainFieldName.subgroupNameFiled))
                    .ThenBy(or => or.Field<string>(analysisParameter.dtInputRawMainFieldName.samplingNameFiled));
            foreach (DataRow row in lstRow)
            {
                DataRow dr = spcPara.InputData.NewRow();
                if (firstSubgroup == "")
                {
                    firstSubgroup = SpcFunction.IsDbNck(row, analysisParameter.dtInputRawMainFieldName.subgroupNameFiled);
                }
                dr["GROUPID"] = 1;
                dr["SAMPLEID"] = 1;//자동증가
                dr["SUBGROUP"] = SpcFunction.IsDbNck(row, analysisParameter.dtInputRawMainFieldName.subgroupNameFiled);
                dr["SUBGROUPNAME"] = SpcFunction.IsDbNck(row, analysisParameter.dtInputRawMainFieldName.subgroupNameFiledName);
                if (analysisParameter.dtInputRawMainFieldName.subgroupNameFiledName01 != null
                    && analysisParameter.dtInputRawMainFieldName.subgroupNameFiledName01 != "")
                {
                    dr["SUBGROUPNAME01"] = SpcFunction.IsDbNck(row, analysisParameter.dtInputRawMainFieldName.subgroupNameFiledName01);
                }

                dr["SAMPLING"] = SpcFunction.IsDbNck(row, analysisParameter.dtInputRawMainFieldName.samplingNameFiled);
                dr["SAMPLINGNAME"] = SpcFunction.IsDbNck(row, analysisParameter.dtInputRawMainFieldName.samplingNameFiledName);
                if (analysisParameter.dtInputRawMainFieldName.samplingNameFiled01 != null
                        && analysisParameter.dtInputRawMainFieldName.samplingNameFiled01 != "")
                {
                    dr["SAMPLING01"] = SpcFunction.IsDbNck(row, analysisParameter.dtInputRawMainFieldName.samplingNameFiled01);
                }
                if (analysisParameter.dtInputRawMainFieldName.samplingNameFiledName01 != null
                        && analysisParameter.dtInputRawMainFieldName.samplingNameFiledName01 != "")
                {
                    dr["SAMPLINGNAME01"] = SpcFunction.IsDbNck(row, analysisParameter.dtInputRawMainFieldName.samplingNameFiledName01);
                }

                if (analysisParameter.dtInputRawMainFieldName.samplingNameFiled02 != null
                        && analysisParameter.dtInputRawMainFieldName.samplingNameFiled02 != "")
                {
                    dr["SAMPLING02"] = SpcFunction.IsDbNck(row, analysisParameter.dtInputRawMainFieldName.samplingNameFiled02);
                }
                if (analysisParameter.dtInputRawMainFieldName.samplingNameFiledName02 != null
                        && analysisParameter.dtInputRawMainFieldName.samplingNameFiledName02 != "")
                {
                    dr["SAMPLINGNAME02"] = SpcFunction.IsDbNck(row, analysisParameter.dtInputRawMainFieldName.samplingNameFiledName02);
                }
                dr["NVALUE"] = SpcFunction.IsDbNckDoubleMin(row, analysisParameter.dtInputRawMainFieldName.nValue);
                dr["NSUBVALUE"] = SpcFunction.IsDbNckDoubleMin(row, analysisParameter.dtInputRawMainFieldName.nSubValue);

                spcPara.InputData.Rows.Add(dr);
            }

            Console.WriteLine(spcPara.InputData.Rows.Count);
            //end - Raw Data 입력 Part .......................................................

            //Start - Spec Data 입력 Part.......................................................
            i = 0;
            int nLength = 0;
            spcPara.InputSpec = new ParPISpecDataTable();

            if (spcPara.tableSubgroupSpec != null && spcPara.tableSubgroupSpec.Rows.Count > 0)
            {
                nLength = spcPara.tableSubgroupSpec.Rows.Count;
                for (i = 0; i < nLength; i++)
                {
                    DataRow rowSpec = spcPara.tableSubgroupSpec.Rows[i];
                    DataRow drs = spcPara.InputSpec.NewRow();
                    drs["TEMP"] = i;
                    drs["SUBGROUP"] = SpcFunction.IsDbNck(rowSpec, analysisParameter.dtInputRawMainFieldName.subgroupNameFiled);

                    drs["CHARTTYPE"] = SpcFunction.IsDbNck(rowSpec, "CTYPE");
                    drs["USL"] = SpcFunction.IsDbNckDoubleMin(rowSpec, "USL");
                    drs["CSL"] = SpcFunction.IsDbNckDoubleMin(rowSpec, "SL");
                    drs["LSL"] = SpcFunction.IsDbNckDoubleMin(rowSpec, "LSL");
                    drs["UCL"] = SpcFunction.IsDbNckDoubleMin(rowSpec, "UCL");
                    drs["CCL"] = SpcFunction.IsDbNckDoubleMin(rowSpec, "SL");
                    drs["LCL"] = SpcFunction.IsDbNckDoubleMin(rowSpec, "LCL");
                    drs["UOL"] = SpcFunction.IsDbNckDoubleMin(rowSpec, "UOL");
                    drs["LOL"] = SpcFunction.IsDbNckDoubleMin(rowSpec, "LOL");

                    spcPara.InputSpec.Rows.Add(drs);
                }
            }
            Console.WriteLine(spcPara.InputSpec.Rows.Count);

            //End - Spec Data 입력 Part.......................................................

            //차트 타입 //통계분석
            spcOption.sigmaType = spcPara.isUnbiasing;
            controlSpec.spcOption = spcOption;
            //chartType = spcPara.ChartTypeMain();

            //subgroup 콤보 설정.
            _spcPara = spcPara;
            this.CoboBoxSettingSubGroupId(1);

            switch (chartMainType)
            {
                case "P":
                case "NP":
                case "C":
                case "U":
                    this.cboChartTypeSetting(2);
                    pnlRightTop.Visible = true;
                    break;
                default:
                    pnlRightTop.Visible = false;
                    break;
            }

            //관리도 분석
            switch (chartMainType)
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
                    rtnDirectChartData = SPCLibs.SpcLibIMR(spcPara, spcOption, ref spcOutData);
                    break;
                case "XBARP":
                case "PL":
                    spcOption.chartType = ControlChartType.Merger;
                    rtnDirectChartData = SPCLibs.SpcLibXBarP(spcPara, spcOption, ref spcOutData);
                    break;
                case "P":
                    spcOption.chartType = ControlChartType.p;
                    rtnDirectChartData = SPCLibs.SpcLibP(spcPara, spcOption, ref spcOutData);
                    break;
                case "NP":
                    spcOption.chartType = ControlChartType.np;
                    rtnDirectChartData = SPCLibs.SpcLibNP(spcPara, spcOption, ref spcOutData);
                    break;
                case "C":
                    spcOption.chartType = ControlChartType.c;
                    rtnDirectChartData = SPCLibs.SpcLibC(spcPara, spcOption, ref spcOutData);
                    break;
                case "U":
                    spcOption.chartType = ControlChartType.u;
                    rtnDirectChartData = SPCLibs.SpcLibU(spcPara, spcOption, ref spcOutData);
                    break;
                default:
                    break;
            }

            controlSpec.spcOption = spcOption;

            if (rtnDirectChartData != null && rtnDirectChartData.Rows.Count > 0)
            {
                this.ControlChartDataMapping(rtnDirectChartData, controlSpec, spcPara);
            }
            _isControlEventsStop = false;
        }


        /// <summary>
        /// Xbar-R Chart 직접 실행 - Test.
        /// </summary>
        public void DirectXBarRExcuteTest(ref SPCPara spcPara)
        {
            _isControlEventsStop = true;
            ControlSpec controlSpec = ControlSpec.Create();
            SPCOption spcOption = spcPara.spcOption;
            SPCOutData spcOutData = SPCOutData.Create();
            //SPCPara spcPara = spcParaData;
            spcPara.InputData = new ParPIDataTable();//입력 측정 자료.
            //GROUPID
            //SAMPLEID
            //SUBGROUP
            //SAMPLING
            //NSUBVALUE
            //NVALUE
            int i = 0;
            int j = 0;

            //string chartType = cboLeftChartType.GetDataValue().ToSafeString().ToUpper().Replace("_", "");
            //string chartType = "XBARS".ToUpper();
            string chartType = spcPara.ChartTypeMain();
            //spcPara.ChartTypeMain(chartType);

            //sia확인 : XBar Test 자료 입력
            //Test Part.......................................................
            string[,] pdata;


            //sia작업 : Test> ucXBar > DirectXBarRExcuteTest
            //siaTest : Test> ucXBar > DirectXBarRExcuteTest
            switch (chartType)
            {
                case "P":
                case "C":
                case "U":
                    //pdata = _spcTestData.CTR_P01();//P Chart 단일 검증용.
                    //pdata = _spcTestData.CTR_P02();//P Chart 단일 Test
                    pdata = _spcTestData.CTR_P03(); //P Chart Subgroup 다중 Test.
                    break;
                case "NP":
                    pdata = _spcTestData.CTR_P04_NP();//NP Chart 단일
                    break;
                case "IMR":
                    pdata = _spcTestData.CTR02_IMR();//단일
                    break;
                default:
                    //pdata = _spcTestData.CTR01();//단일
                    //pdata = _spcTestData.CTR01Not();//단일 - Sampling 같지 않음.
                    //pdata = _spcTestData.CTR02Not_Multi();//다중 - Sampling 같지 않음.
                    //pdata = _spcTestData.CTR03Not_Multi();//다중 - Sampling 같지 않음.
                    //pdata = _spcTestData.CTR02_PL();//단일 합동
                    //pdata = _spcTestData.CTR02Group();//그룹 /R, S Test Data
                    //pdata = _spcTestData.CTR03Group();//그룹 / R, S Test Data

                    #region Rule Check Data
                    //pdata = _spcTestData.TestDataNelsonRules01();
                    //pdata = _spcTestData.TestDataNelsonRules01_01();
                    pdata = _spcTestData.TestDataNelsonRules02_Max();
                    //pdata = _spcTestData.TestDataNelsonRules02_Min();
                    //pdata = _spcTestData.TestDataNelsonRules03();
                    //pdata = _spcTestData.TestDataNelsonRules04();
                    //pdata = _spcTestData.TestDataNelsonRules05();
                    //pdata = _spcTestData.TestDataNelsonRules06();
                    //pdata = _spcTestData.TestDataNelsonRules07();
                    //pdata = _spcTestData.TestDataNelsonRules08();
                    #endregion Rule Check Data
                    break;
            }

            

            Console.WriteLine(controlSpec.nXbar.ccl);
            //controlSpec.nXbar.usl = 30;
            //controlSpec.nXbar.lsl = 2;

            //controlSpec.nXbar.ucl = 15;
            //controlSpec.nXbar.lcl = 5;

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

                        switch (specChartType)
                        {
                            case "I":
                                drs["USL"] = 4.5f;
                                drs["CSL"] = 3f;
                                drs["LSL"] = 2.5f;
                                break;
                            default:
                                break;
                        }
                        spcPara.InputSpec.Rows.Add(drs);
                    }
                }
            }

            Console.WriteLine(spcPara.InputSpec.Rows.Count);
            //End - Spec Data 입력 Part.......................................................

            //string sigmaType = this.comboBoxType.Text.ToString();

            //차트 타입 //통계분석
            spcOption.sigmaType = spcPara.isUnbiasing;
            controlSpec.spcOption = spcOption;
            chartType = spcPara.ChartTypeMain();

            //subgroup 콤보 설정.
            _spcPara = spcPara;
            this.CoboBoxSettingSubGroupId(1);

            switch (chartType)
            {
                case "P":
                case "NP":
                case "C":
                case "U":
                    this.cboChartTypeSetting(2);
                    pnlRightTop.Visible = true;
                    break;
                default:
                    pnlRightTop.Visible = false;
                    break;
            }

            this.ChartLegendsTitleChange(spcOption.chartName.xBarChartType, false);

            //차트 타입 //통계분석
            spcOption.chartName.xBarChartType = spcPara.ChartTypeMain();
            spcOption.chartName.xCpkChartType = spcPara.ChartTypeMain();
            switch (spcOption.chartName.xBarChartType)
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
                case "MR":
                case "I":
                    spcOption.chartType = ControlChartType.I_MR;
                    rtnDirectChartData = SPCLibs.SpcLibIMR(spcPara, spcOption, ref spcOutData);
                    break;
                case "XBARP":
                case "PL":
                    spcOption.chartType = ControlChartType.Merger;
                    rtnDirectChartData = SPCLibs.SpcLibXBarP(spcPara, spcOption, ref spcOutData);
                    break;
                case "P":
                    spcOption.chartType = ControlChartType.p;
                    rtnDirectChartData = SPCLibs.SpcLibP(spcPara, spcOption, ref spcOutData);
                    break;
                case "NP":
                    spcOption.chartType = ControlChartType.np;
                    rtnDirectChartData = SPCLibs.SpcLibNP(spcPara, spcOption, ref spcOutData);
                    break;
                case "C":
                    spcOption.chartType = ControlChartType.c;
                    rtnDirectChartData = SPCLibs.SpcLibC(spcPara, spcOption, ref spcOutData);
                    break;
                case "U":
                    spcOption.chartType = ControlChartType.u;
                    rtnDirectChartData = SPCLibs.SpcLibU(spcPara, spcOption, ref spcOutData);
                    break;
                default:
                    break;
            }

            int nMaxIndex = rtnDirectChartData.Rows.Count - 1;
            DataRow dataRow = rtnDirectChartData.Rows[nMaxIndex];
            spcPara.rtnXBar.XBAR = dataRow["XBAR"].ToSafeDoubleStaMin();
            spcPara.rtnXBar.XRSP = dataRow["RBAR"].ToSafeDoubleStaMin();
            spcPara.rtnXBar.UCL =  dataRow["UCL"].ToSafeDoubleStaMin();
            spcPara.rtnXBar.LCL =  dataRow["LCL"].ToSafeDoubleStaMin();
            spcPara.rtnXBar.RUCL = dataRow["RUCL"].ToSafeDoubleStaMin();
            spcPara.rtnXBar.RLCL = dataRow["RLCL"].ToSafeDoubleStaMin();

            //sia확인 : CP 실행 - UC- Grid ucXBarGrid 실행. (통계분석)
            spcOption.chartName.xCpkChartType = chartType;
            rtnCpkData = SPCLibs.SpcLibPpkCbMuti(spcPara, spcOption, ref spcOutData);
            spcPara.cpkOutData = spcOutData;

            string subGroupBefore = "CL07";
            string subGroupNameBefore = "CL07";
            double nXBar = spcPara.rtnXBar.XBAR;
            controlSpec.sigmaResult = _spcClass.GetSigmaData(controlSpec, rtnCpkData, subGroupBefore, nXBar);
            controlSpec.sigmaResult = _spcClass.GetXBarSigmaData(controlSpec.sigmaResult, controlSpec, spcPara.rtnXBar, subGroupBefore);

            controlSpec.sigmaResult.subGroupName = string.Format("{0}{1}", SpcLimit.ChartGroupLayoutTitleSpace, subGroupNameBefore.ToSafeString());


            Console.WriteLine(rtnCpkData.Rows.Count);
            if (rtnDirectChartData != null && rtnDirectChartData.Rows.Count > 0)
            {
                this.ControlChartDataMapping(rtnDirectChartData, controlSpec, spcPara);
            }

            this.ChrXbarR.Refresh();
            _isControlEventsStop = false;
        }

        /// <summary>
        /// 직접 관리도 분석 
        /// </summary>
        private void DirectAnalysisExcute(string chartType)
        {
            SPCOutData spcOutData = SPCOutData.Create();
            switch (chartType)
            {
                case "XBARR":
                case "R":
                    _spcPara.spcOption.chartType = ControlChartType.XBar_R;
                    rtnDirectChartData = SPCLibs.SpcLibXBarR(_spcPara, _spcPara.spcOption, ref spcOutData);
                    break;
                case "XBARS":
                case "S":
                    _spcPara.spcOption.chartType = ControlChartType.XBar_S;
                    rtnDirectChartData = SPCLibs.SpcLibXBarS(_spcPara, _spcPara.spcOption, ref spcOutData);
                    break;
                case "IMR":
                case "I":
                    _spcPara.spcOption.chartType = ControlChartType.I_MR;
                    break;
                case "XBARP":
                case "PL":
                    _spcPara.spcOption.chartType = ControlChartType.Merger;
                    break;
                case "P":
                    _spcPara.spcOption.chartType = ControlChartType.p;
                    rtnDirectChartData = SPCLibs.SpcLibP(_spcPara, _spcPara.spcOption, ref spcOutData);
                    break;
                case "NP":
                    _spcPara.spcOption.chartType = ControlChartType.np;
                    rtnDirectChartData = SPCLibs.SpcLibNP(_spcPara, _spcPara.spcOption, ref spcOutData);
                    break;
                case "C":
                    _spcPara.spcOption.chartType = ControlChartType.c;
                    rtnDirectChartData = SPCLibs.SpcLibC(_spcPara, _spcPara.spcOption, ref spcOutData);
                    break;
                case "U":
                    _spcPara.spcOption.chartType = ControlChartType.u;
                    rtnDirectChartData = SPCLibs.SpcLibU(_spcPara, _spcPara.spcOption, ref spcOutData);
                    break;
                default:
                    break;
            }


            if (rtnDirectChartData != null && rtnDirectChartData.Rows.Count > 0)
            {
                _controlSpec.spcOption = _spcPara.spcOption;
                this.ControlChartDataMapping(rtnDirectChartData, _controlSpec, _spcPara);
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
            //string pointInfo = string.Format("asixs 라벨: {0},  값: {1}", point.Argument.ToString(), point.Values[0].ToSafeString());
            string subGroupId = string.Empty;
            string subGroupName = string.Empty;
            subGroupId = _SUBGROUP;//sia필수확인 : 8/8 SubGroup이 변경 되지 않음.
            subGroupName = _SUBGROUPNAME;
            //Console.WriteLine(_controlSpec.sigmaResult.cpkResult.SUBGROUP);
            //Console.WriteLine(_spcPara.InputData.Rows.Count);
            //Console.WriteLine(_spcPara.InputData.Rows.Count);

            switch (_controlSpec.spcOption.chartType)
            {
                case ControlChartType.XBar_R:
                case ControlChartType.XBar_S:
                case ControlChartType.I_MR:
                case ControlChartType.Merger:
                    break;
                case ControlChartType.np:
                case ControlChartType.p:
                case ControlChartType.c:
                case ControlChartType.u:
                default:
                    if (subGroupId == "")
                    {
                        subGroupId = this.lblSubgroupId.Tag.ToSafeString();
                        subGroupName = this.lblSubgroupId.Text.ToSafeString();
                    }
                    break;
            }

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
                newRow["NSUBVALUE"] = SpcFunction.IsDbNckDoubleMin(item, "NSUBVALUE");
                ChartRawData.Rows.Add(newRow);
            }

            SpcStatusRawDataPopup frm = new SpcStatusRawDataPopup
            {
                RawData = ChartRawData,
                subgroupId = subGroupId,
                chartType = _controlSpec.spcOption.chartType,
                gridTitle = string.Format("{0}", subGroupName)
            };

            switch (_controlSpec.spcOption.chartType)
            {
                case ControlChartType.XBar_R:
                case ControlChartType.XBar_S:
                case ControlChartType.I_MR:
                case ControlChartType.Merger:
                    break;
                case ControlChartType.np:
                case ControlChartType.p:
                case ControlChartType.c:
                case ControlChartType.u:
                default:
                    frm.GridHeddenPCU();
                    break;
            }

            frm.ShowDialog();

        }

        /// <summary>
        /// Chart Paint - 해석용 관리선 처리.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlLinePaint(object sender, PaintEventArgs e)
        {
            if (_isInterpretation.LCL != true && _isInterpretation.UCL != true)
            {
                return;
            }

            try
            {

                XYDiagram diag = (XYDiagram)ChrXbarR.Diagram;
                XYDiagramPane secondDiagPane = diag.Panes["SecondPane"];

                Axis2D axis2DX = diag.SecondaryAxesX["SecondaryAxisX01"];
                Axis2D axis2DY = diag.SecondaryAxesY["SecondaryAxisY01"];
                ControlCoordinates ccmin = diag.DiagramToPoint(diag.AxisX.VisualRange.MinValue.ToString(), (double)diag.AxisY.VisualRange.MinValue);
                ControlCoordinates ccmax = diag.DiagramToPoint(diag.AxisX.VisualRange.MaxValue.ToString(), (double)diag.AxisY.VisualRange.MaxValue);

                double subMinValue = (double)diag.SecondaryAxesY["SecondaryAxisY01"].VisualRange.MinValue;
                double subMaxValue = (double)diag.SecondaryAxesY["SecondaryAxisY01"].VisualRange.MaxValue;
                
                ControlCoordinates ccminSub = diag.DiagramToPoint(diag.AxisX.VisualRange.MinValue.ToString(), subMinValue, axis2DX, axis2DY, secondDiagPane);
                ControlCoordinates ccmaxSub = diag.DiagramToPoint(diag.AxisX.VisualRange.MaxValue.ToString(), subMaxValue, axis2DX, axis2DY, secondDiagPane);

                //ControlCoordinates ccminSub = diag.DiagramToPoint(diag.SecondaryAxesX["SecondaryAxisY01"].VisualRange.MinValue.ToString(), (double)diag.SecondaryAxesY["SecondaryAxisY01"].VisualRange.MinValue, axis2DX, axis2DY, secondDiagPane);
                //ControlCoordinates ccmaxSub = diag.DiagramToPoint(diag.SecondaryAxesX["SecondaryAxisY01"].VisualRange.MaxValue.ToString(), (double)diag.SecondaryAxesY["SecondaryAxisY01"].VisualRange.MaxValue, axis2DX, axis2DY, secondDiagPane);
                //ControlCoordinates ccmaxSub = diag.DiagramToPoint(diag.SecondaryAxesY["SecondaryAxisY01"].WholeRange.MaxValue.ToString(), (double)diag.SecondaryAxesY["SecondaryAxisY01"].VisualRange.MaxValue, axis2DX, axis2DY, secondDiagPane);

                //txtPointView.Text = string.Format("X: {0},  Y: {1}", ccmin.Point.X, ccmin.Point.Y);

                int height = ccmin.Point.Y - ccmax.Point.Y;
                object op = ChrXbarR.Series[0].Points[0].Values;
                Diagram uDiagram1 = ChrXbarR.Diagram;
                XYDiagram diagram = ChrXbarR.Diagram as XYDiagram;
                Type chartType = ChrXbarR.Series[0].GetType();

                //diagram.DefaultPane.Weight
                string axisXLabel = "";
                double axisXValueAUCL = double.NaN;
                double axisXValueALCL = double.NaN;
                double axisXValueBefore = double.NaN;

                double axisXValueSubAUCL = double.NaN;
                double axisXValueSubALCL = double.NaN;
                double axisXValueSubBefore = double.NaN;

                int nlen = ChrXbarR.Series["Series03Value"].Points.Count;
                int nlenSub = ChrXbarR.Series["Series13Value"].Points.Count;
                ControlCoordinates ccBase;
                ControlCoordinates ccBefore;
                ControlCoordinates ccObjectAUCL = null;
                ControlCoordinates ccBeforeALCL = null;
                ControlCoordinates ccObjectALCL = null;

                ControlCoordinates ccBeforeSub = null;
                ControlCoordinates ccObjectSubAUCL = null;
                ControlCoordinates ccBeforeSubALCL = null;
                ControlCoordinates ccObjectSubALCL = null;

                float xCap = 0f;
                //float yCap = float.NaN;

                float xCapSub = 0f;
                //float yCapSub = float.NaN;

                int i = 0;
                //xCap
                for (i = 0; i < nlen; i++)
                {
                    axisXValueBefore = axisXValueAUCL;
                    axisXValueSubBefore = axisXValueSubAUCL;

                    axisXLabel = ChrXbarR.Series["Series01AUCL"].Points[i].Argument.ToString();

                    axisXValueAUCL = ChrXbarR.Series["Series01AUCL"].Points[i].Values[0];
                    axisXValueALCL = ChrXbarR.Series["Series02ALCL"].Points[i].Values[0];

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

                        axisXLabel = ChrXbarR.Series["Series11AUCL"].Points[i].Argument.ToString();
                        axisXLabel = ChrXbarR.Series["Series11AUCL"].Points[i].Argument.ToString();
                        axisXValueSubAUCL = GetSeriesValue(ChrXbarR, "Series11AUCL", i);
                        axisXValueSubALCL = GetSeriesValue(ChrXbarR, "Series12ALCL", i);

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

                    if (xCapSub == 0f)
                    {
                        xCapSub = (ccObjectSubAUCL.Point.X / 5) + 1.4f;
                    }
                }

                bool firstLineCheck = false;
                bool firstLineSubCheck = false;
                for (i = 0; i < nlen; i++)
                {
                    axisXValueBefore = axisXValueAUCL;
                    axisXLabel = ChrXbarR.Series["Series01AUCL"].Points[i].Argument.ToString();
                    axisXValueAUCL = (double)ChrXbarR.Series["Series01AUCL"].Points[i].Values[0];
                    axisXValueALCL = (double)ChrXbarR.Series["Series02ALCL"].Points[i].Values[0];

#if DEBUG
                    Console.Write(axisXLabel + " : ");
                    Console.WriteLine(axisXValueAUCL);
#endif

                    axisXValueSubBefore = axisXValueSubAUCL;
                    axisXValueSubAUCL = GetSeriesValue(ChrXbarR, "Series11AUCL", i);
                    axisXValueSubALCL = GetSeriesValue(ChrXbarR, "Series12ALCL", i);

                    ccBefore = ccObjectAUCL;
                    ccObjectAUCL = diag.DiagramToPoint(axisXLabel, axisXValueAUCL);
                    ccBeforeALCL = ccObjectALCL;
                    ccObjectALCL = diag.DiagramToPoint(axisXLabel, axisXValueALCL);

                    if (isSubChart != false)
                    {
                        ccBeforeSub = ccObjectSubAUCL;
                        ccObjectSubAUCL = diag.DiagramToPoint(axisXLabel, axisXValueSubAUCL, axis2DX, axis2DY, secondDiagPane);
                        ccBeforeSubALCL = ccObjectSubALCL;
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
                            if (y1 <= ccmin.Point.Y && y1 >= ccmax.Point.Y)
                            {
                                if (ccObjectAUCL.Point.X >= ccmin.Point.X && ccObjectAUCL.Point.X <= ccmax.Point.X)
                                {
                                    e.Graphics.DrawLine(uslPenH, x1, y1, x2, y2);
                                }
                            }
                        }
                        //LCL 좌표값 입력.
                        float yL1 = ccObjectALCL.Point.Y;
                        float yL2 = ccObjectALCL.Point.Y;
                        //Pen uslPenHALCL = new Pen(Color.FromArgb(0, 0, 255), 0.1f);
                        //uslPenH.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                        if (_isInterpretation.LCL != false)
                        {
                            if (yL1 <= ccmin.Point.Y && yL1 >= ccmax.Point.Y)
                            {
                                if (ccObjectALCL.Point.X >= ccmin.Point.X && ccObjectALCL.Point.X <= ccmax.Point.X)
                                {
                                    e.Graphics.DrawLine(uslPenH, x1, yL1, x2, yL2);
                                }
                            }
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
                                if (y11 <= ccminSub.Point.Y && y11 >= ccmaxSub.Point.Y)
                                {
                                    if (ccObjectSubAUCL.Point.X >= ccminSub.Point.X && ccObjectSubAUCL.Point.X <= ccmaxSub.Point.X)
                                    {
                                        e.Graphics.DrawLine(uslPenHSub, x11, y11, x12, y12);
                                    }
                                }
                            }

                            //RLCL 좌표값 입력.
                            float yL11 = ccObjectSubALCL.Point.Y;
                            float yL12 = ccObjectSubALCL.Point.Y;
                            //Pen uslPenHALCL = new Pen(Color.FromArgb(0, 0, 255), 0.1f);
                            //uslPenH.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                            if (_isInterpretation.RLCL != false)
                            {
                                if (yL11 <= ccminSub.Point.Y && yL11 >= ccmaxSub.Point.Y)
                                {
                                    if (ccObjectSubALCL.Point.X >= ccminSub.Point.X && ccObjectSubALCL.Point.X <= ccmaxSub.Point.X)
                                    {
                                        e.Graphics.DrawLine(uslPenHSub, x11, yL11, x12, yL12);
                                    }
                                }
                                
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

            ((XYDiagram)ChrXbarR.Diagram).AxisY.ConstantLines["ConstantLineUOL"].Visible = false;
            ((XYDiagram)ChrXbarR.Diagram).AxisY.ConstantLines["ConstantLineLOL"].Visible = false;
        }

        /// <summary>
        /// Raw Data Copy
        /// </summary>
        /// <param name="dtControlData"></param>
        /// <returns></returns>
        public RtnControlDataTable rtnControlDataTableCopyDeep(RtnControlDataTable dtControlData)
        {
            RtnControlDataTable returnData = new RtnControlDataTable();
            if (dtControlData != null)
            {
                foreach (DataRow rowData in dtControlData)
                {
                    returnData.ImportRow(rowData);
                }
            }
            return returnData;
        }

        //화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 관리도 Chart Test Data 입력.
        /// </summary>
        public void ControlChartDataMapping(RtnControlDataTable dtControlData, ControlSpec controlSpec, SPCPara spcPara)
        {
            //입력자료 전이
            _nChartName = this.Name;

            //_dtControlData = dtControlData;
            //_controlSpec = controlSpec;
            //_spcPara = spcPara;

            _dtControlData = rtnControlDataTableCopyDeep(dtControlData);
            _controlSpec = controlSpec.CopyDeep();
            _spcPara = spcPara.CopyDeep();

            _SUBGROUP = _controlSpec.sigmaResult.subGroup;
            _SUBGROUPNAME = _controlSpec.sigmaResult.subGroupName;


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
        public void ChartWholeRangeChange(IsSpecCancel tempIsCancel, int isChart)
        {
            XYDiagram diagram = (XYDiagram)ChrXbarR.Diagram;
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
            XYDiagram diagram = (XYDiagram)ChrXbarR.Diagram;
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
            XYDiagram diagram = (XYDiagram)ChrXbarR.Diagram;
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
            //sia확인 : 02.관리도 Chart Data 입력 및 Mapping.
            SpcParameter xbarData = SpcParameter.Create();
            //ControlSpec controlSpec = new ControlSpec();
            this.DetailChartControlClearPCU(LayoutVisibility.Always);
            DataRow rowSpcData;
            XYDiagram diagram = (XYDiagram)ChrXbarR.Diagram;
            XYDiagramDefaultPane defaultDiagPane = diagram.DefaultPane;
            XYDiagramPane secondDiagPane = diagram.Panes["SecondPane"];

            this.ChrXbarR.Series["Series03Value"].Label.TextPattern = "Value {A}:{V:#0.000}";
            this.ChrXbarR.Series["Series06OverRuleUSL"].Label.TextPattern = "{A}:{V:#0.000}";
            this.ChrXbarR.Series["Series07OverRuleLSL"].Label.TextPattern = "{A}:{V:#0.000}";

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

            #region Sampling 개수가 같을때 처리.
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
                //sia확인 : 확인처리.
                MessageBox.Show(ex.Message);
            }
            #endregion Sampling 개수가 같을때 처리.

            #region Sampling 개수가 다를때 처리. 
            //1/7추가
            try
            {
                this.lblSpace03.Text = "";
                this.lblSpace03.ToolTip = "";

                if (_controlSpec.spcOption.isSame)
                {
                    this.lblSpace03.Text = SpcLibMessage.common.comCpk1038;//같지 않음
                    this.lblSpace03.ToolTip = SpcLibMessage.common.comCpk1039;//Subgroup별 Sampling 수가 같지 않음.

                    //해석용 일때.
                    switch (_controlSpec.spcOption.limitType)
                    {
                        case LimitType.Interpretation:
                            var rowDatas = _dtControlData.AsEnumerable();
                            var rw = rowDatas.LastOrDefault();

                            //XBAR
                            tempSpecXBar.XDBar = rw.XBAR.ToSafeDoubleStaMax();
                            tempSpecXBar.ucl = rw.UCL.ToSafeDoubleStaMax();
                            tempSpecXBar.ccl = rw.CL.ToSafeDoubleStaMax();
                            tempSpecXBar.lcl = rw.LCL.ToSafeDoubleStaMin();

                            //R
                            tempSpecXR.XDRar = rw.RCL.ToSafeDoubleStaMax();
                            tempSpecXR.ucl = rw.RUCL.ToSafeDoubleStaMax();
                            tempSpecXR.ccl = rw.RCL.ToSafeDoubleStaMax();
                            tempSpecXR.lcl = rw.RLCL.ToSafeDoubleStaMin();
                            switch (_controlSpec.spcOption.chartType)
                            {
                                case ControlChartType.XBar_R:
                                    break;
                                case ControlChartType.XBar_S:
                                    if (_controlSpec.spcOption.sigmaType == SigmaType.No)
                                    {
                                        tempSpecXR.XDRar = rw.RBAR.ToSafeDoubleStaMax();
                                    }
                                    break;
                                case ControlChartType.I_MR:
                                    break;
                                case ControlChartType.Merger:
                                    break;
                                case ControlChartType.np:
                                    break;
                                case ControlChartType.p:
                                    break;
                                case ControlChartType.c:
                                    break;
                                case ControlChartType.u:
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case LimitType.Management:
                            break;
                        case LimitType.Direct:
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion Sampling 개수가 다를때 처리.

            //해석용 구분
            _IsPaint = false;
            switch (_controlSpec.spcOption.limitType)
            {
                case LimitType.Interpretation:
                    this.lblSpace02.Text = SpcLibMessage.common.comCpk1031;//해석용
                    _controlSpec.nXbar.ucl = tempSpecXBar.ucl;
                    //_controlSpec.nXbar.ccl = tempSpecXBar.ccl;
                    _controlSpec.nXbar.lcl = tempSpecXBar.lcl;
                    _controlSpec.nR.ucl = tempSpecXR.ucl;
                    _controlSpec.nR.ccl = tempSpecXR.ccl;
                    _controlSpec.nR.lcl = tempSpecXR.lcl;
                    break;
                case LimitType.Management:
                    this.lblSpace02.Text = SpcLibMessage.common.comCpk1040;//관리용
                    break;
                case LimitType.Direct:
                    this.lblSpace02.Text = SpcLibMessage.common.comCpk1041;//직접입력
                    break;
                default:
                    this.lblSpace02.Text = "";
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
            ChrXbarR.Series["Series06OverRuleUSL"].Visible = true;
            ChrXbarR.Series["Series07OverRuleLSL"].Visible = true;
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

            this.ChrXbarR.DataSource = xbarData.SpcData;

            #region Chart Series Mapping
            // Create a series, and add it to the chart. 
            //Series series1 = new Series("My Series", ViewType.Bar);
            this.ChrXbarR.Series["Series01AUCL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series01AUCL"].ValueDataMembers.AddRange(new string[] { "AUCL" });
            this.ChrXbarR.Series["Series02ALCL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series02ALCL"].ValueDataMembers.AddRange(new string[] { "ALCL" });

            this.ChrXbarR.Series["Series03Value"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series03Value"].ValueDataMembers.AddRange(new string[] { "nValue" });
            //this.ChrXbarR.Series["Series03Value"].ValueDataMembers.AddRange(new string[] { "nValue", "ALCL" });


            switch (_controlSpec.spcOption.chartType)
            {
                case ControlChartType.XBar_R:
                case ControlChartType.XBar_S:
                case ControlChartType.I_MR:
                case ControlChartType.Merger:
                    this.ChrXbarR.Series["Series11AUCL"].ArgumentDataMember = "Label";
                    this.ChrXbarR.Series["Series11AUCL"].ValueDataMembers.AddRange(new string[] { "RAUCL" });
                    this.ChrXbarR.Series["Series12ALCL"].ArgumentDataMember = "Label";
                    this.ChrXbarR.Series["Series12ALCL"].ValueDataMembers.AddRange(new string[] { "RALCL" });
                    this.ChrXbarR.Series["Series13Value"].ArgumentDataMember = "Label";
                    this.ChrXbarR.Series["Series13Value"].ValueDataMembers.AddRange(new string[] { "nRValue" });
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
            this.ChrXbarR.Series["Series04OverRuleUCL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series04OverRuleUCL"].ValueDataMembers.AddRange(new string[] { "nOverRuleUCL" });
            //LCL
            this.ChrXbarR.Series["Series05OverRuleLCL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series05OverRuleLCL"].ValueDataMembers.AddRange(new string[] { "nOverRuleLCL" });

            //USL OverRules - Spec
            this.ChrXbarR.Series["Series06OverRuleUSL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series06OverRuleUSL"].ValueDataMembers.AddRange(new string[] { "nOverRuleUSL" });
            //LSL
            this.ChrXbarR.Series["Series07OverRuleLSL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series07OverRuleLSL"].ValueDataMembers.AddRange(new string[] { "nOverRuleLSL" });

            //UOL OverRules - Outlier
            this.ChrXbarR.Series["Series08OverRuleUOL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series08OverRuleUOL"].ValueDataMembers.AddRange(new string[] { "nOverRuleUOL" });
            //LOL
            this.ChrXbarR.Series["Series09OverRuleLOL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series09OverRuleLOL"].ValueDataMembers.AddRange(new string[] { "nOverRuleLOL" });

            //LSL OverRules
            this.ChrXbarR.Series["Series14OverRuleRUCL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series14OverRuleRUCL"].ValueDataMembers.AddRange(new string[] { "nROverRuleUCL" });

            this.ChrXbarR.Series["Series15OverRuleRLCL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series15OverRuleRLCL"].ValueDataMembers.AddRange(new string[] { "nROverRuleLCL" });



            #endregion

            pnlMain.Dock = DockStyle.Fill;

            #region Option - Text View (Chart)

            string textView = "";
            textView = Math.Round(_controlSpec.sigmaResult.cpkResult.nCP, SpcDefVal.disOpt.decPoint.Cpk).ToSafeString();
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

            textView = Math.Round(_controlSpec.sigmaResult.cpkResult.nCPK, SpcDefVal.disOpt.decPoint.Cpk).ToSafeString();
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

            textView = Math.Round(_controlSpec.sigmaResult.cpkResult.nPP, SpcDefVal.disOpt.decPoint.Cpk).ToSafeString();
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

            textView = Math.Round(_controlSpec.sigmaResult.cpkResult.nPPK, SpcDefVal.disOpt.decPoint.Cpk).ToSafeString();
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
            textView = Math.Round(_controlSpec.nXbar.ucl, SpcDefVal.disOpt.decPoint.Control).ToSafeString();
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
            textView = Math.Round(_controlSpec.nXbar.XDBar, SpcDefVal.disOpt.decPoint.Control).ToSafeString();
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
            textView = Math.Round(_controlSpec.nXbar.lcl, SpcDefVal.disOpt.decPoint.Control).ToSafeString();
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
            textView = Math.Round(_controlSpec.nXbar.usl, SpcDefVal.disOpt.decPoint.Spec).ToSafeString();
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
            textView = Math.Round(_controlSpec.nXbar.csl, SpcDefVal.disOpt.decPoint.Spec).ToSafeString();
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
            textView = Math.Round(_controlSpec.nXbar.lsl, SpcDefVal.disOpt.decPoint.Spec).ToSafeString();
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
            textView = Math.Round(_controlSpec.nXbar.uol, SpcDefVal.disOpt.decPoint.Uot).ToSafeString();
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
            textView = Math.Round(_controlSpec.nXbar.lol, SpcDefVal.disOpt.decPoint.Uot).ToSafeString();
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
            textView = Math.Round(_controlSpec.nR.ucl, SpcDefVal.disOpt.decPoint.RControl).ToSafeString();
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
            textView = Math.Round(_controlSpec.nR.XDRar, SpcDefVal.disOpt.decPoint.RControl).ToSafeString();
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
            textView = Math.Round(_controlSpec.nR.lcl, SpcDefVal.disOpt.decPoint.RControl).ToSafeString();
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

            //관리도 Option Display
            string viewChartType = "";
            string viewSigmaType = "";
            //controlSpec.spcOption.sigmaType;
            if (_controlSpec != null && _controlSpec.spcOption != null)
            {
                viewChartType = _controlSpec.spcOption.chartType.ToString().Replace('_', '-').ToUpper();
                //if("MERGER")
                switch (viewChartType)
                {
                    case "MERGER":
                    case "PL":
                        viewChartType = SpcLibMessage.common.comCpk1042; ;//SBar 합동
                        break;
                    default:
                        break;
                }
                if (_controlSpec.spcOption.sigmaType == SigmaType.Yes)
                {
                    viewSigmaType = SpcLibMessage.common.comCpk1035;//추정치 사용
                }
                else
                {
                    viewSigmaType = SpcLibMessage.common.comCpk1036;//추정치 미사용
                }
            }
            else
            {
                viewSigmaType = SpcLibMessage.common.comCpk1037;//추정치 미선택
            }

            viewSigmaType = string.Format("{0}, {1}", viewSigmaType, viewChartType);
            this.lblSpace01.Text = viewSigmaType;

            //if (defaultDiagPane != null)
            //{
            //    secondDiagPane.
            //    defaultDiagPane.Title.Visibility = DefaultBoolean.True;
            //}

            #region Chart Option - Sigma Text View

            try
            {
                //Sigma 1
                textView = _controlSpec.sigmaResult.nSigma1Round.ToSafeString();
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                this.lblSigma1Value.Text = "Error";
            }


            //sia확인 : Chart에 Data Mapping, ControlChartDataMapping
            //Sigma 2

            try
            {
                textView = _controlSpec.sigmaResult.nSigma2Round.ToSafeString();
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                this.lblSigma2Value.Text = "Error";
            }

            //Sigma 3

            try
            {
                textView = _controlSpec.sigmaResult.nSigma3Round.ToSafeString();
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

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                this.lblSigma3Value.Text = "Error";
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

            //UCL --> Control Limit 구분이 해석용 일때 처리됨.
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
                diagram.AxisY.ConstantLines["ConstantLineTarget"].AxisValue = "";
                diagram.AxisY.ConstantLines["ConstantLineTarget"].Visible = false;
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

            this.ChrXbarR.Refresh();
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
            _isControlEventsStop = true;
            //sia확인 : 02.관리도 Chart Data 입력 및 Mapping.
            SpcParameter xbarData = SpcParameter.Create();
            ControlSpec tempControlSpec = new ControlSpec();
            ChartWholeValue tempWholeXPCU = ChartWholeValue.Create();
            IsSpecCancel tempIsCancel = IsSpecCancel.Create();

            DataRow rowSpcData;
            double nXbarValue = SpcLimit.MAX;
            //double nXRValue = SpcLimit.MAX;
            xbarData.SpcData.Rows.Clear();
            string subgroupID = "";
            string subgroupIDCheck = "";
            if (pnlTop.Visible)
            {
                subgroupID = this.cboOrderMode.GetDataValue().ToSafeString();
                if (_dtControlData != null)
                {
                    foreach (DataRow r in _dtControlData)
                    {
                        subgroupIDCheck = r["SUBGROUP"].ToSafeString();
                        if (subgroupIDCheck == subgroupID)
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
                }
                else
                {
                    return;
                }
            }
            else
            {
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
                        xbarData.SpcData.Rows.Add(rowSpcData);
                    }
                }
            }


            //해석용 구분
            _IsPaint = false;

            //OverRules Check
            //xbarData.SpecCheckXBar(_controlSpec, ref xbarData);

            this.ChrXbarR.DataSource = xbarData.SpcData;

            //마지막 값 처리.
            var rowLastData = xbarData.SpcData.AsEnumerable();
            var lstLastRow = rowLastData
                .AsParallel()
                //.Where(w => w.Field<string>("ctype") == "XBARR")
                .Select(s => new
                {
                    Label = s.Field<string>("Label"),
                    nValue = s.Field<double>("nValue"),
                    AUCL = s.Field<double>("AUCL"),
                    ALCL = s.Field<double>("ALCL")
                })
                .OrderBy(or => or.Label)
                .LastOrDefault();

            if (lstLastRow != null)
            {
                _controlSpec.nXbar.usl = lstLastRow.AUCL.ToSafeDoubleStaMin();
                _controlSpec.nXbar.lsl = lstLastRow.ALCL.ToSafeDoubleStaMax();
            }
            else
            {
                _controlSpec.nXbar.usl = SpcLimit.MIN;
                _controlSpec.nXbar.lsl = SpcLimit.MAX;
            }
            _controlSpec.sigmaResult.nSigma3Max = _controlSpec.nXbar.usl;
            _controlSpec.sigmaResult.nSigma3Min = _controlSpec.nXbar.lsl;

            //MAX값 처리.
            var rowDatas = xbarData.SpcData.AsEnumerable();
            var query = from r in rowDatas
                        group r by 1 into g
                        select new
                        {
                            Style = g.Key,
                            maxValue = g.Max(s => s.Field<double>("nValue")),
                            minValue = g.Min(s => s.Field<double>("nValue")),
                            maxAUCL = g.Max(s => s.Field<double>("AUCL")),
                            minALCL = g.Min(s => s.Field<double>("ALCL")),
                            //AverageListPrice = g.Average(product => product.Field<Decimal>("ListPrice"))
                            nCount = g.Count()
                        };
            foreach (var rw in query)
            {
                _controlSpec.nXbar.BarMax = rw.maxValue.ToSafeDoubleStaMin();
                _controlSpec.nXbar.BarMin = rw.maxValue.ToSafeDoubleStaMax();
                _controlSpec.nXbar.ucl = rw.maxAUCL.ToSafeDoubleStaMax();
                _controlSpec.nXbar.lcl = rw.minALCL.ToSafeDoubleStaMax();
                //rowMaxInputData = rw.nCount;
                Console.WriteLine("Product style: {0} nCount: {1}", rw.Style, rw.nCount);
            }

            #region Chart Series Mapping
            // Create a series, and add it to the chart. 
            //Series series1 = new Series("My Series", ViewType.Bar);
            this.ChrXbarR.Series["Series01AUCL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series01AUCL"].ValueDataMembers.AddRange(new string[] { "AUCL" });
            this.ChrXbarR.Series["Series02ALCL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series02ALCL"].ValueDataMembers.AddRange(new string[] { "ALCL" });

            this.ChrXbarR.Series["Series03Value"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series03Value"].ValueDataMembers.AddRange(new string[] { "nValue" });

            //this.ChrXbarR.Series["Series11AUCL"].Visible = false;
            //this.ChrXbarR.Series["Series12ALCL"].Visible = false;
            //this.ChrXbarR.Series["Series13Value"].Visible = false;

            //UCL OverRules
            this.ChrXbarR.Series["Series04OverRuleUCL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series04OverRuleUCL"].ValueDataMembers.AddRange(new string[] { "nOverRuleUCL" });
            //LCL OverRules
            this.ChrXbarR.Series["Series05OverRuleLCL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series05OverRuleLCL"].ValueDataMembers.AddRange(new string[] { "nOverRuleLCL" });

            //USL OverRules
            this.ChrXbarR.Series["Series06OverRuleUSL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series06OverRuleUSL"].ValueDataMembers.AddRange(new string[] { "nOverRuleUSL" });
            //LSL OverRules
            this.ChrXbarR.Series["Series07OverRuleLSL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series07OverRuleLSL"].ValueDataMembers.AddRange(new string[] { "nOverRuleLSL" });

            ////UOL OverRules
            //this.ChrXbarR.Series["Series08OverRuleUOL"].ArgumentDataMember = "Label";
            //this.ChrXbarR.Series["Series08OverRuleUOL"].ValueDataMembers.AddRange(new string[] { "nOverRuleUOL" });
            ////LOL OverRules
            //this.ChrXbarR.Series["Series09OverRuleLOL"].ArgumentDataMember = "Label";
            //this.ChrXbarR.Series["Series09OverRuleLOL"].ValueDataMembers.AddRange(new string[] { "nOverRuleLOL" });

            ////RUCL OverRules
            //this.ChrXbarR.Series["Series14OverRuleRUCL"].Visible = false;
            ////RLCL OverRules
            //this.ChrXbarR.Series["Series15OverRuleRLCL"].Visible = false;

            #endregion

            pnlMain.Dock = DockStyle.Fill;

            //해석용 Control Limit Rules Check 
            this.SpecRuleCheckPCU();

            #region Option - Text View (Chart)

            string textView = "";
            textView = Math.Round(_controlSpec.sigmaResult.cpkResult.nCP, SpcDefVal.disOpt.decPoint.Cpk).ToSafeString();
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

            textView = Math.Round(_controlSpec.sigmaResult.cpkResult.nCPK, SpcDefVal.disOpt.decPoint.Cpk).ToSafeString();
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

            textView = Math.Round(_controlSpec.sigmaResult.cpkResult.nPP, SpcDefVal.disOpt.decPoint.Cpk).ToSafeString();
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

            textView = Math.Round(_controlSpec.sigmaResult.cpkResult.nPPK, SpcDefVal.disOpt.decPoint.Cpk).ToSafeString();
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
            textView = Math.Round(_controlSpec.nXbar.ucl, SpcDefVal.disOpt.decPoint.Control).ToSafeString();
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
            textView = Math.Round(_controlSpec.nXbar.ccl, SpcDefVal.disOpt.decPoint.Control).ToSafeString();
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
            textView = Math.Round(_controlSpec.nXbar.lcl, SpcDefVal.disOpt.decPoint.Control).ToSafeString();
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
            textView = Math.Round(_controlSpec.nXbar.usl, SpcDefVal.disOpt.decPoint.Spec).ToSafeString();
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
            textView = Math.Round(nXbarValue, SpcDefVal.disOpt.decPoint.Spec).ToSafeString();
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
            textView = Math.Round(_controlSpec.nXbar.lsl, SpcDefVal.disOpt.decPoint.Spec).ToSafeString();
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
            //textView = Math.Round(_controlSpec.nXbar.uol, SpcDefVal.disOpt.decPoint.Uot).ToSafeString();
            //if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            //{
            //    this.lblUOLValue.Text = textView;
            //    this.lblUOLValue.ForeColor = Color.Black;
            //    this.chkUOL.ForeColor = Color.Black;
            //    this.chkUOL.Checked = true;
            //    this.chkUOL.Enabled = true;
            //}
            //else
            //{
            //    this.lblUOLValue.Text = SpcLimit.NONEDATA;
            //    this.lblUOLValue.ForeColor = Color.Gray;
            //    this.chkUOL.ForeColor = Color.Gray;
            //    this.chkUOL.Checked = false;
            //    this.chkUOL.Enabled = false;
            //}

            //LOL
            //textView = Math.Round(_controlSpec.nXbar.lol, SpcDefVal.disOpt.decPoint.Uot).ToSafeString();
            //if (textView != SpcLimit.MINTXT && textView != SpcLimit.MINTXT)
            //{
            //    this.lblLOLValue.Text = textView;
            //    this.lblLOLValue.ForeColor = Color.Black;
            //    this.chkLOL.ForeColor = Color.Black;
            //    this.chkLOL.Checked = true;
            //    this.chkLOL.Enabled = true;
            //}
            //else
            //{
            //    this.lblLOLValue.Text = SpcLimit.NONEDATA;
            //    this.lblLOLValue.ForeColor = Color.Gray;
            //    this.chkLOL.ForeColor = Color.Gray;
            //    this.chkLOL.Checked = false;
            //    this.chkLOL.Enabled = false;
            //}

            //RUCL
            //textView = Math.Round(_controlSpec.nR.ucl, SpcDefVal.disOpt.decPoint.RControl).ToSafeString();
            //if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            //{
            //    this.lblRUCLValue.Text = textView;
            //    this.lblRUCLValue.ForeColor = Color.Black;
            //    this.chkRUCL.ForeColor = Color.Blue;
            //    this.chkRUCL.Checked = true;
            //    this.chkRUCL.Enabled = true;
            //}
            //else
            //{
            //    this.lblRUCLValue.Text = SpcLimit.NONEDATA;
            //    this.lblRUCLValue.ForeColor = Color.Gray;
            //    this.chkRUCL.ForeColor = Color.Gray;
            //    this.chkRUCL.Checked = false;
            //    this.chkRUCL.Enabled = false;
            //}
            //RCCL
            //textView = Math.Round(_controlSpec.nR.ccl, SpcDefVal.disOpt.decPoint.RControl).ToSafeString();
            //if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            //{
            //    this.lblRCCLValue.Text = textView;
            //    this.lblRCCLValue.ForeColor = Color.Black;
            //    this.chkRCCL.ForeColor = Color.Green;
            //    this.chkRCCL.Checked = true;
            //    this.chkRCCL.Enabled = true;
            //    this.chkRCCL.Text = "R";
            //}
            //else
            //{
            //    this.lblRCCLValue.Text = SpcLimit.NONEDATA;
            //    this.lblRCCLValue.ForeColor = Color.Gray;
            //    this.chkRCCL.ForeColor = Color.Gray;
            //    this.chkRCCL.Checked = false;
            //    this.chkRCCL.Enabled = false;
            //    this.chkRCCL.Text = "R";
            //}
            //RLCL
            //textView = Math.Round(_controlSpec.nR.lcl, SpcDefVal.disOpt.decPoint.RControl).ToSafeString();
            //if (textView != SpcLimit.MINTXT && textView != SpcLimit.MINTXT)
            //{
            //    this.lblRLCLValue.Text = textView;
            //    this.lblRLCLValue.ForeColor = Color.Black;
            //    this.chkRLCL.ForeColor = Color.Blue;
            //    this.chkRLCL.Checked = true;
            //    this.chkRLCL.Enabled = true;
            //}
            //else
            //{
            //    this.lblRLCLValue.Text = SpcLimit.NONEDATA;
            //    this.lblRLCLValue.ForeColor = Color.Gray;
            //    this.chkRLCL.ForeColor = Color.Gray;
            //    this.chkRLCL.Checked = false;
            //    this.chkRLCL.Enabled = false;
            //}
            #endregion

            XYDiagram diagram = (XYDiagram)ChrXbarR.Diagram;
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
                //this.lblSigma1Value.Text = textView;
                //this.lblSigma1Value.ForeColor = Color.Black;
                //this.chkSigma1.ForeColor = SpcViewOption.att.sigma1.checkBox.ForeColor;
                //this.chkSigma1.Checked = false;
                //this.chkSigma1.Enabled = true;
                ////Strip01Sigma.
                //diagram.AxisY.Strips["Strip01Sigma"].Visible = false;
                //diagram.AxisY.Strips["Strip01Sigma"].MinLimit.AxisValue = 0;
                //diagram.AxisY.Strips["Strip01Sigma"].MaxLimit.AxisValue = _controlSpec.sigmaResult.nSigma1Max;
                //diagram.AxisY.Strips["Strip01Sigma"].MinLimit.AxisValue = _controlSpec.sigmaResult.nSigma1Min;
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
                //this.lblSigma2Value.Text = textView;
                //this.lblSigma2Value.ForeColor = Color.Black;
                //this.chkSigma2.ForeColor = SpcViewOption.att.sigma2.checkBox.ForeColor;
                //this.chkSigma2.Checked = false;
                //this.chkSigma2.Enabled = true;
                ////Strip02Sigma.
                //diagram.AxisY.Strips["Strip02Sigma"].Visible = false;
                //diagram.AxisY.Strips["Strip02Sigma"].MinLimit.AxisValue = 0;
                //diagram.AxisY.Strips["Strip02Sigma"].MaxLimit.AxisValue = _controlSpec.sigmaResult.nSigma2Max;
                //diagram.AxisY.Strips["Strip02Sigma"].MinLimit.AxisValue = _controlSpec.sigmaResult.nSigma2Min;
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
                //this.lblSigma3Value.Text = textView;
                //this.lblSigma3Value.ForeColor = Color.Black;
                //this.chkSigma3.ForeColor = SpcViewOption.att.sigma3.checkBox.ForeColor;
                //this.chkSigma3.Checked = false;
                //this.chkSigma3.Enabled = true;
                ////Strip03Sigma.
                //diagram.AxisY.Strips["Strip03Sigma"].Visible = false;
                //diagram.AxisY.Strips["Strip03Sigma"].MinLimit.AxisValue = 0;
                //diagram.AxisY.Strips["Strip03Sigma"].MaxLimit.AxisValue = _controlSpec.sigmaResult.nSigma3Max;
                //diagram.AxisY.Strips["Strip03Sigma"].MinLimit.AxisValue = _controlSpec.sigmaResult.nSigma3Min;
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
                switch (_controlSpec.spcOption.limitType)
                {
                    case LimitType.Interpretation:
                        _isInterpretation.UCL = true;
                        diagram.AxisY.ConstantLines["ConstantLineUCL"].Visible = false;
                        break;
                    case LimitType.Management:
                    case LimitType.Direct:
                    default:
                        _isInterpretation.LCL = false;
                        diagram.AxisY.ConstantLines["ConstantLineUCL"].AxisValue = _controlSpec.nXbar.ucl;
                        diagram.AxisY.ConstantLines["ConstantLineUCL"].Visible = true;
                        break;
                }
            }
            else
            {
                diagram.AxisY.ConstantLines["ConstantLineUCL"].AxisValue = "";
                diagram.AxisY.ConstantLines["ConstantLineUCL"].Visible = false;
            }

            //LCL
            if (_controlSpec.nXbar.lcl != SpcLimit.MAX && _controlSpec.nXbar.lcl != SpcLimit.MAX)
            {
                switch (_controlSpec.spcOption.limitType)
                {
                    case LimitType.Interpretation:
                        _isInterpretation.LCL = true;
                        diagram.AxisY.ConstantLines["ConstantLineLCL"].Visible = false;
                        break;
                    case LimitType.Management:
                    case LimitType.Direct:
                    default:
                        _isInterpretation.LCL = false;
                        diagram.AxisY.ConstantLines["ConstantLineLCL"].AxisValue = _controlSpec.nXbar.lcl;
                        diagram.AxisY.ConstantLines["ConstantLineLCL"].Visible = true;
                        break;
                }
            }
            else
            {
                diagram.AxisY.ConstantLines["ConstantLineLCL"].AxisValue = "";
                diagram.AxisY.ConstantLines["ConstantLineLCL"].Visible = false;
            }

            //USL
            if (_controlSpec.nXbar.usl != SpcLimit.MAX && _controlSpec.nXbar.usl != SpcLimit.MAX)
            {
                switch (_controlSpec.spcOption.limitType)
                {
                    case LimitType.Interpretation:
                        _isInterpretation.UCL = true;
                        diagram.AxisY.ConstantLines["ConstantLineUSL"].Visible = false;
                        break;
                    case LimitType.Management:
                    case LimitType.Direct:
                    default:
                        _isInterpretation.UCL = false;
                        diagram.AxisY.ConstantLines["ConstantLineUSL"].AxisValue = _controlSpec.nXbar.ucl;
                        diagram.AxisY.ConstantLines["ConstantLineUSL"].Visible = true;
                        break;
                }
            }
            else
            {
                _isInterpretation.UCL = false;
                diagram.AxisY.ConstantLines["ConstantLineUSL"].AxisValue = "";
                diagram.AxisY.ConstantLines["ConstantLineUSL"].Visible = false;
            }

            //LSL
            if (_controlSpec.nXbar.lsl != SpcLimit.MAX && _controlSpec.nXbar.lsl != SpcLimit.MAX)
            {
                switch (_controlSpec.spcOption.limitType)
                {
                    case LimitType.Interpretation:
                        _isInterpretation.LCL = true;
                        diagram.AxisY.ConstantLines["ConstantLineLSL"].Visible = false;
                        break;
                    case LimitType.Management:
                    case LimitType.Direct:
                    default:
                        _isInterpretation.LCL = false;
                        diagram.AxisY.ConstantLines["ConstantLineLSL"].AxisValue = _controlSpec.nXbar.ucl;
                        diagram.AxisY.ConstantLines["ConstantLineLSL"].Visible = true;
                        break;
                }
            }
            else
            {
                _isInterpretation.LCL = false;
                diagram.AxisY.ConstantLines["ConstantLineLSL"].AxisValue = "";
                diagram.AxisY.ConstantLines["ConstantLineLSL"].Visible = false;
            }

            ////UOL
            //if (_controlSpec.nXbar.uol != SpcLimit.MAX && _controlSpec.nXbar.uol != SpcLimit.MAX)
            //{
            //    diagram.AxisY.ConstantLines["ConstantLineUOL"].AxisValue = _controlSpec.nXbar.uol;
            //    diagram.AxisY.ConstantLines["ConstantLineUOL"].Visible = true;
            //}
            //else
            //{
            //    diagram.AxisY.ConstantLines["ConstantLineUOL"].AxisValue = "";
            //    diagram.AxisY.ConstantLines["ConstantLineUOL"].Visible = false;
            //}

            ////LOL
            //if (_controlSpec.nXbar.lol != SpcLimit.MAX && _controlSpec.nXbar.lol != SpcLimit.MAX)
            //{
            //    diagram.AxisY.ConstantLines["ConstantLineLOL"].AxisValue = _controlSpec.nXbar.lol;
            //    diagram.AxisY.ConstantLines["ConstantLineLOL"].Visible = true;
            //}
            //else
            //{
            //    diagram.AxisY.ConstantLines["ConstantLineLOL"].AxisValue = "";
            //    diagram.AxisY.ConstantLines["ConstantLineLOL"].Visible = false;
            //}

            //Pans 2 처리.
            //if (secondDiagPane.Visible == true)
            //{
            //    //RUCL
            //    if (_controlSpec.nR.ucl != SpcLimit.MAX && _controlSpec.nR.ucl != SpcLimit.MAX)
            //    {
            //        diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRUCL"].AxisValue = _controlSpec.nR.ucl;
            //        diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRUCL"].Visible = true;
            //    }
            //    else
            //    {
            //        diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRUCL"].AxisValue = "";
            //        diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRUCL"].Visible = false;
            //    }

            //    //RCCL
            //    if (_controlSpec.nR.ccl != SpcLimit.MAX && _controlSpec.nR.ccl != SpcLimit.MAX)
            //    {
            //        diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRCCL"].AxisValue = _controlSpec.nR.ccl;
            //        diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRCCL"].Visible = true;
            //    }
            //    else
            //    {
            //        diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRCCL"].AxisValue = "";
            //        diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRCCL"].Visible = false;
            //    }

            //    //RLCL
            //    if (_controlSpec.nR.lcl != SpcLimit.MAX && _controlSpec.nR.lcl != SpcLimit.MAX)
            //    {
            //        diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRLCL"].AxisValue = _controlSpec.nR.lcl;
            //        diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRLCL"].Visible = true;
            //    }
            //    else
            //    {
            //        diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRLCL"].AxisValue = "";
            //        diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRLCL"].Visible = false;
            //    }
            //}

            IsSpecCancel.Clear(ref tempIsCancel);
            tempWholeXPCU = SpcFunction.chartWholeRange(_controlSpec.nXbar, tempControlSpec.spcOption, tempIsCancel);

            diagram.AxisY.WholeRange.MaxValue = tempWholeXPCU.max;
            diagram.AxisY.WholeRange.MinValue = tempWholeXPCU.min;

            this.DetailChartControlClearPCU(LayoutVisibility.Never);

            this._IsPaint = true;

            _isControlEventsStop = false;
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
            this.ChrXbarR.DataSource = xbarData.SpcData;

            // Create a series, and add it to the chart. 
            //Series series1 = new Series("My Series", ViewType.Bar);
            this.ChrXbarR.Series["Series01AUCL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series01AUCL"].ValueDataMembers.AddRange(new string[] { "AUCL" });
            this.ChrXbarR.Series["Series02ALCL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series02ALCL"].ValueDataMembers.AddRange(new string[] { "ALCL" });

            this.ChrXbarR.Series["Series03Value"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series03Value"].ValueDataMembers.AddRange(new string[] { "nValue" });

            this.ChrXbarR.Series["Series11AUCL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series11AUCL"].ValueDataMembers.AddRange(new string[] { "RAUCL" });
            this.ChrXbarR.Series["Series12ALCL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series12ALCL"].ValueDataMembers.AddRange(new string[] { "RALCL" });

            this.ChrXbarR.Series["Series13Value"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series13Value"].ValueDataMembers.AddRange(new string[] { "nRValue" });

            //LSL OverRules
            this.ChrXbarR.Series["Series04OverRuleUCL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series04OverRuleUCL"].ValueDataMembers.AddRange(new string[] { "nOverRuleUCL" });

            this.ChrXbarR.Series["Series05OverRuleLCL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series05OverRuleLCL"].ValueDataMembers.AddRange(new string[] { "nOverRuleLCL" });

            //USL OverRules
            this.ChrXbarR.Series["Series06OverRuleUSL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series06OverRuleUSL"].ValueDataMembers.AddRange(new string[] { "nOverRuleUSL" });

            this.ChrXbarR.Series["Series07OverRuleLSL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series07OverRuleLSL"].ValueDataMembers.AddRange(new string[] { "nOverRuleLSL" });

            //USL OverRules
            this.ChrXbarR.Series["Series08OverRuleUOL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series08OverRuleUOL"].ValueDataMembers.AddRange(new string[] { "nOverRuleUOL" });

            this.ChrXbarR.Series["Series09OverRuleLOL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series09OverRuleLOL"].ValueDataMembers.AddRange(new string[] { "nOverRuleLOL" });

            //LSL OverRules
            this.ChrXbarR.Series["Series14OverRuleRUCL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series14OverRuleRUCL"].ValueDataMembers.AddRange(new string[] { "nROverRuleUCL" });

            this.ChrXbarR.Series["Series15OverRuleRLCL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series15OverRuleRLCL"].ValueDataMembers.AddRange(new string[] { "nROverRuleLCL" });

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

            XYDiagram diagram = (XYDiagram)ChrXbarR.Diagram;
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
        /// Chart Control 초기화
        /// </summary>
        public void ControlChartDataMappingClear()
        {
            this.pnlTop.Visible = false;
            this.pnlRightTop.Visible = false;
            this.layoutItemScrollOption.Visibility = LayoutVisibility.Never;

            this.lblSpace01.Text = "";
            this.lblSpace01.ToolTip = "";
            this.lblSpace02.Text = "";
            this.lblSpace02.ToolTip = "";
            this.lblSpace03.Text = "";
            this.lblSpace03.ToolTip = "";
            this._SUBGROUPNAME = "";

            _spcPara = null;
            SpcParameter xbarData = SpcParameter.Create();
            ControlSpec controlSpec = new ControlSpec();

            ControlChartSerialCaption serialCaption = null;
            //serialCaption = controlSpec.spcOption.chartName.SerialCaption;
            //serialCaption = SpcFunction.ControlChartToolsNameSetting(serialCaption);

            xbarData.SpcData = xbarData.ClearDataSpcXBar(out controlSpec);
            this.ChrXbarR.DataSource = xbarData.SpcData;

            this.ChrXbarR.Legends["LegendPane01"].Title.TextColor = Color.Gray;
            this.ChrXbarR.Legends["LegendPane02"].Title.TextColor = Color.Gray;

            this.ChrXbarR.RuntimeHitTesting = true;
            
            string forAnalysisMessage = SpcLibMessage.common.comCpk1031;//해석용
            string overRuleMessage = SpcLibMessage.common.comCpk1032;//Over Rule
            string avgValueMessage = SpcLibMessage.common.comCpk1033;//값(Value)

            // Create a series, and add it to the chart. 
            //Series series1 = new Series("My Series", ViewType.Bar);
            this.ChrXbarR.Series["Series01AUCL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series01AUCL"].ValueDataMembers.AddRange(new string[] { "AUCL" });
            this.ChrXbarR.Series["Series01AUCL"].CrosshairLabelPattern = forAnalysisMessage +" UCL : {V:F3}";
            //this.ChrXbarR.Series["Series01AUCL"].ValueDataMembersSerializable = "Min;Max";

            this.ChrXbarR.Series["Series02ALCL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series02ALCL"].ValueDataMembers.AddRange(new string[] { "ALCL" });
            this.ChrXbarR.Series["Series02ALCL"].CrosshairLabelPattern = forAnalysisMessage + " LCL : {V:F3}";

            //OverlappedRangeBarSeriesView overlappedRangeBarSeriesView1 = new OverlappedRangeBarSeriesView();
            // RangeBarSeriesLabel rangeValueSeries03Value = new RangeBarSeriesLabel();
            //rangeBarSeriesLabel2.Indent = 2;
            //rangeValueSeries03Value.TextPattern = "{V:F2}";

            this.ChrXbarR.Series["Series03Value"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series03Value"].ValueDataMembers.AddRange(new string[] { "nValue" });
            this.ChrXbarR.Series["Series03Value"].CrosshairLabelPattern = avgValueMessage + " : {V:F3}";
            //this.ChrXbarR.Series["Series03Value"].CrosshairLabelPattern = "Value : {V:F3}\n해석용 LSL : {V:F3}";
            //this.ChrXbarR.Series["Series03Value"].ValueDataMembersSerializable = "nValue;ALCL";
            //this.ChrXbarR.Series["Series03Value"].View = overlappedRangeBarSeriesView1;
            //this.ChrXbarR.Series["Series03Value"].ValueDataMembersSerializable = "nValue";
            
            this.ChrXbarR.Series["Series11AUCL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series11AUCL"].ValueDataMembers.AddRange(new string[] { "RAUCL" });
            this.ChrXbarR.Series["Series11AUCL"].CrosshairLabelPattern = forAnalysisMessage + " UCL : {V:F3}";

            this.ChrXbarR.Series["Series12ALCL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series12ALCL"].ValueDataMembers.AddRange(new string[] { "RALCL" });
            this.ChrXbarR.Series["Series12ALCL"].CrosshairLabelPattern = forAnalysisMessage + " LCL : {V:F3}";

            this.ChrXbarR.Series["Series13Value"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series13Value"].ValueDataMembers.AddRange(new string[] { "nRValue" });
            this.ChrXbarR.Series["Series13Value"].CrosshairLabelPattern = avgValueMessage + " : {V:F3}";

            //LSL OverRules
            this.ChrXbarR.Series["Series04OverRuleUCL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series04OverRuleUCL"].ValueDataMembers.AddRange(new string[] { "nOverRuleUCL" });
            this.ChrXbarR.Series["Series04OverRuleUCL"].CrosshairLabelPattern = overRuleMessage +" UCL : {V:F3}";

            this.ChrXbarR.Series["Series05OverRuleLCL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series05OverRuleLCL"].ValueDataMembers.AddRange(new string[] { "nOverRuleLCL" });
            this.ChrXbarR.Series["Series05OverRuleLCL"].CrosshairLabelPattern = overRuleMessage + " LCL : {V:F3}";

            //USL OverRules
            this.ChrXbarR.Series["Series06OverRuleUSL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series06OverRuleUSL"].ValueDataMembers.AddRange(new string[] { "nOverRuleUSL" });
            this.ChrXbarR.Series["Series06OverRuleUSL"].CrosshairLabelPattern = overRuleMessage + " USL : {V:F3}";

            this.ChrXbarR.Series["Series07OverRuleLSL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series07OverRuleLSL"].ValueDataMembers.AddRange(new string[] { "nOverRuleLSL" });
            this.ChrXbarR.Series["Series07OverRuleLSL"].CrosshairLabelPattern = overRuleMessage + " LSL : {V:F3}";

            //USL OverRules
            this.ChrXbarR.Series["Series08OverRuleUOL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series08OverRuleUOL"].ValueDataMembers.AddRange(new string[] { "nOverRuleUOL" });
            this.ChrXbarR.Series["Series08OverRuleUOL"].CrosshairLabelPattern = overRuleMessage + " UOL : {V:F3}";

            this.ChrXbarR.Series["Series09OverRuleLOL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series09OverRuleLOL"].ValueDataMembers.AddRange(new string[] { "nOverRuleLOL" });
            this.ChrXbarR.Series["Series09OverRuleLOL"].CrosshairLabelPattern = overRuleMessage + " LOL : {V:F3}";

            //LSL OverRules
            this.ChrXbarR.Series["Series14OverRuleRUCL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series14OverRuleRUCL"].ValueDataMembers.AddRange(new string[] { "nROverRuleUCL" });
            this.ChrXbarR.Series["Series14OverRuleRUCL"].CrosshairLabelPattern = overRuleMessage + " UCL : {V:F3}";

            this.ChrXbarR.Series["Series15OverRuleRLCL"].ArgumentDataMember = "Label";
            this.ChrXbarR.Series["Series15OverRuleRLCL"].ValueDataMembers.AddRange(new string[] { "nROverRuleLCL" });
            this.ChrXbarR.Series["Series15OverRuleRLCL"].CrosshairLabelPattern = overRuleMessage + " LCL : {V:F3}";

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

            XYDiagram diagram = (XYDiagram)ChrXbarR.Diagram;
            XYDiagramPane secondDiagPane = diagram.Panes["SecondPane"];
            
            this.ChrXbarR.Series["Series03Value"].Label.TextPattern = avgValueMessage + " {A}:{V:#0.000}";
            this.ChrXbarR.Series["Series06OverRuleUSL"].Label.TextPattern = overRuleMessage + " USL {A}:{V:#0.000}";
            this.ChrXbarR.Series["Series07OverRuleLSL"].Label.TextPattern = overRuleMessage + " LSL {A}:{V:#0.000}";

            //'dxChart.Diagram.Series(0).Label.TextPattern = "{A}:{V:$0.00}"  
            //ChrXbarR.Legend.Title.Visible = true;
            //ChrXbarR.Legend.Title.Text = "만세!!";
            //ChrXbarR.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            //ChrXbarR.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.Top;
            //ChrXbarR.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
            //ChrXbarR.Legend.TextVisible = false;
            //ChrXbarR.Legend.MarkerMode = LegendMarkerMode.None;
            //ChrXbarR.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;

            //diagram.AxisX.Title = "";
            //switch (_controlSpec.spcOption.chartType)
            //{
            //    case ControlChartType.XBar_R:
            //    case ControlChartType.XBar_S:
            //    case ControlChartType.I_MR:
            //    case ControlChartType.Merger:
            //        secondDiagPane.Visible = true;
            //        break;
            //    case ControlChartType.np:
            //    case ControlChartType.p:
            //    case ControlChartType.c:
            //    case ControlChartType.u:
            //        secondDiagPane.Visible = false;
            //        break;
            //    default:
            //        break;
            //}

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
        public void ChartLegendsTitleChange(string chartTitle, bool isChartMappingClear = true)
        {
            if (isChartMappingClear)
            {
                this.ControlChartDataMappingClear();
            }

            this.ChrXbarR.Legends["LegendPane01"].Title.TextColor = Color.Black;
            this.ChrXbarR.Legends["LegendPane02"].Title.TextColor = Color.Black;

            switch (chartTitle.ToUpper())
            {
                case "XBARR":
                case "R":
                    this.ChrXbarR.Legends["LegendPane01"].Title.Text = "XBAR";
                    this.ChrXbarR.Legends["LegendPane02"].Title.Text = "R";
                    break;
                case "XBARS":
                case "S":
                    this.ChrXbarR.Legends["LegendPane01"].Title.Text = "XBAR";
                    this.ChrXbarR.Legends["LegendPane02"].Title.Text = "S";
                    break;
                case "XBARP":
                case "MERGER":
                case "PM":
                    this.ChrXbarR.Legends["LegendPane01"].Title.Text = "XBAR";
                    this.ChrXbarR.Legends["LegendPane02"].Title.Text = "PM";
                    break;
                case "IMR":
                case "I":
                    this.ChrXbarR.Legends["LegendPane01"].Title.Text = "I";
                    this.ChrXbarR.Legends["LegendPane02"].Title.Text = "MR";
                    break;
                case "P":
                    this.ChrXbarR.Legends["LegendPane01"].Title.Text = "P";
                    this.ChrXbarR.Legends["LegendPane02"].Title.Text = "";
                    break;
                case "NP":
                    this.ChrXbarR.Legends["LegendPane01"].Title.Text = "NP";
                    this.ChrXbarR.Legends["LegendPane02"].Title.Text = "";
                    break;
                case "C":
                    this.ChrXbarR.Legends["LegendPane01"].Title.Text = "C";
                    this.ChrXbarR.Legends["LegendPane02"].Title.Text = "";
                    break;
                case "U":
                    this.ChrXbarR.Legends["LegendPane01"].Title.Text = "U";
                    this.ChrXbarR.Legends["LegendPane02"].Title.Text = "";
                    break;
                default:
                    this.ChrXbarR.Legends["LegendPane01"].Title.Text = "";
                    this.ChrXbarR.Legends["LegendPane02"].Title.Text = "";
                    break;
            }

            this.DetailChartControlClearPCU();

        }
        /// <summary>
        /// 관리도별 Chart Option Control 표시 및 여부 처리.
        /// </summary>
        /// <param name="value"></param>
        public void DetailChartControlOptionVisibleAndTitleClear(string chartTitle, LayoutVisibility value = LayoutVisibility.Never)
        {
            //LegendPane02
            string dd = this.ChrXbarR.Legends["LegendPane01"].Title.Text.ToString();
            this.DetailChartControlClearPCU(value);
        }
        /// <summary>
        /// 관리도별 Chart Option Control 표시여부 처리.
        /// </summary>
        /// <param name="value"></param>
        public void DetailChartControlClearPCU(LayoutVisibility value = LayoutVisibility.Never, string defaultCSLCaption = "")
        {
            _IsThisCboChartTypeTextChanged = true;
            XYDiagram diagram = (XYDiagram)ChrXbarR.Diagram;
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

                layoutItemTemp01.Visibility = value;
                layoutItemTemp01Value.Visibility = value;
                layoutItemTemp02.Visibility = value;
                layoutItemTemp02Value.Visibility = value;
                layoutItemTemp03.Visibility = value;
                layoutItemTemp03Value.Visibility = value;
                layoutItemTemp04.Visibility = value;
                layoutItemTemp04Value.Visibility = value;



                //Spec
                //layoutItemUSL.Visibility = value;
                //layoutItemLSL.Visibility = value;
                //layoutItemUSLValue.Visibility = value;
                //layoutItemLSLValue.Visibility = value;

                //Spec
                layoutItemUCL.Visibility = value;
                layoutItemLCL.Visibility = value;
                layoutItemUCLValue.Visibility = value;
                layoutItemLCLValue.Visibility = value;
                if (secondDiagPane.Visible)
                {
                    chkUSL.Text = "USL";
                    chkLSL.Text = "LSL";
                }
                else
                {
                    chkUSL.Text = "UCL";
                    chkLSL.Text = "LCL";
                }

                if (defaultCSLCaption != "")
                {
                    chkCSL.Text = defaultCSLCaption;
                }


                //Uotlier
                layoutItemUOL.Visibility = value;
                layoutItemUOLValue.Visibility = value;
                layoutItemLOL.Visibility = value;
                layoutItemLOLValue.Visibility = value;

                this.layoutItemCCL.Visibility = value;
                this.layoutItemCCLValue.Visibility = value;
                this.layoutItemSpace04.Visibility = value;
                this.layoutItemRUCL.Visibility = value;
                this.layoutItemRUCLValue.Visibility = value;
                this.layoutItemRCCL.Visibility = value;
                this.layoutItemRCCLValue.Visibility = value;
                this.layoutItemRLCL.Visibility = value;
                this.layoutItemRLCLValue.Visibility = value;

                if(this.chkSigma3.Checked) this.chkSigma3.Checked = false;
                if(this.chkSigma2.Checked) this.chkSigma2.Checked = false;
                if(this.chkSigma1.Checked) this.chkSigma1.Checked = false;

                //if(_controlSpec !=null && _controlSpec.spcOption != null)
                //{
                //    if (this.chkUCL.Checked) this.chkUCL.Checked = false;
                //    if (this.chkCCL.Checked) this.chkCCL.Checked = false;
                //    if (this.chkLCL.Checked) this.chkLCL.Checked = false;
                //    if (this.chkRUCL.Checked) this.chkRUCL.Checked = false;
                //    if (this.chkRCCL.Checked) this.chkRCCL.Checked = false;
                //    if (this.chkRLCL.Checked) this.chkRLCL.Checked = false;
                //    if (this.chkUOL.Checked) this.chkUOL.Checked = false;
                //    if (this.chkLOL.Checked) this.chkLOL.Checked = false;
                //}
            }


        }

        /// <summary>
        /// 해석용 Rule Check
        /// </summary>
        public void SpecRuleCheckPCU()
        {
            Application.DoEvents();
            double[] val = new double[1];
            double nVal;
            double naUcl;
            double naLcl;
            int maxNValueCount = 0;
            bool isRuleCheck = false;
            try
            {
                maxNValueCount = this.ChrXbarR.Series["Series03Value"].Points.Count;
                for (int i = 0; i < maxNValueCount; i++)
                {
                    nVal = this.ChrXbarR.Series["Series03Value"].Points[i].Values[0];
                    naUcl = this.ChrXbarR.Series["Series01AUCL"].Points[i].Values[0];
                    naLcl = this.ChrXbarR.Series["Series02ALCL"].Points[i].Values[0];

                    isRuleCheck = false;
                    if (nVal > naUcl)
                    {
                        isRuleCheck = true;
                    }
                    if (nVal < naLcl)
                    {
                        isRuleCheck = true;
                    }

                    if (isRuleCheck)
                    {
                        //this.ChrXbarR.Series["Series03Value"].Points[i].Color = Color.Blue;
                        this.ChrXbarR.Series["Series03Value"].Points[i].Color = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //this.ChrXbarR.Refresh();
        }

        /// <summary>
        /// 콤보박스 Chart Type 설정.
        /// </summary>
        /// <param name="option">
        /// 1: R, S, PL, MR
        /// 2: P, np, c, u
        /// 3: IMR
        /// 4: P, IMR
        /// </param>
        public void cboChartTypeSetting(int option = 0)
        {
            DataTable dtLeftChartType = SpcClass.CreateTableSpcComboBox(this.cboChartType.Name.ToString());
            switch (option)
            {
                case 1:
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.XBar_R);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.XBar_S);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.Merger);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.I_MR);
                    break;
                case 2:
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.p);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.np);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.c);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.u);
                    break;
                case 3: //I-MR
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.I_MR);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.XBar_R);
                    break;
                case 4: //P
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.p);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.I_MR);
                    break;
                default:
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.XBar_R);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.XBar_S);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.Merger);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.I_MR);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.p);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.np);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.c);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.u);
                    break;
            }


            this.cboChartType.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            this.cboChartType.ShowHeader = false;
            this.cboChartType.DisplayMember = "Display";
            this.cboChartType.ValueMember = "Value";
            this.cboChartType.DataSource = dtLeftChartType;

            this.cboChartType.ItemIndex = 0;
        }


        #endregion

        #region Sigmar
        /// <summary>
        /// 공정능력 분석 자료 반환.
        /// </summary>
        /// <param name="subgroup"></param>
        /// <param name="centerData"></param>
        /// <returns></returns>
        public SpcSigmaResult GetSigmaData_Temp20191217(ControlSpec controlSpec, string subgroup, double centerData)
        {
            SpcSigmaResult result = SpcSigmaResult.Create();
            if (rtnCpkData == null)
            {
                return result;
            }

            //sia확인 : 3 sigma 계산 부분.
            result = GetCpkResultData_Temp20191217(subgroup); //공정능력 결과 1행(한행/1Row) 반환.

            Console.WriteLine(centerData);
            if (centerData == 0 || centerData == SpcLimit.MIN || centerData == SpcLimit.MAX)
            {
                Console.WriteLine(centerData);
            }

            double sigmaWith = SpcLimit.MIN;
            double sigmaTotal = SpcLimit.MIN;
            switch (controlSpec.spcOption.sigmaType)
            {
                case SigmaType.Yes:
                    switch (controlSpec.spcOption.chartType)
                    {
                        case ControlChartType.XBar_R:
                            sigmaWith = result.cpkResult.nSVALUE_RTDC4;//R - RTDC4
                            sigmaTotal = result.cpkResult.nPVALUE_STDC4;
                            break;
                        case ControlChartType.XBar_S:
                            sigmaWith = result.cpkResult.nSVALUE_STDC4;//S - STDC4
                            sigmaTotal = result.cpkResult.nPVALUE_STDC4;
                            break;
                        case ControlChartType.I_MR:
                            break;
                        case ControlChartType.Merger:
                            sigmaWith = result.cpkResult.nSVALUE_PTDC4;//합동 - PTDC4
                            sigmaTotal = result.cpkResult.nPVALUE_STDC4;
                            break;
                        case ControlChartType.np:
                            break;
                        case ControlChartType.p:
                            break;
                        case ControlChartType.c:
                            break;
                        case ControlChartType.u:
                            break;
                        default:
                            break;
                    }
                    break;
                case SigmaType.No:
                    switch (controlSpec.spcOption.chartType)
                    {
                        case ControlChartType.XBar_R:
                            sigmaWith = result.cpkResult.nSVALUE_RTD;
                            sigmaTotal = result.cpkResult.nPVALUE_STD;
                            break;
                        case ControlChartType.XBar_S:
                            sigmaWith = result.cpkResult.nSVALUE_STD;
                            sigmaTotal = result.cpkResult.nPVALUE_STD;
                            break;
                        case ControlChartType.I_MR:
                            break;
                        case ControlChartType.Merger:
                            sigmaWith = result.cpkResult.nSVALUE_PTD;
                            sigmaTotal = result.cpkResult.nPVALUE_STD;
                            break;
                        case ControlChartType.np:
                            break;
                        case ControlChartType.p:
                            break;
                        case ControlChartType.c:
                            break;
                        case ControlChartType.u:
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

            //Sigma
            result.nSigma = sigmaWith;
            result.nSigmaWithin = sigmaWith;
            result.nSigmaTotal = sigmaTotal;
            result.subGroup = result.cpkResult.SUBGROUP;

            if (result.nSigma != SpcLimit.MAX && result.nSigma != SpcLimit.MIN && result.nSigma.ToString() != double.NaN.ToString())
            {
                //result.nSigma = result.cpkResult.nSVALUE_PTDC4;
                //result.subGroup = result.cpkResult.SUBGROUP;

                result.nSigma1 = Math.Round((result.nSigma / 3), 6);
                result.nSigma2 = Math.Round((result.nSigma1 * 2), 6);
                result.nSigma3 = Math.Round((result.nSigma), 6);

                result.nSigma1Max = centerData + result.nSigma1;
                result.nSigma1Max = Math.Round((result.nSigma1Max), 6);
                result.nSigma2Max = centerData + result.nSigma2;
                result.nSigma2Max = Math.Round((result.nSigma2Max), 6);
                result.nSigma3Max = centerData + result.nSigma3;
                result.nSigma3Max = Math.Round((result.nSigma3Max), 6);

                result.nSigma1Min = centerData - result.nSigma1;
                result.nSigma1Min = Math.Round((result.nSigma1Min), 6);
                result.nSigma2Min = centerData - result.nSigma2;
                result.nSigma2Min = Math.Round((result.nSigma2Min), 6);
                result.nSigma3Min = centerData - result.nSigma3;
                result.nSigma3Min = Math.Round((result.nSigma3Min), 6);
            }

            return result;

        }

        /// <summary>
        /// 공정능력 결과 1행 반환.
        /// </summary>
        /// <param name="subgroup"></param>
        /// <returns></returns>
        public SpcSigmaResult GetCpkResultData_Temp20191217(string subgroup)
        {
            SpcSigmaResult result = SpcSigmaResult.Create();
            //Sigma 추출
            var sigmaData = from item in rtnCpkData
                            where (item.SUBGROUP == subgroup)
                            select item;
            foreach (DataRow item in sigmaData)
            {

                //sia확인 : 공정능력 결과 DB 전이.
                result.cpkResult.nSEQNO = SpcFunction.IsDbNckInt64(item, "SEQNO");
                result.cpkResult.nGROUPID = SpcFunction.IsDbNckInt64(item, "GROUPID");
                result.cpkResult.SUBGROUP = SpcFunction.IsDbNck(item, "SUBGROUP");
                result.cpkResult.nEXTRAID = SpcFunction.IsDbNckInt64(item, "EXTRAID");
                result.cpkResult.EXTRACONDITIONS = SpcFunction.IsDbNck(item, "EXTRACONDITIONS");
                result.cpkResult.nLSL = SpcFunction.IsDbNckDoubleMin(item, "LSL");
                result.cpkResult.nCSL = SpcFunction.IsDbNckDoubleMax(item, "CSL");
                result.cpkResult.nUSL = SpcFunction.IsDbNckDoubleMax(item, "USL");
                result.cpkResult.SPECMODE = SpcFunction.IsDbNck(item, "SPECMODE");
                result.cpkResult.nSAMPLINGCOUNT = SpcFunction.IsDbNckInt64(item, "SAMPLINGCOUNT");
                result.cpkResult.nSUBGROUPCOUNT = SpcFunction.IsDbNckInt64(item, "SUBGROUPCOUNT");
                result.cpkResult.nPCOUNT = SpcFunction.IsDbNckInt64(item, "PCOUNT");
                result.cpkResult.nKCOUNT = SpcFunction.IsDbNckInt64(item, "KCOUNT");
                result.cpkResult.nNVALUE_TOL = SpcFunction.IsDbNckDoubleMax(item, "NVALUE_TOL");
                result.cpkResult.nNVALUE_AVG = SpcFunction.IsDbNckDoubleMax(item, "NVALUE_AVG");
                result.cpkResult.nSVALUE_AVG = SpcFunction.IsDbNckDoubleMax(item, "SVALUE_AVG");
                result.cpkResult.nSVALUE_RTD = SpcFunction.IsDbNckDoubleMax(item, "SVALUE_RTD");
                result.cpkResult.nSVALUE_RTDC4 = SpcFunction.IsDbNckDoubleMax(item, "SVALUE_RTDC4");
                result.cpkResult.nSVALUE_STD = SpcFunction.IsDbNckDoubleMax(item, "SVALUE_STD");
                result.cpkResult.nSVALUE_STDC4 = SpcFunction.IsDbNckDoubleMax(item, "SVALUE_STDC4");
                result.cpkResult.nSVALUE_PTD = SpcFunction.IsDbNckDoubleMax(item, "SVALUE_PTD");
                result.cpkResult.nSVALUE_PTDC4 = SpcFunction.IsDbNckDoubleMax(item, "SVALUE_PTDC4");
                result.cpkResult.nCP = SpcFunction.IsDbNckDoubleMax(item, "CP");
                result.cpkResult.nCPL = SpcFunction.IsDbNckDoubleMax(item, "CPL");
                result.cpkResult.nCPU = SpcFunction.IsDbNckDoubleMax(item, "CPU");
                result.cpkResult.nCPK = SpcFunction.IsDbNckDoubleMax(item, "CPK");
                result.cpkResult.nCPM = SpcFunction.IsDbNckDoubleMax(item, "CPM");
                result.cpkResult.nJUDGMENTCPK = SpcFunction.IsDbNck(item, "JUDGMENTCPK");
                result.cpkResult.nPCISUBGROUP = SpcFunction.IsDbNck(item, "PCISUBGROUP");
                result.cpkResult.nPCI_d2 = SpcFunction.IsDbNckDoubleMax(item, "PCI_d2");
                result.cpkResult.nPCI_c4S = SpcFunction.IsDbNckDoubleMax(item, "PCI_c4S");
                result.cpkResult.nPCI_c4C = SpcFunction.IsDbNckDoubleMax(item, "PCI_c4C");
                result.cpkResult.nPVALUE_AVG = SpcFunction.IsDbNckDoubleMax(item, "PVALUE_AVG");
                result.cpkResult.nPVALUE_STD = SpcFunction.IsDbNckDoubleMax(item, "PVALUE_STD");
                result.cpkResult.nPVALUE_STDC4 = SpcFunction.IsDbNckDoubleMax(item, "PVALUE_STDC4");
                result.cpkResult.nPP = SpcFunction.IsDbNckDoubleMax(item, "PP");
                result.cpkResult.nPPL = SpcFunction.IsDbNckDoubleMax(item, "PPL");
                result.cpkResult.nPPU = SpcFunction.IsDbNckDoubleMax(item, "PPU");
                result.cpkResult.nPPK = SpcFunction.IsDbNckDoubleMax(item, "PPK");
                result.cpkResult.JUDGMENTPPK = SpcFunction.IsDbNck(item, "JUDGMENTPPK");
                result.cpkResult.nPPI_c4 = SpcFunction.IsDbNckDoubleMax(item, "PPI_c4");

                result.cpkResult.nTargetUSL = SpcFunction.IsDbNckDoubleMax(item, "TAUSL");
                result.cpkResult.nTargetCSL = SpcFunction.IsDbNckDoubleMax(item, "TACSL");
                result.cpkResult.nTargetLSL = SpcFunction.IsDbNckDoubleMax(item, "TALSL");

                result.cpkResult.ppmWithinLSL = SpcFunction.IsDbNckDoubleMin(item, "PPMWITHINLSL");
                result.cpkResult.ppmWithinUSL = SpcFunction.IsDbNckDoubleMin(item, "PPMWITHINUSL");
                result.cpkResult.ppmWithinTOT = SpcFunction.IsDbNckDoubleMin(item, "PPMWITHINTOT");
                result.cpkResult.ppmOverallLSL = SpcFunction.IsDbNckDoubleMin(item, "PPMOVERALLLSL");
                result.cpkResult.ppmOverallUSL = SpcFunction.IsDbNckDoubleMin(item, "PPMOVERALLUSL");
                result.cpkResult.ppmOverallTOT = SpcFunction.IsDbNckDoubleMin(item, "PPMOVERALLTOT");
                result.cpkResult.ppmObserveLSLN = SpcFunction.IsDbNckDoubleMin(item, "PPMOBSERVELSLN");
                result.cpkResult.ppmObserveUSLN = SpcFunction.IsDbNckDoubleMin(item, "PPMOBSERVEUSLN");
                result.cpkResult.ppmObserveLSL = SpcFunction.IsDbNckDoubleMin(item, "PPMOBSERVELSL");
                result.cpkResult.ppmObserveUSL = SpcFunction.IsDbNckDoubleMin(item, "PPMOBSERVEUSL");
                result.cpkResult.ppmObserveTOT = SpcFunction.IsDbNckDoubleMin(item, "PPMOBSERVETOT");

                result.cpkResult.nSTATUS = SpcFunction.IsDbNckInt64(item, "STATUS");
                result.cpkResult.STATUSMESSAGE = SpcFunction.IsDbNck(item, "STATUSMESSAGE");
                result.cpkResult.nERRORNO = SpcFunction.IsDbNckInt64(item, "ERRORNO");
                result.cpkResult.ERRORMESSAGE = SpcFunction.IsDbNck(item, "ERRORMESSAGE");

                //Cpk 결과값 한행(1Raw)을 Data Table 형식으로 전이.
                SpcSigmaResult.CpkResultDataTableWrite(ref result);
            }
            return result;
        }
        #endregion Sigmar

        #region Nelson Rules Check
        public RulesCheck RulesChecking(RulesCheck rulesCheckData)
        {
            // _dtControlData
            Console.WriteLine(_dtControlData.Rows.Count);

            //입력자료 Check
            if (rulesCheckData != null
                && rulesCheckData.lstRulesPara != null
                && rulesCheckData.lstRulesPara.Count > 0
                && rulesCheckData.lstRuleCheckResult != null)
            {
            }
            else
            {
                return rulesCheckData;
            }

            rulesCheckData.lstRuleCheckResult.Clear();
            rulesCheckData.nPointMaxCount = rulesCheckData.lstRulesPara.Count;
            RuleCheckResult ruleResult01 = RuleCheckResult.Create();
            RuleCheckResult ruleResult02 = RuleCheckResult.Create();
            RuleCheckResult ruleResult03 = RuleCheckResult.Create();
            RuleCheckResult ruleResult04 = RuleCheckResult.Create();
            RuleCheckResult ruleResult05 = RuleCheckResult.Create();
            RuleCheckResult ruleResult06 = RuleCheckResult.Create();
            RuleCheckResult ruleResult07 = RuleCheckResult.Create();
            RuleCheckResult ruleResult08 = RuleCheckResult.Create();

            //Rules Check
            bool isSubDataGroupCheck = false;
            RtnControlDataTable staDataSingle = new RtnControlDataTable();
            foreach (RulesPara item in rulesCheckData.lstRulesPara)
            {
                Console.WriteLine(item.ruleNo);
                
                if (pnlTop.Visible)
                {
                    //Console.WriteLine(rtnDirectChartData.Rows.Count);
                    isSubDataGroupCheck = true;
                    string singleSubGroupID = this.cboOrderMode.GetDataValue().ToString();
                    //var subgroupRawData = _dtControlData.AsEnumerable().Where(x => x.SUBGROUP == singleSubGroupID);
                    var subgroupRawData = rtnDirectChartData.AsEnumerable().Where(x => x.SUBGROUP == singleSubGroupID);
                    foreach (DataRow rawitem in subgroupRawData)
                    {
                        staDataSingle.ImportRow(rawitem);
                    }
                }

                //sia확인 :RulesCheck - ucXBar> RulesChecking
                switch (item.ruleNo)
                {
                    case 1:
                        if (!isSubDataGroupCheck)
                        {
                            ruleResult01 = Rule1_Check(_dtControlData);
                        }
                        else
                        {
                            ruleResult01 = Rule1_Check(staDataSingle);
                        }
                        ruleResult01.RuleColor = rulesCheckData.Rule01Color;
                        ruleResult01.discription = item.messageDiscription;
                        ruleResult01.comment = item.messageComment;
                        rulesCheckData.lstRuleCheckResult.Add(ruleResult01);
                        break;
                    case 2:
                        if (!isSubDataGroupCheck)
                        {
                            ruleResult02 = Rule2_Check(_dtControlData);
                        }
                        else
                        {
                            ruleResult02 = Rule2_Check(staDataSingle);
                        }
                        ruleResult02.RuleColor = rulesCheckData.Rule02Color;
                        ruleResult02.discription = item.messageDiscription;
                        ruleResult02.comment = item.messageComment;
                        rulesCheckData.lstRuleCheckResult.Add(ruleResult02);
                        break;
                    case 3:
                        if (!isSubDataGroupCheck)
                        {
                            ruleResult03 = Rule3_Check(_dtControlData);
                        }
                        else
                        {
                            ruleResult03 = Rule3_Check(staDataSingle);
                        }
                        ruleResult03 = Rule3_Check(_dtControlData);
                        ruleResult03.RuleColor = rulesCheckData.Rule03Color;
                        ruleResult03.discription = item.messageDiscription;
                        ruleResult03.comment = item.messageComment;
                        rulesCheckData.lstRuleCheckResult.Add(ruleResult03);
                        break;
                    case 4:
                        if (!isSubDataGroupCheck)
                        {
                            ruleResult04 = Rule4_Check(_dtControlData);
                        }
                        else
                        {
                            ruleResult04 = Rule4_Check(staDataSingle);
                        }
                        ruleResult04.RuleColor = rulesCheckData.Rule04Color;
                        ruleResult04.discription = item.messageDiscription;
                        ruleResult04.comment = item.messageComment;
                        rulesCheckData.lstRuleCheckResult.Add(ruleResult04);
                        break;
                    case 5:
                        ruleResult05 = Rule5_Check(_dtControlData);
                        ruleResult05.RuleColor = rulesCheckData.Rule05Color;
                        ruleResult05.discription = item.messageDiscription;
                        ruleResult05.comment = item.messageComment;
                        rulesCheckData.lstRuleCheckResult.Add(ruleResult05);
                        break;
                    case 6:
                        ruleResult06 = Rule6_Check(_dtControlData);
                        ruleResult06.RuleColor = rulesCheckData.Rule06Color;
                        ruleResult06.discription = item.messageDiscription;
                        ruleResult06.comment = item.messageComment;
                        rulesCheckData.lstRuleCheckResult.Add(ruleResult06);
                        break;
                    case 7:
                        ruleResult07 = Rule7_Check(_dtControlData);
                        ruleResult07.RuleColor = rulesCheckData.Rule07Color;
                        ruleResult07.discription = item.messageDiscription;
                        ruleResult07.comment = item.messageComment;
                        rulesCheckData.lstRuleCheckResult.Add(ruleResult07);
                        break;
                    case 8:
                        ruleResult08 = Rule8_Check(_dtControlData);
                        ruleResult08.RuleColor = rulesCheckData.Rule08Color;
                        ruleResult08.discription = item.messageDiscription;
                        ruleResult08.comment = item.messageComment;
                        rulesCheckData.lstRuleCheckResult.Add(ruleResult08);
                        break;
                    default:
                        break;
                }

            }

            //Chart 적용.
            foreach (RuleCheckResult item in rulesCheckData.lstRuleCheckResult)
            {
                if (item.status == 0)
                {
                    if (item.nRuleCheckCount > 0)
                    {
                        SpecRuleCheckNelsonColor(item);
                        //MessageBox.Show(string.Format("Rules Check {0}번", ruleNo.ToString()));
                    }
                }
            }


            if (rulesCheckData != null
                && rulesCheckData.lstRuleCheckResult != null
                && rulesCheckData.lstRuleCheckResult.Count > 0)
            {
                SpcControlRulesResultPopup frmResult = new SpcControlRulesResultPopup();
                frmResult.rulesCheckData = rulesCheckData;
                frmResult.ControlSpecData = _controlSpec;
                frmResult.spcOption = _spcPara.spcOption;
                frmResult.RulesModeCheck();
                frmResult.ShowDialog();
                //frmResult.Show();
            }



            return rulesCheckData;
        }

        /// <summary>
        /// 한 점은 평균에서 3 개 이상의 표준 편차입니다.
        /// One point is more than 3 standard deviations from the mean.
        /// </summary>
        /// <param name="staData">분석 자료 입력</param>
        /// <returns></returns>
        public RuleCheckResult Rule1_Check(RtnControlDataTable staData)
        {
            RuleCheckResult ruleResult = RuleCheckResult.Create();
            ruleResult.nRuleCheckCount = 0;
            ruleResult.RuleNo = 1;
            ruleResult.message = ChartMessage.rules.chk1000; //정상검증.

            try
            {
                //입력 자료가 없습니다.		
                if (staData == null)
                {
                    ruleResult.status = 1011;
                    ruleResult.message = ChartMessage.rules.chk1001; //입력 자료가 없습니다, Data is null.;
                    goto NEXT;
                }

                int i = 0;
                int nRowMax = staData.Rows.Count;
                int nSeqID = 0;
                double nValue = SpcLimit.MIN;
                double nBeforeValueMax = SpcLimit.MIN;
                double nBeforeValueMin = SpcLimit.MAX;
                int nCheckCount = 0;

                //Sigmar 3 Max Check
                if (_controlSpec != null 
                    && _controlSpec.sigmaResult != null 
                    && _controlSpec.sigmaResult.nSigma3Max != SpcLimit.MAX
                    && _controlSpec.sigmaResult.nSigma3Max != SpcLimit.MIN)
                {
                    //Sigmar 3 사용
                    nBeforeValueMax = _controlSpec.sigmaResult.nSigma3Max;
                    
                }
                else
                {
                    ruleResult.status = 1012;
                    ruleResult.message = ChartMessage.rules.chk1002; //UCL 3 Sigmar가 없습니다.;
                    goto NEXT;
                }

                //Sigmar 3 Min Check
                if (_controlSpec != null
                    && _controlSpec.sigmaResult != null
                    && _controlSpec.sigmaResult.nSigma3Min != SpcLimit.MAX
                    && _controlSpec.sigmaResult.nSigma3Min != SpcLimit.MIN)
                {
                    //Sigmar 3 사용
                    nBeforeValueMin = _controlSpec.sigmaResult.nSigma3Min;
                }
                else
                {
                    ruleResult.status = 1013;
                    ruleResult.message = ruleResult.message = ChartMessage.rules.chk1002; //LCL 3 Sigmar가 없습니다.
                    goto NEXT;
                }


                for (i = 0; i < nRowMax; i++)
                {
                    DataRow rowdata = staData.Rows[i];
                    nSeqID = rowdata["TEMPID"].ToSafeInt32();

                    nValue = rowdata["BAR"].ToSafeDoubleStaMin();

                    //Console.WriteLine(string.Format("Max: {0}, Value: {1}, Min:{2}", nBeforeValueMax, nValue, nBeforeValueMin));
                    if (nValue <= nBeforeValueMax && nValue >= nBeforeValueMin)
                    {
                        nCheckCount = 0;
                    }
                    else
                    {
                        if (nCheckCount == 0)
                        {
                            ruleResult.nStartPoint = nSeqID;
                        }
                        ruleResult.nEndPoint = nSeqID;
                        nCheckCount += 1;
                    }


                    if (nCheckCount == 1)
                    {
                        RuleCheckSerialsPoint rPoint = RuleCheckSerialsPoint.Create();
                        rPoint.nStartPoint = ruleResult.nStartPoint;
                        rPoint.nEndPoint = ruleResult.nEndPoint + 1;
                        #region 값 저장.
                        ruleResult.isRuleOver = true;
                        for (int n = rPoint.nStartPoint; n < rPoint.nEndPoint; n++)
                        {
                            DataRow rowValue = staData.Rows[n];
                            RuleCheckSerialsPointData pointData = RuleCheckSerialsPointData.Create();
                            pointData.nindex = n;
                            pointData.nCheckValue = rowValue["BAR"].ToSafeDoubleStaMin();
                            rPoint.listPointData.Add(pointData);
                        }
                        #endregion 값 저장.
                        ruleResult.listPoint.Add(rPoint);
                        ruleResult.nRuleCheckCount += 1;
                        nCheckCount = 0;
                        ruleResult.message = ChartMessage.rules.chk1011; //Rule Over 발생.
                    }

                }

                if (!ruleResult.isRuleOver)
                {
                    ruleResult.nStartPoint = 0;
                    ruleResult.nEndPoint = 0;
                }

            }
            catch (Exception ex)
            {
                ruleResult.status = 1;
                ruleResult.message = ex.Message.ToString();
                Console.WriteLine(ex.Message);
            }
        NEXT:
            return ruleResult;

        }
        /// <summary>
        /// 행에서 9 개 이상의 점이 평균의 같은쪽에 있습니다.
        /// Nine (or more) points in a row are on the same side of the mean.
        /// </summary>
        /// <param name="staData"></param>
        /// <returns></returns>
        public RuleCheckResult Rule2_Check(RtnControlDataTable staData)
        {
            RuleCheckResult ruleResult = RuleCheckResult.Create();
            ruleResult.nRuleCheckCount = 0;
            ruleResult.RuleNo = 2;
            ruleResult.message = ChartMessage.rules.chk1000; //정상검증.

            try
            {
                //입력자료가 없습니다.
                if (staData == null)
                {
                    ruleResult.status = 1011;
                    ruleResult.message = ChartMessage.rules.chk1001; //입력 자료가 없습니다, Data is null.;
                    goto NEXT;
                }

                int i = 0;
                int nRowMax = staData.Rows.Count;
                int nSeqID = 0;
                double nXBar = SpcLimit.MIN;
                double nValue = SpcLimit.MIN;
                int nCheckCount = 0;
                int nisMax = 0;//0-기본, 1-상한, 2-하한
                int nCheckRowIndex = -1;

                for (i = 0; i < nRowMax; i++)
                {
                    DataRow rowdata = staData.Rows[i];
                    nSeqID = rowdata["TEMPID"].ToSafeInt32();

                    if (nXBar == SpcLimit.MIN)
                    {
                        nXBar = rowdata["XBAR"].ToSafeDoubleStaMin();
                        if (nXBar == SpcLimit.MIN)
                        {
                            ruleResult.status = 1012;
                            ruleResult.message = SpcLibMessage.common.rule1009; //평균 값이 없습니다.
                            goto NEXT;
                        }
                    }

                    nValue = rowdata["BAR"].ToSafeDoubleStaMin();
                    if (nValue > nXBar)
                    {
                        if (nisMax == 2)
                        {
                            nCheckCount = 0;
                        }

                        if (nCheckCount == 0)
                        {
                            nisMax = 1;//상한
                            ruleResult.nStartPoint = nSeqID;

                        }

                        ruleResult.nEndPoint = nSeqID;
                        nCheckCount += 1;
                    }
                    else if (nValue < nXBar)
                    {
                        if (nisMax == 1)
                        {
                            nCheckCount = 0;
                        }

                        if (nCheckCount == 0)
                        {
                            nisMax = 2;//하한
                            ruleResult.nStartPoint = nSeqID;
                        }

                        ruleResult.nEndPoint = nSeqID;
                        nCheckCount += 1;
                    }
                    else
                    {
                        nisMax = 0;//기본값
                        nCheckCount = 0;
                    }

                    if (nCheckCount >= 9)
                    {
                        if (nCheckCount == 9)
                        {
                            RuleCheckSerialsPoint rPoint = RuleCheckSerialsPoint.Create();
                            rPoint.nStartPoint = ruleResult.nStartPoint;
                            rPoint.nEndPoint = ruleResult.nEndPoint + 1;
                            #region 값 저장.
                            ruleResult.isRuleOver = true;
                            for (int n = rPoint.nStartPoint; n < rPoint.nEndPoint; n++)
                            {
                                DataRow rowValue = staData.Rows[n];
                                RuleCheckSerialsPointData pointData = RuleCheckSerialsPointData.Create();
                                pointData.nindex = n;
                                pointData.nCheckValue = rowValue["BAR"].ToSafeDoubleStaMin();
                                rPoint.listPointData.Add(pointData);
                            }
                            #endregion 값 저장.
                            ruleResult.listPoint.Add(rPoint);
                            ruleResult.nRuleCheckCount += 1;
                            ruleResult.message = ChartMessage.rules.chk1011; //Rule Over 발생.
                        }
                        else
                        {
                            if (ruleResult.listPoint != null && ruleResult.listPoint.Count > 0)
                            {
                                nCheckRowIndex = ruleResult.nRuleCheckCount - 1;
                                ruleResult.listPoint[nCheckRowIndex].nEndPoint = ruleResult.nEndPoint + 1;
                            }
                        }
                    }
                }

                if (ruleResult.isRuleOver)
                {
                    ruleResult.nEndPoint += 1;
                }
                else
                {
                    ruleResult.nStartPoint = 0;
                    ruleResult.nEndPoint = 0;
                }

                ruleResult.status = 0;

            }
            catch (Exception ex)
            {
                ruleResult.status = 1;
                ruleResult.message = ex.Message.ToString();
                Console.WriteLine(ex.Message);
            }

        NEXT:
            return ruleResult;

        }
        /// <summary>
        /// 연속으로 6 개 이상의 포인트가 지속적으로 증가 (또는 감소)합니다.
        /// Six (or more) points in a row are continually increasing (or decreasing).
        /// </summary>
        /// <param name="staData"></param>
        /// <returns></returns>
        public RuleCheckResult Rule3_Check(RtnControlDataTable staData)
        {
            RuleCheckResult ruleResult = RuleCheckResult.Create();
            ruleResult.nRuleCheckCount = 0;
            ruleResult.RuleNo = 3;
            ruleResult.message = ChartMessage.rules.chk1000; //정상검증.

            try
            {
                //입력자료가 없습니다.
                if (staData == null)
                {
                    ruleResult.status = 1011;
                    ruleResult.message = ChartMessage.rules.chk1001; //입력 자료가 없습니다, Data is null.;
                    goto NEXT;
                }

                int i = 0;
                int nRowMax = staData.Rows.Count;
                int nSeqID = 0;
                //double nXBar = SpcLimit.MIN;
                double nValue = SpcLimit.MIN;
                double nBeforeValueMax = SpcLimit.MIN;
                double nBeforeValueMin = SpcLimit.MAX;
                int nCheckCount = 0;
                int nCheckCount01 = 0;
                int nCheckRowIndex = -1;

                for (i = 0; i < nRowMax; i++)
                {
                    DataRow rowdata = staData.Rows[i];
                    nSeqID = rowdata["TEMPID"].ToSafeInt32();

                    nValue = rowdata["BAR"].ToSafeDoubleStaMin();

                    // 증가 Check
                    if (nValue > nBeforeValueMax)
                    {
                        nBeforeValueMax = nValue;
                        if (nCheckCount == 0)
                        {
                            ruleResult.nStartPoint = nSeqID;
                        }

                        ruleResult.nEndPoint = nSeqID;
                        nCheckCount += 1;
                    }
                    else
                    {
                        nBeforeValueMax = nValue;
                        nCheckCount = 0;
                    }

                    if (nCheckCount >= 6)
                    {
                        if (nCheckCount == 6)
                        {
                            RuleCheckSerialsPoint rPoint = RuleCheckSerialsPoint.Create();
                            rPoint.nStartPoint = ruleResult.nStartPoint;
                            rPoint.nEndPoint = ruleResult.nEndPoint + 1;
                            #region 값 저장.
                            ruleResult.isRuleOver = true;
                            for (int n = rPoint.nStartPoint; n < rPoint.nEndPoint; n++)
                            {
                                DataRow rowValue = staData.Rows[n];
                                RuleCheckSerialsPointData pointData = RuleCheckSerialsPointData.Create();
                                pointData.nindex = n;
                                pointData.nCheckValue = rowValue["BAR"].ToSafeDoubleStaMin();
                                rPoint.listPointData.Add(pointData);
                            }
                            #endregion 값 저장.
                            ruleResult.listPoint.Add(rPoint);
                            ruleResult.nRuleCheckCount += 1;
                            ruleResult.message = ChartMessage.rules.chk1011; //Rule Over 발생.
                        }
                        else
                        {
                            if (ruleResult.listPoint != null && ruleResult.listPoint.Count > 0)
                            {
                                nCheckRowIndex = ruleResult.nRuleCheckCount - 1;
                                ruleResult.listPoint[nCheckRowIndex].nEndPoint = ruleResult.nEndPoint + 1;
                            }
                        }
                    }


                    // 감소 Check
                    if (nValue < nBeforeValueMin)
                    {
                        nBeforeValueMin = nValue;
                        if (nCheckCount01 == 0)
                        {
                            ruleResult.nStartPoint01 = nSeqID;
                        }

                        ruleResult.nEndPoint01 = nSeqID;
                        nCheckCount01 += 1;
                    }
                    else
                    {
                        nBeforeValueMin = nValue;
                        nCheckCount01 = 0;
                    }

                    if (nCheckCount01 >= 6)
                    {
                        if (nCheckCount01 == 6)
                        {
                            RuleCheckSerialsPoint rPoint = RuleCheckSerialsPoint.Create();
                            rPoint.nStartPoint = ruleResult.nStartPoint01;
                            rPoint.nEndPoint = ruleResult.nEndPoint01 + 1;
                            #region 값 저장.
                            ruleResult.isRuleOver = true;
                            for (int n = rPoint.nStartPoint; n < rPoint.nEndPoint; n++)
                            {
                                DataRow rowValue = staData.Rows[n];
                                RuleCheckSerialsPointData pointData = RuleCheckSerialsPointData.Create();
                                pointData.nindex = n;
                                pointData.nCheckValue = rowValue["BAR"].ToSafeDoubleStaMin();
                                rPoint.listPointData.Add(pointData);
                            }
                            #endregion 값 저장.
                            ruleResult.listPoint.Add(rPoint);
                            ruleResult.nRuleCheckCount += 1;
                            ruleResult.message = ChartMessage.rules.chk1011; //Rule Over 발생.
                        }
                        else
                        {
                            if (ruleResult.listPoint != null && ruleResult.listPoint.Count > 0)
                            {
                                nCheckRowIndex = ruleResult.nRuleCheckCount - 1;
                                ruleResult.listPoint[nCheckRowIndex].nEndPoint = ruleResult.nEndPoint + 1;
                            }
                        }
                    }

                }

                if (!ruleResult.isRuleOver)
                {
                    ruleResult.nStartPoint = 0;
                    ruleResult.nEndPoint = 0;
                }

                ruleResult.status = 0;

            }
            catch (Exception ex)
            {
                ruleResult.status = 1;
                ruleResult.message = ex.Message.ToString();
                Console.WriteLine(ex.Message);
            }

        NEXT:
            return ruleResult;

        }
        /// <summary>
        ///행에서 14 개 이상의 포인트가 방향을 번갈아 가면서 증가하고 감소합니다.
        ///Fourteen (or more) points in a row alternate in direction, increasing then decreasing.
        /// </summary>
        /// <param name="staData"></param>
        /// <returns></returns>
        public RuleCheckResult Rule4_Check(RtnControlDataTable staData)
        {
            RuleCheckResult ruleResult = RuleCheckResult.Create();
            ruleResult.nRuleCheckCount = 0;
            ruleResult.RuleNo = 4;
            ruleResult.message = ChartMessage.rules.chk1000; //정상검증.

            try
            {
                //입력자료가 없습니다.
                if (staData == null)
                {
                    ruleResult.status = 1011;
                    ruleResult.message = ChartMessage.rules.chk1001; //입력 자료가 없습니다, Data is null.;
                    goto NEXT;
                }

                int i = 0;
                int nRowMax = staData.Rows.Count;
                int nSeqID = 0;
                //double nXBar = SpcLimit.MIN;
                double nValue = SpcLimit.MIN;
                double nBeforeValue = SpcLimit.MIN;
                //double nBeforeValueMax = SpcLimit.MIN;
                //double nBeforeValueMin = SpcLimit.MAX;
                int nCheckCount = 0;
                //int nCheckCount01 = 0;
                int nAddCheck = 0;
                int nCheckRowIndex = -1;

                for (i = 0; i < nRowMax; i++)
                {
                    DataRow rowdata = staData.Rows[i];
                    nSeqID = rowdata["TEMPID"].ToSafeInt32();

                    nValue = rowdata["BAR"].ToSafeDoubleStaMin();

                    if (nBeforeValue == SpcLimit.MIN)
                    {
                        nBeforeValue = nValue;
                    }

                    if (nValue > nBeforeValue)
                    {
                        if (nAddCheck < 0 || nAddCheck == 0)
                        {
                            nAddCheck = 1;
                            if (nCheckCount == 0)
                            {
                                ruleResult.nStartPoint = nSeqID;
                            }
                            ruleResult.nEndPoint = nSeqID;
                            nCheckCount += 1;
                        }
                        else
                        {
                            nCheckCount = 0;
                        }
                    }

                    if (nValue < nBeforeValue)
                    {
                        if (nAddCheck > 0 || nAddCheck == 0)
                        {
                            nAddCheck = -1;
                            if (nCheckCount == 0)
                            {
                                ruleResult.nStartPoint = nSeqID;
                            }
                            ruleResult.nEndPoint = nSeqID;
                            nCheckCount += 1;
                        }
                        else
                        {
                            nCheckCount = 0;
                        }
                    }
                    else
                    {
                    }

                    nBeforeValue = nValue;

                    if (nCheckCount >= 14)
                    {
                        if (nCheckCount == 14)
                        {
                            RuleCheckSerialsPoint rPoint = RuleCheckSerialsPoint.Create();
                            rPoint.nStartPoint = ruleResult.nStartPoint;
                            rPoint.nEndPoint = ruleResult.nEndPoint + 1;
                            #region 값 저장.
                            ruleResult.isRuleOver = true;
                            for (int n = rPoint.nStartPoint; n < rPoint.nEndPoint; n++)
                            {
                                DataRow rowValue = staData.Rows[n];
                                RuleCheckSerialsPointData pointData = RuleCheckSerialsPointData.Create();
                                pointData.nindex = n;
                                pointData.nCheckValue = rowValue["BAR"].ToSafeDoubleStaMin();
                                rPoint.listPointData.Add(pointData);
                            }
                            #endregion 값 저장.
                            ruleResult.listPoint.Add(rPoint);
                            ruleResult.nRuleCheckCount += 1;
                            ruleResult.message = ChartMessage.rules.chk1011; //Rule Over 발생.
                        }
                        else
                        {
                            if (ruleResult.listPoint != null && ruleResult.listPoint.Count > 0)
                            {
                                nCheckRowIndex = ruleResult.nRuleCheckCount - 1;
                                ruleResult.listPoint[nCheckRowIndex].nEndPoint = ruleResult.nEndPoint + 1;
                            }
                        }
                    }

                }

                if (!ruleResult.isRuleOver)
                {
                    ruleResult.nStartPoint = 0;
                    ruleResult.nEndPoint = 0;
                }

                ruleResult.status = 0;

            }
            catch (Exception ex)
            {
                ruleResult.status = 1;
                ruleResult.message = ex.Message.ToString();
                Console.WriteLine(ex.Message);
            }

        NEXT:
            return ruleResult;

        }

        /// <summary>
        ///행의 세 점 중 두 개 (또는 세 개)는 같은 방향의 평균에서 두 개 이상의 표준 편차입니다.
        ///Two (or three) out of three points in a row are more than 2 standard deviations from the mean in the same direction.
        /// </summary>
        /// <param name="staData"></param>
        /// <returns></returns>
        public RuleCheckResult Rule5_Check(RtnControlDataTable staData)
        {
            RuleCheckResult ruleResult = RuleCheckResult.Create();
            ruleResult.nRuleCheckCount = 0;
            ruleResult.RuleNo = 5;
            ruleResult.message = ChartMessage.rules.chk1000; //정상검증.

            try
            {
                //입력자료가 없습니다.
                if (staData == null)
                {
                    ruleResult.status = 1011;
                    ruleResult.message = ChartMessage.rules.chk1001; //입력 자료가 없습니다, Data is null.;
                    goto NEXT;
                }

                int i = 0;
                int nRowMax = staData.Rows.Count;
                int nSeqID = 0;
                //double nXBar = SpcLimit.MIN;
                double nValue = SpcLimit.MIN;
                //double nBeforeValue = SpcLimit.MIN;
                double nBeforeValueMax = SpcLimit.MIN;
                double nBeforeValueMin = SpcLimit.MAX;
                int nCheckCount = 0;
                //int nCheckCount01 = 0;
                //int nAddCheck = 0;
                int nCheckRowIndex = -1;

                //Sigma Check.
                if (_controlSpec != null && _controlSpec.sigmaResult != null)
                {
                    //Sigma 2 사용.
                    if (_controlSpec.sigmaResult.nSigma2Max != SpcLimit.MAX && _controlSpec.sigmaResult.nSigma2Max != SpcLimit.MIN)
                    {
                        nBeforeValueMax = _controlSpec.sigmaResult.nSigma2Max;
                    }
                    else
                    {
                        ruleResult.status = 1012;
                        ruleResult.message = SpcLibMessage.common.rule1005; //UCL 2 Sigma가 없습니다.
                        goto NEXT;
                    }

                    if (_controlSpec.sigmaResult.nSigma2Min != SpcLimit.MAX && _controlSpec.sigmaResult.nSigma2Min != SpcLimit.MIN)
                    {
                        nBeforeValueMin = _controlSpec.sigmaResult.nSigma2Min;
                    }
                    else
                    {
                        ruleResult.status = 1013;
                        ruleResult.message = SpcLibMessage.common.rule1006; //LCL 2 Sigma가 없습니다.
                        goto NEXT;
                    }
                }
                else
                {
                    ruleResult.status = 1014;
                    ruleResult.message = SpcLibMessage.common.rule1004; //Sigma가 없습니다.
                    goto NEXT;
                }

                for (i = 0; i < nRowMax; i++)
                {
                    DataRow rowdata = staData.Rows[i];
                    nSeqID = rowdata["TEMPID"].ToSafeInt32();

                    nValue = rowdata["BAR"].ToSafeDoubleStaMin();
                    
                    if (nValue > nBeforeValueMax || nValue < nBeforeValueMin)
                    {
                        if (nCheckCount == 0)
                        {
                            ruleResult.nStartPoint = nSeqID;
                        }
                        ruleResult.nEndPoint = nSeqID;
                        nCheckCount += 1;
                    }
                    else
                    {
                        nCheckCount = 0;
                    }


                    if (nCheckCount >= 2)
                    {
                        if (nCheckCount == 2)
                        {
                            RuleCheckSerialsPoint rPoint = RuleCheckSerialsPoint.Create();
                            rPoint.nStartPoint = ruleResult.nStartPoint;
                            rPoint.nEndPoint = ruleResult.nEndPoint + 1;
                            #region 값 저장.
                            ruleResult.isRuleOver = true;
                            for (int n = rPoint.nStartPoint; n < rPoint.nEndPoint; n++)
                            {
                                DataRow rowValue = staData.Rows[n];
                                RuleCheckSerialsPointData pointData = RuleCheckSerialsPointData.Create();
                                pointData.nindex = n;
                                pointData.nCheckValue = rowValue["BAR"].ToSafeDoubleStaMin();
                                rPoint.listPointData.Add(pointData);
                            }
                            #endregion 값 저장.
                            ruleResult.listPoint.Add(rPoint);
                            ruleResult.nRuleCheckCount += 1;
                            ruleResult.message = ChartMessage.rules.chk1011; //Rule Over 발생.
                        }
                        else
                        {
                            if (ruleResult.listPoint != null && ruleResult.listPoint.Count > 0)
                            {
                                nCheckRowIndex = ruleResult.nRuleCheckCount - 1;
                                ruleResult.listPoint[nCheckRowIndex].nEndPoint = ruleResult.nEndPoint + 1;
                            }
                        }
                    }

                }

                if (!ruleResult.isRuleOver)
                {
                    ruleResult.nStartPoint = 0;
                    ruleResult.nEndPoint = 0;
                }

                ruleResult.status = 0;

            }
            catch (Exception ex)
            {
                ruleResult.status = 1;
                ruleResult.message = ex.Message.ToString();
                Console.WriteLine(ex.Message);
            }

        NEXT:
            return ruleResult;

        }
        /// <summary>
        /// 한 행의 5 개 점 중 4 개 (또는 5 개)는 같은 방향으로 평균에서 1 표준 편차 이상입니다.
        /// our (or five) out of five points in a row are more than 1 standard deviation from the mean in the same direction.
        /// </summary>
        /// <param name="staData"></param>
        /// <returns></returns>
        public RuleCheckResult Rule6_Check(RtnControlDataTable staData)
        {
            RuleCheckResult ruleResult = RuleCheckResult.Create();
            ruleResult.nRuleCheckCount = 0;
            ruleResult.RuleNo = 6;
            ruleResult.message = ChartMessage.rules.chk1000; //정상검증.

            try
            {
                //입력자료가 없습니다.
                if (staData == null)
                {
                    ruleResult.status = 1011;
                    ruleResult.message = ChartMessage.rules.chk1001; //입력 자료가 없습니다, Data is null.;
                    goto NEXT;
                }

                int i = 0;
                int nRowMax = staData.Rows.Count;
                int nSeqID = 0;
                //double nXBar = SpcLimit.MIN;
                double nValue = SpcLimit.MIN;
                //double nBeforeValue = SpcLimit.MIN;
                double nBeforeValueMax = SpcLimit.MIN;
                double nBeforeValueMin = SpcLimit.MAX;
                int nCheckCount = 0;
                //int nCheckCount01 = 0;
                //int nAddCheck = 0;
                int nCheckRowIndex = -1;

                //Sigma Check.
                if (_controlSpec != null && _controlSpec.sigmaResult != null)
                {
                    //Sigma 1 사용.
                    if (_controlSpec.sigmaResult.nSigma1Max != SpcLimit.MAX && _controlSpec.sigmaResult.nSigma1Max != SpcLimit.MIN)
                    {
                        nBeforeValueMax = _controlSpec.sigmaResult.nSigma1Max;
                    }
                    else
                    {
                        ruleResult.status = 1012;
                        ruleResult.message = SpcLibMessage.common.rule1007; //UCL 1 Sigma가 없습니다.
                        goto NEXT;
                    }

                    if (_controlSpec.sigmaResult.nSigma1Min != SpcLimit.MAX && _controlSpec.sigmaResult.nSigma1Min != SpcLimit.MIN)
                    {
                        nBeforeValueMin = _controlSpec.sigmaResult.nSigma1Min;
                    }
                    else
                    {
                        ruleResult.status = 1013;
                        ruleResult.message = SpcLibMessage.common.rule1008; //LCL 1 Sigma가 없습니다.
                        goto NEXT;
                    }
                }
                else
                {
                    ruleResult.status = 1014;
                    ruleResult.message = SpcLibMessage.common.rule1004; //Sigma가 없습니다.
                    goto NEXT;
                }

                for (i = 0; i < nRowMax; i++)
                {
                    DataRow rowdata = staData.Rows[i];
                    nSeqID = rowdata["TEMPID"].ToSafeInt32();

                    nValue = rowdata["BAR"].ToSafeDoubleStaMin();

                    if (nValue > nBeforeValueMax || nValue < nBeforeValueMin)
                    {
                        if (nCheckCount == 0)
                        {
                            ruleResult.nStartPoint = nSeqID;
                        }
                        ruleResult.nEndPoint = nSeqID;
                        nCheckCount += 1;
                    }
                    else
                    {
                        nCheckCount = 0;
                    }


                    if (nCheckCount >= 4)
                    {
                        if (nCheckCount == 4)
                        {
                            RuleCheckSerialsPoint rPoint = RuleCheckSerialsPoint.Create();
                            rPoint.nStartPoint = ruleResult.nStartPoint;
                            rPoint.nEndPoint = ruleResult.nEndPoint + 1;
                            #region 값 저장.
                            ruleResult.isRuleOver = true;
                            for (int n = rPoint.nStartPoint; n < rPoint.nEndPoint; n++)
                            {
                                DataRow rowValue = staData.Rows[n];
                                RuleCheckSerialsPointData pointData = RuleCheckSerialsPointData.Create();
                                pointData.nindex = n;
                                pointData.nCheckValue = rowValue["BAR"].ToSafeDoubleStaMin();
                                rPoint.listPointData.Add(pointData);
                            }
                            #endregion 값 저장.
                            ruleResult.listPoint.Add(rPoint);
                            ruleResult.nRuleCheckCount += 1;
                            ruleResult.message = ChartMessage.rules.chk1011; //Rule Over 발생.
                        }
                        else
                        {
                            if (ruleResult.listPoint != null && ruleResult.listPoint.Count > 0)
                            {
                                nCheckRowIndex = ruleResult.nRuleCheckCount - 1;
                                ruleResult.listPoint[nCheckRowIndex].nEndPoint = ruleResult.nEndPoint + 1;
                            }
                        }
                    }

                }

                if (!ruleResult.isRuleOver)
                {
                    ruleResult.nStartPoint = 0;
                    ruleResult.nEndPoint = 0;
                }

            NEXT:
                ruleResult.status = 0;

            }
            catch (Exception ex)
            {
                ruleResult.status = 1;
                ruleResult.message = ex.Message.ToString();
                Console.WriteLine(ex.Message);
            }

            return ruleResult;

        }

        /// <summary>
        /// 연속으로 15 개의 점이 모두 평균의 양쪽에있는 평균의 1 표준 편차 내에 있습니다.
        /// Fifteen points in a row are all within 1 standard deviation of the mean on either side of the mean.
        /// </summary>
        /// <param name="staData"></param>
        /// <returns></returns>
        public RuleCheckResult Rule7_Check(RtnControlDataTable staData)
        {
            RuleCheckResult ruleResult = RuleCheckResult.Create();
            ruleResult.nRuleCheckCount = 0;
            ruleResult.RuleNo = 7;
            ruleResult.message = ChartMessage.rules.chk1000; //정상검증.

            try
            {
                //입력자료가 없습니다.
                if (staData == null)
                {
                    ruleResult.status = 1011;
                    ruleResult.message = ChartMessage.rules.chk1001; //입력 자료가 없습니다, Data is null.;
                    goto NEXT;
                }

                int i = 0;
                int nRowMax = staData.Rows.Count;
                int nSeqID = 0;
                //double nXBar = SpcLimit.MIN;
                double nValue = SpcLimit.MIN;
                //double nBeforeValue = SpcLimit.MIN;
                double nBeforeValueMax = SpcLimit.MIN;
                double nBeforeValueMin = SpcLimit.MAX;
                int nCheckCount = 0;
                //int nCheckCount01 = 0;
                //int nAddCheck = 0;
                int nCheckRowIndex = -1;

                //Sigma Check.
                if (_controlSpec != null && _controlSpec.sigmaResult != null)
                {
                    //Sigma 1 사용.
                    if (_controlSpec.sigmaResult.nSigma1Max != SpcLimit.MAX && _controlSpec.sigmaResult.nSigma1Max != SpcLimit.MIN)
                    {
                        nBeforeValueMax = _controlSpec.sigmaResult.nSigma1Max;
                    }
                    else
                    {
                        ruleResult.status = 1012;
                        ruleResult.message = SpcLibMessage.common.rule1007; //UCL 1 Sigma가 없습니다.
                        goto NEXT;
                    }

                    if (_controlSpec.sigmaResult.nSigma1Min != SpcLimit.MAX && _controlSpec.sigmaResult.nSigma1Min != SpcLimit.MIN)
                    {
                        nBeforeValueMin = _controlSpec.sigmaResult.nSigma1Min;
                    }
                    else
                    {
                        ruleResult.status = 1013;
                        ruleResult.message = SpcLibMessage.common.rule1008; //LCL 1 Sigma가 없습니다.
                        goto NEXT;
                    }
                }
                else
                {
                    ruleResult.status = 1014;
                    ruleResult.message = SpcLibMessage.common.rule1004; //Sigma가 없습니다.
                    goto NEXT;
                }

                for (i = 0; i < nRowMax; i++)
                {
                    DataRow rowdata = staData.Rows[i];
                    nSeqID = rowdata["TEMPID"].ToSafeInt32();

                    nValue = rowdata["BAR"].ToSafeDoubleStaMin();

                    if (nValue <= nBeforeValueMax && nValue >= nBeforeValueMin)
                    {
                        if (nCheckCount == 0)
                        {
                            ruleResult.nStartPoint = nSeqID;
                        }
                        ruleResult.nEndPoint = nSeqID;
                        nCheckCount += 1;
                    }
                    else
                    {
                        nCheckCount = 0;
                    }


                    if (nCheckCount >= 15)
                    {
                        if (nCheckCount == 15)
                        {
                            RuleCheckSerialsPoint rPoint = RuleCheckSerialsPoint.Create();
                            rPoint.nStartPoint = ruleResult.nStartPoint;
                            rPoint.nEndPoint = ruleResult.nEndPoint + 1;
                            #region 값 저장.
                            ruleResult.isRuleOver = true;
                            for (int n = rPoint.nStartPoint; n < rPoint.nEndPoint; n++)
                            {
                                DataRow rowValue = staData.Rows[n];
                                RuleCheckSerialsPointData pointData = RuleCheckSerialsPointData.Create();
                                pointData.nindex = n;
                                pointData.nCheckValue = rowValue["BAR"].ToSafeDoubleStaMin();
                                rPoint.listPointData.Add(pointData);
                            }
                            #endregion 값 저장.
                            ruleResult.listPoint.Add(rPoint);
                            ruleResult.nRuleCheckCount += 1;
                            ruleResult.message = ChartMessage.rules.chk1011; //Rule Over 발생.
                        }
                        else
                        {
                            if (ruleResult.listPoint != null && ruleResult.listPoint.Count > 0)
                            {
                                nCheckRowIndex = ruleResult.nRuleCheckCount - 1;
                                ruleResult.listPoint[nCheckRowIndex].nEndPoint = ruleResult.nEndPoint + 1;
                            }
                        }
                    }

                }

                if (!ruleResult.isRuleOver)
                {
                    ruleResult.nStartPoint = 0;
                    ruleResult.nEndPoint = 0;
                }

                ruleResult.status = 0;

            }
            catch (Exception ex)
            {
                ruleResult.status = 1;
                ruleResult.message = ex.Message.ToString();
                Console.WriteLine(ex.Message);
            }

        NEXT:
            return ruleResult;

        }
        /// <summary>
        /// 한 행에 8 개의 점이 있지만 평균의 1 표준 편차 내에있는 점은 없으며 평균과 양 방향에 있지 않습니다.
        /// Eight points in a row exist, but none within 1 standard deviation of the mean, and the points are in both directions from the mean.
        /// </summary>
        /// <param name="staData"></param>
        /// <returns></returns>
        public RuleCheckResult Rule8_Check(RtnControlDataTable staData)
        {
            RuleCheckResult ruleResult = RuleCheckResult.Create();
            ruleResult.nRuleCheckCount = 0;
            ruleResult.RuleNo = 8;
            ruleResult.message = ChartMessage.rules.chk1000; //정상검증.

            try
            {
                //입력자료가 없습니다.
                if (staData == null)
                {
                    ruleResult.status = 1011;
                    ruleResult.message = ChartMessage.rules.chk1001; //입력 자료가 없습니다, Data is null.;
                    goto NEXT;
                }

                int i = 0;
                int nRowMax = staData.Rows.Count;
                int nSeqID = 0;
                //double nXBar = SpcLimit.MIN;
                double nValue = SpcLimit.MIN;
                //double nBeforeValue = SpcLimit.MIN;
                double nBeforeValueMax = SpcLimit.MIN;
                double nBeforeValueMin = SpcLimit.MAX;
                int nCheckCount = 0;
                //int nCheckCount01 = 0;
                //int nAddCheck = 0;
                int nCheckRowIndex = -1;

                //Sigma Check.
                if (_controlSpec != null && _controlSpec.sigmaResult != null)
                {
                    //Sigma 1 사용.
                    if (_controlSpec.sigmaResult.nSigma1Max != SpcLimit.MAX && _controlSpec.sigmaResult.nSigma1Max != SpcLimit.MIN)
                    {
                        nBeforeValueMax = _controlSpec.sigmaResult.nSigma1Max;
                    }
                    else
                    {
                        ruleResult.status = 1012;
                        ruleResult.message = SpcLibMessage.common.rule1007; //UCL 1 Sigma가 없습니다.
                        goto NEXT;
                    }

                    if (_controlSpec.sigmaResult.nSigma1Min != SpcLimit.MAX && _controlSpec.sigmaResult.nSigma1Min != SpcLimit.MIN)
                    {
                        nBeforeValueMin = _controlSpec.sigmaResult.nSigma1Min;
                    }
                    else
                    {
                        ruleResult.status = 1013;
                        ruleResult.message = SpcLibMessage.common.rule1008; //LCL 1 Sigma가 없습니다.
                        goto NEXT;
                    }
                }
                else
                {
                    ruleResult.status = 1014;
                    ruleResult.message = SpcLibMessage.common.rule1004; //Sigma가 없습니다.
                    goto NEXT;
                }

                for (i = 0; i < nRowMax; i++)
                {
                    DataRow rowdata = staData.Rows[i];
                    nSeqID = rowdata["TEMPID"].ToSafeInt32();

                    nValue = rowdata["BAR"].ToSafeDoubleStaMin();

                    //한 행에 8 개의 점이 있지만 평균의 1 표준 편차 내에있는 점은 없으며 평균과 양 방향에 있지 않습니다.
                    Console.WriteLine(string.Format("Max: {0}, Value: {1}, Min:{2}", nBeforeValueMax, nValue, nBeforeValueMin));
                    if (nValue <= nBeforeValueMax && nValue >= nBeforeValueMin)
                    {
                        nCheckCount = 0;
                    }
                    else
                    {
                        if (nCheckCount == 0)
                        {
                            ruleResult.nStartPoint = nSeqID;
                        }
                        ruleResult.nEndPoint = nSeqID;
                        nCheckCount += 1;
                    }


                    if (nCheckCount >= 8)
                    {
                        if (nCheckCount == 8)
                        {
                            RuleCheckSerialsPoint rPoint = RuleCheckSerialsPoint.Create();
                            rPoint.nStartPoint = ruleResult.nStartPoint;
                            rPoint.nEndPoint = ruleResult.nEndPoint + 1;
                            #region 값 저장.
                            ruleResult.isRuleOver = true;
                            for (int n = rPoint.nStartPoint; n < rPoint.nEndPoint; n++)
                            {
                                DataRow rowValue = staData.Rows[n];
                                RuleCheckSerialsPointData pointData = RuleCheckSerialsPointData.Create();
                                pointData.nindex = n;
                                pointData.nCheckValue = rowValue["BAR"].ToSafeDoubleStaMin();
                                rPoint.listPointData.Add(pointData);
                            }
                            #endregion 값 저장.
                            ruleResult.listPoint.Add(rPoint);
                            ruleResult.nRuleCheckCount += 1;
                            ruleResult.message = ChartMessage.rules.chk1011; //Rule Over 발생.
                        }
                        else
                        {
                            if (ruleResult.listPoint != null && ruleResult.listPoint.Count > 0)
                            {
                                nCheckRowIndex = ruleResult.nRuleCheckCount - 1;
                                ruleResult.listPoint[nCheckRowIndex].nEndPoint = ruleResult.nEndPoint + 1;
                            }
                        }
                    }

                }

                if (!ruleResult.isRuleOver)
                {
                    ruleResult.nStartPoint = 0;
                    ruleResult.nEndPoint = 0;
                }

                ruleResult.status = 0;

            }
            catch (Exception ex)
            {
                ruleResult.status = 1;
                ruleResult.message = ex.Message.ToString();
                Console.WriteLine(ex.Message);
            }

        NEXT:
            return ruleResult;

        }

        /// <summary>
        /// 해석용 Rule Check
        /// </summary>
        public void SpecRuleCheckNelsonColor(RuleCheckResult ruleResult)
        {
            Application.DoEvents();
            double[] val = new double[1];
            int i = 0;
            int maxNValueCount = 0;
            bool isRuleCheck = false;
            Color color = ruleResult.RuleColor;

            try
            {
                maxNValueCount = this.ChrXbarR.Series["Series03Value"].Points.Count;
                if (ruleResult.listPoint != null && ruleResult.listPoint.Count > 0)
                {
                    foreach (RuleCheckSerialsPoint item in ruleResult.listPoint)
                    {
                        if (item.nEndPoint >= maxNValueCount)
                        {
                            item.nEndPoint = maxNValueCount;
                        }

                        for (i = item.nStartPoint; i < item.nEndPoint; i++)
                        {
                            //nVal = this.ChrXbarR.Series["Series03Value"].Points[i].Values[0];
                            //this.ChrXbarR.Series["Series03Value"].Points[i].Color = Color.Lime;
                            this.ChrXbarR.Series["Series03Value"].Points[i].Color = color;
                            this.ChrXbarR.Series["Series04OverRuleUCL"].Points[i].Color = color;
                            this.ChrXbarR.Series["Series05OverRuleLCL"].Points[i].Color = color;
                            this.ChrXbarR.Series["Series05OverRuleLCL"].Points[i].Color = color;
                            this.ChrXbarR.Series["Series07OverRuleLSL"].Points[i].Color = color;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //this.ChrXbarR.Refresh();
        }

        #endregion Nelson Rules Check

        
    }


}//end namespace