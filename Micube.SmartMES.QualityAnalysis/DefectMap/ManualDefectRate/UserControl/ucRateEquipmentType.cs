#region using

using Micube.Framework;
using Micube.Framework.SmartControls;

using DevExpress.XtraCharts;

using System.Data;
using System.Linq;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질 관리 > Defect Map > 검사공정 최종 불량 현황에 표시되는 Sub Control
    /// 업  무  설  명  : Aoi, BBT, Hole에 따른 불량률 필터가들어간 RateControl
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-10-07
    /// 필  수  처  리  :
    /// 
    /// </summary>
    public partial class ucRateEquipmentType : DevExpress.XtraEditors.XtraUserControl
    {
        #region Local Variables

        /// <summary>
        /// 생성자에서 받은 Orignal Data
        /// </summary>
        private DataTable _totalDataTable;

        /// <summary>
        /// 검사공정 타입
        /// </summary>
        private EquipmentType _equipmentType;

        /// <summary>
        /// View Type
        /// </summary>
        private SubViewType _subViewType;

        #endregion

        #region 생성자

        public ucRateEquipmentType()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 생성자
        /// 생성자
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dt"></param>
        /// <param name="type"></param>
        /// <param name="subViewType"></param>
        public ucRateEquipmentType(string title, DataTable dt, EquipmentType type, SubViewType subViewType)
        {
            InitializeComponent();

            _totalDataTable = dt;
            _equipmentType = type;
            _subViewType = subViewType;

            grbMain.Text = Language.Get(title);
            grdMain.Caption = Language.Get(title);
            //grbMain.LanguageKey = Language.Get(title);
            //grdMain.LanguageKey = Language.Get(title);

            layoutDefectGroup.Visibility = _equipmentType.Equals(EquipmentType.EQUIPMENTTYPE_AOI) ?
                                           DevExpress.XtraLayout.Utils.LayoutVisibility.Always :
                                           DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            InitializeLanguageKey();
            InitializeCombo();
            InitializeEvent();

            DrawingDisplay(_totalDataTable);
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            layoutDefectGroup.Text = Language.Get("DEFECTGROUP");
            layoutDefectSub.Text = Language.Get("DEFECTCODE");
            btnFilter.Text = Language.Get("FILTER");

            //layoutSheet.SetLanguageKey(layoutDefectGroup, "DEFECTGROUP");
            //layoutSheet.SetLanguageKey(layoutDefectSub, "DEFECTCODE");
            //btnFilter.LanguageKey = "FILTER";
        }

        /// <summary>
        /// Combo Box 설정
        /// </summary>
        private void InitializeCombo()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CODEID", typeof(string));
            dt.Columns.Add("CODENAME", typeof(string));

            if (_equipmentType.Equals(EquipmentType.EQUIPMENTTYPE_AOI))
            {
                #region Defect Group Combo

                cboDefectGroup.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
                cboDefectGroup.ShowHeader = false;
                cboDefectGroup.DisplayMember = "CODENAME";
                cboDefectGroup.ValueMember = "CODEID";
                cboDefectGroup.EmptyItemCaption = Language.Get("ALLVIEWS");
                cboDefectGroup.EmptyItemValue = "*";
                cboDefectGroup.UseEmptyItem = true;

                //! Real Defect Group Code List
                _totalDataTable.AsEnumerable()
                               .GroupBy(x => new { CODEID = x.Field<string>("GROUPCODE"), CODENAME = x.Field<string>("GROUPNAME") })
                               .OrderBy(x => x.Key.CODEID)
                               .Select(x => new { x.Key.CODEID, x.Key.CODENAME })
                               .ForEach(x =>
                               {
                                   dt.Rows.Add(new object[] { x.CODEID, x.CODENAME });
                               });

                cboDefectGroup.DataSource = dt;
                cboDefectGroup.EditValue = "*";

                #endregion

                #region Defect Sub Combo

                DataTable subDt = dt.Clone();
                subDt.Rows.Clear();

                cboDefectSub.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
                cboDefectSub.ShowHeader = false;
                cboDefectSub.DisplayMember = "CODENAME";
                cboDefectSub.ValueMember = "CODEID";
                cboDefectSub.UseEmptyItem = true;
                cboDefectSub.EmptyItemCaption = Language.Get("ALLVIEWS");
                cboDefectSub.EmptyItemValue = "*";
                cboDefectSub.DataSource = subDt;
                cboDefectSub.EditValue = "*";

                #endregion
            }
            else
            {
                #region Defect Sub Combo

                cboDefectSub.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
                cboDefectSub.ShowHeader = false;
                cboDefectSub.DisplayMember = "CODENAME";
                cboDefectSub.ValueMember = "CODEID";
                cboDefectSub.UseEmptyItem = true;
                cboDefectSub.EmptyItemCaption = Language.Get("ALLVIEWS");
                cboDefectSub.EmptyItemValue = "*";

                _totalDataTable.AsEnumerable()
                               .GroupBy(x => new { CODEID = x.Field<string>("SUBCODE"), CODENAME = x.Field<string>("SUBNAME") })
                               .OrderBy(x => x.Key.CODEID)
                               .Select(x => new { x.Key.CODEID, x.Key.CODENAME })
                               .ForEach(x =>
                               {
                                   dt.Rows.Add(new object[] { x.CODEID, x.CODENAME });
                               });

                cboDefectSub.DataSource = dt;
                cboDefectSub.EditValue = "*";

                #endregion
            }
        }

        #endregion

        #region Event

        /// <summary>
        /// Event
        /// </summary>
        private void InitializeEvent()
        {
            //! Lot Tab일 경우 Line Chart Point 클릭 이벤트
            chartMain.MouseClick += (s, e) =>
            {
                if (e.Button.Equals(MouseButtons.Right))
                {
                    return;
                }

                //! View Type이 Lot인 경우에만 이벤트 생성
                if (_subViewType.Equals(SubViewType.SUBVIEWTYPE_PRODUCT))
                {
                    return;
                }

                ChartHitInfo hi = chartMain.CalcHitInfo(e.Location);
                grdMain.View.ActiveFilterString = DefectMapHelper.IsNull(hi.SeriesPoint) ? string.Empty :  "[LOTID] = '" + hi.SeriesPoint.Argument + "'";
            };

            //! Filter 버튼 클릭 이벤트
            btnFilter.Click += (s, e) =>
            {
                if (DefectMapHelper.IsNull(_totalDataTable))
                {
                    return;
                }

                DataTable filterDt;

                if (_equipmentType.Equals(EquipmentType.EQUIPMENTTYPE_AOI))
                {
                    filterDt = cboDefectGroup.EditValue.Equals("*") ?
                                    _totalDataTable :
                                    cboDefectSub.EditValue.Equals("*") ?
                                        _totalDataTable.AsEnumerable()
                                                       .Where(x => cboDefectGroup.GetValuesByList().Contains(x.Field<string>("GROUPCODE")))
                                                       .CopyToDataTable() :
                                        _totalDataTable.AsEnumerable()
                                                       .Where(x => cboDefectGroup.GetValuesByList().Contains(x.Field<string>("GROUPCODE"))
                                                                && cboDefectSub.GetValuesByList().Contains(x.Field<string>("SUBCODE")))
                                                       .CopyToDataTable();
                }
                else
                {
                    filterDt = cboDefectSub.EditValue.Equals("*") ?
                               _totalDataTable :
                               _totalDataTable.AsEnumerable()
                                               .Where(x => cboDefectSub.GetValuesByList().Contains(x.Field<string>("SUBCODE")))
                                               .CopyToDataTable();
                }

                if (filterDt.Rows.Count.Equals(0))
                {
                    MSGBox.Show(MessageBoxType.Information, "NoSelectData");
                    return;
                }

                DrawingDisplay(filterDt);
            };

            //! Combo Defect Group 값 변경시 이벤트
            cboDefectGroup.EditValueChanged += (s, e) =>
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("CODEID", typeof(string));
                dt.Columns.Add("CODENAME", typeof(string));

                _totalDataTable.AsEnumerable()
                               .Where(x => cboDefectGroup.GetValuesByList().Contains(x.Field<string>("GROUPCODE")))
                               .GroupBy(x => new { CODEID = x.Field<string>("SUBCODE"), CODENAME = x.Field<string>("SUBNAME") })
                               .OrderBy(x => x.Key.CODEID)
                               .Select(x => new { x.Key.CODEID, x.Key.CODENAME})
                               .ForEach(x =>
                               {
                                   dt.Rows.Add(new object[] { x.CODEID, x.CODENAME });
                               });

                cboDefectSub.DataSource = dt;
                cboDefectSub.EditValue = "*";
            };
            
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

        /// <summary>
        /// 화면 그리기
        /// </summary>
        /// <param name="dt"></param>
        private void DrawingDisplay(DataTable dt)
        {
            chartMain.ClearSeries();
            grdMain.View.ClearColumns();
            grdMain.View.ClearDatas();

            DataTable inspDt;

            if (_subViewType.Equals(SubViewType.SUBVIEWTYPE_LOT))
            {
                inspDt = DefectMapHelper.GetDefectAnalysisByLotRate(dt, _equipmentType);
                DefectMapHelper.SetGridColumnByRateLot(grdMain, _equipmentType);
                DefectMapHelper.DrawingLineChartByAOI(chartMain, inspDt, "LOTID", "SUMRATEBYLOT");
            }
            else
            {
                inspDt = DefectMapHelper.GetDefectAnalysisByProductRate(dt, _equipmentType);
                DefectMapHelper.SetGridColumnByEquipmentType(grdMain, _equipmentType);

                if (_equipmentType.Equals(EquipmentType.EQUIPMENTTYPE_AOI))
                {
                    DefectMapHelper.DrawingSideBySideStackedBarChart(chartMain, inspDt, "DEFECTCODE", "DEFECTGROUP", "DEFECTCOUNT");
                    DefectMapHelper.AddSecondarySeriesToLineChart(chartMain, inspDt, "DEFECTGROUP", "DEFECTGROUPRATE");
                }
                else
                {
                    DefectMapHelper.DrawingBarChart(chartMain, inspDt, "DEFECTCODE", "DEFECTCOUNT");
                    DefectMapHelper.AddSecondarySeriesToLineChart(chartMain, inspDt, "DEFECTCODE", "DEFECTRATE");
                }
            }

            grdMain.DataSource = inspDt;
        }

        #endregion
    }
}
