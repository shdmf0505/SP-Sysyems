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
using DevExpress.Utils;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using DevExpress.XtraEditors.Controls;
using Micube.SmartMES.QualityAnalysis.Helper;
using Micube.SmartMES.Commons;
#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 계측기 관리> 수리/폐기> 등록 Popup
    /// 업  무  설  명  : 계측기 수리/폐기 등록 Popup.
    /// 생    성    자  : 이승우
    /// 생    성    일  : 2019-10-01
    /// 수  정  이  력  : 2020-02-10 강유라
    ///                  2019-10-29 Layout 변경
    ///                  2019-10-01 최초 생성일
    ///                  2021.02.23 전우성 관리번호 상세 조회 안되는 오류 수정
    /// 
    /// </summary>
    public partial class MeasuringInstRepairPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
   
        #region Interface
        public DataRow CurrentDataRow { get; set; }
        #endregion

        #region Local Variables
        private DataRow _rowParent;
 
        private bool _isNewData;//팝업을 열때 insertData OR updateData
        private bool _isDisplay;//display 용 여부
        #endregion

        #region 생성자
        public MeasuringInstRepairPopup(bool dataTypeflag, bool displayFlag)
        {
            _isNewData = dataTypeflag;
            _isDisplay = displayFlag;

            InitializeComponent();
            InitializeGridApprovalInfo();
            InitializeGridSymptom(displayFlag);
            InitializeGridOccurrence(displayFlag);
            InitializeGridQATeamAction(displayFlag);
            SetEnableControl();
            BindingRadioButtonData();
            SetFormatSpin();
            InitializeEvent();
            
            fpcImage.showImage = true;
        }
        #endregion

        #region 컨텐츠 영역 초기화
        /// <summary>
        /// updateData 일때(_isNewData == false)
        /// 관리번호 수정 하지 못하게하는 함수
        /// 관리부서, 제조사, 제조국, 장비구분, 계측기명
        /// 모델, 시리얼 넘버 
        /// =>  관리번호선택시 자동입력 : 디자인으로 readOnly = true, useReadOnlyAppearance = false
        ///  수리/폐기 ID => 변경불가
        /// </summary>
        private void SetEnableControl()
        {
            if (!_isNewData)
            {
                popupControlNo.ReadOnly = true;
                popupControlNo.ClearButtonReadOnly = true;
                popupControlNo.SearchButtonReadOnly = true;
                txtRepairDisposalId.ReadOnly = true;
            }

            if (_isDisplay)
            {
                btnSave.Enabled = false;

                popupDepartment.ReadOnly = true;
                popupDepartment.ClearButtonReadOnly = true;
                popupDepartment.SearchButtonReadOnly = true;

                txtPosition.ReadOnly = true;    
                txtRepairuser.ReadOnly = true;
                dteDateOfOccurrence.ReadOnly = true;
                rdoIsLossDanger.ReadOnly = true;
                txtOccurplace.ReadOnly = true;
                dteQaenDtime.ReadOnly = true;
                spinRepairCost.ReadOnly = true;

                fpcImage.ButtonVisibleDownload();
            }
        }

        private void SetFormatSpin()
        {
            spinRepairCost.Properties.MinValue = 0;
            spinRepairCost.Properties.MaxValue = decimal.MaxValue;
            spinRepairCost.Properties.Mask.EditMask = "###,###,###,###,###0";
            spinRepairCost.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;

        }
        /// <summary>
        /// 손망실 구분, 장비구분 라디오 버튼 초기화
        /// </summary>
        private void BindingRadioButtonData()
        {
            Dictionary<string, object> radioParam1 = new Dictionary<string, object>()
            {
                {"CODECLASSID", "MeasuringLossType" },
                {"LANGUAGETYPE", UserInfo.Current.LanguageType}
            };

            DataTable radioDt1 = SqlExecuter.Query("GetCodeList", "00001", radioParam1);

            foreach (DataRow dr in radioDt1.Rows)
                rdoIsLossDanger.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(dr["CODEID"], dr["CODENAME"].ToString()));

        }


        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridApprovalInfo()
        {
            grdApproval.GridButtonItem = GridButtonItem.Export;           
            grdApproval.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdApproval.View.SetIsReadOnly();

            grdApproval.View.AddTextBoxColumn("DEPARTMENT", 150).SetLabel("CSMREQUESTDEPARTMENT");//요청부서
            grdApproval.View.AddComboBoxColumn("CHARGERMODE", 150, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=MeasuringChargerMode")).SetLabel("MEASURINGCHARGERMODE").SetIsReadOnly();//담당자구분
            grdApproval.View.AddTextBoxColumn("CHARGERID", 150).SetLabel("MEASURINGCHARGERID");//담당자ID
            grdApproval.View.AddTextBoxColumn("STATUS", 150).SetLabel("RECEIVER");//상태 - 수신자
            grdApproval.View.AddComboBoxColumn("ISAPPROVAL", 150, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=MeasuringIsapproval")).SetLabel("ISACCEPT").SetIsReadOnly();//승인여부
            grdApproval.View.AddTextBoxColumn("APPROVALTIME", 150).SetLabel("MEASURINGAPPROVALTIME").SetDisplayFormat("yyyy-MM-dd HH;mm:ss");//결제시간
            grdApproval.View.AddTextBoxColumn("REJECTCOMMENTS", 150).SetLabel("REJECTCOMMENTS");//반려사유
            grdApproval.View.AddTextBoxColumn("SERIALNO", 150).SetLabel("MEASURINGSERIALNO");//시리얼번호 PK
            grdApproval.View.AddTextBoxColumn("CONTROLNO", 150).SetLabel("MEASURINGCONTROLNO");//관리번호 PK
            grdApproval.View.AddTextBoxColumn("REPAIRSCRAPID", 150).SetLabel("MEASURINGREPAIRSCRAPID");//수리폐기ID PK
            grdApproval.View.AddTextBoxColumn("SEQ", 150).SetLabel("MAINTSEQUENCE");//순번

            grdApproval.View.PopulateColumns();//Grid 활성화

            grdApproval.View.OptionsView.AllowCellMerge = true; //CellMerge
            //grdApproval.View.Columns["DEPARTMENT"].OptionsColumn.AllowMerge = DefaultBoolean.True;

            grdApproval.View.Columns["SEQ"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdApproval.View.Columns["CHARGERMODE"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdApproval.View.Columns["CHARGERID"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdApproval.View.Columns["APPROVALTIME"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdApproval.View.Columns["REJECTCOMMENTS"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdApproval.View.Columns["SERIALNO"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdApproval.View.Columns["CONTROLNO"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdApproval.View.Columns["REPAIRSCRAPID"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdApproval.View.Columns["STATUS"].OptionsColumn.AllowMerge = DefaultBoolean.False;


        }

        //Grid - 증상
        private void InitializeGridSymptom(bool isDisplay)
        {
            if(isDisplay)
            grdOccurrence.GridButtonItem = GridButtonItem.Export;
            else
            grdOccurrence.GridButtonItem = GridButtonItem.Add | GridButtonItem.Export;

            grdOccurrence.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdOccurrence.View.SetSortOrder("SEQ");

            grdOccurrence.View.AddSpinEditColumn("SEQ", 70).SetLabel("MAINTSEQUENCE").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);//순번
            grdOccurrence.View.AddTextBoxColumn("COMMENTS", 480).SetLabel("MEASURINGDESCRIPTION");//내용
            grdOccurrence.View.AddTextBoxColumn("SERIALNO", 150).SetLabel("MEASURINGSERIALNO").SetIsHidden();//시리얼번호 PK
            grdOccurrence.View.AddTextBoxColumn("CONTROLNO", 150).SetLabel("MEASURINGCONTROLNO").SetIsHidden();//관리번호 PK
            grdOccurrence.View.AddTextBoxColumn("REPAIRSCRAPID", 150).SetLabel("MEASURINGREPAIRSCRAPID").SetIsHidden();//수리폐기ID PK
            grdOccurrence.View.AddTextBoxColumn("CANDELETE", 150).SetIsHidden();//삭제가능 여부
            grdOccurrence.View.PopulateColumns();

        }

        //Grid - 발생내용
        private void InitializeGridOccurrence(bool isDisplay)
        {
            if (isDisplay)
                grdSymptom.GridButtonItem = GridButtonItem.Export;
            else
                grdSymptom.GridButtonItem = GridButtonItem.Add | GridButtonItem.Export;

            grdSymptom.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdSymptom.View.SetSortOrder("SEQ");

            grdSymptom.View.AddSpinEditColumn("SEQ", 70).SetLabel("MAINTSEQUENCE").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);//순번
            grdSymptom.View.AddTextBoxColumn("COMMENTS", 460).SetLabel("MEASURINGDESCRIPTION");//내용
            grdSymptom.View.AddTextBoxColumn("SERIALNO", 150).SetLabel("MEASURINGSERIALNO").SetIsHidden();//시리얼번호 PK
            grdSymptom.View.AddTextBoxColumn("CONTROLNO", 150).SetLabel("MEASURINGCONTROLNO").SetIsHidden();//관리번호 PK
            grdSymptom.View.AddTextBoxColumn("REPAIRSCRAPID", 150).SetLabel("MEASURINGREPAIRSCRAPID").SetIsHidden();//수리폐기ID PK
            grdSymptom.View.AddTextBoxColumn("CANDELETE", 150).SetIsHidden();//삭제가능 여부
            grdSymptom.View.PopulateColumns();

        }

        //Grid - QA팀 조치 내용
        private void InitializeGridQATeamAction(bool isDisplay)
        {
            if (isDisplay)
                grdQATeamAction.GridButtonItem = GridButtonItem.Export;
            else
                grdQATeamAction.GridButtonItem = GridButtonItem.Add | GridButtonItem.Export;

            grdQATeamAction.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdQATeamAction.View.SetSortOrder("SEQ");

            grdQATeamAction.View.AddSpinEditColumn("SEQ", 70).SetLabel("MAINTSEQUENCE").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);//순번
            grdQATeamAction.View.AddTextBoxColumn("COMMENTS", 1050).SetLabel("MEASURINGDESCRIPTION");//내용
            grdQATeamAction.View.AddTextBoxColumn("SERIALNO", 150).SetLabel("MEASURINGSERIALNO").SetIsReadOnly().SetIsHidden();//시리얼번호 PK
            grdQATeamAction.View.AddTextBoxColumn("CONTROLNO", 150).SetLabel("MEASURINGCONTROLNO").SetIsReadOnly().SetIsHidden();//관리번호 PK
            grdQATeamAction.View.AddTextBoxColumn("REPAIRSCRAPID", 150).SetLabel("MEASURINGREPAIRSCRAPID").SetIsReadOnly().SetIsHidden();//수리폐기ID PK
            grdQATeamAction.View.AddTextBoxColumn("CANDELETE", 150).SetIsHidden();//삭제가능 여부
            grdQATeamAction.View.PopulateColumns();

        }

        #endregion

        #region Event
        private void InitializeEvent()
        {
            Load += (s, e) =>
            {
                InitializePopup();
                this.DataEditRead();

                this.GetMeasuringRepairApprovalGridData();//결제 이력 조회
            };

            popupControlNo.ButtonClick += (s, e) =>
            {
                if (e.Button.Kind.Equals(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear))
                {
                    txtDepartment.Text = string.Empty;
                    txtManuFacturer.Text = string.Empty;
                    txtManuFactureCountry.Text = string.Empty;
                    txtEquipmentType.Text = string.Empty;
                    txtMeasureId.Text = string.Empty;
                    txtMeasureProductdefId.Text = string.Empty;
                    txtSerialNo.Text = string.Empty;
                    txtPurchaseDate.Text = string.Empty;
                    memoTypeSpec.EditValue = string.Empty;
                }
            };


            //저장버튼 클릭 이벤트
            btnSave.Click += BtnSave_Click;

            //Sub Grid + 버튼 클릭시 이벤트
            grdOccurrence.ToolbarAddingRow += SubGrd_ToolbarAddingRow;
            grdSymptom.ToolbarAddingRow += SubGrd_ToolbarAddingRow;
            grdQATeamAction.ToolbarAddingRow += SubGrd_ToolbarAddingRow;

            grdOccurrence.View.AddingNewRow += SubGrd_AddingNewRow;
            grdSymptom.View.AddingNewRow += SubGrd_AddingNewRow;
            grdQATeamAction.View.AddingNewRow += SubGrd_AddingNewRow;

            //Sub Grid - 버튼 클릭시 이벤트
            grdOccurrence.ToolbarDeleteRow += SubGrd_ToolbarDeleteRow;
            grdSymptom.ToolbarDeleteRow += SubGrd_ToolbarDeleteRow;
            grdQATeamAction.ToolbarDeleteRow += SubGrd_ToolbarDeleteRow;

            if(!_isDisplay)
            { 
                new SetGridDeleteButonVisibleSimple(grdOccurrence);
                new SetGridDeleteButonVisibleSimple(grdSymptom);
                new SetGridDeleteButonVisibleSimple(grdQATeamAction);
            }

            //2020-02-18 강유라 파일컨트롤의 포커스된 row바뀔 때이벤트
            fpcImage.FocusedFileRowChangedEvent += (row) => BindingImage(row);
        }

        #region DB 저장 및 Read
        /// <summary>
        /// 입력 자료 저장 Data Table 구성.
        /// </summary>
        /// <param name="data"></param>
        private void DataEditRead()
        {
            if(!_isNewData)
            { 
                popupDepartment.Text = _rowParent["DEPARTMENTMNT"].ToSafeString();//사용부서
                popupDepartment.SetValue(_rowParent["DEPARTMENTMNT"].ToSafeString());//사용부서
                popupDepartment.Text = _rowParent["DEPARTMENTMNT"].ToSafeString();//사용부서
                txtPosition.Text = _rowParent["POSITION"].ToSafeString();//수리내역 등록자 직급
                txtRepairuser.Text = _rowParent["REPAIRUSER"].ToSafeString();//수리내역 등록자 성명
                txtDepartment.Text = _rowParent["DEPARTMENT"].ToSafeString();//관리부서
                txtManuFactureCountry.Text = _rowParent["MANUFACTURECOUNTRY"].ToSafeString();//제조국
                txtManuFacturer.Text = _rowParent["MANUFACTURER"].ToSafeString();//제조사
                //PK
                popupControlNo.Text = _rowParent["CONTROLNO"].ToSafeString();//관리번호 PK
                popupControlNo.SetValue(_rowParent["CONTROLNO"].ToSafeString());//관리번호 PK
                txtSerialNo.Text = _rowParent["SERIALNO"].ToSafeString();//시리얼번호 PK
        

                txtRepairDisposalId.Text = _rowParent["REPAIRSCRAPID"].ToSafeString();//수리폐기ID PK

                //발생일자
                dteDateOfOccurrence.EditValue = _rowParent["OCCURTIME"].ToSafeString();
            

                //손망실 구분
                rdoIsLossDanger.EditValue = _rowParent["ISLOSSDANGERS"].ToSafeString();//손망실 구분
    
                //장비구분
                txtEquipmentType.EditValue = _rowParent["EQUIPMENTTYPENAME"].ToSafeString();//손망실 구분

                //계측기명
                txtMeasureId.Text =  _rowParent["MEASURINGID"].ToSafeString();//계측기명
                //모델
                txtMeasureProductdefId.Text =  _rowParent["MEASURINGPRODUCTID"].ToSafeString();//모델

                //발생장소
                txtOccurplace.Text = _rowParent["OCCURPLACE"].ToSafeString();

                //구매일
                txtPurchaseDate.Text = _rowParent["PURCHASEDATE"].ToSafeString();
           
                //QA완료일자
                dteQaenDtime.EditValue = _rowParent["QAENDTIME"].ToSafeString();       

                //TypeSpec
                memoTypeSpec.EditValue= _rowParent["TYPESPEC"].ToSafeString();

                this.spinRepairCost.Text = _rowParent["REPAIRCOST"].ToSafeString();

                string fileResourceId = Format.GetString(_rowParent["SERIALNO"]) 
                    + Format.GetString(_rowParent["CONTROLNO"]) + Format.GetString(_rowParent["REPAIRSCRAPID"]);
                SearchFile(fileResourceId, "MeasurementScrap", fpcImage, true);

                //Sub 그리드 표시.
                this.grdRepairSubDataView();
            }

        }

        /// <summary>
        /// - 버튼 클릭하여 ROW 없앨때 순번 다시 매기기
        /// rowState가 added인것만
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubGrd_ToolbarDeleteRow(object sender, EventArgs e)
        {
            
            SmartBandedGrid grid = sender as SmartBandedGrid;

            int totalCount = grid.View.RowCount;// 전체 row 수
            DataTable dt = grid.GetChangesAdded();//added인 row 수
            var handles = grid.View.GetRowHandlesByValue("CANDELETE", "Y");//added인 row의 RowHandles
        
            if (handles != null)
            {
                int seq = totalCount - dt.Rows.Count + 1; // seq매길 시작 숫자

                foreach (int item in handles)
                {
                    grid.View.SetRowCellValue(item,"SEQ" ,seq);
                    seq++;                  
                }

            }
        }


        /// <summary>
        /// 닫기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        private void SaveData()
        {
            grdOccurrence.View.CloseEditor();
            grdSymptom.View.CloseEditor();
            grdQATeamAction.View.CloseEditor();

            if (!CheckHasComment(grdOccurrence.GetChangedRows(), grdSymptom.GetChangedRows(), grdQATeamAction.GetChangedRows()))
                return;

            if (!ValidationRequired())
                return;

            DialogResult result = System.Windows.Forms.DialogResult.No;
            this.DialogResult = DialogResult.Cancel;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "InfoSave");

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    btnSave.Enabled = false;
                    btnClose.Enabled = false;
                    this.ShowWaitArea();

                    //저장 Rule 변경
                    DataTable dtScrap = CreateDataTableScrap();
                    this.DataEditSaveScrap(ref dtScrap);
    
                    DataTable dtSymptoms = SetSubGrdDefault(grdSymptom.GetChangedRows().Copy());
                    DataTable dtOccur = SetSubGrdDefault(grdOccurrence.GetChangedRows().Copy());
                    DataTable dtAction = SetSubGrdDefault(grdQATeamAction.GetChangedRows().Copy());

                    DataTable equipmentImage = fpcImage.GetChangedRows();

                    equipmentImage.Columns.Add("PROCESSINGSTATUS");

                    int fileSize = 0;

                    foreach (DataRow row in equipmentImage.Rows)
                    {
                        row["PROCESSINGSTATUS"] = "Wait";
                        row["FILEPATH"] = "MeasurementScrap/Image";
                        fileSize += row.GetInteger("FILESIZE");
                    }

                    if (equipmentImage.Rows.Count > 0)
                    {
                        FileProgressDialog fileProgressDialog = new FileProgressDialog(equipmentImage, UpDownType.Upload, "", fileSize);
                        fileProgressDialog.ShowDialog(this);

                        if (fileProgressDialog.DialogResult == DialogResult.Cancel)
                            throw MessageException.Create("CancelMessageToUploadFile");  //파일업로드를 취소하였습니다.

                        ProgressingResult fileResult = fileProgressDialog.Result;

                        if (!fileResult.IsSuccess)
                            throw MessageException.Create("FailMessageToUploadFile"); //파일업로드를 실패하였습니다.
                    }

                MessageWorker worker = new MessageWorker("SaveMeasuringInstRepair");
                worker.SetBody(new MessageBody()
                {
                    { "list", dtScrap},
                    { "list2", dtSymptoms},
                    { "list3", dtOccur},
                    { "list4", dtAction},
                    { "list5", equipmentImage.Copy()},
                });

                    worker.Execute();
                    this.ShowMessage("SuccessSave");//sia확인 : 다국어 처리.                 
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {

                    this.CloseWaitArea();
                    btnSave.Enabled = true;
                    btnClose.Enabled = true;

                }
            }
            
        }

        /// <summary>
        /// 입력 자료 저장 Data Table 구성.
        /// </summary>
        /// <param name="data"></param>
        private void DataEditSaveScrap(ref DataTable data)
        {
            DataRow dataRow = data.NewRow();
            //PK
            dataRow["SERIALNO"] = txtSerialNo.Text;
            dataRow["CONTROLNO"] = popupControlNo.GetValue().ToString();
            dataRow["REPAIRSCRAPID"] = txtRepairDisposalId.Text;

            //Data
            dataRow["DEPARTMENTMNT"] = popupDepartment.Text;
            dataRow["DEPARTMENT"] = txtDepartment.Text;
            //손망실 구분           
            dataRow["ISLOSSDANGERS"] = rdoIsLossDanger.EditValue;

            dataRow["POSITION"] = txtPosition.Text;     //수리내역 등록자 직급
            dataRow["REPAIRUSER"] = txtRepairuser.Text; //수리내역 등록자 성명

            //장비구분                    
            dataRow["OCCURTIME"] = string.IsNullOrWhiteSpace(Format.GetString(dteDateOfOccurrence.EditValue)) ? DBNull.Value : dteDateOfOccurrence.EditValue;
            dataRow["OCCURPLACE"] = txtOccurplace.Text;
            dataRow["PHOTO"] = "";
            dataRow["QAENDTIME"] = string.IsNullOrWhiteSpace(Format.GetString(dteQaenDtime.EditValue)) ? DBNull.Value : dteQaenDtime.EditValue;
            
            dataRow["REPAIRCOST"] = spinRepairCost.Text;
            dataRow["VALIDSTATE"] = "Valid";

            data.Rows.Add(dataRow);
            
        }

        /// <summary>
        /// Sub그리드 저장시 시리얼,관리번호,수리폐시 Id
        /// 입력 및 내용 없는 경우 거르기
        /// </summary>
        /// <param name="table1"></param>
        /// <param name="table2"></param>
        /// <param name="table3"></param>
        private bool CheckHasComment(DataTable table1, DataTable table2, DataTable table3)
        {
            bool result = true;

            table1.Merge(table2);
            table1.Merge(table3);

            var noCommentCount = table1.AsEnumerable()
                .Where(r => string.IsNullOrWhiteSpace(Format.GetString(r["COMMENTS"])))
                .Count();

            if (noCommentCount > 0)
            {
                result =false;
                //MSGBox.Show(MessageBoxType.Error,"NoCommentsMeasurement");
                throw MessageException.Create("NoCommentsMeasurement");
            }
            return result;
        }

        /// <summary>
        /// 리얼,관리번호,수리폐시 Id 입력
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private DataTable SetSubGrdDefault(DataTable table)
        {
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    row["SERIALNO"] = txtSerialNo.Text;
                    row["CONTROLNO"] = popupControlNo.GetValue();
                    row["REPAIRSCRAPID"] = txtRepairDisposalId.Text;
                }

            }
            return table;
        }
      

        /// <summary>
        /// 저장 버튼 클릭시 필수값 입력 여부체크
        /// </summary>
        private bool ValidationRequired()
        {
            bool result = true;

            if (string.IsNullOrWhiteSpace(Format.GetString(txtSerialNo.Text)))
            {
                result = false;
                throw MessageException.Create("NoSerialMeasurement");
            }

            if (string.IsNullOrWhiteSpace(Format.GetString(popupControlNo.Text)))
            {
                result = false;
                throw MessageException.Create("NoControlNoMeasurement");
            }

            if (string.IsNullOrWhiteSpace(Format.GetString(txtRepairDisposalId.Text)))
            {
                result = false;
                throw MessageException.Create("NoRepairscrapIdMeasurement");
            }

            return result;
        }
        #endregion DB 저장


        /// <summary>
        /// 계측기 이력카드 DataTable 생성.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable CreateDataTableScrap(string tableName = "measuringinstRepairScrap")
        {
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            //Filed
            dt.Columns.Add("SERIALNO", typeof(string));
            dt.Columns.Add("CONTROLNO", typeof(string));
            dt.Columns.Add("REPAIRSCRAPID", typeof(string));
            dt.Columns.Add("DEPARTMENTMNT", typeof(string));
            dt.Columns.Add("DEPARTMENT", typeof(string));
            dt.Columns.Add("ISLOSSDANGERS", typeof(string));
            dt.Columns.Add("POSITION", typeof(string));
            dt.Columns.Add("REPAIRUSER", typeof(string));
            dt.Columns.Add("OCCURTIME", typeof(DateTime));
            dt.Columns.Add("OCCURPLACE", typeof(string));
            dt.Columns.Add("PHOTO", typeof(string));
            dt.Columns.Add("QAENDTIME", typeof(DateTime));
            dt.Columns.Add("REPAIRCOST", typeof(double));
            dt.Columns.Add("VALIDSTATE", typeof(string));

            return dt;
        }

        #region Grid 이벤트
        /// <summary>
        /// row 추가시 sequence 자동 입력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void SubGrd_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            SmartBandedGridView view = sender as SmartBandedGridView;

            int rowCount = view.RowCount;
            args.NewRow["SEQ"] = rowCount;
            args.NewRow["CANDELETE"] = "Y";
        }

        /// <summary>
        /// Sub Grid +버튼 클릭시 이벤트
        /// 시리얼, 관리,수리폐기ID가 없을경우 row추가 불가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubGrd_ToolbarAddingRow(object sender, CancelEventArgs e)
        {

            if (!ValidationRequired())
                e.Cancel = true;
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
                this.grdApproval.View.ClearDatas();
                Dictionary<string, object> param = new Dictionary<string, object>();
                //param.Add("P_PLANTID", plantID);
                param.Add("P_SERIALNO", _rowParent["SERIALNO"].ToSafeString());
                param.Add("P_CONTROLNO", _rowParent["CONTROLNO"].ToSafeString());
                param.Add("P_REPAIRSCRAPID", _rowParent["REPAIRSCRAPID"].ToSafeString());

                //SqlQuery rnrUserList = new SqlQuery("GetAreaidListByCsm", "10001", $"P_PLANTID={UserInfo.Current.Plant}", $"P_AREANAME={UserInfo.Current.Area}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
                SqlQuery approvalList = new SqlQuery("SelectRawMeasuringRepairApproveListDirect", "10001", param);
                DataTable approvalTable = approvalList.Execute();
                if (approvalTable != null && approvalTable.Rows.Count > 0)
                {
                    this.grdApproval.DataSource = approvalTable;
                }
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

        #region Sub Grid DB 검색
        /// <summary>
        /// 그리드 - (발생, 증상, 조치) 자료 설정.
        /// </summary>
        private void grdRepairSubDataView()
        {
            try
            {
                this.OnSearchMeasuringRepairOccurList();
                this.OnSearchMeasuringRepairSymptomsList();
                this.OnSearchMeasuringRepairActionList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            finally
            {
            }
        }

        /// <summary>
        /// 자료조회 - 발생
        /// </summary>
        private void OnSearchMeasuringRepairOccurList()
        {
            try
            {
                string serialNo = this.txtSerialNo.Text;
                string controlNo = popupControlNo.GetValue().ToString();
                string repairScrapID = this.txtRepairDisposalId.Text;

                Dictionary<string, object> param = new Dictionary<string, object>();
                //param.Add("P_PLANTID", plantID);
                param.Add("P_SERIALNO", serialNo);
                param.Add("P_CONTROLNO", controlNo);
                param.Add("P_REPAIRSCRAPID", repairScrapID);

                //SqlQuery rnrUserList = new SqlQuery("GetAreaidListByCsm", "10001", $"P_PLANTID={UserInfo.Current.Plant}", $"P_AREANAME={UserInfo.Current.Area}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
                SqlQuery occurList = new SqlQuery("SelectRawmeasuringRepairOccurDirect", "10001", param);
                DataTable occurTable = occurList.Execute();
               
                this.grdOccurrence.DataSource = occurTable;
                
               
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        /// <summary>
        /// 자료조회 - 증상
        /// </summary>
        private void OnSearchMeasuringRepairSymptomsList()
        {
            try
            {
                string serialNo = this.txtSerialNo.Text;
                string controlNo = popupControlNo.GetValue().ToString();
                string repairScrapID = this.txtRepairDisposalId.Text;


                Dictionary<string, object> param = new Dictionary<string, object>();
                //param.Add("P_PLANTID", plantID);
                param.Add("P_SERIALNO", serialNo);
                param.Add("P_CONTROLNO", controlNo);
                param.Add("P_REPAIRSCRAPID", repairScrapID);

                //SqlQuery rnrUserList = new SqlQuery("GetAreaidListByCsm", "10001", $"P_PLANTID={UserInfo.Current.Plant}", $"P_AREANAME={UserInfo.Current.Area}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
                SqlQuery symptomsList = new SqlQuery("SelectRawmeasuringRepairSymptomsDirect", "10001", param);
                DataTable symptomsTable = symptomsList.Execute();
                
                this.grdSymptom.DataSource = symptomsTable;
              
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        /// <summary>
        /// 자료조회 - 조치 
        /// </summary>
        private void OnSearchMeasuringRepairActionList()
        {
            try
            {
                string serialNo = this.txtSerialNo.Text;
                string controlNo = popupControlNo.GetValue().ToString();
                string repairScrapID = this.txtRepairDisposalId.Text;

                Dictionary<string, object> param = new Dictionary<string, object>();
                //param.Add("P_PLANTID", plantID);
                param.Add("P_SERIALNO", serialNo);
                param.Add("P_CONTROLNO", controlNo);
                param.Add("P_REPAIRSCRAPID", repairScrapID);

                //SqlQuery rnrUserList = new SqlQuery("GetAreaidListByCsm", "10001", $"P_PLANTID={UserInfo.Current.Plant}", $"P_AREANAME={UserInfo.Current.Area}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
                SqlQuery actionList = new SqlQuery("SelectRawmeasuringRepairActionDirect", "10001", param);
                DataTable actionTable = actionList.Execute();
                this.grdQATeamAction.DataSource = actionTable;
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        #endregion

        #endregion

        #region Public Function    
        /// <summary>
        /// Popup 실행 폼의 Grid Row Data 전이
        /// </summary>
        /// <param name="_rowParent"></param>
        public void SetRowParent(DataRow rowParent)
        {
            this._rowParent = rowParent;
           
        }

        #endregion

        #region Initialize Popup

        /// <summary>
        /// Popup 컨트롤 초기화
        /// </summary>
        private void InitializePopup()
        {
            //관리번호
            popupControlNo.SelectPopupCondition = MeasuringControlNoPopupMuti();

            //사용부서
            popupDepartment.SelectPopupCondition = DepartmentInfoPopup();         
        }
     
        #region 관리번호 선택 팝업 - 계측기 
        /// <summary>
        /// 관리번호 팝업
        /// </summary>
        /// <returns></returns>
        private ConditionItemSelectPopup MeasuringControlNoPopupMuti(bool isMultiSelect = true)
        {
            Dictionary<string, object> value = new Dictionary<string, object>()
            {
                {"ISDISPLAY","Y"},
                {"LANGUAGETYPE",UserInfo.Current.LanguageType}

            };

            //sia작업 : RepairPopup-관리번호 팝업
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();
            popup.SetPopupLayoutForm(800, 800, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("MEASURINGCONTROLNO", PopupButtonStyles.Ok_Cancel, true, true);
            popup.Id = "CONTROLNO";
            popup.SearchQuery = new SqlQuery("SelectRawMeasuringCardInformation", "10001", value);
            popup.DisplayFieldName = "CONTROLNO";
            popup.ValueFieldName = "CONTROLNO";
            popup.SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
            {
                foreach (DataRow row in selectedRow)
                {
                    if (selectedRow.Count() > 0)
                    {
                        txtDepartment.Text = row["DEPARTMENT"].ToString();
                        txtManuFacturer.Text = row["MANUFACTURER"].ToString();
                        txtManuFactureCountry.Text = row["MANUFACTURECOUNTRY"].ToString();
                        txtEquipmentType.EditValue = row["EQUIPMENTTYPENAME"].ToString();
                        txtMeasureId.Text = row["MEASURINGID"].ToString();
                        txtMeasureProductdefId.Text = row["MEASURINGPRODUCTID"].ToString();
                        txtSerialNo.Text = row["SERIALNO"].ToString();
                        txtPurchaseDate.Text = row["PURCHASEDATE"].ToString();
                        txtControlName.Text = row["CONTROLNAME"].ToString();
                        memoTypeSpec.EditValue = row["TYPESPEC"].ToString();
                       
                    }
                    else
                    {
                        txtDepartment.Text = string.Empty;
                        txtManuFacturer.Text = string.Empty;
                        txtManuFactureCountry.Text = string.Empty;
                        txtEquipmentType.Text = string.Empty;
                        txtMeasureId.Text = string.Empty;
                        txtMeasureProductdefId.Text = string.Empty;
                        txtSerialNo.Text = string.Empty;
                        txtPurchaseDate.Text = string.Empty;
                        txtControlName.Text = string.Empty;
                        memoTypeSpec.EditValue = string.Empty;

                    }
                }
            });

            popup.Conditions.AddTextBox("P_MEASURINGCONTROLNO").SetLabel("MEASURINGCONTROLNO");
            popup.Conditions.AddTextBox("P_MEASURINGSERIALNO").SetLabel("MEASURINGSERIALNO");
            popup.Conditions.AddTextBox("P_PLANTID").SetLabel("PLANTID");

            popup.GridColumns.AddTextBoxColumn("SERIALNO", 150).SetLabel("MEASURINGSERIALNO");
            popup.GridColumns.AddTextBoxColumn("CONTROLNO", 150).SetLabel("MEASURINGCONTROLNO") ;
            popup.GridColumns.AddTextBoxColumn("CONTROLNAME", 150).SetLabel("MEASURINGCONTROLNAME") ;
            popup.GridColumns.AddTextBoxColumn("MEASURINGID", 150).SetLabel("MEASURINGEQUIPMENTID");
            popup.GridColumns.AddTextBoxColumn("MEASURINGPRODUCTID", 150).SetLabel("MEASURINGPRODUCTDEFNAME");

            return popup;
        }
        #endregion

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

            popup.SetPopupLayoutForm(400, 600, FormBorderStyle.FixedDialog);
            //popup.SetPopupLayout(Language.Get("DEPARTMENTNAME"), PopupButtonStyles.Ok_Cancel, true, false);
            popup.SetPopupLayout("DEPARTMENTINFO", PopupButtonStyles.Ok_Cancel, true, true);
            popup.Id = "DEPARTMENTNAME";
            popup.SearchQuery = new SqlQuery("GetMeasuringSearchDepartment", "10001", param);
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "DEPARTMENTNAME";
            popup.ValueFieldName = "DEPARTMENTNAME";

            popup.Conditions.AddTextBox("DEPARTMENTNAME");
            
            popup.GridColumns.AddTextBoxColumn("DEPARTMENTNAME", 150).SetLabel("DEPARTMENTNAME");

            return popup;
        }

        #endregion

        #endregion

   
    }
}

