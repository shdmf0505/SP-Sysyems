#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.SystemManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 시스템 관리 > 사용자 관리 > 사용자 정보
    /// 업  무  설  명  : 사용자 정보를 관리한다.
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2019-04-01
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class User : SmartConditionManualBaseForm
    {
        #region Local Variables

        private Random _randomSeed = new Random();

        #endregion

        #region 생성자

        public User()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화
        /// <summary>
        /// 우측 컨텐츠 영역에 초기화할 코드를 넣으세요.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            InitializeGrid();
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            //TODO : 그리드를 초기화 하세요
            grdUser.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdUser.View.SetSortOrder("USERID");

            grdUser.View.AddTextBoxColumn("USERID", 90)
                .SetValidationKeyColumn();
            //.SetValidationIsRequired()


            grdUser.View.AddTextBoxColumn("USERNAME", 100);
            grdUser.View.AddTextBoxColumn("DESCRIPTION", 200);
            grdUser.View.AddTextBoxColumn("PASSWORD", 150)
                .SetIsReadOnly()
                .SetIsHidden();
            grdUser.View.AddComboBoxColumn("PLANTID", 70, new SqlQuery("GetPlantList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                //.SetEmptyItem()
                .SetDefault(UserInfo.Current.Plant)
                .SetTextAlignment(TextAlignment.Center)
                .SetValidationIsRequired();
            //grdUser.View.AddTextBoxColumn("AREAID", 100);
            grdUser.View.AddTextBoxColumn("NICKNAME", 100);
            grdUser.View.AddTextBoxColumn("DEPARTMENT", 150);
            grdUser.View.AddTextBoxColumn("POSITION", 70);
            grdUser.View.AddTextBoxColumn("DUTY", 70);
            grdUser.View.AddTextBoxColumn("EMAILADDRESS", 150);
                //.SetValidationIsRequired();
            grdUser.View.AddTextBoxColumn("CELLPHONENUMBER", 100);
            grdUser.View.AddComboBoxColumn("DEFAULTLANGUAGETYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=LanguageType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetValidationIsRequired();
            grdUser.View.AddComboBoxColumn("VALIDSTATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);
            grdUser.View.AddComboBoxColumn("USERSTATE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=UserState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Create")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdUser.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdUser.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetIsReadOnly()
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center);
            grdUser.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdUser.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetIsReadOnly()
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center);

            grdUser.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            btnChangePassword.Click += BtnChangePassword_Click;
        }

        private void BtnChangePassword_Click(object sender, EventArgs e)
        {
            DataTable user = grdUser.View.GetCheckedRows();

            if (user.Rows.Count == 0)
            {
                ShowMessage("GridNoChecked");
                return;
            }

            if (user.Rows.Count > 1)
            {
                // 비밀번호를 변경할 사용자를 한 명만 선택하시기 바랍니다.
                ShowMessage("SelectOneUserForChangePassword");
                return;
            }

            ChangePassword popup = new ChangePassword();
            popup.ShowDialog();

            if (popup.DialogResult == DialogResult.OK)
            {
                string changePassword = popup.GetChangePassword();

                user.AsEnumerable().ForEach(r =>
                {
                    r["PASSWORD"] = changePassword;
                    r["USERSTATE"] = "Forgot";
                });

                ExecuteRule("InitPassword", user);

                ShowMessage("ChangePasswordSuccess");
            }
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        /// <returns></returns>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            //TODO :  RuleName을 수정하세요 (저장기능이 없다면 현재 함수를 삭제하세요.)
            DataTable changed = grdUser.GetChangedRows();

            foreach (DataRow row in changed.Rows)
            {
                if (row.Field<string>("_STATE_") == "added")
                {
                    row["PASSWORD"] = GetRandomPassword(8);
                    //row["USERSTATE"] = "Create";
                }
            }

            ExecuteRule("SaveUser", changed);
        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델, 비동기 모델은 검색에서만 제공합니다. ESC키로 취소 가능합니다.
        /// </summary>
        /// <returns></returns>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();

            //TODO : Id를 수정하세요            
            //DataTable dtUser = await ProcedureAsync("usp_com_selectUser", values);
            DataTable dtUser = await QueryAsync("SelectUser", "10001", values);

            if (dtUser.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            grdUser.DataSource = dtUser;
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            //TODO : 그리드의 유효성 검사
            grdUser.View.CheckValidation();

            DataTable changed = grdUser.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            foreach (DataRow row in changed.Rows)
            {
                if (row.Field<string>("_STATE_") == "added" || row.Field<string>("_STATE_") == "modified")
                {
                    if (string.IsNullOrEmpty(row["USERID"].ToString()))
                        throw MessageException.Create("RequireColumn", Language.Get("USERID"));

                    //if (string.IsNullOrEmpty(row["EMAILADDRESS"].ToString()))
                    //    throw MessageException.Create("RequireColumn", Language.Get("EMAILADDRESS"));

                    if (string.IsNullOrEmpty(row["DEFAULTLANGUAGETYPE"].ToString()))
                        throw MessageException.Create("RequireColumn", Language.Get("DEFAULTLANGUAGETYPE"));
                }
            }
        }

        #endregion

        #region Private Function

        private string GetRandomPassword(int passwordLength)
        {
            Random random = new Random(_randomSeed.Next());
            string upperCase = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string lowerCase = @"abcdefghijklmnopqrstuvwxyz";
            string number = @"0123456789";

            string allCharacters = upperCase + lowerCase + number;

            string temp = System.IO.Path.GetRandomFileName();

            string password = "";

            for (int i = 0; i < passwordLength; i++)
            {
                double rand = random.NextDouble();

                password += allCharacters.ToCharArray()[(int)Math.Floor(rand * allCharacters.Length)];
            }

            return password;
        }

        #endregion
    }
}