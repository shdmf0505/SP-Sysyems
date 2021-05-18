#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using System.Data;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 출하검사 > 평탄도 측정값 상세 팝업
    /// 업  무  설  명  : 평탄도 설비 인퍼페이스 측정값 Popup 
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2020-02-28
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class popupFlatnessMeasure : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Interface

        /// <summary>
        /// 부모의 선택된 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }

        #endregion

        #region Local Variables

        #endregion

        #region 생성자

        public popupFlatnessMeasure()
        {
            InitializeComponent();

            InitializeContent();
            InitializeLanguageKey();
            InitializeEvent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// Controler 초기화
        /// </summary>
        private void InitializeContent()
        {
            this.CancelButton = btnOk;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            txtLot.ReadOnly = true;
            txtProduct.ReadOnly = true;
            txtProductName.ReadOnly = true;
            txtProductVersion.ReadOnly = true;
            txtSite.ReadOnly = true;
            txtArea.ReadOnly = true;
            txtFactory.ReadOnly = true;
            txtEquipment.ReadOnly = true;
            txtProcess.ReadOnly = true;
            txtSpecRange.ReadOnly = true;
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            this.LanguageKey = "MEASURVALUE";
            grpInfo.LanguageKey = "DEFAULTINFO";
            grdMain.LanguageKey = "MEASUREDVALUE";
            btnOk.LanguageKey = "OK";

            layoutInfo.SetLanguageKey(layoutLot, "LOTID");
            layoutInfo.SetLanguageKey(layoutProduct, "PRODUCTDEFID");
            layoutInfo.SetLanguageKey(layoutProductName, "PRODUCTDEFNAME");
            layoutInfo.SetLanguageKey(layoutProductVersion, "PRODUCTDEFVERSION");
            layoutInfo.SetLanguageKey(layoutSite, "SITE");
            layoutInfo.SetLanguageKey(layoutFactory, "FACTORY");
            layoutInfo.SetLanguageKey(layoutProcess, "STANDARDOPERATIONID");
            layoutInfo.SetLanguageKey(layoutArea, "AREAID");
            layoutInfo.SetLanguageKey(layoutEquipment, "EQUIPMENTUNIT");
            layoutInfo.SetLanguageKey(layoutSpecRange, "SPECRANGE");
            layoutInfo.SetLanguageKey(layoutdegree, "DEGREE");
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid(int maxPoint)
        {
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdMain.View.AddTextBoxColumn("DEGREE", 10).SetIsHidden();
            grdMain.View.AddTextBoxColumn("PIECEID", 20).SetTextAlignment(TextAlignment.Right);

            for (int i = 0; i < maxPoint; i++)
            {
                grdMain.View.AddTextBoxColumn(string.Concat("POINT_", (i + 1)), 60)
                            .SetLabel(Format.GetString((i + 1)))
                            .SetTextAlignment(TextAlignment.Right);

                grdMain.View.AddTextBoxColumn(string.Concat("RESULT_", (i + 1)), 10).SetIsHidden();
            }

            grdMain.View.PopulateColumns();
            grdMain.View.SetIsReadOnly();
            grdMain.View.BestFitColumns();

            grdMain.GridButtonItem = GridButtonItem.Export;
            grdMain.View.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            grdMain.View.ActiveFilterString = string.Concat("[DEGREE] = ''");
        }

        #endregion

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // 화면 로드 이벤트
            this.Load += (s, e) =>
            {
                txtLot.Text = DefectMapHelper.StringByDataRowObejct(CurrentDataRow, "LOTID");
                txtProduct.Text = DefectMapHelper.StringByDataRowObejct(CurrentDataRow, "PRODUCTDEFID");
                txtProductName.Text = DefectMapHelper.StringByDataRowObejct(CurrentDataRow, "PRODUCTDEFNAME");
                txtProductVersion.Text = DefectMapHelper.StringByDataRowObejct(CurrentDataRow, "PRODUCTDEFVERSION");
                txtSite.Text = DefectMapHelper.StringByDataRowObejct(CurrentDataRow, "PLANTID");
                txtFactory.Text = DefectMapHelper.StringByDataRowObejct(CurrentDataRow, "FACTORYNAME");
                txtProcess.Text = DefectMapHelper.StringByDataRowObejct(CurrentDataRow, "PROCESSSEGMENTNAME");
                txtArea.Text = DefectMapHelper.StringByDataRowObejct(CurrentDataRow, "AREANAME");
                txtSpecRange.Text = DefectMapHelper.StringByDataRowObejct(CurrentDataRow, "SPECRANGE");
                txtEquipment.Text = DefectMapHelper.StringByDataRowObejct(CurrentDataRow, "EQUIPMENTUNIT");

                GetMeasureValueList(CurrentDataRow);
                DefectMapHelper.SetDegree(cboDegree, DefectMapHelper.StringByDataRowObejct(CurrentDataRow, "DEGREE"));
            };

            // 차수 변경 이벤트
            cboDegree.EditValueChanged += (s, e) => grdMain.View.ActiveFilterString = string.Concat("[DEGREE] = '", cboDegree.Text, "'");

            // NG 발생시 색상변경
            grdMain.View.RowCellStyle += (s, e) =>
            {
                if(!e.Column.FieldName.Substring(0, 5).Equals("POINT"))
                {
                    return;
                }

                string pointId = e.Column.FieldName.Substring(6, e.Column.FieldName.Length - 6);

                if(Format.GetString(grdMain.View.GetRowCellValue(e.RowHandle, string.Concat("RESULT_", pointId))) is string result)
                {
                    e.Appearance.BackColor = result.Equals("OK") ? e.Appearance.BackColor : Color.Red;
                    e.Appearance.ForeColor = result.Equals("OK") ? e.Appearance.ForeColor : Color.White;
                }
            };
        }

        #endregion

        #region Private Function

        private void GetMeasureValueList(DataRow dr)
        {
            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                {"P_SPECCLASSID", "SAVE_MEASURE_FLATNESS" },
                {"P_LOTID", dr["LOTID"] },
                {"P_PRODUCTDEFID", dr["PRODUCTDEFID"] },
                {"P_PRODUCTDEFVERSION", dr["PRODUCTDEFVERSION"] },
                {"P_PROCESSSEGMENTID", dr["PROCESSSEGMENTID"] },
                {"P_AREAID", dr["AREAID"] },
                {"P_EQUIPMENTID", dr["EQUIPMENTID"] },
                {"P_PLANTID", dr["PLANTID"] },
                {"P_DAITEMID", "Z" }
            };

            if (SqlExecuter.Query("GetFlatnessMeasureByLot", "10001", param) is DataTable dt)
            {
                if (dt.AsEnumerable()
                      .Where(x => x.Field<decimal>("DEGREE") == Format.GetDecimal(dr["DEGREE"]))
                      .Max(x => Format.GetInteger(x.Field<string>("DAPOINTID"))) is int maxPoint)
                {
                    InitializeGrid(maxPoint);

                    DataTable cloneDt = (grdMain.DataSource as DataTable).Clone();
                    DataRow newRow = cloneDt.NewRow();
                    int defaultNo = 0;

                    foreach (DataRow cloneDr in dt.Rows)
                    {
                        if (DefectMapHelper.IntByDataRowObject(cloneDr, "DAPOINTID") > defaultNo)
                        {
                            newRow["DEGREE"] = DefectMapHelper.StringByDataRowObejct(cloneDr, "DEGREE");
                            newRow["PIECEID"] = DefectMapHelper.StringByDataRowObejct(cloneDr, "PIECEID");
                            defaultNo = DefectMapHelper.IntByDataRowObject(cloneDr, "DAPOINTID");
                            newRow[string.Concat("POINT_", defaultNo)] = DefectMapHelper.StringByDataRowObejct(cloneDr, "VALUE");
                            newRow[string.Concat("RESULT_", defaultNo)] = DefectMapHelper.StringByDataRowObejct(cloneDr, "RESULT");
                        }
                        else
                        {
                            cloneDt.Rows.Add(newRow);
                            newRow = cloneDt.NewRow();
                            newRow["DEGREE"] = DefectMapHelper.StringByDataRowObejct(cloneDr, "DEGREE");
                            newRow["PIECEID"] = DefectMapHelper.StringByDataRowObejct(cloneDr, "PIECEID");
                            defaultNo = DefectMapHelper.IntByDataRowObject(cloneDr, "DAPOINTID");
                            newRow[string.Concat("POINT_", defaultNo)] = DefectMapHelper.StringByDataRowObejct(cloneDr, "VALUE");
                            newRow[string.Concat("RESULT_", defaultNo)] = DefectMapHelper.StringByDataRowObejct(cloneDr, "RESULT");
                        }
                    }

                    cloneDt.Rows.Add(newRow);
                    grdMain.DataSource = cloneDt;
                };
            }
        }

        #endregion
    }
}