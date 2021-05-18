#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;

using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
using System;
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
    /// 프 로 그 램 명  : 공정 관리 > 공정작업 > 인수 등록
    /// 업  무  설  명  : 입고 검사 완료된 인수 대기 중인 Lot을 인수 등록 한다.
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2019-06-27
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    /// 
    
    public partial class LotAccept : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        private bool isMessageLoaded = false;
        private string lotId = null;
        #endregion

        #region 생성자

        public LotAccept()
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

            UseAutoWaitArea = false;

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
            pfsInfo.ProcessType = ProcessType.LotAccept;
            pfsInfo.SetControlsVisible();
            //pfsInfo.Enabled = false;
            pfsInfo.SetReadOnly(true);
            pfsInfo.UnitComboReadOnly = true;
            pfsDetail.ProcessType = ProcessType.LotAccept;
            pfsDetail.InitializeTabPageVisible();
            //pfsDetail.Enabled = false;
            pfsDetail.SetReadOnly(true);

            ConditionItemSelectPopup areaCondition = new ConditionItemSelectPopup();
            areaCondition.Id = "AREA";
            areaCondition.SearchQuery = new SqlQuery("GetAreaListByAuthority", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", "AREATYPE=Area", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
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
            txtLotId.Editor.Click += Editor_Click;

            pfsDetail.DefectQtyChanged += PfsDetail_DefectQtyChanged;
            pfsInfo.unitChanged += PfsInfo_unitChanged;
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

        private void PfsInfo_unitChanged()
        {
            PfsDetail_DefectQtyChanged(null, null);
        }

        private void Editor_Click(object sender, EventArgs e)
        {
            txtLotId.Editor.SelectAll();
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

                if(dt.Rows.Count < 1)
                {
                    //존재 하지 않는 Lot No. 입니다.
                    ShowMessage("NotExistLotNo");
                    return;
                }
                //작업장이나, 자원이 없을 경우
                if (string.IsNullOrWhiteSpace(Format.GetFullTrimString(dt.Rows[0]["AREAID"])) || string.IsNullOrWhiteSpace(Format.GetFullTrimString(dt.Rows[0]["RESOURCEID"])))
                {
                    SelectResourcePopup resourcePopup = new SelectResourcePopup(Format.GetString(dt.Rows[0]["LOTID"]), Format.GetString(dt.Rows[0]["PROCESSSEGMENTID"]),string.Empty);
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
                    param.Add("PROCESSSTATE", "WaitForReceive");
                    param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                    DataTable processDefTypeInfo = SqlExecuter.Query("GetProcessDefTypeByProcess", "10001", param);

                    string processDefType = "";

                    if (processDefTypeInfo.Rows.Count > 0)
                    {
                        DataRow row = processDefTypeInfo.AsEnumerable().FirstOrDefault();

                        processDefType = Format.GetString(row["PROCESSDEFTYPE"]);
                    }

                    string queryVersion = "10001";

                    if (processDefType == "Rework")
                        queryVersion = "10011";

                    // TODO : Query Version 변경
                    DataTable lotInfo = SqlExecuter.Query("SelectLotInfoByProcess", processDefType == "Rework" ? "10032" : "10031", param);

                    if (lotInfo.Rows.Count < 1)
                    {


                        DataTable AreaLot = SqlExecuter.Query("GetLotAreaAndState", "10001", param);

                        // 조회할 데이터가 없습니다.
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

                    /*
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
                    */

                    //InTransit 상태 체크
                    if(Format.GetFullTrimString(lotInfo.Rows[0]["LOTSTATE"]).Equals("InTransit"))
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

                    if (Format.GetFullTrimString(lotInfo.Rows[0]["OSMCHECK"]).Equals("N"))
                    {
                        ShowMessage("CheckOutSideConfirm");
                        return;
                    }
                        /*
                         //2019.11.22 입고검사체크 lot work result 바라보는걸로 변경
                        Dictionary<string, object> inspectionParam = new Dictionary<string, object>();
                        inspectionParam.Add("LOTID", txtLotId.Text);
                        inspectionParam.Add("INSPECTIONDEFID", "SelfInspectionTake");

                        DataTable inspectionResult = SqlExecuter.Query("GetCheckLotInspection", "10002", inspectionParam);

                        if (inspectionResult.Rows.Count > 0)
                        {
                            DataRow row = inspectionResult.Rows.Cast<DataRow>().FirstOrDefault();

                            string checkInspection = Format.GetString(row["CHECKINSPECTION"]);

                            if (checkInspection == "N")
                            {
                                // 입고검사 결과가 없습니다. Lot No. : {0}
                                ShowMessage("NotExistsInspectionResult", txtLotId.Text);
                                ClearDetailInfo();

                                txtLotId.Text = "";
                                txtLotId.Focus();

                                return;
                            }
                        }
                        */

                        string pathType = Format.GetString(lotInfo.Rows[0]["PATHTYPE"]);

                    if (!pathType.StartsWith("Start") && processDefType != "Rework" && processDefType != "Repeat" && Format.GetFullTrimString(lotInfo.Rows[0]["SELFTAKEINSPRESULT"]).Equals("N"))
                    {
                        // 입고검사 결과가 없습니다. 입고검사 화면으로 이동하시겠습니까? Lot No. : {0}
                        DialogResult drResult = ShowMessage(MessageBoxButtons.YesNo, "NotExistsInspectionResult", txtLotId.Text);
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

                            OpenMenu("PG-SG-0150", menuParam);
                        }

                        return;
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

                    this.lotId = txtLotId.Text;
                    this.isMessageLoaded = false;
                    grdLotInfo.DataSource = lotInfo;
                    pfsDetail.SetLotInfo(txtLotId.Text, Format.GetDecimal(grdLotInfo.GetFieldValue("PANELPERQTY")), Format.GetDecimal(grdLotInfo.GetFieldValue("PNLQTY")), Format.GetDecimal(grdLotInfo.GetFieldValue("PCSQTY")), queryVersion, lotInfo);
                    pfsInfo.SetControlsInfo(txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"].ToString(), txtLotId.Text, lotInfo, queryVersion);

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

                    /*
                    Dictionary<string, object> defectParam = new Dictionary<string, object>();
                    defectParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    defectParam.Add("PLANTID", UserInfo.Current.Plant);
                    defectParam.Add("LOTID", txtLotId.Text);
                    defectParam.Add("PROCESSSEGMENTCLASSTYPE", "MiddleProcessSegmentClass");
                    defectParam.Add("RESOURCETYPE", "QCSegmentID");

                    DataTable defectInfo = SqlExecuter.Query("SelectDefectByProcess", "10001", defectParam);

                    pfsDetail.DefectDataSource = defectInfo;
                    */

                    Dictionary<string, object> equipmentParam = new Dictionary<string, object>();
                    equipmentParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    equipmentParam.Add("PLANTID", UserInfo.Current.Plant);
                    equipmentParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    equipmentParam.Add("LOTID", txtLotId.Text);
                    equipmentParam.Add("EQUIPMENTTYPE", "Production");
                    equipmentParam.Add("DETAILEQUIPMENTTYPE", "Main");
                    // TODO : Query Version 변경
                    DataTable equipmentInfo = SqlExecuter.Query("SelectEquipmentByArea", "10031", equipmentParam);

                    pfsDetail.EquipmentUseStatusDataSource = equipmentInfo;

                    pfsDetail.DisplayTabRequire();

                    //2021.03.04 UOM정보 셋팅
                    pfsInfo.setBasicSetUOM();
                    pfsDetail.setQtyColText();
                    // txtLotId.Text = "";
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

        public void  PfsDetail_DefectQtyChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
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
            string unit = workInfo["UNIT"].ToString();
            double goodQty = Format.GetDouble(workInfo["GOODQTY"], 0);
            double goodPnlQty = Format.GetDouble(workInfo["GOODPNLQTY"], 0);
            double defectQty = Format.GetDouble(workInfo["DEFECTQTY"], 0);
            double defectPnlQty = Format.GetDouble(workInfo["DEFECTPNLQTY"], 0);
            string comment = workInfo["COMMENT"].ToString();
            DataTable defectInfo = pfsDetail.DefectDataSource as DataTable;
            string equipmentId = pfsDetail.GetCheckedEquipmentIdUseStatus();

            try
            {
                pnlContent.ShowWaitArea();

                MessageWorker messageWorker = new MessageWorker("SaveReceiveLot");
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
                    { "Comment", comment },
                    { "DefectList", defectInfo },
                    { "EquipmentId", equipmentId },
                    { "IsBatch", "N" }
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


            string processSegmentType = Format.GetString(grdLotInfo.GetFieldValue("PROCESSSEGMENTTYPE"));

            // Marking Print 공정 인 경우 Marking Lot Id 채번 후 Lot Card 재출력
            if (processSegmentType == "MKPrint" && UserInfo.Current.Enterprise.Equals(Constants.EnterPrise_YoungPoong))
                CommonFunction.PrintLotCard(lotId, LotCardType.Normal);


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
            /*
            if (string.IsNullOrEmpty(pfsDetail.GetCheckedEquipmentId()))
            {
                // 인수 등록 할 설비를 선택하시기 바랍니다.
                throw MessageException.Create("SelectReceiveEquipment");
            }
            */
            string unit = values["UNIT"].ToString();
            //decimal goodPnlQty = Format.GetDecimal(values["GOODPNLQTY"]);

            decimal panelPerQty = Format.GetDecimal(grdLotInfo.GetFieldValue("PANELPERQTY"));
            decimal goodQty = Format.GetDecimal(values["GOODQTY"]);

            //if (unit != "PCS" && goodPnlQty % 1 != 0)
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