#region using

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
    /// 프 로 그 램 명  : 품질관리 > 날짜선택 Popup
    /// 업  무  설  명  : 
    /// 생    성    자  : 류시윤
    /// 생    성    일  : 2019-10-25
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class DaySelectPopup : SmartPopupBaseForm
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

        public Dictionary<string, object> Conditions { get; set; }

        #endregion

        #region Local Variables

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public DaySelectPopup()
        {
            InitializeComponent();

            InitializeEvent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        private void InitializeGrid()
        {
            grdDays.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdDays.View.SetIsReadOnly();

            // 일자
            grdDays.View.AddTextBoxColumn("SUMMARYDATE", 80).SetLabel("DATE").SetTextAlignment(TextAlignment.Center);

            // 투입수
            grdDays.View.AddTextBoxColumn("TOTALINPUTQTY", 100)
                .SetLabel("PCSINPUTQTY")
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            // 불량수
            grdDays.View.AddTextBoxColumn("TOTALDEFECTQTY", 100)
                .SetLabel("PCSDEFECTQTY")
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            // 불량율
            grdDays.View.AddTextBoxColumn("TOTALDEFECTRATE", 100)
                    .SetLabel("PCSDEFECTRATE")
                    .SetTextAlignment(TextAlignment.Center)
                    .SetDisplayFormat("##0.#0 %", MaskTypes.Numeric);

            grdDays.View.PopulateColumns();
        }

        #endregion

        #region Popup

        /// <summary>
        /// 품목 검색팝업
        /// </summary>
        private void selectProudctPopup()
        {
            //ConditionItemSelectPopup popup = new ConditionItemSelectPopup();

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
            this.Load += DaySelectPopup_Load;

            this.btnLOTView.Click += (s, e) =>
            {
                LotViewPopup popup = new LotViewPopup();

                popup.StartPosition = FormStartPosition.CenterParent;

                DataTable dt = grdDays.View.GetCheckedRows();
                string summaryDate = string.Join(",", dt.Rows.OfType<DataRow>().Select(r => r[0].ToString()));
                if (this.Conditions.ContainsKey("CHECKDATE"))
                    this.Conditions["CHECKDATE"] = summaryDate;
                else
                    this.Conditions.Add("CHECKDATE", summaryDate);

                popup.Conditions = this.Conditions;

                popup.ShowDialog();
                if (popup.DialogResult == DialogResult.OK)
                {
                    this._checkTable = popup._checkTable;
                    this.Conditions = popup.Conditions;

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            };
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
            //if (grdAffectLot.View.RowCount == 0)
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
        private void DaySelectPopup_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            InitializeGrid();

            Search();

            //selectProudctPopup();
        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색
        /// </summary>
        /// <returns></returns>
        private void Search()
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                {"LANGUAGETYPE" , Framework.UserInfo.Current.LanguageType},
                {"PLANTID" , Framework.UserInfo.Current.Plant},
                {"ENTERPRISEID" , Framework.UserInfo.Current.Enterprise},
                {"P_PERIOD_PERIODFR", Conditions["P_PERIOD_PERIODFR"] },
                {"P_PERIOD_PERIODTO", Conditions["P_PERIOD_PERIODTO"] },
                {"P_PRODUCTDEFID", Conditions["P_PRODUCTDEFID"] },
                {"P_INSPTYPE", "" }
            };

            string qryYPE = string.Empty;
            if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
                qryYPE = "YPE";

            DataTable dt = SqlExecuter.Query("SelectYieldRateByDay" + qryYPE, "10001", param);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData");
            }

            grdDays.DataSource = dt;
            grdDays.Refresh();
        }

        #endregion

        #region Private Function

        #endregion
    }

}
