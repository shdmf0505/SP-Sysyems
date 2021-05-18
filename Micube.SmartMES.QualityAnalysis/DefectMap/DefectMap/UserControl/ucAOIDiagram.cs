#region using

using Micube.Framework;

using DevExpress.Utils;
using DevExpress.XtraDiagram;

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : AOI Diagram User Control
    /// 업  무  설  명  : AOI Defect Map 표현을 위한 Control
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-05-01
    /// 필  수  처  리  :
    /// *** 2차 개발 기준 개발 필수
    /// ****** 1. VRS Defect인 경우 Tooltip에 Image를 보여 준다. 122번째 줄
    /// 수  정  이  력  : 
    /// [2019.06.18] Shape Border Color 변경
    /// </summary>
    public partial class ucAOIDiagram : DevExpress.XtraEditors.XtraUserControl
    {
        #region Global Variable

        /// <summary>
        /// Diagram Shape 선택시 발생 이벤트 Handler
        /// </summary>
        /// <param name="str"></param>
        public delegate void PointSelectionDataHandler(string str);

        /// <summary>
        /// Diagram Shape 선택시 발생 이벤트
        /// </summary>
        public event PointSelectionDataHandler PointSelectionEvent;

        /// <summary>
        /// Diagram Shape 선택시 VRS/AOI 여부 확인 이벤트 Handler
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public delegate bool InspectionTypeHandler(string str);

        /// <summary>
        /// Diagram Shape 선택시 VRS/AOI 여부 확인 이벤트
        /// </summary>
        public event InspectionTypeHandler InspectionTypeEvent;

        /// <summary>
        /// Lot별 Defect Map Popup 발생 이벤트 Handler
        /// </summary>
        public delegate void ShowPopUpEventHandler();

        /// <summary>
        /// Lot별 Defect Map Popup 발생 이벤트
        /// </summary>
        public event ShowPopUpEventHandler ShowPopupEvent;

        /// <summary>
        /// Defect Point 도형 Size
        /// </summary>
        private const float SIZE_VALUE = 3;

        /// <summary>
        /// 배경이미지
        /// </summary>
        private Image _backgroundImage;

        /// <summary>
        /// Diagram Type
        /// </summary>
        private AOIMode _viewMode = AOIMode.AOIMODE_MAIN;

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="width">canvers width</param>
        /// <param name="height">canvers height</param>
        /// <param name="mode">View Mode</param>
        /// <param name="bgImage">BackGround Image</param>
        /// <param name="title">ViewMode가 Nail일 경우 타이틀</param>
        public ucAOIDiagram(int width, int height, AOIMode mode, Image bgImage = null, string title = "")
        {
            _backgroundImage = bgImage;
            _viewMode = mode;
            InitializeComponent();
            InitializeControls(width, height, title);
            InitializeEvent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 컨트롤 초기설정
        /// 설정 순서 바꾸지 말 것
        /// </summary>
        /// <param name="width">canvers width</param>
        /// <param name="height">canvers height</param>
        /// <param name="title">타이틀 명</param>
        private void InitializeControls(int width, int height, string title)
        {
            diagramControlMap.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            diagramControlMap.Dock = DockStyle.Fill;
            diagramControlMap.OptionsBehavior.SelectedStencils
                = new DevExpress.Diagram.Core.StencilCollection(new string[] { "BasicShapes", "BasicFlowchartShapes" });
            diagramControlMap.OptionsBehavior.SnapToGrid = false;
            diagramControlMap.OptionsBehavior.SnapToItems = false;
            diagramControlMap.OptionsBehavior.ScrollMode = DevExpress.Diagram.Core.DiagramScrollMode.Page;
            diagramControlMap.OptionsView.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            diagramControlMap.OptionsView.ShowGrid = false;
            diagramControlMap.OptionsView.ShowPageBreaks = false;
            diagramControlMap.OptionsView.ShowRulers = false;
            diagramControlMap.OptionsView.CanvasSizeMode = DevExpress.Diagram.Core.CanvasSizeMode.AutoSize;
            diagramControlMap.Margin = new Padding(0);
            diagramControlMap.OptionsView.PageSize = new Size(width, height);
            layoutMain.AllowCustomization = false;
            diagramControlMap.FitToPage();

            colorPickMain.Color = Color.Transparent;
            colorPickMain.Properties.ColorAlignment = HorzAlignment.Center;
            colorPickMain.Properties.Appearance.BackColor = Color.Transparent;
            colorPickMain.Properties.AutomaticColor = Color.Transparent;
            colorPickMain.Properties.ShowWebColors = false;
            colorPickMain.Properties.ShowSystemColors = false;
            colorPickMain.Properties.AutomaticColorButtonCaption = "Default";
            colorPickMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;

            chkMain.Text = title;
            chkMain.ToolTip = title;

            layoutControlItemColorBox.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

            if (_viewMode.Equals(AOIMode.AOIMODE_MAIN))
            {
                ToolTipController toolTip = new ToolTipController();
                toolTip.GetActiveObjectInfo += (s, e) =>
                {
                    if (!e.SelectedControl.Equals(diagramControlMap))
                    {
                        return;
                    }

                    if (diagramControlMap.CalcHitItem(e.ControlMousePosition) is DiagramShape shape)
                    {
                        if (!InspectionTypeEvent(Format.GetString(shape.Tag)))
                        {
                            return;
                        }

                        // TODO : 현재 무조건 return 됨. 추후 이미지 처리 진행해야함.
                        if (!DefectMapHelper.IsNull(shape.Tag))
                        {
                            return;
                        }

                        ToolTipControlInfo toolTipControlInfo = new ToolTipControlInfo(shape, Format.GetString(shape.Tag));
                        SuperToolTip superToolTip = new SuperToolTip();
                        //superToolTip.Items.AddTitle(shape.Tag.ToString());
                        superToolTip.Items.Add(new ToolTipItem()
                        {
                            Image = new Bitmap(Properties.Resources._113_2006_b_aoi_target_gbr_Top, new Size(200, 300))
                        });

                        toolTipControlInfo.SuperTip = superToolTip;
                        e.Info = toolTipControlInfo;
                    }
                };

                diagramControlMap.ToolTipController = toolTip;

                this.BorderStyle = BorderStyle.None;
                diagramControlMap.Enabled = true;
                layoutControlItemCheckBox.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItemColorBox.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                //! Defect Map Selection Changed Event
                diagramControlMap.SelectionChanged += (s, e) =>
                {
                    string str = "[DEFECTNO] = ";

                    for (int i = 0; i < diagramControlMap.SelectedItems.Count; i++)
                    {
                        str += diagramControlMap.SelectedItems[i].Tag;
                        if (i != diagramControlMap.SelectedItems.Count - 1)
                        {
                            str += " OR [DEFECTNO]=";
                        }
                    }

                    PointSelectionEvent(str);
                };
            }
            else if (_viewMode.Equals(AOIMode.AOIMODE_NAIL)) //Nail
            {
                chkMain.CheckStateChanged += (s, e) =>
                {
                    if (chkMain.CheckState.Equals(CheckState.Checked))
                    {
                        chkMain.BackColor = Color.Gold;
                        colorPickMain.BackColor = Color.Gold;
                    }
                    else
                    {
                        chkMain.BackColor = Color.Transparent;
                        colorPickMain.BackColor = Color.Transparent;
                    }
                };

                layoutMain.MouseClick += new MouseEventHandler((sender, e) =>
                {
                    if (DefectMapHelper.IsNull(GetChildAtPoint(e.Location)))
                    {
                        return;
                    }

                    if (e.Button.Equals(MouseButtons.Left))
                    {
                        chkMain.Checked = chkMain.CheckState.Equals(CheckState.Checked) ? false : true;
                    }
                });

                diagramControlMap.Enabled = false;

                layoutControlItemCheckBox.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
                layoutControlItemCheckBox.MaxSize = new Size(168, 20);
                layoutControlItemCheckBox.MinSize = new Size(168, 20);
            }
            else if (_viewMode.Equals(AOIMode.AOIMODE_POPUPNAIL)) //popup Nail
            {
                diagramControlMap.Enabled = false;
                layoutControlItemCheckBox.Enabled = false;
                layoutControlItemColorBox.Enabled = false;

                layoutControlItemCheckBox.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
                layoutControlItemCheckBox.MaxSize = new Size(218, 20);
                layoutControlItemCheckBox.MinSize = new Size(218, 20);
            }

            diagramControlMap.CustomDrawBackground += (sender, e) =>
            {
                if (!DefectMapHelper.IsNull(_backgroundImage) && !e.Context.Equals(DiagramDrawingContext.Print))
                {
                    e.Graphics.DrawImage(_backgroundImage, e.TotalBounds);
                }
            };
        }

        #endregion

        #region Event

        /// <summary>        
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {

        }

        #endregion

        #region Private Function

        /// <summary>
        /// 팝업 발생 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailsView(object sender, EventArgs e) => ShowPopupEvent();

        #endregion

        #region Public Function

        /// <summary>
        /// DiagramControl에 Item 추가
        /// </summary>
        /// <param name="xValue">좌표 x</param>
        /// <param name="yValue">좌표 y</param>
        /// <param name="color">point 색상</param>
        /// <param name="tooltip">point 툴팁에 표시되는 Text</param>
        /// <returns></returns>
        public void AddXYPoint(float xValue, float yValue, Color color, string tooltip)
        {
            DiagramShape diagramItem = new DiagramShape
            {
                MinSize = new SizeF(1, 1),
                Height = SIZE_VALUE,
                Width = SIZE_VALUE,
                Shape = DevExpress.Diagram.Core.BasicShapes.Ellipse,
                Tag = tooltip,
                CanSelect = true,
                CanMove = false,
                CanResize = false,
                CanEdit = false,
                CanCopy = false,
                CanDelete = false,
                CanChangeParameter = false,
                CanRotate = false,
                CanSnapToOtherItems = false,
                CanSnapToThisItem = false,
                CanHideSubordinates = false,
                CanAttachConnectorBeginPoint = false,
                CanAttachConnectorEndPoint = false,
                CanChangeParent = false
            };

            diagramItem.Appearance.BackColor = color;
            diagramItem.Appearance.BorderColor = color;

            float x = xValue < 2 ? xValue + 2 : xValue;
            float y = yValue < 2 ? yValue + 2 : yValue;

            diagramItem.Position = new PointFloat(x - (diagramItem.Width / 2), y - (diagramItem.Height / 2));

            diagramControlMap.Items.Add(diagramItem);
        }

        /// <summary>
        /// 품목별로 Nail Map이 Lot별로 나올 경우 ContextMenu를 만들어준다.
        /// </summary>
        /// <param name="value">AOI Nail Map 구분자</param>
        public void SetContextMenu(string value)
        {
            MenuItem menuItem = value.Equals(Format.GetString(ComboType.LOTID)) ?
                    new MenuItem("Details View", new EventHandler(DetailsView)) :
                    new MenuItem("Mode View", new EventHandler(DetailsView));

            layoutMain.ContextMenu = new ContextMenu(new MenuItem[] { menuItem });
        }

        /// <summary>
        /// 다이어그램 배경이미지 적용하기
        /// </summary>
        /// <param name="image"></param>
        public void SetDiagramBackGroundImage(Image image)
        {
            _backgroundImage = image;
            diagramControlMap.Invalidate();
        }

        /// <summary>
        /// Map 선택시 체크박스에 checked
        /// </summary>
        /// <param name="value"></param>
        public void SetCheckMode(bool value) => chkMain.Checked = chkMain.Checked != value ? value : chkMain.Checked;

        /// <summary>
        /// Diagram title을 가져온다
        /// </summary>
        /// <returns>string</returns>
        public string GetDiagramTitle => chkMain.Text;

        /// <summary>
        /// 현재 체크박스 상태 리턴
        /// </summary>
        /// <returns></returns>
        public bool IsCheckState => chkMain.Checked;

        /// <summary>
        /// Color Pick 색상 가져오기
        /// </summary>
        public Color GetNailColor => colorPickMain.Color;

        #endregion
    }
}