#region using
using Micube.Framework.Net;
using Micube.Framework;
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

using DevExpress.XtraCharts;
using DevExpress.Utils;
using Micube.Framework.SmartControls.Validations;
#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프로그램명 : 기준정보 > Setup > 속성 그룹
    /// 업 무 설명 : 속성 그룹 등록
    /// 생  성  자 : 윤성원
    /// 생  성  일 : 2019-06-27
    /// 수정 이 력 : 
    /// 
    /// 
    /// </summary>
    public partial class AttributeGroup : SmartConditionManualBaseForm
	{

        #region Local Variables
        #endregion

        #region 생성자
        public AttributeGroup()
		{
			InitializeComponent();
            InitializeEvent();
           
        }

        #endregion

        #region 컨텐츠 영역 초기화


        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridIdDefinitionManagement()
        {
            // MDCL GRID 초기화
            //grdMDCList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdAAGList.GridButtonItem = GridButtonItem.All;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdAAGList.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdAAGList.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdAAGList.View.AddTextBoxColumn("VALIDSTATE").SetIsHidden();
            grdAAGList.View.AddTextBoxColumn("ATTRIBUTEGROUPID", 100).SetValidationIsRequired().SetValidationKeyColumn();
            grdAAGList.View.AddTextBoxColumn("DESCRIPTION", 180).SetValidationIsRequired();
            grdAAGList.View.PopulateColumns();
            //속성
            grdAttribGList.GridButtonItem = GridButtonItem.All;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdAttribGList.View.AddTextBoxColumn("ATTRIBUTEGROUPID").SetIsHidden();
            grdAttribGList.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdAttribGList.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdAttribGList.View.AddTextBoxColumn("VALIDSTATE").SetIsHidden();
            grdAttribGList.View.AddSpinEditColumn("ATTRIBUTESEQUENCE", 150).SetIsReadOnly();

            // 팝업세팅
            InitializeGrid_CodeClassListPopup();
          
            grdAttribGList.View.AddTextBoxColumn("CODENAME", 250);
            grdAttribGList.View.AddTextBoxColumn("DESCRIPTION", 250);
            grdAttribGList.View.PopulateColumns();
        }

        private void InitializeGrid_CodeClassListPopup()
        {
            var parentCodeClassPopupColumn = this.grdAttribGList.View.AddSelectPopupColumn("CODEID", new SqlQuery("GetItemAttributeCodeClassinfo", "10001"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("CODEID", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                //.SetPopupResultMapping("CODECLASSID", "CODECLASSID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정
                //.SetPopupAutoFillColumns("CODECLASSNAME")
                // Validation 이 필요한 경우 호출할 Method 지정
                .SetPopupValidationCustom(ValidationCodeClassIdPopup);

            // 팝업에서 사용할 조회조건 항목 추가
            parentCodeClassPopupColumn.Conditions.AddTextBox("CODENAME");

            // 팝업 그리드 설정
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("CODEID", 150);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("CODENAME", 200);
        }

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGridIdDefinitionManagement();
        }

        //팝업 로우 선택후 이벤트
        private ValidationResultCommon ValidationCodeClassIdPopup(DataRow currentRow, IEnumerable<DataRow> popupSelections)
        {

            ValidationResultCommon result = new ValidationResultCommon();

            object obj = grdAttribGList.DataSource;
            DataTable dt = (DataTable)obj;

            foreach (DataRow row in popupSelections)
            {
                //코드클래스 , 코드가 동일한 내역이 존재 하는지 체크
                if (dt.Select("CODEID = '" + row["CODEID"].ToString() + "'").Length != 0)
                {
                    Language.LanguageMessageItem item = Language.GetMessage("SelectOverlap", row["CODEID"].ToString());
                    result.IsSucced = false;
                    result.FailMessage = item.Message;
                    result.Caption = item.Title;
                }
               
                currentRow["CODEID"] = row["CODEID"];
                currentRow["CODENAME"] = row["CODENAME"];

            }

            return result;
        }

        #endregion

        #region 이벤트
        /// <summary>
        /// 이벤트 초기화
        /// </summary>
      
        private void InitializeEvent()
        {
            grdAAGList.View.FocusedRowChanged += grdAAGList_FocusedRowChanged;
            grdAAGList.View.AddingNewRow += grdAAGList_AddingNewRow;
            grdAttribGList.View.AddingNewRow += grdAttribGList_AddingNewRow;
            grdAttribGList.View.ShownEditor += grdAttribGList_ShownEditor;
            grdAAGList.View.ShownEditor += grdAAGList_ShownEditor;
        }

        #region 그리드 Event

        //그리드 초기 수정 이벤트
        private void grdAAGList_ShownEditor(object sender, EventArgs e)
        {

            DataTable dt = this.grdAttribGList.GetChangedRows();
            // 품목속성 데이터 수정여부에 따라 읽기 만 가능하게 변경 
            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    ShowMessage("ChangeAttributeCheck");
                    grdAAGList.View.SetIsReadOnly(true);
                    return;
                }
            }
            else
            {
                grdAAGList.View.SetIsReadOnly(false);
            }
        }
        //그리드 초기 수정 이벤트
        private void grdAttribGList_ShownEditor(object sender, EventArgs e)
        {

            DataTable dt = this.grdAAGList.GetChangedRows();
            // 품목속성그룹 데이터 수정여부에 따라 읽기 만 가능하게 변경 
            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    ShowMessage("ChangeAttributeGroupCheck");
                    grdAttribGList.View.SetIsReadOnly(true);
                    return;
                }
            }
            else
            {
                grdAttribGList.View.SetIsReadOnly(false);
            }

        }

        // 그리드 추가 이벤트
        private void grdAAGList_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            //속성변경시 속성그룹 추가 불가 
            DataTable dt = this.grdAttribGList.GetChangedRows();
            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    sender.CancelAddNew();

                    ShowMessage("ChangeAttributeGroupCheck");
                    sender.CancelAddNew();
                    return;
                }

            }

            DataRow row = this.grdAAGList.View.GetFocusedDataRow();
            args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            args.NewRow["PLANTID"] = UserInfo.Current.Plant;
            args.NewRow["VALIDSTATE"] = "Valid";

        }

        private void grdAttribGList_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            //속성그룹에 내역이 존재 하지 않으면 추가 불가 
            if (grdAAGList.View.RowCount == 0)
            {
                sender.CancelAddNew();
                return;
            }

            //속성그룹 변경되었을 경우 속성 추가 불가
            DataTable dt = this.grdAAGList.GetChangedRows();
            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    ShowMessage("ChangeAttributeGroupCheck");
                    sender.CancelAddNew();
                    return;
                }

            }

            DataRow row = this.grdAAGList.View.GetFocusedDataRow();
            args.NewRow["ATTRIBUTEGROUPID"] = row["ATTRIBUTEGROUPID"];
            args.NewRow["ENTERPRISEID"] = row["ENTERPRISEID"];
            args.NewRow["PLANTID"] = row["PLANTID"];
            args.NewRow["VALIDSTATE"] = "Valid";

            object objMax = grdAttribGList.DataSource;
            DataTable dtMax = (DataTable)objMax;

            // 순번 증가
            if (grdAttribGList.View.RowCount != 1)
            {
                args.NewRow["ATTRIBUTESEQUENCE"] = int.Parse(dtMax.Compute("MAX(ATTRIBUTESEQUENCE)", "").ToString()) + 1;
            }
            else
            {
                args.NewRow["ATTRIBUTESEQUENCE"] = 1;
            }
        }
        //현재 로우 선택 이벤트
        private void grdAAGList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataTable dt = this.grdAttribGList.GetChangedRows();

            // 속성변경시 속성그룹을 쓰기 불가
            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    ShowMessage("ChangeAttributeCheck");
                    grdAAGList.View.SetIsReadOnly(true);
                    return;
                }
            }
            else
            {
                grdAAGList.View.SetIsReadOnly(false);
            }

            FocusedRowChanged();
        }
        // 속성 조회
        private void FocusedRowChanged()
        {
            grdAttribGList.View.SetIsReadOnly(false);
            if (grdAAGList.View.FocusedRowHandle < 0)
                return;
            var values = Conditions.GetValues();
            DataRow row = this.grdAAGList.View.GetFocusedDataRow();
            values.Add("ATTRIBUTEGROUPID", row["ATTRIBUTEGROUPID"].ToString());
            values.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());
            DataTable dtAttrib = SqlExecuter.Query("GetAttributeList", "10001", values);
            this.grdAttribGList.DataSource = dtAttrib;
        }
        #endregion

        #endregion

        #region 툴바
        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            DataTable changed = new DataTable();
            DataTable changed1 = new DataTable();

            changed = grdAAGList.GetChangedRows();
            ExecuteRule("AttributeGroup", changed);
            changed1 = grdAttribGList.GetChangedRows();
            ExecuteRule("Attributes", changed1);

            grdAAGList.View.SetIsReadOnly(false);
            grdAttribGList.View.SetIsReadOnly(false);

        }
        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
		{
			await base.OnSearchAsync();

            // 조회조건에 입력된 값들을 Dictionary<string, object> Type으로 가져옴

            var values = this.Conditions.GetValues();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            //TODO : Id를 수정하세요            
            // Stored Procedure 호출
            //this.grdCodeClass.DataSource = await this.ProcedureAsync("usp_com_selectCodeClass", values);
            // Server Xml Query 호출

            DataTable dtAAGList = await SqlExecuter.QueryAsync("GetAttributeGroupList", "10001", values);

            if (dtAAGList.Rows.Count < 1) // 
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            this.grdAAGList.DataSource = dtAAGList;
        }
        #endregion

        #region 유효성 검사
        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            DataTable changed = new DataTable();
            grdAAGList.View.CheckValidation();
            changed = grdAAGList.GetChangedRows();

            DataTable changed1 = new DataTable();
            grdAttribGList.View.CheckValidation();
            changed1 = grdAttribGList.GetChangedRows();


            if (changed.Rows.Count == 0 && changed1.Rows.Count == 0)
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
