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
    /// 업  무  설  명  : 부자재 Routing BOM 화면에서 부자재 타입이 HP인 경우 복사하기 위한 팝업
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2021-01-29
    /// 수  정  이  력  :
    ///
    /// </summary>
    public partial class popupSubmaterialRoutingHpCopy : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Interface

        public DataRow CurrentDataRow { get; set; }

        #endregion Interface

        #region Local Variables

        /// <summary>
        /// Check Data 전달
        /// </summary>
        /// <param name="dt"></param>
        public delegate void SelectedRowTableSetHandler(DataSet ds);
        public event SelectedRowTableSetHandler SelectedRowTableSetEvent;

        #endregion Local Variables

        #region 생성자

        public popupSubmaterialRoutingHpCopy()
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
            var condition = new ConditionItemSelectPopup();
            condition.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            condition.SetPopupLayout("PRODUCTDEFID", PopupButtonStyles.Ok_Cancel);

            condition.Id = "PRODUCTDEFID";
            condition.LabelText = "PRODUCTDEFID";
            condition.SearchQuery = new SqlQuery("GetProductDefHP", "10001",
                                                new Dictionary<string, object> {    { "SUBMATERIALTYPE", "HP" },
                                                                                    { "ENTERPRISEID", UserInfo.Current.Enterprise },
                                                                                    { "PLANTID", UserInfo.Current.Plant } });
            condition.IsMultiGrid = false;
            condition.DisplayFieldName = "PRODUCTDEFID";
            condition.ValueFieldName = "PRODUCTDEFID";
            condition.LanguageKey = "PRODUCTDEFID";
            condition.Conditions.AddTextBox("PRODUCTDEFID");
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 80);
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 150);
            condition.SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
            {
                selectedRow.ForEach(row =>
                {
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
            grdInfo.LanguageKey = "HPINFO";
            grdLayup.LanguageKey = "LAYUPLIST";
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

            grdLeft.View.AddTextBoxColumn("SUBMATERIALTYPE").SetIsHidden();
            grdLeft.View.AddTextBoxColumn("ITEMID").SetIsHidden();
            grdLeft.View.AddTextBoxColumn("ITEMVERSION").SetIsHidden();
            grdLeft.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID").SetIsHidden();
            grdLeft.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdLeft.View.AddTextBoxColumn("PLANTID").SetIsHidden();

            grdLeft.View.PopulateColumns();
            grdLeft.View.BestFitColumns();
            grdLeft.View.SetIsReadOnly();

            grdLeft.ShowStatusBar = false;

            #endregion 공정 리스트

            #region Grid H/P 내역

            grdInfo.GridButtonItem = GridButtonItem.None;
            grdInfo.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdInfo.View.AddComboBoxColumn("RACKSIZE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=RackSize", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdInfo.View.AddTextBoxColumn("TIMEMINUTE", 60).SetTextAlignment(TextAlignment.Right);
            grdInfo.View.AddTextBoxColumn("PRESS", 70).SetLabel("PRESSUREBAR").SetTextAlignment(TextAlignment.Right);
            grdInfo.View.AddTextBoxColumn("TEMPC", 80).SetTextAlignment(TextAlignment.Right);
            grdInfo.View.AddTextBoxColumn("COOLINGTEMPC", 90).SetTextAlignment(TextAlignment.Right);
            grdInfo.View.AddTextBoxColumn("GAONTIMEMINUTE", 90).SetLabel("GAONTIME").SetTextAlignment(TextAlignment.Right);
            grdInfo.View.AddTextBoxColumn("GAMONTIMEMINUTE", 90).SetLabel("GAMONTIME").SetTextAlignment(TextAlignment.Right);
            grdInfo.View.AddTextBoxColumn("STACK", 50).SetTextAlignment(TextAlignment.Right);
            grdInfo.View.AddComboBoxColumn("SINGLE", 40, new SqlQuery("GetCodeList", "00001", "CODECLASSID=SingleColumn", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdInfo.View.AddComboBoxColumn("COL", 40, new SqlQuery("GetCodeList", "00001", "CODECLASSID=SingleColumn", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetLabel("COLUMN");
            grdInfo.View.AddComboBoxColumn("BOX", 50, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdInfo.View.AddComboBoxColumn("BAKING", 70, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdInfo.View.PopulateColumns();
            grdInfo.View.SetIsReadOnly();

            grdInfo.ShowStatusBar = true;

            #endregion Grid H/P 내역

            #region Layup 이력

            grdLayup.GridButtonItem = GridButtonItem.Export;
            grdLayup.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdLayup.View.AddComboBoxColumn("UOMDEFID", 50, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=OutsourcingSpec"), "UOMDEFNAME", "UOMDEFID")
                         .SetLabel("UOM")
                         .SetTextAlignment(TextAlignment.Center);
            grdLayup.View.AddComboBoxColumn("DETAILTYPE", 70, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecVacuumNormal", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                         .SetLabel("VACUUMNORMAL");
            grdLayup.View.AddComboBoxColumn("SPECSUBTYPE", 110, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecStackMaterialType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                         .SetLabel("LAYUPSTRUCT");

            grdLayup.View.AddTextBoxColumn("MATERIALDEFID", 120);
            grdLayup.View.AddTextBoxColumn("MATERIALNAME", 150);
            grdLayup.View.AddTextBoxColumn("SPEC", 70);
            grdLayup.View.AddComboBoxColumn("COMPONENTUOM", 70, new SqlQuery("GetUOMList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "UOMDEFID", "UOMDEFNAME");
            grdLayup.View.AddTextBoxColumn("MINVALUE", 90).SetTextAlignment(TextAlignment.Right).SetLabel("COMPONENTQTY");
            grdLayup.View.AddTextBoxColumn("REALQTY", 60).SetTextAlignment(TextAlignment.Right);

            grdLayup.View.AddTextBoxColumn("OUTSOURCINGSPECNO").SetIsHidden();
            grdLayup.View.AddTextBoxColumn("MATERIALDEFVERSION").SetIsHidden();
            grdLayup.View.AddTextBoxColumn("ITEMTYPE").SetIsHidden();

            grdLayup.View.PopulateColumns();
            grdLayup.View.SetIsReadOnly();

            grdLayup.ShowStatusBar = true;

            #endregion Layup 이력
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
                DataSet ds = new DataSet();
                ds.Tables.Add((grdInfo.DataSource as DataTable).Copy());
                ds.Tables.Add(grdLayup.View.GetCheckedRows());

                this.DialogResult = DialogResult.OK;
                SelectedRowTableSetEvent(ds);
            };

            // 공정 Grid Row 변경시 이벤트
            grdLeft.View.FocusedRowChanged += (s, e) =>
            {
                if(e.FocusedRowHandle < 0)
                {
                    return;
                }

                Dictionary<string, object> param = grdLeft.View.GetFocusedDataRow().Table.Columns
                                                          .Cast<DataColumn>()
                                                          .ToDictionary(col => col.ColumnName, col => grdLeft.View.GetFocusedDataRow()[col.ColumnName]);
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                grdInfo.View.ClearDatas();
                grdLayup.View.ClearDatas();

                if (SqlExecuter.Query("SelectSubMaterialDivisionByHPBOMRouting", "10001", param) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData");
                        return;
                    }

                    grdInfo.DataSource = dt;

                    param.Add("SINGLE", dt.Rows[0]["SINGLE"]);
                    param.Add("COL", dt.Rows[0]["COL"]);
                    grdLayup.DataSource = SqlExecuter.Query("SelectSubMaterialDivisionByHPBOMRoutingLayup", "10001", param);
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

            if(Format.GetString(sspProduct.Text, string.Empty).Equals(string.Empty))
            {
                ShowMessage("ToolRequestProductCodeValidation");
                return;
            }

            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                { "MATERIALTYPE", "HP" },
                { "PRODUCTDEFID", sspProduct.Text },
                { "PRODUCTDEFVERSION", txtProductRev.Text },
                { "ENTERPRISEID", UserInfo.Current.Enterprise },
                { "PLANTID", UserInfo.Current.Plant },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType }
            };

            if (SqlExecuter.Query("GetProcesssegmentCopyHp", "10001", param) is DataTable dt)
            {
                if (dt.Rows.Count.Equals(0))
                {
                    ShowMessage("NoSelectData");
                    return;
                }

                grdLeft.DataSource = dt;
                grdLeft.View.SelectRow(0);
            }
        }

        #endregion private Function
    }
}