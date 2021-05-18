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
    /// 프 로 그 램 명  : 품질 관리 > 수입검사 > 자재 Shield
    /// 업  무  설  명  : 자재 Shield Full List
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2020-04-10
    /// 수  정  이  력  : 
    /// 
    /// </summary>
    public partial class MaterialsShield : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public MaterialsShield()
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
            grdMain.LanguageKey = "MATERIALSSHIELD";
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

            group.AddDateEditColumn("REGISTERDATE", 100).SetLabel("TRANSACTIONDATE").SetValidationIsRequired()
                 .SetDisplayFormat("yyyy-MM-dd").SetTextAlignment(TextAlignment.Left);

            group.AddDateEditColumn("FINISHDATE", 100).SetLabel("INSPECTIONDATE").SetValidationIsRequired()
                 .SetDisplayFormat("yyyy-MM-dd").SetTextAlignment(TextAlignment.Left);

            group.AddTextBoxColumn("PRODUCTDEFTYPE", 80).SetLabel("PRODUCESAMPLE").SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("INCOMQTY", 80).SetLabel("INCOMEQTY").SetValidationIsRequired().SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("VENDOR", 80).SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("MODEL", 120).SetLabel("MODELNAME").SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("LOTID", 180).SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("MATERIALSTYPE", 80).SetLabel("TYPENAME").SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);

            group = grdMain.View.AddGroupColumn("RELIABILITYTEST");
            group.AddTextBoxColumn("SIZEVALUE", 60).SetLabel("SIZEMEASURE").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("GLOSSVALUE", 60).SetLabel("GLOSSMEASURE").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("LEGVALUE", 60).SetLabel("LEGHEIGHTMEASURE").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("SOLDERABILITY", 60).SetTextAlignment(TextAlignment.Right);

            group = grdMain.View.AddGroupColumn("EXTERIORINSPECTION");
            group.AddTextBoxColumn("DEFECTNAME", 120).SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("DEFECTCOUNT", 60).SetLabel("DEFECTQTYFAIL").SetTextAlignment(TextAlignment.Right);

            group = grdMain.View.AddGroupColumn("");
            group.AddTextBoxColumn("FINALJUDGMENT", 60).SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("INSPECTIONUSER", 60).SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
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
            grdMain.View.InitNewRow += (s, e) =>
            {
                grdMain.View.SetRowCellValue(e.RowHandle, "REGISTERDATE", DateTime.Now);
                grdMain.View.SetRowCellValue(e.RowHandle, "FINISHDATE", DateTime.Now);
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
                    if (CommonFunction.GetFtpImageFileToByte(@"FULLLIST/Materials_Shield.xlsx", true) is byte[] excelByte)
                    {
                        SmartSpreadSheet sheet = new SmartSpreadSheet();
                        sheet.LoadDocument(excelByte);

                        sheet.BeginUpdate();

                        for (int i = 0; i < grdMain.View.DataRowCount; i++)
                        {
                            DataRow dr = grdMain.View.GetDataRow(i);

                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("B", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "REGISTERDATE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("C", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FINISHDATE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("D", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "PRODUCTDEFTYPE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("E", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "INCOMQTY"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("F", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "VENDOR"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("G", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "MODEL"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("H", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "LOTID"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("I", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "MATERIALSTYPE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("J", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SIZEVALUE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("K", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "GLOSSVALUE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("L", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "LEGVALUE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("M", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SOLDERABILITY"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("N", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "DEFECTNAME"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("O", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "DEFECTCOUNT"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("P", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "INSPECTIONUSER"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("Q", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FINALJUDGMENT"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("R", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "DESCRIPTION"));
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

            ExecuteRule("MaterialsShield", grdMain.GetChangedRows());
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

                if (await SqlExecuter.QueryAsync("GetMaterialsShield", "10001", Conditions.GetValues()) is DataTable dt)
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
