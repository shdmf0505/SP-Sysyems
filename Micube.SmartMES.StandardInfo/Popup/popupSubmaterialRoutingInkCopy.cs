#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 부자재 Routing BOM > 복사하기 이벤트 팝업
    /// 업  무  설  명  : 부자재 Routing BOM 화면에서 부자재 타입이 Ink인 경우 발생하는 팝업
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2021-02-17
    /// 수  정  이  력  :
    ///
    /// </summary>
    public partial class popupSubmaterialRoutingInkCopy : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Interface

        public DataRow CurrentDataRow { get; set; }

        #endregion Interface

        #region Local Variables

        /// <summary>
        /// 공정리스트 조회하기 위한 파라미터
        /// </summary>
        private Dictionary<string, object> _param;

        /// <summary>
        /// CheckRow 전달
        /// </summary>
        /// <param name="dt"></param>
        public delegate void SelectedRowTableHandler(DataRow dr);

        public event SelectedRowTableHandler SelectedRowTableEvent;

        #endregion Local Variables

        #region 생성자

        public popupSubmaterialRoutingInkCopy()
        {
            InitializeComponent();

            InitializeLanguageKey();
            InitializeGrid();
            InitializeEvent();
            InitializeControls();
        }

        #endregion 생성자

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

            // 품목조회
            ConditionItemSelectPopup condition = new ConditionItemSelectPopup();
            condition.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            condition.SetPopupLayout("PRODUCTDEFID", PopupButtonStyles.Ok_Cancel);

            condition.Id = "PRODUCTDEFID";
            condition.LabelText = "PRODUCTDEFID";
            condition.SearchQuery = new SqlQuery("GetProductDefInk", "10001",
                                                new Dictionary<string, object> {    { "ENTERPRISEID", UserInfo.Current.Enterprise },
                                                                                    { "PLANTID", UserInfo.Current.Plant } });
            condition.IsMultiGrid = false;
            condition.DisplayFieldName = "PRODUCTDEFID";
            condition.ValueFieldName = "PRODUCTDEFID";
            condition.LanguageKey = "PRODUCTDEFID";
            condition.Conditions.AddTextBox("PRODUCTDEFID");
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 80);
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 70);
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 150);
            condition.SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
            {
                selectedRow.ForEach(row =>
                {
                    _param = row.Table.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => row[col.ColumnName]);
                    _param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    _param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    _param.Add("PLANTID", UserInfo.Current.Plant);

                    sspProduct.Text = Format.GetString(row["PRODUCTDEFID"], string.Empty);
                    txtProductRev.Text = Format.GetString(row["PRODUCTDEFVERSION"], string.Empty);
                    txtProductName.Text = Format.GetString(row["PRODUCTDEFNAME"], string.Empty);
                    Search();
                });
            });

            sspProduct.SelectPopupCondition = condition;
            txtProductRev.ReadOnly = true;

            layoutProduct.AppearanceItemCaption.ForeColor = Color.Red;
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            this.LanguageKey = "COPY";
            layoutMain.SetLanguageKey(layoutProduct, "PRODUCTDEFID");
            layoutMain.SetLanguageKey(layoutProductRev, "ASSEMBLYITEMVERSION");
            layoutMain.SetLanguageKey(layoutProductName, "PRODUCTDEFNAME");
            btnSearch.LanguageKey = "SEARCH";
            btnOK.LanguageKey = "OK";
            btnCancel.LanguageKey = "CANCEL";
            grdMain.LanguageKey = "SUBSIDIARYINFO";
        }

        /// <summary>
        /// Grid 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region 부자재 Routing BOM 항목

            grdMain.GridButtonItem = GridButtonItem.None;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdMain.View.AddTextBoxColumn("PROCESSSEGMENTID", 80);
            grdMain.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 130);
            grdMain.View.AddTextBoxColumn("MATERIALID", 140).SetLabel("CONSUMABLEDEFID");
            grdMain.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 120);

            grdMain.View.AddSpinEditColumn("INKIMPORTANCE", 80).SetLabel("INKSPECIFICGRAVITY");
            grdMain.View.AddSpinEditColumn("NONVOLATILEMATTER", 80);
            grdMain.View.AddSpinEditColumn("COVERAGECS", 90);
            grdMain.View.AddSpinEditColumn("COVERAGESS", 90);
            grdMain.View.AddSpinEditColumn("INKTHICKNESS", 80);
            grdMain.View.AddSpinEditColumn("STANDARDQTY", 80);

            grdMain.View.AddSpinEditColumn("PRODUCTLOSS", 80);
            grdMain.View.AddSpinEditColumn("OUTPUTLOSS", 80);
            grdMain.View.AddSpinEditColumn("STANDARDQTYLOSS", 80);

            grdMain.View.PopulateColumns();
            grdMain.View.BestFitColumns();
            grdMain.View.SetIsReadOnly();

            grdMain.ShowStatusBar = true;

            #endregion 부자재 Routing BOM 항목
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// Event 초기화
        /// </summary>
        private void InitializeEvent()
        {
            // 조회 버튼 클릭 이벤트
            btnSearch.Click += (s, e) => Search();

            // 확인 버튼 클릭 이벤트
            btnOK.Click += (s, e) =>
            {
                if (grdMain.View.FocusedRowHandle < 0)
                {
                    this.Close();
                }

                this.DialogResult = DialogResult.OK;
                SelectedRowTableEvent(grdMain.View.GetFocusedDataRow());
            };

            // 취소 버튼 클릭 이벤트
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
        }

        #endregion Event

        #region private Function

        /// <summary>
        /// 조회 버튼 클릭
        /// </summary>
        private void Search()
        {
            grdMain.View.ClearDatas();

            if (Format.GetString(sspProduct.Text, string.Empty).Equals(string.Empty))
            {
                ShowMessage("ToolRequestProductCodeValidation");
                return;
            }

            _param["PRODUCTDEFID"] = Format.GetString(sspProduct.Text, string.Empty);
            _param["PRODUCTDEFVERSION"] = Format.GetString(txtProductRev.Text, string.Empty);

            if (SqlExecuter.Query("GetProcesssegmentCopyInk", "10001", _param) is DataTable dt)
            {
                if (dt.Rows.Count.Equals(0))
                {
                    ShowMessage("NoSelectData");
                    return;
                }

                grdMain.DataSource = dt;
            }
        }

        #endregion private Function

    }
}