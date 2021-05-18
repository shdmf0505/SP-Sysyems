#region using
using DevExpress.XtraEditors.Repository;
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

namespace Micube.SmartMES.ToolManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 치공구관리 > 치공구 수리관리 > 치공구 수리결과 등록
    /// 업  무  설  명  : 수리의뢰된 치공구의 수리결과를 등록한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-08-06
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class RegistRepairToolResult : SmartConditionManualBaseForm
    {
        #region Local Variables
        string _currentStatus;
        string _searchVendorID;
        bool _isHeaderSelected = false;

        ConditionItemSelectPopup areaCondition;
        ConditionItemSelectPopup vendorPopup;
        ConditionItemSelectPopup productPopup;
        #endregion

        #region 생성자

        public RegistRepairToolResult()
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
            InitializeGrid();
        }

        #region InitializeGrid - 제작입고목록을 초기화한다.
        /// <summary>        
        /// 치공구내역목록을 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {            
            // TODO : 그리드 초기화 로직 추가
            grdRepairToolResult.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기            
            grdRepairToolResult.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdRepairToolResult.View.CheckMarkSelection.ShowCheckBoxHeader = false;


            //grdRepairToolResult.View.AddTextBoxColumn("ROWSELECTOR", 80)               //체크박스 선택
            //              ;
            grdRepairToolResult.View.AddTextBoxColumn("SENDDATE", 150)               //출고일자
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly(true);
            grdRepairToolResult.View.AddTextBoxColumn("SENDSEQUENCE", 60)
                .SetIsReadOnly(true);                                               //출고순번
            grdRepairToolResult.View.AddTextBoxColumn("TOOLREPAIRTYPE", 60)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                               //수리구분
            grdRepairToolResult.View.AddTextBoxColumn("REQUESTDATE", 120)
                .SetIsReadOnly(true);                                               //의뢰일
            grdRepairToolResult.View.AddTextBoxColumn("REQUESTSEQUENCE", 60)
                .SetIsReadOnly(true);                                               //의뢰순번
            grdRepairToolResult.View.AddTextBoxColumn("TOOLMAKETYPEID")
                .SetIsHidden();                                                     //제작구분아이디
            grdRepairToolResult.View.AddTextBoxColumn("TOOLMAKETYPE", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                               //제작구분
            grdRepairToolResult.View.AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsReadOnly(true);                                               //품목코드
            grdRepairToolResult.View.AddTextBoxColumn("PRODUCTDEFNAME", 280)
                .SetIsReadOnly(true);                                               //품목명
            grdRepairToolResult.View.AddTextBoxColumn("TOOLNUMBER", 180)
                .SetIsReadOnly(true);                                               //Tool번호
            grdRepairToolResult.View.AddTextBoxColumn("TOOLNAME", 350)
                .SetIsReadOnly(true);                                               //Tool번호
            grdRepairToolResult.View.AddTextBoxColumn("RECEIPTAREAID")
               .SetIsHidden()
               ;                                                                       //입고작업장아이디
            grdRepairToolResult.View.AddTextBoxColumn("RECEIPTAREA", 150)
                .SetIsReadOnly(true)
                ;                                                                       //입고작업장
            grdRepairToolResult.View.AddComboBoxColumn("TOOLFORMCODE", 150, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=ToolForm"), "CODENAME", "CODEID")
               .SetTextAlignment(TextAlignment.Center)
               .SetIsReadOnly(true);                                                  //상태 - (이 회면에서 입력해야 한다면 SetIsReadOnly를 풀어준다.
            grdRepairToolResult.View.AddTextBoxColumn("TOOLCATEGORYID")
                .SetIsHidden();                                                        //Tool구분아이디
            grdRepairToolResult.View.AddTextBoxColumn("TOOLCATEGORY", 120)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                  //Tool구분
            grdRepairToolResult.View.AddTextBoxColumn("TOOLCATEGORYDETAILID")
                .SetIsHidden();                                                        //Tool유형아이디
            grdRepairToolResult.View.AddTextBoxColumn("TOOLCATEGORYDETAIL", 150)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                  //Tool유형
            grdRepairToolResult.View.AddTextBoxColumn("TOOLDETAILID")
                .SetIsHidden();                                                        //Tool유형아이디
            grdRepairToolResult.View.AddTextBoxColumn("TOOLDETAIL", 180)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);
            grdRepairToolResult.View.AddTextBoxColumn("REPAIRFINISHDATE", 150)       //수리일자
               .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
               .SetIsReadOnly(true);
            grdRepairToolResult.View.AddTextBoxColumn("REPAIRFINISHERID")                 
               .SetIsHidden();                                                      //수리자아이디
            grdRepairToolResult.View.AddTextBoxColumn("REPAIRFINISHER", 120)
               .SetIsReadOnly(true);                                                //수리자
            grdRepairToolResult.View.AddTextBoxColumn("REPAIRDESCRIPTION", 200)                
                .SetIsReadOnly(true)
               ;                                                                    //수리자내용
            //REPAIRRESULTCOMMENT
            grdRepairToolResult.View.AddTextBoxColumn("REPAIRRESULTCOMMENT", 200)
                .SetValidationIsRequired()
               ;                                                                    //수리결과내용
            grdRepairToolResult.View.AddSpinEditColumn("WEIGHT", 80)
               .SetDisplayFormat("#,###.####")
                .IsFloatValue = true;                                               //무게
            grdRepairToolResult.View.AddSpinEditColumn("HORIZONTAL", 80)
               .SetDisplayFormat("#,###.####")
                .IsFloatValue = true;                                               //가로
            grdRepairToolResult.View.AddSpinEditColumn("VERTICAL", 80)
               .SetDisplayFormat("#,###.####")
                .IsFloatValue = true;                                               //세로
            grdRepairToolResult.View.AddSpinEditColumn("THEIGHT", 80)
               .SetDisplayFormat("#,###.####")
                .IsFloatValue = true;                                               //높이
            grdRepairToolResult.View.AddSpinEditColumn("POLISHTHICKNESS", 80)
               .SetDisplayFormat("#,###.####")
                .IsFloatValue = true;                                               //연마 후 두께
            grdRepairToolResult.View.AddTextBoxColumn("TOTALPOLISHTHICKNESS", 80)
               .SetIsReadOnly(true);                                                //연마 전 두께
            grdRepairToolResult.View.AddSpinEditColumn("ORIGINTHICKNESS", 80)
               .SetDisplayFormat("#,###.####")
               .SetIsHidden()
               .IsFloatValue = true;                                                //연마 전 두께
            grdRepairToolResult.View.AddSpinEditColumn("TOOLTHICKNESSLIMIT", 80)
               .SetDisplayFormat("#,###.####")
               .SetIsReadOnly(true)
               .IsFloatValue = true;                                                //두께기준
            grdRepairToolResult.View.AddSpinEditColumn("CREATEDTHICKNESS", 80)
               .SetDisplayFormat("#,###.####")
               .SetIsReadOnly(true)
               .IsFloatValue = true;                                                //두께기준
            grdRepairToolResult.View.AddTextBoxColumn("REQUESTUSER", 80)
                .SetIsReadOnly(true);                                               //의뢰자
            grdRepairToolResult.View.AddTextBoxColumn("REQUESTUSERID", 80)
                .SetIsHidden();                                                    //의뢰자
            grdRepairToolResult.View.AddTextBoxColumn("MAKEVENDOR", 150)
                .SetIsReadOnly(true);                                               //제작업체
            grdRepairToolResult.View.AddTextBoxColumn("VENDORID")
                .SetIsHidden();                                                    //제작업체아이디

            grdRepairToolResult.View.PopulateColumns();
        }
        #endregion
        #endregion

        #region Event
        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            btnSave.Click += BtnSave_Click;
            grdRepairToolResult.View.CheckStateChanged += grdRepairToolResult_CheckStateChanged;
            grdRepairToolResult.View.CellValueChanged += grdRepairToolResult_CellValueChanged;

            Shown += RegistRepairToolResult_Shown;
        }

        #region RegistRepairToolResult_Shown - Site관련정보를 화면로딩후 설정한다.
        private void RegistRepairToolResult_Shown(object sender, EventArgs e)
        {
            ChangeSiteCondition();

            ((SmartComboBox)Conditions.GetControl("P_PLANTID")).EditValueChanged += ConditionPlant_EditValueChanged;
        }
        #endregion

        #region ConditionPlant_EditValueChanged - 검색조건의 Site정보 변경시 관련 쿼리들을 일괄 변경한다.
        private void ConditionPlant_EditValueChanged(object sender, EventArgs e)
        {
            ChangeSiteCondition();
        }
        #endregion

        #region grdRepairToolResult_CellValueChanged - 그리드내의 특정 셀 변경 이벤트
        private void grdRepairToolResult_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.Equals("POLISHTHICKNESS"))
            {
                grdRepairToolResult.View.SetRowCellValue(e.RowHandle, "TOTALPOLISHTHICKNESS", grdRepairToolResult.View.GetDataRow(e.RowHandle).GetInteger("ORIGINTHICKNESS") + Convert.ToInt32(e.Value));
            }
        }
        #endregion

        #region grdRepairToolResult_CheckStateChanged - 그리드내의 체크박스변경이벤트
        private void grdRepairToolResult_CheckStateChanged(object sender, EventArgs e)
        {
            DataRow dataRow = grdRepairToolResult.View.GetFocusedDataRow();
            if ((Boolean)grdRepairToolResult.View.GetFocusedRowCellValue("_INTERNAL_CHECKMARK_SELECTION_"))
            {
                grdRepairToolResult.View.SetFocusedRowCellValue("REPAIRFINISHDATE", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                grdRepairToolResult.View.SetFocusedRowCellValue("REPAIRFINISHERID", UserInfo.Current.Id);
                grdRepairToolResult.View.SetFocusedRowCellValue("REPAIRFINISHER", UserInfo.Current.Name);
                grdRepairToolResult.View.SetFocusedRowModified();
                lblTopTitle.Focus();
            }
            else
            {
                grdRepairToolResult.View.SetFocusedRowCellValue("REPAIRFINISHDATE", "");
                grdRepairToolResult.View.SetFocusedRowCellValue("REPAIRFINISHERID", "");
                grdRepairToolResult.View.SetFocusedRowCellValue("REPAIRFINISHER", "");
                grdRepairToolResult.View.SetFocusedRowModified();
                lblTopTitle.Focus();
            }
        }
        #endregion

        #region BtnSave_Click - 저장버튼클릭이벤트
        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        #endregion
        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();
        }

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Save"))
                BtnSave_Click(sender, e);
        }

        #endregion

        #region 검색
        #region OnSearchAsync - 검색
        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            InitializeInsertForm();
            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);
            if (Conditions.GetValue("VENDORNAME").ToString() != "")
                values.Add("VENDORID", _searchVendorID);
            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolRepairResult = SqlExecuter.Query("GetRegistToolRepairListForResultByTool", "10001", values);

            grdRepairToolResult.DataSource = toolRepairResult;

            if (toolRepairResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            _currentStatus = "modified";
        }
        #endregion

        #region Research - 재검색(데이터 입력후와 같은 경우에 사용)
        /// <summary>
        /// 저장 삭제후 재 검색시 사용
        /// 생성 및 수정시 발생한 아이디를 통해 자동 선택되도록 유도하며 삭제시에는 null로 매개변수를 받아서 첫번째 행을 보여준다. 행이 없다면 빈 값으로 설정
        /// </summary>
        /// <param name="inboudNo">입력 및 수정시 발생한 값의 아이디.</param>
        private void Research()
        {
            // TODO : 조회 SP 변경
            InitializeInsertForm();

            var values = Conditions.GetValues();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);
            if (Conditions.GetValue("VENDORNAME").ToString() != "")
                values.Add("VENDORID", _searchVendorID);
            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable sparePartSearchResult = SqlExecuter.Query("GetRegistToolRepairListForResultByTool", "10001", values);

            if (sparePartSearchResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdRepairToolResult.DataSource = sparePartSearchResult;
            
            _currentStatus = "modified";
        }
        #endregion        

        #region 조회조건 제어
        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            //InitializeCondition_Plant();
            InitializeRepairVendors();
            InitializeConditionProductCodePopup();
            InitializeConditionAreaPopup();
            InitializeConditionRegistType();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeCondition_Plant()
        {
            var planttxtbox = Conditions.AddComboBox("PLANTID", new SqlQuery("GetPlantList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
               .SetLabel("PLANT")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.1)
               .SetIsReadOnly(true)
               .SetDefault(UserInfo.Current.Plant, "PLANTID") //기본값 설정 UserInfo.Current.Plant
            ;
        }

        private void InitializeConditionRegistType()
        {
            var planttxtbox = Conditions.AddComboBox("TOOLREGISTTYPE", new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=ToolRegistType"), "CODENAME", "CODEID")
               .SetLabel("TOOLREPAIRRESULTCONDITION")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.4)               
               .SetDefault("NotRegist", "CODEID")
            ;
        }

        #region InitializeReceiptArea : 작업장 검색조건
        /// <summary>
        /// 작업장 검색조건
        /// </summary>
        private void InitializeConditionAreaPopup()
        {
            areaCondition = Conditions.AddSelectPopup("AREAID", new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CURRENTLOGINID={UserInfo.Current.Id}"))
            .SetPopupLayout("AREAID", PopupButtonStyles.Ok_Cancel, true, true)
            .SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow)
            .SetPopupAutoFillColumns("AREAID")
            .SetLabel("RECEIPTAREA")
            .SetPopupResultCount(1)
            .SetPosition(0.3)
            .SetPopupResultMapping("AREAID", "AREAID");

            //areaCondition.SetValidationIsRequired();
            areaCondition.ValueFieldName = "AREAID";
            areaCondition.DisplayFieldName = "AREANAME";

            areaCondition.Conditions.AddTextBox("AREANAME");

            areaCondition.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetIsHidden();
            areaCondition.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }
        #endregion

        #region InitializeRepairVendors : 팝업창 제어
        /// <summary>
        /// 팝업창 제어
        /// </summary>
        private void InitializeRepairVendors()
        {
            vendorPopup = Conditions.AddSelectPopup("VENDORNAME", new SqlQuery("GetVendorListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
            .SetPopupLayout("MAKEVENDOR", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
            .SetPopupResultMapping("VENDORNAME", "VENDORNAME")
            .SetLabel("MAKEVENDOR")
            .SetPopupResultCount(1)
            .SetPosition(0.2)
            .SetPopupApplySelection((selectedRows, dataGridRow) =>
            {
                int i = 0;

                foreach (DataRow row in selectedRows)
                {
                    if (i == 0)
                    {
                        _searchVendorID = row["VENDORID"].ToString();
                    }
                    i++;
                }
            })
            ;

            // 팝업에서 사용할 조회조건 항목 추가
            vendorPopup.Conditions.AddTextBox("VENDORNAME");

            // 팝업 그리드 설정
            vendorPopup.GridColumns.AddTextBoxColumn("VENDORID", 300)
                .SetIsHidden();
            vendorPopup.GridColumns.AddTextBoxColumn("VENDORNAME", 300)
                .SetIsReadOnly();

        }
        /// <summary>
        /// 품목코드 조회 설정
        /// </summary>
        private void InitializeConditionProductCodePopup()
        {
            productPopup = Conditions.AddSelectPopup("PRODUCTDEFID", new SqlQuery("GetProductdefidPoplistByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"PRODUCTDEFTYPE=Product"))
            .SetPopupLayout("PRODUCTDEFIDPOPUP", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow)
            .SetPopupResultMapping("PRODUCTDEFID", "PRODUCTDEFID")
            .SetLabel("PRODUCTDEF")
            .SetPopupResultCount(1)
            .SetPosition(2.5)
            ;

            productPopup.ValueFieldName = "PRODUCTDEFID";
            productPopup.DisplayFieldName = "PRODUCTDEFNAME";


            // 팝업 조회조건
            productPopup.Conditions.AddTextBox("PRODUCTDEF")
                .SetLabel("PRODUCTDEF");
            //productPopup.Conditions.AddComboBox("PRODUCTDEFTYPE", new SqlQuery("GetCodeListByOspProductdeftype", "10001", new Dictionary<string, object>() {{"LANGUAGETYPE", UserInfo.Current.LanguageType}}), "CODENAME", "CODEID")
            //    .SetEmptyItem("", "", true)
            //    .SetLabel("PRODUCTDEFTYPE")
            //    .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
            //    ;
            //productPopup.Conditions.AddTextBox("PRODUCTDEFID")
            //    .SetLabel("PRODUCTDEFID");


            // 팝업 그리드
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 250)
                .SetIsReadOnly();
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly();
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 350)
                .SetIsReadOnly();
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFCODE", 50)
                .SetIsHidden();
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFTYPE", 200)
                .SetIsReadOnly();
        }
        #endregion

        #endregion
        #endregion

        #region 유효성 검사

        protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }

        #region ValidateContent : 저장하기 전 Validation을 체크한다.
        /// <summary>
        /// 입력/수정 시의 Validation 체크
        /// 입력/수정 각각의 체크값이 서로 다르다면 상태에 따른 분기가 있어야 한다.
        /// </summary>
        /// <returns></returns>
        private bool ValidateContent(out string messageCode)
        {
            messageCode = "";
            //각각의 상태에 따라 보여줄 메세지가 상이하다면 각각의 분기에서 메세지를 표현해야 한다.     
            DataTable dataTables = grdRepairToolResult.GetChangedRows();
            foreach (DataRow currentRow in dataTables.Rows)
            {
                if (!ValidateCellInGrid(currentRow, new string[] { "REPAIRRESULTCOMMENT" }))
                {
                    messageCode = "ValidateRequiredData";
                    return false;
                }

                if(currentRow.GetString("ISMODIFY").Equals("N"))
                {
                    messageCode = "ValidateToolResultReqSec";//권한이 없는 작업장의 치공구에 대한 결과는 등록할 수 없습니다.
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region ValidateComboBoxEqualValue : 2개의 콤보박스의 값이 서로 같지 않아야 하는 경우에 사용
        /// <summary>
        /// 2개의 콤보박스에 데이터가 모두 있어야 하며
        /// 2개의 콤보박스의 데이터가 서로 동일하면 안된다.
        /// </summary>
        /// <param name="originBox"></param>
        /// <param name="targetBox"></param>
        /// <returns></returns>
        private bool ValidateComboBoxEqualValue(SmartComboBox originBox, SmartComboBox targetBox)
        {
            //두 콤보박스의 값이 같으면 안된다.
            if (originBox.EditValue.Equals(targetBox.EditValue))
                return false;
            return true;
        }
        #endregion

        #region ValidateNumericBox : 숫자를 입력하는 컨트롤에 대하여 입력받은 값보다 같거나 큰지 체크
        /// <summary>
        /// 숫자를 입력하는 컨트롤을 대상으로 하여 입력받은 기준값보다 값이 같거나 크다면 true 작다면 false를 반환하는 메소드
        /// </summary>
        /// <param name="originBox"></param>
        /// <param name="ruleValue"></param>
        /// <returns></returns>
        private bool ValidateNumericBox(SmartTextBox originBox, int ruleValue)
        {
            //값이 없으면 안된다.
            if (originBox.EditValue == null)
                return false;

            int resultValue = 0;

            //입력받은 기준값(예를 들어 0)보다 작다면 false를 반환
            if (Int32.TryParse(originBox.EditValue.ToString(), out resultValue))
                if (resultValue <= ruleValue)
                    return false;

            //모두 통과했으므로 Validation Check완료
            return true;
        }
        #endregion

        #region ValidateEditValue : 입력받은 editValue에 대해 Validation을 수행한다.
        /// <summary>
        /// 입력받은 editValue에 아무런 값이 입력되지 않았다면 false 입력받았다면 true
        /// </summary>
        /// <param name="editValue"></param>
        /// <returns></returns>
        private bool ValidateEditValue(object editValue)
        {
            if (editValue == null)
                return false;

            if (editValue.ToString() == "")
                return false;

            return true;
        }
        #endregion

        #region ValidateCellInGrid : 그리드내의 셀에 대하여 Validation을 수행한다.
        private bool ValidateCellInGrid(DataRow currentRow, string[] columnNames)
        {
            foreach (string columnName in columnNames)
            {
                if (currentRow[columnName] == null)
                    return false;
                if (currentRow[columnName].ToString() == "")
                    return false;
            }

            return true;
        }
        #endregion

        #region SetRequiredValidationControl : 필수입력항목을 빨간색으로 표시한다.
        private void SetRequiredValidationControl(Control requiredControl)
        {
            requiredControl.ForeColor = Color.Red;
        }
        #endregion

        #endregion

        #region Private Function
        #region InitializeInsertForm : 입고등록하기 위한 화면 초기화
        /// <summary>
        /// 입고등록정보를 입력하기 위해 화면을 초기화 한다.
        /// </summary>
        private void InitializeInsertForm()
        {
            try
            {
                _currentStatus = "added";           //현재 화면의 상태는 입력중이어야 한다.
                
                //컨트롤 접근여부는 작성상태로 변경한다.
                ControlEnableProcess("added");

                //ReadOnly 처리된 DataGrid를 
            }
            catch (Exception err)
            {
                ShowError(err);
            }
        }
        #endregion

        #region controlEnableProcess : 입력/수정/삭제를 위한 화면내 컨트롤들의 Enable 제어
        /// <summary>
        /// 진행상태값에 따른 입력 항목 lock 처리
        /// </summary>
        /// <param name="blProcess"></param>
        private void ControlEnableProcess(string currentStatus)
        {
            if (currentStatus == "added") //초기화 버튼을 클릭한 경우
            {
            }
            else if (currentStatus == "modified") //
            {
            }
            else
            {
            }
        }
        #endregion

        #region CreateSaveDatatable : ToolRepairSendLot 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// <summary>
        /// ToolRequest 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// 입력시에는 ToolRequest와 ToolRequestDetail에 입력하고
        /// 수정시에는 ToolRequest와 ToolRequestDetail 및 ToolRequestDetailLot에 까지 데이터를 기입한다.
        /// </summary>
        /// <returns></returns>
        private DataTable CreateSaveDatatable(bool useState)
        {
            DataTable dt = new DataTable();
            dt.TableName = "toolRepairSendLotList";
            //===================================================================================            
            dt.Columns.Add("SENDDATE");
            dt.Columns.Add("SENDSEQUENCE");
            dt.Columns.Add("FINISHDATE");
            dt.Columns.Add("FINISHERID");
            dt.Columns.Add("FINISHER");
            dt.Columns.Add("TOOLNUMBER");
            dt.Columns.Add("REPAIRDESCRIPTION");
            dt.Columns.Add("RESULTCOMMENT");
            dt.Columns.Add("WEIGHT");
            dt.Columns.Add("HEIGHT");
            dt.Columns.Add("THEIGHT");
            dt.Columns.Add("HORIZONTAL");
            dt.Columns.Add("VERTICAL");
            dt.Columns.Add("THICKNESS");


            dt.Columns.Add("CREATOR");
            dt.Columns.Add("CREATEDTIME");
            dt.Columns.Add("MODIFIER");
            dt.Columns.Add("MODIFIEDTIME");
            dt.Columns.Add("LASTTXNHISTKEY");
            dt.Columns.Add("LASTTXNID");
            dt.Columns.Add("LASTTXNUSER");
            dt.Columns.Add("LASTTXNTIME");
            dt.Columns.Add("LASTTXNCOMMENT");
            dt.Columns.Add("VALIDSTATE");

            if (useState)
                dt.Columns.Add("_STATE_");

            return dt;
        }
        #endregion

        #region SaveData : 입력/수정을 수행
        private void SaveData()
        {
            //Validation 체크 부분 작성 필요
            this.ShowWaitArea();

            //저장 로직
            try
            {
                btnSave.Enabled = false;
                string messageCode = "";
                //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                if (grdRepairToolResult.View.RowCount > 0)
                {
                    if (ValidateContent(out messageCode))
                    {
                        var values = Conditions.GetValues();
                        DataSet toolRepairSet = new DataSet();
                        //치공구 제작의뢰를 입력
                        DataTable toolRepairTable = CreateSaveDatatable(true);

                        DataTable changedTable = grdRepairToolResult.GetChangedRows();
                        toolRepairTable.Columns.Add("P_SENDDATE_PERIODFR");
                        toolRepairTable.Columns.Add("P_SENDDATE_PERIODTO");
                        foreach (DataRow changedRow in changedTable.Rows)
                        {
                            if (changedRow.GetString("SENDDATE") != "")
                            {
                                DateTime sendDate = Convert.ToDateTime(changedRow.GetString("SENDDATE"));


                                DataRow toolRepairRow = toolRepairTable.NewRow();

                                toolRepairRow["SENDDATE"] = sendDate.ToString("yyyy-MM-dd HH:mm:ss");
                                toolRepairRow["SENDSEQUENCE"] = changedRow["SENDSEQUENCE"];
                                toolRepairRow["P_SENDDATE_PERIODFR"] = Conditions.GetValues()["P_SENDDATE_PERIODFR"].ToString();
                                toolRepairRow["P_SENDDATE_PERIODTO"] = Conditions.GetValues()["P_SENDDATE_PERIODTO"].ToString();
                                if (changedRow.GetString("REPAIRFINISHDATE") != null && changedRow.GetString("REPAIRFINISHDATE") != "")
                                {
                                    toolRepairRow["FINISHDATE"] = Convert.ToDateTime(changedRow.GetString("REPAIRFINISHDATE")).ToString("yyyy-MM-dd HH:mm:ss");
                                    toolRepairRow["FINISHERID"] = changedRow.GetString("REPAIRFINISHERID");
                                    toolRepairRow["FINISHER"] = changedRow.GetString("REPAIRFINISHER");
                                }

                                toolRepairRow["TOOLNUMBER"] = changedRow["TOOLNUMBER"];
                                toolRepairRow["REPAIRDESCRIPTION"] = changedRow["REPAIRDESCRIPTION"];
                                toolRepairRow["RESULTCOMMENT"] = changedRow["REPAIRRESULTCOMMENT"];
                                toolRepairRow["WEIGHT"] = changedRow["WEIGHT"];
                                toolRepairRow["HEIGHT"] = changedRow["THEIGHT"];
                                toolRepairRow["HORIZONTAL"] = changedRow["HORIZONTAL"];
                                toolRepairRow["VERTICAL"] = changedRow["VERTICAL"];
                                toolRepairRow["THICKNESS"] = changedRow["POLISHTHICKNESS"];

                                toolRepairRow["MODIFIER"] = UserInfo.Current.Id;

                                toolRepairRow["VALIDSTATE"] = "Valid";
                                //무조건 수정작업만 이루어진다.
                                toolRepairRow["_STATE_"] = "modified";

                                toolRepairTable.Rows.Add(toolRepairRow);
                            }
                        }

                        toolRepairSet.Tables.Add(toolRepairTable);

                        DataTable resultTable = this.ExecuteRule<DataTable>("ToolRegistRepairResult", toolRepairSet);

                        ////Server에서 결과값을 현재는 반환하지 않음 만약 반환해야 한다면 아래로직 구현
                        DataRow resultRow = resultTable.Rows[0];

                        ControlEnableProcess("modified");

                        ////가져온 결과값에 따라 재검색한 후 가져온 아이디에 맞는 행을 선택으로 변경한다.
                        Research();
                    }
                    else
                    {
                        this.ShowMessage(MessageBoxButtons.OK, messageCode, "");
                    }
                }
                else
                {
                    //ShowMessage(MessageBoxButtons.OK, String.Format("SelectItem", Language.GetDictionary("DURABLE")), "");
                    ShowMessage(MessageBoxButtons.OK, "SelectItem", Language.Get("DURABLE"));
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
                btnSave.Enabled = true;
            }
        }
        #endregion

        #region ChangeSiteCondition - 검색조건의 Site변경시 관련된 쿼리들을 변경
        private void ChangeSiteCondition()
        {
            if (areaCondition != null)
                areaCondition.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"CURRENTLOGINID={UserInfo.Current.Id}");            

            if (vendorPopup != null)
                vendorPopup.SearchQuery = new SqlQuery("GetVendorListByTool", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            if (productPopup != null)
                productPopup.SearchQuery = new SqlQuery("GetProductdefidPoplistByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"PRODUCTDEFTYPE=Product");

            //작업장 검색조건 초기화
            ((SmartSelectPopupEdit)Conditions.GetControl("AREAID")).ClearValue();//VENDORNAME
            ((SmartSelectPopupEdit)Conditions.GetControl("VENDORNAME")).ClearValue();//VENDORNAME
        }
        #endregion

        #endregion
    }
}
