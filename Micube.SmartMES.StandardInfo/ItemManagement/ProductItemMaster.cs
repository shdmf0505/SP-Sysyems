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
using Micube.Framework.SmartControls.Grid.BandedGrid;
using DevExpress.Utils.Menu;

#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품목관리 > 품목등록(사양)
    /// 업  무  설  명  : 자재품목등록
    /// 생    성    자  :  윤성원
    /// 생    성    일  : 2019-06-28
    /// 수  정  이  력  : 
    ///         2021.04.21 전우성 : 사용층 필드 추가
    /// </summary>
	public partial class ProductItemMaster : SmartConditionManualBaseForm
    {
        // TODO : 그리드 컬럼중 PK를 NewRow 일때만 수정가능하도록 변경

        #region Local Variables
        DataRow[] globalrow = null; // 선택된 데이터로우 복사 

        ConditionItemComboBox _layerColumn = null;
        private List<DXMenuItem> menuList = new List<DXMenuItem>();
        #endregion

        #region 생성자
        public ProductItemMaster()
        {
            InitializeComponent();
            InitializeGrid();
            InitializeEvent();
            InitializeQuickMenuList();
        }
        #endregion

        #region 컨텐츠 영역 초기화


        public override void LoadForm(Dictionary<string, object> parameters)
        {
            base.LoadForm(parameters);
            if (parameters != null)
            {
                _parameters = parameters;
                Conditions.SetValue("P_PRODUCTDEFID", 0, parameters["ITEMID"]);
                Conditions.SetValue("P_PRODUCTDEFVERSION", 0, parameters["ITEMVERSION"]);
                Conditions.SetValue("p_ProductName", 0, parameters["ITEMNAME"]);
                OnSearchAsync();
            }
        }
        
        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdMain.GridButtonItem = GridButtonItem.All;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
			grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
  
            grdMain.View.AddTextBoxColumn("MODELCODE", 100).SetLabel("HIGHITEMCODE").SetIsHidden();
            grdMain.View.AddTextBoxColumn("ITEMID", 100);
            grdMain.View.AddTextBoxColumn("ITEMVERSION", 50).SetLabel("PRODUCTDEFVERSION");
            grdMain.View.AddTextBoxColumn("ITEMNAME", 200).SetValidationIsRequired();

            // 이용희 과장 요청 (2020-03-12)
            grdMain.View.AddTextBoxColumn("SPEC", 150);

            InitializeGrid_CustCgPopup(); // 고객 ID
            grdMain.View.AddTextBoxColumn("CUSTOMERNAME", 150).SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();

            grdMain.View.AddComboBoxColumn("MASTERDATACLASSID", 80, new SqlQuery("GetmasterdataclassList", "10001", "ITEMOWNER=Specifications", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "MASTERDATACLASSNAME", "MASTERDATACLASSID")
            .SetValidationIsRequired();

            grdMain.View.AddComboBoxColumn("PRODUCTTYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetEmptyItem("", "")
                .SetTextAlignment(TextAlignment.Center);
                // 제품 타입도 제품만 필수
                //.SetValidationIsRequired();

            grdMain.View.AddComboBoxColumn("LAYER", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Layer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))        // 층수'
                .SetEmptyItem("", "")
                .SetTextAlignment(TextAlignment.Center);
                //층수 -> 제품은 필수/반제품 옵션
                //.SetValidationIsRequired();

            InitializeGrid_SaleCgPopup();//판매범주
            grdMain.View.AddComboBoxColumn("PRODUCTRATING", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductLevel", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")) // 제품등급
                .SetEmptyItem("", "")
                .SetTextAlignment(TextAlignment.Center);

            grdMain.View.AddComboBoxColumn("ITEMUOM", 60, new SqlQuery("GetUOMList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "UOMDEFNAME", "UOMDEFID")
                .SetTextAlignment(TextAlignment.Center)
                .SetValidationIsRequired();

            grdMain.View.AddComboBoxColumn("CONSUMABLETYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ConsumableType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetValidationIsRequired();

            grdMain.View.AddComboBoxColumn("ITEMSTATUS", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ItemStatus", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            grdMain.View.AddComboBoxColumn("PRODUCTIONTYPE", 60,  new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetValidationIsRequired();
            grdMain.View.AddTextBoxColumn("PLANTID", 60)
                .SetIsHidden();
            grdMain.View.AddComboBoxColumn("FACTORYID", 60, new SqlQuery("SelectOwnerFactory", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"P_PLANTID={UserInfo.Current.Plant}"), "FACTORYNAME", "FACTORYID")
                .SetEmptyItem("", "")
                .SetTextAlignment(TextAlignment.Center);
            grdMain.View.AddComboBoxColumn("JOBTYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=JobType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetEmptyItem("", "")
                .SetTextAlignment(TextAlignment.Center);

            grdMain.View.AddComboBoxColumn("USERLAYER", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=UserLayer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();

            grdMain.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdMain.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdMain.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdMain.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdMain.View.AddTextBoxColumn("ISINPUT", 50).SetIsHidden();

            grdMain.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            .SetIsHidden();
            grdMain.View.AddTextBoxColumn("NEWREQUEST", 200).SetIsReadOnly()
            .SetIsHidden();
            grdMain.View.AddTextBoxColumn("ENGINEERINGCHANGE", 200).SetIsReadOnly()
            .SetIsHidden();
            grdMain.View.AddDateEditColumn("IMPLEMENTATIONDATE", 200)
            .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime)
            .SetTextAlignment(TextAlignment.Center).SetIsReadOnly()
            .SetIsHidden();

            grdMain.View.AddComboBoxColumn("MAKEBUYTYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=MakeBuyType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();
            grdMain.View.AddComboBoxColumn("LOTCONTROL", new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();
            grdMain.View.AddComboBoxColumn("AGING", new SqlQuery("GetCodeList", "00001", "CODECLASSID=Aging", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();
            grdMain.View.AddSpinEditColumn("AGINGDAY", 150).SetIsHidden();
            grdMain.View.AddComboBoxColumn("CYCLECOUNT", new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetEmptyItem("","",true).SetIsHidden();
            grdMain.View.AddComboBoxColumn("ENDTYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetEmptyItem("", "", true).SetIsHidden();
            grdMain.View.AddTextBoxColumn("NM_CREATOR", 200).SetIsHidden();
            grdMain.View.AddTextBoxColumn("NM_SPECIFICATIONMAN", 200).SetIsHidden();
            grdMain.View.AddTextBoxColumn("ISINPUT", 50).SetIsHidden();

            grdMain.View.PopulateColumns();

            RepositoryItemTextEdit repository = grdMain.View.Columns["ITEMID"].ColumnEdit as RepositoryItemTextEdit;
            repository.Mask.MaskType = MaskType.RegEx;
            repository.Mask.EditMask = @"([-\\^A-Z0-9])+";
        }

        #endregion

        #region 이벤트
        private void InitializeEvent()
        {
			grdMain.View.CellValueChanged += View_CellValueChanged;
            grdMain.View.CellValueChanging += View_CellValueChanging;
            grdMain.View.AddingNewRow += grdIMList_AddingNewRow;
            grdMain.View.SelectionChanged += grdIMList_SelectionChanged;
            grdMain.View.ShowingEditor += grdIMListView_ShowingEditor;
            grdMain.InitContextMenuEvent += GrdChangePointList_InitContextMenuEvent;
        }


        /// <summary>
        /// Customizing Context Menu Item 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void GrdChangePointList_InitContextMenuEvent(SmartBandedGrid sender, Framework.SmartControls.Grid.InitContextMenuEventArgs args)
        {
            for (int i = 0; i < menuList.Count; i++)
            {
                args.AddMenus.Add(menuList[i]);
            }
        }

        /// <summary>
        /// 퀵 메뉴 리스트 등록
        /// </summary>
        private void InitializeQuickMenuList()
        {
            menuList.Add(new DXMenuItem(Language.Get("MENU_PG-SD-0292"), OpenForm) { BeginGroup = true, Tag = "PG-SD-0292" });
            menuList.Add(new DXMenuItem(Language.Get("MENU_PG-SD-0350"), OpenForm) { Tag = "PG-SD-0350" });
            menuList.Add(new DXMenuItem(Language.Get("MENU_PG-SD-0295"), OpenForm) { Tag = "PG-SD-0295" });
            menuList.Add(new DXMenuItem(Language.Get("MENU_PG-SD-0351"), OpenForm) { Tag = "PG-SD-0351" });
            menuList.Add(new DXMenuItem(Language.Get("MENU_PG-SD-0358"), OpenForm) { Tag = "PG-SD-0358" });
            menuList.Add(new DXMenuItem(Language.Get("MENU_PG-SD-0360"), OpenForm) { Tag = "PG-SD-0360" });
            menuList.Add(new DXMenuItem(Language.Get("MENU_PG-SD-0352"), OpenForm) { Tag = "PG-SD-0352" });
            menuList.Add(new DXMenuItem(Language.Get("MENU_PG-SD-0365"), OpenForm) { Tag = "PG-SD-0365" });
        }

        /// <summary>
        /// Customizing Context Menu Item Click 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OpenForm(object sender, EventArgs args)
        {
            try
            {
                DialogManager.ShowWaitDialog();

                DataRow currentRow = grdMain.View.GetFocusedDataRow();
                if (currentRow == null) return;

                string menuId = (sender as DXMenuItem).Tag.ToString();

                var param = currentRow.Table.Columns
                    .Cast<DataColumn>()
                    .ToDictionary(col => col.ColumnName, col => currentRow[col.ColumnName]);

                Dictionary<string, object> value = new Dictionary<string, object>();
                value.Add("MENUID", menuId);
                DataTable dt = SqlExecuter.Query("GetProgramIdbyMenuId", "10001", value);
                string[] words = Format.GetString(dt.Rows[0]["PROGRAMID"]).Split('.');
                param.Add("CALLMENU", words[3]);
                OpenMenu(menuId, param); 
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
            finally
            {
                DialogManager.Close();
            }
        }

        /// <summary>
        /// 셀 변경 이벤트 - 층수 필수값 설정 & 해제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			DataRow selectRow = grdMain.View.GetFocusedDataRow();
			if(selectRow == null)
            {
                return;
            }

            switch (Format.GetString(selectRow["MASTERDATACLASSID"]))
			{
				case "Product":
                    selectRow["CONSUMABLETYPE"] = "FG";
                    break;
                case "Commodity":
					selectRow["CONSUMABLETYPE"] = "FG";
					break;
				default:
					break;
			}
		}

		private void grdIMListView_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (grdMain.View.GetFocusedDataRow().RowState != DataRowState.Added && (grdMain.View.FocusedColumn.FieldName.Equals("ITEMID") || grdMain.View.FocusedColumn.FieldName.Equals("ITEMVERSION")))
            {
                e.Cancel = true;
            }
        }

        private void grdIMList_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {

            DataRow row = this.grdMain.View.GetFocusedDataRow();
            if (row != null)
            {
                switch (grdMain.View.FocusedColumn.FieldName)
                {
                    case "CONSUMABLETYPE":
                        switch (row["MASTERDATACLASSID"].ToString())
                        {
                            case "Product":
                            case "Commodity":
                                grdMain.View.SetIsReadOnly(true);
                                break;
                            default:
                                grdMain.View.SetIsReadOnly(false);
                                break;
                        }
                        break;
                    default:
                        grdMain.View.SetIsReadOnly(false);
                        break;
                }
            }
        }

        private void BtnItemSpec_Click(object sender, EventArgs e)
        {
            DataRow row = this.grdMain.View.GetFocusedDataRow();
            ProductItemSpecPopup pis = new ProductItemSpecPopup(row["ITEMID"].ToString(),  row["ITEMVERSION"].ToString(), row["MASTERDATACLASSID"].ToString(), row["ENTERPRISEID"].ToString(), row["IMPLEMENTATIONDATE"].ToString());
            pis.ShowDialog();
        }

        private void BtnFileUpload_Click(object sender, EventArgs e)
        {
            DataRow row = this.grdMain.View.GetFocusedDataRow();
            ItemMasterfilePopup pis = new ItemMasterfilePopup(row["ITEMID"].ToString(), row["ITEMVERSION"].ToString(), "ItemMaster", "ItemMasterMgnt / ItemMaster");
            pis.ShowDialog();
        }

        private void InitializeGrid_CustCgPopup()
        {
            var parentPopupColumn = this.grdMain.View.AddSelectPopupColumn("CUSTOMERID", 60, new SqlQuery("GetCustomerList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
                .SetPopupLayout("CUSTOMERID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)

                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                 {
                      foreach (DataRow row in selectedRows)
                      {
                          dataGridRow["CUSTOMERNAME"] = row["CUSTOMERNAME"].ToString();
                      }
                 });

            // 팝업에서 사용할 조회조건 항목 추가
            parentPopupColumn.Conditions.AddTextBox("TXTCUSTOMERID");
            // 팝업 그리드 설정
            parentPopupColumn.GridColumns.AddTextBoxColumn("CUSTOMERID", 80);
            parentPopupColumn.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 150);
            parentPopupColumn.GridColumns.AddTextBoxColumn("ADDRESS", 250);
            parentPopupColumn.GridColumns.AddTextBoxColumn("CEONAME", 100);
            parentPopupColumn.GridColumns.AddTextBoxColumn("TELNO", 100);
            parentPopupColumn.GridColumns.AddTextBoxColumn("FAXNO", 100);
        }

        private void InitializeGrid_SaleCgPopup()
        {
            var parentPopupColumn = this.grdMain.View.AddSelectPopupColumn("SALEORDERCATEGORY", 200, new SqlQuery("GetCategoryPopup", "10001", $"TOPPARENTCATEGORYID={"Sales"}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("SALEORDERCATEGORY", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupResultMapping("SALEORDERCATEGORY", "CATEGORYID")
                .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow);
                //.SetValidationIsRequired();

            // 팝업에서 사용할 조회조건 항목 추가
            parentPopupColumn.Conditions.AddComboBox("PARENTCATEGORYID", new SqlQuery("GetCategoryMidList", "10001", $"TOPPARENTCATEGORYID={"Sales"}"), "CATEGORYNAME", "CATEGORYID").SetValidationIsRequired();
            parentPopupColumn.Conditions.AddTextBox("CATEGORYNAME");
            // 팝업 그리드 설정
            parentPopupColumn.GridColumns.AddTextBoxColumn("CATEGORYID", 150);
            parentPopupColumn.GridColumns.AddTextBoxColumn("CATEGORYNAME", 200);

        }

        private void InitializeGrid_CostCgPopup()
        {
            var parentPopupColumn = this.grdMain.View.AddSelectPopupColumn("COSTCATEGORY", new SqlQuery("GetCategoryPopup", "10001", $"TOPPARENTCATEGORYID={"Cost"}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("COSTCATEGORY", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupResultMapping("COSTCATEGORY", "CATEGORYID")
                .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow);

			parentPopupColumn.Conditions.AddComboBox("PARENTCATEGORYID", new SqlQuery("GetCategoryMidList", "10001", $"TOPPARENTCATEGORYID={"Cost"}"), "CATEGORYNAME", "CATEGORYID").SetValidationIsRequired();
            parentPopupColumn.Conditions.AddTextBox("CATEGORYNAME");
            // 팝업 그리드 설정
            parentPopupColumn.GridColumns.AddTextBoxColumn("CATEGORYID", 150);
            parentPopupColumn.GridColumns.AddTextBoxColumn("CATEGORYNAME", 200);
        }

        private void InitializeGrid_InventoryCgPopup()
        {
            var parentPopupColumn = this.grdMain.View.AddSelectPopupColumn("INVENTORYCATEGORY", new SqlQuery("GetCategoryPopup", "10001", $"TOPPARENTCATEGORYID={"Inventory"}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("INVENTORYCATEGORY", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupResultMapping("INVENTORYCATEGORY", "CATEGORYID")
                .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow);

			parentPopupColumn.Conditions.AddComboBox("PARENTCATEGORYID", new SqlQuery("GetCategoryMidList", "10001", $"TOPPARENTCATEGORYID={"Inventory"}"), "CATEGORYNAME", "CATEGORYID").SetValidationIsRequired();
            parentPopupColumn.Conditions.AddTextBox("CATEGORYNAME");
            
            // 팝업 그리드 설정
            parentPopupColumn.GridColumns.AddTextBoxColumn("CATEGORYID", 150);
            parentPopupColumn.GridColumns.AddTextBoxColumn("CATEGORYNAME", 200);
        }

        private void View_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            object obj = grdMain.DataSource;
            DataTable dt = (DataTable)obj;
            DataRow row = this.grdMain.View.GetFocusedDataRow();

            if (row["IMPLEMENTATIONDATE"].ToString() != "")
            {
                grdMain.View.SetIsReadOnly(true);
            }


            switch (grdMain.View.FocusedColumn.FieldName)
            {
                case "S":
                    bool chk = bool.Parse(e.Value.ToString());
                    row["S"] = e.Value;

                    if (dt.Select("S = True").Length != 0 && chk == false)
                    {
                        grdMain.View.CancelFixColumn();
                    }
                    break;

                //2019-12-11 : 자동채번으로 해당 로직은 주석처리

                //case "ITEMID":
                //    if (row["IDCLASSIDRULE"].ToString() == "Y")
                //    {
                //        grdIMList.View.CancelFixColumn();
                //    }
                //    break;
                //case "ITEMNAME":

                //    if (row["IDCLASSIDRULE"].ToString() == "Y")
                //    {
                //        grdIMList.View.CancelFixColumn();
                //    }
                //    break;

                case "PRODUCTTYPE":
                    if (row.RowState != DataRowState.Added)
                    {
                        //grdIMList.View.CancelFixColumn();
                    }
                    break;

            }
        }

        private void grdIMList_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            args.NewRow["PRODUCTRATING"] = "B";
            args.NewRow["LOTCONTROL"] = "Y";
            args.NewRow["ITEMSTATUS"] = "Active";
            args.NewRow["PRODUCTIONTYPE"] = "Sample";

            object obj = grdMain.DataSource;
            DataTable dt = (DataTable)obj;
            
            if (globalrow == null)
            {
       
                args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                args.NewRow["PLANTID"] = UserInfo.Current.Plant;
                args.NewRow["VALIDSTATE"] = "Valid";

            }
            else
            {
                if (globalrow.Length == 0)
                {
                    args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                    args.NewRow["PLANTID"] = UserInfo.Current.Plant;
                    args.NewRow["VALIDSTATE"] = "Valid";
                }
                else
                {
                    foreach (DataRow rowSelect in globalrow)
                    {
                        foreach (DataColumn col in dt.Columns)
                        {
                            switch (col.ColumnName)
                            {
                                case "S":
                                case "CREATEDTIME":
                                    break;
                                default:
                                    args.NewRow[col.ColumnName] = rowSelect[col.ColumnName];
                                    break;
                            }
                        }
                    }
                }
            }

        }

        private void View_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            object obj = grdMain.DataSource;
            DataTable dt = (DataTable)obj;

            switch (grdMain.View.FocusedColumn.FieldName)
            {
                case "S":
                    bool chk = bool.Parse(grdMain.View.GetFocusedRowCellValue("S").ToString());

                    if (dt.Select("S = True").Length != 0 && chk == false)
                    {
                        grdMain.View.CancelFixColumn();
                    }
                    break;
            }
        }



        #endregion

        #region 조회조건 영역
        /// <summary>
        /// 검색조건 초기화. 
        /// 조회조건 정보, 메뉴 - 조회조건 매핑 화면에 등록된 정보를 기준으로 구성됩니다.
        /// DB에 등록한 정보를 제외한 추가 조회조건 구성이 필요한 경우 사용합니다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            InitializeCondition_Popup();
            InitializeCondition_Popup2();

            DataTable dtserial = new DataTable();
            dtserial.Columns.Add("CODE");
            dtserial.Columns.Add("NAME");

            DataRow serial1 = dtserial.NewRow();
            serial1["CODE"] = "F";
            serial1["NAME"] = "IFC";
            dtserial.Rows.Add(serial1);

            DataRow serial2 = dtserial.NewRow();
            serial2["CODE"] = "C";
            serial2["NAME"] = "CCT";
            dtserial.Rows.Add(serial2);

            DataRow serial3 = dtserial.NewRow();
            serial3["CODE"] = "Y";
            serial3["NAME"] = "YPE";
            dtserial.Rows.Add(serial3);

            DataRow serial4 = dtserial.NewRow();
            serial4["CODE"] = "P";
            serial4["NAME"] = "YPEV";
            dtserial.Rows.Add(serial4);


            DataTable dtProdClass = new DataTable();
            dtProdClass.Columns.Add("CODE");
            dtProdClass.Columns.Add("NAME");

            DataRow ProdClass1 = dtProdClass.NewRow();
            ProdClass1["CODE"] = "S";
            ProdClass1["NAME"] = "SINGLE";
            dtProdClass.Rows.Add(ProdClass1);

            DataRow ProdClass2 = dtProdClass.NewRow();
            ProdClass2["CODE"] = "D";
            ProdClass2["NAME"] = "DOUBLE";
            dtProdClass.Rows.Add(ProdClass2);

            DataRow ProdClass3 = dtProdClass.NewRow();
            ProdClass3["CODE"] = "M";
            ProdClass3["NAME"] = "MULTI";
            dtProdClass.Rows.Add(ProdClass3);

        }

        /// <summary>
        /// 검색조건 팝업 
        /// </summary>
        private void InitializeCondition_Popup()
        {
            //팝업 컬럼 설정
            var parentAreaPopupColumn = Conditions.AddSelectPopup("CUSTOMER", new SqlQuery("GetCustomerList", "10001", new string[] { $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}" }), "CUSTOMERNAME", "CUSTOMERID")
               .SetPopupLayout("CUSTOMER", PopupButtonStyles.Ok_Cancel, true, false)  //팝업창 UI 설정.
               .SetPopupResultCount(1);  //팝업창 선택가능한 개수

            parentAreaPopupColumn.Conditions.AddTextBox("TXTCUSTOMERID");

            //팝업 그리드
            parentAreaPopupColumn.GridColumns.AddTextBoxColumn("CUSTOMERID", 80);
            parentAreaPopupColumn.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 150);
            parentAreaPopupColumn.SetPosition(1);

        }
        #endregion

        #region 툴바

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Copy"))
            {

                if (grdMain.View.FocusedRowHandle < 0)
                    return;

                DataRow row = grdMain.View.GetFocusedDataRow();

                //if (row["MASTERDATACLASSID"].ToString() == "SubAssembly")
                //{
                //    return;
                //}

                if (row["ISINPUT"].Equals("Y"))
                {
                    throw MessageException.Create("IsInputValidateCheck");//미투입된 품목만 복사할 수 있습니다.
                }

                RoutingItemCopyPopup copy = new RoutingItemCopyPopup(row);
                copy.ShowDialog();

            }
            else if (btn.Name.ToString().Equals("ProductSpec"))
            {

                // 등록 팝업
                DataRow row = this.grdMain.View.GetFocusedDataRow();
                MaterialItemSpecPopup pis = new MaterialItemSpecPopup(row["ITEMID"].ToString(), row["ITEMVERSION"].ToString(), row["MASTERDATACLASSID"].ToString(), row["ENTERPRISEID"].ToString());
                pis.ShowDialog();

            }
            else if (btn.Name.ToString().Equals("AttachFile"))
            {
                DataRow row = this.grdMain.View.GetFocusedDataRow();
                ItemMasterfilePopup pis = new ItemMasterfilePopup(row["ITEMID"].ToString(), row["ITEMVERSION"].ToString(), "ItemMaster", "ItemMasterMgnt/ItemMaster");
                pis.ShowDialog();

            }
            else if (btn.Name.Equals("SubProductItemCode"))
            {
				//반재품 채번
				DataRow row = this.grdMain.View.GetFocusedDataRow();
				if (!row["MASTERDATACLASSID"].Equals("Product"))
					throw MessageException.Create("CheckProductType");

                Dictionary<string, object> param = new Dictionary<string, object>();
                DataTable dt = new DataTable();



                if (UserInfo.Current.Enterprise == "YOUNGPOONG")
                {
                    param.Add("PRODUCTCODEID", row["ITEMID"].ToString());
                    param.Add("PRODUCTCODEVERSION", row["ITEMVERSION"].ToString());
                    param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);

                    dt = SqlExecuter.Query("CompareSemiProductByProduct", "10002", param);
                }

                else
                {
                    param.Add("PRODUCTCODEID", row["ITEMID"].ToString().Substring(0, 7));
                    //param.Add("PRODUCTCODEVERSION", row["ITEMVERSION"].ToString().Substring(0, 7));
                    param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);

                    dt = SqlExecuter.Query("CompareSemiProductByProduct", "10001", param);
                }


		
                




				if (dt.Rows.Count > 0)
					throw MessageException.Create("NoCreatedSubAssembly");

				SubProductItemCodePopup semiProductItemCode = new SubProductItemCodePopup(row);
				semiProductItemCode.AddSemiProductEventHandler += semiProductItemCode_AddSemiProductEventHandler;
				//subProductItemCode.Initialize(row["ITEMID"].ToString(), row["ITEMVERSION"].ToString(), row["ITEMNAME"].ToString());
				semiProductItemCode.ShowDialog();
                
            }

        }

        private void semiProductItemCode_AddSemiProductEventHandler(object sender, AddSemiProductEventArgs e)
        {
            try
            {
                var codeList = e.SemiProductCodeDictionary;
                foreach (string key in codeList.Keys)
                {
                    foreach (string itemCode in codeList[key])
                    {
                        grdMain.View.AddingNewRow -= grdIMList_AddingNewRow;
                        
                        grdMain.View.AddNewRow();

                        grdMain.View.ClearColumnsFilter();
                        grdMain.View.FocusedRowHandle = grdMain.View.RowCount - 1;

                        grdMain.View.SetFocusedRowCellValue("LOTCONTROL", "Y");
                        grdMain.View.SetFocusedRowCellValue("ITEMSTATUS", "Active");
                        grdMain.View.SetFocusedRowCellValue("PRODUCTIONTYPE","Production");

                        grdMain.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise);
                        grdMain.View.SetFocusedRowCellValue("PLANTID", UserInfo.Current.Plant);
                        grdMain.View.SetFocusedRowCellValue("VALIDSTATE", "Valid");

                        grdMain.View.SetFocusedRowCellValue("ITEMID", itemCode);
                        grdMain.View.SetFocusedRowCellValue("CONSUMABLETYPE", key);
                        grdMain.View.SetFocusedRowCellValue("MASTERDATACLASSID", "SubAssembly");
                        grdMain.View.SetFocusedRowCellValue("RTRSHT", "SHT");
                        //기존항목복사
                        grdMain.View.SetFocusedRowCellValue("ITEMNAME", e.ProductDataRow["ITEMNAME"]);
                        grdMain.View.SetFocusedRowCellValue("ITEMVERSION", e.ProductDataRow["ITEMVERSION"]);
                        grdMain.View.SetFocusedRowCellValue("PRODUCTRATING", e.ProductDataRow["PRODUCTRATING"]);
                        grdMain.View.SetFocusedRowCellValue("CUSTOMERID", e.ProductDataRow["CUSTOMERID"]);
                        grdMain.View.SetFocusedRowCellValue("CUSTOMERNAME", e.ProductDataRow["CUSTOMERNAME"]);
                        grdMain.View.SetFocusedRowCellValue("JOBTYPE", e.ProductDataRow["JOBTYPE"]);
                        grdMain.View.SetFocusedRowCellValue("PRODUCTIONTYPE", e.ProductDataRow["PRODUCTIONTYPE"]);
                        grdMain.View.SetFocusedRowCellValue("FACTORYID", e.ProductDataRow["FACTORYID"]);

                        switch (UserInfo.Current.Enterprise)
                        {
                            case "YOUNGPOONG":

                                break;
                            case "INTERFLEX":

                                break;
                        }
                    }
                }
                grdMain.View.AddingNewRow += grdIMList_AddingNewRow;
            }
            catch (Exception ex)
            {
                MessageException.Create("CheckProductdefCode");//잘못된 제품 코드입니다.
            }
        }

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {

            base.OnToolbarSaveClick();

            DataTable changed = grdMain.GetChangedRows();

            foreach (DataRow dr in changed.Rows)
            {
                if (Format.GetString(dr["ITEMID"]).Contains("-") && Format.GetString(dr["ITEMVERSION"]).Equals(""))
                {
                    throw MessageException.Create("RequiredRev");
                }

                if (Format.GetString(dr["MASTERDATACLASSID"]).Equals("Product") && Format.GetString(dr["LAYER"]).Equals(""))
                {
                    throw MessageException.Create("LayerIsEssential");
                }

                if (Format.GetString(dr["MASTERDATACLASSID"]).Equals("Product") && Format.GetString(dr["PRODUCTTYPE"]).Equals(""))
                {
                    throw MessageException.Create("ProductTypeIsEssential");
                }
            }

            ExecuteRule("ItemMasterWithIDCreate", changed);
        }

        #region back - 2019-12-10 
        //protected override void OnToolbarSaveClick()
        //{

        //    base.OnToolbarSaveClick();



        //    DataTable changed = new DataTable();
        //    DataTable dtItemserialI = new DataTable();

        //    // 제품 그룹 정보
        //    DataTable dtProductclass = new DataTable();
        //    dtProductclass.Columns.Add("PRODUCTCLASSID");
        //    dtProductclass.Columns.Add("PRODUCTCLASSTYPE");
        //    dtProductclass.Columns.Add("_STATE_");

        //    // 사양 스펙
        //    DataTable dtproductitemspec = new DataTable();

        //    dtproductitemspec.Columns.Add("ENTERPRISEID");
        //    dtproductitemspec.Columns.Add("ITEMID");
        //    dtproductitemspec.Columns.Add("ITEMVERSION");
        //    dtproductitemspec.Columns.Add("PRODUCTTYPE");
        //    dtproductitemspec.Columns.Add("VALIDSTATE");

        //    dtproductitemspec.Columns.Add("CUSTOMERID");
        //    dtproductitemspec.Columns.Add("PRODUCTRATING");
        //    dtproductitemspec.Columns.Add("JOBTYPE");
        //    dtproductitemspec.Columns.Add("PRODUCTIONTYPE");
        //    dtproductitemspec.Columns.Add("RTRSHT");
        //    dtproductitemspec.Columns.Add("FACTORYID");
        //    dtproductitemspec.Columns.Add("CUSTOMERNAME");
        //    dtproductitemspec.Columns.Add("LAYER");

        //    dtproductitemspec.Columns.Add("_STATE_");


        //    // 제품 정보
        //    DataTable dtproductdefinition = new DataTable();
        //    dtproductdefinition.Columns.Add("PRODUCTDEFID");
        //    dtproductdefinition.Columns.Add("PRODUCTDEFVERSION");
        //    dtproductdefinition.Columns.Add("PRODUCTCLASSID");
        //    dtproductdefinition.Columns.Add("PRODUCTDEFNAME");
        //    dtproductdefinition.Columns.Add("ENTERPRISEID");
        //    dtproductdefinition.Columns.Add("PRODUCTDEFTYPE");
        //    dtproductdefinition.Columns.Add("PRODUCTIONTYPE");
        //    dtproductdefinition.Columns.Add("UNIT");
        //    dtproductdefinition.Columns.Add("LEADTIME");
        //    dtproductdefinition.Columns.Add("DESCRIPTION");
        //    dtproductdefinition.Columns.Add("_STATE_");
        //    dtproductdefinition.Columns.Add("VALIDSTATE");
        //    dtproductdefinition.Columns.Add("MATERIALCLASS");
        //    dtproductdefinition.Columns.Add("PLANTID");
        //    dtproductdefinition.Columns.Add("PRODUCTSHAPE");

        //    dtproductdefinition.Columns.Add("PROCESSDEFID");
        //    dtproductdefinition.Columns.Add("PROCESSDEFVERSION");

        //    dtproductdefinition.Columns.Add("CUSTOMERID");

        //    dtproductdefinition.Columns.Add("MATERIALSEQUENCE");
        //    dtproductdefinition.Columns.Add("RTRSHT");
        //    dtproductdefinition.Columns.Add("LAYER");



        //    // 반제품 정보
        //    DataTable dtconsumabledefinition = new DataTable();
        //    dtconsumabledefinition.Columns.Add("CONSUMABLEDEFID");
        //    dtconsumabledefinition.Columns.Add("CONSUMABLEDEFVERSION");
        //    dtconsumabledefinition.Columns.Add("CONSUMABLECLASSID");
        //    dtconsumabledefinition.Columns.Add("CONSUMABLEDEFNAME");
        //    dtconsumabledefinition.Columns.Add("ENTERPRISEID");
        //    dtconsumabledefinition.Columns.Add("CONSUMABLETYPE");
        //    dtconsumabledefinition.Columns.Add("UNIT");
        //    dtconsumabledefinition.Columns.Add("DESCRIPTION");
        //    dtconsumabledefinition.Columns.Add("_STATE_");
        //    dtconsumabledefinition.Columns.Add("VALIDSTATE");
        //    dtconsumabledefinition.Columns.Add("ISLOTMNG");





        //    switch (tabIdManagement.SelectedTabPageIndex)
        //    {
        //        case 0:// IML

        //            changed = grdIMList.GetChangedRows();
        //            foreach (DataRow row in changed.Rows)
        //            {
        //                // 추가 
        //                if (row["_STATE_"].ToString() == "added")
        //                {

        //                    if (UserInfo.Current.Enterprise == "INTERFLEX")
        //                    {
        //                        string sitemid = "";

        //                        if (row["MASTERDATACLASSID"].ToString() == "Product")
        //                        {
        //                            sitemid = "1";
        //                        }
        //                        if (row["MASTERDATACLASSID"].ToString() == "SubAssembly")
        //                        {
        //                            sitemid = "2";
        //                        }
        //                        if (row["MASTERDATACLASSID"].ToString() == "Commodity")
        //                        {
        //                            sitemid = "9";
        //                        }


        //                        string sserial = "";

        //                        Dictionary<string, object> paramISS = new Dictionary<string, object>();
        //                        paramISS.Add("CODECLASSID", "ItemSserialSite");
        //                        paramISS.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
        //                        DataTable dtserial = SqlExecuter.Query("GetCodeList", "00001", paramISS);

        //                        //DataTable dt = SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
        //                        DataRow[] rowserial = dtserial.Select("CODENAME = '" + UserInfo.Current.Plant + "'");
        //                        sserial = rowserial[0]["CODEID"].ToString();
        //                        sitemid = sitemid + sserial;

        //                        string sProductType = "";
        //                        sProductType = row["PRODUCTTYPE"].ToString();

        //                        if (row["MASTERDATACLASSID"].ToString() == "Product")
        //                        {
        //                            if (sProductType == "")
        //                            {
        //                                ShowMessage("ProductType"); // 제품시 필수 항목
        //                                return;
        //                            }
        //                        }

        //                        if (sProductType != "")
        //                        {
        //                            sitemid = sitemid + sProductType;
        //                        }

        //                        // 채번 시리얼 존재 유무 체크
        //                        Dictionary<string, object> param = new Dictionary<string, object>();
        //                        param.Add("IDCLASSID", "InterflexProduct");
        //                        param.Add("PREFIX", sitemid);
        //                        DataTable dtItemserialChk = SqlExecuter.Query("GetProductitemserial", "10001", param);

        //                        dtItemserialI = dtItemserialChk.Clone();
        //                        dtItemserialI.Columns.Add("_STATE_");


        //                        if (dtItemserialChk != null)
        //                        {
        //                            if (dtItemserialChk.Rows.Count == 0)
        //                            {
        //                                DataRow rowItemserialI = dtItemserialI.NewRow();
        //                                rowItemserialI["IDCLASSID"] = "InterflexProduct";
        //                                rowItemserialI["PREFIX"] = sitemid;
        //                                rowItemserialI["LASTSERIALNO"] = "00001";
        //                                rowItemserialI["_STATE_"] = "added";
        //                                dtItemserialI.Rows.Add(rowItemserialI);


        //                            }
        //                            else
        //                            {
        //                                DataRow rowItemserialI = dtItemserialI.NewRow();
        //                                rowItemserialI["IDCLASSID"] = "InterflexProduct";
        //                                rowItemserialI["PREFIX"] = sitemid;

        //                                int ilastserialno = 0;
        //                                ilastserialno = Int32.Parse(dtItemserialChk.Rows[0]["LASTSERIALNO"].ToString());
        //                                ilastserialno = ilastserialno + 1;


        //                                rowItemserialI["LASTSERIALNO"] = ("0000" + ilastserialno.ToString()).Substring(("0000" + ilastserialno.ToString()).Length - 5, 5);
        //                                rowItemserialI["_STATE_"] = "modified";
        //                                dtItemserialI.Rows.Add(rowItemserialI);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            DataRow rowItemserialI = dtItemserialI.NewRow();
        //                            rowItemserialI["IDCLASSID"] = "InterflexProduct";
        //                            rowItemserialI["PREFIX"] = sitemid;
        //                            rowItemserialI["LASTSERIALNO"] = "00001";
        //                            rowItemserialI["_STATE_"] = "added";
        //                            dtItemserialI.Rows.Add(rowItemserialI);
        //                        }

        //                        // 자동채번일경우
        //                        if (row["IDCLASSIDRULE"].ToString() == "Y")
        //                        {
        //                            row["ITEMID"] = sitemid + dtItemserialI.Rows[0]["LASTSERIALNO"].ToString() + "A1";

        //                            row["ITEMVERSION"] = "A1";
        //                        }
        //                    }



        //                    // 제품 그룹 정보 유무 체크
        //                    Dictionary<string, object> paramPcl = new Dictionary<string, object>();
        //                    paramPcl.Add("PRODUCTCLASSID", row["PRODUCTTYPE"].ToString());
        //                    DataTable dtPclChk = SqlExecuter.Query("GetProductclassList", "10001", paramPcl);

        //                    if (dtPclChk != null)
        //                    {
        //                        if (dtPclChk.Rows.Count == 0)
        //                        {
        //                            // 제품 그룹 정보  등록
        //                            DataRow rowPclNew = dtProductclass.NewRow();
        //                            rowPclNew["PRODUCTCLASSID"] = row["PRODUCTTYPE"].ToString();
        //                            rowPclNew["PRODUCTCLASSTYPE"] = row["MASTERDATACLASSID"].ToString();
        //                            dtProductclass.Rows.Add(rowPclNew);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        // 제품 그룹 정보  등록
        //                        DataRow rowPclNew = dtProductclass.NewRow();
        //                        rowPclNew["PRODUCTCLASSID"] = row["PRODUCTTYPE"].ToString();
        //                        rowPclNew["PRODUCTCLASSTYPE"] = row["MASTERDATACLASSID"].ToString();
        //                        dtProductclass.Rows.Add(rowPclNew);
        //                    }

        //                    // 품목 스펙 등록
        //                    DataRow rowproductitemspec = dtproductitemspec.NewRow();


        //                    rowproductitemspec["ENTERPRISEID"] = row["ENTERPRISEID"];
        //                    rowproductitemspec["ITEMID"] = row["ITEMID"];
        //                    rowproductitemspec["ITEMVERSION"] = row["ITEMVERSION"];
        //                    rowproductitemspec["PRODUCTTYPE"] = row["PRODUCTTYPE"];
        //                    rowproductitemspec["VALIDSTATE"] = "Valid";

        //                    rowproductitemspec["CUSTOMERID"] = row["CUSTOMERID"];
        //                    rowproductitemspec["CUSTOMERNAME"] = row["CUSTOMERNAME"];
        //                    rowproductitemspec["PRODUCTRATING"] = row["PRODUCTRATING"];
        //                    rowproductitemspec["JOBTYPE"] = row["JOBTYPE"];
        //                    rowproductitemspec["PRODUCTIONTYPE"] = row["PRODUCTIONTYPE"];
        //                    rowproductitemspec["RTRSHT"] = row["RTRSHT"];
        //                    rowproductitemspec["FACTORYID"] = row["FACTORYID"];
        //                    rowproductitemspec["LAYER"] = row["LAYER"];

        //                    rowproductitemspec["_STATE_"] = "added";
        //                    dtproductitemspec.Rows.Add(rowproductitemspec);


        //                    if (row["MASTERDATACLASSID"].ToString() != "OperationItem")
        //                    {

        //                        //제품 정보 등록
        //                        DataRow rowproductdefinition = dtproductdefinition.NewRow();
        //                        rowproductdefinition["PRODUCTDEFID"] = row["ITEMID"];
        //                        rowproductdefinition["PRODUCTDEFVERSION"] = row["ITEMVERSION"];

        //                        rowproductdefinition["PROCESSDEFID"] = row["ITEMID"];
        //                        rowproductdefinition["PROCESSDEFVERSION"] = row["ITEMVERSION"];

        //                        rowproductdefinition["PRODUCTDEFNAME"] = row["ITEMNAME"];
        //                        rowproductdefinition["ENTERPRISEID"] = row["ENTERPRISEID"];
        //                        rowproductdefinition["PLANTID"] = UserInfo.Current.Plant;
        //                        rowproductdefinition["PRODUCTDEFTYPE"] = row["MASTERDATACLASSID"].ToString();
        //                        rowproductdefinition["RTRSHT"] = row["RTRSHT"].ToString();
        //                        rowproductdefinition["LAYER"] = row["LAYER"].ToString();

        //                        if (row["MASTERDATACLASSID"].ToString() == "SubAssembly")
        //                        {
        //                            rowproductdefinition["PRODUCTCLASSID"] = row["MASTERDATACLASSID"].ToString();
        //                            rowproductdefinition["MATERIALCLASS"] = row["CONSUMABLETYPE"];
        //                            rowproductdefinition["MATERIALSEQUENCE"] = row["ITEMID"].ToString().Substring(row["ITEMID"].ToString().Length - 4, 2);

        //                        }
        //                        else
        //                        {
        //                            //인터인경우
        //                            if (row["ENTERPRISEID"].ToString() == "INTERFLEX")
        //                            {
        //                                // 상품경우
        //                                if (row["MASTERDATACLASSID"].ToString() == "Commodity")
        //                                {
        //                                    rowproductdefinition["PRODUCTDEFTYPE"] = "";
        //                                    rowproductdefinition["PRODUCTCLASSID"] = row["MASTERDATACLASSID"].ToString();
        //                                }
        //                                else
        //                                {
        //                                    rowproductdefinition["PRODUCTDEFTYPE"] = row["MASTERDATACLASSID"].ToString();
        //                                    rowproductdefinition["PRODUCTCLASSID"] = row["MASTERDATACLASSID"].ToString();
        //                                }
        //                            }
        //                            else
        //                            {
        //                                //영품인경우
        //                                if (row["MASTERDATACLASSID"].ToString() == "Commodity")
        //                                {
        //                                    rowproductdefinition["PRODUCTDEFTYPE"] = "Product";
        //                                    rowproductdefinition["PRODUCTCLASSID"] = row["MASTERDATACLASSID"].ToString();
        //                                }
        //                                else
        //                                {
        //                                    rowproductdefinition["PRODUCTDEFTYPE"] = row["MASTERDATACLASSID"].ToString();
        //                                    rowproductdefinition["PRODUCTCLASSID"] = row["MASTERDATACLASSID"].ToString();
        //                                }


        //                            }

        //                        }

        //                        // 제품만
        //                        if (row["MASTERDATACLASSID"].ToString() == "Product")
        //                        {
        //                            rowproductdefinition["PRODUCTSHAPE"] = row["PRODUCTTYPE"];

        //                            rowproductdefinition["MATERIALCLASS"] = "FG";
        //                            rowproductdefinition["MATERIALSEQUENCE"] = "00";

        //                        }
        //                        else
        //                        {
        //                            rowproductdefinition["PRODUCTSHAPE"] = "";
        //                        }

        //                        //rowproductdefinition["PRODUCTDEFTYPE"] = row["MASTERDATACLASSID"];
        //                        rowproductdefinition["PRODUCTIONTYPE"] = row["PRODUCTIONTYPE"];
        //                        //rowproductdefinition["PRODUCTIONTYPE"] = "Production";

        //                        rowproductdefinition["UNIT"] = row["ITEMUOM"];
        //                        rowproductdefinition["LEADTIME"] = row["LEADTIME"];
        //                        rowproductdefinition["DESCRIPTION"] = row["DESCRIPTION"];
        //                        rowproductdefinition["VALIDSTATE"] = "Valid";
        //                        rowproductdefinition["_STATE_"] = "added";


        //                        dtproductdefinition.Rows.Add(rowproductdefinition);
        //                    }
        //                    //반제품

        //                    if (row["MASTERDATACLASSID"].ToString() == "SubAssembly")
        //                    {
        //                        //반제품 정보 등록
        //                        DataRow rowconsumabledefinition = dtconsumabledefinition.NewRow();
        //                        rowconsumabledefinition["CONSUMABLEDEFID"] = row["ITEMID"];
        //                        rowconsumabledefinition["CONSUMABLEDEFVERSION"] = row["ITEMVERSION"];
        //                        rowconsumabledefinition["CONSUMABLECLASSID"] = row["MASTERDATACLASSID"];
        //                        rowconsumabledefinition["CONSUMABLEDEFNAME"] = row["ITEMNAME"];
        //                        rowconsumabledefinition["ENTERPRISEID"] = row["ENTERPRISEID"];
        //                        rowconsumabledefinition["CONSUMABLETYPE"] = row["CONSUMABLETYPE"];
        //                        rowconsumabledefinition["UNIT"] = row["ITEMUOM"];
        //                        rowconsumabledefinition["DESCRIPTION"] = row["DESCRIPTION"];
        //                        rowconsumabledefinition["_STATE_"] = "added";
        //                        rowconsumabledefinition["VALIDSTATE"] = "Valid";

        //                        rowconsumabledefinition["ISLOTMNG"] = row["LOTCONTROL"];

        //                        dtconsumabledefinition.Rows.Add(rowconsumabledefinition);

        //                    }

        //                }
        //                // 수정
        //                if (row["_STATE_"].ToString() == "modified")
        //                {

        //                    // 품목 스펙 등록
        //                    DataRow rowproductitemspec = dtproductitemspec.NewRow();


        //                    rowproductitemspec["ENTERPRISEID"] = row["ENTERPRISEID"];
        //                    rowproductitemspec["ITEMID"] = row["ITEMID"];
        //                    rowproductitemspec["ITEMVERSION"] = row["ITEMVERSION"];
        //                    rowproductitemspec["PRODUCTTYPE"] = row["PRODUCTTYPE"];
        //                    rowproductitemspec["VALIDSTATE"] = "Valid";

        //                    rowproductitemspec["CUSTOMERID"] = row["CUSTOMERID"];
        //                    rowproductitemspec["PRODUCTRATING"] = row["PRODUCTRATING"];
        //                    rowproductitemspec["JOBTYPE"] = row["JOBTYPE"];
        //                    rowproductitemspec["PRODUCTIONTYPE"] = row["PRODUCTIONTYPE"];
        //                    rowproductitemspec["RTRSHT"] = row["RTRSHT"];
        //                    rowproductitemspec["FACTORYID"] = row["FACTORYID"];

        //                    rowproductitemspec["CUSTOMERNAME"] = row["CUSTOMERNAME"];
        //                    rowproductitemspec["LAYER"] = row["LAYER"];

        //                    rowproductitemspec["_STATE_"] = "modified";
        //                    dtproductitemspec.Rows.Add(rowproductitemspec);

        //                    //제품 정보 등록
        //                    if (row["MASTERDATACLASSID"].ToString() != "OperationItem")
        //                    {
        //                        DataRow rowproductdefinition = dtproductdefinition.NewRow();
        //                        rowproductdefinition["PRODUCTDEFID"] = row["ITEMID"];
        //                        rowproductdefinition["PRODUCTDEFVERSION"] = row["ITEMVERSION"];
        //                        rowproductdefinition["PRODUCTCLASSID"] = row["MASTERDATACLASSID"];

        //                        if (row["MASTERDATACLASSID"].ToString() == "Product")
        //                        {
        //                            rowproductdefinition["PRODUCTSHAPE"] = row["PRODUCTTYPE"];
        //                        }

        //                        rowproductdefinition["PRODUCTDEFNAME"] = row["ITEMNAME"];
        //                        rowproductdefinition["ENTERPRISEID"] = row["ENTERPRISEID"];
        //                        rowproductdefinition["PRODUCTDEFTYPE"] = row["MASTERDATACLASSID"];
        //                        rowproductdefinition["PRODUCTIONTYPE"] = row["PRODUCTIONTYPE"];
        //                        rowproductdefinition["UNIT"] = row["ITEMUOM"];
        //                        rowproductdefinition["LEADTIME"] = row["LEADTIME"];
        //                        rowproductdefinition["DESCRIPTION"] = row["DESCRIPTION"];
        //                        rowproductdefinition["VALIDSTATE"] = row["VALIDSTATE"];
        //                        rowproductdefinition["_STATE_"] = "modified";
        //                        rowproductdefinition["MATERIALCLASS"] = row["CONSUMABLETYPE"];
        //                        rowproductdefinition["RTRSHT"] = row["RTRSHT"];
        //                        rowproductdefinition["CUSTOMERID"] = row["CUSTOMERID"];
        //                        rowproductdefinition["LAYER"] = row["LAYER"];

        //                        dtproductdefinition.Rows.Add(rowproductdefinition);

        //                        if (row["MASTERDATACLASSID"].ToString() == "SubAssembly")
        //                        {
        //                            //반제품 정보 수정

        //                            DataRow rowconsumabledefinition = dtconsumabledefinition.NewRow();
        //                            rowconsumabledefinition["CONSUMABLEDEFID"] = row["ITEMID"];
        //                            rowconsumabledefinition["CONSUMABLEDEFVERSION"] = row["ITEMVERSION"];
        //                            rowconsumabledefinition["CONSUMABLECLASSID"] = row["MASTERDATACLASSID"];
        //                            rowconsumabledefinition["CONSUMABLEDEFNAME"] = row["ITEMNAME"];
        //                            rowconsumabledefinition["ENTERPRISEID"] = row["ENTERPRISEID"];
        //                            rowconsumabledefinition["CONSUMABLETYPE"] = row["CONSUMABLETYPE"];
        //                            rowconsumabledefinition["UNIT"] = row["ITEMUOM"];
        //                            rowconsumabledefinition["DESCRIPTION"] = row["DESCRIPTION"];
        //                            rowconsumabledefinition["_STATE_"] = row["_STATE_"].ToString();
        //                            rowconsumabledefinition["ISLOTMNG"] = row["LOTCONTROL"];
        //                            rowconsumabledefinition["VALIDSTATE"] = "Valid";
        //                            dtconsumabledefinition.Rows.Add(rowconsumabledefinition);

        //                        }

        //                    }

        //                }
        //            }

        //            DataSet dSchanged = new DataSet();

        //            // 품목
        //            changed.TableName = "itemmaster";
        //            dSchanged.Tables.Add(changed);

        //            dtItemserialI.TableName = "itemserial";
        //            dSchanged.Tables.Add(dtItemserialI);

        //            // 제품유형
        //            dtProductclass.TableName = "productclass";
        //            dSchanged.Tables.Add(dtProductclass);

        //            // 품목스펙
        //            dtproductitemspec.TableName = "productitemspec";
        //            dSchanged.Tables.Add(dtproductitemspec);

        //            // 제품 정보
        //            dtproductdefinition.TableName = "productdefinition";
        //            dSchanged.Tables.Add(dtproductdefinition);

        //            // 자재 그룹 정보
        //            dtconsumabledefinition.TableName = "consumabledefinition";
        //            dSchanged.Tables.Add(dtconsumabledefinition);


        //            ExecuteRule("ItemMaster", dSchanged);

        //            break;
        //        case 1:// AAG
        //               //changed = grdAAGList.GetChangedRows();



        //            //ExecuteRule("ItemMaster", changed);
        //            //ExecuteRule("AssignAttributeGroup", changed);
        //            break;
        //        case 2:// MDCI
        //            //changed = grdIdDefinitionList.GetChangedRows();
        //            //ExecuteRule("IdDefinitionManagement", changed);
        //            break;

        //    }


        //    //DataTable changed = new DataTable();
        //    //changed = grdIMList.GetChangedRows();

        //}

        #endregion 
        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
		{
			await base.OnSearchAsync();

			grdMain.View.ClearDatas();

            var values = Conditions.GetValues();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            DataTable dtIMList = SqlExecuter.Query("GetProductItemMaster", "10003", values);

			if (dtIMList.Rows.Count < 1)
			{ 
				// 조회할 데이터가 없습니다.
				ShowMessage("NoSelectData");
				return;
			}

			this.grdMain.DataSource = dtIMList;

        }
        #endregion


        /// <summary>
        /// 검색조건 팝업 예제
        /// </summary>
        private void InitializeCondition_Popup2()
        {
            //팝업 컬럼 설정
            var parentPopupColumn = Conditions.AddSelectPopup("ITEMID", new SqlQuery("GetProductItemGroup", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "ITEMID", "ITEMID")
                                              .SetPopupLayout("ITEMID", PopupButtonStyles.Ok_Cancel, true, false)  //팝업창 UI 설정.
                                              .SetPopupResultCount(0)  //팝업창 선택가능한 개수
                                              .SetPosition(1)
                                              .SetPopupApplySelection((selectRow, gridRow) => 
                                              {
                                                  List<string> productRevisionList = new List<string>();

                                                  selectRow.AsEnumerable().ForEach(r => 
                                                  {
                                                      productRevisionList.Add(Format.GetString(r["ITEMNAME"]));
                                                  });

                                                  Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFNAME").EditValue = string.Join(",", productRevisionList);
                                              });

            parentPopupColumn.Conditions.AddTextBox("ITEMID");
            parentPopupColumn.Conditions.AddTextBox("ITEMNAME");

            parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMID", 150);
            parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMNAME", 200);
            parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMVERSION", 100);
            parentPopupColumn.GridColumns.AddTextBoxColumn("SPEC", 250);
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFNAME").ReadOnly = true;
            Conditions.GetControl<SmartSelectPopupEdit>("ITEMID").EditValueChanged += ProductDefIDChanged;

			//조회기간 3개월 설정
			SmartPeriodEdit period = Conditions.GetControl<SmartPeriodEdit>("P_PERIOD");
			DateTime toDate = Convert.ToDateTime(period.datePeriodTo.EditValue);
			period.datePeriodFr.EditValue = toDate.AddMonths(-3);
		}

        private void ProductDefIDChanged(object sender, EventArgs e)
        {
            SmartSelectPopupEdit PopProdutid = sender as SmartSelectPopupEdit;

            if (Format.GetFullTrimString(PopProdutid.EditValue).Equals(string.Empty))
            {
                Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFNAME").EditValue = string.Empty;
            }
        }


        #region 유효성 검사
        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            DataTable changed = new DataTable();
            grdMain.View.CheckValidation();
            changed = grdMain.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
            foreach(DataRow row in changed.Rows)
            {
                // 제품일경우만 
                if (row["MASTERDATACLASSID"].ToString() == "Product")
                {
                    if (row["PRODUCTTYPE"].ToString() == "")
                    {
                        throw MessageException.Create("PRODUCTTYPE");
                    }
                }

            }
            
        }
        #endregion

        #region private Fuction

        #endregion

    }

  
}


