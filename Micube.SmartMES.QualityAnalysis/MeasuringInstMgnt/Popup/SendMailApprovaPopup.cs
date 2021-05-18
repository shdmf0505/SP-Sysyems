#region using

using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Log;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
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

namespace Micube.SmartMES.QualityAnalysis
{

    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 메일보내기 팝업
    /// 업  무  설  명  : 유저를 검색하여 메일과 SMS를 보낸다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-06-17
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class SendMailApprovaPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// 팝업 그리드와 원래 그리드를 비교하기 위한 변수
        /// </summary>
        private DataTable _mappingDataSource = new DataTable();

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }

        private bool _isGridMainViewClick = false;
        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public SendMailApprovaPopup()
        {
            InitializeComponent();

            InitializeEvent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// Mail로 보낼 데이터 MemoEdit에 바인딩
        /// </summary>
        private void InitializeCurrentData()
        {
            //grbIssueInformation.GridButtonItem = GridButtonItem.None;

            //txtAnalysisDate.EditValue = CurrentDataRow["ANALYSISDATE"]; // 분석일
            //txtEquipmentName.EditValue = CurrentDataRow["EQUIPMENTNAME"]; // 설비명
            //txtChemicalLevel.EditValue = CurrentDataRow["CHEMICALLEVEL"]; // 약품등급
            //txtDegree.EditValue = CurrentDataRow["DEGREE"]; // 차수
            //txtChildEquipmentName.EditValue = CurrentDataRow["CHILDEQUIPMENTNAME"]; // 설비단명
            //txtManagementScope.EditValue = CurrentDataRow["MANAGEMENTSCOPE"]; // 관리범위
            //txtProcesssegmentclassName.EditValue = CurrentDataRow["PROCESSSEGMENTCLASSNAME"]; // 대공정명
            //txtChemicalName.EditValue = CurrentDataRow["CHEMICALNAME"]; // 약품
            //txtAnalysisValue.EditValue = CurrentDataRow["ANALYSISVALUE"]; // 분석치
        }
        /// <summary>        
        /// 그리드 초기화 - 결제 정보 List
        /// </summary>
        private void InitializeGridApproval()
        {
            grdMain.GridButtonItem = GridButtonItem.Export;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdMain.View.SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("CHARGERMODE", 150).SetLabel("MODE");                     //담당자구분
            grdMain.View.AddTextBoxColumn("STATUS", 150).SetLabel("RECEIVER");                      //상태 - 담당자 수신자
            grdMain.View.AddTextBoxColumn("CHARGERID", 150).SetLabel("CHARGERID");                  //담당자ID
            grdMain.View.AddTextBoxColumn("ISAPPROVAL", 150).SetLabel("MEASURINGISAPPROVAL");       //승인여부
            grdMain.View.AddTextBoxColumn("APPROVALTIME", 150).SetLabel("MEASURINGAPPROVALTIME");   //결제시간
            grdMain.View.AddTextBoxColumn("REJECTCOMMENTS", 150).SetLabel("REJECTCOMMENTS");        //반려사유
            grdMain.View.AddTextBoxColumn("DESCRIPTION", 150).SetLabel("DESCRIPTION");              //설명
            grdMain.View.AddTextBoxColumn("SERIALNO", 150).SetLabel("MEASURINGSERIALNO");           //시리얼번호 PK
            grdMain.View.AddTextBoxColumn("CONTROLNO", 150).SetLabel("MEASURINGCONTROLNO");         //관리번호 PK
            grdMain.View.AddTextBoxColumn("REPAIRSCRAPID", 150).SetLabel("MEASURINGREPAIRSCRAPID"); //수리폐기ID PK
            grdMain.View.AddTextBoxColumn("DEPARTMENT", 150).SetLabel("DEPARTMENT");                //요청부서
            grdMain.View.AddTextBoxColumn("SEQ", 150).SetLabel("SEQ");                              //순번
            grdMain.View.AddTextBoxColumn("CREATOR", 150).SetLabel("CREATOR");                      //생성자
            grdMain.View.AddTextBoxColumn("CREATEDTIME", 150).SetLabel("CREATEDTIME");              //생성일
            grdMain.View.AddTextBoxColumn("MODIFIER", 150).SetLabel("MODIFIER");                    //수정자
            grdMain.View.AddTextBoxColumn("MODIFIEDTIME", 150).SetLabel("MODIFIEDTIME");            //수정일
            grdMain.View.BestFitColumns();
            grdMain.View.PopulateColumns();
        }

        /// <summary>        
        /// 그리드 초기화 - 수리/폐기 이력
        /// </summary>
        private void InitializeGridScrap()
        {
            grdMain.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdMain.View.SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("SERIALNO", 150).SetLabel("MEASURINGSERIALNO");                         //시리얼번호 PK
            grdMain.View.AddTextBoxColumn("CONTROLNO", 150).SetLabel("MEASURINGCONTROLNO");                       //관리번호 PK
            grdMain.View.AddTextBoxColumn("REPAIRSCRAPID", 150).SetLabel("MEASURINGREPAIRSCRAPID");               //수리폐기ID PK
            grdMain.View.AddTextBoxColumn("DEPARTMENT", 150).SetLabel("MEASURINGDEPARTMENT");                     //사용부서(부서ID)
            grdMain.View.AddTextBoxColumn("ISLOSSDANGERS", 150).SetLabel("MEASURINGLOSSDIVISION");               //손망실구분
            grdMain.View.AddTextBoxColumn("POSITION", 150).SetLabel("MEASURINGPOSITION");                         //수리내역 등록자 지급
            grdMain.View.AddTextBoxColumn("REPAIRUSER", 150).SetLabel("MEASURINGREPAIRUSER");                     //수리내역 등록 성명
            grdMain.View.AddTextBoxColumn("EQUIPMENTTYPE", 150).SetLabel("MEASURINGEQUIPMENTTYPE");               //장비유형(설비유형)
            grdMain.View.AddTextBoxColumn("EQUIPMENTID", 150).SetLabel("MEASURINGEQUIPMENTID");                   //설비 ID(계측기ID)
            grdMain.View.AddTextBoxColumn("DEPARTMENTMNT", 150).SetLabel("MEASURINGMANAGEMENTDEPARTMENT");               //관리부서
            grdMain.View.AddTextBoxColumn("PURCHASEDATE", 150).SetLabel("MEASURINGPURCHASEDATE");                 //구매일
            grdMain.View.AddTextBoxColumn("MANUFACTURECOUNTRY", 150).SetLabel("MEASURINGMANUFACTURECOUNTRY");     //제조국 ID
            grdMain.View.AddTextBoxColumn("MANUFACTURER", 150).SetLabel("MEASURINGMANUFACTURER");                 //제조사 ID
            grdMain.View.AddTextBoxColumn("OCCURTIME", 150).SetLabel("MEASURINGDATEOFOCCURRENCE");                       //발생일시
            grdMain.View.AddTextBoxColumn("OCCURPLACE", 150).SetLabel("MEASURINGOCCURPLACE");                     //발생장소
            grdMain.View.AddTextBoxColumn("PHOTO", 150).SetLabel("MEASURINGPHOTO");                               //사진
            grdMain.View.AddTextBoxColumn("QAENDTIME", 150).SetLabel("MEASURINGPURCHASEDATE");                       //QA완료일자
            grdMain.View.AddTextBoxColumn("REPAIRCOST", 150).SetLabel("MEASURINGREPAIRCOST");                     //수리비용
            grdMain.View.BestFitColumns();
            grdMain.View.PopulateColumns();

        }
        /// <summary>
        /// 유저 리스트 그리드 초기화
        /// </summary>
        private void InitializeUserList()
        {
            grdUserList.GridButtonItem = GridButtonItem.Export;
            grdUserList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdUserList.View.SetIsReadOnly();

            grdUserList.View.AddTextBoxColumn("USERID", 100); // 유저 ID
            grdUserList.View.AddTextBoxColumn("USERNAME", 100); // 유저 Name
            grdUserList.View.AddTextBoxColumn("EMAILADDRESS", 220); // Email
            grdUserList.View.AddTextBoxColumn("CELLPHONENUMBER", 150); // 핸드폰

            grdUserList.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += ChemicalissuePopup_Load;

            btnAdd.Click += BtnAdd_Click;
            btnDelete.Click += BtnDelete_Click;
            btnSend.Click += BtnSend_Click;
            btnClose.Click += BtnClose_Click;

            this.grdMain.View.Click += grdMain_View_Click;

            
        }

        #region Grid Event
        /// <summary>
        /// 그리드 View Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdMain_View_Click(object sender, EventArgs e)
        {
            grdMainViewClick();
        }

        /// <summary>
        /// 기본값 입력
        /// </summary>
        private void DefaultDataSetting()
        {
            if (CurrentDataRow != null)
            {
                string titleFix = "[결제 요청] 수리/ 폐기 이력 건";//sia확인 : 다국어
                string equipmenttype = CurrentDataRow["EQUIPMENTTYPE"].ToSafeString();
                string equipmentid = CurrentDataRow["EQUIPMENTID"].ToSafeString();
                //string productdefid = CurrentDataRow["PRODUCTDEFID"].ToSafeString();
                //string productdefversion = CurrentDataRow["PRODUCTDEFVERSION"].ToSafeString();
                string productdefid = CurrentDataRow["MEASURINGPRODUCTID"].ToSafeString(); // 2020.06.04-유석진-모델명으로 변경
                string departmentmnt = CurrentDataRow["DEPARTMENTMNT"].ToSafeString();
                string purchasedate = CurrentDataRow["PURCHASEDATE"].ToSafeString();
                string manufacturecountry = CurrentDataRow["MANUFACTURECOUNTRY"].ToSafeString();
                string manufacturer = CurrentDataRow["MANUFACTURER"].ToSafeString();
                string occurtime = CurrentDataRow["OCCURTIME"].ToSafeString();
                string occurplace = CurrentDataRow["OCCURPLACE"].ToSafeString();

                //string titleMain = string.Format("{0} - {1}, {2}", titleFix, equipmentid, productdefid);
                string titleMain = string.Format("{0}", titleFix); // 2020.06.05-유석진-메일 타이틀 수정
                this.txtTitle.Text = string.Format("{0}", titleMain);

                string repairInfo = string.Format(
                    "1. 장비유형 : {0}{9}2. 계측기ID : {1}{9}3. 모델명 : {2}{9}" +
                    "4. 관리부서 : {3}{9}5. 구매일 : {4}{9}6. 제조국 : {5}{9}7. 제조사 : {6}{9}8. 발생일시 : {7}{9}9. 발생장소 : {8}{9}",
                    equipmenttype, equipmentid, productdefid, departmentmnt, purchasedate
                    , manufacturecountry, manufacturer, occurtime, occurplace, Environment.NewLine);
                this.memoEmail.EditValue = repairInfo;
            }
        }
        
        /// <summary>
        /// 그리드 Cell Click시 자료 조회 (발생, 증상, 조치)
        /// </summary>
        private void grdMainViewClick()
        {
            if (_isGridMainViewClick) return;

            _isGridMainViewClick = true;
            try
            {
                if (grdMain.View.FocusedRowHandle < 0) return;

                //DXMouseEventArgs args = e as DXMouseEventArgs;
                //GridView view = sender as GridView;
                //GridHitInfo info = view.CalcHitInfo(args.Location);
                DataRow row = this.grdMain.View.GetFocusedDataRow();

                //부서
                popupDepartment.Text = row["DEPARTMENT"].ToSafeString();
                popupDepartment.SetValue(row["DEPARTMENT"].ToSafeString());
                //작성자
                popupChargerID.Text = row["CHARGERID"].ToSafeString();
                popupChargerID.SetValue(row["CHARGERID"].ToSafeString());
                //구분(담당자 구분)
                cboChargermode.EditValue = row["CHARGERMODE"].ToSafeString();
                //승인여부
                cboisapproval.EditValue = row["ISAPPROVAL"].ToSafeString();
                //수신자
                popupReceiver.Text = row["STATUS"].ToSafeString();
                popupReceiver.SetValue(row["STATUS"].ToSafeString());
                //메일 내용
                memoEmail.EditValue = row["DESCRIPTION"].ToSafeString();
                //반려 사유
                txtRejectComments.Text = row["REJECTCOMMENTS"].ToSafeString();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            finally
            {
                _isGridMainViewClick = false;
            }
        }
        #endregion Grid Event


        /// <summary>
        /// 메일 송신
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSend_Click(object sender, EventArgs e)
        {
            int result = 1;
            this.DialogResult = DialogResult.Cancel;
            result = SaveApproval();//DB 저장 - 결제정보.

            if (result != 0)
            {
                return;
            }

            //sia작업 : SendMailApprova-BtnSend_Click
            DataTable dt = grdUserList.DataSource as DataTable;

            if (dt.Rows.Count == 0)
            {
                throw MessageException.Create("NoSaveData");
            }

            if (ShowMessage(MessageBoxButtons.YesNo, "IsSendMail") == DialogResult.Yes)
            {

                DataTable sendTable = new DataTable();
                sendTable.TableName = "list";
                DataRow row = null;

                sendTable.Columns.Add(new DataColumn("USERID", typeof(string)));
                sendTable.Columns.Add(new DataColumn("EMAILADDRESS", typeof(string)));
                sendTable.Columns.Add(new DataColumn("TITLE", typeof(string)));
                //sendTable.Columns.Add(new DataColumn("CONTENT", typeof(string))); // 2020.06.05-유석진-주석처리
                sendTable.Columns.Add(new DataColumn("EQUIPMENTTYPE", typeof(string))); // 2020.06.05-유석진-장비유형
                sendTable.Columns.Add(new DataColumn("EQUIPMENTID", typeof(string))); // 2020.06.05-유석진-계측기ID
                sendTable.Columns.Add(new DataColumn("PRODUCTDEFID", typeof(string))); // 2020.06.05-유석진-모델명
                sendTable.Columns.Add(new DataColumn("DEPARTMENTMNT", typeof(string))); // 2020.06.05-유석진-관리부서
                sendTable.Columns.Add(new DataColumn("PURCHASEDATE", typeof(string))); // 2020.06.05-유석진-구매일
                sendTable.Columns.Add(new DataColumn("MANUFACTURECOUNTRY", typeof(string))); // 2020.06.05-유석진-제조국
                sendTable.Columns.Add(new DataColumn("MANUFACTURER", typeof(string))); // 2020.06.05-유석진-제조사
                sendTable.Columns.Add(new DataColumn("OCCURTIME", typeof(string))); // 2020.06.05-유석진-발생일시
                sendTable.Columns.Add(new DataColumn("OCCURPLACE", typeof(string))); // 2020.06.05-유석진-발생장소

                foreach (DataRow dr in dt.Rows)
                {
                    row = sendTable.NewRow();
                    row["USERID"] = UserInfo.Current.Id;
                    row["EMAILADDRESS"] = dr["EMAILADDRESS"];
                    row["TITLE"] = txtTitle.EditValue;
                    //row["CONTENT"] = memoEmail.EditValue; // 2020.06.05-유석진-주석처리
                    if (CurrentDataRow != null)
                    {
                        row["EQUIPMENTTYPE"] = "1. 장비유형 : " + CurrentDataRow["EQUIPMENTTYPE"].ToSafeString();
                        row["EQUIPMENTID"] = "2. 계측기ID : " + CurrentDataRow["EQUIPMENTID"].ToSafeString();
                        row["PRODUCTDEFID"] = "3. 모델명 : " + CurrentDataRow["PRODUCTDEFID"].ToSafeString();
                        row["DEPARTMENTMNT"] = "4. 관리부서 : " + CurrentDataRow["DEPARTMENTMNT"].ToSafeString();
                        row["PURCHASEDATE"] = "5. 구매일 : " + CurrentDataRow["PURCHASEDATE"].ToSafeString();
                        row["MANUFACTURECOUNTRY"] = "6. 제조국 : " + CurrentDataRow["MANUFACTURECOUNTRY"].ToSafeString();
                        row["MANUFACTURER"] = "7. 제조사 : " + CurrentDataRow["MANUFACTURER"].ToSafeString();
                        row["OCCURTIME"] = "8. 발생일시 : " + CurrentDataRow["OCCURTIME"].ToSafeString();
                        row["OCCURPLACE"] = "9. 발생장소 : " + CurrentDataRow["OCCURPLACE"].ToSafeString();
                    }

                    sendTable.Rows.Add(row);
                }

                DataSet rullSet = new DataSet();
                rullSet.Tables.Add(sendTable);
                ExecuteRule("SendMailApproval", rullSet);

                ShowMessage("SuccessSendMail");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        private void btnSend_Click_1(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 유저 삭제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (grdUserList.View.RowCount == 0) return;

            DataTable userList = (grdUserList.DataSource as DataTable).Clone();

            for (int i = 0; i < grdUserList.View.RowCount; i++)
            {
                if (!grdUserList.View.IsRowChecked(i))
                {
                    userList.ImportRow((grdUserList.DataSource as DataTable).Rows[i]);
                }
            }

            grdUserList.View.ClearDatas();
            grdUserList.DataSource = userList;
        }

        /// <summary>
        /// 유저 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            selectUserPopup();
        }

        /// <summary>
        /// 닫기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void SendMailApprovaPopup_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Form Road시 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChemicalissuePopup_Load(object sender, EventArgs e)
        {
            //this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.tpgSMS.PageVisible = false;
            InitializeGridApproval();           //Grid 초기화 - 결제 정보
            //InitializeGridScrap();              //Grid 초기화 - 수리/폐기 정보.
            InitializeUserList();               //Grid 초기화 - 수신자 List.
            InitializeCurrentData();
            SetComboBoxCreateValueChargermode();//구분
            SetComboBoxCreateValueIsapproval();//승인여부
            //관리부서
            popupDepartment.SelectPopupCondition = DepartmentInfoPopup();
            //작성자
            popupChargerID.SelectPopupCondition = UserChargerPopup();
            //수신자
            popupReceiver.SelectPopupCondition = UserReceiverPopup();


            //수리폐기 이력 PK별 결제 정보 조회 
            GetMeasuringRepairApprovalGridData();

            //수리/폐기 정보 - Grid 표시.
            //GetMeasuringRepairGridData();

            this.DefaultDataSetting();
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 수리폐기 이력 PK별 결제 정보 조회 
        /// </summary>
        private void GetMeasuringRepairApprovalGridData()
        {
            try
            {
                this.grdMain.View.ClearDatas();
                Dictionary<string, object> param = new Dictionary<string, object>();
                //param.Add("P_PLANTID", plantID);
                param.Add("P_SERIALNO", CurrentDataRow["SERIALNO"].ToSafeString());
                param.Add("P_CONTROLNO", CurrentDataRow["CONTROLNO"].ToSafeString());
                param.Add("P_REPAIRSCRAPID", CurrentDataRow["REPAIRSCRAPID"].ToSafeString());

                //SqlQuery rnrUserList = new SqlQuery("GetAreaidListByCsm", "10001", $"P_PLANTID={UserInfo.Current.Plant}", $"P_AREANAME={UserInfo.Current.Area}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
                SqlQuery approvalList = new SqlQuery("SelectRawMeasuringRepairApproveListDirect", "10001", param);
                DataTable approvalTable = approvalList.Execute();
                if (approvalTable != null && approvalTable.Rows.Count > 0)
                {
                    this.grdMain.DataSource = approvalTable;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }




        /// <summary>
        /// 수리폐기 이력정보 Grid에 입력.
        /// </summary>
        private void GetMeasuringRepairGridData()
        {
            DataTable RepairTable = grdMain.DataSource as DataTable;
            var filter = (grdMain.View.DataSource as DataView).RowStateFilter;
            RepairTable.ImportRow(CurrentDataRow);
        }
        /// <summary>
        /// 콤보박스 초기 공통 속성값 설정.
        /// </summary>
        /// <param name="cboBox"></param>
        private void SetComboBoxInitial(SmartComboBox cboBox)
        {
            cboBox.ValueMember = "CODEID";
            cboBox.DisplayMember = "CODENAME";
            cboBox.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboBox.ShowHeader = false;
        }

        /// <summary>
        /// CheckBox에서 결제 구분 값 설정.
        /// </summary>
        private void SetComboBoxCreateValueChargermode()
        {
            DataTable dataTable;
            SetComboBoxInitial(cboChargermode);
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dicParam.Add("CODECLASSID", "MeasuringChargerMode");

            dataTable = SqlExecuter.Query("GetCodeList", "00001", dicParam);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                this.cboChargermode.DataSource = dataTable;
            }
        }
        /// <summary>
        /// CheckBox에서 승인여부 값 설정.
        /// </summary>
        private void SetComboBoxCreateValueIsapproval()
        {
            DataTable dataTable;
            SetComboBoxInitial(cboisapproval);
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dicParam.Add("CODECLASSID", "MeasuringIsapproval");

            dataTable = SqlExecuter.Query("GetCodeList", "00001", dicParam);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                this.cboisapproval.DataSource = dataTable;
            }
        }

        /// <summary>
        /// 저장
        /// </summary>
        private int SaveApproval()
        {
            int result = 1;
            //this.DialogResult = DialogResult.Cancel;

            //if (_isControlinitial) return;

            //저장 Rule 변경
            DataTable dtApproval = CreateDataTableApproval();//결제

            try
            {
                result = this.DataEditSaveApproval(ref dtApproval);
                if (result == 0)
                {
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dtApproval);

                    ExecuteRule("SaveMeasuringInstApproval", ds);

                    result = 0;
                }
                else
                {

                }
                
                //this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
          
                ShowMessage("정상적으로 자료를 저장하지 못했습니다.");//sia확인 : 다국어 처리.
                Logger.Error(ex.Message);
            }

            return result;
        }
        /// <summary>
        /// 결제 입력 자료 저장 Data Table 구성.
        /// </summary>
        /// <param name="data"></param>
        private int DataEditSaveApproval(ref DataTable data)
        {
            int result = 1;
            DataRow dataRow = data.NewRow();
            string serialNo = "";
            string controlno = "";
            string repairscrapid = "";
            string department = "";
            string chargermode = "";
            int nSeq = 0;

            string receiver = "";
            string isApproval = "";
            string chargerID = "";
            //PK
            serialNo = CurrentDataRow["SERIALNO"].ToString();
            controlno = CurrentDataRow["CONTROLNO"].ToString();
            repairscrapid = CurrentDataRow["REPAIRSCRAPID"].ToString();
            department = popupDepartment.GetValue().ToString();
            if (cboChargermode.Text != "")
            {
                chargermode = cboChargermode.GetDataValue().ToString();
            }
            //nSeq = 1;

            //Data
            chargerID = popupChargerID.GetValue().ToString();               //담당자ID
            receiver = popupReceiver.GetValue().ToString();                 //수신자
            if (cboisapproval.Text != "")
            {
                isApproval = cboisapproval.GetDataValue().ToString();           //승인여부
            }
            

            if (serialNo == null || serialNo == "")
            {
                result = 3;
                this.ShowMessage("시리얼번호가 없습니다."); //시리얼번호가 없습니다.
                return result;
            }
            if (controlno == null || controlno == "")
            {
                result = 3;
                this.ShowMessage("관리번호가 없습니다."); //관리번호가 없습니다.
                return result;
            }
            if (repairscrapid == null || repairscrapid == "")
            {
                result = 3;
                this.ShowMessage("수리폐기ID가 없습니다."); //수리폐기ID가 없습니다.
                return result;
            }

            if (department == null || department == "")
            {
                result = 3;
                this.ShowMessage("부서코드가 없습니다."); //부서코드가 없습니다.
                return result;
            }

            if (chargerID == null || chargerID == "")
            {
                result = 3;
                this.ShowMessage("작성자가 없습니다."); //부서코드가 없습니다.
                return result;
            }

            if (chargermode == null || chargermode == "")
            {
                result = 3;
                this.ShowMessage("구분이 없습니다."); //부서코드가 없습니다.
                return result;
            }

            if (isApproval == null || isApproval == "")
            {
                result = 3;
                this.ShowMessage("승인구분이 없습니다."); //부서코드가 없습니다.
                return result;
            }

            if (receiver == null || receiver == "")
            {
                result = 3;
                this.ShowMessage("수신자가 없습니다."); //부서코드가 없습니다.
                return result;
            }

            //PK
            dataRow["SERIALNO"] = serialNo;               //시리얼번호 PK
            dataRow["CONTROLNO"] = controlno;             //관리번호 PK
            dataRow["REPAIRSCRAPID"] = repairscrapid;     //수리폐기ID PK
            dataRow["DEPARTMENT"] = department;           //요청부서
            dataRow["CHARGERMODE"] = chargermode;         //담당자구분
            //순번
            nSeq = SearchMeasuringRepairApprovalSeq(serialNo, controlno, repairscrapid, department, chargermode);
            dataRow["SEQ"] = nSeq;                        

            //Data
            dataRow["STATUS"] = receiver;                //상태 (승인자)
            dataRow["CHARGERID"] = chargerID;            //담당자ID
            dataRow["ISAPPROVAL"] = isApproval;          //승인여부

            //결제시간
            DateTime approvalTime = DateTime.Now;
            if (approvalTime != null)
            {
                dataRow["APPROVALTIME"] = approvalTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            dataRow["REJECTCOMMENTS"] = this.txtRejectComments.Text;                //반려사유
            dataRow["DESCRIPTION"] = memoEmail.EditValue;   //설명
            //dataRow["CREATOR           "] = this.txtRejectComments.Text;   //생성자
            //dataRow["CREATEDTIME       "] = this.txtRejectComments.Text;   //생성일
            //dataRow["MODIFIER          "] = this.txtRejectComments.Text;   //수정자
            //dataRow["MODIFIEDTIME      "] = this.txtRejectComments.Text;   //수정일
            //dataRow["LASTTXNHISTKEY    "] = this.txtRejectComments.Text;   //마지막TXN HistKey
            //dataRow["LASTTXNID         "] = this.txtRejectComments.Text;   //마지막TXN ID
            //dataRow["LASTTXNUSER       "] = this.txtRejectComments.Text;   //마지막TXN 사용자
            //dataRow["LASTTXNTIME       "] = this.txtRejectComments.Text;   //마지막TXN 시간
            //dataRow["LASTTXNCOMMENT    "] = this.txtRejectComments.Text;   //마지막TXN 코멘트
            dataRow["VALIDSTATE"] = "Y";   //유효여부

            data.Rows.Add(dataRow);
            result = 0;
            return result;
        }
        /// <summary>
        /// 계측기 결제 정보 - DataTable 구성
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable CreateDataTableApproval(string tableName = "measuringinstRepairApproval")
        {
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            //Filed
            dt.Columns.Add("SERIALNO", typeof(string));
            dt.Columns.Add("CONTROLNO", typeof(string));
            dt.Columns.Add("REPAIRSCRAPID", typeof(string));
            dt.Columns.Add("DEPARTMENT", typeof(string));
            dt.Columns.Add("CHARGERMODE", typeof(string));
            dt.Columns.Add("SEQ", typeof(int));
            dt.Columns.Add("STATUS", typeof(string));
            dt.Columns.Add("CHARGERID", typeof(string));
            dt.Columns.Add("ISAPPROVAL", typeof(string));
            dt.Columns.Add("APPROVALTIME", typeof(DateTime));
            dt.Columns.Add("REJECTCOMMENTS", typeof(string));
            dt.Columns.Add("DESCRIPTION", typeof(string));
            //dt.Columns.Add("CREATOR", typeof(string));
            //dt.Columns.Add("CREATEDTIME", typeof(string));
            //dt.Columns.Add("MODIFIER", typeof(string));
            //dt.Columns.Add("MODIFIEDTIME", typeof(string));
            //dt.Columns.Add("LASTTXNHISTKEY", typeof(string));
            //dt.Columns.Add("LASTTXNID", typeof(string));
            //dt.Columns.Add("LASTTXNUSER", typeof(string));
            //dt.Columns.Add("LASTTXNTIME", typeof(string));
            //dt.Columns.Add("LASTTXNCOMMENT", typeof(string));
            dt.Columns.Add("VALIDSTATE", typeof(string));

            return dt;
        }


        /// <summary>
        /// 결정정보 SEQ 순번 자료 조회
        /// </summary>
        private int SearchMeasuringRepairApprovalSeq(string serialNo, string controlno, string repairscrapid, string department, string chargermode)
        {
            int result = 1;

            try
            {

                Dictionary<string, object> param = new Dictionary<string, object>();
                //param.Add("P_PLANTID", plantID);
                param.Add("P_SERIALNO", serialNo);
                param.Add("P_CONTROLNO", controlno);
                param.Add("P_REPAIRSCRAPID", repairscrapid);
                param.Add("P_DEPARTMENT", department);
                param.Add("P_CHARGERMODE", chargermode);

                //SqlQuery rnrUserList = new SqlQuery("GetAreaidListByCsm", "10001", $"P_PLANTID={UserInfo.Current.Plant}", $"P_AREANAME={UserInfo.Current.Area}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
                SqlQuery approvalList = new SqlQuery("SelectRawMeasuringRepairApproveDirect", "10001", param);
                DataTable approvalTable = approvalList.Execute();
                if (approvalTable != null && approvalTable.Rows.Count > 0)
                {
                    result = approvalTable.Rows[0][0].ToSafeInt32();
                    result += 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return result;
        }
        #endregion

        #region Popup

        /// <summary>
        /// 작성자 선택 팝업
        /// </summary>
        private void selectUserPopup()
        {
            var userPopup = this.CreateSelectPopup("USERID", new SqlQuery("GetUserList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(0)
                .SetPopupLayoutForm(500, 600, System.Windows.Forms.FormBorderStyle.FixedToolWindow);

            //userPopup.SetPopupResultCount(1);
            userPopup.Conditions.AddTextBox("USERIDNAME");

            userPopup.GridColumns.AddTextBoxColumn("USERID", 120);
            userPopup.GridColumns.AddTextBoxColumn("USERNAME", 120);
            userPopup.GridColumns.AddTextBoxColumn("EMAILADDRESS", 250); 
            userPopup.GridColumns.AddTextBoxColumn("CELLPHONENUMBER", 150);


            DataTable mappingTable = grdUserList.DataSource as DataTable;
            var filter = (grdUserList.View.DataSource as DataView).RowStateFilter;

            IEnumerable<DataRow> selectedDatas = this.ShowPopup(userPopup, mappingTable.Rows.Cast<DataRow>()
                                                                                            .Where(m => !grdUserList.View.IsDeletedRow(m)));

            if (selectedDatas == null)
            {
                return;
            }
            else
            {
                if (_mappingDataSource.Columns.Count == 0) // 최초 검색했을때 변수에 담아두어 유지하고 있어야함.
                {
                    _mappingDataSource.Columns.Add("USERID", typeof(string));
                    _mappingDataSource.Columns.Add("USERNAME", typeof(string));
                    _mappingDataSource.Columns.Add("EMAILADDRESS", typeof(string));
                    _mappingDataSource.Columns.Add("CELLPHONENUMBER", typeof(string));
                }

                DataTable mdt = (grdUserList.DataSource as DataTable).Clone();
                
                foreach (DataRow row in selectedDatas)
                {
                    mdt.ImportRow(row);
                }

               (grdUserList.DataSource as DataTable).Merge(mdt, true, MissingSchemaAction.Ignore);
                grdUserList.DataSource = (grdUserList.DataSource as DataTable).DefaultView.ToTable(true);

                // DataTable의 RowStatus 즉 추가, 삭제, 수정 형태를 유지해주는 메소드
                // 원래것과 매핑결과를 비교하여 새로운 DataSource를 생성해 준다
                //grdUserList.SetDataSourceRemainRowStatus(DataSourceHelper.MappingChanged(_mappingDataSource, selectedDatas, new List<string>() { "USERID" }));
            }
        }
        /// <summary>
        /// 작성자 선택 팝업
        /// </summary>
        private ConditionItemSelectPopup UserChargerPopup()
        {
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();

            popup = this.CreateSelectPopup("USERID", new SqlQuery("GetUserList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(0)
                .SetPopupLayoutForm(500, 600, System.Windows.Forms.FormBorderStyle.FixedToolWindow);
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "USERNAME";
            popup.ValueFieldName = "USERID";

            popup.SetPopupResultCount(1);
            popup.Conditions.AddTextBox("USERIDNAME");

            popup.GridColumns.AddTextBoxColumn("USERID", 120);
            popup.GridColumns.AddTextBoxColumn("USERNAME", 120);
            popup.GridColumns.AddTextBoxColumn("EMAILADDRESS", 250);
            popup.GridColumns.AddTextBoxColumn("CELLPHONENUMBER", 150);

            //DataTable mappingTable = grdUserList.DataSource as DataTable;
            //var filter = (grdUserList.View.DataSource as DataView).RowStateFilter;

            //popup.SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
            //{
            //    foreach (DataRow row in selectedRow)
            //    {
            //        if (selectedRow.Count() > 0)
            //        {
            //            DataRow gridRow = mappingTable.NewRow();
            //            gridRow["USERID"] = row["USERID"];
            //            gridRow["USERNAME"] = row["USERNAME"];
            //            gridRow["EMAILADDRESS"] = row["EMAILADDRESS"];
            //            gridRow["CELLPHONENUMBER"] = row["CELLPHONENUMBER"];
            //            mappingTable.Rows.Add(gridRow);
            //            //        _mappingDataSource.Columns.Add("USERID", typeof(string));
            //            //        _mappingDataSource.Columns.Add("USERNAME", typeof(string));
            //            //        _mappingDataSource.Columns.Add("EMAILADDRESS", typeof(string));
            //            //        _mappingDataSource.Columns.Add("CELLPHONENUMBER", typeof(string));
            //        }
            //    }
            //});



            return popup;
        }
        /// <summary>
        /// 수신자 선택 팝업
        /// </summary>
        private ConditionItemSelectPopup UserReceiverPopup()
        {
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();

            popup = this.CreateSelectPopup("USERID", new SqlQuery("GetUserList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(0)
                .SetPopupLayoutForm(500, 600, System.Windows.Forms.FormBorderStyle.FixedToolWindow);
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "USERNAME";
            popup.ValueFieldName = "USERID";

            popup.SetPopupResultCount(1);
            popup.Conditions.AddTextBox("USERIDNAME");

            popup.GridColumns.AddTextBoxColumn("USERID", 120);
            popup.GridColumns.AddTextBoxColumn("USERNAME", 120);
            popup.GridColumns.AddTextBoxColumn("EMAILADDRESS", 250);
            popup.GridColumns.AddTextBoxColumn("CELLPHONENUMBER", 150);

            DataTable mappingTable = grdUserList.DataSource as DataTable;
            var filter = (grdUserList.View.DataSource as DataView).RowStateFilter;

            popup.SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
            {
                foreach (DataRow row in selectedRow)
                {
                    if (selectedRow.Count() > 0)
                    {
                        grdUserList.View.ClearDatas();

                        DataRow gridRow = mappingTable.NewRow();
                        gridRow["USERID"] = row["USERID"];
                        gridRow["USERNAME"] = row["USERNAME"];
                        gridRow["EMAILADDRESS"] = row["EMAILADDRESS"];
                        gridRow["CELLPHONENUMBER"] = row["CELLPHONENUMBER"];
                        mappingTable.Rows.Add(gridRow);
                        
                        //grdUserList.DataSource = mappingTable;
                        //        _mappingDataSource.Columns.Add("USERID", typeof(string));
                        //        _mappingDataSource.Columns.Add("USERNAME", typeof(string));
                        //        _mappingDataSource.Columns.Add("EMAILADDRESS", typeof(string));
                        //        _mappingDataSource.Columns.Add("CELLPHONENUMBER", typeof(string));
                    }
                }
            });




            //IEnumerable<DataRow> selectedDatas = this.ShowPopup(popup, mappingTable.Rows.Cast<DataRow>()
            //                                                                                .Where(m => !grdUserList.View.IsDeletedRow(m)));

            //if (selectedDatas != null)
            //{
            //    if (_mappingDataSource.Columns.Count == 0) // 최초 검색했을때 변수에 담아두어 유지하고 있어야함.
            //    {
            //        _mappingDataSource.Columns.Add("USERID", typeof(string));
            //        _mappingDataSource.Columns.Add("USERNAME", typeof(string));
            //        _mappingDataSource.Columns.Add("EMAILADDRESS", typeof(string));
            //        _mappingDataSource.Columns.Add("CELLPHONENUMBER", typeof(string));
            //    }

            //    DataTable mdt = (grdUserList.DataSource as DataTable).Clone();

            //    foreach (DataRow row in selectedDatas)
            //    {
            //        mdt.ImportRow(row);
            //    }

            //   (grdUserList.DataSource as DataTable).Merge(mdt, true, MissingSchemaAction.Ignore);
            //    grdUserList.DataSource = (grdUserList.DataSource as DataTable).DefaultView.ToTable(true);

            //    // DataTable의 RowStatus 즉 추가, 삭제, 수정 형태를 유지해주는 메소드
            //    // 원래것과 매핑결과를 비교하여 새로운 DataSource를 생성해 준다
            //    //grdUserList.SetDataSourceRemainRowStatus(DataSourceHelper.MappingChanged(_mappingDataSource, selectedDatas, new List<string>() { "USERID" }));
            //}
            //else
            //{
            //    grdUserList = null;
            //}

            return popup;
        }

        /// <summary>
        /// 부서정보
        /// </summary>
        /// <returns></returns>
        private ConditionItemSelectPopup DepartmentInfoPopup()
        {
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();

            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            popup.SetPopupLayoutForm(300, 600, FormBorderStyle.FixedDialog);
            //popup.SetPopupLayout(Language.Get("DEPARTMENTNAME"), PopupButtonStyles.Ok_Cancel, true, false);
            popup.SetPopupLayout("DEPARTMENTINFO", PopupButtonStyles.Ok_Cancel, true, false);
            popup.Id = "DEPARTMENTNAME";
            popup.SearchQuery = new SqlQuery("GetMeasuringSearchDepartment", "10001", param);
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "DEPARTMENTNAME";
            popup.ValueFieldName = "DEPARTMENTNAME";

            popup.GridColumns.AddTextBoxColumn("DEPARTMENTNAME", 150)
                .SetLabel("DEPARTMENTNAME");
            //popup.GridColumns.AddTextBoxColumn("DEPARTMENTNAME", 150)
            //    .SetLabel("DEPARTMENTNAME");

            return popup;
        }

        #endregion

        private void btnAdd_Click_1(object sender, EventArgs e)
        {

        }

        private void popupReceiver_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Clear)
            {
                this.grdUserList.View.ClearDatas();
            }
        }


    }
}
