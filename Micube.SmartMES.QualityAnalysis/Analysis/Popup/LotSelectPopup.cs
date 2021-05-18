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
    /// 프 로 그 램 명  : 품질관리 > LOT 선택 Popup
    /// 업  무  설  명  : 
    /// 생    성    자  : 류시윤
    /// 생    성    일  : 2019-10-25
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class LotSelectPopup : SmartPopupBaseForm
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

        public DataTable ParentDataTable
        {
            get { return this._parentTable; }
            set { this._parentTable = value; }
        }

        public string LotIDs = string.Empty;

        public DateTime FromTime;
        public DateTime ToTime;

        public string StrPeriod
        {
            set { lblTbxPeriod.EditValue = value; }
        }

        public string ProductDefID
        {
            get { return this.prodDefID; }
            set
            {
                this.prodDefID = value;
                popupProductId.EditValue = value;
            }
        }

        public Dictionary<string, object> SearchConditions { get; set; }

        #endregion

        #region Local Variables

        DataTable _parentTable = null;

        private string prodDefID;

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public LotSelectPopup()
        {
            InitializeComponent();

            InitializeEvent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        private void InitializeGrid()
        {
            #region Source Grid Setting

            gridLeft.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            gridLeft.View.SetIsReadOnly();
            gridLeft.View.AddTextBoxColumn("LOTID", 200)
                .SetTextAlignment(TextAlignment.Center);
            // 품목명
            gridLeft.View.AddTextBoxColumn("PRODUCTDEFNAME", 100).SetTextAlignment(TextAlignment.Left);
            // 품목코드
            gridLeft.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetTextAlignment(TextAlignment.Left);
            // 품목REV
            gridLeft.View.AddTextBoxColumn("PRODUCTDEFVERSION", 70).SetTextAlignment(TextAlignment.Center);

            gridLeft.View.AddTextBoxColumn("DEFECTQTY", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetTextAlignment(TextAlignment.Right);
            gridLeft.View.AddTextBoxColumn("DEFECTRATE", 80)
                .SetDisplayFormat("##0.#0 %", MaskTypes.Numeric)
                .SetTextAlignment(TextAlignment.Right);

            gridLeft.GridButtonItem = GridButtonItem.None;
            gridLeft.View.PopulateColumns();

            #endregion Source Grid Setting

            #region Target Grid Setting

            gridRight.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            gridRight.View.SetIsReadOnly();

            gridRight.View.AddTextBoxColumn("LOTID", 200)
                .SetTextAlignment(TextAlignment.Center);
            // 품목명
            gridRight.View.AddTextBoxColumn("PRODUCTDEFNAME", 100).SetTextAlignment(TextAlignment.Left);
            // 품목코드
            gridRight.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetTextAlignment(TextAlignment.Left);
            // 품목REV
            gridRight.View.AddTextBoxColumn("PRODUCTDEFVERSION", 70).SetTextAlignment(TextAlignment.Center);
            gridRight.View.AddTextBoxColumn("DEFECTQTY", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetTextAlignment(TextAlignment.Right);
            gridRight.View.AddTextBoxColumn("DEFECTRATE", 80)
                .SetDisplayFormat("##0.#0 %", MaskTypes.Numeric)
                .SetTextAlignment(TextAlignment.Right);

            gridRight.GridButtonItem = GridButtonItem.None;
            gridRight.View.PopulateColumns();

            #endregion Target Grid Setting

            leftRightBtn.SourceGrid = gridLeft;
            leftRightBtn.TargetGrid = gridRight;
        }

        #endregion

        #region Popup

        /// <summary>
        /// 품목 검색팝업
        /// </summary>
        private void selectProudctPopup()
        {
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();

            //AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductListByYieldStatus", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFIDVERSION", "PRODUCTDEFIDVERSION")
            //                             .SetLabel("PRODUCTDEFID")
            //                             .SetPopupLayout("GRIDPRODUCTLIST", PopupButtonStyles.Ok_Cancel, true, false)
            //                             .SetPopupLayoutForm(650, 400, FormBorderStyle.FixedToolWindow)
            //                             .SetPosition(1.1)
            //                             .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
            //                             {
            //                                 foreach (DataRow row in selectedRow)
            //                                 {
            //                                     SetCondition(row);
            //                                 }
            //                             });
            popup.SetPopupLayoutForm(600, 400, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("GRIDPRODUCTLIST", PopupButtonStyles.Ok_Cancel, true, false);
            popup.Id = "PRODUCT";
            popup.SearchQuery = new SqlQuery("GetProductListByYieldStatus", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "PRODUCTDEFIDVERSION";
            popup.ValueFieldName = "PRODUCTDEFIDVERSION";
            popup.LanguageKey = "PRODUCTDEFID";

            popup.Conditions.AddTextBox("PRODUCTDEFID");

            popup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            popup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            popup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 50);

            popupProductId.SelectPopupCondition = popup;
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += LotSelectPopup_Load;

            btnSeach.Click += BtnSeach_Click;
        }

        /// <summary>
        /// LOT 검색
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSeach_Click(object sender, EventArgs e)
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
        private void LotSelectPopup_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            InitializeGrid();
            selectProudctPopup();

            if (!string.IsNullOrEmpty(LotIDs))
            {
                Dictionary<string, object> param = SearchConditions;

                if (param.ContainsKey("P_PRODUCTDEFID"))
                    param["P_PRODUCTDEFID"] = popupProductId.GetValue().ToString();

                if (!string.IsNullOrEmpty(LotIDs))
                    param.Add("P_LOTID", LotIDs);

                string qryYPE = string.Empty;
                if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
                    qryYPE = "YPE";

                DataTable dt = SqlExecuter.Query("SelectYieldLOTListPopup" + qryYPE, "10001", param);

                if (dt.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData");
                }
                gridLeft.DataSource = dt;
                gridLeft.Refresh();
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
            gridLeft.View.ClearDatas();
            gridRight.View.ClearDatas();

            Dictionary<string, object> param = SearchConditions;

            if (param.ContainsKey("P_PRODUCTDEFID"))
                param["P_PRODUCTDEFID"] = popupProductId.GetValue().ToString();

            if (!string.IsNullOrEmpty(LotIDs))
            {
                if (param.ContainsKey("P_LOTID"))
                    param["P_LOTID"] = LotIDs;
                else
                    param.Add("P_LOTID", LotIDs);
            }

            string qryYPE = string.Empty;
            if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
                qryYPE = "YPE";

            DataTable dt = SqlExecuter.Query("SelectYieldLOTListPopup" + qryYPE, "10001", param);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData");
            }
            gridLeft.DataSource = dt;
            gridLeft.Refresh();
        }

        #endregion

        #region Private Function

        private void btnLotAnalysis_Click(object sender, EventArgs e)
        {
            //this.OpenMenu("PG-QC-0554", null);
            if (gridRight.View.RowCount == 0)
            {
                return;
            }
            else
            {
                // LotSelectEvent(grdAffectLot.View.GetCheckedRows());
                
                gridRight.View.CheckedAll();
                _checkTable = gridRight.View.GetCheckedRows();

                DataTable nTab = _checkTable.DefaultView.ToTable(true, new string[] { "PRODUCTDEFID", "PRODUCTDEFVERSION" });

                if (nTab.Rows.Count == 0)
                {
                    ShowMessage("NoSelectData"); // 선택된 데이터가 없습니다.

                    return;
                }
                else if (nTab.Rows.Count > 1)
                {
                        ShowMessage("NoMatchProductCodeVer"); // 동일하지 않은 품목코드와 버전으로 LOT 분석을 진행할 수 없습니다.

                        return;
                }
                else
                {
                    prodDefID = nTab.Rows[0]["PRODUCTDEFID"].ToString() + nTab.Rows[0]["PRODUCTDEFVERSION"].ToString();

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        #endregion

    }

}
