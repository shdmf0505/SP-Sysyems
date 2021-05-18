#region using

using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 수율현황 및 불량분석 > 품목별 수율 및 불량현황
    /// 업  무  설  명  : 제품 품목별 수율현황/불량코드/불량세부를 확인한다.
    /// 생    성    자  : 류시윤
    /// 생    성    일  : 2019-12-19
    /// 수  정  이  력  :
    ///     2021.01.26 전우성 양산구분을 CheckCombo로 변경요청
    ///                       기존 MRB인 경우 Query부분에서 처리가 달라 P_LOTCREATEDTYPE 파라미터를 생성하여 MRB인 경우 로직 추가
    ///     2021.04.08 전우성 화면 최적화 및 화면별 Field Hiden 처리
    /// </summary>
    public partial class YieldDefectStatusByProductItem : SmartConditionManualBaseForm
    {
        #region 생성자

        public YieldDefectStatusByProductItem()
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
            #region 품목별 수율

            gridYieldRate.GridButtonItem = GridButtonItem.Export;
            gridYieldRate.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            var group = gridYieldRate.View.AddGroupColumn("");
            group.AddTextBoxColumn("PRODUCTSHAPE", 70); // 제품타입
            group.AddComboBoxColumn("LAYER", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Layer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetEmptyItem("", ""); // 층수            // 고객사
            group.AddTextBoxColumn("COMPANYCLIENT", 50); // 고객사
            group.AddTextBoxColumn("PRODUCTDEFNAME", 100); // 품목명
            group.AddTextBoxColumn("PRODUCTDEFID", 100); // 품목코드
            group.AddTextBoxColumn("LOCALE", 50).SetIsHidden(); // 구분
            group.AddTextBoxColumn("SHIPMENTSITE", 65).SetIsHidden(); // 출하SITE
            group.AddTextBoxColumn("RELATEDSITE", 80).SetIsHidden(); // 연계SITE
            group.AddTextBoxColumn("INTERSECTSITE", 65).SetIsHidden(); // 구간
            group.AddTextBoxColumn("PRODUCTDEFVERSION", 70); // 품목REV
            group.AddTextBoxColumn("LOTCOUNT", 70).SetLabel("PROCESSEDLOTCOUNT").SetTextAlignment(TextAlignment.Right); // lOT 수량

            group = gridYieldRate.View.AddGroupColumn("GROUPPCSYIELDRATE");
            group.AddTextBoxColumn("PCSYIELDRATE", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("##0.#0 %", MaskTypes.Custom); // 수율
            group.AddTextBoxColumn("PCSDEFECTRATE", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric); // 불량율
            group.AddTextBoxColumn("PCSINPUTQTY", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric); // 투입수
            group.AddTextBoxColumn("PCSDEFECTQTY", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric); // 불량수
            group.AddTextBoxColumn("PCSNORMALQTY", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric); // 양품수

            group = gridYieldRate.View.AddGroupColumn("GROUPAREAYIELDRATE");
            group.AddTextBoxColumn("AREAYIELDRATE", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric); // 수율
            group.AddTextBoxColumn("AREADEFECTRATE", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric); // 불량율
            group.AddTextBoxColumn("AREAINPUTQTY", 50).SetLabel("INPUTM2").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("##0.#0", MaskTypes.Numeric); // 투입수
            group.AddTextBoxColumn("AREADEFECTQTY", 50).SetLabel("DEFECTM2").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("##0.#0", MaskTypes.Numeric); // 불량수
            group.AddTextBoxColumn("AREANORMALQTY", 50).SetLabel("NORMALM2").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("##0.#0", MaskTypes.Numeric); // 양품수

            group = gridYieldRate.View.AddGroupColumn("GROUPPRICEYIELDRATE");
            group.AddTextBoxColumn("PRICEYIELDRATE", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric); // 수율
            group.AddTextBoxColumn("PRICEDEFECTRATE", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric); // 불량율
            group.AddTextBoxColumn("PRICEINPUTQTY", 50).SetLabel("INPUTCOST").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric); // 투입수
            group.AddTextBoxColumn("PRICEDEFECTQTY", 50).SetLabel("DEFECTCOST").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric); // 불량수
            group.AddTextBoxColumn("SMTPRICEDEFECTQTY", 50).SetLabel("SMTDEFECTPRICE").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric); // SMT불량금액
            group.AddTextBoxColumn("PRICENORMALQTY", 50).SetLabel("NORMALCOST").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric); // 양품수

            group = gridYieldRate.View.AddGroupColumn("");
            group.AddTextBoxColumn("INSPPROCESSSEGMENT", 100); // 검사공정
            group.AddTextBoxColumn("BARESMTTYPE", 80).SetLabel("DIVBARESMT"); // BASE/SMT 구분
            group.AddTextBoxColumn("SUMMARYDATE", 100).SetLabel("SUMDAY").SetTextAlignment(TextAlignment.Right); // 집계일자
            group.AddTextBoxColumn("PRODUCTDEFTYPE", 100).SetLabel("LOTPRODUCTTYPE"); // 양산구분

            gridYieldRate.View.PopulateColumns();

            gridYieldRate.View.SetIsReadOnly();

            #endregion 품목별 수율

            #region 품목별 불량현황

            gridDefectStatus.GridButtonItem = GridButtonItem.Export;
            gridDefectStatus.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            group = gridDefectStatus.View.AddGroupColumn("");
            group.AddTextBoxColumn("PRODUCTSHAPE", 70); // 제품타입
            group.AddComboBoxColumn("LAYER", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Layer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetEmptyItem("", ""); // 층수
            group.AddTextBoxColumn("COMPANYCLIENT", 50); // 고객사
            group.AddTextBoxColumn("PRODUCTDEFNAME", 100); // 품목명
            group.AddTextBoxColumn("PRODUCTDEFID", 100); // 품목코드
            group.AddTextBoxColumn("LOCALE", 50).SetIsHidden(); // 구분
            group.AddTextBoxColumn("SHIPMENTSITE", 65).SetIsHidden(); // 출하SITE
            group.AddTextBoxColumn("RELATEDSITE", 80).SetIsHidden(); // 연결SITE
            group.AddTextBoxColumn("PRODUCTDEFVERSION", 70); // 품목REV

            group = gridDefectStatus.View.AddGroupColumn("DEFECTINFO");
            group.AddTextBoxColumn("PCSINPUTQTY", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric); // 투입수
            group.AddTextBoxColumn("DEFECTCODE", 50).SetLabel("DEFECTCODEID"); // 불량코드
            group.AddTextBoxColumn("DEFECTNAME", 50); // 불량명
            group.AddTextBoxColumn("QCSEGMENTNAME", 50).SetLabel("QCSEGMENT"); // 품질공정
            group.AddTextBoxColumn("PCSDEFECTQTY", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric); // 불량수
            group.AddTextBoxColumn("PCSDEFECTRATE", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric); // 불량율
            group.AddTextBoxColumn("PCSYIELDRATE", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric); // 수율

            group = gridDefectStatus.View.AddGroupColumn("GROUPAREAYIELDRATE");
            group.AddTextBoxColumn("AREAINPUTQTY", 50).SetLabel("INPUTM2").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.#0", MaskTypes.Numeric); // 투입면적
            group.AddTextBoxColumn("AREADEFECTQTY", 50).SetLabel("DEFECTAREAM2").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.#0", MaskTypes.Numeric); // 불량면적
            group.AddTextBoxColumn("AREADEFECTRATE", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric); // 불량면적
            group.AddTextBoxColumn("AREAYIELDRATE", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric); // 면적수율

            group = gridDefectStatus.View.AddGroupColumn(string.Concat("GROUPPRICEYIELDRATE", UserInfo.Current.Plant.Equals("YPE") ? "KRW" :
                                                                                              UserInfo.Current.Plant.Equals("YPEV") ? "VND" :
                                                                                              UserInfo.Current.Plant.Equals("CCT") ? "CNY" : "USD"));
            group.AddTextBoxColumn("PRICEINPUTQTY", 50).SetLabel("INPUTAMOUNT").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric); // 투입금액
            group.AddTextBoxColumn("PRICEDEFECTQTY", 50).SetLabel("DEFECTPRICE").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric); // 불량금액
            group.AddTextBoxColumn("PRICEYIELDRATE", 50).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric); // 금액수율

            group = gridDefectStatus.View.AddGroupColumn("");

            group.AddTextBoxColumn("INSPPROCESSSEGMENT", 100); // 검사공정
            group.AddTextBoxColumn("BARESMTTYPE", 80).SetLabel("DIVBARESMT"); // BASE/SMT 구분
            group.AddTextBoxColumn("SUMMARYDATE", 100).SetLabel("SUMDAY").SetTextAlignment(TextAlignment.Right); // 집계일자
            group.AddTextBoxColumn("PRODUCTDEFTYPE", 100).SetLabel("LOTPRODUCTTYPE"); // 양산구분

            gridDefectStatus.View.PopulateColumns();

            gridDefectStatus.View.SetIsReadOnly();
            gridDefectStatus.View.OptionsView.AllowCellMerge = true;

            #endregion 품목별 불량현황

            #region 품목별 불량세부

            gridDefectDetail.GridButtonItem = GridButtonItem.Export;
            gridDefectDetail.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            group = gridDefectDetail.View.AddGroupColumn("");
            group.AddTextBoxColumn("PRODUCTSHAPE", 70); // 제품타입
            group.AddComboBoxColumn("LAYER", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Layer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetEmptyItem("", ""); // 층수
            group.AddTextBoxColumn("COMPANYCLIENT", 50); // 고객사
            group.AddTextBoxColumn("PRODUCTDEFNAME", 100); // 품목명
            group.AddTextBoxColumn("PRODUCTDEFID", 100); // 품목코드
            group.AddTextBoxColumn("LOCALE", 50).SetIsHidden(); // 구분
            group.AddTextBoxColumn("SHIPMENTSITE", 65).SetIsHidden(); // 출하SITE
            group.AddTextBoxColumn("RELATEDSITE", 80).SetIsHidden(); // 연계SITE
            group.AddTextBoxColumn("PRODUCTDEFVERSION", 70); // 품목REV

            group = gridDefectDetail.View.AddGroupColumn("DEFECTINFO");
            group.AddTextBoxColumn("DISCOVERYSITE", 80).SetLabel("DEFECTFOUNDSITE"); // 발견 Site
            group.AddTextBoxColumn("PROCESSSEGMENTNAME", 100).SetLabel("PROCESSSEGMENTNAME"); // 발견 공정
            group.AddTextBoxColumn("PCSINPUTQTY", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric); // 투입수
            group.AddTextBoxColumn("DEFECTCODE", 50).SetLabel("DEFECTCODEID"); // 불량코드
            group.AddTextBoxColumn("DEFECTNAME", 50); // 불량명
            group.AddTextBoxColumn("QCSEGMENTID", 50).SetIsHidden(); // 품질공정ID
            group.AddTextBoxColumn("QCSEGMENTNAME", 50).SetLabel("QCSEGMENT"); // 품질공정
            group.AddTextBoxColumn("PCSDEFECTQTY", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric); // 불량수
            group.AddTextBoxColumn("PCSDEFECTRATE", 50).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric); // 불량율
            group.AddTextBoxColumn("DEFECTPRICE", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric); // 불량금액

            group = gridDefectDetail.View.AddGroupColumn("DEFECTCAUSEINFO");
            group.AddTextBoxColumn("REASONSITE", 50).SetLabel("SITE"); // Site
            group.AddTextBoxColumn("INPUTQTY", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric); // 투입수
            group.AddTextBoxColumn("DEFECTRATE", 50).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric); // 불량률
            group.AddTextBoxColumn("REASONCONSUMABLEDEFNAME", 50).SetLabel("REASONPRODUCT"); // 원인품목
            group.AddTextBoxColumn("REASONSEGMENTNAME", 50).SetLabel("CAUSEPROCESS"); // 원인 공정
            group.AddTextBoxColumn("REASONAREANAME", 50).SetLabel("REASONAREA"); // 원인 작업장

            group = gridDefectDetail.View.AddGroupColumn("");
            group.AddTextBoxColumn("INSPPROCESSSEGMENT", 100); // 검사공정
            group.AddTextBoxColumn("BARESMTTYPE", 100).SetLabel("DIVBARESMT"); // BASE/SMT 구분
            group.AddTextBoxColumn("SUMMARYDATE", 100).SetLabel("SUMDAY"); // 집계일자
            group.AddTextBoxColumn("PRODUCTDEFTYPE", 100).SetLabel("LOTPRODUCTTYPE"); // 양산구분

            gridDefectDetail.View.PopulateColumns();

            gridDefectDetail.View.SetIsReadOnly();

            #endregion 품목별 불량세부
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            smartTabControl1.SelectedPageChanged += (s, e) =>
            {
                if (smartTabControl1.SelectedTabPageIndex != 0)
                {
                    Conditions.GetControl<SmartComboBox>("P_SHOWINTERSECT").Enabled = false;
                    Conditions.GetControl<SmartComboBox>("P_PROCESSTYPE").Enabled = false;
                }
                else
                {
                    Conditions.GetControl<SmartComboBox>("P_SHOWINTERSECT").Enabled = true;
                    Conditions.GetControl<SmartComboBox>("P_PROCESSTYPE").Enabled = true;
                }
            };

            // 조회조건 '구분'의 값이 Local인 경우 조회조건 '연계SITE'항목 선택
            Conditions.GetControl<SmartComboBox>("P_LOCALEDIV").EditValueChanged += (s, e) =>
            {
                Conditions.GetControl<SmartCheckedComboBox>("P_LINKPLANTID").EditValue = "*";
                Conditions.GetControl<SmartCheckedComboBox>("P_LINKPLANTID").Enabled = !(s as SmartComboBox).EditValue.Equals("LOCAL");
            };

            // 그리드 셀 병합
            gridDefectStatus.View.CellMerge += (s, e) =>
            {
                if (e.Column.FieldName.Equals("COMPANYCLIENT"))
                {
                    var dr1 = gridDefectStatus.View.GetDataRow(e.RowHandle1); //위에 행 정보
                    var dr2 = gridDefectStatus.View.GetDataRow(e.RowHandle2); //아래 행 정보

                    //비교하는 이유 그래야 정상적으로 나옴.
                    e.Merge = dr1["COMPANYCLIENT"].ToString().Equals(dr2["COMPANYCLIENT"].ToString());
                }
                else if (e.Column.FieldName.Equals("PRODUCTDEFNAME"))
                {
                    var dr1 = gridDefectStatus.View.GetDataRow(e.RowHandle1); //위에 행 정보
                    var dr2 = gridDefectStatus.View.GetDataRow(e.RowHandle2); //아래 행 정보

                    //비교하는 이유 그래야 정상적으로 나옴.
                    e.Merge = dr1["PRODUCTDEFNAME"].ToString().Equals(dr2["PRODUCTDEFNAME"].ToString());
                }
                else if (e.Column.FieldName.Equals("PRODUCTDEFID"))
                {
                    var dr1 = gridDefectStatus.View.GetDataRow(e.RowHandle1); //위에 행 정보
                    var dr2 = gridDefectStatus.View.GetDataRow(e.RowHandle2); //아래 행 정보

                    //비교하는 이유 그래야 정상적으로 나옴.
                    e.Merge = dr1["PRODUCTDEFID"].ToString().Equals(dr2["PRODUCTDEFID"].ToString());
                }
                else if (e.Column.FieldName.Equals("PRODUCTDEFVERSION"))
                {
                    var dr1 = gridDefectStatus.View.GetDataRow(e.RowHandle1); //위에 행 정보
                    var dr2 = gridDefectStatus.View.GetDataRow(e.RowHandle2); //아래 행 정보

                    //비교하는 이유 그래야 정상적으로 나옴.
                    e.Merge = dr1["PRODUCTDEFVERSION"].ToString().Equals(dr2["PRODUCTDEFVERSION"].ToString());
                }
                else
                {
                    e.Merge = false;
                }

                e.Handled = true;
            };

            // Custom Cell Value
            gridDefectStatus.View.CustomColumnDisplayText += (s, e) =>
            {
                if (e.Column.FieldName == "PCSYIELDRATE" || e.Column.FieldName == "AREAYIELDRATE" || e.Column.FieldName == "PRICEYIELDRATE"
                   || e.Column.FieldName == "PCSINPUTQTY" || e.Column.FieldName == "AREAINPUTQTY" || e.Column.FieldName == "PRICEINPUTQTY")
                {
                    if (gridDefectStatus.View.GetRowCellValue(e.ListSourceRowIndex, "LV").ToSafeDoubleNaN().Equals(0))
                    {
                        e.DisplayText = "";
                    }
                }
            };

            // 품목별 수율 Grid Double lick
            gridYieldRate.View.DoubleClick += (s, e) =>
            {
                GridView view = s as GridView;
                GridHitInfo info = view.CalcHitInfo((e as DXMouseEventArgs).Location);
                if (info.InRow || info.InRowCell)
                {
                    string strProductDefID = view.GetRowCellValue(info.RowHandle, "PRODUCTDEFID").ToString() + view.GetRowCellValue(info.RowHandle, "PRODUCTDEFVERSION").ToString().Split(' ')[0];

                    Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValue = strProductDefID;

                    smartTabControl1.SelectedTabPage = tapPageItemDefectStatus;
                    SendKeys.Send("{F5}");
                }
            };

            // 품목별 불량현황 Grid Double lick
            gridDefectStatus.View.DoubleClick += (s, e) =>
            {
                GridView view = s as GridView;
                GridHitInfo info = view.CalcHitInfo((e as DXMouseEventArgs).Location);
                if (info.InRow || info.InRowCell)
                {
                    string strProductDefID = view.GetRowCellValue(info.RowHandle, "PRODUCTDEFID").ToString() + view.GetRowCellValue(info.RowHandle, "PRODUCTDEFVERSION").ToString().Split(' ')[0];

                    Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValue = strProductDefID;

                    smartTabControl1.SelectedTabPage = tapPageItemDefectDetail;
                    SendKeys.Send("{F5}");
                }
            };

            // 품목별 불량세부 Grid Double lick
            gridDefectDetail.View.DoubleClick += (s, e) =>
            {
                GridView view = s as GridView;
                GridHitInfo info = view.CalcHitInfo((e as DXMouseEventArgs).Location);
                if (info.InRow || info.InRowCell)
                {
                    string strProductDefID = view.GetRowCellValue(info.RowHandle, "PRODUCTDEFID").ToString();
                    string strProdeuctDefVer = view.GetRowCellValue(info.RowHandle, "PRODUCTDEFVERSION").ToString().Split(' ')[0];
                    string strProdeuctNm = view.GetRowCellValue(info.RowHandle, "PRODUCTDEFNAME").ToString();

                    var parameters = Conditions.GetValues();

                    if (parameters.ContainsKey("PRODUCTDEFID"))
                    {
                        parameters["PRODUCTDEFID"] = strProductDefID;
                    }
                    else
                    {
                        parameters.Add("PRODUCTDEFID", strProductDefID);
                    }

                    if (parameters.ContainsKey("PRODUCTDEFVERSION"))
                    {
                        parameters["PRODUCTDEFVERSION"] = strProdeuctDefVer;
                    }
                    else
                    {
                        parameters.Add("PRODUCTDEFVERSION", strProdeuctDefVer);
                    }

                    if (parameters.ContainsKey("P_PRODUCTNAME"))
                    {
                        parameters["P_PRODUCTNAME"] = strProdeuctNm;
                    }
                    else
                    {
                        parameters.Add("P_PRODUCTNAME", strProdeuctNm);
                    }

                    parameters.Add("Tab", "Detail");

                    string strLevel = view.GetRowCellValue(info.RowHandle, "LV").ToString();
                    if (strLevel.ToNullOrDouble() == null || strLevel.ToNullOrDouble() != 0)
                    {
                        return;
                    }

                    OpenMenu("PG-QC-0470", parameters);
                }
            };

            // 품목별 수율 Context Menu 이벤트
            gridYieldRate.InitContextMenuEvent += (s, e) =>
            {
                // LOT 별 수율현황
                e.AddMenus.Add(new DXMenuItem(Language.Get("MENU_PG-QC-0470"), OpenForm) { BeginGroup = true, Tag = "PG-QC-0470" });
                // 이상발생현황
                e.AddMenus.Add(new DXMenuItem(Language.Get("MENU_PG-SG-0450"), OpenForm) { Tag = "PG-SG-0450" });
                // Affect LOT 산정(출)
                e.AddMenus.Add(new DXMenuItem(Language.Get("MENU_PG-QC-0556"), OpenForm) { Tag = "PG-QC-0556" });
            };

            // 품목별 불량현황 Context Menu 이벤트
            gridDefectStatus.InitContextMenuEvent += (s, e) =>
            {
                e.AddMenus.Add(new DXMenuItem(Language.Get("MENU_PG-QC-0470"), OpenForm) { BeginGroup = true, Tag = "PG-QC-0470" });
            };

            gridDefectDetail.InitContextMenuEvent += (s, e) =>
            {
                e.AddMenus.Add(new DXMenuItem(Language.Get("MENU_PG-QC-0470"), OpenFormDetail) { BeginGroup = true, Tag = "PG-QC-0470" });
            };

            //gridYieldRate.View.RowCellStyle += View_RowCellStyle;
            gridDefectStatus.View.RowCellStyle += View_RowCellStyle;
            gridDefectDetail.View.RowCellStyle += View_RowCellStyle;
        }

        private void View_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            DataRowView row = (sender as GridView).GetRow(e.RowHandle) as DataRowView;

            if (smartTabControl1.SelectedTabPageIndex.Equals(0) && Format.GetInteger(row["LV"]).Equals(0))
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml("#D8DADE");
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                //e.Appearance.ForeColor = Color.Black;
            }
            else if (row["LV"].ToString().ToInt32() > 1)
            {
                e.Appearance.BackColor = Color.LightSkyBlue;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        private void OpenFormDetail(object sender, EventArgs args)
        {
            if(gridDefectDetail.View.GetFocusedDataRow() is DataRow dr)
            {
                string strProductDefID = Format.GetString(dr["PRODUCTDEFID"], string.Empty);
                string strProdeuctDefVer = Format.GetString(dr["PRODUCTDEFVERSION"], string.Empty);
                string strProdeuctNm = Format.GetString(dr["PRODUCTDEFNAME"], string.Empty);
                string strdefectCode = Format.GetString(dr["DEFECTCODE"], string.Empty);
                string strSegment = Format.GetString(dr["QCSEGMENTID"], string.Empty);
                string strLevel = Format.GetString(dr["LV"], string.Empty);

                var parameters = Conditions.GetValues();

                if (parameters.ContainsKey("PRODUCTDEFID"))
                {
                    parameters["PRODUCTDEFID"] = strProductDefID;
                }
                else
                {
                    parameters.Add("PRODUCTDEFID", strProductDefID);
                }

                if (parameters.ContainsKey("PRODUCTDEFVERSION"))
                {
                    parameters["PRODUCTDEFVERSION"] = strProdeuctDefVer;
                }
                else
                {
                    parameters.Add("PRODUCTDEFVERSION", strProdeuctDefVer);
                }

                if (parameters.ContainsKey("P_PRODUCTNAME"))
                {
                    parameters["P_PRODUCTNAME"] = strProdeuctNm;
                }
                else
                {
                    parameters.Add("P_PRODUCTNAME", strProdeuctNm);
                }

                if (parameters.ContainsKey("P_QCSEGMENTID"))
                {
                    parameters["P_QCSEGMENTID"] = strSegment;
                }
                else
                {
                    parameters.Add("P_QCSEGMENTID", strSegment);
                }

                if (parameters.ContainsKey("P_DEFECTCODE"))
                {
                    parameters["P_DEFECTCODE"] = strdefectCode;
                }
                else
                {
                    parameters.Add("P_DEFECTCODE", strdefectCode);
                }

                parameters.Add("Tab", "Detail");

                if (strLevel.ToNullOrDouble() == null || strLevel.ToNullOrDouble() != 0)
                {
                    return;
                }

                OpenMenu("PG-QC-0470", parameters);
            }
        }

        // Context Popup Form Event
        private void OpenForm(object sender, EventArgs args)
        {
            try
            {
                DialogManager.ShowWaitDialog();

                SmartBandedGrid grid = smartTabControl1.SelectedTabPage.Equals(tapPageItemYieldRate) ? gridYieldRate : gridDefectStatus;
                DataRow currentRow = grid.View.GetFocusedDataRow();

                string menuId = (sender as DXMenuItem).Tag.ToString();

                var values = Conditions.GetValues();
                var param = currentRow.Table.Columns
                    .Cast<DataColumn>()
                    .ToDictionary(col => col.ColumnName, col => currentRow[col.ColumnName]);

                // DEFECT MAP 화면 이동
                if (values.ContainsKey("PRODUCTDEFVERSION"))
                {
                    values["PRODUCTDEFVERSION"] = param["PRODUCTDEFVERSION"] != null ? param["PRODUCTDEFVERSION"].ToString() : "";
                }
                else
                {
                    values.Add("PRODUCTDEFVERSION", param["PRODUCTDEFVERSION"] != null ? param["PRODUCTDEFVERSION"].ToString() : "");
                }

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

                if (param["LV"].ToNullOrDouble() == null || param["LV"].ToNullOrDouble() != 0)
                {
                    return;
                }

                OpenMenu(menuId, values);
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

        public override void LoadForm(Dictionary<string, object> parameters)
        {
            base.LoadForm(parameters);

            if (parameters != null)
            {
                if (Conditions.GetCondition<ConditionItemComboBox>("P_LOCALEDIV").DefaultValue.Equals("LOCAL"))
                {
                    Conditions.GetControl<SmartCheckedComboBox>("P_LINKPLANTID").Enabled = false;
                }
                else
                {
                    Conditions.GetControl<SmartCheckedComboBox>("P_LINKPLANTID").EditValue = parameters["P_LINKPLANTID"].ToString();
                }

                Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(parameters["P_PRODUCTDEFID"].ToString());
                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = Format.GetString(parameters["P_PRODUCTNAME"]);
                Conditions.GetControl<SmartComboBox>("P_PLANTID").EditValue = parameters["P_PLANTID"].ToString();
                SmartPeriodEdit period = Conditions.GetControl<SmartPeriodEdit>("P_PERIOD");
                period.datePeriodFr.EditValue = parameters["P_PERIOD_PERIODFR"];
                period.datePeriodTo.EditValue = parameters["P_PERIOD_PERIODTO"];

                if (parameters.ContainsKey("P_INSPTYPE"))
                    Conditions.GetControl<SmartComboBox>("P_INSPTYPE").EditValue = parameters["P_INSPTYPE"].ToString();

                if (parameters.ContainsKey("P_PLANTID"))
                    Conditions.GetControl<SmartComboBox>("P_PLANTID").EditValue = parameters["P_PLANTID"].ToString();

                if (parameters.ContainsKey("P_PRODUCTIONDIVISION"))
                    Conditions.GetControl<SmartComboBox>("P_PRODUCTIONDIVISION").EditValue = parameters["P_PRODUCTIONDIVISION"].ToString();

                if (parameters.ContainsKey("P_LOTSTANDARD"))
                    Conditions.GetControl<SmartComboBox>("P_LOTSTANDARD").EditValue = parameters["P_LOTSTANDARD"].ToString();

                if (parameters.ContainsKey("P_BARESMTTYPE"))
                    Conditions.GetControl<SmartComboBox>("P_BARESMTTYPE").EditValue = parameters["P_BARESMTTYPE"].ToString();

                if (parameters.ContainsKey("P_INSPECTIONPROCESS"))
                    Conditions.GetControl<SmartComboBox>("P_INSPECTIONPROCESS").EditValue = parameters["P_INSPECTIONPROCESS"].ToString();

                if (parameters.ContainsKey("P_PRODSHAPETYPE"))
                    Conditions.GetControl<SmartComboBox>("P_PRODSHAPETYPE").EditValue = parameters["P_PRODSHAPETYPE"].ToString();

                SendKeys.Send("{F5}");
            }
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

                string lotCreateType = string.Empty;

                Dictionary<string, object> values = Conditions.GetValues();

                // 2021.01.26 전우성 양산구분을 CheckCombo로 변경요청
                // 기존 MRB인 경우 Query부분에서 처리가 달라 P_LOTCREATEDTYPE 파라미터를 생성하여 MRB인 경우 로직 추가
                if (!Format.GetString(values["P_PRODUCTIONDIVISION"], string.Empty).Equals("*"))
                {
                    if (!Format.GetString(values["P_PRODUCTIONDIVISION"], string.Empty).Split(',').Where(x => x.Equals("MRB")).Count().Equals(0))
                    {
                        lotCreateType = "MRB";
                    }
                }

                values.Add("P_LOTCREATEDTYPE", lotCreateType);
                values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                values.Add("PLANT", UserInfo.Current.Plant);

                SmartBandedGrid grid = smartTabControl1.SelectedTabPage.Equals(tapPageItemYieldRate) ? gridYieldRate :
                                       smartTabControl1.SelectedTabPage.Equals(tapPageItemDefectStatus) ? gridDefectStatus : gridDefectDetail;

                string query = smartTabControl1.SelectedTabPage.Equals(tapPageItemYieldRate) ? "SelectYieldRateByProductItemYPE" :
                               smartTabControl1.SelectedTabPage.Equals(tapPageItemDefectStatus) ? "SelectDefectStatusByProductItemYPE" : "SelectDefectDetailByProductItemYPE";

                grid.View.ClearDatas();

                if (await SqlExecuter.QueryAsync(query, "10001", values) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData");
                        return;
                    }

                    grid.DataSource = dt;
                    grid.View.BestFitColumns(true);
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

            #region 품목

            var condition = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductListByYieldStatus", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFIDVERSION", "PRODUCTDEFIDVERSION")
                                      .SetLabel("PRODUCTDEFID")
                                      .SetPopupLayout("GRIDPRODUCTLIST", PopupButtonStyles.Ok_Cancel, true, false)
                                      .SetPopupLayoutForm(600, 400, FormBorderStyle.FixedToolWindow)
                                      .SetPosition(Conditions.GetCondition<ConditionItemTextBox>("P_PRODUCTNAME").Position - 0.1)
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
                                      })
                                      .SetRelationIds("P_CUSTOMER");

            condition.Conditions.AddTextBox("PRODUCTDEFID").SetLabel("PRODUCTDEFID");

            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 120).SetLabel("PRODUCTDEFID");
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 250).SetLabel("PRODUCTDEFNAME");
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100).SetLabel("PRODUCTDEFVERSION");

            #endregion 품목

            #region 고객사

            condition = Conditions.AddSelectPopup("P_CUSTOMER", new SqlQuery("GetCustomerListByYield", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID")
                                  .SetLabel("CUSTOMER")
                                  .SetPopupLayout("CUSTOMER", PopupButtonStyles.Ok_Cancel, true, false)
                                  .SetPopupLayoutForm(600, 400, FormBorderStyle.FixedToolWindow)
                                  .SetPopupResultCount(0)
                                  .SetPosition(Conditions.GetCondition<ConditionItemComboBox>("P_PRODSHAPETYPE").Position + 0.1)
                                  .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                  {
                                      foreach (DataRow dr in selectedRow)
                                      {
                                          if (!DefectMapHelper.IsNull(dr.GetObject("P_PRODUCTDEFID")))
                                          {
                                              Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(dr["PRODUCTDEFID"]);

                                              Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = dr["PRODUCTDEFNAME"].ToString();
                                          }
                                      }
                                  });

            condition.Conditions.AddTextBox("CUSTOMERNAME").SetLabel("CUSTOMER");

            condition.GridColumns.AddTextBoxColumn("CUSTOMERID", 120).SetLabel("CUSTOMERID");
            condition.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 250).SetLabel("CUSTOMERNAME");

            #endregion 고객사
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            Conditions.GetCondition<ConditionItemTextBox>("P_PRODUCTNAME").SetIsReadOnly();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += (s, e) =>
            {
                var values = Conditions.GetValues();
                string strValue = Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValue.ToString();

                if (string.IsNullOrEmpty(strValue))
                {
                    Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = "";
                }
            };
        }

        #endregion 검색
    }
}