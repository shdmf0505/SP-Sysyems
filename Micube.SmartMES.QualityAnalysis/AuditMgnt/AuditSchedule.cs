#region using

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > Audit 관리 > Audit 계획표  
    /// 업  무  설  명  : Audit를 년단위로 월의 주차별로 날짜를 관리한다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-08-07
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class AuditSchedule : SmartConditionManualBaseForm
    {
        #region Local Variables

        private string _productionYearMonth = "";
        private string _processsegmentClassId = "";
        private string _col;

        #endregion

        #region 생성자

        public AuditSchedule()
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

            InitializeSubsidiaryGrid();
            InitializeSemiProductGrid();
            InitializeSheetFormGrid();
        }

        /// <summary>        
        /// 부자재 부문 그리드 초기화
        /// </summary>
        private void InitializeSubsidiaryGrid()
        {
            grdSubsidiary.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdSubsidiary.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Copy | GridButtonItem.Export;

            var standardInfo = grdSubsidiary.View.AddGroupColumn("STANDARD");
            standardInfo.AddTextBoxColumn("REQUESTNO", 100).SetIsHidden();

            // Vendor Popup
            var vendorPopup = standardInfo.AddSelectPopupColumn("VENDORNAME", 180, new SqlQuery("GetVendorListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("VENDOR", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetValidationKeyColumn()
                .SetPopupAutoFillColumns("VENDORNAME")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                    if (selectedRows.Count() < 1)
                    {
                        return;
                    }

                    foreach (DataRow row in selectedRows)
                    {                       
                        dataGridRow["VENDORNAME"] = row["VENDORNAME"].ToString();
                        dataGridRow["VENDORID"] = row["VENDORID"].ToString();
                    }
                });

            vendorPopup.Conditions.AddComboBox("PLANTID", new SqlQuery("GetPlantListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "PLANTID", "PLANTID")
                .SetDefault(UserInfo.Current.Plant);
            vendorPopup.Conditions.AddTextBox("VENDORIDNAME");

            vendorPopup.GridColumns.AddTextBoxColumn("PLANTID", 80);
            vendorPopup.GridColumns.AddTextBoxColumn("VENDORID", 80);
            vendorPopup.GridColumns.AddTextBoxColumn("VENDORNAME", 200);

            // Area Popup
            var areaPopup = standardInfo.AddSelectPopupColumn("AREANAME", 180, new SqlQuery("GetAreaListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("AREAID")
                .SetValidationKeyColumn()
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    if (selectedRows.Count() < 1)
                    {
                        return;
                    }

                    foreach (DataRow row in selectedRows)
                    {
                        dataGridRow["AREANAME"] = row["AREANAME"].ToString();
                        dataGridRow["AREAID"] = row["AREAID"].ToString();
                    }
                });

            areaPopup.Conditions.AddTextBox("AREAIDNAME");
            areaPopup.Conditions.AddTextBox("P_VENDORID")
                .SetPopupDefaultByGridColumnId("VENDORID")
                .SetLabel("")
                .SetIsHidden();

            areaPopup.GridColumns.AddTextBoxColumn("AREAID", 150);
            areaPopup.GridColumns.AddTextBoxColumn("AREANAME", 200);

            /* 2020.04.13-자재명 관리 안하도록 주석처리
            // Consumable Popup
            var consumablePopup = standardInfo.AddSelectPopupColumn("CONSUMABLEDEFNAME", 200, new SqlQuery("GetConsumableList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("CONSUMABLE", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("CONSUMABLEDEFID")
                //.SetValidationKeyColumn()
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    if (selectedRows.Count() < 1)
                    {
                        return;
                    }

                    foreach (DataRow row in selectedRows)
                    {
                        dataGridRow["CONSUMABLEDEFNAME"] = row["CONSUMABLEDEFNAME"].ToString();
                        dataGridRow["CONSUMABLEDEFID"] = row["CONSUMABLEDEFID"].ToString();
                        dataGridRow["CONSUMABLEDEFVERSION"] = row["CONSUMABLEDEFVERSION"].ToString();
                    }
                });

            consumablePopup.Conditions.AddTextBox("CONSUMABLEIDNAME");

            consumablePopup.GridColumns.AddTextBoxColumn("CONSUMABLEDEFID", 150);
            consumablePopup.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
            consumablePopup.GridColumns.AddTextBoxColumn("CONSUMABLEDEFVERSION", 100)
                .SetIsHidden();
            */

            standardInfo.AddTextBoxColumn("COMMENTS", 200)
                .SetLabel("UNIQUENESS");

            standardInfo.AddTextBoxColumn("VENDORID", 150)
                .SetIsHidden();
            standardInfo.AddTextBoxColumn("AREAID", 150)
                .SetIsHidden();
            standardInfo.AddTextBoxColumn("CONSUMABLEDEFID", 150)
                .SetIsHidden();
            standardInfo.AddTextBoxColumn("CONSUMABLEDEFVERSION", 150)
                .SetIsHidden();
            standardInfo.AddTextBoxColumn("YEAR", 150)
                .SetIsHidden();
            standardInfo.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();
            standardInfo.AddTextBoxColumn("PLANTID", 150)
                .SetIsHidden();

            var m1 = grdSubsidiary.View.AddGroupColumn("1M");

            m1.AddDateEditColumn("1M1W", 80)
                .SetLabel("1W");
            m1.AddDateEditColumn("1M2W", 80)
                .SetLabel("2W");
            m1.AddDateEditColumn("1M3W", 80)
                .SetLabel("3W");
            m1.AddDateEditColumn("1M4W", 80)
                .SetLabel("4W");

            var m2 = grdSubsidiary.View.AddGroupColumn("2M");

            m2.AddDateEditColumn("2M1W", 80)
                .SetLabel("1W");
            m2.AddDateEditColumn("2M2W", 80)
                .SetLabel("2W");
            m2.AddDateEditColumn("2M3W", 80)
                .SetLabel("3W");
            m2.AddDateEditColumn("2M4W", 80)
                .SetLabel("4W");

            var m3 = grdSubsidiary.View.AddGroupColumn("3M");

            m3.AddDateEditColumn("3M1W", 80)
                .SetLabel("1W");
            m3.AddDateEditColumn("3M2W", 80)
                .SetLabel("2W");
            m3.AddDateEditColumn("3M3W", 80)
                .SetLabel("3W");
            m3.AddDateEditColumn("3M4W", 80)
                .SetLabel("4W");

            var m4 = grdSubsidiary.View.AddGroupColumn("4M");

            m4.AddDateEditColumn("4M1W", 80)
                .SetLabel("1W");
            m4.AddDateEditColumn("4M2W", 80)
                .SetLabel("2W");
            m4.AddDateEditColumn("4M3W", 80)
                .SetLabel("3W");
            m4.AddDateEditColumn("4M4W", 80)
                .SetLabel("4W");

            var m5 = grdSubsidiary.View.AddGroupColumn("5M");

            m5.AddDateEditColumn("5M1W", 80)
                .SetLabel("1W");
            m5.AddDateEditColumn("5M2W", 80)
                .SetLabel("2W");
            m5.AddDateEditColumn("5M3W", 80)
                .SetLabel("3W");
            m5.AddDateEditColumn("5M4W", 80)
                .SetLabel("4W");

            var m6 = grdSubsidiary.View.AddGroupColumn("6M");

            m6.AddDateEditColumn("6M1W", 80)
                .SetLabel("1W");
            m6.AddDateEditColumn("6M2W", 80)
                .SetLabel("2W");
            m6.AddDateEditColumn("6M3W", 80)
                .SetLabel("3W");
            m6.AddDateEditColumn("6M4W", 80)
                .SetLabel("4W");

            var m7 = grdSubsidiary.View.AddGroupColumn("7M");

            m7.AddDateEditColumn("7M1W", 80)
                .SetLabel("1W");
            m7.AddDateEditColumn("7M2W", 80)
                .SetLabel("2W");
            m7.AddDateEditColumn("7M3W", 80)
                .SetLabel("3W");
            m7.AddDateEditColumn("7M4W", 80)
                .SetLabel("4W");

            var m8 = grdSubsidiary.View.AddGroupColumn("8M");

            m8.AddDateEditColumn("8M1W", 80)
                .SetLabel("1W");
            m8.AddDateEditColumn("8M2W", 80)
                .SetLabel("2W");
            m8.AddDateEditColumn("8M3W", 80)
                .SetLabel("3W");
            m8.AddDateEditColumn("8M4W", 80)
                .SetLabel("4W");

            var m9 = grdSubsidiary.View.AddGroupColumn("9M");

            m9.AddDateEditColumn("9M1W", 80)
                .SetLabel("1W");
            m9.AddDateEditColumn("9M2W", 80)
                .SetLabel("2W");
            m9.AddDateEditColumn("9M3W", 80)
                .SetLabel("3W");
            m9.AddDateEditColumn("9M4W", 80)
                .SetLabel("4W");

            var m10 = grdSubsidiary.View.AddGroupColumn("10M");

            m10.AddDateEditColumn("10M1W", 80)
                .SetLabel("1W");
            m10.AddDateEditColumn("10M2W", 80)
                .SetLabel("2W");
            m10.AddDateEditColumn("10M3W", 80)
                .SetLabel("3W");
            m10.AddDateEditColumn("10M4W", 80)
                .SetLabel("4W");

            var m11 = grdSubsidiary.View.AddGroupColumn("11M");

            m11.AddDateEditColumn("11M1W", 80)
                .SetLabel("1W");
            m11.AddDateEditColumn("11M2W", 80)
                .SetLabel("2W");
            m11.AddDateEditColumn("11M3W", 80)
                .SetLabel("3W");
            m11.AddDateEditColumn("11M4W", 80)
                .SetLabel("4W");

            var m12 = grdSubsidiary.View.AddGroupColumn("12M");

            m12.AddDateEditColumn("12M1W", 80)
                .SetLabel("1W");
            m12.AddDateEditColumn("12M2W", 80)
                .SetLabel("2W");
            m12.AddDateEditColumn("12M3W", 80)
                .SetLabel("3W");
            m12.AddDateEditColumn("12M4W", 80)
                .SetLabel("4W");

            grdSubsidiary.View.PopulateColumns();

            RepositoryItemDateEdit date = new RepositoryItemDateEdit();
            date.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            date.Mask.EditMask = "MM-dd";
            date.Mask.UseMaskAsDisplayFormat = true;

            for (int i = 1; i <= 12; i++)
            {
                date.Tag = new DateTime(Convert.ToInt32(Convert.ToDateTime(this.Conditions.GetValue("P_YEAR")).Year), i, 1);

                for (int j = 1; j <= 4; j++)
                {
                    grdSubsidiary.View.Columns[i+"M"+j+"W"].ColumnEdit = date;
                }
            }

            grdSubsidiary.Caption = Language.Get("METERIALFIELD") + " [" + grdSubsidiary.View.RowCount + "]" + Language.Get("SEGMENTCOUNT");

            grdSubsidiary.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        /// <summary>        
        /// 반제품 부문 그리드 초기화
        /// </summary>
        private void InitializeSemiProductGrid()
        {
            grdSemiProduct.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdSemiProduct.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Copy | GridButtonItem.Export;

            var standardInfo = grdSemiProduct.View.AddGroupColumn("STANDARD");

            // Vendor Popup
            var vendorPopup = standardInfo.AddSelectPopupColumn("VENDORNAME", 180, new SqlQuery("GetVendorListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("VENDOR", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("VENDORNAME")
                .SetValidationKeyColumn()
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                    if (selectedRows.Count() < 1)
                    {
                        return;
                    }

                    foreach (DataRow row in selectedRows)
                    {
                        dataGridRow["VENDORNAME"] = row["VENDORNAME"].ToString();
                        dataGridRow["VENDORID"] = row["VENDORID"].ToString();
                    }
                });

            vendorPopup.Conditions.AddComboBox("PLANTID", new SqlQuery("GetPlantListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "PLANTID", "PLANTID")
                .SetDefault(UserInfo.Current.Plant);
            vendorPopup.Conditions.AddTextBox("VENDORIDNAME");

            vendorPopup.GridColumns.AddTextBoxColumn("PLANTID", 80);
            vendorPopup.GridColumns.AddTextBoxColumn("VENDORID", 80);
            vendorPopup.GridColumns.AddTextBoxColumn("VENDORNAME", 200);

            // Area Popup
            var areaPopup = standardInfo.AddSelectPopupColumn("AREANAME", 180, new SqlQuery("GetAreaListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(500, 560, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("AREAID")
                .SetValidationKeyColumn()
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    if (selectedRows.Count() < 1)
                    {
                        return;
                    }

                    foreach (DataRow row in selectedRows)
                    {
                        dataGridRow["AREANAME"] = row["AREANAME"].ToString();
                        dataGridRow["AREAID"] = row["AREAID"].ToString();
                    }
                });

            areaPopup.Conditions.AddTextBox("AREAIDNAME");
            areaPopup.Conditions.AddTextBox("P_VENDORID")
                .SetPopupDefaultByGridColumnId("VENDORID")
                .SetLabel("")
                .SetIsHidden();

            areaPopup.GridColumns.AddTextBoxColumn("AREAID", 150);
            areaPopup.GridColumns.AddTextBoxColumn("AREANAME", 200);

            // Processsegment Popup
            var largeProsegPopup = standardInfo.AddSelectPopupColumn("PROCESSSEGMENTNAME", 200, new SqlQuery("GetSmallProcesssegmentListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("PROCESSSEGMENTNAME", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("PROCESSSEGMENTID")
                .SetValidationKeyColumn()
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    if (selectedRows.Count() < 1)
                    {
                        return;
                    }

                    foreach (DataRow row in selectedRows)
                    {
                        dataGridRow["PROCESSSEGMENTNAME"] = row["PROCESSSEGMENTNAME"].ToString();
                        dataGridRow["PROCESSSEGMENTID"] = row["PROCESSSEGMENTID"].ToString();
                        dataGridRow["PROCESSSEGMENTVERSION"] = row["PROCESSSEGMENTVERSION"].ToString();
                    }
                });

            largeProsegPopup.Conditions.AddTextBox("PROCESSSEGMENTIDNAME");

            largeProsegPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            largeProsegPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            largeProsegPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTVERSION", 150)
                .SetIsHidden();

            standardInfo.AddTextBoxColumn("COMMENTS", 200)
                .SetLabel("UNIQUENESS");

            standardInfo.AddTextBoxColumn("VENDORID", 150)
                .SetIsHidden();
            standardInfo.AddTextBoxColumn("AREAID", 150)
                .SetIsHidden();
            standardInfo.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetIsHidden();
            standardInfo.AddTextBoxColumn("PROCESSSEGMENTVERSION", 150)
                .SetIsHidden();
            standardInfo.AddTextBoxColumn("YEAR", 150)
                .SetIsHidden();
            standardInfo.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();
            standardInfo.AddTextBoxColumn("PLANTID", 150)
                .SetIsHidden();

            var m1 = grdSemiProduct.View.AddGroupColumn("1M");

            m1.AddDateEditColumn("1M1W", 80)
                .SetLabel("1W");
            m1.AddDateEditColumn("1M2W", 80)
                .SetLabel("2W");
            m1.AddDateEditColumn("1M3W", 80)
                .SetLabel("3W");
            m1.AddDateEditColumn("1M4W", 80)
                .SetLabel("4W");

            var m2 = grdSemiProduct.View.AddGroupColumn("2M");

            m2.AddDateEditColumn("2M1W", 80)
                .SetLabel("1W");
            m2.AddDateEditColumn("2M2W", 80)
                .SetLabel("2W");
            m2.AddDateEditColumn("2M3W", 80)
                .SetLabel("3W");
            m2.AddDateEditColumn("2M4W", 80)
                .SetLabel("4W");

            var m3 = grdSemiProduct.View.AddGroupColumn("3M");

            m3.AddDateEditColumn("3M1W", 80)
                .SetLabel("1W");
            m3.AddDateEditColumn("3M2W", 80)
                .SetLabel("2W");
            m3.AddDateEditColumn("3M3W", 80)
                .SetLabel("3W");
            m3.AddDateEditColumn("3M4W", 80)
                .SetLabel("4W");

            var m4 = grdSemiProduct.View.AddGroupColumn("4M");

            m4.AddDateEditColumn("4M1W", 80)
                .SetLabel("1W");
            m4.AddDateEditColumn("4M2W", 80)
                .SetLabel("2W");
            m4.AddDateEditColumn("4M3W", 80)
                .SetLabel("3W");
            m4.AddDateEditColumn("4M4W", 80)
                .SetLabel("4W");

            var m5 = grdSemiProduct.View.AddGroupColumn("5M");

            m5.AddDateEditColumn("5M1W", 80)
                .SetLabel("1W");
            m5.AddDateEditColumn("5M2W", 80)
                .SetLabel("2W");
            m5.AddDateEditColumn("5M3W", 80)
                .SetLabel("3W");
            m5.AddDateEditColumn("5M4W", 80)
                .SetLabel("4W");

            var m6 = grdSemiProduct.View.AddGroupColumn("6M");

            m6.AddDateEditColumn("6M1W", 80)
                .SetLabel("1W");
            m6.AddDateEditColumn("6M2W", 80)
                .SetLabel("2W");
            m6.AddDateEditColumn("6M3W", 80)
                .SetLabel("3W");
            m6.AddDateEditColumn("6M4W", 80)
                .SetLabel("4W");

            var m7 = grdSemiProduct.View.AddGroupColumn("7M");

            m7.AddDateEditColumn("7M1W", 80)
                .SetLabel("1W");
            m7.AddDateEditColumn("7M2W", 80)
                .SetLabel("2W");
            m7.AddDateEditColumn("7M3W", 80)
                .SetLabel("3W");
            m7.AddDateEditColumn("7M4W", 80)
                .SetLabel("4W");

            var m8 = grdSemiProduct.View.AddGroupColumn("8M");

            m8.AddDateEditColumn("8M1W", 80)
                .SetLabel("1W");
            m8.AddDateEditColumn("8M2W", 80)
                .SetLabel("2W");
            m8.AddDateEditColumn("8M3W", 80)
                .SetLabel("3W");
            m8.AddDateEditColumn("8M4W", 80)
                .SetLabel("4W");

            var m9 = grdSemiProduct.View.AddGroupColumn("9M");

            m9.AddDateEditColumn("9M1W", 80)
                .SetLabel("1W");
            m9.AddDateEditColumn("9M2W", 80)
                .SetLabel("2W");
            m9.AddDateEditColumn("9M3W", 80)
                .SetLabel("3W");
            m9.AddDateEditColumn("9M4W", 80)
                .SetLabel("4W");

            var m10 = grdSemiProduct.View.AddGroupColumn("10M");

            m10.AddDateEditColumn("10M1W", 80)
                .SetLabel("1W");
            m10.AddDateEditColumn("10M2W", 80)
                .SetLabel("2W");
            m10.AddDateEditColumn("10M3W", 80)
                .SetLabel("3W");
            m10.AddDateEditColumn("10M4W", 80)
                .SetLabel("4W");

            var m11 = grdSemiProduct.View.AddGroupColumn("11M");

            m11.AddDateEditColumn("11M1W", 80)
                .SetLabel("1W");
            m11.AddDateEditColumn("11M2W", 80)
                .SetLabel("2W");
            m11.AddDateEditColumn("11M3W", 80)
                .SetLabel("3W");
            m11.AddDateEditColumn("11M4W", 80)
                .SetLabel("4W");

            var m12 = grdSemiProduct.View.AddGroupColumn("12M");

            m12.AddDateEditColumn("12M1W", 80)
                .SetLabel("1W");
            m12.AddDateEditColumn("12M2W", 80)
                .SetLabel("2W");
            m12.AddDateEditColumn("12M3W", 80)
                .SetLabel("3W");
            m12.AddDateEditColumn("12M4W", 80)
                .SetLabel("4W");

            grdSemiProduct.View.PopulateColumns();

            RepositoryItemDateEdit date = new RepositoryItemDateEdit();
            date.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            date.Mask.EditMask = "MM-dd";
            date.Mask.UseMaskAsDisplayFormat = true;

            for (int i = 1; i <= 12; i++)
            {
                date.Tag = new DateTime(Convert.ToInt32(Convert.ToDateTime(this.Conditions.GetValue("P_YEAR")).Year), i, 1);

                for (int j = 1; j <= 4; j++)
                {
                    grdSemiProduct.View.Columns[i + "M" + j + "W"].ColumnEdit = date;
                }         
            }

            grdSemiProduct.Caption = Language.Get("PRODUCTFIELD") + " [" + grdSemiProduct.View.RowCount + "]" + Language.Get("SEGMENTCOUNT");

            grdSemiProduct.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        /// <summary>        
        /// 점검시트 양식 그리드 초기화
        /// </summary>
        private void InitializeSheetFormGrid()
        {
            grdSheetForm.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdSheetForm.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Copy | GridButtonItem.Export;

            grdSheetForm.View.AddDateEditColumn("PRODUCTIONYEARMONTH", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetValidationKeyColumn(); // 제작년월

            // 대공정 Popup
            var prosegClassPopup = grdSheetForm.View.AddSelectPopupColumn("PROCESSSEGMENTCLASSNAME", 200, new SqlQuery("GetLargeProcesssegmentListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("LARGEPROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, true)
                .SetLabel("LARGEPROCESSSEGMENT")
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(400, 500, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("PROCESSSEGMENTCLASSID")
                .SetTextAlignment(TextAlignment.Center)
                .SetValidationKeyColumn()
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    if (selectedRows.Count() < 1)
                    {
                        return;
                    }

                    foreach (DataRow row in selectedRows)
                    {
                        dataGridRow["PROCESSSEGMENTCLASSNAME"] = row["PROCESSSEGMENTCLASSNAME"].ToString();
                        dataGridRow["PROCESSSEGMENTCLASSID"] = row["PROCESSSEGMENTCLASSID"].ToString();
                    }
                });

            prosegClassPopup.Conditions.AddTextBox("PROCESSSEGMENTCLASSIDNAME")
                .SetLabel("LARGEPROCESSSEGMENTIDNAME");

            prosegClassPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 150);
            prosegClassPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 150);

            grdSheetForm.View.AddTextBoxColumn("DESCRIPTION", 300); // 설명

            grdSheetForm.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 100)
                .SetIsHidden(); // 대공정 ID
            grdSheetForm.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden(); // Enterprise ID
            grdSheetForm.View.AddTextBoxColumn("PLANTID", 100)
                 .SetIsHidden(); // Plant ID

            grdSheetForm.View.PopulateColumns();

            RepositoryItemDateEdit date = new RepositoryItemDateEdit();
            date.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
            date.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            date.Mask.EditMask = "yyyy-MM";
            date.Mask.UseMaskAsDisplayFormat = true;

            grdSheetForm.View.Columns["PRODUCTIONYEARMONTH"].ColumnEdit = date;

            fileSheetForm.LanguageKey = "FILELIST";

            grdSheetForm.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            grdSheetForm.View.CellValueChanged += View_CellValueChanged;
            grdSubsidiary.View.CellValueChanged += View_CellValueChanged1;
            grdSemiProduct.View.CellValueChanged += View_CellValueChanged2;

            grdSheetForm.View.RowClick += View_RowClick;
            grdSheetForm.View.FocusedRowChanged += View_FocusedRowChanged;

            grdSubsidiary.View.AddingNewRow += View_AddingNewRow;
            grdSemiProduct.View.AddingNewRow += View_AddingNewRow;
            grdSheetForm.View.AddingNewRow += View_AddingNewRow;

            grdSubsidiary.View.RowCellClick += View_RowCellClick;
            grdSemiProduct.View.RowCellClick += View_RowCellClick;        
        }

        /// <summary>
        /// 그리드의 DateEdit에 접근하기 위한 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.ColumnEdit is RepositoryItemDateEdit date)
            {
                _col = e.Column.FieldName;
                date.QueryPopUp += Date_QueryPopUp;
            }
        }

        /// <summary>
        /// 그리드의 DateEdit 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Date_QueryPopUp(object sender, CancelEventArgs e)
        {
            string[] split = _col.Split('M');

            int month = Convert.ToInt32(split[0]);

            DateEdit edit = sender as DateEdit;
            edit.EditValue = new DateTime(DateTime.Now.Year, month, 1);
        }

        /// <summary>
        /// Row를 추가할때 로그인한 사용자의 Default Enterprise추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            args.NewRow["PLANTID"] = UserInfo.Current.Plant;
        }

        /// <summary>
        /// 제작년월의 대공정별로 저장된 파일을 검색한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grdSheetForm.View.GetFocusedDataRow() == null) return;

            if (grdSheetForm.View.GetFocusedDataRow().RowState == DataRowState.Unchanged)
            {
                pnlContent.ShowWaitArea();
                SearchAuditSheetFile();
                pnlContent.CloseWaitArea();

                fileSheetForm.Resource.Id = _productionYearMonth + _processsegmentClassId;
                fileSheetForm.Resource.Type = "AuditSheet";
                fileSheetForm.Resource.Version = "1";
                fileSheetForm.UploadPath = "AuditMgnt/AuditSheet";
            }
            else
            {
                _productionYearMonth = grdSheetForm.View.GetFocusedRowCellValue("PRODUCTIONYEARMONTH").ToString();
                _processsegmentClassId = grdSheetForm.View.GetFocusedRowCellValue("PROCESSSEGMENTCLASSID").ToString();
                fileSheetForm.ClearData();
            }
        }

        /// <summary>
        /// 제작년월의 대공정별로 저장된 파일을 검색한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (grdSheetForm.View.GetFocusedDataRow() == null) return;

            if (grdSheetForm.View.GetFocusedDataRow().RowState == DataRowState.Unchanged)
            {
                pnlContent.ShowWaitArea();
                SearchAuditSheetFile();
                pnlContent.CloseWaitArea();

                fileSheetForm.Resource.Id = _productionYearMonth + _processsegmentClassId;
                fileSheetForm.Resource.Type = "AuditSheet";
                fileSheetForm.Resource.Version = "1";
                fileSheetForm.UploadPath = "AuditMgnt/AuditSheet";
            }
            else
            {
                _productionYearMonth = grdSheetForm.View.GetFocusedRowCellValue("PRODUCTIONYEARMONTH").ToString();
                _processsegmentClassId = grdSheetForm.View.GetFocusedRowCellValue("PROCESSSEGMENTCLASSID").ToString();
                fileSheetForm.ClearData();
            }
        }

        /// <summary>
        /// 계획날짜를 MM-dd형식으로 지정한다. (반제품부문)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged2(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow row = grdSemiProduct.View.GetFocusedDataRow();

            if (e.Column.FieldName != "YEAR" && (e.Column.FieldName.Length == 4 || e.Column.FieldName.Length == 5))
            {
                if (!string.IsNullOrEmpty(e.Value.ToString()))
                {
                    row[e.Column.FieldName] = e.Value.ToString().Substring(5, 5);
                }
            }

            // 날짜가 바인딩됬을때 그 날짜가 해당 월의 주차인지 여부 체크
            if (e.Column.ColumnEdit is RepositoryItemDateEdit date)
            {
                if (e.Value == DBNull.Value)
                {
                    return;
                }

                string[] split = _col.Split('M');
                int columnDateWeekDegree = Convert.ToInt32(split[1].Replace("W", "")); // 컬럼 주차

                DateTime selectDate = Convert.ToDateTime(e.Value); // 선택된 날짜
                int selectDateWeekDegree = GetCurrentWeekOfMonth(CultureInfo.GetCultureInfo("ko-KR"), selectDate); // 선택된 날짜의 주차

                // 선택된 일의 주차가 4이하라면 
                if (selectDateWeekDegree < 4)
                {
                    // 선택된 일의 주차가 컬럼의 주차와 일치하지 않는다면 Exception
                    if (selectDateWeekDegree != columnDateWeekDegree)
                    {                     
                        this.ShowMessage("NoMatchWeekDay");
                        grdSemiProduct.View.CellValueChanged -= View_CellValueChanged2;
                        grdSemiProduct.View.SetFocusedRowCellValue(_col, null);
                        grdSemiProduct.View.CellValueChanged += View_CellValueChanged2;
                    }
                }
                else
                {
                    // 컬럼의 주차가 4라면 일치하는것으로 간주
                    if (columnDateWeekDegree == 4)
                    {
                        return;
                    }
                    else
                    {
                        this.ShowMessage("NoMatchWeekDay");
                        grdSemiProduct.View.CellValueChanged -= View_CellValueChanged2;
                        grdSemiProduct.View.SetFocusedRowCellValue(_col, null);
                        grdSemiProduct.View.CellValueChanged += View_CellValueChanged2;
                    }
                }
            }
        }

        /// <summary>
        /// 계획날짜를 MM-dd형식으로 지정한다. (부자재부문)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged1(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow row = grdSubsidiary.View.GetFocusedDataRow();

            if (e.Column.FieldName != "YEAR" && (e.Column.FieldName.Length == 4 || e.Column.FieldName.Length == 5))
            {
                if (!string.IsNullOrEmpty(e.Value.ToString()))
                {
                    row[e.Column.FieldName] = e.Value.ToString().Substring(5, 5);
                }
            }

            // 날짜가 바인딩됬을때 그 날짜가 해당 월의 주차인지 여부 체크
            if (e.Column.ColumnEdit is RepositoryItemDateEdit date)
            {
                if (e.Value == DBNull.Value)
                {
                    return;
                }

                string[] split = _col.Split('M');
                int columnDateWeekDegree = Convert.ToInt32(split[1].Replace("W", "")); // 컬럼 주차

                DateTime selectDate = Convert.ToDateTime(e.Value); // 선택된 날짜
                int selectDateWeekDegree = GetCurrentWeekOfMonth(CultureInfo.GetCultureInfo("ko-KR"), selectDate); // 선택된 날짜의 주차

                // 선택된 일의 주차가 4이하라면 
                if (selectDateWeekDegree < 4)
                {
                    // 선택된 일의 주차가 컬럼의 주차와 일치하지 않는다면 Exception
                    if (selectDateWeekDegree != columnDateWeekDegree)
                    {
                        this.ShowMessage("NoMatchWeekDay");
                        grdSubsidiary.View.CellValueChanged -= View_CellValueChanged1;
                        grdSubsidiary.View.SetFocusedRowCellValue(_col, null);
                        grdSubsidiary.View.CellValueChanged += View_CellValueChanged1;
                    }
                }
                else
                {
                    // 컬럼의 주차가 4라면 일치하는것으로 간주
                    if (columnDateWeekDegree == 4)
                    {
                        return;
                    }
                    else
                    {
                        this.ShowMessage("NoMatchWeekDay");
                        grdSubsidiary.View.CellValueChanged -= View_CellValueChanged1;
                        grdSubsidiary.View.SetFocusedRowCellValue(_col, null);
                        grdSubsidiary.View.CellValueChanged += View_CellValueChanged1;
                    }
                }
            }
        }

        /// <summary>
        /// 제작년월을 yyyy-MM형식으로 지정한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow row = grdSheetForm.View.GetFocusedDataRow();

            if (e.Column.FieldName == "PRODUCTIONYEARMONTH")
            {
                if (!string.IsNullOrEmpty(e.Value.ToString()))
                {
                    row["PRODUCTIONYEARMONTH"] = e.Value.ToString().Substring(0, 7);
                }
            }
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            var values = Conditions.GetValues();

            // Audit 계획탭일때 저장버튼 로직
            if (tabAuditSchedule.SelectedTabPage.Name == "tpgAuditSchedule")
            {
                DataSet ds = new DataSet();

                if (grdSubsidiary.DataSource != null)
                {
                    if (grdSubsidiary.GetChangedRows().Rows.Count != 0)
                    {
                        // 부자재부문일때 
                        DataTable changed1 = grdSubsidiary.GetChangedRows();
                        DataTable changed1Clone = changed1.Clone();

                        changed1Clone.Columns.Remove("VENDORNAME");
                        changed1Clone.Columns.Remove("AREANAME");
                        //changed1Clone.Columns.Remove("CONSUMABLEDEFNAME");
                        changed1Clone.Columns.Remove("COMMENTS");
                        changed1Clone.Columns.Remove("VENDORID");
                        changed1Clone.Columns.Remove("AREAID");
                        changed1Clone.Columns.Remove("CONSUMABLEDEFID");
                        changed1Clone.Columns.Remove("CONSUMABLEDEFVERSION");
                        changed1Clone.Columns.Remove("YEAR");
                        changed1Clone.Columns.Remove("ENTERPRISEID");
                        changed1Clone.Columns.Remove("PLANTID");
                        changed1Clone.Columns.Remove("REQUESTNO");

                        DataTable hiddenChanged1 = new DataTable();
                        hiddenChanged1.TableName = "list1";

                        hiddenChanged1.Columns.Add("VENDORID", typeof(string));
                        hiddenChanged1.Columns.Add("AREAID", typeof(string));
                        hiddenChanged1.Columns.Add("CONSUMABLEDEFID", typeof(string));
                        hiddenChanged1.Columns.Add("CONSUMABLEDEFVERSION", typeof(string));
                        hiddenChanged1.Columns.Add("YEAR", typeof(string));
                        hiddenChanged1.Columns.Add("MONTH", typeof(string));
                        hiddenChanged1.Columns.Add("WEEK", typeof(string));
                        hiddenChanged1.Columns.Add("COMMENTS", typeof(string));
                        hiddenChanged1.Columns.Add("PLANDATE", typeof(DateTime));
                        hiddenChanged1.Columns.Add("ENTERPRISEID");
                        hiddenChanged1.Columns.Add("PLANTID");
                        hiddenChanged1.Columns.Add("_STATE_", typeof(string));
                        hiddenChanged1.Columns.Add("REQUESTNO");

                        // Pivot으로 보여지는 데이터를 Row단위로 수정
                        foreach (DataRow row in changed1.Rows)
                        {
                            foreach (DataColumn col in changed1Clone.Columns)
                            {
                                string[] monthWeek = col.ColumnName.Split('M');
                                DataRow rowNew = hiddenChanged1.NewRow();

                                rowNew["VENDORID"] = row["VENDORID"];
                                rowNew["AREAID"] = row["AREAID"];
                                rowNew["CONSUMABLEDEFID"] = row["CONSUMABLEDEFID"];
                                rowNew["CONSUMABLEDEFVERSION"] = row["CONSUMABLEDEFVERSION"];
                                rowNew["COMMENTS"] = row["COMMENTS"];
                                rowNew["YEAR"] = Convert.ToDateTime(values["P_YEAR"]).Year.ToString();
                                rowNew["ENTERPRISEID"] = row["ENTERPRISEID"];
                                rowNew["PLANTID"] = row["PLANTID"];
                                rowNew["_STATE_"] = row["_STATE_"];
                                rowNew["REQUESTNO"] = row["REQUESTNO"];

                                if (!col.ColumnName.Equals("_STATE_"))
                                {
                                    rowNew["MONTH"] = monthWeek[0];
                                    rowNew["WEEK"] = monthWeek[1].Replace("W", "");
                                }

                                if (row[col.ColumnName] != DBNull.Value && !col.ColumnName.Equals("_STATE_"))
                                {
                                    rowNew["PLANDATE"] = Convert.ToDateTime(row[col.ColumnName]);
                                    hiddenChanged1.Rows.Add(rowNew);
                                }
                            }
                        }

                        // 계획일자는 최소 1개이상 입력되야합니다.
                        if (hiddenChanged1.Rows.Count == 0)
                        {
                            throw MessageException.Create("PlandateIsRequired");
                        }
                        else
                        {
                            ds.Tables.Add(hiddenChanged1.Copy());
                        }
                    }
                }

                if (grdSemiProduct.DataSource != null)
                {
                    if (grdSemiProduct.GetChangedRows().Rows.Count != 0)
                    {
                        // 반제품부문일때
                        DataTable changed2 = grdSemiProduct.GetChangedRows();
                        DataTable changed2Clone = changed2.Clone();

                        changed2Clone.Columns.Remove("VENDORNAME");
                        changed2Clone.Columns.Remove("AREANAME");
                        changed2Clone.Columns.Remove("PROCESSSEGMENTNAME");
                        changed2Clone.Columns.Remove("COMMENTS");
                        changed2Clone.Columns.Remove("VENDORID");
                        changed2Clone.Columns.Remove("AREAID");
                        changed2Clone.Columns.Remove("PROCESSSEGMENTID");
                        changed2Clone.Columns.Remove("PROCESSSEGMENTVERSION");
                        changed2Clone.Columns.Remove("YEAR");
                        changed2Clone.Columns.Remove("ENTERPRISEID");
                        changed2Clone.Columns.Remove("PLANTID");

                        DataTable hiddenChanged2 = new DataTable();
                        hiddenChanged2.TableName = "list2";

                        hiddenChanged2.Columns.Add("VENDORID", typeof(string));
                        hiddenChanged2.Columns.Add("AREAID", typeof(string));
                        hiddenChanged2.Columns.Add("PROCESSSEGMENTID", typeof(string));
                        hiddenChanged2.Columns.Add("PROCESSSEGMENTVERSION", typeof(string));
                        hiddenChanged2.Columns.Add("YEAR", typeof(string));
                        hiddenChanged2.Columns.Add("MONTH", typeof(string));
                        hiddenChanged2.Columns.Add("WEEK", typeof(string));
                        hiddenChanged2.Columns.Add("COMMENTS", typeof(string));
                        hiddenChanged2.Columns.Add("PLANDATE", typeof(DateTime));
                        hiddenChanged2.Columns.Add("ENTERPRISEID");
                        hiddenChanged2.Columns.Add("PLANTID");
                        hiddenChanged2.Columns.Add("_STATE_", typeof(string));

                        // Pivot으로 보여지는 데이터를 Row단위로 수정
                        foreach (DataRow row in changed2.Rows)
                        {
                            foreach (DataColumn col in changed2Clone.Columns)
                            {
                                string[] monthWeek = col.ColumnName.Split('M');
                                DataRow rowNew = hiddenChanged2.NewRow();

                                rowNew["VENDORID"] = row["VENDORID"];
                                rowNew["AREAID"] = row["AREAID"];
                                rowNew["PROCESSSEGMENTID"] = row["PROCESSSEGMENTID"];
                                rowNew["PROCESSSEGMENTVERSION"] = row["PROCESSSEGMENTVERSION"];
                                rowNew["COMMENTS"] = row["COMMENTS"];
                                rowNew["YEAR"] = Convert.ToDateTime(values["P_YEAR"]).Year.ToString();
                                rowNew["ENTERPRISEID"] = row["ENTERPRISEID"];
                                rowNew["PLANTID"] = row["PLANTID"];
                                rowNew["_STATE_"] = row["_STATE_"];

                                if (!col.ColumnName.Equals("_STATE_"))
                                {
                                    rowNew["MONTH"] = monthWeek[0];
                                    rowNew["WEEK"] = monthWeek[1].Replace("W", "");
                                }

                                if (row[col.ColumnName] != DBNull.Value && !col.ColumnName.Equals("_STATE_"))
                                {
                                    rowNew["PLANDATE"] = Convert.ToDateTime(row[col.ColumnName]);
                                    hiddenChanged2.Rows.Add(rowNew);
                                }
                            }
                        }

                        // 계획일자는 최소 1개이상 입력되야합니다.
                        if (hiddenChanged2.Rows.Count == 0)
                        {
                            throw MessageException.Create("PlandateIsRequired");
                        }
                        else
                        {
                            ds.Tables.Add(hiddenChanged2.Copy());
                        }
                    }
                }

                this.ExecuteRule("SaveAuditSchedule", ds);               
            }

            // 점검시트 양식탭일때 저장버튼 로직
            else
            {
                DataTable changed = grdSheetForm.GetChangedRows();
                DataTable fileChaned = fileSheetForm.GetChangedRows();

                fileChaned.TableName = "fileList";

                // 저장할 파일데이터가 있다면 서버에 업로드한다.
                if (fileChaned.Rows.Count != 0)
                {
                    int chkAdded = 0;

                    foreach (DataRow row in fileChaned.Rows)
                    {
                        if (row["_STATE_"].ToString() == "added")
                        {
                            chkAdded++;
                        }

                        row["RESOURCEID"] = _productionYearMonth + _processsegmentClassId;
                        row["RESOURCETYPE"] = "AuditSheet";
                        row["RESOURCEVERSION"] = "1";
                        row["FILEPATH"] = "AuditMgnt/AuditSheet";
                    }

                    if (chkAdded > 0)
                    {
                        fileSheetForm.SaveChangedFiles();
                    }
                }

                DataSet ds = new DataSet();
                ds.Tables.Add(changed.Copy());
                ds.Tables.Add(fileChaned.Copy());

                this.ExecuteRule("SaveAuditSheet", ds);
                fileSheetForm.BeginInvoke(new MethodInvoker(() => { SearchAuditSheetFile(); }));
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

            var values = Conditions.GetValues();
            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
            values["P_YEAR"] = Convert.ToDateTime(values["P_YEAR"]).Year;

            if (tabAuditSchedule.SelectedTabPage.Name == "tpgAuditSchedule")
            {
                DataTable dt = await SqlExecuter.QueryAsync("GetAuditScheduleByMaterial", "10001", values);
                DataTable dt2 = await SqlExecuter.QueryAsync("GetAuditScheduleByProduct", "10001", values);

                if (dt.Rows.Count == 0 && dt2.Rows.Count == 0)
                {
                    // 조회할 데이터가 없습니다.
                    this.ShowMessage("NoSelectData");
                    grdSubsidiary.DataSource = null;
                    grdSemiProduct.DataSource = null;
                    return;
                }

                grdSubsidiary.DataSource = PivotMaterialDataTable(dt);
                grdSemiProduct.DataSource = PivotProductDataTable(dt2);

                grdSubsidiary.Caption = Language.Get("METERIALFIELD") + " [" + grdSubsidiary.View.RowCount + "]" + Language.Get("SEGMENTCOUNT");
                grdSemiProduct.Caption = Language.Get("PRODUCTFIELD") + " [" + grdSemiProduct.View.RowCount + "]" + Language.Get("SEGMENTCOUNT");
            }
            else
            {
                DataTable dt = await SqlExecuter.QueryAsync("GetAuditScheduleSheet", "10001", values);

                if (dt.Rows.Count == 0)
                {
                    // 조회할 데이터가 없습니다.
                    this.ShowMessage("NoSelectData");
                    grdSheetForm.DataSource = null;
                    fileSheetForm.ClearData();
                    return;
                }

                grdSheetForm.DataSource = dt;
            }
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

            // 조회조건에 구성된 Control에 대한 처리가 필요한 경우
            SmartDateEdit dateEdit = Conditions.GetControl<SmartDateEdit>("p_year");
            dateEdit.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearsGroupView;
            dateEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            dateEdit.Properties.Mask.EditMask = "yyyy";
            dateEdit.EditValue = DateTime.Now;
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            if (tabAuditSchedule.SelectedTabPage.Name == "tpgAuditSchedule")
            {
                grdSubsidiary.View.CheckValidation();
                grdSemiProduct.View.CheckValidation();

                DataTable changed1 = new DataTable();
                DataTable changed2 = new DataTable();

                if (grdSubsidiary.DataSource != null)
                {
                    changed1 = grdSubsidiary.GetChangedRows();
                }
                 
                if (grdSemiProduct.DataSource != null)
                {
                    changed2 = grdSemiProduct.GetChangedRows();
                }

                if (changed1.Rows.Count == 0 && changed2.Rows.Count == 0)
                {
                    // 저장할 데이터가 존재하지 않습니다.
                    throw MessageException.Create("NoSaveData");
                }
            }
            else
            {
                grdSheetForm.View.CheckValidation();

                DataTable changed1 = grdSheetForm.GetChangedRows();
                DataTable changed2 = fileSheetForm.GetChangedRows();

                if (changed1.Rows.Count == 0 && changed2.Rows.Count == 0)
                {
                    // 저장할 데이터가 존재하지 않습니다.
                    throw MessageException.Create("NoSaveData");
                }
            }
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 부자재부문 데이터를 검색해와서 Pivot형태로 변환한다.
        /// </summary>
        private DataTable PivotMaterialDataTable(DataTable dt)
        {
            // Column 생성
            for (int i = 1; i <= 12; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    dt.Columns.Add(i+"M"+j+"W", typeof(string));
                }
            }

            // 중복을 제외한 테이블
            DataTable pivot = dt.Copy();
            pivot.Columns.Remove("MONTH");
            pivot.Columns.Remove("WEEK");
            pivot.Columns.Remove("PLANDATE");
            pivot = pivot.DefaultView.ToTable(true);

            foreach (DataRow row in pivot.Rows)
            {
                // 2020.04.08-유석진-자재번호는 key에서 제외 RequestNo 추가로 인한 조건 변경
                DataTable pivotDetail = dt.Rows.Cast<DataRow>().Where(r => r["VENDORID"].Equals(row["VENDORID"])
                                                                        && r["AREAID"].Equals(row["AREAID"])
                                                                        && r["REQUESTNO"].Equals(row["REQUESTNO"])
                                                                        //&& r["CONSUMABLEDEFID"].Equals(row["CONSUMABLEDEFID"])
                                                                        //&& r["CONSUMABLEDEFVERSION"].Equals(row["CONSUMABLEDEFVERSION"])
                                                                        && r["YEAR"].Equals(row["YEAR"])
                                                                        && r["COMMENTS"].Equals(row["COMMENTS"])).CopyToDataTable();

                // 실제 PivotTable에 데이터 삽입
                for (int i = 0; i < pivotDetail.Rows.Count; i++)
                {
                    string month = pivotDetail.Rows[i]["MONTH"].ToString();
                    string week = pivotDetail.Rows[i]["WEEK"].ToString();

                    row["VENDORID"] = pivotDetail.Rows[i]["VENDORID"];
                    row["VENDORNAME"] = pivotDetail.Rows[i]["VENDORNAME"];
                    row["AREAID"] = pivotDetail.Rows[i]["AREAID"];
                    row["AREANAME"] = pivotDetail.Rows[i]["AREANAME"];
                    row["CONSUMABLEDEFID"] = pivotDetail.Rows[i]["CONSUMABLEDEFID"];
                    row["CONSUMABLEDEFVERSION"] = pivotDetail.Rows[i]["CONSUMABLEDEFVERSION"];
                    row["CONSUMABLEDEFNAME"] = pivotDetail.Rows[i]["CONSUMABLEDEFNAME"];
                    row["YEAR"] = pivotDetail.Rows[i]["YEAR"];
                    row["COMMENTS"] = pivotDetail.Rows[i]["COMMENTS"];
                    row[month + "M" + week + "W"] = pivotDetail.Rows[i]["PLANDATE"];
                }
            }          

            return pivot;
        }

        /// <summary>
        /// 반제품부문 데이터를 검색해와서 Pivot형태로 변환한다.
        /// </summary>
        private DataTable PivotProductDataTable(DataTable dt)
        {
            // Column 생성
            for (int i = 1; i <= 12; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    dt.Columns.Add(i + "M" + j + "W", typeof(string));
                }
            }

            // 중복을 제외한 테이블
            DataTable pivot = dt.Copy();
            pivot.Columns.Remove("MONTH");
            pivot.Columns.Remove("WEEK");
            pivot.Columns.Remove("PLANDATE");
            pivot = pivot.DefaultView.ToTable(true);

            foreach (DataRow row in pivot.Rows)
            {
                DataTable pivotDetail = dt.Rows.Cast<DataRow>().Where(r => r["VENDORID"].Equals(row["VENDORID"])
                                                                        && r["AREAID"].Equals(row["AREAID"])
                                                                        && r["PROCESSSEGMENTID"].Equals(row["PROCESSSEGMENTID"])
                                                                        && r["PROCESSSEGMENTVERSION"].Equals(row["PROCESSSEGMENTVERSION"])
                                                                        && r["YEAR"].Equals(row["YEAR"])
                                                                        && r["COMMENTS"].Equals(row["COMMENTS"])).CopyToDataTable();

                // 실제 PivotTable에 데이터 삽입
                for (int i = 0; i < pivotDetail.Rows.Count; i++)
                {
                    string month = pivotDetail.Rows[i]["MONTH"].ToString();
                    string week = pivotDetail.Rows[i]["WEEK"].ToString();

                    row["VENDORID"] = pivotDetail.Rows[i]["VENDORID"];
                    row["VENDORNAME"] = pivotDetail.Rows[i]["VENDORNAME"];
                    row["AREAID"] = pivotDetail.Rows[i]["AREAID"];
                    row["AREANAME"] = pivotDetail.Rows[i]["AREANAME"];
                    row["PROCESSSEGMENTID"] = pivotDetail.Rows[i]["PROCESSSEGMENTID"];
                    row["PROCESSSEGMENTVERSION"] = pivotDetail.Rows[i]["PROCESSSEGMENTVERSION"];
                    row["PROCESSSEGMENTNAME"] = pivotDetail.Rows[i]["PROCESSSEGMENTNAME"];
                    row["YEAR"] = pivotDetail.Rows[i]["YEAR"];
                    row["COMMENTS"] = pivotDetail.Rows[i]["COMMENTS"];
                    row[month + "M" + week + "W"] = pivotDetail.Rows[i]["PLANDATE"];
                }
            }

            return pivot;
        }

        /// <summary>
        /// Audit 점검시트별로 등록된 파일정보를 조회한다.
        /// </summary>
        private void SearchAuditSheetFile()
        {
            if ((grdSheetForm.DataSource as DataTable).Rows.Count == 0) return;

            _productionYearMonth = grdSheetForm.View.GetFocusedRowCellValue("PRODUCTIONYEARMONTH").ToString();
            _processsegmentClassId = grdSheetForm.View.GetFocusedRowCellValue("PROCESSSEGMENTCLASSID").ToString();

            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                { "RESOURCEID", _productionYearMonth + _processsegmentClassId},
                { "RESOURCETYPE", "AuditSheet"},
                { "RESOURCEVERSION", "1"}
            };

            DataTable dt = SqlExecuter.Query("GetAuditSheetFile", "10001", param);

            if (dt.Rows.Count == 0)
            {
                fileSheetForm.ClearData();
                return;
            }

            fileSheetForm.DataSource = dt;
        }

        /// <summary>
        /// 현재일이 포함된 년중 주와 현재달의 첫째날의 년중 주를 구하여 차로서 해당일의 월간 주간을 알아낼 수 있다.
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="selectDate"></param>
        /// <returns></returns>
        public int GetCurrentWeekOfMonth(CultureInfo culture, DateTime selectDate)
        {
            DateTime firstDayOfMonth = System.DateTime.Parse(selectDate.ToString("yyyy-MM-01"));
            int firstWeekOfMonth = GetWeekOfYear(firstDayOfMonth, culture);
            int nowWeekOfMonth = GetWeekOfYear(selectDate, culture);

            return (nowWeekOfMonth - firstWeekOfMonth) + 1;
        }

        public int GetWeekOfYear(DateTime targetDate)
        {
            return GetWeekOfYear(targetDate, null);
        }

        /// <summary>
        /// 주어진 날짜가 1년 중 몇 번째 주(week)인가를 반환한다.
        /// 달력 규칙은 매개변수로 주어진 CultureInfo를 사용한다.
        /// </summary>
        /// <param name="targetDate"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public int GetWeekOfYear(DateTime targetDate, CultureInfo culture)
        {
            if (culture == null)
            {
                culture = CultureInfo.CurrentCulture;
            }
            CalendarWeekRule weekRule = culture.DateTimeFormat.CalendarWeekRule;
            DayOfWeek firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;
            return culture.Calendar.GetWeekOfYear(targetDate, weekRule, firstDayOfWeek);
        }

        /// <summary>
        /// 년, 월을 입력하면 해당 월의 일수를 구해주는 함수
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="wkstart"></param>
        /// <returns></returns>
        public int Weeks(int year, int month, DayOfWeek wkstart)
        {

            DateTime first = new DateTime(year, month, 1);
            int firstwkday = (int)first.DayOfWeek;

            int otherwkday = (int)wkstart;
            int offset = ((otherwkday + 7) - firstwkday) % 7;

            double weeks = (double)(DateTime.DaysInMonth(year, month) - offset) / 7d;
            return (int)Math.Ceiling(weeks);
        }

        #endregion
    }
}
