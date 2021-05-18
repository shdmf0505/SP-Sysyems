using Micube.Framework.SmartControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Micube.SmartMES.Test
{
    public partial class TestUser : SmartConditionBaseForm
    {
        public TestUser()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGrid();
        }

        private void InitializeGrid()
        {
            // AddTextBoxColumn Grid에 TextBox 형식의 Control을 생성
            //      AddTextBoxColumn("생성할Key", 가로 Size);
            grdMain.View.AddTextBoxColumn("USERID", 100);
            grdMain.View.AddTextBoxColumn("PLANTID", 100);
            grdMain.View.AddTextBoxColumn("ENTERPRISEID", 100);
            grdMain.View.AddTextBoxColumn("USERNAME", 100);
            grdMain.View.AddTextBoxColumn("DEPARTMENT", 100);
            grdMain.View.AddTextBoxColumn("DUTY", 100);
            grdMain.View.AddTextBoxColumn("HOMEADDRESS", 250);
            grdMain.View.AddTextBoxColumn("CELLPHONENUMBER", 100);

            grdMain.View.PopulateColumns();
        }

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            Dictionary<string, object> param = Conditions.GetValues();

            DataTable dtList = await QueryAsync("SelectUserList", "10001", Conditions.GetValues());

            grdMain.DataSource = dtList;
        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            Conditions.AddTextBox("USERID").SetPosition(1.1);
            Conditions.AddTextBox("USERNAME").SetPosition(1.2);
        }

        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            ((SmartTextBox)Conditions.GetControl("USERNAME")).ReadOnly = true;
        }
    }
}
