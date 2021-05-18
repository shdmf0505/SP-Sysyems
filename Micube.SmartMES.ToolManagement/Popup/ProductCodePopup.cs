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

namespace Micube.SmartMES.ToolManagement.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 제품코드정보 팝업
    /// 업  무  설  명  : 치공구 제작의뢰를 위한 제품코드를 조회할 때 사용
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-07-12
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ProductCodePopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        public DataRow CurrentDataRow { get; set; }

        #region Local Variables
        string _sPlantid = "";
        /// <summary>
        ///  선택한 제품품 list를 보내기 위한 Handler
        /// </summary>
        /// <param name="dt"></param>
        public delegate void chat_room_evecnt_handler(DataRow row);
        public event chat_room_evecnt_handler write_handler;
        #endregion

        #region Properties
        /// <summary>
        /// 품목의 코드를 반환 및 설정한다.
        /// </summary>
        public string ProductDeptCode
        {
            get { return txtItemCode.Text; }
            set { txtItemCode.Text = value; }
        }
        /// <summary>
        /// 외부로부터 받은 데이터테이블을 바인딩한다.
        /// </summary>
        public DataTable SearchResult
        {
            set
            {
                grdDurableCodeList.DataSource = value;
            }
        }
        #endregion

        #region 생성자
        public ProductCodePopup()
        {
            InitializeComponent();

            InitializeEvent();
           
            InitializeCondition();

            InitializeComboBox();
        }

        public ProductCodePopup(string plantID) : this()
        {
            _sPlantid = plantID;
        }
        #endregion

        #region 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        private void InitializeCondition()
        {
            //그리드 초기화
            InitializeGridIdDefinitionManagement();
        }
        #endregion

        #region ComboBox  

        /// <summary>
        /// SmartComboBox 컨트롤 추가
        /// </summary>
        private void InitializeComboBox()
        {
            // 작업구분값 정의 
            cboProductDefType.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboProductDefType.ValueMember = "CODEID";
            cboProductDefType.DisplayMember = "CODENAME";
            //cboProductDefType.EditValue = "Product";
            cboProductDefType.UseEmptyItem = true;
            cboProductDefType.EmptyItemCaption = "";
            cboProductDefType.EmptyItemValue = "";
            DataTable dtproductdeftype = SqlExecuter.Query("GetCodeListByOspProductdeftype", "10001"
             , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } });

            cboProductDefType.DataSource = dtproductdeftype;

            cboProductDefType.ShowHeader = false;
        }

        #endregion

        #region  이벤트
        private void InitializeEvent()
        {
            btnSearch.Click += BtnSearch_Click;
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;
            grdDurableCodeList.View.DoubleClick += grdProductItem_DoubleClick;

            txtItemNm.KeyPress += TxtItemNm_KeyPress;
            txtItemCode.KeyPress += TxtItemCode_KeyPress;
        }

        #region TxtItemCode_KeyPress - 품목코드 키입력이벤트
        private void TxtItemCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                Search(txtItemNm.Text, txtItemCode.Text);
            }
        }
        #endregion

        #region TxtItemNm_KeyPress - 품목명 키입력이벤트
        private void TxtItemNm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Search(txtItemNm.Text, txtItemCode.Text);
            }
        }
        #endregion

        #region BtnConfirm_Click - 확인번튼클릭이벤트
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            DataRow row = grdDurableCodeList.View.GetFocusedDataRow();
            CurrentDataRow = row;
            write_handler(row);
            this.Close();
        }
        #endregion

        #region grdProductItem_DoubleClick - 그리드 더블클릭이벤트
        private void grdProductItem_DoubleClick(object sender, EventArgs e)
        {
            DataRow row = grdDurableCodeList.View.GetFocusedDataRow();
            CurrentDataRow = row;
            write_handler(row);
            this.Close();
        }
        #endregion

        #region BtnSearch_Click - 검색버튼 이벤트
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            //if (txtItemNm.Text.Trim().Equals("")&& txtItemCode.Text.Trim().Equals(""))
            //{
            //    ShowMessage("RequiredSearch");
            //    return;

            //}
            Search(txtItemNm.Text, txtItemCode.Text);
        }
        #endregion

        #region BtnCancel_Click - 취소버튼 이벤트
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #endregion

        #region 컨트롤 초기화
        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridIdDefinitionManagement()
        {
            // GRID 초기화
            grdDurableCodeList.GridButtonItem = GridButtonItem.None;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
                                                                   //grdProductItemSpec.View.AddTextBoxColumn("ENTERPIRSEID").SetIsHidden();
            grdDurableCodeList.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetIsReadOnly();
            grdDurableCodeList.View.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsReadOnly();
            grdDurableCodeList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100).SetIsReadOnly();
            //grdDurableCodeList.View.AddTextBoxColumn("DURABLEDEFID", 100).SetIsReadOnly();
            //grdDurableCodeList.View.AddTextBoxColumn("DURABLEDEFVERSION", 50).SetIsReadOnly();            
            //grdDurableCodeList.View.AddTextBoxColumn("DURABLEDEFNAME", 150).SetIsReadOnly();            
            grdDurableCodeList.View.AddTextBoxColumn("PRODUCTDEFCODE").SetIsHidden();
            grdDurableCodeList.View.AddTextBoxColumn("PRODUCTDEFTYPE", 130).SetIsReadOnly();
            //grdDurableCodeList.View.AddTextBoxColumn("DURABLECLASSID").SetIsHidden();
            //grdDurableCodeList.View.AddTextBoxColumn("DURABLECLASSNAME", 130).SetIsReadOnly();
            //grdDurableCodeList.View.AddTextBoxColumn("DESCRIPTION", 150).SetIsReadOnly();
            //grdDurableCodeList.View.SetAutoFillColumn("PRODUCTDEFNAME");
            grdDurableCodeList.View.PopulateColumns();
        }
        #endregion

        #region 검색
        /// <summary>
        /// 검색을 수행한다. 각 컨트롤에 입력된 값을 파라미터로 받아들인다.
        /// </summary>
        /// <param name="durableDefName"></param>
        /// <param name="durableDefID"></param>
        /// <param name="durableDefVersion"></param>
        void Search(string productName, string productID)
        {
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("PRODUCTDEFID", productID);
            Param.Add("PRODUCTDEFTYPE", "Product");
            Param.Add("PRODUCTDEFNAME", productName);
            Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());
            Param.Add("PLANTID", _sPlantid);
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dt = SqlExecuter.Query("GetProductdefidPoplistByTool", "10001", Param);

            grdDurableCodeList.DataSource = dt;

        }
        #endregion
    }
}
