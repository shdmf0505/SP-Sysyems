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
using DevExpress.XtraGrid.Views.Grid;
#endregion

namespace Micube.SmartMES.OutsideOrderMgnt
{
    /// <summary>
    /// 프 로 그 램 명  :  외주관리> 외주 가공비마감 > 외주비 집계 및 마감
    /// 업  무  설  명  :  외주비 집계 및 마감한다.
    /// 생    성    자  : CHOISSTAR
    /// 생    성    일  : 2019-08-28
    /// 수  정  이  력  : 
    /// 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcingCostClosing : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        string _strrequsetno = "";
        string _strPeriodid = "";
        string _strPeriodstate = "";
        string _strPeriodidMajor = "";
        string _strPeriodstateMajor = "";
        #endregion

        #region 생성자

        public OutsourcingCostClosing()
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

            InitializeEvent();

            if (pnlToolbar.Controls["layoutToolbar"] != null)
            {
                if (pnlToolbar.Controls["layoutToolbar"].Controls["Settleprocess"] != null)
                    pnlToolbar.Controls["layoutToolbar"].Controls["Settleprocess"].Visible = false;
                if (pnlToolbar.Controls["layoutToolbar"].Controls["Settlelist"] != null)
                    pnlToolbar.Controls["layoutToolbar"].Controls["Settlelist"].Visible = false;

                if (pnlToolbar.Controls["layoutToolbar"].Controls["Recalculation"] != null)
                    pnlToolbar.Controls["layoutToolbar"].Controls["Recalculation"].Visible = false;

            }
            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
        }
        /// <summary>
        ///  그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            InitializeGrid_Closing();

            InitializeGrid_Major();
        }
        /// <summary>        
        ///  그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid_Closing()
        {
            // TODO : 그리드 초기화 로직 추가
            grdCostClosing.GridButtonItem = GridButtonItem.Export;
            grdCostClosing.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdCostClosing.View.AddTextBoxColumn("REQUESTNO", 120).SetIsHidden();            //  
            grdCostClosing.View.AddTextBoxColumn("PERIODID", 120).SetIsHidden();            //  
            grdCostClosing.View.AddTextBoxColumn("AREAID", 120).SetIsHidden();            //  
            grdCostClosing.View.AddTextBoxColumn("VENDORID", 120).SetIsHidden();            // 
            grdCostClosing.View.AddTextBoxColumn("PLANTID", 120).SetIsHidden();            //  
            grdCostClosing.View.AddTextBoxColumn("PERIODTYPE", 120).SetIsHidden();            //  
            grdCostClosing.View.AddTextBoxColumn("SETTLEUSER", 120).SetIsHidden();            //  
            grdCostClosing.View.AddTextBoxColumn("PERIODNAME", 120).SetIsHidden();            // 
            grdCostClosing.View.AddTextBoxColumn("ENDDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd")
                .SetIsHidden();
            var costClose = grdCostClosing.View.AddGroupColumn("OSPCLOSEINFOR");
            costClose.AddComboBoxColumn("PERIODSTATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=PeriodState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Open")
                .SetValidationIsRequired();
            costClose.AddTextBoxColumn("AREANAME", 120)
               .SetIsReadOnly();          //  
            costClose.AddTextBoxColumn("OSPVENDORNAME", 120)
                .SetIsReadOnly();          //  
            var costtotlotprd = grdCostClosing.View.AddGroupColumn("OSPLOTTYPEPRD");
            costtotlotprd.AddTextBoxColumn("OSPPRODUCTIONCNT", 80)
              .SetLabel("OSPCLOSECOUNT")
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costtotlotprd.AddSpinEditColumn("OSPPRODUCTIONAMOUNT", 100)
                .SetLabel("OSPCLOSEAMOUNT")
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            costtotlotprd.AddSpinEditColumn("OSPPRODUCTIONREDUCEAMOUNT", 100)
                .SetLabel("OSPCLOSEREDUCEAMOUNT")
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();

            costtotlotprd.AddTextBoxColumn("OSPPRODUCTIONDSETTLEAMOUNT", 120)
              .SetLabel("OSPCLOSESETTLEAMOUNT")
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            var costtotlotsim = grdCostClosing.View.AddGroupColumn("OSPLOTTYPESIM");
            costtotlotsim.AddTextBoxColumn("OSPSAMPLECNT", 80)
             .SetLabel("OSPCLOSECOUNT")
             .SetTextAlignment(TextAlignment.Right)
             .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costtotlotsim.AddSpinEditColumn("OSPSAMPLEAMOUNT", 100)
                .SetLabel("OSPCLOSEAMOUNT")
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            costtotlotsim.AddSpinEditColumn("OSPSAMPLEREDUCEAMOUNT", 100)
                .SetLabel("OSPCLOSEREDUCEAMOUNT")
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();

            costtotlotsim.AddTextBoxColumn("OSPSAMPLESETTLEAMOUNT", 120)
             .SetLabel("OSPCLOSESETTLEAMOUNT")
             .SetTextAlignment(TextAlignment.Right)
             .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            var costtotlotSMT= grdCostClosing.View.AddGroupColumn("OSPLOTTYPESMT");
            costtotlotSMT.AddTextBoxColumn("OSPSMTCNT", 80)
             .SetLabel("OSPCLOSECOUNT")
             .SetTextAlignment(TextAlignment.Right)
             .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costtotlotSMT.AddSpinEditColumn("OSPSMTAMOUNT", 100)
                .SetLabel("OSPCLOSEAMOUNT")
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            costtotlotSMT.AddSpinEditColumn("OSPSMTREDUCEAMOUNT", 100)
                .SetLabel("OSPCLOSEREDUCEAMOUNT")
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();

            costtotlotSMT.AddTextBoxColumn("OSPSMTSETTLEAMOUNT", 120)
             .SetLabel("OSPCLOSESETTLEAMOUNT")
             .SetTextAlignment(TextAlignment.Right)
             .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costActual = grdCostClosing.View.AddGroupColumn("OSPCLOSEACTUALAINFO");
            costActual.AddSpinEditColumn("OSPCLOSECOUNT", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            costActual.AddSpinEditColumn("OSPCLOSEAMOUNT", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            costActual.AddSpinEditColumn("OSPCLOSEREDUCEAMOUNT", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            costActual.AddSpinEditColumn("OSPCLOSESETTLEAMOUNT", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();


            var costEctWork = grdCostClosing.View.AddGroupColumn("OSPCLOSEETCWORKINFO");
            costEctWork.AddSpinEditColumn("OSPETCWORKCOUNT", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            costEctWork.AddSpinEditColumn("OSPETCWORKSUM", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            var costEctAmount = grdCostClosing.View.AddGroupColumn("OSPCLOSEETAMOUTINFO");
            costEctAmount.AddSpinEditColumn("OSPETCAMOUNTCOUNT", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            costEctAmount.AddSpinEditColumn("OSPETCAMOUNTSUM", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();

            var costClaim = grdCostClosing.View.AddGroupColumn("OSPCLAIMINFOR");
            costClaim.AddSpinEditColumn("AMOUNTCLAIM", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);             //  
            costClaim.AddSpinEditColumn("AMOUNTQUALITY", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            costClaim.AddComboBoxColumn("CLAIMPERIODSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=PeriodState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Open")
                .SetIsReadOnly();//  
            var costsummaryprice = grdCostClosing.View.AddGroupColumn("OSPCOST");
            costsummaryprice.AddSpinEditColumn("SUMMARYPRICE", 100)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric)
               .SetIsReadOnly();
            var costInterface = grdCostClosing.View.AddGroupColumn("OSPINTERFACEINFOR");
            costInterface.AddTextBoxColumn("INTERFACENO", 120)
                .SetIsReadOnly();        //  
            costInterface.AddTextBoxColumn("INTERFACEDATE", 100)
                .SetDisplayFormat("yyyy-MM-dd")
                .SetIsReadOnly();                    //  
            costInterface.AddSpinEditColumn("INTERFACECOUNT", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();

            grdCostClosing.View.PopulateColumns();
        }
        /// <summary>        
        ///  그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid_Major()
        {
            // TODO : 그리드 초기화 로직 추가
            grdCostClosingMajor.GridButtonItem = GridButtonItem.Export;
            grdCostClosingMajor.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdCostClosingMajor.View.AddTextBoxColumn("REQUESTNO", 120).SetIsHidden();            // 
            grdCostClosingMajor.View.AddTextBoxColumn("PERIODID", 120).SetIsHidden();            //
            grdCostClosingMajor.View.AddTextBoxColumn("PERIODNAME", 120).SetIsHidden();            // 
            grdCostClosingMajor.View.AddTextBoxColumn("AREAID", 120).SetIsHidden();            //  
            grdCostClosingMajor.View.AddTextBoxColumn("VENDORID", 120).SetIsHidden();            // 
            grdCostClosingMajor.View.AddTextBoxColumn("PLANTID", 120).SetIsHidden();            //  
            grdCostClosingMajor.View.AddTextBoxColumn("PERIODTYPE", 120).SetIsHidden();            //  
            grdCostClosingMajor.View.AddTextBoxColumn("SETTLEUSER", 120).SetIsHidden();            //  


            grdCostClosingMajor.View.AddTextBoxColumn("ENDDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd")
                .SetIsHidden();
            var costClose = grdCostClosingMajor.View.AddGroupColumn("OSPCLOSEINFOR");
            costClose.AddComboBoxColumn("PERIODSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=PeriodState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Open")
                .SetValidationIsRequired();
            costClose.AddTextBoxColumn("AREANAME", 120)
                .SetIsReadOnly();          //  
            costClose.AddTextBoxColumn("OSPVENDORNAME", 120)
                .SetIsReadOnly();

            var costtotlotprd = grdCostClosingMajor.View.AddGroupColumn("OSPLOTTYPEPRD");
            costtotlotprd.AddTextBoxColumn("OSPPRODUCTIONCNT", 80)
              .SetLabel("OSPCLOSECOUNT")
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costtotlotprd.AddSpinEditColumn("OSPPRODUCTIONAMOUNT", 100)
                .SetLabel("OSPCLOSEAMOUNT")
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            costtotlotprd.AddSpinEditColumn("OSPPRODUCTIONREDUCEAMOUNT", 100)
                .SetLabel("OSPCLOSEREDUCEAMOUNT")
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();

            costtotlotprd.AddTextBoxColumn("OSPPRODUCTIONDSETTLEAMOUNT", 120)
              .SetLabel("OSPCLOSESETTLEAMOUNT")
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            var costtotlotsim = grdCostClosingMajor.View.AddGroupColumn("OSPLOTTYPESIM");
            costtotlotsim.AddTextBoxColumn("OSPSAMPLECNT", 80)
             .SetLabel("OSPCLOSECOUNT")
             .SetTextAlignment(TextAlignment.Right)
             .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costtotlotsim.AddSpinEditColumn("OSPSAMPLEAMOUNT", 100)
                .SetLabel("OSPCLOSEAMOUNT")
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();

            costtotlotsim.AddSpinEditColumn("OSPSAMPLEREDUCEAMOUNT", 100)
               .SetLabel("OSPCLOSEREDUCEAMOUNT")
               .SetDisplayFormat("#,##0", MaskTypes.Numeric)
               .SetIsReadOnly();
            costtotlotsim.AddTextBoxColumn("OSPSAMPLESETTLEAMOUNT", 120)
                .SetLabel("OSPCLOSESETTLEAMOUNT")
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            var costtotlotSMT = grdCostClosingMajor.View.AddGroupColumn("OSPLOTTYPESMT");
            costtotlotSMT.AddTextBoxColumn("OSPSMTCNT", 80)
             .SetLabel("OSPCLOSECOUNT")
             .SetTextAlignment(TextAlignment.Right)
             .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costtotlotSMT.AddSpinEditColumn("OSPSMTAMOUNT", 100)
                .SetLabel("OSPCLOSEAMOUNT")
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            costtotlotSMT.AddSpinEditColumn("OSPSMTREDUCEAMOUNT", 100)
                .SetLabel("OSPCLOSEREDUCEAMOUNT")
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();

            costtotlotSMT.AddTextBoxColumn("OSPSMTSETTLEAMOUNT", 120)
             .SetLabel("OSPCLOSESETTLEAMOUNT")
             .SetTextAlignment(TextAlignment.Right)
             .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            var costActual = grdCostClosingMajor.View.AddGroupColumn("OSPCLOSEACTUALAINFO");
            costActual.AddSpinEditColumn("OSPCLOSECOUNT", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            costActual.AddSpinEditColumn("OSPCLOSEAMOUNT", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            costActual.AddSpinEditColumn("OSPCLOSEREDUCEAMOUNT", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            costActual.AddSpinEditColumn("OSPCLOSESETTLEAMOUNT", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();


            var costEctWork = grdCostClosingMajor.View.AddGroupColumn("OSPCLOSEETCWORKINFO");
            costEctWork.AddSpinEditColumn("OSPETCWORKCOUNT", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            costEctWork.AddSpinEditColumn("OSPETCWORKSUM", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            var costEctAmount = grdCostClosingMajor.View.AddGroupColumn("OSPCLOSEETAMOUTINFO");
            costEctAmount.AddSpinEditColumn("OSPETCAMOUNTCOUNT", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            costEctAmount.AddSpinEditColumn("OSPETCAMOUNTSUM", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            var costsummaryprice = grdCostClosingMajor.View.AddGroupColumn("OSPCOST");
            costsummaryprice.AddSpinEditColumn("SUMMARYPRICE", 100)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric)
               .SetIsReadOnly();
            var costMajorAmount = grdCostClosingMajor.View.AddGroupColumn("OSPCLOSEMAJORSETTLEINFO");


            costMajorAmount.AddSpinEditColumn("MAJORVENDORAMOUNT", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            costMajorAmount.AddSpinEditColumn("OSPMAJORSETTLECOUNT", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            costMajorAmount.AddSpinEditColumn("OSPMAJORSETTLEAMOUNT", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            costMajorAmount.AddSpinEditColumn("OSPMAJORSETTLESUB", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();

            var costInterface = grdCostClosingMajor.View.AddGroupColumn("OSPINTERFACEINFOR");
            costInterface.AddTextBoxColumn("INTERFACENO", 120)
                .SetIsReadOnly();        //  
            costInterface.AddTextBoxColumn("INTERFACEDATE", 100)
                .SetDisplayFormat("yyyy-MM-dd")
                .SetIsReadOnly();                    //  
            costInterface.AddSpinEditColumn("INTERFACECOUNT", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();

            grdCostClosingMajor.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            //// TODO : 화면에서 사용할 이벤트 추가
            //grdList.View.AddingNewRow += View_AddingNewRow;
            grdCostClosing.View.ShowingEditor += View_ShowingEditor;
            grdCostClosingMajor.View.ShowingEditor += ViewMajor_ShowingEditor;

            grdCostClosingMajor.View.RowCellStyle += ViewMajor_RowCellStyle;

            grdCostClosing.View.CellValueChanged += View_CellValueChanged;
            //  grdCostClosingMajor.View.CellValueChanged += ViewMajor_CellValueChanged;


            btnAggregate.Click += BtnAggregate_Click;
            btnSave.Click += BtnSave_Click;
            btnCloseSend.Click += BtnCloseSend_Click;

            btnSettlemantList.Click += BtnSettlemantList_Click;

            tabClose.SelectedPageChanged += tabClose_SelectedPageChanged;
            //정산처리 
            btnSettlemantProcess.Click += BtnSettlemantProcess_Click;
        }
        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            grdCostClosing.View.CellValueChanged -= View_CellValueChanged;
            DataRow row = grdCostClosing.View.GetFocusedDataRow();
            string strOspclosesettleamount = row["OSPCLOSESETTLEAMOUNT"].ToString();
            decimal decOspclosesettleamount = (strOspclosesettleamount.ToString().Equals("") ? 0 : Convert.ToDecimal(strOspclosesettleamount)); //

            string strOspetcworksum = row["OSPETCWORKSUM"].ToString();
            decimal decOspetcworksum = (strOspetcworksum.ToString().Equals("") ? 0 : Convert.ToDecimal(strOspetcworksum)); //

            string strOspetcamountsum = row["OSPETCAMOUNTSUM"].ToString();
            decimal decOspetcamountsum = (strOspetcamountsum.ToString().Equals("") ? 0 : Convert.ToDecimal(strOspetcamountsum)); //
            string strAmountclaim = row["AMOUNTCLAIM"].ToString();
            decimal decAmountclaim = (strAmountclaim.ToString().Equals("") ? 0 : Convert.ToDecimal(strAmountclaim)); //

            row["SUMMARYPRICE"] = decOspclosesettleamount + decOspetcworksum + decOspetcamountsum + decAmountclaim;
            grdCostClosing.View.CellValueChanged += View_CellValueChanged;
        }


        private void tabClose_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page == tapExpense)
            {
                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Settleprocess"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Settleprocess"].Visible = false;
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Settlelist"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Settlelist"].Visible = false;
                }
            }
            else
            {
                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Settleprocess"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Settleprocess"].Visible = true;
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Settlelist"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Settlelist"].Visible = true;


                }

            }

        }
        /// <summary>
        /// 정산처리 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSettlemantProcess_Click(object sender, EventArgs e)
        {
            DataTable dtcheck = grdCostClosingMajor.View.GetCheckedRows();

            if (dtcheck.Rows.Count == 0)
            {
                ShowMessage("GridNoChecked"); //(선택된 내역이 없습니다)/
                return;
            }
            if (blAggregateCheck() == false) return;

            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnSettlemantProcess.Text);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnSave.Enabled = false;
                    btnSettlemantList.Enabled = false;
                    btnSettlemantProcess.Enabled = false;
                    DataTable dtSave = grdCostClosingMajor.View.GetCheckedRows();
                    for (int i = 0; i < dtSave.Rows.Count; i++)
                    {
                        DataRow row = dtSave.Rows[i];
                        if (row["MAJORVENDORAMOUNT"].ToString().Equals(""))
                        {
                            row["MAJORVENDORAMOUNT"] = 0;
                        }
                        row["SETTLEUSER"] = UserInfo.Current.Id.ToString();

                    }
                    ExecuteRule("OutsourcedCostsforMajorSuppliersSettle", dtSave);
                    //재조회 하기...
                    OnSaveConfrimSearch();
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();


                    btnSave.Enabled = true;
                    btnSettlemantList.Enabled = true;
                    btnSettlemantProcess.Enabled = true;

                }
            }

        }
        /// <summary>
        /// 정산내역조회 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSettlemantList_Click(object sender, EventArgs e)
        {

            //마감년도 
            //
            if (grdCostClosingMajor.View.DataRowCount == 0)
            {
                return;
            }

            String strperiodid = grdCostClosingMajor.View.GetFocusedRowCellValue("PERIODID").ToString();
            String strplantid = grdCostClosingMajor.View.GetFocusedRowCellValue("PLANTID").ToString();

            String strvendorid = grdCostClosingMajor.View.GetFocusedRowCellValue("VENDORID").ToString();
            String strvendorname = grdCostClosingMajor.View.GetFocusedRowCellValue("OSPVENDORNAME").ToString();
            String strperiodname = grdCostClosingMajor.View.GetFocusedRowCellValue("PERIODNAME").ToString();
            OutsourcedCostsforMajorSuppliersYoungPongPopup itemPopup = new OutsourcedCostsforMajorSuppliersYoungPongPopup(strperiodid, strplantid, strvendorid, strvendorname, strperiodname);
            itemPopup.ShowDialog(this);

        }

        /// <summary>
        /// 마감전송....
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCloseSend_Click(object sender, EventArgs e)
        {
            DataTable dtcheck = grdCostClosing.View.GetCheckedRows();

            if (blCloseSendCheck() == false) return;

            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnCloseSend.Text);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnSave.Enabled = false;
                    btnAggregate.Enabled = false;
                    btnCloseSend.Enabled = false;

                    DataTable dtSend = null;
                    if (tabClose.SelectedTabPage.Name.Equals("tapExpense"))
                    {
                        dtSend = grdCostClosing.View.GetCheckedRows();
                    }

                    else if (tabClose.SelectedTabPage.Name.Equals("tapMajor"))
                    {
                        dtSend = grdCostClosingMajor.View.GetCheckedRows();
                    }
                    string strPeriodname = "";
                    string strPeriodid = "";
                    for (int i = 0; i < dtSend.Rows.Count; i++)
                    {
                        DataRow row = dtSend.Rows[i];

                        row["SETTLEUSER"] = UserInfo.Current.Id.ToString();
                        strPeriodname = row["PERIODNAME"].ToString();
                        strPeriodid = row["PERIODID"].ToString();

                    };

                    dtSend.TableName = "list";
                    DataTable dtConSave = createSaveDatatable();
                    DataRow dr = dtConSave.NewRow();
                    string strParametervalue = "";
                    dr["ENTERPRISEID"] = UserInfo.Current.Enterprise.ToString();
                    dr["PLANTID"] = UserInfo.Current.Plant.ToString();

                    dr["USERID"] = UserInfo.Current.Id.ToString();
                    dr["FUNCTIONID"] = "OSPCloseSend";   ///
                    strParametervalue = strParametervalue + "Plantid :" + UserInfo.Current.Plant.ToString() + " ,";
                    strParametervalue = strParametervalue + "Periodname :" + strPeriodname + " ,";
                    strParametervalue = strParametervalue + "Periodid :" + strPeriodid + " ,";

                    dr["PARAMETERVALUE"] = strParametervalue;
                    dr["USERID"] = UserInfo.Current.Id.ToString();

                    dr["_STATE_"] = "added";


                    dtConSave.Rows.Add(dr);
                    DataTable saveResult = this.ExecuteRule<DataTable>("OspConCurRequestSaveCloseSend", dtConSave);
                    DataRow resultData = saveResult.Rows[0];
                    string strtempRequestno = resultData.ItemArray[0].ToString();
                    string strtempInterfaceno = resultData.ItemArray[1].ToString();
                    _strrequsetno = strtempRequestno;
                    string strInterfaceno = "";
                    string strrowcnt = "";
                    int rowcnt = 0;
                    for (int i = 0; i < dtSend.Rows.Count; i++)
                    {
                        DataRow row = dtSend.Rows[i];
                        rowcnt = i + 1;
                        strrowcnt = rowcnt.ToString();
                        strrowcnt = strrowcnt.PadLeft(4, '0');

                        strInterfaceno = strtempInterfaceno + "-" + strrowcnt;
                        row["INTERFACENO"] = strInterfaceno;
                        row["REQUESTNO"] = strtempRequestno;

                    };
                    MessageWorker worker = new MessageWorker("OutsourcingCostClosingCloseSend");
                    worker.SetBody(new MessageBody()
                        {
                            { "list", dtSend }
                        });
                    worker.ExecuteNoResponse();
                    ShowMessage("SuccessOspRequest");
                    //worker.ExecuteWithTimeout(300);
                    //this.ExecuteRule("OutsourcingCostClosingCloseSend", dtSend);
                    // ShowMessage("SuccessOspProcess");
                    //재조회 해야함.
                    OnSaveConfrimSearch();

                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();
                    btnSave.Enabled = true;
                    btnAggregate.Enabled = true;
                    btnCloseSend.Enabled = true;


                }
            }

        }
        /// <summary>
        /// 저장시 기본테이블 생성
        /// </summary>
        /// <returns></returns>
        private DataTable createSaveDatatable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "list";

            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");
            dt.Columns.Add("USERID");
            dt.Columns.Add("REQUESTNO");
            dt.Columns.Add("FUNCTIONID");
            dt.Columns.Add("PARAMETERVALUE");
            dt.Columns.Add("_STATE_");
            return dt;
        }
        /// <summary>
        /// 외주비집계 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAggregate_Click(object sender, EventArgs e)
        {
            if (blAggregateCheck() == false) return;

            //_strPeriodid 
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnAggregate.Text);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnSave.Enabled = false;
                    btnAggregate.Enabled = false;
                    btnCloseSend.Enabled = false;
                    //datatable 생성 
                    DataTable dt = createSaveHeaderDatatable();
                    DataTable dtcheck = null;
                    if (tabClose.SelectedTabPage.Name.Equals("tapExpense"))
                    {
                        dtcheck = grdCostClosing.View.GetCheckedRows();
                    }

                    else if (tabClose.SelectedTabPage.Name.Equals("tapMajor"))
                    {
                        dtcheck = grdCostClosingMajor.View.GetCheckedRows();
                    }

                    ExecuteRule("OutsourcingCostClosingAggregate", dtcheck);
                    ShowMessage("SuccessOspProcess");
                    OnSaveConfrimSearch();
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();
                    btnSave.Enabled = true;
                    btnAggregate.Enabled = true;
                    btnCloseSend.Enabled = true;


                }
            }

        }
        /// <summary>
        /// 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (tabClose.SelectedTabPage.Name.Equals("tapExpense"))
            {
                grdCostClosing.View.FocusedRowHandle = grdCostClosing.View.FocusedRowHandle;
                grdCostClosing.View.FocusedColumn = grdCostClosing.View.Columns["OSPVENDORNAME"];
                grdCostClosing.View.ShowEditor();
                DataTable dtchanged = grdCostClosing.DataSource as DataTable;
                //DataTable dtchanged = grdCostClosing.GetChangedRows();
                if (dtchanged.Rows.Count == 0)
                {
                    // 저장할 데이터가 존재하지 않습니다.
                    this.ShowMessage(MessageBoxButtons.OK, "NoSaveData"); //메세지 
                    return;
                }
            }
            else if (tabClose.SelectedTabPage.Name.Equals("tapMajor"))
            {
                grdCostClosingMajor.View.FocusedRowHandle = grdCostClosingMajor.View.FocusedRowHandle;
                grdCostClosingMajor.View.FocusedColumn = grdCostClosingMajor.View.Columns[""];
                grdCostClosingMajor.View.ShowEditor();
                DataTable dtchangedMajor = grdCostClosingMajor.DataSource as DataTable;
                //DataTable dtchangedMajor = grdCostClosingMajor.GetChangedRows();
                if (dtchangedMajor.Rows.Count == 0)
                {
                    // 저장할 데이터가 존재하지 않습니다.
                    this.ShowMessage(MessageBoxButtons.OK, "NoSaveData"); //메세지 
                    return;
                }
            }

            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnSave.Text);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnSave.Enabled = false;
                    btnAggregate.Enabled = false;
                    btnCloseSend.Enabled = false;
                    if (tabClose.SelectedTabPage.Name.Equals("tapExpense"))
                    {
                        //DataTable dtSave = grdCostClosing.GetChangedRows();
                        DataTable dtSave = grdCostClosing.DataSource as DataTable;
                        dtSave.TableName = "list";
                        for (int i = 0; i < dtSave.Rows.Count; i++)
                        {
                            DataRow row = dtSave.Rows[i];
                            if (row["AMOUNTCLAIM"].ToString().Equals(""))
                            {
                                row["AMOUNTCLAIM"] = 0;
                            }
                            row["SETTLEUSER"] = UserInfo.Current.Id.ToString();

                        }
                        ExecuteRule("OutsourcingCostClosing", dtSave);
                    }

                    else if (tabClose.SelectedTabPage.Name.Equals("tapMajor"))
                    {
                        //DataTable dtSave = grdCostClosingMajor.GetChangedRows();
                        DataTable dtSave = grdCostClosingMajor.DataSource as DataTable;
                        dtSave.TableName = "list";
                        for (int i = 0; i < dtSave.Rows.Count; i++)
                        {
                            DataRow row = dtSave.Rows[i];
                            if (row["MAJORVENDORAMOUNT"].ToString().Equals(""))
                            {
                                row["MAJORVENDORAMOUNT"] = 0;
                            }
                            row["SETTLEUSER"] = UserInfo.Current.Id.ToString();

                        }
                        ExecuteRule("OutsourcedCostsforMajorSuppliers", dtSave);
                    }

                    ShowMessage("SuccessOspProcess");
                    OnSaveConfrimSearch();
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();
                    btnSave.Enabled = true;
                    btnAggregate.Enabled = true;
                    btnCloseSend.Enabled = true;


                }
            }

        }
        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            throw new NotImplementedException();
        }
        private void View_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (!(grdCostClosing.View.GetFocusedRowCellValue("INTERFACENO").ToString().Equals("")))
            {
                e.Cancel = true;
            }
            if (_strPeriodstate.Equals("Close"))
            {
                e.Cancel = true;
            }

        }
        private void ViewMajor_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (!(grdCostClosingMajor.View.GetFocusedRowCellValue("INTERFACENO").ToString().Equals("")))
            {
                e.Cancel = true;
            }
            if (_strPeriodstateMajor.Equals("Close"))
            {
                e.Cancel = true;
            }

        }

        /// <summary>
		/// Cell Style
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ViewMajor_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            if (e.Column.FieldName == "MAJORVENDORAMOUNT")
            {
                string strMajorvendoramount = grdCostClosingMajor.View.GetRowCellValue(e.RowHandle, "MAJORVENDORAMOUNT").ToString();
                decimal decMajorvendoramount = (strMajorvendoramount.ToString().Equals("") ? 0 : Convert.ToDecimal(strMajorvendoramount)); //
                string strOspmajorsettleamount = grdCostClosingMajor.View.GetRowCellValue(e.RowHandle, "OSPMAJORSETTLEAMOUNT").ToString();
                decimal decOspmajorsettleamount = (strOspmajorsettleamount.ToString().Equals("") ? 0 : Convert.ToDecimal(strOspmajorsettleamount)); //
                string strOspmajorsettlesub = grdCostClosingMajor.View.GetRowCellValue(e.RowHandle, "OSPMAJORSETTLESUB").ToString();
                decimal decOspmajorsettlesub = (strOspmajorsettlesub.ToString().Equals("") ? 0 : Convert.ToDecimal(strOspmajorsettlesub)); //
                if ((decMajorvendoramount != (decOspmajorsettleamount + decOspmajorsettlesub)))
                {
                    e.Appearance.BackColor = Color.LightCoral;
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

        }
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;
            if (btn.Name.ToString().Equals("Save"))
            {

                BtnSave_Click(null, null);
            }

            //if (btn.Name.ToString().Equals("Recalculation"))
            //{

            //    BtnAggregate_Click(null, null);
            //}

            if (btn.Name.ToString().Equals("Outclosesend"))
            {

                BtnCloseSend_Click(null, null);
            }
            if (btn.Name.ToString().Equals("Settleprocess"))
            {

                BtnSettlemantProcess_Click(null, null);
            }
            if (btn.Name.ToString().Equals("Settlelist"))
            {

                BtnSettlemantList_Click(null, null);
            }
            if (btn.Name.ToString().Equals("OspWorkStatus"))
            {

                ProcOspWorkStatus(btn.Text);
            }
        }

        private void ProcOspWorkStatus(string strtitle)
        {
            OspConCurRequestPopup itemPopup = new OspConCurRequestPopup(_strrequsetno, "OSPCloseSend");
            itemPopup.ShowDialog(this);
        }
        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();
            string stryearmonth = values["P_YEARMONTH"].ToString();
            DateTime dtyearmonth = Convert.ToDateTime(stryearmonth);
            values.Add("P_PERIODNAME", dtyearmonth.ToString("yyyy-MM"));
            if (tabClose.SelectedTabPage.Name.Equals("tapExpense"))
            {
                values.Add("P_PERIODTYPE", "OutSourcing");
            }

            else if (tabClose.SelectedTabPage.Name.Equals("tapMajor"))
            {
                values.Add("P_PERIODTYPE", "MajorSuppliers");
                //values.Add("P_PERIODTYPE", "OutSourcing");
            }
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtPeriod = await SqlExecuter.QueryAsync("GetOutsourcedClaimPeriod", "10001", values);
            if (dtPeriod.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                // 다국어 마감년 정보가 없습니다.
                ShowMessage("InValidOspData008"); // 
                if (tabClose.SelectedTabPage.Name.Equals("tapExpense"))
                {
                    DataTable dtPeriodClaim = (grdCostClosing.DataSource as DataTable).Clone();
                    grdCostClosing.DataSource = dtPeriodClaim;
                    txtperiod.Text = "";
                    _strPeriodstate = "";
                    _strPeriodid = "";
                }

                else if (tabClose.SelectedTabPage.Name.Equals("tapMajor"))
                {
                    DataTable dtMajor = (grdCostClosingMajor.DataSource as DataTable).Clone();
                    grdCostClosingMajor.DataSource = dtMajor;
                    txtperiodMajor.Text = "";
                    _strPeriodstateMajor = ""; _strPeriodidMajor = "";
                }

                return;
            }

            string strWorktime = dtPeriod.Rows[0]["WORKTIME"].ToString();
            if (tabClose.SelectedTabPage.Name.Equals("tapExpense"))
            {
                txtperiod.Text = dtPeriod.Rows[0]["PERIODSTATE"].ToString();
                _strPeriodstate = dtPeriod.Rows[0]["PERIODSTATE"].ToString();
                _strPeriodid = dtPeriod.Rows[0]["PERIODID"].ToString();
                values.Add("P_PERIODID", _strPeriodid);
            }

            else if (tabClose.SelectedTabPage.Name.Equals("tapMajor"))
            {

                txtperiodMajor.Text = dtPeriod.Rows[0]["PERIODSTATE"].ToString();
                _strPeriodstateMajor = dtPeriod.Rows[0]["PERIODSTATE"].ToString();
                _strPeriodidMajor = dtPeriod.Rows[0]["PERIODID"].ToString();
                values.Add("P_PERIODID", _strPeriodidMajor);
            }

            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            values = Commons.CommonFunction.ConvertParameter(values);

            if (tabClose.SelectedTabPage.Name.Equals("tapExpense"))
            {
                DataTable dtSearch = await SqlExecuter.QueryAsync("GetOutsourcingCostClosingExpense", "10001", values);
                if (dtSearch.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.

                }
                grdCostClosing.DataSource = dtSearch;
            }

            else if (tabClose.SelectedTabPage.Name.Equals("tapMajor"))
            {
                DataTable dtSearch = await SqlExecuter.QueryAsync("GetOutsourcingCostClosingMajor", "10001", values);
                if (dtSearch.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.

                }

                grdCostClosingMajor.DataSource = dtSearch;
            }



        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            //1.site
            //InitializeConditionPopup_Plant();
            //2.마감년월 필수
            InitializeCondition_Yearmonth();
            //작업장.
             InitializeConditionPopup_OspAreaid();
            //3.협력사 
            InitializeConditionPopup_OspVendorid();
        }
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionPopup_Plant()
        {

            var planttxtbox = Conditions.AddComboBox("p_plantid", new SqlQuery("GetPlantList", "00001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
               .SetLabel("PLANT")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
               .SetPosition(0.2)
               .SetDefault(UserInfo.Current.Plant, "p_plantId") //기본값 설정 UserInfo.Current.Plant
               .SetIsReadOnly(true)
               .SetValidationIsRequired()
            ;
            //   

        }


        /// <summary>
        /// 마감년월
        /// </summary>
        private void InitializeCondition_Yearmonth()
        {
            DateTime dateNow = DateTime.Now;
            string strym = dateNow.ToString("yyyy-MM");
            var YearmonthDT = Conditions.AddDateEdit("p_yearmonth")
               .SetLabel("CLOSEYM")
               .SetDisplayFormat("yyyy-MM")
               .SetPosition(0.4)
               .SetDefault(strym)
               .SetValidationIsRequired()
            ;

        }
        /// <summary>
        /// 작업장
        /// </summary>
        private void InitializeConditionPopup_OspAreaid()
        {
            // 팝업 컬럼설정
            var vendoridPopupColumn = Conditions.AddSelectPopup("p_areaid", new SqlQuery("GetAreaidPopupListByOsp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID")
               .SetPopupLayout("AREANAME", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(520, 600)
               .SetLabel("AREANAME")
               .SetRelationIds("p_plantId")
               .SetPopupResultCount(1)
               .SetPosition(0.5);
            // 팝업 조회조건
            vendoridPopupColumn.Conditions.AddTextBox("P_AREANAME")
                .SetLabel("AREANAME");

            // 팝업 그리드
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("AREAID", 120);
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("AREANAME", 200);


        }
        /// <summary>
        /// 작업업체 .고객 조회조건
        /// </summary>
        private void InitializeConditionPopup_OspVendorid()
        {
            // 팝업 컬럼설정
            var vendoridPopupColumn = Conditions.AddSelectPopup("p_ospvendorid", new SqlQuery("GetVendorListByOsp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "OSPVENDORNAME", "OSPVENDORID")
               .SetPopupLayout("OSPVENDORID", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("OSPVENDORID")
               .SetPopupResultCount(1)
               .SetRelationIds("p_plantid")
               .SetPosition(0.6);

            // 팝업 조회조건
            vendoridPopupColumn.Conditions.AddTextBox("OSPVENDORNAME")
                .SetLabel("OSPVENDORNAME");

            // 팝업 그리드
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORID", 150)
                .SetValidationKeyColumn();
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORNAME", 200);

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

        private void OnSaveConfrimSearch()
        {

            var values = Conditions.GetValues();
            string stryearmonth = values["P_YEARMONTH"].ToString();
            DateTime dtyearmonth = Convert.ToDateTime(stryearmonth);
            values.Add("P_PERIODNAME", dtyearmonth.ToString("yyyy-MM"));
            if (tabClose.SelectedTabPage.Name.Equals("tapExpense"))
            {
                values.Add("P_PERIODTYPE", "OutSourcing");
            }

            else if (tabClose.SelectedTabPage.Name.Equals("tapMajor"))
            {
                values.Add("P_PERIODTYPE", "MajorSuppliers");
                // values.Add("P_PERIODTYPE", "OutSourcing");
            }
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtPeriod = SqlExecuter.Query("GetOutsourcedClaimPeriod", "10001", values);
            if (dtPeriod.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                // 다국어 마감년 정보가 없습니다.
                ShowMessage("InValidOspData008"); // 
                if (tabClose.SelectedTabPage.Name.Equals("tapExpense"))
                {
                    DataTable dtPeriodClaim = (grdCostClosing.DataSource as DataTable).Clone();
                    grdCostClosing.DataSource = dtPeriodClaim;
                    txtperiod.Text = "";
                    _strPeriodstate = "";
                    _strPeriodid = "";
                }

                else if (tabClose.SelectedTabPage.Name.Equals("tapMajor"))
                {
                    DataTable dtMajor = (grdCostClosingMajor.DataSource as DataTable).Clone();
                    grdCostClosingMajor.DataSource = dtMajor;
                    txtperiodMajor.Text = "";
                    _strPeriodstateMajor = ""; _strPeriodidMajor = "";
                }

                return;
            }

            string strWorktime = dtPeriod.Rows[0]["WORKTIME"].ToString();
            if (tabClose.SelectedTabPage.Name.Equals("tapExpense"))
            {
                txtperiod.Text = dtPeriod.Rows[0]["PERIODSTATE"].ToString();
                _strPeriodstate = dtPeriod.Rows[0]["PERIODSTATE"].ToString();
                _strPeriodid = dtPeriod.Rows[0]["PERIODID"].ToString();

                values.Add("P_PERIODID", _strPeriodid);
            }

            else if (tabClose.SelectedTabPage.Name.Equals("tapMajor"))
            {

                txtperiodMajor.Text = dtPeriod.Rows[0]["PERIODSTATE"].ToString();
                _strPeriodstateMajor = dtPeriod.Rows[0]["PERIODSTATE"].ToString();
                _strPeriodidMajor = dtPeriod.Rows[0]["PERIODID"].ToString();
                values.Add("P_PERIODID", _strPeriodidMajor);
            }

            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            values = Commons.CommonFunction.ConvertParameter(values);

            if (tabClose.SelectedTabPage.Name.Equals("tapExpense"))
            {
                DataTable dtSearch = SqlExecuter.Query("GetOutsourcingCostClosingExpense", "10001", values);
                if (dtSearch.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.

                }
                grdCostClosing.DataSource = dtSearch;
            }

            else if (tabClose.SelectedTabPage.Name.Equals("tapMajor"))
            {
                DataTable dtSearch = SqlExecuter.Query("GetOutsourcingCostClosingMajor", "10001", values);
                if (dtSearch.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.

                }

                grdCostClosingMajor.DataSource = dtSearch;
            }


        }
        /// <summary>
        /// 복사시 기본테이블 생성
        /// </summary>
        /// <returns></returns>
        private DataTable createSaveHeaderDatatable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "listMain";
            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");
            dt.Columns.Add("PERIODID");
            dt.Columns.Add("PERIODNAME");
            dt.Columns.Add("PERIODTYPE");
            dt.Columns.Add("VENDORID");
            dt.Columns.Add("AREAID");
            return dt;
        }
        #endregion
        private bool blAggregateCheck()
        {
            int idatacount = 0;
            string strInterfaceNo = "";
            DataRow dr = null;
            var values = Conditions.GetValues();
            string stryearmonth = values["P_YEARMONTH"].ToString();
            DateTime dtyearmonth = Convert.ToDateTime(stryearmonth);
            if (tabClose.SelectedTabPage.Name.Equals("tapExpense"))
            {
                DataTable dtcheck = grdCostClosing.View.GetCheckedRows();
                //선택된 내역 체크
                if (dtcheck.Rows.Count == 0)
                {
                    ShowMessage("GridNoChecked");
                    return false;
                }
                if (_strPeriodstate.Equals("Close"))
                {
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspData010", "Close month " + dtyearmonth.ToString("yyyy-MM")); //메세지
                    return false;
                }
                for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                {
                    dr = dtcheck.Rows[irow];
                    strInterfaceNo = dr["INTERFACENO"].ToString();

                    if (!(strInterfaceNo.Equals("")))
                    {
                        string lblConsumabledefid = grdCostClosing.View.Columns["OSPVENDORNAME"].Caption.ToString();

                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspData016", lblConsumabledefid + dr["OSPVENDORNAME"].ToString());
                        return false;

                    }

                    idatacount = idatacount + 1;

                }
            }

            else if (tabClose.SelectedTabPage.Name.Equals("tapMajor"))
            {
                DataTable dtcheck = grdCostClosingMajor.View.GetCheckedRows();
                //선택된 내역 체크
                if (dtcheck.Rows.Count == 0)
                {
                    ShowMessage("GridNoChecked");
                    return false;
                }
                if (_strPeriodstateMajor.Equals("Close"))
                {
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspData010", "Close month " + dtyearmonth.ToString("yyyy-MM")); //메세지
                    return false;
                }
                for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                {
                    dr = dtcheck.Rows[irow];
                    strInterfaceNo = dr["INTERFACENO"].ToString();

                    if (!(strInterfaceNo.Equals("")))
                    {
                        string lblConsumabledefid = grdCostClosingMajor.View.Columns["OSPVENDORNAME"].Caption.ToString();

                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspData016", lblConsumabledefid + dr["OSPVENDORNAME"].ToString());
                        return false;

                    }

                    idatacount = idatacount + 1;

                }
            }

            if (idatacount == 0)
            {
                this.ShowMessage(MessageBoxButtons.OK, "OspDataCountCheck", btnAggregate.Text);   //다국어 메세지 처리 
                return false;
            }
            return true;
        }

        private bool blCloseSendCheck()
        {
            int idatacount = 0;
            string strInterfaceNo = "";
            string strPeriodstate = "";
            DataRow dr = null;
            var values = Conditions.GetValues();
            string stryearmonth = values["P_YEARMONTH"].ToString();
            DateTime dtyearmonth = Convert.ToDateTime(stryearmonth);
            if (tabClose.SelectedTabPage.Name.Equals("tapExpense"))
            {
                DataTable dtcheck = grdCostClosing.View.GetCheckedRows();
                //선택된 내역 체크
                if (dtcheck.Rows.Count == 0)
                {
                    ShowMessage("GridNoChecked");
                    return false;
                }
                if (_strPeriodstate.Equals("Close"))
                {
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspData015", "Close month " + dtyearmonth.ToString("yyyy-MM")); //메세지
                    return false;
                }
                for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                {
                    dr = dtcheck.Rows[irow];
                    strInterfaceNo = dr["INTERFACENO"].ToString();
                    strPeriodstate = dr["PERIODSTATE"].ToString();
                    if (!(strInterfaceNo.Equals("")))
                    {
                        string lblConsumabledefid = grdCostClosing.View.Columns["OSPVENDORNAME"].Caption.ToString();

                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspData016", lblConsumabledefid + dr["OSPVENDORNAME"].ToString());
                        return false;

                    }
                    if (!(strPeriodstate.Equals("Close")))
                    {
                        //	먼저 마감처리 해야합니다.해당 작업이 불가능합니다.
                        string lblConsumabledefid = grdCostClosing.View.Columns["OSPVENDORNAME"].Caption.ToString();

                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspData015", lblConsumabledefid + dr["OSPVENDORNAME"].ToString());

                        return false;
                    }
                    idatacount = idatacount + 1;

                }
            }

            else if (tabClose.SelectedTabPage.Name.Equals("tapMajor"))
            {
                DataTable dtcheck = grdCostClosingMajor.View.GetCheckedRows();
                //선택된 내역 체크
                if (dtcheck.Rows.Count == 0)
                {
                    ShowMessage("GridNoChecked");
                    return false;
                }
                if (_strPeriodstateMajor.Equals("Close"))
                {
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspData010", "Close month " + dtyearmonth.ToString("yyyy-MM")); //메세지
                    return false;
                }
                for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                {
                    dr = dtcheck.Rows[irow];
                    strInterfaceNo = dr["INTERFACENO"].ToString();
                    strPeriodstate = dr["PERIODSTATE"].ToString();
                    if (!(strInterfaceNo.Equals("")))
                    {
                        string lblConsumabledefid = grdCostClosingMajor.View.Columns["OSPVENDORNAME"].Caption.ToString();

                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspData016", lblConsumabledefid + dr["OSPVENDORNAME"].ToString());
                        return false;

                    }
                    if (!(strPeriodstate.Equals("Close")))
                    {
                        //	먼저 마감처리 해야합니다.해당 작업이 불가능합니다.
                        string lblConsumabledefid = grdCostClosingMajor.View.Columns["OSPVENDORNAME"].Caption.ToString();

                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspData015", lblConsumabledefid + dr["OSPVENDORNAME"].ToString());

                        return false;
                    }

                    idatacount = idatacount + 1;

                }
            }

            if (idatacount == 0)
            {
                this.ShowMessage(MessageBoxButtons.OK, "OspDataCountCheck", btnAggregate.Text);   //다국어 메세지 처리 
                return false;
            }
            return true;
        }
        private void txtperiod_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
