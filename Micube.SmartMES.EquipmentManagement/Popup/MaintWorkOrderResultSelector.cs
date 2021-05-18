#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
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

namespace Micube.SmartMES.EquipmentManagement.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 설비/제조PM실적선택팝업
    /// 업  무  설  명  : 설비PM 및 제조PM 실적을 선택하는 팝업
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-09-16
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class MaintWorkOrderResultSelector : SmartPopupBaseForm, ISmartCustomPopup
    {
        public DataRow CurrentDataRow { get; set; }
        DataTable _selectTable;
        public delegate void reSearchEvent();
        public event reSearchEvent SearchHandler;
        string _plantID;
        string _Factoryid;

        public MaintWorkOrderResultSelector(DataTable selectTable, string caption, string plantID,string factoryid) : this()
        {
            _selectTable = selectTable;
            grdSelectList.Caption = caption;
            _plantID = plantID;
            _Factoryid = factoryid;
        }
        private MaintWorkOrderResultSelector()
        {
            InitializeComponent();
            InitializeEvent();
        }

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        private void InitializeContent()
        {
            SetGrid();
        }

        #region SetGrid : 스케쥴러 컨트롤의 옵션을 설정한다.
        private void SetGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdSelectList.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기    
            grdSelectList.View.SetIsReadOnly(true);

            grdSelectList.View.AddTextBoxColumn("WORKORDERID", 180)                //출소상태
                .SetIsReadOnly(true);
            grdSelectList.View.AddTextBoxColumn("WORKORDERNAME", 300)             //의뢰일자
                .SetIsReadOnly(true);
            grdSelectList.View.AddTextBoxColumn("WORKORDERSTATUS", 150)
                .SetIsHidden();
            grdSelectList.View.AddTextBoxColumn("WORKORDERTYPE", 300)
                .SetIsHidden();
            grdSelectList.View.AddTextBoxColumn("FACTORYID", 300)
                .SetIsHidden();
            grdSelectList.View.PopulateColumns();
        }

        #endregion

        #endregion

        #region  이벤트
        private void InitializeEvent()
        {
            Shown += RegistResultMaintWorkOrderProductPopup_Shown;
            grdSelectList.View.DoubleClick += grdSelectList_DoubleClick;
            btnClose.Click += BtnClose_Click;

            grdSelectList.View.RowCellStyle += grdSelectList_RowCellStyle;
        }

        private void grdSelectList_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if(e.Column.FieldName.Equals("WORKORDERID"))
            {
                if (grdSelectList.View.GetRowCellValue(e.RowHandle, "WORKORDERSTATUS").Equals("Create"))
                {
                    e.Appearance.BackColor = Color.Gold;
                    e.Appearance.BackColor2 = Color.Gold;
                    e.Appearance.ForeColor = Color.Black;
                }
                else if(grdSelectList.View.GetRowCellValue(e.RowHandle, "WORKORDERSTATUS").Equals("Finish"))
                {
                    e.Appearance.BackColor = Color.LimeGreen;
                    e.Appearance.BackColor2 = Color.LimeGreen;

                    e.Appearance.ForeColor = Color.Black;
                }
                else if (grdSelectList.View.GetRowCellValue(e.RowHandle, "WORKORDERSTATUS").Equals("Skip"))
                {
                    e.Appearance.BackColor = Color.LightGray;
                    e.Appearance.BackColor2 = Color.LightGray;

                    e.Appearance.ForeColor = Color.Black;
                }
                else if (grdSelectList.View.GetRowCellValue(e.RowHandle, "WORKORDERSTATUS").Equals("Delay"))
                {
                    e.Appearance.BackColor = Color.Salmon;
                    e.Appearance.BackColor2 = Color.Salmon;

                    e.Appearance.ForeColor = Color.Black;
                }
                else if (grdSelectList.View.GetRowCellValue(e.RowHandle, "WORKORDERSTATUS").Equals("Start"))
                {
                    e.Appearance.BackColor = Color.PowderBlue;
                    e.Appearance.BackColor2 = Color.PowderBlue;
                    e.Appearance.ForeColor = Color.Black;
                }
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            SearchHandler?.Invoke();
            Dispose();
        }

        private void grdSelectList_DoubleClick(object sender, EventArgs e)
        {
            //this.ShowWaitArea();
            try
            {
                if (grdSelectList.View.GetFocusedDataRow() != null)
                {
                    string workOrderID = grdSelectList.View.GetFocusedRowCellValue("WORKORDERID").ToString();
                    string workOrderType = grdSelectList.View.GetFocusedRowCellValue("WORKORDERTYPE").ToString();
                    string isModify = grdSelectList.View.GetFocusedRowCellValue("ISMODIFY").ToString();
                    string factoryid = grdSelectList.View.GetFocusedRowCellValue("FACTORYID").ToString();

                    if (workOrderType.Equals("PM"))
                    {
                        Popup.RegistResultMaintWorkOrderAppPopup pmPopup = new Popup.RegistResultMaintWorkOrderAppPopup(workOrderID, _plantID, isModify, factoryid);
                        pmPopup.BindHandler += Rebind;
                        pmPopup.ShowDialog();
                    }
                    else
                    {
                        Popup.RegistResultMaintWorkOrderProductPopup productPopup = new Popup.RegistResultMaintWorkOrderProductPopup(workOrderID, _plantID, isModify);
                        productPopup.BindHandler += Rebind;
                        productPopup.ShowDialog();
                    }
                }
            }
            catch(Exception err)
            {
                throw err;
            }
            finally
            {
               ///this.CloseWaitArea();
            }
        }

        private void RegistResultMaintWorkOrderProductPopup_Shown(object sender, EventArgs e)
        {
            //화면 갱신후 서버로부터 데이터를 가져온다.    
            InitializeContent();
            grdSelectList.DataSource = _selectTable;            
        }

        #endregion

        #region Rebind : 새롭게 DataTable을 수정하여 바인딩한다.
        protected void Rebind(string workOrderID, string workOrderStatus)
        {
            for(int i = 0; i < grdSelectList.View.RowCount; i++)
            {
                if(grdSelectList.View.GetRowCellValue(i, "WORKORDERID").ToString().Equals(workOrderID))
                {
                    grdSelectList.View.SetRowCellValue(i, "WORKORDERSTATUS", workOrderStatus);
                }
            }
        }
        #endregion
    }
}
