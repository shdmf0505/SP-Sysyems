#region using
using DevExpress.XtraTreeList.Nodes;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.StandardInfo.Popup;
using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프로그램명 : 기준정보 > Setup > 작업장 관리 
    /// 업 무 설명 :  Site 별(Plant) 작업장 관리 화면, AreaType에 따라 상하 구조를 가짐
    ///					area 와 equipmentclass 를 자원에 매핑한다.
    /// 생  성  자 :  정승원
    /// 생  성  일 :  2019-05-10
    /// 수정 이 력 : 
    /// 
    /// 
    /// </summary>
    public partial class AreaManagement : SmartConditionManualBaseForm
    {
        #region Local Variables

        //AreaType = Factory, Floor, Area
        private const string AREA_TYPE_FACTORY = "Factory"; //AreaType이 Factory인 것
        private const string AREA_TYPE_FLOOR = "Floor"; //AreaType이 Floor인 것
        private const string AREA_TYPE_AREA = "Area"; //AreaType이 Area인 것

        //Resource Type = Equipment
        private const string RESOURCE_TYPE_EQUIPMENT = "Production";   // EquipmentType
        private const string RESOURCE_CLASSID = "Machine";//ResourceClassId = Machine 고정 값

        private string _focusParentAreaId = "";
        private string _focusAreaId = "";


        #endregion

        #region 생성자
        public AreaManagement()
        {
            InitializeComponent();
        }
        #endregion

        #region 컨텐츠 영역 초기화
        /// <summary>
        /// 설정 초기화
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();
            grpArea.GridButtonItem = GridButtonItem.Refresh;
            InitializeLanguageKey();
            InitializeTreeArea();
            InitAreaGrid();
            InitResourceGrid();
            InitializeEvent();

            LoadDataArea();
            InitAreaStatusGrid();
            InitResourceListGrid();


            tabArea.SetLanguageKey(this.xtraTabPage1, "AREAREGIST");
            tabArea.SetLanguageKey(this.xtraTabPage2, "AREASTATUS");
            tabArea.SetLanguageKey(this.xtraTabPage3, "RESOURCESTATTUS");

            SetConditionVisiblility("P_PLANTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
            SetConditionVisiblility("P_CONDITIONVALUE", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
            SetConditionVisiblility("P_CONDITIONITEM2", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
            SetConditionVisiblility("P_CONDITIONITEM", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);

        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            tabArea.SetLanguageKey(this.xtraTabPage1, "AREAREGIST");
            tabArea.SetLanguageKey(this.xtraTabPage2, "AREASTATUS");
            tabArea.SetLanguageKey(this.xtraTabPage3, "RESOURCESTATTUS");
            grpArea.LanguageKey = "TREEAREALIST";
            grdAreaList.LanguageKey = "AREAINFOLIST";
            grdResourceList.LanguageKey = "RESOURCEINFOLIST";
            grdAreaClass.LanguageKey = "AREAINFOLIST";
            grdResource.LanguageKey = "RESOURCEINFOLIST";
        }

        /// <summary>
        /// 작업장 그리드 초기화
        /// </summary>
        private void InitAreaGrid()
        {
            grdAreaList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdAreaList.View.AddTextBoxColumn("ENTERPIRSEID").SetIsHidden().SetDefault(UserInfo.Current.Enterprise);
            grdAreaList.View.AddTextBoxColumn("PLANTID").SetIsHidden().SetDefault(UserInfo.Current.Plant);

            grdAreaList.View.AddTextBoxColumn("AREAID", 150);

            grdAreaList.View.AddLanguageColumn("AREANAME", 200);
            //grdAreaList.View.AddTextBoxColumn("DESCRIPTION", 150);
            //창고
            InitializeGrid_WarehouseListPopup();
            grdAreaList.View.AddTextBoxColumn("WAREHOUSENAME", 150);
            //거래처
            InitializeGrid_VendorListPopup();
            grdAreaList.View.AddTextBoxColumn("VENDORNAME", 150);

            //grdAreaList.View.AddTextBoxColumn("VENDORID", 150);


            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("CODECLASSID", "AreaType");
            param.Add("PLANTID", UserInfo.Current.Plant);

            grdAreaList.View.AddComboBoxColumn("AREATYPE", 100, new SqlQuery("GetTypeList", "10001", param), "CODENAME", "CODEID")
                .SetValidationIsRequired();

            if (UserInfo.Current.Enterprise.Equals("INTERFLEX"))
                grdAreaList.View.AddTreeListColumn("PARENTAREAID", 150, new SqlQuery("GetParentAreaId", "10001", param), "AREANAME", "AREAID", "PARENTAREAID")
                    .SetIsRefreshByOpen(true)
                    .SetIsHidden();
            else
                grdAreaList.View.AddTreeListColumn("PARENTAREAID", 150, new SqlQuery("GetParentAreaId", "10001", param), "AREANAME", "AREAID", "PARENTAREAID")
                    .SetIsRefreshByOpen(true);


            param["CODECLASSID"] = "OwnType";
            grdAreaList.View.AddComboBoxColumn("OWNTYPE", 150, new SqlQuery("GetTypeList", "10001", param), "CODENAME", "CODEID")
                .SetValidationIsRequired(); //자사구분

            if (UserInfo.Current.Enterprise.Equals("INTERFLEX"))
                grdAreaList.View.AddComboBoxColumn("FACTORYID", 150, new SqlQuery("GetFactoryInfo", "10001", param), "FACTORYNAME", "FACTORYID")
                    .SetIsHidden();
            else
                grdAreaList.View.AddComboBoxColumn("FACTORYID", 150, new SqlQuery("GetFactoryInfo", "10001", param), "FACTORYNAME", "FACTORYID"); //공장구분
            grdAreaList.View.AddComboBoxColumn("ISSUBCONTRACT", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                    .SetTextAlignment(TextAlignment.Center);

            //유효상태, 생성자, 수정자...
            grdAreaList.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);
            grdAreaList.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdAreaList.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdAreaList.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdAreaList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdAreaList.View.PopulateColumns();
        }

        /// <summary>
        /// 자원 그리드 초기화
        /// </summary>
        private void InitResourceGrid()
        {
            grdResourceList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdResourceList.GridButtonItem = GridButtonItem.All;

            grdResourceList.View.AddComboBoxColumn("RESOURCETYPE", 100, new SqlQuery("GetTypeList", "10001", "CODECLASSID=EquipmentType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            InitializeGrid_EquipmentClassListPopup();

            grdResourceList.View.AddTextBoxColumn("EQUIPMENTCLASSNAME", 200);
            grdResourceList.View.AddTextBoxColumn("RESOURCEID", 150);
            grdResourceList.View.AddTextBoxColumn("DESCRIPTION", 200).SetLabel("RESOURCENAME");

            grdResourceList.View.AddComboBoxColumn("STEPCLASS", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=StepType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                .SetEmptyItem("", "", true);

            grdResourceList.View.AddTextBoxColumn("STEPTYPE", 250);
            grdResourceList.View.AddComboBoxColumn("ISOSPINSPCONTROL", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));


            grdResourceList.View.AddTextBoxColumn("RESOURCECLASSID")
                .SetDefault(RESOURCE_CLASSID)
                .SetIsHidden();
            grdResourceList.View.AddTextBoxColumn("ENTERPRISEID")
                .SetDefault(UserInfo.Current.Enterprise)
                .SetIsHidden();
            grdResourceList.View.AddTextBoxColumn("PLANTID")
                .SetDefault(UserInfo.Current.Plant)
                .SetIsHidden();
            grdResourceList.View.AddTextBoxColumn("AREAID");
            grdResourceList.View.AddTextBoxColumn("AREANAME")
                .SetIsHidden();

            //유효상태, 생성자, 수정자...
            grdResourceList.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);
            grdResourceList.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdResourceList.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdResourceList.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdResourceList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdResourceList.View.PopulateColumns();
        }

        private void InitAreaStatusGrid()
        {
            grdAreaClass.GridButtonItem -= GridButtonItem.CRUD;
            grdAreaClass.GridButtonItem -= GridButtonItem.Import;

            grdAreaClass.View.SetIsReadOnly();
            grdAreaClass.View.AddTextBoxColumn("ENTERPIRSEID")
                .SetIsHidden()
                .SetDefault(UserInfo.Current.Enterprise);
            grdAreaClass.View.AddTextBoxColumn("PLANTID")
                    .SetIsHidden()
                    .SetDefault(UserInfo.Current.Plant);

            grdAreaClass.View.AddTextBoxColumn("AREAID", 80)
                .SetTextAlignment(TextAlignment.Center);
            grdAreaClass.View.AddLanguageColumn("AREANAME", 200);
            //grdAreaClass.View.AddTextBoxColumn("DESCRIPTION", 150);
            //창고
            grdAreaClass.View.AddTextBoxColumn("WAREHOUSEID", 80)
                .SetTextAlignment(TextAlignment.Center);
            grdAreaClass.View.AddTextBoxColumn("WAREHOUSENAME", 150);
            //거래처
            grdAreaClass.View.AddTextBoxColumn("VENDORID", 80)
                .SetTextAlignment(TextAlignment.Center);
            grdAreaClass.View.AddTextBoxColumn("VENDORNAME", 150);

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("CODECLASSID", "AreaType");
            param.Add("PLANTID", UserInfo.Current.Plant);

            grdAreaClass.View.AddComboBoxColumn("AREATYPE", 100, new SqlQuery("GetTypeList", "10001", param), "CODENAME", "CODEID")
                .SetTextAlignment(TextAlignment.Center);

            grdAreaClass.View.AddTreeListColumn("PARENTAREAID", 150, new SqlQuery("GetParentAreaId", "10001", param), "AREANAME", "AREAID", "PARENTAREAID")
                    .SetIsRefreshByOpen(true);

            param["CODECLASSID"] = "OwnType";
            grdAreaClass.View.AddComboBoxColumn("OWNTYPE", 80, new SqlQuery("GetTypeList", "10001", param), "CODENAME", "CODEID") //자사구분
                .SetTextAlignment(TextAlignment.Center);

            grdAreaClass.View.AddComboBoxColumn("FACTORYID", 80, new SqlQuery("GetFactoryListByPlant", "10001", param), "FACTORYNAME", "FACTORYID") //공장구분
                .SetTextAlignment(TextAlignment.Center);
            grdAreaClass.View.AddComboBoxColumn("ISSUBCONTRACT", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                 .SetTextAlignment(TextAlignment.Center);


            //유효상태, 생성자, 수정자...
            grdAreaClass.View.AddComboBoxColumn("VALIDSTATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetTextAlignment(TextAlignment.Center);
            grdAreaClass.View.AddTextBoxColumn("CREATOR", 80)
                    .SetIsReadOnly()
                    .SetTextAlignment(TextAlignment.Center);
            grdAreaClass.View.AddTextBoxColumn("CREATEDTIME", 130)
                    .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                    .SetIsReadOnly()
                    .SetTextAlignment(TextAlignment.Center);
            grdAreaClass.View.AddTextBoxColumn("MODIFIER", 80)
                    .SetIsReadOnly()
                    .SetTextAlignment(TextAlignment.Center);
            grdAreaClass.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                    .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                    .SetIsReadOnly()
                    .SetTextAlignment(TextAlignment.Center);

            grdAreaClass.View.PopulateColumns();
        }


        /// <summary>
		/// 자원 그리드 초기화
		/// </summary>
		private void InitResourceListGrid()
        {

            grdResource.View.SetIsReadOnly();
            grdResource.GridButtonItem -= GridButtonItem.CRUD;
            grdResource.GridButtonItem -= GridButtonItem.Import;

            grdResource.View.AddTextBoxColumn("RESOURCETYPE", 90)
                .SetTextAlignment(TextAlignment.Center);

            grdResource.View.AddTextBoxColumn("RESOURCEID", 120)
                .SetTextAlignment(TextAlignment.Center);
            grdResource.View.AddTextBoxColumn("DESCRIPTION", 200).SetLabel("RESOURCENAME");


            grdResource.View.AddTextBoxColumn("STEPCLASS", 80)
                .SetTextAlignment(TextAlignment.Center);
            grdResource.View.AddTextBoxColumn("STEPTYPE", 200)
                .SetTextAlignment(TextAlignment.Center);

            grdResource.View.AddTextBoxColumn("ISOSPINSPCONTROL", 80)
                .SetTextAlignment(TextAlignment.Center);

            grdResource.View.AddTextBoxColumn("RESOURCECLASSID")
                .SetIsHidden();
            grdResource.View.AddTextBoxColumn("PLANTID")
                .SetTextAlignment(TextAlignment.Center);
            grdResource.View.AddTextBoxColumn("AREAID", 80)
                .SetTextAlignment(TextAlignment.Center);
            grdResource.View.AddTextBoxColumn("AREANAME", 200);
            grdResource.View.AddTextBoxColumn("EQUIPMENTCLASSID", 80)
                .SetTextAlignment(TextAlignment.Center);

            grdResource.View.AddTextBoxColumn("EQUIPMENTCLASSNAME", 150);
            //유효상태, 생성자, 수정자...
            grdResource.View.AddTextBoxColumn("VALIDSTATE", 60)
                .SetTextAlignment(TextAlignment.Center);
            grdResource.View.AddTextBoxColumn("CREATOR", 80)
                .SetTextAlignment(TextAlignment.Center);
            grdResource.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center);
            grdResource.View.AddTextBoxColumn("MODIFIER", 80)
                .SetTextAlignment(TextAlignment.Center);
            grdResource.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center);

            grdResource.View.PopulateColumns();
        }

        /// <summary>
        /// 설비 팝업
        /// </summary>
        private void InitializeGrid_EquipmentClassListPopup()
        {
            Micube.Framework.SmartControls.Validations.ValidationResultCommon result = new Framework.SmartControls.Validations.ValidationResultCommon();

            var parentCodeClassPopupColumn = this.grdResourceList.View.AddSelectPopupColumn("EQUIPMENTCLASSID", new SqlQuery("SelectEquipMentClass", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("SELECTEQUIPMENTCLASSID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(0)
                .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupValidationCustom(ValidationEquipmentClassIdPopup)
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {

                    DataTable dt2 = grdResourceList.DataSource as DataTable;
                    int handle = grdResourceList.View.FocusedRowHandle;
                    dt2.Rows.RemoveAt(handle);

                    DataRow dr = grdAreaList.View.GetFocusedDataRow();

                    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                    foreach (DataRow row in selectedRows)
                    {
                        DataRow newrow = dt2.NewRow();

                        newrow["RESOURCECLASSID"] = RESOURCE_CLASSID;
                        newrow["RESOURCETYPE"] = RESOURCE_TYPE_EQUIPMENT;
                        newrow["EQUIPMENTCLASSID"] = row["EQUIPMENTCLASSID"];
                        newrow["EQUIPMENTCLASSNAME"] = row["EQUIPMENTCLASSNAME"];

                        string currentLanguage = "$$" + UserInfo.Current.LanguageType.ToUpper();
                        string areaName = "AREANAME" + currentLanguage;

                        //resource Id 만듦
                        newrow["RESOURCEID"] = dr["AREAID"].ToString() + row["EQUIPMENTCLASSID"].ToString();
                        newrow["DESCRIPTION"] = dr[areaName].ToString() + " " + row["EQUIPMENTCLASSNAME"];

                        // Default 값
                        newrow["PLANTID"] = dr["PLANTID"];
                        newrow["AREAID"] = dr["AREAID"];
                        newrow["AREANAME"] = dr[areaName].ToString();
                        newrow["VALIDSTATE"] = "Valid";

                        dt2.Rows.Add(newrow);
                    }
                });


            // 팝업에서 사용할 조회조건 항목 추가
            parentCodeClassPopupColumn.Conditions.AddTextBox("EQUIPMENTCLASSID");
            parentCodeClassPopupColumn.Conditions.AddTextBox("EQUIPMENTCLASSNAME");

            // 팝업 그리드 설정
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSID", 100);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSNAME", 150);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("TOPEQUIPMENTCLASS", 150);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("MIDDLEEQUIPMENTCLASS", 150);

        }

        /// <summary>
        /// 거래처 팝업
        /// </summary>
        private void InitializeGrid_VendorListPopup()
        {
            var values = Conditions.GetValues();
            string plantId = values["P_PLANTID"].ToString();

            var vendorPopupColumn = grdAreaList.View.AddSelectPopupColumn("VENDORID", new SqlQuery("GetVendorList", "10001"))
                .SetPopupLayout("SELECTVENDORID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("VENDORNAME")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    foreach (DataRow row in selectedRows)
                    {
                        dataGridRow["VENDORNAME"] = row["VENDORNAME"].ToString();
                    }
                });

            vendorPopupColumn.Conditions.AddComboBox("PLANTID", new SqlQuery("GetPlantList", "10001"), "PLANTNAME", "PLANTID")
                .SetLabel("SITE")
                .SetIsReadOnly()
                .SetPopupDefaultByGridColumnId("PLANTID");
            vendorPopupColumn.Conditions.AddTextBox("VENDORID");

            vendorPopupColumn.GridColumns.AddTextBoxColumn("VENDORID", 150);
            vendorPopupColumn.GridColumns.AddTextBoxColumn("VENDORNAME", 250);



        }

        /// <summary>
        /// 창고 컬럼 팝업
        /// </summary>
        private void InitializeGrid_WarehouseListPopup()
        {
            var values = Conditions.GetValues();
            //string plantId = values["P_PLANTID"].ToString();

            var warehousePopupColumn = grdAreaList.View.AddSelectPopupColumn("WAREHOUSEID", new SqlQuery("GetWarehouseList", "10002"))
                .SetPopupLayout("SELECTWAREHOUSEID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("WAREHOUSENAME")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    foreach (DataRow row in selectedRows)
                    {
                        dataGridRow["WAREHOUSENAME"] = row["WAREHOUSENAME"].ToString();
                    }
                });


            warehousePopupColumn.Conditions.AddComboBox("PLANTID", new SqlQuery("GetPlantList", "10001"), "PLANTNAME", "PLANTID")
                .SetLabel("SITE")
                .SetIsReadOnly()
                .SetPopupDefaultByGridColumnId("PLANTID");

            warehousePopupColumn.Conditions.AddTextBox("TXTWAREHOUSE");

            warehousePopupColumn.GridColumns.AddTextBoxColumn("WAREHOUSEID", 150)
                .SetValidationKeyColumn()
                .SetIsReadOnly();
            warehousePopupColumn.GridColumns.AddTextBoxColumn("WAREHOUSENAME", 200)
                .SetIsReadOnly();

        }


        #endregion

        #region 이벤트
        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            tabArea.SelectedPageChanging += TabArea_SelectedPageChanging;
            grpArea.CustomButtonClick += SmartGroupBox1_CustomButtonClick;
            treeParentArea.FocusedNodeChanged += TreeParentArea_FocusedNodeChanged;

            grdAreaList.View.AddingNewRow += View_AddingNewRow_Area;
            grdAreaList.View.FocusedRowChanged += grdAreaList_FocusedRowChanged;
            grdAreaList.View.CellValueChanged += grdAreaList_CellValueChanged;
            grdAreaList.View.ShowingEditor += View_ShowingEditor;
            grdAreaList.View.CustomRowFilter += View_CustomRowFilter;

            grdResourceList.View.ValidateRow += grdResourceList_ValidateRow;
            grdResourceList.View.FocusedColumnChanged += grdResourceList_FocusedColumnChanged;
            grdResourceList.View.AddingNewRow += View_AddingNewRow_Resource;

            tabArea.SelectedPageChanged += TabArea_SelectedPageChanged;
        }

        /// <summary>
        /// 필터 걸었을 때 보여지는 첫번째 row로 포커스 이동
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CustomRowFilter(object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
        {
            if (e == null) return;

            if (e.Visible)
            {
                grdAreaList.View.FocusedRowHandle = e.ListSourceRow;
                return;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DataRow selectRow = grdAreaList.View.GetFocusedDataRow();
            if (selectRow == null) return;

            string focusColumn = grdAreaList.View.FocusedColumn.FieldName;

            if (!string.IsNullOrWhiteSpace(Format.GetString(selectRow["WAREHOUSEID"])) && (focusColumn.Equals("WAREHOUSEID") || focusColumn.Equals("WAREHOUSENAME")))
            {
                e.Cancel = true;
            }
        }

        private void TabArea_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            var buttons = pnlToolbar.Controls.Find<SmartButton>(true);
            if (e.Page.Name == "xtraTabPage1")
            {
                SetConditionVisiblility("P_PLANTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
                SetConditionVisiblility("P_CONDITIONVALUE", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
                SetConditionVisiblility("P_CONDITIONITEM2", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
                SetConditionVisiblility("P_CONDITIONITEM", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);

                foreach (SmartButton button in buttons)
                {
                    if (button.Name == "Save")
                        button.Visible = true;
                }

            }
            else if (e.Page.Name == "xtraTabPage2") // 작업장 현황
            {
                SetConditionVisiblility("P_PLANTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
                SetConditionVisiblility("P_CONDITIONITEM2", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
                SetConditionVisiblility("P_CONDITIONITEM", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
                SetConditionVisiblility("P_CONDITIONVALUE", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);

                foreach (SmartButton button in buttons)
                {
                    if (button.Name == "Save")
                        button.Visible = false;
                }

            }
            else if (e.Page.Name == "xtraTabPage3")   // 자원 현황
            {
                SetConditionVisiblility("P_PLANTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
                SetConditionVisiblility("P_CONDITIONITEM", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
                SetConditionVisiblility("P_CONDITIONITEM2", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
                SetConditionVisiblility("P_CONDITIONVALUE", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);

                foreach (SmartButton button in buttons)
                {
                    if (button.Name == "Save")
                        button.Visible = false;
                }
            }
        }

        /// <summary>
        /// 탭 이동 시 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabArea_SelectedPageChanging(object sender, DevExpress.XtraTab.TabPageChangingEventArgs e)
        {
            DataTable dtAreaChanged = grdAreaList.GetChangedRows();
            DataTable dtResourceChanged = grdResourceList.GetChangedRows();

            if (tabArea.SelectedTabPageIndex.Equals(0))
            {
                if (dtAreaChanged.Rows.Count > 0 || dtResourceChanged.Rows.Count > 0)
                {
                    DialogResult result = ShowMessageBox("WRITTENBEDELETE", Language.Get("MESSAGEINFO"), MessageBoxButtons.OKCancel);
                    if (result == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        DataTable dtArea = (grdAreaList.DataSource as DataTable).Clone();
                        foreach (DataRow aRow in (grdAreaList.DataSource as DataTable).Rows)
                        {
                            foreach (DataRow cRow in dtAreaChanged.Rows)
                            {
                                if (!aRow["AREAID"].Equals(cRow["AREAID"]))
                                {
                                    dtArea.ImportRow(aRow);
                                }
                            }
                        }

                        DataTable dtResource = (grdResourceList.DataSource as DataTable).Clone();
                        foreach (DataRow rRow in (grdResourceList.DataSource as DataTable).Rows)
                        {
                            foreach (DataRow cRow in dtResourceChanged.Rows)
                            {
                                if (!rRow["RESOURCEID"].Equals(cRow["RESOURCEID"]))
                                {
                                    dtResource.ImportRow(rRow);
                                }
                            }
                        }

                        grdAreaList.DataSource = dtArea;
                        grdResourceList.DataSource = dtResource;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdResourceList_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DataRow focusRow = grdResourceList.View.GetFocusedDataRow();
            if (focusRow == null) return;
            if (focusRow["STEPCLASS"].ToString().Equals(""))
            {
                return;
            }
            switch (grdResourceList.View.FocusedColumn.FieldName)
            {
                case "STEPCLASS":
                    Dictionary<string, object> Param = new Dictionary<string, object>();
                    Param.Add("CODECLASSID", focusRow["STEPCLASS"].ToString());
                    Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    DataTable dtStep = SqlExecuter.Query("GetCodeList", "00001", Param);

                    string sSteptype = "";
                    foreach (DataRow rowStep in dtStep.Rows)
                    {
                        sSteptype = sSteptype + rowStep["CODENAME"].ToString() + ",";
                    }
                    sSteptype = sSteptype.Substring(0, sSteptype.Length - 1);
                    sSteptype = sSteptype + "";
                    focusRow["STEPTYPE"] = sSteptype;
                    break;
            }

        }

        private void grdResourceList_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            DataRow row = grdResourceList.View.GetFocusedDataRow();
            if (row == null) return;

            if (row.RowState != DataRowState.Added)
            {
                switch (grdResourceList.View.FocusedColumn.FieldName)
                {
                    case "VALIDSTATE":
                    case "STEPCLASS":
                    case "ISOSPINSPCONTROL":
                        grdResourceList.View.SetIsReadOnly(false);
                        break;
                    default:
                        grdResourceList.View.SetIsReadOnly(true);
                        break;
                }
            }
            else
            {
                grdResourceList.View.SetIsReadOnly(false);
            }
        }


        private void grdAreaList_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow row = grdAreaList.View.GetFocusedDataRow();
            if (row == null) return;
            switch (e.Column.FieldName)
            {

                case "VALIDSTATE":
                    foreach (DataRow rowResource in ((DataTable)grdResourceList.DataSource).Select("AREAID = '" + row["AREAID"].ToString() + "'"))
                    {
                        rowResource["VALIDSTATE"] = e.Value;
                    }
                    break;

            }



        }

        private void grdAreaList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grdAreaList.View.FocusedRowHandle < 0 || e.PrevFocusedRowHandle < 0) return;

            DataRow drarea = grdAreaList.View.GetFocusedDataRow();
            if (Format.GetString(drarea["AREATYPE"]).Equals("Area"))
            {
                grdResourceList.GridButtonItem = GridButtonItem.All;
            }
            else
            {
                grdResourceList.GridButtonItem = GridButtonItem.None;
                grdResourceList.GridButtonItem = GridButtonItem.Export | GridButtonItem.Import;

            }

            DataTable changed = grdResourceList.GetChangedRows();
            if (changed.Rows.Count > 0)
            {
                DialogResult result = ShowMessageBox("WRITTENBEDELETE", Language.Get("MESSAGEINFO"), MessageBoxButtons.OKCancel);
                if (result == DialogResult.Cancel)
                {
                    grdAreaList.View.FocusedRowChanged -= grdAreaList_FocusedRowChanged;
                    grdAreaList.View.FocusedRowHandle = e.PrevFocusedRowHandle;
                    grdAreaList.View.SelectRow(e.PrevFocusedRowHandle);
                    grdAreaList.View.FocusedRowChanged += grdAreaList_FocusedRowChanged;
                    return;
                }
            }




            DataRow row = grdAreaList.View.GetFocusedDataRow();
            if (row == null) return;

            _focusAreaId = Format.GetString(row["AREAID"]);
            DataTable dtResourceList = (DataTable)grdResourceList.DataSource;
            if (dtResourceList != null)
            {
                dtResourceList.Clear();
            }

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("areaId", row["AREAID"].ToString());
            param.Add("LanguageType", UserInfo.Current.LanguageType);
            param.Add("p_validState", Conditions.GetValues()["P_VALIDSTATE"].ToString());

            dtResourceList = SqlExecuter.Query("SelectResourceList", "10001", param);//Procedure("usp_com_selectresource", param);
            grdResourceList.DataSource = dtResourceList;
        }




        /// <summary>
        /// 자원 grid row 추가 시 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow_Resource(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (!UserInfo.Current.Plant.Equals(Conditions.GetValues()["P_PLANTID"].ToString()))
            {
                // 사용자의 plant 와 조회된 Plant가 다를경우는 추가 안됨.
                ShowMessage(MessageBoxButtons.OK, "Powers");
                args.IsCancel = true; //취소
                return;
            }


            if (grdAreaList.View.FocusedRowHandle < 0)
                return;



            DataRow row = grdAreaList.View.GetFocusedDataRow();

            if (row["AREAID"].ToString() == "" || row["AREATYPE"].ToString() != "Area")
            {
                // 작업장 Type 이 작업장이 아닐경우 등록 안되게.

                ShowMessage(MessageBoxButtons.OK, "NoAreaSelected");
                args.IsCancel = true; //취소
                return;
            }

            args.NewRow["PLANTID"] = row["PLANTID"];

            args.NewRow["AREAID"] = row["AREAID"];
            string currentLanguage = "$$" + UserInfo.Current.LanguageType.ToUpper();
            string areaName = "AREANAME" + currentLanguage;

            args.NewRow["AREANAME"] = row[areaName].ToString();
            //args.NewRow["STEPCLASS"] = string.Empty;

        }

        /// <summary>
        /// GroupBox 새로 고침 눌렀을 때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SmartGroupBox1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "GridRefresh")
            {
                InitializeTreeArea();
                treeParentArea.FocusedNode = treeParentArea.GetNodeByVisibleIndex(0);
            }
        }

        /// <summary>
        /// 트리 리스트에서 다른 노드 선택했을 때 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeParentArea_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node == null || e.OldNode == null) return;
            treeParentArea.FocusedNodeChanged -= TreeParentArea_FocusedNodeChanged;

            DataTable dtAreaChanged = grdAreaList.GetChangedRows();
            DataTable dtResourceChanged = grdResourceList.GetChangedRows();

            if (dtAreaChanged.Rows.Count > 0 || dtResourceChanged.Rows.Count > 0)
            {
                DialogResult result = ShowMessageBox("WRITTENBEDELETE", Language.Get("MESSAGEINFO"), MessageBoxButtons.OKCancel);
                if (result == DialogResult.Cancel)
                {
                    treeParentArea.FocusedNode = e.OldNode;
                    treeParentArea.FocusedNodeChanged += TreeParentArea_FocusedNodeChanged;
                    return;
                }
            }

            var values = Conditions.GetValues();

            string plantId = UserInfo.Current.Plant;
            if (!values["P_PLANTID"].Equals("*") && !values["P_PLANTID"].Equals(UserInfo.Current.Plant))
            {
                plantId = values["P_PLANTID"].ToString();
            }

            DataRow focusRow = treeParentArea.GetFocusedDataRow();
            string areaType = focusRow["AREAID"].Equals("*") ? AREA_TYPE_FACTORY : "*";

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("p_plantId", plantId);
            param.Add("p_parentAreaId", focusRow["AREAID"].ToString());
            param.Add("p_areaType", areaType);
            param.Add("p_validState", Conditions.GetValues()["P_VALIDSTATE"].ToString());
            param.Add("p_conditionItem", "*");
            param.Add("p_conditionValue", "");

            switch (focusRow["AREATYPE"].ToString())
            {
                case AREA_TYPE_AREA:
                    grdAreaList.View.OptionsCustomization.AllowFilter = false;
                    grdAreaList.View.OptionsCustomization.AllowSort = false;
                    grdAreaList.ShowButtonBar = false;

                    grdAreaList.DataSource = null;

                    DataTable dt = (DataTable)grdAreaList.DataSource;
                    if (dt != null)
                    {
                        dt.Clear();
                    }

                    break;

                default:
                    grdAreaList.View.OptionsCustomization.AllowFilter = true;
                    grdAreaList.View.OptionsCustomization.AllowSort = true;
                    grdAreaList.ShowButtonBar = true;

                    grdAreaList.DataSource = SqlExecuter.Query("SelectAreaList", "10001", param);//Procedure("usp_com_selectarea_grid", param);

                    DataRow drarea = grdAreaList.View.GetFocusedDataRow();
                    if (drarea != null)
                    {
                        if (Format.GetString(drarea["AREATYPE"]).Equals("Area"))
                        {
                            grdResourceList.GridButtonItem = GridButtonItem.All;
                        }
                        else
                        {
                            grdResourceList.GridButtonItem = GridButtonItem.None;
                            grdResourceList.GridButtonItem = GridButtonItem.Export | GridButtonItem.Import;

                        }
                    }

                    DataTable dtResourceList = (DataTable)grdResourceList.DataSource;
                    if (dtResourceList != null)
                    {
                        dtResourceList.Clear();
                    }




                    if (!string.IsNullOrEmpty(this._focusAreaId))
                        grdAreaList.View.FocusedRowHandle = grdAreaList.View.GetRowHandleByValue("AREAID", _focusAreaId);

                    DataRow row = grdAreaList.View.GetFocusedDataRow();
                    if (row != null)
                    {
                        Dictionary<string, object> paramAreaList = new Dictionary<string, object>();
                        paramAreaList.Add("areaId", row["AREAID"].ToString());
                        paramAreaList.Add("LanguageType", UserInfo.Current.LanguageType);
                        paramAreaList.Add("p_validState", Conditions.GetValues()["P_VALIDSTATE"].ToString());

                        dtResourceList = SqlExecuter.Query("SelectResourceList", "10001", paramAreaList);//Procedure("usp_com_selectresource", param);
                        grdResourceList.DataSource = dtResourceList;
                    }


                    break;
            }

            treeParentArea.FocusedNodeChanged += TreeParentArea_FocusedNodeChanged;

        }

        /// <summary>
        /// Grid에서 새 항목 추가 시 ParentArea / AreaType 설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow_Area(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (!UserInfo.Current.Plant.Equals(Conditions.GetValues()["P_PLANTID"].ToString()))
            {
                // 사용자의 plant 와 조회된 Plant가 다를경우는 추가 안됨.
                ShowMessage(MessageBoxButtons.OK, "Powers");
                args.IsCancel = true; //취소
                return;
            }

            DataRow focusRow = treeParentArea.GetFocusedDataRow();
            string parentAreaId = focusRow["AREAID"].ToString();

            args.NewRow["PARENTAREAID"] = parentAreaId == "*" ? "" : parentAreaId;

            args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;

            string areaType = focusRow["AREATYPE"].ToString();
            if (areaType == "")
            {
                args.NewRow["AREATYPE"] = AREA_TYPE_FACTORY;
            }
            else if (areaType.Equals("Floor"))
            {
                args.NewRow["ISSUBCONTRACT"] = "N";
            }

            else
            {
                args.NewRow["AREATYPE"] = areaType == AREA_TYPE_FACTORY ? AREA_TYPE_FLOOR : AREA_TYPE_AREA;
                args.NewRow["FACTORYID"] = focusRow["PARENTAREAID"].ToString();
            }

            if (args.NewRow["AREATYPE"].Equals(AREA_TYPE_AREA))
            {
                args.NewRow["AREAID"] = parentAreaId;
            }
        }
        #endregion

        #region 조회조건 영역

        /// <summary>
        /// 조회조건 영역 초기화 시작
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
        }

        #endregion

        #region 툴바
        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            _focusParentAreaId = treeParentArea.GetFocusedDataRow()["AREAID"].ToString();

            base.OnToolbarSaveClick();

            DataTable dtArea = grdAreaList.GetChangedRows();
            DataTable dtResource = grdResourceList.GetChangedRows();


            if (dtArea.Rows.Count > 0 || dtResource.Rows.Count > 0)
            {
                DataTable dtResourceCopy = new DataTable();
                foreach (DataRow row in dtArea.Rows)
                {
                    // 수정시 자원업데이트
                    if (row["PARENTAREAID"].ToString().Equals("") && (row["AREATYPE"].ToString().Equals("Floor") || row["AREATYPE"].ToString().Equals("Area")))
                    {
                        throw MessageException.Create("TopAreaRequired");
                    }

                    if (row["_STATE_"].ToString() == "modified")
                    {
                        Dictionary<string, object> param = new Dictionary<string, object>();
                        param.Add("areaId", row["AREAID"].ToString());
                        param.Add("LanguageType", UserInfo.Current.LanguageType);
                        DataTable dtResourceList = SqlExecuter.Query("SelectResourceList", "10001", param);//Procedure("usp_com_selectresource", param);

                        dtResourceCopy = dtResourceList.Copy();

                        dtResourceCopy.Columns.Add("_STATE_");

                        foreach (DataRow rowNew in dtResourceCopy.Rows)
                        {

                            if (String.IsNullOrEmpty(rowNew["DESCRIPTION"].ToString()))
                            {
                                if (UserInfo.Current.LanguageType.ToUpper() == "KO-KR")
                                {
                                    rowNew["DESCRIPTION"] = row["AREANAME$$KO-KR"] + " " + rowNew["EQUIPMENTCLASSNAME"];
                                }

                                if (UserInfo.Current.LanguageType.ToUpper() == "EN-US")
                                {
                                    rowNew["DESCRIPTION"] = row["AREANAME$$EN-US"] + " " + rowNew["EQUIPMENTCLASSNAME"];
                                }

                                if (UserInfo.Current.LanguageType.ToUpper() == "ZH-CN")
                                {
                                    rowNew["DESCRIPTION"] = row["AREANAME$$ZH-CN"] + " " + rowNew["EQUIPMENTCLASSNAME"];
                                }

                                if (UserInfo.Current.LanguageType.ToUpper() == "VI-VN")
                                {
                                    rowNew["DESCRIPTION"] = row["AREANAME$$VI-VN"] + " " + rowNew["EQUIPMENTCLASSNAME"];
                                }
                            }
                            rowNew["_STATE_"] = "modified";
                        }
                    }
                }



                MessageWorker worker = new MessageWorker("AreaManagement");
                worker.SetBody(new MessageBody()
                {
                    { "enterpriseId", UserInfo.Current.Enterprise },
                    { "plantId", UserInfo.Current.Plant },
                    { "areaList", dtArea },
                    { "resourceList", dtResource }

                });
                worker.Execute();

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

            int page = tabArea.SelectedTabPageIndex;
            switch (page)
            {
                case 0:

                    try
                    {
                        grdAreaList.View.FocusedRowChanged -= grdAreaList_FocusedRowChanged;

                        TreeListNode prevTreeNode = treeParentArea.FocusedNode;
                        int iPrevAreaHandle = grdAreaList.View.FocusedRowHandle;
                        int iPrevResourceHandle = grdResourceList.View.FocusedRowHandle;

                        grdAreaList.View.ClearDatas();
                        grdResourceList.View.ClearDatas();

                        var values = Conditions.GetValues();
                        values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                        DataTable dtAreaTree = SqlExecuter.Query("SelectAreaTreeList", "10001", values);
                        dtAreaTree.DefaultView.RowFilter = "AREATYPE IN ('Factory','Floor')";
                        treeParentArea.DataSource = dtAreaTree.DefaultView.ToTable();
                        treeParentArea.PopulateColumns();
                        treeParentArea.ExpandAll();

                        treeParentArea.FocusedNode = prevTreeNode;

                        grdAreaList.View.FocusedRowChanged += grdAreaList_FocusedRowChanged;

                        //SET 작업장 focus
                        int areaHandle = (iPrevAreaHandle <= 0) ? 0 : iPrevAreaHandle;
                        grdAreaList.View.FocusedRowHandle = areaHandle;


                        //SET 자원 focus
                        int resourceHandle = (iPrevResourceHandle <= 0) ? 0 : iPrevResourceHandle;
                        grdResourceList.View.FocusedRowHandle = resourceHandle;
                    }
                    catch (Exception)
                    {
                        grdAreaList.View.FocusedRowChanged += grdAreaList_FocusedRowChanged;
                    }
                    /*
                    DataRow treeRow = treeParentArea.GetFocusedDataRow();
                    if(treeRow != null && (Format.GetString(treeRow["AREATYPE"]).Equals(AREA_TYPE_FACTORY) || Format.GetString(treeRow["AREAID"]).Equals("*")))
                    {
                        treeParentArea.FocusedNodeChanged -= TreeParentArea_FocusedNodeChanged;
                        InitializeTreeArea();
                        treeParentArea.FocusedNodeChanged += TreeParentArea_FocusedNodeChanged;
                    }

                    if (!string.IsNullOrEmpty(_focusParentAreaId))
                    {
                        treeParentArea.FocusedNodeChanged -= TreeParentArea_FocusedNodeChanged;
                        treeParentArea.SetFocusedNode(treeParentArea.FindNodeByFieldValue("AREAID", _focusParentAreaId));
                        treeParentArea.FocusedNodeChanged += TreeParentArea_FocusedNodeChanged;
                    }

                    int focusIndex = grdAreaList.View.FocusedRowHandle;
                    DataRow focusRow = treeParentArea.GetFocusedDataRow();
                    var values = Conditions.GetValues();

                    grdResourceList.View.ClearDatas();
                    grdAreaList.View.ClearDatas();

                    switch (values["P_CONDITIONVALUE"].ToString())
                    {
                        case "":
                        case "*":
                            values.Add("P_PARENTAREAID", focusRow["AREAID"]);
                            break;
                    }
                    values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);


                    grdAreaList.DataSource = await QueryAsync("SelectAreaList", "10001", values);

                    focusIndex = (focusIndex <= 0) ? 0 : focusIndex;

                    grdAreaList.View.FocusedRowHandle = focusIndex;
                    grdAreaList.View.SelectRow(focusIndex);
                    */
                    break;
                case 1:

                    var values2 = Conditions.GetValues();
                    values2.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                    DataTable dtareaList = await QueryAsync("SelectAreaList", "10002", values2);

                    if (dtareaList.Rows.Count < 1)
                    {
                        ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                    }

                    grdAreaClass.DataSource = dtareaList;
                    break;
                case 2:

                    var values3 = Conditions.GetValues();
                    values3.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                    DataTable dtareaList2 = await QueryAsync("SelectResourceList", "10002", values3);

                    if (dtareaList2.Rows.Count < 1)
                    {
                        ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                    }

                    grdResource.DataSource = dtareaList2;
                    break;
            }


            /*  UTC 시간설정 Test
			for (int i = 0; i < dtAreaList.Rows.Count; i++)
			{
				DataRow row = dtAreaList.Rows[i];

				string createdTime = row["CREATEDTIME"].ToString();
				if (!string.IsNullOrEmpty(createdTime))
				{
					DateTime dateTime = Convert.ToDateTime(createdTime);
					row["CREATEDTIME"] = (dateTime.ToLocalTime()).ToString();
					Console.WriteLine(row["CREATEDTIME"]);
				}
			}
			*/

        }
        #endregion

        #region 유효성 검사
        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            DataTable dbAreaList = new DataTable();
            dbAreaList = grdAreaList.GetChangedRows();
            if (dbAreaList.Rows.Count != 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                grdAreaList.View.CheckValidation();
            }

            foreach (DataRow row in dbAreaList.Rows)
            {
                switch (row["OWNTYPE"].ToString())
                {
                    case "InHouseOSP":
                    case "OutsideOSP":
                    case "MajorSuppliers":
                        if (row["AREATYPE"].ToString() == "Area" && row["VENDORID"].ToString() == "")
                        {
                            throw MessageException.Create("OWNTYPEOSP");
                        }
                        break;
                    default:
                        break;
                }

                if (row["_STATE_"].ToString().Equals("deleted"))
                {
                    // lot  등록여부
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());
                    param.Add("AREAID", row["AREAID"].ToString());
                    DataTable dtAreaUpdateChk = SqlExecuter.Query("GetAreaUpdateChk", "10001", param);

                    if (dtAreaUpdateChk != null)
                    {
                        if (dtAreaUpdateChk.Rows.Count != 0)
                        {
                            throw MessageException.Create("LotWorkResult");
                        }
                    }

                    // 자원 등록여부
                    Dictionary<string, object> paramRoutingArea = new Dictionary<string, object>();
                    paramRoutingArea.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());
                    paramRoutingArea.Add("AREAID", row["AREAID"].ToString());
                    DataTable dtRoutingAreaUpdateChk = SqlExecuter.Query("GetRoutingAreaUpdateChk", "10001", paramRoutingArea);

                    if (dtRoutingAreaUpdateChk != null)
                    {
                        if (dtRoutingAreaUpdateChk.Rows.Count != 0)
                        {
                            throw MessageException.Create("ResourceCheck");
                        }
                    }


                    // 설비 등록여부
                    Dictionary<string, object> paramEquipmentChk = new Dictionary<string, object>();
                    paramEquipmentChk.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());
                    paramEquipmentChk.Add("AREAID", row["AREAID"].ToString());
                    DataTable dtEquipmentChk = SqlExecuter.Query("GetEquipmentChk", "10001", paramEquipmentChk);
                    if (dtEquipmentChk != null)
                    {
                        if (dtEquipmentChk.Rows.Count != 0)
                        {
                            throw MessageException.Create("EquipmentCheck");
                        }
                    }
                }

            }


            DataTable dbResourceList = new DataTable();
            dbResourceList = grdResourceList.GetChangedRows();

            if (dbResourceList.Rows.Count != 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                grdResourceList.View.CheckValidation();
            }

            foreach (DataRow row in dbResourceList.Rows)
            {
                if (row["_STATE_"].ToString().Equals("deleted"))
                {
                    // 자원 등록여부
                    Dictionary<string, object> paramResource = new Dictionary<string, object>();
                    paramResource.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());
                    paramResource.Add("RESOURCEID", row["RESOURCEID"].ToString());
                    DataTable dtBillofresourceChk = SqlExecuter.Query("GetBillofresourceChk", "10001", paramResource);
                    if (dtBillofresourceChk != null)
                    {
                        if (dtBillofresourceChk.Rows.Count != 0)
                        {
                            throw MessageException.Create("ResourceCheck");
                        }
                    }
                }
            }


            if (dbAreaList.Rows.Count == 0 && dbResourceList.Rows.Count == 0)
            {
                throw MessageException.Create("NoSaveData");
            }

        }
        #endregion

        #region private Fuction

        /// <summary>
        /// area 체크 확인
        /// </summary>
        private void CheckedAreaRows(DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                throw MessageException.Create("NoAreaSelected");
            }
            else if (dt.Rows.Count > 1)
            {
                throw MessageException.Create("SelectOnlyOneArea");
            }

            foreach (DataRow row in dt.Rows)
            {
                string areaType = row["AREATYPE"].ToString();
                if (!areaType.Equals(AREA_TYPE_AREA))
                {
                    throw MessageException.Create("WrongSelectAreaType");
                }
            }
        }

        /// <summary>
        /// 팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑
        /// </summary>
        /// <param name="currentGridRow">화면 grid row</param>
        /// <param name="popupSelections">팝업에서 선택한 row</param>
        /// <returns></returns>
        private Micube.Framework.SmartControls.Validations.ValidationResultCommon ValidationEquipmentClassIdPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            Micube.Framework.SmartControls.Validations.ValidationResultCommon result = new Framework.SmartControls.Validations.ValidationResultCommon();

            object obj = grdResourceList.DataSource;
            DataTable dt = (DataTable)obj;

            foreach (DataRow row in popupSelections)
            {
                if (dt.Select("EQUIPMENTCLASSID = '" + row["EQUIPMENTCLASSID"].ToString() + "'").Length != 0)
                {
                    Language.LanguageMessageItem item = Language.GetMessage("SelectOverlap", row["EQUIPMENTCLASSID"].ToString());
                    result.IsSucced = false;
                    result.FailMessage = item.Message;
                    result.Caption = item.Title;
                }
            }

            return result;
        }

        /// <summary>
        /// Area Tree 초기화
        /// </summary>
        private void InitializeTreeArea()
        {
            treeParentArea.SetResultCount(1);
            treeParentArea.SetIsReadOnly();
            treeParentArea.SetEmptyRoot("Root", "*");
            treeParentArea.SetMember("AREANAME", "AREAID", "PARENTAREAID");

            var values = Conditions.GetValues();

            string plantId = UserInfo.Current.Plant;
            if (!values["P_PLANTID"].Equals("*") && !values["P_PLANTID"].Equals(UserInfo.Current.Plant))
            {
                plantId = values["P_PLANTID"].ToString();
            }

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_PLANTID", plantId);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dtAreaTree = SqlExecuter.Query("SelectAreaTreeList", "10001", param);

            dtAreaTree.DefaultView.RowFilter = "AREATYPE IN ('Factory','Floor')";

            treeParentArea.DataSource = dtAreaTree.DefaultView.ToTable();


            treeParentArea.PopulateColumns();

            treeParentArea.ExpandAll();

        }

        /// <summary>
        /// Area Grid 데이터 로드
        /// </summary>
        private async void LoadDataArea()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("p_plantId", UserInfo.Current.Plant);
            param.Add("p_parentAreaId", "*");
            param.Add("p_areaType", AREA_TYPE_FACTORY);
            param.Add("p_validState", "Valid");
            param.Add("p_conditionItem", "*");
            param.Add("p_conditionValue", "");

            grdAreaList.DataSource = await QueryAsync("SelectAreaList", "10001", param);
        }

        #endregion

        private void grdAreaClass_Load(object sender, EventArgs e)
        {

        }

        private void smartBandedGrid1_Load(object sender, EventArgs e)
        {

        }
    }
}
