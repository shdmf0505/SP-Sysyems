#region using

using DevExpress.XtraEditors.Repository;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;
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
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품질 기준 정보 > 검사기준정보
    /// 업  무  설  명  : 검사 기준 정보 연계정보를 관리 한다
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-06-18
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class InspectionItemRelationManagement : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        int _tabIndex = 0;// 선택된 tab의 Index
        DataRow _rowInspection = null; //inpection그리드의 Focused Row가 변경될 때 선택된 Row를 담을 변수 , Rel그리드 조회시 파라미터로 필요
        DataRow _rowResource = null; //resource그리드의 Focused Row가 변경될 때 선택된 Row를 담을 변수 , Rel그리드 조회시 파라미터로 필요
        DataRow _rowInspItemclass = null; //inspItemClass그리드의 Focused Row가 변경될 때 선택된 Row를 담을 변수 , Rel그리드 조회시 파라미터로 필요
        DataRow _rowRel = null; //Rel그리드의 Focused Row가 변경될 때 선택된 Row를 담을 변수 , point 그리드 조회시 파라미터로 필요
        SmartBandedGrid _grdInspection, _grdResource, _grdItemClass, _grdRel, _grdPoint; //탭마다의 그리드를 담을 변수


        #endregion

        #region 생성자

        public InspectionItemRelationManagement()
        {
            InitializeComponent();
        }

        #endregion
     
        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();
        }

        #region 수입검사(원자재) 탭의 그리드 초기화

        /// <summary>        
        /// 수입검사(원자재) 검사 종류 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdRawInspection()
        {
            // TODO : 그리드 초기화 로직 추가
            grdRawInspection.GridButtonItem = GridButtonItem.None;
            grdRawInspection.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdRawInspection.View.SetIsReadOnly();

            grdRawInspection.View.AddTextBoxColumn("INSPECTIONCLASSID", 150);

            grdRawInspection.View.AddTextBoxColumn("INSPECTIONCLASSNAME", 200);

            grdRawInspection.View.AddTextBoxColumn("INSPECTIONDEFID", 150);

            grdRawInspection.View.AddTextBoxColumn("INSPECTIONDEFNAME", 200);

            grdRawInspection.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 200);

            grdRawInspection.View.AddTextBoxColumn("RESOURCETYPE", 200);

            grdRawInspection.View.PopulateColumns();
        }

        /// <summary>        
        /// 수입검사(원자재) 검사품목 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdRawConsumable()
        {
            // TODO : 그리드 초기화 로직 추가
            grdRawConsumable.GridButtonItem = GridButtonItem.Add;
            grdRawConsumable.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            InitializeGrdRaw_ConsumableDefIdPopup();

            grdRawConsumable.View.AddTextBoxColumn("RESOURCENAME", 200)
                .SetLabel("CONSUMABLEDEFNAME")
                .SetIsReadOnly();

            grdRawConsumable.View.AddTextBoxColumn("RESOURCEVERSION", 150)
                .SetLabel("RESOURCEVERSION")
                .SetIsReadOnly();
         
            grdRawConsumable.View.PopulateColumns();
        }
        /// <summary>
        /// 검사 품목을 선택하는 팝업
        /// </summary>
        private void InitializeGrdRaw_ConsumableDefIdPopup()
        {
            //팝업 컬럼 설정
            var consumableDefId = grdRawConsumable.View.AddSelectPopupColumn("RESOURCEID", new SqlQuery("GetConsumableDefList", "10001", "CONSUMABLECLASSID=RawMaterial", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                            .SetPopupLayout("CONSUMABLE", PopupButtonStyles.Ok_Cancel, true, false)
                                            .SetPopupResultCount(1)
                                            .SetLabel("CONSUMABLEDEFID")
                                            .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                            .SetPopupAutoFillColumns("CONSUMABLEDEFNAME")
                                            .SetValidationKeyColumn()
                                            .SetPopupResultMapping("RESOURCEID", "CONSUMABLEDEFID")
                                            .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                            {
                                                // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                                                // dataGridRow : 현재 Focus가 있는 그리드의 DataRow

                                                foreach (DataRow row in selectedRows)
                                                {
                                                    dataGridRow["RESOURCENAME"] = row["CONSUMABLEDEFNAME"].ToString();
                                                    dataGridRow["RESOURCEVERSION"] = row["CONSUMABLEDEFVERSION"].ToString();
                                                }

                                                _rowResource = dataGridRow;
                                                
                                            });

            consumableDefId.Conditions.AddTextBox("CONSUMABLEDEFID");

            consumableDefId.Conditions.AddTextBox("CONSUMABLEDEFNAME");

            // 팝업 그리드
            consumableDefId.GridColumns.AddTextBoxColumn("CONSUMABLEDEFID", 150);
            consumableDefId.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
            consumableDefId.GridColumns.AddTextBoxColumn("CONSUMABLEDEFVERSION", 200);
        }

        /// <summary>        
        /// 수입검사(원자재) 검사 방법 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdRawInspItemClass()
        {
            // TODO : 그리드 초기화 로직 추가
            grdRawInspItemClass.GridButtonItem = GridButtonItem.Add;
            grdRawInspItemClass.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            //Relation을 위해 파라미터로 넘겨줄 컬럼
            grdRawInspItemClass.View.AddTextBoxColumn("INSPECTIONCLASSID", 150 )
                .SetIsHidden();

            grdRawInspItemClass.View.AddTextBoxColumn("PARENTINSPITEMCLASSID", 150)
                .SetIsReadOnly();

            grdRawInspItemClass.View.AddTextBoxColumn("PARENTINSPITEMCLASSNAME", 200)
                .SetIsReadOnly();

            InitializeGrdRawInspItem_Popup();

            grdRawInspItemClass.View.AddTextBoxColumn("INSPITEMCLASSNAME", 200)
                .SetIsReadOnly();

            grdRawInspItemClass.View.PopulateColumns();
        }
        /// <summary>
        /// INSPITEMCLASSID를 검색하는 팝업
        /// </summary>
        private void InitializeGrdRawInspItem_Popup()
        {
            var inspItemclassId = grdRawInspItemClass.View.AddSelectPopupColumn("INSPITEMCLASSID", 150, new SqlQuery("GetInspItemByInspectionClassId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("INSPITEMCLASS", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetRelationIds("INSPECTIONCLASSID")
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetValidationKeyColumn()
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                    foreach (DataRow row in selectedRows)
                    {
                        dataGridRow["PARENTINSPITEMCLASSID"] = row["PARENTINSPITEMCLASSID"].ToString();
                        dataGridRow["PARENTINSPITEMCLASSNAME"] = row["PARENTINSPITEMCLASSNAME"].ToString();
                        dataGridRow["INSPITEMCLASSNAME"] = row["INSPITEMCLASSNAME"].ToString();
                    }

                    FocusedRowChange_grdInspItem(dataGridRow);
                });

            inspItemclassId.Conditions.AddTextBox("INSPITEMCLASSID");

            inspItemclassId.Conditions.AddTextBox("INSPITEMCLASSNAME");

            inspItemclassId.Conditions.AddTextBox("INSPECTIONCLASSID")
                .SetPopupDefaultByGridColumnId("INSPECTIONCLASSID")
                .SetIsHidden();

            inspItemclassId.GridColumns.AddTextBoxColumn("PARENTINSPITEMCLASSID", 150)
                .SetIsReadOnly();
            inspItemclassId.GridColumns.AddTextBoxColumn("PARENTINSPITEMCLASSNAME", 150)
                .SetIsReadOnly();
            inspItemclassId.GridColumns.AddTextBoxColumn("INSPITEMCLASSID", 150)
                .SetIsReadOnly();
            inspItemclassId.GridColumns.AddTextBoxColumn("INSPITEMCLASSNAME", 150)
                .SetIsReadOnly();
        }

        /// <summary>        
        /// 수입검사(원자재) 검사항목 연계 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdRawRel()
        {
            // TODO : 그리드 초기화 로직 추가
            grdRawRel.GridButtonItem = GridButtonItem.None;
            grdRawRel.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdRawRel.View.CheckMarkSelection.MultiSelectCount = 1;

            //inspectionItemRel, InspectionAttribute 테이블의 PK
            grdRawRel.View.AddTextBoxColumn("INSPITEMCLASSID", 150)
                .SetIsHidden();

            grdRawRel.View.AddTextBoxColumn("INSPITEMID", 150)
                .SetIsReadOnly();

            grdRawRel.View.AddTextBoxColumn("INSPITEMNAME", 150)
                .SetIsReadOnly();

            grdRawRel.View.AddTextBoxColumn("INSPITEMVERSION", 150)
                .SetIsReadOnly();

            grdRawRel.View.AddTextBoxColumn("INSPECTIONDEFID", 150)
                .SetIsHidden();

            grdRawRel.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 150)
                .SetIsHidden();

            grdRawRel.View.AddTextBoxColumn("RESOURCETYPE", 150)
                .SetIsHidden();

            grdRawRel.View.AddTextBoxColumn("RESOURCEID", 150)
                .SetIsHidden();

            grdRawRel.View.AddTextBoxColumn("RESOURCEVERSION", 150)
                .SetIsHidden();

            grdRawRel.View.AddTextBoxColumn("PLNATID", 100)
                .SetIsHidden();

            grdRawRel.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();

            //inspectionItemRel 테이블
            grdRawRel.View.AddTextBoxColumn("VALIDTYPE", 100);

            grdRawRel.View.AddSpinEditColumn("TARGETVALUE", 100);

            grdRawRel.View.AddSpinEditColumn("LOWERSPECLIMIT", 100);

            grdRawRel.View.AddSpinEditColumn("UPPERSPECLIMIT", 100);

            grdRawRel.View.AddSpinEditColumn("LOWERCONTROLLIMIT", 100);

            grdRawRel.View.AddSpinEditColumn("UPPERCONTROLLIMIT", 100);

            grdRawRel.View.AddSpinEditColumn("LOWERSCREENLIMIT", 100);

            grdRawRel.View.AddSpinEditColumn("UPPERSCREENLIMIT", 100);
            //inspectionItemRel 단위 CODECLASSID 수정
            grdRawRel.View.AddComboBoxColumn("UNIT", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            //InspectionAttribute 테이블
            grdRawRel.View.AddComboBoxColumn("HASSPECSEQUENCE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();

            grdRawRel.View.AddTextBoxColumn("SPECSEQUENCE", 150)
                .SetIsHidden();

            grdRawRel.View.AddTextBoxColumn("SPECCLASSID", 150)
                .SetIsHidden();

            //InspectionAttribute 검사필수여부 CODECLASSID 수정
            grdRawRel.View.AddComboBoxColumn("ISINSPECTIONREQUIRED", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdRawRel.View.AddSpinEditColumn("LOTSIZE", 100);

            grdRawRel.View.AddTextBoxColumn("QTYTYPE", 100);

            grdRawRel.View.AddTextBoxColumn("INSPECTIONLEVEL", 100);

            grdRawRel.View.AddSpinEditColumn("DEFECTLEVEL", 100);

            grdRawRel.View.AddSpinEditColumn("INSPECTIONQTY", 100);

            grdRawRel.View.AddTextBoxColumn("QTYUNIT", 100);

            grdRawRel.View.AddTextBoxColumn("CYCLE", 100);

            grdRawRel.View.AddTextBoxColumn("INSPECTORDEGREE", 100);

            grdRawRel.View.AddTextBoxColumn("EQUIPMENTID", 100);

            grdRawRel.View.AddTextBoxColumn("INSPECTIONUNIT", 100);

            grdRawRel.View.AddTextBoxColumn("DEFECTRATE", 100);

            grdRawRel.View.AddTextBoxColumn("DECISIONDEGREE", 100);


            grdRawRel.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdRawRel.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdRawRel.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdRawRel.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdRawRel.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdRawRel.View.PopulateColumns();
        }

        #endregion   

        #region 수입검사(원자재가공품) 탭의 그리드 초기화
        /// <summary>        
        /// 수입검사(원자재가공품) 검사 종류 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdSubassemblyInspection()
        {
            // TODO : 그리드 초기화 로직 추가
            grdSubassemblyInspection.GridButtonItem = GridButtonItem.None;
            grdSubassemblyInspection.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdSubassemblyInspection.View.SetIsReadOnly();

            grdSubassemblyInspection.View.AddTextBoxColumn("INSPECTIONCLASSID", 150);

            grdSubassemblyInspection.View.AddTextBoxColumn("INSPECTIONCLASSNAME", 200);

            grdSubassemblyInspection.View.AddTextBoxColumn("INSPECTIONDEFID", 150);

            grdSubassemblyInspection.View.AddTextBoxColumn("INSPECTIONDEFNAME", 200);

            grdSubassemblyInspection.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 200);

            grdSubassemblyInspection.View.AddTextBoxColumn("RESOURCETYPE", 200);

            grdSubassemblyInspection.View.PopulateColumns();
        }

        /// <summary>        
        /// 수입검사(원자재가공품) 검사품목 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdSubassemblyConsumable()
        {
            // TODO : 그리드 초기화 로직 추가
            grdSubassemblyConsumable.GridButtonItem = GridButtonItem.Add;
            grdSubassemblyConsumable.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            InitializeGrdSubassembly_ConsumableDefIdPopup();

            grdSubassemblyConsumable.View.AddTextBoxColumn("RESOURCENAME", 200)
                .SetLabel("CONSUMABLEDEFNAME")
                .SetIsReadOnly();

            grdSubassemblyConsumable.View.AddTextBoxColumn("RESOURCEVERSION", 150)
                .SetLabel("RESOURCEVERSION")
                .SetIsReadOnly();

            grdSubassemblyConsumable.View.AddTextBoxColumn("INSPECTORDEGREE", 150);

            grdSubassemblyConsumable.View.AddTextBoxColumn("INSPECTIONLEVEL", 150);

            grdSubassemblyConsumable.View.AddTextBoxColumn("DEFECTLEVEL", 150);

            grdSubassemblyConsumable.View.PopulateColumns();
        }

        /// <summary>
        /// 검사 품목을 선택하는 팝업
        /// </summary>
        private void InitializeGrdSubassembly_ConsumableDefIdPopup()
        {
            //팝업 컬럼 설정
            var consumableDefId = grdSubassemblyConsumable.View.AddSelectPopupColumn("RESOURCEID", new SqlQuery("GetConsumableDefList", "10001", "CONSUMABLECLASSID=RawAssy", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                            .SetPopupLayout("CONSUMABLE", PopupButtonStyles.Ok_Cancel, true, false)
                                            .SetPopupResultCount(1)
                                            .SetLabel("CONSUMABLEDEFID")
                                            .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                            .SetPopupAutoFillColumns("CONSUMABLEDEFNAME")
                                            .SetValidationKeyColumn()
                                            .SetPopupResultMapping("RESOURCEID", "CONSUMABLEDEFID")
                                            .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                            {
                                                // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                                                // dataGridRow : 현재 Focus가 있는 그리드의 DataRow

                                                foreach (DataRow row in selectedRows)
                                                {
                                                    dataGridRow["RESOURCENAME"] = row["CONSUMABLEDEFNAME"].ToString();
                                                    dataGridRow["RESOURCEVERSION"] = row["CONSUMABLEDEFVERSION"].ToString();
                                                }

                                                _rowResource = dataGridRow;

                                            });

            consumableDefId.Conditions.AddTextBox("CONSUMABLEDEFID");

            consumableDefId.Conditions.AddTextBox("CONSUMABLEDEFNAME");

            // 팝업 그리드
            consumableDefId.GridColumns.AddTextBoxColumn("CONSUMABLEDEFID", 150);
            consumableDefId.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
            consumableDefId.GridColumns.AddTextBoxColumn("CONSUMABLEDEFVERSION", 200);
        }

        /// <summary>        
        /// 수입검사(원자재가공품) 검사 방법 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdSubassemblyInspItemClass()
        {
            // TODO : 그리드 초기화 로직 추가
            grdSubassemblyInspItemClass.GridButtonItem = GridButtonItem.Add;
            grdSubassemblyInspItemClass.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            //Relation을 위해 파라미터로 넘겨줄 컬럼
            grdSubassemblyInspItemClass.View.AddTextBoxColumn("INSPECTIONCLASSID", 150)
                .SetIsHidden();

            grdSubassemblyInspItemClass.View.AddComboBoxColumn("INSPITEMCLASSID", 150, new SqlQuery("GetInspItemByInspectionClassId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}") , "INSPITEMCLASSID", "INSPITEMCLASSID")
                .SetRelationIds("INSPECTIONCLASSID")
                .SetValidationKeyColumn();

            grdSubassemblyInspItemClass.View.AddTextBoxColumn("INSPITEMCLASSNAME", 200)
                .SetIsReadOnly();

            grdSubassemblyInspItemClass.View.PopulateColumns();
        }  

        /// <summary>        
        /// 수입검사(원자재가공품) 검사항목 연계 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdSubassemblyRel()
        {
            // TODO : 그리드 초기화 로직 추가
            grdSubassemblyRel.GridButtonItem = GridButtonItem.None;
            grdSubassemblyRel.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdSubassemblyRel.View.CheckMarkSelection.MultiSelectCount = 1;

            //inspectionItemRel, InspectionAttribute 테이블의 PK
            grdSubassemblyRel.View.AddTextBoxColumn("INSPITEMCLASSID", 150)
                .SetIsHidden();

            grdSubassemblyRel.View.AddTextBoxColumn("INSPITEMID", 150)
                .SetIsReadOnly();

            grdSubassemblyRel.View.AddTextBoxColumn("INSPITEMNAME", 150)
                .SetIsReadOnly();

            grdSubassemblyRel.View.AddTextBoxColumn("INSPITEMVERSION", 150)
                .SetIsReadOnly();

            grdSubassemblyRel.View.AddTextBoxColumn("INSPECTIONDEFID", 150)
                .SetIsHidden();

            grdSubassemblyRel.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 150)
                .SetIsHidden();

            grdSubassemblyRel.View.AddTextBoxColumn("RESOURCETYPE", 150)
                .SetIsHidden();

            grdSubassemblyRel.View.AddTextBoxColumn("RESOURCEID", 150)
                .SetIsHidden();

            grdSubassemblyRel.View.AddTextBoxColumn("RESOURCEVERSION", 150)
                .SetIsHidden();

            grdSubassemblyRel.View.AddTextBoxColumn("PLNATID", 100)
                .SetIsHidden();

            grdSubassemblyRel.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();

            //inspectionItemRel 테이블
            grdSubassemblyRel.View.AddTextBoxColumn("VALIDTYPE", 100);

            grdSubassemblyRel.View.AddSpinEditColumn("TARGETVALUE", 100);

            grdSubassemblyRel.View.AddSpinEditColumn("LOWERSPECLIMIT", 100);

            grdSubassemblyRel.View.AddSpinEditColumn("UPPERSPECLIMIT", 100);

            grdSubassemblyRel.View.AddSpinEditColumn("LOWERCONTROLLIMIT", 100);

            grdSubassemblyRel.View.AddSpinEditColumn("UPPERCONTROLLIMIT", 100);

            grdSubassemblyRel.View.AddSpinEditColumn("LOWERSCREENLIMIT", 100);

            grdSubassemblyRel.View.AddSpinEditColumn("UPPERSCREENLIMIT", 100);
            //inspectionItemRel 단위 CODECLASSID 수정
            grdSubassemblyRel.View.AddComboBoxColumn("UNIT", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            //InspectionAttribute 테이블
            grdSubassemblyRel.View.AddComboBoxColumn("HASSPECSEQUENCE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();

            grdSubassemblyRel.View.AddTextBoxColumn("SPECSEQUENCE", 150)
                .SetIsHidden();

            grdSubassemblyRel.View.AddTextBoxColumn("SPECCLASSID", 150)
                .SetIsHidden();

            //InspectionAttribute 검사필수여부 CODECLASSID 수정
            grdSubassemblyRel.View.AddComboBoxColumn("ISINSPECTIONREQUIRED", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdSubassemblyRel.View.AddSpinEditColumn("LOTSIZE", 100);

            grdSubassemblyRel.View.AddTextBoxColumn("QTYTYPE", 100);

            grdSubassemblyRel.View.AddSpinEditColumn("INSPECTIONQTY", 100);

            grdSubassemblyRel.View.AddTextBoxColumn("QTYUNIT", 100);

            grdSubassemblyRel.View.AddTextBoxColumn("CYCLE", 100);

            grdSubassemblyRel.View.AddTextBoxColumn("EQUIPMENTID", 100);

            grdSubassemblyRel.View.AddTextBoxColumn("INSPECTIONUNIT", 100);

            grdSubassemblyRel.View.AddTextBoxColumn("DEFECTRATE", 100);

            grdSubassemblyRel.View.AddTextBoxColumn("DECISIONDEGREE", 100);

            grdSubassemblyRel.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdSubassemblyRel.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSubassemblyRel.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSubassemblyRel.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSubassemblyRel.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSubassemblyRel.View.PopulateColumns();
        }

        /// <summary>
        /// 수입검사(원자재가공품) 측정시기 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdSubassemblyPoint()
        {
            grdSubassemblyPoint.GridButtonItem = GridButtonItem.Add;
            grdSubassemblyPoint.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            //PK
            grdSubassemblyPoint.View.AddTextBoxColumn("INSPECTIONPOINTID",100)
                .SetIsHidden();

            grdSubassemblyPoint.View.AddTextBoxColumn("INDPITEMID", 100)
                .SetIsHidden();

            grdSubassemblyPoint.View.AddTextBoxColumn("INSPITEMVERSION", 100)
                .SetIsHidden();

            grdSubassemblyPoint.View.AddTextBoxColumn("INSPITEMCLASSID", 100)
                .SetIsHidden();

            grdSubassemblyPoint.View.AddTextBoxColumn("INSPECTIONDEFID", 100)
                .SetIsHidden();

            grdSubassemblyPoint.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 100)
                .SetIsHidden();

            grdSubassemblyPoint.View.AddTextBoxColumn("RESOURCEID", 100)
                .SetIsHidden();

            grdSubassemblyPoint.View.AddTextBoxColumn("RESOURCEVERSION", 100)
                .SetIsHidden();

            grdSubassemblyPoint.View.AddTextBoxColumn("RESOURCETYPE", 100)
                .SetIsHidden();

            grdSubassemblyPoint.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();

            grdSubassemblyPoint.View.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden();
            
            grdSubassemblyPoint.View.AddComboBoxColumn("INSPECTIONPOINTNAME", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspectionPointName", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdSubassemblyPoint.View.AddSpinEditColumn("INSPECTIONQTY", 100);

            grdSubassemblyPoint.View.AddSpinEditColumn("POINTQTY", 100);

            grdSubassemblyPoint.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetValidationIsRequired()
               .SetDefault("Valid")
               .SetTextAlignment(TextAlignment.Center);

            grdSubassemblyPoint.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSubassemblyPoint.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSubassemblyPoint.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSubassemblyPoint.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);


            grdSubassemblyPoint.View.PopulateColumns();
        }
        #endregion

        #region 수입검사(부자재/포장재) 탭의 그리드 초기화
        /// <summary>        
        /// 수입검사(부자재/포장재) 검사 종류 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdSubsidiaryInspection()
        {
            // TODO : 그리드 초기화 로직 추가
            grdSubsidiaryInspection.GridButtonItem = GridButtonItem.None;
            grdSubsidiaryInspection.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdSubsidiaryInspection.View.SetIsReadOnly();

            grdSubsidiaryInspection.View.AddTextBoxColumn("INSPECTIONCLASSID", 150);

            grdSubsidiaryInspection.View.AddTextBoxColumn("INSPECTIONCLASSNAME", 200);

            grdSubsidiaryInspection.View.AddTextBoxColumn("INSPECTIONDEFID", 150);

            grdSubsidiaryInspection.View.AddTextBoxColumn("INSPECTIONDEFNAME", 200);

            grdSubsidiaryInspection.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 200);

            grdSubsidiaryInspection.View.AddTextBoxColumn("RESOURCETYPE", 200);

            grdSubsidiaryInspection.View.PopulateColumns();
        }

        /// <summary>        
        /// 수입검사(부자재/포장재) 검사품목 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdSubsidiaryConsumable()
        {
            // TODO : 그리드 초기화 로직 추가
            grdSubsidiaryConsumable.GridButtonItem = GridButtonItem.Add;
            grdSubsidiaryConsumable.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            InitializeGrdSubsidiary_ConsumableDefIdPopup();

            grdSubsidiaryConsumable.View.AddTextBoxColumn("RESOURCENAME", 200)
                .SetLabel("CONSUMABLEDEFNAME")
                .SetIsReadOnly();

            grdSubsidiaryConsumable.View.AddTextBoxColumn("RESOURCEVERSION", 150)
                .SetLabel("RESOURCEVERSION")
                .SetIsReadOnly();

            grdSubsidiaryConsumable.View.AddTextBoxColumn("INSPECTORDEGREE", 150);

            grdSubsidiaryConsumable.View.AddTextBoxColumn("INSPECTIONLEVEL", 150);

            grdSubsidiaryConsumable.View.AddTextBoxColumn("DEFECTLEVEL", 150);

            grdSubsidiaryConsumable.View.PopulateColumns();
        }

        /// <summary>
        /// 검사 품목을 선택하는 팝업
        /// </summary>
        private void InitializeGrdSubsidiary_ConsumableDefIdPopup()
        {
            //팝업 컬럼 설정
            var consumableDefId = grdSubsidiaryConsumable.View.AddSelectPopupColumn("RESOURCEID", new SqlQuery("GetConsumableDefList", "10001", "CONSUMABLECLASSID=Subsidiary", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                            .SetPopupLayout("CONSUMABLE", PopupButtonStyles.Ok_Cancel, true, false)
                                            .SetPopupResultCount(1)
                                            .SetLabel("CONSUMABLEDEFID")
                                            .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                            .SetPopupAutoFillColumns("CONSUMABLEDEFNAME")
                                            .SetValidationKeyColumn()
                                            .SetPopupResultMapping("RESOURCEID", "CONSUMABLEDEFID")
                                            .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                            {
                                                // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                                                // dataGridRow : 현재 Focus가 있는 그리드의 DataRow

                                                foreach (DataRow row in selectedRows)
                                                {
                                                    dataGridRow["RESOURCENAME"] = row["CONSUMABLEDEFNAME"].ToString();
                                                    dataGridRow["RESOURCEVERSION"] = row["CONSUMABLEDEFVERSION"].ToString();
                                                }

                                                _rowResource = dataGridRow;

                                            });

            consumableDefId.Conditions.AddTextBox("CONSUMABLEDEFID");

            consumableDefId.Conditions.AddTextBox("CONSUMABLEDEFNAME");

            // 팝업 그리드
            consumableDefId.GridColumns.AddTextBoxColumn("CONSUMABLEDEFID", 150);
            consumableDefId.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
            consumableDefId.GridColumns.AddTextBoxColumn("CONSUMABLEDEFVERSION", 200);
        }

        /// <summary>        
        /// 수입검사(부자재/포장재) 검사 방법 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdSubsidiaryInspItemClass()
        {
            // TODO : 그리드 초기화 로직 추가
            grdSubsidiaryInspItemClass.GridButtonItem = GridButtonItem.Add;
            grdSubsidiaryInspItemClass.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            //Relation을 위해 파라미터로 넘겨줄 컬럼
            grdSubsidiaryInspItemClass.View.AddTextBoxColumn("INSPECTIONCLASSID", 150)
                .SetIsHidden();

            grdSubsidiaryInspItemClass.View.AddComboBoxColumn("INSPITEMCLASSID", 150, new SqlQuery("GetInspItemByInspectionClassId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPITEMCLASSID", "INSPITEMCLASSID")
                .SetRelationIds("INSPECTIONCLASSID")
                .SetValidationKeyColumn();

            grdSubsidiaryInspItemClass.View.AddTextBoxColumn("INSPITEMCLASSNAME", 200)
                .SetIsReadOnly();

            grdSubsidiaryInspItemClass.View.PopulateColumns();
        }

        /// <summary>        
        /// 수입검사(부자재/포장재) 검사항목 연계 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdSubsidiaryRel()
        {
            // TODO : 그리드 초기화 로직 추가
            grdSubsidiaryRel.GridButtonItem = GridButtonItem.None;
            grdSubsidiaryRel.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdSubsidiaryRel.View.CheckMarkSelection.MultiSelectCount = 1;

            //inspectionItemRel, InspectionAttribute 테이블의 PK
            grdSubsidiaryRel.View.AddTextBoxColumn("INSPITEMCLASSID", 150)
                .SetIsHidden();

            grdSubsidiaryRel.View.AddTextBoxColumn("INSPITEMID", 150)
                .SetIsReadOnly();

            grdSubsidiaryRel.View.AddTextBoxColumn("INSPITEMNAME", 150)
                .SetIsReadOnly();

            grdSubsidiaryRel.View.AddTextBoxColumn("INSPITEMVERSION", 150)
                .SetIsReadOnly();

            grdSubsidiaryRel.View.AddTextBoxColumn("INSPECTIONDEFID", 150)
                .SetIsHidden();

            grdSubsidiaryRel.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 150)
                .SetIsHidden();

            grdSubsidiaryRel.View.AddTextBoxColumn("RESOURCETYPE", 150)
                .SetIsHidden();

            grdSubsidiaryRel.View.AddTextBoxColumn("RESOURCEID", 150)
                .SetIsHidden();

            grdSubsidiaryRel.View.AddTextBoxColumn("RESOURCEVERSION", 150)
                .SetIsHidden();

            grdSubsidiaryRel.View.AddTextBoxColumn("PLNATID", 100)
              .SetIsHidden();

            grdSubsidiaryRel.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();

            //inspectionItemRel 테이블
            grdSubsidiaryRel.View.AddTextBoxColumn("VALIDTYPE", 100);

            grdSubsidiaryRel.View.AddSpinEditColumn("TARGETVALUE", 100);

            grdSubsidiaryRel.View.AddSpinEditColumn("LOWERSPECLIMIT", 100);

            grdSubsidiaryRel.View.AddSpinEditColumn("UPPERSPECLIMIT", 100);

            grdSubsidiaryRel.View.AddSpinEditColumn("LOWERCONTROLLIMIT", 100);

            grdSubsidiaryRel.View.AddSpinEditColumn("UPPERCONTROLLIMIT", 100);

            grdSubsidiaryRel.View.AddSpinEditColumn("LOWERSCREENLIMIT", 100);

            grdSubsidiaryRel.View.AddSpinEditColumn("UPPERSCREENLIMIT", 100);
            //inspectionItemRel 단위 CODECLASSID 수정
            grdSubsidiaryRel.View.AddComboBoxColumn("UNIT", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            //InspectionAttribute 테이블
            grdSubsidiaryRel.View.AddComboBoxColumn("HASSPECSEQUENCE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();

            grdSubsidiaryRel.View.AddTextBoxColumn("SPECSEQUENCE", 150)
                .SetIsHidden();

            grdSubsidiaryRel.View.AddTextBoxColumn("SPECCLASSID", 150)
                .SetIsHidden();

            //InspectionAttribute 검사필수여부 CODECLASSID 수정
            grdSubsidiaryRel.View.AddComboBoxColumn("ISINSPECTIONREQUIRED", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdSubsidiaryRel.View.AddSpinEditColumn("LOTSIZE", 100);

            grdSubsidiaryRel.View.AddTextBoxColumn("QTYTYPE", 100);

            grdSubsidiaryRel.View.AddSpinEditColumn("INSPECTIONQTY", 100);

            grdSubsidiaryRel.View.AddTextBoxColumn("QTYUNIT", 100);

            grdSubsidiaryRel.View.AddTextBoxColumn("CYCLE", 100);

            grdSubsidiaryRel.View.AddTextBoxColumn("EQUIPMENTID", 100);

            grdSubsidiaryRel.View.AddTextBoxColumn("INSPECTIONUNIT", 100);

            grdSubsidiaryRel.View.AddTextBoxColumn("DEFECTRATE", 100);

            grdSubsidiaryRel.View.AddTextBoxColumn("DECISIONDEGREE", 100);

            grdSubsidiaryRel.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdSubsidiaryRel.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSubsidiaryRel.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSubsidiaryRel.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSubsidiaryRel.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSubsidiaryRel.View.PopulateColumns();
        }

        /// <summary>
        /// 수입검사(부자재/포장재) 측정시기 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdSubsidiaryPoint()
        {
            grdSubsidiaryPoint.GridButtonItem = GridButtonItem.Add;
            grdSubsidiaryPoint.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            //PK
            grdSubsidiaryPoint.View.AddTextBoxColumn("INSPECTIONPOINTID", 100)
                .SetIsHidden();

            grdSubsidiaryPoint.View.AddTextBoxColumn("INDPITEMID", 100)
                .SetIsHidden();

            grdSubsidiaryPoint.View.AddTextBoxColumn("INSPITEMVERSION", 100)
                .SetIsHidden();

            grdSubsidiaryPoint.View.AddTextBoxColumn("INSPITEMCLASSID", 100)
                .SetIsHidden();

            grdSubsidiaryPoint.View.AddTextBoxColumn("INSPECTIONDEFID", 100)
                .SetIsHidden();

            grdSubsidiaryPoint.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 100)
                .SetIsHidden();

            grdSubsidiaryPoint.View.AddTextBoxColumn("RESOURCEID", 100)
                .SetIsHidden();

            grdSubsidiaryPoint.View.AddTextBoxColumn("RESOURCEVERSION", 100)
                .SetIsHidden();

            grdSubsidiaryPoint.View.AddTextBoxColumn("RESOURCETYPE", 100)
                .SetIsHidden();

            grdSubsidiaryPoint.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();

            grdSubsidiaryPoint.View.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden();

            grdSubsidiaryPoint.View.AddComboBoxColumn("INSPECTIONPOINTNAME", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspectionPointName", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdSubsidiaryPoint.View.AddSpinEditColumn("INSPECTIONQTY", 100);

            grdSubsidiaryPoint.View.AddSpinEditColumn("POINTQTY", 100);

            grdSubsidiaryPoint.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetValidationIsRequired()
               .SetDefault("Valid")
               .SetTextAlignment(TextAlignment.Center);

            grdSubsidiaryPoint.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSubsidiaryPoint.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSubsidiaryPoint.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSubsidiaryPoint.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);


            grdSubsidiaryPoint.View.PopulateColumns();
        }
        #endregion

        #region 수입검사(공정) 탭의 그리드 초기화
        /// <summary>        
        /// 수입검사(공정) 검사 종류 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdOSPInspection()
        {
            // TODO : 그리드 초기화 로직 추가
            grdOSPInspection.GridButtonItem = GridButtonItem.None;
            grdOSPInspection.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdOSPInspection.View.SetIsReadOnly();

            grdOSPInspection.View.AddTextBoxColumn("INSPECTIONCLASSID", 150);

            grdOSPInspection.View.AddTextBoxColumn("INSPECTIONCLASSNAME", 200);

            grdOSPInspection.View.AddTextBoxColumn("INSPECTIONDEFID", 150);

            grdOSPInspection.View.AddTextBoxColumn("INSPECTIONDEFNAME", 200);

            grdOSPInspection.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 200);

            grdOSPInspection.View.AddTextBoxColumn("RESOURCETYPE", 200);

            //변경**
            grdOSPInspection.View.AddTextBoxColumn("INSPECTIONDEFSEGMENT", 200);

            grdOSPInspection.View.PopulateColumns();
        }

        /// <summary>        
        /// 출하 검사 검사품목 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdOSPProduct()
        {
            // TODO : 그리드 초기화 로직 추가
            grdOSPProduct.GridButtonItem = GridButtonItem.Add;
            grdOSPProduct.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            InitializegrdOSPProduct_ProductDefIdPopup();

            grdOSPProduct.View.AddTextBoxColumn("RESOURCENAME", 200)
                .SetLabel("CONSUMABLEDEFNAME")
                .SetIsReadOnly();

            grdOSPProduct.View.AddTextBoxColumn("RESOURCEVERSION", 150)
                .SetLabel("RESOURCEVERSION")
                .SetIsReadOnly();

            grdOSPProduct.View.AddTextBoxColumn("QTYTYPE", 100);

            grdOSPProduct.View.AddTextBoxColumn("INSPECTORDEGREE", 150);

            grdOSPProduct.View.AddTextBoxColumn("INSPECTIONLEVEL", 150);

            grdOSPProduct.View.AddTextBoxColumn("DEFECTLEVEL", 150);

            grdOSPProduct.View.PopulateColumns();
        }

        /// <summary>
        /// 검사 품목을 선택하는 팝업
        /// </summary>
        private void InitializegrdOSPProduct_ProductDefIdPopup()
        {
            //팝업 컬럼 설정
            var productDefId = grdOSPProduct.View.AddSelectPopupColumn("RESOURCEID", new SqlQuery("GetProductDefList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                            .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, false)
                                            .SetPopupResultCount(1)
                                            .SetLabel("PRODUCTDEFID")
                                            .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                            .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                                            .SetValidationKeyColumn()
                                            .SetPopupResultMapping("RESOURCEID", "PRODUCTDEFID")
                                            .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                            {
                                                // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                                                // dataGridRow : 현재 Focus가 있는 그리드의 DataRow

                                                foreach (DataRow row in selectedRows)
                                                {
                                                    dataGridRow["RESOURCENAME"] = row["PRODUCTDEFNAME"].ToString();
                                                    dataGridRow["RESOURCEVERSION"] = row["PRODUCTDEFVERSION"].ToString();
                                                }

                                                _rowResource = dataGridRow;

                                            });

            productDefId.Conditions.AddTextBox("PRODUCTDEFID");
            productDefId.Conditions.AddTextBox("PRODUCTDEFNAME");

            // 팝업 그리드
            productDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            productDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            productDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 200);
        }


        /// <summary>        
        /// 수입검사(공정) 검사 방법 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdOSPInspItemClass()
        {
            // TODO : 그리드 초기화 로직 추가
            grdOSPInspItemClass.GridButtonItem = GridButtonItem.Add;
            grdOSPInspItemClass.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            //Relation을 위해 파라미터로 넘겨줄 컬럼
            grdOSPInspItemClass.View.AddTextBoxColumn("INSPECTIONCLASSID", 150)
                .SetIsHidden();

            grdOSPInspItemClass.View.AddComboBoxColumn("INSPITEMCLASSID", 150, new SqlQuery("GetInspItemByInspectionClassId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPITEMCLASSID", "INSPITEMCLASSID")
                .SetRelationIds("INSPECTIONCLASSID")
                .SetValidationKeyColumn();

            grdOSPInspItemClass.View.AddTextBoxColumn("INSPITEMCLASSNAME", 200)
                .SetIsReadOnly();

            grdOSPInspItemClass.View.PopulateColumns();
        }

        /// <summary>        
        /// 수입검사(공정) 검사항목 연계 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdOSPRel()
        {
            // TODO : 그리드 초기화 로직 추가
            grdOSPRel.GridButtonItem = GridButtonItem.None;
            grdOSPRel.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdOSPRel.View.CheckMarkSelection.MultiSelectCount = 1;

            //inspectionItemRel, InspectionAttribute 테이블의 PK
            grdOSPRel.View.AddTextBoxColumn("INSPITEMCLASSID", 150)
                .SetIsHidden();

            grdOSPRel.View.AddTextBoxColumn("INSPITEMID", 150)
                .SetIsReadOnly();

            grdOSPRel.View.AddTextBoxColumn("INSPITEMNAME", 150)
                .SetIsReadOnly();

            grdOSPRel.View.AddTextBoxColumn("INSPITEMVERSION", 150)
                .SetIsReadOnly();

            grdOSPRel.View.AddTextBoxColumn("INSPECTIONDEFID", 150)
                .SetIsHidden();

            grdOSPRel.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 150)
                .SetIsHidden();

            grdOSPRel.View.AddTextBoxColumn("RESOURCETYPE", 150)
                .SetIsHidden();

            grdOSPRel.View.AddTextBoxColumn("RESOURCEID", 150)
                .SetIsHidden();

            grdOSPRel.View.AddTextBoxColumn("RESOURCEVERSION", 150)
                .SetIsHidden();

            grdOSPRel.View.AddTextBoxColumn("PLNATID", 100)
               .SetIsHidden();

            grdOSPRel.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();

            //inspectionItemRel 테이블
            grdOSPRel.View.AddTextBoxColumn("VALIDTYPE", 100);

            grdOSPRel.View.AddSpinEditColumn("TARGETVALUE", 100);

            grdOSPRel.View.AddSpinEditColumn("LOWERSPECLIMIT", 100);

            grdOSPRel.View.AddSpinEditColumn("UPPERSPECLIMIT", 100);

            grdOSPRel.View.AddSpinEditColumn("LOWERCONTROLLIMIT", 100);

            grdOSPRel.View.AddSpinEditColumn("UPPERCONTROLLIMIT", 100);

            grdOSPRel.View.AddSpinEditColumn("LOWERSCREENLIMIT", 100);

            grdOSPRel.View.AddSpinEditColumn("UPPERSCREENLIMIT", 100);
            //inspectionItemRel 단위 CODECLASSID 수정
            grdOSPRel.View.AddComboBoxColumn("UNIT", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            //InspectionAttribute 테이블
            grdOSPRel.View.AddComboBoxColumn("HASSPECSEQUENCE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();

            grdOSPRel.View.AddTextBoxColumn("SPECSEQUENCE", 150)
                .SetIsHidden();

            grdOSPRel.View.AddTextBoxColumn("SPECCLASSID", 150)
                .SetIsHidden();

            //InspectionAttribute 검사필수여부 CODECLASSID 수정
            grdOSPRel.View.AddComboBoxColumn("ISINSPECTIONREQUIRED", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdOSPRel.View.AddSpinEditColumn("LOTSIZE", 100);

            grdOSPRel.View.AddTextBoxColumn("QTYTYPE", 100);

            grdOSPRel.View.AddTextBoxColumn("INSPECTIONLEVEL", 100);

            grdOSPRel.View.AddSpinEditColumn("DEFECTLEVEL", 100);

            grdOSPRel.View.AddSpinEditColumn("INSPECTIONQTY", 100);

            grdOSPRel.View.AddTextBoxColumn("QTYUNIT", 100);

            grdOSPRel.View.AddTextBoxColumn("CYCLE", 100);

            grdOSPRel.View.AddTextBoxColumn("INSPECTORDEGREE", 100);

            grdOSPRel.View.AddTextBoxColumn("EQUIPMENTID", 100);

            grdOSPRel.View.AddTextBoxColumn("INSPECTIONUNIT", 100);

            grdOSPRel.View.AddTextBoxColumn("DEFECTRATE", 100);

            grdOSPRel.View.AddTextBoxColumn("DECISIONDEGREE", 100);

            grdOSPRel.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdOSPRel.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdOSPRel.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdOSPRel.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdOSPRel.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdOSPRel.View.PopulateColumns();
        }

        /// <summary>
        /// 수입검사(공정) 측정시기 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdOSPPoint()
        {
            grdOSPPoint.GridButtonItem = GridButtonItem.Add;
            grdOSPPoint.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            //PK
            grdOSPPoint.View.AddTextBoxColumn("INSPECTIONPOINTID", 100)
                .SetIsHidden();

            grdOSPPoint.View.AddTextBoxColumn("INDPITEMID", 100)
                .SetIsHidden();

            grdOSPPoint.View.AddTextBoxColumn("INSPITEMVERSION", 100)
                .SetIsHidden();

            grdOSPPoint.View.AddTextBoxColumn("INSPITEMCLASSID", 100)
                .SetIsHidden();

            grdOSPPoint.View.AddTextBoxColumn("INSPECTIONDEFID", 100)
                .SetIsHidden();

            grdOSPPoint.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 100)
                .SetIsHidden();

            grdOSPPoint.View.AddTextBoxColumn("RESOURCEID", 100)
                .SetIsHidden();

            grdOSPPoint.View.AddTextBoxColumn("RESOURCEVERSION", 100)
                .SetIsHidden();

            grdOSPPoint.View.AddTextBoxColumn("RESOURCETYPE", 100)
                .SetIsHidden();

            grdOSPPoint.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();

            grdOSPPoint.View.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden();

            grdOSPPoint.View.AddComboBoxColumn("INSPECTIONPOINTNAME", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspectionPointName", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdOSPPoint.View.AddSpinEditColumn("INSPECTIONQTY", 100);

            grdOSPPoint.View.AddSpinEditColumn("POINTQTY", 100);

            grdOSPPoint.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetValidationIsRequired()
               .SetDefault("Valid")
               .SetTextAlignment(TextAlignment.Center);

            grdOSPPoint.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdOSPPoint.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdOSPPoint.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdOSPPoint.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);


            grdOSPPoint.View.PopulateColumns();
        }
        #endregion

        #region 자주검사(입고) 탭의 그리드 초기화
        /// <summary>        
        /// 자주검사(입고) 검사 종류 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdSelfTakeInspection()
        {
            // TODO : 그리드 초기화 로직 추가
            grdSelfTakeInspection.GridButtonItem = GridButtonItem.None;
            grdSelfTakeInspection.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdSelfTakeInspection.View.SetIsReadOnly();

            grdSelfTakeInspection.View.AddTextBoxColumn("INSPECTIONCLASSID", 150);

            grdSelfTakeInspection.View.AddTextBoxColumn("INSPECTIONCLASSNAME", 200);

            grdSelfTakeInspection.View.AddTextBoxColumn("INSPECTIONDEFID", 150);

            grdSelfTakeInspection.View.AddTextBoxColumn("INSPECTIONDEFNAME", 200);

            grdSelfTakeInspection.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 200);

            grdSelfTakeInspection.View.AddTextBoxColumn("RESOURCETYPE", 200);

            //변경**
            grdSelfTakeInspection.View.AddTextBoxColumn("INSPECTIONDEFSEGMENT", 200);

            grdSelfTakeInspection.View.PopulateColumns();
        }
        /// <summary>        
        /// 자주검사(입고) 검사 방법 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdSelfTakeInspItemClass()
        {
            // TODO : 그리드 초기화 로직 추가
            grdSelfTakeInspItemClass.GridButtonItem = GridButtonItem.Add;
            grdSelfTakeInspItemClass.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            //Relation을 위해 파라미터로 넘겨줄 컬럼
            grdSelfTakeInspItemClass.View.AddTextBoxColumn("INSPECTIONCLASSID", 150)
                .SetIsHidden();

            grdSelfTakeInspItemClass.View.AddComboBoxColumn("INSPITEMCLASSID", 150, new SqlQuery("GetInspItemByInspectionClassId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPITEMCLASSID", "INSPITEMCLASSID")
                .SetRelationIds("INSPECTIONCLASSID")
                .SetValidationKeyColumn();

            grdSelfTakeInspItemClass.View.AddTextBoxColumn("INSPITEMCLASSNAME", 200)
                .SetIsReadOnly();

            grdSelfTakeInspItemClass.View.PopulateColumns();
        }

        /// <summary>        
        /// 자주검사(입고) 검사항목 연계 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdSelfTakeRel()
        {
            // TODO : 그리드 초기화 로직 추가
            grdSelfTakeRel.GridButtonItem = GridButtonItem.None;
            grdSelfTakeRel.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            grdSelfTakeRel.View.CheckMarkSelection.MultiSelectCount = 1;

            //inspectionItemRel, InspectionAttribute 테이블의 PK
            grdSelfTakeRel.View.AddTextBoxColumn("INSPITEMCLASSID", 150)
                .SetIsHidden();

            grdSelfTakeRel.View.AddTextBoxColumn("INSPITEMID", 150)
                .SetIsReadOnly();

            grdSelfTakeRel.View.AddTextBoxColumn("INSPITEMNAME", 150)
                .SetIsReadOnly();

            grdSelfTakeRel.View.AddTextBoxColumn("INSPITEMVERSION", 150)
                .SetIsReadOnly();

            grdSelfTakeRel.View.AddTextBoxColumn("INSPECTIONDEFID", 150)
                .SetIsHidden();

            grdSelfTakeRel.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 150)
                .SetIsHidden();

            grdSelfTakeRel.View.AddTextBoxColumn("RESOURCETYPE", 150)
                .SetIsHidden();

            grdSelfTakeRel.View.AddTextBoxColumn("RESOURCEID", 150)
                .SetIsHidden();

            grdSelfTakeRel.View.AddTextBoxColumn("RESOURCEVERSION", 150)
                .SetIsHidden();

            grdSelfTakeRel.View.AddTextBoxColumn("PLNATID", 100)
               .SetIsHidden();

            grdSelfTakeRel.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();

            //inspectionItemRel 테이블
            grdSelfTakeRel.View.AddTextBoxColumn("VALIDTYPE", 100);

            grdSelfTakeRel.View.AddSpinEditColumn("TARGETVALUE", 100);

            grdSelfTakeRel.View.AddSpinEditColumn("LOWERSPECLIMIT", 100);

            grdSelfTakeRel.View.AddSpinEditColumn("UPPERSPECLIMIT", 100);

            grdSelfTakeRel.View.AddSpinEditColumn("LOWERCONTROLLIMIT", 100);

            grdSelfTakeRel.View.AddSpinEditColumn("UPPERCONTROLLIMIT", 100);

            grdSelfTakeRel.View.AddSpinEditColumn("LOWERSCREENLIMIT", 100);

            grdSelfTakeRel.View.AddSpinEditColumn("UPPERSCREENLIMIT", 100);
            //inspectionItemRel 단위 CODECLASSID 수정
            grdSelfTakeRel.View.AddComboBoxColumn("UNIT", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            //InspectionAttribute 테이블
            grdSelfTakeRel.View.AddComboBoxColumn("HASSPECSEQUENCE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();

            grdSelfTakeRel.View.AddTextBoxColumn("SPECSEQUENCE", 150)
                .SetIsHidden();

            grdSelfTakeRel.View.AddTextBoxColumn("SPECCLASSID", 150)
                .SetIsHidden();

            //InspectionAttribute 검사필수여부 CODECLASSID 수정
            grdSelfTakeRel.View.AddComboBoxColumn("ISINSPECTIONREQUIRED", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdSelfTakeRel.View.AddSpinEditColumn("LOTSIZE", 100);

            grdSelfTakeRel.View.AddTextBoxColumn("QTYTYPE", 100);

            grdSelfTakeRel.View.AddTextBoxColumn("INSPECTIONLEVEL", 100);

            grdSelfTakeRel.View.AddSpinEditColumn("DEFECTLEVEL", 100);

            grdSelfTakeRel.View.AddSpinEditColumn("INSPECTIONQTY", 100);

            grdSelfTakeRel.View.AddTextBoxColumn("QTYUNIT", 100);

            grdSelfTakeRel.View.AddTextBoxColumn("CYCLE", 100);

            grdSelfTakeRel.View.AddTextBoxColumn("INSPECTORDEGREE", 100);

            grdSelfTakeRel.View.AddTextBoxColumn("EQUIPMENTID", 100);

            grdSelfTakeRel.View.AddTextBoxColumn("INSPECTIONUNIT", 100);

            grdSelfTakeRel.View.AddTextBoxColumn("DEFECTRATE", 100);

            grdSelfTakeRel.View.AddTextBoxColumn("DECISIONDEGREE", 100);

            grdSelfTakeRel.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdSelfTakeRel.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSelfTakeRel.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSelfTakeRel.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSelfTakeRel.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSelfTakeRel.View.PopulateColumns();
        }

        /// <summary>
        /// 자주검사(입고) 측정시기 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdSelfTakePoint()
        {
            grdSelfTakePoint.GridButtonItem = GridButtonItem.Add;
            grdSelfTakePoint.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            //PK
            grdSelfTakePoint.View.AddTextBoxColumn("INSPECTIONPOINTID", 100)
                .SetIsHidden();

            grdSelfTakePoint.View.AddTextBoxColumn("INDPITEMID", 100)
                .SetIsHidden();

            grdSelfTakePoint.View.AddTextBoxColumn("INSPITEMVERSION", 100)
                .SetIsHidden();

            grdSelfTakePoint.View.AddTextBoxColumn("INSPITEMCLASSID", 100)
                .SetIsHidden();

            grdSelfTakePoint.View.AddTextBoxColumn("INSPECTIONDEFID", 100)
                .SetIsHidden();

            grdSelfTakePoint.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 100)
                .SetIsHidden();

            grdSelfTakePoint.View.AddTextBoxColumn("RESOURCEID", 100)
                .SetIsHidden();

            grdSelfTakePoint.View.AddTextBoxColumn("RESOURCEVERSION", 100)
                .SetIsHidden();

            grdSelfTakePoint.View.AddTextBoxColumn("RESOURCETYPE", 100)
                .SetIsHidden();

            grdSelfTakePoint.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();

            grdSelfTakePoint.View.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden();

            grdSelfTakePoint.View.AddComboBoxColumn("INSPECTIONPOINTNAME", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspectionPointName", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdSelfTakePoint.View.AddSpinEditColumn("INSPECTIONQTY", 100);

            grdSelfTakePoint.View.AddSpinEditColumn("POINTQTY", 100);

            grdSelfTakePoint.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetValidationIsRequired()
               .SetDefault("Valid")
               .SetTextAlignment(TextAlignment.Center);

            grdSelfTakePoint.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSelfTakePoint.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSelfTakePoint.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSelfTakePoint.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);


            grdSelfTakePoint.View.PopulateColumns();
        }
        #endregion

        #region 자주검사(출고) 탭의 그리드 초기화
        /// <summary>        
        /// 자주검사(출고) 검사 종류 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdSelfShipInspection()
        {
            // TODO : 그리드 초기화 로직 추가
            grdSelfShipInspection.GridButtonItem = GridButtonItem.None;
            grdSelfShipInspection.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdSelfShipInspection.View.SetIsReadOnly();

            grdSelfShipInspection.View.AddTextBoxColumn("INSPECTIONCLASSID", 150);

            grdSelfShipInspection.View.AddTextBoxColumn("INSPECTIONCLASSNAME", 200);

            grdSelfShipInspection.View.AddTextBoxColumn("INSPECTIONDEFID", 150);

            grdSelfShipInspection.View.AddTextBoxColumn("INSPECTIONDEFNAME", 200);

            grdSelfShipInspection.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 200);

            grdSelfShipInspection.View.AddTextBoxColumn("RESOURCETYPE", 200);

            //변경**
            grdSelfShipInspection.View.AddTextBoxColumn("INSPECTIONDEFSEGMENT", 200);

            grdSelfShipInspection.View.PopulateColumns();
        }
        /// <summary>        
        /// 자주검사(출고) 검사 방법 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdSelfShipInspItemClass()
        {
            // TODO : 그리드 초기화 로직 추가
            grdSelfShipInspItemClass.GridButtonItem = GridButtonItem.Add;
            grdSelfShipInspItemClass.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            //Relation을 위해 파라미터로 넘겨줄 컬럼
            grdSelfShipInspItemClass.View.AddTextBoxColumn("INSPECTIONCLASSID", 150)
                .SetIsHidden();

            grdSelfShipInspItemClass.View.AddComboBoxColumn("INSPITEMCLASSID", 150, new SqlQuery("GetInspItemByInspectionClassId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPITEMCLASSID", "INSPITEMCLASSID")
                .SetRelationIds("INSPECTIONCLASSID")
                .SetValidationKeyColumn();

            grdSelfShipInspItemClass.View.AddTextBoxColumn("INSPITEMCLASSNAME", 200)
                .SetIsReadOnly();

            grdSelfShipInspItemClass.View.PopulateColumns();
        }

        /// <summary>        
        /// 자주검사(출고) 검사항목 연계 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdSelfShipRel()
        {
            // TODO : 그리드 초기화 로직 추가
            grdSelfShipRel.GridButtonItem = GridButtonItem.None;
            grdSelfShipRel.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            grdSelfShipRel.View.CheckMarkSelection.MultiSelectCount = 1;

            //inspectionItemRel, InspectionAttribute 테이블의 PK
            grdSelfShipRel.View.AddTextBoxColumn("INSPITEMCLASSID", 150)
                .SetIsHidden();

            grdSelfShipRel.View.AddTextBoxColumn("INSPITEMID", 150)
                .SetIsReadOnly();

            grdSelfShipRel.View.AddTextBoxColumn("INSPITEMNAME", 150)
                .SetIsReadOnly();

            grdSelfShipRel.View.AddTextBoxColumn("INSPITEMVERSION", 150)
                .SetIsReadOnly();

            grdSelfShipRel.View.AddTextBoxColumn("INSPECTIONDEFID", 150)
                .SetIsHidden();

            grdSelfShipRel.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 150)
                .SetIsHidden();

            grdSelfShipRel.View.AddTextBoxColumn("RESOURCETYPE", 150)
                .SetIsHidden();

            grdSelfShipRel.View.AddTextBoxColumn("RESOURCEID", 150)
                .SetIsHidden();

            grdSelfShipRel.View.AddTextBoxColumn("RESOURCEVERSION", 150)
                .SetIsHidden();

            grdSelfShipRel.View.AddTextBoxColumn("PLNATID", 100)
               .SetIsHidden();

            grdSelfShipRel.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();

            //inspectionItemRel 테이블
            grdSelfShipRel.View.AddTextBoxColumn("VALIDTYPE", 100);

            grdSelfShipRel.View.AddSpinEditColumn("TARGETVALUE", 100);

            grdSelfShipRel.View.AddSpinEditColumn("LOWERSPECLIMIT", 100);

            grdSelfShipRel.View.AddSpinEditColumn("UPPERSPECLIMIT", 100);

            grdSelfShipRel.View.AddSpinEditColumn("LOWERCONTROLLIMIT", 100);

            grdSelfShipRel.View.AddSpinEditColumn("UPPERCONTROLLIMIT", 100);

            grdSelfShipRel.View.AddSpinEditColumn("LOWERSCREENLIMIT", 100);

            grdSelfShipRel.View.AddSpinEditColumn("UPPERSCREENLIMIT", 100);
            //inspectionItemRel 단위 CODECLASSID 수정
            grdSelfShipRel.View.AddComboBoxColumn("UNIT", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            //InspectionAttribute 테이블
            grdSelfShipRel.View.AddComboBoxColumn("HASSPECSEQUENCE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetIsReadOnly();

            grdSelfShipRel.View.AddTextBoxColumn("SPECSEQUENCE", 150)
                .SetIsHidden();

            grdSelfShipRel.View.AddTextBoxColumn("SPECCLASSID", 150)
                .SetIsHidden();

            //InspectionAttribute 검사필수여부 CODECLASSID 수정
            grdSelfShipRel.View.AddComboBoxColumn("ISINSPECTIONREQUIRED", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdSelfShipRel.View.AddSpinEditColumn("LOTSIZE", 100);

            grdSelfShipRel.View.AddTextBoxColumn("QTYTYPE", 100);

            grdSelfShipRel.View.AddTextBoxColumn("INSPECTIONLEVEL", 100);

            grdSelfShipRel.View.AddSpinEditColumn("DEFECTLEVEL", 100);

            grdSelfShipRel.View.AddSpinEditColumn("INSPECTIONQTY", 100);

            grdSelfShipRel.View.AddTextBoxColumn("QTYUNIT", 100);

            grdSelfShipRel.View.AddTextBoxColumn("CYCLE", 100);

            grdSelfShipRel.View.AddTextBoxColumn("INSPECTORDEGREE", 100);

            grdSelfShipRel.View.AddTextBoxColumn("EQUIPMENTID", 100);

            grdSelfShipRel.View.AddTextBoxColumn("INSPECTIONUNIT", 100);

            grdSelfShipRel.View.AddTextBoxColumn("DEFECTRATE", 100);

            grdSelfShipRel.View.AddTextBoxColumn("DECISIONDEGREE", 100);


            grdSelfShipRel.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdSelfShipRel.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSelfShipRel.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSelfShipRel.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSelfShipRel.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSelfShipRel.View.PopulateColumns();
        }

        /// <summary>
        /// 자주검사(출고) 측정시기 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdSelfShipPoint()
        {
            grdSelfShipPoint.GridButtonItem = GridButtonItem.Add;
            grdSelfShipPoint.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            //PK
            grdSelfShipPoint.View.AddTextBoxColumn("INSPECTIONPOINTID", 100)
                .SetIsHidden();

            grdSelfShipPoint.View.AddTextBoxColumn("INDPITEMID", 100)
                .SetIsHidden();

            grdSelfShipPoint.View.AddTextBoxColumn("INSPITEMVERSION", 100)
                .SetIsHidden();

            grdSelfShipPoint.View.AddTextBoxColumn("INSPITEMCLASSID", 100)
                .SetIsHidden();

            grdSelfShipPoint.View.AddTextBoxColumn("INSPECTIONDEFID", 100)
                .SetIsHidden();

            grdSelfShipPoint.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 100)
                .SetIsHidden();

            grdSelfShipPoint.View.AddTextBoxColumn("RESOURCEID", 100)
                .SetIsHidden();

            grdSelfShipPoint.View.AddTextBoxColumn("RESOURCEVERSION", 100)
                .SetIsHidden();

            grdSelfShipPoint.View.AddTextBoxColumn("RESOURCETYPE", 100)
                .SetIsHidden();

            grdSelfShipPoint.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();

            grdSelfShipPoint.View.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden();

            grdSelfShipPoint.View.AddComboBoxColumn("INSPECTIONPOINTNAME", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspectionPointName", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdSelfShipPoint.View.AddSpinEditColumn("INSPECTIONQTY", 100);

            grdSelfShipPoint.View.AddSpinEditColumn("POINTQTY", 100);

            grdSelfShipPoint.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetValidationIsRequired()
               .SetDefault("Valid")
               .SetTextAlignment(TextAlignment.Center);

            grdSelfShipPoint.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSelfShipPoint.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSelfShipPoint.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSelfShipPoint.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);


            grdSelfShipPoint.View.PopulateColumns();
        }
        #endregion

        #region 신뢰성검증 탭의 그리드 초기화
        /// <summary>        
        /// 신뢰성검증 검사 종류 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdReliabilityInspection()
        {
            // TODO : 그리드 초기화 로직 추가
            grdReliabilityInspection.GridButtonItem = GridButtonItem.None;
            grdReliabilityInspection.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdReliabilityInspection.View.SetIsReadOnly();

            grdReliabilityInspection.View.AddTextBoxColumn("INSPECTIONCLASSID", 150);

            grdReliabilityInspection.View.AddTextBoxColumn("INSPECTIONCLASSNAME", 200);

            grdReliabilityInspection.View.AddTextBoxColumn("INSPECTIONDEFID", 150);

            grdReliabilityInspection.View.AddTextBoxColumn("INSPECTIONDEFNAME", 200);

            grdReliabilityInspection.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 200);

            grdReliabilityInspection.View.AddTextBoxColumn("RESOURCETYPE", 200);

            //변경**
            grdReliabilityInspection.View.AddTextBoxColumn("INSPECTIONDEFSEGMENT", 200);

            grdReliabilityInspection.View.PopulateColumns();
        }
        /// <summary>        
        /// 신뢰성검증 검사 방법 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdReliabilityInspItemClass()
        {
            // TODO : 그리드 초기화 로직 추가
            grdReliabilityInspItemClass.GridButtonItem = GridButtonItem.Add;
            grdReliabilityInspItemClass.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            //Relation을 위해 파라미터로 넘겨줄 컬럼
            grdReliabilityInspItemClass.View.AddTextBoxColumn("INSPECTIONCLASSID", 150)
                .SetIsHidden();

            grdReliabilityInspItemClass.View.AddComboBoxColumn("INSPITEMCLASSID", 150, new SqlQuery("GetInspItemByInspectionClassId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPITEMCLASSID", "INSPITEMCLASSID")
                .SetRelationIds("INSPECTIONCLASSID")
                .SetValidationKeyColumn();

            grdReliabilityInspItemClass.View.AddTextBoxColumn("INSPITEMCLASSNAME", 200)
                .SetIsReadOnly();

            grdReliabilityInspItemClass.View.PopulateColumns();
        }

        /// <summary>        
        /// 신뢰성검증 검사항목 연계 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdReliabilityRel()
        {
            // TODO : 그리드 초기화 로직 추가
            grdReliabilityRel.GridButtonItem = GridButtonItem.None;
            grdReliabilityRel.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            grdReliabilityRel.View.CheckMarkSelection.MultiSelectCount = 1;

            //inspectionItemRel, InspectionAttribute 테이블의 PK
            grdReliabilityRel.View.AddTextBoxColumn("INSPITEMCLASSID", 150)
                .SetIsHidden();

            grdReliabilityRel.View.AddTextBoxColumn("INSPITEMID", 150)
                .SetIsReadOnly();

            grdReliabilityRel.View.AddTextBoxColumn("INSPITEMNAME", 150)
                .SetIsReadOnly();

            grdReliabilityRel.View.AddTextBoxColumn("INSPITEMVERSION", 150)
                .SetIsReadOnly();

            grdReliabilityRel.View.AddTextBoxColumn("INSPECTIONDEFID", 150)
                .SetIsHidden();

            grdReliabilityRel.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 150)
                .SetIsHidden();

            grdReliabilityRel.View.AddTextBoxColumn("RESOURCETYPE", 150)
                .SetIsHidden();

            grdReliabilityRel.View.AddTextBoxColumn("RESOURCEID", 150)
                .SetIsHidden();

            grdReliabilityRel.View.AddTextBoxColumn("RESOURCEVERSION", 150)
                .SetIsHidden();
            grdReliabilityRel.View.AddTextBoxColumn("PLNATID", 100)
                          .SetIsHidden();

            grdReliabilityRel.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();

            //inspectionItemRel 테이블
            grdReliabilityRel.View.AddTextBoxColumn("VALIDTYPE", 100);

            grdReliabilityRel.View.AddSpinEditColumn("TARGETVALUE", 100);

            grdReliabilityRel.View.AddSpinEditColumn("LOWERSPECLIMIT", 100);

            grdReliabilityRel.View.AddSpinEditColumn("UPPERSPECLIMIT", 100);

            grdReliabilityRel.View.AddSpinEditColumn("LOWERCONTROLLIMIT", 100);

            grdReliabilityRel.View.AddSpinEditColumn("UPPERCONTROLLIMIT", 100);

            grdReliabilityRel.View.AddSpinEditColumn("LOWERSCREENLIMIT", 100);

            grdReliabilityRel.View.AddSpinEditColumn("UPPERSCREENLIMIT", 100);
            //inspectionItemRel 단위 CODECLASSID 수정
            grdReliabilityRel.View.AddComboBoxColumn("UNIT", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            //InspectionAttribute 테이블
            grdReliabilityRel.View.AddComboBoxColumn("HASSPECSEQUENCE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetIsReadOnly();

            grdReliabilityRel.View.AddTextBoxColumn("SPECSEQUENCE", 150)
                .SetIsHidden();

            grdReliabilityRel.View.AddTextBoxColumn("SPECCLASSID", 150)
                .SetIsHidden();

            //InspectionAttribute 검사필수여부 CODECLASSID 수정
            grdReliabilityRel.View.AddComboBoxColumn("ISINSPECTIONREQUIRED", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdReliabilityRel.View.AddSpinEditColumn("LOTSIZE", 100);

            grdReliabilityRel.View.AddTextBoxColumn("QTYTYPE", 100);

            grdReliabilityRel.View.AddTextBoxColumn("INSPECTIONLEVEL", 100);

            grdReliabilityRel.View.AddSpinEditColumn("DEFECTLEVEL", 100);

            grdReliabilityRel.View.AddSpinEditColumn("INSPECTIONQTY", 100);

            grdReliabilityRel.View.AddTextBoxColumn("QTYUNIT", 100);

            grdReliabilityRel.View.AddTextBoxColumn("CYCLE", 100);

            grdReliabilityRel.View.AddTextBoxColumn("INSPECTORDEGREE", 100);

            grdReliabilityRel.View.AddTextBoxColumn("EQUIPMENTID", 100);

            grdReliabilityRel.View.AddTextBoxColumn("INSPECTIONUNIT", 100);

            grdReliabilityRel.View.AddTextBoxColumn("DEFECTRATE", 100);

            grdReliabilityRel.View.AddTextBoxColumn("DECISIONDEGREE", 100);

            grdReliabilityRel.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdReliabilityRel.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdReliabilityRel.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdReliabilityRel.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdReliabilityRel.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdReliabilityRel.View.PopulateColumns();
        }

        /// <summary>
        /// 신뢰성검증 측정시기 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdReliabilityPoint()
        {
            grdReliabilityPoint.GridButtonItem = GridButtonItem.Add;
            grdReliabilityPoint.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            //PK
            grdReliabilityPoint.View.AddTextBoxColumn("INSPECTIONPOINTID", 100)
                .SetIsHidden();

            grdReliabilityPoint.View.AddTextBoxColumn("INDPITEMID", 100)
                .SetIsHidden();

            grdReliabilityPoint.View.AddTextBoxColumn("INSPITEMVERSION", 100)
                .SetIsHidden();

            grdReliabilityPoint.View.AddTextBoxColumn("INSPITEMCLASSID", 100)
                .SetIsHidden();

            grdReliabilityPoint.View.AddTextBoxColumn("INSPECTIONDEFID", 100)
                .SetIsHidden();

            grdReliabilityPoint.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 100)
                .SetIsHidden();

            grdReliabilityPoint.View.AddTextBoxColumn("RESOURCEID", 100)
                .SetIsHidden();

            grdReliabilityPoint.View.AddTextBoxColumn("RESOURCEVERSION", 100)
                .SetIsHidden();

            grdReliabilityPoint.View.AddTextBoxColumn("RESOURCETYPE", 100)
                .SetIsHidden();

            grdReliabilityPoint.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();

            grdReliabilityPoint.View.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden();

            grdReliabilityPoint.View.AddComboBoxColumn("INSPECTIONPOINTNAME", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspectionPointName", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdReliabilityPoint.View.AddSpinEditColumn("INSPECTIONQTY", 100);

            grdReliabilityPoint.View.AddSpinEditColumn("POINTQTY", 100);

            grdReliabilityPoint.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetValidationIsRequired()
               .SetDefault("Valid")
               .SetTextAlignment(TextAlignment.Center);

            grdReliabilityPoint.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdReliabilityPoint.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdReliabilityPoint.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdReliabilityPoint.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);


            grdReliabilityPoint.View.PopulateColumns();
        }
        #endregion

        #region 최종검사 탭의 그리드 초기화
        /// <summary>        
        /// 최종 검사 종류 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdFinishInspection()
        {
            // TODO : 그리드 초기화 로직 추가
            grdFinishInspection.GridButtonItem = GridButtonItem.None;
            grdFinishInspection.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdFinishInspection.View.SetIsReadOnly();

            grdFinishInspection.View.AddTextBoxColumn("INSPECTIONCLASSID", 150);

            grdFinishInspection.View.AddTextBoxColumn("INSPECTIONCLASSNAME", 200);

            grdFinishInspection.View.AddTextBoxColumn("INSPECTIONDEFID", 150);

            grdFinishInspection.View.AddTextBoxColumn("INSPECTIONDEFNAME", 200);

            grdFinishInspection.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 200);

            grdFinishInspection.View.AddTextBoxColumn("RESOURCETYPE", 200);

            //변경**
            grdFinishInspection.View.AddTextBoxColumn("INSPECTIONDEFSEGMENT", 200);

            grdFinishInspection.View.PopulateColumns();
        }

        /// <summary>        
        /// 최종 검사 검사품목 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdFinishProduct()
        {
            // TODO : 그리드 초기화 로직 추가
            grdFinishProduct.GridButtonItem = GridButtonItem.Add;
            grdFinishProduct.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            InitializeGrdFinishProduct_ProductDefIdPopup();

            grdFinishProduct.View.AddTextBoxColumn("RESOURCENAME", 200)
                .SetLabel("CONSUMABLEDEFNAME")
                .SetIsReadOnly();

            grdFinishProduct.View.AddTextBoxColumn("RESOURCEVERSION", 150)
                .SetLabel("RESOURCEVERSION")
                .SetIsReadOnly();

            grdFinishProduct.View.AddTextBoxColumn("QTYTYPE", 100);

            grdFinishProduct.View.AddTextBoxColumn("INSPECTORDEGREE", 150);

            grdFinishProduct.View.AddTextBoxColumn("INSPECTIONLEVEL", 150);

            grdFinishProduct.View.AddTextBoxColumn("DEFECTLEVEL", 150);

            grdFinishProduct.View.PopulateColumns();
        }

        /// <summary>
        /// 검사 품목을 선택하는 팝업
        /// </summary>
        private void InitializeGrdFinishProduct_ProductDefIdPopup()
        {
            //팝업 컬럼 설정
            var productDefId = grdFinishProduct.View.AddSelectPopupColumn("RESOURCEID", new SqlQuery("GetProductDefList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                            .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, false)
                                            .SetPopupResultCount(1)
                                            .SetLabel("PRODUCTDEFID")
                                            .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                            .SetPopupAutoFillColumns("PRODUCTDEFNAME")       
                                            .SetValidationKeyColumn()
                                            .SetPopupResultMapping("RESOURCEID", "PRODUCTDEFID")
                                            .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                            {
                                                // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                                                // dataGridRow : 현재 Focus가 있는 그리드의 DataRow

                                                foreach (DataRow row in selectedRows)
                                                {
                                                    dataGridRow["RESOURCENAME"] = row["PRODUCTDEFNAME"].ToString();
                                                    dataGridRow["RESOURCEVERSION"] = row["PRODUCTDEFVERSION"].ToString();
                                                }

                                                _rowResource = dataGridRow;

                                            });

            productDefId.Conditions.AddTextBox("PRODUCTDEFID");
            productDefId.Conditions.AddTextBox("PRODUCTDEFNAME");

            // 팝업 그리드
            productDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            productDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            productDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 200);
        }

        /// <summary>        
        /// 최종 검사 검사 방법 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdFinishInspItemClass()
        {
            // TODO : 그리드 초기화 로직 추가
            grdFinishInspItemClass.GridButtonItem = GridButtonItem.Add;
            grdFinishInspItemClass.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            //Relation을 위해 파라미터로 넘겨줄 컬럼
            grdFinishInspItemClass.View.AddTextBoxColumn("INSPECTIONCLASSID", 150)
                .SetIsHidden();

            grdFinishInspItemClass.View.AddComboBoxColumn("INSPITEMCLASSID", 150, new SqlQuery("GetInspItemByInspectionClassId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPITEMCLASSID", "INSPITEMCLASSID")
                .SetRelationIds("INSPECTIONCLASSID")
                .SetValidationKeyColumn();

            grdFinishInspItemClass.View.AddTextBoxColumn("INSPITEMCLASSNAME", 200)
                .SetIsReadOnly();

            grdFinishInspItemClass.View.PopulateColumns();
        }

        /// <summary>        
        /// 최종 검사 검사항목 연계 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdFinishRel()
        {
            // TODO : 그리드 초기화 로직 추가
            grdFinishRel.GridButtonItem = GridButtonItem.None;
            grdFinishRel.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            grdFinishRel.View.CheckMarkSelection.MultiSelectCount = 1;

            //inspectionItemRel, InspectionAttribute 테이블의 PK
            grdFinishRel.View.AddTextBoxColumn("INSPITEMCLASSID", 150)
                .SetIsHidden();

            grdFinishRel.View.AddTextBoxColumn("INSPITEMID", 150)
                .SetIsReadOnly();

            grdFinishRel.View.AddTextBoxColumn("INSPITEMNAME", 150)
                .SetIsReadOnly();

            grdFinishRel.View.AddTextBoxColumn("INSPITEMVERSION", 150)
                .SetIsReadOnly();

            grdFinishRel.View.AddTextBoxColumn("INSPECTIONDEFID", 150)
                .SetIsHidden();

            grdFinishRel.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 150)
                .SetIsHidden();

            grdFinishRel.View.AddTextBoxColumn("RESOURCETYPE", 150)
                .SetIsHidden();

            grdFinishRel.View.AddTextBoxColumn("RESOURCEID", 150)
                .SetIsHidden();

            grdFinishRel.View.AddTextBoxColumn("RESOURCEVERSION", 150)
                .SetIsHidden();

            grdFinishRel.View.AddTextBoxColumn("PLNATID", 100)
                .SetIsHidden();

            grdFinishRel.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();

            //inspectionItemRel 테이블
            grdFinishRel.View.AddTextBoxColumn("VALIDTYPE", 100);

            grdFinishRel.View.AddSpinEditColumn("TARGETVALUE", 100);

            grdFinishRel.View.AddSpinEditColumn("LOWERSPECLIMIT", 100);

            grdFinishRel.View.AddSpinEditColumn("UPPERSPECLIMIT", 100);

            grdFinishRel.View.AddSpinEditColumn("LOWERCONTROLLIMIT", 100);

            grdFinishRel.View.AddSpinEditColumn("UPPERCONTROLLIMIT", 100);

            grdFinishRel.View.AddSpinEditColumn("LOWERSCREENLIMIT", 100);

            grdFinishRel.View.AddSpinEditColumn("UPPERSCREENLIMIT", 100);
            //inspectionItemRel 단위 CODECLASSID 수정
            grdFinishRel.View.AddComboBoxColumn("UNIT", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            //InspectionAttribute 테이블
            grdFinishRel.View.AddComboBoxColumn("HASSPECSEQUENCE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();

            grdFinishRel.View.AddTextBoxColumn("SPECSEQUENCE", 150)
                .SetIsHidden();

            grdFinishRel.View.AddTextBoxColumn("SPECCLASSID", 150)
                .SetIsHidden();

            //InspectionAttribute 검사필수여부 CODECLASSID 수정
            grdFinishRel.View.AddComboBoxColumn("ISINSPECTIONREQUIRED", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdFinishRel.View.AddSpinEditColumn("LOTSIZE", 100);

            grdFinishRel.View.AddSpinEditColumn("INSPECTIONQTY", 100);

            grdFinishRel.View.AddTextBoxColumn("QTYUNIT", 100);

            grdFinishRel.View.AddTextBoxColumn("CYCLE", 100);

            grdFinishRel.View.AddTextBoxColumn("EQUIPMENTID", 100);

            grdFinishRel.View.AddTextBoxColumn("INSPECTIONUNIT", 100);

            grdFinishRel.View.AddTextBoxColumn("DEFECTRATE", 100);

            grdFinishRel.View.AddTextBoxColumn("DECISIONDEGREE", 100);

            grdFinishRel.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdFinishRel.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdFinishRel.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdFinishRel.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdFinishRel.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdFinishRel.View.PopulateColumns();
        }

        /// <summary>
        /// 최종 검사 측정시기 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdFinishPoint()
        {
            grdFinishPoint.GridButtonItem = GridButtonItem.Add;
            grdFinishPoint.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            //PK
            grdFinishPoint.View.AddTextBoxColumn("INSPECTIONPOINTID", 100)
                .SetIsHidden();

            grdFinishPoint.View.AddTextBoxColumn("INDPITEMID", 100)
                .SetIsHidden();

            grdFinishPoint.View.AddTextBoxColumn("INSPITEMVERSION", 100)
                .SetIsHidden();

            grdFinishPoint.View.AddTextBoxColumn("INSPITEMCLASSID", 100)
                .SetIsHidden();

            grdFinishPoint.View.AddTextBoxColumn("INSPECTIONDEFID", 100)
                .SetIsHidden();

            grdFinishPoint.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 100)
                .SetIsHidden();

            grdFinishPoint.View.AddTextBoxColumn("RESOURCEID", 100)
                .SetIsHidden();

            grdFinishPoint.View.AddTextBoxColumn("RESOURCEVERSION", 100)
                .SetIsHidden();

            grdFinishPoint.View.AddTextBoxColumn("RESOURCETYPE", 100)
                .SetIsHidden();

            grdFinishPoint.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();

            grdFinishPoint.View.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden();

            grdFinishPoint.View.AddComboBoxColumn("INSPECTIONPOINTNAME", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspectionPointName", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdFinishPoint.View.AddSpinEditColumn("INSPECTIONQTY", 100);

            grdFinishPoint.View.AddSpinEditColumn("POINTQTY", 100);

            grdFinishPoint.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetValidationIsRequired()
               .SetDefault("Valid")
               .SetTextAlignment(TextAlignment.Center);

            grdFinishPoint.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdFinishPoint.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdFinishPoint.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdFinishPoint.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);


            grdFinishPoint.View.PopulateColumns();
        }
        #endregion

        #region 출하검사 탭의 그리드 초기화
        /// <summary>        
        /// 출하 검사 종류 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdShipmentInspection()
        {
            // TODO : 그리드 초기화 로직 추가
            grdShipmentInspection.GridButtonItem = GridButtonItem.None;
            grdShipmentInspection.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdShipmentInspection.View.SetIsReadOnly();

            grdShipmentInspection.View.AddTextBoxColumn("INSPECTIONCLASSID", 150);

            grdShipmentInspection.View.AddTextBoxColumn("INSPECTIONCLASSNAME", 200);

            grdShipmentInspection.View.AddTextBoxColumn("INSPECTIONDEFID", 150);

            grdShipmentInspection.View.AddTextBoxColumn("INSPECTIONDEFNAME", 200);

            grdShipmentInspection.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 200);

            grdShipmentInspection.View.AddTextBoxColumn("RESOURCETYPE", 200);

            //변경**
            grdShipmentInspection.View.AddTextBoxColumn("INSPECTIONDEFSEGMENT", 200);

            grdShipmentInspection.View.PopulateColumns();
        }

        /// <summary>        
        /// 출하 검사 검사품목 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdShipmentProduct()
        {
            // TODO : 그리드 초기화 로직 추가
            grdShipmentProduct.GridButtonItem = GridButtonItem.Add;
            grdShipmentProduct.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            InitializegrdShipmentProduct_ProductDefIdPopup();

            grdShipmentProduct.View.AddTextBoxColumn("RESOURCENAME", 200)
                .SetLabel("CONSUMABLEDEFNAME")
                .SetIsReadOnly();

            grdShipmentProduct.View.AddTextBoxColumn("RESOURCEVERSION", 150)
                .SetLabel("RESOURCEVERSION")
                .SetIsReadOnly();

            grdShipmentProduct.View.AddTextBoxColumn("QTYTYPE", 100);

            grdShipmentProduct.View.AddTextBoxColumn("INSPECTORDEGREE", 150);

            grdShipmentProduct.View.AddTextBoxColumn("INSPECTIONLEVEL", 150);

            grdShipmentProduct.View.AddTextBoxColumn("DEFECTLEVEL", 150);

            grdShipmentProduct.View.PopulateColumns();
        }

        /// <summary>
        /// 검사 품목을 선택하는 팝업
        /// </summary>
        private void InitializegrdShipmentProduct_ProductDefIdPopup()
        {
            //팝업 컬럼 설정
            var productDefId = grdShipmentProduct.View.AddSelectPopupColumn("RESOURCEID", new SqlQuery("GetProductDefList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                            .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, false)
                                            .SetPopupResultCount(1)
                                            .SetLabel("PRODUCTDEFID")
                                            .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                            .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                                            .SetValidationKeyColumn()
                                            .SetPopupResultMapping("RESOURCEID", "PRODUCTDEFID")
                                            .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                            {
                                                // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                                                // dataGridRow : 현재 Focus가 있는 그리드의 DataRow

                                                foreach (DataRow row in selectedRows)
                                                {
                                                    dataGridRow["RESOURCENAME"] = row["PRODUCTDEFNAME"].ToString();
                                                    dataGridRow["RESOURCEVERSION"] = row["PRODUCTDEFVERSION"].ToString();
                                                }

                                                _rowResource = dataGridRow;

                                            });

            productDefId.Conditions.AddTextBox("PRODUCTDEFID");
            productDefId.Conditions.AddTextBox("PRODUCTDEFNAME");

            // 팝업 그리드
            productDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            productDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            productDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 200);
        }

        /// <summary>        
        /// 출하 검사 검사 방법 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdShipmentInspItemClass()
        {
            // TODO : 그리드 초기화 로직 추가
            grdShipmentInspItemClass.GridButtonItem = GridButtonItem.Add;
            grdShipmentInspItemClass.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            //Relation을 위해 파라미터로 넘겨줄 컬럼
            grdShipmentInspItemClass.View.AddTextBoxColumn("INSPECTIONCLASSID", 150)
                .SetIsHidden();

            grdShipmentInspItemClass.View.AddComboBoxColumn("INSPITEMCLASSID", 150, new SqlQuery("GetInspItemByInspectionClassId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPITEMCLASSID", "INSPITEMCLASSID")
                .SetRelationIds("INSPECTIONCLASSID")
                .SetValidationKeyColumn();

            grdShipmentInspItemClass.View.AddTextBoxColumn("INSPITEMCLASSNAME", 200)
                .SetIsReadOnly();

            grdShipmentInspItemClass.View.PopulateColumns();
        }

        /// <summary>        
        /// 출하 검사 검사항목 연계 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdShipmentRel()
        {
            // TODO : 그리드 초기화 로직 추가
            grdShipmentRel.GridButtonItem = GridButtonItem.None;
            grdShipmentRel.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdShipmentRel.View.CheckMarkSelection.MultiSelectCount = 1;

            //inspectionItemRel, InspectionAttribute 테이블의 PK
            grdShipmentRel.View.AddTextBoxColumn("INSPITEMCLASSID", 150)
                .SetIsHidden();

            grdShipmentRel.View.AddTextBoxColumn("INSPITEMID", 150)
                .SetIsReadOnly();

            grdShipmentRel.View.AddTextBoxColumn("INSPITEMNAME", 150)
                .SetIsReadOnly();

            grdShipmentRel.View.AddTextBoxColumn("INSPITEMVERSION", 150)
                .SetIsReadOnly();

            grdShipmentRel.View.AddTextBoxColumn("INSPECTIONDEFID", 150)
                .SetIsHidden();

            grdShipmentRel.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 150)
                .SetIsHidden();

            grdShipmentRel.View.AddTextBoxColumn("RESOURCETYPE", 150)
                .SetIsHidden();

            grdShipmentRel.View.AddTextBoxColumn("RESOURCEID", 150)
                .SetIsHidden();

            grdShipmentRel.View.AddTextBoxColumn("RESOURCEVERSION", 150)
                .SetIsHidden();

            grdShipmentRel.View.AddTextBoxColumn("PLNATID", 100)
                .SetIsHidden();

            grdShipmentRel.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();

            //inspectionItemRel 테이블
            grdShipmentRel.View.AddTextBoxColumn("VALIDTYPE", 100);

            grdShipmentRel.View.AddSpinEditColumn("TARGETVALUE", 100);

            grdShipmentRel.View.AddSpinEditColumn("LOWERSPECLIMIT", 100);

            grdShipmentRel.View.AddSpinEditColumn("UPPERSPECLIMIT", 100);

            grdShipmentRel.View.AddSpinEditColumn("LOWERCONTROLLIMIT", 100);

            grdShipmentRel.View.AddSpinEditColumn("UPPERCONTROLLIMIT", 100);

            grdShipmentRel.View.AddSpinEditColumn("LOWERSCREENLIMIT", 100);

            grdShipmentRel.View.AddSpinEditColumn("UPPERSCREENLIMIT", 100);
            //inspectionItemRel 단위 CODECLASSID 수정
            grdShipmentRel.View.AddComboBoxColumn("UNIT", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            //InspectionAttribute 테이블
            grdShipmentRel.View.AddComboBoxColumn("HASSPECSEQUENCE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();

            grdShipmentRel.View.AddTextBoxColumn("SPECSEQUENCE", 150)
                .SetIsHidden();

            grdShipmentRel.View.AddTextBoxColumn("SPECCLASSID", 150)
                .SetIsHidden();

            //InspectionAttribute 검사필수여부 CODECLASSID 수정
            grdShipmentRel.View.AddComboBoxColumn("ISINSPECTIONREQUIRED", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdShipmentRel.View.AddSpinEditColumn("LOTSIZE", 100);

            grdShipmentRel.View.AddSpinEditColumn("INSPECTIONQTY", 100);

            grdShipmentRel.View.AddTextBoxColumn("QTYUNIT", 100);

            grdShipmentRel.View.AddTextBoxColumn("CYCLE", 100);

            grdShipmentRel.View.AddTextBoxColumn("EQUIPMENTID", 100);

            grdShipmentRel.View.AddTextBoxColumn("INSPECTIONUNIT", 100);

            grdShipmentRel.View.AddTextBoxColumn("DEFECTRATE", 100);

            grdShipmentRel.View.AddTextBoxColumn("DECISIONDEGREE", 100);

            grdShipmentRel.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdShipmentRel.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdShipmentRel.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdShipmentRel.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdShipmentRel.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdShipmentRel.View.PopulateColumns();
        }

        /// <summary>
        /// 출하 검사 측정시기 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdShipmentPoint()
        {
            grdShipmentPoint.GridButtonItem = GridButtonItem.Add;
            grdShipmentPoint.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            //PK
            grdShipmentPoint.View.AddTextBoxColumn("INSPECTIONPOINTID", 100)
                .SetIsHidden();

            grdShipmentPoint.View.AddTextBoxColumn("INDPITEMID", 100)
                .SetIsHidden();

            grdShipmentPoint.View.AddTextBoxColumn("INSPITEMVERSION", 100)
                .SetIsHidden();

            grdShipmentPoint.View.AddTextBoxColumn("INSPITEMCLASSID", 100)
                .SetIsHidden();

            grdShipmentPoint.View.AddTextBoxColumn("INSPECTIONDEFID", 100)
                .SetIsHidden();

            grdShipmentPoint.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 100)
                .SetIsHidden();

            grdShipmentPoint.View.AddTextBoxColumn("RESOURCEID", 100)
                .SetIsHidden();

            grdShipmentPoint.View.AddTextBoxColumn("RESOURCEVERSION", 100)
                .SetIsHidden();

            grdShipmentPoint.View.AddTextBoxColumn("RESOURCETYPE", 100)
                .SetIsHidden();

            grdShipmentPoint.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();

            grdShipmentPoint.View.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden();

            grdShipmentPoint.View.AddComboBoxColumn("INSPECTIONPOINTNAME", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspectionPointName", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdShipmentPoint.View.AddSpinEditColumn("INSPECTIONQTY", 100);

            grdShipmentPoint.View.AddSpinEditColumn("POINTQTY", 100);

            grdShipmentPoint.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetValidationIsRequired()
               .SetDefault("Valid")
               .SetTextAlignment(TextAlignment.Center);

            grdShipmentPoint.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdShipmentPoint.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdShipmentPoint.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdShipmentPoint.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);


            grdShipmentPoint.View.PopulateColumns();
        }
        #endregion

        #region 계측검사 탭의 그리드 초기화
        /// <summary>        
        /// 계측검사 종류 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdOperationInspection()
        {
            // TODO : 그리드 초기화 로직 추가
            grdOperationInspection.GridButtonItem = GridButtonItem.None;
            grdOperationInspection.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdOperationInspection.View.SetIsReadOnly();

            grdOperationInspection.View.AddTextBoxColumn("INSPECTIONCLASSID", 150);

            grdOperationInspection.View.AddTextBoxColumn("INSPECTIONCLASSNAME", 200);

            grdOperationInspection.View.AddTextBoxColumn("INSPECTIONDEFID", 150);

            grdOperationInspection.View.AddTextBoxColumn("INSPECTIONDEFNAME", 200);

            grdOperationInspection.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 200);

            grdOperationInspection.View.AddTextBoxColumn("RESOURCETYPE", 200);

            //변경**
            grdOperationInspection.View.AddTextBoxColumn("INSPECTIONDEFSEGMENT", 200);


            grdOperationInspection.View.PopulateColumns();
        }

        /// <summary>        
        /// 계측검사 검사품목 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdOperationProduct()
        {
            // TODO : 그리드 초기화 로직 추가
            grdOperationProduct.GridButtonItem = GridButtonItem.None;
            grdOperationProduct.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            InitializegrdOperationProduct_ProductDefIdPopup();

            grdOperationProduct.View.AddTextBoxColumn("RESOURCENAME", 200)
                .SetLabel("CONSUMABLEDEFNAME")
                .SetIsReadOnly();

            grdOperationProduct.View.AddTextBoxColumn("RESOURCEVERSION", 150)
                .SetLabel("RESOURCEVERSION")
                .SetIsReadOnly();

            grdOperationProduct.View.AddTextBoxColumn("QTYTYPE", 100);

            grdOperationProduct.View.AddTextBoxColumn("INSPECTORDEGREE", 150);

            grdOperationProduct.View.AddTextBoxColumn("INSPECTIONLEVEL", 150);

            grdOperationProduct.View.AddTextBoxColumn("DEFECTLEVEL", 150);

            grdOperationProduct.View.PopulateColumns();
        }

        /// <summary>
        /// 검사 품목을 선택하는 팝업
        /// </summary>
        private void InitializegrdOperationProduct_ProductDefIdPopup()
        {
            //팝업 컬럼 설정
            var productDefId = grdOperationProduct.View.AddSelectPopupColumn("RESOURCEID", new SqlQuery("GetProductDefList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                            .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, false)
                                            .SetPopupResultCount(1)
                                            .SetLabel("PRODUCTDEFID")
                                            .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                            .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                                            .SetValidationKeyColumn()
                                            .SetPopupResultMapping("RESOURCEID", "PRODUCTDEFID")
                                            .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                            {
                                                // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                                                // dataGridRow : 현재 Focus가 있는 그리드의 DataRow

                                                foreach (DataRow row in selectedRows)
                                                {
                                                    dataGridRow["RESOURCENAME"] = row["PRODUCTDEFNAME"].ToString();
                                                    dataGridRow["RESOURCEVERSION"] = row["PRODUCTDEFVERSION"].ToString();
                                                }

                                                _rowResource = dataGridRow;

                                            });

            productDefId.Conditions.AddTextBox("PRODUCTDEFID");
            productDefId.Conditions.AddTextBox("PRODUCTDEFNAME");

            // 팝업 그리드
            productDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            productDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            productDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 200);
        }

        /// <summary>        
        /// 계측검사 검사 방법 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdOperationInspItemClass()
        {
            // TODO : 그리드 초기화 로직 추가
            grdOperationInspItemClass.GridButtonItem = GridButtonItem.None;
            grdOperationInspItemClass.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            //Relation을 위해 파라미터로 넘겨줄 컬럼
            grdOperationInspItemClass.View.AddTextBoxColumn("INSPECTIONCLASSID", 150)
                .SetIsHidden();

            grdOperationInspItemClass.View.AddComboBoxColumn("INSPITEMCLASSID", 150, new SqlQuery("GetInspItemByInspectionClassId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPITEMCLASSID", "INSPITEMCLASSID")
                .SetRelationIds("INSPECTIONCLASSID")
                .SetValidationKeyColumn();

            grdOperationInspItemClass.View.AddTextBoxColumn("INSPITEMCLASSNAME", 200)
                .SetIsReadOnly();

            grdOperationInspItemClass.View.PopulateColumns();
        }

        /// <summary>        
        /// 계측검사 검사항목 연계 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdOperationRel()
        {
            // TODO : 그리드 초기화 로직 추가
            grdOperationRel.GridButtonItem = GridButtonItem.None;
            grdOperationRel.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdOperationRel.View.CheckMarkSelection.MultiSelectCount = 1;

            //inspectionItemRel, InspectionAttribute 테이블의 PK
            grdOperationRel.View.AddTextBoxColumn("INSPITEMCLASSID", 150)
                .SetIsHidden();

            grdOperationRel.View.AddTextBoxColumn("INSPITEMID", 150)
                .SetIsReadOnly();

            grdOperationRel.View.AddTextBoxColumn("INSPITEMNAME", 150)
                .SetIsReadOnly();

            grdOperationRel.View.AddTextBoxColumn("INSPITEMVERSION", 150)
                .SetIsReadOnly();

            grdOperationRel.View.AddTextBoxColumn("INSPECTIONDEFID", 150)
                .SetIsHidden();

            grdOperationRel.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 150)
                .SetIsHidden();

            grdOperationRel.View.AddTextBoxColumn("RESOURCETYPE", 150)
                .SetIsHidden();

            grdOperationRel.View.AddTextBoxColumn("RESOURCEID", 150)
                .SetIsHidden();

            grdOperationRel.View.AddTextBoxColumn("RESOURCEVERSION", 150)
                .SetIsHidden();

            grdOperationRel.View.AddTextBoxColumn("PLNATID", 100)
                .SetIsHidden();

            grdOperationRel.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();

            //inspectionItemRel 테이블
            grdOperationRel.View.AddTextBoxColumn("VALIDTYPE", 100);

            grdOperationRel.View.AddSpinEditColumn("TARGETVALUE", 100);

            grdOperationRel.View.AddSpinEditColumn("LOWERSPECLIMIT", 100);

            grdOperationRel.View.AddSpinEditColumn("UPPERSPECLIMIT", 100);

            grdOperationRel.View.AddSpinEditColumn("LOWERCONTROLLIMIT", 100);

            grdOperationRel.View.AddSpinEditColumn("UPPERCONTROLLIMIT", 100);

            grdOperationRel.View.AddSpinEditColumn("LOWERSCREENLIMIT", 100);

            grdOperationRel.View.AddSpinEditColumn("UPPERSCREENLIMIT", 100);
            //inspectionItemRel 단위 CODECLASSID 수정
            grdOperationRel.View.AddComboBoxColumn("UNIT", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            //InspectionAttribute 테이블
            grdOperationRel.View.AddComboBoxColumn("HASSPECSEQUENCE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();

            grdOperationRel.View.AddTextBoxColumn("SPECSEQUENCE", 150)
                .SetIsHidden();

            grdOperationRel.View.AddTextBoxColumn("SPECCLASSID", 150)
                .SetIsHidden();

            //InspectionAttribute 검사필수여부 CODECLASSID 수정
            grdOperationRel.View.AddComboBoxColumn("ISINSPECTIONREQUIRED", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdOperationRel.View.AddSpinEditColumn("LOTSIZE", 100);

            grdOperationRel.View.AddSpinEditColumn("INSPECTIONQTY", 100);

            grdOperationRel.View.AddTextBoxColumn("QTYUNIT", 100);

            grdOperationRel.View.AddTextBoxColumn("CYCLE", 100);

            grdOperationRel.View.AddTextBoxColumn("EQUIPMENTID", 100);

            grdOperationRel.View.AddTextBoxColumn("INSPECTIONUNIT", 100);

            grdOperationRel.View.AddTextBoxColumn("DEFECTRATE", 100);

            grdOperationRel.View.AddTextBoxColumn("DECISIONDEGREE", 100);

            grdOperationRel.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdOperationRel.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdOperationRel.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdOperationRel.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdOperationRel.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdOperationRel.View.PopulateColumns();
        }

        /// <summary>
        /// 계측검사 측정시기 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdOperationPoint()
        {
            grdOperationPoint.GridButtonItem = GridButtonItem.None;
            grdOperationPoint.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            //PK
            grdOperationPoint.View.AddTextBoxColumn("INSPECTIONPOINTID", 100)
                .SetIsHidden();

            grdOperationPoint.View.AddTextBoxColumn("INDPITEMID", 100)
                .SetIsHidden();

            grdOperationPoint.View.AddTextBoxColumn("INSPITEMVERSION", 100)
                .SetIsHidden();

            grdOperationPoint.View.AddTextBoxColumn("INSPITEMCLASSID", 100)
                .SetIsHidden();

            grdOperationPoint.View.AddTextBoxColumn("INSPECTIONDEFID", 100)
                .SetIsHidden();

            grdOperationPoint.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 100)
                .SetIsHidden();

            grdOperationPoint.View.AddTextBoxColumn("RESOURCEID", 100)
                .SetIsHidden();

            grdOperationPoint.View.AddTextBoxColumn("RESOURCEVERSION", 100)
                .SetIsHidden();

            grdOperationPoint.View.AddTextBoxColumn("RESOURCETYPE", 100)
                .SetIsHidden();

            grdOperationPoint.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();

            grdOperationPoint.View.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden();

            grdOperationPoint.View.AddComboBoxColumn("INSPECTIONPOINTNAME", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspectionPointName", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdOperationPoint.View.AddSpinEditColumn("INSPECTIONQTY", 100);

            grdOperationPoint.View.AddSpinEditColumn("POINTQTY", 100);

            grdOperationPoint.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetValidationIsRequired()
               .SetDefault("Valid")
               .SetTextAlignment(TextAlignment.Center);

            grdOperationPoint.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdOperationPoint.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdOperationPoint.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdOperationPoint.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);


            grdOperationPoint.View.PopulateColumns();
        }
        #endregion

        #region 계측기 R&R 탭의 그리드 초기화

        /// <summary>        
        /// 수입검사(원자재) 검사 종류 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdMeasurementInspection()
        {
            // TODO : 그리드 초기화 로직 추가
            grdMeasurementInspection.GridButtonItem = GridButtonItem.None;
            grdMeasurementInspection.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdMeasurementInspection.View.SetIsReadOnly();

            grdMeasurementInspection.View.AddTextBoxColumn("INSPECTIONCLASSID", 150);

            grdMeasurementInspection.View.AddTextBoxColumn("INSPECTIONCLASSNAME", 200);

            grdMeasurementInspection.View.AddTextBoxColumn("INSPECTIONDEFID", 150);

            grdMeasurementInspection.View.AddTextBoxColumn("INSPECTIONDEFNAME", 200);

            grdMeasurementInspection.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 200);

            grdMeasurementInspection.View.AddTextBoxColumn("RESOURCETYPE", 200);

            //변경**
            grdMeasurementInspection.View.AddTextBoxColumn("INSPECTIONDEFSEGMENT", 200);

            grdMeasurementInspection.View.PopulateColumns();
        }

        /// <summary>        
        /// 수입검사(원자재) 검사품목 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdMeasurementConsumable()
        {
            // TODO : 그리드 초기화 로직 추가
            grdMeasurementResource.GridButtonItem = GridButtonItem.Add;
            grdMeasurementResource.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            InitializeGrdMeasurement_ResourcePopup();

            grdMeasurementResource.View.AddTextBoxColumn("RESOURCENAME", 200)
                .SetLabel("CONSUMABLEDEFNAME")
                .SetIsReadOnly();

            grdMeasurementResource.View.AddTextBoxColumn("RESOURCEVERSION", 150)
                .SetLabel("RESOURCEVERSION")
                .SetIsReadOnly();

            grdMeasurementResource.View.PopulateColumns();
        }
        /// <summary>
        /// 검사 품목을 선택하는 팝업
        /// </summary>
        private void InitializeGrdMeasurement_ResourcePopup()
        {
            //팝업 컬럼 설정
            var consumableDefId = grdMeasurementResource.View.AddSelectPopupColumn("RESOURCEID", new SqlQuery("GetConsumableDefList", "10001", "CONSUMABLECLASSID=RawMaterial", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                            .SetPopupLayout("CONSUMABLE", PopupButtonStyles.Ok_Cancel, true, false)
                                            .SetPopupResultCount(1)
                                            .SetLabel("CONSUMABLEDEFID")
                                            .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                            .SetPopupAutoFillColumns("CONSUMABLEDEFNAME")
                                            .SetValidationKeyColumn()
                                            .SetPopupResultMapping("RESOURCEID", "CONSUMABLEDEFID")
                                            .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                            {
                                                // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                                                // dataGridRow : 현재 Focus가 있는 그리드의 DataRow

                                                foreach (DataRow row in selectedRows)
                                                {
                                                    dataGridRow["RESOURCENAME"] = row["CONSUMABLEDEFNAME"].ToString();
                                                    dataGridRow["RESOURCEVERSION"] = row["CONSUMABLEDEFVERSION"].ToString();
                                                }

                                                _rowResource = dataGridRow;

                                            });

            consumableDefId.Conditions.AddTextBox("CONSUMABLEDEFID");

            consumableDefId.Conditions.AddTextBox("CONSUMABLEDEFNAME");

            // 팝업 그리드
            consumableDefId.GridColumns.AddTextBoxColumn("CONSUMABLEDEFID", 150);
            consumableDefId.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
            consumableDefId.GridColumns.AddTextBoxColumn("CONSUMABLEDEFVERSION", 200);
        }

        /// <summary>        
        /// 수입검사(원자재) 검사 방법 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdMeasurementInspItemClass()
        {
            // TODO : 그리드 초기화 로직 추가
            grdMeasurementInspItemClass.GridButtonItem = GridButtonItem.Add;
            grdMeasurementInspItemClass.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            //Relation을 위해 파라미터로 넘겨줄 컬럼
            grdMeasurementInspItemClass.View.AddTextBoxColumn("INSPECTIONCLASSID", 150)
                .SetIsHidden();

            grdMeasurementInspItemClass.View.AddTextBoxColumn("PARENTINSPITEMCLASSID", 150)
                .SetIsReadOnly();

            grdMeasurementInspItemClass.View.AddTextBoxColumn("PARENTINSPITEMCLASSNAME", 200)
                .SetIsReadOnly();

            InitializeGrdMeasurementInspItem_Popup();

            grdMeasurementInspItemClass.View.AddTextBoxColumn("INSPITEMCLASSNAME", 200)
                .SetIsReadOnly();

            grdMeasurementInspItemClass.View.PopulateColumns();
        }
        /// <summary>
        /// INSPITEMCLASSID를 검색하는 팝업
        /// </summary>
        private void InitializeGrdMeasurementInspItem_Popup()
        {
            var inspItemclassId = grdMeasurementInspItemClass.View.AddSelectPopupColumn("INSPITEMCLASSID", 150, new SqlQuery("GetInspItemByInspectionClassId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("INSPITEMCLASS", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetRelationIds("INSPECTIONCLASSID")
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetValidationKeyColumn()
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                    foreach (DataRow row in selectedRows)
                    {
                        dataGridRow["PARENTINSPITEMCLASSID"] = row["PARENTINSPITEMCLASSID"].ToString();
                        dataGridRow["PARENTINSPITEMCLASSNAME"] = row["PARENTINSPITEMCLASSNAME"].ToString();
                        dataGridRow["INSPITEMCLASSNAME"] = row["INSPITEMCLASSNAME"].ToString();
                    }

                    FocusedRowChange_grdInspItem(dataGridRow);
                });

            inspItemclassId.Conditions.AddTextBox("INSPITEMCLASSID");

            inspItemclassId.Conditions.AddTextBox("INSPITEMCLASSNAME");

            inspItemclassId.Conditions.AddTextBox("INSPECTIONCLASSID")
                .SetPopupDefaultByGridColumnId("INSPECTIONCLASSID")
                .SetIsHidden();

            inspItemclassId.GridColumns.AddTextBoxColumn("PARENTINSPITEMCLASSID", 150)
                .SetIsReadOnly();
            inspItemclassId.GridColumns.AddTextBoxColumn("PARENTINSPITEMCLASSNAME", 150)
                .SetIsReadOnly();
            inspItemclassId.GridColumns.AddTextBoxColumn("INSPITEMCLASSID", 150)
                .SetIsReadOnly();
            inspItemclassId.GridColumns.AddTextBoxColumn("INSPITEMCLASSNAME", 150)
                .SetIsReadOnly();
        }

        /// <summary>        
        /// 수입검사(원자재) 검사항목 연계 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdMeasurementRel()
        {
            // TODO : 그리드 초기화 로직 추가
            grdMeasurementRel.GridButtonItem = GridButtonItem.None;
            grdMeasurementRel.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdMeasurementRel.View.CheckMarkSelection.MultiSelectCount = 1;

            //inspectionItemRel, InspectionAttribute 테이블의 PK
            grdMeasurementRel.View.AddTextBoxColumn("INSPITEMCLASSID", 150)
                .SetIsHidden();

            grdMeasurementRel.View.AddTextBoxColumn("INSPITEMID", 150)
                .SetIsReadOnly();

            grdMeasurementRel.View.AddTextBoxColumn("INSPITEMNAME", 150)
                .SetIsReadOnly();

            grdMeasurementRel.View.AddTextBoxColumn("INSPITEMVERSION", 150)
                .SetIsReadOnly();

            grdMeasurementRel.View.AddTextBoxColumn("INSPECTIONDEFID", 150)
                .SetIsHidden();

            grdMeasurementRel.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 150)
                .SetIsHidden();

            grdMeasurementRel.View.AddTextBoxColumn("RESOURCETYPE", 150)
                .SetIsHidden();

            grdMeasurementRel.View.AddTextBoxColumn("RESOURCEID", 150)
                .SetIsHidden();

            grdMeasurementRel.View.AddTextBoxColumn("RESOURCEVERSION", 150)
                .SetIsHidden();

            grdMeasurementRel.View.AddTextBoxColumn("PLNATID", 100)
               .SetIsHidden();

            grdMeasurementRel.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();

            //inspectionItemRel 테이블
            grdMeasurementRel.View.AddTextBoxColumn("VALIDTYPE", 100);

            grdMeasurementRel.View.AddSpinEditColumn("TARGETVALUE", 100);

            grdMeasurementRel.View.AddSpinEditColumn("LOWERSPECLIMIT", 100);

            grdMeasurementRel.View.AddSpinEditColumn("UPPERSPECLIMIT", 100);

            grdMeasurementRel.View.AddSpinEditColumn("LOWERCONTROLLIMIT", 100);

            grdMeasurementRel.View.AddSpinEditColumn("UPPERCONTROLLIMIT", 100);

            grdMeasurementRel.View.AddSpinEditColumn("LOWERSCREENLIMIT", 100);

            grdMeasurementRel.View.AddSpinEditColumn("UPPERSCREENLIMIT", 100);
            //inspectionItemRel 단위 CODECLASSID 수정
            grdMeasurementRel.View.AddComboBoxColumn("UNIT", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            //InspectionAttribute 테이블
            grdMeasurementRel.View.AddComboBoxColumn("HASSPECSEQUENCE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();

            grdMeasurementRel.View.AddTextBoxColumn("SPECSEQUENCE", 150)
                .SetIsHidden();

            grdMeasurementRel.View.AddTextBoxColumn("SPECCLASSID", 150)
                .SetIsHidden();

            //InspectionAttribute 검사필수여부 CODECLASSID 수정
            grdMeasurementRel.View.AddComboBoxColumn("ISINSPECTIONREQUIRED", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdMeasurementRel.View.AddSpinEditColumn("LOTSIZE", 100);

            grdMeasurementRel.View.AddTextBoxColumn("QTYTYPE", 100);

            grdMeasurementRel.View.AddTextBoxColumn("INSPECTIONLEVEL", 100);

            grdMeasurementRel.View.AddSpinEditColumn("DEFECTLEVEL", 100);

            grdMeasurementRel.View.AddSpinEditColumn("INSPECTIONQTY", 100);

            grdMeasurementRel.View.AddTextBoxColumn("QTYUNIT", 100);

            grdMeasurementRel.View.AddTextBoxColumn("CYCLE", 100);

            grdMeasurementRel.View.AddTextBoxColumn("INSPECTORDEGREE", 100);

            grdMeasurementRel.View.AddTextBoxColumn("EQUIPMENTID", 100);

            grdMeasurementRel.View.AddTextBoxColumn("INSPECTIONUNIT", 100);

            grdMeasurementRel.View.AddTextBoxColumn("DEFECTRATE", 100);

            grdMeasurementRel.View.AddTextBoxColumn("DECISIONDEGREE", 100);
            grdMeasurementRel.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdMeasurementRel.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdMeasurementRel.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdMeasurementRel.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdMeasurementRel.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdMeasurementRel.View.PopulateColumns();
        }

        #endregion

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가

            //화면 로드 이벤트
            this.Load += InspectionItemRelationManagement_Load;
            //선택 된 탭이 바뀔 때 이벤트
            tabInspection.SelectedPageChanged += TabInspection_SelectedPageChanged;
            //Spec 버튼 클릭시 이벤트
            btnToSpec.Click += BtnToSpec_Click;

            #region 수입검사(원자재) 탭의 이벤트
            //grdRawInspection 그리드의 Focused Row가 바뀔 때 이벤트
            grdRawInspection.View.FocusedRowChanged += GrdInspection_FocusedRowChanged;
            //grdRawConsumable 그리드의 Row추가시 확인 이벤트
            grdRawConsumable.ToolbarAddingRow += View_ToolbarAddingRow;
            //grdRawConsumable 그리드의 Focused Row가 바뀔 때 이벤트
            grdRawConsumable.View.FocusedRowChanged += GrdResource_FocusedRowChanged;
            //grdRawInspItem 그리드의 Row추가시 확인 이벤트
            grdRawInspItemClass.ToolbarAddingRow += View_ToolbarAddingRow;
            //grdRawInspItem 그리드의 새 row 추가 이벤트
            grdRawInspItemClass.View.AddingNewRow += View_GrdRawInspItemClass_AddingNewRow;
            //grdRawInspItem 그리드의 Focused Row가 바뀔 때 이벤트
            grdRawInspItemClass.View.FocusedRowChanged += View_GrdInspItemClass_FocusedRowChanged;
            #endregion

            #region 수입검사(원자재가공품) 탭의 이벤트
            //grdSubassemblyInspection 그리드의 Focused Row가 바뀔 때 이벤트
            grdSubassemblyInspection.View.FocusedRowChanged += GrdInspection_FocusedRowChanged;
            //grdSubassemblyConsumable 그리드의 Row추가시 확인 이벤트
            grdSubassemblyConsumable.ToolbarAddingRow += View_ToolbarAddingRow;
            //grdSubassemblyConsumable 그리드의 Focused Row가 바뀔 때 이벤트
            grdSubassemblyConsumable.View.FocusedRowChanged += GrdResource_FocusedRowChanged;
            //grdSubassemblyInspItemClass 그리드의 Row추가시 확인 이벤트
            grdSubassemblyInspItemClass.ToolbarAddingRow += View_ToolbarAddingRow;
            //grdSubassemblyInspItemClass 그리드의 새 row 추가 이벤트
            grdSubassemblyInspItemClass.View.AddingNewRow += View_GrdRawInspItemClass_AddingNewRow;
            //grdSubassemblyInspItemClass 그리드의 Focused Row가 바뀔 때 이벤트
            grdSubassemblyInspItemClass.View.FocusedRowChanged += View_GrdInspItemClass_FocusedRowChanged;
            //grdSubassemblyInspItemClass 그리드의 CellValue가 바뀔 때 이벤트 
            grdSubassemblyInspItemClass.View.CellValueChanged += View_GrdInspItemClass_CellValueChanged;
            //grdSubassemblyRel 그리드의 Focused Row가 바뀔 때 이벤트
            grdSubassemblyRel.View.FocusedRowChanged += View_GrdRel_FocusedRowChanged;
            //grdSubassemblyPoint 그리드의 새 row 추가 이벤트
            grdSubassemblyPoint.View.AddingNewRow += View_GrdPoint_AddingNewRow;
            //grdSubassemblyPoint 그리드의 Row추가시 확인 이벤트
            grdSubassemblyPoint.ToolbarAddingRow += GrdPoint_ToolbarAddingRow;
            #endregion

            #region 수입검사(부자재/포장재) 탭의 이벤트
            //grdSubsidiaryInspection 그리드의 Focused Row가 바뀔 때 이벤트
            grdSubsidiaryInspection.View.FocusedRowChanged += GrdInspection_FocusedRowChanged;
            //grdSubsidiaryConsumable 그리드의 Row추가시 확인 이벤트
            grdSubsidiaryConsumable.ToolbarAddingRow += View_ToolbarAddingRow;
            //grdSubsidiaryConsumable 그리드의 Focused Row가 바뀔 때 이벤트
            grdSubsidiaryConsumable.View.FocusedRowChanged += GrdResource_FocusedRowChanged;
            //grdSubsidiaryInspItemClass 그리드의 Row추가시 확인 이벤트
            grdSubsidiaryInspItemClass.ToolbarAddingRow += View_ToolbarAddingRow;
            //grdSubsidiaryInspItemClass 그리드의 새 row 추가 이벤트
            grdSubsidiaryInspItemClass.View.AddingNewRow += View_GrdRawInspItemClass_AddingNewRow;
            //grdSubsidiaryInspItemClass 그리드의 Focused Row가 바뀔 때 이벤트
            grdSubsidiaryInspItemClass.View.FocusedRowChanged += View_GrdInspItemClass_FocusedRowChanged;
            //grdSubsidiaryInspItemClass 그리드의 CellValue가 바뀔 때 이벤트 
            grdSubsidiaryInspItemClass.View.CellValueChanged += View_GrdInspItemClass_CellValueChanged;
            //grdSubsidiaryRel 그리드의 Focused Row가 바뀔 때 이벤트
            grdSubsidiaryRel.View.FocusedRowChanged += View_GrdRel_FocusedRowChanged;
            //grdSubsidiaryPoint 그리드의 새 row 추가 이벤트
            grdSubsidiaryPoint.View.AddingNewRow += View_GrdPoint_AddingNewRow;
            //grdSubsidiaryPoint 그리드의 Row추가시 확인 이벤트
            grdSubsidiaryPoint.ToolbarAddingRow += GrdPoint_ToolbarAddingRow;
            #endregion

            #region 수입검사(공정) 탭의 이벤트
            //grdOSPInspection 그리드의 Focused Row가 바뀔 때 이벤트
            grdOSPInspection.View.FocusedRowChanged += GrdInspection_FocusedRowChanged;
            //grdOSPProduct 그리드의 Row추가시 확인 이벤트
            grdOSPProduct.ToolbarAddingRow += View_ToolbarAddingRow;
            //grdOSPProduct 그리드의 Focused Row가 바뀔 때 이벤트
            grdOSPProduct.View.FocusedRowChanged += GrdResource_FocusedRowChanged;
            //grdOSPInspItemClass 그리드의 Row추가시 확인 이벤트
            grdOSPInspItemClass.ToolbarAddingRow += View_ToolbarAddingRow;
            //grdOSPInspItemClass 그리드의 새 row 추가 이벤트
            grdOSPInspItemClass.View.AddingNewRow += View_GrdRawInspItemClass_AddingNewRow;
            //grdOSPInspItemClass 그리드의 Focused Row가 바뀔 때 이벤트
            grdOSPInspItemClass.View.FocusedRowChanged += View_GrdInspItemClass_FocusedRowChanged;
            //grdOSPInspItemClass 그리드의 CellValue가 바뀔 때 이벤트 
            grdOSPInspItemClass.View.CellValueChanged += View_GrdInspItemClass_CellValueChanged;
            //grdOSPRel 그리드의 Focused Row가 바뀔 때 이벤트
            grdOSPRel.View.FocusedRowChanged += View_GrdRel_FocusedRowChanged;
            //grdOSPPoint 그리드의 새 row 추가 이벤트
            grdOSPPoint.View.AddingNewRow += View_GrdPoint_AddingNewRow;
            //grdOSPPoint 그리드의 Row추가시 확인 이벤트
            grdOSPPoint.ToolbarAddingRow += GrdPoint_ToolbarAddingRow;
            #endregion

            #region 자주검사(입고) 탭의 이벤트
            //grdSelfTakeInspection 그리드의 Focused Row가 바뀔 때 이벤트
            grdSelfTakeInspection.View.FocusedRowChanged += GrdInspection_FocusedRowChanged;
            //grdSelfTakeInspItemClass 그리드의 Row추가시 확인 이벤트
            grdSelfTakeInspItemClass.ToolbarAddingRow += View_ToolbarAddingRow;
            //grdSelfTakeInspItemClass 그리드의 새 row 추가 이벤트
            grdSelfTakeInspItemClass.View.AddingNewRow += View_GrdRawInspItemClass_AddingNewRow;
            //grdSelfTakeInspItemClass 그리드의 Focused Row가 바뀔 때 이벤트
            grdSelfTakeInspItemClass.View.FocusedRowChanged += View_GrdInspItemClass_FocusedRowChanged;
            //grdSelfTakeInspItemClass 그리드의 CellValue가 바뀔 때 이벤트 
            grdSelfTakeInspItemClass.View.CellValueChanged += View_GrdInspItemClass_CellValueChanged;
            //grdSelfTakeRel 그리드의 Focused Row가 바뀔 때 이벤트
            grdSelfTakeRel.View.FocusedRowChanged += View_GrdRel_FocusedRowChanged;
            //grdSelfTakePoint 그리드의 새 row 추가 이벤트
            grdSelfTakePoint.View.AddingNewRow += View_GrdPoint_AddingNewRow;
            //grdSelfTakePoint 그리드의 Row추가시 확인 이벤트
            grdSelfTakePoint.ToolbarAddingRow += GrdPoint_ToolbarAddingRow;
            #endregion

            #region 자주검사(출고) 탭의 이벤트
            //grdSelfShipInspection 그리드의 Focused Row가 바뀔 때 이벤트
            grdSelfShipInspection.View.FocusedRowChanged += GrdInspection_FocusedRowChanged;
            //grdSelfShipInspItemClass 그리드의 Row추가시 확인 이벤트
            grdSelfShipInspItemClass.ToolbarAddingRow += View_ToolbarAddingRow;
            //grdSelfShipInspItemClass 그리드의 새 row 추가 이벤트
            grdSelfShipInspItemClass.View.AddingNewRow += View_GrdRawInspItemClass_AddingNewRow;
            //grdSelfShipInspItemClass 그리드의 Focused Row가 바뀔 때 이벤트
            grdSelfShipInspItemClass.View.FocusedRowChanged += View_GrdInspItemClass_FocusedRowChanged;
            //grdSelfShipInspItemClass 그리드의 CellValue가 바뀔 때 이벤트 
            grdSelfShipInspItemClass.View.CellValueChanged += View_GrdInspItemClass_CellValueChanged;
            //grdSelfShipRel 그리드의 Focused Row가 바뀔 때 이벤트
            grdSelfShipRel.View.FocusedRowChanged += View_GrdRel_FocusedRowChanged;
            //grdSelfShipPoint 그리드의 새 row 추가 이벤트
            grdSelfShipPoint.View.AddingNewRow += View_GrdPoint_AddingNewRow;
            //grdSelfShipPoint 그리드의 Row추가시 확인 이벤트
            grdSelfShipPoint.ToolbarAddingRow += GrdPoint_ToolbarAddingRow;
            #endregion

            #region 신뢰성검증 탭의 이벤트
            //grdReliabilityInspection 그리드의 Focused Row가 바뀔 때 이벤트
            grdReliabilityInspection.View.FocusedRowChanged += GrdInspection_FocusedRowChanged;
            //grdReliabilityInspItemClass 그리드의 Row추가시 확인 이벤트
            grdReliabilityInspItemClass.ToolbarAddingRow += View_ToolbarAddingRow;
            //grdReliabilityInspItemClass 그리드의 새 row 추가 이벤트
            grdReliabilityInspItemClass.View.AddingNewRow += View_GrdRawInspItemClass_AddingNewRow;
            //grdReliabilityInspItemClass 그리드의 Focused Row가 바뀔 때 이벤트
            grdReliabilityInspItemClass.View.FocusedRowChanged += View_GrdInspItemClass_FocusedRowChanged;
            //grdReliabilityInspItemClass 그리드의 CellValue가 바뀔 때 이벤트 
            grdReliabilityInspItemClass.View.CellValueChanged += View_GrdInspItemClass_CellValueChanged;
            //grdReliabilityRel 그리드의 Focused Row가 바뀔 때 이벤트
            grdReliabilityRel.View.FocusedRowChanged += View_GrdRel_FocusedRowChanged;
            //grdReliabilityPoint 그리드의 새 row 추가 이벤트
            grdReliabilityPoint.View.AddingNewRow += View_GrdPoint_AddingNewRow;
            //grdReliabilityPoint 그리드의 Row추가시 확인 이벤트
            grdReliabilityPoint.ToolbarAddingRow += GrdPoint_ToolbarAddingRow;
            #endregion

            #region  최종검사 탭의 이벤트
            //grdFinishInspection 그리드의 Focused Row가 바뀔 때 이벤트
            grdFinishInspection.View.FocusedRowChanged += GrdInspection_FocusedRowChanged;
            //grdFinishProduct 그리드의 Row추가시 확인 이벤트
            grdFinishProduct.ToolbarAddingRow += View_ToolbarAddingRow;
            //grdFinishProduct 그리드의 Focused Row가 바뀔 때 이벤트
            grdFinishProduct.View.FocusedRowChanged += GrdResource_FocusedRowChanged;
            //grdFinishInspItemClass 그리드의 Row추가시 확인 이벤트
            grdFinishInspItemClass.ToolbarAddingRow += View_ToolbarAddingRow;
            //grdFinishInspItemClass 그리드의 새 row 추가 이벤트
            grdFinishInspItemClass.View.AddingNewRow += View_GrdRawInspItemClass_AddingNewRow;
            //grdFinishInspItemClass 그리드의 Focused Row가 바뀔 때 이벤트
            grdFinishInspItemClass.View.FocusedRowChanged += View_GrdInspItemClass_FocusedRowChanged;
            //grdFinishInspItemClass 그리드의 CellValue가 바뀔 때 이벤트 
            grdFinishInspItemClass.View.CellValueChanged += View_GrdInspItemClass_CellValueChanged;
            //grdFinishRel 그리드의 Focused Row가 바뀔 때 이벤트
            grdFinishRel.View.FocusedRowChanged += View_GrdRel_FocusedRowChanged;
            //grdFinishPoint 그리드의 새 row 추가 이벤트
            grdFinishPoint.View.AddingNewRow += View_GrdPoint_AddingNewRow;
            //grdFinishPoint 그리드의 Row추가시 확인 이벤트
            grdFinishPoint.ToolbarAddingRow += GrdPoint_ToolbarAddingRow;
            #endregion

            #region 출하검사 탭의 이벤트
            //grdShipmentInspection 그리드의 Focused Row가 바뀔 때 이벤트
            grdShipmentInspection.View.FocusedRowChanged += GrdInspection_FocusedRowChanged;
            //grdShipmentProduct 그리드의 Row추가시 확인 이벤트
            grdShipmentProduct.ToolbarAddingRow += View_ToolbarAddingRow;
            //grdShipmentProduct 그리드의 Focused Row가 바뀔 때 이벤트
            grdShipmentProduct.View.FocusedRowChanged += GrdResource_FocusedRowChanged;
            //grdShipmentInspItemClass 그리드의 Row추가시 확인 이벤트
            grdShipmentInspItemClass.ToolbarAddingRow += View_ToolbarAddingRow;
            //grdShipmentInspItemClass 그리드의 새 row 추가 이벤트
            grdShipmentInspItemClass.View.AddingNewRow += View_GrdRawInspItemClass_AddingNewRow;
            //grdShipmentInspItemClass 그리드의 Focused Row가 바뀔 때 이벤트
            grdShipmentInspItemClass.View.FocusedRowChanged += View_GrdInspItemClass_FocusedRowChanged;
            //grdShipmentInspItemClass 그리드의 CellValue가 바뀔 때 이벤트 
            grdShipmentInspItemClass.View.CellValueChanged += View_GrdInspItemClass_CellValueChanged;
            //grdShipmentRel 그리드의 Focused Row가 바뀔 때 이벤트
            grdShipmentRel.View.FocusedRowChanged += View_GrdRel_FocusedRowChanged;
            //grdShipmentPoint 그리드의 새 row 추가 이벤트
            grdShipmentPoint.View.AddingNewRow += View_GrdPoint_AddingNewRow;
            //grdShipmentPoint 그리드의 Row추가시 확인 이벤트
            grdShipmentPoint.ToolbarAddingRow += GrdPoint_ToolbarAddingRow;
            #endregion

            #region 계측검사 탭의 이벤트
            //grdMeasurementInspection 그리드의 Focused Row가 바뀔 때 이벤트
            grdOperationInspection.View.FocusedRowChanged += GrdInspection_FocusedRowChanged;
            //grdMeasurementProduct 그리드의 Focused Row가 바뀔 때 이벤트
            grdOperationProduct.View.FocusedRowChanged += GrdResource_FocusedRowChanged;
            //grdMeasurementInspItemClass 그리드의 Focused Row가 바뀔 때 이벤트
            grdOperationInspItemClass.View.FocusedRowChanged += View_GrdInspItemClass_FocusedRowChanged;
            //grdMeasurementRel 그리드의 Focused Row가 바뀔 때 이벤트
            grdOperationRel.View.FocusedRowChanged += View_GrdRel_FocusedRowChanged;
            #endregion

            #region 계측기 R&R 탭의 이벤트 홀딩하라고 하심 - 바뀔듯 :: 초기화 까지만 작업함
            //grdRawInspection 그리드의 Focused Row가 바뀔 때 이벤트
            grdRawInspection.View.FocusedRowChanged += GrdInspection_FocusedRowChanged;
            //grdRawConsumable 그리드의 Row추가시 확인 이벤트
            grdRawConsumable.ToolbarAddingRow += View_ToolbarAddingRow;
            //grdRawConsumable 그리드의 Focused Row가 바뀔 때 이벤트
            grdRawConsumable.View.FocusedRowChanged += GrdResource_FocusedRowChanged;
            //grdRawInspItem 그리드의 Row추가시 확인 이벤트
            grdRawInspItemClass.ToolbarAddingRow += View_ToolbarAddingRow;
            //grdRawInspItem 그리드의 새 row 추가 이벤트
            grdRawInspItemClass.View.AddingNewRow += View_GrdRawInspItemClass_AddingNewRow;
            //grdRawInspItem 그리드의 Focused Row가 바뀔 때 이벤트
            grdRawInspItemClass.View.FocusedRowChanged += View_GrdInspItemClass_FocusedRowChanged;
            #endregion 
        }

        /// <summary>
        /// Rel 그리드의 row하나를 선택하고 Spec버튼을 클릭했을 때 이벤트
        /// 1)Rel 의 Key 값이 없는 경우 specDefinition 테이블을 조회하여 
        /// 2)_1 등록되어있다면 SpecSequence를 inspectionitemrel 테이블에 update
        /// 2)_2 등록되어있지 않다면 체크된 Rel row를 Spec 등록하는 화면으로 이동시켜준다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnToSpec_Click(object sender, EventArgs e)
        {
            if (_grdRel.View.GetCheckedRows().Rows.Count != 1)
            {//Rel그리드의 선택된 항목이 없을 때
                this.ShowMessage("NoSelectRelDataRow");// 검사 항목 연계를 선택 해 주세요.
                return;
            }

            DataRow checkedRow = _grdRel.View.GetCheckedRows().Rows[0];

            if (checkedRow.RowState.ToString().Equals("Modified"))
            {//Rel그리드의 선택된 항목이 저장되지 않았을 때
                this.ShowMessage("NeedToSaveRelDataRow");// 검사 항목 연계를 우선 저장 해 주세요.
                return;
            }


            if (string.IsNullOrWhiteSpace(checkedRow["SPECSEQUENCE"].ToString()))
            {//선택 된 Rel 그리드의 등록된 SPECSEQUENCE가 없을 때

                DataTable specTable = null;
                Dictionary<string, object> values = new Dictionary<string, object>();
                //values.Add("ENTERPRISEID", Framework.UserInfo.Current.Enterprise);

                if (_tabIndex == 0 || _tabIndex == 1 || _tabIndex == 2)
                {//원자재 스펙 : Key : ConsumableDefId, ParentInspItemclassId, inspItemClassId, InspItemId 
                    values.Add("CONSUMABLEDEFID", checkedRow["RESOURCEID"]);
                    values.Add("INSPITEMID", checkedRow["INSPITEMID"]);
                    values.Add("INSPITEMCLASSID", checkedRow["INSPITEMCLASSID"]);
                    values.Add("PARENTINSPITEMCLASSID", checkedRow["PARENTINSPITMCLASSID"]);

                    if (_tabIndex == 0)
                    {//원자재
                        values.Add("SPECCLASSID", "RawSpec");
                    }
                    else if (_tabIndex == 1)
                    {//가공품
                        values.Add("SPECCLASSID", "SubassemblyInspection");
                    }
                    else if (_tabIndex == 2)
                    {//부자재
                        values.Add("SPECCLASSID", "SubsidiaryInspection");
                    }
                             
                }
                else if (_tabIndex == 3 || _tabIndex == 8)
                {//OSP, Shipment 스펙 : Key : ProcessSegmentClassId(중공정), ProductDefId, inspItemClassId, InspItemId 
                    values.Add("PRODUCTDEFID", checkedRow["RESOURCEID"]);
                    values.Add("INSPITEMID", checkedRow["INSPITEMID"]);
                    values.Add("INSPITEMCLASSID", checkedRow["INSPITEMCLASSID"]);
                    values.Add("PROCESSSEGMENTCLASSID", _rowInspection["INSPECTIONDEFSEGMENT"]);

                    if (_tabIndex == 3)
                    {//OSP
                        values.Add("SPECCLASSID", "OSPSpec");
                    }
                    else if (_tabIndex == 8)
                    {//Shipment
                        values.Add("SPECCLASSID", "ShipmentSpec");
                    }
                }
                else if (_tabIndex == 9)
                {//Operation 스펙 : Key : ProcessSegmentId(표준공정), ProductDefId, InspItemId
                    values.Add("PRODUCTDEFID", checkedRow["RESOURCEID"]);
                    values.Add("INSPITEMID", checkedRow["INSPITEMID"]);
                    values.Add("PROCESSSEGMENTID", _rowInspection["INSPECTIONDEFSEGMENT"]);
                    values.Add("SPECCLASSID", "OperationSpec");
                }
                else if (_tabIndex == 10)
                {//Measurement 스펙 : Key : ProcessSegmentId(표준공정), EquipmentId, inspItemClassId, InspItemId Rel에 설비정보 없는데..?
                 //HOLDING
                }

                specTable = SqlExecuter.Query("SelectSpecByRelInfo", "10001", values);

                if (specTable.Rows.Count == 0)
                {//등록 된 spec정보가 없을때
                    DialogResult result = System.Windows.Forms.DialogResult.No;

                    result = this.ShowMessage(MessageBoxButtons.YesNo, "SpecNotRegistered");//해당 정보의 Spec이 등록 되어있지 않습니다. Spec등록 화면으로 이동 하시겠습니까?

                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        try
                        {
                            DialogManager.ShowWaitDialog();

                            Dictionary<string, object> param = new Dictionary<string, object>();
                            param.Add("row", _grdRel.View.GetCheckedRows().Rows[0]);
                            param.Add("tabIndex", _tabIndex);
                            param.Add("fromMenu", "Inspections");
                            
                            

                            this.OpenMenu("PG-SD-0440", param);
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

                }
                else
                {//등록된 spec 정보가 있을 때
                    DialogResult result = System.Windows.Forms.DialogResult.No;

                    result = this.ShowMessage(MessageBoxButtons.YesNo, "SpecSequenceConfirmNote");//등록된 Spec 정보가 있습니다. 검사 항목 연계와 연동 하시겠습니까?

                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        try
                        {
                            checkedRow["SPECSEQUENCE"] = specTable.Rows[0]["SPECSEQUENCE"];
                            checkedRow["SPECCLASSID"] = specTable.Rows[0]["SPECCLASSID"];
                            DataTable changed = _grdRel.View.GetCheckedRows().Clone();
                            changed.ImportRow(checkedRow);
                            ExecuteRule("SaveInspectionItemAttributeSpecSequence", changed);

                            ShowMessage("SuccessSave");

                            _grdInspection.View.ClearDatas();
                            LoadDataInspection();
                        }
                        catch (Exception ex)
                        {
                            this.ShowError(ex);
                        }
                
                    }

                }

            }
            else
            {//선택 된 Rel 그리드의 등록된 SPECSEQUENCE가 있을 때
                this.ShowMessage("HasSpecSequence");// 스펙정보가 이미 연동되었습니다.
                return;
            }
        }

        /// <summary>
        /// 측정시기를 입력할때 검사 연계 데이터가 저장되있지않으면(new Row) 일때 측정시기 등록을 막는 이벤트 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdPoint_ToolbarAddingRow(object sender, CancelEventArgs e)
        {
            if (_rowRel != null && string.IsNullOrWhiteSpace(_rowRel["CREATEDTIME"].ToString()))
            {
                e.Cancel = true;
                throw MessageException.Create("MustSaveRelData");
            }
        }

        /// <summary>
        /// InspItemClassId 값이 바뀔 때 InspItemName에 값 입력,
        /// AddRow를 _InspItemRow에 할당
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_GrdInspItemClass_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.Equals("INSPITEMCLASSID"))
            {
                SmartBandedGridView grid = sender as SmartBandedGridView;
                if (e.Column.ColumnEdit is RepositoryItemLookUpEdit lookup)
                {
                    object inspItemClassName = lookup.GetDataSourceValue("INSPITEMCLASSNAME", lookup.GetDataSourceRowIndex("INSPITEMCLASSID", e.Value));
                    grid.SetFocusedRowCellValue("INSPITEMCLASSNAME", inspItemClassName);  
                }
                _rowInspItemclass = grid.GetDataRow(e.RowHandle);

                FocusedRowChange_grdInspItem(_rowInspItemclass);

            }
        }

        /// <summary>
        /// Point 그리드에 새로운 Row를 추가 할때 Rel그리드의 값 넘겨주기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_GrdPoint_AddingNewRow(SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            args.NewRow["INSPITEMID"] = _rowRel["INSPITEMID"];
            args.NewRow["INSPITEMVERSION"] = _rowRel["INSPITEMVERSION"];
            args.NewRow["INSPITEMCLASSID"] = _rowRel["INSPITEMCLASSID"];
            args.NewRow["INSPECTIONDEFID"] = _rowRel["INSPECTIONDEFID"];
            args.NewRow["INSPECTIONDEFVERSION"] = _rowRel["INSPECTIONDEFVERSION"];
            args.NewRow["RESOURCEID"] = _rowRel["RESOURCEID"];
            args.NewRow["RESOURCEVERSION"] = _rowRel["RESOURCEVERSION"];
            args.NewRow["RESOURCETYPE"] = _rowRel["RESOURCETYPE"];
            args.NewRow["PLANTID"] = _rowRel["PLANTID"];
            args.NewRow["ENTERPRISEID"] = _rowRel["ENTERPRISEID"];
        }

        /// <summary>
        /// Rel 그리드의 정보를 파라미터로 하여 InspectionPoint 테이블을 조회하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_GrdRel_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SmartBandedGridView grid = sender as SmartBandedGridView;
            if (grid == null) return;
            _rowRel = grid.GetFocusedDataRow();
            if (_rowRel == null) return;

            _grdPoint.View.ClearDatas();
  
            _grdPoint.DataSource = SqlExecuter.Query("SelectInspectionPointByRelInfo", "10001", SetSearchPointParameter());
        }

        /// <summary>
        /// InspItemClass 그리드의 InspItemClass정보를 파라미터로 하여 등록된 InspectionItemRel, InspectionAttribute 정보를 조회하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_GrdInspItemClass_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SmartBandedGridView grid = sender as SmartBandedGridView;
            if (grid == null) return;
            _rowInspItemclass = grid.GetFocusedDataRow();
            if (_rowInspItemclass == null) return;

            //상위 조건이 바뀌면 하위조건 없애주기 위함
            _rowRel = null;
            _grdRel.View.ClearDatas();

            FocusedRowChange_grdInspItem(_rowInspItemclass);
        }     

        /// <summary>
        /// Resource 그리드의 Resource정보를 파라미터로 하여 등록된 InspItemClass 정보를 조회하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdResource_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SmartBandedGridView grid = sender as SmartBandedGridView;
            if (grid == null) return;
            _rowResource = grid.GetFocusedDataRow();
            if (_rowResource == null) return;

            //상위 조건이 바뀌면 하위조건 없애주기 위함
            _rowInspItemclass = null;
            _grdItemClass.View.ClearDatas();

            _grdItemClass.DataSource = SqlExecuter.Query("SelectInspItemClassByResourceInfo", "10001", SetSearchParameter());
        }

        /// <summary>
        /// Inspection 그리드의 Inspection정보 를 파라미터로 하여 등록된 Resource 정보를 조회하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdInspection_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SmartBandedGridView grid = sender as SmartBandedGridView;
            if (grid == null) return;

            _rowInspection = null;
            _rowInspection = grid.GetFocusedDataRow();
            if (_rowInspection == null) return;

            if (_tabIndex == 0 || _tabIndex == 1 || _tabIndex == 2 ||  _tabIndex == 3 || _tabIndex == 7 || _tabIndex == 8 || _tabIndex == 9)
            {//검사 품목이 있을 때
                //상위 조건이 바뀌면 하위조건 없애주기 위함
                _rowResource = null;
                _grdResource.View.ClearDatas();
                _grdItemClass.View.ClearDatas();
                _grdRel.View.ClearDatas();

                if (_tabIndex != 0)
                {
                    _grdPoint.View.ClearDatas();
                }
                _grdResource.DataSource = SqlExecuter.Query("SelectResourceItemByInspectionDefInfo", "10001", SetSearchResourceParameter());
            }
            else
            {//검사 품목이 없을 때
                //상위 조건이 바뀌면 하위조건 없애주기 위함
                _rowInspItemclass = null;
                _grdItemClass.View.ClearDatas();
                _grdRel.View.ClearDatas();
                _grdPoint.View.ClearDatas();
                _grdItemClass.DataSource = SqlExecuter.Query("SelectInspItemClassByResourceInfo", "10001", SetSearchParameter());
            }
        }

        /// <summary>
        /// INSPECTIONCLASSID(파라메터로 필요) 컬럼 에 값을 자동으로 넣어주는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_GrdRawInspItemClass_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {          
            args.NewRow["INSPECTIONCLASSID"] = _rowInspection["INSPECTIONCLASSID"];
        }

        /// <summary>
        /// 새로 추가 한 Row가 이미 있는 경우 메세지 띄운후 Row추가 여부를 정하는 이벤트 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_ToolbarAddingRow(object sender, CancelEventArgs e)
        {
            SmartBandedGrid grid = sender as SmartBandedGrid;
            if (grid == null) return;
            var rowState = (grid.DataSource as DataTable).Rows.Cast<DataRow>()
                .Select(r => r.RowState.ToString()).ToList();

            //추가 한 후 저장하지 않은 데이터가 있을 때
            if (rowState.Contains("Added"))
            {
                DialogResult result = System.Windows.Forms.DialogResult.No;

                result = this.ShowMessage(MessageBoxButtons.YesNo, "DataIsNotSaved");//새로운 항목을 추가 할 경우 입력한 정보는 저장 되지 않습니다. 계속 하시겠습니까?

                if (result == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// 탭이 전환될 때 처음 여는 탭 일경우 
        /// 초기화, 검사 종류 그리드에 데이터를 바인딩, _tabIndex에 현재 탭인덱스 할당 해주는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabInspection_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            _tabIndex = tabInspection.SelectedTabPageIndex;

            //탭이 바뀔때 선택된 Row 초기화
            _rowInspection = null;
            _rowResource = null;
            _rowInspItemclass = null;
            _rowRel = null;

            SetGridVariable();

            //처음으로 탭이 선택될 때만 초기화 및 데이터 바인딩
            if (_tabIndex == 1 && !grdSubassemblyInspection.View.IsInitializeColumns)
            {//수입검사(원자재가공품) 탭의 그리드들이 초기화 안되었을때
                InitializeGrdSubassemblyInspection();
                InitializeGrdSubassemblyConsumable();
                InitializeGrdSubassemblyInspItemClass();
                InitializeGrdSubassemblyRel();
                InitializeGrdSubassemblyPoint();              
            }
            else if (_tabIndex ==2 && !grdSubsidiaryInspection.View.IsInitializeColumns)
            {
                InitializeGrdSubsidiaryInspection();
                InitializeGrdSubsidiaryConsumable();
                InitializeGrdSubsidiaryInspItemClass();
                InitializeGrdSubsidiaryRel();
                InitializeGrdSubsidiaryPoint();
            }
            else if (_tabIndex == 3 && !grdOSPInspection.View.IsInitializeColumns)
            {
                InitializeGrdOSPInspection();
                InitializeGrdOSPInspItemClass();
                InitializeGrdOSPProduct();
                InitializeGrdOSPRel();
                InitializeGrdOSPPoint();
            }
            else if (_tabIndex == 4 && !grdSelfTakeInspection.View.IsInitializeColumns)
            {
                InitializeGrdSelfTakeInspection();
                InitializeGrdSelfTakeInspItemClass();
                InitializeGrdSelfTakeRel();
                InitializeGrdSelfTakePoint();
            }
            else if (_tabIndex == 5 && !grdSelfShipInspection.View.IsInitializeColumns)
            {
                InitializeGrdSelfShipInspection();
                InitializeGrdSelfShipInspItemClass();
                InitializeGrdSelfShipRel();
                InitializeGrdSelfShipPoint();
            }
            else if (_tabIndex == 6 && !grdReliabilityInspection.View.IsInitializeColumns)
            {
                InitializeGrdReliabilityInspection();
                InitializeGrdReliabilityInspItemClass();
                InitializeGrdReliabilityRel();
                InitializeGrdReliabilityPoint();
            }
            else if (_tabIndex == 7 && !grdFinishInspection.View.IsInitializeColumns)
            {
                InitializeGrdFinishInspection();
                InitializeGrdFinishProduct();
                InitializeGrdFinishInspItemClass();
                InitializeGrdFinishRel();
                InitializeGrdFinishPoint();
            }
            else if (_tabIndex == 8 && !grdShipmentInspection.View.IsInitializeColumns)
            {
                InitializeGrdShipmentInspection();
                InitializeGrdShipmentProduct();
                InitializeGrdShipmentInspItemClass();
                InitializeGrdShipmentRel();
                InitializeGrdShipmentPoint();
            }
            else if (_tabIndex == 9 && !grdOperationInspection.View.IsInitializeColumns)
            {
                InitializeGrdOperationInspection();
                InitializeGrdOperationProduct();
                InitializeGrdOperationInspItemClass();
                InitializeGrdOperationRel();
                InitializeGrdOperationPoint();
            }
            else if (_tabIndex == 10 && !grdMeasurementInspection.View.IsInitializeColumns)
            {
                InitializeGrdMeasurementInspection();
                InitializeGrdMeasurementConsumable();
                InitializeGrdMeasurementInspItemClass();
                InitializeGrdMeasurementRel();
            }

            if ( _tabIndex == 7 || _tabIndex == 10)
            {//계측기 R&R _tabIndex ==10 빠질수도 있어서 버튼 비활성화
                btnToSpec.Enabled = false;
            }
            else
            {
                btnToSpec.Enabled = true;
            }

            LoadDataInspection();
        }

        private void InspectionItemRelationManagement_Load(object sender, EventArgs e)
        {
            //수입검사(원자재) 검사 종류 리스트 그리드를 초기화
            InitializeGrdRawInspection();
            //수입검사(원자재) 검사 방법 리스트 그리드를 초기화
            InitializeGrdRawInspItemClass();
            //수입검사(원자재) 검사품목 리스트 그리드를 초기화
            InitializeGrdRawConsumable();
            //수입검사(원자재) 검사항목 연계 리스트 그리드를 초기화
            InitializeGrdRawRel();
            //그리드 전역변수에 그리드 할당
            InitializeVariable();
            //각 탭의 검사 종류 리스트 그리드의 데이터 바인딩
            LoadDataInspection();
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            DataTable relChanged = null;
            DataTable pointChanged = null;

            if (_tabIndex == 0)
            {//품목만 있는 탭
                relChanged = SaveGrdRelWithResoueceItem(_grdRel.GetChangedRows());
                relChanged.TableName = "list";
                ExecuteRule("SaveInspectionItemRelAttributePoint", relChanged);
            }
            else if(_tabIndex == 1 || _tabIndex == 2 || _tabIndex == 3 || _tabIndex == 7 || _tabIndex == 8 || _tabIndex == 9)
            {//품목, 검사시기가있고 ,품목그리드의 검사 추가 정보 업데이트 해야하는 경우

                relChanged = SaveGrdRelWithResoueceItemAndUpdateInspectionInfo(_grdRel.GetChangedRows());
                pointChanged = _grdPoint.GetChangedRows();

                DataSet rullSet = new DataSet();

                relChanged.TableName = "list";
                pointChanged.TableName = "pointList";


                if (relChanged.Rows.Count > 0)
                {
                    rullSet.Tables.Add(relChanged);
                }

                if (pointChanged.Rows.Count > 0)
                {
                    rullSet.Tables.Add(pointChanged);
                }

                ExecuteRule("SaveInspectionItemRelAttributePointWithInspectionInfo", rullSet);

            } else if ( _tabIndex == 4 || _tabIndex == 5 || _tabIndex == 6)
            {//품목 없고 검사시기 만 있는 경우
                relChanged = SaveGrdRel(_grdRel.GetChangedRows());
                pointChanged = _grdPoint.GetChangedRows();

                DataSet rullSet = new DataSet();

                relChanged.TableName = "list";
                pointChanged.TableName = "pointList";

                if (relChanged.Rows.Count > 0)
                {
                    rullSet.Tables.Add(relChanged);
                }

                if (pointChanged.Rows.Count > 0)
                {
                    rullSet.Tables.Add(pointChanged);
                }

                ExecuteRule("SaveInspectionItemRelAttributePoint", rullSet);
            }

        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            _grdInspection.View.ClearDatas();
            LoadDataInspection();
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용

        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            DataTable changed = null;
            DataTable changed2 = null;

            if (_tabIndex == 0)
            {//검사시기가 없는 탭 
                _grdRel.View.CheckValidation();
                changed = _grdRel.GetChangedRows();
                CheckChangedDataOneGrd(changed);
            }
            else
            {//검사시기가 있는 탭
                //rel 그리드
                _grdRel.View.CheckValidation();
                changed = _grdRel.GetChangedRows();

                //point 그리드
                _grdPoint.View.CheckValidation();
                changed2 = _grdPoint.GetChangedRows();
                CheckChangedDataTwoGrd(changed, changed2);

            }
         
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 검사 종류의 데이터를 바인딩 시키는 함수
        /// </summary>
        private void LoadDataInspection()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            values.Add("TABINDEX",_tabIndex);
            DataTable dt = SqlExecuter.Query("SelectInspectionByTabIndex", "10001", values);
            CheckHasData(dt);

            _grdInspection.DataSource = dt;
            _rowInspection = _grdInspection.View.GetFocusedDataRow();

        }

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
        /// InspectionItemRel, inspectionAttribute 테이블의 정보를 조회하는 함수
        /// </summary>
        /// <param name="inspectionDefId"></param>
        /// <param name="inspectionDefVersion"></param>
        /// <param name="resourceId"></param>
        /// <param name="resourceVersion"></param>
        /// <param name="resourceType"></param>
        /// <param name="inspItemClassId"></param>
        /// <returns></returns>
        private DataTable Biding_GrdRel(Dictionary<string,object> values)
        {
            values.Add("P_INSPITEMCLASSID", _rowInspItemclass["INSPITEMCLASSID"]);
            DataTable dt = SqlExecuter.Query("SelectInspectionItemRelByInspItemClass", "10001", values);

            return dt;
        }

        /// <summary>
        /// 검사 품목이 있는 경우 InspectionItemRel, InspectionAttribute 테이블에
        /// 저장 하기전, 각각 그리드의 선택된 항목을 추가 해 주는 함수(품목그리드의 검사정보를 같이 저장하는 경우)
        /// </summary>
        /// <param name="changed"></param>
        /// <returns></returns>
        private DataTable SaveGrdRelWithResoueceItemAndUpdateInspectionInfo(DataTable changed)
        {
            foreach (DataRow row in changed.Rows)
            {
                row["INSPECTIONDEFID"] = _rowInspection["INSPECTIONDEFID"];
                row["INSPECTIONDEFVERSION"] = _rowInspection["INSPECTIONDEFVERSION"];
                row["RESOURCEID"] = _rowResource["RESOURCEID"];
                row["RESOURCEVERSION"] = _rowResource["RESOURCEVERSION"];
                row["QTYTYPE"] = _rowResource["QTYTYPE"];
                row["INSPECTORDEGREE"] = _rowResource["INSPECTORDEGREE"];
                row["INSPECTIONLEVEL"] = _rowResource["INSPECTIONLEVEL"];
                row["DEFECTLEVEL"] = _rowResource["DEFECTLEVEL"];
                row["RESOURCETYPE"] = _rowInspection["RESOURCETYPE"];
                row["INSPITEMCLASSID"] = _rowInspItemclass["INSPITEMCLASSID"];
                row["PLANTID"] = Framework.UserInfo.Current.Plant;
                row["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;
            }

            return changed;
        }

        /// <summary>
        /// 검사 품목이 있는 경우 InspectionItemRel, InspectionAttribute 테이블에
        /// 저장 하기전, 각각 그리드의 선택된 항목을 추가 해 주는 함수(품목그리드의 검사정보를 같이 저장하지 않을때)
        /// </summary>
        /// <param name="changed"></param>
        /// <returns></returns>
        private DataTable SaveGrdRelWithResoueceItem(DataTable changed)
        {
            foreach (DataRow row in changed.Rows)
            {
                row["INSPECTIONDEFID"] = _rowInspection["INSPECTIONDEFID"];
                row["INSPECTIONDEFVERSION"] = _rowInspection["INSPECTIONDEFVERSION"];
                row["RESOURCEID"] = _rowResource["RESOURCEID"];
                row["RESOURCEVERSION"] = _rowResource["RESOURCEVERSION"];
                row["RESOURCETYPE"] = _rowInspection["RESOURCETYPE"];
                row["INSPITEMCLASSID"] = _rowInspItemclass["INSPITEMCLASSID"];
                row["PLANTID"] = Framework.UserInfo.Current.Plant;
                row["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;
            }

            return changed;
        }

        /// <summary>
        /// 검사 품목이 없는 경우 InspectionItemRel, InspectionAttribute 테이블에
        /// 저장 하기전, 각각 그리드의 선택된 항목을 추가 해 주는 함수
        /// </summary>
        /// <param name="changed"></param>
        /// <returns></returns>
        private DataTable SaveGrdRel(DataTable changed)
        {
            foreach (DataRow row in changed.Rows)
            {
                row["INSPECTIONDEFID"] = _rowInspection["INSPECTIONDEFID"];
                row["INSPECTIONDEFVERSION"] = _rowInspection["INSPECTIONDEFVERSION"];
                //변경**
                //row["RESOURCEID"] = _rowInspection["INSPECTIONDEFID"];
                //row["RESOURCEVERSION"] = _rowInspection["INSPECTIONDEFVERSION"];
                row["RESOURCEID"] = _rowInspection["INSPECTIONDEFSEGMENT"];
                row["RESOURCEVERSION"] = "0";
                row["RESOURCETYPE"] = _rowInspection["RESOURCETYPE"];
                row["INSPITEMCLASSID"] = _rowInspItemclass["INSPITEMCLASSID"];
                row["PLANTID"] = Framework.UserInfo.Current.Plant;
                row["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;
            }

            return changed;
        }

        /// <summary>
        /// Point그리드를 입력하지 않는 경우 유효성 검사 함수
        /// </summary>
        /// <param name="table"></param>
        private void CheckChangedDataOneGrd(DataTable table)
        {
            if (table.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        /// <summary>
        /// Point 그리드가 있는 경우 유효성 검사 함수
        /// 검사 항목이 우선 저장된 후에 측정 포인트 저장 해야함
        /// </summary>
        /// <param name="table"></param>
        /// <param name="table2"></param>
        private void CheckChangedDataTwoGrd(DataTable table, DataTable table2)
        {
            if (table.Rows.Count == 0 && table2.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }       
        }

        /// <summary>
        /// grdInspItemClass 그리드의 포커스된 Row가 바뀔 때 Rel 그리드를 조회 하는 함수
        /// </summary>
        /// <param name="row"></param>
        private void FocusedRowChange_grdInspItem(DataRow row)
        {
            _grdRel.DataSource = Biding_GrdRel(SetSearchParameter());
        }

        /// <summary>
        /// 그리드 전역변수를 초기화 하는 함수
        /// </summary>
        private void InitializeVariable()
        {
            _grdInspection = grdRawInspection;
            _grdResource = grdRawConsumable;
            _grdItemClass = grdRawInspItemClass;
            _grdRel = grdRawRel;
        }

        /// <summary>
        /// 탭이 바뀔 때 마다 그리드 전역변수에 해당 탭의 그리드를 할당해주는 함수
        /// </summary>
        private void SetGridVariable()
        {
            switch (_tabIndex)
            {
                case 0://수입검사(원자재)    
                    _grdInspection = grdRawInspection;
                    _grdResource = grdRawConsumable;
                    _grdItemClass = grdRawInspItemClass;
                    _grdRel = grdRawRel;
                    break;

                case 1://수입검사(원자재가공품)
                    _grdInspection = grdSubassemblyInspection;
                    _grdResource = grdSubassemblyConsumable;
                    _grdItemClass = grdSubassemblyInspItemClass;
                    _grdRel = grdSubassemblyRel;
                    _grdPoint = grdSubassemblyPoint;
                    break;

                case 2://수입검사(부자재/포장재)
                    _grdInspection = grdSubsidiaryInspection;
                    _grdResource = grdSubsidiaryConsumable;
                    _grdItemClass = grdSubsidiaryInspItemClass;
                    _grdRel = grdSubsidiaryRel;
                    _grdPoint = grdSubsidiaryPoint;
                    break;

                case 3://수입검사(공정)
                    _grdInspection = grdOSPInspection;
                    _grdResource = grdOSPProduct;
                    _grdItemClass = grdOSPInspItemClass;
                    _grdRel = grdOSPRel;
                    _grdPoint = grdOSPPoint;
                    break;

                case 4://자주검사(입고)
                    _grdInspection = grdSelfTakeInspection;
                    _grdItemClass = grdSelfTakeInspItemClass;
                    _grdRel = grdSelfTakeRel;
                    _grdPoint = grdSelfTakePoint;
                    break;

                case 5://자주검사(출고)
                    _grdInspection = grdSelfShipInspection;
                    _grdItemClass = grdSelfShipInspItemClass;
                    _grdRel = grdSelfShipRel;
                    _grdPoint = grdSelfShipPoint;
                    break;

                case 6://신뢰성검증 
                    _grdInspection = grdReliabilityInspection;
                    _grdItemClass = grdReliabilityInspItemClass;
                    _grdRel = grdReliabilityRel;
                    _grdPoint = grdReliabilityPoint;
                    break;

                case 7://최종검사 
                    _grdInspection = grdFinishInspection;
                    _grdResource = grdFinishProduct;
                    _grdItemClass = grdFinishInspItemClass;
                    _grdRel = grdFinishRel;
                    _grdPoint = grdFinishPoint;
                    break;

                case 8://출하검사 
                    _grdInspection = grdShipmentInspection;
                    _grdResource = grdShipmentProduct;
                    _grdItemClass = grdShipmentInspItemClass;
                    _grdRel = grdShipmentRel;
                    _grdPoint = grdShipmentPoint;
                    break;

                case 9://계측검사
                    _grdInspection = grdOperationInspection;
                    _grdResource = grdOperationProduct;
                    _grdItemClass = grdOperationInspItemClass;
                    _grdRel = grdOperationRel;
                    _grdPoint = grdOperationPoint;
                    break;

                case 10://계측기 R&R 검사
                    _grdInspection = grdMeasurementInspection;
                    _grdResource = grdMeasurementResource;
                    _grdItemClass = grdMeasurementInspItemClass;
                    _grdRel = grdMeasurementRel;
                    break;
            }
        }

        /// <summary>
        /// Resource 그리드를 조회 하기 위한 파라미터 세팅 함수
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, object> SetSearchResourceParameter()
        {
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            values.Add("P_INSPECTIONDEFID", _rowInspection["INSPECTIONDEFID"]);
            values.Add("P_INSPECTIONDEFVERSION", _rowInspection["INSPECTIONDEFVERSION"]);
            values.Add("P_RESOURCETYPE", _rowInspection["RESOURCETYPE"]);

            return values;
        }

        /// <summary>
        /// inspItemClass 그리드를 조회 하기 위한 파라미터 세팅 함수
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, object> SetSearchParameter()
        {
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            values.Add("P_INSPECTIONDEFID", _rowInspection["INSPECTIONDEFID"]);
            values.Add("P_INSPECTIONDEFVERSION", _rowInspection["INSPECTIONDEFVERSION"]);
            if (_tabIndex == 0 || _tabIndex == 1 || _tabIndex == 2 || _tabIndex == 3 || _tabIndex == 7 || _tabIndex == 8 || _tabIndex == 9)
            {//검사 품목이 있을 때
                values.Add("P_RESOURCEID", _rowResource["RESOURCEID"]);
                values.Add("P_RESOURCEVERSION", _rowResource["RESOURCEVERSION"]);
            }
            else
            {//검사 품목이 없을 때
                //변경**
                //values.Add("P_RESOURCEID", _rowInspection["INSPECTIONDEFID"]);
                //values.Add("P_RESOURCEVERSION", _rowInspection["INSPECTIONDEFVERSION"]);
                values.Add("P_RESOURCEID", _rowInspection["INSPECTIONDEFSEGMENT"]);
                values.Add("P_RESOURCEVERSION", "0");
            }        
            values.Add("P_RESOURCETYPE", _rowInspection["RESOURCETYPE"]);

            return values;
        }

        /// <summary>
        /// point 그리드를 조회 하기 위한 파라미터 세팅 함수
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, object> SetSearchPointParameter()
        {
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            values.Add("P_INSPECTIONDEFID", _rowRel["INSPECTIONDEFID"]);
            values.Add("P_INSPECTIONDEFVERSION", _rowRel["INSPECTIONDEFVERSION"]);         
            values.Add("P_RESOURCEID", _rowRel["RESOURCEID"]);
            values.Add("P_RESOURCEVERSION", _rowRel["RESOURCEVERSION"]);
            values.Add("P_RESOURCETYPE", _rowRel["RESOURCETYPE"]);
            values.Add("P_INSPITEMCLASSID", _rowRel["INSPITEMCLASSID"]);
            values.Add("P_INSPITEMID", _rowRel["INSPITEMID"]);
            values.Add("P_INSPITEMVERSION", _rowRel["INSPITEMVERSION"]);

            return values;
        }

        #endregion
    }
}
