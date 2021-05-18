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
    /// 프로그램명 : 기준정보 > Setup > 품목규칙 등록
    /// 업 무 설명 : 품목 코드,명,계산식 등록
    /// 생  성  자 : 윤성원
    /// 생  성  일 : 2019-06-27
    /// 수정 이 력 : 
    /// </summary> 
	public partial class RuleDefinition : SmartConditionManualBaseForm
	{
        #region Local Variables
        #endregion

        #region 생성자
        public RuleDefinition()
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
            
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // 규칙 구성 그리드 초기화
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            grdRuleDefinitionList.GridButtonItem = GridButtonItem.All;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdRuleDefinitionList.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdRuleDefinitionList.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdRuleDefinitionList.View.AddTextBoxColumn("VALIDSTATE").SetIsHidden();
            grdRuleDefinitionList.View.AddTextBoxColumn("RULESET").SetIsHidden();
            
            grdRuleDefinitionList.View.AddTextBoxColumn("RULEID", 100).SetValidationIsRequired().SetValidationKeyColumn();
            grdRuleDefinitionList.View.AddTextBoxColumn("RULENAME", 180).SetValidationIsRequired();
            grdRuleDefinitionList.View.AddComboBoxColumn("TARGETATTRIBUTE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=TargetAttribute", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetEmptyItem("", "", true);
            grdRuleDefinitionList.View.AddComboBoxColumn("RULETYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=RuleType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetEmptyItem("", "", true);
            grdRuleDefinitionList.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetEmptyItem("", "", true);
            grdRuleDefinitionList.View.PopulateColumns();

            
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // 규칙 그리드 초기화
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            grdRuleFormula.GridButtonItem = GridButtonItem.All;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdRuleFormula.View.AddTextBoxColumn("RULEID").SetIsHidden();
            grdRuleFormula.View.AddTextBoxColumn("VALIDSTATE").SetIsHidden();
            grdRuleFormula.View.AddSpinEditColumn("SEQUENCE", 150).SetIsReadOnly();
            InitializeGrid_CodeClassListPopup();
            grdRuleFormula.View.AddTextBoxColumn("CODENAME", 250);
            grdRuleFormula.View.AddTextBoxColumn("FRONTBRACKET", 150);
            grdRuleFormula.View.AddComboBoxColumn("OPERATOR", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Operator", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetEmptyItem("", "", true);
            grdRuleFormula.View.AddTextBoxColumn("BACKBRACKET", 150);
            grdRuleFormula.View.AddComboBoxColumn("SEPARATORCODE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=SeparatorCode", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetEmptyItem("", "", true);
            grdRuleFormula.View.PopulateColumns();
        }

        // 컬럼이나 숫자 코드관리에서 데이터를 가져온다.
        private void InitializeGrid_CodeClassListPopup()
        {
            var parentCodeClassPopupColumn = this.grdRuleFormula.View.AddSelectPopupColumn("CODEID", new SqlQuery("GetCodeList", "00001", "CODECLASSID=ItemSpecAttribute", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("ATTRIBUTE", PopupButtonStyles.Ok_Cancel, true, false)
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
            parentCodeClassPopupColumn.Conditions.AddTextBox("CODEID");

            // 팝업 그리드 설정
            //parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("CODECLASSID", 150);
            //parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("CODECLASSNAME", 200);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("CODEID", 150);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("CODENAME", 200);
        }

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGridIdDefinitionManagement();
        }

        //계산식이나 명 규칙에 필요한 컬럼과 값을 코드관리 에서 가져와 등록한다.  
        private ValidationResultCommon ValidationCodeClassIdPopup(DataRow currentRow, IEnumerable<DataRow> popupSelections)
        {

            ValidationResultCommon result = new ValidationResultCommon();
            //string myCodeClassId = currentRow.ToStringNullToEmpty("CODECLASSID");

            object obj = grdRuleFormula.DataSource;
            DataTable dt = (DataTable)obj;

            foreach (DataRow row in popupSelections)
            {
                //컬럼이 두개 이상인 경우 오류 메시지 
                if (dt.Select("CODEID = '" + row["CODEID"].ToString() + "'").Length != 0)
                {
                    Language.LanguageMessageItem item = Language.GetMessage("SelectOverlap", row["CODENAME"].ToString());
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

        private void InitializeEvent()
        {
            // 품목 규칙 대상 그리드 이벤트
            grdRuleDefinitionList.View.FocusedRowChanged += grdRuleDefinitionList_FocusedRowChanged;
            grdRuleDefinitionList.View.AddingNewRow += grdRuleDefinitionList_AddingNewRow;
            // 대상에 대한 규칙 그리드 이벤트
            grdRuleFormula.View.AddingNewRow += grdRuleFormula_AddingNewRow;
            //저장 버튼 
            btnRDSave.Click += BtnRDSave_Click;
            btnRSave.Click += BtnRSave_Click;

        }

        // 룰 타겟 저장
        private void BtnRSave_Click(object sender, EventArgs e)
        {
            DataTable changed = this.grdRuleFormula.GetChangedRows();

            if (changed != null)
            {
                if (changed.Rows.Count == 0)
                {
                    ShowMessage("ChangeMasterDataClassCheck");
                }
            }
            else
            {
                ShowMessage("ChangeMasterDataClassCheck");
            }
            grdRuleFormula.View.CheckValidation();
            ExecuteRule("RuleFormula", changed);
            ShowMessage("SuccedSave");
        }
        // 룰 대상 저장
        private void BtnRDSave_Click(object sender, EventArgs e)
        {

            DataTable changed = this.grdRuleDefinitionList.GetChangedRows();

            if (changed != null)
            {
                if (changed.Rows.Count == 0)
                {
                    ShowMessage("ChangeMasterDataClassCheck");
                }
            }
            else
            {
                ShowMessage("ChangeMasterDataClassCheck");
            }

            
            grdRuleDefinitionList.View.CheckValidation();
            ExecuteRule("RuleDefinition", changed);
            ShowMessage("SuccedSave");

         }

       

       
        // 대상에 필요한 계산식이나 명에 따른 규칙을 추가시 추가 하가너 제어 한다.
        private void grdRuleFormula_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {

            object objRuleDefinition =   this.grdRuleDefinitionList.DataSource;
            DataTable dtRuleDefinition = (DataTable)objRuleDefinition;

            // 헤더 그리드에 데이터가 하나도 없으면 추가 취소 함수를 호출한다.
            if (dtRuleDefinition == null)
            {
                sender.CancelAddNew();
                return;
            }
            else
            {
                // 헤더 그리드에 데이터가 하나도 없으면 추가 취소 함수를 호출한다.
                if (dtRuleDefinition.Rows.Count == 0)
                {
                    sender.CancelAddNew();
                    return;
                }
            }


            object obj = this.grdRuleFormula.DataSource;
            DataTable dt = (DataTable)obj;

            args.NewRow["SEQUENCE"] = 0;
            
            // 순번 맥스 + 1
            DataRow[] row1 = dt.Select("1=1", "SEQUENCE DESC");
            args.NewRow["SEQUENCE"] = int.Parse(row1[0]["SEQUENCE"].ToString()) + 10;

            DataRow row = this.grdRuleDefinitionList.View.GetFocusedDataRow();
            args.NewRow["RULEID"] = row["RULEID"].ToString();
            args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;

            //args.NewRow["VALIDSTATE"] = "Valid";

        }

        private void grdRuleDefinitionList_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {

            DataRow row = this.grdRuleDefinitionList.View.GetFocusedDataRow();
            //기본으로 회사,공장,유효상태를 그리드 추가시 넣어 준다.
            args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            args.NewRow["PLANTID"] = UserInfo.Current.Plant;
            args.NewRow["VALIDSTATE"] = "Valid";
            args.NewRow["RULESET"] = "Item";
            
        }

        // 대상 에 대한 품목명 규칙이나 계산 내역을 보여준다.
        private void grdRuleDefinitionList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FocusedRowChanged();
        }

        private void FocusedRowChanged()
        {
            // 헤더그리드가 없을 경우 리턴
            if (grdRuleDefinitionList.View.FocusedRowHandle < 0)
                return;

            var values = Conditions.GetValues();
            DataRow row = this.grdRuleDefinitionList.View.GetFocusedDataRow();
            //룰id
            values.Add("RULEID", row["RULEID"].ToString());
            //회사
            values.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());

            DataTable dtRuleFormula = SqlExecuter.Query("GetRuleFormula", "10001", values);

            this.grdRuleFormula.DataSource = dtRuleFormula;

        }

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

            // 룰 대상 컬럼  및 타입을 저장  
            changed = grdRuleDefinitionList.GetChangedRows();
            ExecuteRule("AttributeGroup", changed);
            // 계산에 필요한 컬럼과 값을 저장
            changed1 = grdRuleDefinitionList.GetChangedRows();
            ExecuteRule("Attributes", changed1);

            // 저장후 룰대상 그리드를 읽기 전용으로 변경 처리 한다.
            grdRuleDefinitionList.View.SetIsReadOnly(false);
            grdRuleDefinitionList.View.SetIsReadOnly(false);

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
            values.Add("RULESET", "Item");
            
            //TODO : Id를 수정하세요            
            // Stored Procedure 호출
            //this.grdCodeClass.DataSource = await this.ProcedureAsync("usp_com_selectCodeClass", values);
            // Server Xml Query 호출

            DataTable dtRuleDefinition = await SqlExecuter.QueryAsync("GetRuleDefinition", "10001", values);

            if (dtRuleDefinition.Rows.Count < 1) // 
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            this.grdRuleDefinitionList.DataSource = dtRuleDefinition;
            
            

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
            grdRuleDefinitionList.View.CheckValidation();
            changed = grdRuleDefinitionList.GetChangedRows();

            DataTable changed1 = new DataTable();
            grdRuleDefinitionList.View.CheckValidation();
            changed1 = grdRuleDefinitionList.GetChangedRows();


            if (changed.Rows.Count == 0 && changed1.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }
        #endregion

       

    }
}
