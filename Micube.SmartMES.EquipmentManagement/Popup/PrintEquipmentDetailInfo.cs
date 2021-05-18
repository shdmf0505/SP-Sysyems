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

namespace Micube.SmartMES.EquipmentManagement.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 설비점검 정보출력
    /// 업  무  설  명  : 설비점검의 정보를 출력한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2020-02-11
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class PrintEquipmentDetailInfo : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Vaiables and Property        
        public DataRow CurrentDataRow { get; set; } //설비의 기본정보
        public DataTable InfoList { get; set; } //점검항목목록
        #endregion

        #region 생성자
        public PrintEquipmentDetailInfo()
        {
            InitializeComponent();

            InitializeEvents();
        }
        #endregion

        #region Events
        private void InitializeEvents()
        {
            Shown += PrintEquipmentDetailInfo_Shown;
            btnPrint.Click += BtnPrint_Click;
            btnExit.Click += BtnExit_Click;
        }

        #region BtnExit_Click - 취소버튼이벤트
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        #endregion

        #region BtnPrint_Click - 출력버튼이벤트
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            dcDocument.SaveDocumentAs();
        }
        #endregion

        #region PrintEquipmentDetailInfo_Shown - 화면 로딩후 이벤트
        private void PrintEquipmentDetailInfo_Shown(object sender, EventArgs e)
        {
            this.ShowWaitArea();
            try
            {
                if (CurrentDataRow != null)
                {
                    dcDocument.LoadDocument(global::Micube.SmartMES.EquipmentManagement.Properties.EqpResource.MaintWorkOrderDetailReportSample);

                    IWorkbook workBook = dcDocument.Document;
                    Worksheet workSheet = workBook.Worksheets[0];

                    //설비명 G1 : EQUIPMENTNAME
                    InsertSingleValue(workSheet, "G1", CurrentDataRow.GetString("EQUIPMENTNAME"));
                    //공정그룹명 M1 : PROCESSSEGMENTCLASSNAME
                    InsertSingleValue(workSheet, "M1", CurrentDataRow.GetString("PROCESSSEGMENTCLASSNAME"));
                    //설비코드 G2 : EQUIOPMENTID
                    InsertSingleValue(workSheet, "G2", CurrentDataRow.GetString("EQUIPMENTID"));
                    //작업장 M2 : AREANAME
                    InsertSingleValue(workSheet, "M2", CurrentDataRow.GetString("AREANAME"));
                    //작업번호 G3 : WORKORDERID
                    InsertSingleValue(workSheet, "G3", CurrentDataRow.GetString("WORKORDERID"));
                    //수리담당자 M3 : REPAIRUSER
                    InsertSingleValue(workSheet, "M3", CurrentDataRow.GetString("REPAIRUSER"));

                    int excelIndex = 5;
                    foreach (DataRow infoRow in InfoList.Rows)
                    {
                        //"MAINTPOSITION"               //점검부위  A5~
                        InsertSingleValue(workSheet, "A" + Convert.ToString(excelIndex), infoRow.GetString("MAINTPOSITION"));
                        //"MAINTDETAILITEM"             //점검항목  G5~
                        InsertSingleValue(workSheet, "G" + Convert.ToString(excelIndex), infoRow.GetString("MAINTDETAILITEM"));
                        //"MAINTMETHOD"                 //점검방법  J5~
                        InsertSingleValue(workSheet, "J" + Convert.ToString(excelIndex), infoRow.GetString("MAINTMETHOD"));
                        //"MAINTRESULT"                 //점검결과  M5~
                        InsertSingleValue(workSheet, "M" + Convert.ToString(excelIndex), infoRow.GetString("MAINTRESULT"));
                        //"ACTIONDESCRIPTION"           //조치내용  N5~     
                        InsertSingleValue(workSheet, "N" + Convert.ToString(excelIndex), infoRow.GetString("ACTIONDESCRIPTION"));
                        excelIndex++;
                    }
                }
            }
            catch(Exception err)
            {
                throw err;
            }
            finally
            {
                this.CloseWaitArea();
            }
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

        private string GetRegularSheetName(string sheetName)
        {
            sheetName = sheetName.Replace("\\", "").Replace("/", "").Replace("?", "").Replace("*", "").Replace("[", "").Replace("]", "");

            return sheetName;
        }
        #endregion
        #endregion
    }
}
