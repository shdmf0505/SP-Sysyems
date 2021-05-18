#region using

using DevExpress.XtraGrid.Views.Base;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

#endregion

namespace Micube.SmartMES.SystemManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 시스템 관리 > 사용자 관리 > 사용자 그룹 복제
    /// 업  무  설  명  : 사용자의 사용자 그룹 매핑을 복제한다.
    /// 생    성    자  : 정수현
    /// 생    성    일  : 2021-04-27
    /// 수  정  이  력  : 
    /// </summary>
    public partial class UserClassCopy : SmartConditionManualBaseForm
    {
        #region 생성자

        public UserClassCopy()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화
        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            InitializeUserGrid();
            InitializeCopyUserGrid();
            InitializeUserClassGrid();
        }

        private void InitializeUserGrid() //사용자 그리드
        {
            grdUser.GridButtonItem = GridButtonItem.None;

            grdUser.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect; //체크박스 다중선택
            grdUser.View.CheckMarkSelection.MultiSelectCount = 1;

            grdUser.View.AddTextBoxColumn("USERID", 90) //사용자ID
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();

            grdUser.View.AddTextBoxColumn("USERNAME", 120) //사용자명
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();

            grdUser.View.AddTextBoxColumn("DEPARTMENT", 120) //부서
                .SetTextAlignment(TextAlignment.Left)
                .SetIsReadOnly();

            grdUser.View.AddTextBoxColumn("POSITION", 80) //직위
               .SetTextAlignment(TextAlignment.Center)
               .SetIsReadOnly();

            grdUser.View.PopulateColumns();
        }

        private void InitializeUserClassGrid() //사용자그룹
        {
            grdUserClass.GridButtonItem = GridButtonItem.None;

            grdUserClass.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdUserClass.View.AddTextBoxColumn("USERCLASSID", 370) //사용자 그룹 ID
               .SetTextAlignment(TextAlignment.Left)
               .SetIsReadOnly();

            grdUserClass.View.AddTextBoxColumn("USERCLASSNAME", 260) //사용자 그룹명
                .SetTextAlignment(TextAlignment.Left)
                .SetIsReadOnly();

            grdUserClass.View.PopulateColumns();
        }

        private void InitializeCopyUserGrid() //복제 대상 사용자
        {
            grdCopyUser.GridButtonItem = GridButtonItem.None;

            grdCopyUser.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdCopyUser.View.AddTextBoxColumn("USERID", 90) //사용자의 MES 로그인 ID
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();

            grdCopyUser.View.AddTextBoxColumn("USERNAME", 120) //사용자명
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();

            grdCopyUser.View.AddTextBoxColumn("DEPARTMENT", 120) //부서
                .SetTextAlignment(TextAlignment.Left)
                .SetIsReadOnly();

            grdCopyUser.View.AddTextBoxColumn("POSITION", 80) //직위
               .SetTextAlignment(TextAlignment.Center)
               .SetIsReadOnly();

            grdCopyUser.View.PopulateColumns();
        }

        private void InitializeEvent()
        {
            grdUser.View.FocusedRowChanged += gridUser_View_FocusedRowChanged;
        }

        private void gridUser_View_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            GridUser_View_FocusedRowChanged();
        }

        private void GridUser_View_FocusedRowChanged()
        {
            DataRow focusedUser = grdUser.View.GetFocusedDataRow();

            var param = new Dictionary<string, object>();
            param.Add("USERID", focusedUser["USERID"].ToString());
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            grdUserClass.DataSource = SqlExecuter.Query("GetUserClassUser", "00001", param);
        }        
        #endregion

        #region 검색조건 설정
        /// <summary>
        /// 검색조건 설정
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            Conditions.AddTextBox("DEPARTMENT"); //부서
            Conditions.AddTextBox("POSITION"); //직위
        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();
            DataTable searchUserList = SqlExecuter.Query("GetUserGridUser", "00001", values);

            if (searchUserList.Rows.Count < 1)
            {
                ShowMessage("NoSelectData");
            }

            grdUser.DataSource = searchUserList;

            DataTable copyUserList = SqlExecuter.Query("GetUserGridUser", "00001");
            grdCopyUser.DataSource = copyUserList;

            if (searchUserList.Rows.Count < 1)
            {
                grdUserClass.DataSource = null;
            }
            else
            {
                grdUser.View.FocusedRowHandle = 0;
                GridUser_View_FocusedRowChanged();
            }
        }
        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            DataTable userClass = grdUserClass.View.GetCheckedRows();//COPY할 USERCLASS
            DataTable destUsers = grdCopyUser.View.GetCheckedRows();//COPY할 대상 사용자

            if (destUsers.Rows.Count == 0 || userClass.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            MessageWorker worker = new MessageWorker("SaveCopyUserClass");
            worker.SetBody(new MessageBody()
            {
                { "destUserList", destUsers}, //복제 대상 userid들
                { "userClassList", userClass} //insert해야될 classList
            });

            worker.Execute();
            GridUser_View_FocusedRowChanged();
        }
        #endregion
    }
}

