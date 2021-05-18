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
    /// 프 로 그 램 명  : 제조및설비지연사유입력팝업
    /// 업  무  설  명  : 제조및 설비의 지연사유를 입력하는 팝업화면
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-11-01
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class RegistDelayReasonMaintWorkOrder : SmartPopupBaseForm, ISmartCustomPopup
    {
        public DataRow CurrentDataRow { get; set; }
        string _workOrderStatusID;
        string _repairUserID;
        string _maintItemID;
        string _areaID;
        string _processSegment;
        string _workOrderID;
        string _workOrderTypeID;
        string _plantID;
        string _isModify;

        public delegate void reSearchEvent();
        public event reSearchEvent SearchHandler;

        public delegate void reBindhEvent(string workOrderID, string workOrderStatus);
        public event reBindhEvent BindHandler;

        public RegistDelayReasonMaintWorkOrder(string workOrderID, string plantID, string isModify) : this()
        {
            _workOrderID = workOrderID;
            _plantID = plantID;
            _isModify = isModify;
        }
        public RegistDelayReasonMaintWorkOrder()
        {
            InitializeComponent();
            InitializeEvent();
        }

        #region  이벤트
        private void InitializeEvent()
        {
            Shown += RegistResultMaintWorkOrderProductPopup_Shown;
            btnClose.Click += BtnClose_Click;
            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void RegistResultMaintWorkOrderProductPopup_Shown(object sender, EventArgs e)
        {
            //화면 갱신후 서버로부터 데이터를 가져온다.
            Search();

            if (_isModify.Equals("Y"))
                btnSave.Enabled = true;
            else
                btnSave.Enabled = false;
        }

        #endregion

        #region 검색
        /// <summary>
        /// 검색을 수행한다. 각 컨트롤에 입력된 값을 파라미터로 받아들인다.
        /// </summary>
        /// <param name="durableDefName"></param>
        /// <param name="durableDefID"></param>
        /// <param name="durableDefVersion"></param>
        void Search()
        {
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("WORKORDERID", _workOrderID);
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dt = SqlExecuter.Query("GetMaintWorkOrderPMResultDetailByEqp", "10001", Param);

            if (dt.Rows.Count > 0)
            {
                DisplayMaintWorkOrderInfo(dt.Rows[0]);
                InitializeInsertForm(_workOrderStatusID);
            }
        }
        #endregion

        #region 유효성 검사
        /// <summary>
        /// 입력/수정 시의 Validation 체크
        /// 입력/수정 각각의 체크값이 서로 다르다면 상태에 따른 분기가 있어야 한다.
        /// </summary>
        /// <param name="messageCode">에러메세지를 표현할 각각의 코드값</param>
        /// <returns></returns>
        private bool ValidateContent(string action, out string messageCode)
        {
            messageCode = "";

            if (action.Equals("Save")) //저장을 하고자 하는 경우
            {
                if (_workOrderStatusID.Equals("Finish") || _workOrderStatusID.Equals("Omit"))
                {
                    messageCode = "ValidateMaintWorkOrderStatusModified";
                    return false;
                }
                else //저장이 가능한 상태이므로 필수값 체크를 진행한다.
                {
                    if (_workOrderID.Equals(""))
                    {
                        messageCode = "ValidateDontFindMaintWorkOrderID";
                        return false;
                    }
                    if (!ValidateEditValue(txtDelayReason.EditValue))
                    {
                        messageCode = "ValidateDontFindDelayReason";
                        return false;
                    }
                }
            }
            return true;
        }

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
            //입력받은 기준값(예를 들어 0)보다 작다면 false를 반환
            if (Int32.TryParse(originBox.EditValue.ToString(), out resultValue))
            {
                if (resultValue < ruleValue)
                    return false;
            }
            else
            {
                return false;
            }
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
        private void InitializeInsertForm(string workOrderStatus)
        {
            try
            {
                ControlEnableProcess(workOrderStatus);
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
            if (currentStatus == "Create") //생성된 상태
            {
                txtWorkOrderID.ReadOnly = true;
                txtProgressStatus.ReadOnly = true;
                txtWorkOrderStatus.ReadOnly = true;
                txtEquipmentID.ReadOnly = true;
                txtEquipmentName.ReadOnly = true;
                txtPlanDate.ReadOnly = true;
                txtArea.ReadOnly = true;
                txtProcessSegment.ReadOnly = true;
                txtPMItem.ReadOnly = true;                
                txtDelayReason.ReadOnly = false;
                
                //상태에 따른 필수값 선정 변경
                SetRequiredValidationControl(lblDelayReason, true);
            }
            else if (currentStatus == "Start") //
            {
                txtWorkOrderID.ReadOnly = true;
                txtProgressStatus.ReadOnly = true;
                txtWorkOrderStatus.ReadOnly = true;
                txtEquipmentID.ReadOnly = true;
                txtEquipmentName.ReadOnly = true;
                txtPlanDate.ReadOnly = true;
                txtArea.ReadOnly = true;
                txtProcessSegment.ReadOnly = true;
                txtPMItem.ReadOnly = true;                
                txtDelayReason.ReadOnly = false;
                
                //상태에 따른 필수값 선정 변경
                SetRequiredValidationControl(lblDelayReason, true);
            }
            else if (currentStatus == "Omit") //
            {
                txtWorkOrderID.ReadOnly = true;
                txtProgressStatus.ReadOnly = true;
                txtWorkOrderStatus.ReadOnly = true;
                txtEquipmentID.ReadOnly = true;
                txtEquipmentName.ReadOnly = true;
                txtPlanDate.ReadOnly = true;
                txtArea.ReadOnly = true;
                txtProcessSegment.ReadOnly = true;
                txtPMItem.ReadOnly = true;                
                txtDelayReason.ReadOnly = true;
                
                //상태에 따른 필수값 선정 변경
                SetRequiredValidationControl(lblDelayReason, false);
            }
            else
            {
                txtWorkOrderID.ReadOnly = true;
                txtProgressStatus.ReadOnly = true;
                txtWorkOrderStatus.ReadOnly = true;
                txtEquipmentID.ReadOnly = true;
                txtEquipmentName.ReadOnly = true;
                txtPlanDate.ReadOnly = true;
                txtArea.ReadOnly = true;
                txtProcessSegment.ReadOnly = true;
                txtPMItem.ReadOnly = true;                
                txtDelayReason.ReadOnly = true;
                
                SetRequiredValidationControl(txtDelayReason, false);
            }
        }
        #endregion

        #region DisplayMaintWorkOrderInfo : 상세정보 화면 바인딩
        void DisplayMaintWorkOrderInfo(DataRow workOrderRow)
        {
            txtWorkOrderID.EditValue = workOrderRow.GetString("WORKORDERID");
            txtProgressStatus.EditValue = "";
            _workOrderStatusID = workOrderRow.GetString("WORKORDERSTATUSID");
            txtWorkOrderStatus.EditValue = workOrderRow.GetString("WORKORDERSTATUS");
            _workOrderTypeID = workOrderRow.GetString("WORKORDERTYPE");
            txtEquipmentID.EditValue = workOrderRow.GetString("EQUIPMENTID");
            txtEquipmentName.EditValue = workOrderRow.GetString("EQUIPMENTNAME");
            txtPlanDate.EditValue = workOrderRow.GetString("SCHEDULETIME");
            txtArea.EditValue = workOrderRow.GetString("AREANAME");
            _areaID = workOrderRow.GetString("AREAID");
            txtProcessSegment.EditValue = workOrderRow.GetString("PROCESSSEGMENTCLASSNAME");
            _processSegment = workOrderRow.GetString("PROCESSSEGMENTCLASSID");
            txtPMItem.EditValue = workOrderRow.GetString("MAINTITEMNAME");
            _maintItemID = workOrderRow.GetString("MAINTITEMID");            
            txtDelayReason.EditValue = workOrderRow.GetString("DELAYMAINTREASON");            
        }
        #endregion

        #region GetMaintWorkOrderTable : MaintWorkOrder 데이터저장용 테이블 
        private DataTable GetMaintWorkOrderTable()
        {
            DataTable returnTable = new DataTable("maintWorkOrderList");

            returnTable.Columns.Add("WORKORDERID");
            returnTable.Columns.Add("WORKORDERSTATUS");
            returnTable.Columns.Add("WORKORDERTYPE");
            returnTable.Columns.Add("STARTTIME");
            returnTable.Columns.Add("REPAIRUSERID");
            returnTable.Columns.Add("REPAIRUSERCNT");
            returnTable.Columns.Add("ISEQUIPMENTDOWN");
            returnTable.Columns.Add("FINISHDATE");
            returnTable.Columns.Add("REPAIRCOMMENT");
            returnTable.Columns.Add("DELAYMAINTREASON");
            returnTable.Columns.Add("DONOTMAINTREASON");

            returnTable.Columns.Add("EQUIPMENTID");

            returnTable.Columns.Add("ENTERPRISEID");
            returnTable.Columns.Add("PLANTID");

            returnTable.Columns.Add("CREATOR");
            returnTable.Columns.Add("CREATEDTIME");
            returnTable.Columns.Add("MODIFIER");
            returnTable.Columns.Add("MODIFIEDTIME");
            returnTable.Columns.Add("LASTTXNHISTKEY");
            returnTable.Columns.Add("LASTTXNID");
            returnTable.Columns.Add("LASTTXNUSER");
            returnTable.Columns.Add("LASTTXNTIME");
            returnTable.Columns.Add("LASTTXNCOMMENT");
            returnTable.Columns.Add("VALIDSTATE");

            returnTable.Columns.Add("_STATE_");

            return returnTable;
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
                btnClose.Enabled = false;
                btnSave.Enabled = false;

                string messageCode = "";
                //점검생략을 선택한 경우

                #region 설비수리실적 저장
                //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                if (ValidateContent("Save", out messageCode))
                {
                    DataSet maintWorkOrderSet = new DataSet();
                    //치공구 제작의뢰를 입력
                    DataTable maintWorkOrderTable = GetMaintWorkOrderTable();

                    DataRow maintWorkOrderRow = maintWorkOrderTable.NewRow();
                    
                    maintWorkOrderRow["WORKORDERID"] = _workOrderID;
                    maintWorkOrderRow["WORKORDERSTATUS"] = "Delay";
                    maintWorkOrderRow["WORKORDERTYPE"] = _workOrderTypeID;

                    maintWorkOrderRow["DELAYMAINTREASON"] = txtDelayReason.EditValue;

                    maintWorkOrderRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                    maintWorkOrderRow["PLANTID"] = _plantID;

                    maintWorkOrderRow["MODIFIER"] = UserInfo.Current.Id;
                    maintWorkOrderRow["VALIDSTATE"] = "Valid";
                    maintWorkOrderRow["_STATE_"] = "modified";

                    maintWorkOrderTable.Rows.Add(maintWorkOrderRow);

                    maintWorkOrderSet.Tables.Add(maintWorkOrderTable);

                    ExecuteRule<DataTable>("MaintWorkOrder", maintWorkOrderSet);

                    SearchHandler?.Invoke();

                    if (_workOrderStatusID.Equals("Create"))
                        BindHandler?.Invoke(_workOrderID, "Start");
                    else
                        BindHandler?.Invoke(_workOrderID, "Finish");
                    Dispose();
                }
                else
                {
                    this.ShowMessage(MessageBoxButtons.OK, messageCode, "");
                }
                #endregion

            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
                btnClose.Enabled = true;
                btnSave.Enabled = true;
            }
        }
        #endregion
        #endregion
    }
}
