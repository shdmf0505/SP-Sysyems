#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Micube.SmartMES.OutsideOrderMgnt.Popup;
#endregion

namespace Micube.SmartMES.OutsideOrderMgnt
{
    /// <summary>
    /// 프 로 그 램 명  :  외주관리> 외주창고 > 외주창고출고
    /// 업  무  설  명  : 외주창고출고 등록한다.
    /// 생    성    자  : CHOISSTAR
    /// 생    성    일  : 2019-07-09
    /// 수  정  이  력  : 
    /// 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcedWarehouseShipment : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        bool blIsuseplantauthority = true;
        DataTable _dtSite = null;
        #endregion

        #region 생성자

        public OutsourcedWarehouseShipment()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();
            //combox setting 
            InitializeComboBox();
            //이벤트 처리 
            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
            // 입고자명 셋팅 
            txtOspsenderName.EditValue = UserInfo.Current.Id.ToString();
        }
        #region ComboBox  

        /// <summary>
        /// SmartComboBox 컨트롤 추가
        /// </summary>
        private void InitializeComboBox()
        {
            // 작업구분값 정의 
            cboPlantid.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboPlantid.ValueMember = "PLANTID";
            cboPlantid.DisplayMember = "PLANTID";
            cboPlantid.EditValue = UserInfo.Current.Plant.ToString();
            _dtSite = SqlExecuter.Query("GetPlantListAuthorityBycms", "10001"
                                                     , new Dictionary<string, object>() { { "USERID", UserInfo.Current.Id }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
            cboPlantid.DataSource = _dtSite;

            cboPlantid.ShowHeader = false;
            DataRow[] rowsPlant = _dtSite.Select("PLANTID = '" + UserInfo.Current.Plant.ToString() + "'");

            if (rowsPlant.Length > 0)
            {

                if (rowsPlant[0]["ISUSEPLANTAUTHORITY"].ToString().Equals("Y"))
                {
                    blIsuseplantauthority = true;
                    if (pnlToolbar.Controls["layoutToolbar"] != null)
                    {
                        if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                            pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = true;
                        if (pnlToolbar.Controls["layoutToolbar"].Controls["Lotnosearch"] != null)
                            pnlToolbar.Controls["layoutToolbar"].Controls["Lotnosearch"].Enabled = true;
                    }
                }
                else
                {
                    blIsuseplantauthority = false;
                    if (pnlToolbar.Controls["layoutToolbar"] != null)
                    {
                        if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                            pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = false;
                        if (pnlToolbar.Controls["layoutToolbar"].Controls["Lotnosearch"] != null)
                            pnlToolbar.Controls["layoutToolbar"].Controls["Lotnosearch"].Enabled = false;
                    }
                }

            }
            selectOspAreaidPopup(UserInfo.Current.Plant);
        }

        /// <summary>
        /// 작업장 
        /// </summary>
        /// <param name="sPlantid"></param>
        private void selectOspAreaidPopup(string sPlantid)
        {

            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();
            popup.SetPopupLayoutForm(520, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("AREANAME", PopupButtonStyles.Ok_Cancel, true, false);
            popup.Id = "AREANAME";
            popup.LabelText = "AREANAME";
            popup.SearchQuery = new SqlQuery("GetAreaidListAuthorityByOsp", "10001", $"P_ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                           , $"USERID={UserInfo.Current.Id}"
                                                                           , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                                                                           , $"P_OWNTYPE={"Y"}"
                                                                           , $"P_PLANTID={sPlantid}");
            popup.IsMultiGrid = false;

            popup.DisplayFieldName = "AREANAME";
            popup.ValueFieldName = "AREAID";
            popup.LanguageKey = "AREAID";
            popup.SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
            {
                foreach (DataRow row in selectedRow)
                {
                    if (selectedRow.Count() > 0)
                    {
                        if (row["ISMODIFY"].ToString().Equals("Y"))
                        {
                            if (blIsuseplantauthority == true)
                            {
                                if (pnlToolbar.Controls["layoutToolbar"] != null)
                                {
                                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                                        pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = true;
                                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Lotnosearch"] != null)
                                        pnlToolbar.Controls["layoutToolbar"].Controls["Lotnosearch"].Enabled = true;
                                }
                                txtLotid.Text = "";
                                txtLotid.Enabled = true;
                            }
                            else
                            {
                                if (pnlToolbar.Controls["layoutToolbar"] != null)
                                {
                                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                                        pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = false;
                                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Lotnosearch"] != null)
                                        pnlToolbar.Controls["layoutToolbar"].Controls["Lotnosearch"].Enabled = false;
                                }
                                txtLotid.Text = "";
                                txtLotid.Enabled = false;
                            }
                        }
                        else
                        {

                            if (pnlToolbar.Controls["layoutToolbar"] != null)
                            {
                                if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                                    pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = false;
                                if (pnlToolbar.Controls["layoutToolbar"].Controls["Lotnosearch"] != null)
                                    pnlToolbar.Controls["layoutToolbar"].Controls["Lotnosearch"].Enabled = false;
                            }
                            txtLotid.Text = "";
                            txtLotid.Enabled = false;
                        }

                    }

                }
            });
            popup.Conditions.AddTextBox("P_AREANAME")
                .SetLabel("AREANAME");
            popup.GridColumns.AddTextBoxColumn("AREAID", 120)
                .SetLabel("AREAID");
            popup.GridColumns.AddTextBoxColumn("AREANAME", 200)
                .SetLabel("AREANAME");
            popup.GridColumns.AddTextBoxColumn("ISMODIFY", 200)
                .SetLabel("ISMODIFY")
                .SetIsHidden();
            popupOspAreaid.SelectPopupCondition = popup;
           
        }
        #endregion
        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가

            grdOutsourcedWarehouseShipment.GridButtonItem = GridButtonItem.Delete | GridButtonItem.Export;

            grdOutsourcedWarehouseShipment.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("ENTERPRISEID", 120)
                .SetIsHidden();                                                               //  회사 ID
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("PLANTID", 120)
                .SetIsHidden();                                                               //  공장 ID
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("RECEIPTUSER", 120)
                .SetIsHidden();
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("SENDUSER", 120)
                .SetIsHidden();
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("RECEIPTSEQUENCE", 120)
                .SetIsHidden();
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("LOTHISTKEY", 120)
                .SetIsHidden();                                                               //  LOTHISTKEY

            grdOutsourcedWarehouseShipment.View.AddComboBoxColumn("LOTTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=LotType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();                                                             //  양산구분               

            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("RECEIPTDATE", 80)
                .SetDisplayFormat("yyyy-MM-dd")
                .SetIsReadOnly();                                                              // 입고일 
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("RECEIPTUSERNAME", 120)
                .SetIsReadOnly();                                                             // 입고자

            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsReadOnly();                                                             //  제품 정의 ID
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly();                                                             //  제품 정의 Version
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("PRODUCTDEFNAME", 200)
                .SetIsReadOnly();                                                             //  제품명
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("LOTID", 200)
                .SetIsReadOnly();
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("USERSEQUENCE", 80)
                .SetIsReadOnly();                                                            //  공정 수순
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("PROCESSSEGMENTID", 120)
                .SetIsHidden();                                                             //  공정 ID
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)
                .SetIsReadOnly();                                                             //  공정명
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("AREAID", 120)
                .SetIsHidden();                                                             //  작업장 AREAID
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("AREANAME", 120)
                .SetIsReadOnly();                                                               //  작업장 AREAID
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

            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("PREVPROCESSSEGMENTID", 120)
                .SetIsHidden();                                                              //  이전공정 ID
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("PREVPROCESSSEGMENTNAME", 150)
                .SetIsReadOnly();                                                             //  이전공정명
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("PREVAREAID", 120)
                .SetIsHidden();                                                             //  이전 작업장 PREVAREAID
            grdOutsourcedWarehouseShipment.View.AddTextBoxColumn("PREVAREANAME", 120)
                .SetIsReadOnly();                                                               //  이전 작업장 PREVAREAID



            grdOutsourcedWarehouseShipment.View.PopulateColumns();


        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
           
            txtLotid.KeyDown += txtLotid_KeyDown;
            cboPlantid.EditValueChanged += CboPlantid_EditValueChanged;
            btnLotnoSearch.Click += BtnLotnoSearch_Click;
            grdOutsourcedWarehouseShipment.ToolbarDeleteRow += GrdOutsourcedWarehouseShipment_ToolbarDeleteRow;
          
        }
        private void GrdOutsourcedWarehouseShipment_ToolbarDeleteRow(object sender, EventArgs e)
        {
            //DataRow focusedRow = grdOutsourcedWarehouseShipment.View.GetFocusedDataRow();
            DataTable dt = grdOutsourcedWarehouseShipment.DataSource as DataTable;
            if (dt.Rows.Count==0)
            {
                cboPlantid.Enabled = true;
                popupOspAreaid.Enabled = true;
                
            }
            //(grdOutsourcedWarehouseShipment.View.DataSource as DataView).Delete(grdOutsourcedWarehouseShipment.View.FocusedRowHandle);
            //(grdOutsourcedWarehouseShipment.View.DataSource as DataView).Table.AcceptChanges();
        }
        /// <summary>
        /// Plantid 변경 따른 Areaid 변경처리 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboPlantid_EditValueChanged(object sender, EventArgs e)
        {
            if (_dtSite == null) return;
            popupOspAreaid.SetValue("");
            popupOspAreaid.Text = "";
            popupOspAreaid.EditValue = "";
            DataRow[] rowsPlant = _dtSite.Select("PLANTID = '" + cboPlantid.EditValue.ToString() + "'");
            txtLotid.Text = "";
            if (rowsPlant.Length > 0)
            {

                if (rowsPlant[0]["ISUSEPLANTAUTHORITY"].ToString().Equals("Y"))
                {
                    blIsuseplantauthority = true;
                    if (pnlToolbar.Controls["layoutToolbar"] != null)
                    {
                        if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                            pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = true;
                        if (pnlToolbar.Controls["layoutToolbar"].Controls["Lotnosearch"] != null)
                            pnlToolbar.Controls["layoutToolbar"].Controls["Lotnosearch"].Enabled = true;
                       
                    }
                    txtLotid.Enabled = true;
                }
                else
                {
                    blIsuseplantauthority = false;
                    if (pnlToolbar.Controls["layoutToolbar"] != null)
                    {
                        if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                            pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = false;
                        if (pnlToolbar.Controls["layoutToolbar"].Controls["Lotnosearch"] != null)
                            pnlToolbar.Controls["layoutToolbar"].Controls["Lotnosearch"].Enabled = false;
                    }
                    txtLotid.Enabled = false;
                }

            }
            selectOspAreaidPopup(cboPlantid.EditValue.ToString());

        }
        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {

        }
        /// <summary>
        /// lot no Search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLotnoSearch_Click(object sender, EventArgs e)
        {
            //작업장 필수 선택 
            if (popupOspAreaid.GetValue().ToString().Equals(""))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblOspAreaid.Text); //메세지 
                popupOspAreaid.Focus();
                return;
            }
            //
            OutsourcedWarehouseShipmentPopup popup = new OutsourcedWarehouseShipmentPopup(cboPlantid.EditValue.ToString(), popupOspAreaid.GetValue().ToString(), popupOspAreaid.EditValue.ToString());

            popup.AffectLotSelectEvent += (dt) =>
            {
                foreach (DataRow row in dt.Rows)
                {
                    string sLotid = row["LOTID"].ToString();
                    int icheck = checkGridLotId(sLotid);
                    if (icheck == -1)
                    {
                        grdOutsourcedWarehouseShipment.View.AddNewRow();
                        grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
                        grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("PLANTID", cboPlantid.EditValue);// plantid
                        grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("LOTHISTKEY", row["LOTHISTKEY"]);// LOTHISTKEY
                        grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("RECEIPTSEQUENCE", row["RECEIPTSEQUENCE"]);// LOTID
                        grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("LOTID", row["LOTID"]);// LOTID
                        grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("LOTTYPE", row["LOTTYPE"]); //  양산구분       
                        grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("PRODUCTDEFID", row["PRODUCTDEFID"]);// 제품 정의 ID
                        grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("PRODUCTDEFVERSION", row["PRODUCTDEFVERSION"]);// 제품 정의 Version
                        grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("PRODUCTDEFNAME", row["PRODUCTDEFNAME"]);// 제품명
                        grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("USERSEQUENCE", row["USERSEQUENCE"]);// 공정 수순
                        grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("PROCESSSEGMENTID", row["PROCESSSEGMENTID"]);// 공정 ID
                        grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("PROCESSSEGMENTNAME", row["PROCESSSEGMENTNAME"]);// 공정명
                        grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("AREAID", row["AREAID"]);// 작업장 AREAID
                        grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("AREANAME", row["AREANAME"]);// 작업장 AREAID
                        grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("PREVPROCESSSEGMENTID", row["PREVPROCESSSEGMENTID"]);//이전공정 ID
                        grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("PREVPROCESSSEGMENTNAME", row["PREVPROCESSSEGMENTNAME"]);// 이전공정명
                        grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("PREVAREAID", row["PREVAREAID"]);// PREVAREAID
                        grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("PREVAREANAME", row["PREVAREANAME"]);// PREVAREANAME
                        grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("PCSQTY", row["PCSQTY"]);// LOTHISTKEY

                        grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("PANELQTY", row["PANELQTY"]);// PANELQTY
                        grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("OSPMM", row["OSPMM"]);// LOTHISTKEY
                        grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("RECEIPTUSER", row["RECEIPTUSER"]);// 
                        grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("RECEIPTUSERNAME", row["RECEIPTUSERNAME"]);// 
                        grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("RECEIPTDATE", row["RECEIPTDATE"]);// 
                        grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("SENDUSER", UserInfo.Current.Id.ToString());// 
                        cboPlantid.Enabled = false;
                        popupOspAreaid.Enabled = false;
                    }


                }
            };
            popup.ShowDialog(this);

        }
        /// <summary>
        /// lot id  key down 이벤트 처리 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLotid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtLotid.EditValue.ToString().Equals("")) return;
                //1. 무결성 확인 (그리드 상)

                for (int i = 0; i < grdOutsourcedWarehouseShipment.View.DataRowCount; i++)
                {
                    if (grdOutsourcedWarehouseShipment.View.GetRowCellValue(i, "LOTID").ToString().Equals((txtLotid.EditValue.ToString())))
                    {
                        //포커스 이동 처리  OspDataOverlapCheck
                        this.ShowMessage(MessageBoxButtons.OK, "OspDataOverlapCheck"); //메세지 
                        //다국어 입력된 내용이 있습니다.
                        grdOutsourcedWarehouseShipment.View.FocusedRowHandle = i;
                        txtLotid.SelectionStart = 0;
                        txtLotid.SelectionLength = txtLotid.Text.Trim().ToString().Length;
                        txtLotid.Focus();
                        return;
                    }
                }
              
                Dictionary<string, object> Param = new Dictionary<string, object>();
                Param.Add("P_PLANTID", cboPlantid.EditValue);
                Param.Add("P_LOTID", txtLotid.EditValue.ToString());
                Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                Param.Add("P_SHIPOKCHECK", "Y");
                Param = Commons.CommonFunction.ConvertParameter(Param);
                DataTable dtLotid = SqlExecuter.Query("GetOutsourcedWarehouseShipment", "10001", Param);

                if (dtLotid.Rows.Count > 0)
                {
                    DataRow row = dtLotid.Rows[0];
                    string strShipOkCheck = row["SHIPOKCHECK"].ToString();
                    string strAreaid = popupOspAreaid.GetValue().ToString();
                    string strTempAreaid = row["AREAID"].ToString();
                    string strAreaname = row["AREANAME"].ToString();
                    if (strShipOkCheck.Equals("OK"))
                    {
                        if (strAreaid.Equals(strTempAreaid))
                        {
                            grdOutsourcedWarehouseShipment.View.AddNewRow();
                            grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
                            grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("PLANTID", cboPlantid.EditValue);// plantid
                            grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("LOTHISTKEY", row["LOTHISTKEY"]);// LOTHISTKEY
                            grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("RECEIPTSEQUENCE", row["RECEIPTSEQUENCE"]);// LOTID
                            grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("LOTID", row["LOTID"]);// LOTID
                            grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("LOTTYPE", row["LOTTYPE"]); //  양산구분       
                            grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("PRODUCTDEFID", row["PRODUCTDEFID"]);// 제품 정의 ID
                            grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("PRODUCTDEFVERSION", row["PRODUCTDEFVERSION"]);// 제품 정의 Version
                            grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("PRODUCTDEFNAME", row["PRODUCTDEFNAME"]);// 제품명
                            grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("USERSEQUENCE", row["USERSEQUENCE"]);// 공정 수순
                            grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("PROCESSSEGMENTID", row["PROCESSSEGMENTID"]);// 공정 ID
                            grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("PROCESSSEGMENTNAME", row["PROCESSSEGMENTNAME"]);// 공정명
                            grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("AREAID", row["AREAID"]);// 작업장 AREAID
                            grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("AREANAME", row["AREANAME"]);// 작업장 AREAID
                            grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("PREVPROCESSSEGMENTID", row["PREVPROCESSSEGMENTID"]);//이전공정 ID
                            grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("PREVPROCESSSEGMENTNAME", row["PREVPROCESSSEGMENTNAME"]);// 이전공정명
                            grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("PREVAREAID", row["PREVAREAID"]);// PREVAREAID
                            grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("PREVAREANAME", row["PREVAREANAME"]);// PREVAREANAME
                            grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("PCSQTY", row["PCSQTY"]);// LOTHISTKEY

                            grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("PANELQTY", row["PANELQTY"]);// PANELQTY
                            grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("OSPMM", row["OSPMM"]);// LOTHISTKEY
                            grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("RECEIPTUSER", row["RECEIPTUSER"]);// 
                            grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("RECEIPTUSERNAME", row["RECEIPTUSERNAME"]);// 
                            grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("RECEIPTDATE", row["RECEIPTDATE"]);// 
                            grdOutsourcedWarehouseShipment.View.SetFocusedRowCellValue("SENDUSER", UserInfo.Current.Id.ToString());// 
                            txtLotid.Text = "";

                            txtLotid.Focus();
                            cboPlantid.Enabled = false;
                            popupOspAreaid.Enabled = false;
                            
                        }
                        else
                        {
                            this.ShowMessage(MessageBoxButtons.OK, DialogResult.None, "InValidOspData020", strAreaname); //메세지 
                            txtLotid.SelectionStart = 0;
                            txtLotid.SelectionLength = txtLotid.Text.Trim().ToString().Length;
                            txtLotid.Focus();
                            return;
                        }
                    }
                    else
                    {
                        // 다국어  해당 lot no에 해당하는 정보가 없습니다. 메세지 처리 
                        this.ShowMessage(MessageBoxButtons.OK, DialogResult.None, "InValidOspData019", txtLotid.Text); //메세지 
                        txtLotid.SelectionStart = 0;
                        txtLotid.SelectionLength = txtLotid.Text.Trim().ToString().Length;
                        txtLotid.Focus();
                        return;
                    }
                }
                else
                {

                    // 다국어  해당 lot no에 해당하는 정보가 없습니다. 메세지 처리 
                    this.ShowMessage(MessageBoxButtons.OK, DialogResult.None, "InValidOspData003", txtLotid.Text); //메세지 
                    txtLotid.SelectionStart = 0;
                    txtLotid.SelectionLength = txtLotid.Text.Trim().ToString().Length;
                    txtLotid.Focus();
                    return;
                }

            }
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            //base.OnToolbarSaveClick();

            //// TODO : 저장 Rule 변경
            //DataTable changed = grdOutsourcedWarehouseShipment.GetChangedRows();

            //ExecuteRule("OutsourcedWarehouseShipment", changed);
        }
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Save"))
            {
                ProcSave(btn.Text);
            }
            if (btn.Name.ToString().Equals("Lotnosearch"))
            {

                BtnLotnoSearch_Click(null, null);
            }

        }
        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {

            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            txtLotid.EditValue = "";
            //조회용 임시 테이블 생성 null 이면 에러 발생 
            DataTable dtSearch = (grdOutsourcedWarehouseShipment.DataSource as DataTable).Clone();

            grdOutsourcedWarehouseShipment.DataSource = dtSearch;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용

        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            grdOutsourcedWarehouseShipment.View.CheckValidation();

            DataTable changed = grdOutsourcedWarehouseShipment.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 마스터 정보를 조회한다.
        /// </summary>
        private void LoadData()
        {

        }

        /// <summary>
        /// lotid 중복 체크 처리 
        /// </summary>
        /// <param name="sLotId"></param>
        /// <returns></returns>
        private int checkGridLotId(string sLotId)
        {

            for (int i = 0; i < grdOutsourcedWarehouseShipment.View.DataRowCount; i++)
            {
                if (grdOutsourcedWarehouseShipment.View.GetRowCellValue(i, "LOTID").ToString().Equals(sLotId))
                {
                    return i;
                }
            }
            return -1;
        }

        private void ProcSave(string strtitle)
        {
            grdOutsourcedWarehouseShipment.View.CheckValidation();

            DataTable changed = grdOutsourcedWarehouseShipment.DataSource as DataTable;

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK,"NoSaveData");
                return;
            }

            
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", strtitle);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    DataTable dtSave = grdOutsourcedWarehouseShipment.DataSource as DataTable;
                    ExecuteRule("OutsourcedWarehouseShipment", dtSave);
                    ShowMessage("SuccessOspProcess");
                    grdOutsourcedWarehouseShipment.View.ClearDatas();
                    cboPlantid.Enabled = true;
                    popupOspAreaid.Enabled = true;
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();


                }
            }
        }
        #endregion
    }
}
