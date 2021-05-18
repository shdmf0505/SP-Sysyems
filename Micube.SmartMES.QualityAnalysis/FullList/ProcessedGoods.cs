#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;

using DevExpress.Spreadsheet;
using DevExpress.XtraEditors.Repository;

using System;
using System.IO;
using System.Data;
using System.Windows.Forms;
using System.Threading.Tasks;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질 관리 > 수입검사 > 가공품
    /// 업  무  설  명  : 가공품 부자재 Full List
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2020-04-16
    /// 수  정  이  력  : 
    /// 
    /// </summary>
    public partial class ProcessedGoods : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public ProcessedGoods()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();
            InitializeLanguageKey();
            InitializeGrid();
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            grdMain.LanguageKey = "PROCESSEDGOODS";
            btnFullList.LanguageKey = "FULLLIST";
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdMain.View.AddTextBoxColumn("SEQUENCE", 10).SetIsHidden();

            grdMain.View.AddDateEditColumn("REGISTERDATE", 100).SetLabel("RECEPTDATE").SetValidationIsRequired()
                 .SetDisplayFormat("yyyy-MM-dd").SetTextAlignment(TextAlignment.Left);

            grdMain.View.AddTextBoxColumn("MATERIALSTYPE", 120).SetLabel("TYPENAME").SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("INBOUNDDEGREE", 80).SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("INBOUNDQTY", 80).SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("VENDOR", 150).SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("MODEL", 120).SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddTextBoxColumn("CODENO", 120).SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddTextBoxColumn("SPAREPARTNAME", 100).SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddTextBoxColumn("LAYER", 100).SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddTextBoxColumn("LOTID", 120).SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddTextBoxColumn("INSPECTIONUSER", 60).SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("FINALJUDGMENT", 60).SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("DESCRIPTION", 260).SetLabel("UNIQUENESS").SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("MEASURINGINSPECTIONQTY", 60).SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddTextBoxColumn("DEFECTQTY", 60).SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddTextBoxColumn("REDEFAULT", 60).SetTextAlignment(TextAlignment.Right);

            grdMain.View.PopulateColumns();
            grdMain.View.BestFitColumns();

            grdMain.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;

            RepositoryItemDateEdit edit = new RepositoryItemDateEdit();
            edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            edit.Mask.EditMask = @"(19|20)\d{2}-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[0-1])";
            edit.Mask.UseMaskAsDisplayFormat = true;

            grdMain.View.Columns["REGISTERDATE"].ColumnEdit = edit;
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            //! Row 추가 이벤트
            grdMain.View.InitNewRow += (s, e) =>
            {
                grdMain.View.SetRowCellValue(e.RowHandle, "REGISTERDATE", DateTime.Now);
            };

            //! Full List 클릭 이벤트
            btnFullList.Click += (s, e) =>
            {
                try
                {
                    if (grdMain.View.RowCount.Equals(0))
                    {
                        ShowMessage("NoMappingData");
                        return;
                    }

                    SaveFileDialog dialog = new SaveFileDialog()
                    {
                        Filter = "Excel File|*.xlsx"
                    };

                    if (!dialog.ShowDialog().Equals(DialogResult.OK))
                    {
                        return;
                    }

                    // 저장된 엑셀을 시트에 넣는다.
                    if (CommonFunction.GetFtpImageFileToByte(@"FULLLIST/ProcessedGoods_SubMaterials.xlsx", true) is byte[] excelByte)
                    {
                        SmartSpreadSheet sheet = new SmartSpreadSheet();
                        sheet.LoadDocument(excelByte);

                        sheet.BeginUpdate();

                        for (int i = 0; i < grdMain.View.DataRowCount; i++)
                        {
                            DataRow dr = grdMain.View.GetDataRow(i);

                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("B", i + 5)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "REGISTERDATE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("C", i + 5)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "MATERIALSTYPE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("D", i + 5)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "INBOUNDDEGREE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("E", i + 5)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "INBOUNDQTY"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("F", i + 5)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "VENDOR"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("G", i + 5)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "MODEL"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("H", i + 5)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "CODENO"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("I", i + 5)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SPAREPARTNAME"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("J", i + 5)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "LAYER"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("K", i + 5)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "LOTID"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("L", i + 5)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "INSPECTIONUSER"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("M", i + 5)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FINALJUDGMENT"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("N", i + 5)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "DESCRIPTION"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("P", i + 5)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "DEFECTQTY"));
                        }

                        sheet.EndUpdate();

                        using (FileStream stream = new FileStream(dialog.FileName, FileMode.Create, FileAccess.ReadWrite))
                        {
                            sheet.SaveDocument(stream, DocumentFormat.Xlsx);
                            ShowMessage("SuccedSave");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MSGBox.Show(MessageBoxType.Error, ex.Message);
                }
            };
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            ExecuteRule("ProcessedGoods", grdMain.GetChangedRows());
        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);

                await base.OnSearchAsync();
                grdMain.View.ClearDatas();

                if (await SqlExecuter.QueryAsync("GetProcessedGoods", "10001", Conditions.GetValues()) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        grdMain.View.ClearDatas();
                        ShowMessage("NoSelectData");
                        return;
                    }

                    grdMain.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                throw MessageException.Create(Format.GetString(ex));
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);
            }
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            if (grdMain.GetChangedRows().Rows.Count.Equals(0))
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion
    }
}
