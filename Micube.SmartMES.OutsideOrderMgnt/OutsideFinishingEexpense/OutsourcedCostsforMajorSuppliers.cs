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
    /// 프 로 그 램 명  : 외주관리 > 외주가공비마감 > 주요 협력사 가공비 등록
    /// 업  무  설  명  : 주요 협력사 가공비 등록한다.
    /// 생    성    자  : choisstar
    /// 생    성    일  : 2019-08-13
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcedCostsforMajorSuppliers : SmartConditionManualBaseForm
    {
        #region Local Variables


        private string _strPeriodstate = "";
        #endregion

        #region 생성자

        public OutsourcedCostsforMajorSuppliers()
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
            grdClaimConfirm.View.AddTextBoxColumn("PERIODNAME", 120).SetIsHidden();            //  
            grdClaimConfirm.View.AddTextBoxColumn("VENDORID", 120).SetIsHidden();            //  
            grdClaimConfirm.View.AddTextBoxColumn("PLANTID", 120).SetIsHidden();            // 
            grdClaimConfirm.View.AddTextBoxColumn("AREAID", 120).SetIsHidden();            // 
            grdClaimConfirm.View.AddTextBoxColumn("SETTLEUSER", 120).SetIsHidden();            //  
            grdClaimConfirm.View.AddComboBoxColumn("PERIODSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=PeriodState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Open")
                .SetIsReadOnly();       
            grdClaimConfirm.View.AddTextBoxColumn("OSPVENDORNAME", 120)
                .SetIsReadOnly();       // 
           
            grdClaimConfirm.View.AddTextBoxColumn("PERFORMANCECNT", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("PERFORMANCEAMOUNT", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("ISEXCEPTCNT", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly(); 
            grdClaimConfirm.View.AddTextBoxColumn("ISERRORCNT", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("MAJORVENDORAMOUNT", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
              
            grdClaimConfirm.View.AddTextBoxColumn("VENDORAMOUNT", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();        // 
            grdClaimConfirm.View.AddTextBoxColumn("VENDORCNT", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("ETCVENDORAMOUNT", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric)
                .SetIsReadOnly();
            grdClaimConfirm.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 정산내역조회 
            btnSettlemantList.Click += BtnSettlemantList_Click;
            //저장 
            btnSave.Click += BtnSave_Click;
            //정산처리 
            btnSettlemantProcess.Click += BtnSettlemantProcess_Click;
            //그리드 lock 
            grdClaimConfirm.View.ShowingEditor += View_ShowingEditor;
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
            if (grdClaimConfirm.View.DataRowCount ==0)
            {
                return;
            }
          
            String strperiodid = grdClaimConfirm.View.GetFocusedRowCellValue("PERIODID").ToString();
            String strplantid = grdClaimConfirm.View.GetFocusedRowCellValue("PLANTID").ToString();
            String strAreaid = grdClaimConfirm.View.GetFocusedRowCellValue("AREAID").ToString();
            String strAreaname = grdClaimConfirm.View.GetFocusedRowCellValue("AREANAME").ToString();
            String strvendorid = grdClaimConfirm.View.GetFocusedRowCellValue("VENDORID").ToString();
            String strvendorname = grdClaimConfirm.View.GetFocusedRowCellValue("OSPVENDORNAME").ToString();
            String strperiodname = grdClaimConfirm.View.GetFocusedRowCellValue("PERIODNAME").ToString();
            OutsourcedCostsforMajorSuppliersPopup itemPopup = new OutsourcedCostsforMajorSuppliersPopup(strperiodid, strplantid, strvendorid, strAreaid ,strvendorname, strAreaname, strperiodname);
            itemPopup.ShowDialog(this);
           
        }

        /// <summary>
        /// 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            DataTable changed = grdClaimConfirm.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnSave.Text);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnSave.Enabled = false;
                    btnSettlemantList.Enabled = false;
                    btnSettlemantProcess.Enabled = false;
                    DataTable dtSave = grdClaimConfirm.GetChangedRows();
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
        /// 정산처리 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSettlemantProcess_Click(object sender, EventArgs e)
        {
            DataTable dtcheck = grdClaimConfirm.View.GetCheckedRows();

            if (dtcheck.Rows.Count == 0)
            {
                ShowMessage("GridNoChecked"); //(선택된 내역이 없습니다)/
                return;
            }
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
                    DataTable dtSave = grdClaimConfirm.View.GetCheckedRows();
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
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            
        }
        /// <summary>
        /// 셀 ReadOnly 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_ShowingEditor(object sender, CancelEventArgs e)
        {

            if (!(grdClaimConfirm.View.GetFocusedRowCellValue("PERIODSTATE").ToString().Equals("Open")))
            {
                e.Cancel = true;
            }
 
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
            values.Add("P_PERIODTYPE", "MajorSuppliers");
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtPeriod = await SqlExecuter.QueryAsync("GetOutsourcedClaimPeriod", "10001", values);
            if (dtPeriod.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                // 다국어 마감년 정보가 없습니다.
                ShowMessage("InValidOspData008"); // 
                DataTable dtPeriodClaim = (grdClaimConfirm.DataSource as DataTable).Clone();
                grdClaimConfirm.DataSource = dtPeriodClaim;
                txtperiod.Text = "";
                _strPeriodstate = "";
                btnSave.Enabled = false;
                return;
            }
            _strPeriodstate = dtPeriod.Rows[0]["PERIODSTATE"].ToString();
            string strWorktime = dtPeriod.Rows[0]["WORKTIME"].ToString();
            txtperiod.Text = dtPeriod.Rows[0]["PERIODDESCRIPTION"].ToString();
            values.Add("P_STARTDATE", dtPeriod.Rows[0]["STARTDATE"].ToString());
            values.Add("P_ENDDATE", dtPeriod.Rows[0]["ENDDATE"].ToString());
            DateTime dtStartTrans = Convert.ToDateTime(dtPeriod.Rows[0]["STARTTIME"].ToString());
            DateTime dtEndTrans = Convert.ToDateTime(dtPeriod.Rows[0]["ENDTIME"].ToString());
            values.Add("P_STARTTIME", dtStartTrans.ToString("yyyy-MM-dd HH:mm:ss"));
            values.Add("P_ENDTIME", dtEndTrans.ToString("yyyy-MM-dd HH:mm:ss"));
            values.Add("P_PERIODID", dtPeriod.Rows[0]["PERIODID"].ToString());
        
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtClaim = await SqlExecuter.QueryAsync("GetOutsourcedCostsforMajorSuppliers", "10001", values);
            if (dtClaim.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.

            }
            grdClaimConfirm.DataSource = dtClaim;

            if (_strPeriodstate.Equals("Open"))
            {
                btnSave.Enabled = true;
                btnSettlemantProcess.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
                btnSettlemantProcess.Enabled = false;
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            // site
            //InitializeConditionPopup_Plant();
            // 기간 
            InitializeCondition_Yearmonth();
            //협력사
            InitializeConditionPopup_OspVendorid();
        }
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionPopup_Plant()
        {
            var plantCbobox = Conditions.AddComboBox("p_plantid", new SqlQuery("GetPlantList", "00001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
              .SetLabel("PLANT")
              .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
              .SetPosition(0.1)
              .SetDefault(UserInfo.Current.Plant, "p_plantId") //기본값 설정 UserInfo.Current.Plant
              .SetValidationIsRequired()
              .SetIsReadOnly();

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
        /// <summary>
        /// 저장 후 재조회용 
        /// </summary>

        private void OnSaveConfrimSearch()
        {

            var values = Conditions.GetValues();
            string stryearmonth = values["P_YEARMONTH"].ToString();
            DateTime dtyearmonth = Convert.ToDateTime(stryearmonth);
            values.Add("P_PERIODNAME", dtyearmonth.ToString("yyyy-MM"));
            values.Add("P_PERIODTYPE", "MajorSuppliers");
            DataTable dtPeriod = SqlExecuter.Query("GetOutsourcedClaimPeriod", "10001", values);
            values = Commons.CommonFunction.ConvertParameter(values);
            if (dtPeriod.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                // 다국어 마감년 정보가 없습니다.
                ShowMessage("InValidOspData008"); // 
                DataTable dtPeriodClaim = (grdClaimConfirm.DataSource as DataTable).Clone();
                grdClaimConfirm.DataSource = dtPeriodClaim;
                txtperiod.Text = "";
                _strPeriodstate = "";
                btnSave.Enabled = false;
                return;
            }
            _strPeriodstate = dtPeriod.Rows[0]["PERIODSTATE"].ToString();
            string strWorktime = dtPeriod.Rows[0]["WORKTIME"].ToString();
            txtperiod.Text = dtPeriod.Rows[0]["PERIODDESCRIPTION"].ToString();
            values.Add("P_STARTDATE", dtPeriod.Rows[0]["STARTDATE"].ToString());
            values.Add("P_ENDDATE", dtPeriod.Rows[0]["ENDDATE"].ToString());
            DateTime dtStartTrans = Convert.ToDateTime(dtPeriod.Rows[0]["STARTTIME"].ToString());
            DateTime dtEndTrans = Convert.ToDateTime(dtPeriod.Rows[0]["ENDTIME"].ToString());
            values.Add("P_STARTTIME", dtStartTrans.ToString("yyyy-MM-dd HH:mm:ss"));
            values.Add("P_ENDTIME", dtEndTrans.ToString("yyyy-MM-dd HH:mm:ss"));
            values.Add("P_PERIODID", dtPeriod.Rows[0]["PERIODID"].ToString());

            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtClaim = SqlExecuter.Query("GetOutsourcedCostsforMajorSuppliers", "10001", values);
            if (dtClaim.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.

            }
            grdClaimConfirm.DataSource = dtClaim;

            if (_strPeriodstate.Equals("Open"))
            {
                btnSave.Enabled = true;
                btnSettlemantProcess.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
                btnSettlemantProcess.Enabled = false;
            }


        }
        #endregion
    }
}
