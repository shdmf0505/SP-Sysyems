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
    /// 프 로 그 램 명  : 필름 제품코드 사용자 컨트롤 
    /// 업  무  설  명  : 필름 제품코드를 조회하여 필름코드, 공정버전등의 정보를 한번에 조회하고자 사용 FilmCodePopup을 사용한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-08-19
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ucFilmCodePopup : System.Windows.Forms.UserControl
    {
        #region Local Variables
        public delegate void showMessageEvent(string messageCode);             //현재재고 조회를 위한 델리게이트
        public event showMessageEvent msgHandler;                               //현재재고 조회를 위한 이벤트
        string _inputFiledName;
        bool _isSearch = true;
        string _plantID;
        SmartTextBox _tempProductDefID;
        SmartTextBox _tempProductDefName;
        SmartTextBox _tempFilmType;
        SmartTextBox _tempFilmCategory;
        SmartTextBox _tempFilmDetailCategory;
        SmartTextBox _tempLayer;
        SmartTextBox _tempJobType;
        SmartTextBox _tempProcessSegment;
        #endregion

        #region 생성자
        public ucFilmCodePopup()
        {
            InitializeComponent();

            txtVersion.ReadOnly = true;

            btnSearch.Click += BtnSearch_Click;
            txtCode.KeyPress += TxtCode_KeyPress;
            txtCode.Leave += TxtCode_Leave;
            txtCode.TextChanged += TxtCode_TextChanged;
        }
        #endregion

        #region public Variables

        #region FilmCode - 필름코드 Property
        public string FilmCode
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
        #endregion

        #region FilmVersion - 필름버전 Property
        public string FilmVersion
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
        #endregion

        #region ReadOnly - 읽기여부 Property
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
                btnSearch.Enabled = value == true ? false : true;
            }
        }
        #endregion

        #region IsSearch - 검색여부 Property
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
        #endregion

        #region PlantID - Site아이디 Property
        public string PlantID
        {
            set { _plantID = value; }
        }
        #endregion

        #region SetSmartTextBoxForSearchData - 선택된 데이터를 할당받을 텍스트박스의 초기화
        public void SetSmartTextBoxForSearchData(SmartTextBox tempProductDefID, SmartTextBox tempProductDefName
            , SmartTextBox tempFilmType, SmartTextBox tempFilmCategory, SmartTextBox tempFilmDetailCategory, SmartTextBox tempLayer
            , SmartTextBox tempJobType, SmartTextBox tempProcessSegment, string inputFieldName)
        {
            _tempProductDefID = tempProductDefID;
            _tempProductDefName = tempProductDefName;
            _tempFilmType = tempFilmType;
            _tempFilmCategory = tempFilmCategory;
            _tempFilmDetailCategory = tempFilmDetailCategory;
            _tempLayer = tempLayer;
            _tempJobType = tempJobType;
            _tempProcessSegment = tempProcessSegment;
            _inputFiledName = inputFieldName;
        }
        #endregion
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

        private void TxtCode_TextChanged(object sender, EventArgs e)
        {
            if (_isSearch)
                _isSearch = false;
        }
        #endregion
        #endregion

        #region ItemPopup_write_handler - 팝업창에서 데이터를 입력하기 위해 호출하는 델리게이트(이벤트)메소드
        /// <summary>
        /// 팝업창에서 데이터를 입력하기 위해 호출하는 델리게이트(이벤트)메소드
        /// </summary>
        /// <param name="row"></param>
        private void LoadDataHandler(DataRow row)
        {
            //선택입력이므로 더이상 검색하지 않는다.
            _isSearch = true;

            _tempProductDefID.EditValue = row.GetString("PRODUCTDEFID");
            _tempProductDefName.EditValue = row.GetString("PRODUCTDEFNAME");
            _tempFilmType.EditValue = row.GetString("FILMTYPE");
            _tempFilmCategory.EditValue = row.GetString("FILMCATEGORY");
            _tempFilmDetailCategory.EditValue = row.GetString("FILMDETAILCATEGORY");
            _tempLayer.EditValue = row.GetString("FILMUSELAYER");
            _tempJobType.EditValue = row.GetString("JOBTYPE");
            _tempProcessSegment.EditValue = row.GetString("PROCESSSEGMENTVERSION");
            txtCode.EditValue = row["FILMCODE"];
            txtVersion.EditValue = row["FILMVERSION"];            
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

            FilmCodePopup filmPopup = new FilmCodePopup(_plantID);
            filmPopup.loadDataHandler += LoadDataHandler;

            // 검색조건을 입력하였다면
            filmPopup.FilmCode = txtCode.Text;

            if (searchResult != null)
                filmPopup.SearchResult = searchResult;

            filmPopup.ShowDialog();
        }
        #endregion

        #region SearchItem - 입력받은 코드로 데이터를 검색해 1개의 행일 경우 바로 데이터를 기입하고 2개 이상이거나 0개 일때는 팝업창을 오픈한다.
        /// <summary>
        /// 입력받은 코드로 데이터를 검색해 1개의 행일 경우 바로 데이터를 기입하고 2개 이상이거나 0개 일때는 팝업창을 오픈한다.
        /// </summary>
        private void SearchItem()
        {
            Dictionary<string, object> Param = new Dictionary<string, object>();

            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            Param.Add("DURABLEDEFID", txtCode.Text);
            Param.Add("CURRENTLOGINID", UserInfo.Current.Id);
            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dt = SqlExecuter.Query("GetFilmCodePopupListByTool", "10001", Param);
            //두번 쿼리하지 않게 제어
            _isSearch = true;

            if (dt != null)
            {
                //RowCount가 1일 경우에만 동작
                if (dt.Rows.Count == 1)
                {
                    LoadDataHandler(dt.Rows[0]);
                }
                else //값이 여러개이거나 0개일 경우 팝업창을 띄워서 처리
                {
                    //msgHandler("FILMCODEVALIDATION"); // 입력하신 SparePart가 존재하지 않습니다. 다시 입력 해주세요.
                    //팝업창 오픈
                    PopupShow(dt);
                }
            }
        }
        #endregion
    }
}
