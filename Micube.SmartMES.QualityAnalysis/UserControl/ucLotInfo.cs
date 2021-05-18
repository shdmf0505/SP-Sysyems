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

namespace Micube.SmartMES.QualityAnalysis
{
    public partial class ucLotInfo : UserControl
    {
        #region Local Variables
        #endregion

        #region 생성자
        public ucLotInfo()
        {
            InitializeComponent();
            if (!smartPanel1.IsDesignMode())
            {
                InitializeEvent();
                InitializeGrid();
            }
        }
        #endregion

        #region 컨텐츠 영역 초기화
        public void InitializeGrid()
        {
        }
        #endregion

        #region Event
        public void InitializeEvent()
        {

        }
        #endregion

        #region Public Function
        /// <summary>
        /// data clear public function
        /// </summary>
        public void ClearLotInfoData()
        {
            ControlCollection controls = tblLotInfo.Controls;
            foreach (Control con in controls)
            {
                AllClearControl(con);
            }
        }

        /// <summary>
        /// 컨트롤에 전체 데이타를 할당하는 public 함수
        /// </summary>
        /// <param name="table"></param>
        public void SetData(DataRow row)
        {
            txtLotId.Editor.EditValue = row["LOTID"];
            txtPreProcessSegement.Editor.EditValue = row["PREVPROCESSSEGEMENTNAME"];
            txtProcessSegment.Editor.EditValue = row["PROCESSSEGMENTNAME"];
            txtNextProcessSegment.Editor.EditValue = row["NEXTPROCESSSEGMENTNAME"];
            txtUserSequence.Editor.EditValue = row["USERSEQUENCE"];

            txtProductDefId.Editor.EditValue = row["PRODUCTDEFID"];
            txtProductDefName.Editor.EditValue = row["PRODUCTDEFNAME"];
            txtInputDate.Editor.EditValue = row["INPUTDATE"];
            txtSalesOrderId.Editor.EditValue = row["SALESORDERID"];
            txtDueDate.Editor.EditValue = row["DUEDATE"];

            txtProductType.Editor.EditValue = row["PRODUCTTYPE"];
            txtLotProductType.Editor.EditValue = row["LOTPRODUCTTYPE"];
            txtIsLocking.Editor.EditValue = row["ISLOCKING"];

            txtCustomer.Editor.EditValue = row["COMPANYCLIENT"];

            txtUom.Editor.EditValue = row["UOM"];
            txtPnlQty.Editor.EditValue = row["PNLQTY"];
            txtArrayQty.Editor.EditValue = row["ARRAYQTY"];
            txtPcsQty.Editor.EditValue = row["PCSQTY"];
            txtMM.Editor.EditValue = row["MM"];
        }
        /// <summary>
        /// tblLotInfo의 해당 위치에 있는 컨트롤에 값을 할당해주는 함수 
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="value"></param>
        public void SetFileData(int col,int row, object value)
        {
            if (tblLotInfo.GetControlFromPosition(col, row) is SmartLabelTextBox txtBox)
            {
                txtBox.Editor.EditValue = value;
            }
        }
        #endregion

        #region Private Function
        /// <summary>
        /// 모든 컬트롤 값을 empty 처리하는 함수
        /// </summary>
        /// <param name="con"></param>
        private void AllClearControl(Control con)
        {
            if (con is SmartLabelTextBox txtBox)
            {
                txtBox.Editor.EditValue = string.Empty;
            }
        }

        
        #endregion
    }
}
