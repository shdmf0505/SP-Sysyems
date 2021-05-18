using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Micube.SmartMES.EquipmentManagement.Popup;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;

namespace Micube.SmartMES.EquipmentManagement
{
    public partial class ucDurableCodePopup : UserControl
    {
        /// <summary>
        /// 프 로 그 램 명  : 부품코드 사용자 컨트롤 
        /// 업  무  설  명  : 부품코드를 조회하여 규격, 부품명, 적정재고등의 정보를 한번에 조회하고자 사용 DurableCodePopup을 사용한다.
        /// 생    성    자  : 김용조
        /// 생    성    일  : 2019-07-04
        /// 수  정  이  력  : 
        /// 
        /// 
        /// </summary>

        #region public Variables
        public delegate void showMessageEvent(string messageCode);             //현재재고 조회를 위한 델리게이트
        public event showMessageEvent msgHandler;            //현재재고 조회를 위한 이벤트
        private bool _isSearch = false;                     //자동재검색을 막기위한 1차변수        
        private bool _isSelected = false;
        private string _plantID;
        public string DurableDefinitionCode
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

        public string PlantID
        {
            get { return _plantID; }
            set { _plantID = value; }
        }

        public string DurableDefinitionVersion
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

        public bool IsSearch
        {
            get { return _isSearch; }
            set { _isSearch = value; }
        }

        #region 데이터를 기입할 컨트롤의 초기화
        /// <summary>
        /// 검색된 값의 중분류, 소분류, 규격, 적정재고를 입력하기 위해 부모창의
        /// 각각 해당하는 컨트롤을 전역변수에 할당한다.
        /// </summary>
        /// <param name="tempParentDurableClassCodeTextBox"></param>
        /// <param name="tempParentDUrableClassNameTextBox"></param>
        /// <param name="tempDurableClassCodeTextBox"></param>
        /// <param name="tempDurableClassNameTextBox"></param>
        /// <param name="tempSpecTextBox"></param>
        /// <param name="tempSafetyStockTextBox"></param>
        public void SetSmartTextBoxForSearchData(SmartTextBox tempParentDurableClassCodeTextBox, SmartTextBox tempParentDUrableClassNameTextBox
            , SmartTextBox tempDurableClassCodeTextBox, SmartTextBox tempDurableClassNameTextBox, SmartTextBox tempSpecTextBox, SmartTextBox tempSafetyStockTextBox
            , SmartTextBox tempDurableDefinitionNameTextBox, SmartTextBox tempPriceTextBox, SmartTextBox tempModelNameTextBox, SmartPictureEdit tempImage)
        {
            _tempParentDurableClassCodeTextBox = tempParentDurableClassCodeTextBox;
            _tempParentDurableClassNameTextBox = tempParentDUrableClassNameTextBox;
            _tempDurableClassCodeTextBox = tempDurableClassCodeTextBox;
            _tempDurableClassNameTextBox = tempDurableClassNameTextBox;
            _tempSpecTextBox = tempSpecTextBox;
            _tempSafetyStockTextBox = tempSafetyStockTextBox;
            _tempDurableDefinitionNameTextBox = tempDurableDefinitionNameTextBox;
            _tempPriceTextBox = tempPriceTextBox;
            _tempModelNameTextBox = tempModelNameTextBox;
            _tempImage = tempImage;
        }
        #endregion
        #endregion

        #region Local Variables
        SmartTextBox _tempParentDurableClassCodeTextBox;
        SmartTextBox _tempParentDurableClassNameTextBox;
        SmartTextBox _tempDurableClassCodeTextBox;
        SmartTextBox _tempDurableClassNameTextBox;
        SmartTextBox _tempSpecTextBox;
        SmartTextBox _tempSafetyStockTextBox;
        SmartTextBox _tempDurableDefinitionNameTextBox;
        SmartTextBox _tempPriceTextBox;
        SmartTextBox _tempModelNameTextBox;
        SmartPictureEdit _tempImage;
        #endregion

        public ucDurableCodePopup()
        {
            InitializeComponent();
            btnSearch.Click += BtnSearch_Click;
            txtCode.KeyPress += TxtCode_KeyPress;
            txtCode.Leave += TxtCode_Leave;
            txtCode.TextChanged += TxtCode_TextChanged;
            
            txtVersion.ReadOnly = true;
        }

        private void TxtCode_TextChanged(object sender, EventArgs e)
        {
            if (_isSearch)
                _isSearch = false;
        }

        private void TxtCode_Leave(object sender, EventArgs e)
        {
            if (txtCode.Text != "" && !_isSearch)
                SearchItem();
        }

        private void TxtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //엔터키 입력시 동작
            if (e.KeyChar == 13)
                SearchItem();

            if (_isSearch)
                _isSearch = false;
        }

        #region btnSearch_Click - 조회버튼 클릭 이벤트
        /// <summary>
        /// 조회버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            PopupShow(null);
        }
        #endregion

        #region ItemPopup_write_handler - 팝업창에서 호출하여 데이터를 입력하는 메소드(이벤트)
        /// <summary>
        /// 팝업창에서 호출하여 데이터를 입력하는 메소드(이벤트)
        /// </summary>
        /// <param name="row"></param>
        private void ItemPopup_write_handler(DataRow row, Image imageData)
        {
            //선택입력이므로 더이상 검색하지 않는다.
            _isSearch = true;

            //_tempParentDurableClassCodeTextBox.EditValue = row["PARENTDURABLECLASSID"];
            //_tempParentDurableClassNameTextBox.EditValue = row["PARENTDURABLECLASSNAME"];
            //_tempDurableClassCodeTextBox.EditValue = row["DURABLECLASSID"];
            //_tempDurableClassNameTextBox.EditValue = row["DURABLECLASSNAME"];
            _tempSpecTextBox.EditValue = row["SPEC"];
            _tempSafetyStockTextBox.EditValue = row["SAFETYSTOCK"];
            _tempDurableDefinitionNameTextBox.EditValue = row["SPAREPARTNAME"];
            _tempPriceTextBox.EditValue = row["PRICE"];
            _tempModelNameTextBox.EditValue = row["MODELNAME"];
            txtCode.EditValue = row["SPAREPARTID"];
            txtVersion.EditValue = row["SPAREPARTVERSION"];

            _tempImage.Image = imageData;
        }
        #endregion

        #region PopupShow - 팝업창을 오픈한다.
        /// <summary>
        /// 팝업창을 오픈한다.
        /// </summary>
        public void PopupShow(DataTable searchResult)
        {
            //현재 매개변수를 전달할 필요가 없으나 향후 필요할 때 활용
            //string sItemCode = CODE.Text;
            //string sItemVer = VERSION.Text;
            //string sItemName = NAME.Text;

            DurableCodePopup durablePopup = new DurableCodePopup(_plantID);
            durablePopup.write_handler += ItemPopup_write_handler;

            //검색조건을 입력하였다면
            durablePopup.DurableDeptCode = txtCode.Text;

            if(searchResult != null)
                durablePopup.SearchResult = searchResult;

            durablePopup.ShowDialog();            
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
            Param.Add("DURABLEDEFVERSION", txtVersion.Text);
            Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            //Param.Add("PLANTID", _plantID);
            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dt = SqlExecuter.Query("GetDurableDefinitionListByEqp", "10001", Param);
            //두번 쿼리하지 않게 제어
            _isSearch = true;
            if (dt != null)
            {
                //RowCount가 1일 경우에만 동작
                if (dt.Rows.Count == 1)
                {
                    ItemPopup_write_handler(dt.Rows[0], DisplayImage(txtCode.Text, txtVersion.Text));
                }
                else //값이 여러개이거나 0개일 경우 팝업창을 띄워서 처리
                {
                    PopupShow(dt);
                   // msgHandler("SparePartCodeValidation"); // 입력하신 SparePart가 존재하지 않습니다. 다시 입력 해주세요.
                }
            }
        }
        #endregion

        #region DisplayImage - 서버로부터 이미지를 가져와서 출력
        private Image DisplayImage(string itemID, string itemVersion)
        {

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("ITEMID", itemID);
            values.Add("ITEMVERSION", itemVersion);
            #endregion

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable sparePartSearchResult = SqlExecuter.Query("GetSparePartImageByEqp", "10001", values);

            if (sparePartSearchResult.Rows.Count > 0)
            {
                if (sparePartSearchResult.Rows[0]["IMAGE"] != null && !Format.GetString(sparePartSearchResult.Rows[0]["IMAGE"]).Equals(string.Empty))
                {
                    //speImage.BringToFront();
                    return (Bitmap)new ImageConverter().ConvertFrom(sparePartSearchResult.Rows[0]["IMAGE"]);
                }
                else
                {
                    //speImage.SendToBack();
                    return null;
                }
            }
            else
            {
                //speImage.SendToBack();
                return null;
            }
        }
        #endregion
    }
}
