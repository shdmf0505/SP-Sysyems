#region using

using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System;
using System.Collections;
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
    /// 프 로 그 램 명  : 품질관리 > 품질규격 > 품질규격 이상발생 > CAR 요청팝업
    /// 업  무  설  명  : 품질 규격 측정값중 NG가 발생한 항목들에 대해 CAR 요청을 할 수 있다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-10-21
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class QualitySpecificationIssuePopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }
        private DataTable _changedAffectLot; // 저장 할 affectLot

        /// <summary>
        /// 팝업진입전 그리드의 컨트롤
        /// </summary>
        public QualitySpecificationIssue ParentControl { get; set; }

        /// <summary>
        /// SmartBaseForm을 사용하기 위한 객체
        /// </summary>
        public SmartBaseForm _form = new SmartBaseForm();

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public QualitySpecificationIssuePopup(DataRow row)
        {
            InitializeComponent();

            InitializeEvent();

            CurrentDataRow = row;

            InitializeCurrentData();

            issueRegistrationControl.isShowReasonCombo = true;
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

            txtOccurDate.EditValue = CurrentDataRow["MEASUREDATETIME"]; // 측정일시
            txtProcesssegment.EditValue = CurrentDataRow["PROCESSSEGMENTNAME"]; // 공정명
            txtArea.EditValue = CurrentDataRow["AREANAME"]; // 작업장명
            txtEquipment.EditValue = CurrentDataRow["EQUIPMENTNAME"]; // 설비명
            txtWorkstartDate.EditValue = CurrentDataRow["WORKSTARTTIME"]; // 작업시작
            txtWorkendDate.EditValue = CurrentDataRow["WORKENDTIME"]; // 작업종료
            txtProduct.EditValue = CurrentDataRow["PRODUCTDEFNAME"]; // 품목명
            txtLot.EditValue = CurrentDataRow["LOTID"]; // Lot No
            txtLotType.EditValue = CurrentDataRow["LOTTYPE"]; // 양산구분
            txtInspitem.EditValue = CurrentDataRow["INSPITEMNAME"]; // 측정항목명
            txtSpecscope.EditValue = CurrentDataRow["SPECSCOPE"]; // 규격범위
            txtAverage.EditValue = CurrentDataRow["AVERAGEVALUE"]; // 평균값
            txtMaxvalue.EditValue = CurrentDataRow["MAXVALUE"]; // 최대값
            txtMinvalue.EditValue = CurrentDataRow["MINVALUE"]; // 최소값
            txtDeviation.EditValue = CurrentDataRow["DEVIATION"]; // 편차

            issueRegistrationControl.ParentDataRow = CurrentDataRow;
            issueRegistrationControl._state = CurrentDataRow["STATE"].ToString();
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
            btnDownload.Click += BtnDownload_Click;
            tabNCRAndDefectImage.SelectedPageChanged += TabNCRAndDefectImage_SelectedPageChanged;
        }
        
        /// <summary>
        /// 탭에 따른 버튼 Visible 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabNCRAndDefectImage_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            // 측정항목 사진 탭일때는 저장버튼 InVisible
            if(tabNCRAndDefectImage.SelectedTabPage.Name.Equals("tpgMeasuredImage"))
            {
                btnSave.Visible = false;
            }
            else
            {
                btnSave.Visible = true;
            }
        }

        /// <summary>
        /// 이미지 다운로드 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDownload_Click(object sender, EventArgs e)
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
                    CheckTempData(0, Format.GetString(issueRegistrationControl.cboRequestNumber.EditValue), issueRegistrationControl._requestDt);;
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

            issueRegistrationControl._queryId = "GetConcurrenceCount";
            issueRegistrationControl._queryVersion = "10001";

            ncrProgressControl.CurrentDataRow = CurrentDataRow;
            SearchImage();
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
                    throw MessageException.Create("NeedToSaveTemp"); // 임시저장을 해주세요.
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
        /// 자동으로 AffectLot대상 Lot을 조회하는 함수
        /// </summary>
        public void SearchAutoAffectLot()
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "ABNOCRNO", CurrentDataRow["ABNOCRNO"] },
                { "ABNOCRTYPE", CurrentDataRow["ABNOCRTYPE"]},
                { "EQUIPMENTID", CurrentDataRow["EQUIPMENTID"] },
                { "LOTID", CurrentDataRow["LOTID"]},
                { "ENTERPRISEID", Framework.UserInfo.Current.Enterprise},
                { "PLANTID", Framework.UserInfo.Current.Plant},
                { "LANGUAGETYPE", Framework.UserInfo.Current.LanguageType}
            };

            DataTable autoAffectLot = SqlExecuter.Query("QualitySpecIssueAffectLot", "10001", values);

            ncrProgressControl.SetDataGrd(autoAffectLot);
        }

        /// <summary>
        /// AffectLot 저장 테이블에 이상발생번호 및 타입추가
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
        /// 품질규격 측정항목 이미지 조회
        /// </summary>
        private void SearchImage()
        {
            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                { "RESOURCETYPE", "OperationInspection" },
                { "RESOURCEVERSION", "*" },
                { "LOTID", CurrentDataRow["LOTID"] },
                { "DAITEMID", CurrentDataRow["INSPITEMID"] }
            };

            DataTable fileDt = SqlExecuter.Query("GetOperationInspMeasureImage", "10001", param); // 측정항목의 파일테이블

            if (fileDt != null && fileDt.Rows.Count != 0)
            {
                foreach (DataRow dr in fileDt.Rows)
                {
                    //string filePath = Format.GetString(grdFileList.View.GetFocusedRowCellValue("FILEPATH"));
                    //string fileName = string.Join(".", Format.GetString(grdFileList.View.GetFocusedRowCellValue("FILENAME")), Format.GetString(grdFileList.View.GetFocusedRowCellValue("FILEEXT")));

                    //picDefectPhoto.EditValue = CommonFunction.GetFtpImageFileToByte(filePath, fileName);

                    //string fileURL = AppConfiguration.GetString("Application.SmartDeploy.Url") + dr["URL"]; // 파일 URL
                    //2020-01-28 강유라
                    string filePath = Format.GetString(dr["FILEPATH"]);
                    string fileName = Format.GetString(dr["FILENAME"]) + "." +Format.GetString(dr["FILEEXT"]); // 파일명
                    string textValue = Language.Get("MEASURVALUE") + dr["DAPOINTID"] + " : " + dr["VALUE"] + " / " + dr["RESULT"];

                    ucSelfImageControl imageControl = new ucSelfImageControl(filePath, fileName);
                    flowMeasuredPicture.Controls.Add(imageControl);
                    imageControl.SetImageTextValue(textValue);
                }
            }
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
