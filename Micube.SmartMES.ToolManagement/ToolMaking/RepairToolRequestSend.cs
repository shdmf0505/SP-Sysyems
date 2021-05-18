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

namespace Micube.SmartMES.ToolManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 치공구관리 > 치공구수리관리 > 치공구 수리출고
    /// 업  무  설  명  : 치공구 수정의뢰된 항목을 수리출고한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-07-25
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class RepairToolRequestSend : SmartConditionManualBaseForm
    {
        #region Local Variables
        
        // TODO : 화면에서 사용할 내부 변수 추가
        string _currentStatus;                                      // 현재상태
        string _sendUserID;                                         // 출고자아이디
        string _selectedVendor;
        string _searchUserID;
        string[] _clipDatas;
        int _clipIndex = 1;
        IDataObject _clipBoardData;
        bool _isGoodCopy = true;

        ConditionItemSelectPopup popupGridToolArea;
        ConditionItemComboBox conditionFactoryBox;
        ConditionItemSelectPopup popupGridToolVendor;
        ConditionItemSelectPopup areaCondition;
        ConditionItemSelectPopup userCodePopup;
        ConditionItemSelectPopup productPopup;
        ConditionItemSelectPopup vendorPopup;
        #endregion

        #region 생성자

        public RepairToolRequestSend()
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
            InitializeGridRepairSend();

            InitializeRepairVendors();
            InitializeToolRepairType();

            InitialRequriedForm();

            deSendDate.EditValue = DateTime.Now;
            _sendUserID = UserInfo.Current.Id;
            txtSendor.EditValue = UserInfo.Current.Name;
            _currentStatus = "added";           //현재 화면의 상태는 입력중이어야 한다.

            InitializeInsertForm();//기본화면 초기화
        }

        #region InitializeGrid - 제작입고목록을 초기화한다.
        /// <summary>        
        /// 치공구내역목록을 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdToolRepiarSend.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기            
            grdToolRepiarSend.View.SetIsReadOnly();

            grdToolRepiarSend.View.AddTextBoxColumn("SENDDATE", 150)                 //입고일자
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdToolRepiarSend.View.AddTextBoxColumn("SENDSEQUENCE", 80)
                ;                                                                  //제작구분
            grdToolRepiarSend.View.AddTextBoxColumn("TOOLREPAIRTYPE", 80)         //수리구분
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdToolRepiarSend.View.AddTextBoxColumn("TOOLNUMBER", 180)
                ;                                                                       //Tool번호
            grdToolRepiarSend.View.AddTextBoxColumn("TOOLCODE", 150)
                ;                                                                       //Tool코드
            grdToolRepiarSend.View.AddTextBoxColumn("TOOLVERSION", 80);            //Tool버전
            grdToolRepiarSend.View.AddTextBoxColumn("TOOLNAME", 350);            //Tool버전
            grdToolRepiarSend.View.AddTextBoxColumn("PRODUCTDEFID", 120)
                ;                                                                       //품목코드
            grdToolRepiarSend.View.AddTextBoxColumn("PRODUCTDEFNAME", 250);         //품목명
            grdToolRepiarSend.View.AddComboBoxColumn("TOOLFORMCODE", 150, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=ToolForm"), "CODENAME", "CODEID")
               .SetTextAlignment(TextAlignment.Center)
               .SetIsReadOnly(true);                                                  //상태 - (이 회면에서 입력해야 한다면 SetIsReadOnly를 풀어준다.
            grdToolRepiarSend.View.AddTextBoxColumn("TOOLCATEGORYID")
                .SetIsHidden();                                                        //Tool구분아이디
            grdToolRepiarSend.View.AddTextBoxColumn("TOOLCATEGORY", 120)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                  //Tool구분
            grdToolRepiarSend.View.AddTextBoxColumn("TOOLCATEGORYDETAILID")
                .SetIsHidden();                                                        //Tool유형아이디
            grdToolRepiarSend.View.AddTextBoxColumn("TOOLCATEGORYDETAIL", 180)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                  //Tool유형
            grdToolRepiarSend.View.AddTextBoxColumn("TOOLDETAILID")
                .SetIsHidden();                                                        //Tool유형아이디
            grdToolRepiarSend.View.AddTextBoxColumn("TOOLDETAIL", 150)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);
            grdToolRepiarSend.View.AddTextBoxColumn("VENDORID")
                .SetIsHidden();                                                         //제작업체아이디
            grdToolRepiarSend.View.AddTextBoxColumn("MAKEVENDOR", 150)
                ;                                                                       //제작업체
            grdToolRepiarSend.View.AddTextBoxColumn("SENDAREAID")
                .SetIsHidden();                                                         //작업장아이디
            grdToolRepiarSend.View.AddTextBoxColumn("SENDAREA", 180)
                ;                                                                       //작업장           
            grdToolRepiarSend.View.AddTextBoxColumn("SENDOR", 100);              //의뢰자
            grdToolRepiarSend.View.AddTextBoxColumn("RECEIPTAREA", 180);                  //무게
            grdToolRepiarSend.View.AddTextBoxColumn("REPAIRDESCRIPTION", 250);                  //무게
            
            grdToolRepiarSend.View.PopulateColumns();
        }
        #endregion

        #region InitializeGridDetail - 제작입고목록입력을 위한 그리드를 초기화한다.
        /// <summary>        
        /// 제작입고목록입력을 위한 그리드를 초기화한다.
        /// </summary>
        private void InitializeGridRepairSend()
        {
            // TODO : 그리드 초기화 로직 추가
            grdInputToolRepairSend.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore; // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기       
            grdInputToolRepairSend.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdInputToolRepairSend.View.AddTextBoxColumn("SENDDATE", 150)           //출고일자
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly(true);
            grdInputToolRepairSend.View.AddTextBoxColumn("SENDSEQUENCE", 80)        //출고순번
                .SetIsReadOnly(true);
            grdInputToolRepairSend.View.AddTextBoxColumn("SENDOR", 100)             //출고자
                .SetIsReadOnly(true);
            grdInputToolRepairSend.View.AddTextBoxColumn("USERID")                   //출고자
                .SetIsHidden();
            //grdInputToolRepairSend.View.AddTextBoxColumn("MAKEVENDOR", 100)             //출고자
            //    .SetIsReadOnly(true);
            InitializeVendorPopupColumnInDetailGrid();
            grdInputToolRepairSend.View.AddTextBoxColumn("VENDORID")             //출고자
                .SetIsHidden();
            grdInputToolRepairSend.View.AddTextBoxColumn("TOOLNUMBER", 150)
                .SetIsReadOnly(true);                                               //Tool 번호            
            grdInputToolRepairSend.View.AddComboBoxColumn("TOOLREPAIRTYPE", 150, new SqlQuery("GetToolRepairTypeByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "TOOLREPAIRTYPENAME", "TOOLREPAIRTYPE")
                .SetTextAlignment(TextAlignment.Center)
                .SetValidationIsRequired();                                          //수리구분
            InitializeAreaPopupColumnInDetailGrid();
            //grdInputToolRepairSend.View.AddTextBoxColumn("RECEIPTAREA", 150)
            //    ;                                                                    //입고작업장
            grdInputToolRepairSend.View.AddTextBoxColumn("RECEIPTAREAID", 150)
                .SetIsHidden();                                                     //입고작업장                
            grdInputToolRepairSend.View.AddTextBoxColumn("ISMODIFY", 150)
                .SetIsHidden();                                                     //입고작업장                
            grdInputToolRepairSend.View.AddTextBoxColumn("REPAIRDESCRIPTION", 250)
                .SetValidationIsRequired()
                ;                                                                    //입고작업장
            grdInputToolRepairSend.View.AddTextBoxColumn("SENDAREA", 150)
                .SetIsReadOnly(true);                                               //출고작업장
            grdInputToolRepairSend.View.AddTextBoxColumn("PRODUCTDEFID", 120)
                .SetIsReadOnly(true);                                               //품목코드
            grdInputToolRepairSend.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsHidden();                                                     //품목버전
            grdInputToolRepairSend.View.AddTextBoxColumn("PRODUCTDEFNAME", 250)
                .SetIsReadOnly(true);                                               //품목명
            grdInputToolRepairSend.View.AddTextBoxColumn("USEDCOUNT", 100)
                .SetIsReadOnly(true);                                               //사용타수
            grdInputToolRepairSend.View.AddTextBoxColumn("CLEANLIMIT", 100)
                .SetIsReadOnly(true);                                               //연마기준타수
            grdInputToolRepairSend.View.AddTextBoxColumn("TOTALCLEANCOUNT", 100)
                .SetIsReadOnly(true);                                               //연마횟수
            grdInputToolRepairSend.View.AddTextBoxColumn("TOTALUSEDCOUNT", 100)
                .SetIsReadOnly(true);                                               //누적타수
            grdInputToolRepairSend.View.AddTextBoxColumn("USEDLIMIT", 100)
                .SetIsReadOnly(true);                                               //보증타수
            grdInputToolRepairSend.View.AddTextBoxColumn("TOTALREPAIRCOUNT", 100)
                .SetIsReadOnly(true);                                               //수리횟수
            grdInputToolRepairSend.View.AddTextBoxColumn("RESULTSTATUS", 100)
                .SetIsReadOnly(true);                                               //수리횟수

            //grdInputToolRepairSend.View.SetAutoFillColumn("PRODUCTDEFNAME");
            grdInputToolRepairSend.View.PopulateColumns();
        }
        #endregion

        #region InitializeAreaPopupColumnInDetailGrid - 작업장을 팝업검색할 필드 설정
        /// <summary>
        /// grid ProcesssegmentidLis
        /// </summary>
        private void InitializeAreaPopupColumnInDetailGrid()
        {
            popupGridToolArea = this.grdInputToolRepairSend.View.AddSelectPopupColumn("RECEIPTAREA", 150, new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"CURRENTLOGINID={UserInfo.Current.Id}", $"ISMOD=Y"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("RECEIPTAREA", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                //필수값설정
                .SetValidationIsRequired()
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("RECEIPTAREA", "AREANAME")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정                
                .SetPopupAutoFillColumns("AREANAME")
                .SetNotUseMultiColumnPaste(true)
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    foreach (DataRow row in selectedRows)
                    {
                        grdInputToolRepairSend.View.GetFocusedDataRow()["RECEIPTAREAID"] = row["AREAID"];
                        grdInputToolRepairSend.View.GetFocusedDataRow()["RECEIPTAREA"] = row["AREANAME"];
                        grdInputToolRepairSend.View.GetFocusedDataRow()["ISMODIFY"] = row["ISMODIFY"];
                    }
                })
            //.SetPopupValidationCustom(ValidationParentAreaIdPopup);
            ;
            // 팝업에서 사용할 조회조건 항목 추가
            popupGridToolArea.Conditions.AddTextBox("AREANAME");
            conditionFactoryBox = popupGridToolArea.Conditions.AddComboBox("FACTORYID", new SqlQuery("GetFactoryListByEqp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "FACTORYNAME", "FACTORYID")
                ;

            // 팝업 그리드 설정
            popupGridToolArea.GridColumns.AddTextBoxColumn("AREAID", 300)
                .SetIsHidden();
            popupGridToolArea.GridColumns.AddTextBoxColumn("AREANAME", 300)
                .SetIsReadOnly();
        }
        #endregion

        #region InitializeVendorPopupColumnInDetailGrid - 제작업체를 팝업검색할 필드 설정

        /// <summary>
        /// grid ProcesssegmentidLis
        /// </summary>
        private void InitializeVendorPopupColumnInDetailGrid()
        {
            popupGridToolVendor = grdInputToolRepairSend.View.AddSelectPopupColumn("MAKEVENDOR", 150, new SqlQuery("GetVendorListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("MAKEVENDOR", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                //필수값설정
                .SetValidationIsRequired()
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("MAKEVENDOR", "VENDORNAME")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(400, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정                
                .SetPopupAutoFillColumns("VENDORNAME")
                .SetValidationIsRequired()
                .SetNotUseMultiColumnPaste(true)
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    int i = 0;
                    int currentIndex = grdInputToolRepairSend.View.GetFocusedDataSourceRowIndex();

                    foreach (DataRow row in selectedRows)
                    {
                        if (i == 0)
                        {
                            grdInputToolRepairSend.View.GetFocusedDataRow()["MAKEVENDOR"] = row["VENDORNAME"];
                            grdInputToolRepairSend.View.GetFocusedDataRow()["VENDORID"] = row["VENDORID"];
                        }
                        i++;
                    }
                })
            //.SetPopupValidationCustom(ValidationParentAreaIdPopup);
            ;
            // 팝업에서 사용할 조회조건 항목 추가

            popupGridToolVendor.Conditions.AddTextBox("VENDORNAME");

            // 팝업 그리드 설정
            popupGridToolVendor.GridColumns.AddTextBoxColumn("VENDORID", 300)
                .SetIsHidden();
            popupGridToolVendor.GridColumns.AddTextBoxColumn("VENDORNAME", 300)
                .SetIsReadOnly();
        }
        #endregion
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            btnSearchTool.Click += BtnSearchTool_Click;            
            btnInitialize.Click += BtnInitialize_Click;
            btnModify.Click += BtnModify_Click;
            btnErase.Click += BtnErase_Click;

            grdToolRepiarSend.View.FocusedRowChanged += grdToolUpdateSend_FocusedRowChanged;
            grdInputToolRepairSend.View.ShowingEditor += grdInputToolRepairSend_ShowingEditor;
            grdInputToolRepairSend.View.CellValueChanged += grdInputToolRepairSend_CellValueChanged;
            grdInputToolRepairSend.View.CheckStateChanged += grdInputToolRepairSend_CheckStateChanged;

            Shown += RepairToolRequestSend_Shown;
        }

        #region RepairToolRequestSend_Shown - Site관련정보를 화면로딩후 설정한다.
        private void RepairToolRequestSend_Shown(object sender, EventArgs e)
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

        #region grdInputToolRepairSend_CheckStateChanged - 그리드내의 체크박스 변경이벤트
        private void grdInputToolRepairSend_CheckStateChanged(object sender, EventArgs e)
        {
            //출고 취소선택을 위해 기존의 생성단계에서만 선택되던 기능을 폐기
            //grdInputToolRepairSend.View.CheckStateChanged -= grdInputToolRepairSend_CheckStateChanged;

            //if (!_currentStatus.Equals("added"))
            //    grdInputToolRepairSend.View.CheckRow(grdInputToolRepairSend.View.FocusedRowHandle, false);

            //grdInputToolRepairSend.View.CheckStateChanged += grdInputToolRepairSend_CheckStateChanged;
        }
        #endregion

        #region grdInputToolRepairSend_CellValueChanged - 그리드의 특정 셀 변경이벤트
        private void grdInputToolRepairSend_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "MAKEVENDOR")
            {                
                grdInputToolRepairSend.View.CellValueChanged -= grdInputToolRepairSend_CellValueChanged;

                DataRow row = grdInputToolRepairSend.View.GetDataRow(e.RowHandle);

                if (row.GetString("MAKEVENDOR").Equals(""))
                {
                    grdInputToolRepairSend.View.CellValueChanged += grdInputToolRepairSend_CellValueChanged;
                    return;
                }
                GetSingleMakeVendor(row.GetString("MAKEVENDOR"), e.RowHandle);
                grdInputToolRepairSend.View.CellValueChanged += grdInputToolRepairSend_CellValueChanged;
            }
            if (e.Column.FieldName == "RECEIPTAREA")
            {
                grdInputToolRepairSend.View.CellValueChanged -= grdInputToolRepairSend_CellValueChanged;

                DataRow row = grdInputToolRepairSend.View.GetDataRow(e.RowHandle);

                if (row["RECEIPTAREA"].ToString().Equals(""))
                {
                    grdInputToolRepairSend.View.CellValueChanged += grdInputToolRepairSend_CellValueChanged;
                    return;
                }
                GetSingleReceiptArea(row.GetString("RECEIPTAREA"), e.RowHandle);
                grdInputToolRepairSend.View.CellValueChanged += grdInputToolRepairSend_CellValueChanged;
            }
        }
        #endregion

        #region grdInputToolRepairSend_ShowingEditor - 그리드의 입력제한 이벤트
        private void grdInputToolRepairSend_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (_currentStatus != "added")
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region BtnErase_Click - 제거버튼 이벤트
        private void BtnErase_Click(object sender, EventArgs e)
        {
            if (IsCurrentProcess())
                DeleteLotGridRow();
            else
                this.ShowMessage(MessageBoxButtons.OK, "CURRENTPROCESSREMOVEDATA", "");
        }
        #endregion

        #region BtnModify_Click - 수정버튼이벤트
        private void BtnModify_Click(object sender, EventArgs e)
        {
            if (IsCurrentProcess())
                SaveData();
            else
                this.ShowMessage(MessageBoxButtons.OK, "CURRENTPROCESSUUPDATEDATA", "");
        }
        #endregion

        #region grdToolUpdateSend_FocusedRowChanged - 그리드의 선택행 변경이벤트
        private void grdToolUpdateSend_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayToolRepairSendInfo();
        }
        #endregion

        #region BtnInitialize_Click - 초기화버튼클릭이벤트
        private void BtnInitialize_Click(object sender, EventArgs e)
        {
            InitializeInsertForm();
        }
        #endregion

        #region BtnSave_Click - 저장버튼이벤트
        private void BtnSave_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region BtnSearchTool_Click - 수리입고할 치공구를 조회한다.
        private void BtnSearchTool_Click(object sender, EventArgs e)
        {
            if(!IsCurrentProcess())
            {
                this.ShowMessage(MessageBoxButtons.OK, "AfterInitValidation", "");
                return;
            }

            if(Conditions.Validation())
                DisplayUpdateToolPopup();
            //if (cboToolRepairType.EditValue != null)
            //{
            //    if (cboToolRepairType.EditValue.ToString() != "")
            //        DisplayUpdateToolPopup();
            //    else
            //        this.ShowMessage(MessageBoxButtons.OK, "AfterInitValidationRepairType", "");
            //}
            //else
            //    this.ShowMessage(MessageBoxButtons.OK, "AfterInitValidationRepairType", "");
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
                BtnModify_Click(sender, e);
            else if (btn.Name.ToString().Equals("Delete"))
                BtnErase_Click(sender, e);
            else if (btn.Name.ToString().Equals("SelectTool"))
                BtnSearchTool_Click(sender, e);
            else if (btn.Name.ToString().Equals("Initialization"))
                BtnInitialize_Click(sender, e);
            else if (btn.Name.ToString().Equals("Cancel"))
                CancelData();
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
            if (Conditions.GetValue("USERNAME").ToString() != "")
                values.Add("SENDUSER", _searchUserID);
            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolRepairResult = SqlExecuter.Query("GetToolRepairSendListByTool", "10001", values);

            grdToolRepiarSend.DataSource = toolRepairResult;

            if (toolRepairResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            else
            {
                grdToolRepiarSend.View.FocusedRowHandle = 0;
                DisplayToolRepairSendInfo(); 
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
        private void Research(string sendSequence, string sendDate)
        {
            // TODO : 조회 SP 변경
            InitializeInsertForm();

            var values = Conditions.GetValues();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);
            if (Conditions.GetValue("USERNAME").ToString() != "")
                values.Add("SENDUSER", _searchUserID);
            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable sparePartSearchResult = SqlExecuter.Query("GetToolRepairSendListByTool", "10001", values);

            if (sparePartSearchResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdToolRepiarSend.DataSource = sparePartSearchResult;

            //현재 작업한 결과를 선택할 수 있도록 한다.
            if (sendSequence == null && sendDate == null) //삭제 작업후
            {
                //값이 하나도 없다면 빈 화면
                if (grdToolRepiarSend.View.RowCount == 0)
                {
                    //입력대기 화면으로 설정
                    InitializeInsertForm();
                }
                else
                {
                    //첫번재 값으로 설정
                    grdToolRepiarSend.View.FocusedRowHandle = 0;
                    DisplayToolRepairSendInfo();
                }
            }
            else //값이 있다면 해당 값에 맞는 행을 찾아서 선택
            {
                ViewSavedData(sendDate, sendSequence);
            }

            _currentStatus = "modified";
        }
        #endregion

        #region SearchLotList - 수정출고정보의 LOT을 조회한다.
        /// <summary>
        /// SearchLotList - 수정출고정보의 LOT을 조회한다.
        /// </summary>
        private void SearchLotList(string sendDate, string sendSequence)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("SENDDATE", Convert.ToDateTime(sendDate).ToString("yyyy-MM-dd HH:mm:ss"));
            values.Add("SENDSEQUENCE", sendSequence);
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);
            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable searchResult = SqlExecuter.Query("GetToolRepairLotListForRepairByTool", "10001", values);

            grdInputToolRepairSend.DataSource = searchResult;

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
            //InitializeConditionPlant();
            InitializeConditionAreaPopup();
            InitializeConditionUserPopup();
            InitializeConditionToolCodePopup();
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
        private void InitializeConditionPlant()
        {
            var planttxtbox = Conditions.AddComboBox("PLANTID", new SqlQuery("GetPlantList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
               .SetLabel("PLANT")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.1)
               .SetIsReadOnly(true)
               .SetDefault(UserInfo.Current.Plant, "PLANTID") //기본값 설정 UserInfo.Current.Plant
               .SetValidationIsRequired()
            ;
        }

        /// <summary>
        /// 작업장 설정 
        /// </summary>
        private void InitializeConditionArea()
        {
            var areaBox = Conditions.AddComboBox("AREAID", new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "AREANAME", "AREAID")
               .SetLabel("AREA")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.2)
               .SetValidationIsRequired()
               .SetDefault(UserInfo.Current.Area, "AREAID") //기본값 설정 UserInfo.Current.Plant
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
            .SetLabel("AREAID")
            .SetPopupResultCount(1)
            .SetPosition(0.2)
            .SetPopupResultMapping("AREAID", "AREAID");

            areaCondition.SetValidationIsRequired();
            areaCondition.ValueFieldName = "AREAID";
            areaCondition.DisplayFieldName = "AREANAME";
            //areaCondition.SetDefault(UserInfo.Current.Area, "AREAID");


            areaCondition.Conditions.AddTextBox("AREANAME");

            areaCondition.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetIsHidden();
            areaCondition.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }
        #endregion

        /// <summary>
        /// 사용자 조회 팝업
        /// </summary>
        private void InitializeConditionUserPopup()
        {
            userCodePopup = Conditions.AddSelectPopup("USERNAME", new SqlQuery("GetUserListForPopupByTool", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
            .SetPopupLayout("USERNAME", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow)
            .SetPopupResultMapping("USERNAME", "USERNAME")
            .SetLabel("SENDUSER")
            .SetPopupResultCount(1)
            .SetPosition(3.2)
            .SetPopupApplySelection((selectedRows, dataGridRow) =>
            {
                int i = 0;

                foreach (DataRow row in selectedRows)
                {
                    if (i == 0)
                    {
                        _searchUserID = row["USERID"].ToString();
                    }
                    i++;
                }
            })
            ;

            // 팝업 조회조건            
            userCodePopup.Conditions.AddTextBox("USERNAME")
                .SetLabel("USERNAME");

            // 팝업 그리드
            userCodePopup.GridColumns.AddTextBoxColumn("USERID", 10)
                .SetIsHidden();
            userCodePopup.GridColumns.AddTextBoxColumn("USERNAME", 150)
                .SetIsReadOnly();
            userCodePopup.GridColumns.AddTextBoxColumn("DEPARTMENT", 150)
                .SetIsReadOnly();
            userCodePopup.GridColumns.AddTextBoxColumn("DUTY", 100)
                .SetIsReadOnly();
            userCodePopup.GridColumns.AddTextBoxColumn("PLANT", 150)
                .SetIsReadOnly();
            userCodePopup.GridColumns.AddTextBoxColumn("ENTERPRISE", 200)
                .SetIsReadOnly();
        }

        /// <summary>
        /// 품목코드 조회 설정
        /// </summary>
        private void InitializeConditionToolCodePopup()
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

        #region InitializeRepairVendors : 팝업창 제어
        /// <summary>
        /// 팝업창 제어
        /// </summary>
        private void InitializeRepairVendors()
        {
            vendorPopup = new ConditionItemSelectPopup();
            vendorPopup.Id = "MAKEVENDOR";
            vendorPopup.SearchQuery = new SqlQuery("GetVendorListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            vendorPopup.ValueFieldName = "VENDORID";
            vendorPopup.DisplayFieldName = "VENDORNAME";
            vendorPopup.SetPopupLayout("MAKEVENDOR", PopupButtonStyles.Ok_Cancel, true, true);
            vendorPopup.SetPopupResultCount(1);
            vendorPopup.SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow);
            vendorPopup.SetPopupAutoFillColumns("VENDORNAME");
            vendorPopup.SetPopupResultMapping("MAKEVENDOR", "VENDORNAME");
            vendorPopup.SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                //작업장 변경 시 작업자 조회
                selectedRows.Cast<DataRow>().ForEach(row =>
                {
                    _selectedVendor = row.GetString("VENDORID");
                    popEditVendor.EditValue = row.GetString("VENDORNAME");
                    InsertRepairVendor(row.GetString("VENDORNAME"));
                });

            });


            vendorPopup.Conditions.AddTextBox("VENDORNAME");

            vendorPopup.GridColumns.AddTextBoxColumn("VENDORID", 150)
                .SetIsHidden();
            vendorPopup.GridColumns.AddTextBoxColumn("VENDORNAME", 200);

            popEditVendor.SelectPopupCondition = vendorPopup;
        }
        #endregion

        #region ComboBox  초기화
        /// <summary>
        /// Factory ComboBox를 초기화한다. 검색조건의 Site에 따라 값이 변경되어야 하므로
        /// 초기화 버튼 클릭시 재로딩하는 것으로 한다.
        /// </summary>
        /// <param name="plantID"></param>
        private void InitializeToolRepairType()
        {
            // 검색조건에 정의된 공장을 정리
            cboToolRepairType.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboToolRepairType.ValueMember = "TOOLREPAIRTYPE";
            cboToolRepairType.DisplayMember = "TOOLREPAIRTYPENAME";
            cboToolRepairType.EditValue = "1";
            cboToolRepairType.UseEmptyItem = true;
            cboToolRepairType.EmptyItemCaption = "";
            cboToolRepairType.EmptyItemValue = "";

            cboToolRepairType.DataSource = SqlExecuter.Query("GetToolRepairTypeByTool", "10001", new Dictionary<string, object>() { {"LANGUAGETYPE", UserInfo.Current.LanguageType}});

            cboToolRepairType.ShowHeader = false;
        }
        #endregion
        #endregion

        #region 유효성 검사


        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
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
            //각각의 상태에 따라 보여줄 메세지가 상이하다면 각각의 분기에서 메세지를 표현해야 한다.            
            messageCode = "VALIDATEREQUIREDVALUES";

            if (!ValidateEditValue(deSendDate.EditValue))
                return false;

            if (grdInputToolRepairSend.View.RowCount == 0)
                return false;

            if (!ValidateEditValue(txtSendor.EditValue))
                return false;

            DataTable dataTables = grdInputToolRepairSend.GetChangedRows();

            foreach (DataRow currentRow in dataTables.Rows)
            {
                if (!ValidateCellInGrid(currentRow, new string[] { "VENDORID", "TOOLREPAIRTYPE", "RECEIPTAREAID", "REPAIRDESCRIPTION"}))
                    return false;

                if(currentRow.GetString("ISMODIFY").Equals("N"))
                {
                    messageCode = "ValidateRepairToolReqArea"; //수리출고할 치공구(들)의 작업장중 권한이 없는 작업장이 있습니다.
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region ValidateCancelContent : 저장하기 전 Validation을 체크한다.
        /// <summary>
        /// 입력/수정 시의 Validation 체크
        /// 입력/수정 각각의 체크값이 서로 다르다면 상태에 따른 분기가 있어야 한다.
        /// </summary>
        /// <returns></returns>
        private bool ValidateCancelContent(out string messageCode)
        {
            //각각의 상태에 따라 보여줄 메세지가 상이하다면 각각의 분기에서 메세지를 표현해야 한다.            
            messageCode = "ValidateCancelRepairToolRequest"; //수리결과등록되거나 입고된 치공구는 출고취소할 수 없습니다.

            DataTable dataTables = grdInputToolRepairSend.View.GetCheckedRows();

            foreach (DataRow currentRow in dataTables.Rows)
            {
                if (currentRow.GetString("RESULTSTATUS").Equals("Result"))
                    return false;
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

                //의뢰일은 오늘날짜를 기본으로 한다.
                DateTime dateNow = DateTime.Now;
                deSendDate.EditValue = dateNow;

                //의뢰순번은 공값으로 한다.
                txtSendSequence.EditValue = null;

                //의뢰부서는 사용자의 부서를 기본으로 한다.
                txtSendor.EditValue = UserInfo.Current.Name;

                _sendUserID = UserInfo.Current.Id;

                cboToolRepairType.EditValue = null;

                _selectedVendor = "";

                popEditVendor.EditValue = null;

                grdInputToolRepairSend.DataSource = null;

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

        #region InitialRequiredForm : 필수입력항목을 설정한다.
        /// <summary>
        /// 필수입력항목을 설정한다.
        /// </summary>
        private void InitialRequriedForm()
        {
            SetRequiredValidationControl(lblSendDate);
            SetRequiredValidationControl(lblSendUser);
            SetRequiredValidationControl(lblRepairType);
            SetRequiredValidationControl(lblRepairVendor);
        }
        #endregion

        #region InsertRepairVendor : 수리업체를 선정하면 그리드의 데이터가 변경되어야 한다.
        private void InsertRepairVendor(string vendorName)
        {
            for (int i = 0; i < grdInputToolRepairSend.View.RowCount; i++)
                grdInputToolRepairSend.View.SetRowCellValue(i, "REPAIRVENDOR", vendorName);
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
                deSendDate.ReadOnly = false;
                txtSendSequence.ReadOnly = true;
                txtSendor.ReadOnly = true;
            }
            else if (currentStatus == "modified") //
            {
                deSendDate.ReadOnly = true;
                txtSendSequence.ReadOnly = true;
                txtSendor.ReadOnly = true;
            }
            else
            {
                deSendDate.ReadOnly = true;
                txtSendSequence.ReadOnly = true;
                txtSendor.ReadOnly = true;
            }
        }
        #endregion

        #region LoadData - 팝업창에서 호출하는 메소드
        /// <summary>
        /// 팝업창에서 호출하는 메소드
        /// </summary>
        private void LoadData(DataTable table)
        {
            DataTable originTable = (DataTable)grdInputToolRepairSend.DataSource;

            if (originTable == null)
            {
                originTable = CreateSaveDatatable(false);

                //값이 없을 경우 빈 테이블을 바인딩
                grdInputToolRepairSend.DataSource = originTable;
            }
            DateTime sendDate = (DateTime)deSendDate.EditValue;
            foreach (DataRow inputRow in table.Rows)
            {
                grdInputToolRepairSend.View.AddNewRow();

                grdInputToolRepairSend.View.FocusedRowHandle = grdInputToolRepairSend.View.RowCount - 1;
                string userName = "", vendorName = "";

                if(txtSendor.EditValue != null)
                    userName = txtSendor.EditValue.ToString();

                if (popEditVendor.EditValue != null)
                    vendorName = popEditVendor.EditValue.ToString();

                grdInputToolRepairSend.View.SetFocusedRowCellValue("REQUESTDATE", inputRow["REQUESTDATE"]);
                grdInputToolRepairSend.View.SetFocusedRowCellValue("REQUESTSEQUENCE", inputRow["REQUESTSEQUENCE"]);
                grdInputToolRepairSend.View.SetFocusedRowCellValue("SENDDATE", sendDate.ToString("yyyy-MM-dd HH:mm:ss"));
                grdInputToolRepairSend.View.SetFocusedRowCellValue("SENDSEQUENCE", "");
                grdInputToolRepairSend.View.SetFocusedRowCellValue("USERID", _sendUserID);
                grdInputToolRepairSend.View.SetFocusedRowCellValue("SENDOR", userName);
                grdInputToolRepairSend.View.SetFocusedRowCellValue("REPAIRVENDOR", vendorName);
                grdInputToolRepairSend.View.SetFocusedRowCellValue("TOOLNUMBER", inputRow["TOOLNUMBER"]);                
                grdInputToolRepairSend.View.SetFocusedRowCellValue("RECEIPTAREA", inputRow["AREANAME"]);
                grdInputToolRepairSend.View.SetFocusedRowCellValue("RECEIPTAREAID", inputRow["AREAID"]);
                grdInputToolRepairSend.View.SetFocusedRowCellValue("ISMODIFY", inputRow["ISMODIFY"]);
                grdInputToolRepairSend.View.SetFocusedRowCellValue("PRODUCTDEFID", inputRow["PRODUCTDEFID"]);
                grdInputToolRepairSend.View.SetFocusedRowCellValue("PRODUCTDEFNAME", inputRow["PRODUCTDEFNAME"]);
                grdInputToolRepairSend.View.SetFocusedRowCellValue("USEDCOUNT", inputRow["USEDCOUNT"]);
                grdInputToolRepairSend.View.SetFocusedRowCellValue("CLEANLIMIT", inputRow["CLEANLIMIT"]);
                grdInputToolRepairSend.View.SetFocusedRowCellValue("TOTALCLEANCOUNT", inputRow["TOTALCLEANCOUNT"]);
                grdInputToolRepairSend.View.SetFocusedRowCellValue("TOTALUSEDCOUNT", inputRow["TOTALUSEDCOUNT"]);
                grdInputToolRepairSend.View.SetFocusedRowCellValue("USEDLIMIT", inputRow["USEDLIMIT"]);
                grdInputToolRepairSend.View.SetFocusedRowCellValue("TOTALREPAIRCOUNT", inputRow["TOTALREPAIRCOUNT"]);
                grdInputToolRepairSend.View.SetFocusedRowCellValue("MAKEVENDOR", inputRow["MAKEVENDOR"]);
                grdInputToolRepairSend.View.SetFocusedRowCellValue("VENDORID", inputRow["VENDORID"]);
                grdInputToolRepairSend.View.SetFocusedRowCellValue("TOOLCODE", inputRow["TOOLCODE"]);
                grdInputToolRepairSend.View.SetFocusedRowCellValue("DURABLEDEFVERSION", inputRow["DURABLEDEFVERSION"]);
                grdInputToolRepairSend.View.SetFocusedRowCellValue("SENDAREA", inputRow["AREANAME"]);
            }

            //첫번째 행에서 포커스를 띄워놓아야 X표시가 안뜬다.
            grdInputToolRepairSend.View.AddNewRow();

            grdInputToolRepairSend.View.DeleteRow(grdInputToolRepairSend.View.RowCount - 1);
        }
        #endregion

        #region CreateSaveParentDatatable : ToolRepairSend 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// <summary>
        /// ToolRequest 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// 입력시에는 ToolRequest와 ToolRequestDetail에 입력하고
        /// 수정시에는 ToolRequest와 ToolRequestDetail 및 ToolRequestDetailLot에 까지 데이터를 기입한다.
        /// </summary>
        /// <returns></returns>
        private DataTable CreateSaveParentDatatable(bool useState)
        {
            DataTable dt = new DataTable();
            dt.TableName = "toolRepairSendList";
            //===================================================================================
            //Key값 ToolRequest 및 ToolRequestDetail
            dt.Columns.Add("SENDDATE");
            dt.Columns.Add("SENDSEQUENCE");
            dt.Columns.Add("SENDOR");
            dt.Columns.Add("TOOLNUMBER");
            dt.Columns.Add("RECEIPTAREA");
            dt.Columns.Add("SENDAREA");
            dt.Columns.Add("PRODUCTDEFID");
            dt.Columns.Add("PRODUCTDEFNAME");
            dt.Columns.Add("REQUESTDATE");
            dt.Columns.Add("REQUESTSEQUENCE");
            dt.Columns.Add("USEDCOUNT");
            dt.Columns.Add("CLEANLIMIT");
            dt.Columns.Add("TOTALCLEANCOUNT");
            dt.Columns.Add("TOTALUSEDCOUNT");
            dt.Columns.Add("USEDLIMIT");
            dt.Columns.Add("TOTALREPAIRCOUNT");
            dt.Columns.Add("MAKEVENDOR");
            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");

            dt.Columns.Add("DESCRIPTION");
            dt.Columns.Add("TOOLREPAIRTYPE");

            dt.Columns.Add("INSERTTYPE");

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
            //Key값 ToolRequest 및 ToolRequestDetail
            dt.Columns.Add("SENDDATE");
            dt.Columns.Add("SENDSEQUENCE");
            dt.Columns.Add("SENDOR");
            dt.Columns.Add("USERID");
            dt.Columns.Add("REPAIRVENDOR");
            dt.Columns.Add("TOOLNUMBER");
            dt.Columns.Add("RECEIPTAREA");
            dt.Columns.Add("RECEIPTAREAID");
            dt.Columns.Add("AREAID");
            dt.Columns.Add("SENDAREA");
            dt.Columns.Add("PRODUCTDEFID");
            dt.Columns.Add("PRODUCTDEFNAME");
            dt.Columns.Add("REQUESTDATE");
            dt.Columns.Add("REQUESTSEQUENCE");
            dt.Columns.Add("TOOLCODE");
            dt.Columns.Add("TOOLNAME");
            dt.Columns.Add("DURABLEDEFVERSION");
            dt.Columns.Add("USEDCOUNT");
            dt.Columns.Add("CLEANLIMIT");
            dt.Columns.Add("TOTALCLEANCOUNT");
            dt.Columns.Add("TOTALUSEDCOUNT");
            dt.Columns.Add("USEDLIMIT");
            dt.Columns.Add("TOTALREPAIRCOUNT");
            dt.Columns.Add("MAKEVENDOR");
            dt.Columns.Add("VENDORID");

            dt.Columns.Add("REPAIRVENDORID");
            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");

            dt.Columns.Add("TOOLREPAIRTYPE");
            dt.Columns.Add("ISMODIFY");

            dt.Columns.Add("FINISHDATE");
            dt.Columns.Add("FINISHER");
            dt.Columns.Add("RECEIPTDATE");
            dt.Columns.Add("RECEIPTSEQUENCE");
            dt.Columns.Add("LOTHISTKEY");
            dt.Columns.Add("DESCRIPTION");
            dt.Columns.Add("REPAIRDESCRIPTION");

            dt.Columns.Add("WEIGHT");
            dt.Columns.Add("HEIGHT");
            dt.Columns.Add("HORIZONTAL");
            dt.Columns.Add("VERTICAL");
            dt.Columns.Add("THICKNESS");

            dt.Columns.Add("INSERTTYPE");

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
         
        #region DisplayUpdateToolPopup : 팝업창을 호출한다.
        private void DisplayUpdateToolPopup()
        {
            Popup.ToolNumberForRepairPopup toolNumberPopup = new Popup.ToolNumberForRepairPopup(Conditions.GetValue("P_PLANTID").ToString());
            toolNumberPopup.choiceHandler += LoadData;

            toolNumberPopup.SetSendArea(
                Conditions.GetValue("AREAID").ToString()
                , Conditions.GetControl("AREAID").Text
                );
            toolNumberPopup.ShowDialog();
        }
        #endregion

        #region DisplayToolRepairSendInfo : 그리드에서 행 선택시 상세정보를 표시
        /// <summary>
        /// 그리드에서 행 선택시 상세정보를 표시하는 메소드
        /// </summary>
        private void DisplayToolRepairSendInfo()
        {
            //포커스 행 체크 
            if (grdToolRepiarSend.View.FocusedRowHandle < 0) return;

            DisplayToolRepairSendInfoDetail(grdToolRepiarSend.View.GetFocusedDataRow());
        }
        #endregion

        #region ViewSavedData : 새로 입력된 데이터를 화면에 바인딩
        /// <summary>
        /// 새로 입력된 데이터를 화면에 바인딩
        /// </summary>
        private void ViewSavedData(string sendDate, string sendSequence)
        {
            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            //values.Add("PLANTID", UserInfo.Current.Plant);
            values.Add("SENDDATE", Convert.ToDateTime(sendDate).ToString("yyyy-MM-dd HH:mm:ss"));
            values.Add("SENDSEQUENCE", Int32.Parse(sendSequence));
            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable savedResult = SqlExecuter.Query("GetToolRepairSendListByTool", "10001", values);

            if (savedResult.Rows.Count > 0)
            {
                DisplayToolRepairSendInfoDetail(savedResult.Rows[0]);
            }
        }
        #endregion

        #region DisplayToolRepairSendInfoDetail : 입력받은 인덱스의 그리드의 내용을 화면에 바인딩
        /// <summary>
        /// 입력받은 그리드내의 인덱스에 대한 내용을 화면에 표시한다.
        /// </summary>
        /// <param name="rowHandle"></param>
        private void DisplayToolRepairSendInfoDetail(DataRow currentRow)
        {
            //해당 업무에 맞는 Enable체크 수행
            //grdToolRepiarSend.View.FocusedRowHandle = rowHandle;
            //DataRow currentRow = grdToolRepiarSend.View.GetFocusedDataRow();

            //수정상태라 판단하여 화면 제어
            ControlEnableProcess("modified");

            //화면상태에 따른 탭및 그리드 제어
            deSendDate.EditValue = currentRow.GetString("SENDDATE");
            _sendUserID = currentRow.GetString("SENDORID");
            txtSendor.EditValue = currentRow.GetString("SENDOR");
            _selectedVendor = currentRow.GetString("MAKEVENDORID");
            popEditVendor.EditValue = currentRow.GetString("MAKEVENDOR");
            cboToolRepairType.EditValue = currentRow.GetString("TOOLREPAIRTYPEID");

            _currentStatus = "modified";

            //그리드 데이터 바인딩
            SearchLotList(currentRow.GetString("SENDDATE"), currentRow.GetString("SENDSEQUENCE"));
            //각 순서에 따라 바인딩

            //그리드의 수정가능 여부
        }
        #endregion

        #region GetRowHandleInGrid : 특정아이디의 값을 바탕으로 해당 내용을 그리드에서 찾은후 결과값의 그리드내의 인덱스를 반환한다.
        /// <summary>
        /// 특정아이디의 값을 바탕으로 해당 내용을 그리드에서 찾은후 결과값의 그리드내의 인덱스를 반환한다.
        /// 현재로선 String비교만을 한다. DateTime및 기타 다른 값들에 대해선 지원하지 않음
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="findValue"></param>
        /// <returns></returns>
        private int GetRowHandleInGrid(SmartBandedGrid targetGrid, string firstColumnName, string secondColumnName, string firstFindValue, string secondFindValue)
        {
            for (int i = 0; i < targetGrid.View.RowCount; i++)
            {
                if (firstFindValue.Equals(targetGrid.View.GetDataRow(i)[firstColumnName].ToString()) && secondFindValue.Equals(targetGrid.View.GetDataRow(i)[secondColumnName].ToString()))
                {
                    return i;
                }
            }
            return -1;
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
                btnInitialize.Enabled = false;
                btnModify.Enabled = false;
                btnErase.Enabled = false;
                if (grdInputToolRepairSend.View.RowCount > 0)
                {
                    string messageCode = "";
                    //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                    if (ValidateContent(out messageCode))
                    {
                        DataSet toolRepairSet = new DataSet();
                        //치공구 제작의뢰를 입력
                        DataTable toolRepairTable = CreateSaveParentDatatable(true);

                        DataRow toolRepairRow = toolRepairTable.NewRow();

                        DateTime sendDate = DateTime.Now;

                        DataRow repairRow = grdToolRepiarSend.View.GetFocusedDataRow();

                        toolRepairRow["SENDDATE"] = sendDate.ToString("yyyy-MM-dd HH:mm:ss");
                        toolRepairRow["SENDOR"] = _sendUserID;
                        toolRepairRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                        toolRepairRow["PLANTID"] = Conditions.GetValue("P_PLANTID");


                        //수리출고인지 수정출고인지 구분
                        toolRepairRow["INSERTTYPE"] = "REPAIR";

                        //toolRepairRow["REPAIRVENDOR"] = _selectedVendor;
                        toolRepairRow["VALIDSTATE"] = "Valid";

                        //현재 화면의 상태에 따라 생성/수정으로 분기된다.
                        if (_currentStatus == "added")
                        {
                            toolRepairRow["SENDSEQUENCE"] = "";
                            //수정출고의 첫번째 업체를 선정한다.
                            //toolRepairRow["MAKEVENDOR"] = _selectedVendor;
                            toolRepairRow["CREATOR"] = UserInfo.Current.Id;
                            toolRepairRow["_STATE_"] = "added";
                        }
                        else
                        {
                            toolRepairRow["SENDSEQUENCE"] = repairRow.GetString("SENDSEQUENCE");
                            //toolRepairRow["MAKEVENDOR"] = _selectedVendor;
                            toolRepairRow["MODIFIER"] = UserInfo.Current.Id;
                            toolRepairRow["_STATE_"] = "modified";
                        }

                        toolRepairTable.Rows.Add(toolRepairRow);

                        toolRepairSet.Tables.Add(toolRepairTable);

                        //제작구분에 따라 제작탭의 그리드, 수정탭의 그리드를 각각 따로 입력이 된다.

                        //제작일 경우
                        if (grdInputToolRepairSend.View.RowCount > 0)
                        {
                            DataTable repairSendLotTable = CreateSaveDatatable(true);
                            DataTable changedRepairSendLotTable = grdInputToolRepairSend.GetChangedRows();

                            foreach (DataRow currentRow in changedRepairSendLotTable.Rows)
                            {
                                DataRow repairSendLotRow = repairSendLotTable.NewRow();

                                repairSendLotRow["TOOLNUMBER"] = currentRow.GetString("TOOLNUMBER");
                                repairSendLotRow["SENDDATE"] = sendDate.ToString("yyyy-MM-dd HH:mm:ss");

                                repairSendLotRow["FINISHDATE"] = "";
                                repairSendLotRow["FINISHER"] = "";
                                //repairSendLotRow["WEIGHT"] = currentRow.GetString("WEIGHT");
                                //repairSendLotRow["HEIGHT"] = currentRow.GetString("HEIGHT");
                                //repairSendLotRow["LENGTH"] = currentRow.GetString("LENGTH");
                                //repairSendLotRow["THICKNESS"] = currentRow.GetString("THICKNESS");
                                repairSendLotRow["AREAID"] = currentRow.GetString("RECEIPTAREAID");
                                repairSendLotRow["RECEIPTDATE"] = "";
                                repairSendLotRow["RECEIPTSEQUENCE"] = "";
                                repairSendLotRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                                repairSendLotRow["PLANTID"] = Conditions.GetValue("P_PLANTID");
                                repairSendLotRow["LOTHISTKEY"] = "";
                                repairSendLotRow["DESCRIPTION"] = currentRow.GetString("REPAIRDESCRIPTION");

                                repairSendLotRow["TOOLREPAIRTYPE"] = currentRow.GetString("TOOLREPAIRTYPE");
                                repairSendLotRow["MAKEVENDOR"] = currentRow.GetString("VENDORID");

                                repairSendLotRow["REQUESTDATE"] = currentRow.GetString("REQUESTDATE");
                                repairSendLotRow["REQUESTSEQUENCE"] = currentRow.GetString("REQUESTSEQUENCE");
                                repairSendLotRow["TOOLCODE"] = currentRow.GetString("TOOLCODE");
                                repairSendLotRow["DURABLEDEFVERSION"] = currentRow.GetString("DURABLEDEFVERSION");

                                //수정출고인지 수리출고인지 구분
                                repairSendLotRow["INSERTTYPE"] = "REPAIR";
                                //내역등록입력종료

                                repairSendLotRow["CREATOR"] = UserInfo.Current.Id;
                                repairSendLotRow["MODIFIER"] = UserInfo.Current.Id;

                                if (currentRow["_STATE_"].ToString() == "added")
                                {
                                    repairSendLotRow["SENDSEQUENCE"] = "";
                                    repairSendLotRow["_STATE_"] = "added";
                                    repairSendLotRow["VALIDSTATE"] = "Valid";
                                }
                                else if (currentRow["_STATE_"].ToString() == "modified")
                                {
                                    repairSendLotRow["SENDSEQUENCE"] = repairRow.GetString("SENDSEQUENCE");
                                    repairSendLotRow["_STATE_"] = "modified";
                                    repairSendLotRow["VALIDSTATE"] = "Valid";
                                }
                                else if (currentRow["_STATE_"].ToString() == "deleted")
                                {
                                    repairSendLotRow["SENDSEQUENCE"] = repairRow.GetString("SENDSEQUENCE");
                                    repairSendLotRow["_STATE_"] = "deleted";
                                    repairSendLotRow["VALIDSTATE"] = "InValid";
                                }

                                repairSendLotTable.Rows.Add(repairSendLotRow);
                            }

                            toolRepairSet.Tables.Add(repairSendLotTable);
                        }
                        DataTable resultTable = this.ExecuteRule<DataTable>("ToolRepairSend", toolRepairSet);

                        ////Server에서 결과값을 현재는 반환하지 않음 만약 반환해야 한다면 아래로직 구현
                        DataRow resultRow = resultTable.Rows[0];

                        ControlEnableProcess("modified");

                        ////가져온 결과값에 따라 재검색한 후 가져온 아이디에 맞는 행을 선택으로 변경한다.
                        Research(resultRow.GetString("SENDSEQUENCE"), sendDate.ToString("yyyy-MM-dd"));
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
                btnInitialize.Enabled = true;
                btnModify.Enabled = true;
                btnErase.Enabled = true;
            }
        }
        #endregion

        #region CancelData : 입력/수정을 수행
        private void CancelData()
        {
            //Validation 체크 부분 작성 필요
            this.ShowWaitArea();

            //저장 로직
            try
            {
                btnInitialize.Enabled = false;
                btnModify.Enabled = false;
                btnErase.Enabled = false;
                if (grdInputToolRepairSend.View.RowCount > 0)
                {
                    string messageCode = "";
                    //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                    if (ValidateCancelContent(out messageCode))
                    {
                        DataSet toolRepairSet = new DataSet();
                        //치공구 제작의뢰를 입력
                        DataTable toolRepairTable = CreateSaveParentDatatable(true);

                        DataRow toolRepairRow = toolRepairTable.NewRow();

                        DateTime sendDate = DateTime.Now;

                        DataRow repairRow = grdToolRepiarSend.View.GetFocusedDataRow();

                        toolRepairRow["SENDDATE"] = Convert.ToDateTime(repairRow.GetString("SENDDATE")).ToString("yyyy-MM-dd HH:mm:ss");
                        toolRepairRow["SENDOR"] = _sendUserID;
                        toolRepairRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                        toolRepairRow["PLANTID"] = Conditions.GetValue("P_PLANTID");


                        //수리출고인지 수정출고인지 구분
                        toolRepairRow["INSERTTYPE"] = "REPAIR";

                        //toolRepairRow["REPAIRVENDOR"] = _selectedVendor;
                        toolRepairRow["VALIDSTATE"] = "Invalid";
                        toolRepairRow["_STATE_"] = "deleted";
                        toolRepairRow["SENDSEQUENCE"] = repairRow.GetString("SENDSEQUENCE");
                        toolRepairRow["MODIFIER"] = UserInfo.Current.Id;
                        toolRepairRow["CREATOR"] = UserInfo.Current.Id;
                        
                        toolRepairTable.Rows.Add(toolRepairRow);

                        toolRepairSet.Tables.Add(toolRepairTable);

                        //제작구분에 따라 제작탭의 그리드, 수정탭의 그리드를 각각 따로 입력이 된다.

                        //제작일 경우
                        if (grdInputToolRepairSend.View.RowCount > 0)
                        {
                            DataTable repairSendLotTable = CreateSaveDatatable(true);
                            DataTable changedRepairSendLotTable = grdInputToolRepairSend.View.GetCheckedRows();

                            foreach (DataRow currentRow in changedRepairSendLotTable.Rows)
                            {
                                DataRow repairSendLotRow = repairSendLotTable.NewRow();

                                repairSendLotRow["TOOLNUMBER"] = currentRow.GetString("TOOLNUMBER");
                                repairSendLotRow["SENDDATE"] = Convert.ToDateTime(currentRow.GetString("SENDDATE")).ToString("yyyy-MM-dd HH:mm:ss");

                                repairSendLotRow["FINISHDATE"] = "";
                                repairSendLotRow["FINISHER"] = "";
                                repairSendLotRow["AREAID"] = currentRow.GetString("RECEIPTAREAID");
                                repairSendLotRow["RECEIPTDATE"] = "";
                                repairSendLotRow["RECEIPTSEQUENCE"] = "";
                                repairSendLotRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                                repairSendLotRow["PLANTID"] = Conditions.GetValue("P_PLANTID");
                                repairSendLotRow["LOTHISTKEY"] = "";
                                repairSendLotRow["DESCRIPTION"] = currentRow.GetString("REPAIRDESCRIPTION");

                                repairSendLotRow["TOOLREPAIRTYPE"] = currentRow.GetString("TOOLREPAIRTYPE");
                                repairSendLotRow["MAKEVENDOR"] = currentRow.GetString("VENDORID");

                                repairSendLotRow["REQUESTDATE"] = currentRow.GetString("REQUESTDATE");
                                repairSendLotRow["REQUESTSEQUENCE"] = currentRow.GetString("REQUESTSEQUENCE");
                                repairSendLotRow["TOOLCODE"] = currentRow.GetString("TOOLCODE");
                                repairSendLotRow["DURABLEDEFVERSION"] = currentRow.GetString("DURABLEDEFVERSION");

                                //수정출고인지 수리출고인지 구분
                                repairSendLotRow["INSERTTYPE"] = "REPAIR";
                                //내역등록입력종료

                                repairSendLotRow["CREATOR"] = UserInfo.Current.Id;
                                repairSendLotRow["MODIFIER"] = UserInfo.Current.Id;

                                repairSendLotRow["SENDSEQUENCE"] = currentRow.GetString("SENDSEQUENCE");
                                repairSendLotRow["_STATE_"] = "deleted";
                                repairSendLotRow["VALIDSTATE"] = "Invalid";


                                repairSendLotTable.Rows.Add(repairSendLotRow);
                            }

                            toolRepairSet.Tables.Add(repairSendLotTable);
                        }
                        this.ExecuteRule<DataTable>("ToolRepairSend", toolRepairSet);


                        ControlEnableProcess("modified");

                        ////가져온 결과값에 따라 재검색한 후 가져온 아이디에 맞는 행을 선택으로 변경한다.
                        Research(null, null);
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
                btnInitialize.Enabled = true;
                btnModify.Enabled = true;
                btnErase.Enabled = true;
            }
        }
        #endregion

        #region DeleteLotGridRow : 삭제버튼을 통해 선택한 행을 삭제한다.
        private void DeleteLotGridRow()
        {
            if (grdInputToolRepairSend.View.GetCheckedRows().Rows.Count > 0)
            {
                grdInputToolRepairSend.View.DeleteCheckedRows();
            }
            else
            {
                //ShowMessage(MessageBoxButtons.OK, String.Format("SelectItem", Language.GetDictionary("DURABLE")), "");
                ShowMessage(MessageBoxButtons.OK, "SelectItem", Language.Get("DURABLE"));
            }
        }
        #endregion

        #region IsCurrentProcess : 현재 화면 상태에 따라 버튼 동작유무를 결정한다.
        /// <summary>
        /// 현재 화면 상태에 따라 버튼 동작유무를 결정한다.
        /// </summary>
        /// <returns></returns>
        private bool IsCurrentProcess()
        {
            if (_currentStatus != "added")
                return false;

            if (!ValidateEditValue(deSendDate.EditValue))
                return false;

            if (!ValidateEditValue(txtSendor.EditValue))
                return false;

            return true;
        }
        #endregion

        #region GetSingleMakeVendor : 사용자가 입력한 제작업체명으로 검색
        /// <summary>
        /// 사용자가 입력한 제작업체명으로 검색
        /// </summary>
        private void GetSingleMakeVendor(string vendorName, int rowHandle)
        {
            if (_clipDatas == null)
            {
                _clipBoardData = Clipboard.GetDataObject();
                if (_clipBoardData != null)
                {
                    string[] clipFormats = _clipBoardData.GetFormats(true);
                    

                    foreach (string format in clipFormats)
                    {
                        if (format.Equals("System.String"))
                        {
                            string tempStr = Convert.ToString(_clipBoardData.GetData(format));
                            tempStr = tempStr.Replace("\r\n", "\n");
                            if(tempStr.Substring(tempStr.Length - 1).Equals("\n"))
                                tempStr = tempStr.Substring(0, tempStr.Length - 1);
                            _clipDatas = tempStr.Split('\n');                            
                        }
                    }
                }
            }

            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("VENDORNAME", vendorName);
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable savedResult = SqlExecuter.Query("GetVendorListByTool", "10001", values);

            if (savedResult.Rows.Count.Equals(1))
            {
                grdInputToolRepairSend.View.SetRowCellValue(rowHandle, "VENDORID", savedResult.Rows[0].GetString("VENDORID"));
                grdInputToolRepairSend.View.SetRowCellValue(rowHandle, "MAKEVENDOR", savedResult.Rows[0].GetString("VENDORNAME"));                
            }
            else
            {
                grdInputToolRepairSend.View.SetRowCellValue(rowHandle, "MAKEVENDOR", "");
                grdInputToolRepairSend.View.SetRowCellValue(rowHandle, "VENDORID", "");

                _isGoodCopy = false; //무조건 메세지 출력
            }

            if (_clipDatas != null)
            {
                if (_clipIndex.Equals(_clipDatas.Length)) //마지막 행을 수행하고 난 이후 메세지 출력
                {
                    if (!_isGoodCopy)
                    {
                        ShowMessage(MessageBoxButtons.OK, "TOOLMAKEVENDORSELECT", "");
                    }

                    //초기화
                    _clipIndex = 1;
                    _clipDatas = null;
                    _isGoodCopy = true;
                }
                else
                {
                    _clipIndex++;
                }
            }            
        }
        #endregion

        #region GetSingleReceiptArea : 사용자가 입력한 입고작업장으로 검색 (클립보드의 데이터를 이용)
        private void GetSingleReceiptArea(string areaName, int rowHandle)
        {
            if (_clipDatas == null)
            {
                _clipBoardData = Clipboard.GetDataObject();
                if (_clipBoardData != null)
                {
                    string[] clipFormats = _clipBoardData.GetFormats(true);


                    foreach (string format in clipFormats)
                    {
                        if (format.Equals("System.String"))
                        {
                            string tempStr = Convert.ToString(_clipBoardData.GetData(format));
                            tempStr = tempStr.Replace("\r\n", "\n");
                            if (tempStr.Substring(tempStr.Length - 1).Equals("\n"))
                                tempStr = tempStr.Substring(0, tempStr.Length - 1);
                            _clipDatas = tempStr.Split('\n');
                        }
                    }
                }
            }

            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("AREANAME", areaName);
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable savedResult = SqlExecuter.Query("GetAreaListByTool", "10001", values);

            if (savedResult.Rows.Count.Equals(1))
            {
                grdInputToolRepairSend.View.SetRowCellValue(rowHandle, "RECEIPTAREAID", savedResult.Rows[0].GetString("AREAID"));
                grdInputToolRepairSend.View.SetRowCellValue(rowHandle, "RECEIPTAREA", savedResult.Rows[0].GetString("AREANAME"));
            }
            else
            {
                grdInputToolRepairSend.View.SetRowCellValue(rowHandle, "RECEIPTAREAID", "");
                grdInputToolRepairSend.View.SetRowCellValue(rowHandle, "RECEIPTAREA", "");

                _isGoodCopy = false; //무조건 메세지 출력
            }

            if (_clipDatas != null)
            {
                if (_clipIndex.Equals(_clipDatas.Length)) //마지막 행을 수행하고 난 이후 메세지 출력
                {
                    if (!_isGoodCopy)
                    {
                        ShowMessage(MessageBoxButtons.OK, "TOOLMAKEVENDORSELECT", "");
                    }

                    //초기화
                    _clipIndex = 1;
                    _clipDatas = null;
                    _isGoodCopy = true;
                }
                else
                {
                    _clipIndex++;
                }
            }
        }
        #endregion

        #region ChangeSiteCondition - 검색조건의 Site변경시 관련된 쿼리들을 변경
        private void ChangeSiteCondition()
        {
            if (popupGridToolArea != null)
                popupGridToolArea.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"CURRENTLOGINID={UserInfo.Current.Id}", $"ISMOD=Y");


            if (conditionFactoryBox != null)
                conditionFactoryBox.Query = new SqlQuery("GetFactoryListByEqp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");


            if (popupGridToolVendor != null)
                popupGridToolVendor.SearchQuery = new SqlQuery("GetVendorListByTool", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            if (areaCondition != null)
                areaCondition.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"CURRENTLOGINID={UserInfo.Current.Id}");

            if (userCodePopup != null)
                userCodePopup.SearchQuery = new SqlQuery("GetUserListForPopupByTool", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            if (vendorPopup != null)
                vendorPopup.SearchQuery = new SqlQuery("GetVendorListByTool", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            if (productPopup != null)
                productPopup.SearchQuery = new SqlQuery("GetProductdefidPoplistByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"PRODUCTDEFTYPE=Product");

            //작업장 검색조건 초기화
            ((SmartSelectPopupEdit)Conditions.GetControl("AREAID")).ClearValue();//USERNAME
            ((SmartSelectPopupEdit)Conditions.GetControl("USERNAME")).ClearValue();//USERNAME
        }
        #endregion

        #endregion
    }
}
