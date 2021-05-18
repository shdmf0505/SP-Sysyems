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

namespace Micube.SmartMES.SPC
{
    public partial class TestSpcBaseChart05XBarFrame : Form
    {
        #region Local Variables
        // TODO : 화면에서 사용할 내부 변수 추가
        #region 1. 통계 Chart 지역변수        
        /// <summary>
        /// 통계분석 Parameter
        /// </summary>
        private AnalysisExecutionParameter _AnalysisParameter = AnalysisExecutionParameter.Create();
        /// <summary>
        /// 재분석 구분 : true - 재분석 실행. false - 분석함.
        /// </summary>
        private bool _isAgainAnalysis = false;
        #endregion

        #endregion

        #region 생성자

        public TestSpcBaseChart05XBarFrame()
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

        private void TestSpcBaseChart05XBarFrame_Load(object sender, EventArgs e)
        {
            //this.ucXBarGrid1.XBarRExcute();
            SpcFunction.isPublicTestMode = 1;
            if (SpcFunction.isPublicTestMode == 1)
            {
                this.ucXBarFrame1.btnReExecutionTest.Visible = true;
                this.ucXBarFrame1.btnTestDirectSpec.Visible = true;
            }
            this.FormResize();

            this.ucXBarFrame1.SpcCpkChartEnterEventHandler += UcXBarFrame1_SpcCpkChartEnterEventHandler;
            this.ucXBarFrame1.cboChartTypeSetting(0);

            this.ucXBarFrame1.ucXBarGrid01.SpcVtChartShowWaitAreaEventHandler += UcXBarGrid01_SpcVtChartShowWaitAreaEventHandler;
        }

        private void UcXBarGrid01_SpcVtChartShowWaitAreaEventHandler(object sender, EventArgs e, SpcShowWaitAreaOption se)
        {
            //try
            //{
            //    if (se.CheckValue)
            //    {
            //        DialogManager.ShowWaitArea(this.pnlContent);
            //    }
            //    else
            //    {
            //        DialogManager.CloseWaitArea(this.pnlContent);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //finally
            //{
            //    spcWaitOption.CheckValue = false;
            //    SpcVtChartShowWaitAreaEventHandler?.Invoke(sender, e, spcWaitOption);
            //}
        }

        /// <summary>
        /// Chart Option 변경.
        /// </summary>
        /// <param name="f"></param>
        private void UcXBarFrame1_SpcCpkChartEnterEventHandler(SpcFrameChangeData f)
        {
            _isAgainAnalysis = f.isAgainAnalysis;
            _AnalysisParameter.spcOption = f.SPCOption;
        }
        /// <summary>
        /// Form Resize
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestSpcBaseChart05XBarFrame_Resize(object sender, EventArgs e)
        {
            this.FormResize();
        }

        /// <summary>
        /// Tab Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            this.ChartAnalysisExecution();
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
            // TODO : 그리드 초기화 로직 추가
        }

        #endregion

        #region Private Function
        // TODO : 화면에서 사용할 내부 함수 추가
        #region 4. 통계용 함수
        /// <summary>
        /// 통계 분석 실행 - 공정능력 실행, 관리도 (Tab 이벤트에서 실행함)
        /// </summary>
        private void ChartAnalysisExecution()
        {
            string subGroupName = "";
            if (this.tabMain.SelectedTabPageIndex == 0)
            {
                subGroupName = this.ucXBarFrame1.ucXBarGrid01.grp01.Text.ToSafeString();
                if (_isAgainAnalysis)
                {
                    _isAgainAnalysis = false;
                    this.ucXBarFrame1.AnalysisExecution(_AnalysisParameter);
                }
            }
            else if (this.tabMain.SelectedTabPageIndex == 1)
            {
                this.FormResize();
                subGroupName = this.ucCpkFrame1.ucCpkGrid01.grp01.Text.ToSafeString();
                _isAgainAnalysis = true;//siaTest : Test-공정능력 실행.,
                if (_isAgainAnalysis)
                {
                    _isAgainAnalysis = false;
                    if (SpcFunction.isPublicTestMode == 0)
                    {
                        //운영.
                        this.ucCpkFrame1.AnalysisExecution(_AnalysisParameter);
                    }
                    else
                    {
                        //Test.
                        this.ucCpkFrame1.TestAnalysisExecution(_AnalysisParameter);
                    }
                    
                }

            }
            
        }
        #endregion

        /// <summary>
        /// Form Resize
        /// </summary>
        private void FormResize()
        {
            try
            {
                int position = 150;
                int rate = 3;
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

        private void ucXBarFrame1_Load(object sender, EventArgs e)
        {

        }
    }//end class
}//end namespace
