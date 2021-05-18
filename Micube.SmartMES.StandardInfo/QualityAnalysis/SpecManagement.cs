#region using
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using Micube.SmartMES.StandardInfo.Popup;
using Micube.SmartMES.Commons;
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
#endregion

namespace Micube.SmartMES.StandardInfo
{
    public partial class SpecManagement : SmartConditionManualBaseForm
    {
        /// <summary>
        /// 프 로 그 램 명  : 기준정보 > 품질 기준 정보 > SPEC 등록
        /// 업  무  설  명  : 품질관리를 위한 SPEC을 관리한다.
        /// 생    성    자  : 강유라
        /// 생    성    일  : 2019-05-14
        /// 수  정  이  력  : 
        /// 
        /// 
        /// </summary>

        #region Local Variables

        SmartBandedGrid _grid;//탭체인지 이벤트 발생시 Select된 탭의 그리드
        string _gridName; //탭체인지 이벤트 발생시 Select된 탭의 그리드이름
        string _inspectionName;//팝업으로 넘겨줄 검사명 (다국어 처리)
        string _specClassId;///팝업으로 넘겨줄 SpecClassId
        string _inspectiontype;//콤보박스의 relation
        private DataRow _relRow = null;//검사기준정보 화면에서 넘겨 준 row
        string _inspectionClassType = "";//검사기준정보 화면에서 넘겨 준 inspectionClassType
        int _tabIndex = 0;
        string _fromMenu = "";//검사기준정보 화면에서 넘겨 준 fromMenu
        private SmartButton _button = null;
        #endregion

        #region 생성자

        public SpecManagement()
        {
            InitializeComponent();
            InitializeVariable();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            InitializeSpecClassGrid();

        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        /// 

        #region //grdSpecclass 초기화
        private void InitializeSpecClassGrid()
        {
            grdSpecClass.View.SetIsReadOnly();

            grdSpecClass.GridButtonItem = GridButtonItem.None;

            grdSpecClass.View.SetSortOrder("SPECCLASSID");

            //grdSpecClass.View.AddTextBoxColumn("ENTERPRISEID", 200);

            //grdSpecClass.View.AddTextBoxColumn("PLANTID", 150);

            grdSpecClass.View.AddTextBoxColumn("SPECCLASSID", 150);

            grdSpecClass.View.AddLanguageColumn("SPECCLASSNAME", 200);

            grdSpecClass.View.AddTextBoxColumn("INSPECTIONDEFID", 200);

            grdSpecClass.View.AddTextBoxColumn("DESCRIPTION", 200);


            grdSpecClass.View.AddTextBoxColumn("VALIDSTATE", 80);

            grdSpecClass.View.AddTextBoxColumn("CREATOR", 80)
                        .SetIsReadOnly()
                        .SetTextAlignment(TextAlignment.Center);

            grdSpecClass.View.AddTextBoxColumn("CREATEDTIME", 130)
                        .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                        .SetIsReadOnly()
                        .SetTextAlignment(TextAlignment.Center);

            grdSpecClass.View.AddTextBoxColumn("MODIFIER", 80)
                        .SetIsReadOnly()
                        .SetTextAlignment(TextAlignment.Center);

            grdSpecClass.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                        .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                        .SetIsReadOnly()
                        .SetTextAlignment(TextAlignment.Center);

            grdSpecClass.View.PopulateColumns();
        }
        #endregion

        #region //grdChemical 초기화
        private void InitializeGridChemical()
        {
            grdChemicalSpec.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdChemicalSpec.GridButtonItem -= GridButtonItem.Delete;

            grdChemicalSpec.View.SetSortOrder("SPECSEQUENCE");

            grdChemicalSpec.View.AddTextBoxColumn("SPECSEQUENCE", 150)
                .SetIsHidden()
                .SetIsReadOnly();

            grdChemicalSpec.View.AddTextBoxColumn("INSPECTIONTYPE", 150)
                .SetIsHidden();

            grdChemicalSpec.View.AddTextBoxColumn("SPECCLASSID", 150)
                .SetIsReadOnly();

            grdChemicalSpec.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();

            grdChemicalSpec.View.AddTextBoxColumn("PLANTID", 150)
                .SetDefault("*")
                .SetIsReadOnly()
                .SetLabel("SITE");

            InitializeGrdChemical_ProcessSegmentClassPopup();

            grdChemicalSpec.View.AddTextBoxColumn("PROCESSSEGMENTID", 200)
                .SetIsHidden();

            InitializeGrdChemical_EquipmentListPopup();

            InitializeGrdChemical_ChileEquipmentListPopup();

            grdChemicalSpec.View.AddTextBoxColumn("PRODUCTDEFID", 200)
                .SetIsHidden();

            grdChemicalSpec.View.AddTextBoxColumn("CONSUMABLEDEFID", 200)
                .SetIsHidden();

            grdChemicalSpec.View.AddTextBoxColumn("CUSTOMERID", 80)
                .SetIsHidden();

            grdChemicalSpec.View.AddTextBoxColumn("VENDORID", 150)
                .SetIsHidden();

            InitializeGrdChemical_InspItemIdPopup();

            //code 등록 후 codeClassId 파라미터 변경 필
            grdChemicalSpec.View.AddTextBoxColumn("WORKTYPE", 100)
                .SetIsHidden();

            //code 등록 후 codeClassId 파라미터 변경 필
            grdChemicalSpec.View.AddTextBoxColumn("WORKCONDITION", 100)
                .SetIsHidden();

            grdChemicalSpec.View.AddTextBoxColumn("RESOURCEID", 150);

            //SPECVERSION - 서버에서 자동으로 0 입력
            grdChemicalSpec.View.AddTextBoxColumn("SPECVERSION", 200)
                .SetIsReadOnly();

            grdChemicalSpec.View.AddComboBoxColumn("UOMDEFID", 100, new SqlQuery("GetUOMList", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "UOMDEFID", "UOMDEFNAME")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdChemicalSpec.View.AddComboBoxColumn("DEFAULTCHARTTYPE", 100, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ControlType", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"))//
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdChemicalSpec.View.AddTextBoxColumn("CONTROLRANGE", 200)
                .SetIsReadOnly();

            //code 등록 후 codeClassId 파라미터 변경 필
            grdChemicalSpec.View.AddComboBoxColumn("ANALYSISTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"))
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdChemicalSpec.View.AddTextBoxColumn("DESCRIPTION", 200);

            grdChemicalSpec.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdChemicalSpec.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdChemicalSpec.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdChemicalSpec.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdChemicalSpec.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdChemicalSpec.View.PopulateColumns();
        }
        /// <summary>
        /// 대공정 선택하는 팝업
        /// </summary>
        private void InitializeGrdChemical_ProcessSegmentClassPopup()
        {
            //팝업 컬럼 설정
            var ProcessSegmentClassId = grdChemicalSpec.View.AddSelectPopupColumn("PROCESSSEGMENTCLASSID", new SqlQuery("GetProcessSegmentClass", "10001", "PROCESSSEGMENTCLASSTYPE=TopProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                                       .SetPopupLayout("PROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, false)
                                                       .SetPopupResultCount(1)
                                                       .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                                                       .SetPopupAutoFillColumns("PROCESSSEGMENTCLASSNAME")
                                                       .SetRelationIds("PLANTID")
                                                       .SetValidationKeyColumn()
                                                       .SetPopupQueryPopup((DataRow currentrow) =>
                                                       {
                                                           if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("PLANTID")))
                                                           {
                                                               this.ShowMessage("NoSelectSite");
                                                               return false;
                                                           }

                                                           return true;
                                                       });

            ProcessSegmentClassId.Conditions.AddTextBox("PROCESSSEGMENTCLASS")
                .SetLabel("TOPPROCESSSEGMENTCLASS");

            ProcessSegmentClassId.Conditions.AddTextBox("PLANTID")
                                          .SetPopupDefaultByGridColumnId("PLANTID")
                                          .SetIsHidden();

            // 팝업 그리드
            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 150);
            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200);

        }

        /// <summary>
        /// 설비를 선택하는 팝업
        /// </summary>
        private void InitializeGrdChemical_EquipmentListPopup()
        {
            //팝업 컬럼 설정
            var equipmentId = grdChemicalSpec.View.AddSelectPopupColumn("EQUIPMENTID", new SqlQuery("GetEquipmentByClassHierarchy", "10001", "DETAILEQUIPMENTTYPE=Main", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                             .SetPopupLayout("EQUIPMENT", PopupButtonStyles.Ok_Cancel, true, false)
                                             .SetPopupResultCount(1)
                                             .SetValidationKeyColumn()
                                             .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                                             .SetPopupAutoFillColumns("EQUIPMENTNAME");

            equipmentId.Conditions.AddComboBox("PARENTEQUIPMENTCLASSID", new SqlQuery("GetEquipmentClassHierarchyAndType", "10001", "HIERARCHY=TopEquipment", "EQUIPMENTCLASSTYPE=Production", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTCLASSNAME", "EQUIPMENTCLASSID")
                                  .SetValidationKeyColumn()
                                  .SetLabel("TOPEQUIPMENTCLASS");

            equipmentId.Conditions.AddComboBox("MIDDLEEQUIPMENTCLASSID", new SqlQuery("GetEquipmentClassHierarchyAndType", "10001", "HIERARCHY=MiddleEquipment", "EQUIPMENTCLASSTYPE=Production", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTCLASSNAME", "EQUIPMENTCLASSID")
                                  .SetValidationKeyColumn()
                                  .SetRelationIds("PARENTEQUIPMENTCLASSID")
                                  .SetLabel("MIDDLEEQUIPMENTCLASS");

            equipmentId.Conditions.AddTextBox("EQUIPMENT");

            // 팝업 그리드
            equipmentId.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSNAME", 150);
            equipmentId.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150);
            equipmentId.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200);

        }

        /// <summary>
        /// 설비단을 선택하는 팝업
        /// </summary>
        private void InitializeGrdChemical_ChileEquipmentListPopup()
        {
            //팝업 컬럼 설정
            var childEquipementId = grdChemicalSpec.View.AddSelectPopupColumn("CHILDEQUIPMENTID", new SqlQuery("GetEquipmentListByDetailType", "10001", "EQUIPMENTCLASSTYPE=Production", "DETAILEQUIPMENTTYPE=Sub", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                                   .SetPopupLayout("EQUIPMENT", PopupButtonStyles.Ok_Cancel, true, false)
                                                   .SetPopupResultCount(1)
                                                   .SetPopupResultMapping("CHILDEQUIPMENTID", "EQUIPMENTID")
                                                   .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                                   .SetPopupAutoFillColumns("EQUIPMENTNAME")
                                                   .SetLabel("CHILDEQUIPMENTID")
                                                   .SetRelationIds("PARENTEQUIPMENTID")
                                                   .SetValidationKeyColumn()
                                                   .SetPopupQueryPopup((DataRow currentrow) =>
                                                   {
                                                       if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("EQUIPMENTID")))
                                                       {
                                                           this.ShowMessage("NoSelectParentEquipment");
                                                           return false;
                                                       }

                                                       return true;
                                                   });

            childEquipementId.Conditions.AddTextBox("EQUIPMENT");

            childEquipementId.Conditions.AddTextBox("PARENTEQUIPMENTID")
                                        .SetPopupDefaultByGridColumnId("EQUIPMENTID")
                                        .SetIsHidden();
            // 팝업 그리드
            childEquipementId.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150);
            childEquipementId.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200);

        }

        /// <summary>
        /// 설비단을 선택하는 팝업
        /// </summary>
        //테이블 생성 후 수정 필
        private void InitializeGrdChemical_InspItemIdPopup()
        {
            //팝업 컬럼 설정
            var inspItemId = grdChemicalSpec.View.AddSelectPopupColumn("INSPITEMID", new SqlQuery("GetInspItemId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                            .SetPopupLayout("INSPITEM", PopupButtonStyles.Ok_Cancel, true, false)
                                            .SetPopupResultCount(1)
                                            .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                            .SetValidationKeyColumn();

            inspItemId.Conditions.AddTextBox("INSPITEMID");
            inspItemId.Conditions.AddTextBox("INSPITEMNAME");

            // 팝업 그리드
            inspItemId.GridColumns.AddTextBoxColumn("WORKTYPE", 150)
                                  .SetLabel("PARENTINSPITEMLCLASSID");
            inspItemId.GridColumns.AddTextBoxColumn("PARENTINSPITEMLCLASSNAME", 150);
            inspItemId.GridColumns.AddTextBoxColumn("WORKCONDITION", 150)
                                  .SetLabel("INSPITEMMCLASSID");
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMMCLASSNAME", 150);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMID", 150);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMNAME", 200);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMVERSION", 200);
        }

        #endregion

        #region //grdWater 초기화
        private void InitializeGridWater()
        {
            grdWaterSpec.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdWaterSpec.GridButtonItem -= GridButtonItem.Delete;

            grdWaterSpec.View.SetSortOrder("SPECSEQUENCE");

            grdWaterSpec.View.AddTextBoxColumn("SPECSEQUENCE", 150)
                .SetIsHidden()
                .SetIsReadOnly();

            grdWaterSpec.View.AddTextBoxColumn("INSPECTIONTYPE", 150)
                .SetIsHidden();

            grdWaterSpec.View.AddTextBoxColumn("SPECCLASSID", 150)
                .SetIsReadOnly();

            grdWaterSpec.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();

            grdWaterSpec.View.AddTextBoxColumn("PLANTID", 150)
                .SetDefault("*")
                .SetIsReadOnly()
                .SetLabel("SITE");

            InitializeGrdWater_ProcessSegmentClassPopup();

            grdWaterSpec.View.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetIsHidden();

            InitializeWater_EquipmentListPopup();

            InitializeWater_ChileEquipmentListPopup();

            grdWaterSpec.View.AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsHidden();

            grdWaterSpec.View.AddTextBoxColumn("CONSUMABLEDEFID", 150)
                .SetIsHidden();

            grdWaterSpec.View.AddTextBoxColumn("CUSTOMERID", 80)
                .SetIsHidden();

            grdWaterSpec.View.AddTextBoxColumn("VENDORID", 150)
                .SetIsHidden();

            InitializeWater_InspItemIdPopup();

            //code 등록 후 codeClassId 파라미터 변경 필
            grdWaterSpec.View.AddTextBoxColumn("WORKTYPE", 200)
                .SetIsHidden();

            //code 등록 후 codeClassId 파라미터 변경 필
            grdWaterSpec.View.AddTextBoxColumn("WORKCONDITION", 200)
                .SetIsHidden();

            grdWaterSpec.View.AddTextBoxColumn("RESOURCEID", 150);

            //SPECVERSION - 서버에서 자동으로 0 입력
            grdWaterSpec.View.AddTextBoxColumn("SPECVERSION", 200)
                .SetIsReadOnly();

            grdWaterSpec.View.AddComboBoxColumn("UOMDEFID", 100, new SqlQuery("GetUOMList", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "UOMDEFID", "UOMDEFNAME")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdWaterSpec.View.AddComboBoxColumn("DEFAULTCHARTTYPE", 100, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ControlType", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"))
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdWaterSpec.View.AddTextBoxColumn("CONTROLRANGE", 200)
                .SetIsReadOnly();

            //code 등록 후 codeClassId 파라미터 변경 필
            grdWaterSpec.View.AddComboBoxColumn("ANALYSISTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"))
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdWaterSpec.View.AddTextBoxColumn("DESCRIPTION", 200);

            grdWaterSpec.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdWaterSpec.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdWaterSpec.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdWaterSpec.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdWaterSpec.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdWaterSpec.View.PopulateColumns();
        }
        /// <summary>
        /// 대공정을 선택하는 팝업
        /// </summary>
        private void InitializeGrdWater_ProcessSegmentClassPopup()
        {
            //팝업 컬럼 설정
            var ProcessSegmentClassId = grdWaterSpec.View.AddSelectPopupColumn("PROCESSSEGMENTCLASSID", new SqlQuery("GetProcessSegmentClass", "10001", "PROCESSSEGMENTCLASSTYPE=TopProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                                    .SetPopupLayout("PROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, false)
                                                    .SetPopupResultCount(1)
                                                    .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                                                    .SetPopupAutoFillColumns("PROCESSSEGMENTCLASSNAME")
                                                    .SetRelationIds("PLANTID")
                                                    .SetValidationKeyColumn()
                                                    .SetPopupQueryPopup((DataRow currentrow) =>
                                                    {
                                                        if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("PLANTID")))
                                                        {
                                                            this.ShowMessage("NoSelectSite");
                                                            return false;
                                                        }

                                                        return true;
                                                    });

            ProcessSegmentClassId.Conditions.AddTextBox("PROCESSSEGMENTCLASS")
                .SetLabel("TOPPROCESSSEGMENTCLASS");

            ProcessSegmentClassId.Conditions.AddTextBox("PLANTID")
                                            .SetPopupDefaultByGridColumnId("PLANTID")
                                            .SetIsHidden();

            // 팝업 그리드
            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 150);
            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200);
        }

        /// <summary>
        /// 설비를 선택하는 팝업
        /// </summary>
        private void InitializeWater_EquipmentListPopup()
        {
            //팝업 컬럼 설정
            var equipmentId = grdWaterSpec.View.AddSelectPopupColumn("EQUIPMENTID", new SqlQuery("GetEquipmentByClassHierarchy", "10001", "DETAILEQUIPMENTTYPE=Main", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                             .SetPopupLayout("EQUIPMENT", PopupButtonStyles.Ok_Cancel, true, false)
                                             .SetPopupResultCount(1)
                                             .SetValidationKeyColumn()
                                             .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                                             .SetPopupAutoFillColumns("EQUIPMENTNAME");

            equipmentId.Conditions.AddComboBox("PARENTEQUIPMENTCLASSID", new SqlQuery("GetEquipmentClassHierarchyAndType", "10001", "HIERARCHY=TopEquipment", "EQUIPMENTCLASSTYPE=Production", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTCLASSNAME", "EQUIPMENTCLASSID")
                                  .SetValidationKeyColumn()
                                  .SetLabel("TOPEQUIPMENTCLASS");

            equipmentId.Conditions.AddComboBox("MIDDLEEQUIPMENTCLASSID", new SqlQuery("GetEquipmentClassHierarchyAndType", "10001", "HIERARCHY=MiddleEquipment", "EQUIPMENTCLASSTYPE=Production", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTCLASSNAME", "EQUIPMENTCLASSID")
                                  .SetValidationKeyColumn()
                                  .SetRelationIds("PARENTEQUIPMENTCLASSID")
                                  .SetLabel("MIDDLEEQUIPMENTCLASS");

            equipmentId.Conditions.AddTextBox("EQUIPMENT");

            // 팝업 그리드
            equipmentId.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSNAME", 150);
            equipmentId.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150);
            equipmentId.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200);

        }
        /// <summary>
        /// 설비단을 선택하는 팝업
        /// </summary>
        private void InitializeWater_ChileEquipmentListPopup()
        {
            //팝업 컬럼 설정
            var childEquipementId = grdWaterSpec.View.AddSelectPopupColumn("CHILDEQUIPMENTID", new SqlQuery("GetEquipmentListByDetailType", "10001", "EQUIPMENTCLASSTYPE=Production", "DETAILEQUIPMENTTYPE=Sub", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                                .SetPopupLayout("EQUIPMENT", PopupButtonStyles.Ok_Cancel, true, false)
                                                .SetPopupResultCount(1)
                                                .SetPopupResultMapping("CHILDEQUIPMENTID", "EQUIPMENTID")
                                                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                                .SetPopupAutoFillColumns("EQUIPMENTNAME")
                                                .SetLabel("CHILDEQUIPMENTID")
                                                .SetRelationIds("PARENTEQUIPMENTID")
                                                .SetValidationKeyColumn()
                                                .SetPopupQueryPopup((DataRow currentrow) =>
                                                {
                                                    if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("EQUIPMENTID")))
                                                    {
                                                        this.ShowMessage("NoSelectParentEquipment");
                                                        return false;
                                                    }

                                                    return true;
                                                });



            childEquipementId.Conditions.AddTextBox("EQUIPMENT");

            childEquipementId.Conditions.AddTextBox("PARENTEQUIPMENTID")
                                        .SetPopupDefaultByGridColumnId("EQUIPMENTID")
                                        .SetIsHidden();

            // 팝업 그리드
            childEquipementId.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150);
            childEquipementId.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200);

        }

        //테이블 생성 후 수정 필
        private void InitializeWater_InspItemIdPopup()
        {
            //팝업 컬럼 설정
            var inspItemId = grdWaterSpec.View.AddSelectPopupColumn("INSPITEMID", new SqlQuery("GetInspItemId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                            .SetPopupLayout("INSPITEM", PopupButtonStyles.Ok_Cancel, true, false)
                                            .SetPopupResultCount(1)
                                            .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                            .SetValidationKeyColumn();

            inspItemId.Conditions.AddTextBox("INSPITEMID");
            inspItemId.Conditions.AddTextBox("INSPITEMNAME");

            // 팝업 그리드
            inspItemId.GridColumns.AddTextBoxColumn("WORKTYPE", 150)
                                  .SetLabel("PARENTINSPITEMLCLASSID");
            inspItemId.GridColumns.AddTextBoxColumn("PARENTINSPITEMLCLASSNAME", 150);
            inspItemId.GridColumns.AddTextBoxColumn("WORKCONDITION", 150)
                                  .SetLabel("INSPITEMMCLASSID");
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMMCLASSNAME", 150);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMID", 150);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMNAME", 200);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMVERSION", 200);
        }


        #endregion

        #region //grdOSP 초기화
        private void InitializeGridOSP()
        {
            grdOSPSpec.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdOSPSpec.GridButtonItem -= GridButtonItem.Delete;

            grdOSPSpec.View.SetSortOrder("SPECSEQUENCE");

            grdOSPSpec.View.AddTextBoxColumn("SPECSEQUENCE", 150)
                .SetIsHidden()
                .SetIsReadOnly();

            grdOSPSpec.View.AddTextBoxColumn("SPECCLASSID", 150)
                .SetIsReadOnly();

            grdOSPSpec.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();

            grdOSPSpec.View.AddTextBoxColumn("PLANTID", 150)
                .SetDefault("*")
                .SetIsReadOnly()
                .SetLabel("SITE");

            InitializeOSP_ProcessSegmentClassPopup();

            grdOSPSpec.View.AddTextBoxColumn("PROCESSSEGMENTID", 200)
                .SetIsHidden();

            grdOSPSpec.View.AddTextBoxColumn("EQUIPMENTID", 200)
                      .SetIsHidden();

            grdOSPSpec.View.AddTextBoxColumn("CHILDEQUIPMENTID", 200)
                .SetIsHidden();

            InitializeOSP_ProductDefIdPopup();

            grdOSPSpec.View.AddTextBoxColumn("CONSUMABLEDEFID", 200)
                .SetIsHidden();

            grdOSPSpec.View.AddComboBoxColumn("CUSTOMERID", 150, new SqlQuery("GetCustomerList", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "CUSTOMERNAME", "CUSTOMERID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                .SetValidationKeyColumn();

            grdOSPSpec.View.AddTextBoxColumn("VENDORID", 200)
                .SetIsHidden();

            //code 등록 후 codeClassId 파라미터 변경 필
            grdOSPSpec.View.AddTextBoxColumn("WORKTYPE", 100)
                .SetIsHidden();

            //inspItemclass : InitializeOSP_InspItemIdPopup 팝업 선택으로 자동 입력 될때
            //grdOSPSpec.View.AddTextBoxColumn("WORKCONDITION", 100)
            //    .SetIsReadOnly()
            //    .SetValidationKeyColumn();

            grdOSPSpec.View.AddTextBoxColumn("INSPECTIONTYPE", 200)
                .SetDefault("OSPInspection", "OSPInspection")
                .SetIsHidden();

            //inspItemclass : 콤보로 선택 후 InitializeOSP_InspItemIdPopup에 relation 걸릴 때
            grdOSPSpec.View.AddComboBoxColumn("WORKCONDITION", 150, new SqlQuery("GetInspItemClass", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}", "TYPE=ParentClass"), "INSPITEMCLASSNAME", "INSPITEMCLASSID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                .SetValidationKeyColumn()
                .SetRelationIds("INSPECTIONTYPE");

            InitializeOSP_InspItemIdPopup();

            grdOSPSpec.View.AddTextBoxColumn("RESOURCEID", 150);

            //SPECVERSION - 서버에서 자동으로 0 입력
            grdOSPSpec.View.AddTextBoxColumn("SPECVERSION", 200)
                .SetIsReadOnly();

            grdOSPSpec.View.AddComboBoxColumn("UOMDEFID", 100, new SqlQuery("GetUOMList", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "UOMDEFID", "UOMDEFNAME")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdOSPSpec.View.AddComboBoxColumn("DEFAULTCHARTTYPE", 100, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ControlType", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"))
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdOSPSpec.View.AddTextBoxColumn("CONTROLRANGE", 200)
                .SetIsReadOnly();

            //code 등록 후 codeClassId 파라미터 변경 필
            grdOSPSpec.View.AddComboBoxColumn("ANALYSISTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"))
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdOSPSpec.View.AddTextBoxColumn("DESCRIPTION", 200);

            grdOSPSpec.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdOSPSpec.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdOSPSpec.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdOSPSpec.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdOSPSpec.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdOSPSpec.View.PopulateColumns();
        }


        /// <summary>
        /// 중공정 선택하는 팝업
        /// </summary>
        private void InitializeOSP_ProcessSegmentClassPopup()
        {
            //팝업 컬럼 설정

            var ProcessSegmentClassId = grdOSPSpec.View.AddSelectPopupColumn("PROCESSSEGMENTCLASSID", new SqlQuery("GetProcessSegmentClass", "10001", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                                  .SetPopupLayout("PROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, false)
                                                  .SetPopupResultCount(1)
                                                  .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                                                  .SetRelationIds("PLANTID")
                                                  .SetValidationKeyColumn()
                                                  .SetPopupQueryPopup((DataRow currentrow) =>
                                                  {
                                                      if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("PLANTID")))
                                                      {
                                                          this.ShowMessage("NoSelectSite");
                                                          return false;
                                                      }

                                                      return true;
                                                  });

            ProcessSegmentClassId.Conditions.AddTextBox("PROCESSSEGMENTCLASS")
                .SetLabel("MIDDLEPROCESSSEGMENTCLASS");

            ProcessSegmentClassId.Conditions.AddTextBox("PLANTID")
                                            .SetPopupDefaultByGridColumnId("PLANTID")
                                            .SetIsHidden();

            // 팝업 그리드
            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PARENTPROCESSSEGMENTCLASSID", 150);
            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PARENTPROCESSSEGMENTCLASSNAME", 200);
            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 150);
            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200);

        }
        /// <summary>
        /// 품목을 선택하는 팝업
        /// </summary>
        private void InitializeOSP_ProductDefIdPopup()
        {
            //팝업 컬럼 설정

            var productDefId = grdOSPSpec.View.AddSelectPopupColumn("PRODUCTDEFID", new SqlQuery("GetProductDefList", "10001"/*, "PRODUCTDEFTYPE=Product"*/, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                              .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, false)
                                              .SetPopupResultCount(1)
                                              .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                              .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                                              .SetRelationIds("PLANTID")
                                              .SetLabel("PRODUCTDEFID")
                                              .SetValidationKeyColumn()
                                              .SetPopupQueryPopup((DataRow currentrow) =>
                                              {
                                                  if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("PLANTID")))
                                                  {
                                                      this.ShowMessage("NoSelectSite");
                                                      return false;
                                                  }

                                                  return true;
                                              });

            productDefId.Conditions.AddTextBox("PRODUCTDEF");

            productDefId.Conditions.AddTextBox("PLANTID")
                                   .SetPopupDefaultByGridColumnId("PLANTID")
                                   .SetIsHidden();

            // 팝업 그리드
            productDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            productDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);

        }

        //테이블 생성 후 수정 필
        /// <summary>
        /// 검사항목을 조회하는 팝업
        /// </summary>
        private void InitializeOSP_InspItemIdPopup()
        {
            //팝업 컬럼 설정
            var inspItemId = grdOSPSpec.View.AddSelectPopupColumn("INSPITEMID", new SqlQuery("GetInspItemId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                            .SetPopupLayout("INSPITEM", PopupButtonStyles.Ok_Cancel, true, false)
                                            .SetPopupResultCount(1)
                                            .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                            .SetRelationIds("INSPITEMCLASSID")
                                            .SetValidationKeyColumn()
                                            .SetPopupQueryPopup((DataRow currentrow) =>
                                            {
                                                if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("WORKCONDITION")))
                                                {
                                                    this.ShowMessage("NoSelectInspItemclassId");
                                                    return false;
                                                }

                                                return true;
                                            });

            inspItemId.Conditions.AddTextBox("INSPITEMID");
            inspItemId.Conditions.AddTextBox("INSPITEMNAME");

            inspItemId.Conditions.AddTextBox("INSPITEMCLASSID")
                .SetPopupDefaultByGridColumnId("WORKCONDITION")
                .SetIsHidden();

            // 팝업 그리드
            //inspItemId.GridColumns.AddTextBoxColumn("WORKTYPE", 150)
            //                      .SetLabel("PARENTINSPITEMLCLASSID");
            //inspItemId.GridColumns.AddTextBoxColumn("PARENTINSPITEMLCLASSNAME", 150);

            inspItemId.GridColumns.AddTextBoxColumn("WORKCONDITION", 150)
                                  .SetLabel("INSPITEMMCLASSID");
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMMCLASSNAME", 150);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMID", 150);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMNAME", 200);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMVERSION", 200);

        }

        // 테이블 생성 후 수정 필
        /// <summary>
        /// WORKCONDITION을 조회하는 팝업 
        /// INSPITEMCLASS의 inspitemclassid
        /// </summary>
        private void InitializeOSP_WorkCondition()
        {
            //팝업 컬럼 설정

            var inspItemId = grdOSPSpec.View.AddSelectPopupColumn("WORKCONDITION", new SqlQuery("GetInspItemClass", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "TYPE=Class"))
                                       .SetPopupLayout("PROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, false)
                                       .SetPopupResultCount(1)
                                       .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                       .SetPopupAutoFillColumns("PROCESSSEGMENTNAME")
                                       .SetPopupResultMapping("INSPITEMID", "PROCESSSEGMENTID")
                                       .SetValidationKeyColumn();

            inspItemId.Conditions.AddTextBox("CHEMICAL");

            // 팝업 그리드
            inspItemId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            inspItemId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

        }
        #endregion

        #region //grdRaw 초기화
        private void InitializeGridRaw()
        {
            grdRawSpec.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdRawSpec.GridButtonItem -= GridButtonItem.Delete;

            grdRawSpec.View.SetSortOrder("SPECSEQUENCE");

            grdRawSpec.View.AddTextBoxColumn("SPECSEQUENCE", 150)
                .SetIsHidden()
                .SetIsReadOnly();

            grdRawSpec.View.AddTextBoxColumn("SPECCLASSID", 150)
                .SetIsReadOnly();

            grdRawSpec.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();

            grdRawSpec.View.AddComboBoxColumn("PLANTID", 150, new SqlQuery("GetPlantList", "10001", $"ENTERPRISEID={Framework.UserInfo.Current.Enterprise}", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                .SetEmptyItem("*", "*", true)
                .SetValidationKeyColumn()
                .SetLabel("SITE");

            grdRawSpec.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 200)
                .SetIsHidden();

            grdRawSpec.View.AddTextBoxColumn("PROCESSSEGMENTID", 200)
                .SetIsHidden();

            grdRawSpec.View.AddTextBoxColumn("EQUIPMENTID", 200)
                .SetIsHidden();

            grdRawSpec.View.AddTextBoxColumn("CHILDEQUIPMENTID", 200)
                .SetIsHidden();

            grdRawSpec.View.AddTextBoxColumn("PRODUCTDEFID", 200)
                .SetIsHidden();

            InitializeRaw_ConsumableDefIdPopup();

            grdRawSpec.View.AddTextBoxColumn("CUSTOMERID", 80)
                .SetIsHidden();

            grdRawSpec.View.AddComboBoxColumn("VENDORID", 150, new SqlQuery("GetVendorList", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "VENDORNAME", "VENDORID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                .SetEmptyItem("*", "*", true)
                .SetValidationKeyColumn();

            //InitializeRaw_InspItemIdPopup 팝업 선택으로 자동 입력 될때
            //grdRawSpec.View.AddTextBoxColumn("WORKTYPE", 150)
            //    .SetValidationKeyColumn()
            //    .SetIsReadOnly();


            //grdRawSpec.View.AddTextBoxColumn("WORKCONDITION", 150)
            //    .SetValidationKeyColumn()
            //    .SetIsReadOnly();

            grdRawSpec.View.AddTextBoxColumn("INSPECTIONTYPE", 200)
                .SetDefault("RawInspection", "RawInspection")
                .SetIsHidden();

            //inspItemclass : 콤보로 선택 후 InitializeRaw_InspItemIdPopup relation 걸릴 때
            grdRawSpec.View.AddComboBoxColumn("WORKTYPE", 150, new SqlQuery("GetInspItemClass", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}", "TYPE=ParentClass"), "INSPITEMCLASSNAME", "INSPITEMCLASSID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                .SetValidationKeyColumn()
                .SetRelationIds("INSPECTIONTYPE");

            grdRawSpec.View.AddComboBoxColumn("WORKCONDITION", 150, new SqlQuery("GetInspItemClass", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}", "TYPE=Class"), "INSPITEMCLASSNAME", "INSPITEMCLASSID")
              .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
              .SetRelationIds("WORKTYPE")
              .SetValidationKeyColumn();

            InitializeRaw_InspItemIdPopup();

            grdRawSpec.View.AddTextBoxColumn("RESOURCEID", 150);

            //SPECVERSION - 서버에서 자동으로 0 입력
            grdRawSpec.View.AddTextBoxColumn("SPECVERSION", 200)
                .SetIsReadOnly();

            grdRawSpec.View.AddComboBoxColumn("UOMDEFID", 100, new SqlQuery("GetUOMList", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "UOMDEFID", "UOMDEFNAME")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdRawSpec.View.AddComboBoxColumn("DEFAULTCHARTTYPE", 100, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ControlType", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"))
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdRawSpec.View.AddTextBoxColumn("CONTROLRANGE", 200)
                .SetIsReadOnly();

            //code 등록 후 codeClassId 파라미터 변경 필
            grdRawSpec.View.AddComboBoxColumn("ANALYSISTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"))
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdRawSpec.View.AddTextBoxColumn("DESCRIPTION", 200);

            grdRawSpec.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdRawSpec.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdRawSpec.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdRawSpec.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdRawSpec.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdRawSpec.View.PopulateColumns();
        }
        /// <summary>
        /// 소모성자재를 선택하는 팝업
        /// </summary>
        private void InitializeRaw_ConsumableDefIdPopup()
        {
            //팝업 컬럼 설정
            var consumableDefId = grdRawSpec.View.AddSelectPopupColumn("CONSUMABLEDEFID", new SqlQuery("GetConsumableDefList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                            .SetPopupLayout("CONSUMABLE", PopupButtonStyles.Ok_Cancel, true, false)
                                            .SetPopupResultCount(1)
                                            .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                            .SetPopupAutoFillColumns("CONSUMABLEDEFNAME")
                                            .SetPopupResultMapping("CONSUMABLEDEFID", "CONSUMABLEDEFID")
                                            .SetRelationIds("PLANTID")
                                            .SetValidationKeyColumn()
                                            .SetPopupQueryPopup((DataRow currentrow) =>
                                            {
                                                if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("PLANTID")))
                                                {
                                                    this.ShowMessage("NoSelectSite");
                                                    return false;
                                                }

                                                return true;
                                            });

            consumableDefId.Conditions.AddTextBox("CONSUMABLEDEF");

            consumableDefId.Conditions.AddTextBox("PLANTID")
                                      .SetPopupDefaultByGridColumnId("PLANTID")
                                      .SetIsHidden();

            // 팝업 그리드
            consumableDefId.GridColumns.AddTextBoxColumn("CONSUMABLEDEFID", 150);
            consumableDefId.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);

        }
        //테이블 생성 후 수정 필
        /// <summary>
        /// 검사항목을 조회하는 팝업
        /// </summary>
        private void InitializeRaw_InspItemIdPopup()
        {
            //팝업 컬럼 설정
            var inspItemId = grdRawSpec.View.AddSelectPopupColumn("INSPITEMID", new SqlQuery("GetInspItemId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                            .SetPopupLayout("INSPITEM", PopupButtonStyles.Ok_Cancel, true, false)
                                            .SetPopupResultCount(1)
                                            .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                            .SetValidationKeyColumn()
                                            .SetRelationIds("WORKCONDITION")
                                            //.SetPopupApplySelection((selectedRows, dataGridRow) =>
                                            //{
                                            //    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                                            //    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                                            //    foreach (DataRow row in selectedRows)
                                            //    {
                                            //        dataGridRow["WORKTYPE"] = row["WORKTYPE"].ToString();
                                            //        dataGridRow["WORKCONDITION"] = row["WORKCONDITION"].ToString();
                                            //    }
                                            //});
                                            .SetPopupQueryPopup((DataRow currentrow) =>
                                            {
                                                if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("WORKCONDITION")))
                                                {
                                                    this.ShowMessage("NoSelectInspItemclassId");
                                                    return false;
                                                }

                                                return true;
                                            });

            inspItemId.Conditions.AddTextBox("INSPITEMID");
            inspItemId.Conditions.AddTextBox("INSPITEMNAME");

            inspItemId.Conditions.AddTextBox("INSPITEMCLASSID")
                .SetPopupDefaultByGridColumnId("WORKCONDITION")
                .SetIsHidden();

            // 팝업 그리드
            inspItemId.GridColumns.AddTextBoxColumn("WORKTYPE", 150)
                                  .SetLabel("PARENTINSPITEMLCLASSID");
            inspItemId.GridColumns.AddTextBoxColumn("PARENTINSPITEMLCLASSNAME", 150);
            inspItemId.GridColumns.AddTextBoxColumn("WORKCONDITION", 150)
                                  .SetLabel("INSPITEMMCLASSID");
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMMCLASSNAME", 150);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMID", 150);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMNAME", 200);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMVERSION", 200);

        }

        // 테이블 생성 후 수정 필
        /// <summary>
        /// WORKTYPE을 조회하는 팝업 
        /// INSPITEMCLASS의 parentinspitemclassid
        /// </summary>
        private void InitializeRaw_WorkType()
        {
            //팝업 컬럼 설정

            var inspItemId = grdRawSpec.View.AddSelectPopupColumn("WORKTYPE", new SqlQuery("GetInspItemClass", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "TYPE=ParentClass"))
                                       .SetPopupLayout("INSPITEMCLASS", PopupButtonStyles.Ok_Cancel, true, false)
                                       .SetPopupResultCount(1)
                                       .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                       .SetPopupAutoFillColumns("INSPITEMCLASSNAME")
                                       .SetPopupResultMapping("WORKTYPE", "INSPITEMCLASSID")
                                       .SetValidationKeyColumn();

            inspItemId.Conditions.AddTextBox("INSPITEMCLASS");

            // 팝업 그리드
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMCLASSID", 150);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMCLASSNAME", 200);

        }


        // 테이블 생성 후 수정 필
        /// <summary>
        /// WORKCONDITION을 조회하는 팝업 
        /// INSPITEMCLASS의 inspitemclassid
        /// </summary>
        private void InitializeRaw_WorkCondition()
        {
            //팝업 컬럼 설정

            var inspItemId = grdRawSpec.View.AddSelectPopupColumn("WORKCONDITION", new SqlQuery("GetInspItemClass", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "TYPE=Class"))
                                       .SetPopupLayout("INSPITEMCLASS", PopupButtonStyles.Ok_Cancel, true, false)
                                       .SetPopupResultCount(1)
                                       .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                       .SetPopupAutoFillColumns("INSPITEMCLASSNAME")
                                       .SetPopupResultMapping("WORKCONDITION", "INSPITEMCLASSID")
                                       .SetValidationKeyColumn();

            inspItemId.Conditions.AddTextBox("INSPITEMCLASS");

            // 팝업 그리드
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMCLASSID", 150);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMCLASSNAME", 200);

        }

        #endregion

        #region //grdSubassembly(원자재 가공품) 초기화
        private void InitializeGridSubassembly()
        {
            grdSubassemblySpec.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdSubassemblySpec.GridButtonItem -= GridButtonItem.Delete;

            grdSubassemblySpec.View.SetSortOrder("SPECSEQUENCE");

            grdSubassemblySpec.View.AddTextBoxColumn("SPECSEQUENCE", 150)
                .SetIsHidden()
                .SetIsReadOnly();

            grdSubassemblySpec.View.AddTextBoxColumn("SPECCLASSID", 150)
                .SetIsReadOnly();

            grdSubassemblySpec.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();

            grdSubassemblySpec.View.AddComboBoxColumn("PLANTID", 150, new SqlQuery("GetPlantList", "10001", $"ENTERPRISEID={Framework.UserInfo.Current.Enterprise}", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                .SetEmptyItem("*", "*", true)
                .SetValidationKeyColumn()
                .SetLabel("SITE");

            grdSubassemblySpec.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 200)
                .SetIsHidden();

            grdSubassemblySpec.View.AddTextBoxColumn("PROCESSSEGMENTID", 200)
                .SetIsHidden();

            grdSubassemblySpec.View.AddTextBoxColumn("EQUIPMENTID", 200)
                .SetIsHidden();

            grdSubassemblySpec.View.AddTextBoxColumn("CHILDEQUIPMENTID", 200)
                .SetIsHidden();

            grdSubassemblySpec.View.AddTextBoxColumn("PRODUCTDEFID", 200)
                .SetIsHidden();

            //*************InitializeSubassembly_ConsumableDefIdPopup();
            InitializeSubassembly_ProductDefIdPopup();

            grdSubassemblySpec.View.AddTextBoxColumn("CUSTOMERID", 80)
                .SetIsHidden();

            grdSubassemblySpec.View.AddComboBoxColumn("VENDORID", 150, new SqlQuery("GetVendorList", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "VENDORNAME", "VENDORID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                .SetEmptyItem("*", "*", true)
                .SetValidationKeyColumn();

            //InitializeRaw_InspItemIdPopup 팝업 선택으로 자동 입력 될때
            //grdSubassemblySpec.View.AddTextBoxColumn("WORKTYPE", 150)
            //    .SetValidationKeyColumn()
            //    .SetIsReadOnly();


            //grdSubassemblySpec.View.AddTextBoxColumn("WORKCONDITION", 150)
            //    .SetValidationKeyColumn()
            //    .SetIsReadOnly();

            grdSubassemblySpec.View.AddTextBoxColumn("INSPECTIONTYPE", 200)
                .SetDefault("SubassemblyInspection", "SubassemblyInspection")
                .SetIsHidden();

            grdSubassemblySpec.View.AddTextBoxColumn("WORKTYPE", 100)
                .SetIsHidden();

            //inspItemclass : 콤보로 선택 후 InitializeSubassembly_InspItemIdPopup relation 걸릴 때
            grdSubassemblySpec.View.AddComboBoxColumn("WORKCONDITION", 150, new SqlQuery("GetInspItemClass", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}", "TYPE=ParentClass"), "INSPITEMCLASSNAME", "INSPITEMCLASSID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                .SetValidationKeyColumn()
                .SetRelationIds("INSPECTIONTYPE");

            InitializeSubassembly_InspItemIdPopup();

            grdSubassemblySpec.View.AddTextBoxColumn("RESOURCEID", 150);

            //SPECVERSION - 서버에서 자동으로 0 입력
            grdSubassemblySpec.View.AddTextBoxColumn("SPECVERSION", 200)
                .SetIsReadOnly();

            grdSubassemblySpec.View.AddComboBoxColumn("UOMDEFID", 100, new SqlQuery("GetUOMList", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "UOMDEFID", "UOMDEFNAME")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdSubassemblySpec.View.AddComboBoxColumn("DEFAULTCHARTTYPE", 100, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ControlType", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"))
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdSubassemblySpec.View.AddTextBoxColumn("CONTROLRANGE", 200)
                .SetIsReadOnly();

            //code 등록 후 codeClassId 파라미터 변경 필
            grdSubassemblySpec.View.AddComboBoxColumn("ANALYSISTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"))
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdSubassemblySpec.View.AddTextBoxColumn("DESCRIPTION", 200);

            grdSubassemblySpec.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdSubassemblySpec.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSubassemblySpec.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSubassemblySpec.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSubassemblySpec.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSubassemblySpec.View.PopulateColumns();
        }

        /// <summary>
        /// 품목(반제품)을 선택하는 팝업
        /// 20191004 윤성원 PRODUCTDEFTYPE=SemiProduct 에서 PRODUCTDEFTYPE=SubAssembly 로 변경
        /// </summary>
        private void InitializeSubassembly_ProductDefIdPopup()
        {
            //팝업 컬럼 설정
            var productDefId = grdSubassemblySpec.View.AddSelectPopupColumn("CONSUMABLEDEFID", new SqlQuery("GetProductDefList", "10001", "PRODUCTDEFTYPE=SubAssembly", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                              .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, false)
                                              .SetPopupResultCount(1)
                                              .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                              .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                                              .SetPopupResultMapping("CONSUMABLEDEFID", "PRODUCTDEFID")
                                              .SetRelationIds("PLANTID")
                                              .SetLabel("SEMIPRODUCTCODE")
                                              .SetValidationKeyColumn()
                                              .SetPopupQueryPopup((DataRow currentrow) =>
                                              {
                                                  if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("PLANTID")))
                                                  {
                                                      this.ShowMessage("NoSelectSite");
                                                      return false;
                                                  }

                                                  return true;
                                              });

            productDefId.Conditions.AddTextBox("PRODUCTDEF");

            productDefId.Conditions.AddTextBox("PLANTID")
                                   .SetPopupDefaultByGridColumnId("PLANTID")
                                   .SetIsHidden();
            // 팝업 그리드
            productDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            productDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);

        }

        /// <summary>
        /// 소모성자재를 선택하는 팝업
        /// </summary>
        private void InitializeSubassembly_ConsumableDefIdPopup()
        {
            //팝업 컬럼 설정
            var consumableDefId = grdSubassemblySpec.View.AddSelectPopupColumn("CONSUMABLEDEFID", new SqlQuery("GetConsumableDefList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                            .SetPopupLayout("CONSUMABLE", PopupButtonStyles.Ok_Cancel, true, false)
                                            .SetPopupResultCount(1)
                                            .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                            .SetPopupAutoFillColumns("CONSUMABLEDEFNAME")
                                            .SetPopupResultMapping("CONSUMABLEDEFID", "CONSUMABLEDEFID")
                                            .SetRelationIds("PLANTID")
                                            .SetValidationKeyColumn()
                                            .SetPopupQueryPopup((DataRow currentrow) =>
                                            {
                                                if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("PLANTID")))
                                                {
                                                    this.ShowMessage("NoSelectSite");
                                                    return false;
                                                }

                                                return true;
                                            });

            consumableDefId.Conditions.AddTextBox("CONSUMABLEDEF");

            consumableDefId.Conditions.AddTextBox("PLANTID")
                                      .SetPopupDefaultByGridColumnId("PLANTID")
                                      .SetIsHidden();

            // 팝업 그리드
            consumableDefId.GridColumns.AddTextBoxColumn("CONSUMABLEDEFID", 150);
            consumableDefId.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);

        }
        //테이블 생성 후 수정 필
        /// <summary>
        /// 검사항목을 조회하는 팝업
        /// </summary>
        private void InitializeSubassembly_InspItemIdPopup()
        {
            //팝업 컬럼 설정
            var inspItemId = grdSubassemblySpec.View.AddSelectPopupColumn("INSPITEMID", new SqlQuery("GetInspItemId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                            .SetPopupLayout("INSPITEM", PopupButtonStyles.Ok_Cancel, true, false)
                                            .SetPopupResultCount(1)
                                            .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                            .SetValidationKeyColumn()
                                            .SetRelationIds("WORKCONDITION")
                                            //.SetPopupApplySelection((selectedRows, dataGridRow) =>
                                            //{
                                            //    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                                            //    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                                            //    foreach (DataRow row in selectedRows)
                                            //    {
                                            //        dataGridRow["WORKTYPE"] = row["WORKTYPE"].ToString();
                                            //        dataGridRow["WORKCONDITION"] = row["WORKCONDITION"].ToString();
                                            //    }
                                            //});
                                            .SetPopupQueryPopup((DataRow currentrow) =>
                                            {
                                                if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("WORKCONDITION")))
                                                {
                                                    this.ShowMessage("NoSelectInspItemclassId");
                                                    return false;
                                                }

                                                return true;
                                            });

            inspItemId.Conditions.AddTextBox("INSPITEMID");
            inspItemId.Conditions.AddTextBox("INSPITEMNAME");

            inspItemId.Conditions.AddTextBox("INSPITEMCLASSID")
                .SetPopupDefaultByGridColumnId("WORKCONDITION")
                .SetIsHidden();

            // 팝업 그리드
            inspItemId.GridColumns.AddTextBoxColumn("WORKTYPE", 150)
                                  .SetLabel("PARENTINSPITEMLCLASSID");
            inspItemId.GridColumns.AddTextBoxColumn("PARENTINSPITEMLCLASSNAME", 150);
            inspItemId.GridColumns.AddTextBoxColumn("WORKCONDITION", 150)
                                  .SetLabel("INSPITEMMCLASSID");
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMMCLASSNAME", 150);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMID", 150);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMNAME", 200);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMVERSION", 200);

        }

        // 테이블 생성 후 수정 필
        /// <summary>
        /// WORKTYPE을 조회하는 팝업 
        /// INSPITEMCLASS의 parentinspitemclassid
        /// </summary>
        private void InitializeSubassembly_WorkType()
        {
            //팝업 컬럼 설정

            var inspItemId = grdSubassemblySpec.View.AddSelectPopupColumn("WORKTYPE", new SqlQuery("GetInspItemClass", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "TYPE=ParentClass"))
                                       .SetPopupLayout("INSPITEMCLASS", PopupButtonStyles.Ok_Cancel, true, false)
                                       .SetPopupResultCount(1)
                                       .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                       .SetPopupAutoFillColumns("INSPITEMCLASSNAME")
                                       .SetPopupResultMapping("WORKTYPE", "INSPITEMCLASSID")
                                       .SetValidationKeyColumn();

            inspItemId.Conditions.AddTextBox("INSPITEMCLASS");

            // 팝업 그리드
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMCLASSID", 150);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMCLASSNAME", 200);

        }


        // 테이블 생성 후 수정 필
        /// <summary>
        /// WORKCONDITION을 조회하는 팝업 
        /// INSPITEMCLASS의 inspitemclassid
        /// </summary>
        private void InitializeSubassembly_WorkCondition()
        {
            //팝업 컬럼 설정

            var inspItemId = grdSubassemblySpec.View.AddSelectPopupColumn("WORKCONDITION", new SqlQuery("GetInspItemClass", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "TYPE=Class"))
                                       .SetPopupLayout("INSPITEMCLASS", PopupButtonStyles.Ok_Cancel, true, false)
                                       .SetPopupResultCount(1)
                                       .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                       .SetPopupAutoFillColumns("INSPITEMCLASSNAME")
                                       .SetPopupResultMapping("WORKCONDITION", "INSPITEMCLASSID")
                                       .SetValidationKeyColumn();

            inspItemId.Conditions.AddTextBox("INSPITEMCLASS");

            // 팝업 그리드
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMCLASSID", 150);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMCLASSNAME", 200);

        }

        #endregion

        #region //grdSubsidiary(부자재/포장재) 초기화
        private void InitializeGridSubsidiary()
        {
            grdSubsidiarySpec.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdSubsidiarySpec.GridButtonItem -= GridButtonItem.Delete;

            grdSubsidiarySpec.View.SetSortOrder("SPECSEQUENCE");

            grdSubsidiarySpec.View.AddTextBoxColumn("SPECSEQUENCE", 150)
                .SetIsHidden()
                .SetIsReadOnly();

            grdSubsidiarySpec.View.AddTextBoxColumn("SPECCLASSID", 150)
                .SetIsReadOnly();

            grdSubsidiarySpec.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();

            grdSubsidiarySpec.View.AddComboBoxColumn("PLANTID", 150, new SqlQuery("GetPlantList", "10001", $"ENTERPRISEID={Framework.UserInfo.Current.Enterprise}", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                .SetEmptyItem("*", "*", true)
                .SetValidationKeyColumn()
                .SetLabel("SITE");

            grdSubsidiarySpec.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 200)
                .SetIsHidden();

            grdSubsidiarySpec.View.AddTextBoxColumn("PROCESSSEGMENTID", 200)
                .SetIsHidden();

            grdSubsidiarySpec.View.AddTextBoxColumn("EQUIPMENTID", 200)
                .SetIsHidden();

            grdSubsidiarySpec.View.AddTextBoxColumn("CHILDEQUIPMENTID", 200)
                .SetIsHidden();

            grdSubsidiarySpec.View.AddTextBoxColumn("PRODUCTDEFID", 200)
                .SetIsHidden();

            InitializeSubsidiary_ConsumableDefIdPopup();

            grdSubsidiarySpec.View.AddTextBoxColumn("CUSTOMERID", 80)
                .SetIsHidden();

            grdSubsidiarySpec.View.AddComboBoxColumn("VENDORID", 150, new SqlQuery("GetVendorList", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "VENDORNAME", "VENDORID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                .SetEmptyItem("*", "*", true)
                .SetValidationKeyColumn();

            //InitializeRaw_InspItemIdPopup 팝업 선택으로 자동 입력 될때
            //grdSubsidiarySpec.View.AddTextBoxColumn("WORKTYPE", 150)
            //    .SetValidationKeyColumn()
            //    .SetIsReadOnly();


            //grdSubsidiarySpec.View.AddTextBoxColumn("WORKCONDITION", 150)
            //    .SetValidationKeyColumn()
            //    .SetIsReadOnly();

            grdSubsidiarySpec.View.AddTextBoxColumn("INSPECTIONTYPE", 200)
                .SetDefault("SubsidiaryInspection", "SubsidiaryInspection")
                .SetIsHidden();

            grdSubsidiarySpec.View.AddTextBoxColumn("WORKTYPE", 100)
                .SetIsHidden();

            //inspItemclass : 콤보로 선택 후 InitializeSubsidiary_InspItemIdPopup relation 걸릴 때
            grdSubsidiarySpec.View.AddComboBoxColumn("WORKCONDITION", 150, new SqlQuery("GetInspItemClass", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}", "TYPE=ParentClass"), "INSPITEMCLASSNAME", "INSPITEMCLASSID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                .SetValidationKeyColumn()
                .SetRelationIds("INSPECTIONTYPE");

            InitializeSubsidiary_InspItemIdPopup();

            grdSubsidiarySpec.View.AddTextBoxColumn("RESOURCEID", 150);

            //SPECVERSION - 서버에서 자동으로 0 입력
            grdSubsidiarySpec.View.AddTextBoxColumn("SPECVERSION", 200)
                .SetIsReadOnly();

            grdSubsidiarySpec.View.AddComboBoxColumn("UOMDEFID", 100, new SqlQuery("GetUOMList", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "UOMDEFID", "UOMDEFNAME")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdSubsidiarySpec.View.AddComboBoxColumn("DEFAULTCHARTTYPE", 100, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ControlType", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"))
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdSubsidiarySpec.View.AddTextBoxColumn("CONTROLRANGE", 200)
                .SetIsReadOnly();

            //code 등록 후 codeClassId 파라미터 변경 필
            grdSubsidiarySpec.View.AddComboBoxColumn("ANALYSISTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"))
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdSubsidiarySpec.View.AddTextBoxColumn("DESCRIPTION", 200);

            grdSubsidiarySpec.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdSubsidiarySpec.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSubsidiarySpec.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSubsidiarySpec.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSubsidiarySpec.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSubsidiarySpec.View.PopulateColumns();
        }
        /// <summary>
        /// 소모성자재를 선택하는 팝업
        /// </summary>
        private void InitializeSubsidiary_ConsumableDefIdPopup()
        {
            //팝업 컬럼 설정
            var consumableDefId = grdSubsidiarySpec.View.AddSelectPopupColumn("CONSUMABLEDEFID", new SqlQuery("GetConsumableDefList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                            .SetPopupLayout("CONSUMABLE", PopupButtonStyles.Ok_Cancel, true, false)
                                            .SetPopupResultCount(1)
                                            .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                            .SetPopupAutoFillColumns("CONSUMABLEDEFNAME")
                                            .SetPopupResultMapping("CONSUMABLEDEFID", "CONSUMABLEDEFID")
                                            .SetRelationIds("PLANTID")
                                            .SetValidationKeyColumn()
                                            .SetPopupQueryPopup((DataRow currentrow) =>
                                            {
                                                if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("PLANTID")))
                                                {
                                                    this.ShowMessage("NoSelectSite");
                                                    return false;
                                                }

                                                return true;
                                            });

            consumableDefId.Conditions.AddTextBox("CONSUMABLEDEF");

            consumableDefId.Conditions.AddTextBox("PLANTID")
                                      .SetPopupDefaultByGridColumnId("PLANTID")
                                      .SetIsHidden();

            // 팝업 그리드
            consumableDefId.GridColumns.AddTextBoxColumn("CONSUMABLEDEFID", 150);
            consumableDefId.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);

        }
        //테이블 생성 후 수정 필
        /// <summary>
        /// 검사항목을 조회하는 팝업
        /// </summary>
        private void InitializeSubsidiary_InspItemIdPopup()
        {
            //팝업 컬럼 설정
            var inspItemId = grdSubsidiarySpec.View.AddSelectPopupColumn("INSPITEMID", new SqlQuery("GetInspItemId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                            .SetPopupLayout("INSPITEM", PopupButtonStyles.Ok_Cancel, true, false)
                                            .SetPopupResultCount(1)
                                            .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                            .SetValidationKeyColumn()
                                            .SetRelationIds("WORKCONDITION")
                                            //.SetPopupApplySelection((selectedRows, dataGridRow) =>
                                            //{
                                            //    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                                            //    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                                            //    foreach (DataRow row in selectedRows)
                                            //    {
                                            //        dataGridRow["WORKTYPE"] = row["WORKTYPE"].ToString();
                                            //        dataGridRow["WORKCONDITION"] = row["WORKCONDITION"].ToString();
                                            //    }
                                            //});
                                            .SetPopupQueryPopup((DataRow currentrow) =>
                                            {
                                                if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("WORKCONDITION")))
                                                {
                                                    this.ShowMessage("NoSelectInspItemclassId");
                                                    return false;
                                                }

                                                return true;
                                            });

            inspItemId.Conditions.AddTextBox("INSPITEMID");
            inspItemId.Conditions.AddTextBox("INSPITEMNAME");

            inspItemId.Conditions.AddTextBox("INSPITEMCLASSID")
                .SetPopupDefaultByGridColumnId("WORKCONDITION")
                .SetIsHidden();

            // 팝업 그리드
            inspItemId.GridColumns.AddTextBoxColumn("WORKTYPE", 150)
                                  .SetLabel("PARENTINSPITEMLCLASSID");
            inspItemId.GridColumns.AddTextBoxColumn("PARENTINSPITEMLCLASSNAME", 150);
            inspItemId.GridColumns.AddTextBoxColumn("WORKCONDITION", 150)
                                  .SetLabel("INSPITEMMCLASSID");
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMMCLASSNAME", 150);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMID", 150);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMNAME", 200);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMVERSION", 200);

        }

        // 테이블 생성 후 수정 필
        /// <summary>
        /// WORKTYPE을 조회하는 팝업 
        /// INSPITEMCLASS의 parentinspitemclassid
        /// </summary>
        private void InitializeSubsidiary_WorkType()
        {
            //팝업 컬럼 설정

            var inspItemId = grdSubsidiarySpec.View.AddSelectPopupColumn("WORKTYPE", new SqlQuery("GetInspItemClass", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "TYPE=ParentClass"))
                                       .SetPopupLayout("INSPITEMCLASS", PopupButtonStyles.Ok_Cancel, true, false)
                                       .SetPopupResultCount(1)
                                       .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                       .SetPopupAutoFillColumns("INSPITEMCLASSNAME")
                                       .SetPopupResultMapping("WORKTYPE", "INSPITEMCLASSID")
                                       .SetValidationKeyColumn();

            inspItemId.Conditions.AddTextBox("INSPITEMCLASS");

            // 팝업 그리드
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMCLASSID", 150);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMCLASSNAME", 200);

        }


        // 테이블 생성 후 수정 필
        /// <summary>
        /// WORKCONDITION을 조회하는 팝업 
        /// INSPITEMCLASS의 inspitemclassid
        /// </summary>
        private void InitializeSubsidiary_WorkCondition()
        {
            //팝업 컬럼 설정

            var inspItemId = grdSubsidiarySpec.View.AddSelectPopupColumn("WORKCONDITION", new SqlQuery("GetInspItemClass", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "TYPE=Class"))
                                       .SetPopupLayout("INSPITEMCLASS", PopupButtonStyles.Ok_Cancel, true, false)
                                       .SetPopupResultCount(1)
                                       .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                       .SetPopupAutoFillColumns("INSPITEMCLASSNAME")
                                       .SetPopupResultMapping("WORKCONDITION", "INSPITEMCLASSID")
                                       .SetValidationKeyColumn();

            inspItemId.Conditions.AddTextBox("INSPITEMCLASS");

            // 팝업 그리드
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMCLASSID", 150);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMCLASSNAME", 200);

        }

        #endregion

        #region //grdOperation 초기화
        private void InitializeGridOperation()
        {
            grdOperationSpec.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdOperationSpec.GridButtonItem = GridButtonItem.None;

            grdOperationSpec.View.SetIsReadOnly();

            grdOperationSpec.View.SetSortOrder("SPECSEQUENCE");

            grdOperationSpec.View.AddTextBoxColumn("SPECSEQUENCE", 150)
                .SetIsHidden()
                .SetIsReadOnly();

            grdOperationSpec.View.AddTextBoxColumn("SPECCLASSID", 150)
                .SetIsReadOnly();

            grdOperationSpec.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();

            grdOperationSpec.View.AddComboBoxColumn("PLANTID", 150, new SqlQuery("GetPlantList", "10001", $"ENTERPRISEID={Framework.UserInfo.Current.Enterprise}", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                .SetDefault(Framework.UserInfo.Current.Plant)
                .SetLabel("SITE")
                .SetValidationKeyColumn();

            grdOperationSpec.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 200)
                .SetIsHidden();

            InitializeOperation_ProcessSegmentIdPopup();

            grdOperationSpec.View.AddTextBoxColumn("EQUIPMENTIID", 200)
                .SetIsHidden();

            grdOperationSpec.View.AddTextBoxColumn("CHILDEQUIPMENTID", 200)
                .SetIsHidden();

            InitializeOperation_ProductDefIdPopup();

            grdOperationSpec.View.AddTextBoxColumn("CONSUMABLEDEFID", 200)
                .SetIsHidden();

            grdOperationSpec.View.AddTextBoxColumn("CUSTOMERID", 80)
                .SetIsHidden();

            grdOperationSpec.View.AddTextBoxColumn("VENDORID", 200)
                .SetIsHidden();

            InitializeOperation_InspItemIdPopup();

            //code 등록 후 codeClassId 파라미터 변경 필
            grdOperationSpec.View.AddTextBoxColumn("WORKTYPE", 200)
                .SetIsHidden();

            //code 등록 후 codeClassId 파라미터 변경 필
            grdOperationSpec.View.AddTextBoxColumn("WORKCONDITION", 200)
                .SetIsHidden();

            grdOperationSpec.View.AddTextBoxColumn("RESOURCEID", 150);

            //SPECVERSION - 서버에서 자동으로 0 입력
            grdOperationSpec.View.AddTextBoxColumn("SPECVERSION", 200)
                .SetIsReadOnly();

            grdOperationSpec.View.AddComboBoxColumn("UOMDEFID", 100, new SqlQuery("GetUOMList", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "UOMDEFID", "UOMDEFNAME")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdOperationSpec.View.AddComboBoxColumn("DEFAULTCHARTTYPE", 100, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ControlType", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"))
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdOperationSpec.View.AddTextBoxColumn("CONTROLRANGE", 200)
                .SetIsReadOnly();

            //code 등록 후 codeClassId 파라미터 변경 필
            grdOperationSpec.View.AddComboBoxColumn("ANALYSISTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"))
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdOperationSpec.View.AddTextBoxColumn("DESCRIPTION", 200);

            grdOperationSpec.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdOperationSpec.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdOperationSpec.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdOperationSpec.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdOperationSpec.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdOperationSpec.View.PopulateColumns();
        }
        /// <summary>
        /// 표준공정 선택하는 팝업
        /// </summary>
        private void InitializeOperation_ProcessSegmentIdPopup()
        {
            //팝업 컬럼 설정

            var processSegmentId = grdOperationSpec.View.AddSelectPopupColumn("PROCESSSEGMENTID", new SqlQuery("GetProcessSegmentList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                                   .SetPopupLayout("PROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, false)
                                                   .SetPopupResultCount(1)
                                                   .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                                   .SetPopupAutoFillColumns("PROCESSSEGMENTNAME")
                                                   .SetRelationIds("PLANTID")
                                                   .SetValidationKeyColumn()
                                                   .SetPopupQueryPopup((DataRow currentrow) =>
                                                   {
                                                       if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("PLANTID")))
                                                       {
                                                           this.ShowMessage("NoSelectSite");
                                                           return false;
                                                       }

                                                       return true;
                                                   });

            processSegmentId.Conditions.AddTextBox("PROCESSSEGMENT");

            processSegmentId.Conditions.AddTextBox("PLANTID")
                                       .SetPopupDefaultByGridColumnId("PLANTID")
                                       .SetIsHidden();

            // 팝업 그리드
            processSegmentId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            processSegmentId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

        }
        /// <summary>
        /// 품목을 선택하는 팝업
        /// </summary>
        private void InitializeOperation_ProductDefIdPopup()
        {
            //팝업 컬럼 설정
            var productDefId = grdOperationSpec.View.AddSelectPopupColumn("PRODUCTDEFID", new SqlQuery("GetProductDefList", "10001", "PRODUCTDEFTYPE=Product", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                               .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, false)
                                               .SetPopupResultCount(1)
                                               .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                               .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                                               .SetRelationIds("PLANTID")
                                               .SetLabel("PRODUCTDEFID")
                                               .SetValidationKeyColumn()
                                               .SetPopupQueryPopup((DataRow currentrow) =>
                                               {
                                                   if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("PLANTID")))
                                                   {
                                                       this.ShowMessage("NoSelectSite");
                                                       return false;
                                                   }

                                                   return true;
                                               });

            productDefId.Conditions.AddTextBox("PRODUCTDEF");

            productDefId.Conditions.AddTextBox("PLANTID")
                                   .SetPopupDefaultByGridColumnId("PLANTID")
                                   .SetIsHidden();
            // 팝업 그리드
            productDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            productDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);

        }


        //테이블 생성 후 수정 필
        /// <summary>
        /// 검사항목을 조회하는 팝업
        /// </summary>
        private void InitializeOperation_InspItemIdPopup()
        {
            //팝업 컬럼 설정
            var inspItemId = grdOperationSpec.View.AddSelectPopupColumn("INSPITEMID", new SqlQuery("GetInspItemId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                            .SetPopupLayout("INSPITEM", PopupButtonStyles.Ok_Cancel, true, false)
                                            .SetPopupResultCount(1)
                                            .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                            .SetValidationKeyColumn();

            inspItemId.Conditions.AddTextBox("INSPITEMID");
            inspItemId.Conditions.AddTextBox("INSPITEMNAME");

            // 팝업 그리드
            inspItemId.GridColumns.AddTextBoxColumn("WORKTYPE", 150)
                                  .SetLabel("PARENTINSPITEMLCLASSID");
            inspItemId.GridColumns.AddTextBoxColumn("PARENTINSPITEMLCLASSNAME", 150);
            inspItemId.GridColumns.AddTextBoxColumn("WORKCONDITION", 150)
                                  .SetLabel("INSPITEMMCLASSID");
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMMCLASSNAME", 150);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMID", 150);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMNAME", 200);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMVERSION", 200);

        }

        #endregion

        #region //grdEtching 초기화
        private void InitializeGridEtching()
        {
            grdEtchingSpec.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdEtchingSpec.GridButtonItem -= GridButtonItem.Delete;

            grdEtchingSpec.View.SetSortOrder("SPECSEQUENCE");

            grdEtchingSpec.View.AddTextBoxColumn("SPECSEQUENCE", 150)
                .SetIsHidden()
                .SetIsReadOnly();

            grdEtchingSpec.View.AddTextBoxColumn("SPECCLASSID", 150)
                .SetIsReadOnly();

            grdEtchingSpec.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();

            grdEtchingSpec.View.AddTextBoxColumn("PLANTID", 150)
                .SetDefault("*")
                .SetIsReadOnly()
                .SetLabel("SITE");

            InitializeEtchingRate_ProcessSegmentClassPopup();

            grdEtchingSpec.View.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetIsHidden();

            InitializeEtchingRate_EquipmentListPopup();

            InitializeEtchingRate_ChileEquipmentListPopup();

            grdEtchingSpec.View.AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsHidden();

            grdEtchingSpec.View.AddTextBoxColumn("CONSUMABLEDEFID", 150)
                .SetIsHidden();

            grdEtchingSpec.View.AddTextBoxColumn("CUSTOMERID", 80)
                .SetIsHidden();

            grdEtchingSpec.View.AddTextBoxColumn("VENDORID", 150)
                .SetIsHidden();

            InitializeEtchingRate_InspItemIdPopup();

            //code 등록 후 codeClassId 파라미터 변경 필
            grdEtchingSpec.View.AddTextBoxColumn("WORKTYPE", 150)
                .SetIsHidden();

            grdEtchingSpec.View.AddTextBoxColumn("WORKCONDITION", 150)
                .SetIsHidden();

            grdEtchingSpec.View.AddTextBoxColumn("RESOURCEID", 150);

            //SPECVERSION - 서버에서 자동으로 0 입력
            grdEtchingSpec.View.AddTextBoxColumn("SPECVERSION", 200)
                .SetIsReadOnly();

            grdEtchingSpec.View.AddComboBoxColumn("UOMDEFID", 100, new SqlQuery("GetUOMList", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "UOMDEFID", "UOMDEFNAME")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdEtchingSpec.View.AddComboBoxColumn("DEFAULTCHARTTYPE", 100, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ControlType", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"))
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdEtchingSpec.View.AddTextBoxColumn("CONTROLRANGE", 200)
                .SetIsReadOnly();

            //code 등록 후 codeClassId 파라미터 변경 필
            grdEtchingSpec.View.AddComboBoxColumn("ANALYSISTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"))
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdEtchingSpec.View.AddTextBoxColumn("DESCRIPTION", 200);

            grdEtchingSpec.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdEtchingSpec.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdEtchingSpec.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdEtchingSpec.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdEtchingSpec.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdEtchingSpec.View.PopulateColumns();
        }
        /// <summary>
        /// 중공정을 선택하는 팝업
        /// </summary>
        private void InitializeEtchingRate_ProcessSegmentClassPopup()
        {
            //팝업 컬럼 설정
            var ProcessSegmentClassId = grdEtchingSpec.View.AddSelectPopupColumn("PROCESSSEGMENTCLASSID", new SqlQuery("GetProcessSegmentClass", "10001", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                                      .SetPopupLayout("PROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, false)
                                                      .SetPopupResultCount(1)
                                                      .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                                                      .SetRelationIds("PLANTID")
                                                      .SetValidationKeyColumn()
                                                      .SetPopupQueryPopup((DataRow currentrow) =>
                                                      {
                                                          if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("PLANTID")))
                                                          {
                                                              this.ShowMessage("NoSelectSite");
                                                              return false;
                                                          }

                                                          return true;
                                                      });

            ProcessSegmentClassId.Conditions.AddTextBox("PROCESSSEGMENTCLASS")
                .SetLabel("MIDDLEPROCESSSEGMENTCLASS");


            ProcessSegmentClassId.Conditions.AddTextBox("PLANTID")
                                            .SetPopupDefaultByGridColumnId("PLANTID")
                                            .SetIsHidden();

            // 팝업 그리드
            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PARENTPROCESSSEGMENTCLASSID", 150);
            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PARENTPROCESSSEGMENTCLASSNAME", 200);
            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 150);
            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200);

        }

        /// <summary>
        /// 설비를 선택하는 팝업
        /// </summary>
        private void InitializeEtchingRate_EquipmentListPopup()
        {
            //팝업 컬럼 설정
            var equipmentId = grdEtchingSpec.View.AddSelectPopupColumn("EQUIPMENTID", new SqlQuery("GetEquipmentByClassHierarchy", "10001", "DETAILEQUIPMENTTYPE=Main", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                             .SetPopupLayout("EQUIPMENT", PopupButtonStyles.Ok_Cancel, true, false)
                                             .SetPopupResultCount(1)
                                             .SetValidationKeyColumn()
                                             .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                                             .SetPopupAutoFillColumns("EQUIPMENTNAME");

            equipmentId.Conditions.AddComboBox("PARENTEQUIPMENTCLASSID", new SqlQuery("GetEquipmentClassHierarchyAndType", "10001", "HIERARCHY=TopEquipment", "EQUIPMENTCLASSTYPE=Production", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTCLASSNAME", "EQUIPMENTCLASSID")
                                  .SetValidationKeyColumn()
                                  .SetLabel("TOPEQUIPMENTCLASS");

            equipmentId.Conditions.AddComboBox("MIDDLEEQUIPMENTCLASSID", new SqlQuery("GetEquipmentClassHierarchyAndType", "10001", "HIERARCHY=MiddleEquipment", "EQUIPMENTCLASSTYPE=Production", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTCLASSNAME", "EQUIPMENTCLASSID")
                                  .SetValidationKeyColumn()
                                  .SetRelationIds("PARENTEQUIPMENTCLASSID")
                                  .SetLabel("MIDDLEEQUIPMENTCLASS");

            equipmentId.Conditions.AddTextBox("EQUIPMENT");

            // 팝업 그리드
            equipmentId.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSNAME", 150);
            equipmentId.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150);
            equipmentId.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200);

        }
        /// <summary>
        /// 설비단을 선택하는 팝업
        /// </summary>
        private void InitializeEtchingRate_ChileEquipmentListPopup()
        {
            //팝업 컬럼 설정
            var childEquipementId = grdEtchingSpec.View.AddSelectPopupColumn("CHILDEQUIPMENTID", new SqlQuery("GetEquipmentListByDetailType", "10001", "EQUIPMENTCLASSTYPE=Production", "DETAILEQUIPMENTTYPE=Sub", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                                  .SetPopupLayout("EQUIPMENT", PopupButtonStyles.Ok_Cancel, true, false)
                                                  .SetPopupResultCount(1)
                                                  .SetPopupResultMapping("CHILDEQUIPMENTID", "EQUIPMENTID")
                                                  .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                                  .SetPopupAutoFillColumns("EQUIPMENTNAME")
                                                  .SetLabel("CHILDEQUIPMENTID")
                                                  .SetRelationIds("PARENTEQUIPMENTID")
                                                  .SetValidationKeyColumn()
                                                  .SetPopupQueryPopup((DataRow currentrow) =>
                                                  {
                                                      if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("EQUIPMENTID")))
                                                      {
                                                          this.ShowMessage("NoSelectParentEquipment");
                                                          return false;
                                                      }

                                                      return true;
                                                  });

            childEquipementId.Conditions.AddTextBox("EQUIPMENT");

            childEquipementId.Conditions.AddTextBox("PARENTEQUIPMENTID")
                                        .SetPopupDefaultByGridColumnId("EQUIPMENTID")
                                        .SetIsHidden();
            // 팝업 그리드
            childEquipementId.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150);
            childEquipementId.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200);

        }

        //테이블 생성 후 수정 필
        /// <summary>
        /// 검사항목을 조회하는 팝업
        /// </summary>
        private void InitializeEtchingRate_InspItemIdPopup()
        {
            //팝업 컬럼 설정
            var inspItemId = grdEtchingSpec.View.AddSelectPopupColumn("INSPITEMID", new SqlQuery("GetInspItemId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                            .SetPopupLayout("INSPITEM", PopupButtonStyles.Ok_Cancel, true, false)
                                            .SetPopupResultCount(1)
                                            .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                            .SetValidationKeyColumn();

            inspItemId.Conditions.AddTextBox("INSPITEMID");
            inspItemId.Conditions.AddTextBox("INSPITEMNAME");

            // 팝업 그리드
            inspItemId.GridColumns.AddTextBoxColumn("WORKTYPE", 150)
                                  .SetLabel("PARENTINSPITEMLCLASSID");
            inspItemId.GridColumns.AddTextBoxColumn("PARENTINSPITEMLCLASSNAME", 150);
            inspItemId.GridColumns.AddTextBoxColumn("WORKCONDITION", 150)
                                  .SetLabel("INSPITEMMCLASSID");
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMMCLASSNAME", 150);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMID", 150);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMNAME", 200);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMVERSION", 200);

        }

        #endregion

        #region //grdShipment초기화
        private void InitializeGridShipment()
        {
            grdShipmentSpec.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdShipmentSpec.GridButtonItem -= GridButtonItem.Delete;

            grdShipmentSpec.View.SetSortOrder("SPECSEQUENCE");

            grdShipmentSpec.View.AddTextBoxColumn("SPECSEQUENCE", 150)
                .SetIsHidden()
                .SetIsReadOnly();

            grdShipmentSpec.View.AddTextBoxColumn("SPECCLASSID", 150)
                .SetIsReadOnly();

            grdShipmentSpec.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();

            grdShipmentSpec.View.AddTextBoxColumn("PLANTID", 150)
                .SetDefault("*")
                .SetIsReadOnly()
                .SetLabel("SITE");

            InitializeShipment_ProcessSegmentClassPopup();

            grdShipmentSpec.View.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetIsHidden();

            grdShipmentSpec.View.AddTextBoxColumn("EQUIPMENTID", 150)
                .SetIsHidden();

            grdShipmentSpec.View.AddTextBoxColumn("CHILDEQUIPMENTID", 150)
                .SetIsHidden();

            InitializeShipment_ProductDefIdPopup();

            //InitializeShipment_ConsumableDefIdPopup();

            grdShipmentSpec.View.AddComboBoxColumn("CUSTOMERID", 150, new SqlQuery("GetCustomerList", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "CUSTOMERNAME", "CUSTOMERID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                .SetValidationKeyColumn();

            grdShipmentSpec.View.AddComboBoxColumn("VENDORID", 150, new SqlQuery("GetVendorList", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "VENDORNAME", "VENDORID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                .SetValidationKeyColumn();

            grdShipmentSpec.View.AddTextBoxColumn("WORKTYPE", 100)
                .SetIsHidden();

            //inspItemclass : InitializeShipment_InspItemIdPopup 팝업 선택으로 자동 입력 될때
            //grdShipmentSpec.View.AddTextBoxColumn("WORKCONDITION", 100)
            //    .SetIsReadOnly()
            //    .SetValidationKeyColumn();  


            grdShipmentSpec.View.AddTextBoxColumn("INSPECTIONTYPE", 200)
                .SetDefault("ShipmentInspection", "ShipmentInspection")
                .SetIsHidden();

            //inspItemclass : 콤보로 선택 후 InitializeShipment_InspItemIdPopup relation 걸릴 때
            grdShipmentSpec.View.AddComboBoxColumn("WORKCONDITION", 150, new SqlQuery("GetInspItemClass", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}", "TYPE=ParentClass"), "INSPITEMCLASSNAME", "INSPITEMCLASSID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                .SetValidationKeyColumn()
                .SetRelationIds("INSPECTIONTYPE");

            InitializeShipment_InspItemIdPopup();

            grdShipmentSpec.View.AddTextBoxColumn("RESOURCEID", 150);

            //SPECVERSION - 서버에서 자동으로 0 입력
            grdShipmentSpec.View.AddTextBoxColumn("SPECVERSION", 200)
                .SetIsReadOnly();

            grdShipmentSpec.View.AddComboBoxColumn("UOMDEFID", 100, new SqlQuery("GetUOMList", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "UOMDEFID", "UOMDEFNAME")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdShipmentSpec.View.AddComboBoxColumn("DEFAULTCHARTTYPE", 100, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ControlType", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"))
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdShipmentSpec.View.AddTextBoxColumn("CONTROLRANGE", 200)
                .SetIsReadOnly();

            //code 등록 후 codeClassId 파라미터 변경 필
            grdShipmentSpec.View.AddComboBoxColumn("ANALYSISTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"))
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdShipmentSpec.View.AddTextBoxColumn("DESCRIPTION", 200);

            grdShipmentSpec.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdShipmentSpec.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdShipmentSpec.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdShipmentSpec.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdShipmentSpec.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdShipmentSpec.View.PopulateColumns();
        }
        /// <summary>
        /// 중공정을 선택하는 팝업
        /// </summary>
        private void InitializeShipment_ProcessSegmentClassPopup()
        {
            //팝업 컬럼 설정
            var ProcessSegmentClassId = grdShipmentSpec.View.AddSelectPopupColumn("PROCESSSEGMENTCLASSID", new SqlQuery("GetProcessSegmentClass", "10001", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                                       .SetPopupLayout("PROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, false)
                                                       .SetPopupResultCount(1)
                                                       .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                                                       .SetPopupAutoFillColumns("PROCESSSEGMENTCLASSNAME")
                                                       .SetRelationIds("PLANTID")
                                                       .SetValidationKeyColumn()
                                                       .SetPopupQueryPopup((DataRow currentrow) =>
                                                       {
                                                           if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("PLANTID")))
                                                           {
                                                               this.ShowMessage("NoSelectSite");
                                                               return false;
                                                           }

                                                           return true;
                                                       });

            ProcessSegmentClassId.Conditions.AddTextBox("PROCESSSEGMENTCLASS")
                .SetLabel("MIDDLEPROCESSSEGMENTCLASS");

            ProcessSegmentClassId.Conditions.AddTextBox("PLANTID")
                                            .SetPopupDefaultByGridColumnId("PLANTID")
                                            .SetIsHidden();

            // 팝업 그리드
            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PARENTPROCESSSEGMENTCLASSID", 150);
            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PARENTPROCESSSEGMENTCLASSNAME", 200);
            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 150);
            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200);

        }
        /// <summary>
        /// 품목을 선택하는 팝업
        /// </summary>
        private void InitializeShipment_ProductDefIdPopup()
        {
            //팝업 컬럼 설정
            var productDefId = grdShipmentSpec.View.AddSelectPopupColumn("PRODUCTDEFID", new SqlQuery("GetProductDefList", "10001", "PRODUCTDEFTYPE=Product", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                              .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, false)
                                              .SetPopupResultCount(1)
                                              .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                              .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                                              .SetRelationIds("PLANTID")
                                              .SetLabel("PRODUCTDEFID")
                                              .SetValidationKeyColumn()
                                              .SetPopupQueryPopup((DataRow currentrow) =>
                                              {
                                                  if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("PLANTID")))
                                                  {
                                                      this.ShowMessage("NoSelectSite");
                                                      return false;
                                                  }

                                                  return true;
                                              });

            productDefId.Conditions.AddTextBox("PRODUCTDEF");

            productDefId.Conditions.AddTextBox("PLANTID")
                                   .SetPopupDefaultByGridColumnId("PLANTID")
                                   .SetIsHidden();
            // 팝업 그리드
            productDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            productDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);

        }
        /// <summary>
        /// 소모성자재를 선택하는 팝업
        /// </summary>
        private void InitializeShipment_ConsumableDefIdPopup()
        {
            //팝업 컬럼 설정
            var consumableDefId = grdShipmentSpec.View.AddSelectPopupColumn("CONSUMABLEDEFID", new SqlQuery("GetConsumableDefList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                                 .SetPopupLayout("CONSUMABLE", PopupButtonStyles.Ok_Cancel, true, false)
                                                 .SetPopupResultCount(1)
                                                 .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                                 .SetPopupAutoFillColumns("CONSUMABLEDEFNAME")
                                                 .SetPopupResultMapping("CONSUMABLEDEFID", "CONSUMABLEDEFID")
                                                 .SetRelationIds("PLANTID")
                                                  .SetValidationKeyColumn()
                                                 .SetPopupQueryPopup((DataRow currentrow) =>
                                                 {
                                                     if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("PLANTID")))
                                                     {
                                                         this.ShowMessage("NoSelectSite");
                                                         return false;
                                                     }

                                                     return true;
                                                 });

            consumableDefId.Conditions.AddTextBox("CONSUMABLEDEF");

            consumableDefId.Conditions.AddTextBox("PLANTID")
                                      .SetPopupDefaultByGridColumnId("PLANTID")
                                      .SetIsHidden();
            // 팝업 그리드
            consumableDefId.GridColumns.AddTextBoxColumn("CONSUMABLEDEFID", 150);
            consumableDefId.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);

        }

        //테이블 생성 후 수정 필
        /// <summary>
        /// 검사항목을 조회하는 팝업
        /// </summary>
        private void InitializeShipment_InspItemIdPopup()
        {
            //팝업 컬럼 설정
            var inspItemId = grdShipmentSpec.View.AddSelectPopupColumn("INSPITEMID", new SqlQuery("GetInspItemId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                            .SetPopupLayout("INSPITEM", PopupButtonStyles.Ok_Cancel, true, false)
                                            .SetPopupResultCount(1)
                                            .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                            .SetRelationIds("INSPITEMCLASSID")
                                            .SetValidationKeyColumn()
                                            .SetPopupQueryPopup((DataRow currentrow) =>
                                            {
                                                if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("WORKCONDITION")))
                                                {
                                                    this.ShowMessage("NoSelectInspItemclassId");
                                                    return false;
                                                }

                                                return true;
                                            });

            inspItemId.Conditions.AddTextBox("INSPITEMID");
            inspItemId.Conditions.AddTextBox("INSPITEMNAME");

            inspItemId.Conditions.AddTextBox("INSPITEMCLASSID")
                .SetPopupDefaultByGridColumnId("WORKCONDITION")
                .SetIsHidden();

            // 팝업 그리드
            //inspItemId.GridColumns.AddTextBoxColumn("WORKTYPE", 150)
            //                      .SetLabel("PARENTINSPITEMLCLASSID");
            //inspItemId.GridColumns.AddTextBoxColumn("PARENTINSPITEMLCLASSNAME", 150);

            inspItemId.GridColumns.AddTextBoxColumn("WORKCONDITION", 150)
                                  .SetLabel("INSPITEMMCLASSID");
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMMCLASSNAME", 150);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMID", 150);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMNAME", 200);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMVERSION", 200);

        }

        // 테이블 생성 후 수정 필
        /// <summary>
        /// WORKCONDITION을 조회하는 팝업 
        /// INSPITEMCLASS의 inspitemclassid
        /// </summary>
        private void InitializeShipment_WorkCondition()
        {
            //팝업 컬럼 설정

            var inspItemId = grdShipmentSpec.View.AddSelectPopupColumn("WORKCONDITION", new SqlQuery("GetInspItemClass", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "TYPE=Class"))
                                       .SetPopupLayout("INSPITEMCLASS", PopupButtonStyles.Ok_Cancel, true, false)
                                       .SetPopupResultCount(1)
                                       .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                       .SetPopupAutoFillColumns("INSPITEMCLASSNAME")
                                       .SetPopupResultMapping("WORKCONDITION", "INSPITEMCLASSID")
                                       .SetValidationKeyColumn();

            inspItemId.Conditions.AddTextBox("INSPITEMCLASS");

            // 팝업 그리드
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMCLASSID", 150);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMCLASSNAME", 200);

        }
        #endregion

        #region //grdMeasurement 초기화
        private void InitializeGridMeasurement()
        {
            grdMeasurementSpec.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdMeasurementSpec.GridButtonItem -= GridButtonItem.Delete;

            grdMeasurementSpec.View.SetSortOrder("SPECSEQUENCE");

            grdMeasurementSpec.View.AddTextBoxColumn("SPECSEQUENCE", 150)
                .SetIsHidden()
                .SetIsReadOnly();

            grdMeasurementSpec.View.AddTextBoxColumn("SPECCLASSID", 150)
                .SetIsReadOnly();

            grdMeasurementSpec.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();

            grdMeasurementSpec.View.AddComboBoxColumn("PLANTID", 150, new SqlQuery("GetPlantList", "10001", $"ENTERPRISEID={Framework.UserInfo.Current.Enterprise}", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                .SetEmptyItem("*", "*", true)
                .SetLabel("SITE")
                .SetValidationKeyColumn();

            grdMeasurementSpec.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 150)
                .SetIsHidden();

            InitializeMeasurement_ProcessSegmentIdPopup();

            InitializeMeasurement_EquipmentListPopup();

            grdMeasurementSpec.View.AddTextBoxColumn("CHILDEQUIPMENTID", 150)
                .SetIsHidden();

            grdMeasurementSpec.View.AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsHidden();

            grdMeasurementSpec.View.AddTextBoxColumn("CONSUMABLEDEFID", 150)
                .SetIsHidden();

            grdMeasurementSpec.View.AddTextBoxColumn("CUSTOMERID", 80)
                .SetIsHidden();

            grdMeasurementSpec.View.AddComboBoxColumn("VENDORID", 150, new SqlQuery("GetVendorList", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "VENDORNAME", "VENDORID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                .SetValidationKeyColumn();

            //code 등록 후 codeClassId 파라미터 변경 필
            grdMeasurementSpec.View.AddTextBoxColumn("WORKTYPE", 100)
                .SetIsHidden();

            //inspItemclass : InitializeMeasurement_InspItemIdPopup 팝업 선택으로 자동 입력 될때
            //grdMeasurementSpec.View.AddTextBoxColumn("WORKCONDITION", 100)
            //   .SetIsReadOnly()
            //   .SetValidationKeyColumn();

            grdMeasurementSpec.View.AddTextBoxColumn("INSPECTIONTYPE", 200)
                .SetDefault("MeasurementInspection", "MeasurementInspection")
                .SetIsHidden();

            //inspItemclass : 콤보로 선택 후 InitializeMeasurement_InspItemIdPopup relation 걸릴 때
            grdMeasurementSpec.View.AddComboBoxColumn("WORKCONDITION", 150, new SqlQuery("GetInspItemClass", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}", "TYPE=ParentClass"), "INSPITEMCLASSNAME", "INSPITEMCLASSID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                .SetValidationKeyColumn()
                .SetRelationIds("INSPECTIONTYPE");

            InitializeMeasurement_InspItemIdPopup();

            grdMeasurementSpec.View.AddTextBoxColumn("RESOURCEID", 150);

            //SPECVERSION - 서버에서 자동으로 0 입력
            grdMeasurementSpec.View.AddTextBoxColumn("SPECVERSION", 200)
                .SetIsReadOnly();

            grdMeasurementSpec.View.AddComboBoxColumn("UOMDEFID", 100, new SqlQuery("GetUOMList", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "UOMDEFID", "UOMDEFNAME")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdMeasurementSpec.View.AddComboBoxColumn("DEFAULTCHARTTYPE", 100, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ControlType", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"))
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdMeasurementSpec.View.AddTextBoxColumn("CONTROLRANGE", 200)
                .SetIsReadOnly();

            //code 등록 후 codeClassId 파라미터 변경 필
            grdMeasurementSpec.View.AddComboBoxColumn("ANALYSISTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"))
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            grdMeasurementSpec.View.AddTextBoxColumn("DESCRIPTION", 200);

            grdMeasurementSpec.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdMeasurementSpec.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdMeasurementSpec.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdMeasurementSpec.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdMeasurementSpec.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdMeasurementSpec.View.PopulateColumns();
        }
        /// <summary>
        /// 표준공정 선택하는 팝업
        /// </summary>
        private void InitializeMeasurement_ProcessSegmentIdPopup()
        {
            //팝업 컬럼 설정
            var processSegmentId = grdMeasurementSpec.View.AddSelectPopupColumn("PROCESSSEGMENTID", new SqlQuery("GetProcessSegmentList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                                     .SetPopupLayout("PROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, false)
                                                     .SetPopupResultCount(1)
                                                     .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                                     .SetPopupAutoFillColumns("PROCESSSEGMENTNAME")
                                                     .SetRelationIds("PLANTID")
                                                     .SetValidationKeyColumn()
                                                     .SetPopupQueryPopup((DataRow currentrow) =>
                                                     {
                                                         if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("PLANTID")))
                                                         {
                                                             this.ShowMessage("NoSelectSite");
                                                             return false;
                                                         }

                                                         return true;
                                                     });

            processSegmentId.Conditions.AddTextBox("PROCESSSEGMENT");

            processSegmentId.Conditions.AddTextBox("PLANTID")
                                       .SetPopupDefaultByGridColumnId("PLANTID")
                                       .SetIsHidden();
            // 팝업 그리드
            processSegmentId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            processSegmentId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

        }

        /// <summary>
        /// 설비를 선택하는 팝업
        /// </summary>
        private void InitializeMeasurement_EquipmentListPopup()
        {
            //팝업 컬럼 설정
            var equipmentId = grdMeasurementSpec.View.AddSelectPopupColumn("EQUIPMENTID", new SqlQuery("GetEquipmentByClassHierarchy", "10001", "DETAILEQUIPMENTTYPE=Main", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                             .SetPopupLayout("EQUIPMENT", PopupButtonStyles.Ok_Cancel, true, false)
                                             .SetPopupResultCount(1)
                                             .SetValidationKeyColumn()
                                             .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                                             .SetPopupAutoFillColumns("EQUIPMENTNAME");

            equipmentId.Conditions.AddComboBox("PARENTEQUIPMENTCLASSID", new SqlQuery("GetEquipmentClassHierarchyAndType", "10001", "HIERARCHY=TopEquipment", "EQUIPMENTCLASSTYPE=Measure", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTCLASSNAME", "EQUIPMENTCLASSID")
                                  .SetValidationKeyColumn()
                                  .SetLabel("TOPEQUIPMENTCLASS");

            equipmentId.Conditions.AddComboBox("MIDDLEEQUIPMENTCLASSID", new SqlQuery("GetEquipmentClassHierarchyAndType", "10001", "HIERARCHY=MiddleEquipment", "EQUIPMENTCLASSTYPE=Measure", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTCLASSNAME", "EQUIPMENTCLASSID")
                                  .SetValidationKeyColumn()
                                  .SetRelationIds("PARENTEQUIPMENTCLASSID")
                                  .SetLabel("MIDDLEEQUIPMENTCLASS");

            equipmentId.Conditions.AddTextBox("EQUIPMENT");

            // 팝업 그리드
            equipmentId.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSNAME", 150);
            equipmentId.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150);
            equipmentId.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200);

        }


        //테이블 생성 후 수정 필
        /// <summary>
        /// 검사항목을 조회하는 팝업
        /// </summary>
        private void InitializeMeasurement_InspItemIdPopup()
        {
            //팝업 컬럼 설정
            var inspItemId = grdMeasurementSpec.View.AddSelectPopupColumn("INSPITEMID", new SqlQuery("GetInspItemId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                            .SetPopupLayout("INSPITEM", PopupButtonStyles.Ok_Cancel, true, false)
                                            .SetPopupResultCount(1)
                                            .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                            .SetRelationIds("INSPITEMCLASSID")
                                            .SetValidationKeyColumn()
                                            .SetPopupQueryPopup((DataRow currentrow) =>
                                            {
                                                if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("WORKCONDITION")))
                                                {
                                                    this.ShowMessage("NoSelectInspItemclassId");
                                                    return false;
                                                }

                                                return true;
                                            });

            inspItemId.Conditions.AddTextBox("INSPITEMID");
            inspItemId.Conditions.AddTextBox("INSPITEMNAME");

            inspItemId.Conditions.AddTextBox("INSPITEMCLASSID")
                .SetPopupDefaultByGridColumnId("WORKCONDITION")
                .SetIsHidden();

            // 팝업 그리드
            //inspItemId.GridColumns.AddTextBoxColumn("WORKTYPE", 150)
            //                      .SetLabel("PARENTINSPITEMLCLASSID");
            //inspItemId.GridColumns.AddTextBoxColumn("PARENTINSPITEMLCLASSNAME", 150);

            inspItemId.GridColumns.AddTextBoxColumn("WORKCONDITION", 150)
                                  .SetLabel("INSPITEMMCLASSID");
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMMCLASSNAME", 150);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMID", 150);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMNAME", 200);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMVERSION", 200);

        }

        // 테이블 생성 후 수정 필
        /// <summary>
        /// WORKCONDITION을 조회하는 팝업 
        /// INSPITEMCLASS의 inspitemclassid
        /// </summary>
        private void InitializeMeasurement_WorkCondition()
        {
            //팝업 컬럼 설정
            var inspItemId = grdMeasurementSpec.View.AddSelectPopupColumn("WORKCONDITION", new SqlQuery("GetInspItemClass", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "TYPE=Class"))
                                               .SetPopupLayout("INSPITEMCLASS", PopupButtonStyles.Ok_Cancel, true, false)
                                               .SetPopupResultCount(1)
                                               .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                               .SetPopupAutoFillColumns("INSPITEMCLASSNAME")
                                               .SetPopupResultMapping("INSPITEMCLASSID", "PROCESSSEGMENTID")
                                               .SetValidationKeyColumn();

            inspItemId.Conditions.AddTextBox("INSPITEMCLASS");

            // 팝업 그리드
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMCLASSID", 150);
            inspItemId.GridColumns.AddTextBoxColumn("INSPITEMCLASSNAME", 200);

        }



        #endregion
        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            Load += SpecManagement_Load;

            tabSpec.SelectedPageChanged += SmartTabControl1_SelectedPageChanged;

            grdChemicalSpec.View.DoubleClick += View_DoubleClick;
            new SetGridDeleteButonVisible(grdChemicalSpec);
            grdChemicalSpec.View.AddingNewRow += View_AddingNewRow;

            grdWaterSpec.View.DoubleClick += View_DoubleClick;
            new SetGridDeleteButonVisible(grdWaterSpec);
            grdWaterSpec.View.AddingNewRow += View_AddingNewRow;

            grdOSPSpec.View.DoubleClick += View_DoubleClick;
            new SetGridDeleteButonVisible(grdOSPSpec);
            grdOSPSpec.View.AddingNewRow += View_AddingNewRow;

            grdRawSpec.View.DoubleClick += View_DoubleClick;
            new SetGridDeleteButonVisible(grdRawSpec);
            grdRawSpec.View.AddingNewRow += View_AddingNewRow;

            grdSubassemblySpec.View.DoubleClick += View_DoubleClick;
            new SetGridDeleteButonVisible(grdSubassemblySpec);
            grdSubassemblySpec.View.AddingNewRow += View_AddingNewRow;

            grdSubsidiarySpec.View.DoubleClick += View_DoubleClick;
            new SetGridDeleteButonVisible(grdSubsidiarySpec);
            grdSubsidiarySpec.View.AddingNewRow += View_AddingNewRow;

            //grdOperationSpec.View.DoubleClick += View_DoubleClick;
            //new SetGridDeleteButonVisible(grdOperationSpec);
            //grdOperationSpec.View.AddingNewRow += View_AddingNewRow;

            grdEtchingSpec.View.DoubleClick += View_DoubleClick;
            new SetGridDeleteButonVisible(grdEtchingSpec);
            grdEtchingSpec.View.AddingNewRow += View_AddingNewRow;

            grdShipmentSpec.View.DoubleClick += View_DoubleClick;
            new SetGridDeleteButonVisible(grdShipmentSpec);
            grdShipmentSpec.View.AddingNewRow += View_AddingNewRow;

            grdMeasurementSpec.View.DoubleClick += View_DoubleClick;
            new SetGridDeleteButonVisible(grdMeasurementSpec);
            grdMeasurementSpec.View.AddingNewRow += View_AddingNewRow;

            grdOperationSpec.View.DoubleClick += View_DoubleClick;

        }

        //폼로드시 relrow, tanindex할당 
        public override void LoadForm(Dictionary<string, object> parameters)
        {
            if (parameters != null && parameters.ContainsKey("row") && parameters.ContainsKey("tabIndex") && parameters.ContainsKey("fromMenu"))
            {
                _relRow = parameters["row"] as DataRow;
                _fromMenu = parameters["fromMenu"].ToString();
                _inspectionClassType = parameters["inspectionClassType"].ToString();

                if (_fromMenu.Equals("Inspections"))
                {//검사기준정보로부터 넘겨받았을 때
                    switch (_inspectionClassType)
                    {
                        case "OSPInspection"://OSP
                            _tabIndex = 3;
                            break;

                        case "RawSpec"://원자재
                            _tabIndex = 4;
                            break;

                        case "SubassemblyInspection"://가공품
                            _tabIndex = 5;
                            break;

                        case "SubsidiaryInspection"://부자재
                            _tabIndex = 6;
                            break;

                        case "OperationInspection"://규격계측
                            _tabIndex = 7;
                            break;

                        case "ShipmentInspection"://출하
                            _tabIndex = 9;
                            break;


                    }
                }
                else
                {//약품 , 수질로부터 넘겨 받았을 때
                    switch (_inspectionClassType)
                    {
                        case "ChemicalInspection"://약품
                            _tabIndex = 1;
                            break;

                        case "WaterInspection"://수질
                            _tabIndex = 2;
                            break;
                    }

                }
            }

            base.LoadForm(parameters);
        }
        /// <summary>
        /// 화면로드시 SpecClass조회하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpecManagement_Load(object sender, EventArgs e)
        {

            Control[] controls = pnlToolbar.Controls.Find("Save", true);
            if (controls.Count() > 0 && controls[0] is SmartButton)
            {
                _button = controls[0] as SmartButton;
                _button.Visible = false;
            }

            if (_relRow != null)
            {
                tabSpec.SelectedTabPageIndex = _tabIndex;
                _grid.View.AddNewRow();
            }
            else
            {
                LoadSpecClassData();
            }
        }
        /// <summary>
        /// 스펙등록시 SpecClass자동 입력해주는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            args.NewRow["SPECCLASSID"] = _specClassId;
            args.NewRow["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;
            args.NewRow["INSPECTIONTYPE"] = _inspectiontype;

            if (_relRow != null)
            { //rel 화면에서 받아온 row 탭별로, 컬럼
                //이벤트 분리? 약품수질..
                args.NewRow["EQUIPMENTID"] = _relRow["TEST"];
                args.NewRow["CHILDEQUIPMENTID"] = "CHILDTEST";
            }
        }

        /// <summary>
        /// 그리드ROW 더블클릭시 SPEC을 입력할수 있는 POPUP창 띄우는 이벤트
        /// ROWSTATE가 ADDED인 경우 SPEC등록 불가(Spec 입력 POPUP창 뜨지않음)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            SmartBandedGridView view = sender as SmartBandedGridView;
            GridHitInfo info = view.CalcHitInfo(ea.Location);

            DataRow chkData = _grid.View.GetDataRow(info.RowHandle);

            if (chkData == null || chkData.RowState.Equals(DataRowState.Added)) return;

            DialogManager.ShowWaitArea(pnlContent);

            SpecRegisterDetailPopUp popup = new SpecRegisterDetailPopUp();
            popup._specSequence = chkData["SPECSEQUENCE"].ToString();
            popup._specClassId = chkData["SPECCLASSID"].ToString();
            popup._cboCheck = chkData["DEFAULTCHARTTYPE"].ToString();//hc.kim  데이터 넘김 
            if (string.IsNullOrEmpty(_inspectionName))
            {
                _inspectionName = this.tabSpec.SelectedTabPage.Text;
            }
            if (_gridName.Equals("grdOperationSpec"))
            {
                popup.buttonType = false;
            }
            popup._inspectionName = _inspectionName;
            popup.Owner = this;
            popup.ShowDialog();

            DialogManager.CloseWaitArea(pnlContent);

            if (popup.DialogResult == DialogResult.OK)
            {
                SearchSpecDef();
            }
        }
        /// <summary>
        /// 선택된 탭이 변경될때 해당 탭의 그리드가 초기화 되지않은 경우 초기화 해줍니다.
        /// 선택된 탭의 그리드에 맞게 변수 할당해줍니다.
        /// 탭마다 다른 조회조건은 바인딩 해줍니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SmartTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            _grid = e.Page.Controls[0] as SmartBandedGrid;
            _gridName = _grid.Name.ToString();
            _inspectionName = e.Page.Text;
            _specClassId = _gridName.Substring(3);

            if (!_grid.View.IsInitializeColumns)
            {
                switch (_gridName)
                {
                    case "grdChemicalSpec":
                        InitializeGridChemical();
                        break;

                    case "grdWaterSpec":
                        InitializeGridWater();
                        break;

                    case "grdOSPSpec":
                        InitializeGridOSP();
                        break;

                    case "grdRawSpec":
                        InitializeGridRaw();
                        break;

                    case "grdSubassemblySpec":
                        InitializeGridSubassembly();
                        break;

                    case "grdSubsidiarySpec":
                        InitializeGridSubsidiary();
                        break;

                    case "grdOperationSpec":
                        InitializeGridOperation();
                        break;

                    case "grdEtchingSpec":
                        InitializeGridEtching();
                        break;

                    case "grdShipmentSpec":
                        InitializeGridShipment();
                        break;

                    case "grdMeasurementSpec":
                        InitializeGridMeasurement();
                        break;
                }

            }


            if (tabSpec.SelectedTabPageIndex == 0)
            {
                _button.Visible = false;
            }
            else
            {
                _button.Visible = true;
            }

            switch (tabSpec.SelectedTabPageIndex)
            {
                case 1:
                    _inspectiontype = "ChemicalInspection";
                    break;

                case 2:
                    _inspectiontype = "WaterInspection";
                    break;

                case 3:
                    _inspectiontype = "OSPInspection";
                    break;

                case 4:
                    _inspectiontype = "RawInspection";
                    break;

                case 5:
                    _inspectiontype = "SubassemblyInspection";
                    break;

                case 6:
                    _inspectiontype = "SubsidiaryInspection";
                    break;

                case 7:
                    _inspectiontype = "OperationInspection";
                    break;

                case 9:
                    _inspectiontype = "ShipmentInspection";
                    break;

                case 10:
                    _inspectiontype = "MeasurementInspection";
                    break;
            }

            ChangeCondition();
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            if (tabSpec.SelectedTabPageIndex != 0)
            {
                DataTable changed = _grid.GetChangedRows();

                ExecuteRule("SaveSpecDefinition", changed);
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

            if (tabSpec.SelectedTabPageIndex != 0)
            {
                var values = Conditions.GetValues();

                values.Add("P_LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
                values.Add("P_SPECCLASSID", _specClassId);
                DataTable dt = await SqlExecuter.QueryAsync("SelectSpecDefinition", "10001", values);

                if (dt.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                _grid.DataSource = dt;

            }

        }
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
        }

        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            ChangeCondition();
        }
        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            _grid.View.CheckValidation();

            if (tabSpec.SelectedTabPageIndex == 0)
            {
                throw MessageException.Create("NoSaveData");
            }

            DataTable changed = _grid.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 화면로드시 내부변수에 초기값 할당해주는 함수
        /// </summary>
        private void InitializeVariable()
        {
            _grid = this.grdSpecClass;
            _gridName = "grdSpecClass";
        }

        /// <summary>
        /// 탭 마다 다른 조회조건을 바인딩 시켜주기 위한 함수
        /// </summary>
        private void ChangeCondition()
        {
            SqlQuery condition = new SqlQuery("GetConditionItemDataTabChangeSpecManagement", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"GRIDNAME={_gridName}");
            DataTable conditionTable = condition.Execute();
            this.Conditions.GetControl<SmartComboBox>("p_conditionItem").ValueMember = "CODEID";
            this.Conditions.GetControl<SmartComboBox>("p_conditionItem").DisplayMember = "CODENAME";
            this.Conditions.GetControl<SmartComboBox>("p_conditionItem").EditValue = "*";
            this.Conditions.GetControl<SmartComboBox>("p_conditionItem").DataSource = conditionTable;
        }

        /// <summary>
        /// SpecClass를 조회하는 함수
        /// </summary>
        private void LoadSpecClassData()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            grdSpecClass.DataSource = SqlExecuter.Query("SelectSpecClass", "10001", param);
        }

        /// <summary>
        /// specDef를 조회하는 함수
        /// </summary>
        private async void SearchSpecDef()
        {
            await OnSearchAsync();
        }
        #endregion
    }
}
