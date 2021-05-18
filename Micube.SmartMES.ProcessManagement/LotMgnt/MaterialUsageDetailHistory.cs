#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 >  자재관리 > 자재 사용 상세 이력
    /// 업  무  설  명  :
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2021-03-15
    /// 수  정  이  력  :
    ///
    ///
    /// </summary>
    public partial class MaterialUsageDetailHistory : SmartConditionManualBaseForm
    {
        #region Local Variables

        /// <summary>
        /// SelectPopup에 들어가는 parameter 전역 변수
        /// </summary>
        private readonly Dictionary<string, object> _param = new Dictionary<string, object>
        {
            { "LANGUAGETYPE", UserInfo.Current.LanguageType },
            { "ENTERPRISEID", UserInfo.Current.Enterprise },
            { "PLANTID", UserInfo.Current.Plant }
        };

        #endregion Local Variables

        #region 생성자

        public MaterialUsageDetailHistory()
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
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            tabMain.SetLanguageKey(tabPage1, "DAILYREQUIRE");
            tabMain.SetLanguageKey(tabPage2, "MATERIALREQUIRE");
            tabMain.SetLanguageKey(tabPage3, "EQUIPMENTMATERIALREQUIRE");
            tabMain.SetLanguageKey(tabPage4, "PROCESSMATERIALREQUIRE");
            tabMain.SetLanguageKey(tabPage5, "MATERIALDETAILSTATE");

            grdPage1.LanguageKey = "LIST";
            grdPage2.LanguageKey = "LIST";
            grdPage3.LanguageKey = "LIST";
            grdPage4.LanguageKey = "LIST";
            grdPage5.LanguageKey = "LIST";
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region 일자별 소요량

            grdPage1.GridButtonItem = GridButtonItem.Export;
            grdPage1.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdPage1.View.SetIsReadOnly();
            grdPage1.ShowStatusBar = true;

            #endregion 일자별 소요량

            #region 자재별 소요량

            grdPage2.GridButtonItem = GridButtonItem.Export;
            grdPage2.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdPage2.View.AddTextBoxColumn("AREAID").SetIsHidden();

            grdPage2.View.AddTextBoxColumn("VENDORID", 80);
            grdPage2.View.AddTextBoxColumn("VENDORNAME", 120);
            grdPage2.View.AddTextBoxColumn("AREANAME", 150);
            grdPage2.View.AddTextBoxColumn("CONSUMABLEDEFID", 80);
            grdPage2.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 150);
            grdPage2.View.AddTextBoxColumn("UNIT", 80);
            grdPage2.View.AddSpinEditColumn("REQUIREMENTQTY", 100).SetDisplayFormat("{0:F}", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
            grdPage2.View.AddTextBoxColumn("BOM", 60);

            grdPage2.View.PopulateColumns();
            grdPage2.View.SetIsReadOnly();
            grdPage2.ShowStatusBar = true;

            #endregion 자재별 소요량

            #region 설비별 자재소요량

            grdPage3.GridButtonItem = GridButtonItem.Export;
            grdPage3.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdPage3.View.AddTextBoxColumn("AREAID").SetIsHidden();

            grdPage3.View.AddTextBoxColumn("VENDORID", 80);
            grdPage3.View.AddTextBoxColumn("VENDORNAME", 120);
            grdPage3.View.AddTextBoxColumn("AREANAME", 150);
            grdPage3.View.AddTextBoxColumn("EQUIPMENTID", 80);
            grdPage3.View.AddTextBoxColumn("EQUIPMENTNAME", 120);
            grdPage3.View.AddTextBoxColumn("UNIT", 80);
            grdPage3.View.AddTextBoxColumn("CONSUMABLEDEFID", 80);
            grdPage3.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 150);
            grdPage3.View.AddSpinEditColumn("BOMQTY", 100).SetDisplayFormat("{0:F}", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
            grdPage3.View.AddSpinEditColumn("NONBOMQTY", 140).SetDisplayFormat("{0:F}", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
            grdPage3.View.AddSpinEditColumn("PRODUCTIONRESULTM2", 100).SetTextAlignment(TextAlignment.Right);
            grdPage3.View.AddSpinEditColumn("ONEUNITQTYM2", 160).SetTextAlignment(TextAlignment.Right);

            grdPage3.View.PopulateColumns();
            grdPage3.View.SetIsReadOnly();
            grdPage3.ShowStatusBar = true;

            #endregion 설비별 자재소요량

            #region 공정별 자재소요량

            grdPage4.GridButtonItem = GridButtonItem.Export;
            grdPage4.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdPage4.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID").SetIsHidden();

            grdPage4.View.AddTextBoxColumn("VENDORID", 80);
            grdPage4.View.AddTextBoxColumn("VENDORNAME", 120);
            grdPage4.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 150);
            grdPage4.View.AddTextBoxColumn("CONSUMABLEDEFID", 80);
            grdPage4.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 150);
            grdPage4.View.AddTextBoxColumn("UNIT", 80);
            grdPage4.View.AddSpinEditColumn("BOMQTY", 100).SetDisplayFormat("{0:F}", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
            grdPage4.View.AddSpinEditColumn("NONBOMQTY", 140).SetDisplayFormat("{0:F}", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
            grdPage4.View.AddSpinEditColumn("PRODUCTIONRESULTM2", 100).SetDisplayFormat("{0:F2}", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
            grdPage4.View.AddSpinEditColumn("ONEUNITQTYM2", 160).SetDisplayFormat("{0:F2}", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);

            grdPage4.View.PopulateColumns();
            grdPage4.View.SetIsReadOnly();
            grdPage4.ShowStatusBar = true;

            #endregion 공정별 자재소요량

            #region 자재 상세현황

            grdPage5.GridButtonItem = GridButtonItem.Export;
            grdPage5.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdPage5.View.AddTextBoxColumn("AREAID").SetIsHidden();
            grdPage5.View.AddTextBoxColumn("PROCESSSEGMENTID").SetIsHidden();
            grdPage5.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID").SetIsHidden();
            grdPage5.View.AddTextBoxColumn("DAILY").SetIsHidden();

            grdPage5.View.AddTextBoxColumn("WORKENDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            grdPage5.View.AddTextBoxColumn("AREANAME", 150);
            grdPage5.View.AddTextBoxColumn("VENDORID", 80);
            grdPage5.View.AddTextBoxColumn("VENDORNAME", 120);
            grdPage5.View.AddTextBoxColumn("CONSUMABLEDEFID", 80);
            grdPage5.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 150);
            grdPage5.View.AddTextBoxColumn("CONSUMABLELOTID", 150);
            grdPage5.View.AddSpinEditColumn("CONSUMEDQTY", 90).SetDisplayFormat("{0:F}", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
            grdPage5.View.AddTextBoxColumn("UNIT", 80);
            grdPage5.View.AddTextBoxColumn("BOM", 60);
            grdPage5.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 150);
            grdPage5.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            grdPage5.View.AddTextBoxColumn("EQUIPMENTID", 80);
            grdPage5.View.AddTextBoxColumn("EQUIPMENTNAME", 120);
            grdPage5.View.AddTextBoxColumn("LOTID", 160);
            grdPage5.View.AddTextBoxColumn("PRODUCTDEFID", 100);
            grdPage5.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            grdPage5.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            grdPage5.View.AddSpinEditColumn("PCS", 60).SetDisplayFormat("{0:N}", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
            grdPage5.View.AddSpinEditColumn("PNL", 60).SetDisplayFormat("{0:N}", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
            grdPage5.View.AddSpinEditColumn("M2", 60).SetDisplayFormat("{0:F2}", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);

            grdPage5.View.PopulateColumns();
            grdPage5.View.SetIsReadOnly();
            grdPage5.ShowStatusBar = true;

            #endregion 자재 상세현황
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // Tab Page 변경 이벤트
            tabMain.SelectedPageChanged += (s, e) => SetAnalysis(e.Page.TabIndex, grdPage5.DataSource as DataTable);

            Conditions.GetControls<SmartSelectPopupEdit>().ForEach(control =>
            {
                // 조회조건의 ID 항목을 backSpace로 모두 삭제시에 이름 삭제
                control.EditValueChanged += (s, e) =>
                {
                    if (Format.GetFullTrimString(control.EditValue).Equals(string.Empty))
                    {
                        switch (control.Name)
                        {
                            case "AREAID":
                                Conditions.GetControl<SmartTextBox>("AREANAME").Text = string.Empty;
                                break;

                            case "EQUIPMENTID":
                                Conditions.GetControl<SmartTextBox>("EQUIPMENTNAME").Text = string.Empty;
                                break;

                            case "PROCESSSEGMENTCLASSID":
                                Conditions.GetControl<SmartTextBox>("PROCESSSEGMENTCLASSNAME").Text = string.Empty;
                                break;

                            case "CONSUMABLEDEFID":
                                Conditions.GetControl<SmartTextBox>("CONSUMABLEDEFNAME").Text = string.Empty;
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
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);

                await base.OnSearchAsync();

                Dictionary<string, object> param = Conditions.GetValues();
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                param.Add("USERID", UserInfo.Current.Id);
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                grdPage5.View.ClearDatas();

                if (await SqlExecuter.QueryAsync("SelectMaterialUsageDetailHistory", "10001", param) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData");
                        return;
                    }

                    SetGirdPage1(DateTime.Parse(param["P_PERIOD_PERIODFR"].ToString()), DateTime.Parse(param["P_PERIOD_PERIODTO"].ToString()));
                    SetAnalysis(tabMain.SelectedTabPageIndex, dt);
                    grdPage5.DataSource = dt;
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

            #region 작업장

            var condition = Conditions.AddSelectPopup("AREAID", new SqlQuery("GetAreaID", "10001", _param))
                                      .SetPopupLayout("AREAIDNAME", PopupButtonStyles.Ok_Cancel, true, true)
                                      .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                                      .SetPopupResultCount(1)
                                      .SetPopupAutoFillColumns("AREANAME")
                                      .SetPosition(3)
                                      .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                      {
                                          selectedRow.ForEach(row =>
                                          {
                                              Conditions.GetControl<SmartTextBox>("AREANAME").Text = Format.GetString(row.GetObject("AREANAME"), "");
                                          });
                                      });

            condition.Conditions.AddTextBox("P_AREA").SetLabel("AREAIDNAME");

            condition.GridColumns.AddTextBoxColumn("AREAID", 120);
            condition.GridColumns.AddTextBoxColumn("AREANAME", 180);

            // 작업장명
            Conditions.AddTextBox("AREANAME").SetIsReadOnly().SetPosition(3.1);

            #endregion 작업장

            #region 설비

            condition = Conditions.AddSelectPopup("EQUIPMENTID", new SqlQuery("GetEquipmentAndArea", "10001", _param))
                                      .SetPopupLayout("EQUIPMENTIDNAME", PopupButtonStyles.Ok_Cancel, true, true)
                                      .SetPopupLayoutForm(400, 600, FormBorderStyle.FixedToolWindow)
                                      .SetPopupResultCount(1)
                                      .SetPopupAutoFillColumns("EQUIPMENTNAME")
                                      .SetPosition(4)
                                      .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                      {
                                          selectedRow.ForEach(row =>
                                          {
                                              Conditions.GetControl<SmartTextBox>("EQUIPMENTNAME").Text = Format.GetString(row.GetObject("EQUIPMENTNAME"), "");
                                              Conditions.GetControl<SmartTextBox>("AREANAME").Text = string.Empty;
                                          });
                                      });

            condition.Conditions.AddTextBox("P_EQUIPMENT").SetLabel("EQUIPMENTIDNAME");

            condition.GridColumns.AddTextBoxColumn("EQUIPMENTID", 80);
            condition.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 120);

            // 설비명
            Conditions.AddTextBox("EQUIPMENTNAME").SetIsReadOnly().SetPosition(4.1);

            #endregion 설비

            #region 공정그룹ID (중공정)

            condition = Conditions.AddSelectPopup("PROCESSSEGMENTCLASSID", new SqlQuery("GetProcessSegMentMiddle", "10003", _param))
                                  .SetPopupLayout("TXTLOADMIDDLESEGMENTCLASS", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(400, 600, FormBorderStyle.FixedToolWindow)
                                  .SetPopupResultCount(1)
                                  .SetPopupAutoFillColumns("PROCESSSEGMENTCLASSNAME")
                                  .SetPosition(5)
                                  .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                  {
                                      selectedRow.ForEach(row =>
                                      {
                                          Conditions.GetControl<SmartTextBox>("PROCESSSEGMENTCLASSNAME").Text = Format.GetString(row.GetObject("PROCESSSEGMENTCLASSNAME"), "");
                                      });
                                  });

            condition.Conditions.AddTextBox("PROCESSSEGMENTCLASSID").SetLabel("TXTLOADMIDDLESEGMENTCLASS");

            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 80);
            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 120);

            // 공정그룹명
            Conditions.AddTextBox("PROCESSSEGMENTCLASSNAME").SetIsReadOnly().SetPosition(5.1);

            #endregion 공정그룹ID (중공정)

            #region 자재

            condition = Conditions.AddSelectPopup("CONSUMABLEDEFID", new SqlQuery("GetConsumableDefinition", "10001", _param))
                                  .SetPopupLayout("CONSUMABLE", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                                  .SetPopupResultCount(1)
                                  .SetPopupAutoFillColumns("CONSUMABLEDEFNAME")
                                  .SetLabel("CONSUMABLEDEFID")
                                  .SetPosition(6)
                                  .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                  {
                                      selectedRow.ForEach(row =>
                                      {
                                          Conditions.GetControl<SmartTextBox>("CONSUMABLEDEFNAME").Text = Format.GetString(row.GetObject("CONSUMABLEDEFNAME"), "");
                                      });
                                  });

            condition.Conditions.AddTextBox("P_CONSUMABLEIDNAME").SetLabel("CONSUMABLEIDNAME");

            condition.GridColumns.AddTextBoxColumn("CONSUMABLEDEFID", 120);
            condition.GridColumns.AddTextBoxColumn("CONSUMABLEDEFVERSION", 80);
            condition.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 180);

            // 자재명
            Conditions.AddTextBox("CONSUMABLEDEFNAME").SetIsReadOnly().SetPosition(6.1);

            #endregion 자재
        }

        #endregion 검색

        #region private Function

        /// <summary>
        /// 일자별 소요량 조회 화면 Grid를 다시 그려준다.
        /// </summary>
        /// <param name="from">조회조건의 From 날짜</param>
        /// <param name="to">조회조건의 To 날짜</param>
        private void SetGirdPage1(DateTime from, DateTime to)
        {
            grdPage1.View.ClearColumns();
            grdPage1.View.AddTextBoxColumn("VENDORID").SetIsHidden();
            grdPage1.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID").SetIsHidden();
            grdPage1.View.AddTextBoxColumn("EQUIPMENTID").SetIsHidden();
            grdPage1.View.AddTextBoxColumn("CONSUMABLEDEFID").SetIsHidden();

            var group = grdPage1.View.AddGroupColumn("");
            group.AddTextBoxColumn("VENDORNAME", 120);
            group.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 150);
            group.AddTextBoxColumn("EQUIPMENTNAME", 150);
            group.AddTextBoxColumn("CONSUMABLEDEFNAME", 150);

            // 일의 기준은 08:30이며, 08:30분 이전의 Data는 이전일로 표시한다
            if (Convert.ToDateTime(string.Concat(from.ToString("yyyy-MM-dd"), " 08:30:00")) > from)
            {
                string date = from.AddDays(-1).ToString("yyyy-MM-dd");

                group = grdPage1.View.AddGroupColumn(date);
                group.AddTextBoxColumn(date + "BOM", 100).SetLabel("BOMQTY").SetDisplayFormat("{0:F}", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
                group.AddTextBoxColumn(date + "NONBOM", 140).SetLabel("NONBOMQTY").SetDisplayFormat("{0:F}", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
                group.AddSpinEditColumn(date + "PRODUCTIONRESULT", 100).SetLabel("PRODUCTIONRESULTM2").SetTextAlignment(TextAlignment.Right);
                group.AddSpinEditColumn(date + "ONEUNITQTY", 160).SetLabel("ONEUNITQTYM2").SetDisplayFormat("{0:F}", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
            }

            for (DateTime day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
            {
                group = grdPage1.View.AddGroupColumn(day.ToString("yyyy-MM-dd"));
                group.AddTextBoxColumn(day.ToString("yyyy-MM-dd") + "BOM", 100).SetLabel("BOMQTY").SetDisplayFormat("{0:F}", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
                group.AddTextBoxColumn(day.ToString("yyyy-MM-dd") + "NONBOM", 140).SetLabel("NONBOMQTY").SetDisplayFormat("{0:F}", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
                group.AddSpinEditColumn(day.ToString("yyyy-MM-dd") + "PRODUCTIONRESULT", 100).SetLabel("PRODUCTIONRESULTM2").SetTextAlignment(TextAlignment.Right);
                group.AddSpinEditColumn(day.ToString("yyyy-MM-dd") + "ONEUNITQTY", 160).SetLabel("ONEUNITQTYM2").SetDisplayFormat("{0:F}", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
            }

            group = grdPage1.View.AddGroupColumn("USAGETOTALSUM");
            group.AddTextBoxColumn("SUMBOMQTY", 100).SetLabel("BOMQTY").SetDisplayFormat("{0:F}", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("SUMNONBOMQTY", 140).SetLabel("NONBOMQTY").SetDisplayFormat("{0:F}", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
            group.AddSpinEditColumn("SUMPRODUCTIONRESULT", 100).SetLabel("PRODUCTIONRESULTM2").SetTextAlignment(TextAlignment.Right);
            group.AddSpinEditColumn("SUMONEUNITQTY", 160).SetLabel("ONEUNITQTYM2").SetDisplayFormat("{0:F}", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);

            group = grdPage1.View.AddGroupColumn("AVERAGEQTY");
            group.AddTextBoxColumn("AVEBOMQTY", 100).SetLabel("BOMQTY").SetDisplayFormat("{0:F}", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("AVENONBOMQTY", 140).SetLabel("NONBOMQTY").SetDisplayFormat("{0:F}", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
            group.AddSpinEditColumn("AVEPRODUCTIONRESULT", 100).SetLabel("PRODUCTIONRESULTM2").SetTextAlignment(TextAlignment.Right);
            group.AddSpinEditColumn("AVEONEUNITQTY", 160).SetLabel("ONEUNITQTYM2").SetDisplayFormat("{0:F}", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);

            grdPage1.View.PopulateColumns();
        }

        /// <summary>
        /// Tab 별 Data 생성
        /// </summary>
        /// <param name="tabPage"></param>
        /// <param name="data"></param>
        private void SetAnalysis(int tabPage, DataTable data)
        {
            if (data == null || data.Rows.Count.Equals(0))
            {
                return;
            }

            switch (tabPage)
            {
                case 0:
                    List<MaterialQtyDaily> dailyList = data.AsEnumerable()
                                                           .GroupBy(x => new Tuple<string, string, string, string, string>
                                                                        (x.Field<string>("VENDORID"), x.Field<string>("PROCESSSEGMENTCLASSID"), x.Field<string>("EQUIPMENTID"), x.Field<string>("CONSUMABLEDEFID"), x.Field<string>("DAILY")))
                                                           .Select(x => new MaterialQtyDaily
                                                           {
                                                               VENDORID = x.Key.Item1.Equals(string.Empty) ? "-" : x.Key.Item1,
                                                               VENDORNAME = x.FirstOrDefault().Field<string>("VENDORNAME"),
                                                               PROCESSSEGMENTCLASSID = x.Key.Item2.Equals(string.Empty) ? "-" : x.Key.Item2,
                                                               PROCESSSEGMENTCLASSNAME = x.FirstOrDefault().Field<string>("PROCESSSEGMENTCLASSNAME"),
                                                               EQUIPMENTID = x.Key.Item3.Equals(string.Empty) ? "-" : x.Key.Item3,
                                                               EQUIPMENTNAME = x.FirstOrDefault().Field<string>("EQUIPMENTNAME"),
                                                               CONSUMABLEDEFID = x.Key.Item4.Equals(string.Empty) ? "-" : x.Key.Item4,
                                                               CONSUMABLEDEFNAME = x.FirstOrDefault().Field<string>("CONSUMABLEDEFNAME"),
                                                               DAILY = x.Key.Item5,
                                                               BOM = x.Where(c => c.Field<string>("BOM").Equals("Y")).Sum(s => s.Field<decimal>("CONSUMEDQTY")),
                                                               NONBOM = x.Where(c => c.Field<string>("BOM").Equals("N")).Sum(s => s.Field<decimal>("CONSUMEDQTY")),
                                                               PRODUCTIONRESULT = x.Sum(s => s.Field<decimal>("M2")),
                                                               ONEUNITQTY = x.FirstOrDefault().Field<decimal>("M2").Equals(0) ? 0 :
                                                                              (x.Where(c => c.Field<string>("BOM").Equals("Y")).Sum(s => s.Field<decimal>("CONSUMEDQTY")) +
                                                                              x.Where(c => c.Field<string>("BOM").Equals("N")).Sum(s => s.Field<decimal>("CONSUMEDQTY"))) /
                                                                              x.Sum(s => s.Field<decimal>("M2"))
                                                           }).ToList();

                    PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(typeof(MaterialQtyDaily));
                    DataTable tab = new DataTable();

                    foreach (PropertyDescriptor pro in pdc)
                    {
                        tab.Columns.Add(pro.Name, pro.PropertyType);
                    }

                    object[] val = new object[pdc.Count];
                    dailyList.ForEach(item =>
                    {
                        val[0] = pdc["VENDORID"].GetValue(item);
                        val[1] = pdc["VENDORNAME"].GetValue(item);
                        val[2] = pdc["PROCESSSEGMENTCLASSID"].GetValue(item);
                        val[3] = pdc["PROCESSSEGMENTCLASSNAME"].GetValue(item);
                        val[4] = pdc["EQUIPMENTID"].GetValue(item);
                        val[5] = pdc["EQUIPMENTNAME"].GetValue(item);
                        val[6] = pdc["CONSUMABLEDEFID"].GetValue(item);
                        val[7] = pdc["CONSUMABLEDEFNAME"].GetValue(item);
                        val[8] = pdc["DAILY"].GetValue(item);
                        val[9] = pdc["BOM"].GetValue(item);
                        val[10] = pdc["NONBOM"].GetValue(item);
                        val[11] = pdc["PRODUCTIONRESULT"].GetValue(item);
                        val[12] = pdc["ONEUNITQTY"].GetValue(item);

                        tab.Rows.Add(val);
                    });

                    DataView view = new DataView(tab)
                    {
                        Sort = "VENDORID, PROCESSSEGMENTCLASSID, EQUIPMENTID, CONSUMABLEDEFID, DAILY"
                    };

                    string daily = string.Empty;

                    DataTable dt = (grdPage1.DataSource as DataTable).Clone();
                    DataRow dr = dt.NewRow();
                    int i = 1;
                    decimal bomSum = 0;
                    decimal nonBomSum = 0;
                    decimal productionResultSum = 0;

                    view.ToTable()
                        .AsEnumerable()
                        .ForEach(row =>
                        {
                            if (dr["VENDORID"].Equals(row["VENDORID"]) && dr["PROCESSSEGMENTCLASSID"].Equals(row["PROCESSSEGMENTCLASSID"])
                                && dr["EQUIPMENTID"].Equals(row["EQUIPMENTID"]) && dr["CONSUMABLEDEFID"].Equals(row["CONSUMABLEDEFID"]))
                            {
                                dr[row["DAILY"] + "BOM"] = row["BOM"];
                                dr[row["DAILY"] + "NONBOM"] = row["NONBOM"];
                                dr[row["DAILY"] + "PRODUCTIONRESULT"] = row["PRODUCTIONRESULT"];
                                dr[row["DAILY"] + "ONEUNITQTY"] = row["ONEUNITQTY"];
                        
                                bomSum += Convert.ToInt32(Format.GetDouble(row.GetObject("BOM"), 0));
                                nonBomSum += Convert.ToInt32(Format.GetDouble(row.GetObject("NONBOM"), 0));
                                productionResultSum += Convert.ToInt32(Format.GetDouble(row.GetObject("PRODUCTIONRESULT"), 0));
                                i++;
                            }
                            else
                            {
                                dr["SUMBOMQTY"] = bomSum;
                                dr["SUMNONBOMQTY"] = nonBomSum;
                                dr["SUMPRODUCTIONRESULT"] = productionResultSum;
                                dr["SUMONEUNITQTY"] = productionResultSum.Equals(0) ? 0 : (bomSum + nonBomSum) / productionResultSum;
                                dr["AVEBOMQTY"] = bomSum / i;
                                dr["AVENONBOMQTY"] = nonBomSum / i;
                                dr["AVEPRODUCTIONRESULT"] = productionResultSum / i;
                                dr["AVEONEUNITQTY"] = (productionResultSum / i).Equals(0) ? 0 : ((bomSum / i) + (nonBomSum / i)) / (productionResultSum / i);
                        
                                dt.Rows.Add(dr);
                                i = 1;
                        
                                dr = dt.NewRow();
                                dr["VENDORID"] = row["VENDORID"];
                                dr["VENDORNAME"] = row["VENDORNAME"];
                                dr["PROCESSSEGMENTCLASSID"] = row["PROCESSSEGMENTCLASSID"];
                                dr["PROCESSSEGMENTCLASSNAME"] = row["PROCESSSEGMENTCLASSNAME"];
                                dr["EQUIPMENTID"] = row["EQUIPMENTID"];
                                dr["EQUIPMENTNAME"] = row["EQUIPMENTNAME"];
                                dr["CONSUMABLEDEFID"] = row["CONSUMABLEDEFID"];
                                dr["CONSUMABLEDEFNAME"] = row["CONSUMABLEDEFNAME"];
                                dr[row["DAILY"] + "BOM"] = row["BOM"];
                                dr[row["DAILY"] + "NONBOM"] = row["NONBOM"];
                                dr[row["DAILY"] + "PRODUCTIONRESULT"] = row["PRODUCTIONRESULT"];
                                dr[row["DAILY"] + "ONEUNITQTY"] = row["ONEUNITQTY"];
                        
                                bomSum = Convert.ToInt32(Format.GetDouble(row.GetObject("BOM"), 0));
                                nonBomSum = Convert.ToInt32(Format.GetDouble(row.GetObject("NONBOM"), 0));
                                productionResultSum = Convert.ToInt32(Format.GetDouble(row.GetObject("PRODUCTIONRESULT"), 0));
                            }
                        
                            daily = Format.GetString(row.GetObject("DAILY"), string.Empty);
                        });

                    dr["SUMBOMQTY"] = bomSum;
                    dr["SUMNONBOMQTY"] = nonBomSum;
                    dr["SUMPRODUCTIONRESULT"] = productionResultSum;
                    dr["SUMONEUNITQTY"] = productionResultSum.Equals(0) ? 0 : (bomSum + nonBomSum) / productionResultSum;
                    dr["AVEBOMQTY"] = bomSum / i;
                    dr["AVENONBOMQTY"] = nonBomSum / i;
                    dr["AVEPRODUCTIONRESULT"] = productionResultSum / i;
                    dr["AVEONEUNITQTY"] = (productionResultSum / i).Equals(0) ? 0 : ((bomSum / i) + (nonBomSum / i)) / (productionResultSum / i);

                    dt.Rows.Add(dr);
                    dt.Rows.RemoveAt(0);
                    grdPage1.DataSource = dt;
                    break;

                case 1:
                    List<MaterialQty> list = data.AsEnumerable()
                                                 .GroupBy(x => new Tuple<string, string, string, string>(x.Field<string>("VENDORID"), x.Field<string>("AREAID"), x.Field<string>("CONSUMABLEDEFID"), x.Field<string>("BOM")))
                                                 .Select(x => new MaterialQty
                                                 {
                                                     VENDORID = x.Key.Item1,
                                                     VENDORNAME = x.FirstOrDefault().Field<string>("VENDORNAME"),
                                                     AREAID = x.Key.Item2,
                                                     AREANAME = x.FirstOrDefault().Field<string>("AREANAME"),
                                                     CONSUMABLEDEFID = x.Key.Item3,
                                                     CONSUMABLEDEFNAME = x.FirstOrDefault().Field<string>("CONSUMABLEDEFNAME"),
                                                     UNIT = x.FirstOrDefault().Field<string>("UNIT"),
                                                     REQUIREMENTQTY = x.Sum(s => s.Field<decimal>("CONSUMEDQTY")),
                                                     BOM = x.Key.Item4
                                                 }).ToList();

                    string sumStr = Language.Get("SUBTOTAL");

                    data.AsEnumerable()
                        .GroupBy(x => x.Field<string>("CONSUMABLEDEFID"))
                        .Select(x => new MaterialQty
                        {
                            VENDORID = sumStr,
                            VENDORNAME = "",
                            AREAID = "",
                            AREANAME = "",
                            CONSUMABLEDEFID = x.Key,
                            CONSUMABLEDEFNAME = x.FirstOrDefault().Field<string>("CONSUMABLEDEFNAME"),
                            UNIT = "",
                            REQUIREMENTQTY = x.Sum(s => s.Field<decimal>("CONSUMEDQTY")),
                            BOM = ""
                        })
                        .ForEach(x =>
                        {
                            list.Add(new MaterialQty
                            {
                                VENDORID = x.VENDORID,
                                VENDORNAME = x.VENDORNAME,
                                AREAID = x.AREAID,
                                AREANAME = x.AREANAME,
                                CONSUMABLEDEFID = x.CONSUMABLEDEFID,
                                CONSUMABLEDEFNAME = x.CONSUMABLEDEFNAME,
                                UNIT = x.UNIT,
                                REQUIREMENTQTY = x.REQUIREMENTQTY,
                                BOM = x.BOM
                            });
                        });

                    PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(MaterialQty));
                    DataTable table = new DataTable();

                    foreach (PropertyDescriptor prop in props)
                    {
                        table.Columns.Add(prop.Name, prop.PropertyType);
                    }

                    object[] values = new object[props.Count];

                    list.OrderBy(x => x.CONSUMABLEDEFID).ForEach(item =>
                    {
                        values[0] = props["VENDORID"].GetValue(item);
                        values[1] = props["VENDORNAME"].GetValue(item);
                        values[2] = props["AREAID"].GetValue(item);
                        values[3] = props["AREANAME"].GetValue(item);
                        values[4] = props["CONSUMABLEDEFID"].GetValue(item);
                        values[5] = props["CONSUMABLEDEFNAME"].GetValue(item);
                        values[6] = props["UNIT"].GetValue(item);
                        values[7] = props["REQUIREMENTQTY"].GetValue(item);
                        values[8] = props["BOM"].GetValue(item);

                        table.Rows.Add(values);
                    });

                    grdPage2.DataSource = table;

                    break;

                case 2:
                    grdPage3.DataSource = data.AsEnumerable()
                                              .GroupBy(x => new Tuple<string, string, string, string>(x.Field<string>("VENDORID"), x.Field<string>("AREAID"), x.Field<string>("EQUIPMENTID"), x.Field<string>("CONSUMABLEDEFID")))
                                              .Select(x => new
                                              {
                                                  VENDORID = x.Key.Item1,
                                                  VENDORNAME = x.FirstOrDefault().Field<string>("VENDORNAME"),
                                                  AREAID = x.Key.Item2,
                                                  AREANAME = x.FirstOrDefault().Field<string>("AREANAME"),
                                                  EQUIPMENTID = x.Key.Item3,
                                                  EQUIPMENTNAME = x.FirstOrDefault().Field<string>("EQUIPMENTNAME"),
                                                  UNIT = x.FirstOrDefault().Field<string>("UNIT"),
                                                  CONSUMABLEDEFID = x.Key.Item4,
                                                  CONSUMABLEDEFNAME = x.FirstOrDefault().Field<string>("CONSUMABLEDEFNAME"),
                                                  BOMQTY = x.Where(c => c.Field<string>("BOM").Equals("Y")).Sum(s => s.Field<decimal>("CONSUMEDQTY")),
                                                  NONBOMQTY = x.Where(c => c.Field<string>("BOM").Equals("N")).Sum(s => s.Field<decimal>("CONSUMEDQTY")),
                                                  PRODUCTIONRESULTM2 = x.Sum(s => s.Field<decimal>("M2")),
                                                  ONEUNITQTYM2 = x.FirstOrDefault().Field<decimal>("M2").Equals(0) ? 0 :
                                                                  (x.Where(c => c.Field<string>("BOM").Equals("Y")).Sum(s => s.Field<decimal>("CONSUMEDQTY")) +
                                                                  x.Where(c => c.Field<string>("BOM").Equals("N")).Sum(s => s.Field<decimal>("CONSUMEDQTY"))) /
                                                                  x.Sum(s => s.Field<decimal>("M2"))
                                              });
                    break;

                case 3:
                    grdPage4.DataSource = data.AsEnumerable()
                                              .GroupBy(x => new Tuple<string, string, string>(x.Field<string>("VENDORID"), x.Field<string>("PROCESSSEGMENTCLASSID"), x.Field<string>("CONSUMABLEDEFID")))
                                              .Select(x => new
                                              {
                                                  VENDORID = x.Key.Item1,
                                                  VENDORNAME = x.FirstOrDefault().Field<string>("VENDORNAME"),
                                                  PROCESSSEGMENTCLASSID = x.Key.Item2,
                                                  PROCESSSEGMENTCLASSNAME = x.FirstOrDefault().Field<string>("PROCESSSEGMENTCLASSNAME"),
                                                  UNIT = x.FirstOrDefault().Field<string>("UNIT"),
                                                  CONSUMABLEDEFID = x.Key.Item3,
                                                  CONSUMABLEDEFNAME = x.FirstOrDefault().Field<string>("CONSUMABLEDEFNAME"),
                                                  BOMQTY = x.Where(c => c.Field<string>("BOM").Equals("Y")).Sum(s => s.Field<decimal>("CONSUMEDQTY")),
                                                  NONBOMQTY = x.Where(c => c.Field<string>("BOM").Equals("N")).Sum(s => s.Field<decimal>("CONSUMEDQTY")),
                                                  PRODUCTIONRESULTM2 = x.Sum(s => s.Field<decimal>("M2")),
                                                  ONEUNITQTYM2 = x.FirstOrDefault().Field<decimal>("M2").Equals(0) ? 0 :
                                                                  (x.Where(c => c.Field<string>("BOM").Equals("Y")).Sum(s => s.Field<decimal>("CONSUMEDQTY")) +
                                                                  x.Where(c => c.Field<string>("BOM").Equals("N")).Sum(s => s.Field<decimal>("CONSUMEDQTY"))) /
                                                                  x.Sum(s => s.Field<decimal>("M2"))
                                              });
                    break;

                default:
                    break;
            }
        }

        #endregion private Function

        #region Class

        /// <summary>
        /// 일자별 소요량 조회 화면에 사용되는 Class
        /// </summary>
        private class MaterialQtyDaily
        {
            public string VENDORID { get; set; }
            public string VENDORNAME { get; set; }
            public string PROCESSSEGMENTCLASSID { get; set; }
            public string PROCESSSEGMENTCLASSNAME { get; set; }
            public string EQUIPMENTID { get; set; }
            public string EQUIPMENTNAME { get; set; }
            public string CONSUMABLEDEFID { get; set; }
            public string CONSUMABLEDEFNAME { get; set; }
            public string DAILY { get; set; }
            public decimal BOM { get; set; }
            public decimal NONBOM { get; set; }
            public decimal PRODUCTIONRESULT { get; set; }
            public decimal ONEUNITQTY { get; set; }
        }

        /// <summary>
        /// 자재별 소요량 조회 화면에 사용되는 Class
        /// </summary>
        private class MaterialQty
        {
            public string VENDORID { get; set; }
            public string VENDORNAME { get; set; }
            public string AREAID { get; set; }
            public string AREANAME { get; set; }
            public string CONSUMABLEDEFID { get; set; }
            public string CONSUMABLEDEFNAME { get; set; }
            public string UNIT { get; set; }
            public decimal REQUIREMENTQTY { get; set; }
            public string BOM { get; set; }
        }

        #endregion Class
    }
}