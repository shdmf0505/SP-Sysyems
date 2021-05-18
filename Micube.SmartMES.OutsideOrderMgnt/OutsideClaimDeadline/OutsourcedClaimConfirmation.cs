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
using DevExpress.XtraEditors.Repository;
using Micube.SmartMES.OutsideOrderMgnt.Popup;
#endregion

namespace Micube.SmartMES.OutsideOrderMgnt
{
    /// <summary>
    /// 프 로 그 램 명  :  외주관리 > 외주 Claim 마감 >Claim 대상 확정 및 마감
    /// 업  무  설  명  :  Claim 대상 확정 및 마감한다..
    /// 생    성    자  : choisstar
    /// 생    성    일  : 2019-08-28
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcedClaimConfirmation : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        private string _strPlantid = ""; // plant 변경시 작업 
        private string _strPeriodstate = "";
        #endregion

        #region 생성자

        public OutsourcedClaimConfirmation()
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

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
            // 작업용 plant 셋팅 (조회시 다시 셋팅)
            _strPlantid = UserInfo.Current.Plant.ToString();
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
           
            // TODO : 그리드 초기화 로직 추가
            grdClaimConfirm.GridButtonItem = GridButtonItem.Export;
            grdClaimConfirm.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdClaimConfirm.View.AddTextBoxColumn("ENTERPRISEID", 120).SetIsHidden();            //  
            grdClaimConfirm.View.AddTextBoxColumn("PERIODID", 120).SetIsHidden();            //  
            grdClaimConfirm.View.AddTextBoxColumn("VENDORID", 120).SetIsHidden();            //  
            grdClaimConfirm.View.AddTextBoxColumn("PLANTID", 120).SetIsHidden();            // 
            grdClaimConfirm.View.AddTextBoxColumn("WORKGUBUN", 120).SetIsHidden();            // 
            grdClaimConfirm.View.AddTextBoxColumn("CLAIMSEQUENCE", 120).SetIsHidden();            //
            grdClaimConfirm.View.AddTextBoxColumn("LOTHISTKEY", 120).SetIsHidden();            // 
            grdClaimConfirm.View.AddTextBoxColumn("CLAIMYN", 100)
                .SetDefault(false);
            grdClaimConfirm.View.AddTextBoxColumn("ISCLOSE", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsHidden(); //마감여부

            grdClaimConfirm.View.AddTextBoxColumn("PERIODNAME", 80)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddComboBoxColumn("PERIODSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=PeriodState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
              .SetDefault("Open")
              .SetIsHidden();
            grdClaimConfirm.View.AddComboBoxColumn("CLAIMTYPE", 120, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ClaimType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();       // 
            grdClaimConfirm.View.AddDateEditColumn("DEFINETIME", 120)
                .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime)
                .SetIsReadOnly();       // 
            grdClaimConfirm.View.AddDateEditColumn("OCCURTIME", 120)
                .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime)
                .SetIsReadOnly();       // 
            grdClaimConfirm.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 120)
                .SetIsReadOnly();// 
            grdClaimConfirm.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120)
                .SetIsReadOnly();       // 
            grdClaimConfirm.View.AddTextBoxColumn("REASONAREANAME", 120)
                .SetIsReadOnly();       // 
            grdClaimConfirm.View.AddTextBoxColumn("OSPVENDORNAME", 120)
                .SetIsReadOnly();       // 
            grdClaimConfirm.View.AddTextBoxColumn("PRODUCTDEFID", 120)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("PRODUCTDEFNAME", 200)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("LOTID", 200)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("CLAIMPRICE", 120)
                .SetIsHidden();       // 
            grdClaimConfirm.View.AddTextBoxColumn("DEFECTCODE", 100)
                .SetIsReadOnly();
           
            grdClaimConfirm.View.AddTextBoxColumn("DEFECTNAME", 120)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("DEFECTQTY", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("DEFECTAMOUNT", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("OSPREDUCEQTY", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("REDUCEQTYAMOUNT", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)  
                .SetIsReadOnly();        // 
            grdClaimConfirm.View.AddTextBoxColumn("OSPREDUCERATE", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("REDUCERATEAMOUNT", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();

            grdClaimConfirm.View.AddTextBoxColumn("CLAIMAMOUNT", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("FINDPROCESSSEGMENTNAME", 120)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("CREATORNAME", 120)
                .SetIsReadOnly();

            grdClaimConfirm.View.AddTextBoxColumn("DEFINEUSERNAME", 120)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("DESCRIPTION", 200)
                .SetIsReadOnly();
            grdClaimConfirm.View.PopulateColumns();

            //RepositoryItemCheckEdit repositoryCheckEdit1 = grdClaimConfirm.View.GridControl.RepositoryItems.Add("CheckEdit") as RepositoryItemCheckEdit;
            //repositoryCheckEdit1.ValueChecked = "Y";
            //repositoryCheckEdit1.ValueUnchecked = "N";
            //repositoryCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            //grdClaimConfirm.View.Columns["CLAIMYN"].ColumnEdit = repositoryCheckEdit1;
            
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            ////grdList.View.AddingNewRow += View_AddingNewRow;
            //마감관련
            btnClosePeriod.Click += BtnClosePeriod_Click;
            // 감면처리 
            btnReduction.Click += BtnReduction_Click;

            btnSave.Click += BtnSave_Click;

        }
        /// <summary>
        /// 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            grdClaimConfirm.View.FocusedRowHandle = grdClaimConfirm.View.FocusedRowHandle;
            grdClaimConfirm.View.FocusedColumn = grdClaimConfirm.View.Columns["OSPVENDORNAME"];
            grdClaimConfirm.View.ShowEditor();
            DataTable changed = grdClaimConfirm.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK,"NoSaveData");
                return;
            }
            //마감여부 확인 하여 처리 해야함.
            //InValidOspData012 메세지 월마감되었습니다.해당 작업이 불가능합니다.
            DataRow dr = null;
            string strPeriodstate = "";

            for (int irow = 0; irow < changed.Rows.Count; irow++)
            {
                dr = changed.Rows[irow];
                strPeriodstate = dr["PERIODSTATE"].ToString();
                if (strPeriodstate.Equals("Close"))
                {
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspData012");
                    return;
                }
            }
                //상태값 체크 여부 추가 (의뢰만)
                DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnSave.Text);//저장하시겠습니까? 
          
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnSave.Enabled = false;
                    btnClosePeriod.Enabled = false;
                    btnReduction.Enabled = false;

                    DataTable dtsave = grdClaimConfirm.GetChangedRows();

                    ExecuteRule("OutsourcedClaimConfirmation", dtsave);

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
                    btnClosePeriod.Enabled = true;
                    btnReduction.Enabled = true;
                    btnSave.Enabled = true;
                  

                }
            }

        }

        /// <summary>
        /// 감면처리 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReduction_Click(object sender, EventArgs e)
        {
            string strReduceqty = txtReduceqty.Text.Trim().ToString();
            double decReduceqty = (strReduceqty.ToString().Equals("") ? 0 : Convert.ToDouble(strReduceqty)); //
            string strReducerate = txtReducerate.Text.Trim().ToString();
            double decReducerate = (strReducerate.ToString().Equals("") ? 0 : Convert.ToDouble(strReducerate)); //
            //if (decReduceqty.Equals(0) && decReducerate.Equals(0))
            //{
            //    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblReduceqty.Text); //메세지 
            //    txtReduceqty.Focus();
            //    return;
            //}
            DataTable dtcheck = grdClaimConfirm.View.GetCheckedRows();
            
            if (dtcheck.Rows.Count == 0)
            {
                ShowMessage("GridNoChecked"); //(선택된 내역이 없습니다)/
                return;
            }
            //마감여부 체크 해야함. 마감된 것은 수정이 불가능합.
            //InValidOspData012 메세지 월마감되었습니다.해당 작업이 불가능합니다.
            DataRow dr = null;
            string strPeriodstate = "";


            for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
            {
                dr = dtcheck.Rows[irow];
                strPeriodstate = dr["PERIODSTATE"].ToString();
                if (strPeriodstate.Equals("Close"))
                {
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspData012");
                    return;
                }
            }
            var checkedRowHandles = grdClaimConfirm.View.GetCheckedRowsHandle();
            string strWorkgubun = "";
           
            checkedRowHandles.ForEach(rowHandle =>
            {
                DataRow row = this.grdClaimConfirm.View.GetDataRow(rowHandle);

                if (row != null)
                {
                    strWorkgubun = row["WORKGUBUN"].ToString();

                    if (strWorkgubun.Equals("LOTCLAIMDEFECT"))
                    {
                        string strDefectqty = row["DEFECTQTY"].ToString();
                        double decDefectqty = (strDefectqty.ToString().Equals("") ? 0 : Convert.ToDouble(strDefectqty)); //
                        string strClaimprice = row["CLAIMPRICE"].ToString();
                        double decClaimprice = (strClaimprice.ToString().Equals("") ? 0 : Convert.ToDouble(strClaimprice)); //
                        string strDefectamount = row["DEFECTAMOUNT"].ToString();
                        double decDefectamount = (strDefectamount.ToString().Equals("") ? 0 : Convert.ToDouble(strDefectamount)); //
                        if (decReduceqty > 0)
                        {
                            if (decReduceqty > decDefectqty)
                            {
                                row["OSPREDUCEQTY"] = decDefectqty;
                                row["REDUCEQTYAMOUNT"] =Math.Truncate( decDefectqty * decClaimprice);
                            }
                            else
                            {
                                row["OSPREDUCEQTY"] = decReduceqty;
                                row["REDUCEQTYAMOUNT"] = Math.Truncate(decReduceqty * decClaimprice);
                            }
                        }
                        else
                        {
                            row["OSPREDUCEQTY"] = 0;
                            row["REDUCEQTYAMOUNT"] = 0;
                        }
                        string strReduceqtyamount = row["REDUCEQTYAMOUNT"].ToString();
                        double decReduceqtyamount = (strReduceqtyamount.ToString().Equals("") ? 0 : Convert.ToDouble(strReduceqtyamount)); //
                        if (decReducerate > 0)
                        {
                            row["OSPREDUCERATE"] = decReducerate;
                            row["REDUCERATEAMOUNT"] =  Math.Truncate((decReducerate) * (0.01) * (decDefectamount- decReduceqtyamount));
                        }
                        else
                        {
                            row["OSPREDUCERATE"] = 0;
                            row["REDUCERATEAMOUNT"] = 0;
                        }
                        string strReducerateamount = row["REDUCERATEAMOUNT"].ToString();
                        double decReducerateamount = (strReducerateamount.ToString().Equals("") ? 0 : Convert.ToDouble(strReducerateamount)); //
                        double decClaimamount = decDefectamount - decReduceqtyamount - decReducerateamount;
                        if (decClaimamount<0 )
                        {
                            row["CLAIMAMOUNT"] = 0 ;
                        }
                        else
                        {
                            row["CLAIMAMOUNT"] = decDefectamount - decReduceqtyamount - decReducerateamount;
                        }
                        row["DESCRIPTION"] = txtDescription.Text;
                    }
                }
            });
            

        }
        /// <summary>
        /// 마감관리 팝업으로 이동 처리 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClosePeriod_Click(object sender, EventArgs e)
        {
            var values = Conditions.GetValues();
            string strPlantid = values["P_PLANTID"].ToString();
            ClaimClosePeriodPopup itemPopup = new ClaimClosePeriodPopup(strPlantid);
            itemPopup.ShowDialog(this);
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

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

        }
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Reduction"))
            {

                BtnReduction_Click(null, null);
            }
            if (btn.Name.ToString().Equals("ReductionSave"))
            {

                 BtnSave_Click(null, null);
            }
            if (btn.Name.ToString().Equals("Closeperiod"))
            {

                 BtnClosePeriod_Click(null, null);
            }
            if (btn.Name.ToString().Equals("CloseCheck"))
            {

                ProcCloseCheck(btn.Text);
            }
            if (btn.Name.ToString().Equals("ReductionSave"))
            {

                BtnReduction_Click(null, null);
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

            //// TODO : 조회 SP 변경
            
            var values = Conditions.GetValues();
           
            if (values["P_YEARMONTH"] != null)
            {
                string stryearmonth = values["P_YEARMONTH"].ToString();
                DateTime dtyearmonth = Convert.ToDateTime(stryearmonth);
                values.Add("P_PERIODNAME", dtyearmonth.ToString("yyyy-MM"));
                values.Add("P_PERIODTYPE", "Claim");
            }
            
           
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("P_PERIODTYPEOSP", "OutSourcing");
            #region 기간 검색형 전환 처리 
            if (!(values["P_PERIOD_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_PERIOD_PERIODFR"]);
                values.Remove("P_PERIOD_PERIODFR");
                string strFr = string.Format("{0:yyyy-MM-dd}", requestDateFr);
                values.Add("P_PERIOD_PERIODFR", strFr);
            }
            if (!(values["P_PERIOD_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_PERIOD_PERIODTO"]);
               // requestDateTo = requestDateTo.AddDays(1);
                values.Remove("P_PERIOD_PERIODTO");
                string strTo = string.Format("{0:yyyy-MM-dd}", requestDateTo);
                values.Add("P_PERIOD_PERIODTO", strTo);
            }


            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtClaim = await SqlExecuter.QueryAsync("GetOutsourcedClaimConfirmation", "10001", values);
            if (dtClaim.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                
            }
            grdClaimConfirm.DataSource = dtClaim;

           
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            //1 SITE
            //InitializeConditionPopup_Plant();
            //2.마감년월
            InitializeCondition_Yearmonth();

            //3.발생일자

            //4.원인공장
            InitializeConditionPopup_Factory();
            //5.원인작업장
            InitializeConditionPopup_Areaid();
            //6.원인업체
            InitializeConditionPopup_OspVendorid();
            //7.품목코드
            InitializeConditionPopup_Product();
            //8.lottype구분
            InitializeCondition_LotType();
            //9.LOT
           
            InitializeCondition_Lotid();
            //10.감면여부
            InitializeCondition_ReduceYesNo();
            ////11.마감포함여부
            //InitializeCondition_CloseYesNo();
        }
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionPopup_Plant()
        {

            var planttxtbox = Conditions.AddComboBox("p_plantid", new SqlQuery("GetPlantList", "00001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
               .SetLabel("PLANT")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
               .SetPosition(0.1)
               .SetDefault(UserInfo.Current.Plant, "p_plantId")
               .SetIsReadOnly()
               .SetValidationIsRequired();
            ;

        }
        /// <summary>
        /// 기준년월
        /// </summary>
        private void InitializeCondition_Yearmonth()
        {
            DateTime dateNow = DateTime.Now;
            string strym = dateNow.ToString("yyyy-MM");
            var YearmonthDT = Conditions.AddDateEdit("p_yearmonth")
               .SetLabel("CLOSEYM")
               .SetDisplayFormat("yyyy-MM")
               .SetValidationIsRequired()
               .SetPosition(0.2)
            ;

        }

        /// <summary>
        /// REASONfactory 설정 
        /// </summary>
        private void InitializeConditionPopup_Factory()
        {
            var plantCbobox = Conditions.AddComboBox("p_factoryid", new SqlQuery("GetFactoryListByCsm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "FACTORYNAME", "FACTORYID")
              .SetLabel("FACTORYNAME")
              //.SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
              .SetPosition(2.1)
              
              .SetRelationIds("p_plantid");
        }
   
        /// <summary>
        /// 작업장 설정 
        /// </summary>
        private void InitializeConditionPopup_Areaid()
        {   /// GetAreaidListByOsp
            var popupProduct = Conditions.AddSelectPopup("p_areaid",
                                                               new SqlQuery("GetAreaidPopupListByOsp", "10001"
                                                                               , $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"
                                                                               , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                               ), "AREANAME", "AREAID")
            .SetPopupLayout("AREAID", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(450, 600)
            .SetLabel("AREANAME")
            .SetPopupResultCount(1)
            .SetRelationIds("p_plantid")
            .SetPosition(2.2);
            // 팝업 조회조건
            popupProduct.Conditions.AddTextBox("AREANAME")
                .SetLabel("AREANAME");
            popupProduct.GridColumns.AddTextBoxColumn("AREAID", 100)
                .SetValidationKeyColumn();
            popupProduct.GridColumns.AddTextBoxColumn("AREANAME", 200);

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
               .SetRelationIds("p_plantid", "p_areaid")
               .SetPopupResultCount(1)
               .SetPosition(2.3);
            // 팝업 조회조건
            vendoridPopupColumn.Conditions.AddTextBox("OSPVENDORNAME")
                .SetLabel("OSPVENDORNAME");
            // 팝업 그리드
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORID", 150)
                .SetValidationKeyColumn();
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORNAME", 200);

            var txtOspVendor = Conditions.AddTextBox("P_OSPVENDORNAME")
             .SetLabel("OSPVENDORNAME")
             .SetPosition(2.4);
        }

        /// <summary>
        /// Product 설정 
        /// </summary>
        private void InitializeConditionPopup_Product()
        {
            var popupProduct = Conditions.AddSelectPopup("p_productcode",
                                                               new SqlQuery("GetProductdefidlistByOsp", "10001"
                                                                               , $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"
                                                                               , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                               ), "PRODUCTDEFNAME", "PRODUCTDEFCODE")
                .SetPopupLayout("PRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(650, 600)
                .SetLabel("PRODUCTDEFID")
                .SetPopupResultCount(1)
                .SetPosition(3.2);

            // 팝업 조회조건
            popupProduct.Conditions.AddComboBox("P_PRODUCTDEFTYPE", new SqlQuery("GetCodeListByOspProductdeftype", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("PRODUCTDEFTYPE")
                .SetDefault("Product");
            popupProduct.Conditions.AddTextBox("PRODUCTDEFNAME")
                .SetLabel("PRODUCTDEFID");
            // 팝업 그리드
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFCODE", 150)
                .SetIsHidden();
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetValidationKeyColumn();
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100)
               .SetValidationKeyColumn();
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);

        }
        /// <summary>
        /// 양산구분
        /// </summary>
        private void InitializeCondition_LotType()
        {
            var LotTypecbobox = Conditions.AddComboBox("p_lottype", new SqlQuery("GetCodeList", "00001", $"CODECLASSID=LotType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("LOTTYPE")
                .SetPosition(3.3)

                .SetEmptyItem("", "")
             ;
        }
        /// <summary>
        /// lotid 조회조건
        /// </summary>
        private void InitializeCondition_Lotid()
        {
            var txtLotid = Conditions.AddTextBox("p_lotid")
               .SetLabel("LOTID")
               .SetPosition(3.4);
        }
        /// <summary>
        /// 감면여부
        /// </summary>
        private void InitializeCondition_ReduceYesNo()
        {
            var YesNobox = Conditions.AddComboBox("p_ReduceYesNo", new SqlQuery("GetCodeList", "00001", $"CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("ReduceYesNo")
                .SetPosition(3.6)
                .SetEmptyItem("전체")
             ;
        }

        /// <summary>
        /// 확인여부
        /// </summary>
        private void InitializeCondition_CloseYesNo()
        {
            var YesNobox = Conditions.AddComboBox("p_Closeyesno", new SqlQuery("GetCodeList", "00001", $"CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("CLOSEYESNO")
                .SetPosition(3.8)
                .SetEmptyItem("전체")
             ;
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            InitializeDatePeriod();
        }
        /// <summary>
        /// 기간 포맷 재정의 
        /// </summary>
        private void InitializeDatePeriod()
        {

            InitializeDatePeriodSetting("P_PERIOD");

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

        private void ProcCloseCheck(string strtitle)
        {

            DataTable dtcheck = grdClaimConfirm.View.GetCheckedRows();

            if (dtcheck.Rows.Count == 0)
            {
                ShowMessage("GridNoChecked"); //(선택된 내역이 없습니다)/
                return;
            }

            var checkedRowHandles = grdClaimConfirm.View.GetCheckedRowsHandle();
          
            checkedRowHandles.ForEach(rowHandle =>
            {
                DataRow row = this.grdClaimConfirm.View.GetDataRow(rowHandle);

                if (row != null)
                {
                     row["CLAIMYN"] = "Y";
                    
                }
            });


        }

        /// <summary>
        /// 기간 포맷 재정의 
        /// </summary>
        private void InitializeDatePeriodSetting(string sPeriodname)
        {
            // 기간 포맷 재정의 
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add(sPeriodname, "CUSTOM");
            values.Add(sPeriodname + "_PERIODFR", "");
            values.Add(sPeriodname + "_PERIODTO", "");

            Conditions.GetControl<SmartPeriodEdit>(sPeriodname).SetValue(values);

        }

        private void OnSaveConfrimSearch()
        {

            var values = Conditions.GetValues();
            if (values["P_YEARMONTH"] != null)
            {
                string stryearmonth = values["P_YEARMONTH"].ToString();
                DateTime dtyearmonth = Convert.ToDateTime(stryearmonth);
                values.Add("P_PERIODNAME", dtyearmonth.ToString("yyyy-MM"));
                values.Add("P_PERIODTYPE", "Claim");
            }
          
           
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("P_PERIODTYPEOSP", "OutSourcing");
            #region 기간 검색형 전환 처리 
            if (!(values["P_PERIOD_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_PERIOD_PERIODFR"]);
                values.Remove("P_PERIOD_PERIODFR");
                string strFr = string.Format("{0:yyyy-MM-dd}", requestDateFr);
                values.Add("P_PERIOD_PERIODFR", strFr);
            }
            if (!(values["P_PERIOD_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_PERIOD_PERIODTO"]);
                //requestDateTo = requestDateTo.AddDays(1);
                values.Remove("P_PERIOD_PERIODTO");
                string strTo = string.Format("{0:yyyy-MM-dd}", requestDateTo) ;
                values.Add("P_PERIOD_PERIODTO", strTo);
            }


            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtClaim =  SqlExecuter.Query("GetOutsourcedClaimConfirmation", "10001", values);
            if (dtClaim.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.

            }
            grdClaimConfirm.DataSource = dtClaim;



        }


        #endregion


    }
}
