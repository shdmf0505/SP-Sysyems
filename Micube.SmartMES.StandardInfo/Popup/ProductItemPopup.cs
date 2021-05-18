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
    /// 프 로 그 램 명  : 기준정보 > Setup > 작업장 관리 > 설비 유형 팝업
    /// 업  무  설  명  : 설비 유형(EquipmentClass)을 선택
    /// 생    성    자  :  정승원
    /// 생    성    일  : 2019-05-22
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ProductItemPopup : SmartPopupBaseForm, ISmartCustomPopup
	{
        public DataRow CurrentDataRow { get; set; }

        #region Local Variables
        string _sCD_ITEM = "";
        string _sITEMVERSION = "";
        string _sNM_ITEM = "";

      

		//Resource Type = Equipment
		
        /// <summary>
        ///  선택한 list를 보내기 위한 Handler
        /// </summary>
        /// <param name="dt"></param>
        public delegate void chat_room_evecnt_handler(DataRow row);
        public event chat_room_evecnt_handler write_handler;
        #endregion

        #region 생성자
        public ProductItemPopup()
        {
            InitializeComponent();
			InitializeEvent();
            InitializeCondition();
        }
        public ProductItemPopup(string sCD_ITEM)
        {
            InitializeComponent();

            _sCD_ITEM = sCD_ITEM;
            InitializeEvent();
            InitializeCondition();
            InitializeGridIdDefinitionManagement();
            txtSearch.Text = sCD_ITEM;
            Search(txtSearch.Text,"", "", "");
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
            grdProductItem.GridButtonItem = GridButtonItem.None;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdProductItem.View.AddTextBoxColumn("ITEMCODE", 150).SetIsReadOnly();
            grdProductItem.View.AddTextBoxColumn("ITEMVERSION", 150).SetIsReadOnly();
            grdProductItem.View.AddTextBoxColumn("ITEMNAME", 150).SetIsReadOnly();
            grdProductItem.View.PopulateColumns();
        }
        #endregion

        #region  이벤트
        private void InitializeEvent()
        {
            btnSearch.Click += BtnSearch_Click;
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;
            grdProductItem.View.DoubleClick += grdProductItem_DoubleClick;      
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            DataRow row = grdProductItem.View.GetFocusedDataRow();
            CurrentDataRow = row;
            write_handler(row);
            this.Close();
        }

        private void grdProductItem_DoubleClick(object sender, EventArgs e)
        {
            DataRow row = grdProductItem.View.GetFocusedDataRow();
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
            Search(txtSearch.Text, txtItemCode.Text, txtItemVer.Text, txtItemNm.Text);
        }

        void Search(string sSEARCH, string sCD_ITEM,string _sITEMVERSION,string sNM_ITEM)
        {
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("ITEMCODE", sCD_ITEM);
            Param.Add("ITEMVERSION", _sITEMVERSION);
            Param.Add("ITEMNAME", sNM_ITEM);
            Param.Add("SEARCH", sSEARCH);
            
            DataTable dt = SqlExecuter.Query("GetProductItemMaster", "10001", Param);

            // 해당 로우가 1개 인경우 
            if (dt.Rows.Count == 1)
            {
                foreach (DataRow row in dt.Rows)
                {
                    CurrentDataRow = row;
                   //this.Close();
                }
            }
            grdProductItem.DataSource = dt;
            
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
