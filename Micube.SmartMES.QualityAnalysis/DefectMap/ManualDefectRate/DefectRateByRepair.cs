#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using Micube.SmartMES.Commons.Controls;

using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질 관리 > Defect Map >  AOI 분석 전/후 비교
    /// 업  무  설  명  : 선택된 품목에 따른 Repair된 AOI 분석 전/후 수율을 확인 한다.
    ///                  수동입력을 등록된 Data만 조회된다.
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-10-28
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class DefectRateByRepair : SmartConditionManualBaseForm
    {
        #region Local Variables

        /// <summary>
        /// 조회된 Raw Data
        /// </summary>
        private DataTable _totalDataTable;

        /// <summary>
        /// Lot/품목 조회 조건 정보 Row
        /// Lot/품목이 중 하나만 필수이기 때문에 사용
        /// </summary>
        private DataRow _selectedRow;

        #endregion

        #region 생성자

        public DefectRateByRepair()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGrid();
            InitializeLanguageKey();
            InitializeEvent();

            flowLot.AutoScroll = true;
            flowProduct.AutoScroll = true;
        }

        /// <summary>
        /// Control Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            btnApply.LanguageKey = "APPLY";
            grbTop.LanguageKey = "LOTANALYSISLIST";
            grdSummary.LanguageKey = "TABSUMMARY";
            grbBottom.LanguageKey = "BEFOREANDAFTER";
            tabMain.SetLanguageKey(tabProduct, "PRODUCT");
            tabMain.SetLanguageKey(tabLot, "LOT");
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region 상단 Grid

            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdMain.View.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("LOTID", 160).SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("INSPECTIONQTY", 60).SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddTextBoxColumn("BEFOREDEFECTCNT", 40).SetLabel("DEFECTBEFOREANALYSIS").SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddTextBoxColumn("BEFOREDEFECTRATE", 40).SetLabel("RATEBEFOREANALYSIS")
                        .SetDisplayFormat("{0:f2}%", MaskTypes.Custom)
                        .SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddTextBoxColumn("ANALYSISTARGET", 60).SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddTextBoxColumn("ANALYSISRESULT", 60).SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddTextBoxColumn("ANALYSISRATE", 40).SetLabel("ANALYSISRATE")
                        .SetDisplayFormat("{0:f2}%", MaskTypes.Custom)
                        .SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddTextBoxColumn("AFTERDEFECTCNT", 60).SetLabel("DEFECTAFTERANALYSIS").SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddTextBoxColumn("AFTERDEFECTRATE", 60).SetLabel("RATEAFTERANALYSIS")
                        .SetDisplayFormat("{0:f2}%", MaskTypes.Custom)
                        .SetTextAlignment(TextAlignment.Right);

            grdMain.View.PopulateColumns();
            grdMain.View.BestFitColumns();
            grdMain.View.SetIsReadOnly();

            grdMain.View.OptionsCustomization.AllowColumnMoving = false;
            grdMain.ShowButtonBar = false;

            grdMain.GridButtonItem = GridButtonItem.Export;

            #endregion

            #region 하단 Grid

            grdSummary.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            grdSummary.View.AddTextBoxColumn("INSPECTIONQTY", 60).SetTextAlignment(TextAlignment.Right);
            grdSummary.View.AddTextBoxColumn("BEFOREDEFECTCNT", 40).SetLabel("DEFECTBEFOREANALYSIS").SetTextAlignment(TextAlignment.Right);
            grdSummary.View.AddTextBoxColumn("BEFOREDEFECTRATE", 80).SetLabel("RATEBEFOREANALYSIS")
                        .SetDisplayFormat("{0:f2}%", MaskTypes.Custom)
                        .SetTextAlignment(TextAlignment.Right);
            grdSummary.View.AddTextBoxColumn("ANALYSISTARGET", 80).SetTextAlignment(TextAlignment.Right);
            grdSummary.View.AddTextBoxColumn("ANALYSISRESULT", 80).SetTextAlignment(TextAlignment.Right);
            grdSummary.View.AddTextBoxColumn("ANALYSISRATE", 40).SetLabel("ANALYSISRATE")
                        .SetDisplayFormat("{0:f2}%", MaskTypes.Custom)
                        .SetTextAlignment(TextAlignment.Right);
            grdSummary.View.AddTextBoxColumn("AFTERDEFECTCNT", 80).SetLabel("DEFECTAFTERANALYSIS").SetTextAlignment(TextAlignment.Right);
            grdSummary.View.AddTextBoxColumn("AFTERDEFECTRATE", 80).SetLabel("RATEAFTERANALYSIS")
                        .SetDisplayFormat("{0:f2}%", MaskTypes.Custom)
                        .SetTextAlignment(TextAlignment.Right);

            grdSummary.View.PopulateColumns();
            grdSummary.View.BestFitColumns();
            grdSummary.View.SetIsReadOnly();

            grdSummary.View.OptionsCustomization.AllowColumnMoving = false;

            grdSummary.GridButtonItem = GridButtonItem.Export;

            #endregion
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            //! 적용 버튼
            btnApply.Click += (s, e) =>
            {
                try
                {
                    if (grdMain.View.GetCheckedRows().Rows.Count.Equals(0))
                    {
                        ShowMessage("GridNoChecked");
                        return;
                    }

                    chartMain.ClearSeries();
                    flowLot.Controls.Clear();
                    flowProduct.Controls.Clear();

                    DialogManager.ShowWaitArea(this.pnlContent);
                    btnApply.IsBusy = true;

                    List<string> lotList = grdMain.View.GetCheckedRows().AsEnumerable()
                                                                        .Select(x => x.Field<string>("LOTID"))
                                                                        .ToList();

                    SetControl(_totalDataTable.AsEnumerable()
                                              .Where(x => lotList.Contains(Format.GetString(x.Field<string>("LOTID"))))
                                              .CopyToDataTable());

                    DataTable SummaryDt = DefectMapHelper.GetDefectAnalysisByRepairSummary(grdMain.View.GetCheckedRows());
                    grdSummary.DataSource = SummaryDt;

                    DefectMapHelper.DrawingBarChartByRepair(chartMain, SummaryDt, Language.Get("BEFOREDEFECTRATE"), Language.Get("AFTERDEFECTRATE"));
                }
                catch (Exception ex)
                {
                    throw MessageException.Create(Format.GetString(ex));
                }
                finally
                {
                    DialogManager.CloseWaitArea(this.pnlContent);
                    btnApply.IsBusy = false;
                }
            };
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
                flowProduct.Controls.Clear();
                flowLot.Controls.Clear();
                chartMain.ClearSeries();
                grdSummary.View.ClearDatas();
                grdMain.View.ClearDatas();

                DialogManager.ShowWaitArea(this.pnlContent);
                btnApply.IsBusy = true;

                await base.OnSearchAsync();

                if (DefectMapHelper.IsNull(_selectedRow))
                {
                    ShowMessage("NoLotProductdefCondition");
                    return;
                }

                if (await SqlExecuter.QueryAsync("GetDefectRateByRepair", "10001",
                                                 DefectMapHelper.AddLanguageTypeToConditions(Conditions.GetValues())) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData");
                        return;
                    }

                    _totalDataTable = dt;

                    grdMain.DataSource = DefectMapHelper.GetDefectAnalysisByRepair(dt);
                }
            }
            catch (Exception ex)
            {
                throw MessageException.Create(Format.GetString(ex));
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);
                btnApply.IsBusy = false;
            }
        }

        protected override void InitializeCondition()
        {
            #region Lot

            Conditions.AddSelectPopup("P_LOTID", new popupLotListByPeriod(), "P_LOTID", "P_LOTID")
                      .SetLabel("LOTID")
                      .SetPosition(1.1)
                      .SetPopupCustomParameter((popup, dataRow) =>
                      {
                          (popup as popupLotListByPeriod).SetParams(
                              Conditions.GetControl<SmartPeriodEdit>("P_PERIOD").datePeriodFr.Text,
                              Conditions.GetControl<SmartPeriodEdit>("P_PERIOD").datePeriodTo.Text,
                              Conditions.GetControl<SmartSelectPopupEdit>("P_LOTID").Text
                          );

                          (popup as popupLotListByPeriod).SelectedRowEvent += (dr) => SetCondition(dr);
                      });

            #endregion

            #region 품목 

            Conditions.AddSelectPopup("P_PRODUCTDEFID", new popupProductListByPeriod(), "P_PRODUCTDEFID", "P_PRODUCTDEFID")
                      .SetLabel("PRODUCTDEFID")
                      .SetPosition(1.2)
                      .SetValidationKeyColumn()
                      .SetPopupCustomParameter((popup, dataRow) =>
                      {
                          (popup as popupProductListByPeriod).SetParams(
                              Conditions.GetControl<SmartPeriodEdit>("P_PERIOD").datePeriodFr.Text,
                              Conditions.GetControl<SmartPeriodEdit>("P_PERIOD").datePeriodTo.Text,
                              Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").Text
                          );

                          (popup as popupProductListByPeriod).SelectedRowEvent += (dr) => SetCondition(dr);
                      });

            #endregion

            #region 품목명

            Conditions.GetCondition<ConditionItemTextBox>("P_PRODUCTNAME").SetIsReadOnly();

            #endregion

            #region 품목 Version

            Conditions.AddComboBox("P_PRODUCTDEFVERSION",
                                    new SqlQuery("GetProductDefVersionByRate", "10001"))
                      .SetLabel("PRODUCTDEFVERSION")
                      .SetRelationIds("P_PRODUCTDEFID")
                      .SetEmptyItem()
                      .SetResultCount(0)
                      .SetPosition(3);

            #endregion

            #region AOI 공정

            Conditions.AddComboBox("P_AOIPROCESS",
                                   new SqlQuery("GetProcessByProductdef", "10001",
                                                DefectMapHelper.AddLanguageTypeToConditions(new Dictionary<string, object>
                                                {
                                                    { "P_ENTERPRISEID", UserInfo.Current.Enterprise },
                                                    { "P_PLANTID", UserInfo.Current.Plant },
                                                    { "P_INSPECTIONTYPE", "AOIInspection" }
                                                })))
                      .SetLabel("AOIPROCESS")
                      .SetRelationIds("P_PRODUCTDEFID", "P_PRODUCTDEFVERSION")
                      .SetEmptyItem()
                      .SetResultCount(0)
                      .SetPosition(4);

            #endregion
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 조회 조건 설정
        /// </summary>
        /// <param name="dr"></param>
        private void SetCondition(DataRow dr)
        {
            Conditions.GetControl<SmartSelectPopupEdit>("P_LOTID").SetValue(DefectMapHelper.StringByDataRowObejct(dr, "P_LOTID"));
            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(DefectMapHelper.StringByDataRowObejct(dr, "P_PRODUCTDEFID"));
            Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = DefectMapHelper.StringByDataRowObejct(dr, "P_PRODUCTDEFNAME");

            Conditions.GetControl<SmartPeriodEdit>("P_PERIOD").datePeriodFr.Text = DefectMapHelper.StringByDataRowObejct(dr, "P_PERIOD_PERIODFR");
            Conditions.GetControl<SmartPeriodEdit>("P_PERIOD").datePeriodTo.Text = DefectMapHelper.StringByDataRowObejct(dr, "P_PERIOD_PERIODTO");

            _selectedRow = dr;
        }

        /// <summary>
        /// sub List Control 생성
        /// </summary>
        /// <param name="dt">Raw Data</param>
        /// <returns></returns>
        private void SetControl(DataTable dt)
        {
            EquipmentType type = EquipmentType.EQUIPMENTTYPE_AOI;
            DataTable beforeDt = dt;
            DataTable summaryDt = DefectMapHelper.GetRepairAnalysisByEnterpriseid(dt);

            flowProduct.Controls.Add(DefectMapHelper.AddRateControlByEquipment(Language.Get("BEFORE"), beforeDt, type, SubViewType.SUBVIEWTYPE_PRODUCT));
            flowProduct.Controls.Add(DefectMapHelper.AddRateControlByEquipment(Language.Get("AFTER"), summaryDt, type, SubViewType.SUBVIEWTYPE_PRODUCT));
            flowLot.Controls.Add(DefectMapHelper.AddRateControlByEquipment(Language.Get("BEFORE"), beforeDt, type, SubViewType.SUBVIEWTYPE_LOT));
            flowLot.Controls.Add(DefectMapHelper.AddRateControlByEquipment(Language.Get("AFTER"), summaryDt, type, SubViewType.SUBVIEWTYPE_LOT));
        }

        #endregion
    }
}
