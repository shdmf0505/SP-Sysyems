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
    /// 프 로 그 램 명  : 공정 관리 > 공정작업 > 작업 완료
    /// 업  무  설  명  : 완료 대기중인 Lot을 작업 완료 처리한다.
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2019-08-02
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class WorkCompletion : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        private string _processSegmentType = "";

        private bool _isInspectionProcess = false;
        private bool _isRepairProcess = false;

        private bool isMessageLoaded = false;
        private string lotId = null;
        #endregion

        #region 생성자

        public WorkCompletion()
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
            // TODO : 컨트롤 초기화 로직 구성
            UseAutoWaitArea = false;
            InitializeControls();
            InitializeEvent();
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
          
            pfsInfo.SetControlsVisible();
            //pfsInfo.Enabled = false;
            pfsInfo.SetReadOnly(true);
            pfsInfo.UnitComboReadOnly = true;
            pfsDetail.ProcessType = ProcessType.WorkCompletion;
            pfsDetail.InitializeTabPageVisible();
            //pfsDetail.Enabled = false;
            pfsDetail.SetReadOnly(true);
            pfsDetail.SetEquipmentWorkTimeColumnHidden();

            ConditionItemSelectPopup areaCondition = new ConditionItemSelectPopup();
            areaCondition.Id = "AREA";
            areaCondition.SearchQuery = new SqlQuery("GetAreaListByAuthority", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "AREATYPE=Area");
            areaCondition.ValueFieldName = "AREAID";
            areaCondition.DisplayFieldName = "AREANAME";
            areaCondition.SetPopupLayout("SELECTAREA", PopupButtonStyles.Ok_Cancel, true, false);
            areaCondition.SetPopupResultCount(1);
            areaCondition.SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow);
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
            pfsDetail.DefectMapLinkClick += PfsDetail_DefectMapLinkClick;
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
                //작업장이나, 자원이 없을 경우
                if (string.IsNullOrWhiteSpace(Format.GetFullTrimString(dt.Rows[0]["AREAID"])) || string.IsNullOrWhiteSpace(Format.GetFullTrimString(dt.Rows[0]["RESOURCEID"])))
                {
                    SelectResourcePopup resourcePopup = new SelectResourcePopup(Format.GetString(dt.Rows[0]["LOTID"]), Format.GetString(dt.Rows[0]["PROCESSSEGMENTID"]), string.Empty);
                    resourcePopup.ShowDialog();

                    if (resourcePopup.DialogResult == DialogResult.OK)
                    {
                        MessageWorker resourceWorker = new MessageWorker("SaveLotResourceId");
                        resourceWorker.SetBody(new MessageBody()
                            {
                                { "LotId", txtLotId.Text },
                                { "ResourceId", resourcePopup.ResourceId }
                            });

                        resourceWorker.Execute();

                        TxtLotId_KeyDown(sender, e);
                    }
                    else
                    {
                        // 현재 공정에서 사용할 자원을 선택하시기 바랍니다.
                        throw MessageException.Create("SelectResourceForCurrentProcess");
                    }
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
                    param.Add("PROCESSSTATE", "Run");
                    param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                    DataTable processDefTypeInfo = SqlExecuter.Query("GetProcessDefTypeByProcess", "10001", param);

                    string processDefType = "";
                    string lastRework = "";
                    string processDefId = "";
                    string processDefVersion = "";

                    if (processDefTypeInfo.Rows.Count > 0)
                    {
                        DataRow row = processDefTypeInfo.AsEnumerable().FirstOrDefault();

                        processDefType = Format.GetString(row["PROCESSDEFTYPE"]);
                        processDefId = Format.GetString(row["PROCESSDEFID"]);
                        processDefVersion = Format.GetString(row["PROCESSDEFVERSION"]);
                        lastRework = Format.GetString(row["LASTREWORK"]);
                    }

                    string queryVersion = "10001";

                    if (processDefType == "Rework")
                        queryVersion = "10011";

                    // TODO : Query Version 변경
                    DataTable lotInfo = SqlExecuter.Query("SelectLotInfoByProcess", processDefType == "Rework" ? "10032" : "10031", param);

                    if (lotInfo.Rows.Count < 1)
                    {
                        // 조회할 데이터가 없습니다.
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

                    /*
                    // Resource Id 체크
                    string resourceId = Format.GetString(lotInfo.Rows[0]["RESOURCEID"]);

                    if (string.IsNullOrWhiteSpace(resourceId))
                    {
                        SelectResourcePopup resourcePopup = new SelectResourcePopup(Format.GetString(lotInfo.Rows[0]["LOTID"]), Format.GetString(lotInfo.Rows[0]["PROCESSSEGMENTID"]), Format.GetString(txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"]));
                        resourcePopup.ShowDialog();

                        if (resourcePopup.DialogResult == DialogResult.OK)
                        {
                            MessageWorker resourceWorker = new MessageWorker("SaveLotResourceId");
                            resourceWorker.SetBody(new MessageBody()
                            {
                                { "LotId", txtLotId.Text },
                                { "ResourceId", resourcePopup.ResourceId }
                            });

                            resourceWorker.Execute();

                            TxtLotId_KeyDown(sender, e);
                        }
                        else
                        {
                            // 현재 공정에서 사용할 자원을 선택하시기 바랍니다.
                            throw MessageException.Create("SelectResourceForCurrentProcess");
                        }
                    }
                    */

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
                    //InTransit 상태 체크
                    if (Format.GetFullTrimString(lotInfo.Rows[0]["LOTSTATE"]).Equals("InTransit"))
                    {
                        // 물류창고 입/출고 처리 대상입니다
                        ShowMessage("CheckOSLogisticStatus");
                        return;
                    }
                    //if (isLocking == "Y")
                    //{
                    //    // Locking 상태의 Lot 입니다.
                    //    ShowMessage("LotIsLocking", string.Format("LotId = {0}", txtLotId.Text));

                    //    ClearDetailInfo();

                    //    txtLotId.Text = "";
                    //    txtLotId.Focus();

                    //    return;
                    //}

                    //InTransit 상태 체크
                    if (Format.GetFullTrimString(lotInfo.Rows[0]["LOTSTATE"]).Equals("InTransit"))
                    {
                        // 물류창고 입/출고 처리 대상입니다
                        ShowMessage("CheckOSLogisticStatus");
                        return;
                    }
                    else if (Format.GetFullTrimString(lotInfo.Rows[0]["LOTSTATE"]).Equals("OverSeaInTransit"))
                    {
                        // 해외 물류 이동중입니다.
                        ShowMessage("CheckOverSeasLogistic");
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

                    // AOI Repair 공정인 경우 (인터플렉스 : 회로재생, 영풍전자 : VRS 검사) AOI 불량 탭 보여줌
                    if (UserInfo.Current.Enterprise == Constants.EnterPrise_InterFlex)
                    {
                        string processSegmentId = Format.GetString(grdLotInfo.GetFieldValue("PROCESSSEGMENTID"));

                        if (processDefType == "Rework")
                        {
                            Dictionary<string, object> checkParam = new Dictionary<string, object>();
                            checkParam.Add("LOTID", txtLotId.Text);
                            checkParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                            checkParam.Add("ISREWORK", "Y");

                            DataTable checkList = new DataTable();

                            if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                                checkList = SqlExecuter.Query("SelectDefectMapDataByWorkComplete_YP", "10001", checkParam);
                            else
                                checkList = SqlExecuter.Query("SelectDefectMapDataByWorkComplete", "10001", checkParam);

                            if (checkList.Rows.Count > 0)
                            {
                                _isRepairProcess = true;

                                pfsDetail.SetAOIDefectPageVisible(true);
                            }
                        }
                    }
                    else if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                    {
                        if (_processSegmentType == "VRS")
                        {
                            _isRepairProcess = true;

                            pfsDetail.SetAOIDefectPageVisible(true);
                        }
                    }

                    if (!_isRepairProcess)
                    {
                        // AOI/BBT/HOLE 검사 공정인 경우 AOI/BBT/HOLE 불량 입력 탭 보여줌
                        if (_processSegmentType == "AOIInspection")
                        {
                            _isInspectionProcess = true;

                            pfsDetail.SetAOIDefectPageVisible();
                        }
                        else if (_processSegmentType == "BBTInspection")
                        {
                            _isInspectionProcess = true;

                            pfsDetail.SetBBTHOLEDefectPageVisible("BBT");
                        }
                        else if (_processSegmentType == "AOIHoleInspection")
                        {
                            _isInspectionProcess = true;

                            pfsDetail.SetBBTHOLEDefectPageVisible("HOLE");
                        }
                    }
                    //2021.03.04 UOM정보 셋팅
                    pfsInfo.setBasicSetUOM();
                    pfsDetail.setQtyColText();

                    Dictionary<string, object> lotWorkerParam = new Dictionary<string, object>();
                    lotWorkerParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    lotWorkerParam.Add("PLANTID", UserInfo.Current.Plant);
                    lotWorkerParam.Add("LOTID", txtLotId.Text);

                    DataTable lotWorkerInfo = SqlExecuter.Query("GetLotWorkerByTrackOut", "10001", lotWorkerParam);

                    if (lotWorkerInfo.Rows.Count > 0)
                    {
                        string workerId = string.Join(",", lotWorkerInfo.Rows.Cast<DataRow>().Select(row => Format.GetString(row["WORKERID"])));
                        string workerName = string.Join(",", lotWorkerInfo.Rows.Cast<DataRow>().Select(row => Format.GetString(row["WORKERNAME"])));

                        pfsInfo.SetWorker(workerId, workerName);
                    }

                    /*
                    DataTable equipmentList = pfsInfo.GetEquipmentList();
                    equipmentList.Columns.Add("RECIPEID", typeof(string));

                    List<string> lotEquipmentList = pfsInfo.GetLotEquipmentList();

                    pfsDetail.SetEquipmentDataSource(equipmentList, lotEquipmentList);
                    */

                    Dictionary<string, object> equipmentParam = new Dictionary<string, object>();
                    equipmentParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    equipmentParam.Add("PLANTID", UserInfo.Current.Plant);
                    equipmentParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    equipmentParam.Add("LOTID", txtLotId.Text);
                    equipmentParam.Add("EQUIPMENTTYPE", "Production");
                    equipmentParam.Add("DETAILEQUIPMENTTYPE", "Main");
                    // TODO : Query Version 변경
                    DataTable equipmentList = SqlExecuter.Query("GetEquipmentByArea", "10031", equipmentParam);
                    DataTable lotEquipmentList = SqlExecuter.Query("GetLotEquipmentByArea", "10031", equipmentParam);

                    if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong && lotEquipmentList.Rows.Count == 1)
                    {
                        decimal pcsQty = Format.GetInteger(grdLotInfo.GetFieldValue("PCSQTY"));
                        decimal panelPerQty = Format.GetInteger(grdLotInfo.GetFieldValue("PANELPERQTY"));
                        decimal pnlQty = 0;
                        if (panelPerQty > 0)
                            pnlQty = Math.Ceiling(pcsQty / panelPerQty);

                        DataRow equipmentRow = lotEquipmentList.AsEnumerable().First();
                        equipmentRow["PCSQTY"] = pcsQty;
                        equipmentRow["PNLQTY"] = pnlQty;

                        lotEquipmentList.AcceptChanges();
                    }

                    List<string> lstLotEquipment = lotEquipmentList.Rows.Cast<DataRow>().Select(row => Format.GetString(row["EQUIPMENTID"])).ToList();

                    pfsDetail.SetEquipmentDataSource(lotEquipmentList);


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
                    */

                    Dictionary<string, object> consumableParam = new Dictionary<string, object>();
                    consumableParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    consumableParam.Add("PLANTID", UserInfo.Current.Plant);
                    consumableParam.Add("LOTID", txtLotId.Text);
                    consumableParam.Add("MATERIALTYPE", "Consumable");


                    DataTable consumableList = SqlExecuter.Query("SelectConsumableListByProcessWorkComplete", queryVersion, consumableParam);

                    pfsDetail.SetConsumableWorkCompleteDataSource(consumableList);


                    Dictionary<string, object> standardRequirementParam = new Dictionary<string, object>();
                    standardRequirementParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    standardRequirementParam.Add("PLANTID", UserInfo.Current.Plant);
                    standardRequirementParam.Add("LOTID", txtLotId.Text);

                    DataTable standardRequirementInfo = new DataTable();

                    if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                    {
                        if (Format.GetString(grdLotInfo.GetFieldValue("DESCRIPTION")) == "MIG")
                            standardRequirementInfo = SqlExecuter.Query("SelectStandardRequirementByProcess_MIG", "10001", standardRequirementParam);
                        else
                            standardRequirementInfo = SqlExecuter.Query("SelectStandardRequirementByProcess_YP", "10001", standardRequirementParam);
                    }
                    else
                        standardRequirementInfo = SqlExecuter.Query("SelectStandardRequirementByProcess", queryVersion, standardRequirementParam);

                    pfsDetail.StandardRequirementCompleteDataSource = standardRequirementInfo;


                    //치공구 BOR 등록 
                    Dictionary<string, object> durableParam = new Dictionary<string, object>();
                    durableParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    durableParam.Add("PLANTID", UserInfo.Current.Plant);
                    durableParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    durableParam.Add("LOTID", txtLotId.Text);
                    durableParam.Add("MATERIALTYPE", "Durable");

                    DataTable durableList = SqlExecuter.Query("GetDurableDEFByBOR", queryVersion, durableParam);

                    pfsDetail.DurableDefDataSource = durableList;


                    DataTable durableLotList = SqlExecuter.Query("SelectDurableListByProcessWorkComplete", "10002", durableParam);

                    pfsDetail.SetDurableWorkCompleteDataSource(durableLotList);

                    /*
                    Dictionary<string, object> aoiBBTDefectParam = new Dictionary<string, object>();
                    aoiBBTDefectParam.Add("LOTID", txtLotId.Text);
                    aoiBBTDefectParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                    DataTable aoiBbtDefectInfo = SqlExecuter.Query("SelectAoiBbtDefectInfoByProcess", "10001", aoiBBTDefectParam);

                    pfsDetail.AOIBBTDefectInfoDataSource = aoiBbtDefectInfo;
                    */

                    // 설비별 Recipe 조회
                    //string equipmentId = string.Join(",", equipmentList.Rows.Cast<DataRow>().Select(row => Format.GetString(row["EQUIPMENTID"])));
                    string equipmentId = string.Join(",", lotEquipmentList.Rows.Cast<DataRow>().Select(row => Format.GetString(row["EQUIPMENTID"])));
                    string recipeNameFieldName = "";
                    string parameterNameFieldName = "";

                    switch (UserInfo.Current.LanguageType)
                    {
                        case "ko-KR":
                            recipeNameFieldName = "KRECIPENAME";
                            parameterNameFieldName = "KPARAMETERNAME";
                            break;
                        case "en-US":
                            recipeNameFieldName = "ERECIPENAME";
                            parameterNameFieldName = "EPARAMETERNAME";
                            break;
                        case "zh-CN":
                            recipeNameFieldName = "CRECIPENAME";
                            parameterNameFieldName = "CPARAMETERNAME";
                            break;
                        case "vi-VN":
                            recipeNameFieldName = "VRECIPENAME";
                            parameterNameFieldName = "VPARAMETERNAME";
                            break;
                    }

                    string selectedRecipeList = "";

                    lotEquipmentList.AsEnumerable().ForEach(r =>
                    {
                        string recipeId = Format.GetString(r["RECIPEID"]);
                        string recipeVersion = Format.GetString(r["RECIPEVERSION"]);

                        if (!string.IsNullOrEmpty(recipeId) && !string.IsNullOrEmpty(recipeVersion))
                        {
                            string recipe = string.Join("|", recipeId, recipeVersion);

                            selectedRecipeList += recipe + ",";
                        }
                    });

                    selectedRecipeList = selectedRecipeList.TrimEnd(',');

                    Dictionary<string, object> recipeParam = new Dictionary<string, object>();
                    recipeParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    recipeParam.Add("RECIPENAME", recipeNameFieldName);
                    recipeParam.Add("PARAMETERNAME", parameterNameFieldName);
                    recipeParam.Add("PRODUCTID", grdLotInfo.GetFieldValue("PRODUCTDEFID"));
                    recipeParam.Add("PRODUCTVERSION", grdLotInfo.GetFieldValue("PRODUCTDEFVERSION"));
                    recipeParam.Add("PROCESSDEFID", processDefId);
                    recipeParam.Add("PROCESSDEFVERSION", processDefVersion);
                    recipeParam.Add("SEGMENTID", grdLotInfo.GetFieldValue("PROCESSSEGMENTID"));
                    recipeParam.Add("RESOURCEID", grdLotInfo.GetFieldValue("RESOURCEID"));
                    recipeParam.Add("LOTID", txtLotId.Text);
                    recipeParam.Add("EQUIPMENTID", equipmentId);
                    recipeParam.Add("RECIPEIDVERSION", selectedRecipeList);

                    DataTable equipmentRecipeInfo = SqlExecuter.Query("SelectEquipmentRecipe", queryVersion, recipeParam);

                    pfsDetail.EquipmentRecipeDataSource = equipmentRecipeInfo;

                    pfsDetail.SetRecipeComboDataSource(null);


                    string stepType = grdLotInfo.GetFieldValue("STEPTYPE").ToString();
                    string[] step = stepType.Replace("{", "").Replace("}", "").Split(',');

                    bool isLastStep = true;

                    step.ForEach(value =>
                    {
                        if (value.Equals(Constants.WaitForSend))
                            isLastStep = false;
                    });

                    if (isLastStep)
                    {
                        pfsDetail.SetPostProcessEquipmentWipVisible();

                        Dictionary<string, object> postProcessEquipmentWipParam = new Dictionary<string, object>();
                        postProcessEquipmentWipParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                        postProcessEquipmentWipParam.Add("PLANTID", UserInfo.Current.Plant);
                        postProcessEquipmentWipParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                        postProcessEquipmentWipParam.Add("LOTID", txtLotId.Text);
                        postProcessEquipmentWipParam.Add("PROCESSSEGMENTID", Format.GetString(grdLotInfo.GetFieldValue("NEXTPROCESSSEGMENTID")));
                        postProcessEquipmentWipParam.Add("PROCESSSEGMENTVERSION", Format.GetString(grdLotInfo.GetFieldValue("NEXTPROCESSSEGMENTVERSION")));
                        postProcessEquipmentWipParam.Add("RESOURCETYPE", "Area");

                        DataTable postProcessEquipmentWipInfo = SqlExecuter.Query("SelectPostProcessEquipmentWipByArea", "10031", postProcessEquipmentWipParam);

                        pfsDetail.PostProcessEquipmentWipDataSource = postProcessEquipmentWipInfo;
                    }
                    else
                    {
                        pfsDetail.SetPostProcessEquipmentWipVisible(false);
                        pfsDetail.ClearPostProcessEquipmentWipData();
                    }


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

        private void PfsDetail_DefectMapLinkClick(object sender, EventArgs e)
        {
            try
            {
                DialogManager.ShowWaitDialog();

                string lotId = Format.GetString(grdLotInfo.GetFieldValue("LOTID"));
                string productDefId = Format.GetString(grdLotInfo.GetFieldValue("PRODUCTDEFID"));
                string productDefVersion = Format.GetString(grdLotInfo.GetFieldValue("PRODUCTDEFVERSION"));

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("LOTID", lotId);
                param.Add("PRODUCTDEFID", productDefId);
                param.Add("PRODUCTREVISION", productDefVersion);

                string menuId = "DefectMapByLot";

                OpenMenu(menuId, param);
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                DialogManager.Close();
            }
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
            //string equipmentClassId = workInfo["EQUIPMENTCLASS"].ToString();
            //string equipmentId = workInfo["EQUIPMENT"].ToString();
            double goodQty = Format.GetDouble(workInfo["GOODQTY"], 0);
            double goodPnlQty = Format.GetDouble(workInfo["GOODPNLQTY"], 0);
            double defectQty = Format.GetDouble(workInfo["DEFECTQTY"], 0);
            double defectPnlQty = Format.GetDouble(workInfo["DEFECTPNLQTY"], 0);
            string printWeek = workInfo.ContainsKey("PRINTWEEK") ? workInfo["PRINTWEEK"].ToString() : "";
            string resourceId = workInfo.ContainsKey("RESOURCEID") ? workInfo["RESOURCEID"].ToString() : "";
            string transitArea = workInfo.ContainsKey("TRANSITAREA") ? workInfo["TRANSITAREA"].ToString() : "";
            string comment = workInfo["COMMENT"].ToString();
            DataTable defectInfo = pfsDetail.DefectDataSource as DataTable;
            DataTable consumableInfo = pfsDetail.ConsumableCompleteDataSource as DataTable;
            DataTable durableInfo = pfsDetail.DurableCompleteDataSource as DataTable;
            DataTable aoiDefectInfo = pfsDetail.AOIDefectDataSource as DataTable;
            DataTable bbtHoleDefectInfo = pfsDetail.BBTHOLEDefectDataSource as DataTable;
            DataTable recipeParameterInfo = pfsDetail.EquipmentRecipeDataSource as DataTable;
            //DataTable consumableDefectInfo = pfsDetail.GetConsumableDefectList();

            string equipmentId = string.Join(",", (pfsDetail.EquipmentDataSource as DataTable).Rows.Cast<DataRow>().Select(row => Format.GetString(row["EQUIPMENTID"])));
            DataTable equipmentInfo = pfsDetail.EquipmentDataSource as DataTable;

            // 기준타수 초과 치공구 조회
            MessageWorker checkDurableLimit = new MessageWorker("CheckDurableOverLimit");
            checkDurableLimit.SetBody(new MessageBody()
            {
                { "LOTID", lotId },
                { "TRANSACTIONTYPE", "TrackOut" },
                { "DURABLELOTIDLIST", string.Join(",", durableInfo.AsEnumerable().Select(row => "'" + row.Field<string>("durablelotid") + "'").ToList()) }
            });

            var saveResult = checkDurableLimit.Execute<DataTable>();
            DataTable result = saveResult.GetResultSet();

            if (result.Rows.Count > 0)
            {
                var useOver95p = new List<string>();
                var cleanOver95p = new List<string>();
                var useOver80p = new List<string>();
                var cleanOver80p = new List<string>();

                foreach (DataRow row in result.Rows)
                {
                    if (row["TYPEID"].ToString() == "USELIMITOVER95P")
                    {
                        useOver95p.Add(row["DURABLELOTID"].ToString());
                    }
                    else if(row["TYPEID"].ToString() == "CLEANLIMITOVER95P")
                    {
                        cleanOver95p.Add(row["DURABLELOTID"].ToString());
                    }
                    else if (row["TYPEID"].ToString() == "USELIMITOVER80P")
                    {
                        useOver80p.Add(row["DURABLELOTID"].ToString());
                    }
                    else if (row["TYPEID"].ToString() == "CLEANLIMITOVER80P")
                    {
                        cleanOver80p.Add(row["DURABLELOTID"].ToString());
                    }
                } 

                /*
                if (useOver95p.Count > 0)
                {
                    ReloadDurableLotList();
                    // 치공구의 누적타수가 보증타수를 초과(95%)하여 작업을 진행 할 수 없습니다. {0} 
                    throw MessageException.Create("DurableLotOverUseLimit95P", 
                        string.Format("{0} : {1}", Language.Get("DURABLELOTID"), string.Join(",", useOver95p)));
                }
                if (cleanOver95p.Count > 0)
                {
                    ReloadDurableLotList();
                    // 치공구의 사용타수가 연마기준 타수를 초과(95%)하여 작업을 진행 할 수 없습니다. {0} 
                    throw MessageException.Create("DurableLotOverCleanLimit95P", 
                        string.Format("{0} : {1}", Language.Get("DURABLELOTID"), string.Join(",", cleanOver95p)));
                }
                */
                if (useOver80p.Count > 0 || useOver95p.Count > 0)
                {
                    // 치공구의 누적타수가 보증타수를 초과(80%)합니다. 계속 진행하시겠습니까? {0}
                    var wantProceed = this.ShowMessage(MessageBoxButtons.YesNo, "DurableLotOverUseLimit80P", string.Join(",", useOver80p));
                    if (wantProceed != DialogResult.Yes)
                    {
                        return;
                    }
                }
                if (cleanOver80p.Count > 0 || cleanOver95p.Count > 0)
                {
                    // 치공구의 사용타수가 연마기준 타수를 초과(80%) 합니다. 계속 진행하시겠습니까? {0}
                    var wantProceed = this.ShowMessage(MessageBoxButtons.YesNo, "DurableLotOverCleanLimit80P", string.Join(",", cleanOver80p));
                    if (wantProceed != DialogResult.Yes)
                    {
                        return;
                    }
                }
            }

            bool isReworkPublish = pfsDetail.IsReworkPublishChecked;
            string subProcessDefId = "";
            string subProcessDefVersion = "";
            string reworkResourceId = "";
            string reworkAreaId = "";

            if (pfsDetail.AOIDefectPageVisible && isReworkPublish)
            {
                ReworkRoutingForAoiRepairPopup popup = new ReworkRoutingForAoiRepairPopup();
                popup.LotId = lotId;
                popup.ProcessSegmentId = processSegmentId;
                //popup.TopProcessSegmentId = "70";
                popup.ShowDialog();

                if (popup.DialogResult == DialogResult.OK)
                {
                    DataRow reworkRouting = popup.CurrentDataRow;
                    subProcessDefId = Format.GetString(reworkRouting["REWORKNUMBER"]);
                    subProcessDefVersion = Format.GetString(reworkRouting["REWORKVERSION"]);
                    reworkResourceId = popup.ResourceId;
                    reworkAreaId = popup.AreaId;
                }
                else
                {
                    // 재작업 라우팅을 선택하시기 바랍니다.
                    throw MessageException.Create("SelectReworkRouting");
                }
            }

            bool hasAnalysisTarget = false;

            if (UserInfo.Current.Enterprise == Constants.EnterPrise_InterFlex)
            {
                if (aoiDefectInfo.Select("DEFECTCODEGROUPID = '005' AND ANALYSISQTY > 0").Count() > 0)
                    hasAnalysisTarget = true;
            }
            else if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
            {
                //if (aoiDefectInfo.Select("(DEFECTCODE = '1001' OR DEFECTCODE = '2001') AND ANALYSISQTY > 0").Count() > 0)
                if (aoiDefectInfo.Select("ANALYSISQTY > 0").Count() > 0)
                    hasAnalysisTarget = true;
            }


            if (_isInspectionProcess)
            {
                if (_processSegmentType == "AOIInspection")
                {
                    if (aoiDefectInfo.Rows.Count < 1)
                    {
                        // AOI 불량이 등록되지 않았습니다. AOI 불량이 없는게 확실합니까?
                        if (ShowMessage(MessageBoxButtons.YesNo, DialogResult.No, "NotInputAOIDefect") == DialogResult.No)
                        {
                            pfsDetail.SelectAOIDefectTabPage();

                            // AOI 불량을 등록하시기 바랍니다.
                            throw MessageException.Create("InputAOIDefect");
                        }
                    }
                }
            }


            /* 2020-03-13 박윤신 추가(Reel 자재 입력 팝업)
            * 
            * //////Reel 자재 입력 팝업 추가
            */
            DataTable reelConsumableInfo = null;

            if (grdLotInfo.GetFieldValue("PROCESSSEGMENTCLASSID").ToString().Equals("7512"))
            {
                AddReelConsumableInputPopup reelConsumablePopup = new AddReelConsumableInputPopup();
                reelConsumablePopup.ShowDialog();
                reelConsumableInfo = reelConsumablePopup.GetData;
            }
          

            try
            {
                pnlContent.ShowWaitArea();

                MessageWorker messageWorker = new MessageWorker("SaveTrackOutLot");
                messageWorker.SetBody(new MessageBody()
                {
                    { "EnterpriseId", UserInfo.Current.Enterprise },
                    { "PlantId", UserInfo.Current.Plant },
                    { "Worker", worker },
                    { "LotId", lotId },
                    { "ProcessPathId", processPathId },
                    { "ProcessSegmentId", processSegmentId },
                    { "DefectUnit", unit },
                    //{ "EquipmentClassId", equipmentClassId },
                    { "EquipmentId", equipmentId },
                    { "EquipmentList", equipmentInfo },
                    { "GoodQty", goodQty },
                    { "GoodPnlQty", goodPnlQty },
                    { "DefectQty", defectQty },
                    { "DefectPnlQty", defectPnlQty },
                    { "PrintWeek", printWeek },
                    { "ResourceId", resourceId },
                    { "TransitArea", transitArea },
                    { "Comment", comment },
                    { "DefectList", defectInfo },
                    { "ConsumableList", consumableInfo },
                    { "DurableList", durableInfo },
                    { "ProcessSegmentType", _processSegmentType },
                    { "IsInspectionProcess", _isInspectionProcess },
                    { "IsRepairProcess", _isRepairProcess },
                    { "AOIDefectList", aoiDefectInfo },
                    { "BBTHOLEDefectList", bbtHoleDefectInfo },
                    { "HasAnalysisTarget", hasAnalysisTarget },
                    { "IsReworkPublish", isReworkPublish },
                    { "SubProcessDefId", subProcessDefId },
                    { "SubProcessDefVersion", subProcessDefVersion },
                    { "ReworkResourceId", reworkResourceId },
                    { "ReworkAreaId", reworkAreaId },
                    { "RecipeParameterList", recipeParameterInfo },
                    { "ReelConsumableList", reelConsumableInfo }
                    //{ "AoiBbtDefectList", aoibbtDefectInfo }
                    //{ "ConsumableDefectList", consumableDefectInfo }
                });

                messageWorker.Execute();
            }
            catch (Exception ex)
            {
                throw MessageException.Create(ex.Message);
            }
            finally
            {
                pnlContent.CloseWaitArea();
                ShowMessage("SuccedSave");
            }


            ClearDetailInfo();

            txtLotId.Text = "";
            txtLotId.Focus();


            if (isReworkPublish)
            {
                CommonFunction.PrintLotCard(lotId, LotCardType.Rework);
            }
        }

        private void ReloadDurableLotList()
        {
            // 재작업 여부 확인
            var param = new Dictionary<string, object>();
            param.Add("LOTID", txtLotId.Text);
            param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("AREAID", txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"].ToString());
            param.Add("LOTID", txtLotId.Text);
            param.Add("PROCESSSTATE", "Run");
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            string processDefType = "";

            DataTable processDefTypeInfo = SqlExecuter.Query("GetProcessDefTypeByProcess", "10001", param);
            if (processDefTypeInfo.Rows.Count > 0)
            {
                DataRow row = processDefTypeInfo.AsEnumerable().FirstOrDefault();
                processDefType = Format.GetString(row["PROCESSDEFTYPE"]);
            }

            string queryVersion = "10001";

            if (processDefType == "Rework")
                queryVersion = "10011";

            // 치공구 BOR 등록 
            Dictionary<string, object> durableParam = new Dictionary<string, object>();
            durableParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            durableParam.Add("PLANTID", UserInfo.Current.Plant);
            durableParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            durableParam.Add("LOTID", txtLotId.Text);
            durableParam.Add("MATERIALTYPE", "Durable");

            DataTable durableList = SqlExecuter.Query("GetDurableDEFByBOR", queryVersion, durableParam);

            pfsDetail.DurableDefDataSource = durableList;


            DataTable durableLotList = SqlExecuter.Query("SelectDurableListByProcessWorkComplete", "10002", durableParam);

            pfsDetail.SetDurableWorkCompleteDataSource(durableLotList);

        /*

            // 재작업 여부 확인
            var param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("AREAID", txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"].ToString());
            param.Add("LOTID", txtLotId.Text);
            param.Add("PROCESSSTATE", "Wait");
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable processDefTypeInfo = SqlExecuter.Query("GetProcessDefTypeByProcess", "10001", param);

            string processDefType = "";
            string processDefId = "";
            string processDefVersion = "";

            if (processDefTypeInfo.Rows.Count > 0)
            {
                DataRow row = processDefTypeInfo.AsEnumerable().FirstOrDefault();

                processDefType = Format.GetString(row["PROCESSDEFTYPE"]);
                processDefId = Format.GetString(row["PROCESSDEFID"]);
                processDefVersion = Format.GetString(row["PROCESSDEFVERSION"]);
            }

            string queryVersion = "10001";

            if (processDefType == "Rework")
                queryVersion = "10011";

            //치공구 BOR 등록 
            Dictionary<string, object> durableparam = new Dictionary<string, object>();
            durableparam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            durableparam.Add("PLANTID", UserInfo.Current.Plant);
            durableparam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            durableparam.Add("LOTID", txtLotId.Text);

            string productDefType = Format.GetString(grdLotInfo.GetFieldValue("PRODUCTDEFTYPEID"));
            if (productDefType == "SubAssembly")
                durableparam.Add("DURABLESTATE", "Available,InUse");
            else
                durableparam.Add("DURABLESTATE", "Available");

            DataTable durableList = SqlExecuter.Query("GetDurableDEFByBOR", queryVersion, durableparam);

            pfsDetail.DurableDefDataSource = durableList;

            DataTable durableLotList = SqlExecuter.Query("SelectDurableListByProcessWorkStart", queryVersion, durableparam);

            pfsDetail.SetDurableWorkStartDataSource(durableLotList);

        */
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

            if (values.ContainsKey("PRINTWEEK") && string.IsNullOrEmpty(Format.GetString(values["PRINTWEEK"])))
            {
                // 인쇄주차를 입력하시기 바랍니다.
                throw MessageException.Create("PrintWeekIsRequired");
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

            if ((pfsDetail.EquipmentDataSource as DataTable).Rows.Count < 1)
            {
                // 작업 완료 할 설비를 선택하시기 바랍니다.
                throw MessageException.Create("SelectWorkCompletionEquipment");
            }

            //pfsDetail.GetCheckedEquipmentList().Rows.Cast<DataRow>().ForEach(row =>
            //{
            //    string recipeId = Format.GetString(row["RECIPEID"]);

            //    if (string.IsNullOrEmpty(recipeId))
            //    {
            //        // 설비에 사용할 Recipe가 선택되지 않았습니다. Equipment Id = {0}
            //        throw MessageException.Create("NotSelectRecipe", Format.GetString(row["EQUIPMENTID"]));
            //    }
            //});

            string unit = values["UNIT"].ToString();
            decimal panelPerQty = Format.GetDecimal(grdLotInfo.GetFieldValue("PANELPERQTY"));
            decimal goodQty = Format.GetDecimal(values["GOODQTY"]);

            if (unit == "PNL" && panelPerQty > 0 && goodQty % panelPerQty != 0)
            {
                // 단위가 PCS가 아닌 경우 PNL 수량은 정수로 나와야 합니다.
                throw MessageException.Create("PanelQtyHasNotInteger");
            }

            ///일단 자재가 등록 되었는지 먼저 체크 간단하게 2019.09.24 배선용
            
            DataTable consumableRequirementList = pfsDetail.StandardRequirementCompleteDataSource as DataTable;
            DataTable consumableLotList = pfsDetail.ConsumableCompleteDataSource as DataTable;

            foreach (DataRow row in consumableRequirementList.Rows)
            {
                decimal requirementQty = Format.GetDecimal(row["REQUIREMENTQTY"]);

                if (requirementQty == 0)
                {
                    string consumableId = Format.GetString(row["CONSUMABLEDEFID"]);

                    // 자재 소요량이 0 입니다. Routing의 자재 소요량을 확인하시기 바랍니다. 자재 : {0}
                    throw MessageException.Create("ConsumableRequirementQtyIsZero", consumableId);
                }
            }

            if (consumableRequirementList.Rows.Count > 0 && consumableLotList.Rows.Count == 0)
            {
                // BOM 기준 사용 자재가 모두 등록되지 않았습니다.
                throw MessageException.Create("DoNotInsertMaterialByBOM");

            }
            var query = from consumable in (pfsDetail.ConsumableCompleteDataSource as DataTable).Rows.Cast<DataRow>()
                        group consumable by consumable["KEYCOLUMN"].ToString() into g
                        select new
                        {
                            ConsumableId = g.Key,
                            InputQty = g.Sum(qty => Format.GetDecimal(qty["INPUTQTY"])),
                            RealInputQty = g.Sum(qty => Format.GetDecimal(qty["ORGINPUTQTY"])),
                            DefectQty = g.Sum(qty => Format.GetDecimal(qty["DEFECTQTY"]))
                        };

            Dictionary<string, decimal> consumableList = query.ToDictionary(value => value.ConsumableId, value => value.RealInputQty);

            bool isUsedConsumable = true;
            bool isUseQtyMatch = true;
            bool isRealInputQtyLagerThanRequirementQty = true;
            bool isInputQtyLessThanQty = true;

            string consumableDefId = "";

            consumableRequirementList.Rows.Cast<DataRow>().ForEach(row =>
            {
                if (!consumableList.ContainsKey(row["KEYCOLUMN"].ToString()))
                    isUsedConsumable = false;

                if (consumableList[row["KEYCOLUMN"].ToString()] != Format.GetDecimal(row["REQUIREMENTQTY"]))
                    isUseQtyMatch = false;

                if (consumableList[row["KEYCOLUMN"].ToString()] > Format.GetDecimal(row["REQUIREMENTQTY"]))
                {
                    isRealInputQtyLagerThanRequirementQty = false;
                    consumableDefId = row["KEYCOLUMN"].ToString();
                }
            });

            //DataTable consumableLotList = pfsDetail.ConsumableCompleteDataSource as DataTable; 위로 이동

            consumableLotList.Rows.Cast<DataRow>().ForEach(row =>
            {
                if (Format.GetDecimal(row["INPUTQTY"]) < Format.GetDecimal(row["INPUTQTY"]))
                    isInputQtyLessThanQty = false;
            });

            if (!isUsedConsumable)
            {
                // BOM 기준 사용 자재가 모두 등록되지 않았습니다.
                throw MessageException.Create("DoNotInsertMaterialByBOM");
            }

            if (!isRealInputQtyLagerThanRequirementQty)
            {
                // {0} 자재의 자재 투입량이 BOM 기준 소요량 보다 많습니다. 자재불량을 등록하시기 바랍니다.
                throw MessageException.Create("InputQtyLagerThanRequirementQty", consumableDefId);
            }

            if (!isUseQtyMatch)
            {
                // 사용 자재 수량은 BOM 기준 소요량과 같아야 합니다.
                throw MessageException.Create("UsingMaterialQtyLessThanBomRequirementQty");
            }

            if (!isInputQtyLessThanQty)
            {
                // 투입수량은 자재 수량을 초과할 수 없습니다.
                throw MessageException.Create("CanNotInputQtyLagerThanQty");
            }

            if (!CheckDurable())
            {
                throw MessageException.Create("CheckRequireDurable");
            }

            // 치공구 설비 필수
            DataTable durableLotList = pfsDetail.DurableCompleteDataSource as DataTable;

            durableLotList.Rows.Cast<DataRow>().ForEach(row =>
            {
                if (string.IsNullOrWhiteSpace(Format.GetString(row["EQUIPMENTID"])))
                {
                    // 설비는 필수로 선택해야 합니다. {0}
                    throw MessageException.Create("EquipmentIsRequired", string.Format("Durable Lot Id : {0}", Format.GetString(row["DURABLELOTID"])));
                }
            });

            if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
            {
                DataTable equipmentList = pfsDetail.EquipmentDataSource as DataTable;

                int equipmentPcsQty = equipmentList.AsEnumerable().Sum(row => Format.GetInteger(row["PCSQTY"]));
                int lotPcsQty = Format.GetInteger(grdLotInfo.GetFieldValue("PCSQTY"));

                if (equipmentPcsQty != lotPcsQty)
                {
                    // Lot Pcs 수량과 설비에서 작업되는 Pcs 수량의 합이 일치하지 않습니다.
                    throw MessageException.Create("EquipmentQtyIsNotEqualLotQty");
                }
            }

            // 작업 중인 설비 존재 여부 체크
            if (!CheckExistsUsingEquipment())
            {
                // 현재 Lot을 작업 중인 설비가 존재하지 않습니다.
                throw MessageException.Create("NotExistsUsingEquipment");
            }

            // 작업 중인 설비에서 사용되는 치공구 존재 여부 체크
            if (durableLotList.Rows.Count > 0)
            {
                string checkEquipmentId = CheckExistsUsingDurableOnEquipment();
                if (!string.IsNullOrWhiteSpace(checkEquipmentId))
                {
                    // 현재 Lot을 작업 중인 설비에서 사용되는 치공구가 없습니다. Equipment Id : {0}
                    throw MessageException.Create("NotExistsDurableOnEquipment", checkEquipmentId);
                }
            }

            //pfsDetail.ValidateAOIGrid();

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

            _processSegmentType = "";
            _isInspectionProcess = false;
            _isRepairProcess = false;

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

        private bool CheckDurable()
        {
            DataTable durableInfo = pfsDetail.DurableCompleteDataSource as DataTable;
            DataTable durableBOR = pfsDetail.DurableDefDataSource;

            List<string> list = durableBOR.AsEnumerable().Select(c => Format.GetString(c["DURABLEDEFID"])).ToList();

            int icnt = durableInfo.AsEnumerable().Select(c => Format.GetString(c["DURABLEDEFID"])).Where(c => list.Contains(c)).Distinct().Count();

            if (icnt < list.Count)
            {
                return false;
            }
            return true;
        }

        private bool CheckExistsUsingEquipment()
        {
            DataTable equipmentList = pfsDetail.EquipmentDataSource as DataTable;

            var list = equipmentList.AsEnumerable().Where(row => string.IsNullOrEmpty(Format.GetString(row["TRACKOUTTIME"])));

            if (list.Count() < 1)
                return false;

            return true;
        }

        private string CheckExistsUsingDurableOnEquipment()
        {
            string result = "";

            DataTable equipmentList = pfsDetail.EquipmentDataSource as DataTable;
            DataTable durableList = pfsDetail.DurableCompleteDataSource as DataTable;

            var list = equipmentList.AsEnumerable().Where(row => string.IsNullOrEmpty(Format.GetString(row["TRACKOUTTIME"])));

            list.ForEach(row =>
            {
                string equipmentId = Format.GetString(row["EQUIPMENTID"]);

                var listDurable = durableList.AsEnumerable().Where(r => string.IsNullOrEmpty(Format.GetString(r["WORKENDTIME"])) && Format.GetString(r["EQUIPMENTID"]) == equipmentId);


                if (listDurable.Count() < 1)
                    result += equipmentId + ",";
            });

            if (!string.IsNullOrWhiteSpace(result))
                result.TrimEnd(',');

            return result;
        }

        #endregion
    }
}
