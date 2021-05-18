#region using

using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System;
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
    /// 프 로 그 램 명  : 품질관리 > 타입별 현황 Popup
    /// 업  무  설  명  : 
    /// 생    성    자  : 류시윤
    /// 생    성    일  : 2020-01-20
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class StatusByItemPopup : SmartPopupBaseForm
    {
        #region Global Variable

        DXMenuItem _myContextMenu1, _myContextMenu2;

        /// <summary>
        /// 부모 그리드로 데이터를 바인딩 시켜줄 델리게이트
        /// </summary>
        /// <param name="dt"></param>
        public delegate void LotSelectionDataHandler(DataTable dt);
        public event LotSelectionDataHandler LotSelectEvent;
        public DataTable _checkTable;
        public DataRow CurrentDataRow { get; set; }

        public Dictionary<string, object> SearchConditions { get; set; }

        public string OpenMenu { get; set; }

        public string OpenAction { get; set; }

        public DataTable DataSource { get; set; }

        #endregion

        #region Local Variables

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public StatusByItemPopup()
        {
            InitializeComponent();

            InitializeEvent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        private void InitializeGrid()
        {
            grdStatusByItem.View.SetIsReadOnly();

            var type = grdStatusByItem.View.AddGroupColumn("");
            type.AddTextBoxColumn("PRODUCTSHAPE", 80)
                .SetTextAlignment(TextAlignment.Center);

            type.AddTextBoxColumn("PRODUCTDEFID", 80)
                .SetTextAlignment(TextAlignment.Center);

            type.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetTextAlignment(TextAlignment.Center);

            type.AddTextBoxColumn("PRODUCTDEFNAME", 80)
                .SetTextAlignment(TextAlignment.Center);

            var pcs = grdStatusByItem.View.AddGroupColumn("GROUPPCSYIELDRATE");
            pcs.AddTextBoxColumn("PCSDEFECTQTY", 80)
                .SetLabel("PCSDEFECTQTY")
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            pcs.AddTextBoxColumn("PCSDEFECTRATE", 50)
                .SetLabel("PCSDEFECTRATE")
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("##0.#0 %", MaskTypes.Numeric);

            var areaCol = grdStatusByItem.View.AddGroupColumn("GROUPAREAYIELDRATE");
            // 불량면적
            areaCol.AddTextBoxColumn("AREADEFECTQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.#0", MaskTypes.Numeric);
            // 면적불량율
            areaCol.AddTextBoxColumn("AREADEFECTRATE", 50).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric);

            var priceCol = grdStatusByItem.View.AddGroupColumn("GROUPPRICEYIELDRATE");
            // 불량금액
            priceCol.AddTextBoxColumn("PRICEDEFECTQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);
            // 금액불량율
            priceCol.AddTextBoxColumn("PRICEDEFECTRATE", 50).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric);

            var inputCol = grdStatusByItem.View.AddGroupColumn("GROUPINPUTQTY");

            // PCS 투입수
            inputCol.AddTextBoxColumn("PCSINPUTQTY", 80).SetLabel("PCS").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);

            // 면적투입수
            inputCol.AddTextBoxColumn("AREAINPUTQTY", 80).SetLabel("M2QTY").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.#0", MaskTypes.Numeric);

            // 금액투입수
            inputCol.AddTextBoxColumn("PRICEINPUTQTY", 100).SetLabel("AMOUNTS").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);

            grdStatusByItem.View.PopulateColumns();
            //grdDays.View.BestFitColumns(true);
        }

        #endregion

        #region Popup

        /// <summary>
        /// 품목 검색팝업
        /// </summary>
        private void selectProudctPopup()
        {
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();

            //popup.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            //popup.SetPopupLayout("PRODUCTLIST", PopupButtonStyles.Ok_Cancel, true, false);
            //popup.Id = "PRODUCT";
            //popup.SearchQuery = new SqlQuery("GetProductListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            //popup.IsMultiGrid = false;
            //popup.DisplayFieldName = "PRODUCTDEFNAME";
            //popup.ValueFieldName = "PRODUCTDEFID";
            //popup.LanguageKey = "PRODUCT";

            //popup.Conditions.AddTextBox("PRODUCTDEFIDNAME");

            //popup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            //popup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);

            //popupProductId.SelectPopupCondition = popup;
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += ItemSelectPopup_Load;
        }

        /// <summary>
        /// 검색
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        /// <summary>
        /// 적용
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnApply_Click(object sender, EventArgs e)
        {
            //if (grdStatusByItem.View.RowCount == 0)
            //{
            //    return;
            //}
            //else
            //{
            //    // LotSelectEvent(grdAffectLot.View.GetCheckedRows());
            //    this.DialogResult = DialogResult.OK;
            //    _checkTable = grdAffectLot.View.GetCheckedRows();
            //    this.Close();
            //}
        }

        /// <summary>
        /// 닫기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Form Road시 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemSelectPopup_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            InitializeGrid();

            selectProudctPopup();

            if (DataSource != null && DataSource.Rows.Count > 0)
            {
                DataTable newDt = (from row in DataSource.AsEnumerable()
                              group row by new
                              {
                                  ProdShape = row.Field<string>("PRODUCTSHAPE"),
                                  ProdDefID = row.Field<string>("PRODUCTDEFID"),
                                  ProdDefVer = row.Field<string>("PRODUCTDEFVERSION"),
                                  ProdDefNm = row.Field<string>("PRODUCTDEFNAME"),
                              } into grp
                              select new
                              {
                                  PRODUCTSHAPE = grp.Key.ProdShape,
                                  PRODUCTDEFID = grp.Key.ProdDefID,
                                  PRODUCTDEFVERSION = grp.Key.ProdDefVer,
                                  PRODUCTDEFNAME = grp.Key.ProdDefNm,
                                  PCSDEFECTQTY = grp.Sum(r => r.Field<decimal?>("PCSDEFECTQTY")),
                                  PCSDEFECTRATE = grp.Sum(r => r.Field<decimal?>("PCSDEFECTRATE")),
                                  AREADEFECTQTY = grp.Sum(r => r.Field<decimal?>("AREADEFECTQTY")),
                                  AREADEFECTRATE = grp.Sum(r => r.Field<decimal?>("AREADEFECTRATE")),
                                  PRICEDEFECTQTY = grp.Sum(r => r.Field<double?>("PRICEDEFECTQTY")),
                                  PRICEDEFECTRATE = grp.Sum(r => r.Field<decimal?>("PRICEDEFECTRATE")),
                                  PCSINPUTQTY = grp.Max(r => r.Field<decimal?>("PCSINPUTQTY")),
                                  AREAINPUTQTY = grp.Max(r => r.Field<decimal?>("AREAINPUTQTY")),
                                  PRICEINPUTQTY = grp.Max(r => r.Field<decimal?>("PRICEINPUTQTY")),
                              })
                              .Select(g =>
                              {
                                  var row = DataSource.NewRow();

                                  row["PRODUCTSHAPE"] = g.PRODUCTSHAPE;
                                  row["PRODUCTDEFID"] = g.PRODUCTDEFID;
                                  row["PRODUCTDEFVERSION"] = g.PRODUCTDEFVERSION;
                                  row["PRODUCTDEFNAME"] = g.PRODUCTDEFNAME;
                                  row["PCSDEFECTQTY"] = g.PCSDEFECTQTY;
                                  row["PCSDEFECTRATE"] = g.PCSDEFECTRATE;
                                  row["AREADEFECTQTY"] = g.AREADEFECTQTY;
                                  row["AREADEFECTRATE"] = g.AREADEFECTRATE;
                                  row["PRICEDEFECTQTY"] = g.PRICEDEFECTQTY;
                                  row["PRICEDEFECTRATE"] = g.PRICEDEFECTRATE;
                                  row["PCSINPUTQTY"] = g.PCSINPUTQTY;
                                  row["AREAINPUTQTY"] = g.AREAINPUTQTY;
                                  row["PRICEINPUTQTY"] = g.PRICEINPUTQTY;

                                  return row;
                              }).CopyToDataTable();

                grdStatusByItem.DataSource = newDt;
            }

            grdStatusByItem.View.BestFitColumns(true);

            grdStatusByItem.View.DoubleClick += View_DoubleClick;

            grdStatusByItem.InitContextMenuEvent += GrdStatusByItem_InitContextMenuEvent;
        }

        private void GrdStatusByItem_InitContextMenuEvent(SmartBandedGrid sender, Framework.SmartControls.Grid.InitContextMenuEventArgs args)
        {
            // LOT 별 수율현황
            args.AddMenus.Add(_myContextMenu1 = new DXMenuItem(Language.Get("MENU_PG-QC-0470") + " - " + Language.Get("LOTYIELDRATE"), OpenForm) { BeginGroup = true, Tag = "PG-QC-0470" });
            args.AddMenus.Add(_myContextMenu2 = new DXMenuItem(Language.Get("MENU_PG-QC-0470") + " - " + Language.Get("DEFECTDETAIL"), OpenForm) { BeginGroup = true, Tag = "PG-QC-0470" });
        }

        private void OpenForm(object sender, EventArgs args)
        {
            try
            {
                DialogManager.ShowWaitDialog();

                CurrentDataRow = grdStatusByItem.View.GetFocusedDataRow();

                string menuId = (sender as DXMenuItem).Tag.ToString();
                string selectedMenuCaption = (sender as DXMenuItem).Caption.ToString();
                string strByLOT1stTab = Language.Get("MENU_PG-QC-0470") + " - " + Language.Get("LOTYIELDRATE");
                string strByLOT2ndTab = Language.Get("MENU_PG-QC-0470") + " - " + Language.Get("DEFECTDETAIL");

                var values = SearchConditions;

                var param = CurrentDataRow.Table.Columns
                    .Cast<DataColumn>()
                    .ToDictionary(col => col.ColumnName, col => CurrentDataRow[col.ColumnName]);
                
                if(selectedMenuCaption.Equals(strByLOT1stTab))
                {

                }
                else if (selectedMenuCaption.Equals(strByLOT2ndTab))
                {
                    values.Add("Tab", "Detail");
                }

                    if (param.ContainsKey("PRODUCTDEFVERSION"))
                {
                    if (values.ContainsKey("PRODUCTDEFVERSION"))
                        values["PRODUCTDEFVERSION"] = param["PRODUCTDEFVERSION"].ToString();
                    else
                        values.Add("PRODUCTDEFVERSION", param["PRODUCTDEFVERSION"].ToString());
                }

                if (param.ContainsKey("PRODUCTDEFID"))
                {
                    if (values.ContainsKey("PRODUCTDEFID"))
                        values["PRODUCTDEFID"] = param["PRODUCTDEFID"].ToString();
                    else
                        values.Add("PRODUCTDEFID", param["PRODUCTDEFID"].ToString());
                }

                if (values.ContainsKey("P_PRODUCTDEFID"))
                    values["P_PRODUCTDEFID"] = param["PRODUCTDEFID"].ToString() + param["PRODUCTDEFVERSION"].ToString();
                else
                    values.Add("P_PRODUCTDEFID", param["PRODUCTDEFID"].ToString() + param["PRODUCTDEFVERSION"].ToString());

                //param.Add("P_PLANTID", values["P_PLANTID"].ToString());
                //param.Add("P_LINKPLANTID", values["P_LINKPLANTID"].ToString());
                //param.Add("P_PERIOD", values["P_PERIOD"].ToString());
                //param.Add("P_PERIOD_PERIODFR", values["P_PERIOD_PERIODFR"]);
                //param.Add("P_PERIOD_PERIODTO", values["P_PERIOD_PERIODTO"]);

                //OpenMenu(menuId, param); //다른창 호출..

                OpenMenu = "PG-QC-0470";
                OpenAction = "YieldStatusByLot";

                this.DialogResult = DialogResult.OK;
                this.Close();
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

        /// <summary>
        /// 그리드 더블클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_DoubleClick(object sender, EventArgs e)
        {
            CurrentDataRow = grdStatusByItem.View.GetFocusedDataRow();

            var values = SearchConditions;

            var param = CurrentDataRow.Table.Columns
                    .Cast<DataColumn>()
                    .ToDictionary(col => col.ColumnName, col => CurrentDataRow[col.ColumnName]);

            if (param.ContainsKey("PRODUCTDEFVERSION"))
            {
                if (values.ContainsKey("PRODUCTDEFVERSION"))
                    values["PRODUCTDEFVERSION"] = param["PRODUCTDEFVERSION"].ToString();
                else
                    values.Add("PRODUCTDEFVERSION", param["PRODUCTDEFVERSION"].ToString());
            }

            if (param.ContainsKey("PRODUCTDEFID"))
            {
                if (values.ContainsKey("PRODUCTDEFID"))
                    values["PRODUCTDEFID"] = param["PRODUCTDEFID"].ToString();
                else
                    values.Add("PRODUCTDEFID", param["PRODUCTDEFID"].ToString());
            }

            if (values.ContainsKey("P_PRODUCTDEFID"))
                values["P_PRODUCTDEFID"] = param["PRODUCTDEFID"].ToString() + param["PRODUCTDEFVERSION"].ToString();
            else
                values.Add("P_PRODUCTDEFID", param["PRODUCTDEFID"].ToString() + param["PRODUCTDEFVERSION"].ToString());

            if (values.ContainsKey("P_PRODUCTNAME"))
                values["P_PRODUCTNAME"] = param["PRODUCTDEFNAME"].ToString();
            else
                values.Add("P_PRODUCTNAME", param["PRODUCTDEFNAME"].ToString());

            OpenMenu = "PG-QC-0550";
            OpenAction = "DefectStatusByDefect";

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색
        /// </summary>
        /// <returns></returns>
        private void Search()
        {
            //Dictionary<string, object> param = new Dictionary<string, object>
            //{
            //    {"PRODUCTDEFID", popupProductId.GetValue().ToString()},
            //    {"LOTID", txtLotId.EditValue.ToString()},
            //    {"LANGUAGETYPE" , Framework.UserInfo.Current.LanguageType},
            //    {"PLANTID" , Framework.UserInfo.Current.Plant},
            //    {"ENTERPRISEID" , Framework.UserInfo.Current.Enterprise},
            //    {"ABNOCRNO" , CurrentDataRow["ABNOCRNO"]},
            //    {"ABNOCRTYPE" , CurrentDataRow["ABNOCRTYPE"]}
            //};

            //DataTable dt = SqlExecuter.Query("SelectLotToAddAffectLot", "10001", param);

            //if (dt.Rows.Count < 1)
            //{
            //    ShowMessage("NoSelectData");
            //}
            //grdAffectLot.DataSource = dt;
            //grdAffectLot.Refresh();
        }

        #endregion

        #region Private Function

        #endregion
    }

}
