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
    /// 프 로 그 램 명  : 품질 관리 > 수입검사 > 입고품 일일 입고 List
    /// 업  무  설  명  : 외주 입고품 공정 일일 입고 Full List
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2020-04-10
    /// 수  정  이  력  : 
    /// 
    /// </summary>
    public partial class OutIncomingDaily : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public OutIncomingDaily()
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
            grdMain.LanguageKey = "DAILYWAREHOUSINGLIST";
            btnFullList.LanguageKey = "FULLLIST";
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdMain.View.AddTextBoxColumn("SEQUENCE", 10).SetIsHidden();

            var group = grdMain.View.AddGroupColumn("");
            group.AddTextBoxColumn("NOTE", 60).SetLabel("REMARK").SetTextAlignment(TextAlignment.Left);

            group.AddDateEditColumn("REGISTERDATE", 100).SetLabel("DAYCATEGORY").SetValidationIsRequired()
                 .SetDisplayFormat("yyyy-MM-dd").SetTextAlignment(TextAlignment.Left);

            group.AddTextBoxColumn("INCOMINGTIME", 80).SetLabel("RECEIPTTIME").SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("PROCESSCLASS", 60).SetLabel("PROCESSSEGMENTCLASS").SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("DETAILPROCESS", 120).SetLabel("DETAILOPERATION").SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("MODEL", 120).SetLabel("MODELNAME").SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("LOTID", 180).SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            
            group.AddTextBoxColumn("REV", 100).SetLabel("ITEMVERSION").SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("INSPECTIONQTY", 60).SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("DEFECTCOUNT", 60).SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("DEFECTRATE", 60).SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("DEFECTNAME", 120).SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("JUDGMENT", 60).SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("PRCESSCONTENT", 200).SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("IRCARD", 60).SetLabel("CREATEIRCARD").SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("INSPECTIONGROUP", 60).SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("INCOMINGVENDER", 100).SetLabel("TOOKREWORKRECEIPTVENDOR").SetTextAlignment(TextAlignment.Left);

            group.AddDateEditColumn("FINISHDATE", 100).SetLabel("NONCONFORMITYFINISHDATE")
                 .SetDisplayFormat("yyyy-MM-dd").SetTextAlignment(TextAlignment.Left);

            group.AddTextBoxColumn("FINISHTIME", 80).SetLabel("SENDTIME").SetTextAlignment(TextAlignment.Left);

            group = grdMain.View.AddGroupColumn("SELECTIONITEMINSPRESULT");
            group.AddTextBoxColumn("RESULTINSPECTIONQTY", 80).SetLabel("INSPECTIONQTY").SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("RESULTDEFECTCOUNT", 80).SetLabel("DEFECTCOUNT").SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("RESULTINSPECTIONRATE", 80).SetLabel("DEFECTRATE").SetTextAlignment(TextAlignment.Left);

            group = grdMain.View.AddGroupColumn("");
            group.AddTextBoxColumn("DESCRIPTION", 260).SetLabel("UNIQUENESS").SetTextAlignment(TextAlignment.Left);

            grdMain.View.PopulateColumns();
            grdMain.View.BestFitColumns();

            grdMain.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;

            RepositoryItemDateEdit edit = new RepositoryItemDateEdit();
            edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            edit.Mask.EditMask = @"(19|20)\d{2}-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[0-1])";
            edit.Mask.UseMaskAsDisplayFormat = true;

            grdMain.View.Columns["REGISTERDATE"].ColumnEdit = edit;
            grdMain.View.Columns["FINISHDATE"].ColumnEdit = edit;
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            //! Row 추가 이벤트
            grdMain.View.InitNewRow += (s, e) => grdMain.View.SetRowCellValue(e.RowHandle, "REGISTERDATE", DateTime.Now);

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
                    if (CommonFunction.GetFtpImageFileToByte(@"FULLLIST/Outsorcing_Incoming_Daily.xlsx", true) is byte[] excelByte)
                    {
                        SmartSpreadSheet sheet = new SmartSpreadSheet();
                        sheet.LoadDocument(excelByte);

                        sheet.BeginUpdate();

                        for (int i = 0; i < grdMain.View.DataRowCount; i++)
                        {
                            DataRow dr = grdMain.View.GetDataRow(i);

                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("A", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "NOTE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("B", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "REGISTERDATE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("C", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "INCOMINGTIME"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("D", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "PROCESSCLASS"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("E", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "DETAILPROCESS"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("F", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "MODEL"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("G", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "LOTID"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("H", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "REV"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("I", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "INSPECTIONQTY"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("J", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "DEFECTCOUNT"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("K", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "DEFECTRATE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("L", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "DEFECTNAME"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("M", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "JUDGMENT"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("N", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "PRCESSCONTENT"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("O", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "IRCARD"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("P", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "INSPECTIONGROUP"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("Q", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "INCOMINGVENDER"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("R", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FINISHDATE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("S", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FINISHTIME"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("T", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "RESULTINSPECTIONQTY"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("U", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "RESULTDEFECTCOUNT"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("V", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "RESULTINSPECTIONRATE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("W", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "DESCRIPTION"));
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

            ExecuteRule("OutIncomingDaily", grdMain.GetChangedRows());
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

                if (await SqlExecuter.QueryAsync("GetOutIncomingDaily", "10001", Conditions.GetValues()) is DataTable dt)
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