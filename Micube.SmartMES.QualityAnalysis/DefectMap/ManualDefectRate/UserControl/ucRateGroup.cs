#region using

using Micube.Framework;
using Micube.Framework.SmartControls;

using DevExpress.XtraCharts;

using System.Data;
using System.Linq;
using System.Collections.Generic;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 수율 화면 Group Control Root
    /// 업  무  설  명  : Layer, 공정/설비, 작업조건(Recipe) 등 수율 화면에 사용되는 Group Root
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-10-07
    /// 필  수  처  리  :
    /// 
    /// </summary>
    public partial class ucRateGroup : DevExpress.XtraEditors.XtraUserControl
    {
        #region Local Variables

        /// <summary>
        /// 생성자에서 받은 Orignal Data
        /// </summary>
        private DataTable _totalDataTable = default(DataTable);

        /// <summary>
        /// 부모단에 선택한 Group List
        /// </summary>
        private DataTable _groupList = default(DataTable);

        /// <summary>
        /// 검사공정 타입
        /// </summary>
        private EquipmentType _equipmentType = default(EquipmentType);

        /// <summary>
        /// View Type
        /// </summary>
        private SubViewType _subViewType = default(SubViewType);

        /// <summary>
        /// 수율 Type
        /// </summary>
        private RateGroupType _groupType = default(RateGroupType);

        #endregion

        #region 생성자

        public ucRateGroup()
        {
            InitializeComponent();

            InitializeLanguageKey();

            flowList.AutoScroll = true;
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
            //! 필터 버튼 클릭 이벤트
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

                DrawingSubList(filterDt);
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
                               .Select(x => new { x.Key.CODEID, x.Key.CODENAME })
                               .ForEach(x =>
                               {
                                   dt.Rows.Add(new object[] { x.CODEID, x.CODENAME });
                               });

                cboDefectSub.DataSource = dt;
                cboDefectSub.EditValue = "*";
            };
        }

        #endregion

        #region Private Function

        /// <summary>
        /// Flow List 구성하기
        /// </summary>
        /// <param name="dt"></param>
        private void DrawingSubList(DataTable dt)
        {
            flowList.Controls.Clear();

            #region root Control
            flowList.Controls.Add(DefectMapHelper.AddRateControl("Total", SetConversionData(dt), _equipmentType, ChartType));

            #endregion

            #region sub Control

            string title = string.Empty;
            string itemStr = string.Empty;

            _groupList.AsEnumerable()
                      .GroupBy(x => x.Field<string>("GROUP"))
                      .ForEach(item =>
                      {
                          List<string> list = new List<string>();
                          title = item.Key + " >> ";

                          foreach (DataRow dr in item)
                          {
                              itemStr = DefectMapHelper.StringByDataRowObejct(dr, "ITEM");
                              list.Add(itemStr);
                              title = title + DefectMapHelper.StringByDataRowObejct(dr, "NAME") + "-";
                          }

                          var GroupDt = dt.AsEnumerable()
                                          .Where(x => list.Contains(Format.GetString(x.Field<object>(Format.GetString(_groupType)))));

                          if (GroupDt.Count().Equals(0))
                          {
                              return;
                          }

                          flowList.Controls.Add(DefectMapHelper.AddRateControl(title.Substring(0, title.Length - 1), SetConversionData(GroupDt.CopyToDataTable()), _equipmentType, ChartType));
                      });

            #endregion
        }

        /// <summary>
        /// DataTable Analysis
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable SetConversionData(DataTable dt) => _subViewType.Equals(SubViewType.SUBVIEWTYPE_LOT) ?
                                                                DefectMapHelper.GetDefectAnalysisByLotRate(dt, _equipmentType) :
                                                                DefectMapHelper.GetDefectAnalysisByProductRate(dt, _equipmentType);

        /// <summary>
        /// View Type에 따라 Chart Type 설정하기
        /// </summary>
        private ViewType ChartType => _subViewType.Equals(SubViewType.SUBVIEWTYPE_PRODUCT) ? ViewType.StackedBar : ViewType.Line;

        #endregion

        #region Public Function

        /// <summary>
        /// Data 구성
        /// </summary>
        /// <param name="groupType"></param>
        /// <param name="dt"></param>
        /// <param name="groupList"></param>
        /// <param name="viewType"></param>
        /// <param name="type"></param>
        public void SetData(RateGroupType groupType, DataTable dt, DataTable groupList, SubViewType viewType, EquipmentType type)
        {
            _totalDataTable = dt;
            _groupList = groupList;
            _subViewType = viewType;
            _equipmentType = type;
            _groupType = groupType;

            layoutDefectGroup.Visibility = _equipmentType.Equals(EquipmentType.EQUIPMENTTYPE_AOI) ?
                                                    DevExpress.XtraLayout.Utils.LayoutVisibility.Always :
                                                    DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            InitializeCombo();
            InitializeEvent();
            DrawingSubList(dt);
        }

        #endregion
    }
}
