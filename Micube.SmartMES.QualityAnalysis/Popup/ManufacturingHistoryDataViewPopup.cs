using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Micube.SmartMES.QualityAnalysis
{
    public partial class ManufacturingHistoryDataViewPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }

        /// <summary>
        /// 부모  Control 받기
        /// </summary>
        public SmartBandedGrid tParent { get; set; }


        #region 생성자

        public ManufacturingHistoryDataViewPopup()
        {
            InitializeComponent();

            InitializeGrid(); // 그리드 초기화
            InitializeEvent(); // 이벤트 초기화
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
            grdMeasureHistory.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdMeasureHistory.View
                .AddTextBoxColumn("USERSEQUENCE", 100)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 공정순서(번호)

            grdMeasureHistory.View
                .AddTextBoxColumn("PROCESSSEGMENTNAME", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 공정명

            grdMeasureHistory.View
                .AddTextBoxColumn("AREANAME", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 작업장

            grdMeasureHistory.View
                .AddTextBoxColumn("EQUIPMENTNAME", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 설비(호기)

            grdMeasureHistory.View
                .AddTextBoxColumn("TRACKOUTTIME", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 작업종료시간

            grdMeasureHistory.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += ManufacturingHistoryDataViewPopup_Load; ;
            treeMeasureHistory.FocusedNodeChanged += TreeMeasureHistory_FocusedNodeChanged; ;
            btnApply.Click += BtnApply_Click;
            btnClose.Click += BtnClose_Click;
        }

        /// <summary>        
        /// 적용 버튼 선택
        /// </summary>
        private void BtnApply_Click(object sender, EventArgs e)
        {
            DataTable dt = grdMeasureHistory.View.GetCheckedRows();
            DataView pDt = tParent.DataSource as DataView;
            //if (pDt == null) pDt = (tParent.DataSource as DataView).ToTable();
            //if (pDt..Rows.Count == 0)
            //{
            //    tParent.DataSource = dt;
            //    //RowState Added 변경
            //    foreach (DataRow dr in (tParent.DataSource as DataTable).Rows)
            //    {
            //        dr.SetAdded();
            //    }
            //}
            //else
            //{


            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        if (pDt.Rows.Cast<DataRow>().Where(
            //            r => (r["LOTID"].ToString() == dr["LOTID"].ToString()) 
            //        && (r["LOTWORKTXNHISTKEY"].ToString() == dr["LOTWORKTXNHISTKEY"].ToString())// dr["PROCESSSEGMENTNAME"].ToString())
            //        ).ToList().Count == 0)
            //        {
            //            DataRow addRow = pDt.NewRow();
            //            //addRow.ItemArray = dr.ItemArray.Clone() as object[];

            //            foreach (DataColumn col in dt.Columns)
            //            {
            //                if(pDt.Columns.Contains(col.ColumnName))
            //                    addRow[col.ColumnName] = dr[col.ColumnName];
            //            }

            //            //addRow.SetAdded();
            //            pDt.Rows.Add(addRow);
            //        }
            //    }
            //}

            foreach (DataRow dr in dt.Rows)
            {
                if (pDt.ToTable().Rows.Cast<DataRow>().Where(
                    r => (r["LOTID"].ToString() == dr["LOTID"].ToString())
                && (r["LOTWORKTXNHISTKEY"].ToString() == dr["LOTWORKTXNHISTKEY"].ToString())// dr["PROCESSSEGMENTNAME"].ToString())
                ).ToList().Count == 0)
                {
                    DataRowView addRow = pDt.AddNew();
                    //addRow.ItemArray = dr.ItemArray.Clone() as object[];

                    addRow["LOTID"] = dr["LOTID"];
                    addRow["PROCESSSEGMENTID"] = dr["PROCESSSEGMENTID"];
                    DataColumn[] primaryKey = pDt.Table.PrimaryKey;
                    foreach (DataColumn col in dt.Columns)
                    {
                        DataColumn c = null;
                        c = Array.Find(primaryKey, f => f.ColumnName == col.ColumnName);
                        if (c == null && pDt.Table.Columns.Contains(col.ColumnName))
                            addRow[col.ColumnName] = dr[col.ColumnName];
                    }

                    //addRow.SetAdded();
                    //pDt.Rows.Add(addRow);
                }
            }

            this.Close();
        }

        /// <summary>        
        /// 포커시가 변경 되었을 경우 선택한 LOT에 해당하는 이력 조회
        /// </summary>
        private void TreeMeasureHistory_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            DataRow dr = treeMeasureHistory.GetFocusedDataRow();

            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add("P_LOTID", dr["ID"]);
            param.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

            grdMeasureHistory.DataSource = SqlExecuter.Query("GetManufacturingHistoryPopupList", "10001", param);
        }

        /// <summary>        
        /// 닫기 버튼 선택
        /// </summary>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>        
        /// 팝업화면 로드 후 이벤트 호출
        /// </summary>
        private void ManufacturingHistoryDataViewPopup_Load(object sender, EventArgs e)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add("P_LOTID", CurrentDataRow["LOTID"]);

            treeMeasureHistory.SetMember("NAME", "ID", "PARENT");
            treeMeasureHistory.DataSource = SqlExecuter.Query("GetManufacturingHistory_Tree", "10001", param);
            treeMeasureHistory.PopulateColumns();
            treeMeasureHistory.ExpandAll();

            treeMeasureHistory.SetFocusedNode(treeMeasureHistory.FindNodeByFieldValue("ID", CurrentDataRow["LOTID"]));
        }

        #endregion
    }
}
