#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
//using Micube.SmartMES.ProcessManagement;
using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using System.IO;
using DevExpress.LookAndFeel;
using System.Reflection;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 신뢰성검증 > 신뢰성 검증 의뢰(정기)
    /// 업  무  설  명  : 신뢰성 정기 의뢰를 하는 화면이다. 동도금 정기적으로 신뢰성 검증 하는 의뢰를 한다. 계측 값 등록 시 자동으로 신뢰성 의뢰가 등록 됨.  
    /// 생    성    자  : 유석진
    /// 생    성    일  : 2019-07-10
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ReliabilityVerificationRequestRegular : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public ReliabilityVerificationRequestRegular()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGridReliabilityVerificationRequestPrintRegular(); // 신뢰성 의뢰서 출력 그리드 초기화
            InitializeGridReliabilityVerificationRequestRgisterRegular(); // 신뢰성 의뢰접수(정기) 그리드 초기화
            InitializeGridReReliabilityVerificationRequestRegisterRegular(); // 신뢰성 의뢰 재접수(정기) 그리드 초기화
            InitializeEvent();
        }

        /// <summary>        
        /// 그리드 초기화(신뢰서 의뢰 접수(정기))
        /// </summary>
        private void InitializeGridReliabilityVerificationRequestPrintRegular()
        {
            grdReliabiVerifiReqPrintRegular.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdReliabiVerifiReqPrintRegular.GridButtonItem -= GridButtonItem.Delete; // 삭제 버튼 비활성화
            grdReliabiVerifiReqPrintRegular.GridButtonItem -= GridButtonItem.Add; // 추가 버튼 비활성화
            grdReliabiVerifiReqPrintRegular.GridButtonItem -= GridButtonItem.Copy; // 복사 버튼 비활성화
            grdReliabiVerifiReqPrintRegular.GridButtonItem -= GridButtonItem.Import; // Import 버튼 비활성화

            grdReliabiVerifiReqPrintRegular.View.SetSortOrder("OUTPUTDATE");
            grdReliabiVerifiReqPrintRegular.View.SetSortOrder("LOTID");

            grdReliabiVerifiReqPrintRegular.View
               .AddComboBoxColumn("REQUESTSTATUS", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ReliabilityRequestStatus", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetLabel("REQUESTTYPE")
               .SetIsReadOnly()
               .SetTextAlignment(TextAlignment.Left); // 의뢰구분

            grdReliabiVerifiReqPrintRegular.View
               .AddTextBoxColumn("OUTPUTDATE", 180)
               .SetIsReadOnly()
               .SetTextAlignment(TextAlignment.Center); // 출력일시

            grdReliabiVerifiReqPrintRegular.View
                .AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 품목코드

            grdReliabiVerifiReqPrintRegular.View
                .AddTextBoxColumn("PRODUCTDEFNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 품목명

            grdReliabiVerifiReqPrintRegular.View
                .AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 품목Version

            grdReliabiVerifiReqPrintRegular.View
                .AddTextBoxColumn("LOTID", 170)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // LOT NO

            grdReliabiVerifiReqPrintRegular.View
                .AddTextBoxColumn("PROCESSSEGMENTNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 표준공정

            grdReliabiVerifiReqPrintRegular.View.PopulateColumns();
        }

        /// <summary>        
        /// 그리드 초기화(신뢰서 의뢰 접수(정기))
        /// </summary>
        private void InitializeGridReliabilityVerificationRequestRgisterRegular()
        {
            grdReliabiVerifiReqRgistRegular.GridButtonItem -= GridButtonItem.Delete; // 삭제 버튼 비활성화
            grdReliabiVerifiReqRgistRegular.GridButtonItem -= GridButtonItem.Add; // 추가 버튼 비활성화
            grdReliabiVerifiReqRgistRegular.GridButtonItem -= GridButtonItem.Copy; // 복사 버튼 비활성화
            grdReliabiVerifiReqRgistRegular.GridButtonItem -= GridButtonItem.Import; // Import 버튼 비활성화

            grdReliabiVerifiReqRgistRegular.View.SetSortOrder("MEASURECOMPLETIONDATE");
            grdReliabiVerifiReqRgistRegular.View.SetSortOrder("LOTID");

            grdReliabiVerifiReqRgistRegular.View
                .AddCheckBoxColumn("RECEIVE", 150)
                .SetDefault(false)
                .SetTextAlignment(TextAlignment.Center); // 접수

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("MEASURECOMPLETIONDATE", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 계측접수일시

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("SAMPLERECEIVEDATE", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 시료접수일시

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("TRANSITIONDATE", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right); // 경과일

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("REQUESTDEPT", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 의뢰부서(공정)

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("WORKORDERUSER", 100)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left)
                .SetLabel("ACCEPTMAINTWORKORDERUSER"); // 접수자


            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 품목코드

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("PRODUCTDEFNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 품목명

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // Rev

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("LOTID", 170)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // LOT NO

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("PROCESSSEGMENTNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 표준공정

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("AREANAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 작업장

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("EQUIPMENTNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 설비(호기)

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("WORKENDTIME", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 작업종료시간

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("ISREREQUEST", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 재의뢰여부

            grdReliabiVerifiReqRgistRegular.View.PopulateColumns();
        }

        /// <summary>        
        /// 그리드 초기화(신뢰서 의뢰 재접수(정기))
        /// </summary>
        private void InitializeGridReReliabilityVerificationRequestRegisterRegular()
        {
            grdReReliabiVerifiReqRgistRegular.GridButtonItem -= GridButtonItem.Delete; // 삭제 버튼 비활성화
            grdReReliabiVerifiReqRgistRegular.GridButtonItem -= GridButtonItem.Add; // 추가 버튼 비활성화
            grdReReliabiVerifiReqRgistRegular.GridButtonItem -= GridButtonItem.Copy; // 복사 버튼 비활성화
            grdReReliabiVerifiReqRgistRegular.GridButtonItem -= GridButtonItem.Import; // Import 버튼 비활성화

            grdReReliabiVerifiReqRgistRegular.View.SetSortOrder("MEASURECOMPLETIONDATE");
            grdReReliabiVerifiReqRgistRegular.View.SetSortOrder("LOTID");

            grdReReliabiVerifiReqRgistRegular.View
                .AddCheckBoxColumn("RECEIVE", 150)
                .SetTextAlignment(TextAlignment.Center); // 접수

            grdReReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("MEASURECOMPLETIONDATE", 180).SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 계측접수일시

            grdReReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("SAMPLERECEIVEDATE", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 시료접수일시

            grdReReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("TRANSITIONDATE", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right); // 경과일

            grdReReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("REQUESTDEPT", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 의뢰부서(공정)

            grdReReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("WORKORDERUSER", 100)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left)
                .SetLabel("ACCEPTMAINTWORKORDERUSER"); // 접수자

            grdReReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 품목코드

            grdReReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("PRODUCTDEFNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 품목명

            grdReReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // Rev

            grdReReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("LOTID", 170)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // LOT NO

            grdReReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("PROCESSSEGMENTNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 표준공정

            grdReReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("AREANAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 작업장

            grdReReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("EQUIPMENTNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 설비(호기)

            grdReReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("WORKENDTIME", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 작업종료시간

            grdReReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("ISREREQUEST", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 재의뢰여부

            grdReReliabiVerifiReqRgistRegular.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            txtLotId1.EnterKeyDown += TxtLotId1_EnterKeyDown;
            txtLotId2.EnterKeyDown += TxtLotId2_EnterKeyDown;
            txtLotId3.EnterKeyDown += TxtLotId3_EnterKeyDown;

            btnRequestPrinting.Click += BtnRequestPrinting_Click;
            //btnRecieve1.Click += BtnRecieve1_Click;
            //btnRecieve2.Click += BtnRecieve1_Click;
            

            tabReliabiVerifiReqRgistRegular.SelectedPageChanged += TabReliabilityVerificationRequestRegular_SelectedPageChanged;
            grdReliabiVerifiReqRgistRegular.View.DoubleClick += GrdReliabiVerifiReqRgistRegularView_DoubleClick;
            grdReReliabiVerifiReqRgistRegular.View.DoubleClick += grdReReliabiVerifiReqRgistRegularView_DoubleClick;

            // 의뢰서 출력 체크박스 이벤트
            grdReliabiVerifiReqPrintRegular.View.CheckStateChanged += View_CheckStateChanged;
            // 의뢰서 접수 체크박스 이벤트
            grdReliabiVerifiReqRgistRegular.View.CellValueChanged += View_CheckStateChanged1;
            // 재의뢰서 접수 체크박스 이벤트
            grdReReliabiVerifiReqRgistRegular.View.CellValueChanged += View_CheckStateChanged2;
        }

        /// <summary>        
        /// 재의뢰서 접수 체크박스 이벤트
        /// </summary>
        private void View_CheckStateChanged2(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow row = grdReReliabiVerifiReqRgistRegular.View.GetFocusedDataRow();

            grdReReliabiVerifiReqRgistRegular.View.CellValueChanged -= View_CheckStateChanged2;

            //2019-12-27 작업장 제한
            if (!row["ISMODIFY"].ToString().Equals("Y"))
            {
                grdReReliabiVerifiReqRgistRegular.View.SetRowCellValue(e.RowHandle, "RECEIVE", "False");

                grdReReliabiVerifiReqRgistRegular.View.CellValueChanged += View_CheckStateChanged2;

                string area = Format.GetString(row["AREANAME"]);
                throw MessageException.Create("NoMatchingAreaUser", area);// 작업장에 대한 권한이 없습니다.
            }
        }

        /// <summary>        
        /// 의뢰서 접수 체크박스 이벤트
        /// </summary>
        private void View_CheckStateChanged1(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow row = grdReliabiVerifiReqRgistRegular.View.GetFocusedDataRow();

            grdReliabiVerifiReqRgistRegular.View.CellValueChanged -= View_CheckStateChanged1;

            //2019-12-27 작업장 제한
            if (!row["ISMODIFY"].ToString().Equals("Y"))
            {
                grdReliabiVerifiReqRgistRegular.View.SetRowCellValue(e.RowHandle, "RECEIVE", "False");

                grdReliabiVerifiReqRgistRegular.View.CellValueChanged += View_CheckStateChanged1;

                string area = Format.GetString(row["AREANAME"]);
                throw MessageException.Create("NoMatchingAreaUser", area);// 작업장에 대한 권한이 없습니다.
            }
        }

        /// <summary>        
        /// 의뢰서 출력 체크박스 이벤트
        /// </summary>
        private void View_CheckStateChanged(object sender, EventArgs e)
        {
            DataRow row = grdReliabiVerifiReqPrintRegular.View.GetFocusedDataRow();

            //2019-12-27 작업장 제한
            if (!row["ISMODIFY"].ToString().Equals("Y"))
            {
                grdReliabiVerifiReqPrintRegular.View.CheckRow(grdReliabiVerifiReqPrintRegular.View.FocusedRowHandle, false);

                string area = Format.GetString(row["AREANAME"]);
                throw MessageException.Create("NoMatchingAreaUser", area);// 작업장에 대한 권한이 없습니다.
            }
        }

        /// <summary>
        /// 선택한 Lot에 대한 의리서 를 출력한다.
        /// </summary>
        private void BtnRequestPrinting_Click(object sender, EventArgs e)
        {

            DataTable checkedRows = grdReliabiVerifiReqPrintRegular.View.GetCheckedRows();
            DataSet dsReport = new DataSet();

            DataTable headerRows = new DataTable();
            headerRows.Columns.Add(new DataColumn("LBLPRODUCTDEFID", typeof(string)));
            headerRows.Columns.Add(new DataColumn("LBLPRODUCTDEFNAME", typeof(string)));
            headerRows.Columns.Add(new DataColumn("LBLLOTID", typeof(string)));
            headerRows.Columns.Add(new DataColumn("LBLPROCESSSEGMENTNAME", typeof(string)));
            headerRows.Columns.Add(new DataColumn("LBLSAMPLEATTACHMENT", typeof(string)));
            headerRows.Columns.Add(new DataColumn("LBLTOP", typeof(string)));
            headerRows.Columns.Add(new DataColumn("LBLMIDDLE", typeof(string)));
            headerRows.Columns.Add(new DataColumn("LBLBELOW", typeof(string)));
            headerRows.Columns.Add(new DataColumn("LBLEQUIPMENTTNAME", typeof(string)));
            headerRows.Columns.Add(new DataColumn("LBLREQUESTDATENAME", typeof(string)));

            DataRow hearRow = headerRows.NewRow();
            hearRow["LBLPRODUCTDEFID"] = Language.Get("PRODUCTDEFID"); ;
            hearRow["LBLPRODUCTDEFNAME"] = Language.Get("PRODUCTDEFNAME");
            hearRow["LBLLOTID"] = Language.Get("LOTID");
            hearRow["LBLPROCESSSEGMENTNAME"] = Language.Get("PROCESSSEGMENTNAME");
            hearRow["LBLSAMPLEATTACHMENT"] = Language.Get("SAMPLEATTACHMENT");
            hearRow["LBLTOP"] = Language.Get("TOP");
            hearRow["LBLMIDDLE"] = Language.Get("MIDDLE");
            hearRow["LBLBELOW"] = Language.Get("BELOW");
            hearRow["LBLEQUIPMENTTNAME"] = Language.Get("EQUIPMENTUNIT");
            hearRow["LBLREQUESTDATENAME"] = Language.Get("REQUESTDATE");
            headerRows.Rows.Add(hearRow);

            dsReport.Tables.Add(headerRows);
            dsReport.Tables.Add(checkedRows);

            Assembly assembly = Assembly.GetAssembly(this.GetType());
            Stream stream = assembly.GetManifestResourceStream("Micube.SmartMES.QualityAnalysis.Report.RequestPrinting.repx");

            XtraReport report = XtraReport.FromStream(stream);
            report.DataSource = dsReport.Tables[1];
            //report.DataMember = dsReport.Tables[0].TableName;

            Band header = report.Bands["ReportHeader"];
            SetReportControlDataBinding(header.Controls, dsReport.Tables[0]);

            Band band = report.Bands["Detail"];
            SetReportControlDataBinding(band.Controls, dsReport.Tables[1]);

            DataTable dt = grdReliabiVerifiReqPrintRegular.View.GetCheckedRows();

            if (dt.Rows.Count == 0)
            {
                throw MessageException.Create("NotSelectedPintInfo");
            }
            else
            {
                //report.Print();
                report.PrintingSystem.EndPrint += PrintingSystem_EndPrint1; ;
                ReportPrintTool printTool = new ReportPrintTool(report);
                printTool.ShowRibbonPreview();
            }
        }

        /// <summary>        
        /// 의리서 출력 완료후 출력일시 반영 및 조회
        /// </summary>
        private void PrintingSystem_EndPrint1(object sender, EventArgs e)
        {
            SaveRequestPrinting(); // 의뢰서출력 일시 저장
            Search();
        }

        /// <summary>        
        /// 의뢰접수/재접수 접수 처리
        /// </summary>
        private void BtnRecieve1_Click(object sender, EventArgs e)
        {
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "RegisterNote");//접수 하시겠습니까?

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnRecieve1.Enabled = false;
                    btnRecieve2.Enabled = false;

                    if (tabReliabiVerifiReqRgistRegular.SelectedTabPageIndex == 1) // 의뢰 접수
                    {
                        SaveRecept(grdReliabiVerifiReqRgistRegular);
                    }
                    else if (tabReliabiVerifiReqRgistRegular.SelectedTabPageIndex == 2) // 의뢰 재접수
                    {
                        SaveRecept(grdReReliabiVerifiReqRgistRegular);
                    }

                    ShowMessage("SuccessSave");

                    Search(); // 조회
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();
                    btnRecieve1.Enabled = true;
                    btnRecieve2.Enabled = true;
                }
            }
        }

        /// <summary>        
        /// 의뢰서 재접수 lot 번호 입력 후 Enter 입력 시 해당 그리드 행 접수 선택(체크)
        /// </summary>
        private void TxtLotId3_EnterKeyDown(object sender, KeyEventArgs e)
        {
            DataView dv = grdReReliabiVerifiReqRgistRegular.View.DataSource as DataView;
            DataTable dt = dv.Table;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                if (dr["LOTID"].ToString().Equals(txtLotId3.EditValue))
                {
                    dr["RECEIVE"] = true;
                }
            }
        }

        /// <summary>        
        /// 의뢰서 접수 lot 번호 입력 후 Enter 입력 시 해당 그리드 행 접수 선택(체크)
        /// </summary>
        private void TxtLotId2_EnterKeyDown(object sender, KeyEventArgs e)
        {
            DataView dv = grdReliabiVerifiReqRgistRegular.View.DataSource as DataView;
            DataTable dt = dv.Table;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                if (dr["LOTID"].ToString().Equals(txtLotId2.EditValue))
                {
                    dr["RECEIVE"] = true;
                }
            }
        }

        /// <summary>        
        /// 의뢰서 출력 lot 번호 입력 후 Enter 입력 시 해당 그리드 행 선택(체크)
        /// </summary>
        private void TxtLotId1_EnterKeyDown(object sender, KeyEventArgs e)
        {
            /*
            DataView dv = grdReliabiVerifiReqPrintRegular.View.DataSource as DataView;
            DataTable dt = dv.Table;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                if (dr["LOTID"].ToString().Equals(txtLotId1.EditValue))
                {
                    grdReliabiVerifiReqPrintRegular.View.CheckRow(i, true);
                }
            }*/

            //2020-02-12 강유라  
            //Enter 눌렀을 때 입력된 lot 체크되지않는 문제 수정
            var handles = grdReliabiVerifiReqPrintRegular.View.GetRowHandlesByValue("LOTID", Format.GetString(txtLotId1.EditValue));
              
            foreach (var item in handles)
            {
                grdReliabiVerifiReqPrintRegular.View.CheckRow(item, true);
            }
        }

        /// <summary>        
        /// 의뢰재접수 목록 더블클릭 시
        /// </summary>
        private void grdReReliabiVerifiReqRgistRegularView_DoubleClick(object sender, EventArgs e)
        {
            DialogManager.ShowWaitArea(pnlContent);
            DataRow row = grdReReliabiVerifiReqRgistRegular.View.GetFocusedDataRow();

            ReReliabilityVerificationRequestRegularPopup popup = new ReReliabilityVerificationRequestRegularPopup(row);

            //버튼의 enable
            bool isEnable = btnRecieve1.Enabled;

            if (!Format.GetString(row["ISMODIFY"]).Equals("Y"))
            {
                isEnable = false;
            }
            

            popup.isEnable = isEnable;

            popup.ShowDialog();

            DialogManager.CloseWaitArea(pnlContent);
        }

        /// <summary>        
        /// 의뢰접수 목록 더블클릭 시
        /// </summary>
        private void GrdReliabiVerifiReqRgistRegularView_DoubleClick(object sender, EventArgs e)
        {
            DialogManager.ShowWaitArea(pnlContent);
            DataRow row = grdReliabiVerifiReqRgistRegular.View.GetFocusedDataRow();

            ReliabilityVerificationRequestRegularPopup popup = new ReliabilityVerificationRequestRegularPopup(row);

            //버튼의 enable
            bool isEnable = btnRecieve1.Enabled;

            if (!Format.GetString(row["ISMODIFY"]).Equals("Y"))
            {
                isEnable = false;
            }
          

            popup.isEnable = isEnable;

            popup.ShowDialog();

            DialogManager.CloseWaitArea(pnlContent);
        }

        /// <summary>        
        /// 탭이 변경 되었을때 호출
        /// </summary>
        private void TabReliabilityVerificationRequestRegular_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            SmartTabControl tab = sender as SmartTabControl;

            if (tab.SelectedTabPageIndex == 0) // 의뢰서 출력 조회
            {
                SearchReliabiVerifiReqPrintRegular();
            } else if (tab.SelectedTabPageIndex == 1) // 의로서 접수 조회
            {
                SearchReliabiVerifiReqRgistRegular();
            }
            else if (tab.SelectedTabPageIndex == 2) // 의뢰서 재접수 조회
            {
                SearchReReliabiVerifiReqRgistRegular();
            }
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            //DataTable changed = grdCodeClass.GetChangedRows();

            //ExecuteRule("SaveCodeClass", changed);
        }

        /// <summary>
        /// 툴바버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Save"))
            {
                BtnRecieve1_Click(null, null);
            }
        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();
            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dt = null;

            if (tabReliabiVerifiReqRgistRegular.SelectedTabPageIndex == 0) // 신뢰성 의뢰서 출력
            {
                dt = await SqlExecuter.QueryAsync("GetReliabilityVerificationRequestRegularRgisterList", "10001", values);

                if (dt.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData");
                }

                grdReliabiVerifiReqPrintRegular.DataSource = dt;
            }
            else if (tabReliabiVerifiReqRgistRegular.SelectedTabPageIndex == 1) // 신뢰성 의뢰서 접수
            {
                values.Add("P_ISSECOND", "R"); // 의뢰서 접수

                dt = await SqlExecuter.QueryAsync("GetReliabilityVerificationRequestRegularRgisterList", "10002", values);

                if (dt.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData");
                }

                grdReliabiVerifiReqRgistRegular.DataSource = dt;
            }
            else if (tabReliabiVerifiReqRgistRegular.SelectedTabPageIndex == 2) // 신뢰성 의뢰서 재접수
            {
                values.Add("P_ISSECOND", "S"); // 의뢰서 재접수

                dt = await SqlExecuter.QueryAsync("GetReliabilityVerificationRequestRegularRgisterList", "10003", values);

                if (dt.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData");
                }

                grdReReliabiVerifiReqRgistRegular.DataSource = dt;
            }
        }

        #endregion

        #region 조회조건 설정

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            InitializeConditionAreaId_Popup();
            InitializeConditionProcessSegmentId_Popup();
            InitializeConditionPopup_Product();

            // 품목
            //CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 4.3, true, Conditions);

            this.Conditions.AddComboBox("p_processsegmentclassId", new SqlQuery("GetLargeProcesssegmentListByQcm", "10001", "CODECLASSID=ChemicalAnalyRound", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                //.SetRelationIds("p_plantId")
                //.SetRelationIds("p_chemicalWaterType")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetEmptyItem()
                .SetDefault("25")
                .SetLabel("LARGEPROCESSSEGMENT")
                .SetPosition(3.4); // 대공정]

            InitializeConditionEquipment_Popup();
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
               .SetPosition(3.3)
               .SetPopupResultCount(0);

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

        /// <summary>
        /// 팝업형 조회조건 생성 - 작업장
        /// </summary>
        private void InitializeConditionAreaId_Popup()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            //param.Add("AreaType", "Area");
            //param.Add("PlantId", UserInfo.Current.Plant);
            param.Add("LanguageType", UserInfo.Current.LanguageType);
            param.Add("P_USERID", UserInfo.Current.Id);
            //param.Add("AREA", UserInfo.Current.Area);

            //SqlQuery areaList = new SqlQuery("GetAreaidListByCsm", "10001", $"P_PLANTID={UserInfo.Current.Plant}", $"P_AREANAME={UserInfo.Current.Area}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            //DataTable areaTable = areaList.Execute();

            var areaIdPopup = this.Conditions.AddSelectPopup("p_areaId", new SqlQuery("GetAuthorityUserUseArea", "10001", param), "AREANAME", "AREAID")
                .SetPopupLayout(Language.Get("SELECTAREAID"), PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("AREANAME")
                .SetLabel("AREA")
                //.SetDefault(areaTable.Rows[0]["AREANAME"], UserInfo.Current.Area)
                .SetPosition(3.1);

            areaIdPopup.Conditions.AddTextBox("AREAIDNAME")
                .SetLabel("AREAIDNAME");

            areaIdPopup.GridColumns.AddTextBoxColumn("AREAID", 150);
            areaIdPopup.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }

        /// <summary>
        /// 팝업형 조회조건 생성 - 공정
        /// </summary>
        private void InitializeConditionProcessSegmentId_Popup()
        {
            var processSegmentIdPopup = this.Conditions.AddSelectPopup("P_ProcessSegmentId", new SqlQuery("GetProcessSegmentList", "10002", $"PLANTID={UserInfo.Current.Plant}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                .SetPopupLayout(Language.Get("SELECTPROCESSSEGMENTID"), PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("PROCESSSEGMENTNAME")
                .SetLabel("PROCESSSEGMENT")
                .SetPosition(3.2);

            processSegmentIdPopup.Conditions.AddComboBox("PROCESSSEGMENTCLASSID_MIDDLE", new SqlQuery("GetProcessSegmentClassByType", "10001", $"PLANTID={UserInfo.Current.Plant}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                .SetLabel("MIDDLEPROCESSSEGMENT");
            processSegmentIdPopup.Conditions.AddTextBox("PROCESSSEGMENT");


            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_TOP", 150)
                .SetLabel("LARGEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_MIDDLE", 150)
                .SetLabel("MIDDLEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
        }

        /// <summary>
        /// 팝업형 조회조건 생성 - 설비
        /// </summary>
        private void InitializeConditionEquipment_Popup()
        {
            // 팝업 컬럼설정
            var equipmentPopupColumn = Conditions.AddSelectPopup("p_equipmentId", new SqlQuery("GetEquipmentListByChemicalAnalysis", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTNAME", "EQUIPMENTID")
               .SetPopupLayout("EQUIPMENT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(400, 600)
               .SetPopupAutoFillColumns("EQUIPMENTNAME")
               .SetLabel("EQUIPMENT")
               .SetPopupResultCount(1)
               .SetPosition(3.5)
               .SetRelationIds("p_plantId", "p_processsegmentclassId");

            // 팝업 조회조건
            equipmentPopupColumn.Conditions.AddTextBox("EQUIPMENTIDNAME")
                .SetLabel("EQUIPMENTIDNAME");

            // 팝업 그리드
            equipmentPopupColumn.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150)
                .SetValidationKeyColumn();
            equipmentPopupColumn.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200);
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            //grdCodeClass.View.CheckValidation();

            //DataTable changed = grdCodeClass.GetChangedRows();

            //if (changed.Rows.Count == 0)
            //{
            //    // 저장할 데이터가 존재하지 않습니다.
            //    throw MessageException.Create("NoSaveData");
            //}
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가

        /// <summary>
        /// 의뢰서 출력 조회
        /// </summary>
        private void SearchReliabiVerifiReqPrintRegular()
        {
            try
            {
                this.ShowWaitArea();

                var values = Conditions.GetValues();
                values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

                DataTable dt = SqlExecuter.Query("GetReliabilityVerificationRequestRegularRgisterList", "10001", values);

                if (dt.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData");
                }

                grdReliabiVerifiReqPrintRegular.DataSource = dt;
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

        /// <summary>
        /// 의뢰서 접수 조회
        /// </summary>
        private void SearchReliabiVerifiReqRgistRegular()
        {
            try
            {
                this.ShowWaitArea();

                var values = Conditions.GetValues();
                values.Add("P_ISSECOND", "R"); // 의뢰서 접수
                values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

                DataTable dt = SqlExecuter.Query("GetReliabilityVerificationRequestRegularRgisterList", "10002", values);

                if (dt.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData");
                }

                grdReliabiVerifiReqRgistRegular.DataSource = dt;
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

        /// <summary>
        /// 의뢰서 재접수 조회
        /// </summary>
        private void SearchReReliabiVerifiReqRgistRegular()
        {
            try
            {
                this.ShowWaitArea();
                var values = Conditions.GetValues();
                values.Add("P_ISSECOND", "S");
                values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

                DataTable dt = SqlExecuter.Query("GetReliabilityVerificationRequestRegularRgisterList", "10003", values);

                if (dt.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData");
                }

                grdReReliabiVerifiReqRgistRegular.DataSource = dt;
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

        /// <summary>
        /// 의뢰서 출력 저장
        /// </summary>
        private void SaveRequestPrinting()
        {
            DataTable dt = grdReliabiVerifiReqPrintRegular.View.GetCheckedRows();

            // CT_RELIABILITYREFMANUFACTURING Table에 저장될 Data
            DataTable reliabilityVerificationTable = new DataTable();
            reliabilityVerificationTable.TableName = "list";
            DataRow row = null;

            reliabilityVerificationTable.Columns.Add(new DataColumn("REQUESTNO", typeof(string)));
            reliabilityVerificationTable.Columns.Add(new DataColumn("ENTERPRISEID", typeof(string)));
            reliabilityVerificationTable.Columns.Add(new DataColumn("PLANTID", typeof(string)));
            reliabilityVerificationTable.Columns.Add(new DataColumn("LOTID", typeof(string)));
            reliabilityVerificationTable.Columns.Add(new DataColumn("PRODUCTDEFID", typeof(string)));
            reliabilityVerificationTable.Columns.Add(new DataColumn("PRODUCTDEFVERSION", typeof(string)));
            reliabilityVerificationTable.Columns.Add(new DataColumn("LOCKINGTXNHISTKEY", typeof(string)));

            foreach (DataRow dr in dt.Rows)
            {
                row = reliabilityVerificationTable.NewRow();
                row["REQUESTNO"] = dr["REQUESTNO"];
                row["ENTERPRISEID"] = dr["ENTERPRISEID"];
                row["PLANTID"] = dr["PLANTID"];
                row["LOTID"] = dr["LOTID"];
                row["PRODUCTDEFID"] = dr["PRODUCTDEFID"];
                row["PRODUCTDEFVERSION"] = dr["PRODUCTDEFVERSION"];
                row["LOCKINGTXNHISTKEY"] = dr["LOCKINGTXNHISTKEY"];

                reliabilityVerificationTable.Rows.Add(row);
            }

            DataSet rullSet = new DataSet();
            rullSet.Tables.Add(reliabilityVerificationTable);

            ExecuteRule("SaveReliabilityVerificationRequestRegularPrint", rullSet);
        }

        /// <summary>
        /// 의뢰서 접수/재접수 접수 저장
        /// </summary>
        private void SaveRecept(SmartBandedGrid grid)
        {
            int i = 0;

            DataTable dt = grid.DataSource as DataTable;

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["RECEIVE"].ToString() == "True")
                {
                    i++;
                }
            }

            if (i == 0)
            {
                throw MessageException.Create("NoSaveData");
            }

            // CT_RELIABILITYREFMANUFACTURING Table에 저장될 Data
            DataTable reliabilityVerificationTable = new DataTable();
            reliabilityVerificationTable.TableName = "list";
            DataRow row = null;

            reliabilityVerificationTable.Columns.Add(new DataColumn("REQUESTNO", typeof(string)));
            reliabilityVerificationTable.Columns.Add(new DataColumn("ENTERPRISEID", typeof(string)));
            reliabilityVerificationTable.Columns.Add(new DataColumn("PLANTID", typeof(string)));
            reliabilityVerificationTable.Columns.Add(new DataColumn("RECEIVE", typeof(string)));
            reliabilityVerificationTable.Columns.Add(new DataColumn("LOCKINGTXNHISTKEY", typeof(string)));
            reliabilityVerificationTable.Columns.Add(new DataColumn("LOTID", typeof(string)));
            reliabilityVerificationTable.Columns.Add(new DataColumn("PRODUCTDEFID", typeof(string)));
            reliabilityVerificationTable.Columns.Add(new DataColumn("PRODUCTDEFVERSION", typeof(string)));

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["RECEIVE"].ToString() == "True")
                {
                    row = reliabilityVerificationTable.NewRow();
                    row["REQUESTNO"] = dr["REQUESTNO"];
                    row["ENTERPRISEID"] = dr["ENTERPRISEID"];
                    row["PLANTID"] = dr["PLANTID"];
                    row["RECEIVE"] = "Y";
                    row["LOCKINGTXNHISTKEY"] = dr["LOCKINGTXNHISTKEY"];
                    row["LOTID"] = dr["LOTID"];
                    row["PRODUCTDEFID"] = dr["PRODUCTDEFID"];
                    row["PRODUCTDEFVERSION"] = dr["PRODUCTDEFVERSION"];

                    reliabilityVerificationTable.Rows.Add(row);
                }
            }

            DataSet rullSet = new DataSet();
            rullSet.Tables.Add(reliabilityVerificationTable);

            ExecuteRule("SaveReliabilityVerificationRequestRegularRecept", rullSet);
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
                    if (!string.IsNullOrWhiteSpace(control.Tag.ToString()))
                        control.DataBindings.Add("Text", dataSource, control.Tag.ToString());

                    SetReportControlDataBinding(control.Controls, dataSource);
                }
            }
        }

        #endregion

        #region Global Function

        /// <summary>
        /// Popup 닫혔을때 재검색하기 위한 함수
        /// </summary>
        public void Search()
        {
            OnSearchAsync();
        }

        #endregion
    }
}
