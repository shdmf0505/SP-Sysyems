#region using

using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 수율의 조회 조건에 품목 List 팝업이 사용되는 User Control
    /// 업  무  설  명  : 수율화면 품목 List에 들어가는 공통 모듈
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-10-25
    /// 필  수  처  리  :
    /// 
    /// </summary>
    public partial class popupProductListByPeriod : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Interface

        public DataRow CurrentDataRow { get; set; }

        #endregion

        #region Local Variables

        /// <summary>
        /// 선택한 Row 전달
        /// </summary>
        /// <param name="dr"></param>
        public delegate void SelectedRowHandler(DataRow dr);
        public event SelectedRowHandler SelectedRowEvent;

        #endregion

        #region 생성자
        public popupProductListByPeriod()
        {
            InitializeComponent();

            InitializeLanguageKey();
            InitializeGrid();
            InitializeEvent();
            InitializeControls();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// Controler 초기화
        /// </summary>
        private void InitializeControls()
        {
            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            dtpFrom.Properties.EditMask = "yyyy-MM-dd HH:mm:ss";
            dtpFrom.Properties.Mask.UseMaskAsDisplayFormat = true;

            dtpTo.Properties.EditMask = "yyyy-MM-dd HH:mm:ss";
            dtpTo.Properties.Mask.UseMaskAsDisplayFormat = true;
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            layoutMain.SetLanguageKey(layoutFrom, "DATEFROM");
            layoutMain.SetLanguageKey(layoutTo, "DATETO");
            layoutMain.SetLanguageKey(layoutProduct, "PRODUCTDEFID");

            this.LanguageKey = "GRIDPRODUCTLIST";
            grdMain.LanguageKey = "GRIDPRODUCTLIST";
            btnSearch.LanguageKey = "SEARCH";
            btnOK.LanguageKey = "OK";
            btnCancel.LanguageKey = "CANCEL";
        }

        /// <summary>
        /// Grid 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdMain.View.AddTextBoxColumn("P_PRODUCTDEFID", 120).SetLabel("PRODUCTDEFID");
            grdMain.View.AddTextBoxColumn("P_PRODUCTDEFNAME", 250).SetLabel("PRODUCTDEFNAME");
            grdMain.View.AddTextBoxColumn("P_PERIOD_PERIODFR", 250).SetIsHidden();
            grdMain.View.AddTextBoxColumn("P_PERIOD_PERIODTO", 250).SetIsHidden();

            grdMain.View.PopulateColumns();
            grdMain.View.BestFitColumns();
            grdMain.View.SetIsReadOnly();

            grdMain.View.CheckMarkSelection.MultiSelectCount = 1;
            grdMain.View.OptionsCustomization.AllowColumnMoving = false;
            grdMain.GridButtonItem = GridButtonItem.Export;
        }

        #endregion

        #region Event

        /// <summary>
        /// Event 초기화 
        /// </summary>
        private void InitializeEvent()
        {
            //! 조회 버튼 클릭 이벤트
            btnSearch.Click += (s, e) => Search();

            //! OK버튼 클릭 이벤트
            btnOK.Click += (s, e) =>
            {
                if (!grdMain.View.GetCheckedRows().Rows.Count.Equals(0))
                {
                    DataRow dr = grdMain.View.GetCheckedRows().Rows[0];

                    dr["P_PERIOD_PERIODFR"] = dtpFrom.Text;
                    dr["P_PERIOD_PERIODTO"] = dtpTo.Text;

                    this.SelectedRowEvent(dr);
                }

                this.Close();
            };

            //! 취소버튼 클릭 이벤트
            btnCancel.Click += (s, e) => this.Close();
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 조회
        /// </summary>
        private void Search()
        {
            Dictionary<string, object> value = new Dictionary<string, object>()
                {
                    {"P_PERIOD_PERIODFR", dtpFrom.Text },
                    {"P_PERIOD_PERIODTO", dtpTo.Text },
                    {"P_PRODUCTDEFID", txtProduct.Text }
                };

            grdMain.DataSource = SqlExecuter.Query("GetDefectMapProductList", "10002", value);
        }

        #endregion

        #region Public Function

        /// <summary>
        /// Row Data로 화면 설정한다
        /// </summary>
        /// <param name="title">화면 타이틀</param>
        /// <param name="dt">Row Data</param>
        public void SetParams(string from, string to, string product)
        {
            dtpFrom.Text = from;
            dtpTo.Text = to;
            txtProduct.Text = product;

            Search();
        }

        #endregion
    }
}