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
    public partial class DefectStatusByProcessEquipment : SmartConditionManualBaseForm
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

        private DataTable dtAreaEqp;

        #endregion

        #region 생성자

        public DefectStatusByProcessEquipment()
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

            InitializeGridTop();

            InitializeGridBodyProcess();

            InitializeGridAreaEqpInfo();
        }

        private void InitializeGridAreaEqpInfo()
        {
            //grdAreaEqpInfo
            grdAreaEqpInfo.View.OptionsBehavior.Editable = false;
            grdAreaEqpInfo.GridButtonItem = GridButtonItem.None;

            grdAreaEqpInfo.View.AddTextBoxColumn("NO", 100);

            grdAreaEqpInfo.View.AddTextBoxColumn("AREANAME", 150);
            grdAreaEqpInfo.View.AddTextBoxColumn("EQUIPMENTNAME", 150);
            grdAreaEqpInfo.View.AddTextBoxColumn("PCSINPUTQTY", 150)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdAreaEqpInfo.View.AddTextBoxColumn("DEFECTNAME", 150);
            grdAreaEqpInfo.View.AddTextBoxColumn("QCSEGMENTNAME", 150).SetLabel("QCSEGMENT");
            grdAreaEqpInfo.View.AddTextBoxColumn("PCSDEFECTRATE", 150)
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("##0.#0 %", MaskTypes.Numeric);
            grdAreaEqpInfo.View.AddTextBoxColumn("PCSDEFECTQTY", 150)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            grdAreaEqpInfo.View.PopulateColumns();
            grdAreaEqpInfo.View.BestFitColumns();
            grdAreaEqpInfo.View.OptionsView.AllowCellMerge = true;
        }

        private void InitializeGridBodyProcess()
        {
            //grdProcessInfo
            grdProcessInfo.View.OptionsBehavior.Editable = false;
            grdProcessInfo.GridButtonItem = GridButtonItem.None;

            grdProcessInfo.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdProcessInfo.View.AddTextBoxColumn("NO", 100);
            grdProcessInfo.View.AddTextBoxColumn("SITE", 100);
            grdProcessInfo.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 100);

            grdProcessInfo.View.AddTextBoxColumn("PCSDEFECTRATE", 150)
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("##0.#0 %", MaskTypes.Custom); ;
            grdProcessInfo.View.AddTextBoxColumn("PCSINPUTQTY", 150)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdProcessInfo.View.AddTextBoxColumn("PCSDEFECTQTY", 150)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            grdProcessInfo.View.PopulateColumns();
        }

        private void InitializeGridTop()
        {
            //grdTopInfo

            // 제품구분
            grdTopInfo.View.AddTextBoxColumn("PRODUCTDEFTYPE", 50).SetLabel("THEPRODUCTTYPE").SetTextAlignment(TextAlignment.Center);

            // TYPE : 단면, 양면, 멀티
            grdTopInfo.View.AddTextBoxColumn("PRODUCTSHAPE", 70).SetTextAlignment(TextAlignment.Center);

            // 고객사
            grdTopInfo.View.AddTextBoxColumn("COMPANYCLIENT", 50).SetTextAlignment(TextAlignment.Left);

            // 구분
            grdTopInfo.View.AddTextBoxColumn("LOCALE", 50).SetTextAlignment(TextAlignment.Center);

            // 품목코드
            grdTopInfo.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetTextAlignment(TextAlignment.Left);

            // 품목REV
            grdTopInfo.View.AddTextBoxColumn("PRODUCTDEFVERSION", 70).SetTextAlignment(TextAlignment.Center);

            // 품목명
            grdTopInfo.View.AddTextBoxColumn("PRODUCTDEFNAME", 100).SetTextAlignment(TextAlignment.Left);

            grdTopInfo.View.PopulateColumns();
            grdTopInfo.View.BestFitColumns(true);

            

        }



        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가

            #region 공정선택

            grdProcessInfo.View.CheckStateChanged += grdProcessInfoView_CheckStateChanged; ;

            #endregion

            #region Grid Row Style Event

            grdAreaEqpInfo.View.CellMerge += grdViewAreaEpqInfo_CellMerge;
            grdAreaEqpInfo.View.CellValueChanged += grdViewAreaEpqInfo_CellValueChanged;

            #endregion

        }

        private void grdProcessInfoView_CheckStateChanged(object sender, EventArgs e)
        {
            DataView dv = dtAreaEqp.DefaultView;
            DataTable dt = grdProcessInfo.View.GetCheckedRows();

            if (dt.Rows.Count > 0)
            {
                List<string> lstSeqNo = dt.AsEnumerable().Select(g => g.Field<string>("NO")).ToList<string>();
                string joined = "'" + string.Join("','", lstSeqNo) + "'";
                string strFilter = string.Format("NO IN ({0})", joined);

                dv.RowFilter = strFilter;

                grdAreaEqpInfo.DataSource = dv.ToTable();
            }
            else
                grdAreaEqpInfo.View.ClearDatas();

            grdAreaEqpInfo.View.RefreshData();
            grdAreaEqpInfo.View.BestFitColumns();
        }

        private void grdViewAreaEpqInfo_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow row = grdAreaEqpInfo.View.GetFocusedDataRow();

            SmartBandedGrid grid;

            grid = grdAreaEqpInfo;

            grid.View.CellValueChanged -= grdViewAreaEpqInfo_CellValueChanged;

            grid.View.CellValueChanged += grdViewAreaEpqInfo_CellValueChanged;
        }

        private void grdViewAreaEpqInfo_CellMerge(object sender, CellMergeEventArgs e)
        {
            if (e.Column.FieldName.Equals("AREANAME"))
            {
                var dr1 = grdAreaEqpInfo.View.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = grdAreaEqpInfo.View.GetDataRow(e.RowHandle2); //아래 행 정보

                //비교하는 이유 그래야 정상적으로 나옴.
                e.Merge = dr1["AREANAME"].ToString().Equals(dr2["AREANAME"].ToString());
            }
            else
                e.Merge = false;

            if (e.Column.FieldName.Equals("NO"))
            {
                var dr1 = grdAreaEqpInfo.View.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = grdAreaEqpInfo.View.GetDataRow(e.RowHandle2); //아래 행 정보

                //비교하는 이유 그래야 정상적으로 나옴.
                e.Merge = dr1["NO"].ToString().Equals(dr2["NO"].ToString());
            }
            else
                e.Merge = false;

            e.Handled = true;
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

            grdProcessInfo.View.ClearDatas();
            //grdProcessInfo.View.RefreshData();
            grdAreaEqpInfo.View.ClearDatas();
            //grdAreaEqpInfo.View.RefreshData();

            if (await SqlExecuter.QueryAsync("GetProductInfoByProcessSegmentEquipment", "10001", values) is DataTable dt2)
            {
                grdTopInfo.DataSource = dt2;
                grdTopInfo.View.BestFitColumns(true);
            }

            if (await SqlExecuter.QueryAsync("SelectProcessSegmentEquipmentYieldRate", "10001", values) is DataTable dt3)
            {
                grdProcessInfo.DataSource = dt3.Copy();
                grdProcessInfo.View.BestFitColumns(true);
            }

            if (await SqlExecuter.QueryAsync("SelectProcessSegmentEquipmentYieldRateDetail", "10001", values) is DataTable dt4)
            {
                dtAreaEqp = dt4.Copy();
                //grdAreaEqpInfo.DataSource = dt4.Copy();
                //grdAreaEqpInfo.View.BestFitColumns(true);
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

            productDefID.IsRequired = true;

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

            var processSegName = Conditions.AddSelectPopup("P_PROCESSSEGMENTID", new SqlQuery("GetProcessSegmentByCondition", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                                         .SetLabel("PROCESSSEGMENTNAME")
                                         .SetPopupLayout("PROCESSSEGMENTNAME", PopupButtonStyles.Ok_Cancel, true, false)
                                         .SetPopupLayoutForm(650, 400, FormBorderStyle.FixedToolWindow)
                                         .SetPosition(5)
                                         //.SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                         //{
                                         //    string strProdCd = string.Empty;
                                         //    string strProdNm = string.Empty;

                                         //    foreach (DataRow row in selectedRow)
                                         //    {
                                         //        strProdCd += row["PROCESSSEGMENTID"] + ",";
                                         //        strProdNm += row["PROCESSSEGMENTNAME"] + ",";
                                         //    }
                                         //    strProdNm = strProdNm.Substring(0, strProdNm.Length - 1);
                                         //    Conditions.GetControl<SmartSelectPopupEdit>("P_PROCESSSEGMENTID").SetValue(strProdNm);
                                         //})
                                         .SetRelationIds("P_PRODUCTDEFID");

            // 복수 선택 Result Count 지정
            processSegName.SetPopupResultCount(0);

            processSegName.Conditions.AddTextBox("PROCESSSEGMENTNAME")
                                   .SetLabel("PROCESSSEGMENTNAME");

            processSegName.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 120)
                                    .SetLabel("PROCESSSEGMENTID");

            processSegName.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 250)
                                    .SetLabel("PROCESSSEGMENTNAME");

            #endregion

        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += DefectStatusByProcessEquipment_EditValueChanged; ;
        }

        private void DefectStatusByProcessEquipment_EditValueChanged(object sender, EventArgs e)
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
