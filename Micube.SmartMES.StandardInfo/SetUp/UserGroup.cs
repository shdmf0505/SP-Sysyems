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
    /// 프 로 그 램 명  : 기준정보 > 사용자그룹 정보
    /// 업  무  설  명  : 사용자그룹 정보를 관리한다.
    /// 생    성    자  : 장선미
    /// 생    성    일  : 2020-01-02
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class UserGroup : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public UserGroup()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGrid();

            InitializeEvent();
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdUserGroup.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdUserGroup.View.SetSortOrder("USERGROUPID");

            grdUserGroup.View.AddTextBoxColumn("USERGROUPID", 150)
                .SetValidationKeyColumn()
                .SetValidationIsRequired();

            grdUserGroup.View.AddLanguageColumn("USERGROUPNAME", 200);
            //grdUserClass.View.AddTextBoxColumn("KUSERCLASSNAME",200);
            //grdUserClass.View.AddTextBoxColumn("EUSERCLASSNAME",200);
            //grdUserClass.View.AddTextBoxColumn("CUSERCLASSNAME",200);
            //grdUserClass.View.AddTextBoxColumn("LUSERCLASSNAME",200);

            grdUserGroup.View.AddTextBoxColumn("DESCRIPTION", 200);

            grdUserGroup.View.AddComboBoxColumn("VALIDSTATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdUserGroup.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdUserGroup.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH;mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdUserGroup.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdUserGroup.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH;mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdUserGroup.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {

        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();
            
            DataTable changed = grdUserGroup.GetChangedRows();

            ExecuteRule("SaveUserGroup", changed);
        }

        #endregion

        #region 검색
        
        //// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            var values = Conditions.GetValues();
            await base.OnSearchAsync();
            //DataTable dtUserClass = await ProcedureAsync("usp_com_selectUserClass", values);
            DataTable dtUserClass = await QueryAsync("SelectUserGroup", "10001", values);

            if (dtUserClass.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            grdUserGroup.DataSource = dtUserClass;
        }
        
        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            grdUserGroup.View.CheckValidation();

            DataTable changed = grdUserGroup.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        #endregion
    }
}
