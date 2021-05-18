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
    /// <summary>
    /// 프 로 그 램 명  : 부품코드 사용자 컨트롤 (현재재고를 보여주는 버전, SparePart이동에 사용)
    /// 업  무  설  명  : 부품코드를 조회하여 규격, 부품명, 적정재고등의 정보를 한번에 조회하고자 사용 DurableCodeCurrentQtyPopup을 사용한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-07-12
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ucDurableCodeCurrentQtyPopup : UserControl
    {
        #region public Variables
        public delegate void currentQtyEvent();                             //현재재고 조회를 위한 델리게이트
        public event currentQtyEvent qtyHandler;                            //현재재고 조회를 위한 이벤트
        public delegate void showMessageEvent(string messageCode);          //메세지박스를 호출하는 델리게이트
        public event showMessageEvent msgHandler;                           //메세지박스를 호출하는 이벤트
        private bool _isSearch = false;                                     //두번 쿼리하지 않도록 제어하는 변수        
        private string _factoryID;
        DurableCodeCurrentQtyPopup durablePopup;                            //팝업화면을 전역으로 가지고 있는다.
        private string _plantID;

        public string PlantID
        {
            get { return _plantID; }
            set { _plantID = value; }
        }
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

        public string FactoryID
        {
            get { return _factoryID; }
            set
            {
                if (durablePopup != null)
                    durablePopup.FactoryID = value;

                _factoryID = value;
            }
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
                //ReadOnly와 Enabled는 반대로 설정 ReadOnly = true 가 Enabled = false 와 같은 설정
                txtCode.ReadOnly = value;
                btnSearch.Enabled = value == true ? false : true;
            }
        }

        public bool IsSearch
        {
            get { return _isSearch; }
            set { _isSearch = value; }
        }

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
            , SmartTextBox tempDurableDefinitionNameTextBox, SmartTextBox tempModelNameTextBox, SmartPictureEdit tempImageEdit)
        {
            _tempParentDurableClassCodeTextBox = tempParentDurableClassCodeTextBox;
            _tempParentDurableClassNameTextBox = tempParentDUrableClassNameTextBox;
            _tempDurableClassCodeTextBox = tempDurableClassCodeTextBox;
            _tempDurableClassNameTextBox = tempDurableClassNameTextBox;
            _tempSpecTextBox = tempSpecTextBox;
            _tempSafetyStockTextBox = tempSafetyStockTextBox;
            _tempDurableDefinitionNameTextBox = tempDurableDefinitionNameTextBox;
            _tempModelNameTextBox = tempModelNameTextBox;
            _tempImageEdit = tempImageEdit;
        }
        #endregion

        #region Local Variables
        SmartTextBox _tempParentDurableClassCodeTextBox;
        SmartTextBox _tempParentDurableClassNameTextBox;
        SmartTextBox _tempDurableClassCodeTextBox;
        SmartTextBox _tempDurableClassNameTextBox;
        SmartTextBox _tempSpecTextBox;
        SmartTextBox _tempSafetyStockTextBox;
        SmartTextBox _tempDurableDefinitionNameTextBox;
        SmartTextBox _tempModelNameTextBox;
        SmartPictureEdit _tempImageEdit;
        #endregion

        public ucDurableCodeCurrentQtyPopup()
        {
            InitializeComponent();
            btnSearch.Click += BtnSearch_Click;
            txtCode.TextChanged += TxtCode_TextChanged;
            txtCode.Leave += TxtCode_Leave;
            txtCode.KeyPress += TxtCode_KeyPress;

            txtVersion.ReadOnly = true;
        }

        private void TxtCode_TextChanged(object sender, EventArgs e)
        {
            if (_isSearch)
                _isSearch = false; 
        }

        private void TxtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (_isSearch)
                _isSearch = false;

            if (e.KeyChar == 13)
                SearchItem();
        }

        private void TxtCode_Leave(object sender, EventArgs e)
        {
                if (txtCode.Text != "" && !_isSearch)
                SearchItem();
        }
        

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            PopupShow(null);
        }

        private void ItemPopup_write_handler(DataRow row, Image imageData)
        {
            _isSearch = true;

            //_tempParentDurableClassCodeTextBox.EditValue = row["PARENTDURABLECLASSID"].ToString();
            //_tempParentDurableClassNameTextBox.EditValue = row["PARENTDURABLECLASSNAME"].ToString();
            //_tempDurableClassCodeTextBox.EditValue = row["DURABLECLASSID"].ToString();
            //_tempDurableClassNameTextBox.EditValue = row["DURABLECLASSNAME"].ToString();
            _tempSpecTextBox.EditValue = row["SPEC"].ToString();
            _tempSafetyStockTextBox.EditValue = row["SAFETYSTOCK"].ToString();
            _tempDurableDefinitionNameTextBox.EditValue = row["SPAREPARTNAME"].ToString();
            _tempModelNameTextBox.EditValue = row["MODELNAME"].ToString();
            txtCode.EditValue = row["SPAREPARTID"].ToString();
            txtVersion.EditValue = row["SPAREPARTVERSION"].ToString();

            _tempImageEdit.Image = imageData;

            qtyHandler();
        }

        public void PopupShow(DataTable dt)
        {
            //현재 매개변수를 전달할 필요가 없으나 향후 필요할 때 활용
            //string sItemCode = CODE.Text;
            //string sItemVer = VERSION.Text;
            //string sItemName = NAME.Text;

            durablePopup = new DurableCodeCurrentQtyPopup();
            durablePopup.write_handler += ItemPopup_write_handler;

            //검색조건을 입력하였다면
            durablePopup.DurableDeptCode = txtCode.Text;
            durablePopup.FactoryID = _factoryID;

            if (dt != null)
                durablePopup.SearchResult = dt;

            durablePopup.ShowDialog();
        }

        #region SearchItem - 입력받은 코드로 데이터를 검색해 1개의 행일 경우 바로 데이터를 기입하고 2개 이상이거나 0개 일때는 팝업창을 오픈한다.
        /// <summary>
        /// 입력받은 코드로 데이터를 검색해 1개의 행일 경우 바로 데이터를 기입하고 2개 이상이거나 0개 일때는 팝업창을 오픈한다.
        /// </summary>
        private void SearchItem()
        {
            Dictionary<string, object> Param = new Dictionary<string, object>();

            Param.Add("DURABLEDEFID", txtCode.Text);
            Param.Add("FACTORYID", _factoryID);

            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dt = SqlExecuter.Query("GetDurableDefCurrentQtyListByEqp", "10001", Param);
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
                    msgHandler("SparePartCodeValidation"); // 입력하신 SparePart가 존재하지 않습니다. 다시 입력 해주세요.
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
