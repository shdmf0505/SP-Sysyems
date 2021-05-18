#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
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

namespace Micube.SmartMES.StandardInfo.Popup
{
	/// <summary>
	/// 프 로 그 램 명  : ex> 시스템관리 > 코드 관리 > 코드그룹 정보
	/// 업  무  설  명  : ex> 시스템에서 공통으로 사용되는 코드그룹 정보를 관리한다.
	/// 생    성    자  : 홍길동
	/// 생    성    일  : 2019-05-14
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class MaterialItemDetailSpecPopup : SmartPopupBaseForm, ISmartCustomPopup
	{
		public DataRow CurrentDataRow { get; set; }
        string _sITEMID = "";   //품목id
        string _sITEMVERSION = ""; // 품목버전
        string _sENTERPRISEID = ""; // 회사
        #region Local Variables
        #endregion

        #region 생성자

        public MaterialItemDetailSpecPopup()
		{
			InitializeComponent();

			InitializeControls();
			InitializeEvent();
		}

        #endregion

        public MaterialItemDetailSpecPopup(DataRow dr)
        {
            CurrentDataRow = dr;
            InitializeComponent();
            InitializeControls();
            InitializeEvent();

        }
            #region 컨텐츠 영역 초기화

            private void InitializeControls()
		{
			//기본내역

            txtProduct.EditValue = Format.GetString(CurrentDataRow["ITEMID"]);
            txtProduct.Editor.ReadOnly = true;


            //품목구분
            cboProductType.Editor.ValueMember = "MASTERDATACLASSID";
            cboProductType.Editor.DisplayMember = "MASTERDATACLASSNAME";
            cboProductType.Editor.UseEmptyItem = true;
            cboProductType.Editor.ShowHeader = false;
            cboProductType.Editor.DataSource = SqlExecuter.Query("GetmasterdataclassList", "10001", new Dictionary<string, object>() { { "MESITEMTYPE", "Consumable" }, { "ENTERPRISEID", UserInfo.Current.Enterprise } });
            cboProductType.Editor.ReadOnly = true;
            cboProductType.EditValue = Format.GetString(CurrentDataRow["MASTERDATACLASSID"]);


            //구매단위
            cboPurchaseUnit.Editor.ValueMember = "CODEID";
            cboPurchaseUnit.Editor.DisplayMember = "CODENAME";
            cboPurchaseUnit.Editor.UseEmptyItem = true;
            cboPurchaseUnit.Editor.ShowHeader = false;
            cboPurchaseUnit.Editor.DataSource = SqlExecuter.Query("GetTypeList", "10001", new Dictionary<string, object>() { { "CODECLASSID", "PurchaseUnit" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
            cboPurchaseUnit.Editor.ReadOnly = true;
            cboPurchaseUnit.EditValue = Format.GetString(CurrentDataRow["UNITOFPURCHASING"]);


            //수불단위
            cboReceivePayOut.Editor.ValueMember = "CODEID";
            cboReceivePayOut.Editor.DisplayMember = "CODENAME";
            cboReceivePayOut.Editor.UseEmptyItem = true;
            cboReceivePayOut.Editor.ShowHeader = false;
            cboReceivePayOut.Editor.DataSource = SqlExecuter.Query("GetTypeList", "10001", new Dictionary<string, object>() { { "CODECLASSID", "ReceivePayOutUnit" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
            cboReceivePayOut.Editor.ReadOnly = true;
            cboReceivePayOut.EditValue = Format.GetString(CurrentDataRow["UNITOFSTOCK"]);


            //품종
            cboBreeds.Editor.ValueMember = "CODEID";
            cboBreeds.Editor.DisplayMember = "CODENAME";
            cboBreeds.Editor.UseEmptyItem = true;
            cboBreeds.Editor.ShowHeader = false;
            cboBreeds.Editor.DataSource = SqlExecuter.Query("GetBreedsCodeList", "10001", new Dictionary<string, object>() { { "CODECLASSID", "BREEDSCODE" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
            cboBreeds.Editor.ReadOnly = true;
            cboBreeds.EditValue = Format.GetString(CurrentDataRow["MATERIALCLASS"]);


            txtSpec.Editor.ReadOnly = true;
            txtSpec.EditValue = Format.GetString(CurrentDataRow["SPEC"]);


            //품목계정
            cboItemAccount.Editor.ValueMember = "CODEID";
            cboItemAccount.Editor.DisplayMember = "CODENAME";
            cboItemAccount.Editor.UseEmptyItem = true;
            cboItemAccount.Editor.ShowHeader = false;
            cboItemAccount.Editor.DataSource = SqlExecuter.Query("GetTypeList", "10001", new Dictionary<string, object>() { { "CODECLASSID", "ItemAccount2" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
            cboItemAccount.Editor.ReadOnly = true;
            cboItemAccount.EditValue = Format.GetString(CurrentDataRow["ACCOUNTCODE"]);

            txtProcurement.Editor.ReadOnly = true;
            txtProcurement.EditValue = Format.GetString(CurrentDataRow["PROCUREMENT"]);


 
            //구매담당
            ConditionItemSelectPopup options = new ConditionItemSelectPopup();
			options.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedDialog);
			options.SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, false);
			options.Id = "USER";
			//options4.SearchQuery = new SqlQuery("GetUserList", "10001", $"PLANTID={UserInfo.Current.Plant}");
			options.SearchQuery = new SqlQuery("SelectUserGroupUserSearch", "10001", $"P_USERGROUPID={"PurchaseOwner"}");
			options.IsMultiGrid = false;
			options.DisplayFieldName = "USERNAME";
			options.ValueFieldName = "USERID";
			options.LanguageKey = "USER";

			options.Conditions.AddTextBox("USERIDNAME");

			options.GridColumns.AddTextBoxColumn("USERID", 150);
			options.GridColumns.AddTextBoxColumn("USERNAME", 200);

            popPurchaseOwner.Editor.SelectPopupCondition = options;
			popPurchaseOwner.Tag = "PURCHASEMAN";
 

            //발주정책
            cboOrderPolicy.Editor.ValueMember = "CODEID";
			cboOrderPolicy.Editor.DisplayMember = "CODENAME";
			cboOrderPolicy.Editor.UseEmptyItem = true;
			cboOrderPolicy.Editor.ShowHeader = false;
			cboOrderPolicy.Editor.DataSource = SqlExecuter.Query("GetCodeList", "00001", new Dictionary<string, object>() { { "CODECLASSID", "OrderPolicy2" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });

            //자재내역
            //주거래처

            ConditionItemSelectPopup options3 = new ConditionItemSelectPopup();
            options3.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedDialog);
            options3.SetPopupLayout("VENDORID", PopupButtonStyles.Ok_Cancel, true, false);
            options3.Id = "VENDORID";
            options3.SearchQuery = new SqlQuery("GetVendorList", "10001");
            options3.IsMultiGrid = false;
            options3.DisplayFieldName = "VENDORNAME";
            options3.ValueFieldName = "VENDORID";
            options3.LanguageKey = "VENDORID";
            options3.Conditions.AddTextBox("VENDORID");

            options3.GridColumns.AddTextBoxColumn("VENDORID", 150);
            options3.GridColumns.AddTextBoxColumn("VENDORNAME", 250);

            popMainDeal.Editor.SelectPopupCondition = options3;
            popMainDeal.Tag = "CUSTOMERID";


            //자재담당
            ConditionItemSelectPopup options2 = new ConditionItemSelectPopup();
            options2.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedDialog);
            options2.SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, false);
            options2.Id = "USER";
            //options4.SearchQuery = new SqlQuery("GetUserList", "10001", $"PLANTID={UserInfo.Current.Plant}");
            options2.SearchQuery = new SqlQuery("SelectUserGroupUserSearch", "10001", $"P_USERGROUPID={"PurchaseOwner"}");
            options2.IsMultiGrid = false;
            options2.DisplayFieldName = "USERNAME";
            options2.ValueFieldName = "USERID";

            options2.LanguageKey = "USER";

            options2.Conditions.AddTextBox("USERIDNAME");

            options2.GridColumns.AddTextBoxColumn("USERID", 150);
            options2.GridColumns.AddTextBoxColumn("USERNAME", 200);

            popMaterialOwner.Editor.SelectPopupCondition = options2;
            popMaterialOwner.Tag = "MATERIALMAN";


            //주관리창고
            cboWareHouse.Editor.ValueMember = "WAREHOUSEID";
            cboWareHouse.Editor.DisplayMember = "WAREHOUSENAME";
            cboWareHouse.Editor.UseEmptyItem = true;
            cboWareHouse.Editor.ShowHeader = false;
            cboWareHouse.Editor.DataSource = SqlExecuter.Query("GetWarehouseList", "10002", new Dictionary<string, object>() {{ "ENTERPRISEID", UserInfo.Current.Enterprise }, { "PLANTID", UserInfo.Current.Plant }, });



            //자재등급
            cboMaterialGrade.Editor.ValueMember = "CODEID";
			cboMaterialGrade.Editor.DisplayMember = "CODENAME";
			cboMaterialGrade.Editor.UseEmptyItem = true;
			cboMaterialGrade.Editor.ShowHeader = false;
			cboMaterialGrade.Editor.DataSource = SqlExecuter.Query("GetCodeList", "00001", new Dictionary<string, object>() { { "CODECLASSID", "MaterialGrade" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
			
            //출고정책
			cboMaterialOut.Editor.ValueMember = "CODEID";
			cboMaterialOut.Editor.DisplayMember = "CODENAME";
			cboMaterialOut.Editor.UseEmptyItem = true;
			cboMaterialOut.Editor.ShowHeader = false;
			cboMaterialOut.Editor.DataSource = SqlExecuter.Query("GetCodeList", "00001", new Dictionary<string, object>() { { "CODECLASSID", "MaterialOutType2" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
            cboMaterialOut.Tag = "GIPOLICY";


            txtProduct.Tag = "ITEMID";
            txtHsNo.Tag = "HSNO";
            chkIsRefund.Tag = "DRAWBACKFLAG";
            txtRefundLevel.Tag = "DRAWBACKLEVEL";
            txtRefundUnit.Tag = "DRAWBACKUNIT";
            popMainDeal.Tag = "CUSTOMERID";
            txtPurchaseLoss.Tag = "PURCHASELOSS";
            txtMinPurchaseQty.Tag = "MINORDERQTY";

            txtPackingQty.Tag = "PACKINGQTY";
            popPurchaseOwner.Tag = "PURCHASEMAN";
            cboOrderPolicy.Tag = "ORDERPOLICY";
            txtNormalLeadTime.Tag = "NORMALLEADTIME";
            txtEmergencyLeadTime.Tag = "URGENCYLEADTIME";
            txtWeight.Tag = "WEIGHT";
            txtDutyRate.Tag = "DRAWBACKRATE";
            txtBuDaeRate.Tag = "INCIDENTALRATE";
            popMaterialOwner.Tag = "MATERIALMAN";

            txtStock.Tag = "SAFETYSTOCK";
            cboWareHouse.Tag = "MAINWAREHOUSEID";
            cboMaterialGrade.Tag = "MATERIALRATING";
            cboMaterialOut.Tag = "GIPOLICY";
            txtJickChulType.Tag = "DIRECTGI";
            txtMaterialLocation.Tag = "LOCATION";
            txtMaterialCycle.Tag = "STOCKCYCLE";

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("ITEMID", Format.GetString(CurrentDataRow["ITEMID"]));

            DataTable dt = SqlExecuter.Query("GetMaterialItemMaster", "10003", param);
           

            CommonFunction.SuspendDrawing(this);
            try
            {
                SetControlsFrom scf = new SetControlsFrom();
                if (dt.Rows.Count == 0)
                {
                    return;
                }
                scf.SetControlsFromBoxControlDelRow(smartSplitTableLayoutPanel2, dt.Rows[0]);
                scf.SetControlsFromBoxControlDelRow(smartSplitTableLayoutPanel1, dt.Rows[0]);
            }

            finally
            {
                CommonFunction.ResumeDrawing(this);
            }

        }

		#endregion

		#region Event

		/// <summary>
		/// 화면에서 사용되는 이벤트를 초기화한다.
		/// </summary>
		public void InitializeEvent()
		{
			btnConfrim.Click += BtnConfrim_Click;
			btnCancel.Click += BtnCancel_Click;
		}


		private void BtnConfrim_Click(object sender, EventArgs e)
		{
            /*
            DataTable dtinfo = new DataTable();
            dtinfo.Columns.Add("ITEMID");
            dtinfo.Columns.Add("ENTERPRISEID");

            dtinfo.Columns.Add("HSNO");
            dtinfo.Columns.Add("DRAWBACKFLAG");
            dtinfo.Columns.Add("DRAWBACKLEVEL");
            dtinfo.Columns.Add("DRAWBACKUNIT");
            dtinfo.Columns.Add("CUSTOMERID");
            dtinfo.Columns.Add("PURCHASELOSS");
            dtinfo.Columns.Add("MINORDERQTY");
            dtinfo.Columns.Add("PACKINGQTY");
            dtinfo.Columns.Add("PURCHASEMAN");
            dtinfo.Columns.Add("ORDERPOLICY");
            dtinfo.Columns.Add("NORMALLEADTIME");
            dtinfo.Columns.Add("URGENCYLEADTIME");
            dtinfo.Columns.Add("WEIGHT");
            dtinfo.Columns.Add("DRAWBACKRATE");
            dtinfo.Columns.Add("INCIDENTALRATE");
            dtinfo.Columns.Add("MATERIALMAN");
            dtinfo.Columns.Add("SAFETYSTOCK");
            dtinfo.Columns.Add("MAINWAREHOUSEID");
            dtinfo.Columns.Add("MATERIALRATING");
            dtinfo.Columns.Add("GIPOLICY");
            dtinfo.Columns.Add("DIRECTGI");
            dtinfo.Columns.Add("LOCATION");
            dtinfo.Columns.Add("STOCKCYCLE");
            dtinfo.Columns.Add("_STATE_");

            DataRow dr = dtinfo.NewRow();


            dr["ITEMID"] = txtProduct.EditValue;
            dr["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            dr["HSNO"] = txtHsNo.EditValue;
            dr["DRAWBACKFLAG"] = chkIsRefund.EditValue;
            dr["DRAWBACKLEVEL"] = txtRefundLevel.EditValue;
            dr["DRAWBACKUNIT"] = txtRefundUnit.EditValue;

            dr["PURCHASELOSS"] = txtPurchaseLoss.EditValue;
            dr["MINORDERQTY"] = txtMinPurchaseQty.EditValue;

            dr["PACKINGQTY"] = txtPackingQty.EditValue;

            dr["ORDERPOLICY"] = cboOrderPolicy.EditValue;
            dr["NORMALLEADTIME"] = txtNormalLeadTime.EditValue;
            dr["URGENCYLEADTIME"] = txtEmergencyLeadTime.EditValue;
            dr["WEIGHT"] = txtWeight.EditValue;
            dr["DRAWBACKRATE"] = txtDutyRate.EditValue;
            dr["INCIDENTALRATE"] = txtBuDaeRate.EditValue;
         

            dr["SAFETYSTOCK"] = txtStock.EditValue;
            dr["MAINWAREHOUSEID"] = cboWareHouse.EditValue;
            dr["MATERIALRATING"] = cboMaterialGrade.EditValue;
            dr["GIPOLICY"] = cboMaterialOut.EditValue;
            dr["DIRECTGI"] = txtJickChulType.EditValue;
            dr["LOCATION"] = txtMaterialLocation.EditValue;
            dr["STOCKCYCLE"] = txtMaterialCycle.EditValue;


            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_USERGROUPID", "PurchaseOwner");
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);

            param.Add("USERIDNAME", popPurchaseOwner.EditValue);

            DataTable dt = SqlExecuter.Query("SelectUserGroupUserSearch", "10001", param);
            dr["PURCHASEMAN"] = dt.Rows[0]["USERID"];
            param.Remove("USERIDNAME");
            param.Add("USERIDNAME", popMaterialOwner.EditValue);
            dt = SqlExecuter.Query("SelectUserGroupUserSearch", "10001", param);
            dr["MATERIALMAN"] = dt.Rows[0]["USERID"];


            param.Add("VENDORID", popMainDeal.EditValue);

            dt = SqlExecuter.Query("GetVendorList", "10002", param);

            dr["CUSTOMERID"] = dt.Rows[0]["VENDORID"];


            dr["_STATE_"] = "added";


            dtinfo.Rows.Add(dr);
            DataSet ds = new DataSet();
            dtinfo.TableName = "popupmaterialspec";
            ds.Tables.Add(dtinfo);
             
            ExecuteRule("SaveMaterialItemMaster_YPE", ds);
            ShowMessage("SuccessSave");
            */
            this.Close();

        }


		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		#endregion

		#region Private Function

		#endregion
	}
}