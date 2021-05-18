using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Micube.SmartMES.ToolManagement.Popup;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;

namespace Micube.SmartMES.ToolManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 치공구 제품코드 사용자 컨트롤 
    /// 업  무  설  명  : 치공구 제품코드를 조회하여 규격, 제품명등의 정보를 한번에 조회하고자 사용 ProductCodePopup을 사용한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-07-12
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ucProductCodePopup : System.Windows.Forms.UserControl
    {
        
        public delegate void showMessageEvent(string messageCode);             //현재재고 조회를 위한 델리게이트
        public event showMessageEvent msgHandler;                               //현재재고 조회를 위한 이벤트
        
        #region public Variables
        /// <summary>
        /// 제품코드 프로퍼티
        /// </summary>
        public string ProductDefinitionCode
        {
            get
            {
                if (txtCode.EditValue != null)
                    return txtCode.EditValue.ToString();
                else
                    return "";
            }
            set
            {
                txtCode.EditValue = value;
            }
        }

        /// <summary>
        /// 제품명 프로퍼티
        /// </summary>
        public string ProductDefinitionVersion
        {
            get
            {
                if (txtVersion.EditValue != null)
                    return txtVersion.EditValue.ToString();
                else
                    return "";
            }
            set
            {
                txtVersion.EditValue = value;
            }
        }

        /// <summary>
        /// 검색버튼의 ReadOnly 속성을 제어한다.
        /// </summary>
        public bool ReadOnly
        {
            get
            {
                return btnSearch.Enabled == true ? false : true;
            }

            set
            {
                txtCode.ReadOnly = value;
                //ReadOnly와 Enabled는 반대로 설정 ReadOnly = true 가 Enabled = false 와 같은 설정
                btnSearch.Enabled = value==true?false:true;
            }
        }

        /// <summary>
        /// 화면바인딩후에는 곧바로 Search하지 않도록 한다.
        /// </summary>
        public bool IsSearch
        {
            get
            {
                return _isSearch;
            }

            set
            {
                _isSearch = value;
            }
        }

        public string PlantID
        {
            get { return _plantID; }
            set { _plantID = value; }
        }

        /// <summary>
        /// 부모창의 컨트롤중 값을 대입할 컨트롤을 저장한다.
        /// </summary>
        /// <param name="tempDurableDeptNameTextBox">품목명</param>
        public void SetSmartTextBoxForSearchData(SmartTextBox tempDurableDeptNameTextBox, SmartBandedGrid tempToolReqDetailGrid, string inputFieldName)
        {
            _tempDurableDeptNameTextBox = tempDurableDeptNameTextBox;
            _tempToolReqDetailGrid = tempToolReqDetailGrid;
            _inputFieldName = inputFieldName;
        }
        #endregion

        #region Local Variables        
        SmartTextBox _tempDurableDeptNameTextBox;           //부모창의 입력컨트롤과 매핑되는 텍스트 박스
        SmartBandedGrid _tempToolReqDetailGrid;
        string _inputFieldName;
        private bool _isSearch = true;                     //자동재검색을 막기위한 1차변수
        private string _plantID;
        #endregion

        #region 생성자
        /// <summary>
        /// 생성자
        /// </summary>
        public ucProductCodePopup()
        {
            InitializeComponent();
            txtVersion.ReadOnly = true;

            btnSearch.Click += BtnSearch_Click;
            txtCode.KeyPress += TxtCode_KeyPress;
            //txtCode.Leave += TxtCode_Leave;
            txtCode.TextChanged += TxtCode_TextChanged;
        }

        private void TxtCode_TextChanged(object sender, EventArgs e)
        {
            if (_isSearch)
                _isSearch = false;
        }
        #endregion

        #region event

        #region TxtCode_Leave - 코드입력 텍스트박스에서 포커스가 떠나갈 때를 처리
        /// <summary>
        /// 코드입력 텍스트박스에서 포커스가 떠나갈 때를 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtCode_Leave(object sender, EventArgs e)
        {
            if (txtCode.Text != "" && !_isSearch)
                SearchItem();
        }
        #endregion

        #region TxtCode_KeyPress - 코드입력 텍스트박스에 키가 눌러졌을 때를 처리
        /// <summary>
        /// 코드입력 텍스트박스에 키가 눌러졌을 때를 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //엔터키 입력시 동작
            if (e.KeyChar == 13)
                SearchItem();

            if (_isSearch)
                _isSearch = false;
        }
        #endregion

        #region BtnSearch_Click - 검색이벤트
        /// <summary>
        /// 검색이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            PopupShow(null);
        }
        #endregion
        #endregion

        #region ItemPopup_write_handler - 팝업창에서 데이터를 입력하기 위해 호출하는 델리게이트(이벤트)메소드
        /// <summary>
        /// 팝업창에서 데이터를 입력하기 위해 호출하는 델리게이트(이벤트)메소드
        /// </summary>
        /// <param name="row"></param>
        private void ItemPopup_write_handler(DataRow row)
        {
            //선택입력이므로 더이상 검색하지 않는다.
            _isSearch = true;
            if (row != null)
            {
                _tempDurableDeptNameTextBox.EditValue = row["PRODUCTDEFNAME"];
                txtCode.EditValue = row["PRODUCTDEFID"];
                txtVersion.EditValue = row["PRODUCTDEFVERSION"];

                if (_tempToolReqDetailGrid != null)
                {
                    //전체 행의 특정 셀에 대한 데이터 입력
                    for (int i = 0; i < _tempToolReqDetailGrid.View.RowCount; i++)
                        _tempToolReqDetailGrid.View.SetRowCellValue(i, _inputFieldName, row["PRODUCTDEFID"]);
                }
            }
        }
        #endregion

        #region PopupShow - 제품조회를 위한 팝업창을 호출한다.
        /// <summary>
        /// 제품조회를 위한 팝업창을 호출한다.
        /// </summary>
        public void PopupShow(DataTable searchResult)
        {
            //현재 매개변수를 전달할 필요가 없으나 향후 필요할 때 활용
            //string sItemCode = CODE.Text;
            //string sItemVer = VERSION.Text;
            //string sItemName = NAME.Text;

            ProductCodePopup productPopup = new ProductCodePopup(PlantID);
            productPopup.write_handler += ItemPopup_write_handler;

            // 검색조건을 입력하였다면
            productPopup.ProductDeptCode = txtCode.Text;

            if (searchResult != null)
                productPopup.SearchResult = searchResult;

            productPopup.ShowDialog();            
        }
        #endregion

        #region SearchItem - 입력받은 코드로 데이터를 검색해 1개의 행일 경우 바로 데이터를 기입하고 2개 이상이거나 0개 일때는 팝업창을 오픈한다.
        /// <summary>
        /// 입력받은 코드로 데이터를 검색해 1개의 행일 경우 바로 데이터를 기입하고 2개 이상이거나 0개 일때는 팝업창을 오픈한다.
        /// </summary>
        private void SearchItem()
        {
            Dictionary<string, object> Param = new Dictionary<string, object>();

            Param.Add("PRODUCTDEFID", txtCode.Text);
            Param.Add("PLANTID", PlantID);
            Param.Add("PRODUCTDEFTYPE", "Product");
            Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            Param = Commons.CommonFunction.ConvertParameter(Param);            
            DataTable dt = SqlExecuter.Query("GetProductdefidPoplistByTool", "10001", Param);
            //두번 쿼리하지 않게 제어
            _isSearch = true;

            if (dt != null)
            {
                //RowCount가 1일 경우에만 동작
                if (dt.Rows.Count == 1)
                {
                    ItemPopup_write_handler(dt.Rows[0]);
                }
                else //값이 여러개이거나 0개일 경우 팝업창을 띄워서 처리
                {
                    //msgHandler("PRODUCTCODEVALIDATION"); // 입력하신 SparePart가 존재하지 않습니다. 다시 입력 해주세요.
                    PopupShow(dt);
                }
            }
        }
        #endregion
    }
}
