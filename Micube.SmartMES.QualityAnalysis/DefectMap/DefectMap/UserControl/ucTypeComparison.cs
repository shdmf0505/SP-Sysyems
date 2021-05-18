#region using

using Micube.Framework.SmartControls;

using System;
using System.Data;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : Type Comparison Diagram User Control
    /// 업  무  설  명  : AOI/BBT/Hole AOI 비교 Defect Map 표현을 위한 Control
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-07-12
    /// 수  정  이  력  : 
    /// 
    /// </summary>
    public partial class ucTypeComparison : DevExpress.XtraEditors.XtraUserControl
    {
        #region Global Variable

        /// <summary>
        /// 컨트롤의 속성관리를 위한 Class
        /// </summary>
        private class EquipTypeClass
        {
            /// <summary>
            ///  GroupBox Control
            /// </summary>
            public SmartGroupBox GroupBoxControl { get; set; }

            /// <summary>
            /// Grid Control
            /// </summary>
            public SmartBandedGrid GridControl { get; set; }

            /// <summary>
            /// GroupBox Control안에 들어가는 DataTable
            /// </summary>
            public DataTable GroupBoxDataTable { get; set; }

            /// <summary>
            /// Grid에 들어가는 DataTable
            /// </summary>
            public DataTable GridDataTable { get; set; }

            /// <summary>
            /// Data 초기화
            /// </summary>
            public void Close()
            {
                GridControl.DataSource = null;
                GridControl.View.ClearColumns();

                GroupBoxControl.Controls.Clear();

                GroupBoxDataTable = null;
                GridDataTable = null;
            }
        }

        /// <summary>
        /// Left에 들어가는 속성
        /// </summary>
        private EquipTypeClass leftClass = null;

        /// <summary>
        /// Right에 들어가는 속성
        /// </summary>
        private EquipTypeClass rightClass = null;

        /// <summary>
        /// 모든 DataTable
        /// </summary>
        private DataTable _mainDataTable = null;

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public ucTypeComparison()
        {
            InitializeComponent();

            InitializeControls();
            InitializeLanguageKey();
            InitializeEvent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// Controls 초기화
        /// </summary>
        private void InitializeControls()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CODEID", typeof(int));
            dt.Columns.Add("CODENAME", typeof(string));

            dt.Rows.Add(0, "AOI");
            dt.Rows.Add(1, "BBT");
            dt.Rows.Add(2, "HOLE");

            cboLeftType.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboLeftType.Editor.ShowHeader = false;
            cboLeftType.Editor.DisplayMember = "CODENAME";
            cboLeftType.Editor.ValueMember = "CODEID";
            cboLeftType.Editor.DataSource = dt;
            cboLeftType.Editor.ItemIndex = 0;

            cboRightType.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboRightType.Editor.ShowHeader = false;
            cboRightType.Editor.DisplayMember = "CODENAME";
            cboRightType.Editor.ValueMember = "CODEID";
            cboRightType.Editor.DataSource = dt;
            cboRightType.Editor.ItemIndex = 1;

            leftClass = new EquipTypeClass()
            {
                GroupBoxControl = grpLeft,
                GridControl = grdLeft,
                GroupBoxDataTable = new DataTable(),
                GridDataTable = new DataTable()
            };

            rightClass = new EquipTypeClass()
            {
                GroupBoxControl = grpRight,
                GridControl = grdRight,
                GroupBoxDataTable = new DataTable(),
                GridDataTable = new DataTable()
            };
        }

        /// <summary>
        /// Language 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            cboLeftType.LanguageKey = "LEFT";
            cboRightType.LanguageKey = "RIGHT";
            btnRun.LanguageKey = "APPLY";
            grdLeft.LanguageKey = "LEFT";
            grdRight.LanguageKey = "RIGHT";
            grpLeft.LanguageKey = "LEFT";
            grpRight.LanguageKey = "RIGHT";
        }

        #endregion

        #region Event

        /// <summary>
        /// Event 초기화
        /// </summary>
        private void InitializeEvent()
        {
            //! 비교버튼 클릭 이벤트
            btnRun.Click += (s, e) => SetDisplay();
        }

        #endregion

        #region Private Function

        /// <summary>
        /// Type Class 모듈 설정
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        private void SetModule(int value, EquipTypeClass type)
        {
            switch (value)
            {
                case (int)EquipmentType.EQUIPMENTTYPE_AOI:
                    //type.GroupBoxControl.LanguageKey = "TABAOI";
                    SetAOI(type);
                    break;
                case (int)EquipmentType.EQUIPMENTTYPE_BBT:
                    //type.GroupBoxControl.LanguageKey = "TABBBT";
                    SetBBT(type);
                    break;
                case (int)EquipmentType.EQUIPMENTTYPE_HOLE:
                    //type.GroupBoxControl.LanguageKey = "TABHOLE";
                    SetHole(type);
                    break;
            }
        }

        /// <summary>
        /// AOI 설정
        /// </summary>
        /// <param name="type"></param>
        private void SetAOI(EquipTypeClass type)
        {
            //type.GridControl.LanguageKey = "TABAOI";

            DefectMapHelper.SetGridColumnByEquipmentType(type.GridControl, EquipmentType.EQUIPMENTTYPE_AOI);

            type.GroupBoxDataTable = DefectMapHelper.GetDefectDataOfEquipmentType(_mainDataTable, EquipmentType.EQUIPMENTTYPE_AOI);

            if (DefectMapHelper.IsNull(type.GroupBoxDataTable))
            {
                return;
            }

            type.GridControl.DataSource = type.GridDataTable = DefectMapHelper.GetDefectAnalysisByAOI(type.GroupBoxDataTable);

            type.GroupBoxControl.Controls.Add(DefectMapHelper.DrawingAOIDiagramByMain(type.GroupBoxDataTable));
        }

        /// <summary>
        /// BBT 설정
        /// </summary>
        /// <param name="type"></param>
        private void SetBBT(EquipTypeClass type)
        {
            //type.GridControl.LanguageKey = "TABBBT";

            DefectMapHelper.SetGridColumnByEquipmentType(type.GridControl, EquipmentType.EQUIPMENTTYPE_BBT);

            type.GroupBoxDataTable = DefectMapHelper.GetDefectDataOfEquipmentType(_mainDataTable, EquipmentType.EQUIPMENTTYPE_BBT);

            if (DefectMapHelper.IsNull(type.GroupBoxDataTable))
            {
                return;
            }

            type.GridControl.DataSource = type.GridDataTable = DefectMapHelper.GetDefectAnalysisByBBT(type.GroupBoxDataTable);

            type.GroupBoxControl.Controls.Add(DefectMapHelper.DrawingBBTDiagramByMain(type.GroupBoxDataTable));
        }

        /// <summary>
        /// Hole 설정
        /// </summary>
        /// <param name="type"></param>
        private void SetHole(EquipTypeClass type)
        {
            //type.GridControl.LanguageKey = "TABHOLE";

            DefectMapHelper.SetGridColumnByEquipmentType(type.GridControl, EquipmentType.EQUIPMENTTYPE_HOLE);

            type.GroupBoxDataTable = DefectMapHelper.GetDefectDataOfEquipmentType(_mainDataTable, EquipmentType.EQUIPMENTTYPE_HOLE);

            if (DefectMapHelper.IsNull(type.GroupBoxDataTable))
            {
                return;
            }

            type.GridControl.DataSource = type.GridDataTable = DefectMapHelper.GetDefectAnalysisByHOLE(type.GroupBoxDataTable);

            type.GroupBoxControl.Controls.Add(DefectMapHelper.DrawingAOIDiagramByMain(type.GroupBoxDataTable));
        }

        /// <summary>
        /// 화면 설정
        /// </summary>
        private void SetDisplay()
        {
            SetClose();
            SetModule(cboLeftType.GetValue().ToSafeInt16(), leftClass);
            SetModule(cboRightType.GetValue().ToSafeInt16(), rightClass);
            DefectMapHelper.DrawingBarChartByComparison(chartMain, leftClass.GridDataTable, rightClass.GridDataTable,
                                                        cboLeftType.Editor.GetDisplayText(), cboRightType.Editor.GetDisplayText());
        }

        #endregion

        #region Public Function

        /// <summary>
        /// Row Data로 화면 설정한다
        /// </summary>
        /// <param name="dt">Row Data</param>
        public void SetData(DataTable dt)
        {
            SetClose();

            _mainDataTable = dt;

            SetDisplay();
        }

        /// <summary>
        /// 화면 초기화
        /// </summary>
        public void SetClose()
        {
            leftClass.Close();
            rightClass.Close();
            chartMain.Series.Clear();
        }

        #endregion
    }
}
