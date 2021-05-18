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
    /// 프 로 그 램 명  : 품질 관리 > 수입검사 > 포장재
    /// 업  무  설  명  : 포장재 Tray Full List
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2020-04-16
    /// 수  정  이  력  : 
    /// 
    /// </summary>
    public partial class TrayParkingMaterials : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public TrayParkingMaterials()
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
            grdMain.LanguageKey = "PARKINGMATERIALS";
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
            group.AddDateEditColumn("TESTREGISTERDATE", 100).SetValidationIsRequired()
                 .SetDisplayFormat("yyyy-MM-dd").SetTextAlignment(TextAlignment.Left);

            group.AddDateEditColumn("TESTFINISHDATE", 100).SetValidationIsRequired()
                 .SetDisplayFormat("yyyy-MM-dd").SetTextAlignment(TextAlignment.Left);

            group.AddTextBoxColumn("TARYMARTER", 80).SetLabel("TRAYMARKER").SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("MATERIALTYPE", 180).SetLabel("MATERIALSTYPE").SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("MODEL", 120).SetLabel("MODELID").SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("LOTID", 180).SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);

            group = grdMain.View.AddGroupColumn("EXTERIORINSPECTION");
            group.AddTextBoxColumn("SCRATCH", 60).SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("STAMPED", 60).SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("BUMP", 60).SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("TRAYMODIFY", 60).SetLabel("TRAYDEFORMATION").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("FOREIGNBODY", 60).SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("BREAK", 60).SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("STAIN", 60).SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("PRESS", 60).SetTextAlignment(TextAlignment.Right);
            
            group = grdMain.View.AddGroupColumn("XRFANALYSISPPM");
            group.AddTextBoxColumn("CDVALUE", 60).SetLabel("CDRANGE").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("PBVALUE", 60).SetLabel("PBRANGE").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("HGVALUE", 60).SetLabel("HGRANGE").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("BRVALUE", 60).SetLabel("BRRANGE").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("CRVALUE", 60).SetLabel("CRRANGE").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("CLVALUE", 60).SetLabel("CLRANGE").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("SBVALUE", 60).SetLabel("SBRANGE").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("SNVALUE", 60).SetLabel("SNRANGE").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("SVALUE", 60).SetLabel("SRANGE").SetTextAlignment(TextAlignment.Right);

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

            grdMain.View.Columns["TESTREGISTERDATE"].ColumnEdit = edit;
            grdMain.View.Columns["TESTFINISHDATE"].ColumnEdit = edit;
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
                grdMain.View.SetRowCellValue(e.RowHandle, "TESTREGISTERDATE", DateTime.Now);
                grdMain.View.SetRowCellValue(e.RowHandle, "TESTFINISHDATE", DateTime.Now);
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
                    if (CommonFunction.GetFtpImageFileToByte(@"FULLLIST/Tray_ParkingMaterials.xlsx", true) is byte[] excelByte)
                    {
                        SmartSpreadSheet sheet = new SmartSpreadSheet();
                        sheet.LoadDocument(excelByte);

                        sheet.BeginUpdate();

                        for (int i = 0; i < grdMain.View.DataRowCount; i++)
                        {
                            DataRow dr = grdMain.View.GetDataRow(i);

                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("B", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "TESTREGISTERDATE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("C", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "TESTFINISHDATE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("D", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "TARYMARTER"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("E", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "MATERIALTYPE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("F", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "MODEL"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("G", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "LOTID"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("H", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SCRATCH"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("I", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "STAMPED"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("J", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "BUMP"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("K", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "TRAYMODIFY"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("L", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FOREIGNBODY"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("M", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "BREAK"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("N", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "STAIN"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("O", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "PRESS"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("P", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "CDVALUE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("Q", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "PBVALUE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("R", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "HGVALUE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("S", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "BRVALUE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("T", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "CRVALUE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("U", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "CLVALUE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("V", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SBVALUE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("W", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SNVALUE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("X", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SVALUE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("Y", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FINALJUDGMENT"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("Z", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "INSPECTIONUSER"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AA", i + 6)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "DESCRIPTION"));
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

            ExecuteRule("ParkingMaterials", grdMain.GetChangedRows());
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

                if (await SqlExecuter.QueryAsync("GetTrayParkingMaterials", "10001", Conditions.GetValues()) is DataTable dt)
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