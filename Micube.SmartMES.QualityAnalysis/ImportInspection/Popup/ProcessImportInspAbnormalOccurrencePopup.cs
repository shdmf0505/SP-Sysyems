using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.Net.Data;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 수입 검사 관리 > 공정 수입검사 이상발생 팝업
    /// 업  무  설  명  : 공정 수입검사 이상발생 등록하는 팝업
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-10-26
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ProcessImportInspAbnormalOccurrencePopup : SmartPopupBaseForm, ISmartCustomPopup
    {

        #region Interface

        public DataRow CurrentDataRow { get; set; }
        
        #endregion

        #region Local Variables
        private DataTable _changedAffectLot;//저장 할 affectLot
        private DataTable _claimInfo;//저장 할 claim 정보
        private DataTable _selectMeasureValueDt;
        public bool isEnable = true;
        #endregion

        #region 생성자
        public ProcessImportInspAbnormalOccurrencePopup(DataRow row)
        {
            InitializeComponent();

            CurrentDataRow = row;
            InitializeEvent();
            InitializeGrid();
            InitializeControl();
            //ucLotInfo Setting
            SettingLotgrd();
            issueRegistrationControl.isShowReasonCombo = true;
            issueRegistrationControl.SetControlsVisible();
            SetStandardDataToUserControl();

        }
        #endregion

        #region 컨텐츠 영역 초기화
        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            #region 외관검사 탭 초기화
            grdInspectionItem.GridButtonItem = GridButtonItem.None;
            grdInspectionItem.View.SetIsReadOnly();

            //구분 등 검사항목
            var item = grdInspectionItem.View.AddGroupColumn("");

            item.AddTextBoxColumn("DEFECTCODE", 250)
                .SetIsHidden();

            item.AddTextBoxColumn("DEFECTCODENAME", 150)
                .SetIsReadOnly();

            //검사 수량
            var groupInspQty = grdInspectionItem.View.AddGroupColumn("INSPECTIONQTY");
            //PCS
            groupInspQty.AddSpinEditColumn("INSPECTIONQTY", 100)
                .SetTextAlignment(TextAlignment.Right)
                .SetLabel("PCS");

            //PNL
            groupInspQty.AddSpinEditColumn("INSPECTIONQTYPNL", 100)
                .SetTextAlignment(TextAlignment.Right)
                .SetLabel("PNL");


            //불량수량
            var groupSpecOutQty = grdInspectionItem.View.AddGroupColumn("DEFECTQTY");
            //PCS
            groupSpecOutQty.AddSpinEditColumn("DEFECTQTY", 100)
                .SetTextAlignment(TextAlignment.Right)
                .SetLabel("PCS");
            //PNL
            groupSpecOutQty.AddSpinEditColumn("DEFECTQTYPNL", 100)
                .SetTextAlignment(TextAlignment.Right)
                .SetLabel("PNL");

            //결과
            var result = grdInspectionItem.View.AddGroupColumn("");

            result.AddTextBoxColumn("DEFECTRATE", 100)
                .SetIsReadOnly()
               .SetDisplayFormat("###.#", MaskTypes.Numeric);

            result.AddComboBoxColumn("INSPECTIONRESULT", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OKNG", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();

            result.AddTextBoxColumn("TXNHISTKEY", 300)
                .SetIsHidden();

            result.AddTextBoxColumn("RESOURCEID", 300)
                .SetIsHidden();

            result.AddTextBoxColumn("RESOURCETYPE", 300)
                .SetIsHidden();

            result.AddTextBoxColumn("PROCESSRELNO", 300)
                .SetIsHidden();

            grdInspectionItem.View.PopulateColumns();
            #endregion

            #region 측정검사 탭 초기화

            #region 검사 항목 그리드 초기화

            grdInspectionItemSpec.GridButtonItem = GridButtonItem.None;
            grdInspectionItemSpec.View.SetIsReadOnly();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMID", 200)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMNAME", 150);

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPECTIONSTANDARD", 250);

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMVERSION", 200)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMTYPE", 200)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("USL", 200)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("LSL", 200)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("LCL", 200)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("UCL", 200)
                .SetIsHidden();

            grdInspectionItemSpec.View.PopulateColumns();

            #endregion

            #region 측정값 그리드 초기화

            grdMeasuredValue.GridButtonItem = GridButtonItem.None;
            grdMeasuredValue.View.SetIsReadOnly();

            grdMeasuredValue.View.AddTextBoxColumn("INSPITEMID", 200)
                .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("INSPITEMVERSION", 200)
                .SetIsHidden();

            grdMeasuredValue.View.AddSpinEditColumn("MEASUREVALUE", 100)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,0.######", MaskTypes.Numeric);

            grdMeasuredValue.View.AddComboBoxColumn("INSPECTIONRESULT", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OKNG", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();

            grdMeasuredValue.View.AddTextBoxColumn("TXNHISTKEY", 300)
                .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("RESOURCEID", 300)
                .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("RESOURCETYPE", 300)
                .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("PROCESSRELNO", 300)
                .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("DEGREE", 300)
                .SetIsHidden();

            grdMeasuredValue.View.PopulateColumns();

            #endregion

            #endregion

            #region 불량코드 탭 초기화

            grdDefect.GridButtonItem = GridButtonItem.None;
            grdDefect.View.SetIsReadOnly();

            //구분 등 검사항목
            var standardItem = grdDefect.View.AddGroupColumn("");

            //LOT ID
            standardItem.AddTextBoxColumn("LOTID", 150)
                .SetIsHidden();

            //불량코드
            standardItem.AddTextBoxColumn("DEFECTCODE", 150)
                .SetIsHidden();

            //불량코드명
            standardItem.AddTextBoxColumn("DEFECTCODENAME", 150);

            //품질공정
            standardItem.AddTextBoxColumn("QCSEGMENTID", 100)
                .SetIsHidden();

            standardItem.AddTextBoxColumn("QCSEGMENTNAME", 250);

            //검사 수량
            var groupInspectionQty = grdDefect.View.AddGroupColumn("INSPECTIONQTY");
            //PCS
            groupInspectionQty.AddSpinEditColumn("INSPECTIONQTY", 150)
                .SetLabel("PCS")
                .SetDisplayFormat("#,##0", MaskTypes.Numeric, false)
                .SetTextAlignment(TextAlignment.Right);
            //PNL
            groupInspectionQty.AddSpinEditColumn("INSPECTIONQTYPNL", 150)
                .SetLabel("PNL")
                .SetDisplayFormat("#,##0", MaskTypes.Numeric, false)
                .SetTextAlignment(TextAlignment.Right);

            //불량수량
            var groupDefectQty = grdDefect.View.AddGroupColumn("SPECOUTQTY");
            //PCS
            groupDefectQty.AddSpinEditColumn("DEFECTQTY", 150)
                .SetLabel("PCS")
                .SetDisplayFormat("#,##0", MaskTypes.Numeric, false)
                .SetTextAlignment(TextAlignment.Right);

            //PNL
            groupDefectQty.AddSpinEditColumn("DEFECTQTYPNL", 150)
                .SetLabel("PNL")
                .SetDisplayFormat("#,##0", MaskTypes.Numeric, false)
                .SetTextAlignment(TextAlignment.Right);

            //결과
            var inspResult = grdDefect.View.AddGroupColumn("");

            //불량률
            inspResult.AddSpinEditColumn("DEFECTRATE", 80)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true)
                .SetTextAlignment(TextAlignment.Right);

            // 원인품목..
            var reason = grdDefect.View.AddGroupColumn("");
            reason.AddTextBoxColumn("REASONCONSUMABLEDEFNAME", 150)
                .SetLabel("REASONPRODUCTDEFNAME");

            reason.AddTextBoxColumn("REASONCONSUMABLEDEFVERSION", 150);

            //원인품목 -> 원인자재?
            reason.AddTextBoxColumn("REASONCONSUMABLELOTID", 150)
                .SetLabel("CAUSEMATERIALLOT");

            //원인공정
            reason.AddTextBoxColumn("REASONSEGMENTID", 150)
                .SetIsHidden();

            reason.AddTextBoxColumn("REASONSEGMENTNAME", 150);

            reason.AddTextBoxColumn("REASONAREANAME", 150);

            grdDefect.View.PopulateColumns();

            #endregion
        }

        /// <summary>
        /// 컨트롤 초기화
        /// </summary>
        public void InitializeControl()
        {
            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                {"LANGUAGETYPE", UserInfo.Current.LanguageType},
                { "CODECLASSID","InspectionDecisionClass"}
            };

            DataTable dt = SqlExecuter.Query("GetTypeList", "10001", values);

            cboStandardType.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboStandardType.Editor.ShowHeader = false;
            cboStandardType.Editor.DisplayMember = "CODENAME";
            cboStandardType.Editor.ValueMember = "CODEID";
            cboStandardType.Editor.UseEmptyItem = false;
            cboStandardType.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboStandardType.Editor.DataSource = dt;

            values.Remove("CODECLASSID");
            values.Add("CODECLASSID", "YesNo");

            DataTable dt2 = SqlExecuter.Query("GetTypeList", "10001", values);

            cboClaim.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboClaim.Editor.ShowHeader = false;
            cboClaim.Editor.DisplayMember = "CODENAME";
            cboClaim.Editor.ValueMember = "CODEID";
            cboClaim.Editor.UseEmptyItem = false;
            cboClaim.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboClaim.Editor.DataSource = dt2;
        }

        /// <summary>
        /// userControl 또는 control에 CurrentDataRow 할당 함수
        /// </summary>
        public void SetStandardDataToUserControl()
        {
            txtLotId.Text = CurrentDataRow["LOTID"].ToString();
            txtReasonCode.Text = CurrentDataRow["REASONCODENAME"].ToString();
            ncrProgressControl.CurrentDataRow = CurrentDataRow;

            txtReasonCode.Editor.ReadOnly = true;

            issueRegistrationControl._queryId = "GetConcurrenceCount";
            issueRegistrationControl._queryVersion = "10001";

            issueRegistrationControl.ParentDataRow = CurrentDataRow;
            issueRegistrationControl._state = CurrentDataRow["STATE"].ToString();

            cboStandardType.Editor.EditValue = CurrentDataRow["JUDGMENTCRITERIA"];
            cboClaim.Editor.EditValue = CurrentDataRow["ISCLAIMPROCESS"];
            txtOtherCost.Editor.EditValue = CurrentDataRow["AMOUNT"];
            if (CurrentDataRow["ISCLAIMPROCESS"] != null && !string.IsNullOrWhiteSpace(CurrentDataRow["ISCLAIMPROCESS"].ToString()))
            {
                cboClaim.Editor.ReadOnly = true;
            }

            if (CurrentDataRow["AMOUNT"] != null && !string.IsNullOrWhiteSpace(CurrentDataRow["AMOUNT"].ToString()))
            {
                txtOtherCost.Editor.ReadOnly = true;
            }
        }
        #endregion

        #region Event
        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            Load += ProcessImportInspAbnormalOccurrencePopup_Load;
            //저장 버튼 이벤트
            btnSave.Click += BtnSave_Click;
            btnClose.Click += (s, e) => 
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            txtOtherCost.Editor.EditValueChanged += Editor_EditValueChanged;

            grdInspectionItem.View.FocusedRowChanged += (s, e) => 
            {
                DataRow row = grdInspectionItem.View.GetDataRow(e.FocusedRowHandle);

                if (row == null) return;

                string resourceId = CurrentDataRow["LOTID"] + row["DEFECTCODE"].ToString() + row["QCSEGMENTID"].ToString() + "O" + CurrentDataRow["DEGREE"].ToString();
                SearchImage(resourceId, picExterior, picExterior2);

            };


            grdInspectionItemSpec.View.FocusedRowChanged += (s, e) =>
            {
                DataRow row = grdInspectionItemSpec.View.GetDataRow(e.FocusedRowHandle);

                if (row == null) return;

                string resourceId = CurrentDataRow["LOTID"] + row["INSPITEMID"].ToString() + CurrentDataRow["DEGREE"].ToString();
                SearchImage(resourceId, picMeasure, picMeasure2);

            };

            grdDefect.View.FocusedRowChanged += (s, e) =>
            {
                DataRow row = grdDefect.View.GetDataRow(e.FocusedRowHandle);

                if (row == null) return;

                string resourceId = CurrentDataRow["LOTID"] + row["DEFECTCODE"].ToString() + row["QCSEGMENTID"].ToString() + "D" + CurrentDataRow["DEGREE"].ToString();
                SearchImage(resourceId, picDefect, picDefect2);

            };
        }

        /// <summary>
        /// 비용 -값 불가 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Editor_EditValueChanged(object sender, EventArgs e)
        {
            double amount = txtOtherCost.Editor.EditValue.ToSafeDoubleZero();

            if (amount < 0)
            {
                ShowMessage("CantInputMinusCost");
                txtOtherCost.Editor.EditValue = 0;
            }
        }

        /// <summary>
        /// Load 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessImportInspAbnormalOccurrencePopup_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = isEnable;

            issueRegistrationControl._queryId = "GetNGCountAbnormal";
            issueRegistrationControl._queryVersion = "10001";
            issueRegistrationControl._inspectionType = "ProcessInspection";
        }

        /// <summary>
        /// 데이터를 저장하는 에빈트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (cboClaim.Editor.EditValue == null)
            {
                ShowMessage("IsClaimDisposalRequierd");//Claim 처리 여부를 입력하세요.
                return;
            }

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
            }
        }
        #endregion

        #region Public Function
        public int SelectData()
        {
            int rowCount = 0;

            //lotinfoSelect
            ucLotInfo.ClearData();

            rowCount = SearchLotInfo();

            OnSearch();

            SearchAutoAffectLot();

            return rowCount;
        }
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
            DataTable dt8 = ToSaveClaimInfo(); // ClaimInfo

            int saveRowCount = dt1.Rows.Count + dt2.Rows.Count + dt3.Rows.Count
                             + dt4.Rows.Count + dt5.Rows.Count + dt6.Rows.Count + dt7.Rows.Count
                             + dt8.Rows.Count;

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
                { "claimInfoList", dt8 }
            });

            worker.Execute();
            //var result = worker.Execute<DataTable>();
            //var resultData = result.GetResultSet();
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
        /// CAR 요청 저장
        /// </summary>
        private void SaveCarRequest()
        {
            //DataSet ds = new DataSet();

            //ds.Tables.Add(issueRegistrationControl.RequestCarData());
            //ds.Tables.Add(_changedAffectLot);
            //ds.Tables.Add(ToSaveClaimInfo());


            MessageWorker worker = new MessageWorker("SaveCarRequest");
            worker.SetBody(new MessageBody()
                {

                    { "list", issueRegistrationControl.RequestCarData() },
                    { "affectLotList", _changedAffectLot},
                    { "claimInfoList", ToSaveClaimInfo()},

                });

            var result = worker.Execute<DataTable>();
            var resultData = result.GetResultSet();
     
            ShowMessage(resultData);
        }

        /// <summary>
        /// CAR 접수 저장
        /// </summary>
        private void SaveCarReceipt()
        {
            DataSet ds = issueRegistrationControl.ReceiptCarData();
            //ds.Tables.Add(_changedAffectLot);
            //ds.Tables.Add(ToSaveClaimInfo());

            //var result = ExecuteRule<DataTable>("SaveCarReceipt", ds);

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

            MessageWorker worker = new MessageWorker("SaveCarReceipt");
            worker.SetBody(new MessageBody()
                {

                    { "list", ds.Tables["list"] },
                    { "receiptFileList", ds.Tables["receiptFileList"]},
                    { "affectLotList", _changedAffectLot },
                    { "claimInfoList", ToSaveClaimInfo()},

                });

            var result = worker.Execute<DataTable>();
            var resultData = result.GetResultSet();

            ShowMessage(resultData);
        }

        /// <summary>
        /// CAR 승인 저장
        /// </summary>
        private void SaveCarAccept()
        {
            //DataSet ds = new DataSet();
            //ds.Tables.Add(issueRegistrationControl.AcceptCarData());
            //ds.Tables.Add(_changedAffectLot);
            //ds.Tables.Add(ToSaveClaimInfo());

            //var result = ExecuteRule<DataTable>("SaveCarAccept", ds);

            MessageWorker worker = new MessageWorker("SaveCarAccept");
            worker.SetBody(new MessageBody()
                {

                    { "list", issueRegistrationControl.AcceptCarData() },
                    { "affectLotList", _changedAffectLot},
                    { "claimInfoList", ToSaveClaimInfo()},

                });

            var result = worker.Execute<DataTable>();
            var resultData = result.GetResultSet();

            ShowMessage(resultData);


        }

        /// <summary>
        /// 유효성평가 저장
        /// </summary>
        private void SaveCarValidation()
        {
            DataSet ds = issueRegistrationControl.ValidationCarData();
            //ds.Tables.Add(_changedAffectLot);
            //ds.Tables.Add(ToSaveClaimInfo());
         
            MessageWorker worker = new MessageWorker("SaveCarValidation");
            worker.SetBody(new MessageBody()
                {
  
                    { "list", ds.Tables["list"] }, 
                    { "validationFileList", ds.Tables["validationFileList"]},
                    { "affectLotList", _changedAffectLot }, 
                    { "claimInfoList", ToSaveClaimInfo()},

                });

            var result = worker.Execute<DataTable>();
            var resultData = result.GetResultSet();

            ShowMessage(resultData);
        }

        /// <summary>
        /// ucLotInfo 설정
        /// </summary>
        private void SettingLotgrd()
        {
            txtLotId.Editor.ReadOnly = true;
            // LOT 정보 GRID
            ucLotInfo.ColumnCount = 5;
            ucLotInfo.LabelWidthWeight = "40%";
            ucLotInfo.SetInvisibleFields("PROCESSPATHID", "PROCESSDEFID", "PROCESSDEFVERSION", "PROCESSSEGMENTVERSION", "NEXTPROCESSSEGMENTVERSION",
                "ISLOCKING", "CUSTOMERNAME", "PANELPERQTY", "PROCESSSEGMENTTYPE", "STEPTYPE");

            grpLotInfo.GridButtonItem = GridButtonItem.None;
            grpClaim.GridButtonItem = GridButtonItem.None;
        }

        /// <summary>
        /// 불량 정보 및 detail select 함수
        /// </summary>
        private void OnSearch()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", Framework.UserInfo.Current.Enterprise);
            values.Add("PLANTID", CurrentDataRow["PLANTID"]);
            values.Add("P_RESOURCEID", CurrentDataRow["LOTID"]);
            values.Add("RESOURCETYPE", "ProcessInspection");
            values.Add("P_PROCESSRELNO", CurrentDataRow["PROCESSRELNO"]);
            values.Add("P_RELRESOURCEID", CurrentDataRow["PRODUCTDEFID"]);
            values.Add("P_RELRESOURCEVERSION", CurrentDataRow["PRODUCTDEFVERSION"]);
            values.Add("P_RELRESOURCETYPE", "Product");
            //values.Add("P_INSPITEMTYPE", "OK_NG");
            values.Add("P_INSPECTIONDEFID", "OSPInspection");
            values.Add("P_INSPECTIONCLASSID", "OSPInspection");
            values.Add("P_RESULTTXNHISTKEY", CurrentDataRow["TXNHISTKEY"].ToString());
            values.Add("P_RESULTTXNGROUPHISTKEY", CurrentDataRow["TXNGROUPHISTKEY"].ToString());
            values.Add("P_RESULT", "NG");
            //측정검사 검사항목 가져오기위한 파라미터
            values.Add("P_PROCESSSEGMENTID", CurrentDataRow["PROCESSSEGMENTID"]);
            values.Add("P_PROCESSSEGMENTVERSION", CurrentDataRow["PROCESSSEGMENTVERSION"]);
            values.Add("P_INSPITEMTYPE", "SPC");

            //외관검사 - 불량코드
            DataTable exteriorDefectCode = SqlExecuter.Query("SelectOSPInspectionExterior", "10001", values);
            grdInspectionItem.DataSource = exteriorDefectCode;

            values.Remove("P_INSPECTIONDEFID");
            values.Remove("P_INSPECTIONCLASSID");
            values.Add("P_INSPECTIONDEFID", "OperationInspection");
            values.Add("P_INSPECTIONCLASSID", "OperationInspection");

            //측정검사
            //Item
            DataTable itemDt = SqlExecuter.Query("SelectOSPInspectionMeasure", "10001", values);

            SelectMeasureValueAfterSave();

            grdInspectionItemSpec.DataSource = itemDt;
           
            DataRow itemRow = grdInspectionItemSpec.View.GetFocusedDataRow();
            BindingMeasureValueAfterSave(itemRow);

            //불량처리
            DataTable defectTable = SqlExecuter.Query("SelectOSPInspDefect", "10001", values);
            grdDefect.DataSource = defectTable;
        }

        /// <summary>
        /// 측정검사의 검사항목이 바뀔때 다른 측정값을 검색하는 함수
        /// </summary>
        private void SelectMeasureValueAfterSave()
        {
            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                {"ENTERPRISEID", UserInfo.Current.Enterprise},
                {"PLANTID", CurrentDataRow["PLANTID"]},
                {"P_TXNGROUPHISTKEY", CurrentDataRow["TXNGROUPHISTKEY"]},
                {"P_RESOURCEID",CurrentDataRow["LOTID"]},
                {"P_PROCESSRELNO",CurrentDataRow["PROCESSRELNO"]},
                { "RESOURCETYPE","ProcessInspection"}
            };

            _selectMeasureValueDt = SqlExecuter.Query("SelectOSPMeasureByInspItem", "10001", values);
        }

        /// <summary>
        /// Select한 측정값중 inspitemId와 version 으로 해당 내역만 바인딩하는 함수
        /// </summary>
        /// <param name="row"></param>
        private void BindingMeasureValueAfterSave(DataRow row)
        {
            if (row == null)
            {
                grdMeasuredValue.View.ClearDatas();
                return;
            }

            var toBiding = _selectMeasureValueDt.AsEnumerable()
                .Where(r => r["INSPITEMID"].ToString().Equals(row["INSPITEMID"]) && r["INSPITEMVERSION"].ToString().Equals(row["INSPITEMVERSION"]))
                .ToList();

            if (toBiding.Count > 0)
            {
                grdMeasuredValue.DataSource = toBiding.CopyToDataTable();
            }
            else
            {
                grdMeasuredValue.View.ClearDatas();
            }
        }

        /// <summary>
        /// lotInfo를 조회하는 함수
        /// </summary>
        private int SearchLotInfo()
        {
            int rowCount = 0;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", CurrentDataRow["PLANTID"]);
            param.Add("LOTID", txtLotId.Text);
            param.Add("LOTHISTKEY", CurrentDataRow["LOTHISTKEY"]);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            //2019 12-14 재작업일 경우 다른 쿼리
            //string queryVersion = CurrentDataRow["ISREWORK"].ToString().Equals("Y") ? "10002" : "10001";

            DataTable lotInfo = SqlExecuter.Query("SelectLotInfoByLotIDOSP", "10002", param);

            rowCount = lotInfo.Rows.Count;

            if (rowCount > 0)
            {
                ucLotInfo.DataSource = lotInfo;
            }
            else
            {
                
                ucLotInfo.DataSource = lotInfo;
                // TODO : Inner Join 조건 확인(존재하지만 공정이 없는 LOT도 조회 안됨)
                // 해당 Lot이 존재하지 않습니다. {0}
                this.ShowMessage("NotExistLot", string.Format("LotId = {0}", txtLotId.Text));
                this.Close();
            }

            return rowCount;
        }

        /// <summary>
        /// 자동으로 AffectLot대상 Lot을 조회하는 함수
        /// </summary>
        private void SearchAutoAffectLot()
        {

            Dictionary<string, object> values = new Dictionary<string, object>
            {//***파라미터설정 필요
                {"ABNOCRNO", CurrentDataRow["ABNOCRNO"] },
                {"ABNOCRTYPE", CurrentDataRow["ABNOCRTYPE"]},
                {"LOTID",CurrentDataRow["LOTID"] },
                {"ENTERPRISEID",Framework.UserInfo.Current.Enterprise},
                {"PLANTID",CurrentDataRow["PLANTID"]},
                {"LANGUAGETYPE",Framework.UserInfo.Current.LanguageType}
            };

            DataTable autoAffectLot = SqlExecuter.Query("SelectAffectLotInspAbnormalPopup", "10001", values);

            ncrProgressControl.SetDataGrd(autoAffectLot);
        }
        /// <summary>
        /// 저장 할 claim 테이블 생성★★ 컬럼 확인후 수정 
        /// </summary>
        private DataTable ToSaveClaimInfo()
        {
            string isClaimOldValue = CurrentDataRow["ISCLAIMPROCESS"].ToString();
            string isClaimNewValue = cboClaim.Editor.EditValue.ToString();

            string otherCostOldValue = CurrentDataRow["AMOUNT"].ToString();
            string otherCostNewValue = txtOtherCost.Editor.EditValue.ToString();


            _claimInfo = new DataTable();

            _claimInfo.Columns.Add(new DataColumn("ABNOCRNO", typeof(string)));
            _claimInfo.Columns.Add(new DataColumn("ABNOCRTYPE", typeof(string)));
            _claimInfo.Columns.Add(new DataColumn("ISCLAIMPROCESS", typeof(string)));
            _claimInfo.Columns.Add(new DataColumn("AMOUNT", typeof(double)));
            _claimInfo.Columns.Add(new DataColumn("USERID", typeof(string)));
            _claimInfo.Columns.Add(new DataColumn("ISCHANGED", typeof(string)));
            _claimInfo.Columns.Add(new DataColumn("_STATE_", typeof(string)));

            if (isClaimOldValue != isClaimNewValue || otherCostOldValue != otherCostNewValue)
            {
                DataRow row = _claimInfo.NewRow();

                row["ABNOCRNO"] = CurrentDataRow["ABNOCRNO"];
                row["ABNOCRTYPE"] = CurrentDataRow["ABNOCRTYPE"];
                row["USERID"] = UserInfo.Current.Id;
                row["ISCLAIMPROCESS"] = cboClaim.Editor.EditValue;
                row["AMOUNT"] = txtOtherCost.Editor.EditValue.ToSafeDoubleZero();
                row["_STATE_"] = "modified";

                if (otherCostOldValue != otherCostNewValue)
                {//예전 값과 현재 값 다른가?
                    row["ISCHANGED"] = "Y";
                }
                else
                {
                    row["ISCHANGED"] = "N";
                }

                _claimInfo.Rows.Add(row);
                _claimInfo.TableName = "claimInfoList";
            }

            return _claimInfo;
        }

        /// <summary>
        /// rule에서 return받은 메세지 아이디 보여주는 함수
        /// </summary>
        /// <param name="result"></param>
        private void ShowMessage(DataTable result)
        {
            if (result == null) return;
            string messageId = result.Rows[0]["MESSAGEID"].ToString();

            if (!string.IsNullOrWhiteSpace(messageId))
            {
                ShowMessage("messageId");
            }
        }

        /// <summary>
        /// 외관검사 focusedRow change시 이미지 검색 함수
        /// </summary>
        /// <param name="row"></param>
        private void SearchImage(string resourceId, SmartPictureEdit pic1, SmartPictureEdit pic2)
        {
            pic1.Image = null;
            pic2.Image = null;

            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                { "RESOURCETYPE", "ProcessInspection" },
                { "RESOURCEID", resourceId},
                { "RESOURCEVERSION", "*"}
            };

            DataTable fileDt = SqlExecuter.Query("GetFileHttpPathFromObjectFileByStandardInfo", "10001", values);
            foreach (DataRow fileRow in fileDt.Rows)
            {
                string filenameAndExt = fileRow.GetString("FILENAME") + "." + fileRow.GetString("FILEEXT");
                if (pic1.Image == null)
                {   //2020-01-28 파일 경로변경
                    //pic1.Image = GetImageFromWeb(AppConfiguration.GetString("Application.SmartDeploy.Url") + fileRow.GetString("WEBPATH"));
                    pic1.Image = CommonFunction.GetFtpImageFileToBitmap(fileRow.GetString("FILEPATH"), filenameAndExt);
                }
                else
                {
                    //pic2.Image = GetImageFromWeb(AppConfiguration.GetString("Application.SmartDeploy.Url") + fileRow.GetString("WEBPATH"));
                    pic2.Image = CommonFunction.GetFtpImageFileToBitmap(fileRow.GetString("FILEPATH"), filenameAndExt);
                }

            }
        }
        /*
        /// <summary>
        /// Web Path를 이용하여 이미지를 반환하는 함수
        /// </summary>
        /// <param name="webPath"></param>
        /// <returns></returns>
        private Image GetImageFromWeb(string webPath)
        {
            System.Net.WebClient Downloader = new System.Net.WebClient();

            Bitmap downloadImage;
            try
            {
                Stream ImageStream = Downloader.OpenRead(webPath);

                downloadImage = Bitmap.FromStream(ImageStream) as Bitmap;

            }
            catch (Exception ex)
            {
                throw Framework.MessageException.Create(ex.ToString());
            }

            return downloadImage;
        }*/
        #endregion
    }
}
