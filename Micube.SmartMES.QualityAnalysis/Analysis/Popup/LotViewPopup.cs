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
    /// 프 로 그 램 명  : 품질관리 > LOT 보기 Popup
    /// 업  무  설  명  : 
    /// 생    성    자  : 류시윤
    /// 생    성    일  : 2020-02-10
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class LotViewPopup : SmartPopupBaseForm
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
        public LotViewPopup()
        {
            InitializeComponent();

            InitializeEvent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        private void InitializeGrid()
        {
            grdLOTView.View.SetIsReadOnly();
            grdLOTView.View.OptionsBehavior.Editable = false;
            grdLOTView.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            var defaultCol = grdLOTView.View.AddGroupColumn("");

            // WORST 모델
            var worstModelCol = grdLOTView.View.AddGroupColumn("");
            // 품목코드
            worstModelCol.AddTextBoxColumn("PRODUCTDEFID", 100).SetTextAlignment(TextAlignment.Left);
            // 품목REV
            worstModelCol.AddTextBoxColumn("PRODUCTDEFVERSION", 70).SetTextAlignment(TextAlignment.Left);
            // 품목명
            worstModelCol.AddTextBoxColumn("PRODUCTDEFNAME", 100).SetTextAlignment(TextAlignment.Left);
            // LOT NO
            defaultCol.AddTextBoxColumn("LOTID", 170).SetTextAlignment(TextAlignment.Left).SetLabel("LOT No.");
            
            // 불량율 
            worstModelCol.AddTextBoxColumn("PCSDEFECTRATE", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric);
            // 불량수
            worstModelCol.AddTextBoxColumn("PCSDEFECTQTY", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);
            
            #region WORST

            for (int siteNo = 1; siteNo < 11; siteNo++)
            {
                var worstGroup = grdLOTView.View.AddGroupColumn("WORST" + siteNo);

                // 불량명
                worstGroup.AddTextBoxColumn("DEFECTNAME" + siteNo, 50)
                        .SetTextAlignment(TextAlignment.Left)
                        .SetLabel("DEFECTNAME");
                // 불량수
                worstGroup.AddTextBoxColumn("PCSDEFECTQTY" + siteNo, 50)
                    .SetTextAlignment(TextAlignment.Right)
                    .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                    .SetLabel("PCSDEFECTQTY");
                // 불량율 
                worstGroup.AddTextBoxColumn("PCSDEFECTRATE" + siteNo, 50)
                    .SetTextAlignment(TextAlignment.Right)
                    .SetDisplayFormat("##0.#0 %", MaskTypes.Numeric)
                    .SetLabel("PCSDEFECTRATE");
            }

            #endregion WORST

            var extGroup = grdLOTView.View.AddGroupColumn("Etc");

            // 불량명
            extGroup.AddTextBoxColumn("EXTDEFECTNAME", 50)
                    .SetTextAlignment(TextAlignment.Left)
                    .SetLabel("DEFECTNAME");
            // 불량수
            extGroup.AddTextBoxColumn("EXTPCSDEFECTQTY", 50)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetLabel("PCSDEFECTQTY");
            // 불량율 
            extGroup.AddTextBoxColumn("EXTPCSDEFECTRATE", 50)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("##0.#0 %", MaskTypes.Numeric)
                .SetLabel("PCSDEFECTRATE");

            grdLOTView.View.PopulateColumns();
            grdLOTView.View.BestFitColumns(true);
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
            this.Load += LotViewPopup_Load;

            this.btnLOTAnalysis.Click += BtnLOTAnalysis_Click;
        }

        private void BtnLOTAnalysis_Click(object sender, EventArgs e)
        {
            _checkTable = grdLOTView.View.GetCheckedRows();

            DataTable nTab = _checkTable.DefaultView.ToTable(true, new string[] { "LOTID" });

            if (nTab.Rows.Count == 0)
            {
                ShowMessage("NoSelectData"); // 선택된 데이터가 없습니다.

                return;
            }
            else
            {
                string strLOTs = string.Join(",", _checkTable.Rows.OfType<DataRow>().Select(r => r["LOTID"].ToString()));

                if (Conditions.ContainsKey("P_LOTS"))
                {
                    Conditions["P_LOTS"] = strLOTs;
                }
                else
                    Conditions.Add("P_LOTS", strLOTs);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
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
        private void LotViewPopup_Load(object sender, EventArgs e)
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
                {"CHECKDATE", Conditions["CHECKDATE"] },
                {"P_INSPTYPE", "" }
            };

            string qryYPE = string.Empty;
            if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
                qryYPE = "YPE";

            DataTable dt = SqlExecuter.Query("SelectYieldLOTView" + qryYPE, "10001", param);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData");
            }

            grdLOTView.DataSource = dt;
            grdLOTView.Refresh();
            grdLOTView.View.BestFitColumns(true);
        }

        #endregion

        #region Private Function

        #endregion
    }

}
