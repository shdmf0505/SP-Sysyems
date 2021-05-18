#region using

using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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
    /// 프 로 그 램 명  : 품질관리 > 수율현황 및 불량분석 > 불량별 현황
    /// 업  무  설  명  : 불량별 현황을 확인한다.
    /// 생    성    자  : 류시윤
    /// 생    성    일  : 2020-01-16
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class YieldDefectStatusByDefect : SmartConditionManualBaseForm
    {
        #region Local Variables

        DXMenuItem _myContextMenu1, _myContextMenu2, _myContextMenu3, _myContextMenu4, _myContextMenu5;

        // TODO : 화면에서 사용할 내부 변수 추가
        string _Text;                                       // 설명
        SmartBandedGrid grdList = new SmartBandedGrid();    // Main Grid
        
        /// <summary>
        /// 조회 조건 품목에서 찾은 정보 Row
        /// </summary>
        private DataRow _selectedRow;

        private DataTable _dtPopupSearch;

        #endregion

        #region 생성자

        public YieldDefectStatusByDefect()
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

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();         
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            //grdList.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore;
            //grdList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            initGridYieldDefectStatus();
        }

        private void initGridYieldDefectStatus()
        {
            gridYieldDefectStatus.View.OptionsBehavior.Editable = false;
            gridYieldDefectStatus.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            var defaultCol = gridYieldDefectStatus.View.AddGroupColumn("");
            // NO
            defaultCol.AddTextBoxColumn("NO", 70).SetTextAlignment(TextAlignment.Center);
            // 불량명
            defaultCol.AddTextBoxColumn("DEFECTNAME", 50).SetTextAlignment(TextAlignment.Left);


            #region PCS 수율 컬럼

            var pcsCol = gridYieldDefectStatus.View.AddGroupColumn("GROUPPCSYIELDRATE");

            // 불량수
            pcsCol.AddTextBoxColumn("PCSDEFECTQTY", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);

            // 불량율
            pcsCol.AddTextBoxColumn("PCSDEFECTRATE", 50).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric);

            // 불량점유
            pcsCol.AddTextBoxColumn("PCSDEFECTOCC", 50)
                .SetLabel("DEFECTOCCUPANCY")
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("##0.#0 %", MaskTypes.Numeric);

            #endregion PCS 수율 컬럼

            #region 면적당 수율 컬럼

            var areaCol = gridYieldDefectStatus.View.AddGroupColumn("GROUPAREAYIELDRATE");

            // 불량수
            areaCol.AddTextBoxColumn("AREADEFECTQTY", 50)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.#0", MaskTypes.Numeric)
                .SetLabel("DEFECTM2");

            // 불량율
            areaCol.AddTextBoxColumn("AREADEFECTRATE", 50).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric);

            // 불량점유
            areaCol.AddTextBoxColumn("AREADEFECTOCC", 50)
                .SetLabel("DEFECTOCCUPANCY")
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("##0.#0 %", MaskTypes.Numeric);

            #endregion 면적당 수율 컬럼

            #region 금액 수율 컬럼

            var priceCol = gridYieldDefectStatus.View.AddGroupColumn("GROUPPRICEYIELDRATE");

            // 불량수
            priceCol.AddTextBoxColumn("PRICEDEFECTQTY", 50)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetLabel("DEFECTCOST");

            // 불량율
            priceCol.AddTextBoxColumn("PRICEDEFECTRATE", 50).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric);

            // 불량점유
            priceCol.AddTextBoxColumn("PRICEDEFECTOCC", 50)
                .SetLabel("DEFECTOCCUPANCY")
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("##0.#0 %", MaskTypes.Numeric);

            #endregion 금액 수율 컬럼

            var inputCol = gridYieldDefectStatus.View.AddGroupColumn("GROUPINPUTQTY");

            // PCS 투입수
            inputCol.AddTextBoxColumn("PCSINPUTQTY", 50).SetLabel("PCS").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);

            // 면적투입수
            inputCol.AddTextBoxColumn("AREAINPUTQTY", 50).SetLabel("M2QTY").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.#0", MaskTypes.Numeric);

            // 금액투입수
            inputCol.AddTextBoxColumn("PRICEINPUTQTY", 50).SetLabel("INPUTCOST").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);


            gridYieldDefectStatus.View.PopulateColumns();

            gridYieldDefectStatus.View.BestFitColumns(true);
        }
        
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가

            Conditions.GetControl<SmartComboBox>("P_LOCALEDIV").EditValueChanged += (s, e) =>
            {
                SmartComboBox cbDiv = s as SmartComboBox;
                
                if(cbDiv.EditValue.Equals("LOCAL"))
                    Conditions.GetControl<SmartCheckedComboBox>("P_LINKPLANTID").Enabled = false;
                else
                    Conditions.GetControl<SmartCheckedComboBox>("P_LINKPLANTID").Enabled = true;
            };

            #region Grid Row Style Event

            //gridYieldDefectStatus.View.RowCellStyle += View_RowCellStyle;

            #endregion

            #region Grid ContextMenu Event

            //gridYieldDefectStatus.InitContextMenuEvent += Grid_InitContextMenuEvent;

            #endregion
        }

        private void View_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;

            DataRowView row = view.GetRow(e.RowHandle) as DataRowView;

            if (row["LV"].ToString().ToInt32() > 1)
            {
                e.Appearance.BackColor = Color.LightSkyBlue;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        private void Grid_InitContextMenuEvent(SmartBandedGrid sender, Framework.SmartControls.Grid.InitContextMenuEventArgs args)
        {
            //// LOT 별 수율현황
            //args.AddMenus.Add(_myContextMenu1 = new DXMenuItem(Language.Get("MENU_PG-QC-0470"), OpenForm) { BeginGroup = true, Tag = "PG-QC-0470" });
            //// 이상발생현황
            //args.AddMenus.Add(_myContextMenu4 = new DXMenuItem(Language.Get("MENU_PG-SG-0450"), OpenForm) { Tag = "PG-SG-0450" });
            //// Affect LOT 산정(출)
            //args.AddMenus.Add(_myContextMenu5 = new DXMenuItem(Language.Get("MENU_PG-QC-0556"), OpenForm) { Tag = "PG-QC-0556" });
        }

        private void OpenForm(object sender, EventArgs args)
        {
            //try
            //{
            //    SmartBandedGrid grid;
            //    if (smartTabControl1.SelectedTabPage == tapPageItemYieldRate)
            //        grid = gridYieldDefectStatus;
            //    else
            //        grid = gridDefectStatus;

            //    DialogManager.ShowWaitDialog();

            //    DataRow currentRow = grid.View.GetFocusedDataRow();

            //    string menuId = (sender as DXMenuItem).Tag.ToString();

            //    var param = currentRow.Table.Columns
            //        .Cast<DataColumn>()
            //        .ToDictionary(col => col.ColumnName, col => currentRow[col.ColumnName]);

            //    OpenMenu(menuId, param); //다른창 호출..
            //}
            //catch (Exception ex)
            //{
            //    this.ShowError(ex);
            //}
            //finally
            //{
            //    DialogManager.Close();
            //}
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
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("PLANT", UserInfo.Current.Plant);

            string qryYPE = string.Empty;
            if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
                qryYPE = "YPE";

            if (await SqlExecuter.QueryAsync("SelectDefectStatusByDefect" + qryYPE, "10002", values) is DataTable dt2)
            {
                gridYieldDefectStatus.DataSource = dt2;
                gridYieldDefectStatus.View.BestFitColumns(true);
            }

            if (await SqlExecuter.QueryAsync("SelectDefectStatusByDefect" + qryYPE, "10001", values) is DataTable dt3)
            {
                _dtPopupSearch = dt3.Copy();
            }

            if (_dtPopupSearch.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            //base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용        

            #region 공장조건
            // 영풍일 경우에만

            //if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
            //{
            //    //double dPosition = Conditions.GetCondition<ConditionItemTextBox>("P_LINKPLANTID").Position;

            //    var vFactory = Conditions.AddComboBox("P_FACTORY", new SqlQuery("GetFactoryListByYield", "10001"), "FACTORYNAME", "FACTORYID")
            //                             .SetPosition(3.1);
            //}

            #endregion 공장조건

            #region 품목명

            Conditions.GetCondition<ConditionItemTextBox>("P_PRODUCTNAME").SetIsReadOnly();
            double posProdName = Conditions.GetCondition<ConditionItemTextBox>("P_PRODUCTNAME").Position;

            #endregion

            #region 품목 

            var productDefID = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductListByYieldStatus", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFIDVERSION", "PRODUCTDEFIDVERSION")
                                         .SetLabel("PRODUCTDEFID")
                                         .SetPopupLayout("GRIDPRODUCTLIST", PopupButtonStyles.Ok_Cancel, true, false)
                                         .SetPopupLayoutForm(600, 400, FormBorderStyle.FixedToolWindow)
                                         .SetPosition(posProdName - 0.1)
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
                                         .SetRelationIds("P_CUSTOMER")
                                         ;

            // 복수 선택 Result Count 지정
            productDefID.SetPopupResultCount(0);

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


            #region 고객사

            double posType = Conditions.GetCondition<ConditionItemComboBox>("P_PRODSHAPETYPE").Position;

            var vCustomer = Conditions.AddSelectPopup("P_CUSTOMER", new SqlQuery("GetCustomerListByYield", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID")
                                         .SetLabel("CUSTOMER")
                                         .SetPopupLayout("CUSTOMER", PopupButtonStyles.Ok_Cancel, true, false)
                                         .SetPopupLayoutForm(600, 400, FormBorderStyle.FixedToolWindow)
                                         .SetPosition(posType + 0.1)
                                         .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                         {
                                             foreach (DataRow row in selectedRow)
                                             {
                                                 SetCondition(row);
                                             }
                                         });
            ;

            vCustomer.Conditions.AddTextBox("CUSTOMERNAME")
                                   .SetLabel("CUSTOMER");

            vCustomer.GridColumns.AddTextBoxColumn("CUSTOMERID", 120)
                                    .SetLabel("CUSTOMERID");

            vCustomer.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 250)
                                    .SetLabel("CUSTOMERNAME");

            // 복수 선택 Result Count 지정
            vCustomer.SetPopupResultCount(0);

            #endregion 고객사

            //double posExct = Conditions.GetCondition<ConditionItemComboBox>("P_EXCEPTDEFECTCLASS").Position;
            
            //var vSum = Conditions.AddComboBox("P_SUMTYPE", new SqlQuery("GetSummaryTypeByYield", "10001", $"P_SUMTYPE=2,3,4,5,6,7,8,9,10,11", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CODENAME", "CODEID")
            //                             .SetLabel("YIELDSUMTYPE")
            //                             .SetDefault("11")
            //                             .SetPosition(posExct + 0.1);

        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #region 버튼 클릭 이벤트

        private void btnByType_Click(object sender, EventArgs e)
        {
            DataTable dt = gridYieldDefectStatus.View.GetCheckedRows();

            if(dt.Rows.Count < 1)
            {
                ShowMessage("NoSelections");
                return;
            }

            StatusByTypePopup popup = new StatusByTypePopup();
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.Width = 790;

            var values = Conditions.GetValues();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("PLANT", UserInfo.Current.Plant);

            popup.SearchConditions = values;

            string strDefectNm = string.Join("','", dt.Rows.OfType<DataRow>().Select(r => r["DEFECTNAME"].ToString()));

            string filterExpress = string.Format("DEFECTNAME IN ('{0}') AND LV <> 11", strDefectNm);
            string ordering = "";

            DataRow[] rows = _dtPopupSearch.Select(filterExpress, ordering);
            popup.DataSource = rows.CopyToDataTable();

            popup.ShowDialog();
            if (popup.DialogResult == DialogResult.OK)
            {
                if(!string.IsNullOrEmpty(popup.OpenAction) || !string.IsNullOrEmpty(popup.OpenMenu))
                    OpenMenu(popup.OpenMenu, popup.SearchConditions); //다른창 호출..
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

            }
        }

        private void btnByItem_Click(object sender, EventArgs e)
        {
            DataTable dt = gridYieldDefectStatus.View.GetCheckedRows();

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelections"); 
                return;
            }

            StatusByItemPopup popup = new StatusByItemPopup();
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.Width = 1220;

            var values = Conditions.GetValues();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("PLANT", UserInfo.Current.Plant);

            popup.SearchConditions = values;

            string strDefectNm = string.Join("','", dt.Rows.OfType<DataRow>().Select(r => r["DEFECTNAME"].ToString()));

            string filterExpress = string.Format("DEFECTNAME IN ('{0}') AND LV <> 11", strDefectNm);
            string ordering = "";

            DataRow[] rows = _dtPopupSearch.Select(filterExpress, ordering);
            popup.DataSource = rows.CopyToDataTable();

            popup.ShowDialog();
            if (popup.DialogResult == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(popup.OpenAction) || !string.IsNullOrEmpty(popup.OpenMenu))
                    OpenMenu(popup.OpenMenu, popup.SearchConditions); // LOT별 수율현황 화면 or 불량별 수율현황 화면
            }
        }

        #endregion

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

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 마스터 정보를 조회한다.
        /// </summary>
        private void LoadData()
        {

        }

        private void SetCondition(DataRow dr)
        {
            if (!DefectMapHelper.IsNull(dr.GetObject("P_PRODUCTDEFID")))
            {
                Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(dr["PRODUCTDEFID"]);

                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = dr["PRODUCTDEFNAME"].ToString();
            }
            _selectedRow = dr;
        }

        #endregion

        public override void LoadForm(Dictionary<string, object> parameters)
        {
            base.LoadForm(parameters);

            if (parameters != null)
            {
                string strLocal = Conditions.GetCondition<ConditionItemComboBox>("P_LOCALEDIV").DefaultValue.ToString();

                if (strLocal.Equals("LOCAL"))
                {
                    Conditions.GetControl<SmartCheckedComboBox>("P_LINKPLANTID").Enabled = false;
                }
                Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(Format.GetString(parameters["P_PRODUCTDEFID"]));
                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = Format.GetString(parameters["P_PRODUCTNAME"]);
                Conditions.GetControl<SmartComboBox>("P_PLANTID").EditValue = parameters["P_PLANTID"].ToString();
                SmartPeriodEdit period = Conditions.GetControl<SmartPeriodEdit>("P_PERIOD");
                period.datePeriodFr.EditValue = parameters["P_PERIOD_PERIODFR"];
                period.datePeriodTo.EditValue = parameters["P_PERIOD_PERIODTO"];

                if (parameters.ContainsKey("P_INSPTYPE"))
                    Conditions.GetControl<SmartComboBox>("P_INSPTYPE").EditValue = parameters["P_INSPTYPE"].ToString();

                if (parameters.ContainsKey("P_PLANTID"))
                    Conditions.GetControl<SmartComboBox>("P_PLANTID").EditValue = parameters["P_PLANTID"].ToString();

                if (parameters.ContainsKey("P_LINKPLANTID"))
                    Conditions.GetControl<SmartCheckedComboBox>("P_LINKPLANTID").EditValue = parameters["P_LINKPLANTID"].ToString();

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

                this.OnSearchAsync();
            }
        }
    }
}
