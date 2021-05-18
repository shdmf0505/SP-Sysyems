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
    /// 프 로 그 램 명  : 시스템 관리 > 코드 관리 > 코드그룹 정보
    /// 업  무  설  명  : 코드그룹 정보를 관리한다.
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2019-04-01
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class CodeClass : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자

        public CodeClass()
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

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGridCodeClass();
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGridCodeClass()
        {
            // TODO : 그리드 초기화 로직 추가
            grdCodeClass.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdCodeClass.View.SetSortOrder("CODECLASSID");

            // 코드그룹ID
            grdCodeClass.View.AddTextBoxColumn("CODECLASSID", 150)
                .SetValidationKeyColumn()
                .SetValidationIsRequired();
            // 코드그룹명
            grdCodeClass.View.AddLanguageColumn("CODECLASSNAME", 200);
            // 설명
            grdCodeClass.View.AddTextBoxColumn("DESCRIPTION", 200);
            // 코드그룹타입
            grdCodeClass.View.AddComboBoxColumn("CODECLASSTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=CodeClassType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetValidationIsRequired()
                .SetDefault("System")
                .SetTextAlignment(TextAlignment.Center);
            // 상위코드그룹ID
            InitializeGrid_ParentCodeClassListPopup();
            // 유효상태
            grdCodeClass.View.AddComboBoxColumn("VALIDSTATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);
            // 생성자
            grdCodeClass.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            // 생성일
            grdCodeClass.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            // 수정자
            grdCodeClass.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            // 수정일
            grdCodeClass.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdCodeClass.View.PopulateColumns();
        }

        /// <summary>
        /// 상위코드그룹 선택 팝업 컬럼을 초기화한다.
        /// </summary>
        private void InitializeGrid_ParentCodeClassListPopup()
        {
            // 팝업 컬럼 설정
            //var parentEquipmentPopupColumn = grdCodeClass.View.AddSelectPopupColumn("PARENTCODECLASSID", new SqlQuery("GetParentCodeClassId", "00001", $"CODECLASSID ={CurrentDataRow("CODECLASSID")}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            var parentCodeClassPopupColumn = grdCodeClass.View.AddSelectPopupColumn("PARENTCODECLASSID", new SqlQuery("GetParentCodeClassId", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                // .SetRelationIds("PLANT")
                .SetPopupLayout("SELECTPARENTCODECLASSID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupResultMapping("PARENTCODECLASSID", "CODECLASSID")
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("CODECLASSNAME")
                .SetPopupValidationCustom(ValidationParentCodeClassIdPopup);

            // 코드그룹ID/명
            parentCodeClassPopupColumn.Conditions.AddTextBox("TXTCODECLASSIDNAME");

            // 코드그룹ID
            parentCodeClassPopupColumn.Conditions.AddTextBox("CODECLASSID")
                .SetPopupDefaultByGridColumnId("CODECLASSID")
                .SetIsHidden();

            // 코드그룹ID
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("CODECLASSID", 100)
                .SetValidationKeyColumn();
            // 코드그룹명
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("CODECLASSNAME", 170);
        }

        #endregion

        #region Event

        /// <summary>        
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            DataTable changed = grdCodeClass.GetChangedRows();

            ExecuteRule("SaveCodeClass", changed);
        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            //DataTable dtCodeClass = await ProcedureAsync("usp_com_selectCodeClass", values);
            DataTable dtCodeClass = await QueryAsync("SelectCodeClass", "00001", values);

            if (dtCodeClass.Rows.Count < 1)
            {
                // 조회할 데이터가 없습니다.
                ShowMessage("NoSelectData");
            }

            grdCodeClass.DataSource = dtCodeClass;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            grdCodeClass.View.CheckValidation();

            DataTable changed = grdCodeClass.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 팝업에서 항목 선택 시 자신과 같은 코드그룹은 선택 할 수 없도록 유효성을 검사한다.
        /// </summary>
        /// <param name="currentGridRow"></param>
        /// <param name="popupSelections"></param>
        /// <returns></returns>
        private Micube.Framework.SmartControls.Validations.ValidationResultCommon ValidationParentCodeClassIdPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            Micube.Framework.SmartControls.Validations.ValidationResultCommon result = new Framework.SmartControls.Validations.ValidationResultCommon();

            string myCodeClassId = currentGridRow.ToStringNullToEmpty("CODECLASSID");
            if (popupSelections.Any(s => s.ToStringNullToEmpty("CODECLASSID") == myCodeClassId))
            {
                result.IsSucced = false;
                result.FailMessage = Language.GetMessage("NoSameParentCodeClass").Message;
                result.Caption = Language.GetMessage("NoSameParentCodeClass").Title;
                //ShowMessage("NoSameParentCodeClass");
            }

            return result;
        }

        #endregion
    }
}