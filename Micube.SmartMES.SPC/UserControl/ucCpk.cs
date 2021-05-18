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

#endregion

namespace Micube.SmartMES.SPC.UserControl
{
    /// <summary>
    /// 프 로 그 램 명  : SPC User Control XBar Chart
    /// 업  무  설  명  : SPC 통계에서 사용되는 XBar-R & S Chart
    /// 생    성    자  : 이승우
    /// 생    성    일  : 2019-07-22
    /// 수  정  이  력  : 
    /// 
    /// 2019-09-02  중심치 이탈 추가. Raw Data View 수정.
    /// 2019-08-20  Chart Whole Range 처리, 상태 Message 처리
    /// 2019-07-22  Chart Data 초기화 추가
    /// 2019-07-11  최초작성
    /// 
    /// </summary>
    public partial class ucCpk : DevExpress.XtraEditors.XtraUserControl
    {
        #region Local Variables
        /// <summary>
        /// 최초 화면 Load 구분자
        /// </summary>
        private bool _IsFirstInitialize = false;
        //public delegate void ControlDoubleClickEventHandler(object sender, BaseCustomEventArgs arg);
        //public virtual event ControlDoubleClickEventHandler ControlDoubleClickEvent;
        //public delegate void ControlMouseClickEventHandler(object sender, MouseEventArgs e);
        //public virtual event ControlMouseClickEventHandler ControlMouseClickEvent;
        public delegate void SpcChartEnterEventHandler(object sender, EventArgs e, SpcEventsOption se);
        public virtual event SpcChartEnterEventHandler SpcCpkChartEnterEventHandler;

        public SpcEventsOption spcEop = new SpcEventsOption();
        //public bool IsTestMode = false;
        public bool IsDetailPopupLock = false;
        private RtnControlDataTable _dtControlData;
        /// <summary>
        /// 관리도 SPEC 정의
        /// </summary>
        private ControlSpec _controlSpec;
        /// <summary>
        /// SPC 통계 - 관리도 알고리즘 입력자료
        /// </summary>
        private SPCPara _spcPara;
        /// <summary>
        /// Chart의 Whole 값
        /// </summary>
        ChartWholeValue _wholeRangeCpk = ChartWholeValue.Create();
        /// <summary>
        /// Spec Cancel flag
        /// </summary>
        IsSpecCancel _isCancelCpk = IsSpecCancel.Create();
        private bool _IsOverRule = false;
        private bool _IsPaint = false;
        private int _x = 0;
        private int _y = 0;
        /// <summary>
        /// Subgroup ID 입력
        /// </summary>
        private string _SUBGROUP = "";
        /// <summary>
        /// Subgroup Name 입력
        /// </summary>
        private string _SUBGROUPNAME = "";
        #endregion

        #region 생성자

        /// <summary>
        /// User Control XBar Chart 생성자
        /// </summary>
        public ucCpk()
        {
            InitializeComponent();

            ChartMessage.MessageSetting();
            this.ControlChartDataMappingClear();

            ////IsTestMode = true;
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

            _IsFirstInitialize = true;
        }

        #endregion

        #region 컨텐츠 영역 초기화

        private void InitializeContent()
        {
            InitializeEvent();
            InitializeGrid();
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {

        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this._IsFirstInitialize = false;
            //공정능력분석 Chart Click 이벤트
            this.ChrCpk.Click += (s, e) =>
            {
                //sia확인 : Chart 팝업 - ChrXbarR_Click
                ChartHitInfo hitInfo = ((s as SmartChart).CalcHitInfo((e as MouseEventArgs).Location));
                SeriesPoint sp = hitInfo.SeriesPoint;
                _x = hitInfo.HitPoint.X;
                _y = hitInfo.HitPoint.Y;
                _IsPaint = true;
            };

            //Chart Paint
            this.ChrCpk.Paint += (s, e) =>
            {
                if (_IsPaint == false)
                {
                    //ControlLinePaint(sender, e);
                }
                else
                {
                    _IsPaint = false;
                    // Obtain hit information under the test point. 
                    ChartHitInfo hi = ChrCpk.CalcHitInfo(_x, _y);

                    // Obtain the series point under the test point. 
                    SeriesPoint point = hi.SeriesPoint;

                    // Check whether the series point was clicked or not. 
                    if (point != null)
                    {
                        this.PopupRawDataEdit(point);
                        //this.txtPointView.Text = point.Argument.ToString();
                        //string pointInfo = string.Format("asixs 라벨: {0},  값: {1}"
                        //    , point.Argument.ToString()
                        //    , point.Values[0].ToSafeString());
                        //MessageBox.Show(pointInfo);
                    }
                }
            };

            //Cpk Chart Enter Focus 이벤트
            this.ChrCpk.Enter += (s, e) =>
            {
                spcEop.groupName = "groupName";
                spcEop.subName = "subName";
                spcEop.ChartName = ChrCpk.Name.ToSafeString();
                SpcCpkChartEnterEventHandler?.Invoke(s, e, spcEop);
            };

            //공정능력 Chart USL 표시 유무
            //sia확인 : 04.Cpk Spec 이벤트
            this.chkUsl.CheckedChanged += (s, e) =>
            {
                ((XYDiagram)ChrCpk.Diagram).AxisX.ConstantLines["ConstantLine01USL"].Visible = this.chkUsl.Checked;
            };

            //공정능력 Chart Mean 표시 유무
            this.chkMean.CheckedChanged += (s, e) =>
            {
                ((XYDiagram)ChrCpk.Diagram).AxisX.ConstantLines["ConstantLine02CSL"].Visible = this.chkMean.Checked;
            };

            //공정능력 Chart LSL 표시 유무
            this.chkLsl.CheckedChanged += (s, e) =>
            {
                if (_IsFirstInitialize != true) return;
                ((XYDiagram)ChrCpk.Diagram).AxisX.ConstantLines["ConstantLine03LSL"].Visible = this.chkLsl.Checked;
            };

            //목표값 Chart CSL 표시 유무
            this.chkCsl.CheckedChanged += (s, e) =>
            {
                if (_IsFirstInitialize != true) return;
                ((XYDiagram)ChrCpk.Diagram).AxisX.ConstantLines["ConstantLineTACSL"].Visible = this.chkCsl.Checked;
                ChartWholeRangeChange(_isCancelCpk);
            };

            //중심치 이탈 상한
            this.chkUtl.CheckedChanged += (s, e) =>
            {
                if (_IsFirstInitialize != true) return;
                ((XYDiagram)ChrCpk.Diagram).AxisX.ConstantLines["ConstantLineTAUSL"].Visible = this.chkUtl.Checked;
                _isCancelCpk.taUsl = !chkUtl.Checked;
                ChartWholeRangeChange(_isCancelCpk);
            };

            ////중심치 이탈 하한
            //this.chkLtl.CheckedChanged += (s, e) =>
            //{
            //    if (_IsFirstInitialize != true) return;
            //    ((XYDiagram)ChrCpk.Diagram).AxisX.ConstantLines["ConstantLineTALSL"].Visible = this.chkLtl.Checked;
            //    _isCancelCpk.taLsl = !chkLtl.Checked;
            //    ChartWholeRangeChange(_isCancelCpk);
            //};

            this._IsFirstInitialize = true;
            //ChrCpk.Paint += new System.Windows.Forms.PaintEventHandler(this.ChrCpk_Paint);
            //// TODO : 화면에서 사용할 이벤트 추가
            //chkMean.CheckedChanged += new System.EventHandler(ChkSigma3_CheckedChanged);
            //chkSigma2.CheckedChanged += new System.EventHandler(chkSigma2_CheckedChanged);
            //chkSigma1.CheckedChanged += new System.EventHandler(chkSigma1_CheckedChanged);
            //chkUsl.CheckedChanged += new System.EventHandler(chkUSL_CheckedChanged);
            //chkCSL.CheckedChanged += new System.EventHandler(ChkCSL_CheckedChanged);
            //chkLsl.CheckedChanged += new System.EventHandler(ChkLSL_CheckedChanged);
            //chkUCL.CheckedChanged += new System.EventHandler(ChkUCL_CheckedChanged);
            //chkCCL.CheckedChanged += new System.EventHandler(ChkCCL_CheckedChanged);
            //chkLCL.CheckedChanged += new System.EventHandler(ChkLCL_CheckedChanged);
            //chkRUCL.CheckedChanged += new System.EventHandler(ChkRUCL_CheckedChanged);
            //chkRCCL.CheckedChanged += new System.EventHandler(ChkRCCL_CheckedChanged);
            //chkRLCL.CheckedChanged += new System.EventHandler(ChkRLCL_CheckedChanged);
            //chkUOL.CheckedChanged += new System.EventHandler(ChkUOL_CheckedChanged);
            //chkLOL.CheckedChanged += new System.EventHandler(ChkLOL_CheckedChanged);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChrCpk_DoubleClick(object sender, EventArgs e)
        {
            if (!IsDetailPopupLock)
            {
                IsDetailPopupLock = true;

                if (_controlSpec != null && _controlSpec != null && _spcPara != null)
                {
                    if (_spcPara.InputData != null)
                    {
                        try
                        {
                            //TestSpcBaseChart0301Raw frm = new TestSpcBaseChart0301Raw();//Test
                            SpcStatusDetailChartPopup frm = new SpcStatusDetailChartPopup();
                            frm.ucChartDetail1.SpcDetailPopupInitialize(_controlSpec);
                            frm.ucChartDetail1.ucCpk1.IsDetailPopupLock = true;
                            frm.ucChartDetail1.ucCpk1.ControlChartDataMapping(_dtControlData, _controlSpec, _spcPara);
                            frm.ucChartDetail1.ucXBar1.IsDetailPopupLock = true;
                            frm.ucChartDetail1.ucXBar1.ControlChartDataMapping(_dtControlData, _controlSpec, _spcPara);
                            frm.ShowDialog();
                            IsDetailPopupLock = false;
                        }
                        catch (Exception)
                        {
                            IsDetailPopupLock = false;
                        }
                        finally
                        {
                            IsDetailPopupLock = false;
                        }

                    }
                }
            }
        }

        /// <summary>
        /// SeriesPoint
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChrCpk_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            string seriesName = e.Series.Name;
            double nValue = double.NaN;
            double nUSL = 73f;
            if (seriesName == "Series03Value")
            {
                nValue = e.SeriesPoint.Values[0].ToSafeDoubleNaN();
                if (nValue > nUSL)
                {

                    //AreaDrawOptions drawOptions = e.SeriesDrawOptions as AreaDrawOptions;
                    //if (drawOptions == null)
                    //    return;
                    //Color c = GetThicknessColor(val);
                    //Color c = Color.Red;
                    //e.SeriesPoint.Color = c;
                    //e.SeriesDrawOptions.Color = c;
                    //options.Color2 = Color.FromArgb(154, 196, 84);
                    //drawOptions.Color = Color.FromArgb(81, 137, 3);
                    //drawOptions.Border.Color = Color.FromArgb(100, 39, 91, 1);
                    //drawOptions.Color = c;
                    //e.SeriesDrawOptions.Color = Color.Green;
                    //e.SeriesPoint.Color = Color.Red;
                }
            }
        }
        
        #endregion

        #region Private Function

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
            //lblSpace01.Text = "";
            //lblSpace02.Text = "";  
            //lblSpace03.Text = "";
            //lblSpace04.Text = "";
            //lblSpace05.Text = "";
            //XYDiagram diagram = (XYDiagram)ChrCpk.Diagram;
            //diagram.AxisY.ConstantLines["ConstantLineUOL"].Visible = false;
            //diagram.AxisY.ConstantLines["ConstantLineLOL"].Visible = false;
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
            int fieldIndex = 0;
            int rowIndex = 0;
            string samplingName = string.Empty;
            string samplingRow = string.Empty;
            ParPIDataTable ChartRawData = new ParPIDataTable();
            var subGroupData = _spcPara.InputData.Where(x => x.SUBGROUP == subGroupId)
                                                .OrderBy(r1 => r1.SAMPLING).ThenByDescending(r2 => r2.NVALUE);
            //foreach (ParPIRow item in subGroupData)
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
                chartType = _controlSpec.spcOption.chartType,
                gridTitle = string.Format("{0} Raw Data", subGroupName)
            };

            frm.ShowDialog();
        }

        /// <summary>
        /// 관리도 Chart Test Data 입력.
        /// </summary>
        private void ControlChartDataMappingTest()
        {
            //sia확인 : 02.관리도 Chart Data 입력 및 Mapping.
            SpcParameter xbarData = SpcParameter.Create();
            ControlSpec controlSpec = new ControlSpec();
            xbarData.SpcData = xbarData.TestDataSpcCpk(out controlSpec);
            this.ChrCpk.DataSource = xbarData.SpcData;

            // Create a series, and add it to the chart. 
            //Series series1 = new Series("My Series", ViewType.Bar);
            this.ChrCpk.Series["Series01Bar"].ArgumentDataMember = "LabelInt";
            this.ChrCpk.Series["Series01Bar"].ValueDataMembers.AddRange(new string[] { "nValue" });

            pnlMain.Dock = DockStyle.Fill;

            //Chart Option
            //this.lblUCLValue.Text = Math.Round(controlSpec.nXbar.ucl, 3).ToSafeString();
            //this.lblLCLValue.Text = Math.Round(controlSpec.nXbar.lcl, 3).ToSafeString();

            //this.lblUSLValue.Text = Math.Round(controlSpec.nXbar.usl, 3).ToSafeString();
            //this.lblCSLValue.Text = Math.Round(controlSpec.nXbar.csl, 3).ToSafeString();
            //this.chkCSL.Text = "XBAR";
            //this.chkCSL.ForeColor = Color.Green;
            //this.lblLSLValue.Text = Math.Round(controlSpec.nXbar.lsl, 3).ToSafeString();

            XYDiagram diagram = (XYDiagram)ChrCpk.Diagram;
            diagram.AxisX.ConstantLines["ConstantLine01USL"].AxisValue = controlSpec.nXbar.usl;
            diagram.AxisX.ConstantLines["ConstantLine02CSL"].AxisValue = controlSpec.nXbar.csl;
            diagram.AxisX.ConstantLines["ConstantLine03LSL"].AxisValue = controlSpec.nXbar.lsl;
        }

        /// <summary>
        /// 변경 Spec명
        /// </summary>
        /// <param name="specName"></param>
        public void ChartWholeRangeChange(IsSpecCancel tempIsCancel)
        {
            XYDiagram diagram = (XYDiagram)ChrCpk.Diagram;
            XYDiagramDefaultPane defaultDiagPane = diagram.DefaultPane;
            ControlSpec tempControlSpec = new ControlSpec();
            ChartWholeValue tempWholeRangeXBar = ChartWholeValue.Create();

            //Chart Whole Range 
            tempControlSpec = _controlSpec;
            tempWholeRangeXBar = SpcFunction.chartWholeRangeCpk(tempControlSpec.nXbar, tempControlSpec.spcOption, tempIsCancel);
            //_wholeRangeCpk = SpcFunction.chartWholeRangeCpk(_controlSpec.nXbar, _controlSpec.spcOption, tempIsCancel);
            diagram.AxisX.WholeRange.MaxValue = tempWholeRangeXBar.max;
            diagram.AxisX.WholeRange.MinValue = tempWholeRangeXBar.min;
        }
        #endregion

        #region Public Function

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
        /// <summary>
        /// 관리도 Chart Test Data 입력.
        /// 화면에서 사용할 내부 함수 추가
        /// </summary>
        public void ControlChartDataMapping(RtnControlDataTable dtControlData, ControlSpec controlSpec, SPCPara spcPara)
        {
            this._IsFirstInitialize = false;
            string funsionMessage = "";
            string subGroupStatusMessage = "";
            //_dtControlData = rtnControlDataTableCopyDeep(dtControlData);
            //_controlSpec = controlSpec;
            //_spcPara = spcPara;

            // 분석 Message Check
            int nStatusMessageResult = 0;
            subGroupStatusMessage = "";
            if (spcPara.cpkOutData != null)
            {
                nStatusMessageResult = spcPara.cpkOutData.GetSubGroupStatusMessage(spcPara.subGrouopID.ToSafeString(), ref subGroupStatusMessage);
                if (nStatusMessageResult != 0)
                {
                    this.ControlChartDataMappingClear(1);
                    if (subGroupStatusMessage != "")
                    {
                        this.lbStatusMessage.Text = subGroupStatusMessage;
                        this.lbStatusMessage.Visible = true;
                    }
                    else
                    {
                        this.lbStatusMessage.Text = "";
                        this.lbStatusMessage.Visible = false;
                    }
                    return;
                }
                else
                {
                    this.lbStatusMessage.Text = "";
                    this.lbStatusMessage.Visible = false;
                }
            }

            _dtControlData = rtnControlDataTableCopyDeep(dtControlData);
            _controlSpec = controlSpec.CopyDeep();
            _spcPara = spcPara.CopyDeep();

            _SUBGROUP = controlSpec.sigmaResult.subGroup;
            _SUBGROUPNAME = controlSpec.sigmaResult.subGroupName;

            if (controlSpec == null)
            {
                //funsionMessage = "공정능력 SPEC Control이 정의되지 않았습니다. [controlSpec]";
                funsionMessage = SpcLibMessage.common.comCpk1022;
                this.ControlChartDataMappingClear(1);
                this.lbStatusMessage.Text = funsionMessage;
                this.lbStatusMessage.Visible = true;
                return;
            }

            if (controlSpec.sigmaResult == null)
            {
                //funsionMessage = "공정능력 Sigma 결과 값이 정의되지 않았습니다. [controlSpec.sigmaResult]";
                funsionMessage = SpcLibMessage.common.comCpk1023;
                this.ControlChartDataMappingClear(1);
                this.lbStatusMessage.Text = funsionMessage;
                this.lbStatusMessage.Visible = true;
                return;
            }

            if (controlSpec.sigmaResult.nSigmaWithin == SpcLimit.MIN || controlSpec.sigmaResult.nSigmaWithin == SpcLimit.MAX)
            {
                //funsionMessage = "공정능력 내부 Sigma 값이 정의되지 않았습니다. [controlSpec.sigmaResult.nSigmaWithin]";
                funsionMessage = SpcLibMessage.common.comCpk1024;
                this.ControlChartDataMappingClear(1);
                this.lbStatusMessage.Text = funsionMessage;
                this.lbStatusMessage.Visible = true;
                return;
            }

            if (controlSpec.sigmaResult.nSigmaTotal == SpcLimit.MIN || controlSpec.sigmaResult.nSigmaTotal == SpcLimit.MAX)
            {
                //funsionMessage = "공정능력 전체 Sigma 값이 정의되지 않았습니다. [controlSpec.sigmaResult.nSigmaTotal]";
                funsionMessage = SpcLibMessage.common.comCpk1025;
                this.ControlChartDataMappingClear(1);
                this.lbStatusMessage.Text = funsionMessage;
                this.lbStatusMessage.Visible = true;
                return;
            }

            //this.ChrCpk.Series[0].
            //sia확인 : 02.공정능력 Chart Data 입력 및 Mapping [ucCpk].
            SpcParameter xbarData = SpcParameter.Create();
            int i = 0;
            double samplingMin = 0;
            double samplingMax = 0;
            double samplingRange = 0;
            int samplingCount = 0;

            spcPara.subGrouopID = controlSpec.sigmaResult.subGroup;
            switch (controlSpec.spcOption.chartType)
            {
                case ControlChartType.I_MR:
                    //// using System.Linq; 필수선언.
                    var samplingDataIMR = spcPara.InputDataSum.Where(w => w.SUBGROUP == spcPara.subGrouopID)
                                              .GroupBy(g => g.SUBGROUP)
                                              .Select(s => new
                                              {
                                                  s.Key,
                                                  Min = s.Min(g => g.NVALUE),
                                                  Max = s.Max(g => g.NVALUE),
                                                  Count = s.Count()
                                              });

                    foreach (var item in samplingDataIMR)
                    {
                        samplingMin = item.Min.ToSafeDoubleZero();
                        samplingMax = item.Max.ToSafeDoubleZero();
                        samplingCount = item.Count;
                        samplingRange = samplingMax - samplingMin;
                    }
                    break;
                case ControlChartType.XBar_R:
                case ControlChartType.XBar_S:
                case ControlChartType.Merger:
                default:
                    //// using System.Linq; 필수선언.
                    var samplingData = spcPara.InputData.Where(w => w.SUBGROUP == spcPara.subGrouopID)
                                              .GroupBy(g => g.SUBGROUP)
                                              .Select(s => new
                                              {
                                                  s.Key,
                                                  Min = s.Min(g => g.NVALUE),
                                                  Max = s.Max(g => g.NVALUE),
                                                  Count = s.Count()
                                              });

                    foreach (var item in samplingData)
                    {
                        samplingMin = item.Min.ToSafeDoubleZero();
                        samplingMax = item.Max.ToSafeDoubleZero();
                        samplingCount = item.Count;
                        samplingRange = samplingMax - samplingMin;
                    }
                    break;
            }


            double[] cpkDatas = null;
            if (samplingMax > 0)
            {
                cpkDatas = new double[samplingCount];//Data 초기화.
            }

            ParPIDataTable hitoData = new ParPIDataTable();
            switch (controlSpec.spcOption.chartType)
            {
                case ControlChartType.I_MR:
                    break;
                case ControlChartType.XBar_R:
                case ControlChartType.XBar_S:
                case ControlChartType.Merger:
                default:
                    break;
            }

            switch (controlSpec.spcOption.chartType)
            {
                case ControlChartType.I_MR:
                    if (cpkDatas != null)
                    {
                        i = 0;
                        var cpkData = spcPara.InputDataSum.Where(item => item.SUBGROUP == controlSpec.sigmaResult.subGroup);

                        foreach (var item in cpkData)
                        {
                            cpkDatas[i] = item.NVALUE;
                            i++;
                        }
                    }
                    break;
                case ControlChartType.XBar_R:
                case ControlChartType.XBar_S:
                case ControlChartType.Merger:
                default:
                    if (cpkDatas != null)
                    {
                        i = 0;
                        var cpkData = spcPara.InputData.Where(item => item.SUBGROUP == controlSpec.sigmaResult.subGroup);

                        foreach (var item in cpkData)
                        {
                            cpkDatas[i] = item.NVALUE;
                            i++;
                        }
                    }
                    break;
            }

            //Console.WriteLine(i);

            //double xMax = SpcLimit.MIN;
            //double xMin = SpcLimit.MAX;
            double yMax = SpcLimit.MIN;

            //sia확인 : Histogram 처리 시작.
            const int maxinterval = 150;
            const int digit = 6;
            SPCValue spcCpkData = new SPCValue();
            spcCpkData.SetValues(cpkDatas);
            spcCpkData.setNct(samplingCount);
            spcCpkData.SetMax(samplingMax);
            spcCpkData.SetMin(samplingMin);
            spcCpkData.SetRange(samplingRange);
            spcCpkData.SetMean(controlSpec.sigmaResult.cpkResult.nNVALUE_AVG);
            //spcCpkData.SetStddev(controlSpec.sigmaResult.cpkResult.nSVALUE_STD);
            spcCpkData.SetStddev(controlSpec.sigmaResult.nSigmaWithin);//1/8
            // U_TODO: Histogram을 그리기 위하여 HistogramNormalDistributionLine을 생성한다.
            SPCHistogram histogram = SPCHistogram.Create(spcCpkData, maxinterval, digit);

            //Histogram 구성 초기값 Check
            if (histogram == null || histogram.GetIntervalWidth() == 0
                || histogram.GetIntervalWidth() == SpcLimit.MIN
                || histogram.GetIntervalWidth() == SpcLimit.MAX)
            {
                this.ControlChartDataMappingClear(1);
                //Histogram 간격 폭 계산 할 수 없습니다.
                this.lbStatusMessage.Text = SpcLibMessage.common.comCpk1026;
                this.lbStatusMessage.Visible = true;
                return;
            }

            //급의 수를 지정한다.
            //
            int barCnt = 0;
            double xValue = 0;
            double yValue = 0;
            DataRow rowSpcData;

            histogram.dtAnalysis.dtBar.Clear();
            for (i = 0; i < histogram.GetIntervalSize(); ++i)
            {
                xValue = histogram.GetCenter(i);
                yValue = histogram.GetFrequency(i);

                if (yValue != 0)
                {
                    SeriesPoint seriesPoint = new SeriesPoint();
                    seriesPoint.NumericalArgument = xValue;
                    double[] val = new double[1];
                    val[0] = yValue;
                    seriesPoint.Values = val;
                    Console.WriteLine(this.ChrCpk.Series[0].Points.Count);
                    this.ChrCpk.Series[0].Points.Add(seriesPoint);

                    rowSpcData = histogram.dtAnalysis.dtBar.NewRow();
                    rowSpcData["TempId"] = barCnt;
                    rowSpcData["GroupLabel"] = "Bar";
                    rowSpcData["nLabeldbl"] = xValue;
                    rowSpcData["nValuedbl"] = yValue;
                    histogram.dtAnalysis.dtBar.Rows.Add(rowSpcData);
                    barCnt++;
                }
            }

            ((BarSeriesView)this.ChrCpk.Series[0].View).BarWidth = histogram.GetIntervalWidth();

            //분포곡선의 값을 지정한다.
            //Console.WriteLine(controlSpec.sigmaResult.cpkResult.nKCOUNT.ToSafeInt64());

            int n = 0;
            int maxCnt = histogram.GetIntervalSize() * 10;
            double stddev_st = controlSpec.sigmaResult.nSigmaWithin;// 군내
            double stddev_lt = controlSpec.sigmaResult.nSigmaTotal; // 전체
            double cx = histogram.GetNormalCenter(stddev_st);
            double sx = histogram.GetNormalLeft(cx, stddev_st);
            double ex = histogram.GetNormalRight(cx, stddev_st);
            double iv = (ex - sx) / maxCnt;
            double x = sx;

            double axisX = 0.0000000001;
            double axisY = 0;
            //this.ChrCpk.Series[1].Points.Clear();

            histogram.dtAnalysis.dtWithin.Clear();
            while (n < maxCnt)
            {
                double y_st = histogram.GetNormaly(x, stddev_st);

                yMax = Math.Max(yMax, y_st);

                //*chart.Data.X[0, n] = x;
                //chart.Series[1].Points[n].XValue = x;
                //*chart.Data.Y[0, n] = y_st;
                //chart.Series[1].Points[n].YValues = y_st;

                ////chart.Series[1].Points.AddXY(x + 0.01, y_st);
                //axisX = x + 0.01;
                //axisX = x + 0.000000001;
                axisX = x;
                axisY = y_st;

                //SeriesPoint[80] aa = new SeriesPoint[80];
                SeriesPoint seriesPoint = new SeriesPoint();
                seriesPoint.NumericalArgument = axisX;
                double[] val = new double[1];
                val[0] = axisY;
                seriesPoint.Values = val;
                this.ChrCpk.Series[1].Points.Add(seriesPoint);

                rowSpcData = histogram.dtAnalysis.dtWithin.NewRow();
                rowSpcData["TempId"] = n;
                rowSpcData["GroupLabel"] = "Within";
                rowSpcData["nLabelDbl"] = axisX.ToSafeDoubleStaMin();
                rowSpcData["nValuedbl"] = axisY.ToSafeDoubleStaMin();
                histogram.dtAnalysis.dtWithin.Rows.Add(rowSpcData);

                //System.Console.WriteLine("X : " + x.ToString() + "  Y : " + y_st.ToString());
                x += iv;
                n++;
            }

            n = 0;
            cx = histogram.GetNormalCenter(stddev_lt);
            sx = histogram.GetNormalLeft(cx, stddev_lt);
            ex = histogram.GetNormalRight(cx, stddev_lt);
            iv = (ex - sx) / maxCnt;
            x = sx;

            histogram.dtAnalysis.dtTotal.Clear();
            while (n < maxCnt)
            {
                double y_lt = histogram.GetNormaly(x, stddev_lt);

                //*chart.Data.X[1, n] = x;
                //*chart.Data.Y[1, n] = y_lt;
                //chart.Series[2].Points[n].SetValueXY(x, y_lt);

                //chart.Series[2].Points.AddXY(x, y_lt);
                //axisX = x + 0.01;
                axisX = x;
                axisY = y_lt;

                //SeriesPoint[80] aa = new SeriesPoint[80];
                SeriesPoint seriesPoint = new SeriesPoint();
                seriesPoint.NumericalArgument = axisX;
                double[] val = new double[1];
                val[0] = axisY;
                seriesPoint.Values = val;
                this.ChrCpk.Series[2].Points.Add(seriesPoint);

                rowSpcData = histogram.dtAnalysis.dtTotal.NewRow();
                rowSpcData["TempId"] = n;
                rowSpcData["GroupLabel"] = "Total";
                rowSpcData["nLabelDbl"] = axisX.ToSafeDoubleStaMin();
                rowSpcData["nValuedbl"] = axisY.ToSafeDoubleStaMin();
                histogram.dtAnalysis.dtTotal.Rows.Add(rowSpcData);

                x += iv;
                n++;
            }


            //SPCValue resultValue = null;
            //ResultHistogram resultData = new ResultHistogram();
            //histogramData.SPCHistogramCreate(spcCpkData, maxinterval, digit, out resultData);

            XYDiagram diagram = (XYDiagram)ChrCpk.Diagram;
            XYDiagramDefaultPane defaultDiagPane = diagram.DefaultPane;
            //XYDiagramPane secondDiagPane = diagram.Panes["SecondPane"];

            //DataRow rowSpcData;
            double nXbarValue = SpcLimit.MAX;
            double nXRValue = SpcLimit.MAX;
            int xAsixValue = 0;


            _IsPaint = false;


            #region WholeRange
            //Chart Whole Range 
            _controlSpec.spcOption.CpkTotalize = histogram.dtAnalysis.GetAnalysisTotalize(histogram.dtAnalysis);
            _controlSpec.nXbar.taUsl = controlSpec.sigmaResult.cpkResult.nTargetUSL;
            _controlSpec.nXbar.taCsl = controlSpec.sigmaResult.cpkResult.nTargetCSL;
            _controlSpec.nXbar.taLsl = controlSpec.sigmaResult.cpkResult.nTargetLSL;
            IsSpecCancel.Clear(ref _isCancelCpk);
            _wholeRangeCpk = SpcFunction.chartWholeRangeCpk(_controlSpec.nXbar, _controlSpec.spcOption, _isCancelCpk);

            //diagram.AxisX.Label.Visible = false;
            //diagram.AxisX.VisualRange.Auto = false;
            //diagram.AxisX.VisualRange.MaxValueSerializable = _wholeRangeCpk.max.ToSafeString();
            //diagram.AxisX.VisualRange.MinValueSerializable = _wholeRangeCpk.min.ToSafeString();
            //diagram.AxisY.AutoScaleBreaks.Enabled = true;
            //diagram.AxisY.AutoScaleBreaks.MaxCount = 2;
            //diagram.AxisX.VisualRange.MaxValue = _wholeRangeCpk.max;
            //diagram.AxisX.VisualRange.MinValue = _wholeRangeCpk.min;
            //diagram.AxisX.WholeRange.AutoSideMargins = false;

            //NumericScaleOptions xAxisOptions = diagram.AxisX.NumericScaleOptions;
            //xAxisOptions.ScaleMode = ScaleMode.Continuous;
            ////double visualRange = _wholeRangeCpk.max - _wholeRangeCpk.min;
            ////double visualRangeValue = Math.Ceiling(visualRange / 20);
            //xAxisOptions.GridOffset = _wholeRangeCpk.min;
            //xAxisOptions.GridSpacing = _wholeRangeCpk.min;
            //xAxisOptions.GridAlignment = NumericGridAlignment.Custom;
            //xAxisOptions.AggregateFunction = AggregateFunction.Average;

            //NumericScaleOptions yAxisOptions = diagram.AxisY.NumericScaleOptions;
            //yAxisOptions.ScaleMode = ScaleMode.Automatic;

            diagram.EnableAxisXScrolling = true;
            diagram.EnableAxisXZooming = true;
            //diagram.EnableAxisYScrolling = true;
            //diagram.EnableAxisYZooming = true;

            diagram.AxisX.VisualRange.Auto = false;
            diagram.AxisX.VisualRange.SetMinMaxValues(_wholeRangeCpk.min, _wholeRangeCpk.max);

            diagram.AxisX.WholeRange.SideMarginsValue = 0;
            diagram.AxisX.WholeRange.Auto = false;
            diagram.AxisX.WholeRange.SetMinMaxValues(_wholeRangeCpk.min, _wholeRangeCpk.max);


            //diagram.AxisX.NumericScaleOptions.AggregateFunction = AggregateFunction.Average;
            //diagram.AxisX.NumericScaleOptions.ScaleMode = ScaleMode.Automatic;
            
            //double visualRange = _wholeRangeCpk.max - _wholeRangeCpk.min;
            //double visualRangeValue = Math.Ceiling(visualRange / 20);
            //diagram.AxisX.NumericScaleOptions.AutomaticMeasureUnitsCalculator = new CustomNumericMeasureUnitCalculator();
            ////diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;

            //const scale = d3.scaleLinear().domain([0, 4955]).range([0, 100]);
            //console.log(scale(2345)); //47.32

            //diagram.AxisX.WholeRange.MaxValue = _wholeRangeCpk.max;
            //diagram.AxisX.WholeRange.MinValue = _wholeRangeCpk.min;
            #endregion

            pnlMain.Dock = DockStyle.Fill;

            #region Option - Text View (Chart)

            SpcCpkDefinition cpkOption = SpcCpkDefinition.Create();
            ControlChartType chartType;
            SigmaType sigmaType;
            string chartTypeName = "";
            string chartTypeDisplay = "";
            string sigmaTypeName = "";
            string cpkOptionMessage = "";

            //Within
            chartType = controlSpec.spcOption.chartType;
            chartTypeDisplay = SpcClass.ConversionChartTypeByString(chartType, out chartTypeName);

            string cpkSigmaValue = "";
            string ppkSigmaValue = "";
            sigmaType = controlSpec.spcOption.sigmaType;
            switch (controlSpec.spcOption.chartType)
            {
                case ControlChartType.XBar_R:
                    switch (sigmaType)
                    {
                        case SigmaType.Yes:
                            cpkSigmaValue = Math.Round(controlSpec.sigmaResult.cpkResult.nSVALUE_RTDC4, cpkOption.SigmadecimalPlace).ToSafeString();
                            ppkSigmaValue = Math.Round(controlSpec.sigmaResult.cpkResult.nPVALUE_STDC4, cpkOption.SigmadecimalPlace).ToSafeString();
                            sigmaTypeName = SpcLibMessage.common.comCpk1035;//Sigma 사용
                            break;
                        case SigmaType.No:
                            //cpkSigmaValue = Math.Round(controlSpec.sigmaResult.cpkResult.nSVALUE_RTD, cpkOption.SigmadecimalPlace).ToSafeString();
                            cpkSigmaValue = Math.Round(controlSpec.sigmaResult.cpkResult.nSVALUE_RTDC4, cpkOption.SigmadecimalPlace).ToSafeString();//1/8 MiniTab에서는 무조건 추정치를 사용함.
                            ppkSigmaValue = Math.Round(controlSpec.sigmaResult.cpkResult.nPVALUE_STD, cpkOption.SigmadecimalPlace).ToSafeString();
                            sigmaTypeName = SpcLibMessage.common.comCpk1036;//Sigma 미사용
                            break;
                        default:
                            sigmaTypeName = SpcLibMessage.common.comCpk1037;//추정치 사용여부 값이 없습니다
                            break;
                    }
                    break;
                case ControlChartType.XBar_S:
                    switch (sigmaType)
                    {
                        case SigmaType.Yes:
                            cpkSigmaValue = Math.Round(controlSpec.sigmaResult.cpkResult.nSVALUE_STDC4, cpkOption.SigmadecimalPlace).ToSafeString();
                            ppkSigmaValue = Math.Round(controlSpec.sigmaResult.cpkResult.nPVALUE_STDC4, cpkOption.SigmadecimalPlace).ToSafeString();
                            sigmaTypeName = SpcLibMessage.common.comCpk1035;//Sigma 사용
                            break;
                        case SigmaType.No:
                            cpkSigmaValue = Math.Round(controlSpec.sigmaResult.cpkResult.nSVALUE_STD, cpkOption.SigmadecimalPlace).ToSafeString();
                            ppkSigmaValue = Math.Round(controlSpec.sigmaResult.cpkResult.nPVALUE_STD, cpkOption.SigmadecimalPlace).ToSafeString();
                            sigmaTypeName = SpcLibMessage.common.comCpk1036;//Sigma 미사용
                            break;
                        default:
                            sigmaTypeName = SpcLibMessage.common.comCpk1037;//추정치 사용여부 값이 없습니다
                            break;
                    }
                    break;
                case ControlChartType.Merger:
                    switch (sigmaType)
                    {
                        case SigmaType.Yes:
                            cpkSigmaValue = Math.Round(controlSpec.sigmaResult.cpkResult.nSVALUE_PTDC4, cpkOption.SigmadecimalPlace).ToSafeString();
                            ppkSigmaValue = Math.Round(controlSpec.sigmaResult.cpkResult.nPVALUE_STDC4, cpkOption.SigmadecimalPlace).ToSafeString();
                            sigmaTypeName = SpcLibMessage.common.comCpk1035;//Sigma 사용
                            break;
                        case SigmaType.No:
                            cpkSigmaValue = Math.Round(controlSpec.sigmaResult.cpkResult.nSVALUE_PTD, cpkOption.SigmadecimalPlace).ToSafeString();
                            ppkSigmaValue = Math.Round(controlSpec.sigmaResult.cpkResult.nPVALUE_STD, cpkOption.SigmadecimalPlace).ToSafeString();
                            sigmaTypeName = SpcLibMessage.common.comCpk1036;//Sigma 미사용
                            break;
                        default:
                            sigmaTypeName = SpcLibMessage.common.comCpk1037;//추정치 사용여부 값이 없습니다
                            break;
                    }
                    break;
                case ControlChartType.I_MR:
                case ControlChartType.np:
                case ControlChartType.p:
                case ControlChartType.c:
                case ControlChartType.u:
                    switch (sigmaType)
                    {
                        case SigmaType.Yes:
                            cpkSigmaValue = Math.Round(controlSpec.sigmaResult.cpkResult.nSVALUE_STDC4, cpkOption.SigmadecimalPlace).ToSafeString();
                            ppkSigmaValue = Math.Round(controlSpec.sigmaResult.cpkResult.nPVALUE_STDC4, cpkOption.SigmadecimalPlace).ToSafeString();
                            sigmaTypeName = SpcLibMessage.common.comCpk1035;//Sigma 사용
                            break;
                        case SigmaType.No:
                            cpkSigmaValue = Math.Round(controlSpec.sigmaResult.cpkResult.nSVALUE_STD, cpkOption.SigmadecimalPlace).ToSafeString();
                            ppkSigmaValue = Math.Round(controlSpec.sigmaResult.cpkResult.nPVALUE_STD, cpkOption.SigmadecimalPlace).ToSafeString();
                            sigmaTypeName = SpcLibMessage.common.comCpk1036;//Sigma 미사용
                            break;
                        default:
                            sigmaTypeName = SpcLibMessage.common.comCpk1037;//추정치 사용여부 값이 없습니다
                            break;
                    }
                    break;
            }



            cpkOptionMessage = string.Format("{0}, {1}", chartTypeDisplay, sigmaTypeName);

            if (cpkOptionMessage != "")
            {
                this.lblCpkOptionMessage.ForeColor = Color.Black;
                this.lblCpkOptionMessage.Text = cpkOptionMessage;
            }
            else
            {
                this.lblCpkOptionMessage.Text = "";
            }

            //*Sampling Count가 같은지 확인.
            if (controlSpec.sigmaResult.cpkResult.ISSAME)
            {
                this.lblSpace02.Text = SpcLibMessage.common.comCpk1038;//같지 않음.
            }
            else
            {
                this.lblSpace02.Text = "";//같음.
            }


            //*공정능력 값 표시.
            string textView = "";
            //DataSize(n)

            textView = controlSpec.sigmaResult.cpkResult.nKCOUNT.ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblDataSize.ForeColor = Color.Black;
                this.lblDataSizeValue.Text = textView;
                this.lblDataSizeValue.ForeColor = Color.Black;
            }
            else
            {
                this.lblDataSize.ForeColor = Color.Gray;
                this.lblDataSizeValue.Text = SpcLimit.NONEDATA;
                this.lblDataSizeValue.ForeColor = Color.Gray;
            }

            //Mean
            textView = Math.Round(controlSpec.sigmaResult.cpkResult.nNVALUE_AVG, cpkOption.decimalPlace).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.chkMean.ForeColor = Color.Green;
                this.chkMean.Checked = true;
                this.chkMean.Enabled = true;
                this.lblMeanValue.Text = textView;
                this.lblMeanValue.ForeColor = Color.Black;
                //XYDiagram diagram = (XYDiagram)ChrCpk.Diagram;
                diagram.AxisX.ConstantLines["ConstantLine02CSL"].AxisValue = controlSpec.sigmaResult.cpkResult.nNVALUE_AVG;
                diagram.AxisX.ConstantLines["ConstantLine02CSL"].Visible = true;
            }
            else
            {
                this.chkMean.ForeColor = Color.Gray;
                this.chkMean.Checked = false;
                this.lblMeanValue.Text = SpcLimit.NONEDATA;
                this.lblMeanValue.ForeColor = Color.Gray;
                diagram.AxisX.ConstantLines["ConstantLine02CSL"].AxisValue = 0;
                diagram.AxisX.ConstantLines["ConstantLine02CSL"].Visible = false;
            }

            //USL
            textView = Math.Round(controlSpec.sigmaResult.cpkResult.nUSL, cpkOption.decimalPlace).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.chkUsl.ForeColor = Color.Red;
                this.chkUsl.Checked = true;
                this.chkUsl.Enabled = true;
                this.lblUslValue.Text = textView;
                this.lblUslValue.ForeColor = Color.Black;
                diagram.AxisX.ConstantLines["ConstantLine01USL"].AxisValue = controlSpec.sigmaResult.cpkResult.nUSL;
                diagram.AxisX.ConstantLines["ConstantLine01USL"].Visible = true;
            }
            else
            {
                this.chkUsl.ForeColor = Color.Gray;
                this.chkUsl.Checked = false;
                this.lblUslValue.Text = SpcLimit.NONEDATA;
                this.lblUslValue.ForeColor = Color.Gray;
                diagram.AxisX.ConstantLines["ConstantLine01USL"].AxisValue = 0;
                diagram.AxisX.ConstantLines["ConstantLine01USL"].Visible = false;
            }

            //CSL
            textView = Math.Round(controlSpec.sigmaResult.cpkResult.nTargetCSL, cpkOption.decimalPlace).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.chkCsl.ForeColor = Color.Purple;
                this.chkCsl.Checked = true;
                this.chkCsl.Enabled = true;
                this.chkCsl.Text = "Target";
                this.lblCslValue.Text = textView;
                this.lblCslValue.ForeColor = Color.Black;
                diagram.AxisX.ConstantLines["ConstantLineTACSL"].AxisValue = controlSpec.sigmaResult.cpkResult.nTargetCSL;
                diagram.AxisX.ConstantLines["ConstantLineTACSL"].Visible = true;
            }
            else
            {
                this.chkCsl.ForeColor = Color.Gray;
                this.chkCsl.Checked = false;
                this.chkCsl.Text = "Target";
                this.lblCslValue.Text = SpcLimit.NONEDATA;
                this.lblCslValue.ForeColor = Color.Gray;
                diagram.AxisX.ConstantLines["ConstantLineTACSL"].AxisValue = 0;
                diagram.AxisX.ConstantLines["ConstantLineTACSL"].Visible = false;
            }

            //LSL
            textView = Math.Round(controlSpec.sigmaResult.cpkResult.nLSL, cpkOption.decimalPlace).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.chkLsl.ForeColor = Color.Red;
                this.chkLsl.Checked = true;
                this.chkLsl.Enabled = true;
                this.lblLslValue.Text = textView;
                this.lblLslValue.ForeColor = Color.Black;
                diagram.AxisX.ConstantLines["ConstantLine03LSL"].AxisValue = controlSpec.sigmaResult.cpkResult.nLSL;
                diagram.AxisX.ConstantLines["ConstantLine03LSL"].Visible = true;
            }
            else
            {
                this.chkLsl.ForeColor = Color.Gray;
                this.chkLsl.Checked = false;
                this.lblLslValue.Text = SpcLimit.NONEDATA;
                this.lblLslValue.ForeColor = Color.Gray;
                diagram.AxisX.ConstantLines["ConstantLine03LSL"].AxisValue = 0;
                diagram.AxisX.ConstantLines["ConstantLine03LSL"].Visible = false;
            }

            //중심치 이탈
            //textView = Math.Round(controlSpec.sigmaResult.cpkResult.nTargetUSL, cpkOption.decimalPlace).ToSafeString();
            textView = SpcLimit.MAXTXT;
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.chkUtl.ForeColor = Color.Purple;
                this.chkUtl.Checked = true;
                this.chkUtl.Enabled = true;
                this.lblUtlValue.Text = textView;
                this.lblUtlValue.ForeColor = Color.Black;
                diagram.AxisX.ConstantLines["ConstantLineTAUSL"].AxisValue = controlSpec.sigmaResult.cpkResult.nTargetUSL;
                diagram.AxisX.ConstantLines["ConstantLineTAUSL"].Visible = true;
            }
            else
            {
                this.chkUtl.ForeColor = Color.Gray;
                this.chkUtl.Checked = false;
                this.lblUtlValue.Text = SpcLimit.NONEDATA;
                this.lblUtlValue.ForeColor = Color.Gray;
                diagram.AxisX.ConstantLines["ConstantLineTAUSL"].AxisValue = 0;
                diagram.AxisX.ConstantLines["ConstantLineTAUSL"].Visible = false;

                this.layoutItemUTL.Visibility = LayoutVisibility.Never;
                this.layoutItemUTLValue.Visibility = LayoutVisibility.Never;
            }

            //Target LSL
            textView = Math.Round(controlSpec.sigmaResult.cpkResult.nTargetLSL, cpkOption.decimalPlace).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblPtl.ForeColor = Color.Purple;
                this.lblPtl.Enabled = true;
                this.lblPtlValue.Text = textView;
                this.lblPtlValue.ForeColor = Color.Black;
            }
            else
            {
                this.lblPtl.ForeColor = Color.Gray;
                this.lblPtlValue.Text = SpcLimit.NONEDATA;
                this.lblPtlValue.ForeColor = Color.Gray;
            }

            //Within
            if (cpkSigmaValue != SpcLimit.MAXTXT && cpkSigmaValue != SpcLimit.MINTXT)
            {
                this.lblWithin.ForeColor = Color.Red;
                this.lblWithinValue.Text = cpkSigmaValue;
                this.lblWithinValue.ForeColor = Color.Black;
            }
            else
            {
                this.lblWithin.ForeColor = Color.Gray;
                this.lblWithinValue.Text = SpcLimit.NONEDATA;
                this.lblWithinValue.ForeColor = Color.Gray;
            }

            //Total
            if (ppkSigmaValue != SpcLimit.MAXTXT && ppkSigmaValue != SpcLimit.MINTXT)
            {
                this.lblTotal.ForeColor = Color.Black;
                this.lblTotalValue.Text = ppkSigmaValue;
                this.lblTotalValue.ForeColor = Color.Black;
            }
            else
            {
                this.lblTotal.ForeColor = Color.Gray;
                this.lblTotalValue.Text = SpcLimit.NONEDATA;
                this.lblTotalValue.ForeColor = Color.Gray;
            }

            //Cp
            textView = Math.Round(controlSpec.sigmaResult.cpkResult.nCP, cpkOption.decimalPlace).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblCp.ForeColor = Color.Black;
                this.lblCpValue.Text = textView;
                this.lblCpValue.ForeColor = Color.Black;
            }
            else
            {
                this.lblCpValue.Text = SpcLimit.NONEDATA;
                this.lblCp.ForeColor = Color.Gray;
                this.lblCpValue.Text = SpcLimit.NONEDATA;
                this.lblCpValue.ForeColor = Color.Gray;
            }

            //Cpl
            textView = Math.Round(controlSpec.sigmaResult.cpkResult.nCPL, cpkOption.decimalPlace).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblCpl.ForeColor = Color.Black;
                this.lblCplValue.Text = textView;
                this.lblCplValue.ForeColor = Color.Black;

            }
            else
            {
                this.lblCpl.ForeColor = Color.Gray;
                this.lblCplValue.Text = SpcLimit.NONEDATA;
                this.lblCplValue.ForeColor = Color.Gray;
            }

            //Cpu
            textView = Math.Round(controlSpec.sigmaResult.cpkResult.nCPU, cpkOption.decimalPlace).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblCpu.ForeColor = Color.Black;
                this.lblCpuValue.Text = textView;
                this.lblCpuValue.ForeColor = Color.Black;

            }
            else
            {
                this.lblCpu.ForeColor = Color.Gray;
                this.lblCpuValue.Text = SpcLimit.NONEDATA;
                this.lblCpuValue.ForeColor = Color.Gray;
            }

            //Cpk
            textView = Math.Round(controlSpec.sigmaResult.cpkResult.nCPK, cpkOption.decimalPlace).ToSafeString();
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

            //Cpm
            textView = Math.Round(controlSpec.sigmaResult.cpkResult.nCPM, cpkOption.decimalPlace).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblCpm.ForeColor = Color.Black;
                this.lblCpmValue.Text = textView;
                this.lblCpmValue.ForeColor = Color.Black;
            }
            else
            {
                this.lblCpm.ForeColor = Color.Gray;
                this.lblCpmValue.Text = SpcLimit.NONEDATA;
                this.lblCpmValue.ForeColor = Color.Gray;
            }

            //장기공정능력
            //Pp 
            textView = Math.Round(controlSpec.sigmaResult.cpkResult.nPP, cpkOption.decimalPlace).ToSafeString();
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

            //Ppl
            textView = Math.Round(controlSpec.sigmaResult.cpkResult.nPPL, cpkOption.decimalPlace).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblPpl.ForeColor = Color.Black;
                this.lblPplValue.Text = textView;
                this.lblPplValue.ForeColor = Color.Black;
            }
            else
            {
                this.lblPpl.ForeColor = Color.Gray;
                this.lblPplValue.Text = SpcLimit.NONEDATA;
                this.lblPplValue.ForeColor = Color.Gray;
            }

            //Ppu
            textView = Math.Round(controlSpec.sigmaResult.cpkResult.nPPU, cpkOption.decimalPlace).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblPpu.ForeColor = Color.Black;
                this.lblPpuValue.Text = textView;
                this.lblPpuValue.ForeColor = Color.Black;
            }
            else
            {
                this.lblPpu.ForeColor = Color.Gray;
                this.lblPpuValue.Text = SpcLimit.NONEDATA;
                this.lblPpuValue.ForeColor = Color.Gray;
            }

            //Ppk
            textView = Math.Round(controlSpec.sigmaResult.cpkResult.nPPK, cpkOption.decimalPlace).ToSafeString();
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

            //Ppm
            textView = Math.Round(controlSpec.sigmaResult.cpkResult.nPPM, cpkOption.decimalPlace).ToSafeString();
            if (textView != SpcLimit.MAXTXT && textView != SpcLimit.MINTXT)
            {
                this.lblPpm.ForeColor = Color.Black;
                this.lblPpmValue.Text = textView;
                this.lblPpmValue.ForeColor = Color.Black;
            }
            else
            {
                this.lblCpmValue.Text = SpcLimit.NONEDATA;
                this.lblCpm.ForeColor = Color.Gray;
                this.lblCpmValue.Text = SpcLimit.NONEDATA;
                this.lblCpmValue.ForeColor = Color.Gray;
            }

            //CCL
            textView = Math.Round(controlSpec.nXbar.ccl, 3).ToSafeString();
            if (textView != SpcLimit.MINTXT && textView != SpcLimit.MINTXT)
            {
                //this.lblCCLValue.Text = textView;
                //this.lblCCLValue.ForeColor = Color.Black;
                //this.chkCCL.ForeColor = Color.Blue;
                //this.chkCCL.Checked = true;
                //this.chkCCL.Enabled = true;
            }
            else
            {
                //this.lblCCLValue.Text = SpcLimit.noneData;
                //this.lblCCLValue.ForeColor = Color.Gray;
                //this.chkCCL.ForeColor = Color.Gray;
                //this.chkCCL.Checked = false;
                //this.chkCCL.Enabled = false;
            }

            #endregion

            this._IsFirstInitialize = true;
            //diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRLCL"].AxisValue = controlSpec.nR.lcl;
        }


        /// <summary>
        /// Chart Control 초기화
        /// </summary>
        public void ControlChartDataMappingClear(int option = 0)
        {
            this._IsFirstInitialize = false;
            if (option == 0)
            {
                _spcPara = null;
            }

            this._SUBGROUPNAME = "";

            switch (SpcFlag.isTestMode)
            {
                case 1://Test Mode
                    this.pnlTop.Visible = true;
                    this.pnlRightTop.Visible = true;
                    this.btnOptionTest.Visible = true;
                    break;
                default:
                    this.pnlTop.Visible = false;
                    this.pnlRightTop.Visible = false;
                    break;
            }

            if (this.ChrCpk.Series[0].Points.Count > 1)
            {
                this.ChrCpk.Series[0].Points.Clear();
                this.ChrCpk.Series[1].Points.Clear();
                this.ChrCpk.Series[2].Points.Clear();
            }
            SpcParameter xbarData = SpcParameter.Create();
            ControlSpec controlSpec = new ControlSpec();
            xbarData.SpcData = xbarData.CpkClearData(out controlSpec);
            ChrCpk.DataSource = null;
            ChrCpk.DataSource = xbarData.SpcData;

            this.lbStatusMessage.Text = "";
            this.lbStatusMessage.Visible = false;
            //ChrCpk.Series["Series01Bar"].ArgumentDataMember = "LabelInt";
            //ChrCpk.Series["Series01Bar"].ValueDataMembers.AddRange(new string[] { "nValue" });

            //ChrCpk.Series["Series02Within"].ArgumentDataMember = "LabelInt";
            //ChrCpk.Series["Series02Within"].ValueDataMembers.AddRange(new string[] { "nValue" });

            //ChrCpk.Series["Series03Total"].ArgumentDataMember = "LabelInt";
            //ChrCpk.Series["Series03Total"].ValueDataMembers.AddRange(new string[] { "nValue" });

            XYDiagram diagram = (XYDiagram)ChrCpk.Diagram;
            diagram.AxisX.ConstantLines["ConstantLine03LSL"].Visible = false;
            diagram.AxisX.ConstantLines["ConstantLine02CSL"].Visible = false;
            diagram.AxisX.ConstantLines["ConstantLine01USL"].Visible = false;

            diagram.AxisX.ConstantLines["ConstantLineTAUSL"].Visible = false;
            diagram.AxisX.ConstantLines["ConstantLineTACSL"].Visible = false;
            diagram.AxisX.ConstantLines["ConstantLineTALSL"].Visible = false;


            pnlMain.Dock = DockStyle.Fill;
            this.chkMean.Checked = false;
            this.chkLsl.Checked = false;
            this.chkCsl.Checked = false;
            this.chkCsl.Text = "Target";
            this.chkUsl.Checked = false;
            
            this.chkUtl.Text = SpcLibMessage.common.comCpk1020;//중심치 이탈
            this.lblPtl.Text = SpcLibMessage.common.comCpk1021;//중심치 이탈(%)

            this.layoutItemUTL.Visibility = LayoutVisibility.Never;
            this.layoutItemUTLValue.Visibility = LayoutVisibility.Never;

            this.chkUtl.Checked = false;//중심치 이탈 Target secede
            this.chkMean.Enabled = false;
            this.chkLsl.Enabled = false;
            this.chkCsl.Enabled = false;
            this.chkUsl.Enabled = false;

            this.lblPtl.Enabled = false;
            
            this.chkUtl.Enabled = false;

            this.chkMean.ForeColor = Color.Gray;
            this.chkLsl.ForeColor = Color.Gray;
            this.chkCsl.ForeColor = Color.Gray;
            this.chkUsl.ForeColor = Color.Gray;

            //this.chkLtl.ForeColor = Color.Gray;
            this.chkUtl.ForeColor = Color.Gray;

            this.lblSpace01.Text = "";
            this.lblVarianceValue.Text = "";
            this.lblMeanValue.Text = "";
            this.lblDataSizeValue.Text = "";
            this.lblLslValue.Text = "";
            this.lblCslValue.Text = "";
            this.lblUslValue.Text = "";

            this.lblUtlValue.Text = "";
            this.lblPtlValue.Text = "";

            this.lblSpace02.Text = "";
            this.lblCpkOptionMessage.Text = "";
            this.lblWithinValue.Text = "";
            this.lblTotalValue.Text = "";
            this.lblCpValue.Text = "";
            this.lblCplValue.Text = "";
            this.lblCpuValue.Text = "";
            this.lblCpkValue.Text = "";
            this.lblCpmValue.Text = "";

            this.lblSpace03.Text = "";
            this.lblPpValue.Text = "";
            this.lblPplValue.Text = "";
            this.lblPpuValue.Text = "";
            this.lblPpkValue.Text = "";
            this.lblPpmValue.Text = "";

            this.lblSpace04.Text = "";

            //공정능력 값
            this.lblCpuValue.ForeColor = Color.Gray;
            layoutItemSpace01.Visibility = LayoutVisibility.Never;
            layoutItemTemp01.Visibility = LayoutVisibility.Never;
            layoutItemTemp01Value.Visibility = LayoutVisibility.Never;
            layoutItemVariance.Visibility = LayoutVisibility.Never;
            layoutItemVarianceValue.Visibility = LayoutVisibility.Never;
            layoutItemCpm.Visibility = LayoutVisibility.Never;
            layoutItemCpmValue.Visibility = LayoutVisibility.Never;
            layoutItemPpm.Visibility = LayoutVisibility.Never;
            layoutItemPpmValue.Visibility = LayoutVisibility.Never;



            this.ChrCpk.Refresh();

            this._IsFirstInitialize = true;
        }

        /// <summary>
        /// Series의 값 반환.
        /// </summary>
        /// <param name="chart"></param>
        /// <param name="seriesName"></param>
        /// <param name="pointsIndex"></param>
        /// <returns></returns>
        public double GetSeriesValue(SmartChart chart, string seriesName, int pointsIndex)
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

        #endregion

        private void btnOptionTest_Click(object sender, EventArgs e)
        {
            this.Test_WholeRangeSetting();
        }

        
        private void Test_WholeRangeSetting()
        {
            XYDiagram diagram = (XYDiagram)ChrCpk.Diagram;
            XYDiagramDefaultPane defaultDiagPane = diagram.DefaultPane;


            #region WholeRange
            //Chart Whole Range 
            //_controlSpec.spcOption.CpkTotalize = histogram.dtAnalysis.GetAnalysisTotalize(histogram.dtAnalysis);
            //_controlSpec.nXbar.taUsl = controlSpec.sigmaResult.cpkResult.nTargetUSL;
            //_controlSpec.nXbar.taCsl = controlSpec.sigmaResult.cpkResult.nTargetCSL;
            //_controlSpec.nXbar.taLsl = controlSpec.sigmaResult.cpkResult.nTargetLSL;
            IsSpecCancel.Clear(ref _isCancelCpk);
            _wholeRangeCpk = SpcFunction.chartWholeRangeCpk(_controlSpec.nXbar, _controlSpec.spcOption, _isCancelCpk);

            //diagram.AxisX.Label.Visible = false;
            //diagram.AxisX.VisualRange.Auto = false;
            //diagram.AxisX.VisualRange.MaxValueSerializable = _wholeRangeCpk.max.ToSafeString();
            //diagram.AxisX.VisualRange.MinValueSerializable = _wholeRangeCpk.min.ToSafeString();
            //diagram.AxisY.AutoScaleBreaks.Enabled = true;
            //diagram.AxisY.AutoScaleBreaks.MaxCount = 2;
            //diagram.AxisX.VisualRange.MaxValue = _wholeRangeCpk.max;
            //diagram.AxisX.VisualRange.MinValue = _wholeRangeCpk.min;
            //diagram.AxisX.WholeRange.AutoSideMargins = false;

            NumericScaleOptions xAxisOptions = diagram.AxisX.NumericScaleOptions;
            
            //xAxisOptions.ScaleMode = ScaleMode.Continuous;
            ////double visualRange = _wholeRangeCpk.max - _wholeRangeCpk.min;
            ////double visualRangeValue = Math.Ceiling(visualRange / 20);
            //xAxisOptions.GridOffset = _wholeRangeCpk.min;
            //xAxisOptions.GridSpacing = _wholeRangeCpk.min;
            //xAxisOptions.GridAlignment = NumericGridAlignment.Custom;
            //xAxisOptions.AggregateFunction = AggregateFunction.Average;

            //NumericScaleOptions yAxisOptions = diagram.AxisY.NumericScaleOptions;
            //yAxisOptions.ScaleMode = ScaleMode.Automatic;

            diagram.EnableAxisXScrolling = true;
            diagram.EnableAxisXZooming = true;

            //diagram.EnableAxisYScrolling = true;
            //diagram.EnableAxisYZooming = true;

            //sia작업 : Cpk Test_WholeRangeSetting
            xAxisOptions.GridOffset = 0.00001;
            xAxisOptions.GridSpacing = 0.00001;
            _wholeRangeCpk.min = -0.01;
            _wholeRangeCpk.max = 0.01;
            //xAxisOptions.Assign(ChartElement)

            //diagram.AxisX.NumericScaleOptions.MeasureUnit = NumericMeasureUnit.Custom;
            //diagram.AxisX.NumericScaleOptions.GridAlignment = NumericGridAlignment.Custom;
            //diagram.AxisX.NumericScaleOptions.GridSpacing = 0.00001;
            ScaleBreak scae1 = new ScaleBreak();
            //scae1.
            //diagram.AxisX.ScaleBreaks.Add(scaleBreak)
            diagram.AxisX.ScaleBreakOptions.Style = ScaleBreakStyle.Waved;
            //diagram.AxisX.GridLines.Visible = true;
            //diagram.AxisX.Interlaced = true;

            diagram.AxisX.WholeRange.SideMarginsValue = 0;
            diagram.AxisX.WholeRange.Auto = false;
            diagram.AxisX.WholeRange.AutoSideMargins = false;
            diagram.AxisX.WholeRange.SetMinMaxValues(_wholeRangeCpk.min, _wholeRangeCpk.max);

            diagram.AxisX.VisualRange.AutoSideMargins = false;
            diagram.AxisX.VisualRange.Auto = false;
            diagram.AxisX.VisualRange.SetMinMaxValues(_wholeRangeCpk.min, _wholeRangeCpk.max);
            //this.ChrCpk.Refresh();
            //diagram.ZoomIn(defaultDiagPane = 0.0001);


            diagram.DefaultPane.ScrollBarOptions.XAxisScrollBarAlignment = ScrollBarAlignment.Far;
            //diagram.DefaultPane.ScrollBarOptions.YAxisScrollBarAlignment = ScrollBarAlignment.Far;
            diagram.DefaultPane.ScrollBarOptions.XAxisScrollBarVisible = true;
            //diagram.DefaultPane.ScrollBarOptions.YAxisScrollBarVisible = true;
            //diagram.DefaultPane.ScrollBarOptions.BarThickness = 10;
            //diagram.DefaultPane.ScrollBarOptions.BackColor = Color.DarkGray;
            //diagram.DefaultPane.ScrollBarOptions.BarColor = Color.LightGray;
            //diagram.DefaultPane.ScrollBarOptions.BorderColor = Color.Black;

            //diagram.AxisX.NumericScaleOptions.AggregateFunction = AggregateFunction.Average;
            //diagram.AxisX.NumericScaleOptions.ScaleMode = ScaleMode.Automatic;

            //double visualRange = _wholeRangeCpk.max - _wholeRangeCpk.min;
            //double visualRangeValue = Math.Ceiling(visualRange / 20);
            //diagram.AxisX.NumericScaleOptions.AutomaticMeasureUnitsCalculator = new CustomNumericMeasureUnitCalculator();
            ////diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;

            //const scale = d3.scaleLinear().domain([0, 4955]).range([0, 100]);
            //console.log(scale(2345)); //47.32

            //diagram.AxisX.WholeRange.MaxValue = _wholeRangeCpk.max;
            //diagram.AxisX.WholeRange.MinValue = _wholeRangeCpk.min;
            #endregion

        }

        #region Chart Tool Strip Menu Item 이벤트

        /// <summary>
        /// Image 복사
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspMnuItemImageCopy_Click(object sender, EventArgs e)
        {
            SpcClass.ChartImageCopy(this.ChrCpk);
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
            defaultFileName = string.Format("{0}{1}", SpcLibMessage.common.com1012, defaultFileName);
            SpcClass.ChartImageSave(this.ChrCpk, defaultFileName, SpcLibMessage.common.com1007);
        }
        #endregion Chart Tool Strip Menu Item 이벤트
    }

    class CustomNumericMeasureUnitCalculator : INumericMeasureUnitsCalculator
    {
        public double CalculateMeasureUnit(
                IEnumerable<Series> series,
                double axisLength,
                int pixelsPerUnit,
                double visualMin,
                double visualMax,
                double wholeMin,
                double wholeMax)
        {
            double visualRange = visualMax - visualMin;
            return Math.Ceiling(visualRange / 20);
        }
    }



}//end namespace
