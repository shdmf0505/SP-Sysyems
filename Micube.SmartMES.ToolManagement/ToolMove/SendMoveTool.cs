#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.ToolManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 치공구 이동 출고
    /// 업  무  설  명  : 치공구관리 > 치공구 이동관리 > 치공구 이동입고
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-08-06
    /// 수  정  이  력  :
    ///
    ///
    /// </summary>
    public partial class SendMoveTool : SmartConditionManualBaseForm
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
        private ConditionItemSelectPopup _popupGridToolArea;

        private ConditionItemSelectPopup areaCondition;
        private ConditionItemSelectPopup sendAreaCondition;
        private ConditionItemSelectPopup receiptAreaCondition;
        private string[] _clipDatas;
        private int _clipIndex = 1;
        private IDataObject _clipBoardData;
        private bool _isGoodCopy = true;

        //이관용 Site
        private DataTable _moveSite;

        #endregion Local Variables

        #region 생성자

        public SendMoveTool()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();
            InitializeLanguageKey();
            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
            InitializeInputGrid();

            InitializeSiteComboBox();

            InitializeToolMoveTypeComboBox();
            InitializeIsReceiptComboBox();

            InitializeSendArea();
            InitializeReceiptArea();
            InitializeSendAreaSearch();

            InitializeInfo();
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            smartLayoutControl1.SetLanguageKey(layoutControlItem1, "SITE");
            smartLayoutControl1.SetLanguageKey(layoutControlItem2, "SENDAREA");
            smartLayoutControl1.SetLanguageKey(layoutControlItem3, "TOOLMOVETYPE");
            smartLayoutControl1.SetLanguageKey(layoutControlItem4, "SENDDATE");
            smartLayoutControl1.SetLanguageKey(layoutControlItem6, "TOOLNUMBER");
            smartLayoutControl1.SetLanguageKey(layoutControlItem7, "ISRECEIPT");
            smartLayoutControl1.SetLanguageKey(layoutControlItem8, "RECEIPTSITE");
            smartLayoutControl1.SetLanguageKey(layoutControlItem9, "RECEIPTAREA");

            smartLayoutControl3.SetLanguageKey(layoutControlItem12, "SENDPLANT");
            smartLayoutControl3.SetLanguageKey(layoutControlItem13, "SENDAREA");
            smartLayoutControl3.SetLanguageKey(lblToolMoveType, "TOOLMOVETYPE");
            smartLayoutControl3.SetLanguageKey(lblReceiptSite, "RECEIPTSITE");

            smartLayoutControl2.SetLanguageKey(layoutControlItem11, "PRODUCTDEF");
        }

        #region InitializeGrid - 치공구이동출고내역목록을 초기화한다.

        /// <summary>
        /// 치공구내역목록을 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdSendMoveToolStatus.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdSendMoveToolStatus.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdSendMoveToolStatus.ShowStatusBar = true;

            grdSendMoveToolStatus.View.AddTextBoxColumn("SENDDATE", 160)                 //출고일자
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly(true);
            grdSendMoveToolStatus.View.AddTextBoxColumn("TOOLMOVETYPEID")
                .SetIsHidden();                                                         //이동구분아이디
            grdSendMoveToolStatus.View.AddTextBoxColumn("TOOLMOVETYPE", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                   //이동구분
            grdSendMoveToolStatus.View.AddTextBoxColumn("TOOLNUMBER", 180)
                .SetIsReadOnly(true);                                                   //Tool 번호
            grdSendMoveToolStatus.View.AddTextBoxColumn("TOOLNAME", 400)
                .SetIsReadOnly(true);                                                   //Tool 번호
            grdSendMoveToolStatus.View.AddComboBoxColumn("TOOLFORMCODE", 150, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=ToolForm"), "CODENAME", "CODEID")
               .SetTextAlignment(TextAlignment.Center)
               .SetIsReadOnly(true);                                                  //상태 - (이 회면에서 입력해야 한다면 SetIsReadOnly를 풀어준다.
            grdSendMoveToolStatus.View.AddTextBoxColumn("SENDPLANTID")
                .SetIsHidden();                                                         //출고SITE아이디
            grdSendMoveToolStatus.View.AddTextBoxColumn("SENDPLANT", 100)
                .SetIsReadOnly(true);                                                   //출고SITE
            grdSendMoveToolStatus.View.AddTextBoxColumn("SENDAREAID")
                .SetIsHidden();                                                         //출고작업장아이디
            grdSendMoveToolStatus.View.AddTextBoxColumn("SENDAREA", 180)
                .SetIsReadOnly(true);                                                   //출고작업장
            grdSendMoveToolStatus.View.AddTextBoxColumn("RECEIPTPLANTID")
                .SetIsHidden();                                                         //입고SITE아이디
            grdSendMoveToolStatus.View.AddTextBoxColumn("RECEIPTPLANT", 100)
                .SetIsReadOnly(true);                                                   //입고SITE
            grdSendMoveToolStatus.View.AddTextBoxColumn("RECEIPTAREAID")
                .SetIsHidden();                                                         //입고작업장아이디
            grdSendMoveToolStatus.View.AddTextBoxColumn("RECEIPTAREA", 180)
                .SetIsReadOnly(true);                                                   //입고작업장
            grdSendMoveToolStatus.View.AddTextBoxColumn("RECEIPTDATE", 160)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly(true);                                                   //의뢰일자
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

        #endregion InitializeGrid - 치공구이동출고내역목록을 초기화한다.

        #region InitializeInputGrid - 입력대상인 치공구목록을 초기화한다.

        /// <summary>
        /// 입력대상인 치공구목록을 초기화한다.
        /// </summary>
        private void InitializeInputGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdToolMoveInput.GridButtonItem = GridButtonItem.Export;
            grdToolMoveInput.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdToolMoveInput.View.AddTextBoxColumn("TOOLNUMBER", 180)
                .SetIsReadOnly(true);                                               //Tool 번호
            grdToolMoveInput.View.AddTextBoxColumn("TOOLNAME", 400)
                .SetIsReadOnly(true);                                               //Tool 번호
            grdToolMoveInput.View.AddComboBoxColumn("TOOLFORMCODE", 150, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=ToolForm"), "CODENAME", "CODEID")
               .SetTextAlignment(TextAlignment.Center)
               .SetIsReadOnly(true);                                                  //상태 - (이 회면에서 입력해야 한다면 SetIsReadOnly를 풀어준다.
            grdToolMoveInput.View.AddTextBoxColumn("SENDPLANTID")
                .SetIsHidden();                                                     //출고SITE
            grdToolMoveInput.View.AddTextBoxColumn("SENDPLANT")
                .SetIsHidden();
            grdToolMoveInput.View.AddTextBoxColumn("SENDAREAID")
                .SetIsHidden();                                                     //출고작업장
            grdToolMoveInput.View.AddTextBoxColumn("SENDAREA")
                .SetIsHidden();
            InitializeAreaPopupColumnInDetailGrid();
            grdToolMoveInput.View.AddTextBoxColumn("ISMODIFY")
                .SetIsHidden();
            grdToolMoveInput.View.AddTextBoxColumn("RECEIPTAREAID")
                .SetIsHidden();                                                     //출고작업장아이디
            grdToolMoveInput.View.AddTextBoxColumn("PRODUCTDEFID", 120)
                .SetIsReadOnly(true);                                               //품목코드
            grdToolMoveInput.View.AddTextBoxColumn("PRODUCTDEFVERSION", 60)
                .SetIsReadOnly(true);                                               //품목코드
            grdToolMoveInput.View.AddTextBoxColumn("PRODUCTDEFNAME", 300)
                .SetIsReadOnly(true);                                               //품목명

            grdToolMoveInput.View.PopulateColumns();

            grdToolMoveInput.ShowStatusBar = false;
            grdToolMoveInput.View.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            grdToolMoveInput.View.GridControl.ToolTipController = new DevExpress.Utils.ToolTipController();
        }

        #endregion InitializeInputGrid - 입력대상인 치공구목록을 초기화한다.

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

        #endregion Site ComboBox  초기화

        #region InitializeInfo : 기초정보 초기화 초기화

        /// <summary>
        /// 각각의 작업장 관련 콤보박스를 초기화한다.
        /// </summary>
        /// <param name="plantID"></param>
        private void InitializeInfo()
        {
            lblToolMoveType.AppearanceItemCaption.ForeColor = Color.Red;
            lblReceiptSite.AppearanceItemCaption.ForeColor = Color.Red;

            tab1TopGroup.GridButtonItem = GridButtonItem.None;
            tab2TopGroup.GridButtonItem = GridButtonItem.None;

            _sendUserID = UserInfo.Current.Id;

            cboReceiptSiteInput.EditValue = UserInfo.Current.Plant;

            cboSiteSearch.EditValue = UserInfo.Current.Plant;

            cboSite.EditValue = UserInfo.Current.Plant;

            ConditionsVisible = false;
        }

        #endregion InitializeInfo : 기초정보 초기화 초기화

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

        #endregion ToolMoveType ComboBox  초기화

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

        #endregion IsReceipt ComboBox  초기화

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

        #endregion InitializeSendArea : 출고작업장 제어



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

        #endregion InitializeReceiptArea : 입고작업장 제어



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

        #endregion InitializeSendAreaSearch : 출고작업장 (입력을 위한 검색용) 제어



        #region InitializeAreaPopupColumnInDetailGrid - 작업장을 팝업검색할 필드 설정

        /// <summary>
        /// grid ProcesssegmentidLis
        /// </summary>
        private void InitializeAreaPopupColumnInDetailGrid()
        {
            _popupGridToolArea = grdToolMoveInput.View.AddSelectPopupColumn("RECEIPTAREA", 180, new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"CURRENTLOGINID={UserInfo.Current.Id}", "ISMOD=Y"))
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
                        grdToolMoveInput.View.SetFocusedRowCellValue("ISMODIFY", row["ISMODIFY"]);
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

        #endregion InitializeAreaPopupColumnInDetailGrid - 작업장을 팝업검색할 필드 설정

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            cboReceiptSiteInput.EditValueChanged += CboReceiptSiteInput_EditValueChanged;
            cboToolMoveTypeInput.EditValueChanged += CboToolMoveTypeInput_EditValueChanged;
            grdSendMoveToolStatus.View.CheckStateChanged += grdSendMoveToolStatus_CheckStateChanged;
            grdToolMoveInput.View.CellValueChanged += grdToolMoveInput_CellValueChanged;
            cboReceiptSite.EditValueChanged += CboReceiptSite_EditValueChanged;

            cboSite.EditValueChanged += CboSite_EditValueChanged;

            tabMakeReceipt.SelectedPageChanged += TabMakeReceipt_SelectedPageChanged;

            Shown += SendMoveTool_Shown;

            txtProductdef.TextChanged += (s, e) => grdToolMoveInput.View.ActiveFilterString = string.Concat("Contains([PRODUCTDEFID], '", txtProductdef.Text, "')");
        }

        #region CboReceiptSite_EditValueChanged - 입고Site변경시 이벤트

        private void CboReceiptSite_EditValueChanged(object sender, EventArgs e)
        {
            if (cboReceiptSite.EditValue != null)
            {
                _receiptSiteInput = cboReceiptSite.EditValue.ToString();
                receiptAreaCondition.SearchQuery =
                   new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={_receiptSiteInput}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CURRENTLOGINID={UserInfo.Current.Id}");
            }
        }

        #endregion CboReceiptSite_EditValueChanged - 입고Site변경시 이벤트

        #region CboSite_EditValueChanged - Site변경시 이벤트

        private void CboSite_EditValueChanged(object sender, EventArgs e)
        {
            ChangeSiteConditionForResult();
        }

        #endregion CboSite_EditValueChanged - Site변경시 이벤트

        #region TabMakeReceipt_SelectedPageChanged - 탭변경 이벤트

        private void TabMakeReceipt_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (tabMakeReceipt.SelectedTabPage == tabPageMakeReceipt)
            {
                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Visible = false;
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Cancel"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Cancel"].Visible = true;
                }
            }
            else
            {
                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Visible = true;
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Cancel"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Cancel"].Visible = false;
                }
            }
        }

        #endregion TabMakeReceipt_SelectedPageChanged - 탭변경 이벤트

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
                if (pnlToolbar.Controls["layoutToolbar"].Controls["Cancel"] != null)
                    pnlToolbar.Controls["layoutToolbar"].Controls["Cancel"].Visible = true;
            }
        }

        private void ConditionPlant_EditValueChanged(object sender, EventArgs e)
        {
            ChangeSiteCondition();
        }

        #endregion SendMoveTool_Shown - ToolBar 및 Site관련 설정을 화면 로딩후에 일괄 적용

        #region grdToolMoveInput_CellValueChanged - 출고그리드의 입고작업장 값 변경시 이벤트

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

        #endregion grdToolMoveInput_CellValueChanged - 출고그리드의 입고작업장 값 변경시 이벤트

        #region CboToolMoveTypeInput_EditValueChanged - 치공구출고 이동타입변경 이벤트

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

        #endregion CboToolMoveTypeInput_EditValueChanged - 치공구출고 이동타입변경 이벤트

        #region grdSendMoveToolStatus_CheckStateChanged - 그리드 체크박스변경이벤트

        private void grdSendMoveToolStatus_CheckStateChanged(object sender, EventArgs e)
        {
            //이미 입고된 데이터는 체크할 수 없다.
            if (!grdSendMoveToolStatus.View.GetFocusedDataRow().GetString("RECEIPTDATE").Equals(""))
            {
                grdSendMoveToolStatus.View.SetFocusedRowCellValue("_INTERNAL_CHECKMARK_SELECTION_", false);
            }
        }

        #endregion grdSendMoveToolStatus_CheckStateChanged - 그리드 체크박스변경이벤트

        #region CboReceiptSiteInput_EditValueChanged - 입고작업장 변경이벤트

        private void CboReceiptSiteInput_EditValueChanged(object sender, EventArgs e)
        {
            if (cboReceiptSiteInput.EditValue != null)
            {
                _receiptSiteInput = cboReceiptSiteInput.EditValue.ToString();
                _popupGridToolArea.SearchQuery =
                   new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={_receiptSiteInput}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CURRENTLOGINID={UserInfo.Current.Id}", "ISMOD=Y");
            }
        }

        #endregion CboReceiptSiteInput_EditValueChanged - 입고작업장 변경이벤트

        #endregion Event

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
            {
                SaveData();
            }
            else if (btn.Name.ToString().Equals("Cancel"))
            {
                DeleteData();
            }
            else if (btn.Name.ToString().Equals("Search"))
            {
                if (tabMakeReceipt.SelectedTabPage == tabPageMakeReceipt)
                {
                    Search();
                }
                else
                {
                    SearchSendTarget();
                }
            }
        }

        #endregion 툴바

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

            #endregion 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolRepairResult = SqlExecuter.Query("GetMoveToolStatusListByTool", "10001", values);

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

        #endregion OnSearchAsync - 검색

        #region Search - 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async void Search()
        {
            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);
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
                    values.Add("SENDDATE_PERIODFR", sendDateFr.ToString("yyyy-MM-dd"));
                }

                if (ValidateEditValue(deSendDateTo.EditValue))
                {
                    DateTime sendDateTo = Convert.ToDateTime(deSendDateTo.EditValue);
                    values.Add("SENDDATE_PERIODTO", sendDateTo.ToString("yyyy-MM-dd"));
                }

                if (ValidateEditValue(txtToolNo.EditValue))
                    values.Add("DURABLELOTID", txtToolNo.EditValue);

                if (ValidateEditValue(cboIsReceipt.EditValue))
                    values.Add("ISRECEIPT", cboIsReceipt.EditValue);

                if (ValidateEditValue(cboReceiptSite.EditValue))
                    values.Add("TOPLANTID", cboReceiptSite.EditValue);
                values = Commons.CommonFunction.ConvertParameter(values);
                DataTable toolMoveList = await SqlExecuter.QueryAsync("GetMoveToolStatusListByTool", "10001", values);

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
            catch (Exception ex)
            {
                throw MessageException.Create(Format.GetString(ex));
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);
            }
        }

        #endregion Search - 검색

        #region SearchSendTarget - 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected void SearchSendTarget()
        {
            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);
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

                values = Commons.CommonFunction.ConvertParameter(values);
                DataTable toolMoveList = SqlExecuter.Query("GetMoveToolInputListByTool", "10001", values);

                grdToolMoveInput.DataSource = toolMoveList;

                if (toolMoveList.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                _currentStatus = "modified";
            }
            catch (Exception ex)
            {
                throw MessageException.Create(Format.GetString(ex));
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);
            }
        }

        #endregion SearchSendTarget - 검색

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

        #endregion 조회조건 제어

        #endregion 검색

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }

        #region ValidateContent - 내용점검

        private bool ValidateContent(out string messageCode)
        {
            messageCode = "";

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

                if (currentRow.GetString("ISMODIFY").Equals("N"))
                {
                    messageCode = "ValidateToolMoveReqSec"; //입고작업장중 권한이 없는 작업장이 있습니다.
                    return false;
                }
            }

            return true;
        }

        #endregion ValidateContent - 내용점검

        #region ValidateComboBoxEqualValue - 2개의 콤보박스를 비교

        private bool ValidateComboBoxEqualValue(SmartComboBox originBox, SmartComboBox targetBox)
        {
            //두 콤보박스의 값이 같으면 안된다.
            if (originBox.EditValue.Equals(targetBox.EditValue))
                return false;
            return true;
        }

        #endregion ValidateComboBoxEqualValue - 2개의 콤보박스를 비교

        #region ValidateNumericBox - 숫자형텍스트박스의 값 점검 및 기준숫자보다 높은지 점검

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

        #endregion ValidateNumericBox - 숫자형텍스트박스의 값 점검 및 기준숫자보다 높은지 점검

        #region ValidateEditValue - 입력된 값 점검

        private bool ValidateEditValue(object editValue)
        {
            if (editValue == null)
                return false;

            if (editValue.ToString() == "")
                return false;

            return true;
        }

        #endregion ValidateEditValue - 입력된 값 점검

        #region ValidateCellInGrid - 그리드내 특정컬럼의 값 점검

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

        #endregion ValidateCellInGrid - 그리드내 특정컬럼의 값 점검

        #region ValidateCompareCellInGrid - 그리드내의 특정 컬럼2개의 값을 비교

        private bool ValidateCompareCellInGrid(DataTable currentTable, string firstColumnName, string secondColumnName)
        {
            foreach (DataRow currentRow in currentTable.Rows)
                if (currentRow.GetString(firstColumnName).Equals(currentRow.GetString(secondColumnName)))
                    return false;

            return true;
        }

        #endregion ValidateCompareCellInGrid - 그리드내의 특정 컬럼2개의 값을 비교

        #region ValidateProductCode : 품목코드와 관계된 치공구를 부르기 위해 품목코드가 입력되었는지 확인한다.

        private bool ValidateProductCode()
        {
            return true;
        }

        #endregion ValidateProductCode : 품목코드와 관계된 치공구를 부르기 위해 품목코드가 입력되었는지 확인한다.

        #endregion 유효성 검사

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

                //이동구분
                cboToolMoveTypeInput.EditValue = null;

                _sendUserID = UserInfo.Current.Id;

                cboReceiptSiteInput.EditValue = null;
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

        #endregion InitializeInsertForm : 입고등록하기 위한 화면 초기화

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
                cboReceiptSiteInput.ReadOnly = false;
                cboToolMoveTypeInput.ReadOnly = false;
            }
            else if (currentStatus == "modified") //
            {
                popEditSendArea.ReadOnly = false;
                cboReceiptSiteInput.ReadOnly = false;
                cboToolMoveTypeInput.ReadOnly = false;
            }
            else
            {
                popEditSendArea.ReadOnly = false;
                cboReceiptSiteInput.ReadOnly = true;
                cboToolMoveTypeInput.ReadOnly = true;
            }
        }

        #endregion controlEnableProcess : 입력/수정/삭제를 위한 화면내 컨트롤들의 Enable 제어

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

            if (useState)
                dt.Columns.Add("_STATE_");

            return dt;
        }

        #endregion CreateSaveDatatable : ToolMove 입력/수정/삭제를 위한 DataTable의 Template 생성

        #region SaveData : 입력/수정을 수행

        private void SaveData()
        {
            //Validation 체크 부분 작성 필요
            this.ShowWaitArea();

            //저장 로직
            try
            {
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

                                toolMoveRow["TOOLNUMBER"] = moveTargetRow.GetString("TOOLNUMBER");
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
            }
        }

        #endregion SaveData : 입력/수정을 수행

        #region DeleteData : 삭제를 진행

        private void DeleteData()
        {
            //Validation 체크 부분 작성 필요
            this.ShowWaitArea();

            //저장 로직
            try
            {
                if (grdSendMoveToolStatus.View.GetCheckedRows().Rows.Count > 0)
                {
                    DataSet toolMoveSet = new DataSet();

                    DataTable toolMoveTable = CreateSaveDatatable(true);

                    DataTable deleteTargetTable = grdSendMoveToolStatus.View.GetCheckedRows();

                    foreach (DataRow deleteTargetRow in deleteTargetTable.Rows)
                    {
                        DataRow toolMoveRow = toolMoveTable.NewRow();

                        toolMoveRow["TOOLNUMBER"] = deleteTargetRow.GetString("TOOLNUMBER");
                        toolMoveRow["AREAID"] = deleteTargetRow.GetString("SENDAREAID");
                        toolMoveRow["SENDDATE"] = Convert.ToDateTime(deleteTargetRow.GetString("SENDDATE")).ToString("yyyy-MM-dd HH:mm:ss");

                        toolMoveRow["CREATOR"] = _sendUserID;
                        toolMoveRow["MODIFIER"] = _sendUserID;

                        //로직상 무조건 삭제만 진행된다.
                        toolMoveRow["VALIDSTATE"] = "Invalid";
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
            }
        }

        #endregion DeleteData : 삭제를 진행

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

        #endregion DisplayToolMoveInfo : 그리드에서 행 선택시 상세정보를 표시

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

        #endregion DisplayToolMoveInfoDetail : 입력받은 인덱스의 그리드의 내용을 화면에 바인딩

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

        #endregion GetSingleReceiptArea : 사용자가 입력한 입고작업장으로 검색 (클립보드의 데이터를 이용)

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

        #endregion ChangeSiteCondition - 검색조건의 Site변경시 관련된 쿼리들을 변경

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
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Cancel"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Cancel"].Enabled = true;
                }
            }
            else
            {
                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Cancel"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Cancel"].Enabled = false;
                }
            }
        }

        #endregion ChangeSiteConditionForResult - 검색조건의 Site변경시 관련된 쿼리들을 변경

        #region SetPlantFromSystem - 시스템으로 설정된 Site의 정보를 바인딩

        private void SetPlantFromSystem()
        {
            //cboSiteSearch
            cboSiteSearch.DataSource = ((SmartComboBox)Conditions.GetControl("P_PLANTID")).DataSource;

            cboSite.DataSource = ((SmartComboBox)Conditions.GetControl("P_PLANTID")).DataSource;
        }

        #endregion SetPlantFromSystem - 시스템으로 설정된 Site의 정보를 바인딩

        #endregion Private Function
    }
}