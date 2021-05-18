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
    /// 프 로 그 램 명  : 품질 관리 > 수입검사 > 자재 Connector
    /// 업  무  설  명  : 자재 Connector Full List
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2020-04-10
    /// 수  정  이  력  : 
    /// 
    /// </summary>
    public partial class MaterialsConnector : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public MaterialsConnector()
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
            grdMain.LanguageKey = "MATERIALSCONNECTOR";
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
            group.AddTextBoxColumn("REPORTCOUNT", 60).SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);

            group.AddDateEditColumn("REGISTERDATE", 100).SetLabel("RECEPTDATE").SetValidationIsRequired()
                 .SetDisplayFormat("yyyy-MM-dd").SetTextAlignment(TextAlignment.Left);

            group.AddDateEditColumn("FINISHDATE", 100).SetValidationIsRequired()
                 .SetDisplayFormat("yyyy-MM-dd").SetTextAlignment(TextAlignment.Left);

            group.AddTextBoxColumn("CUSTOMER", 80).SetLabel("COMPANYCLIENT").SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("MODEL", 120).SetLabel("MODELNAME").SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("MATERIALSTYPE", 80).SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("MATERIALSNAME", 180).SetLabel("COMPONENTITEMNAME").SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("VENDOR", 80).SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("AGENT", 80).SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("LOTID", 180).SetValidationIsRequired().SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("INCOMQTY", 80).SetLabel("INCOMEQTY").SetValidationIsRequired().SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("MOQ", 80).SetLabel("INSPECTQTY").SetValidationIsRequired().SetTextAlignment(TextAlignment.Right);

            group = grdMain.View.AddGroupColumn("EXTERIORINSPECTION");
            group.AddTextBoxColumn("BREAKAGE", 60).SetLabel("TOOLREWORKBROKEN").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("POLLUTION", 60).SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("DEFORMATION", 60).SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("FOREIGNBOYD", 60).SetLabel("FOREIGNBODY").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("DISCOLORATION", 60).SetTextAlignment(TextAlignment.Right);

            group = grdMain.View.AddGroupColumn("SIZEINSPNOTE");
            group.AddTextBoxColumn("XVALUE", 60).SetLabel("CONNECTORXTYPE1").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("YVALUE", 60).SetLabel("CONNECTORYTYPE1").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("LINEAR", 60).SetLabel("CONNECTORLINEARTYPE1").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("PINPITCH", 60).SetLabel("CONNECTORPINPITCHTYPE1").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("TVALUE", 60).SetLabel("CONNECTORTTYPE1").SetTextAlignment(TextAlignment.Right);

            group = grdMain.View.AddGroupColumn("MATERIALIMPORTLBLSUBTITLE");
            group.AddTextBoxColumn("CDVALUE", 60).SetLabel("CONNECTORCDRANGETYPE1").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("PBVALUE", 60).SetLabel("CONNECTORPBRANGETYPE1").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("HGVALUE", 60).SetLabel("CONNECTORHGRANGETYPE1").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("BRVALUE", 60).SetLabel("CONNECTORBRRANGETYPE1").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("CRVALUE", 60).SetLabel("CONNECTORCRRANGETYPE1").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("CLVALUE", 60).SetLabel("CONNECTORCLRANGETYPE1").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("SBVALUE", 60).SetLabel("CONNECTORSBRANGETYPE1").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("SNVALUE", 60).SetLabel("CONNECTORSNRANGETYPE1").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("SVALUE", 60).SetLabel("CONNECTORSRANGETYPE1").SetTextAlignment(TextAlignment.Right);

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
                    if (CommonFunction.GetFtpImageFileToByte(@"FULLLIST/Materials_Connector.xlsx", true) is byte[] excelByte)
                    {
                        SmartSpreadSheet sheet = new SmartSpreadSheet();
                        sheet.LoadDocument(excelByte);

                        sheet.BeginUpdate();

                        for (int i = 0; i < grdMain.View.DataRowCount; i++)
                        {
                            DataRow dr = grdMain.View.GetDataRow(i);

                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("B", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "REPORTCOUNT"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("C", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "REGISTERDATE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("D", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FINISHDATE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("E", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "CUSTOMER"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("F", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "MODEL"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("G", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "MATERIALSTYPE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("H", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "MATERIALSNAME"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("I", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "VENDOR"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("J", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "AGENT"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("K", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "LOTID"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("L", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "INCOMQTY"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("M", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "MOQ"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("N", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "BREAKAGE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("O", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "POLLUTION"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("P", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "DEFORMATION"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("Q", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FOREIGNBOYD"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("R", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "DISCOLORATION"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("S", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "XVALUE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("T", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "YVALUE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("U", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "LINEAR"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("V", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "PINPITCH"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("W", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "TVALUE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("X", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "CDVALUE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("Y", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "PBVALUE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("Z", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "HGVALUE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AA", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "BRVALUE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AB", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "CRVALUE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AC", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "CLVALUE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AD", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SBVALUE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AE", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SNVALUE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AF", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "SVALUE"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AG", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "FINALJUDGMENT"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AH", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "INSPECTIONUSER"));
                            sheet.Document.Worksheets.ActiveWorksheet.Cells[string.Concat("AI", i + 7)].SetValueFromText(DefectMapHelper.StringByDataRowObejct(dr, "DESCRIPTION"));
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

            ExecuteRule("MaterialsConnector", grdMain.GetChangedRows());
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

                if (await SqlExecuter.QueryAsync("GetMaterialsConnector", "10001", Conditions.GetValues()) is DataTable dt)
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