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
    /// 프 로 그 램 명  : 치공구관리 > 필름관리 > 필름제작요청
    /// 업  무  설  명  : 필름에 대한 제작을 요청한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-08-19
    /// 수  정  이  력  : 
    /// 
    /// ** 요구사항 변경으로 인하여 현재 이 화면은 사용하지 않음
    /// </summary>
    public partial class RequestMakeFilm : SmartConditionManualBaseForm
    {
        #region Local Variables

        //화면의 현재 상태  added, modified, browse;
        private string _currentStatus = "browse";
        private string _filmSequence = "";
        private string _requestUserID = "";
        private string _makeVendorID = "";
        private string _receiptAreaID = "";
        private string _searchAreaID = "";
        private string _isModify = "";

        ConditionItemSelectPopup makeVendorPopup;
        ConditionItemSelectPopup areaCondition;
        ConditionItemSelectPopup receiptAreaPopup;
        ConditionItemSelectPopup filmCodeCondition;
        ConditionItemComboBox segmentCondition;
        #endregion

        #region 생성자
        public RequestMakeFilm()
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
            InitRequiredControl();
            InitializeGrid();
            InitializeMakeVendorsPopup();
            InitializeReceiptAreaPopup();

            InitializeIsCoatingCombo();
            InitializePriorityCombo();
            InitializeInsertForm();

            //팝업창 오픈후 입력받은 컨트롤들을 설정
            ucFilmCode.msgHandler += ShowMessageInfo;
            ucFilmCode.SetSmartTextBoxForSearchData(txtProductDefID, txtProductDefName, txtFilmType, txtFilmCategory, txtFilmDetailCategory, txtLayer, txtJobType
                , txtUseProcess, "PRODUCTDEFID");
        }

        private void InitialInputControls()
        {
            //필수항목 등록
        }

        #region InitRequiredControl - 필수입력항목들을 체크한다.
        private void InitRequiredControl()
        {
            SetRequiredValidationControl(lblRequestDate);
            SetRequiredValidationControl(lblReceiptArea);
            SetRequiredValidationControl(lblQty);
            SetRequiredValidationControl(lblRequestQty);
        }
        #endregion

        #region InitializeGrid - 필름내역목록을 초기화한다.
        /// <summary>        
        /// 필름내역목록을 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdFilmRequest.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기            
            grdFilmRequest.View.SetIsReadOnly();

            grdFilmRequest.View.AddTextBoxColumn("FILMSEQUENCE").SetIsHidden();                             //필름시퀀스
            grdFilmRequest.View.AddTextBoxColumn("FILMPROGRESSSTATUSID").SetIsHidden();                     //진행상태아이디
            grdFilmRequest.View.AddTextBoxColumn("FILMPROGRESSSTATUS", 80)
                .SetTextAlignment(TextAlignment.Center)
                ;                                                                                           //진행상태            
            grdFilmRequest.View.AddTextBoxColumn("REQUESTDEPARTMENT", 100);                                 //요청부서
            grdFilmRequest.View.AddTextBoxColumn("REQUESTUSERID").SetIsHidden();                            //요청자아이디                                               
            grdFilmRequest.View.AddTextBoxColumn("REQUESTUSER", 80);                                        //요청자
            grdFilmRequest.View.AddTextBoxColumn("REQUESTDATE", 160)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");                                                   //요청일시
            grdFilmRequest.View.AddTextBoxColumn("FILMCODE", 150);                                          //필름코드                                           
            grdFilmRequest.View.AddTextBoxColumn("FILMVERSION", 80);                                        //Rev No.
            grdFilmRequest.View.AddTextBoxColumn("FILMNAME", 350);                                          //필름명
            grdFilmRequest.View.AddTextBoxColumn("PRODUCTDEFID", 120);                                      //품목코드
            grdFilmRequest.View.AddTextBoxColumn("PRODUCTDEFNAME", 280);                                    //품목명
            grdFilmRequest.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80);                                  //품목버전
            grdFilmRequest.View.AddTextBoxColumn("JOBTYPEID", 80).SetIsHidden();                            //작업구분아이디
            grdFilmRequest.View.AddTextBoxColumn("JOBTYPE", 100)
                .SetTextAlignment(TextAlignment.Center);                                                    //작업구분
            grdFilmRequest.View.AddTextBoxColumn("PRODUCTIONTYPEID", 80).SetIsHidden();                     //생산구분아이디
            grdFilmRequest.View.AddTextBoxColumn("PRODUCTIONTYPE", 80);                                     //생산구분
            grdFilmRequest.View.AddTextBoxColumn("FILMTYPEID").SetIsHidden();                               //필름구분아이디
            grdFilmRequest.View.AddTextBoxColumn("FILMTYPE", 80)                                            //필름구분
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdFilmRequest.View.AddTextBoxColumn("FILMCATEGORYID").SetIsHidden();                           //필름유형아이디
            grdFilmRequest.View.AddTextBoxColumn("FILMCATEGORY", 80)                                        //필름유형
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdFilmRequest.View.AddTextBoxColumn("FILMDETAILCATEGORYID").SetIsHidden();                     //상세유형아이디
            grdFilmRequest.View.AddTextBoxColumn("FILMDETAILCATEGORY", 80)                                  //상세유형
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdFilmRequest.View.AddTextBoxColumn("FILMUSERLAYER", 80);                                      //Layer          
            grdFilmRequest.View.AddTextBoxColumn("CUSTOMERID").SetIsHidden();                               //고객사아이디
            grdFilmRequest.View.AddTextBoxColumn("CUSTOMER", 120);                                          //고객사
            grdFilmRequest.View.AddTextBoxColumn("USEPROCESSSEGMENT", 80).SetIsHidden();                    //사용공정
            grdFilmRequest.View.AddTextBoxColumn("RESOLUTION", 80);                                         //해상도
            grdFilmRequest.View.AddTextBoxColumn("ISCOATING", 80)                                           //코팅유무
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdFilmRequest.View.AddTextBoxColumn("CONTRACTIONX", 120);                                      //요청수출율X
            grdFilmRequest.View.AddTextBoxColumn("CONTRACTIONY", 120);                                      //요청수축율Y
            grdFilmRequest.View.AddTextBoxColumn("CHANGECONTRACTIONX", 120);                                //요청수축율(%)X
            grdFilmRequest.View.AddTextBoxColumn("CHANGECONTRACTIONY", 120);                                //요청수축율(%)Y
            grdFilmRequest.View.AddTextBoxColumn("QTY", 80);                                                //수량
            grdFilmRequest.View.AddTextBoxColumn("USEPLANDATE", 100).SetIsHidden();                         //사용예정일
            grdFilmRequest.View.AddTextBoxColumn("PRIORITYID").SetIsHidden();                               //우선순위아이디
            grdFilmRequest.View.AddTextBoxColumn("PRIORITY", 80)                                            //우선순위
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdFilmRequest.View.AddTextBoxColumn("MAKEVENDORID").SetIsHidden();                             //제작업체아이디
            grdFilmRequest.View.AddTextBoxColumn("MAKEVENDOR", 150);                                        //제작업체
            grdFilmRequest.View.AddTextBoxColumn("RECEIPTAREAID").SetIsHidden();                            //입고작업장아이디
            grdFilmRequest.View.AddTextBoxColumn("RECEIPTAREA", 150);                                       //입고작업장
            grdFilmRequest.View.AddTextBoxColumn("ISMODIFY").SetIsHidden();                                 //작업장권한여부
            grdFilmRequest.View.AddTextBoxColumn("REQUESTCOMMENT", 200);                                    //요청내용
            grdFilmRequest.View.AddTextBoxColumn("ACCEPTDATE", 160)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")                                                    //접수일시
                .SetIsHidden();
            grdFilmRequest.View.AddTextBoxColumn("ACCEPTDEPARTMENT", 100).SetIsHidden();                    //접수부서
            grdFilmRequest.View.AddTextBoxColumn("ACCEPTUSERID").SetIsHidden();                             //접수자아이디
            grdFilmRequest.View.AddTextBoxColumn("ACCEPTUSER", 100).SetIsHidden();                          //접수자
            grdFilmRequest.View.AddTextBoxColumn("RELEASEDATE", 160)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsHidden();                                                                             //출고일시
            grdFilmRequest.View.AddTextBoxColumn("RELEASEUSERID").SetIsHidden();                            //출고자아이디
            grdFilmRequest.View.AddTextBoxColumn("RELEASEUSER", 100).SetIsHidden();                         //출고자
            grdFilmRequest.View.AddTextBoxColumn("MEASURECONTRACTIONX", 80).SetIsHidden();                  //실측수축율X
            grdFilmRequest.View.AddTextBoxColumn("MEASURECONTRACTIONY", 80).SetIsHidden();                  //실측수축율Y
            grdFilmRequest.View.AddTextBoxColumn("RECEIVEDATE", 160)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsHidden();                                                                             //인수일시
            grdFilmRequest.View.AddTextBoxColumn("RECEIVEUSERID").SetIsHidden();                            //인수자아이디
            grdFilmRequest.View.AddTextBoxColumn("RECEIVEUSER", 100).SetIsHidden();                         //인수자
            grdFilmRequest.View.PopulateColumns();
        }
        #endregion

        #region InitializeMakeVendorsPopup : 제작업체 팝업창
        /// <summary>
        /// 팝업창 제어
        /// </summary>
        private void InitializeMakeVendorsPopup()
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
                    _makeVendorID = row.GetString("VENDORID");
                    popEditMakeVendor.EditValue = row.GetString("VENDORNAME");
                });

            });


            makeVendorPopup.Conditions.AddTextBox("VENDORNAME");

            makeVendorPopup.GridColumns.AddTextBoxColumn("VENDORID", 150)
                .SetIsHidden();
            makeVendorPopup.GridColumns.AddTextBoxColumn("VENDORNAME", 200);

            popEditMakeVendor.SelectPopupCondition = makeVendorPopup;
        }
        #endregion

        #region InitializeReceiptAreaPopup : 입고작업장 팝업창
        /// <summary>
        /// 팝업창 제어
        /// </summary>
        private void InitializeReceiptAreaPopup()
        {
            areaCondition = new ConditionItemSelectPopup();
            areaCondition.Id = "RECEIPTAREA";
            areaCondition.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CURRENTLOGINID={UserInfo.Current.Id}", $"ISMOD=Y");
            areaCondition.ValueFieldName = "AREAID";
            areaCondition.DisplayFieldName = "AREANAME";
            areaCondition.SetPopupLayout("RECEIPTAREA", PopupButtonStyles.Ok_Cancel, true, true);
            areaCondition.SetPopupResultCount(1);
            areaCondition.SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow);
            areaCondition.SetPopupAutoFillColumns("AREANAME");
            areaCondition.SetPopupResultMapping("RECEIPTAREA", "AREANAME");
            areaCondition.SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                //작업장 변경 시 작업자 조회
                selectedRows.Cast<DataRow>().ForEach(row =>
                {
                    _receiptAreaID = row.GetString("AREAID");
                    popEditReceiptArea.EditValue = row.GetString("AREANAME");
                    _isModify = row.GetString("ISMODIFY");

                    if(_isModify.Equals("Y"))
                    {
                        if (pnlToolbar.Controls["layoutToolbar"].Controls["Request"] != null)
                            pnlToolbar.Controls["layoutToolbar"].Controls["Request"].Enabled = true;
                        if (pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"] != null)
                            pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"].Enabled = true;
                        if (pnlToolbar.Controls["layoutToolbar"].Controls["Delete"] != null)
                            pnlToolbar.Controls["layoutToolbar"].Controls["Delete"].Enabled = true;
                    }
                    else
                    {
                        if (pnlToolbar.Controls["layoutToolbar"].Controls["Request"] != null)
                            pnlToolbar.Controls["layoutToolbar"].Controls["Request"].Enabled = false;
                        if (pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"] != null)
                            pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"].Enabled = false;
                        if (pnlToolbar.Controls["layoutToolbar"].Controls["Delete"] != null)
                            pnlToolbar.Controls["layoutToolbar"].Controls["Delete"].Enabled = false;
                    }
                });

            });


            areaCondition.Conditions.AddTextBox("AREANAME");

            areaCondition.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetIsHidden();
            areaCondition.GridColumns.AddTextBoxColumn("AREANAME", 200);

            popEditReceiptArea.SelectPopupCondition = areaCondition;
        }
        #endregion

        #region InitializeIsCoatingCombo  : 코팅유무 콤보박스 초기화
        /// <summary>
        /// 코팅유무 콤보박스 초기화
        /// </summary>        
        private void InitializeIsCoatingCombo()
        {     
            cboIsCoating.DataSource = SqlExecuter.Query("GetCodeList", "00001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "YesNo" } });
            cboIsCoating.ValueMember = "CODEID";
            cboIsCoating.DisplayMember = "CODENAME";
            cboIsCoating.UseEmptyItem = true;
            cboIsCoating.EmptyItemValue = "";
            cboIsCoating.EmptyItemCaption = "";
            cboIsCoating.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboIsCoating.Columns["CODEID"].Visible = false;
            cboIsCoating.ShowHeader = false;
        }
        #endregion

        #region InitializePriorityCombo  : 우선순위 콤보박스 초기화
        /// <summary>
        /// 코팅유무 콤보박스 초기화
        /// </summary>        
        private void InitializePriorityCombo()
        {   
            cboPriority.DataSource = SqlExecuter.Query("GetCodeList", "00001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "FilmPriorityCode" } });
            cboPriority.ValueMember = "CODEID";
            cboPriority.DisplayMember = "CODENAME";
            cboPriority.UseEmptyItem = true;
            cboPriority.EmptyItemValue = "";
            cboPriority.EmptyItemCaption = "";
            cboPriority.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboPriority.Columns["CODEID"].Visible = false;
            cboPriority.ShowHeader = false;
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

            // TODO : 저장 Rule 변경            
        }

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Request"))
                BtnModify_Click(sender, e);
            else if (btn.Name.ToString().Equals("RequestAgain"))
                BtnReRequest_Click(sender, e);
            else if (btn.Name.ToString().Equals("Delete"))
                BtnErase_Click(sender, e);            
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
            values.Add("USERID", UserInfo.Current.Id);
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);
            #endregion

            if (Conditions.GetValue("AREANAME").ToString() != "")
                values.Add("AREAID", _searchAreaID);

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolReqSearchResult = SqlExecuter.Query("GetRequestMakingFilmListByTool", "10001", values);

            grdFilmRequest.DataSource = toolReqSearchResult;

            if (toolReqSearchResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            else
            {
                grdFilmRequest.View.FocusedRowHandle = 0;
                DisplayFilmRequestInfo();
            }
        }
        #endregion

        #region Research - 재검색(데이터 입력후와 같은 경우에 사용)
        /// <summary>
        /// 저장 삭제후 재 검색시 사용
        /// 생성 및 수정시 발생한 아이디를 통해 자동 선택되도록 유도하며 삭제시에는 null로 매개변수를 받아서 첫번째 행을 보여준다. 행이 없다면 빈 값으로 설정
        /// </summary>
        /// <param name="inboudNo">입력 및 수정시 발생한 값의 아이디.</param>
        private void Research(string filmSequence)
        {
            // TODO : 조회 SP 변경
            InitializeInsertForm();
            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("USERID", UserInfo.Current.Id);
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);
            #endregion

            if (Conditions.GetValue("AREANAME").ToString() != "")
                values.Add("AREAID", _searchAreaID);

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolReqSearchResult = SqlExecuter.Query("GetRequestMakingFilmListByTool", "10001", values);

            grdFilmRequest.DataSource = toolReqSearchResult;

            //현재 작업한 결과를 선택할 수 있도록 한다.
            if (filmSequence == null) //삭제 작업후
            {
                //값이 하나도 없다면 빈 화면
                if (grdFilmRequest.View.RowCount == 0)
                {
                    //입력대기 화면으로 설정
                    InitializeInsertForm();
                }
                else
                {
                    //첫번재 값으로 설정
                    grdFilmRequest.View.FocusedRowHandle = 0;
                    DisplayFilmRequestInfo();
                }
            }
            else //값이 있다면 해당 값에 맞는 행을 찾아서 선택
            {
                ViewSavedData(filmSequence);
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
            //InitializeConditionPlant();
            InitializeConditionFilmProgressStatus();
            InitializeConditionToolCodePopup();
            InitializeReceiptArea();
            InitializeConditionJobType();
            InitializeConditionProductionType();
            //InitializeFilmCode();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #region InitializeConditionPlant : 사이트 검색조건
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
            ;
        }
        #endregion

        #region InitializeConditionFilmProgressStatus : 진행상태 조회조건
        /// <summary>
        /// 진행상태 조회조건
        /// </summary>
        private void InitializeConditionFilmProgressStatus()
        {
            var planttxtbox = Conditions.AddComboBox("FILMPROGRESSSTATUS", new SqlQuery("GetFilmProgressStatusCodeListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"FILMSCREENSTATUS=MakeFilm"), "CODENAME", "CODEID")
               .SetLabel("FILMPROGRESSSTATUS")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(3.4)
               .SetEmptyItem("", "", true)
            ;
        }
        #endregion

        #region InitializeConditionJobType : 작업구분 조회조건
        /// <summary>
        /// 진행상태 조회조건
        /// </summary>
        private void InitializeConditionJobType()
        {
            var planttxtbox = Conditions.AddComboBox("JOBTYPEID", new SqlQuery("GetCodeListByToolWithLanguage", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=JobType"), "CODENAME", "CODEID")
               .SetLabel("JOBTYPE")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(3.1)
               .SetEmptyItem("", "", true)
            ;
        }
        #endregion

        #region InitializeConditionProductionType : 생산구분 조회조건
        /// <summary>
        /// 진행상태 조회조건
        /// </summary>
        private void InitializeConditionProductionType()
        {
            var planttxtbox = Conditions.AddComboBox("PRODUCTIONTYPEID", new SqlQuery("GetCodeListByToolWithLanguage", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=ProductionType"), "CODENAME", "CODEID")
               .SetLabel("PRODUCTIONTYPE")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(3.2)
               .SetEmptyItem("", "", true)
            ;
        }
        #endregion

        #region InitializeConditionToolCodePopup : 품목코드 검색조건
        /// <summary>
        /// 품목코드 조회 설정
        /// </summary>
        private void InitializeConditionToolCodePopup()
        {
            ConditionItemSelectPopup productPopup = Conditions.AddSelectPopup("PRODUCTDEFID", new SqlQuery("GetProductdefidPoplistByTool", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", "PRODUCTDEFTYPE=Product"))
            .SetPopupLayout("PRODUCTDEFIDPOPUP", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow)
            .SetPopupResultMapping("PRODUCTDEFID", "PRODUCTDEFID")
            .SetLabel("PRODUCTDEFID")
            .SetPopupResultCount(1)
            .SetPosition(1.1)
            .SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                //품목코드 변경시 조회조건에 데이터 변경
                selectedRows.Cast<DataRow>().ForEach(row =>
                {
                    Conditions.SetValue("p_filmVersion", 0, row.GetString("PRODUCTDEFVERSION"));
                    Conditions.SetValue("p_productDefName", 0, row.GetString("PRODUCTDEFNAME"));
                });

            });

            productPopup.ValueFieldName = "PRODUCTDEFID";
            productPopup.DisplayFieldName = "PRODUCTDEFNAME";

            // 팝업 조회조건
            productPopup.Conditions.AddTextBox("PRODUCTDEF")
                .SetLabel("PRODUCTDEF");
            //toolCodePopup.Conditions.AddComboBox("PRODUCTDEFTYPE", new SqlQuery("GetCodeListByOspProductdeftype", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } }), "CODENAME", "CODEID")
            //    .SetEmptyItem("", "", true)
            //    .SetLabel("PRODUCTDEFTYPE")
            //    .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly                
            //    ;PRODUCTDEFVERSION

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

        #region InitializeReceiptArea : 작업장 검색조건
        /// <summary>
        /// 작업장 검색조건
        /// </summary>
        private void InitializeReceiptArea()
        {
            receiptAreaPopup = Conditions.AddSelectPopup("AREANAME", new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CURRENTLOGINID={UserInfo.Current.Id}"))
            .SetPopupLayout("RECEIPTAREA", PopupButtonStyles.Ok_Cancel, true, true)            
            .SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow)
            .SetPopupAutoFillColumns("AREANAME")
            .SetLabel("RECEIPTAREA")
            .SetPopupResultCount(1)
            .SetPosition(3.3)
            .SetPopupResultMapping("AREANAME", "AREANAME")
            .SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                //작업장 변경 시 작업자 조회
                selectedRows.Cast<DataRow>().ForEach(row =>
                {
                    _searchAreaID = row.GetString("AREAID");
                });

            });


            receiptAreaPopup.Conditions.AddTextBox("AREANAME");

            receiptAreaPopup.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetIsHidden();
            receiptAreaPopup.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }
        #endregion

        #region InitializeFilmCode : 필름코드 검색조건
        /// <summary>
        /// 작업장 검색조건
        /// </summary>
        private void InitializeFilmCode()
        {
            filmCodeCondition = Conditions.AddSelectPopup("FILMCODE", new SqlQuery("GetFilmCodePopupListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CURRENTLOGINID={UserInfo.Current.Id}"))
            .SetPopupLayout("FILMCODE", PopupButtonStyles.Ok_Cancel, true, true)
            .SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow)
            //.SetPopupAutoFillColumns("FILMCODE")
            .SetLabel("FILMCODE")
            .SetPopupResultCount(1)
            .SetPosition(1.2)
            .SetPopupResultMapping("FILMCODE", "FILMCODE")
            ;

            filmCodeCondition.Conditions.AddTextBox("PRODUCTDEFNAME");
            filmCodeCondition.Conditions.AddTextBox("PRODUCTDEFID");
            segmentCondition = filmCodeCondition.Conditions.AddComboBox("PROCESSSEGMENTVERSION", new SqlQuery("GetProcessSegmentByTool", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "PLANTID", UserInfo.Current.Plant } }), "PROCESSSEGEMENTNAME", "PROCESSSEGEMENTID");
            segmentCondition.SetEmptyItem("", "", true);
            segmentCondition.SetLabel("PROCESSSEGMENTVERSION");
            segmentCondition.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;

            filmCodeCondition.GridColumns.AddTextBoxColumn("FILMCODE", 150).SetIsReadOnly();
            filmCodeCondition.GridColumns.AddTextBoxColumn("FILMVERSION", 80).SetIsReadOnly();
            filmCodeCondition.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150).SetIsReadOnly();
            filmCodeCondition.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsHidden();
            filmCodeCondition.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 300).SetIsReadOnly();
            filmCodeCondition.GridColumns.AddTextBoxColumn("FILMCATEGORYID", 10).SetIsHidden();
            filmCodeCondition.GridColumns.AddTextBoxColumn("FILMCATEGORY", 100).SetIsReadOnly();
            filmCodeCondition.GridColumns.AddTextBoxColumn("FILMDETAILCATEGORYID", 10).SetIsHidden();
            filmCodeCondition.GridColumns.AddTextBoxColumn("FILMDETAILCATEGORY", 100).SetIsReadOnly();
            filmCodeCondition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTVERSION", 100).SetIsReadOnly();
            filmCodeCondition.GridColumns.AddTextBoxColumn("FILMUSELAYER", 100).SetIsHidden();
            filmCodeCondition.GridColumns.AddTextBoxColumn("JOBTYPEID", 100).SetIsHidden();
            filmCodeCondition.GridColumns.AddTextBoxColumn("JOBTYPE", 100).SetIsHidden();
            filmCodeCondition.GridColumns.AddTextBoxColumn("CUSTOMERID", 100).SetIsHidden();
            filmCodeCondition.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 100).SetIsHidden();            
        }
        #endregion
        #endregion

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가       
            btnInitialize.Click += BtnInitialize_Click;
            btnModify.Click += BtnModify_Click;
            btnErase.Click += BtnErase_Click;
            btnReRequest.Click += BtnReRequest_Click;

            txtShrinkageRate.TextChanged += TxtShrinkageRate_TextChanged;
            txtShrinkageRateY.TextChanged += TxtShrinkageRateY_TextChanged;
            grdFilmRequest.View.FocusedRowChanged += grdFilmRequest_FocusedRowChanged;

            grdFilmRequest.View.RowCellStyle += grdFilmRequest_RowCellStyle;

            Shown += RequestMakeFilm_Shown;
        }

        #region ReceiptRepairTool_Shown - Site관련정보를 화면로딩후 설정한다.
        private void RequestMakeFilm_Shown(object sender, EventArgs e)
        {
            ((Micube.Framework.SmartControls.SmartSelectPopupEdit)Conditions.GetControl("PRODUCTDEFID")).ButtonClick += RequestMakeFilm_ButtonClick;
            ((Micube.Framework.SmartControls.SmartTextBox)Conditions.GetControl("p_filmVersion")).ReadOnly = true;
            ((Micube.Framework.SmartControls.SmartTextBox)Conditions.GetControl("p_productDefName")).ReadOnly = true;

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

        private void RequestMakeFilm_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)
            {
                Conditions.GetControl("p_filmVersion").Text = "";
                Conditions.GetControl("p_productDefName").Text = "";
            }
        }

        private void grdFilmRequest_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (grdFilmRequest.View.GetDataRow(e.RowHandle).GetString("FILMPROGRESSSTATUSID").Equals("CancelAccept"))
            {
                e.Appearance.BackColor = Color.Gold;
                e.Appearance.BackColor2 = Color.Gold;
                e.Appearance.ForeColor = Color.Black;
            }
            else if (grdFilmRequest.View.GetDataRow(e.RowHandle).GetString("FILMPROGRESSSTATUSID").Equals("RequestAgain"))
            {
                e.Appearance.BackColor = Color.LimeGreen;
                e.Appearance.BackColor2 = Color.LimeGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void BtnReRequest_Click(object sender, EventArgs e)
        {
            ReSaveData();
        }

        private void TxtShrinkageRateY_TextChanged(object sender, EventArgs e)
        {
            txtShrinkageRatePercentageY.EditValue = GetContractionPercentage(txtShrinkageRateY.Text);
        }

        private void TxtShrinkageRate_TextChanged(object sender, EventArgs e)
        {
            txtShrinkageRatePercentage.EditValue = GetContractionPercentage(txtShrinkageRate.Text);
        }

        private void grdFilmRequest_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayFilmRequestInfo();          
        }

        private void BtnErase_Click(object sender, EventArgs e)
        {
            DeleteData();
        }

        private void BtnModify_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void BtnInitialize_Click(object sender, EventArgs e)
        {
            InitializeInsertForm();
        }
        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }

        #region ValidateCurrentStatus : 현재 저장요청한 정보가 저장 가능한 정보인지 검증
        private bool ValidateCurrentStatus(string wishStatus, out string messageCode)
        {
            messageCode = "";
            if (wishStatus.Equals("Request"))
            {
                //입력상태라면 저장가능
                if (!_currentStatus.Equals("added"))
                {
                    messageCode = "ValidateFilmInitialStatusForRequest";
                    return false;
                }

                if (grdFilmRequest.View.GetFocusedDataRow() != null)
                {
                    //현재 선택된 항목이 Request상태라면 수정가능
                    if (!grdFilmRequest.View.GetFocusedDataRow().GetString("FILMPROGRESSSTATUSID").Equals("Request"))
                    {
                        messageCode = "ValidateFilmInitialStatusForRequest";
                        return false;
                    }
                }
            }
            else
            {
                if (grdFilmRequest.View.GetFocusedDataRow() != null)
                {
                    //현재 선택된 항목이 CancelAccept라면 재요청가능
                    if (!grdFilmRequest.View.GetFocusedDataRow().GetString("FILMPROGRESSSTATUSID").Equals("CancelAccept"))
                    {
                        messageCode = "ValidateFilmReActionStatusForRequest";
                        return false;
                    }
                }
                else
                {
                    messageCode = "ValidateFilmReActionStatusForRequest";
                    return false;
                }
            }
            
            return true;
        }
        #endregion

        /// <summary>
        /// 입력/수정 시의 Validation 체크
        /// 입력/수정 각각의 체크값이 서로 다르다면 상태에 따른 분기가 있어야 한다.
        /// </summary>
        /// <returns></returns>
        private bool ValidateContent()
        {
            if (!ValidateEditValue(deRequestDate.EditValue))
                return false;

            if (!ValidateEditValue(txtRequestUser.EditValue))
                return false;

            if (!ValidateEditValue(txtRequestDepartment.EditValue))
                return false;

            if (ucFilmCode.FilmCode == null && ucFilmCode.FilmCode == "")
                return false;

            if (ucFilmCode.FilmVersion == null && ucFilmCode.FilmVersion == "")
                return false;

            if (!ValidateEditValue(txtProductDefID.EditValue))
                return false;

            if (!ValidateEditValue(popEditReceiptArea.EditValue))
                return false;

            if (!ValidateNumericBox(txtRequestQty, 0))
                return false;

            return true;
        }

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

        private void SetRequiredValidationControl(Control requiredControl)
        {
            requiredControl.ForeColor = Color.Red;
        }

        #region ValidateProductCode : 품목코드와 관계된 치공구를 부르기 위해 품목코드가 입력되었는지 확인한다.
        private bool ValidateProductCode()
        {
            if (ucFilmCode.FilmCode.Equals(""))
                return false;

            return true;
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
                _filmSequence = null;            //RequestSequence는 공값이어야 한다.
                _currentStatus = "added";           //현재 화면의 상태는 입력중이어야 한다.

                //의뢰일은 오늘날짜를 기본으로 한다.
                DateTime dateNow = DateTime.Now;
                deRequestDate.EditValue = dateNow;

                //의뢰순번은 공값으로 한다.
                txtRequestUser.EditValue = UserInfo.Current.Name;
                _requestUserID = UserInfo.Current.Id;

                //의뢰부서는 사용자의 부서를 기본으로 한다.
                txtRequestDepartment.EditValue = UserInfo.Current.Department;

                //제작구분은 선택없이 한다.                
                txtProductDefID.EditValue = "";

                txtProductDefName.EditValue = "";

                //품목코드와 버전역시 비운다.
                ucFilmCode.FilmCode = "";
                ucFilmCode.FilmVersion = "";

                txtFilmCategory.EditValue = "";
                txtFilmType.EditValue = "";
                txtFilmDetailCategory.EditValue = "";
                txtLayer.EditValue = "";
                txtJobType.EditValue = "";
                txtUseProcess.EditValue = "";

                txtResolution.EditValue = "";
                txtShrinkageRate.EditValue = "";
                txtShrinkageRateY.EditValue = "";

                txtShrinkageRatePercentage.EditValue = "";
                txtShrinkageRatePercentageY.EditValue = "";

                cboIsCoating.EditValue = "";

                popEditMakeVendor.EditValue = "";
                _makeVendorID = "";

                popEditReceiptArea.EditValue = "";
                _receiptAreaID = "";

                cboPriority.EditValue = "";
                deUsePlanDate.EditValue = dateNow;

                txtRequestQty.EditValue = "0";
                txtRequestComment.EditValue = "";

                //컨트롤 접근여부는 작성상태로 변경한다.
                ControlEnableProcess("added");

                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Request"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Request"].Visible = true;
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"].Visible = false;
                }
                //btnReRequest.Visible = false;
                //btnModify.Visible = true;
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
                deRequestDate.ReadOnly = true;
                txtRequestUser.ReadOnly = true;
                txtRequestDepartment.ReadOnly = true;
                txtProductDefID.ReadOnly = true;
                txtProductDefName.ReadOnly = true;
                ucFilmCode.ReadOnly = false;
                txtFilmCategory.ReadOnly = true;
                txtFilmType.ReadOnly = true;
                txtFilmDetailCategory.ReadOnly = true;
                txtLayer.ReadOnly = true;
                txtJobType.ReadOnly = true;
                txtUseProcess.ReadOnly = true;

                txtResolution.ReadOnly = false;
                txtShrinkageRate.ReadOnly = false;
                txtShrinkageRateY.ReadOnly = false;

                txtShrinkageRatePercentage.ReadOnly = true;
                txtShrinkageRatePercentageY.ReadOnly = true;

                cboIsCoating.ReadOnly = false;

                popEditMakeVendor.ReadOnly = false;
                popEditReceiptArea.ReadOnly = false;
                cboPriority.ReadOnly = false;
                deUsePlanDate.ReadOnly = false;
                txtRequestQty.ReadOnly = false;
                txtRequestComment.ReadOnly = false;
            }
            else if (currentStatus == "modified") //
            {
                deRequestDate.ReadOnly = true;
                txtRequestUser.ReadOnly = true;
                txtRequestDepartment.ReadOnly = true;
                txtProductDefID.ReadOnly = true;
                txtProductDefName.ReadOnly = true;
                ucFilmCode.ReadOnly = false;
                txtFilmCategory.ReadOnly = true;
                txtFilmType.ReadOnly = true;
                txtFilmDetailCategory.ReadOnly = true;
                txtLayer.ReadOnly = true;
                txtJobType.ReadOnly = true;
                txtUseProcess.ReadOnly = true;

                txtResolution.ReadOnly = false;
                txtShrinkageRate.ReadOnly = false;
                txtShrinkageRateY.ReadOnly = false;

                txtShrinkageRatePercentage.ReadOnly = true;
                txtShrinkageRatePercentageY.ReadOnly = true;

                cboIsCoating.ReadOnly = false;

                popEditMakeVendor.ReadOnly = false;
                popEditReceiptArea.ReadOnly = false;
                cboPriority.ReadOnly = false;
                deUsePlanDate.ReadOnly = false;
                txtRequestQty.ReadOnly = false;
                txtRequestComment.ReadOnly = false;
            }
            else
            {
                deRequestDate.ReadOnly = true;
                txtRequestUser.ReadOnly = true;
                txtRequestDepartment.ReadOnly = true;
                txtProductDefID.ReadOnly = true;
                txtProductDefName.ReadOnly = true;
                ucFilmCode.ReadOnly = true;
                txtFilmCategory.ReadOnly = true;
                txtFilmType.ReadOnly = true;
                txtFilmDetailCategory.ReadOnly = true;
                txtLayer.ReadOnly = true;
                txtJobType.ReadOnly = true;
                txtUseProcess.ReadOnly = true;

                txtResolution.ReadOnly = true;
                txtShrinkageRate.ReadOnly = true;
                txtShrinkageRateY.ReadOnly = true;

                txtShrinkageRatePercentage.ReadOnly = true;
                txtShrinkageRatePercentageY.ReadOnly = true;

                cboIsCoating.ReadOnly = true;

                popEditMakeVendor.ReadOnly = true;
                popEditReceiptArea.ReadOnly = true;
                cboPriority.ReadOnly = true;
                deUsePlanDate.ReadOnly = true;
                txtRequestQty.ReadOnly = true;
                txtRequestComment.ReadOnly = true;
            }
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
            dt.TableName = "filmRequestMakingList";
            //===================================================================================
            dt.Columns.Add("FILMSEQUENCE");
            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");
            dt.Columns.Add("REQUESTDATE");
            dt.Columns.Add("REQUESTDEPARTMENT");
            dt.Columns.Add("REQUESTUSERID");
            dt.Columns.Add("FILMCODE");
            dt.Columns.Add("FILMVERSION");
            dt.Columns.Add("CONTRACTIONX");
            dt.Columns.Add("CONTRACTIONY");
            dt.Columns.Add("RESOLUTION");
            dt.Columns.Add("ISCOATING");
            dt.Columns.Add("QTY");
            dt.Columns.Add("PRIORITYID");
            dt.Columns.Add("USEPLANDATE");
            dt.Columns.Add("RECEIPTAREAID");
            dt.Columns.Add("MAKEVENDORID");
            dt.Columns.Add("REQUESTCOMMENT");
            dt.Columns.Add("FILMPROGRESSSTATUS");

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
                btnInitialize.Enabled = false;
                btnModify.Enabled = false;
                btnErase.Enabled = false;

                string messageCode = "";
                //신규 혹은 요청 상태만 변경가능하다.
                if (ValidateCurrentStatus("Request", out messageCode))
                {
                    //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                    if (ValidateContent())
                    {
                        DataSet filmReqSet = new DataSet();
                        //치공구 제작의뢰를 입력
                        DataTable filmReqTable = CreateSaveDatatable();
                        DataRow filmReqRow = filmReqTable.NewRow();

                        DateTime requestDate = Convert.ToDateTime(deRequestDate.EditValue.ToString());
                        DateTime usePlanDate = Convert.ToDateTime(deUsePlanDate.EditValue.ToString());

                        filmReqRow["REQUESTDATE"] = requestDate.ToString("yyyy-MM-dd HH:mm:ss");

                        filmReqRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                        filmReqRow["PLANTID"] = Conditions.GetValue("P_PLANTID");

                        filmReqRow["REQUESTDEPARTMENT"] = txtRequestDepartment.EditValue;
                        filmReqRow["REQUESTUSERID"] = _requestUserID;
                        filmReqRow["FILMCODE"] = ucFilmCode.FilmCode;
                        filmReqRow["FILMVERSION"] = ucFilmCode.FilmVersion;

                        filmReqRow["CONTRACTIONX"] = txtShrinkageRate.EditValue;
                        filmReqRow["CONTRACTIONY"] = txtShrinkageRateY.EditValue;

                        filmReqRow["RESOLUTION"] = txtResolution.EditValue;
                        filmReqRow["ISCOATING"] = cboIsCoating.EditValue;
                        filmReqRow["QTY"] = txtRequestQty.EditValue;
                        filmReqRow["PRIORITYID"] = cboPriority.EditValue;
                        filmReqRow["USEPLANDATE"] = usePlanDate.ToString("yyyy-MM-dd");
                        filmReqRow["RECEIPTAREAID"] = _receiptAreaID;
                        filmReqRow["MAKEVENDORID"] = _makeVendorID;
                        filmReqRow["REQUESTCOMMENT"] = txtRequestComment.EditValue;

                        filmReqRow["VALIDSTATE"] = "Valid";

                        //현재 화면의 상태에 따라 생성/수정으로 분기된다.
                        if (_currentStatus == "added")
                        {
                            filmReqRow["FILMPROGRESSSTATUS"] = "Request";
                            filmReqRow["CREATOR"] = UserInfo.Current.Id;
                            filmReqRow["_STATE_"] = "added";
                        }
                        else
                        {
                            //수정할 PK를 전달
                            filmReqRow["FILMSEQUENCE"] = _filmSequence;
                            filmReqRow["MODIFIER"] = UserInfo.Current.Id;
                            filmReqRow["_STATE_"] = "modified";
                        }

                        filmReqTable.Rows.Add(filmReqRow);

                        filmReqSet.Tables.Add(filmReqTable);

                        DataTable resultTable = this.ExecuteRule<DataTable>("FilmRequestMaking", filmReqSet);

                        ////Server에서 결과값을 현재는 반환하지 않음 만약 반환해야 한다면 아래로직 구현
                        DataRow resultRow = resultTable.Rows[0];

                        ControlEnableProcess("modified");

                        ////가져온 결과값에 따라 재검색한 후 가져온 아이디에 맞는 행을 선택으로 변경한다.
                        Research(resultRow["SEQUENCE"].ToString());
                    }
                    else
                    {
                        ShowMessage(MessageBoxButtons.OK, "VALIDATEREQUIREDVALUES", "");
                    }
                }
                else
                {
                    ShowMessage(MessageBoxButtons.OK, messageCode, "");
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

        #region ReSaveData : 재접수를 수행
        private void ReSaveData()
        {
            //Validation 체크 부분 작성 필요
            this.ShowWaitArea();
            //저장 로직
            try
            {
                btnInitialize.Enabled = false;
                btnModify.Enabled = false;
                btnErase.Enabled = false;

                if (grdFilmRequest.View.GetFocusedDataRow() != null)
                {
                    string messageCode = "";
                    //신규 혹은 요청 상태만 변경가능하다.
                    if (ValidateCurrentStatus("RequestAgain", out messageCode))
                    {
                        //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                        if (ValidateContent())
                        {
                            DataSet filmReqSet = new DataSet();
                            //치공구 제작의뢰를 입력
                            DataTable filmReqTable = CreateSaveDatatable();
                            DataRow filmReqRow = filmReqTable.NewRow();

                            DateTime requestDate = Convert.ToDateTime(deRequestDate.EditValue.ToString());
                            DateTime usePlanDate = Convert.ToDateTime(deUsePlanDate.EditValue.ToString());

                            filmReqRow["REQUESTDATE"] = requestDate.ToString("yyyy-MM-dd HH:mm:ss");

                            filmReqRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                            filmReqRow["PLANTID"] = Conditions.GetValue("P_PLANTID");

                            filmReqRow["REQUESTDEPARTMENT"] = txtRequestDepartment.EditValue;
                            filmReqRow["REQUESTUSERID"] = _requestUserID;
                            filmReqRow["FILMCODE"] = ucFilmCode.FilmCode;
                            filmReqRow["FILMVERSION"] = ucFilmCode.FilmVersion;

                            filmReqRow["CONTRACTIONX"] = txtShrinkageRate.EditValue;
                            filmReqRow["CONTRACTIONY"] = txtShrinkageRateY.EditValue;

                            filmReqRow["RESOLUTION"] = txtResolution.EditValue;
                            filmReqRow["ISCOATING"] = cboIsCoating.EditValue;
                            filmReqRow["QTY"] = txtRequestQty.EditValue;
                            filmReqRow["PRIORITYID"] = cboPriority.EditValue;
                            filmReqRow["USEPLANDATE"] = usePlanDate.ToString("yyyy-MM-dd");
                            filmReqRow["RECEIPTAREAID"] = _receiptAreaID;
                            filmReqRow["MAKEVENDORID"] = _makeVendorID;
                            filmReqRow["REQUESTCOMMENT"] = txtRequestComment.EditValue;

                            filmReqRow["VALIDSTATE"] = "Valid";

                            //현재 화면의 상태에 따라 생성/수정으로 분기된다.
                            if (_currentStatus == "added")
                            {
                                filmReqRow["FILMPROGRESSSTATUS"] = "RequestAgain";
                                filmReqRow["CREATOR"] = UserInfo.Current.Id;
                                filmReqRow["_STATE_"] = "added";
                            }
                            else
                            {
                                //수정할 PK를 전달
                                filmReqRow["FILMSEQUENCE"] = _filmSequence;
                                filmReqRow["MODIFIER"] = UserInfo.Current.Id;
                                filmReqRow["_STATE_"] = "modified";
                            }

                            filmReqTable.Rows.Add(filmReqRow);

                            filmReqSet.Tables.Add(filmReqTable);

                            DataTable resultTable = this.ExecuteRule<DataTable>("FilmRequestMaking", filmReqSet);

                            ////Server에서 결과값을 현재는 반환하지 않음 만약 반환해야 한다면 아래로직 구현
                            DataRow resultRow = resultTable.Rows[0];

                            ControlEnableProcess("modified");

                            ////가져온 결과값에 따라 재검색한 후 가져온 아이디에 맞는 행을 선택으로 변경한다.
                            Research(resultRow["SEQUENCE"].ToString());
                        }
                        else
                        {
                            ShowMessage(MessageBoxButtons.OK, "VALIDATEREQUIREDVALUES", "");
                        }
                    }
                    else
                    {
                        ShowMessage(MessageBoxButtons.OK, messageCode, "");
                    }
                }
                else
                {
                    //ShowMessage(MessageBoxButtons.OK, String.Format("SelectItem", Language.GetDictionary("DURABLE")), "");
                    ShowMessage(MessageBoxButtons.OK, "SelectItem", Language.Get("FILM"));
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

                if (grdFilmRequest.View.GetFocusedDataRow() != null)
                {
                    DialogResult result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnErase.Text);//삭제하시겠습니까? 

                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        DataSet filmReqSet = new DataSet();
                        //치공구 제작의뢰를 입력
                        DataTable filmReqTable = CreateSaveDatatable();
                        DataRow filmReqRow = filmReqTable.NewRow();

                        filmReqRow["FILMSEQUENCE"] = _filmSequence;
                        filmReqRow["MODIFIER"] = UserInfo.Current.Id;
                        filmReqRow["VALIDSTATE"] = "Invalid";
                        filmReqRow["_STATE_"] = "deleted";

                        filmReqTable.Rows.Add(filmReqRow);

                        filmReqSet.Tables.Add(filmReqTable);

                        DataTable resultTable = this.ExecuteRule<DataTable>("FilmRequestMaking", filmReqSet);

                        ////Server에서 결과값을 현재는 반환하지 않음 만약 반환해야 한다면 아래로직 구현
                        DataRow resultRow = resultTable.Rows[0];

                        ControlEnableProcess("modified");

                        ////가져온 결과값에 따라 재검색한 후 가져온 아이디에 맞는 행을 선택으로 변경한다.
                        Research(null);
                    }
                }
                else
                { 
                    //ShowMessage(MessageBoxButtons.OK, String.Format("SelectItem", Language.GetDictionary("DURABLE")), "");
                    ShowMessage(MessageBoxButtons.OK, "SelectItem", Language.Get("FILM"));                    
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

        #region DisplayFilmRequestInfo : 그리드에서 행 선택시 상세정보를 표시
        /// <summary>
        /// 그리드에서 행 선택시 상세정보를 표시하는 메소드
        /// </summary>
        private void DisplayFilmRequestInfo()
        {
            //포커스 행 체크 
            if (grdFilmRequest.View.FocusedRowHandle < 0) return;

            DisplayFilmRequestInfoDetail(grdFilmRequest.View.GetFocusedDataRow());
        }
        #endregion

        #region ViewSavedData : 새로 입력된 데이터를 화면에 바인딩
        /// <summary>
        /// 새로 입력된 데이터를 화면에 바인딩
        /// </summary>
        private void ViewSavedData(string filmSequence)
        {
            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("FILMSEQUENCE", Int32.Parse(filmSequence));
            #endregion

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable savedResult = SqlExecuter.Query("GetRequestMakingFilmListByTool", "10001", values);

            if (savedResult.Rows.Count > 0)
            {
                grdFilmRequest.View.FocusedRowHandle = GetRowHandleInGrid(grdFilmRequest, "FILMSEQUENCE", filmSequence);

                DisplayFilmRequestInfoDetail(savedResult.Rows[0]);
            }
        }
        #endregion

        #region DisplayFilmRequestInfoDetail : 입력받은 인덱스의 그리드의 내용을 화면에 바인딩
        /// <summary>
        /// 입력받은 그리드내의 인덱스에 대한 내용을 화면에 표시한다.
        /// </summary>
        /// <param name="rowHandle"></param>
        private void DisplayFilmRequestInfoDetail(DataRow filmInfo)
        {
            //해당 업무에 맞는 Enable체크 수행
            deRequestDate.EditValue = filmInfo["REQUESTDATE"];
            txtRequestUser.EditValue = filmInfo.GetString("REQUESTUSER");
            _requestUserID = filmInfo.GetString("REQUESTUSERID");
            txtRequestDepartment.EditValue = filmInfo.GetString("REQUESTDEPARTMENT");
            ucFilmCode.FilmCode = filmInfo.GetString("FILMCODE");
            ucFilmCode.FilmVersion = filmInfo.GetString("FILMVERSION");
            txtProductDefID.EditValue = filmInfo.GetString("PRODUCTDEFID");
            txtProductDefName.EditValue = filmInfo.GetString("PRODUCTDEFNAME");
            txtFilmType.EditValue = filmInfo.GetString("FILMTYPE");
            txtFilmCategory.EditValue = filmInfo.GetString("FILMCATEGORY");
            txtFilmDetailCategory.EditValue = filmInfo.GetString("FILMDETAILCATEGORY");
            txtJobType.EditValue = filmInfo.GetString("JOBTYPE");
            txtLayer.EditValue = filmInfo.GetString("FILMUSERLAYER");
            txtUseProcess.EditValue = filmInfo.GetString("USEPROCESSSEGMENT");
            txtResolution.EditValue = filmInfo.GetString("RESOLUTION");
            txtShrinkageRate.EditValue = filmInfo.GetString("CONTRACTIONX");
            txtShrinkageRateY.EditValue = filmInfo.GetString("CONTRACTIONY");
            cboIsCoating.EditValue = filmInfo["ISCOATING"];
            txtShrinkageRatePercentage.EditValue = filmInfo.GetString("CHANGECONTRACTIONX");
            txtShrinkageRatePercentageY.EditValue = filmInfo.GetString("CHANGECONTRACTIONY");
            popEditMakeVendor.EditValue = filmInfo.GetString("MAKEVENDOR");
            _makeVendorID = filmInfo.GetString("MAKEVENDORID");
            cboPriority.EditValue = filmInfo["PRIORITYID"];
            deUsePlanDate.EditValue = filmInfo["USEPLANDATE"];
            popEditReceiptArea.EditValue = filmInfo.GetString("RECEIPTAREA");
            _receiptAreaID = filmInfo.GetString("RECEIPTAREAID");
            txtRequestQty.EditValue = filmInfo.GetString("QTY");
            txtRequestComment.EditValue = filmInfo.GetString("REQUESTCOMMENT");

            _filmSequence = filmInfo.GetString("FILMSEQUENCE");

            //버튼제어
            if(filmInfo.GetString("FILMPROGRESSSTATUSID").Equals("CancelAccept"))
            {
                //btnReRequest.Visible = true;
                //btnModify.Visible = false;
                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Request"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Request"].Visible = false;
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"].Visible = true;
                }
            }
            else
            {
                //btnReRequest.Visible = false;
                //btnModify.Visible = true;
                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Request"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Request"].Visible = true;
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"].Visible = false;
                }
            }

            //작업장에 관련된 버튼제어
            if (filmInfo.GetString("ISMODIFY").Equals("Y"))
            {
                if (pnlToolbar.Controls["layoutToolbar"].Controls["Request"] != null)
                    pnlToolbar.Controls["layoutToolbar"].Controls["Request"].Enabled = true;
                if (pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"] != null)
                    pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"].Enabled = true;
                if (pnlToolbar.Controls["layoutToolbar"].Controls["Delete"] != null)
                    pnlToolbar.Controls["layoutToolbar"].Controls["Delete"].Enabled = true;
            }
            else
            {
                if (pnlToolbar.Controls["layoutToolbar"].Controls["Request"] != null)
                    pnlToolbar.Controls["layoutToolbar"].Controls["Request"].Enabled = false;
                if (pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"] != null)
                    pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"].Enabled = false;
                if (pnlToolbar.Controls["layoutToolbar"].Controls["Delete"] != null)
                    pnlToolbar.Controls["layoutToolbar"].Controls["Delete"].Enabled = false;
            }

            //수정상태라 판단하여 화면 제어
            ControlEnableProcess("modified");

            _currentStatus = "modified";
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

        #region GetContractionPercentage : 요청수축률을 구한다.
        private string GetContractionPercentage(string ruleValue)
        {
            //수축율(%) 산출 수식 :: (1 - 수축율) *100, 소수점세자리   수축율은 소수다섯자리까지 입력
            double contractionValue = Convert.ToDouble(ruleValue);

            double resultValue = Math.Round((contractionValue - 1) * 100, 3);

            return Convert.ToString(resultValue);
        }
        #endregion

        #region ShowMessageInfo : 메세지를 출력한다.
        private void ShowMessageInfo(string messageID)
        {
            ShowMessage(MessageBoxButtons.OK, messageID, "");
        }
        #endregion

        #region ChangeSiteCondition - 검색조건의 Site변경시 관련된 쿼리들을 변경
        private void ChangeSiteCondition()
        {
            if (receiptAreaPopup != null)
                receiptAreaPopup.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"CURRENTLOGINID={UserInfo.Current.Id}");

            if (segmentCondition != null)
                segmentCondition.Query = new SqlQuery("GetProcessSegmentByTool", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "PLANTID", Conditions.GetValue("P_PLANTID") }, {"CURRENTLOGINID", UserInfo.Current.Id}  });

            if (makeVendorPopup != null)
                makeVendorPopup.SearchQuery = new SqlQuery("GetVendorListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            if (areaCondition != null)
                areaCondition.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"CURRENTLOGINID={UserInfo.Current.Id}", $"ISMOD=Y");

            if (filmCodeCondition != null)
                filmCodeCondition.SearchQuery = new SqlQuery("GetFilmCodePopupListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CURRENTLOGINID={UserInfo.Current.Id}");

            ucFilmCode.PlantID = Conditions.GetValue("P_PLANTID").ToString();

            //작업장 검색조건 초기화
            ((SmartSelectPopupEdit)Conditions.GetControl("AREANAME")).ClearValue();
        }
        #endregion

        #endregion
    }
}
