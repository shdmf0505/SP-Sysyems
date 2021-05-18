#region using

using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Micube.Framework;
using Micube.Framework.Net;
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
using Micube.SmartMES.Commons.Controls;
using Micube.SmartMES.QualityAnalysis.KeyboardClipboard;
using System.Drawing.Imaging;
using Micube.SmartMES.Commons;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 신뢰성 검증 > 결과등록(정기외)
    /// 업  무  설  명  : 신뢰성 검증 결과등록(정기외)를 한다.
    /// 생    성    자  : 유호석
    /// 생    성    일  : 2019-08-13
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ReliaVerifiResultNonRegularPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }

        /// <summary>
        /// 그리드에 보여줄 데이터테이블
        /// </summary>
        string _sMode = string.Empty; // New/Modify
        string _sENTERPRISEID = string.Empty;
        string _sPLANTID = string.Empty;
        string _sREQUESTNO = string.Empty;

        DataTable _dtInspectionFile;
        DataTable _dtReliabilityRequest;

        private string[] formats = Enum.GetNames(typeof(ClipboardFormat));
        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public ReliaVerifiResultNonRegularPopup()
        {
            InitializeComponent();
            InitializeEvent();
        }

        public ReliaVerifiResultNonRegularPopup(string sMode, string sEnterprise, string sPLANTID, string sREQUESTNO)
        {
            InitializeComponent();
            InitializeEvent();

            //ClipboardManager KeyboardManager = new ClipboardManager();
            //KeyboardManager.OnChanged += KeyboardManager_OnChanged;
            //KeyboardManager.OnError += KeyboardManager_OnError;
            _sMode = sMode;
            _sENTERPRISEID = sEnterprise;
            _sPLANTID = sPLANTID;
            _sREQUESTNO = sREQUESTNO;
        }

        //private void KeyboardManager_OnError(Exception ex, string message)
        //{
         
        //}

        //private void KeyboardManager_OnChanged(ClipboardFormat format, object data)
        //{
        //    CheckForIllegalCrossThreadCalls = false;
        //    //flowMeasuredPicture.Focus();
        //    string[] files = (string[])data;
        //    //foreach (string file_name in files)
        //    for (int i = files.Length - 1; i >= 0; i--)
        //    {
        //        MessageBox.Show(files[i]);
        //        Bitmap image = new Bitmap(files[i]);
        //        MultiVerificationResultControl vr = new MultiVerificationResultControl(image, files[i]);
        //        flowMeasuredPicture.Controls.Add(vr);
        //    }
        //}

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            //grdQCReliabilityLot
            grdQCReliabilityResultLot.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore;
            //grdQCReliabilityLot.View.SetIsReadOnly();
            grdQCReliabilityResultLot.View.AddTextBoxColumn("REQUESTNO", 100).SetIsReadOnly().SetIsHidden();                //의뢰번호
            grdQCReliabilityResultLot.View.AddTextBoxColumn("ENTERPRISEID", 100).SetIsReadOnly().SetIsHidden();             //회사 ID
            grdQCReliabilityResultLot.View.AddTextBoxColumn("PLANTID", 100).SetIsReadOnly().SetIsHidden();                  //Site ID
            grdQCReliabilityResultLot.View.AddTextBoxColumn("CUSTOMERID").SetTextAlignment(TextAlignment.Center).SetIsReadOnly();                    // 고객사 ID
            grdQCReliabilityResultLot.View.AddTextBoxColumn("CUSTOMERNAME").SetTextAlignment(TextAlignment.Left).SetIsReadOnly(); // 고객사 명                     // 고객사 명
            grdQCReliabilityResultLot.View.AddTextBoxColumn("PRODUCTDEFID", 130).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();             //제품 정의 ID/품목
            grdQCReliabilityResultLot.View.AddTextBoxColumn("PRODUCTDEFNAME", 160).SetIsReadOnly();
            grdQCReliabilityResultLot.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();        //제품 정의 Version
            grdQCReliabilityResultLot.View.AddTextBoxColumn("LOTID", 180).SetIsReadOnly();                    //LOT ID
            grdQCReliabilityResultLot.View.AddTextBoxColumn("WEEK", 60).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdQCReliabilityResultLot.View.AddTextBoxColumn("PROCESSSEGMENTID", 100).SetIsReadOnly().SetIsHidden();         //공정 ID
            grdQCReliabilityResultLot.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 100).SetIsReadOnly().SetIsHidden();    //공정 Version
            grdQCReliabilityResultLot.View.AddTextBoxColumn("AREAID", 100).SetIsReadOnly().SetIsHidden();                   //작업장 ID
            grdQCReliabilityResultLot.View.AddTextBoxColumn("OUTPUTDATE", 100).SetIsReadOnly().SetIsHidden();               //출력일시
            grdQCReliabilityResultLot.View.AddSpinEditColumn("REQUESTQTY", 60).SetIsReadOnly();                           //의뢰수량
            grdQCReliabilityResultLot.View.AddTextBoxColumn("INSPITEMID", 100).SetIsHidden();                                              //검증항목
            grdQCReliabilityResultLot.View.AddTextBoxColumn("INSPITEMNAME", 100).SetIsReadOnly();                                              //검증항목
            grdQCReliabilityResultLot.View.AddComboBoxColumn("INSPECTIONRESULT", 60, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=OKNGNA")).SetTextAlignment(TextAlignment.Center);// 판정결과 공통코드(OKNG)
            //grdQCReliabilityLot.View.AddSpinEditColumn("ISNCRPUBLISH", 100);                                              //NCR 발행여부(Y/N)
            grdQCReliabilityResultLot.View.AddComboBoxColumn("ISNCRPUBLISH", 100, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=YesNo")).SetTextAlignment(TextAlignment.Center);// NCR 발행여부(Y/N) 공통코드(YesNo)
            //DEFECTCODE
            //DEFECTNAME
            DefectCodePopup(); // 불량코드
            grdQCReliabilityResultLot.View.AddTextBoxColumn("DEFECTNAME", 100).SetIsReadOnly();
            grdQCReliabilityResultLot.View.AddTextBoxColumn("QCSEGMENTNAME", 100).SetIsReadOnly();
            grdQCReliabilityResultLot.View.AddTextBoxColumn("QCSEGMENTID", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            //grdQCReliabilityLot.View.AddSpinEditColumn("ISCOMPLETION", 100);                                              //완료여부(Y/N)
            grdQCReliabilityResultLot.View.AddComboBoxColumn("ISCOMPLETION", 60, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=YesNo")).SetTextAlignment(TextAlignment.Center);// 완료여부(Y/N) 공통코드(YesNo)
            grdQCReliabilityResultLot.View.PopulateColumns();

            grdMeasurValue.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore | GridButtonItem.Export;
            grdMeasurValue.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdMeasurValue.View.AddTextBoxColumn("REQUESTNO", 100).SetIsReadOnly().SetIsHidden();                //의뢰번호
            grdMeasurValue.View.AddTextBoxColumn("ENTERPRISEID", 100).SetIsReadOnly().SetIsHidden();             //회사 ID
            grdMeasurValue.View.AddTextBoxColumn("PLANTID", 100).SetIsReadOnly().SetIsHidden();                  //Site ID
            grdMeasurValue.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetIsReadOnly().SetIsHidden();             //제품 정의 ID/품목
            grdMeasurValue.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100).SetTextAlignment(TextAlignment.Right).SetIsReadOnly().SetIsHidden();        //제품 정의 Version
            grdMeasurValue.View.AddTextBoxColumn("LOTID", 200).SetIsReadOnly().SetIsHidden();                    //LOT ID
            grdMeasurValue.View.AddTextBoxColumn("INSPITEMID", 100).SetTextAlignment(TextAlignment.Center).SetIsReadOnly().SetIsHidden();//검사 항목 ID
            grdMeasurValue.View.AddTextBoxColumn("INSPITEMVERSION", 100).SetIsReadOnly().SetIsHidden();     //검사 항목 Version       
            grdMeasurValue.View.AddTextBoxColumn("MEASURETYPE", 100).SetIsReadOnly().SetIsHidden();    //측정구분
            grdMeasurValue.View.AddTextBoxColumn("SEQUENCE", 100).SetIsReadOnly().SetIsHidden();                   //순서
            grdMeasurValue.View.AddTextBoxColumn("TITLE", 200);                                         //제목
            grdMeasurValue.View.AddSpinEditColumn("MEASUREVALUE", 90).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{#,###,##0.####}", MaskTypes.Numeric); // 측정값
            grdMeasurValue.View.AddTextBoxColumn("COMMENTS", 400);                                      //내용
            grdMeasurValue.View.AddTextBoxColumn("UNIT", 100).SetIsReadOnly().SetIsHidden();            //단위

            grdMeasurValue.View.PopulateColumns();

            RepositoryItemTextEdit edit = new RepositoryItemTextEdit();
            edit.Mask.EditMask = "#,###,##0.####";
            edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            edit.Mask.UseMaskAsDisplayFormat = true;

            grdMeasurValue.View.Columns["MEASUREVALUE"].ColumnEdit = edit;

            fpcAttach.grdFileList.View.Columns["FILENAME"].Width = 100;
            fpcAttach.grdFileList.View.Columns["FILEEXT"].Width = 50;
            fpcAttach.grdFileList.View.Columns["FILESIZE"].Width = 60;
            fpcAttach.grdFileList.View.Columns["COMMENTS"].Width = 200;

            fpcReportResult.grdFileList.View.Columns["FILENAME"].Width = 100;
            fpcReportResult.grdFileList.View.Columns["FILEEXT"].Width = 50;
            fpcReportResult.grdFileList.View.Columns["FILESIZE"].Width = 60;
            fpcReportResult.grdFileList.View.Columns["COMMENTS"].Width = 200;
        }
        #endregion                                 
        
        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += ReliaVerifiResultNonRegularPopup_Load;
            grdQCReliabilityResultLot.View.FocusedRowChanged += View_FocusedRowChanged;
            grdQCReliabilityResultLot.View.ShowingEditor += View_CancelChange;
            btnClose.Click += BtnClose_Click;
            //팝업저장버튼을 클릭시 이벤트
            btnSave.Click += BtnSave_Click;
            this.KeyDown += ReliaVerifiResultRegularPopup_KeyDown;
            btnAddMeasurValue.Click += BtnAddMeasurValue_Click;
            btnDeleteMeasurValue.Click += BtnDeleteMeasurValue_Click;
            //grdQCReliabilityLot.View.DoubleClick += grdQCReliabilityLot_DoubleClick;

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
        /// 붙여넣기 호출
        /// </summary>
        private void ReliaVerifiResultRegularPopup_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            //{

            //    try
            //    {
            //        // Get the DataObject.
            //        IDataObject data_object = Clipboard.GetDataObject();

            //        // Look for a file drop.
            //        if (data_object.GetDataPresent(DataFormats.FileDrop))
            //        {
            //            flowMeasuredPicture.Focus();
            //            string[] files = (string[])data_object.GetData(DataFormats.FileDrop);
            //            //foreach (string file_name in files)
            //            for (int i = files.Length - 1; i >= 0; i--)
            //            {
            //                Bitmap image = new Bitmap(files[i]);
            //                MultiVerificationResultControl vr = new MultiVerificationResultControl(image, files[i]);
            //                flowMeasuredPicture.Controls.Add(vr);
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //}
            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                var iData = Clipboard.GetDataObject();

                ClipboardFormat? format = null;

                foreach (var f in formats)
                    if (iData.GetDataPresent(f))
                    {
                        format = (ClipboardFormat)Enum.Parse(typeof(ClipboardFormat), f);
                        break;
                    }

                var data = iData.GetData(format.ToString());

                if (data == null || format == null)
                    return;

                flowMeasuredPicture.Focus();
                if ((ClipboardFormat)format == ClipboardFormat.FileDrop)
                {
                    string[] files = (string[])data;
                    //foreach (string file_name in files)
                    for (int i = files.Length - 1; i >= 0; i--)
                    {
                        Bitmap image = new Bitmap(files[i]);
                        MultiVerificationResultControl vr = new MultiVerificationResultControl(image, files[i]);
                        flowMeasuredPicture.Controls.Add(vr);
                    }
                }
                else if ((ClipboardFormat)format == ClipboardFormat.Bitmap)
                {
                    Bitmap image = (Bitmap)data;
                    string sBitmapName = "ClipboardBitmap-" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".png";
                    MultiVerificationResultControl vr = new MultiVerificationResultControl(image, sBitmapName);
                    flowMeasuredPicture.Controls.Add(vr);
                    image.Save(sBitmapName, ImageFormat.Png);
                }

            }
        }
        #region 그리드이벤트
        /// <summary>        
        /// 의뢰접수 목록 더블클릭 시
        /// </summary>
        //private void grdQCReliabilityLot_DoubleClick(object sender, EventArgs e)
        //{
        //    if (grdQCReliabilityLot.View.FocusedRowHandle < 0) return;

        //    DXMouseEventArgs args = e as DXMouseEventArgs;
        //    GridView view = sender as GridView;
        //    GridHitInfo info = view.CalcHitInfo(args.Location);
        //    DataRow row = this.grdQCReliabilityLot.View.GetFocusedDataRow();

        //    Dictionary<string, object> param = new Dictionary<string, object>();
        //    param.Add("P_ENTERPRISEID", _sENTERPRISEID);
        //    param.Add("P_PLANTID", _sPLANTID);
        //    param.Add("P_REQUESTNO", _sREQUESTNO);
        //    param.Add("P_PRODUCTDEFID", row["PRODUCTDEFID"].ToString());
        //    param.Add("P_LOTID", row["LOTID"].ToString());
        //    DataTable dtReliabilityMeasurValue = SqlExecuter.Query("GetReliabilityMeasureList", "10001", param);
        //    grdMeasurValue.DataSource = dtReliabilityMeasurValue;


        //    Dictionary<string, object> fileValues = new Dictionary<string, object>();
        //    //이미지 파일Search parameter
        //    fileValues.Add("P_FILERESOURCETYPE", "ReliaVerifiResultNonRegularFile");
        //    fileValues.Add("P_FILERESOURCEID", _sREQUESTNO + row["TXNHISTKEY"].ToString() );//CT_QCRELIABILITYLOT.REQUESTNO + SF_INSPECTIONRESULT.TXNHISTKEY
        //    fileValues.Add("P_FILERESOURCEVERSION", "0");

        //    DataTable fileTable = SqlExecuter.Query("SelectFileInspResult", "10001", fileValues);

        //    fpcAttach.DataSource = fileTable;

        //    Dictionary<string, object> fileValues2 = new Dictionary<string, object>();
        //    //이미지 파일Search parameter
        //    fileValues2.Add("P_FILERESOURCETYPE", "ReliaVerifiResultNonRegularReport");
        //    fileValues2.Add("P_FILERESOURCEID", _sREQUESTNO + row["TXNHISTKEY"].ToString());//CT_QCRELIABILITYLOT.REQUESTNO + SF_INSPECTIONRESULT.TXNHISTKEY
        //    fileValues2.Add("P_FILERESOURCEVERSION", "0");

        //    DataTable fileTable2 = SqlExecuter.Query("SelectFileInspResult", "10001", fileValues2);

        //    fpcReport.DataSource = fileTable2;
        //}
        /// <summary>
        /// fpcReport
        /// 저장할 파일의 Key생성에 사용되는 컬럼목록 추가 해줌
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void OnRowChanged(object sender, DataRowChangeEventArgs args)
        {
            if (args.Action == DataRowAction.Add)
            {
                //if (!args.Row.Table.Columns.Contains("REQUESTNO")) args.Row.Table.Columns.Add("REQUESTNO");
                if (!args.Row.Table.Columns.Contains("RESOURCEID")) args.Row.Table.Columns.Add("RESOURCEID");

                DataRow dr = args.Row;
                //dr["REQUESTNO"] = _sREQUESTNO;
                DataRow rowFocused = grdQCReliabilityResultLot.View.GetFocusedDataRow();
                //_sREQUESTNO + row["PRODUCTDEFID"].ToString() + row["PRODUCTDEFVERSION"].ToString() + row["LOTID"].ToString() + row["INSPITEMID"].ToString() + row["INSPITEMVERSION"].ToString());//요청번호,품목,버전,lot,검증item,버전
                dr["RESOURCEID"] = _sREQUESTNO + rowFocused["PRODUCTDEFID"].ToString() + rowFocused["PRODUCTDEFVERSION"].ToString() + rowFocused["LOTID"].ToString() + rowFocused["INSPITEMID"].ToString() + rowFocused["INSPITEMVERSION"].ToString();
            }
        }
        #endregion

        private void BtnAddMeasurValue_Click(object sender, EventArgs e)
        {
            DataTable dtMeasurValue = grdMeasurValue.DataSource as DataTable;
            DataRow newRow = dtMeasurValue.NewRow();
            newRow["PLANTID"] = _sPLANTID;
            newRow["ENTERPRISEID"] = _sENTERPRISEID;
            newRow["REQUESTNO"] = _sREQUESTNO;
            newRow["INSPITEMID"] = "*";
            newRow["INSPITEMVERSION"] = "*";
            newRow["MEASURETYPE"] = "MeasuredValue";

            if (cboUnit.Text.Length == 0)
            {
                ShowMessage("CheckUnitSelection");//단위선택을 해주세요. 
                return;
            }
            newRow["UNIT"] = cboUnit.Text;
            DataRow row = this.grdQCReliabilityResultLot.View.GetFocusedDataRow();
            if (row != null)
            {
                newRow["PRODUCTDEFID"] = row["PRODUCTDEFID"];//제품 정의 ID
                newRow["PRODUCTDEFVERSION"] = row["PRODUCTDEFVERSION"];//제품 정의 Version
                newRow["LOTID"] = row["LOTID"];
                newRow["INSPITEMID"] = row["INSPITEMID"];
                newRow["INSPITEMVERSION"] = row["INSPITEMVERSION"];
                dtMeasurValue.Rows.Add(newRow);
            }
            else
            {
                ShowMessage("CheckLotFocusedDataRow");//제품정보를 선택해주세요.. 
                return;
            }
        }
        /// <summary>
        /// 제품정보 삭제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteMeasurValue_Click(object sender, EventArgs e)
        {
            ValidationCheckFile(grdMeasurValue);

            grdMeasurValue.View.DeleteCheckedRows();
        }
        ///////////////////////////
        /// <summary>
        /// <para>@@네트워크 경로 변경</para>
        /// <para>이미지 파일 to DataTable  변환</para>
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private DataTable GetInspectionFile(int iRowHandle)
        {
            DataRow rFocusedDataRow = null;
            rFocusedDataRow = this.grdQCReliabilityResultLot.View.GetDataRow(iRowHandle);
            //DataRow rFocusedDataRow = grdQCReliabilityResultLot.View.GetDataRow(e.PrevFocusedRowHandle);
            DataTable dtInspectionFile = null;
            //_dtInspectionFile 조회할떄 세팅된다
            dtInspectionFile = _dtInspectionFile.Clone();
            if (!dtInspectionFile.Columns.Contains("LOTID")) dtInspectionFile.Columns.Add("LOTID");
            if (!dtInspectionFile.Columns.Contains("MEASURETYPE")) dtInspectionFile.Columns.Add("MEASURETYPE");
            if (!dtInspectionFile.Columns.Contains("MEASUREVALUE")) dtInspectionFile.Columns.Add("MEASUREVALUE");
            if (!dtInspectionFile.Columns.Contains("TITLE")) dtInspectionFile.Columns.Add("TITLE");
            if (!dtInspectionFile.Columns.Contains("FILEPATH")) dtInspectionFile.Columns.Add("FILEPATH");
            if (!dtInspectionFile.Columns.Contains("LOCALFILEPATH")) dtInspectionFile.Columns.Add("LOCALFILEPATH");
            if (!dtInspectionFile.Columns.Contains("FILEINSPECTIONTYPE")) dtInspectionFile.Columns.Add("FILEINSPECTIONTYPE");
            if (!dtInspectionFile.Columns.Contains("FILERESOURCEID")) dtInspectionFile.Columns.Add("FILERESOURCEID");
            if (!dtInspectionFile.Columns.Contains("FILEFULLPATH")) dtInspectionFile.Columns.Add("FILEFULLPATH");
            if (!dtInspectionFile.Columns.Contains("DBVALUE_FILEID")) dtInspectionFile.Columns.Add("DBVALUE_FILEID");//
            if (!dtInspectionFile.Columns.Contains("SEQUENCE")) dtInspectionFile.Columns.Add("SEQUENCE");
            int iSequence = 1;
            Control.ControlCollection ctr = flowMeasuredPicture.Controls;
            foreach (Control c in ctr)
            {
                if (c.GetType() == typeof(MultiVerificationResultControl))
                {
                    MultiVerificationResultControl vr = c as MultiVerificationResultControl;
                    string strFile = vr.strFile;
                    DataRow dr = dtInspectionFile.NewRow();
                    dr["INSPECTIONTYPE"] = "ReliaVerifiResultNonRegular";
                    dr["RESOURCEID"] = rFocusedDataRow["PRODUCTDEFID"].ToString();//품목
                    dr["PROCESSRELNO"] = rFocusedDataRow["REQUESTNO"].ToString();
                    dr["REQUESTNO"] = rFocusedDataRow["REQUESTNO"].ToString();
                    dr["PLANTID"] = _sPLANTID;
                    dr["ENTERPRISEID"] = _sENTERPRISEID;
                    dr["PRODUCTDEFID"] = rFocusedDataRow["PRODUCTDEFID"].ToString();
                    dr["PRODUCTDEFVERSION"] = rFocusedDataRow["PRODUCTDEFVERSION"].ToString();
                    dr["INSPITEMID"] = rFocusedDataRow["INSPITEMID"].ToString();
                    dr["INSPITEMVERSION"] = rFocusedDataRow["INSPITEMVERSION"].ToString();
                    dr["LOTID"] = rFocusedDataRow["LOTID"].ToString();
                    dr["FILERESOURCEID"] = rFocusedDataRow["PRODUCTDEFID"].ToString() + rFocusedDataRow["PRODUCTDEFVERSION"].ToString() + rFocusedDataRow["LOTID"].ToString() + rFocusedDataRow["INSPITEMID"].ToString() + rFocusedDataRow["INSPITEMVERSION"].ToString();
                    dr["TITLE"] = vr.selectTitle();//CT_QCRELIABILITYMEASURE.TITLE
                    /*
                    if (vr.selectValue().Trim().Length == 0)
                        dr["MEASUREVALUE"] = DBNull.Value;//CT_QCRELIABILITYMEASURE.MEASUREVALUE 
                    else
                        dr["MEASUREVALUE"] = vr.selectValue();
                    */
                    dr["MEASURETYPE"] = "VerificationResult";//CT_QCRELIABILITYMEASURE.MEASURETYPE
                                                             //dr["FILEID"] =서버로직에서 SF_INSPECTIONFILE 신규입력[fileId]를 입력 CT_QCRELIABILITYMEASURE.FILEID

                    // 2020.03.01-유석진-itemid, value 등록하는 로직 추가
                    vr.grdMeasureValue.View.CloseEditor();
                    DataTable vrDt = vr.grdMeasureValue.DataSource as DataTable;

                    for (int i = 0; i < vrDt.Rows.Count; i++)
                    {
                        DataRow vrDr = vrDt.Rows[i];

                        dr["MEASUREITEMID" + (i + 1)] = vrDr["MEASUREITEMID"];
                        if (i > 0)
                            dr["MEASUREVALUE" + (i + 1)] = vrDr["MEASUREVALUE"];
                        else
                            dr["MEASUREVALUE"] = vrDr["MEASUREVALUE"];
                    }

                    FileInfo fileInfo = new FileInfo(strFile);
                    Image image = vr.picMeasurePrinted.Image;

                    // byte 변환 변경-2020-01-29
                    Bitmap newImage = new Bitmap(image);
                    image.Dispose();
                    image = null;
                    byte[] memoryImage = (byte[])new ImageConverter().ConvertTo(newImage, typeof(byte[]));

                    vr.picMeasurePrinted.Image = newImage;
                    vr.picMeasurePrinted.Tag = newImage;

                    dr["FILESIZE"] = Math.Round(Convert.ToDouble(memoryImage.Length) / 1024);
                    dr["FILEDATA"] = memoryImage;
                    dr["FILENAME"] = fileInfo.Name;
                    dr["FILEEXT"] = fileInfo.Extension.Replace(".", "");      //파일업로드시에는 확장자에서 . 을 빼야 한다.
                    dr["FILEPATH"] = "ReliaVerifiResultNonRegular";
                    dr["FILEFULLPATH"] = strFile;
                    dr["FILEINSPECTIONTYPE"] = "ReliaVerifiResultNonRegular";
                    dr["SEQUENCE"] = (iSequence++).ToString();

                    //서버로직에서 SF_INSPECTIONFILE 신규입력[fileId]를 입력
                    //화면에서는 수정되었는지 비교를 위해 참조
                    if (fileInfo.Exists)
                    {
                        //dr["FILEID"] = "FILE-" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                        dr["FILEID"] = "FILE-" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + dr["SEQUENCE"].ToString();
                        dr["DBVALUE_FILEID"] =  "N";//FILEID
                    }
                    else
                    {
                        dr["FILEID"] = vr.Tag;
                        dr["DBVALUE_FILEID"] = "Y";//FILEID
                    }

                    dtInspectionFile.Rows.Add(dr);

                }
            }
            return dtInspectionFile;
        }
        /// <summary>
        /// 이전 로우 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SavePrevFocusedRow(DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            grdQCReliabilityResultLot.View.CloseEditor();
            grdMeasurValue.View.CloseEditor();
            fpcAttach.grdFileList.View.CloseEditor();
            fpcReportResult.grdFileList.View.CloseEditor();
            //DataRow rFocusedDataRow = this.grdQCReliabilityResultLot.View.GetFocusedDataRow();
            DataRow rFocusedDataRow = grdQCReliabilityResultLot.View.GetDataRow(e.PrevFocusedRowHandle);
            //Reportfile DataTable
            DataTable fileChanged = fpcAttach.GetChangedRows();
            //grdQCReliabilityResultLot.View.CheckValidation();

            DataTable dtReliabilityLot = grdQCReliabilityResultLot.GetChangedRows();         //LOT정보 + Inspectionresult
            DataTable dtReliabilityMeasurValue = grdMeasurValue.GetChangedRows();       //계측값
            DataTable dtAttach = fpcAttach.GetChangedRows();                             //첨부파일
            DataTable dtReport = fpcReportResult.GetChangedRows();                            //보고서

            DataTable dtInspectionFile = null;
            //dtInspectionFile = GetInspectionFile(e);
            //@@네트워크 경로 변경
            dtInspectionFile = GetInspectionFile(e.PrevFocusedRowHandle);

            #region dtInspectionFile
            ////_dtInspectionFile 조회할떄 세팅된다
            //dtInspectionFile = _dtInspectionFile.Clone();
            //if (!dtInspectionFile.Columns.Contains("LOTID")) dtInspectionFile.Columns.Add("LOTID");
            //if (!dtInspectionFile.Columns.Contains("MEASURETYPE")) dtInspectionFile.Columns.Add("MEASURETYPE");
            //if (!dtInspectionFile.Columns.Contains("MEASUREVALUE")) dtInspectionFile.Columns.Add("MEASUREVALUE");
            //if (!dtInspectionFile.Columns.Contains("TITLE")) dtInspectionFile.Columns.Add("TITLE");

            //Control.ControlCollection ctr = flowMeasuredPicture.Controls;
            //foreach (Control c in ctr)
            //{
            //    if (c.GetType() == typeof(MultiVerificationResultControl))
            //    {
            //        MultiVerificationResultControl vr = c as MultiVerificationResultControl;
            //        string strFile = vr.strFile;
            //        DataRow dr = dtInspectionFile.NewRow();
            //        dr["INSPECTIONTYPE"] = "ReliaVerifiResultNonRegular";
            //        dr["RESOURCEID"] = rFocusedDataRow["PRODUCTDEFID"].ToString();//품목
            //        dr["PROCESSRELNO"] = rFocusedDataRow["REQUESTNO"].ToString();
            //        dr["REQUESTNO"] = rFocusedDataRow["REQUESTNO"].ToString();
            //        dr["PLANTID"] = _sPLANTID;
            //        dr["ENTERPRISEID"] = _sENTERPRISEID;
            //        dr["PRODUCTDEFID"] = rFocusedDataRow["PRODUCTDEFID"].ToString();
            //        dr["PRODUCTDEFVERSION"] = rFocusedDataRow["PRODUCTDEFVERSION"].ToString();
            //        dr["INSPITEMID"] = rFocusedDataRow["INSPITEMID"].ToString();
            //        dr["INSPITEMVERSION"] = rFocusedDataRow["INSPITEMVERSION"].ToString();
            //        dr["LOTID"] = rFocusedDataRow["LOTID"].ToString();
            //        dr["TITLE"] = vr.selectTitle();//CT_QCRELIABILITYMEASURE.TITLE
            //        if (vr.selectValue().Trim().Length == 0)
            //            dr["MEASUREVALUE"] = DBNull.Value;//CT_QCRELIABILITYMEASURE.MEASUREVALUE 
            //        else
            //            dr["MEASUREVALUE"] = vr.selectValue();
            //        dr["MEASURETYPE"] = "VerificationResult";//CT_QCRELIABILITYMEASURE.MEASURETYPE
            //                                                 //dr["FILEID"] =서버로직에서 SF_INSPECTIONFILE 신규입력[fileId]를 입력 CT_QCRELIABILITYMEASURE.FILEID
            //        Image image = vr.picMeasurePrinted.Image;

            //        byte[] memoryImage = (byte[])new ImageConverter().ConvertTo(image, typeof(byte[]));
            //        dr["FILESIZE"] = Math.Round(Convert.ToDouble(memoryImage.Length) / 1024);
            //        dr["FILEDATA"] = memoryImage;
            //        FileInfo fileInfo = new FileInfo(strFile);
            //        dr["FILENAME"] = fileInfo.Name;
            //        dr["FILEEXT"] = fileInfo.Extension;
            //        dtInspectionFile.Rows.Add(dr);

            //    }
            //} 
            #endregion


            DataTable dtLotInspectionResult = grdQCReliabilityResultLot.DataSource as DataTable;

            //제품정보 그리드에 다수 Row가 존재 하더라도 현재 선택된 Row의 측정값/첨부파일/보고서만 저장 가능하다
            //측정값,첨부파일,보고서 저장시 매핑될 Inspectionresult가 없으면 에러.
            //if (dtReliabilityMeasurValue.Rows.Count > 0 ||
            //    dtAttach.Rows.Count > 0 ||
            //    dtReport.Rows.Count > 0 ||
            //    dtInspectionFile.Rows.Count > 0)

            //{
            //    var changedCols = new List<DataColumn>();

            //    if (rFocusedDataRow["TXNHISTKEY"].ToString().Length > 0)//SF_INSPECTIONRESULT 데이터가 존재 한다
            //    {
            //        changedCols.Add(rFocusedDataRow.Table.Columns["TXNHISTKEY"]);
            //    }
            //    else if (rFocusedDataRow.RowState == DataRowState.Modified)//SF_INSPECTIONRESULT 없는 데이터는  grdQCReliabilityLot.GetChangedRows()가 존재 해야 한다.
            //    {
            //        foreach (DataColumn dc in dtLotInspectionResult.Columns)
            //        {
            //            if (!rFocusedDataRow[dc, DataRowVersion.Original].Equals(
            //                 rFocusedDataRow[dc, DataRowVersion.Current]))
            //            {
            //                changedCols.Add(dc);
            //            }
            //        }
            //    }

            //    if (changedCols.Count == 0)
            //    {
            //        ShowMessage("CheckSaveReliaVerifiResultNonRegular");//검증결과등록전 제품의 판정결과를 등록해야 합니다. 
            //        return;
            //    }
            //} 

            //LOT정보 + Inspectionresult 조합이고, 저장시 Inspectionresult 테이블만 저장한다, RowState를 변경한다
            foreach (DataRow dr in dtReliabilityLot.Rows)
            {
                //if (dr["TXNHISTKEY"].ToString() == string.Empty)
                //    dr["_STATE_"] = "added";
                //else
                //    dr["_STATE_"] = "modified";
                if (!dtReliabilityLot.Columns.Contains("RESOURCEID")) dtReliabilityLot.Columns.Add("RESOURCEID");

                dr["RESOURCEID"] = dr["LOTID"];

                //if ((dr["INSPECTIONRESULT"] == DBNull.Value || dr["INSPECTIONRESULT"].ToString().Length <= 1) && dr["ISCOMPLETION"].ToString() == "Y")
                //{
                //    rFocusedDataRow["ISCOMPLETION"] = string.Empty;
                //    ShowMessage("CheckSaveReliaVerifiResultNonRegular");//검증결과등록전 제품의 판정결과를 등록해야 합니다.
                //    return;
                //}

                if (dr["ISNCRPUBLISH"].ToString() == "Y" && dr["ISCOMPLETION"].ToString() != "Y")
                {
                    ShowMessage("CheckIsNcrPublishReliaVerifiResultNonRegular");//이상발생이면 검증결과가 완료되어야 합니다.
                    grdQCReliabilityResultLot.View.FocusedRowChanged -= View_FocusedRowChanged;
                    grdQCReliabilityResultLot.View.FocusedRowHandle = e.PrevFocusedRowHandle;
                    grdQCReliabilityResultLot.View.FocusedRowChanged += View_FocusedRowChanged;
                    return;
                }

                if (dr["INSPECTIONRESULT"].ToString() == "NG" && dr["DEFECTCODE"].ToString().Trim().Length == 0)
                {
                    ShowMessage("CheckInspectionResultReliaVerifiResultNonRegular");//판정결과 NG이면 불량코드가 선택되어야 합니다 . 
                    grdQCReliabilityResultLot.View.FocusedRowChanged -= View_FocusedRowChanged;
                    grdQCReliabilityResultLot.View.FocusedRowHandle = e.PrevFocusedRowHandle;
                    grdQCReliabilityResultLot.View.FocusedRowChanged += View_FocusedRowChanged;
                    return;
                }

                if (dr["ISNCRPUBLISH"].ToString() == "Y")
                {
                    //이상발생 사유코드 등록
                    if (!dtReliabilityLot.Columns.Contains("REASONCODEID")) dtReliabilityLot.Columns.Add("REASONCODEID");

                    //2020-02-18 강유라 오타 수정
                    //dr["REASONCODEID"] = "LockReliablOutRegNonconform";
                    dr["REASONCODEID"] = "LockReliablOutRegNonconfirm";
                }
            }

            //if (dtReliabilityLot.Rows.Count > 0
            //    || dtReliabilityMeasurValue.Rows.Count > 0
            //    || dtAttach.Rows.Count > 0
            //    || dtReport.Rows.Count > 0
            //    || dtInspectionFile.Rows.Count > 0
            //    || (dtInspectionFile.Rows.Count != _dtInspectionFile.Rows.Count))//삭제 해서 dtInspectionFile.Rows.Count == 0 일경우 
            //{
            //    result = this.ShowMessage(MessageBoxButtons.YesNo, "InfoPopupSave");//변경 내용을 저장하시겠습니까??
            //}

            //if (result == System.Windows.Forms.DialogResult.No) return;

            try
            {
                this.ShowWaitArea();
                btnSave.Enabled = false;
                btnClose.Enabled = false;

                dtReliabilityLot.TableName = "inspectionresultList";                //LOT정보 + Inspectionresult
                dtReliabilityMeasurValue.TableName = "measuredValueList";           //계측값 입력
                dtAttach.TableName = "attachFileList";                              //첨부파일
                dtReport.TableName = "reportFileList";                              //리포트
                if (dtInspectionFile != null) dtInspectionFile.TableName = "inspectionFile";                      //검증결과 이미지

                DataTable dtFocusedDataRow = dtLotInspectionResult.Clone();       //기본 정보용(그리드에 바인딩된 Data)
                DataRow dr = dtFocusedDataRow.NewRow();
                dr["REQUESTNO"] = rFocusedDataRow["REQUESTNO"].ToString();
                dr["ENTERPRISEID"] = rFocusedDataRow["ENTERPRISEID"].ToString();
                dr["PLANTID"] = rFocusedDataRow["PLANTID"].ToString();
                dr["PRODUCTDEFID"] = rFocusedDataRow["PRODUCTDEFID"].ToString();
                dr["PRODUCTDEFVERSION"] = rFocusedDataRow["PRODUCTDEFVERSION"].ToString();
                dr["LOTID"] = rFocusedDataRow["LOTID"].ToString();
                //dr["TXNHISTKEY"] = rFocusedDataRow["TXNHISTKEY"].ToString();//SF_INSPECTIONRESULT.txnhistkey
                dr["INSPITEMID"] = rFocusedDataRow["INSPITEMID"].ToString();
                dr["INSPITEMVERSION"] = rFocusedDataRow["INSPITEMVERSION"].ToString();

                dtFocusedDataRow.Rows.Add(dr);
                dtFocusedDataRow.TableName = "focusedDataRow";

                _dtReliabilityRequest.Rows[0]["COMMENTS"] = txtCOMMENTS.Text.ToString().Length > 2500 ? txtCOMMENTS.Text.Substring(0, 2500) : txtCOMMENTS.Text;
                _dtReliabilityRequest.Rows[0]["INSPECTOR"] = popInspector.Text;
                DataTable dtReliabilityRequest = _dtReliabilityRequest.Copy();
                dtReliabilityRequest.TableName = "reliabilityRequest";

                //DataSet rullSet = new DataSet();
                //rullSet.Tables.Add(dtReliabilityLot);
                //rullSet.Tables.Add(dtReliabilityMeasurValue);
                //rullSet.Tables.Add(dtAttach);
                //rullSet.Tables.Add(dtReport);
                //if (dtInspectionFile != null) rullSet.Tables.Add(dtInspectionFile);
                //rullSet.Tables.Add(dtFocusedDataRow);
                //rullSet.Tables.Add(dtReliabilityRequest);
                //ExecuteRule("SaveReliaVerifiResultNonRegular", rullSet);

                //ShowMessage("SuccessSave");

                MessageWorker messageWorker = new MessageWorker("SaveReliaVerifiResultNonRegular");

                if (dtInspectionFile != null)
                {
                    messageWorker.SetBody(new MessageBody()
                    {
                        { "inspectionresultList", dtReliabilityLot },
                        { "measuredValueList", dtReliabilityMeasurValue },
                        { "attachFileList", dtAttach },
                        { "reportFileList", dtReport },
                        { "inspectionFile", dtInspectionFile },
                        { "focusedDataRow", dtFocusedDataRow},
                        { "reliabilityRequest", dtReliabilityRequest}
                    });
                }
                else
                {
                    messageWorker.SetBody(new MessageBody()
                    {
                        { "inspectionresultList", dtReliabilityLot },
                        { "measuredValueList", dtReliabilityMeasurValue },
                        { "attachFileList", dtAttach },
                        { "reportFileList", dtReport },
                        { "inspectionFile", dtInspectionFile },
                        { "reliabilityRequest", dtReliabilityRequest}
                    });
                }

                DataTable isSendEmailRS = messageWorker.Execute<DataTable>().GetResultSet();
                //@@네트워크 경로 변경----------------------------------------------------------------------
                //파일업로드 팝업 데이터 전달
                DataTable fileUploadTable = GetImageFileTable();
                int totalFileSize = 0;
                foreach (DataRow originRow in dtInspectionFile.Rows)
                {
                    if (!originRow.IsNull("FILENAME") && originRow["DBVALUE_FILEID"].ToString() == "N")
                    {
                        DataRow newRow = fileUploadTable.NewRow();
                        newRow["FILEID"] = originRow["FILEID"];         //Server에서 FileID를 생성하여서 가져와야 한다.
                        newRow["FILENAME"] = originRow["FILEID"];
                        newRow["FILEEXT"] = originRow.GetString("FILEEXT");      //파일업로드시에는 확장자에서 . 을 빼야 한다.
                        newRow["FILEPATH"] = originRow["FILEPATH"];// originRow["FILEFULLPATH1"];
                        newRow["SAFEFILENAME"] = originRow["FILENAME"];
                        newRow["FILESIZE"] = originRow["FILESIZE"];
                        newRow["SEQUENCE"] = originRow["SEQUENCE"];
                        newRow["LOCALFILEPATH"] = originRow["FILEFULLPATH"];
                        newRow["RESOURCETYPE"] = originRow["FILEINSPECTIONTYPE"];
                        newRow["RESOURCEID"] = originRow["FILERESOURCEID"]; //productDefID + productDefVersion + lotID + Inspitemid + Inspitemversion;
                        newRow["RESOURCEVERSION"] = "*";
                        newRow["PROCESSINGSTATUS"] = "Wait";

                        //YJKIM : 서버에서 데이타베이스를 입력하기 위해 파일아이디를 전달해야 한다.
                        totalFileSize += originRow.GetInteger("FILESIZE");

                        fileUploadTable.Rows.Add(newRow);   
                    }
                }

                if (fileUploadTable.Rows.Count > 0)
                {
                    FileProgressDialog fileProgressDialog = new Micube.SmartMES.Commons.Controls.FileProgressDialog(fileUploadTable, UpDownType.Upload, "", totalFileSize);
                    fileProgressDialog.ShowDialog(this);

                    if (fileProgressDialog.DialogResult == DialogResult.Cancel)
                        throw MessageException.Create("CancelMessageToUploadFile");  //파일업로드를 취소하였습니다.

                    ProgressingResult fileResult = fileProgressDialog.Result;

                    if (!fileResult.IsSuccess)
                        throw MessageException.Create("FailMessageToUploadFile"); //파일업로드를 실패하였습니다.
                }
                //임시저장 삭제
                var lClipboardBitmap = dtInspectionFile.AsEnumerable().Where(r => r["FILEFULLPATH"].ToString().StartsWith("ClipboardBitmap-") == true).ToList();
                if (lClipboardBitmap != null && lClipboardBitmap.Count > 0)
                {
                    foreach (DataRow drClipboardBitmap in lClipboardBitmap)
                    {
                        File.Delete(drClipboardBitmap["FILEFULLPATH"].ToString());//@"C:\Temp\Data\Authors.txt"
                    }

                }
                //------------------------------------------------------------------------------------------------------
                if (isSendEmailRS != null && isSendEmailRS.Rows.Count > 0)
                {
                    DataRow drLot = dtReliabilityLot.Rows[0];
                    DataTable toSendDt = CommonFunction.CreateReliabilityNonRegularEmailDt();

                    DataRow newRow = toSendDt.NewRow();
                    newRow["PRODUCTDEFNAME"] = drLot["PRODUCTDEFNAME"];
                    newRow["PRODUCTDEFID"] = drLot["PRODUCTDEFID"];
                    newRow["PRODUCTDEFVERSION"] = drLot["PRODUCTDEFVERSION"];
                    newRow["LOTID"] = drLot["LOTID"];
                    newRow["PROCESSSEGMENTID"] = drLot["PROCESSSEGMENTID"];
                    newRow["PROCESSSEGMENTNAME"] = drLot["PROCESSSEGMENTNAME"];
                    newRow["AREAID"] = drLot["AREAID"]; 
                    newRow["AREANAME"] = drLot["AREANAME"];
                    newRow["INSPECTIONRESULT"] = drLot["INSPECTIONRESULT"];
                    newRow["DEFECTNAME"] = drLot["DEFECTNAME"];
                    newRow["REMARK"] = "";
                    newRow["USERID"] = UserInfo.Current.Id;
                    newRow["TITLE"] = Language.Get("RELIABILITYNONABNORMALTITLE");
                    newRow["INSPECTION"] = "ReliabilityNonRegular";
                    newRow["LANGUAGETYPE"] = UserInfo.Current.LanguageType;

                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("LOTID", drLot["LOTID"].ToString());
                    param.Add("PROCESSSEGMENTID", drLot["PROCESSSEGMENTID"].ToString());
                    param.Add("PROCESSSEGMENTVERSION", drLot["PROCESSSEGMENTVERSION"].ToString());
                    param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    DataTable dtEquipmentList = SqlExecuter.Query("GetEquipmentListByLotAndProcessSegmentId", "10001", param);

                    if (dtEquipmentList != null && dtEquipmentList.Rows.Count > 0)
                    {
                        newRow["EQUIPMENTLIST"] = dtEquipmentList.Rows[0]["EQUIPMENTLIST"].ToString();
                    }

                    toSendDt.Rows.Add(newRow);

                    CommonFunction.ShowSendEmailPopupDataTable(toSendDt);
                }

                ShowMessage("SuccessSave");
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
                //저장후 팝업을 닫지 않는다
                //_dtInspectionFile = null;
                //if (dtReliabilityLot != null && dtReliabilityLot.Rows.Count > 0) (grdQCReliabilityResultLot.DataSource as DataTable).AcceptChanges();
                //this.DialogResult = DialogResult.OK;
                //this.Close();

                rFocusedDataRow["ISCOMPLETION_ORG"] = rFocusedDataRow["ISCOMPLETION"];
                DataTable dtQCReliabilityResultLot = grdQCReliabilityResultLot.DataSource as DataTable;
                dtQCReliabilityResultLot.AcceptChanges();
                if (dtQCReliabilityResultLot.Rows.Count > 0 && dtQCReliabilityResultLot.AsEnumerable().Where(r => r["ISCOMPLETION"].ToString() != "Y").ToList().Count == 0)
                {
                    //모두 완료 이므로 저장후 팝업을 닫는다
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    //저장후 팝업을 닫지 않는다
                    //Search();
                    focusedRowChanged();
                }
            }
        }


        /////////////////////////////

        /// 
        /// <summary>
        /// 저장버튼을 클릭했을때 검사 결과를 저장하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            DialogResult result = System.Windows.Forms.DialogResult.No;
            grdQCReliabilityResultLot.View.CloseEditor();
            grdMeasurValue.View.CloseEditor();
            fpcAttach.grdFileList.View.CloseEditor();
            fpcReportResult.grdFileList.View.CloseEditor();
            DataRow rFocusedDataRow = this.grdQCReliabilityResultLot.View.GetFocusedDataRow();
            //Reportfile DataTable
            DataTable fileChanged = fpcAttach.GetChangedRows();
            grdQCReliabilityResultLot.View.CheckValidation();

            DataTable dtReliabilityLot = grdQCReliabilityResultLot.GetChangedRows();         //LOT정보 + Inspectionresult
            DataTable dtReliabilityMeasurValue = grdMeasurValue.GetChangedRows();       //계측값
            DataTable dtAttach = fpcAttach.GetChangedRows();                             //첨부파일
            DataTable dtReport = fpcReportResult.GetChangedRows();                            //보고서

            //if (dtReliabilityMeasurValue.Rows.Count > 0)
            //{
            //    if (!dtReliabilityMeasurValue.Columns.Contains("TXNHISTKEY")) dtReliabilityMeasurValue.Columns.Add("TXNHISTKEY");
            //    foreach (DataRow dr in dtReliabilityMeasurValue.Rows)
            //    {
            //        dr["TXNHISTKEY"] = rFocusedDataRow["TXNHISTKEY"].ToString();//SF_INSPECTIONRESULT.txnhistkey
            //    }
            //}

            //if (dtAttach.Rows.Count > 0)
            //{
            //    if (!dtAttach.Columns.Contains("TXNHISTKEY")) dtAttach.Columns.Add("TXNHISTKEY");
            //    foreach (DataRow dr in dtAttach.Rows)
            //    {
            //        dr["TXNHISTKEY"] = rFocusedDataRow["TXNHISTKEY"].ToString();//SF_INSPECTIONRESULT.txnhistkey
            //    }
            //}

            //if (dtReport.Rows.Count > 0)
            //{
            //    if (!dtReport.Columns.Contains("TXNHISTKEY")) dtReport.Columns.Add("TXNHISTKEY");
            //    foreach (DataRow dr in dtReport.Rows)
            //    {
            //        dr["TXNHISTKEY"] = rFocusedDataRow["TXNHISTKEY"].ToString();//SF_INSPECTIONRESULT.txnhistkey
            //    }
            //}
            DataTable dtInspectionFile = null;
            //dtInspectionFile = _dtInspectionFile.Clone();
            //@@네트워크 경로 변경
            dtInspectionFile = GetInspectionFile(this.grdQCReliabilityResultLot.View.FocusedRowHandle);
            DataTable dtLotInspectionResult = grdQCReliabilityResultLot.DataSource as DataTable;

            //제품정보 그리드에 다수 Row가 존재 하더라도 현재 선택된 Row의 측정값/첨부파일/보고서만 저장 가능하다
            //측정값,첨부파일,보고서 저장시 매핑될 Inspectionresult가 없으면 에러.
            //if (dtReliabilityMeasurValue.Rows.Count > 0 ||
            //    dtAttach.Rows.Count > 0 ||
            //    dtReport.Rows.Count > 0 ||
            //    dtInspectionFile.Rows.Count > 0)
                
            //{
            //    var changedCols = new List<DataColumn>();

            //    if (rFocusedDataRow["TXNHISTKEY"].ToString().Length > 0)//SF_INSPECTIONRESULT 데이터가 존재 한다
            //    {
            //        changedCols.Add(rFocusedDataRow.Table.Columns["TXNHISTKEY"]);
            //    }
            //    else if (rFocusedDataRow.RowState == DataRowState.Modified)//SF_INSPECTIONRESULT 없는 데이터는  grdQCReliabilityLot.GetChangedRows()가 존재 해야 한다.
            //    {
            //        foreach (DataColumn dc in dtLotInspectionResult.Columns)
            //        {
            //            if (!rFocusedDataRow[dc, DataRowVersion.Original].Equals(
            //                 rFocusedDataRow[dc, DataRowVersion.Current]))
            //            {
            //                changedCols.Add(dc);
            //            }
            //        }
            //    }

            //    if (changedCols.Count == 0)
            //    {
            //        ShowMessage("CheckSaveReliaVerifiResultNonRegular");//검증결과등록전 제품의 판정결과를 등록해야 합니다. 
            //        return;
            //    }
            //} 
            
            //LOT정보 + Inspectionresult 조합이고, 저장시 Inspectionresult 테이블만 저장한다, RowState를 변경한다
            foreach (DataRow dr in dtReliabilityLot.Rows)
            {
                //if (dr["TXNHISTKEY"].ToString() == string.Empty)
                //    dr["_STATE_"] = "added";
                //else
                //    dr["_STATE_"] = "modified";
                if (!dtReliabilityLot.Columns.Contains("RESOURCEID")) dtReliabilityLot.Columns.Add("RESOURCEID");

                dr["RESOURCEID"] = dr["LOTID"];

                //if ((dr["INSPECTIONRESULT"] == DBNull.Value || dr["INSPECTIONRESULT"].ToString().Length <= 1) && dr["ISCOMPLETION"].ToString() == "Y")
                //{
                //    rFocusedDataRow["ISCOMPLETION"] = string.Empty;
                //    ShowMessage("CheckSaveReliaVerifiResultNonRegular");//검증결과등록전 제품의 판정결과를 등록해야 합니다.
                //    return;
                //}

                if (dr["ISNCRPUBLISH"].ToString() == "Y" && dr["ISCOMPLETION"].ToString() != "Y")
                {
                    ShowMessage("CheckIsNcrPublishReliaVerifiResultNonRegular");//이상발생이면 검증결과가 완료되어야 합니다. 
                    return;
                }

                if (dr["INSPECTIONRESULT"].ToString() == "NG" && dr["DEFECTCODE"].ToString().Trim().Length == 0)
                {
                    ShowMessage("CheckInspectionResultReliaVerifiResultNonRegular");//판정결과 NG이면 불량코드가 선택되어야 합니다 . 
                    return;
                }

                if (dr["ISNCRPUBLISH"].ToString() == "Y")
                {
                    //이상발생 사유코드 등록
                    if (!dtReliabilityLot.Columns.Contains("REASONCODEID")) dtReliabilityLot.Columns.Add("REASONCODEID");

                    //2020-02-18 강유라 오타 수정
                    //dr["REASONCODEID"] = "LockReliablOutRegNonconform";
                    dr["REASONCODEID"] = "LockReliablOutRegNonconfirm";
                }
            }

            //if (dtReliabilityLot.Rows.Count > 0
            //    || dtReliabilityMeasurValue.Rows.Count > 0
            //    || dtAttach.Rows.Count > 0
            //    || dtReport.Rows.Count > 0
            //    || dtInspectionFile.Rows.Count > 0
            //    || (dtInspectionFile.Rows.Count != _dtInspectionFile.Rows.Count))//삭제 해서 dtInspectionFile.Rows.Count == 0 일경우 
            //{
            //    result = this.ShowMessage(MessageBoxButtons.YesNo, "InfoPopupSave");//변경 내용을 저장하시겠습니까??
            //}

            //if (result == System.Windows.Forms.DialogResult.No) return;

            try
            {
                this.ShowWaitArea();
                btnSave.Enabled = false;
                btnClose.Enabled = false;

                dtReliabilityLot.TableName = "inspectionresultList";                //LOT정보 + Inspectionresult
                dtReliabilityMeasurValue.TableName = "measuredValueList";           //계측값 입력
                dtAttach.TableName = "attachFileList";                              //첨부파일
                dtReport.TableName = "reportFileList";                              //리포트
                if (dtInspectionFile != null) dtInspectionFile.TableName = "inspectionFile";                      //검증결과 이미지

                DataTable dtFocusedDataRow = dtLotInspectionResult.Clone();       //기본 정보용(그리드에 바인딩된 Data)
                DataRow dr = dtFocusedDataRow.NewRow();
                dr["REQUESTNO"] = rFocusedDataRow["REQUESTNO"].ToString();
                dr["ENTERPRISEID"] = rFocusedDataRow["ENTERPRISEID"].ToString();
                dr["PLANTID"] = rFocusedDataRow["PLANTID"].ToString();
                dr["PRODUCTDEFID"] = rFocusedDataRow["PRODUCTDEFID"].ToString();
                dr["PRODUCTDEFVERSION"] = rFocusedDataRow["PRODUCTDEFVERSION"].ToString();
                dr["LOTID"] = rFocusedDataRow["LOTID"].ToString();
                //dr["TXNHISTKEY"] = rFocusedDataRow["TXNHISTKEY"].ToString();//SF_INSPECTIONRESULT.txnhistkey
                dr["INSPITEMID"] = rFocusedDataRow["INSPITEMID"].ToString();
                dr["INSPITEMVERSION"] = rFocusedDataRow["INSPITEMVERSION"].ToString();

                dtFocusedDataRow.Rows.Add(dr);
                dtFocusedDataRow.TableName = "focusedDataRow";

                _dtReliabilityRequest.Rows[0]["COMMENTS"] = txtCOMMENTS.Text.ToString().Length > 2500 ? txtCOMMENTS.Text.Substring(0, 2500) : txtCOMMENTS.Text;
                _dtReliabilityRequest.Rows[0]["INSPECTOR"] = popInspector.Text;
                DataTable dtReliabilityRequest = _dtReliabilityRequest.Copy();
                dtReliabilityRequest.TableName = "reliabilityRequest";

                //DataSet rullSet = new DataSet();
                //rullSet.Tables.Add(dtReliabilityLot);
                //rullSet.Tables.Add(dtReliabilityMeasurValue);
                //rullSet.Tables.Add(dtAttach);
                //rullSet.Tables.Add(dtReport);
                //if(dtInspectionFile != null) rullSet.Tables.Add(dtInspectionFile);
                //rullSet.Tables.Add(dtFocusedDataRow);
                //rullSet.Tables.Add(dtReliabilityRequest);
                //ExecuteRule("SaveReliaVerifiResultNonRegular", rullSet);


                //"○ 품목명(품목코드/REV) :
                //○ LOT No : 
                //○ 표준공정:CT_QCRELIABILITYLOT.processsegmentid
                //○ 작업장:CT_QCRELIABILITYLOT.areaid
                //○ 설비호기:GetEquipmentListByLotAndProcessSegmentId
                //○ 판정결과11:
                //○ 불량명11:

                MessageWorker messageWorker = new MessageWorker("SaveReliaVerifiResultNonRegular");

                if (dtInspectionFile != null)
                {
                    messageWorker.SetBody(new MessageBody()
                    {
                        { "inspectionresultList", dtReliabilityLot },
                        { "measuredValueList", dtReliabilityMeasurValue },
                        { "attachFileList", dtAttach },
                        { "reportFileList", dtReport },
                        { "inspectionFile", dtInspectionFile },
                        { "focusedDataRow", dtFocusedDataRow},
                        { "reliabilityRequest", dtReliabilityRequest}
                    });
                }
                else
                {
                    messageWorker.SetBody(new MessageBody()
                    {
                        { "inspectionresultList", dtReliabilityLot },
                        { "measuredValueList", dtReliabilityMeasurValue },
                        { "attachFileList", dtAttach },
                        { "reportFileList", dtReport },
                        { "inspectionFile", dtInspectionFile },
                        { "reliabilityRequest", dtReliabilityRequest}
                    });
                }

                DataTable isSendEmailRS = messageWorker.Execute<DataTable>().GetResultSet();

                //@@네트워크 경로 변경----------------------------------------------------------------------
                //파일업로드 팝업 데이터 전달
                DataTable fileUploadTable = GetImageFileTable();
                int totalFileSize = 0;
                foreach (DataRow originRow in dtInspectionFile.Rows)
                {
                    if (!originRow.IsNull("FILENAME") && originRow["DBVALUE_FILEID"].ToString() == "N")
                    {
                        DataRow newRow = fileUploadTable.NewRow();
                        newRow["FILEID"] = originRow["FILEID"];         //Server에서 FileID를 생성하여서 가져와야 한다.
                        newRow["FILENAME"] = originRow["FILEID"];
                        newRow["FILEEXT"] = originRow.GetString("FILEEXT");      //파일업로드시에는 확장자에서 . 을 빼야 한다.
                        newRow["FILEPATH"] = originRow["FILEPATH"];// originRow["FILEFULLPATH1"];
                        newRow["SAFEFILENAME"] = originRow["FILENAME"];
                        newRow["FILESIZE"] = originRow["FILESIZE"];
                        newRow["SEQUENCE"] = originRow["SEQUENCE"]; 
                        newRow["LOCALFILEPATH"] = originRow["FILEFULLPATH"];
                        newRow["RESOURCETYPE"] = originRow["FILEINSPECTIONTYPE"];
                        newRow["RESOURCEID"] = originRow["FILERESOURCEID"]; //productDefID + productDefVersion + lotID + Inspitemid + Inspitemversion;
                        newRow["RESOURCEVERSION"] = "*";
                        newRow["PROCESSINGSTATUS"] = "Wait";


                        //YJKIM : 서버에서 데이타베이스를 입력하기 위해 파일아이디를 전달해야 한다.
                        totalFileSize += originRow.GetInteger("FILESIZE");

                        fileUploadTable.Rows.Add(newRow);
                    }
                }

                if (fileUploadTable.Rows.Count > 0)
                {
                    FileProgressDialog fileProgressDialog = new Micube.SmartMES.Commons.Controls.FileProgressDialog(fileUploadTable, UpDownType.Upload, "", totalFileSize);
                    fileProgressDialog.ShowDialog(this);

                    if (fileProgressDialog.DialogResult == DialogResult.Cancel)
                        throw MessageException.Create("CancelMessageToUploadFile");  //파일업로드를 취소하였습니다.

                    ProgressingResult fileResult = fileProgressDialog.Result;

                    if (!fileResult.IsSuccess)
                        throw MessageException.Create("FailMessageToUploadFile"); //파일업로드를 실패하였습니다.
                }
                //임시저장 삭제
                var lClipboardBitmap = dtInspectionFile.AsEnumerable().Where(r => r["FILEFULLPATH"].ToString().StartsWith("ClipboardBitmap-") == true).ToList();
                if (lClipboardBitmap != null && lClipboardBitmap.Count > 0)
                {
                    foreach (DataRow drClipboardBitmap in lClipboardBitmap)
                    {
                        File.Delete(drClipboardBitmap["FILEFULLPATH"].ToString());//@"C:\Temp\Data\Authors.txt"
                    }
                    
                }
                //-------------------------------------------------------------------------------------------------------
                if (isSendEmailRS != null && isSendEmailRS.Rows.Count > 0)
                {
                    //DataRow drLot = dtReliabilityLot.Rows[0];
                    //StringBuilder _sbMailContents = new StringBuilder();
                    //_sbMailContents.Append(string.Format("○ {0}({1}/{2}):", Language.Get("PRODUCTDEFNAME"), Language.Get("PRODUCTDEFID"), Language.Get("PRODUCTDEFVERSION")));
                    //_sbMailContents.AppendLine(string.Format("{0}({1}/{2})",drLot["PRODUCTDEFID"].ToString(), drLot["PRODUCTDEFNAME"].ToString(), drLot["PRODUCTDEFVERSION"].ToString()));
                    //_sbMailContents.Append(string.Format("○ {0}:", Language.Get("LOTID")));
                    //_sbMailContents.AppendLine(string.Format("{0}",drLot["LOTID"].ToString()));

                    //_sbMailContents.Append(string.Format("○ {0}:", Language.Get("STANDARDSEGMENT")));//표준공정
                    //_sbMailContents.AppendLine(string.Format("{0}", drLot["PROCESSSEGMENTID"].ToString()));
                    //_sbMailContents.Append(string.Format("○ {0}:", Language.Get("AREA")));//작업장
                    //if (drLot["AREANAME_DESCRIPTION"].ToString().Length > 0)
                    //    _sbMailContents.AppendLine(string.Format("{0}({1})", drLot["AREANAME"].ToString(), drLot["AREANAME_DESCRIPTION"].ToString()));
                    //else
                    //    _sbMailContents.AppendLine(string.Format("{0}", drLot["AREANAME"].ToString()));
                    //Dictionary<string, object> param = new Dictionary<string, object>();
                    //param.Add("LOTID", drLot["LOTID"].ToString());
                    //param.Add("PROCESSSEGMENTID", drLot["PROCESSSEGMENTID"].ToString());
                    //param.Add("PROCESSSEGMENTVERSION", drLot["PROCESSSEGMENTVERSION"].ToString());
                    //param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    //DataTable dtEquipmentList = SqlExecuter.Query("GetEquipmentListByLotAndProcessSegmentId", "10001", param);

                    //if (dtEquipmentList != null && dtEquipmentList.Rows.Count > 0)
                    //{
                    //    _sbMailContents.Append(string.Format("○ {0}:", Language.Get("EQUIPMENTUNIT")));//설비호기
                    //    _sbMailContents.AppendLine(string.Format("{0}", dtEquipmentList.Rows[0]["EQUIPMENTLIST"].ToString()));
                    //}
                    //_sbMailContents.Append(string.Format("○ {0}:", Language.Get("INSPECTIONRESULT")));//판정결과
                    //_sbMailContents.AppendLine(string.Format("{0}", drLot["INSPECTIONRESULT"].ToString()));
                    //_sbMailContents.Append(string.Format("○ {0}:", Language.Get("DEFECTNAME")));//불량명
                    //_sbMailContents.AppendLine(string.Format("{0}", drLot["DEFECTNAME"].ToString()));

                    //Micube.SmartMES.Commons.CommonFunction.ShowSendEmailPopup(Language.Get("NONCONFORMITYTITLE"), _sbMailContents.ToString());

                    DataRow drLot = dtReliabilityLot.Rows[0];
                    DataTable toSendDt = CommonFunction.CreateReliabilityNonRegularEmailDt();

                    DataRow newRow = toSendDt.NewRow();
                    newRow["PRODUCTDEFNAME"] = drLot["PRODUCTDEFNAME"];
                    newRow["PRODUCTDEFID"] = drLot["PRODUCTDEFID"];
                    newRow["PRODUCTDEFVERSION"] = drLot["PRODUCTDEFVERSION"];
                    newRow["LOTID"] = drLot["LOTID"];
                    newRow["PROCESSSEGMENTID"] = drLot["PROCESSSEGMENTID"];
                    newRow["PROCESSSEGMENTNAME"] = drLot["PROCESSSEGMENTNAME"];
                    newRow["AREAID"] = drLot["AREAID"];
                    newRow["AREANAME"] = drLot["AREANAME"];
                    newRow["INSPECTIONRESULT"] = drLot["INSPECTIONRESULT"];
                    newRow["DEFECTNAME"] = drLot["DEFECTNAME"];
                    newRow["REMARK"] = "";
                    newRow["USERID"] = UserInfo.Current.Id;
                    newRow["TITLE"] = Language.Get("RELIABILITYNONABNORMALTITLE");
                    newRow["INSPECTION"] = "ReliabilityNonRegular";
                    newRow["LANGUAGETYPE"] = UserInfo.Current.LanguageType;

                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("LOTID", drLot["LOTID"].ToString());
                    param.Add("PROCESSSEGMENTID", drLot["PROCESSSEGMENTID"].ToString());
                    param.Add("PROCESSSEGMENTVERSION", drLot["PROCESSSEGMENTVERSION"].ToString());
                    param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    DataTable dtEquipmentList = SqlExecuter.Query("GetEquipmentListByLotAndProcessSegmentId", "10001", param);

                    if (dtEquipmentList != null && dtEquipmentList.Rows.Count > 0)
                    {
                        newRow["EQUIPMENTLIST"] = dtEquipmentList.Rows[0]["EQUIPMENTLIST"].ToString();
                    }

                    toSendDt.Rows.Add(newRow);

                    CommonFunction.ShowSendEmailPopupDataTable(toSendDt);
                }

                ShowMessage("SuccessSave");

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
                //저장후 팝업을 닫지 않는다
                //_dtInspectionFile = null;
                
                //if (dtReliabilityLot != null && dtReliabilityLot.Rows.Count > 0) (grdQCReliabilityResultLot.DataSource as DataTable).AcceptChanges();
                Search();
                focusedRowChanged();
                //this.DialogResult = DialogResult.OK;
                //this.Close();
            }
        }
        /// <summary>
        /// 그리드의 Row Click시 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.PrevFocusedRowHandle < 0)// 초기 폼로딩
            {
                focusedRowChanged();
            }
            else
            {
                DataTable dtQCReliabilityResultLot = grdQCReliabilityResultLot.DataSource as DataTable;
                if (dtQCReliabilityResultLot.Rows.Count > 0 && dtQCReliabilityResultLot.AsEnumerable().Where(r => r["ISCOMPLETION_ORG"].ToString() != "Y").ToList().Count == 0)
                {
                    focusedRowChanged();
                }
                else
                {
                    var row = grdQCReliabilityResultLot.View.GetDataRow(e.PrevFocusedRowHandle);
                    string sISCOMPLETION = row["ISCOMPLETION_ORG"].ToString();
                    if (sISCOMPLETION == "Y")
                    {
                        (grdQCReliabilityResultLot.DataSource as DataTable).RejectChanges();
                        focusedRowChanged();
                    }
                    else
                    {
                        DialogResult result = System.Windows.Forms.DialogResult.No;

                        DataTable dtReliabilityLot = grdQCReliabilityResultLot.GetChangedRows();         //LOT정보 + Inspectionresult
                        DataTable dtReliabilityMeasurValue = grdMeasurValue.GetChangedRows();       //계측값
                        DataTable dtAttach = fpcAttach.GetChangedRows();                             //첨부파일
                        DataTable dtReport = fpcReportResult.GetChangedRows();                            //보고서
                        DataTable dtInspectionFile = null;
                        //dtInspectionFile = GetInspectionFile(e);
                        dtInspectionFile = GetInspectionFile(e.PrevFocusedRowHandle);
                        #region 편집된 항목있는지 판별
                        bool bCheckIsSave = false;
                        if (_dtInspectionFile.Rows.Count != dtInspectionFile.Rows.Count)
                        {
                            bCheckIsSave = true;
                        }
                        else
                        {

                            foreach (DataRow dr in _dtInspectionFile.Rows)
                            {
                                IEnumerable<DataRow> dataRow = from im in dtInspectionFile.AsEnumerable()
                                                               where im.Field<string>("FILEID") == dr["FILEID"].ToString()
                                                               select im;
                                if (dataRow == null)
                                {
                                    bCheckIsSave = true;
                                    break;
                                }
                                else
                                {
                                    foreach (DataRow vr in dataRow)
                                    {
                                        foreach (DataColumn dc in _dtInspectionFile.Columns)
                                        {
                                            if (dtInspectionFile.Columns.Contains(dc.ColumnName))
                                            {
                                                if (dr[dc.ColumnName].ToString() != vr[dc.ColumnName].ToString()
                                                    && (dc.ColumnName == "TITLE"
                                                    || dc.ColumnName == "MEASUREVALUE"
                                                    || dc.ColumnName == "FILEID")
                                                    )
                                                {
                                                    bCheckIsSave = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        if (dtReliabilityLot.Rows.Count > 0
                            || dtReliabilityMeasurValue.Rows.Count > 0
                            || dtAttach.Rows.Count > 0
                            || dtReport.Rows.Count > 0
                            || bCheckIsSave)
                        {
                            result = this.ShowMessage(MessageBoxButtons.YesNo, "InfoSaveAndSearch");//편집중 데이터를 저장 하시겠습니까?
                            if (result == System.Windows.Forms.DialogResult.No)
                            {
                                //grdQCReliabilityResultLot.View.SelectRow(e.PrevFocusedRowHandle);
                                grdQCReliabilityResultLot.View.FocusedRowChanged -= View_FocusedRowChanged;
                                grdQCReliabilityResultLot.View.FocusedRowHandle = e.PrevFocusedRowHandle;
                                grdQCReliabilityResultLot.View.FocusedRowChanged += View_FocusedRowChanged;
                                return;
                            }
                            else
                                SavePrevFocusedRow(e);
                        }
                        else
                        {
                            focusedRowChanged();
                        }
                    }
                }
                
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
        /// Form Road시 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReliaVerifiResultNonRegularPopup_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            InitializeGrid();
            selectUnitList(); // 단위

            Search();

            InitializePopup();
        }

        #endregion

        #region Private Function
        /// <summary>
        /// @@네트워크 경로 변경
        /// GetImageFileTable - 이미지파일을 ProgressFileDialog를 이용하여 전송하기 위한 DataTemplate을 생성하는 메소드
        /// 이미지파일 서버 전송을 위한 DataTable을 반환
        /// </summary>
        /// <returns></returns>
        private DataTable GetImageFileTable()
        {
            DataTable fileTable = new DataTable("");

            fileTable.Columns.Add("FILEID");
            fileTable.Columns.Add("FILENAME");
            fileTable.Columns.Add("FILEEXT");
            fileTable.Columns.Add("FILEPATH");
            fileTable.Columns.Add("SAFEFILENAME");
            fileTable.Columns.Add("FILESIZE");
            fileTable.Columns.Add("SEQUENCE");
            fileTable.Columns.Add("LOCALFILEPATH");
            fileTable.Columns.Add("RESOURCETYPE");
            fileTable.Columns.Add("RESOURCEID");
            fileTable.Columns.Add("RESOURCEVERSION");
            fileTable.Columns.Add("PROCESSINGSTATUS");

            return fileTable;
        }
        /// <summary>
        /// 불량코드 팝업 조회
        /// </summary>
        private void DefectCodePopup()
        {
            //var defectcodePopup = grdQCReliabilityLot.View.AddSelectPopupColumn("DEFECTCODE", new SqlQuery("GetDefectCodeList", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            var defectcodePopup = grdQCReliabilityResultLot.View.AddSelectPopupColumn("DEFECTCODE",80, new SqlQuery("GetDefectCodeList", "10004", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("DEFECTCODE", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(700, 500, FormBorderStyle.FixedToolWindow)
                .SetTextAlignment(TextAlignment.Center)
                //.SetValidationIsRequired()
                .SetPopupAutoFillColumns("DEFECTCODE")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                    if (selectedRows.Count() < 1)
                    {
                        return;
                    }

                    foreach (DataRow row in selectedRows)
                    {
                        dataGridRow["DEFECTCODE"] = row["DEFECTCODE"].ToString();
                        dataGridRow["DEFECTNAME"] = row["DEFECTCODENAME"].ToString();
                        dataGridRow["QCSEGMENTID"] = row["QCSEGMENTID"].ToString();
                        dataGridRow["QCSEGMENTNAME"] = row["QCSEGMENTNAME"].ToString();
                    }
                });

            defectcodePopup.Conditions.AddTextBox("DEFECTCODENAME");

            defectcodePopup.GridColumns.AddTextBoxColumn("DEFECTCODE", 150);
            defectcodePopup.GridColumns.AddTextBoxColumn("DEFECTCODENAME", 150);
            defectcodePopup.GridColumns.AddTextBoxColumn("QCSEGMENTID", 150);
            defectcodePopup.GridColumns.AddTextBoxColumn("QCSEGMENTNAME", 150);
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
        /// 체크한 항목이 있는지 확인
        /// </summary>
        private void ValidationCheckFile(SmartBandedGrid grd)
        {
            DataTable selectedFiles = grd.View.GetCheckedRows();

            if (selectedFiles.Rows.Count < 1)
            {
                throw MessageException.Create("GridNoChecked");
            }
        }

        private void View_CancelChange(object sender, CancelEventArgs e)
        {
            DataRow row = grdQCReliabilityResultLot.View.GetFocusedDataRow();
            if (row["ISCOMPLETION_ORG"].ToString() == "Y")
            {
                e.Cancel = true;
            }
            //else if (grdQCReliabilityResultLot.View.GetFocusedDataRow().RowState == DataRowState.Modified
            //    && grdQCReliabilityResultLot.View.GetFocusedRowCellValue("ISCOMPLETION").ToString().Length > 0
            //    && row["INSPECTIONRESULT"].ToString().Length == 0)
            //{
            //    row["ISCOMPLETION"] = string.Empty;
            //    ShowMessage("CheckSaveReliaVerifiResultNonRegular");//검증결과등록전 제품의 판정결과를 등록해야 합니다.
            //    e.Cancel = true;
            //}
        }
        /// <summary>
        /// 그리드의 포커스 행 변경시 행의 작업목록의 내용을 매핑처리 함.
        /// </summary>
        private void focusedRowChanged()
        {
            //포커스 행 체크 
            if (grdQCReliabilityResultLot.View.FocusedRowHandle < 0) return;
            btnSave.Enabled = true;
            var row = grdQCReliabilityResultLot.View.GetDataRow(grdQCReliabilityResultLot.View.FocusedRowHandle);
            //완료여부(Y)-> 수정 불가능
            string sISCOMPLETION = row["ISCOMPLETION"].ToString();
            if (sISCOMPLETION == "Y")
            {
                btnSave.Enabled = false;
            }
            else
            {
                btnSave.Enabled = (this.Owner as ReliaVerifiResultNonRegular).btnFlag.Enabled;
            }

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_ENTERPRISEID", _sENTERPRISEID);
            param.Add("P_PLANTID", _sPLANTID);
            param.Add("P_REQUESTNO", _sREQUESTNO);
            param.Add("P_PRODUCTDEFID", row["PRODUCTDEFID"].ToString());
            param.Add("P_LOTID", row["LOTID"].ToString());
            param.Add("P_MEASURETYPE", "MeasuredValue");
            param.Add("P_INSPITEMID", row["INSPITEMID"].ToString());
            param.Add("P_INSPITEMVERSION", row["INSPITEMVERSION"].ToString());
            DataTable dtReliabilityMeasurValue = SqlExecuter.Query("GetReliabilityNonRegularMeasureList", "10001", param);
            grdMeasurValue.DataSource = dtReliabilityMeasurValue;
            if (dtReliabilityMeasurValue.Rows.Count > 0)
            {
                cboUnit.EditValue = dtReliabilityMeasurValue.Rows[0]["UNIT"].ToString();
            }

            Dictionary<string, object> fileValues = new Dictionary<string, object>();
            //이미지 파일Search parameter
            fileValues.Add("P_FILERESOURCETYPE", "ReliaVerifiResultNonRegularFile");
            fileValues.Add("P_FILERESOURCEID", _sREQUESTNO + row["PRODUCTDEFID"].ToString() + row["PRODUCTDEFVERSION"].ToString() + row["LOTID"].ToString() + row["INSPITEMID"].ToString() + row["INSPITEMVERSION"].ToString());//요청번호,품목,버전,lot,검증item,버전
            fileValues.Add("P_FILERESOURCEVERSION", "0");

            DataTable fileTable = SqlExecuter.Query("SelectFileInspResult", "10001", fileValues);

            fpcAttach.DataSource = fileTable;

            (fpcAttach.DataSource as DataTable).RowChanged -= new DataRowChangeEventHandler(OnRowChanged);
            (fpcAttach.DataSource as DataTable).RowChanged += new DataRowChangeEventHandler(OnRowChanged);

            Dictionary<string, object> fileValues2 = new Dictionary<string, object>();
            //이미지 파일Search parameter
            fileValues2.Add("P_FILERESOURCETYPE", "ReliaVerifiResultNonRegularReport");
            fileValues2.Add("P_FILERESOURCEID", _sREQUESTNO + row["PRODUCTDEFID"].ToString() + row["PRODUCTDEFVERSION"].ToString() + row["LOTID"].ToString() + row["INSPITEMID"].ToString() + row["INSPITEMVERSION"].ToString());//요청번호,품목,버전,lot,검증item,버전
            fileValues2.Add("P_FILERESOURCEVERSION", "0");

            DataTable fileTable2 = SqlExecuter.Query("SelectFileInspResult", "10001", fileValues2);

            fpcReportResult.DataSource = fileTable2;

            (fpcReportResult.DataSource as DataTable).RowChanged -= new DataRowChangeEventHandler(OnRowChanged);
            (fpcReportResult.DataSource as DataTable).RowChanged += new DataRowChangeEventHandler(OnRowChanged);

            param.Add("P_INSPECTIONTYPE", "ReliaVerifiResultNonRegular");
            param.Add("P_RESOURCEID", row["PRODUCTDEFID"].ToString() + row["PRODUCTDEFVERSION"].ToString() + row["LOTID"].ToString() + row["INSPITEMID"].ToString() + row["INSPITEMVERSION"].ToString());//품목,버전,lot,검증item,버전
            param.Add("P_PROCESSRELNO", _sREQUESTNO);
            param["P_MEASURETYPE"] = "VerificationResult";
            _dtInspectionFile = SqlExecuter.Query("SelectInspectionFile", "10001", param);

            flowMeasuredPicture.Controls.Clear();
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
                            //@@네트워크 경로 변경
                            //Bitmap image = new Bitmap(GetImageFromWeb(AppConfiguration.GetString("Application.SmartDeploy.Url") + dr.GetString("WEBPATH")));

                            fileName = string.Join(".", fileName, fileText);
                            Bitmap image = CommonFunction.GetFtpImageFileToBitmap(filePath, fileName);

                            MultiVerificationResultControl vr = new MultiVerificationResultControl(image, fileName);
                            vr.setTitle(title);
                            vr.Tag = dr["FILEID"].ToString();
                            flowMeasuredPicture.Controls.Add(vr);

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
        private void CheckAllCtrl(bool bEnable)
        {
            btnAddMeasurValue.Enabled = bEnable;
            btnDeleteMeasurValue.Enabled = bEnable;
            txtCOMMENTS.Enabled = bEnable;

            fpcAttach.btnFileAdd.Enabled = bEnable;
            fpcAttach.btnFileDelete.Enabled = bEnable;

           
        }
        //popup이 닫힐때 그리드를 재조회 하는 이벤트 (팝업결과 저장 후 재조회)
        private async void Popup_FormClosed()
        {
            //await OnSearchAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        private void Search()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("P_ENTERPRISEID", _sENTERPRISEID);
            param.Add("P_PLANTID", _sPLANTID);
            param.Add("P_REQUESTNO", _sREQUESTNO);
            param.Add("P_RELIABILITYTYPE", "NonRegular");
            DataTable dtReliabilityRequest = SqlExecuter.Query("GetReliabilityVerificationRequestNonRegularRgisterList", "10001", param);           //신뢰성의뢰
            DataTable dtReliabilityLot = SqlExecuter.Query("GetRequestNonRegularQCReliabilityLot", "10001", param);               //LOT정보
            //grdQCReliabilityResultLot.View.FocusedRowHandle = -1;
            grdQCReliabilityResultLot.DataSource = dtReliabilityLot;

            if (dtReliabilityRequest.Rows.Count > 0)
            {
                _dtReliabilityRequest = dtReliabilityRequest;
                DataRow dr = dtReliabilityRequest.Rows[0];
                txtCOMMENTS.Text = dr["COMMENTS"].ToString();
                popInspector.Text = dr["INSPECTOR"].ToString();

                string sANALYSISTOOL = dr["ANALYSISTOOL"].ToString();
                string[] arrANALYSISTOOL = sANALYSISTOOL.Split(',');
                foreach (string s in arrANALYSISTOOL)
                {
                    if (s.Trim().Length > 0 && s.Trim() == "Visual") chkVisual.Checked = true;
                    if (s.Trim().Length > 0 && s.Trim() == "MS") chkMS.Checked = true;
                    if (s.Trim().Length > 0 && s.Trim() == "XRay") chkXRay.Checked = true;
                    if (s.Trim().Length > 0 && s.Trim() == "FE") chkFE.Checked = true;
                    if (s.Trim().Length > 0 && s.Trim() == "EDS") chkEDS.Checked = true;
                    if (s.Trim().Length > 0 && s.Trim() == "3D") chk3D.Checked = true;
                }

                string sAREAPOINT = dr["AREAPOINT"].ToString();
                string[] arrAREAPOINT = sAREAPOINT.Split(',');
                foreach (string s in arrAREAPOINT)
                {
                    if (s.Trim().Length > 0 && s.Trim() == "Hole") chkHole.Checked = true;
                    if (s.Trim().Length > 0 && s.Trim() == "Plate") chkPlate.Checked = true;
                    if (s.Trim().Length > 0 && s.Trim() == "OLB") chkOLB.Checked = true;
                    if (s.Trim().Length > 0 && s.Trim() == "PSR") chkPSR.Checked = true;
                    if (s.Trim().Length > 0 && s.Trim() == "Adhesive") chkAdhesive.Checked = true;
                    if (s.Trim().Length > 0 && s.Trim() == "PI") chkPI.Checked = true;
                    if (s.Trim().Length > 0 && s.Trim() == "Inforce") chkInforce.Checked = true;
                }
            }

            chkVisual.Enabled = false;
            chkMS.Enabled = false;
            chkXRay.Enabled = false;
            chkFE.Enabled = false;
            chkEDS.Enabled = false;
            chk3D.Enabled = false;
            chkHole.Enabled = false;
            chkPlate.Enabled = false;
            chkOLB.Enabled = false;
            chkPSR.Enabled = false;
            chkAdhesive.Enabled = false;
            chkPI.Enabled = false;
            chkInforce.Enabled = false;

            //완료여부(Y)-> 수정 불가능
            //focusedRowChanged 이벤트에서 현재 선택된 로우의 완료여부에 따라 저장 버튼 활성화를 선택한다
            //if (dtReliabilityLot.Rows.Count > 0 && dtReliabilityLot.AsEnumerable().Where(r => r["ISCOMPLETION"].ToString() != "Y").ToList().Count == 0)
            //    btnSave.Enabled = false;
            //else
            //    btnSave.Enabled = true;
        }

        /// <summary>
        /// 검사자를 선택하는 팝업
        /// </summary>
        private void InitializePopup()
        {
            popInspector.Editor.SelectPopupCondition = CreateUserPopup();
        }

        /// <summary>
        /// 검사자 팝업
        /// </summary>
        /// <returns></returns>
        private ConditionItemSelectPopup CreateUserPopup()
        {
            ConditionItemSelectPopup conditionItem = new ConditionItemSelectPopup();
            conditionItem.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            conditionItem.SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, true);
            conditionItem.Id = "INSPECTIONUSER";
            conditionItem.LabelText = "INSPECTIONUSER";
            conditionItem.SearchQuery = new SqlQuery("GetUserList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            conditionItem.IsMultiGrid = false;
            conditionItem.DisplayFieldName = "USERNAME";
            conditionItem.ValueFieldName = "USERID";
            conditionItem.LanguageKey = "INSPECTIONUSER";

            conditionItem.Conditions.AddTextBox("USERIDNAME");

            conditionItem.GridColumns.AddTextBoxColumn("USERID", 150);
            conditionItem.GridColumns.AddTextBoxColumn("USERNAME", 200);

            return conditionItem;
        }
        #endregion

        #region 유효성 검사
        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        //protected override void OnValidateContent()
        //{
        //    base.OnValidateContent();

        //    grdAuditManageregist.View.CheckValidation();

        //    DataTable changed = grdAuditManageregist.GetChangedRows();

        //    if (changed.Rows.Count == 0)
        //    {
        //        // 저장할 데이터가 존재하지 않습니다.
        //        throw MessageException.Create("NoSaveData");
        //    }
        //}
        #endregion
    }
}
 