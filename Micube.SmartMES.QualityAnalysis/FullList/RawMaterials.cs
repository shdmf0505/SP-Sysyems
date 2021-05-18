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
    /// 프 로 그 램 명  : 품질 관리 > 수입검사 > 원자재
    /// 업  무  설  명  : 원자재 신뢰성, 부적합 Full List
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2020-04-09
    /// 수  정  이  력  : 
    /// 
    /// </summary>
    public partial class RawMaterials : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public RawMaterials()
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
            tabMain.SetLanguageKey(tabPageReliability, "RAWMATERIALRELIABILITY");
            tabMain.SetLanguageKey(tabPageNonconformity, "RAWMATERIALNONCONFORMITY");
            btnFullList.LanguageKey = "FULLLIST";
            btnFullList2.LanguageKey = "FULLLIST";
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region 원자재 신뢰성

            grdReliability.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdReliability.View.AddTextBoxColumn("SEQUENCE", 10).SetIsHidden();

            var group = grdReliability.View.AddGroupColumn("");
            group.AddDateEditColumn("TESTREGISTERDATE", 100).SetValidationIsRequired()
                 .SetDisplayFormat("yyyy-MM-dd").SetTextAlignment(TextAlignment.Left);

            group.AddDateEditColumn("TESTFINISHDATE", 100).SetValidationIsRequired()
                 .SetDisplayFormat("yyyy-MM-dd").SetTextAlignment(TextAlignment.Left);

            group.AddTextBoxColumn("MATERIALMARKER", 150).SetLabel("RAWMATERIALMARKER")
                 .SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);

            group.AddTextBoxColumn("MATERIALTYPE", 120)
                 .SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);

            group.AddTextBoxColumn("INCOMEQTY", 120)
                 .SetValidationIsRequired().SetTextAlignment(TextAlignment.Right);

            group.AddTextBoxColumn("MODEL", 120).SetLabel("MODELID")
                 .SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);

            group.AddTextBoxColumn("LOTID", 180)
                 .SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);

            group = grdReliability.View.AddGroupColumn("EXTERIORINSPECTIONSPEC");
            group.AddTextBoxColumn("BUMP", 80).SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("STABBED", 80).SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("FOREIGNBODY", 80).SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("STAIN", 80).SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("PICOLOR", 80).SetTextAlignment(TextAlignment.Left);

            group = grdReliability.View.AddGroupColumn("COPPERTHICKNESSYM");
            group.AddTextBoxColumn("SPECRANGEFIRST", 80).SetLabel("SPECCHANGETYPE").SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("FIRSTSPECNO1", 60).SetLabel("#1").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("FIRSTSPECNO2", 60).SetLabel("#2").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("FIRSTSPECNO3", 60).SetLabel("#3").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("FIRSTSPECNO4", 60).SetLabel("#4").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("FIRSTSPECNO5", 60).SetLabel("#5").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("FIRSTAVERAGE", 40).SetLabel("AVG").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("SPECRANGESECOND", 80).SetLabel("SPECCHANGETYPE").SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("SECONDSPECNO1", 60).SetLabel("#1").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("SECONDSPECNO2", 60).SetLabel("#2").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("SECONDSPECNO3", 60).SetLabel("#3").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("SECONDSPECNO4", 60).SetLabel("#4").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("SECONDSPECNO5", 60).SetLabel("#5").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("SECONDAVERAGE", 40).SetLabel("AVG").SetTextAlignment(TextAlignment.Right);

            group = grdReliability.View.AddGroupColumn("SUMTHICKNESSYM");
            group.AddTextBoxColumn("SPECRANGESUM", 80).SetLabel("SPECCHANGETYPE").SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("SUMSPECNO1", 60).SetLabel("#1").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("SUMSPECNO2", 60).SetLabel("#2").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("SUMSPECNO3", 60).SetLabel("#3").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("SUMSPECNO4", 60).SetLabel("#4").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("SUMSPECNO5", 60).SetLabel("#5").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("SUMAVERAGE", 40).SetLabel("AVG").SetTextAlignment(TextAlignment.Right);

            group = grdReliability.View.AddGroupColumn("PEELTESTKGFCM");
            group.AddTextBoxColumn("SPECRANGETEST", 80).SetLabel("SPECCHANGETYPE").SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("TESTSPECNO1", 60).SetLabel("#1").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("TESTSPECNO2", 60).SetLabel("#2").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("TESTSPECNO3", 60).SetLabel("#3").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("TESTSPECNO4", 60).SetLabel("#4").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("TESTSPECNO5", 60).SetLabel("#5").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("TESTAVERAGE", 40).SetLabel("AVG").SetTextAlignment(TextAlignment.Right);

            group = grdReliability.View.AddGroupColumn("RESINFLOWMM");
            group.AddTextBoxColumn("SPECRANGEFLOW", 80).SetLabel("SPECCHANGETYPE").SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("FLOWSPECNO1", 60).SetLabel("#1").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("FLOWSPECNO2", 60).SetLabel("#2").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("FLOWSPECNO3", 60).SetLabel("#3").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("FLOWSPECNO4", 60).SetLabel("#4").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("FLOWSPECNO5", 60).SetLabel("#5").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("FLOWAVERAGE", 40).SetLabel("AVG").SetTextAlignment(TextAlignment.Right);

            group = grdReliability.View.AddGroupColumn("RESISTANCESIGMASPEC");
            group.AddTextBoxColumn("RESISTANCESPECNO1", 60).SetLabel("#1").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("RESISTANCESPECNO2", 60).SetLabel("#2").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("RESISTANCESPECNO3", 60).SetLabel("#3").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("RESISTANCESPECNO4", 60).SetLabel("#4").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("RESISTANCESPECNO5", 60).SetLabel("#5").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("RESISTANCEAVERAGE", 40).SetLabel("AVG").SetTextAlignment(TextAlignment.Right);

            group = grdReliability.View.AddGroupColumn("");
            group.AddTextBoxColumn("VOLTAGESPEC", 40).SetLabel("VOLTAGETESTSPEC").SetTextAlignment(TextAlignment.Left);

            group = grdReliability.View.AddGroupColumn("RESINCONTENTPERSENT");
            group.AddTextBoxColumn("SPECRANGERESIN", 80).SetLabel("SPECCHANGETYPE").SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("RESINMEASUREVALUE", 90).SetLabel("SPCNVALUE").SetTextAlignment(TextAlignment.Right);

            group = grdReliability.View.AddGroupColumn("");
            group.AddTextBoxColumn("TAPINGTESTSPEC", 150).SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("SOLDERRESISTANCESPEC", 150).SetTextAlignment(TextAlignment.Left);

            group = grdReliability.View.AddGroupColumn("CONTRACTIONMMTD");
            group.AddTextBoxColumn("SPECRANGEFACTOR", 80).SetLabel("SPECMDTD").SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("FACTORMDFIRST", 60).SetLabel("MD1").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("FACTORMDSECOND", 60).SetLabel("MD2").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("FACTORTDFIRST", 60).SetLabel("TD1").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("FACTORTDSECOND", 60).SetLabel("TD2").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("FACTORMDAVERAGE", 60).SetLabel("MD(AVG)").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("FACTORTDAVERAGE", 60).SetLabel("MD(AVG)").SetTextAlignment(TextAlignment.Right);

            group = grdReliability.View.AddGroupColumn("XRFANALYSISPPM");
            group.AddTextBoxColumn("CDVALUE", 90).SetLabel("CDRANGE").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("PBVALUE", 90).SetLabel("PBRANGE").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("HGVALUE", 90).SetLabel("HGRANGE").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("BRVALUE", 90).SetLabel("BRRANGE").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("CRVALUE", 90).SetLabel("CRRANGE").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("CLVALUE", 90).SetLabel("CLRANGE").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("SBVALUE", 90).SetLabel("SBRANGE").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("SNVALUE", 90).SetLabel("SNRANGE").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("SVALUE", 90).SetLabel("SRANGE").SetTextAlignment(TextAlignment.Right);

            group = grdReliability.View.AddGroupColumn("");
            group.AddTextBoxColumn("FINALJUDGMENT", 60).SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("INSPECTIONUSER", 60).SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("NOTE", 250).SetLabel("REMARK").SetTextAlignment(TextAlignment.Left);            
            group.AddTextBoxColumn("DESCRIPTION", 260).SetLabel("COMMENT").SetTextAlignment(TextAlignment.Left);

            grdReliability.View.PopulateColumns();
            grdReliability.View.BestFitColumns();

            grdReliability.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;

            #endregion

            #region 원자재 부적합

            grdNonconformity.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdNonconformity.View.AddTextBoxColumn("SEQUENCE", 10).SetIsHidden();

            group = grdNonconformity.View.AddGroupColumn("");
            group.AddDateEditColumn("TESTREGISTERDATE", 100).SetLabel("RECEPTDATE").SetValidationIsRequired()
                 .SetDisplayFormat("yyyy-MM-dd").SetTextAlignment(TextAlignment.Left);

            group.AddDateEditColumn("TESTFINISHDATE", 100).SetLabel("OCCURRENCEDATE").SetValidationIsRequired()
                 .SetDisplayFormat("yyyy-MM-dd").SetTextAlignment(TextAlignment.Left);

            group.AddTextBoxColumn("MATERIALMARKER", 150).SetLabel("RAWMATERIALMARKER")
                 .SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);

            group.AddTextBoxColumn("MATERIALTYPE", 120)
                 .SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);

            group.AddTextBoxColumn("MODEL", 120).SetLabel("MODELID")
                 .SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);

            group.AddTextBoxColumn("LOTID", 180)
                 .SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);

            group.AddTextBoxColumn("DEFECTREASON", 250).SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("ACTIONCONTENT", 200).SetLabel("ACTIONDESCRIPTION").SetTextAlignment(TextAlignment.Left);
            group.AddDateEditColumn("RECEIPTDATE", 80).SetLabel("TRAFFICIMPROVEMENTDATE")
                 .SetDisplayFormat("yyyy-MM-dd").SetTextAlignment(TextAlignment.Left);

            group = grdNonconformity.View.AddGroupColumn("CAREVALUATIONSSPEC");
            group.AddTextBoxColumn("VALIDATIONHISTORY", 120).SetLabel("CARVALDATIONHISTORY").SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("ISEVALUATION", 80).SetLabel("EVALUATIONABNORMALITIES").SetTextAlignment(TextAlignment.Left);

            group = grdNonconformity.View.AddGroupColumn("");
            group.AddTextBoxColumn("TRANSVERSENOTE", 260).SetLabel("TRANSVERSUSTWISTCONTENT").SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("DEFECTPHENOMENON", 260).SetLabel("DEFECTSTATUS").SetTextAlignment(TextAlignment.Left);

            grdNonconformity.View.PopulateColumns();
            grdNonconformity.View.BestFitColumns();

            grdNonconformity.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;

            #endregion

            RepositoryItemDateEdit edit = new RepositoryItemDateEdit();
            edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            edit.Mask.EditMask = @"(19|20)\d{2}-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[0-1])";
            edit.Mask.UseMaskAsDisplayFormat = true;
            
            grdReliability.View.Columns["TESTREGISTERDATE"].ColumnEdit = edit;
            grdReliability.View.Columns["TESTFINISHDATE"].ColumnEdit = edit;
            grdNonconformity.View.Columns["TESTREGISTERDATE"].ColumnEdit = edit;
            grdNonconformity.View.Columns["TESTFINISHDATE"].ColumnEdit = edit;
            grdNonconformity.View.Columns["RECEIPTDATE"].ColumnEdit = edit;
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            //! 입력값 변경 이벤트
            grdReliability.View.CellValueChanged += (s, e) => SetValueChange(s as BandedGridView, e);
            grdNonconformity.View.CellValueChanged += (s, e) => SetValueChange(s as BandedGridView, e);

            //! Row 추가 이벤트
            grdReliability.View.InitNewRow += (s, e) => SetInitNewRow(s as BandedGridView, e);
            grdNonconformity.View.InitNewRow += (s, e) => SetInitNewRow(s as BandedGridView, e);

            //! Full List 클릭 이벤트
            btnFullList.Click += (s, e) => SetFullList(grdReliability);
            btnFullList2.Click += (s, e) => SetFullList(grdNonconformity);
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            if (tabMain.SelectedTabPageIndex.Equals(0))
            {
                ExecuteRule("MaterialsReliability", grdReliability.GetChangedRows());
            }
            else
            {
                ExecuteRule("MaterialsNonconformity", grdNonconformity.GetChangedRows());
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

                SmartBandedGrid grid = tabMain.SelectedTabPageIndex.Equals(0) ? grdReliability : grdNonconformity;

                grid.View.ClearDatas();

                if (await SqlExecuter.QueryAsync(tabMain.SelectedTabPageIndex.Equals(0) ? "GetMaterialsReliability" : "GetMaterialsNonconformity",
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

            SmartBandedGrid grid = tabMain.SelectedTabPageIndex.Equals(0) ? grdReliability : grdNonconformity;

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
                                        @"FULLLIST/Materials_Reliability.xlsx" :
                                        @"FULLLIST/Materials_Nonconformity.xlsx";

                // 저장된 엑셀을 시트에 넣는다.
                if (CommonFunction.GetFtpImageFileToByte(filePath, true) is byte[] excelByte)
                {
                    SmartSpreadSheet sheet = new SmartSpreadSheet();
                    sheet.LoadDocument(excelByte);

                    sheet.BeginUpdate();

                    if (s.Name.Equals("grdReliability"))
                    {
                        SetMaterialsReliability(sheet, s.DataSource as DataTable);
                    }
                    else
                    {
                        SetMaterialsNonconformity(sheet, s.DataSource as DataTable);
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
        /// 원자재 신뢰성 엑셀 입력
        /// </summary>
        /// <param name="sheet">SmartSpreadSheet</param>
        /// <param name="dt">DataTable</param>
        private void SetMaterialsReliability(SmartSpreadSheet sheet, DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("A", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "TESTREGISTERDATE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("B", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "TESTFINISHDATE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("C", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "MATERIALMARKER"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("D", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "MATERIALTYPE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("E", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "INCOMEQTY"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("F", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "MODEL"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("G", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "LOTID"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("H", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "BUMP"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("I", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "STABBED"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("J", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FOREIGNBODY"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("K", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "STAIN"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("L", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "PICOLOR"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("M", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SPECRANGEFIRST"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("N", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FIRSTSPECNO1"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("O", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FIRSTSPECNO2"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("P", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FIRSTSPECNO3"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("Q", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FIRSTSPECNO4"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("R", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FIRSTSPECNO5"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("S", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FIRSTAVERAGE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("T", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SPECRANGESECOND"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("U", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SECONDSPECNO1"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("V", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SECONDSPECNO2"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("W", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SECONDSPECNO3"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("X", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SECONDSPECNO4"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("Y", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SECONDSPECNO5"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("Z", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SECONDAVERAGE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AA", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SPECRANGESUM"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AB", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SUMSPECNO1"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AC", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SUMSPECNO2"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AD", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SUMSPECNO3"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AE", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SUMSPECNO4"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AF", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SUMSPECNO5"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AG", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SUMAVERAGE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AH", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SPECRANGETEST"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AI", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "TESTSPECNO1"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AJ", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "TESTSPECNO2"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AK", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "TESTSPECNO3"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AL", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "TESTSPECNO4"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AM", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "TESTSPECNO5"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AN", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "TESTAVERAGE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AO", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SPECRANGEFLOW"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AP", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FLOWSPECNO1"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AQ", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FLOWSPECNO2"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AR", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FLOWSPECNO3"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AS", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FLOWSPECNO4"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AT", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FLOWSPECNO5"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AU", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FLOWAVERAGE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AV", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "RESISTANCESPECNO1"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AW", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "RESISTANCESPECNO2"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AX", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "RESISTANCESPECNO3"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AY", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "RESISTANCESPECNO4"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AZ", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "RESISTANCESPECNO5"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("BA", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "RESISTANCEAVERAGE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("BB", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "VOLTAGESPEC"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("BC", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SPECRANGERESIN"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("BD", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "RESINMEASUREVALUE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("BE", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "TAPINGTESTSPEC"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("BF", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SOLDERRESISTANCESPEC"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("BG", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SPECRANGEFACTOR"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("BH", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FACTORMDFIRST"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("BI", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FACTORMDSECOND"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("BJ", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FACTORTDFIRST"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("BK", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FACTORTDSECOND"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("BL", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FACTORMDAVERAGE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("BM", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FACTORTDAVERAGE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("BN", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "CDVALUE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("BO", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "PBVALUE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("BP", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "HGVALUE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("BQ", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "BRVALUE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("BR", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "CRVALUE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("BS", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "CLVALUE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("BT", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SBVALUE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("BU", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SNVALUE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("BV", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SVALUE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("BW", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FINALJUDGMENT"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("BX", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "INSPECTIONUSER"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("BY", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "NOTE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("BZ", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "DESCRIPTION"));
            }
        }

        /// <summary>
        /// 원자재 부적합 엑셀 입력
        /// </summary>
        /// <param name="sheet">SmartSpreadSheet</param>
        /// <param name="dt">DataTable</param>
        private void SetMaterialsNonconformity(SmartSpreadSheet sheet, DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("B", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "TESTREGISTERDATE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("C", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "TESTFINISHDATE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("D", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "MATERIALMARKER"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("E", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "MATERIALTYPE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("F", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "MODEL"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("G", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "LOTID"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("H", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "DEFECTREASON"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("I", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "ACTIONCONTENT"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("J", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "RECEIPTDATE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("K", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "VALIDATIONHISTORY"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("L", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "ISEVALUATION"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("M", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "TRANSVERSENOTE"));
                sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("N", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "DEFECTPHENOMENON"));
            }
        }

        #endregion
    }
}