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
    /// 프 로 그 램 명  : 품질관리 > AffectLot 추가 Popup
    /// 업  무  설  명  : 
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-05-31
    /// 수  정  이  력  : 2019-08-08 강유라
    /// 
    /// 
    /// </summary>
    public partial class AffectLotPopup : SmartPopupBaseForm
    {
        #region Global Variable

        /// <summary>
        /// 부모 그리드로 데이터를 바인딩 시켜줄 델리게이트
        /// </summary>
        /// <param name="dt"></param>
        public delegate void AffectLotSelectionDataHandler(DataTable dt);
        public event AffectLotSelectionDataHandler AffectLotSelectEvent;
        public DataTable _checkTable;
        public DataRow CurrentDataRow { get; set; }

        #endregion

        #region Local Variables

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public AffectLotPopup()
        {
            InitializeComponent();

            InitializeEvent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        private void InitializeGrid()
        {
            grdAffectLot.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            //grdAffectLot.View.SetIsReadOnly();

            var standard = grdAffectLot.View.AddGroupColumn("STANDARD");
            standard.AddTextBoxColumn("PRODUCTDEFID", 150);
            standard.AddTextBoxColumn("PRODUCTDEFNAME", 250);
            standard.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            standard.AddTextBoxColumn("LOTID", 200)
                .SetTextAlignment(TextAlignment.Center);
            standard.AddTextBoxColumn("PANELQTY", 80)
                .SetTextAlignment(TextAlignment.Right);
            standard.AddTextBoxColumn("PCSQTY", 80)
                .SetTextAlignment(TextAlignment.Right);

            var location = grdAffectLot.View.AddGroupColumn("LOCATION");
            location.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetIsHidden();
            location.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            location.AddTextBoxColumn("AREAID", 150)
                .SetIsHidden();
            location.AddTextBoxColumn("AREANAME", 150);
            location.AddTextBoxColumn("ISLOTLOCKING", 150)
                .SetTextAlignment(TextAlignment.Center);

            grdAffectLot.View.PopulateColumns();
        }

        #endregion

        #region Popup

        /// <summary>
        /// 품목 검색팝업
        /// </summary>
        private void selectProudctPopup()
        {
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();

            popup.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("GRIDPRODUCTLIST", PopupButtonStyles.Ok_Cancel, true, false);
            popup.Id = "PRODUCT";
            popup.SearchQuery = new SqlQuery("GetProductListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "PRODUCTDEFNAME";
            popup.ValueFieldName = "PRODUCTDEFID";
            popup.LanguageKey = "PRODUCT";

            popup.Conditions.AddTextBox("PRODUCTDEFIDNAME");

            popup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            popup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 150);
            popup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);

            popupProductId.SelectPopupCondition = popup;
            popupProductId.SelectPopupCondition = popup;
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += ChemicalissuePopup_Load;

            btnAffectLotCalc.Click += BtnAffectLotCalc_Click;
            btnSearch.Click += BtnSearch_Click;
            btnClose.Click += BtnClose_Click;
            btnApply.Click += BtnApply_Click;
        }

        /// <summary>
        /// Affect Lot 산정(출)연계
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAffectLotCalc_Click(object sender, EventArgs e)
        {
            
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
            if (grdAffectLot.View.RowCount == 0)
            {
                return;
            }
            else
            {
                // AffectLotSelectEvent(grdAffectLot.View.GetCheckedRows());
                this.DialogResult = DialogResult.OK;
                _checkTable = grdAffectLot.View.GetCheckedRows();
                this.Close();
            }
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
        private void ChemicalissuePopup_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            InitializeGrid();

            selectProudctPopup();
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
                {"PRODUCTDEFID", popupProductId.GetValue().ToString()},
                {"LOTID", txtLotId.EditValue.ToString()},
                {"LANGUAGETYPE" , Framework.UserInfo.Current.LanguageType},
                {"PLANTID" , Framework.UserInfo.Current.Plant},
                {"ENTERPRISEID" , Framework.UserInfo.Current.Enterprise},
                {"ABNOCRNO" , CurrentDataRow["ABNOCRNO"]},
                {"ABNOCRTYPE" , CurrentDataRow["ABNOCRTYPE"]}
            };

            DataTable dt = SqlExecuter.Query("SelectLotToAddAffectLot", "10001", param);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData");
            }
            grdAffectLot.DataSource = dt;
            grdAffectLot.Refresh();
        }

        #endregion

        #region Private Function

        #endregion
    }

}
