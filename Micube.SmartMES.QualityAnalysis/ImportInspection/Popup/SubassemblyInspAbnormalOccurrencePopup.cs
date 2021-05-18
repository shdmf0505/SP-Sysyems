using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
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
    /// 프 로 그 램 명  : 품질관리 > 수입 검사 관리 >원자재 가공품 수입검사 이상발생의 CAR요청 팝업
    /// 업  무  설  명  : 
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-08-29
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    /// 

    public partial class SubassemblyInspAbnormalOccurrencePopup : SmartPopupBaseForm, ISmartCustomPopup
    {

        #region Interface

        public DataRow CurrentDataRow { get; set; }
        

        #endregion

        #region Local Variables
        public DataTable _defectTable;
        public DataTable _measureTable;
        private DataTable _changedAffectLot;//저장 할 affectLot
        public bool isEnable = true;


        /// <summary>
        /// SmartBaseForm을 사용하기 위한 객체
        /// </summary>
        public SmartBaseForm _form = new SmartBaseForm();
        #endregion

        #region 생성자
        public SubassemblyInspAbnormalOccurrencePopup(DataRow row)
        {
            InitializeComponent();
            InitializeEvent();

            CurrentDataRow = row;
            grpSubAssemblyInfo.GridButtonItem = GridButtonItem.None;

            InitializeRawAssyGrid();
            InitializeMeasureGrid();

            InitializeCurrentData();

            ucIssueRegistration.isShowReasonCombo = true;
            ucIssueRegistration.SetControlsVisible();
        }
        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 그리드에서 선택한 행에 대한 데이터바인딩
        /// </summary>
        private void InitializeCurrentData()
        {
            txtVendorName.EditValue = CurrentDataRow["VENDORNAME"]; // 업체명
            txtAcceptDate.EditValue = CurrentDataRow["ACCEPTDATE"];//검사입고일시
            txtLotId.EditValue = CurrentDataRow["LOTID"];//LOTID
            txtQty.EditValue = CurrentDataRow["CREATEDQTY"]; // 대상수량           
            txtProcessSegmentName.EditValue = CurrentDataRow["PROCESSSEGMENTNAME"]; // 공정명           
            txtMaterialLotId.EditValue = CurrentDataRow["MATERIALLOTID"]; // 자재LOT ID
            ucIssueRegistration.ParentDataRow = CurrentDataRow;


            ncrProgressControl.CurrentDataRow = CurrentDataRow;
            SearchAutoAffectLot();
        }

        private void InitializeRawAssyGrid()
        {
            grdInspectionItem.GridButtonItem = GridButtonItem.None;
            grdInspectionItem.View.OptionsView.AllowCellMerge = true;
            grdInspectionItem.View.SetIsReadOnly();

            grdInspectionItem.View.SetSortOrder("SORT");

            //grdInspectionItem.View.AddTextBoxColumn("TYPE", 200)
            //   .SetIsReadOnly()
            //   .SetLabel("INSPTYPE");

            grdInspectionItem.View.AddTextBoxColumn("INSPITEMCLASSID", 200)
                .SetIsReadOnly()
                .SetIsHidden();

            grdInspectionItem.View.AddTextBoxColumn("INSPECTIONMETHODNAME", 150)
                .SetIsReadOnly();

            grdInspectionItem.View.AddTextBoxColumn("INSPITEMID", 200)
                .SetIsReadOnly()
                .SetIsHidden();

            grdInspectionItem.View.AddTextBoxColumn("INSPITEMNAME", 150)
                .SetIsReadOnly();

            grdInspectionItem.View.AddTextBoxColumn("INSPITEMVERSION", 300)
                .SetIsHidden();

            grdInspectionItem.View.AddTextBoxColumn("INSPITEMTYPE", 300)
                .SetIsHidden();

            grdInspectionItem.View.AddSpinEditColumn("INSPECTIONQTY", 100)
                .SetTextAlignment(TextAlignment.Right);

            grdInspectionItem.View.AddSpinEditColumn("SPECOUTQTY", 100)
                .SetTextAlignment(TextAlignment.Right);

            grdInspectionItem.View.AddTextBoxColumn("DEFECTRATE", 100)
                .SetIsReadOnly()
               // .SetDisplayFormat("{###.#:P0}", MaskTypes.Numeric);
               .SetDisplayFormat("###.#", MaskTypes.Numeric)
               .SetTextAlignment(TextAlignment.Right);


            grdInspectionItem.View.AddComboBoxColumn("INSPECTIONRESULT", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OKNG", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();

            grdInspectionItem.View.AddTextBoxColumn("SORT", 300)
                .SetIsHidden();

            grdInspectionItem.View.AddTextBoxColumn("TXNHISTKEY", 300)
                .SetIsHidden();

            grdInspectionItem.View.AddTextBoxColumn("RESOURCEID", 300)
                .SetIsHidden();

            grdInspectionItem.View.AddTextBoxColumn("RESOURCETYPE", 300)
                .SetIsHidden();

            grdInspectionItem.View.AddTextBoxColumn("PROCESSRELNO", 300)
                .SetIsHidden();

            grdInspectionItem.View.PopulateColumns();
        }

        private void InitializeMeasureGrid()
        {
            grdInspectionItemSpec.GridButtonItem = GridButtonItem.None;
            grdInspectionItemSpec.View.OptionsView.AllowCellMerge = true;
            grdInspectionItemSpec.View.SetIsReadOnly();

            grdInspectionItemSpec.View.SetSortOrder("SORT");

            //grdInspectionItemSpec.View.AddTextBoxColumn("TYPE", 200)
            //    .SetIsReadOnly()
            //    .SetLabel("INSPTYPE");

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMCLASSID", 200)
                .SetIsReadOnly()
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPECTIONMETHODNAME", 150)
                .SetIsReadOnly();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMID", 200)
                .SetIsReadOnly()
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMNAME", 150)
                .SetIsReadOnly();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMVERSION", 200)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMTYPE", 200)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("MEASUREVALUE", 100)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,0.######", MaskTypes.Numeric);

            grdInspectionItemSpec.View.AddComboBoxColumn("INSPECTIONRESULT", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OKNG", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPECTIONSTANDARD", 200)
                .SetIsReadOnly();

            grdInspectionItemSpec.View.AddTextBoxColumn("SORT", 300)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("TXNHISTKEY", 300)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("RESOURCEID", 300)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("RESOURCETYPE", 300)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("PROCESSRELNO", 300)
                .SetIsHidden();

            grdInspectionItemSpec.View.PopulateColumns();
        }

        #endregion

        #region Event
        
        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // 폼 로드
            this.Load += RawMaterialImportInspectionAbnormalOccurrencePopup_Load;

            // 저장버튼
            btnSave.Click += BtnSave_Click;
            // 닫기버튼
            btnClose.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            grdInspectionItem.View.CellMerge += View_CellMerge;
            grdInspectionItemSpec.View.CellMerge += View_CellMerge;


            // 외관검사 이미지
            grdInspectionItem.View.FocusedRowChanged += View_FocusedRowChanged;
        }

        /// <summary>
        /// 외관검사 그리드 포커스 바뀔때 이미지 바인딩 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = grdInspectionItem.View.GetDataRow(e.FocusedRowHandle);
            SearchImage(row);
        }
        /// <summary>
        /// CellMerge이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            GridView view = sender as GridView;

            if (view == null) return;

            if (e.Column.FieldName == "INSPECTIONMETHODNAME" || e.Column.FieldName == "INSPECTIONSTANDARD")
            {
                string str1 = view.GetRowCellDisplayText(e.RowHandle1, e.Column);
                string str2 = view.GetRowCellDisplayText(e.RowHandle2, e.Column);
                e.Merge = (str1 == str2);
            }
            else
            {
                e.Merge = false;
            }

            e.Handled = true;
        }
        /// <summary>
        /// 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            // 임시저장 했는지 체크
            switch (ucIssueRegistration._tabIndex)
            {
                case 0:
                    CheckTempData(0, Format.GetString(ucIssueRegistration.cboRequestNumber.EditValue), ucIssueRegistration._requestDt);
                    break;
                case 1:
                    CheckTempData(1, Format.GetString(ucIssueRegistration.cboReceiptNumber.EditValue), ucIssueRegistration._receiptDt);
                    break;
                case 2:
                    CheckTempData(2, Format.GetString(ucIssueRegistration.cboAcceptNumber.EditValue), ucIssueRegistration._acceptDt);
                    break;
                case 3:
                    CheckTempData(3, Format.GetString(ucIssueRegistration.cboValidationNumber.EditValue), ucIssueRegistration._validationDt);
                    break;
            }

            if (this.ShowMessageBox("InfoPopupSave", "Caption", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                // 저장할 데이터가 있는지 체크 후 저장
                CheckDataAndSaveCarAllState(ucIssueRegistration);
                this.Close();
                this.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// Form Road시 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RawMaterialImportInspectionAbnormalOccurrencePopup_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = isEnable;

            ucIssueRegistration._queryId = "GetNGCountAbnormal";
            ucIssueRegistration._queryVersion = "10001";
            ucIssueRegistration._inspectionType = "SubassemblyInspection";
    
            grdInspectionItem.DataSource = _defectTable;
            grdInspectionItemSpec.DataSource = _measureTable;
        }

        #endregion

        #region PublicFunction
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

        #endregion

        #region Private Function

        /// <summary>
        /// CAR 요청 저장
        /// </summary>
        private void SaveCarRequest()
        {
            //DataSet ds = new DataSet();
            //ds.Tables.Add(ucIssueRegistration.RequestCarData());
            //ds.Tables.Add(_chemicalDetailTable.Copy());
            //ExecuteRule("SaveCarRequest", ds);

            DataSet ds = new DataSet();
            ds.Tables.Add(ucIssueRegistration.RequestCarData());
            ds.Tables.Add(_changedAffectLot);

            ExecuteRule("SaveCarRequest", ds);
        }
        /// <summary>
        /// CAR 접수 저장
        /// </summary>
        private void SaveCarReceipt()
        {
            DataSet ds = ucIssueRegistration.ReceiptCarData();

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

            ds.Tables.Add(_changedAffectLot);

            ExecuteRule("SaveCarReceipt", ds);
        }

        /// <summary>
        /// CAR 승인 저장
        /// </summary>
        private void SaveCarAccept()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(ucIssueRegistration.AcceptCarData());
            ds.Tables.Add(_changedAffectLot);

            ExecuteRule("SaveCarAccept", ds);
        }

        /// <summary>
        /// 유효성평가 저장
        /// </summary>
        private void SaveCarValidation()
        {
            ucIssueRegistration.ValidationCarData().Tables.Add(_changedAffectLot);
            ExecuteRule("SaveCarValidation", ucIssueRegistration.ValidationCarData());
        }

        /// <summary>
        /// 해당 순번에 임시저장된 데이터가 없으면 Exception
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="tempDt"></param>
        private void CheckTempData(int tapIndex, string sequence, DataTable tempDt)
        {
            if (ucIssueRegistration.IsInputData(tapIndex))
            {
                if (!ucIssueRegistration.CheckIsTempSaved(sequence, tempDt))
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
        /// 외관검사 focusedRow change시 이미지 검색 함수
        /// </summary>
        /// <param name="row"></param>
        private void SearchImage(DataRow row)
        {
            if (row == null) return;

            picDefect.Image = null;
            picDefect2.Image = null;

            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                { "RESOURCETYPE", "SubassemblyInspection" },
                { "RESOURCEID", CurrentDataRow["LOTID"].ToString() + row["INSPITEMID"]},
                { "RESOURCEVERSION", "*"}
            };

            DataTable fileDt = SqlExecuter.Query("GetFileHttpPathFromObjectFileByStandardInfo", "10001", values);
            foreach (DataRow fileRow in fileDt.Rows)
            {
                string filenameAndExt = fileRow.GetString("FILENAME") + "." + fileRow.GetString("FILEEXT");
                if (picDefect.Image == null)
                {
                    //2020-01-28 파일 경로변경
                    picDefect.Image = CommonFunction.GetFtpImageFileToBitmap(fileRow.GetString("FILEPATH"), filenameAndExt);
                }
                else
                {
                    picDefect2.Image = CommonFunction.GetFtpImageFileToBitmap(fileRow.GetString("FILEPATH"), filenameAndExt);
                }

            }
        }
        #endregion

    }
}
