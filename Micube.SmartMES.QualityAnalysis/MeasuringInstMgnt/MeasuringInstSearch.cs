#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 계측기 관리> 계측기 관리대장
    /// 업  무  설  명  : 계측기 관리사항을 조회함.
    /// 생    성    자  : 이승우
    /// 생    성    일  : 2019-10-01
    /// 수  정  이  력  : 2020-02-25 강유라
    ///                  2019-10-29 Layout 변경
    ///                  2019-10-01 최초 생성일
    ///                  2021-03-24 전우성 전면최적화 및 메일전송 기능 추가
    ///
    /// </summary>
    public partial class MeasuringInstSearch : SmartConditionManualBaseForm
    {
        #region 생성자

        public MeasuringInstSearch()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeControls();
            InitializeGrid();
            InitializeEvent();
        }

        /// <summary>
        /// 기타 컨트롤러 설정
        /// </summary>
        private void InitializeControls()
        {
            //파일 다운 로드 후 실행 이벤트 적용여부
            fpcCalibration.executeFileAfterDown = true;
            fpcMiddle.executeFileAfterDown = true;
            fpcRNR.executeFileAfterDown = true;

            //파일 컨트롤 다운로드 버튼만 보이게 설정
            fpcCalibration.ButtonVisibleDownload();
            fpcMiddle.ButtonVisibleDownload();
            fpcRNR.ButtonVisibleDownload();

            #region cboQuarterType 설정

            cboQuarterType.ValueMember = "CODEID";
            cboQuarterType.DisplayMember = "CODENAME";
            cboQuarterType.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboQuarterType.ShowHeader = false;

            Dictionary<string, object> dicParam = new Dictionary<string, object>
            {
                { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                { "CODECLASSID", "QuarterType" }
            };

            if (SqlExecuter.Query("GetCodeList", "00001", dicParam) is DataTable dt)
            {
                this.cboQuarterType.DataSource = dt;
                this.cboQuarterType.EditValue = "*";
            }

            #endregion cboQuarterType 설정
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdMain.GridButtonItem = GridButtonItem.Export;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdMain.View.SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("SERIALNO", 150).SetLabel("MEASURINGSERIALNO").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("CONTROLNO", 150).SetLabel("MEASURINGCONTROLNO").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("CONTROLNAME", 150).SetLabel("MEASURINGCONTROLNAME").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("EQUIPMENTTYPE", 150).SetIsHidden();
            grdMain.View.AddTextBoxColumn("EQUIPMENTTYPENAME", 150).SetLabel("MEASURINGEQUIPMENTTYPENAME").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("MEASURINGID", 150).SetLabel("MEASURINGEQUIPMENTID").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("MEASURINGPRODUCTID", 150).SetLabel("MEASURINGPRODUCTDEFNAME").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("ENTERPRISEID", 150).SetLabel("MEASURINGENTERPRISEID").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("AREAID", 150).SetLabel("MEASURINGAREAID").SetIsReadOnly().SetIsHidden();
            grdMain.View.AddTextBoxColumn("AREANAME", 150).SetLabel("MEASURINGAREAID").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("PLANTID", 150).SetLabel("MEASURINGPLANTID").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("UNIT", 150).SetLabel("MEASURINGUNIT").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("MANUFACTURES", 150).SetLabel("MEASURINGMANUFACTURES").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("MANUFACTURECOUNTRY", 150).SetLabel("MEASURINGMANUFACTURECOUNTRY").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("MANUFACTURER", 150).SetLabel("MEASURINGMANUFACTURER").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetLabel("MEASURINGPROCESSSEGMENTID").SetIsReadOnly().SetIsHidden();
            grdMain.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150).SetLabel("MEASURINGPROCESSSEGMENTID").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 150).SetLabel("MEASURINGPROCESSSEGMENTVERSION").SetIsReadOnly().SetIsHidden();
            grdMain.View.AddTextBoxColumn("DEPARTMENT", 150).SetLabel("MEASURINGDEPARTMENT").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("PURCHASEDATE", 150).SetLabel("MEASURINGPURCHASEDATE").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("PURCHASEAMOUNT", 150).SetLabel("MEASURINGPURCHASEAMOUNT").SetIsReadOnly().SetDisplayFormat("{0:N2}", MaskTypes.Numeric);
            grdMain.View.AddTextBoxColumn("ISPC", 150).SetLabel("MEASURINGISPC").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("OSVERSION", 150).SetLabel("MEASURINGOSVERSION").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("SORTWAREVERSION", 150).SetLabel("MEASURINGSORTWAREVERSION").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("SCRAPDATE", 150).SetLabel("MEASURINGSCRAPDATE").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("ISSCRAP", 150).SetLabel("MEASURINGISSCRAP").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("REPAIRDISPOSALID", 150).SetLabel("MEASURINGREPAIRDISPOSALID").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("INSPECTIONCALIBRATIONCYCLE", 150).SetLabel("MEASURINGINSPECTIONCALIBRATIONCYCLE").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("ISINSPECTIONCALIBRATION", 150).SetLabel("MEASURINGISINSPECTIONCALIBRATION").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("ENDCALIBRATIONDATE", 150).SetLabel("MEASURINGENDCALIBRATIONDATE").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("NEXTCALIBRATIONDATE", 150).SetLabel("MEASURINGNEXTCALIBRATIONDATE").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("CALIBRATIONRESULTNAME", 150).SetLabel("MEASURINGCALIBRATIONRESULT").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("ISMIDDLE", 150).SetLabel("ISMIDDLE").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("MIDDLECHECKCYCLE", 150).SetLabel("MEASURINGMIDDLECHECKCYCLE").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("MIDDLECHECKDATE", 150).SetLabel("MEASURINGMIDDLECHECKDATE").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("NEXTMIDDLECHECKDATE", 150).SetLabel("MEASURINGNEXTMIDDLECHECKDATE").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("NEXTMIDDLECHECKRESULTNAME", 150).SetLabel("MEASURINGNEXTMIDDLECHECKRESULT").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("ISRNRTARGET", 150).SetLabel("MEASURINGISRNRTARGET").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("RNRCYCLE", 150).SetLabel("MEASURINGRNRCYCLE").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("ISRNR", 150).SetLabel("MEASURINGISRNR").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("RNRPROGRESSDATE", 150).SetLabel("MEASURINGRNRPROGRESSDATE").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("RNRNEXTPROGRESSDATE", 150).SetLabel("MEASURINGRNRNEXTPROGRESSDATE").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("RNRRESULTNAME", 150).SetLabel("MEASURINGRNRRESULT").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("RNRREPORTTYPE", 150).SetLabel("MEASURINGRNRREPORTTYPE").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("RNRREPORTFILENAME", 150).SetLabel("MEASURINGRNRREPORTFILENAME").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("CORRECTION", 150).SetLabel("MEASURINGCORRECTION").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("PERMITVALUE", 150).SetLabel("MEASURINGPERMITVALUE").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("ANALYTICALOCCASION", 150).SetLabel("MEASURINGANALYTICALOCCASION").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("INSPECTIONCALIBRATIONCOA", 150).SetLabel("MEASURINGINSPECTIONCALIBRATIONCOA").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("ISMEASURINGINSTRUMENT", 150).SetLabel("MEASURINGISMEASURINGINSTRUMENT").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("COMMENTS", 150).SetLabel("MEASURINGCOMMENTS").SetIsReadOnly();

            grdMain.View.PopulateColumns();

            grdMain.View.BestFitColumns();
            grdMain.View.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // 계측기 리스트 더블클릭 - 이력카드 조회
            grdMain.View.DoubleClick += (s, e) =>
            {
                if (grdMain.View.FocusedRowHandle < 0)
                {
                    return;
                }

                DialogManager.ShowWaitArea(pnlContent);

                MeasuringCardInformationPopup frmPopup = new MeasuringCardInformationPopup(false)
                {
                    isReadOnlyControls = true,
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.Sizable,
                    Owner = this
                };

                frmPopup.SetRowParent(grdMain.View.GetFocusedDataRow());
                frmPopup.SaveButtonVisible(false);

                DialogManager.CloseWaitArea(pnlContent);

                frmPopup.ShowDialog();
            };

            // 게측기 리스트 로우 포커스 변경시 이벤트
            grdMain.View.FocusedRowChanged += (s, e) =>
            {
                if (e.FocusedRowHandle < 0)
                {
                    return;
                }

                SetFileProcessingControls(e.FocusedRowHandle);
            };

            // 검사가 Y이며, 결과가 X일 경우 차기 검교정 일자 지나면  => 빨강 아니면 파랑
            grdMain.View.RowCellStyle += (s, e) =>
            {
                if (e.RowHandle < 0)
                {
                    return;
                }

                DataRow row = grdMain.View.GetDataRow(e.RowHandle);

                //검사 여부 N일때 이벤트 적용하지 않음
                if (Format.GetString(row["ISINSPECTIONCALIBRATION"]).Equals("N"))
                {
                    return;
                }

                //검교정 성적서 파일 등록하지 않았을 때
                if (Format.GetString(row["HASINSPFILE"]).Equals("N"))
                {
                    if (Format.GetString(row["NEXTCALIBRATIONDATE"], string.Empty) is string tempString)
                    {
                        if (tempString.Equals(string.Empty))
                        {
                            return;
                        }

                        if (grdMain.View.IsCellSelected(e.RowHandle, e.Column))
                        {
                            //현재일자 기준으로 차기검교정 일자 지났는지 판단
                            e.Appearance.ForeColor = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")) > DateTime.Parse(tempString) ? Color.Red : Color.Blue;
                        }
                    }
                }
            };

            // 분기 콤보박스 변경 이벤트
            cboQuarterType.EditValueChanged += (s, e) =>
            {
                string filter = string.Empty;
                string year = Format.GetString(DateTime.Now.Year);

                switch (cboQuarterType.EditValue)
                {
                    case "Quarter1":
                        filter = string.Concat("[NEXTCALIBRATIONDATE] Between('", year, "-01-01', '", year, "03-31')");
                        break;

                    case "Quarter2":
                        filter = string.Concat("[NEXTCALIBRATIONDATE] Between('", year, "-04-01', '", year, "06-30')");
                        break;

                    case "Quarter3":
                        filter = string.Concat("[NEXTCALIBRATIONDATE] Between('", year, "-07-01', '", year, "09-30')");
                        break;

                    case "Quarter4":
                        filter = string.Concat("[NEXTCALIBRATIONDATE] Between('", year, "-10-01', '", year, "12-31')");
                        break;

                    default:
                        break;
                }

                grdMain.View.ActiveFilterString = filter;
            };

            // 메일발송 버튼 클릭 이벤트
            btnMailSend.Click += (s, e) =>
            {
                Language.LanguageMessageItem msg = Language.GetMessage("MEASURINGINSTCONTENTS");

                Commons.CommonFunction.ShowSendEmailPopup(msg.Title, msg.Message.Replace("/r/n", Environment.NewLine));
            };
        }

        #endregion Event

        #region 검색

        /// <summary>
        /// 조회 - 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);

                await base.OnSearchAsync();

                var value = Conditions.GetValues();
                value.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                value.Add("ISDISPLAY", "Y");

                if (await SqlExecuter.QueryAsync("SelectRawMeasuringCardInformation", "10001", value) is DataTable dt)
                {
                    grdMain.DataSource = dt;

                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                    }
                    else
                    {
                        SetFileProcessingControls(0);
                    }
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

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "AREATYPE", "Area" },
                { "PLANTID", UserInfo.Current.Plant },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                { "AREA", UserInfo.Current.Area }
            };

            //Site 공장
            Conditions.AddTextBox("p_plantId").SetLabel("PLANT").SetPosition(1.1);

            //장비구분
            Conditions.AddComboBox("p_measurementtype", new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID={"MeasurementType"}"), "CODENAME", "CODEID")
                      .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                      .SetDefault("Measurement")
                      .SetValidationIsRequired()
                      .SetLabel("MEASURINGEQUIPMENTTYPE")
                      .SetPosition(2.1);

            #region 계측기 정보 장비명

            var popup = Conditions.AddSelectPopup("P_MEASURINGID", new SqlQuery("GetMeasuringIdList", "10001", param), "MEASURINGID", "MEASURINGID")
                                  .SetPopupLayout("MEASURINGEQUIPMENTID", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupResultCount(1)
                                  .SetPopupLayoutForm(400, 600, FormBorderStyle.FixedToolWindow)
                                  .SetLabel("MEASURINGEQUIPMENTID")
                                  .SetRelationIds("P_PLANTID")
                                  .SetPopupAutoFillColumns("MEASURINGID")
                                  .SetPosition(2.2);

            popup.Conditions.AddTextBox("MEASURINGID").SetLabel("MEASURINGEQUIPMENTID");
            popup.GridColumns.AddTextBoxColumn("MEASURINGID", 150).SetLabel("MEASURINGEQUIPMENTID");

            #endregion 계측기 정보 장비명

            #region 모델

            popup = Conditions.AddSelectPopup("P_MEASURINGPRODUCTID", new SqlQuery("GetMeasuringProductIdList", "10001", param), "MEASURINGPRODUCTID", "MEASURINGPRODUCTID")
                              .SetPopupLayout("MEASURINGPRODUCTDEFNAME", PopupButtonStyles.Ok_Cancel, true, true)
                              .SetPopupResultCount(1)
                              .SetPopupLayoutForm(400, 600, FormBorderStyle.FixedToolWindow)
                              .SetLabel("MEASURINGPRODUCTDEFNAME")
                              .SetRelationIds("P_PLANTID")
                              .SetPopupAutoFillColumns("MEASURINGPRODUCTID")
                              .SetPosition(2.3);

            popup.Conditions.AddTextBox("MEASURINGPRODUCTID").SetLabel("MEASURINGPRODUCTDEFNAME");
            popup.GridColumns.AddTextBoxColumn("MEASURINGPRODUCTID", 150).SetLabel("MEASURINGPRODUCTDEFNAME");

            #endregion 모델

            //검/교정유부
            Conditions.AddComboBox("p_inspectionCalibration", new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID={"YesNo"}"), "CODENAME", "CODEID")
                      .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                      .SetLabel("MEASURINGISINSPECTIONCALIBRATION")
                      .SetPosition(3.1);

            //검/교정주기
            Conditions.AddSpinEdit("p_inspectionCalibrationCycle")
                      .SetLabel("MEASURINGINSPECTIONCALIBRATIONCYCLE")
                      .SetPosition(3.2);

            //차기검교정일
            Conditions.AddDateEdit("p_NextCalibrationDate")
                      .SetLabel("MEASURINGNEXTCALIBRATIONDATE")
                      .SetPosition(3.3);

            #region 부서

            popup = Conditions.AddSelectPopup("p_departmentInfo", new SqlQuery("GetMeasuringSearchDepartment", "10001", param), "DEPARTMENTNAME", "DEPARTMENTNAME")
                              .SetPopupLayout("DEPARTMENTNAME", PopupButtonStyles.Ok_Cancel, true, true)
                              .SetPopupLayoutForm(400, 600, FormBorderStyle.FixedToolWindow)
                              .SetLabel("MEASURINGDEPARTMENT")
                              .SetPosition(3.4);

            popup.Conditions.AddTextBox("DEPARTMENTNAME");
            popup.GridColumns.AddTextBoxColumn("DEPARTMENTNAME", 150).SetLabel("DEPARTMENTNAME");

            #endregion 부서

            #region 작업장

            popup = Conditions.AddSelectPopup("p_areaId", new SqlQuery("GetAreaList", "10003", param), "AREANAME", "AREAID")
                              .SetPopupLayout(Language.Get("SELECTAREAID"), PopupButtonStyles.Ok_Cancel, true, true)
                              .SetPopupResultCount(1)
                              .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                              .SetPopupAutoFillColumns("AREANAME")
                              .SetLabel("AREA")
                              .SetPosition(3.5);

            popup.Conditions.AddTextBox("AREA").SetLabel("TXTAREA");

            popup.GridColumns.AddTextBoxColumn("AREAID", 150);
            popup.GridColumns.AddTextBoxColumn("AREANAME", 200);

            #endregion 작업장
        }

        #endregion 검색

        #region Private Function

        /// <summary>
        /// SmartFileProcessingControl 조회 설정
        /// </summary>
        /// <param name="rowHandle"></param>
        private void SetFileProcessingControls(int rowHandle)
        {
            DataRow row = grdMain.View.GetDataRow(rowHandle);
            string resourceId = Format.GetString(row["SERIALNO"]) + Format.GetString(row["CONTROLNO"]);

            //1.검교정 파일
            SearchFile(resourceId, "CalibrationFile", fpcCalibration);
            //2.중간 점검 파일
            SearchFile(resourceId, "MiddleFile", fpcMiddle);
            //3.R&R 파일
            SearchFile(resourceId, "RNRFile", fpcRNR);
        }

        /// <summary>
        /// 파일 컨트롤에 바인딩 할 파일 데이터 조회 함수
        /// </summary>
        /// <param name="resourceId"></param>
        /// <param name="resourceType"></param>
        private void SearchFile(string resourceId, string resourceType, SmartFileProcessingControl control)
        {
            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                {"P_FILERESOURCEID", resourceId},
                {"P_FILERESOURCETYPE", resourceType},
                {"P_FILERESOURCEVERSION", "0"}
            };

            DataTable fileDt = SqlExecuter.Query("SelectFileInspResult", "10001", values);

            control.DataSource = fileDt;
        }

        #endregion Private Function
    }
}