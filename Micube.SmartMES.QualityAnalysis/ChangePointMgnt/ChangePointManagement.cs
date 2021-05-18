#region using

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 변경점 관리 > 변경점 신청서 등록 
    /// 업  무  설  명  : 변경점에 대한 신청서를 등록하고 조회할 수 있는 화면이다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-09-09
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ChangePointManagement : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public ChangePointManagement()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 외부에서 호출시 자동 조회
        /// </summary>
        /// <param name="parameters"></param>
        public override void LoadForm(Dictionary<string, object> parameters)
        {
            base.LoadForm(parameters);

            if (parameters != null)
            {
                _parameters = parameters;

                if ((parameters["CALLMENU"].Equals("ProductHistoryView") || parameters["CALLMENU"].Equals("GovernanceChange2")) && parameters.ContainsKey("CHANGEPOINTNO"))
                {
                    Dictionary<string, object> param = new Dictionary<string, object>()
                    {
                        { "P_CHANGEPOINTNO", parameters["CHANGEPOINTNO"] },
                        { "P_LANGUAGETYPE", UserInfo.Current.LanguageType }
                    };
                    DataTable dt = SqlExecuter.Query("GetChangePointHistory", "10001", param);
                    grdChangePointStatus.DataSource = dt;

                    //2019-12-13 장선미 : 품목이력조회 화면에서 화면 호출할 경우 parameter 확인 후 출력 팝업 화면 발생 
                    if (parameters["CALLMENU"].Equals("ProductHistoryView"))
                    {
                        grdChangePointStatus.View.CheckedAll();
                        BtnPrint_Click(null, null);
                    }
                }
            }
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

            InitializeGrid();
        }

        /// <summary>        
        /// 변경점 이력조회 그리드
        /// </summary>
        private void InitializeGrid()
        {
            grdChangePointStatus.GridButtonItem = GridButtonItem.Export;
            grdChangePointStatus.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            //grdChangePointStatus.View.CheckMarkSelection.MultiSelectCount = 1;
            grdChangePointStatus.View.SetIsReadOnly();

            grdChangePointStatus.View.AddTextBoxColumn("CHANGEPOINTNO", 150)
                .SetTextAlignment(TextAlignment.Center); // 변경점 관리번호
            grdChangePointStatus.View.AddTextBoxColumn("CHANGEPOINTTYPE", 120)
                .SetTextAlignment(TextAlignment.Center); // 변경점 구분
            grdChangePointStatus.View.AddTextBoxColumn("REQUESTDATE", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("CHANGEPOINTDATE"); // 변경점 신청일
            grdChangePointStatus.View.AddTextBoxColumn("REQUESTDEPARTMENT", 180)
                .SetLabel("CHANGEPOINTDEPARTMENT"); // 변경점 신청부서
            grdChangePointStatus.View.AddTextBoxColumn("CUSTOMERNAME", 180); // 고객사명
            grdChangePointStatus.View.AddTextBoxColumn("PRODUCTDEFID", 150); // 품목 ID
            grdChangePointStatus.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetTextAlignment(TextAlignment.Center); // 품목 Version
            grdChangePointStatus.View.AddTextBoxColumn("PRODUCTDEFNAME", 250); // 품목명
            grdChangePointStatus.View.AddTextBoxColumn("PROCESSSEGMENTID", 150); // 공정 ID
            grdChangePointStatus.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 80)
                .SetTextAlignment(TextAlignment.Center); // 공정 Version
            grdChangePointStatus.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 220); // 공정명
            grdChangePointStatus.View.AddTextBoxColumn("SEGMENTTYPENAME", 250)
                .SetLabel("SEGMENTTYPE"); // 공정분류명
            grdChangePointStatus.View.AddTextBoxColumn("CHANGETYPENAME", 250)
                .SetLabel("CHANGETYPE"); // 변경유형명
            grdChangePointStatus.View.AddTextBoxColumn("RATINGDECISION", 100)
                .SetTextAlignment(TextAlignment.Center); // 등급분류
            grdChangePointStatus.View.AddTextBoxColumn("PROCESSTYPENAME", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("PROCESSTYPE"); // 절차구분
            grdChangePointStatus.View.AddTextBoxColumn("CHARGETYPENAME", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("CHARGETYPE"); // 역할구분
            grdChangePointStatus.View.AddTextBoxColumn("USERNAME", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("OWNERNAME"); // 담당자명
            grdChangePointStatus.View.AddTextBoxColumn("APPROVALSTATENAME", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("APPROVALSTATE"); // 결재상태
            grdChangePointStatus.View.AddTextBoxColumn("APPROVALRESULTNAME", 100)
                .SetLabel("APPROVALRESULT")
                .SetTextAlignment(TextAlignment.Center); // 결재결과

            grdChangePointStatus.View.AddTextBoxColumn("SUBJECT", 100)
                .SetIsHidden(); // 변경제목
            grdChangePointStatus.View.AddTextBoxColumn("CUSTOMERID", 100)
                .SetIsHidden(); // 고객사ID
            grdChangePointStatus.View.AddTextBoxColumn("CONSUMABLENAME", 100)
                .SetIsHidden(); // 자재명
            grdChangePointStatus.View.AddTextBoxColumn("LOTID", 100)
                .SetIsHidden(); // Lot No
            grdChangePointStatus.View.AddTextBoxColumn("STOCKSTATUS", 100)
                .SetIsHidden(); // 재고현황
            grdChangePointStatus.View.AddTextBoxColumn("SEGMENTTYPE", 100)
                .SetIsHidden(); // 공정분류
            grdChangePointStatus.View.AddTextBoxColumn("CHANGETYPE", 100)
                .SetIsHidden(); // 변경유형
            grdChangePointStatus.View.AddTextBoxColumn("APPLICATIONTYPE", 100)
                .SetIsHidden(); // 적용구분
            grdChangePointStatus.View.AddTextBoxColumn("APPLICATIONTYPENAME", 100)
                .SetIsHidden(); // 적용구분명
            grdChangePointStatus.View.AddTextBoxColumn("CHANGEITEMMGNT", 100)
                .SetIsHidden(); // 변경품 관리
            grdChangePointStatus.View.AddTextBoxColumn("STOCKHANDLEMETHOD", 100)
                .SetIsHidden(); // 재고처리방안
            grdChangePointStatus.View.AddTextBoxColumn("REASONCOMMENTS", 100)
                .SetIsHidden(); // 변경사유
            grdChangePointStatus.View.AddTextBoxColumn("BEFORECOMMENTS", 100)
                .SetIsHidden(); // 변경전
            grdChangePointStatus.View.AddTextBoxColumn("AFTERCOMMENTS", 100)
                .SetIsHidden(); // 변경후
            grdChangePointStatus.View.AddTextBoxColumn("CHANGEDETAILS", 100)
                .SetIsHidden(); // 변경 세부내용
            grdChangePointStatus.View.AddTextBoxColumn("APPROVALNO", 100)
                .SetIsHidden(); // 결재번호
            grdChangePointStatus.View.AddTextBoxColumn("PROCESSTYPE", 100)
                .SetIsHidden(); // 절차구분
            grdChangePointStatus.View.AddTextBoxColumn("APPROVALSTATE", 100)
                .SetIsHidden(); // 결재상태

            grdChangePointStatus.View.PopulateColumns();

            grdChangePointStatus.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            //btnRegistration.Click += BtnRegistration_Click;
            //btnPrint.Click += BtnPrint_Click;

            grdChangePointStatus.View.RowClick += View_RowClick;
        }

        /// <summary>
        /// 변경점 신청서 출력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            DataSet dsReport = new DataSet();
            DataTable labelData = grdChangePointStatus.View.GetCheckedRows();

            if (labelData.Rows.Count == 0)
            {
                throw MessageException.Create("GridNoChecked");
            }

            labelData.Columns.Add(new DataColumn("LBLCHANGEAPPLICATION", typeof(string))); // 변경 신청서
            labelData.Columns.Add(new DataColumn("LBLCHANGEPOINTNO", typeof(string))); // 변경점 번호
            labelData.Columns.Add(new DataColumn("LBLAPPLICATIONDATE", typeof(string))); // 변경점 신청일
            labelData.Columns.Add(new DataColumn("LBLCHANGEPOINTTITLE", typeof(string))); // 변경점 제목
            labelData.Columns.Add(new DataColumn("LBLPROCESSSEGMENT", typeof(string))); // 공정
            labelData.Columns.Add(new DataColumn("LBLPRODUCT", typeof(string))); // 품목
            labelData.Columns.Add(new DataColumn("LBLHOPEDATE", typeof(string))); // 희망적용일
            labelData.Columns.Add(new DataColumn("LBLSTOCKSTATUS", typeof(string))); // 재고현황
            labelData.Columns.Add(new DataColumn("LBLPRODUCTMANAGE", typeof(string))); // 변경품관리
            labelData.Columns.Add(new DataColumn("LBLPROCESSSEGMENTTYPE", typeof(string))); // 공정분류
            labelData.Columns.Add(new DataColumn("LBLCHANGETYPE", typeof(string))); // 변경유형
            labelData.Columns.Add(new DataColumn("LBLAPPLICATIONTYPE", typeof(string))); // 적용구분
            labelData.Columns.Add(new DataColumn("LBLSTOCKHANDLEMETHOD", typeof(string))); // 재고처리방안
            labelData.Columns.Add(new DataColumn("LBLREASONCHANGE", typeof(string))); // 변경사유
            labelData.Columns.Add(new DataColumn("LBLBEFORECHANGE", typeof(string))); // 변경전
            labelData.Columns.Add(new DataColumn("LBLAFTERCHANGE", typeof(string))); // 변경후
            labelData.Columns.Add(new DataColumn("LBLDETAILCHANGE", typeof(string))); // 변경세부내용
            labelData.Columns.Add(new DataColumn("LBLRESULT", typeof(string))); // 관련 부서 담당자 및 검토/승인결과
            labelData.Columns.Add(new DataColumn("LBLNAME", typeof(string))); // 성명
            labelData.Columns.Add(new DataColumn("LBLDEPARTMENT", typeof(string))); // 부서명
            labelData.Columns.Add(new DataColumn("LBLCHECKDATE", typeof(string))); // 검토일자
            labelData.Columns.Add(new DataColumn("LBLCOMMENT", typeof(string))); // 처리의견
            labelData.Columns.Add(new DataColumn("RESULTCOMMENTS", typeof(string))); // 검토및 승인결과 (파라미터 데이터)

            foreach (DataRow row in labelData.Rows)
            {
                row["LBLCHANGEAPPLICATION"] = Language.Get("CHANGEAPPLICATION"); // 변경 신청서
                row["LBLCHANGEPOINTNO"] = Language.Get("CHANGEPOINTNO"); // 변경점 번호
                row["LBLAPPLICATIONDATE"] = Language.Get("CHANGEPOINTDATE"); // 변경점 신청일
                row["LBLCHANGEPOINTTITLE"] = Language.Get("CHANGEPOINTTITLE"); // 변경점 제목
                row["LBLPROCESSSEGMENT"] = Language.Get("PROCESSSEGMENT"); // 공정
                row["LBLPRODUCT"] = Language.Get("BYPRODUCT"); // 품목
                row["LBLHOPEDATE"] = Language.Get("HOPEDATE"); // 희망적용일
                row["LBLSTOCKSTATUS"] = Language.Get("STOCKSTATE"); // 재고현황
                row["LBLPRODUCTMANAGE"] = Language.Get("CHANGEPRODUCTMANAGEMENT"); // 변경품관리
                row["LBLPROCESSSEGMENTTYPE"] = Language.Get("PROCESSSEGMENTTYPE"); // 공정구분
                row["LBLCHANGETYPE"] = Language.Get("CHANGETYPE"); // 변경유형
                row["LBLAPPLICATIONTYPE"] = Language.Get("APPLICATIONTYPE"); // 적용구분
                row["LBLSTOCKHANDLEMETHOD"] = Language.Get("STOCKTREATMENTPLAN"); // 재고처리방안
                row["LBLREASONCHANGE"] = Language.Get("CHANGEREASON"); // 변경사유
                row["LBLBEFORECHANGE"] = Language.Get("BEFORECHANGE"); // 변경전
                row["LBLAFTERCHANGE"] = Language.Get("AFTERCHANGE"); // 변경후
                row["LBLDETAILCHANGE"] = Language.Get("CHANGECONTENTS"); // 변경내용
                row["LBLRESULT"] = Language.Get("REVIEWANDAPPROVALRESULTS"); // 관련 부서 담당자 및 검토/승인결과
                row["LBLNAME"] = Language.Get("USERNAME"); // 성명
                row["LBLDEPARTMENT"] = Language.Get("DEPARTMENT"); // 부서
                row["LBLCHECKDATE"] = Language.Get("REVIEWTIME"); // 검토일자
                row["LBLCOMMENT"] = Language.Get("COMMENTS"); // 처리의견

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
                param.Add("P_CHANGEPOINTNO", row["CHANGEPOINTNO"]);

                DataTable approvalUserData = SqlExecuter.Query("GetChangePointReview", "10001", param);

                foreach (DataRow row2 in approvalUserData.Rows)
                {
                    row["RESULTCOMMENTS"] += "\r\n" + row2["USERNAME"] + ", " + row2["DEPARTMENT"] + ", " + row2["REVIEWDATE"]
                                             + "\r\n" + row2["REVIEWCOMMENTS"] + "\r\n";
                }
            }

            dsReport.Tables.Add(labelData);

            Assembly assembly = Assembly.GetAssembly(this.GetType());
            Stream stream = assembly.GetManifestResourceStream("Micube.SmartMES.QualityAnalysis.Report.ChangePointApplicationReport.repx");

            XtraReport report = XtraReport.FromStream(stream);
            report.DataSource = dsReport;
            //report.DataMember = dsReport.Tables[1].TableName;

            Band headerPage = report.Bands["PageHeader"];
            SetReportControlDataBinding(headerPage.Controls, dsReport.Tables[0]);

            Band detailPage = report.Bands["Detail"];
            SetReportControlDataBinding(detailPage.Controls, dsReport.Tables[0]);

            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.ShowRibbonPreview();
        }

        /// <summary>
        /// 등록된 변경점 신청서 확인
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                pnlContent.ShowWaitArea();

                ChangePointRequestPopup popup = new ChangePointRequestPopup();
                popup.Owner = this;
                popup.CurrentDataRow = this.grdChangePointStatus.View.GetFocusedDataRow();
                popup.Type = "Old";
                popup.StartPosition = FormStartPosition.CenterParent;
                popup.btnSave.Enabled = btnFlag.Enabled;

                if (popup.ShowDialog() == DialogResult.OK)
                {
                    grdChangePointStatus.BeginInvoke(new MethodInvoker(() =>
                    {
                        OnSearchAsync();
                    }));
                }

                pnlContent.CloseWaitArea();
            }
        }

        /// <summary>
        /// 변경점 신청 등록버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRegistration_Click(object sender, EventArgs e)
        {
            pnlContent.ShowWaitArea();
      
            ChangePointRequestPopup popup = new ChangePointRequestPopup();
            popup.Owner = this;
            popup.CurrentDataRow = this.grdChangePointStatus.View.GetFocusedDataRow();
            popup.Type = "New";
            popup.StartPosition = FormStartPosition.CenterParent;

            if (popup.ShowDialog() == DialogResult.OK)
            {
                grdChangePointStatus.BeginInvoke(new MethodInvoker(() =>
                {
                    OnSearchAsync();
                }));
            }

            pnlContent.CloseWaitArea();
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            //base.OnToolbarSaveClick();

            //DataTable changed = grdWorkDefectPriceStatus.GetChangedRows();

            //ExecuteRule("SaveInspectionGrade", changed);
        }

        /// <summary>
        /// 툴바버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Print"))
            {
                BtnPrint_Click(null, null);
            }
            else if (btn.Name.ToString().Equals("Regist"))
            {
                BtnRegistration_Click(null, null);
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

            var values = Conditions.GetValues();
            values.Add("p_languageType", UserInfo.Current.LanguageType);

            DataTable dt = await SqlExecuter.QueryAsync("GetChangePointHistory", "10001", values);

            if (dt.Rows.Count < 1)
            {
                // 조회할 데이터가 없습니다.
                this.ShowMessage("NoSelectData");
                grdChangePointStatus.DataSource = null;
                return;
            }

            grdChangePointStatus.DataSource = dt;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            InitializeConditionPopup_Customer();
            InitializeConditionPopup_Product();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
        }

        /// <summary>
        /// 고객사 조회조건
        /// </summary>
        private void InitializeConditionPopup_Customer()
        {
            // 팝업 컬럼설정
            var areaPopup = Conditions.AddSelectPopup("p_customerId", new SqlQuery("GetCustomerListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID")
               .SetPopupLayout("CUSTOMER", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("CUSTOMER")
               .SetPopupResultCount(1)
               .SetPosition(2.2);

            // 팝업 조회조건
            areaPopup.Conditions.AddTextBox("CUSTOMERIDNAME");

            // 팝업 그리드
            areaPopup.GridColumns.AddTextBoxColumn("CUSTOMERID", 150)
                .SetValidationKeyColumn();
            areaPopup.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 200);
        }

        /// <summary>
        /// 품목 조회조건
        /// </summary>
        private void InitializeConditionPopup_Product()
        {
            // 팝업 컬럼설정
            var productPopup = Conditions.AddSelectPopup("p_productdefId", new SqlQuery("GetProductListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFNAME", "PRODUCTDEFIDVERSION")
               .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(600, 800)
               .SetLabel("PRODUCT")
               .SetPopupAutoFillColumns("PRODUCTDEFNAME")
               .SetPopupResultCount(1)
               .SetPosition(1.3);

            // 팝업 조회조건
            productPopup.Conditions.AddTextBox("PRODUCTDEFIDNAME");

            // 팝업 그리드
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 120)
                .SetValidationKeyColumn();
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 220);
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFIDVERSION", 100)
                .SetIsHidden();
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            //grdWorkDefectPriceStatus.View.CheckValidation();

            //DataTable changed = grdWorkDefectPriceStatus.GetChangedRows();

            //if (changed.Rows.Count == 0)
            //{
            //    // 저장할 데이터가 존재하지 않습니다.
            //    throw MessageException.Create("NoSaveData");
            //}
        }

        #endregion

        #region Private Function

        /// <summary>
        /// Report 파일의 컨트롤 중 Tag(FieldName) 값이 있는 컨트롤에 DataBinding(Text)를 추가한다.
        /// </summary>
        private void SetReportControlDataBinding(XRControlCollection controls, DataTable dataSource)
        {
            if (controls.Count > 0)
            {
                foreach (XRControl control in controls)
                {
                    if (!string.IsNullOrWhiteSpace(control.Tag.ToString()))
                        control.DataBindings.Add("Text", dataSource, control.Tag.ToString());

                    SetReportControlDataBinding(control.Controls, dataSource);
                }
            }
        }

        #endregion
    }
}
