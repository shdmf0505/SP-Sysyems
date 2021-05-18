#region using

using DevExpress.Spreadsheet;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using Micube.SmartMES.QualityAnalysis.Helper;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 출하검사 > 출하검사성적서 엑셀출력 > 출하검사성적서 엑셀출력 POPUP
    /// 업  무  설  명  : 선택된 Lot에 대하여 출하검사상적서를 Excel 파일 형식으로 내보내고, 측정 값을 입력, 저장 할 수 있는 화면
    /// 생    성    자  : JAR
    /// 생    성    일  : 2020-01-12
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ShipmentInspExportPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        public int WidthSize { get; set; }
        public int HeightSize { get; set; }
        //SpreadSheet에서 입력된 Data를 저장하는 Grid
        SmartBandedGrid _grdExportData;    // Main Grid
        SmartBaseForm _msg = new SmartBaseForm();
        private string strTxnHISTKey = string.Empty;
        public DataRow CurrentDataRow { get; set; }
        private DataTable _GDataTable;
        #endregion

        #region 생성자

        public ShipmentInspExportPopup()
        {
            InitializeComponent();
            InitializeEvent();
            //Sheet의 Paste 기능 제한
            //기존 팝업 메뉴 제한
            shtExport.Options.Behavior.ShowPopupMenu = DevExpress.XtraSpreadsheet.DocumentCapability.Disabled;
            //하단 워크시트 Tab 추가 제한
            shtExport.Options.Behavior.Worksheet.Insert = DevExpress.XtraSpreadsheet.DocumentCapability.Disabled;
            //하단 워크시트 Tab + 버튼 히든
            shtExport.Options.Behavior.Worksheet.Insert = DevExpress.XtraSpreadsheet.DocumentCapability.Hidden;
        }

        #endregion

        #region 컨텐츠 영역 초기화
        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            _grdExportData.View.AddTextBoxColumn("LOTID");
            _grdExportData.View.AddTextBoxColumn("RESOURCETYPE");
            _grdExportData.View.AddTextBoxColumn("PROCESSRELNO");
            _grdExportData.View.AddTextBoxColumn("SHEETINDEX");
            _grdExportData.View.AddTextBoxColumn("EXCELFIELD");
            _grdExportData.View.PopulateColumns();
        }
        #endregion

        #region Event
        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            this.FormClosed += (s, e) =>
            {
                DisposingSpreadSheet();
            };

            this.Shown += (s, e) =>
            {
                this.Size = new Size(WidthSize, HeightSize);
                this.CenterToScreen();
                ContextMenu PopupMenu = new ContextMenu();
                MenuItem Item = new MenuItem();
                Item.Tag = "ATTACHIMAGE";
                Item.Text = Framework.Language.GetDictionary("ATTACHIMAGE").Name;
                Item.Click += Item_Click;
                PopupMenu.MenuItems.Add(Item);

                Item = new MenuItem();
                Item.Tag = "LOT";
                Item.Text = "LOT";

                Item.MenuItems.AddRange(new MenuItem[]
                                        { new MenuItem() { Tag = "ADD", Text = Framework.Language.GetDictionary("ADD").Name },
                                          new MenuItem() { Tag = "CONVERSION", Text = Framework.Language.GetDictionary("CONVERSION").Name }});

                foreach(MenuItem item in Item.MenuItems)
                    item.Click += Item_Click;

                PopupMenu.MenuItems.Add(Item);
                this.shtExport.ContextMenu = PopupMenu;

                //해당 품목에 대한 출하검사성적서를 찾는다
                //성적서가 없으면 POP닫기
                if (!SearchExportSheetRevision())
                {
                    _msg.ShowMessage("NoExcelRevision");
                    this.Close();
                    return;
                }
                else
                {
                    //콤보박스 바인딩 후 이벤트 추가
                    this.cboExportRev.EditValueChanged += CboExportRev_EditValueChanged;
                }

                cboExportRev.EditValue = CurrentDataRow["REVISION"].ToSafeInt32();
            };

            this.btnExport.Click += (s, e) =>
            {
                //출하성적서 내보내기 
                SaveFileDialog opf = new SaveFileDialog();

                opf.FileName = @"출하성적서_" + txtCustomer.Text + "_" + txtProductModel.Text + "(" + txtLotNo.Text + ")_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx"; //초기 파일명을 지정할 때 사용한다.
                opf.Filter = "Excel Files(*.xlsx)|*.xlsx;*.xls";
                opf.Title = "Save an Excel File";

                opf.ShowDialog();
                if (opf.FileName.Length > 0)
                    shtExport.SaveDocument(opf.FileName.ToString());
            };

            this.btnSave.Click += (s, e) =>
            {
                DialogResult dr = new DialogResult();
                dr = _msg.ShowMessage(MessageBoxButtons.YesNo, "InfoPopupSave");
                if (dr == DialogResult.No) return;//변경 내용을 저장하시겠습니까??

                try
                {
                    this.ShowWaitArea();

                    btnSave.Enabled = false;
                    btnExport.Enabled = false;
                    
                    DataTable dtSource = (_grdExportData.DataSource as DataTable).Copy();
                    DataTable dt = dtSource.Clone();
                    
                    foreach(DataRow row in dtSource.Rows)
                    {
                        if(!row["VALUE"].ToSafeString().Equals(string.Empty))
                        {
                            dt.ImportRow(row);
                        }
                    }

                    dt.TableName = "list";
                    var view = cboExportRev.GetSelectedDataRow() as DataRowView;
                    MessageWorker messageWorker = new MessageWorker("SaveShipmentExportDataHIST");
                    messageWorker.SetBody(new MessageBody()
                        {
                            { "list", dt },
                            { "resourceid", CurrentDataRow["LOTID"].ToString() },
                            { "productdefid", CurrentDataRow["PRODUCTDEFID"].ToString() },
                            { "productdefversion", CurrentDataRow["PRODUCTDEFVERSION"].ToString() },
                            { "productclassid", CurrentDataRow["PRODUCTCLASSID"].ToString() },
                            { "revision", cboExportRev.EditValue.ToString() },
                            { "txnhistkey", strTxnHISTKey },
                            { "fileid", view["FILEID"].ToString() }

                        });
                    var response = messageWorker.Execute<DataTable>();
                    if (!response.Success)
                    {
                        _msg.ShowMessage(response.GetFailMessage());
                        return;
                    }

                    //저장확인 후 이미지파일 업로드
                    try
                    {
                        DataTable dtImageFile = (_grdExportData.DataSource as DataTable).AsEnumerable().Where(
                                                                w => w.Field<string>("ISIMAGE").Equals("Y")
                                                                   && !w.Field<string>("LOCALFILEPATH").Equals("")
                                                                   && (w.Field<string>("STATE").Equals("NEW") || w.Field<string>("STATE").Equals("MODIFY"))
                                                                ).CopyToDataTable();

                        dtImageFile.TableName = "ImageList";

                        //@@네트워크 경로 변경----------------------------------------------------------------------
                        //파일업로드 팝업 데이터 전달
                        //기존 MI 로직
                        DataTable fileUploadTable = QcmImageHelper.GetImageFileTable();
                        int totalFileSize = 0;
                        foreach (DataRow originRow in dtImageFile.Rows)
                        {
                            DataRow newRow = fileUploadTable.NewRow();
                            newRow["FILEID"] = originRow["Value"];
                            newRow["FILENAME"] = originRow["Value"];
                            newRow["SAFEFILENAME"] = originRow["SAFEFILENAME"].ToString().Split('.')[0];
                            newRow["FILEEXT"] = originRow["FILEEXT"];
                            newRow["FILEPATH"] = "ShipmentMappingDocument_Image/";
                            newRow["LOCALFILEPATH"] = originRow["LOCALFILEPATH"];
                            newRow["FILESIZE"] = originRow["FILESIZE"];
                            newRow["PROCESSINGSTATUS"] = "Wait";
                            newRow["SEQUENCE"] = 1; //의미가 없어 여기에선

                            totalFileSize += originRow.GetInteger("FILESIZE");
                            fileUploadTable.Rows.Add(newRow);
                        }

                        if (fileUploadTable.Rows.Count > 0)
                        {
                            FileProgressDialog fileProgressDialog = new Micube.SmartMES.Commons.Controls.FileProgressDialog(fileUploadTable, UpDownType.Upload, Application.StartupPath + "\\Temp", totalFileSize);
                            fileProgressDialog.ShowDialog(this);

                            if (fileProgressDialog.DialogResult == DialogResult.Cancel)
                                throw MessageException.Create("CancelMessageToUploadFile");  //파일업로드를 취소하였습니다.

                            ProgressingResult fileResult = fileProgressDialog.Result;

                            if (!fileResult.IsSuccess)
                                throw MessageException.Create("FailMessageToUploadFile"); //파일업로드를 실패하였습니다.
                        }
                    }
                    catch(Exception ex) { }
                    //정보 업데이트
                    SearchExportWriteData();
                    _msg.ShowMessage("SuccessSave");
                }
                catch (Exception ex)
                {
                    _msg.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();
                    btnSave.Enabled = true;
                    btnExport.Enabled = true;
                }
            };

            //수기 등록된 이미지 삭제
            //해당 이미지의 위치를 찾아서 Value를 string.Empty로 바꿈
            shtExport.ShapeRemoving += (s, e) =>
            {
                if (e.ShapeType != ShapeType.Picture) return;

                try
                {
                    string strPictureFiled = (s as DevExpress.XtraSpreadsheet.SpreadsheetControl).SelectedPicture.TopLeftCell.ToString();
                    //필요없는 구분자 제거
                    //Field값은 Index 1, 그외 값은 필요없음
                    char[] delimiterChars = { ' ', ',', ':', '"' };
                    string[] strSplit = strPictureFiled.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

                    DataTable dtCopy = (_grdExportData.DataSource as DataTable);
                    //값이 변경 된 Cell의 좌표값과 SheetSeq를 찾음
                    var query = dtCopy.AsEnumerable().Where(w => w.Field<string>("EXCELFIELD").Equals(strSplit[1]) && w.Field<int>("SHEETINDEX").Equals((s as DevExpress.XtraSpreadsheet.SpreadsheetControl).ActiveWorksheet.Index));

                    if (query.Count() == 0) return;

                    foreach (DataRow item in query)
                    {
                        item.Delete();
                    }
                    dtCopy.AcceptChanges();
                    _grdExportData.DataSource = dtCopy;
                }
                catch (Exception ex)
                {
                    _msg.ShowError(ex);
                }
            };
            //새로고침 시 저장된 데이터를 비우고 다시불러온다
            btnReload.Click += (s, e) =>
            {
                try
                {
                    shtExport.BeginUpdate();
                    try
                    {
                        //Header 정보 변경
                        var view = cboExportRev.GetSelectedDataRow() as DataRowView;
                        
                        txtLotNo.Text = CurrentDataRow["LOTID"].ToString();
                        txtCustomer.Text = view["CUSTOMERNAME"].ToString();
                        txtInspectionDate.Text = view["INSPECTIONDATE"].ToString();
                        txtProductModel.Text = view["MODELVALUE"].ToString();

                        StringBuilder sbFilePath = new StringBuilder();

                        //파일을 저장 할 임시폴더 생성
                        sbFilePath.Append(Application.StartupPath).Append("\\Temp");
                        string strFolderPath = sbFilePath.ToString();

                        //폴더가 존재하지 않으면 폴더생성
                        System.IO.DirectoryInfo dic = new System.IO.DirectoryInfo(sbFilePath.ToString());
                        if (!dic.Exists)
                            dic.Create();

                        sbFilePath.Append("\\");
                        //현재양식이 아닌 출하검사성적서 양식매핑에 등록된 파일ID로 다시 불러온다.
                        //2020.04.02-유석진-파일ID가 아닌 파일명으로 파일이 생성 되도록 수정
                        sbFilePath.Append(view["FILENAME"].ToString());
                        //sbFilePath.Append(view["FILEID"].ToString());
                        //Sheet 다운로드
                        if (new System.IO.FileInfo(sbFilePath.ToString()).Exists)
                        {
                            //파일이 존재하면 바로 Load
                            shtExport.LoadDocument(sbFilePath.ToString());
                        }
                        else
                        {
                            //파일이 존재하지 않을시 NAS 서버에서 파일 다운로드 후 Load
                            //MI 로직
                            using (System.Net.WebClient web = new System.Net.WebClient())
                            {
                                string ftpServerUserId = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Id"));
                                string ftpServerPassword = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Password"));
                                string ServerPath = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Url")) + "ShipmentMappingDocument";
                                ServerPath = ServerPath + ((ServerPath.EndsWith("/")) ? "" : "/") + view["CURRENTFILEID"].ToString();
                                web.Credentials = new System.Net.NetworkCredential(ftpServerUserId, ftpServerPassword);

                                web.DownloadFile(ServerPath, sbFilePath.ToString());
                            }
                            shtExport.LoadDocument(sbFilePath.ToString());
                        }
                        //현재 양식을 매핑된 양식으로 저장하도록 업데이트 한다.
                        view["FILEID"] = view["CURRENTFILEID"].ToString();
                    }
                    catch (Exception ex)
                    {
                        _msg.ShowError(ex);
                    }
                    shtExport.EndUpdate();
                    //그리드 초기화, 기존에 저장된 데이터를 모두 비우고 다시 바인딩 한다.
                    GetWriteData(); //Grid DataSource가 null이면 CellValueChange 시 오류 남
                    _grdExportData.DataSource = ((DataTable)_grdExportData.DataSource).Clone();

                    SearchExportHeader(CurrentDataRow["LOTID"].ToString(), shtExport);
                    SearchExportMESInpectionData(CurrentDataRow["LOTID"].ToString(), this.shtExport);
                }
                catch (Exception ex)
                {
                    _msg.ShowError(ex);
                }
            };
            this.shtExport.CellValueChanged += shtExport_CellValueChanged;
        }


        /// <summary>
        /// ContextMenu ClickEvent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_Click(object sender, EventArgs e)
        {
            shtExport.BeginUpdate();
            try
            {
                if (((MenuItem)sender).Tag.ToString().Equals("ATTACHIMAGE"))
                {
                    //드래그 크기만큼 이미지 삽입
                    OpenFileDialog dr = new OpenFileDialog();
                    dr.Filter = "이미지 파일 ( *.jpg, *.jpeg, *.png, *.bmp, *.gif,*.tiff) | *.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tiff;*.tif;*.JPG;*.JPEG;*.PNG;*.GIF;*.BMP;*.TIFF;*.TIF;";
                    if (dr.ShowDialog() != DialogResult.OK) return;
                    IList<DevExpress.Spreadsheet.Range> IRange = shtExport.GetSelectedRanges();

                    //쓸때없는 구분자를 삭제함
                    //Cell의 시작지점을 찾기 위하여
                    //Field값은 Index 1, 그외 값은 필요없음
                    char[] delimiterChars = { ' ', ',', ':', '"' };
                    string[] strResult = IRange[0].ToString().Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

                    //Cell 시작
                    string strStart = strResult[1];
                    //이미지 가로 사이즈
                    int intColumnSize = IRange[0].ColumnCount;
                    //이미지 세로 사이즈
                    int intRowSize = IRange[0].RowCount;
                    
                    try
                    {
                        using (Image img = Image.FromFile(dr.FileName))
                        {
                            long fileSize = new System.IO.FileInfo(dr.FileName).Length;
                            SetImage(shtExport.ActiveWorksheet, img, shtExport.ActiveWorksheet.Cells[strStart], intColumnSize, intRowSize);
                            SetCellValueToGrid(strStart, IRange[0].Worksheet.Index, dr,false, true, intColumnSize, intRowSize, fileSize);
                        }
                        shtExport.EndUpdate();
                    }
                    catch (Exception ex)
                    {
                        _msg.ShowError(ex);
                    }
                }
                else
                {
                    MenuItem m = sender as MenuItem;
                    //Lot 추가 
                    ShipmentInspExportRegistPopup popup = new ShipmentInspExportRegistPopup(CurrentDataRow["PRODUCTDEFID"].ToString(), CurrentDataRow["PRODUCTDEFVERSION"].ToString())
                    {
                        StartPosition = FormStartPosition.CenterParent
                    };

                    if (popup.ShowDialog().Equals(DialogResult.OK))
                    {
                        using (SmartSpreadSheet tempSheet = new SmartSpreadSheet())
                        {
                            #region 양식파일 Down
                            var view = cboExportRev.GetSelectedDataRow() as DataRowView;
                            StringBuilder sbFilePath = new StringBuilder();
                            //파일을 저장 할 임시폴더 생성
                            sbFilePath.Append(Application.StartupPath).Append("\\Temp");
                            string strFolderPath = sbFilePath.ToString();

                            //폴더가 존재하지 않으면 폴더생성
                            System.IO.DirectoryInfo dic = new System.IO.DirectoryInfo(sbFilePath.ToString());
                            if (!dic.Exists)
                                dic.Create();

                            sbFilePath.Append("\\");
                            //2020.04.02-유석진-파일ID가 아닌 파일명으로 파일이 생성 되도록 수정
                            sbFilePath.Append(view["FILENAME"].ToString());
                            //sbFilePath.Append(view["FILEID"].ToString());

                            //Sheet 다운로드
                            if (new System.IO.FileInfo(sbFilePath.ToString()).Exists)
                            {
                                //파일이 존재하면 바로 Load
                                tempSheet.LoadDocument(sbFilePath.ToString());
                            }
                            else
                            {
                                //파일이 존재하지 않을시 NAS 서버에서 파일 다운로드 후 Load
                                //MI 로직
                                using (System.Net.WebClient web = new System.Net.WebClient())
                                {
                                    string ftpServerUserId = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Id"));
                                    string ftpServerPassword = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Password"));
                                    string ServerPath = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Url")) + "ShipmentMappingDocument";
                                    ServerPath = ServerPath + ((ServerPath.EndsWith("/")) ? "" : "/") + view["FILEID"].ToString();
                                    web.Credentials = new System.Net.NetworkCredential(ftpServerUserId, ftpServerPassword);

                                    web.DownloadFile(ServerPath, sbFilePath.ToString());
                                }
                                tempSheet.LoadDocument(sbFilePath.ToString());
                            }
                            #endregion
                            bool isMappingHederBinded = false;
                            bool isMappingItemBinded = false;
                            int CurrentWorkSheetIndex = shtExport.Document.Worksheets.ActiveWorksheet.Index;
                            if (shtExport.Document.Worksheets.ActiveWorksheet.Tag == null)
                            {
                                isMappingItemBinded = SearchExportMESInpectionData(popup.LotNo, tempSheet, true, false, CurrentWorkSheetIndex);
                                isMappingHederBinded = SearchExportHeader(popup.LotNo, tempSheet, true, CurrentWorkSheetIndex);
                            }
                            else
                            {
                                isMappingItemBinded = SearchExportMESInpectionData(popup.LotNo, tempSheet, true, false, shtExport.Document.Worksheets.ActiveWorksheet.Tag.ToSafeInt32());
                                isMappingHederBinded = SearchExportHeader(popup.LotNo, tempSheet, true, shtExport.Document.Worksheets.ActiveWorksheet.Tag.ToSafeInt32());
                            }
                            if (isMappingHederBinded || isMappingItemBinded)
                            {
                                if (((MenuItem)sender).Tag.ToString().Equals("ADD"))
                                {
                                    string SheetName = (shtExport.Document.Worksheets.ActiveWorksheet.Name + "_" + Guid.NewGuid().ToString()).Substring(0, 31);
                                    try
                                    {
                                        shtExport.Document.Worksheets.Add(SheetName);
                                    }
                                    catch
                                    {
                                        SheetName = Guid.NewGuid().ToString().Substring(0, 31);
                                        shtExport.Document.Worksheets.Add(SheetName);
                                    }
                                    if (shtExport.Document.Worksheets[CurrentWorkSheetIndex].Tag == null)
                                    {
                                        shtExport.Document.Worksheets[SheetName].CopyFrom(tempSheet.Document.Worksheets[CurrentWorkSheetIndex]);
                                        shtExport.Document.Worksheets[SheetName].Tag = CurrentWorkSheetIndex.ToString();
                                    }
                                    else
                                    {
                                        shtExport.Document.Worksheets[SheetName].CopyFrom(tempSheet.Document.Worksheets[shtExport.Document.Worksheets[CurrentWorkSheetIndex].Tag.ToSafeInt32()]);
                                        shtExport.Document.Worksheets[SheetName].Tag = shtExport.Document.Worksheets[CurrentWorkSheetIndex].Tag.ToString();
                                    }
                                }
                                else
                                {
                                    if(shtExport.Document.Worksheets[CurrentWorkSheetIndex].Tag == null)
                                        shtExport.Document.Worksheets[CurrentWorkSheetIndex].CopyFrom(tempSheet.Document.Worksheets[CurrentWorkSheetIndex]);
                                    else
                                        shtExport.Document.Worksheets[CurrentWorkSheetIndex].CopyFrom(tempSheet.Document.Worksheets[shtExport.Document.Worksheets[CurrentWorkSheetIndex].Tag.ToSafeInt32()]);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _msg.ShowError(ex);
            }
            shtExport.EndUpdate();
        }
        //Revision ComboBox ValueChanged
        private void CboExportRev_EditValueChanged(object sender, EventArgs e)
        {
            shtExport.BeginUpdate();
            try
            {
                //Header 정보 변경
                var view = cboExportRev.GetSelectedDataRow() as DataRowView;
                bool isExistsData = view["SAVEFLAG"].ToString().Equals("Y") ? true : false;
                txtLotNo.Text = CurrentDataRow["LOTID"].ToString();
                txtCustomer.Text = view["CUSTOMERNAME"].ToString();
                txtInspectionDate.Text = view["INSPECTIONDATE"].ToString();
                txtProductModel.Text = view["MODELVALUE"].ToString();

                StringBuilder sbFilePath = new StringBuilder();

                //파일을 저장 할 임시폴더 생성
                sbFilePath.Append(Application.StartupPath).Append("\\Temp");
                string strFolderPath = sbFilePath.ToString();

                //폴더가 존재하지 않으면 폴더생성
                System.IO.DirectoryInfo dic = new System.IO.DirectoryInfo(sbFilePath.ToString());
                if (!dic.Exists)
                    dic.Create();

                sbFilePath.Append("\\");
                //2020.04.02-유석진-파일ID가 아닌 파일명으로 파일이 생성 되도록 수정
                sbFilePath.Append(view["FILENAME"].ToString());
                //sbFilePath.Append(view["FILEID"].ToString());
                //성적서가 저장된 정보를 불러온 경우
                if (isExistsData)
                {
                    using (System.Net.WebClient web = new System.Net.WebClient())
                    {
                        string ftpServerUserId = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Id"));
                        string ftpServerPassword = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Password"));
                        string ServerPath = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Url")) + "ShipmentMappingDocument";
                        ServerPath = ServerPath + ((ServerPath.EndsWith("/")) ? "" : "/") + view["FILEID"].ToString();
                        web.Credentials = new System.Net.NetworkCredential(ftpServerUserId, ftpServerPassword);

                        web.DownloadFile(ServerPath, sbFilePath.ToString());
                    }
                    shtExport.LoadDocument(sbFilePath.ToString());
                    GetWriteData();
                    SearchExportWriteData();
                    shtExport.EndUpdate();
                    return;
                }
                else
                {
                    //Sheet 다운로드
                    if (new System.IO.FileInfo(sbFilePath.ToString()).Exists)
                    {
                        //파일이 존재하면 바로 Load
                        shtExport.LoadDocument(sbFilePath.ToString());
                    }
                    else
                    {
                        //파일이 존재하지 않을시 NAS 서버에서 파일 다운로드 후 Load
                        //MI 로직
                        using (System.Net.WebClient web = new System.Net.WebClient())
                        {
                            string ftpServerUserId = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Id"));
                            string ftpServerPassword = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Password"));
                            string ServerPath = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Url")) + "ShipmentMappingDocument";
                            ServerPath = ServerPath + ((ServerPath.EndsWith("/")) ? "" : "/") + view["FILEID"].ToString();
                            web.Credentials = new System.Net.NetworkCredential(ftpServerUserId, ftpServerPassword);

                            web.DownloadFile(ServerPath, sbFilePath.ToString());
                        }
                        shtExport.LoadDocument(sbFilePath.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                _msg.ShowError(ex);
            }
            shtExport.EndUpdate();

            GetWriteData();

            SearchExportHeader(CurrentDataRow["LOTID"].ToString(), shtExport);
            SearchExportMESInpectionData(CurrentDataRow["LOTID"].ToString(), this.shtExport);
            SearchExportWriteData();
        }

        //SpreadSheet Cell값 변경 시 내부 Grid _grdExportData에 담는 이벤트
        private void shtExport_CellValueChanged(object sender, DevExpress.XtraSpreadsheet.SpreadsheetCellEventArgs e)
        {
            SetCellValueToGrid(e.Cell.GetReferenceA1(), e.Worksheet.Index, e.Cell.Value);
        }
        #endregion

        #region Private Function
        // TODO : 화면에서 사용할 내부 함수 추가
        
        private void SetCellValueToGrid(string Reference, int Index, object Value, bool isGData = false, bool IsImage = false, int ColSize = 0, int RowSize = 0, long FileSize = 0)
        {
            try
            {
                DataTable dtCopy = (_grdExportData.DataSource as DataTable);
                //값이 변경 된 Cell의 좌표값과 SheetSeq를 찾음
                var query = dtCopy.AsEnumerable().Where(w => w.Field<string>("EXCELFIELD").Equals(Reference) && w.Field<int>("SHEETINDEX").Equals(Index));

                string strLotNo = CurrentDataRow["LOTID"].ToString();
                int intRevisionNo = cboExportRev.EditValue.ToSafeInt32();

                object objValue = Value;
                string strFilePath = string.Empty;
                string strFileEXT = string.Empty;
                string strFileName = string.Empty;
                string strSafeFileName = string.Empty;
                string strLocalFilePath = string.Empty;
                bool blFlag = false;

                //넘어오는 Value의 값이 OpenFileDialog이면 이미지 파일
                if (Value.GetType() == typeof(OpenFileDialog) || IsImage)
                {
                    OpenFileDialog dr = Value as OpenFileDialog;
                    strFilePath = "ShipmentMappingDocument_Image";
                    if (dr != null)
                    {
                        objValue = "FILE -" + CurrentDataRow["LOTID"] + DateTime.Now.ToString("yyyyMMddHHmmss");
                        strFileEXT = Path.GetExtension(dr.FileName).Replace(".", "");
                        strFileName = Path.GetFileNameWithoutExtension(dr.FileName);
                        strSafeFileName = dr.SafeFileName.Split('.')[0];
                        strLocalFilePath = dr.FileName;
                    }
                    else
                    {
                        //직접 올린 이미지가 아닌 신뢰성이나 품질규격측정에서 넘어온 이미지임
                        strFileEXT = Path.GetExtension(Value.ToString().Replace(".", ""));
                        strFileName = Path.GetFileNameWithoutExtension(Value.ToString());
                        strFilePath = Value.ToString();
                    }
                    blFlag = true;
                }

                if (query.Count() == 0)
                {
                    DataRow r = dtCopy.NewRow();
                    r["TXNHISTKEY"] = strTxnHISTKey;
                    r["RESOURCEID"] = strLotNo;
                    r["REVISIONNO"] = intRevisionNo;
                    r["PRODUCTDEFID"] = CurrentDataRow["PRODUCTDEFID"];
                    r["PRODUCTDEFVERSION"] = CurrentDataRow["PRODUCTDEFVERSION"];
                    r["PRODUCTCLASSID"] = CurrentDataRow["PRODUCTCLASSID"];
                    r["SHEETINDEX"] = Index;
                    r["EXCELFIELD"] = Reference;
                    r["VALUE"] = objValue;
                    r["ISIMAGE"] = IsImage == true ? "Y" : "N";
                    r["IMAGECOLUMNRANGE"] = ColSize;
                    r["IMAGEROWRANGE"] = RowSize;
                    r["FILESIZE"] = FileSize;
                    r["FILENAME"] = strFileName;
                    r["SAFEFILENAME"] = strSafeFileName;
                    r["FILEEXT"] = strFileEXT;
                    r["FILEPATH"] = strFilePath;
                    r["LOCALFILEPATH"] = strLocalFilePath;
                    r["STATE"] = "NEW";

                    dtCopy.Rows.Add(r);
                    dtCopy.AcceptChanges();
                }
                else 
                {
                    //GData는 최초에 생성된 데이터만 저장하도록 합니다.
                    if (!isGData)
                    {
                        foreach (var r in query)
                        {
                            r["VALUE"] = objValue;
                            r["ISIMAGE"] = IsImage == true ? "Y" : "N";
                            r["IMAGECOLUMNRANGE"] = ColSize;
                            r["IMAGEROWRANGE"] = RowSize;
                            r["FILESIZE"] = FileSize;
                            r["FILENAME"] = strFileName;
                            r["SAFEFILENAME"] = strSafeFileName;
                            r["FILEEXT"] = strFileEXT;
                            r["FILEPATH"] = strFilePath;
                            r["LOCALFILEPATH"] = strLocalFilePath;
                            r["STATE"] = (objValue.ToString() == null | objValue.ToString().Equals(string.Empty)) == true ? "DELETE" : "MODIFY";
                        }
                    }
                }
                _grdExportData.DataSource = dtCopy;
            }
            catch (Exception ex)
            {
                _msg.ShowError(ex);
            }
        }

        /// <summary>
        /// SpreadSheet Resource Disposing
        /// </summary>
        private void DisposingSpreadSheet()
        {
            try
            {
                if (shtExport != null)
                {
                    shtExport.Document.Dispose();
                    shtExport.Dispose();
                }

                System.IO.DirectoryInfo dic = new System.IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath + "\\Temp");
                if (dic.Exists)
                    dic.Delete(true);

                GC.Collect();
            }
            catch(Exception ex)
            {
                _msg.ShowError(ex);
            }
        }

        /// <summary>
        /// 해당 품목에 대한 출하검사성적서 Load
        /// </summary>
        private bool SearchExportSheetRevision()
        {
            try
            {
                //Revision Data Load
                //CT_SHIPMENTMAPPINGLIST Table Data
                //제품정의ID, 제품정의버전, 제품그룹ID
                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    {"PLANTID",CurrentDataRow["PLANTID"].ToString() },
                    {"RESOURCEID",CurrentDataRow["LOTID"].ToString() },
                    {"PRODUCTDEFID",CurrentDataRow["PRODUCTDEFID"].ToString() },
                    {"PRODUCTDEFVERSION",CurrentDataRow["PRODUCTDEFVERSION"].ToString() },
                    {"PRODUCTCLASSID",CurrentDataRow["PRODUCTCLASSID"].ToString() }
                };

                DataTable dt = SqlExecuter.Query("SelectShipmentExportInspectionRevisionData", "10001", param);

                if (dt.Rows.Count < 1)
                {
                    //해당 Table의 Data가 없다는 의미는 해당 제품에 대하여 양식이 매핑되어있는 양식이 없다는 의미
                    //그러므로 해당 화면의 작업을 수행 할 수 없으므로 Close Return
                    return false;
                }

                //ComboBox DataBinding
                cboExportRev.DataSource = dt.Copy();
                cboExportRev.DisplayMember = "REVISIONNO";
                cboExportRev.ValueMember = "REVISIONNO";
                cboExportRev.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
                for (int i = 0; i < cboExportRev.Columns.Count; i++)
                {
                    if (cboExportRev.Columns[i].FieldName == "REVISIONNO") continue;
                    cboExportRev.Columns[i].Visible = false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _msg.ShowError(ex);
                return false;
            }
        }

        #region MappingData 불러오기
        /// <summary>
        /// Header Data 불러오고 Binding
        /// </summary>
        private bool SearchExportHeader(string LotID, SmartSpreadSheet spreadsheet, bool isDiffLot = false, int CurrentSheetIndex = 0)
        {
            //LotID, 제품정의ID, 제품정의버전, RevisionNo
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                {"PLANTID",CurrentDataRow["PLANTID"].ToString() },
                {"LOTID",LotID },
                {"PRODUCTDEFID",CurrentDataRow["PRODUCTDEFID"].ToString() },
                {"PRODUCTDEFVERSION",CurrentDataRow["PRODUCTDEFVERSION"].ToString() },
                {"REVISIONNO",CurrentDataRow["REVISION"].ToString() }
            };

            DataTable dtHeader = SqlExecuter.Query("SelectShipmentExportInspectionHeader", "10001", param);

            if (dtHeader.Rows.Count < 1) return false;

            int WorkSheetIndex = 0;
            //Mapping Header binding
            spreadsheet.BeginUpdate();
            try
            {
                for (int r = 0; r < dtHeader.Rows.Count; r++)
                {
                    WorkSheetIndex = dtHeader.Rows[r]["SHEETINDEX"].ToSafeInt32();

                    if (isDiffLot)
                    {
                        if (WorkSheetIndex != CurrentSheetIndex)
                            continue;
                    }
                    for (int c = 3; c < dtHeader.Columns.Count; c++)
                    {
                        //SHEETINDEX                0
                        //INSPECTORLANGTYPE         1
                        //INSPECTRESULTLANGTYPE     2
                        //3부터 Field : Values
                        string[] str = dtHeader.Rows[r][c].ToString().Split(':');
                        //Split한 배열이 2개가 아니라면 Mapping이 되어있는 Data가 아니란 소리
                        //2개 이하라면 주소값이 없거나 Data값이 없기때문에 Continue
                        if (str.Count() < 2) continue;
                        if (str[0] != string.Empty)
                        {
                            if (dtHeader.Columns[c].ColumnName == "INSPECTOR")
                            {
                                if (str[1] == "")
                                    str[1] = UserInfo.Current.Name;
                            }
                            spreadsheet.Document.Worksheets[dtHeader.Rows[r]["SHEETINDEX"].ToSafeInt32()].Range[str[0]].SetValueFromText(str[1]);
                            SetCellValueToGrid(str[0], dtHeader.Rows[r]["SHEETINDEX"].ToSafeInt32(), str[1]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _msg.ShowError(ex);
            }
            spreadsheet.EndUpdate();
            return true;
        }

        /// <summary>
        /// 계측,신뢰성 Data 불러오고 Binding
        /// </summary>
        private bool SearchExportMESInpectionData(string LotID, SmartSpreadSheet spreadsheet, bool isReload = false, bool isDiffLotID = false, int CurrentSheetIndex = 0)
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                {"PLANTID",CurrentDataRow["PLANTID"].ToString() },
                {"RESOURCEID",LotID },
                {"PRODUCTDEFID",CurrentDataRow["PRODUCTDEFID"].ToString() },
                {"PRODUCTCLASSID",CurrentDataRow["PRODUCTCLASSID"].ToString() },
                {"PRODUCTDEFVERSION",CurrentDataRow["PRODUCTDEFVERSION"].ToString() },
                {"REVISIONNO",cboExportRev.EditValue }
            };

            DataTable dtSPC = SqlExecuter.Query("SelectShipmentExportInspectionData", "10001", param); 
            DataTable dtMapping = SqlExecuter.Query("SelectShipmentExportInspectionSPCMapping", "10001", param);

            if (dtMapping.Rows.Count == 0) return false;

            spreadsheet.BeginUpdate();
            try
            {
                foreach (DataRow drMapping in dtMapping.Rows)
                {
                    int intSheetIndex = drMapping["SHEETINDEX"].ToSafeInt32() - 1;
                    if (isDiffLotID)
                    {
                        if (CurrentSheetIndex != intSheetIndex)
                            continue;
                    }
                    //samplesize(매핑 기준정보)
                    int intSampleSize = drMapping["SAMPLESIZE"].ToSafeInt32();
                    //x축 시작
                    int intColStart = GetExcelColumnIndex(drMapping["COLUMNSTART"].ToString());
                    //x축 종료
                    int intColEnd = GetExcelColumnIndex(drMapping["COLUMNEND"].ToString());
                    //y축 시작
                    int intRowStart = drMapping["ROWSTART"].ToSafeInt32();
                    //y축 종료
                    int intRowEnd = drMapping["ROWEND"].ToSafeInt32();
                    //바인딩 시 Cell 간격
                    int intColSpan = drMapping["COLUMNSPAN"].ToSafeInt32();
                    //바인딩 시 Row 간격
                    int intRowSpan = drMapping["ROWSPAN"].ToSafeInt32();
                    //이미지 여부
                    bool isImage = drMapping["ISIMAGE"].Equals("Y") ? true : false;
                    //이미지 사이즈 (컬럼)
                    int intImageColSize = drMapping["IMAGECOLUMNRANGE"].ToSafeInt32();
                    //이미지 사이즈 (행)
                    int intImageRowSize = drMapping["IMAGEROWRANGE"].ToSafeInt32();
                    //이미지 비율
                    bool isImageRatio = drMapping["ISKEEPRATIO"].Equals("Y") ? true : false;
                    //세로 바인딩 여부
                    bool isVertical = drMapping["ISVERTICALBIND"].Equals("Y") ? true : false;
                    //측정값 / 측정시간 출력 구분
                    bool isInspectionDate = drMapping["ISINSPECTIONTIME"].Equals("Y") ? true : false;

                    //GDATA 관련 변수
                    //GData 사용 여부
                    bool UseGData = false;// drMapping["USEGDATA"].Equals("Y") ? true : false;
                    //목표값
                    double TargetValue = drMapping["TARGETVALUE"].ToSafeDoubleZero();
                    TargetValue = TargetValue * 1000.0;
                    //보정값
                    double MeasuringCorrectionValue = drMapping["MEASURINGCORRECTION"].ToSafeDoubleZero();
                    //반올림
                    int RoundingOff = drMapping["ROUNDINGOFF"].ToSafeInt32();

                    //총 시료수
                    int intDataCount = 0;
                    int intSampleNo = 0;

                    int mergedCount = 0;
                    bool isGDataValue = false;
                    //GDATA 생성 Param (목표값, 소수값, 생성개수, 반올림)
                    double[] dblGDataValue = new Commons.SPCLibrary.SPCPdf().NORMSINVary(TargetValue, MeasuringCorrectionValue, intSampleSize + 1, RoundingOff);
                    //생성해서 빈 공간에 순차적으로 채울것
                    var Values = dtSPC.AsEnumerable().Where(w =>
                                                                 w.Field<string>("INSPITEM").Equals(drMapping["INSPITEMID"].ToString())
                                                              && w.Field<string>("INSPECTIONCLASSID").Equals(drMapping["INSPECTIONCLASSID"].ToString())
                                                              && w.Field<string>("INSPECTIONMETHODID").Equals(drMapping["INSPECTIONMETHODID"].ToString())
                                                              && w.Field<string>("SHIPMENTINSPECTIONTYPE").Equals(drMapping["SHIPMENTINSPECTIONTYPE"].ToString())
                                                              && w.Field<int>("SAMPLENO") <= intSampleSize.ToDecimal()
                                                                 ).OrderBy(o => o.Field<int>("SAMPLENO"));

                    int ResultNGcnt = Values.AsEnumerable().Where(w => w.Field<string>("INSPECTIONRESULT").Equals("NG")).Count();

                    for(int valueCount = 0; valueCount < intSampleSize; valueCount++)
                    {
                        //GDATA 사용 안할 시 (사용시 주석처리 할것)-------------------------------/
                        if (valueCount == Values.Count())
                            break;
                        DataRow dtValue = Values.CopyToDataTable().Rows[valueCount];
                        //------------------------------------------------------------------------/

                        /* GDATA / G-DATA 사용시
                        isGDataValue = false;
                        if (!UseGData)
                        {
                            if (valueCount == Values.Count())
                                break;
                        }
                        DataRow dtValue = dtSPC.NewRow();
                        //현재 바인딩 대상 이 측정값 SEQ보다 큰 경우 G-DATA를 생성해서 채운다.
                        if (valueCount + 1 > Values.Count() && UseGData)
                        {
                            dtValue["VALUE"] = dblGDataValue[valueCount + 1];
                            isGDataValue = true;
                            dtValue["FILEID"] = string.Empty;
                        }
                        else
                        {
                            if(valueCount + 1 <= Values.Count())
                            {
                                if (UseGData)
                                {
                                    //현재 바인딩 대상 샘플번호가 전체 샘플수량보다 적을 때 NG인 경우 G-DATA로 대체한다.
                                    dtValue = Values.CopyToDataTable().Rows[valueCount];
                                    //측정값 중 NG가 하나라도 있는 경우 전체 G-Data로 대체한다.
                                    if (ResultNGcnt > 0)
                                    {
                                        dtValue["VALUE"] = dblGDataValue[valueCount + 1];
                                        isGDataValue = true;
                                    }
                                    /*
                                    //측정 값 중 NG인 경우 G-Data로 대체한다.
                                    if(dtValue["INSPECTIONRESULT"].ToSafeString() != "OK" && UseGData)
                                    {
                                        dtValue["VALUE"] = dblGDataValue[valueCount + 1];
                                        isGDataValue = true;
                                    }
                                    */
                        /*
                    }
                    else
                        dtValue = Values.CopyToDataTable().Rows[valueCount];
                }
                else
                {
                    if (!UseGData)
                    {
                        dtValue = Values.CopyToDataTable().Rows[valueCount];
                        break;
                    }   
                    dtValue["VALUE"] = dblGDataValue[valueCount + 1];
                    isGDataValue = true;
                    dtValue["FILEID"] = string.Empty;
                }
            }
            */
                        //세로가로에 따라 증가해줘야하는 값이 다름
                        //바인딩 ColIndex
                        int row = 0;
                        //바인딩 RowIndex
                        int col = 0;
                        //세로출력 시
                        if (isVertical)
                        {
                            //ex) Y축 바인딩 Index + 시료순번 + 머지된 행 수
                            row = intRowStart + (intSampleNo) - 1 + mergedCount;//(isImage ? 0 : mergedCount);
                            //ex) X축 바인딩 Index
                            col = (intColStart - 1);
                        }
                        else
                        {
                            row = intRowStart - 1;
                            col = (intColStart + (intSampleNo) - 1) + mergedCount;// (isImage ? 0 : mergedCount);
                        }


                        //가로 : x축 + 바인딩샘플카운트 만큼 순서대로 바인딩
                        //세로 : y축 + 바인딩샘플카운트 만큼 순서대로 바인딩
                        if (isImage && dtValue["FILEID"].ToString() != string.Empty)
                        {
                            try
                            {
                                //이미지 다운로드 
                                Worksheet wsheet = spreadsheet.Document.Worksheets[intSheetIndex];
                                SetImage(wsheet, Bitmap.FromStream(GetStreamFromWeb(dtValue["FILEPATH"].ToString())) as Bitmap, wsheet.Cells[row, col], intImageColSize, intImageRowSize, isImageRatio);
                                SetCellValueToGrid(wsheet.Cells[row, col].GetReferenceA1(), intSheetIndex, dtValue["FILEPATH"].ToString(), false, true, intImageColSize, intImageRowSize, (long)dtValue["FILESIZE"].ToSafeDoubleZero());
                            }
                            catch { }
                        }
                        else if (!isImage)
                        {
                            //측정시간 출력시 셀 서식 변경
                            if (isInspectionDate)
                                spreadsheet.Document.Worksheets[intSheetIndex].Cells[row, col].NumberFormat = "yyyy-MM-dd HH:mm:ss";

                            spreadsheet.Document.Worksheets[intSheetIndex].Cells[row, col].SetValueFromText(isInspectionDate ? dtValue["INSPECTIONDATE"].ToString() : dtValue["VALUE"].ToString());

                            if (isGDataValue)
                            {
                                SetCellValueToGrid(spreadsheet.Document.Worksheets[intSheetIndex].Cells[row, col].GetReferenceA1(), intSheetIndex, dtValue["VALUE"].ToString(), true);
                            }
                            else
                            {
                                SetCellValueToGrid(spreadsheet.Document.Worksheets[intSheetIndex].Cells[row, col].GetReferenceA1(), intSheetIndex, isInspectionDate ? dtValue["INSPECTIONDATE"].ToString() : dtValue["VALUE"].ToString(), false);
                            }
                            //첨부 파일이 엑셀일 경우 해당 엑셀의 Sheet를 Copy하여 현 Sheet에 Insert 한다.
                            if (new Regex(@"(.xlsm|.xlsx|.xls)").IsMatch(dtValue["FILEPATH"].ToString()))
                            {
                                try
                                {
                                    using (Stream stream = GetStreamFromWeb(dtValue["FILEPATH"].ToString()))
                                    {
                                        DevExpress.XtraSpreadsheet.SpreadsheetControl sht = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
                                        sht.LoadDocument(stream);
                                        //sht.LoadDocument(@"D:\(13)PWB-C215-A(A0).xlsx");
                                        for (int i = 0; i < sht.Document.Worksheets.Count; i++)
                                        {
                                            Worksheet wsht = sht.Document.Worksheets[i];
                                            //똑같은 이름의 worksheet가 있을경우 기존 worksheet 이름 + Guid를 붙여서 추가해준다.
                                            string strShtName = wsht.Name;
                                            try { spreadsheet.Document.Worksheets.Add(strShtName); }
                                            catch { spreadsheet.Document.Worksheets.Add(strShtName = wsht.Name + Guid.NewGuid().ToString().Substring(0, 6)); }
                                            spreadsheet.Document.Worksheets[strShtName].CopyFrom(wsht);
                                        }
                                        sht.Document.Dispose();
                                        sht.Dispose();
                                    }
                                }
                                catch { }
                            }
                        }
                        intSampleNo++;

                        //가로 : 컬럼 시작 증가
                        //세로 : 로우 시작 증가
                        //세로출력시
                        if (isVertical)
                        {
                            intRowStart = intRowStart + (intRowSpan - 1);// + (intImageRowSize - 1);
                            //바인딩 Range의 마지막 주소값인 경우 다음열로 넘어가도록 컬럼 + 1 후 행 주소 초기화
                            if (intRowStart + intSampleNo > intRowEnd)
                            {
                                //바인딩샘플카운트 초기화
                                intSampleNo = 0;
                                intRowStart = this.GetExcelColumnIndex(drMapping["ROWSTART"].ToString());
                                intColStart = intColStart + intColSpan;// + intImageColSize -1;
                                if (intColStart > intColEnd) break;
                            }
                        }
                        else
                        {
                            intColStart = intColStart + (intColSpan - 1);// + (intImageColSize - 1);
                            //바인딩 Range의 마지막 주소값인 경우 다음행으로 넘어가도록 행 + 1 후 컬럼 주소 초기화
                            if (intColStart + intSampleNo > intColEnd)
                            {
                                //바인딩샘플카운트 초기화
                                intSampleNo = 0;
                                intColStart = this.GetExcelColumnIndex(drMapping["COLUMNSTART"].ToString());
                                intRowStart = intRowStart + intRowSpan;// + intImageRowSize - 1;
                                if (intRowStart > intRowEnd) break;
                            }
                            //spreadsheet.Document.Worksheets[intSheetIndex].MergeCells(spreadsheet.Document.Worksheets[intSheetIndex].Range[GetExcelColumnName(intColStart.ToString()) + intRowStart.ToString() + ":" + GetExcelColumnName((intColEnd + mergedCount).ToString()) + intRowEnd.ToString()]);

                        }
                        //현재 바인딩 한 셀의 Merge 된 컬럼 구하기
                        IList<Range> cells = spreadsheet.Document.Worksheets[intSheetIndex].Range[GetExcelColumnName(col.ToString()) + (row + 1).ToString() + ":" + GetExcelColumnName((col).ToString()) + (row + 1).ToString()].GetMergedRanges();
                        //**Merge된 행/열의 경우 다음 순서 바인딩 시 건너뛰어야 하기 때문에 로직에 반영
                        if (cells.Count > 0)
                        {
                            if (isVertical)
                                mergedCount += cells[0].RowCount - 1;
                            else
                                mergedCount += cells[0].ColumnCount - 1;
                        }
                        else
                            mergedCount = 0;
                        
                        //총 시료수 체크
                        intDataCount++;
                        //총 시료수(실제값) == 총 시료수(매핑) 같을 시 바인딩 종료. 
                        if (intDataCount == intSampleSize)
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                _msg.ShowError(ex);
            }
            spreadsheet.EndUpdate();
            return true;
        }
        private void GetWriteData()
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                {"LOTID",CurrentDataRow["LOTID"].ToString() },
                {"REVISIONNO",cboExportRev.EditValue.ToString() }
            };

            DataTable dt = SqlExecuter.Query("SelectShipmentExportInspectionWriteData", "10001", param);

            if (_grdExportData == null) _grdExportData = new SmartBandedGrid();

            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn {ColumnName = "FILENAME", DataType = typeof(string), DefaultValue = string.Empty },
                new DataColumn {ColumnName = "LOCALFILEPATH", DataType = typeof(string), DefaultValue = string.Empty },
                new DataColumn {ColumnName = "STATE", DataType = typeof(string), DefaultValue = string.Empty }
            });

            _grdExportData.DataSource = dt;
        }
        /// <summary>
        /// 수기작성 Data 불러오고 Binding
        /// </summary>
        private void SearchExportWriteData()
        {
            DataTable dt = (DataTable)_grdExportData.DataSource;

            if (dt.Rows.Count < 1) return;
            strTxnHISTKey = dt.Rows[0][0].ToString();

            //수기Data가 없지만 TXNHISTKEY가 있을 경우
            if ((dt.Rows.Count == 1 && dt.Rows[0]["EXCELFIELD"].ToString() == string.Empty))
            {
                dt.Rows.RemoveAt(0);
                return;
            }

            shtExport.BeginUpdate();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    if (item["ISIMAGE"].ToString().ToUpper() == "Y")
                    {
                        try
                        {
                            SetImage(shtExport.Document.Worksheets[item["SHEETINDEX"].ToSafeInt32()], Bitmap.FromStream(GetStreamFromWeb(item["FTPFILEPATH"].ToString())) as Bitmap, shtExport.Document.Worksheets[item["SHEETINDEX"].ToSafeInt32()].Cells[item["EXCELFIELD"].ToString()], item["IMAGECOLUMNRANGE"].ToSafeInt32(), item["IMAGEROWRANGE"].ToSafeInt32());
                        }
                        catch { }
                    }
                    else
                    {
                        if (item["SHEETINDEX"].ToSafeInt32() > shtExport.Document.Worksheets.Count - 1)
                            continue;
                        shtExport.Document.Worksheets[item["SHEETINDEX"].ToSafeInt32()].Cells[item["EXCELFIELD"].ToString()].SetValueFromText(item["VALUE"].ToString());
                    }
                }
            }
            catch(Exception ex)
            {
                _msg.ShowError(ex);
            }
            shtExport.EndUpdate();
        }
        #endregion

        /// <summary>
        /// //@@네트워크 경로 변경
        /// </summary>
        /// <param name="webPath"></param>
        /// <returns></returns>
        private System.IO.Stream GetStreamFromWeb(string FileName)
        {
            System.IO.Stream Stream;
            using (System.Net.WebClient web = new System.Net.WebClient())
            {
                string ftpServerUserId = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Id"));
                string ftpServerPassword = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Password"));
                string ServerPath = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Url")) ;
                ServerPath = ServerPath + ((ServerPath.EndsWith("/")) ? "" : "/") + FileName;
                web.Credentials = new System.Net.NetworkCredential(ftpServerUserId, ftpServerPassword);
                Stream = web.OpenRead(ServerPath);
            }
            return Stream;
        }
        
        /// <summary>
        /// 이미지 Load
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="image"></param>
        /// <param name="TargetCell"></param>
        /// <param name="ColSpan"></param>
        /// <param name="RowSpan"></param>
        private void SetImage(Worksheet worksheet, Image image, Cell TargetCell, int ColSize, int RowSize , bool IsRatio = false)
        {
            try
            {
                /*
                //퍼센트 0.8 or 0.5 ..
                float nPercent = ((float)0.8 / 100);

                //넓이와 높이
                int OriginalWidth = image.Width;
                int OriginalHeight = image.Height;

                //조절될 퍼센트 계산
                int adjustWidth = (int)(OriginalWidth * nPercent);
                int adjustHeight = (int)(OriginalHeight * nPercent);

                Bitmap objBitmap = new Bitmap(image, new Size(adjustWidth, adjustHeight));
                */
                Bitmap objBitmap = new Bitmap(image, new Size(720, 540));

                float ColWidth = 0.0F;
                float RowHeight = 0.0F;
                Picture picture = worksheet.Pictures.AddPicture(objBitmap, TargetCell, true);

                //사진 비율 유지
                picture.LockAspectRatio = IsRatio;

                IList<Range> mergedcells = worksheet.Range[TargetCell.GetReferenceA1()].GetMergedRanges();

                if (objBitmap != null)
                {
                    //Col, Row 사이즈 만큼 각 Cell의 높이와 너비를 각가 구함
                    for (int i = 0; i < ColSize; i++)
                    {
                        //Cell의 너비를 더함
                        ColWidth += (float)TargetCell.ColumnWidth;
                        //Col 사이즈만큼 Cell의 Col을 1씩 이동
                        TargetCell = worksheet.Cells[TargetCell.RowIndex, TargetCell.ColumnIndex + 1];
                    }
                    for (int i = 0; i < RowSize; i++)
                    {
                        //Cell의 높이를 더함
                        RowHeight += (float)TargetCell.RowHeight;
                        //Row 사이즈만큼 Cell의 Row를 1씩 이동
                        TargetCell = worksheet.Cells[TargetCell.RowIndex + 1, TargetCell.ColumnIndex];
                    }
                    picture.Width = ColWidth;// * mergedcells[0].ColumnCount;
                    picture.Height = RowHeight;// * mergedcells[0].RowCount;
                }
            }
            catch (Exception ex)
            {
                _msg.ShowError(ex);
            }
        }

        /// <summary>
        /// 스프레드 시트의 컬럼 인덱스를 문자 주소로 변경
        /// </summary>
        /// <param name="ColumnIndex">Column Index</param>
        /// <returns>컬럼 인덱스 문자값(1 -> A, 2 -> B, 27 -> AA~)</returns>
        private string GetExcelColumnName(string ColumnIndex)
        {
            int CheckIndex = 0;
            if (!Int32.TryParse(ColumnIndex.ToString(), out CheckIndex))
                return ColumnIndex.ToString();

            CheckIndex = Convert.ToInt32(ColumnIndex);

            int ColumnNo = CheckIndex + 1;
            string ColumnName = String.Empty;
            int Modulo;

            while (ColumnNo > 0)
            {
                Modulo = (ColumnNo - 1) % 26;
                ColumnName = Convert.ToChar(65 + Modulo).ToString() + ColumnName;
                ColumnNo = (int)((ColumnNo - Modulo) / 26);
            }

            return ColumnName;
        }
        /// <summary>
        /// 컬럼 좌표값 변환
        /// </summary>
        /// <param name="CulumnName"></param>
        /// <returns></returns>
        private int GetExcelColumnIndex(string CulumnName)
        {
            CulumnName = CulumnName.ToUpperInvariant();

            int sum = 0;
            if (int.TryParse(CulumnName, out sum))
            {
                return sum;
            }

            for (int i = 0; i < CulumnName.Length; i++)
            {
                sum *= 26;
                sum += (CulumnName[i] - 'A' + 1);
            }

            return sum;
        }
        #endregion
    }
}