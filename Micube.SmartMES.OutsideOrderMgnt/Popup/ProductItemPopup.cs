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

namespace Micube.SmartMES.OutsideOrderMgnt.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 제품코드 정보 팝업. 
    /// 업  무  설  명  : 제품정보 팝업 
    /// 생    성    자  : 정승원,최별
    /// 생    성    일  : 2019-06-18
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class productdefidPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        public DataRow CurrentDataRow { get; set; }

        #region Local Variables
        string _sCD_ITEM = "";
        string _sPlantid = "";
        /// <summary>
        ///  Handler
        /// </summary>
        /// <param name="dt"></param>
        public delegate void chat_room_evecnt_handler(DataRow row);
        public event chat_room_evecnt_handler write_handler;
        #endregion

        #region 생성자
        public productdefidPopup()
        {
            InitializeComponent();
            InitializeComboBox();  // 콤보박스 셋팅 
            InitializeEvent();
           
            InitializeCondition();

            _sPlantid =  UserInfo.Current.Plant.ToString();

        }
        public productdefidPopup(string sCD_ITEM,string sPlantid)
        {
            InitializeComponent();
            InitializeComboBox();  // 콤보박스 셋팅 
            _sCD_ITEM = sCD_ITEM;
            InitializeEvent();
            InitializeCondition();
            InitializeGridIdDefinitionManagement();

           
            if (!(sPlantid.Equals("")))
            {
                _sPlantid = sPlantid;
            }
            else
            {
                _sPlantid = UserInfo.Current.Plant.ToString();
            }
         
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sCD_ITEM"></param>
        /// <param name="sItemVer"></param>
        /// <param name="sItemName"></param>
        /// <param name="sPlantid"></param>
        public productdefidPopup(string sCD_ITEM, string sItemVer, string sItemName,  string sPlantid)
        {
            InitializeComponent();

            _sCD_ITEM = sCD_ITEM;
            txtItemCode.EditValue = _sCD_ITEM;
            InitializeComboBox();  // 콤보박스 셋팅 
            txtItemNm.EditValue = sItemName;
            InitializeEvent();
            InitializeCondition();
            InitializeGridIdDefinitionManagement();


            if (!(sPlantid.Equals("")))
            {
                _sPlantid = sPlantid;
            }
            else
            {
                _sPlantid = UserInfo.Current.Plant.ToString();
            }
            // Search( "", "", "");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sCD_ITEM"></param>
        /// <param name="sItemVer"></param>
        /// <param name="sItemName"></param>
        /// <param name="sPlantid"></param>
        public productdefidPopup(string sCD_ITEM,  string sPlantid, DataTable dtResult)
        {
            InitializeComponent();

            _sCD_ITEM = sCD_ITEM;
            txtItemCode.EditValue = _sCD_ITEM;
            InitializeComboBox();  // 콤보박스 셋팅 
            InitializeEvent();
            InitializeCondition();
            InitializeGridIdDefinitionManagement();


            if (!(sPlantid.Equals("")))
            {
                _sPlantid = sPlantid;
            }
            else
            {
                _sPlantid = UserInfo.Current.Plant.ToString();
            }

            grdProductItem.DataSource = dtResult;
            // Search( "", "", "");
        }
        #region ComboBox  

        /// <summary>
        /// SmartComboBox 컨트롤 추가
        /// </summary>
        private void InitializeComboBox()
        {
            // 작업구분값 정의 
            cboproductdeftype.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboproductdeftype.ValueMember = "CODEID";
            cboproductdeftype.DisplayMember = "CODENAME";
            cboproductdeftype.EditValue = "Product";
            DataTable dtproductdeftype = SqlExecuter.Query("GetCodeListByOspProductdeftype", "10001"
             , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
         
            cboproductdeftype.DataSource = dtproductdeftype;

            cboproductdeftype.ShowHeader = false;
           
        }

        #endregion
        #endregion

        #region 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        private void InitializeCondition()
        {


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
            Search( txtItemCode.Text,  txtItemNm.Text);
        }

        void Search( string sCD_ITEM, string sNM_ITEM)
        {
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("PRODUCTDEFID", sCD_ITEM);
            Param.Add("PRODUCTDEFTYPE", cboproductdeftype.EditValue);
            Param.Add("PRODUCTDEFNAME", sNM_ITEM);
            Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());
            Param.Add("PLANTID", _sPlantid);
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dt = SqlExecuter.Query("GetProductdefidPoplistByOsp", "10001", Param);
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

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridIdDefinitionManagement()
        {
            // GRID 초기화
            grdProductItem.GridButtonItem = GridButtonItem.None;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdProductItem.View.AddComboBoxColumn("PRODUCTDEFTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();
            grdProductItem.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetIsReadOnly();
            grdProductItem.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsReadOnly();
            grdProductItem.View.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsReadOnly();
            
            //grdProductItem.View.SetAutoFillColumn("PRODUCTDEFNAME");
            grdProductItem.View.PopulateColumns();
        }

        private void smartMemoEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
