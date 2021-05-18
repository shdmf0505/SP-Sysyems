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
    /// 프 로 그 램 명  : 시스템 관리 > 다국어 관리 > 메세지그룹 정보
    /// 업  무  설  명  : 메세지그룹 정보를 관리한다.
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2019-04-01
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class MessageClass : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public MessageClass()
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
        /// 초기화
        /// </summary>
        private void InitializeGrid()
        {
            //TODO : 그리드를 초기화 하세요
            grdMessageClass.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdMessageClass.View.SetSortOrder("MESSAGECLASSID");

            grdMessageClass.View.AddTextBoxColumn("MESSAGECLASSID", 150)
                .SetValidationKeyColumn()
                .SetValidationIsRequired();

            grdMessageClass.View.AddLanguageColumn("MESSAGECLASSNAME", 200);
            //grdDictionaryMessageClass.View.AddTextBoxColumn("KMESSAGECLASSNAME", 200);
            //grdDictionaryMessageClass.View.AddTextBoxColumn("EMESSAGECLASSNAME", 200);
            //grdDictionaryMessageClass.View.AddTextBoxColumn("CMESSAGECLASSNAME", 200);
            //grdDictionaryMessageClass.View.AddTextBoxColumn("LMESSAGECLASSNAME", 200);

            grdMessageClass.View.AddTextBoxColumn("DESCRIPTION", 200);

            grdMessageClass.View.AddComboBoxColumn("VALIDSTATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center)
                .SetDefault("Valid")
                .SetValidationIsRequired();

            grdMessageClass.View.AddTextBoxColumn("CREATOR", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            grdMessageClass.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdMessageClass.View.AddTextBoxColumn("MODIFIER", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            grdMessageClass.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");

            grdMessageClass.View.PopulateColumns();
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
            DataTable changed = grdMessageClass.GetChangedRows();

            ExecuteRule("SaveMessageClass", changed);
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

            //DataTable dtResult = await SqlExecuter.ProcedureAsync("usp_com_selectMessageClassList", values);
            DataTable dtResult = await QueryAsync("SelectMessageClass", "10001", values);

            if (dtResult.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdMessageClass.DataSource = dtResult;
        }
        
        #endregion

        #region 유효성 검사

        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            grdMessageClass.View.CheckValidation();

            DataTable changed = grdMessageClass.GetChangedRows();

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