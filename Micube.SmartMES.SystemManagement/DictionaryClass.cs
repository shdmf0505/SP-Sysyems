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
    /// 프 로 그 램 명  : 시스템 관리 > 다국어 관리 > 사전그룹 정보
    /// 업  무  설  명  : 사전그룹 정보를 관리한다.
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2019-04-01
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class DictionaryClass : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public DictionaryClass()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            InitializeGrid();
        }

        /// <summary>
        /// 사전그룹 리스트 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            //TODO : 그리드를 초기화 하세요
            grdDictionaryClass.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdDictionaryClass.View.SetSortOrder("DICTIONARYCLASSID");

            grdDictionaryClass.View.AddTextBoxColumn("DICTIONARYCLASSID", 150)
                .SetValidationKeyColumn()
                .SetValidationIsRequired();

            grdDictionaryClass.View.AddLanguageColumn("DICTIONARYCLASSNAME", 200);

            grdDictionaryClass.View.AddTextBoxColumn("DESCRIPTION", 200);

            grdDictionaryClass.View.AddComboBoxColumn("VALIDSTATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center)
                .SetDefault("Valid")
                .SetValidationIsRequired();

            grdDictionaryClass.View.AddTextBoxColumn("CREATOR", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            grdDictionaryClass.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly();
            grdDictionaryClass.View.AddTextBoxColumn("MODIFIER", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            grdDictionaryClass.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly();

            grdDictionaryClass.View.PopulateColumns();

        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {

        }

        #endregion

        #region 툴바

        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            //TODO :  RuleName을 수정하세요 (저장기능이 없다면 현재 함수를 삭제하세요.)
            DataTable changed = grdDictionaryClass.GetChangedRows();

            ExecuteRule("SaveDictionaryClass", changed);
        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();

            //DataTable dtResult = await ProcedureAsync("usp_com_selectDictionaryClassList", values);
            DataTable dtResult = await QueryAsync("SelectDictionaryClass", "10001", values);

            if (dtResult.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdDictionaryClass.DataSource = dtResult;
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            grdDictionaryClass.View.CheckValidation();

            DataTable changed = grdDictionaryClass.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                throw MessageException.Create("NoSaveData"); //저장할 데이터 없음
            }
        }

        #endregion

        #region Private Function

        #endregion
    }
}
