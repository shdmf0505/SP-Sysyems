#region using

using DevExpress.XtraCharts;

using System.Data;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 수율 화면에 사용되는 Chart/Sheet 기본 User Control
    /// 업  무  설  명  : Layer, 공정/설비, 작업조건(Recipe) 등 수율 화면에 표시되는 Chart/Sheet
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-10-04
    /// 필  수  처  리  :
    /// 
    /// </summary>
    public partial class ucRateDefault : DevExpress.XtraEditors.XtraUserControl
    {
        #region Local Variables

        #endregion

        #region 생성자

        public ucRateDefault()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        #endregion

        #region Event
        
        /// <summary>
        /// Event
        /// </summary>
        private void InitializeEvent(ViewType viewType)
        {
            if (viewType.Equals(ViewType.Line))
            {
                //! Lot Tab일 경우 Line Chart Point 클릭 이벤트
                chartMain.MouseClick += (s, e) =>
                {
                    if (e.Button.Equals(MouseButtons.Right))
                    {
                        return;
                    }

                    ChartHitInfo hi = chartMain.CalcHitInfo(e.Location);
                    grdMain.View.ActiveFilterString = DefectMapHelper.IsNull(hi.SeriesPoint) ?
                                                      "" : "[LOTID] = '" + hi.SeriesPoint.Argument + "'";
                };
            }

            //! Defect List Grid Merge 이벤트
            grdMain.View.CellMerge += (s, e) =>
            {
                if (!(s is DevExpress.XtraGrid.Views.Grid.GridView view))
                {
                    return;
                }

                if (e.Column.FieldName.Equals("DEFECTGROUP"))
                {
                    string str1 = view.GetRowCellDisplayText(e.RowHandle1, e.Column);
                    string str2 = view.GetRowCellDisplayText(e.RowHandle2, e.Column);
                    e.Merge = str1.Equals(str2);
                }
                else if (e.Column.FieldName.Equals("DEFECTGROUPCOUNT") || e.Column.FieldName.Equals("DEFECTGROUPRATE"))
                {
                    string str1 = view.GetRowCellDisplayText(e.RowHandle1, view.Columns["DEFECTGROUP"]);
                    string str2 = view.GetRowCellDisplayText(e.RowHandle2, view.Columns["DEFECTGROUP"]);
                    e.Merge = str1.Equals(str2);
                }
                else
                {
                    e.Merge = false;
                }

                e.Handled = true;
            };
        }

        #endregion

        #region Private Function

        #endregion

        #region Public Function

        /// <summary>
        /// 화면 설정
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dt"></param>
        /// <param name="type"></param>
        /// <param name="viewType"></param>
        public void SetData(string title, DataTable dt, EquipmentType type, ViewType viewType)
        {
            grbMain.Text = title;
            grdMain.LanguageKey = string.Concat(title, " List");

            if (viewType.Equals(ViewType.Line))
            {
                DefectMapHelper.SetGridColumnByRateLot(grdMain, type);
                DefectMapHelper.DrawingLineChartByAOI(chartMain, dt, "LOTID", "SUMRATEBYLOT");
            }
            else
            {
                DefectMapHelper.SetGridColumnByEquipmentType(grdMain, type);

                if (type.Equals(EquipmentType.EQUIPMENTTYPE_AOI))
                {
                    DefectMapHelper.DrawingSideBySideStackedBarChart(chartMain, dt, "DEFECTCODE", "DEFECTGROUP", "DEFECTCOUNT");
                    DefectMapHelper.AddSecondarySeriesToLineChart(chartMain, dt, "DEFECTGROUP", "DEFECTGROUPRATE");
                }
                else
                {
                    DefectMapHelper.DrawingBarChart(chartMain, dt, "DEFECTCODE", "DEFECTCOUNT");
                    DefectMapHelper.AddSecondarySeriesToLineChart(chartMain, dt, "DEFECTCODE", "DEFECTRATE");
                }
            }

            InitializeEvent(viewType);
            grdMain.DataSource = dt;
        }

        #endregion
    }
}
