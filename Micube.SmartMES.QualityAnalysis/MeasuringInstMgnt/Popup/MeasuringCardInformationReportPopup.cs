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
using DevExpress.XtraPrinting;
using Micube.Framework;
using SmartDeploy.Common;
using Micube.SmartMES.Commons.Controls;
using DevExpress.Spreadsheet;
using Micube.Framework.Log;
using Micube.Framework.Net;
using Micube.SmartMES.Commons;
#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질 > 레칭레이트 > 에칭레이트 측정값 등록
    /// 업  무  설  명  : 에칭레이트 측정값 등록 화면의 이상발생 그리드를 더블 클릭하여 재 측정값을 관리한다.
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-07-17
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class MeasuringCardInformationReportPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
   
        #region Interface

        public DataRow CurrentDataRow { get; set; }
        public Image currentImage;
        #endregion

        #region Local Variables
        private DataTable _rnrDataTable = null;
        private string _result = "";
        private string _serialNo = "";
        private string _controlNo = "";
        private DateTime _measureDate = DateTime.Now;
        private string _processsegmentId = "";
        private string _processsegmentVersion = "";
        private string _equipmentId = "";
        private string _equipmentVersion = "";

        #endregion

        #region 생성자
        public MeasuringCardInformationReportPopup()
        {
            InitializeComponent();
            InitializeGrid();
            InitializeEvent();
        }
        #endregion

        #region 컨텐츠 영역 초기화
        private void InitializeGrid()
        {

        }

        #endregion

        #region Event
        private void InitializeEvent()
        {

            //Load += (s, e) =>
            //{
            //};
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            //btnCancel.Click += (s, e) =>
            //{
            //    this.DialogResult = DialogResult.Cancel;
            //    this.Close();
            //};
        }

        private void MeasuringCardInformationReportPopup_Load(object sender, EventArgs e)
        {
            ////smartSpreadSheet1.LoadDocument(Properties.Resources.InstrumentationDocument019);
            //shtRNR.LoadDocument(@"C:\60.Source\SmartMES\src\Micube.SmartMES.SPC\Resources\InstrumentationDocument019.xls");
            //sheetClear();

        }
        public void InitializeReport()
        {
            string ftpFilePath = Format.GetString(CurrentDataRow["CARDFILEPATH"]);
            string fileName = Format.GetString(CurrentDataRow["CARDSAFEFILENAME"]);
            //DialogManager.ShowWaitArea(this.pnlMain);
            //shtCardList.LoadDocument(Properties.Resources.MeasuringInstCardList01);
            shtCardList.LoadDocument(CommonFunction.GetFtpImageFileToByte(ftpFilePath, fileName));
            //shtCardList.LoadDocument(@"C:\60.Source\95.계측기 양식\MeasuringInstCardList01.xls");
            sheetClear();
            DialogManager.CloseWaitArea(this.pnlMain);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {   /*
            string messageData = "작업을 종료하겠습니까?";
            if (this.ShowMessage(MessageBoxButtons.YesNo, messageData) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            */
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion

        #region Private Function


        #endregion

        #region Public Function
        private void sheetClear()
        {
            string cardData = "";
            bool isDataCancel = false;
            try
            {
                //DB 계측기 입력자료 조회.
                //this.OnSearchMeasuringRNRData();

                IWorkbook workbook = shtCardList.Document;
                Worksheet worksheet = workbook.Worksheets[0];
                for (int i = 0; i < worksheet.Comments.Count; i++)
                {
                    string memoCoordinatesReference = worksheet.Comments[i].Reference.ToString();
                    string memoCoordinatesText = worksheet.Comments[i].Text.ToString();

                    char[] delimiterChars_sym1 = { '#' };
                    string[] strDtcAry;
                    strDtcAry = memoCoordinatesText.Split(delimiterChars_sym1);
                    if (strDtcAry != null && strDtcAry.Length > 0)
                    {
                        isDataCancel = false;
                        switch (strDtcAry[0].ToString())
                        {
                            case "IMAGE":
                                isDataCancel = true;
                                this.ImageInsert(workbook, worksheet, currentImage, memoCoordinatesReference);
                                break;
                            case "INFO":
                                cardData = readGroupData(strDtcAry[1].ToString());
                                break;
                            case "DATA":
                                if (_rnrDataTable != null && _rnrDataTable.Rows.Count > 0)
                                {
                                    //rnrData = readMeasuredData(strDtcAry);
                                    cardData = "";
                                }
                                else
                                {
                                    cardData = "";
                                }
                                break;
                            default:
                                break;
                        }
                    }

                    if (!isDataCancel)
                    {
                        worksheet.Cells[memoCoordinatesReference].SetValueFromText(cardData);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

        }

        /// <summary>
        /// Sheet에 Image 입력.
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="worksheet"></param>
        /// <param name="image"></param>
        /// <param name="cellRangeValue"></param>
        private void ImageInsert(IWorkbook workbook, Worksheet worksheet, Image image, string cellRangeValue)
        {
            try
            {
                if (image != null)
                {
                    //Image image1 = Image.FromFile(@"C:\60.Source\97.image\Seyung.PNG");
                    var src = SpreadsheetImageSource.FromImage(image);
                    //string cellRangeValue = this.txtImageLocation.Text;
                    if (cellRangeValue != null && cellRangeValue != "")
                    {
                        var targetCell = worksheet.Cells[cellRangeValue];
                        var picture = worksheet.Pictures.AddPicture(src, targetCell, true);
                        picture.Move(150, 30);
                        //picture.Placement = Placement.MoveAndSize;
                        picture.Width = 100; // (850 / 150) * 100;
                        //picture.Height = 1285;
                        picture.Height = 900;

                        //picture.Width = picture.OriginalWidth;
                        //picture.Height = picture.OriginalHeight;
                        //picture.Placement = Placement.Move;
                        //picture.TopLeftCell.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
                        //picture.TopLeftCell.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                    }
                }
                //Comment comment = worksheet.SelectedComment;
            }
            catch (Exception ex)
            {
               
            }

        }

        /// <summary>
        /// Group 및 Header 자료 조회
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string readGroupData(string id)
        {
            string returnData = "";

            try
            {
                if (CurrentDataRow != null)
                {
                    returnData = CurrentDataRow[id].ToString();
                }
            }
            catch (Exception ex)
            {
                returnData = id;
              
            }

            return returnData;
        }
        /// <summary>
        /// 측정자료 조회.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string readMeasuredData(string[] data)
        {
            string returnData = "";

            try
            {
                var rowDatas = _rnrDataTable.AsEnumerable();
                var query = from r in rowDatas
                            where ((r.Field<string>("PARTTYPE") == data[1])
                                && (r.Field<string>("PARTSEQ") == data[2])
                                && (r.Field<string>("MEASURENO") == data[3])
                                )
                            select r;
                foreach (var rw in query)
                {
                    returnData = rw["MEASUREVALUE"].ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return returnData;
        }
        /// <summary>
        /// 계측기 R&R 측정자료 조회.
        /// </summary>
        private void OnSearchMeasuringRNRData()
        {
            DataTable rnrDataTable = null;
            try
            {
                if (CurrentDataRow != null)
                {
                    _serialNo = CurrentDataRow["SERIALNO"].ToString();
                    _controlNo = CurrentDataRow["CONTROLNO"].ToString();
                    _measureDate = DateTime.Parse(CurrentDataRow["MEASUREDATE"].ToString());
                    _processsegmentId = CurrentDataRow["PROCESSSEGMENTID"].ToString();
                    _processsegmentVersion = CurrentDataRow["PROCESSSEGMENTVERSION"].ToString();
                    if (_processsegmentVersion == null || _processsegmentVersion == "")
                    {
                        _processsegmentVersion = "*";
                    }
                    _equipmentId = CurrentDataRow["EQUIPMENTID"].ToString();
                    _equipmentVersion = "#";//sia확인 : 버전 처리
                }
                else
                {
                    return;
                }

                //DataRow rowMain = this.grdMain.View.GetFocusedDataRow();
                //this.grdSub.View.SetFocusedRowCellValue("PROCESSSEGMENTID", rowMain["PROCESSSEGMENTID"]);
                //string serialNo = rowMain["SERIALNO"].ToSafeString();
                //string controlNo = rowMain["CONTROLNO"].ToSafeString();

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("P_PLANTID", UserInfo.Current.Plant);
                param.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
                param.Add("P_SERIALNO", _serialNo);
                param.Add("P_CONTROLNO", _controlNo);
                param.Add("P_MEASUREDATE", _measureDate.ToString("yyyy-MM-dd"));
                param.Add("P_PROCESSSEGMENTID", _processsegmentId);
                param.Add("P_PROCESSSEGMENTVERSION", _processsegmentVersion);

                //SqlQuery rnrUserList = new SqlQuery("GetAreaidListByCsm", "10001", $"P_PLANTID={UserInfo.Current.Plant}", $"P_AREANAME={UserInfo.Current.Area}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
                SqlQuery rnrDataList = new SqlQuery("SelectRawMeasuringRNRData", "10001", param);
                //DataTable rnrDataTable = rnrDataList.Execute();
                rnrDataTable = rnrDataList.Execute();
                if (rnrDataTable != null && rnrDataTable.Rows.Count > 0)
                {
                    _rnrDataTable = rnrDataTable;
                }
                else
                {
                    _rnrDataTable = null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

        }
        private void btnSearchData_Click(object sender, EventArgs e)
        {
            IWorkbook workbook = shtCardList.Document;
            Worksheet worksheet = workbook.Worksheets[0];
            for (int i = 0; i < worksheet.Comments.Count; i++)
            {
                string memoCoordinates = worksheet.Comments[i].Reference.ToString();
                worksheet.Cells[memoCoordinates].SetValueFromText(memoCoordinates);
            }
            //Cell cell = worksheet.Cells["A0"];
            //Add a comment to a cell. 
            //Comment comment = worksheet.Comments.Add(cell, workbook.CurrentAuthor, "Comment Text");
            Comment comment = worksheet.SelectedComment;
            //worksheet.Comments.InnerList.Items[0].Text
            //worksheet.Comments[0].
            //(new System.Collections.Generic.Mscorlib_CollectionDebugView<DevExpress.XtraSpreadsheet.API.Native.Implementation.NativeComment>(((DevExpress.XtraSpreadsheet.API.Native.Implementation.NativeCollectionBase<DevExpress.Spreadsheet.Comment, DevExpress.XtraSpreadsheet.API.Native.Implementation.NativeComment, DevExpress.XtraSpreadsheet.Model.Comment>)((DevExpress.XtraSpreadsheet.API.Native.Implementation.NativeWorksheet)worksheet).Comments).InnerList).Items[0]).Text
            //string dd = worksheet.SelectedComment.Text;
            //comment.Runs[0].Text = "요렇게~^^.,";

            // Format comment text as bold and italic. 
            //comment.Runs[0].Font.FontStyle = DevExpress.Spreadsheet.SpreadsheetFontStyle.BoldItalic;

        }

        #endregion

        /// <summary>
        /// Export 버튼 Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {

            SaveFileDialog opf = new SaveFileDialog();
                
            opf.FileName = @"Measuring R&R Report_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx"; //초기 파일명을 지정할 때 사용한다.
            opf.Filter = "Excel Files(*.xlsx)|*.xlsx;*.xls";
            opf.Title = "Save an Excel File";
        
            DevExpress.XtraPivotGrid.PivotXlsxExportOptions exOpt = new DevExpress.XtraPivotGrid.PivotXlsxExportOptions();
            exOpt.ExportType = DevExpress.Export.ExportType.WYSIWYG;
            exOpt.ShowGridLines = true;
                
            opf.ShowDialog();
            if (opf.FileName.Length > 0)
            {
                //pvProductPlan.ExportToXlsx(opf.FileName, exOpt);
                shtCardList.SaveDocument(opf.FileName.ToString());
            }
           
        }

        /// <summary>
        /// 입력값 저장.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            string dataType = "";
            string partType = "";
            string partSeq = "";
            string measureNo = "";
            double nMeasureValue = 0f;
            string tempMeasureValue = "";
            string errorMessage = "";
            string validState = "";
            int isLogNo = 0;
            DataTable dtRNRData = CreateTableRNRData();
            try
            {
                IWorkbook workbook = shtCardList.Document;
                Worksheet worksheet = workbook.Worksheets[0];
                for (int i = 0; i < worksheet.Comments.Count; i++)
                {
                    string memoCoordinatesReference = worksheet.Comments[i].Reference.ToString();
                    string memoCoordinatesText = worksheet.Comments[i].Text.ToString();
                    //worksheet.Cells[memoCoordinatesReference].SetValueFromText("");
                    tempMeasureValue = worksheet.Cells[memoCoordinatesReference].DisplayText;
                    
                    char[] delimiterChars_sym1 = { '#' };
                    string[] strDtcAry; //경계 기호 자료 저장 배열 변수.
                    strDtcAry = memoCoordinatesText.Split(delimiterChars_sym1); //Check Char : "#"
                    if (strDtcAry != null && strDtcAry.Length > 0)
                    {
                        dataType = strDtcAry[0];
                        if (dataType.ToUpper() == "DATA")
                        {
                            //Index
                            partType = strDtcAry[1];
                            partSeq = strDtcAry[2];
                            measureNo = strDtcAry[3];
                            validState = "Y";

                            //측정값 Check
                            nMeasureValue = RNRNumericCheck(tempMeasureValue, out isLogNo, out errorMessage);
                            DataRow drData = dtRNRData.NewRow();
                            drData["SERIALNO"] = _serialNo;
                            drData["CONTROLNO"] = _controlNo;
                            drData["MEASUREDATE"] = _measureDate.ToString("yyyy-MM-dd");
                            drData["PROCESSSEGMENTID"] = _processsegmentId;
                            drData["PROCESSSEGMENTVERSION"] = _processsegmentVersion;
                            //Index
                            drData["PARTTYPE"] = partType;
                            drData["PARTSEQ"] = partSeq;
                            drData["MEASURENO"] = measureNo;
                            //Data
                            drData["MEASUREVALUE"] = nMeasureValue;
                            drData["VALIDSTATE"] = validState;
                            //숫자만 입력
                            if (isLogNo == 0)
                            {
                                //측정값 입력.
                                dtRNRData.Rows.Add(drData);
                            }
                            else
                            {
                                //isLogNo = 11001;//숫자가 아닌 자료 있음.
                            }

                        }
                    }
                }

                string messageId = "";
                switch (isLogNo)
                {
                    case 11001:
                        messageId = "SaveMeasurementRNR";
                            
                        break;
                    default:
                        messageId = "InfoSave";
                        break;
                }


                //this.DialogResult = DialogResult.OK;
                if (this.ShowMessage(MessageBoxButtons.YesNo, messageId) == DialogResult.Yes)
                {
                    int result = SaveData(dtRNRData);
                    this.ShowMessage("SuccessSave");
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();

                    //MessageWorker worker = new MessageWorker("SaveLotDefectMergeApproval");
                    //worker.SetBody(new MessageBody()
                    //{
                    //    { "approvalUser", UserInfo.Current.Id }, // 승인자
                    //    { "approvalDate", DateTime.Now }, // 승인일시   
                    //    { "approvalComment", memoApprovalComment.Text }, // 승인/반려 Comment
                    //    { "requestNo", CurrentDataRow["REQUESTNO"]}, // 요청번호
                    //    { "productDefId", CurrentDataRow["PRODUCTDEFID"] }, // 품목 ID
                    //    { "productDefVersion", CurrentDataRow["PRODUCTDEFVERSION"]}, // 품목 Version
                    //    { "approvalFlag", approvalFlag} // 승인/반려플래그
                    //});

                    //worker.Execute();

                    //this.ShowMessage("저장되었습니다."); // 병합 요청되었습니다.
                    //this.Close();
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

        }

        /// <summary>
        /// 계측기 Data 입력
        /// </summary>
        /// <param name="dtData"></param>
        /// <returns></returns>
        private int SaveData(DataTable dtData)
        {
            int returnData = 0;
            try
            {
                //Rule 입력/수정
                DataSet dsMeasuredData = new DataSet();
                dsMeasuredData.Tables.Add(dtData);

                ExecuteRule("SaveMeasuringInstRNRData", dsMeasuredData);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            return returnData;
        }

        /// <summary>
        /// double 형식으로 변환.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private double RNRNumericCheck(string data, out int status, out string errorMessage)
        {
            double result = -999999999;
            status = 11001; //숫자가 아닌 자료 있음.
            errorMessage = "";
            if (data == "")
            {
                status = 10002;
                errorMessage = "입력값 없음";//sia처리03 : 다국어 처리
                return result;
            }
            try
            {
                status = 0;
                result = Convert.ToDouble(data);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
            }

            return result;
        }
        /// <summary>
        /// 계측기 RNR 측정값 Data DataTable
        /// </summary>
        /// <returns></returns>
        private DataTable CreateTableRNRData()
        {
            DataTable dataTable = new DataTable();
            dataTable.TableName = "measuringRNRData";
            //PK
            dataTable.Columns.Add("SERIALNO", typeof(string));
            dataTable.Columns.Add("CONTROLNO", typeof(string));
            dataTable.Columns.Add("MEASUREDATE", typeof(DateTime));
            dataTable.Columns.Add("PARTTYPE", typeof(string));
            dataTable.Columns.Add("PARTSEQ", typeof(string));
            dataTable.Columns.Add("MEASURENO", typeof(string));
            dataTable.Columns.Add("PROCESSSEGMENTID", typeof(string));
            dataTable.Columns.Add("PROCESSSEGMENTVERSION", typeof(string));
            //Data
            dataTable.Columns.Add("MEASUREVALUE", typeof(double));
            //dataTable.Columns.Add("DESCRIPTION", typeof(string));
            //dataTable.Columns.Add("CREATOR", typeof(string));
            //dataTable.Columns.Add("CREATEDTIME", typeof(DateTime));
            //dataTable.Columns.Add("MODIFIER", typeof(string));
            //dataTable.Columns.Add("MODIFIEDTIME", typeof(DateTime));
            //dataTable.Columns.Add("LASTTXNHISTKEY", typeof(string));
            //dataTable.Columns.Add("LASTTXNID", typeof(string));
            //dataTable.Columns.Add("LASTTXNUSER", typeof(string));
            //dataTable.Columns.Add("LASTTXNTIME", typeof(DateTime));
            //dataTable.Columns.Add("LASTTXNCOMMENT", typeof(string));
            dataTable.Columns.Add("VALIDSTATE", typeof(string));
            
            return dataTable;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            using (PrintingSystem printingSystem = new PrintingSystem())
            {
                using (PrintableComponentLink link = new PrintableComponentLink(printingSystem))
                {
                    link.Component = this.shtCardList;
                    link.CreateDocument();
                    link.ShowPreviewDialog();
                }
            }
        }
    }//end class

    
}
