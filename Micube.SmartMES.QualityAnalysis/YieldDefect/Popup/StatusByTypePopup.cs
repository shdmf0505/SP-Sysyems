#region using

using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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
    public partial class StatusByTypePopup : SmartPopupBaseForm
    {
        #region Global Variable

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
        public StatusByTypePopup()
        {
            InitializeComponent();

            InitializeEvent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        private void InitializeGrid()
        {
            grdStatusByType.View.SetIsReadOnly();

            var type = grdStatusByType.View.AddGroupColumn("TYPE");
            type.AddTextBoxColumn("PRODUCTSHAPE", 80)
                .SetTextAlignment(TextAlignment.Center);

            var pcs = grdStatusByType.View.AddGroupColumn("GROUPPCSYIELDRATE");
            pcs.AddTextBoxColumn("PCSDEFECTQTY", 80)
                .SetLabel("PCSDEFECTQTY")
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            pcs.AddTextBoxColumn("PCSDEFECTRATE", 50)
                .SetLabel("PCSDEFECTRATE")
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("##0.#0 %", MaskTypes.Numeric);
            
            var areaCol = grdStatusByType.View.AddGroupColumn("GROUPAREAYIELDRATE");
            // 불량면적
            areaCol.AddTextBoxColumn("AREADEFECTQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.#0", MaskTypes.Numeric);
            // 면적불량율
            areaCol.AddTextBoxColumn("AREADEFECTRATE", 50).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric);

            var priceCol = grdStatusByType.View.AddGroupColumn("GROUPPRICEYIELDRATE");
            // 불량금액
            priceCol.AddTextBoxColumn("PRICEDEFECTQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);
            // 금액불량율
            priceCol.AddTextBoxColumn("PRICEDEFECTRATE", 50).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric);

            var inputCol = grdStatusByType.View.AddGroupColumn("GROUPINPUTQTY");

            // PCS 투입수
            inputCol.AddTextBoxColumn("PCSINPUTQTY", 80).SetLabel("PCS").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);

            // 면적투입수
            inputCol.AddTextBoxColumn("AREAINPUTQTY", 80).SetLabel("M2QTY").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.#0", MaskTypes.Numeric);

            // 금액투입수
            inputCol.AddTextBoxColumn("PRICEINPUTQTY", 100).SetLabel("AMOUNTS").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);

            grdStatusByType.View.PopulateColumns();
            grdStatusByType.View.BestFitColumns(true);
        }

        #endregion

        #region Popup

        /// <summary>
        /// 품목 검색팝업
        /// </summary>
        private void selectProudctPopup()
        {
            ;
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += statusByTypePopup_Load;
        }

        /// <summary>
        /// Form Load시 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void statusByTypePopup_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            InitializeGrid();

            selectProudctPopup();

            if (DataSource != null && DataSource.Rows.Count > 0)
            {
                DataTable newDt = DataSource.AsEnumerable()
                                      .GroupBy(r => r.Field<string>("PRODUCTSHAPE"))
                                      .Select(g =>
                                      {
                                          var row = DataSource.NewRow();

                                          row["PRODUCTSHAPE"] = g.Key;
                                          row["PCSDEFECTQTY"] = g.Sum(r => r.Field<decimal?>("PCSDEFECTQTY"));
                                          row["PCSDEFECTRATE"] = g.Sum(r => r.Field<decimal?>("PCSDEFECTRATE"));
                                          row["AREADEFECTQTY"] = g.Sum(r => r.Field<decimal?>("AREADEFECTQTY"));
                                          row["AREADEFECTRATE"] = g.Sum(r => r.Field<decimal?>("AREADEFECTRATE"));
                                          row["PRICEDEFECTQTY"] = g.Sum(r => r.Field<double?>("PRICEDEFECTQTY"));
                                          row["PRICEDEFECTRATE"] = g.Sum(r => r.Field<decimal?>("PRICEDEFECTRATE"));
                                          row["PCSINPUTQTY"] = g.Max(r => r.Field<decimal?>("PCSINPUTQTY"));
                                          row["AREAINPUTQTY"] = g.Max(r => r.Field<decimal?>("AREAINPUTQTY"));
                                          row["PRICEINPUTQTY"] = g.Max(r => r.Field<decimal?>("PRICEINPUTQTY"));

                                          return row;
                                      }).CopyToDataTable();

                grdStatusByType.DataSource = newDt;
            }

            grdStatusByType.View.BestFitColumns(true);


            grdStatusByType.View.DoubleClick += View_DoubleClick;
        }

        /// <summary>
        /// 그리드 뷰의 더블클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(ea.Location);

            //DataTable dt;// = DataSource.Clone();

            DataRow dr = view.GetDataRow(info.RowHandle);
            //DataRow newRow = null;

            string strFilter = "PRODUCTSHAPE = '" + dr["PRODUCTSHAPE"].ToString() + "'";

            DataView dv = DataSource.DefaultView;
            dv.RowFilter = strFilter;
            DataTable dt = dv.ToTable().Copy();
            //if (dr.ItemArray.Count() > 0)
            //{
            //    newRow = dt.NewRow();
            //    foreach (DataColumn col in dt.Columns)
            //    {
            //        newRow[col.ToString()] = dr[col.ToString()];
            //    }
            //}

            //if (newRow != null)
            //    dt.Rows.Add(newRow);

            StatusByItemPopup popup = new StatusByItemPopup();
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.Width = 1220;
 
            popup.SearchConditions = this.SearchConditions;

            popup.DataSource = dt;

            popup.ShowDialog();
            if (popup.DialogResult == DialogResult.OK)
            {
                this.OpenMenu = popup.OpenMenu;
                this.OpenAction = popup.OpenAction;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                this.OpenMenu = string.Empty;
                this.OpenAction = string.Empty;

                this.Close();
            }
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
