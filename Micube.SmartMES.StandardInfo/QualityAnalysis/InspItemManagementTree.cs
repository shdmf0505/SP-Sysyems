#region using

using DevExpress.XtraEditors.Repository;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.StandardInfo.Popup;

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
using Micube.SmartMES.Commons;

using DevExpress.XtraTreeList.Columns;

using DevExpress.XtraCharts;
using DevExpress.Utils;
using Micube.Framework.SmartControls.Validations;

#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품질 기준 정보 > 검사항목관리
    /// 업  무  설  명  : 품질 기준 정보의 검사 방법, 검사 항목을 관리한다.
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-06-14
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class InspItemManagementTree : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        //private ConditionItemTextBox productDefIdBox;
        private ConditionItemComboBox comboboxINSPITEMCLASSTYPE;
        #endregion

        #region 생성자

        public InspItemManagementTree()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeEvent();
            InitializeGridInspItemclass();
            InitializeGridInspItemClassList();
            InitializeGridInspItem();
            InitializeGridIGroup();
            InitializeGridInspItemSR();
            InitializeGridIGroupSR();

            // 트리초기화
            TreeListColumn tcKey = treeInspItem.Columns.Add();
            tcKey.FieldName = "INSPITEMCLASSNAME";
            tcKey.Caption = "SORT";
            tcKey.Visible = false;
            tcKey.VisibleIndex = 1;

         

        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>

        #region grdInspItemClass 초기화
        private void InitializeGridInspItemclass()
        {
            // TODO : 그리드 초기화 로직 추가
            grdInspItemClass.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdInspItemClass.GridButtonItem -= GridButtonItem.Delete;

            grdInspItemClass.View.SetSortOrder("PARENTINSPITEMCLASSID");
            grdInspItemClass.View.SetSortOrder("INSPITEMCLASSID");

            //grdInspItemClass.View.AddComboBoxColumn("PARENTINSPITEMCLASSID", 150, new SqlQuery("GetParentInspItmeClassId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPITEMCLASSNAME", "INSPITEMCLASSID");

            grdInspItemClass.View.AddComboBoxColumn("INSPITEMCLASSTYPE", 150, new SqlQuery("GetQcInspectionDefinitionCombo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPECTIONDEFNAME", "INSPECTIONDEFID");
            grdInspItemClass.View.AddTextBoxColumn("INSPITEMCLASSID", 150)
                .SetValidationKeyColumn();

            grdInspItemClass.View.AddLanguageColumn("INSPITEMCLASSNAME", 200);

            //grdInspItemClass.View.AddTextBoxColumn("INSPITEMCLASSNAME$$KO-KR", 150).SetValidationIsRequired();
            //grdInspItemClass.View.AddTextBoxColumn("INSPITEMCLASSNAME$$EN-US", 150);
            //grdInspItemClass.View.AddTextBoxColumn("INSPITEMCLASSNAME$$ZH-CN", 150);
            //grdInspItemClass.View.AddTextBoxColumn("INSPITEMCLASSNAME$$VI-VN", 150);
            
                //.SetIsReadOnly();

            grdInspItemClass.View.AddTextBoxColumn("ENTERPRISEID", 200)
                .SetIsHidden();

            grdInspItemClass.View.AddTextBoxColumn("PLANTID", 200)
                .SetIsHidden();

            grdInspItemClass.View.AddTextBoxColumn("PARENTINSPITEMCLASSID", 200).SetIsHidden();
            //InitializeGrid_INSPITEMCLASSPopup();


            grdInspItemClass.View.AddTextBoxColumn("DESCRIPTION", 150);

            grdInspItemClass.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdInspItemClass.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdInspItemClass.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdInspItemClass.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdInspItemClass.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdInspItemClass.View.PopulateColumns();
        }


        private void InitializeGrid_INSPITEMCLASSPopup()
        {

            var parentInspItemClass = this.grdInspItemClass.View.AddSelectPopupColumn("PARENTINSPITEMCLASSID",150, new SqlQuery("SelectInspItemClass", "10001", $"P_VALIDSTATE={"Valid"}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
            .SetPopupLayout("PARENTINSPITEMCLASSID", PopupButtonStyles.Ok_Cancel, true, false)
            // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
            .SetPopupResultCount(1)
            // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
            //.SetPopupResultMapping("CODECLASSID", "CODECLASSID")
            // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
            .SetPopupResultMapping("PARENTINSPITEMCLASSID", "INSPITEMCLASSID")

            .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow);
            // 그리드의 남은 영역을 채울 컬럼 설정
            //.SetPopupAutoFillColumns("CODECLASSNAME")
            // Validation 이 필요한 경우 호출할 Method 지정

            //.SetPopupValidationCustom(ValidationAreaPopup);

            // 팝업에서 사용할 조회조건 항목 추가

            parentInspItemClass.Conditions.AddComboBox("INSPECTIONDEFID", new SqlQuery("GetQcInspectionDefinitionCombo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPECTIONDEFNAME", "INSPECTIONDEFID")
           .SetPopupDefaultByGridColumnId("INSPITEMCLASSTYPE");

            parentInspItemClass.Conditions.AddTextBox("INSPITEMCLASSID");
            parentInspItemClass.Conditions.AddTextBox("INSPITEMCLASSNAME");

            //parentProcesssegMnet.Conditions.AddTextBox("AREANAME");

            // 팝업 그리드 설정
            parentInspItemClass.GridColumns.AddComboBoxColumn("INSPITEMCLASSTYPE", 80, new SqlQuery("GetQcInspectionDefinitionCombo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPECTIONDEFNAME", "INSPECTIONDEFID");
            parentInspItemClass.GridColumns.AddTextBoxColumn("INSPITEMCLASSID", 150);
            parentInspItemClass.GridColumns.AddTextBoxColumn("INSPITEMCLASSNAME", 200);
        }

        #endregion

        #region grdInspItemClassList 초기화
        private void InitializeGridInspItemClassList()
        {
            // TODO : 그리드 초기화 로직 추가
            //grdInspItemClassList.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            //;
            //grdInspItemClassList.GridButtonItem = GridButtonItem.None;
            //grdInspItemClassList.View.SetIsReadOnly();
            //grdInspItemClassList.View.SetSortOrder("PARENTINSPITEMCLASSID");
            //grdInspItemClassList.View.SetSortOrder("INSPITEMCLASSID");
            //grdInspItemClassList.View.AddTextBoxColumn("PARENTINSPITEMCLASSID", 150)
            //    .SetIsHidden();
            //grdInspItemClassList.View.AddTextBoxColumn("PARENTINSPITEMCLASSNAME", 150);

            //grdInspItemClassList.View.AddTextBoxColumn("INSPITEMCLASSID", 150)
            //    .SetIsHidden();
            //grdInspItemClassList.View.AddTextBoxColumn("INSPITEMCLASSNAME", 150);

            //grdInspItemClassList.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();

            //grdInspItemClassList.View.PopulateColumns();
        }
        #endregion

        #region grdInspItem 초기화
        private void InitializeGridInspItem()
        {
            // TODO : 그리드 초기화 로직 추가
            grdInspItem.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdInspItem.GridButtonItem -= GridButtonItem.Delete;

            grdInspItem.View.AddTextBoxColumn("INSPITEMCLASSID")
                .SetIsHidden();

            grdInspItem.View.AddTextBoxColumn("INSPITEMID", 150)
                .SetValidationKeyColumn();

            grdInspItem.View.AddLanguageColumn("INSPITEMNAME", 200);

            grdInspItem.View.AddComboBoxColumn("INSPITEMTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspItemType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetValidationKeyColumn();
              
            grdInspItem.View.AddTextBoxColumn("DESCRIPTION", 150);

            grdInspItem.View.AddTextBoxColumn("ENTERPRISEID", 200)
                .SetIsHidden();

            grdInspItem.View.AddTextBoxColumn("PLANTID", 200)
                .SetIsHidden();

            grdInspItem.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdInspItem.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdInspItem.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdInspItem.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdInspItem.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdInspItem.View.PopulateColumns();
        }
        #endregion
      

        #region // griGroup초기화   MM_INSPITEMGROUP
        private void InitializeGridIGroup()           
        {
            grdInspItemGroup.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdInspItemGroup.GridButtonItem -= GridButtonItem.Delete;
            grdInspItemGroup.View.AddTextBoxColumn("INSPECTIONDEFID").SetIsHidden();
            grdInspItemGroup.View.AddTextBoxColumn("INSPECTIONDEFVERSION").SetIsHidden();
            grdInspItemGroup.View.AddTextBoxColumn("RESOURCETYPE").SetIsHidden();
            grdInspItemGroup.View.AddTextBoxColumn("RESOURCEVERSION").SetIsHidden();
            grdInspItemGroup.View.AddTextBoxColumn("INSPITEMGROUPTYPE").SetIsHidden();
            grdInspItemGroup.View.AddTextBoxColumn("INSPITEMGROUPID").SetIsHidden();
            grdInspItemGroup.View.AddTextBoxColumn("INSPITEMGROUPVERSION").SetIsHidden();
            grdInspItemGroup.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdInspItemGroup.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdInspItemGroup.View.AddTextBoxColumn("PARENTINSPITEMGROUPID").SetIsHidden();

            grdInspItemGroup.View.AddTextBoxColumn("RESOURCEID");
            grdInspItemGroup.View.AddLanguageColumn("RESOURCENAME", 200);  // 대분류명
            
            grdInspItemGroup.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdInspItemGroup.View.PopulateColumns();

        }


        private void InitializeGridInspItemSR()
        {
            grdInspItemSR.GridButtonItem = GridButtonItem.All;
            grdInspItemSR.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdInspItemSR.View.AddTextBoxColumn("INSPECTIONDEFID").SetIsHidden();
            grdInspItemSR.View.AddTextBoxColumn("INSPECTIONDEFVERSION").SetIsHidden();
            grdInspItemSR.View.AddTextBoxColumn("RESOURCETYPE").SetIsHidden();
            grdInspItemSR.View.AddTextBoxColumn("RESOURCEVERSION").SetIsHidden();
            grdInspItemSR.View.AddTextBoxColumn("INSPITEMGROUPTYPE").SetIsHidden();
            grdInspItemSR.View.AddTextBoxColumn("INSPITEMGROUPID").SetIsHidden();
            grdInspItemSR.View.AddTextBoxColumn("INSPITEMGROUPVERSION").SetIsHidden();
            grdInspItemSR.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdInspItemSR.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdInspItemSR.View.AddTextBoxColumn("PARENTINSPITEMGROUPID").SetIsHidden();
            grdInspItemSR.View.AddTextBoxColumn("RESOURCEID");
            grdInspItemSR.View.AddLanguageColumn("RESOURCENAME", 200);  // 대분류명
            grdInspItemSR.View.AddComboBoxColumn("INSPITEMGROUPTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspItemType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetEmptyItem("","",true);
            grdInspItemSR.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            .SetDefault("Valid")
            .SetValidationIsRequired()
            .SetTextAlignment(TextAlignment.Center);
            grdInspItemSR.View.PopulateColumns();


        }


        private void InitializeGridIGroupSR()
        {
            grdInspItemGroupSR.GridButtonItem = GridButtonItem.All;
            grdInspItemGroupSR.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdInspItemGroupSR.View.AddTextBoxColumn("INSPECTIONDEFID").SetIsHidden();
            grdInspItemGroupSR.View.AddTextBoxColumn("INSPECTIONDEFVERSION").SetIsHidden();
            grdInspItemGroupSR.View.AddTextBoxColumn("RESOURCETYPE").SetIsHidden();
            grdInspItemGroupSR.View.AddTextBoxColumn("RESOURCEVERSION").SetIsHidden();
            grdInspItemGroupSR.View.AddTextBoxColumn("INSPITEMGROUPTYPE").SetIsHidden();
            grdInspItemGroupSR.View.AddTextBoxColumn("INSPITEMGROUPID").SetIsHidden();
            grdInspItemGroupSR.View.AddTextBoxColumn("INSPITEMGROUPVERSION").SetIsHidden();
            grdInspItemGroupSR.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdInspItemGroupSR.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdInspItemGroupSR.View.AddTextBoxColumn("PARENTINSPITEMGROUPID").SetIsHidden();
            grdInspItemGroupSR.View.AddTextBoxColumn("RESOURCEID");
            grdInspItemGroupSR.View.AddTextBoxColumn("RESOURCENAMER", 200);  // 대분류명
            grdInspItemGroupSR.View.AddComboBoxColumn("INSPITEMGROUPTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspItemType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetEmptyItem("", "", true);
            grdInspItemGroupSR.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            .SetDefault("Valid")
            .SetValidationIsRequired()
            .SetTextAlignment(TextAlignment.Center);
            grdInspItemGroupSR.View.PopulateColumns();

        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            //선택된 탭이 바뀔때 이벤트
            tabInspItem1.SelectedPageChanged += TabInspItem_SelectedPageChanged;
            
            //새로운 row추가 이벤트
            grdInspItemClass.View.AddingNewRow += grdInspItemClass_AddingNewRow;
            //InspClass가 새로운 row가 아닐때 PARENTINSPITEMCLASSID 컬럼 readOnly처리
            // grdInspItemClass.View.ShowingEditor += View_ShowingEditor;
            //grdInspItemClassList의 포커스된 Row가 변경될 때 이벤트
            //grdInspItemClassList.View.FocusedRowChanged += grdInspItemClassList_FocusedRowChanged;

            grdInspItemClass.View.DoubleClick += grdInspItemClass_DoubleClick;

            treeInspItem.FocusedNodeChanged += TreeInspItem_FocusedNodeChanged;
            treeInspItemClass.FocusedNodeChanged += TreeInspItemClass_FocusedNodeChanged;
            //새로운 row추가 이벤트
            grdInspItem.View.AddingNewRow += View_AddingNewRowInspItem;
            //새로운 row추가 이벤트
            grdInspItem.ToolbarAddingRow += GrdInspItem_ToolbarAddingRow;

            grdInspItemSR.View.AddingNewRow += grdPMItemMapping_AddingNewRow1;
            grdInspItemGroupSR.View.AddingNewRow += grdChack_AddingNewRow;

            grdInspItemGroup.View.AddingNewRow += grdInspItemGroup_AddingNewRow;
            grdInspItemSR.View.AddingNewRow += grdInspItemSR_AddingNewRow;

            btnSelectItemAdd.Click += BtnSelectItemAdd_Click;
            btnSelectItemDel.Click += BtnSelectItemDel_Click;

            new SetGridDeleteButonVisible(grdInspItemClass);
            new SetGridDeleteButonVisible(grdInspItem);
            new SetGridDeleteButonVisible(grdInspItemGroup);
        }

        

        private void grdInspItemClass_DoubleClick(object sender, EventArgs e)
        {

            //원자재일경우
            //select* from sf_code
            //where CODECLASSID = 'ConsumableType'
            //원자재가공은
            //select* from sf_code
            //where CODECLASSID = 'MaterialType'

            switch (grdInspItemClass.View.FocusedColumn.FieldName)
            {
                case "INSPITEMCLASSID":
                    string CODECLASSID = "";


                    DataRow row = grdInspItemClass.View.GetFocusedDataRow();
                    //원자재
                    if (row["INSPITEMCLASSTYPE"].ToString() == "RawInspection")
                    {
                        CODECLASSID = "ConsumableType";
                        InspitemcllassPopup popup = new InspitemcllassPopup(CODECLASSID);
                        popup.ShowDialog();
                        if (popup.CurrentDataRow != null)
                        {

                            grdInspItemClass.View.SetRowCellValue(grdInspItemClass.View.FocusedRowHandle, "INSPITEMCLASSID", popup.CurrentDataRow["CODEID"]);
                            //row["INSPITEMCLASSID"] = popup.CurrentDataRow["CODEID"];
                            //row["INSPITEMCLASSNAME"] = popup.CurrentDataRow["CODENAME"];
                         

                        }
                    }
                    //원자재가공은
                    if (row["INSPITEMCLASSTYPE"].ToString() == "SubassemblyInspection")
                    {
                        CODECLASSID = "MaterialType";
                        InspitemcllassPopup popup = new InspitemcllassPopup(CODECLASSID);
                        popup.ShowDialog();
                        if (popup.CurrentDataRow != null)
                        {
                            //row["INSPITEMCLASSID"] = popup.CurrentDataRow["CODEID"];
                            grdInspItemClass.View.SetRowCellValue(grdInspItemClass.View.FocusedRowHandle, "INSPITEMCLASSID", popup.CurrentDataRow["CODEID"]);
                            //row["INSPITEMCLASSNAME"] = popup.CurrentDataRow["CODENAME"];
                           
                        }
                    }


                    break;
            }

        }

        private void TreeInspItemClass_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            SearchInspItemClass();
        }
        private void TreeInspItem_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            SearchInspItem();
        }

        private void BtnSelectItemDel_Click(object sender, EventArgs e)
        {
            DataTable dtItemSR = grdInspItemSR.GetChangedRows();

            foreach (DataRow row in dtItemSR.Rows)
            {
                row.Delete();
                row.EndEdit();
            }

        }

        private void BtnSelectItemAdd_Click(object sender, EventArgs e)
        {
            DataTable dtItemSR = grdInspItemSR.GetChangedRows();

            foreach(DataRow row in dtItemSR.Rows)
            {
                grdInspItemGroupSR.View.AddNewRow();

                DataRow rowGroup = grdInspItemGroup.View.GetFocusedDataRow();
                DataRow rowNew = grdInspItemGroupSR.View.GetFocusedDataRow();

                rowNew["INSPECTIONDEFID"] = row["INSPECTIONDEFID"];
                rowNew["INSPECTIONDEFVERSION"] = "*";
                rowNew["RESOURCETYPE"] = row["INSPECTIONDEFID"];

                if (row["INSPECTIONDEFID"].ToString() == "SubsidiaryInspection")
                {
                    rowNew["RESOURCETYPE"] = rowGroup["INSPITEMGROUPID"];
                }

                if (row["INSPECTIONDEFID"].ToString() == "RawInspection")
                {
                    rowNew["RESOURCETYPE"] = rowGroup["INSPITEMGROUPID"];
                }
                rowNew["RESOURCEID"] = row["RESOURCEID"];
                rowNew["INSPITEMGROUPID"] = rowGroup["RESOURCEID"];
                rowNew["RESOURCEVERSION"] = "*";
                rowNew["INSPITEMGROUPVERSION"] = "*";
                rowNew["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                rowNew["PLANTID"] = "";
                rowNew["PARENTINSPITEMGROUPID"] = "";
                rowNew["VALIDSTATE"] = "Valid";

            }

        }

        private void grdInspItemSR_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            var values = Conditions.GetValues();

            args.NewRow["INSPECTIONDEFID"] = values["INSPECTIONDEFID"];
            args.NewRow["INSPECTIONDEFVERSION"] = "*";

            GetNumber number = new GetNumber();

            if (values["INSPECTIONDEFID"].ToString() == "SubsidiaryInspection")
            {
                args.NewRow["RESOURCETYPE"] = "SubItem";
                args.NewRow["RESOURCEID"] = number.GetStdNumber("INSPITEMGROUP", "SubItem");
                args.NewRow["INSPITEMGROUPID"] = "SubItem";
            }
            if (values["INSPECTIONDEFID"].ToString() == "RawInspection")
            {
                args.NewRow["RESOURCETYPE"] = "RawItem";
                args.NewRow["RESOURCEID"] = number.GetStdNumber("INSPITEMGROUP", "RawItem");
                args.NewRow["INSPITEMGROUPID"] = "RawItem";
            }
            args.NewRow["RESOURCEVERSION"] = "*";
            args.NewRow["INSPITEMGROUPVERSION"] = "*";
            args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            args.NewRow["PLANTID"] = "";
            args.NewRow["PARENTINSPITEMGROUPID"] = "";
            args.NewRow["VALIDSTATE"] = "Valid";
        }

        private void grdInspItemGroup_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            var values = Conditions.GetValues();

            args.NewRow["INSPECTIONDEFID"] = values["INSPECTIONDEFID"];
            args.NewRow["INSPECTIONDEFVERSION"] = "*";

            GetNumber number = new GetNumber();

            if (values["INSPECTIONDEFID"].ToString() == "SubsidiaryInspection")
            {
                args.NewRow["RESOURCETYPE"] = "SubItem";
                args.NewRow["RESOURCEID"] = number.GetStdNumber("INSPITEMGROUP", "SubItem");
                args.NewRow["INSPITEMGROUPID"] = "SubItemGroup";
            }
            if (values["INSPECTIONDEFID"].ToString() == "RawInspection")
            {
                args.NewRow["RESOURCETYPE"] = "RawItem";
                args.NewRow["RESOURCEID"] = number.GetStdNumber("INSPITEMGROUP", "RawItem");
                args.NewRow["INSPITEMGROUPID"] = "RawItemGroup";
            }
            args.NewRow["RESOURCEVERSION"] = "*";
            args.NewRow["INSPITEMGROUPVERSION"] = "*";
            args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            args.NewRow["PLANTID"] = "";
            args.NewRow["PARENTINSPITEMGROUPID"] = "";
            args.NewRow["VALIDSTATE"] = "Valid";
        }

        private void grdPMItemMapping_AddingNewRow1(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            var values = Conditions.GetValues();
            
            args.NewRow["INSPECTIONDEFID"] = values["INSPECTIONDEFID"].ToString();
            args.NewRow["INSPECTIONDEFVERSION"] = "*";
      

            GetNumber number = new GetNumber();
            string sDICTIONARYID = number.GetStdNumber("INSPITEMGROUP", values["INSPECTIONDEFID"].ToString());
            args.NewRow["RESOURCEID"] = sDICTIONARYID;
            args.NewRow["RESOURCEVERSION"] = "*";
            if (values["INSPECTIONDEFID"].ToString() == "RawInspection")
            {
                args.NewRow["RESOURCETYPE"] = "RawItem";
                args.NewRow["INSPITEMGROUPID"] = "RawItem";
            }
            if (values["INSPECTIONDEFID"].ToString() == "SubItem")
            {
                args.NewRow["RESOURCETYPE"] = "SubItem";
                args.NewRow["INSPITEMGROUPID"] = "SubItem";
            }
         
         


        }

        private void grdChack_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            DataRow rowGroup = grdInspItemGroup.View.GetFocusedDataRow();
            DataRow rowPMItemMapping = grdInspItemSR.View.GetFocusedDataRow();
            args.NewRow["INSPITEMGROUPID"] = rowGroup["INSPITEMGROUPID"];

            args.NewRow["INSPECTIONDEFID"] = rowPMItemMapping["INSPECTIONDEFID"];
            args.NewRow["inspectiondefversion"] = "*";
            args.NewRow["RESOURCDID"] = rowPMItemMapping["RESOURCDID"];
            args.NewRow["RESOURCDID"] = rowPMItemMapping["RESOURCDID"];
        }

       
        private void BTNRIGHT_Click(object sender, EventArgs e)
        {
            DataTable dt = grdInspItemGroupSR.View.GetCheckedRows();
            foreach (DataRow row in dt.Rows)
            {
                grdInspItemSR.View.AddNewRow();
                //grdPMItemMapping.View.
            }
        }

        private void BTNLEFT_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GrdInspItem_ToolbarAddingRow(object sender, CancelEventArgs e)
        {
            //if (grdInspItemClassList.View.GetFocusedDataRow() == null)
            //{
            //    e.Cancel = true;
            //}
        }

        /// <summary>
        /// InspClass가 새로운 row가 아닐때 PARENTINSPITEMCLASSID 컬럼 readOnly처리
        /// PARENTINSPITEMCLASSID가 없는 경우가 있으므로
        /// setValidationKeyColumn 으로 처리할수 없음
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_ShowingEditor(object sender, CancelEventArgs e)
        {
            DataTable dt = grdInspItemClass.DataSource as DataTable;
            if (grdInspItemClass.View.FocusedColumn.FieldName == "PARENTINSPITEMCLASSID")
            {
                if (!grdInspItemClass.View.GetFocusedDataRow().RowState.ToString().Equals("Added"))
                {
                    this.ShowMessage("CantChangeParentInspClassId");
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// 조회조건의 검사종류가 null이 아닐때, 검사항목 탭으로 이동시 검사방법이 조회된다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabInspItem_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            SmartComboBox combobox = Conditions.GetControl<SmartComboBox>("INSPECTIONDEFID");
            combobox.DisplayMember = "INSPECTIONDEFNAME";
            combobox.ValueMember = "INSPECTIONDEFID";
            // combobox.ShowHeader = false;
            Dictionary<string, object> Param = new Dictionary<string, object>();
            DataTable dt = null;
  
            switch (tabInspItem1.SelectedTabPageIndex)
            {
                case 0:
                case 1:
                    //Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    //Param.Add("INSPECTIONTYPE", "");
                    //Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    
                    //dt = SqlExecuter.Query("GetQcInspectionDefinitionCombo", "10001", Param);
                    //combobox.DataSource = dt;

                    //combobox.ItemIndex = 0;
                    break;
                case 2:
                case 3:
                   
                    //Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    //Param.Add("INSPECTIONTYPE", "Consumable");
                    //Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    //dt = SqlExecuter.Query("GetQcInspectionDefinitionCombo", "10001", Param);
                    //combobox.DataSource = dt;

                    //combobox.ItemIndex = 0;

                    break;
            }
        }

        ///// <summary>
        ///// 선택된 InspItemClassId를 파라미터로 InspItem을 조회한다
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void grdInspItemClassList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        //{
        //    SearchInspItem();
        //}

        /// <summary>
        /// 검사방법을 등록 할 때 자동으로 ENTERPRISEID, PLANTID를 입력해 주는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void grdInspItemClass_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            args.NewRow["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;
            args.NewRow["PLANTID"] = "*";

            DataRow rowClass = treeInspItemClass.GetFocusedDataRow();

            var values = Conditions.GetValues();
            if(rowClass["INSPITEMCLASSID"].ToString() == "PARENT")
            {
                args.NewRow["INSPITEMCLASSTYPE"] = values["INSPECTIONDEFID"];
                args.NewRow["PARENTINSPITEMCLASSID"] = "";
            }
            else
            {
                args.NewRow["INSPITEMCLASSTYPE"] = rowClass["INSPITEMCLASSTYPE"];
                args.NewRow["PARENTINSPITEMCLASSID"] = rowClass["INSPITEMCLASSID"];
            }


        }

        /// <summary>
        /// 검사항목을 등록 할 때 자동으로 ENTERPRISEID, PLANTID, INSPITEMCLASSID 를 입력해 주는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRowInspItem(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            args.NewRow["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;
            args.NewRow["PLANTID"] = "*";

            DataRow row = treeInspItem.GetFocusedDataRow();
            args.NewRow["INSPITEMCLASSID"] = row["INSPITEMCLASSID"];       

        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// 선택된 탭에 따라 다른 룰 실행
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            DataTable changed = null;
            // TODO : 저장 Rule 변경
            switch (tabInspItem1.SelectedTabPageIndex)
            {
                case 0:
                    changed = grdInspItemClass.GetChangedRows();
                    ExecuteRule("SaveInspItemClass", changed);
                    break;
                case 1:
                    changed = grdInspItem.GetChangedRows();
                    ExecuteRule("SaveInspItem", changed);
                    break;
                case 2:
                    changed = grdInspItemGroup.GetChangedRows();
                    DataTable dtSR = grdInspItemSR.GetChangedRows();
                    if (dtSR != null)
                    {
                        changed.Merge(dtSR);
                    }
                    ExecuteRule("SaveInspItemGroup", changed);
                    
                    break;
            }
        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// 선택된 탭에 따라 다른 쿼리 실행
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            DataTable dt = null;
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", Framework.UserInfo.Current.Enterprise);

            switch (tabInspItem1.SelectedTabPageIndex)
            {
                case 0:

                    treeInspItemClass.SetResultCount(1);
                    treeInspItemClass.SetIsReadOnly();
                    treeInspItemClass.SetEmptyRoot(UserInfo.Current.Enterprise, "PARENT");

                    // treeRouting.SetEmptyRoot(rowFocu["ASSEMBLYITEMID"].ToString() + " " + rowFocu["ASSEMBLYITEMVERSION"].ToString(), "A");
                    treeInspItemClass.SetMember("INSPITEMCLASSNAME", "INSPITEMCLASSID", "PARENTINSPITEMCLASSID");

                    //Dictionary<string, object> param = new Dictionary<string, object>();
                    //param.Add("ENTERPRISEID", rowFocu["ENTERPRISEID"].ToString());
                    //param.Add("ASSEMBLYITEMID", rowFocu["ASSEMBLYITEMID"].ToString());
                    //param.Add("ASSEMBLYITEMVERSION", rowFocu["ASSEMBLYITEMVERSION"].ToString());
                    //param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                    dt = SqlExecuter.Query("SelectInspItemClass", "10001", values);

                    //dttreeSet = dt;

                    if (dt != null)
                    {

                        if (dt.Rows.Count != 0)
                        {
                            treeInspItemClass.DataSource = dt;
                            //treeRouting.PopulateColumns();

                            //treeRouting.ExpandAll();
                            //treeInspItemClass.ExpandToLevel(1);

                            treeInspItemClass.PopulateColumns();
                            treeInspItemClass.ExpandAll();

                        }
                    }

                    DataRow rowClass = treeInspItemClass.GetFocusedDataRow();
                    if (rowClass != null)
                    {
                        Dictionary<string, object> param = new Dictionary<string, object>();
                        param.Add("PARENTINSPITEMCLASSID", rowClass["INSPITEMCLASSID"].ToString());
                        param.Add("INSPECTIONDEFID", rowClass["INSPITEMCLASSTYPE"].ToString());
                        param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                        DataTable dtInspItem = SqlExecuter.Query("SelectInspItemClass", "10001", param);
                        grdInspItemClass.DataSource = dtInspItem;
                    }


                    //dt = await SqlExecuter.QueryAsync("SelectInspItemClass", "10001", values);
                    //grdInspItemClass.DataSource = dt;
                    break;
                case 1:

                    treeInspItem.SetResultCount(1);
                    treeInspItem.SetIsReadOnly();
                    // treeRouting.SetEmptyRoot(rowFocu["ASSEMBLYITEMID"].ToString() + " " + rowFocu["ASSEMBLYITEMVERSION"].ToString(), "A");
                    treeInspItem.SetMember("INSPITEMCLASSNAME", "INSPITEMCLASSID", "PARENTINSPITEMCLASSID");

                    //Dictionary<string, object> param = new Dictionary<string, object>();
                    //param.Add("ENTERPRISEID", rowFocu["ENTERPRISEID"].ToString());
                    //param.Add("ASSEMBLYITEMID", rowFocu["ASSEMBLYITEMID"].ToString());
                    //param.Add("ASSEMBLYITEMVERSION", rowFocu["ASSEMBLYITEMVERSION"].ToString());
                    //param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                    dt = SqlExecuter.Query("SelectInspItemClass", "10001", values);

                    //dttreeSet = dt;

                    if (dt != null)
                    {

                        if (dt.Rows.Count != 0)
                        {
                            treeInspItem.DataSource = dt;
                            //treeRouting.PopulateColumns();

                            //treeRouting.ExpandAll();
                            treeInspItem.ExpandToLevel(1);

                        }
                    }


                    //////////grdInspItem.View.ClearDatas();
                    //////////grdInspItemClassList.View.ClearDatas();
                    //////////dt = await SqlExecuter.QueryAsync("SelectInspItemClass", "10001", values);
                    //////////grdInspItemClassList.DataSource = dt;


                    DataRow row = treeInspItem.GetFocusedDataRow();
                    if (row != null)
                    {
                        values.Add("P_INSPITEMCLASSID", row["INSPITEMCLASSID"].ToString());
                        DataTable dtInspItem = SqlExecuter.Query("SelectInspItem", "10001", values);
                        grdInspItem.DataSource = dtInspItem;
                    }

                    //dt = await SqlExecuter.QueryAsync("SelectInspItemClassOnlyMiddle", "10001", values);
                    //grdInspItemClassList.DataSource = dt;
                    //SearchInspItem();
                    break;
                case 2:
                    values.Add("INSPITEMGROUPID", "'RawItemGroup', 'SubItemGroup'");
                    dt = await SqlExecuter.QueryAsync("GetInspitemGroup", "10001", values);
                    grdInspItemGroup.DataSource = dt;

                    values["INSPITEMGROUPID"] = "'SubItem','RawItem'";
                    dt = await SqlExecuter.QueryAsync("GetInspitemGroup", "10001", values);
                    grdInspItemSR.DataSource = dt;

                    values["INSPITEMGROUPID"] = "";
                    values.Add("RESOURCETYPE", "'RawItemGroup', 'SubItemGroup'");
                    dt = await SqlExecuter.QueryAsync("GetInspitemGroup", "10001", values);
                    grdInspItemGroupSR.DataSource = dt;


                    break;
                case 3:
                    

                    break;


            }
        }


        

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용

            // 검사정의
            Conditions.AddComboBox("INSPECTIONDEFID", new SqlQuery("GetQcInspectionDefinitionCombo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"),"INSPECTIONDEFNAME","INSPECTIONDEFID");

            Conditions.AddTextBox("INSPITEMCLASSID");
            Conditions.AddTextBox("INSPITEMCLASSNAME");

            InitializeCondition_Popup();

        }

        /// <summary>
        /// 검색조건 품목팝업 
        /// </summary>
        private void InitializeCondition_Popup()
        {
            //팝업 컬럼 설정
            var parentPopupColumn = Conditions.AddSelectPopup("PARENTINSPITEMCLASSID", new SqlQuery("SelectInspItemClass", "10001", $"P_VALIDSTATE={"Valid"}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPITEMCLASSNAME", "INSPITEMCLASSID")
               .SetPopupLayout("PARENTINSPITEMCLASSID", PopupButtonStyles.Ok_Cancel, true, false)  //팝업창 UI 설정.
               .SetPopupResultMapping("PARENTINSPITEMCLASSID", "INSPITEMCLASSID")
               
               .SetPopupResultCount(1);  //팝업창 선택가능한 개수


            comboboxINSPITEMCLASSTYPE = parentPopupColumn.Conditions.AddComboBox("INSPECTIONDEFID", new SqlQuery("GetQcInspectionDefinitionCombo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPECTIONDEFNAME", "INSPECTIONDEFID");
            

            parentPopupColumn.Conditions.AddTextBox("INSPITEMCLASSID");
            parentPopupColumn.Conditions.AddTextBox("INSPITEMCLASSNAME");

            //parentProcesssegMnet.Conditions.AddTextBox("AREANAME");

            // 팝업 그리드 설정
            parentPopupColumn.GridColumns.AddComboBoxColumn("INSPITEMCLASSTYPE", 80, new SqlQuery("GetQcInspectionDefinitionCombo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPECTIONDEFNAME", "INSPECTIONDEFID");
            parentPopupColumn.GridColumns.AddTextBoxColumn("INSPITEMCLASSID", 150);
            parentPopupColumn.GridColumns.AddTextBoxColumn("INSPITEMCLASSNAME", 200);

        }

        /// <summary>
        /// 최초에 첫번째 탭에 조회조건 바인딩
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();


            SmartComboBox comboboxINSPECTIONDEFID = Conditions.GetControl<SmartComboBox>("INSPECTIONDEFID");
            comboboxINSPECTIONDEFID.ItemIndex = 1;
            comboboxINSPITEMCLASSTYPE.SetDefault(comboboxINSPECTIONDEFID.EditValue);

            comboboxINSPECTIONDEFID.TextChanged += ComboboxINSPECTIONDEFID_TextChanged;


            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용     
        }

        private void ComboboxINSPECTIONDEFID_TextChanged(object sender, EventArgs e)
        {
            SmartComboBox combox = (SmartComboBox)sender;
            comboboxINSPITEMCLASSTYPE.SetDefault(combox.EditValue);
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// 선택된 탭에 따라 다른 그리드 유효성
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            DataTable changed = null;

            switch (tabInspItem1.SelectedTabPageIndex)
            {
                case 0:
                    grdInspItemClass.View.CheckValidation();
                    changed = grdInspItemClass.GetChangedRows();

                    foreach(DataRow row in changed.Rows)
                    {

                        Dictionary<string, object> Param = new Dictionary<string, object>();
                        Param.Add("P_INSPITEMCLASSID", row["INSPITEMCLASSID"].ToString());

                        DataTable dtInspItem = SqlExecuter.Query("SelectInspItem", "10001", Param);
                        if(dtInspItem != null)
                        {
                            if(dtInspItem.Rows.Count !=0)
                            {
                                throw MessageException.Create("IngState");
                            }
                        }
                        
                    }

                    break;
                case 1:
                    grdInspItem.View.CheckValidation();
                    changed = grdInspItem.GetChangedRows();

                    foreach (DataRow row in changed.Rows)
                    {
                        Dictionary<string, object> Param = new Dictionary<string, object>();
                        Param.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());
                        Param.Add("INSPITEMID", row["INSPITEMID"].ToString());

                        DataTable dtInspItem = SqlExecuter.Query("GetInspectionitemRelChk", "10001", Param);
                        if (dtInspItem != null)
                        {
                            if (dtInspItem.Rows.Count != 0)
                            {
                                throw MessageException.Create("IngState");
                            }
                        }

                    }

                    break;

                case 2:
                    grdInspItemGroup.View.CheckValidation();
                    changed = grdInspItemGroup.GetChangedRows();
                    break;

            }

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 바인딩 할 데이터 테이블에 데이터있는지 확인하는 함수
        /// </summary>
        /// <param name="table"></param>
        private void CheckHasData(DataTable table)
        {
            if (table.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
        }

        /// <summary>
        /// inspItem을 조회하는 함수
        /// </summary>
        private void SearchInspItem()
        {
            DataRow row = treeInspItem.GetFocusedDataRow();
            if(row != null)
            { 
                var values = Conditions.GetValues();
                values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
                values.Add("P_INSPITEMCLASSID", row["INSPITEMCLASSID"].ToString());

                DataTable dt = SqlExecuter.Query("SelectInspItem", "10001", values);
                grdInspItem.DataSource = dt;
            }
        }


        /// <summary>
        /// inspItemClass을 조회하는 함수
        /// </summary>
        private void SearchInspItemClass()
        {
            //DataRow row = treeInspItemClass.GetFocusedDataRow();
            //if (row != null)
            //{
            //    var values = Conditions.GetValues();
            //    values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            //    values.Add("P_INSPITEMCLASSID", row["INSPITEMCLASSID"].ToString());

            //    DataTable dt = SqlExecuter.Query("SelectInspItem", "10001", values);
            //    grdInspItemClass.DataSource = dt;
            //}

            

            var values = Conditions.GetValues();
            DataRow rowClass = treeInspItemClass.GetFocusedDataRow();
            if (rowClass != null)
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("PARENTINSPITEMCLASSID", rowClass["INSPITEMCLASSID"].ToString());

                if (rowClass["INSPITEMCLASSID"].ToString() == "PARENT")
                {
                    param.Add("INSPECTIONDEFID", values["INSPECTIONDEFID"].ToString());
                    
                }
                else
                {
                    param.Add("INSPECTIONDEFID", rowClass["INSPITEMCLASSTYPE"].ToString());
                }
                    

                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                DataTable dtInspItem = SqlExecuter.Query("SelectInspItemClass", "10001", param);
                grdInspItemClass.DataSource = dtInspItem;
            }

        }

        #endregion

        private void grdPMItemNotMapping_Load(object sender, EventArgs e)
        {

        }
    }
}
#endregion
