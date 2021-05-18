#region using

using DevExpress.XtraCharts;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : ex> 시스템관리 > 코드 관리 > 코드그룹 정보
    /// 업  무  설  명  : ex> 시스템에서 공통으로 사용되는 코드그룹 정보를 관리한다.
    /// 생    성    자  : 홍길동
    /// 생    성    일  : 2019-05-14
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class YieldStatus : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        string _Text;                                       // 설명
        SmartBandedGrid grdList = new SmartBandedGrid();    // Main Grid

        /// <summary>
        /// 선택된 불량코드 DataTable
        /// </summary>
        private DataTable _DefectList;

        /// <summary>
        /// 조회 조건 품목에서 찾은 정보 Row
        /// </summary>
        private DataRow _selectedRow;

        /// <summary>
        /// 마우스 더블 클릭 이벤트 발생 체크
        /// </summary>
        bool isDoubleClick = false;

        /// <summary>
        /// 팝업에서 선택된 데이터
        /// </summary>
        DataTable _checked;

        #endregion

        #region 생성자

        public YieldStatus()
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

            if (!smartTabControl1.IsDesignMode())
            {
                InitializeEvent();

                // TODO : 컨트롤 초기화 로직 구성
                InitializeGrid();
            }
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {

            // TODO : 그리드 초기화 로직 추가
            //grdList.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore;
            //grdList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            //// 코드그룹ID
            //grdList.View.AddTextBoxColumn("CODECLASSID", 150)
            //    .SetValidationIsRequired();
            //// 코드그룹명
            //grdList.View.AddTextBoxColumn("CODECLASSNAME", 200);

            //grdList.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            //grdList.View.AddingNewRow += View_AddingNewRow;

            this.smartTabControl1.SelectedPageChanged += SmartTabControl1_SelectedPageChanged;

            #region 일별 탭 이벤트

            tabUCDayYieldRate.ChartMonth.MouseDoubleClick += ChartPeriod_MouseDoubleClick;
            tabUCDayYieldRate.ChartWeek.MouseDoubleClick += ChartPeriod_MouseDoubleClick;
            tabUCDayYieldRate.ChartDay.MouseDoubleClick += ChartPeriod_MouseDoubleClick;

            //tabUCDayYieldRate.ChartDay.DoubleClick += ChartDay_DoubleClick;
            tabUCDayYieldRate.GridYield.View.DoubleClick += GridView_DoubleClick;

            #endregion

            #region 품목별 탭 이벤트

            tabUCItemYieldRate.ChartItemYield.DoubleClick += daySelectionPopup_DoubleClick;
            tabUCItemYieldRate.ChartLotYield.DoubleClick += chartLotSelectionPopup_DoubleClick;

            tabUCItemYieldRate.CboItemYieldChartViewType.EditValueChanged += CboItemYieldChartViewType_EditValueChanged;

            #endregion

            #region LOT별 탭 이벤트

            tabUCLotYieldRate.ChartDefectCodeRate.MouseDoubleClick += ChartDefectCodeRate_MouseDoubleClick;

            #endregion

            #region LOT분석 버튼 이벤트

            this.tabUCLotYieldRate.ButtonLOTAnalysis.Click += (s, e) => {
                var values = Conditions.GetValues();

                SmartChart chart = tabUCLotYieldRate.ChartLotYieldRate;
                string strArgs = string.Empty;

                foreach (Series ser in chart.SelectedItems)
                {
                    if (string.IsNullOrEmpty(strArgs))
                        strArgs = ser.Name;
                    else
                        strArgs = strArgs + "," + ser.Name;
                }

                if (string.IsNullOrEmpty(strArgs))
                {
                    this.ShowMessage("NoSelectData");
                    return;
                }

                    values.Add("P_LOTS", strArgs);

                    this.OpenMenu("PG-QC-0554", values);

            };

            #endregion
        }

        /// <summary>
        /// 수율현황 : LOT별 수율, 불량차트 더블 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChartDefectCodeRate_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SmartChart chart = sender as SmartChart;

            ChartHitInfo info = chart.CalcHitInfo(e.X, e.Y);

            string strInfo = info.HitObject.ToString();
            string strFilter = string.Format("DEFECTCODENAME='{0}'", strInfo);

            DataTable dt = chart.DataSource as DataTable;
            dt.DefaultView.RowFilter = strFilter;

            List<string> list = dt.DefaultView.ToTable(true, new string[] { "DEFECTCODE" }).Rows.OfType<DataRow>().Select(dr => (string)dr["DEFECTCODE"]).ToList();
            string strCode = string.Join(",", list);
            strCode = strCode.Replace(':', '-');

            Conditions.GetControl<SmartSelectPopupEdit>("P_DEFECTCODE").SetValue(strCode);
            Conditions.GetControl<SmartSelectPopupEdit>("P_DEFECTCODE").EditValue = strInfo;

            OnSearchAsync();
        }

        private void SmartTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if(e.PrevPage.Name != e.Page.Name)
            {
                if(e.Page.Name.Equals("tabPageLotYieldRate"))
                    setCondtionControlDefectCode(true);
                else
                    setCondtionControlDefectCode(false);

                if (e.Page.Name.Equals("tabPageLotYieldRate"))
                {
                    Conditions.GetCondition("P_PRODUCTDEFID").SetValidationIsRequired();
                }
                else if(e.Page.Name.Equals("tabPageItemYieldRate"))
                {
                    string strChartType = tabUCItemYieldRate.CboItemYieldChartViewType.EditValue.ToString();
                    if(strChartType.Equals("Daily"))
                        Conditions.GetCondition("P_PRODUCTDEFID").SetValidationIsRequired();
                    else
                        Conditions.GetCondition("P_PRODUCTDEFID").IsRequired = false;
                }
                else
                    Conditions.GetCondition("P_PRODUCTDEFID").IsRequired = false;

                if (this.isDoubleClick)
                {
                    isDoubleClick = false;

                    SmartSelectPopupEdit ctrlProdID = Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID");
                    string strProdID = ctrlProdID.GetValue().ToString();

                    if (Conditions.GetCondition("P_PRODUCTDEFID").IsRequired)
                    {
                        if (!string.IsNullOrEmpty(strProdID))
                            this.OnSearchAsync();
                        else
                             ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                    }
                    else
                        this.OnSearchAsync();
                }
            }
        }

        private void ChartPeriod_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ChartControl chart = sender as ChartControl;
            this.isDoubleClick = true;

            if (chart.SelectedItems.Count > 0)
            {
                ChartHitInfo hi = chart.CalcHitInfo(e.Location);
                SeriesPoint pt = hi.SeriesPoint;

                if (!string.IsNullOrEmpty(pt.Argument))
                {
                    DateTime dateStart = new DateTime();
                    DateTime dateEnd = new DateTime();

                    if (chart.Name.Equals("chartYiedRateMonth"))
                    {
                        string[] arrArgs = pt.Argument.Split(' ');
                        if (arrArgs.Length > 1)
                        {
                            dateStart = new DateTime(Convert.ToInt32(arrArgs[0].Substring(0, 4)), Convert.ToInt32(arrArgs[1].Substring(0, 2)), 1);
                            dateStart = dateStart.AddHours(8).AddMinutes(30);
                            dateEnd = dateStart.AddMonths(1);
                        }
                    }
                    else if (chart.Name.Equals("chartYiedRateWeek"))
                    {
                        string[] arrArgs = pt.Argument.Split(' ');
                        DateTime year = Convert.ToDateTime(arrArgs[0].Substring(0, 4) + "-01-01");
                        dateStart = GetFirstDateOfWeek(year.Year, Convert.ToInt32(arrArgs[1].Substring(0, 2)));

                        // GetFirstDateOfWeek 년의 시작이 0주차여서 일주일 차감하여 계산
                        dateStart = dateStart.AddDays(-7).AddHours(8).AddMinutes(30);
                        dateEnd = dateStart.AddDays(7);
                    }
                    else
                    {
                        string[] arrArgs = pt.Argument.Split('-');

                        if (arrArgs.Length > 2)
                        {
                            dateStart = new DateTime(Convert.ToInt32(arrArgs[0]), Convert.ToInt32(arrArgs[1]), Convert.ToInt32(arrArgs[2]));
                            dateStart = dateStart.AddHours(8).AddMinutes(30);
                            dateEnd = dateStart.AddDays(1);
                        }
                    }
                    //Annotation an = hi.Annotation;

                    SmartPeriodEdit condPeriod = Conditions.GetControl<SmartPeriodEdit>("P_PERIOD");
                    condPeriod.datePeriodFr.EditValue = dateStart;
                    condPeriod.datePeriodTo.EditValue = dateEnd;
                    
                    //var cntls = Conditions.GetControl<SmartPeriodEdit>("P_PERIOD").Controls;
                }

                this.smartTabControl1.SelectedTabPage = tabPageItemYieldRate;
            }
            else
                this.isDoubleClick = false;
        }

        #region 특정 주차 시작일 구하기 - GetFirstDateOfWeek(year, week)

        /// <summary>
        /// 특정 주차 시작일 구하기
        /// </summary>
        /// <param name="year">연도</param>
        /// <param name="week">주차</param>
        /// <returns>특정 주차 시작일</returns>
        public DateTime GetFirstDateOfWeek(int year, int week)
        {
            DateTime firstDateOfYear = new DateTime(year, 1, 1);
            DateTime firstDateOfFirstWeek = firstDateOfYear.AddDays(7 - (int)(firstDateOfYear.DayOfWeek) + 1);
            return firstDateOfFirstWeek.AddDays(7 * (week - 1));
        }

        #endregion

        /// <summary>
        /// 수율현황 - 품목별 수율 유저 컨트롤 기간합계/일별 콤보박스 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboItemYieldChartViewType_EditValueChanged(object sender, EventArgs e)
        {
            SmartComboBox comboBox = sender as SmartComboBox;

            if (this.smartTabControl1.SelectedTabPage == tabPageItemYieldRate)
            {
                string strChartType = comboBox.EditValue.ToString();
                if (strChartType.Equals("Daily"))
                {
                    if (Conditions.GetCondition("P_PRODUCTDEFID").IsRequired == false)
                    {
                        Conditions.GetCondition("P_PRODUCTDEFID").SetValidationIsRequired();
                    }
                }
                else
                {
                    Conditions.GetCondition("P_PRODUCTDEFID").IsRequired = false;
                }
            }
        }

        /// <summary>
        /// 일별수율 탭의 그리드 더블클릭 이벤트 처리
        /// 품목수율 탭으로 이동
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView_DoubleClick(object sender, EventArgs e)
        {
            this.smartTabControl1.SelectedTabPage = tabPageItemYieldRate;
        }

        private void daySelectionPopup_DoubleClick(object sender, EventArgs e)
        {
            ChartControl chart = sender as ChartControl;
            if (chart.SelectedItems.Count > 0)
            {
                this.smartTabControl1.SelectedTabPage = tabPageLotYieldRate;

                string strArgs = string.Empty;
                if (tabUCItemYieldRate.ItemComboValue.ToString().Equals("Daily"))
                {
                    // 일별일 경우, 품목코드가 조회조건에 반듯이 들어감
                    var condVals = Conditions.GetValues();
                    string txtPeriod = condVals["P_PERIOD_PERIODFR"].ToString().Substring(0, 10) + " ~ " + condVals["P_PERIOD_PERIODTO"].ToString().Substring(0, 10);

                    this.tabUCLotYieldRate.PeriodText = txtPeriod;

                    if (condVals.ContainsKey("P_PRODUCTDEFID") && string.IsNullOrEmpty(condVals["P_PRODUCTDEFID"].ToString()))
                    {
                        foreach (SeriesPoint sp in chart.SelectedItems)
                        {
                            if (string.IsNullOrEmpty(strArgs))
                                strArgs = sp.Argument;
                            else
                                strArgs = strArgs + "," + sp.Argument;
                        }

                        Dictionary<string, object> values = new Dictionary<string, object>();
                        values.Add("ITEMIDS", strArgs);

                        DataTable dt = SqlExecuter.Query("GetProductListByYieldStatus", "10002", values);
                        List<string> lstItems = dt.AsEnumerable().Select(x => x.Field<string>("PRODUCTDEFIDVERSION")).Distinct().ToList();
                        List<string> lstNames = dt.AsEnumerable().Select(x => x.Field<string>("PRODUCTDEFNAME")).Distinct().ToList();
                        string strProductID = string.Join(",", lstItems);
                        string strProductNames = string.Join(",", lstNames);
                        Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(strProductID);
                        Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = strProductNames;

                        this.tabUCLotYieldRate.ItemText = strProductNames;
                    }
                }
                else
                {
                    foreach (SeriesPoint sp in chart.SelectedItems)
                    {
                        if (string.IsNullOrEmpty(strArgs))
                            strArgs = sp.Argument;
                        else
                            strArgs = strArgs + "," + sp.Argument;
                    }

                    Dictionary<string, object> values = new Dictionary<string, object>();
                    values.Add("ITEMIDS", strArgs);

                    DataTable dt = SqlExecuter.Query("GetProductListByYieldStatus", "10002", values);
                    List<string> lstItems = dt.AsEnumerable().Select(x => x.Field<string>("PRODUCTDEFIDVERSION")).Distinct().ToList();
                    List<string> lstNames = dt.AsEnumerable().Select(x => x.Field<string>("PRODUCTDEFNAME")).Distinct().ToList();
                    string strProductID = string.Join(",", lstItems);
                    string strProductNames = string.Join(",", lstNames);
                    Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(strProductID);
                    Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = strProductNames;

                    this.tabUCLotYieldRate.ItemText = strProductNames;

                    var condVals = Conditions.GetValues();
                    string txtPeriod = condVals["P_PERIOD_PERIODFR"].ToString().Substring(0, 10) + " ~ " + condVals["P_PERIOD_PERIODTO"].ToString().Substring(0, 10);

                    this.tabUCLotYieldRate.PeriodText = txtPeriod;
                }

                OnSearchAsync();
            }
            else
            {
                if (tabUCItemYieldRate.ItemComboValue.ToString().Equals("Daily"))
                {
                    DaySelectPopup popup = new DaySelectPopup();
                    popup.StartPosition = FormStartPosition.CenterParent;
                    //popup.CurrentDataRow = CurrentDataRow;
                    var values = Conditions.GetValues();

                    values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                    popup.Conditions = values;

                    popup.ShowDialog();
                    if (popup.DialogResult == DialogResult.OK)
                    {
                        //_checked = popup._checkTable;
                        //var lotList = (grdNCRProgress.DataSource as DataTable).AsEnumerable().Select(r => r["LOTID"]).ToList();
                        //string lotId = "";
                        //foreach (DataRow row in _checked.Rows)
                        //{
                        //    lotId = row["LOTID"].ToString();
                        //    if (!lotList.Contains(lotId))
                        //    {
                        //        (grdNCRProgress.DataSource as DataTable).ImportRow(row);
                        //    }
                        //}

                        OpenMenu("PG-QC-0554", popup.Conditions);
                    }
                }
            }
        }

        private void chartLotSelectionPopup_DoubleClick(object sender, EventArgs e)
        {
            LotSelectPopup popup = new LotSelectPopup();
            popup.StartPosition = FormStartPosition.CenterParent;

            #region 팝업 설정

            var values = Conditions.GetValues();
            popup.SearchConditions = values;

            popup.FromTime = Convert.ToDateTime(values["P_PERIOD_PERIODFR"]);
            popup.ToTime = Convert.ToDateTime(values["P_PERIOD_PERIODTO"]);

            string txtPeriod = values["P_PERIOD_PERIODFR"].ToString().Substring(0, 10) + " ~ " + values["P_PERIOD_PERIODTO"].ToString().Substring(0, 10);
            popup.StrPeriod = txtPeriod;

            popup.ProductDefID = values["P_PRODUCTDEFID"].ToString();

            DataTable dt = tabUCItemYieldRate.ChartLotYield.DataSource as DataTable;
            //popup.ParentDataTable = dt.Copy();

            dt = dt.DefaultView.ToTable(true, "LOTID");
            popup.LotIDs = string.Join(",", dt.Rows.OfType<DataRow>().Select(r => r["LOTID"].ToString()));
            
            #endregion

            popup.ShowDialog();
            if (popup.DialogResult == DialogResult.OK)
            {
                #region LOT 선택 팝업 선택 결과에 의한 화면 처리

                _checked = popup._checkTable;

                if (_checked.Rows.Count > 0)
                {
                    string strLot = string.Join(",", _checked.Rows.OfType<DataRow>().Select(r => r["LOTID"].ToString()));
                    string strProdName = string.Join(",", _checked.Rows.OfType<DataRow>().Select(r => r["PRODUCTDEFNAME"].ToString()));

                    values.Add("P_LOTS", strLot);
                    values.Add("P_PRODUCTDEFNAME", strProdName);
                    if (!string.IsNullOrEmpty(strProdName))
                    {
                        if (values.ContainsKey("P_PRODUCTNAME"))
                            values["P_PRODUCTNAME"] = strProdName;
                        else
                            values.Add("P_PRODUCTNAME", strProdName);
                    }

                    if (!string.IsNullOrEmpty(popup.ProductDefID))
                    {
                        if (values.ContainsKey("P_PRODUCTDEFID"))
                            values["P_PRODUCTDEFID"] = popup.ProductDefID;
                        else
                            values.Add("P_PRODUCTDEFID", popup.ProductDefID);
                    }

                    this.OpenMenu("PG-QC-0554", values);
                }

                #endregion
            }
        }

        /// <summary>
        /// 품목코드 변경시 처리 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchOption_ProductDef_EditValueChanged(object sender, EventArgs e)
        {
            var values = Conditions.GetValues();
            string strValue = Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValue.ToString();   // values["P_PRODUCTDEFID"].ToString();

            if (string.IsNullOrEmpty(strValue))
            {
                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = "";
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

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            DataTable changed = grdList.GetChangedRows();

            ExecuteRule("SaveCodeClass", changed);
        }

        #endregion

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

                // TODO : 조회 SP 변경
                var values = Conditions.GetValues();
                DataTable dt;
                //dt = await SqlExecuter.QueryAsync("GetDefectStatus", "10001", values);
                string txtPeriod = values["P_PERIOD_PERIODFR"].ToString().Substring(0, 10) + " ~ " + values["P_PERIOD_PERIODTO"].ToString().Substring(0, 10);
                string txtItemName = values["P_PRODUCTNAME"].ToString();
                values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                string qryYPE = string.Empty;
                if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
                    qryYPE = "YPE";

                if (this.smartTabControl1.SelectedTabPage == tabPageDayYieldRate)
                {
                    dt = await SqlExecuter.QueryAsync("SelectDailyYieldRate" + qryYPE, "10001", values);

                    this.tabUCDayYieldRate.PeriodText = txtPeriod;
                    this.tabUCDayYieldRate.ItemText = txtItemName;

                    tabUCDayYieldRate.SetChartDataSource(dt);

                    DataTable dt2 = tabUCDayYieldRate.CreateGridTable(dt);
                    tabUCDayYieldRate.SetGridDataSource(dt2);
                }
                else if (this.smartTabControl1.SelectedTabPage == tabPageItemYieldRate)
                {
                    this.tabUCItemYieldRate.PeriodText = txtPeriod;
                    this.tabUCItemYieldRate.ItemText = txtItemName;

                    DataSet ds = new DataSet();

                    string strQuery = "SelectItemYieldRatePeriodSum";
                    if (tabUCItemYieldRate.CboItemYieldChartViewType.EditValue.Equals("PeriodSummary"))
                    {
                        strQuery = "SelectItemYieldRatePeriodSum" + qryYPE;
                    }
                    else
                        strQuery = "SelectItemYieldRateDaily" + qryYPE;

                    if (await SqlExecuter.QueryAsync(strQuery, "10001", values) is DataTable dt1)
                    {
                        dt1.TableName = "Table1";
                        ds.Tables.Add(dt1.Copy());
                    }

                    if (await SqlExecuter.QueryAsync("SelectItemYieldDefect" + qryYPE, "10001", values) is DataTable dt2)
                    {
                        dt2.TableName = "Table2";
                        ds.Tables.Add(dt2.Copy());
                    }

                    tabUCItemYieldRate.SetChartDataItemYield(ds.Tables[0], ds.Tables[1], tabUCItemYieldRate.CboItemYieldChartViewType.EditValue.ToString());


                    // 화면 하단 차트 2개
                    tabUCItemYieldRate.SetChartDataDefect(ds.Tables[1], tabUCItemYieldRate.CboItemYieldChartViewType.EditValue.ToString());

                }
                else if (this.smartTabControl1.SelectedTabPage == tabPageLotYieldRate)
                {
                    this.tabUCLotYieldRate.PeriodText = txtPeriod;
                    this.tabUCLotYieldRate.ItemText = txtItemName;
                    DataSet ds = new DataSet();

                    string strDefectSegCd = values["P_DEFECTCODE"].ToString();

                    if(await SqlExecuter.QueryAsync("SelectDailyLotYieldRate" + qryYPE, "10001", values) is DataTable dt2)
                    {
                        dt2.TableName = "Table1";
                        
                        ds.Tables.Add(dt2.Copy());
                    }

                    if (await SqlExecuter.QueryAsync("SelectDailyLOTDefectRate" + qryYPE, "10001", values) is DataTable dt3)
                    {
                        dt3.TableName = "Table2";

                        ds.Tables.Add(dt3.Copy());
                    }

                    if (ds.Tables.Count < 2) return;

                    tabUCLotYieldRate.SetData(ds, strDefectSegCd);
                }
                else
                    return;


                //if (dt.Rows.Count < 1)
                //{
                //    this.ShowMessage("NoSelectData");
                //    //grdDefectStatusSearch.DataSource = null;
                //    return;
                //}

                //grdDefectStatusSearch.DataSource = dt;

            }
            catch (Exception ex)
            {
                throw Framework.MessageException.Create(ex.ToString());
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
            //base.InitializeCondition();
            // 초기 Tab 선택
            this.smartTabControl1.SelectedTabPageIndex = 0;

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            #region 품목 

            var productDefID = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductListByYieldStatus", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFIDVERSION", "PRODUCTDEFIDVERSION")
                                         .SetLabel("PRODUCTDEFID")
                                         .SetPopupLayout("GRIDPRODUCTLIST", PopupButtonStyles.Ok_Cancel, true, false)
                                         .SetPopupLayoutForm(650, 400, FormBorderStyle.FixedToolWindow)
                                         .SetPosition(1.1)
                                         .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                         {
                                             foreach (DataRow row in selectedRow)
                                             {
                                                 SetCondition(row);
                                             }
                                         });

            SmartTextBox tbProductID = Conditions.GetControl<SmartTextBox>("PRODUCTDEFID");

            productDefID.Conditions.AddTextBox("PRODUCTDEFID")
                                   .SetLabel("PRODUCTDEFID");

            //productDefID.Conditions.GetControl<SmartTextBox>("PRODUCTDEFID").EditValue = tbProductID.EditValue;

            productDefID.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 120)
                                    .SetLabel("PRODUCTDEFID");

            productDefID.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 250)
                                    .SetLabel("PRODUCTDEFNAME");

            productDefID.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100)
                                    .SetLabel("PRODUCTDEFVERSION");


            #endregion

            #region 품목명

            Conditions.GetCondition<ConditionItemTextBox>("P_PRODUCTNAME").SetIsReadOnly();

            #endregion

            #region 불량선택
            // DefectSelectPopup
            //DefectSelectPopup defectPopup = new DefectSelectPopup();
            //var defectCodePopup = Conditions.AddSelectPopup("P_DEFECTCODE", (ISmartCustomPopup)defectPopup, "DEFECTSEGNAME", "DEFECTSEGNAME")
            //     .SetPopupLayoutForm(500, 600)
            //     .SetLabel("DEFECTCODE")
            //     .SetPopupCustomParameter((popup, dataRow) =>
            //     {
            //         (popup as DefectSelectPopup).SelectedDefectHandlerEvent += (dt) => { _DefectList = dt; };
            //     });
            ;


            var defectCodePopup = Conditions.AddSelectPopup("P_DEFECTCODE", new SqlQuery("GetDefectCodeByYieldStatus", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "DEFECTSEGNAME", "DEFECTSEGID")
               .SetPopupLayout("DEFECTCODE", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("DEFECTCODE")
               .SetPopupResultCount(1)
               .SetPosition(5.1)
               .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
               {

                   //foreach (DataRow row in selectedRow)
                   //{
                   //    Conditions.GetControl<SmartSelectPopupEdit>("P_DEFECTCODE").SetValue(row["DEFECTSEGNAME"].ToString());
                   //    Conditions.GetControl<SmartTextBox>("P_DEFECTNAME").EditValue = row["DEFECTSEGNAME"].ToString();
                   //}

                   #region Commented

                   //string codeName = "";
                   //string codeID = "";

                   //selectedRow.Cast<DataRow>().ForEach(row =>
                   //{
                   //    codeName += row["DEFECTNAME"].ToString() + ",";
                   //    codeID += row["DEFECTCODE"].ToString() + ",";

                   //});
                   //codeName = codeName.TrimEnd(',');
                   //codeID = codeID.TrimEnd(',');
                   //Conditions.GetControl<SmartSelectPopupEdit>("DEFECTCODE").SetValue(codeName);
                   //Conditions.GetControl<SmartTextBox>("DEFECTNAME").Text = codeName;

                   #endregion
               });

            // 팝업 조회조건
            defectCodePopup.Conditions.AddTextBox("P_DEFECTCODE").SetLabel("DEFECTNAME");

            // 팝업 그리드
            defectCodePopup.GridColumns.AddTextBoxColumn("DEFECTNAME", 150);
            defectCodePopup.GridColumns.AddTextBoxColumn("QCSEGMENT", 200);


            #endregion

            #region 불량명

            //var defectName = Conditions.AddTextBox("P_DEFECTNAME").SetLabel("DEFECTNAME").SetPosition(5.2).SetIsReadOnly();

            #endregion
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += SearchOption_ProductDef_EditValueChanged;

            setCondtionControlDefectCode(false);

            Conditions.GetControl<SmartSelectPopupEdit>("P_DEFECTCODE").EditValueChanged += (s, e) =>
            {
                // ＠ 2020-01-20 류시윤 LOT분석 동작 이슈로 인한 조건에 의한 버튼보임을 모두 FALSE 처리
                if (this.smartTabControl1.SelectedTabPage == tabPageLotYieldRate)
                {
                    string strDefectNames = Conditions.GetControl<SmartSelectPopupEdit>("P_DEFECTCODE").EditValue.ToString();

                    #region 랏별 수율 LOT 분석 버튼 Visible Property

                    // 항상 보이기 전태선 대리 요청
                    //if (string.IsNullOrEmpty(strDefectNames))
                    //    this.tabUCLotYieldRate.ButtonLOTAnalysis.Visible = false;
                    //else
                    //    this.tabUCLotYieldRate.ButtonLOTAnalysis.Visible = true;

                    #endregion
                }
            };

            //SmartDateRangeEdit frDate = Conditions.GetControl<SmartDateRangeEdit>("P_EVALUATIONDATE");
            //frDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            //frDate.Properties.Mask.EditMask = "yyyy-MM-dd";
            //frDate.EditValue = DateTime.Now.AddYears(1);
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            grdList.View.CheckValidation();

            DataTable changed = grdList.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        private void SetCondition(DataRow dr)
        {
            if (!DefectMapHelper.IsNull(dr.GetObject("DEFECTCODE")))
            {
                Conditions.GetControl<SmartSelectPopupEdit>("P_DEFECTCODE").SetValue(dr["DEFECTCODE"]);

                //Conditions.GetControl<SmartTextBox>("DEFECTNAME").Text = dr["DEFECTNAME"].ToString();
            }
            else
            {
                Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(dr["PRODUCTDEFID"]);

                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = dr["PRODUCTDEFNAME"].ToString();
            }
            _selectedRow = dr;
        }

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 마스터 정보를 조회한다.
        /// </summary>
        private void LoadData()
        {

        }

        /// <summary>
        /// 조회조건 - 불량코드 컨트롤 Enable 설정
        /// </summary>
        /// <param name="bEnable"></param>
        private void setCondtionControlDefectCode(bool bEnable)
        {
            if (bEnable)
            {
                Conditions.GetControl<SmartSelectPopupEdit>("P_DEFECTCODE").Enabled = true;
            }
            else
            {
                //Conditions.GetControl<SmartSelectPopupEdit>("P_DEFECTCODE").Visible = false;
                //Conditions.GetControl<SmartSelectPopupEdit>("P_DEFECTCODE").ReadOnly = true;
                Conditions.GetControl<SmartSelectPopupEdit>("P_DEFECTCODE").Enabled = false;
            }
        }
        
        #endregion
    }
}
