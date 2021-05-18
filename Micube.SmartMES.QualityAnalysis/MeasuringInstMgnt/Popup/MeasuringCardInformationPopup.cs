#region using
using Micube.Framework.SmartControls;
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
using System.Diagnostics;
using Micube.Framework;
using SmartDeploy.Common;
using Micube.SmartMES.Commons.Controls;
using Micube.Framework.Net;
using Micube.Framework.Log;
using Micube.SmartMES.QualityAnalysis.Helper;
using DevExpress.XtraEditors.Controls;
using Micube.SmartMES.Commons;
#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 계측기 관리> 이력카드 등록> 이력카드 등록 Popup
    /// 업  무  설  명  : 계측기 이력카드 정보 등록 Popup.
    /// 생    성    자  : 이승우
    /// 생    성    일  : 2019-10-01
    /// 수  정  이  력  : 2020-01-29 강유라 
    ///                  2019-10-29 Layout 변경
    ///                  2019-10-01 최초 생성일
    ///                  2021.02.23 전우성 검교정 파일리스트 파일이름으로 정렬순서 변경
    /// </summary>
    public partial class MeasuringCardInformationPopup : SmartPopupBaseForm, ISmartCustomPopup
    {

        #region Interface

        public DataRow CurrentDataRow { get; set; }

        #endregion

        #region Local Variables
        private bool _isControlinitial = true;
        private string _result = "";
        private DataRow _rowParent;
        /// <summary>
        /// dateTime Control format 지정.
        /// </summary>
        private string _dateDisplayFormat = "yyyy-MM-dd";
        /// <summary>
        /// 박스 그리기 유무
        /// </summary>
        private bool _isDrawRectangle = true;
        private string _inspectionDefId = "MeasuringCard01";
        private DataTable _displayPhotoList;
        //2020-01-30 강유라 신규, 수정 구분위한 플레그
        private bool _isNewData;

        //2020-02-06 보고서, 이력카드 관련 테이블
        private DataTable _reportFileDt;
        private DataTable _cardFileDt;
        private DataTable _documentFileDt;

        private string reportFilePath ="";
        private string reportSafeFileName="";

        private string cardFilePath="";
        private string cardSafeFileName="";

        private string documentFilePath = "";
        private string documentSafeFileName = "";

        private Image cardImage;
        private bool _doCheck = false;
        #endregion

        #region 생성자
        //2020-01-30 강유라 신규, 수정 구분위한 플레그 추가
        public MeasuringCardInformationPopup(bool flag)
        {
            _isNewData = flag;
            InitializeComponent();
            InitializeGridScrap();
            InitializeEvent();

            fpcEquipmentImage.showImage = true;
            fpcCalibration.countRows = true;
            fpcMiddle.countRows = true;
            fpcRNR.countRows = true;

            VisibleProcessSegmentContorol();
        }
        #endregion

        #region 컨텐츠 영역 초기화
        //Grid - 수리/폐기 이력 정보
        private void InitializeGridScrap()
        {
            grdRepairScrap.GridButtonItem = GridButtonItem.Export;
            grdRepairScrap.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdRepairScrap.View.SetIsReadOnly();

            grdRepairScrap.View.AddTextBoxColumn("OCCURTIME", 150).SetLabel("OCCURTIME").SetDisplayFormat("yyyy-MM-dd HH;mm:ss");//내용
            grdRepairScrap.View.AddTextBoxColumn("REPAIRSCRAPID", 150).SetLabel("MEASURINGREPAIRSCRAPID");//수리폐기ID PK
            grdRepairScrap.View.AddTextBoxColumn("SERIALNO", 150).SetLabel("MEASURINGSERIALNO").SetIsHidden();//시리얼번호 PK
            grdRepairScrap.View.AddTextBoxColumn("CONTROLNO", 150).SetLabel("MEASURINGCONTROLNO").SetIsHidden();//관리번호 PK
            grdRepairScrap.View.PopulateColumns();
        }

        #endregion

        #region Event
        private void InitializeEvent()
        {
            Load += (s, e) =>
            {
                this.SetComboBoxCreateValueEquipmentType();
                //this.SetComboBoxCreateValuePlantId(); 일반적인 PLANT 사용 안함
                this.SetComboBoxCreateValueYesNo();
                this.SetComboBoxCreateValueResult();
                this.SetComboCreateValidstate();
                this.SetComboCreateIsMiddleDateMode();
                //2020-01-29 강유라 계측기 관리번호 코드 콤보박스 초기화
                this.SetComboBoxCreateValueMeasureCode();
                //2020-01-29 강유라 계측기 사진 컨트롤 버튼 생성
                InitializeContent();
                //2020-01-30 강유라 신규, 수정data에 따른 컨트롤 설정
                SetPKReadOnlyAndEvent();
                //2020-01-30 강유라 spinEdit 하한값 설정
                SetSpingEditFormat();                
                this.DataEditRead();
                //2020-02-06 보고서, 이력카드 Dt 생성
                CreateFileDataTable();

                SetReadOnlyControls();
            };

            btnSave.Click += btnSave_Click;

            btnClose.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            //2020-02-06 강유라 보고서, 이력카드 파일등록 이벤트
            btnReportFile.ButtonClick += BtnReportFile_ButtonClick;
            btnCardFile.ButtonClick += BtnCardFile_ButtonClick;

            //2020-02-17 강유라 경위서 파일 등록 이벤트
            btnDetailDocFile.ButtonClick += BtnDocumentFile_ButtonClick;

            //2020-02-17 
            if(!isReadOnlyControls)
            {
                //2020-02-17 검사여부에 따라 하위 항목 readOnly 처리
                cboisInspectionCalibration.EditValueChanged += (s, e) => {

                    if (Format.GetString(cboisInspectionCalibration.EditValue).Equals("Y"))
                    {
                        spinInspectionCalibrationCycle.ReadOnly = false;
                        dteEndCalibrationDate.ReadOnly = false;

                        //int fileCount = (fpcCalibration.DataSource as DataTable).Rows.Count;
                        //cboCalibrationResult.EditValue = fileCount > 0 ? "Pass" : "Fail";

                    }
                    else
                    {
                        dteEndCalibrationDate.EditValue = DBNull.Value;
                        dteNextCalibrationDate.EditValue = DBNull.Value;
                        spinInspectionCalibrationCycle.EditValue = 1;
                        cboCalibrationResult.EditValue = string.Empty;

                        spinInspectionCalibrationCycle.ReadOnly = true;
                        dteEndCalibrationDate.ReadOnly = true;
                    }
                };

                //2020-02-17 검사여부에 따라 하위 항목 readOnly 처리
                cboIsMiddle.EditValueChanged += (s, e) => {

                    if (Format.GetString(cboIsMiddle.EditValue).Equals("Y"))
                    {
                        spinMiddleCheckCycle.ReadOnly = false;
                        dteMiddleCheckDate.ReadOnly = false;
                        cboIsMiddleDateMode.ReadOnly = false;
                        cboIsMiddleDateMode.ItemIndex = 0;
                        //int fileCount = (fpcMiddle.DataSource as DataTable).Rows.Count;
                        //cboMiddleCheckResult.EditValue = fileCount > 0 ? "Pass" : "Fail";
                    }
                    else
                    {
                        dteMiddleCheckDate.EditValue = DBNull.Value;
                        dteNextMiddleCheckDate.EditValue = DBNull.Value;
                        spinMiddleCheckCycle.EditValue = 1;
                        cboMiddleCheckResult.EditValue = string.Empty;
                        cboIsMiddleDateMode.ItemIndex = 0;
                        cboIsMiddleDateMode.ReadOnly = true;
                        spinMiddleCheckCycle.ReadOnly = true;
                        dteMiddleCheckDate.ReadOnly = true;
                    }
                };

                //2020-02-17 검사여부에 따라 하위 항목 readOnly 처리
                cboisRNR.EditValueChanged += (s, e) => {

                    if (Format.GetString(cboisRNR.EditValue).Equals("Y"))
                    {
                        cboisRNRTarget.ReadOnly = false;
                        spinRNRCycle.ReadOnly = false;
                        dteRNRProgressDate.ReadOnly = false;

                        //int fileCount = (fpcRNR.DataSource as DataTable).Rows.Count;
                        //cboRNRResult.EditValue = fileCount > 0 ? "Pass" : "Fail";
                    }
                    else
                    {
                        dteRNRProgressDate.EditValue = DBNull.Value;
                        dteRNRNextProgressDate.EditValue = DBNull.Value;
                        spinRNRCycle.EditValue = 1;
                        cboRNRResult.EditValue = string.Empty;

                        //2020-03-18 강유라
                        cboisRNRTarget.EditValue = "N";
                        cboisRNRTarget.ReadOnly = true;
                        spinRNRCycle.ReadOnly = true;
                        dteRNRProgressDate.ReadOnly = true;
                    }
                };

                dteEndCalibrationDate.EditValueChanged += DteEndCalibrationDate_EditValueChanged;

                spinInspectionCalibrationCycle.EditValueChanged += DteEndCalibrationDate_EditValueChanged;

                cboIsMiddleDateMode.EditValueChanged += DteMiddleCheckDate_EditValueChanged;

                dteMiddleCheckDate.EditValueChanged += DteMiddleCheckDate_EditValueChanged;

                spinMiddleCheckCycle.EditValueChanged += DteMiddleCheckDate_EditValueChanged;

                dteRNRProgressDate.EditValueChanged += DteRNRProgressDate_EditValueChanged;
               
                spinRNRCycle.EditValueChanged += DteRNRProgressDate_EditValueChanged;
            }

            //2020-02-18 강유라 파일컨트롤의 포커스된 row바뀔 때이벤트
            fpcEquipmentImage.FocusedFileRowChangedEvent += (row) => BindingImage(row);
            fpcCalibration.FileRowCountChangedEvent += (rowCount) => SetCalibrationResult(rowCount);
            fpcMiddle.FileRowCountChangedEvent += (rowCount) => SetMiddleResult(rowCount);
            fpcRNR.FileRowCountChangedEvent += (rowCount) => SetRNRResult(rowCount);
          
        }

        private void DteRNRProgressDate_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Format.GetString(dteRNRProgressDate.EditValue)) ||
                     string.IsNullOrWhiteSpace(Format.GetString(spinRNRCycle.EditValue)))
                return;

            string temp = GetTempDateStrig(Format.GetString(dteRNRProgressDate.EditValue));

            DateTime date = Convert.ToDateTime(temp);
            DateTime nextDate = date.AddMonths(Format.GetInteger(spinRNRCycle.EditValue));
            dteRNRNextProgressDate.EditValue = nextDate;
        }

        private void DteMiddleCheckDate_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Format.GetString(dteMiddleCheckDate.EditValue)) ||
                       string.IsNullOrWhiteSpace(Format.GetString(spinMiddleCheckCycle.EditValue)))
                return;

            string temp = GetTempDateStrig(Format.GetString(dteMiddleCheckDate.EditValue));
            DateTime date = Convert.ToDateTime(temp);

            string IsMiddleDateMode = "";
            IsMiddleDateMode = this.cboIsMiddleDateMode.EditValue.ToSafeString();
            DateTime nextDate;
            switch (IsMiddleDateMode)
            {
                case "DAY":
                    nextDate = date.AddDays(Format.GetInteger(spinMiddleCheckCycle.EditValue));
                    break;
                case "MONTH":
                    nextDate = date.AddMonths(Format.GetInteger(spinMiddleCheckCycle.EditValue));
                    break;
                case "YEAR":
                    nextDate = date.AddYears(Format.GetInteger(spinMiddleCheckCycle.EditValue));
                    break;
                default:
                    cboIsMiddleDateMode.ItemIndex = 0;
                    nextDate = date.AddMonths(Format.GetInteger(spinMiddleCheckCycle.EditValue));
                    break;
            }
            
            dteNextMiddleCheckDate.EditValue = nextDate;
        }

        private void DteEndCalibrationDate_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Format.GetString(dteEndCalibrationDate.EditValue)) ||
                        string.IsNullOrWhiteSpace(Format.GetString(spinInspectionCalibrationCycle.EditValue)))
                return;

            string temp = GetTempDateStrig(Format.GetString(dteEndCalibrationDate.EditValue));

            DateTime date = Convert.ToDateTime(temp);
            DateTime nextDate = date.AddMonths(Format.GetInteger(spinInspectionCalibrationCycle.EditValue));
            dteNextCalibrationDate.EditValue = nextDate;
        }

        /*
        private void PnlFirst_Paint(object sender, PaintEventArgs e)
        {         
            ControlPaint.DrawBorder(e.Graphics, this.smartPanel3.ClientRectangle, Color.Blue, ButtonBorderStyle.Solid);      
        }*/


        /// <summary>
        /// Form Load 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeasuringCardInformationPopup_Load(object sender, EventArgs e)
        {
            _isControlinitial = true;
            InitializePopup();
            this.grdRepairScrap.View.DoubleClick += GrdRepairScrap_DoubleClick;
            _isControlinitial = false;
        }

        /// <summary>
        /// 수리/폐기 목록 Grid Double Click 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdRepairScrap_DoubleClick(object sender, EventArgs e)
        {
            // 등록 팝업
            if (grdRepairScrap.View.FocusedRowHandle < 0) return;

            DataRow rowScrapGroup = this.grdRepairScrap.View.GetFocusedDataRow();

            if (rowScrapGroup == null) return;

            DialogManager.ShowWaitArea(pnlMain);
            MeasuringInstRepairPopup frmPopup = new MeasuringInstRepairPopup(false, true);
            frmPopup.SetRowParent(rowScrapGroup);
            frmPopup.StartPosition = FormStartPosition.CenterParent;//폼 중앙 배치.
            frmPopup.Owner = this;

            DialogManager.CloseWaitArea(this.pnlMain);

            frmPopup.ShowDialog();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_isControlinitial) return;

            //2020-01-29 강유라 시리얼 번호, 관리번호 입력 필수
            if (string.IsNullOrWhiteSpace(Format.GetString(txtSerialNo.EditValue)) ||
                string.IsNullOrWhiteSpace(Format.GetString(txtControlNo.EditValue)))
            {
                throw MessageException.Create("NoSerialAndControlNo");
            }

            if (_isNewData && _doCheck==false)
            {
                txtControlNo.Focus();
                throw MessageException.Create("DoCheckSerialAndControlNoExist");//입력한 시리얼번호와 관리번호의 중복체크를 해주세요.
            }
            
            string areaId = popupAreaId.GetValue().ToString();
            //cboEquipmentType
            //// TODO : 저장 Rule 변경
            DataTable data = CreateDataTable();

            try
            {
                btnSave.Enabled = false;
                btnClose.Enabled = false;
                this.ShowWaitArea();
                this.DataEditSave(ref data);


                //2020-01-29 강유라 사진 업로드 로직
                //새로 추가 되거나 수정되었을 때만 저장
                DataTable totalFileDt = QcmImageHelper.GetImageFileTable();
                int totalFileSize = 0;

                if (Format.GetString(_rowParent["CHANGEFLAG"]).Equals("added"))
                { 
                    DataTable fileUploadTable = QcmImageHelper.GetImageFileTable();
                    DataRow newRow = fileUploadTable.NewRow();

                    newRow["FILEID"] = _rowParent["FILEID"];
                    newRow["FILENAME"] = _rowParent["FILENAME"];
                    newRow["FILEEXT"] = _rowParent.GetString("FILEEXT").Replace(".", "");//파일업로드시에는 확장자에서 . 을 빼야 한다.
                    newRow["FILEPATH"] = _rowParent["FILEPATH"];// originRow["FILEFULLPATH1"];
                    newRow["SAFEFILENAME"] = _rowParent["FILENAME"];
                    newRow["FILESIZE"] = _rowParent["FILESIZE"];
                    newRow["LOCALFILEPATH"] = _rowParent["FILEFULLPATH"];
                    newRow["PROCESSINGSTATUS"] = "Wait";
                    newRow["SEQUENCE"] = 1;


                    //서버에서 데이타베이스를 입력하기 위해 파일아이디를 전달해야 한다.
                    totalFileSize += _rowParent.GetInteger("FILESIZE");

                    fileUploadTable.Rows.Add(newRow);

                    totalFileDt.Merge(fileUploadTable.Copy());
                }

                if (Format.GetString(_rowParent["REPORTCHANGEFLAG"]).Equals("added"))
                {
                    totalFileSize += _reportFileDt.Rows[0].GetInteger("FILESIZE");
                    totalFileDt.Merge(_reportFileDt.Copy());
                }

                if (Format.GetString(_rowParent["CARDCHANGEFLAG"]).Equals("added"))
                {
                    totalFileSize += _cardFileDt.Rows[0].GetInteger("FILESIZE");
                    totalFileDt.Merge(_cardFileDt.Copy());
                }
                

                if (Format.GetString(_rowParent["DOCUMENTCHANGEFLAG"]).Equals("added"))
                {
                    totalFileSize += _documentFileDt.Rows[0].GetInteger("FILESIZE");
                    totalFileDt.Merge(_documentFileDt.Copy());
                }

                DataTable equipmentImage = fpcEquipmentImage.GetChangedRows();
                DataTable calibrationFileDt = fpcCalibration.GetChangedRows();
                DataTable middleFileDt = fpcMiddle.GetChangedRows();
                DataTable rnrFileDt = fpcRNR.GetChangedRows();

                //2020-02-17 강유라 계측기 이미지 파일 서버에 올릴 데이터
                if (equipmentImage.Rows.Count > 0)
                {
                    foreach (DataRow row in equipmentImage.Rows)
                    {
                        if (Format.GetString(row["_STATE_"]).Equals("deleted"))
                            continue;
                      
                        DataRow newRow = totalFileDt.NewRow();

                        newRow["FILENAME"] = row["FILENAME"];
                        newRow["FILEEXT"] = row["FILEEXT"];
                        newRow["FILEPATH"] = "MeasurementCard/MeasurementImage"; ;
                        newRow["SAFEFILENAME"] = row["SAFEFILENAME"];
                        newRow["LOCALFILEPATH"] = row["LOCALFILEPATH"];
                        newRow["PROCESSINGSTATUS"] = "Wait";
                        newRow["SEQUENCE"] = row["SEQUENCE"];

                        totalFileDt.Rows.Add(newRow);


                    }
                }

                //2020-02-17 강유라 검교정 파일 서버에 올릴 데이터
                if (calibrationFileDt.Rows.Count > 0)
                {
                    foreach (DataRow row in calibrationFileDt.Rows)
                    {
                        if (Format.GetString(row["_STATE_"]).Equals("deleted"))
                            continue;

                        DataRow newRow = totalFileDt.NewRow();

                        newRow["FILENAME"] = row["FILENAME"];
                        newRow["FILEEXT"] = row["FILEEXT"];
                        newRow["FILEPATH"] = "MeasurementCard/CalibrationFile"; ;
                        newRow["SAFEFILENAME"] = row["SAFEFILENAME"];
                        newRow["LOCALFILEPATH"] = row["LOCALFILEPATH"];
                        newRow["PROCESSINGSTATUS"] = "Wait";
                        newRow["SEQUENCE"] = row["SEQUENCE"];

                        totalFileDt.Rows.Add(newRow);
                    }
                }

                //2020-02-17 강유라 중간점검 파일 서버에 올릴 데이터
                if (middleFileDt.Rows.Count > 0)
                {
                    foreach (DataRow row in middleFileDt.Rows)
                    {
                        // 2021-01-28 오근영 파일삭제 관련 추가
                        if (Format.GetString(row["_STATE_"]).Equals("deleted"))
                            continue;

                        DataRow newRow = totalFileDt.NewRow();

                        newRow["FILENAME"] = row["FILENAME"];
                        newRow["FILEEXT"] = row["FILEEXT"];
                        newRow["FILEPATH"] = "MeasurementCard/MiddleFile"; ;
                        newRow["SAFEFILENAME"] = row["SAFEFILENAME"];
                        newRow["LOCALFILEPATH"] = row["LOCALFILEPATH"];
                        newRow["PROCESSINGSTATUS"] = "Wait";
                        newRow["SEQUENCE"] = row["SEQUENCE"];

                        totalFileDt.Rows.Add(newRow);
                    }
                }

                //2020-02-17 강유라 R&R 파일 서버에 올릴 데이터
                if (rnrFileDt.Rows.Count > 0)
                {
                    // 2021-01-28 오근영 파일삭제 관련 추가
                    foreach (DataRow row in rnrFileDt.Rows)
                    {
                        if (Format.GetString(row["_STATE_"]).Equals("deleted"))
                            continue;

                        DataRow newRow = totalFileDt.NewRow();

                        newRow["FILENAME"] = row["FILENAME"];
                        newRow["FILEEXT"] = row["FILEEXT"];
                        newRow["FILEPATH"] = "MeasurementCard/RNRFile"; ;
                        newRow["SAFEFILENAME"] = row["SAFEFILENAME"];
                        newRow["LOCALFILEPATH"] = row["LOCALFILEPATH"];
                        newRow["PROCESSINGSTATUS"] = "Wait";
                        newRow["SEQUENCE"] = row["SEQUENCE"];

                        totalFileDt.Rows.Add(newRow);
                    }
                }


                if (totalFileDt.Rows.Count > 0)
                {
                    FileProgressDialog fileProgressDialog = new FileProgressDialog(totalFileDt, UpDownType.Upload, "", totalFileSize);
                    fileProgressDialog.ShowDialog(this);

                    if (fileProgressDialog.DialogResult == DialogResult.Cancel)
                        throw MessageException.Create("CancelMessageToUploadFile");  //파일업로드를 취소하였습니다.

                    ProgressingResult fileResult = fileProgressDialog.Result;

                    if (!fileResult.IsSuccess)
                        throw MessageException.Create("FailMessageToUploadFile"); //파일업로드를 실패하였습니다.
                }

                MessageWorker worker = new MessageWorker("SaveMeasuringinstCard");
                worker.SetBody(new MessageBody()
                    {
                        { "list", data},
                        { "list2", _reportFileDt},
                        { "list3", _cardFileDt},
                        { "list4", _documentFileDt},

                        { "list5", equipmentImage.Copy()},
                        { "list6", calibrationFileDt.Copy()},
                        { "list7", middleFileDt.Copy()},
                        { "list8", rnrFileDt.Copy()},
                    });

                worker.Execute();

            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
            finally
            {
                this.DialogResult = DialogResult.OK;
                this.CloseWaitArea();
                btnSave.Enabled = true;
                btnClose.Enabled = true;
                this.Close();
            }

        }
        /// <summary>
        /// 입력 자료 저장 Data Table 구성.
        /// </summary>
        /// <param name="data"></param>
        private void DataEditSave(ref DataTable data)
        {
            DataRow dataRow = data.NewRow();

            dataRow["SERIALNO"] = txtSerialNo.Text;
            dataRow["CONTROLNO"] = _isNewData?Format.GetString(cboControlNoPrefix.EditValue) + Format.GetString(txtControlNo.Text):Format.GetString(_rowParent["CONTROLNO"]);
            dataRow["CONTROLNAME"] = txtControlName.Text;
            dataRow["TYPESPEC"] = memoSpec.Text;
            dataRow["EQUIPMENTTYPE"] = cboEquipmentType.GetDataValue();
            dataRow["MEASURINGID"] = txtMeasuringId.EditValue;
            dataRow["MEASURINGPRODUCTID"] = txtMeasuringProductdefId.EditValue;
            dataRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            //2020-01-31 강유라
            dataRow["AREAID"] = popupAreaId.GetValue();
            dataRow["PLANTID"] = txtPlantId.Text;
            dataRow["UNIT"] = txtSpecNuit.Text;
            //2020-01-29 강유라 상한/하한 => 규격에서 관리로 주석처리
            //dataRow["LSL"] = spinUSL.Text;
            //dataRow["USL"] = spinLSL.Text;
            dataRow["MANUFACTURECOUNTRY"] = txtManuFactureCountry.Text;
            dataRow["MANUFACTURER"] = txtManuFacturer.Text;
            //2020-01-31 강유라
            //dataRow["PROCESSSEGMENTID"] = txtProcesssegMentId.Tag;
            dataRow["PROCESSSEGMENTID"] = UserInfo.Current.Enterprise.Equals("INTERFLEX")? popupProcesssegmentId.GetValue(): txtProcessSegmentId.Text;
            //dataRow["PROCESSSEGMENTVERSION"] = "XX";
            dataRow["DEPARTMENT"] = popupDepartment.GetValue().ToString();
            dataRow["PURCHASEDATE"] = string.IsNullOrWhiteSpace(Format.GetString(dtePurchaseDate.EditValue)) ? DBNull.Value : dtePurchaseDate.EditValue;
            dataRow["PURCHASEAMOUNT"] = numPurchaseAmount.Text;
            dataRow["ISPC"] = cboisPC.GetDataValue();
            dataRow["OSVERSION"] = txtOSVersion.Text;
            dataRow["SORTWAREVERSION"] = txtSortWareVersion.Text;
            dataRow["SCRAPDATE"] = string.IsNullOrWhiteSpace(Format.GetString(dteScrapDate.EditValue)) ? DBNull.Value : dteScrapDate.EditValue;
            dataRow["ISSCRAP"] = cboisScrap.GetDataValue();
            //dataRow["REPAIRDISPOSALID"] = "XX";//수리폐이력 ID
            dataRow["INSPECTIONCALIBRATIONCYCLE"] = spinInspectionCalibrationCycle.Text;
            dataRow["ISINSPECTIONCALIBRATION"] = cboisInspectionCalibration.GetDataValue();
            dataRow["ENDCALIBRATIONDATE"] = string.IsNullOrWhiteSpace(Format.GetString(dteEndCalibrationDate.EditValue)) ? DBNull.Value : dteEndCalibrationDate.EditValue;
            dataRow["NEXTCALIBRATIONDATE"] = string.IsNullOrWhiteSpace(Format.GetString(dteNextCalibrationDate.EditValue)) ? DBNull.Value : dteNextCalibrationDate.EditValue; 
            dataRow["CALIBRATIONRESULT"] = cboCalibrationResult.GetDataValue();
            dataRow["ISMIDDLE"] = cboIsMiddle.EditValue;
            dataRow["MIDDLECHECKCYCLETYPE"] = cboIsMiddleDateMode.EditValue;//2020-04-28 이승우 추가
            dataRow["MIDDLECHECKCYCLE"] = spinMiddleCheckCycle.Text;
            dataRow["MIDDLECHECKDATE"] = string.IsNullOrWhiteSpace(Format.GetString(dteMiddleCheckDate.EditValue)) ? DBNull.Value : dteMiddleCheckDate.EditValue;
            dataRow["NEXTMIDDLECHECKDATE"] = string.IsNullOrWhiteSpace(Format.GetString(dteNextMiddleCheckDate.EditValue)) ? DBNull.Value : dteNextMiddleCheckDate.EditValue;
            dataRow["NEXTMIDDLECHECKRESULT"] = cboMiddleCheckResult.GetDataValue();
            dataRow["ISRNRTARGET"] = cboisRNRTarget.GetDataValue();
            dataRow["RNRCYCLE"] = spinRNRCycle.Text;
            dataRow["ISRNR"] = cboisRNR.GetDataValue();
            dataRow["RNRPROGRESSDATE"] = string.IsNullOrWhiteSpace(Format.GetString(dteRNRProgressDate.EditValue)) ? DBNull.Value : dteRNRProgressDate.EditValue;
            dataRow["RNRNEXTPROGRESSDATE"] = string.IsNullOrWhiteSpace(Format.GetString(dteRNRNextProgressDate.EditValue)) ? DBNull.Value : dteRNRNextProgressDate.EditValue;
            dataRow["RNRRESULT"] = cboRNRResult.GetDataValue();
            //2020-01-29 강유라 설비이미지 컨트롤 변경 으로 주석 처리
            //dataRow["EQUIPMENTIMAGE"] = txtEquipmentImage.Tag;
            //dataRow["PHOTO"] = txtEquipmentImage.Text;
            //2020-01-30 강유라 설비사진
            dataRow["CHANGEFLAG"] = _rowParent["CHANGEFLAG"];
            dataRow["FILENAME"] = _rowParent["FILENAME"];
            dataRow["FILESIZE"] = _rowParent["FILESIZE"];
            dataRow["FILEEXT"] = _rowParent["FILEEXT"];
            dataRow["FILEPATH"] = _rowParent["FILEPATH"];

            dataRow["ISMEASURINGINSTRUMENT"] = cboisMeasuringInstrument.GetDataValue();
            dataRow["COMMENTS"] = txtComments.Text;
            //2020-01-29 강유라 
            dataRow["VALIDSTATE"] = cboValid.EditValue; ;

            data.Rows.Add(dataRow);


        }

        /// <summary>
        /// 입력 자료 저장 Data Table 구성.
        /// </summary>
        /// <param name="data"></param>
        private void DataEditRead()
        {
            if (!_isNewData)
            {
                txtSerialNo.Text = _rowParent["SERIALNO"].ToSafeString();
                cboControlNoPrefix.EditValue = _rowParent["CONTROLNOPREFIX"].ToSafeString();
                txtControlNo.Text = _rowParent["CONTROLNO"].ToSafeString();

                //계측기
                txtMeasuringId.EditValue = _rowParent["MEASURINGID"].ToSafeString();
                txtMeasuringProductdefId.EditValue = _rowParent["MEASURINGPRODUCTID"].ToSafeString();
     
                //작업장
                popupAreaId.SetValue(_rowParent["AREAID"].ToSafeString());
                popupAreaId.Text = _rowParent["AREANAME"].ToSafeString();
   

                txtSpecNuit.Text = _rowParent["UNIT"].ToSafeString();
                //2020-01-29 강유라 상한/하한 => 규격에서 관리로 주석처리
                //spinLSL.Text = _rowParent["LSL"].ToSafeString();
                //spinUSL.Text = _rowParent["USL"].ToSafeString();
                txtManuFactureCountry.Text = _rowParent["MANUFACTURECOUNTRY"].ToSafeString();
                txtManuFacturer.Text = _rowParent["MANUFACTURER"].ToSafeString();
                //공정
                if (UserInfo.Current.Enterprise.Equals("INTERFLEX"))
                {
                    popupProcesssegmentId.SetValue(_rowParent["PROCESSSEGMENTID"].ToSafeString());
                    popupProcesssegmentId.Text = _rowParent["PROCESSSEGMENTNAME"].ToSafeString();
                }
                else
                {
                    txtProcessSegmentId.Text = _rowParent["PROCESSSEGMENTID"].ToSafeString();
                }

                popupDepartment.SetValue(_rowParent["DEPARTMENT"].ToSafeString());
                popupDepartment.Text = _rowParent["DEPARTMENT"].ToSafeString();

                cboValid.EditValue = _rowParent["VALIDSTATE"].ToSafeString();
                dtePurchaseDate.EditValue = _rowParent["PURCHASEDATE"].ToSafeString();
                numPurchaseAmount.Text = _rowParent["PURCHASEAMOUNT"].ToSafeString();
                cboisPC.EditValue = _rowParent["ISPC"].ToSafeString();
                txtOSVersion.Text = _rowParent["OSVERSION"].ToSafeString();
                txtSortWareVersion.Text = _rowParent["SORTWAREVERSION"].ToSafeString();

                dteScrapDate.EditValue = _rowParent["SCRAPDATE"].ToSafeString();

                cboisScrap.EditValue = _rowParent["ISSCRAP"].ToSafeString();
                //"XX" = //수리폐이력 ID//_rowParent["REPAIRDISPOSALID"]
                spinInspectionCalibrationCycle.Text = _rowParent["INSPECTIONCALIBRATIONCYCLE"].ToSafeString();
                cboisInspectionCalibration.EditValue = _rowParent["ISINSPECTIONCALIBRATION"].ToSafeString();

                dteEndCalibrationDate.EditValue = _rowParent["ENDCALIBRATIONDATE"].ToSafeString();

                dteNextCalibrationDate.EditValue = _rowParent["NEXTCALIBRATIONDATE"].ToSafeString();

                cboCalibrationResult.EditValue = _rowParent["CALIBRATIONRESULT"].ToSafeString();

                cboIsMiddle.EditValue = _rowParent["ISMIDDLE"].ToSafeString();

                cboIsMiddleDateMode.EditValue = _rowParent["MIDDLECHECKCYCLETYPE"].ToSafeString();//2020-04-28 이승우 추가

                spinMiddleCheckCycle.Text = _rowParent["MIDDLECHECKCYCLE"].ToSafeString();

                dteMiddleCheckDate.EditValue = _rowParent["MIDDLECHECKDATE"].ToSafeString();

                dteNextMiddleCheckDate.EditValue = _rowParent["NEXTMIDDLECHECKDATE"].ToSafeString();

                cboMiddleCheckResult.EditValue = _rowParent["NEXTMIDDLECHECKRESULT"].ToSafeString();
                cboisRNRTarget.EditValue = _rowParent["ISRNRTARGET"].ToSafeString();
                spinRNRCycle.Text = _rowParent["RNRCYCLE"].ToSafeString();
                cboisRNR.EditValue = _rowParent["ISRNR"].ToSafeString();

                dteRNRProgressDate.EditValue = _rowParent["RNRPROGRESSDATE"].ToSafeString();

                dteRNRNextProgressDate.EditValue = _rowParent["RNRNEXTPROGRESSDATE"].ToSafeString();

                cboRNRResult.EditValue = _rowParent["RNRRESULT"].ToSafeString();

                //2020-01-29 강유라 설비이미지 컨트롤 변경 으로 주석 처리
                //2020-02-17 강유라 설비이미지 한개 -> 여러장으로 변경
                //string filePath = Format.GetString(_rowParent["FILEPATH"]);
                //string safeFileName = Format.GetString(_rowParent["SAFEFILENAME"]);
                //btnDetailDocFile.EditValue = safeFileName;
                /*
                if (!string.IsNullOrWhiteSpace(filePath) && !string.IsNullOrWhiteSpace(safeFileName))
                {
                    picPhoto.Image = CommonFunction.GetFtpImageFileToBitmap(filePath, safeFileName);
                }
                */

                cboisMeasuringInstrument.EditValue = _rowParent["ISMEASURINGINSTRUMENT"].ToSafeString();
                txtComments.Text = _rowParent["COMMENTS"].ToSafeString();

                txtControlName.Text = _rowParent["CONTROLNAME"].ToSafeString();
                memoSpec.Text = _rowParent["TYPESPEC"].ToSafeString();
            }
            
            SetReportNCardFileInfo();

            //site
            txtPlantId.EditValue = _rowParent["PLANTID"].ToSafeString();
            //장비Type
            cboEquipmentType.EditValue = _rowParent["EQUIPMENTTYPE"].ToSafeString();

            //2020-02-18 강유라 파일 컨트롤 데이터 바인딩
            string fileResourceId = Format.GetString(_rowParent["SERIALNO"]) + Format.GetString(_rowParent["CONTROLNO"]);
            //1.계측기 사진
            SearchFile(fileResourceId, "MeasurementImage",fpcEquipmentImage, true, true);
            //2.검교정 파일
            SearchFile(fileResourceId, "CalibrationFile", fpcCalibration);
            //2.중간 점검 파일
            SearchFile(fileResourceId, "MiddleFile", fpcMiddle);
            //2.R&R 파일
            SearchFile(fileResourceId, "RNRFile", fpcRNR);

            fpcCalibration.SetGridColumnsSort("FILENAME", DevExpress.Data.ColumnSortOrder.Descending);
        }


        /// <summary>
        /// 계측기 이력카드 DataTable 생성.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable CreateDataTable(string tableName = "measuringinstCard")
        {
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            //Filed
            dt.Columns.Add("SERIALNO", typeof(string));
            dt.Columns.Add("CONTROLNO", typeof(string));
            dt.Columns.Add("EQUIPMENTTYPE", typeof(string));
            dt.Columns.Add("CONTROLNAME", typeof(string));
            dt.Columns.Add("TYPESPEC", typeof(string));
            //dt.Columns.Add("EQUIPMENTID", typeof(string));
            dt.Columns.Add("MEASURINGID", typeof(string));
            dt.Columns.Add("MEASURINGPRODUCTID", typeof(string));
            dt.Columns.Add("ENTERPRISEID", typeof(string));
            dt.Columns.Add("AREAID", typeof(string));
            dt.Columns.Add("PLANTID", typeof(string));           
            dt.Columns.Add("UNIT", typeof(string));
            //2020-01-29 강유라 상한/하한 => 규격에서 관리로 주석처리
            //dt.Columns.Add("LSL", typeof(double));
            //dt.Columns.Add("USL", typeof(double));
            dt.Columns.Add("SPECNUIT", typeof(string));
            dt.Columns.Add("MANUFACTURECOUNTRY", typeof(string));
            dt.Columns.Add("MANUFACTURER", typeof(string));
            dt.Columns.Add("PROCESSSEGMENTID", typeof(string));
            dt.Columns.Add("PROCESSSEGMENTVERSION", typeof(string));
            dt.Columns.Add("DEPARTMENT", typeof(string));
            dt.Columns.Add("PURCHASEDATE", typeof(DateTime));
            dt.Columns.Add("PURCHASEAMOUNT", typeof(int));
            dt.Columns.Add("ISPC", typeof(string));
            dt.Columns.Add("OSVERSION", typeof(string));
            dt.Columns.Add("SORTWAREVERSION", typeof(string));
            dt.Columns.Add("SCRAPDATE", typeof(DateTime));
            dt.Columns.Add("ISSCRAP", typeof(string));
            dt.Columns.Add("REPAIRDISPOSALID", typeof(string));
            dt.Columns.Add("INSPECTIONCALIBRATIONCYCLE", typeof(int));
            dt.Columns.Add("ISINSPECTIONCALIBRATION", typeof(string));
            dt.Columns.Add("ENDCALIBRATIONDATE", typeof(DateTime));
            dt.Columns.Add("NEXTCALIBRATIONDATE", typeof(DateTime));
            dt.Columns.Add("CALIBRATIONRESULT", typeof(string));
            dt.Columns.Add("ISMIDDLE", typeof(string));
            dt.Columns.Add("MIDDLECHECKCYCLETYPE", typeof(string));
            dt.Columns.Add("MIDDLECHECKCYCLE", typeof(int));
            dt.Columns.Add("MIDDLECHECKDATE", typeof(DateTime));
            dt.Columns.Add("NEXTMIDDLECHECKDATE", typeof(DateTime));
            dt.Columns.Add("NEXTMIDDLECHECKRESULT", typeof(string));
            dt.Columns.Add("ISRNRTARGET", typeof(string));
            dt.Columns.Add("RNRCYCLE", typeof(int));
            dt.Columns.Add("ISRNR", typeof(string));
            dt.Columns.Add("RNRPROGRESSDATE", typeof(DateTime));
            dt.Columns.Add("RNRNEXTPROGRESSDATE", typeof(DateTime));
            dt.Columns.Add("RNRRESULT", typeof(string));
            dt.Columns.Add("RNRREPORTTYPE", typeof(string));
            dt.Columns.Add("RNRREPORTFILENAME", typeof(string));
            dt.Columns.Add("CORRECTION", typeof(double));
            dt.Columns.Add("PERMITVALUE", typeof(double));
            dt.Columns.Add("ANALYTICALOCCASION", typeof(string));
            dt.Columns.Add("INSPECTIONCALIBRATIONCOA", typeof(string));
            dt.Columns.Add("EQUIPMENTIMAGE", typeof(string));
            dt.Columns.Add("PHOTO", typeof(string));
            //2020-01-30 강유라 설비사진
            dt.Columns.Add("CHANGEFLAG", typeof(string));
            dt.Columns.Add("FILENAME", typeof(string));
            dt.Columns.Add("FILESIZE", typeof(int));
            dt.Columns.Add("FILEEXT", typeof(string));
            dt.Columns.Add("FILEPATH", typeof(string));
            dt.Columns.Add("ISMEASURINGINSTRUMENT", typeof(string));
            dt.Columns.Add("COMMENTS", typeof(string));   
            dt.Columns.Add("VALIDSTATE", typeof(string));

            return dt;
        }
        #region 이미지 관련 이벤트 => 하나 등록에서 여러개 등록으로 이벤트 수정
        /*
        /// <summary>
        /// 2020-01-29 강유라
        /// 설비 이미지 등록 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnImageFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch ((sender as DevExpress.XtraEditors.ButtonEdit).Properties.Buttons.IndexOf(e.Button))
            {
                case 0:
                    OpenFileDialog dialog = new OpenFileDialog
                    {
                        Multiselect = false,
                        Filter = "Image Files (*.bmp, *.jpg, *.jpeg, *.png)|*.BMP;*.JPG;*.JPEG;*.PNG",
                        FilterIndex = 0
                    };

                    if (dialog.ShowDialog().Equals(DialogResult.OK))
                    {
                        btnDetailDoc.Text = new FileInfo(dialog.FileName).FullName;

                        _rowParent["FILEID"] = "";//서버에서 설정
                        _rowParent["FILENAME"] = Path.GetFileNameWithoutExtension(dialog.SafeFileName);
                        _rowParent["FILESIZE"] = new FileInfo(dialog.FileName).Length;
                        _rowParent["FILEEXT"] = Path.GetExtension(dialog.FileName).Substring(1);
                        _rowParent["FILEPATH"] = "MeasurementCard/Image";
                        _rowParent["CHANGEFLAG"] = "added";
                        _rowParent["LOCALFILEPATH"] = new FileInfo(dialog.FileName).FullName;
                        _rowParent["FILEFULLPATH"] = dialog.FileName; //파일의 전체경로 저장
                        _rowParent["PROCESSINGSTATUS"] = "Wait";

                        btnDetailDoc.EditValue = Path.GetFileNameWithoutExtension(dialog.SafeFileName);
                        picPhoto.Image = QcmImageHelper.GetImageFromFile(Format.GetString(dialog.FileName));
                    };
                    break;

                case 1:
                    if (string.IsNullOrWhiteSpace(Format.GetString(_rowParent["CHANGEFLAG"])))
                    {
                        return;
                    }

                    FolderBrowserDialog sdialog = new FolderBrowserDialog();

                    // DB 조회후 다운로드
                    if (Format.GetString(_rowParent["LOCALFILEPATH"]).Equals(string.Empty))
                    {
                        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                        folderBrowserDialog.SelectedPath = Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

                        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {

                                string ftpServerPath = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Url"));
                                string ftpServerUserId = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Id"));
                                string ftpServerPassword = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Password"));

                                try
                                {
                                    string filePath = Format.GetString(_rowParent["FILEPATH"]);
                                    string safeFileName = Format.GetString(_rowParent["SAFEFILENAME"]);
                                    string downloadPath = folderBrowserDialog.SelectedPath;

                                    using (System.Net.WebClient client = new System.Net.WebClient())
                                    {
                                        string serverPath = ftpServerPath + Format.GetString(filePath);
                                        serverPath = serverPath + ((serverPath.EndsWith("/")) ? "" : "/") + Format.GetString(safeFileName);

                                        client.Credentials = new System.Net.NetworkCredential(ftpServerUserId, ftpServerPassword);

                                        client.DownloadFile(serverPath, string.Join("\\", downloadPath, Format.GetString(safeFileName)));

                                    }
                                }
                                catch (Exception e1)
                                {
                                    throw MessageException.Create(e1.Message);
                                }


                            }
                            catch (Exception ex)
                            {
                                throw MessageException.Create(ex.Message);
                            }

                            ShowMessage("SuccedSave");
                        }
                    }

                    break;
                case 2:

                    //새로 추가된 이미지가 아니라면 삭제할수 없음(추가, 수정가능)
                    if (!Format.GetString(_rowParent["CHANGEFLAG"]).Equals("added")) return;

                    btnDetailDoc.Text = string.Empty;

                    _rowParent["FILEID"] = string.Empty;
                    _rowParent["FILENAME"] = string.Empty;
                    _rowParent["FILESIZE"] = DBNull.Value;
                    _rowParent["FILEEXT"] = string.Empty;
                    _rowParent["FILEPATH"] = string.Empty;
                    _rowParent["SAFEFILENAME"] = string.Empty;
                    _rowParent["CHANGEFLAG"] = string.Empty;
                    _rowParent["LOCALFILEPATH"] = string.Empty;
                    _rowParent["FILEFULLPATH"] = string.Empty;
                    _rowParent["PROCESSINGSTATUS"] = string.Empty;

                    btnDetailDoc.EditValue = null;
                    picPhoto.Image = null;

                    break;
            }
        }
        */
        #endregion

        #region
        /// <summary>
        /// 2020-02-17 강유라
        /// 경위서 파일 등록 이벤트
        /// sf_objectfile
        /// fileId = FILE-MeasureMentDocumentFileUnique
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDocumentFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            switch ((sender as DevExpress.XtraEditors.ButtonEdit).Properties.Buttons.IndexOf(e.Button))
            {
                case 0:
                    OpenFileDialog dialog = new OpenFileDialog
                    {
                        Multiselect = false,
                        Filter = "Excel Files(*.xlsx)|*.xlsx;*.xls",
                        FilterIndex = 0
                    };
                    
                    if (dialog.ShowDialog().Equals(DialogResult.OK))
                    {
                        btnDetailDocFile.Text = new FileInfo(dialog.FileName).FullName;

                        _documentFileDt.Rows.Clear();
                        DataRow newRow = _documentFileDt.NewRow();

                        _rowParent["DOCUMENTCHANGEFLAG"] = "added";
                        newRow["FILEID"] = "";
                        newRow["FILENAME"] = Path.GetFileNameWithoutExtension(dialog.SafeFileName);
                        newRow["FILEEXT"] = Path.GetExtension(dialog.FileName).Substring(1);
                        newRow["FILEPATH"] = "MeasurementCard/DocumentFile";
                        newRow["SAFEFILENAME"] = Path.GetFileNameWithoutExtension(dialog.SafeFileName);
                        newRow["FILESIZE"] = new FileInfo(dialog.FileName).Length;
                        newRow["LOCALFILEPATH"] = dialog.FileName; //파일의 전체경로 저장
                        newRow["PROCESSINGSTATUS"] = "Wait";
                        newRow["SEQUENCE"] = 1;

                        _documentFileDt.Rows.Add(newRow);
                        btnDetailDocFile.EditValue = Path.GetFileNameWithoutExtension(dialog.SafeFileName);

                    };
                    break;

                case 1:
                    if (string.IsNullOrWhiteSpace(Format.GetString(_rowParent["DOCUMENTCHANGEFLAG"])))
                    {
                        return;
                    }

                    FolderBrowserDialog sdialog = new FolderBrowserDialog();
                    
                    // DB 조회후 다운로드
                    if (!string.IsNullOrWhiteSpace(documentFilePath) && !string.IsNullOrWhiteSpace(documentSafeFileName))
                    {
                        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                        folderBrowserDialog.SelectedPath = Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

                        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {

                                string ftpServerPath = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Url"));
                                string ftpServerUserId = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Id"));
                                string ftpServerPassword = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Password"));

                                try
                                {
                                    string filePath = documentFilePath;
                                    string safeFileName = documentSafeFileName;
                                    string downloadPath = folderBrowserDialog.SelectedPath;

                                    using (System.Net.WebClient client = new System.Net.WebClient())
                                    {
                                        string serverPath = ftpServerPath + Format.GetString(filePath);
                                        serverPath = serverPath + ((serverPath.EndsWith("/")) ? "" : "/") + Format.GetString(safeFileName);

                                        client.Credentials = new System.Net.NetworkCredential(ftpServerUserId, ftpServerPassword);

                                        client.DownloadFile(serverPath, string.Join("\\", downloadPath, Format.GetString(safeFileName)));

                                    }
                                }
                                catch (Exception e1)
                                {
                                    throw MessageException.Create(e1.Message);
                                }


                            }
                            catch (Exception ex)
                            {
                                throw MessageException.Create(ex.Message);
                            }

                            ShowMessage("SuccedSave");
                        }
                    }

                    break;
            }
        }
        #endregion

        #region
        /// <summary>
        /// 2020-02-06 강유라
        /// 보고서 파일 등록 이벤트
        /// sf_objectfile
        /// fileId = FILE-MeasureMentReportFileUnique
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReportFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
           
            switch ((sender as DevExpress.XtraEditors.ButtonEdit).Properties.Buttons.IndexOf(e.Button))
            {
                case 0:
                    OpenFileDialog dialog = new OpenFileDialog
                    {
                        Multiselect = false,
                        Filter  = "Excel Files(*.xlsx)|*.xlsx;*.xls",
                        FilterIndex = 0
                    };

                    if (dialog.ShowDialog().Equals(DialogResult.OK))
                    {
                        btnReportFile.Text = new FileInfo(dialog.FileName).FullName;

                        _reportFileDt.Rows.Clear();
                        DataRow newRow =  _reportFileDt.NewRow();

                        _rowParent["REPORTCHANGEFLAG"] = "added";
                        newRow["FILEID"] = "";
                        newRow["FILENAME"] = Path.GetFileNameWithoutExtension(dialog.SafeFileName);
                        newRow["FILEEXT"] = Path.GetExtension(dialog.FileName).Substring(1);
                        newRow["FILEPATH"] = "MeasurementCard/ReportFile";
                        newRow["SAFEFILENAME"] = Path.GetFileNameWithoutExtension(dialog.SafeFileName);
                        newRow["FILESIZE"] = new FileInfo(dialog.FileName).Length;
                        newRow["LOCALFILEPATH"] = dialog.FileName; //파일의 전체경로 저장
                        newRow["PROCESSINGSTATUS"] = "Wait";
                        newRow["SEQUENCE"] = 1;

                        _reportFileDt.Rows.Add(newRow);
                        btnReportFile.EditValue = Path.GetFileNameWithoutExtension(dialog.SafeFileName);
                        
                    };
                    break;

                case 1:
                    if (string.IsNullOrWhiteSpace(Format.GetString(_rowParent["REPORTCHANGEFLAG"])))
                    {
                        return;
                    }

                    FolderBrowserDialog sdialog = new FolderBrowserDialog();

                    // DB 조회후 다운로드
                    if (!string.IsNullOrWhiteSpace(reportFilePath) && !string.IsNullOrWhiteSpace(reportSafeFileName))
                    {
                        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                        folderBrowserDialog.SelectedPath = Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

                        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {

                                string ftpServerPath = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Url"));
                                string ftpServerUserId = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Id"));
                                string ftpServerPassword = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Password"));

                                try
                                {
                                    string filePath = reportFilePath;
                                    string safeFileName = reportSafeFileName;
                                    string downloadPath = folderBrowserDialog.SelectedPath;

                                    using (System.Net.WebClient client = new System.Net.WebClient())
                                    {
                                        string serverPath = ftpServerPath + Format.GetString(filePath);
                                        serverPath = serverPath + ((serverPath.EndsWith("/")) ? "" : "/") + Format.GetString(safeFileName);

                                        client.Credentials = new System.Net.NetworkCredential(ftpServerUserId, ftpServerPassword);

                                        client.DownloadFile(serverPath, string.Join("\\", downloadPath, Format.GetString(safeFileName)));

                                    }
                                }
                                catch (Exception e1)
                                {
                                    throw MessageException.Create(e1.Message);
                                }


                            }
                            catch (Exception ex)
                            {
                                throw MessageException.Create(ex.Message);
                            }

                            ShowMessage("SuccedSave");
                        }
                    }

                    break;               
            }
        }

        /// <summary>
        /// 2020-02-06 강유라
        /// 이력카드 파일 등록 이벤트
        /// sf_objectfile
        /// fileId = FILE-MeasureMentCardFileUnique
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCardFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch ((sender as DevExpress.XtraEditors.ButtonEdit).Properties.Buttons.IndexOf(e.Button))
            {
                case 0:
                    OpenFileDialog dialog = new OpenFileDialog
                    {
                        Multiselect = false,
                        Filter = "Excel Files(*.xlsx)|*.xlsx;*.xls",
                        FilterIndex = 0
                    };

                    if (dialog.ShowDialog().Equals(DialogResult.OK))
                    {
                        btnCardFile.Text = new FileInfo(dialog.FileName).FullName;

                        _cardFileDt.Rows.Clear();
                        DataRow newRow = _cardFileDt.NewRow();

                        _rowParent["CARDCHANGEFLAG"] = "added";
                        newRow["FILEID"] = "";
                        newRow["FILENAME"] = Path.GetFileNameWithoutExtension(dialog.SafeFileName);
                        newRow["FILEEXT"] = Path.GetExtension(dialog.FileName).Substring(1);
                        newRow["FILEPATH"] = "MeasurementCard/CardFile";
                        newRow["SAFEFILENAME"] = Path.GetFileNameWithoutExtension(dialog.SafeFileName);
                        newRow["FILESIZE"] = new FileInfo(dialog.FileName).Length;
                        newRow["LOCALFILEPATH"] = dialog.FileName; //파일의 전체경로 저장
                        newRow["PROCESSINGSTATUS"] = "Wait";
                        newRow["SEQUENCE"] = 1;

                        _cardFileDt.Rows.Add(newRow);
                        btnCardFile.EditValue = Path.GetFileNameWithoutExtension(dialog.SafeFileName);

                    };
                    break;

                case 1:
                    if (string.IsNullOrWhiteSpace(Format.GetString(_rowParent["CARDCHANGEFLAG"])))
                    {
                        return;
                    }

                    FolderBrowserDialog sdialog = new FolderBrowserDialog();

                    // DB 조회후 다운로드
                    if (!string.IsNullOrWhiteSpace(cardFilePath) && !string.IsNullOrWhiteSpace(cardSafeFileName))
                    {
                        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                        folderBrowserDialog.SelectedPath = Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

                        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {

                                string ftpServerPath = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Url"));
                                string ftpServerUserId = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Id"));
                                string ftpServerPassword = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Password"));

                                try
                                {
                                    string filePath = cardFilePath;
                                    string safeFileName = cardSafeFileName;
                                    string downloadPath = folderBrowserDialog.SelectedPath;

                                    using (System.Net.WebClient client = new System.Net.WebClient())
                                    {
                                        string serverPath = ftpServerPath + Format.GetString(filePath);
                                        serverPath = serverPath + ((serverPath.EndsWith("/")) ? "" : "/") + Format.GetString(safeFileName);

                                        client.Credentials = new System.Net.NetworkCredential(ftpServerUserId, ftpServerPassword);

                                        client.DownloadFile(serverPath, string.Join("\\", downloadPath, Format.GetString(safeFileName)));

                                    }
                                }
                                catch (Exception e1)
                                {
                                    throw MessageException.Create(e1.Message);
                                }


                            }
                            catch (Exception ex)
                            {
                                throw MessageException.Create(ex.Message);
                            }

                            ShowMessage("SuccedSave");
                        }
                    }

                    break;
            }
        }
        #endregion

        #endregion

        #region Private Function

        /// <summary>
        /// 콤보박스에서 장비구분 값 설정.
        /// </summary>
        private void SetComboBoxCreateValueEquipmentType()
        {
            DataTable dataTableEquipmentType;

            SetComboBoxInitial(cboEquipmentType);

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dicParam.Add("CODECLASSID", "MeasurementType");

            dataTableEquipmentType = SqlExecuter.Query("GetCodeList", "00001", dicParam);
            if (dataTableEquipmentType != null)
            {
                this.cboEquipmentType.DataSource = dataTableEquipmentType;
            }
        }

        /// <summary>
        /// 콤보박스에서 공장 값 설정.
        /// </summary>
        private void SetComboBoxCreateValuePlantId()
        {/*
            DataTable dataTablePlantId;

            cboPlantId.ValueMember = "PLANTID";
            cboPlantId.DisplayMember = "PLANTNAME";
            cboPlantId.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboPlantId.ShowHeader = false;

            //cboPlantId.SetDefault(UserInfo.Current.Plant);
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dicParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);

            dataTablePlantId = SqlExecuter.Query("GetPlantListByQcm", "10001", dicParam);

            if (dataTablePlantId != null)
            {
                this.cboPlantId.DataSource = dataTablePlantId;
                //this.cboPlantId.Text = UserInfo.Current.Plant.ToString();
            }*/

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

        private void SetComboBoxYNInitial(SmartComboBox cboBox)
        {
            cboBox.EditValue = "Y";    
        }

        private void SetComboBoxPassFailInitial(SmartComboBox cboBox)
        {
            //cboBox.EditValue = "None";
            cboBox.UseEmptyItem = true;
        }

        /// <summary>
        /// 콤보박스에서 YesNo 값 설정.
        /// </summary>
        private void SetComboBoxCreateValueYesNo()
        {
            DataTable dataTableYesNo;

            SetComboBoxInitial(cboisPC);
            SetComboBoxInitial(cboisScrap);
            SetComboBoxInitial(cboisInspectionCalibration);
            SetComboBoxInitial(cboisRNRTarget);
            SetComboBoxInitial(cboisRNR);
            SetComboBoxInitial(cboisMeasuringInstrument);
            SetComboBoxInitial(cboIsMiddle);


            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dicParam.Add("CODECLASSID", "YesNo");

            dataTableYesNo = SqlExecuter.Query("GetCodeList", "00001", dicParam);
            if (dataTableYesNo != null)
            {
                cboisPC.DataSource = dataTableYesNo;
                cboisScrap.DataSource = dataTableYesNo;
                cboisInspectionCalibration.DataSource = dataTableYesNo;
                cboisRNRTarget.DataSource = dataTableYesNo;
                cboisRNR.DataSource = dataTableYesNo;
                cboisMeasuringInstrument.DataSource = dataTableYesNo;
                cboIsMiddle.DataSource = dataTableYesNo;
            }

            SetComboBoxYNInitial(cboisInspectionCalibration);
            SetComboBoxYNInitial(cboIsMiddle);
            SetComboBoxYNInitial(cboisRNR);
            SetComboBoxYNInitial(cboisRNRTarget);

        }

        /// <summary>
        /// 콤보박스에서 합격/불합격 값 설정.
        /// </summary>
        private void SetComboBoxCreateValueResult()
        {
            DataTable dataTableOkNg;

            SetComboBoxInitial(cboCalibrationResult);
            SetComboBoxInitial(cboMiddleCheckResult);
            SetComboBoxInitial(cboRNRResult);

            //초기값 none => empty
            SetComboBoxPassFailInitial(cboCalibrationResult);
            SetComboBoxPassFailInitial(cboMiddleCheckResult);
            SetComboBoxPassFailInitial(cboRNRResult);

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dicParam.Add("CODECLASSID", "PassFailType");

            dataTableOkNg = SqlExecuter.Query("GetCodeList", "00001", dicParam);
            if (dataTableOkNg != null)
            {
                cboCalibrationResult.DataSource = dataTableOkNg;
                cboMiddleCheckResult.DataSource = dataTableOkNg;
                cboRNRResult.DataSource = dataTableOkNg;
            }

        }
        
        /// <summary>
        /// 콤보박스에 유효상태 값 설정.
        /// </summary>
        private void SetComboCreateValidstate()
        {
            DataTable dataTableValid;

            SetComboBoxInitial(cboValid);

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dicParam.Add("CODECLASSID", "ValidState");

            dataTableValid = SqlExecuter.Query("GetCodeList", "00001", dicParam);
            if (dataTableValid != null)
            {
                cboValid.DataSource = dataTableValid;
                cboValid.ItemIndex = 0;
            }
        }


        /// <summary>
        /// 콤보박스에 중간점검 일자 유형 값 설정.
        /// 2020-04-28 이승우
        /// </summary>
        private void SetComboCreateIsMiddleDateMode()
        {
            DataTable dataTableValid;

            SetComboBoxInitial(cboIsMiddleDateMode);

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dicParam.Add("CODECLASSID", "IsMiddleDateMode");
            dataTableValid = SqlExecuter.Query("GetCodeList", "00001", dicParam);
            if (dataTableValid != null)
            {
                cboIsMiddleDateMode.DataSource = dataTableValid;
                cboIsMiddleDateMode.ItemIndex = 0;
            }
        }

        /// <summary>
        /// 2020-01-29 강유라 - 계측기 관리번호 코드 콤보박스 초기화       
        /// </summary>
        private void SetComboBoxCreateValueMeasureCode()
        {
            DataTable dataTableCode;

            SetComboBoxInitial(cboControlNoPrefix); 

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dicParam.Add("CODECLASSID", "MeasurementCodeType");
            //cboControlNoPrefix.UseEmptyItem = true;

            dataTableCode = SqlExecuter.Query("GetCodeList", "00001", dicParam);
            if (dataTableCode.Rows.Count > 0)
            {
                cboControlNoPrefix.DataSource = dataTableCode;
                cboControlNoPrefix.ItemIndex = 0;
            }

        }

        /// <summary>
        /// 2020-01-29 강유라
        /// 이미지 추가 컨트롤 초기화
        /// </summary>
        private void InitializeContent()
        {
            if(!isReadOnlyControls)
            { 
                btnDetailDocFile.Properties.Buttons.Add(new EditorButton(ButtonPredefines.Down));
     
                btnReportFile.Properties.Buttons.Add(new EditorButton(ButtonPredefines.Down));

                btnCardFile.Properties.Buttons.Add(new EditorButton(ButtonPredefines.Down));
            }

            btnDetailDocFile.ReadOnly = true;
            btnReportFile.ReadOnly = true;
            btnCardFile.ReadOnly = true;
        }

        /// <summary>
        /// 2020-01-30 강유라
        /// PK 수정 불가 설정 함수
        /// </summary>
        private void SetPKReadOnlyAndEvent()
        {
            //true => insertData인 경우만 관리번호 코드 prefix이벤트 연결
            if (_isNewData)
            {
                btnCardReport.Enabled = false;

                txtControlNo.EditValueChanged += (s, e) => { _doCheck = false; };
                txtControlNo.KeyDown += TxtControlNo_KeyDown;
                cboControlNoPrefix.EditValueChanged += CboControlNoPrefix_EditValueChanged;

                /*2020-02-27 강유라 관리번호 자동채번 안되게 -> 수동입력
                 * 입력 후 엔터 -> 기존있는 데이터인지 확인
                //2020-01-29 강유라 계측기 관리번호 코드 바뀔때 관리번호 부여 이벤트
                cboControlNoPrefix.EditValueChanged += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(Format.GetString(cboControlNoPrefix.EditValue)))
                    {
                        txtControlNo.EditValue = string.Empty;
                        txtControlNo.Text = string.Empty;
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(Format.GetString(txtSerialNo.EditValue)))
                    {
                        cboControlNoPrefix.ItemIndex = 0;
                        throw MessageException.Create("NoSerialMeasurement");//시리얼번호를 입력 하세요.
                    }

                    Dictionary<string, object> param = new Dictionary<string, object>()
                    {
                        {"CONTROLNOPREFIX",cboControlNoPrefix.EditValue},
                        { "SERIALNO", txtSerialNo.EditValue}
                    };

                    DataTable dt = SqlExecuter.Query("GetNextMeasurementControlNoSerial", "10001", param);

                    string controlNoSerial = "";

                    if (dt.Rows.Count == 0)
                    {//시리얼 번호에 , 관리번호 앞코드(M,C,E)로 시작되는 관리번호 없을때
                        controlNoSerial = "0001";
                    }
                    else
                    {
                        controlNoSerial = Format.GetString(dt.Rows[0]["CONTROLSERIAL"]);
                    }

                    txtControlNo.EditValue = Format.GetString(cboControlNoPrefix.EditValue + controlNoSerial);
                    txtControlNo.Text = Format.GetString(cboControlNoPrefix.EditValue + controlNoSerial);

                    txtControlName.EditValue = Format.GetString(cboControlNoPrefix.EditValue + controlNoSerial);
                    txtControlName.Text = Format.GetString(cboControlNoPrefix.EditValue + controlNoSerial);             

                };*/
            }
            else
            {//false => updateData인 경우 시리얼, 관리번호 수정 불가 
                cboControlNoPrefix.Enabled = false;
                txtControlNo.Enabled = false;
                txtSerialNo.Enabled = false;

                btnCardReport.Click += btnCardReport_Click;
            }
        }

        /// <summary>
        /// 관리번호 코드 변경시 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboControlNoPrefix_EditValueChanged(object sender, EventArgs e)
        {
            _doCheck = false;
            txtControlName.EditValue = Format.GetString(cboControlNoPrefix.EditValue + txtControlNo.Text);
            txtControlName.Text = Format.GetString(cboControlNoPrefix.EditValue + txtControlNo.Text);
        }

        /// <summary>
        /// 컨트롤 번호 엔터 쳤을때 이미 존재하는지 확인
        /// 중복확인 했는지 여부
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtControlNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(Format.GetString(txtSerialNo.EditValue)))
                {
                    throw MessageException.Create("NoSerialMeasurement");//시리얼번호를 입력 하세요.
                }

                if (string.IsNullOrWhiteSpace(Format.GetString(txtControlNo.Text)))
                {
                    throw MessageException.Create("NoControlNoMeasurement");//관리번호를 입력 하세요.
                }

                Dictionary<string, object> param = new Dictionary<string, object>()
                    {
                        {"CONTROLNO", Format.GetString(cboControlNoPrefix.EditValue) + Format.GetString(txtControlNo.EditValue)},
                        { "SERIALNO", txtSerialNo.EditValue}
                    };

                DataTable dt = SqlExecuter.Query("GetMeasurementControlNoExist", "10001", param);

                if (dt.Rows.Count == 0)
                {//입력한 관리번호가 존재 하지 않을 때
                    _doCheck = true;

                    txtControlName.EditValue = Format.GetString(cboControlNoPrefix.EditValue + txtControlNo.Text);
                    txtControlName.Text = Format.GetString(cboControlNoPrefix.EditValue + txtControlNo.Text);

                    ShowMessage("CanRegisterMeasurement");//등록가능 한 시리얼 번호와 관리 번호 입니다.
                }
                else
                {//입력한 관리 번호이미 존재
                    _doCheck = false;
                    ShowMessage("InValidData002", string.Concat(
                                                     Language.Get("MEASURINGSERIALNO")," : ",
                                                     Format.GetString(txtSerialNo.EditValue),
                                                     Language.Get("MEASURINGCONTROLNO"), " : ",
                                                     Format.GetString(cboControlNoPrefix.EditValue) + Format.GetString(txtControlNo.EditValue)));
                }

        
            }
        }

        /// <summary>
        /// 2020-01-30 강유라
        /// spinEdit 마이너스 값 입력 불가
        /// </summary>
        private void SetSpingEditFormat()
        {
            
            spinInspectionCalibrationCycle.Properties.MinValue = 1;
            spinMiddleCheckCycle.Properties.MinValue = 1;
            spinRNRCycle.Properties.MinValue = 1;

            spinInspectionCalibrationCycle.Properties.MaxValue = 60;
            spinMiddleCheckCycle.Properties.MaxValue = 60;
            spinRNRCycle.Properties.MaxValue = 60;
          
        }

        /// <summary>
        /// 보고서, 이력카드 데이터 넣을 dt 생성
        /// </summary>
        private void CreateFileDataTable()
        {
            _reportFileDt = QcmImageHelper.GetImageFileTable();
            _cardFileDt = QcmImageHelper.GetImageFileTable();
            _documentFileDt = QcmImageHelper.GetImageFileTable();
        }

        /// <summary>
        /// 수리폐기 이력 PK별 결제 정보 조회 
        /// </summary>
        private void GetMeasuringRepairScrapGridData()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Format.GetString(_rowParent["SERIALNO"])) || string.IsNullOrWhiteSpace(Format.GetString(_rowParent["CONTROLNO"])))
                    return;
                
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("P_SERIALNO", _rowParent["SERIALNO"].ToSafeString());
                param.Add("P_CONTROLNO", _rowParent["CONTROLNO"].ToSafeString());
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                DataTable ScrapTable = SqlExecuter.Query("SelectRawMeasuringRepairScrapListDirect", "10001", param);
              
                this.grdRepairScrap.DataSource = ScrapTable;
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        /// <summary>
        /// 파일 컨트롤에 바인딩 할 파일 데이터 조회 함수
        /// </summary>
        /// <param name="resourceId"></param>
        /// <param name="resourceType"></param>
        private void SearchFile(string resourceId, string resourceType, SmartFileProcessingControl control, bool showImage = false, bool setFirstImg = false)
        {
            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                {"P_FILERESOURCEID", resourceId},
                {"P_FILERESOURCETYPE", resourceType},
                {"P_FILERESOURCEVERSION", "0"}
            };

            DataTable fileDt = SqlExecuter.Query("SelectFileInspResult", "10001", values);

            control.DataSource = fileDt;

            
            if (setFirstImg)
            {
                if (fileDt.Rows.Count == 0) return;

                DataRow fileRow = fileDt.Rows[0];
                string filePath = Format.GetString(fileRow["FILEPATH"]);
                string safeFileName = Format.GetString(fileRow["SAFEFILENAME"]);

                if (!string.IsNullOrWhiteSpace(filePath) && !string.IsNullOrWhiteSpace(safeFileName))
                {
                    cardImage = CommonFunction.GetFtpImageFileToBitmap(filePath, safeFileName);
                }
            }

         }

        /// <summary>
        ///계측기 이미지를 바인딩 하는 함수
        /// </summary>
        private void BindingImage(DataRow fileRow)
        {
            if (fileRow == null) return;

            string state = Format.GetString(fileRow.RowState);

            if (state.Equals("Added"))
            {
                picPhoto.Image = QcmImageHelper.GetImageFromFile(fileRow["LOCALFILEPATH"].ToString());
            }
            else
            { 
                string filePath = Format.GetString(fileRow["FILEPATH"]);
                string safeFileName = Format.GetString(fileRow["SAFEFILENAME"]);

                if (!string.IsNullOrWhiteSpace(filePath) && !string.IsNullOrWhiteSpace(safeFileName))
                {
                    picPhoto.Image = CommonFunction.GetFtpImageFileToBitmap(filePath, safeFileName);
                }
            }
        }

        /// <summary>
        ///  날짜 add 하기전 값 조정
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string GetTempDateStrig(string date)
        {
            string returnDate = "";
            returnDate = date.Contains(":") ? date : date + " 12:00:00";
            return returnDate;
        }
         #endregion

         #region Public Function
         /// <summary>
         /// 저장 버튼 활성화 여부 설정.
         /// </summary>
         /// <param name="isValue"></param>
         public void SaveButtonVisible(bool isValue = true)
         {
             _isControlinitial = true;
             this.btnSave.Enabled = isValue;
             _isControlinitial = false;
         }
         /// <summary>
         /// Popup 실행 폼의 Grid Row Data 전이
         /// </summary>
         /// <param name="_rowParent"></param>
         public void SetRowParent(DataRow _rowParent)
         {
             this._rowParent = _rowParent;

         }
        /*
         /// <summary>
         /// 수리폐기 이력 상세 정보 조회.
         /// </summary>
         public DataRow GetMeasuringRepairScrapDetailData(DataRow row)
         {
             DataRow rowScrap = null;

             try
             {
                 string serialNo = "";
                 string controlNo = "";
                 string repairScrapID = "";

                 serialNo = row["SERIALNO"].ToSafeString();
                 controlNo = row["CONTROLNO"].ToSafeString();
                 repairScrapID = row["REPAIRSCRAPID"].ToSafeString();

                 if (serialNo == "" || controlNo == "" || repairScrapID == "")
                 {
                     return rowScrap;
                 }

                 Dictionary<string, object> param = new Dictionary<string, object>();
                 param.Add("P_SERIALNO", serialNo);
                 param.Add("P_CONTROLNO", controlNo);
                 param.Add("P_REPAIRSCRAPID", repairScrapID);
                 param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                SqlQuery ScrapList = new SqlQuery("SelectRawMeasuringRepairScrapListDirect", "10001", param);
                 DataTable ScrapTable = ScrapList.Execute();
                 if (ScrapTable != null && ScrapTable.Rows.Count > 0)
                 {
                     rowScrap = ScrapTable.Rows[0];
                 }
             }
             catch (Exception ex)
             {
                 Logger.Error(ex.Message);
             }

             return rowScrap;
         }
         */
         /// <summary>
         /// 신규 등록시 등록되어있는 파일 정보를 가져오는 함수
         /// 다운, 디스플레이용
         /// </summary>
         public void SetReportNCardFileInfo()
         {
             DataTable fileInfo = SqlExecuter.Query("GetMeasureReportNCardFileInfo", "10001");

             var hasFile = fileInfo.AsEnumerable()
                 .Select(r=>r["FILEID"])
                 .ToList();

             if (hasFile.Contains("FILE-MeasureMentReportFileUnique"))
             {//report파일 등록 된것 있는 경우
                 var reportInfo = fileInfo.Rows.Cast<DataRow>()
                             .Where(r => Format.GetString(r["FILEID"]).Equals("FILE-MeasureMentReportFileUnique"))
                             .Select(r => new { reportFilePath = r["FILEPATH"], reportSafeFileName = r["SAFEFILENAME"] })
                             .ToList();

                 reportFilePath = Format.GetString(reportInfo[0].reportFilePath);
                 reportSafeFileName = Format.GetString(reportInfo[0].reportSafeFileName);

                 btnReportFile.EditValue = reportSafeFileName;
             }

             if (hasFile.Contains("FILE-MeasureMentCardFileUnique"))
             {//이력카드파일 등록 된것 있는 경우
                 var cardInfo = fileInfo.Rows.Cast<DataRow>()
                      .Where(r => Format.GetString(r["FILEID"]).Equals("FILE-MeasureMentCardFileUnique"))
                      .Select(r => new { cardFilePath = r["FILEPATH"], cardSafeFileName = r["SAFEFILENAME"] })
                      .ToList();

                 cardFilePath = Format.GetString(cardInfo[0].cardFilePath);
                 cardSafeFileName = Format.GetString(cardInfo[0].cardSafeFileName);

                 btnCardFile.EditValue = cardSafeFileName;
             }

             if (hasFile.Contains("FILE-MeasureMentDocumentFileUnique"))
             {//이력카드파일 등록 된것 있는 경우
                 var documentInfo = fileInfo.Rows.Cast<DataRow>()
                      .Where(r => Format.GetString(r["FILEID"]).Equals("FILE-MeasureMentDocumentFileUnique"))
                      .Select(r => new { documentFilePath = r["FILEPATH"], documentSafeFileName = r["SAFEFILENAME"] })
                      .ToList();

                 documentFilePath = Format.GetString(documentInfo[0].documentFilePath);
                 documentSafeFileName = Format.GetString(documentInfo[0].documentSafeFileName);

                 btnDetailDocFile.EditValue = documentSafeFileName;
             }

         }


         #endregion

         #region Initialize Popup

         /// <summary>
         /// Popup 컨트롤 초기화
         /// </summary>
         private void InitializePopup()
         {
             //공정
             popupProcesssegmentId.SelectPopupCondition = ProcessSegmentIdPopup();

             //사용부서
             popupDepartment.SelectPopupCondition = DepartmentInfoPopup();

             //작업장(설치장소)
             popupAreaId.SelectPopupCondition = AreaIdPopup();

             //수리/ 폐기 이력
             GetMeasuringRepairScrapGridData();
         }

         #region - AddConditionProductPopup :: 검색조건에 품목 선택 팝업을 추가한다. |

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
             popup.SetPopupLayout("DEPARTMENTNAME", PopupButtonStyles.Ok_Cancel, true, true);
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

         /// <summary>
         /// 작업장
         /// </summary>
         /// <returns></returns>
         private ConditionItemSelectPopup AreaIdPopup()
         {
             ConditionItemSelectPopup popup = new ConditionItemSelectPopup();

             Dictionary<string, object> param = new Dictionary<string, object>();
             param.Add("AreaType", "Area");
             param.Add("PlantId", UserInfo.Current.Plant);
             param.Add("LanguageType", UserInfo.Current.LanguageType);
             param.Add("AREA", UserInfo.Current.Area);

             popup.SetPopupLayoutForm(600, 800, FormBorderStyle.FixedDialog);
             popup.SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, true);
             popup.Id = "AREA";
             popup.SearchQuery = new SqlQuery("GetAreaList", "10003", param);
             popup.IsMultiGrid = false;
             popup.DisplayFieldName = "AREANAME";
             popup.ValueFieldName = "AREAID";


             popup.Conditions.AddTextBox("AREA").SetLabel("TXTAREA");
             popup.GridColumns.AddTextBoxColumn("AREAID", 150);
             popup.GridColumns.AddTextBoxColumn("AREANAME", 200);

             return popup;
         }

         /// <summary>
         /// 공정
         /// </summary>
         /// <returns></returns>
         private ConditionItemSelectPopup ProcessSegmentIdPopup()
         {
             ConditionItemSelectPopup popup = new ConditionItemSelectPopup();

             Dictionary<string, object> param = new Dictionary<string, object>();

             param.Add("PLANTID", UserInfo.Current.Plant);
             param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
             param.Add("PROCESSSEGMENTCLASSTYPE", "MiddleProcessSegmentClass");
             popup.SetPopupLayoutForm(800, 800, FormBorderStyle.FixedDialog);
             popup.SetPopupLayout(Language.Get("SELECTPROCESSSEGMENTID"), PopupButtonStyles.Ok_Cancel, true, false);
             popup.Id = "AREA";
             popup.SearchQuery = new SqlQuery("GetProcessSegmentList", "10002", param);
             popup.IsMultiGrid = false;
             popup.DisplayFieldName = "PROCESSSEGMENTNAME";
             popup.ValueFieldName = "PROCESSSEGMENTID";

             popup.Conditions.AddComboBox("PROCESSSEGMENTCLASSID_MIDDLE", new SqlQuery("GetProcessSegmentClassByType", "10001", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                 .SetLabel("MIDDLEPROCESSSEGMENT");
             popup.Conditions.AddTextBox("PROCESSSEGMENT");


             popup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_TOP", 150)
                 .SetLabel("LARGEPROCESSSEGMENT");
             popup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_MIDDLE", 150)
                 .SetLabel("MIDDLEPROCESSSEGMENT");
             popup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
             popup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

             return popup;
         }


         #endregion
         private void btnCardReport_Click(object sender, EventArgs e)
         {
  
             if (_rowParent == null)
             {
                 throw MessageException.Create("NoSaveData");
             }

             //등록된 파일 없을때 팝업 열지 않음
             if (string.IsNullOrWhiteSpace(Format.GetString(_rowParent["CARDFILEPATH"])) ||
                 string.IsNullOrWhiteSpace(Format.GetString(_rowParent["CARDSAFEFILENAME"])))
             {
                 throw MessageException.Create("NoFileMasuremnentCard");
             }

            
             if (_rowParent != null && _rowParent["SERIALNO"].ToString() != ""
                 && _rowParent["CONTROLNO"].ToString() != "")
             {
                DialogManager.ShowWaitArea(pnlMain);
                MeasuringCardInformationReportPopup frmPopup = new MeasuringCardInformationReportPopup();
                 //frmPopup.SetRowParent(row);
                 frmPopup.StartPosition = FormStartPosition.CenterParent;//폼 중앙 배치.
                 frmPopup.Owner = this;
                 frmPopup.CurrentDataRow = _rowParent;
                 frmPopup.currentImage = cardImage;
                 frmPopup.InitializeReport();
                 DialogManager.CloseWaitArea(pnlMain);
                 frmPopup.ShowDialog();

                 if (frmPopup.DialogResult == DialogResult.OK)
                 {
                     //Popup_FormClosed();
                     //this.OnSearchAsync();
                 }
             }
         }

        /// <summary>
        /// 검교정, 중간점검, RNR 파일 첨부 갯수에따라 결과 값 설정 함수
        /// </summary>
        /// 1.검교정
        private void SetCalibrationResult(int rowCount)
        {
            cboCalibrationResult.EditValue = rowCount > 0 ? "Pass" : "Fail";
        }

        //2.중간점검
        private void SetMiddleResult(int rowCount)
        {
            cboMiddleCheckResult.EditValue = rowCount > 0 ? "Pass" : "Fail";
        }

        //3.RNR
        private void SetRNRResult(int rowCount)
        {
            cboRNRResult.EditValue = rowCount > 0 ? "Pass" : "Fail";
        }

        private void VisibleProcessSegmentContorol()
        {
            if (UserInfo.Current.Enterprise.Equals("INTERFLEX"))
            {
                layPopupProcesssegment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layTxtProcesssegment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                layPopupProcesssegment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layTxtProcesssegment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }
        #region Property
        public bool isReadOnlyControls { get; set; }
         private void SetReadOnlyControls()
         {
             if (isReadOnlyControls)
             {
                txtMeasuringId.ReadOnly = true;
                txtMeasuringProductdefId.ReadOnly = true;
                cboEquipmentType.ReadOnly = true;
                txtSpecNuit.ReadOnly = true;
                txtManuFacturer.ReadOnly = true;
                txtManuFactureCountry.ReadOnly = true;
                txtPlantId.ReadOnly = true;     
                dtePurchaseDate.ReadOnly = true;
                numPurchaseAmount.ReadOnly = true;
                cboisPC.ReadOnly = true;
                txtOSVersion.ReadOnly = true;
                txtSortWareVersion.ReadOnly = true;
                cboisScrap.ReadOnly = true;
                dteScrapDate.ReadOnly = true;
                cboisMeasuringInstrument.ReadOnly = true;
                cboisInspectionCalibration.ReadOnly = true;
                cboCalibrationResult.ReadOnly = true;
                spinInspectionCalibrationCycle.ReadOnly = true;
                dteEndCalibrationDate.ReadOnly = true;
                dteMiddleCheckDate.ReadOnly = true;
                dteNextCalibrationDate.ReadOnly = true;
                dteNextMiddleCheckDate.ReadOnly = true;
                dteRNRProgressDate.ReadOnly = true;
                dteRNRNextProgressDate.ReadOnly = true;
                cboIsMiddle.ReadOnly = true;
                spinMiddleCheckCycle.ReadOnly = true;
                cboMiddleCheckResult.ReadOnly = true;
                cboisRNR.ReadOnly = true;
                cboisRNRTarget.ReadOnly = true;
                cboRNRResult.ReadOnly = true;
                cboisRNRTarget.ReadOnly = true;
                cboisRNRTarget.ReadOnly = true;
                spinRNRCycle.ReadOnly = true;
                txtComments.ReadOnly = true;
                txtControlName.ReadOnly = true;
                memoSpec.ReadOnly = true;
                cboValid.ReadOnly = true;


                popupProcesssegmentId.ReadOnly = true;
                popupProcesssegmentId.ClearButtonReadOnly = true;
                popupProcesssegmentId.SearchButtonReadOnly = true;

                txtProcessSegmentId.ReadOnly = true;
            
                popupDepartment.ReadOnly = true;
                popupDepartment.ClearButtonReadOnly = true;
                popupDepartment.SearchButtonReadOnly = true;
            
                popupAreaId.ReadOnly = true;
                popupAreaId.ClearButtonReadOnly = true;
                popupAreaId.SearchButtonReadOnly = true;

                btnDetailDocFile.Properties.Buttons.Clear();
                btnReportFile.Properties.Buttons.Clear();
                btnCardFile.Properties.Buttons.Clear();

                fpcEquipmentImage.ButtonVisibleDownload();
                fpcCalibration.ButtonVisibleDownload();
                fpcMiddle.ButtonVisibleDownload();
                fpcRNR.ButtonVisibleDownload();

            }
         }


        #endregion

     
    }
 }

