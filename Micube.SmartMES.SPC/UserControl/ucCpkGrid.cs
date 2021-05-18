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
using DevExpress.XtraEditors;
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
    public partial class ucCpkGrid : DevExpress.XtraEditors.XtraUserControl
    {
        #region Local Variables

        /// <summary>
        /// 통계분석에 필요한 입력 기준정보 저장 Class
        /// </summary>
        public SPCPara spcPara = new SPCPara();
        /// <summary>
        /// 관리도 결과 Table
        /// </summary>
        public RtnControlDataTable rtnChartData = new RtnControlDataTable();
        /// <summary>
        /// 공정능력 결과 Table
        /// </summary>
        public RtnPPDataTable rtnCpkData = new RtnPPDataTable();
        /// <summary>
        /// SPC 통계 - 관리도 알고리즘 분석 Option
        /// </summary>
        public SPCOption spcOption = SPCOption.Create();
        /// <summary>
        /// NavigatorRowEnter 사용자 이벤트
        /// </summary>
        /// <param name="p"></param>
        public delegate void SpcNavigatorRowEnter(SpcEventNavigatorRowEnter p);
        /// <summary>
        /// NavigatorRowEnter 사용자 이벤트
        /// </summary>
        public virtual event SpcNavigatorRowEnter spcNavigatorRowEnter;
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
        /// 
        /// Display Chart Data
        /// </summary>
        public RtnControlDataTable chartData = new RtnControlDataTable();

        /// <summary>
        /// Display Chart Raw Data
        /// </summary>
        public RtnControlDataTable staRowData = new RtnControlDataTable();

        private SPCTestData _spcTestData = new SPCTestData();
        /// <summary>
        /// SPC 통계용 Form 공통 사용 함수 정의
        /// </summary>
        private SpcClass _spcClass = new SpcClass();
        #endregion

        #region 생성자

        public ucCpkGrid()
        {
            InitializeComponent();
            InitializeContent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 컨텐츠 초기화
        /// </summary>
        protected void InitializeContent()
        {
            InitializeEvent();
            InitializeGrid();
            InitializeControls();
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
            //User Control Load 이벤트
            //this.Load += (s, e) => InitializeContent();

            //Navigator 버튼 Click 이벤트
            this.nvgData.ButtonClick += (s, e) =>
            {
                //Console.WriteLine(this.nvgData.DataSource.);
                Console.WriteLine(this.nvgData.Text.ToSafeString());
            };

            //Grid Navigator RowEnter 이벤트
            this.grdNavigator.RowEnter += (s, e) =>
            {
                try
                {
                    spcWaitOption.CheckValue = true;
                    SpcVtChartShowWaitAreaEventHandler?.Invoke(s, e, spcWaitOption);
                    //sia확인 : CpkGrid Navigator -RowEnter 이벤트 - Page 처리 부분.
                    ControlChartDisplayParameterCpk parameter = new ControlChartDisplayParameterCpk();
                    parameter.seqStart = grdNavigator.Rows[e.RowIndex].Cells["SEQIDSTART"].Value.ToSafeInt64();
                    parameter.seqEnd = grdNavigator.Rows[e.RowIndex].Cells["SEQIDEND"].Value.ToSafeInt64();

                    this.ucCpk1.ControlChartDataMappingClear();
                    this.ucCpk2.ControlChartDataMappingClear();
                    this.ucCpk3.ControlChartDataMappingClear();
                    this.ucCpk4.ControlChartDataMappingClear();

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

            switch (spcPara.spcOption.chartType)
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
                chartType = spcPara.spcOption.chartType,
                gridTitle = string.Format("{0}", subGroupName)
            };

            frm.ShowDialog();

        }
        #endregion

        #region Public Function

        /// <summary>
        /// 공정능력 Chart 실행.
        /// </summary>
        public void CpkExcute(SPCPara spcParaData)
        {
            SPCOutData xBarOutData = SPCOutData.Create();
            SPCOutData cpkOutData = SPCOutData.Create();
            ControlSpec controlSpec = new ControlSpec();
            
            //입력 측정 자료 전이.
            spcPara = spcParaData;
            if (spcParaData != null && spcParaData.spcOption != null)
            {
                spcOption = spcParaData.spcOption;
            }
            spcOption.sigmaType = spcPara.isUnbiasing;

            //sia확인 : 8/6 공정능력 Chart type 조정.
            spcOption.chartName.xCpkChartType = spcPara.ChartTypeMain();
            int i = 0;
            int j = 0;
            //sia확인 : test - UC-Frame XBarChart 실행.
            string chartType = spcPara.ChartTypeMain();
            //spcOption.chartName.xBarChartType = chartType;
            //spcOption.chartName.xCpkChartType = chartType;
            //spcOption.sigmaType = spcPara.isUnbiasing;

            //sia확인 : 8/6 공정능력 Chart type 조정.
            //string chartType = this.comboBoxMode.Text.ToString();

            spcOption.chartName.xCpkChartType = spcPara.ChartTypeMain();
            switch (spcOption.chartName.xCpkChartType)
            {
                case "XBARR":
                case "R":
                    spcOption.chartType = ControlChartType.XBar_R;
                    rtnChartData = SPCLibs.SpcLibXBarR(spcPara, spcOption, ref xBarOutData);
                    break;
                case "XBARS":
                case "S":
                    spcOption.chartType = ControlChartType.XBar_S;
                    rtnChartData = SPCLibs.SpcLibXBarS(spcPara, spcOption, ref xBarOutData);
                    break;
                case "IMR":
                case "MR":
                case "I":
                    spcOption.chartType = ControlChartType.I_MR;
                    rtnChartData = SPCLibs.SpcLibIMR(spcPara, spcOption, ref xBarOutData);
                    break;
                case "XBARP":
                case "PL":
                    spcOption.chartType = ControlChartType.Merger;
                    rtnChartData = SPCLibs.SpcLibXBarP(spcPara, spcOption, ref xBarOutData);
                    break;
                case "P":
                case "NP":
                case "C":
                case "U":
                    spcOption.chartType = ControlChartType.p;
                    rtnChartData = SPCLibs.SpcLibP(spcPara, spcOption, ref xBarOutData);
                    break;
                default:
                    break;
            }

            spcPara.xBarOutData = xBarOutData;
            spcPara.spcOption = spcOption;
            controlSpec.spcOption = spcOption;
            //sia확인 : 시간 Check 9/19
            rtnCpkData = SPCLibs.SpcLibPpkCbMuti(spcPara, spcOption, ref cpkOutData);
            spcPara.cpkOutData = cpkOutData;
            Console.WriteLine(rtnChartData.Rows.Count);

            if (rtnChartData != null)
            {
                if (xBarOutData.spcDataTable.tableNavigator != null
                    && xBarOutData.spcDataTable.tableNavigator.Rows.Count > 0)
                {
                    this.nvgData.DataSource = xBarOutData.spcDataTable.tableNavigator;
                    this.grdNavigator.DataSource = xBarOutData.spcDataTable.tableNavigator;
                }
            }
            //Console.WriteLine(_rtnChartData.ToString());
        }

        /// <summary>
        /// 공정능력 Chart 실행 - Test.
        /// </summary>
        public void CpkExcuteTest(SPCPara spcParaData)
        {
            SPCOutData xBarOutData = SPCOutData.Create();
            SPCOutData cpkOutData = SPCOutData.Create();
            ControlSpec controlSpec = new ControlSpec();
            spcPara = spcParaData;
            spcOption.sigmaType = spcPara.isUnbiasing;

            //sia확인 : 8/6 공정능력 Chart type 조정.
            spcOption.chartName.xCpkChartType = spcPara.ChartTypeMain();
            spcPara.spcOption = spcOption;
            //int i = 0;
            //int j = 0;

            //sia필수확인 : UC-Frame XBarChart 실행.
            //Test Part.......................................................
            //Data 직접 입력 - Test DATA
            XBarChartTestData(ref spcPara);

            //sia확인 : test - UC-Frame XBarChart 실행.
            
            string chartType = spcPara.ChartTypeMain();
            if (chartType == null || chartType == "")
            {
                chartType = "XBARR";
            }
            //
            //chartType = "MR";

            spcOption.chartName.xBarChartType = chartType;
            spcOption.chartName.xCpkChartType = chartType;

            //sia필수확인 : UC- Grid ucCpkGrid 실행. (통계분석)
            //Chart Type별 통계분석
            switch (spcOption.chartName.xBarChartType)
            {
                case "XBARR":
                case "R":
                    spcOption.chartType = ControlChartType.XBar_R;
                    rtnChartData = SPCLibs.SpcLibXBarR(spcPara, spcOption, ref xBarOutData);
                    break;
                case "XBARS":
                case "S":
                    spcOption.chartType = ControlChartType.XBar_S;
                    rtnChartData = SPCLibs.SpcLibXBarS(spcPara, spcOption, ref xBarOutData);
                    break;
                case "IMR":
                case "MR":
                case "I":
                    spcOption.chartType = ControlChartType.I_MR;
                    rtnChartData = SPCLibs.SpcLibIMR(spcPara, spcOption, ref xBarOutData);
                    break;
                case "XBARP":
                case "PL":
                    spcOption.chartType = ControlChartType.Merger;
                    rtnChartData = SPCLibs.SpcLibXBarP(spcPara, spcOption, ref xBarOutData);
                    break;
                case "P":
                case "NP":
                case "C":
                case "U":
                    spcOption.chartType = ControlChartType.p;
                    rtnChartData = SPCLibs.SpcLibP(spcPara, spcOption, ref xBarOutData);
                    break;
                default:
                    break;
            }

            spcPara.xBarOutData = xBarOutData;
            spcPara.spcOption = spcOption;
            controlSpec.spcOption = spcOption;

            //Test.
            spcPara.spcOption.limitType = LimitType.Direct;
            //spcPara.cpkTempSpec = SpcSpec.Create();
            spcPara.USL = 0.003;
            spcPara.LSL = -0.003;

            rtnCpkData = SPCLibs.SpcLibPpkCbMuti(spcPara, spcOption, ref cpkOutData);
            spcPara.cpkOutData = cpkOutData;
            Console.WriteLine(rtnCpkData.Rows.Count);

            if (rtnChartData != null)
            {
                //ControlChartDisplayParameter parameter = new ControlChartDisplayParameter();

                if (xBarOutData.spcDataTable.tableNavigator != null
                    && xBarOutData.spcDataTable.tableNavigator.Rows.Count > 0)
                {
                    this.nvgData.DataSource = xBarOutData.spcDataTable.tableNavigator;
                    this.grdNavigator.DataSource = xBarOutData.spcDataTable.tableNavigator;
                    //DataRow navRow = spcOutData.spcDataTable.tableNavigator.Rows[0];
                    //parameter.seqStart = navRow["SEQIDSTART"].ToSafeInt64();
                    //parameter.seqEnd = navRow["SEQIDEND"].ToSafeInt64();
                }
            }
            //Console.WriteLine(_rtnChartData.ToString());
            //this.grdViewPPI.DataSource = _rtnChartData;
            //this.gridResultView(_rtnChartData);
        }


        /// <summary>
        /// CpkTestData
        /// </summary>
        /// <param name="spcPara"></param>
        public void XBarChartTestData(ref SPCPara spcPara)
        {
            string firstSubgroup = "";
            int i = 0;
            //Test Part.......................................................
            //string[,] pdata = _spcTestData.CTR_P01();//P Chart 단일
            //string[,] pdata = _spcTestData.CTR01();//단일
            //string[,] pdata = _spcTestData.CTR01_SubgroupOne();//단일
            //string[,] pdata = _spcTestData.CTR01Not();//단일
            string[,] pdata = _spcTestData.CTR01Digit();//단일
            //string[,] pdata = _spcTestData.CTR01_ErrMode();//Error Test
            //string[,] pdata = _spcTestData.CTR03Not_Multi();//단일

            //string[,] pdata = _spcTestData.CTR02_PL();//단일 합동
            //string[,] pdata = _spcTestData.CTR02_IMR();//단일
            //string[,] pdata = _spcTestData.CTR02Group();//그룹 /R, S Test Data
            //string[,] pdata = _spcTestData.CTR03Group();//그룹 / R, S Test Data
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
                        drs["USL"] = 5f;// 135f;
                        drs["CSL"] = 2.5f;// 75f;
                        drs["LSL"] = 2f;//35f;
                        drs["UCL"] = 1f;//95f;
                        drs["CCL"] = 1f;//63f;
                        drs["LCL"] = 1f;//25f;
                        drs["UOL"] = 1f;//165f;
                        drs["LOL"] = 1f;// 5f;
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
        /// Chart Grid 표시.
        /// </summary>
        /// <returns></returns>
        private int ControlChartDisplay(ControlChartDisplayParameterCpk parameter)
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
            var pageChartData = from item in rtnChartData
                                where (item.TEMPID >= parameter.seqStart && item.TEMPID <= parameter.seqEnd)
                                select item;
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
                dataRow["RUCL"] = item.RUCL.ToNullOrDouble();
                dataRow["RLCL"] = item.RLCL.ToNullOrDouble();
                dataRow["RCL"] = item.RCL.ToNullOrDouble();
                dataRow["BAR"] = item.BAR.ToNullOrDouble();
                dataRow["UCL"] = item.UCL.ToNullOrDouble();
                dataRow["LCL"] = item.LCL.ToNullOrDouble();
                dataRow["CL"] = item.CL.ToNullOrDouble();
                dataRow["XBAR"] = item.XBAR.ToNullOrDouble();
                dataRow["RBAR"] = item.RBAR.ToNullOrDouble();
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
                            if (i < nRowMax)
                            {
                                dataRow = staRowData.Rows[i];//sia확인 : Log처리 할것 SpcFlag.DataEnd 인식못함. 8/21
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
                            ControlSpec controlSpec = ControlSpec.Create();
                            controlSpec.spcOption = spcOption;

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
                            //sia확인 : Cpk XBar.
                            switch (controlSpec.spcOption.chartType)
                            {
                                case ControlChartType.XBar_R:
                                case ControlChartType.XBar_S:
                                case ControlChartType.Merger:
                                case ControlChartType.I_MR:
                                    spcPara.rtnXBar.XBarSigma = chartData.Rows[nMaxIndex]["STDEVALUE"].ToSafeDoubleStaMax();
                                    spcPara.rtnXBar.XBAR = chartData.Rows[nMaxIndex]["XBAR"].ToSafeDoubleStaMax();
                                    spcPara.rtnXBar.XRSP = chartData.Rows[nMaxIndex]["RBAR"].ToSafeDoubleStaMax();
                                    spcPara.rtnXBar.UCL = chartData.Rows[nMaxIndex]["UCL"].ToSafeDoubleStaMax();
                                    spcPara.rtnXBar.LCL = chartData.Rows[nMaxIndex]["LCL"].ToSafeDoubleStaMax();
                                    spcPara.rtnXBar.RUCL = chartData.Rows[nMaxIndex]["RUCL"].ToSafeDoubleStaMax();
                                    spcPara.rtnXBar.RLCL = chartData.Rows[nMaxIndex]["RLCL"].ToSafeDoubleStaMax();
                                    break;
                                case ControlChartType.np:
                                    break;
                                case ControlChartType.p:
                                    spcPara.rtnXBar.XBAR = chartData.Rows[nMaxIndex]["XBAR"].ToSafeDoubleStaMax();
                                    break;
                                case ControlChartType.c:
                                    break;
                                case ControlChartType.u:
                                    break;
                                default:
                                    break;
                            }

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
                                    this.ucCpk1.ControlChartDataMapping(chartData, controlSpec, spcPara);
                                    break;
                                case 2:
                                    this.grp02.Text = string.Format("{0}{1}", SpcLimit.ChartGroupLayoutTitleSpace, subGroupNameBefore.ToSafeString());
                                    this.grp02.Tag = subGroupBefore.ToSafeString();
                                    this.ucCpk2.ControlChartDataMapping(chartData, controlSpec, spcPara);
                                    break;
                                case 3:
                                    this.grp03.Text = string.Format("{0}{1}", SpcLimit.ChartGroupLayoutTitleSpace, subGroupNameBefore.ToSafeString());
                                    this.grp03.Tag = subGroupBefore.ToSafeString();
                                    this.ucCpk3.ControlChartDataMapping(chartData, controlSpec, spcPara);
                                    break;
                                case 4:
                                    this.grp04.Text = string.Format("{0}{1}", SpcLimit.ChartGroupLayoutTitleSpace, subGroupNameBefore.ToSafeString());
                                    this.grp04.Tag = subGroupBefore.ToSafeString();
                                    this.ucCpk4.ControlChartDataMapping(chartData, controlSpec, spcPara);
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
                                catch (Exception)
                                {
                                    //sia확인 : 오류처리 
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




        #endregion

    }

    /// <summary>
    /// Chart Grid 표시용 입력 자료.
    /// </summary>
    class ControlChartDisplayParameterCpk
    {
        public long seqStart;
        public long seqEnd;
    }
}
