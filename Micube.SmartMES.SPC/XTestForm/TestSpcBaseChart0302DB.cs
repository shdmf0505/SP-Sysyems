using DevExpress.Spreadsheet;
using DevExpress.XtraPrinting;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Micube.SmartMES.SPC
{
    public partial class TestSpcBaseChart0302DB : SmartConditionManualBaseForm
    {

        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자
        /// <summary>
        /// 생성자
        /// </summary>
        public TestSpcBaseChart0302DB()
        {
            InitializeComponent();
        }

        #endregion

        #region 툴바
        //저장버튼 클릭

        #endregion

        #region 검색
        //비동기 override 모델

        #endregion

        #region 유효성 검사
        //데이터 저장할때 컨텐츠 영역의 유효성 검사
        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
        }
        private void btnSearchData_Click(object sender, EventArgs e)
        {
            IWorkbook workbook = smartSpreadSheet1.Document;
            //Sheet Index 확인 주의 
            Worksheet worksheet = workbook.Worksheets[1];
            for (int i = 0; i < worksheet.Comments.Count; i++)
            {
                string memoCoordinates = worksheet.Comments[i].Reference.ToString();
                Console.WriteLine("{0}", memoCoordinates);
                this.txtLocation.Text = memoCoordinates;
                string changeDataValue = this.txtChangeData.Text;
                if (changeDataValue != null && changeDataValue != "")
                {
                }
                else
                {
                    changeDataValue = "입력값이 없네요~.,";
                }
                worksheet.Cells[memoCoordinates].SetValueFromText(changeDataValue);
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
        private void btnExcelImageInsert_Click(object sender, EventArgs e)
        {
            IWorkbook workbook = smartSpreadSheet1.Document;
            //Sheet Index 확인 주의 
            Worksheet worksheet = workbook.Worksheets[1];
            float nRevise = 15; //1920x1080 기준 화면에서 대략 1mm 는 15 Pixels(화소) 정도임.
            try
            {
                //2안
                Image image1 = Image.FromFile(@"C:\60.Source\97.image\Seyung.PNG");
                var src = SpreadsheetImageSource.FromImage(image1);
                string cellRangeValue = this.txtImageLocation.Text;
                if (cellRangeValue != null && cellRangeValue != "")
                {
                }
                else
                {
                    //cellRangeValue = "AH17";
                    cellRangeValue = "V17";
                    this.txtImageLocation.Text = cellRangeValue;
                }

                var targetCell = worksheet.Cells[cellRangeValue];
                var picture = worksheet.Pictures.AddPicture(src, targetCell, true);
                //picture.Left = 20;//전체 Sheet에서 시작됨.
                
                //picture.Placement = Placement.FreeFloating;
                picture.LockAspectRatio = false;//image 크기 비율 설정 미사용.
                picture.Move(3, 3);
                picture.Width = 74 * nRevise; // 75mm * 15화소
                picture.Height = 34 * nRevise;// 35mm * 15화소

                Console.WriteLine(string.Format("Width: {0}, Height: {1} ", picture.Width, picture.Height));
                Console.WriteLine("");
                //picture.Width = picture.OriginalWidth;
                //picture.Height = picture.OriginalHeight;
                //picture.Placement = Placement.Move;
                //picture.TopLeftCell.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
                //picture.TopLeftCell.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                //Comment comment = worksheet.SelectedComment;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void btnExcelImageInsert_Click1(object sender, EventArgs e)
        {
            IWorkbook workbook = smartSpreadSheet1.Document;
            Worksheet worksheet = workbook.Worksheets[0];

            try
            {
                //1안 - 실패
                Image image1 = Image.FromFile(@"C:\60.Source\97.image\Seyung.PNG");
                var src = SpreadsheetImageSource.FromImage(image1);
                var targetCell = worksheet.Cells["AH17"];
                var picture = worksheet.Pictures.AddPicture(src, targetCell, lockAspectRatio: true);
                //workbook.Worksheets[0].Pictures.AddPicture(@"C:\60.Source\97.image\Seyung.PNG", worksheet["V16"], false);
                Comment comment = worksheet.SelectedComment;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //smartSpreadSheet1.Shapes.AddPicture("D:\\ds3UHPUKKCw.jpg", worksheet["B2"], false);
            
        }


        /// <summary>
        /// Export 버튼 Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {

            SaveFileDialog opf = new SaveFileDialog();

            opf.FileName = @"성적서_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx"; //초기 파일명을 지정할 때 사용한다.
            opf.Filter = "Excel Files(*.xlsx)|*.xlsx;*.xls";
            opf.Title = "Save an Excel File";

            DevExpress.XtraPivotGrid.PivotXlsxExportOptions exOpt = new DevExpress.XtraPivotGrid.PivotXlsxExportOptions();
            exOpt.ExportType = DevExpress.Export.ExportType.WYSIWYG;
            exOpt.ShowGridLines = true;

            opf.ShowDialog();
            if (opf.FileName.Length > 0)
            {
                //pvProductPlan.ExportToXlsx(opf.FileName, exOpt);
                //Console.WriteLine("{0}/ {1}", opf.FileName.ToString(), exOpt.ToString());
                smartSpreadSheet1.SaveDocument(opf.FileName.ToString());
            }

        }
        /// <summary>
        /// 프린트 버튼 Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            using (PrintingSystem printingSystem = new PrintingSystem())
            {
                using (PrintableComponentLink link = new PrintableComponentLink(printingSystem))
                {
                    link.Component = smartSpreadSheet1;
                    link.CreateDocument();
                    link.ShowPreviewDialog();
                }
            }
        }
        #endregion

        #region 컨텐츠 영역 초기화

        protected void InitializeContent()
        {
            InitializeEvent();
            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가

        #endregion


        private void TestSpcBaseChart0302DB_Load(object sender, EventArgs e)
        {
            //Properties.Resources.InstrumentationDocument019
            //GetDocPropRange().Comment.Shape.TextFrame.Characters(misValue, misValue).Text;

            //smartSpreadSheet1.LoadDocument(Properties.Resources.InstrumentationDocument019);
            //smartSpreadSheet1.LoadDocument(@"C:\60.Source\SmartMES\src\Micube.SmartMES.SPC\Resources\InstrumentationDocument019.xls");
            smartSpreadSheet1.LoadDocument(@"C:\60.Source\96.성적서양식\(I16-63) MINT IMAGIS PF(B)(DS)-양식변경완료 180329.xlsx");

        }
        protected async override Task OnSearchAsync()
        {
            //sia확인 : 폼 조회.
            //this.ucXBarGrid1.XBarRExcute();

            await base.OnSearchAsync();

            var values = Conditions.GetValues();

            //DateTime analysisDate = Convert.ToDateTime(values["P_ANALYSISDATE"]);
            //values["P_ANALYSISDATE"] = string.Format("{0:yyyy-MM-dd}", analysisDate);
            //values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);//ko-KR

            DataTable chemicalAnalysisDt = await SqlExecuter.QueryAsync("GetChemicalAnalysis", "10001", values);

            if (chemicalAnalysisDt.Rows.Count < 1)
            {
                // 조회할 데이터가 없습니다.
                ShowMessage("NoSelectData");
            }

            Console.WriteLine(chemicalAnalysisDt.Rows.Count);
            //grdChemicalRegistration.DataSource = chemicalAnalysisDt;
            //_chemicalSpecDt.Clear();
        }
        private void TestSpcBaseChart0302DB_Resize(object sender, EventArgs e)
        {
            FormResize();
        }
        /// <summary>
        /// Form Resize
        /// </summary>
        private void FormResize()
        {
            try
            {
                int position = 150;
                int rate = 3;
            }
            catch (Exception)
            {
                //throw;
            }
        }


    }
}
