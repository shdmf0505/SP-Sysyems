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
    public partial class TestSpcDataCreate01 : Form
    {
        public TestSpcDataCreate01()
        {
            InitializeComponent();
        }

        private void buttonDataCreate_Click(object sender, EventArgs e)
        {
            string dataType = this.cboDataType.Text.Substring(0, 1);

            switch (dataType)
            {
                case "0":
                    AnalysisDataCreateXBar();
                    break;
                case "1":
                    ChartDataCreateXBar();
                    break;
                default:
                    break;
            }

        }

        private void ChartDataCreateXBar()
        {
            int i = 0;
            int length = 30;
            string axisXLabel = "";
            float nValue = 0.0f;
            float randomValue = 0.0f;
            float digitValue = 0.0f;
            Random r = new Random();
            SpcParameter xbarData = SpcParameter.Create();

            for (i = 0; i < length; i++)
            {
                //axisXLabel = string.Format("{0,100:D8} {0,10:X8}", i, i);
                axisXLabel = string.Format("Lot-{0:D3}", i + 1);
                randomValue = r.Next(1, 100);
                digitValue = (float)(r.Next(1, 100) * 0.01);
                nValue = randomValue + digitValue;
                //Console.WriteLine("{0} : {1} + {2} = {3}", axisXLabel, randomValue, digitValue, nValue);

                DataRow rowSpcData = xbarData.SpcData.NewRow();
                rowSpcData["Label"] = axisXLabel.Trim();
                rowSpcData["nValue"] = nValue;
                xbarData.SpcData.Rows.Add(rowSpcData);

                string rowDataAddHeader = "";
                string rowDataAddScript = "";
                rowDataAddHeader = string.Format(@"SpcParameter xbarData = SpcParameter.Create();
                DataRow rowSpcData;");

                rowDataAddScript = string.Format(@"rowSpcData = xbarData.SpcData.NewRow(); " +
                    @" rowSpcData[""Label""] = ""{0}""; " +
                    @" rowSpcData[""nValue""] = {1}f; " +
                    @" xbarData.SpcData.Rows.Add(rowSpcData);"
                    , axisXLabel, nValue);
                Console.WriteLine(rowDataAddScript);
            }

            Console.WriteLine("종료");
        }
        /// <summary>
        /// Test 분석용 자료 생성.
        /// </summary>
        private void AnalysisDataCreateXBar()
        {
            int i = 0;
            int j = 0;
            int n = 0;
            int lengthK = 12;
            int lengthJ = 5;
            int lengthN = 10;
            string groupLabel = "";
            string subLabel = "";
            string apppndGroup = "";
            string apppndSub = "";
            string apppndView = "";
            float nValue = 0.0f;
            float randomValue = 0.0f;
            float digitValue = 0.0f;
            Random r = new Random();
            SpcParameter xbarData = SpcParameter.Create();
            int nIdx = 0;
            string dataView = "";

            //sia확인 : Test - Test 분석용 자료 생성.
            for (i = 0; i < lengthK; i++)
            {
                groupLabel = string.Format("ProductID-{0:D3}", i + 1);
                apppndGroup = string.Format("Group-{0:D3}", i + 1);
                for (j = 0; j < lengthJ; j++)
                {
                    subLabel = string.Format("LotyyyyMMddID-{0:D3}", j + 1);
                    apppndSub = string.Format("Sub-{0:D3}", j + 1);
                    for (n = 0; n < lengthN; n++)
                    {
                        apppndView = "";
                        if (n == 0)
                        {
                            apppndView = string.Format("//{0} - {1}", apppndGroup, apppndSub);
                        }
                        //axisXLabel = string.Format("{0,100:D8} {0,10:X8}", j, i);
                        randomValue = r.Next(1, 100);
                        digitValue = (float)(r.Next(1, 100) * 0.01);
                        nValue = randomValue + digitValue;
                        //Console.WriteLine("{0} : {1} + {2} = {3}", axisXLabel, randomValue, digitValue, nValue);
                        string rowDataAddScript = "";
                        // pdata[1, 01] = "1"; pdata[2, 01] = "CL07"; pdata[3, 01] = "LOTYYYYMMDD01"; pdata[4, 01] = "1.000"; //subgroup 1
                        nIdx++;
                        rowDataAddScript = string.Format(@"" +
                                                    @" pdata[1, {0}] = ""{1}""; " +
                                                    @" pdata[2, {0}] = ""{2}""; " +
                                                    @" pdata[3, {0}] = ""{3}""; " +
                                                    @" pdata[4, {0}] = ""{4}""; {5}"
                                                    , nIdx, "1", groupLabel, subLabel, nValue, apppndView);
                        dataView += string.Format("{0}{1}", rowDataAddScript, Environment.NewLine);
                        Console.WriteLine(rowDataAddScript);
                    }
                }
            }

            Console.WriteLine("분석용 자료 생성 종료");

            this.MemoEditDisplay.Text = dataView;
        }

        private void buttonTestData_Click(object sender, EventArgs e)
        {

        }
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자

        //public ChemicalSpcStatus()
        //{
        //    InitializeComponent();
        //}

        #endregion

        #region 툴바

        ///// <summary>
        ///// 저장버튼 클릭
        ///// </summary>
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

        ///// <summary>
        ///// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        ///// </summary>
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

        // TODO : 화면에서 사용할 내부 함수 추가

        #endregion

        private void TestSpcForm02_Load(object sender, EventArgs e)
        {
            this.cboDataType.SelectedIndex = 0;
        }

        private void btnDateTest_Click(object sender, EventArgs e)
        {
            List<SpcConditionDateDays> rtnlstDay;
            string startDate = "2020-02-01 08:30:00";
            string endDate = "2020-03-01 08:30:00";
            rtnlstDay = SpcClass.SpcConditionList(startDate, endDate);
            foreach (SpcConditionDateDays item in rtnlstDay)
            {
                Console.WriteLine(string.Format("{0} ~ {1}", item.DateStart, item.DateEnd));
            }
            Console.WriteLine("1");
        }
        
    }//end class

    
}//end namespace
