#region using

using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.Framework.SmartControls;
using Micube.Framework.Net;
using Micube.Framework;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > NCR 진행현황 User Control
    /// 업  무  설  명  : 품질관리에서 사용되는 NCR 진행현황 User Control이다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-06-19
    /// 수  정  이  력  : 2019-08-08 강유라
    /// 
    /// 
    /// </summary>
    public partial class NCRProgressControl : UserControl
    {
        #region Local Variables

        /// <summary>
        /// 팝업 그리드와 원래 그리드를 비교하기 위한 변수
        /// </summary>
        private DataTable _mappingDataSource = new DataTable();
        private DataTable _checked;
        public DataRow CurrentDataRow { get; set; }


        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public NCRProgressControl()
        {
            InitializeComponent();

            if (!gbxNCRProgress.IsDesignMode())
            {
                InitializeEvent();

                InitializeGrid();

                InitializeComboBox();
            }
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdNCRProgress.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdNCRProgress.View.SetIsReadOnly();

            var standard = grdNCRProgress.View.AddGroupColumn("STANDARD");
            standard.AddTextBoxColumn("PRODUCTDEFID", 120);
            standard.AddTextBoxColumn("PRODUCTDEFNAME", 250);
            standard.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            standard.AddTextBoxColumn("LOTID", 200)
                .SetTextAlignment(TextAlignment.Center);
            standard.AddTextBoxColumn("LOCKINGTXNHISTKEY", 100)
                .SetIsHidden();
            standard.AddTextBoxColumn("PANELQTY", 80)
                .SetTextAlignment(TextAlignment.Right);
            standard.AddTextBoxColumn("PCSQTY", 80)
                .SetTextAlignment(TextAlignment.Right);

            var location = grdNCRProgress.View.AddGroupColumn("LOCATION");
            location.AddTextBoxColumn("PROCESSSEGMENTID", 120)
                .SetIsHidden();
            location.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            location.AddTextBoxColumn("AREAID", 100)
                .SetIsHidden();
            location.AddTextBoxColumn("AREANAME", 150);

            //실제 lot의 locking 여부
            location.AddTextBoxColumn("ISLOTLOCKING", 150)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("REALLOTLOCKING");

            var etc = grdNCRProgress.View.AddGroupColumn("ETC");
            etc.AddComboBoxColumn("RESULTCODE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProcessingStatus", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();

            //affectLot locking 여부
            etc.AddComboBoxColumn("ISLOCKING", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("AFFECTLOCKING");

            etc.AddTextBoxColumn("ABNOCRNO", 100).SetIsHidden();
            etc.AddTextBoxColumn("ABNOCRTYPE", 100).SetIsHidden();
            etc.AddTextBoxColumn("ISADDED", 100).SetIsHidden();

            grdNCRProgress.View.PopulateColumns();
        }

        #endregion

        #region ComboBox

        /// <summary>
        /// ComboBox 초기화
        /// </summary>
        private void InitializeComboBox()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("CODECLASSID", "ProcessingStatus");
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            cboHandling.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboHandling.ValueMember = "CODEID";
            cboHandling.DisplayMember = "CODENAME";
            cboHandling.DataSource = SqlExecuter.Query("GetCodeList", "00001", param);
            cboHandling.ShowHeader = false;
        }

        #endregion

        #region Popup

        #endregion

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {
            btnApply.Click += BtnApply_Click;
            btnLockingReset.Click += BtnLockingReset_Click;
            btnLockingApply.Click += BtnLockingApply_Click;
            btnAddAffectLot.Click += BtnAddAffectLot_Click;
            btnDelete.Click += BtnDelete_Click;
            //2020-01-16 affectLot isLocking 에 따른 추가 
            grdNCRProgress.View.RowCellStyle += View_RowCellStyle;
        }

        /// <summary>
        /// affectLot isLocking Y 시 Row 빨간색
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView grid = sender as GridView;
            if (e.RowHandle < 0) return;

            DataRow row = (DataRow)grid.GetDataRow(e.RowHandle);

            if (Format.GetString(row["ISLOCKING"]).Equals("Y"))
            {
                if (!grid.IsCellSelected(e.RowHandle, e.Column))
                    e.Appearance.ForeColor = Color.Red;
            }

        }

        /// <summary>
        /// 처리여부 일괄적용
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnApply_Click(object sender, EventArgs e)
        {
            DataTable origin = grdNCRProgress.DataSource as DataTable;
            DataTable checkedTable = grdNCRProgress.View.GetCheckedRows();

            if (checkedTable.Rows.Count == 0)
            {
                MSGBox.Show(MessageBoxType.Information, "GridNoChecked");
                return;
            }

            var lotIdList = checkedTable.AsEnumerable().Select(r => r["LOTID"]).ToList();

            for (int i = 0; i < origin.Rows.Count; i++)
            {
                if (lotIdList.Contains(origin.Rows[i]["LOTID"].ToString()) && cboHandling.EditValue != null)
                {
                    origin.Rows[i]["RESULTCODE"] = cboHandling.EditValue.ToString();
                }

            }
        }

        /// <summary>
        /// AffectLot에 Locking 일괄초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLockingReset_Click(object sender, EventArgs e)
        {
            DataTable origin = grdNCRProgress.DataSource as DataTable;
            DataTable checkedTable = grdNCRProgress.View.GetCheckedRows();

            if (checkedTable.Rows.Count == 0)
            {
                MSGBox.Show(MessageBoxType.Information, "GridNoChecked");
                return;
            }

            var lotIdList = checkedTable.AsEnumerable().Select(r => r["LOTID"]).ToList();

            for (int i = 0; i < origin.Rows.Count; i++)
            {
                if (lotIdList.Contains(origin.Rows[i]["LOTID"].ToString()))
                {
                    origin.Rows[i]["ISLOCKING"] = "N";
                }

            }
        }

        /// <summary>
        /// AffectLot에 Locking 일괄적용
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLockingApply_Click(object sender, EventArgs e)
        {
            DataTable origin = grdNCRProgress.DataSource as DataTable;
            DataTable checkedTable = grdNCRProgress.View.GetCheckedRows();

            if (checkedTable.Rows.Count == 0)
            {
                MSGBox.Show(MessageBoxType.Information, "GridNoChecked");
                return;
            }

            //2020-01-13 LotState 체크하여 재공상태 아니면 locking 불가
            //LOTSTATE 가  InTransit 또는 InProduction 가 아닌경우
            string[] lotstateList = checkedTable.AsEnumerable()
                .Where(r => !(r["LOTSTATE"].ToString().Equals("InTransit") || r["LOTSTATE"].ToString().Equals("InProduction")))
                .Select(r => r["LOTID"].ToString()).ToArray();
            

            if (lotstateList.Length > 0)
            {
                MSGBox.Show(MessageBoxType.Information, "ValidWipLot", lotstateList);
                return;
            }
                

            var lotIdList = checkedTable.AsEnumerable().Select(r => r["LOTID"]).ToList();

            for (int i = 0; i < origin.Rows.Count; i++)
            {
                if (lotIdList.Contains(origin.Rows[i]["LOTID"].ToString()))
                {
                    origin.Rows[i]["ISLOCKING"] = "Y";
                }

            }
        }

        /// <summary> 
        /// AffectLot 삭제버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DataTable origin = grdNCRProgress.DataSource as DataTable;
            for (int i = origin.Rows.Count - 1; i >= 0; i--)
            {
                if (grdNCRProgress.View.IsRowChecked(i) && origin.Rows[i]["ISADDED"].ToString().Equals("Y"))
                {
                    origin.Rows.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// AffectLot 추가버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddAffectLot_Click(object sender, EventArgs e)
        {
            AffectLotPopup popup = new AffectLotPopup();
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.FormBorderStyle = FormBorderStyle.Sizable;
            popup.CurrentDataRow = CurrentDataRow;
            popup.ShowDialog();
            if (popup.DialogResult == DialogResult.OK)
            {
                _checked = popup._checkTable;
                var lotList = (grdNCRProgress.DataSource as DataTable).AsEnumerable().Select(r => r["LOTID"]).ToList();
                string lotId = "";
                foreach (DataRow row in _checked.Rows)
                {
                    lotId = row["LOTID"].ToString();
                    if (!lotList.Contains(lotId))
                    {
                        (grdNCRProgress.DataSource as DataTable).ImportRow(row);
                    }
                }

            }
            //popup.AffectLotSelectEvent += (dt) =>
            //{
            //    var lotList = (grdNCRProgress.DataSource as DataTable).AsEnumerable().Select(r => r["LOTID"]).ToList();
            //    string lotId = "";
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        lotId = row["LOTID"].ToString();
            //        if (!lotList.Contains(lotId))
            //        {
            //            (grdNCRProgress.DataSource as DataTable).Rows.Add(row.ItemArray);
            //        }
            //    }
            //};
            //popup.ShowDialog();

        }

        #endregion

        #region Private Function

        #endregion

        #region Global Function
        /// <summary>
        /// grdNCRProgress 그리드에 데이터바인딩 하는 함수
        /// </summary>
        /// <param name="table"></param>
        public void SetDataGrd(DataTable table)
        {
            grdNCRProgress.DataSource = table;
        }

        /// <summary>
        /// grdNCRProgress 그리드 변경row를 가져오는 함수
        /// </summary>
        /// <returns></returns>
        public DataTable GetChangedData()
        {
            DataTable toRealSave = grdNCRProgress.GetChangedRows().Clone();
            //수정된 row
            DataTable changed = grdNCRProgress.GetChangedRows();

            if(changed.Rows.Count >0 )
            {
                //수정된 row중 업데이트 될 row
                var noAddedList = changed.AsEnumerable()
                    .Where(r => !r["ISADDED"].ToString().Equals("Y"))
                    .ToList();

                if (noAddedList.Count > 0)
                {
                    toRealSave = noAddedList.CopyToDataTable();
                }
            }

            //새로 추가된 row => 추가만 하고 locking, 처리여부 바꾸지않는경우 저장되지 않는문제 해결위해
            var AddedList = (grdNCRProgress.DataSource as DataTable).AsEnumerable()
                .Where(r => r["ISADDED"].ToString().Equals("Y"))
                .ToList();

            if (AddedList.Count > 0)
            {
                toRealSave.Merge(AddedList.CopyToDataTable());
            }

            return toRealSave;
        }
        #endregion
    }
}
