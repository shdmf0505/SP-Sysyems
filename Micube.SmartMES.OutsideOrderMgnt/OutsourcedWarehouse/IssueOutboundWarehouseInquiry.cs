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
using System.Reflection;

using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraReports.UI;

using Micube.Framework.SmartControls.Grid.BandedGrid;

using System.IO;

#endregion

namespace Micube.SmartMES.OutsideOrderMgnt
{
    /// <summary>
    /// 프 로 그 램 명  :  외주관리> 외주창고 > 외주창고출고조회발행
    /// 업  무  설  명  : 외주창고출고조회발행한다.
    /// 생    성    자  : CHOISSTAR
    /// 생    성    일  : 2019-07-05
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class IssueOutboundWarehouseInquiry : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        
       

        #endregion

        #region 생성자

        public IssueOutboundWarehouseInquiry()
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

            grdIssueOutboundWarehouseInquiry.GridButtonItem =  GridButtonItem.Export;

            grdIssueOutboundWarehouseInquiry.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdIssueOutboundWarehouseInquiry.View.AddTextBoxColumn("ENTERPRISEID", 120)
                .SetIsHidden();                                                               //  회사 ID
            grdIssueOutboundWarehouseInquiry.View.AddTextBoxColumn("PLANTID", 120)
                .SetIsHidden();                                                               //  공장 ID
            grdIssueOutboundWarehouseInquiry.View.AddTextBoxColumn("RECEIPTUSER", 120)
                .SetIsHidden();
           // grdIssueOutboundWarehouseInquiry.View.AddTextBoxColumn("OSPSENDUSER", 120)
           //     .SetIsHidden();
            grdIssueOutboundWarehouseInquiry.View.AddTextBoxColumn("PRINTUSER", 120)
                .SetIsHidden();
            grdIssueOutboundWarehouseInquiry.View.AddTextBoxColumn("LOTHISTKEY", 120)
                .SetIsHidden();                                                               //  LOTHISTKEY
            grdIssueOutboundWarehouseInquiry.View.AddTextBoxColumn("RECEIPTSEQUENCE", 120)
                .SetIsHidden();
            grdIssueOutboundWarehouseInquiry.View.AddTextBoxColumn("LOTID", 200)
                .SetIsReadOnly();                                                             //  LOTID
            grdIssueOutboundWarehouseInquiry.View.AddTextBoxColumn("SENDDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly();
            grdIssueOutboundWarehouseInquiry.View.AddTextBoxColumn("OSPPRTSENDUSERNAME", 100)
                 .SetIsReadOnly();                                                             // 입고자// 출고일 
            grdIssueOutboundWarehouseInquiry.View.AddTextBoxColumn("PRINTCOUNT", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                  //인쇄 횟수
            grdIssueOutboundWarehouseInquiry.View.AddTextBoxColumn("RECEIPTDATE", 80)
                .SetDisplayFormat("yyyy-MM-dd")
                .SetIsReadOnly();                                                              // 입고일 
            grdIssueOutboundWarehouseInquiry.View.AddTextBoxColumn("RECEIPTUSERNAME", 120)
                 .SetIsReadOnly();                                                             // 입고자
            grdIssueOutboundWarehouseInquiry.View.AddComboBoxColumn("LOTTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=LotType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();                                                             //  양산구분               
            grdIssueOutboundWarehouseInquiry.View.AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsReadOnly();                                                             //  제품 정의 ID
            grdIssueOutboundWarehouseInquiry.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly();                                                             //  제품 정의 Version
            grdIssueOutboundWarehouseInquiry.View.AddTextBoxColumn("PRODUCTDEFNAME", 200)
                .SetIsReadOnly();                                                             //  제품명
            grdIssueOutboundWarehouseInquiry.View.AddTextBoxColumn("PROCESSSEGMENTID", 120)
                .SetIsHidden();                                                             //  공정 ID
            grdIssueOutboundWarehouseInquiry.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)
                .SetIsReadOnly();                                                             //  공정명
            grdIssueOutboundWarehouseInquiry.View.AddTextBoxColumn("AREAID", 120)
                .SetIsHidden();                                                             //  작업장 AREAID
            grdIssueOutboundWarehouseInquiry.View.AddTextBoxColumn("AREANAME", 120)
                .SetIsReadOnly();                                                               //  작업장 AREAID
            grdIssueOutboundWarehouseInquiry.View.AddTextBoxColumn("PREVPROCESSSEGMENTID", 120)
                .SetIsHidden();                                                              //  이전공정 ID
            grdIssueOutboundWarehouseInquiry.View.AddTextBoxColumn("PREVPROCESSSEGMENTNAME", 150)
                .SetIsReadOnly();                                                             //  이전공정명
            grdIssueOutboundWarehouseInquiry.View.AddTextBoxColumn("PREVAREAID", 120)
                .SetIsHidden();                                                             //  이전 작업장 PREVAREAID
            grdIssueOutboundWarehouseInquiry.View.AddTextBoxColumn("PREVAREANAME", 120)
                .SetIsReadOnly();                                                               //  이전 작업장 PREVAREAID

            grdIssueOutboundWarehouseInquiry.View.AddTextBoxColumn("PCSQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  pcsqty
           
            grdIssueOutboundWarehouseInquiry.View.AddTextBoxColumn("PANELQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  panelqty
            grdIssueOutboundWarehouseInquiry.View.AddTextBoxColumn("OSPMM", 120)
                .SetIsHidden()
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric);                                //  panelqty

            grdIssueOutboundWarehouseInquiry.View.AddTextBoxColumn("PATHSEQUENCESTART", 120)
                .SetIsHidden();                                                              //  이전공정 ID
            grdIssueOutboundWarehouseInquiry.View.AddTextBoxColumn("PATHSEQUENCEEND", 120)
                .SetIsHidden();                                                              //  이전공정 ID
            grdIssueOutboundWarehouseInquiry.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            btnOutputslip.Click += BtnOutputslip_Click;
        }


        /// <summary>
        /// 저장 -기타 외주 작업 내역을 등록
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOutputslip_Click(object sender, EventArgs e)
        {
            // 선택값 그리드 

            DataTable dtcheck = grdIssueOutboundWarehouseInquiry.View.GetCheckedRows();

            if (dtcheck.Rows.Count == 0)
            {
                ShowMessage("GridNoChecked"); //(선택된 내역이 없습니다)/
                return;
            }
            
            // 건수 재비교 처리해야함.
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnOutputslip.Text);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnOutputslip.Enabled = false;


                    DataTable dt = (grdIssueOutboundWarehouseInquiry.DataSource as DataTable).Clone();

                    for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                    {
                        DataRow dr = dtcheck.Rows[irow];
                        dr["PRINTUSER"] = UserInfo.Current.Id.ToString();
                        dt.ImportRow(dr);
                        
                    }

                    this.ExecuteRule("OutsourcingIssueOutboundWarehouseInquiry", dt);
                    //createSaveDatatable
                  
                    DataTable dtprint = createSaveDatatable();
                    // ShowMessage("SuccessOspProcess");
                    for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                    {
                        DataRow dr = dtcheck.Rows[irow];
                        Dictionary<string, object> Param = new Dictionary<string, object>();

                        Param.Add("P_LOTHISTKEY", dr["LOTHISTKEY"]);
                        Param.Add("P_LOTID", dr["LOTID"]);
                        Param.Add("P_RECEIPTSEQUENCE", dr["RECEIPTSEQUENCE"]);
                        Param.Add("P_PATHSEQUENCESTART", dr["PATHSEQUENCESTART"]);
                        Param.Add("P_PATHSEQUENCEEND", dr["PATHSEQUENCEEND"]);
                       
                        Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                        Param = Commons.CommonFunction.ConvertParameter(Param);
                        DataTable dtSlip = SqlExecuter.Query("GetIssueOutboundWarehouseInquiryOutputslip", "10001", Param);

                        dtprint.Merge(dtSlip);
                    }
                    OutputslipSub(dtprint);
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();

                    btnOutputslip.Enabled = true;
                   
                }
            }

        }
        /// <summary>
        /// 출고전표 메인 
        /// </summary>
        /// <param name="dtprint"></param>
        private void OutputslipSub( DataTable dtprint)
        {
            Assembly assembly = Assembly.GetAssembly(this.GetType());
            Stream stream = assembly.GetManifestResourceStream("Micube.SmartMES.OutsideOrderMgnt.report.IssueOutboundWarehouseInquiry.repx");
            DataSet dsReport = new DataSet();
            DataTable header = new DataTable();
            header.Columns.Add(new DataColumn("LBLTITLE", typeof(string)));
            DataRow headerRow = header.NewRow();
            //headerRow["LBLTITLE"] =  Language.Get("RAWASSYIMPORTREPORTTITLE");
            header.Rows.Add(headerRow);
           

            dsReport.Tables.Add(header);
            dsReport.Tables.Add(dtprint);
            XtraReport report = XtraReport.FromStream(stream);
            report.DataSource = dsReport;
            report.DataMember = dsReport.Tables[1].TableName;
         
            Band band = report.Bands["Detail"];
            SetReportControlDataBinding(band.Controls, dsReport.Tables[1]);
            setLabelLaungage(band);

            //report.Print();
            //report.PrintingSystem.EndPrint += PrintingSystem_EndPrint1; ;
            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.ShowRibbonPreview();

        }
        /// <summary>
        /// Report 파일의 컨트롤 중 Tag(FieldName) 값이 있는 컨트롤에 DataBinding(Text)를 추가한다.
        /// </summary>
        private void SetReportControlDataBinding(XRControlCollection controls, DataTable dataSource)
        {
            if (controls.Count > 0)
            {
                foreach (XRControl control in controls)
                {
                    if (!string.IsNullOrWhiteSpace(control.Tag.ToString()) && !(control.Name.Substring(0, 3).Equals("lbl")))
                        control.DataBindings.Add("Text", dataSource, control.Tag.ToString());

                    SetReportControlDataBinding(control.Controls, dataSource);
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

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            //DataTable changed = grdList.GetChangedRows();

            // ExecuteRule("SaveCodeClass", changed);
        }
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Slipoutput"))
            {

                BtnOutputslip_Click(null, null);
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
            var values = Conditions.GetValues();

            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("USERID", UserInfo.Current.Id.ToString());
            #region 품목코드 전환 처리 
            string sproductcode = "";
            if (!(values["P_PRODUCTCODE"] == null))
            {
                sproductcode = values["P_PRODUCTCODE"].ToString();
            }
            // 품목코드값이 있으면
            if (!(sproductcode.Equals("")))
            {
                string[] sproductd = sproductcode.Split('|');
                // plant 정보 다시 가져오기 
                values.Add("P_PRODUCTDEFID", sproductd[0].ToString());
                values.Add("P_PRODUCTDEFVERSION", sproductd[1].ToString());
            }
            #endregion

            #region 기간 검색형 전환 처리 

            if (!(values["P_RECEIPTDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_RECEIPTDATE_PERIODFR"]);
                values.Remove("P_RECEIPTDATE_PERIODFR");
                values.Add("P_RECEIPTDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_RECEIPTDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_RECEIPTDATE_PERIODTO"]);
                values.Remove("P_RECEIPTDATE_PERIODTO");
                //requestDateTo = requestDateTo.AddDays(1);
                values.Add("P_RECEIPTDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }

            if (!(values["P_SENDDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime actualDateFr = Convert.ToDateTime(values["P_SENDDATE_PERIODFR"]);
                values.Remove("P_SENDDATE_PERIODFR");
                values.Add("P_SENDDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", actualDateFr));
            }
            if (!(values["P_SENDDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime actualDateTo = Convert.ToDateTime(values["P_SENDDATE_PERIODTO"]);
                values.Remove("P_SENDDATE_PERIODTO");
                //actualDateTo = actualDateTo.AddDays(1);
                values.Add("P_SENDDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", actualDateTo));
            }

            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtInquiry = await SqlExecuter.QueryAsync("GetIssueOutboundWarehouseInquiry", "10001", values); 

            if (dtInquiry.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdIssueOutboundWarehouseInquiry.DataSource = dtInquiry;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            // SITE
            //InitializeConditionPopup_Plant();
            // 입고자
            InitializeConditionPopup_Requestor();
            //출고자
            InitializeConditionPopup_Senduser();
            //품목코드
            InitializeConditionPopup_Product();
            //작업장
            InitializeConditionPopup_Areaid();
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
               .SetIsReadOnly(true)
               .SetDefault(UserInfo.Current.Plant, "p_plantId") //기본값 설정 UserInfo.Current.Plant
            ;
          

        }
        /// <summary>
        /// 의뢰자 조회조건
        /// </summary>
        private void InitializeConditionPopup_Requestor()
        {
            // 팝업 컬럼설정
            var requesterPopupColumn = Conditions.AddSelectPopup("p_recceiptuser", new SqlQuery("GetUseridListByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "USERNAME", "USERID")
               .SetPopupLayout("RECEIPTUSER", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(400, 600)
               .SetLabel("RECEIPTUSER")
               .SetPopupResultCount(1)
               .SetRelationIds("p_plantid")
               .SetPosition(1.1);

            // 팝업 조회조건
            requesterPopupColumn.Conditions.AddTextBox("USERNAME")
                .SetLabel("RECEIPTUSER");

            // 팝업 그리드
            requesterPopupColumn.GridColumns.AddTextBoxColumn("USERID", 150)
                .SetValidationKeyColumn();
            requesterPopupColumn.GridColumns.AddTextBoxColumn("USERNAME", 200);
        }

        /// <summary>
        /// 출고자 조회조건
        /// </summary>
        private void InitializeConditionPopup_Senduser()
        {
            // 팝업 컬럼설정
            var requesterPopupColumn = Conditions.AddSelectPopup("p_senduser", new SqlQuery("GetUseridListByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "USERNAME", "USERID")
               .SetPopupLayout("SENDUSER", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(400, 600)
               .SetLabel("SENDUSER")
               .SetPopupResultCount(1)
               .SetRelationIds("p_plantid")
               .SetPosition(2.1);

            // 팝업 조회조건
            requesterPopupColumn.Conditions.AddTextBox("USERNAME")
                .SetLabel("SENDUSER");

            // 팝업 그리드
            requesterPopupColumn.GridColumns.AddTextBoxColumn("USERID", 150)
                .SetValidationKeyColumn();
            requesterPopupColumn.GridColumns.AddTextBoxColumn("USERNAME", 200);
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
                .SetPosition(2.2);

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
        /// 작업장 설정 
        /// </summary>
        private void InitializeConditionPopup_Areaid()
        {

            var popupProduct = Conditions.AddSelectPopup("p_areaid",
                                                               new SqlQuery("GetAreaidListAuthorityByOsp", "10001"
                                                                               , $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"
                                                                               , $"USERID={UserInfo.Current.Id}"
                                                                                , $"P_OWNTYPE={"Y"}"
                                                                               , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                               ), "AREANAME", "AREAID")
            .SetPopupLayout("AREAID", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(450, 600)
            .SetLabel("AREANAME")
            .SetPopupResultCount(1)
            .SetRelationIds("p_plantid")
            .SetPosition(2.3);

            // 팝업 조회조건
            popupProduct.Conditions.AddTextBox("AREANAME")
                .SetLabel("AREANAME");

          
            popupProduct.GridColumns.AddTextBoxColumn("AREAID", 100)
                .SetValidationKeyColumn();
            popupProduct.GridColumns.AddTextBoxColumn("AREANAME", 200);

        }
        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            // 기간 포맷 용
            InitializeDatePeriod();
        }
        /// <summary>
        /// 기간 포맷 재정의 
        /// </summary>
        private void InitializeDatePeriod()
        {
            //InitializeDatePeriodSetting("P_SENDDATE");

            InitializeDatePeriodSetting("P_RECEIPTDATE");

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
        /// <summary>
        /// 저장및  삭제용 data table 생성 
        /// </summary>
        /// <returns></returns>
        private DataTable createSaveDatatable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "list";
            dt.Columns.Add("LOTID");
            dt.Columns.Add("PRODUCTDEFID");
            dt.Columns.Add("PRODUCTDEFVERSION");
            dt.Columns.Add("PRODUCTDEFNAME");
            dt.Columns.Add("PCSQTY", typeof(decimal));
            dt.Columns.Add("PNLQTY",typeof(decimal));
            dt.Columns.Add("SENDUSERNAME");
            dt.Columns.Add("PROCESSSEGMENTNAME_01");
            dt.Columns.Add("PROCESSSEGMENTNAME_02");
            dt.Columns.Add("PROCESSSEGMENTNAME_03");
            dt.Columns.Add("PROCESSSEGMENTNAME_04");
            dt.Columns.Add("PROCESSSEGMENTNAME_05");
    
            dt.Columns.Add("CONSUMABLEDEFNAME_01");
            dt.Columns.Add("CONSUMABLEDEFNAME_02");
            dt.Columns.Add("CONSUMABLEDEFNAME_03");
            dt.Columns.Add("CONSUMABLEDEFNAME_04");
            dt.Columns.Add("CONSUMABLEDEFNAME_05");
            dt.Columns.Add("CONSUMABLEDEFNAME_06");
            dt.Columns.Add("CONSUMABLEDEFNAME_07");
            dt.Columns.Add("CONSUMABLEDEFNAME_08");
            dt.Columns.Add("CONSUMABLEDEFNAME_09");
          
            dt.Columns.Add("CONSUMEDQTY_01", typeof(decimal));
            dt.Columns.Add("CONSUMEDQTY_02", typeof(decimal));
            dt.Columns.Add("CONSUMEDQTY_03", typeof(decimal));
            dt.Columns.Add("CONSUMEDQTY_04", typeof(decimal));
            dt.Columns.Add("CONSUMEDQTY_05", typeof(decimal));
            dt.Columns.Add("CONSUMEDQTY_06", typeof(decimal)); 
            dt.Columns.Add("CONSUMEDQTY_07", typeof(decimal));
            dt.Columns.Add("CONSUMEDQTY_08", typeof(decimal));
            dt.Columns.Add("CONSUMEDQTY_09", typeof(decimal));

            dt.Columns.Add("_STATE_");
            return dt;
        }
        /// <summary>
		/// 다국어 명 적용
		/// </summary>
		/// <param name="band"></param>
        private void setLabelLaungage(object band)
        {
            if (band is DetailBand)
            {
                DetailBand detailband = band as DetailBand;
                //Band groupHeader = detailReport.Bands[strGroupHeader];

                foreach (XRControl control in detailband.Controls)
                {
                    if (control is DevExpress.XtraReports.UI.XRLabel)
                    {
                        if (!string.IsNullOrEmpty(control.Tag.ToString()))
                        {
                            if (control.Name.Substring(0, 3).Equals("lbl"))
                            {
                                string bindText = Language.Get(control.Tag.ToString());
                                Font ft = BestSizeEstimator.GetFontToFitBounds(control as XRLabel, bindText);
                                if (ft.Size < control.Font.Size)
                                {
                                    control.Font = ft;
                                }

                                control.Text = bindText;
                            }
                        }
                    }
                    else if (control is DevExpress.XtraReports.UI.XRTable)
                    {
                        XRTable xt = control as XRTable;

                        foreach (XRTableRow tr in xt.Rows)
                        {
                            for (int i = 0; i < tr.Cells.Count; i++)
                            {
                                if (!string.IsNullOrEmpty(tr.Cells[i].Tag.ToString()) && (tr.Cells[i].Name.Substring(0, 3).Equals("lbl")))
                                {
                                    tr.Cells[i].Text = Language.Get(tr.Cells[i].Tag.ToString());

                                }

                            }
                        }

                    }
                }

            }

        }
        #endregion

        private void btnOutputslip_Click_1(object sender, EventArgs e)
        {

        }
    }
}
