#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 신뢰성 검증/계측기 > 이력카드 등록
    /// 업  무  설  명  : 계측기 관리사항을 조회함.
    /// 생    성    자  : 이승우
    /// 생    성    일  : 2019-10-01
    /// 수  정  이  력  : 2020-01-29 다국어적용 컬럼명 변경
    ///                  2019-10-29 Layout 변경
    ///                  2019-10-01 최초 생성일
    ///                  2021.02.23 전우성 화면 최적화 및 정리. GrdMain +버튼 클릭시 Row가 생성되지 않고 popup 출력
    ///
    /// </summary>
    public partial class MeasuringInstCalibration : SmartConditionManualBaseForm
    {
        #region 생성자

        public MeasuringInstCalibration()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeLanguageKey();
            InitializeGrid();
            InitializeEvent();
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            grdMain.LanguageKey = "MEASUREMENTLIST";
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdMain.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdMain.View.AddTextBoxColumn("CREATEDTIME", 180).SetLabel("CREATIONDATE");
            grdMain.View.AddTextBoxColumn("MODIFIEDTIME", 180);
            grdMain.View.AddTextBoxColumn("SERIALNO", 150).SetLabel("MEASURINGSERIALNO");
            grdMain.View.AddTextBoxColumn("CONTROLNO", 150).SetLabel("MEASURINGCONTROLNO");
            grdMain.View.AddTextBoxColumn("CONTROLNAME", 150).SetLabel("MEASURINGCONTROLNAME");
            grdMain.View.AddTextBoxColumn("EQUIPMENTTYPE", 150).SetIsHidden();
            grdMain.View.AddTextBoxColumn("EQUIPMENTTYPENAME", 150).SetLabel("MEASURINGEQUIPMENTTYPENAME");
            grdMain.View.AddTextBoxColumn("MEASURINGID", 150).SetLabel("MEASURINGEQUIPMENTID");
            grdMain.View.AddTextBoxColumn("MEASURINGPRODUCTID", 150).SetLabel("MEASURINGPRODUCTDEFNAME");
            grdMain.View.AddTextBoxColumn("ENTERPRISEID", 150).SetLabel("MEASURINGENTERPRISEID");
            grdMain.View.AddTextBoxColumn("AREAID", 150).SetLabel("MEASURINGAREAID").SetIsHidden();
            grdMain.View.AddTextBoxColumn("AREANAME", 150).SetLabel("MEASURINGAREAID");
            grdMain.View.AddTextBoxColumn("PLANTID", 150).SetLabel("MEASURINGPLANTID");
            grdMain.View.AddTextBoxColumn("UNIT", 150).SetLabel("MEASURINGUNIT");
            grdMain.View.AddTextBoxColumn("TYPESPEC", 150).SetLabel("TYPESPEC");
            grdMain.View.AddTextBoxColumn("MANUFACTURES", 150).SetLabel("MEASURINGMANUFACTURES");
            grdMain.View.AddTextBoxColumn("MANUFACTURECOUNTRY", 150).SetLabel("MEASURINGMANUFACTURECOUNTRY");
            grdMain.View.AddTextBoxColumn("MANUFACTURER", 150).SetLabel("MEASURINGMANUFACTURER");
            grdMain.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetLabel("MEASURINGPROCESSSEGMENTID").SetIsHidden();
            grdMain.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150).SetLabel("MEASURINGPROCESSSEGMENTID");
            grdMain.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 150).SetLabel("MEASURINGPROCESSSEGMENTVERSION").SetIsHidden();
            grdMain.View.AddTextBoxColumn("DEPARTMENT", 150).SetLabel("MEASURINGDEPARTMENT");
            grdMain.View.AddTextBoxColumn("PURCHASEDATE", 150).SetLabel("MEASURINGPURCHASEDATE");
            grdMain.View.AddTextBoxColumn("PURCHASEAMOUNT", 150).SetLabel("MEASURINGPURCHASEAMOUNT").SetDisplayFormat("{0:N2}", MaskTypes.Numeric);
            grdMain.View.AddTextBoxColumn("ISPC", 150).SetLabel("MEASURINGISPC");
            grdMain.View.AddTextBoxColumn("OSVERSION", 150).SetLabel("MEASURINGOSVERSION");
            grdMain.View.AddTextBoxColumn("SORTWAREVERSION", 150).SetLabel("MEASURINGSORTWAREVERSION");
            grdMain.View.AddTextBoxColumn("SCRAPDATE", 150).SetLabel("MEASURINGSCRAPDATE");
            grdMain.View.AddTextBoxColumn("ISSCRAP", 150).SetLabel("MEASURINGISSCRAP");
            grdMain.View.AddTextBoxColumn("REPAIRDISPOSALID", 150).SetLabel("MEASURINGREPAIRDISPOSALID");
            grdMain.View.AddTextBoxColumn("INSPECTIONCALIBRATIONCYCLE", 150).SetLabel("MEASURINGINSPECTIONCALIBRATIONCYCLE");
            grdMain.View.AddTextBoxColumn("ISINSPECTIONCALIBRATION", 150).SetLabel("MEASURINGISINSPECTIONCALIBRATION");
            grdMain.View.AddTextBoxColumn("ENDCALIBRATIONDATE", 150).SetLabel("MEASURINGENDCALIBRATIONDATE");
            grdMain.View.AddTextBoxColumn("NEXTCALIBRATIONDATE", 150).SetLabel("MEASURINGNEXTCALIBRATIONDATE");
            grdMain.View.AddTextBoxColumn("CALIBRATIONRESULTNAME", 150).SetLabel("MEASURINGCALIBRATIONRESULT");
            grdMain.View.AddTextBoxColumn("ISMIDDLE", 150).SetLabel("ISMIDDLE");
            grdMain.View.AddTextBoxColumn("MIDDLECHECKCYCLETYPE", 150).SetLabel("MIDDLECHECKCYCLETYPE");
            grdMain.View.AddTextBoxColumn("MIDDLECHECKCYCLE", 150).SetLabel("MEASURINGMIDDLECHECKCYCLE");
            grdMain.View.AddTextBoxColumn("MIDDLECHECKDATE", 150).SetLabel("MEASURINGMIDDLECHECKDATE");
            grdMain.View.AddTextBoxColumn("NEXTMIDDLECHECKDATE", 150).SetLabel("MEASURINGNEXTMIDDLECHECKDATE");
            grdMain.View.AddTextBoxColumn("NEXTMIDDLECHECKRESULTNAME", 150).SetLabel("MEASURINGNEXTMIDDLECHECKRESULT");
            grdMain.View.AddTextBoxColumn("ISRNRTARGET", 150).SetLabel("MEASURINGISRNRTARGET");
            grdMain.View.AddTextBoxColumn("RNRCYCLE", 150).SetLabel("MEASURINGRNRCYCLE");
            grdMain.View.AddTextBoxColumn("ISRNR", 150).SetLabel("MEASURINGISRNR");
            grdMain.View.AddTextBoxColumn("RNRPROGRESSDATE", 150).SetLabel("MEASURINGRNRPROGRESSDATE");
            grdMain.View.AddTextBoxColumn("RNRNEXTPROGRESSDATE", 150).SetLabel("MEASURINGRNRNEXTPROGRESSDATE");
            grdMain.View.AddTextBoxColumn("RNRRESULTNAME", 150).SetLabel("MEASURINGRNRRESULT");
            grdMain.View.AddTextBoxColumn("RNRREPORTTYPE", 150).SetLabel("MEASURINGRNRREPORTTYPE");
            grdMain.View.AddTextBoxColumn("RNRREPORTFILENAME", 150).SetLabel("MEASURINGRNRREPORTFILENAME");
            grdMain.View.AddTextBoxColumn("CORRECTION", 150).SetLabel("MEASURINGCORRECTION");
            grdMain.View.AddTextBoxColumn("PERMITVALUE", 150).SetLabel("MEASURINGPERMITVALUE");
            grdMain.View.AddTextBoxColumn("ANALYTICALOCCASION", 150).SetLabel("MEASURINGANALYTICALOCCASION");
            grdMain.View.AddTextBoxColumn("INSPECTIONCALIBRATIONCOA", 150).SetLabel("MEASURINGINSPECTIONCALIBRATIONCOA");
            grdMain.View.AddTextBoxColumn("ISMEASURINGINSTRUMENT", 150).SetLabel("MEASURINGISMEASURINGINSTRUMENT");
            grdMain.View.AddTextBoxColumn("COMMENTS", 150).SetLabel("MEASURINGCOMMENTS");
            grdMain.View.AddTextBoxColumn("CHANGEFLAG", 150).SetIsHidden();
            grdMain.View.AddTextBoxColumn("FILENAME", 150).SetIsHidden();
            grdMain.View.AddTextBoxColumn("FILESIZE", 150).SetIsHidden();
            grdMain.View.AddTextBoxColumn("FILEEXT", 150).SetIsHidden();
            grdMain.View.AddTextBoxColumn("FILEPATH", 150).SetIsHidden();
            grdMain.View.AddTextBoxColumn("REPORTCHANGEFLAG", 150).SetIsHidden();
            grdMain.View.AddTextBoxColumn("CARDCHANGEFLAG", 150).SetIsHidden();
            grdMain.View.AddTextBoxColumn("DOCUMENTCHANGEFLAG", 150).SetIsHidden();
            grdMain.View.AddComboBoxColumn("VALIDSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdMain.View.PopulateColumns();

            grdMain.View.SetIsReadOnly();
            grdMain.View.BestFitColumns();
            grdMain.ShowStatusBar = true;
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // Grid에 +버튼 클릭시 이벤트
            grdMain.ToolbarAddingRow += (s, e) =>
            {
                try
                {
                    e.Cancel = true;
                    DialogManager.ShowWaitArea(pnlContent);

                    DataRow row = (grdMain.DataSource as DataTable).NewRow();
                    MeasuringCardInformationPopup frmPopup = new MeasuringCardInformationPopup(string.IsNullOrWhiteSpace(Format.GetString(row["SERIALNO"])))
                    {
                        StartPosition = FormStartPosition.CenterParent,
                        FormBorderStyle = FormBorderStyle.Sizable,
                        isReadOnlyControls = false
                    };

                    frmPopup.SetRowParent(row);
                    frmPopup.Owner = this;

                    if (frmPopup.ShowDialog().Equals(DialogResult.OK))
                    {
                        SendKeys.Send("{F5}");
                    }
                }
                finally
                {
                    DialogManager.CloseWaitArea(pnlContent);
                }
            };

            // Grid Row DoubleClick 이벤트
            grdMain.View.DoubleClick += (s, e) =>
            {
                if (grdMain.View.FocusedRowHandle < 0)
                {
                    return;
                }

                try
                {
                    DialogManager.ShowWaitArea(pnlContent);

                    DataRow row = grdMain.View.GetFocusedDataRow();
                    MeasuringCardInformationPopup frmPopup = new MeasuringCardInformationPopup(string.IsNullOrWhiteSpace(Format.GetString(row["SERIALNO"])))
                    {
                        StartPosition = FormStartPosition.CenterParent,
                        FormBorderStyle = FormBorderStyle.Sizable,
                        isReadOnlyControls = false
                    };

                    frmPopup.SetRowParent(row);
                    frmPopup.Owner = this;

                    if (frmPopup.ShowDialog().Equals(DialogResult.OK))
                    {
                        SendKeys.Send("{F5}");
                    }
                }
                finally
                {
                    DialogManager.CloseWaitArea(pnlContent);
                }
            };
        }

        #endregion Event

        #region 툴바

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Delete"))
            {
                grdMain.View.CloseEditor();
                DataTable dt = grdMain.GetChangesDeleted();

                if (dt.Rows.Count.Equals(0))
                {
                    ShowMessage(MessageBoxButtons.OK, "CHECKREMOVETOOLDATA"); // 삭제할 데이터를 선택해주세요.
                    return;
                }

                if (ShowMessage(MessageBoxButtons.YesNo, "IsDeleted", btn.Text).Equals(DialogResult.Yes)) // 삭제하시겠습니까?
                {
                    try
                    {
                        this.ShowWaitArea();

                        ExecuteRule("SaveMeasurementDelete", dt);
                        ShowMessage("SuccessSave");

                        SendKeys.Send("{F5}");
                    }
                    catch (Exception ex)
                    {
                        ShowError(ex);
                    }
                    finally
                    {
                        this.CloseWaitArea();
                    }
                }
            }
        }

        #endregion 툴바

        #region 검색

        /// <summary>
        /// 조회 - 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            try
            {
                await base.OnSearchAsync();

                DialogManager.ShowWaitArea(this.pnlContent);

                grdMain.View.ClearDatas();

                var value = Conditions.GetValues();
                value.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                if (await SqlExecuter.QueryAsync("SelectRawMeasuringCardInformation", "10001", value) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData");
                    }

                    grdMain.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                throw MessageException.Create(Format.GetString(ex));
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);
            }
        }

        /// <summary>
        /// 검색 조건 설정.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            //일자 사용 여부
            Conditions.AddCheckEdit("p_isUseDate").SetLabel("ISUSERDATECOND").SetDefault(false).SetPosition(0.2);

            //검/교정주기
            Conditions.AddSpinEdit("p_inspectionCalibrationCycle").SetLabel("MEASURINGINSPECTIONCALIBRATIONCYCLE").SetPosition(8.1);

            //차기검교정일
            Conditions.AddDateEdit("p_NextCalibrationDate").SetLabel("MEASURINGNEXTCALIBRATIONDATE").SetPosition(8.2);
        }

        #endregion 검색
    }
}