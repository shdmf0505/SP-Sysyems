using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;
using Micube.SmartMES.Commons.SPCLibrary;
using static Micube.SmartMES.Commons.SPCLibrary.DataSets.SpcDataSet;

namespace Micube.SmartMES.SPC
{
    public partial class TestSpcBaseChart01 : DevExpress.XtraEditors.XtraForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자

        public TestSpcBaseChart01()
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
            // TODO : 화면에서 사용할 이벤트 추가
        }
        private void TestSpcForm01_Load(object sender, EventArgs e)
        {
            //this.smartTabControl1.SelectedTabPage = this.xtraTabPageFunction;
            this.smartTabControl1.SelectedTabPage = this.xtraTabPageBoxPolt;
            this.ucAnalysis1.TestSubGroupIdSetting();
            this.XBarRExcute();
            this.ucAnalysis1.SpcVtChartShowWaitAreaEventHandler += UcAnalysis1_SpcVtChartShowWaitAreaEventHandler;
        }
        /// <summary>
        /// Chart Wait 실행.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="se"></param>
        private void UcAnalysis1_SpcVtChartShowWaitAreaEventHandler(object sender, EventArgs e, SpcShowWaitAreaOption se)
        {
            if (se.CheckValue)
            {
                //MessageBox.Show("실행중...");
            }
            else
            {
               // MessageBox.Show("실행 완료!");
            }
        }

        //private void smartChart1_Paint_B2019062601(object sender, PaintEventArgs e)
        /// <summary>
        /// Chart Paint 이벤트 - 해석용 관리선 처리.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChrXbarR_Paint(object sender, PaintEventArgs e)
        {
            ControlLinePaint(sender, e);
        }
        private void ControlLinePaint(object sender, PaintEventArgs e)
        {
            XYDiagram diag = (XYDiagram)ChrXbarR.Diagram;
            ControlCoordinates ccmin = diag.DiagramToPoint(diag.AxisX.VisualRange.MinValue.ToString(), (double)diag.AxisY.VisualRange.MinValue);
            ControlCoordinates ccmax = diag.DiagramToPoint(diag.AxisX.VisualRange.MinValue.ToString(), (double)diag.AxisY.VisualRange.MaxValue);

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
            int len = ChrXbarR.Series[0].Points.Count;
            ControlCoordinates ccBase;
            ControlCoordinates ccBefore;
            ControlCoordinates ccObjectAUCL = null;
            ControlCoordinates ccBeforeALCL = null;
            ControlCoordinates ccObjectALCL = null;
            //ccBase = diag.DiagramToPoint("",);
            float xCap = 0.0f;
            float yCap = 20.0f;
            int i = 0;

            for (i = 0; i < len; i++)
            {
                axisXValueBefore = axisXValueAUCL;
                axisXLabel = ChrXbarR.Series["Series01AUCL"].Points[i].Argument.ToString();
                axisXValueAUCL = (double)ChrXbarR.Series["Series01AUCL"].Points[i].Values[0];
                axisXValueAUCL = (double)ChrXbarR.Series["Series02ALCL"].Points[i].Values[0];

                ccBefore = ccObjectAUCL;
                ccObjectAUCL = diag.DiagramToPoint(axisXLabel, axisXValueAUCL);

                ccBeforeALCL = ccObjectALCL;
                ccObjectALCL = diag.DiagramToPoint(axisXLabel, axisXValueALCL);
                if (xCap == 0f)
                {
                    if (ccBefore != null && ccObjectAUCL != null)
                    {
                        xCap = ((ccObjectAUCL.Point.X - ccBefore.Point.X) / 2) + 1.4f;
                        break;
                    }
                }
            }

            bool firstLineCheck = false;
            for (i = 0; i < len; i++)
            {
                axisXValueBefore = axisXValueAUCL;
                axisXLabel = ChrXbarR.Series["Series01AUCL"].Points[i].Argument.ToString();
                axisXValueAUCL = (double)ChrXbarR.Series["Series01AUCL"].Points[i].Values[0];
                axisXValueALCL = (double)ChrXbarR.Series["Series02ALCL"].Points[i].Values[0];
                Console.Write(axisXLabel + " : ");
                Console.WriteLine(axisXValueAUCL);
                ccBefore = ccObjectAUCL;
                ccObjectAUCL = diag.DiagramToPoint(axisXLabel, axisXValueAUCL);

                ccBeforeALCL = ccObjectALCL;
                ccObjectALCL = diag.DiagramToPoint(axisXLabel, axisXValueALCL);

                if (axisXValueAUCL != 0f)
                {
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
                }
            }


            //e.Graphics.DrawLine(uslPen, x1,y1,x2,y2);
        }

        private void ChrXbarR_Paint_20190701(object sender, PaintEventArgs e)
        {
            XYDiagram diag = (XYDiagram)ChrXbarR.Diagram;
            ControlCoordinates ccmin = diag.DiagramToPoint(diag.AxisX.VisualRange.MinValue.ToString(), (double)diag.AxisY.VisualRange.MinValue);
            ControlCoordinates ccmax = diag.DiagramToPoint(diag.AxisX.VisualRange.MinValue.ToString(), (double)diag.AxisY.VisualRange.MaxValue);

            //ControlCoordinates ccmax1 = diag.DiagramToPoint("Lot-02",(double)2.5);

            int height = ccmin.Point.Y - ccmax.Point.Y;
            object op = ChrXbarR.Series[0].Points[0].Values;
            DevExpress.XtraCharts.Diagram uDiagram1 = ChrXbarR.Diagram;
            XYDiagram diagram = ChrXbarR.Diagram as XYDiagram;
            Type chartType = ChrXbarR.Series[0].GetType();

            //diagram.DefaultPane.Weight
            string axisXLabel = "";
            double axisXValue = double.NaN;
            double axisXValueBefore = double.NaN;
            int len = ChrXbarR.Series[0].Points.Count;
            ControlCoordinates ccBase;
            ControlCoordinates ccBefore;
            ControlCoordinates ccObject = null;
            //ccBase = diag.DiagramToPoint("",);
            float xCap = 0.0f;
            float yCap = 20.0f;
            int i = 0;

            for (i = 0; i < len; i++)
            {
                axisXValueBefore = axisXValue;
                axisXLabel = ChrXbarR.Series[0].Points[i].Argument.ToString();
                axisXValue = (double)ChrXbarR.Series[0].Points[i].Values[0];

                ccBefore = ccObject;
                ccObject = diag.DiagramToPoint(axisXLabel, axisXValue);
                if (xCap == 0f)
                {
                    if (ccBefore != null && ccObject != null)
                    {
                        xCap = ((ccObject.Point.X - ccBefore.Point.X) / 2) + 1.4f;
                        break;
                    }
                }
            }

            bool firstLineCheck = false;
            for (i = 0; i < len; i++)
            {
                axisXValueBefore = axisXValue;
                axisXLabel = ChrXbarR.Series[0].Points[i].Argument.ToString();
                axisXValue = (double)ChrXbarR.Series[0].Points[i].Values[0];
                Console.Write(axisXLabel + " : ");
                Console.WriteLine(axisXValue);
                ccBefore = ccObject;
                ccObject = diag.DiagramToPoint(axisXLabel, axisXValue);

                if (axisXValue != 0f)
                {
                    float x1;
                    float x2;
                    x1 = ccObject.Point.X - xCap;
                    x2 = ccObject.Point.X + xCap;
                    //UCL 좌표값 입력.
                    float y1 = ccObject.Point.Y + yCap;
                    float y2 = ccObject.Point.Y + yCap;
                    Pen uslPenH = new Pen(Color.FromArgb(0, 0, 255), 0.1f);
                    uslPenH.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    e.Graphics.DrawLine(uslPenH, x1, y1, x2, y2);

                    if (ccBefore != null && axisXValueBefore != 0f)
                    {
                        float xH1 = x1;
                        float xH2 = x1;
                        //UCL 좌표값 입력.
                        float yH1 = y2;
                        float yH2 = ccBefore.Point.Y + yCap;
                        Pen uslPenV = new Pen(Color.FromArgb(0, 0, 255), 0.1f);
                        uslPenV.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                        if (firstLineCheck != false)
                        {
                            e.Graphics.DrawLine(uslPenV, xH1, yH1, xH2, yH2);
                        }
                        else
                        {
                            firstLineCheck = true;
                        }
                    }
                }
            }


            //e.Graphics.DrawLine(uslPen, x1,y1,x2,y2);
        }
        private void ChrXbarR_Paint_B2019062801(object sender, PaintEventArgs e)
        {
            XYDiagram diag = (XYDiagram)ChrXbarR.Diagram;
            ControlCoordinates ccmin = diag.DiagramToPoint(diag.AxisX.VisualRange.MinValue.ToString(), (double)diag.AxisY.VisualRange.MinValue);
            ControlCoordinates ccmax = diag.DiagramToPoint(diag.AxisX.VisualRange.MinValue.ToString(), (double)diag.AxisY.VisualRange.MaxValue);

            //ControlCoordinates ccmax1 = diag.DiagramToPoint("Lot-02",(double)2.5);

            int height = ccmin.Point.Y - ccmax.Point.Y;
            object op = ChrXbarR.Series[0].Points[0].Values;
            DevExpress.XtraCharts.Diagram uDiagram1 = ChrXbarR.Diagram;
            XYDiagram diagram = ChrXbarR.Diagram as XYDiagram;
            //diagram.AxisY.Title.Position = AxisTitlePosition.Outside;
            //diagram.AxisY.Title.Text = "Total Harmonic Distortion";
            //diagram.AxisY.Title.Alignment = System.Drawing.StringAlignment.Center;
            //diagram.AxisY.Label.Alignment = AxisLabelAlignment.Far;
            //diagram.AxisY.LabelPosition = AxisLabelPosition.Inside;
            //diagram.AxisY.Label.TextPattern = "{V:0.##}%";
            //diagram.AxisY.Alignment = AxisAlignment.Near;
            Type chartType = ChrXbarR.Series[0].GetType();
            //smartChart1.Diagram.
            //float x1 = 0.1f;
            //float x2 = 0.1f;
            //float y1 = 500.1f;
            //float y2 = 500.1f;

            //diagram.DefaultPane.Weight
            string axisXLabel = "";
            double axisXValue = double.NaN;
            double axisXValueBefore = double.NaN;
            int len = ChrXbarR.Series[0].Points.Count;
            ControlCoordinates ccBase;
            ControlCoordinates ccBefore;
            ControlCoordinates ccObject = null;
            //ccBase = diag.DiagramToPoint("",);
            float xCap = 0.0f;
            float yCap = 20.0f;
            int i = 0;

            for (i = 0; i < len; i++)
            {
                axisXValueBefore = axisXValue;
                axisXLabel = ChrXbarR.Series[0].Points[i].Argument.ToString();
                axisXValue = (double)ChrXbarR.Series[0].Points[i].Values[0];

                ccBefore = ccObject;
                ccObject = diag.DiagramToPoint(axisXLabel, axisXValue);
                if (xCap == 0f)
                {
                    if (ccBefore != null && ccObject != null)
                    {
                        xCap = ((ccObject.Point.X - ccBefore.Point.X) / 2) + 1.4f;
                        break;
                    }
                }
            }

            bool firstLineCheck = false;
            for (i = 0; i < len; i++)
            {
                axisXValueBefore = axisXValue;
                axisXLabel = ChrXbarR.Series[0].Points[i].Argument.ToString();
                axisXValue = (double)ChrXbarR.Series[0].Points[i].Values[0];
                Console.Write(axisXLabel + " : ");
                Console.WriteLine(axisXValue);
                ccBefore = ccObject;
                ccObject = diag.DiagramToPoint(axisXLabel, axisXValue);
                //if (xCap == 0f)
                //{
                //    if (ccBefore != null)
                //    {
                //        xCap = ((ccObject.Point.X - ccBefore.Point.X) / 2 ) + 1.4f;
                //    }
                //}

                if (axisXValue != 0f)
                {
                    float x1;
                    float x2;
                    //if (i == 0)
                    //{
                    //    x1 = ccObject.Point.X - (ccObject.Point.X / 2);
                    //    x2 = ccObject.Point.X + (ccObject.Point.X / 2);
                    //}
                    //else
                    //{
                    //    x1 = ccObject.Point.X - xCap;
                    //    x2 = ccObject.Point.X + xCap;
                    //}

                    x1 = ccObject.Point.X - xCap;
                    x2 = ccObject.Point.X + xCap;
                    //UCL 좌표값 입력.
                    float y1 = ccObject.Point.Y + yCap;
                    float y2 = ccObject.Point.Y + yCap;
                    Pen uslPenH = new Pen(Color.FromArgb(0, 0, 255), 0.1f);
                    uslPenH.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    e.Graphics.DrawLine(uslPenH, x1, y1, x2, y2);

                    if (ccBefore != null && axisXValueBefore != 0f)
                    {
                        float xH1 = x1;
                        float xH2 = x1;
                        //UCL 좌표값 입력.
                        float yH1 = y2;
                        float yH2 = ccBefore.Point.Y + yCap;
                        Pen uslPenV = new Pen(Color.FromArgb(0, 0, 255), 0.1f);
                        uslPenV.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                        if (firstLineCheck != false)
                        {
                            e.Graphics.DrawLine(uslPenV, xH1, yH1, xH2, yH2);
                        }
                        else
                        {
                            firstLineCheck = true;
                        }
                    }
                }
            }


            //float x1 = (float)ccmax1.Point.X-30;
            //float x2 = (float)ccmax1.Point.X+30;
            //float y1 = (float)ccmax1.Point.Y+20;
            //float y2 = (float)ccmax1.Point.Y+20;
            //Pen uslPen = new Pen(Color.FromArgb(0, 0, 255), 0.1f);
            //uslPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            //e.Graphics.DrawLine(uslPen, x1,y1,x2,y2);
        }
        /// <summary>
        /// Test Sample
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChrXbarR_Paint_B(object sender, PaintEventArgs e)
        {
            var chartCtrl = (ChartControl)sender;
            if (chartCtrl.Series.Count == 0) return;
            var diagram = chartCtrl.Diagram as DevExpress.XtraCharts.XYDiagram;
            //var center = diagram.DiagramToPoint(arguments[0], 0).Point.X;
            //var right = (center + diagram.DiagramToPoint(arguments[1], 0).Point.X) / 2;
            //int bottom = diagram.DiagramToPoint(arguments[1], 0).Point.Y;

            //while (!chartCtrl.CalcHitInfo(right, bottom).InSeries && right > center)
            //    right--;
            //var width = (right - center) * 2;

            //for (int i = 0; i < arguments.Length; i++)
            //{
            //    var x1 = diagram.DiagramToPoint(arguments[i], 0).Point.X - (width / 2);
            //    var x2 = diagram.DiagramToPoint(arguments[i], 0).Point.X + (width / 2);
            //    var y1 = diagram.DiagramToPoint(arguments[i], 0).Point.Y;
            //    var y2 = diagram.DiagramToPoint(arguments[i], 500).Point.Y;
            //    e.Graphics.DrawLine(Pens.Red, (float)x1, y1, (float)x1, y2);
            //    e.Graphics.DrawLine(Pens.Red, (float)x2, y1, (float)x2, y2);
            //}
        }

        private void BtnXBarRExcute_Click(object sender, EventArgs e)
        {
            double x = "asdf".ToSafeDoubleNaN();
            XBarRExcute();
        }
        #endregion

        #region 컨텐츠 영역 초기화

        //protected override void InitializeContent()
        //{
        //    base.InitializeContent();

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
        //화면에서 사용할 내부 함수 추가
        /// <summary>
        /// XbarR Data 입력.
        /// </summary>
        private void XBarRExcute()
        {
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
        }








        #endregion

        private void smartTabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(this.smartTabControl1.SelectedTabPage.Name);
            Console.WriteLine(this.smartTabControl1.SelectedTabPageIndex);
            Console.WriteLine(this.smartTabControl1.SelectedTabPageIndex);
        }

        private void smartTabControl1_SelectedPageChanging(object sender, DevExpress.XtraTab.TabPageChangingEventArgs e)
        {
            Console.WriteLine(this.smartTabControl1.SelectedTabPage.Name);
            Console.WriteLine(this.smartTabControl1.SelectedTabPageIndex);
            Console.WriteLine(this.smartTabControl1.SelectedTabPageIndex);
        }

        private void smartTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            Console.WriteLine(this.smartTabControl1.SelectedTabPage.Name);
            Console.WriteLine(this.smartTabControl1.SelectedTabPageIndex);
            Console.WriteLine(this.smartTabControl1.SelectedTabPageIndex);
        }

        private void scrolSpecLimit_Click(object sender, EventArgs e)
        {

        }

        private void chkLOL_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void BtnXBarRExcute_Click_1(object sender, EventArgs e)
        {
            SPCPara spcParaEdit = new SPCPara();
            DataTable dtRaw = new DataTable();
            DataTable dtSpec = new DataTable();
            spcParaEdit.ChartTypeMain("P");
            spcParaEdit.tableRawData = dtRaw;
            spcParaEdit.tableSubgroupSpec = dtSpec;
            //this.ucXBarDirect.DirectXBarRExcute(ref spcParaEdit);
            this.ucXBarDirect.DirectXBarRExcuteTest(ref spcParaEdit);
        }

        private void BtnXBarRClear_Click(object sender, EventArgs e)
        {
            this.ucXBarDirect.ControlChartDataMappingClear();
        }

        private void btnBoxPlotExe_Click(object sender, EventArgs e)
        {
            this.ucAnalysis1.TestSubGroupIdSetting();
        }

        private void btnText_Click(object sender, EventArgs e)
        {
            DataTable dtIPDF = CreateDataTableIPDF();
            SPCPdf spcPdf = new SPCPdf();
            double[] nPdfValueArray;

            double nValue = 0;
            double nFew = 0;
            int nCount = 0;
            int nDigit = 0;

            try
            {
                nValue = this.txtValue.Text.ToSafeDoubleZero();
                nFew = this.txtFew.Text.ToSafeDoubleZero();
                nCount = this.txtCount.Text.ToSafeInt32();
                nCount += 1;
                nDigit = this.txtDigit.Text.ToSafeInt32();
            }
            catch (Exception ex)
            {
                MessageBox.Show("입력 값이 잘 못 되었습니다.");
                return;
            }

            Console.WriteLine("//Pdf Start -----------------------------------");
            //for (int i = 0; i < 12; i++)
            //{
            //    nPdfValue = spcPdf.NORMSINV(241, 0.04);
            //    Console.WriteLine(nPdfValue);
            //    //Application.DoEvents();
            //}
            //int nCount = 12;
            //nPdfValueArray = spcPdf.NORMSINVary(241, 0.04, nCount);
            nPdfValueArray = spcPdf.NORMSINVary(nValue, nFew, nCount, nDigit);
            for (int i = 1; i < nCount; i++)
            {
                Console.WriteLine(nPdfValueArray[i]);
                DataRow dataRow = dtIPDF.NewRow();
                dataRow["TEMPID"] = i;
                dataRow["NVALUEDBL"] = nPdfValueArray[i];
                dtIPDF.Rows.Add(dataRow);
            }

            Console.WriteLine("//Pdf End -----------------------------------");

            this.grdViewIPdfData.DataSource = dtIPDF;
            //this.dataGridViewResult.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.grdViewIPdfData.AutoSize = true;
        }


        public DataTable CreateDataTableIPDF(string tableName = "dtAnalysisIpdf")
        {
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            //Filed
            dt.Columns.Add("TEMPID", typeof(long));
            dt.Columns.Add("NVALUEDBL", typeof(double));
            return dt;
        }

        private int gridResultView(DataTable dt, int rowIndex = 0)
        {
            int j = 0;
            ResultViewDataTable dtResultView = new ResultViewDataTable();
            //rtnData
            DataRow dr1 = dt.Rows[rowIndex];
            for (j = 0; j < dt.Columns.Count; j++)
            {

                DataRow dr2 = dtResultView.NewRow();
                dr2["SEQ"] = j;
                dr2["ITEM"] = dt.Columns[j].Caption.ToString();
                dr2["NVALUE"] = dr1[j].ToSafeString();
                dtResultView.Rows.Add(dr2);
            }

            this.grdViewIPdfData.DataSource = dtResultView;
            //this.dataGridViewResult.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.grdViewIPdfData.AutoSize = true;
            return 0;
        }
    }//End class

}//end namespace
