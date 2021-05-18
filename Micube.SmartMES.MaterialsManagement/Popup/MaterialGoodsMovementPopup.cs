#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using Micube.Framework.SmartControls.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace Micube.SmartMES.MaterialsManagement.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 재 이동 등록popup
    /// 업  무  설  명  : 재 이동 등록 리스트 조히 
    /// 생    성    자  : 최별
    /// 생    성    일  : 2019-08-05
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class MaterialGoodsMovementPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        public DataRow CurrentDataRow { get; set; }
        #region Local Variables
        /// <summary>
        ///  Handler
        /// </summary>
        /// <param name="dt"></param>
        public delegate void chat_room_evecnt_handler(DataRow row);
        public event chat_room_evecnt_handler write_handler;
        private string _sWorkTime = "";
        #endregion

        #region 생성자
        public MaterialGoodsMovementPopup()
        {
            InitializeComponent();

            InitializeEvent();

            InitializeCondition();



        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SPlantid"></param>
        /// <param name="SAreaid"></param>

        public MaterialGoodsMovementPopup(string SPlantid, string SAreaid, string SAreaName)
        {
            InitializeComponent();

            InitializeComboBox();

            InitializeEvent();

            cboPlantid.EditValue = SPlantid;
            selectOspAreaidPopup(SPlantid);
            if (!(SAreaid.Equals("")))
            {

                popupOspAreaid.SetValue(SAreaid);
                popupOspAreaid.Text = SAreaName;
                popupOspAreaid.EditValue = SAreaName;
            }
            OnPlantidInformationSearch(cboPlantid.EditValue.ToString());
            InitializeCondition();

            InitializeGrid();

            InitializePopup();
        }

        #endregion
        #region POPUP  

        /// <summary>
        /// popup 컨트롤 추가
        /// </summary>
        private void InitializePopup()
        {
            // 1.거래사유 
            selectTransactionreasoncodePopup();
        }
        /// <summary>
        /// 거래사유  팝업
        /// </summary>
        private void selectTransactionreasoncodePopup()
        {
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();
            popup.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("TRANSACTIONREASONCODE", PopupButtonStyles.Ok_Cancel, true, false);
            popup.Id = "TRANSACTIONREASONCODE";
            popup.LabelText = "TRANSACTIONREASONCODE";
            popup.SearchQuery = new SqlQuery("GetTransactionreasoncodeByCsm", "10001", $"P_PLANTID={cboPlantid.EditValue}"
                                                                                  , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                                                                                  , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                                  );
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "TRANSACTIONREASONCODE";

            popup.ValueFieldName = "TRANSACTIONREASONCODE";
            popup.LanguageKey = "TRANSACTIONREASONCODE";
            popup.Conditions.AddTextBox("P_TRANSACTIONCODE")
                .SetDefault("TRANSFER_ISSUE")
                .SetIsHidden();
            popup.Conditions.AddTextBox("TRANSACTIONREASONNAME");

            popup.GridColumns.AddTextBoxColumn("TRANSACTIONREASONCODE", 150)
                .SetLabel("TRANSACTIONREASONCODE");
            popup.GridColumns.AddTextBoxColumn("TRANSACTIONREASONCODENAME", 200)
                .SetLabel("TRANSACTIONREASONCODENAME");

            popupTransactionreasoncode.SelectPopupCondition = popup;
        }
        private void selectOspAreaidPopup(string sPlantid)
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

            popup.Conditions.AddTextBox("P_AREANAME")
                .SetLabel("AREANAME");
            popup.GridColumns.AddTextBoxColumn("AREAID", 120)
                .SetLabel("AREAID");
            popup.GridColumns.AddTextBoxColumn("AREANAME", 200)
                .SetLabel("AREANAME");

            popupOspAreaid.SelectPopupCondition = popup;
        }
        #endregion

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

           


        }
      

        #endregion

        #region 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        private void InitializeCondition()
        {
            // 계획년월
            DateTime dateNow = DateTime.Now;
            dtpStartDate.EditValue = dateNow.AddDays(-7).ToString("yyyy-MM-dd");
            dtpEndDate.EditValue = dateNow.ToString("yyyy-MM-dd");
        }
        #endregion

        #region  이벤트
        private void InitializeEvent()
        {
            // 검색
            btnSearch.Click += BtnSearch_Click;
            // 단기
            btnClose.Click += BtnClose_Click;
            btnConfirm.Click += BtnConfirm_Click;
            cboPlantid.EditValueChanged += CboPlantid_EditValueChanged;
           
        }

        private void grdMaterialReceiptPopup_DoubleClick(object sender, EventArgs e)
        {
            int irow = grdMaterialReceiptPopup.View.FocusedRowHandle;
            if (irow < 0) return;
            DataRow row = grdMaterialReceiptPopup.View.GetFocusedDataRow();
            CurrentDataRow = row;
            write_handler(row);
            this.Close();
        }

        /// <summary>
        /// Plantid 변경 따른 Areaid 변경처리 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboPlantid_EditValueChanged(object sender, EventArgs e)
        {

           
            OnPlantidInformationSearch(cboPlantid.EditValue.ToString());
        }
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            DataRow row = grdMaterialReceiptPopup.View.GetFocusedDataRow();
            CurrentDataRow = row;
            write_handler(row);
            this.Close();
        }
        /// <summary>
        /// 닫기 클릭시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        /// <summary>
        /// 조회 클릭 - 메인 grid에 체크 데이터 전달
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            //필수 값 체크 기간 필수 처리 예정임.

            SearchReceiptPopup();
        }

        /// <summary>
        /// 계획 환율정보 가져오기 .
        /// </summary>
        private void SearchReceiptPopup()
        {

           
            string sPlantid = "";
            if (cboPlantid.EditValue != null)
            {
                sPlantid = cboPlantid.EditValue.ToString();
            }
            string strStartdate = "";
            string strEnddate = "";
            string strDateFormat = "";
            strDateFormat = "yyyy-MM-dd " + _sWorkTime;
            if (!dtpStartDate.Text.Equals(""))
            {
                DateTime dtStartdat = Convert.ToDateTime(dtpStartDate.Text.ToString());
                strStartdate = dtStartdat.ToString(strDateFormat);
            }
            if (!dtpEndDate.Text.Equals(""))
            {

                DateTime dtEnddat = Convert.ToDateTime(dtpEndDate.Text.ToString());
                strEnddate = dtEnddat.AddDays(1).ToString(strDateFormat);
            }
           
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("P_STARTDATE", strStartdate);
            Param.Add("P_ENDDATE", strEnddate);
            Param.Add("P_CONSUMABLECLASSNAME", txtConsumabledefid.Text);
            Param.Add("P_TRANSACTIONREASONCODE", popupTransactionreasoncode.GetValue().ToString());
            Param.Add("P_ENTERPRISEID", UserInfo.Current.Enterprise.ToString());
            Param.Add("P_PLANTID", sPlantid);
            Param.Add("P_TOAREAID", popupOspAreaid.GetValue().ToString());
            Param.Add("P_TRANSACTIONTYPE", "Move");
            Param.Add("P_TRANSACTIONCODE", "TRANSFER_ISSUE");
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            Param.Add("USERID", UserInfo.Current.Id.ToString());
            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dtReceipt = SqlExecuter.Query("GetMaterialGoodsMovementPopup", "10001", Param);
            grdMaterialReceiptPopup.DataSource = dtReceipt;

        }

        /// <summary>
        /// 취소 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdMaterialReceiptPopup.GridButtonItem = GridButtonItem.Export;
            grdMaterialReceiptPopup.View.SetIsReadOnly();
            grdMaterialReceiptPopup.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdMaterialReceiptPopup.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();
            grdMaterialReceiptPopup.View.AddTextBoxColumn("PLANTID", 150)
                .SetIsHidden();
            grdMaterialReceiptPopup.View.AddTextBoxColumn("TRANSACTIONNOMOVE", 120);
            grdMaterialReceiptPopup.View.AddTextBoxColumn("TRANSACTIONDATEMOVE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetDefault("")
                .SetIsReadOnly();
            grdMaterialReceiptPopup.View.AddTextBoxColumn("TRANSACTIONSEQUENCE", 80).SetIsHidden();
            grdMaterialReceiptPopup.View.AddTextBoxColumn("CONSUMABLEDEFID", 100);
            grdMaterialReceiptPopup.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 80);
            grdMaterialReceiptPopup.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
            grdMaterialReceiptPopup.View.AddTextBoxColumn("CONSUMABLELOTID", 100);
            grdMaterialReceiptPopup.View.AddTextBoxColumn("FROMAREAID", 150).SetIsHidden();
            grdMaterialReceiptPopup.View.AddTextBoxColumn("AREANAME", 150);
            grdMaterialReceiptPopup.View.AddTextBoxColumn("TOAREAID", 150).SetIsHidden();
            grdMaterialReceiptPopup.View.AddTextBoxColumn("TOAREANAME", 150);
           
            grdMaterialReceiptPopup.View.AddTextBoxColumn("TRANSACTIONCODE", 150).SetIsHidden();
            grdMaterialReceiptPopup.View.AddTextBoxColumn("TRANSACTIONREASONCODE", 100).SetIsHidden(); 
            grdMaterialReceiptPopup.View.AddTextBoxColumn("TRANSACTIONREASONCODENAME", 150).SetIsHidden();
            grdMaterialReceiptPopup.View.AddTextBoxColumn("ISMODIFY", 150).SetIsHidden();
            grdMaterialReceiptPopup.View.AddComboBoxColumn("TXNADDEDCODE", 150,
                new SqlQuery("GetCodeList", "00001", "CODECLASSID=ConsumableTxnAddedCode", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();   //  
            grdMaterialReceiptPopup.View.AddTextBoxColumn("UNIT", 80);   //  
            grdMaterialReceiptPopup.View.AddSpinEditColumn("QTY", 100)
                .SetDisplayFormat("#,###,##0.#####", MaskTypes.Numeric);
            grdMaterialReceiptPopup.View.AddTextBoxColumn("MATREMARK", 300);
           
            grdMaterialReceiptPopup.View.PopulateColumns();

        }


        /// <summary>
        /// plant정보에서 WORKTIME 
        /// </summary>
        private void OnPlantidInformationSearch(string sPlantid)
        {

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("P_PLANTID", sPlantid);
            DataTable dtPlantInfo = SqlExecuter.Query("GetPlantidInformatByCsm", "10001", dicParam);

            if (dtPlantInfo.Rows.Count == 1)
            {
                DataRow drPlant = dtPlantInfo.Rows[0];
                _sWorkTime = drPlant["WORKTIME"].ToString();
            }
            else
            {

                return;
            }

        }
    }
}
