#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.ToolManagement.Popup;

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
    /// 프 로 그 램 명  : 치공구관리> 치공구제작수정 > 치공구제작입고
    /// 업  무  설  명  : 제작/수정의뢰된 치공구를 입고한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-07-18
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ReceiptRequestToolMaking : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        string _searchVendorID;                                       // 설명
        string _searchUserID;
        SmartBandedGrid grdList = new SmartBandedGrid();    // Main Grid
        ConditionItemSelectPopup popupGridToolArea;
        ConditionItemComboBox conditionFactoryBox;
        ConditionItemSelectPopup vendorPopup;
        ConditionItemSelectPopup userCodePopup;
        ConditionItemSelectPopup productPopup;
        ConditionItemSelectPopup areaCondition;

        string _isModify = "N";
        #endregion

        #region 생성자

        public ReceiptRequestToolMaking()
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
            InitializeGridReceiptInput();
            InitRequiredControl();

            deInboundDate.ReadOnly = true;
            deInboundDate.EditValue = DateTime.Now;
        }

        #region InitializeControl
        private void InitializeControl()
        {
            
        }
        #endregion

        #region InitRequiredControl - 필수입력항목들을 체크한다.
        private void InitRequiredControl()
        {
            SetRequiredValidationControl(lblInboundDate);
        }
        #endregion

        #region InitializeGrid - 제작입고목록을 초기화한다.
        /// <summary>        
        /// 치공구내역목록을 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdToolRequestReceipt.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기            
            grdToolRequestReceipt.View.SetIsReadOnly();
            grdToolRequestReceipt.View.AddTextBoxColumn("RECEIPTDATE", 150)              //입고일자
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdToolRequestReceipt.View.AddTextBoxColumn("TOOLMAKETYPE")
                .SetIsHidden();                                                         //제작구분
            grdToolRequestReceipt.View.AddTextBoxColumn("TOOLMAKETYPENAME", 100)
                .SetTextAlignment(TextAlignment.Center);                                //제작구분명
            grdToolRequestReceipt.View.AddTextBoxColumn("TOOLNUMBER", 150)
                ;                                                                       //Tool번호
            grdToolRequestReceipt.View.AddTextBoxColumn("TOOLCODE", 150)
                ;                                                                       //Tool코드
            grdToolRequestReceipt.View.AddTextBoxColumn("TOOLVERSION", 60);            //Tool버전
            grdToolRequestReceipt.View.AddTextBoxColumn("TOOLNAME", 350);               //치공구명
            grdToolRequestReceipt.View.AddTextBoxColumn("PRODUCTDEFID", 100)
                ;                                                                       //품목코드
            grdToolRequestReceipt.View.AddTextBoxColumn("PRODUCTDEFNAME", 250);         //품목명
            grdToolRequestReceipt.View.AddComboBoxColumn("TOOLFORMCODE", 150, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=ToolForm"), "CODENAME", "CODEID")
               .SetTextAlignment(TextAlignment.Center)
               .SetIsReadOnly(true);                                                  //상태 - (이 회면에서 입력해야 한다면 SetIsReadOnly를 풀어준다.
            grdToolRequestReceipt.View.AddTextBoxColumn("TOOLCATEGORYID")
                .SetIsHidden();                                                        //Tool구분아이디
            grdToolRequestReceipt.View.AddTextBoxColumn("TOOLCATEGORY", 120)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                  //Tool구분
            grdToolRequestReceipt.View.AddTextBoxColumn("TOOLCATEGORYDETAILID")
                .SetIsHidden();                                                        //Tool유형아이디
            grdToolRequestReceipt.View.AddTextBoxColumn("TOOLCATEGORYDETAIL", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                  //Tool유형
            grdToolRequestReceipt.View.AddTextBoxColumn("TOOLDETAILID")
                .SetIsHidden();                                                        //Tool유형아이디
            grdToolRequestReceipt.View.AddTextBoxColumn("TOOLDETAIL", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                  //Tool유형
            grdToolRequestReceipt.View.AddTextBoxColumn("VENDORID")
                .SetIsHidden();                                                         //제작업체아이디
            grdToolRequestReceipt.View.AddTextBoxColumn("MAKEVENDOR", 120)
                ;                                                                       //제작업체
            grdToolRequestReceipt.View.AddTextBoxColumn("AREAID")
                .SetIsHidden();                                                         //작업장아이디
            grdToolRequestReceipt.View.AddTextBoxColumn("RECEIPTAREA", 120)
                ;                                                                       //작업장
            grdToolRequestReceipt.View.AddTextBoxColumn("RECEIPTERID")
                .SetIsHidden();                                                         //의뢰자명
            grdToolRequestReceipt.View.AddTextBoxColumn("RECEIPTUSER", 90);              //의뢰자
            grdToolRequestReceipt.View.AddSpinEditColumn("WEIGHT", 100)
                .SetDisplayFormat("#,###.####")
                .IsFloatValue = true;                                                    //무게
            grdToolRequestReceipt.View.AddSpinEditColumn("HORIZONTAL", 100)
                .SetDisplayFormat("#,###.####")
                .IsFloatValue = true;                                                    //가로
            grdToolRequestReceipt.View.AddSpinEditColumn("VERTICAL", 100)
                .SetDisplayFormat("#,###.####")
                .IsFloatValue = true;                                                    //세로
            grdToolRequestReceipt.View.AddSpinEditColumn("THEIGHT", 100)
                .SetDisplayFormat("#,###.####")
                .IsFloatValue = true;                                                     //높이
            grdToolRequestReceipt.View.AddSpinEditColumn("CREATEDTHICKNESS", 100)
                .SetDisplayFormat("#,###.####")
                .IsFloatValue = true;                                                     //두께
            grdToolRequestReceipt.View.AddTextBoxColumn("REQUESTCOMMENT", 250);           //제작사유            
            grdToolRequestReceipt.View.PopulateColumns();
        }
        #endregion

        #region InitializeGridReceiptInput - 제작입고목록입력을 위한 그리드를 초기화한다.
        /// <summary>        
        /// 제작입고목록입력을 위한 그리드를 초기화한다.
        /// </summary>
        private void InitializeGridReceiptInput()
        {
            // TODO : 그리드 초기화 로직 추가
            grdInputToolRequestReceipt.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기       
            grdInputToolRequestReceipt.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdInputToolRequestReceipt.View.AddTextBoxColumn("RECEIPTDATE", 150)             //입고일자
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsHidden();
            grdInputToolRequestReceipt.View.AddTextBoxColumn("RECEIPTUSER", 80)               //입고자
                .SetIsHidden();
            grdInputToolRequestReceipt.View.AddTextBoxColumn("TOOLNUMBER", 150)                
                .SetIsReadOnly(true);                                                       //Tool 번호
            grdInputToolRequestReceipt.View.AddTextBoxColumn("TOOLNAME", 350)
                .SetIsReadOnly(true);                                                       //Tool 번호

            grdInputToolRequestReceipt.View.AddTextBoxColumn("AREAID", 80)                  //작업장아이디
                .SetIsHidden();
            InitializeAreaPopupColumnInDetailGrid();
            grdInputToolRequestReceipt.View.AddTextBoxColumn("ISMODIFY", 80)                //작업장권한
                .SetIsHidden();
            //grdInputToolRequestReceipt.View.AddComboBoxColumn("RECEIPTAREA", 150, new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID")
            //    .SetValidationIsRequired();                                                 //작업장
            grdInputToolRequestReceipt.View.AddSpinEditColumn("WEIGHT", 120)
                .SetDisplayFormat("#,###.####")
                .IsFloatValue = true;                                                       //무게
            grdInputToolRequestReceipt.View.AddSpinEditColumn("HORIZONTAL", 120)
                .SetDisplayFormat("#,###.####")
                .IsFloatValue = true;                                                       //가로
            grdInputToolRequestReceipt.View.AddSpinEditColumn("VERTICAL", 120)
                .SetDisplayFormat("#,###.####")
                .IsFloatValue = true;                                                       //세로
            grdInputToolRequestReceipt.View.AddSpinEditColumn("THEIGHT", 120)
                .SetDisplayFormat("#,###.####")
                .IsFloatValue = true;                                                       //높이
            grdInputToolRequestReceipt.View.AddSpinEditColumn("CREATEDTHICKNESS", 120)
                .SetDisplayFormat("#,###.####")
                .IsFloatValue = true;                                                       //두께
            grdInputToolRequestReceipt.View.AddTextBoxColumn("REQUESTDATE")
                .SetIsReadOnly(true);                                                       //의뢰일자
            grdInputToolRequestReceipt.View.AddTextBoxColumn("REQUESTSEQUENCE", 80)
                .SetIsReadOnly(true);                                                       //의뢰순번
            grdInputToolRequestReceipt.View.AddTextBoxColumn("PRODUCTDEFID")
                .SetIsReadOnly(true);                                                       //품목코드
            grdInputToolRequestReceipt.View.AddTextBoxColumn("PRODUCTDEFNAME", 250)
                .SetIsReadOnly(true);                                                       //품목명
            grdInputToolRequestReceipt.View.AddTextBoxColumn("TOOLCODE")
                .SetIsHidden();                                                             //툴코드(DurableDefID)
            grdInputToolRequestReceipt.View.AddTextBoxColumn("DURABLEDEFVERSION")
                .SetIsHidden();                                                             //툴코드버전(DurableDefVersion)
            grdInputToolRequestReceipt.View.AddTextBoxColumn("VENDORID")
                .SetIsHidden();                                                             //제작업체
            grdInputToolRequestReceipt.View.AddTextBoxColumn("OWNSHIPTYPE")
                .SetIsHidden();                                                             //소유구분


            grdInputToolRequestReceipt.View.PopulateColumns();
        }
        #endregion

        #region InitializeAreaPopupColumnInDetailGrid - 작업장을 팝업검색할 필드 설정
        /// <summary>
        /// grid ProcesssegmentidLis
        /// </summary>
        private void InitializeAreaPopupColumnInDetailGrid()
        {
            popupGridToolArea = this.grdInputToolRequestReceipt.View.AddSelectPopupColumn("RECEIPTAREA", 150, new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"CURRENTLOGINID={UserInfo.Current.Id}", $"ISMOD=Y"))
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
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    int i = 0;
                    int currentIndex = grdInputToolRequestReceipt.View.GetFocusedDataSourceRowIndex();

                    foreach (DataRow row in selectedRows)
                    {
                        if (i == 0)
                        {
                            grdInputToolRequestReceipt.View.GetFocusedDataRow()["AREAID"] = row["AREAID"];
                            grdInputToolRequestReceipt.View.GetFocusedDataRow()["RECEIPTAREA"] = row["AREANAME"];
                            grdInputToolRequestReceipt.View.GetFocusedDataRow()["ISMODIFY"] = row["ISMODIFY"];
                        }
                        i++;
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

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            btnSearchTool.Click += BtnSearchTool_Click;
            btnInitialize.Click += BtnInitialize_Click;
            btnModify.Click += BtnModify_Click;
            btnErase.Click += BtnErase_Click;

            Shown += ReceiptRequestToolMaking_Shown;

            grdInputToolRequestReceipt.View.AddingNewRow += grdInputToolRequestReceipt_AddingNewRow;
            tabMakeReceipt.SelectedPageChanged += TabMakeReceipt_SelectedPageChanged;
        }

        #region ReceiptRequestToolMaking_Shown - Site관련정보를 화면로딩후 설정한다.
        private void ReceiptRequestToolMaking_Shown(object sender, EventArgs e)
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

        #region TabMakeReceipt_SelectedPageChanged - 탭페이지 변경이벤트
        private void TabMakeReceipt_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page == tabPageMakeReceipt)
            {
                if (pnlToolbar.Controls["layoutToolbar"] != null)
                    foreach (Control subControl in pnlToolbar.Controls["layoutToolbar"].Controls)
                    {
                        if (subControl is SmartButton)
                            subControl.Visible = true;
                    }
            }
            else
            {
                if (pnlToolbar.Controls["layoutToolbar"] != null)
                    foreach (Control subControl in pnlToolbar.Controls["layoutToolbar"].Controls)
                    {
                        if (subControl is SmartButton)
                            subControl.Visible = false;
                    }
            }
        }
        #endregion

        #region BtnErase_Click - 삭제버튼 이벤트
        private void BtnErase_Click(object sender, EventArgs e)
        {
            if (tabMakeReceipt.SelectedTabPage == tabPageMakeReceipt)
                DeleteData();
        }
        #endregion

        #region grdInputToolRequestReceipt_AddingNewRow - 그리드행추가이벤트
        private void grdInputToolRequestReceipt_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            
        }
        #endregion

        #region BtnModify_Click - 수정버튼이벤트
        private void BtnModify_Click(object sender, EventArgs e)
        {
            if(tabMakeReceipt.SelectedTabPage == tabPageMakeReceipt)
                SaveData();
        }
        #endregion

        #region BtnInitialize_Click - 초기화버튼이벤트
        private void BtnInitialize_Click(object sender, EventArgs e)
        {
            if (tabMakeReceipt.SelectedTabPage == tabPageMakeReceipt)
                InitializeInsertForm();
        }
        #endregion

        #region BtnSearchTool_Click - 치공구검색이벤트 - 제작입고할 치공구를 조회한다.
        private void BtnSearchTool_Click(object sender, EventArgs e)
        {
            if (tabMakeReceipt.SelectedTabPage == tabPageMakeReceipt)
            {
                ToolNumberPopup toolNumberPopup = new ToolNumberPopup(Conditions.GetValue("P_PLANTID").ToString());
                toolNumberPopup.choiceHandler += LoadData;

                if (Conditions.GetValue("VENDORNAME").ToString() != "")
                {
                    toolNumberPopup.SetMakeVendor(_searchVendorID, Conditions.GetValue("VENDORNAME").ToString());
                }

                toolNumberPopup.ShowDialog();
            }
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

        #region OnSearchAsync : 검색
        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            if (tabMakeReceipt.SelectedTabPage != tabPageMakeReceiptList)
                tabMakeReceipt.SelectedTabPage = tabPageMakeReceiptList;

            InitializeInsertForm();
            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);

            if (Conditions.GetValue("VENDORNAME").ToString() != "")
                values.Add("VENDORID", _searchVendorID);
            //if (Conditions.GetValue("USERNAME").ToString() != "")
            //    values.Add("USERID", _searchUserID);
            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolReqSearchResult = SqlExecuter.Query("GetToolMakeReceiptListByTool", "10001", values);



            grdToolRequestReceipt.DataSource = toolReqSearchResult;

            //입력그리드는 제거한다.
            grdInputToolRequestReceipt.DataSource = null;

            if (toolReqSearchResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
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
            if (tabMakeReceipt.SelectedTabPage != tabPageMakeReceiptList)
                tabMakeReceipt.SelectedTabPage = tabPageMakeReceiptList;
            // TODO : 조회 SP 변경
            InitializeInsertForm();

            var values = Conditions.GetValues();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);

            if (Conditions.GetValue("VENDORNAME").ToString() != "")
                values.Add("VENDORID", _searchVendorID);
            //if (Conditions.GetValue("USERNAME").ToString() != "")
            //    values.Add("USERID", _searchUserID);
            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolReqSearchResult = SqlExecuter.Query("GetToolMakeReceiptListByTool", "10001", values);

            grdToolRequestReceipt.DataSource = toolReqSearchResult;

            //입력그리드는 제거한다.
            grdInputToolRequestReceipt.DataSource = null;

            if (toolReqSearchResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }            
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
            InitializeCondition_Vendor();
            //InitializeCondition_UserPopup();
            InitializeConditionAreaPopup();
            InitializeCondition_ToolCodePopup();
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
        /// <summary>
        /// 제작업체 설정
        /// </summary>
        private void InitializeCondition_Vendor()
        {
            vendorPopup = Conditions.AddSelectPopup("VENDORNAME", new SqlQuery("GetVendorListByTool", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
            .SetPopupLayout("MAKEVENDOR", PopupButtonStyles.Ok_Cancel, true, false)
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
            .SetPopupResultMapping("AREAID", "AREAID")
            .SetPopupApplySelection((selectedRows, dataGridRow) =>
            {
                foreach (DataRow row in selectedRows)
                {
                    for(int index = 0; index < grdInputToolRequestReceipt.View.RowCount; index++)
                    {
                        grdInputToolRequestReceipt.View.SetRowCellValue(index, "RECEIPTAREAID", row["AREAID"]);
                        grdInputToolRequestReceipt.View.SetRowCellValue(index, "RECEIPTAREA", row["AREANAME"]);
                        grdInputToolRequestReceipt.View.SetRowCellValue(index, "ISMODIFY", row.GetString("ISMODIFY"));
                    }

                    _isModify = row.GetString("ISMODIFY");
                }
            })
            ;

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
        private void InitializeCondition_UserPopup()
        {
            userCodePopup = Conditions.AddSelectPopup("USERNAME", new SqlQuery("GetUserListForPopupByTool", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
            .SetPopupLayout("USERNAME", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow)
            .SetPopupResultMapping("USERNAME", "USERNAME")
            .SetLabel("USERNAME")
            .SetPopupResultCount(1)
            .SetPosition(0.2)
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

        ///// <summary>
        ///// 제작업체 설정
        ///// </summary>
        //private void InitializeCondition_User()
        //{

        //    var planttxtbox = Conditions.AddComboBox("USERID", new SqlQuery("GetUserListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "USERNAME", "USERID")
        //       .SetLabel("USERID")
        //       .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
        //       .SetPosition(0.3)
        //       .SetEmptyItem("","",true)
        //    ;
        //}

        /// <summary>
        /// 품목코드 조회 설정
        /// </summary>
        private void InitializeCondition_ToolCodePopup()
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

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }

        #region ValidateContent - 내용을 Validation한다.
        private bool ValidateContent(out string messageCode)
        {
            //각각의 상태에 따라 보여줄 메세지가 상이하다면 각각의 분기에서 메세지를 표현해야 한다.            
            messageCode = "";

            //요청일자는 필수이다.
            if (!ValidateEditValue(deInboundDate.EditValue))
            {
                messageCode = "ToolMakeReceiptServerValidation";
                return false;
            }

            if (grdInputToolRequestReceipt.View.RowCount < 1)
            {
                messageCode = "ToolMakeReceiptServerValidation";
                return false;
            }

            //전체 행을 검사한다.
            DataTable dataTables = (DataTable)grdInputToolRequestReceipt.DataSource;
            foreach (DataRow currentRow in dataTables.Rows)
            {
                if (!ValidateCellInGrid(currentRow, new string[] { "RECEIPTAREA" }))
                {
                    messageCode = "ToolMakeReceiptServerValidation";
                    return false;
                }

                if(currentRow.GetString("ISMODIFY").Equals("N"))
                {
                    messageCode = "ValidateMakeReceiptToolAreaReq";  //제작입고할 치공구들의 작업장중 권한이 없는 작업장이 있습니다.
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region ValidateCellInGrid - 그리드내의 특정컬럼을 Validate
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

        #region ValidateComboBoxEqualValue - 2개의 콤보박스의 데이터를 비교한다.
        private bool ValidateComboBoxEqualValue(SmartComboBox originBox, SmartComboBox targetBox)
        {
            //두 콤보박스의 값이 같으면 안된다.
            if (originBox.EditValue.Equals(targetBox.EditValue))
                return false;
            return true;
        }
        #endregion

        #region ValidateNumericBox - 숫자를 입력하는 컨트롤을 대상으로 하여 입력받은 기준값보다 값이 같거나 크다면 true 작다면 false를 반환하는 메소드
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

        #region ValidateEditValue - 입력받은 editValue에 아무런 값이 입력되지 않았다면 false 입력받았다면 true
        private bool ValidateEditValue(object editValue)
        {
            if (editValue == null)
                return false;

            if (editValue.ToString() == "")
                return false;

            return true;
        }
        #endregion

        #region SetRequiredValidationControl - 필수입력항목설정
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
                //의뢰일은 오늘날짜를 기본으로 한다.
                DateTime dateNow = DateTime.Now;
                deInboundDate.EditValue = dateNow;

                //그리드를 비운다.
                grdInputToolRequestReceipt.DataSource = null;

                //컨트롤 접근여부는 작성상태로 변경한다.
                ControlEnableProcess("added");                
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
                deInboundDate.ReadOnly = true;
            }
            else if (currentStatus == "modified") //
            {
                deInboundDate.ReadOnly = true;
            }
            else
            {
                deInboundDate.ReadOnly = true;
            }
        }
        #endregion

        #region LoadData - 팝업창에서 호출하는 메소드
        /// <summary>
        /// 팝업창에서 호출하는 메소드
        /// </summary>
        private void LoadData(DataTable table)
        {
            //저장 및 조회 모두 사용하는 테이블 할당
            DataTable bindTable = CreateSaveDatatable();

            foreach(DataRow currentRow in table.Rows)
            {
                DataRow bindRow = bindTable.NewRow();

                bindRow["REQUESTDATE"] = currentRow["REQUESTDATE"];
                bindRow["REQUESTSEQUENCE"] = currentRow["REQUESTSEQUENCE"];
                bindRow["TOOLNUMBER"] = currentRow["TOOLNUMBER"];
                bindRow["TOOLCODE"] = currentRow["TOOLCODE"];
                bindRow["TOOLNAME"] = currentRow["TOOLNAME"];
                bindRow["DURABLEDEFVERSION"] = currentRow["DURABLEDEFVERSION"];
                bindRow["PRODUCTDEFID"] = currentRow["PRODUCTDEFID"];
                bindRow["PRODUCTDEFNAME"] = currentRow["PRODUCTDEFNAME"];
                bindRow["TOOLCATEGORYID"] = currentRow["TOOLCATEGORYID"];
                bindRow["TOOLCATEGORY"] = currentRow["TOOLCATEGORY"];
                bindRow["TOOLCATEGORYDETAILID"] = currentRow["TOOLCATEGORYDETAILID"];
                bindRow["TOOLCATEGORYDETAIL"] = currentRow["TOOLCATEGORYDETAIL"];
                bindRow["TOOLDETAILID"] = currentRow["TOOLDETAILID"];
                bindRow["TOOLDETAIL"] = currentRow["TOOLDETAIL"];
                bindRow["REQUESTDEPARTMENT"] = currentRow["REQUESTDEPARTMENT"];
                bindRow["REQUESTUSERID"] = currentRow["REQUESTUSERID"];
                bindRow["REQUESTUSER"] = currentRow["REQUESTUSER"];
                bindRow["VENDORID"] = currentRow["VENDORID"];
                bindRow["VENDORNAME"] = currentRow["VENDORNAME"];
                bindRow["REQUESTDELIVERYDATE"] = currentRow["REQUESTDELIVERYDATE"];
                bindRow["PLANDELIVERYDATE"] = currentRow["PLANDELIVERYDATE"];

                if(Conditions.GetValue("AREAID") != null)
                {
                    bindRow["RECEIPTAREA"] = ((SmartSelectPopupEdit)Conditions.GetControl("AREAID")).Text;
                    bindRow["AREAID"] = Conditions.GetValue("AREAID");
                    bindRow["ISMODIFY"] = _isModify;
                }
                else
                {
                    bindRow["RECEIPTAREA"] = currentRow["AREANAME"];
                    bindRow["AREAID"] = currentRow["AREAID"];
                    bindRow["ISMODIFY"] = currentRow["ISMODIFY"];
                }                

                bindRow["WEIGHT"] = 0;
                bindRow["THEIGHT"] = 0;
                bindRow["THICKNESS"] = 0;
                bindRow["HORIZONTAL"] = 0;
                bindRow["VERTICAL"] = 0;
                bindRow["OWNSHIPTYPE"] = currentRow["OWNSHIPTYPE"];                

                bindTable.Rows.Add(bindRow);
            }

            grdInputToolRequestReceipt.DataSource = bindTable;
        }
        #endregion

        #region CreateSaveDatatable : ToolRequest 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// <summary>
        /// ToolRequest 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// 입력시에는 ToolRequest와 ToolRequestDetail에 입력하고
        /// 수정시에는 ToolRequest와 ToolRequestDetail 및 ToolRequestDetailLot에 까지 데이터를 기입한다.
        /// </summary>
        /// <returns></returns>
        private DataTable CreateSaveDatatable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "toolMakeReceiptList";
            //===================================================================================
            //Key값 ToolRequest 및 ToolRequestDetail
            dt.Columns.Add("RECEIPTDATE");
            dt.Columns.Add("RECEIPTUSER");
            dt.Columns.Add("WEIGHT");
            dt.Columns.Add("HORIZONTAL");
            dt.Columns.Add("VERTICAL");
            dt.Columns.Add("THEIGHT");
            dt.Columns.Add("HEIGHT");
            dt.Columns.Add("THICKNESS");
            dt.Columns.Add("CREATEDTHICKNESS");

            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");

            dt.Columns.Add("REQUESTDATE");
            dt.Columns.Add("REQUESTSEQUENCE");
            dt.Columns.Add("TOOLNUMBER");
            dt.Columns.Add("DURABLEDEFVERSION");
            dt.Columns.Add("PRODUCTDEFID");
            dt.Columns.Add("PRODUCTDEFVERSION");
            dt.Columns.Add("TOOLCODE");
            dt.Columns.Add("TOOLNAME");

            dt.Columns.Add("PRODUCTDEFNAME");
            dt.Columns.Add("TOOLCATEGORYID");
            dt.Columns.Add("TOOLCATEGORY");
            dt.Columns.Add("TOOLCATEGORYDETAILID");
            dt.Columns.Add("TOOLCATEGORYDETAIL");
            dt.Columns.Add("TOOLDETAILID");
            dt.Columns.Add("TOOLDETAIL");
            dt.Columns.Add("REQUESTDEPARTMENT");
            dt.Columns.Add("REQUESTUSERID");
            dt.Columns.Add("REQUESTUSER");
            dt.Columns.Add("VENDORID");
            dt.Columns.Add("VENDORNAME");
            dt.Columns.Add("REQUESTDELIVERYDATE");
            dt.Columns.Add("PLANDELIVERYDATE");
            dt.Columns.Add("AREAID");
            dt.Columns.Add("RECEIPTAREA");
            dt.Columns.Add("AREANAME");
            dt.Columns.Add("OWNSHIPTYPE");

            dt.Columns.Add("ISMODIFY");

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
            //dt.Columns.Add("_STATE_");

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
                btnInitialize.Enabled = false;
                btnModify.Enabled = false;
                btnErase.Enabled = false;

                string messgaeCode = "";

                if (grdInputToolRequestReceipt.View.RowCount > 0)
                {
                    //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                    if (ValidateContent(out messgaeCode))
                    {
                        DataSet toolReceiptSet = new DataSet();
                        //치공구 제작의뢰를 입력
                        DataTable toolReceiptData = CreateSaveDatatable();
                        //_STATE_열을 생성
                        toolReceiptData.Columns.Add("_STATE_");

                        DataTable inputTable = (DataTable)grdInputToolRequestReceipt.DataSource;

                        if (inputTable.Rows.Count > 0)
                        {

                            DateTime inboundDate = DateTime.Now;

                            foreach (DataRow inputRow in inputTable.Rows)
                            {
                                DataRow toolReceiptRow = toolReceiptData.NewRow();
                                //ToolRequestDetailLot에는 Date타입이지만 ToolMakeReceipt에는 TimeStamp
                                toolReceiptRow["RECEIPTDATE"] = inboundDate.ToString("yyyy-MM-dd HH:mm:ss");

                                toolReceiptRow["REQUESTDATE"] = inputRow["REQUESTDATE"];
                                toolReceiptRow["REQUESTSEQUENCE"] = inputRow["REQUESTSEQUENCE"];
                                toolReceiptRow["TOOLNUMBER"] = inputRow["TOOLNUMBER"];
                                toolReceiptRow["DURABLEDEFVERSION"] = inputRow["DURABLEDEFVERSION"];
                                toolReceiptRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                                toolReceiptRow["PLANTID"] = Conditions.GetValue("P_PLANTID");
                                toolReceiptRow["TOOLCODE"] = inputRow["TOOLCODE"];
                                toolReceiptRow["RECEIPTAREA"] = inputRow["AREAID"];
                                toolReceiptRow["VENDORID"] = inputRow["VENDORID"];
                                toolReceiptRow["OWNSHIPTYPE"] = inputRow["OWNSHIPTYPE"];
                                //toolReceiptRow["PRODUCTDEFID"]
                                //toolReceiptRow["PRODUCTDEFVERSION"]
                                toolReceiptRow["WEIGHT"] = inputRow["WEIGHT"];
                                toolReceiptRow["HORIZONTAL"] = inputRow["HORIZONTAL"];
                                toolReceiptRow["VERTICAL"] = inputRow["VERTICAL"];
                                toolReceiptRow["HEIGHT"] = inputRow["THEIGHT"];
                                toolReceiptRow["THICKNESS"] = inputRow["CREATEDTHICKNESS"];
                                toolReceiptRow["RECEIPTUSER"] = UserInfo.Current.Id;
                                toolReceiptRow["CREATOR"] = UserInfo.Current.Id;
                                toolReceiptRow["MODIFIER"] = UserInfo.Current.Id;
                                toolReceiptRow["VALIDSTATE"] = "Valid";
                                toolReceiptRow["_STATE_"] = "added";

                                //현재 화면의 상태에 따라 생성/수정으로 분기된다. -- 현재 수정은 불가함으로 처리
                                //if (_currentStatus == "added")
                                //{
                                //    toolRequestRow["_STATE_"] = "added";
                                //}
                                //else
                                //{
                                //    toolRequestRow["_STATE_"] = "modified";
                                //}

                                toolReceiptData.Rows.Add(toolReceiptRow);
                            }
                            toolReceiptSet.Tables.Add(toolReceiptData);

                            //서버에 데이터가 입력이 가능한지 검증 (트랜잭션 관련문제로 서버가 아닌 클라이언트에서 호출)
                            if (ValidateContentToServer(toolReceiptData))
                            {
                                DataTable resultTable = this.ExecuteRule<DataTable>("ToolMakeReceipt", toolReceiptSet);

                                ////Server에서 결과값을 현재는 반환하지 않음 만약 반환해야 한다면 아래로직 구현
                                //DataRow resultRow = resultTable.Rows[0];

                                ControlEnableProcess("modified");

                                Research();
                            }
                            else
                            {
                                this.ShowMessage(MessageBoxButtons.OK, "ToolMakeReceiptServerValidation", "");
                            }
                        }
                        else
                        {
                            this.ShowMessage(MessageBoxButtons.OK, "CheckSelectData", "");
                        }
                    }
                    else
                    {
                        this.ShowMessage(MessageBoxButtons.OK, messgaeCode, "");
                    }
                }
                else
                {
                    //ShowMessage(MessageBoxButtons.OK, String.Format("SelectItem", Language.GetDictionary("DURABLE")), "");
                    ShowMessage(MessageBoxButtons.OK, "SelectItem", Language.Get("MAKERECEIPT"));
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

        #region DeleteData : 삭제를 수행
        private void DeleteData()
        {
            //Validation 체크 부분 작성 필요

            this.ShowWaitArea();

            //저장 로직
            try
            {
                btnInitialize.Enabled = false;
                btnModify.Enabled = false;
                btnErase.Enabled = false;

                if (grdInputToolRequestReceipt.View.GetCheckedRows() != null)
                {
                    DialogResult result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnErase.Text);//삭제하시겠습니까? 

                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        //DataTable checkedTable = grdInputToolRequestReceipt.View.GetCheckedRows();

                        //foreach(DataRow cuRow in checkedTable.Rows)
                        //{
                        //    if(cuRow.GetString("ISMODIFY").Equals("N"))
                        //    {
                        //        ShowMessage("ValidateDeleteReceiptToolAreaReq"); //삭제할 치공구(들)의 작업장중 권한이 없는 작업장이 있습니다.

                        //        return;
                        //    }
                        //}
                        grdInputToolRequestReceipt.View.RemoveRow(grdInputToolRequestReceipt.View.GetCheckedRowsHandle());
                    }
                }
                else
                {
                    //ShowMessage(MessageBoxButtons.OK, String.Format("SelectItem", Language.GetDictionary("DURABLE")), "");
                    ShowMessage(MessageBoxButtons.OK, "SelectItem", Language.Get("MAKERECEIPT"));
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

        #region ValidateContentToServer : 서버에서의 Validation을 실행한다.(입력가능한 데이터인지 검증)
        private bool ValidateContentToServer(DataTable inputTable)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();

            string durableLotIDs = "";

            foreach(DataRow validRow in inputTable.Rows)
            {
                if(durableLotIDs.Equals(""))
                    durableLotIDs += "'" + validRow.GetString("TOOLNUMBER") + "'";
                else
                    durableLotIDs += ", '" + validRow.GetString("TOOLNUMBER") + "'";
            }

            if(durableLotIDs.Equals(""))
            {                
                return false;
            }

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("DURABLELOTIDS", durableLotIDs);
            #endregion
            //values = Commons.CommonFunction.ConvertParameter(values);
            DataTable resultTable = SqlExecuter.Query("GetExistsToolMakeReceiptByTool", "10001", values);

            int resultCount = resultTable.Rows[0].GetInteger("RECEIPTCOUNT");

            if (inputTable.Rows.Count == resultCount)
                return true;
            
            return false;
        }
        #endregion

        #region ChangeSiteCondition - 검색조건의 Site변경시 관련된 쿼리들을 변경
        private void ChangeSiteCondition()
        {
            if (popupGridToolArea != null)
                popupGridToolArea.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"CURRENTLOGINID={UserInfo.Current.Id}", $"ISMOD=Y");


            if (conditionFactoryBox != null)
                conditionFactoryBox.Query = new SqlQuery("GetFactoryListByEqp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");


            if (vendorPopup != null)
                vendorPopup.SearchQuery = new SqlQuery("GetVendorListByTool", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            if (userCodePopup != null)
                userCodePopup.SearchQuery = new SqlQuery("GetUserListForPopupByTool", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            if (productPopup != null)
                productPopup.SearchQuery = new SqlQuery("GetProductdefidPoplistByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"PRODUCTDEFTYPE=Product");

            if (areaCondition != null)
                areaCondition.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"CURRENTLOGINID={UserInfo.Current.Id}");

            //사이트변경시 연관되는 검색조건은 초기화
            ((SmartSelectPopupEdit)Conditions.GetControl("VENDORNAME")).ClearValue();
            ((SmartSelectPopupEdit)Conditions.GetControl("AREAID")).ClearValue();
            
        }
        #endregion
        #endregion
    }
}
