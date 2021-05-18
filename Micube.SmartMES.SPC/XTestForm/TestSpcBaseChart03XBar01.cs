using DevExpress.XtraCharts;
using DevExpress.XtraLayout.Utils;
using Micube.SmartMES.Commons.SPCLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Micube.SmartMES.Commons.SPCLibrary.DataSets.SpcDataSet;

namespace Micube.SmartMES.SPC
{
    public partial class TestSpcBaseChart03XBar01 : Form
    {

        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자

        public TestSpcBaseChart03XBar01()
        {
            InitializeComponent();

        }

        #endregion

        #region 툴바
        //저장버튼 클릭

        #endregion

        #region 검색
        //비동기 override 모델

        #endregion

        #region 유효성 검사
        //데이터 저장할때 컨텐츠 영역의 유효성 검사
        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
        }

        /// <summary>
        /// Form Load 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestSpcBaseChart03_Load(object sender, EventArgs e)
        {
            SPCPara spcPara = new SPCPara();
            this.InitializeGrid();

            //Test 실행.
            TestExeXBarDirect();
            //this.ucXBarDirect.DetailChartControlClearPCU(LayoutVisibility.Never, "P");

            //CustomLegendItem customLegendItem1 = smartChart1.CustomLegendItem();
            //this.ucXBarGrid1.XBarRExcute(spcPara);
            //this.splMain.SplitterPosition = this.Height - (this.Height / 5);
        }
        #endregion

        #region 컨텐츠 영역 초기화

        protected void InitializeContent()
        {
            InitializeEvent();
            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region Trend Table

            grdTrend.View.ClearColumns();

            //grdTrend.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdTrend.View.AddTextBoxColumn("SEQID", 100);
            grdTrend.View.AddTextBoxColumn("DEFECTNAME", 150);
            grdTrend.View.AddTextBoxColumn("DEFECTTOTAL", 150);
            grdTrend.View.AddTextBoxColumn("D01", 50);
            grdTrend.View.AddTextBoxColumn("D02", 50);
            grdTrend.View.AddTextBoxColumn("D03", 50);
            grdTrend.View.AddTextBoxColumn("D04", 50);
            grdTrend.View.AddTextBoxColumn("D05", 50);
            grdTrend.View.AddTextBoxColumn("D06", 50);
            grdTrend.View.AddTextBoxColumn("D07", 50);
            grdTrend.View.AddTextBoxColumn("D08", 50);
            grdTrend.View.AddTextBoxColumn("D09", 50);
            grdTrend.View.AddTextBoxColumn("D10", 50);
            grdTrend.View.AddTextBoxColumn("D11", 50);
            grdTrend.View.AddTextBoxColumn("D12", 50);
            grdTrend.View.AddTextBoxColumn("D13", 50);
            grdTrend.View.AddTextBoxColumn("D14", 50);
            grdTrend.View.AddTextBoxColumn("D15", 50);
            grdTrend.View.AddTextBoxColumn("D16", 50);
            grdTrend.View.AddTextBoxColumn("D17", 50);
            grdTrend.View.AddTextBoxColumn("D18", 50);
            grdTrend.View.AddTextBoxColumn("D19", 50);
            grdTrend.View.AddTextBoxColumn("D20", 50);
            grdTrend.View.AddTextBoxColumn("D21", 50);
            grdTrend.View.AddTextBoxColumn("D22", 50);
            grdTrend.View.AddTextBoxColumn("D23", 50);
            grdTrend.View.AddTextBoxColumn("D24", 50);
            grdTrend.View.AddTextBoxColumn("D25", 50);
            grdTrend.View.AddTextBoxColumn("D26", 50);
            grdTrend.View.AddTextBoxColumn("D27", 50);
            grdTrend.View.AddTextBoxColumn("D28", 50);
            grdTrend.View.AddTextBoxColumn("D29", 50);
            grdTrend.View.AddTextBoxColumn("D30", 50);
            grdTrend.View.AddTextBoxColumn("D31", 50);

            grdTrend.View.PopulateColumns();
            grdTrend.View.BestFitColumns();
            grdTrend.ShowStatusBar = false;
            grdTrend.View.OptionsView.ShowGroupPanel = false;
            grdTrend.View.OptionsBehavior.Editable = false;
            //grdTrend.GridButtonItem = GridButtonItem.None;
            grdTrend.Caption = "불량률";
            //grdTrendSum.Caption = string.Concat(Language.Get("INSERTDATE"), " : ", _date.Year, " / ", string.Format("{0:MM}", _date));

            #endregion
            #region RowData

            #endregion

            #region Over Rule

            #endregion
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>Grid에 결과 자료를 세로형으로 표시.
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
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

            this.grdViewResult.DataSource = dtResultView;
            //this.dataGridViewResult.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.grdViewResult.AutoSize = true;
            return 0;
        }





        #endregion

        private void TestSpcBaseChart03_Resize(object sender, EventArgs e)
        {
            this.splMain.SplitterPosition = this.Height - (this.Height / 4);
        }


        /// <summary>
        /// P Chart 직접 실행 Test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnXBarDirect_Click(object sender, EventArgs e)
        {
            TestExeXBarDirect();
        }

        /// <summary>
        /// XBAR 직접 Test 실행.
        /// </summary>
        private void TestExeXBarDirect()
        {
            SPCPara spcParaEdit = new SPCPara();
            spcParaEdit.spcOption = SPCOption.Create();
            spcParaEdit.spcOption.limitType = LimitType.Interpretation;
            //spcParaEdit.isUnbiasing = SigmaType.No;
            spcParaEdit.isUnbiasing = SigmaType.Yes;
            //spcParaEdit.spcOption.limitType = LimitType.Management;
            spcParaEdit.ChartTypeMain("XBARR");
            //spcParaEdit.ChartTypeMain("XBARS");
            //spcParaEdit.ChartTypeMain("XBARP");
            //spcParaEdit.ChartTypeMain("I");

            this.ucXBarDirect.DirectXBarRExcuteTest(ref spcParaEdit);

            //Application.DoEvents();
            //this.ucXBarDirect.DirectRuleCheck();
            //this.Invalidate();
            //DefectRateListView(spcParaEdit);
        }

        private void btnXBarDirect_Click_P(object sender, EventArgs e)
        {
            SPCPara spcParaEdit = new SPCPara();
            spcParaEdit.spcOption = SPCOption.Create();
            spcParaEdit.spcOption.limitType = LimitType.Interpretation;
            
            spcParaEdit.ChartTypeMain("P");
            this.ucXBarDirect.DirectXBarRExcuteTest(ref spcParaEdit);
            //Application.DoEvents();
            //this.ucXBarDirect.DirectRuleCheck();
            //this.Invalidate();
            //DefectRateListView(spcParaEdit);
        }
        //public AnalysisExecutionParameter btnReExecutionTestClick()
        //{
        //    //단일 Test.
        //    //Chart 구분
        //    _AnalysisParameter.spcOption = SPCOption.Create();
        //    string chartType = this.cboLeftChartType.GetDataValue().ToSafeString().ToUpper().Replace("_", "");
        //    _AnalysisParameter.spcOption.chartName.xBarChartType = chartType;
        //    _AnalysisParameter.spcOption.chartName.xCpkChartType = chartType;
        //    _AnalysisParameter.spcOption.sigmaType = this.chkLeftEstimate.Checked ? SigmaType.Yes : SigmaType.No;//추정치 사용 유무
        //    _isAgainAnalysis = true;
        //    //_AnalysisParameter.dtInputRawData = _dtInputRawData;
        //    //_AnalysisParameter.dtInputSpecData = _dtInputSpecData;
        //    SpcFrameChangeData f = new SpcFrameChangeData();
        //    f.isAgainAnalysis = _isAgainAnalysis;
        //    f.SPCOption = _AnalysisParameter.spcOption;
        //    if (SpcFunction.isPublicTestMode == 1)
        //    {
        //        SpcCpkChartEnterEventHandler(f);
        //    }

        //    this.AnalysisExecutionTest(ref _AnalysisParameter);
        //    return _AnalysisParameter;
        //}
        private void btnDirectRuleCheck_Click(object sender, EventArgs e)
        {
            //this.ucXBarDirect.SpecRuleCheckPCU();
            RulesCheck rulesCheckData = RulesCheck.Create();
            SpcControlRulesSettingPopup frmPopup = new SpcControlRulesSettingPopup();
            frmPopup.ShowDialog();
            if (frmPopup.DialogResult == DialogResult.OK)
            {
                rulesCheckData = frmPopup.RulesCheckData;
                Console.Write(rulesCheckData.lstRulesPara.Count);
                if (rulesCheckData.lstRulesPara.Count > 0)
                {
                    rulesCheckData = this.ucXBarDirect.RulesChecking(rulesCheckData);
                }
                //Popup_FormClosed();
                //this.OnSearchAsync();
            }
            
        }
        /// <summary>
        /// 불량코드별 불량률 표시.
        /// </summary>
        /// <param name="spcPara"></param>
        private void DefectRateListView(SPCPara spcPara)
        {
            #region 불량률 계산
            SpcLibraryHelper spcLibHelper = new SpcLibraryHelper();
            DataTable dtDefactTotal;
            DataTable dtDefactDetail;
            DataTable dtDefactView;
            DataTable dtDefact;
            string[] gridCaption;
            string statusMessage;
            string valueType = "rate";
            dtDefactDetail = spcLibHelper.DefectRateList(spcPara, out dtDefactTotal, out dtDefact, out gridCaption, out statusMessage);
            dtDefactView = spcLibHelper.DefectRateView(dtDefactDetail, valueType, out statusMessage);

            int colIndexDefault = 2;
            int colIndexAdd = 0;
            string defectiveWorst = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCUIDEFECTIVEWORST");//WORST
            string defectiveName = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCUIDEFECTIVENAME");//불량명
            string defectiveCount = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCUIDEFECTIVECOUNT");//불량수량
            string defectiveProductName = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCUIDEFECTIVEPRODUCTNAME");//모델명

            this.grdTrend.View.Columns[0].Caption = defectiveWorst;// "WORST";
            this.grdTrend.View.Columns[1].Caption = defectiveName;// "불량명"
            this.grdTrend.View.Columns[2].Caption = defectiveCount;// "부량수량";

            if (gridCaption != null && gridCaption.Length > 0 && gridCaption[1] != "")
            {
                for (int i = 1; i < 32; i++)
                {
                    colIndexAdd = colIndexDefault + i;
                    this.grdTrend.View.Columns[colIndexAdd].Caption = gridCaption[i];
                }
            }
            this.grdTrend.DataSource = null;
            this.grdTrend.DataSource = dtDefactView;

            #endregion//불량률 계산
        }

        private void txtView_TextChanged(object sender, EventArgs e)
        {

        }
    }//end class
}//end namespace
