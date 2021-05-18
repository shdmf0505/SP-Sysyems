using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
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
    public partial class SecondOpinionPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }

        /// <summary>
        /// 부모 그리드(ProductINfo)
        /// </summary>
        public SmartBandedGrid ParentGrid { get; set; }

        /// <summary>
        /// 부모 그리드(ProductINfo)
        /// </summary>
        public SmartBandedGrid SecondOpinionGrid { get; set; }

        #endregion

        #region 생성자

        public SecondOpinionPopup(string _lotid)
        {
            InitializeComponent();
            InitializeGrid(); // 그리드 초기화
            InitailizeEvent();

            txtLotId.EditValue = _lotid;
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            InitializeSecondOpinion(); // 재의뢰접수 초기화
        }

        /// <summary>
        /// 재의뢰 그리드 초기화
        /// </summary>
        private void InitializeSecondOpinion()
        {
            grdSecodOpinion.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdSecodOpinion.View
                .AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 품목코드

            grdSecodOpinion.View
                .AddTextBoxColumn("PRODUCTDEFNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 품목명

            grdSecodOpinion.View
                .AddTextBoxColumn("LOTID", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // LOT ID

            grdSecodOpinion.View
                .AddTextBoxColumn("PROCESSSEGMENTNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 표준공정

            grdSecodOpinion.View
                .AddTextBoxColumn("AREANAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 작업장

            grdSecodOpinion.View
                .AddTextBoxColumn("EQUIPMENTNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 설비(호기)

            grdSecodOpinion.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        private void InitailizeEvent()
        {
            btnSearch.Click += BtnSearch_Click;
            btnApply.Click += BtnApply_Click;
        }

        /// <summary>        
        /// 적용
        /// </summary>
        private void BtnApply_Click(object sender, EventArgs e)
        {
            DataTable parentDt = ParentGrid.DataSource as DataTable;
            DataTable dt = grdSecodOpinion.View.GetCheckedRows();

            dt.Columns.Add(new DataColumn("REQUESTQTY", typeof(string)));
            dt.Columns.Add(new DataColumn("REQUESTREASONS", typeof(string)));
            dt.Columns.Add(new DataColumn("REQUESTNO", typeof(string)));

            if (dt.Rows.Count > 0)
            {
                dt.Rows[0]["REQUESTNO"] = parentDt.Rows[0]["REQUESTNO"];
                SecondOpinionGrid.DataSource = dt;
            } else
            {
                throw MessageException.Create("NoSaveData");
            }

            this.Close();
        }

        /// <summary>        
        /// 검색
        /// </summary>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            DataTable parentDt = ParentGrid.DataSource as DataTable;

            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("P_PROCESSSEGMENTCLASSID", parentDt.Rows[0]["PROCESSSEGMENTCLASSID"]);
            values.Add("P_PRODUCTDEFID", parentDt.Rows[0]["PRODUCTDEFID"]);
            values.Add("P_LOTID", txtLotId.EditValue);
            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dt = SqlExecuter.Query("SelectSecondOpinion", "10001", values);

            grdSecodOpinion.DataSource = dt;
        }

        #endregion

        #region Private Function

        #endregion
    }
}
