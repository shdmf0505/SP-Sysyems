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
using Micube.Framework.SmartControls.Validations;
#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프로그램명 : 기준정보 > 사양진행현황 > 모델등록 & 진행현황
    /// 업 무 설명 : 
    /// 생  성  자 : 윤성원
    /// 생  성  일 : 2019-06-27
    /// 수정 이 력 : 2019-11-29 정승원 설계변경
    ///                  2020-01-02 장선미 삭제로직 추가
    /// 
    /// 
    /// </summary> 
	public partial class Governance2MaterialSpec : SmartConditionManualBaseForm
    {
        #region Local Variables
		/// <summary>
		/// 거버넌스 NO
		/// </summary>
		private string _governanceNo = string.Empty;
		private string _governanceType = string.Empty;

        private DataTable _inputDt = new DataTable();
        #endregion

        #region 생성자
        public Governance2MaterialSpec()
        {
            InitializeComponent();
        }
        #endregion

        #region 조회 조건 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

			//고객사
			InitializeCondition_CustomerPopup();
			//1
			//InitializeGrid_InventoryCgPopup();
			//품목코드
			InitializeCondition_ProductPopup();
			//사양담당
			InitializeCondition_Customer();
			//CAM담당
			InitializeCondition_CamOwner();
		}

		/// <summary>
		/// 조회조건의 컨트롤을 추가한다.
		/// </summary>
		protected override void InitializeConditionControls()
		{
			base.InitializeConditionControls();

			//다국어 변경
			SmartSelectPopupEdit specOwner = Conditions.GetControl<SmartSelectPopupEdit>("P_SPECOWNER");
			specOwner.LanguageKey = "SPECOWNER";
			specOwner.LabelText = Language.Get(specOwner.LanguageKey);

			SmartSelectPopupEdit customer = Conditions.GetControl<SmartSelectPopupEdit>("P_CUSTOMER");
			customer.LanguageKey = "COMPANYCLIENT";
			customer.LabelText = Language.Get(customer.LanguageKey);

			SmartSelectPopupEdit camOwner = Conditions.GetControl<SmartSelectPopupEdit>("P_CAMOWNER");
			camOwner.LanguageKey = "CAMOWNER";
			camOwner.LabelText = Language.Get(camOwner.LanguageKey);

			Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").ReadOnly = true;
            Conditions.GetControl<SmartSelectPopupEdit>("P_ITEMID").EditValueChanged += ProductDefIDChanged;

            /*
			SmartSelectPopupEdit category = Conditions.GetControl<SmartSelectPopupEdit>("P_INVENTORYCATEGORY");
			category.LanguageKey = "PRODUCTTYPE";
			category.LabelText = Language.Get(category.LanguageKey); 
			*/
        }

        private void ProductDefIDChanged(object sender, EventArgs e)
        {
            SmartSelectPopupEdit PopProdutid = sender as SmartSelectPopupEdit;

            if (Format.GetFullTrimString(PopProdutid.EditValue).Equals(string.Empty))
            {
                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = string.Empty;
            }
        }
        /// <summary>
        /// 팝업형 조회조건 생성 - 고객사
        /// </summary>
        private void InitializeCondition_CustomerPopup()
		{
			var conditionCustomer = Conditions.AddSelectPopup("P_CUSTOMER", new SqlQuery("GetCustomerList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID")
				.SetPopupLayout("SELECTCUSTOMERID", PopupButtonStyles.Ok_Cancel, true, false)
				.SetPopupLayoutForm(800, 800)
				.SetPopupAutoFillColumns("CUSTOMERNAME")
				.SetLabel("CUSTOMERID")
				.SetPosition(2.5)
				.SetPopupResultCount(0);

			// 팝업 조회조건
			conditionCustomer.Conditions.AddTextBox("TXTCUSTOMERID");

			// 팝업 그리드
			conditionCustomer.GridColumns.AddTextBoxColumn("CUSTOMERID", 150);
			conditionCustomer.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 200);
		}

		/// <summary>
		/// 팝업형 조회조건 생성 - 제품범주
		/// </summary>
		private void InitializeGrid_InventoryCgPopup()
		{
			var parentPopupColumn = Conditions.AddSelectPopup("P_INVENTORYCATEGORY", new SqlQuery("GetCategoryPopup", "10021", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetPopupLayout("INVENTORYCATEGORY", PopupButtonStyles.Ok_Cancel, true, false)
				.SetPopupResultCount(1)
				.SetPopupResultMapping("INVENTORYCATEGORY", "CATEGORYID")
				.SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
				.SetPosition(2.6)
				.SetLabel("INVENTORYCATEGORY");

			parentPopupColumn.Conditions.AddComboBox("PARENTCATEGORYID", new SqlQuery("GetCategoryMidList", "10001", "TOPPARENTCATEGORYID=Inventory"), "CATEGORYNAME", "CATEGORYID").SetValidationIsRequired();
			parentPopupColumn.Conditions.AddTextBox("CATEGORYNAME");

			// 팝업 그리드 설정
			parentPopupColumn.GridColumns.AddTextBoxColumn("CATEGORYID", 150);
			parentPopupColumn.GridColumns.AddTextBoxColumn("CATEGORYNAME", 200);

		}

		/// <summary>
		/// 팝업형 조회조건 생성 - 품목
		/// </summary>
		private void InitializeCondition_ProductPopup()
		{
			// SelectPopup 항목 추가
			var conditionProductId = Conditions.AddSelectPopup("P_ITEMID", new SqlQuery("GetItemMasterList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "ITEM", "ITEM")
				.SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
				.SetPopupLayoutForm(800, 800)
				.SetPopupAutoFillColumns("ITEMNAME")
				.SetLabel("ITEMID")
				.SetPosition(4.2)
				.SetPopupResultCount(0)
				.SetPopupApplySelection((selectRow, gridRow) => {
					string productDefName = "";

					selectRow.AsEnumerable().ForEach(r => {
						productDefName += Format.GetString(r["ITEMNAME"]) + ",";
					});

					Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = productDefName.TrimEnd(',');
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
		/// 팝업형 조회조건 생성 - 사양담당
		/// </summary>
		private void InitializeCondition_Customer()
		{
            //var conditionCustomer = Conditions.AddSelectPopup("P_SPECOWNER", new SqlQuery("GetUserList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "USERNAME", "USERID")
            //	.SetPopupLayout("SELECTSPECOWNER", PopupButtonStyles.Ok_Cancel, true, false)
            //	.SetPopupLayoutForm(800, 800)
            //	.SetPopupAutoFillColumns("DEPARTMENT")
            //	.SetLabel("USERID")
            //	.SetPosition(9.5)
            //	.SetPopupResultCount(0);

            var conditionCustomer = Conditions.AddSelectPopup("P_SPECOWNER", new SqlQuery("SelectUserGroupUserSearch", "10001", $"P_USERGROUPID={"SPECOWNER"}"), "USERNAME", "USERID")
                .SetPopupLayout("SELECTSPECOWNER", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("DEPARTMENT")
                .SetLabel("USERID")
                .SetPosition(9.5)
                .SetPopupResultCount(0);


            // 팝업 조회조건
            conditionCustomer.Conditions.AddTextBox("USERIDNAME");

			// 팝업 그리드
			conditionCustomer.GridColumns.AddTextBoxColumn("USERID", 150);
			conditionCustomer.GridColumns.AddTextBoxColumn("USERNAME", 200);
			conditionCustomer.GridColumns.AddTextBoxColumn("DEPARTMENT", 200);
		}

		/// <summary>
		/// 팝업형 조회조건 - CAM담당
		/// </summary>
		private void InitializeCondition_CamOwner()
		{
			var conditionCustomer = Conditions.AddSelectPopup("P_CAMOWNER", new SqlQuery("SelectUserGroupUserSearch", "10001", $"P_USERGROUPID={"CAMOWNER"}"), "USERNAME", "USERID")
							.SetPopupLayout("SELECTCAMOWNER", PopupButtonStyles.Ok_Cancel, true, false)
							.SetPopupLayoutForm(800, 800)
							.SetPopupAutoFillColumns("DEPARTMENT")
							.SetLabel("USERID")
							.SetPosition(9.5)
							.SetPopupResultCount(0);

			// 팝업 조회조건
			conditionCustomer.Conditions.AddTextBox("USERIDNAME");

			// 팝업 그리드
			conditionCustomer.GridColumns.AddTextBoxColumn("USERID", 150);
			conditionCustomer.GridColumns.AddTextBoxColumn("USERNAME", 200);
			conditionCustomer.GridColumns.AddTextBoxColumn("DEPARTMENT", 200);
		}
		#endregion

		#region 컨텐츠 영역 초기화
		/// <summary>
		/// 컨텐츠 영역 초기화
		/// </summary>
		protected override void InitializeContent()
		{
			base.InitializeContent();

			InitializeEvent();

			InitializeGrid();
			InitializeControl();
		}

		/// <summary>
		/// 그리드 초기화
		/// </summary>
		private void InitializeGrid()
		{
			grdGovernance.View.SetIsReadOnly();
			grdGovernance.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdGovernance.GridButtonItem = GridButtonItem.Export | GridButtonItem.Delete;

            //접수일
            grdGovernance.View.AddTextBoxColumn("RECEPTDATE", 100).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            //고객사
            grdGovernance.View.AddTextBoxColumn("CUSTOMERID", 100).SetIsHidden();
			grdGovernance.View.AddTextBoxColumn("CUSTOMERNAME", 150).SetIsReadOnly();
			//고객사버전
			grdGovernance.View.AddTextBoxColumn("CUSTOMERVERSION", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdGovernance.View.AddTextBoxColumn("PRODUCTTYPE", 60).SetIsReadOnly().SetIsHidden();
            grdGovernance.View.AddTextBoxColumn("LAYER", 60).SetIsReadOnly().SetIsHidden();        //층수

            /*
			grdGovernance.View.AddComboBoxColumn("CATEGORYID", 70, new SqlQuery("GetCategoryPopup", "10001", "TOPPARENTCATEGORYID=Inventory", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CATEGORYNAME", "CATEGORYID")
				.SetTextAlignment(TextAlignment.Center)
				.SetLabel("INVENTORYCATEGORY");
			*/
            //작업구분
            grdGovernance.View.AddComboBoxColumn("JOBTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=JobType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetTextAlignment(TextAlignment.Center)
				.SetLabel("OSPETCTYPENAME")
				.SetIsReadOnly();
			//생산구분
			grdGovernance.View.AddComboBoxColumn("PRODUCTIONTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetTextAlignment(TextAlignment.Center)
				.SetIsReadOnly();
			//모델ID
			grdGovernance.View.AddTextBoxColumn("MODELNO", 210).SetIsReadOnly();
			//품목코드
			grdGovernance.View.AddTextBoxColumn("PRODUCTDEFID", 80)
                .SetTextAlignment(TextAlignment.Center)
				.SetIsReadOnly()
				.SetIsHidden();
			//품목버전
			grdGovernance.View.AddTextBoxColumn("PRODUCTDEFVERSION", 60)
                .SetTextAlignment(TextAlignment.Center)
				.SetIsReadOnly()
				.SetIsHidden();
			//품목명
			grdGovernance.View.AddTextBoxColumn("ITEMNAME", 200).SetIsReadOnly().SetIsHidden();
            // 제품등급
            grdGovernance.View.AddTextBoxColumn("PRODUCTRATING", 60).SetIsReadOnly().SetIsHidden();
            //사양작업
            grdGovernance.View.AddComboBoxColumn("SPECWORKTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=GovernanceState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetTextAlignment(TextAlignment.Center).SetLabel("SPECWORKTYPENAME")
				.SetIsReadOnly();
			//거버넌스STATUS
			grdGovernance.View.AddTextBoxColumn("GOVERNANCESTATE").SetIsHidden();
			//CAM작업
			grdGovernance.View.AddComboBoxColumn("CAMWORKSTATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=CAMState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetTextAlignment(TextAlignment.Center)
				.SetLabel("CAMWORKSTATENAME")
				.SetIsReadOnly(); 
			//Holding
			grdGovernance.View.AddTextBoxColumn("ISHOLDING", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
			//사양담당
			grdGovernance.View.AddTextBoxColumn("SPECOWNERID", 80).SetTextAlignment(TextAlignment.Center).SetIsHidden();
			grdGovernance.View.AddTextBoxColumn("SPECOWNERNAME", 90).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
			//CAM담당
			grdGovernance.View.AddTextBoxColumn("CAMOWNERID", 80).SetTextAlignment(TextAlignment.Center).SetIsHidden();
			grdGovernance.View.AddTextBoxColumn("CAMOWNERNAME", 90).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
			//PNL SIZE
			grdGovernance.View.AddTextBoxColumn("PNLSIZE", 100).SetIsReadOnly().SetIsHidden();
			//합수
			grdGovernance.View.AddTextBoxColumn("ARRAY", 50).SetIsReadOnly().SetIsHidden();
			//표면도금타입1
			grdGovernance.View.AddTextBoxColumn("SURFACETYPE1", 100).SetIsReadOnly().SetIsHidden();
			//표면도금타입2
			grdGovernance.View.AddTextBoxColumn("SURFACETYPE2", 100).SetIsReadOnly().SetIsHidden();
			grdGovernance.View.AddTextBoxColumn("RECEPTDATE_YYYYMMDD", 130).SetIsHidden(); 
			//승인일
			grdGovernance.View.AddTextBoxColumn("APPROVEDATE", 130).SetTextAlignment(TextAlignment.Center).SetIsReadOnly().SetIsHidden();
			//투입일
			//grdGovernance.View.AddTextBoxColumn("INPUTDATE", 130).SetTextAlignment(TextAlignment.Center);
			//접수번호
			grdGovernance.View.AddTextBoxColumn("GOVERNANCENO", 130).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
			//거버넌스 타입
			grdGovernance.View.AddTextBoxColumn("GOVERNANCETYPE", 100).SetIsHidden();
			//영업담당
			grdGovernance.View.AddTextBoxColumn("SALESOWNERNAME", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
			//비고
			grdGovernance.View.AddTextBoxColumn("COMMENT", 150).SetIsReadOnly();

            grdGovernance.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdGovernance.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);


            grdGovernance.View.PopulateColumns();
		}

		/// <summary>
		/// 컨트롤 초기화
		/// </summary>
		private void InitializeControl()
        {
			//접수일
			txtReceptDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
			txtReceptDate.ReadOnly = true;
			txtReceptDate.Tag = "RECEPTDATE_YYYYMMDD";
			txtReceptDate.Properties.Mask.EditMask = "yyyy-MM-dd";

			//제품범주
			ConditionItemSelectPopup options13 = new ConditionItemSelectPopup();
			options13.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedDialog);
			options13.SetPopupLayout("SELECTCATEGORYID", PopupButtonStyles.Ok_Cancel, true, false);
			options13.Id = "CATEGORYID";
			options13.SearchQuery = new SqlQuery("GetCategoryPopup", "10021", $"ENTERPRISEID ={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
			options13.IsMultiGrid = false;
			options13.DisplayFieldName = "CATEGORYNAME";
			options13.ValueFieldName = "CATEGORYID";
			options13.LanguageKey = "INVENTORYCATEGORY";

			options13.Conditions.AddComboBox("PARENTCATEGORYID", new SqlQuery("GetCategoryMidList", "10001", "TOPPARENTCATEGORYID=Inventory"), "CATEGORYNAME", "CATEGORYID").SetValidationIsRequired();
			options13.Conditions.AddTextBox("CATEGORYNAME");

			options13.GridColumns.AddTextBoxColumn("CATEGORYID", 150);
			options13.GridColumns.AddTextBoxColumn("CATEGORYNAME", 200);


			//작업구분
			cboGovernanceType.DisplayMember = "CODENAME";
			cboGovernanceType.ValueMember = "CODEID";
			cboGovernanceType.ShowHeader = false;
            cboGovernanceType.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboGovernanceType.DataSource = SqlExecuter.Query("GetCodeList", "00001", new Dictionary<string, object>(){ { "CODECLASSID", "JobType" },
																															  { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
			cboGovernanceType.Tag = "JOBTYPE";

			//생산구분
			cboProductionType.DisplayMember = "CODENAME";
			cboProductionType.ValueMember = "CODEID";
			cboProductionType.ShowHeader = false;
            cboProductionType.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboProductionType.DataSource = SqlExecuter.Query("GetCodeList", "00001", new Dictionary<string, object>(){ { "CODECLASSID", "ProductionType" },
																															  { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
			cboProductionType.Tag = "PRODUCTIONTYPE";

			//고객사
			ConditionItemSelectPopup options12 = new ConditionItemSelectPopup();
			options12.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedDialog);
			options12.SetPopupLayout("SELECTCUSTOMERID", PopupButtonStyles.Ok_Cancel, true, false);
			options12.Id = "CUSTOMERID";
			options12.SearchQuery = new SqlQuery("GetCustomerList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
			options12.IsMultiGrid = false;
			options12.DisplayFieldName = "CUSTOMERNAME";
			options12.ValueFieldName = "CUSTOMERID";
			options12.LanguageKey = "CUSTOMER";
            options12.SetValidationIsRequired();

			options12.Conditions.AddTextBox("TXTCUSTOMERID");

			options12.GridColumns.AddTextBoxColumn("CUSTOMERID", 150);
			options12.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 200);

			popCustomerId.SelectPopupCondition = options12;
			popCustomerId.Tag = "CUSTOMERID";

			//고객사버전
			txtCustomerVersion.Text = string.Empty;
			txtCustomerVersion.Tag = "CUSTOMERVERSION";

			//모델ID
			txtModelId.Text = string.Empty;
			txtModelId.Tag = "MODELNO";
            
            //품목코드
            ConditionItemSelectPopup options11 = new ConditionItemSelectPopup();
			options11.SetPopupLayoutForm(800, 800, FormBorderStyle.FixedDialog);
			options11.SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false);
			options11.Id = "ITEMID";
			options11.SearchQuery = new SqlQuery("GetItemMasterList", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}");
			options11.IsMultiGrid = false;
			options11.DisplayFieldName = "ITEMID";
			options11.ValueFieldName = "ITEMID";
			options11.LanguageKey = "ITEMID";
            options11.SetPopupApplySelection((selectRow, gridRow) =>
            {
                DataRow row = selectRow.FirstOrDefault();
                //popItemId.Tag = row["ITEM"];
                txtItemVersion.Text = row["ITEMVERSION"].ToString();
                txtItemName.Text = row["ITEMNAME"].ToString();
            });

            options11.Conditions.AddTextBox("TXTITEM").SetLabel("PRODUCT");

			options11.GridColumns.AddTextBoxColumn("ITEMID", 150);
			options11.GridColumns.AddTextBoxColumn("ITEMVERSION", 50);
			options11.GridColumns.AddTextBoxColumn("ITEMNAME", 200);
			options11.GridColumns.AddTextBoxColumn("PNLSIZE", 100);
			options11.GridColumns.AddTextBoxColumn("LAYER", 60);
			options11.GridColumns.AddTextBoxColumn("SURFACETYPE1", 100);
			options11.GridColumns.AddTextBoxColumn("SURFACETYPE2", 100);

			popItemId.SelectPopupCondition = options11;
			popItemId.Tag = "PRODUCTDEFID";

			//품목버전
			txtItemVersion.ReadOnly = true;
			txtItemVersion.Tag = "PRODUCTDEFVERSION";

			//품목명
			txtItemName.ReadOnly = true;
			txtItemName.Tag = "ITEMNAME";

			//CAM작업
			ConditionItemSelectPopup options1 = new ConditionItemSelectPopup();
			options1.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedDialog);
			options1.SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, false);
			options1.Id = "USER";
            //options1.SearchQuery = new SqlQuery("GetUserList", "10001", $"PLANTID={UserInfo.Current.Plant}");
            options1.SearchQuery = new SqlQuery("SelectUserGroupUserSearch", "10001", $"P_USERGROUPID={"CAMOWNER"}");
            options1.IsMultiGrid = false;
			options1.DisplayFieldName = "USERNAME";
			options1.ValueFieldName = "USERID";
			options1.LanguageKey = "USER";

			options1.Conditions.AddTextBox("USERIDNAME");

			options1.GridColumns.AddTextBoxColumn("USERID", 150);
			options1.GridColumns.AddTextBoxColumn("USERNAME", 200);

			popCamOwner.SelectPopupCondition = options1;
			popCamOwner.Tag = "CAMOWNERID";

			//HOLDING
			cboIsHolding.DisplayMember = "CODENAME";
			cboIsHolding.ValueMember = "CODEID";
			cboIsHolding.ShowHeader = false;
			cboIsHolding.DataSource = SqlExecuter.Query("GetCodeList", "00001", new Dictionary<string, object>(){ { "CODECLASSID", "YesNo" },
																												  { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
			cboIsHolding.ItemIndex = 1;
			cboIsHolding.Tag = "ISHOLDING";

			//사양담당
			ConditionItemSelectPopup options2 = new ConditionItemSelectPopup();
			options2.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedDialog);
			options2.SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, false);
			options2.Id = "USER";
            //options2.SearchQuery = new SqlQuery("GetUserList", "10001", $"PLANTID={UserInfo.Current.Plant}");
            options2.SearchQuery = new SqlQuery("SelectUserGroupUserSearch", "10001", $"P_USERGROUPID={"SPECOWNER"}");
            options2.IsMultiGrid = false;
			options2.DisplayFieldName = "USERNAME";
			options2.ValueFieldName = "USERID";
			options2.LanguageKey = "USER";

			options2.Conditions.AddTextBox("USERIDNAME");

			options2.GridColumns.AddTextBoxColumn("USERID", 150);
			options2.GridColumns.AddTextBoxColumn("USERNAME", 200);

			popSpecOwner.SelectPopupCondition = options2;
			popSpecOwner.Tag = "SPECOWNERID";

			////CAM담당
			//ConditionItemSelectPopup options3 = new ConditionItemSelectPopup();
			//options3.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedDialog);
			//options3.SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, true);
			//options3.Id = "USER";
			//options3.SearchQuery = new SqlQuery("GetUserList", "10001", $"PLANTID={UserInfo.Current.Plant}");
			//options3.IsMultiGrid = false;
			//options3.DisplayFieldName = "USERNAME";
			//options3.ValueFieldName = "USERID";
			//options3.LanguageKey = "USER";

			//options3.Conditions.AddTextBox("USERIDNAME");

			//options3.GridColumns.AddTextBoxColumn("USERID", 150);
			//options3.GridColumns.AddTextBoxColumn("USERNAME", 200);

			//popCamOwner.SelectPopupCondition = options3;
			//popCamOwner.Tag = "CAMOWNERID";


			//영업담당
			ConditionItemSelectPopup options4 = new ConditionItemSelectPopup();
			options4.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedDialog);
			options4.SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, false);
			options4.Id = "USER";
            //options4.SearchQuery = new SqlQuery("GetUserList", "10001", $"PLANTID={UserInfo.Current.Plant}");
            options4.SearchQuery = new SqlQuery("SelectUserGroupUserSearch", "10001", $"P_USERGROUPID={"SALESOWNER"}");
            options4.IsMultiGrid = false;
			options4.DisplayFieldName = "USERNAME";
			options4.ValueFieldName = "USERID";
			options4.LanguageKey = "USER";

			options4.Conditions.AddTextBox("USERIDNAME");

			options4.GridColumns.AddTextBoxColumn("USERID", 150);
			options4.GridColumns.AddTextBoxColumn("USERNAME", 200);

			popSalesOwner.SelectPopupCondition = options4;
			popSalesOwner.Tag = "SALESOWNERID";

			//CAM 작업
			cboCamState.DisplayMember = "CODENAME";
			cboCamState.ValueMember = "CODEID";
			cboCamState.ShowHeader = false;
			cboCamState.DataSource = SqlExecuter.Query("GetCodeList", "00001", new Dictionary<string, object>(){ { "CODECLASSID", "CAMState" },
																														  { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
			cboCamState.Tag = "CAMWORKSTATE";

			//비고
			txtComment.Text = string.Empty;
			txtComment.Tag = "COMMENT";
		}

		#endregion

		#region 툴바

		/// <summary>
		/// 저장 버튼 클릭
		/// </summary>
		/// <returns></returns>
		protected override void OnToolbarSaveClick()
		{
			base.OnToolbarSaveClick();

			DataTable dt = new DataTable();
			dt.Columns.Add("REQUESTDATE", typeof(object));
			dt.Columns.Add("INVENTORYCATEGORY", typeof(object));
			dt.Columns.Add("JOBTYPE", typeof(object));
			dt.Columns.Add("PRODUCTIONTYPE", typeof(object));
			dt.Columns.Add("CUSTOMERID", typeof(object));
			dt.Columns.Add("CUSTOMERVERSION", typeof(object));
			dt.Columns.Add("MODELID", typeof(object));
			dt.Columns.Add("ITEMID", typeof(object));
			dt.Columns.Add("ITEMVERSION", typeof(object));
			dt.Columns.Add("ITEMNAME", typeof(object));
			dt.Columns.Add("ISHOLDING", typeof(object));
			dt.Columns.Add("SPECOWNERID", typeof(object));
			dt.Columns.Add("CAMOWNERID", typeof(object));
			dt.Columns.Add("SALESOWNERID", typeof(object));
			dt.Columns.Add("COMMENT", typeof(object));
			dt.Columns.Add("CAMSTATE", typeof(object));
			dt.Columns.Add("GOVERNANCENO", typeof(object));
			dt.Columns.Add("GOVERNANCETYPE", typeof(object));

			if (string.IsNullOrEmpty(txtModelId.Text))
			{
				//모델은 필수 입력입니다.
				ShowMessage("REQUIREDINPUTMODELNO");
				return;
			}

			if (popCustomerId.GetValue() == null || string.IsNullOrEmpty(popCustomerId.GetValue().ToString()))
			{
				//고객사는 필수 입력입니다.
				ShowMessage("REQUIREDINPUTCUSTOMER");
				return;
			}

			/*
			if (cboGovernanceType.Editor.GetDataValue() == null)
			{
				//작업구분은 필수 입력입니다.
				ShowMessage("REQUIREDINPUTGOVERNANCETYPE");
				return;
			}
			*/

			if (Format.GetString(cboGovernanceType.GetDataValue()).Equals("RunningChange"))
			{
				//R/C 등록은 'RunningChange 등록' 화면에서 가능합니다.
				ShowMessage("NOTINSERTRUNNINGCHANGE");
				return;
			}

			DataRow row = dt.NewRow();
			row["REQUESTDATE"] = txtReceptDate.Text;
			row["JOBTYPE"] = cboGovernanceType.GetDataValue();
			row["PRODUCTIONTYPE"] = cboProductionType.GetDataValue();
			row["CUSTOMERID"] = popCustomerId.GetValue().ToString().Contains("=") ? popCustomerId.GetValue().ToString().Split('=')[0] : popCustomerId.GetValue();
			row["CUSTOMERVERSION"] = txtCustomerVersion.Text;
			row["MODELID"] = txtModelId.Text;
			row["ITEMID"] = popItemId.GetValue().ToString().Contains("=") ? popItemId.GetValue().ToString().Split('=')[0] : popItemId.GetValue();
			row["ITEMVERSION"] = txtItemVersion.Text;
			row["ITEMNAME"] = txtItemName.Text;
			row["ISHOLDING"] = cboIsHolding.GetDataValue();
			row["SPECOWNERID"] = popSpecOwner.GetValue().ToString().Contains("=") ? popSpecOwner.GetValue().ToString().Split('=')[0] : popSpecOwner.GetValue();
			row["CAMOWNERID"] = popCamOwner.GetValue().ToString().Contains("=") ? popCamOwner.GetValue().ToString().Split('=')[0] : popCamOwner.GetValue();
			row["SALESOWNERID"] = popSalesOwner.GetValue().ToString().Contains("=") ? popSalesOwner.GetValue().ToString().Split('=')[0] : popSalesOwner.GetValue();
			row["COMMENT"] = txtComment.Text;
			row["CAMSTATE"] = cboCamState.GetDataValue();
			row["GOVERNANCENO"] = _governanceNo;
			row["GOVERNANCETYPE"] = "NewRequest";

			dt.Rows.Add(row);

			string saveType = ReturnSaveState();

			MessageWorker worker = new MessageWorker("SaveGovernance");
			worker.SetBody(new MessageBody()
			{
				{ "saveState", saveType },
				{ "requester", UserInfo.Current.Id },
				{ "enterpriseId", UserInfo.Current.Enterprise },
				{ "plantId", UserInfo.Current.Plant },
				{ "governanceList", dt },
                { "governanceDeleteList", grdGovernance.GetChangesDeleted() }       // 삭제 추가

			});

			worker.Execute();

		}

		#endregion

		#region 검색

		/// <summary>
		/// 비동기 override 모델
		/// </summary>
		protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

			//초기화
			grdGovernance.View.ClearDatas();
			//txtModelId.ReadOnly = false;
			//popCustomerId.ReadOnly = false;

			var values = Conditions.GetValues();
			values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
			values.Add("PLANTID", UserInfo.Current.Plant);
			values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

			DataTable dt = await QueryAsync("SelectGovernanceList", "10003", values);

			if (dt.Rows.Count < 1)
			{
				// 조회할 데이터가 없습니다.
				ShowMessage("NoSelectData");
				return;
			}

			AddMaterialTypeColumnToGrid(dt);
			grdGovernance.DataSource = dt;

			int iHandle = grdGovernance.View.GetRowHandleByValue("GOVERNANCENO", _governanceNo);

            grdGovernance.View.FocusedRowChanged -= View_FocusedRowChanged;
            grdGovernance.View.FocusedRowHandle = (iHandle <= 0) ? 0 : iHandle;
			grdGovernance.View.SelectRow(grdGovernance.View.FocusedRowHandle);
            grdGovernance.View.FocusedRowChanged += View_FocusedRowChanged;

            DataRow row = grdGovernance.View.GetFocusedDataRow();
			FocusedRowDataBind(row);
            _inputDt = GetRowDataByDataTable(_inputDt, row);
        }

		/// <summary>
		/// column 생성
		/// </summary>
		/// <param name="dt"></param>
		private void AddMaterialTypeColumnToGrid(DataTable dt)
		{
			//grdGovernance.DataSource = null;

			//InitializeGrid();
			DataTable grdDt = grdGovernance.DataSource as DataTable;
			foreach (DataColumn col in dt.Columns)
			{
				if(!grdDt.Columns.Contains(col.ColumnName))
				{
					var grdCol = grdGovernance.View.AddTextBoxColumn(col.ColumnName, 100);
					if (col.ColumnName.Equals("SERIALNO")
					|| col.ColumnName.Equals("CATEGORYID")
					|| col.ColumnName.Equals("SPECWORKTYPENAME")
					|| col.ColumnName.Equals("CAMWORKSTATENAME")
					|| col.ColumnName.Equals("SALESOWNERID")
					|| col.ColumnName.Equals("ENTERPRISEID")
					|| col.ColumnName.Equals("PLANTID")
					|| col.ColumnName.Equals("SPECAPPROVALDATE"))
					{
						grdCol.SetIsHidden();
					}
				}
			}

			grdGovernance.View.PopulateColumns();
		}

		#endregion

		#region 이벤트
		private void InitializeEvent()
        {
			grdGovernance.View.FocusedRowChanged += View_FocusedRowChanged;
            grdGovernance.View.RowStyle += View_RowStyle;

			popItemId.EditValueChanged += Editor_EditValueChanged;

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void View_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
			if (e.RowHandle < 0)
				return;

			if ( grdGovernance.View.GetRowCellValue(e.RowHandle, "ISHOLDING") != null && grdGovernance.View.GetRowCellValue(e.RowHandle, "ISHOLDING").ToString() == "Y")
            {
                e.HighPriority = true;
                e.Appearance.ForeColor = Color.White;
				e.Appearance.BackColor = Color.Red;
            }
        }

        /// <summary>
        /// 품목 팝업 VALUE 변경 시 이벤트 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Editor_EditValueChanged(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(Format.GetString(popItemId.EditValue)))
			{
				popItemId.Tag = "PRODUCTDEFID";
				txtItemVersion.Text = string.Empty;
				txtItemName.Text = string.Empty;
			}
			else
			{
                //2019-12-06 : 변경된 값으로 다시 바인딩되어야 함.  아래 로직은 이전 등록된 품목정보를 바인딩해줌.
				//if(popItemId.Tag.Equals("PRODUCTDEFID"))
				//{
				//	DataRow row = grdGovernance.View.GetFocusedDataRow();
				//	if(row == null) return;

				//	txtItemVersion.Text = Format.GetString(row["PRODUCTDEFVERSION"]);
				//	txtItemName.Text = Format.GetString(row["ITEMNAME"]);
				//}
				//else
				//{ 
				//	string product = Format.GetString(popItemId.Tag);
				//	txtItemVersion.Text = product.Split('|')[1];
				//	txtItemName.Text = product.Split('|')[2];
				//}
			}
		}

		/// <summary>
		/// row 포커스 변경 시 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			//if(grdGovernance.View.FocusedRowHandle < 0) return;
            //
			//DataRow selectRow = grdGovernance.View.GetFocusedDataRow();
            //
			//FocusedRowDataBind(selectRow);

            //			if(!isRunNextProcess) return;

            //포커된 ROW의 KEY 값을 넣어줌(저장 상태 체크)
            //_governanceNo = Format.GetString(selectRow["GOVERNANCENO"]);
            //_governanceType = Format.GetString(selectRow["GOVERNANCETYPE"]);

            if (grdGovernance.View.FocusedRowHandle < 0 || e.PrevFocusedRowHandle < 0) return;

            DataRow selectRow = grdGovernance.View.GetFocusedDataRow();

            DataTable compareDt = new DataTable();
            compareDt = GetRowDataByDataTable(compareDt, grdGovernance.View.GetDataRow(e.PrevFocusedRowHandle));

            DialogResult result = DialogResult.None;
            bool isChanged = CheckDifferentInputData(_inputDt, compareDt, out result);

            if (isChanged || result == DialogResult.None)
            {
                FocusedRowDataBind(selectRow);
            }
            else
            {
                grdGovernance.View.FocusedRowChanged -= View_FocusedRowChanged;
                grdGovernance.View.SelectRow(e.PrevFocusedRowHandle);
                grdGovernance.View.FocusedRowHandle = e.PrevFocusedRowHandle;
                grdGovernance.View.FocusedRowChanged += View_FocusedRowChanged;
            }

            _inputDt = GetRowDataByDataTable(_inputDt, selectRow);
        }

        /// <summary>
		/// 변경된 값 비교
		/// </summary>
		private bool CheckDifferentInputData(DataTable original, DataTable compare, out DialogResult result)
        {
            bool isDifference = false;
            result = DialogResult.None;

            if (original == null || compare == null) return isDifference;
            if (original.Rows.Count <= 0 || compare.Rows.Count <= 0) return isDifference;

            foreach (DataColumn oCol in original.Columns)
            {
                foreach (DataColumn cCol in compare.Columns)
                {
                    if (oCol.ColumnName.Equals(cCol.ColumnName)
                    && Format.GetString(oCol.Table.Rows[0][oCol.ColumnName]) != Format.GetString(cCol.Table.Rows[0][cCol.ColumnName]))
                    {
                        //입력한 내용이 있으면 현재 작성중인 내용인 모두 삭제됩니다.
                        result = ShowMessageBox("WRITTENBEDELETE", Language.Get("MESSAGEINFO"), MessageBoxButtons.OKCancel);
                        if (result == DialogResult.OK)
                        {
                            isDifference = true;
                        }
                        break;
                    }
                }//foreach

                if (isDifference || result == DialogResult.Cancel)
                {
                    break;
                }

            }//foreach

            return isDifference;
        }

        /// <summary>
        /// 일부 ROW 데이터를 DataTable에 넣기
        /// </summary>
        /// <param name="row"></param>
        private DataTable GetRowDataByDataTable(DataTable dt, DataRow row)
        {
            if (row == null) return null;

            dt = row.Table.Clone();

            dt.ImportRow(row);
            dt.AcceptChanges();

            dt.Rows[0]["JOBTYPE"] = cboGovernanceType.GetDataValue();
            dt.Rows[0]["PRODUCTIONTYPE"] = cboProductionType.GetDataValue();
            dt.Rows[0]["CUSTOMERID"] = popCustomerId.GetValue();
            dt.Rows[0]["MODELNO"] = txtModelId.EditValue;
            dt.Rows[0]["CUSTOMERVERSION"] = txtCustomerVersion.EditValue;
            dt.Rows[0]["CAMOWNERID"] = popCamOwner.GetValue();
            dt.Rows[0]["CAMWORKSTATE"] = cboCamState.GetDataValue();
            dt.Rows[0]["SPECOWNERID"] = popSpecOwner.GetValue();
            dt.Rows[0]["SALESOWNERID"] = popSalesOwner.GetValue();
            dt.Rows[0]["COMMENT"] = txtComment.EditValue;
            dt.Rows[0]["ISHOLDING"] = cboIsHolding.EditValue;

            return dt;
        }

        /// <summary>
        /// 툴바 버튼 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

			string buttonId = btn.Name.ToString();
			DataRow selectRow = null;
			switch (buttonId)
			{
				case "PaymentRequest":
					selectRow = grdGovernance.View.GetFocusedDataRow();
					if (selectRow == null) return;


					if (string.IsNullOrEmpty(Format.GetString(selectRow["GOVERNANCENO"])) || string.IsNullOrEmpty(Format.GetString(selectRow["GOVERNANCETYPE"])))
					{
						//모델 등록을 먼저 진행하세요.
						ShowMessage("FIRSTSETMODELNO");
						return;
					}

					if (string.IsNullOrEmpty(Format.GetString(selectRow["PRODUCTDEFID"])) || string.IsNullOrEmpty(Format.GetString(selectRow["PRODUCTDEFVERSION"])))
					{
						//품목 매핑을 먼저 진행하세요.
						ShowMessage("FIRSTSETITEMMASTER");
						return;
					}


					if (string.IsNullOrEmpty(Format.GetString(selectRow["SPECOWNERID"])) || string.IsNullOrEmpty(Format.GetString(selectRow["SPECOWNERNAME"])))
					{
						//사양담당자를 지정하세요.
						ShowMessage("SETSPECOWNER");
						return;
					}
					SetApproveListPopup popup = new SetApproveListPopup(Format.GetString(selectRow["GOVERNANCENO"]),
																		Format.GetString(selectRow["GOVERNANCETYPE"]),
																		Format.GetString(selectRow["SPECOWNERID"]),
																		Format.GetString(selectRow["SPECOWNERNAME"]));
					popup.ShowDialog();


					OnSearchAsync();
					break;

				case "Initialization":
					_governanceNo = string.Empty;
					_governanceType = string.Empty;

					//txtModelId.ReadOnly = false;
					//popCustomerId.ReadOnly = false;

					txtReceptDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
					cboGovernanceType.EditValue = string.Empty;
					cboProductionType.EditValue = string.Empty;
					popCustomerId.ClearValue();
					txtCustomerVersion.Text = string.Empty;
					txtModelId.Text = string.Empty;
					popItemId.ClearValue();
					txtItemVersion.Text = string.Empty;
					txtItemName.Text = string.Empty;
					popCamOwner.ClearValue();
					popSpecOwner.ClearValue();
					popSalesOwner.ClearValue();
					cboCamState.EditValue = string.Empty;
					cboIsHolding.EditValue = "N";
					txtComment.Text = string.Empty;
					break;
				
				case "InputMaterialSpec":
					selectRow = grdGovernance.View.GetFocusedDataRow();
					if (selectRow == null) return;

					GovernanceMaterialSpecPopup materialSpecPopup = new GovernanceMaterialSpecPopup(_governanceNo);
					materialSpecPopup.ShowDialog();

					int iHandle = grdGovernance.View.GetRowHandleByValue("GOVERNANCENO", _governanceNo);

					var param = Conditions.GetValues();
					grdGovernance.DataSource = SqlExecuter.Query("SelectGovernanceList", "10003", param);

					grdGovernance.View.FocusedRowHandle = (iHandle <= 0) ? 0 : iHandle;
					grdGovernance.View.SelectRow(grdGovernance.View.FocusedRowHandle);

					//OnSearchAsync();
					break;
			}

        }
        #endregion

        #region Private Function

        /// <summary>
        /// 저장 상태 리턴
        /// </summary>
        /// <returns></returns>
        private string ReturnSaveState()
		{
			return (string.IsNullOrEmpty(_governanceNo) && string.IsNullOrEmpty(_governanceType)) == true ? "added" : "modified";
		}

		/// <summary>
		/// ROW 데이터 바인트 
		/// </summary>
		private bool FocusedRowDataBind(DataRow row)
		{
			if (row == null)
			{
				_governanceNo = string.Empty;
				_governanceType = string.Empty;

				return false;
			}
			else
			{
				RowDataBindInTableControls(row);
				//txtModelId.ReadOnly = true;
				//popCustomerId.ReadOnly = true;

				//포커된 ROW의 KEY 값을 넣어줌(저장 상태 체크)
				_governanceNo = Format.GetString(row["GOVERNANCENO"]);
				_governanceType = Format.GetString(row["GOVERNANCETYPE"]);

				return true;
			}
			
		}

		/// <summary>
		/// SmartSplitTableLayoutPanel 안에 있는 컨트롤에 row데이터 바인드
		/// </summary>
		private void RowDataBindInTableControls(DataRow r)
		{
			foreach(Control ctl in tlpInputInfo.Controls)
			{
				string tag = Format.GetString(ctl.Tag);
				if (string.IsNullOrEmpty(Format.GetString(ctl.Tag))) continue;

				switch (ctl.GetType().Name)
				{
					case "SmartSelectPopupEdit":
						SmartSelectPopupEdit pop = ctl as SmartSelectPopupEdit;
                        if (!string.IsNullOrWhiteSpace(Format.GetString(r[tag])))
                        {
                            if (tag.EndsWith("ID"))
                            {
                                if (r.Table.Columns.Contains(tag.Remove(tag.Length - 2) + "NAME"))
                                    pop.SetValue(r[tag] + "=" + r[tag.Remove(tag.Length - 2) + "NAME"]);
                                else
                                    pop.SetValue(r[tag]);
                            }
                            else
							{
								pop.SetValue(r[tag]);
							}
                        }
                        else
						{
							pop.ClearValue();
						}

                        break;
					case "SmartComboBox":
						SmartComboBox cbo = ctl as SmartComboBox;
						cbo.EditValue = r[tag];

						break;
					case "SmartTextBox":
					case "SmartMemoEdit":
						ctl.Text = r[tag].ToString();
						break;
				}
			}
		}


		#endregion

	}
}
