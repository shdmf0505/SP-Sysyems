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
    /// 프 로 그 램 명  : 부품코드정보 팝업 (현재재고를 표시하는 버전)
    /// 업  무  설  명  : Spare Part 이동등록을 위해 부품코드를 조회할 때 사용
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-07-12
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class DurableCodeCurrentQtyPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Varialbes and Properties
        public DataRow CurrentDataRow { get; set; }

        #region FactoryID - 공장아이디 Property
        public string FactoryID
        {
            get { return _sFactoryID; }
            set { _sFactoryID = value; }
        }
        #endregion

        #region SearchResult - 검색결과 Property
        public DataTable SearchResult
        {
            set { grdDurableCodeList.DataSource = value; }
        }
        #endregion

        #region DurableDeptCode - Durable Definition Code Property
        public string DurableDeptCode
        {
            get {return txtItemCode.Text; }
            set { txtItemCode.Text = value; }
        }
        #endregion

        #region DurableCodeCurrentQtyPopup - 
        public DurableCodeCurrentQtyPopup(string factoryID) : this()
        {
            _sFactoryID = factoryID;
        }
        #endregion
        #endregion

        #region Local Variables
        string _sPlantid = "";
        string _sFactoryID = "";
        /// <summary>
        ///  선택한 부품 list를 보내기 위한 Handler
        /// </summary>
        /// <param name="dt"></param>
        public delegate void chat_room_evecnt_handler(DataRow row, Image imageData);
        public event chat_room_evecnt_handler write_handler;
        #endregion

        #region 생성자        
        public DurableCodeCurrentQtyPopup()
        {
            InitializeComponent();

            InitializeEvent();
           
            InitializeCondition();

            _sPlantid =  UserInfo.Current.Plant.ToString();
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

           // InitializeFactoryComboBox(UserInfo.Current.Plant, _sFactoryID);
        }

        #region InitializeFactoryComboBox - 콤보박스초기화
        //private void InitializeFactoryComboBox(string plantID, string factoryID)
        //{
        //    // 검색조건에 정의된 공장을 정리
        //    cboFactory.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
        //    cboFactory.ValueMember = "FACTORYID";
        //    cboFactory.DisplayMember = "FACTORYNAME";
        //    cboFactory.EditValue = "1";

        //    if (plantID.Equals(""))
        //    {
        //        cboFactory.DataSource = SqlExecuter.Query("GetFactoryListByEqp", "10001"
        //         , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
        //    }
        //    else
        //    {
        //        cboFactory.DataSource = SqlExecuter.Query("GetFactoryListByEqp", "10001"
        //         , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "PLANTID", plantID } });
        //    }

        //    cboFactory.ShowHeader = false;
        //    cboFactory.ReadOnly = true;

        //    if (factoryID != null && factoryID != "")
        //    {
        //        cboFactory.EditValue = factoryID;
        //    }            
        //}
        #endregion
        #endregion

        #region  이벤트
        private void InitializeEvent()
        {
            btnSearch.Click += BtnSearch_Click;
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;
            grdDurableCodeList.View.DoubleClick += grdProductItem_DoubleClick;
            grdDurableCodeList.View.FocusedRowChanged += grdDurableCodeList_FocusedRowChanged;

            txtItemCode.KeyPress += TxtItemCode_KeyPress;
            txtItemNm.KeyPress += TxtItemNm_KeyPress;
            txtmodelname.KeyPress += TxtItemVer_KeyPress;
        }

        #region grdDurableCodeList_FocusedRowChanged - 그리드의 행변경이벤트
        private void grdDurableCodeList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayImage(e.FocusedRowHandle);
        }
        #endregion

        #region TxtItemVer_KeyPress - 버전 텍스트박스의 키입력이벤트
        private void TxtItemVer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                Search(txtItemNm.Text, txtItemCode.Text, txtmodelname.Text, _sFactoryID);
        }
        #endregion

        #region TxtItemNm_KeyPress - 명칭 텍스트박스의 키입력이벤트
        private void TxtItemNm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                Search(txtItemNm.Text, txtItemCode.Text, txtmodelname.Text, _sFactoryID); 
        }
        #endregion

        #region TxtItemCode_KeyPress - 코드 텍스트박스의 키입력이벤트
        private void TxtItemCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                Search(txtItemNm.Text, txtItemCode.Text, txtmodelname.Text, _sFactoryID);
        }
        #endregion

        #region BtnConfirm_Click - 확인버튼 클릭이벤트
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            DataRow row = grdDurableCodeList.View.GetFocusedDataRow();
            CurrentDataRow = row;
            write_handler(row, speImage.Image);
            this.Close();
        }
        #endregion

        #region grdProductItem_DoubleClick - 그리드 더블클릭이벤트
        private void grdProductItem_DoubleClick(object sender, EventArgs e)
        {
            DataRow row = grdDurableCodeList.View.GetFocusedDataRow();
            CurrentDataRow = row;
            write_handler(row, speImage.Image);
            this.Close();
        }
        #endregion

        #region BtnSearch_Click - 검색버튼클릭이벤트
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Search( txtItemNm.Text, txtItemCode.Text, txtmodelname.Text, _sFactoryID);
        }
        #endregion

        #region Search - 검색
        void Search( string durableDefName, string durableDefID, string txtmodelname, string factoryID)
        {
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());
            //PLANTID를 입력할 시 데이터가 나오지 않으므로 현재 주석 처리
            Param.Add("PLANTID", _sPlantid);      
            Param.Add("DURABLEDEFNAME", durableDefName);
            Param.Add("DURABLEDEFID", durableDefID);
            Param.Add("MODELNAME", txtmodelname);
            Param.Add("FACTORYID", factoryID);
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dt = SqlExecuter.Query("GetDurableDefCurrentQtyListByEqp", "10001", Param);
            
            grdDurableCodeList.DataSource = dt;
        }
        #endregion

        #region BtnCancel_Click - 취소버튼클릭이벤트
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #endregion

        #region InitializeGridIdDefinitionManagement - 그리드초기화
        private void InitializeGridIdDefinitionManagement()
        {
            // GRID 초기화
            grdDurableCodeList.GridButtonItem = GridButtonItem.None;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
                                                                   //grdProductItemSpec.View.AddTextBoxColumn("ENTERPIRSEID").SetIsHidden();
            grdDurableCodeList.View.AddTextBoxColumn("SPAREPARTID", 100).SetIsReadOnly();
            grdDurableCodeList.View.AddTextBoxColumn("SPAREPARTVERSION", 50).SetIsReadOnly();            
            grdDurableCodeList.View.AddTextBoxColumn("SPAREPARTNAME", 250).SetIsReadOnly();
            grdDurableCodeList.View.AddTextBoxColumn("SAFETYSTOCK", 80).SetIsReadOnly();
            grdDurableCodeList.View.AddTextBoxColumn("CURRENTQTY", 80).SetIsReadOnly();
            grdDurableCodeList.View.AddTextBoxColumn("SPEC", 80).SetIsReadOnly();
            grdDurableCodeList.View.AddTextBoxColumn("MODELNAME", 150).SetIsReadOnly();
            //grdDurableCodeList.View.AddTextBoxColumn("PARENTDURABLECLASSID").SetIsHidden();
            //grdDurableCodeList.View.AddTextBoxColumn("PARENTDURABLECLASSNAME", 200)
            //    .SetTextAlignment(TextAlignment.Center)
            //    .SetIsReadOnly();
            //grdDurableCodeList.View.AddTextBoxColumn("DURABLECLASSID").SetIsHidden();
            //grdDurableCodeList.View.AddTextBoxColumn("DURABLECLASSNAME", 150)
            //    .SetTextAlignment(TextAlignment.Center)
            //    .SetIsReadOnly();
            //grdDurableCodeList.View.AddTextBoxColumn("FACTORYID").SetIsHidden();
            //grdDurableCodeList.View.AddTextBoxColumn("FACTORYNAME", 130).SetIsReadOnly();
            grdDurableCodeList.View.AddTextBoxColumn("DESCRIPTION", 150).SetIsReadOnly();
            //grdDurableCodeList.View.SetAutoFillColumn("PRODUCTDEFNAME");
            grdDurableCodeList.View.PopulateColumns();
        }
        #endregion

        #region DisplayImage - 서버로부터 이미지를 가져와서 출력
        private void DisplayImage(int rowHandle)
        {
            DataRow currentRow = grdDurableCodeList.View.GetDataRow(rowHandle);

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("ITEMID", currentRow.GetString("SPAREPARTID"));
            values.Add("ITEMVERSION", currentRow.GetString("SPAREPARTVERSION"));
            #endregion

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable sparePartSearchResult = SqlExecuter.Query("GetSparePartImageByEqp", "10001", values);

            if (sparePartSearchResult.Rows.Count > 0)
            {
                if (sparePartSearchResult.Rows[0]["IMAGE"] != null && !Format.GetString(sparePartSearchResult.Rows[0]["IMAGE"]).Equals(string.Empty))
                {
                    //speImage.BringToFront();
                    speImage.Image = (Bitmap)new ImageConverter().ConvertFrom(sparePartSearchResult.Rows[0]["IMAGE"]);
                }
                else
                {
                    //speImage.SendToBack();
                    speImage.Image = null;
                }
            }
            else
            {
                //speImage.SendToBack();
                speImage.Image = null;
            }
        }
        #endregion
    }
}
