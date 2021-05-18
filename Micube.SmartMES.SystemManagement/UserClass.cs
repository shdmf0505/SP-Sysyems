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
    /// 프 로 그 램 명  : 시스템 관리 > 사용자 관리 > 사용자그룹 정보
    /// 업  무  설  명  : 사용자그룹 정보를 관리한다.
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2019-04-01
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class UserClass : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public UserClass()
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
            grdUserClass.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdUserClass.View.SetSortOrder("USERCLASSID");

            grdUserClass.View.AddTextBoxColumn("USERCLASSID", 150)
                .SetValidationKeyColumn()
                .SetValidationIsRequired();

            grdUserClass.View.AddLanguageColumn("USERCLASSNAME", 200);
            //grdUserClass.View.AddTextBoxColumn("KUSERCLASSNAME",200);
            //grdUserClass.View.AddTextBoxColumn("EUSERCLASSNAME",200);
            //grdUserClass.View.AddTextBoxColumn("CUSERCLASSNAME",200);
            //grdUserClass.View.AddTextBoxColumn("LUSERCLASSNAME",200);

            grdUserClass.View.AddTextBoxColumn("DESCRIPTION", 200);

            //smjang - audit 여부 컬럼 추가
            grdUserClass.View.AddComboBoxColumn("ISAUDIT", 70, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center);


            grdUserClass.View.AddComboBoxColumn("VALIDSTATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdUserClass.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdUserClass.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH;mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdUserClass.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdUserClass.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH;mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdUserClass.View.PopulateColumns();
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
            
            DataTable changed = grdUserClass.GetChangedRows();

            ExecuteRule("SaveUserClass", changed);
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
            //smjang - 버전 변경 (ISAUDIT 추가)
            //DataTable dtUserClass = await QueryAsync("SelectUserClass", "10001", values);
            DataTable dtUserClass = await QueryAsync("SelectUserClass", "10002", values);

            if (dtUserClass.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            grdUserClass.DataSource = dtUserClass;
        }
        
        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            grdUserClass.View.CheckValidation();

            DataTable changed = grdUserClass.GetChangedRows();

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