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
    /// 프 로 그 램 명  : 치공구 선택 팝업
    /// 업  무  설  명  : 치공구 수정출고를 선택하기 위한 팝업
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-07-31
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ToolNumberForUpdatePopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        public DataRow CurrentDataRow { get; set; }
        public ToolNumberForUpdatePopup()
        {
            InitializeComponent();
            InitializeCondition();
            InitializeEvent();
        }

        #region Local Variables
        public delegate void toolCodeChoice(DataTable table);
        public event toolCodeChoice choiceHandler;
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
        }

        #region popupControl : Product팝업
        private void SetProductDefIDPopup()
        {
            ConditionItemSelectPopup productCondition = new ConditionItemSelectPopup();
            productCondition.Id = "PRODUCTDEFID";
            productCondition.SearchQuery = new SqlQuery("GetProductdefidPoplistByOsp", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            productCondition.SetPopupLayout("PRODUCTDEFIDPOPUP", PopupButtonStyles.Ok_Cancel, true, false);
            productCondition.SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow);
            productCondition.ValueFieldName = "PRODUCTDEFID";
            productCondition.DisplayFieldName = "PRODUCTDEFID";
            productCondition.SetPopupResultCount(1);
            productCondition.SetPopupAutoFillColumns("PRODUCTDEFID");
            productCondition.SetPopupResultMapping("PRODUCTDEFID", "PRODUCTDEFID");
            productCondition.SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                //작업장 변경 시 작업자 조회
                selectedRows.Cast<DataRow>().ForEach(row =>
                {
                    txtProductDefID.EditValue = row.GetString("PRODUCTDEFID");
                });

            });

            // 팝업 조회조건
            productCondition.Conditions.AddTextBox("PRODUCTDEFNAME")
                .SetLabel("PRODUCTDEFNAME");
            productCondition.Conditions.AddComboBox("PRODUCTDEFTYPE", new SqlQuery("GetCodeListByOspProductdeftype", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } }), "CODENAME", "CODEID")
                .SetLabel("PRODUCTDEFTYPE")
                .SetDefault("Product")
                .SetIsHidden()
                .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
                ;
            productCondition.Conditions.AddTextBox("PRODUCTDEFID")
                .SetLabel("PRODUCTDEFID");
            

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

            txtProductDefID.SelectPopupCondition = productCondition;
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
            cboSite.EditValue = UserInfo.Current.Plant;

            cboSite.DataSource = SqlExecuter.Query("GetPlantList", "00001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } });

            cboSite.ShowHeader = false;

            cboSite.ReadOnly = true;
        }

        /// <summary>
        /// Vendor ComboBox를 초기화한다.
        /// </summary>
        /// <param name="plantID"></param>
        private void InitializeVendorComboBox()
        {
            // 검색조건에 정의된 공장을 정리
            cboVendor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboVendor.ValueMember = "VENDORID";
            cboVendor.DisplayMember = "VENDORNAME";
            cboVendor.UseEmptyItem = true;
            cboVendor.EmptyItemCaption = "";
            cboVendor.EmptyItemValue = "";

            cboVendor.DataSource = SqlExecuter.Query("GetVendorListByTool", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "PLANTID", UserInfo.Current.Plant } });

            cboVendor.ShowHeader = false;
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
            if (deRequestDateTO.EditValue != null)
            {
                DateTime dtRequestDateTo = Convert.ToDateTime(deRequestDateTO.EditValue);

                values.Add("P_REQUESTDATE_PERIODTO", dtRequestDateTo.ToString("yyyy-MM-dd"));                   //REQUESTDATE
            }
            if (deRequestDateFR.EditValue != null)
            {
                DateTime dtRequestDateFr = Convert.ToDateTime(deRequestDateFR.EditValue);

                values.Add("P_REQUESTDATE_PERIODFR", dtRequestDateFr.ToString("yyyy-MM-dd"));                   //REQUESTDATE
            }
            //REQUESTDELIVERYDATE
            if (deRequestDeliveryDateTO.EditValue != null)
            {
                DateTime dtRequestDeliveryDateTo = Convert.ToDateTime(deRequestDeliveryDateTO.EditValue);

                values.Add("P_REQUESTDELIVERYDATE_PERIODTO", dtRequestDeliveryDateTo.ToString("yyyy-MM-dd"));    //REQUESTDELIVERYDATE
            }
            if (deRequestDeliveryDateFR.EditValue != null)
            {
                DateTime dtRequestDeliveryDateFr = Convert.ToDateTime(deRequestDeliveryDateFR.EditValue);

                values.Add("P_REQUESTDELIVERYDATE_PERIODFR", dtRequestDeliveryDateFr.ToString("yyyy-MM-dd"));    //REQUESTDELIVERYDATE
            }
            //PLANDELIVERYDATE
            if (dePlanDeliveryDateTO.EditValue != null)
            {
                DateTime dtPlanDeliveryDateTo = Convert.ToDateTime(dePlanDeliveryDateTO.EditValue);

                values.Add("P_PLANDELIVERYDATE_PERIODTO", dtPlanDeliveryDateTo.ToString("yyyy-MM-dd"));         //PLANDELIVERYDATE
            }
            if (dePlanDeliveryDateFR.EditValue != null)
            {
                DateTime dtPlanDeliveryDateFr = Convert.ToDateTime(dePlanDeliveryDateFR.EditValue);

                values.Add("P_PLANDELIVERYDATE_PERIODFR", dtPlanDeliveryDateFr.ToString("yyyy-MM-dd"));         //PLANDELIVERYDATE
            }
            values.Add("PRODUCTDEFID", txtProductDefID.EditValue);                                              //PRODUCTDEFID
            values.Add("VENDORID", cboVendor.EditValue);                                                        //VENDORID

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            #endregion

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolReqSearchResult = SqlExecuter.Query("GetToolRequestListForUpdateByTool", "10001", values);



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
        }

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

            grdToolCodeList.View.AddTextBoxColumn("REQUESTDATE", 100).SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("REQUESTSEQUENCE", 80).SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("TOOLNUMBER", 180).SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("PRODUCTDEFID", 120).SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("TOOLCODE", 180).SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("DURABLEDEFVERSION", 80).SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("REVDURABLEDEFVERSION", 80).SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("TOOLCATEGORYID").SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("TOOLCATEGORY", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("TOOLCATEGORYDETAILID").SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("TOOLCATEGORYDETAIL", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("TOOLDETAILID").SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("TOOLDETAIL", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("REQUESTDEPARTMENT", 100).SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("REQUESTUSERID").SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("REQUESTUSER", 100).SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("REQUESTCOMMENT", 200).SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("VENDORID").SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("MAKEVENDOR", 120).SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("REQUESTDELIVERYDATE", 100).SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("PLANDELIVERYDATE", 100).SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("AREAID").SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("RECEIPTAREA", 100).SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("OWNSHIPTYPE").SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("USEDCOUNT").SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("CLEANLIMIT").SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("TOTALCLEANCOUNT").SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("TOTALUSEDCOUNT").SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("USEDLIMIT").SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("TOTALREPAIRCOUNT").SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("SENDAREAID").SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("SENDAREANAME").SetIsHidden();
            //grdDurableCodeList.View.SetAutoFillColumn("PRODUCTDEFNAME");
            grdToolCodeList.View.PopulateColumns();
        }
        #endregion
    }
}
