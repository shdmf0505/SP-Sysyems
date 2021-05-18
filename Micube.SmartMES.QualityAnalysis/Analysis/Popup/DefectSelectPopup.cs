#region using

using DevExpress.XtraGrid.Views.Grid;
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

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 수율현황 
    /// 업  무  설  명  : 불량코드를 보여준다.
    /// 생    성    자  : 류시윤
    /// 생    성    일  : 2019-12-04
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class DefectSelectPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }

        public event SelectPopupApplyEventHandler Selected;

        /// <summary>
        /// 선택한 불량정보를 보낸다.
        /// </summary>
        /// <param name="dt"></param>
        public delegate void SelectedDefectHandler(DataTable dt);
        public event SelectedDefectHandler SelectedDefectHandlerEvent;

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public DefectSelectPopup()
        {
            InitializeComponent();

            InitializeEvent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeDefectCodeGridList()
        {
            DataTable srcTable = new DataTable();
            DataTable tgtTable = new DataTable();
            gridSource.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            //gridSource.View.CheckMarkSelection.MultiSelectCount = 1;
            gridSource.View.SetIsReadOnly();

            gridTarget.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            //gridTarget.View.CheckMarkSelection.MultiSelectCount = 1;
            gridTarget.View.SetIsReadOnly();

            srcTable.Columns.Add(new DataColumn("DEFECTNAME"));
            srcTable.Columns.Add(new DataColumn("QCSEGMENT"));
            gridSource.DataSource = srcTable;

            tgtTable.Columns.Add(new DataColumn("DEFECTNAME"));
            tgtTable.Columns.Add(new DataColumn("QCSEGMENT"));
            gridTarget.DataSource = tgtTable;

            gridSource.View.AddTextBoxColumn("DEFECTNAME", 120);
            gridSource.View.AddTextBoxColumn("QCSEGMENT", 120);
            gridTarget.View.AddTextBoxColumn("DEFECTNAME", 120);
            gridTarget.View.AddTextBoxColumn("QCSEGMENT", 120);

            gridSource.View.PopulateColumns();
            gridTarget.View.PopulateColumns();

            lrBtnDefectSelect.SourceGrid = gridSource;
            lrBtnDefectSelect.TargetGrid = gridTarget;

            
            //grdDefectLeft.View.SetIsReadOnly();
            //grdDefectLeft.View.SetAutoFillColumn("DEFECTQTY");

            //grdDefectLeft.View.AddTextBoxColumn("PROCESSDATE", 200)
            //    .SetTextAlignment(TextAlignment.Center); // 처리일시
            //grdDefectLeft.View.AddTextBoxColumn("DEFECTCODE", 150); // 불량코드
            //grdDefectLeft.View.AddTextBoxColumn("DEFECTCODENAME", 180); // 불량명
            //grdDefectLeft.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 180)
            //    .SetLabel("REASONSEGMENTNAME"); // 원인공정
            //grdDefectLeft.View.AddTextBoxColumn("DEFECTQTY", 80); // 불량수

            //grdDefectLeft.View.PopulateColumns();

            //grdDefectLeft.DataSource = _defectDt;
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += DefectSelectPopup_Load;

            btnSearch.Click += (s, e) =>
            {
                SearchDefectCode();
            };

            btnSave.Click += (s, e) =>
            {
                DataTable resultDataTable = new DataTable();
                //resultDataTable.Columns.Add("DEFECTCODE", typeof(string));
                resultDataTable.Columns.Add("DEFECTNAME", typeof(string));
                //resultDataTable.Columns.Add("QCSEGMENTID", typeof(string));
                resultDataTable.Columns.Add("QCSEGMENT", typeof(string));

                for(int i = 0; i < gridTarget.View.RowCount; i++)
                {
                    DataRow row = gridTarget.View.GetDataRow(i);

                    if(row != null)
                    {
                        resultDataTable.Rows.Add(row["DEFECTNAME"], row["QCSEGMENT"]);
                    }
                }

                
                DataTable dt = CurrentDataRow.Table.Clone();
                foreach (var item in resultDataTable.AsEnumerable().GroupBy(x => x.Field<string>("DEFECTNAME")))
                {
                    DataRow dr = dt.NewRow();
                    dr["DEFECTNAME"] = item.Key;
                    dr["P_DEFECTCODE"] = item.Key;
                    dt.Rows.Add(dr);
                }

                Selected(this, new SelectPopupApplyEventArgs() { Selections = dt.Rows.Cast<DataRow>().OrderBy(x => x.Field<string>("DEFECTNAME")) });

                this.SelectedDefectHandlerEvent(resultDataTable);
                this.Close();
            };
            //btnClose.Click += BtnClose_Click;
        }

        /// <summary>
        /// 닫기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Form Road시 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefectSelectPopup_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            InitializeDefectCodeGridList();

            //SearchDefectCode();
        }

        #endregion
 
        #region Private Function

        /// <summary>
        /// 메인 그리드에서 Lot ID를 가져와서 해당 Lot의 불량코드 정보조회
        /// </summary>
        private void SearchDefectCode()
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                {"LANGUAGETYPE" , Framework.UserInfo.Current.LanguageType},
                {"PLANTID" , Framework.UserInfo.Current.Plant},
                {"ENTERPRISEID" , Framework.UserInfo.Current.Enterprise},
            };

            if (ltbDefectName.EditValue != null)
                param.Add("DEFECTNAME", ltbDefectName.EditValue.ToString());

            if(ltbQCProcess.EditValue != null)
                param.Add("QCSEGMENTNAME", ltbQCProcess.EditValue.ToString());

            DataTable defectDt = SqlExecuter.Query("GetDefectCodeByYieldStatus", "10001", param);

            gridSource.DataSource = defectDt;
            //lrBtnDefectSelect.SourceGrid = gridSource;
        }

        #endregion
    }
}
