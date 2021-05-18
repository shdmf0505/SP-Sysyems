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
    /// 프 로 그 램 명  : 품질 관리 > Defect Map > 검사공정 최종 불량 현황
    /// 업  무  설  명  : 조회 조건에 따른 검사공정별 수율을 본다
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-05-27
    /// 수  정  이  력  : 
    /// [2019-09-11] 제목변경 AOI/BBT 불량 현황 -> 검사공정 최종 불량 현황
    ///              설계 변경에 따른 화면 변경 
    /// [2019-10-10] 설계 변경에 따른 화면 변경 2차 (전면 변경)
    /// </summary>
    public partial class DefectRateByEquipmentType : SmartConditionManualBaseForm
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

        public DefectRateByEquipmentType()
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

            flowProduct.AutoScroll = true;
            flowLot.AutoScroll = true;
        }

        /// <summary>
        /// Control Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            btnApply.LanguageKey = "APPLY";
            grbComparison.LanguageKey = "TABCOMPARISON";
            grbInspectionType.LanguageKey = "INSPECTIONTYPE";
            tabMain.SetLanguageKey(tabProduct, "PRODUCT");
            tabMain.SetLanguageKey(tabLot, "LOT");
        }

        /// <summary>
        /// Grid 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            var group = grdMain.View.AddGroupColumn("");
            group.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Left);

            group = grdMain.View.AddGroupColumn("DEFECTRATE");
            group.AddTextBoxColumn("AOIDEFECTRATE", 50).SetLabel("AOI").SetDisplayFormat("{0:f2}%", MaskTypes.Custom).SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("BBTDEFECTRATE", 50).SetLabel("BBT").SetDisplayFormat("{0:f2}%", MaskTypes.Custom).SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("HOLEDEFECTRATE", 50).SetLabel("HOLE").SetDisplayFormat("{0:f2}%", MaskTypes.Custom).SetTextAlignment(TextAlignment.Right);

            group = grdMain.View.AddGroupColumn("INSPECTIONQTY");
            group.AddTextBoxColumn("AOIQTY", 50).SetLabel("AOI").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("BBTQTY", 50).SetLabel("BBT").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("HOLEQTY", 50).SetLabel("HOLE").SetTextAlignment(TextAlignment.Right);

            group = grdMain.View.AddGroupColumn("DEFECTCOUNT");
            group.AddTextBoxColumn("AOICOUNT", 50).SetLabel("AOI").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("BBTCOUNT", 50).SetLabel("BBT").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("HOLECOUNT", 50).SetLabel("HOLE").SetTextAlignment(TextAlignment.Right);

            grdMain.View.PopulateColumns();
            grdMain.View.BestFitColumns();
            grdMain.View.SetIsReadOnly();

            grdMain.View.OptionsCustomization.AllowColumnMoving = false;
            grdMain.ShowButtonBar = false;

            grdMain.GridButtonItem = GridButtonItem.Export;
        }

        #endregion

        #region Event

        /// <summary>        
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        private void InitializeEvent()
        {
            //! 적용 버튼 이벤트
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

                    DataTable mainDt = _totalDataTable.AsEnumerable()
                                                      .Where(x => lotList.Contains(Format.GetString(x.Field<string>("LOTID"))))
                                                      .CopyToDataTable();

                    DataTable aoi = DefectMapHelper.GetDefectDataOfEquipmentType(mainDt, EquipmentType.EQUIPMENTTYPE_AOI);
                    DataTable bbt = DefectMapHelper.GetDefectDataOfEquipmentType(mainDt, EquipmentType.EQUIPMENTTYPE_BBT);
                    DataTable hole = DefectMapHelper.GetDefectDataOfEquipmentType(mainDt, EquipmentType.EQUIPMENTTYPE_HOLE);
                    Dictionary<string, List<int>> comparisonList = new Dictionary<string, List<int>>();

                    if (!DefectMapHelper.IsNull(aoi) || aoi?.Rows.Count > 0)
                    {
                        comparisonList.Add("AOI", new List<int>
                        {
                            Format.GetInteger(grdMain.View.GetCheckedRows().AsEnumerable().Sum(x => x.Field<double>("AOIQTY"))),
                            Format.GetInteger(grdMain.View.GetCheckedRows().AsEnumerable().Sum(x => x.Field<double>("AOICOUNT")))
                        });

                        flowLot.Controls.Add(DefectMapHelper.AddRateControlByEquipment("TABAOI", aoi, EquipmentType.EQUIPMENTTYPE_AOI, SubViewType.SUBVIEWTYPE_LOT));
                        flowProduct.Controls.Add(DefectMapHelper.AddRateControlByEquipment("TABAOI", aoi, EquipmentType.EQUIPMENTTYPE_AOI, SubViewType.SUBVIEWTYPE_PRODUCT));
                    }

                    if (!DefectMapHelper.IsNull(bbt) || bbt?.Rows.Count > 0)
                    {
                        comparisonList.Add("BBT", new List<int>
                        {
                            Format.GetInteger(grdMain.View.GetCheckedRows().AsEnumerable().Sum(x => x.Field<double>("BBTQTY"))),
                            Format.GetInteger(grdMain.View.GetCheckedRows().AsEnumerable().Sum(x => x.Field<double>("BBTCOUNT")))
                        });

                        flowLot.Controls.Add(DefectMapHelper.AddRateControlByEquipment("TABBBT", bbt, EquipmentType.EQUIPMENTTYPE_BBT, SubViewType.SUBVIEWTYPE_LOT));
                        flowProduct.Controls.Add(DefectMapHelper.AddRateControlByEquipment("TABBBT", bbt, EquipmentType.EQUIPMENTTYPE_BBT, SubViewType.SUBVIEWTYPE_PRODUCT));
                    }

                    if (!DefectMapHelper.IsNull(hole) || hole?.Rows.Count > 0)
                    {
                        comparisonList.Add("HOLE", new List<int>
                        {
                            Format.GetInteger(grdMain.View.GetCheckedRows().AsEnumerable().Sum(x => x.Field<double>("HOLEQTY"))),
                            Format.GetInteger(grdMain.View.GetCheckedRows().AsEnumerable().Sum(x => x.Field<double>("HOLECOUNT")))
                        });

                        flowLot.Controls.Add(DefectMapHelper.AddRateControlByEquipment("TABHOLE", hole, EquipmentType.EQUIPMENTTYPE_HOLE, SubViewType.SUBVIEWTYPE_LOT));
                        flowProduct.Controls.Add(DefectMapHelper.AddRateControlByEquipment("TABHOLE", hole, EquipmentType.EQUIPMENTTYPE_HOLE, SubViewType.SUBVIEWTYPE_PRODUCT));
                    }

                    DefectMapHelper.DrawingComparisonBarChart(chartMain, comparisonList);
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
                chartMain.ClearSeries();
                flowLot.Controls.Clear();
                flowProduct.Controls.Clear();
                grdMain.View.ClearDatas();

                DialogManager.ShowWaitArea(this.pnlContent);
                btnApply.IsBusy = true;

                await base.OnSearchAsync();

                if (DefectMapHelper.IsNull(_selectedRow))
                {
                    ShowMessage("NoLotProductdefCondition");
                    return;
                }

                if (await SqlExecuter.QueryAsync("GetDefectRateByEquipmentTypeList", "10001",
                                                 DefectMapHelper.AddLanguageTypeToConditions(Conditions.GetValues())) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData");
                        return;
                    }

                    _totalDataTable = DefectMapHelper.GetRepairAnalysisByEnterpriseid(dt);

                    grdMain.DataSource = DefectMapHelper.GetEquipmentTypeByTypeCount(_totalDataTable);
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

            #region BBT 공정

            Conditions.AddComboBox("P_BBTPROCESS",
                                   new SqlQuery("GetProcessByProductdef", "10001",
                                                DefectMapHelper.AddLanguageTypeToConditions(new Dictionary<string, object>
                                                {
                                                    { "P_ENTERPRISEID", UserInfo.Current.Enterprise },
                                                    { "P_PLANTID", UserInfo.Current.Plant },
                                                    { "P_INSPECTIONTYPE", "BBTInspection" }
                                                })))
                      .SetLabel("BBTPROCESS")
                      .SetRelationIds("P_PRODUCTDEFID", "P_PRODUCTDEFVERSION")
                      .SetEmptyItem()
                      .SetResultCount(0)
                      .SetPosition(5);

            #endregion

            #region HOLE 공정

            Conditions.AddComboBox("P_HOLEPROCESS",
                                   new SqlQuery("GetProcessByProductdef", "10001",
                                                DefectMapHelper.AddLanguageTypeToConditions(new Dictionary<string, object>
                                                {
                                                    { "P_ENTERPRISEID", UserInfo.Current.Enterprise },
                                                    { "P_PLANTID", UserInfo.Current.Plant },
                                                    { "P_INSPECTIONTYPE", "AOIHoleInspection" }
                                                })))
                      .SetLabel("HOLEPROCESS")
                      .SetRelationIds("P_PRODUCTDEFID", "P_PRODUCTDEFVERSION")
                      .SetEmptyItem()
                      .SetResultCount(0)
                      .SetPosition(6);

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

        #endregion
    }
}
