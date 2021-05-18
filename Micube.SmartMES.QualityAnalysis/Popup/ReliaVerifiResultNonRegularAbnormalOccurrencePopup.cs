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
using Micube.Framework.Net;
using Micube.Framework;
using System.IO;
using DevExpress.XtraEditors.Repository;
using Micube.SmartMES.Commons;

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 신뢰성검증 > 신뢰성 검증 이상 발생(정기외)
    /// 업  무  설  명  : 신뢰성 검증 이상 발생(정기)를 등록하는 팝업
    /// 생    성    자  : 유호석
    /// 생    성    일  : 2019-10-08
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ReliaVerifiResultNonRegularAbnormalOccurrencePopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }

        /// <summary>
        /// 팝업진입전 그리드의 컨트롤
        /// </summary>
        public ReliabilityVerificationAbnormalOccurrenceNonRegular ParentControl { get; set; }

        /// <summary>
        /// SmartBaseForm을 사용하기 위한 객체
        /// </summary>
        public SmartBaseForm _form = new SmartBaseForm();

        private DataTable _changedAffectLot; //저장 할 affectLot
        private DataTable _changedMainTable; // 특이사항 저장

        DataTable _dtInspectionFile;

        #endregion

        #region 생성자

        public ReliaVerifiResultNonRegularAbnormalOccurrencePopup(DataRow dr)
        {
            InitializeComponent();

            CurrentDataRow = dr;

            InitializeControl();
            InitializeGrid();
            InitializeEvent();

            issueRegistrationControl2.ParentDataRow = CurrentDataRow;
            issueRegistrationControl2.isShowReasonCombo = true;
            issueRegistrationControl2.SetControlsVisible();
            ncrProgressControl4.CurrentDataRow = CurrentDataRow;
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 입력 컨트롤 초기화
        /// </summary>
        private void InitializeControl()
        {
            memComments.Text = CurrentDataRow["COMMENTS"].ToString();
            lbDfectName.Text = CurrentDataRow["DEFECTNAME"].ToString();
            lbAbnormalAccurence.Text = CurrentDataRow["RESONCODENAME"].ToString();

            selectUnitList(); // 단위
            selectDefectCodePopup(); // 불량코 팝업
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            InitializeGrdProductInformation(); // 제품 정보 그리드 초기화
            InitializeGrdMeasureHistory(); // 제조 이력 그리드 초기화
            InitializeGridMeasureValue(); // 측정값 입력 그리드 초기화
        }

        /// <summary>
        /// 제품 정보 그리드 초기화
        /// </summary>
        private void InitializeGrdProductInformation()
        {
            grdProductnformation.View
                .AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 품목코드

            grdProductnformation.View
                .AddTextBoxColumn("PRODUCTDEFNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 품목명

            grdProductnformation.View
                .AddTextBoxColumn("LOTID", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 작업장

            grdProductnformation.View
                .AddTextBoxColumn("WEEK", 100)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 설비(호기)

            grdProductnformation.View.PopulateColumns();
        }

        /// <summary>
        /// 제조 이력 그리드 초기화
        /// </summary>
        private void InitializeGrdMeasureHistory()
        {
            grdManufacturingHistory.View
                .AddTextBoxColumn("LOTWORKTXNHISTKEY", 100)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 공정순서(번호)

            grdManufacturingHistory.View
                .AddTextBoxColumn("LOTID", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // LOT ID

            grdManufacturingHistory.View
                .AddTextBoxColumn("PROCESSSEGMENTNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 공정명

            grdManufacturingHistory.View
                .AddTextBoxColumn("AREANAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 작업장

            grdManufacturingHistory.View
                .AddTextBoxColumn("EQUIPMENTNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 설비(호기)

            grdManufacturingHistory.View
                .AddTextBoxColumn("TRACKOUTTIME", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 작업종료시간

            grdManufacturingHistory.View.PopulateColumns();
        }

        private void InitializeGridMeasureValue()
        {
            grdMeasureValue.GridButtonItem = GridButtonItem.Export;

            grdMeasureValue.View.SetIsReadOnly();

            grdMeasureValue.View
                .AddTextBoxColumn("TITLE", 210)
                .SetTextAlignment(TextAlignment.Left); // 제목

            grdMeasureValue.View
                .AddSpinEditColumn("MEASUREVALUE", 210)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("{#,###,##0.####}", MaskTypes.Numeric); // 측정값

            grdMeasureValue.View
                .AddTextBoxColumn("COMMENTS", 500)
                .SetTextAlignment(TextAlignment.Left); // 내용

            grdMeasureValue.View.PopulateColumns();

            /*
            RepositoryItemTextEdit edit = new RepositoryItemTextEdit();
            edit.Mask.EditMask = "#,###,##0.####";
            edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            edit.Mask.UseMaskAsDisplayFormat = true;

            grdMeasureValue.View.Columns["MEASUREVALUE"].ColumnEdit = edit;
            */
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {
            this.Load += ReliaVerifiResultRegularPopup_Load;
            btnSave.Click += BtnSave_Click;
            btnClose.Click += BtnClose_Click;

            // 이미지 저장 이벤트
            btnImageSave.Click += (s, e) =>
            {
                var enumerator = flowMeasuredPicture.Controls.GetEnumerator();
                var i = 0;

                while (enumerator.MoveNext())
                {
                    MultiVerificationResultControl vr = enumerator.Current as MultiVerificationResultControl;

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
                            MultiVerificationResultControl vr = enumerator.Current as MultiVerificationResultControl;

                            if (vr.selectImage().ToString() != string.Empty && vr.chkPicture.Checked == true)
                            {
                                Bitmap image = (Bitmap)vr.selectImage();
                                Bitmap newImage = new Bitmap(image);
                                image.Dispose();
                                image = null;
                                newImage.Save(string.Concat(folderPath, "\\", vr.strFileName()));

                                vr.picMeasurePrinted.Image = newImage;
                                vr.picMeasurePrinted.Tag = newImage;
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
        /// 닫기버튼 클릭시 화면 닫기
        /// </summary>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            // 임시저장 했는지 체크
            switch (issueRegistrationControl2._tabIndex)
            {
                case 0:
                    CheckTempData(0, Format.GetString(issueRegistrationControl2.cboRequestNumber.EditValue), issueRegistrationControl2._requestDt);
                    break;
                case 1:
                    CheckTempData(0, Format.GetString(issueRegistrationControl2.cboRequestNumber.EditValue), issueRegistrationControl2._requestDt);
                    break;
                case 2:
                    CheckTempData(0, Format.GetString(issueRegistrationControl2.cboRequestNumber.EditValue), issueRegistrationControl2._requestDt);
                    break;
                case 3:
                    CheckTempData(0, Format.GetString(issueRegistrationControl2.cboRequestNumber.EditValue), issueRegistrationControl2._requestDt);
                    break;
            }

            if (this.ShowMessageBox("InfoSave", "Caption", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                // 저장할 데이터가 있는지 체크 후 저장
                CheckDataAndSaveCarAllState(issueRegistrationControl2);
                this.Close();
                ParentControl.Search();
            }
        }

        /// <summary>
        /// Form Road시 초기화
        /// </summary>
        private void ReliaVerifiResultRegularPopup_Load(object sender, EventArgs e)
        {
            grdAttachedFile.LanguageKey = "ATTACHEDFILE";
            grdReport.LanguageKey = "REPORT";
            btnImageSave.LanguageKey = "EXPORT";

            SearchProductInformation(); // 제품 정보 조회

            SearchInspectionFile(); // 검증 결과 조회
            SearchMeasuringHistory(); // 제조 이력 조회
            SearchMeasuredValue(); // 측정값 조회
            SearchAttachFile(); // 첨부파일 조회
            SearchReport(); // 보고서

            flowMeasuredPicture.AutoScroll = true; // 스크롤 생기게 함

            issueRegistrationControl2._queryId = "GetReliabilityRegulaConcurrenceCount";
            issueRegistrationControl2._queryVersion = "10001";
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
            if (issueRegistrationControl2.IsInputData(tapIndex))
            {
                if (!issueRegistrationControl2.CheckIsTempSaved(sequence, tempDt))
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
            _changedAffectLot = ncrProgressControl4.GetChangedData();
            _changedAffectLot = SaveAffectLot(_changedAffectLot);

            _changedMainTable = new DataTable();
            _changedMainTable.Columns.Add(new DataColumn("REQUESTNO", typeof(string)));
            _changedMainTable.Columns.Add(new DataColumn("LOTID", typeof(string)));
            _changedMainTable.Columns.Add(new DataColumn("DESCRIPTION", typeof(string)));

            DataRow row = _changedMainTable.NewRow();
            row["REQUESTNO"] = CurrentDataRow["REQUESTNO"];
            row["LOTID"] = CurrentDataRow["LOTID"];
            row["DESCRIPTION"] = memComments.Text;
            _changedMainTable.Rows.Add(row);

            DataTable dt1 = con._requestDt; // 요청
            DataTable dt2 = con._receiptDt; // 접수
            DataTable dt3 = con._receiptFileDt; // 접수파일
            DataTable dt4 = con._acceptDt; // 승인
            DataTable dt5 = con._validationDt; // 유효성
            DataTable dt6 = con._validationFileDt; // 유효성파일
            DataTable dt7 = _changedAffectLot; // AffectLot
            DataTable dt8 = _changedMainTable; // mainList

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
                { "mainList", dt8 }
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
            {//***파라미터설정 필요
                {"ABNOCRNO", CurrentDataRow["ABNOCRNO"] },
                {"ABNOCRTYPE", CurrentDataRow["ABNOCRTYPE"]},
                {"ENTERPRISEID",Framework.UserInfo.Current.Enterprise},
                {"PLANTID",Framework.UserInfo.Current.Plant},
                {"LANGUAGETYPE",Framework.UserInfo.Current.LanguageType},
                {"LOTID",CurrentDataRow["LOTID"]}
            };

            //DataTable autoAffectLot = SqlExecuter.Query("ReliabilityVerificationAbnormalOccurrenceAffecLotList", "10001", values);
            DataTable autoAffectLot = SqlExecuter.Query("SelectAffectLotInspAbnormalPopup", "10001", values);


            ncrProgressControl4.SetDataGrd(autoAffectLot);
        }

        /// <summary>
        /// 제품 정보 조회
        /// </summary>
        private void SearchProductInformation()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("P_REQUESTNO", CurrentDataRow["REQUESTNO"]);
            values.Add("P_PLANTID", CurrentDataRow["PLANTID"]);

            values.Add("P_PRODUCTDEFID", CurrentDataRow["PRODUCTDEFID"]);
            values.Add("P_PRODUCTDEFVERSION", CurrentDataRow["PRODUCTDEFVERSION"]);
            values.Add("P_LOTID", CurrentDataRow["LOTID"]);
            values.Add("P_INSPITEMID", CurrentDataRow["INSPITEMID"]);
            values.Add("P_INSPITEMVERSION", CurrentDataRow["INSPITEMVERSION"]);

            values.Add("P_ISJUDGMENTRESULT", "*");
            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dt = SqlExecuter.Query("GetReliaVerifiResultNonRegularlist", "10001", values);
            //dt.Columns.Add(new DataColumn("REQUESTQTY", typeof(string)));
            dt.Columns.Add(new DataColumn("REQUESTREASONS", typeof(string)));


            grdProductnformation.DataSource = dt;
        }

        /// <summary>
        /// 검증 결과 조회
        /// </summary>
        private void SearchInspectionFile()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            //param.Add("P_INSPECTIONTYPE", "ReliaVerifiResultNonRegular");
            //param.Add("P_RESOURCEID", CurrentDataRow["LOTID"].ToString());//품목 + LOT
            //param.Add("P_PROCESSRELNO", CurrentDataRow["REQUESTNO"].ToString());
            //param.Add("P_MEASURETYPE", "VerificationResult");

            param.Add("P_INSPECTIONTYPE", "ReliaVerifiResultNonRegular");
            param.Add("P_RESOURCEID", CurrentDataRow["PRODUCTDEFID"].ToString() + CurrentDataRow["PRODUCTDEFVERSION"].ToString() + CurrentDataRow["LOTID"].ToString() + CurrentDataRow["INSPITEMID"].ToString() + CurrentDataRow["INSPITEMVERSION"].ToString());//품목 + LOT
            param.Add("P_PROCESSRELNO", CurrentDataRow["REQUESTNO"].ToString());
            param.Add("P_MEASURETYPE", "VerificationResult");


            _dtInspectionFile = SqlExecuter.Query("SelectInspectionFile", "10001", param);

            if (_dtInspectionFile != null && _dtInspectionFile.Rows.Count != 0)
            {
                ImageConverter converter = new ImageConverter();
                foreach (DataRow dr in _dtInspectionFile.Rows)
                {
                    // 2020.03.01-유석진-등록된 item, value 조회하는 로직 적용
                    DataTable measureValue = new DataTable();
                    measureValue.Columns.Add("MEASUREITEMID", typeof(string));
                    measureValue.Columns.Add("MEASUREVALUE", typeof(double));

                    object file = dr["FILEDATA"];
                    string filePath = dr["FILEPATH"].ToString();
                    string fileName = dr["FILENAME"].ToString();
                    string fileText = dr["FILEEXT"].ToString();
                    string title = dr["TITLE"].ToString();

                    if (file != null)
                    {
                        try
                        {
                            //Bitmap image = new Bitmap((Image)converter.ConvertFrom(file));
                            //Bitmap image = new Bitmap(GetImageFromWeb(AppConfiguration.GetString("Application.SmartDeploy.Url") + dr.GetString("WEBPATH")));

                            fileName = string.Join(".", fileName, fileText);
                            Bitmap image = CommonFunction.GetFtpImageFileToBitmap(filePath, fileName);

                            MultiVerificationResultControl vr = new MultiVerificationResultControl(image, fileName);
                            vr.setTitle(title);
                            vr.txtTitle.Enabled = false;
                            vr.btnAdd.Enabled = false;
                            vr.btnDelete.Enabled = false;
                            vr.btnClose.Enabled = false;
                            flowMeasuredPicture.Controls.Add(vr);
                            // 2020.03.01-유석진-추가,삭제버튼 비활성화
                            vr.grdMeasureValue.GridButtonItem = GridButtonItem.None;
                            vr.grdMeasureValue.View.SetIsReadOnly(true);

                            // 2020.03.01-유석진-등록된 item, value 조회하는 로직 적용
                            for (int i = 0; i < 10; i++)
                            {
                                if (!string.IsNullOrEmpty(dr["MEASUREITEMID" + (i + 1)].ToString()))
                                {
                                    DataRow measureDr = measureValue.NewRow();
                                    measureDr["MEASUREITEMID"] = dr["MEASUREITEMID" + (i + 1)];
                                    if (i > 0)
                                        measureDr["MEASUREVALUE"] = dr["MEASUREVALUE" + (i + 1)];
                                    else
                                        measureDr["MEASUREVALUE"] = dr["MEASUREVALUE"].ToString();

                                    measureValue.Rows.Add(measureDr);
                                }
                            }

                            vr.grdMeasureValue.DataSource = measureValue;
                        } catch(Exception ex)
                        {
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 제조 이력 정보 조회
        /// </summary>
        private void SearchMeasuringHistory()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("P_REQUESTNO", CurrentDataRow["REQUESTNO"]);
            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dt = SqlExecuter.Query("GetManufacturingHistoryList", "10001", values);

            grdManufacturingHistory.DataSource = dt;
        }

        /// <summary>
        /// 측정값 조회
        /// </summary>
        private void SearchMeasuredValue()
        {
            //Dictionary<string, object> values = new Dictionary<string, object>();
            //values.Add("P_REQUESTNO", CurrentDataRow["REQUESTNO"].ToString());
            //values.Add("P_ENTERPRISEID", CurrentDataRow["ENTERPRISEID"].ToString());
            //values.Add("P_PLANTID", CurrentDataRow["PLANTID"].ToString());
            //values.Add("P_LOTID", CurrentDataRow["LOTID"].ToString());
            ////values.Add("P_LOTWORKTXNHISTKEY", CurrentDataRow["TXNHISTKEY"].ToString());
            //values.Add("P_MEASURETYPE", "MeasuredValue");

            //DataTable dt = SqlExecuter.Query("GetQcreliabilityMeasureList", "10001", values);
            //fileValues.Add("P_FILERESOURCEID", CurrentDataRow["REQUESTNO"].ToString() + CurrentDataRow["PRODUCTDEFID"].ToString() + CurrentDataRow["PRODUCTDEFVERSION"].ToString() + CurrentDataRow["LOTID"].ToString() + CurrentDataRow["INSPITEMID"].ToString() + CurrentDataRow["INSPITEMVERSION"].ToString());
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_ENTERPRISEID", CurrentDataRow["ENTERPRISEID"].ToString());
            param.Add("P_PLANTID", CurrentDataRow["PLANTID"].ToString());
            param.Add("P_REQUESTNO", CurrentDataRow["REQUESTNO"].ToString());
            param.Add("P_PRODUCTDEFID", CurrentDataRow["PRODUCTDEFID"].ToString());
            param.Add("P_LOTID", CurrentDataRow["LOTID"].ToString());
            param.Add("P_MEASURETYPE", "MeasuredValue");
            param.Add("P_INSPITEMID", CurrentDataRow["INSPITEMID"].ToString());
            param.Add("P_INSPITEMVERSION", CurrentDataRow["INSPITEMVERSION"].ToString());
            DataTable dtReliabilityMeasurValue = SqlExecuter.Query("GetReliabilityNonRegularMeasureList", "10001", param);

            if (dtReliabilityMeasurValue.Rows.Count > 0)
            {
                cboUnit.EditValue = dtReliabilityMeasurValue.Rows[0]["UNIT"]; // 단위 설정
            }

            grdMeasureValue.DataSource = dtReliabilityMeasurValue;
        }

        /// <summary>
        /// 첨부파일 조회
        /// </summary>
        private void SearchAttachFile()
        {
            //Dictionary<string, object> values = new Dictionary<string, object>();
            //values.Add("P_FILERESOURCETYPE", "ReliaVerifiResultNonRegularFile");
            //values.Add("P_FILERESOURCEID", CurrentDataRow["REQUESTNO"].ToString() + CurrentDataRow["LOTID"].ToString());
            //values.Add("P_FILERESOURCEVERSION", "1");

            //DataTable dt = SqlExecuter.Query("SelectFileInspResult", "10001", values);

            //grdAttachedFile.DataSource = dt;

            Dictionary<string, object> fileValues = new Dictionary<string, object>();
            //이미지 파일Search parameter
            fileValues.Add("P_FILERESOURCETYPE", "ReliaVerifiResultNonRegularFile");
            fileValues.Add("P_FILERESOURCEID", CurrentDataRow["REQUESTNO"].ToString() + CurrentDataRow["PRODUCTDEFID"].ToString() + CurrentDataRow["PRODUCTDEFVERSION"].ToString() + CurrentDataRow["LOTID"].ToString() + CurrentDataRow["INSPITEMID"].ToString() + CurrentDataRow["INSPITEMVERSION"].ToString());
            fileValues.Add("P_FILERESOURCEVERSION", "0");

            DataTable fileTable = SqlExecuter.Query("SelectFileInspResult", "10001", fileValues);
            grdAttachedFile.DataSource = fileTable;

           
        }

        /// <summary>
        /// 보고서 조회
        /// </summary>
        private void SearchReport()
        {
            //Dictionary<string, object> values = new Dictionary<string, object>();
            //values.Add("P_FILERESOURCETYPE", "ReliaVerifiResultNonRegularReport");
            //values.Add("P_FILERESOURCEID", CurrentDataRow["REQUESTNO"].ToString() + CurrentDataRow["LOTID"].ToString());
            //values.Add("P_FILERESOURCEVERSION", "1");

            //DataTable dt = SqlExecuter.Query("SelectFileInspResult", "10001", values);

            //grdReport.DataSource = dt;

            Dictionary<string, object> fileValues2 = new Dictionary<string, object>();
            //이미지 파일Search parameter
            fileValues2.Add("P_FILERESOURCETYPE", "ReliaVerifiResultNonRegularReport");
            fileValues2.Add("P_FILERESOURCEID", CurrentDataRow["REQUESTNO"].ToString() + CurrentDataRow["PRODUCTDEFID"].ToString() + CurrentDataRow["PRODUCTDEFVERSION"].ToString() + CurrentDataRow["LOTID"].ToString() + CurrentDataRow["INSPITEMID"].ToString() + CurrentDataRow["INSPITEMVERSION"].ToString());
            fileValues2.Add("P_FILERESOURCEVERSION", "0");

            DataTable fileTable2 = SqlExecuter.Query("SelectFileInspResult", "10001", fileValues2);

            grdReport.DataSource = fileTable2;
        }

        /// <summary>
        /// 단위 조회
        /// </summary>
        private void selectUnitList()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("ENTERPRISEID", "*");
            values.Add("PLANTID", "*");
            values.Add("UOMTYPE", "Unit");

            cboUnit.ValueMember = "UOMDEFID";
            cboUnit.DisplayMember = "UOMDEFNAME";
            cboUnit.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboUnit.ShowHeader = false;

            cboUnit.DataSource = SqlExecuter.Query("GetUOMList", "10001", values); ;
        }
        /// <summary>
        /// //@@네트워크 경로 변경
        /// </summary>
        /// <param name="webPath"></param>
        /// <returns></returns>
        private Image GetImageFromWeb(string webPath)
        {
            System.Net.WebClient Downloader = new System.Net.WebClient();

            Stream ImageStream = Downloader.OpenRead(webPath);

            Bitmap downloadImage = Bitmap.FromStream(ImageStream) as Bitmap;

            return downloadImage;
        }
        /// <summary>
        /// 공통코드 조회
        /// </summary>
        private void selectCodeList(string clodeCalss, object control)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("CODECLASSID", clodeCalss);

            SmartLabelComboBox lbCombo = control as SmartLabelComboBox;
            lbCombo.Control.ValueMember = "CODEID";
            lbCombo.Control.DisplayMember = "CODENAME";
            lbCombo.Control.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            lbCombo.Control.ShowHeader = false;

            lbCombo.Control.DataSource = SqlExecuter.Query("GetCodeList", "00001", values); ;
        }

        /// <summary>
        /// 불량코드 팝업 조회
        /// </summary>
        private void selectDefectCodePopup()
        {
            ConditionItemSelectPopup defectcodePopup = new ConditionItemSelectPopup();

            defectcodePopup.Id = "DEFECTCODE";
            defectcodePopup.SearchQuery = new SqlQuery("GetDefectCodeList", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            defectcodePopup.ValueFieldName = "DEFECTCODE";
            defectcodePopup.DisplayFieldName = "DEFECTNAME";
            defectcodePopup.SetPopupLayout("DEFECTCODE", PopupButtonStyles.Ok_Cancel, true, true);
            defectcodePopup.SetPopupResultCount(1);
            defectcodePopup.SetPopupLayoutForm(400, 500, FormBorderStyle.FixedToolWindow);
            defectcodePopup.SetValidationIsRequired();
            defectcodePopup.SetPopupAutoFillColumns("DEFECTCODE");

            defectcodePopup.Conditions.AddTextBox("DEFECTCODENAME");
            defectcodePopup.GridColumns.AddTextBoxColumn("DEFECTCODE", 150);
            defectcodePopup.GridColumns.AddTextBoxColumn("DEFECTNAME", 150);
        }

        #endregion

        #region 저장

        /// <summary>
        /// CAR 요청 저장
        /// </summary>
        private void SaveCarRequest()
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(issueRegistrationControl2.RequestCarData());

            DataTable _chemicalDetailTable = new DataTable();
            _chemicalDetailTable.ImportRow(CurrentDataRow);

            ds.Tables.Add(_chemicalDetailTable);
            ds.Tables.Add(_changedAffectLot);
            ds.Tables.Add(_changedMainTable);

            ExecuteRule("SaveCarRequest", ds);
        }

        /// <summary>
        /// CAR 접수 저장
        /// </summary>
        private void SaveCarReceipt()
        {
            DataSet ds = issueRegistrationControl2.ReceiptCarData();
            ds.Tables.Add(_changedAffectLot);
            ds.Tables.Add(_changedMainTable);

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
            ds.Tables.Add(issueRegistrationControl2.AcceptCarData());
            ds.Tables.Add(_changedAffectLot);
            ds.Tables.Add(_changedMainTable);

            ExecuteRule("SaveCarAccept", ds);
        }

        /// <summary>
        /// 유효성평가 저장
        /// </summary>
        private void SaveCarValidation()
        {
            DataSet ds = issueRegistrationControl2.ValidationCarData();
            ds.Tables.Add(_changedAffectLot);
            ds.Tables.Add(_changedMainTable);

            ExecuteRule("SaveCarValidation", ds);
        }

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

        #endregion
    }
}
