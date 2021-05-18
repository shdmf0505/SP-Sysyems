#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품목관리 > 마케팅 제품정보
    /// 업  무  설  명  : 마케팅 제품정보를 등록, 수정한다.
    /// 생    성    자  : 한주석
    /// 생    성    일  : 2020-04-23
    /// 수  정  이  력  :
    ///     2021.02.15 전우성 : 코드 정리 및 Row 존재하지 않을 시 오류 수정
    ///
    /// </summary>
    public partial class MarketingProductInfo : SmartConditionManualBaseForm
    {
        #region 생성자

        public MarketingProductInfo()
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

            InitializeGrid();
            InitializeControl();
            InitializeEvent();
        }

        /// <summary>
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            grdMain.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore | GridButtonItem.Export | GridButtonItem.Import;

            //회사ID
            grdMain.View.AddTextBoxColumn("ENTERPRISEID", 70).SetIsHidden();
            //제품코드
            grdMain.View.AddTextBoxColumn("ITEMID", 100).SetTextAlignment(TextAlignment.Center);
            //내부REV
            grdMain.View.AddTextBoxColumn("ITEMVERSION", 70).SetLabel("PRODUCTDEFVERSION").SetTextAlignment(TextAlignment.Center);
            //제품명
            grdMain.View.AddTextBoxColumn("ITEMNAME", 200).SetLabel("PRODUCTDEFNAME");
            //제품TYPE
            grdMain.View.AddTextBoxColumn("PRODUCTTYPE", 100).SetTextAlignment(TextAlignment.Center);
            //층수
            grdMain.View.AddComboBoxColumn("LAYER", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Layer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            //고객코드/고객명
            grdMain.View.AddTextBoxColumn("CUSTOMERCODENAME", 200);
            //제품등급
            grdMain.View.AddComboBoxColumn("PRODUCTRATING", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductLevel", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetLabel("PRODUCTLEVEL").SetTextAlignment(TextAlignment.Center);
            //생산구분
            grdMain.View.AddComboBoxColumn("PRODUCTIONTYPE", 70, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            //품목단위
            grdMain.View.AddTextBoxColumn("UOM", 80).SetLabel("ITEMUOM").SetTextAlignment(TextAlignment.Center);
            //주제조공장
            grdMain.View.AddTextBoxColumn("MAINFACTORY", 100).SetTextAlignment(TextAlignment.Center);
            //사양담당
            grdMain.View.AddTextBoxColumn("SPECPERSONNAME", 100).SetLabel("SPECOWNERNAME").SetTextAlignment(TextAlignment.Center);
            //영업담당
            grdMain.View.AddTextBoxColumn("SALESOWNERNAME", 100).SetTextAlignment(TextAlignment.Center);
            //거래처 품목코드
            grdMain.View.AddTextBoxColumn("CUSTOMERPRODUCTCODE", 100).SetLabel("CUSTOMERITEMID").SetTextAlignment(TextAlignment.Center);
            //거래처 REV
            grdMain.View.AddTextBoxColumn("CUSTOMERPRODUCTVERSION", 80).SetLabel("CUSTOMERITEMVERSION").SetTextAlignment(TextAlignment.Center);
            //품목계정
            grdMain.View.AddComboBoxColumn("ITEMACCOUNT", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ItemAccount", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            //품목종류
            grdMain.View.AddComboBoxColumn("ITEMCLASS", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ItemClass2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            //BASIC CODE
            grdMain.View.AddTextBoxColumn("BASICCODE", 100).SetTextAlignment(TextAlignment.Center);
            //판매범주
            grdMain.View.AddSelectPopupColumn("SALEORDERCATEGORY", 100, new SqlQuery("GetCategoryPopup", "10001", $"TOPPARENTCATEGORYID={"Sales"}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdMain.View.AddSpinEditColumn("PNLSIZEXAXIS", 100).SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddSpinEditColumn("PNLSIZEYAXIS", 100).SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddSpinEditColumn("ARRAYPCSPNL", 100).SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddSpinEditColumn("CALCULATION2", 100).SetTextAlignment(TextAlignment.Right);

            //QR사업부정보
            grdMain.View.AddComboBoxColumn("QRBUSINESSINFO", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=QRBusinessInfo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            //QR사업부 SUB
            grdMain.View.AddComboBoxColumn("QRBUSINESSSUB", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=QRBusinessSub", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            //QR생산구분
            grdMain.View.AddComboBoxColumn("QRPRODUCTIONTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=QRProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            //QR자재REV
            grdMain.View.AddTextBoxColumn("QRMATERIALREV", 80).SetTextAlignment(TextAlignment.Center);
            //APN 입력
            grdMain.View.AddTextBoxColumn("INPUTAPN", 100).SetTextAlignment(TextAlignment.Center);
            //신규 DATA접수
            grdMain.View.AddComboBoxColumn("RECEIVENEWDATA", 70, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            //고객 모델명
            grdMain.View.AddTextBoxColumn("CUSTOMERMODELNAME", 150).SetTextAlignment(TextAlignment.Center);
            //판매구분
            grdMain.View.AddComboBoxColumn("SALESTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=SalesType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            //견적수율
            grdMain.View.AddTextBoxColumn("ESTIMATEYIELD", 100).SetTextAlignment(TextAlignment.Center);
            //수주처
            grdMain.View.AddSelectPopupColumn("CONTRACTORNAME", 100, new SqlQuery("GetCustomerList", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}")).SetLabel("CONTRACTOR");
            //납품처
            grdMain.View.AddSelectPopupColumn("SHIPTONAME", 100, new SqlQuery("GetCustomerList", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}")).SetLabel("SHIPTO");
            //매출처
            grdMain.View.AddSelectPopupColumn("BILLTONAME", 100, new SqlQuery("GetCustomerList", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}")).SetLabel("BILLTO");
            //단종구분
            grdMain.View.AddComboBoxColumn("ENDTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DiscontinueType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            //비고
            grdMain.View.AddTextBoxColumn("REMARK", 200);

            grdMain.View.PopulateColumns();

            grdMain.View.SetIsReadOnly();
            grdMain.ShowStatusBar = true;
        }

        /// <summary>
        /// 컨텐츠 초기화
        /// </summary>
        private void InitializeControl()
        {
            #region 사양담당, 영업담당 사용자 Control

            ConditionItemSelectPopup managerSelectPopup = new ConditionItemSelectPopup();
            managerSelectPopup.Id = "USERID";
            managerSelectPopup.SearchQuery = new SqlQuery("GetUserList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            managerSelectPopup.ValueFieldName = "USERID";
            managerSelectPopup.DisplayFieldName = "USERNAME";
            managerSelectPopup.SetPopupLayout("SELECTUSER", PopupButtonStyles.Ok_Cancel, true, true);
            managerSelectPopup.SetPopupResultCount(0);
            managerSelectPopup.SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow);
            managerSelectPopup.SetPopupAutoFillColumns("DEPARTMENT");

            managerSelectPopup.Conditions.AddTextBox("USERIDNAME");
            managerSelectPopup.GridColumns.AddTextBoxColumn("USERID", 150);
            managerSelectPopup.GridColumns.AddTextBoxColumn("USERNAME", 200);
            managerSelectPopup.GridColumns.AddTextBoxColumn("DEPARTMENT", 60);

            smartSelectPopupEditSpecMen.SelectPopupCondition = managerSelectPopup;
            smartSelectPopupEditSalesMen.SelectPopupCondition = managerSelectPopup;

            #endregion 사양담당, 영업담당 사용자 Control

            #region 판매범주

            ConditionItemSelectPopup categorySelectPopup = new ConditionItemSelectPopup();
            categorySelectPopup.Id = "CATEGORYID";
            categorySelectPopup.SearchQuery = new SqlQuery("GetCategoryPopup", "10001", $"TOPPARENTCATEGORYID={"Sales"}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            categorySelectPopup.ValueFieldName = "CATEGORYID";
            categorySelectPopup.DisplayFieldName = "CATEGORYNAME";
            categorySelectPopup.SetPopupLayout("CATEGORYID", PopupButtonStyles.Ok_Cancel, true, true);
            categorySelectPopup.SetPopupResultCount(0);
            categorySelectPopup.SetPopupResultMapping("SALEORDERCATEGORY", "CATEGORYID");
            categorySelectPopup.SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow);

            categorySelectPopup.Conditions.AddComboBox("PARENTCATEGORYID", new SqlQuery("GetCategoryMidList", "10001", $"TOPPARENTCATEGORYID={"Sales"}"), "CATEGORYNAME", "CATEGORYID").SetValidationIsRequired();
            categorySelectPopup.Conditions.AddTextBox("CATEGORYNAME");

            categorySelectPopup.GridColumns.AddTextBoxColumn("CATEGORYID", 150);
            categorySelectPopup.GridColumns.AddTextBoxColumn("CATEGORYNAME", 200);

            popcategory.SelectPopupCondition = categorySelectPopup;

            #endregion 판매범주

            #region 수저처, 납품처, 매출처 Control

            ConditionItemSelectPopup cisidvendorCode = new ConditionItemSelectPopup();
            cisidvendorCode.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            cisidvendorCode.SetPopupLayout("CUSTOMERID", PopupButtonStyles.Ok_Cancel);

            cisidvendorCode.Id = "CUSTOMERID";
            cisidvendorCode.LabelText = "CUSTOMERID";
            cisidvendorCode.SearchQuery = new SqlQuery("GetCustomerList", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            cisidvendorCode.IsMultiGrid = false;
            cisidvendorCode.DisplayFieldName = "CUSTOMERNAME";
            cisidvendorCode.ValueFieldName = "CUSTOMERID";
            cisidvendorCode.LanguageKey = "CUSTOMERID";

            cisidvendorCode.Conditions.AddTextBox("TXTCUSTOMERID");
            cisidvendorCode.GridColumns.AddTextBoxColumn("CUSTOMERID", 80);
            cisidvendorCode.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 150);
            cisidvendorCode.GridColumns.AddTextBoxColumn("ADDRESS", 250);
            cisidvendorCode.GridColumns.AddTextBoxColumn("CEONAME", 100);
            cisidvendorCode.GridColumns.AddTextBoxColumn("TELNO", 100);
            cisidvendorCode.GridColumns.AddTextBoxColumn("FAXNO", 100);

            popcontractor.SelectPopupCondition = cisidvendorCode;
            popbillto.SelectPopupCondition = cisidvendorCode;
            popshipto.SelectPopupCondition = cisidvendorCode;

            #endregion 수저처, 납품처, 매출처 Control

            #region 고객명 Control

            ConditionItemSelectPopup cisidvendorCode2 = new ConditionItemSelectPopup();
            cisidvendorCode2.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            cisidvendorCode2.SetPopupLayout("CUSTOMERID", PopupButtonStyles.Ok_Cancel);

            cisidvendorCode2.Id = "CUSTOMERID";
            cisidvendorCode2.LabelText = "CUSTOMERID";
            cisidvendorCode2.SearchQuery = new SqlQuery("GetCustomerList", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            cisidvendorCode2.IsMultiGrid = false;
            cisidvendorCode2.DisplayFieldName = "CUSTOMER";
            cisidvendorCode2.ValueFieldName = "CUSTOMER";
            cisidvendorCode2.LanguageKey = "CUSTOMERID";

            cisidvendorCode2.Conditions.AddTextBox("TXTCUSTOMERID");
            cisidvendorCode2.GridColumns.AddTextBoxColumn("CUSTOMERID", 80);
            cisidvendorCode2.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 150);
            cisidvendorCode2.GridColumns.AddTextBoxColumn("ADDRESS", 250);
            cisidvendorCode2.GridColumns.AddTextBoxColumn("CEONAME", 100);
            cisidvendorCode2.GridColumns.AddTextBoxColumn("TELNO", 100);
            cisidvendorCode2.GridColumns.AddTextBoxColumn("FAXNO", 100);

            popcustomername.SelectPopupCondition = cisidvendorCode2;

            #endregion 고객명 Control

            #region ComboBox 설정

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "CODECLASSID", "ProductionType" },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType }
            };

            SetSmartComboBox(cboProductionType, SqlExecuter.Query("GetTypeList", "10001", param));

            param["CODECLASSID"] = "ItemAccount";
            SetSmartComboBox(cbodtItemAccount, SqlExecuter.Query("GetTypeList", "10001", param));

            param["CODECLASSID"] = "ItemClass2";
            SetSmartComboBox(cboItemClass, SqlExecuter.Query("GetTypeList", "10001", param));

            param["CODECLASSID"] = "YesNo";
            SetSmartComboBox(cbNewDataValid, SqlExecuter.Query("GetTypeList", "10001", param));

            param["CODECLASSID"] = "Layer";
            SetSmartComboBox(cbLayer, SqlExecuter.Query("GetTypeList", "10001", param));

            param["CODECLASSID"] = "QRProductionType";
            SetSmartComboBox(cboqrproducttype, SqlExecuter.Query("GetTypeList", "10001", param));

            param["CODECLASSID"] = "QRBusinessSub";
            SetSmartComboBox(cboqrbusinesssub, SqlExecuter.Query("GetTypeList", "10001", param));

            param["CODECLASSID"] = "QRBusinessInfo";
            SetSmartComboBox(cboqrbusinessinfo, SqlExecuter.Query("GetTypeList", "10001", param));

            param["CODECLASSID"] = "DiscontinueType";
            SetSmartComboBox(cboendtype, SqlExecuter.Query("GetTypeList", "10001", param));

            param["CODECLASSID"] = "SalesType";
            SetSmartComboBox(cbosalestype, SqlExecuter.Query("GetTypeList", "10001", param));

            #endregion ComboBox 설정
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            grdMain.View.FocusedRowChanged += (s, e) => DisplayRow();
            grdMain.View.ColumnFilterChanged += (s, e) => DisplayRow();
        }

        #endregion Event

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            ExecuteRule("SaveMarketingProductInfo", grdMain.GetChangedRows());
        }

        #endregion 툴바

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            grdMain.View.ClearDatas();

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);

            if (!string.IsNullOrEmpty(Format.GetString(values["P_ITEMID"])))
            {
                values.Add("ITEMID", Format.GetString(values["P_ITEMID"]).Split('|')[0]);
                values.Add("ITEMVERSION", Format.GetString(values["P_ITEMID"]).Split('|')[1]);
            }

            if (SqlExecuter.Query("SelectCustomerProductSpec", "10001", values) is DataTable dt)
            {
                grdMain.DataSource = dt;

                if (dt.Rows.Count.Equals(0))
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            #region 품목코드

            var condition = Conditions.AddSelectPopup("P_ITEMID", new SqlQuery("GetItemMasterList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "ITEM", "ITEM")
                                      .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                                      .SetPopupLayoutForm(800, 800)
                                      .SetPopupAutoFillColumns("ITEMNAME")
                                      .SetLabel("ITEMID")
                                      .SetPosition(1.2)
                                      .SetPopupResultCount(1);

            condition.Conditions.AddTextBox("TXTITEM");

            condition.GridColumns.AddTextBoxColumn("ITEMID", 150);
            condition.GridColumns.AddTextBoxColumn("ITEMNAME", 200);
            condition.GridColumns.AddTextBoxColumn("ITEMVERSION", 100);

            #endregion 품목코드
        }

        #endregion 검색

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            if (grdMain.View.RowCount.Equals(0))
            {
                throw MessageException.Create("NoSaveData");
            }

            GetControlsFrom.GetControlsPanelControlDelGrid(tlpProductInfo, grdMain);
        }

        #endregion 유효성 검사

        #region Private Function

        /// <summary>
        /// Combo 초기화 공통
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="dt"></param>
        private void SetSmartComboBox(SmartComboBox comboBox, DataTable dt)
        {
            comboBox.DisplayMember = "CODENAME";
            comboBox.ValueMember = "CODEID";
            comboBox.ShowHeader = false;
            comboBox.UseEmptyItem = true;
            comboBox.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;

            comboBox.DataSource = dt.Copy();
        }

        /// <summary>
        ///
        /// </summary>
        private void DisplayRow()
        {
            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);

                // 속도향상을 위해 화면 드로잉 정지
                CommonFunction.SuspendDrawing(this);

                if (grdMain.View.GetFocusedDataRow() is DataRow row)
                {
                    CommonFunctionProductSpec.SearchDataBind(row, smartGroupBox1);
                    if (string.IsNullOrEmpty(Format.GetString(row["ENDTYPE"])))
                    {
                        cboendtype.EditValue = "0";
                    }
                }
                else
                {
                    GetControlsFrom.SetControlsEmpty(tlpProductInfo);
                }
            }
            finally
            {
                CommonFunction.ResumeDrawing(this);
                DialogManager.CloseWaitArea(this.pnlContent);
            }
        }

        #endregion Private Function
    }
}