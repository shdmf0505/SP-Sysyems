#region using

using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > Audit 관리 > Audit 관리대장
    /// 업  무  설  명  : 협력업체 관리대장 현황 을 조회하여 점검결과를 등록한다.
    /// 생    성    자  : 유호석
    /// 생    성    일  : 2019-08-09
    /// 수  정  이  력  :
    ///     1. 2021.01.19 전우성 : 소스 전면 정리 및 수정
    ///
    /// </summary>
    public partial class AuditManagementledger : SmartConditionManualBaseForm
    {
        #region 생성자

        public AuditManagementledger()
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
            InitializeGrid();
        }

        /// <summary>
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            grdAudit.GridButtonItem = GridButtonItem.Export;
            grdAudit.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            var group = grdAudit.View.AddGroupColumn("AUDITMANAGEVENDOR");
            group.AddTextBoxColumn("VENDORNAME", 200);
            group.AddTextBoxColumn("AREANAME", 200);
            group.AddComboBoxColumn("AUDITTYPE", 80, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=AuditType"))
                 .SetLabel("CLASS").SetTextAlignment(TextAlignment.Center);

            group = grdAudit.View.AddGroupColumn("PREQUARTER");
            group.AddSpinEditColumn("PRE_MONTHLYSALESAMOUNT", 100).SetTextAlignment(TextAlignment.Right).SetLabel("MONTHLYSALESAMOUNT");

            group = grdAudit.View.AddGroupColumn("FIRSTQUARTER");
            group.AddTextBoxColumn("INSPECTIONPLANMONTHWEEK_1", 100).SetTextAlignment(TextAlignment.Center).SetLabel("INSPECTIONPLANMONTHWEEK");
            group.AddTextBoxColumn("INSPECTIONDATE_1", 100).SetTextAlignment(TextAlignment.Center).SetLabel("AUDITDATE");
            group.AddSpinEditColumn("INSPECTIONRESULT_1", 100).SetTextAlignment(TextAlignment.Right).SetLabel("EXAMINRESULT");
            group.AddTextBoxColumn("QUALITYWARNINGLETTER_1", 100).SetLabel("QUALITYWARNINGLETTER");
            group.AddTextBoxColumn("THINKINGPERSONALITY_1", 100).SetLabel("THINKINGPERSONALITY");
            group.AddTextBoxColumn("NCRDESCRIPTION_1", 100).SetLabel("NCRDESCRIPTION");
            group.AddTextBoxColumn("REENACTIVATION_1", 100).SetLabel("REENACTIVATION");
            group.AddTextBoxColumn("GENERALJUDGMENT_1", 100).SetLabel("GENERALJUDGMENT");
            group.AddSpinEditColumn("MONTHLYSALESAMOUNT_1", 100).SetTextAlignment(TextAlignment.Right).SetLabel("MONTHLYSALESAMOUNT");
            group.AddTextBoxColumn("ISOPRATIONSTOP_1", 100).SetTextAlignment(TextAlignment.Center).SetLabel("ISOPRATIONSTOP");

            group = grdAudit.View.AddGroupColumn("SECONDQUARTER");
            group.AddTextBoxColumn("INSPECTIONPLANMONTHWEEK_2", 100).SetTextAlignment(TextAlignment.Center).SetLabel("INSPECTIONPLANMONTHWEEK");
            group.AddTextBoxColumn("INSPECTIONDATE_2", 100).SetTextAlignment(TextAlignment.Center).SetLabel("AUDITDATE");
            group.AddSpinEditColumn("INSPECTIONRESULT_2", 100).SetTextAlignment(TextAlignment.Right).SetLabel("EXAMINRESULT");
            group.AddTextBoxColumn("QUALITYWARNINGLETTER_2", 100).SetLabel("QUALITYWARNINGLETTER");
            group.AddTextBoxColumn("THINKINGPERSONALITY_2", 100).SetLabel("THINKINGPERSONALITY");
            group.AddTextBoxColumn("NCRDESCRIPTION_2", 100).SetLabel("NCRDESCRIPTION");
            group.AddTextBoxColumn("REENACTIVATION_2", 100).SetLabel("REENACTIVATION");
            group.AddTextBoxColumn("GENERALJUDGMENT_2", 100).SetLabel("GENERALJUDGMENT");
            group.AddSpinEditColumn("MONTHLYSALESAMOUNT_2", 100).SetTextAlignment(TextAlignment.Right).SetLabel("MONTHLYSALESAMOUNT");
            group.AddTextBoxColumn("ISOPRATIONSTOP_2", 100).SetTextAlignment(TextAlignment.Center).SetLabel("ISOPRATIONSTOP");

            group = grdAudit.View.AddGroupColumn("THIRDQUARTER");
            group.AddTextBoxColumn("INSPECTIONPLANMONTHWEEK_3", 100).SetTextAlignment(TextAlignment.Center).SetLabel("INSPECTIONPLANMONTHWEEK");
            group.AddTextBoxColumn("INSPECTIONDATE_3", 100).SetTextAlignment(TextAlignment.Center).SetLabel("AUDITDATE");
            group.AddSpinEditColumn("INSPECTIONRESULT_3", 100).SetTextAlignment(TextAlignment.Right).SetLabel("EXAMINRESULT");
            group.AddTextBoxColumn("QUALITYWARNINGLETTER_3", 100).SetLabel("QUALITYWARNINGLETTER");
            group.AddTextBoxColumn("THINKINGPERSONALITY_3", 100).SetLabel("THINKINGPERSONALITY");
            group.AddTextBoxColumn("NCRDESCRIPTION_3", 100).SetLabel("NCRDESCRIPTION");
            group.AddTextBoxColumn("REENACTIVATION_3", 100).SetLabel("REENACTIVATION");
            group.AddTextBoxColumn("GENERALJUDGMENT_3", 100).SetLabel("GENERALJUDGMENT");
            group.AddSpinEditColumn("MONTHLYSALESAMOUNT_3", 100).SetTextAlignment(TextAlignment.Right).SetLabel("MONTHLYSALESAMOUNT");
            group.AddTextBoxColumn("ISOPRATIONSTOP_3", 100).SetTextAlignment(TextAlignment.Center).SetLabel("ISOPRATIONSTOP");

            group = grdAudit.View.AddGroupColumn("FOURTHQUARTER");
            group.AddTextBoxColumn("INSPECTIONPLANMONTHWEEK_4", 100).SetTextAlignment(TextAlignment.Center).SetLabel("INSPECTIONPLANMONTHWEEK");
            group.AddTextBoxColumn("INSPECTIONDATE_4", 100).SetTextAlignment(TextAlignment.Center).SetLabel("AUDITDATE");
            group.AddSpinEditColumn("INSPECTIONRESULT_4", 100).SetTextAlignment(TextAlignment.Right).SetLabel("EXAMINRESULT");
            group.AddTextBoxColumn("QUALITYWARNINGLETTER_4", 100).SetLabel("QUALITYWARNINGLETTER");
            group.AddTextBoxColumn("THINKINGPERSONALITY_4", 100).SetLabel("THINKINGPERSONALITY");
            group.AddTextBoxColumn("NCRDESCRIPTION_4", 100).SetLabel("NCRDESCRIPTION");
            group.AddTextBoxColumn("REENACTIVATION_4", 100).SetLabel("REENACTIVATION");
            group.AddTextBoxColumn("GENERALJUDGMENT_4", 100).SetLabel("GENERALJUDGMENT");
            group.AddSpinEditColumn("MONTHLYSALESAMOUNT_4", 100).SetTextAlignment(TextAlignment.Right).SetLabel("MONTHLYSALESAMOUNT");
            group.AddTextBoxColumn("ISOPRATIONSTOP_4", 100).SetTextAlignment(TextAlignment.Center).SetLabel("ISOPRATIONSTOP");

            grdAudit.View.PopulateColumns();
            grdAudit.View.SetIsReadOnly();
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            grdAudit.View.DoubleClick += (s, e) =>
            {
                if (grdAudit.View.FocusedRowHandle < 0)
                {
                    return;
                }

                GridHitInfo info = (s as GridView).CalcHitInfo((e as DXMouseEventArgs).Location);

                if(!info.InRowCell)
                {
                    return;
                }

                DataRow row = grdAudit.View.GetFocusedDataRow();

                if (info.Column.FieldName.Equals("VENDORNAME") || info.Column.FieldName.Equals("AREANAME")
                    || info.Column.FieldName.Equals("AUDITTYPE") || info.Column.FieldName.Equals("INSPECTIONDATE")
                    || info.Column.FieldName.Equals("EXAMINRESULT") || info.Column.FieldName.Equals("ISOPRATIONSTOP"))
                {
                    AuditManageRegistPopup popup = new AuditManageRegistPopup(row["ENTERPRISEID"].ToString(), row["PLANTID"].ToString(), row["VENDORID"].ToString(), row["AREAID"].ToString(), row["VENDORNAME"].ToString(), row["AREANAME"].ToString())
                    {
                        StartPosition = FormStartPosition.CenterParent,
                        Owner = this
                    };
                    popup.btnSave.Enabled = btnFlag.Enabled;

                    if (popup.ShowDialog().Equals(DialogResult.OK))
                    {
                        Popup_FormClosed();
                    }
                }
                else
                {
                    string quarter = string.Empty;

                    if (info.Column.FieldName.Equals("INSPECTIONPLANMONTHWEEK_1") || info.Column.FieldName.Equals("INSPECTIONDATE_1")
                        || info.Column.FieldName.Equals("INSPECTIONRESULT_1") || info.Column.FieldName.Equals("QUALITYWARNINGLETTER_1")
                        || info.Column.FieldName.Equals("THINKINGPERSONALITY_1") || info.Column.FieldName.Equals("NCRDESCRIPTION_1")
                        || info.Column.FieldName.Equals("REENACTIVATION_1") || info.Column.FieldName.Equals("GENERALJUDGMENT_1")
                        || info.Column.FieldName.Equals("MONTHLYSALESAMOUNT_1") || info.Column.FieldName.Equals("ISOPRATIONSTOP_1"))
                    {
                        quarter = "1";
                    }
                    else if (info.Column.FieldName.Equals("INSPECTIONPLANMONTHWEEK_2") || info.Column.FieldName.Equals("INSPECTIONDATE_2")
                            || info.Column.FieldName.Equals("INSPECTIONRESULT_2") || info.Column.FieldName.Equals("QUALITYWARNINGLETTER_2")
                            || info.Column.FieldName.Equals("THINKINGPERSONALITY_2") || info.Column.FieldName.Equals("NCRDESCRIPTION_2")
                            || info.Column.FieldName.Equals("REENACTIVATION_2") || info.Column.FieldName.Equals("GENERALJUDGMENT_2")
                            || info.Column.FieldName.Equals("MONTHLYSALESAMOUNT_2") || info.Column.FieldName.Equals("ISOPRATIONSTOP_2"))
                    {
                        quarter = "2";
                    }
                    else if (info.Column.FieldName.Equals("INSPECTIONPLANMONTHWEEK_3") || info.Column.FieldName.Equals("INSPECTIONDATE_3")
                            || info.Column.FieldName.Equals("INSPECTIONRESULT_3") || info.Column.FieldName.Equals("QUALITYWARNINGLETTER_3")
                            || info.Column.FieldName.Equals("THINKINGPERSONALITY_3") || info.Column.FieldName.Equals("NCRDESCRIPTION_3")
                            || info.Column.FieldName.Equals("REENACTIVATION_3") || info.Column.FieldName.Equals("GENERALJUDGMENT_3")
                            || info.Column.FieldName.Equals("MONTHLYSALESAMOUNT_3") || info.Column.FieldName.Equals("ISOPRATIONSTOP_3"))
                    {
                        quarter = "3";
                    }
                    else
                    {
                        quarter = "4";
                    }

                    AuditManageRegistQuarterPopup popup = new AuditManageRegistQuarterPopup(row["ENTERPRISEID"].ToString()
                        , row["PLANTID"].ToString()
                        , row["VENDORID"].ToString()
                        , row["AREAID"].ToString()
                        , Convert.ToDateTime(Conditions.GetValues()["P_YEAR"]).Year.ToString()
                        , quarter
                        , row["VENDORNAME"].ToString()
                        , row["AREANAME"].ToString())
                    {
                        StartPosition = FormStartPosition.CenterParent,
                        Owner = this
                    };
                    popup.btnSave.Enabled = btnFlag.Enabled;

                    if (popup.ShowDialog().Equals(DialogResult.OK))
                    {
                        Popup_FormClosed();
                    }
                }
            };
        }

        #endregion Event

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);

                await base.OnSearchAsync();

                grdAudit.View.ClearDatas();

                Dictionary<string, object> searchParam = Conditions.GetValues();
                searchParam["P_YEAR"] = Convert.ToDateTime(searchParam["P_YEAR"]).Year;
                searchParam.Add("P_PREYEAR", Convert.ToInt16(searchParam["P_YEAR"]) - 1);
                searchParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                if (await SqlExecuter.QueryAsync("SelectAuditManageResult", "10001", searchParam) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData");
                        return;
                    }

                    grdAudit.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                throw MessageException.Create(Format.GetString(ex));
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            #region vendor

            var condition = Conditions.AddSelectPopup("p_vendorid", new SqlQuery("GetVendorList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "VENDORNAME", "VENDORID")
                                        .SetPopupLayout("VENDOR", PopupButtonStyles.Ok_Cancel, true, false)
                                        .SetPopupLayoutForm(400, 600)
                                        .SetPopupResultCount(1)
                                        .SetPosition(2.1)
                                        .SetLabel("OSPVENDORNAME")
                                        .SetPopupAutoFillColumns("VENDORNAME");

            // 팝업 조회조건
            condition.Conditions.AddTextBox("VENDORID").SetLabel("VENDOR");

            // 팝업 그리드
            condition.GridColumns.AddTextBoxColumn("VENDORID", 50);
            condition.GridColumns.AddTextBoxColumn("VENDORNAME", 200);

            #endregion vendor

            #region 작업장

            condition = Conditions.AddSelectPopup("p_areaid", new SqlQuery("GetSfAreaByVendor", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}"), "AREANAME", "AREAID")
                                         .SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, false)
                                         .SetPopupLayoutForm(600, 600)
                                         .SetPopupResultCount(1)
                                         .SetPosition(2.2)
                                         .SetRelationIds("p_vendorid")
                                         .SetLabel("AREA");

            condition.Conditions.AddTextBox("AREA");

            condition.GridColumns.AddTextBoxColumn("AREAID", 150);
            condition.GridColumns.AddTextBoxColumn("AREANAME", 200);

            #endregion 작업장
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            SmartDateEdit dateEdit = Conditions.GetControl<SmartDateEdit>("p_year");
            dateEdit.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearsGroupView;
            dateEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            dateEdit.Properties.Mask.EditMask = "yyyy";
            dateEdit.EditValue = DateTime.Now;
        }

        #endregion 검색

        #region Private Function

        /// <summary>
        /// popup이 닫힐때 그리드를 재조회 하는 이벤트 (팝업결과 저장 후 재조회)
        /// </summary>
        private async void Popup_FormClosed()
        {
            await OnSearchAsync();
        }

        #endregion Private Function
    }
}