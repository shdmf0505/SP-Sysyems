#region using

using DevExpress.XtraLayout.Utils;
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

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품목 관리 > 부자재 Routing BOM History
    /// 업  무  설  명  : 약품 BOM 소요량 기준정보 이력
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2021-02-23
    /// 수  정  이  력  :
    ///
    ///
    /// </summary>
    public partial class SubMaterialRoutingBomHistory : SmartConditionManualBaseForm
    {
        #region Local Variables

        /// <summary>
        /// SelectPopup에 들어가는 parameter 전역 변수
        /// </summary>
        private readonly Dictionary<string, object> _param = new Dictionary<string, object>
        {
            { "LANGUAGETYPE", UserInfo.Current.LanguageType },
            { "ENTERPRISEID", UserInfo.Current.Enterprise },
            { "PLANTID", UserInfo.Current.Plant },
            { "P_PRODUCTDEFTYPE", "Product" }
        };

        #endregion Local Variables

        #region 생성자

        public SubMaterialRoutingBomHistory()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeLanguageKey();
            InitializeGrid();
            InitializeEvent();

            SetConditionVisiblility("P_PERIOD", LayoutVisibility.Never);
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            tabMain.SetLanguageKey(tabDF, "DRYFILIM");
            tabMain.SetLanguageKey(tabHP, "HOTPRESS");
            tabMain.SetLanguageKey(tabChemical, "CHEMICAL");
            tabMain.SetLanguageKey(tabInk, "INK");
            grdTab1.LanguageKey = "DRYFILIM";
            grdTab2.LanguageKey = "HOTPRESS";
            grdTab3.LanguageKey = "CHEMICAL";
            grdTab4.LanguageKey = "INK";
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region Dry Filim

            grdTab1.GridButtonItem = GridButtonItem.Export;
            grdTab1.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdTab1.View.AddTextBoxColumn("TXNID", 60).SetIsHidden();
            grdTab1.View.AddTextBoxColumn("PRODUCTDEFID", 120);
            grdTab1.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center);
            grdTab1.View.AddTextBoxColumn("PRODUCTDEFNAME", 150);
            grdTab1.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            grdTab1.View.AddTextBoxColumn("DRYFILIMNO", 50).SetLabel("NO");
            grdTab1.View.AddTextBoxColumn("DRYFILIMTYPE", 50).SetLabel("TYPE");
            grdTab1.View.AddTextBoxColumn("STICKMETHOD", 60);
            grdTab1.View.AddTextBoxColumn("STICKDIRECTION", 60);
            grdTab1.View.AddTextBoxColumn("LOSSPERLOGIC", 100).SetTextAlignment(TextAlignment.Right);
            grdTab1.View.AddTextBoxColumn("LOSSPER", 80).SetDisplayFormat("{0:n4}%").SetTextAlignment(TextAlignment.Right);
            grdTab1.View.AddTextBoxColumn("CONSUMABLEDEFID", 140);
            grdTab1.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 140);
            grdTab1.View.AddTextBoxColumn("WORKPLANE", 100);
            grdTab1.View.AddTextBoxColumn("EQUIPMENTID", 100);
            grdTab1.View.AddTextBoxColumn("EQUIPMENTNAME", 150);
            grdTab1.View.AddSpinEditColumn("QTYM2PNL", 120).SetDisplayFormat("###,###,##0.#########", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
            grdTab1.View.AddTextBoxColumn("CIRCUITDIVISION", 60);
            grdTab1.View.AddTextBoxColumn("STANDARDDIVISION", 60);
            grdTab1.View.AddTextBoxColumn("CREATOR", 80).SetTextAlignment(TextAlignment.Center);
            grdTab1.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            grdTab1.View.AddTextBoxColumn("MODIFIER", 80).SetTextAlignment(TextAlignment.Center);
            grdTab1.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);

            grdTab1.View.PopulateColumns();
            grdTab1.View.SetIsReadOnly();

            grdTab1.ShowStatusBar = true;

            #endregion Dry Filim

            #region H/P

            grdTab2.GridButtonItem = GridButtonItem.Export;
            grdTab2.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdTab2.View.AddTextBoxColumn("TXNID", 60).SetIsHidden();
            grdTab2.View.AddTextBoxColumn("PRODUCTDEFID", 120);
            grdTab2.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center);
            grdTab2.View.AddTextBoxColumn("PRODUCTDEFNAME", 150);
            grdTab2.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            grdTab2.View.AddTextBoxColumn("RACKSIZE", 110);
            grdTab2.View.AddSpinEditColumn("TEMPC", 100).SetTextAlignment(TextAlignment.Right);
            grdTab2.View.AddSpinEditColumn("PRESS", 90).SetLabel("PRESSUREBAR").SetTextAlignment(TextAlignment.Right);
            grdTab2.View.AddSpinEditColumn("TIMEMINUTE", 80).SetTextAlignment(TextAlignment.Right);
            grdTab2.View.AddSpinEditColumn("COOLINGTEMPC", 110).SetTextAlignment(TextAlignment.Right);
            grdTab2.View.AddSpinEditColumn("GAONTIMEMINUTE", 110).SetLabel("GAONTIME").SetTextAlignment(TextAlignment.Right);
            grdTab2.View.AddSpinEditColumn("GAMONTIMEMINUTE", 110).SetLabel("GAMONTIME").SetTextAlignment(TextAlignment.Right);
            grdTab2.View.AddSpinEditColumn("STACK", 70).SetTextAlignment(TextAlignment.Right);
            grdTab2.View.AddTextBoxColumn("SINGLE", 60);
            grdTab2.View.AddTextBoxColumn("COL", 70);
            grdTab2.View.AddTextBoxColumn("BOX", 70);
            grdTab2.View.AddTextBoxColumn("BAKING", 90);
            grdTab2.View.AddTextBoxColumn("DETAILTYPE", 70).SetLabel("VACUUMNORMAL");
            grdTab2.View.AddTextBoxColumn("MATERIALDEFID", 120).SetLabel("MATERIALDEF");
            grdTab2.View.AddTextBoxColumn("MATERIALNAME", 150);
            grdTab2.View.AddTextBoxColumn("SPEC", 70);
            grdTab2.View.AddSpinEditColumn("MINVALUE", 90).SetLabel("COMPONENTQTY").SetTextAlignment(TextAlignment.Right);
            grdTab2.View.AddTextBoxColumn("CREATOR", 80).SetTextAlignment(TextAlignment.Center);
            grdTab2.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            grdTab2.View.AddTextBoxColumn("MODIFIER", 80).SetTextAlignment(TextAlignment.Center);
            grdTab2.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);

            grdTab2.View.PopulateColumns();
            grdTab2.View.SetIsReadOnly();

            grdTab2.ShowStatusBar = true;

            #endregion H/P

            #region 약품

            grdTab3.GridButtonItem = GridButtonItem.Export;
            grdTab3.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdTab3.View.AddTextBoxColumn("TXNID", 60).SetIsHidden();
            grdTab3.View.AddTextBoxColumn("PRODUCTDEFID", 120);
            grdTab3.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center);
            grdTab3.View.AddTextBoxColumn("PRODUCTDEFNAME", 150);
            grdTab3.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            grdTab3.View.AddTextBoxColumn("EQUIPMENTCLASSID", 120);
            grdTab3.View.AddTextBoxColumn("EQUIPMENTCLASSNAME", 120);
            grdTab3.View.AddTextBoxColumn("EQUIPMENTID", 100);
            grdTab3.View.AddTextBoxColumn("EQUIPMENTNAME", 150);
            grdTab3.View.AddTextBoxColumn("CONSUMABLEDEFID", 100);
            grdTab3.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 120);
            grdTab3.View.AddTextBoxColumn("STOCKUNIT", 100);
            grdTab3.View.AddTextBoxColumn("WORKPLANE", 100);
            grdTab3.View.AddSpinEditColumn("BOMQTYSTANDARD", 120).SetDisplayFormat("###,###,##0.#########", MaskTypes.Numeric).SetIsReadOnly().SetTextAlignment(TextAlignment.Right);
            grdTab3.View.AddSpinEditColumn("PNLBYAMOUNTBOM", 120).SetDisplayFormat("###,###,##0.#########", MaskTypes.Numeric).SetIsReadOnly().SetTextAlignment(TextAlignment.Right);
            grdTab3.View.AddSpinEditColumn("PNLAREA", 120).SetLabel("PNLAREAM2").SetDisplayFormat("###,###,##0.#########", MaskTypes.Numeric).SetIsReadOnly().SetTextAlignment(TextAlignment.Right);
            grdTab3.View.AddTextBoxColumn("CREATOR", 80).SetTextAlignment(TextAlignment.Center);
            grdTab3.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            grdTab3.View.AddTextBoxColumn("MODIFIER", 80).SetTextAlignment(TextAlignment.Center);
            grdTab3.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);

            grdTab3.View.PopulateColumns();
            grdTab3.View.SetIsReadOnly();

            grdTab3.ShowStatusBar = true;

            #endregion 약품

            #region 잉크

            grdTab4.GridButtonItem = GridButtonItem.Export;
            grdTab4.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdTab4.View.AddTextBoxColumn("TXNID", 60).SetIsHidden();
            grdTab4.View.AddTextBoxColumn("PRODUCTDEFID", 120);
            grdTab4.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center);
            grdTab4.View.AddTextBoxColumn("PRODUCTDEFNAME", 150);
            grdTab4.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            grdTab4.View.AddTextBoxColumn("CONSUMABLEDEFID", 100);
            grdTab4.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 120);
            grdTab4.View.AddSpinEditColumn("INKSPECIFICGRAVITY", 80).SetDisplayFormat("#0.####").SetTextAlignment(TextAlignment.Right);
            grdTab4.View.AddSpinEditColumn("NONVOLATILEMATTER", 80).SetDisplayFormat("#0.####").SetTextAlignment(TextAlignment.Right);
            grdTab4.View.AddSpinEditColumn("COVERAGECS", 110).SetDisplayFormat("###,##0.#########").SetTextAlignment(TextAlignment.Right);
            grdTab4.View.AddSpinEditColumn("COVERAGESS", 110).SetDisplayFormat("###,##0.#########").SetTextAlignment(TextAlignment.Right);
            grdTab4.View.AddSpinEditColumn("INKTHICKNESS", 100).SetDisplayFormat("#0.####").SetTextAlignment(TextAlignment.Right);
            grdTab4.View.AddTextBoxColumn("STANDARDQTY", 100).SetDisplayFormat("0.#########").SetTextAlignment(TextAlignment.Right);
            grdTab4.View.AddSpinEditColumn("PRODUCTLOSS", 80).SetDisplayFormat("#0.####").SetTextAlignment(TextAlignment.Right);
            grdTab4.View.AddSpinEditColumn("OUTPUTLOSS", 100).SetDisplayFormat("#0.####").SetTextAlignment(TextAlignment.Right);
            grdTab4.View.AddTextBoxColumn("STANDARDQTYLOSS", 100).SetDisplayFormat("0.#########").SetTextAlignment(TextAlignment.Right);
            grdTab4.View.AddTextBoxColumn("PRODUCTLOSSREASON", 200);
            grdTab4.View.AddTextBoxColumn("OUTPUTLOSSREASON", 200);
            grdTab4.View.AddTextBoxColumn("CREATOR", 80).SetTextAlignment(TextAlignment.Center);
            grdTab4.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            grdTab4.View.AddTextBoxColumn("MODIFIER", 80).SetTextAlignment(TextAlignment.Center);
            grdTab4.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);

            grdTab4.View.PopulateColumns();
            grdTab4.View.SetIsReadOnly();

            grdTab4.ShowStatusBar = true;

            #endregion 잉크
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // 조회조건의 History Type 값 변경시 이벤트
            if (Conditions.GetControl<SmartComboBox>("P_HISTORYTYPE") is var control)
            {
                control.EditValueChanging += (s, e) => SetConditionVisiblility("P_PERIOD", e.NewValue.Equals("Real") ? LayoutVisibility.Never : LayoutVisibility.Always);
            };

            // Tab Page 변경시 이벤트 - 조회조건 작업장은 HP와 약품에서는 사용되지 않는다.
            tabMain.SelectedPageChanged += (s, e) =>
            {
                if (e.Page.Equals(tabHP) || e.Page.Equals(tabInk))
                {
                    SetConditionVisiblility("P_AREAID", LayoutVisibility.Never);
                    SetConditionVisiblility("AREANAME2", LayoutVisibility.Never);
                }
                else
                {
                    SetConditionVisiblility("P_AREAID", LayoutVisibility.Always);
                    SetConditionVisiblility("AREANAME2", LayoutVisibility.Always);
                }
            };

            Conditions.GetControls<SmartSelectPopupEdit>().ForEach(pop =>
            {
                // 조회조건의 ID 항목을 backSpace로 모두 삭제시에 이름 삭제
                pop.EditValueChanged += (s, e) =>
                {
                    if (Format.GetFullTrimString(pop.EditValue).Equals(string.Empty))
                    {
                        switch (pop.Name)
                        {
                            case "P_PRODUCTDEFID":
                                Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").Text = string.Empty;
                                Conditions.GetControl<SmartTextBox>("PRODUCTDEFNAME").Text = string.Empty;
                                break;

                            case "P_PROCESSSEGMENTCLASSID":
                                Conditions.GetControl<SmartTextBox>("PROCESSSEGMENTCLASSNAME").Text = string.Empty;
                                break;

                            case "P_PROCESSSEGMENTID":
                                Conditions.GetControl<SmartTextBox>("PROCESSSEGMENTNAME").Text = string.Empty;
                                break;

                            case "P_AREAID":
                                Conditions.GetControl<SmartTextBox>("AREANAME2").Text = string.Empty;
                                break;

                            default:
                                break;
                        }
                    }
                };
            });
        }

        #endregion Event

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// tab1 : D/F / tab2 : H/P / tab3 : 약품 / tab4 : Ink
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);

                await base.OnSearchAsync();

                SmartBandedGrid grid = tabMain.SelectedTabPageIndex.Equals(0) ? grdTab1 :
                                       tabMain.SelectedTabPageIndex.Equals(1) ? grdTab2 :
                                       tabMain.SelectedTabPageIndex.Equals(2) ? grdTab3 : grdTab4;

                grid.View.ClearDatas();

                Dictionary<string, object> param = Conditions.GetValues();
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                param.Add("PLANTID", UserInfo.Current.Plant);

                string query = tabMain.SelectedTabPageIndex.Equals(0) ? "SelectSubMaterialRoutingBomHistoryByDryFilm" :
                               tabMain.SelectedTabPageIndex.Equals(1) ? "SelectSubMaterialRoutingBomHistoryByHP" :
                               tabMain.SelectedTabPageIndex.Equals(2) ? "SelectSubMaterialRoutingBomHistoryByChemical" : "SelectSubMaterialRoutingBomHistoryByInk";

                if (await SqlExecuter.QueryAsync(query, "10001", param) is DataTable dt)
                {
                    grid.View.Columns["TXNID"].OwnerBand.Visible = !param["P_HISTORYTYPE"].Equals("Real");
                    grid.View.Columns["TXNID"].Visible = !param["P_HISTORYTYPE"].Equals("Real");

                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData");
                        return;
                    }

                    grid.DataSource = dt;
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

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            #region 품목코드

            var condition = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefinitionList", "10002", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEFID", "PRODUCTDEFID")
                                      .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                                      .SetPopupLayoutForm(800, 800)
                                      .SetLabel("PRODUCTDEFID")
                                      .SetPosition(0.1)
                                      .SetPopupResultCount(1)
                                      .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                      {
                                          selectedRow.ForEach(row =>
                                          {
                                              Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").Text = Format.GetString(row.GetObject("PRODUCTDEFVERSION"), "");
                                              Conditions.GetControl<SmartTextBox>("PRODUCTDEFNAME").Text = Format.GetString(row.GetObject("PRODUCTDEFNAME"), "");
                                          });
                                      });

            condition.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
            condition.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                                .SetDefault("Product");

            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            condition.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            condition.GridColumns.AddComboBoxColumn("PRODUCTIONTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            condition.GridColumns.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=OutsourcingSpec"), "UOMDEFNAME", "UOMDEFID");

            // 내부 Rev, 품목명
            Conditions.AddTextBox("P_PRODUCTDEFVERSION").SetLabel("PRODUCTDEFVERSION").SetIsReadOnly().SetPosition(0.2);
            Conditions.AddTextBox("PRODUCTDEFNAME").SetIsReadOnly().SetPosition(0.3);

            #endregion 품목코드

            #region 공정그룹ID (중공정)

            condition = Conditions.AddSelectPopup("P_PROCESSSEGMENTCLASSID", new SqlQuery("GetProcessSegMentMiddle", "10003", _param), "PROCESSSEGMENTCLASSID", "PROCESSSEGMENTCLASSID")
                                      .SetPopupLayout("PROCESSSEGMENTCLASSID", PopupButtonStyles.Ok_Cancel, true, true)
                                      .SetPopupLayoutForm(400, 600, FormBorderStyle.FixedToolWindow)
                                      .SetLabel("PROCESSSEGMENTCLASSID")
                                      .SetPopupResultCount(1)
                                      .SetPopupAutoFillColumns("PROCESSSEGMENTCLASSNAME")
                                      .SetPosition(0.4)
                                      .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                      {
                                          selectedRow.ForEach(row =>
                                          {
                                              Conditions.GetControl<SmartTextBox>("PROCESSSEGMENTCLASSNAME").Text = Format.GetString(row.GetObject("PROCESSSEGMENTCLASSNAME"), "");
                                          });
                                      });

            condition.Conditions.AddTextBox("PROCESSSEGMENTCLASSID").SetLabel("PROCESSSEGMENTGROUPIDNAME");

            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 80);
            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 120);

            // 공정그룹명
            Conditions.AddTextBox("PROCESSSEGMENTCLASSNAME").SetIsReadOnly().SetPosition(0.5);

            #endregion 공정그룹ID (중공정)

            #region 공정

            condition = Conditions.AddSelectPopup("P_PROCESSSEGMENTID", new SqlQuery("GetProcesssegmentID", "10001", _param), "PROCESSSEGMENTID", "PROCESSSEGMENTID")
                                  .SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                                  .SetLabel("PROCESSSEGMENTID")
                                  .SetPopupResultCount(1)
                                  .SetRelationIds("P_PROCESSSEGMENTCLASSID")
                                  .SetPopupAutoFillColumns("PROCESSSEGMENTNAME")
                                  .SetPosition(0.6)
                                  .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                  {
                                      selectedRow.ForEach(row =>
                                      {
                                          Conditions.GetControl<SmartTextBox>("PROCESSSEGMENTNAME").Text = Format.GetString(row.GetObject("PROCESSSEGMENTNAME"), "");
                                      });
                                  });

            condition.Conditions.AddTextBox("PROCESSSEGMENTIDNAME").SetLabel("PROCESSSEGMENTIDNAME");
            condition.Conditions.AddTextBox("PROCESSSEGMENTCLASSID").SetIsHidden();

            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 120);
            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 180);

            // 공정명
            Conditions.AddTextBox("PROCESSSEGMENTNAME").SetIsReadOnly().SetPosition(0.7);

            #endregion 공정

            #region 작업장

            condition = Conditions.AddSelectPopup("P_AREAID", new SqlQuery("GetAreaID", "10001", _param), "AREAID", "AREAID")
                                  .SetPopupLayout("AREAID", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                                  .SetPopupResultCount(1)
                                  .SetPopupAutoFillColumns("AREANAME")
                                  .SetLabel("AREAID")
                                  .SetPosition(0.8)
                                  .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                  {
                                      selectedRow.ForEach(row =>
                                      {
                                          Conditions.GetControl<SmartTextBox>("AREANAME2").Text = Format.GetString(row.GetObject("AREANAME"), "");
                                      });
                                  });

            condition.Conditions.AddTextBox("P_AREA").SetLabel("AREAIDNAME");

            condition.GridColumns.AddTextBoxColumn("AREAID", 120);
            condition.GridColumns.AddTextBoxColumn("AREANAME", 180);

            // 작업장명
            Conditions.AddTextBox("AREANAME2").SetIsReadOnly().SetPosition(0.9);

            #endregion 작업장
        }

        #endregion 검색
    }
}