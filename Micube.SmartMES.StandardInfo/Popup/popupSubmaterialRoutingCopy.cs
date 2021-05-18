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
    /// 업  무  설  명  : 부자재 Routing BOM 화면에서 부자재 타입에 따라 복사하기 위한 팝업
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2021-01-13
    /// 수  정  이  력  :
    ///
    /// </summary>
    public partial class popupSubmaterialRoutingCopy : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Interface

        public DataRow CurrentDataRow { get; set; }

        #endregion Interface

        #region Local Variables

        /// <summary>
        /// 부자재 타입
        /// </summary>
        private string _subMaterialType = string.Empty;

        /// <summary>
        /// 공정리스트 조회하기 위한 파라미터
        /// </summary>
        private Dictionary<string, object> _param;

        /// <summary>
        /// CheckRow 전달
        /// </summary>
        /// <param name="dt"></param>
        public delegate void SelectedRowTableHandler(DataTable dt);

        public event SelectedRowTableHandler SelectedRowTableEvent;

        #endregion Local Variables

        #region 생성자

        public popupSubmaterialRoutingCopy(string subMaterialType)
        {
            InitializeComponent();

            _subMaterialType = subMaterialType;

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
            condition.SearchQuery = new SqlQuery("GetProductDefXY", "10001",
                                                new Dictionary<string, object> {    { "SUBMATERIALTYPE", _subMaterialType },
                                                                                    { "ENTERPRISEID", UserInfo.Current.Enterprise },
                                                                                    { "PLANTID", UserInfo.Current.Plant } });
            condition.IsMultiGrid = false;
            condition.DisplayFieldName = "PRODUCTDEFID";
            condition.ValueFieldName = "PRODUCTDEFID";
            condition.LanguageKey = "PRODUCTDEFID";
            condition.Conditions.AddTextBox("PRODUCTDEFID");
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 80);
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 150);
            condition.GridColumns.AddTextBoxColumn("PNLX", 50).SetIsHidden();
            condition.GridColumns.AddTextBoxColumn("PNLY", 50).SetIsHidden();
            condition.SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
            {
                selectedRow.ForEach(row =>
                {
                    _param = row.Table.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => row[col.ColumnName]);
                    _param.Add("SUBMATERIALTYPE", _subMaterialType);
                    _param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    _param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    _param.Add("PLANTID", UserInfo.Current.Plant);

                    sspProduct.Text = Format.GetString(row["PRODUCTDEFID"], string.Empty);
                    txtProductRev.Text = Format.GetString(row["PRODUCTDEFVERSION"], string.Empty);
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
            btnSearch.LanguageKey = "SEARCH";
            btnOK.LanguageKey = "OK";
            btnCancel.LanguageKey = "CANCEL";
            grdLeft.LanguageKey = "OPERATION";
            grdRight.LanguageKey = "SUBSIDIARYINFO";
        }

        /// <summary>
        /// Grid 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region 공정 리스트

            grdLeft.GridButtonItem = GridButtonItem.Export;
            grdLeft.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdLeft.View.AddTextBoxColumn("PROCESSSEGMENTID", 80);
            grdLeft.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 130);
            grdLeft.View.AddTextBoxColumn("MATERIALTYPE").SetIsHidden();
            grdLeft.View.AddTextBoxColumn("PRODUCTDEFID").SetIsHidden();
            grdLeft.View.AddTextBoxColumn("PRODUCTDEFVERSION").SetIsHidden();
            grdLeft.View.AddTextBoxColumn("PNLX").SetIsHidden();
            grdLeft.View.AddTextBoxColumn("PNLY").SetIsHidden();

            grdLeft.View.PopulateColumns();
            grdLeft.View.BestFitColumns();
            grdLeft.View.SetIsReadOnly();

            grdLeft.ShowStatusBar = true;

            #endregion 공정 리스트

            #region 부자재 Routing BOM 항목

            grdRight.GridButtonItem = GridButtonItem.Export;
            grdRight.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdRight.View.AddComboBoxColumn("DRYFILIMTYPE", 40, new SqlQuery("GetCodeList", "00001", "CODECLASSID=STICKTYPE", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetLabel("TYPE");
            grdRight.View.AddComboBoxColumn("STICKMETHOD", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=STICKMETHOD", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdRight.View.AddComboBoxColumn("STICKDIRECTION", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=STICKDIRECTION", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdRight.View.AddTextBoxColumn("MATERIALID", 110).SetLabel("CONSUMABLEDEFID");
            grdRight.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 160);
            grdRight.View.AddComboBoxColumn("WORKPLANE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DoubleSingleSided", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdRight.View.AddTextBoxColumn("EQUIPMENTID", 70);
            grdRight.View.AddTextBoxColumn("EQUIPMENTNAME", 160);

            grdRight.View.AddTextBoxColumn("MATERIALTYPE").SetIsHidden();
            grdRight.View.AddTextBoxColumn("DRYFILIMNO", 50).SetIsHidden();
            grdRight.View.AddTextBoxColumn("PNLX").SetIsHidden();
            grdRight.View.AddTextBoxColumn("PNLY").SetIsHidden();
            grdRight.View.AddTextBoxColumn("PNLAREA").SetIsHidden();
            grdRight.View.AddTextBoxColumn("CIRCUITDIVISION").SetIsHidden();
            grdRight.View.AddTextBoxColumn("STANDARDDIVISION").SetIsHidden();

            grdRight.View.PopulateColumns();
            grdRight.View.SetIsReadOnly();

            grdRight.ShowStatusBar = true;

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
                if (!grdRight.View.GetCheckedRows().Rows.Count.Equals(0))
                {
                    this.DialogResult = DialogResult.OK;
                    SelectedRowTableEvent(grdRight.View.GetCheckedRows());
                }

                this.Close();
            };

            // 공정 Grid Row 변경시 이벤트
            grdLeft.View.FocusedRowChanged += (s, e) =>
            {
                if (e.FocusedRowHandle < 0)
                {
                    return;
                }

                Dictionary<string, object> param = grdLeft.View.GetFocusedDataRow().Table.Columns
                                                          .Cast<DataColumn>()
                                                          .ToDictionary(col => col.ColumnName, col => grdLeft.View.GetFocusedDataRow()[col.ColumnName]);
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                grdRight.View.ClearDatas();

                if (SqlExecuter.Query("GetSubMaterialRoutingCopy", "10001", param) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData");
                        return;
                    }

                    grdRight.DataSource = dt;
                }
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
            grdLeft.View.ClearDatas();
            grdRight.View.ClearDatas();

            if (Format.GetString(sspProduct.Text, string.Empty).Equals(string.Empty))
            {
                ShowMessage("ToolRequestProductCodeValidation");
                return;
            }

            _param["PRODUCTDEFID"] = Format.GetString(sspProduct.Text, string.Empty);
            _param["PRODUCTDEFVERSION"] = Format.GetString(txtProductRev.Text, string.Empty);

            if (SqlExecuter.Query("GetProcesssegmentCopyXY", "10001", _param) is DataTable dt)
            {
                if (dt.Rows.Count.Equals(0))
                {
                    ShowMessage("NoSelectData");
                    return;
                }

                grdLeft.DataSource = dt;
            }
        }

        #endregion private Function
    }
}