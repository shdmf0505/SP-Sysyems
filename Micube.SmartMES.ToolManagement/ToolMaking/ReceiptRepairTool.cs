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
    /// 프 로 그 램 명  : 치공구 수리입고
    /// 업  무  설  명  : 치공구관리 > 치공구 수리관리 > 치공구 수리입고
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-08-06
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ReceiptRepairTool : SmartConditionManualBaseForm
    {
        #region Local Variables
        string _currentStatus;                                      // 현재상태
        string _sendUserID;                                         // 출고자아이디
        string _selectedVendor;
        string _searchVendorID;
        string _searchUserID;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repCheckEdit;

        string[] _clipDatas;
        int _clipIndex = 1;
        IDataObject _clipBoardData;
        bool _isGoodCopy = true;

        ConditionItemSelectPopup popupGridToolArea;
        ConditionItemComboBox conditionFactoryBox;
        ConditionItemSelectPopup areaCondition;
        ConditionItemSelectPopup productPopup;
        ConditionItemSelectPopup vendorPopup;
        ConditionItemSelectPopup userCodePopup;
        ConditionItemSelectPopup makeVendorPopup;
        #endregion

        #region 생성자
        public ReceiptRepairTool()
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
            InitializeGridDetail();

            InitializeRepairVendors();            

            InitialRequriedForm();

            InitializeInsertForm();
        }

        #region InitializeGrid - 수리입고목록을 초기화한다.
        /// <summary>        
        /// 치공구내역목록을 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdToolRepairReceipt.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기            
            grdToolRepairReceipt.View.SetIsReadOnly();

            grdToolRepairReceipt.View.AddTextBoxColumn("RECEIPTDATE", 150)              //입고일자
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdToolRepairReceipt.View.AddTextBoxColumn("RECEIPTSEQUENCE", 80)
                ;                                                                      //제작구분
            grdToolRepairReceipt.View.AddTextBoxColumn("TOOLREPAIRTYPEID")
                .SetIsHidden();                                                        //수리구분
            grdToolRepairReceipt.View.AddTextBoxColumn("TOOLREPAIRTYPE", 100)          //수리구분
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdToolRepairReceipt.View.AddTextBoxColumn("TOOLNUMBER", 150)
                ;                                                                       //Tool번호
            grdToolRepairReceipt.View.AddTextBoxColumn("TOOLCODE", 150)
                ;                                                                       //Tool코드
            grdToolRepairReceipt.View.AddTextBoxColumn("TOOLVERSION", 80);              //Tool버전
            grdToolRepairReceipt.View.AddTextBoxColumn("PREVTOOLVERSION", 120);          //이전Tool버전
            grdToolRepairReceipt.View.AddTextBoxColumn("TOOLNAME", 400);          //이전Tool버전
            grdToolRepairReceipt.View.AddTextBoxColumn("PRODUCTDEFID", 120)
                ;                                                                       //품목코드
            grdToolRepairReceipt.View.AddTextBoxColumn("PRODUCTDEFNAME", 280);          //품목명
            grdToolRepairReceipt.View.AddComboBoxColumn("TOOLFORMCODE", 150, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=ToolForm"), "CODENAME", "CODEID")
               .SetTextAlignment(TextAlignment.Center)
               .SetIsReadOnly(true);                                                  //상태 - (이 회면에서 입력해야 한다면 SetIsReadOnly를 풀어준다.
            grdToolRepairReceipt.View.AddTextBoxColumn("TOOLCATEGORYID")
                .SetIsHidden();                                                        //Tool구분아이디
            grdToolRepairReceipt.View.AddTextBoxColumn("TOOLCATEGORY", 120)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                  //Tool구분
            grdToolRepairReceipt.View.AddTextBoxColumn("TOOLCATEGORYDETAILID")
                .SetIsHidden();                                                        //Tool유형아이디
            grdToolRepairReceipt.View.AddTextBoxColumn("TOOLCATEGORYDETAIL", 150)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                  //Tool유형
            grdToolRepairReceipt.View.AddTextBoxColumn("TOOLDETAILID")
                .SetIsHidden();                                                        //Tool유형아이디
            grdToolRepairReceipt.View.AddTextBoxColumn("TOOLDETAIL", 180)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                  //Tool유형
            grdToolRepairReceipt.View.AddTextBoxColumn("MAKEVENDORID")
                .SetIsHidden();                                                         //제작업체아이디
            grdToolRepairReceipt.View.AddTextBoxColumn("MAKEVENDOR", 150)
                ;                                                                       //제작업체
            grdToolRepairReceipt.View.AddTextBoxColumn("REPAIRDESCRIPTION", 200)               
              ;                                                                    //수리자내용
            grdToolRepairReceipt.View.AddTextBoxColumn("RECEIPTAREAID")
                .SetIsHidden();                                                         //입고작업장아이디
            grdToolRepairReceipt.View.AddTextBoxColumn("RECEIPTAREA", 180)
                ;                                                                       //입고작업장 
            grdToolRepairReceipt.View.AddTextBoxColumn("RECEIPTUSERID")
                .SetIsHidden();                                                         //입고자아이디
            grdToolRepairReceipt.View.AddTextBoxColumn("RECEIPTUSER", 150);             //입고자
            
            grdToolRepairReceipt.View.PopulateColumns();
        }
        #endregion

        #region InitializeGridDetail - 수리입고목록입력을 위한 그리드를 초기화한다.
        /// <summary>        
        /// 제작입고목록입력을 위한 그리드를 초기화한다.
        /// </summary>
        private void InitializeGridDetail()
        {
            // TODO : 그리드 초기화 로직 추가
            grdInputToolRepairReceipt.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기       
            grdInputToolRepairReceipt.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            

            grdInputToolRepairReceipt.View.AddTextBoxColumn("RECEIPTDATE", 150)       //입고일자
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly(true);
            grdInputToolRepairReceipt.View.AddTextBoxColumn("RECEIPTSEQUENCE", 80)    //입고순번
                .SetIsReadOnly(true);
            grdInputToolRepairReceipt.View.AddTextBoxColumn("RECEIPTUSER", 100)       //입고자
                .SetIsReadOnly(true);
            grdInputToolRepairReceipt.View.AddTextBoxColumn("RECEIPTUSERID")          //입고자아이디
                .SetIsHidden();
            //grdInputToolRepairReceipt.View.AddTextBoxColumn("MAKEVENDOR", 100)      //수리업체
            //    .SetIsReadOnly(true);
            InitializeVendorPopupColumnInDetailGrid();
            grdInputToolRepairReceipt.View.AddTextBoxColumn("MAKEVENDORID")         //수리업체아이디
                .SetIsHidden();
            
            grdInputToolRepairReceipt.View.AddTextBoxColumn("TOOLNUMBER", 150)
                .SetIsReadOnly(true);                                                 //Tool 번호
            grdInputToolRepairReceipt.View.AddTextBoxColumn("TOOLNAME", 400)
                .SetIsReadOnly(true);                                                 //Tool 번호
            grdInputToolRepairReceipt.View.AddCheckBoxColumn("POLISH", 60)
               .SetIsReadOnly(false)                 
               ;                                                                    //연마여부
            grdInputToolRepairReceipt.View.AddTextBoxColumn("REPAIRDESCRIPTION", 200)
              ;                                                                    //수리자내용
            InitializeAreaPopupColumnInDetailGrid();

            grdInputToolRepairReceipt.View.AddTextBoxColumn("RECEIPTAREAID")
                .SetIsHidden();                                                       //입고작업장
                
            grdInputToolRepairReceipt.View.AddTextBoxColumn("SENDAREAID")
                .SetIsHidden();                                                       //입고작업장
            grdInputToolRepairReceipt.View.AddTextBoxColumn("SENDAREA", 180)
                .SetIsReadOnly(true);                                               //출고작업장
            grdInputToolRepairReceipt.View.AddTextBoxColumn("PRODUCTDEFID", 120)
                .SetIsReadOnly(true);                                               //품목코드            
            grdInputToolRepairReceipt.View.AddTextBoxColumn("PRODUCTDEFNAME", 280)
                .SetIsReadOnly(true);                                               //품목명
            grdInputToolRepairReceipt.View.AddTextBoxColumn("WEIGHT", 100)
                .SetIsReadOnly(true);                                               //무게
            grdInputToolRepairReceipt.View.AddTextBoxColumn("HORIZONTAL", 100)
                .SetIsReadOnly(true);                                               //가로
            grdInputToolRepairReceipt.View.AddTextBoxColumn("VERTICAL", 100)
                .SetIsReadOnly(true);                                               //세로
            grdInputToolRepairReceipt.View.AddTextBoxColumn("THEIGHT", 100)
                .SetIsReadOnly(true);                                               //높이
            grdInputToolRepairReceipt.View.AddTextBoxColumn("POLISHTHICKNESS", 100)
                .SetIsReadOnly(true);                                               //연마두께           
            grdInputToolRepairReceipt.View.AddTextBoxColumn("TOTALPOLISHTHICKNESS", 100)
                .SetIsReadOnly(true);                                               //누적연마두께           
            grdInputToolRepairReceipt.View.AddTextBoxColumn("CREATEDTHICKNESS", 100)
                .SetIsReadOnly(true);                                               //최초두께           

            grdInputToolRepairReceipt.View.AddTextBoxColumn("SENDDATE")
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsHidden();                                                       //출고일
            grdInputToolRepairReceipt.View.AddTextBoxColumn("SENDSEQUENCE")
                .SetIsHidden();                                                       //출고순번
            grdInputToolRepairReceipt.View.AddTextBoxColumn("REQUESTDATE")
                .SetIsHidden();                                                       //의뢰일자
            grdInputToolRepairReceipt.View.AddTextBoxColumn("REQUESTSEQUENCE")
                .SetIsHidden();                                                       //의뢰순번
            grdInputToolRepairReceipt.View.AddTextBoxColumn("TOOLCODE")
                .SetIsHidden();                                                       //툴코드
            grdInputToolRepairReceipt.View.AddTextBoxColumn("TOOLVERSION")
                .SetIsHidden();                                                       //툴버전
            grdInputToolRepairReceipt.View.AddTextBoxColumn("PREVTOOLVERSION")
                .SetIsHidden();                                                       //새로운툴버전
            grdInputToolRepairReceipt.View.AddTextBoxColumn("TOOLREPAIRTYPEID")
                .SetIsHidden();                                                       //새로운툴버전




            grdInputToolRepairReceipt.View.PopulateColumns();
        }
        #endregion

        #region InitializeAreaPopupColumnInDetailGrid - 작업장을 팝업검색할 필드 설정
        /// <summary>
        /// grid ProcesssegmentidLis
        /// </summary>
        private void InitializeAreaPopupColumnInDetailGrid()
        {
            popupGridToolArea = this.grdInputToolRepairReceipt.View.AddSelectPopupColumn("RECEIPTAREA", 180, new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"CURRENTLOGINID={UserInfo.Current.Id}"))
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
                .SetIsReadOnly(true)
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    foreach (DataRow row in selectedRows)
                    {
                        grdInputToolRepairReceipt.View.GetFocusedDataRow()["RECEIPTAREAID"] = row["AREAID"];
                        grdInputToolRepairReceipt.View.GetFocusedDataRow()["RECEIPTAREA"] = row["AREANAME"];
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
            ConditionItemSelectPopup popupGridToolVendor = grdInputToolRepairReceipt.View.AddSelectPopupColumn("MAKEVENDOR", 150, new SqlQuery("GetVendorListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
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
                //.SetValidationIsRequired()
                .SetIsReadOnly(true)
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    int i = 0;
                    int currentIndex = grdInputToolRepairReceipt.View.GetFocusedDataSourceRowIndex();

                    foreach (DataRow row in selectedRows)
                    {
                        if (i == 0)
                        {
                            grdInputToolRepairReceipt.View.GetFocusedDataRow()["MAKEVENDOR"] = row["VENDORNAME"];
                            grdInputToolRepairReceipt.View.GetFocusedDataRow()["MAKEVENDORID"] = row["VENDORID"];
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

            grdToolRepairReceipt.View.FocusedRowChanged += grdToolUpdateSend_FocusedRowChanged;
            grdInputToolRepairReceipt.View.DataSourceChanged += grdInputToolRepairReceipt_DataSourceChanged;
            grdInputToolRepairReceipt.View.ShowingEditor += grdInputToolRepairReceipt_ShowingEditor;
            grdInputToolRepairReceipt.View.CheckStateChanged += grdInputToolRepairReceipt_CheckStateChanged;
            grdInputToolRepairReceipt.View.CellValueChanged += grdInputToolRepairReceipt_CellValueChanged;

            Shown += ReceiptRepairTool_Shown;
        }

        #region ReceiptRepairTool_Shown - Site관련정보를 화면로딩후 설정한다.
        private void ReceiptRepairTool_Shown(object sender, EventArgs e)
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

        #region grdInputToolRepairReceipt_CellValueChanged - 입력그리드의 셀 데이터 변경 이벤트 (입고작업장 복사 붙여넣기 대응)
        private void grdInputToolRepairReceipt_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "RECEIPTAREA")
            {
                grdInputToolRepairReceipt.View.CellValueChanged -= grdInputToolRepairReceipt_CellValueChanged;

                DataRow row = grdInputToolRepairReceipt.View.GetDataRow(e.RowHandle);

                if (row["RECEIPTAREA"].ToString().Equals(""))
                {
                    grdInputToolRepairReceipt.View.CellValueChanged += grdInputToolRepairReceipt_CellValueChanged;
                    return;
                }
                GetSingleReceiptArea(row.GetString("RECEIPTAREA"), e.RowHandle);
                grdInputToolRepairReceipt.View.CellValueChanged += grdInputToolRepairReceipt_CellValueChanged;
            }
        }
        #endregion

        #region grdInputToolRepairReceipt_CheckStateChanged - 입력그리드의 체크상태변경 이벤트 - 입력상태일때만 체크가능
        private void grdInputToolRepairReceipt_CheckStateChanged(object sender, EventArgs e)
        {
            grdInputToolRepairReceipt.View.CheckStateChanged -= grdInputToolRepairReceipt_CheckStateChanged;

            if (!_currentStatus.Equals("added"))
                grdInputToolRepairReceipt.View.CheckRow(grdInputToolRepairReceipt.View.FocusedRowHandle, false);

            grdInputToolRepairReceipt.View.CheckStateChanged += grdInputToolRepairReceipt_CheckStateChanged;
        }
        #endregion

        #region grdInputToolRepairReceipt_ShowingEditor - 입력드리드의 입력 제어 - 입력상태일때만 제어가능
        private void grdInputToolRepairReceipt_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (_currentStatus == "added")
            {
                if (grdInputToolRepairReceipt.View.FocusedColumn.FieldName == "POLISH")
                {
                    if (grdInputToolRepairReceipt.View.GetFocusedDataRow().GetString("TOOLREPAIRTYPEID").Equals("Polish"))
                        e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region grdInputToolRepairReceipt_DataSourceChanged - 입력그리드의 DataSource의 데이터 변경이벤트
        private void grdInputToolRepairReceipt_DataSourceChanged(object sender, EventArgs e)
        {
            DateTime receiptDate = (DateTime)deReceiptDate.EditValue;
            string userName = "", vendorName = "";

            if (txtReceiptUser.EditValue != null)
                userName = txtReceiptUser.EditValue.ToString();

            if (popEditVendor.EditValue != null)
                vendorName = popEditVendor.EditValue.ToString();

            for (int i = 0; i < grdInputToolRepairReceipt.View.RowCount; i++)
            {
                grdInputToolRepairReceipt.View.SetRowCellValue(i, "RECEIPTDATE", receiptDate.ToString("yyyy-MM-dd HH:mm:ss"));
                grdInputToolRepairReceipt.View.SetRowCellValue(i, "RECEIPTSEQUENCE", "");
                grdInputToolRepairReceipt.View.SetRowCellValue(i, "RECEIPTUSERID", _sendUserID);
                grdInputToolRepairReceipt.View.SetRowCellValue(i, "RECEIPTUSER", userName);
                grdInputToolRepairReceipt.View.SetRowCellValue(i, "REPAIRVENDORID", _selectedVendor);
                grdInputToolRepairReceipt.View.SetRowCellValue(i, "REPAIRVENDOR", vendorName);
            }
        }
        #endregion

        #region BtnErase_Click - 삭제버튼 이벤트
        private void BtnErase_Click(object sender, EventArgs e)
        {
            if (grdInputToolRepairReceipt.View.GetCheckedRows().Rows.Count > 0)
            {
                if (IsCurrentProcess())
                    DeleteLotGridRow();
                else
                    ShowMessage(MessageBoxButtons.OK, "CURRENTPROCESSREMOVEDATA", "");
            }
            else
            {
                //ShowMessage(MessageBoxButtons.OK, String.Format("SelectItem", Language.GetDictionary("DURABLE")), "");
                ShowMessage(MessageBoxButtons.OK, "SelectItem", Language.Get("DURABLE"));
            }
        }
        #endregion

        #region BtnModify_Click - 수정버튼 이벤트
        private void BtnModify_Click(object sender, EventArgs e)
        {
            if (IsCurrentProcess())
                SaveData();
            else
                ShowMessage(MessageBoxButtons.OK, "CURRENTPROCESSUUPDATEDATA", "");
        }
        #endregion

        #region grdToolUpdateSend_FocusedRowChanged - 그리드의 Row 변경 이벤트
        private void grdToolUpdateSend_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayToolRepairSendInfo();
        }
        #endregion

        #region BtnInitialize_Click - 초기화버튼 이벤트
        private void BtnInitialize_Click(object sender, EventArgs e)
        {
            InitializeInsertForm();
        }
        #endregion

        #region BtnSave_Click - 저장이벤트
        private void BtnSave_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region BtnSearchTool_Click - 툴 검색 선택 이벤트 - 팝업창 제어
        private void BtnSearchTool_Click(object sender, EventArgs e)
        {
            if (IsCurrentProcess())
            {
                if (Conditions.Validation())
                    DisplayUpdateToolPopup();
            }
            else
                ShowMessage(MessageBoxButtons.OK, "AfterInitValidation", "");
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
            if (Conditions.GetValue("USERNAME").ToString() != "")
                values.Add("RECEIPTUSERID", _searchUserID);
            if (Conditions.GetValue("VENDORNAME").ToString() != "")
                values.Add("VENDORID", _searchVendorID);
            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolRepairResult = SqlExecuter.Query("GetReceiptToolRepairListForReceiptByTool", "10001", values);

            grdToolRepairReceipt.DataSource = toolRepairResult;

            if (toolRepairResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            else
            {
                grdToolRepairReceipt.View.FocusedRowHandle = 0;
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
        private void Research(string receiptSequence, string receiptDate)
        {
            // TODO : 조회 SP 변경
            InitializeInsertForm();

            var values = Conditions.GetValues();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            if (Conditions.GetValue("USERNAME").ToString() != "")
                values.Add("RECEIPTUSERID", _searchUserID);
            if (Conditions.GetValue("VENDORNAME").ToString() != "")
                values.Add("VENDORID", _searchVendorID);
            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable sparePartSearchResult = SqlExecuter.Query("GetReceiptToolRepairListForReceiptByTool", "10001", values);

            if (sparePartSearchResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdToolRepairReceipt.DataSource = sparePartSearchResult;

            //현재 작업한 결과를 선택할 수 있도록 한다.
            if (receiptSequence == null && receiptDate == null) //삭제 작업후
            {
                //값이 하나도 없다면 빈 화면
                if (grdToolRepairReceipt.View.RowCount == 0)
                {
                    //입력대기 화면으로 설정
                    InitializeInsertForm();
                }
                else
                {
                    //첫번재 값으로 설정
                    grdToolRepairReceipt.View.FocusedRowHandle = 0;
                    DisplayToolRepairSendInfo();
                }
            }
            else //값이 있다면 해당 값에 맞는 행을 찾아서 선택
            {
                ViewSavedData(receiptDate, receiptSequence);
            }

            _currentStatus = "modified";
        }
        #endregion

        #region SearchLotList - 수정출고정보의 LOT을 조회한다.
        /// <summary>
        /// SearchLotList - 수정출고정보의 LOT을 조회한다.
        /// </summary>
        private void SearchLotList(string receiptDate, string receiptSequence)
        {
            DataRow currentRow = grdToolRepairReceipt.View.GetFocusedDataRow();
            if (currentRow != null)
            {
                Dictionary<string, object> values = new Dictionary<string, object>();

                #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                values.Add("RECEIPTDATE", Convert.ToDateTime(receiptDate).ToString("yyyy-MM-dd HH:mm:ss"));
                values.Add("RECEIPTSEQUENCE", receiptSequence);
                #endregion
                values = Commons.CommonFunction.ConvertParameter(values);
                DataTable searchResult = SqlExecuter.Query("GetRepairToolResultLotListForReceiptByTool", "10001", values);

                grdInputToolRepairReceipt.DataSource = searchResult;
            }
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
            //InitializeConditionUser();
            InitializeConditionUserPopup();
            InitializeConditionProductPopup();
            InitializeConditionVendor();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #region InitializeConditionPlant - Site 조회조건
        private void InitializeConditionPlant()
        {
            var planttxtbox = Conditions.AddComboBox("PLANTID", new SqlQuery("GetPlantList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
               .SetLabel("PLANT")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.1)
               .SetIsReadOnly(true)
               .SetDefault(UserInfo.Current.Plant, "PLANTID") //기본값 설정 UserInfo.Current.Plant
            ;
        }
        #endregion

        #region InitializeConditionArea - 작업장 콤보박스 조회조건
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
               .SetDefault(UserInfo.Current.Area, "AREAID")
            ;
        }
        #endregion

        #region InitializeConditionAreaPopup : 작업장 검색조건
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

        #region InitializeConditionUser - 사용자 콤보박스 조회조건
        private void InitializeConditionUser()
        {
            var planttxtbox = Conditions.AddComboBox("USERID", new SqlQuery("GetUserListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "USERNAME", "USERID")
               .SetLabel("RECEIPTUSER")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(4)
               .SetEmptyItem("","",true)
            ;
        }
        #endregion

        #region InitializeConditionProductPopup - 품목조회조건
        private void InitializeConditionProductPopup()
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

        #region InitializeConditionVendor - 협력업체 조회조건
        private void InitializeConditionVendor()
        {
            vendorPopup = Conditions.AddSelectPopup("VENDORNAME", new SqlQuery("GetVendorListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
            .SetPopupLayout("REPAIRVENDOR", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
            .SetPopupResultMapping("VENDORNAME", "VENDORNAME")
            .SetLabel("MAKEVENDOR")
            .SetPopupResultCount(1)
            .SetPosition(0.3)
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
        #endregion

        #region InitializeConditionUserPopup - 사용자 조회조건
        private void InitializeConditionUserPopup()
        {
            userCodePopup = Conditions.AddSelectPopup("USERNAME", new SqlQuery("GetUserListForPopupByTool", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
            .SetPopupLayout("USERNAME", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow)
            .SetPopupResultMapping("USERNAME", "USERNAME")
            .SetLabel("RECEIPTUSER")
            .SetPopupResultCount(1)
            .SetPosition(4)
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
        #endregion
        #endregion

        #region InitializeRepairVendors : 팝업창 제어
        /// <summary>
        /// 팝업창 제어
        /// </summary>
        private void InitializeRepairVendors()
        {
            makeVendorPopup = new ConditionItemSelectPopup();
            makeVendorPopup.Id = "MAKEVENDOR";
            makeVendorPopup.SearchQuery = new SqlQuery("GetVendorListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            makeVendorPopup.ValueFieldName = "VENDORID";
            makeVendorPopup.DisplayFieldName = "VENDORNAME";
            makeVendorPopup.SetPopupLayout("MAKEVENDOR", PopupButtonStyles.Ok_Cancel, true, true);
            makeVendorPopup.SetPopupResultCount(1);
            makeVendorPopup.SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow);
            makeVendorPopup.SetPopupAutoFillColumns("VENDORNAME");
            makeVendorPopup.SetPopupResultMapping("MAKEVENDOR", "VENDORNAME");
            makeVendorPopup.SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                //작업장 변경 시 작업자 조회
                selectedRows.Cast<DataRow>().ForEach(row =>
                {
                    _selectedVendor = row.GetString("VENDORID");
                    popEditVendor.EditValue = row.GetString("VENDORNAME");
                    InsertRepairVendor(row.GetString("VENDORNAME"));
                });

            });


            makeVendorPopup.Conditions.AddTextBox("VENDORNAME");

            makeVendorPopup.GridColumns.AddTextBoxColumn("VENDORID", 150)
                .SetIsHidden();
            makeVendorPopup.GridColumns.AddTextBoxColumn("VENDORNAME", 200);

            popEditVendor.SelectPopupCondition = makeVendorPopup;
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
        private bool ValidateContent()
        {
            //각각의 상태에 따라 보여줄 메세지가 상이하다면 각각의 분기에서 메세지를 표현해야 한다.            

            if (!ValidateEditValue(deReceiptDate.EditValue))
                return false;

            if (grdInputToolRepairReceipt.View.RowCount == 0)
                return false;

            if (!ValidateEditValue(txtReceiptUser.EditValue))
                return false;
            
            //if (!ValidateEditValue(popEditVendor.EditValue))
            //    return false;

            //if (_selectedVendor == null || _selectedVendor == "")
            //    return false;

            //DataTable dataTables = (DataTable)grdInputToolRepairReceipt.DataSource;
            //foreach (DataRow currentRow in dataTables.Rows)
            //{
            //    if (!ValidateCellInGrid(currentRow, new string[] { "RECEIPTAREAID", "MAKEVENDORID"}))
            //        return false;
            //}

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
        private void InitializeInsertForm()
        {
            try
            {
                _currentStatus = "added";           //현재 화면의 상태는 입력중이어야 한다.

                //의뢰일은 오늘날짜를 기본으로 한다.
                DateTime dateNow = DateTime.Now;
                deReceiptDate.EditValue = dateNow;

                //의뢰순번은 공값으로 한다.
                txtReceiptSequence.EditValue = null;

                //의뢰부서는 사용자의 부서를 기본으로 한다.
                txtReceiptUser.EditValue = UserInfo.Current.Name;

                _sendUserID = UserInfo.Current.Id;

                _selectedVendor = "";

                popEditVendor.EditValue = null;

                grdInputToolRepairReceipt.DataSource = null;

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
        private void InitialRequriedForm()
        {
            SetRequiredValidationControl(lblSendDate);
            SetRequiredValidationControl(lblSendUser);
            SetRequiredValidationControl(lblRepairVendor);
        }
        #endregion

        #region InsertRepairVendor : 수리업체를 선정하면 그리드의 데이터가 변경되어야 한다.
        private void InsertRepairVendor(string vendorName)
        {
            for (int i = 0; i < grdInputToolRepairReceipt.View.RowCount; i++)
                grdInputToolRepairReceipt.View.SetRowCellValue(i, "REPAIRVENDOR", vendorName);
        }
        #endregion

        #region controlEnableProcess : 입력/수정/삭제를 위한 화면내 컨트롤들의 Enable 제어
        private void ControlEnableProcess(string currentStatus)
        {
            if (currentStatus == "added") //초기화 버튼을 클릭한 경우
            {
                deReceiptDate.ReadOnly = false;
                txtReceiptSequence.ReadOnly = true;
                txtReceiptUser.ReadOnly = true;
            }
            else if (currentStatus == "modified") //
            {
                deReceiptDate.ReadOnly = true;
                txtReceiptSequence.ReadOnly = true;
                txtReceiptUser.ReadOnly = true;
            }
            else
            {
                deReceiptDate.ReadOnly = true;
                txtReceiptSequence.ReadOnly = true;
                txtReceiptUser.ReadOnly = true;
            }
        }
        #endregion

        #region LoadData - 팝업창에서 호출하는 메소드
        private void LoadData(DataTable table)
        {
            #region checkbox로 인하여 쓰지 않는 소스
            //DataTable originTable = (DataTable)grdInputToolRepairReceipt.DataSource;

            //if (originTable == null)
            //{
            //    originTable = CreateSaveDatatable(false);

            //    //값이 없을 경우 빈 테이블을 바인딩
            //    grdInputToolRepairReceipt.DataSource = originTable;
            //}
            //DateTime receiptDate = (DateTime)deReceiptDate.EditValue;
            //foreach (DataRow inputRow in table.Rows)
            //{
            //    grdInputToolRepairReceipt.View.AddNewRow();

            //    grdInputToolRepairReceipt.View.FocusedRowHandle = grdInputToolRepairReceipt.View.RowCount - 1;
            //    string userName = "", vendorName = "";

            //    if (txtReceiptUser.EditValue != null)
            //        userName = txtReceiptUser.EditValue.ToString();

            //    if (popEditVendor.EditValue != null)
            //        vendorName = popEditVendor.EditValue.ToString();

            //    grdInputToolRepairReceipt.View.SetFocusedRowCellValue("RECEIPTDATE", receiptDate.ToString("yyyy-MM-dd"));
            //    grdInputToolRepairReceipt.View.SetFocusedRowCellValue("RECEIPTSEQUENCE", "");
            //    grdInputToolRepairReceipt.View.SetFocusedRowCellValue("RECEIPTUSERID", userName);
            //    grdInputToolRepairReceipt.View.SetFocusedRowCellValue("RECEIPTUSER", _sendUserID);
            //    grdInputToolRepairReceipt.View.SetFocusedRowCellValue("REPAIRVENDORID", _selectedVendor);
            //    grdInputToolRepairReceipt.View.SetFocusedRowCellValue("REPAIRVENDOR", vendorName);
            //    grdInputToolRepairReceipt.View.SetFocusedRowCellValue("TOOLNUMBER", inputRow["TOOLNUMBER"]);
            //    grdInputToolRepairReceipt.View.SetFocusedRowCellValue("RECEIPTAREAID", inputRow["RECEIPTAREAID"]);
            //    grdInputToolRepairReceipt.View.SetFocusedRowCellValue("RECEIPTAREA", inputRow["RECEIPTAREA"]);
            //    grdInputToolRepairReceipt.View.SetFocusedRowCellValue("SENDAREAID", inputRow["SENDAREAID"]);
            //    grdInputToolRepairReceipt.View.SetFocusedRowCellValue("SENDAREA", inputRow["SENDAREA"]);
            //    grdInputToolRepairReceipt.View.SetFocusedRowCellValue("PRODUCTDEFID", inputRow["PRODUCTDEFID"]);
            //    grdInputToolRepairReceipt.View.SetFocusedRowCellValue("PRODUCTDEFNAME", inputRow["PRODUCTDEFNAME"]);
            //    grdInputToolRepairReceipt.View.SetFocusedRowCellValue("WEIGHT", inputRow["WEIGHT"]);
            //    grdInputToolRepairReceipt.View.SetFocusedRowCellValue("LENGTH", inputRow["LENGTH"]);
            //    grdInputToolRepairReceipt.View.SetFocusedRowCellValue("HEIGHT", inputRow["HEIGHT"]);
            //    grdInputToolRepairReceipt.View.SetFocusedRowCellValue("THICKNESS", inputRow["THICKNESS"]);

            //    grdInputToolRepairReceipt.View.SetFocusedRowCellValue("POLISH", null);

            //    grdInputToolRepairReceipt.View.SetFocusedRowCellValue("SENDDATE", inputRow["SENDDATE"]);
            //    grdInputToolRepairReceipt.View.SetFocusedRowCellValue("SENDSEQUENCE", inputRow["SENDSEQUENCE"]);
            //    grdInputToolRepairReceipt.View.SetFocusedRowCellValue("REQUESTDATE", inputRow["REQUESTDATE"]);
            //    grdInputToolRepairReceipt.View.SetFocusedRowCellValue("REQUESTSEQUENCE", inputRow["REQUESTSEQUENCE"]);
            //    grdInputToolRepairReceipt.View.SetFocusedRowCellValue("TOOLCODE", inputRow["TOOLCODE"]);
            //    grdInputToolRepairReceipt.View.SetFocusedRowCellValue("TOOLVERSION", inputRow["TOOLVERSION"]);
            //}

            ////첫번째 행에서 포커스를 띄워놓아야 X표시가 안뜬다.
            //grdInputToolRepairReceipt.View.AddNewRow();

            //grdInputToolRepairReceipt.View.DeleteRow(grdInputToolRepairReceipt.View.RowCount - 1);
            #endregion

            string durableIDs = "";
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (i == 0)
                    durableIDs = "'" + table.Rows[i].GetString("TOOLNUMBER") + "'";
                else
                    durableIDs += ", '" + table.Rows[i].GetString("TOOLNUMBER") + "'";
            }

            Dictionary<string, object> values = new Dictionary<string, object>();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("DURABLELOTIDS", durableIDs);
            #endregion
            //values = Commons.CommonFunction.ConvertParameter(values);
            DataTable searchResult = SqlExecuter.Query("GetRepairToolResultListForReceiptByTool", "10001", values);


            DateTime receiptDate = (DateTime)deReceiptDate.EditValue;
            string userName = "", vendorName = "";

            if (txtReceiptUser.EditValue != null)
                userName = txtReceiptUser.EditValue.ToString();

            if (popEditVendor.EditValue != null)
                vendorName = popEditVendor.EditValue.ToString();

            for (int i = 0; i < searchResult.Rows.Count; i++)
            {
                searchResult.Rows[i]["RECEIPTDATE"] =  receiptDate.ToString("yyyy-MM-dd HH:mm:ss");
                searchResult.Rows[i]["RECEIPTSEQUENCE"] =  "";
                searchResult.Rows[i]["RECEIPTUSERID"] =  _sendUserID;
                searchResult.Rows[i]["RECEIPTUSER"] =  userName;
                //searchResult.Rows[i]["MAKEVENDORID"] =  _selectedVendor;
                //searchResult.Rows[i]["MAKEVENDOR"] =  vendorName;
            }

            grdInputToolRepairReceipt.DataSource = searchResult;           

            _currentStatus = "added";
        }
        #endregion

        #region CreateSaveParentDatatable : ReceipRepairSend 입력/수정/삭제를 위한 DataTable의 Template 생성
        private DataTable CreateSaveParentDatatable(bool useState)
        {
            DataTable dt = new DataTable();
            dt.TableName = "toolReceiptList";
            //===================================================================================
            //Key값 ToolRequest 및 ToolRequestDetail
            dt.Columns.Add("RECEIPTDATE");
            dt.Columns.Add("RECEIPTSEQUENCE");
            dt.Columns.Add("REPAIRVENDORID");
            dt.Columns.Add("RECEIPTUSERID");
            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");
            dt.Columns.Add("DESCRIPTION");
            
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

        #region CreateSaveDatatable : ReceiptRepairSendLot 입력/수정/삭제를 위한 DataTable의 Template 생성
        private DataTable CreateSaveDatatable(bool useState)
        {
            DataTable dt = new DataTable();
            dt.TableName = "toolReceiptLotList";
            //===================================================================================
            dt.Columns.Add("RECEIPTDATE");
            dt.Columns.Add("RECEIPTSEQUENCE");
            dt.Columns.Add("RECEIPTUSERID");
            dt.Columns.Add("RECEIPTUSER");
            dt.Columns.Add("REPAIRVENDORID");
            dt.Columns.Add("REPAIRVENDOR");
            dt.Columns.Add("TOOLNUMBER");
            dt.Columns.Add("RECEIPTAREAID");
            dt.Columns.Add("RECEIPTAREA");
            dt.Columns.Add("SENDAREAID");
            dt.Columns.Add("SENDAREA");
            dt.Columns.Add("PRODUCTDEFID");
            dt.Columns.Add("PRODUCTDEFNAME");
            dt.Columns.Add("WEIGHT");
            dt.Columns.Add("HORIZONTAL");
            dt.Columns.Add("VERTICAL");
            dt.Columns.Add("HEIGHT");
            dt.Columns.Add("THICKNESS");

            dt.Columns.Add("POLISH");
            dt.Columns.Add("TOOLREPAIRTYPEID");

            dt.Columns.Add("SENDDATE");
            dt.Columns.Add("SENDSEQUENCE");
            dt.Columns.Add("REQUESTDATE");
            dt.Columns.Add("REQUESTSEQUENCE");
            dt.Columns.Add("TOOLCODE");
            dt.Columns.Add("TOOLVERSION");
            dt.Columns.Add("PREVTOOLVERSION");

            dt.Columns.Add("LOTHISTKEY");

            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");

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
            Popup.ToolNumberForReceiptPopup toolNumberPopup = new Popup.ToolNumberForReceiptPopup(Conditions.GetValue("P_PLANTID").ToString());
            toolNumberPopup.choiceHandler += LoadData;

            toolNumberPopup.SetReceiptArea(
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
            if (grdToolRepairReceipt.View.FocusedRowHandle < 0) return;

            DisplayToolInfoDetail(grdToolRepairReceipt.View.GetFocusedDataRow());
        }
        #endregion

        #region ViewSavedData : 새로 입력된 데이터를 화면에 바인딩
        private void ViewSavedData(string receiptDate, string receiptSequence)
        {
            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            //values.Add("PLANTID", UserInfo.Current.Plant);
            values.Add("RECEIPTDATE", Convert.ToDateTime(receiptDate).ToString("yyyy-MM-dd HH:mm:ss"));
            values.Add("RECEIPTSEQUENCE", Int32.Parse(receiptSequence));
            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable savedResult = SqlExecuter.Query("GetReceiptToolRepairListForReceiptByTool", "10001", values);

            if (savedResult.Rows.Count > 0)
            {
                DisplayToolInfoDetail(savedResult.Rows[0]);
            }
        }
        #endregion

        #region DisplayToolInfoDetail : 입력받은 인덱스의 그리드의 내용을 화면에 바인딩
        private void DisplayToolInfoDetail(DataRow currentRow)
        {
            //해당 업무에 맞는 Enable체크 수행
            //grdToolRepairReceipt.View.FocusedRowHandle = rowHandle;

            //DataRow currentRow = grdToolRepairReceipt.View.GetFocusedDataRow();

            //수정상태라 판단하여 화면 제어
            ControlEnableProcess("modified");

            //화면상태에 따른 탭및 그리드 제어
            deReceiptDate.EditValue = currentRow.GetString("RECEIPTDATE");
            _sendUserID = currentRow.GetString("RECEIPTUSERID");
            txtReceiptUser.EditValue = currentRow.GetString("RECEIPTUSER");
            _selectedVendor = currentRow.GetString("MAKEVENDORID");
            popEditVendor.EditValue = currentRow.GetString("MAKEVENDOR");
            //cboToolRepairType.EditValue = currentRow.GetString("TOOLREPAIRTYPEID");

            _currentStatus = "modified";

            //그리드 데이터 바인딩
            SearchLotList(currentRow.GetString("RECEIPTDATE"), currentRow.GetString("RECEIPTSEQUENCE"));
            //각 순서에 따라 바인딩

            //그리드의 수정가능 여부

        }
        #endregion

        #region GetRowHandleInGrid : 특정아이디의 값을 바탕으로 해당 내용을 그리드에서 찾은후 결과값의 그리드내의 인덱스를 반환한다.
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
        /// <summary>
        /// 특정아이디의 값을 바탕으로 해당 내용을 그리드에서 찾은후 결과값의 그리드내의 인덱스를 반환한다.
        /// 현재로선 String비교만을 한다. DateTime및 기타 다른 값들에 대해선 지원하지 않음
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="findValue"></param>
        /// <returns></returns>
        private int GetRowHandleInGrid(SmartBandedGrid targetGrid, string firstColumnName, string firstFindValue)
        {
            for (int i = 0; i < targetGrid.View.RowCount; i++)
            {
                if (firstFindValue.Equals(targetGrid.View.GetDataRow(i)[firstColumnName].ToString()))
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

                if (grdInputToolRepairReceipt.View.RowCount > 0)
                {
                    //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                    if (ValidateContent())
                    {
                        DataSet toolReceiptSet = new DataSet();
                        //치공구 제작의뢰를 입력
                        DataTable toolReceiptTable = CreateSaveParentDatatable(true);

                        DataRow toolReceiptRow = toolReceiptTable.NewRow();

                        DateTime receiptDate = DateTime.Now;

                        DataRow gridRow = grdToolRepairReceipt.View.GetFocusedDataRow();

                        toolReceiptRow["RECEIPTDATE"] = receiptDate.ToString("yyyy-MM-dd HH:mm:ss");
                        toolReceiptRow["RECEIPTSEQUENCE"] = "";
                        toolReceiptRow["RECEIPTUSERID"] = _sendUserID;

                        toolReceiptRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                        toolReceiptRow["PLANTID"] = Conditions.GetValue("P_PLANTID");
                        toolReceiptRow["DESCRIPTION"] = "";

                        toolReceiptRow["VALIDSTATE"] = "Valid";

                        //업무로직상 추가만 진행한다.
                        toolReceiptRow["CREATOR"] = UserInfo.Current.Id;
                        toolReceiptRow["MODIFIER"] = UserInfo.Current.Id;
                        toolReceiptRow["_STATE_"] = "added";


                        toolReceiptTable.Rows.Add(toolReceiptRow);

                        toolReceiptSet.Tables.Add(toolReceiptTable);

                        //세부항목
                        if (grdInputToolRepairReceipt.View.RowCount > 0)
                        {
                            DataTable receiptLotTable = CreateSaveDatatable(true);
                            DataTable changedGridTable = grdInputToolRepairReceipt.View.GetCheckedRows();

                            foreach (DataRow currentRow in changedGridTable.Rows)
                            {
                                DataRow receiptLotRow = receiptLotTable.NewRow();

                                //제작/수정의뢰를 거치지 않은 수리건은 RequestDate가 존재하지 않는다.
                                if (currentRow.GetString("REQUESTDATE") != "")
                                {
                                    DateTime requestDate = Convert.ToDateTime(currentRow.GetString("REQUESTDATE"));
                                    receiptLotRow["REQUESTDATE"] = requestDate.ToString("yyyy-MM-dd");
                                }

                                if (currentRow.GetString("SENDDATE") != "")
                                {
                                    DateTime sendDate = Convert.ToDateTime(currentRow.GetString("SENDDATE"));
                                    receiptLotRow["SENDDATE"] = sendDate.ToString("yyyy-MM-dd HH:mm:ss");
                                }

                                //변경툴버전이 있다면 할당하고 아니라면 원래값을 할당한다.
                                if (currentRow.GetString("PREVTOOLVERSION") != "")
                                    receiptLotRow["PREVTOOLVERSION"] = currentRow.GetString("PREVTOOLVERSION");
                                else
                                    receiptLotRow["PREVTOOLVERSION"] = currentRow.GetString("TOOLVERSION");

                                receiptLotRow["TOOLNUMBER"] = currentRow.GetString("TOOLNUMBER");
                                receiptLotRow["RECEIPTDATE"] = receiptDate.ToString("yyyy-MM-dd HH:mm:ss");
                                receiptLotRow["RECEIPTSEQUENCE"] = "";
                                receiptLotRow["RECEIPTAREAID"] = currentRow.GetString("RECEIPTAREAID");
                                receiptLotRow["REPAIRVENDORID"] = currentRow.GetString("MAKEVENDORID");

                                receiptLotRow["SENDSEQUENCE"] = currentRow.GetString("SENDSEQUENCE");

                                receiptLotRow["REQUESTSEQUENCE"] = currentRow.GetString("REQUESTSEQUENCE");
                                receiptLotRow["TOOLCODE"] = currentRow.GetString("TOOLCODE");
                                receiptLotRow["TOOLVERSION"] = currentRow.GetString("TOOLVERSION");


                                receiptLotRow["WEIGHT"] = currentRow.GetString("WEIGHT");
                                receiptLotRow["HORIZONTAL"] = currentRow.GetString("HORIZONTAL");
                                receiptLotRow["VERTICAL"] = currentRow.GetString("VERTICAL");
                                receiptLotRow["HEIGHT"] = currentRow.GetString("THEIGHT");
                                receiptLotRow["THICKNESS"] = currentRow.GetString("POLISHTHICKNESS");


                                receiptLotRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                                receiptLotRow["PLANTID"] = Conditions.GetValue("P_PLANTID");
                                receiptLotRow["POLISH"] = currentRow.GetBoolean("POLISH") ? "Y" : "N";
                                receiptLotRow["LOTHISTKEY"] = "";

                                //내역등록입력종료

                                receiptLotRow["CREATOR"] = UserInfo.Current.Id;
                                receiptLotRow["MODIFIER"] = UserInfo.Current.Id;

                                //로직상 무조건 추가만 진행된다.
                                receiptLotRow["_STATE_"] = "added";
                                receiptLotRow["VALIDSTATE"] = "Valid";


                                receiptLotTable.Rows.Add(receiptLotRow);
                            }

                            toolReceiptSet.Tables.Add(receiptLotTable);
                        }
                        DataTable resultTable = this.ExecuteRule<DataTable>("ToolRepairReceipt", toolReceiptSet);

                        ////Server에서 결과값을 현재는 반환하지 않음 만약 반환해야 한다면 아래로직 구현
                        DataRow resultRow = resultTable.Rows[0];

                        ControlEnableProcess("modified");

                        ////가져온 결과값에 따라 재검색한 후 가져온 아이디에 맞는 행을 선택으로 변경한다.
                        Research(resultRow.GetString("RECEIPTSEQUENCE"), receiptDate.ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        this.ShowMessage(MessageBoxButtons.OK, "ToolDetailValidation", "");
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
            grdInputToolRepairReceipt.View.DeleteCheckedRows();
        }
        #endregion

        #region IsCurrentProcess : 현재 화면 상태에 따라 버튼 동작유무를 결정한다.
        private bool IsCurrentProcess()
        {
            if (_currentStatus != "added")
                return false;

            if (!ValidateEditValue(deReceiptDate.EditValue))
                return false;

            if (!ValidateEditValue(txtReceiptUser.EditValue))
                return false;

            return true;
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
                grdInputToolRepairReceipt.View.SetRowCellValue(rowHandle, "RECEIPTAREAID", savedResult.Rows[0].GetString("AREAID"));
                grdInputToolRepairReceipt.View.SetRowCellValue(rowHandle, "RECEIPTAREA", savedResult.Rows[0].GetString("AREANAME"));
            }
            else
            {
                grdInputToolRepairReceipt.View.SetRowCellValue(rowHandle, "RECEIPTAREAID", "");
                grdInputToolRepairReceipt.View.SetRowCellValue(rowHandle, "RECEIPTAREA", "");

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
                popupGridToolArea.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"CURRENTLOGINID={UserInfo.Current.Id}");


            if (conditionFactoryBox != null)
                conditionFactoryBox.Query = new SqlQuery("GetFactoryListByEqp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");


            if (makeVendorPopup != null)
                makeVendorPopup.SearchQuery = new SqlQuery("GetVendorListByTool", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            if (areaCondition != null)
                areaCondition.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"CURRENTLOGINID={UserInfo.Current.Id}");

            if (userCodePopup != null)
                userCodePopup.SearchQuery = new SqlQuery("GetUserListForPopupByTool", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            if (vendorPopup != null)
                vendorPopup.SearchQuery = new SqlQuery("GetVendorListByTool", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            if (productPopup != null)
                productPopup.SearchQuery = new SqlQuery("GetProductdefidPoplistByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"PRODUCTDEFTYPE=Product");

            //작업장 검색조건 초기화
            ((SmartSelectPopupEdit)Conditions.GetControl("AREAID")).ClearValue();
        }
        #endregion

        #endregion
    }
}
