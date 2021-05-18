#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using Micube.SmartMES.Commons.Controls;
using Micube.SmartMES.OutsideOrderMgnt.Popup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.OutsideOrderMgnt
{
    /// <summary>
    /// 프 로 그 램 명  :  외주관리> 외주 단가 관리> 외주단가관리
    /// 업  무  설  명  : 외주단가분류관리
    /// 생    성    자  : CHOISSTAR
    /// 생    성    일  : 2020-02-11
    /// 수  정  이  력  :
    ///
    ///
    ///
    /// </summary>
    public partial class OutsourcingYoungPongPriceCode : SmartConditionManualBaseForm
    {
        #region 생성자

        public OutsourcingYoungPongPriceCode()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
        }

        /// <summary>
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            InitializeGrid_Master();
            InitializeGrid_Detail();

            InitializeGrid_Tab1Detail();
            InitializeGrid_Tab2Detail();
            InitializeGrid_Tab1Master();
            InitializeGrid_Tab2Master();
        }

        /// <summary>
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid_Master()
        {
            grdMaster.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;

            grdMaster.View.AddTextBoxColumn("PLANTID", 200)
                .SetIsHidden();                                                          //  회사 ID
            grdMaster.View.AddTextBoxColumn("ENTERPRISEID", 200)
                .SetIsHidden();
            grdMaster.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 80)
                .SetIsReadOnly()

                ;//
            grdMaster.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200)
                .SetIsReadOnly();//

            grdMaster.View.AddComboBoxColumn("ISEXCEPT", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("N")
                .SetLabel("ISRESULT");

            grdMaster.View.SetKeyColumn("PLANTID", "PROCESSSEGMENTCLASSID");

            grdMaster.View.PopulateColumns();
        }

        /// <summary>
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid_Detail()
        {
            grdDetail.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;

            grdDetail.View.AddTextBoxColumn("ENTERPRISEID", 200).SetIsHidden();
            grdDetail.View.AddTextBoxColumn("PLANTID", 200).SetIsHidden();
            grdDetail.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 200).SetIsHidden();
            grdDetail.View.AddTextBoxColumn("OSPPRICECODE", 80).SetIsHidden();

            grdDetail.View.AddTextBoxColumn("PRICENAME", 150).SetLabel("OSPPRICENAME");
            
            //단가분류 팝업
            InitializeGrid_Priceclassid();

            grdDetail.View.AddTextBoxColumn("PRICECLASSNAME", 150).SetIsReadOnly();
            grdDetail.View.AddSpinEditColumn("PRIORITY", 100).SetTextAlignment(TextAlignment.Right).SetValidationIsRequired();
            grdDetail.View.AddSpinEditColumn("GROUPNO", 100).SetTextAlignment(TextAlignment.Right).SetValidationIsRequired();
            grdDetail.View.AddComboBoxColumn("CALCULATEUNIT", 100, new SqlQuery("GetUomDefinitionMapListByOsp", "10001", "UOMCATEGORY=OSP", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "UOMDEFID", "UOMDEFID")
                .SetValidationIsRequired();

            grdDetail.View.AddTextBoxColumn("PRICEITEMID01", 150)
                .SetIsHidden();
            grdDetail.View.AddTextBoxColumn("PRICEITEMNAME01", 150)
                .SetIsReadOnly()
                .SetLabel("PRICEITEMNAME");
            grdDetail.View.AddTextBoxColumn("ISRANGE01", 80)
                .SetIsReadOnly()
                .SetLabel("ISRANGE");

            grdDetail.View.AddTextBoxColumn("RANGEUNIT01", 80)
                .SetIsReadOnly()

                .SetLabel("RANGEUNIT");
            grdDetail.View.AddTextBoxColumn("PRICEITEMID02", 150)
                .SetIsHidden();
            grdDetail.View.AddTextBoxColumn("PRICEITEMNAME02", 150)
               .SetIsReadOnly()
               .SetLabel("PRICEITEMNAME");
            grdDetail.View.AddTextBoxColumn("ISRANGE02", 80)
                .SetIsReadOnly()
                .SetLabel("ISRANGE");
            grdDetail.View.AddTextBoxColumn("RANGEUNIT02", 80)
                .SetIsReadOnly()
                .SetLabel("RANGEUNIT");
            grdDetail.View.AddTextBoxColumn("PRICEITEMID03", 150)
                .SetIsHidden();
            grdDetail.View.AddTextBoxColumn("PRICEITEMNAME03", 150)
               .SetIsReadOnly()
               .SetLabel("PRICEITEMNAME");
            grdDetail.View.AddTextBoxColumn("ISRANGE03", 80)
                .SetIsReadOnly()
                .SetLabel("ISRANGE");
            grdDetail.View.AddTextBoxColumn("RANGEUNIT03", 80)
                .SetIsReadOnly()
                .SetLabel("RANGEUNIT");
            grdDetail.View.AddTextBoxColumn("PRICEITEMID04", 150)
                .SetIsHidden();
            grdDetail.View.AddTextBoxColumn("PRICEITEMNAME04", 150)
               .SetIsReadOnly()
               .SetLabel("PRICEITEMNAME");
            grdDetail.View.AddTextBoxColumn("ISRANGE04", 80)
                .SetIsReadOnly()
                .SetLabel("ISRANGE");
            grdDetail.View.AddTextBoxColumn("RANGEUNIT04", 80)
                .SetIsReadOnly()
                .SetLabel("RANGEUNIT");
            grdDetail.View.AddTextBoxColumn("PRICEITEMID05", 150)
                .SetIsHidden();
            grdDetail.View.AddTextBoxColumn("PRICEITEMNAME05", 150)
               .SetIsReadOnly()
               .SetLabel("PRICEITEMNAME");
            grdDetail.View.AddTextBoxColumn("ISRANGE05", 80)
                .SetIsReadOnly()
                .SetLabel("ISRANGE");
            grdDetail.View.AddTextBoxColumn("RANGEUNIT05", 80)
                .SetIsReadOnly()
                .SetLabel("RANGEUNIT");

            grdDetail.View.AddComboBoxColumn("VALIDSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetDefault("Valid")
               .SetValidationIsRequired()
               .SetIsReadOnly();
            grdDetail.View.AddTextBoxColumn("COMPONENTTYPE01", 80).SetIsHidden();
            grdDetail.View.AddTextBoxColumn("DATASETTYPE01", 80).SetIsHidden();
            grdDetail.View.AddTextBoxColumn("DATASET01", 80).SetIsHidden();
            grdDetail.View.AddTextBoxColumn("FORMATMASK01", 80).SetIsHidden();
            grdDetail.View.AddTextBoxColumn("COMPONENTTYPE02", 80).SetIsHidden();
            grdDetail.View.AddTextBoxColumn("DATASETTYPE02", 80).SetIsHidden();
            grdDetail.View.AddTextBoxColumn("DATASET02", 80).SetIsHidden();
            grdDetail.View.AddTextBoxColumn("FORMATMASK02", 80).SetIsHidden();
            grdDetail.View.AddTextBoxColumn("COMPONENTTYPE03", 80).SetIsHidden();
            grdDetail.View.AddTextBoxColumn("DATASETTYPE03", 80).SetIsHidden();
            grdDetail.View.AddTextBoxColumn("DATASET03", 80).SetIsHidden();
            grdDetail.View.AddTextBoxColumn("FORMATMASK03", 80).SetIsHidden();
            grdDetail.View.AddTextBoxColumn("COMPONENTTYPE04", 80).SetIsHidden();
            grdDetail.View.AddTextBoxColumn("DATASETTYPE04", 80).SetIsHidden();
            grdDetail.View.AddTextBoxColumn("DATASET04", 80).SetIsHidden();
            grdDetail.View.AddTextBoxColumn("FORMATMASK04", 80).SetIsHidden();
            grdDetail.View.AddTextBoxColumn("COMPONENTTYPE05", 80).SetIsHidden();
            grdDetail.View.AddTextBoxColumn("DATASETTYPE05", 80).SetIsHidden();
            grdDetail.View.AddTextBoxColumn("DATASET05", 80).SetIsHidden();
            grdDetail.View.AddTextBoxColumn("FORMATMASK05", 80).SetIsHidden();

            grdDetail.View.SetKeyColumn("PLANTID", "PRICECLASSID");
            grdDetail.View.PopulateColumns();
        }

        /// <summary>
        /// grid Priceclassid 가져오기
        /// </summary>
        private void InitializeGrid_Priceclassid()
        {
            var popupGridConsumabledefid = this.grdDetail.View.AddSelectPopupColumn("PRICECLASSID", 120, new SqlQuery("GetPriceclassidByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("PRICECLASSPOPUP", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                .SetNotUseMultiColumnPaste()
                 // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                 .SetRelationIds("PLANTID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(800, 600)
                // 그리드의 남은 영역을 채울 컬럼 설정
                .SetValidationIsRequired()
                //.SetPopupAutoFillColumns("CONSUMABLEDEFNAME")

                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    int irow = 0;
                    int crow = 0;
                    DataRow classRow = grdDetail.View.GetFocusedDataRow();
                    crow = grdDetail.View.FocusedRowHandle;

                    foreach (DataRow row in selectedRows)
                    {
                        if (irow == 0)
                        {
                            string sPriceitemid = row["PRICECLASSID"].ToString();

                            int icheck = SearchidKey(sPriceitemid, "PRICECLASSID", crow);
                            if (icheck == -1)
                            {
                                classRow["PRICECLASSID"] = row["PRICECLASSID"];
                                classRow["PRICECLASSNAME"] = row["PRICECLASSNAME"];
                                classRow["PRICEITEMID01"] = row["PRICEITEMID01"];
                                classRow["PRICEITEMID02"] = row["PRICEITEMID02"];
                                classRow["PRICEITEMID03"] = row["PRICEITEMID03"];
                                classRow["PRICEITEMID04"] = row["PRICEITEMID04"];
                                classRow["PRICEITEMID05"] = row["PRICEITEMID05"];
                                classRow["PRICEITEMNAME01"] = row["PRICEITEMNAME01"];
                                classRow["PRICEITEMNAME02"] = row["PRICEITEMNAME02"];
                                classRow["PRICEITEMNAME03"] = row["PRICEITEMNAME03"];
                                classRow["PRICEITEMNAME04"] = row["PRICEITEMNAME04"];
                                classRow["PRICEITEMNAME05"] = row["PRICEITEMNAME05"];
                                classRow["ISRANGE01"] = row["ISRANGE01"];
                                classRow["ISRANGE02"] = row["ISRANGE02"];
                                classRow["ISRANGE03"] = row["ISRANGE03"];
                                classRow["ISRANGE04"] = row["ISRANGE04"];
                                classRow["ISRANGE05"] = row["ISRANGE05"];
                                classRow["RANGEUNIT01"] = row["RANGEUNIT01"];
                                classRow["RANGEUNIT02"] = row["RANGEUNIT02"];
                                classRow["RANGEUNIT03"] = row["RANGEUNIT03"];
                                classRow["RANGEUNIT04"] = row["RANGEUNIT04"];
                                classRow["RANGEUNIT05"] = row["RANGEUNIT05"];
                                grdDetail.View.RaiseValidateRow(crow);
                            }
                            else
                            {
                                classRow["PRICECLASSID"] = "";
                                classRow["PRICECLASSNAME"] = "";
                                classRow["PRICEITEMID01"] = "";
                                classRow["PRICEITEMID02"] = "";
                                classRow["PRICEITEMID03"] = "";
                                classRow["PRICEITEMID04"] = "";
                                classRow["PRICEITEMID05"] = "";
                                classRow["PRICEITEMNAME01"] = "";
                                classRow["PRICEITEMNAME02"] = "";
                                classRow["PRICEITEMNAME03"] = "";
                                classRow["PRICEITEMNAME04"] = "";
                                classRow["PRICEITEMNAME05"] = "";
                                classRow["ISRANGE01"] = "";
                                classRow["ISRANGE02"] = "";
                                classRow["ISRANGE03"] = "";
                                classRow["ISRANGE04"] = "";
                                classRow["ISRANGE05"] = "";
                                classRow["RANGEUNIT01"] = "";
                                classRow["RANGEUNIT02"] = "";
                                classRow["RANGEUNIT03"] = "";
                                classRow["RANGEUNIT04"] = "";
                                classRow["RANGEUNIT05"] = "";
                                irow = irow - 1;
                            }
                        }

                        irow = irow + 1;
                    }
                })

            ;

            // 팝업 조회조건

            popupGridConsumabledefid.Conditions.AddTextBox("P_PRICECLASSNAME")
               .SetLabel("PRICECLASSNAME");
            popupGridConsumabledefid.Conditions.AddTextBox("P_PLANTID")
              .SetPopupDefaultByGridColumnId("PLANTID")
              .SetLabel("")
              .SetIsHidden();
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("PRICECLASSID", 80).SetIsReadOnly();
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("PRICECLASSNAME", 150).SetIsReadOnly();
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("PRICEITEMID01", 80).SetIsHidden().SetLabel("");
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("PRICEITEMID02", 80).SetIsHidden();
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("PRICEITEMID03", 80).SetIsHidden();
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("PRICEITEMID04", 80).SetIsHidden();
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("PRICEITEMID05", 80).SetIsHidden();
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("PRICEITEMNAME01", 100).SetIsReadOnly().SetLabel("PRICEITEMNAME");
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("ISRANGE01", 80).SetIsReadOnly().SetLabel("ISRANGE");
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("RANGEUNIT01", 80).SetIsReadOnly().SetLabel("RANGEUNIT");
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("PRICEITEMNAME02", 100).SetIsReadOnly().SetLabel("PRICEITEMNAME");
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("ISRANGE02", 80).SetIsReadOnly().SetLabel("ISRANGE");
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("RANGEUNIT02", 80).SetIsReadOnly().SetLabel("RANGEUNIT");
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("PRICEITEMNAME03", 100).SetIsReadOnly().SetLabel("PRICEITEMNAME");
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("ISRANGE03", 80).SetIsReadOnly().SetLabel("ISRANGE");
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("RANGEUNIT03", 80).SetIsReadOnly().SetLabel("RANGEUNIT");
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("PRICEITEMNAME04", 100).SetIsReadOnly().SetLabel("PRICEITEMNAME");
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("ISRANGE04", 80).SetIsReadOnly().SetLabel("ISRANGE");
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("RANGEUNIT04", 80).SetIsReadOnly().SetLabel("RANGEUNIT");
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("PRICEITEMNAME05", 100).SetIsReadOnly().SetLabel("PRICEITEMNAME");
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("ISRANGE05", 80).SetIsReadOnly().SetLabel("ISRANGE");
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("RANGEUNIT05", 80).SetIsReadOnly().SetLabel("RANGEUNIT");
        }

        /// <summary>
        /// 단가 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid_Tab1Master()
        {
            grdTab1Master.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdTab1Master.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdTab1Master.View.SetIsReadOnly();
            grdTab1Master.View.AddTextBoxColumn("ENTERPRISEID", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("PLANTID", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("PRICECOMBINATIONID", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("PRICECOMBINATIONNAME", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("OSPPRICECODE", 100).SetIsHidden();
            grdTab1Master.View.AddComboBoxColumn("OSPPRODUCTIONTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OSPProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("PRODUCTIONTYPE")
                .SetEmptyItem("*", "*")
                .SetIsHidden();  //
            grdTab1Master.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsHidden();

            grdTab1Master.View.AddTextBoxColumn("PRICEITEMID01", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("PRICEITEMID02", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("PRICEITEMID03", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("PRICEITEMID04", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("PRICEITEMID05", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("ISRANGE01", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("ISRANGE02", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("ISRANGE03", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("ISRANGE04", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("ISRANGE05", 200).SetIsHidden();
            grdTab1Master.View.AddSpinEditColumn("ITEMVALUE01FR", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric)
                .SetIsHidden(); //
            grdTab1Master.View.AddSpinEditColumn("ITEMVALUE01TO", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric)
                .SetIsHidden(); //
            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE01", 100).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE01NAME", 100).SetIsHidden();

            grdTab1Master.View.AddSpinEditColumn("ITEMVALUE02FR", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric)
                .SetIsHidden(); //
            grdTab1Master.View.AddSpinEditColumn("ITEMVALUE02TO", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric)
                .SetIsHidden(); //
            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE02", 100).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE02NAME", 100).SetIsHidden();

            grdTab1Master.View.AddSpinEditColumn("ITEMVALUE03FR", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric)
                .SetIsHidden(); //
            grdTab1Master.View.AddSpinEditColumn("ITEMVALUE03TO", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric)
                .SetIsHidden(); //
            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE03", 100).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE03NAME", 100).SetIsHidden();

            grdTab1Master.View.AddSpinEditColumn("ITEMVALUE04FR", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric)
                .SetIsHidden(); //
            grdTab1Master.View.AddSpinEditColumn("ITEMVALUE04TO", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric)
                .SetIsHidden(); //
            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE04", 100).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE04NAME", 100).SetIsHidden();

            grdTab1Master.View.AddSpinEditColumn("ITEMVALUE05FR", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric)
                .SetIsHidden(); //
            grdTab1Master.View.AddSpinEditColumn("ITEMVALUE05TO", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric)
                .SetIsHidden(); //
            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE05", 100).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE05NAME", 100).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("OSPVENDORID", 100).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("OSPVENDORNAME", 200).SetIsHidden();
            grdTab1Master.View.AddComboBoxColumn("PRICEUNIT", 100, new SqlQuery("GetUomDefinitionMapListByOsp", "10001", "UOMCATEGORY=OSPPRICEUNIT", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "UOMDEFID", "UOMDEFID")
                .SetValidationIsRequired()
                .SetDefault("")
               .SetIsHidden();

            grdTab1Master.View.AddComboBoxColumn("VALIDSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetValidationIsRequired()
                .SetDefault("Valid")
                .SetIsReadOnly()
                .SetIsHidden();
            grdTab1Master.View.PopulateColumns();
        }

        /// <summary>
        /// 단가 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid_Tab2Master()
        {
            grdTab2Master.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdTab2Master.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdTab2Master.View.SetIsReadOnly();
            grdTab2Master.View.AddTextBoxColumn("ENTERPRISEID", 200).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("PLANTID", 200).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("PRICECOMBINATIONID", 200).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("PRICECOMBINATIONNAME", 200).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("OSPPRICECODE", 100).SetIsHidden();
            grdTab2Master.View.AddComboBoxColumn("OSPPRODUCTIONTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OSPProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("PRODUCTIONTYPE")
                .SetEmptyItem("*", "*")
                .SetIsHidden();  //

            grdTab2Master.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsHidden();

            grdTab2Master.View.AddTextBoxColumn("PRICEITEMID01", 200).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("PRICEITEMID02", 200).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("PRICEITEMID03", 200).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("PRICEITEMID04", 200).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("PRICEITEMID05", 200).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("ISRANGE01", 200).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("ISRANGE02", 200).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("ISRANGE03", 200).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("ISRANGE04", 200).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("ISRANGE05", 200).SetIsHidden();
            grdTab2Master.View.AddSpinEditColumn("ITEMVALUE01FR", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric)
                .SetIsHidden(); //
            grdTab2Master.View.AddSpinEditColumn("ITEMVALUE01TO", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric)
                .SetIsHidden(); //
            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE01", 100).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE01NAME", 100).SetIsHidden();

            grdTab2Master.View.AddSpinEditColumn("ITEMVALUE02FR", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric)
                .SetIsHidden(); //
            grdTab2Master.View.AddSpinEditColumn("ITEMVALUE02TO", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric)
                .SetIsHidden(); //
            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE02", 100).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE02NAME", 100).SetIsHidden();

            grdTab2Master.View.AddSpinEditColumn("ITEMVALUE03FR", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric)
                .SetIsHidden(); //
            grdTab2Master.View.AddSpinEditColumn("ITEMVALUE03TO", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric)
                .SetIsHidden(); //
            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE03", 100).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE03NAME", 100).SetIsHidden();

            grdTab2Master.View.AddSpinEditColumn("ITEMVALUE04FR", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric)
                .SetIsHidden(); //
            grdTab2Master.View.AddSpinEditColumn("ITEMVALUE04TO", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric)
                .SetIsHidden(); //
            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE04", 100).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE04NAME", 100).SetIsHidden();

            grdTab2Master.View.AddSpinEditColumn("ITEMVALUE05FR", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric)
                .SetIsHidden(); //
            grdTab2Master.View.AddSpinEditColumn("ITEMVALUE05TO", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric)
                .SetIsHidden(); //
            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE05", 100).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE05NAME", 100).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("OSPVENDORID", 100).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("OSPVENDORNAME", 200).SetIsHidden();
            grdTab2Master.View.AddComboBoxColumn("PRICEUNIT", 100, new SqlQuery("GetUomDefinitionMapListByOsp", "10001", "UOMCATEGORY=OSPPRICEUNIT", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "UOMDEFID", "UOMDEFID")
                .SetValidationIsRequired()
                .SetDefault("")
               .SetIsHidden();

            grdTab2Master.View.AddComboBoxColumn("VALIDSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetValidationIsRequired()
                .SetDefault("Valid")
                .SetIsReadOnly()
                .SetIsHidden();
            grdTab2Master.View.PopulateColumns();
        }

        /// <summary>
        /// 단가 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid_Tab1Detail()
        {
            grdTab1Detail.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            //grdTab1Detail.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdTab1Detail.View.SetSortOrder("STARTDATE", DevExpress.Data.ColumnSortOrder.Descending);
            grdTab1Detail.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();
            grdTab1Detail.View.AddTextBoxColumn("PRICECOMBINATIONID", 100)
                .SetIsHidden();
            grdTab1Detail.View.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden();
            grdTab1Detail.View.AddDateEditColumn("STARTDATE", 100)
                .SetValidationIsRequired()
                .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime);                              //
            grdTab1Detail.View.AddDateEditColumn("ENDDATE", 100)
                .SetValidationIsRequired()
                .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime);                              //
            grdTab1Detail.View.AddSpinEditColumn("OSPPRICE", 100)
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);                                     //
            grdTab1Detail.View.AddTextBoxColumn("DESCRIPTION", 200);
            grdTab1Detail.View.AddComboBoxColumn("VALIDSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetDefault("Valid")
               .SetValidationIsRequired()
               .SetIsReadOnly();
            grdTab1Detail.View.SetKeyColumn("PLANTID", "ENTERPRISEID", "PRICECOMBINATIONID", "STARTDATE");
            grdTab1Detail.View.PopulateColumns();
        }

        /// <summary>
        /// 단가 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid_Tab2Detail()
        {
            grdTab2Detail.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            //grdTab2Detail.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdTab2Detail.View.SetSortOrder("STARTDATE", DevExpress.Data.ColumnSortOrder.Descending);
            grdTab2Detail.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();
            grdTab2Detail.View.AddTextBoxColumn("PRICECOMBINATIONID", 100)
                .SetIsHidden();
            grdTab2Detail.View.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden();
            grdTab2Detail.View.AddDateEditColumn("STARTDATE", 100)
                .SetValidationIsRequired()
                .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime);                              //
            grdTab2Detail.View.AddDateEditColumn("ENDDATE", 100)
                .SetValidationIsRequired()
                .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime);                              //
            grdTab2Detail.View.AddSpinEditColumn("OSPPRICE", 100)
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);                                     //
            grdTab2Detail.View.AddTextBoxColumn("DESCRIPTION", 200);
            grdTab2Detail.View.AddComboBoxColumn("VALIDSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetIsReadOnly();
            grdTab2Detail.View.SetKeyColumn("PLANTID", "ENTERPRISEID", "PRICECOMBINATIONID", "STARTDATE");
            grdTab2Detail.View.PopulateColumns();
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            grdMaster.View.FocusedRowChanged += GrdMaster_FocusedRowChanged;
            grdDetail.View.FocusedRowChanged += GrdDetail_FocusedRowChanged;

            grdTab1Master.View.FocusedRowChanged += GrdTab1Master_FocusedRowChanged;
            grdTab2Master.View.FocusedRowChanged += GrdTab2Master_FocusedRowChanged;

            grdDetail.View.AddingNewRow += GrdDetail_AddingNewRow;
            grdDetail.View.ShowingEditor += GrdDetail_ShowingEditor;

            grdTab1Master.View.ShowingEditor += GrdTab1Master_ShowingEditor;
            //   grdTab1Master.View.AddingNewRow += GrdTab1Master_AddingNewRow;

            grdTab2Master.View.ShowingEditor += GrdTab2Master_ShowingEditor;

            grdTab1Detail.View.AddingNewRow += GrdTab1Detail_AddingNewRow;
            grdTab2Detail.View.AddingNewRow += GrdTab2Detail_AddingNewRow;
            grdTab1Detail.View.CellValueChanged += GrdTab1Detail_CellValueChanged;
            grdTab2Detail.View.CellValueChanged += GrdTab2Detail_CellValueChanged;

            grdDetail.ToolbarDeleteRow += GrdDetail_ToolbarDeleteRow;
            grdTab1Master.ToolbarAddingRow += GrdTab1Master_ToolbarAddingRow;
            grdTab2Master.ToolbarAddingRow += GrdTab2Master_ToolbarAddingRow;
            grdTab1Master.ToolbarDeleteRow += GrdTab1Master_ToolbarDeleteRow;
            grdTab2Master.ToolbarDeleteRow += GrdTab2Master_ToolbarDeleteRow;
            grdTab1Detail.ToolbarDeleteRow += GrdTab1Detail_ToolbarDeleteRow;
            grdTab2Detail.ToolbarDeleteRow += GrdTab2Detail_ToolbarDeleteRow;
        }

        private void GrdTab1Detail_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "STARTDATE")
            {
                grdTab1Detail.View.CellValueChanged -= GrdTab1Detail_CellValueChanged;

                DataRow row = grdTab1Detail.View.GetFocusedDataRow();

                if (row["STARTDATE"].ToString().Equals(""))
                {
                    grdTab1Detail.View.CellValueChanged += GrdTab1Detail_CellValueChanged;
                    return;
                }

                DateTime dateBudget = Convert.ToDateTime(row["STARTDATE"].ToString());
                grdTab1Detail.View.SetFocusedRowCellValue("STARTDATE", dateBudget.ToString("yyyy-MM-dd"));// 예산일자
                grdTab1Detail.View.CellValueChanged += GrdTab1Detail_CellValueChanged;
            }

            if (e.Column.FieldName == "ENDDATE")
            {
                grdTab1Detail.View.CellValueChanged -= GrdTab1Detail_CellValueChanged;

                DataRow row = grdTab1Detail.View.GetFocusedDataRow();

                if (row["ENDDATE"].ToString().Equals(""))
                {
                    grdTab1Detail.View.CellValueChanged += GrdTab1Detail_CellValueChanged;
                    return;
                }
                DateTime dateBudget = Convert.ToDateTime(row["ENDDATE"].ToString());
                grdTab1Detail.View.SetFocusedRowCellValue("ENDDATE", dateBudget.ToString("yyyy-MM-dd"));// 예산일자
                grdTab1Detail.View.CellValueChanged += GrdTab1Detail_CellValueChanged;
            }
        }

        private void GrdTab2Detail_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "STARTDATE")
            {
                grdTab2Detail.View.CellValueChanged -= GrdTab2Detail_CellValueChanged;

                DataRow row = grdTab2Detail.View.GetFocusedDataRow();

                if (row["STARTDATE"].ToString().Equals(""))
                {
                    grdTab2Detail.View.CellValueChanged += GrdTab2Detail_CellValueChanged;
                    return;
                }

                DateTime dateBudget = Convert.ToDateTime(row["STARTDATE"].ToString());
                grdTab2Detail.View.SetFocusedRowCellValue("STARTDATE", dateBudget.ToString("yyyy-MM-dd"));// 예산일자
                grdTab2Detail.View.CellValueChanged += GrdTab2Detail_CellValueChanged;
            }

            if (e.Column.FieldName == "ENDDATE")
            {
                grdTab2Detail.View.CellValueChanged -= GrdTab2Detail_CellValueChanged;

                DataRow row = grdTab2Detail.View.GetFocusedDataRow();

                if (row["ENDDATE"].ToString().Equals(""))
                {
                    grdTab2Detail.View.CellValueChanged += GrdTab2Detail_CellValueChanged;
                    return;
                }
                DateTime dateBudget = Convert.ToDateTime(row["ENDDATE"].ToString());
                grdTab2Detail.View.SetFocusedRowCellValue("ENDDATE", dateBudget.ToString("yyyy-MM-dd"));// 예산일자
                grdTab2Detail.View.CellValueChanged += GrdTab2Detail_CellValueChanged;
            }
        }

        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void GrdTab1Master_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (grdMaster.View.DataRowCount == 0)
            {
                int intfocusRow = grdTab1Master.View.FocusedRowHandle;
                grdTab1Master.View.DeleteRow(intfocusRow);
                return;
            }
            DataTable changed = grdMaster.GetChangedRows();
            if (changed.Rows.Count != 0)
            {
                int intfocusRow = grdTab1Master.View.FocusedRowHandle;
                grdTab1Master.View.DeleteRow(intfocusRow);
                return;
            }
            if (grdDetail.View.DataRowCount == 0)
            {
                int intfocusRow = grdTab1Master.View.FocusedRowHandle;
                grdTab1Master.View.DeleteRow(intfocusRow);
                return;
            }
            DataTable changedetail = grdDetail.GetChangedRows();
            if (changedetail.Rows.Count != 0)
            {
                int intfocusRow = grdTab1Master.View.FocusedRowHandle;
                grdTab1Master.View.DeleteRow(intfocusRow);
                return;
            }
            grdTab1Master.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdTab1Master.View.SetFocusedRowCellValue("PLANTID", grdDetail.View.GetFocusedRowCellValue("PLANTID"));// plantid

            grdTab1Master.View.SetFocusedRowCellValue("OSPPRICECODE", grdDetail.View.GetFocusedRowCellValue("OSPPRICECODE"));// plantid

            grdTab1Master.View.SetFocusedRowCellValue("OSPPRODUCTIONTYPE", "*");
            grdTab1Master.View.SetFocusedRowCellValue("PRODUCTDEFID", "*");
            grdTab1Master.View.SetFocusedRowCellValue("PRODUCTDEFVERSION", "*");
            grdTab1Master.View.SetFocusedRowCellValue("OSPVENDORID", "*");
            grdTab1Master.View.SetFocusedRowCellValue("OSPVENDORNAME", "*");
            grdTab1Master.View.SetFocusedRowCellValue("PRICEITEMID01", grdDetail.View.GetFocusedRowCellValue("PRICEITEMID01"));
            grdTab1Master.View.SetFocusedRowCellValue("PRICEITEMID02", grdDetail.View.GetFocusedRowCellValue("PRICEITEMID02"));
            grdTab1Master.View.SetFocusedRowCellValue("PRICEITEMID03", grdDetail.View.GetFocusedRowCellValue("PRICEITEMID03"));
            grdTab1Master.View.SetFocusedRowCellValue("PRICEITEMID04", grdDetail.View.GetFocusedRowCellValue("PRICEITEMID04"));
            grdTab1Master.View.SetFocusedRowCellValue("PRICEITEMID05", grdDetail.View.GetFocusedRowCellValue("PRICEITEMID05"));
            grdTab1Master.View.SetFocusedRowCellValue("ISRANGE01", grdDetail.View.GetFocusedRowCellValue("ISRANGE01"));
            grdTab1Master.View.SetFocusedRowCellValue("ISRANGE02", grdDetail.View.GetFocusedRowCellValue("ISRANGE02"));
            grdTab1Master.View.SetFocusedRowCellValue("ISRANGE03", grdDetail.View.GetFocusedRowCellValue("ISRANGE03"));
            grdTab1Master.View.SetFocusedRowCellValue("ISRANGE04", grdDetail.View.GetFocusedRowCellValue("ISRANGE04"));
            grdTab1Master.View.SetFocusedRowCellValue("ISRANGE05", grdDetail.View.GetFocusedRowCellValue("ISRANGE05"));
            if (!(grdDetail.View.GetFocusedRowCellValue("PRICEITEMID01").Equals("")))
            {
                grdTab1Master.View.SetFocusedRowCellValue("ITEMVALUE01", "*");
                grdTab1Master.View.SetFocusedRowCellValue("ITEMVALUE01NAME", "*");
            }
            if (!(grdDetail.View.GetFocusedRowCellValue("PRICEITEMID02").Equals("")))
            {
                grdTab1Master.View.SetFocusedRowCellValue("ITEMVALUE02", "*");
                grdTab1Master.View.SetFocusedRowCellValue("ITEMVALUE02NAME", "*");
            }
            if (!(grdDetail.View.GetFocusedRowCellValue("PRICEITEMID03").Equals("")))
            {
                grdTab1Master.View.SetFocusedRowCellValue("ITEMVALUE03", "*");
                grdTab1Master.View.SetFocusedRowCellValue("ITEMVALUE03NAME", "*");
            }
            if (!(grdDetail.View.GetFocusedRowCellValue("PRICEITEMID04").Equals("")))
            {
                grdTab1Master.View.SetFocusedRowCellValue("ITEMVALUE04", "*");
                grdTab1Master.View.SetFocusedRowCellValue("ITEMVALUE04NAME", "*");
            }
            if (!(grdDetail.View.GetFocusedRowCellValue("PRICEITEMID05").Equals("")))
            {
                grdTab1Master.View.SetFocusedRowCellValue("ITEMVALUE05", "*");
                grdTab1Master.View.SetFocusedRowCellValue("ITEMVALUE05NAME", "*");
            }

            grdTab1Master.View.SetFocusedRowCellValue("VALIDSTATE", "Valid");//
        }

        private void GrdTab2Master_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (grdMaster.View.DataRowCount == 0)
            {
                int intfocusRow = grdTab2Master.View.FocusedRowHandle;
                grdTab2Master.View.DeleteRow(intfocusRow);
                return;
            }
            DataTable changed = grdMaster.GetChangedRows();
            if (changed.Rows.Count != 0)
            {
                int intfocusRow = grdTab2Master.View.FocusedRowHandle;
                grdTab2Master.View.DeleteRow(intfocusRow);
                return;
            }
            if (grdDetail.View.DataRowCount == 0)
            {
                int intfocusRow = grdTab2Master.View.FocusedRowHandle;
                grdTab2Master.View.DeleteRow(intfocusRow);
                return;
            }
            DataTable changedetail = grdDetail.GetChangedRows();
            if (changedetail.Rows.Count != 0)
            {
                int intfocusRow = grdTab2Master.View.FocusedRowHandle;
                grdTab2Master.View.DeleteRow(intfocusRow);
                return;
            }
            grdTab2Master.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdTab2Master.View.SetFocusedRowCellValue("PLANTID", grdDetail.View.GetFocusedRowCellValue("PLANTID"));// plantid

            grdTab2Master.View.SetFocusedRowCellValue("OSPPRICECODE", grdDetail.View.GetFocusedRowCellValue("OSPPRICECODE"));// plantid

            grdTab2Master.View.SetFocusedRowCellValue("OSPPRODUCTIONTYPE", "*");
            grdTab2Master.View.SetFocusedRowCellValue("PRODUCTDEFID", "*");
            grdTab2Master.View.SetFocusedRowCellValue("PRODUCTDEFVERSION", "*");
            grdTab2Master.View.SetFocusedRowCellValue("OSPVENDORID", "*");
            grdTab2Master.View.SetFocusedRowCellValue("OSPVENDORNAME", "*");
            grdTab2Master.View.SetFocusedRowCellValue("PRICEITEMID01", grdDetail.View.GetFocusedRowCellValue("PRICEITEMID01"));
            grdTab2Master.View.SetFocusedRowCellValue("PRICEITEMID02", grdDetail.View.GetFocusedRowCellValue("PRICEITEMID02"));
            grdTab2Master.View.SetFocusedRowCellValue("PRICEITEMID03", grdDetail.View.GetFocusedRowCellValue("PRICEITEMID03"));
            grdTab2Master.View.SetFocusedRowCellValue("PRICEITEMID04", grdDetail.View.GetFocusedRowCellValue("PRICEITEMID04"));
            grdTab2Master.View.SetFocusedRowCellValue("PRICEITEMID05", grdDetail.View.GetFocusedRowCellValue("PRICEITEMID05"));
            grdTab2Master.View.SetFocusedRowCellValue("ISRANGE01", grdDetail.View.GetFocusedRowCellValue("ISRANGE01"));
            grdTab2Master.View.SetFocusedRowCellValue("ISRANGE02", grdDetail.View.GetFocusedRowCellValue("ISRANGE02"));
            grdTab2Master.View.SetFocusedRowCellValue("ISRANGE03", grdDetail.View.GetFocusedRowCellValue("ISRANGE03"));
            grdTab2Master.View.SetFocusedRowCellValue("ISRANGE04", grdDetail.View.GetFocusedRowCellValue("ISRANGE04"));
            grdTab2Master.View.SetFocusedRowCellValue("ISRANGE05", grdDetail.View.GetFocusedRowCellValue("ISRANGE05"));
            if (!(grdDetail.View.GetFocusedRowCellValue("PRICEITEMID01").Equals("")))
            {
                grdTab2Master.View.SetFocusedRowCellValue("ITEMVALUE01", "*");
                grdTab2Master.View.SetFocusedRowCellValue("ITEMVALUE01NAME", "*");
            }
            if (!(grdDetail.View.GetFocusedRowCellValue("PRICEITEMID02").Equals("")))
            {
                grdTab2Master.View.SetFocusedRowCellValue("ITEMVALUE02", "*");
                grdTab2Master.View.SetFocusedRowCellValue("ITEMVALUE02NAME", "*");
            }
            if (!(grdDetail.View.GetFocusedRowCellValue("PRICEITEMID03").Equals("")))
            {
                grdTab2Master.View.SetFocusedRowCellValue("ITEMVALUE03", "*");
                grdTab2Master.View.SetFocusedRowCellValue("ITEMVALUE03NAME", "*");
            }
            if (!(grdDetail.View.GetFocusedRowCellValue("PRICEITEMID04").Equals("")))
            {
                grdTab2Master.View.SetFocusedRowCellValue("ITEMVALUE04", "*");
                grdTab2Master.View.SetFocusedRowCellValue("ITEMVALUE04NAME", "*");
            }
            if (!(grdDetail.View.GetFocusedRowCellValue("PRICEITEMID05").Equals("")))
            {
                grdTab2Master.View.SetFocusedRowCellValue("ITEMVALUE05", "*");
                grdTab2Master.View.SetFocusedRowCellValue("ITEMVALUE05NAME", "*");
            }

            grdTab2Master.View.SetFocusedRowCellValue("VALIDSTATE", "Valid");//
        }

        /// <summary>
        /// 단가등록 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void GrdTab1Detail_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            //추가 할때 단가기준 그리드의 상태값을 체크 처리

            if (grdMaster.View.DataRowCount == 0)
            {
                int intfocusRow = grdTab1Detail.View.FocusedRowHandle;
                grdTab1Detail.View.DeleteRow(intfocusRow);
                return;
            }
            DataTable changed = grdMaster.GetChangedRows();
            if (changed.Rows.Count != 0)
            {
                int intfocusRow = grdTab1Detail.View.FocusedRowHandle;
                grdTab1Detail.View.DeleteRow(intfocusRow);
                return;
            }
            if (grdDetail.View.DataRowCount == 0)
            {
                int intfocusRow = grdTab1Detail.View.FocusedRowHandle;
                grdTab1Detail.View.DeleteRow(intfocusRow);
                return;
            }
            DataTable changedetail = grdDetail.GetChangedRows();
            if (changedetail.Rows.Count != 0)
            {
                int intfocusRow = grdTab1Detail.View.FocusedRowHandle;
                grdTab1Detail.View.DeleteRow(intfocusRow);
                return;
            }

            if (grdTab1Master.View.DataRowCount == 0)
            {
                int intfocusRow = grdTab1Detail.View.FocusedRowHandle;
                grdTab1Detail.View.DeleteRow(intfocusRow);
                return;
            }
            DataTable changetab1Master = grdTab1Master.GetChangedRows();
            if (changedetail.Rows.Count != 0)
            {
                int intfocusRow = grdTab1Detail.View.FocusedRowHandle;
                grdTab1Detail.View.DeleteRow(intfocusRow);
                return;
            }

            grdTab1Detail.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdTab1Detail.View.SetFocusedRowCellValue("PLANTID", grdTab1Master.View.GetFocusedRowCellValue("PLANTID"));// plantid
            grdTab1Detail.View.SetFocusedRowCellValue("PRICECOMBINATIONID", grdTab1Master.View.GetFocusedRowCellValue("PRICECOMBINATIONID"));// plantid
            DateTime dateNow = DateTime.Now;
            grdTab1Detail.View.SetFocusedRowCellValue("STARTDATE", dateNow.ToString("yyyy-MM-dd"));// 시작일
            grdTab1Detail.View.SetFocusedRowCellValue("ENDDATE", "9999-12-31");//종료일
            //grdOspPrice.View.CellValueChanged += GrdOspPrice_CellValueChanged;
            grdTab1Detail.View.ClearSorting();
            grdTab1Detail.View.Columns["STARTDATE"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            grdTab1Detail.View.SetFocusedRowCellValue("VALIDSTATE", "Valid");//
        }

        /// <summary>
        /// 단가등록 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void GrdTab2Detail_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            //추가 할때 단가기준 그리드의 상태값을 체크 처리

            if (grdMaster.View.DataRowCount == 0)
            {
                int intfocusRow = grdTab2Detail.View.FocusedRowHandle;
                grdTab2Detail.View.DeleteRow(intfocusRow);
                return;
            }
            DataTable changed = grdMaster.GetChangedRows();
            if (changed.Rows.Count != 0)
            {
                int intfocusRow = grdTab2Detail.View.FocusedRowHandle;
                grdTab2Detail.View.DeleteRow(intfocusRow);
                return;
            }
            if (grdDetail.View.DataRowCount == 0)
            {
                int intfocusRow = grdTab2Detail.View.FocusedRowHandle;
                grdTab2Detail.View.DeleteRow(intfocusRow);
                return;
            }
            DataTable changedetail = grdDetail.GetChangedRows();
            if (changedetail.Rows.Count != 0)
            {
                int intfocusRow = grdTab2Detail.View.FocusedRowHandle;
                grdTab2Detail.View.DeleteRow(intfocusRow);
                return;
            }

            if (grdTab2Master.View.DataRowCount == 0)
            {
                int intfocusRow = grdTab2Detail.View.FocusedRowHandle;
                grdTab2Detail.View.DeleteRow(intfocusRow);
                return;
            }
            DataTable changetab1Master = grdTab2Master.GetChangedRows();
            if (changedetail.Rows.Count != 0)
            {
                int intfocusRow = grdTab2Detail.View.FocusedRowHandle;
                grdTab2Detail.View.DeleteRow(intfocusRow);
                return;
            }

            grdTab2Detail.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdTab2Detail.View.SetFocusedRowCellValue("PLANTID", grdTab2Master.View.GetFocusedRowCellValue("PLANTID"));// plantid
            grdTab2Detail.View.SetFocusedRowCellValue("PRICECOMBINATIONID", grdTab2Master.View.GetFocusedRowCellValue("PRICECOMBINATIONID"));// plantid
            DateTime dateNow = DateTime.Now;
            grdTab2Detail.View.SetFocusedRowCellValue("STARTDATE", dateNow.ToString("yyyy-MM-dd"));// 시작일
            grdTab2Detail.View.SetFocusedRowCellValue("ENDDATE", "9999-12-31");//종료일
            //grdOspPrice.View.CellValueChanged += GrdOspPrice_CellValueChanged;
            grdTab2Detail.View.ClearSorting();
            grdTab2Detail.View.Columns["STARTDATE"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            grdTab2Detail.View.SetFocusedRowCellValue("VALIDSTATE", "Valid");//
        }

        /// <summary>
        /// GrdDetail_ToolbarDeleteRow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdDetail_ToolbarDeleteRow(object sender, EventArgs e)
        {
            DataRow row = grdDetail.View.GetFocusedDataRow();
            if (row == null)
            {
                return;
            }
            if (row.RowState != DataRowState.Added)
            {
                row["VALIDSTATE"] = "Invalid";
                (grdDetail.View.DataSource as DataView).Table.AcceptChanges();
            }
        }

        /// <summary>
        /// GrdTab1Master_ToolbarDeleteRow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdTab1Master_ToolbarAddingRow(object sender, CancelEventArgs e)
        {
            if (pnlToolbar.Controls["layoutToolbar"] != null)
            {
                if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] == null)
                {
                    e.Cancel = true;
                    return;
                }
            }
            else
            {
                e.Cancel = true;
                return;
            }

            if (grdMaster.View.DataRowCount == 0)
            {
                e.Cancel = true;
                return;
            }
            DataTable changed = grdMaster.GetChangedRows();
            if (changed.Rows.Count > 0)
            {
                e.Cancel = true;
                return;
            }
            if (grdDetail.View.DataRowCount == 0)
            {
                e.Cancel = true;
                return;
            }
            DataTable changedetail = grdDetail.GetChangedRows();
            if (changedetail.Rows.Count > 0)
            {
                e.Cancel = true;
                return;
            }
            DataRow row = grdDetail.View.GetFocusedDataRow();
            OutsourcingYoungPongPriceCodePopup itemPopup = new OutsourcingYoungPongPriceCodePopup("N", row);
            itemPopup.ShowDialog(this);

            grdTab1Master.ToolbarAddingRow -= GrdTab1Master_ToolbarAddingRow;
            DataTable dtTab1Master = GetOspypricecombination("N");
            if (dtTab1Master.Rows.Count < 1)
            {
                //단가 그리드 clear
                DataTable dtPrice = (grdTab1Master.DataSource as DataTable).Clone();
                //grdDetail.View.ClearDatas();
                grdTab1Master.DataSource = dtPrice;
                grdTab1Detail.View.ClearDatas();
            }
            else
            {
                grdTab1Master.DataSource = dtTab1Master;
            }
            if (dtTab1Master.Rows.Count > 0)
            {
                grdTab1Master.View.FocusedRowHandle = 0;
                grdTab1Master.View.SelectRow(0);
                focusedRowChangedTab1Master();
            }
            grdTab1Master.ToolbarAddingRow += GrdTab1Master_ToolbarAddingRow;
            e.Cancel = true;

            //재조회??
        }

        /// <summary>
        /// GrdTab1Master_ToolbarDeleteRow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdTab2Master_ToolbarAddingRow(object sender, CancelEventArgs e)
        {
            if (pnlToolbar.Controls["layoutToolbar"] != null)
            {
                if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] == null)
                {
                    e.Cancel = true;
                    return;
                }
            }
            else
            {
                e.Cancel = true;
                return;
            }
            if (grdMaster.View.DataRowCount == 0)
            {
                e.Cancel = true;
                return;
            }
            DataTable changed = grdMaster.GetChangedRows();
            if (changed.Rows.Count != 0)
            {
                e.Cancel = true;
                return;
            }
            if (grdDetail.View.DataRowCount == 0)
            {
                e.Cancel = true;
                return;
            }
            DataTable changedetail = grdDetail.GetChangedRows();
            if (changedetail.Rows.Count != 0)
            {
                e.Cancel = true;
                return;
            }
            DataRow row = grdDetail.View.GetFocusedDataRow();
            OutsourcingYoungPongPriceCodePopup itemPopup = new OutsourcingYoungPongPriceCodePopup("Y", row);
            itemPopup.ShowDialog(this);

            grdTab2Master.ToolbarAddingRow -= GrdTab2Master_ToolbarAddingRow;
            DataTable dtTab2Master = GetOspypricecombination("Y");
            if (dtTab2Master.Rows.Count < 1)
            {
                //단가 그리드 clear
                DataTable dtPrice = (grdTab2Master.DataSource as DataTable).Clone();
                //grdDetail.View.ClearDatas();
                grdTab2Master.DataSource = dtPrice;
                grdTab2Detail.View.ClearDatas();
            }
            else
            {
                grdTab2Master.DataSource = dtTab2Master;
            }
            if (dtTab2Master.Rows.Count > 0)
            {
                grdTab2Master.View.FocusedRowHandle = 0;
                grdTab2Master.View.SelectRow(0);
                focusedRowChangedTab2Master();
            }
            grdTab2Master.ToolbarAddingRow += GrdTab2Master_ToolbarAddingRow;
            e.Cancel = true;

            //재조회??
        }

        /// <summary>
        /// GrdTab1Master_ToolbarDeleteRow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdTab1Master_ToolbarDeleteRow(object sender, EventArgs e)
        {
            DataRow row = grdTab1Master.View.GetFocusedDataRow();
            if (row == null)
            {
                return;
            }
            if (row.RowState != DataRowState.Added)
            {
                row["VALIDSTATE"] = "Invalid";
                (grdTab1Master.View.DataSource as DataView).Table.AcceptChanges();
            }
        }

        /// <summary>
        /// GrdTab1Master_ToolbarDeleteRow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdTab2Master_ToolbarDeleteRow(object sender, EventArgs e)
        {
            DataRow row = grdTab2Master.View.GetFocusedDataRow();
            if (row == null)
            {
                return;
            }
            if (row.RowState != DataRowState.Added)
            {
                row["VALIDSTATE"] = "Invalid";
                (grdTab2Master.View.DataSource as DataView).Table.AcceptChanges();
            }
        }

        /// <summary>
        /// GrdTab1Detail_ToolbarDeleteRow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdTab1Detail_ToolbarDeleteRow(object sender, EventArgs e)
        {
            DataRow row = grdTab1Detail.View.GetFocusedDataRow();
            if (row == null)
            {
                return;
            }
            if (row.RowState != DataRowState.Added)
            {
                row["VALIDSTATE"] = "Invalid";
                (grdTab1Detail.View.DataSource as DataView).Table.AcceptChanges();
            }
        }

        /// <summary>
        /// GrdTab2Detail_ToolbarDeleteRow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdTab2Detail_ToolbarDeleteRow(object sender, EventArgs e)
        {
            DataRow row = grdTab2Detail.View.GetFocusedDataRow();
            if (row == null)
            {
                return;
            }
            if (row.RowState != DataRowState.Added)
            {
                row["VALIDSTATE"] = "Invalid";
                (grdTab2Detail.View.DataSource as DataView).Table.AcceptChanges();
            }
        }

        /// <summary>
        /// 단가코드 리스트 그리드의 포커스 행 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            focusedRowChangedMaster();
        }

        /// <summary>
        /// 단가코드 리스트 그리드의 포커스 행 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdDetail_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            focusedRowChangedDetail();
        }

        /// <summary>
        /// 단가코드 리스트 그리드의 포커스 행 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdTab1Master_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            focusedRowChangedTab1Master();
        }

        private void GrdTab2Master_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            focusedRowChangedTab2Master();
        }

        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void GrdDetail_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (grdMaster.View.DataRowCount == 0)
            {
                int intfocusRow = grdDetail.View.FocusedRowHandle;
                grdDetail.View.DeleteRow(intfocusRow);
                return;
            }
            DataTable changed = grdMaster.GetChangedRows();
            if (changed.Rows.Count != 0)
            {
                int intfocusRow = grdDetail.View.FocusedRowHandle;
                grdDetail.View.DeleteRow(intfocusRow);
                return;
            }

            grdDetail.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdDetail.View.SetFocusedRowCellValue("PLANTID", grdMaster.View.GetFocusedRowCellValue("PLANTID"));// plantid
            grdDetail.View.SetFocusedRowCellValue("PROCESSSEGMENTCLASSID", grdMaster.View.GetFocusedRowCellValue("PROCESSSEGMENTCLASSID"));//
            grdDetail.View.SetFocusedRowCellValue("VALIDSTATE", "Valid");//
        }

        private void GrdDetail_ShowingEditor(object sender, CancelEventArgs e)
        {
            DataTable changed = grdMaster.GetChangedRows();

            if (changed.Rows.Count != 0)
            {
                e.Cancel = true;
            }
        }

        private void GrdTab1Master_ShowingEditor(object sender, CancelEventArgs e)
        {
            ////
            DataRow row = grdTab1Master.View.GetFocusedDataRow();

            if (row.RowState != DataRowState.Added)
            {
                e.Cancel = true;
            }
        }

        private void GrdTab2Master_ShowingEditor(object sender, CancelEventArgs e)
        {
            //
            DataRow row = grdTab2Master.View.GetFocusedDataRow();

            if (row.RowState != DataRowState.Added)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            throw new NotImplementedException();
        }

        #endregion Event

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
        }

        #endregion 툴바

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("P_ENTERPRISEID", UserInfo.Current.Enterprise);
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtPriceItem = await SqlExecuter.QueryAsync("GetOutsourcingYoungPongPriceCode", "10001", values);

            if (dtPriceItem.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                grdDetail.View.ClearDatas();
            }
            grdMaster.DataSource = dtPriceItem;
            if (dtPriceItem.Rows.Count > 0)
            {
                grdMaster.View.FocusedRowHandle = 0;
                grdMaster.View.SelectRow(0);
                focusedRowChangedMaster();
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            // 중공정
            InitializeConditionPopup_Processsegmentclassid();
        }

        /// <summary>
        /// 중공정 설정
        /// </summary>
        private void InitializeConditionPopup_Processsegmentclassid()
        {
            ////// 팝업 컬럼설정
            ////var processsegmentclassidPopupColumn = Conditions.AddSelectPopup("p_processsegmentclassid", new SqlQuery("GetProcesssegmentclassidListByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "MIDDLEPROCESSSEGMENTCLASSNAME", "MIDDLEPROCESSSEGMENTCLASSID")
            ////   .SetPopupLayout("MIDDLEPROCESSSEGMENTCLASS", PopupButtonStyles.Ok_Cancel, true, false)
            ////   .SetPopupLayoutForm(400, 600)
            ////   .SetLabel("MIDDLEPROCESSSEGMENTCLASS")
            ////   .SetPopupResultCount(1)
            ////   .SetPosition(0.2);

            ////// 팝업 조회조건
            ////processsegmentclassidPopupColumn.Conditions.AddTextBox("MIDDLEPROCESSSEGMENTCLASSNAME")
            ////    .SetLabel("MIDDLEPROCESSSEGMENTCLASSNAME");

            ////// 팝업 그리드
            ////processsegmentclassidPopupColumn.GridColumns.AddTextBoxColumn("MIDDLEPROCESSSEGMENTCLASSID", 150)
            ////    .SetValidationKeyColumn();
            ////processsegmentclassidPopupColumn.GridColumns.AddTextBoxColumn("MIDDLEPROCESSSEGMENTCLASSNAME", 200);
            var txtprocesssegmentclassid = Conditions.AddTextBox("p_processsegmentclassid")
             .SetLabel("MIDDLEPROCESSSEGMENTCLASS")
             .SetPosition(0.2);
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #endregion 검색

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            //base.OnValidateContent();

            //// TODO : 유효성 로직 변경
            //grdList.View.CheckValidation();

            //DataTable changed = grdList.GetChangedRows();

            //if (changed.Rows.Count == 0)
            //{
            //    // 저장할 데이터가 존재하지 않습니다.
            //    throw MessageException.Create("NoSaveData");
            //}
        }

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Save"))
            {
                ProcSave(btn.Text);
            }
        }

        #endregion 유효성 검사

        #region Private Function

        /// <summary>
        /// 그리드의 포커스 행 변경시 행의 작업목록의 내용을 매핑처리 함.
        /// </summary>
        private void focusedRowChangedMaster()
        {
            //포커스 행 체크
            if (grdMaster.View.FocusedRowHandle < 0) return;

            //단가코드 정보 가져오기
            Dictionary<string, object> Param = new Dictionary<string, object>();

            Param.Add("P_PLANTID", grdMaster.View.GetFocusedRowCellValue("PLANTID"));
            Param.Add("P_PROCESSSEGMENTCLASSID", grdMaster.View.GetFocusedRowCellValue("PROCESSSEGMENTCLASSID"));
            Param.Add("P_VALIDSTATE", "Valid");
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dtDetail = SqlExecuter.Query("GetOutsourcingYoungPongPriceCodeDetail", "10001", Param);

            if (dtDetail.Rows.Count < 1)
            {
                //단가 그리드 clear
                DataTable dtPrice = (grdDetail.DataSource as DataTable).Clone();
                //grdDetail.View.ClearDatas();
                grdDetail.DataSource = dtPrice;
                grdTab1Master.View.ClearDatas();
                grdTab2Master.View.ClearDatas();
                grdTab1Detail.View.ClearDatas();
                grdTab2Detail.View.ClearDatas();
            }
            else
            {
                grdDetail.DataSource = dtDetail;
            }
            if (dtDetail.Rows.Count > 0)
            {
                grdDetail.View.FocusedRowHandle = 0;
                grdDetail.View.SelectRow(0);
                focusedRowChangedDetail();
            }
            //포커스 이동 처리
        }

        private void focusedRowChangedDetail()
        {
            GrdTab1MasterDisplayCaptionHidden();
            GrdTab2MasterDisplayCaptionHidden();
            if (grdDetail.View.DataRowCount == 0)
            {
                grdTab1Master.View.ClearDatas();
                grdTab2Master.View.ClearDatas();
                grdTab1Detail.View.ClearDatas();
                grdTab2Detail.View.ClearDatas();
                return;
            }
            DataRow row = grdDetail.View.GetFocusedDataRow();
            if (row == null)
            {
                grdTab1Master.View.ClearDatas();
                grdTab2Master.View.ClearDatas();
                grdTab1Detail.View.ClearDatas();
                grdTab2Detail.View.ClearDatas();
                return;
            }

            //InitializeGrid_Tab1Master();
            InitializeGrid_GrdTab1MasterDisplayCaption();

            DataTable dtTab1Master = GetOspypricecombination("N");
            if (dtTab1Master.Rows.Count < 1)
            {
                //단가 그리드 clear
                DataTable dtPrice = (grdTab1Master.DataSource as DataTable).Clone();

                grdTab1Master.DataSource = dtPrice;
                grdTab1Detail.View.ClearDatas();
            }
            else
            {
                grdTab1Master.DataSource = dtTab1Master;
            }
            if (dtTab1Master.Rows.Count > 0)
            {
                grdTab1Master.View.FocusedRowHandle = 0;
                grdTab1Master.View.SelectRow(0);
                focusedRowChangedTab1Master();
            }

            ///InitializeGrid_GrdTab2MasterDisplay("Y");
            //InitializeGrid_Tab2Master();
            InitializeGrid_GrdTab2MasterDisplayCaption();
            DataTable dtTab2Master = GetOspypricecombination("Y");

            if (dtTab2Master.Rows.Count < 1)
            {
                //단가 그리드 clear
                DataTable dtPrice = (grdTab2Master.DataSource as DataTable).Clone();

                grdTab2Master.DataSource = dtPrice;
                grdTab2Detail.View.ClearDatas();
            }
            else
            {
                grdTab2Master.DataSource = dtTab2Master;
            }
            if (dtTab2Master.Rows.Count > 0)
            {
                grdTab2Master.View.FocusedRowHandle = 0;
                grdTab2Master.View.SelectRow(0);
                focusedRowChangedTab2Master();
            }

            //포커스 이동 처리
        }

        /// <summary>
        /// 그리드의 포커스 행 변경시 행의 작업목록의 내용을 매핑처리 함.
        /// </summary>
        private void focusedRowChangedTab1Master()
        {
            //포커스 행 체크
            if (grdTab1Master.View.FocusedRowHandle < 0) return;

            Dictionary<string, object> Param = new Dictionary<string, object>();

            Param.Add("P_PLANTID", grdTab1Master.View.GetFocusedRowCellValue("PLANTID"));
            Param.Add("P_PRICECOMBINATIONID", grdTab1Master.View.GetFocusedRowCellValue("PRICECOMBINATIONID"));
            Param.Add("P_VALIDSTATE", "Valid");
            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dtPrice = SqlExecuter.Query("GetOutsourcedBasedOspPriceList", "10001", Param);
            grdTab1Detail.DataSource = dtPrice;
            if (dtPrice.Rows.Count < 1)
            {
                //단가 그리드 clear
                DataTable dtDetail = (grdTab1Detail.DataSource as DataTable).Clone();
                //grdDetail.View.ClearDatas();
                grdTab1Detail.DataSource = dtDetail;
            }
            else
            {
                grdTab1Detail.DataSource = dtPrice;
            }
        }

        /// <summary>
        /// 그리드의 포커스 행 변경시 행의 작업목록의 내용을 매핑처리 함.
        /// </summary>
        private void focusedRowChangedTab2Master()
        {
            //포커스 행 체크
            if (grdTab2Master.View.FocusedRowHandle < 0) return;

            Dictionary<string, object> Param = new Dictionary<string, object>();

            Param.Add("P_PLANTID", grdTab2Master.View.GetFocusedRowCellValue("PLANTID"));
            Param.Add("P_PRICECOMBINATIONID", grdTab2Master.View.GetFocusedRowCellValue("PRICECOMBINATIONID"));
            Param.Add("P_VALIDSTATE", "Valid");
            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dtPrice = SqlExecuter.Query("GetOutsourcedBasedOspPriceList", "10001", Param);

            if (dtPrice.Rows.Count < 1)
            {
                //단가 그리드 clear
                DataTable dtDetail = (grdTab2Detail.DataSource as DataTable).Clone();
                //grdDetail.View.ClearDatas();
                grdTab2Detail.DataSource = dtDetail;
            }
            else
            {
                grdTab2Detail.DataSource = dtPrice;
            }
        }

        private DataTable GetOspypricecombination(string strProductGubun)
        {
            //단가코드 정보 가져오기
            Dictionary<string, object> Param = new Dictionary<string, object>();

            Param.Add("P_PLANTID", grdDetail.View.GetFocusedRowCellValue("PLANTID"));
            Param.Add("P_OSPPRICECODE", grdDetail.View.GetFocusedRowCellValue("OSPPRICECODE"));
            Param.Add("P_PRODUCTDEFID", strProductGubun);
            Param.Add("P_ISRANGE01", grdDetail.View.GetFocusedRowCellValue("ISRANGE01"));
            Param.Add("P_ISRANGE02", grdDetail.View.GetFocusedRowCellValue("ISRANGE02"));
            Param.Add("P_ISRANGE03", grdDetail.View.GetFocusedRowCellValue("ISRANGE03"));
            Param.Add("P_ISRANGE04", grdDetail.View.GetFocusedRowCellValue("ISRANGE04"));
            Param.Add("P_ISRANGE05", grdDetail.View.GetFocusedRowCellValue("ISRANGE05"));
            Param.Add("P_COMPONENTTYPE01", grdDetail.View.GetFocusedRowCellValue("COMPONENTTYPE01"));
            Param.Add("P_COMPONENTTYPE02", grdDetail.View.GetFocusedRowCellValue("COMPONENTTYPE02"));
            Param.Add("P_COMPONENTTYPE03", grdDetail.View.GetFocusedRowCellValue("COMPONENTTYPE03"));
            Param.Add("P_COMPONENTTYPE04", grdDetail.View.GetFocusedRowCellValue("COMPONENTTYPE04"));
            Param.Add("P_COMPONENTTYPE05", grdDetail.View.GetFocusedRowCellValue("COMPONENTTYPE05"));
            Param.Add("P_VALIDSTATE", "Valid");
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dtDetail = SqlExecuter.Query("GetOutsourcingYoungPongPricecombination", "10001", Param);

            return dtDetail;
        }

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 마스터 정보를 조회한다.
        /// </summary>
        private void LoadData()
        {
        }

        /// <summary>
        ///  중복 체크 함수
        /// </summary>
        /// <param name="dateStartdate"></param>
        /// <param name="dateEnddate"></param>
        /// <param name="icurRow"></param>
        /// <returns></returns>
        private int SearchidKey(string strValue, string colstringName, int icurRow)
        {
            int iresultRow = -1;

            for (int irow = 0; irow < grdDetail.View.DataRowCount; irow++)
            {
                if (icurRow != irow)
                {
                    DataRow row = grdDetail.View.GetDataRow(irow);

                    // 행 삭제만 제외
                    if (grdDetail.View.IsDeletedRow(row) == false)
                    {
                        string strTemnpValue = row[colstringName].ToString();

                        if (strValue.Equals(strTemnpValue))
                        {
                            return irow;
                        }
                    }
                }
            }
            return iresultRow;
        }

        private bool ProcSaveCheck()
        {
            grdMaster.View.FocusedRowHandle = grdMaster.View.FocusedRowHandle;
            grdMaster.View.FocusedColumn = grdMaster.View.Columns["PRICECLASSID"];
            grdMaster.View.ShowEditor();

            grdMaster.View.CheckValidation();

            DataTable changed = grdMaster.GetChangedRows();

            grdDetail.View.FocusedRowHandle = grdDetail.View.FocusedRowHandle;
            grdDetail.View.FocusedColumn = grdDetail.View.Columns["PRICEITEMNAME"];
            grdDetail.View.ShowEditor();

            grdDetail.View.CheckValidation();
            DataTable changedDetail = grdDetail.GetChangedRows();
            if (changedDetail.Rows.Count > 0)
            {
                if (CheckPriceDateKeyColumns() == false)
                {
                    return false;
                }
            }
            DataTable changedTab1Master = null;
            DataTable changedTabDetail = null;
            if (tabCom.SelectedTabPage.Name.Equals("tabProcesssegment"))
            {
                grdTab1Master.View.FocusedRowHandle = grdTab1Master.View.FocusedRowHandle;
                grdTab1Master.View.FocusedColumn = grdTab1Master.View.Columns["OSPVENDORNAME"];
                grdTab1Master.View.ShowEditor();

                grdTab1Master.View.CheckValidation();
                DataRow row = grdTab1Master.View.GetFocusedDataRow();
                changedTab1Master = grdTab1Master.GetChangedRows();
                grdTab1Detail.View.FocusedRowHandle = grdTab1Detail.View.FocusedRowHandle;
                grdTab1Detail.View.FocusedColumn = grdTab1Detail.View.Columns["STARTDATE"];
                grdTab1Detail.View.ShowEditor();

                grdTab1Detail.View.CheckValidation();
                changedTabDetail = grdTab1Detail.GetChangedRows();
            }
            else
            {
                grdTab2Master.View.FocusedRowHandle = grdTab2Master.View.FocusedRowHandle;
                grdTab2Master.View.FocusedColumn = grdTab2Master.View.Columns["OSPVENDORNAME"];
                grdTab2Master.View.ShowEditor();

                grdTab2Master.View.CheckValidation();
                changedTab1Master = grdTab2Master.GetChangedRows();

                grdTab2Detail.View.FocusedRowHandle = grdTab2Detail.View.FocusedRowHandle;
                grdTab2Detail.View.FocusedColumn = grdTab2Detail.View.Columns["STARTDATE"];
                grdTab2Detail.View.ShowEditor();

                grdTab2Detail.View.CheckValidation();
                changedTabDetail = grdTab2Detail.GetChangedRows();
            }
            if (changedTab1Master.Rows.Count > 0)
            {
                if (ProcSaveCheckTabMaster() == false)
                {
                    return false;
                }
            }
            if (changedTabDetail.Rows.Count > 0)
            {
                if (ProcSaveCheckTabDetail() == false)
                {
                    return false;
                }
            }
            if (changed.Rows.Count == 0 && changedDetail.Rows.Count == 0 && changedTab1Master.Rows.Count == 0 && changedTabDetail.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK, "NoSaveData");
                return false;
            }

            return true;
        }

        private bool ProcSaveCheckTabMaster()
        {
            if (tabCom.SelectedTabPage.Name.Equals("tabProcesssegment"))
            {
                for (int irow = 0; irow < grdTab1Master.View.DataRowCount; irow++)
                {
                    DataRow row = grdTab1Master.View.GetDataRow(irow);

                    if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                    {
                        if (grdTab1Master.View.Columns["ITEMVALUE01"].Visible == true && row["ITEMVALUE01"].ToString().Equals(""))
                        {
                            string lblQty = grdTab1Master.View.Columns["ITEMVALUE01"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                            return false;
                        }
                        if (grdTab1Master.View.Columns["ITEMVALUE02"].Visible == true && row["ITEMVALUE02"].ToString().Equals(""))
                        {
                            string lblQty = grdTab1Master.View.Columns["ITEMVALUE02"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                            return false;
                        }
                        if (grdTab1Master.View.Columns["ITEMVALUE03"].Visible == true && row["ITEMVALUE03"].ToString().Equals(""))
                        {
                            string lblQty = grdTab1Master.View.Columns["ITEMVALUE03"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                            return false;
                        }
                        if (grdTab1Master.View.Columns["ITEMVALUE04"].Visible == true && row["ITEMVALUE04"].ToString().Equals(""))
                        {
                            string lblQty = grdTab1Master.View.Columns["ITEMVALUE04"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                            return false;
                        }
                        if (grdTab1Master.View.Columns["ITEMVALUE05"].Visible == true && row["ITEMVALUE05"].ToString().Equals(""))
                        {
                            string lblQty = grdTab1Master.View.Columns["ITEMVALUE01FR"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                            return false;
                        }
                        if (grdTab1Master.View.Columns["ITEMVALUE01FR"].Visible == true && row["ITEMVALUE01FR"].ToString().Equals(""))
                        {
                            string lblQty = grdTab1Master.View.Columns["ITEMVALUE01FR"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                            return false;
                        }
                        if (grdTab1Master.View.Columns["ITEMVALUE01TO"].Visible == true && row["ITEMVALUE01TO"].ToString().Equals(""))
                        {
                            string lblQty = grdTab1Master.View.Columns["ITEMVALUE01TO"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                            return false;
                        }
                        if (grdTab1Master.View.Columns["ITEMVALUE02FR"].Visible == true && row["ITEMVALUE02FR"].ToString().Equals(""))
                        {
                            string lblQty = grdTab1Master.View.Columns["ITEMVALUE02FR"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                            return false;
                        }
                        if (grdTab1Master.View.Columns["ITEMVALUE02TO"].Visible == true && row["ITEMVALUE02TO"].ToString().Equals(""))
                        {
                            string lblQty = grdTab1Master.View.Columns["ITEMVALUE02TO"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                            return false;
                        }
                        if (grdTab1Master.View.Columns["ITEMVALUE03FR"].Visible == true && row["ITEMVALUE03FR"].ToString().Equals(""))
                        {
                            string lblQty = grdTab1Master.View.Columns["ITEMVALUE03FR"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                            return false;
                        }
                        if (grdTab1Master.View.Columns["ITEMVALUE03TO"].Visible == true && row["ITEMVALUE03TO"].ToString().Equals(""))
                        {
                            string lblQty = grdTab1Master.View.Columns["ITEMVALUE03TO"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                        }
                        if (grdTab1Master.View.Columns["ITEMVALUE04FR"].Visible == true && row["ITEMVALUE04FR"].ToString().Equals(""))
                        {
                            string lblQty = grdTab1Master.View.Columns["ITEMVALUE04FR"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                            return false;
                        }
                        if (grdTab1Master.View.Columns["ITEMVALUE04TO"].Visible == true && row["ITEMVALUE04TO"].ToString().Equals(""))
                        {
                            string lblQty = grdTab1Master.View.Columns["ITEMVALUE04TO"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                        }
                        if (grdTab1Master.View.Columns["ITEMVALUE05FR"].Visible == true && row["ITEMVALUE05FR"].ToString().Equals(""))
                        {
                            string lblQty = grdTab1Master.View.Columns["ITEMVALUE05FR"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                            return false;
                        }
                        if (grdTab1Master.View.Columns["ITEMVALUE05TO"].Visible == true && row["ITEMVALUE05TO"].ToString().Equals(""))
                        {
                            string lblQty = grdTab1Master.View.Columns["ITEMVALUE05TO"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                            return false;
                        }
                        if (row["OSPVENDORID"].ToString().Equals(""))
                        {
                            row["OSPVENDORID"] = "*";
                        }
                        int checkrow = SearchTab1MasterValueKey(irow);
                        if (checkrow > -1)
                        {
                            string lblPeriodid = grdTab1Master.Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspData007", lblPeriodid);
                            return false;
                        }
                    }
                }
            }
            else
            {
                for (int irow = 0; irow < grdTab2Master.View.DataRowCount; irow++)
                {
                    DataRow row = grdTab2Master.View.GetDataRow(irow);

                    if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                    {
                        if (row["PRODUCTDEFID"].ToString().Equals("") || row["PRODUCTDEFID"].ToString().Equals("*"))
                        {
                            string lblQty = grdTab2Master.View.Columns["PRODUCTDEFID"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                            return false;
                        }
                        if (grdTab2Master.View.Columns["ITEMVALUE01"].Visible == true && row["ITEMVALUE01"].ToString().Equals(""))
                        {
                            string lblQty = grdTab2Master.View.Columns["ITEMVALUE01"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                            return false;
                        }
                        if (grdTab2Master.View.Columns["ITEMVALUE02"].Visible == true && row["ITEMVALUE02"].ToString().Equals(""))
                        {
                            string lblQty = grdTab2Master.View.Columns["ITEMVALUE02"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                            return false;
                        }
                        if (grdTab2Master.View.Columns["ITEMVALUE03"].Visible == true && row["ITEMVALUE03"].ToString().Equals(""))
                        {
                            string lblQty = grdTab2Master.View.Columns["ITEMVALUE03"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                            return false;
                        }
                        if (grdTab2Master.View.Columns["ITEMVALUE04"].Visible == true && row["ITEMVALUE04"].ToString().Equals(""))
                        {
                            string lblQty = grdTab2Master.View.Columns["ITEMVALUE04"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                            return false;
                        }
                        if (grdTab2Master.View.Columns["ITEMVALUE05"].Visible == true && row["ITEMVALUE05"].ToString().Equals(""))
                        {
                            string lblQty = grdTab2Master.View.Columns["ITEMVALUE01FR"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                            return false;
                        }
                        if (grdTab2Master.View.Columns["ITEMVALUE01FR"].Visible == true && row["ITEMVALUE01FR"].ToString().Equals(""))
                        {
                            string lblQty = grdTab2Master.View.Columns["ITEMVALUE01FR"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                            return false;
                        }
                        if (grdTab2Master.View.Columns["ITEMVALUE01TO"].Visible == true && row["ITEMVALUE01TO"].ToString().Equals(""))
                        {
                            string lblQty = grdTab2Master.View.Columns["ITEMVALUE01TO"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                            return false;
                        }
                        if (grdTab2Master.View.Columns["ITEMVALUE02FR"].Visible == true && row["ITEMVALUE02FR"].ToString().Equals(""))
                        {
                            string lblQty = grdTab2Master.View.Columns["ITEMVALUE02FR"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                            return false;
                        }
                        if (grdTab2Master.View.Columns["ITEMVALUE02TO"].Visible == true && row["ITEMVALUE02TO"].ToString().Equals(""))
                        {
                            string lblQty = grdTab2Master.View.Columns["ITEMVALUE02TO"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                            return false;
                        }
                        if (grdTab2Master.View.Columns["ITEMVALUE03FR"].Visible == true && row["ITEMVALUE03FR"].ToString().Equals(""))
                        {
                            string lblQty = grdTab2Master.View.Columns["ITEMVALUE03FR"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                            return false;
                        }
                        if (grdTab2Master.View.Columns["ITEMVALUE03TO"].Visible == true && row["ITEMVALUE03TO"].ToString().Equals(""))
                        {
                            string lblQty = grdTab2Master.View.Columns["ITEMVALUE03TO"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                            return false;
                        }
                        if (grdTab2Master.View.Columns["ITEMVALUE04FR"].Visible == true && row["ITEMVALUE04FR"].ToString().Equals(""))
                        {
                            string lblQty = grdTab2Master.View.Columns["ITEMVALUE04FR"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                        }
                        if (grdTab2Master.View.Columns["ITEMVALUE04TO"].Visible == true && row["ITEMVALUE04TO"].ToString().Equals(""))
                        {
                            string lblQty = grdTab2Master.View.Columns["ITEMVALUE04TO"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                            return false;
                        }
                        if (grdTab2Master.View.Columns["ITEMVALUE05FR"].Visible == true && row["ITEMVALUE05FR"].ToString().Equals(""))
                        {
                            string lblQty = grdTab2Master.View.Columns["ITEMVALUE05FR"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                            return false;
                        }
                        if (grdTab2Master.View.Columns["ITEMVALUE05TO"].Visible == true && row["ITEMVALUE05TO"].ToString().Equals(""))
                        {
                            string lblQty = grdTab2Master.View.Columns["ITEMVALUE05TO"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                            return false;
                        }
                        if (row["OSPVENDORID"].ToString().Equals(""))
                        {
                            row["OSPVENDORID"] = "*";
                        }
                        int checkrow = SearchTab2MasterValueKey(irow);
                        if (checkrow > -1)
                        {
                            string lblPeriodid = grdTab2Master.Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspData007", lblPeriodid);
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private bool ProcSaveCheckTabDetail()
        {
            DataTable changedTabDetail = null;
            if (tabCom.SelectedTabPage.Name.Equals("tabProcesssegment"))
            {
                grdTab1Detail.View.FocusedRowHandle = grdTab1Detail.View.FocusedRowHandle;
                grdTab1Detail.View.FocusedColumn = grdTab1Detail.View.Columns["STARTDATE"];
                grdTab1Detail.View.ShowEditor();

                grdTab1Detail.View.CheckValidation();
                changedTabDetail = grdTab1Detail.GetChangedRows();
                for (int irow = 0; irow < changedTabDetail.Rows.Count; irow++)
                {
                    DataRow row = changedTabDetail.Rows[irow];
                    DateTime StartDate = Convert.ToDateTime(row["STARTDATE"]);
                    DateTime EndDate = Convert.ToDateTime(row["ENDDATE"]);
                    if (StartDate > EndDate)
                    {
                        // 다국어 메세지 처리 (종료일 보다 시작일이 자료가 존재합니다.)

                        this.ShowMessage(MessageBoxButtons.OK, "OspCheckStartEnd");
                        return false;
                    }
                }
                if (CheckPriceTabDateKeyColumns(grdTab1Detail) == false)
                {
                    this.ShowMessage(MessageBoxButtons.OK, "OspCheckDuplStartEnd");
                    return false;
                }
            }
            else
            {
                grdTab2Detail.View.FocusedRowHandle = grdTab2Detail.View.FocusedRowHandle;
                grdTab2Detail.View.FocusedColumn = grdTab2Detail.View.Columns["STARTDATE"];
                grdTab2Detail.View.ShowEditor();

                grdTab2Detail.View.CheckValidation();
                changedTabDetail = grdTab2Detail.GetChangedRows();

                for (int irow = 0; irow < changedTabDetail.Rows.Count; irow++)
                {
                    DataRow row = changedTabDetail.Rows[irow];
                    DateTime StartDate = Convert.ToDateTime(row["STARTDATE"]);
                    DateTime EndDate = Convert.ToDateTime(row["ENDDATE"]);
                    if (StartDate > EndDate)
                    {
                        // 다국어 메세지 처리 (종료일 보다 시작일이 자료가 존재합니다.)

                        this.ShowMessage(MessageBoxButtons.OK, "OspCheckStartEnd");
                        return false;
                    }
                }
                if (CheckPriceTabDateKeyColumns(grdTab2Detail) == false)
                {
                    this.ShowMessage(MessageBoxButtons.OK, "OspCheckDuplStartEnd");
                    return false;
                }
            }

            return true;
        }

        private void ProcSave(string strtitle)
        {
            if (ProcSaveCheck() == false)
            {
                return;
            }

            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", strtitle);//저장하시겠습니까?

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    DataTable dtSaveMaster = grdMaster.GetChangedRows();
                    dtSaveMaster.TableName = "listMain";
                    DataTable dtSaveDetail = grdDetail.GetChangedRows();
                    dtSaveDetail.TableName = "listDetail";
                    DataTable dtSaveTab1Master = null;
                    DataTable dtSaveTab1Temp = null;

                    if (tabCom.SelectedTabPage.Name.Equals("tabProcesssegment"))
                    {
                        dtSaveTab1Master = grdTab1Master.GetChangedRows();
                        dtSaveTab1Temp = grdTab1Detail.GetChangedRows();
                    }
                    else
                    {
                        dtSaveTab1Master = grdTab2Master.GetChangedRows();
                        dtSaveTab1Temp = grdTab2Detail.GetChangedRows();
                    }
                    dtSaveTab1Master.TableName = "listTab1Main";

                    DataTable dtSaveTab1Detail = dtSaveTab1Temp.Clone(); //테이블 레이아웃 복사
                    DataRow[] drSearch = dtSaveTab1Temp.Select("_STATE_ ='deleted'");
                    for (int i = 0; i < drSearch.Length; i++)
                    {
                        dtSaveTab1Detail.ImportRow(drSearch[i]);
                    }

                    drSearch = dtSaveTab1Temp.Select("_STATE_ ='modified'");
                    for (int i = 0; i < drSearch.Length; i++)
                    {
                        dtSaveTab1Detail.ImportRow(drSearch[i]);
                    }

                    drSearch = dtSaveTab1Temp.Select("_STATE_ ='added'");
                    for (int i = 0; i < drSearch.Length; i++)
                    {
                        dtSaveTab1Detail.ImportRow(drSearch[i]);
                    }
                    dtSaveTab1Detail.TableName = "listTab1Detail";
                    DataSet dsSave = new DataSet();
                    dsSave.Tables.Add(dtSaveMaster);
                    dsSave.Tables.Add(dtSaveDetail);
                    dsSave.Tables.Add(dtSaveTab1Master);
                    dsSave.Tables.Add(dtSaveTab1Detail);

                    DataTable saveResult = this.ExecuteRule<DataTable>("OutsourcingYoungPongPriceCode", dsSave);
                    DataRow resultData = saveResult.Rows[0];
                    string strtempno = resultData.ItemArray[0].ToString();
                    ShowMessage("SuccessOspProcess");
                    if (dtSaveMaster.Rows.Count > 0)
                    {
                        OnSaveMasterDisplay();
                    }
                    if (dtSaveDetail.Rows.Count > 0)
                    {
                        OnSaveDetailDisplay();
                    }
                    if (dtSaveTab1Master.Rows.Count > 0)
                    {
                        OnSaveTabMasterDisplay(strtempno);
                    }
                    if (dtSaveTab1Detail.Rows.Count > 0)
                    {
                        OnSaveTabDetailDisplay();
                    }
                    //이동 처리 ..
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();
                }
            }
        }

        private void OnSaveMasterDisplay()
        {
            string strProcesssegmentclassid = grdMaster.View.GetFocusedRowCellValue("PROCESSSEGMENTCLASSID").ToString();
            OnSaveConfrimSearch();
            if (!(strProcesssegmentclassid.Equals("")))
            {
                int irow = GetGridRowSearch(grdMaster.View, "PROCESSSEGMENTCLASSID", strProcesssegmentclassid);
                if (irow >= 0)
                {
                    grdMaster.View.FocusedRowHandle = irow;
                    grdMaster.View.SelectRow(irow);
                    focusedRowChangedMaster();
                }
                else
                {
                    grdMaster.View.FocusedRowHandle = 0;
                    grdMaster.View.SelectRow(0);
                    focusedRowChangedMaster();
                }
            }
        }

        private void OnSaveDetailDisplay()
        {
            string stOsppricecode = grdDetail.View.GetFocusedRowCellValue("OSPPRICECODE").ToString();
            focusedRowChangedMaster();
            int irow = GetGridRowSearch(grdDetail.View, "OSPPRICECODE", stOsppricecode);
            if (irow >= 0)
            {
                grdDetail.View.FocusedRowHandle = irow;
                grdDetail.View.SelectRow(irow);
                focusedRowChangedDetail();
            }
            else
            {
                grdDetail.View.FocusedRowHandle = 0;
                grdDetail.View.SelectRow(0);
                focusedRowChangedDetail();
            }
        }

        private void OnSaveTabMasterDisplay(string strtempno)
        {
            if (tabCom.SelectedTabPage.Name.Equals("tabProcesssegment"))
            {
                //재조회
                DataTable dtTab1Master = GetOspypricecombination("N");
                if (dtTab1Master.Rows.Count < 1)
                {
                    //단가 그리드 clear
                    DataTable dtPrice = (grdTab1Master.DataSource as DataTable).Clone();
                    //grdDetail.View.ClearDatas();
                    grdTab1Master.DataSource = dtPrice;
                    grdTab1Detail.View.ClearDatas();
                }
                else
                {
                    grdTab1Master.DataSource = dtTab1Master;
                }
                int irow = GetGridRowSearch(grdTab1Master.View, "PRICECOMBINATIONID", strtempno);
                if (irow >= 0)
                {
                    grdTab1Master.View.FocusedRowHandle = irow;
                    grdTab1Master.View.SelectRow(irow);
                    focusedRowChangedTab1Master();
                }
                else
                {
                    grdTab1Master.View.FocusedRowHandle = 0;
                    grdTab1Master.View.SelectRow(0);
                    focusedRowChangedTab1Master();
                }
            }
            else
            {
                DataTable dtTab1Master = GetOspypricecombination("Y");
                if (dtTab1Master.Rows.Count < 1)
                {
                    //단가 그리드 clear
                    DataTable dtPrice = (grdTab2Master.DataSource as DataTable).Clone();
                    //grdDetail.View.ClearDatas();
                    grdTab2Master.DataSource = dtPrice;
                    grdTab2Detail.View.ClearDatas();
                }
                else
                {
                    grdTab2Master.DataSource = dtTab1Master;
                }
                int irow = GetGridRowSearch(grdTab2Master.View, "PRICECOMBINATIONID", strtempno);
                if (irow >= 0)
                {
                    grdTab2Master.View.FocusedRowHandle = irow;
                    grdTab2Master.View.SelectRow(irow);
                    focusedRowChangedTab2Master();
                }
                else
                {
                    grdTab2Master.View.FocusedRowHandle = 0;
                    grdTab2Master.View.SelectRow(0);
                    focusedRowChangedTab2Master();
                }
            }
        }

        private void OnSaveTabDetailDisplay()
        {
            if (tabCom.SelectedTabPage.Name.Equals("tabProcesssegment"))
            {
                //재조회
                string strtempno = grdTab1Detail.View.GetFocusedRowCellValue("STARTDATE").ToString();
                focusedRowChangedTab1Master();
                int irow = GetGridRowSearch(grdTab1Detail.View, "STARTDATE", strtempno);
                if (irow >= 0)
                {
                    grdTab1Detail.View.FocusedRowHandle = irow;
                    grdTab1Detail.View.SelectRow(irow);
                }
                else
                {
                    grdTab1Detail.View.FocusedRowHandle = 0;
                    grdTab1Detail.View.SelectRow(0);
                }
            }
            else
            {
                string strtempno = grdTab2Detail.View.GetFocusedRowCellValue("STARTDATE").ToString();
                focusedRowChangedTab2Master();
                int irow = GetGridRowSearch(grdTab2Detail.View, "STARTDATE", strtempno);
                if (irow >= 0)
                {
                    grdTab2Detail.View.FocusedRowHandle = irow;
                    grdTab2Detail.View.SelectRow(irow);
                }
                else
                {
                    grdTab2Detail.View.FocusedRowHandle = 0;
                    grdTab2Detail.View.SelectRow(0);
                }
            }
        }

        private void OnSaveConfrimSearch()
        {
            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("P_ENTERPRISEID", UserInfo.Current.Enterprise);
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtPriceItem = SqlExecuter.Query("GetOutsourcingYoungPongPriceCode", "10001", values);

            grdMaster.DataSource = dtPriceItem;
        }

        /// <summary>
        /// 그리드 이동에 필요한 row 찾기
        /// </summary>
        /// <param name="strRequestno"></param>
        private int GetGridRowSearch(SmartBandedGridView TargetView, string strcol, string strvalue)
        {
            int iRow = -1;
            if (TargetView.DataRowCount == 0)
            {
                return iRow;
            }
            for (int i = 0; i < TargetView.DataRowCount; i++)
            {
                if (TargetView.GetRowCellValue(i, strcol).ToString().Equals(strvalue))
                {
                    return i;
                }
            }
            return iRow;
        }

        private void InitializeGrid_GrdTab2MasterDisplay(string IsProduct)
        {
            grdTab2Master.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdTab2Master.View.AddTextBoxColumn("ENTERPRISEID", 200).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("PLANTID", 200).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("PRICECOMBINATIONID", 200).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("PRICECOMBINATIONNAME", 200).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("OSPPRICECODE", 100).SetIsHidden();

            grdTab2Master.View.AddComboBoxColumn("OSPPRODUCTIONTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OSPProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("PRODUCTIONTYPE")
                .SetEmptyItem("*", "*")
                .SetIsHidden();  //
            InitializeGrid_productdePopup();
            grdTab2Master.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsReadOnly();
            grdTab2Master.View.AddTextBoxColumn("PRODUCTDEFNAME", 150).SetIsReadOnly();
            grdTab2Master.View.AddTextBoxColumn("PRICEITEMID01", 200).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("PRICEITEMID02", 200).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("PRICEITEMID03", 200).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("PRICEITEMID04", 200).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("PRICEITEMID05", 200).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("ISRANGE01", 200).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("ISRANGE02", 200).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("ISRANGE03", 200).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("ISRANGE04", 200).SetIsHidden();
            grdTab2Master.View.AddTextBoxColumn("ISRANGE05", 200).SetIsHidden();
            DataRow row = grdDetail.View.GetFocusedDataRow();
            if (row != null)
            {
                #region PRICEITEMID01

                if (!(row["PRICEITEMID01"].ToString().Equals("")))
                {
                    if (row["ISRANGE01"].ToString().Equals("Y"))  //구간여부
                    {
                        string strFormatmask = row["FORMATMASK01"].ToString();
                        if (strFormatmask.Equals(""))
                        {
                            strFormatmask = "#,###";
                        }
                        grdTab2Master.View.AddSpinEditColumn("ITEMVALUE01FR", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric);//
                        grdTab2Master.View.AddSpinEditColumn("ITEMVALUE01TO", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric);//
                        grdTab2Master.View.AddTextBoxColumn("ITEMVALUE01", 100).SetIsHidden();
                        grdTab2Master.View.AddTextBoxColumn("ITEMVALUE01NAME", 100).SetIsHidden();
                    }
                    else
                    {
                        grdTab2Master.View.AddTextBoxColumn("ITEMVALUE01FR", 80).SetIsHidden();
                        grdTab2Master.View.AddTextBoxColumn("ITEMVALUE01TO", 80).SetIsHidden();
                        if (row["COMPONENTTYPE01"].ToString().Equals("PopUp"))
                        {
                            InitializeGrid_Tab2CommonPopup(row["DATASETTYPE01"].ToString(), row["DATASET01"].ToString(), "ITEMVALUE01", "ITEMVALUE01NAME");
                            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE01NAME", 100).SetIsReadOnly();
                        }
                        else if (row["COMPONENTTYPE01"].ToString().Equals("ComboBox"))
                        {
                            if (row["DATASETTYPE01"].ToString().Equals("CodeClass"))
                            {
                                string param = "CODECLASSID=" + row["DATASET01"].ToString();
                                grdTab2Master.View.AddComboBoxColumn("ITEMVALUE01", 100, new SqlQuery("GetCodeList", "00001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                   .SetEmptyItem("*", "*");  //
                            }
                            else
                            {
                                string param = row["DATASET01"].ToString();
                                grdTab2Master.View.AddComboBoxColumn("ITEMVALUE01", 100, new SqlQuery("GetCodeList", "10001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                   .SetEmptyItem("*", "*");  //
                            }
                            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE01NAME", 100).SetIsHidden();
                        }
                        else
                        {
                            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE01", 100);
                            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE01NAME", 100).SetIsHidden();
                        }
                    }
                }
                else
                {
                    grdTab2Master.View.AddTextBoxColumn("ITEMVALUE01", 200).SetIsHidden();
                    grdTab2Master.View.AddTextBoxColumn("ITEMVALUE01FR", 200).SetIsHidden();
                    grdTab2Master.View.AddTextBoxColumn("ITEMVALUE01TO", 200).SetIsHidden();
                }

                #endregion PRICEITEMID01

                #region PRICEITEMID02

                if (!(row["PRICEITEMID02"].ToString().Equals("")))
                {
                    if (row["ISRANGE02"].ToString().Equals("Y"))  //구간여부
                    {
                        string strFormatmask = row["FORMATMASK02"].ToString();
                        if (strFormatmask.Equals(""))
                        {
                            strFormatmask = "#,###";
                        }
                        grdTab2Master.View.AddSpinEditColumn("ITEMVALUE02FR", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric);//
                        grdTab2Master.View.AddSpinEditColumn("ITEMVALUE02TO", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric);//
                        grdTab2Master.View.AddTextBoxColumn("ITEMVALUE02", 100).SetIsHidden();
                        grdTab2Master.View.AddTextBoxColumn("ITEMVALUE02NAME", 100).SetIsHidden();
                    }
                    else
                    {
                        grdTab2Master.View.AddTextBoxColumn("ITEMVALUE02FR", 80).SetIsHidden();
                        grdTab2Master.View.AddTextBoxColumn("ITEMVALUE02TO", 80).SetIsHidden();
                        if (row["COMPONENTTYPE02"].ToString().Equals("PopUp"))
                        {
                            InitializeGrid_Tab2CommonPopup(row["DATASETTYPE02"].ToString(), row["DATASET02"].ToString(), "ITEMVALUE02", "ITEMVALUE02NAME");
                            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE02NAME", 100).SetIsReadOnly();
                        }
                        else if (row["COMPONENTTYPE02"].ToString().Equals("ComboBox"))
                        {
                            if (row["DATASETTYPE02"].ToString().Equals("CodeClass"))
                            {
                                string param = "CODECLASSID=" + row["DATASET02"].ToString();
                                grdTab2Master.View.AddComboBoxColumn("ITEMVALUE02", 100, new SqlQuery("GetCodeList", "00001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                   .SetEmptyItem("*", "*");  //
                            }
                            else
                            {
                                string param = row["DATASET02"].ToString();
                                grdTab2Master.View.AddComboBoxColumn("ITEMVALUE02", 100, new SqlQuery("GetCodeList", "10001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                   .SetEmptyItem("*", "*");  //
                            }
                            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE02NAME", 100).SetIsHidden();
                        }
                        else
                        {
                            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE02", 100);
                            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE02NAME", 100).SetIsHidden();
                        }
                    }
                }
                else
                {
                    grdTab2Master.View.AddTextBoxColumn("ITEMVALUE02", 200).SetIsHidden();
                    grdTab2Master.View.AddTextBoxColumn("ITEMVALUE02FR", 200).SetIsHidden();
                    grdTab2Master.View.AddTextBoxColumn("ITEMVALUE02TO", 200).SetIsHidden();
                }

                #endregion PRICEITEMID02

                #region PRICEITEMID03

                if (!(row["PRICEITEMID03"].ToString().Equals("")))
                {
                    if (row["ISRANGE03"].ToString().Equals("Y"))  //구간여부
                    {
                        string strFormatmask = row["FORMATMASK03"].ToString();
                        if (strFormatmask.Equals(""))
                        {
                            strFormatmask = "#,###";
                        }
                        grdTab2Master.View.AddSpinEditColumn("ITEMVALUE03FR", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric);//
                        grdTab2Master.View.AddSpinEditColumn("ITEMVALUE03TO", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric);//
                        grdTab2Master.View.AddTextBoxColumn("ITEMVALUE03", 100).SetIsHidden();
                        grdTab2Master.View.AddTextBoxColumn("ITEMVALUE03NAME", 100).SetIsHidden();
                    }
                    else
                    {
                        grdTab2Master.View.AddTextBoxColumn("ITEMVALUE03FR", 80).SetIsHidden();
                        grdTab2Master.View.AddTextBoxColumn("ITEMVALUE03TO", 80).SetIsHidden();
                        if (row["COMPONENTTYPE03"].ToString().Equals("PopUp"))
                        {
                            InitializeGrid_Tab2CommonPopup(row["DATASETTYPE03"].ToString(), row["DATASET03"].ToString(), "ITEMVALUE03", "ITEMVALUE03NAME");
                            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE03NAME", 100).SetIsReadOnly();
                        }
                        else if (row["COMPONENTTYPE03"].ToString().Equals("ComboBox"))
                        {
                            if (row["DATASETTYPE03"].ToString().Equals("CodeClass"))
                            {
                                string param = "CODECLASSID=" + row["DATASET03"].ToString();
                                grdTab2Master.View.AddComboBoxColumn("ITEMVALUE03", 100, new SqlQuery("GetCodeList", "00001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                   .SetEmptyItem("*", "*");  //
                            }
                            else
                            {
                                string param = row["DATASET03"].ToString();
                                grdTab2Master.View.AddComboBoxColumn("ITEMVALUE03", 100, new SqlQuery("GetCodeList", "10001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                   .SetEmptyItem("*", "*");  //
                            }
                            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE03NAME", 100).SetIsHidden();
                        }
                        else
                        {
                            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE03", 100);
                            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE03NAME", 100).SetIsHidden();
                        }
                    }
                }
                else
                {
                    grdTab2Master.View.AddTextBoxColumn("ITEMVALUE03", 200).SetIsHidden();
                    grdTab2Master.View.AddTextBoxColumn("ITEMVALUE03FR", 200).SetIsHidden();
                    grdTab2Master.View.AddTextBoxColumn("ITEMVALUE03TO", 200).SetIsHidden();
                }

                #endregion PRICEITEMID03

                #region PRICEITEMID04

                if (!(row["PRICEITEMID04"].ToString().Equals("")))
                {
                    if (row["ISRANGE04"].ToString().Equals("Y"))  //구간여부
                    {
                        string strFormatmask = row["FORMATMASK04"].ToString();
                        if (strFormatmask.Equals(""))
                        {
                            strFormatmask = "#,###";
                        }
                        grdTab2Master.View.AddSpinEditColumn("ITEMVALUE04FR", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric);//
                        grdTab2Master.View.AddSpinEditColumn("ITEMVALUE04TO", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric);//
                        grdTab2Master.View.AddTextBoxColumn("ITEMVALUE04", 100).SetIsHidden();
                        grdTab2Master.View.AddTextBoxColumn("ITEMVALUE04NAME", 100).SetIsHidden();
                    }
                    else
                    {
                        grdTab2Master.View.AddTextBoxColumn("ITEMVALUE04FR", 80).SetIsHidden();
                        grdTab2Master.View.AddTextBoxColumn("ITEMVALUE04TO", 80).SetIsHidden();
                        if (row["COMPONENTTYPE04"].ToString().Equals("PopUp"))
                        {
                            InitializeGrid_Tab2CommonPopup(row["DATASETTYPE04"].ToString(), row["DATASET04"].ToString(), "ITEMVALUE04", "ITEMVALUE04NAME");
                            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE04NAME", 100).SetIsReadOnly();
                        }
                        else if (row["COMPONENTTYPE04"].ToString().Equals("ComboBox"))
                        {
                            if (row["DATASETTYPE04"].ToString().Equals("CodeClass"))
                            {
                                string param = "CODECLASSID=" + row["DATASET04"].ToString();
                                grdTab2Master.View.AddComboBoxColumn("ITEMVALUE04", 100, new SqlQuery("GetCodeList", "00001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                   .SetEmptyItem("*", "*");  //
                            }
                            else
                            {
                                string param = row["DATASET04"].ToString();
                                grdTab2Master.View.AddComboBoxColumn("ITEMVALUE04", 100, new SqlQuery("GetCodeList", "10001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                   .SetEmptyItem("*", "*");  //
                            }
                            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE04NAME", 100).SetIsHidden();
                        }
                        else
                        {
                            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE04", 100);
                            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE04NAME", 100).SetIsHidden();
                        }
                    }
                }
                else
                {
                    grdTab2Master.View.AddTextBoxColumn("ITEMVALUE04", 200).SetIsHidden();
                    grdTab2Master.View.AddTextBoxColumn("ITEMVALUE04FR", 200).SetIsHidden();
                    grdTab2Master.View.AddTextBoxColumn("ITEMVALUE04TO", 200).SetIsHidden();
                }

                #endregion PRICEITEMID04

                #region PRICEITEMID05

                if (!(row["PRICEITEMID05"].ToString().Equals("")))
                {
                    if (row["ISRANGE05"].ToString().Equals("Y"))  //구간여부
                    {
                        string strFormatmask = row["FORMATMASK05"].ToString();
                        if (strFormatmask.Equals(""))
                        {
                            strFormatmask = "#,###";
                        }
                        grdTab2Master.View.AddSpinEditColumn("ITEMVALUE05FR", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric);//
                        grdTab2Master.View.AddSpinEditColumn("ITEMVALUE05TO", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric);//
                        grdTab2Master.View.AddTextBoxColumn("ITEMVALUE05", 100).SetIsHidden();
                        grdTab2Master.View.AddTextBoxColumn("ITEMVALUE05NAME", 100).SetIsHidden();
                    }
                    else
                    {
                        grdTab2Master.View.AddTextBoxColumn("ITEMVALUE05FR", 80).SetIsHidden();
                        grdTab2Master.View.AddTextBoxColumn("ITEMVALUE05TO", 80).SetIsHidden();
                        if (row["COMPONENTTYPE05"].ToString().Equals("PopUp"))
                        {
                            //수정 예정

                            InitializeGrid_Tab2CommonPopup(row["DATASETTYPE05"].ToString(), row["DATASET05"].ToString(), "ITEMVALUE05", "ITEMVALUE05NAME");
                            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE05NAME", 100).SetIsReadOnly();
                        }
                        else if (row["COMPONENTTYPE05"].ToString().Equals("ComboBox"))
                        {
                            if (row["DATASETTYPE05"].ToString().Equals("CodeClass"))
                            {
                                string param = "CODECLASSID=" + row["DATASET05"].ToString();
                                grdTab2Master.View.AddComboBoxColumn("ITEMVALUE05", 100, new SqlQuery("GetCodeList", "00001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                   .SetEmptyItem("*", "*");  //
                            }
                            else
                            {
                                string param = row["DATASET05"].ToString();
                                grdTab2Master.View.AddComboBoxColumn("ITEMVALUE05", 100, new SqlQuery("GetCodeList", "10001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                   .SetEmptyItem("*", "*");  //
                            }
                            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE05NAME", 100).SetIsHidden();
                        }
                        else
                        {
                            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE05", 100);
                            grdTab2Master.View.AddTextBoxColumn("ITEMVALUE05NAME", 100).SetIsHidden();
                        }
                    }
                }
                else
                {
                    grdTab2Master.View.AddTextBoxColumn("ITEMVALUE05", 200).SetIsHidden();
                    grdTab2Master.View.AddTextBoxColumn("ITEMVALUE05FR", 200).SetIsHidden();
                    grdTab2Master.View.AddTextBoxColumn("ITEMVALUE05TO", 200).SetIsHidden();
                }

                #endregion PRICEITEMID05
            }

            this.InitializeGrid_Tab2VendorPopup();//
            grdTab2Master.View.AddTextBoxColumn("OSPVENDORNAME", 200).SetIsReadOnly();
            grdTab2Master.View.AddComboBoxColumn("PRICEUNIT", 100, new SqlQuery("GetUomDefinitionMapListByOsp", "10001", "UOMCATEGORY=OSP", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "UOMDEFID", "UOMDEFID")
                .SetValidationIsRequired()
                .SetDefault("");

            grdTab2Master.View.AddComboBoxColumn("VALIDSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetValidationIsRequired()
                .SetDefault("Valid")
                .SetIsReadOnly();
        }

        private void InitializeGrid_GrdTab1MasterDisplay(string IsProduct)
        {
            grdTab1Master.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdTab1Master.View.AddTextBoxColumn("ENTERPRISEID", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("PLANTID", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("PRICECOMBINATIONID", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("PRICECOMBINATIONNAME", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("OSPPRICECODE", 100).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("OSPPRODUCTIONTYPE", 100).SetIsReadOnly();

            //grdTab1Master.View.AddComboBoxColumn("OSPPRODUCTIONTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OSPProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            //    .SetLabel("PRODUCTIONTYPE")
            //    .SetEmptyItem("*", "*");  //

            grdTab1Master.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsHidden();

            grdTab1Master.View.AddTextBoxColumn("PRICEITEMID01", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("PRICEITEMID02", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("PRICEITEMID03", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("PRICEITEMID04", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("PRICEITEMID05", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("ISRANGE01", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("ISRANGE02", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("ISRANGE03", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("ISRANGE04", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("ISRANGE05", 200).SetIsHidden();
            DataRow row = grdDetail.View.GetFocusedDataRow();
            if (row != null)
            {
                #region PRICEITEMID01

                if (!(row["PRICEITEMID01"].ToString().Equals("")))
                {
                    if (row["ISRANGE01"].ToString().Equals("Y"))  //구간여부
                    {
                        string strFormatmask = row["FORMATMASK01"].ToString();
                        if (strFormatmask.Equals(""))
                        {
                            strFormatmask = "#,###";
                        }
                        grdTab1Master.View.AddSpinEditColumn("ITEMVALUE01FR", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric);//
                        grdTab1Master.View.AddSpinEditColumn("ITEMVALUE01TO", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric);//
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE01", 100).SetIsHidden();
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE01NAME", 100).SetIsHidden();
                    }
                    else
                    {
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE01FR", 80).SetIsHidden();
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE01TO", 80).SetIsHidden();
                        if (row["COMPONENTTYPE01"].ToString().Equals("PopUp"))
                        {
                            // InitializeGrid_Tab1CommonPopup(row["DATASETTYPE01"].ToString(), row["DATASET01"].ToString(), "ITEMVALUE01", "ITEMVALUE01NAME");
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE01", 80).SetIsReadOnly();
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE01NAME", 100).SetIsReadOnly();
                        }
                        else if (row["COMPONENTTYPE01"].ToString().Equals("ComboBox"))
                        {
                            if (row["DATASETTYPE01"].ToString().Equals("CodeClass"))
                            {
                                //string param = "CODECLASSID=" + row["DATASET01"].ToString();
                                //grdTab1Master.View.AddComboBoxColumn("ITEMVALUE01", 100, new SqlQuery("GetCodeList", "00001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                //   .SetEmptyItem("*", "*");  //
                                grdTab1Master.View.AddTextBoxColumn("ITEMVALUE01", 80).SetIsReadOnly();
                            }
                            else
                            {
                                //string param = row["DATASET01"].ToString();
                                //grdTab1Master.View.AddComboBoxColumn("ITEMVALUE01", 100, new SqlQuery("GetCodeList", "10001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                //   .SetEmptyItem("*", "*");  //
                                grdTab1Master.View.AddTextBoxColumn("ITEMVALUE01", 80).SetIsReadOnly();
                            }
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE01NAME", 100).SetIsHidden();
                        }
                        else
                        {
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE01", 100);
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE01NAME", 100).SetIsHidden();
                        }
                    }
                }
                else
                {
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE01", 200).SetIsHidden();
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE01FR", 200).SetIsHidden();
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE01TO", 200).SetIsHidden();
                }

                #endregion PRICEITEMID01

                #region PRICEITEMID02

                if (!(row["PRICEITEMID02"].ToString().Equals("")))
                {
                    if (row["ISRANGE02"].ToString().Equals("Y"))  //구간여부
                    {
                        string strFormatmask = row["FORMATMASK02"].ToString();
                        if (strFormatmask.Equals(""))
                        {
                            strFormatmask = "#,###";
                        }
                        grdTab1Master.View.AddSpinEditColumn("ITEMVALUE02FR", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric);//
                        grdTab1Master.View.AddSpinEditColumn("ITEMVALUE02TO", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric);//
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE02", 100).SetIsHidden();
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE02NAME", 100).SetIsHidden();
                    }
                    else
                    {
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE02FR", 80).SetIsHidden();
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE02TO", 80).SetIsHidden();
                        if (row["COMPONENTTYPE02"].ToString().Equals("PopUp"))
                        {
                            // InitializeGrid_Tab1CommonPopup(row["DATASETTYPE02"].ToString(), row["DATASET02"].ToString(), "ITEMVALUE02", "ITEMVALUE02NAME");
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE02", 80).SetIsReadOnly();
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE02NAME", 100).SetIsReadOnly();
                        }
                        else if (row["COMPONENTTYPE02"].ToString().Equals("ComboBox"))
                        {
                            if (row["DATASETTYPE02"].ToString().Equals("CodeClass"))
                            {
                                grdTab1Master.View.AddTextBoxColumn("ITEMVALUE02", 80).SetIsReadOnly();
                            }
                            else
                            {
                                grdTab1Master.View.AddTextBoxColumn("ITEMVALUE02", 80).SetIsReadOnly();
                            }
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE02NAME", 100).SetIsHidden();
                        }
                        else
                        {
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE02", 100);
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE02NAME", 100).SetIsHidden();
                        }
                    }
                }
                else
                {
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE02", 200).SetIsHidden();
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE02FR", 200).SetIsHidden();
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE02TO", 200).SetIsHidden();
                }

                #endregion PRICEITEMID02

                #region PRICEITEMID03

                if (!(row["PRICEITEMID03"].ToString().Equals("")))
                {
                    if (row["ISRANGE03"].ToString().Equals("Y"))  //구간여부
                    {
                        string strFormatmask = row["FORMATMASK03"].ToString();
                        if (strFormatmask.Equals(""))
                        {
                            strFormatmask = "#,###";
                        }
                        grdTab1Master.View.AddSpinEditColumn("ITEMVALUE03FR", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric);//
                        grdTab1Master.View.AddSpinEditColumn("ITEMVALUE03TO", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric);//
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE03", 100).SetIsHidden();
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE03NAME", 100).SetIsHidden();
                    }
                    else
                    {
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE03FR", 80).SetIsHidden();
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE03TO", 80).SetIsHidden();
                        if (row["COMPONENTTYPE03"].ToString().Equals("PopUp"))
                        {
                            // InitializeGrid_Tab1CommonPopup(row["DATASETTYPE03"].ToString(), row["DATASET03"].ToString(), "ITEMVALUE03", "ITEMVALUE03NAME");
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE03", 80).SetIsReadOnly();
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE03NAME", 100).SetIsReadOnly();
                        }
                        else if (row["COMPONENTTYPE03"].ToString().Equals("ComboBox"))
                        {
                            if (row["DATASETTYPE03"].ToString().Equals("CodeClass"))
                            {
                                //string param = "CODECLASSID=" + row["DATASET03"].ToString();
                                //grdTab1Master.View.AddComboBoxColumn("ITEMVALUE03", 100, new SqlQuery("GetCodeList", "00001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                //   .SetEmptyItem("*", "*");  //

                                grdTab1Master.View.AddTextBoxColumn("ITEMVALUE03", 80).SetIsReadOnly();
                            }
                            else
                            {
                                string param = row["DATASET03"].ToString();
                                grdTab1Master.View.AddComboBoxColumn("ITEMVALUE03", 100, new SqlQuery("GetCodeList", "10001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                   .SetEmptyItem("*", "*");  //
                            }
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE03NAME", 100).SetIsHidden();
                        }
                        else
                        {
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE03", 100);
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE03NAME", 100).SetIsHidden();
                        }
                    }
                }
                else
                {
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE03", 200).SetIsHidden();
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE03FR", 200).SetIsHidden();
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE03TO", 200).SetIsHidden();
                }

                #endregion PRICEITEMID03

                #region PRICEITEMID04

                if (!(row["PRICEITEMID04"].ToString().Equals("")))
                {
                    if (row["ISRANGE04"].ToString().Equals("Y"))  //구간여부
                    {
                        string strFormatmask = row["FORMATMASK04"].ToString();
                        if (strFormatmask.Equals(""))
                        {
                            strFormatmask = "#,###";
                        }
                        grdTab1Master.View.AddSpinEditColumn("ITEMVALUE04FR", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric);//
                        grdTab1Master.View.AddSpinEditColumn("ITEMVALUE04TO", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric);//
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE04", 100).SetIsHidden();
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE04NAME", 100).SetIsHidden();
                    }
                    else
                    {
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE04FR", 80).SetIsHidden();
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE04TO", 80).SetIsHidden();
                        if (row["COMPONENTTYPE04"].ToString().Equals("PopUp"))
                        {
                            InitializeGrid_Tab1CommonPopup(row["DATASETTYPE04"].ToString(), row["DATASET04"].ToString(), "ITEMVALUE04", "ITEMVALUE04NAME");
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE04NAME", 100).SetIsReadOnly();
                        }
                        else if (row["COMPONENTTYPE04"].ToString().Equals("ComboBox"))
                        {
                            if (row["DATASETTYPE04"].ToString().Equals("CodeClass"))
                            {
                                string param = "CODECLASSID=" + row["DATASET04"].ToString();
                                grdTab1Master.View.AddComboBoxColumn("ITEMVALUE04", 100, new SqlQuery("GetCodeList", "00001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                   .SetEmptyItem("*", "*");  //
                            }
                            else
                            {
                                string param = row["DATASET04"].ToString();
                                grdTab1Master.View.AddComboBoxColumn("ITEMVALUE04", 100, new SqlQuery("GetCodeList", "10001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                   .SetEmptyItem("*", "*");  //
                            }
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE04NAME", 100).SetIsHidden();
                        }
                        else
                        {
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE04", 100);
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE04NAME", 100).SetIsHidden();
                        }
                    }
                }
                else
                {
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE04", 200).SetIsHidden();
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE04FR", 200).SetIsHidden();
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE04TO", 200).SetIsHidden();
                }

                #endregion PRICEITEMID04

                #region PRICEITEMID05

                if (!(row["PRICEITEMID05"].ToString().Equals("")))
                {
                    if (row["ISRANGE05"].ToString().Equals("Y"))  //구간여부
                    {
                        string strFormatmask = row["FORMATMASK05"].ToString();
                        if (strFormatmask.Equals(""))
                        {
                            strFormatmask = "#,###";
                        }
                        grdTab1Master.View.AddSpinEditColumn("ITEMVALUE05FR", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric);//
                        grdTab1Master.View.AddSpinEditColumn("ITEMVALUE05TO", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric);//
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE05", 100).SetIsHidden();
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE05NAME", 100).SetIsHidden();
                    }
                    else
                    {
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE05FR", 80).SetIsHidden();
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE05TO", 80).SetIsHidden();
                        if (row["COMPONENTTYPE05"].ToString().Equals("PopUp"))
                        {
                            //수정 예정

                            InitializeGrid_Tab1CommonPopup(row["DATASETTYPE05"].ToString(), row["DATASET05"].ToString(), "ITEMVALUE05", "ITEMVALUE05NAME");
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE05NAME", 100).SetIsReadOnly();
                        }
                        else if (row["COMPONENTTYPE05"].ToString().Equals("ComboBox"))
                        {
                            if (row["DATASETTYPE05"].ToString().Equals("CodeClass"))
                            {
                                string param = "CODECLASSID=" + row["DATASET05"].ToString();
                                grdTab1Master.View.AddComboBoxColumn("ITEMVALUE05", 100, new SqlQuery("GetCodeList", "00001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                   .SetEmptyItem("*", "*");  //
                            }
                            else
                            {
                                string param = row["DATASET05"].ToString();
                                grdTab1Master.View.AddComboBoxColumn("ITEMVALUE05", 100, new SqlQuery("GetCodeList", "10001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                   .SetEmptyItem("*", "*");  //
                            }
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE05NAME", 100).SetIsHidden();
                        }
                        else
                        {
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE05", 100);
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE05NAME", 100).SetIsHidden();
                        }
                    }
                }
                else
                {
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE05", 200).SetIsHidden();
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE05FR", 200).SetIsHidden();
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE05TO", 200).SetIsHidden();
                }

                #endregion PRICEITEMID05
            }

            this.InitializeGrid_Tab1VendorPopup();//
            grdTab1Master.View.AddTextBoxColumn("OSPVENDORNAME", 200).SetIsReadOnly();
            grdTab1Master.View.AddComboBoxColumn("PRICEUNIT", 100, new SqlQuery("GetUomDefinitionMapListByOsp", "10001", "UOMCATEGORY=OSP", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "UOMDEFID", "UOMDEFID")
                .SetValidationIsRequired()
                .SetDefault("");

            grdTab1Master.View.AddComboBoxColumn("VALIDSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetValidationIsRequired()
                .SetDefault("Valid")
                .SetIsReadOnly();
        }

        /// <summary>
        /// InitializeGrid_productdePopup
        /// </summary>
        /// <param name="TargetView"></param>
        private void InitializeGrid_productdePopup()
        {
            var popupGridProcessSegments = grdTab2Master.View.AddSelectPopupColumn("PRODUCTDEFID", 120,
               new SqlQuery("GetProductdefidPoplistByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"))
               // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
               .SetPopupLayout("PRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
               // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
               .SetPopupResultCount(1)
               .SetRelationIds("p_plantid")
               // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
               .SetPopupResultMapping("PRODUCTDEFID", "PRODUCTDEFID")
               // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
               .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
               // 그리드의 남은 영역을 채울 컬럼 설정

               .SetPopupAutoFillColumns("PRODUCTDEFID")
               .SetPopupApplySelection((selectedRows, dataGridRow) =>
               {
                   DataRow classRow = grdTab2Master.View.GetFocusedDataRow();

                   foreach (DataRow row in selectedRows)
                   {
                       classRow["PRODUCTDEFVERSION"] = row["PRODUCTDEFVERSION"];
                       classRow["PRODUCTDEFNAME"] = row["PRODUCTDEFNAME"];
                   }
               })
           ;
            popupGridProcessSegments.Conditions.AddComboBox("PRODUCTDEFTYPE", new SqlQuery("GetCodeListByOspProductdeftype", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("PRODUCTDEFTYPE")
                .SetDefault("Product");
            popupGridProcessSegments.Conditions.AddTextBox("PRODUCTDEFNAME");
            // 팝업 그리드 설정
            popupGridProcessSegments.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 150);
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
        }

        /// <summary>
        /// InitializeGrid_CommonPopup
        /// </summary>
        /// <param name="TargetView"></param>
        private void InitializeGrid_Tab2CommonPopup(string strDataSettype, string strDataset, string targetid, string targetname)
        {
            //GetCodeListPopupByosp
            string SqlQueryid = "";
            string param = "CODECLASSID=" + strDataset;
            if (strDataSettype.Equals("CodeClass"))
            {
                SqlQueryid = "GetCodeListPopupByOsp";
                param = "CODECLASSID=" + strDataset;
            }
            else
            {
                SqlQueryid = strDataSettype;
                param = "CODECLASSID=" + strDataset;
            }
            var popupGridProcessSegments = grdTab2Master.View.AddSelectPopupColumn(targetid, 120,
                new SqlQuery(SqlQueryid, "10001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("OSPCOMMONPOPUP", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupResultMapping(targetid, "CODEID")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    DataRow classRow = grdTab2Master.View.GetFocusedDataRow();

                    foreach (DataRow row in selectedRows)
                    {
                        classRow[targetid] = row[targetid];
                        classRow[targetname] = row[targetname];
                    }
                })
            ;

            popupGridProcessSegments.Conditions.AddTextBox("P_CODENAME");
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("CODEID", 100);
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("CODENAME", 200);
        }

        private void InitializeGrid_Tab1CommonPopup(string strDataSettype, string strDataset, string targetid, string targetname)
        {
            //GetCodeListPopupByosp
            string SqlQueryid = "";
            string param = "CODECLASSID=" + strDataset;
            if (strDataSettype.Equals("CodeClass"))
            {
                SqlQueryid = "GetCodeListPopupByOsp";
                param = "CODECLASSID=" + strDataset;
            }
            else
            {
                SqlQueryid = strDataSettype;
                param = "CODECLASSID=" + strDataset;
            }
            var popupGridProcessSegments = grdTab1Master.View.AddSelectPopupColumn(targetid, 120,
                new SqlQuery(SqlQueryid, "10001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("OSPCOMMONPOPUP", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupResultMapping(targetid, "CODEID")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    DataRow classRow = grdTab1Master.View.GetFocusedDataRow();

                    foreach (DataRow row in selectedRows)
                    {
                        classRow[targetid] = row["CODEID"];
                        classRow[targetname] = row["CODENAME"];
                    }
                })
            ;

            popupGridProcessSegments.Conditions.AddTextBox("P_CODENAME")
                .SetLabel("CODENAME");
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("CODEID", 100);
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("CODENAME", 200);
        }

        /// <summary>
        /// InitializeGrid_VendorPopup
        /// </summary>
        private void InitializeGrid_Tab2VendorPopup()
        {
            var popupGridProcessSegments = grdTab2Master.View.AddSelectPopupColumn("OSPVENDORID",
                new SqlQuery("GetVendorListByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("OSPVENDORID", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                .SetRelationIds("PLANTID")
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("OSPVENDORID", "OSPVENDORID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정

                .SetPopupAutoFillColumns("OSPVENDORNAME")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    DataRow classRow = grdTab2Master.View.GetFocusedDataRow();

                    foreach (DataRow row in selectedRows)
                    {
                        classRow["OSPVENDORNAME"] = row["OSPVENDORNAME"];
                    }
                })
            ;
            // 팝업에서 사용할 조회조건 항목 추가
            popupGridProcessSegments.Conditions.AddTextBox("OSPVENDORNAME");
            popupGridProcessSegments.Conditions.AddTextBox("PLANTID")
               .SetPopupDefaultByGridColumnId("PLANTID")
                .SetLabel("")
               .SetIsHidden();
            // 팝업 그리드 설정
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("OSPVENDORID", 150)
                .SetValidationKeyColumn();
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("OSPVENDORNAME", 200);
        }

        private void InitializeGrid_Tab1VendorPopup()
        {
            var popupGridProcessSegments = grdTab1Master.View.AddSelectPopupColumn("OSPVENDORID",
                new SqlQuery("GetVendorListByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("OSPVENDORID", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                .SetRelationIds("PLANTID")
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("OSPVENDORID", "OSPVENDORID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정

                .SetPopupAutoFillColumns("OSPVENDORNAME")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    DataRow classRow = grdTab1Master.View.GetFocusedDataRow();

                    foreach (DataRow row in selectedRows)
                    {
                        classRow["OSPVENDORNAME"] = row["OSPVENDORNAME"];
                    }
                })
            ;
            // 팝업에서 사용할 조회조건 항목 추가
            popupGridProcessSegments.Conditions.AddTextBox("OSPVENDORNAME");
            popupGridProcessSegments.Conditions.AddTextBox("PLANTID")
               .SetPopupDefaultByGridColumnId("PLANTID")
                .SetLabel("")
               .SetIsHidden();
            // 팝업 그리드 설정
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("OSPVENDORID", 150)
                .SetValidationKeyColumn();
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("OSPVENDORNAME", 200);
        }

        private void GrdTab1MasterDisplayCaptionHidden()
        {
            //DataRow row = grdDetail.View.GetFocusedDataRow();

            //if (row == null)
            //{
            //    return;
            //}
            grdTab1Master.View.Columns["OSPPRODUCTIONTYPE"].OwnerBand.Visible = false;
            grdTab1Master.View.Columns["OSPPRODUCTIONTYPE"].Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE01FR"].OwnerBand.Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE01TO"].OwnerBand.Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE01"].OwnerBand.Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE01NAME"].OwnerBand.Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE01FR"].Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE01TO"].Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE01"].Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE01NAME"].Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE02FR"].OwnerBand.Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE02TO"].OwnerBand.Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE02"].OwnerBand.Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE02NAME"].OwnerBand.Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE02FR"].Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE02TO"].Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE02"].Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE02NAME"].Visible = false;

            grdTab1Master.View.Columns["ITEMVALUE03FR"].OwnerBand.Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE03TO"].OwnerBand.Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE03"].OwnerBand.Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE03NAME"].OwnerBand.Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE03FR"].Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE03TO"].Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE03"].Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE03NAME"].Visible = false;

            grdTab1Master.View.Columns["ITEMVALUE04FR"].OwnerBand.Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE04TO"].OwnerBand.Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE04"].OwnerBand.Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE04NAME"].OwnerBand.Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE04FR"].Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE04TO"].Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE04"].Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE04NAME"].Visible = false;

            grdTab1Master.View.Columns["ITEMVALUE05FR"].OwnerBand.Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE05TO"].OwnerBand.Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE05"].OwnerBand.Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE05NAME"].OwnerBand.Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE05FR"].Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE05TO"].Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE05"].Visible = false;
            grdTab1Master.View.Columns["ITEMVALUE05NAME"].Visible = false;

            grdTab1Master.View.Columns["OSPVENDORID"].OwnerBand.Visible = false;
            grdTab1Master.View.Columns["OSPVENDORNAME"].OwnerBand.Visible = false;
            grdTab1Master.View.Columns["PRICEUNIT"].OwnerBand.Visible = false;
            grdTab1Master.View.Columns["VALIDSTATE"].OwnerBand.Visible = false;
            grdTab1Master.View.Columns["OSPVENDORID"].Visible = false;
            grdTab1Master.View.Columns["OSPVENDORNAME"].Visible = false;
            grdTab1Master.View.Columns["PRICEUNIT"].Visible = false;
            grdTab1Master.View.Columns["VALIDSTATE"].Visible = false;
        }

        private void GrdTab2MasterDisplayCaptionHidden()
        {
            //DataRow row = grdDetail.View.GetFocusedDataRow();

            //if (row == null)
            //{
            //    return;
            //}
            if (grdTab2Master.View.Columns["OSPPRODUCTIONTYPE"] != null)
            {
                grdTab2Master.View.Columns["OSPPRODUCTIONTYPE"].OwnerBand.Visible = false;
                grdTab2Master.View.Columns["OSPPRODUCTIONTYPE"].Visible = false;
            }
            if (grdTab2Master.View.Columns["ITEMVALUE01"] != null)
            {
                grdTab2Master.View.Columns["ITEMVALUE01"].OwnerBand.Visible = false;
                grdTab2Master.View.Columns["ITEMVALUE01"].Visible = false;
            }

            if (grdTab2Master.View.Columns["ITEMVALUE01FR"] != null)
            {
                grdTab2Master.View.Columns["ITEMVALUE01FR"].OwnerBand.Visible = false;
                grdTab2Master.View.Columns["ITEMVALUE01FR"].Visible = false;
            }
            if (grdTab2Master.View.Columns["ITEMVALUE01NAME"] != null)
            {
                grdTab2Master.View.Columns["ITEMVALUE01NAME"].OwnerBand.Visible = false;
                grdTab2Master.View.Columns["ITEMVALUE01NAME"].Visible = false;
            }
            if (grdTab2Master.View.Columns["ITEMVALUE01TO"] != null)
            {
                grdTab2Master.View.Columns["ITEMVALUE01TO"].OwnerBand.Visible = false;
                grdTab2Master.View.Columns["ITEMVALUE01TO"].Visible = false;
            }
            if (grdTab2Master.View.Columns["ITEMVALUE02"] != null)
            {
                grdTab2Master.View.Columns["ITEMVALUE02"].OwnerBand.Visible = false;
                grdTab2Master.View.Columns["ITEMVALUE02"].Visible = false;
            }
            if (grdTab2Master.View.Columns["ITEMVALUE02FR"] != null)
            {
                grdTab2Master.View.Columns["ITEMVALUE02FR"].OwnerBand.Visible = false;
                grdTab2Master.View.Columns["ITEMVALUE02FR"].Visible = false;
            }
            if (grdTab2Master.View.Columns["ITEMVALUE02NAME"] != null)
            {
                grdTab2Master.View.Columns["ITEMVALUE02NAME"].OwnerBand.Visible = false;
                grdTab2Master.View.Columns["ITEMVALUE02NAME"].Visible = false;
            }
            if (grdTab2Master.View.Columns["OSPPRODUCTIONTYPE"] != null)
            {
                grdTab2Master.View.Columns["ITEMVALUE02TO"].OwnerBand.Visible = false;
                grdTab2Master.View.Columns["ITEMVALUE02TO"].Visible = false;
            }
            if (grdTab2Master.View.Columns["ITEMVALUE03"] != null)
            {
                grdTab2Master.View.Columns["ITEMVALUE03"].OwnerBand.Visible = false;
                grdTab2Master.View.Columns["ITEMVALUE03"].Visible = false;
            }
            if (grdTab2Master.View.Columns["ITEMVALUE03FR"] != null)
            {
                grdTab2Master.View.Columns["ITEMVALUE03FR"].OwnerBand.Visible = false;
                grdTab2Master.View.Columns["ITEMVALUE03FR"].Visible = false;
            }
            if (grdTab2Master.View.Columns["ITEMVALUE03NAME"] != null)
            {
                grdTab2Master.View.Columns["ITEMVALUE03NAME"].OwnerBand.Visible = false;
                grdTab2Master.View.Columns["ITEMVALUE03NAME"].Visible = false;
            }
            if (grdTab2Master.View.Columns["ITEMVALUE03TO"] != null)
            {
                grdTab2Master.View.Columns["ITEMVALUE03TO"].OwnerBand.Visible = false;
                grdTab2Master.View.Columns["ITEMVALUE03TO"].Visible = false;
            }
            if (grdTab2Master.View.Columns["ITEMVALUE04"] != null)
            {
                grdTab2Master.View.Columns["ITEMVALUE04"].OwnerBand.Visible = false;
                grdTab2Master.View.Columns["ITEMVALUE04"].Visible = false;
            }
            if (grdTab2Master.View.Columns["ITEMVALUE04FR"] != null)
            {
                grdTab2Master.View.Columns["ITEMVALUE04FR"].OwnerBand.Visible = false;
                grdTab2Master.View.Columns["ITEMVALUE04FR"].Visible = false;
            }
            if (grdTab2Master.View.Columns["ITEMVALUE04NAME"] != null)
            {
                grdTab2Master.View.Columns["ITEMVALUE04NAME"].OwnerBand.Visible = false;
                grdTab2Master.View.Columns["ITEMVALUE04NAME"].Visible = false;
            }
            if (grdTab2Master.View.Columns["ITEMVALUE04TO"] != null)
            {
                grdTab2Master.View.Columns["ITEMVALUE04TO"].OwnerBand.Visible = false;
                grdTab2Master.View.Columns["ITEMVALUE04TO"].Visible = false;
            }
            if (grdTab2Master.View.Columns["ITEMVALUE05"] != null)
            {
                grdTab2Master.View.Columns["ITEMVALUE05"].OwnerBand.Visible = false;
                grdTab2Master.View.Columns["ITEMVALUE05"].Visible = false;
            }
            if (grdTab2Master.View.Columns["ITEMVALUE05FR"] != null)
            {
                grdTab2Master.View.Columns["ITEMVALUE05FR"].OwnerBand.Visible = false;
                grdTab2Master.View.Columns["ITEMVALUE05FR"].Visible = false;
            }
            if (grdTab2Master.View.Columns["ITEMVALUE05NAME"] != null)
            {
                grdTab2Master.View.Columns["ITEMVALUE05NAME"].OwnerBand.Visible = false;
                grdTab2Master.View.Columns["ITEMVALUE05NAME"].Visible = false;
            }
            if (grdTab2Master.View.Columns["ITEMVALUE05TO"] != null)
            {
                grdTab2Master.View.Columns["ITEMVALUE05TO"].OwnerBand.Visible = false;
                grdTab2Master.View.Columns["ITEMVALUE05TO"].Visible = false;
            }
            if (grdTab2Master.View.Columns["OSPVENDORID"] != null)
            {
                grdTab2Master.View.Columns["OSPVENDORID"].OwnerBand.Visible = false;
                grdTab2Master.View.Columns["OSPVENDORID"].Visible = false;
            }
            if (grdTab2Master.View.Columns["OSPVENDORNAME"] != null)
            {
                grdTab2Master.View.Columns["OSPVENDORNAME"].OwnerBand.Visible = false;
                grdTab2Master.View.Columns["OSPVENDORNAME"].Visible = false;
            }
            if (grdTab2Master.View.Columns["PRICEUNIT"] != null)
            {
                grdTab2Master.View.Columns["PRICEUNIT"].OwnerBand.Visible = false;
                grdTab2Master.View.Columns["PRICEUNIT"].Visible = false;
            }
            if (grdTab2Master.View.Columns["VALIDSTATE"] != null)
            {
                grdTab2Master.View.Columns["VALIDSTATE"].OwnerBand.Visible = false;
                grdTab2Master.View.Columns["VALIDSTATE"].Visible = false;
            }
        }

        private void InitializeGrid_GrdTab1MasterDisplayCaption()
        {
            DataRow row = grdDetail.View.GetFocusedDataRow();

            if (row == null)
            {
                return;
            }
            grdTab1Master.View.Columns["OSPPRODUCTIONTYPE"].OwnerBand.Visible = true;
            grdTab1Master.View.Columns["OSPPRODUCTIONTYPE"].Visible = true;

            if (row != null)
            {
                #region PRICEITEMID01

                if (!(row["PRICEITEMID01"].ToString().Equals("")))
                {
                    if (row["ISRANGE01"].ToString().Equals("Y"))  //구간여부
                    {
                        grdTab1Master.View.Columns["ITEMVALUE01FR"].OwnerBand.Visible = true;
                        grdTab1Master.View.Columns["ITEMVALUE01TO"].OwnerBand.Visible = true;

                        grdTab1Master.View.Columns["ITEMVALUE01FR"].Visible = true;
                        grdTab1Master.View.Columns["ITEMVALUE01TO"].Visible = true;

                        grdTab1Master.View.Columns["ITEMVALUE01FR"].Caption = row["PRICEITEMNAME01"].ToString() + "Fr";
                        grdTab1Master.View.Columns["ITEMVALUE01FR"].ToolTip = row["PRICEITEMNAME01"].ToString() + "Fr";
                        grdTab1Master.View.Columns["ITEMVALUE01TO"].Caption = row["PRICEITEMNAME01"].ToString() + "To";
                        grdTab1Master.View.Columns["ITEMVALUE01TO"].ToolTip = row["PRICEITEMNAME01"].ToString() + "To";
                    }
                    else
                    {
                        if (row["COMPONENTTYPE01"].ToString().Equals("PopUp"))
                        {
                            grdTab1Master.View.Columns["ITEMVALUE01"].OwnerBand.Visible = true;
                            grdTab1Master.View.Columns["ITEMVALUE01NAME"].OwnerBand.Visible = true;

                            grdTab1Master.View.Columns["ITEMVALUE01"].Visible = true;
                            grdTab1Master.View.Columns["ITEMVALUE01NAME"].Visible = true;
                            grdTab1Master.View.Columns["ITEMVALUE01"].Caption = row["PRICEITEMNAME01"].ToString() + "Id";
                            grdTab1Master.View.Columns["ITEMVALUE01"].ToolTip = row["PRICEITEMNAME01"].ToString() + "Id";
                            grdTab1Master.View.Columns["ITEMVALUE01NAME"].Caption = row["PRICEITEMNAME01"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE01NAME"].ToolTip = row["PRICEITEMNAME01"].ToString();
                        }
                        else if (row["COMPONENTTYPE01"].ToString().Equals("ComboBox"))
                        {
                            grdTab1Master.View.Columns["ITEMVALUE01NAME"].OwnerBand.Visible = true;
                            grdTab1Master.View.Columns["ITEMVALUE01NAME"].Visible = true;
                            grdTab1Master.View.Columns["ITEMVALUE01NAME"].ToolTip = row["PRICEITEMNAME01"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE01NAME"].Caption = row["PRICEITEMNAME01"].ToString();
                        }
                        else
                        {
                            grdTab1Master.View.Columns["ITEMVALUE01"].OwnerBand.Visible = true;
                            grdTab1Master.View.Columns["ITEMVALUE01"].Visible = true;
                            grdTab1Master.View.Columns["ITEMVALUE01"].ToolTip = row["PRICEITEMNAME01"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE01"].Caption = row["PRICEITEMNAME01"].ToString();
                        }
                    }
                }

                #endregion PRICEITEMID01

                #region PRICEITEMID02

                if (!(row["PRICEITEMID02"].ToString().Equals("")))
                {
                    if (row["ISRANGE02"].ToString().Equals("Y"))  //구간여부
                    {
                        grdTab1Master.View.Columns["ITEMVALUE02FR"].OwnerBand.Visible = true;
                        grdTab1Master.View.Columns["ITEMVALUE02TO"].OwnerBand.Visible = true;

                        grdTab1Master.View.Columns["ITEMVALUE02FR"].Visible = true;
                        grdTab1Master.View.Columns["ITEMVALUE02TO"].Visible = true;

                        grdTab1Master.View.Columns["ITEMVALUE02FR"].Caption = row["PRICEITEMNAME02"].ToString() + "Fr";
                        grdTab1Master.View.Columns["ITEMVALUE02FR"].ToolTip = row["PRICEITEMNAME02"].ToString() + "Fr";
                        grdTab1Master.View.Columns["ITEMVALUE02TO"].Caption = row["PRICEITEMNAME02"].ToString() + "To";
                        grdTab1Master.View.Columns["ITEMVALUE02TO"].ToolTip = row["PRICEITEMNAME02"].ToString() + "To";
                    }
                    else
                    {
                        if (row["COMPONENTTYPE02"].ToString().Equals("PopUp"))
                        {
                            grdTab1Master.View.Columns["ITEMVALUE02"].OwnerBand.Visible = true;
                            grdTab1Master.View.Columns["ITEMVALUE02NAME"].OwnerBand.Visible = true;

                            grdTab1Master.View.Columns["ITEMVALUE02"].Visible = true;
                            grdTab1Master.View.Columns["ITEMVALUE02NAME"].Visible = true;

                            grdTab1Master.View.Columns["ITEMVALUE02"].Caption = row["PRICEITEMNAME02"].ToString() + "Id";
                            grdTab1Master.View.Columns["ITEMVALUE02"].ToolTip = row["PRICEITEMNAME02"].ToString() + "Id";
                            grdTab1Master.View.Columns["ITEMVALUE02NAME"].Caption = row["PRICEITEMNAME02"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE02NAME"].ToolTip = row["PRICEITEMNAME02"].ToString();
                        }
                        else if (row["COMPONENTTYPE02"].ToString().Equals("ComboBox"))
                        {
                            grdTab1Master.View.Columns["ITEMVALUE02NAME"].OwnerBand.Visible = true;

                            grdTab1Master.View.Columns["ITEMVALUE02NAME"].Visible = true;
                            grdTab1Master.View.Columns["ITEMVALUE02NAME"].ToolTip = row["PRICEITEMNAME02"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE02NAME"].Caption = row["PRICEITEMNAME02"].ToString();
                        }
                        else
                        {
                            grdTab1Master.View.Columns["ITEMVALUE02"].OwnerBand.Visible = true;

                            grdTab1Master.View.Columns["ITEMVALUE02"].Visible = true;

                            grdTab1Master.View.Columns["ITEMVALUE02"].ToolTip = row["PRICEITEMNAME02"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE02"].Caption = row["PRICEITEMNAME02"].ToString();
                        }
                    }
                }

                #endregion PRICEITEMID02

                #region PRICEITEMID03

                if (!(row["PRICEITEMID03"].ToString().Equals("")))
                {
                    if (row["ISRANGE03"].ToString().Equals("Y"))  //구간여부
                    {
                        grdTab1Master.View.Columns["ITEMVALUE03FR"].OwnerBand.Visible = true;
                        grdTab1Master.View.Columns["ITEMVALUE03TO"].OwnerBand.Visible = true;

                        grdTab1Master.View.Columns["ITEMVALUE03FR"].Visible = true;
                        grdTab1Master.View.Columns["ITEMVALUE03TO"].Visible = true;

                        grdTab1Master.View.Columns["ITEMVALUE03FR"].Caption = row["PRICEITEMNAME03"].ToString() + "Fr";
                        grdTab1Master.View.Columns["ITEMVALUE03FR"].ToolTip = row["PRICEITEMNAME03"].ToString() + "Fr";
                        grdTab1Master.View.Columns["ITEMVALUE03TO"].Caption = row["PRICEITEMNAME03"].ToString() + "To";
                        grdTab1Master.View.Columns["ITEMVALUE03TO"].ToolTip = row["PRICEITEMNAME03"].ToString() + "To";
                    }
                    else
                    {
                        if (row["COMPONENTTYPE03"].ToString().Equals("PopUp"))
                        {
                            grdTab1Master.View.Columns["ITEMVALUE03"].OwnerBand.Visible = true;
                            grdTab1Master.View.Columns["ITEMVALUE03NAME"].OwnerBand.Visible = true;

                            grdTab1Master.View.Columns["ITEMVALUE03"].Visible = true;
                            grdTab1Master.View.Columns["ITEMVALUE03NAME"].Visible = true;
                            grdTab1Master.View.Columns["ITEMVALUE03"].Caption = row["PRICEITEMNAME03"].ToString() + "Id";
                            grdTab1Master.View.Columns["ITEMVALUE03"].ToolTip = row["PRICEITEMNAME03"].ToString() + "Id";
                            grdTab1Master.View.Columns["ITEMVALUE03NAME"].Caption = row["PRICEITEMNAME03"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE03NAME"].ToolTip = row["PRICEITEMNAME03"].ToString();
                        }
                        else if (row["COMPONENTTYPE03"].ToString().Equals("ComboBox"))
                        {
                            grdTab1Master.View.Columns["ITEMVALUE03NAME"].OwnerBand.Visible = true;

                            grdTab1Master.View.Columns["ITEMVALUE03NAME"].Visible = true;
                            grdTab1Master.View.Columns["ITEMVALUE03NAME"].Caption = row["PRICEITEMNAME03"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE03NAME"].ToolTip = row["PRICEITEMNAME03"].ToString();
                        }
                        else
                        {
                            grdTab1Master.View.Columns["ITEMVALUE03"].OwnerBand.Visible = true;

                            grdTab1Master.View.Columns["ITEMVALUE03"].Visible = true;

                            grdTab1Master.View.Columns["ITEMVALUE03"].Caption = row["PRICEITEMNAME03"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE03"].ToolTip = row["PRICEITEMNAME03"].ToString();
                        }
                    }
                }

                #endregion PRICEITEMID03

                #region PRICEITEMID04

                if (!(row["PRICEITEMID04"].ToString().Equals("")))
                {
                    if (row["ISRANGE04"].ToString().Equals("Y"))  //구간여부
                    {
                        grdTab1Master.View.Columns["ITEMVALUE04FR"].OwnerBand.Visible = true;
                        grdTab1Master.View.Columns["ITEMVALUE04TO"].OwnerBand.Visible = true;

                        grdTab1Master.View.Columns["ITEMVALUE04FR"].Visible = true;
                        grdTab1Master.View.Columns["ITEMVALUE04TO"].Visible = true;

                        grdTab1Master.View.Columns["ITEMVALUE04FR"].Caption = row["PRICEITEMNAME04"].ToString() + "Fr";
                        grdTab1Master.View.Columns["ITEMVALUE04FR"].ToolTip = row["PRICEITEMNAME04"].ToString() + "Fr";
                        grdTab1Master.View.Columns["ITEMVALUE04TO"].Caption = row["PRICEITEMNAME04"].ToString() + "To";
                        grdTab1Master.View.Columns["ITEMVALUE04TO"].ToolTip = row["PRICEITEMNAME04"].ToString() + "To";
                    }
                    else
                    {
                        if (row["COMPONENTTYPE04"].ToString().Equals("PopUp"))
                        {
                            ;
                            grdTab1Master.View.Columns["ITEMVALUE04"].OwnerBand.Visible = true;
                            grdTab1Master.View.Columns["ITEMVALUE04NAME"].OwnerBand.Visible = true;

                            grdTab1Master.View.Columns["ITEMVALUE04"].Visible = true;
                            grdTab1Master.View.Columns["ITEMVALUE04NAME"].Visible = true;
                            grdTab1Master.View.Columns["ITEMVALUE04"].Caption = row["PRICEITEMNAME04"].ToString() + "Id";
                            grdTab1Master.View.Columns["ITEMVALUE04"].ToolTip = row["PRICEITEMNAME04"].ToString() + "Id";
                            grdTab1Master.View.Columns["ITEMVALUE04NAME"].Caption = row["PRICEITEMNAME04"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE04NAME"].ToolTip = row["PRICEITEMNAME04"].ToString();
                        }
                        else if (row["COMPONENTTYPE04"].ToString().Equals("ComboBox"))
                        {
                            grdTab1Master.View.Columns["ITEMVALUE04NAME"].OwnerBand.Visible = true;

                            grdTab1Master.View.Columns["ITEMVALUE04NAME"].Visible = true;
                            grdTab1Master.View.Columns["ITEMVALUE04NAME"].Caption = row["PRICEITEMNAME04"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE04NAME"].ToolTip = row["PRICEITEMNAME04"].ToString();
                        }
                        else
                        {
                            grdTab1Master.View.Columns["ITEMVALUE04"].OwnerBand.Visible = true;

                            grdTab1Master.View.Columns["ITEMVALUE04"].Visible = true;

                            grdTab1Master.View.Columns["ITEMVALUE04"].Caption = row["PRICEITEMNAME04"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE04"].ToolTip = row["PRICEITEMNAME04"].ToString();
                        }
                    }
                }

                #endregion PRICEITEMID04

                #region PRICEITEMID05

                if (!(row["PRICEITEMID05"].ToString().Equals("")))
                {
                    if (row["ISRANGE05"].ToString().Equals("Y"))  //구간여부
                    {
                        grdTab1Master.View.Columns["ITEMVALUE05FR"].OwnerBand.Visible = true;
                        grdTab1Master.View.Columns["ITEMVALUE05TO"].OwnerBand.Visible = true;

                        grdTab1Master.View.Columns["ITEMVALUE05FR"].Visible = true;
                        grdTab1Master.View.Columns["ITEMVALUE05TO"].Visible = true;

                        grdTab1Master.View.Columns["ITEMVALUE05FR"].Caption = row["PRICEITEMNAME05"].ToString() + "Fr";
                        grdTab1Master.View.Columns["ITEMVALUE05FR"].ToolTip = row["PRICEITEMNAME05"].ToString() + "Fr";
                        grdTab1Master.View.Columns["ITEMVALUE05TO"].Caption = row["PRICEITEMNAME05"].ToString() + "To";
                        grdTab1Master.View.Columns["ITEMVALUE05TO"].ToolTip = row["PRICEITEMNAME05"].ToString() + "To";
                    }
                    else
                    {
                        if (row["COMPONENTTYPE05"].ToString().Equals("PopUp"))
                        {
                            grdTab1Master.View.Columns["ITEMVALUE05"].OwnerBand.Visible = true;
                            grdTab1Master.View.Columns["ITEMVALUE05NAME"].OwnerBand.Visible = true;

                            grdTab1Master.View.Columns["ITEMVALUE05"].Visible = true;
                            grdTab1Master.View.Columns["ITEMVALUE05NAME"].Visible = true;
                            grdTab1Master.View.Columns["ITEMVALUE05"].Caption = row["PRICEITEMNAME05"].ToString() + "Id";
                            grdTab1Master.View.Columns["ITEMVALUE05"].ToolTip = row["PRICEITEMNAME05"].ToString() + "Id";
                            grdTab1Master.View.Columns["ITEMVALUE05NAME"].Caption = row["PRICEITEMNAME05"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE05NAME"].ToolTip = row["PRICEITEMNAME05"].ToString();
                        }
                        else if (row["COMPONENTTYPE05"].ToString().Equals("ComboBox"))
                        {
                            grdTab1Master.View.Columns["ITEMVALUE05NAME"].OwnerBand.Visible = true;

                            grdTab1Master.View.Columns["ITEMVALUE05NAME"].Visible = true;
                            grdTab1Master.View.Columns["ITEMVALUE05NAME"].Caption = row["PRICEITEMNAME05"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE05NAME"].ToolTip = row["PRICEITEMNAME05"].ToString();
                        }
                        else
                        {
                            grdTab1Master.View.Columns["ITEMVALUE05"].OwnerBand.Visible = true;

                            grdTab1Master.View.Columns["ITEMVALUE05"].Visible = true;

                            grdTab1Master.View.Columns["ITEMVALUE05"].Caption = row["PRICEITEMNAME05"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE05"].ToolTip = row["PRICEITEMNAME05"].ToString();
                        }
                    }
                }

                #endregion PRICEITEMID05

                grdTab1Master.View.Columns["OSPVENDORID"].OwnerBand.Visible = true;
                grdTab1Master.View.Columns["OSPVENDORNAME"].OwnerBand.Visible = true;
                grdTab1Master.View.Columns["PRICEUNIT"].OwnerBand.Visible = true;
                grdTab1Master.View.Columns["VALIDSTATE"].OwnerBand.Visible = true;
                grdTab1Master.View.Columns["OSPVENDORID"].Visible = true;
                grdTab1Master.View.Columns["OSPVENDORNAME"].Visible = true;
                grdTab1Master.View.Columns["PRICEUNIT"].Visible = true;
                grdTab1Master.View.Columns["VALIDSTATE"].Visible = true;
            }
        }

        private void InitializeGrid_GrdTab2MasterDisplayCaption()
        {
            DataRow row = grdDetail.View.GetFocusedDataRow();
            if (row == null)
            {
                //GrdTab2MasterDisplayCaptionHidden();
                return;
            }

            grdTab2Master.View.Columns["PRODUCTDEFID"].OwnerBand.Visible = true;
            grdTab2Master.View.Columns["PRODUCTDEFVERSION"].OwnerBand.Visible = true;
            grdTab2Master.View.Columns["PRODUCTDEFNAME"].OwnerBand.Visible = true;

            grdTab2Master.View.Columns["PRODUCTDEFID"].Visible = true;
            grdTab2Master.View.Columns["PRODUCTDEFVERSION"].Visible = true;
            grdTab2Master.View.Columns["PRODUCTDEFNAME"].Visible = true;
            if (row != null)
            {
                #region PRICEITEMID01

                if (!(row["PRICEITEMID01"].ToString().Equals("")))
                {
                    if (row["ISRANGE01"].ToString().Equals("Y"))  //구간여부
                    {
                        grdTab2Master.View.Columns["ITEMVALUE01FR"].OwnerBand.Visible = true;
                        grdTab2Master.View.Columns["ITEMVALUE01TO"].OwnerBand.Visible = true;

                        grdTab2Master.View.Columns["ITEMVALUE01FR"].Visible = true;
                        grdTab2Master.View.Columns["ITEMVALUE01TO"].Visible = true;

                        grdTab2Master.View.Columns["ITEMVALUE01FR"].Caption = row["PRICEITEMNAME01"].ToString() + "Fr";
                        grdTab2Master.View.Columns["ITEMVALUE01FR"].ToolTip = row["PRICEITEMNAME01"].ToString() + "Fr";
                        grdTab2Master.View.Columns["ITEMVALUE01TO"].Caption = row["PRICEITEMNAME01"].ToString() + "To";
                        grdTab2Master.View.Columns["ITEMVALUE01TO"].ToolTip = row["PRICEITEMNAME01"].ToString() + "To";
                    }
                    else
                    {
                        if (row["COMPONENTTYPE01"].ToString().Equals("PopUp"))
                        {
                            grdTab2Master.View.Columns["ITEMVALUE01"].OwnerBand.Visible = true;
                            grdTab2Master.View.Columns["ITEMVALUE01NAME"].OwnerBand.Visible = true;

                            grdTab2Master.View.Columns["ITEMVALUE01"].Visible = true;
                            grdTab2Master.View.Columns["ITEMVALUE01NAME"].Visible = true;
                            grdTab2Master.View.Columns["ITEMVALUE01"].Caption = row["PRICEITEMNAME01"].ToString() + "Id";
                            grdTab2Master.View.Columns["ITEMVALUE01"].ToolTip = row["PRICEITEMNAME01"].ToString() + "Id";
                            grdTab2Master.View.Columns["ITEMVALUE01NAME"].Caption = row["PRICEITEMNAME01"].ToString();
                            grdTab2Master.View.Columns["ITEMVALUE01NAME"].ToolTip = row["PRICEITEMNAME01"].ToString();
                        }
                        else if (row["COMPONENTTYPE01"].ToString().Equals("ComboBox"))
                        {
                            grdTab2Master.View.Columns["ITEMVALUE01NAME"].OwnerBand.Visible = true;

                            grdTab2Master.View.Columns["ITEMVALUE01NAME"].Visible = true;
                            grdTab2Master.View.Columns["ITEMVALUE01NAME"].ToolTip = row["PRICEITEMNAME01"].ToString();
                            grdTab2Master.View.Columns["ITEMVALUE01NAME"].Caption = row["PRICEITEMNAME01"].ToString();
                        }
                        else
                        {
                            grdTab2Master.View.Columns["ITEMVALUE01"].OwnerBand.Visible = true;

                            grdTab2Master.View.Columns["ITEMVALUE01"].Visible = true;

                            grdTab2Master.View.Columns["ITEMVALUE01"].ToolTip = row["PRICEITEMNAME01"].ToString();
                            grdTab2Master.View.Columns["ITEMVALUE01"].Caption = row["PRICEITEMNAME01"].ToString();
                        }
                    }
                }

                #endregion PRICEITEMID01

                #region PRICEITEMID02

                if (!(row["PRICEITEMID02"].ToString().Equals("")))
                {
                    if (row["ISRANGE02"].ToString().Equals("Y"))  //구간여부
                    {
                        grdTab2Master.View.Columns["ITEMVALUE02FR"].OwnerBand.Visible = true;
                        grdTab2Master.View.Columns["ITEMVALUE02TO"].OwnerBand.Visible = true;

                        grdTab2Master.View.Columns["ITEMVALUE02FR"].Visible = true;
                        grdTab2Master.View.Columns["ITEMVALUE02TO"].Visible = true;

                        grdTab2Master.View.Columns["ITEMVALUE02FR"].Caption = row["PRICEITEMNAME02"].ToString() + "Fr";
                        grdTab2Master.View.Columns["ITEMVALUE02FR"].ToolTip = row["PRICEITEMNAME02"].ToString() + "Fr";
                        grdTab2Master.View.Columns["ITEMVALUE02TO"].Caption = row["PRICEITEMNAME02"].ToString() + "To";
                        grdTab2Master.View.Columns["ITEMVALUE02TO"].ToolTip = row["PRICEITEMNAME02"].ToString() + "To";
                    }
                    else
                    {
                        if (row["COMPONENTTYPE02"].ToString().Equals("PopUp"))
                        {
                            grdTab2Master.View.Columns["ITEMVALUE02"].OwnerBand.Visible = true;
                            grdTab2Master.View.Columns["ITEMVALUE02NAME"].OwnerBand.Visible = true;

                            grdTab2Master.View.Columns["ITEMVALUE02"].Visible = true;
                            grdTab2Master.View.Columns["ITEMVALUE02NAME"].Visible = true;

                            grdTab2Master.View.Columns["ITEMVALUE02"].Caption = row["PRICEITEMNAME02"].ToString() + "Id";
                            grdTab2Master.View.Columns["ITEMVALUE02"].ToolTip = row["PRICEITEMNAME02"].ToString() + "Id";
                            grdTab2Master.View.Columns["ITEMVALUE02NAME"].Caption = row["PRICEITEMNAME02"].ToString();
                            grdTab2Master.View.Columns["ITEMVALUE02NAME"].ToolTip = row["PRICEITEMNAME02"].ToString();
                        }
                        else if (row["COMPONENTTYPE02"].ToString().Equals("ComboBox"))
                        {
                            grdTab2Master.View.Columns["ITEMVALUE02NAME"].OwnerBand.Visible = true;

                            grdTab2Master.View.Columns["ITEMVALUE02NAME"].Visible = true;
                            grdTab2Master.View.Columns["ITEMVALUE02NAME"].ToolTip = row["PRICEITEMNAME02"].ToString();
                            grdTab2Master.View.Columns["ITEMVALUE02NAME"].Caption = row["PRICEITEMNAME02"].ToString();
                        }
                        else
                        {
                            grdTab2Master.View.Columns["ITEMVALUE02"].OwnerBand.Visible = true;

                            grdTab2Master.View.Columns["ITEMVALUE02"].Visible = true;

                            grdTab2Master.View.Columns["ITEMVALUE02"].ToolTip = row["PRICEITEMNAME02"].ToString();
                            grdTab2Master.View.Columns["ITEMVALUE02"].Caption = row["PRICEITEMNAME02"].ToString();
                        }
                    }
                }

                #endregion PRICEITEMID02

                #region PRICEITEMID03

                if (!(row["PRICEITEMID03"].ToString().Equals("")))
                {
                    if (row["ISRANGE03"].ToString().Equals("Y"))  //구간여부
                    {
                        grdTab2Master.View.Columns["ITEMVALUE03FR"].OwnerBand.Visible = true;
                        grdTab2Master.View.Columns["ITEMVALUE03TO"].OwnerBand.Visible = true;

                        grdTab2Master.View.Columns["ITEMVALUE03FR"].Visible = true;
                        grdTab2Master.View.Columns["ITEMVALUE03TO"].Visible = true;

                        grdTab2Master.View.Columns["ITEMVALUE03FR"].Caption = row["PRICEITEMNAME03"].ToString() + "Fr";
                        grdTab2Master.View.Columns["ITEMVALUE03FR"].ToolTip = row["PRICEITEMNAME03"].ToString() + "Fr";
                        grdTab2Master.View.Columns["ITEMVALUE03TO"].Caption = row["PRICEITEMNAME03"].ToString() + "To";
                        grdTab2Master.View.Columns["ITEMVALUE03TO"].ToolTip = row["PRICEITEMNAME03"].ToString() + "To";
                    }
                    else
                    {
                        if (row["COMPONENTTYPE03"].ToString().Equals("PopUp"))
                        {
                            grdTab2Master.View.Columns["ITEMVALUE03"].OwnerBand.Visible = true;
                            grdTab2Master.View.Columns["ITEMVALUE03NAME"].OwnerBand.Visible = true;

                            grdTab2Master.View.Columns["ITEMVALUE03"].Visible = true;
                            grdTab2Master.View.Columns["ITEMVALUE03NAME"].Visible = true;
                            grdTab2Master.View.Columns["ITEMVALUE03"].Caption = row["PRICEITEMNAME03"].ToString() + "Id";
                            grdTab2Master.View.Columns["ITEMVALUE03"].ToolTip = row["PRICEITEMNAME03"].ToString() + "Id";
                            grdTab2Master.View.Columns["ITEMVALUE03NAME"].Caption = row["PRICEITEMNAME03"].ToString();
                            grdTab2Master.View.Columns["ITEMVALUE03NAME"].ToolTip = row["PRICEITEMNAME03"].ToString();
                        }
                        else if (row["COMPONENTTYPE03"].ToString().Equals("ComboBox"))
                        {
                            grdTab2Master.View.Columns["ITEMVALUE03NAME"].OwnerBand.Visible = true;

                            grdTab2Master.View.Columns["ITEMVALUE03NAME"].Visible = true;
                            grdTab2Master.View.Columns["ITEMVALUE03NAME"].Caption = row["PRICEITEMNAME03"].ToString();
                            grdTab2Master.View.Columns["ITEMVALUE03NAME"].ToolTip = row["PRICEITEMNAME03"].ToString();
                        }
                        else
                        {
                            grdTab2Master.View.Columns["ITEMVALUE03"].OwnerBand.Visible = true;

                            grdTab2Master.View.Columns["ITEMVALUE03"].Visible = true;

                            grdTab2Master.View.Columns["ITEMVALUE03"].Caption = row["PRICEITEMNAME03"].ToString();
                            grdTab2Master.View.Columns["ITEMVALUE03"].ToolTip = row["PRICEITEMNAME03"].ToString();
                        }
                    }
                }

                #endregion PRICEITEMID03

                #region PRICEITEMID04

                if (!(row["PRICEITEMID04"].ToString().Equals("")))
                {
                    if (row["ISRANGE04"].ToString().Equals("Y"))  //구간여부
                    {
                        grdTab2Master.View.Columns["ITEMVALUE04FR"].OwnerBand.Visible = true;
                        grdTab2Master.View.Columns["ITEMVALUE04TO"].OwnerBand.Visible = true;

                        grdTab2Master.View.Columns["ITEMVALUE04FR"].Visible = true;
                        grdTab2Master.View.Columns["ITEMVALUE04TO"].Visible = true;

                        grdTab2Master.View.Columns["ITEMVALUE04FR"].Caption = row["PRICEITEMNAME04"].ToString() + "Fr";
                        grdTab2Master.View.Columns["ITEMVALUE04FR"].ToolTip = row["PRICEITEMNAME04"].ToString() + "Fr";
                        grdTab2Master.View.Columns["ITEMVALUE04TO"].Caption = row["PRICEITEMNAME04"].ToString() + "To";
                        grdTab2Master.View.Columns["ITEMVALUE04TO"].ToolTip = row["PRICEITEMNAME04"].ToString() + "To";
                    }
                    else
                    {
                        if (row["COMPONENTTYPE04"].ToString().Equals("PopUp"))
                        {
                            grdTab2Master.View.Columns["ITEMVALUE04"].OwnerBand.Visible = true;
                            grdTab2Master.View.Columns["ITEMVALUE04NAME"].OwnerBand.Visible = true;

                            grdTab2Master.View.Columns["ITEMVALUE04"].Visible = true;
                            grdTab2Master.View.Columns["ITEMVALUE04NAME"].Visible = true;
                            grdTab2Master.View.Columns["ITEMVALUE04"].Caption = row["PRICEITEMNAME04"].ToString() + "Id";
                            grdTab2Master.View.Columns["ITEMVALUE04"].ToolTip = row["PRICEITEMNAME04"].ToString() + "Id";
                            grdTab2Master.View.Columns["ITEMVALUE04NAME"].Caption = row["PRICEITEMNAME04"].ToString();
                            grdTab2Master.View.Columns["ITEMVALUE04NAME"].ToolTip = row["PRICEITEMNAME04"].ToString();
                        }
                        else if (row["COMPONENTTYPE04"].ToString().Equals("ComboBox"))
                        {
                            grdTab2Master.View.Columns["ITEMVALUE04NAME"].OwnerBand.Visible = true;

                            grdTab2Master.View.Columns["ITEMVALUE04NAME"].Visible = true;
                            grdTab2Master.View.Columns["ITEMVALUE04NAME"].Caption = row["PRICEITEMNAME04"].ToString();
                            grdTab2Master.View.Columns["ITEMVALUE04NAME"].ToolTip = row["PRICEITEMNAME04"].ToString();
                        }
                        else
                        {
                            grdTab2Master.View.Columns["ITEMVALUE04"].OwnerBand.Visible = true;

                            grdTab2Master.View.Columns["ITEMVALUE04"].Visible = true;

                            grdTab2Master.View.Columns["ITEMVALUE04"].Caption = row["PRICEITEMNAME04"].ToString();
                            grdTab2Master.View.Columns["ITEMVALUE04"].ToolTip = row["PRICEITEMNAME04"].ToString();
                        }
                    }
                }

                #endregion PRICEITEMID04

                #region PRICEITEMID05

                if (!(row["PRICEITEMID05"].ToString().Equals("")))
                {
                    if (row["ISRANGE05"].ToString().Equals("Y"))  //구간여부
                    {
                        grdTab2Master.View.Columns["ITEMVALUE05FR"].OwnerBand.Visible = true;
                        grdTab2Master.View.Columns["ITEMVALUE05TO"].OwnerBand.Visible = true;

                        grdTab2Master.View.Columns["ITEMVALUE05FR"].Visible = true;
                        grdTab2Master.View.Columns["ITEMVALUE05TO"].Visible = true;

                        grdTab2Master.View.Columns["ITEMVALUE05FR"].Caption = row["PRICEITEMNAME05"].ToString() + "Fr";
                        grdTab2Master.View.Columns["ITEMVALUE05FR"].ToolTip = row["PRICEITEMNAME05"].ToString() + "Fr";
                        grdTab2Master.View.Columns["ITEMVALUE05TO"].Caption = row["PRICEITEMNAME05"].ToString() + "To";
                        grdTab2Master.View.Columns["ITEMVALUE05TO"].ToolTip = row["PRICEITEMNAME05"].ToString() + "To";
                    }
                    else
                    {
                        if (row["COMPONENTTYPE05"].ToString().Equals("PopUp"))
                        {
                            grdTab2Master.View.Columns["ITEMVALUE05"].OwnerBand.Visible = true;
                            grdTab2Master.View.Columns["ITEMVALUE05NAME"].OwnerBand.Visible = true;

                            grdTab2Master.View.Columns["ITEMVALUE05"].Visible = true;
                            grdTab2Master.View.Columns["ITEMVALUE05NAME"].Visible = true;
                            grdTab2Master.View.Columns["ITEMVALUE05"].Caption = row["PRICEITEMNAME05"].ToString() + "Id";
                            grdTab2Master.View.Columns["ITEMVALUE05"].ToolTip = row["PRICEITEMNAME05"].ToString() + "Id";
                            grdTab2Master.View.Columns["ITEMVALUE05NAME"].Caption = row["PRICEITEMNAME05"].ToString();
                            grdTab2Master.View.Columns["ITEMVALUE05NAME"].ToolTip = row["PRICEITEMNAME05"].ToString();
                        }
                        else if (row["COMPONENTTYPE05"].ToString().Equals("ComboBox"))
                        {
                            grdTab2Master.View.Columns["ITEMVALUE05NAME"].OwnerBand.Visible = true;

                            grdTab2Master.View.Columns["ITEMVALUE05NAME"].Visible = true;
                            grdTab2Master.View.Columns["ITEMVALUE05NAME"].Caption = row["PRICEITEMNAME05"].ToString();
                            grdTab2Master.View.Columns["ITEMVALUE05NAME"].ToolTip = row["PRICEITEMNAME05"].ToString();
                        }
                        else
                        {
                            grdTab2Master.View.Columns["ITEMVALUE05"].OwnerBand.Visible = true;

                            grdTab2Master.View.Columns["ITEMVALUE05"].Visible = true;

                            grdTab2Master.View.Columns["ITEMVALUE05"].Caption = row["PRICEITEMNAME05"].ToString();
                            grdTab2Master.View.Columns["ITEMVALUE05"].ToolTip = row["PRICEITEMNAME05"].ToString();
                        }
                    }
                }

                #endregion PRICEITEMID05

                grdTab2Master.View.Columns["OSPVENDORID"].OwnerBand.Visible = true;
                grdTab2Master.View.Columns["OSPVENDORNAME"].OwnerBand.Visible = true;
                grdTab2Master.View.Columns["PRICEUNIT"].OwnerBand.Visible = true;
                grdTab2Master.View.Columns["VALIDSTATE"].OwnerBand.Visible = true;
                grdTab2Master.View.Columns["OSPVENDORID"].Visible = true;
                grdTab2Master.View.Columns["OSPVENDORNAME"].Visible = true;
                grdTab2Master.View.Columns["PRICEUNIT"].Visible = true;
                grdTab2Master.View.Columns["VALIDSTATE"].Visible = true;
            }
        }

        private bool CheckPriceDateKeyColumns()
        {
            bool blcheck = true;
            if (grdDetail.View.DataRowCount == 0)
            {
                return blcheck;
            }

            for (int irow = 0; irow < grdDetail.View.DataRowCount; irow++)
            {
                DataRow row = grdDetail.View.GetDataRow(irow);

                if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                {
                    string strValue = row["OSPPRICECODE"].ToString();

                    if (SearchPeriodidKey(strValue, "OSPPRICECODE", irow) < 0)
                    {
                        blcheck = true;
                    }
                    else
                    {
                        // 에러 외주단가 항목
                        string lblPeriodid = grdDetail.View.Columns["OSPPRICECODE"].Caption.ToString();
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspData007", lblPeriodid);
                        return false;
                    }
                    strValue = row["PRIORITY"].ToString();

                    if (SearchPeriodidKey(strValue, "PRIORITY", irow) < 0)
                    {
                        blcheck = true;
                    }
                    else
                    {
                        // 에러 우선순위 중복
                        string lblPeriodid = grdDetail.View.Columns["PRIORITY"].Caption.ToString();
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspData007", lblPeriodid);
                        return false;
                    }
                    //중복값 체크 처리
                }
            }
            return blcheck;
        }

        /// <summary>
        ///  중복 체크 함수
        /// </summary>
        /// <param name="dateStartdate"></param>
        /// <param name="dateEnddate"></param>
        /// <param name="icurRow"></param>
        /// <returns></returns>
        private int SearchTab1MasterValueKey(int icurRow)
        {
            int iresultRow = -1;
            DataRow dr = grdDetail.View.GetFocusedDataRow();
            string strPriceitemid01 = dr["PRICEITEMID01"].ToString();
            string strPriceitemid02 = dr["PRICEITEMID02"].ToString();
            string strPriceitemid03 = dr["PRICEITEMID03"].ToString();
            string strPriceitemid04 = dr["PRICEITEMID04"].ToString();
            string strPriceitemid05 = dr["PRICEITEMID05"].ToString();
            string strIsrange01 = dr["ISRANGE01"].ToString();
            string strIsrange02 = dr["ISRANGE02"].ToString();
            string strIsrange03 = dr["ISRANGE03"].ToString();
            string strIsrange04 = dr["ISRANGE04"].ToString();
            string strIsrange05 = dr["ISRANGE05"].ToString();
            DataRow drValue = grdTab1Master.View.GetDataRow(icurRow);
            string strOspproductiontype = drValue["OSPPRODUCTIONTYPE"].ToString();
            string strProductdefid = drValue["PRODUCTDEFID"].ToString();
            string strProductdefversion = drValue["PRODUCTDEFVERSION"].ToString();
            string strOspvendorid = drValue["OSPVENDORID"].ToString();
            string strItemvalue01 = ""; string strItemvalue01fr = ""; string strItemvalue01to = "";
            string strItemvalue02 = ""; string strItemvalue02fr = ""; string strItemvalue02to = "";
            string strItemvalue03 = ""; string strItemvalue03fr = ""; string strItemvalue03to = "";
            string strItemvalue04 = ""; string strItemvalue04fr = ""; string strItemvalue04to = "";
            string strItemvalue05 = ""; string strItemvalue05fr = ""; string strItemvalue05to = "";

            #region strItemvalue셋팅

            #region PRICEITEMID01

            if (strPriceitemid01.Equals(""))
            {
                strItemvalue01 = ""; strItemvalue01fr = ""; strItemvalue01to = "";
            }
            else
            {
                if (strIsrange01.Equals("N"))
                {
                    strItemvalue01 = drValue["ITEMVALUE01"].ToString();
                    strItemvalue01fr = ""; strItemvalue01to = "";
                }
                else
                {
                    strItemvalue01 = "";
                    strItemvalue01fr = drValue["ITEMVALUE01FR"].ToString(); strItemvalue01to = drValue["ITEMVALUE01TO"].ToString();
                }
            }

            #endregion PRICEITEMID01

            #region PRICEITEMID02

            if (strPriceitemid02.Equals(""))
            {
                strItemvalue02 = ""; strItemvalue02fr = ""; strItemvalue02to = "";
            }
            else
            {
                if (strIsrange02.Equals("N"))
                {
                    strItemvalue02 = drValue["ITEMVALUE02"].ToString();
                    strItemvalue02fr = ""; strItemvalue02to = "";
                }
                else
                {
                    strItemvalue02 = "";
                    strItemvalue02fr = drValue["ITEMVALUE02FR"].ToString(); strItemvalue02to = drValue["ITEMVALUE02TO"].ToString();
                }
            }

            #endregion PRICEITEMID02

            #region PRICEITEMID03

            if (strPriceitemid03.Equals(""))
            {
                strItemvalue03 = ""; strItemvalue03fr = ""; strItemvalue03to = "";
            }
            else
            {
                if (strIsrange03.Equals("N"))
                {
                    strItemvalue03 = drValue["ITEMVALUE03"].ToString();
                    strItemvalue03fr = ""; strItemvalue03to = "";
                }
                else
                {
                    strItemvalue03 = "";
                    strItemvalue03fr = drValue["ITEMVALUE03FR"].ToString(); strItemvalue03to = drValue["ITEMVALUE03TO"].ToString();
                }
            }

            #endregion PRICEITEMID03

            #region PRICEITEMID04

            if (strPriceitemid04.Equals(""))
            {
                strItemvalue04 = ""; strItemvalue04fr = ""; strItemvalue04to = "";
            }
            else
            {
                if (strIsrange04.Equals("N"))
                {
                    strItemvalue04 = drValue["ITEMVALUE04"].ToString();
                    strItemvalue04fr = ""; strItemvalue04to = "";
                }
                else
                {
                    strItemvalue04 = "";
                    strItemvalue04fr = drValue["ITEMVALUE04FR"].ToString(); strItemvalue04to = drValue["ITEMVALUE04TO"].ToString();
                }
            }

            #endregion PRICEITEMID04

            #region PRICEITEMID05

            if (strPriceitemid05.Equals(""))
            {
                strItemvalue05 = ""; strItemvalue05fr = ""; strItemvalue05to = "";
            }
            else
            {
                if (strIsrange05.Equals("N"))
                {
                    strItemvalue05 = drValue["ITEMVALUE05"].ToString();
                    strItemvalue05fr = ""; strItemvalue01to = "";
                }
                else
                {
                    strItemvalue05 = "";
                    strItemvalue05fr = drValue["ITEMVALUE05FR"].ToString(); strItemvalue05to = drValue["ITEMVALUE05TO"].ToString();
                }
            }

            #endregion PRICEITEMID05

            #endregion strItemvalue셋팅

            for (int irow = 0; irow < grdTab1Master.View.DataRowCount; irow++)
            {
                if (icurRow != irow)
                {
                    DataRow row = grdTab1Master.View.GetDataRow(irow);
                    // 행 삭제만 제외
                    if (grdTab1Master.View.IsDeletedRow(row) == false)
                    {
                        string strTempOspproductiontype = row["OSPPRODUCTIONTYPE"].ToString();
                        string strTempProductdefid = row["PRODUCTDEFID"].ToString();
                        string strTempProductdefversion = row["PRODUCTDEFVERSION"].ToString();
                        string strTempOspvendorid = row["OSPVENDORID"].ToString();
                        string strTempItemvalue01 = ""; string strTempItemvalue01fr = ""; string strTempItemvalue01to = "";
                        string strTempItemvalue02 = ""; string strTempItemvalue02fr = ""; string strTempItemvalue02to = "";
                        string strTempItemvalue03 = ""; string strTempItemvalue03fr = ""; string strTempItemvalue03to = "";
                        string strTempItemvalue04 = ""; string strTempItemvalue04fr = ""; string strTempItemvalue04to = "";
                        string strTempItemvalue05 = ""; string strTempItemvalue05fr = ""; string strTempItemvalue05to = "";

                        #region strTempItemvalue셋팅

                        #region PRICEITEMID01

                        if (strPriceitemid01.Equals(""))
                        {
                            strTempItemvalue01 = ""; strTempItemvalue01fr = ""; strTempItemvalue01to = "";
                        }
                        else
                        {
                            if (strIsrange01.Equals("N"))
                            {
                                strTempItemvalue01 = row["ITEMVALUE01"].ToString();
                                strTempItemvalue01fr = ""; strTempItemvalue01to = "";
                            }
                            else
                            {
                                strTempItemvalue01 = "";
                                strTempItemvalue01fr = row["ITEMVALUE01FR"].ToString(); strTempItemvalue01to = row["ITEMVALUE01TO"].ToString();
                            }
                        }

                        #endregion PRICEITEMID01

                        #region PRICEITEMID02

                        if (strPriceitemid02.Equals(""))
                        {
                            strTempItemvalue02 = ""; strTempItemvalue02fr = ""; strTempItemvalue02to = "";
                        }
                        else
                        {
                            if (strIsrange02.Equals("N"))
                            {
                                strTempItemvalue02 = row["ITEMVALUE02"].ToString();
                                strTempItemvalue02fr = ""; strTempItemvalue02to = "";
                            }
                            else
                            {
                                strTempItemvalue02 = "";
                                strTempItemvalue02fr = row["ITEMVALUE02FR"].ToString(); strTempItemvalue02to = row["ITEMVALUE02TO"].ToString();
                            }
                        }

                        #endregion PRICEITEMID02

                        #region PRICEITEMID03

                        if (strPriceitemid03.Equals(""))
                        {
                            strTempItemvalue03 = ""; strTempItemvalue03fr = ""; strTempItemvalue03to = "";
                        }
                        else
                        {
                            if (strIsrange03.Equals("N"))
                            {
                                strTempItemvalue03 = row["ITEMVALUE03"].ToString();
                                strTempItemvalue03fr = ""; strTempItemvalue03to = "";
                            }
                            else
                            {
                                strTempItemvalue03 = "";
                                strTempItemvalue03fr = row["ITEMVALUE03FR"].ToString(); strTempItemvalue03to = row["ITEMVALUE03TO"].ToString();
                            }
                        }

                        #endregion PRICEITEMID03

                        #region PRICEITEMID04

                        if (strPriceitemid04.Equals(""))
                        {
                            strTempItemvalue04 = ""; strTempItemvalue04fr = ""; strTempItemvalue04to = "";
                        }
                        else
                        {
                            if (strIsrange04.Equals("N"))
                            {
                                strTempItemvalue04 = row["ITEMVALUE04"].ToString();
                                strTempItemvalue04fr = ""; strTempItemvalue04to = "";
                            }
                            else
                            {
                                strTempItemvalue04 = "";
                                strTempItemvalue04fr = row["ITEMVALUE04FR"].ToString(); strTempItemvalue04to = row["ITEMVALUE04TO"].ToString();
                            }
                        }

                        #endregion PRICEITEMID04

                        #region PRICEITEMID05

                        if (strPriceitemid05.Equals(""))
                        {
                            strTempItemvalue05 = ""; strTempItemvalue05fr = ""; strTempItemvalue05to = "";
                        }
                        else
                        {
                            if (strIsrange05.Equals("N"))
                            {
                                strTempItemvalue05 = row["ITEMVALUE05"].ToString();
                                strTempItemvalue05fr = ""; strTempItemvalue01to = "";
                            }
                            else
                            {
                                strTempItemvalue05 = "";
                                strTempItemvalue05fr = row["ITEMVALUE05FR"].ToString(); strTempItemvalue05to = row["ITEMVALUE05TO"].ToString();
                            }
                        }

                        #endregion PRICEITEMID05

                        #endregion strTempItemvalue셋팅

                        if (strOspproductiontype.Equals(strTempOspproductiontype) && strProductdefid.Equals(strTempProductdefid) && strProductdefversion.Equals(strTempProductdefversion)
                            && strItemvalue01.Equals(strTempItemvalue01) && strItemvalue01fr.Equals(strTempItemvalue01fr) && strItemvalue01to.Equals(strTempItemvalue01to)
                            && strItemvalue02.Equals(strTempItemvalue02) && strItemvalue02fr.Equals(strTempItemvalue02fr) && strItemvalue02to.Equals(strTempItemvalue02to)
                            && strItemvalue03.Equals(strTempItemvalue03) && strItemvalue03fr.Equals(strTempItemvalue03fr) && strItemvalue03to.Equals(strTempItemvalue03to)
                            && strItemvalue04.Equals(strTempItemvalue04) && strItemvalue04fr.Equals(strTempItemvalue04fr) && strItemvalue04to.Equals(strTempItemvalue04to)
                            && strItemvalue05.Equals(strTempItemvalue05) && strItemvalue05fr.Equals(strTempItemvalue05fr) && strItemvalue05to.Equals(strTempItemvalue05to)
                            && strOspvendorid.Equals(strTempOspvendorid))
                        {
                            return irow;
                        }
                    }
                }
            }
            return iresultRow;
        }

        /// <summary>
        ///  중복 체크 함수
        /// </summary>
        /// <param name="dateStartdate"></param>
        /// <param name="dateEnddate"></param>
        /// <param name="icurRow"></param>
        /// <returns></returns>
        private int SearchTab2MasterValueKey(int icurRow)
        {
            int iresultRow = -1;
            DataRow dr = grdDetail.View.GetFocusedDataRow();
            string strPriceitemid01 = dr["PRICEITEMID01"].ToString();
            string strPriceitemid02 = dr["PRICEITEMID02"].ToString();
            string strPriceitemid03 = dr["PRICEITEMID03"].ToString();
            string strPriceitemid04 = dr["PRICEITEMID04"].ToString();
            string strPriceitemid05 = dr["PRICEITEMID05"].ToString();
            string strIsrange01 = dr["ISRANGE01"].ToString();
            string strIsrange02 = dr["ISRANGE02"].ToString();
            string strIsrange03 = dr["ISRANGE03"].ToString();
            string strIsrange04 = dr["ISRANGE04"].ToString();
            string strIsrange05 = dr["ISRANGE05"].ToString();
            DataRow drValue = grdTab2Master.View.GetDataRow(icurRow);
            string strOspproductiontype = drValue["OSPPRODUCTIONTYPE"].ToString();
            string strProductdefid = drValue["PRODUCTDEFID"].ToString();
            string strProductdefversion = drValue["PRODUCTDEFVERSION"].ToString();
            string strOspvendorid = drValue["OSPVENDORID"].ToString();
            string strItemvalue01 = ""; string strItemvalue01fr = ""; string strItemvalue01to = "";
            string strItemvalue02 = ""; string strItemvalue02fr = ""; string strItemvalue02to = "";
            string strItemvalue03 = ""; string strItemvalue03fr = ""; string strItemvalue03to = "";
            string strItemvalue04 = ""; string strItemvalue04fr = ""; string strItemvalue04to = "";
            string strItemvalue05 = ""; string strItemvalue05fr = ""; string strItemvalue05to = "";

            #region strItemvalue셋팅

            #region PRICEITEMID01

            if (strPriceitemid01.Equals(""))
            {
                strItemvalue01 = ""; strItemvalue01fr = ""; strItemvalue01to = "";
            }
            else
            {
                if (strIsrange01.Equals("N"))
                {
                    strItemvalue01 = drValue["ITEMVALUE01"].ToString();
                    strItemvalue01fr = ""; strItemvalue01to = "";
                }
                else
                {
                    strItemvalue01 = "";
                    strItemvalue01fr = drValue["ITEMVALUE01FR"].ToString(); strItemvalue01to = drValue["ITEMVALUE01TO"].ToString();
                }
            }

            #endregion PRICEITEMID01

            #region PRICEITEMID02

            if (strPriceitemid02.Equals(""))
            {
                strItemvalue02 = ""; strItemvalue02fr = ""; strItemvalue02to = "";
            }
            else
            {
                if (strIsrange02.Equals("N"))
                {
                    strItemvalue02 = drValue["ITEMVALUE02"].ToString();
                    strItemvalue02fr = ""; strItemvalue02to = "";
                }
                else
                {
                    strItemvalue02 = "";
                    strItemvalue02fr = drValue["ITEMVALUE02FR"].ToString(); strItemvalue02to = drValue["ITEMVALUE02TO"].ToString();
                }
            }

            #endregion PRICEITEMID02

            #region PRICEITEMID03

            if (strPriceitemid03.Equals(""))
            {
                strItemvalue03 = ""; strItemvalue03fr = ""; strItemvalue03to = "";
            }
            else
            {
                if (strIsrange03.Equals("N"))
                {
                    strItemvalue03 = drValue["ITEMVALUE03"].ToString();
                    strItemvalue03fr = ""; strItemvalue03to = "";
                }
                else
                {
                    strItemvalue03 = "";
                    strItemvalue03fr = drValue["ITEMVALUE03FR"].ToString(); strItemvalue03to = drValue["ITEMVALUE03TO"].ToString();
                }
            }

            #endregion PRICEITEMID03

            #region PRICEITEMID04

            if (strPriceitemid04.Equals(""))
            {
                strItemvalue04 = ""; strItemvalue04fr = ""; strItemvalue04to = "";
            }
            else
            {
                if (strIsrange04.Equals("N"))
                {
                    strItemvalue04 = drValue["ITEMVALUE04"].ToString();
                    strItemvalue04fr = ""; strItemvalue04to = "";
                }
                else
                {
                    strItemvalue04 = "";
                    strItemvalue04fr = drValue["ITEMVALUE04FR"].ToString(); strItemvalue04to = drValue["ITEMVALUE04TO"].ToString();
                }
            }

            #endregion PRICEITEMID04

            #region PRICEITEMID05

            if (strPriceitemid05.Equals(""))
            {
                strItemvalue05 = ""; strItemvalue05fr = ""; strItemvalue05to = "";
            }
            else
            {
                if (strIsrange05.Equals("N"))
                {
                    strItemvalue05 = drValue["ITEMVALUE05"].ToString();
                    strItemvalue05fr = ""; strItemvalue01to = "";
                }
                else
                {
                    strItemvalue05 = "";
                    strItemvalue05fr = drValue["ITEMVALUE05FR"].ToString(); strItemvalue05to = drValue["ITEMVALUE05TO"].ToString();
                }
            }

            #endregion PRICEITEMID05

            #endregion strItemvalue셋팅

            for (int irow = 0; irow < grdTab2Master.View.DataRowCount; irow++)
            {
                if (icurRow != irow)
                {
                    DataRow row = grdTab2Master.View.GetDataRow(irow);
                    // 행 삭제만 제외
                    if (grdTab2Master.View.IsDeletedRow(row) == false)
                    {
                        string strTempOspproductiontype = row["OSPPRODUCTIONTYPE"].ToString();
                        string strTempProductdefid = row["PRODUCTDEFID"].ToString();
                        string strTempProductdefversion = row["PRODUCTDEFVERSION"].ToString();
                        string strTempOspvendorid = row["OSPVENDORID"].ToString();
                        string strTempItemvalue01 = ""; string strTempItemvalue01fr = ""; string strTempItemvalue01to = "";
                        string strTempItemvalue02 = ""; string strTempItemvalue02fr = ""; string strTempItemvalue02to = "";
                        string strTempItemvalue03 = ""; string strTempItemvalue03fr = ""; string strTempItemvalue03to = "";
                        string strTempItemvalue04 = ""; string strTempItemvalue04fr = ""; string strTempItemvalue04to = "";
                        string strTempItemvalue05 = ""; string strTempItemvalue05fr = ""; string strTempItemvalue05to = "";

                        #region strTempItemvalue셋팅

                        #region PRICEITEMID01

                        if (strPriceitemid01.Equals(""))
                        {
                            strTempItemvalue01 = ""; strTempItemvalue01fr = ""; strTempItemvalue01to = "";
                        }
                        else
                        {
                            if (strIsrange01.Equals("N"))
                            {
                                strTempItemvalue01 = row["ITEMVALUE01"].ToString();
                                strTempItemvalue01fr = ""; strTempItemvalue01to = "";
                            }
                            else
                            {
                                strTempItemvalue01 = "";
                                strTempItemvalue01fr = row["ITEMVALUE01FR"].ToString(); strTempItemvalue01to = row["ITEMVALUE01TO"].ToString();
                            }
                        }

                        #endregion PRICEITEMID01

                        #region PRICEITEMID02

                        if (strPriceitemid02.Equals(""))
                        {
                            strTempItemvalue02 = ""; strTempItemvalue02fr = ""; strTempItemvalue02to = "";
                        }
                        else
                        {
                            if (strIsrange02.Equals("N"))
                            {
                                strTempItemvalue02 = row["ITEMVALUE02"].ToString();
                                strTempItemvalue02fr = ""; strTempItemvalue02to = "";
                            }
                            else
                            {
                                strTempItemvalue02 = "";
                                strTempItemvalue02fr = row["ITEMVALUE02FR"].ToString(); strTempItemvalue02to = row["ITEMVALUE02TO"].ToString();
                            }
                        }

                        #endregion PRICEITEMID02

                        #region PRICEITEMID03

                        if (strPriceitemid03.Equals(""))
                        {
                            strTempItemvalue03 = ""; strTempItemvalue03fr = ""; strTempItemvalue03to = "";
                        }
                        else
                        {
                            if (strIsrange03.Equals("N"))
                            {
                                strTempItemvalue03 = row["ITEMVALUE03"].ToString();
                                strTempItemvalue03fr = ""; strTempItemvalue03to = "";
                            }
                            else
                            {
                                strTempItemvalue03 = "";
                                strTempItemvalue03fr = row["ITEMVALUE03FR"].ToString(); strTempItemvalue03to = row["ITEMVALUE03TO"].ToString();
                            }
                        }

                        #endregion PRICEITEMID03

                        #region PRICEITEMID04

                        if (strPriceitemid04.Equals(""))
                        {
                            strTempItemvalue04 = ""; strTempItemvalue04fr = ""; strTempItemvalue04to = "";
                        }
                        else
                        {
                            if (strIsrange04.Equals("N"))
                            {
                                strTempItemvalue04 = row["ITEMVALUE04"].ToString();
                                strTempItemvalue04fr = ""; strTempItemvalue04to = "";
                            }
                            else
                            {
                                strTempItemvalue04 = "";
                                strTempItemvalue04fr = row["ITEMVALUE04FR"].ToString(); strTempItemvalue04to = row["ITEMVALUE04TO"].ToString();
                            }
                        }

                        #endregion PRICEITEMID04

                        #region PRICEITEMID05

                        if (strPriceitemid05.Equals(""))
                        {
                            strTempItemvalue05 = ""; strTempItemvalue05fr = ""; strTempItemvalue05to = "";
                        }
                        else
                        {
                            if (strIsrange05.Equals("N"))
                            {
                                strTempItemvalue05 = row["ITEMVALUE05"].ToString();
                                strTempItemvalue05fr = ""; strTempItemvalue01to = "";
                            }
                            else
                            {
                                strTempItemvalue05 = "";
                                strTempItemvalue05fr = row["ITEMVALUE05FR"].ToString(); strTempItemvalue05to = row["ITEMVALUE05TO"].ToString();
                            }
                        }

                        #endregion PRICEITEMID05

                        #endregion strTempItemvalue셋팅

                        if (strOspproductiontype.Equals(strTempOspproductiontype) && strProductdefid.Equals(strTempProductdefid) && strProductdefversion.Equals(strTempProductdefversion)
                            && strItemvalue01.Equals(strTempItemvalue01) && strItemvalue01fr.Equals(strTempItemvalue01fr) && strItemvalue01to.Equals(strTempItemvalue01to)
                            && strItemvalue02.Equals(strTempItemvalue02) && strItemvalue02fr.Equals(strTempItemvalue02fr) && strItemvalue02to.Equals(strTempItemvalue02to)
                            && strItemvalue03.Equals(strTempItemvalue03) && strItemvalue03fr.Equals(strTempItemvalue03fr) && strItemvalue03to.Equals(strTempItemvalue03to)
                            && strItemvalue04.Equals(strTempItemvalue04) && strItemvalue04fr.Equals(strTempItemvalue04fr) && strItemvalue04to.Equals(strTempItemvalue04to)
                            && strItemvalue05.Equals(strTempItemvalue05) && strItemvalue05fr.Equals(strTempItemvalue05fr) && strItemvalue05to.Equals(strTempItemvalue05to)
                            && strOspvendorid.Equals(strTempOspvendorid))
                        {
                            return irow;
                        }
                    }
                }
            }
            return iresultRow;
        }

        /// <summary>
        ///  중복 체크 함수
        /// </summary>
        /// <param name="dateStartdate"></param>
        /// <param name="dateEnddate"></param>
        /// <param name="icurRow"></param>
        /// <returns></returns>
        private int SearchPeriodidKey(string strValue, string colstringName, int icurRow)
        {
            int iresultRow = -1;

            for (int irow = 0; irow < grdDetail.View.DataRowCount; irow++)
            {
                if (icurRow != irow)
                {
                    DataRow row = grdDetail.View.GetDataRow(irow);

                    // 행 삭제만 제외
                    if (grdDetail.View.IsDeletedRow(row) == false)
                    {
                        string strTemnpValue = row[colstringName].ToString();

                        if (strValue.Equals(strTemnpValue))
                        {
                            return irow;
                        }
                    }
                }
            }
            return iresultRow;
        }

        /// <summary>
        /// 단가 기준  key 중복 체크
        /// </summary>
        /// <returns></returns>
        private bool CheckPriceTabDateKeyColumns(SmartBandedGrid TargetDetail)
        {
            bool blcheck = true;
            if (TargetDetail.View.DataRowCount == 0)
            {
                return blcheck;
            }

            for (int irow = 0; irow < TargetDetail.View.DataRowCount; irow++)
            {
                DataRow row = TargetDetail.View.GetDataRow(irow);

                if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                {
                    DateTime dateStartdate = Convert.ToDateTime(row["STARTDATE"]);
                    DateTime dateEnddate = Convert.ToDateTime(row["ENDDATE"]);
                    if (SearchPriceDateKey(TargetDetail, dateStartdate, dateEnddate, irow) < 0)
                    {
                        blcheck = true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return blcheck;
        }

        /// <summary>
        /// 기간 중복 체크 함수
        /// </summary>
        /// <param name="dateStartdate"></param>
        /// <param name="dateEnddate"></param>
        /// <param name="icurRow"></param>
        /// <returns></returns>
        private int SearchPriceDateKey(SmartBandedGrid TargetDetail, DateTime dateStartdate, DateTime dateEnddate, int icurRow)
        {
            int iresultRow = -1;

            for (int irow = 0; irow < TargetDetail.View.DataRowCount; irow++)
            {
                if (icurRow != irow)
                {
                    DataRow row = TargetDetail.View.GetDataRow(irow);

                    // 행 삭제만 제외
                    if (TargetDetail.View.IsDeletedRow(row) == false)
                    {
                        DateTime dateSearchStartdate = Convert.ToDateTime(row["STARTDATE"]);
                        DateTime dateSearchEnddate = Convert.ToDateTime(row["ENDDATE"]);
                        //시작일 비교
                        if (dateStartdate >= dateSearchStartdate && dateStartdate <= dateSearchEnddate)
                        {
                            return irow;
                        }
                        // 종료일 비교
                        if (dateEnddate >= dateSearchStartdate && dateEnddate <= dateSearchEnddate)
                        {
                            return irow;
                        }
                    }
                }
            }
            return iresultRow;
        }

        #endregion Private Function
    }
}