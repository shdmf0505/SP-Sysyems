#region using

using Micube.Framework.SmartControls;

using System.Data;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 수율 화면 기본 화면 User Control
    /// 업  무  설  명  : Layer, 공정/설비, 작업조건(Recipe) 등 수율 화면의 메인화면
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-10-04
    /// 필  수  처  리  :
    /// 
    /// </summary>
    public partial class ucRateMain : DevExpress.XtraEditors.XtraUserControl
    {
        #region Local Variables

        #endregion

        #region 생성자

        public ucRateMain()
        {
            InitializeComponent();

            InitializeLanguageKey();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            tabMain.SetLanguageKey(tabProduct, "PRODUCT");
            tabMain.SetLanguageKey(tabLot, "LOT");
            tabMain.SetLanguageKey(tabRawData, "TABROWDATA");
            grdRawData.LanguageKey = "TABROWDATA";
        }

        /// <summary>
        /// Grid 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdRawData.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdRawData.View.AddTextBoxColumn("EVENTTIME", 120).SetTextAlignment(TextAlignment.Left);
            grdRawData.View.AddTextBoxColumn("EQUIPMENTTYPE", 120).SetTextAlignment(TextAlignment.Left);
            grdRawData.View.AddTextBoxColumn("PLANTID", 120).SetTextAlignment(TextAlignment.Left);
            grdRawData.View.AddTextBoxColumn("ENTERPRISEID", 120).SetTextAlignment(TextAlignment.Left);
            grdRawData.View.AddTextBoxColumn("AREAID", 120).SetTextAlignment(TextAlignment.Left);
            grdRawData.View.AddTextBoxColumn("PRODUCTDEFID", 120).SetTextAlignment(TextAlignment.Left);
            grdRawData.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100).SetTextAlignment(TextAlignment.Left);
            grdRawData.View.AddTextBoxColumn("PROCESSSEGMENTID", 120).SetTextAlignment(TextAlignment.Left);
            grdRawData.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 100).SetTextAlignment(TextAlignment.Left);
            grdRawData.View.AddTextBoxColumn("PROCESSDEFID", 120).SetTextAlignment(TextAlignment.Left);
            grdRawData.View.AddTextBoxColumn("PROCESSDEFVERSION", 100).SetLabel("PROCESSDEFVER").SetTextAlignment(TextAlignment.Left);
            grdRawData.View.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Left);
            grdRawData.View.AddTextBoxColumn("LAYERID", 120).SetTextAlignment(TextAlignment.Left);
            grdRawData.View.AddTextBoxColumn("ISREWORK", 100).SetTextAlignment(TextAlignment.Left);
            grdRawData.View.AddTextBoxColumn("INSPECTIONQTY", 80).SetTextAlignment(TextAlignment.Right);
            grdRawData.View.AddTextBoxColumn("REPAIRTARGETQTY", 70).SetLabel("ANALYSISTARGET").SetTextAlignment(TextAlignment.Right);
            grdRawData.View.AddTextBoxColumn("REPAIRRESULTQTY", 70).SetLabel("ANALYSISRESULT").SetTextAlignment(TextAlignment.Right);
            grdRawData.View.AddTextBoxColumn("GROUPCODE", 120).SetTextAlignment(TextAlignment.Left);
            grdRawData.View.AddTextBoxColumn("GROUPNAME", 120).SetTextAlignment(TextAlignment.Left);
            grdRawData.View.AddTextBoxColumn("SUBCODE", 120).SetTextAlignment(TextAlignment.Left);
            grdRawData.View.AddTextBoxColumn("SUBNAME", 120).SetTextAlignment(TextAlignment.Left);
            grdRawData.View.AddTextBoxColumn("DEFECTCOUNT", 80).SetTextAlignment(TextAlignment.Right);

            grdRawData.View.PopulateColumns();
            grdRawData.View.SetIsReadOnly();

            grdRawData.ShowStatusBar = true;
            grdRawData.GridButtonItem = GridButtonItem.Export;
        }

        #endregion

        #region Public Function

        /// <summary>
        /// Data 설정
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="groupList"></param>
        /// <param name="type"></param>
        public void SetData(RateGroupType groupType, DataTable dt, DataTable groupList, EquipmentType type)
        {
            grdRawData.View.ClearColumns();
            InitializeGrid();
            grdRawData.DataSource = dt;

            tabProduct.Controls.Add(new ucRateGroup() { Dock = DockStyle.Fill });
            ((ucRateGroup)tabProduct.Controls[0]).SetData(groupType, dt, groupList, SubViewType.SUBVIEWTYPE_PRODUCT, type);

            tabLot.Controls.Add(new ucRateGroup() { Dock = DockStyle.Fill });
            ((ucRateGroup)tabLot.Controls[0]).SetData(groupType, dt, groupList, SubViewType.SUBVIEWTYPE_LOT, type);
        }

        #endregion
    }
}
