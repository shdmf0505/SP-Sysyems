#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
using DevExpress.XtraSpreadsheet;
using DevExpress.Spreadsheet;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace Micube.SmartMES.ToolManagement.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 치공구 의뢰서 출력
    /// 업  무  설  명  : 치공구 요청 의뢰서를 출력한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-10-15
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class PrintRequestToolDocument : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables
        public DataRow CurrentDataRow { get; set; }
        public DataTable _headerTable;
        public DataTable _toolInfoTable;
        public DataTable _durableSummaryTable;
        public delegate void PrintDataEventHandler(DataRow insertRow);
        public event PrintDataEventHandler SaveData;
        #endregion

        public PrintRequestToolDocument()
        {
            InitializeComponent();

            InitializeEvents();
        }

        #region HeaderDataTable - 헤더정보데이터테이블 property
        public DataTable HeaderDataTable
        {
            set { _headerTable = value; }
        }
        #endregion

        #region ToolInfoTable - 치공구정보 Property
        public DataTable ToolInfoTable
        {
            set { _toolInfoTable = value; }
        }
        #endregion

        #region DurableSummaryTable - Durable요약정보 Property
        public DataTable DurableSummaryTable
        {
            set { _durableSummaryTable = value; }
        }
        #endregion

        #region Event
        private void InitializeEvents()
        {
            Shown += PrintRequestFilmDocument_Shown;
            btnPrint.Click += BtnPrint_Click;
            btnExit.Click += BtnExit_Click;
            btnExport.Click += BtnExport_Click;
        }

        #region BtnExport_Click - 내보내기 버튼 클릭이벤트
        private void BtnExport_Click(object sender, EventArgs e)
        {
            DialogResult dgResult = fileSavor.ShowDialog();

            if(dgResult == DialogResult.OK)
            {
                dcDocument.SaveDocument(fileSavor.FileName, DocumentFormat.Xlsx);

                System.Diagnostics.Process.Start(fileSavor.FileName);
            }
        }
        #endregion

        #region PrintRequestFilmDocument_Shown - 화면로딩후 이벤트 - 출력 로직구현
        private void PrintRequestFilmDocument_Shown(object sender, EventArgs e)
        {
            //문서로드 en-US, ko-KR, vi-VN, zh-CN - 다국어 처리이전 각 다국어별 문서를 따로 출력했을 때의 소스
            //switch(UserInfo.Current.LanguageType)
            //{
            //    case "en-US":
            //        dcDocument.LoadDocument(global::Micube.SmartMES.ToolManagement.Properties.ToolResource.ToolReworkReport_eng);
            //        break;
            //    case "vi-VN":
            //        dcDocument.LoadDocument(global::Micube.SmartMES.ToolManagement.Properties.ToolResource.ToolReworkReport_vtn);
            //        break;
            //    case "zh-CN":
            //        dcDocument.LoadDocument(global::Micube.SmartMES.ToolManagement.Properties.ToolResource.ToolReworkReport_chn);
            //        break;
            //    case "ko-KR":
            //        dcDocument.LoadDocument(global::Micube.SmartMES.ToolManagement.Properties.ToolResource.ToolReworkReport);
            //        break;                
            //}

            dcDocument.LoadDocument(global::Micube.SmartMES.ToolManagement.Properties.ToolResource.ToolReworkReport);

            //로드후 작업시작
            if (_headerTable != null && _toolInfoTable != null && _durableSummaryTable != null)
            {
                IWorkbook workBook = dcDocument.Document;
                Worksheet workSheet = workBook.Worksheets[0];

                DataRow headerRow = _headerTable.Rows[0];

                InsertHeaderInfo(workSheet);

                InsertSingleValue(workSheet, "B7:C7", headerRow.GetString("CUSTOMERNAME"));
                InsertSingleValue(workSheet, "D7:G7", headerRow.GetString("PRODUCTDEFNAME"));
                InsertSingleValue(workSheet, "J7:K7", headerRow.GetString("PRODUCTDEFID"));
                InsertSingleValue(workSheet, "L7", headerRow.GetString("QTY"));

                int rowIndex = 11;
                int summaryIndex = 33; //아래쪽 Summary입력부분
                for(int index = 0; index < _toolInfoTable.Rows.Count; index++)
                {
                    if (index > 0)
                    {
                        AddNewRow(workSheet, rowIndex, 1);
                        PaintingCellBorder(workSheet, "B", "M", rowIndex + 1, rowIndex + 1, BorderLineStyle.Thin, Color.Black);
                        MergeCell(workSheet, "C", "D", rowIndex + 1, rowIndex + 1);
                        rowIndex++;
                        summaryIndex++;
                    }
                }

                rowIndex = 11;
                foreach(DataRow toolInfoRow in _toolInfoTable.Rows)
                {
                    InsertSingleValueCenterAlign(workSheet, "C" + rowIndex.ToString() + ":D" + rowIndex.ToString(), toolInfoRow.GetString("TOOLCATEGORY"));
                    InsertSingleValueCenterAlign(workSheet, "F" + rowIndex.ToString(), toolInfoRow.GetString("QTY"));
                    InsertSingleValueCenterAlign(workSheet, "H" + rowIndex.ToString(), toolInfoRow.GetString("USEDLIMIT"));
                    InsertSingleValueCenterAlign(workSheet, "I" + rowIndex.ToString(), ""); //실적타수
                    InsertSingleValueCenterAlign(workSheet, "J" + rowIndex.ToString(), ""); //연마횟수
                    InsertSingleValueCenterAlign(workSheet, "K" + rowIndex.ToString(), toolInfoRow.GetString("DELIVERYDATE"));
                    InsertSingleValueCenterAlign(workSheet, "M" + rowIndex.ToString(), toolInfoRow.GetString("TOOLMAKETYPE"));

                    rowIndex++;
                }

                //InsertSingleValue(workSheet, "B" + Convert.ToString(rowIndex), Language.GetMessage("REWORKTOOLMSG1").Message + "\r\n" + Language.GetMessage("REWORKTOOLMSG2").Message + "\r\n"
                //    + Language.GetMessage("REWORKTOOLMSG3").Message + "\r\n" + Language.GetMessage("REWORKTOOLMSG4").Message + "AA");
                InsertSingleValue(workSheet, "B" + Convert.ToString(rowIndex), "");

                int summaryInputIndex = summaryIndex; //아래쪽 Summary입력부분
                for (int index = 0; index < _durableSummaryTable.Rows.Count; index++)
                {
                    if (index > 0)
                    {
                        AddNewRow(workSheet, summaryInputIndex, 1);
                        PaintingCellBorder(workSheet, "B", "M", summaryInputIndex + 1, summaryInputIndex + 1, BorderLineStyle.Thin, Color.Black);
                        MergeCell(workSheet, "B", "C", summaryInputIndex + 1, summaryInputIndex + 1);
                        MergeCell(workSheet, "D", "E", summaryInputIndex + 1, summaryInputIndex + 1);
                        MergeCell(workSheet, "G", "H", summaryInputIndex + 1, summaryInputIndex + 1);
                        MergeCell(workSheet, "I", "J", summaryInputIndex + 1, summaryInputIndex + 1);
                        MergeCell(workSheet, "L", "M", summaryInputIndex + 1, summaryInputIndex + 1);
                        summaryInputIndex++;
                    }
                }

                foreach (DataRow summaryRow in _durableSummaryTable.Rows)
                {
                    InsertSingleValueCenterAlign(workSheet, "B" + summaryIndex.ToString() + ":C" + summaryIndex.ToString(), summaryRow.GetString("TOOLCATEGORY"));
                    InsertSingleValueCenterAlign(workSheet, "D" + summaryIndex.ToString() + ":E" + summaryIndex.ToString(), ""); //제작일자
                    InsertSingleValueCenterAlign(workSheet, "F" + summaryIndex.ToString(), summaryRow.GetString("QTY"));
                    InsertSingleValueCenterAlign(workSheet, "G" + summaryIndex.ToString() + ":H" + summaryIndex.ToString(), summaryRow.GetString("AREANAME"));
                    InsertSingleValueCenterAlign(workSheet, "I" + summaryIndex.ToString() + ":J" + summaryIndex.ToString(), summaryRow.GetString("PRODUCTDEFID"));
                    InsertSingleValueCenterAlign(workSheet, "K" + summaryIndex.ToString(), summaryRow.GetString("ISUSED").Equals("Y") ? "유" : "무");

                    summaryIndex++;
                }

                SaveData?.Invoke(CurrentDataRow);

                ShowMessage(MessageBoxButtons.OK, "CompletePrintReport", "");
            }
        }
        #endregion

        #region BtnExit_Click - 종료버튼 클릭이벤트
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        #endregion

        #region BtnPrint_Click - 인쇄버튼 클릭이벤트
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            //if (_reportTable != null)
            //{
            //    IWorkbook workBook = dcDocument.Document;
            //    Worksheet workSheet = workBook.Worksheets[0];

            //    if (_reportTable.Rows.Count > 1)
            //    {
            //        for (int i = 1; i < _reportTable.Rows.Count; i++)
            //        {
            //            Worksheet tempSheet = workBook.Worksheets.Add(_reportTable.Rows[i].GetString("DURABLEDEFID"));

            //            tempSheet.CopyFrom(workSheet);
            //        }
            //    }
            //    workSheet.Name = _reportTable.Rows[0].GetString("DURABLEDEFID");

            //    for(int i =0; i < _reportTable.Rows.Count; i++)
            //    {
            //        workSheet = workBook.Worksheets[i];

            //        InsertSingleValue(workSheet, "C5:E5", DateTime.Now.ToString("yyyyMMdd") + "000");
            //        InsertSingleValue(workSheet, "J6:L6", DateTime.Now.ToString("yyyy년 MM월 dd일"));

            //        InsertSingleValue(workSheet, "D8:G8", _reportTable.Rows[i].GetString("CUSTOMERNAME"));
            //        InsertSingleValue(workSheet, "I8:L8", _reportTable.Rows[i].GetString("PRODUCTDEFTYPE"));
            //        InsertSingleValue(workSheet, "D10:G10", _reportTable.Rows[i].GetString("DURABLEDEFID"));
            //        InsertSingleValue(workSheet, "I10:L10", _reportTable.Rows[i].GetString("DURABLEDEFNAME"));
            //        InsertSingleValue(workSheet, "D12:G12", _reportTable.Rows[i].GetString("DURABLEDEFVERSION"));
            //        InsertSingleValue(workSheet, "I12:L12", _reportTable.Rows[i].GetString("FILMUSELAYER"));
            //        InsertSingleValue(workSheet, "D14:G14", _reportTable.Rows[i].GetString("DELIVERYDATE"));
            //        InsertSingleValue(workSheet, "I14:L14", _reportTable.Rows[i].GetString("REQUESTUSER"));

            //        InsertSingleValue(workSheet, "D18:G18", _reportTable.Rows[i].GetString("TOOLMAKETYPE"));
            //        InsertSingleValue(workSheet, "I18:L18", _reportTable.Rows[i].GetString("QTY"));
            //        InsertSingleValue(workSheet, "D24:G24", _reportTable.Rows[i].GetString("USEDLIMIT"));
            //        InsertSingleValue(workSheet, "D28:L28", _reportTable.Rows[i].GetString("REQUESTCOMMENT"));
            //    }                

            //    //titleRange1.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            //    //titleRange1.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            //    //titleRange1.Borders.SetOutsideBorders(Color.Black, BorderLineStyle.Medium);

            //    //Formatting rangeFormatting = titleRange1.BeginUpdateFormatting();
            //    //rangeFormatting.Borders.TopBorder.LineStyle = BorderLineStyle.Dashed;

            //    //Table printTable = workSheet.Tables.Add(workSheet["B2:E8"], true);

            //    //printTable.Style = workBook.TableStyles[BuiltInTableStyleId.TableStyleMedium27];

            //    //// Access table columns and name them. 
            //    //TableColumn productColumn = table.Columns[0];
            //    //productColumn.Name = "Product";
            //    //TableColumn priceColumn = table.Columns[1];
            //    //priceColumn.Name = "Price";
            //    //TableColumn quantityColumn = table.Columns[2];
            //    //quantityColumn.Name = "Quantity";
            //    //TableColumn discountColumn = table.Columns[3];
            //    //discountColumn.Name = "Discount";
            //    //TableColumn amountColumn = table.Columns[4];
            //    //amountColumn.Name = "Amount";
            //    SaveData?.Invoke(CurrentDataRow);
            //}
        }
        #endregion
        #endregion

        #region Private Function
        #region InsertHeader
        private void InsertHeaderInfo(Worksheet workSheet)
        {
            InsertSingleValue(workSheet, "B5", "■" + Language.Get("PRODUCTINFORMATION"));
            InsertSingleValue(workSheet, "B6", Language.Get("TOOLREWORKBUYER"));
            InsertSingleValue(workSheet, "D6", Language.Get("MODELNAME"));
            InsertSingleValue(workSheet, "H6", Language.Get("PLANQTY"));
            InsertSingleValue(workSheet, "J6", Language.Get("PRODUCTDEFID"));
            InsertSingleValue(workSheet, "L6", Language.Get("ARRAY"));
            InsertSingleValue(workSheet, "M6", Language.Get("REMARK"));
            InsertSingleValue(workSheet, "B9", "■" + Language.Get("TOOLREWORKINFO"));
            InsertSingleValue(workSheet, "M9", Language.Get("TOOLREWORKDESCRIPTION"));
            InsertSingleValue(workSheet, "B10", Language.Get("CLASS"));
            InsertSingleValue(workSheet, "C10", Language.Get("TOOLREWORKTYPE"));
            InsertSingleValue(workSheet, "E10", Language.Get("TOOLREWORKMAKEDATE"));
            InsertSingleValue(workSheet, "F10", Language.Get("TOOLREWORKQTY"));
            InsertSingleValue(workSheet, "G10", Language.Get("TOOKREWORKRECEIPTVENDOR"));
            InsertSingleValue(workSheet, "H10", Language.Get("USEDLIMIT"));
            InsertSingleValue(workSheet, "I10", Language.Get("RESULTLIMIT"));
            InsertSingleValue(workSheet, "J10", Language.Get("TOTALCLEANCOUNT"));
            InsertSingleValue(workSheet, "K10", Language.Get("TOOLREWORKREQUESTRECEIPT"));
            InsertSingleValue(workSheet, "L10", Language.Get("TOOLREWORKRULERECEIPT"));
            InsertSingleValue(workSheet, "M10", Language.Get("TOOLREWORKADDTYPE"));
            InsertSingleValue(workSheet, "B12", "");
            InsertSingleValue(workSheet, "B15", "■" + Language.Get("TOOLREWORKADDREASONDETAIL"));
            InsertSingleValue(workSheet, "B16", "    ○" + Language.Get("TOOLREWORKCAPAADD"));
            InsertSingleValue(workSheet, "B17", Language.Get("TOOLREWORKPNLPCS"));
            InsertSingleValue(workSheet, "E17", Language.Get("TOOLREWORKADDPNLPCS"));
            InsertSingleValue(workSheet, "H17", Language.Get("TOOLREWORKPNLPCSQTY"));
            InsertSingleValue(workSheet, "K17", Language.Get("REMARK"));
            InsertSingleValue(workSheet, "B23", "    ○" + Language.Get("TOOLREWRKDETAILREASON"));
            InsertSingleValue(workSheet, "J23", Language.Get("DFFREMARKS"));
            InsertSingleValue(workSheet, "B24", Language.Get("TOOLREWORKREPAIR"));            
            InsertSingleValue(workSheet, "D24", Language.Get("TOOLREWORKBROKEN"));
            InsertSingleValue(workSheet, "D25", Language.Get("TOOLREWORKACTIONREPAIR"));
            InsertSingleValue(workSheet, "B26", Language.Get("TOOLREWORKCHANGEEQUIPMENT"));
            InsertSingleValue(workSheet, "D26", Language.Get("OVERUSEDLIMIT"));
            InsertSingleValue(workSheet, "D27", Language.Get("TOOLREWORKOVERCLEANCOUNT"));
            InsertSingleValue(workSheet, "B28", Language.Get("TOOLREWORKRECYCLE"));
            InsertSingleValue(workSheet, "B29", Language.Get("ETCPRODUCT"));

            InsertSingleValue(workSheet, "B31", "■" + Language.Get("TOOLREWORKADDSTATUS"));
            InsertSingleValue(workSheet, "B32", Language.Get("TOOLREWORKTYPE"));
            InsertSingleValue(workSheet, "D32", Language.Get("TOOLREWORKMAKEDATE"));
            InsertSingleValue(workSheet, "F32", Language.Get("TOOLREWORKADDQTY"));
            InsertSingleValue(workSheet, "G32", Language.Get("TOOLREWORKADDVENDOR"));
            InsertSingleValue(workSheet, "I32", Language.Get("PRODUCTDEFID"));
            InsertSingleValue(workSheet, "K32", Language.Get("TOOLREWORKISUSE"));
            InsertSingleValue(workSheet, "L32", Language.Get("REMARK"));

            InsertSingleValue(workSheet, "B35", "■" + Language.Get("TOOLREWORKAMOUNTBUYING"));
            InsertSingleValue(workSheet, "M35", Language.GetMessage("TOOLREWORKAMOUNTMSG").Message);
            InsertSingleValue(workSheet, "B36", Language.Get("TOOLREWORKBIDDINGVENDORCOUNT"));
            InsertSingleValue(workSheet, "G36", Language.Get("TOOLREWORKBIDDATE"));
            InsertSingleValue(workSheet, "K36", Language.Get("TOOLREWORKFREE"));
            InsertSingleValue(workSheet, "B37", Language.Get("TOOLREWORKSELECTVENDOR"));
            InsertSingleValue(workSheet, "G37", Language.Get("TOOLMAKETYPE"));
            InsertSingleValue(workSheet, "B38", Language.Get("TOOLREWORKTYPE"));
            InsertSingleValue(workSheet, "E38", Language.Get("QTYLABEL"));
            InsertSingleValue(workSheet, "F38", Language.Get("UNITPRICE"));
            InsertSingleValue(workSheet, "G38", Language.Get("TOOLREWORKMINAMOUNT"));
            InsertSingleValue(workSheet, "I38", Language.Get("TOOLREWORKLIMIT"));
            InsertSingleValue(workSheet, "J38", Language.Get("TOOLREWORKRECEIPTDATE"));
            InsertSingleValue(workSheet, "K38", Language.Get("REMARK"));
            InsertSingleValue(workSheet, "B47", Language.Get("SUM"));
            InsertSingleValue(workSheet, "B48", Language.Get("ATTACHEDFILE"));

            InsertSingleValue(workSheet, "B39", "");
            InsertSingleValue(workSheet, "B40", "");
            InsertSingleValue(workSheet, "B41", "");
            InsertSingleValue(workSheet, "B42", "");
            InsertSingleValue(workSheet, "B43", "");
            InsertSingleValue(workSheet, "B44", "");
            InsertSingleValue(workSheet, "B45", "");
            InsertSingleValue(workSheet, "B46", "");
        }
        #endregion

        #region InsertSingleValue : 데이터를 특정 셀에 입력한다.
        private void InsertSingleValue(Worksheet sheet, string rangeColumn, string dataValue)
        {
            Range inputRange = sheet[rangeColumn];

            inputRange.SetValue(dataValue);
        }
        private void InsertSingleValueCenterAlign(Worksheet sheet, string rangeColumn, string dataValue)
        {
            Range inputRange = sheet[rangeColumn];

            inputRange.SetValue(dataValue);
            inputRange.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
        }
        #endregion

        #region AddNewRow
        private void AddNewRow(Worksheet sheet, int rowIndex, int addCount)
        {
            sheet.Rows.Insert(rowIndex, addCount);
        }
        #endregion

        #region MergeCell
        private void MergeCell(Worksheet sheet, string startColumn, string endColumn, int startIndex, int endIndex)
        {
            Range cellRange = sheet[startColumn + startIndex.ToString() + ":" + endColumn + endIndex.ToString()];
            cellRange.Merge(MergeCellsMode.Default);
        }
        #endregion

        #region PaintingCellBorder
        private void PaintingCellBorder(Worksheet sheet, string startColumn, string endColumn, int startIndex, int endIndex, BorderLineStyle lineStyle, Color lineColor)
        {
            Range cellRange = sheet[startColumn + startIndex.ToString() + ":" + endColumn + endIndex.ToString()];
            Formatting cellFormat = cellRange.BeginUpdateFormatting();
            Borders cellBorders = cellFormat.Borders;

            cellBorders.InsideHorizontalBorders.LineStyle = lineStyle;
            cellBorders.InsideHorizontalBorders.Color = lineColor;

            cellBorders.InsideVerticalBorders.LineStyle = lineStyle;
            cellBorders.InsideVerticalBorders.Color = lineColor;

            cellBorders.SetOutsideBorders(lineColor, lineStyle);

            cellRange.EndUpdateFormatting(cellFormat);
        }
        #endregion

        #region InsertDataRow : DataRow의 값들을 순차적으로 시작셀로부터 가로로 입력한다.
        private void InsertDataRow(Worksheet sheet, string startColumn, int startRow, DataRow inputRow)
        {
            Range inputRange = sheet[startColumn + Convert.ToString(startRow)];
            object[] oValues = inputRow.ItemArray;

            inputRange.SetValue(oValues[0]);

            int columnIndex = inputRange.RightColumnIndex;

            for (int index = 1; index < oValues.Length; index++)
            {
                sheet.Cells[startRow, columnIndex + index - 1].SetValue(oValues[index]);
            }
        }
        #endregion
        #endregion
    }
}
