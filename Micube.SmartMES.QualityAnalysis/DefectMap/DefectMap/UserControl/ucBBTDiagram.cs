#region using

using Micube.Framework;

using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

using System;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// 프 로 그 램 명  : BBT Diagram User Control
    /// 업  무  설  명  : BBT Defect Map 표현을 위한 Control
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-05-01
    /// 필  수  처  리  :
    /// 수  정  이  력  : 
    /// 
    public partial class ucBBTDiagram : DevExpress.XtraEditors.XtraUserControl
    {
        #region Global Variable

        /// <summary>
        /// 품목 별 Defect Map 조회시 Nail View가 Lot일 경우 팝업 이벤트 Handler
        /// </summary>
        public delegate void MapDetailsEventHandler();

        /// <summary>
        /// 품목 별 Defect Map 조회시 Nail View가 Lot일 경우 팝업 이벤트
        /// </summary>
        public event MapDetailsEventHandler MapDetailsEvent;

        /// <summary>
        /// BBT Defect Code/Name 리스트
        /// </summary>
        private DataTable _bbtDefectState;

        /// <summary>
        /// Main Grid ToolTip Control
        /// </summary>
        private ToolTipController _toolTipControl;

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="diagramMode">BBT Daigram Type</param>
        /// <param name="title">Nail View일 경우 Title</param>
        /// <param name="value">BBT Defect Code/Name List [기준 정보]</param>
        public ucBBTDiagram(BBTMode diagramMode, string title = "", DataTable value = null)
        {
            InitializeComponent();

            InitializeControls(diagramMode, title, value);
            InitializeEvent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// Controls 초기화
        /// </summary>
        /// <param name="diagramMode">BBT View Mode</param>
        /// <param name="title">타이틀</param>
        /// <param name="values">BBT Defect Code/Name 리스트</param>
        private void InitializeControls(BBTMode diagramMode, string title, DataTable values)
        {
            if (diagramMode.Equals(BBTMode.BBTMODE_MAIN))
            {
                grdViewMain.Appearance.Row.Font = new Font("Arial", 8);
                grdViewMain.RowCellStyle += GrdViewMain_RowCellStyle;
                grdViewMain.CalcRowHeight += (s, e) =>
                {
                    int size = (grdControlMain.Height / ((DataTable)grdControlMain.DataSource).Rows.Count);
                    e.RowHeight = size % 2 == 0 ? size : size - 1;
                };
            }
            else if (diagramMode.Equals(BBTMode.BBTMODE_NAIL))
            {
                _bbtDefectState = values;
                chkMain.Text = title;
                layoutControlItemCheckBox.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                grdViewMain.Appearance.Row.Font = new Font("Arial", 1);

                chkMain.CheckStateChanged += CheckEditMain_CheckStateChanged;
                grdControlMain.MouseClick += (s, e) =>
                {
                    if (e.Button.Equals(MouseButtons.Left))
                    {
                        chkMain.Checked = chkMain.CheckState.Equals(CheckState.Checked) ? false : true;
                    }
                };

                grdViewMain.RowCellStyle += (s, e) =>
                {
                    e.Appearance.ForeColor = Color.White;

                    if (string.IsNullOrEmpty(Format.GetString(e.CellValue)))
                    {
                        return;
                    }

                    string colorStr = _bbtDefectState.Rows.Count < 1
                                            ? "#000000"
                                            : _bbtDefectState.AsEnumerable()
                                                             .Where(x => x.Field<string>("DEFECTCODE") == Format.GetString(e.CellValue))
                                                             .FirstOrDefault()["COLOR"].ToString();

                    e.Appearance.BackColor = ColorTranslator.FromHtml(colorStr);
                };
            }
            else if (diagramMode.Equals(BBTMode.BBTMODE_LOTNAIL))
            {
                chkMain.Text = title;
                layoutControlItemCheckBox.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                grdViewMain.Appearance.Row.Font = new Font("Arial", 5);
                grdViewMain.RowCellStyle += GrdViewMain_RowCellStyle;
                chkMain.CheckStateChanged += CheckEditMain_CheckStateChanged;
                grdControlMain.MouseClick += (s, e) =>
                {
                    if (e.Button.Equals(MouseButtons.Left))
                    {
                        chkMain.Checked = chkMain.CheckState.Equals(CheckState.Checked) ? false : true;
                    }
                };
            }

            grdViewMain.BestFitColumns();
            grdViewMain.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never;
            grdViewMain.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never;
            grdViewMain.OptionsView.ShowColumnHeaders = false;
            grdViewMain.OptionsView.ShowGroupPanel = false;
            grdViewMain.OptionsBehavior.Editable = false;
            grdViewMain.OptionsSelection.EnableAppearanceFocusedRow = false;
            grdViewMain.OptionsSelection.EnableAppearanceFocusedCell = false;
            grdViewMain.OptionsView.ShowIndicator = false;
        }

        #endregion

        #region Event

        /// <summary>
        /// 좌표별 defect 수에 따른 bgcolor 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdViewMain_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            e.Appearance.ForeColor = Color.White;

            if (!int.TryParse(Format.GetString(e.CellValue), out int defectCnt))
            {
                return;
            }

            if (defectCnt >= 1 && defectCnt <= 5)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml("#E7B9C0");
            }
            else if (defectCnt >= 6 && defectCnt <= 10)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml("#CF7C89");
            }
            else if (defectCnt >= 11 && defectCnt <= 15)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml("#B84A5B");
            }
            else if (defectCnt >= 16 && defectCnt <= 20)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml("#660010");
            }
            else if (defectCnt == 0)
            {
                e.Appearance.BackColor = Color.White;
            }
            else
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml("#44000A");
            }
        }

        /// <summary>        
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            grdViewMain.RowCellDefaultAlignment += (s, e) => e.HorzAlignment = HorzAlignment.Center;
        }

        /// <summary>
        /// 체크박스 선택시 배경이미지 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckEditMain_CheckStateChanged(object sender, EventArgs e)
            => chkMain.BackColor = chkMain.CheckState.Equals(CheckState.Checked) ?
                    chkMain.BackColor = Color.Gold :
                    chkMain.BackColor = ColorTranslator.FromHtml("#F0F0F0");

        #endregion

        #region Private Function

        /// <summary>
        /// 팝업 발생 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailsView(object sender, EventArgs e) => MapDetailsEvent();

        #endregion

        #region Public Function

        /// <summary>
        /// 품목별로 Nail Map이 Lot별로 나올 경우 ContextMenu를 만들어준다.
        /// </summary>
        public void SetContextMenu()
        {
            grdControlMain.ContextMenu = new ContextMenu(new MenuItem[]
            {
                new MenuItem("Details View", new EventHandler(DetailsView))
            });
        }

        /// <summary>
        /// Main Defect Map의 경우 ToolTip 적용
        /// </summary>
        /// <param name="tooltip"></param>
        public void SetToolTip(DataTable tooltip)
        {
            _toolTipControl = new ToolTipController();
            _toolTipControl.GetActiveObjectInfo += (s, e) =>
            {
                if (DefectMapHelper.IsNull(e.Info) && e.SelectedControl.Equals(grdControlMain))
                {
                    if (grdViewMain.CalcHitInfo(e.ControlMousePosition) is GridHitInfo info)
                    {
                        if (info.InRowCell)
                        {
                            string text = DefectMapHelper.StringByDataRowObejct(tooltip.Rows[info.RowHandle], info.Column.FieldName);
                            string key = string.Join(" - ", info.RowHandle, info.Column.FieldName);
                            e.Info = new ToolTipControlInfo(key, text);
                        }
                    }
                }
            };

            grdControlMain.ToolTipController = _toolTipControl;
        }

        /// <summary>
        /// Diagram 선택시 체크박스 상태변경
        /// </summary>
        /// <param name="value">check 상태값</param>
        public void SetCheckMode(bool value) => chkMain.Checked = value;

        /// <summary>
        /// Diagram Data 지정
        /// </summary>
        /// <param name="dt">Row Data</param>
        public void SetData(DataTable dt) => grdControlMain.DataSource = dt;

        /// <summary>
        /// Diagram title을 가져온다
        /// </summary>
        /// <returns>string</returns>
        public string GetDiagramTitle => chkMain.Text;

        /// <summary>
        /// Diagram Check 상태 여부를 가져온다.
        /// </summary>
        /// <returns></returns>
        public bool IsCheckState => chkMain.Checked;

        #endregion
    }
}
