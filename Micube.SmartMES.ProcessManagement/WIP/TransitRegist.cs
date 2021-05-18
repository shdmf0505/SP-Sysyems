#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;

using Micube.SmartMES.Commons;

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

#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정 관리 > 공정작업 > 인계 등록
    /// 업  무  설  명  : 작업 완료 된 Lot을 다음 공정으로 인계 등록 처리한다.
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2019-08-16
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class TransitRegist : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        private string _processSegmentType = "";

        private bool _isInspectionProcess = false;

        private bool isMessageLoaded = false;
        private string lotId = null;
        #endregion

        #region 생성자

        public TransitRegist()
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
            InitializeControls();
        }

        private void InitializeControls()
        {
            txtLotId.ImeMode = ImeMode.Alpha;

            lblIsRework.Visible = false;
            lblIsRCLot.Visible = false;

            grdLotInfo.ColumnCount = 5;
            grdLotInfo.LabelWidthWeight = "40%";
            grdLotInfo.SetInvisibleFields("PROCESSSTATE", "PROCESSPATHID", "PROCESSSEGMENTID", "PROCESSSEGMENTVERSION",
                "NEXTPROCESSSEGMENTID", "NEXTPROCESSSEGMENTVERSION", "NEXTPROCESSSEGMENTTYPE", "PRODUCTDEFVERSION",
                "PRODUCTTYPE", "PRODUCTDEFTYPEID", "LOTTYPE", "ISHOLD",
                "AREANAME", "DEFECTUNIT", "PCSPNL", "PANELPERQTY",
                "PROCESSSEGMENTTYPE", "STEPTYPE", "ISPRINTLOTCARD", "ISPRINTRCLOTCARD",
                "TRACKINUSER", "TRACKINUSERNAME", "MATERIALCLASS", "AREAID",
                "ISRCLOT", "SELFSHIPINSPRESULT", "SELFTAKEINSPRESULT", "MEASUREINSPRESULT",
                "OSPINSPRESULT", "ISBEFOREROLLCUTTING", "PATHTYPE", "LOTSTATE",
                "WAREHOUSEID", "ISWEEKMNG", "DESCRIPTION", "PARENTPROCESSSEGMENTCLASSID",
                "RTRSHT", "PROCESSSEGMENTCLASSID", "RESOURCENAME", "ISCLAIMLOT", "OSMCHECK", "PROCESSUOM", "PCSARY", "BLKQTY", "RESOURCEID");
            grdLotInfo.Enabled = false;
            pfsInfo.ProcessType = ProcessType.TransitRegist;
            pfsInfo.SetControlsVisible();
            //pfsInfo.Enabled = false;
            pfsInfo.SetReadOnly(true);
            pfsInfo.UnitComboReadOnly = true;
            pfsDetail.ProcessType = ProcessType.TransitRegist;
            pfsDetail.InitializeTabPageVisible();
            //pfsDetail.Enabled = false;
            pfsDetail.SetReadOnly(true);

            ConditionItemSelectPopup areaCondition = new ConditionItemSelectPopup();
            areaCondition.Id = "AREA";
            areaCondition.SearchQuery = new SqlQuery("GetAreaListByAuthority", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "AREATYPE=Area");
            areaCondition.ValueFieldName = "AREAID";
            areaCondition.DisplayFieldName = "AREANAME";
            areaCondition.SetPopupLayout("SELECTAREA", PopupButtonStyles.Ok_Cancel, true, false);
            areaCondition.SetPopupResultCount(1);
            areaCondition.SetPopupLayoutForm(600, 800, FormBorderStyle.SizableToolWindow);
            areaCondition.SetPopupAutoFillColumns("AREANAME");

            areaCondition.Conditions.AddTextBox("AREA");

            areaCondition.GridColumns.AddTextBoxColumn("AREAID", 150);
            areaCondition.GridColumns.AddTextBoxColumn("AREANAME", 200);

            txtArea.Editor.SelectPopupCondition = areaCondition;
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            btnInit.Click += BtnInit_Click;

            txtLotId.Editor.KeyDown += TxtLotId_KeyDown;
            txtLotId.Editor.Click += TxtLotId_Click;

            pfsInfo.EquipmentClassEditValueChanged += PfsInfo_EquipmentClassEditValueChanged;

            pfsDetail.DefectQtyChanged += PfsDetail_DefectQtyChanged;
            pfsDetail.tabProcessFourStepDetail.SelectedPageChanged += TabProcessFourStepDetail_SelectedPageChanged;

            pfsDetail.OnStartLoadingDefect += PfsDetail_OnStartLoadingDefect;
            pfsDetail.OnEndLoadingDefect += PfsDetail_OnEndLoadingDefect;
        }

        private void PfsDetail_OnStartLoadingDefect(object sender, EventArgs e)
        {
            var buttons = pnlToolbar.Controls.Find<SmartButton>(true);
            buttons.ForEach(button => button.IsBusy = true);
            pnlContent.ShowWaitArea();
        }

        private void PfsDetail_OnEndLoadingDefect(object sender, EventArgs e)
        {
            var buttons = pnlToolbar.Controls.Find<SmartButton>(true);
            pnlContent.CloseWaitArea();
            buttons.ForEach(button => button.IsBusy = false);
        }

        private void TabProcessFourStepDetail_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if(e.Page.Name == "tpgMessage")
            {
                loadMessage();
            }
        }

        private void BtnInit_Click(object sender, EventArgs e)
        {
            txtArea.Editor.SetValue(null);
            txtLotId.Text = "";

            ClearDetailInfo();
        }

        private void TxtLotId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ClearDetailInfo();
                if (txtArea.Editor.SelectedData.Count() < 1 || string.IsNullOrWhiteSpace(txtArea.EditValue.ToString()) || string.IsNullOrWhiteSpace(txtLotId.Text))
                {
                    // 작업장, LOT No.는 필수 입력 항목입니다.
                    ShowMessage("AreaLotIdIsRequired");
                    return;
                }

                var buttons = pnlToolbar.Controls.Find<SmartButton>(true);

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("LOTID", txtLotId.Text);

                DataTable dt = SqlExecuter.Query("selectAreaResourceByLot", "10001", param);

                if (dt.Rows.Count < 1)
                {
                    //존재 하지 않는 Lot No. 입니다.
                    ShowMessage("NotExistLotNo");
                    return;
                }

                try
                {
                    buttons.ForEach(button => button.IsBusy = true);
                    pnlContent.ShowWaitArea();

                    // 재작업 여부 확인
                    param = new Dictionary<string, object>();
                    param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    param.Add("PLANTID", UserInfo.Current.Plant);
                    param.Add("AREAID", txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"].ToString());
                    param.Add("LOTID", txtLotId.Text);
                    param.Add("PROCESSSTATE", "WaitForSend");
                    param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                    DataTable processDefTypeInfo = SqlExecuter.Query("GetProcessDefTypeByProcess", "10001", param);

                    string processDefType = "";
                    string lastRework = "";

                    if (processDefTypeInfo.Rows.Count > 0)
                    {
                        DataRow row = processDefTypeInfo.AsEnumerable().FirstOrDefault();

                        processDefType = Format.GetString(row["PROCESSDEFTYPE"]);
                        lastRework = Format.GetString(row["LASTREWORK"]);
                    }

                    string queryVersion = "10001";

                    if (processDefType == "Rework")
                        queryVersion = "10011";

                    // TODO : Query Version 변경
                    DataTable lotInfo = SqlExecuter.Query("SelectLotInfoByProcess", processDefType == "Rework" ? "10032" : "10031", param);

                    if (lotInfo.Rows.Count < 1)
                    {
                        DataTable AreaLot = SqlExecuter.Query("GetLotAreaAndState", "10001", param);
                        ShowMessage("NotAreaOrState", Format.GetString(AreaLot.Rows[0]["AREANAME"]), Format.GetString(AreaLot.Rows[0]["PROCESSTATE"]));
                        ClearDetailInfo();

                        txtLotId.Text = "";
                        txtLotId.Focus();
                        return;
                    }

                    if (!CommonFunction.CheckLotProcessStateByStepType(pfsInfo.ProcessType, lotInfo.Rows[0]["PROCESSSTATE"].ToString(), lotInfo.Rows[0]["STEPTYPE"].ToString()))
                    {
                        ClearDetailInfo();

                        txtLotId.Text = "";
                        txtLotId.Focus();
                        return;
                    }

                    Dictionary<string, object> plantParam = new Dictionary<string, object>();
                    plantParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    plantParam.Add("PLANTID", UserInfo.Current.Plant);

                    DataTable isWipSurveyResult = SqlExecuter.Query("GetPlantIsWipSurvey", "10001", plantParam);

                    if (isWipSurveyResult.Rows.Count > 0)
                    {
                        DataRow row = isWipSurveyResult.AsEnumerable().FirstOrDefault();

                        string isWipSurvey = Format.GetString(row["ISWIPSURVEY"]);

                        if (isWipSurvey == "Y")
                        {
                            // 재공실사가 진행 중 입니다. {0}을 진행할 수 없습니다.
                            ShowMessage("PLANTINWIPSURVEY", Language.Get(string.Join("_", "MENU", MenuId)));
                            ClearDetailInfo();

                            txtLotId.Text = "";
                            txtLotId.Focus();

                            return;
                        }
                    }

                    //체공 상태 체크
                    DataTable dtStaying = SqlExecuter.Query("GetCheckStaying", "10001", param);

                    if (Format.GetTrimString(dtStaying.Rows[0]["ISLOCKING"]).Equals("Y"))
                    {
                        StayReasonCode popup = new StayReasonCode(dtStaying);
                        popup.ShowDialog();

                        if (popup.DialogResult == DialogResult.Cancel)
                        {
                            return;
                        }
                    }

                    if (!CommonFunction.CheckRCLot(lotInfo))
                    {
                        ClearDetailInfo();

                        txtLotId.Text = "";
                        txtLotId.Focus();

                        return;
                    }

                    string isHold = Format.GetString(lotInfo.Rows[0]["ISHOLD"]);
                    string isLocking = Format.GetString(lotInfo.Rows[0]["ISLOCKING"]);

                    if (isHold == "Y")
                    {
                        // 보류 상태의 Lot 입니다.
                        ShowMessage("LotIsHold", string.Format("LotId = {0}", txtLotId.Text));

                        ClearDetailInfo();

                        txtLotId.Text = "";
                        txtLotId.Focus();

                        return;
                    }

                    if (isLocking == "Y")
                    {
                        // Locking 상태의 Lot 입니다.
                        ShowMessage("LotIsLocking", string.Format("LotId = {0}", txtLotId.Text));

                        ClearDetailInfo();

                        txtLotId.Text = "";
                        txtLotId.Focus();

                        return;
                    }

                    //자주검사(출하) 유무 체크
                    if (processDefType != "Rework" && processDefType != "Repeat" && Format.GetFullTrimString(lotInfo.Rows[0]["SELFSHIPINSPRESULT"]).Equals("N"))
                    {
                        // 출하검사 결과가 없습니다. 출하검사 화면으로 이동하시겠습니까? Lot No. : {0}
                        DialogResult drResult = ShowMessage(MessageBoxButtons.YesNo, "NotExistsInspectionShipResult", txtLotId.Text);
                        string lotId = txtLotId.Text;

                        ClearDetailInfo();

                        txtLotId.Text = "";
                        txtLotId.Focus();


                        if (drResult == DialogResult.Yes)
                        {
                            Dictionary<string, object> menuParam = new Dictionary<string, object>();
                            menuParam.Add("AREAID", Format.GetString(txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"]));
							menuParam.Add("AREANAME", Format.GetString(txtArea.Editor.SelectedData.FirstOrDefault()["AREANAME"]));
							menuParam.Add("LOTID", lotId);

                            OpenMenu("PG-SG-0240", menuParam);
                        }

                        return;
                    }

                    //계측검사 검사 유무 체크
                    if (processDefType != "Rework" && processDefType != "Repeat" && Format.GetFullTrimString(lotInfo.Rows[0]["MEASUREINSPRESULT"]).Equals("NA"))
                    {
                        ShowMessage("NotExistsOperationInspectionResult", txtLotId.Text);
                        ClearDetailInfo();

                        txtLotId.Text = "";
                        txtLotId.Focus();

                        return;
                    }
                    //계측검사 결과 체크
                    if (processDefType != "Rework" && processDefType != "Repeat" && Format.GetFullTrimString(lotInfo.Rows[0]["MEASUREINSPRESULT"]).Equals("N"))
                    {

                        // 계측 결과가 NG 입니다. Lot No. : {0}
                        ShowMessage("OperationInspectionResultIsNG", txtLotId.Text);
                        ClearDetailInfo();

                        txtLotId.Text = "";
                        txtLotId.Focus();

                        return;
                    }

                    // 수입검사 필수 여부 체크
                    if (Format.GetFullTrimString(lotInfo.Rows[0]["OSPINSPRESULT"]).Equals("Y"))
                    {
                        // 외주입고 공정입니다. 외주입고품 수입검사를 진행하시기 바랍니다. Lot No. : {0}
                        ShowMessage("OSPInspectionIsRequired", txtLotId.Text);
                        ClearDetailInfo();

                        txtLotId.Text = "";
                        txtLotId.Focus();

                        return;
                    }
                    /*
                    // 자주검사(출하) 여부 확인
                    Dictionary<string, object> inspectionParam = new Dictionary<string, object>();
                    inspectionParam.Add("LOTID", txtLotId.Text);
                    inspectionParam.Add("INSPECTIONDEFID", "SelfInspectionShip");

                    DataTable inspectionResult = SqlExecuter.Query("GetCheckLotInspection", "10002", inspectionParam);

                    if (inspectionResult.Rows.Count > 0)
                    {
                        DataRow row = inspectionResult.Rows.Cast<DataRow>().FirstOrDefault();

                        //string isRequired = Format.GetString(row["ISREQUIRED"]);
                        //string inspectResult = Format.GetString(row["INSPECTIONRESULT"]);

                        //if (isRequired == "Y" && string.IsNullOrWhiteSpace(inspectResult))
                        string checkInspection = Format.GetString(row["CHECKINSPECTION"]);

                        if (checkInspection == "N")
                        {
                            // 출하검사 결과가 없습니다. Lot No. : {0}
                            ShowMessage("NotExistsInspectionShipResult", txtLotId.Text);
                            ClearDetailInfo();

                            txtLotId.Text = "";
                            txtLotId.Focus();

                            return;
                        }
                    }

                    // 계측 입력 여부 체크
                    inspectionParam["INSPECTIONDEFID"] = "OperationInspection";

                    inspectionResult = SqlExecuter.Query("GetCheckLotInspection", "10002", inspectionParam);

                    if (inspectionResult.Rows.Count > 0)
                    {
                        DataRow row = inspectionResult.Rows.Cast<DataRow>().FirstOrDefault();

                        string isRequired = Format.GetString(row["ISREQUIRED"]);
                        string inspectResult = Format.GetString(row["INSPECTIONRESULT"]);

                        if (isRequired == "Y" && string.IsNullOrWhiteSpace(inspectResult))
                        {
                            // 입력된 계측 결과가 없습니다. Lot No. : {0}
                            ShowMessage("NotExistsOperationInspectionResult", txtLotId.Text);
                            ClearDetailInfo();

                            txtLotId.Text = "";
                            txtLotId.Focus();

                            return;
                        }

                        if (isRequired == "Y" && inspectResult == "NG")
                        {
                            // 계측 결과가 NG 입니다. Lot No. : {0}
                            ShowMessage("OperationInspectionResultIsNG", txtLotId.Text);
                            ClearDetailInfo();

                            txtLotId.Text = "";
                            txtLotId.Focus();

                            return;
                        }
                    }
                    */
                    string processSegmentId = Format.GetString(lotInfo.Rows[0]["PROCESSSEGMENTID"]);
                    string description = Format.GetString(lotInfo.Rows[0]["DESCRIPTION"]).ToUpper();
                    string rtrSht = Format.GetString(lotInfo.Rows[0]["RTRSHT"]).ToUpper();

                    string topProcessSegmentClassId = Format.GetString(lotInfo.Rows[0]["PARENTPROCESSSEGMENTCLASSID"]);

                    string materialClass = Format.GetString(lotInfo.Rows[0]["MATERIALCLASS"]);

                    //if ((processSegmentId == "1020101" || processSegmentId == "1020001") && description != "MIG")
                    if (topProcessSegmentClassId == "10" && description != "MIG" && rtrSht == "RTR" && (materialClass == "FG" || materialClass == "SB" || materialClass == "BA"))
                    {
                        Dictionary<string, object> cancelParam = new Dictionary<string, object>();
                        cancelParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                        cancelParam.Add("PLANTID", UserInfo.Current.Plant);
                        cancelParam.Add("LOTID", txtLotId.Text);
                        cancelParam.Add("PROCESSSEGMENTID", processSegmentId);
                        cancelParam.Add("PROCESSSTATE", "WaitForSend");
                        cancelParam.Add("PREVPROCESSSTATE", "WaitForReceive");

                        DataTable checkSendCancel = SqlExecuter.Query("GetLotCancelWorkResultByRollCutting", "10001", cancelParam);

                        if (checkSendCancel.Rows.Count < 1)
                        {
                            // ROLL CUTTING 공정 입니다. {0} 화면에서 인계 등록을 처리하시기 바랍니다.
                            ShowMessage("ProcessSegmentIsRollCutting", Language.Get("MENU_PG-SG-0230"));

                            ClearDetailInfo();

                            txtLotId.Text = "";
                            txtLotId.Focus();

                            return;
                        }
                    }



                    this.lotId = txtLotId.Text;
                    this.isMessageLoaded = false;
                    grdLotInfo.DataSource = lotInfo;
                    pfsDetail.SetLotInfo(txtLotId.Text, Format.GetDecimal(grdLotInfo.GetFieldValue("PANELPERQTY")), Format.GetDecimal(grdLotInfo.GetFieldValue("PNLQTY")), Format.GetDecimal(grdLotInfo.GetFieldValue("PCSQTY")), queryVersion, lotInfo);
                    pfsInfo.SetControlsInfo(txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"].ToString(), txtLotId.Text, lotInfo, queryVersion, lastRework);

                    grdLotInfo.Enabled = true;
                    //pfsInfo.Enabled = true;
                    pfsInfo.SetReadOnly(false);
                    //pfsDetail.Enabled = true;
                    pfsDetail.SetReadOnly(false);

                    pfsDetail.SetDefectGridComboData(txtLotId.Text);

                    string productDefVersion = Format.GetString(grdLotInfo.GetFieldValue("PRODUCTDEFVERSION"));
                    string isRcLot = Format.GetString(grdLotInfo.GetFieldValue("ISRCLOT"));

                    lblInnerRevisionText.Text = productDefVersion;
                    if (processDefType == "Rework")
                        lblIsRework.Visible = true;
                    else
                        lblIsRework.Visible = false;
                    if (isRcLot == "Y")
                        lblIsRCLot.Visible = true;
                    else
                        lblIsRCLot.Visible = false;


                    _processSegmentType = Format.GetString(grdLotInfo.GetFieldValue("PROCESSSEGMENTTYPE"));

                    if ((UserInfo.Current.Enterprise == Constants.EnterPrise_InterFlex && _processSegmentType == "AOIInspection") ||
                        (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong && _processSegmentType == "VRS"))
                    {
                        Dictionary<string, object> checkParam = new Dictionary<string, object>();
                        checkParam.Add("LOTID", txtLotId.Text);
                        checkParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                        checkParam.Add("ISREWORK", "Y");

                        DataTable checkList = SqlExecuter.Query("SelectDefectMapDataForSend", "10001", checkParam);

                        if (checkList.Rows.Count > 0)
                        {
                            _isInspectionProcess = true;

                            pfsDetail.SetAOIDefectPageVisible(false, true);
                        }
                    }


                    pfsInfo.SetUnitOfMaterialControlReadOnly();


                    Dictionary<string, object> commentParam = new Dictionary<string, object>();
                    commentParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    commentParam.Add("PLANTID", UserInfo.Current.Plant);
                    commentParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    commentParam.Add("LOTID", txtLotId.Text);
                    commentParam.Add("PROCESSSEGMENTID", grdLotInfo.GetFieldValue("PROCESSSEGMENTID").ToString());

                    DataTable commentInfo = SqlExecuter.Query("SelectCommentByProcess", queryVersion, commentParam);

                    pfsDetail.CommentDataSource = commentInfo;


                    Dictionary<string, object> processSpecParam = new Dictionary<string, object>();
                    processSpecParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    processSpecParam.Add("PLANTID", UserInfo.Current.Plant);
                    processSpecParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    processSpecParam.Add("LOTID", txtLotId.Text);
                    processSpecParam.Add("PROCESSSEGMENTID", grdLotInfo.GetFieldValue("PROCESSSEGMENTID").ToString());
                    processSpecParam.Add("CONTROLTYPE", "XBARR");
                    processSpecParam.Add("SPECCLASSID", "OperationSpec");

                    DataTable processSpecInfo = SqlExecuter.Query("SelectProcessSpecByProcess", queryVersion, processSpecParam);

                    pfsDetail.ProcessSpecDataSource = processSpecInfo;

                    //2021.03.04 UOM정보 셋팅
                    pfsInfo.setBasicSetUOM();
                    pfsDetail.setQtyColText();

                    /*
                    Dictionary<string, object> defectParam = new Dictionary<string, object>();
                    defectParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    defectParam.Add("PLANTID", UserInfo.Current.Plant);
                    defectParam.Add("LOTID", txtLotId.Text);
                    defectParam.Add("PROCESSSEGMENTCLASSTYPE", "MiddleProcessSegmentClass");
                    defectParam.Add("RESOURCETYPE", "QCSegmentID");

                    DataTable defectInfo = SqlExecuter.Query("SelectDefectByProcess", "10001", defectParam);

                    pfsDetail.DefectDataSource = defectInfo;


                    pfsDetail.ClearCompleteWorkData();


                    Dictionary<string, object> consumableParam = new Dictionary<string, object>();
                    consumableParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    consumableParam.Add("PLANTID", UserInfo.Current.Plant);
                    consumableParam.Add("LOTID", txtLotId.Text);
                    consumableParam.Add("MATERIALTYPE", "Consumable");

                    DataTable consumableList = SqlExecuter.Query("SelectConsumableListByProcessWorkComplete", "10001", consumableParam);

                    pfsDetail.SetConsumableWorkCompleteDataSource(consumableList);


                    Dictionary<string, object> standardRequirementParam = new Dictionary<string, object>();
                    standardRequirementParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    standardRequirementParam.Add("PLANTID", UserInfo.Current.Plant);
                    standardRequirementParam.Add("LOTID", txtLotId.Text);

                    DataTable standardRequirementInfo = SqlExecuter.Query("SelectStandardRequirementByProcess", "10001", standardRequirementParam);

                    pfsDetail.StandardRequirementCompleteDataSource = standardRequirementInfo;


                    //치공구 BOR 등록 
                    Dictionary<string, object> durableParam = new Dictionary<string, object>();
                    durableParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    durableParam.Add("PLANTID", UserInfo.Current.Plant);
                    durableParam.Add("LOTID", txtLotId.Text);

                    DataTable durableList = SqlExecuter.Query("GetDurableLotByProcess", "10001", durableParam);

                    pfsDetail.DurableDefDataSource = durableList;


                    Dictionary<string, object> aoiBBTDefectParam = new Dictionary<string, object>();
                    aoiBBTDefectParam.Add("LOTID", txtLotId.Text);
                    aoiBBTDefectParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                    DataTable aoiBbtDefectInfo = SqlExecuter.Query("SelectAoiBbtDefectInfoByProcess", "10001", aoiBBTDefectParam);

                    pfsDetail.AOIBBTDefectInfoDataSource = aoiBbtDefectInfo;
                    */

                    pfsDetail.SetPostProcessEquipmentWipVisible();

                    Dictionary<string, object> postProcessEquipmentWipParam = new Dictionary<string, object>();
                    postProcessEquipmentWipParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    postProcessEquipmentWipParam.Add("PLANTID", UserInfo.Current.Plant);
                    postProcessEquipmentWipParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    postProcessEquipmentWipParam.Add("LOTID", txtLotId.Text);
                    postProcessEquipmentWipParam.Add("PROCESSSEGMENTID", Format.GetString(grdLotInfo.GetFieldValue("NEXTPROCESSSEGMENTID")));
                    postProcessEquipmentWipParam.Add("PROCESSSEGMENTVERSION", Format.GetString(grdLotInfo.GetFieldValue("NEXTPROCESSSEGMENTVERSION")));
                    postProcessEquipmentWipParam.Add("RESOURCETYPE", "Area");
                    // TODO : Query Version 변경
                    DataTable postProcessEquipmentWipInfo = SqlExecuter.Query("SelectPostProcessEquipmentWipByArea", "10031", postProcessEquipmentWipParam);

                    pfsDetail.PostProcessEquipmentWipDataSource = postProcessEquipmentWipInfo;


                    /*
                    Dictionary<string, object> equipmentParam = new Dictionary<string, object>();
                    equipmentParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    equipmentParam.Add("PLANTID", UserInfo.Current.Plant);
                    equipmentParam.Add("LOTID", txtLotId.Text);
                    equipmentParam.Add("RESOURCETYPE", "EquipmentClass");
                    equipmentParam.Add("EQUIPMENTTYPE", "Production");
                    equipmentParam.Add("DETAILEQUIPMENTTYPE", "Main");

                    DataTable equipmentInfo = SqlExecuter.Query("SelectEquipmentByProcess", "10001", equipmentParam);

                    pfsDetail.EquipmentDataSource = equipmentInfo;
                    */

                    pfsDetail.DisplayTabRequire();

                    //txtLotId.Text = "";
                }
                catch (Exception ex)
                {
                    throw MessageException.Create(ex.ToString());
                }
                finally
                {
                    pnlContent.CloseWaitArea();
                    buttons.ForEach(button => button.IsBusy = false);
                }
            }
        }

        private void loadMessage()
        {
            if (!string.IsNullOrEmpty(this.lotId) && !this.isMessageLoaded)
            {
                this.isMessageLoaded = true;

                var buttons = pnlToolbar.Controls.Find<SmartButton>(true);
                try
                {                
                    buttons.ForEach(button => button.IsBusy = true);
                    pnlContent.ShowWaitArea();

                    Dictionary<string, object> messageParam = new Dictionary<string, object>();
                    messageParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    messageParam.Add("PLANTID", UserInfo.Current.Plant);
                    messageParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    messageParam.Add("LOTID", this.lotId);
                    messageParam.Add("PRODUCTDEFID", grdLotInfo.GetFieldValue("PRODUCTDEFID").ToString());
                    messageParam.Add("PRODUCTDEFVERSION", grdLotInfo.GetFieldValue("PRODUCTDEFVERSION").ToString());
                    messageParam.Add("PROCESSSEGMENTID", grdLotInfo.GetFieldValue("PROCESSSEGMENTID").ToString());
                    messageParam.Add("PROCESSSEGMENTVERSION", grdLotInfo.GetFieldValue("PROCESSSEGMENTVERSION").ToString());

                    DataTable messageInfo = SqlExecuter.Query("SelectLotHistoryMessage", "10001", messageParam);

                    pfsDetail.MessageDataSource = messageInfo;
                }
                catch (Exception ex)
                {
                    throw MessageException.Create(ex.ToString());
                }
                finally
                {
                    pnlContent.CloseWaitArea();
                    buttons.ForEach(button => button.IsBusy = false);
                }
            }
        }

        private void TxtLotId_Click(object sender, EventArgs e)
        {
            txtLotId.Editor.SelectAll();
        }

        private void PfsInfo_EquipmentClassEditValueChanged(object sender, EventArgs e)
        {
            DataView equipmentList = pfsInfo.GetEquipmentDataSource();

            pfsDetail.SetDurableEquipmentDataSource(equipmentList);
        }

        private void PfsDetail_DefectQtyChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            SmartBandedGridView view = sender as SmartBandedGridView;
            
            decimal pcsPnlQty = Format.GetDecimal(grdLotInfo.GetFieldValue("PCSPNL"));
            decimal panelQty = Format.GetDecimal(grdLotInfo.GetFieldValue("PNLQTY"));
            decimal qty = Format.GetDecimal(grdLotInfo.GetFieldValue("PCSQTY"));

            DataTable defectList = pfsDetail.DefectDataSource as DataTable;

            decimal defectQty = 0;

            defectList.Rows.Cast<DataRow>().ForEach(row =>
            {
                defectQty += Format.GetDecimal(row["QTY"]);
            });

            pfsInfo.SetQty(defectQty);
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            if (!grdLotInfo.Enabled)
                throw MessageException.Create("NoSaveData");

            if (ShowMessage(MessageBoxButtons.YesNo, "InfoSave") == DialogResult.No) return;

            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            Dictionary<string, object> workInfo = pfsInfo.GetValues();

            string worker = workInfo["WORKER"].ToString();
            string lotId = grdLotInfo.GetFieldValue("LOTID").ToString();
            string processPathId = grdLotInfo.GetFieldValue("PROCESSPATHID").ToString();
            string processSegmentId = grdLotInfo.GetFieldValue("PROCESSSEGMENTID").ToString();
            string unit = Format.GetString(workInfo["UNIT"]);
            double goodQty = Format.GetDouble(workInfo["GOODQTY"], 0);
            double goodPnlQty = Format.GetDouble(workInfo["GOODPNLQTY"], 0);
            double defectQty = Format.GetDouble(workInfo["DEFECTQTY"], 0);
            double defectPnlQty = Format.GetDouble(workInfo["DEFECTPNLQTY"], 0);
            string resourceId = workInfo.ContainsKey("RESOURCEID") ? workInfo["RESOURCEID"].ToString() : "";
            string transitArea = workInfo.ContainsKey("TRANSITAREA") ? workInfo["TRANSITAREA"].ToString() : "";
            string comment = workInfo["COMMENT"].ToString();
            DataTable defectInfo = pfsDetail.DefectDataSource as DataTable;
            DataTable aoiDefectInfo = pfsDetail.AOIDefectDataSource as DataTable;


            MessageWorker messageWorker = new MessageWorker("SaveSendLot");
            messageWorker.SetBody(new MessageBody()
            {
                { "EnterpriseId", UserInfo.Current.Enterprise },
                { "PlantId", UserInfo.Current.Plant },
                { "Worker", worker },
                { "LotId", lotId },
                { "ProcessPathId", processPathId },
                { "ProcessSegmentId", processSegmentId },
                { "DefectUnit", unit },
                { "GoodQty", goodQty },
                { "GoodPnlQty", goodPnlQty },
                { "DefectQty", defectQty },
                { "DefectPnlQty", defectPnlQty },
                { "ResourceId", resourceId },
                { "TransitArea", transitArea },
                { "Comment", comment },
                { "DefectList", defectInfo },
                { "ProcessSegmentType", _processSegmentType },
                { "IsInspectionProcess", _isInspectionProcess },
                { "AOIDefectList", aoiDefectInfo }
            });

            messageWorker.Execute();

            ShowMessage("SuccedSave");

            ClearDetailInfo();

            txtLotId.Text = "";
            txtLotId.Focus();
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
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
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

            // TODO : 유효성 로직 변경
            Dictionary<string, object> values = pfsInfo.GetValues();

            if (values.ContainsKey("WORKER") && string.IsNullOrEmpty(values["WORKER"].ToString()))
            {
                // 작업자는 필수 입력 항목입니다.
                throw MessageException.Create("WorkerIsRequired");
            }

            if (values.ContainsKey("GOODQTY") && Format.GetDecimal(values["GOODQTY"]) < 0)
            {
                // 양품수량은 0 보다 커야 합니다.
                throw MessageException.Create("GoodQtyLargerThanZero");
            }

            if (values.ContainsKey("TRANSITAREA") && string.IsNullOrEmpty(values["TRANSITAREA"].ToString()))
            {
                // 인계 작업장을 선택하시기 바랍니다.
                throw MessageException.Create("SelectTransitArea");
            }

            if (values.ContainsKey("RESOURCEID") && string.IsNullOrEmpty(values["RESOURCEID"].ToString()))
            {
                // 인계 자원을 선택하시기 바랍니다.
                throw MessageException.Create("SelectTransitResource");
            }
            
            string unit = values["UNIT"].ToString();
            decimal panelPerQty = Format.GetDecimal(grdLotInfo.GetFieldValue("PANELPERQTY"));
            decimal goodQty = Format.GetDecimal(values["GOODQTY"]);

            if (unit == "PNL" && panelPerQty > 0 && goodQty % panelPerQty != 0)
            {
                // 단위가 PCS가 아닌 경우 PNL 수량은 정수로 나와야 합니다.
                throw MessageException.Create("PanelQtyHasNotInteger");
            }

            pfsDetail.ValidateDefectGrid();

            //DataTable list = pfsDetail.DefectDataSource as DataTable;
            //foreach (DataRow row in list.Rows)
            //{
            //    string defectCode = Format.GetString(row["DEFECTCODE"]);
            //    decimal pnlQty = Format.GetDecimal(row["PNLQTY"]);
            //    decimal qty = Format.GetDecimal(row["QTY"]);

            //    if (string.IsNullOrEmpty(defectCode))
            //    {
            //        // 불량코드를 선택하여 주십시오.
            //        throw MessageException.Create("NoDefectCode");
            //    }

            //    if (pnlQty <= 0 || qty <= 0)
            //    {
            //        // 불량수량을 입력해야 합니다.
            //        throw MessageException.Create("DefectQtyRequired");
            //    }
            //}
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        private void ClearDetailInfo()
        {
            lblInnerRevisionText.Text = "";
            lblIsRework.Visible = false;
            lblIsRCLot.Visible = false;

            grdLotInfo.ClearData();
            pfsInfo.ClearData();
            pfsDetail.ClearData();

            pfsDetail.ClearTabPageHeaderColor();

            grdLotInfo.Enabled = false;
            //pfsInfo.Enabled = false;
            pfsInfo.SetReadOnly(true);
            pfsInfo.UnitComboReadOnly = true;
            //pfsDetail.Enabled = false;
            pfsDetail.SetReadOnly(true);

            pfsDetail.SetLotInfo("", 0, 0, 0);
        }

        #endregion
    }
}
