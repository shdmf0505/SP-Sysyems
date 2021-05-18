#region using

using Micube.Framework;
using Micube.Framework.Log;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 신뢰성 검증/계측기 > 수리/폐기 이력등록
    /// 업  무  설  명  : 계측기 관리사항을 조회함.
    /// 생    성    자  : 이승우
    /// 생    성    일  : 2019-10-01
    /// 수  정  이  력  : 2020-02-08 강유라
    ///                  2019-10-29 Layout 변경
    ///                  2019-10-01 최초 생성일
    ///                  2021.02.23 전우성 화면 최적화 및 정리. GrdMain +버튼 클릭시 Row가 생성되지 않고 popup 출력
    ///
    /// </summary>
    public partial class MeasuringInstRepair : SmartConditionManualBaseForm
    {
        #region 생성자

        public MeasuringInstRepair()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGrid();
            InitializeEvent();
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region 수리/폐기 이력정보

            grdMain.GridButtonItem = GridButtonItem.Add | GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            //결제 정보
            var groupSub01 = grdMain.View.AddGroupColumn("MEASURINGBILLINGINFORMATION");
            groupSub01.AddComboBoxColumn("CHARGERMODE", 150, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=MeasuringChargerMode")).SetLabel("MODE").SetIsReadOnly();                                   //결제 - 담당자 구분
            groupSub01.AddTextBoxColumn("DEPARTMENTAPM", 150).SetLabel("DEPARTMENT");                           //결제 - 부서
            groupSub01.AddTextBoxColumn("RECEIVER", 150).SetLabel("RECEIVER");                                  //결제 - 담당자 ID
            groupSub01.AddComboBoxColumn("ISAPPROVAL", 150, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=MeasuringIsapproval")).SetLabel("ISACCEPT").SetIsReadOnly();                              //결제 - 승인여부
            groupSub01.AddTextBoxColumn("APPROVALTIME", 150).SetLabel("MEASURINGAPPROVALTIME");                 //결제 - 결졔일자
            groupSub01.AddTextBoxColumn("REJECTCOMMENTS", 150).SetLabel("REJECTCOMMENTS");                      //결제 - 반려사유
            //손망실 내용
            var groupSub02 = grdMain.View.AddGroupColumn("MEASURINGLOSSLIST");
            groupSub02.AddTextBoxColumn("DEPARTMENT", 150).SetLabel("MEASURINGDEPARTMENT");                     //사용부서(부서ID)
            groupSub02.AddTextBoxColumn("POSITION", 150).SetLabel("MEASURINGPOSITION");                         //수리내역 등록자 지급
            groupSub02.AddTextBoxColumn("REPAIRUSER", 150).SetLabel("MEASURINGREPAIRUSER");                     //수리내역 등록 성명
            groupSub02.AddTextBoxColumn("DEPARTMENTMNT", 150).SetLabel("MEASURINGMANAGEMENTDEPARTMENT");        //관리부서
            groupSub02.AddTextBoxColumn("MANUFACTURECOUNTRY", 150).SetLabel("MEASURINGMANUFACTURECOUNTRY");     //제조국 ID
            groupSub02.AddTextBoxColumn("MANUFACTURER", 150).SetLabel("MEASURINGMANUFACTURER");                 //제조사 ID
            groupSub02.AddTextBoxColumn("OCCURTIME", 150).SetLabel("MEASURINGDATEOFOCCURRENCE");                //발생일시
            groupSub02.AddTextBoxColumn("EQUIPMENTTYPE", 150).SetLabel("MEASURINGEQUIPMENTTYPE");               //장비유형(설비유형)
            groupSub02.AddTextBoxColumn("EQUIPMENTID", 150).SetLabel("MEASURINGEQUIPMENTID").SetIsHidden();     //설비 ID(계측기ID)
            groupSub02.AddTextBoxColumn("EQUIPMENTNAME", 150).SetLabel("MEASURINGEQUIPMENTNAME").SetIsHidden(); //설비 명
            groupSub02.AddTextBoxColumn("MEASURINGID", 150).SetLabel("MEASURINGEQUIPMENTID");                   //계측기명
            groupSub02.AddTextBoxColumn("MEASURINGPRODUCTID", 150).SetLabel("MEASURINGPRODUCTDEFNAME");           //모델
            groupSub02.AddTextBoxColumn("SERIALNO", 150).SetLabel("MEASURINGSERIALNO");                         //시리얼번호 PK
            groupSub02.AddTextBoxColumn("PURCHASEDATE", 150).SetLabel("MEASURINGPURCHASEDATE");                 //구매일
            groupSub02.AddTextBoxColumn("OCCURPLACE", 150).SetLabel("MEASURINGOCCURPLACE");                     //발생장소
            groupSub02.AddTextBoxColumn("CONTROLNO", 150).SetLabel("MEASURINGCONTROLNO");                       //관리번호 PK
            groupSub02.AddTextBoxColumn("CONTROLNAME", 150).SetLabel("MEASURINGCONTROLNAME");                       //관리번호 PK
            groupSub02.AddTextBoxColumn("REPAIRSCRAPID", 150).SetLabel("MEASURINGREPAIRSCRAPID");               //수리폐기ID PK
            groupSub02.AddComboBoxColumn("ISLOSSDANGERS", 150, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=MeasuringLossType")).SetLabel("MEASURINGLOSSDIVISION").SetIsReadOnly();                //손망실구분
            groupSub02.AddTextBoxColumn("PHOTO", 150).SetLabel("MEASURINGPHOTO").SetIsHidden();                 //사진
            groupSub02.AddTextBoxColumn("PLANTID", 150).SetIsHidden();                                          //PLANTID

            //QA팀 조치사항
            var groupSub03 = grdMain.View.AddGroupColumn("MEASURINGQATEAMFINIACTION");
            groupSub03.AddTextBoxColumn("QAENDTIME", 150).SetLabel("MEASURINGPURCHASEDATE");                    //QA완료일자
            groupSub03.AddTextBoxColumn("REPAIRCOST", 150).SetLabel("MEASURINGREPAIRCOST");                     //수리비용

            groupSub03.AddTextBoxColumn("FILEID", 150).SetIsHidden();
            groupSub03.AddTextBoxColumn("FILENAME", 150).SetIsHidden();
            groupSub03.AddTextBoxColumn("FILESIZE", 150).SetIsHidden();
            groupSub03.AddTextBoxColumn("FILEEXT", 150).SetIsHidden();
            groupSub03.AddTextBoxColumn("FILEPATH", 150).SetIsHidden();
            groupSub03.AddTextBoxColumn("CHANGEFLAG", 150).SetIsHidden();
            groupSub03.AddTextBoxColumn("LOCALFILEPATH", 150).SetIsHidden();
            groupSub03.AddTextBoxColumn("FILEFULLPATH", 150).SetIsHidden();
            groupSub03.AddTextBoxColumn("SAFEFILENAME", 150).SetIsHidden();
            groupSub03.AddTextBoxColumn("PROCESSINGSTATUS", 150).SetIsHidden();

            grdMain.View.BestFitColumns();
            grdMain.View.PopulateColumns();

            grdMain.View.SetIsReadOnly();

            #endregion 수리/폐기 이력정보

            #region 발생내용

            grdOccurrence.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdOccurrence.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdOccurrence.View.AddTextBoxColumn("SEQ", 70).SetLabel("MAINTSEQUENCE");//순번
            grdOccurrence.View.AddTextBoxColumn("COMMENTS", 500).SetLabel("MEASURINGDESCRIPTION");//내용
            grdOccurrence.View.AddTextBoxColumn("SERIALNO", 150).SetLabel("MEASURINGSERIALNO").SetIsHidden();//시리얼번호 PK
            grdOccurrence.View.AddTextBoxColumn("CONTROLNO", 150).SetLabel("MEASURINGCONTROLNO").SetIsHidden();//관리번호 PK
            grdOccurrence.View.AddTextBoxColumn("REPAIRSCRAPID", 150).SetLabel("MEASURINGREPAIRSCRAPID").SetIsHidden();//수리폐기ID PK

            grdOccurrence.View.PopulateColumns();
            grdOccurrence.View.SetIsReadOnly();

            #endregion 발생내용

            #region 증상

            grdSymptom.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdSymptom.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdSymptom.View.AddTextBoxColumn("SEQ", 70).SetLabel("MAINTSEQUENCE");//순번
            grdSymptom.View.AddTextBoxColumn("COMMENTS", 500).SetLabel("MEASURINGDESCRIPTION");//내용
            grdSymptom.View.AddTextBoxColumn("SERIALNO", 150).SetLabel("MEASURINGSERIALNO").SetIsHidden();//시리얼번호 PK
            grdSymptom.View.AddTextBoxColumn("CONTROLNO", 150).SetLabel("MEASURINGCONTROLNO").SetIsHidden();//관리번호 PK
            grdSymptom.View.AddTextBoxColumn("REPAIRSCRAPID", 150).SetLabel("MEASURINGREPAIRSCRAPID").SetIsHidden();//수리폐기ID PK

            grdSymptom.View.PopulateColumns();
            grdSymptom.View.SetIsReadOnly();

            #endregion 증상

            #region 조치내용

            grdQATeamAction.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdQATeamAction.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            grdQATeamAction.View.AddSpinEditColumn("SEQ", 70).SetLabel("MAINTSEQUENCE").SetIsReadOnly();//순번
            grdQATeamAction.View.AddTextBoxColumn("COMMENTS", 500).SetLabel("MEASURINGDESCRIPTION");//내용
            grdQATeamAction.View.AddTextBoxColumn("SERIALNO", 150).SetLabel("MEASURINGSERIALNO").SetIsReadOnly().SetIsHidden();//시리얼번호 PK
            grdQATeamAction.View.AddTextBoxColumn("CONTROLNO", 150).SetLabel("MEASURINGCONTROLNO").SetIsReadOnly().SetIsHidden();//관리번호 PK
            grdQATeamAction.View.AddTextBoxColumn("REPAIRSCRAPID", 150).SetLabel("MEASURINGREPAIRSCRAPID").SetIsReadOnly().SetIsHidden();//수리폐기ID PK

            grdQATeamAction.View.PopulateColumns();
            grdQATeamAction.View.SetIsReadOnly();

            #endregion 조치내용
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
                    MeasuringInstRepairPopup frmPopup = new MeasuringInstRepairPopup(string.IsNullOrWhiteSpace(Format.GetString(row["SERIALNO"])), false)
                    {
                        StartPosition = FormStartPosition.CenterParent,
                        Owner = this
                    };

                    frmPopup.SetRowParent(row);

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

            // Grid Row 더블클릭 이벤트
            grdMain.View.DoubleClick += (s, e) =>
            {
                if (grdMain.View.FocusedRowHandle < 0)
                {
                    return;
                }

                try
                {
                    DialogManager.ShowWaitArea(pnlMain);

                    DataRow row = grdMain.View.GetFocusedDataRow();
                    MeasuringInstRepairPopup frmPopup = new MeasuringInstRepairPopup(string.IsNullOrWhiteSpace(Format.GetString(row["SERIALNO"])), false)
                    {
                        StartPosition = FormStartPosition.CenterParent,
                        Owner = this
                    };

                    frmPopup.SetRowParent(row);

                    if (frmPopup.ShowDialog().Equals(DialogResult.OK))
                    {
                        SendKeys.Send("{F5}");
                    }
                }
                finally
                {
                    DialogManager.CloseWaitArea(pnlMain);
                }
            };

            // Grid Row 선택시 이벤트
            grdMain.View.FocusedRowChanged += (s, e) =>
            {
                if (e.FocusedRowHandle < 0)
                {
                    return;
                }

                try
                {
                    grdOccurrence.View.ClearDatas();
                    grdSymptom.View.ClearDatas();
                    grdQATeamAction.View.ClearDatas();

                    DataRow dr = grdMain.View.GetFocusedDataRow();
                    Dictionary<string, object> param = new Dictionary<string, object>
                    {
                        { "P_SERIALNO", dr["SERIALNO"].ToSafeString() },
                        { "P_CONTROLNO", dr["CONTROLNO"].ToSafeString() },
                        { "P_REPAIRSCRAPID", dr["REPAIRSCRAPID"].ToSafeString() }
                    };

                    this.ShowWaitArea();

                    grdOccurrence.DataSource = SqlExecuter.Query("SelectRawmeasuringRepairOccurDirect", "10001", param);
                    grdSymptom.DataSource = SqlExecuter.Query("SelectRawmeasuringRepairSymptomsDirect", "10001", param);
                    grdQATeamAction.DataSource = SqlExecuter.Query("SelectRawmeasuringRepairActionDirect", "10001", param);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message);
                }
                finally
                {
                    this.CloseWaitArea();
                }
            };

            // Button 메일발송 Click 이벤트
            btnMailSend.Click += (s, e) =>
            {
                if (grdMain.View.FocusedRowHandle < 0)
                {
                    ShowMessage("CheckSelectData"); // 데이터를 선택하세요.
                    return;
                }

                SendMailApprovaPopup popup = new SendMailApprovaPopup
                {
                    Owner = this,
                    CurrentDataRow = grdMain.View.GetFocusedDataRow()
                };

                if (popup.ShowDialog().Equals(DialogResult.OK))
                {
                    SendKeys.Send("{F5}");
                }
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
                await base.OnSearchAsync();

                DialogManager.ShowWaitArea(this.pnlContent);

                grdMain.View.ClearDatas();

                var value = Conditions.GetValues();
                value.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                if (await SqlExecuter.QueryAsync("SelectRawmeasuringRepairScrap", "10001", value) is DataTable dt)
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

            DateTime dateNow = DateTime.Now;

            Conditions.AddDateEdit("P_PERIOD_PERIODFR").SetLabel("MEASURINGDATEOFOCCURRENCE").SetDisplayFormat("yyyy-MM-dd")
                      .SetPosition(0.1)
                      .SetDefault(dateNow.ToString("yyyy-01-01"))
                      .SetValidationIsRequired();

            Conditions.AddDateEdit("P_PERIOD_PERIODTO").SetLabel("").SetDisplayFormat("yyyy-MM-dd")
                      .SetPosition(0.2)
                      .SetDefault(dateNow.ToString("yyyy-12-31"))
                      .SetValidationIsRequired();

            //Site 공장
            Conditions.AddTextBox("p_plantId").SetLabel("PLANT").SetPosition(1.1);

            //장비구분
            Conditions.AddComboBox("p_inspectionResultResouceType", new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID={"MeasurementType"}"), "CODENAME", "CODEID")
                      .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                      .SetEmptyItem()
                      .SetLabel("MEASURINGEQUIPMENTTYPE")
                      .SetPosition(1.2);

            //관리번호
            Conditions.AddTextBox("P_CONTROLNO").SetLabel("MEASURINGCONTROLNO").SetPosition(1.3);

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "PLANTID", UserInfo.Current.Plant },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType }
            };

            #region 부서

            var popup = Conditions.AddSelectPopup("p_departmentInfo", new SqlQuery("GetMeasuringSearchDepartment", "10001", param), "DEPARTMENTNAME", "DEPARTMENTNAME")
                                  .SetPopupLayout("DEPARTMENTNAME", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(300, 600, FormBorderStyle.FixedToolWindow)
                                  .SetLabel("MEASURINGDEPARTMENT")
                                  .SetPosition(3.1);

            popup.GridColumns.AddTextBoxColumn("DEPARTMENTNAME", 150).SetLabel("DEPARTMENTNAME");

            #endregion 부서

            #region 계측기 정보

            popup = Conditions.AddSelectPopup("P_MEASURINGID", new SqlQuery("GetMeasuringIdList", "10001", param), "MEASURINGID", "MEASURINGID")
                              .SetPopupLayout("MEASURINGEQUIPMENTID", PopupButtonStyles.Ok_Cancel, true, true)
                              .SetPopupResultCount(1)
                              .SetPopupLayoutForm(400, 600, FormBorderStyle.FixedToolWindow)
                              .SetLabel("MEASURINGEQUIPMENTID")
                              .SetRelationIds("P_PLANTID")
                              .SetPopupAutoFillColumns("MEASURINGID")
                              .SetPosition(3.2);

            popup.Conditions.AddTextBox("MEASURINGID").SetLabel("MEASURINGEQUIPMENTID");
            popup.GridColumns.AddTextBoxColumn("MEASURINGID", 150).SetLabel("MEASURINGEQUIPMENTID");

            #endregion 계측기 정보

            #region 모델

            popup = Conditions.AddSelectPopup("P_MEASURINGPRODUCTID", new SqlQuery("GetMeasuringProductIdList", "10001", param), "MEASURINGPRODUCTID", "MEASURINGPRODUCTID")
                              .SetPopupLayout("MEASURINGPRODUCTDEFNAME", PopupButtonStyles.Ok_Cancel, true, true)
                              .SetPopupResultCount(1)
                              .SetPopupLayoutForm(400, 800, FormBorderStyle.FixedToolWindow)
                              .SetLabel("MEASURINGPRODUCTDEFNAME")
                              .SetRelationIds("P_PLANTID")
                              .SetPopupAutoFillColumns("MEASURINGPRODUCTID")
                              .SetPosition(3.3);

            popup.Conditions.AddTextBox("MEASURINGPRODUCTID").SetLabel("MEASURINGPRODUCTDEFNAME");
            popup.GridColumns.AddTextBoxColumn("MEASURINGPRODUCTID", 150).SetLabel("MEASURINGPRODUCTDEFNAME");

            #endregion 모델
        }

        #endregion 검색
    }
}