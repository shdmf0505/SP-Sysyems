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
    public partial class NonAdjRatio : SmartConditionManualBaseForm
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

        public NonAdjRatio()
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

            InitializeItemGrid();

            InitializeItemProcess();

        }

        private void InitializeItemProcess()
        {
            //gridItemProcessNonAdjust
            gridItemProcessNonAdjust.View.OptionsBehavior.Editable = false;

            // 양산구분
            gridItemProcessNonAdjust.View.AddTextBoxColumn("PRODUCTDEFTYPE", 100)
                  .SetLabel("LOTPRODUCTTYPE")
                  .SetTextAlignment(TextAlignment.Center);

            // TYPE : 단면, 양면, 멀티
            gridItemProcessNonAdjust.View.AddTextBoxColumn("PRODUCTSHAPE", 70).SetTextAlignment(TextAlignment.Center);

            // 고객사
            gridItemProcessNonAdjust.View.AddTextBoxColumn("COMPANYCLIENT", 50).SetTextAlignment(TextAlignment.Center);

            // 구분
            gridItemProcessNonAdjust.View.AddTextBoxColumn("LOCALE", 50).SetTextAlignment(TextAlignment.Center);

            // 품목코드
            gridItemProcessNonAdjust.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetTextAlignment(TextAlignment.Left);
            // 품목REV
            gridItemProcessNonAdjust.View.AddTextBoxColumn("PRODUCTDEFVERSION", 70).SetTextAlignment(TextAlignment.Center);
            // 품목명
            gridItemProcessNonAdjust.View.AddTextBoxColumn("PRODUCTDEFNAME", 100).SetTextAlignment(TextAlignment.Left);

            // 공정수순
            gridItemProcessNonAdjust.View.AddTextBoxColumn("USERSEQUENCE", 100).SetTextAlignment(TextAlignment.Left);

            // 수율
            gridItemProcessNonAdjust.View
                .AddTextBoxColumn("YIELDRATE", 50)
                .SetLabel("YIELDRATE")
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("##0.#0 %", MaskTypes.Custom);

            // 직행율
            gridItemProcessNonAdjust.View
                .AddTextBoxColumn("NONADJRATIO", 50)
                .SetLabel("직행율")
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("##0.#0 %", MaskTypes.Custom);

            // RTR/SHT 타입
            gridItemProcessNonAdjust.View
                .AddTextBoxColumn("RTRSHTTYPE", 50)
                .SetLabel("RTR/SHT")
                .SetTextAlignment(TextAlignment.Center);

            // 투입수
            gridItemProcessNonAdjust.View
                .AddTextBoxColumn("RECEIVEQTY", 50)
                .SetLabel("PCSINPUTQTY")
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            // 불량수
            gridItemProcessNonAdjust.View
                .AddTextBoxColumn("DEFECTQTY", 50)
                .SetLabel("PCSDEFECTQTY")
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);


            // 재생수
            gridItemProcessNonAdjust.View
                .AddTextBoxColumn("REPAIRQTY", 50)
                .SetLabel("REPAIRQTY")
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            // 재작업수
            gridItemProcessNonAdjust.View
                .AddTextBoxColumn("REWORKQTY", 50)
                .SetLabel("REWORK")
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            // LOT 수
            gridItemProcessNonAdjust.View
                .AddTextBoxColumn("LOTCOUNT", 50)
                .SetLabel("LOTCNT")
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            gridItemProcessNonAdjust.View.PopulateColumns();
        }

        private void InitializeItemGrid()
        {
            //grdTopInfo

            gridItemNonAdjust.View.OptionsBehavior.Editable = false;

            var defaultCol = gridItemNonAdjust.View.AddGroupColumn("");
            
            // 양산구분
            defaultCol.AddTextBoxColumn("PRODUCTDEFTYPE", 100)
                  .SetLabel("LOTPRODUCTTYPE")
                  .SetTextAlignment(TextAlignment.Center);

            // TYPE : 단면, 양면, 멀티
            defaultCol.AddTextBoxColumn("PRODUCTSHAPE", 70).SetTextAlignment(TextAlignment.Center);

            // 고객사
            defaultCol.AddTextBoxColumn("COMPANYCLIENT", 50).SetTextAlignment(TextAlignment.Center);

            // 구분
            defaultCol.AddTextBoxColumn("LOCALE", 50).SetTextAlignment(TextAlignment.Center);

            // 품목코드
            defaultCol.AddTextBoxColumn("PRODUCTDEFID", 100).SetTextAlignment(TextAlignment.Left);
            // 품목REV
            defaultCol.AddTextBoxColumn("PRODUCTDEFVERSION", 70).SetTextAlignment(TextAlignment.Center);
            // 품목명
            defaultCol.AddTextBoxColumn("PRODUCTDEFNAME", 100).SetTextAlignment(TextAlignment.Left);

            // 직행율 그룹
            var vNonAdjRatio = gridItemNonAdjust.View.AddGroupColumn("NONADJUSTEDRATIO");

            // 수율
            vNonAdjRatio.AddTextBoxColumn("NONADJRATIO", 50)
                .SetLabel("YIELDRATE")
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("##0.#0 %", MaskTypes.Custom);

            // SHEET
            vNonAdjRatio.AddTextBoxColumn("SHEETNONADJ", 50)
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("##0.#0 %", MaskTypes.Custom);

            // ROLL
            vNonAdjRatio.AddTextBoxColumn("ROLLNONADJ", 50)
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("##0.#0 %", MaskTypes.Custom);

            // SHEET 그룹
            var vSheetGrp = gridItemNonAdjust.View.AddGroupColumn("SHEETNONADJ");

            // 투입수
            vSheetGrp.AddTextBoxColumn("SHEETINPUTQTY", 50)
                .SetLabel("PCSINPUTQTY")
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            // 불량수
            vSheetGrp.AddTextBoxColumn("SHEETDEFECTQTY", 50)
                .SetLabel("PCSDEFECTQTY")
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            // 재생량
            vSheetGrp.AddTextBoxColumn("SHEETREPAIRQTY", 50)
                .SetLabel("REPAIRQTY")
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            // 재작업량
            vSheetGrp.AddTextBoxColumn("SHEETREWORKQTY", 50)
                .SetLabel("REWORKQTY")
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            // LOT수
            vSheetGrp.AddTextBoxColumn("SHEETLOTQTY", 50)
                .SetLabel("LOTCNT")
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            // ROLL 그룹
            var vRollGrp = gridItemNonAdjust.View.AddGroupColumn("ROLLNONADJ");

            // 투입수
            vRollGrp.AddTextBoxColumn("ROLLINPUTQTY", 50)
                .SetLabel("PCSINPUTQTY")
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            // 불량수
            vRollGrp.AddTextBoxColumn("ROLLDEFECTQTY", 50)
                .SetLabel("PCSDEFECTQTY")
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            // 재생량
            vRollGrp.AddTextBoxColumn("ROLLREPAIRQTY", 50)
                .SetLabel("REPAIRQTY")
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            // 재작업량
            vRollGrp.AddTextBoxColumn("ROLLREWORKQTY", 50)
                .SetLabel("REWORKQTY")
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            // LOT수
            vRollGrp.AddTextBoxColumn("ROLLLOTQTY", 50)
                .SetLabel("LOTCNT")
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            var extCol = gridItemNonAdjust.View.AddGroupColumn("");

            // ROLL/SHEET 구분
            extCol.AddTextBoxColumn("RTRSHTTYPE", 100)
                .SetLabel("Roll/Sheet")
                .SetTextAlignment(TextAlignment.Center);

            
            gridItemNonAdjust.View.PopulateColumns();

            gridItemNonAdjust.View.BestFitColumns(true);
        }



        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가

            smartTabControl1.SelectedPageChanged += (s, e) =>
            {
                if(smartTabControl1.SelectedTabPageIndex == 1)
                {
                    Conditions.GetCondition("P_PRODUCTDEFID").IsRequired = true;
                }
                else
                {
                    Conditions.GetCondition("P_PRODUCTDEFID").IsRequired = false;
                }
            };
            //Conditions.GetControl<SmartComboBox>("P_LOCALEDIV").EditValueChanged += (s, e) =>
            //{
            //    SmartComboBox cbDiv = s as SmartComboBox;
                
            //    if(cbDiv.EditValue.Equals("LOCAL"))
            //        Conditions.GetControl<SmartCheckedComboBox>("P_LINKPLANTID").Enabled = false;
            //    else
            //        Conditions.GetControl<SmartCheckedComboBox>("P_LINKPLANTID").Enabled = true;
            //};

            //#region Grid Row Style Event

            ////gridYieldDefectStatus.View.RowCellStyle += View_RowCellStyle;

            //#endregion

            //#region Grid ContextMenu Event

            ////gridYieldDefectStatus.InitContextMenuEvent += Grid_InitContextMenuEvent;

            //#endregion
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

            if (this.smartTabControl1.SelectedTabPage == tapItemNonAdjusted)
            {
                if (await SqlExecuter.QueryAsync("SelectProductItemNonAdjustedRatio", "10001", values) is DataTable dt2)
                {
                    
                    gridItemNonAdjust.DataSource = dt2;
                    gridItemNonAdjust.View.BestFitColumns(true);
                }
            }
            else
            {
                if (await SqlExecuter.QueryAsync("SelectProductProcessSegmentNonAdjustedRatio", "10001", values) is DataTable dt3)
                {
                    gridItemProcessNonAdjust.DataSource = dt3.Copy();
                    gridItemProcessNonAdjust.View.BestFitColumns(true);
                }
            }

            //if (_dtPopupSearch.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            //{
            //    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            //}
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

            #region 고객사

             var vCustomer = Conditions.AddSelectPopup("P_CUSTOMER", new SqlQuery("GetCustomerListByYield", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID")
                                         .SetLabel("CUSTOMER")
                                         .SetPosition(posProdName - 0.2)
                                         .SetPopupLayout("CUSTOMER", PopupButtonStyles.Ok_Cancel, true, false)
                                         .SetPopupLayoutForm(600, 400, FormBorderStyle.FixedToolWindow)
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

            #region 품목 

            var productDefID = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductListByYieldStatus", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFIDVERSION", "PRODUCTDEFIDVERSION")
                                         .SetLabel("PRODUCTDEFID")
                                         .SetPopupLayout("GRIDPRODUCTLIST", PopupButtonStyles.Ok_Cancel, true, false)
                                         .SetPopupLayoutForm(650, 400, FormBorderStyle.FixedToolWindow)
                                         .SetPosition(posProdName - 0.1)
                                         .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                         {
                                             string strProdCd = string.Empty;
                                             string strProdNm = string.Empty;

                                             foreach (DataRow row in selectedRow)
                                             {
                                                 strProdCd += row["PRODUCTDEFIDVERSION"] + ",";
                                                 strProdNm += row["PRODUCTDEFNAME"] + ",";

                                                 _selectedRow = row;
                                             }
                                             strProdCd = strProdCd.Substring(0, strProdCd.Length - 1);
                                             Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(strProdCd);
                                             strProdNm = strProdNm.Substring(0, strProdNm.Length - 1);
                                             Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = strProdNm;

                                         });

            //productDefID.IsRequired = true;

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

            #region 공정명 

            //var processSegName = Conditions.AddSelectPopup("P_PROCESSSEGMENTID", new SqlQuery("GetProcessSegmentByCondition", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
            //                             .SetLabel("PROCESSSEGMENTNAME")
            //                             .SetPopupLayout("PROCESSSEGMENTNAME", PopupButtonStyles.Ok_Cancel, true, false)
            //                             .SetPopupLayoutForm(650, 400, FormBorderStyle.FixedToolWindow)
            //                             .SetPosition(5)
            //                             //.SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
            //                             //{
            //                             //    string strProdCd = string.Empty;
            //                             //    string strProdNm = string.Empty;

            //                             //    foreach (DataRow row in selectedRow)
            //                             //    {
            //                             //        strProdCd += row["PROCESSSEGMENTID"] + ",";
            //                             //        strProdNm += row["PROCESSSEGMENTNAME"] + ",";
            //                             //    }
            //                             //    strProdNm = strProdNm.Substring(0, strProdNm.Length - 1);
            //                             //    Conditions.GetControl<SmartSelectPopupEdit>("P_PROCESSSEGMENTID").SetValue(strProdNm);
            //                             //})
            //                             .SetRelationIds("P_PRODUCTDEFID");

            //// 복수 선택 Result Count 지정
            //processSegName.SetPopupResultCount(0);

            //processSegName.Conditions.AddTextBox("PROCESSSEGMENTNAME")
            //                       .SetLabel("PROCESSSEGMENTNAME");

            //processSegName.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 120)
            //                        .SetLabel("PROCESSSEGMENTID");

            //processSegName.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 250)
            //                        .SetLabel("PROCESSSEGMENTNAME");

            #endregion

        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += NonAdjRatio_EditValueChanged; ;
        }

        private void NonAdjRatio_EditValueChanged(object sender, EventArgs e)
        {
            var values = Conditions.GetValues();
            string strValue = Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValue.ToString();   // values["P_PRODUCTDEFID"].ToString();

            if (string.IsNullOrEmpty(strValue))
            {
                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = "";
            }
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

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 마스터 정보를 조회한다.
        /// </summary>
        private void LoadData()
        {

        }

        private void SetCondition(DataRow dr)
        {
            if (dr.GetObject("P_PRODUCTDEFID") != null)
            {
                //Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(dr["PRODUCTDEFID"]);

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
