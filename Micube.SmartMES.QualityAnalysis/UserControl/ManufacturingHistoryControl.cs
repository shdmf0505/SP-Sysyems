using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.Framework;
using Micube.Framework.SmartControls;
using Micube.Framework.Net;


namespace Micube.SmartMES.QualityAnalysis
{
    public partial class ManufacturingHistoryControl : UserControl
    {
        #region Local Variables

        /// <summary>
        /// 부모 Control
        /// </summary>
        public object tPaent;

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }

        #endregion

        #region 생성자

        public ManufacturingHistoryControl()
        {
            InitializeComponent();

            if (!spcManufacturingHistoryControl.IsDesignMode())
            {
                InitializeGrid();
                InitailizeEvent();
            }
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            InitializeGrdMeasureHistory();
        }

        /// <summary>
        /// 제조 이력 그리드 초기화
        /// </summary>
        private void InitializeGrdMeasureHistory()
        {
            grdManufacturingHistory.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore;
            grdManufacturingHistory.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdManufacturingHistory.View
                .AddTextBoxColumn("USERSEQUENCE", 100)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 공정순서(번호)

            grdManufacturingHistory.View
                .AddTextBoxColumn("LOTID", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // LOT ID

            grdManufacturingHistory.View
                .AddTextBoxColumn("PROCESSSEGMENTNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 공정명

            grdManufacturingHistory.View
                .AddTextBoxColumn("AREANAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 작업장

            grdManufacturingHistory.View
                .AddTextBoxColumn("EQUIPMENTNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 설비(호기)

            grdManufacturingHistory.View
                .AddTextBoxColumn("TRACKOUTTIME", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 작업종료시간

            grdManufacturingHistory.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitailizeEvent()
        {
            this.Load += ManufacturingHistoryControl_Load;
            grdManufacturingHistory.HeaderButtonClickEvent += GrdManufacturingHistory_HeaderButtonClickEvent;
            btnManufacturingHistory.Click += BtnManufacturingHistory_Click;
            btnDelete.Click += BtnDelete_Click;
        }

        /// <summary>
        /// 화면 Load 시 호출
        /// </summary>
        private void ManufacturingHistoryControl_Load(object sender, EventArgs e)
        {
            SearchMeasuringHistory();
        }

        /// <summary>
        /// 제조이력 선택건 삭제
        /// </summary>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DataTable dt = grdManufacturingHistory.DataSource as DataTable;
            for (int i=dt.Rows.Count-1; i>=0; i--)
            {
                if (grdManufacturingHistory.View.IsRowChecked(i))
                {
                    dt.Rows.RemoveAt(i);
                }
            }

            //grdManufacturingHistory.DataSource = dt;
        }

        /// <summary>
        /// 제조이력조회 펍업 화면 호출
        /// </summary>
        private void BtnManufacturingHistory_Click(object sender, EventArgs e)
        {
            ManufacturingHistoryPopup popup = new ManufacturingHistoryPopup();
            popup.tParent = grdManufacturingHistory;
            popup.CurrentDataRow = CurrentDataRow;
            popup.ShowDialog();
        }

        /// <summary>
        /// 그리드 해더 확장, 축소 버튼 에빈트
        /// </summary>
        private void GrdManufacturingHistory_HeaderButtonClickEvent(object sender, Framework.SmartControls.HeaderButtonClickArgs args)
        {
            if(args.ClickItem == GridButtonItem.Expand) // 확장
            {
                grdManufacturingHistory.Parent = tPaent as System.Windows.Forms.Control;
                grdManufacturingHistory.BringToFront();
            }
            else if (args.ClickItem == GridButtonItem.Restore) // 축소
            {
                grdManufacturingHistory.Parent = spcManufacturingHistoryControl;
                grdManufacturingHistory.BringToFront();
            }
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 제조 이력 정보 조회
        /// </summary>
        private void SearchMeasuringHistory()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("P_REQUESTNO", CurrentDataRow["REQUESTNO"]);
            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dt = SqlExecuter.Query("GetManufacturingHistoryList", "10001", values);

            grdManufacturingHistory.DataSource = dt;
        }

        #endregion

        #region Global Function

        /// <summary>
        /// 테이블 정보 참조
        /// </summary>
        public DataTable CurrentDataTable()
        {
            return grdManufacturingHistory.DataSource as DataTable;
        }

        #endregion
    }
}
