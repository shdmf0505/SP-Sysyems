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
    /// 업  무  설  명  : 치공구 제작입고를 위한 치공구를 조회할 때 사용
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-07-29
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ToolNumberPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables
        public DataRow CurrentDataRow { get; set; }
        string _selectedVendor;
        public delegate void toolCodeChoice(DataTable table);
        public event toolCodeChoice choiceHandler;
        string _plantID;
        #endregion

        #region 생성자
        public ToolNumberPopup(string plantID)
        {
            InitializeComponent();
            InitializeCondition();
            InitializeEvent();

            _plantID = plantID;
        }
        #endregion

        #region Properties
        #region SetMakeVendor : 제작업체 검색조건에 초기값을 할당 및 가져온다.
        public void SetMakeVendor(string vendorID, string vendorName)
        {
            _selectedVendor = vendorID;
            popEditVendor.EditValue = vendorName;
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
            InitializeRepairVendors();
        }

        #region popupControl
        private void SetProductDefIDPopup()
        {
            ConditionItemSelectPopup areaCondition = new ConditionItemSelectPopup();
            areaCondition.Id = "PRODUCTDEFID";
            areaCondition.SearchQuery = new SqlQuery("GetProductdefidPoplistByTool", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={_plantID}", $"PRODUCTDEFTYPE=Product");
            areaCondition.SetPopupLayout("PRODUCTDEFIDPOPUP", PopupButtonStyles.Ok_Cancel, true, false);
            areaCondition.SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow);
            areaCondition.ValueFieldName = "PRODUCTDEFID";
            areaCondition.DisplayFieldName = "PRODUCTDEFNAME";
            areaCondition.SetPopupResultCount(1);
            areaCondition.SetPopupAutoFillColumns("PRODUCTDEFID");
            areaCondition.SetPopupResultMapping("PRODUCTDEFID", "PRODUCTDEFID");
            areaCondition.SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                //작업장 변경 시 작업자 조회
                selectedRows.Cast<DataRow>().ForEach(row =>
                {
                    txtProductDefID.SetValue(row.GetString("PRODUCTDEFID"));
                    txtProductDefID.Text = row.GetString("PRODUCTDEFNAME");
                    txtProductDefID.EditValue = row.GetString("PRODUCTDEFNAME");
                });

            });

            // 팝업 조회조건
            areaCondition.Conditions.AddTextBox("PRODUCTDEF")
                .SetLabel("PRODUCTDEF");
            //areaCondition.Conditions.AddComboBox("PRODUCTDEFTYPE", new SqlQuery("GetCodeListByOspProductdeftype", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } }), "CODENAME", "CODEID")                
            //    .SetLabel("PRODUCTDEFTYPE")
            //    .SetDefault("Product")
            //    .SetIsHidden()
            //    .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly                
            //    ;
            //areaCondition.Conditions.AddTextBox("PRODUCTDEFID")
            //    .SetLabel("PRODUCTDEFID");
            

            // 팝업 그리드
            areaCondition.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 250)
                .SetIsReadOnly();
            areaCondition.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly();
            areaCondition.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 350)
                .SetIsReadOnly();
            areaCondition.GridColumns.AddTextBoxColumn("PRODUCTDEFCODE", 50)
                .SetIsHidden();
            areaCondition.GridColumns.AddTextBoxColumn("PRODUCTDEFTYPE", 200)
                .SetIsReadOnly();

            txtProductDefID.SelectPopupCondition = areaCondition;

        }
        #endregion

        #region InitializeRepairVendors : 팝업창 제어
        /// <summary>
        /// 팝업창 제어
        /// </summary>
        private void InitializeRepairVendors()
        {
            ConditionItemSelectPopup areaCondition = new ConditionItemSelectPopup();
            areaCondition.Id = "MAKEVENDOR";
            areaCondition.SearchQuery = new SqlQuery("GetVendorListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={_plantID}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            areaCondition.ValueFieldName = "VENDORID";
            areaCondition.DisplayFieldName = "VENDORNAME";
            areaCondition.SetPopupLayout("MAKEVENDOR", PopupButtonStyles.Ok_Cancel, true, true);
            areaCondition.SetPopupResultCount(1);
            areaCondition.SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow);
            areaCondition.SetPopupAutoFillColumns("VENDORNAME");
            areaCondition.SetPopupResultMapping("MAKEVENDOR", "VENDORNAME");
            areaCondition.SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                //작업장 변경 시 작업자 조회
                selectedRows.Cast<DataRow>().ForEach(row =>
                {
                    _selectedVendor = row.GetString("VENDORID");
                    popEditVendor.EditValue = row.GetString("VENDORNAME");
                });

            });


            areaCondition.Conditions.AddTextBox("VENDORNAME");

            areaCondition.GridColumns.AddTextBoxColumn("VENDORID", 150)
                .SetIsHidden();
            areaCondition.GridColumns.AddTextBoxColumn("VENDORNAME", 200);

            popEditVendor.SelectPopupCondition = areaCondition;
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
            cboSite.EditValue = _plantID;

            cboSite.DataSource = SqlExecuter.Query("GetPlantList", "00001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } });

            cboSite.ShowHeader = false;

            cboSite.ReadOnly = true;
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
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);                                                  //Current Login User ID
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
            string strProductDefID = txtProductDefID.GetValue().ToString();
            if (!(strProductDefID.Equals("")))
            {
                strProductDefID = strProductDefID.Substring(0, 7);
            }
            
            values.Add("PRODUCTDEFID", strProductDefID);                                              //PRODUCTDEFID
            if(popEditVendor.Text != "")
                values.Add("VENDORID", _selectedVendor);                                                        //VENDORID


            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            #endregion

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolReqSearchResult = SqlExecuter.Query("GetToolRequestListForReceiptByTool", "10001", values);



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

            Shown += ToolNumberPopup_Shown;
        }

        #region ToolNumberPopup_Shown - 화면로딩후 이벤트
        private void ToolNumberPopup_Shown(object sender, EventArgs e)
        {
            cboSite.EditValue = _plantID;
        }
        #endregion

        #region BtnConfirm_Click - 확인버튼이벤트
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            DataTable table = grdToolCodeList.View.GetCheckedRows();            
            choiceHandler(table);
            this.Close();
        }
        #endregion

        #region grdToolCodeList_DoubleClick - 그리드 더블클릭이벤트
        private void grdToolCodeList_DoubleClick(object sender, EventArgs e)
        {
            //DataTable table = grdToolCodeList.View.GetCheckedRows();            
            //choiceHandler(table);
            //this.Close();
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
            grdToolCodeList.View.AddTextBoxColumn("TOOLNUMBER", 180)
                .SetIsReadOnly();
            //.SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("TOOLNAME", 350)
                .SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("PRODUCTDEFID", 120).SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("PRODUCTDEFNAME", 250).SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("TOOLCODE", 180).SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("DURABLEDEFVERSION", 80).SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("TOOLNAME", 350).SetIsReadOnly();
            grdToolCodeList.View.AddComboBoxColumn("TOOLFORMCODE", 150, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=ToolForm"), "CODENAME", "CODEID")
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                  //상태 - (이 회면에서 입력해야 한다면 SetIsReadOnly를 풀어준다.
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
            grdToolCodeList.View.AddTextBoxColumn("VENDORID").SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("VENDORNAME", 120).SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("REQUESTDELIVERYDATE", 100).SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("PLANDELIVERYDATE", 100).SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("AREAID").SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("AREANAME", 100).SetIsReadOnly();
            grdToolCodeList.View.AddTextBoxColumn("ISMODIFY").SetIsHidden();
            grdToolCodeList.View.AddTextBoxColumn("OWNSHIPTYPE").SetIsHidden();
            //grdDurableCodeList.View.SetAutoFillColumn("PRODUCTDEFNAME");
            grdToolCodeList.View.PopulateColumns();            
        }
        #endregion
    }
}
