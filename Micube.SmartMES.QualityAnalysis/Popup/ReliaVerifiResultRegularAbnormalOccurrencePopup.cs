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
    /// 프 로 그 램 명  : 품질관리 > 신뢰성검증 > 신뢰성 검증 이상 발생(정기)
    /// 업  무  설  명  : 신뢰성 검증 이상 발생(정기)를 등록하는 팝업
    /// 생    성    자  : 유석진
    /// 생    성    일  : 2019-09-27
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ReliaVerifiResultRegularAbnormalOccurrencePopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }

        /// <summary>
        /// 팝업진입전 그리드의 컨트롤
        /// </summary>
        public ReliabilityVerificationAbnormalOccurrenceRegular ParentControl { get; set; }

        /// <summary>
        /// SmartBaseForm을 사용하기 위한 객체
        /// </summary>
        public SmartBaseForm _form = new SmartBaseForm();

        private DataTable _changedAffectLot; //저장 할 affectLot
        private DataTable _changedMainTable; // 특이사항 저장

        DataTable _dtInspectionFile;
        public bool isEnable = true;

        #endregion

        #region 생성자

        public ReliaVerifiResultRegularAbnormalOccurrencePopup(DataRow dr)
        {
            InitializeComponent();

            issueRegistrationControl1.isShowReasonCombo = true;
            issueRegistrationControl1.SetControlsVisible();

            CurrentDataRow = dr;

            InitializeControl();
            InitializeGrid();
            InitializeEvent();

            issueRegistrationControl1.ParentDataRow = CurrentDataRow;
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
            grdProductnformation.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore;

            grdProductnformation.View
                .AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 품목코드

            grdProductnformation.View
                .AddTextBoxColumn("PRODUCTDEFNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 품목명

            grdProductnformation.View
                .AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 품목Version

            grdProductnformation.View
                .AddTextBoxColumn("LOTID", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // LOT ID

            grdProductnformation.View
                .AddTextBoxColumn("PROCESSSEGMENTNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 표준공정

            grdProductnformation.View
                .AddTextBoxColumn("AREANAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 작업장

            grdProductnformation.View
                .AddTextBoxColumn("EQUIPMENTNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 설비(호기)

            grdProductnformation.View
                .AddTextBoxColumn("TRACKOUTTIME", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 작업종료시간

            grdProductnformation.View.PopulateColumns();
        }

        /// <summary>
        /// 제조 이력 그리드 초기화
        /// </summary>
        private void InitializeGrdMeasureHistory()
        {
            grdManufacturingHistory.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore;

            grdManufacturingHistory.View
                .AddTextBoxColumn("USERSEQUENCE", 100)
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
            tabMeasuredValue.SelectedPageChanged += TabMeasuredValue_SelectedPageChanged;

            // 이미지 저장 이벤트
            btnImageSave.Click += (s, e) =>
            {
                var enumerator = flowMeasuredPicture.Controls.GetEnumerator();
                var i = 0;

                while (enumerator.MoveNext())
                {
                    ReqularMultiVerificationResultControl vr = enumerator.Current as ReqularMultiVerificationResultControl;

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
                            ReqularMultiVerificationResultControl vr = enumerator.Current as ReqularMultiVerificationResultControl;

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
        /// 측정 사진 Tab을 선택 했을 경우 control을 보여줌
        /// </summary>
        private void TabMeasuredValue_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            SmartTabControl tab = sender as SmartTabControl;

            if (tab.SelectedTabPageIndex == 1)
            {
                // 2020.05.06-유석진-기존에 있던 control 삭제
                int _vCnt = flowMeasuredPicture.Controls.Count;

                for(int i= _vCnt-1; i>=0; i--)
                {
                    flowMeasuredPicture.Controls.RemoveAt(i);
                }

                SearchInspectionFile(); // 검증 결과 조회
            }
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
            switch (issueRegistrationControl1._tabIndex)
            {
                case 0:
                    CheckTempData(0, Format.GetString(issueRegistrationControl1.cboRequestNumber.EditValue), issueRegistrationControl1._requestDt);
                    break;
                case 1:
                    CheckTempData(1, Format.GetString(issueRegistrationControl1.cboReceiptNumber.EditValue), issueRegistrationControl1._receiptDt);
                    break;
                case 2:
                    CheckTempData(2, Format.GetString(issueRegistrationControl1.cboAcceptNumber.EditValue), issueRegistrationControl1._acceptDt);
                    break;
                case 3:
                    CheckTempData(3, Format.GetString(issueRegistrationControl1.cboValidationNumber.EditValue), issueRegistrationControl1._validationDt);
                    break;
            }

            if (this.ShowMessageBox("InfoSave", "Caption", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                // 저장할 데이터가 있는지 체크 후 저장
                CheckDataAndSaveCarAllState(issueRegistrationControl1);
                this.Close();
                ParentControl.Search();
            }
        }

        /// <summary>
        /// Form Road시 초기화
        /// </summary>
        private void ReliaVerifiResultRegularPopup_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = isEnable;

            grdAttachedFile.LanguageKey = "ATTACHEDFILE";
            grdReport.LanguageKey = "REPORT";
            btnImageSave.LanguageKey = "EXPORT";

            SearchProductInformation(); // 제품 정보 조회

            //SearchInspectionFile(); // 검증 결과 조회
            SearchMeasuringHistory(); // 제조 이력 조회
            SearchMeasuredValue(); // 측정값 조회
            SearchAttachFile(); // 첨부파일 조회
            SearchReport(); // 보고서

            issueRegistrationControl1._queryId = "GetReliabilityRegulaConcurrenceCount";
            issueRegistrationControl1._queryVersion = "10001";
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
            if (issueRegistrationControl1.IsInputData(tapIndex))
            {
                if (!issueRegistrationControl1.CheckIsTempSaved(sequence, tempDt))
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
                {"ENTERPRISEID",UserInfo.Current.Enterprise},
                {"PLANTID",UserInfo.Current.Plant},
                {"LANGUAGETYPE",UserInfo.Current.LanguageType},
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
            values.Add("P_ISJUDGMENTRESULT", "*");
            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dt = SqlExecuter.Query("GetReliaVerifiResultRegularlist", "10001", values);
            dt.Columns.Add(new DataColumn("REQUESTQTY", typeof(string)));
            dt.Columns.Add(new DataColumn("REQUESTREASONS", typeof(string)));


            grdProductnformation.DataSource = dt;
        }

        /// <summary>
        /// 검증 결과 조회
        /// </summary>
        private void SearchInspectionFile()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_INSPECTIONTYPE", "ReliaVerifiResultRegular");
            param.Add("P_RESOURCEID", CurrentDataRow["LOTID"].ToString());//품목 + LOT
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

                            ReqularMultiVerificationResultControl vr = new ReqularMultiVerificationResultControl(image, fileName, flowMeasuredPicture);
                            vr.setTitle(title);
                            vr.txtTitle.Enabled = false;
                            vr.btnAdd.Enabled = false;
                            vr.btnDelete.Enabled = false;
                            vr.btnClose.Enabled = false;
                            vr.Tag = dr["FILEID"].ToString();
                            flowMeasuredPicture.Controls.Add(vr);
                            // 2020.03.01-유석진-추가,삭제버튼 비활성화
                            vr.grdResultValue.View.SetIsReadOnly(true);
                            vr.grdMeasureValue.View.SetIsReadOnly(true);

                            // 2020.05.04-유석진-결과값 검증항목 그리드 맵핑
                            DataTable resultDt = vr.grdResultValue.DataSource as DataTable;

                            foreach (DataRow resultDr in resultDt.Rows)
                            {
                                // 2020.03.01-유석진-등록된 item, value 조회하는 로직 적용
                                for (int i = 0; i < 30; i++)
                                {
                                    if (!string.IsNullOrEmpty(dr["MEASUREITEMID" + (i + 1)].ToString()))
                                    {
                                        if (resultDr["MEASUREITEMID"].Equals(dr["MEASUREITEMID" + (i + 1)].ToString()))
                                        {
                                            if (i > 0)
                                                resultDr["MEASUREVALUE"] = dr["MEASUREVALUE" + (i + 1)];
                                            else
                                                resultDr["MEASUREVALUE"] = dr["MEASUREVALUE"];
                                        }
                                    }
                                }
                            }

                            // 2020.05.04-유석진-측정값 검증항목 그리드 맵핑
                            DataTable measureDt = vr.grdMeasureValue.DataSource as DataTable;

                            foreach (DataRow measureDr in measureDt.Rows)
                            {
                                // 2020.03.01-유석진-등록된 item, value 조회하는 로직 적용
                                for (int i = 0; i < 30; i++)
                                {
                                    if (!string.IsNullOrEmpty(dr["MEASUREITEMID" + (i + 1)].ToString()))
                                    {
                                        if (measureDr["MEASUREITEMID"].Equals(dr["MEASUREITEMID" + (i + 1)].ToString()))
                                        {
                                            if (i > 0)
                                                measureDr["MEASUREVALUE"] = dr["MEASUREVALUE" + (i + 1)];
                                            else
                                                measureDr["MEASUREVALUE"] = dr["MEASUREVALUE"];
                                        }
                                    }
                                }
                            }
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
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("P_REQUESTNO", CurrentDataRow["REQUESTNO"].ToString());
            values.Add("P_ENTERPRISEID", CurrentDataRow["ENTERPRISEID"].ToString());
            values.Add("P_PLANTID", CurrentDataRow["PLANTID"].ToString());
            values.Add("P_LOTID", CurrentDataRow["LOTID"].ToString());
            //values.Add("P_LOTWORKTXNHISTKEY", CurrentDataRow["TXNHISTKEY"].ToString());
            values.Add("P_MEASURETYPE", "MeasuredValue");

            DataTable dt = SqlExecuter.Query("GetQcreliabilityMeasureList", "10001", values);

            if (dt.Rows.Count > 0)
            {
                cboUnit.EditValue = dt.Rows[0]["UNIT"]; // 단위 설정
            }

            grdMeasureValue.DataSource = dt;
        }

        /// <summary>
        /// 첨부파일 조회
        /// </summary>
        private void SearchAttachFile()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("P_FILERESOURCETYPE", "ReliaVerifiResultRegularAttach");
            values.Add("P_FILERESOURCEID", CurrentDataRow["REQUESTNO"].ToString() + CurrentDataRow["LOTID"].ToString());
            values.Add("P_FILERESOURCEVERSION", "1");

            DataTable dt = SqlExecuter.Query("SelectFileInspResult", "10001", values);

            grdAttachedFile.DataSource = dt;
        }

        /// <summary>
        /// 보고서 조회
        /// </summary>
        private void SearchReport()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("P_FILERESOURCETYPE", "ReliaVerifiResultRegularReport");
            values.Add("P_FILERESOURCEID", CurrentDataRow["REQUESTNO"].ToString() + CurrentDataRow["LOTID"].ToString());
            values.Add("P_FILERESOURCEVERSION", "1");

            DataTable dt = SqlExecuter.Query("SelectFileInspResult", "10001", values);

            grdReport.DataSource = dt;
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

        #endregion

        #region 저장

        /// <summary>
        /// CAR 요청 저장
        /// </summary>
        private void SaveCarRequest()
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(issueRegistrationControl1.RequestCarData());

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
            DataSet ds = issueRegistrationControl1.ReceiptCarData();
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
            ds.Tables.Add(issueRegistrationControl1.AcceptCarData());
            ds.Tables.Add(_changedAffectLot);
            ds.Tables.Add(_changedMainTable);

            ExecuteRule("SaveCarAccept", ds);
        }

        /// <summary>
        /// 유효성평가 저장
        /// </summary>
        private void SaveCarValidation()
        {
            DataSet ds = issueRegistrationControl1.ValidationCarData();
            ds.Tables.Add(_changedAffectLot);
            ds.Tables.Add(_changedMainTable);

            ExecuteRule("SaveCarValidation", ds);
        }

        private DataTable SaveAffectLot(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                row["ABNOCRNO"] = CurrentDataRow["ABNOCRNO"];
                row["ABNOCRTYPE"] = CurrentDataRow["ABNOCRTYPE"];

                if (row["_STATE_"].ToString().Equals("modified"))
                {
                    if (string.IsNullOrWhiteSpace(row["LASTTXNID"].ToString()))
                    {
                        row["_STATE_"] = "added";
                    }
                }
            }
            return dataTable;
        }

        #endregion
    }
}
