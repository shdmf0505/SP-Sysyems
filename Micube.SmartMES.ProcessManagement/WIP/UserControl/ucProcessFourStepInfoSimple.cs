#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using Micube.SmartMES.Commons;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
    public delegate void delUnitChangedSimple();

    /// <summary>
    /// 프 로 그 램 명  : 공정 4-Step 정보 User Control
    /// 업  무  설  명  : 공정 4-Step 화면에서 사용되는 정보를 보여주는 사용자 컨트롤을 생성한다.
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2019-07-01
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ucProcessFourStepInfoSimple : UserControl
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        private DataTable _lotInfoDataSource;

        #endregion

        #region 생성자

        public event delUnitChangedSimple unitChanged;
        public ucProcessFourStepInfoSimple()
        {
            InitializeComponent();
            
            InitializeEvent();

            if (!pnlProcessFourStepInfo.IsDesignMode())
            {
                InitializeControls();
            }
        }

        #endregion

        #region 컨트롤 초기화

        /// <summary>
        /// 화면의 컨트롤을 초기화한다.
        /// </summary>
        private void InitializeControls()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("UOMCLASSID", "Process");

            // UOM
            cboUnitOfMaterial.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboUnitOfMaterial.Editor.ShowHeader = false;
            cboUnitOfMaterial.Editor.ValueMember = "UOMDEFID";
            cboUnitOfMaterial.Editor.DisplayMember = "UOMDEFNAME";
            cboUnitOfMaterial.Editor.UseEmptyItem = true;
            cboUnitOfMaterial.Editor.EmptyItemValue = "";
            cboUnitOfMaterial.Editor.EmptyItemCaption = "";
            cboUnitOfMaterial.Editor.DataSource = SqlExecuter.Query("GetUomDefinitionList", "10001", param);

            numGoodQty.ReadOnly = true;
            numGoodPnlQty.ReadOnly = true;
            numDefectQty.ReadOnly = true;
            numDefectPnlQty.ReadOnly = true;

            //설비는 하단 탭으로, 설비 그룹은 사용 안함
            //일단 visible false처리

            cboEquipment.Visible = false;
            cboEquipmentClass.Visible = false;

            lblGoodQtyUOM.Font = new Font("Malgun Gothic", 9);
            lblDefectQtyUom.Font = new Font("Malgun Gothic", 9);
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        private void InitializeEvent()
        {
            SizeChanged += UcWriteWorkResult_SizeChanged;

            cboEquipmentClass.Editor.EditValueChanged += CboEquipmentClass_EditValueChanged;

            cboUnitOfMaterial.Editor.EditValueChanged += CboUnitOfMaterial_EditValueChanged;
            txtWorker.Editor.QueryPopupEvent += Editor_QueryPopupEvent;
        }

        private void Editor_QueryPopupEvent(object sender, CancelEventArgs e)
        {
            if (ProcessType.Equals(ProcessType.LotAccept) || ProcessType.Equals(ProcessType.TransitRegist))
                e.Cancel = true;
                
            else
                e.Cancel = false;
        }

        private void UcWriteWorkResult_SizeChanged(object sender, EventArgs e)
        {
            tlpSpectionCommnet.ColumnStyles[0].Width = lblGoodQty.Width;
        }

        private void CboEquipmentClass_EditValueChanged(object sender, EventArgs e)
        {
            if (!cboEquipmentClass.Visible)
                return;

            if (cboEquipment.Editor.DataSource == null)
                return;

            DataView view = (cboEquipment.Editor.DataSource as DataTable).DefaultView;

            if (cboEquipmentClass.EditValue == null || string.IsNullOrEmpty(cboEquipmentClass.EditValue.ToString()))
                view.RowFilter = "";
            else
                view.RowFilter = "EQUIPMENTCLASSID = '" + cboEquipmentClass.EditValue.ToString() + "'";

            EquipmentClassEditValueChanged?.Invoke(cboEquipment.Editor, e);
        }

        private void CboUnitOfMaterial_EditValueChanged(object sender, EventArgs e)
        {
            SmartComboBox combo = sender as SmartComboBox;

            if (combo.EditValue == null)
                return;


            if (unitChanged != null)
            {
                unitChanged();
            }
            /*
            DataRow row = _lotInfoDataSource.Rows[0];
            decimal panelPerQty = Format.GetDecimal(row["PANELPERQTY"]);

            numGoodQty.Value = numGoodPnlQty.Value * panelPerQty;
            numDefectQty.Value = numDefectPnlQty.Value * panelPerQty;
            */
        }

        #endregion

        #region Property

        /// <summary>
        /// User Control이 사용되는 화면 구분
        /// </summary>
        public ProcessType ProcessType { get; set; }


        [Browsable(false)]
        public bool UnitComboReadOnly
        {
            get
            {
                return cboUnitOfMaterial.Editor.ReadOnly;
            }
            set
            {
                cboUnitOfMaterial.Editor.ReadOnly = value;
            }
        }

        #endregion

        #region EventHandler

        public event EventHandler EquipmentClassEditValueChanged;

        #endregion

        #region Public Function

        public void SetControlsVisible()
        {
            switch (ProcessType)
            {
                case ProcessType.LotAccept:
                    cboEquipmentClass.Visible = false;
                    cboEquipment.Visible = false;
                    txtPrintWeek.Visible = false;
                    break;
                case ProcessType.StartWork:
                    txtPrintWeek.Visible = false;
                    break;
                case ProcessType.WorkCompletion:
                    txtPrintWeek.Visible = false;
                    break;
                case ProcessType.TransitRegist:
                    cboEquipmentClass.Visible = false;
                    cboEquipment.Visible = false;
                    txtPrintWeek.Visible = false;
                    break;
            }
        }
        private void setUserInfo()
        {
            //GetUserList 10001
            ConditionItemSelectPopup options = new ConditionItemSelectPopup();
            options.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            options.SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, true);
            options.Id = "WORKER";
            options.SearchQuery = new SqlQuery("GetUserList", "10002", $"PLANTID={UserInfo.Current.Plant}");
            options.IsMultiGrid = false;
            options.DisplayFieldName = "WORKERNAME";
            options.ValueFieldName = "WORKERID";
            options.LanguageKey = "USER";

            options.Conditions.AddTextBox("USERIDNAME");

            options.GridColumns.AddTextBoxColumn("WORKERID", 150);
            options.GridColumns.AddTextBoxColumn("WORKERNAME", 200);

            txtWorker.Editor.SelectPopupCondition = options;

            txtWorker.Editor.SetValue(UserInfo.Current.Id);
            txtWorker.Editor.Text = UserInfo.Current.Name;
        }
        private void setUserInfoByArea(string areaId)
        {
            // 작업자
            ConditionItemSelectPopup workerCondition = new ConditionItemSelectPopup();
            workerCondition.Id = "WORKER";
            
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("USERID", UserInfo.Current.Id);
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("AREAID", areaId);

            workerCondition.SearchQuery = new SqlQuery("GetWorkerByProcess", "10001", param);
            workerCondition.ValueFieldName = "WORKERID";
            workerCondition.DisplayFieldName = "WORKERNAME";
            workerCondition.SetPopupLayout("SELECTWORKER", PopupButtonStyles.Ok_Cancel, true, true);
            workerCondition.SetPopupResultCount(0);
            workerCondition.SetPopupLayoutForm(700, 800, FormBorderStyle.FixedToolWindow);
            workerCondition.SetPopupAutoFillColumns("WORKERTYPE");

            // 팝업에서 사용되는 검색조건 (작업자ID/명)
            workerCondition.Conditions.AddTextBox("TXTWORKERIDNAME");

            // 팝업 그리드에서 보여줄 컬럼 정의
            // 작업자ID
            workerCondition.GridColumns.AddTextBoxColumn("WORKERID", 80);
            // 작업자명
            workerCondition.GridColumns.AddTextBoxColumn("WORKERNAME", 100);
            // 사번
            workerCondition.GridColumns.AddTextBoxColumn("EMPLOYEENO", 90);
            // 부서
            workerCondition.GridColumns.AddTextBoxColumn("DEPARTMENT", 110);
            // 자사구분
            workerCondition.GridColumns.AddComboBoxColumn("OWNTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OwnType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 작업자유형
            workerCondition.GridColumns.AddComboBoxColumn("WORKERTYPE", 120, new SqlQuery("GetCodeList", "00001", "CODECLASSID=WorkerType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));


            txtWorker.Editor.SelectPopupCondition = workerCondition;

            DataRow dr = _lotInfoDataSource.Rows[0];

            //if(!Format.GetFullTrimString(dr["TRACKINUSER"]).Equals(string.Empty))
            //{
            //    txtWorker.Editor.SetValue(Format.GetFullTrimString(dr["TRACKINUSER"]));
            //    txtWorker.Editor.Text = Format.GetFullTrimString(dr["TRACKINUSERNAME"]);
            //}


        }

        public void SetControlsInfo(string areaId, string lotId, DataTable dataSource, string queryVersion = "10001", string lastRework = "N")
        {
            _lotInfoDataSource = dataSource;

            DataRow row = _lotInfoDataSource.Rows[0];


            if (ProcessType.Equals(ProcessType.LotAccept) || ProcessType.Equals(ProcessType.TransitRegist))
            {
                setUserInfo();
                txtWorker.Editor.ReadOnly = true;
                txtWorker.Editor.SearchButtonReadOnly = true;
                txtWorker.Editor.ClearButtonVisible = false;
            }
            else
            {
                setUserInfoByArea(areaId);
                //txtWorker.Editor.ReadOnly = false;
                txtWorker.Editor.SearchButtonReadOnly = false;
                txtWorker.Editor.ClearButtonVisible = true;
            }

            /*
            Dictionary<string, object> equipmentClassParam = new Dictionary<string, object>();
            equipmentClassParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            equipmentClassParam.Add("PLANTID", UserInfo.Current.Plant);
            equipmentClassParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            equipmentClassParam.Add("EQUIPMENTTYPE", "Production");
            equipmentClassParam.Add("DETAILEQUIPMENTTYPE", "Main");
            equipmentClassParam.Add("LOTID", lotId);
            equipmentClassParam.Add("RESOURCETYPE", "EquipmentClass");
            equipmentClassParam.Add("PROCESSTYPE", SystemCommonClass.GetEnumToString(ProcessType));

            Dictionary<string, object> equipmentParam = new Dictionary<string, object>();
            equipmentParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            equipmentParam.Add("PLANTID", UserInfo.Current.Plant);
            equipmentParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            equipmentParam.Add("LOTID", lotId);
            equipmentParam.Add("RESOURCETYPE", "EquipmentClass");
            equipmentParam.Add("EQUIPMENTTYPE", "Production");
            equipmentParam.Add("DETAILEQUIPMENTTYPE", "Main");
            equipmentParam.Add("PROCESSTYPE", SystemCommonClass.GetEnumToString(ProcessType));
            */

            Dictionary<string, object> transitAreaParam = new Dictionary<string, object>();
            transitAreaParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            transitAreaParam.Add("PLANTID", UserInfo.Current.Plant);
            transitAreaParam.Add("LOTID", lotId);
            transitAreaParam.Add("PROCESSSEGMENTID", row["NEXTPROCESSSEGMENTID"].ToString());
            transitAreaParam.Add("PROCESSSEGMENTVERSION", row["NEXTPROCESSSEGMENTVERSION"].ToString());
            //transitAreaParam.Add("RESOURCETYPE", "Area");
            // TODO : Resource
            transitAreaParam.Add("RESOURCETYPE", "Resource");
            transitAreaParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable transitAreaList = new DataTable();
            string primaryAreaId = "";

            Dictionary<string, object> lotParam = new Dictionary<string, object>();
            DataTable lotInfo = new DataTable();

            string plantId = "";
            string pathType = "";
            string productDefType = "";

            switch (ProcessType)
            {
                case ProcessType.LotAccept:
                    break;
                case ProcessType.StartWork:
                    /*
                    // 설비그룹
                    cboEquipmentClass.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
                    cboEquipmentClass.Editor.ShowHeader = false;
                    cboEquipmentClass.Editor.ValueMember = "EQUIPMENTCLASSID";
                    cboEquipmentClass.Editor.DisplayMember = "EQUIPMENTCLASSNAME";
                    cboEquipmentClass.Editor.UseEmptyItem = true;
                    cboEquipmentClass.Editor.EmptyItemValue = "";
                    cboEquipmentClass.Editor.EmptyItemCaption = "";
                    cboEquipmentClass.Editor.DataSource = SqlExecuter.Query("GetEquipmentClassByProcess", queryVersion, equipmentClassParam);
                    cboEquipmentClass.EditValue = cboEquipmentClass.Editor.EmptyItemValue;

                    // 설비
                    cboEquipment.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
                    cboEquipment.Editor.ShowHeader = false;
                    cboEquipment.Editor.ValueMember = "EQUIPMENTID";
                    cboEquipment.Editor.DisplayMember = "EQUIPMENTNAME";
                    cboEquipment.Editor.UseEmptyItem = false;
                    //cboEquipment.Editor.EmptyItemValue = "";
                    //cboEquipment.Editor.EmptyItemCaption = "";
                    cboEquipment.Editor.DataSource = SqlExecuter.Query("GetEquipmentByProcess", queryVersion, equipmentParam);
                    //cboEquipment.EditValue = cboEquipment.Editor.EmptyItemValue;
                    */
                    break;
                case ProcessType.WorkCompletion:
                    string processSegmentType = row["PROCESSSEGMENTTYPE"].ToString();
                    string isWeekMng = Format.GetFullTrimString(row["ISWEEKMNG"]);
                    string stepType = row["STEPTYPE"].ToString();

                    if (isWeekMng.Equals("Y"))
                        txtPrintWeek.Visible = true;
                    else
                        txtPrintWeek.Visible = false;

                    // TODO : Resource
                    // 인계 작업장
                    if (!stepType.Split(',').Contains("WaitForSend"))
                    {
                        
                        lotParam = new Dictionary<string, object>();
                        lotParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                        lotParam.Add("PLANTID", UserInfo.Current.Plant);
                        lotParam.Add("LOTID", lotId);

                        lotInfo = SqlExecuter.Query("GetLotProductTypePathType", "10001", lotParam);

                        if (lotInfo.Rows.Count > 0)
                        {
                            DataRow lotRow = lotInfo.Rows.Cast<DataRow>().FirstOrDefault();

                            plantId = Format.GetString(lotRow["PLANTID"]);
                            pathType = Format.GetString(lotRow["PATHTYPE"]);
                            productDefType = Format.GetString(lotRow["PRODUCTDEFTYPE"]);
                        }

                        if (pathType.EndsWith("End") && productDefType == "SubAssembly")
                        {

                            if (UserInfo.Current.Enterprise == Constants.EnterPrise_InterFlex)
                            {
                                Dictionary<string, object> areaParam = new Dictionary<string, object>();
                                areaParam.Add("PLANTID", plantId);
                                areaParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                                // TODO : Resource
                                //transitAreaList = SqlExecuter.Query("GetAreaList", "10003", areaParam);
                                transitAreaList = SqlExecuter.Query("GetAreaList", "10004", areaParam);

                                primaryAreaId = row["AREAID"].ToString();
                            }
                            else if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                            {
                                Dictionary<string, object> areaParam = new Dictionary<string, object>();
                                areaParam.Add("PLANTID", plantId);
                                areaParam.Add("LOTID", lotId);
                                areaParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                                transitAreaList = SqlExecuter.Query("GetAreaList", "10012", areaParam);

                                for (int i = 0; i < transitAreaList.Rows.Count; i++)
                                {
                                    DataRow areaRow = transitAreaList.Rows[i];

                                    if (areaRow["ISPRIMARYRESOURCE"].ToString() == "Y")
                                    {
                                        // TODO : Resource
                                        //primaryAreaId = areaRow["AREAID"].ToString();
                                        primaryAreaId = areaRow["AREAID"].ToString();
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {

                            // TODO : Resource
                            //transitAreaList = SqlExecuter.Query("GetTransitAreaList", lastRework == "Y" ? "10001" : queryVersion, transitAreaParam);
                            transitAreaList = SqlExecuter.Query("GetTransitAreaList", lastRework == "Y" ? "10032" : "10031", transitAreaParam);

                            primaryAreaId = "";
                            if (string.IsNullOrWhiteSpace(row["NEXTPROCESSSEGMENTID"].ToString()))
                            {
                                // TODO : Resource
                                //primaryAreaId = row["AREAID"].ToString();
                                primaryAreaId = row["RESOURCEID"].ToString();
                            }
                            else
                            {
                                for (int i = 0; i < transitAreaList.Rows.Count; i++)
                                {
                                    DataRow areaRow = transitAreaList.Rows[i];

                                    if (areaRow["ISPRIMARYRESOURCE"].ToString() == "Y")
                                    {
                                        // TODO : Resource
                                        //primaryAreaId = areaRow["AREAID"].ToString();
                                        primaryAreaId = areaRow["RESOURCEID"].ToString();
                                        break;
                                    }
                                }
                            }
                        }

                        break;
                    }
                    else
                    {

                    }

                    /*
                    // 설비그룹
                    cboEquipmentClass.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
                    cboEquipmentClass.Editor.ShowHeader = false;
                    cboEquipmentClass.Editor.ValueMember = "EQUIPMENTCLASSID";
                    cboEquipmentClass.Editor.DisplayMember = "EQUIPMENTCLASSNAME";
                    cboEquipmentClass.Editor.UseEmptyItem = true;
                    cboEquipmentClass.Editor.EmptyItemValue = "";
                    cboEquipmentClass.Editor.EmptyItemCaption = "";
                    cboEquipmentClass.Editor.DataSource = SqlExecuter.Query("GetEquipmentClassByProcess", queryVersion, equipmentClassParam);
                    cboEquipmentClass.EditValue = cboEquipmentClass.Editor.EmptyItemValue;

                    // 설비
                    equipmentParam["PROCESSTYPE"] = SystemCommonClass.GetEnumToString(ProcessType.StartWork);
                    DataTable equipmentList = SqlExecuter.Query("GetEquipmentByProcess", queryVersion, equipmentParam);

                    cboEquipment.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
                    cboEquipment.Editor.ShowHeader = false;
                    cboEquipment.Editor.ValueMember = "EQUIPMENTID";
                    cboEquipment.Editor.DisplayMember = "EQUIPMENTNAME";
                    cboEquipment.Editor.UseEmptyItem = false;
                    //cboEquipment.Editor.EmptyItemValue = "";
                    //cboEquipment.Editor.EmptyItemCaption = "";
                    cboEquipment.Editor.DataSource = equipmentList;
                    //cboEquipment.EditValue = cboEquipment.Editor.EmptyItemValue;

                    equipmentParam["PROCESSTYPE"] = SystemCommonClass.GetEnumToString(ProcessType);
                    DataTable lotEquipmentList = SqlExecuter.Query("GetEquipmentByProcess", queryVersion, equipmentParam);

                    string equipmentId = "";

                    lotEquipmentList.Rows.Cast<DataRow>().ForEach(equipmentRow =>
                    {
                        equipmentId += equipmentRow["EQUIPMENTID"].ToString() + ",";
                    });

                    if (equipmentId.Length > 0)
                        equipmentId = equipmentId.Substring(0, equipmentId.Length - 1);

                    cboEquipment.Editor.EditValue = equipmentId;
                    */
                    break;
                case ProcessType.TransitRegist:
                    lotParam = new Dictionary<string, object>();
                    lotParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    lotParam.Add("PLANTID", UserInfo.Current.Plant);
                    lotParam.Add("LOTID", lotId);

                    lotInfo = SqlExecuter.Query("GetLotProductTypePathType", "10001", lotParam);

                    if (lotInfo.Rows.Count > 0)
                    {
                        DataRow lotRow = lotInfo.Rows.Cast<DataRow>().FirstOrDefault();

                        plantId = Format.GetString(lotRow["PLANTID"]);
                        pathType = Format.GetString(lotRow["PATHTYPE"]);
                        productDefType = Format.GetString(lotRow["PRODUCTDEFTYPE"]);
                    }

                    if (pathType.EndsWith("End") && productDefType == "SubAssembly")
                    {

                        if (UserInfo.Current.Enterprise == Constants.EnterPrise_InterFlex)
                        {
                            Dictionary<string, object> areaParam = new Dictionary<string, object>();
                            areaParam.Add("PLANTID", plantId);
                            areaParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                            // TODO : Resource
                            //transitAreaList = SqlExecuter.Query("GetAreaList", "10003", areaParam);
                            transitAreaList = SqlExecuter.Query("GetAreaList", "10004", areaParam);

                            primaryAreaId = row["AREAID"].ToString();
                        }
                        else if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                        {
                            Dictionary<string, object> areaParam = new Dictionary<string, object>();
                            areaParam.Add("PLANTID", plantId);
                            areaParam.Add("LOTID", lotId);
                            areaParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                            transitAreaList = SqlExecuter.Query("GetAreaList", "10012", areaParam);

                            for (int i = 0; i < transitAreaList.Rows.Count; i++)
                            {
                                DataRow areaRow = transitAreaList.Rows[i];

                                if (areaRow["ISPRIMARYRESOURCE"].ToString() == "Y")
                                {
                                    // TODO : Resource
                                    //primaryAreaId = areaRow["AREAID"].ToString();
                                    primaryAreaId = areaRow["AREAID"].ToString();
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {

                        // TODO : Resource
                        //transitAreaList = SqlExecuter.Query("GetTransitAreaList", lastRework == "Y" ? "10001" : queryVersion, transitAreaParam);
                        transitAreaList = SqlExecuter.Query("GetTransitAreaList", lastRework == "Y" ? "10032" : "10031", transitAreaParam);

                        primaryAreaId = "";
                        if (string.IsNullOrWhiteSpace(row["NEXTPROCESSSEGMENTID"].ToString()))
                        {
                            // TODO : Resource
                            //primaryAreaId = row["AREAID"].ToString();
                            primaryAreaId = row["RESOURCEID"].ToString();
                        }
                        else
                        {
                            for (int i = 0; i < transitAreaList.Rows.Count; i++)
                            {
                                DataRow areaRow = transitAreaList.Rows[i];

                                if (areaRow["ISPRIMARYRESOURCE"].ToString() == "Y")
                                {
                                    // TODO : Resource
                                    //primaryAreaId = areaRow["AREAID"].ToString();
                                    primaryAreaId = areaRow["RESOURCEID"].ToString();
                                    break;
                                }
                            }
                        }
                    }
                    break;
            }

            string defectUnit = Format.GetString(row["DEFECTUNIT"]);

            if (string.IsNullOrWhiteSpace(defectUnit))
            {
                string materialClass = Format.GetString(row["MATERIALCLASS"]);
                
                cboUnitOfMaterial.EditValue = "PNL";

                if (materialClass == "FG")
                {
                    cboUnitOfMaterial.Editor.ReadOnly = false;
                }
                else
                {
                    decimal pcsPnl = Format.GetDecimal(row["PANELPERQTY"]);

                    if (pcsPnl == 0)
                        cboUnitOfMaterial.EditValue = "EA";

                    cboUnitOfMaterial.Editor.ReadOnly = false;
                }

                //string productType = row["PRODUCTTYPE"].ToString();

                //if (productType == "Main" || productType == "SubBase")
                //{
                //    cboUnitOfMaterial.EditValue = "PNL";

                //    cboUnitOfMaterial.Editor.ReadOnly = false;
                //}
                //else
                //{
                //    cboUnitOfMaterial.EditValue = row["UNIT"];

                //    cboUnitOfMaterial.Editor.ReadOnly = true;
                //}
            }
            else
            {
                cboUnitOfMaterial.EditValue = defectUnit;

                cboUnitOfMaterial.Editor.ReadOnly = true;
            }

            string UnitOfMaterial = cboUnitOfMaterial.EditValue.ToString();

            //numGoodQty.Value = Format.GetDecimal(row["PCSQTY"]);
            //numGoodPnlQty.Value = Format.GetDecimal(row["PNLQTY"]);
        }
        public void SetQty(decimal defectQty)
        {
            DataRow row = _lotInfoDataSource.Rows[0];

            string unit = Format.GetString(cboUnitOfMaterial.EditValue);
            decimal CalQty = 0;
            decimal qty = Format.GetDecimal(row["PCSQTY"]);
            decimal dDefectPnlQty = 0;
            decimal dGoodPnlQty = 0;

            if (unit.Equals("BLK"))
            {
                CalQty = Format.GetDecimal(row["PCSPNL"])/  Format.GetDecimal(row["PCSARY"]);
            }
            else
            {
                CalQty = Format.GetDecimal(row["PANELPERQTY"]);
            }

            if (CalQty > 0)
            {
                dGoodPnlQty = Math.Ceiling((qty - defectQty) / CalQty);
                dDefectPnlQty = Math.Ceiling(defectQty / CalQty);
            }
            numDefectQty.Value = defectQty;
            numGoodQty.Value = qty - defectQty;

            numGoodPnlQty.Value = Format.GetInteger(dGoodPnlQty);
            numDefectPnlQty.Value = Format.GetInteger(dDefectPnlQty);
        }

        #region 불량 UOM 로직 변경으로 주석 처리
        /*
        public void SetQty(decimal defectQty)
        {
            DataRow row = _lotInfoDataSource.Rows[0];
                 
            string unit = Format.GetString(cboUnitOfMaterial.EditValue);

            decimal panelQty = Format.GetDecimal(row["PNLQTY"]);
            decimal qty = Format.GetDecimal(row["PCSQTY"]);
            decimal pcsPnl = Format.GetDecimal(row["PANELPERQTY"]);
            decimal dDefectPnlQty = 0;
            decimal dGoodPnlQty = 0;
            if (pcsPnl > 0)
            {
                dGoodPnlQty = Math.Ceiling((qty - defectQty) / pcsPnl);
                dDefectPnlQty = Math.Ceiling(defectQty / pcsPnl);
            }

            //if (unit != "PNL")
            //{
            //    //numGoodPnlQty.Value =Math.Ceiling(panelQty - (defectQty / pcsPnl));
            //    numDefectPnlQty.Value =Format.GetInteger(Math.Ceiling(dDefectPnlQty));
            //    numGoodPnlQty.Value = panelQty - Format.GetInteger(Math.Ceiling(dDefectPnlQty));
                
            //}
            //else
            //{
            //    //numGoodPnlQty.Value = panelQty - (defectQty / pcsPnl);
                
            //    numDefectPnlQty.Value = Math.Round(dDefectPnlQty,2);
            //    numGoodPnlQty.Value = panelQty - Math.Round(dDefectPnlQty,2);
            //}

            numDefectQty.Value = defectQty;
            numGoodQty.Value = qty - defectQty;

            numGoodPnlQty.Value = Format.GetInteger(dGoodPnlQty);
            numDefectPnlQty.Value = Format.GetInteger(dDefectPnlQty);
        }
        */
        #endregion
        public Dictionary<string, object> GetValues()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("WORKER", Format.GetString(txtWorker.Editor.SelectedData.FirstOrDefault() == null ? "" : string.Join(",", txtWorker.Editor.SelectedData.Select(row => Format.GetString(row["WORKERID"])))));
            result.Add("UNIT", Format.GetString(cboUnitOfMaterial.EditValue));
            result.Add("GOODQTY", Format.GetDecimal(numGoodQty.EditValue));
            result.Add("GOODPNLQTY", Format.GetDecimal(numGoodPnlQty.EditValue));
            result.Add("DEFECTQTY", Format.GetDecimal(numDefectQty.EditValue));
            result.Add("DEFECTPNLQTY", Format.GetDecimal(numDefectPnlQty.EditValue));

            //if (cboEquipmentClass.Visible)
            //    result.Add("EQUIPMENTCLASS", cboEquipmentClass.EditValue.ToString());

            //if (cboEquipment.Visible)
            //    result.Add("EQUIPMENT", cboEquipment.EditValue.ToString());


            if (txtPrintWeek.Visible)
                result.Add("PRINTWEEK", txtPrintWeek.Text);

            return result;
        }

        public DataTable GetEquipmentList()
        {
            return cboEquipment.Editor.DataSource as DataTable;
        }

        public List<string> GetLotEquipmentList()
        {
            return cboEquipment.EditValue.ToString().Split(',').ToList();
        }

        public void ClearData()
        {
            ClearControlsEditValue(tlpProcessFourStepInfo.Controls);

            if (ProcessType == ProcessType.WorkCompletion)
            {
                if (txtPrintWeek.Visible)
                    txtPrintWeek.Visible = false;
            }
        }

        public DataView GetEquipmentDataSource()
        {
            return (cboEquipment.Editor.DataSource as DataTable).DefaultView;
        }

        public void SetWorker(object workerId, string workerName)
        {
            txtWorker.Editor.SetValue(workerId);
            txtWorker.Editor.Text = workerName;
        }

        public void SetReadOnly(bool isReadOnly)
        {
            SetReadOnlyControl(isReadOnly, tlpProcessFourStepInfo);
            txtRoutingCommnet.ReadOnly = true;
        }

        public void setBasicSetUOM()
        {
            DataRow row = _lotInfoDataSource.Rows[0];
            string strUOM = Format.GetString(row["PROCESSUOM"]);

            if (!string.IsNullOrEmpty(strUOM))
            {
                cboUnitOfMaterial.EditValue = strUOM;
                cboUnitOfMaterial.Editor.ReadOnly = true;
            }
            else
            {
                cboUnitOfMaterial.EditValue = string.Empty;
                cboUnitOfMaterial.Editor.ReadOnly = false;
            }
            numGoodQty.Value = Format.GetDecimal(row["PCSQTY"]);
            if (strUOM.Equals("BLK"))
            {
                numGoodPnlQty.Value = Format.GetDecimal(row["BLKQTY"]);
                lblGoodQtyUOM.Text = "BLK";
                lblDefectQtyUom.Text = "BLK";

            }
            else
            {
                
                numGoodPnlQty.Value = Format.GetDecimal(row["PNLQTY"]);
                lblGoodQtyUOM.Text = "PNL";
                lblDefectQtyUom.Text = "PNL";
            }
        }

        public void SetRoutingComment(string comment)
        {
            txtRoutingCommnet.Text = comment;
        }
        public void SetUnitOfMaterialControlReadOnly()
        {
            DataRow row = _lotInfoDataSource.Rows[0];

            string defectUnit = Format.GetString(row["DEFECTUNIT"]);

            if (string.IsNullOrWhiteSpace(defectUnit))
            {
                string materialClass = Format.GetString(row["MATERIALCLASS"]);

                cboUnitOfMaterial.EditValue = "PNL";

                if (materialClass == "FG")
                {
                    cboUnitOfMaterial.Editor.ReadOnly = false;
                }
                else
                {
                    decimal pcsPnl = Format.GetDecimal(row["PANELPERQTY"]);

                    if (pcsPnl == 0)
                        cboUnitOfMaterial.EditValue = "EA";

                    cboUnitOfMaterial.Editor.ReadOnly = false;
                }

                //string productType = row["PRODUCTTYPE"].ToString();

                //if (productType == "Main" || productType == "SubBase")
                //{
                //    cboUnitOfMaterial.EditValue = "PNL";

                //    cboUnitOfMaterial.Editor.ReadOnly = false;
                //}
                //else
                //{
                //    cboUnitOfMaterial.EditValue = row["UNIT"];

                //    cboUnitOfMaterial.Editor.ReadOnly = true;
                //}
            }
            else
            {
                cboUnitOfMaterial.EditValue = defectUnit;

                cboUnitOfMaterial.Editor.ReadOnly = true;
            }
        }

        #endregion

        #region Private Function

        private void SetControlsEnabled(bool isEnabled, ControlCollection controls)
        {
            if (tlpProcessFourStepInfo.Controls.Count > 0)
            {
                foreach (Control control in controls)
                {
                    if (control is ISmartLabelControl)
                        control.Enabled = isEnabled;

                    SetControlsEnabled(isEnabled, control.Controls);
                }
            }
        }

        private void ClearControlsEditValue(ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is SmartLabelSelectPopupEdit selectPopupEdit)
                    selectPopupEdit.Editor.SetValue(null);
                else if (control is SmartLabelComboBox comboBox)
                    comboBox.EditValue = null;
                else if (control is SmartComboBox combo)
                    combo.EditValue = null;
                /*
                else if (control is SmartComboBox areaComboBox)
                    areaComboBox.EditValue = "";
                    */
            else if (control is SmartLabelCheckedComboBox checkedComboBox)
                    checkedComboBox.EditValue = null;
                else if (control is SmartLabelTextBox textBox)
                    textBox.Text = "";
                else if (control is SmartSpinEdit spinEdit)
                    spinEdit.Value = 0;
                else if (control is SmartTextBox smartTextBox)
                    smartTextBox.Text = string.Empty;//2019.08.01 텍스트박스 추가 배선용
                if (control.Controls.Count > 0)
                    ClearControlsEditValue(control.Controls);
            }
        }

        private void SetReadOnlyControl(bool isReadOnly, Control control)
        {
            if (control.Controls.Count > 0)
            {
                foreach (Control ctrl in control.Controls)
                {
                    if (ctrl is SmartLabelSelectPopupEdit smartLabelSelectPopup)
                    {
                        if (smartLabelSelectPopup.Name == "txtWorker")
                        {
                            if (ProcessType == ProcessType.LotAccept || ProcessType == ProcessType.TransitRegist)
                            {
                                smartLabelSelectPopup.Editor.ReadOnly = true;
                                smartLabelSelectPopup.Editor.SearchButtonReadOnly = true;
                                smartLabelSelectPopup.Editor.ClearButtonVisible = false;
                                continue;
                            }
                        }

                        smartLabelSelectPopup.Editor.ReadOnly = isReadOnly;
                        smartLabelSelectPopup.Editor.SearchButtonReadOnly = isReadOnly;
                        smartLabelSelectPopup.Editor.ClearButtonVisible = !isReadOnly;
                    }
                    else if (ctrl is SmartLabelComboBox smartLabelCombo && ctrl.Name != "cboUnitOfMaterial")
                        smartLabelCombo.Editor.ReadOnly = isReadOnly;
                    else if (ctrl is SmartLabelCheckedComboBox smartLabelCheckedCombo)
                        smartLabelCheckedCombo.Editor.ReadOnly = isReadOnly;
                    else if (ctrl is SmartTextBox smartText)
                        smartText.ReadOnly = isReadOnly;
                    else if (ctrl is SmartComboBox smartCombo)
                        smartCombo.ReadOnly = isReadOnly;
                    else if (ctrl is SmartLabelTextBox smartLabelText)
                        smartLabelText.Editor.ReadOnly = isReadOnly;

                    SetReadOnlyControl(isReadOnly, ctrl);
                }
            }
        }

        #endregion
    }
}