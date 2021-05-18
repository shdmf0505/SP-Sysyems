#region using

using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
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

namespace Micube.SmartMES.QualityAnalysis.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 약품관리 > 약품분석 이상발생 > CAR 요청팝업
    /// 업  무  설  명  : 
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-05-31
    /// 수  정  이  력  : 2019-08-06 강유라
    /// 
    /// 
    /// </summary>
    public partial class ChemicalissuePopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }
        private DateTime fromWorktime, toWorkTime;//affectLot 조회파라미터
        private DataTable _changedAffectLot;//저장 할 affectLot
        /// <summary>
        /// 팝업진입전 그리드의 컨트롤
        /// </summary>
        public Chemicalissue ParentControl { get; set; }

        /// <summary>
        /// SmartBaseForm을 사용하기 위한 객체
        /// </summary>
        public SmartBaseForm _form = new SmartBaseForm();

        /// <summary>
        /// 팝업진입전 약품분석 키정보
        /// </summary>
        public DataTable _chemicalDetailTable;

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public ChemicalissuePopup(DataRow row)
        {
            InitializeComponent();

            InitializeEvent();

            CurrentDataRow = row;

            InitializeCurrentData();

            issueRegistrationControl.isShowReasonCombo = false;
            issueRegistrationControl.SetControlsVisible();         
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 그리드에서 선택한 행에 대한 데이터바인딩
        /// </summary>
        private void InitializeCurrentData()
        {
            gbxIssueInformation.GridButtonItem = GridButtonItem.None;

            txtAnalysisDate.EditValue = CurrentDataRow["ANALYSISDATE"]; // 분석일
            txtEquipmentName.EditValue = CurrentDataRow["EQUIPMENTNAME"]; // 설비명
            txtChemicalLevel.EditValue = CurrentDataRow["CHEMICALLEVEL"]; // 약품등급
            txtDegree.EditValue = CurrentDataRow["DEGREE"]; // 차수
            txtChildEquipmentName.EditValue = CurrentDataRow["CHILDEQUIPMENTNAME"]; // 설비단명
            txtManagementScope.EditValue = CurrentDataRow["SPECSCOPE"]; // 규격범위
            txtProcesssegmentclassName.EditValue = CurrentDataRow["PROCESSSEGMENTCLASSNAME"]; // 대공정명
            txtChemicalName.EditValue = CurrentDataRow["CHEMICALNAME"]; // 약품
            txtAnalysisValue.EditValue = CurrentDataRow["ANALYSISVALUE"]; // 분석치
            // 2020.02.11-유석진(ANALYSISVALUE(분석치) EditMask 추가)
            txtAnalysisValue.Properties.Mask.EditMask = "#,###,##0.000";
            txtAnalysisValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtAnalysisValue.Properties.Mask.UseMaskAsDisplayFormat = true;

            txtReasonCodeId.EditValue = CurrentDataRow["REASONCODENAME"];//이상발생 사유

            //issueRegistrationControl._abnormalNumber = CurrentDataRow["ABNOCRNO"].ToString();
            //issueRegistrationControl._abnormalType = CurrentDataRow["ABNOCRTYPE"].ToString();

            issueRegistrationControl.ParentDataRow = CurrentDataRow;
            issueRegistrationControl._state = CurrentDataRow["STATE"].ToString();
        }

        /// <summary>
        /// 약품분석 키 정보 저장
        /// </summary>
        private void InitializeDataTable()
        {
            _chemicalDetailTable = new DataTable();
            _chemicalDetailTable.TableName = "chemicalInfo";

            _chemicalDetailTable.Columns.Add("ABNOCRNO");
            _chemicalDetailTable.Columns.Add("ABNOCRTYPE");
            _chemicalDetailTable.Columns.Add("PROCESSSEGMENTCLASSID"); 
            _chemicalDetailTable.Columns.Add("EQUIPMENTID");
            _chemicalDetailTable.Columns.Add("CHILDEQUIPMENTID");
            _chemicalDetailTable.Columns.Add("CHEMICALSEQUENCE");
            _chemicalDetailTable.Columns.Add("ANALYSISTYPE");
            _chemicalDetailTable.Columns.Add("INSPITEMID");
            _chemicalDetailTable.Columns.Add("PMTYPE");
            _chemicalDetailTable.Columns.Add("REANALYSISTYPE");
            _chemicalDetailTable.Columns.Add("DEGREE");
            _chemicalDetailTable.Columns.Add("ANALYSISDATE");

            DataRow row;
            row = _chemicalDetailTable.NewRow();

            row["ABNOCRNO"] = CurrentDataRow["ABNOCRNO"];
            row["ABNOCRTYPE"] = CurrentDataRow["ABNOCRTYPE"];
            row["PROCESSSEGMENTCLASSID"] = CurrentDataRow["PROCESSSEGMENTCLASSID"];
            row["EQUIPMENTID"] = CurrentDataRow["EQUIPMENTID"];
            row["CHILDEQUIPMENTID"] = CurrentDataRow["CHILDEQUIPMENTID"];
            row["CHEMICALSEQUENCE"] = CurrentDataRow["SEQUENCE"];
            row["ANALYSISTYPE"] = CurrentDataRow["ANALYSISTYPE"];
            row["INSPITEMID"] = CurrentDataRow["INSPITEMID"];
            row["PMTYPE"] = CurrentDataRow["PMTYPE"];
            row["REANALYSISTYPE"] = CurrentDataRow["REANALYSISTYPEID"];
            row["DEGREE"] = CurrentDataRow["DEGREE"];
            row["ANALYSISDATE"] = CurrentDataRow["ANALYSISDATE"];

            _chemicalDetailTable.Rows.Add(row);
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += ChemicalissuePopup_Load;

            btnSave.Click += BtnSave_Click;
            btnClose.Click += BtnClose_Click;
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

        /// <summary>
        /// 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            // 작업장 권한 체크
            CheckAuthorityArea(CurrentDataRow);

            // 임시저장 했는지 체크
            switch (issueRegistrationControl._tabIndex)
            {
                case 0:
                    CheckTempData(0, Format.GetString(issueRegistrationControl.cboRequestNumber.EditValue), issueRegistrationControl._requestDt);
                    break;
                case 1:
                    CheckTempData(1, Format.GetString(issueRegistrationControl.cboReceiptNumber.EditValue), issueRegistrationControl._receiptDt);
                    break;
                case 2:
                    CheckTempData(2, Format.GetString(issueRegistrationControl.cboAcceptNumber.EditValue), issueRegistrationControl._acceptDt);
                    break;
                case 3:
                    CheckTempData(3, Format.GetString(issueRegistrationControl.cboValidationNumber.EditValue), issueRegistrationControl._validationDt);
                    break;
            }

            if (this.ShowMessageBox("InfoPopupSave", "Caption", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                // 저장할 데이터가 있는지 체크 후 저장
                CheckDataAndSaveCarAllState(issueRegistrationControl);
                this.Close();
                ParentControl.Search();
            }
        }

        /// <summary>
        /// Form Road시 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChemicalissuePopup_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;

            InitializeDataTable();

            //InitializeComboBox();

            issueRegistrationControl._queryId = "GetConcurrenceCount";
            issueRegistrationControl._queryVersion = "10001";

            // 상태값이 없다면 요청탭의 담당자 기본세팅
            //if (CurrentDataRow["STATE"].Equals("*"))
            //{
            //    issueRegistrationControl.popupManager.SetValue(UserInfo.Current.Id);
            //    issueRegistrationControl.popupManager.Text = UserInfo.Current.Name;
            //}
            //// 상태값이 요청완료라면 접수탭의 담당자 기본세팅
            //else if (CurrentDataRow["STATE"].Equals("CARRequestCompleted"))
            //{
            //    issueRegistrationControl.popupReceiptor.SetValue(UserInfo.Current.Id);
            //    issueRegistrationControl.popupReceiptor.Text = UserInfo.Current.Name;
            //}

            ncrProgressControl.CurrentDataRow = CurrentDataRow;
        }

        #endregion

        #region 저장

        /// <summary>
        /// CAR 요청 저장
        /// </summary>
        private void SaveCarRequest()
        { 
            DataSet ds = new DataSet();

            ds.Tables.Add(issueRegistrationControl.RequestCarData());
            ds.Tables.Add(_chemicalDetailTable.Copy());
            ds.Tables.Add(_changedAffectLot);

            ExecuteRule("SaveCarRequest", ds);                     
        }

        /// <summary>
        /// CAR 접수 저장
        /// </summary>
        private void SaveCarReceipt()
        {
            DataSet ds = issueRegistrationControl.ReceiptCarData();        
            ds.Tables.Add(_changedAffectLot);

            List<object> list = new List<object>();

            foreach (DataTable dt in ds.Tables)
            {
                if (dt.TableName.Equals("list"))
                {
                    if (dt.AsEnumerable().Select(r => r["SEQUENCE"]).Distinct().ToList().Count != 0)
                    {
                        list = dt.AsEnumerable().Select(r => r["SEQUENCE"]).Distinct().ToList();
                    }
                }

                if (dt.TableName.Equals("receiptFileList"))
                {
                    foreach (object sequence in list)
                    {
                        if (dt.AsEnumerable().Where(r => r["NUMBER"].Equals(sequence)).Count() == 0)
                        {
                            throw MessageException.Create("FileIsRequired"); // 파일은 필수등록입니다.
                        }
                    }
                }
            }

            ExecuteRule("SaveCarReceipt", ds);     
        }

        /// <summary>
        /// CAR 승인 저장
        /// </summary>
        private void SaveCarAccept()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(issueRegistrationControl.AcceptCarData());
            ds.Tables.Add(_changedAffectLot);

            ExecuteRule("SaveCarAccept", ds);
        }

        /// <summary>
        /// 유효성평가 저장
        /// </summary>
        private void SaveCarValidation()
        {
            DataSet ds = issueRegistrationControl.ValidationCarData();
            ds.Tables.Add(_changedAffectLot);

            ExecuteRule("SaveCarValidation", ds);
        }

        #endregion

        #region 검색

        #endregion

        #region Private Function

        /// <summary>
        /// 해당 순번에 임시저장된 데이터가 없으면 Exception
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="tempDt"></param>
        private void CheckTempData(int tapIndex, string sequence, DataTable tempDt)
        {
            if (issueRegistrationControl.IsInputData(tapIndex))
            {
                if (!issueRegistrationControl.CheckIsTempSaved(sequence, tempDt))
                {
                    throw MessageException.Create("NeedToSaveTemp");//임시저장을 해주세요.
                }
            }
        }

        /// <summary>
        /// 임시저장 된 데이터가 있는지 확인하고 NCR 임시저장건을 저장하는 함수(요청, 접수, 승인, 유효성, AffectLot)
        /// </summary>
        /// <param name="con"></param>
        private void CheckDataAndSaveCarAllState(IssueRegistrationControl con)
        {
            _changedAffectLot = ncrProgressControl.GetChangedData();
            _changedAffectLot = SaveAffectLot(_changedAffectLot);

            DataTable dt1 = con._requestDt; // 요청
            DataTable dt2 = con._receiptDt; // 접수
            DataTable dt3 = con._receiptFileDt; // 접수파일
            DataTable dt4 = con._acceptDt; // 승인
            DataTable dt5 = con._validationDt; // 유효성
            DataTable dt6 = con._validationFileDt; // 유효성파일
            DataTable dt7 = _changedAffectLot; // AffectLot

            int saveRowCount = dt1.Rows.Count + dt2.Rows.Count + dt3.Rows.Count
                             + dt4.Rows.Count + dt5.Rows.Count + dt6.Rows.Count + dt7.Rows.Count;

            // 저장할 데이터가 없다면 Exception
            if (saveRowCount == 0) throw MessageException.Create("NoSaveData"); // 저장할 데이터가 없습니다.

            // 임시저장된 접수테이블의 순번을 저장
            List<object> list = new List<object>();
            list = dt2.AsEnumerable().Select(r => r["SEQUENCE"]).Distinct().ToList();

            // 해당 순번에 임시저장된 접수테이블이 있지만 파일이 등록되지 않았다면 Exception
            if (list.Count > 0)
            {
                foreach (object sequence in list)
                {
                    if (dt3.AsEnumerable().Where(r => r["NUMBER"].Equals(sequence)).Count() == 0)
                    {
                        throw MessageException.Create("FileIsRequired"); // 파일은 필수등록입니다.
                    }
                }
            }

            // 파일 서버 업로드
            if (dt3.Rows.Count > 0) con.SaveReceiptFile();
            if (dt6.Rows.Count > 0) con.SaveValidationFile();

            MessageWorker worker = new MessageWorker("SaveCarAllState");
            worker.SetBody(new MessageBody()
            {
                { "request", dt1 },
                { "receipt", dt2 },
                { "receiptFile", dt3 },
                { "accept", dt4 },
                { "validation", dt5 },
                { "validationFile", dt6 },
                { "affectLotList", dt7 },
            });

            worker.Execute();
            //var result = worker.Execute<DataTable>();
            //var resultData = result.GetResultSet();
        }

        /// <summary>
        /// Affect Lot을 설정 할 시간을 구하는 함수(자동으로 AffectLot대상 조회파라미터)
        /// </summary>
        private void GetAffectLotTime()
        { 
            //이상발생 항목에 등록 된 회차 정보를 한 row로 (BEFORETIMECYCLE,TIMECYCLE) 조회하는 쿼리
            Dictionary<string, object> values = new Dictionary<string, object>
            {//***파라미터설정 필요
                {"INSPECTIONCLASSID",CurrentDataRow["ANALYSISTYPE"]},
                {"INSPECTIONDEFID",CurrentDataRow["ANALYSISTYPE"].ToString()},
                {"INSPITEMCLASSID",CurrentDataRow["CHILDEQUIPMENTID"]},
                {"INSPITEMID",CurrentDataRow["INSPITEMID"]},
                {"PLANTID",CurrentDataRow["PLANTID"]},
                {"ENTERPRISEID",CurrentDataRow["ENTERPRISEID"]}
            };

            DataTable cycleTable = SqlExecuter.Query("SelectNGCycleTime", "10001", values);

            foreach (DataRow row in cycleTable.Rows)
            {//이상발생 회차row 일때
                if (row["CYCLESEQUENCE"].ToString().Equals(CurrentDataRow["DEGREE"].ToString()))
                {
                    toWorkTime = Convert.ToDateTime(CurrentDataRow["ANALYSISDATE"] + " " + row["TIMECYCLE"].ToString()).AddMinutes(-1);

                    if (string.IsNullOrWhiteSpace(row["BEFORETIMECYCLE"].ToString()))
                    {//BEFORETIMECYCLE가 없을 때 = 첫번째 회차에서 이상 발생시 (마지막 row의 TIMECYCLE을 fromWorkTime으로 설정)
                        fromWorktime = Convert.ToDateTime(CurrentDataRow["ANALYSISDATE"] + " " + cycleTable.Rows[cycleTable.Rows.Count - 1]["TIMECYCLE"].ToString());
                        if (fromWorktime > toWorkTime)
                        {//마지막 row의 측정시간이 첫번째 회차보다 늦은시간을때 
                         //ex) 1회차 오전 8:00...12회차 23:30 -> 8-11 23:30 ~ 8-12 8:00
                            fromWorktime = Convert.ToDateTime(CurrentDataRow["ANALYSISDATE"] + " " + cycleTable.Rows[cycleTable.Rows.Count - 1]["TIMECYCLE"].ToString()).AddDays(-1);
                        }                
                    }
                    else
                    {
                        fromWorktime = Convert.ToDateTime(CurrentDataRow["ANALYSISDATE"] +" "+ row["BEFORETIMECYCLE"].ToString());
                    }

                    break;
                }
            }
        }

        /// <summary>
        /// 자동으로 AffectLot대상 Lot을 조회하는 함수
        /// </summary>
          public void SearchAutoAffectLot()
        {
            GetAffectLotTime();

            Dictionary<string, object> values = new Dictionary<string, object>
            {//***파라미터설정 필요
                {"ABNOCRNO", CurrentDataRow["ABNOCRNO"] },
                {"ABNOCRTYPE", CurrentDataRow["ABNOCRTYPE"]},
                {"EQUIPMENTID",CurrentDataRow["EQUIPMENTID"] },
                {"ENTERPRISEID",Framework.UserInfo.Current.Enterprise},
                {"PLANTID",Framework.UserInfo.Current.Plant},
                {"LANGUAGETYPE",Framework.UserInfo.Current.LanguageType},
                {"FROMWORKTIME", fromWorktime},
                {"TOWORKTIME", toWorkTime}
            };

            //DataTable autoAffectLot = SqlExecuter.Query("SelecToAffectLotCycleTime", "10001", values);
            DataTable autoAffectLot = SqlExecuter.Query("SelectAffectLotInspAbnormalPopup", "10001", values);

            ncrProgressControl.SetDataGrd(autoAffectLot);
        }

        /// <summary>
        /// affectLot 저장시 add || modified 설정 함수
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        private DataTable SaveAffectLot(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                if (row["ISADDED"].ToString().Equals("Y"))
                {
                    row["_STATE_"] = "added";
                    row["ABNOCRNO"] = CurrentDataRow["ABNOCRNO"];
                    row["ABNOCRTYPE"] = CurrentDataRow["ABNOCRTYPE"];
                }
            }
            return dataTable;
        }

        /// <summary>
        /// 로그인한 사용자가 해당 작업장에 대한 수정권한이 있는지 체크 (Row 단위)
        /// </summary>
        /// <param name="dt"></param>
        private void CheckAuthorityArea(DataRow row)
        {
            if (row["ISMODIFY"].Equals("N"))
            {
                string area = Format.GetString(row["AREANAME"]);
                throw MessageException.Create("NoMatchingAreaUser", area);
            }
        }

        #endregion

        #region Global Function

        #endregion
    }

}
