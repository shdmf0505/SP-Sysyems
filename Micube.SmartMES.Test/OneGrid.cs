using Micube.Framework.Net;
using Micube.Framework;
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

using DevExpress.XtraCharts;
using DevExpress.Utils;
using Micube.Framework.SmartControls.Validations;
using Micube.SmartMES.Commons.Controls;

namespace Micube.SmartMES.Test
{
    public partial class OneGrid : SmartConditionManualBaseForm
    {
        public OneGrid()
        {
            InitializeComponent();
        }

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        /// <returns></returns>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            //TODO :  RuleName을 수정하세요 (저장기능이 없다면 현재 함수를 삭제하세요.)
            // 그리드에 수정된 행을 DataTable Type으로 가져옴
            DataTable changed = grdCodeClass.GetChangedRows();

            // 서버 Rule 호출
            this.ExecuteRule("SaveCodeClass", changed);
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

            // 조회조건에 입력된 값들을 Dictionary<string, object> Type으로 가져옴
            var values = this.Conditions.GetValues();

            //TODO : Id를 수정하세요            
            // Stored Procedure 호출
            this.grdCodeClass.DataSource = await this.ProcedureAsync("usp_com_selectCodeClass", values);
            // Server Xml Query 호출
            this.grdCodeClass.DataSource = await SqlExecuter.QueryAsync("GetCodeClassList", "00001", values);
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            DataTable changed = grdCodeClass.GetChangedRows();

            // 수정된 내용이 없는 경우 메시지 처리
            if (changed.Rows.Count == 0)
                throw MessageException.Create("NoSaveData"); //저장할 데이터 없음

            //TODO : 그리드의 유효성 검사
            // 그리드 데이터 Validation 체크
            grdCodeClass.View.CheckValidation();
        }

        #endregion

        #region 컨텐츠 영역 초기화
        /// <summary>
        /// 우측 컨텐츠 영역에 초기화할 코드를 넣으세요.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGrid();
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            this.grdCodeClass.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            // 키 컬럼 지정
            this.grdCodeClass.View.SetKeyColumn("CODECLASSID");

            this.grdCodeClass.View.AddTextBoxColumn("CODECLASSID", 150);
            this.grdCodeClass.View.AddTextBoxColumn("CODECLASSNAME", 200);
            this.grdCodeClass.View.AddTextBoxColumn("DESCRIPTION", 200);
            this.grdCodeClass.View.AddComboBoxColumn("CODECLASSTYPE", new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSTYPE=CodeClassType"))
                // 필수 입력 컬럼 지정
                .SetValidationIsRequired()
                // 행추가 시 기본값 지정
                .SetDefault("User")
                // 데이터 텍스트 정렬 위치 지정
                .SetTextAlignment(TextAlignment.Center);
            this.InitializeGrid_ParentCodeClassListPopup();
            this.grdCodeClass.View.AddComboBoxColumn("VALIDSTATE", 80, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSTYPE=ValidState"))
                .SetValidationIsRequired()
                .SetDefault("Valid")
                .SetTextAlignment(TextAlignment.Center);

            this.grdCodeClass.View.AddTextBoxColumn("CREATOR", 80)
                // ReadOnly 컬럼 지정
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            this.grdCodeClass.View.AddTextBoxColumn("CREATEDTIME", 130)
                // Display Format 지정
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            this.grdCodeClass.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            this.grdCodeClass.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            this.grdCodeClass.View.PopulateColumns();
        }

        private void InitializeGrid_ParentCodeClassListPopup()
        {
            var parentCodeClassPopupColumn = this.grdCodeClass.View.AddSelectPopupColumn("PARENTCODECLASSID", new SqlQuery("GetParentCodeClassList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("SELECTPARENTCODECLASSID", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("PARENTCODECLASSID", "CODECLASSID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정
                .SetPopupAutoFillColumns("CODECLASSNAME")
                // Validation 이 필요한 경우 호출할 Method 지정
                .SetPopupValidationCustom(ValidationParentCodeClassIdPopup);

            // 팝업에서 사용할 조회조건 항목 추가
            parentCodeClassPopupColumn.Conditions.AddTextBox("TXTCODECLASSIDNAME");
            parentCodeClassPopupColumn.Conditions.AddTextBox("CODECLASSID")
                // 기본값 지정 (그리드의 컬럼 Id 지정)
                .SetPopupDefaultByGridColumnId("CODECLASSID")
                // 숨김 처리
                .SetIsHidden();

            // 팝업 그리드 설정
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("CODECLASSID", 150)
                .SetValidationKeyColumn();
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("CODECLASSNAME", 200);
        }

        private ValidationResultCommon ValidationParentCodeClassIdPopup(DataRow currentRow, IEnumerable<DataRow> popupSelections)
        {
            ValidationResultCommon result = new ValidationResultCommon();

            string myCodeClassId = currentRow.ToStringNullToEmpty("CODECLASSID");
            if (popupSelections.Any(s => s.ToStringNullToEmpty("CODECLASSID") == myCodeClassId))
            {
                this.ShowMessage("NoSameParentCodeClass");
            }

            return result;
        }

        #endregion
    }
}
