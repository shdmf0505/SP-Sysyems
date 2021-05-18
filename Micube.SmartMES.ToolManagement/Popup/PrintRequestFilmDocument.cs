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
    /// 프 로 그 램 명  : 필름 의뢰서 출력
    /// 업  무  설  명  : 필름 요청 의뢰서를 출력한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-09-24
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class PrintRequestFilmDocument : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables
        public DataRow CurrentDataRow { get; set; }
        public delegate void PrintDataEventHandler(DataRow insertRow);
        public event PrintDataEventHandler SaveData;
        public DataTable CurrentDataTable { get; set; }
        public List<string> _productDefSerial = new List<string>();

        int _excelIndex = 9;
        int _lastIndex = 19;
        int _excelSerialNo = 1;
        #endregion

        public PrintRequestFilmDocument()
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
        }

        private void PrintRequestFilmDocument_Shown(object sender, EventArgs e)
        {
            //switch (UserInfo.Current.LanguageType)
            //{
            //    case "en-US":
            //        dcDocument.LoadDocument(global::Micube.SmartMES.ToolManagement.Properties.ToolResource.FilmRequestReport_eng);
            //        break;
            //    case "vi-VN":
            //        dcDocument.LoadDocument(global::Micube.SmartMES.ToolManagement.Properties.ToolResource.FilmRequestReport_vtn);
            //        break;
            //    case "zh-CN":
            //        dcDocument.LoadDocument(global::Micube.SmartMES.ToolManagement.Properties.ToolResource.FilmRequestReport_chn);
            //        break;
            //    case "ko-KR":
            //        dcDocument.LoadDocument(global::Micube.SmartMES.ToolManagement.Properties.ToolResource.FilmRequestReport);
            //        break;
            //}


            dcDocument.LoadDocument(global::Micube.SmartMES.ToolManagement.Properties.ToolResource.FilmRequestReport);

            //다른 ProductDefID가 있다면 시트를 구분한다.
            //다른 RequestDepartment가 있다면 시트를 구분한다.
            //다른 RequestUser가 있다면 시트를 구분한다.
            IWorkbook workBook = dcDocument.Document;
            Worksheet workSheet = workBook.Worksheets[0];

            InsertSubject(workSheet);

            string sheetName = "";
            //시트명은 ProductDefID_Serial로 구분한다.
            if (CurrentDataTable.Rows.Count > 0)
            {
                DataRow firstRow = CurrentDataTable.Rows[0];
                GetProductDefinitionSerial(firstRow.GetString("PRODUCTDEFID"), firstRow.GetString("REQUESTDEPARTMENT"), firstRow.GetString("REQUESTUSER"), out sheetName);

                workSheet.Name = sheetName;

                //Header 데이터 입력
                InsertHeader(workSheet, firstRow);

                string prevFilmType = "";

                for (int i = 0; i < CurrentDataTable.Rows.Count; i++)
                {
                    DataRow currentRow = CurrentDataTable.Rows[i];
                    //새로운 시트 생성
                    if(!GetProductDefinitionSerial(currentRow.GetString("PRODUCTDEFID"), currentRow.GetString("REQUESTDEPARTMENT"), currentRow.GetString("REQUESTUSER"), out sheetName))
                    {
                        Worksheet tempSheet = workBook.Worksheets.Add(sheetName);

                        tempSheet.CopyFrom(workSheet);

                        //기존시트의 셀머지
                        MergeBody(workSheet, "C", "D", 9, _lastIndex);

                        //새시트를 할당
                        workSheet = tempSheet;

                        //시트생성후 Header 데이터 입력
                        InsertHeader(workSheet, currentRow);

                        //인덱스 초기화
                        _excelIndex = 9;
                        _lastIndex = 19;
                        _excelSerialNo = 1;

                        //새시트에 데이터입력
                        InsertBody(workSheet, currentRow, ref prevFilmType);                        
                    }
                    else //기존시트에 데이터 입력
                    {
                        InsertBody(workSheet, currentRow, ref prevFilmType);                        
                    }
                }
                //최종시트의 셀머지
                MergeBody(workSheet, "C", "D", 9, _lastIndex);

                ShowMessage(MessageBoxButtons.OK, "CompletePrintReport", "");
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            dcDocument.SaveDocumentAs();
            //■유 / □무
            //□유 / ■무
            //IWorkbook workBook = dcDocument.Document;
            //Worksheet workSheet = workBook.Worksheets[0];

            //InsertSingleValue(workSheet, "I5:P6", Convert.ToDateTime(CurrentDataRow.GetString("REQUESTDATE")).ToString("yyyy.MM.dd"));

            //InsertSingleValue(workSheet, "D7:E7", CurrentDataRow.GetString("CUSTOMER"));
            //InsertSingleValue(workSheet, "K12", CurrentDataRow.GetString("CONTRACTIONX"));
            //InsertSingleValue(workSheet, "M12", CurrentDataRow.GetString("CONTRACTIONY"));
            //InsertSingleValue(workSheet, "N12:O12", CurrentDataRow.GetString("RECEIPTAREA"));
            ////InsertSingleValue(workSheet, "I12", CurrentDataRow.GetString("ISCOATING"));
            //InsertSingleValue(workSheet, "F12", CurrentDataRow.GetString("QTY"));
            //InsertSingleValue(workSheet, "H12:I12", "□유 / ■무");

            //titleRange1.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            //titleRange1.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            //titleRange1.Borders.SetOutsideBorders(Color.Black, BorderLineStyle.Medium);

            //Formatting rangeFormatting = titleRange1.BeginUpdateFormatting();
            //rangeFormatting.Borders.TopBorder.LineStyle = BorderLineStyle.Dashed;

            //Table printTable = workSheet.Tables.Add(workSheet["B2:E8"], true);

            //printTable.Style = workBook.TableStyles[BuiltInTableStyleId.TableStyleMedium27];

            //// Access table columns and name them. 
            //TableColumn productColumn = table.Columns[0];
            //productColumn.Name = "Product";
            //TableColumn priceColumn = table.Columns[1];
            //priceColumn.Name = "Price";
            //TableColumn quantityColumn = table.Columns[2];
            //quantityColumn.Name = "Quantity";
            //TableColumn discountColumn = table.Columns[3];
            //discountColumn.Name = "Discount";
            //TableColumn amountColumn = table.Columns[4];
            //amountColumn.Name = "Amount";
            //SaveData?.Invoke(CurrentDataRow);            
        }
        #endregion

        #region Private Function
        #region InsertSubject : Header의 내용을 입력한다.
        private void InsertSubject(Worksheet workSheet)
        {
            //시트를 생성하고난 이후 바로 호출할 예정이므로 데이터만 입력하면 된다.          FILMREQREPORTTITLE  
            InsertSingleValue(workSheet, "B2", Language.GetDictionary("FILMREQREPORTTITLE").Name);
            InsertSingleValue(workSheet, "B3", Language.GetDictionary("PRODUCTDEFID").Name);
            InsertSingleValue(workSheet, "B4", Language.GetDictionary("PRODUCTDEFNAME").Name);
            InsertSingleValue(workSheet, "B5", Language.GetDictionary("WORKORDERTYPE").Name);
            InsertSingleValue(workSheet, "B6", Language.GetDictionary("PRODUCTIONTYPE").Name);
            InsertSingleValue(workSheet, "B7", Language.GetDictionary("CUTOMERNAME").Name);

            InsertSingleValue(workSheet, "M3", Language.GetDictionary("PRODUCTDEFVERSION").Name);
            InsertSingleValue(workSheet, "M4", Language.GetDictionary("FILMPRINTCOMP").Name);
            InsertSingleValue(workSheet, "M5", Language.GetDictionary("FILMPUBLISHCOMMENT").Name);
            InsertSingleValue(workSheet, "M6", Language.GetDictionary("FILMREQUESTDEPARTMENT").Name);
            InsertSingleValue(workSheet, "M7", Language.GetDictionary("FILMREQUESTUSER").Name);
            
            InsertSingleValue(workSheet, "C8", Language.GetDictionary("WORKPROCESSSEGMENT").Name);
            InsertSingleValue(workSheet, "E8", Language.GetDictionary("USERLAYER").Name);
            InsertSingleValue(workSheet, "G8", Language.GetDictionary("PUBLISHDOCCOUNT").Name);
            InsertSingleValue(workSheet, "I8", Language.GetDictionary("ISCOATING").Name);
            InsertSingleValue(workSheet, "K8", Language.GetDictionary("CONTRACTION").Name);
            InsertSingleValue(workSheet, "O8", Language.GetDictionary("RECEIPTTIME").Name);
            InsertSingleValue(workSheet, "Q8", Language.GetDictionary("DFFREMARKS").Name);
        }
        #endregion

        #region InsertHeader : Header의 내용을 입력한다.
        private void InsertHeader(Worksheet workSheet, DataRow currentRow)
        {
            //시트를 생성하고난 이후 바로 호출할 예정이므로 데이터만 입력하면 된다.            
            InsertSingleValue(workSheet, "F3", currentRow.GetString("PRODUCTDEFID"));
            InsertSingleValue(workSheet, "F4", currentRow.GetString("PRODUCTDEFNAME"));
            InsertSingleValue(workSheet, "F5", "신규");
            InsertSingleValue(workSheet, "F6", currentRow.GetString("PRODUCTIONTYPE"));
            InsertSingleValue(workSheet, "F7", currentRow.GetString("CUSTOMERNAME"));


            InsertSingleValue(workSheet, "P3", currentRow.GetString("DURABLEDEFVERSION"));
            InsertSingleValue(workSheet, "P4", currentRow.GetString("VENDORNAME"));
            InsertSingleValue(workSheet, "P5", "신규");
            InsertSingleValue(workSheet, "P6", currentRow.GetString("REQUESTDEPARTMENT"));
            InsertSingleValue(workSheet, "P7", currentRow.GetString("REQUESTUSER"));
        }
        #endregion

        #region InsertBody : Body의 내용을 입력한다.
        private void InsertBody(Worksheet workSheet, DataRow currentRow, ref string prevFilmType)
        {
            //입력전 작업공정이 같은지 판단하여 머지여부 결정한다. 혹 머지는 최종입력이후 진행한다.
            //마지막 Row의 값을 저장하고 있다가 입력하는 행의 현재 Index의 값이 1 만큰의 차이로 접근하면 행을 늘린다.
            if(_excelIndex == _lastIndex -1)
            {
                AddNewRow(workSheet, _excelIndex, 1);
                PaintingCellBorder(workSheet, "B", "M", _excelIndex + 1, _excelIndex + 1, BorderLineStyle.Thin, Color.Black);
                MergeCell(workSheet, "C", "D", _excelIndex + 1, _excelIndex + 1);
                MergeCell(workSheet, "I", "J", _excelIndex + 1, _excelIndex + 1);
                MergeCell(workSheet, "O", "P", _excelIndex + 1, _excelIndex + 1);
                MergeCell(workSheet, "Q", "T", _excelIndex + 1, _excelIndex + 1);

                //기본정보입력
                InsertSingleValue(workSheet, "H" + (_excelIndex + 1).ToString(), Language.GetDictionary("DOCCOUNT").Name);
                InsertSingleValue(workSheet, "K" + (_excelIndex + 1).ToString(), "X:");
                InsertSingleValue(workSheet, "M" + (_excelIndex + 1).ToString(), "Y:");
                InsertSingleValueCenterAlign(workSheet, "O" + (_excelIndex + 1).ToString(), ":");

                _lastIndex++;
            }
            if (!currentRow.GetString("FILMTYPE").Equals(prevFilmType))
            {
                InsertSingleValue(workSheet, "B" + _excelIndex.ToString(), _excelSerialNo.ToString());
                MergeCell(workSheet, "C", "D", _excelIndex, _excelIndex);
                InsertSingleValue(workSheet, "C" + _excelIndex.ToString(), currentRow.GetString("FILMTYPE"));

                _excelSerialNo++;
                prevFilmType = currentRow.GetString("FILMTYPE");
            }
            InsertSingleValue(workSheet, "E" + _excelIndex.ToString(), currentRow.GetString("FILMUSELAYER"));
            InsertSingleValue(workSheet, "G" + _excelIndex.ToString(), currentRow.GetString("QTY"));
            
            if(currentRow.GetString("ISCOATING").Equals("Y"))
            {
                InsertSingleValue(workSheet, "I" + _excelIndex.ToString(), "■유 / □무");
            }
            else
            {
                InsertSingleValue(workSheet, "I" + _excelIndex.ToString(), "□유 / ■무");
            }

            InsertSingleValue(workSheet, "L" + _excelIndex.ToString(), currentRow.GetString("CONTRACTIONX"));
            InsertSingleValue(workSheet, "N" + _excelIndex.ToString(), currentRow.GetString("CONTRACTIONY"));

            _excelIndex++;
        }
        #endregion

        #region MergeBody
        private void MergeBody(Worksheet workSheet, string startColumn, string endColumn, int startIndex, int endIndex)
        {
            string ruleValue = GetSingleValue(workSheet, startColumn + startIndex.ToString());

            for(int index = startIndex + 1; index <= endIndex; index++)
            {
                if(!ruleValue.Equals(GetSingleValue(workSheet, startColumn + index.ToString())) 
                    && !GetSingleValue(workSheet, startColumn + index.ToString()).Equals(""))
                {
                    MergeCell(workSheet, startColumn, endColumn, startIndex, index - 1);
                    MergeCell(workSheet, "B", "B", startIndex, endIndex);

                    startIndex = index;
                }
            }
            MergeCell(workSheet, startColumn, endColumn, startIndex, endIndex);
            MergeCell(workSheet, "B", "B", startIndex, endIndex);
            PaintingCellBorderBottom(workSheet, startColumn, endColumn, startIndex, endIndex, BorderLineStyle.Medium, Color.FromArgb(0, 0, 255));
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

        #region GetSingleValue : 데이터를 특정 셀에서 가져온다.
        private string GetSingleValue(Worksheet sheet, string rangeColumn)
        {
            Range inputRange = sheet[rangeColumn];

            return inputRange.Value.ToString();
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

        private void PaintingCellBorderCustom(Worksheet sheet, string startColumn, string endColumn, int startIndex, int endIndex, BorderLineStyle lineStyle, Color lineColor
            , BorderLineStyle bottomStyle, Color bottomColor
            , BorderLineStyle topStyle, Color topColor)
        {
            Range cellRange = sheet[startColumn + startIndex.ToString() + ":" + endColumn + endIndex.ToString()];
            Formatting cellFormat = cellRange.BeginUpdateFormatting();
            Borders cellBorders = cellFormat.Borders;

            cellBorders.InsideHorizontalBorders.LineStyle = lineStyle;
            cellBorders.InsideHorizontalBorders.Color = lineColor;

            cellBorders.InsideVerticalBorders.LineStyle = lineStyle;
            cellBorders.InsideVerticalBorders.Color = lineColor;

            //cellBorders.SetOutsideBorders(lineColor, lineStyle);

            cellBorders.BottomBorder.LineStyle = bottomStyle;
            cellBorders.BottomBorder.Color = bottomColor;

            cellBorders.TopBorder.LineStyle = topStyle;
            cellBorders.TopBorder.Color = topColor;

            cellRange.EndUpdateFormatting(cellFormat);
        }
        private void PaintingCellBorderBottom(Worksheet sheet, string startColumn, string endColumn, int startIndex, int endIndex, BorderLineStyle bottomStyle, Color bottomColor)
        {
            Range cellRange = sheet[startColumn + startIndex.ToString() + ":" + endColumn + endIndex.ToString()];
            Formatting cellFormat = cellRange.BeginUpdateFormatting();
            Borders cellBorders = cellFormat.Borders;

            cellBorders.BottomBorder.LineStyle = bottomStyle;
            cellBorders.BottomBorder.Color = bottomColor;

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

        #region GetProductDefinitionSerial : 중복된 값이 있을 경우 시트를 새로 생성할 것을 알림
        private bool GetProductDefinitionSerial(string productDefID, string requestDepartment, string requestUser, out string sheetName)
        {
            sheetName = "";
            string uniqueKey = GetRegularSheetName(productDefID + "_" + requestDepartment + "_" + requestUser);

            foreach(string sheet in _productDefSerial)
            {
                if (sheet.Equals(uniqueKey))
                    return true;
            }

            //같은명의 시트가 없으므로 새로 생성
            _productDefSerial.Add(uniqueKey);

            sheetName = uniqueKey;

            return false;
        }

        private string GetRegularSheetName(string sheetName)
        {
            sheetName = sheetName.Replace("\\", "").Replace("/", "").Replace("?", "").Replace("*","").Replace("[", "").Replace("]", "");

            return sheetName;
        }
        #endregion
        #endregion
    }
}
