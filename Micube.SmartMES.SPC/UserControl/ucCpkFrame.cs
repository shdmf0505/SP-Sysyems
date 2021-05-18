#region using

using Micube.Framework.SmartControls;
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

#endregion

namespace Micube.SmartMES.SPC.UserControl
{
    /// <summary>
    /// 프 로 그 램 명  : SPC > UserControl > ucXBarFrame
    /// 업  무  설  명  : SPC 통계 관리도 분석 Frame
    /// 생    성    자  : 이승우
    /// 생    성    일  : 2019-07-19
    /// 수  정  이  력  : 
    /// 2019-09-02  중심치 이탈 추가. Raw Data View 수정.
    /// 2019-07-19  XBar Chart 연결
    /// 2019-07-01  최초작성
    /// 
    /// </summary>   
    public partial class ucCpkFrame : DevExpress.XtraEditors.XtraUserControl
    {
        #region Local Variables

        /// <summary>
        /// grid 타이틀
        /// </summary>
        private string[] _gridCaption;
        /// <summary>
        /// 통계 분석용 DB Data 원본 자료
        /// </summary>
        private DataTable _dtInputRawData;
        /// <summary>
        /// 통계 분석용 DB Data 원본 자료의 기준정보성 SPEC
        /// </summary>
        private DataTable _dtInputSpecData;
        /// <summary>
        /// SPC 통계 - 관리도 알고리즘 분석 Option
        /// </summary>
        private SPCOption _inputSpcOption;
        /// <summary>
        /// 재분석 구분 : true - 재분석 실행. false - 분석함.
        /// </summary>
        private bool _isAgainAnalysis = false;
        /// <summary>
        /// 통계분석 Parameter
        /// </summary>
        private AnalysisExecutionParameter _AnalysisParameter = new AnalysisExecutionParameter();
        #endregion

        #region 생성자

        public ucCpkFrame()
        {
            InitializeComponent();
            InitializeContent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected void InitializeContent()
        {
            InitializeControls();
            InitializeGrid(0, "");
            InitializeEvent();
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid(int rowMax, string subGroupTitle, string subGroupName = "")
        {
            grdSelectChartRawData.GridButtonItem = GridButtonItem.Export;
            grdSelectChartRawData.Caption = string.Empty;
            grdSelectChartRawData.View.ClearColumns();
            string fieldName = "";
            if (rowMax > 0)
            {
                grdSelectChartRawData.Caption = subGroupTitle;

                for (int i = 0; i < rowMax; i++)
                {
                    fieldName = string.Format("FIELD_{0:D3}", i + 1);
                    grdSelectChartRawData.View.AddSpinEditColumn(fieldName, 150)
                        //.SetDisplayFormat("{0:f}", MaskTypes.Custom) // 측정값
                        .SetDisplayFormat("#,##0.##########", MaskTypes.Numeric)
                        .SetLabel(_gridCaption[i])
                        .SetIsReadOnly();
                }
                grdSelectChartRawData.View.PopulateColumns();
            }


        }

        /// <summary>
        /// 화면 Controls 초기화
        /// </summary>
        private void InitializeControls()
        {
            DataTable dtLeftChartType = SpcClass.CreateTableSpcComboBox(this.cboLeftChartType.Name.ToString());

            SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.XBar_R);
            SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.XBar_S);
            SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.Merger);
            SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.I_MR);
            SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.np);
            SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.p);
            SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.c);
            SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.u);

            this.cboLeftChartType.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            this.cboLeftChartType.ShowHeader = false;
            this.cboLeftChartType.DisplayMember = "Display";
            this.cboLeftChartType.ValueMember = "Value";
            this.cboLeftChartType.DataSource = dtLeftChartType;

            this.cboLeftChartType.ItemIndex = 0;
            this.ControlLimitGroupCheck();

            this.txtAllUclValue.Text = string.Empty;
            this.txtAllCclValue.Text = string.Empty;
            this.txtAllLclValue.Text = string.Empty;

            this.txtAllRUclValue.Text = string.Empty;
            this.txtAllRCclValue.Text = string.Empty;
            this.txtAllRLclValue.Text = string.Empty;
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.rdoLeftGroup.SelectedIndexChanged += (s, e) => ControlLimitGroupCheck();
            this.ucCpkGrid01.ucCpk1.SpcCpkChartEnterEventHandler += (s, e, se) => ChartRawData(ucCpkGrid01.grp01.Tag.ToSafeString());
            this.ucCpkGrid01.ucCpk2.SpcCpkChartEnterEventHandler += (s, e, se) => ChartRawData(ucCpkGrid01.grp02.Tag.ToSafeString());
            this.ucCpkGrid01.ucCpk3.SpcCpkChartEnterEventHandler += (s, e, se) => ChartRawData(ucCpkGrid01.grp03.Tag.ToSafeString());
            this.ucCpkGrid01.ucCpk4.SpcCpkChartEnterEventHandler += (s, e, se) => ChartRawData(ucCpkGrid01.grp04.Tag.ToSafeString());
            //this.ucCpkGrid01.Resize += (s, e) =>
            //{
            //    //this.splMain.SplitterPosition = 170;
            //    //this.splMainSub.SplitterPosition = this.Height - (this.Height / 5);
            //};
            this.ucCpkGrid01.spcNavigatorRowEnter += UcCpkGrid01_spcNavigatorRowEnter;
        }

        /// <summary>
        ///  Grid Navigator RowEnter 
        /// </summary>
        /// <param name="p"></param>
        private void UcCpkGrid01_spcNavigatorRowEnter(SpcEventNavigatorRowEnter p)
        {
            ChartRawData(ucCpkGrid01.grp01.Tag.ToSafeString());
        }

        /// <summary>
        /// 재실행 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReExecution_Click(object sender, EventArgs e)
        {
            //단일 Test.
            //Chart 구분
            _AnalysisParameter.spcOption = SPCOption.Create();
            string chartType = this.cboLeftChartType.GetDataValue().ToSafeString().ToUpper().Replace("_", "");
            _AnalysisParameter.spcOption.chartName.xBarChartType = chartType;
            _AnalysisParameter.spcOption.chartName.xCpkChartType = chartType;
            _AnalysisParameter.spcOption.sigmaType = this.chkLeftEstimate.Checked ? SigmaType.Yes : SigmaType.No;//추정치 사용 유무
            _isAgainAnalysis = true;
            //_AnalysisParameter.dtInputRawData = _dtInputRawData;
            //_AnalysisParameter.dtInputSpecData = _dtInputSpecData;

            //SpcFrameChangeData f = new SpcFrameChangeData();
            //f.isAgainAnalysis = _isAgainAnalysis;
            //f.SPCOption = _AnalysisParameter.spcOption;
            
            if (SpcFunction.isPublicTestMode == 0)
            {
                //원영.
                this.AnalysisExecutionMain(0);
            }
            else
            {
                //Test.
                _inputSpcOption = _AnalysisParameter.spcOption;
                this.AnalysisExecutionMain(1);
            }
        }
        #endregion

        #region Private Function
        public void ClearChartControlsCpk()
        {
            this.ucCpkGrid01.grp01.Text = "";
            this.ucCpkGrid01.grp02.Text = "";
            this.ucCpkGrid01.grp03.Text = "";
            this.ucCpkGrid01.grp04.Text = "";
            this.ucCpkGrid01.grp01.Tag = "";
            this.ucCpkGrid01.grp02.Tag = "";
            this.ucCpkGrid01.grp03.Tag = "";
            this.ucCpkGrid01.grp04.Tag = "";
            this.ucCpkGrid01.ucCpk1.ControlChartDataMappingClear();
            this.ucCpkGrid01.ucCpk2.ControlChartDataMappingClear();
            this.ucCpkGrid01.ucCpk3.ControlChartDataMappingClear();
            this.ucCpkGrid01.ucCpk4.ControlChartDataMappingClear();
            ChartRawData("");

        }


        /// <summary>
        /// Chart Raw Data 표시.
        /// </summary>
        private void ChartRawData(string subGroupName)
        {
            int i = 0;
            grdSelectChartRawData.Caption = "";
            grdSelectChartRawData.DataSource = null;
            grdSelectChartRawData.View.ClearColumns();
            grdSelectChartRawData.GridButtonItem = GridButtonItem.Export;

            if (this.ucCpkGrid01.spcPara.InputData == null)
            {
                return;
            }

            if (subGroupName == "")
            {
                return;
            }
            

            int subGroupMax = 0;
            int samplingMax = 0;
            string strSubgroupTitle = "";

            var sumSubGroup = from g in this.ucCpkGrid01.spcPara.InputData
                              where (g.SUBGROUP == subGroupName)
                              group g by new
                              {
                                  g.SUBGROUP,
                                  g.SAMPLING
                              }
                              into g
                              orderby g.Key.SAMPLING
                              select new
                              {
                                  vGROUPID = g.Max(s => s.GROUPID),
                                  vSAMPLEID = g.Max(s => s.SAMPLEID),
                                  vSUBGROUP = g.Key.SUBGROUP,
                                  vSAMPLING = g.Max(s => s.SAMPLING),
                                  vSUBGROUPNAME = g.Max(s => s.SUBGROUPNAME),
                                  vSAMPLINGNAME = g.Max(s => s.SAMPLINGNAME),
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

            //Cpk
            dataTable.TableName = "ChartRaw";

            for (i = 0; i < subGroupMax; i++)
            {
                fieldName = string.Format("FIELD_{0:D3}", i + 1);
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
            _gridCaption = new string[subGroupMax];
            string samplingCaption = "";
            string samplingName = "";
            string samplingRow = "";

            //var subGroupData = from item in this.ucCpkGrid01.spcPara.InputData
            //                   where (item.SUBGROUP == subGroupName)
            //                   select item;
            var subGroupData = this.ucCpkGrid01.spcPara.InputData
                                 .Where(x => x.SUBGROUP == subGroupName)
                                 .OrderBy(r1 => r1.SAMPLING).ThenByDescending(r2 => r2.NVALUE);
            strSubgroupTitle = "";
            foreach (DataRow item in subGroupData)
            {
                samplingRow = SpcFunction.IsDbNck(item, "SAMPLING");
                if (samplingName != samplingRow)
                {
                    samplingCaption = samplingRow;
                    _gridCaption[fieldIndex] = samplingCaption;
                    fieldIndex++;
                    samplingName = samplingRow;
                    fieldName = string.Format("FIELD_{0:D3}", fieldIndex);
                    rowIndex = 0;
                }

                if (strSubgroupTitle == "")
                {
                    strSubgroupTitle = item["SUBGROUPNAME"].ToSafeString();
                    strSubgroupTitle = strSubgroupTitle.Replace(SpcSym.subgroupSep11, SpcSym.subgroupSep12);
                }

                DataRow dataRow = dataTable.Rows[rowIndex];
                double nNvalue = 0.0f;
                nNvalue = SpcFunction.IsDbNckDoubleMax(item, "NVALUE");
                dataRow[fieldName] = nNvalue;
                rowIndex++;
            }

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                InitializeGrid(subGroupMax, strSubgroupTitle, subGroupName);
                this.grdSelectChartRawData.DataSource = dataTable;
                this.grdSelectChartRawData.Refresh();
            }
            else
            {
                InitializeGrid(0, "");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ControlLimitGroupCheck()
        {
            int rdoLeftGroupIndex = rdoLeftGroup.SelectedIndex;

            switch (rdoLeftGroupIndex)
            {
                case 0:
                case 1:
                    this.txtAllUclValue.ReadOnly = true;
                    this.txtAllCclValue.ReadOnly = true;
                    this.txtAllLclValue.ReadOnly = true;
                    this.txtAllRUclValue.ReadOnly = true;
                    this.txtAllRCclValue.ReadOnly = true;
                    this.txtAllRLclValue.ReadOnly = true;
                    break;
                case 2:
                    this.txtAllUclValue.Text = "";
                    this.txtAllCclValue.Text = "";
                    this.txtAllLclValue.Text = "";
                    this.txtAllRUclValue.Text = "";
                    this.txtAllRCclValue.Text = "";
                    this.txtAllRLclValue.Text = "";
                    this.txtAllUclValue.ReadOnly = false;
                    this.txtAllCclValue.ReadOnly = false;
                    this.txtAllLclValue.ReadOnly = false;
                    this.txtAllRUclValue.ReadOnly = false;
                    this.txtAllRCclValue.ReadOnly = false;
                    this.txtAllRLclValue.ReadOnly = false;
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Public Function
        /// <summary>
        /// 공정능력 Chart 분석 및 표시 실행.
        /// 메뉴 폼에서 실행하는 함수.
        /// </summary>
        public void AnalysisExecution(AnalysisExecutionParameter para)
        {
            _dtInputRawData = para.dtInputRawData;
            _dtInputSpecData = para.dtInputSpecData;
            _inputSpcOption = para.spcOption;
            _AnalysisParameter = para;

            if (_dtInputRawData != null)
            {
                AnalysisExecutionMain();
                ChartRawData(ucCpkGrid01.grp01.Tag.ToSafeString());
            }
            
        }
        /// <summary>
        /// Test - 다중 모드
        /// </summary>
        /// <param name="para"></param>
        public void TestAnalysisExecution(AnalysisExecutionParameter para)
        {
            //sia삭제 : 차후 삭제 요망.
            _dtInputRawData = para.dtInputRawData;
            _dtInputSpecData = para.dtInputSpecData;
            _inputSpcOption = para.spcOption;
            AnalysisExecutionMain(1);

            ChartRawData(ucCpkGrid01.grp01.Tag.ToSafeString());
        }

        /// <summary>
        /// Test - 단일 모드 공정능력 Chart 분석 및 표시 - Test
        /// </summary>
        public void AnalysisExecutionTest()
        {
            AnalysisExecutionMain(1);
            //단일 Test.
            //Chart 구분
            _AnalysisParameter.spcOption = SPCOption.Create();
            string chartType = this.cboLeftChartType.GetDataValue().ToSafeString().ToUpper().Replace("_", "");
            _AnalysisParameter.spcOption.chartName.xBarChartType = chartType;
            _AnalysisParameter.spcOption.chartName.xCpkChartType = chartType;
            _AnalysisParameter.spcOption.sigmaType = this.chkLeftEstimate.Checked ? SigmaType.Yes : SigmaType.No;//추정치 사용 유무
            _isAgainAnalysis = true;
            //_AnalysisParameter.dtInputRawData = _dtInputRawData;
            //_AnalysisParameter.dtInputSpecData = _dtInputSpecData;

            SpcFrameChangeData f = new SpcFrameChangeData();
            f.isAgainAnalysis = _isAgainAnalysis;
            f.SPCOption = _AnalysisParameter.spcOption;

            ChartRawData(ucCpkGrid01.grp01.Tag.ToSafeString());
        }

        /// <summary>
        /// 공정능력 입력 자료 구성.
        /// </summary>
        /// <param name="option"></param>
        private void AnalysisExecutionMain(int option = 0)
        {
            int i = 0;
            string firstSubgroup = "";
            SPCPara specEdit = new SPCPara();
            specEdit.tableSubgroupSpec = SPCPara.tableSubgroupSpecCreate("Cpk");
            int rdoLeftGroupIndex = rdoLeftGroup.SelectedIndex;


            //Chart 구분
            string chartType = "";
            if (_inputSpcOption != null)
            {
                chartType = _inputSpcOption.chartName.xCpkChartType;
            }
            specEdit.ChartTypeMain(chartType);
            if (_AnalysisParameter.spcOption != null)
            {
                specEdit.spcOption = _AnalysisParameter.spcOption;
            }

            //추정치 구분
            if (_inputSpcOption != null)
            {
                specEdit.isUnbiasing = _inputSpcOption.sigmaType;
            }

            //Raw Data 전이
            specEdit.tableRawData = _dtInputRawData;
            specEdit.tableSubgroupSpec = _dtInputSpecData;
            string chartMainType = specEdit.ChartTypeMain();

            #region 공정능력분석 ucCpkFrame DB 입력 Raw Data 구성 부분.
            //sia중요 : 공정능력분석 ucCpkFrame DB 입력 Raw Data 구성 부분.
            //var rowData = _dtInputData.AsEnumerable().Where(w => w["ctype"].ToSafeString() = "").list();
            if (option == 0)
            {
                //Start - Raw Data 입력 Part.......................................................
                var rowDatas = _dtInputRawData.AsEnumerable();
                specEdit.InputData = new ParPIDataTable();
                var lstRow = rowDatas.AsParallel()
                    //.Where(w => w.Field<string>("ctype") == "XBARR")
                    .OrderBy(or => or.Field<string>("SUBGROUP"))
                    .ThenBy(or => or.Field<string>("SAMPLING"));
                foreach (DataRow row in lstRow)
                {
                    DataRow dr = specEdit.InputData.NewRow();
                    dr["GROUPID"] = 1;
                    dr["SAMPLEID"] = 1;//자동증가
                    dr["SUBGROUP"] = SpcFunction.IsDbNck(row, _AnalysisParameter.dtInputRawMainFieldName.subgroupNameFiled);
                    dr["SUBGROUPNAME"] = SpcFunction.IsDbNck(row, _AnalysisParameter.dtInputRawMainFieldName.subgroupNameFiledName);
                    dr["SAMPLING"] = SpcFunction.IsDbNck(row, _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiled);
                    dr["SAMPLINGNAME"] = SpcFunction.IsDbNck(row, _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiledName);
                    if (_AnalysisParameter.dtInputRawMainFieldName.samplingNameFiled01 != null
                            && _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiled01 != "")
                    {
                        dr["SAMPLING01"] = SpcFunction.IsDbNck(row, _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiled01);
                    }
                    if (_AnalysisParameter.dtInputRawMainFieldName.samplingNameFiledName01 != null
                            && _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiledName01 != "")
                    {
                        dr["SAMPLINGNAME01"] = SpcFunction.IsDbNck(row, _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiledName01);
                    }

                    if (_AnalysisParameter.dtInputRawMainFieldName.samplingNameFiled02 != null
                            && _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiled02 != "")
                    {
                        dr["SAMPLING02"] = SpcFunction.IsDbNck(row, _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiled02);
                    }
                    if (_AnalysisParameter.dtInputRawMainFieldName.samplingNameFiledName02 != null
                            && _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiledName02 != "")
                    {
                        dr["SAMPLINGNAME02"] = SpcFunction.IsDbNck(row, _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiledName02);
                    }
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

                if (_dtInputSpecData != null && _dtInputSpecData.Rows.Count > 0)
                {
                    nLength = _dtInputSpecData.Rows.Count;
                    for (i = 0; i < nLength; i++)
                    {
                        DataRow rowSpec = _dtInputSpecData.Rows[i];
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

            }
            #endregion

            switch (_inputSpcOption.limitType)
            {
                case LimitType.Interpretation:
                    break;
                case LimitType.Management:
                    break;
                case LimitType.Direct:
                    specEdit.USL = _inputSpcOption.DirectModeCpkSpec.usl;
                    specEdit.CSL = _inputSpcOption.DirectModeCpkSpec.csl;
                    specEdit.LSL = _inputSpcOption.DirectModeCpkSpec.lsl;
                    break;
                default:
                    break;
            }

            //this.ucCpkGrid01.spcPara.LOGMODE = "N";
            this.ucCpkGrid01.spcPara.SpecMode = 0;
            //this.ucCpkGrid01.spcPara.isUnbiasing = this.chkLeftEstimate.Checked ? "Y" : "N";
            this.ucCpkGrid01.spcPara.isUnbiasing = specEdit.isUnbiasing;
            this.ucCpkGrid01.spcPara.USL = specEdit.USL;
            this.ucCpkGrid01.spcPara.CSL = specEdit.CSL;
            this.ucCpkGrid01.spcPara.LSL = specEdit.LSL;
            this.ucCpkGrid01.spcPara.UCL = specEdit.UCL;
            this.ucCpkGrid01.spcPara.CCL = specEdit.CCL;
            this.ucCpkGrid01.spcPara.LCL = specEdit.LCL;
            this.ucCpkGrid01.spcPara.RUCL = specEdit.RUCL;
            this.ucCpkGrid01.spcPara.RCCL = specEdit.RCCL;
            this.ucCpkGrid01.spcPara.RLCL = specEdit.RLCL;
            //this.ucCpkGrid01.spcPara.UCL = this.txtAllUclValue.

            //Chart 실행.
            if (option == 0)
            {
                this.ucCpkGrid01.CpkExcute(specEdit);
            }
            else
            {
                specEdit.ChartTypeMain(chartType);
                //specEdit.spcOption.chartType = chartType;
                this.ucCpkGrid01.CpkExcuteTest(specEdit);
            }
            

            RtnControlDataTable staRowData = this.ucCpkGrid01.staRowData;

            string subGroupName = this.ucCpkGrid01.grp01.Text.ToSafeString().Trim();
            if (subGroupName != "")
            {
                this.ChartRawData(subGroupName);
            }

            //RtnControlDataTable staRowData = this.ucCpkGrid01.chartData;
            //if (staRowData != null && staRowData.Rows.Count > 0)
            //{
            //    this.grdChemicalRegistration.DataSource = staRowData;
            //    //this.grdChemicalRegistration.Caption
            //}
        }




        #endregion


    }
}
