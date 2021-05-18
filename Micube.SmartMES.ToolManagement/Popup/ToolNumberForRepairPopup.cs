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

namespace Micube.SmartMES.ToolManagement.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 치공구 수리 선택 팝업
    /// 업  무  설  명  : 치공구 수리출고를 선택하기 위한 팝업
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-08-02
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ToolNumberForRepairPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        public ToolNumberForRepairPopup(string plantID)
        {
            InitializeComponent();
            InitializeCondition();
            InitializeEvent();
            _plantID = plantID;
        }

        #region Local Variables
        public delegate void toolCodeChoice(DataTable table);
        public event toolCodeChoice choiceHandler;
        private string _selectedSendArea;
        private string _plantID; 
        public DataRow CurrentDataRow { get; set; }
        #endregion

        #region Properties
        #region SetSendArea : 출고작업장을 설정한다.
        public void SetSendArea(string areaID, string areaName)
        {
            _selectedSendArea = areaID;
            popEditArea.EditValue = areaName;
        }
        #endregion
        #endregion

        #region 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        private void InitializeCondition()
        {
            //그리드 초기화
            InitializeGridIdDefinitionManagement();
            SetProductDefIDPopup();
            InitializePlantComboBox();
            InitializeVendorComboBox();

            InitializeRepairVendors();
        }
        #region popupControl
        private void SetProductDefIDPopup()
        {
            ConditionItemSelectPopup productCondition = new ConditionItemSelectPopup();
            productCondition.Id = "PRODUCTDEFID";
            productCondition.SearchQuery = new SqlQuery("GetProductdefidPoplistByTool", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={_plantID}", $"PRODUCTDEFTYPE=Product");
            productCondition.SetPopupLayout("PRODUCTDEFIDPOPUP", PopupButtonStyles.Ok_Cancel, true, false);
            productCondition.SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow);
            productCondition.ValueFieldName = "PRODUCTDEFID";
            productCondition.DisplayFieldName = "PRODUCTDEFNAME";
            productCondition.SetPopupResultCount(1);
            productCondition.SetPopupAutoFillColumns("PRODUCTDEFID");
            productCondition.SetPopupResultMapping("PRODUCTDEFID", "PRODUCTDEFID");
            productCondition.SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                //작업장 변경 시 작업자 조회
                selectedRows.Cast<DataRow>().ForEach(row =>
                {
                    popEditProductDefID.EditValue = row.GetString("PRODUCTDEFID");
                });

            });

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

        #region InitializeRepairVendors : 팝업창 제어 (출고작업장)
        /// <summary>
        /// 팝업창 제어
        /// </summary>
        private void InitializeRepairVendors()
        {
            ConditionItemSelectPopup areaCondition = new ConditionItemSelectPopup();
            areaCondition.Id = "SENDAREA";
            areaCondition.SearchQuery = new SqlQuery("GetAreaIDListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={_plantID}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CURRENTLOGINID={UserInfo.Current.Id}");
            areaCondition.ValueFieldName = "AREAID";
            areaCondition.DisplayFieldName = "AREANAME";
            areaCondition.SetPopupLayout("SENDAREA", PopupButtonStyles.Ok_Cancel, true, true);
            areaCondition.SetPopupResultCount(1);
            areaCondition.SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow);
            areaCondition.SetPopupAutoFillColumns("AREANAME");
            //areaCondition.SetPopupResultMapping("MAKEVENDOR", "VENDORNAME");
            areaCondition.SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                //작업장 변경 시 작업자 조회
                selectedRows.Cast<DataRow>().ForEach(row =>
                {
                    _selectedSendArea = row.GetString("AREAID");
                    popEditArea.EditValue = row.GetString("AREANAME");
                });

            });


            areaCondition.Conditions.AddTextBox("AREANAME");

            areaCondition.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetIsHidden();
            areaCondition.GridColumns.AddTextBoxColumn("AREANAME", 200);

            popEditArea.SelectPopupCondition = areaCondition;
        }
        #endregion        

        #region ComboBox  초기화
        /// <summary>
        /// Site ComboBox를 초기화한다. 자신이 속한 SITE를 기본으로 한다.
        /// </summary>
        /// <param name="plantID"></param>
        private void InitializePlantComboBox()
        {
            // 검색조건에 정의된 공장을 정리
            cboSite.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboSite.ValueMember = "PLANTID";
            cboSite.DisplayMember = "PLANTID";
            
            cboSite.DataSource = SqlExecuter.Query("GetPlantList", "00001", new Dictionary<string, object>() { {"LANGUAGETYPE",UserInfo.Current.LanguageType}, {"ENTERPRISEID", UserInfo.Current.Enterprise} });

            cboSite.ShowHeader = false;

            cboSite.ReadOnly = true;

            cboSite.EditValue = _plantID;
        }

        /// <summary>
        /// Vendor ComboBox를 초기화한다.
        /// </summary>
        /// <param name="plantID"></param>
        private void InitializeVendorComboBox()
        {
            // 검색조건에 정의된 공장을 정리
            //cboVendor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            //cboVendor.ValueMember = "VENDORID";
            //cboVendor.DisplayMember = "VENDORNAME";


            //cboVendor.DataSource = SqlExecuter.Query("GetVendorListByTool", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "PLANTID", UserInfo.Current.Plant } });

            //cboVendor.ShowHeader = false;
        }
        #endregion
        #endregion

        #region 검색

        #region OnSearchAsync - 검색
        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected void Search()
        {
            // TODO : 조회 SP 변경
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);                                            //ENTERPRISEID
            values.Add("PLANTID", cboSite.EditValue);                                                           //PLANTID
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);
            
            if(popEditArea.EditValue != null)
                if(popEditArea.EditValue.ToString() != "")
                    values.Add("AREAID", _selectedSendArea);                                                        //AREAID

            if (popEditProductDefID.EditValue != null)
                if (popEditProductDefID.EditValue.ToString() != "")
                    values.Add("PRODUCTDEFID", popEditProductDefID.GetValue());                                      //PRODUCTDEFID

            if(chkIsLimitRepairCount.Checked)
                values.Add("CLEANLIMIT", "Y");                                                                  //CLEANLIMIT

            if (chkUsedLimit.Checked)
                values.Add("USEDLIMIT", "Y");                                                                   //USEDLIMIT

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            #endregion

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolReqSearchResult = SqlExecuter.Query("GetToolRequestListForRepairByTool", "10001", values);

            grdToolCodeList.DataSource = toolReqSearchResult;

            if (toolReqSearchResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
        }
        #endregion
        #endregion

        #region  이벤트
        private void InitializeEvent()
        {
            btnBrowse.Click += btnBrowse_Click;
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;
            grdToolCodeList.View.DoubleClick += grdToolCodeList_DoubleClick;

            Shown += ToolNumberForRepairPopup_Shown;
        }

        #region ToolNumberForRepairPopup_Shown - 화면로딩후 이벤트
        private void ToolNumberForRepairPopup_Shown(object sender, EventArgs e)
        {
            cboSite.EditValue = _plantID;
        }
        #endregion

        #region BtnConfirm_Click - 확인버튼 이벤트
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            DataTable table = grdToolCodeList.View.GetCheckedRows();
            choiceHandler(table);
            this.Close();
        }
        #endregion

        #region grdToolCodeList_DoubleClick - 그리드 더블클릭 이벤트
        private void grdToolCodeList_DoubleClick(object sender, EventArgs e)
        {
            DataTable table = grdToolCodeList.View.GetCheckedRows();
            choiceHandler(table);
            this.Close();
        }
        #endregion

        #region btnBrowse_Click - 조회버튼이벤트
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            Search();
        }
        #endregion

        #region BtnCancel_Click - 취소버튼이벤트
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #endregion

        #region 컨트롤 초기화
        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridIdDefinitionManagement()
        {
            // GRID 초기화
            grdToolCodeList.GridButtonItem = GridButtonItem.Export;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
                                                                      //grdProductItemSpec.View.AddTextBoxColumn("ENTERPIRSEID").SetIsHidden();
            grdToolCodeList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdToolCodeList.View.AddTextBoxColumn("REQUESTDATE", 100).SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("REQUESTSEQUENCE", 80).SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("TOOLNUMBER", 180).SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("PRODUCTDEFID", 120).SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("PRODUCTDEFNAME", 250).SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("TOOLCODE", 180).SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("DURABLEDEFVERSION", 80).SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("REVDURABLEDEFVERSION", 80).SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("TOOLNAME", 350).SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("AREAID", 80).SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("AREANAME", 80).SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("ISMODIFY", 80).SetIsHidden();
            grdToolCodeList.View.AddComboBoxColumn("TOOLFORMCODE", 150, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=ToolForm"), "CODENAME", "CODEID")
               .SetTextAlignment(TextAlignment.Center)
               .SetIsReadOnly(true);                                                  //상태 - (이 회면에서 입력해야 한다면 SetIsReadOnly를 풀어준다.
            grdToolCodeList.View.AddTextBoxColumn("TOOLCATEGORYID").SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("TOOLCATEGORY", 100).SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("TOOLCATEGORYDETAILID").SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("TOOLCATEGORYDETAIL", 100).SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("TOOLDETAILID").SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("TOOLDETAIL", 100).SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("REQUESTDEPARTMENT", 100).SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("REQUESTUSERID").SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("REQUESTUSER", 100).SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("REQUESTCOMMENT", 200).SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("VENDORID").SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("MAKEVENDOR", 120).SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("REQUESTDELIVERYDATE", 100).SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("PLANDELIVERYDATE", 100).SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("RECEIPTAREAID").SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("RECEIPTAREA", 100).SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("OWNSHIPTYPE").SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("USEDCOUNT").SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("CLEANLIMIT").SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("TOTALCLEANCOUNT").SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("TOTALUSEDCOUNT").SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("USEDLIMIT").SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("TOTALREPAIRCOUNT").SetIsReadOnly();
            //grdDurableCodeList.View.SetAutoFillColumn("PRODUCTDEFNAME");
            grdToolCodeList.View.PopulateColumns();
        }
        #endregion
    }
}
