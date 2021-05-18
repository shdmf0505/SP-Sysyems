#region using

using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPivotGrid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 수율현황 및 불량분석 > 일별수율현황
    /// 업  무  설  명  : 일별수율현황을 확인한다.
    /// 생    성    자  : 류시윤
    /// 생    성    일  : 2019-12-26
    /// 수  정  이  력  :
    ///         2021.03.11 전우성 : 고객ID, 고객명 추가 및 화면 최적화
    ///
    /// </summary>
    public partial class YieldStatusByDaily : SmartConditionManualBaseForm
    {
        #region Local Variables

        private DXMenuItem _myContextMenu1;

        #endregion Local Variables

        #region 생성자

        public YieldStatusByDaily()
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
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            InitGridYieldDaily();

            InitGridYieldDailyWorst();

            InitPivotGridYieldDailyByType();
        }

        /// <summary>
        /// 일별수율 그리드 초기화
        /// </summary>
        private void InitGridYieldDaily()
        {
            gridYieldRateDaily.GridButtonItem = GridButtonItem.Export;
            gridYieldRateDaily.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            var group = gridYieldRateDaily.View.AddGroupColumn("");
            group.AddTextBoxColumn("SUMMARYDATE", 70).SetLabel("DATE").SetTextAlignment(TextAlignment.Center); // 일자

            group = gridYieldRateDaily.View.AddGroupColumn("Total");
            group.AddTextBoxColumn("TOTALYIELDRATE", 50).SetLabel("YIELDRATE").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric); // 수율
            group.AddTextBoxColumn("TOTALYIELDRATECUM", 50).SetLabel("YIELDRATECUM").SetDisplayFormat("##0.#0 %", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Center); // 누계수율
            group.AddTextBoxColumn("TOTALDEFECTRATE", 50).SetLabel("PCSDEFECTRATE").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric); // 불량율
            group.AddTextBoxColumn("TOTALINPUTQTY", 50).SetLabel("PCSINPUTQTY").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric); // 투입수
            group.AddTextBoxColumn("TOTALDEFECTQTY", 65).SetLabel("PCSDEFECTQTY").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric); // 불량수
            group.AddTextBoxColumn("TOTALNORMALQTY", 80).SetLabel("PCSNORMALQTY").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric); // 양품수

            #region 등록된 Site에 따른 Grid 구성

            DataTable dt = (Conditions.GetControl<SmartComboBox>("P_PLANTID")).DataSource as DataTable;

            for (int siteNo = 0; siteNo < dt.Rows.Count; siteNo++)
            {
                if (dt.Rows[siteNo]["PLANTID"].ToString().Equals("*"))
                {
                    continue;
                }

                group = gridYieldRateDaily.View.AddGroupColumn(dt.Rows[siteNo]["PLANTID"].ToString());
                group.AddTextBoxColumn("SITE" + siteNo.ToString() + "YIELDRATE", 50).SetLabel("YIELDRATE").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric); // 수율
                group.AddTextBoxColumn("SITE" + siteNo.ToString() + "YIELDRATECUM", 50).SetLabel("YIELDRATECUM").SetDisplayFormat("##0.#0 %", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Center); // 누계수율
                group.AddTextBoxColumn("SITE" + siteNo.ToString() + "DEFECTRATE", 50).SetLabel("PCSDEFECTRATE").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric); // 불량율
                group.AddTextBoxColumn("SITE" + siteNo.ToString() + "INPUTQTY", 80).SetLabel("PCSINPUTQTY").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric); // 투입수
                group.AddTextBoxColumn("SITE" + siteNo.ToString() + "DEFECTQTY", 80).SetLabel("PCSDEFECTQTY").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric); // 불량수
                group.AddTextBoxColumn("SITE" + siteNo.ToString() + "NORMALQTY", 80).SetLabel("PCSNORMALQTY").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric); // 양품
            }

            #endregion 등록된 Site에 따른 Grid 구성

            group = gridYieldRateDaily.View.AddGroupColumn("");
            group.AddTextBoxColumn("BARESMTTYPE", 100).SetLabel("DIVBARESMT").SetTextAlignment(TextAlignment.Center); // BASE/SMT 구분
            group.AddTextBoxColumn("PRODUCTDEFTYPE", 100).SetLabel("LOTPRODUCTTYPE"); // 양산구분

            gridYieldRateDaily.View.PopulateColumns();

            gridYieldRateDaily.View.BestFitColumns();
            gridYieldRateDaily.View.OptionsView.ShowFooter = true;
            gridYieldRateDaily.View.OptionsBehavior.Editable = false;
            gridYieldRateDaily.ShowStatusBar = false;

            #region Summary

            gridYieldRateDaily.View.Columns["SUMMARYDATE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridYieldRateDaily.View.Columns["SUMMARYDATE"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));

            gridYieldRateDaily.View.Columns["TOTALYIELDRATE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            gridYieldRateDaily.View.Columns["TOTALYIELDRATE"].SummaryItem.DisplayFormat = "{0:###,###}";
            gridYieldRateDaily.View.Columns["TOTALDEFECTRATE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            gridYieldRateDaily.View.Columns["TOTALDEFECTRATE"].SummaryItem.DisplayFormat = "{0:###,###}";
            gridYieldRateDaily.View.Columns["TOTALINPUTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridYieldRateDaily.View.Columns["TOTALINPUTQTY"].SummaryItem.DisplayFormat = "{0:###,###}";
            gridYieldRateDaily.View.Columns["TOTALDEFECTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridYieldRateDaily.View.Columns["TOTALDEFECTQTY"].SummaryItem.DisplayFormat = "{0:###,###}";
            gridYieldRateDaily.View.Columns["TOTALNORMALQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridYieldRateDaily.View.Columns["TOTALNORMALQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            #endregion Summary
        }

        /// <summary>
        /// 일별 Worst 그리드 초기화
        /// </summary>
        private void InitGridYieldDailyWorst()
        {
            gridYieldRateWorst.GridButtonItem = GridButtonItem.Export;
            gridYieldRateWorst.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            var group = gridYieldRateWorst.View.AddGroupColumn("");
            group.AddTextBoxColumn("NO", 30).SetTextAlignment(TextAlignment.Center); // NO
            group.AddTextBoxColumn("SHIPMENTSITE", 65).SetTextAlignment(TextAlignment.Center); // 출하SITE
            group.AddTextBoxColumn("CUSTOMERID", 100); // 고객ID
            group.AddTextBoxColumn("CUSTOMERNAME", 150); // 고객명
            group.AddTextBoxColumn("PRODUCTDEFID", 100); // 품목코드
            group.AddTextBoxColumn("PRODUCTDEFVERSION", 70); // 품목REV
            group.AddTextBoxColumn("PRODUCTDEFNAME", 100); // 품목명
            group.AddTextBoxColumn("PCSYIELDRATE", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric); // 수율
            group.AddTextBoxColumn("PCSDEFECTRATE", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric); // 불량율
            group.AddTextBoxColumn("PCSINPUTQTY", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric); // 투입수
            group.AddTextBoxColumn("PCSDEFECTQTY", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric); // 불량수
            group.AddTextBoxColumn("DEFECTOCCUPANCY", 50).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric); // 불량점유
            group.AddTextBoxColumn("PCSNORMALQTY", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric); // 양품수

            #region WORST

            for (int siteNo = 1; siteNo < 21; siteNo++) // Site Count 21
            {
                group = gridYieldRateWorst.View.AddGroupColumn("WORST" + siteNo);

                group.AddTextBoxColumn("DEFECTNAME" + siteNo, 50).SetLabel("DEFECTNAME"); // 불량명
                group.AddTextBoxColumn("PCSDEFECTQTY" + siteNo, 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("PCSDEFECTQTY"); // 불량수
                group.AddTextBoxColumn("PCSDEFECTRATE" + siteNo, 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric).SetLabel("PCSDEFECTRATE"); // 불량율
            }

            #endregion WORST

            group = gridYieldRateWorst.View.AddGroupColumn("Etc");

            group.AddTextBoxColumn("EXTDEFECTNAME", 50).SetLabel("DEFECTNAME"); // 불량명
            group.AddTextBoxColumn("EXTPCSDEFECTQTY", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("PCSDEFECTQTY"); // 불량수
            group.AddTextBoxColumn("EXTPCSDEFECTRATE", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric).SetLabel("PCSDEFECTRATE"); // 불량율

            gridYieldRateWorst.View.PopulateColumns();

            gridYieldRateWorst.View.BestFitColumns();
            gridYieldRateWorst.View.OptionsBehavior.Editable = false;
        }

        /// <summary>
        /// 타입별(PIVOT) 그리드 초기화
        /// </summary>
        private void InitPivotGridYieldDailyByType()
        {
            gridYieldRateTypePivot.AddRowField("SUMMARYDATE", "SUMDAY", 120);
            gridYieldRateTypePivot.AddFilterField("PRODUCTSHAPE", 120);
            gridYieldRateTypePivot.AddFilterField("COMPANYCLIENT", 120);
            gridYieldRateTypePivot.AddFilterField("LAYER", 50);
            gridYieldRateTypePivot.AddFilterField("SALEORDERCATEGORY", 80);
            gridYieldRateTypePivot.AddFilterField("PRODUCTIONORDERID", "SALESORDERID", 80);
            gridYieldRateTypePivot.AddFilterField("LINENO", "LINENO", 80);
            gridYieldRateTypePivot.AddFilterField("PRODUCTDEFNAME", 120);
            gridYieldRateTypePivot.AddFilterField("PRODUCTDEFID", 120);
            gridYieldRateTypePivot.AddFilterField("LOCALE", 80);
            gridYieldRateTypePivot.AddFilterField("SHIPMENTSITE", 80);
            gridYieldRateTypePivot.AddFilterField("RELATEDSITE", 80);
            gridYieldRateTypePivot.AddFilterField("INTERSECTSITE", 80);
            gridYieldRateTypePivot.AddFilterField("PRODUCTDEFVERSION", 70);
            gridYieldRateTypePivot.AddFilterField("STARTEDDATE", "INPUTDATE", 120);
            gridYieldRateTypePivot.AddColumnField("PRODUCTSHAPE", "PRODUCTSHAPE", 120);

            gridYieldRateTypePivot.AddDataField("PCSYIELDRATE", "PCSYIELDRATE", 100)
                .SetCellFormat(FormatType.Numeric, "##0.#0 %")
                .SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Custom;

            gridYieldRateTypePivot.AddDataField("PCSDEFECTRATE", "PCSDEFECTRATE", 100)
                .SetCellFormat(FormatType.Numeric, "##0.#0 %")
                .SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Custom;

            gridYieldRateTypePivot.AddDataField("PCSINPUTQTY", "PCSINPUTQTY", 120).SetCellFormat(FormatType.Numeric, "###,##0");
            gridYieldRateTypePivot.AddDataField("PCSDEFECTQTY", "PCSDEFECTQTY", 200).SetCellFormat(FormatType.Numeric, "###,##0");
            gridYieldRateTypePivot.AddDataField("PCSNORMALQTY", "PCSNORMALQTY", 200).SetCellFormat(FormatType.Numeric, "###,##0");

            gridYieldRateTypePivot.PopulateFields();
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += (s, e) =>
            {
                string strLocal = Conditions.GetCondition<ConditionItemComboBox>("P_LOCALEDIV").DefaultValue.ToString();

                if (strLocal.Equals("LOCAL"))
                {
                    Conditions.GetControl<SmartCheckedComboBox>("P_LINKPLANTID").Enabled = false;
                }
            };

            gridYieldRateDaily.View.CustomSummaryCalculate += (s, e) =>
            {
                if (e.SummaryProcess.Equals(DevExpress.Data.CustomSummaryProcess.Finalize))
                {
                    e.TotalValue = 0;
                }
            };

            gridYieldRateTypePivot.CustomCellValue += (s, e) =>
            {
                try
                {
                    double dValue = 0;

                    if ((e.RowValueType.Equals(PivotGridValueType.Value) && e.DataField.FieldName.Equals("PCSDEFECTQTY")) ||
                       (e.RowValueType.Equals(PivotGridValueType.Value) && e.DataField.FieldName.Equals("PCSINPUTQTY")) ||
                       (e.RowValueType.Equals(PivotGridValueType.Value) && e.DataField.FieldName.Equals("PCSNORMALQTY")))
                    {
                        if (double.IsNaN(e.Value.ToSafeDoubleNaN()))
                        {
                            e.Value = "-";
                        }
                    }

                    if (e.RowValueType.Equals(PivotGridValueType.GrandTotal) && e.DataField.FieldName.Equals("PCSDEFECTRATE"))
                    {
                        double sumInputQty = 0;
                        double sumQty = 0;

                        foreach (DataRow row in (gridYieldRateTypePivot.DataSource as DataTable).Rows)
                        {
                            if (row["PCSINPUTQTY"] != DBNull.Value)
                            {
                                sumInputQty += Convert.ToDouble(row["PCSINPUTQTY"]);
                            }

                            if (row["PCSDEFECTQTY"] != DBNull.Value)
                            {
                                sumQty += Convert.ToDouble(row["PCSDEFECTQTY"]);
                            }
                        }

                        dValue = Math.Round(sumQty / sumInputQty, 5);
                    }
                    else if (e.RowValueType.Equals(PivotGridValueType.Value) && e.DataField.FieldName.Equals("PCSDEFECTRATE"))
                    {
                        double sumInputQty = 0;
                        double sumQty = 0;

                        foreach (PivotDrillDownDataRow row in e.CreateDrillDownDataSource())
                        {
                            sumInputQty += Convert.ToDouble(row["PCSINPUTQTY"].ToString());
                            sumQty += Convert.ToDouble(row["PCSDEFECTQTY"].ToString());
                        }

                        dValue = Math.Round(sumQty / sumInputQty, 5);
                    }

                    if (e.RowValueType.Equals(PivotGridValueType.GrandTotal) && e.DataField.FieldName.Equals("PCSYIELDRATE"))
                    {
                        double sumInputQty = 0;
                        double sumQty = 0;

                        foreach (DataRow row in (gridYieldRateTypePivot.DataSource as DataTable).Rows)
                        {
                            if (row["PCSINPUTQTY"] != DBNull.Value)
                            {
                                sumInputQty += Convert.ToDouble(row["PCSINPUTQTY"]);
                            }

                            if (row["PCSNORMALQTY"] != DBNull.Value)
                            {
                                sumQty += Convert.ToDouble(row["PCSNORMALQTY"]);
                            }
                        }

                        dValue = Math.Round(sumQty / sumInputQty, 5);
                    }
                    else if (e.RowValueType.Equals(PivotGridValueType.Value) && e.DataField.FieldName.Equals("PCSYIELDRATE"))
                    {
                        double sumInputQty = 0;
                        double sumQty = 0;

                        foreach (PivotDrillDownDataRow row in e.CreateDrillDownDataSource())
                        {
                            if (row["PCSINPUTQTY"] != DBNull.Value)
                            {
                                sumInputQty += Convert.ToDouble(row["PCSINPUTQTY"].ToString());
                            }

                            if (row["PCSNORMALQTY"] != DBNull.Value)
                            {
                                sumQty += Convert.ToDouble(row["PCSNORMALQTY"].ToString());
                            }
                        }

                        dValue = Math.Round(sumQty / sumInputQty, 5);
                    }

                    if (double.IsNaN(dValue))
                    {
                        e.Value = "-";
                    }
                    else
                    {
                        e.Value = dValue;
                    }
                }
                catch (Exception)
                {
                }
            };

            smartTabControl1.SelectedPageChanged += (s, e) =>
            {
                Conditions.GetControl<SmartComboBox>("P_INTERPLANTID").Enabled = !smartTabControl1.SelectedTabPageIndex.Equals(0);
            };

            Conditions.GetControl<SmartComboBox>("P_LOCALEDIV").EditValueChanged += (s, e) =>
            {
                Conditions.GetControl<SmartCheckedComboBox>("P_LINKPLANTID").Enabled = !(s as SmartComboBox).EditValue.Equals("LOCAL");
            };

            gridYieldRateDaily.View.DoubleClick += (s, e) =>
            {
                GridView view = s as GridView;
                GridHitInfo info = view.CalcHitInfo((e as DXMouseEventArgs).Location);
                BandedGridColumn gridColumn = info.Column as BandedGridColumn;

                if (info.InRow || info.InRowCell)
                {
                    string strPeriod = view.GetRowCellValue(info.RowHandle, "SUMMARYDATE").ToString();
                    DateTime dateTime = Convert.ToDateTime(strPeriod);

                    dateTime = dateTime.AddHours(8).AddMinutes(30);

                    SmartPeriodEdit condPeriod = Conditions.GetControl<SmartPeriodEdit>("P_PERIOD");
                    condPeriod.datePeriodFr.EditValue = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    dateTime = dateTime.AddDays(1);
                    condPeriod.datePeriodTo.EditValue = dateTime.ToString("yyyy-MM-dd HH:mm:ss");

                    smartTabControl1.SelectedTabPage = tapPageYieldRateDailyWorst;

                    if (smartTabControl1.SelectedTabPageIndex != 0)
                    {
                        Conditions.GetControl<SmartComboBox>("P_INTERPLANTID").Enabled = true;
                    }

                    Conditions.GetControl<SmartComboBox>("P_INTERPLANTID").EditValue = "*";

                    SendKeys.Send("{F5}");
                }
            };

            gridYieldRateWorst.InitContextMenuEvent += (s, e) =>
            {
                // LOT 별 수율현황
                e.AddMenus.Add(_myContextMenu1 = new DXMenuItem(Language.Get("MENU_PG-QC-0470"), OpenForm) { BeginGroup = true, Tag = "PG-QC-0470" });
            };
        }

        /// <summary>
        /// ContextMenu Function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OpenForm(object sender, EventArgs args)
        {
            try
            {
                DialogManager.ShowWaitDialog();

                SmartBandedGrid grid = smartTabControl1.SelectedTabPage.Equals(tapPageYieldRateDaily) ? gridYieldRateDaily : gridYieldRateWorst;
                DataRow currentRow = grid.View.GetFocusedDataRow();

                string menuId = (sender as DXMenuItem).Tag.ToString();

                var values = Conditions.GetValues();

                var param = currentRow.Table.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => currentRow[col.ColumnName]);

                #region 품목코드 처리

                if (values.ContainsKey("P_PRODUCTDEFID"))
                {
                    values["P_PRODUCTDEFID"] = param["PRODUCTDEFID"].ToString() + param["PRODUCTDEFVERSION"].ToString().Split(' ')[0];
                }
                else
                {
                    values.Add("P_PRODUCTDEFID", param["PRODUCTDEFID"].ToString() + param["PRODUCTDEFVERSION"].ToString().Split(' ')[0]);
                }

                if (values.ContainsKey("P_PRODUCTNAME"))
                {
                    values["P_PRODUCTNAME"] = param["PRODUCTDEFNAME"].ToString();
                }
                else
                {
                    values.Add("P_PRODUCTNAME", param["PRODUCTDEFNAME"].ToString());
                }

                #endregion 품목코드 처리

                OpenMenu(menuId, values); //다른창 호출..
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

        #endregion Event

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("PLANT", UserInfo.Current.Plant);

            if (smartTabControl1.SelectedTabPage.Equals(tapPageYieldRateDaily))
            {
                gridYieldRateDaily.View.ClearDatas();

                if(await SqlExecuter.QueryAsync("SelectYieldRateByDayYPE", "10001", values) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                    }

                    gridYieldRateDaily.DataSource = dt;
                    gridYieldRateDaily.View.BestFitColumns();
                }
            }
            else if (smartTabControl1.SelectedTabPage == tapPageYieldRateDailyWorst)
            {
                gridYieldRateWorst.View.ClearDatas();

                if (await SqlExecuter.QueryAsync("SelectYieldRateWorstByDayYPE", "10001", values) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                    }

                    gridYieldRateWorst.DataSource = dt;
                    gridYieldRateWorst.View.BestFitColumns();
                }
            }
            else
            {
                gridYieldRateTypePivot.DataSource = null;

                if (await SqlExecuter.QueryAsync("SelectYieldRateTypePivotByDayYPE", "10001", values) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                    }

                    gridYieldRateTypePivot.DataSource = dt;
                }
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            #region 품목명

            Conditions.GetCondition<ConditionItemTextBox>("P_PRODUCTNAME").SetIsReadOnly();
            double posProdName = Conditions.GetCondition<ConditionItemTextBox>("P_PRODUCTNAME").Position;

            #endregion 품목명

            #region 품목

            var conditiosn = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductListByYieldStatus", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFIDVERSION", "PRODUCTDEFIDVERSION")
                                       .SetLabel("PRODUCTDEFID")
                                       .SetPopupLayout("GRIDPRODUCTLIST", PopupButtonStyles.Ok_Cancel, true, false)
                                       .SetPopupLayoutForm(600, 400, FormBorderStyle.FixedToolWindow)
                                       .SetPosition(posProdName - 0.1)
                                       .SetRelationIds("P_CUSTOMER")
                                       .SetPopupResultCount(0)
                                       .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                       {
                                           string strProdCd = string.Empty;
                                           string strProdNm = string.Empty;

                                           foreach (DataRow row in selectedRow)
                                           {
                                               strProdCd += row["PRODUCTDEFIDVERSION"] + ",";
                                               strProdNm += row["PRODUCTDEFNAME"] + ",";
                                           }
                                           strProdCd = strProdCd.Substring(0, strProdCd.Length - 1);
                                           Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(strProdCd);
                                           strProdNm = strProdNm.Substring(0, strProdNm.Length - 1);
                                           Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = strProdNm;
                                       });

            conditiosn.Conditions.AddTextBox("PRODUCTDEFID").SetLabel("PRODUCTDEFID");

            conditiosn.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 120).SetLabel("PRODUCTDEFID");
            conditiosn.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 250).SetLabel("PRODUCTDEFNAME");
            conditiosn.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100).SetLabel("PRODUCTDEFVERSION");

            #endregion 품목

            #region 고객사

            double posType = Conditions.GetCondition<ConditionItemComboBox>("P_PRODSHAPETYPE").Position;

            conditiosn = Conditions.AddSelectPopup("P_CUSTOMER", new SqlQuery("GetCustomerListByYield", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID")
                                   .SetLabel("CUSTOMER")
                                   .SetPopupLayout("CUSTOMER", PopupButtonStyles.Ok_Cancel, true, false)
                                   .SetPopupLayoutForm(600, 400, FormBorderStyle.FixedToolWindow)
                                   .SetPosition(posType + 0.1)
                                   .SetPopupResultCount(0)
                                   .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                   {
                                       selectedRow.ForEach(row =>
                                       {
                                           if (!DefectMapHelper.IsNull(row.GetObject("P_PRODUCTDEFID")))
                                           {
                                               Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(row["PRODUCTDEFID"]);
                                               Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = row["PRODUCTDEFNAME"].ToString();
                                           }
                                       });
                                   });

            conditiosn.Conditions.AddTextBox("CUSTOMERNAME").SetLabel("CUSTOMER");

            conditiosn.GridColumns.AddTextBoxColumn("CUSTOMERID", 120).SetLabel("CUSTOMERID");
            conditiosn.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 250).SetLabel("CUSTOMERNAME");

            #endregion 고객사
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += (s, e) =>
            {
                var values = Conditions.GetValues();
                string strValue = Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValue.ToString();

                if (string.IsNullOrEmpty(strValue))
                {
                    Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = string.Empty;
                }
            };

            Conditions.GetControl<SmartComboBox>("P_INTERPLANTID").Enabled = false;
        }

        #endregion 검색
    }
}