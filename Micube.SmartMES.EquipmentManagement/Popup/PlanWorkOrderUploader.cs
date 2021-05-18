#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace Micube.SmartMES.EquipmentManagement.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 제조PM 자동입력 팝업
    /// 업  무  설  명  : Excel을 이용하여 제조PM을 자동입력하는 팝업
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-11-07
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class PlanWorkOrderUploader : SmartPopupBaseForm, ISmartCustomPopup
    {
        public DataRow CurrentDataRow { get; set; }
        string _currentStatus;
        string _maintType;
        string _factoryID;
        string _equipmentID;
        string _equipmentClassID;
        string _plantID;
        public delegate void reSearchEvent(string subMaintType);
        public event reSearchEvent SearchHandler;

        //메세지
        string _noneEquipmentMsg;
        string _manyEquipmentMsg;
        string _noneMaintItemMsg;
        string _manyMaintItemMsg;
        string _beforeStartDateMsg;
        string _afterEndDateMsg;
        string _noneDateMsg;
        string _existsWorkOrderIDMsg;
        string _noneWorkOrderIDMsg;

        bool _ischeckedData = false;

        private HSSFWorkbook hssfwb;
        private XSSFWorkbook xssfwb;

        private DataTable _result;
        ISheet sheet;

        public PlanWorkOrderUploader(string plantID)
        {
            InitializeComponent();

            InitializeEvent();

            _plantID = plantID;
        }

        public DateTime StartDate
        {
            set { deStartDate.EditValue = value; }
        }

        public DateTime EndDate
        {
            set { deEndDate.EditValue = value; }
        }

        public string MaintType
        {
            set { _maintType = value; }
        }

        public string FactoryID
        {
            set { _factoryID = value; }
        }

        public string EquipmentID
        {
            set { _equipmentID = value; }
        }

        public string EquipmentClassID
        {
            set { _equipmentClassID = value; }
        }

        #region 컨텐츠영역 초기화
        private void InitializeContent()
        {
            InitializeGrid();

            InitializeRequiredControl();
        }

        #region InitializeGrid - PM점검항목목록을 초기화한다.
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdPlanList.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            //grdPlanList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdPlanList.View.AddTextBoxColumn(" ", 50)         //Tool 번호
                .SetIsReadOnly(true);
            grdPlanList.View.AddTextBoxColumn("EQUIPMENTID", 150)         //Tool 번호
                .SetIsReadOnly(true);
            grdPlanList.View.AddTextBoxColumn("EQUIPMENTNAME", 250)           //Tool 코드
                .SetIsReadOnly(true);
            //grdPMPlan.View.AddComboBoxColumn("MAINTITEMID", 150, new SqlQuery("GetMaintItemListByEqp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "MAINTITEMNAME", "MAINTITEMID")         //Tool 버전
            //;
            grdPlanList.View.AddTextBoxColumn("MAINTITEMIDID", 150)
            .SetIsReadOnly(true);
            grdPlanList.View.AddTextBoxColumn("MAINTITEMID", 150)
            .SetIsReadOnly(true);
            //grdPMPlan.View.AddTextBoxColumn("MAINTITEMNAME", 180)
            //    .SetIsReadOnly(true);
            grdPlanList.View.AddTextBoxColumn("PLANDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime)
                .SetIsReadOnly(true)
                ;                                                             //계획일
            grdPlanList.View.AddTextBoxColumn("PROCESSRESULT", 300)   
                .SetIsReadOnly(true)
                ;                                                             //계획일
            grdPlanList.View.AddTextBoxColumn("WORKORDERID", 150)
                .SetIsReadOnly(true);
            grdPlanList.View.AddTextBoxColumn("MAINTITEMCLASSID")
                .SetIsHidden();
            grdPlanList.View.AddTextBoxColumn("MAINTITEMCLASSNAME")
                .SetIsHidden();
            grdPlanList.View.AddTextBoxColumn("ISGOOD")
                .SetIsHidden();
            
            grdPlanList.View.PopulateColumns();            
        }
        #endregion

        #region InitializeRequiredControl - 필수값 설정        
        private void InitializeRequiredControl()
        {
            _noneEquipmentMsg = Language.GetMessage("ValidateMWONONEEQUIPMENT").Message;
            _manyEquipmentMsg = Language.GetMessage("ValidateMWOMANYEQUIPMENT").Message;
            _noneMaintItemMsg = Language.GetMessage("ValidateMWONONEMAINTITEM").Message;
            _manyMaintItemMsg = Language.GetMessage("ValidateMWOMANYMAINTITEM").Message;
            _beforeStartDateMsg = Language.GetMessage("ValidateMWOBEFORESTARTDATE").Message;
            _afterEndDateMsg = Language.GetMessage("ValidateMWOAFTERENDDATE").Message;
            _noneDateMsg = Language.GetMessage("ValidateMWONONEDATE").Message;
            _existsWorkOrderIDMsg = Language.GetMessage("ValidateMWOEXISTSWORKORDERID").Message;
            _noneWorkOrderIDMsg = Language.GetMessage("ValidateMWONONEWORKORDERID").Message;

            deStartDate.ReadOnly = true;
            deEndDate.ReadOnly = true;
        }
        #endregion
        #endregion

        #region Event
        private void InitializeEvent()
        {
            Shown += RegistRequestMaintWorkOrderPopup_Shown;
            btnSave.Click += BtnSave_Click;
            btnClose.Click += BtnClose_Click;
            btnValidate.Click += BtnValidate_Click;
            btnInitialize.Click += BtnInitialize_Click;

            btnLoadExcel.Click += BtnLoadExcel_Click;
        }

        private void BtnLoadExcel_Click(object sender, EventArgs e)
        {
            DialogResult dgResult = fileDiag.ShowDialog();

            if(dgResult == DialogResult.OK)
            {
                if(!fileDiag.FileName.Equals(""))
                {
                    GetExcelFile(fileDiag.FileName);
                }
            }
        }

        private void BtnInitialize_Click(object sender, EventArgs e)
        {
            grdPlanList.View.ClearDatas();
        }

        private void BtnValidate_Click(object sender, EventArgs e)
        {
            CheckData();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            CreateSchedule();
        }

        private void RegistRequestMaintWorkOrderPopup_Shown(object sender, EventArgs e)
        {
            InitializeContent();
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
        private bool ValidateContent(out string messageCode, DataTable insertTable)
        {
            messageCode = "";

            if(insertTable.Rows.Count > 0)
            {
                if (_ischeckedData)
                {
                    foreach (DataRow insertRow in insertTable.Rows)
                    {
                        if (!insertRow.GetString("PROCESSRESULT").Equals(""))
                        {
                            messageCode = "ValidateMWORequiredPassed";
                            return false;
                        }
                    }
                }
                else
                {
                    messageCode = "ValidateMWORequiredPassed";
                    return false;
                }
            }
            else
            {
                messageCode = "ValidateMWORequiredPassed";
                return false;
            }

            return true;
        }
        #endregion

        /// <summary>
        /// 입력/수정 시의 Validation 체크
        /// 입력/수정 각각의 체크값이 서로 다르다면 상태에 따른 분기가 있어야 한다.
        /// </summary>
        /// <returns></returns>
        private bool ValidateContent()
        {
            //각각의 상태에 따라 보여줄 메세지가 상이하다면 각각의 분기에서 메세지를 표현해야 한다.            

            //DataTable inputTable = grdPMPlan.View.GetCheckedRows();

            //foreach (DataRow inputRow in inputTable.Rows)
            //{
            //    if (!ValidateCellInGrid(inputRow, new string[] { "EQUIPMENTID", "MAINTITEMID", "PLANDATE" }))
            //        return false;
            //}

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

        #region Private Function
        #region Search - 검색
        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected void Search()
        {
            InitializeInsertForm();
            // TODO : 조회 SP 변경
            Dictionary<string, object> values = new Dictionary<string, object>();

            _currentStatus = "modified";
        }
        #endregion

        #region InitializeInsertForm : 입고등록하기 위한 화면 초기화
        /// <summary>
        /// 입고등록정보를 입력하기 위해 화면을 초기화 한다.
        /// </summary>
        private void InitializeInsertForm()
        {
            try
            {
                _currentStatus = "added";           //현재 화면의 상태는 입력중이어야 한다.

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

            }
            else if (currentStatus == "modified") //
            {

            }
            else
            {

            }
        }
        #endregion

        #region CreateSaveDatatable : PreventiveMaintItem 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// <summary>
        /// PreventiveMaintItem 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// </summary>
        /// <returns></returns>
        private DataTable CreateSaveDatatable(bool useState)
        {
            DataTable dt = new DataTable();
            dt.TableName = "preventiveMaintPlanList";
            //===================================================================================            
            dt.Columns.Add("MAINTITEMID");
            dt.Columns.Add("EQUIPMENTID");
            dt.Columns.Add("PLANDATE");
            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");

            dt.Columns.Add("STARTDATE");
            dt.Columns.Add("ENDDATE");

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

            if (useState)
                dt.Columns.Add("_STATE_");

            return dt;
        }
        #endregion

        #region CreateDeleteDatatable : PreventiveMaintItem 삭제를 위한 DataTable의 Template 생성
        /// <summary>
        /// PreventiveMaintItem 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// </summary>
        /// <returns></returns>
        private DataTable CreateDeleteDatatable(bool useState)
        {
            DataTable dt = new DataTable();
            dt.TableName = "deletePMConditionList";
            //===================================================================================            
            dt.Columns.Add("DELETETYPE");
            dt.Columns.Add("FACTORYID");
            dt.Columns.Add("LANGUAGETYPE");
            dt.Columns.Add("EQUIPMENTCLASSID");
            dt.Columns.Add("EQUIPMENTID");
            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");

            dt.Columns.Add("MAINTITEMID");
            dt.Columns.Add("MAINTTYPE");

            dt.Columns.Add("P_PLANDATE_PERIODFR");
            dt.Columns.Add("P_PLANDATE_PERIODTO");
            dt.Columns.Add("ISWORKORDERID");

            return dt;
        }
        #endregion

        #region CreateSchedule : CreateSchedule버튼의 기능을 수행
        private void CreateSchedule()
        {
            //Validation 체크 부분 작성 필요
            this.ShowWaitArea();

            //저장 로직
            try
            {
                btnSave.Enabled = false;

                //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                if (ValidateContent())
                {
                    DataSet maintItemSet = new DataSet();
                    DataTable maintItemTable = CreateSaveDatatable(true);
                    DataTable deleteTable = CreateDeleteDatatable(true);

                    string messageCode = "";

                    DataTable ruleTable = (DataTable)grdPlanList.DataSource;

                    if (ValidateContent(out messageCode, ruleTable))
                    {
                        if (ruleTable != null && ruleTable.Rows.Count > 0)
                        {
                            //점검시작일을 기준으로 기존의 Equipment가 점검이 종료되지 않았는지 점검시작일 이후의 점검이 존재하는지 조회한다.
                            foreach (DataRow ruleRow in ruleTable.Rows)
                            {
                                if (IsNotBlankRow(ruleRow))
                                {
                                    DataRow maintItemRow = maintItemTable.NewRow();

                                    maintItemRow["MAINTITEMID"] = ruleRow.GetString("MAINTITEMIDID");
                                    maintItemRow["EQUIPMENTID"] = ruleRow.GetString("EQUIPMENTID");
                                    maintItemRow["PLANDATE"] = Convert.ToDateTime(ruleRow.GetString("PLANDATE")).ToString("yyyy-MM-dd");
                                    maintItemRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                                    maintItemRow["PLANTID"] = _plantID;

                                    maintItemRow["STARTDATE"] = ((DateTime)deStartDate.EditValue).ToString("yyyy-MM-dd");
                                    maintItemRow["ENDDATE"] = ((DateTime)deEndDate.EditValue).ToString("yyyy-MM-dd");

                                    maintItemRow["CREATOR"] = UserInfo.Current.Id;
                                    maintItemRow["MODIFIER"] = UserInfo.Current.Id;
                                    maintItemRow["_STATE_"] = "added";
                                    maintItemRow["VALIDSTATE"] = "Valid";

                                    maintItemTable.Rows.Add(maintItemRow);
                                }
                            }

                            maintItemSet.Tables.Add(maintItemTable);

                            DataRow deleteRow = deleteTable.NewRow();
                            //삭제를 위한 데이터 입력
                            deleteRow["DELETETYPE"] = "AUTO";
                            deleteRow["LANGUAGETYPE"] = UserInfo.Current.LanguageType;
                            deleteRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                            deleteRow["PLANTID"] = _plantID;
                            deleteRow["P_PLANDATE_PERIODFR"] = ((DateTime)deStartDate.EditValue).ToString("yyyy-MM-dd");
                            deleteRow["P_PLANDATE_PERIODTO"] = ((DateTime)deEndDate.EditValue).ToString("yyyy-MM-dd");
                            deleteRow["FACTORYID"] = _factoryID;
                            deleteRow["EQUIPMENTCLASSID"] = _equipmentClassID;
                            deleteRow["EQUIPMENTID"] = _equipmentID;
                            deleteRow["MAINTTYPE"] = _maintType;
                            deleteRow["ISWORKORDERID"] = "Y";

                            deleteTable.Rows.Add(deleteRow);

                            maintItemSet.Tables.Add(deleteTable);

                            ExecuteRule<DataTable>("PreventiveMaintPlanBatch", maintItemSet);

                            ControlEnableProcess("modified");

                            SearchHandler?.Invoke(_maintType);

                            Dispose();
                        }
                        else
                        {
                            ShowMessage(MessageBoxButtons.OK, "ValidateCheckedRowForPMPlan", "");
                        }
                    }
                    else
                    {
                        ShowMessage(MessageBoxButtons.OK, messageCode, "");
                    }
                }
                else
                {
                    ShowMessage(MessageBoxButtons.OK, "ToolDetailValidation", "");
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
            }
        }
        #endregion

        #region GetDateDurationCollection : 시작일과 종료일 내에서 점검주기단위에 따른 주기별 날짜를 계산하여 반환한다.
        private List<DateTime> GetDateDurationCollection(string startDateStr, string endDateStr, int duration, string durationUnit, string planDay, string planType)
        {
            List<DateTime> resultDates = new List<DateTime>();

            DateTime startDate = Convert.ToDateTime(startDateStr);
            DateTime endDate = Convert.ToDateTime(endDateStr);

            bool isStart = true;

            for (; DateTime.Compare(startDate, endDate) <= 0; startDate = GetNextDateTime(startDate, duration, durationUnit))
                if (DateTime.Compare(startDate, endDate) <= 0) //GetNextDateTime으로 받아온 날짜가 endDate를 초과한다면 Add하지 않아야 한다.
                {
                    resultDates.Add(startDate);

                    if (isStart) //시작일을 입력하고자 할때 특별한 로직을 따른다.
                    {
                        if (durationUnit.Equals("Month") && planType.Equals("Plan"))
                        {
                            //입력하고난 시작일을 다시 설정한다. 즉 다음 루프부터는 계획기준일에 맞추어 점검일을 설정한다.
                            startDate = Convert.ToDateTime(startDate.ToString("yyyy-MM") + "-" + planDay);
                        }
                        isStart = false;
                    }
                }

            return resultDates;
        }

        private DateTime GetNextDateTime(DateTime ruleDate, int duration, string durationUnit)
        {
            //ToDo : 요구사항에 결정된 주기에 맞추어 개량할 필요가 있음
            switch (durationUnit)
            {
                case "Quarter":
                    return ruleDate.AddMonths(duration * 3);
                case "Month":
                    return ruleDate.AddMonths(duration);
                case "Week":
                    return ruleDate.AddDays(duration * 7);
                case "Day":
                    return ruleDate.AddDays(duration);
                default:
                    return ruleDate.AddDays(duration);
            }
        }
        #endregion

        #region CheckData
        private void CheckData()
        {
            this.ShowWaitArea();
            try
            {
                string equipmentID = "";
                string maintItemID = "";
                string workOrderID = "";
                string planDate = "";
                bool isSuccess = true;

                if (PublishMaintWorkOrder())
                {
                    for (int i = 0; i < grdPlanList.View.RowCount; i++)
                    {
                        if (IsNotBlankRow(grdPlanList.View.GetDataRow(i)))
                        {
                            if (GetEquipmentID(grdPlanList.View.GetRowCellValue(i, "EQUIPMENTID").ToString(), out equipmentID))
                            {
                                grdPlanList.View.SetRowCellValue(i, " ", "Y");
                                grdPlanList.View.SetRowCellValue(i, "EQUIPMENTID", equipmentID);
                            }
                            else
                            {
                                grdPlanList.View.SetRowCellValue(i, " ", "N");
                                grdPlanList.View.SetRowCellValue(i, "PROCESSRESULT", equipmentID);
                                isSuccess = false;
                            }

                            if (GetMintItemID(equipmentID, grdPlanList.View.GetRowCellValue(i, "MAINTITEMIDID").ToString(), out maintItemID))
                            {
                                grdPlanList.View.SetRowCellValue(i, "MAINTITEMIDID", maintItemID);
                            }
                            else
                            {
                                grdPlanList.View.SetRowCellValue(i, " ", "N");
                                grdPlanList.View.SetRowCellValue(i, "PROCESSRESULT", maintItemID);
                                isSuccess = false;
                            }

                            if (IsDate(grdPlanList.View.GetRowCellValue(i, "PLANDATE").ToString(), out planDate))
                            {
                                ;
                            }
                            else
                            {
                                grdPlanList.View.SetRowCellValue(i, " ", "N");
                                grdPlanList.View.SetRowCellValue(i, "PROCESSRESULT", planDate);
                                isSuccess = false;
                            }
                        }
                    }
                    _ischeckedData = true;

                    if (isSuccess)
                    { 
                        ShowMessage(MessageBoxButtons.OK, "ValidateSuccessMWO", "");
                    }
                    else
                    {
                        ShowMessage(MessageBoxButtons.OK, "ValidateCancelMWO", "");
                    }
                }
                else
                {
                    ShowMessage(MessageBoxButtons.OK, "ValidateMWOPublishMaintWorkOrder", "");
                }
            }
            catch(Exception err)
            {
                throw err;
            }
            finally
            {
                this.CloseWaitArea();
            }
        }
        #endregion

        #region GetEquipmentID
        private bool GetEquipmentID(string equipmentName, out string equipmentID)
        {
            equipmentID = "";

            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("PLANTID", _plantID);
            values.Add("EQUIPMENTID", equipmentName);
            values.Add("MAINTTYPE", _maintType);
            values.Add("FACTORYID", _factoryID);
            values.Add("EQUIPMENTCLASSID", _equipmentClassID);
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);
            values.Add("ISMOD", "Y");

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable searchResult = SqlExecuter.Query("GetEquipmentListByEqp", "10001", values);

            if (searchResult.Rows.Count == 0)
            {
                equipmentID = _noneEquipmentMsg;
                return false;
            }
            else if (searchResult.Rows.Count == 1)
            {
                //상위의 검색조건에 선택된
                if (searchResult.Rows[0].GetString("EQUIPMENTID").Equals(_equipmentID) || _equipmentID.Equals(""))
                {
                    equipmentID = searchResult.Rows[0].GetString("EQUIPMENTID");
                    return true;
                }
                else
                {
                    equipmentID = _noneEquipmentMsg;
                    return false;
                }
            }
            else
            {
                equipmentID = _manyEquipmentMsg;
                return false;
            }
        }
        #endregion

        #region GetMintItemID
        private bool GetMintItemID(string equipmentID, string maintItemName, out string maintItemID)
        {
            maintItemID = "";
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("PLANTID", _plantID);
            values.Add("EQUIPMENTID", equipmentID);
            values.Add("MAINTITEMNAME", maintItemName);

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable searchResult = SqlExecuter.Query("GetMaintItemListByEqp", "10001", values);

            if (searchResult.Rows.Count == 0)
            {
                maintItemID = _noneMaintItemMsg;
                return false;
            }
            else if (searchResult.Rows.Count == 1)
            {
                maintItemID = searchResult.Rows[0].GetString("MAINTITEMID");
                return true;
            }
            else
            {
                maintItemID = _manyMaintItemMsg;
                return false;
            }
        }
        #endregion

        #region IsDate
        private bool IsDate(string planDateStr, out string planDate)
        {
            planDate = "";
            DateTime planDateTime = new DateTime();
            DateTime startDate = (DateTime)deStartDate.EditValue;
            DateTime endDate = (DateTime)deEndDate.EditValue;

            if (DateTime.TryParse(planDateStr, out planDateTime))
            {
                if(DateTime.Compare(startDate, planDateTime) > 0)
                {
                    if (!startDate.ToString("yyyy-MM-dd").Equals(planDateTime.ToString("yyyy-MM-dd")))
                    {
                        planDate = _beforeStartDateMsg;
                        return false;
                    }
                }

                if(DateTime.Compare(endDate, planDateTime) < 0 || DateTime.Compare(endDate, planDateTime) == 0)
                {
                    if (!endDate.ToString("yyyy-MM-dd").Equals(planDateTime.ToString("yyyy-MM-dd")))
                    {
                        planDate = _afterEndDateMsg;
                        return false;
                    }
                }

                planDate = planDateTime.ToString("yyyy-MM-dd");
                return true;
            }
            else
            {
                planDate = _noneDateMsg;
                return false;
            }
        }
        #endregion

        #region ExistsMaintWorkOrder
        private bool ExistsMaintWorkOrder(string equipmentID, string maintItemID, out string workOrderID)
        {
            workOrderID = "";
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("PLANTID", _plantID);
            values.Add("STARTDATE", deStartDate.EditValue.ToString());
            values.Add("ENDDATE", deEndDate.EditValue.ToString());
            values.Add("EQUIPMENTID", equipmentID);
            values.Add("MAINTITEMID", maintItemID);

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable searchResult = SqlExecuter.Query("GetPlanCountInPeriodByEqp", "10001", values);
                        
            if (searchResult.Rows.Count == 1)
            {
                if (searchResult.Rows[0].GetInteger("WORKORDERCREATECOUNT") > 0)
                {
                    workOrderID = _existsWorkOrderIDMsg;
                    return false;
                }
                else
                {
                    workOrderID = "";
                    return true;
                }
            }            
            else
            {
                workOrderID = _noneWorkOrderIDMsg;
                return false;
            }
        }
        #endregion

        #region PublishMaintWorkOrder
        private bool PublishMaintWorkOrder()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("PLANTID", _plantID);
            values.Add("P_PLANDATE_PERIODFR", Convert.ToDateTime(deStartDate.EditValue).ToString("yyyy-MM-dd"));
            values.Add("P_PLANDATE_PERIODTO", Convert.ToDateTime(deEndDate.EditValue).ToString("yyyy-MM-dd"));
            values.Add("FACTORYID", _factoryID);
            values.Add("EQUIPMENTCLASSID", _equipmentClassID);
            values.Add("EQUIPMENTID", _equipmentID);
            values.Add("MAINTTYPE", _maintType);
            values.Add("ISWORKORDERID", "Y");

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable searchResult = SqlExecuter.Query("GetPMPlanListByEqp", "10001", values);

            if (searchResult.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region GetExcelFile
        private void GetExcelFile(string fileName)
        {
            FileStream file = null;
            try
            {
                if (grdPlanList.View.DataSource != null)
                    grdPlanList.View.ClearDatas();


                string fileExtension = Path.GetExtension(fileName);
                string sheetName = "";

                file = new FileStream(fileName, FileMode.Open, FileAccess.Read);

                if (fileExtension == ".xls")
                {
                    hssfwb = new HSSFWorkbook(file);

                    sheetName = hssfwb.GetSheetName(0);

                    sheet = hssfwb.GetSheet(sheetName);
                }
                else if (fileExtension == ".xlsx")
                {
                    xssfwb = new XSSFWorkbook(file);

                    sheetName = xssfwb.GetSheetName(0);

                    sheet = xssfwb.GetSheet(sheetName);
                }

                IEnumerator rows = sheet.GetRowEnumerator();

                int gridRowIndex = 0;

                while (rows.MoveNext())
                {
                    IRow row = (IRow)rows.Current;
                    if (row != sheet.GetRow(0))
                    {
                        grdPlanList.View.AddNewRow();
                        grdPlanList.View.SetRowCellValue(gridRowIndex, "EQUIPMENTID", GetCellValueInExcel(row.GetCell(1)));
                        grdPlanList.View.SetRowCellValue(gridRowIndex, "EQUIPMENTNAME", GetCellValueInExcel(row.GetCell(2)));
                        grdPlanList.View.SetRowCellValue(gridRowIndex, "MAINTITEMIDID", GetCellValueInExcel(row.GetCell(3)));
                        grdPlanList.View.SetRowCellValue(gridRowIndex, "MAINTITEMID", GetCellValueInExcel(row.GetCell(4)));
                        grdPlanList.View.SetRowCellValue(gridRowIndex, "PLANDATE", GetCellValueInExcel(row.GetCell(5)));
                        gridRowIndex++;
                    }                    
                }

                _ischeckedData = false;
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                if (file != null)
                {
                    file.Dispose();
                    file.Close();
                }

                if (hssfwb != null)
                {
                    hssfwb.Dispose();
                    hssfwb.Close();
                }

                if (xssfwb != null)
                {
                    // xssfwb.Dispose();
                    xssfwb.Close();
                }
            }
        }

        private object GetCellValueInExcel(ICell cell)
        {
            if (cell != null)
            {
                switch (cell.CellType)
                {
                    case CellType.Unknown:
                        return "";
                    case CellType.Numeric:
                        if (cell.CellStyle.GetDataFormatString() == "General")
                            return cell.NumericCellValue;
                        else
                            return cell.DateCellValue;
                    case CellType.String:
                        return cell.StringCellValue;
                    case CellType.Formula:
                        return "";
                    case CellType.Blank:
                        return "";
                    case CellType.Boolean:
                        //smjang - boolean 값도 value 값을 받도록 설정
                        return cell.BooleanCellValue;
                    case CellType.Error:
                        return null;
                }
            }

            return "";
        }
        #endregion

        #region IsNotBlankRow - 특정 Row가 비어있는지 여부 조사
        private bool IsNotBlankRow(DataRow row)
        {
            if (row.GetString("EQUIPMENTID").Equals(""))
                return false;
            if (row.GetString("MAINTITEMIDID").Equals(""))
                return false;
            if (row.GetString("PLANDATE").Equals(""))
                return false;

            return true;
        }
        #endregion
        #endregion
    }
}
