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

namespace Micube.SmartMES.QualityAnalysis
{

    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 출하검사 > 자주/최종검사 이상발생 팝업
    /// 업  무  설  명  : 자주/최종검사 이상발생을 등록한다
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-11-05 
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    /// 
    public partial class SelfFinishAOIInspAbnormalOccurrencePopup : SmartPopupBaseForm, ISmartCustomPopup
    {

        #region 인터페이스
        public DataRow CurrentDataRow { get; set; }
        public bool isEnable = true;
        public string _strPlantId = "";
        #endregion

        #region Local Variables  
        private DataTable _changedAffectLot;//저장 할 affectLot
        #endregion

        #region 생성자
        public SelfFinishAOIInspAbnormalOccurrencePopup(DataRow row, string abnormalType)
        {
            InitializeComponent();
            CurrentDataRow = row;
            InitializeEvent();

            issueRegistrationControl.isShowReasonCombo = true;
            issueRegistrationControl.SetControlsVisible();

            issueRegistrationControl.abnormalType = abnormalType;
            issueRegistrationControl.SetCarRequestQueryId();
        }
        #endregion

        #region 컨텐츠 영역 초기화
        /// <summary>
        /// userControl 또는 control에 CurrentDataRow 할당 함수
        /// </summary>
        public void SetStandardDataToUserControl()
        {
            issueRegistrationControl._queryId = "GetNGCountAbnormal";
            issueRegistrationControl._queryVersion = "10001";
            issueRegistrationControl._inspectionType = CurrentDataRow["ABNOCRTYPE"].ToString();

            issueRegistrationControl.ParentDataRow = CurrentDataRow;
            issueRegistrationControl._state = CurrentDataRow["STATE"].ToString();


            ncrProgressControl.CurrentDataRow = CurrentDataRow;

            //제품정보
            txtProductdefId.Editor.EditValue = CurrentDataRow["PRODUCTDEFID"];
            txtProductdefName.Editor.EditValue = CurrentDataRow["PRODUCTDEFNAME"];
            txtParentLotId.Editor.EditValue = CurrentDataRow["PARENTLOTID"];
            txtLotId.Editor.EditValue = CurrentDataRow["LOTID"];
            txtPnlQty.Editor.EditValue = CurrentDataRow["PANELQTY"];
            txtPcsQty.Editor.EditValue = CurrentDataRow["PCSQTY"];
            //공정정보
            txtTopProcessClassName.Editor.EditValue = CurrentDataRow["TOPPROCESSSEGMENTCLASSNAME"];
            txtMiddleProcessClassName.Editor.EditValue = CurrentDataRow["MIDDLEPROCESSSEGMENTCLASSNAME"];
            txtProcessName.Editor.EditValue = CurrentDataRow["PROCESSSEGMENTNAME"];
            txtAreaName.Editor.EditValue = CurrentDataRow["AREANAME"];
            txtInsfectionDefName.Editor.EditValue = CurrentDataRow["INSPECTIONDEFNAME"];
            txtInspectionUser.Editor.EditValue = CurrentDataRow["INSPECTIONUSER"];
            //불량정보
            txtDecisionDegree.Editor.EditValue = CurrentDataRow["DECISIONDEGREE"];
            txtDefectCodeName.Editor.EditValue = CurrentDataRow["DEFECTCODENAME"];
            txtQCSegmentName.Editor.EditValue = CurrentDataRow["QCSEGMENTNAME"];
            txtInspectionQty.Editor.EditValue = CurrentDataRow["INSPECTIONQTY"];
            txtDefectQty.Editor.EditValue = CurrentDataRow["DEFECTQTY"];
            txtDefectRate.Editor.EditValue = CurrentDataRow["DEFECTRATE"];
            //원인공정
            txtReasonConsumableDefName.Editor.EditValue = CurrentDataRow["REASONCONSUMABLEDEFNAME"];
            txtReasonConsumableDefVersion.EditValue = CurrentDataRow["REASONCONSUMABLEDEFVERSION"];
            txtReasonConsumableLotId.Editor.EditValue = CurrentDataRow["REASONCONSUMABLELOTID"];
            txtReasonSegmentName.Editor.EditValue = CurrentDataRow["REASONSEGMENTNAME"];
            txtReasonAreaName.EditValue = CurrentDataRow["REASONAREANAME"];
            txtReasonCodeName.Editor.EditValue = CurrentDataRow["REASONCODENAME"];

        

            SearchAutoAffectLot();
        }
        #endregion

        #region Event
        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            Load += SelfFinishAOIInspAbnormalOccurrencePopup_Load;
            //저장 버튼 이벤트
            btnSave.Click += BtnSave_Click;
            btnClose.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            // 이미지 저장 이벤트
            btnImageSave.Click += (s, e) =>
            {
                var enumerator = flowMeasuredPicture.Controls.GetEnumerator();
                var i = 0;

                while (enumerator.MoveNext())
                {
                    ucSelfImageControl vr = enumerator.Current as ucSelfImageControl;

                    if (vr.selectImage().ToString() != string.Empty && vr.chkPicture.Checked == true)
                    {
                        i++;
                    }
                }

                if (i == 0) // 선택된 건이 없으면 이미지 저장 안됨
                {
                    return;
                }

                try
                {
                    DialogManager.ShowWaitArea(this);
                    FolderBrowserDialog dialog = new FolderBrowserDialog();
                    dialog.SelectedPath = Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

                    if (dialog.ShowDialog().Equals(DialogResult.OK))
                    {
                        string folderPath = dialog.SelectedPath;

                        enumerator = flowMeasuredPicture.Controls.GetEnumerator();

                        while (enumerator.MoveNext())
                        {
                            ucSelfImageControl imageControl = enumerator.Current as ucSelfImageControl;

                            if (imageControl.selectImage().ToString() != string.Empty && imageControl.chkPicture.Checked == true)
                            {
                                Bitmap image = (Bitmap)imageControl.selectImage();
                                image.Save(string.Concat(folderPath, "\\", imageControl.strFileName()));
                            }
                        }

                        ShowMessage("SuccedSave");
                    }
                }
                catch (Exception ex)
                {
                    throw Framework.MessageException.Create(ex.ToString());
                }
                finally
                {
                    DialogManager.CloseWaitArea(this);
                }
            };
        }
        /// <summary>
        /// 팝업 로드 이벤트 > 사진 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelfFinishAOIInspAbnormalOccurrencePopup_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = isEnable;

            string resourceType = CurrentDataRow["INSPECTIONDEFID"].ToString();
            SearchImage(resourceType);
            /*if (resourceType.Equals("SelfInspectionTake") || resourceType.Equals("SelfInspectionShip"))
            {
                SearchImage(resourceType);
            }
            else
            {
                tabAbnormal.TabPages[1].PageVisible = false;
            }*/
        }

        /// <summary>
        /// 데이터를 저장하는 에빈트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
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
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        #endregion

        #region Public Function

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
            DataSet ds = new DataSet();

            ds.Tables.Add(issueRegistrationControl.RequestCarData());
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
        /// 자주검사 사진을 조회하는 함수
        /// </summary>
        private void SearchImage(string resourceType)
        {
            string fileResourceId = CurrentDataRow["FILERESOURCEID"].ToString();

            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                {"FILERESOURCETYPE", resourceType},
                {"FILERESOURCEID", fileResourceId}
            };

            DataTable fileDt = SqlExecuter.Query("SelectSelfInspMeasureImage", "10001", values);

            if (fileDt != null && fileDt.Rows.Count != 0)
            {
                foreach (DataRow dr in fileDt.Rows)
                {
                    string filePath = Format.GetString(dr["FILEPATH"]);
                    string fileName = dr["FILENAME"].ToString() + "." + Format.GetString(dr["FILEEXT"]);

                    ucSelfImageControl imageControl = new ucSelfImageControl(filePath, fileName);
                    flowMeasuredPicture.Controls.Add(imageControl);
                    }
                }
        }

        #endregion

    }
}
