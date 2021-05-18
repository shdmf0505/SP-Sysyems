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

namespace Micube.SmartMES.StandardInfo.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품목관리 > 품목등록(사양)
    /// 업  무  설  명  : 품목등록(사양) 거래처 팝업
    /// 생    성    자  :  윤성원
    /// 생    성    일  : 2019-06-28
    /// 수  정  이  력  : 
    /// </summary>
    public partial class ProductCustomerPopup : SmartPopupBaseForm, ISmartCustomPopup
	{
        #region Local Variables
        public DataRow CurrentDataRow { get; set; }

        string _sCD_ITEM = "";
        /// <summary>
        ///  선택한 설비 list를 보내기 위한 Handler
        /// </summary>
        /// <param name="dt"></param>
        public delegate void chat_room_evecnt_handler(DataRow row);
        public event chat_room_evecnt_handler write_handler;
        #endregion

        #region 생성자
        public ProductCustomerPopup()
        {
            InitializeComponent();

			InitializeEvent();
            //InitializeGird();
            InitializeCondition();

        }
        public ProductCustomerPopup(string sCD_ITEM)
        {
            InitializeComponent();
            _sCD_ITEM = sCD_ITEM;

            InitializeEvent();
            InitializeCondition();
            InitializeGridIdDefinitionManagement();

            txtSearch.Text = sCD_ITEM;
            Search(txtSearch.Text,"", "");
        }

        #endregion

        #region 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        private void InitializeCondition()
        {
            

        }


        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridIdDefinitionManagement()
        {
            // GRID 초기화
            grdProductCustomer.GridButtonItem = GridButtonItem.None;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
                                                                       //grdProductItemSpec.View.AddTextBoxColumn("ENTERPIRSEID").SetIsHidden();
            grdProductCustomer.View.AddTextBoxColumn("CUSTOMERID", 80).SetIsReadOnly();
            grdProductCustomer.View.AddTextBoxColumn("CUSTOMERNAME", 150).SetIsReadOnly();
            grdProductCustomer.View.PopulateColumns();
        }

        #endregion

        #region  이벤트
        private void InitializeEvent()
        {
            btnSearch.Click += BtnSearch_Click;
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;
            grdProductCustomer.View.DoubleClick += grdProductItem_DoubleClick;      
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            DataRow row = grdProductCustomer.View.GetFocusedDataRow();
            CurrentDataRow = row;
            write_handler(row);
            this.Close();
        }

        private void grdProductItem_DoubleClick(object sender, EventArgs e)
        {
            DataRow row = grdProductCustomer.View.GetFocusedDataRow();
            CurrentDataRow = row;
            write_handler(row);
            this.Close();
        }

        

        /// <summary>
        /// 조회 클릭 - 메인 grid에 체크 데이터 전달
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Search(txtSearch.Text, txtCUSTOMERID.Text, txtCUSTOMERNAME.Text);
        }

        void Search(string sSEARCH, string sCUSTOMERID, string sCUSTOMERNAME)
        {
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("CUSTOMERID", sCUSTOMERID);
            Param.Add("CUSTOMERNAME", sCUSTOMERNAME);
            Param.Add("SEARCH", sSEARCH);
            
            DataTable dt = SqlExecuter.Query("GetCustomerList", "10001", Param);

            if (dt.Rows.Count == 1)
            {
                foreach (DataRow row in dt.Rows)
                {
                    CurrentDataRow = row;
                   //this.Close();
                }
            }
            grdProductCustomer.DataSource = dt;
            
        }

        /// <summary>
        /// 취소 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
