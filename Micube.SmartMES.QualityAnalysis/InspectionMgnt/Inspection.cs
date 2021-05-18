#region using

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraReports.UI;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

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
    /// 프 로 그 램 명  : 품질관리 > 검사원 관리 > 검사원 관리
    /// 업  무  설  명  : 검사원에 대한 기준정보를 관리한다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-07-31
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class Inspection : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public Inspection()
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
            InitializeGrid();         
        }

        /// <summary>        
        /// 검사원 관리현황 그리드
        /// </summary>
        private void InitializeGrid()
        {
            grdInspection.GridButtonItem = GridButtonItem.Export;
            grdInspection.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdInspection.View.CheckMarkSelection.MultiSelectCount = 1;
            grdInspection.View.SetIsReadOnly();

            grdInspection.View.AddTextBoxColumn("INSPECTORID", 150)
                .SetTextAlignment(TextAlignment.Center); // 검사원 ID

            grdInspection.View.AddTextBoxColumn("INSPECTORNUMBER", 150); // 검사원번호

            grdInspection.View.AddTextBoxColumn("CAPACITYTYPE", 150); // 자격구분명
            
            grdInspection.View.AddTextBoxColumn("AREANAME", 150); // 작업장명

            grdInspection.View.AddTextBoxColumn("EMPNO", 150)
                .SetLabel("EMPLOYEENUMBER"); // 사원번호

            grdInspection.View.AddTextBoxColumn("INSPECTORNAME", 100); // 검사자명

            grdInspection.View.AddTextBoxColumn("GRADE", 100)
                .SetTextAlignment(TextAlignment.Center); // 검사등급

            grdInspection.View.AddTextBoxColumn("ENTERDATE", 140)
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime); // 입사일자

            grdInspection.View.AddTextBoxColumn("RETIREDATE", 140)
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime); // 퇴사일자

            grdInspection.View.AddTextBoxColumn("CAREER", 150)
                .SetTextAlignment(TextAlignment.Center); // 총경력

            grdInspection.View.AddTextBoxColumn("CERTDATE", 140)
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime); // 인증평가일

            grdInspection.View.AddTextBoxColumn("NEXTCERTDATE", 140)
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime); // 차기인증평가일

            grdInspection.View.AddTextBoxColumn("ISCERTFINISH", 150)
                .SetTextAlignment(TextAlignment.Center); // 인증평가 완료여부

            grdInspection.View.AddTextBoxColumn("EVALUATIONTYPENAME", 100)
                .SetLabel("EVALUATIONTYPE")
                .SetTextAlignment(TextAlignment.Center); // 평가구분

            grdInspection.View.AddTextBoxColumn("ENTERPRISEID")
                .SetIsHidden(); // 회사 ID
            grdInspection.View.AddTextBoxColumn("PLANTID")
                .SetIsHidden(); // Site ID
            grdInspection.View.AddTextBoxColumn("INSPECTIONCLASSID")
                .SetIsHidden(); // 자격구분 ID
            grdInspection.View.AddTextBoxColumn("AREAID")
                .SetIsHidden(); // 작업장 ID
            grdInspection.View.AddTextBoxColumn("PREVCAREERYEAR")
                .SetIsHidden(); // 이전경력(년)
            grdInspection.View.AddTextBoxColumn("PREVCAREERMONTH")
                .SetIsHidden(); // 이전경력(월)
            grdInspection.View.AddTextBoxColumn("SCORE")
                .SetIsHidden(); // 점수
            grdInspection.View.AddTextBoxColumn("EVALUATIONTYPE")
                .SetIsHidden(); // 평가구분코드

            grdInspection.View.PopulateColumns();

            grdInspection.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            //btnRegistartion.Click += BtnRegistartion_Click;
            //btnDelete.Click += BtnDelete_Click;
            //btnPrintLabel.Click += BtnLabelPrint_Click;

            grdInspection.View.RowStyle += View_RowStyle;
            grdInspection.View.RowClick += View_RowClick;
            grdInspection.View.AddingNewRow += View_AddingNewRow;
        }

        /// <summary>
        /// 검사원 라벨 출력버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLabelPrint_Click(object sender, EventArgs e)
        {
            DataSet dsReport = new DataSet();
            DataTable labelData = grdInspection.View.GetCheckedRows();

            if (labelData.Rows.Count == 0)
            {
                throw MessageException.Create("GridNoChecked");
            }
         
            labelData.Columns.Add("INSPECTORBARCODE"); // 라벨 바코드
            labelData.Columns.Add("TODAY"); // 현재날짜

            foreach (DataRow row in labelData.Rows)
            {
                string label = Format.GetString(row["AREAID"]) + Format.GetString(row["EMPNO"]);
                string upperLabel = label.ToUpper();

                row["INSPECTORBARCODE"] = upperLabel;
                row["TODAY"] = DateTime.Now.Date.ToString("yyyy-MM-dd");
            }

            for (int i = 0; i < 139; i++)
            {
                labelData.ImportRow(labelData.Rows[0]);
            }
            
            dsReport.Tables.Add(labelData);

            Assembly assembly = Assembly.GetAssembly(this.GetType());
            Stream stream = assembly.GetManifestResourceStream("Micube.SmartMES.QualityAnalysis.Report.InspectorLabel.repx");

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

        /// <summary>
        /// 삭제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DataTable dt = grdInspection.View.GetCheckedRows();

            if (dt.Rows.Count == 0)
            {
                // 체크된 행이 없습니다.
                this.ShowMessage("GridNoChecked");
            }
            else
            {
                // 삭제하시겠습니까?
                if (this.ShowMessageBox("IsDeleted", "Message", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    DeleteInspection();
                    OnSearchAsync();
                }
            }
        }

        /// <summary>
        /// 검사원 등록할 수 있는 팝업 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRegistartion_Click(object sender, EventArgs e)
        {
            pnlContent.ShowWaitArea();

            InspectorPopup popup = new InspectorPopup();
            popup.Owner = this;
            popup.StartPosition = FormStartPosition.CenterParent;
            popup._inputFlag = "Insert";

            if (popup.ShowDialog() == DialogResult.OK)
            {
                popup.Close();
                OnSearchAsync();
            }

            pnlContent.CloseWaitArea();
        }

        /// <summary>
        /// 오늘날짜 기준으로 차기 인증평가일에 대한 Row 알람설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            DataTable dt = (grdInspection.DataSource as DataTable);

            if (dt == null || dt.Rows.Count == 0) return;

            if (e.RowHandle >= 0)
            {
                // 오늘날짜 기준으로 차기 인증평가일이 지났지만 인증평가 완료여부가 N이라면 붉은색 알람
                if (Convert.ToDateTime(dt.Rows[e.RowHandle]["NEXTCERTDATE"]) < DateTime.Now && dt.Rows[e.RowHandle]["ISCERTFINISH"].Equals("N"))
                {
                    e.Appearance.BackColor = Color.Tomato;
                    e.HighPriority = true;
                }
                // 오늘날짜 기준으로 차기 인증평가일이 2주후이지만 인증평가 완료여부가 N이라면 노란색 알람
                else if (Convert.ToDateTime(dt.Rows[e.RowHandle]["NEXTCERTDATE"]).AddDays(-14) < DateTime.Now && dt.Rows[e.RowHandle]["ISCERTFINISH"].Equals("N"))
                {
                    e.Appearance.BackColor = Color.Gold;
                    e.HighPriority = true;
                }
            }
        }

        /// <summary>
        /// Row Double Click시 해당 검사원에 대해 수정할 수 있는 Popup 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                pnlContent.ShowWaitArea();

                InspectorPopup popup = new InspectorPopup();
                popup.Owner = this;
                popup.CurrentDataRow = grdInspection.View.GetFocusedDataRow();
                popup.StartPosition = FormStartPosition.CenterParent;
                popup._inputFlag = "Update";
                popup.btnSave.Enabled = btnFlag.Enabled;

                if (popup.ShowDialog() == DialogResult.OK)
                {
                    popup.Close();
                    OnSearchAsync();
                }

                pnlContent.CloseWaitArea();
            }
        }

        /// <summary>
        /// Row 추가시 Enterprise와 Plant를 세팅한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            args.NewRow["PLANTID"] = UserInfo.Current.Plant;
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            DataTable changed = grdInspection.GetChangedRows();

            ExecuteRule("SaveInspectionGrade", changed);
        }

        /// <summary>
        /// 툴바버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("PrintLabel"))
            {
                BtnLabelPrint_Click(null, null);
            }
            else if (btn.Name.ToString().Equals("Regist"))
            {
                BtnRegistartion_Click(null, null);
            }
            else if (btn.Name.ToString().Equals("Delete"))
            {
                BtnDelete_Click(null, null);
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

            DataTable dt = await SqlExecuter.QueryAsync("GetInspection", "10001", values);

            if (dt.Rows.Count < 1)
            {
                // 조회할 데이터가 없습니다.
                this.ShowMessage("NoSelectData");
                grdInspection.DataSource = null;
                return;
            }

            // 총 경력을 계산해서 보여준다.
            foreach (DataRow row in dt.Rows)
            {
                string yearLanguage = Language.Get("YEAR");
                string monthLanguage = Language.Get("MONTH");

                DateTime now = DateTime.Now;
                DateTime enterDate = Convert.ToDateTime(row["ENTERDATE"]);

                int diffMonth = 12 * (now.Year - enterDate.Year) + (now.Month - enterDate.Month);
                int diffYear = 0;

                // 총경력 계산
                int diffPrevMonth = Convert.ToInt32(Format.GetDouble(row.GetObject("PREVCAREERMONTH"), 0));
                int diffPrevYear = Convert.ToInt32(Format.GetDouble(row.GetObject("PREVCAREERYEAR"), 0));

                int totalCareerMonth = diffMonth + diffPrevMonth;
                int totalCareerYear = diffYear + diffPrevYear;

                if (totalCareerMonth >= 12)
                {
                    totalCareerYear += totalCareerMonth / 12;
                    totalCareerMonth = totalCareerMonth % 12;

                    if (totalCareerMonth == 0)
                    {
                        row["CAREER"] = totalCareerYear + yearLanguage;
                    }
                    else
                    {
                        row["CAREER"] = totalCareerYear + yearLanguage + " " + totalCareerMonth + monthLanguage;
                    }
                }
                else
                {
                    if (totalCareerYear == 0)
                    {
                        row["CAREER"] = totalCareerMonth + monthLanguage;
                    }
                    else
                    {
                        row["CAREER"] = totalCareerYear + yearLanguage + " " + totalCareerMonth + monthLanguage;
                    }
                }
            }

            grdInspection.DataSource = dt;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            Conditions.AddComboBox("p_isUseDate", new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=YesNo"), "CODENAME", "CODEID")
                .SetLabel("ISUSEDATE")
                .SetDefault("N")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetPosition(1.1);

            InitializeConditionPopup_Area();

            Conditions.AddComboBox("p_capacityType", new SqlQuery("GetCapacityType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "INSPECTIONCLASSNAME", "INSPECTIONCLASSID")
                .SetLabel("CAPACITYTYPE")
                .SetEmptyItem()
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetPosition(4.2);

            Conditions.AddComboBox("p_evaluationType", new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=EvaluationType"), "CODENAME", "CODEID")
                .SetLabel("EVALUATIONTYPE")
                .SetEmptyItem()
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetPosition(4.3);
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            SmartComboBox isUseDate = Conditions.GetControl<SmartComboBox>("p_isUseDate");
            SmartPeriodEdit date1 = Conditions.GetControl<SmartPeriodEdit>("p_evaluationDate");
            SmartPeriodEdit date2 = Conditions.GetControl<SmartPeriodEdit>("p_nextEvaluationDate");

            date1.Enabled = false;
            date2.Enabled = false;

            isUseDate.EditValueChanged += (s, e) =>
            {
                if (isUseDate.EditValue.Equals("Y"))
                {
                    date1.Enabled = true;
                    date2.Enabled = true;
                }
                else
                {
                    date1.Enabled = false;
                    date2.Enabled = false;
                }
            };

            //SmartDateRangeEdit frDate = Conditions.GetControl<SmartDateRangeEdit>("P_EVALUATIONDATE");
            //frDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            //frDate.Properties.Mask.EditMask = "yyyy-MM-dd";
            //frDate.EditValue = DateTime.Now.AddYears(1);
        }

        /// <summary>
        /// 작업장 조회조건
        /// </summary>
        private void InitializeConditionPopup_Area()
        {
            // 팝업 컬럼설정
            var equipmentPopupColumn = Conditions.AddSelectPopup("p_areaId", new SqlQuery("GetAreaListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID")
               .SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("AREA")
               .SetPopupResultCount(1)
               .SetPosition(4.1);

            // 팝업 조회조건
            equipmentPopupColumn.Conditions.AddTextBox("AREAIDNAME")
                .SetLabel("AREAIDNAME");

            // 팝업 그리드
            equipmentPopupColumn.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetValidationKeyColumn();
            equipmentPopupColumn.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            grdInspection.View.CheckValidation();

            DataTable changed = grdInspection.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        /// <summary>
        /// Check한 Row 삭제
        /// </summary>
        private void DeleteInspection()
        {
            DataTable dt = grdInspection.View.GetCheckedRows();
            dt.TableName = "list";

            dt.Columns.Add("_STATE_", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                row["_STATE_"] = "deleted";
            }

            this.ExecuteRule("SaveInspector", dt);           
        }

        #endregion

        #region Popup

        /// <summary>
        /// 그리드 작업장 Ppopup
        /// </summary>
        private void AreaPopup()
        {
            var areaPopup = grdInspection.View.AddSelectPopupColumn("AREAID", 150, new SqlQuery("GetAreaListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("AREAID", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(400, 600, FormBorderStyle.FixedToolWindow)
                .SetLabel("AREA");

            areaPopup.Conditions.AddTextBox("AREAIDNAME");

            areaPopup.GridColumns.AddTextBoxColumn("AREAID", 150);
            areaPopup.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }

        #endregion

    }
}
