#region using
using Micube.Framework.SmartControls;

using DevExpress.XtraCharts;
using DevExpress.XtraLayout.Utils;

using System;
using System.Drawing;
using System.Windows.Forms;
using Micube.SmartMES.Commons.SPCLibrary;
#endregion

namespace Micube.SmartMES.SPC
{
    public partial class TestSpcBaseChart02 : DevExpress.XtraEditors.XtraForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        bool _IsOverRule = false;
        bool _IsPaint = false;
        int _x = 0;
        int _y = 0;
        #endregion

        #region 생성자

        public TestSpcBaseChart02()
        {
            InitializeComponent();
        }
        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        //protected override void OnToolbarSaveClick()
        //{
        //    base.OnToolbarSaveClick();

        //    // TODO : 저장 Rule 변경
        //    //DataTable changed = grdCodeClass.GetChangedRows();

        //    //ExecuteRule("SaveCodeClass", changed);
        //}

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        //protected async override Task OnSearchAsync()
        //{
        //    await base.OnSearchAsync();

        //    // TODO : 조회 SP 변경
        //    var values = Conditions.GetValues();

        //    DataTable dtCodeClass = await ProcedureAsync("usp_com_selectCodeClass", values);

        //    if (dtCodeClass.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
        //    {
        //        ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
        //    }

        //    //grdCodeClass.DataSource = dtCodeClass;
        //}

        //protected override void InitializeCondition()
        //{
        //    base.InitializeCondition();

        //    // TODO : 조회조건 추가 구성이 필요한 경우 사용
        //}

        //protected override void InitializeConditionControls()
        //{
        //    base.InitializeConditionControls();

        //    // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        //}

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        //protected override void OnValidateContent()
        //{
        //    base.OnValidateContent();

        //    // TODO : 유효성 로직 변경
        //    //grdCodeClass.View.CheckValidation();

        //    //DataTable changed = grdCodeClass.GetChangedRows();

        //    //if (changed.Rows.Count == 0)
        //    //{
        //    //    // 저장할 데이터가 존재하지 않습니다.
        //    //    throw MessageException.Create("NoSaveData");
        //    //}
        //}

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

            // TODO : 화면에서 사용할 이벤트 추가
            chkSigma3.CheckedChanged += new System.EventHandler(ChkSigma3_CheckedChanged);
            chkSigma2.CheckedChanged += new System.EventHandler(chkSigma2_CheckedChanged);
            chkSigma1.CheckedChanged += new System.EventHandler(chkSigma1_CheckedChanged);
            chkUSL.CheckedChanged += new System.EventHandler(chkUSL_CheckedChanged);
            chkCSL.CheckedChanged += new System.EventHandler(ChkCSL_CheckedChanged);
            chkLSL.CheckedChanged += new System.EventHandler(ChkLSL_CheckedChanged);
            chkUCL.CheckedChanged += new System.EventHandler(ChkUCL_CheckedChanged);
            chkCCL.CheckedChanged += new System.EventHandler(ChkCCL_CheckedChanged);
            chkLCL.CheckedChanged += new System.EventHandler(ChkLCL_CheckedChanged);
            chkRUCL.CheckedChanged += new System.EventHandler(ChkRUCL_CheckedChanged);
            chkRCCL.CheckedChanged += new System.EventHandler(ChkRCCL_CheckedChanged);
            chkRLCL.CheckedChanged += new System.EventHandler(ChkRLCL_CheckedChanged);
            chkUOL.CheckedChanged += new System.EventHandler(ChkUOL_CheckedChanged);
            chkLOL.CheckedChanged += new System.EventHandler(ChkLOL_CheckedChanged);
        }
        private void TestSpcForm01_Load(object sender, EventArgs e)
        {
            ControlChartDataMapping();
            InitializeEvent();
            ControlDefaultSetting();
            //this.layoutItemTemp01.HideToCustomization();
            //this.layoutItemTemp01Value.HideToCustomization();
            //ChrXbarR.CrosshairEnabled = DefaultBoolean.False;
            //ChrXbarR.RuntimeHitTesting = true;
        }

        //private void smartChart1_Paint_B2019062601(object sender, PaintEventArgs e)
        /// <summary>
        /// Chart Paint 이벤트 - 사용자 Line 적용.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChrXbarR_Paint(object sender, PaintEventArgs e)
        {
            if (_IsPaint == false)
            {
                ControlLinePaint(sender, e);
            }
            else
            {
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
                    //this.txtPointView.Text = point.Argument.ToString();
                    string pointInfo = string.Format("asixs 라벨: {0},  값: {1}"
                        , point.Argument.ToString()
                        , point.Values[0].ToSafeString());
                    MessageBox.Show(pointInfo);
                }
            }

        }
        /// <summary>
        /// Chart Paint - 해석용 관리선 처리.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlLinePaint(object sender, PaintEventArgs e)
        {
            //sia확인 : 03.Chart Paint

            //if (_IsOverRule == false)
            //{
            //    _IsOverRule = true;
            //    ChrXbarR.Series["Series03Value"].Points[4].Color = Color.Red;
            //}
            XYDiagram diag = (XYDiagram)ChrXbarR.Diagram;
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
            object op = ChrXbarR.Series[0].Points[0].Values;
            DevExpress.XtraCharts.Diagram uDiagram1 = ChrXbarR.Diagram;
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
            //ccBase = diag.DiagramToPoint("",);

            ControlCoordinates ccBeforeSub;
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

                axisXLabel = ChrXbarR.Series["Series01AUCL"].Points[i].Argument.ToString();

                axisXValueAUCL = (double)ChrXbarR.Series["Series01AUCL"].Points[i].Values[0];
                axisXValueALCL = (double)ChrXbarR.Series["Series02ALCL"].Points[i].Values[0];


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

            //XCapSub
            for (i = 0; i < nlenSub; i++)
            {
                //axisXValueSubAUCL = (double)ChrXbarR.Series["Series11AUCL"].Points[i].Values[0];
                //axisXValueSubALCL = (double)ChrXbarR.Series["Series12ALCL"].Points[i].Values[0].ToSafeDoubleNaN();

                axisXLabel = ChrXbarR.Series["Series11AUCL"].Points[i].Argument.ToString();
                axisXValueSubAUCL = GetSeriesValue(ChrXbarR, "Series11AUCL", i);
                axisXValueSubALCL = GetSeriesValue(ChrXbarR, "Series12ALCL", i);

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

                ccBeforeSub = ccObjectSubAUCL;
                //ccObjectSubAUCL = diag.DiagramToPoint(axisXLabel, axisXValueSubAUCL);
                ccObjectSubAUCL = diag.DiagramToPoint(axisXLabel, axisXValueSubAUCL, axis2DX, axis2DY, secondDiagPane);
                ccBeforeSubALCL = ccObjectSubALCL;
                //ccObjectSubALCL = diag.DiagramToPoint(axisXLabel, axisXValueSubALCL);
                ccObjectSubALCL = diag.DiagramToPoint(axisXLabel, axisXValueSubALCL, axis2DX, axis2DY, secondDiagPane);

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
                    e.Graphics.DrawLine(uslPenH, x1, y1, x2, y2);

                    //LCL 좌표값 입력.
                    float yL1 = ccObjectALCL.Point.Y;
                    float yL2 = ccObjectALCL.Point.Y;
                    //Pen uslPenHALCL = new Pen(Color.FromArgb(0, 0, 255), 0.1f);
                    //uslPenH.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    e.Graphics.DrawLine(uslPenH, x1, yL1, x2, yL2);


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
                            e.Graphics.DrawLine(uslPenV, xH1, yH1, xH2, yH2);
                            e.Graphics.DrawLine(uslPenV, xH1, yH1ALCL, xH2, yH2ALCL);
                        }
                        else
                        {
                            firstLineCheck = true;
                        }
                    }
                    #endregion

                    #region Second Pane

                    float x11;
                    float x12;
                    x11 = ccObjectSubAUCL.Point.X - xCapSub;
                    x12 = ccObjectSubAUCL.Point.X + xCapSub;

                    //UCL 좌표값 입력.
                    float y11 = ccObjectSubAUCL.Point.Y;
                    float y12 = ccObjectSubAUCL.Point.Y;
                    Pen uslPenHSub = new Pen(Color.FromArgb(0, 0, 255), 0.1f);
                    uslPenHSub.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    e.Graphics.DrawLine(uslPenHSub, x11, y11, x12, y12);

                    //LCL 좌표값 입력.
                    float yL11 = ccObjectSubALCL.Point.Y;
                    float yL12 = ccObjectSubALCL.Point.Y;
                    //Pen uslPenHALCL = new Pen(Color.FromArgb(0, 0, 255), 0.1f);
                    //uslPenH.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    e.Graphics.DrawLine(uslPenHSub, x11, yL11, x12, yL12);


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
                            e.Graphics.DrawLine(uslPenVSub, xH11, yH11, xH12, yH12);
                            e.Graphics.DrawLine(uslPenVSub, xH11, yH11ALCL, xH12, yH12ALCL);
                        }
                        else
                        {
                            firstLineSubCheck = true;
                        }
                    }
                    #endregion

                }
            }

            //e.Graphics.DrawLine(uslPen, x1,y1,x2,y2);
        }

        /// <summary>SeriesPoint
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChrXbarR_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
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

        private void BtnXBarRExcute_Click(object sender, EventArgs e)
        {
            ControlChartDataMapping();
        }
        //sia확인 : 04.Spec 이벤트
        #region Spec Control limit Option 처리
        /// <summary>
        /// Chart sigma3 표시 유무
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkSigma3_CheckedChanged(object sender, EventArgs e)
        {
            //Console.WriteLine(smartCheckEdit1.Checked);
            //XYDiagram diagram = (XYDiagram)ChrXbarR.Diagram;
            //diagram.AxisY.ConstantLines["ConstantLineUSL"].Visible = smartCheckEdit1.Checked;
        }
        /// <summary>
        /// Chart sigma2 표시 유무
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSigma2_CheckedChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Chart sigma1 표시 유무
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSigma1_CheckedChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Chart USL 표시 유무
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkUSL_CheckedChanged(object sender, EventArgs e)
        {
            XYDiagram diagram = (XYDiagram)ChrXbarR.Diagram;
            diagram.AxisY.ConstantLines["ConstantLineUSL"].Visible = chkUSL.Checked;
            ChrXbarR.Series["Series06OverRuleUSL"].Visible = chkUSL.Checked;
        }
        /// <summary>
        /// Chart CSL 표시 유무
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkCSL_CheckedChanged(object sender, EventArgs e)
        {
            XYDiagram diagram = (XYDiagram)ChrXbarR.Diagram;
            diagram.AxisY.ConstantLines["ConstantLineXBAR"].Visible = chkCSL.Checked;
            //ChrXbarR.Series["Series04OverRuleUSL"].Visible = chkCSL.Checked;
        }
        /// <summary>
        /// Chart LSL 표시 유무
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkLSL_CheckedChanged(object sender, EventArgs e)
        {
            XYDiagram diagram = (XYDiagram)ChrXbarR.Diagram;
            diagram.AxisY.ConstantLines["ConstantLineLSL"].Visible = chkLSL.Checked;
            ChrXbarR.Series["Series07OverRuleLSL"].Visible = chkLSL.Checked;
        }
        /// <summary>
        /// Chart UCL 표시 유무
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkUCL_CheckedChanged(object sender, EventArgs e)
        {
            XYDiagram diagram = (XYDiagram)ChrXbarR.Diagram;
            diagram.AxisY.ConstantLines["ConstantLineUCL"].Visible = chkUCL.Checked;
            ChrXbarR.Series["Series04OverRuleUCL"].Visible = chkUCL.Checked;
        }
        /// <summary>
        /// Chart CCL 표시 유무
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkCCL_CheckedChanged(object sender, EventArgs e)
        {
            XYDiagram diagram = (XYDiagram)ChrXbarR.Diagram;
            diagram.AxisY.ConstantLines["ConstantLineXBAR"].Visible = chkCCL.Checked;
        }
        /// <summary>
        /// Chart LCL 표시 유무
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkLCL_CheckedChanged(object sender, EventArgs e)
        {
            XYDiagram diagram = (XYDiagram)ChrXbarR.Diagram;
            diagram.AxisY.ConstantLines["ConstantLineLCL"].Visible = chkLCL.Checked;
            ChrXbarR.Series["Series05OverRuleLCL"].Visible = chkLCL.Checked;
        }
        /// <summary>
        /// Chart RUCL 표시 유무
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkRUCL_CheckedChanged(object sender, EventArgs e)
        {
            XYDiagram diagram = (XYDiagram)ChrXbarR.Diagram;
            diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRUCL"].Visible = chkRUCL.Checked;
            ChrXbarR.Series["Series14OverRuleRUCL"].Visible = chkRUCL.Checked;

        }
        /// <summary>
        /// Chart RCCL 표시 유무
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkRCCL_CheckedChanged(object sender, EventArgs e)
        {
            XYDiagram diagram = (XYDiagram)ChrXbarR.Diagram;
            diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRCCL"].Visible = chkRCCL.Checked;
        }
        /// <summary>
        /// Chart RLCL 표시 유무
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkRLCL_CheckedChanged(object sender, EventArgs e)
        {
            XYDiagram diagram = (XYDiagram)ChrXbarR.Diagram;
            diagram.SecondaryAxesY["SecondaryAxisY01"].ConstantLines["ConstantLineRLCL"].Visible = chkRLCL.Checked;
            ChrXbarR.Series["Series15OverRuleRLCL"].Visible = chkRLCL.Checked;
        }
        /// <summary>
        /// Chart UOL 표시 유무
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkUOL_CheckedChanged(object sender, EventArgs e)
        {
            XYDiagram diagram = (XYDiagram)ChrXbarR.Diagram;
            diagram.AxisY.ConstantLines["ConstantLineUOL"].Visible = chkUOL.Checked;
            ChrXbarR.Series["Series08OverRuleUOL"].Visible = chkUOL.Checked;
        }
        /// <summary>
        /// Chart LOL 표시 유무
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkLOL_CheckedChanged(object sender, EventArgs e)
        {
            XYDiagram diagram = (XYDiagram)ChrXbarR.Diagram;
            diagram.AxisY.ConstantLines["ConstantLineLOL"].Visible = chkLOL.Checked;
            ChrXbarR.Series["Series09OverRuleLOL"].Visible = chkLOL.Checked;
        }
        #endregion



        #endregion

        #region 컨텐츠 영역 초기화

        //protected override void InitializeContent()
        //{
        //    //base.InitializeContent();

        //    InitializeEvent();

        //    // TODO : 컨트롤 초기화 로직 구성
        //    InitializeGrid();
        //}

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
        }

        #endregion

        #region Private Function
        /// <summary>
        /// Control 기본값 설정
        /// </summary>
        private void ControlDefaultSetting()
        {
            layoutItemTemp01.Visibility = LayoutVisibility.Never;
            layoutItemTemp02.Visibility = LayoutVisibility.Never;
            layoutItemTemp03.Visibility = LayoutVisibility.Never;
            layoutItemTemp01Value.Visibility = LayoutVisibility.Never;
            layoutItemTemp02Value.Visibility = LayoutVisibility.Never;
            layoutItemTemp03Value.Visibility = LayoutVisibility.Never;
            lblSpace01.Text = "";
            lblSpace02.Text = "";
            lblSpace03.Text = "";
            lblSpace04.Text = "";
            lblSpace05.Text = "";
            XYDiagram diagram = (XYDiagram)ChrXbarR.Diagram;
            diagram.AxisY.ConstantLines["ConstantLineUOL"].Visible = false;
            diagram.AxisY.ConstantLines["ConstantLineLOL"].Visible = false;
        }
        //화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 관리도 Chart Data 입력.
        /// </summary>
        private void ControlChartDataMapping()
        {
            //sia확인 : 02.관리도 Chart Data 입력 및 Mapping.
            SpcParameter xbarData = SpcParameter.Create();
            ControlSpec controlSpec = new ControlSpec();

            //Data 입력.
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


        #endregion

        private void smartCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void smartCheckEdit1_CheckedChanged_1(object sender, EventArgs e)
        {
            //Console.WriteLine(smartCheckEdit1.Checked);
            //XYDiagram diagram = (XYDiagram)ChrXbarR.Diagram;
            //diagram.AxisY.ConstantLines["ConstantLineUSL"].Visible = smartCheckEdit1.Checked;
        }

        private void smartSplitTableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void smartLabel1_Click(object sender, EventArgs e)
        {

        }

        private void smartButton1_Click(object sender, EventArgs e)
        {
            this.pnlRight.Visible = false;
        }

        private void smartLabel3_Click(object sender, EventArgs e)
        {

        }

        private void lblTemp01_Click(object sender, EventArgs e)
        {

        }


        private void smartScrollableControl1_Click(object sender, EventArgs e)
        {
            
        }


        private void btnOptionTest_Click(object sender, EventArgs e)
        {

            
            //this.layoutItemTemp01.HideToCustomization();
            if (this.layoutItemTemp01.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                layoutItemTemp01.Visibility = LayoutVisibility.Never;
                layoutItemTemp02.Visibility = LayoutVisibility.Never;
                layoutItemTemp03.Visibility = LayoutVisibility.Never;
                layoutItemTemp01Value.Visibility = LayoutVisibility.Never;
                layoutItemTemp02Value.Visibility = LayoutVisibility.Never;
                layoutItemTemp03Value.Visibility = LayoutVisibility.Never;
                lblSpace01.Text = "";
                lblSpace02.Text = "";
                lblSpace03.Text = "";
            }
            else
            {
                layoutItemTemp01.Visibility = LayoutVisibility.Always;
                layoutItemTemp02.Visibility = LayoutVisibility.Always;
                layoutItemTemp03.Visibility = LayoutVisibility.Always;
                layoutItemTemp01Value.Visibility = LayoutVisibility.Always;
                layoutItemTemp02Value.Visibility = LayoutVisibility.Always;
                layoutItemTemp03Value.Visibility = LayoutVisibility.Always;
            }

            //this.layoutItemTemp01.RestoreFromCustomization(this.layoutGroupChartOption);

        }

        private void ChrXbarR_MouseHover(object sender, EventArgs e)
        {

        }

        private void ChrXbarR_DoubleClick(object sender, EventArgs e)
        {
            //ChartHitInfo hi = ((sender as SmartChart).CalcHitInfo((e as MouseEventArgs).Location));
            //////GridHitInfo hitInfo = ((s as GridView).CalcHitInfo((e as MouseEventArgs).Location));
            ////ChartHitInfo hi =  (SmartChart)sender.CalcHitInfo(e.X, e.Y);
            //SeriesPoint point = hi.SeriesPoint;

            //if (point != null)
            //{
            //    Console.WriteLine(point.Argument.ToSafeString());
            //    //Something something = (Something)point.Tag;
            //}
        }

        //private void ChrXbarR_MouseClick(object sender, MouseEventArgs e)
        //{
        //    _IsPaint = false;
        //    ChartHitInfo hitInfo = ((sender as SmartChart).CalcHitInfo((e as MouseEventArgs).Location));
        //    SeriesPoint sp = hitInfo.SeriesPoint;
        //    _IsPaint = true;
        //}

        private void ChrXbarR_MouseMove_B(object sender, MouseEventArgs e)
        {
            // Obtain hit information under the test point. 
            ChartHitInfo hi = ChrXbarR.CalcHitInfo(e.X, e.Y);

            // Obtain the series point under the test point. 
            SeriesPoint point = hi.SeriesPoint;

            // Check whether the series point was clicked or not. 
            if (point != null)
            {
                // Obtain the series point argument. 
                string argument = "Argument: " + point.Argument.ToString();

                // Obtain series point values. 
                string values = "Value(s): " + point.Values[0].ToString();
                if (point.Values.Length > 1)
                {
                    for (int i = 1; i < point.Values.Length; i++)
                    {
                        values = values + ", " + point.Values[i].ToString();
                    }
                }

                // Show the tooltip. 
                txtPointView.Text = argument + "\n" + values + "SeriesPoint Data";
                //toolTipController1.ShowHint(argument + "\n" + values, "SeriesPoint Data");
            }
            else
            {
                txtPointView.Text = "";
                // Hide the tooltip. 
                //toolTipController1.HideHint();
            }
        }

        private void ChrXbarR_Click(object sender, EventArgs e)
        {
            //sia확인 : ChrXbarR_Click
            ChartHitInfo hitInfo = ((sender as SmartChart).CalcHitInfo((e as MouseEventArgs).Location));
            SeriesPoint sp = hitInfo.SeriesPoint;
            _x = hitInfo.HitPoint.X;
            _y = hitInfo.HitPoint.Y;
            _IsPaint = true;
        }
    }//End class

}//end namespace