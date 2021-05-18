#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > Setup >  고객 Import 및 조회
    /// 업  무  설  명  : 고객정보를 Import및 관리한다.
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-05-17
    /// 수  정  이  력  : 윤성원 2019-07-05 using 에 #region #endregion 추가
    /// 
    /// 
    /// </summary>
    public partial class CustomerManagement : SmartConditionManualBaseForm
	{
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자

        public CustomerManagement()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            this.grdCustomer.GridButtonItem = GridButtonItem.Export;
            this.grdCustomer.View.SetIsReadOnly();

            //공장
            this.grdCustomer.View.AddTextBoxColumn("PLANTID", 150);

            //사업자 번호
            this.grdCustomer.View.AddTextBoxColumn("BIZNO", 150)
                            .SetLabel("BUSINESSNUMBER");

            //거래처 ID
            this.grdCustomer.View.AddTextBoxColumn("CUSTOMERID", 80);

            //MES 고객명
            this.grdCustomer.View.AddTextBoxColumn("CUSTOMERNAME", 150)
                            .SetLabel("MESCUSTOMERNAME");

            //Site별 고객명
            this.grdCustomer.View.AddTextBoxColumn("SITECUSTOMERNAME", 200);

            //설명
            this.grdCustomer.View.AddTextBoxColumn("DESCRIPTION", 150);

            //주소
            this.grdCustomer.View.AddTextBoxColumn("ADDRESS", 150);

            //대표자 명
            this.grdCustomer.View.AddTextBoxColumn("CEONAME", 150);

            //전화번호
            this.grdCustomer.View.AddTextBoxColumn("TELNO", 150)
                            .SetLabel("TELNUMBER");

            //Fax 번호
            this.grdCustomer.View.AddTextBoxColumn("FAXNO", 150)
                            .SetLabel("FAXNUMBER");

            //Ship To
            this.grdCustomer.View.AddTextBoxColumn("SHIPTO", 150);

            //Bill To
            this.grdCustomer.View.AddTextBoxColumn("BILLTO", 150);

            //유효상태
            this.grdCustomer.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                            .SetDefault("Valid")
                            .SetValidationIsRequired()
                            .SetTextAlignment(TextAlignment.Center);

            //생성자
            this.grdCustomer.View.AddTextBoxColumn("CREATOR", 80)
                            .SetIsReadOnly()
                            .SetTextAlignment(TextAlignment.Center);

            //생성 일자
            this.grdCustomer.View.AddTextBoxColumn("CREATEDTIME", 130)
                            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                            .SetIsReadOnly()
                            .SetTextAlignment(TextAlignment.Center);

            //수정자
            this.grdCustomer.View.AddTextBoxColumn("MODIFIER", 80)
                            .SetIsReadOnly()
                            .SetTextAlignment(TextAlignment.Center);

            //수정 일자
            this.grdCustomer.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                            .SetIsReadOnly()
                            .SetTextAlignment(TextAlignment.Center);

            this.grdCustomer.View.PopulateColumns();

        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            //Import 버튼 클릭시 이벤트
            this.btnImport.Click += BtnImport_Click;
        }

        /// <summary>
        /// Import 버튼 클릭시 이벤트 - 구현 암함
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnImport_Click(object sender, EventArgs e)
        {
            this.ShowMessage("Import!");
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            DataTable changed = grdCustomer.GetChangedRows();

            ExecuteRule("RULEID", changed);

        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
            values.Add("p_languagetype", Framework.UserInfo.Current.LanguageType);

            DataTable dt = await SqlExecuter.QueryAsync("SelectCustomerManagement", "10001", values);

            if (dt.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdCustomer.DataSource = dt;
        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
        }

        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            grdCustomer.View.CheckValidation();

            DataTable changed = grdCustomer.GetChangedRows();
            
            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가

        #endregion

    }
}
