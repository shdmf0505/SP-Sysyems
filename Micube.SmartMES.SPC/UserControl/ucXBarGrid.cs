#region using

using Micube.SmartMES.Commons.SPCLibrary;
using static Micube.SmartMES.Commons.SPCLibrary.DataSets.SpcDataSet;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.Framework.SmartControls;

#endregion

namespace Micube.SmartMES.SPC.UserControl
{
    /// <summary>
    /// 프 로 그 램 명  : SPC User Control XBar Chart
    /// 업  무  설  명  : SPC 통계에서 사용되는 XBar-R & S Chart
    /// 생    성    자  : 이승우
    /// 생    성    일  : 2019-07-11
    /// 수  정  이  력  : 
    /// 2019-07-16  Chart Navigator 구현
    /// 2019-07-11  최초작성
    /// 
    /// </summary>
    public partial class ucXBarGrid : DevExpress.XtraEditors.XtraUserControl
    {
        #region Local Variables

        /// <summary>
        /// 통계분석에 필요한 입력 기준정보 저장 Class
        /// </summary>
        public SPCPara spcPara = new SPCPara();
        /// <summary>
        /// 관리도 Spec 정의
        /// </summary>
        public ControlSpec controlSpec = new ControlSpec();
        /// <summary>
        /// 
        /// Display Chart Data
        /// </summary>
        public RtnControlDataTable chartData = new RtnControlDataTable();

        /// <summary>
        /// 관리도 결과 Table
        /// </summary>
        public RtnControlDataTable rtnChartData = new RtnControlDataTable();
        /// <summary>
        /// 공정능력 결과 Table
        /// </summary>
        public RtnPPDataTable rtnCpkData = new RtnPPDataTable();
        /// <summary>
        /// NavigatorRowEnter 사용자 이벤트
        /// </summary>
        /// <param name="p"></param>
        public delegate void SpcNavigatorRowEnter(SpcEventNavigatorRowEnter p);
        /// <summary>
        /// NavigatorRowEnter 사용자 이벤트
        /// </summary>
        public virtual event SpcNavigatorRowEnter spcNavigatorRowEnter;
        /// <summary>
        /// SpcChartEnterEventHandler 이벤트 추가 파라메타
        /// </summary>
        public SpcEventsOption spcEop = new SpcEventsOption();
        /// <summary>
        /// Chart GroupBox Double Click 재정의 이벤트 - delegate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="se"></param>
        public delegate void SpcChartGroupBoxDoubleClickEventHandler(object sender, EventArgs e, SpcEventsOption se);
        /// <summary>
        /// Chart GroupBox Double Click 재정의 이벤트
        /// </summary>
        public virtual event SpcChartGroupBoxDoubleClickEventHandler SpcVtChartGroupBoxDoubleClickEventHandler;
        
        #region Spc Chart ShowWaitArea - Event
        public SpcShowWaitAreaOption spcWaitOption = new SpcShowWaitAreaOption();
        public delegate void SpcChartShowWaitAreaEventHandler(object sender, EventArgs e, SpcShowWaitAreaOption se);
        public virtual event SpcChartShowWaitAreaEventHandler SpcVtChartShowWaitAreaEventHandler;
        #endregion Spc Chart ShowWaitArea - Event


        /// <summary>
        /// Display Chart Raw Data
        /// </summary>
        public RtnControlDataTable staRowData = new RtnControlDataTable();
        /// <summary>
        /// 직접입력 WholeRange 설정.
        /// </summary>
        public ChartWholeRangeDirectValue chartGridWholeRange = ChartWholeRangeDirectValue.Create();


        private SPCTestData _spcTestData = new SPCTestData();

        /// <summary>
        /// SPC 통계용 Form 공통 사용 함수 정의
        /// </summary>
        private SpcClass _spcClass = new SpcClass();

        #endregion

        #region 생성자

        public ucXBarGrid()
        {
            InitializeComponent();
            InitializeContent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 컨텐츠 초기화
        /// </summary>
        private void InitializeContent()
        {
            InitializeEvent();
            InitializeControls();
            InitializeGrid();
        }

        /// <summary>
        /// Controls 초기화
        /// </summary>
        private void InitializeControls()
        {
            grp01.Text = "";
            grp02.Text = "";
            grp03.Text = "";
            grp04.Text = "";
            grp01.Tag = "";
            grp02.Tag = "";
            grp03.Tag = "";
            grp04.Tag = "";
            //chk01.Checked = false;
            //chk02.Checked = false;
            //chk03.Checked = false;
            //chk04.Checked = false;
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
            //Grid Navigator RowEnter 이벤트
            this.gridNavigator.RowEnter += (s, e) =>
            {
                try
                {
                    spcWaitOption.CheckValue = true;
                    SpcVtChartShowWaitAreaEventHandler?.Invoke(s, e, spcWaitOption);

                    //sia확인 : XBarGrid Navigator -RowEnter 이벤트 - Page 처리 부분.
                    ControlChartDisplayParameter parameter = new ControlChartDisplayParameter();
                    parameter.seqStart = gridNavigator.Rows[e.RowIndex].Cells["SEQIDSTART"].Value.ToSafeInt64();
                    parameter.seqEnd = gridNavigator.Rows[e.RowIndex].Cells["SEQIDEND"].Value.ToSafeInt64();

                    this.ucXBar1.ControlChartDataMappingClear();
                    this.ucXBar2.ControlChartDataMappingClear();
                    this.ucXBar3.ControlChartDataMappingClear();
                    this.ucXBar4.ControlChartDataMappingClear();


                    this.ControlChartDisplay(parameter);//Chart 표시

                    SpcEventNavigatorRowEnter parNavigatorRowEnter = new SpcEventNavigatorRowEnter();
                    parNavigatorRowEnter.seqStart = parameter.seqStart;
                    parNavigatorRowEnter.seqEnd = parameter.seqEnd;
                    spcNavigatorRowEnter(parNavigatorRowEnter);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    spcWaitOption.CheckValue = false;
                    SpcVtChartShowWaitAreaEventHandler?.Invoke(s, e, spcWaitOption);
                }


            };

            // nvgData 버튼 클릭 이벤트
            this.nvgData.ButtonClick += (s, e) =>
            {
                //Console.WriteLine(this.nvgData.DataSource.);
                //Console.WriteLine(this.nvgData.Text.ToSafeString());
            };

            chk01.CheckedChanged += new System.EventHandler(this.chk01_CheckedChanged);
            chk02.CheckedChanged += new System.EventHandler(this.chk02_CheckedChanged);
            chk03.CheckedChanged += new System.EventHandler(this.chk03_CheckedChanged);
            chk04.CheckedChanged += new System.EventHandler(this.chk04_CheckedChanged);

            //ucXBar1.SpcChartDoubleClickUserEventHandler += UcXBar1_SpcChartDoubleClickUserEventHandler;
            //ucXBar2.SpcChartDoubleClickUserEventHandler += UcXBar2_SpcChartDoubleClickUserEventHandler;
            //ucXBar3.SpcChartDoubleClickUserEventHandler += UcXBar3_SpcChartDoubleClickUserEventHandler;
            //ucXBar4.SpcChartDoubleClickUserEventHandler += UcXBar4_SpcChartDoubleClickUserEventHandler;
        }

        /// <summary>
        /// ucControl  Load 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucXBarGrid_Load(object sender, EventArgs e)
        {
            SubgroupToolTipClear();
        }

        /// <summary>
        /// Subgroup ToolTipClear
        /// </summary>
        private void SubgroupToolTipClear()
        {
            this.lblSubGroup01ToolTip.Text = "";
            this.lblSubGroup01ToolTip.ToolTip = "";
            this.lblSubGroup02ToolTip.Text = "";
            this.lblSubGroup02ToolTip.ToolTip = "";
            this.lblSubGroup03ToolTip.Text = "";
            this.lblSubGroup03ToolTip.ToolTip = "";
            this.lblSubGroup04ToolTip.Text = "";
            this.lblSubGroup04ToolTip.ToolTip = "";
        }

        private void UcXBar1_SpcChartDoubleClickUserEventHandler(object sender, EventArgs e, SpcEventsOption se)
        {
            try
            {
                ShowSpcStatusDetailChartPopup(se, 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private void UcXBar2_SpcChartDoubleClickUserEventHandler(object sender, EventArgs e, SpcEventsOption se)
        {
            try
            {
                ShowSpcStatusDetailChartPopup(se, 2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void UcXBar3_SpcChartDoubleClickUserEventHandler(object sender, EventArgs e, SpcEventsOption se)
        {
            ShowSpcStatusDetailChartPopup(se, 3);
        }

        private void UcXBar4_SpcChartDoubleClickUserEventHandler(object sender, EventArgs e, SpcEventsOption se)
        {
            ShowSpcStatusDetailChartPopup(se, 4);
        }
        private void ShowSpcStatusDetailChartPopup(SpcEventsOption se, int chartIndex)
        {
            switch (controlSpec.spcOption.chartType)
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
                    //IsDetailPopupLock = true;
                    break;
            }

            bool IsDetailPopupLock = false;
            if (!IsDetailPopupLock)
            {
                if (controlSpec != null && controlSpec != null && spcPara != null)
                {
                    if (spcPara.InputData != null)
                    {
                        //TestSpcBaseChart0301Raw frm = new TestSpcBaseChart0301Raw();//Test
                        SpcStatusDetailChartPopup frm = new SpcStatusDetailChartPopup();
                        frm.ucChartDetail1.SpcDetailPopupInitialize(controlSpec);
                        frm.ucChartDetail1.ucCpk1.IsDetailPopupLock = true;
                        frm.ucChartDetail1.ucCpk1.ControlChartDataMapping(chartData, controlSpec, spcPara);
                        frm.ucChartDetail1.ucXBar1.IsDetailPopupLock = true;
                        frm.ucChartDetail1.ucXBar1.ControlChartDataMapping(chartData, controlSpec, spcPara);
                        frm.ShowDialog();
                    }
                }
            }
        }

        /// <summary>
        /// Direct Check 01
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chk01_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk01.Checked != false)
            {
                this.ucXBar1.ChartWholeRangeDirectChange(chartGridWholeRange);
            }
            else
            {
                this.ucXBar1.ChartWholeRangeOrginalChange();
            }
        }
        /// <summary>
        /// Direct Check 02
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chk02_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk02.Checked != false)
            {
                this.ucXBar2.ChartWholeRangeDirectChange(chartGridWholeRange);
            }
            else
            {
                this.ucXBar2.ChartWholeRangeOrginalChange();
            }
        }
        /// <summary>
        /// Direct Check 03
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chk03_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk03.Checked != false)
            {
                this.ucXBar3.ChartWholeRangeDirectChange(chartGridWholeRange);
            }
            else
            {
                this.ucXBar3.ChartWholeRangeOrginalChange();
            }
        }

        /// <summary>
        /// Direct Check 04
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chk04_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk04.Checked != false)
            {
                this.ucXBar4.ChartWholeRangeDirectChange(chartGridWholeRange);
            }
            else
            {
                this.ucXBar4.ChartWholeRangeOrginalChange();
            }
        }

        /// <summary>
        /// GroupBox1 DoubleClick - 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grp01_DoubleClick(object sender, EventArgs e)
        {
            SmartGroupBox grp1 = sender as SmartGroupBox;
            spcEop.nChartNo = 1;
            spcEop.groupId = grp1.Tag.ToSafeString();
            spcEop.groupName = grp1.Text.ToSafeString();

            if (spcEop.groupId != null && spcEop.groupId != "")
            {
                //MessageBox.Show(string.Format("{0}{1}", spcEop.groupId, spcEop.groupName));
                PopupRawGroupBoxDataEdit(spcEop);
                SpcVtChartGroupBoxDoubleClickEventHandler?.Invoke(sender, e, spcEop);
            }

        }
        /// <summary>
        ///  GroupBox2 DoubleClick - 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grp02_DoubleClick(object sender, EventArgs e)
        {
            SmartGroupBox grp2 = sender as SmartGroupBox;
            spcEop.nChartNo = 3;
            spcEop.groupId = grp2.Tag.ToSafeString();
            spcEop.groupName = grp2.Text.ToSafeString();

            if (spcEop.groupId != null && spcEop.groupId != "")
            {
                PopupRawGroupBoxDataEdit(spcEop);
                SpcVtChartGroupBoxDoubleClickEventHandler?.Invoke(sender, e, spcEop);
            }
        }
        /// <summary>
        ///  GroupBox3 DoubleClick - 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grp03_DoubleClick(object sender, EventArgs e)
        {
            SmartGroupBox grp3 = sender as SmartGroupBox;
            spcEop.nChartNo = 3;
            spcEop.groupId = grp3.Tag.ToSafeString();
            spcEop.groupName = grp3.Text.ToSafeString();

            if (spcEop.groupId != null && spcEop.groupId != "")
            {
                PopupRawGroupBoxDataEdit(spcEop);
                SpcVtChartGroupBoxDoubleClickEventHandler?.Invoke(sender, e, spcEop);
            }
        }
        /// <summary>
        ///  GroupBox4 DoubleClick - 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grp04_DoubleClick(object sender, EventArgs e)
        {
            SmartGroupBox grp4 = sender as SmartGroupBox;
            spcEop.nChartNo = 4;
            spcEop.groupId = grp4.Tag.ToSafeString();
            spcEop.groupName = grp4.Text.ToSafeString();

            if (spcEop.groupId != null && spcEop.groupId != "")
            {
                PopupRawGroupBoxDataEdit(spcEop);
                SpcVtChartGroupBoxDoubleClickEventHandler?.Invoke(sender, e, spcEop);
            }
        }
        #endregion

        #region Private Function

        /// <summary>
        /// Chart Grid 표시.
        /// </summary>
        /// <returns></returns>
        private int ControlChartDisplay(ControlChartDisplayParameter parameter)
        {
            int returnValue = 1;
            int i = 0;
            int j = 0;
            int nChartViewID = 1;
            int nRowMax = 0;
            int nColMax = 0;
            string subGroupBefore = "";
            string subGroupValue = "";
            string subGroupNameBefore = "";
            string subGroupNameValue = "";

            //SubGroup Tilte 초기화 
            this.InitializeControls();

            #region Page Data 구성


            chartData = new RtnControlDataTable();
            staRowData = new RtnControlDataTable();
            var pageChartData = rtnChartData.Where(x => x.TEMPID >= parameter.seqStart && x.TEMPID <= parameter.seqEnd);
            //sia필수확인 : 입력자료 예외처리 필요 - 필수항목자료가 없을시 메세지 표시할것. 8/11
            foreach (var item in pageChartData)
            {
                DataRow dataRow = staRowData.NewRow();
                dataRow["TEMPID"] = item.TEMPID;
                dataRow["GROUPID"] = item.GROUPID;
                dataRow["SUBGROUP"] = item.SUBGROUP;
                dataRow["SAMPLING"] = item.SAMPLING;
                dataRow["SUBGROUPNAME"] = item.SUBGROUPNAME;
                dataRow["SAMPLINGNAME"] = item.SAMPLINGNAME;
                dataRow["MAX"] = item.MAX.ToNullOrDouble();
                dataRow["MIN"] = item.MIN.ToNullOrDouble();
                dataRow["R"] = item.R.ToNullOrDouble();
                //SpcFunction.IsDbNckDoubleWriteDBNull(dataRow, "R", item.R);
                dataRow["RUCL"] = item.RUCL.ToNullOrDouble();
                dataRow["RLCL"] = item.RLCL.ToNullOrDouble();
                dataRow["RCL"] = item.RCL.ToNullOrDouble();
                dataRow["BAR"] = item.BAR.ToNullOrDouble();
                dataRow["UCL"] = item.UCL.ToNullOrDouble();
                dataRow["LCL"] = item.LCL.ToNullOrDouble();
                dataRow["CL"] = item.CL.ToNullOrDouble();
                dataRow["XBAR"] = item.XBAR.ToNullOrDouble();
                dataRow["RBAR"] = item.RBAR.ToNullOrDouble();

                dataRow["SAMESIGMA"] = item.SAMESIGMA.ToNullOrDouble();//1/7 추가
                dataRow["ISSAME"] = item.ISSAME;//1/7추가

                //dataRow["SUMVALUE"] = item.SUMVALUE.ToNullOrDouble();
                //dataRow["TOTSUM"] = item.TOTSUM.ToNullOrDouble();
                //dataRow["TOTAVGVALUE"] = item.TOTAVGVALUE.ToNullOrDouble();
                //dataRow["STDEVALUE"] = item.STDEVALUE.ToNullOrDouble();
                dataRow["NN"] = item.NN.ToNullOrDecimal();
                dataRow["SNN"] = item.SNN.ToNullOrDecimal();
                dataRow["TOTNN"] = item.TOTNN.ToNullOrDecimal();
                dataRow["GROUPNN"] = item.GROUPNN.ToNullOrDecimal();
                staRowData.Rows.Add(dataRow);
            }

            #endregion

            #region Chart Grid에 자료 표시

            if (staRowData != null)
            {
                nRowMax = staRowData.Rows.Count;
                nColMax = staRowData.Columns.Count;
                if (nRowMax > 0)
                {
                    chartData.Rows.Clear();
                    for (i = 0; i < nRowMax + 1; i++)
                    {
                        DataRow dataRow = null;
                        if (subGroupValue != SpcFlag.DataEnd)
                        {
                            if (i < nRowMax)//sia확인 : Log처리 SpcFlag.DataEnd 구분 못함 8/21
                            {
                                dataRow = staRowData.Rows[i];
                                subGroupValue = dataRow["SUBGROUP"].ToSafeString();
                                subGroupNameValue = dataRow["SUBGROUPNAME"].ToSafeString();
                                subGroupNameValue = subGroupNameValue != "" ? subGroupNameValue : subGroupValue;
                            }
                            else
                            {
                                subGroupValue = SpcFlag.DataEnd;
                            }
                        }

                        Console.WriteLine("{0} != {1}", subGroupBefore, subGroupValue);

                        if (subGroupBefore != "" && subGroupBefore != subGroupValue)
                        {
                            //this.dataGridViewPPI.DataSource = rtnData;
                            //sia확인 : 폼-분석자료 Chart에 표시.
                            //*Chart 실행
                            //controlSpec = ControlSpec.Create();
                            //cntrolSpec.nXbar.value= double.NaN;
                            switch (controlSpec.spcOption.limitType)
                            {
                                case LimitType.Interpretation:
                                case LimitType.Management:
                                    controlSpec.nXbar = SPCLibs.SpcLibDBControlSpecSearch(spcPara.InputSpec, subGroupBefore, controlSpec);
                                    controlSpec.nR.ucl = controlSpec.nXbar.rUcl;
                                    controlSpec.nR.lcl = controlSpec.nXbar.rLcl;
                                    break;
                                case LimitType.Direct:
                                    controlSpec.nXbar.uol = spcPara.UOL.ToSafeDoubleStaMax();
                                    controlSpec.nXbar.lol = spcPara.LOL.ToSafeDoubleStaMin();

                                    controlSpec.nXbar.usl = spcPara.USL.ToSafeDoubleStaMax();
                                    controlSpec.nXbar.csl = spcPara.CSL.ToSafeDoubleStaMax();
                                    controlSpec.nXbar.lsl = spcPara.LSL.ToSafeDoubleStaMin();

                                    controlSpec.nXbar.ucl = spcPara.UCL.ToSafeDoubleStaMax();
                                    //controlSpec.nXbar.ccl = 23.67f; //xR
                                    controlSpec.nXbar.lcl = spcPara.LCL.ToSafeDoubleStaMin();

                                    //controlSpec.nR.value = double.NaN;
                                    //controlSpec.nR.uol = double.NaN;
                                    //controlSpec.nR.lol = double.NaN;
                                    //controlSpec.nR.usl = double.NaN;
                                    //controlSpec.nR.csl = double.NaN;
                                    //controlSpec.nR.lsl = double.NaN;
                                    controlSpec.nR.ucl = spcPara.RUCL.ToSafeDoubleStaMax();
                                    //cntrolSpec.nR.ccl = double.NaN;
                                    controlSpec.nR.lcl = spcPara.RLCL.ToSafeDoubleStaMin();
                                    break;
                                default:
                                    break;
                            }

                            int nMaxIndex = chartData.Rows.Count - 1;
                            switch (controlSpec.spcOption.chartType)
                            {
                                case ControlChartType.XBar_R:
                                case ControlChartType.XBar_S:
                                case ControlChartType.Merger:
                                case ControlChartType.I_MR:
                                    //spcPara.rtnXBar.XBarSigma = chartData.Rows[nMaxIndex]["STDEVALUE"].ToSafeDoubleStaMin();
                                    controlSpec.spcOption.isSame = Convert.ToBoolean(chartData.Rows[nMaxIndex]["ISSAME"]);
                                    spcPara.rtnXBar.XBAR = chartData.Rows[nMaxIndex]["XBAR"].ToSafeDoubleStaMin();
                                    spcPara.rtnXBar.XRSP = chartData.Rows[nMaxIndex]["RBAR"].ToSafeDoubleStaMin();
                                    spcPara.rtnXBar.UCL = chartData.Rows[nMaxIndex]["UCL"].ToSafeDoubleStaMin();
                                    spcPara.rtnXBar.LCL = chartData.Rows[nMaxIndex]["LCL"].ToSafeDoubleStaMin();
                                    spcPara.rtnXBar.RUCL = chartData.Rows[nMaxIndex]["RUCL"].ToSafeDoubleStaMin();
                                    spcPara.rtnXBar.RLCL = chartData.Rows[nMaxIndex]["RLCL"].ToSafeDoubleStaMin();
                                    break;
                                case ControlChartType.np:
                                    break;
                                case ControlChartType.p:
                                    spcPara.rtnXBar.XBAR = chartData.Rows[nMaxIndex]["XBAR"].ToSafeDoubleStaMin();
                                    break;
                                case ControlChartType.c:
                                    break;
                                case ControlChartType.u:
                                    break;
                                default:
                                    break;
                            }

                            Console.WriteLine(spcPara.rtnXBar.ToString());
                            Console.WriteLine(spcPara.rtnXBar.ToString());

                            //관리도 Sigma 구성.
                            spcPara.subGrouopID = subGroupBefore.ToSafeString();
                            controlSpec.sigmaResult = _spcClass.GetSigmaData(controlSpec, rtnCpkData, subGroupBefore, spcPara.rtnXBar.XBAR);
                            controlSpec.sigmaResult = _spcClass.GetXBarSigmaData(controlSpec.sigmaResult, controlSpec, spcPara.rtnXBar, subGroupBefore);
                            controlSpec.sigmaResult.subGroupName = string.Format("{0}{1}", SpcLimit.ChartGroupLayoutTitleSpace, subGroupNameBefore.ToSafeString());

                            //*분석자료 Chart에 표시.
                            switch (nChartViewID)
                            {
                                case 1:
                                    this.grp01.Text = string.Format("{0}{1}", SpcLimit.ChartGroupLayoutTitleSpace, subGroupNameBefore.ToSafeString());
                                    this.grp01.Tag = subGroupBefore.ToSafeString();
                                    this.lblSubGroup01ToolTip.ToolTip = subGroupBefore.ToSafeString();
                                    this.ucXBar1.ControlChartDataMapping(chartData, controlSpec, spcPara);
                                    break;
                                case 2:
                                    this.grp02.Text = string.Format("{0}{1}", SpcLimit.ChartGroupLayoutTitleSpace, subGroupNameBefore.ToSafeString());
                                    this.grp02.Tag = subGroupBefore.ToSafeString();
                                    this.lblSubGroup02ToolTip.ToolTip = subGroupBefore.ToSafeString();
                                    this.ucXBar2.ControlChartDataMapping(chartData, controlSpec, spcPara);
                                    break;
                                case 3:
                                    this.grp03.Text = string.Format("{0}{1}", SpcLimit.ChartGroupLayoutTitleSpace, subGroupNameBefore.ToSafeString());
                                    this.grp03.Tag = subGroupBefore.ToSafeString();
                                    this.lblSubGroup03ToolTip.ToolTip = subGroupBefore.ToSafeString();
                                    this.ucXBar3.ControlChartDataMapping(chartData, controlSpec, spcPara);
                                    break;
                                case 4:
                                    this.grp04.Text = string.Format("{0}{1}", SpcLimit.ChartGroupLayoutTitleSpace, subGroupNameBefore.ToSafeString());
                                    this.grp04.Tag = subGroupBefore.ToSafeString();
                                    this.lblSubGroup04ToolTip.ToolTip = subGroupBefore.ToSafeString();
                                    this.ucXBar4.ControlChartDataMapping(chartData, controlSpec, spcPara);
                                    break;
                            }
                            spcPara.subGrouopID = "";
                            subGroupBefore = subGroupValue;
                            subGroupNameBefore = subGroupNameValue.Replace(SpcSym.subgroupSep11, SpcSym.subgroupSep12);

                            if (subGroupValue != SpcFlag.DataEnd)
                            {
                                nChartViewID++;
                                chartData.Rows.Clear();
                                DataRow subRow = chartData.NewRow();
                                for (int r = 0; r < nColMax; r++)
                                {
                                    subRow[r] = dataRow[r];
                                }
                                chartData.Rows.Add(subRow);
                            }
                        }
                        else
                        {
                            subGroupBefore = subGroupValue;
                            subGroupNameBefore = subGroupNameValue.Replace(SpcSym.subgroupSep11, SpcSym.subgroupSep12);
                            DataRow subRow = chartData.NewRow();
                            for (int r = 0; r < nColMax; r++)
                            {
                                try
                                {
                                    subRow[r] = dataRow[r];
                                }
                                catch (Exception ex)
                                {
                                    //sia확인 : 오류처리. 8/21
                                    Console.WriteLine(ex.Message);
                                }
                                
                            }
                            chartData.Rows.Add(subRow);

                            if ((i + 1) == nRowMax)
                            {
                                subGroupValue = SpcFlag.DataEnd;
                            }
                        }
                    }
                }
            }

            #endregion

            return returnValue;
        }
        
        /// <summary>
        /// 
        /// </summary>
        private void ChartRawData()
        {
            int i = 0;
            string subGroupName = "";
            string subGroupRow = "";
            subGroupName = this.grp01.Text.ToSafeString();

            int subGroupMax = 0;
            int samplingMax = 0;
            var sumSubGroup = from g in spcPara.InputData
                              where (g.SUBGROUP == subGroupName)
                              group g by new
                              {
                                  g.SUBGROUP,
                                  g.SAMPLING
                              }
                              into g
                              select new
                              {
                                  vGROUPID = g.Max(s => s.GROUPID),
                                  vSAMPLEID = g.Max(s => s.SAMPLEID),
                                  vSUBGROUP = g.Key.SUBGROUP,
                                  vSAMPLING = g.Max(s => s.SAMPLING),
                                  vCOUNT = g.Count()
                              };
            foreach (var item in sumSubGroup)
            {
                subGroupMax++;
                if (item.vCOUNT > samplingMax)
                {
                    samplingMax = item.vCOUNT;
                }
                Console.WriteLine("{0} - {1}", item.vSAMPLING, item.vCOUNT);
            }

            Console.WriteLine("subGroupMax: {0},  samplingMax {1}", subGroupMax, samplingMax);
            Console.WriteLine("subGroupMax: {0},  samplingMax {1}", subGroupMax, samplingMax);

            int rowMax = 0;

            DataTable dataTable = new DataTable();
            string fieldName = "";
            //XBAR
            dataTable.TableName = "ChartRaw";

            for (i = 0; i < subGroupMax; i++)
            {
                fieldName = string.Format("field_{0:D3}", i + 1);
                dataTable.Columns.Add(fieldName, typeof(double));
            }
            for (i = 0; i < samplingMax; i++)
            {
                dataTable.Rows.Add(dataTable.NewRow());
            }

            //xbarData.SpcData.AsEnumerable().ToList<DataRow>().ForEach(r =>
            //{
            //    ;
            //    r["UOL"] = cntrolSpec.nXbar.uol; r["LOL"] = cntrolSpec.nXbar.lol;
            //    r["USL"] = cntrolSpec.nXbar.usl; r["CSL"] = cntrolSpec.nXbar.csl; r["LSL"] = cntrolSpec.nXbar.lsl;
            //    r["UCL"] = cntrolSpec.nXbar.ucl; r["CCL"] = cntrolSpec.nXbar.ccl; r["LCL"] = cntrolSpec.nXbar.lcl;
            //    //r["AUCL"] = nUCL + 15; r["ACCL"] = nCCL; r["ALCL"] = nLCL - 15;
            //});

            int fieldIndex = 0;
            int rowIndex = 0;
            string samplingName = "";
            string samplingRow = "";
            var subGroupData = spcPara.InputData.Where(x => x.SUBGROUP == subGroupName);

            foreach (DataRow item in subGroupData)
            {
                samplingRow = SpcFunction.IsDbNck(item, "SAMPLING");
                if (samplingName != samplingRow)
                {
                    fieldIndex++;
                    samplingName = samplingRow;
                    fieldName = string.Format("field_{0:D3}", fieldIndex);
                    rowIndex = 0;
                }

                DataRow dataRow = dataTable.Rows[rowIndex];
                dataRow[fieldName] = SpcFunction.IsDbNckDoubleMax(item, "NVALUE");
                rowIndex++;
                //dataTable.Rows.Add(dataRow);
            }

            Console.WriteLine(dataTable.Rows.Count);
        }

        /// <summary>
        /// Chart별 GroupBox Raw Data 구성.
        /// </summary>
        /// <param name="point"></param>
        private void PopupRawGroupBoxDataEdit(SpcEventsOption sep)
        {
            string avgValueMessage = SpcLibMessage.common.comCpk1033;//값(Value)
            string asixsLabelMessage = SpcLibMessage.common.comCpk1034;//asixs 라벨:
            //string pointInfo = string.Format("{0} {1},  {2}: {3}", asixsLabelMessage, point.Argument.ToString(), avgValueMessage, point.Values[0].ToSafeString());
            //string pointInfo = string.Format("asixs 라벨: {0},  값: {1}", point.Argument.ToString(), point.Values[0].ToSafeString());
            string subGroupId = string.Empty;
            string subGroupName = string.Empty;
            subGroupId = sep.groupId;
            subGroupName = sep.groupName;

            switch (controlSpec.spcOption.chartType)
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
                    return;
            }

            string samplingName = string.Empty;
            string samplingRow = string.Empty;
            ParPIDataTable ChartRawData = new ParPIDataTable();
            var subGroupData = spcPara.InputData.Where(x => x.SUBGROUP == subGroupId)
                                                .OrderBy(r1 => r1.SAMPLING).ThenByDescending(r2 => r2.NVALUE);
            foreach (DataRow item in subGroupData)
            {
                DataRow newRow = ChartRawData.NewRow();
                newRow["GROUPID"] = SpcFunction.IsDbNckInt64(item, "GROUPID");
                newRow["SAMPLEID"] = SpcFunction.IsDbNckInt64(item, "SAMPLEID");
                newRow["SUBGROUP"] = SpcFunction.IsDbNck(item, "SUBGROUP");
                SpcFunction.IsDbNckStringWrite(newRow, "SUBGROUPNAME", item, "", SpcClass.SubGroupSymbolDb, SpcClass.SubGroupSymbolView);
                //newRow["SUBGROUPNAME"] = SpcFunction.IsDbNck(item, "SUBGROUPNAME");
                newRow["SAMPLING"] = SpcFunction.IsDbNck(item, "SAMPLING");
                newRow["SAMPLINGNAME"] = SpcFunction.IsDbNck(item, "SAMPLINGNAME");
                newRow["NVALUE"] = SpcFunction.IsDbNckDoubleMin(item, "NVALUE");
                newRow["NSUBVALUE"] = SpcFunction.IsDbNckDoubleMin(item, "NSUBVALUE");
                //newRow["USL"] = SpcFunction.IsDbNck(item, "USL");
                //newRow["USL"] = SpcFunction.IsDbNck(item, "LSL");
                ChartRawData.Rows.Add(newRow);
            }

            SpcStatusRawDataAnalysisPopup frm = new SpcStatusRawDataAnalysisPopup
            {
                RawData = ChartRawData,
                subgroupId = subGroupId,
                chartType = controlSpec.spcOption.chartType,
                gridTitle = string.Format("{0}", subGroupName)
            };

            //switch (controlSpec.spcOption.chartType)
            //{
            //    case ControlChartType.XBar_R:
            //    case ControlChartType.XBar_S:
            //    case ControlChartType.I_MR:
            //    case ControlChartType.Merger:
            //        break;
            //    case ControlChartType.np:
            //    case ControlChartType.p:
            //    case ControlChartType.c:
            //    case ControlChartType.u:
            //    default:
            //        frm.GridHeddenPCU();
            //        break;
            //}

            frm.ShowDialog();

        }
        #endregion

        #region Public Function
        /// <summary>
        /// 관리도 분석 실행.
        /// </summary>
        public void XBarRExcute(SPCPara spcParaData)
        {
            controlSpec = ControlSpec.Create();
            SPCLibs spcLib = new SPCLibs();
            SPCOption spcOption = spcParaData.spcOption;//SPC Option 전이
            SPCOutData spcOutData = SPCOutData.Create();

            //입력 측정 자료.
            spcPara = spcParaData;

            //추정치
            spcOption.sigmaType = spcPara.isUnbiasing;

            //차트 타입 //통계분석
            spcOption.chartName.xBarChartType = spcPara.ChartTypeMain();
            spcOption.chartName.xCpkChartType = spcPara.ChartTypeMain();
            switch (spcOption.chartName.xBarChartType)
            {
                case "XBARR":
                case "R":
                    spcOption.chartType = ControlChartType.XBar_R;
                    rtnChartData = SPCLibs.SpcLibXBarR(spcPara, spcOption, ref spcOutData);
                    break;
                case "XBARS":
                case "S":
                    spcOption.chartType = ControlChartType.XBar_S;
                    rtnChartData = SPCLibs.SpcLibXBarS(spcPara, spcOption, ref spcOutData);
                    break;
                case "IMR":
                case "MR":
                case "I":
                    spcOption.chartType = ControlChartType.I_MR;
                    rtnChartData = SPCLibs.SpcLibIMR(spcPara, spcOption, ref spcOutData);
                    break;
                case "XBARP":
                case "PL":
                    spcOption.chartType = ControlChartType.Merger;
                    rtnChartData = SPCLibs.SpcLibXBarP(spcPara, spcOption, ref spcOutData);
                    break;
                case "P":
                    spcOption.chartType = ControlChartType.p;
                    rtnChartData = SPCLibs.SpcLibP(spcPara, spcOption, ref spcOutData);
                    break;
                case "NP":
                    spcOption.chartType = ControlChartType.np;
                    rtnChartData = SPCLibs.SpcLibNP(spcPara, spcOption, ref spcOutData);
                    break;
                case "C":
                    spcOption.chartType = ControlChartType.c;
                    rtnChartData = SPCLibs.SpcLibC(spcPara, spcOption, ref spcOutData);
                    break;
                case "U":
                    spcOption.chartType = ControlChartType.u;
                    rtnChartData = SPCLibs.SpcLibU(spcPara, spcOption, ref spcOutData);
                    break;
                default:
                    break;
            }

            controlSpec.spcOption = spcOption;


            //sia확인 : CP 실행 - UC- Grid ucXBarGrid 실행. (통계분석)
            rtnCpkData = SPCLibs.SpcLibPpkCbMuti(spcPara, spcOption, ref spcOutData);
            spcPara.cpkOutData = spcOutData;

            Console.WriteLine(rtnCpkData.Rows.Count);

            if (rtnChartData != null)
            {
                if (spcOutData.spcDataTable.tableNavigator != null
                    && spcOutData.spcDataTable.tableNavigator.Rows.Count > 0)
                {
                    this.nvgData.DataSource = spcOutData.spcDataTable.tableNavigator;
                    this.gridNavigator.DataSource = spcOutData.spcDataTable.tableNavigator;
                }
            }

        }

        /// <summary>
        /// Clear XBar Navigator
        /// </summary>
        public void ClearNavigator()
        {
            try
            {
                this.nvgData.DataSource = null;
                this.gridNavigator.DataSource = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 관리도 분석 실행 - Test.
        /// </summary>
        public void XBarRExcuteTest(SPCPara spcParaData)
        {
            string firstSubgroup = "";
            controlSpec = ControlSpec.Create();
            SPCOption spcOption = spcParaData.spcOption;//SPC Option 전이
            SPCOutData spcOutData = SPCOutData.Create();

            spcPara = spcParaData;
            //spcPara.InputData = new ParPIDataTable();//입력 측정 자료.
            //GROUPID" />
            //SAMPLEID" />
            //SUBGROUP" />
            //SAMPLING" />
            //NVALUE" />
            int i = 0;
            int j = 0;

            //sia확인 : test - UC-Frame XBarChart 실행.
            string chartType = spcPara.ChartTypeMain();
            spcOption = spcPara.spcOption;
            //string chartType = "IMR".ToUpper();
            spcPara.ChartTypeMain(chartType);

            //Test Part.......................................................
            if (spcPara.InputData == null && spcPara.InputData.Count <= 0)
            {
                this.XBarChartTestData(ref spcPara);
            }
            //Test Part.......................................................

            //차트 타입 //통계분석
            chartType = spcPara.ChartTypeMain();
            switch (spcOption.chartName.xBarChartType)
            {
                case "XBARR":
                case "R":
                    spcOption.chartType = ControlChartType.XBar_R;
                    rtnChartData = SPCLibs.SpcLibXBarR(spcPara, spcOption, ref spcOutData);
                    break;
                case "XBARS":
                case "S":
                    spcOption.chartType = ControlChartType.XBar_S;
                    rtnChartData = SPCLibs.SpcLibXBarS(spcPara, spcOption, ref spcOutData);
                    break;
                case "IMR":
                case "MR":
                case "I":
                    spcOption.chartType = ControlChartType.I_MR;
                    rtnChartData = SPCLibs.SpcLibIMR(spcPara, spcOption, ref spcOutData);
                    break;
                case "XBARP":
                case "PL":
                    spcOption.chartType = ControlChartType.Merger;
                    rtnChartData = SPCLibs.SpcLibXBarP(spcPara, spcOption, ref spcOutData);
                    break;
                case "P":
                    spcOption.chartType = ControlChartType.p;
                    rtnChartData = SPCLibs.SpcLibP(spcPara, spcOption, ref spcOutData);
                    break;
                case "NP":
                    spcOption.chartType = ControlChartType.np;
                    rtnChartData = SPCLibs.SpcLibNP(spcPara, spcOption, ref spcOutData);
                    break;
                case "C":
                    spcOption.chartType = ControlChartType.c;
                    rtnChartData = SPCLibs.SpcLibC(spcPara, spcOption, ref spcOutData);
                    break;
                case "U":
                    spcOption.chartType = ControlChartType.u;
                    rtnChartData = SPCLibs.SpcLibU(spcPara, spcOption, ref spcOutData);
                    break;
                default:
                    break;
            }
            controlSpec.spcOption = spcOption;

            //sia확인 : Test XBarRExcuteTest -  UC- Grid ucXBarGrid 실행. (통계분석)
            //통계분석
            rtnCpkData = SPCLibs.SpcLibPpkCbMuti(spcPara, spcOption, ref spcOutData);
            spcPara.cpkOutData = spcOutData;
            Console.WriteLine(rtnCpkData.Rows.Count);

            if (rtnChartData != null)
            {
                //ControlChartDisplayParameter parameter = new ControlChartDisplayParameter();

                if (spcOutData.spcDataTable.tableNavigator != null
                    && spcOutData.spcDataTable.tableNavigator.Rows.Count > 0)
                {
                    this.nvgData.DataSource = spcOutData.spcDataTable.tableNavigator;
                    this.gridNavigator.DataSource = spcOutData.spcDataTable.tableNavigator;
                }
            }
        }

        /// <summary>
        /// Test Data 입력
        /// </summary>
        /// <param name="spcPara"></param>
        /// <returns></returns>
        public ParPIDataTable XBarChartTestDataInputData(SPCPara spcPara)
        {
            ParPIDataTable tempInputData = new ParPIDataTable();
            this.XBarChartTestData(ref spcPara);

            return tempInputData;
        }
        /// <summary>
        /// Test Spec 입력
        /// </summary>
        /// <param name="spcPara"></param>
        /// <returns></returns>
        public ParPISpecDataTable XBarChartTestDataInputSpec(SPCPara spcPara)
        {
            ParPISpecDataTable tempInputSpec = new ParPISpecDataTable();
            this.XBarChartTestData(ref spcPara);

            return tempInputSpec;
        }

        /// <summary>
        /// Test data 입력
        /// </summary>
        /// <param name="spcPara"></param>
        public void XBarChartTestData(ref SPCPara spcPara)
        {
            string firstSubgroup = "";
            int i = 0;
            //Test Part.......................................................
            
            string[,] pdata;
            string chartType = spcPara.ChartTypeMain();
            switch (chartType)
            {
                case "P":
                    //pdata = _spcTestData.CTR_P01();//P Chart 단일
                    pdata = _spcTestData.CTR_P03(); //P Chart Subgroup 다중 Test.
                    break;
                case "NP":
                case "C":
                case "U":
                    //pdata = _spcTestData.CTR_P01();//P Chart 단일 검증용.
                    //pdata = _spcTestData.CTR_P02();//P Chart 단일 Test
                    pdata = _spcTestData.CTR_P03(); //P Chart Subgroup 다중 Test.
                    break;
                case "I":
                case "MR":
                case "IMR":
                    if (spcPara.spcOption.specDefaultChartType == null)
                    {
                        spcPara.spcOption.specDefaultChartType = "I";
                    }
                    pdata = _spcTestData.CTR02_IMR_Standard_01();
                    break;
                default:
                    //pdata = _spcTestData.CTR03Not_Multi();
                    //pdata = _spcTestData.CTR02_PL();//단일 합동
                    //pdata = _spcTestData.CTR02Group();//그룹 /R, S Test Data
                    pdata = _spcTestData.CTR03Group();//그룹 / R, S Test Data
                    break;
            }


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
                switch (chartType)
                {
                    case "P":
                    case "NP":
                    case "C":
                    case "U":
                        dr["NSUBVALUE"] = pdata[5, i];
                        break;
                    case "IMR":
                    default:
                        dr["NSUBVALUE"] = pdata[4, i] + 1000;
                        break;
                }
                
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
                        switch (chartType)
                        {
                            case "P":
                            case "NP":
                            case "C":
                            case "U":
                                drs["USL"] = 0f;
                                drs["CSL"] = 0f;
                                drs["LSL"] = 0f;
                                drs["UCL"] = 0f;
                                drs["CCL"] = 0f;
                                drs["LCL"] = 0f;
                                drs["UOL"] = 0f;
                                drs["LOL"] = 0f;
                                break;
                            case "IMR":
                            default:
                                drs["USL"] = 135f;
                                drs["CSL"] = 75f;
                                drs["LSL"] = 35f;
                                drs["UCL"] = 95f;
                                drs["CCL"] = 63f;
                                drs["LCL"] = 25f;
                                drs["UOL"] = 165f;
                                drs["LOL"] = 5f;
                                break;
                        }

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


        #endregion



    }

    /// <summary>
    /// Chart Grid 표시용 입력 자료.
    /// </summary>
    class ControlChartDisplayParameter
    {
        public long seqStart;
        public long seqEnd;
    }
}
