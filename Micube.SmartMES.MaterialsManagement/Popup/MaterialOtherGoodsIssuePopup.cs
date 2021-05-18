#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.MaterialsManagement.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 자재 기타 출고popup
    /// 업  무  설  명  : 자재 기타 출고 리스트 조히
    /// 생    성    자  : 최별
    /// 생    성    일  : 2019-08-05
    /// 수  정  이  력  :
    ///     2021.03.22 전우성 화면 최적화 변경. 소스 최적화. 조회조건 미처리되는 부분 수정. 자재ID selectpopup으로 변경
    ///
    /// </summary>
    public partial class MaterialOtherGoodsIssuePopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        public DataRow CurrentDataRow { get; set; }

        #region delegate Variables

        /// <summary>
        ///  Handler
        /// </summary>
        /// <param name="dt"></param>
        public delegate void chat_room_evecnt_handler(DataRow row);
        public event chat_room_evecnt_handler WriteHandler;

        #endregion delegate Variables

        #region 생성자

        /// <summary>
        ///
        /// </summary>
        /// <param name="SPlantid"></param>
        /// <param name="SAreaid"></param>
        /// <param name="SAreaName"></param>
        public MaterialOtherGoodsIssuePopup(string SPlantid, string SAreaid, string SAreaName)
        {
            InitializeComponent();

            InitializeComboBox();

            InitializeEvent();

            SelectOspAreaidPopup(SPlantid);

            if (!SAreaid.Equals(string.Empty))
            {
                popupOspAreaid.SetValue(SAreaid);
                popupOspAreaid.Text = SAreaName;
                popupOspAreaid.EditValue = SAreaName;
            }

            OnPlantidInformationSearch(cboPlantid.EditValue.ToString());

            InitializeGrid();

            SelectTransactionreasoncodePopup();
        }

        #endregion 생성자

        #region POPUP

        /// <summary>
        /// 거래사유  팝업
        /// </summary>
        private void SelectTransactionreasoncodePopup()
        {
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();
            popup.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("TRANSACTIONREASONCODE", PopupButtonStyles.Ok_Cancel, true, false);
            popup.Id = "TRANSACTIONREASONCODE";
            popup.LabelText = "TRANSACTIONREASONCODE";
            popup.SearchQuery = new SqlQuery("GetTransactionreasoncodeByCsm", "10001", $"P_PLANTID={cboPlantid.EditValue}"
                                                                                  , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                                                                                  , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                                  , $"P_TRANSACTIONCODE={"MISC_ISSUE"}"
                                                                                  );
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "TRANSACTIONREASONCODE";

            popup.ValueFieldName = "TRANSACTIONREASONCODE";
            popup.LanguageKey = "TRANSACTIONREASONCODE";
            popup.Conditions.AddTextBox("TRANSACTIONREASONCODENAME");

            popup.GridColumns.AddTextBoxColumn("TRANSACTIONREASONCODE", 150).SetLabel("TRANSACTIONREASONCODE");
            popup.GridColumns.AddTextBoxColumn("TRANSACTIONREASONCODENAME", 200).SetLabel("TRANSACTIONREASONCODENAME");

            popupTransactionreasoncode.SelectPopupCondition = popup;

            #region 자재

            ConditionItemSelectPopup popup2 = new ConditionItemSelectPopup();

            popup2.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            popup2.SetPopupLayout("CONSUMABLEDEFID", PopupButtonStyles.Ok_Cancel, true, false);
            popup2.Id = "CONSUMABLEDEFID";
            popup2.LabelText = "CONSUMABLEDEFID";
            popup2.SearchQuery = new SqlQuery("GetConsumableDefinition", "10001", $"PLANTID={cboPlantid.EditValue}"
                                                                                  , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                                                                                  , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                                  );
            popup2.IsMultiGrid = false;
            popup2.DisplayFieldName = "CONSUMABLEDEFID";
            popup2.ValueFieldName = "CONSUMABLEDEFID";

            popup2.Conditions.AddTextBox("P_CONSUMABLEIDNAME").SetLabel("CONSUMABLEIDNAME");

            popup2.GridColumns.AddTextBoxColumn("CONSUMABLEDEFID", 120);
            popup2.GridColumns.AddTextBoxColumn("CONSUMABLEDEFVERSION", 80);
            popup2.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 180);

            txtConsumabledefid.SelectPopupCondition = popup2;

            #endregion 자재
        }

        #endregion POPUP

        #region ComboBox

        /// <summary>
        /// SmartComboBox 컨트롤 추가
        /// </summary>
        private void InitializeComboBox()
        {
            // plantid
            cboPlantid.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboPlantid.ValueMember = "PLANTID";
            cboPlantid.DisplayMember = "PLANTID";
            cboPlantid.EditValue = UserInfo.Current.Plant.ToString();
            cboPlantid.DataSource = SqlExecuter.Query("GetPlantList", "00001"
                                                     , new Dictionary<string, object>() { { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });

            cboPlantid.ShowHeader = false;

            SelectOspAreaidPopup(UserInfo.Current.Plant.ToString());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sPlantid"></param>
        private void SelectOspAreaidPopup(string sPlantid)
        {
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();
            popup.SetPopupLayoutForm(520, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("AREANAME", PopupButtonStyles.Ok_Cancel, true, false);
            popup.Id = "AREANAME";
            popup.LabelText = "AREANAME";
            popup.SearchQuery = new SqlQuery("GetAreaidListByCsm", "10001", $"P_ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                           , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                                                                           , $"P_PLANTID={sPlantid}");
            popup.IsMultiGrid = false;

            popup.DisplayFieldName = "AREANAME";
            popup.ValueFieldName = "AREAID";
            popup.LanguageKey = "AREAID";

            popup.Conditions.AddTextBox("P_AREANAME").SetLabel("AREANAME");
            popup.GridColumns.AddTextBoxColumn("AREAID", 120).SetLabel("AREAID");
            popup.GridColumns.AddTextBoxColumn("AREANAME", 200).SetLabel("AREANAME");

            popupOspAreaid.SelectPopupCondition = popup;
        }

        #endregion ComboBox

        #region 이벤트

        private void InitializeEvent()
        {
            // 검색
            btnSearch.Click += (s, e) =>
            {
                try
                {
                    DialogManager.ShowWaitArea(this);

                    Dictionary<string, object> Param = new Dictionary<string, object>
                    {
                        { "P_STARTDATE", dtpStartDate.Text },
                        { "P_ENDDATE", dtpEndDate.Text },
                        { "P_CONSUMABLEDEFID", txtConsumabledefid.GetValue() },
                        { "P_TRANSACTIONREASONCODE", popupTransactionreasoncode.GetValue().ToString() },
                        { "P_ENTERPRISEID", UserInfo.Current.Enterprise.ToString() },
                        { "P_TRANSACTIONTYPE", "EtcOutbound" },
                        { "P_TRANSACTIONCODE", "MISC_ISSUE" },
                        { "P_PLANTID", cboPlantid.EditValue },
                        { "P_TOAREAID", popupOspAreaid.GetValue() },
                        { "USERID", UserInfo.Current.Id },
                        { "LANGUAGETYPE", UserInfo.Current.LanguageType }
                    };

                    Param = Commons.CommonFunction.ConvertParameter(Param);

                    if(SqlExecuter.Query("GetMaterialOtherGoodsIssuePopup", "10001", Param) is DataTable dt)
                    {

                        if (dt.Rows.Count.Equals(0))
                        {
                            ShowMessage("NoSelectData");
                            return;
                        }

                        grdMaterialReceiptPopup.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    throw MessageException.Create(Format.GetString(ex));
                }
                finally
                {
                    DialogManager.CloseWaitArea(this);
                }
            };

            btnConfirm.Click += (s, e) =>
            {
                DataRow row = grdMaterialReceiptPopup.View.GetFocusedDataRow();
                CurrentDataRow = row;
                WriteHandler(row);
                this.Close();
            };

            cboPlantid.EditValueChanged += (s, e) =>
            {
                SelectOspAreaidPopup(cboPlantid.EditValue.ToString());
                OnPlantidInformationSearch(cboPlantid.EditValue.ToString());
            };

            // 단기
            btnClose.Click += (s, e) => this.Close();
        }

        #endregion 이벤트

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdMaterialReceiptPopup.GridButtonItem = GridButtonItem.Export;
            grdMaterialReceiptPopup.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdMaterialReceiptPopup.View.AddTextBoxColumn("ENTERPRISEID", 150).SetIsHidden();
            grdMaterialReceiptPopup.View.AddTextBoxColumn("PLANTID", 150).SetIsHidden();
            grdMaterialReceiptPopup.View.AddTextBoxColumn("TRANSACTIONSEQUENCE", 80).SetIsHidden();
            grdMaterialReceiptPopup.View.AddTextBoxColumn("FROMAREAID", 150).SetIsHidden();
            grdMaterialReceiptPopup.View.AddTextBoxColumn("TRANSACTIONCODE", 150).SetIsHidden();
            grdMaterialReceiptPopup.View.AddTextBoxColumn("ISMODIFY", 150).SetIsHidden();

            grdMaterialReceiptPopup.View.AddTextBoxColumn("TRANSACTIONNOISSUE", 120);
            grdMaterialReceiptPopup.View.AddTextBoxColumn("TRANSACTIONDATEISSUE", 150).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetDefault("");
            grdMaterialReceiptPopup.View.AddTextBoxColumn("CONSUMABLEDEFID", 100);
            grdMaterialReceiptPopup.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 80);
            grdMaterialReceiptPopup.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
            grdMaterialReceiptPopup.View.AddTextBoxColumn("CONSUMABLELOTID", 100);
            grdMaterialReceiptPopup.View.AddTextBoxColumn("AREANAME", 150);
            grdMaterialReceiptPopup.View.AddTextBoxColumn("TRANSACTIONREASONCODE", 100);
            grdMaterialReceiptPopup.View.AddTextBoxColumn("TRANSACTIONREASONCODENAME", 150);
            grdMaterialReceiptPopup.View.AddComboBoxColumn("TXNADDEDCODE", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ConsumableTxnAddedCode", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdMaterialReceiptPopup.View.AddTextBoxColumn("UNIT", 150);
            grdMaterialReceiptPopup.View.AddSpinEditColumn("QTY", 150).SetDisplayFormat("#,###,##0.#####", MaskTypes.Numeric);
            grdMaterialReceiptPopup.View.AddTextBoxColumn("MATREMARK", 300);

            grdMaterialReceiptPopup.View.PopulateColumns();
            grdMaterialReceiptPopup.View.SetIsReadOnly();
        }

        /// <summary>
        /// plant정보에서 WORKTIME
        /// </summary>
        private void OnPlantidInformationSearch(string sPlantid)
        {
            Dictionary<string, object> dicParam = new Dictionary<string, object>
            {
                { "P_PLANTID", sPlantid }
            };

            if (SqlExecuter.Query("GetPlantidInformatByCsm", "10001", dicParam) is DataTable dtPlantInfo)
            {
                if (dtPlantInfo.Rows.Count.Equals(1))
                {
                    DataRow drPlant = dtPlantInfo.Rows[0];

                    dtpStartDate.EditValue = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd ") + drPlant["WORKTIME"].ToString();
                    dtpEndDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd ") + drPlant["WORKTIME"].ToString();
                }
            }
        }
    }
}