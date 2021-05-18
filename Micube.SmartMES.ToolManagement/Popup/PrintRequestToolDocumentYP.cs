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
    /// 프 로 그 램 명  : 치공구 의뢰서 출력(영풍전자용)
    /// 업  무  설  명  : 치공구 요청 의뢰서를 출력한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-11-06
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class PrintRequestToolDocumentYP : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables
        public DataRow CurrentDataRow { get; set; }
        public DataTable _toolInfoTable;
        public delegate void PrintDataEventHandler(DataRow insertRow);
        public event PrintDataEventHandler SaveData;
        #endregion

        #region ToolInfoTable - 치공구정보 Property
        public DataTable ToolInfoTable
        {
            set { _toolInfoTable = value; }
        }
        #endregion

        public PrintRequestToolDocumentYP()
        {
            InitializeComponent();

            InitializeEvents();
        }

        #region Event
        private void InitializeEvents()
        {
            Shown += PrintRequestFilmDocument_Shown;
            btnPrint.Click += BtnPrint_Click;
            btnExit.Click += BtnExit_Click;
            btnExport.Click += BtnExport_Click;
        }

        #region BtnExport_Click - 내보내기버튼 이벤트
        private void BtnExport_Click(object sender, EventArgs e)
        {
            DialogResult dgResult = fileSavor.ShowDialog();

            if (dgResult == DialogResult.OK)
            {
                dcDocument.SaveDocument(fileSavor.FileName, DocumentFormat.Xlsx);

                System.Diagnostics.Process.Start(fileSavor.FileName);
            }
        }
        #endregion

        #region PrintRequestFilmDocument_Shown - 화면 로딩후 이벤트 - 실제 문서 출력 로직
        private void PrintRequestFilmDocument_Shown(object sender, EventArgs e)
        {
            //문서로드
            //switch (UserInfo.Current.LanguageType)
            //{
            //    case "en-US":
            //        dcDocument.LoadDocument(global::Micube.SmartMES.ToolManagement.Properties.ToolResource.ToolReworkReport_YP_eng);
            //        break;
            //    case "vi-VN":
            //        dcDocument.LoadDocument(global::Micube.SmartMES.ToolManagement.Properties.ToolResource.ToolReworkReport_YP_vtn);
            //        break;
            //    case "zh-CN":
            //        dcDocument.LoadDocument(global::Micube.SmartMES.ToolManagement.Properties.ToolResource.ToolReworkReport_YP_chn);
            //        break;
            //    case "ko-KR":
            //        dcDocument.LoadDocument(global::Micube.SmartMES.ToolManagement.Properties.ToolResource.ToolReworkReport_YP);
            //        break;
            //}            
            dcDocument.LoadDocument(global::Micube.SmartMES.ToolManagement.Properties.ToolResource.ToolReworkReport_YP);

            //로드후 작업시작
            if (_toolInfoTable != null)
            {
                IWorkbook workBook = dcDocument.Document;
                Worksheet workSheet = workBook.Worksheets[0];

                DataRow toolRow = _toolInfoTable.Rows[0];

                InsertSingleValue(workSheet, "E3", Language.Get("TOOLREQREPORTTITLE"));
                InsertSingleValue(workSheet, "C8", Language.Get("CUTOMERNAME"));
                InsertSingleValue(workSheet, "H8", Language.Get("PRODUCESAMPLE"));
                InsertSingleValue(workSheet, "C10", Language.Get("MODELCODE"));
                InsertSingleValue(workSheet, "H10", Language.Get("MODELNAME"));
                InsertSingleValue(workSheet, "C12", Language.Get("TOOLVERSION"));
                InsertSingleValue(workSheet, "H12", Language.Get("LAYERNAME"));
                InsertSingleValue(workSheet, "C14", Language.Get("DUEDATE"));
                InsertSingleValue(workSheet, "H14", Language.Get("REQUESTUSERNAME"));
                InsertSingleValue(workSheet, "C16", Language.Get("REQUESTCONTENT"));
                InsertSingleValue(workSheet, "C18", Language.Get("TOOLMAKETYPENAME"));
                InsertSingleValue(workSheet, "H18", Language.Get("QTY"));
                InsertSingleValue(workSheet, "C20", Language.Get("TOOLCATEGORY"));
                InsertSingleValue(workSheet, "C22", Language.Get("OWNSHIPTYPE"));
                InsertSingleValue(workSheet, "H22", Language.Get("SALESEXPRECTQTY"));
                InsertSingleValue(workSheet, "C24", Language.Get("USEDLIMIT"));
                InsertSingleValue(workSheet, "H24", Language.Get("GOALQTY"));
                InsertSingleValue(workSheet, "C26", Language.Get("TRADECONDITION"));
                InsertSingleValue(workSheet, "H26", Language.Get("MARKETTOOLEXPCOAST"));
                InsertSingleValue(workSheet, "C28", Language.Get("MAKEREASON"));
                InsertSingleValue(workSheet, "C30", Language.Get("REMARK"));

                InsertSingleValue(workSheet, "C5:E5", toolRow.GetString("REQUESTNO"));
                InsertSingleValue(workSheet, "J6:L6", DateTime.Now.ToString("yyyy-MM-dd"));
                InsertSingleValue(workSheet, "D8:G8", toolRow.GetString("CUSTOMERNAME"));
                InsertSingleValue(workSheet, "I8:L8", toolRow.GetString("TOOLCATEGORY"));
                InsertSingleValue(workSheet, "D10:G10", toolRow.GetString("PRODUCTDEFID"));
                InsertSingleValue(workSheet, "I10:L10", toolRow.GetString("PRODUCTDEFNAME"));
                InsertSingleValue(workSheet, "D12:G12", toolRow.GetString("DURABLEDEFVERSION"));
                InsertSingleValue(workSheet, "I12:L12", toolRow.GetString("LAYER"));
                InsertSingleValue(workSheet, "D14:G14", toolRow.GetString("DELIVERYDATE"));
                InsertSingleValue(workSheet, "I14:L14", toolRow.GetString("USERNAME"));
                InsertSingleValue(workSheet, "D18:G18", toolRow.GetString("TOOLMAKETYPE"));
                InsertSingleValue(workSheet, "I18:L18", toolRow.GetString("QTY"));
                InsertSingleValue(workSheet, "D20:L20", toolRow.GetString("TOOLTYPE"));
                InsertSingleValue(workSheet, "D22:G22", "");
                InsertSingleValue(workSheet, "I22:L22", "");
                InsertSingleValue(workSheet, "D24:G24", toolRow.GetString("USEDLIMIT"));
                InsertSingleValue(workSheet, "I24:L24", "");
                InsertSingleValue(workSheet, "D26:G26", "");
                InsertSingleValue(workSheet, "I26:L26", "");
                InsertSingleValue(workSheet, "D28:L28", toolRow.GetString("REQUESTCOMMENT"));
                InsertSingleValue(workSheet, "D30:L37", toolRow.GetString("DESCRIPTION"));


                SaveData?.Invoke(CurrentDataRow);

                ShowMessage(MessageBoxButtons.OK, "CompletePrintReport", "");
            }
        }
        #endregion

        #region BtnExit_Click - 종료버튼 이벤트
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        #endregion

        #region BtnPrint_Click - 출력버튼이벤트
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
