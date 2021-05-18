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
    /// 프 로 그 램 명  : lot no 선택 팝업
    /// 업  무  설  명  : 외주창고 출고 lot no 선택시 팝업
    /// 생    성    자  : 최별
    /// 생    성    일  : 2019-07-09
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcedWarehouseShipmentPopup : SmartPopupBaseForm
    {
        /// 부모 그리드로 데이터를 바인딩 시켜줄 델리게이트
        /// </summary>
        /// <param name="dt"></param>
        public delegate void AffectLotSelectionDataHandler(DataTable dt);
        public event AffectLotSelectionDataHandler AffectLotSelectEvent;

        #region Local Variables
        
        string _sPlantid = "";
        
        #endregion

        #region 생성자
        public OutsourcedWarehouseShipmentPopup()
        {
            InitializeComponent();

            InitializeEvent();

            InitializeCondition();

            _sPlantid = UserInfo.Current.Plant.ToString();

        }
        /// <summary>
        ///  LOT NO 목록 선택(
        /// </summary>
        /// <param name="SProductdefid"></param>
        /// <param name="SProductdefversion"></param>
        /// <param name="SProductdeaname"></param>
        /// <param name="SPlantid"></param>
        public OutsourcedWarehouseShipmentPopup(string SPlantid,string strAreaid , string strAreaname)
        {
            InitializeComponent();

            _sPlantid = SPlantid;
            // SITE ,외주작업장
            InitializeComboBox();

            InitializeEvent();
            selectProcesssegmentidPopup();
            InitializeCondition();

            InitializeGrid();

            selectOspAreaidPopup(SPlantid);
            popupOspAreaid.SetValue(strAreaid);
            popupOspAreaid.EditValue = strAreaname;
            
        }

        #endregion
        #region ComboBox  

        /// <summary>
        /// SmartComboBox 컨트롤 추가
        /// </summary>
        private void InitializeComboBox()
        {

            // SITE 
            cboPlantid.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboPlantid.ValueMember = "PLANTID";
            cboPlantid.DisplayMember = "PLANTID";
            cboPlantid.EditValue = _sPlantid;
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dicParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            //cboPlantid.DataSource = SqlExecuter.Query("GetPlantList", "00001"
            // , new Dictionary<string, object>() { { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
            cboPlantid.DataSource = SqlExecuter.Query("GetPlantList", "00001", dicParam);
            cboPlantid.ShowHeader = false;
            cboPlantid.Enabled = false;

            //// AREA  GetAreaidListByOsp
            //cboAreaid.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            //cboAreaid.ValueMember = "AREAID";
            //cboAreaid.DisplayMember = "AREANAME";

            //cboAreaid.DataSource = SqlExecuter.Query("GetAreaidListByOsp", "10001", new Dictionary<string, object>() { { "P_PLANTID", _sPlantid }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
            //cboAreaid.ShowHeader = false;

        }

        #endregion
        /// <summary>
        /// 공정 선택팝업
        /// </summary>
        private void selectProcesssegmentidPopup()
        {
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();
            popup.SetPopupLayoutForm(500, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel, true, false);
            popup.Id = "PROCESSSEGMENTID";
            popup.LabelText = "PROCESSSEGMENTID";
            popup.SearchQuery = new SqlQuery("GetProcessSegmentListByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                                                                                  , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                                  );
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "PROCESSSEGMENTNAME";

            popup.ValueFieldName = "PROCESSSEGMENTID";
            popup.LanguageKey = "PROCESSSEGMENTID";

            popup.Conditions.AddTextBox("PROCESSSEGMENTNAME")
                .SetLabel("PROCESSSEGMENT"); 

            popup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetLabel("PROCESSSEGMENTID");
            popup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200)
                .SetLabel("PROCESSSEGMENTNAME");

            popupProcesssegmentid.SelectPopupCondition = popup;
        }

        /// <summary>
        /// 작업장 
        /// </summary>
        /// <param name="sPlantid"></param>
        private void selectOspAreaidPopup(string sPlantid)
        {

            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();
            popup.SetPopupLayoutForm(520, 700, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("AREANAME", PopupButtonStyles.Ok_Cancel, true, false);
            popup.Id = "AREANAME";
            popup.LabelText = "AREANAME";
            popup.SearchQuery = new SqlQuery("GetAreaidPopupListByOsp", "10001", $"P_ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                           , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                                                                           , $"P_PLANTID={sPlantid}");
            popup.IsMultiGrid = false;

            popup.DisplayFieldName = "AREANAME";
            popup.ValueFieldName = "AREAID";
            popup.LanguageKey = "AREAID";
           
            popup.Conditions.AddTextBox("P_AREANAME")
                .SetLabel("AREANAME");
            popup.GridColumns.AddTextBoxColumn("AREAID", 120)
                .SetLabel("AREAID");
            popup.GridColumns.AddTextBoxColumn("AREANAME", 200)
                .SetLabel("AREANAME");
           
            popupOspAreaid.SelectPopupCondition = popup;
        }

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
            // 검색
            btnSearch.Click += BtnSearch_Click;
            // 단기
            btnClose.Click += BtnClose_Click;

            btnApply.Click += BtnApply_Click;

        }
        /// <summary>
        /// 적용 클릭시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnApply_Click(object sender, EventArgs e)
        {
            DataTable dtcheck = grdOutsourcedWarehouseShipment.View.GetCheckedRows();

            if (dtcheck.Rows.Count == 0)
            {
                ShowMessage("GridNoChecked"); //(선택된 내역이 없습니다)/
                return;
            }
           

            AffectLotSelectEvent(dtcheck);
            this.Close();
            
        }
        /// <summary>
        /// 닫기 클릭시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        /// <summary>
        /// 조회 클릭 - 메인 grid에 체크 데이터 전달
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {

            SearchExchangeValue();
        }

        /// <summary>
        /// 
        /// </summary>
        private void SearchExchangeValue()
        {

            Dictionary<string, object> Param = new Dictionary<string, object>();
            
            Param.Add("P_ENTERPRISEID", UserInfo.Current.Enterprise.ToString());
            Param.Add("P_PLANTID", _sPlantid);
            Param.Add("P_AREAID", popupOspAreaid.GetValue().ToString()); //외주작업장
            Param.Add("P_PROCESSSEGMENTID", popupProcesssegmentid.GetValue().ToString());  //공정 
            Param.Add("P_PRODUCTDEFID", usrProductdefid.CODE.EditValue);   //품목코드
            Param.Add("P_PRODUCTDEFVERSION", usrProductdefid.VERSION.EditValue);
           
            Param.Add("P_RECEIPTDATEFR", dtpReceiptdateFr.Text);  //입고일자
            if (!(dtpReceiptdateTo.Text.Equals("")))
            {
                DateTime ReceiptdateTo = Convert.ToDateTime(dtpReceiptdateTo.Text);
                //ReceiptdateTo = ReceiptdateTo.AddDays(1);
                Param.Add("P_RECEIPTDATETO", string.Format("{0:yyyy-MM-dd}", ReceiptdateTo));  //입고일자
            }
            Param.Add("P_SHIPOKCHECK", "Y");

            Param.Add("P_LOTID", txtLotid.Text.Trim().ToString());  //lot no 
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dt = SqlExecuter.Query("GetOutsourcedWarehouseShipment", "10001", Param);
            grdOutsourcedWarehouseShipment.DataSource = dt;
            


           
        }

        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가

            grdOutsourcedWarehouseShipment.GridButtonItem =  GridButtonItem.Export;

            grdOutsourcedWarehouseShipment.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("ENTERPRISEID", 120)
                .SetIsHidden();                                                               //  회사 ID
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("PLANTID", 120)
                .SetIsHidden();                                                               //  공장 ID
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("RECEIPTUSER", 120)
                .SetIsHidden();
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("OSPSENDUSER", 120)
                .SetIsHidden();
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("RECEIPTSEQUENCE", 120)
                .SetIsHidden();
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("LOTHISTKEY", 120)
                .SetIsHidden();                                                               //  LOTHISTKEY
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("LOTID", 200)
                .SetIsReadOnly();                                                             //  LOTID
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("RECEIPTDATE", 80)
                .SetDisplayFormat("yyyy-MM-dd")
                .SetIsReadOnly();                                                              // 입고일 
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("RECEIPTUSERNAME", 120)
                .SetIsReadOnly();                                                             // 입고자

            grdOutsourcedWarehouseShipment.View.AddComboBoxColumn("LOTTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=LotType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();                                                             //  양산구분               
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsReadOnly();                                                             //  제품 정의 ID
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly();                                                             //  제품 정의 Version
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("PRODUCTDEFNAME", 200)
                .SetIsReadOnly();                                                             //  제품명
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("USERSEQUENCE", 80)
                .SetIsReadOnly();                                                             //  공정 수순
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("PROCESSSEGMENTID", 120)
                .SetIsHidden();                                                             //  공정 ID
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)
                .SetIsReadOnly();                                                             //  공정명
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("AREAID", 120)
                .SetIsHidden();                                                             //  작업장 AREAID
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("AREANAME", 120)
                .SetIsReadOnly();                                                               //  작업장 AREAID
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("PREVPROCESSSEGMENTID", 120)
                .SetIsHidden();                                                              //  이전공정 ID
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("PREVPROCESSSEGMENTNAME", 150)
                .SetIsReadOnly();                                                             //  이전공정명
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("PREVAREAID", 120)
                .SetIsHidden();                                                             //  이전 작업장 PREVAREAID
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("PREVAREANAME", 120)
                .SetIsReadOnly();                                                               //  이전 작업장 PREVAREAID

            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("PCSQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  pcsqty
            
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("PANELQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  panelqty
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("OSPMM", 120)
                .SetIsHidden()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric);                                //  panelqty

            grdOutsourcedWarehouseShipment.View.PopulateColumns();


        }
        #endregion

    }
}
