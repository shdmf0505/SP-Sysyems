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

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.BandedGrid;

#endregion

namespace Micube.SmartMES.StandardInfo.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > Setup > 작업장 관리 > 설비 유형 팝업
    /// 업  무  설  명  : 설비 유형(EquipmentClass)을 선택
    /// 생    성    자  :  정승원
    /// 생    성    일  : 2019-05-22
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class PackageProductCopyPopup : SmartPopupBaseForm, ISmartCustomPopup
	{
        public DataRow CurrentDataRow { get; set; }

        #region Local Variables
        string _sCD_ITEM = "";
        string _sITEMVERSION = "";
        string _sNM_ITEM = "";

      

		//Resource Type = Equipment
		
        /// <summary>
        ///  선택한 list를 보내기 위한 Handler
        /// </summary>
        /// <param name="dt"></param>
        public DataTable dtChk;
        
        #endregion

        #region 생성자
        public PackageProductCopyPopup()
        {
            InitializeComponent();
            InitializeEvent();
            InitializeCondition();
            InitializeGridIdDefinitionManagement();
        }

       


        #endregion

        #region 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        private void InitializeCondition()
        {

            ConditionItemSelectPopup CustomerCode = new ConditionItemSelectPopup();
            CustomerCode.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            CustomerCode.SetPopupLayout("CUSTOMERID", PopupButtonStyles.Ok_Cancel);
            CustomerCode.Id = "CUSTOMERID";
            CustomerCode.LabelText = "CUSTOMERID";
            CustomerCode.SearchQuery = new SqlQuery("GetCustomerList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            CustomerCode.IsMultiGrid = false;
            CustomerCode.DisplayFieldName = "CUSTOMERNAME";
            CustomerCode.ValueFieldName = "CUSTOMERID";
            CustomerCode.LanguageKey = "CUSTOMERID";
            CustomerCode.Conditions.AddTextBox("TXTCUSTOMERID");
            CustomerCode.GridColumns.AddTextBoxColumn("CUSTOMERID", 80);
            CustomerCode.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 150);
            sspCustomer.SelectPopupCondition = CustomerCode;

            ConditionItemSelectPopup ItemCode = new ConditionItemSelectPopup();
            ItemCode.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            ItemCode.SetPopupLayout("PRODUCTDEFID", PopupButtonStyles.Ok_Cancel);
            ItemCode.Id = "PRODUCTDEFID";
            ItemCode.LabelText = "PRODUCTDEFID";
            ItemCode.SearchQuery = new SqlQuery("GetPackageProductItemPupopGroup", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"CUSTOMERID={sspCustomer.GetValue()}");
            ItemCode.IsMultiGrid = false;
            ItemCode.DisplayFieldName = "PRODUCTDEFNAME";
            ItemCode.ValueFieldName = "PRODUCTDEFID";
            ItemCode.LanguageKey = "PRODUCTDEFID";
            ItemCode.Conditions.AddTextBox("PRODUCTDEFID");
            ItemCode.Conditions.AddTextBox("PRODUCTDEFNAME");
            ItemCode.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            ItemCode.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            sspItem.SelectPopupCondition = ItemCode;


            // 버전
            cboVer.DisplayMember = "ITEMVERSIONNAME";
            cboVer.ValueMember = "ITEMVERSIONCODE";
            cboVer.ShowHeader = false;
            Dictionary<string, object> ParamVer = new Dictionary<string, object>();
            ParamVer.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ParamVer.Add("PLANTID", UserInfo.Current.Plant);
            DataTable dtConsumableType = SqlExecuter.Query("GetProDuctDefVersion", "10001", ParamVer);
            cboVer.DataSource = dtConsumableType;


       


            // 유효상태
            cboValidState.DisplayMember = "CODENAME";
            cboValidState.ValueMember = "CODEID";
            cboValidState.ShowHeader = false;
            Dictionary<string, object> ParamRt = new Dictionary<string, object>();
            ParamRt.Add("CODECLASSID", "ValidState");
            ParamRt.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtValidState = SqlExecuter.Query("GetCodeList", "00001", ParamRt);
            cboValidState.DataSource = dtValidState;

            cboValidState.EditValue = "Valid";
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridIdDefinitionManagement()
        {
            // GRID 초기화
            grdItemCopy.GridButtonItem = GridButtonItem.None;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdItemCopy.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdItemCopy.View.AddTextBoxColumn("PACKAGECLASS", 100);
            grdItemCopy.View.AddTextBoxColumn("CUSTOMERID", 80);
            grdItemCopy.View.AddTextBoxColumn("PRODUCTDEFID", 100);
            grdItemCopy.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            grdItemCopy.View.AddTextBoxColumn("PRODUCTDEFNAME", 100);
            grdItemCopy.View.AddTextBoxColumn("PLANTID", 100);
            grdItemCopy.View.AddTextBoxColumn("ENTERPRISEID", 100);
            grdItemCopy.View.AddTextBoxColumn("ITEMCODE", 100);
            grdItemCopy.View.AddTextBoxColumn("USERSEQUENCE", 100);
            grdItemCopy.View.AddTextBoxColumn("ISCONFIRMATION", 100);
            grdItemCopy.View.AddTextBoxColumn("PACKAGETYPE", 100);
            grdItemCopy.View.AddTextBoxColumn("PACKQTY", 100);
            grdItemCopy.View.AddTextBoxColumn("CASEQTY", 100);
            grdItemCopy.View.AddTextBoxColumn("BOXQTY", 100);
            grdItemCopy.View.AddTextBoxColumn("PACKAGEQTY", 100);
            grdItemCopy.View.AddTextBoxColumn("PACKINGMETHOD", 100);
            grdItemCopy.View.AddTextBoxColumn("VACUUMMETHOD", 100);
            grdItemCopy.View.AddTextBoxColumn("MOISTURECARD", 100);
            grdItemCopy.View.AddTextBoxColumn("SILICAGEL", 100);
            grdItemCopy.View.AddTextBoxColumn("PACKAGEPAPER", 100);
            grdItemCopy.View.AddTextBoxColumn("PACKINGCODE", 100);
            grdItemCopy.View.AddTextBoxColumn("PACKINGNAME", 100);
            grdItemCopy.View.AddTextBoxColumn("WARRANTY", 100);
            grdItemCopy.View.AddTextBoxColumn("PARTNO1", 100);
            grdItemCopy.View.AddTextBoxColumn("PARTNO2", 100);
            grdItemCopy.View.AddTextBoxColumn("PARTNO3", 100);
            grdItemCopy.View.AddTextBoxColumn("PARTNO4", 100);
            grdItemCopy.View.AddTextBoxColumn("PARTNAME", 100);
            grdItemCopy.View.AddTextBoxColumn("PARTDESCRIPTION", 100);
            grdItemCopy.View.AddTextBoxColumn("SUPPLIERCODE", 100);
            grdItemCopy.View.AddTextBoxColumn("SUPPLIERNAME", 100);
            grdItemCopy.View.AddTextBoxColumn("ETC", 100);
            grdItemCopy.View.AddTextBoxColumn("PCSWEIGHT", 100);
            grdItemCopy.View.AddTextBoxColumn("BOXWEIGHT", 100);
            grdItemCopy.View.AddTextBoxColumn("ISROSH", 100);
            grdItemCopy.View.AddTextBoxColumn("ISHF", 100);
            grdItemCopy.View.AddTextBoxColumn("DESCRIPTION", 100);
            grdItemCopy.View.AddTextBoxColumn("VALIDSTATE", 100);

            grdItemCopy.View.AddTextBoxColumn("CREATOR", 100);
            grdItemCopy.View.AddTextBoxColumn("CREATORNAME", 100);

            this.grdItemCopy.View.AddTextBoxColumn("CREATEDTIME", 130)
            // Display Format 지정
            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            grdItemCopy.View.AddTextBoxColumn("MODIFIER", 100);
            grdItemCopy.View.AddTextBoxColumn("MODIFIERNAME", 100);

            this.grdItemCopy.View.AddTextBoxColumn("MODIFIEDTIME", 130)
            // Display Format 지정
            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);



            grdItemCopy.View.PopulateColumns();

            //RepositoryItemCheckEdit repositoryCheckEdit1 = grdProductItem.View.GridControl.RepositoryItems.Add("CheckEdit") as RepositoryItemCheckEdit;
            //repositoryCheckEdit1.ValueChecked = "True";
            //repositoryCheckEdit1.ValueUnchecked = "False";
            //repositoryCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            //grdProductItem.View.Columns["S"].ColumnEdit = repositoryCheckEdit1;

        }
        #endregion

        #region  이벤트
        private void InitializeEvent()
        {
            btnSearch.Click += BtnSearch_Click;
            btnAPPLY.Click += BtnAPPLY_Click;
            btnCancel.Click += BtnCancel_Click;

            sspCustomer.Validating += SspCustomer_Validating;
            sspItem.Validating += SspItem_Validating;


        }

        private void SspItem_Validating(object sender, CancelEventArgs e)
        {
            //품목정보
            cboVer.DisplayMember = "ITEMVERSIONNAME";
            cboVer.ValueMember = "ITEMVERSIONCODE";
            cboVer.ShowHeader = false;
            Dictionary<string, object> ParamVer = new Dictionary<string, object>();
            ParamVer.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ParamVer.Add("PLANTID", UserInfo.Current.Plant);
            ParamVer.Add("CUSTOMERID", sspCustomer.GetValue());
            ParamVer.Add("PRODUCTDEFID", sspItem.GetValue());

            DataTable dtVer = SqlExecuter.Query("GetProDuctDefVersion", "10001", ParamVer);
            cboVer.DataSource = dtVer;
        }


        private void SspCustomer_Validating(object sender, CancelEventArgs e)
        {

            //품목
            ConditionItemSelectPopup ItemCode = new ConditionItemSelectPopup();
            ItemCode.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            ItemCode.SetPopupLayout("PRODUCTDEFID", PopupButtonStyles.Ok_Cancel);
            ItemCode.Id = "PRODUCTDEFID";
            ItemCode.LabelText = "PRODUCTDEFID";
            ItemCode.SearchQuery = new SqlQuery("GetPackageProductItemPupopGroup", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"CUSTOMERID={sspCustomer.GetValue()}");
            ItemCode.IsMultiGrid = false;
            ItemCode.DisplayFieldName = "PRODUCTDEFNAME";
            ItemCode.ValueFieldName = "PRODUCTDEFID";
            ItemCode.LanguageKey = "PRODUCTDEFID";
            ItemCode.Conditions.AddTextBox("PRODUCTDEFID");
            ItemCode.Conditions.AddTextBox("PRODUCTDEFNAME");
            ItemCode.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            ItemCode.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            sspItem.SelectPopupCondition = ItemCode;

        }

        private void BtnAPPLY_Click(object sender, EventArgs e)
        {
            dtChk = grdItemCopy.View.GetCheckedRows();

            this.Close();
        }

      

        

        /// <summary>
        /// 조회 클릭 - 메인 grid에 체크 데이터 전달
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string sItemVer = "";

            if(cboVer.EditValue == null)
            {
                sItemVer = "";
            }
            else
            {
                sItemVer = cboVer.EditValue.ToString();
            }

            Search(sspCustomer.GetValue().ToString(), sspItem.GetValue().ToString(), sItemVer, cboValidState.EditValue.ToString());
        }

        void Search(string sCust,string _sItem,string sItemver,string sValidstate)
        {
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            Param.Add("P_PLANTID", UserInfo.Current.Plant);
            Param.Add("CUSTOMERID", sCust);

            Param.Add("ITEMCODE", _sItem);
            Param.Add("ITEMVERSION", sItemver);
          
            Param.Add("P_VALIDSTATE", sValidstate);
          

            DataTable dt = SqlExecuter.Query("GetPackageProductList", "10001", Param);
            grdItemCopy.DataSource = dt;
            
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
    }
}
