#region using
using Micube.Framework.Net;
using Micube.Framework;
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
using DevExpress.XtraCharts;
using DevExpress.Utils;
using DevExpress.XtraEditors.Mask;
using Micube.Framework.SmartControls.Validations;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.BandedGrid;
using Micube.SmartMES.StandardInfo.Popup;
using Micube.SmartMES.Commons;
#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품목관리 > 품목등록(자재)
    /// 업  무  설  명  : 자재품목등록
    /// 생    성    자  :  윤성원
    /// 생    성    일  : 2019-06-28
    /// 수  정  이  력  : 
    /// </summary>
    /// 
    public partial class MaterialItemMaster_YPE : SmartConditionManualBaseForm
    {
        #region Local Variables
        DataTable dtmaterial;

        #endregion

        #region 생성자
        public MaterialItemMaster_YPE()
        {
            InitializeComponent();
            InitializeEvent();

        }
		#endregion

		#region 컨텐츠 영역 초기화

		/// <summary>
		/// 컨텐츠 초기화
		/// </summary>
		protected override void InitializeContent()
		{
			base.InitializeContent();

			InitializeGrid();
			InitializeControls();
		}

		/// <summary>
		/// 컨트롤 초기화
		/// </summary>
		private void InitializeControls()
		{
			//품목코드
			txtItemId.Tag = "ITEMID";
			
			//품목구분
			cboItemType.ValueMember = "MASTERDATACLASSID";
			cboItemType.DisplayMember = "MASTERDATACLASSNAME";
			cboItemType.UseEmptyItem = true;
			cboItemType.ShowHeader = false;
			cboItemType.DataSource = SqlExecuter.Query("GetmasterdataclassList", "10001", new Dictionary<string, object>() { { "MESITEMTYPE", "Consumable" }, { "ENTERPRISEID", UserInfo.Current.Enterprise } });
			cboItemType.Tag = "MASTERDATACLASSID";

			//기본코드
    		txtDefaultCode.Tag = "DEFAULTCODE";

			//품명
			txtItemName.Tag = "ITEMNAME";

			//재질
			txtQualityMaterial.Tag = "MATERIALQUALITY";

			//색상
			cboColor.ValueMember = "CODEID";
			cboColor.DisplayMember = "CODENAME";
			cboColor.UseEmptyItem = true;
			cboColor.ShowHeader = false;
			cboColor.DataSource = SqlExecuter.Query("GetCodeList", "00001", new Dictionary<string, object>() { { "CODECLASSID", "Color" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
			cboColor.Tag = "COLOR";

			//구매단위
			cboPurchasingUnit.ValueMember = "CODEID";
			cboPurchasingUnit.DisplayMember = "CODENAME";
			cboPurchasingUnit.UseEmptyItem = true;
			cboPurchasingUnit.ShowHeader = false;
			cboPurchasingUnit.DataSource = SqlExecuter.Query("GetCodeList", "00001", new Dictionary<string, object>() { { "CODECLASSID", "PurchaseUnit" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
			cboPurchasingUnit.Tag = "UNITOFPURCHASING";

			//규격
			txtSpec.Tag = "SPEC";

			//수불단위
			cboStockUnit.ValueMember = "CODEID";
			cboStockUnit.DisplayMember = "CODENAME";
			cboStockUnit.UseEmptyItem = true;
			cboStockUnit.ShowHeader = false;
			cboStockUnit.DataSource = SqlExecuter.Query("GetCodeList", "00001", new Dictionary<string, object>() { { "CODECLASSID", "ReceivePayOutUnit" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
			cboStockUnit.Tag = "UNITOFSTOCK";

			//품종코드
			cboItemKindCode.ValueMember = "CODEID";
			cboItemKindCode.DisplayMember = "CODENAME";
			cboItemKindCode.UseEmptyItem = true;
			cboItemKindCode.ShowHeader = false;
			cboItemKindCode.DataSource = SqlExecuter.Query("GetBreedsCodeList", "10001", new Dictionary<string, object>() { { "CODECLASSID", "BREEDSCODE" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
			cboItemKindCode.Tag = "MATERIALCLASS";

			//품목계정
			cboItemAccount.ValueMember = "CODEID";
			cboItemAccount.DisplayMember = "CODENAME";
			cboItemAccount.UseEmptyItem = true;
			cboItemAccount.ShowHeader = false;
			cboItemAccount.DataSource = SqlExecuter.Query("GetCodeList", "00001", new Dictionary<string, object>() { { "CODECLASSID", "ItemAccount2" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
			cboItemAccount.Tag = "ACCOUNTCODE";

			//조달방법
			cboProcurement.ValueMember = "CODEID";
			cboProcurement.DisplayMember = "CODENAME";
			cboProcurement.UseEmptyItem = true;
			cboProcurement.ShowHeader = false;
			cboProcurement.DataSource = SqlExecuter.Query("GetCodeList", "00001", new Dictionary<string, object>() { { "CODECLASSID", "WayOfSupply" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
			cboProcurement.Tag = "PROCUREMENT";

            //환산계수
            txtExchangeCoefficient.Tag = "CONVERSIONFACTOR";

			//제조사
			InitializeManufacturerPopup();

			//원산지
			cboOrigin.ValueMember = "CODEID";
			cboOrigin.DisplayMember = "CODENAME";
			cboOrigin.UseEmptyItem = true;
			cboOrigin.ShowHeader = false;
			cboOrigin.DataSource = SqlExecuter.Query("GetCodeList", "00001", new Dictionary<string, object>() { { "CODECLASSID", "CountryType" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
			cboOrigin.Tag = "ORIGIN";

			//lme품목구분
            checkLmeItemType.Tag = "LME";

			//거래처품목코드
            txtCustomerItemId.Tag = "CUSTOMERITEMID";

			//adh(um)
			txtAdh.Tag = "ADH";

			//동박유형
			cboCuStuffType.ValueMember = "CODEID";
			cboCuStuffType.DisplayMember = "CODENAME";
			cboCuStuffType.UseEmptyItem = true;
			cboCuStuffType.ShowHeader = false;
			cboCuStuffType.DataSource = SqlExecuter.Query("GetCodeList", "00001", new Dictionary<string, object>() { { "CODECLASSID", "CuFoilType" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
			cboCuStuffType.Tag = "COPPERTYPE";

			//시효기간(day)
			txtLimitDay.Tag = "LIMITATION";

			//품목SPEC
			txtItemSpec1.Tag = "MILL";
			txtItemSpec2.Tag = "OZ1";
			txtItemSpec3.Tag = "OZ2";

			//가로 * 세로
			txtHorizontal.Tag = "WIDTH";
			txtVertical.Tag = "LENGTH";

			//두께
			txtThickness.Tag = "THICK";

			//공통출고창고 
			InitializeCommonShipWarehousePopup();

			//단종구분
			cboDiscontinuedType.ValueMember = "CODEID";
			cboDiscontinuedType.DisplayMember = "CODENAME";
			cboDiscontinuedType.UseEmptyItem = true;
			cboDiscontinuedType.ShowHeader = false;
			cboDiscontinuedType.DataSource = SqlExecuter.Query("GetCodeList", "00001", new Dictionary<string, object>() { { "CODECLASSID", "DiscontinueType" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
			cboDiscontinuedType.Tag = "ENDTYPE";

			//사용제품코드
			cboUseProductCode.Tag = "USEITEMID";

			//LOT관리여부
			cboLotManageType.ValueMember = "CODEID";
			cboLotManageType.DisplayMember = "CODENAME";
			cboLotManageType.UseEmptyItem = true;
			cboLotManageType.ShowHeader = false;
			cboLotManageType.DataSource = SqlExecuter.Query("GetCodeList", "00001", new Dictionary<string, object>() { { "CODECLASSID", "YesNo" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
			cboLotManageType.Tag = "LOTCONTROL";

			//고객품번
			txtCustomerNo.Tag = "CUSTOMERITEMID";

			//비고
			txtRemark.Tag = "DESCRIPTION";
		}

		/// <summary>
		/// 제조사 
		/// </summary>
		private void InitializeManufacturerPopup()
		{
			ConditionItemSelectPopup options = new ConditionItemSelectPopup();
			options.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedDialog);
			options.SetPopupLayout("SELECTVENDORID", PopupButtonStyles.Ok_Cancel, true, false);
			options.Id = "VENDORID";
			options.SearchQuery = new SqlQuery("GetVendorList", "10001");
			options.IsMultiGrid = false;
			options.DisplayFieldName = "VENDORNAME";
			options.ValueFieldName = "VENDORID";
			options.LanguageKey = "MAKER";

			options.Conditions.AddComboBox("PLANTID", new SqlQuery("GetPlantList", "10001"), "PLANTNAME", "PLANTID").SetLabel("SITE").SetDefault(UserInfo.Current.Plant);
			options.Conditions.AddTextBox("VENDORID");

			options.GridColumns.AddTextBoxColumn("VENDORID", 150);
			options.GridColumns.AddTextBoxColumn("VENDORNAME", 200);

			popManufacturer.SelectPopupCondition = options;
			popManufacturer.Tag = "MAKER";
		}

		/// <summary>
		/// 공통출고창고
		/// </summary>
		private void InitializeCommonShipWarehousePopup()
		{
			ConditionItemSelectPopup options = new ConditionItemSelectPopup();
			options.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedDialog);
			options.SetPopupLayout("SELECTWAREHOUSEID", PopupButtonStyles.Ok_Cancel, true, false);
			options.Id = "WAREHOUSEID";
			options.SearchQuery = new SqlQuery("GetWarehouseList", "10002");
			options.IsMultiGrid = false;
			options.DisplayFieldName = "WAREHOUSENAME";
			options.ValueFieldName = "WAREHOUSEID";
			options.LanguageKey = "MAKER";

			options.Conditions.AddComboBox("PLANTID", new SqlQuery("GetPlantList", "10001"), "PLANTNAME", "PLANTID").SetLabel("SITE").SetDefault(UserInfo.Current.Plant);
			options.Conditions.AddTextBox("TXTWAREHOUSE");

			options.GridColumns.AddTextBoxColumn("WAREHOUSEID", 150);
			options.GridColumns.AddTextBoxColumn("WAREHOUSENAME", 200);

			popCommonShipWarehouse.SelectPopupCondition = options;
			popCommonShipWarehouse.Tag = "WAREHOUSEID";
		}

		/// <summary>
		/// 그리드 초기화
		/// </summary>
		private void InitializeGrid()
        {
			grdConsumableList.GridButtonItem = GridButtonItem.Import | GridButtonItem.Export;

			grdConsumableList.View.SetIsReadOnly();

			//SITE
			grdConsumableList.View.AddTextBoxColumn("PLANTID", 60).SetTextAlignment(TextAlignment.Center);
			//품목코드
			grdConsumableList.View.AddTextBoxColumn("ITEMID", 100);

            //품목구분 
            grdConsumableList.View.AddComboBoxColumn("MASTERDATACLASSID", 70, new SqlQuery("GetCodeList", "00001", "CODECLASSID=MaterialType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("ITEMACCOUNT")
                .SetTextAlignment(TextAlignment.Center);
			//기본코드
			grdConsumableList.View.AddTextBoxColumn("DEFAULTCODE", 60).SetTextAlignment(TextAlignment.Center);
			//품명 
			grdConsumableList.View.AddTextBoxColumn("ITEMNAME", 100);
			//규격
			grdConsumableList.View.AddTextBoxColumn("SPEC", 100);
            //재질 
            grdConsumableList.View.AddTextBoxColumn("MATERIALQUALITY", 100)
                .SetLabel("QUALITYMATERIAL");
			//색상
			grdConsumableList.View.AddComboBoxColumn("COLOR", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Color", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
			//구매단위
			grdConsumableList.View.AddComboBoxColumn("UNITOFPURCHASING", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=PurchaseUnit", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            //수불단위 
            grdConsumableList.View.AddComboBoxColumn("UNITOFSTOCK", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ReceivePayOutUnit", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("STOCKUNIT");
            //품종코드 
            grdConsumableList.View.AddComboBoxColumn("MATERIALCLASS", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=BreedsCode", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("ITEMKINDCODE");
            //품목계정 
            grdConsumableList.View.AddComboBoxColumn("ACCOUNTCODE", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ItemAccount2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("ITEMACCOUNT");
            //조달방법
            grdConsumableList.View.AddComboBoxColumn("PROCUREMENT", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProcurementType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
			//환산계수
			grdConsumableList.View.AddTextBoxColumn("CONVERSIONFACTOR", 80);
            //품목스펙1
            grdConsumableList.View.AddTextBoxColumn("MILL", 80)
                .SetIsHidden();    
            //품목스펙2
            grdConsumableList.View.AddTextBoxColumn("OZ1", 80)
                .SetIsHidden();
            //품목스펙3
            grdConsumableList.View.AddTextBoxColumn("OZ2", 80)
                .SetIsHidden();
            //제조사
            grdConsumableList.View.AddTextBoxColumn("MAKER", 100);
			//원산지 
			grdConsumableList.View.AddComboBoxColumn("ORIGIN", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=CountryType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            //LME품목구분 
            grdConsumableList.View.AddTextBoxColumn("LME", 100)
                .SetLabel("LMEITEMTYPE");   
			//가로*세로 
			grdConsumableList.View.AddTextBoxColumn("HORIZONTALVERTICAL", 100);
            //두께 
            grdConsumableList.View.AddTextBoxColumn("THICK", 100)
                .SetLabel("THICKNESS");
			//ADH 
			grdConsumableList.View.AddTextBoxColumn("ADH", 100);
			//동박유형 
			grdConsumableList.View.AddComboBoxColumn("COPPERTYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=CuFoilType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center)
                .SetLabel("CUSTUFFTYPE");
            //시효기간
            grdConsumableList.View.AddDateEditColumn("LIMITATION", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime).SetTextAlignment(TextAlignment.Center)
                .SetLabel("LIMITDAY"); 
            //LOT관리
            grdConsumableList.View.AddComboBoxColumn("LOTCONTROL", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            //수입검사
            grdConsumableList.View.AddTextBoxColumn("ISINCOMMINGINSPECTION", 100)
                .SetLabel("INCOMMINGINSPECTION");
			//비고
			grdConsumableList.View.AddTextBoxColumn("DESCRIPTION", 100);
            //고객품번 
            grdConsumableList.View.AddTextBoxColumn("CUSTOMERITEMNO", 100)
                .SetLabel("CUSTOMERITEMID"); 
            //단종구분
            grdConsumableList.View.AddComboBoxColumn("ENDTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DiscontinueType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            //유효상태
            grdConsumableList.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetDefault("Valid")
				.SetValidationKeyColumn();
			//등록자
			grdConsumableList.View.AddTextBoxColumn("CREATOR", 80)
				.SetTextAlignment(TextAlignment.Center);
			//등록일
			grdConsumableList.View.AddDateEditColumn("CREATEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
				.SetTextAlignment(TextAlignment.Center);
			//수정자
			grdConsumableList.View.AddTextBoxColumn("MODIFIER", 80)
				.SetTextAlignment(TextAlignment.Center);
			//수정일
			grdConsumableList.View.AddDateEditColumn("MODIFIEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
				.SetTextAlignment(TextAlignment.Center);

			grdConsumableList.View.PopulateColumns();
		}
        #endregion

        #region 이벤트
        private void InitializeEvent()
        {
            grdConsumableList.View.FocusedRowChanged += View_FocusedRowChanged;
        }

        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            display();
        }

        #endregion

        #region 조회조건 영역

        /// <summary>
        /// 
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

			//품목
			InitializeCondition_ProductPopup();
			//제조사
			InitializeCondition_Vendor();
		}

		/// <summary>
		/// 팝업형 조회조건 생성 - 품목
		/// </summary>
		private void InitializeCondition_ProductPopup()
		{
			var conditionProductId = Conditions.AddSelectPopup("P_ITEMID", new SqlQuery("GetItemMasterList", "10003", "PRODUCTDIVISION=Consumable", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "ITEMID", "ITEMID")
				.SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
				.SetPopupLayoutForm(800, 800)
				.SetPopupAutoFillColumns("ITEMNAME")
				.SetLabel("ITEMID")
				.SetPosition(1.2)
				.SetPopupResultCount(0)
				.SetPopupApplySelection((selectRow, gridRow) => {

				});

			// 팝업에서 사용되는 검색조건 (품목코드/명)
			conditionProductId.Conditions.AddTextBox("TXTITEM").SetLabel("PRODUCT");


			// 품목코드
			conditionProductId.GridColumns.AddTextBoxColumn("ITEMID", 150);
			// 품목명
			conditionProductId.GridColumns.AddTextBoxColumn("ITEMNAME", 200);
			// 품목버전
			conditionProductId.GridColumns.AddTextBoxColumn("ITEMVERSION", 100);
		}

		/// <summary>
		/// 팝업형 조회조건 생성 - 제조사
		/// </summary>
		private void InitializeCondition_Vendor()
		{
			var conditionId = Conditions.AddSelectPopup("P_VENDORID", new SqlQuery("GetVendorList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "VENDORNAME", "VENDORID")
				.SetPopupLayout("SELECTVENDORID", PopupButtonStyles.Ok_Cancel, true, false)
				.SetPopupLayoutForm(600, 600)
				.SetPopupAutoFillColumns("VENDORNAME")
				.SetLabel("MEASURINGMANUFACTURER")
				.SetPosition(4.2)
				.SetPopupResultCount(0)
				.SetPopupApplySelection((selectRow, gridRow) => {

				});

			conditionId.Conditions.AddTextBox("TXTITEM").SetLabel("PRODUCT");

			// 업체코드
			conditionId.GridColumns.AddTextBoxColumn("VENDORID", 150);
			// 업체명
			conditionId.GridColumns.AddTextBoxColumn("VENDORNAME", 200);
		}

		#endregion

		#region 툴바

		protected override void OnToolbarClick(object sender, EventArgs e)
        {
            base.OnToolbarClick(sender, e);
			SmartButton btn = sender as SmartButton;

            //DataRow drNew = grdConsumableList.View.GetFocusedDataRow();
            //if (drNew == null) return;


            if (btn.Name.Equals("ProductSpec"))
            {
                DataRow row = grdConsumableList.View.GetFocusedDataRow();
                MaterialItemDetailSpecPopup detailSpec = new MaterialItemDetailSpecPopup(row);
                   detailSpec.ShowDialog();
            }
            else if (btn.Name.Equals("ReplacementIdCreate"))
            {
                DataRow row = grdConsumableList.View.GetFocusedDataRow();
                MaterialItemReplacementPopup reIdCreate = new MaterialItemReplacementPopup(row);
                dtmaterial = null;
                reIdCreate.AddSemiProductEventHandler += semiProductItemCode_AddSemiProductEventHandler;
                reIdCreate.ShowDialog();
            }
            else if (btn.Name.Equals("Replacement"))
            {
                DataRow drNew = grdConsumableList.View.GetFocusedDataRow();
                MaterialItemReplicePopup itemReplice = new MaterialItemReplicePopup(drNew);
                itemReplice.StartPosition = FormStartPosition.CenterParent;
                itemReplice.ShowDialog();
            }
            else if (btn.Name.Equals("New"))
            {
                CommonFunctionProductSpec.ClearData(smartGroupBox1);

                dtmaterial = new DataTable();

                dtmaterial = (grdConsumableList.DataSource as DataTable).Clone();
                dtmaterial.Columns.Add("_STATE_");


                DataRow dr = dtmaterial.NewRow();


                var values = this.Conditions.GetValues();
                dr["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                dr["PLANTID"] = values["P_PLANTID"].ToString();

                dr["VALIDSTATE"] = "Valid";
                dr["_STATE_"] = "added";
                dtmaterial.Rows.Add(dr);

            }

        }


        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            {
                base.OnToolbarSaveClick();

                if (dtmaterial != null)
                {
                    foreach (DataRow drmaterial in dtmaterial.Rows)
                    {
                        if (Format.GetString(drmaterial["_STATE_"]).Equals("added"))
                        {




                            foreach (DataRow row in dtmaterial.Rows)
                            {
                                row["PLANTID"] = UserInfo.Current.Plant;
                                row["ITEMID"] = txtItemId.EditValue;
                                row["MASTERDATACLASSID"] = cboItemType.EditValue;
                                row["ITEMNAME"] = txtItemName.EditValue;
                                row["SPEC"] = txtSpec.EditValue;
                                row["MATERIALTYPE"] = cboItemType.EditValue;
                                row["UNITOFPURCHASING"] = cboPurchasingUnit.EditValue;
                                row["COLOR"] = cboColor.EditValue;
                                row["MATERIALQUALITY"] = txtQualityMaterial.EditValue;
                                row["UNITOFSTOCK"] = cboStockUnit.EditValue;
                                row["MATERIALCLASS"] = cboItemKindCode.EditValue;
                                row["ACCOUNTCODE"] = cboItemAccount.EditValue;
                                row["PROCUREMENT"] = cboProcurement.EditValue;
                                row["CONVERSIONFACTOR"] = txtExchangeCoefficient.EditValue;
                                row["MAKER"] = popManufacturer.EditValue;
                                row["ORIGIN"] = cboOrigin.EditValue;
                                row["LME"] = checkLmeItemType.EditValue;
                                if (txtHorizontal != null && !txtHorizontal.EditValue.Equals(""))
                                {
                                    row["WIDTH"] = Convert.ToDouble(txtHorizontal.EditValue);
                                }
                                if (txtVertical != null && !txtVertical.EditValue.Equals(""))
                                {
                                    row["LENGTH"] = Convert.ToDouble(txtVertical.EditValue);

                                }

                                if(!txtThickness.EditValue.Equals(""))
                                {
                                    row["THICK"] = txtThickness.EditValue;
                                }
                                if (!txtAdh.EditValue.Equals(""))
                                {
                                    row["ADH"] = txtAdh.EditValue;
                                }

     
                                row["COPPERTYPE"] = cboCuStuffType.EditValue;
                                row["LIMITATION"] = txtLimitDay.EditValue;   
                                row["LOTCONTROL"] = cboLotManageType.EditValue;
                                row["CUSTOMERITEMID"] = txtCustomerNo.EditValue;
                                //    row["INCOMMINGINSPECTION"] = txtDESCRIPTION.EditValue;  수입검사 저장할 그룹에 X
                                row["DESCRIPTION"] = txtRemark.EditValue;
                                row["ENDTYPE"] = cboDiscontinuedType.EditValue;

                                if (!txtItemSpec1.EditValue.Equals(""))
                                {
                                    row["MILL"] = txtItemSpec1.EditValue;
                                }
                                if (!txtItemSpec2.EditValue.Equals(""))
                                {
                                    row["OZ1"] = txtItemSpec2.EditValue;
                                }
                                if (!txtItemSpec3.EditValue.Equals(""))
                                {
                                    row["OZ2"] = txtItemSpec3.EditValue;
                                }



                                row["USEITEMID"] = cboUseProductCode.EditValue;

                            }
                            ExecuteRule("SaveMaterialItemMaster_YPE", dtmaterial);
                        }

                    }
                }

                DataTable changed = new DataTable();
                changed = grdConsumableList.GetChangedRows();

                ExecuteRule("SaveMaterialItemMaster_YPE", changed);

            }
        }
        #endregion

        #region 검색

            /// <summary>
            /// 비동기 override 모델
            /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();
            
            var values = this.Conditions.GetValues();
            values.Add("PLANTID", UserInfo.Current.Plant);
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //values.Add("MASTERDATACLASSID", "");

            DataTable dtMDCList = await SqlExecuter.QueryAsync("SelectMaterialItemMaster", "10001", values);

            if (dtMDCList.Rows.Count < 1) // 
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            this.grdConsumableList.DataSource = dtMDCList;
            

        }
		#endregion

		#region 유효성 검사
		/// <summary>
		/// 데이터 저장할때 컨텐츠 영역의 유효성 검사
		/// </summary>
		protected override void OnValidateContent()
        {
            base.OnValidateContent();

            DataTable changed = new DataTable();

            if (dtmaterial == null)
            {
                GetControlsFrom gcf = new GetControlsFrom();
                gcf.GetControlsFromBoxControlDelGrid(tplItemInfo, grdConsumableList);


                //grdPackageProduct.View.CheckValidation();

                changed = grdConsumableList.GetChangedRows();
            }

            if (dtmaterial == null && changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }
		#endregion

		#region private Fuction

		/// <summary>
		/// SmartSplitTableLayoutPanel 안에 있는 컨트롤에 row데이터 바인드
		/// </summary>
		private void RowDataBindInTableControls(DataRow r)
		{
			if (r == null) return;

			foreach (Control ctl in tplItemInfo.Controls)
			{
				string tag = Format.GetString(ctl.Tag);
				if (string.IsNullOrEmpty(Format.GetString(ctl.Tag))) continue;

				switch (ctl.GetType().Name)
				{
					case "SmartComboBox":
						SmartComboBox cbo = ctl as SmartComboBox;
						cbo.EditValue = r[tag];
						break;
					case "SmartMemoEdit":
					case "SmartTextBox":
						ctl.Text = r[tag].ToString();
						break;
					case "SmartSelectPopupEdit":
						SmartSelectPopupEdit pop = ctl as SmartSelectPopupEdit;
						if (!string.IsNullOrWhiteSpace(Format.GetString(r[tag])))
						{
							if (tag.EndsWith("ID"))
							{
								if (r.Table.Columns.Contains(tag.Remove(tag.Length - 2) + "NAME"))
								{
									pop.SetValue(r[tag]);
									pop.EditValue = r[tag];
									pop.Text = Format.GetString(r[tag.Remove(tag.Length - 2) + "NAME"]);
								}
								else
								{
									pop.SetValue(r[tag]);
									pop.EditValue = r[tag];
								}
							}
							else
							{
								pop.SetValue(r[tag]);
								pop.EditValue = r[tag];
							}
						}
						else
						{
							pop.ClearValue();
						}
						break;
				}
			}
		}

        #endregion



        private void display()
        {
            CommonFunction.SuspendDrawing(this);    // 속도향상을 위해 화면 드로잉 정지
            try
            {
                SetControlsFrom scf = new SetControlsFrom();
                DataRow row = grdConsumableList.View.GetFocusedDataRow();

                if (row == null)
                {
                    return;
                }
                scf.SetControlsFromBoxControlDelRow(tplItemInfo, row);
            }

            finally
            {
                CommonFunction.ResumeDrawing(this);
            }
        }

        private void txtSpec_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void semiProductItemCode_AddSemiProductEventHandler(object sender, AddSemiProductEventArgs e)
        {
            grdConsumableList.View.AddNewRow();

            grdConsumableList.View.ClearColumnsFilter();
            grdConsumableList.View.FocusedRowHandle = grdConsumableList.View.RowCount - 1;

            grdConsumableList.View.SetFocusedRowCellValue("ITEMID", e.ProductDataRow["ITEMID"]);
            grdConsumableList.View.SetFocusedRowCellValue("MASTERDATACLASSID", e.ProductDataRow["MASTERDATACLASSID"]);
            grdConsumableList.View.SetFocusedRowCellValue("DEFAULTCODE", e.ProductDataRow["DEFAULTCODE"]);
            grdConsumableList.View.SetFocusedRowCellValue("ITEMNAME", e.ProductDataRow["ITEMNAME"]);
            grdConsumableList.View.SetFocusedRowCellValue("SPEC", e.ProductDataRow["SPEC"]);
            grdConsumableList.View.SetFocusedRowCellValue("MATERIALQUALITY", e.ProductDataRow["MATERIALQUALITY"]);

            grdConsumableList.View.SetFocusedRowCellValue("WIDTH", e.ProductDataRow["WIDTH"]);
            grdConsumableList.View.SetFocusedRowCellValue("LENGTH", e.ProductDataRow["LENGTH"]);
            grdConsumableList.View.SetFocusedRowCellValue("LME", e.ProductDataRow["LME"]);
            grdConsumableList.View.SetFocusedRowCellValue("ORIGIN", e.ProductDataRow["ORIGIN"]);
            grdConsumableList.View.SetFocusedRowCellValue("MAKER", e.ProductDataRow["MAKER"]);
            grdConsumableList.View.SetFocusedRowCellValue("CONVERSIONFACTOR", e.ProductDataRow["CONVERSIONFACTOR"]);
            grdConsumableList.View.SetFocusedRowCellValue("PROCUREMENT", e.ProductDataRow["PROCUREMENT"]);
            grdConsumableList.View.SetFocusedRowCellValue("ACCOUNTCODE", e.ProductDataRow["ACCOUNTCODE"]);
            grdConsumableList.View.SetFocusedRowCellValue("MATERIALCLASS", e.ProductDataRow["MATERIALCLASS"]);
            grdConsumableList.View.SetFocusedRowCellValue("UNITOFSTOCK", e.ProductDataRow["UNITOFSTOCK"]);

            grdConsumableList.View.SetFocusedRowCellValue("UNITOFPURCHASING", e.ProductDataRow["UNITOFPURCHASING"]);
            grdConsumableList.View.SetFocusedRowCellValue("COLOR", e.ProductDataRow["COLOR"]);

            grdConsumableList.View.SetFocusedRowCellValue("ENTERPRISEID", e.ProductDataRow["ENTERPRISEID"]);
            grdConsumableList.View.SetFocusedRowCellValue("PLANTID", e.ProductDataRow["PLANTID"]);
            grdConsumableList.View.SetFocusedRowCellValue("VALIDSTATE", "Valid");

            grdConsumableList.View.SetFocusedRowCellValue("CUSTOMERITEMID", e.ProductDataRow["CUSTOMERITEMID"]);
            grdConsumableList.View.SetFocusedRowCellValue("DESCRIPTION", e.ProductDataRow["DESCRIPTION"]);
            grdConsumableList.View.SetFocusedRowCellValue("MASTERDATACLASSID", e.ProductDataRow["ENDTYPE"]);
            grdConsumableList.View.SetFocusedRowCellValue("ISINCOMMINGINSPECTION", e.ProductDataRow["ISINCOMMINGINSPECTION"]);

            grdConsumableList.View.SetFocusedRowCellValue("LOTCONTROL", e.ProductDataRow["LOTCONTROL"]);
            grdConsumableList.View.SetFocusedRowCellValue("LIMITATION", e.ProductDataRow["LIMITATION"]);
            grdConsumableList.View.SetFocusedRowCellValue("COPPERTYPE", e.ProductDataRow["COPPERTYPE"]);
            grdConsumableList.View.SetFocusedRowCellValue("ADH", e.ProductDataRow["ADH"]);
            grdConsumableList.View.SetFocusedRowCellValue("THICK", e.ProductDataRow["THICK"]);
            grdConsumableList.View.SetFocusedRowCellValue("HORIZONTALVERTICAL", e.ProductDataRow["HORIZONTALVERTICAL"]);
        }
    }
}

