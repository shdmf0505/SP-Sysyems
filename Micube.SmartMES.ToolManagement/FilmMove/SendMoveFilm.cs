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
    /// 프 로 그 램 명  : 필름 이동출고
    /// 업  무  설  명  : 치공구관리 > 필름관리 > 필름 이동출고
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-11-20
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class SendMoveFilm : SmartConditionManualBaseForm
    {
        #region Local Variables
        private string _currentStatus = "browse";
        private string _sendUserID;

        private string _statusSendArea;
        private string _statusReceiptArea;

        private string _inputReceiptArea;
        private string _searchSendArea;

        private string _receiptSiteInput;
        //선택값에 따라 쿼리를 변경하기 위한 
        ConditionItemSelectPopup _popupGridToolArea;
        ConditionItemSelectPopup areaCondition;
        ConditionItemSelectPopup sendAreaCondition;
        ConditionItemSelectPopup receiptAreaCondition;
        string[] _clipDatas;
        int _clipIndex = 1;
        IDataObject _clipBoardData;
        bool _isGoodCopy = true;

        //이관용 Site
        DataTable _moveSite;
        #endregion

        #region 생성자

        public SendMoveFilm()
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
            InitializeInputGrid();
            InitRequiredControl();

            InitializeSiteComboBox();

            InitializeToolMoveTypeComboBox();
            InitializeIsReceiptComboBox();

            SetProductDefIDPopup();
            InitializeSendArea();
            InitializeReceiptArea();
            InitializeReceiptAreaInput();
            InitializeSendAreaSearch();

            InitializeInfo();
        }

        #region InitializeGrid - 치공구이동출고내역목록을 초기화한다.
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdSendMoveToolStatus.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기            
            grdSendMoveToolStatus.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdSendMoveToolStatus.View.AddTextBoxColumn("SENDDATE", 160)                 //출고일자
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly(true);
            grdSendMoveToolStatus.View.AddTextBoxColumn("TOOLMOVETYPEID")
                .SetIsHidden();                                                         //이동구분아이디
            grdSendMoveToolStatus.View.AddTextBoxColumn("TOOLMOVETYPE", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                   //이동구분
            grdSendMoveToolStatus.View.AddTextBoxColumn("FILMNO", 180)
                .SetIsReadOnly(true);                                                   //Tool 번호
            grdSendMoveToolStatus.View.AddTextBoxColumn("FILMNAME", 400)
                .SetIsReadOnly(true);                                                   //Tool 번호
            grdSendMoveToolStatus.View.AddTextBoxColumn("SENDPLANTID")
                .SetIsHidden();                                                         //출고SITE아이디
            grdSendMoveToolStatus.View.AddTextBoxColumn("SENDPLANT", 80)
                .SetIsReadOnly(true);                                                   //출고SITE
            grdSendMoveToolStatus.View.AddTextBoxColumn("SENDAREAID")
                .SetIsHidden();                                                         //출고작업장아이디
            grdSendMoveToolStatus.View.AddTextBoxColumn("SENDAREA", 150)
                .SetIsReadOnly(true);                                                   //출고작업장
            grdSendMoveToolStatus.View.AddTextBoxColumn("RECEIPTPLANTID")
                .SetIsHidden();                                                         //입고SITE아이디
            grdSendMoveToolStatus.View.AddTextBoxColumn("RECEIPTPLANT", 80)
                .SetIsReadOnly(true);                                                   //입고SITE
            grdSendMoveToolStatus.View.AddTextBoxColumn("RECEIPTAREAID")
                .SetIsHidden();                                                         //입고작업장아이디
            grdSendMoveToolStatus.View.AddTextBoxColumn("RECEIPTAREA", 150)
                .SetIsReadOnly(true);                                                   //입고작업장
            grdSendMoveToolStatus.View.AddTextBoxColumn("RECEIPTDATE", 160)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly(true);                                                   //의뢰일자
            grdSendMoveToolStatus.View.AddTextBoxColumn("JOBTYPEID")
                .SetIsHidden();                                                         //작업구분아이디
            grdSendMoveToolStatus.View.AddTextBoxColumn("JOBTYPE", 100)
                .SetIsReadOnly(true);                                                   //작업구분
            grdSendMoveToolStatus.View.AddTextBoxColumn("PRODUCTIONTYPEID")
                .SetIsHidden();                                                         //생산구분아이디
            grdSendMoveToolStatus.View.AddTextBoxColumn("PRODUCTIONTYPE", 100)
                .SetIsReadOnly(true);                                                   //생산구분
            grdSendMoveToolStatus.View.AddTextBoxColumn("FILMTYPEID")
                .SetIsHidden();                                                         //필름구분아이디
            grdSendMoveToolStatus.View.AddTextBoxColumn("FILMTYPE", 100)
                .SetIsReadOnly(true);                                                   //필름구분
            grdSendMoveToolStatus.View.AddTextBoxColumn("FILMCATEGORYID")
                .SetIsHidden();                                                         //필름유형아이디
            grdSendMoveToolStatus.View.AddTextBoxColumn("FILMCATEGORY", 120)
                .SetIsReadOnly(true);                                                   //필름유형
            grdSendMoveToolStatus.View.AddTextBoxColumn("FILMDETAILCATEGORYID")
                .SetIsHidden();                                                         //상세유형아이디
            grdSendMoveToolStatus.View.AddTextBoxColumn("FILMDETAILCATEGORY", 150)
                .SetIsReadOnly(true);                                                   //상세유형
            grdSendMoveToolStatus.View.AddTextBoxColumn("FILMUSELAYER1", 100)
                .SetIsReadOnly(true);                                                   //Layer
            grdSendMoveToolStatus.View.AddTextBoxColumn("FILMUSELAYER2", 120)
                .SetIsReadOnly(true);                                                   //Layer
            grdSendMoveToolStatus.View.AddTextBoxColumn("RESOLUTION", 80)
                .SetIsReadOnly(true);                                                   //해상도
            grdSendMoveToolStatus.View.AddTextBoxColumn("ISCOATING", 80)
                .SetIsReadOnly(true);                                                   //코팅유무
            grdSendMoveToolStatus.View.AddTextBoxColumn("RECEIPTUSERID")
                .SetIsHidden();                                                         //의뢰자이이디
            grdSendMoveToolStatus.View.AddTextBoxColumn("RECEIPTUSER", 100)
                .SetIsReadOnly(true);                                                   //의뢰자
            grdSendMoveToolStatus.View.AddTextBoxColumn("SENDUSERID")
                .SetIsHidden();                                                         //출고자아이디
            grdSendMoveToolStatus.View.AddTextBoxColumn("SENDUSER", 100)
                .SetIsReadOnly(true);                                                   //출고자
            grdSendMoveToolStatus.View.PopulateColumns();
        }
        #endregion

        #region InitializeInputGrid - 입력대상인 치공구목록을 초기화한다.
        /// <summary>        
        /// 입력대상인 치공구목록을 초기화한다.
        /// </summary>
        private void InitializeInputGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdToolMoveInput.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기            
            grdToolMoveInput.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdToolMoveInput.View.AddTextBoxColumn("FILMNO", 180)
                .SetIsReadOnly(true);                                               //Film 번호
            grdToolMoveInput.View.AddTextBoxColumn("FILMCODE", 150)
                .SetIsReadOnly(true);                                               //Film Code
            grdToolMoveInput.View.AddTextBoxColumn("FILMNAME", 400)
                .SetIsReadOnly(true);                                               //Film 명칭
            grdToolMoveInput.View.AddTextBoxColumn("FILMVERSION", 120)
                .SetIsReadOnly(true);                                               //Film Version
            grdToolMoveInput.View.AddTextBoxColumn("PRODUCTDEFID", 150)
               .SetIsReadOnly(true);                                               //품목코드
            grdToolMoveInput.View.AddTextBoxColumn("PRODUCTDEFNAME", 250)
                .SetIsReadOnly(true);                                               //품목명칭
            grdToolMoveInput.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly(true);                                               //품목번호
            grdToolMoveInput.View.AddTextBoxColumn("SENDPLANTID")
               .SetIsHidden();                                                      //출고사이트아이디
            grdToolMoveInput.View.AddTextBoxColumn("SENDPLANT", 80)
                .SetIsReadOnly(true);                                               //출고사이트
            grdToolMoveInput.View.AddTextBoxColumn("SENDAREAID")
                .SetIsHidden();                                                     //출고작업장아이디
            grdToolMoveInput.View.AddTextBoxColumn("SENDAREA", 120)
                .SetIsReadOnly(true);                                               //출고작업장
            grdToolMoveInput.View.AddTextBoxColumn("ISMODIFY")
                .SetIsHidden();                                                     //작업장권한
            InitializeAreaPopupColumnInDetailGrid();
            grdToolMoveInput.View.AddTextBoxColumn("RECEIPTAREAID")
                .SetIsHidden();                                                     //입고작업장아이디
            grdToolMoveInput.View.AddTextBoxColumn("JOBTYPEID")
                .SetIsHidden();                                                     //작업구분아이디
            grdToolMoveInput.View.AddTextBoxColumn("JOBTYPE", 80)
                .SetIsReadOnly(true);                                               //작업구분
            grdToolMoveInput.View.AddTextBoxColumn("PRODUCTIONTYPEID")
                .SetIsHidden();                                                     //생산구분아이디
            grdToolMoveInput.View.AddTextBoxColumn("PRODUCTIONTYPE", 80)
                .SetIsReadOnly(true);                                               //생산구분
            grdToolMoveInput.View.AddTextBoxColumn("CUSTOMERID")
                .SetIsHidden();                                                     //고객사아이디
            grdToolMoveInput.View.AddTextBoxColumn("CUSTOMER", 150)
                .SetIsReadOnly(true);                                               //고객사
            grdToolMoveInput.View.AddTextBoxColumn("QTY", 80)
                .SetIsReadOnly(true);                                               //고객사
            grdToolMoveInput.View.AddTextBoxColumn("CONTRACTIONX", 100)
                .SetIsReadOnly(true);                                               //요청수축률X
            grdToolMoveInput.View.AddTextBoxColumn("CONTRACTIONY", 100)
                .SetIsReadOnly(true);                                               //요청수축률Y
            grdToolMoveInput.View.AddTextBoxColumn("FILMTYPEID")
                .SetIsHidden();                                                     //필름구분아이디
            grdToolMoveInput.View.AddTextBoxColumn("FILMTYPE", 100)
                .SetIsReadOnly(true);                                               //필름구분
            grdToolMoveInput.View.AddTextBoxColumn("FILMCATEGORYID")
                .SetIsHidden();                                                     //필름유형아이디
            grdToolMoveInput.View.AddTextBoxColumn("FILMCATEGORY", 120)
                .SetIsReadOnly(true);                                               //필름유형
            grdToolMoveInput.View.AddTextBoxColumn("FILMDETAILCATEGORYID")
                .SetIsHidden();                                                     //필름상세유형아이디
            grdToolMoveInput.View.AddTextBoxColumn("FILMDETAILCATEGORY", 150)
                .SetIsReadOnly(true);                                               //필름상세유형
            grdToolMoveInput.View.AddTextBoxColumn("FILMUSELAYER1", 100)
                .SetIsReadOnly(true);                                               //Layer
            grdToolMoveInput.View.AddTextBoxColumn("FILMUSELAYER2", 120)
                .SetIsReadOnly(true);                                               //Layer
            grdToolMoveInput.View.AddTextBoxColumn("RESOLUTION", 80)
                .SetIsReadOnly(true);                                               //해상도
            grdToolMoveInput.View.AddTextBoxColumn("ISCOATING", 80)
                .SetIsReadOnly(true);                                               //코팅유무
            grdToolMoveInput.View.AddTextBoxColumn("MAKEVENDORID")
                .SetIsHidden();                                                     //제작업체아이디
            grdToolMoveInput.View.AddTextBoxColumn("MAKEVENDOR", 150)
                .SetIsHidden()
                .SetIsReadOnly(true);                                               //제작업체

            grdToolMoveInput.View.PopulateColumns();
        }
        #endregion

        #region Site ComboBox  초기화
        /// <summary>
        /// 각각의 SITE관련 콤보박스를 초기화한다.
        /// </summary>
        /// <param name="plantID"></param>
        private void InitializeSiteComboBox()
        {
            //이동내역의 출고 SITE
            cboSite.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboSite.ValueMember = "PLANTID";
            cboSite.DisplayMember = "PLANTID";
            //cboSite.DataSource = SqlExecuter.Query("GetPlantList", "00001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
            cboSite.ShowHeader = false;

            //이동내역의 입고 SITE
            cboReceiptSite.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboReceiptSite.ValueMember = "PLANTID";
            cboReceiptSite.DisplayMember = "PLANTID";
            cboReceiptSite.UseEmptyItem = true;
            cboReceiptSite.EmptyItemCaption = "";
            cboReceiptSite.EmptyItemValue = "";
            cboReceiptSite.DataSource = SqlExecuter.Query("GetPlantList", "00001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
            cboReceiptSite.ShowHeader = false;

            //이동출고의 입고 SITE
            cboReceiptSiteInput.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboReceiptSiteInput.ValueMember = "PLANTID";
            cboReceiptSiteInput.DisplayMember = "PLANTID";
            _moveSite = SqlExecuter.Query("GetPlantList", "00001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
            cboReceiptSiteInput.DataSource = _moveSite;
            cboReceiptSiteInput.ShowHeader = false;


            //검색의 출고 SITE
            cboSiteSearch.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboSiteSearch.ValueMember = "PLANTID";
            cboSiteSearch.DisplayMember = "PLANTID";
            //cboSiteSearch.UseEmptyItem = true;
            //cboSiteSearch.EmptyItemCaption = "";
            //cboSiteSearch.EmptyItemValue = "";
            //cboSiteSearch.DataSource = SqlExecuter.Query("GetPlantList", "00001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
            cboSiteSearch.ShowHeader = false;
            cboSiteSearch.EditValue = UserInfo.Current.Plant;
            //cboSiteSearch.ReadOnly = true;
        }
        #endregion

        #region InitializeInfo : 기초정보 초기화 초기화
        /// <summary>
        /// 각각의 작업장 관련 콤보박스를 초기화한다.
        /// </summary>
        /// <param name="plantID"></param>
        private void InitializeInfo()
        {
            _sendUserID = UserInfo.Current.Id;
            txtSendUserInput.EditValue = UserInfo.Current.Name;

            deSendDateInput.EditValue = DateTime.Now;

            cboReceiptSiteInput.EditValue = UserInfo.Current.Plant;

            cboSiteSearch.EditValue = UserInfo.Current.Plant;

            cboSite.EditValue = UserInfo.Current.Plant;

            ConditionsVisible = false;
        }
        #endregion

        #region ToolMoveType ComboBox  초기화
        /// <summary>
        /// 각각의 작업장 관련 콤보박스를 초기화한다.
        /// </summary>
        /// <param name="plantID"></param>
        private void InitializeToolMoveTypeComboBox()
        {
            //이동내역의 이동구분
            cboToolMoveType.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboToolMoveType.ValueMember = "CODEID";
            cboToolMoveType.DisplayMember = "CODENAME";
            cboToolMoveType.UseEmptyItem = true;
            cboToolMoveType.EmptyItemCaption = "";
            cboToolMoveType.EmptyItemValue = "";
            cboToolMoveType.DataSource = SqlExecuter.Query("GetCodeListByToolWithLanguage", "10001"
             , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "ToolMoveType" } });

            cboToolMoveType.ShowHeader = false;

            //이동출고의 이동구분
            cboToolMoveTypeInput.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboToolMoveTypeInput.ValueMember = "CODEID";
            cboToolMoveTypeInput.DisplayMember = "CODENAME";
            cboToolMoveTypeInput.DataSource = SqlExecuter.Query("GetCodeListByToolWithLanguage", "10001"
             , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "ToolMoveType" } });

            cboToolMoveTypeInput.ShowHeader = false;
        }
        #endregion

        #region IsReceipt ComboBox  초기화
        /// <summary>
        /// 각각의 입고여부 관련 콤보박스를 초기화한다.
        /// </summary>
        /// <param name="plantID"></param>
        private void InitializeIsReceiptComboBox()
        {
            cboIsReceipt.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboIsReceipt.ValueMember = "CODEID";
            cboIsReceipt.DisplayMember = "CODENAME";
            cboIsReceipt.UseEmptyItem = true;
            cboIsReceipt.EmptyItemCaption = "";
            cboIsReceipt.EmptyItemValue = "";
            cboIsReceipt.DataSource = SqlExecuter.Query("GetCodeList", "00001"
             , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "YesNo" } });

            cboIsReceipt.ShowHeader = false;

            //이동내역의 이동구분
        }
        #endregion

        #region SetProductDefIDPopup : 품목코드 팝업
        /// <summary>
        /// 품목코드 팝업
        /// </summary>
        private void SetProductDefIDPopup()
        {
            ConditionItemSelectPopup productCondition = new ConditionItemSelectPopup();
            productCondition.Id = "PRODUCTDEFID";
            productCondition.SearchQuery = new SqlQuery("GetProductdefidPoplistByTool", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"PRODUCTDEFTYPE=Product");
            productCondition.SetPopupLayout("PRODUCTDEFIDPOPUP", PopupButtonStyles.Ok_Cancel, true, false);
            productCondition.SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow);
            productCondition.ValueFieldName = "PRODUCTDEFID";
            productCondition.DisplayFieldName = "PRODUCTDEFNAME";
            productCondition.SetPopupResultCount(1);
            productCondition.SetPopupAutoFillColumns("PRODUCTDEFID");
            productCondition.SetPopupResultMapping("PRODUCTDEFID", "PRODUCTDEFID");
            //productCondition.SetPopupApplySelection((selectedRows, dataGridRows) =>
            //{
            //    //작업장 변경 시 작업자 조회
            //    selectedRows.Cast<DataRow>().ForEach(row =>
            //    {
            //        popEditProductDefID.EditValue = row.GetString("PRODUCTDEFID");
            //    });

            //});

            // 팝업 조회조건
            productCondition.Conditions.AddTextBox("PRODUCTDEF")
                .SetLabel("PRODUCTDEF");
            //productCondition.Conditions.AddComboBox("PRODUCTDEFTYPE", new SqlQuery("GetCodeListByOspProductdeftype", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } }), "CODENAME", "CODEID")
            //    .SetLabel("PRODUCTDEFTYPE")
            //    .SetDefault("Product")
            //    .SetIsHidden()
            //    .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
            //    ;
            //productCondition.Conditions.AddTextBox("PRODUCTDEFID")
            //    .SetLabel("PRODUCTDEFID");

            // 팝업 그리드
            productCondition.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 250)
                .SetIsReadOnly();
            productCondition.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly();
            productCondition.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 350)
                .SetIsReadOnly();
            productCondition.GridColumns.AddTextBoxColumn("PRODUCTDEFCODE", 50)
                .SetIsHidden();
            productCondition.GridColumns.AddTextBoxColumn("PRODUCTDEFTYPE", 200)
                .SetIsReadOnly();

            popEditProductDefID.SelectPopupCondition = productCondition;

        }
        #endregion

        #region InitializeSendArea : 출고작업장 제어
        /// <summary>
        /// 팝업창 제어
        /// </summary>
        private void InitializeSendArea()
        {
            sendAreaCondition = new ConditionItemSelectPopup();
            sendAreaCondition.Id = "SENDAREA";
            sendAreaCondition.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CURRENTLOGINID={UserInfo.Current.Id}");
            sendAreaCondition.ValueFieldName = "AREAID";
            sendAreaCondition.DisplayFieldName = "AREANAME";
            sendAreaCondition.SetPopupLayout("SENDAREA", PopupButtonStyles.Ok_Cancel, true, true);
            sendAreaCondition.SetPopupResultCount(1);
            sendAreaCondition.SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow);
            sendAreaCondition.SetPopupAutoFillColumns("AREANAME");
            sendAreaCondition.SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                //작업장 변경 시 작업자 조회
                selectedRows.Cast<DataRow>().ForEach(row =>
                {
                    _statusSendArea = row.GetString("AREAID");
                    popEditSendArea.EditValue = row.GetString("AREANAME");
                });
            });

            sendAreaCondition.Conditions.AddTextBox("AREANAME");

            sendAreaCondition.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetIsHidden();
            sendAreaCondition.GridColumns.AddTextBoxColumn("AREANAME", 200);

            popEditSendArea.SelectPopupCondition = sendAreaCondition;
        }
        #endregion        

        #region InitializeReceiptArea : 입고작업장 제어
        /// <summary>
        /// 팝업창 제어
        /// </summary>
        private void InitializeReceiptArea()
        {
            receiptAreaCondition = new ConditionItemSelectPopup();
            receiptAreaCondition.Id = "RECEIPTAREA";
            receiptAreaCondition.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CURRENTLOGINID={UserInfo.Current.Id}");
            receiptAreaCondition.ValueFieldName = "AREAID";
            receiptAreaCondition.DisplayFieldName = "AREANAME";
            receiptAreaCondition.SetPopupLayout("RECEIPTAREA", PopupButtonStyles.Ok_Cancel, true, true);
            receiptAreaCondition.SetPopupResultCount(1);
            receiptAreaCondition.SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow);
            receiptAreaCondition.SetPopupAutoFillColumns("AREANAME");
            receiptAreaCondition.SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                //작업장 변경 시 작업자 조회
                selectedRows.Cast<DataRow>().ForEach(row =>
                {
                    _statusReceiptArea = row.GetString("AREAID");
                    popEditReceiptArea.EditValue = row.GetString("AREANAME");
                });
            });

            receiptAreaCondition.Conditions.AddTextBox("AREANAME");

            receiptAreaCondition.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetIsHidden();
            receiptAreaCondition.GridColumns.AddTextBoxColumn("AREANAME", 200);

            popEditReceiptArea.SelectPopupCondition = receiptAreaCondition;
        }
        #endregion 

        #region InitializeReceiptAreaInput : 입고작업장 (입력용) 제어
        /// <summary>
        /// 팝업창 제어
        /// </summary>
        private void InitializeReceiptAreaInput()
        {
            ConditionItemSelectPopup areaCondition = new ConditionItemSelectPopup();
            areaCondition.Id = "RECEIPTAREAINPUT";
            areaCondition.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={_receiptSiteInput}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CURRENTLOGINID={UserInfo.Current.Id}");
            areaCondition.ValueFieldName = "AREAID";
            areaCondition.DisplayFieldName = "AREANAME";
            areaCondition.SetPopupLayout("RECEIPTAREAINPUT", PopupButtonStyles.Ok_Cancel, true, true);
            areaCondition.SetPopupResultCount(1);
            areaCondition.SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow);
            areaCondition.SetPopupAutoFillColumns("AREANAME");
            areaCondition.SetRelationIds("PLANTID");
            areaCondition.SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                //작업장 변경 시 작업자 조회
                selectedRows.Cast<DataRow>().ForEach(row =>
                {
                    _inputReceiptArea = row.GetString("AREAID");
                    popEditReceiptAreaInput.EditValue = row.GetString("AREANAME");
                });
            });

            areaCondition.Conditions.AddTextBox("AREANAME");

            areaCondition.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetIsHidden();
            areaCondition.GridColumns.AddTextBoxColumn("AREANAME", 200);

            popEditReceiptAreaInput.SelectPopupCondition = areaCondition;
        }
        #endregion

        #region InitializeSendAreaSearch : 출고작업장 (입력을 위한 검색용) 제어
        /// <summary>
        /// 팝업창 제어
        /// </summary>
        private void InitializeSendAreaSearch()
        {
            areaCondition = new ConditionItemSelectPopup();
            areaCondition.Id = "SENDAREASEARCH";
            areaCondition.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CURRENTLOGINID={UserInfo.Current.Id}");
            areaCondition.ValueFieldName = "AREAID";
            areaCondition.DisplayFieldName = "AREANAME";
            areaCondition.SetPopupLayout("SENDAREASEARCH", PopupButtonStyles.Ok_Cancel, true, true);
            areaCondition.SetPopupResultCount(1);
            areaCondition.SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow);
            areaCondition.SetPopupAutoFillColumns("AREANAME");
            areaCondition.SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                //작업장 변경 시 작업자 조회
                selectedRows.Cast<DataRow>().ForEach(row =>
                {
                    _searchSendArea = row.GetString("AREAID");
                    popEditSendAreaSearch.EditValue = row.GetString("AREANAME");
                });
            });

            areaCondition.Conditions.AddTextBox("AREANAME");

            areaCondition.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetIsHidden();
            areaCondition.GridColumns.AddTextBoxColumn("AREANAME", 200);

            popEditSendAreaSearch.SelectPopupCondition = areaCondition;
        }
        #endregion 

        #region InitializeAreaPopupColumnInDetailGrid - 작업장을 팝업검색할 필드 설정
        /// <summary>
        /// grid ProcesssegmentidLis
        /// </summary>
        private void InitializeAreaPopupColumnInDetailGrid()
        {
            _popupGridToolArea = grdToolMoveInput.View.AddSelectPopupColumn("RECEIPTAREA", 180, new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"CURRENTLOGINID={UserInfo.Current.Id}", $"ISMOD=Y"))
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
                    foreach (DataRow row in selectedRows)
                    {
                        grdToolMoveInput.View.GetFocusedDataRow()["RECEIPTAREAID"] = row["AREAID"];
                        grdToolMoveInput.View.GetFocusedDataRow()["RECEIPTAREA"] = row["AREANAME"];
                        grdToolMoveInput.View.GetFocusedDataRow()["ISMODIFY"] = row["ISMODIFY"];
                    }
                })
            //.SetPopupValidationCustom(ValidationParentAreaIdPopup);
            ;
            // 팝업에서 사용할 조회조건 항목 추가
            _popupGridToolArea.Conditions.AddTextBox("AREANAME");
            _popupGridToolArea.Conditions.AddComboBox("FACTORYID", new SqlQuery("GetFactoryListByEqp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "FACTORYNAME", "FACTORYID")
                ;


            // 팝업 그리드 설정
            _popupGridToolArea.GridColumns.AddTextBoxColumn("AREAID", 300)
                .SetIsHidden();
            _popupGridToolArea.GridColumns.AddTextBoxColumn("AREANAME", 300)
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
            btnSearchToolMove.Click += BtnSearchToolMove_Click;
            btnBrowse.Click += BtnBrowse_Click;
            btnSave.Click += BtnSave_Click;
            btnDelete.Click += BtnDelete_Click;
            cboReceiptSiteInput.EditValueChanged += CboReceiptSiteInput_EditValueChanged;
            cboToolMoveTypeInput.EditValueChanged += CboToolMoveTypeInput_EditValueChanged;
            grdSendMoveToolStatus.View.CheckStateChanged += grdSendMoveToolStatus_CheckStateChanged;
            grdToolMoveInput.View.CellValueChanged += grdToolMoveInput_CellValueChanged;

            cboReceiptSite.EditValueChanged += CboReceiptSite_EditValueChanged;

            cboSite.EditValueChanged += CboSite_EditValueChanged;

            tabMakeReceipt.SelectedPageChanged += TabMakeReceipt_SelectedPageChanged;

            Shown += SendMoveTool_Shown;
        }

        private void CboReceiptSite_EditValueChanged(object sender, EventArgs e)
        {
            if (cboReceiptSite.EditValue != null)
            {
                _receiptSiteInput = cboReceiptSite.EditValue.ToString();
                receiptAreaCondition.SearchQuery =
                   new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={_receiptSiteInput}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CURRENTLOGINID={UserInfo.Current.Id}");
            }
        }

        private void CboSite_EditValueChanged(object sender, EventArgs e)
        {
            ChangeSiteConditionForResult();
        }

        private void TabMakeReceipt_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (tabMakeReceipt.SelectedTabPage == tabPageMakeReceipt)
            {
                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Visible = false;
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Cancel2"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Cancel2"].Visible = true;
                }
            }
            else
            {
                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Visible = true;
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Cancel2"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Cancel2"].Visible = false;
                }
            }
        }

        #region SendMoveTool_Shown - ToolBar 및 Site관련 설정을 화면 로딩후에 일괄 적용
        private void SendMoveTool_Shown(object sender, EventArgs e)
        {
            ChangeSiteCondition();

            SetPlantFromSystem();

            //다중 Site 권한을 가진 사용자가 Site를 변경시 환경을 변경해줘야한다.
            cboSiteSearch.EditValueChanged += ConditionPlant_EditValueChanged;

            //Tab Page에 따라 버튼 조절
            if (pnlToolbar.Controls["layoutToolbar"] != null)
            {
                if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                    pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Visible = false;
                if (pnlToolbar.Controls["layoutToolbar"].Controls["Cancel2"] != null)
                    pnlToolbar.Controls["layoutToolbar"].Controls["Cancel2"].Visible = true;
            }
        }

        private void ConditionPlant_EditValueChanged(object sender, EventArgs e)
        {
            ChangeSiteCondition();
        }
        #endregion

        private void grdToolMoveInput_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "RECEIPTAREA")
            {
                grdToolMoveInput.View.CellValueChanged -= grdToolMoveInput_CellValueChanged;

                DataRow row = grdToolMoveInput.View.GetDataRow(e.RowHandle);

                if (row["RECEIPTAREA"].ToString().Equals(""))
                {
                    grdToolMoveInput.View.CellValueChanged += grdToolMoveInput_CellValueChanged;
                    return;
                }
                GetSingleReceiptArea(row.GetString("RECEIPTAREA"), e.RowHandle);
                grdToolMoveInput.View.CellValueChanged += grdToolMoveInput_CellValueChanged;
            }
        }

        private void CboToolMoveTypeInput_EditValueChanged(object sender, EventArgs e)
        {
            if (cboToolMoveTypeInput.EditValue != null)
            {
                if (cboToolMoveTypeInput.EditValue.ToString() == "Move")
                {
                    cboReceiptSiteInput.DataSource = _moveSite;
                    cboReceiptSiteInput.EditValue = UserInfo.Current.Plant;
                    cboReceiptSiteInput.ReadOnly = true;
                }
                else
                {
                    //같은 사이트로는 이관할 수 없다.
                    DataTable tempSite = _moveSite.Copy();

                    for (int i = tempSite.Rows.Count - 1; i >= 0; i--)
                    {
                        if (tempSite.Rows[i].GetString("PLANTID").Equals(cboSiteSearch.EditValue.ToString()))
                            tempSite.Rows.RemoveAt(i);
                    }

                    cboReceiptSiteInput.DataSource = tempSite;

                    cboReceiptSiteInput.ReadOnly = false;
                }
            }
        }

        private void grdSendMoveToolStatus_CheckStateChanged(object sender, EventArgs e)
        {
            //이미 입고된 데이터는 체크할 수 없다.
            if (!grdSendMoveToolStatus.View.GetFocusedDataRow().GetString("RECEIPTDATE").Equals(""))
            {
                grdSendMoveToolStatus.View.SetFocusedRowCellValue("_INTERNAL_CHECKMARK_SELECTION_", false);
            }
        }

        private void CboReceiptSiteInput_EditValueChanged(object sender, EventArgs e)
        {
            if (cboReceiptSiteInput.EditValue != null)
            {
                _receiptSiteInput = cboReceiptSiteInput.EditValue.ToString();
                _popupGridToolArea.SearchQuery =
                   new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={_receiptSiteInput}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CURRENTLOGINID={UserInfo.Current.Id}", $"ISMOD=Y");
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DeleteData();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void BtnSearchToolMove_Click(object sender, EventArgs e)
        {
            SearchSendTarget();
        }
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
            else if (btn.Name.ToString().Equals("Cancel2"))
                BtnDelete_Click(sender, e);
            else if (btn.Name.ToString().Equals("Search"))
            {
                if (tabMakeReceipt.SelectedTabPage == tabPageMakeReceipt)
                    BtnBrowse_Click(sender, e);
                else
                    BtnSearchToolMove_Click(sender, e);
            }
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
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);
            #endregion

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolRepairResult = SqlExecuter.Query("GetMovedFilmListForFilmMoveByTool", "10001", values);

            grdSendMoveToolStatus.DataSource = toolRepairResult;

            if (toolRepairResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            else
            {
                grdSendMoveToolStatus.View.FocusedRowHandle = 0;
                DisplayToolMoveInfo();
            }

            _currentStatus = "modified";
        }
        #endregion

        #region Search - 검색
        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected void Search()
        {
            //탭페이지를 내역페이지를 선택페이지로 변경한다.
            tabMakeReceipt.SelectedTabPage = tabPageMakeReceipt;

            InitializeInsertForm();
            // TODO : 조회 SP 변경
            Dictionary<string, object> values = new Dictionary<string, object>();

            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("PLANTID", cboSite.EditValue);
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);

            if (ValidateEditValue(popEditSendArea.EditValue))
                values.Add("AREAID", _statusSendArea);

            if (ValidateEditValue(popEditReceiptArea.EditValue))
                values.Add("TOAREAID", _statusReceiptArea);

            if (ValidateEditValue(cboToolMoveType.EditValue))
                values.Add("TOOLMOVETYPE", cboToolMoveType.EditValue);

            if (ValidateEditValue(deSendDateFrom.EditValue))
            {
                DateTime sendDateFr = Convert.ToDateTime(deSendDateFrom.EditValue);
                values.Add("SENDDATE_PERIODFR", sendDateFr.ToString("yyyy-MM-dd HH:mm:ss"));
            }

            if (ValidateEditValue(deSendDateTo.EditValue))
            {
                DateTime sendDateTo = Convert.ToDateTime(deSendDateTo.EditValue);
                values.Add("SENDDATE_PERIODTO", sendDateTo.ToString("yyyy-MM-dd HH:mm:ss"));
            }

            if (ValidateEditValue(txtToolNo.EditValue))
                values.Add("DURABLELOTID", txtToolNo.EditValue);

            if (ValidateEditValue(cboIsReceipt.EditValue))
                values.Add("ISRECEIPT", cboIsReceipt.EditValue);

            if (ValidateEditValue(cboReceiptSite.EditValue))
                values.Add("TOPLANTID", cboReceiptSite.EditValue);

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolMoveList = SqlExecuter.Query("GetMovedFilmListForFilmMoveByTool", "10001", values);

            grdSendMoveToolStatus.DataSource = toolMoveList;

            if (toolMoveList.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.                
            }
            else
            {
                grdSendMoveToolStatus.View.FocusedRowHandle = 0;
                DisplayToolMoveInfo();
            }

            _currentStatus = "modified";
        }
        #endregion

        #region SearchSendTarget - 검색
        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected void SearchSendTarget()
        {
            //탭페이지를 내역페이지를 선택페이지로 변경한다.            
            //InitializeInsertForm();
            // TODO : 조회 SP 변경
            Dictionary<string, object> values = new Dictionary<string, object>();

            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);

            if (ValidateEditValue(cboSiteSearch.EditValue))
                values.Add("PLANTID", cboSiteSearch.EditValue);
            if (ValidateEditValue(popEditSendAreaSearch.EditValue))
                values.Add("AREAID", _searchSendArea);

            if (ValidateEditValue(popEditProductDefID.EditValue))
                values.Add("PRODUCTDEFID", popEditProductDefID.GetValue());

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolMoveList = SqlExecuter.Query("GetTargetFilmListForFilmMoveByTool", "10001", values);

            grdToolMoveInput.DataSource = toolMoveList;

            if (toolMoveList.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
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

        #endregion

        #region 유효성 검사
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }

        #region ValidateContent - Validation을 진행한다.
        private bool ValidateContent(out string messageCode)
        {
            messageCode = "";
            //각각의 상태에 따라 보여줄 메세지가 상이하다면 각각의 분기에서 메세지를 표현해야 한다.
            if (!ValidateEditValue(deSendDateInput.EditValue))
            {
                messageCode = "ValidateToolMoveTypeStatus";
                return false;
            }

            if (!ValidateEditValue(txtSendUserInput.EditValue))
            {
                messageCode = "ValidateToolMoveTypeStatus";
                return false;
            }

            if (!ValidateEditValue(cboToolMoveTypeInput.EditValue))
            {
                messageCode = "ValidateToolMoveTypeStatus";
                return false;
            }

            if (!ValidateEditValue(cboReceiptSiteInput.EditValue))
            {
                messageCode = "ValidateToolMoveTypeStatus";
                return false;
            }

            //체크된 값이 없으면 false
            if (grdToolMoveInput.View.GetCheckedRows().Rows.Count == 0)
            {
                messageCode = "VALIDATERECEIPTAREA";
                return false;
            }

            DataTable dataTables = (DataTable)grdToolMoveInput.View.GetCheckedRows();
            foreach (DataRow currentRow in dataTables.Rows)
            {
                if (!ValidateCellInGrid(currentRow, new string[] { "RECEIPTAREAID" }))
                {
                    messageCode = "VALIDATERECEIPTAREA";
                    return false;
                }

                if(currentRow.GetString("ISMODIFY").Equals("N"))
                {
                    messageCode = "ValidateMoveFilmSecurityReq"; //이동할 작업장에 작업권한이 없습니다.
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region ValidateComboBoxEqualValue - 2개의 콤보박스의 값이 동일한지 검증한다.
        private bool ValidateComboBoxEqualValue(SmartComboBox originBox, SmartComboBox targetBox)
        {
            //두 콤보박스의 값이 같으면 안된다.
            if (originBox.EditValue.Equals(targetBox.EditValue))
                return false;
            return true;
        }
        #endregion

        #region ValidateNumericBox - 숫자형 텍스트 박스가 기준값보다 높은지 검증
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

        #region ValidateEditValue - 특정값에 대한 Validation
        private bool ValidateEditValue(object editValue)
        {
            if (editValue == null)
                return false;

            if (editValue.ToString() == "")
                return false;

            return true;
        }
        #endregion

        #region ValidateCellInGrid - 그리드내의 특정컬럼에 대한 Validation
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

        #region ValidateCompareCellInGrid - 그리드내의 2개의 특정 컬럼의 값을 비교한다.
        private bool ValidateCompareCellInGrid(DataTable currentTable, string firstColumnName, string secondColumnName)
        {
            foreach (DataRow currentRow in currentTable.Rows)
                if (currentRow.GetString(firstColumnName).Equals(currentRow.GetString(secondColumnName)))
                    return false;

            return true;
        }
        #endregion

        #region SetRequiredValidationControl - 필수입력항목 설정
        private void SetRequiredValidationControl(Control requiredControl)
        {
            requiredControl.ForeColor = Color.Red;
        }
        #endregion

        #region ValidateProductCode : 품목코드와 관계된 치공구를 부르기 위해 품목코드가 입력되었는지 확인한다.
        private bool ValidateProductCode()
        {
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
                _currentStatus = "added";           //현재 화면의 상태는 입력중이어야 한다.

                //이동일
                DateTime dateNow = DateTime.Now;
                deSendDateInput.EditValue = dateNow;

                //이동구분
                cboToolMoveTypeInput.EditValue = null;

                //출고자
                txtSendUserInput.EditValue = UserInfo.Current.Name;

                _sendUserID = UserInfo.Current.Id;

                cboReceiptSiteInput.EditValue = null;

                popEditReceiptAreaInput.EditValue = "";
                _inputReceiptArea = null;


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
                popEditSendArea.ReadOnly = false;
                txtSendUserInput.ReadOnly = true;
                deSendDateInput.ReadOnly = false;
                popEditReceiptAreaInput.ReadOnly = false;
                cboReceiptSiteInput.ReadOnly = false;
                cboToolMoveTypeInput.ReadOnly = false;
            }
            else if (currentStatus == "modified") //
            {
                popEditSendArea.ReadOnly = false;
                txtSendUserInput.ReadOnly = true;
                deSendDateInput.ReadOnly = true;
                popEditReceiptAreaInput.ReadOnly = false;
                cboReceiptSiteInput.ReadOnly = false;
                cboToolMoveTypeInput.ReadOnly = false;
            }
            else
            {
                popEditSendArea.ReadOnly = false;
                txtSendUserInput.ReadOnly = true;
                deSendDateInput.ReadOnly = true;
                popEditReceiptAreaInput.ReadOnly = true;
                cboReceiptSiteInput.ReadOnly = true;
                cboToolMoveTypeInput.ReadOnly = true;
            }
        }
        #endregion

        #region InitRequiredControl - 필수입력항목들을 체크한다.
        private void InitRequiredControl()
        {
            SetRequiredValidationControl(lblSendDate);
            SetRequiredValidationControl(lblSendUser);
            SetRequiredValidationControl(lblToolMoveType);
            SetRequiredValidationControl(lblReceiptSite);
            SetRequiredValidationControl(lblReceiptArea);
        }
        #endregion

        #region CreateSaveDatatable : ToolMove 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// <summary>
        /// ToolMove 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// 입력시에는 ToolRequest와 ToolRequestDetail에 입력하고
        /// 수정시에는 ToolRequest와 ToolRequestDetail 및 ToolRequestDetailLot에 까지 데이터를 기입한다.
        /// </summary>
        /// <returns></returns>
        private DataTable CreateSaveDatatable(bool useState)
        {
            DataTable dt = new DataTable();
            dt.TableName = "toolMoveList";
            //===================================================================================
            dt.Columns.Add("TOOLNUMBER");
            dt.Columns.Add("AREAID");
            dt.Columns.Add("SENDDATE");
            dt.Columns.Add("SENDUSERID");
            dt.Columns.Add("TOOLMOVETYPEID");
            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");
            dt.Columns.Add("TOAREAID");
            dt.Columns.Add("TOPLANTID");

            dt.Columns.Add("LOTHISTKEY");

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
                if (grdToolMoveInput.View.GetCheckedRows().Rows.Count > 0)
                {
                    //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                    if (ValidateContent(out messageCode))
                    {

                        DataSet toolMoveSet = new DataSet();
                        //치공구 제작의뢰를 입력
                        DataTable toolMoveTable = CreateSaveDatatable(true);

                        DataTable moveTargetTable = grdToolMoveInput.View.GetCheckedRows();
                        DateTime sendDate = DateTime.Now;
                        _sendUserID = UserInfo.Current.Id;

                        if (ValidateCompareCellInGrid(moveTargetTable, "SENDAREAID", "RECEIPTAREAID"))
                        {

                            foreach (DataRow moveTargetRow in moveTargetTable.Rows)
                            {
                                DataRow toolMoveRow = toolMoveTable.NewRow();

                                toolMoveRow["TOOLNUMBER"] = moveTargetRow.GetString("FILMNO");
                                toolMoveRow["AREAID"] = moveTargetRow.GetString("SENDAREAID");
                                toolMoveRow["SENDDATE"] = sendDate.ToString("yyyy-MM-dd HH:mm:ss");
                                toolMoveRow["SENDUSERID"] = _sendUserID;
                                toolMoveRow["TOOLMOVETYPEID"] = cboToolMoveTypeInput.EditValue;
                                toolMoveRow["TOAREAID"] = moveTargetRow.GetString("RECEIPTAREAID");
                                toolMoveRow["TOPLANTID"] = cboReceiptSiteInput.EditValue;

                                toolMoveRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                                toolMoveRow["PLANTID"] = moveTargetRow.GetString("SENDPLANTID");

                                toolMoveRow["CREATOR"] = _sendUserID;
                                toolMoveRow["MODIFIER"] = _sendUserID;

                                //로직상 무조건 추가만 진행된다.
                                toolMoveRow["_STATE_"] = "added";
                                toolMoveRow["VALIDSTATE"] = "Valid";

                                toolMoveTable.Rows.Add(toolMoveRow);
                            }

                            toolMoveSet.Tables.Add(toolMoveTable);

                            this.ExecuteRule<DataTable>("ToolMove", toolMoveSet);

                            ControlEnableProcess("modified");

                            ////가져온 결과값에 따라 재검색한 후 가져온 아이디에 맞는 행을 선택으로 변경한다.
                            Search();
                            SearchSendTarget();
                        }
                        else
                        {
                            ShowMessage(MessageBoxButtons.OK, "VALIDATECOMPARERECEIPTANDSENDAREA", "");
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
                btnSave.Enabled = true;
            }
        }
        #endregion

        #region DeleteData : 삭제를 진행
        private void DeleteData()
        {
            //Validation 체크 부분 작성 필요
            this.ShowWaitArea();

            //저장 로직
            try
            {
                btnDelete.Enabled = false;
                if (grdSendMoveToolStatus.View.GetCheckedRows().Rows.Count > 0)
                {

                    DataSet toolMoveSet = new DataSet();

                    DataTable toolMoveTable = CreateSaveDatatable(true);

                    DataTable deleteTargetTable = grdSendMoveToolStatus.View.GetCheckedRows();

                    foreach (DataRow deleteTargetRow in deleteTargetTable.Rows)
                    {
                        DataRow toolMoveRow = toolMoveTable.NewRow();

                        toolMoveRow["TOOLNUMBER"] = deleteTargetRow.GetString("FILMNO");
                        toolMoveRow["AREAID"] = deleteTargetRow.GetString("SENDAREAID");
                        toolMoveRow["SENDDATE"] = Convert.ToDateTime(deleteTargetRow.GetString("SENDDATE")).ToString("yyyy-MM-dd HH:mm:ss");

                        toolMoveRow["CREATOR"] = _sendUserID;
                        toolMoveRow["MODIFIER"] = _sendUserID;

                        //로직상 무조건 삭제만 진행된다.
                        toolMoveRow["VALIDSTATE"] = "InValid";
                        toolMoveRow["_STATE_"] = "deleted";

                        toolMoveTable.Rows.Add(toolMoveRow);
                    }

                    if (toolMoveTable.Rows.Count > 0)
                    {
                        toolMoveSet.Tables.Add(toolMoveTable);

                        this.ExecuteRule<DataTable>("ToolMove", toolMoveSet);

                        ControlEnableProcess("modified");

                        //가져온 결과값에 따라 재검색한 후 가져온 아이디에 맞는 행을 선택으로 변경한다.
                        Search();
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
                btnDelete.Enabled = true;
            }
        }
        #endregion

        #region DisplayToolMoveInfo : 그리드에서 행 선택시 상세정보를 표시
        /// <summary>
        /// 그리드에서 행 선택시 상세정보를 표시하는 메소드
        /// </summary>
        private void DisplayToolMoveInfo()
        {
            //포커스 행 체크 
            if (grdSendMoveToolStatus.View.FocusedRowHandle < 0) return;

            DisplayToolMoveInfoDetail(grdSendMoveToolStatus.View.GetFocusedDataRow());
        }
        #endregion

        #region DisplayToolMoveInfoDetail : 입력받은 인덱스의 그리드의 내용을 화면에 바인딩
        /// <summary>
        /// 입력받은 그리드내의 인덱스에 대한 내용을 화면에 표시한다.
        /// </summary>
        /// <param name="rowHandle"></param>
        private void DisplayToolMoveInfoDetail(DataRow currentRow)
        {
            //해당 업무에 맞는 Enable체크 수행
            //grdToolRepairReceipt.View.FocusedRowHandle = rowHandle;

            //DataRow currentRow = grdToolRepairReceipt.View.GetFocusedDataRow();

            //수정상태라 판단하여 화면 제어
            ControlEnableProcess("modified");

            ////화면상태에 따른 탭및 그리드 제어
            //deReceiptDate.EditValue = currentRow.GetString("RECEIPTDATE");
            //_sendUserID = currentRow.GetString("RECEIPTUSERID");
            //txtReceiptUser.EditValue = currentRow.GetString("RECEIPTUSER");
            //_selectedVendor = currentRow.GetString("MAKEVENDORID");
            //popEditVendor.EditValue = currentRow.GetString("MAKEVENDOR");
            ////cboToolRepairType.EditValue = currentRow.GetString("TOOLREPAIRTYPEID");

            _currentStatus = "modified";

            //그리드 데이터 바인딩
            //SearchLotList(currentRow.GetString("RECEIPTDATE"), currentRow.GetString("RECEIPTSEQUENCE"));
            //각 순서에 따라 바인딩

            //그리드의 수정가능 여부
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
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);
            values.Add("AREANAME", areaName);

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable savedResult = SqlExecuter.Query("GetAreaListByTool", "10001", values);

            if (savedResult.Rows.Count.Equals(1))
            {
                grdToolMoveInput.View.SetRowCellValue(rowHandle, "RECEIPTAREAID", savedResult.Rows[0].GetString("AREAID"));
                grdToolMoveInput.View.SetRowCellValue(rowHandle, "RECEIPTAREA", savedResult.Rows[0].GetString("AREANAME"));
            }
            else
            {
                grdToolMoveInput.View.SetRowCellValue(rowHandle, "RECEIPTAREAID", "");
                grdToolMoveInput.View.SetRowCellValue(rowHandle, "RECEIPTAREA", "");

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
            //if (productPopup != null)
            //    productPopup.SearchQuery = new SqlQuery("GetProductdefidPoplistByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"PRODUCTDEFTYPE=Product");


            if (areaCondition != null)
                areaCondition.SearchQuery = new SqlQuery("GetAreaIDListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={cboSiteSearch.EditValue.ToString()}", $"CURRENTLOGINID={UserInfo.Current.Id}");

            //저장 버튼을 권한에 따라 막아준다.
            if (cboSiteSearch.EditValue.ToString().Equals(UserInfo.Current.Plant))
            {
                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = true;
                }
            }
            else
            {
                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = false;
                }
            }
        }
        #endregion

        #region ChangeSiteConditionForResult - 검색조건의 Site변경시 관련된 쿼리들을 변경
        private void ChangeSiteConditionForResult()
        {
            if (sendAreaCondition != null)
                sendAreaCondition.SearchQuery = new SqlQuery("GetAreaIDListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={cboSite.EditValue.ToString()}", $"CURRENTLOGINID={UserInfo.Current.Id}");

            //취소 버튼을 권한에 따라 막아준다.
            if (cboSite.EditValue.ToString().Equals(UserInfo.Current.Plant))
            {
                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Cancel2"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Cancel2"].Enabled = true;
                }
            }
            else
            {
                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Cancel2"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Cancel2"].Enabled = false;
                }
            }
        }
        #endregion

        #region SetPlantFromSystem - 시스템으로 설정된 Site의 정보를 바인딩
        private void SetPlantFromSystem()
        {
            //cboSiteSearch
            cboSiteSearch.DataSource = ((SmartComboBox)Conditions.GetControl("P_PLANTID")).DataSource;

            cboSite.DataSource = ((SmartComboBox)Conditions.GetControl("P_PLANTID")).DataSource;
        }
        #endregion

        #endregion
    }
}
