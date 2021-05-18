#region using

using Micube.Framework;
using Micube.Framework.SmartControls;

using System.Data;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 단일 Lot Defect Map User Control
    /// 업  무  설  명  : 단일 Lot Defect Map 모듈 Control
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-05-01
    /// 필  수  처  리  :
    /// 수  정  이  력  : 
    /// 
    /// </summary>
    public partial class ucLotDefetMap : DevExpress.XtraEditors.XtraUserControl
    {
        #region Global Variable

        /// <summary>
        /// 팝업여부
        /// </summary>
        public bool IsPopupPresence = false;

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public ucLotDefetMap()
        {
            InitializeComponent();

            InitializeControl();
            InitializeLanguageKey();
            InitializeGrid();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// Grid 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdRawData.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            DefectMapHelper.SetGridColumnByRowData(grdRawData);

            grdRawData.View.SetIsReadOnly();
            grdRawData.View.PopulateColumns();
            grdRawData.View.BestFitColumns();

            grdRawData.ShowStatusBar = true;
            grdRawData.GridButtonItem = GridButtonItem.Export;
        }

        /// <summary>
        /// Controler 초기화s
        /// </summary>
        private void InitializeControl()
        {
            // Defect Map;
            tabDefectMap.Controls.Add(new ucDefectMapBasicForm()
            {
                Dock = DockStyle.Fill,
                IsPopupPresence = this.IsPopupPresence
            });

            // Comparison
            tabComparison.Controls.Add(new ucTypeComparison()
            {
                Dock = DockStyle.Fill
            });
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            tabDefectMap.Text = Language.Get("TABDEFECTMAP");
            tabComparison.Text = Language.Get("TABCOMPARISON");
            tabRawData.Text = Language.Get("TABROWDATA");
        }

        #endregion

        #region Private Function

        #endregion

        #region Public Function

        /// <summary>
        /// Equipment Type에 Main Tab의 Index로 이동한다.
        /// </summary>
        /// <param name="mode">Equipment Type</param>
        public void SetTabIndex() => tabMain.SelectedTabPageIndex = 0;

        /// <summary>
        /// 동작한다
        /// </summary>
        /// <param name="dt">Row Data</param>
        public void Run(DataTable dt, EquipmentType equipType = EquipmentType.EQUIPMENTTYPE_AOI)
        {
            ((ucDefectMapBasicForm)tabDefectMap.Controls[0]).SetData(dt, equipType);
            ((ucTypeComparison)tabComparison.Controls[0]).SetData(dt);
            grdRawData.DataSource = dt;
        }

        /// <summary>
        /// 초기화 한다
        /// </summary>
        public void Close()
        {
            ((ucDefectMapBasicForm)tabDefectMap.Controls[0]).SetClose();
            ((ucTypeComparison)tabComparison.Controls[0]).SetClose();
            grdRawData.DataSource = null;
        }

        #endregion
    }
}
