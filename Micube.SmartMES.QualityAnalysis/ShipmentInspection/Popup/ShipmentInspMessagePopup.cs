using Micube.Framework;
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
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 출하검사 > 출하검사 Message 등록 팝업
    /// 업  무  설  명  : 출하검사 Message 등록 팝업
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-09-20
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    /// 
    public partial class ShipmentInspMessagePopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region 인터페이스
        public DataRow CurrentDataRow { get; set; }
        //public delegate void SaveData(Dictionary<string, object> data);
        #endregion

        #region Local Variables
        public delegate void SaveMessage(DataTable table, string title, string contents);
        public event SaveMessage SaveMessageTable;
        public DataTable _lotListDt;
        public string _processSegmentName;
        public string _userName;
        #endregion

        #region 생성자
        public ShipmentInspMessagePopup()
        {
            InitializeComponent();
            InitializeEvent();
            InitializeGrid();
        }
        #endregion

        #region 컨텐츠 영역 초기화
        public void InitializeGrid()
        {
            grdLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdLotList.View.SetIsReadOnly();
            grdLotList.View.SetAutoFillColumn("RESOURCEID");

            grdLotList.View.AddTextBoxColumn("RESOURCEID",250)
                .SetLabel("LOTID");

            grdLotList.View.AddTextBoxColumn("DEGREE",100);

            grdLotList.View.PopulateColumns();
        }
        #endregion

        #region Event
        public void InitializeEvent()
        {
            Load += (s, e) => 
            {
                grdLotList.DataSource = _lotListDt;
                txtProcessSegment.Text = _processSegmentName;
                txtUser.Text = _userName;
            };

            //grdLotList 체크박스 이벤트 2020-01-22 결과 상관없이 메세지 입력
            //grdLotList.View.CheckStateChanged += View_CheckStateChanged;

            //저장 버튼클릭 이벤트
            btnSave.Click += BtnSave_Click;
            //닫기 버튼클릭 이벤트
            btnClose.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };
        }

        /// <summary>
        /// 검사 결과가 NG인 경우 메세지 입력불가 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CheckStateChanged(object sender, EventArgs e)
        {
            DataRow row = grdLotList.View.GetDataRow(grdLotList.View.GetFocusedDataSourceRowIndex());
            if (row["INSPECTIONRESULT"].ToString().Equals("NG") || string.IsNullOrWhiteSpace(row["INSPECTIONQTY"].ToString()))
            { 
                grdLotList.View.CheckRow(grdLotList.View.GetFocusedDataSourceRowIndex(), false);
                throw MessageException.Create("OKResultCanSaveMessage");//검사 결과가 OK인 경우만 메세지를 입력 할 수 있습니다.
            }
        }

        /// <summary>
        /// 저장 버튼클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            DataTable checkedRows = grdLotList.View.GetCheckedRows();

            if (checkedRows.Rows.Count < 1)
            {
                this.ShowMessage("MustCheckLotToMessage");//메시지를 입력할 LOTID를 체크하세요.
                return;
            }

            string title = txtTile.Text.ToString();
            string contents = memoContents.Rtf.ToString();
            SaveMessageTable(checkedRows, title, contents);

            this.Close();
        }
        #endregion

        #region Public Function
        #endregion

        #region Private Function
        #endregion
    }
}
