#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;

using DevExpress.Spreadsheet;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.BandedGrid;

using System;
using System.IO;
using System.Data;
using System.Windows.Forms;
using System.Threading.Tasks;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질 관리 > 수입검사 > 부자재
    /// 업  무  설  명  : 적측 부자재, 합자재 Full List
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2020-04-02
    /// 수  정  이  력  : 
    /// 
    /// </summary>
    public partial class SubMaterials : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public SubMaterials()
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
            tabMain.SetLanguageKey(tabPageLaminated, "LAMINATIONSUBMATERIALS");
            tabMain.SetLanguageKey(tabPageSum, "SUMMATERIALS");
            btnFullList.LanguageKey = "FULLLIST";
            btnFullList2.LanguageKey = "FULLLIST";
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region 적층 부자재

            grdLaminated.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdLaminated.View.AddTextBoxColumn("SEQUENCE", 10).SetIsHidden();

            var group = grdLaminated.View.AddGroupColumn("");
            group.AddDateEditColumn("TESTREGISTERDATE", 100).SetValidationIsRequired()
                 .SetDisplayFormat("yyyy-MM-dd").SetTextAlignment(TextAlignment.Left);

            group.AddDateEditColumn("TESTFINISHDATE", 100).SetValidationIsRequired()
                 .SetDisplayFormat("yyyy-MM-dd").SetTextAlignment(TextAlignment.Left);

            group.AddTextBoxColumn("SUMMATERIALMARTER", 150).SetLabel("SUMMATERIALMARKER")
                 .SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);

            group.AddTextBoxColumn("MATERIALTYPE", 120)
                 .SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);

            group.AddTextBoxColumn("MODEL", 120).SetLabel("MODELID")
                 .SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);

            group.AddTextBoxColumn("LOTID", 180)
                 .SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);

            group = grdLaminated.View.AddGroupColumn("EXTERIORINSPECTION");
            group.AddTextBoxColumn("BURR", 80).SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("STAMPED", 80).SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("POLLUTION", 80).SetLabel("FOREIGNANDPOLLUTION").SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("WRINKLE", 80).SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("SCRATCH", 80).SetTextAlignment(TextAlignment.Left);

            group = grdLaminated.View.AddGroupColumn("THICKNESSYM");
            group.AddTextBoxColumn("SPECRANGE", 80).SetLabel("SPECCHANGETYPE").SetTextAlignment(TextAlignment.Left);
            group.AddSpinEditColumn("SPECNO1", 60).SetLabel("#1").SetValueRange(0, 1000).SetTextAlignment(TextAlignment.Right);
            group.AddSpinEditColumn("SPECNO2", 60).SetLabel("#2").SetValueRange(0, 1000).SetTextAlignment(TextAlignment.Right);
            group.AddSpinEditColumn("SPECNO3", 60).SetLabel("#3").SetValueRange(0, 1000).SetTextAlignment(TextAlignment.Right);
            group.AddSpinEditColumn("SPECNO4", 60).SetLabel("#4").SetValueRange(0, 1000).SetTextAlignment(TextAlignment.Right);
            group.AddSpinEditColumn("SPECNO5", 60).SetLabel("#5").SetValueRange(0, 1000).SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("AVERAGE", 40).SetLabel("AVG").SetTextAlignment(TextAlignment.Right);
            
            group = grdLaminated.View.AddGroupColumn("XRFANALYSISPPM");
            group.AddTextBoxColumn("CDVALUE", 90).SetLabel("CDRANGE").SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("PBVALUE", 90).SetLabel("PBRANGE").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("HGVALUE", 90).SetLabel("HGRANGE").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("BRVALUE", 90).SetLabel("BRRANGE").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("CRVALUE", 90).SetLabel("CRRANGE").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("CLVALUE", 90).SetLabel("CLRANGE").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("SBVALUE", 90).SetLabel("SBRANGE").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("SNVALUE", 90).SetLabel("SNRANGE").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("SVALUE", 90).SetLabel("SRANGE").SetTextAlignment(TextAlignment.Right);

            group = grdLaminated.View.AddGroupColumn("");
            group.AddTextBoxColumn("FINALJUDGMENT", 60).SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("INSPECTIONUSER", 60).SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("DESCRIPTION", 260).SetLabel("COMMENT").SetTextAlignment(TextAlignment.Left);

            grdLaminated.View.PopulateColumns();
            grdLaminated.View.BestFitColumns();

            grdLaminated.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;

            #endregion

            #region 합 자채

            grdSumMaterials.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdSumMaterials.View.AddTextBoxColumn("SEQUENCE", 10).SetIsHidden();

            group = grdSumMaterials.View.AddGroupColumn("");
            group.AddDateEditColumn("TESTREGISTERDATE", 100).SetValidationIsRequired()
                 .SetDisplayFormat("yyyy-MM-dd").SetTextAlignment(TextAlignment.Left);

            group.AddDateEditColumn("TESTFINISHDATE", 100).SetValidationIsRequired()
                 .SetDisplayFormat("yyyy-MM-dd").SetTextAlignment(TextAlignment.Left);

            group.AddTextBoxColumn("SUMMATERIALMARKER", 150)
                 .SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);

            group.AddTextBoxColumn("MATERIALTYPE", 120)
                 .SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);

            group.AddTextBoxColumn("INCOMEQTY", 120)
                 .SetValidationIsRequired().SetTextAlignment(TextAlignment.Right);

            group.AddTextBoxColumn("MODEL", 120).SetLabel("MODELID")
                 .SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);

            group.AddTextBoxColumn("LOTID", 180)
                 .SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);

            group = grdSumMaterials.View.AddGroupColumn("EXTERIORINSPECTION");
            group.AddTextBoxColumn("FOREIGNBODY", 80).SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("TAPEJOINT", 80).SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("FOLD", 80).SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("WRINKLE", 80).SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("PRESS", 80).SetTextAlignment(TextAlignment.Left);

            group = grdSumMaterials.View.AddGroupColumn("PVCTHICKNESS");
            group.AddSpinEditColumn("SPECNO1", 60).SetLabel("#1").SetValueRange(0, 1000).SetTextAlignment(TextAlignment.Right);
            group.AddSpinEditColumn("SPECNO2", 60).SetLabel("#2").SetValueRange(0, 1000).SetTextAlignment(TextAlignment.Right);
            group.AddSpinEditColumn("SPECNO3", 60).SetLabel("#3").SetValueRange(0, 1000).SetTextAlignment(TextAlignment.Right);
            group.AddSpinEditColumn("SPECNO4", 60).SetLabel("#4").SetValueRange(0, 1000).SetTextAlignment(TextAlignment.Right);
            group.AddSpinEditColumn("SPECNO5", 60).SetLabel("#5").SetValueRange(0, 1000).SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("AVERAGE", 40).SetLabel("AVG").SetTextAlignment(TextAlignment.Right);

            group = grdSumMaterials.View.AddGroupColumn("SUMTHICKNESS");
            group.AddSpinEditColumn("SUMSPECNO1", 60).SetLabel("#1").SetValueRange(0, 1000).SetTextAlignment(TextAlignment.Right);
            group.AddSpinEditColumn("SUMSPECNO2", 60).SetLabel("#2").SetValueRange(0, 1000).SetTextAlignment(TextAlignment.Right);
            group.AddSpinEditColumn("SUMSPECNO3", 60).SetLabel("#3").SetValueRange(0, 1000).SetTextAlignment(TextAlignment.Right);
            group.AddSpinEditColumn("SUMSPECNO4", 60).SetLabel("#4").SetValueRange(0, 1000).SetTextAlignment(TextAlignment.Right);
            group.AddSpinEditColumn("SUMSPECNO5", 60).SetLabel("#5").SetValueRange(0, 1000).SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("SUMAVERAGE", 40).SetLabel("AVG").SetTextAlignment(TextAlignment.Right);
            
            group = grdSumMaterials.View.AddGroupColumn("");
            group.AddTextBoxColumn("FINALJUDGMENT", 60).SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("INSPECTIONUSER", 60).SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("DESCRIPTION", 260).SetLabel("COMMENT").SetTextAlignment(TextAlignment.Left);

            grdSumMaterials.View.PopulateColumns();
            grdSumMaterials.View.BestFitColumns();

            grdSumMaterials.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;

            #endregion

            RepositoryItemDateEdit edit = new RepositoryItemDateEdit();
            edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            edit.Mask.EditMask = @"(19|20)\d{2}-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[0-1])";
            edit.Mask.UseMaskAsDisplayFormat = true;

            grdLaminated.View.Columns["TESTREGISTERDATE"].ColumnEdit = edit;
            grdLaminated.View.Columns["TESTFINISHDATE"].ColumnEdit = edit;
            grdSumMaterials.View.Columns["TESTREGISTERDATE"].ColumnEdit = edit;
            grdSumMaterials.View.Columns["TESTFINISHDATE"].ColumnEdit = edit;
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            //! 입력값 변경 이벤트
            grdLaminated.View.CellValueChanged += (s, e) => SetValueChange(s as BandedGridView, e);
            grdSumMaterials.View.CellValueChanged += (s, e) => SetValueChange(s as BandedGridView, e);

            //! Row 추가 이벤트
            grdLaminated.View.InitNewRow += (s, e) => SetInitNewRow(s as BandedGridView, e);
            grdSumMaterials.View.InitNewRow += (s, e) => SetInitNewRow(s as BandedGridView, e);

            //! Full List 클릭 이벤트
            btnFullList.Click += (s, e) => SetFullList(grdLaminated);
            btnFullList2.Click += (s, e) => SetFullList(grdSumMaterials);
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            if(tabMain.SelectedTabPageIndex.Equals(0))
            {
                ExecuteRule("LaminatedSubMaterials", grdLaminated.GetChangedRows());
            }
            else
            {
                ExecuteRule("SumMaterials", grdSumMaterials.GetChangedRows());
            }
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

                SmartBandedGrid grid = tabMain.SelectedTabPageIndex.Equals(0) ? grdLaminated : grdSumMaterials;

                grid.View.ClearDatas();

                if (await SqlExecuter.QueryAsync(tabMain.SelectedTabPageIndex.Equals(0) ? "GetLaminatedSubMaterials" : "GetSumMaterials",
                                                 "10001", Conditions.GetValues()) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        grid.View.ClearDatas();
                        ShowMessage("NoSelectData");
                        return;
                    }

                    grid.DataSource = dt;
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

            SmartBandedGrid grid = tabMain.SelectedTabPageIndex.Equals(0) ? grdLaminated : grdSumMaterials;

            grid.View.CheckValidation();

            if (grid.GetChangedRows().Rows.Count.Equals(0))
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 화면내용 지정된 엑셀에 입력해야 내보내기
        /// </summary>
        /// <param name="s">SmartBandedGrid</param>
        private void SetFullList(SmartBandedGrid s)
        {
            try
            {
                if (s.View.RowCount.Equals(0))
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

                string filePath = s.Name.Equals("grdReliability") ?
                                        @"FULLLIST/Laminated_SubMeterials.xlsx" :
                                        @"FULLLIST/SumMaterials.xlsx";

                // 저장된 엑셀을 시트에 넣는다.
                if (CommonFunction.GetFtpImageFileToByte(filePath, true) is byte[] excelByte)
                {
                    SmartSpreadSheet sheet = new SmartSpreadSheet();
                    sheet.LoadDocument(excelByte);

                    sheet.BeginUpdate();

                    if (s.Name.Equals("grdReliability"))
                    {
                        SetLaminatedWorkSheet(sheet, s.DataSource as DataTable);
                    }
                    else
                    {
                        SetSumMaterialWorkSheet(sheet, s.DataSource as DataTable);
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
        }

        /// <summary>
        /// 필드값 변경이벤트
        /// </summary>
        /// <param name="s">BandedGridView</param>
        /// <param name="e">CellValueChangedEventArgs</param>
        private void SetValueChange(BandedGridView s, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (s.FocusedRowHandle < 0)
            {
                return;
            }

            try
            {
                if (e.Column.FieldName.Equals("TESTREGISTERDATE") || e.Column.FieldName.Equals("TESTFINISHDATE"))
                {
                    object startDate = s.GetFocusedRowCellValue("TESTREGISTERDATE");
                    object endDate = s.GetFocusedRowCellValue("TESTFINISHDATE");

                    if (startDate.Equals(DBNull.Value) || endDate.Equals(DBNull.Value))
                    {
                        return;
                    }

                    if (DateTime.Compare(Convert.ToDateTime(startDate), Convert.ToDateTime(endDate)) > 0)
                    {
                        MSGBox.Show(MessageBoxType.Warning, "CheckDate");
                        s.SetRowCellValue(e.RowHandle, "TESTFINISHDATE", startDate);
                    };
                }
            }
            catch
            {
                s.SetRowCellValue(e.RowHandle, "TESTREGISTERDATE", DateTime.Now);
                s.SetRowCellValue(e.RowHandle, "TESTFINISHDATE", DateTime.Now);
            }
        }

        /// <summary>
        /// 신규 Row 생성시 발생 이벤트
        /// </summary>
        /// <param name="s">BandedGridView</param>
        /// <param name="e">InitNewRowEventArgs</param>
        private void SetInitNewRow(BandedGridView s, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            s.SetRowCellValue(e.RowHandle, "TESTREGISTERDATE", DateTime.Now);
            s.SetRowCellValue(e.RowHandle, "TESTFINISHDATE", DateTime.Now);
        }

        /// <summary>
        /// 적층 부자재 엑셀 입력
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="dt"></param>
        private void SetLaminatedWorkSheet(SmartSpreadSheet sheet, DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("B", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "TESTREGISTERDATE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("C", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "TESTFINISHDATE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("D", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SUMMATERIALMARTER"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("E", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "MATERIALTYPE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("F", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "MODEL"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("G", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "LOTID"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("H", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "BURR"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("I", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "STAMPED"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("J", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "POLLUTION"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("K", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "WRINKLE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("L", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SCRATCH"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("M", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SPECRANGE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("N", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SPECNO1"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("O", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SPECNO2"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("P", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SPECNO3"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("Q", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SPECNO4"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("R", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SPECNO5"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("S", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "AVERAGE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("T", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "CDVALUE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("U", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "PBVALUE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("V", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "HGVALUE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("W", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "BRVALUE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("X", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "CRVALUE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("Y", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "CLVALUE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("Z", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SBVALUE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AA", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SNVALUE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AB", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SVALUE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AC", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FINALJUDGMENT"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AD", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "INSPECTIONUSER"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AE", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "DESCRIPTION"));
            }
        }

        /// <summary>
        /// 합자재 엑셀 입력
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="dt"></param>
        private void SetSumMaterialWorkSheet(SmartSpreadSheet sheet, DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("B", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "TESTREGISTERDATE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("C", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "TESTFINISHDATE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("D", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SUMMATERIALMARKER"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("E", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "MATERIALTYPE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("F", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "INCOMEQTY"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("G", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "MODEL"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("H", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "LOTID"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("I", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FOREIGNBODY"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("J", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "TAPEJOINT"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("K", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FOLD"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("L", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "WRINKLE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("M", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "PRESS"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("N", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SPECNO1"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("O", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SPECNO2"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("P", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SPECNO3"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("Q", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SPECNO4"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("R", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SPECNO5"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("S", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "AVERAGE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("T", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SUMSPECNO1"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("U", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SUMSPECNO2"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("V", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SUMSPECNO3"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("W", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SUMSPECNO4"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("X", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SUMSPECNO5"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("Y", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SUMAVERAGE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("Z", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FINALJUDGMENT"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AA", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "INSPECTIONUSER"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AB", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "DESCRIPTION"));
            }
        }

        #endregion
    }
}
