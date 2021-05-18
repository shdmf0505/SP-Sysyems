#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
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

namespace Micube.SmartMES.EquipmentManagement.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 설비수리요청접수 등록 팝업
    /// 업  무  설  명  : 수리요청된 설비를 접수하는 팝업
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-09-20
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class RegistRequestMaintWorkOrderPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables
        public DataRow CurrentDataRow { get; set; }
        public delegate void reSearchEvent(string workOrderID);
        public event reSearchEvent SearchHandler;
        string _acceptUserID;
        string _plantID;
        #endregion

        #region 생성자
        public RegistRequestMaintWorkOrderPopup(DataRow orderRow, string plantID):this()
        {
            CurrentDataRow = orderRow;
            _plantID = plantID;
        }
        private RegistRequestMaintWorkOrderPopup()
        {
            InitializeComponent();

            InitializeContent();

            InitializeEvent();
        }
        #endregion

        #region 컨텐츠영역 초기화
        private void InitializeContent()
        {
            InitializeRequiredControl();
            InitializeDownType();
            InitializeDownCode();
            InitializeAcceptResult();
        }

        #region InitializeRequiredControl - 필수값 설정        
        private void InitializeRequiredControl()
        {
            SetRequiredValidationControl(lblAcceptUser, true);
            SetRequiredValidationControl(lblAcceptResult, true);
            SetRequiredValidationControl(lblDownCode, true);
            SetRequiredValidationControl(lblDownType, true);

            SetRequiredValidationControl(lblScheduleTime, false);
            SetRequiredValidationControl(lblRepairUser, false);
            SetRequiredValidationControl(lblRepairUserCount, false);
        }
        #endregion

        #region InitializeDownType - DownType콤보박스 초기화
        /// <summary>
        /// Factory ComboBox를 초기화한다. 검색조건의 Site에 따라 값이 변경되어야 하므로
        /// 초기화 버튼 클릭시 재로딩하는 것으로 한다.
        /// </summary>
        /// <param name="plantID"></param>
        private void InitializeDownType()
        {
            // 검색조건에 정의된 공장을 정리
            cboDownType.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboDownType.ValueMember = "CODEID";
            cboDownType.DisplayMember = "CODENAME";
            cboDownType.EditValue = "1";

            cboDownType.DataSource = SqlExecuter.Query("GetCodeListByToolWithLanguage", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "EquipmentDownType" } });

            cboDownType.ShowHeader = false;
        }
        #endregion

        #region InitializeDownCode - DownCode콤보박스 초기화
        /// <summary>
        /// Factory ComboBox를 초기화한다. 검색조건의 Site에 따라 값이 변경되어야 하므로
        /// 초기화 버튼 클릭시 재로딩하는 것으로 한다.
        /// </summary>
        /// <param name="plantID"></param>
        private void InitializeDownCode()
        {
            // 검색조건에 정의된 공장을 정리
            cboDownCode.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboDownCode.ValueMember = "CODEID";
            cboDownCode.DisplayMember = "CODENAME";
            cboDownCode.EditValue = "1";

            cboDownCode.DataSource = SqlExecuter.Query("GetCodeListByToolWithLanguage", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "EquipmentDownCode" } });

            cboDownCode.ShowHeader = false;
        }
        #endregion

        #region InitializeAcceptResult - AcceptResult콤보박스 초기화
        /// <summary>
        /// Factory ComboBox를 초기화한다. 검색조건의 Site에 따라 값이 변경되어야 하므로
        /// 초기화 버튼 클릭시 재로딩하는 것으로 한다.
        /// </summary>
        /// <param name="plantID"></param>
        private void InitializeAcceptResult()
        {
            // 검색조건에 정의된 공장을 정리
            cboRequestResult.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboRequestResult.ValueMember = "CODEID";
            cboRequestResult.DisplayMember = "CODENAME";
            cboRequestResult.EditValue = "1";

            cboRequestResult.DataSource = SqlExecuter.Query("GetCodeListByToolWithLanguage", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "AcceptResult" } });

            cboRequestResult.ShowHeader = false;
        }
        #endregion
        #endregion

        #region Event
        private void InitializeEvent()
        {
            Shown += RegistRequestMaintWorkOrderPopup_Shown;
            cboRequestResult.EditValueChanged += CboRequestResult_EditValueChanged;
            btnSave.Click += BtnSave_Click;
            btnClose.Click += BtnClose_Click;
        }

        private void CboRequestResult_EditValueChanged(object sender, EventArgs e)
        {
            if (cboRequestResult.EditValue != null)
            {
                if (cboRequestResult.EditValue.ToString().Equals("Repair"))
                {
                    SetRequiredValidationControl(lblScheduleTime, true);
                    SetRequiredValidationControl(lblRepairUser, false);
                    SetRequiredValidationControl(lblRepairUserCount, true);
                    SetRequiredValidationControl(lblAcceptComment, false);

                    ControlEnableProcess("Repair");
                }
                else
                {
                    SetRequiredValidationControl(lblScheduleTime, false);
                    SetRequiredValidationControl(lblRepairUser, false);
                    SetRequiredValidationControl(lblRepairUserCount, false);
                    SetRequiredValidationControl(lblAcceptComment, true);

                    ControlEnableProcess("Close");
                }
            }
            else
            {
                SetRequiredValidationControl(lblScheduleTime, false);
                SetRequiredValidationControl(lblRepairUser, false);
                SetRequiredValidationControl(lblRepairUserCount, false);
                SetRequiredValidationControl(lblAcceptComment, false);

                ControlEnableProcess("Close");
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void RegistRequestMaintWorkOrderPopup_Shown(object sender, EventArgs e)
        {
            InitializeInsertForm();
            LoadData();
        }
        #endregion

        #region 유효성 검사
        
        #region ValidateCurrentStatus : 현재 저장요청한 정보가 저장 가능한 정보인지 검증
        private bool ValidateCurrentStatus()
        {
            return true;
        }
        #endregion

        #region ValidateContent : 저장가능한 정보인지 체크
        private bool ValidateContent(out string messageCode, string saveStatus)
        {
            messageCode = "";

            //if (!ValidateEditValue(txtWorkNo.EditValue))
            //{
            //    messageCode = "ValidateRequiredData";
            //    return false;
            //}           

            if (!ValidateEditValue(cboDownType.EditValue))
            {
                messageCode = "ValidateRequiredData";
                return false;
            }

            if (!ValidateEditValue(cboDownCode.EditValue))
            {
                messageCode = "ValidateRequiredData";
                return false;
            }

            if (!ValidateEditValue(cboRequestResult.EditValue))
            {
                messageCode = "ValidateRequiredData";
                return false;
            }

            if (saveStatus.Equals("Repair"))
            {
                if (!ValidateEditValue(deScheduleTime.EditValue))
                {
                    messageCode = "ValidateRequiredData";
                    return false;
                }

                //if (!ValidateEditValue(txtRepairUser.EditValue))
                //{
                //    messageCode = "ValidateMaintWorkOrderRepairUserAndCount";
                //    return false;
                //}

                //if (!ValidateNumericValue(txtRepairUserCount.EditValue, 1))
                //{
                //    messageCode = "ValidateMaintWorkOrderRepairUserAndCount";
                //    return false;
                //}
            }
            else
            {
                if (!ValidateEditValue(txtAcceptComments.EditValue))
                {
                    messageCode = "ValidateRequiredData";
                    return false;
                }
            }

            return true;
        }
        #endregion

        /// <summary>
        /// 2개의 콤보박스에 데이터가 모두 있어야 하며
        /// 2개의 콤보박스의 데이터가 서로 동일하면 안된다.
        /// </summary>
        /// <param name="originBox"></param>
        /// <param name="targetBox"></param>
        /// <returns></returns>
        private bool ValidateComboBoxEqualValue(SmartComboBox originBox, SmartComboBox targetBox)
        {
            //두 콤보박스의 값이 같으면 안된다.
            if (originBox.EditValue.Equals(targetBox.EditValue))
                return false;
            return true;
        }

        /// <summary>
        /// 숫자를 입력하는 컨트롤을 대상으로 하여 입력받은 기준값보다 값이 같거나 크다면 true 작다면 false를 반환하는 메소드
        /// </summary>
        /// <param name="originBox"></param>
        /// <param name="ruleValue"></param>
        /// <returns></returns>
        private bool ValidateNumericBox(SmartTextBox originBox, int ruleValue)
        {
            //값이 없으면 안된다.
            if (originBox.EditValue == null)
                return false;

            int resultValue = 0;

            //입력받은 기준값(예를 들어 0)보다 작다면 false를 반환
            if (Int32.TryParse(originBox.EditValue.ToString(), out resultValue))
                if (resultValue <= ruleValue)
                    return false;

            //모두 통과했으므로 Validation Check완료
            return true;
        }

        /// <summary>
        /// 숫자를 입력하는 컨트롤을 대상으로 하여 입력받은 기준값보다 값이 같거나 크다면 true 작다면 false를 반환하는 메소드
        /// </summary>
        /// <param name="originBox"></param>
        /// <param name="ruleValue"></param>
        /// <returns></returns>
        private bool ValidateNumericValue(object editValue, int ruleValue)
        {
            //값이 없으면 안된다.
            if (editValue == null)
                return false;

            int resultValue = 0;

            //입력받은 기준값(예를 들어 0)보다 작다면 false를 반환
            if (Int32.TryParse(editValue.ToString(), out resultValue))
                if (resultValue < ruleValue)
                    return false;

            //모두 통과했으므로 Validation Check완료
            return true;
        }

        /// <summary>
        /// 입력받은 editValue에 아무런 값이 입력되지 않았다면 false 입력받았다면 true
        /// </summary>
        /// <param name="editValue"></param>
        /// <returns></returns>
        private bool ValidateEditValue(object editValue)
        {
            if (editValue == null)
                return false;

            if (editValue.ToString() == "")
                return false;

            return true;
        }

        private bool ValidateCellInGrid(DataRow currentRow, string[] columnNames)
        {
            foreach (string columnName in columnNames)
            {
                if (currentRow[columnName] == null)
                    return false;
                if (currentRow[columnName].ToString() == "")
                    return false;
            }

            return true;
        }

        private void SetRequiredValidationControl(Control requiredControl, bool isRequired)
        {
            if (isRequired)
                requiredControl.ForeColor = Color.Red;
            else
                requiredControl.ForeColor = Color.Black;
        }
        #endregion

        #region Private Functions
        #region InitializeInsertForm : 입고등록하기 위한 화면 초기화
        /// <summary>
        /// 입고등록정보를 입력하기 위해 화면을 초기화 한다.
        /// </summary>
        private void InitializeInsertForm()
        {
            try
            {
                txtWorkOrderID.EditValue = "";
                txtRequestUser.EditValue = "";
                txtRequestTime.EditValue = "";
                txtWorkOrderStatus.EditValue = "";
                txtWorkOrderStep.EditValue = "";
                txtEquipmentID.EditValue = "";
                txtEquipmentName.EditValue = "";
                chkIsUse.Checked = false;
                chkIsEmergency.Checked = false;
                txtRequestComment.EditValue = "";
                txtEmergencyContent.EditValue = "";
                txtAcceptUser.EditValue = UserInfo.Current.Name;
                _acceptUserID = UserInfo.Current.Id;
                cboDownCode.EditValue = null;
                cboDownType.EditValue = null;
                cboRequestResult.EditValue = null;
                deScheduleTime.EditValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                txtRepairUser.EditValue = "";
                txtRepairUserCount.EditValue = "";

                //컨트롤 접근여부는 작성상태로 변경한다.
                ControlEnableProcess("added");
            }
            catch (Exception err)
            {
                ShowError(err);
            }
        }
        #endregion

        #region controlEnableProcess : 입력/수정/삭제를 위한 화면내 컨트롤들의 Enable 제어
        /// <summary>
        /// 진행상태값에 따른 입력 항목 lock 처리
        /// </summary>
        /// <param name="blProcess"></param>
        private void ControlEnableProcess(string currentStatus)
        {
            if (currentStatus == "added") //초기화 버튼을 클릭한 경우
            {
                txtWorkOrderID.ReadOnly = true;
                txtRequestUser.ReadOnly = true;
                txtRequestTime.ReadOnly = true;
                txtWorkOrderStatus.ReadOnly = true;
                txtWorkOrderStep.ReadOnly = true;
                txtEquipmentID.ReadOnly = true;
                txtEquipmentName.ReadOnly = true;
                chkIsUse.ReadOnly = true;
                chkIsEmergency.ReadOnly = true;
                txtRequestComment.ReadOnly = true;
                txtEmergencyContent.ReadOnly = true;
                txtAcceptUser.ReadOnly = false;                
                cboDownCode.ReadOnly = false;
                cboDownType.ReadOnly = false;
                cboRequestResult.ReadOnly = false;
                deScheduleTime.ReadOnly = true;
                txtRepairUser.ReadOnly = true;
                txtRepairUserCount.ReadOnly = true;
                txtAcceptComments.ReadOnly = true;
            }
            else if (currentStatus == "modified") //
            {
                txtWorkOrderID.ReadOnly = true;
                txtRequestUser.ReadOnly = true;
                txtRequestTime.ReadOnly = true;
                txtWorkOrderStatus.ReadOnly = true;
                txtWorkOrderStep.ReadOnly = true;
                txtEquipmentID.ReadOnly = true;
                txtEquipmentName.ReadOnly = true;
                chkIsUse.ReadOnly = true;
                chkIsEmergency.ReadOnly = true;
                txtRequestComment.ReadOnly = true;
                txtEmergencyContent.ReadOnly = true;
                txtAcceptUser.ReadOnly = false;
                cboDownCode.ReadOnly = false;
                cboDownType.ReadOnly = false;
                cboRequestResult.ReadOnly = false;
                deScheduleTime.ReadOnly = true;
                txtRepairUser.ReadOnly = true;
                txtRepairUserCount.ReadOnly = true;
                txtAcceptComments.ReadOnly = false;
            }
            else if (currentStatus == "Repair") //
            {
                txtWorkOrderID.ReadOnly = true;
                txtRequestUser.ReadOnly = true;
                txtRequestTime.ReadOnly = true;
                txtWorkOrderStatus.ReadOnly = true;
                txtWorkOrderStep.ReadOnly = true;
                txtEquipmentID.ReadOnly = true;
                txtEquipmentName.ReadOnly = true;
                chkIsUse.ReadOnly = true;
                chkIsEmergency.ReadOnly = true;
                txtRequestComment.ReadOnly = true;
                txtEmergencyContent.ReadOnly = true;
                txtAcceptUser.ReadOnly = false;
                cboDownCode.ReadOnly = false;
                cboDownType.ReadOnly = false;
                cboRequestResult.ReadOnly = false;
                deScheduleTime.ReadOnly = false;
                txtRepairUser.ReadOnly = false;
                txtRepairUserCount.ReadOnly = false;
                txtAcceptComments.ReadOnly = true;
            }
            else if (currentStatus == "Close") //
            {
                txtWorkOrderID.ReadOnly = true;
                txtRequestUser.ReadOnly = true;
                txtRequestTime.ReadOnly = true;
                txtWorkOrderStatus.ReadOnly = true;
                txtWorkOrderStep.ReadOnly = true;
                txtEquipmentID.ReadOnly = true;
                txtEquipmentName.ReadOnly = true;
                chkIsUse.ReadOnly = true;
                chkIsEmergency.ReadOnly = true;
                txtRequestComment.ReadOnly = true;
                txtEmergencyContent.ReadOnly = true;
                txtAcceptUser.ReadOnly = false;
                cboDownCode.ReadOnly = false;
                cboDownType.ReadOnly = false;
                cboRequestResult.ReadOnly = false;
                deScheduleTime.ReadOnly = true;
                txtRepairUser.ReadOnly = true;
                txtRepairUserCount.ReadOnly = true;
                txtAcceptComments.ReadOnly = false;
            }
            else
            {
                txtWorkOrderID.ReadOnly = true;
                txtRequestUser.ReadOnly = true;
                txtRequestTime.ReadOnly = true;
                txtWorkOrderStatus.ReadOnly = true;
                txtWorkOrderStep.ReadOnly = true;
                txtEquipmentID.ReadOnly = true;
                txtEquipmentName.ReadOnly = true;
                chkIsUse.ReadOnly = true;
                chkIsEmergency.ReadOnly = true;
                txtRequestComment.ReadOnly = true;
                txtEmergencyContent.ReadOnly = true;
                txtAcceptUser.ReadOnly = false;
                cboDownCode.ReadOnly = false;
                cboDownType.ReadOnly = false;
                cboRequestResult.ReadOnly = false;
                deScheduleTime.ReadOnly = false;
                txtRepairUser.ReadOnly = false;
                txtRepairUserCount.ReadOnly = false;
                txtAcceptComments.ReadOnly = true;
            }
        }
        #endregion

        #region CreateSaveDatatable : ToolRequest 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// <summary>
        /// ToolRequest 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// 입력시에는 ToolRequest와 ToolRequestDetail에 입력하고
        /// 수정시에는 ToolRequest와 ToolRequestDetail 및 ToolRequestDetailLot에 까지 데이터를 기입한다.
        /// </summary>
        /// <returns></returns>
        private DataTable CreateSaveDatatable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "maintWorkOrderList";
            //===================================================================================
            dt.Columns.Add("WORKORDERID");            
            dt.Columns.Add("DOWNTYPE");
            dt.Columns.Add("DOWNCODE");
            dt.Columns.Add("ACCEPTTIME");
            dt.Columns.Add("SCHEDULETIME");
            dt.Columns.Add("ACCEPTUSERID");
            dt.Columns.Add("ACCEPTRESULT");
            dt.Columns.Add("REPAIRUSERID");
            dt.Columns.Add("REPAIRUSERCOUNT");
            dt.Columns.Add("ACCEPTCOMMENTS");

            dt.Columns.Add("EQUIPMENTID");

            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");

            dt.Columns.Add("CREATOR");
            dt.Columns.Add("CREATEDTIME");
            dt.Columns.Add("MODIFIER");
            dt.Columns.Add("MODIFIEDTIME");
            dt.Columns.Add("LASTTXNHISTKEY");
            dt.Columns.Add("LASTTXNID");
            dt.Columns.Add("LASTTXNUSER");
            dt.Columns.Add("LASTTXNTIME");
            dt.Columns.Add("LASTTXNCOMMENT");
            dt.Columns.Add("VALIDSTATE");
            dt.Columns.Add("_STATE_");

            return dt;
        }
        #endregion

        #region SaveData : 입력/수정을 수행
        private void SaveData()
        {
            //Validation 체크 부분 작성 필요
            this.ShowWaitArea();

            //저장 로직
            try
            {
                btnSave.Enabled = false;
                btnClose.Enabled = false;
                string messageCode = "";

                string saveStatus = null;
                if (cboRequestResult.EditValue != null)
                    saveStatus = cboRequestResult.EditValue.ToString();
                else
                {
                    ShowMessage(MessageBoxButtons.OK, "ValidateRequiredData", "");
                    return;
                }

                //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                if (ValidateContent(out messageCode, saveStatus))
                {
                    DataSet maintWorkOrderSet = new DataSet();                    
                    DataTable maintWorkOrderTable = CreateSaveDatatable();
                    DataRow orderRow = maintWorkOrderTable.NewRow();

                    DateTime requestDate = DateTime.Now;

                    orderRow["WORKORDERID"] = txtWorkOrderID.EditValue;
                    orderRow["DOWNTYPE"] = cboDownType.EditValue;
                    orderRow["DOWNCODE"] = cboDownCode.EditValue;
                    orderRow["ACCEPTTIME"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");                    
                    orderRow["ACCEPTUSERID"] = txtAcceptUser.EditValue;

                    //EES연동을 위해 EquipmentID를 파라미터로 보낸다.
                    orderRow["EQUIPMENTID"] = txtEquipmentID.EditValue;

                    orderRow["ACCEPTRESULT"] = saveStatus;
                    orderRow["ACCEPTCOMMENTS"] = txtAcceptComments.EditValue;

                    //수리상태일때만 아래의 데이터를 입력
                    if (saveStatus.Equals("Repair"))
                    {
                        orderRow["SCHEDULETIME"] = Convert.ToDateTime(deScheduleTime.EditValue).ToString("yyyy-MM-dd HH:mm:ss");
                        orderRow["REPAIRUSERID"] = txtRepairUser.EditValue;
                        orderRow["REPAIRUSERCOUNT"] = txtRepairUserCount.EditValue;
                    }
                    orderRow["VALIDSTATE"] = "Valid";

                    orderRow["MODIFIER"] = UserInfo.Current.Id;
                    orderRow["_STATE_"] = "modified";


                    maintWorkOrderTable.Rows.Add(orderRow);

                    maintWorkOrderSet.Tables.Add(maintWorkOrderTable);

                    ExecuteRule<DataTable>("RequestMaintWorkOrder", maintWorkOrderSet);

                    SearchHandler(txtWorkOrderID.Text);

                    Dispose();
                }
                else
                {
                    ShowMessage(MessageBoxButtons.OK, messageCode, "");
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
                btnSave.Enabled = true;
                btnClose.Enabled = true;
            }
        }
        #endregion

        #region LoadData - 수리요청 접수 등록 데이터를 화면에 표시한다.
        private void LoadData()
        {
            if (CurrentDataRow != null)
            {
                txtWorkOrderID.EditValue = CurrentDataRow.GetString("WORKORDERID");
                txtRequestUser.EditValue = CurrentDataRow.GetString("REQUESTUSER");
                txtRequestTime.EditValue = CurrentDataRow.GetString("REQUESTTIME");
                txtWorkOrderStatus.EditValue = CurrentDataRow.GetString("WORKORDERSTATUS");
                txtWorkOrderStep.EditValue = CurrentDataRow.GetString("WORKORDERSTEP");
                txtEquipmentID.EditValue = CurrentDataRow.GetString("EQUIPMENTID");
                txtEquipmentName.EditValue = CurrentDataRow.GetString("EQUIPMENTNAME");
                chkIsUse.Checked = CurrentDataRow.GetString("ISEQUIPMENTDOWNREQUEST").Equals("Y");
                chkIsEmergency.Checked = CurrentDataRow.GetString("ISEMERGENCY").Equals("Y");
                txtRequestComment.EditValue = CurrentDataRow.GetString("REQUESTCOMMENT");
                txtEmergencyContent.EditValue = CurrentDataRow.GetString("EMERGENCYREASON");
                txtAcceptComments.EditValue = CurrentDataRow.GetString("ACCEPTCOMMENTS");
                txtAcceptUser.EditValue = UserInfo.Current.Name;
                _acceptUserID = UserInfo.Current.Id;
                cboDownType.EditValue = CurrentDataRow.GetString("DOWNTYPEID");
                cboDownCode.EditValue = CurrentDataRow.GetString("DOWNCODEID");
                //cboRequestResult.EditValue = CurrentDataRow.GetString("");
                if (CurrentDataRow.GetString("SCHEDULETIME").Equals(""))
                    deScheduleTime.EditValue = DateTime.Now;
                else
                    deScheduleTime.EditValue = Convert.ToDateTime(CurrentDataRow.GetString("SCHEDULETIME"));

                txtRepairUser.EditValue = "";
                txtRepairUserCount.EditValue = "0";
            }
            else
            {
                ShowMessage(MessageBoxButtons.OK, "SelectItem", Language.Get("SPAREPART"));
            }
        }
        #endregion
        #endregion
    }
}
