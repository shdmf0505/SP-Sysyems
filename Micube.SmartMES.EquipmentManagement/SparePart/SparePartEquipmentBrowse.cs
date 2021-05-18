#region using

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

namespace Micube.SmartMES.EquipmentManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 설비관리 > 보전관리 > 설비별 S/P 이력조회
    /// 업  무  설  명  : 설비별로 사용되어진 SparePart의 이력을 조회한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-10-02
    /// 수  정  이  력  :
    ///     2021.02.17 전우성 화면 최적화 및 코드 정리. 설비단 항목 추가
    ///
    /// </summary>
    public partial class SparePartEquipmentBrowse : SmartConditionManualBaseForm
    {
        #region 생성자

        public SparePartEquipmentBrowse()
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

            InitializeGrid();
            InitializeEvent();
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdEquipmentSparePart.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;

            grdEquipmentSparePart.View.AddTextBoxColumn("WORKORDERTYPEID", 150).SetIsHidden();

            grdEquipmentSparePart.View.AddTextBoxColumn("EQUIPMENTID", 80);
            grdEquipmentSparePart.View.AddTextBoxColumn("EQUIPMENTNAME", 200);
            grdEquipmentSparePart.View.AddTextBoxColumn("CHILDEQUIPMENTNAME", 120);
            grdEquipmentSparePart.View.AddTextBoxColumn("WORKORDERID", 120);
            grdEquipmentSparePart.View.AddTextBoxColumn("WORKORDERTYPE", 80).SetTextAlignment(TextAlignment.Center);
            grdEquipmentSparePart.View.AddTextBoxColumn("SPAREPARTID", 120);
            grdEquipmentSparePart.View.AddTextBoxColumn("SPAREPARTNAME", 200);
            grdEquipmentSparePart.View.AddTextBoxColumn("MODELNAME", 200);
            grdEquipmentSparePart.View.AddTextBoxColumn("UNITPRICE", 100).SetDisplayFormat("#,###,###,##0");
            grdEquipmentSparePart.View.AddTextBoxColumn("OUTPUTTIME", 80).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdEquipmentSparePart.View.AddTextBoxColumn("OUTPUTQTY", 80);
            grdEquipmentSparePart.View.AddTextBoxColumn("AMOUNT", 100).SetDisplayFormat("#,###,###,##0");

            grdEquipmentSparePart.View.PopulateColumns();

            grdEquipmentSparePart.View.SetIsReadOnly();
            grdEquipmentSparePart.View.OptionsView.ShowFooter = true;
            grdEquipmentSparePart.ShowStatusBar = false;

            grdEquipmentSparePart.View.Columns["OUTPUTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdEquipmentSparePart.View.Columns["OUTPUTQTY"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
            grdEquipmentSparePart.View.Columns["AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdEquipmentSparePart.View.Columns["AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            Conditions.GetControls<SmartSelectPopupEdit>().ForEach(control =>
            {
                // 조회조건의 ID 항목을 backSpace로 모두 삭제시에 이름 삭제
                control.EditValueChanged += (s, e) =>
                {
                    if (Format.GetFullTrimString(control.EditValue).Equals(string.Empty))
                    {
                        Conditions.GetControl<SmartTextBox>("EQUIPMENTNAME").Text = string.Empty;
                    }
                };
            });
        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);

                await base.OnSearchAsync();

                Dictionary<string, object> values = Conditions.GetValues();
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);

                values = Commons.CommonFunction.ConvertParameter(values);

                grdEquipmentSparePart.View.ClearDatas();

                if (await SqlExecuter.QueryAsync("GetSparePartHistoryReportWithEquipmentByEqp", "10001", values) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData");
                        return;
                    }

                    grdEquipmentSparePart.DataSource = dt;
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

            #region Site

            Conditions.AddComboBox("FACTORYID", new SqlQuery("GetFactoryListByEqp", "10001"), "FACTORYNAME", "FACTORYID")
                      .SetLabel("FACTORY")
                      .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                      .SetPosition(0.2)
                      .SetRelationIds("P_PLANTID")
                      .SetValidationIsRequired();

            #endregion Site

            #region 설비명

            var condition = Conditions.AddSelectPopup("EQUIPMENTID", new SqlQuery("GetRequestRepairMaintWorkOrderForEquipmentListByEqp", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}"))
                                      .SetPopupLayout("EQUIPMENTID", PopupButtonStyles.Ok_Cancel, true, false)
                                      .SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow)
                                      .SetPopupResultCount(1)
                                      .SetPosition(1.1)
                                      .SetRelationIds("P_PLANTID")
                                      .SetPopupApplySelection((selectedRow, dataGridRow) =>
                                      {
                                          selectedRow.ForEach(row =>
                                          {
                                              Conditions.GetControl<SmartTextBox>("EQUIPMENTNAME").Text = row["EQUIPMENTNAME"].ToString();
                                          });
                                      });

            condition.Conditions.AddTextBox("EQUIPMENTNAME").SetLabel("EQUIPMENTNAME");

            InitializeReceiptAreaPopupForPopup(condition.Conditions);

            condition.Conditions.AddComboBox("PROCESSSEGMENTCLASSID", new SqlQuery("GetProcessSegmentClassForRequestByEqp", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "PROCESSSEGMENTCLASSTYPE", "TopProcessSegmentClass" } }), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                                .SetEmptyItem("", "", true)
                                .SetLabel("PROCESSSEGMENTCLASS")
                                .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;

            // 팝업 그리드
            condition.IsMultiGrid = false;
            condition.GridColumns.AddTextBoxColumn("FACTORYID", 150).SetIsHidden();
            condition.GridColumns.AddTextBoxColumn("AREAID", 150).SetIsHidden();
            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 200).SetIsHidden();
            condition.GridColumns.AddTextBoxColumn("EQUPMENTLEVEL", 200).SetIsHidden();

            condition.GridColumns.AddTextBoxColumn("EQUIPMENTID", 120).SetIsReadOnly();
            condition.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 250).SetIsReadOnly();
            condition.GridColumns.AddTextBoxColumn("AREANAME", 120).SetIsReadOnly();
            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200).SetIsReadOnly();

            // 설비명
            Conditions.AddTextBox("EQUIPMENTNAME").SetIsReadOnly().SetPosition(1.2);

            #endregion 설비명

            // 모델명
            Conditions.AddTextBox("P_MODELNAME").SetLabel("MODELNAME").SetPosition(4.2);
        }

        /// <summary>
        /// 입고작업장 팝업창
        /// </summary>
        private void InitializeReceiptAreaPopupForPopup(ConditionCollection tempControl)
        {
            ConditionItemSelectPopup areaCondition = tempControl.AddSelectPopup("AREANAME", new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID")
                                                                .SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, true)
                                                                .SetPopupResultCount(1)
                                                                .SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow)
                                                                .SetPopupResultMapping("AREANAME", "AREANAME")
                                                                .SetPopupAutoFillColumns("AREANAME");

            areaCondition.Conditions.AddTextBox("AREANAME");
            areaCondition.Conditions.AddTextBox("AREAID");

            areaCondition.GridColumns.AddTextBoxColumn("AREAID", 150);
            areaCondition.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            SmartComboBox cboFactoryid = Conditions.GetControl<SmartComboBox>("FACTORYID");
            DataTable dtfactory = cboFactoryid.DataSource as DataTable;
            cboFactoryid.EditValue = dtfactory.Rows.Count > 0 ? dtfactory.Rows[0]["FACTORYID"] : cboFactoryid.EditValue;
        }

        #endregion 검색
    }
}