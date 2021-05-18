#region using

using DevExpress.XtraReports.UI;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UserDesigner;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

using System.Runtime.InteropServices;
using DevExpress.XtraReports.Design;
using System.Drawing.Design;
using DevExpress.XtraReports.UserDesigner.Native;
using Micube.SmartMES.Commons.Controls;
using System.ComponentModel.Design;
using DevExpress.XtraReports.Design.Commands;
using DevExpress.XtraBars.Docking2010.Views.Tabbed;
using Micube.SmartMES.StandardInfo.Popup;
using DevExpress.XtraVerticalGrid;
using DevExpress.Utils.Menu;

#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > Setup > 라벨마스터
    /// 업  무  설  명  : 고객별 라벨을 디자인하고 관리하는 화면
    /// 생    성    자  : 정승원
    /// 생    성    일  : 2019-05-29
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class LabelMaster : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        //라벨 양식 저장 테이블
        //  DataTable _dtSend = null;

        private ClosingReportCommandHandler labelClosingEvent;
        #endregion

        #region 생성자

        public LabelMaster()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();


            InitializeLabelDesigner();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeClassGrid();
            InitializeGrid();

            InitializeEvent();
        }

        /// <summary>        
        /// 라벨 그룹 그리드 초기화
        /// </summary>
        private void InitializeClassGrid()
        {
            grdLabelClassList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            //라벨 그룹 ID
            grdLabelClassList.View.AddTextBoxColumn("LABELCLASSID", 150)
                .SetValidationIsRequired()
                .SetValidationKeyColumn();
            //라벨 그룹명
            grdLabelClassList.View.AddTextBoxColumn("LABELCLASSNAME", 200)
                .SetValidationIsRequired()
                .SetValidationKeyColumn();
            //라벨 그룹 타입
            grdLabelClassList.View.AddComboBoxColumn("LABELCLASSTYPE", new SqlQuery("GetTypeList", "10001", "CODECLASSID=LabelClassType", $"LANGUAGE={UserInfo.Current.LanguageType}"));
            //설명
            grdLabelClassList.View.AddTextBoxColumn("DESCRIPTION", 150);
            //회사
            grdLabelClassList.View.AddTextBoxColumn("ENTERPRISEID")
                .SetIsHidden()
                .SetDefault(UserInfo.Current.Enterprise);
            //SITE
            grdLabelClassList.View.AddTextBoxColumn("PLANTID")
                .SetIsHidden()
                .SetDefault(UserInfo.Current.Plant);

            //유효상태, 생성자, 수정자...
            grdLabelClassList.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
            grdLabelClassList.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
            grdLabelClassList.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
            grdLabelClassList.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
            grdLabelClassList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);

            grdLabelClassList.View.PopulateColumns();
        }

        /// <summary>        
        /// 라벨 디자이너 Tab 속성 초기화
        /// </summary>
        private void InitializeLabelDesigner()
        {
            this.labelDesigner1.GetDesignControl().SetCommandVisibility(DevExpress.XtraReports.UserDesigner.ReportCommand.ShowScriptsTab, DevExpress.XtraReports.UserDesigner.CommandVisibility.None);
            this.labelDesigner1.GetDesignControl().SetCommandVisibility(DevExpress.XtraReports.UserDesigner.ReportCommand.ShowHTMLViewTab, DevExpress.XtraReports.UserDesigner.CommandVisibility.None);
            this.labelDesigner1.GetDesignControl().SetCommandVisibility(DevExpress.XtraReports.UserDesigner.ReportCommand.ShowPreviewTab, DevExpress.XtraReports.UserDesigner.CommandVisibility.None);
            this.labelClosingEvent = new ClosingReportCommandHandler(this.labelDesigner1.GetDesignControl());
            this.labelDesigner1.GetDesignControl().AddCommandHandler(this.labelClosingEvent);
        }


        /// <summary>        
        /// 라벨 양식 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdLabelDefList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdLabelDefList.View.OptionsSelection.EnableAppearanceFocusedRow = false;

            //라벨 ID
            grdLabelDefList.View.AddTextBoxColumn("LABELDEFID", 150)
                .SetValidationIsRequired()
                .SetValidationKeyColumn();
            //라벨명
            grdLabelDefList.View.AddTextBoxColumn("LABELDEFNAME", 200);
            //라벨 그룹
            grdLabelDefList.View.AddComboBoxColumn("LABELCLASSID", new SqlQuery("GetLabelClassList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "LABELCLASSNAME", "LABELCLASSID")
                .SetValidationIsRequired()
                .SetValidationKeyColumn()
                .SetIsRefreshByOpen(true);
            //라벨 타입
            grdLabelDefList.View.AddComboBoxColumn("LABELTYPE", new SqlQuery("GetTypeList", "10001", "CODECLASSID=LabelType", $"LANGUAGE={UserInfo.Current.LanguageType}"));
            //쿼리 타입
            grdLabelDefList.View.AddComboBoxColumn("QUERYTYPE", new SqlQuery("GetTypeList", "10001", "CODECLASSID=QueryType", $"LANGUAGE={UserInfo.Current.LanguageType}"));
            //쿼리 ID
            grdLabelDefList.View.AddTextBoxColumn("QUERYID", 180);
            //쿼리 버전
            grdLabelDefList.View.AddTextBoxColumn("QUERYVERSION", 80);
            //설명
            grdLabelDefList.View.AddTextBoxColumn("DESCRIPTION", 150);
            // Page Height
            grdLabelDefList.View.AddSpinEditColumn("PAGEHEIGHT", 80)
                .SetValidationIsRequired();
            // Page Width
            grdLabelDefList.View.AddSpinEditColumn("PAGEWIDTH", 80)
                .SetValidationIsRequired();
            //라벨 데이터
            grdLabelDefList.View.AddTextBoxColumn("LABELDATA")
                .SetIsHidden();
            //파일이름
            grdLabelDefList.View.AddTextBoxColumn("FILENAME")
                .SetIsHidden();
            //파일확장자
            grdLabelDefList.View.AddTextBoxColumn("FILEEXT")
                .SetIsHidden();
            //회사
            grdLabelDefList.View.AddTextBoxColumn("ENTERPRISEID")
                .SetIsHidden()
                .SetDefault(UserInfo.Current.Enterprise);

            //유효상태, 생성자, 수정자...
            grdLabelDefList.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
            grdLabelDefList.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
            grdLabelDefList.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
            grdLabelDefList.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
            grdLabelDefList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);

            grdLabelDefList.View.PopulateColumns();

        }
        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdLabelDefList.View.AddingNewRow += View_AddingNewRow;
            grdLabelDefList.View.RowClick += View_RowClick;
            labelDesigner1.GetDesignControl().AnyDocumentActivated += LabelMaster_AnyDocumentActivated;
            labelDesigner1.GetDesignControl().DesignPanelLoaded += LabelMaster_DesignPanelLoaded;
            labelClosingEvent.OnClosing += ClosingReportCommandHandler_OnClosing;
            labelDesigner1.OnOpenLabel += LabelDesigner1_OnOpenLabel;

        }

        private void LabelDesigner1_OnOpenLabel(object sender)
        {

            DataTable loadLabelTable = ((DataTable)grdLabelDefList.DataSource).Copy();

            if (this.labelDesigner1.LabelInfoTable != null)
            {
                if (this.labelDesigner1.LabelInfoTable.Rows.Count > 0)
                {
                    DataRow row = this.labelDesigner1.LabelInfoTable.Rows[0];

                    DataRow deleteRow = loadLabelTable.Rows.OfType<DataRow>().Where(s => s["LABELDEFID"].ToString().Equals(row["LABELDEFID"].ToString())).FirstOrDefault();

                    loadLabelTable.Rows.Remove(deleteRow);
                }
            }

            LabelMasterOpenPopup popup = new LabelMasterOpenPopup();
            popup.OnLoadLabel += Popup_OnLoadLabel;
            popup.FormBorderStyle = FormBorderStyle.Sizable;
            popup.Owner = this;
            popup.SetLabelDefDataTable(loadLabelTable);
            popup.ShowDialog();
        }

        private void Popup_OnLoadLabel(DataRow loadDataRow)
        {
            XRDesignPanel designPanel = labelDesigner1.GetDesignControl().ActiveDesignPanel;
            DataTable dt = labelDesigner1.LabelInfoTable;

            if (dt != null)
            {
                byte[] labelData = loadDataRow["LABELDATA"] as byte[];
                if (labelData != null)
                {
                    if (labelData.Length > 0)
                    {
                        //라벨 Form 열기
                        using (MemoryStream ms = new MemoryStream(labelData))
                        {
                            ms.Write(labelData, 0, labelData.Length);
                            labelDesigner1.GetDesignControl().ActiveDesignPanel.OpenReport(ms);

                            designPanel.ExecCommand(ReportCommand.Zoom);

                            ZoomService zoom = (ZoomService)designPanel.GetService(typeof(ZoomService));
                            zoom.ZoomFactor = 0.5f;

                        }
                    }

                }
            }

        }

        private void ClosingReportCommandHandler_OnClosing(ReportCommand command, object[] args)
        {
            if (args.Length >= 2)
            {
                System.ComponentModel.CancelEventArgs cancel = args[1] as System.ComponentModel.CancelEventArgs;
                if (args[0] is XRDesignPanelContainerControl && cancel != null)
                {

                    if (labelDesigner1.GetDesignControl().ActiveDesignPanel != null)
                    {
                        if (labelDesigner1.GetDesignControl().ActiveDesignPanel.ReportState == ReportState.Changed)
                        {
                            DialogResult result = ShowMessage(MessageBoxButtons.YesNoCancel, "ExistWorkingLabel", grdLabelDefList.View.GetRowCellValue(labelDesigner1.PrevFocusedRowHandle, "LABELDEFNAME").ToString());

                            switch (result)
                            {
                                case DialogResult.Cancel:
                                    cancel.Cancel = true;
                                    return;
                                case DialogResult.Yes:

                                    OnToolbarSaveClick();

                                    break;
                                case DialogResult.No:
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private void LabelMaster_AnyDocumentActivated(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {
            XRDesignPanel designPanel = labelDesigner1.GetDesignControl().ActiveDesignPanel;
            DataTable dt = labelDesigner1.LabelInfoTable;

            if (dt != null)
            {
                byte[] labelData = dt.Rows[0]["LABELDATA"] as byte[];
                if (labelData != null)
                {
                    if (labelData.Length > 0)
                    {
                        //라벨 Form 열기
                        using (MemoryStream ms = new MemoryStream(labelData))
                        {
                            ms.Write(labelData, 0, labelData.Length);
                            labelDesigner1.GetDesignControl().ActiveDesignPanel.OpenReport(ms);
                            
                            if (grdLabelDefList.View.FocusedRowHandle > -1)
                            {
                                DataRow row = grdLabelDefList.View.GetFocusedDataRow();

                                int pageHeight = Format.GetInteger(row["PAGEHEIGHT"]);
                                int pageWidth = Format.GetInteger(row["PAGEWIDTH"]);

                                labelDesigner1.GetDesignControl().ActiveDesignPanel.Report.PageHeight = pageHeight;
                                labelDesigner1.GetDesignControl().ActiveDesignPanel.Report.PageWidth = pageWidth;

                                labelDesigner1.GetDesignControl().ActiveDesignPanel.Refresh();
                            }

                            designPanel.ExecCommand(ReportCommand.Zoom);

                            ZoomService zoom = (ZoomService)designPanel.GetService(typeof(ZoomService));
                            zoom.ZoomFactor = 0.5f;

                        }
                    }
                    else
                        CreateLabelDesigner(designPanel);
                }
                else
                    CreateLabelDesigner(designPanel);

                //초기 세팅
                XRTabbedMdiManager manager = labelDesigner1.GetDesignControl().XtraTabbedMdiManager;

                (manager.View as TabbedView).DocumentGroupProperties.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InTabControlHeader;
                (manager.View as TabbedView).DocumentGroupProperties.CloseTabOnMiddleClick = DevExpress.XtraTabbedMdi.CloseTabOnMiddleClick.Never;
                (manager.View as TabbedView).DocumentGroupProperties.HeaderButtonsShowMode = DevExpress.XtraTab.TabButtonShowMode.Never;
                (manager.View as TabbedView).PopupMenuShowing += LabelMaster_PopupMenuShowing;
                (manager.View as TabbedView).DocumentProperties.AllowFloat = false;

                IToolboxService ts = (IToolboxService)designPanel.GetService(typeof(IToolboxService));
                // Get a collection of toolbox items.  
                ToolboxItemCollection coll = ts.GetToolboxItems();
                // Iterate through toolbox items.  
                foreach (ToolboxItem item in coll)
                {
                    if ((item as LocalizableToolboxItem).TypeName == typeof(XRCharacterComb).FullName ||
                        (item as LocalizableToolboxItem).TypeName == typeof(XRRichText).FullName ||
                        (item as LocalizableToolboxItem).TypeName == typeof(XRChart).FullName ||
                        (item as LocalizableToolboxItem).TypeName == typeof(XRGauge).FullName ||
                        (item as LocalizableToolboxItem).TypeName == typeof(XRSparkline).FullName ||
                        (item as LocalizableToolboxItem).TypeName == typeof(XRPivotGrid).FullName ||
                        (item as LocalizableToolboxItem).TypeName == typeof(XRSubreport).FullName ||
                        (item as LocalizableToolboxItem).TypeName == typeof(XRTableOfContents).FullName ||
                        (item as LocalizableToolboxItem).TypeName == typeof(XRPageInfo).FullName ||
                        (item as LocalizableToolboxItem).TypeName == typeof(XRPageBreak).FullName ||
                            (item as LocalizableToolboxItem).TypeName == typeof(XRCrossBandLine).FullName ||
                                (item as LocalizableToolboxItem).TypeName == typeof(XRCrossBandBox).FullName)
                        ts.RemoveToolboxItem(item);
                }
            }
        }



        private void View_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

            if (e.Clicks >= 2)
            {
                int currentFocusedRow = grdLabelDefList.View.FocusedRowHandle;
                if (currentFocusedRow < 0) return;

                if (currentFocusedRow == this.labelDesigner1.PrevFocusedRowHandle)
                    return;

                DataRow focusedRow = grdLabelDefList.View.GetFocusedDataRow();
                if (focusedRow.RowState == DataRowState.Added && focusedRow.HasErrors)
                {
                    ShowMessageBox(focusedRow.RowError, "Error", MessageBoxButtons.OK);
                    return;
                }


                labelDesigner1.IsOpenButtonEnable = true;

                XRDesignPanel designPanel = labelDesigner1.GetDesignControl().ActiveDesignPanel;

                if (designPanel == null)
                {

                    labelDesigner1.PrevFocusedRowHandle = currentFocusedRow;

                    labelDesigner1.LabelInfoTable = ((DataTable)grdLabelDefList.DataSource).Clone();
                    labelDesigner1.LabelInfoTable.ImportRow(focusedRow);

                    labelDesigner1.GetDesignControl().CreateNewReport();
                    
                }
                else
                {
                    if (designPanel.ReportState == ReportState.Changed)
                    {
                        DialogResult result = ShowMessage(MessageBoxButtons.YesNoCancel, "ExistWorkingLabel", grdLabelDefList.View.GetRowCellValue(labelDesigner1.PrevFocusedRowHandle, "LABELDEFNAME").ToString());

                        switch (result)
                        {
                            case DialogResult.Cancel:
                                return;
                            case DialogResult.Yes:

                                OnToolbarSaveClick();

                                break;
                            case DialogResult.No:
                                labelDesigner1.GetDesignControl().ActiveDesignPanel.ReportState = ReportState.None;
                                break;
                            default:
                                break;
                        }
                    }

                    labelDesigner1.GetDesignControl().ActiveDesignPanel.ExecCommand(ReportCommand.Close);
                    labelDesigner1.GetDesignControl().ActiveDesignPanel.CloseReport();


                    labelDesigner1.PrevFocusedRowHandle = currentFocusedRow;

                    labelDesigner1.LabelInfoTable = ((DataTable)grdLabelDefList.DataSource).Clone();
                    labelDesigner1.LabelInfoTable.ImportRow(focusedRow);

                    labelDesigner1.GetDesignControl().CreateNewReport();
                }

            }

        }



        /// <summary>
        /// 신규 디자이너 생성 시 필요없는 툴바 제거 로직
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelMaster_DesignPanelLoaded(object sender, DesignerLoadedEventArgs e)
        {
            //2019-08-29 박윤신
            // Label Designer Caption 이름 추가
            int currentFocusedRow = grdLabelDefList.View.FocusedRowHandle;
            if (currentFocusedRow < 0) return;

            DataRow focusedRow = grdLabelDefList.View.GetFocusedDataRow();

            if (!string.IsNullOrWhiteSpace(focusedRow["LABELDEFNAME"].ToString()))
            {
                (sender as XRDesignPanel).Report.DisplayName = focusedRow["LABELDEFNAME"].ToString();
            }
            else
            {
                (sender as XRDesignPanel).Report.DisplayName = "New Label Designer";
            }
        }

    

        private void LabelMaster_PopupMenuShowing(object sender, DevExpress.XtraBars.Docking2010.Views.PopupMenuShowingEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// Row 포커스 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            labelDesigner1.PrevFocusedRowHandle = e.PrevFocusedRowHandle;
        }


        /// <summary>
        /// 라벨 양식 Row 추가 시 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {

            DataTable changed = grdLabelDefList.GetChangedRows();
            List<DataRow> rows = changed.AsEnumerable().Where(c => c.Field<string>("_STATE_") == "added").ToList();
            if (rows.Count == 1)
            {
                //   rows[0]["_STATE_"] = "added";
            }
            else
            {
                args.IsCancel = true;
                return;
            }

        }
        #endregion

        #region 툴바
    
           protected override void OnToolbarClick(object sender, EventArgs e)
     { 
            base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;
   
            if(btn.Name.ToString().Equals("TestOutput"))
            {
                if (labelDesigner1.GetDesignControl().ActiveDesignPanel != null  && labelDesigner1.GetDesignControl().ActiveDesignPanel.Report != null)
                {
                    XtraReport report = labelDesigner1.GetDesignControl().ActiveDesignPanel.Report;
                    XtraReport preview = new XtraReport();

                    DataTable dt = labelDesigner1.LabelInfoTable;

                    string fileName = dt.Rows[0]["LABELDEFID"].ToString();

                    string filePath = Application.StartupPath + "\\" + fileName + ".repx";
                    string fileExt = Path.GetExtension(filePath);

                    labelDesigner1.GetDesignControl().ActiveDesignPanel.SaveReport(filePath);

                    byte[] bytes = null;

                    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        bytes = new byte[fs.Length];
                        fs.Read(bytes, 0, bytes.Length);
                    }

        
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {

                        ms.Write(bytes, 0, bytes.Length);

                        preview = XtraReport.FromStream(ms);
                        preview.Tag = dt.Rows[0]["LABELDEFID"].ToString();


                        DataRow row = grdLabelDefList.View.GetFocusedDataRow();

                        int pageHeight = Format.GetInteger(row["PAGEHEIGHT"]);
                        int pageWidth = Format.GetInteger(row["PAGEWIDTH"]);

                        preview.PageHeight = pageHeight;
                        preview.PageWidth = pageWidth;


                    }
                    preview.DisplayName = report.DisplayName;

                    PrintLabelPopup pop = new PrintLabelPopup(preview, dt);
                    pop.ShowDialog();
                }

                //if (dt != null)
                //{
                //    byte[] labelData = dt.Rows[0]["LABELDATA"] as byte[];
                //    if (labelData != null)
                //    {
                //        if (labelData.Length > 0)
                //        {

                //            //라벨 Form 열기
                //            using (MemoryStream ms = new MemoryStream(labelData))
                //            {
                //                ms.Write(labelData, 0, labelData.Length);
                //                labelDesigner1.GetDesignControl().ActiveDesignPanel.OpenReport(ms);



                //                if (grdLabelDefList.View.FocusedRowHandle > -1)
                //                {
                //                    DataRow row = grdLabelDefList.View.GetFocusedDataRow();

                //                    int pageHeight = Format.GetInteger(row["PAGEHEIGHT"]);
                //                    int pageWidth = Format.GetInteger(row["PAGEWIDTH"]);

                //                    labelDesigner1.GetDesignControl().ActiveDesignPanel.Report.PageHeight = pageHeight;
                //                    labelDesigner1.GetDesignControl().ActiveDesignPanel.Report.PageWidth = pageWidth;

                //                    labelDesigner1.GetDesignControl().ActiveDesignPanel.Refresh();
                //                }

                //                designPanel.ExecCommand(ReportCommand.Zoom);

                //                ZoomService zoom = (ZoomService)designPanel.GetService(typeof(ZoomService));
                //                zoom.ZoomFactor = 0.5f;

                //            }
                //        }


                //    }
                //}


                /*
                report.SaveLayout(Path.Combine(Application.StartupPath, "temp", "temp.repx"));

                XtraReport loadReport = XtraReport.FromFile()
                loadReport.fil
                ImageExportOptions imageOptions = report.ExportOptions.Image;
                imageOptions.Format = ImageFormat.Bmp;
                report.ExportToImage(Path.Combine(Application.StartupPath, "temp", "temp.bmp"), ImageFormat.Bmp);
                //report.PrintingSystem.ExportToImage(Path.Combine(Application.StartupPath, "temp","temp.bmp"), ImageFormat.Bmp);

                ConvertImage();
                */




                //using (MemoryStream ms = new MemoryStream())
                //{
                //    report.SaveLayout(ms);
                //    ms.Seek(0, SeekOrigin.Begin);
                //    XtraReport loadreport = XtraReport.FromStream(ms);

                //    ImageExportOptions imageOptions = loadreport.ExportOptions.Image;
                //    imageOptions.Format = ImageFormat.Bmp;

                //    loadreport.ExportToImage(Commons.SystemCommonClass.ImageTempPath, imageOptions);
                //    ConvertImage();

                //    /*
                //    FileInfo fileDel = new FileInfo(@"D:\temp.bmp");

                //    if (fileDel.Exists) //삭제할 파일이 있는지
                //    {
                //        fileDel.Delete(); //없어도 에러안남
                //    }
                //    */
                //}


            }
        




    }
        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();
       

            // TODO : 저장 Rule 변경
            int index = tabPartition.SelectedTabPageIndex;
            switch (index)
            {
                case 0:
                    DataTable dtLabelList = grdLabelClassList.GetChangedRows();
                    ExecuteRule("LabelClass", dtLabelList);
                    break;
                case 1:
                    string filePath = "";

                    DataTable _dtSend = this.grdLabelDefList.GetChangedRows();

                    if (this.labelDesigner1.GetDesignControl().XtraTabbedMdiManager != null)
                    {
                        if (labelDesigner1.GetDesignControl().XtraTabbedMdiManager.View.ActiveDocument != null
                        && labelDesigner1.GetDesignControl().XtraTabbedMdiManager.View.ActiveDocument.IsActive)
                        {
                            _dtSend = this.labelDesigner1.LabelInfoTable;

                            string fileName = _dtSend.Rows[0]["LABELDEFID"].ToString();


                            DataTable dtOri = this.grdLabelDefList.DataSource as DataTable;
                            if (dtOri != null)
                            {
                                DataRow drOri = dtOri.Rows.OfType<DataRow>().Where(s => s["LABELDEFID"].ToString().Equals(fileName)).FirstOrDefault();

                                if (drOri != null)
                                {
                                    byte[] oriLabelBytes = drOri["LABELDATA"] as byte[];


                                    filePath = Application.StartupPath + "\\" + fileName + ".repx";
                                    string fileExt = Path.GetExtension(filePath);

                                    labelDesigner1.GetDesignControl().ActiveDesignPanel.SaveReport(filePath);

                                    byte[] bytes = null;

                                    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                                    {
                                        bytes = new byte[fs.Length];
                                        fs.Read(bytes, 0, bytes.Length);
                                    }
                                    //if (oriLabelBytes == null)
                                    //{
                                    //    drOri["FILENAME"] = fileName;
                                    //    drOri["FILEEXT"] = fileExt;
                                    //    drOri["LABELDATA"] = bytes;
                                    //}
                                    //else
                                    //{
                                    //    if (!oriLabelBytes.Equals(bytes))
                                    //    {
                                    drOri["FILENAME"] = fileName;
                                    drOri["FILEEXT"] = fileExt;
                                    drOri["LABELDATA"] = bytes;
                                    _dtSend.Rows[0]["FILENAME"] = fileName;
                                    _dtSend.Rows[0]["FILEEXT"] = fileExt;
                                    _dtSend.Rows[0]["LABELDATA"] = bytes;
                                    //    }
                                    //}

                                }
                            }
                        }

                    }


                    _dtSend = this.grdLabelDefList.GetChangedRows();

                    ExecuteRule("LabelDefinition", _dtSend);

                    //저장 테이블 초기화
                    _dtSend = null;

                    //.repx 파일 삭제
                    if (File.Exists(filePath))
                    {
                        try
                        {
                            File.Delete(filePath);
                        }
                        catch (IOException e)
                        {
                            throw MessageException.Create(e.Message);
                        }
                    }
                    break;
            }//switch (index)

        }

    

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
            values.Add("Enterpriseid", UserInfo.Current.Enterprise);
            values.Add("PlantId", UserInfo.Current.Plant);

            int index = tabPartition.SelectedTabPageIndex;
            DataTable dataTable = null;
            switch (index)
            {
                case 0:
                    dataTable = await QueryAsync("SelectLabelClass", "10001", values);
                    if (dataTable.Rows.Count < 1) //
                    {
                        // 조회할 데이터가 없습니다.
                        ShowMessage("NoSelectData");
                    }
                    grdLabelClassList.DataSource = dataTable;
                    break;
                case 1:

                    if (labelDesigner1.GetDesignControl().ActiveDesignPanel != null)
                    {
                        if (labelDesigner1.GetDesignControl().ActiveDesignPanel.ReportState == ReportState.Changed)
                        {
                            DialogResult result = ShowMessage(MessageBoxButtons.YesNoCancel, "ExistWorkingLabel", grdLabelDefList.View.GetRowCellValue(labelDesigner1.PrevFocusedRowHandle, "LABELDEFNAME").ToString());

                            switch (result)
                            {
                                case DialogResult.Cancel:
                                    return;
                                case DialogResult.Yes:

                                    OnToolbarSaveClick();

                                    break;
                                case DialogResult.No:
                                    labelDesigner1.GetDesignControl().ActiveDesignPanel.ReportState = ReportState.None;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    grdLabelDefList.View.ClearDatas();


                    dataTable = await QueryAsync("SelectLabelDefinition", "10001", values);
                    if (dataTable.Rows.Count < 1) //
                    {
                        // 조회할 데이터가 없습니다.
                        ShowMessage("NoSelectData");
                    }
                    grdLabelDefList.DataSource = dataTable;

                    labelDesigner1.LabelInfoTable = null;
                    labelDesigner1.PrevFocusedRowHandle = -1;

                    XRDesignPanel designPanel = labelDesigner1.GetDesignControl().ActiveDesignPanel;

                    if (designPanel != null)
                    {
                        labelDesigner1.GetDesignControl().ActiveDesignPanel.ExecCommand(ReportCommand.Close);
                        labelDesigner1.GetDesignControl().ActiveDesignPanel.CloseReport();
                    }

                    break;
            }

        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
        }

        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            int index = tabPartition.SelectedTabPageIndex;
            DataTable changed = new DataTable();
            switch (index)
            {
                case 0://라벨 그룹
                    grdLabelClassList.View.CheckValidation();
                    changed = grdLabelClassList.GetChangedRows();

                    if (changed.Rows.Count == 0)
                    {
                        // 저장할 데이터가 존재하지 않습니다.
                        throw MessageException.Create("NoSaveData");
                    }
                    break;
                case 1://라벨 양식
                    grdLabelDefList.View.CheckValidation();
                    changed = grdLabelDefList.GetChangedRows();



                    //               if (changed.Rows.Count == 0)
                    //               {
                    //                   // 저장할 데이터가 존재하지 않습니다.
                    //                   throw MessageException.Create("NoSaveData");
                    //               }


                    //               if (changed.Rows.Count != 0)
                    //{
                    //	_dtSend = changed.Copy();
                    //}
                    //else if (changed.Rows.Count == 0 && designPanel.ReportState == ReportState.Changed)
                    //{
                    //	_dtSend = (grdLabelDefList.DataSource as DataTable).Clone();
                    //	DataRow focusedRow = grdLabelDefList.View.GetFocusedDataRow();

                    //	_dtSend.Columns.Add("_STATE_", typeof(string));

                    //	_dtSend.ImportRow(focusedRow);

                    //	foreach(DataRow row in _dtSend.Rows)
                    //	{
                    //		row["_STATE_"] = "modified";
                    //	}
                    //	_dtSend.AcceptChanges();
                    //}
                    //else
                    //{
                    //	// 저장할 데이터가 존재하지 않습니다.
                    //	throw MessageException.Create("NoSaveData");
                    //}
                    break;
            }
        }

        #endregion

        #region Private Function


        private static void ConvertImage()
        {
            string bitmapFilePath = @"D:\temp.bmp";
            int w, h;
            Bitmap b = new Bitmap(Commons.SystemCommonClass.ImageTempPath);
            w = b.Width; h = b.Height;

            //System.Drawing.Imaging.BitmapData bmpData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format1bppIndexed);

            //b.UnlockBits(bmpData);
            //byte[] bitmapFileData = System.IO.File.ReadAllBytes(bitmapFilePath);
            //int fileSize = bitmapFileData.Length;

            //int bitmapDataOffset = int.Parse(bitmapFileData[10].ToString()); ;
            //int width = w; // int.Parse(bitmapFileData[18].ToString()); ;
            //int height = h; // int.Parse(bitmapFileData[22].ToString()); ;
            //int bitsPerPixel = int.Parse(bitmapFileData[28].ToString());
            //int bitmapDataLength = bitmapFileData.Length - bitmapDataOffset;
            //double widthInBytes = Math.Ceiling(width / 8.0);

            //while (widthInBytes % 4 != 0)
            //{
            //    widthInBytes++;
            //}

            // Copy over the actual bitmap data without header data  
            //byte[] bitmap = new byte[bitmapDataLength];

            //Buffer.BlockCopy(bitmapFileData, bitmapDataOffset, bitmap, 0, bitmapDataLength);

            // Invert bitmap colors
            //for (int i = 0; i < bitmapDataLength; i++)
            //{
            //    bitmap[i] ^= 0xFF;
            //}

            // Create ASCII ZPL string of hexadecimal bitmap data
            //string ZPLImageDataString = BitConverter.ToString(bitmap);
            //ZPLImageDataString = ZPLImageDataString.Replace("-", string.Empty);

            string ZPLImageDataString = Micube.SmartMES.Commons.ZPLPrint.GetZPLIIImage(b, 20, 20); //ZPLUtil. SendImageToPrinter(1,1,b);

            // Add new line every 1023 chars characters
            //string ZPLImageDataStringWithNewLine = SpliceText(ZPLImageDataString, 1023);

            // Create ZPL command to print image
            StringBuilder ZPLCommand = new StringBuilder();

            try
            {
                ZPLCommand.AppendLine("^XA");
                //ZPLCommand.AppendLine("^FO10,10");
                //string imgstr = string.Format("~DGR:{3}.GRF,{0},{1},{2}", h, w, ZPLImageDataString, "cn");
                ZPLCommand.AppendLine(ZPLImageDataString);
                //string imgstr = "^GFA," +
                //                bitmapDataLength.ToString() + "," +
                //                bitmapDataLength.ToString() + "," +
                //                widthInBytes.ToString() + "," +
                //                //System.Environment.NewLine +
                //                ZPLImageDataString;
                //ZPLCommand.AppendLine(imgstr);
                ZPLCommand.AppendLine("^XZ");

                Commons.ZPLPrint.SendStringToPrinter(ZPLCommand.ToString());
                b.Dispose();
            }
            catch (Exception ex)
            {

            }

            /*
            ZPLCommand += "^XA";
            ZPLCommand += "^FO20,20";
            ZPLCommand +=
            "^GFA," +
           bitmapDataLength.ToString() + "," +
           bitmapDataLength.ToString() + "," +
           widthInBytes.ToString() + "," +
            //System.Environment.NewLine +
            ZPLImageDataString;
            
            //ZPLImageDataStringWithNewLine;

            ZPLCommand += "^XZ";
            

            bool printResult = false;
            StringBuilder printScript = new StringBuilder();
            try
            {
                printScript.AppendLine("^XA");

                // position of Logo
                printScript.AppendLine("^FO50,50");
                string imgstr = "^GFA,2755,2755,19,,:::::::::X06,X0F,W01F8,W01FC,W03FC,W03FE,W07FF,W07DF,W0F9F,W0F8F8,V01F0F8,V01F07C,:V01E07C,V03E07C,V03E03C,:V03E03E,M03EM03E03EM0FE,M03FEL03E03EL07FE,M03FF8K03E03EK03FFE,M03FFEK03E03CK07FFE,M03IF8J03E07CJ01IFE,M03EFFCJ03E07CJ03FF3E,M03E1FFJ01F07CJ0FF87C,M03E07F8I01F07CI01FE07C,M01F03FCI01F0F8I03FC07C,M01F00FEI01F0F8I07F00FC,M01F807FJ0F8F8I0FE00F8,N0F803FJ0F9FI01FC01F8,N0FC01F8I07FFI01F801F,N07C00F8I07FEI03F003F,N07E00FCI03FEI07E007E,N03F007CI03FCI0FC00FE,N01F803EI01FCI0FC01FC,O0FC03EJ0F8001F803F8,O0FF01FJ0FI03F007F,O07F81FN07E01FE,O03FE1FN07E07FC,O01FF8F8M0FC1FF8,P07IF8L01KF,P03UFC,Q0UF,Q03SFC,R07QFE,S0F8K01FC,S0F8K01F8,S0F8K03F,S0F8K07E,:S0FL0FC,R01FK01F8,:R03FK03F,M0F8I03EK07EN0F,K01IFC007EK0FCL01IFC,K0KF807CK0FCL0KF8,J03KFE0FCJ01F8K03KFE,J0MF9F8J03FL0MF,I01FFE03JF8J03FK03FFE03FFC,I07FEI03IFK07EK07FEI03FE,I0FF8J0FFEK0FCK0FFK0FF,I0FEK03FCJ01F8J01FEK03F8,I0FFK07F8J01F8J03FEK07F8,I07FCI01FFK03FK07FFCI01FF,I03FF800FFEK07EK0JFI0FFC,J0MF8K07EK0FDMF8,J07LFL0FCJ01F87KFE,J01KFCK01F8J03F01KF8,K03IFEL03FK03E007IFE,L07FEM03FK07EI07FE,V07EK07C,V0FCK07C,V0FCK0F8,U01F8K0F8,U03FL0F8,U07EL0F8,U07EL0F,U0FCK01F,T01F8K01F,R01QFC,R0SF8,Q07TF,P01UFC,P03UFE,P0FFE1F8M0FBFF8,O01FF03FN0F87FC,O03FC07EN0F81FE,O07F007EI07J07C0FF,O0FE00FCI0F8I07C03F8,N01FC01F8001F8I07E01F8,N03F803FI01FCI03E00FC,N03F003FI03FEI03F007E,N07E007EI07FEI01F803E,N0FC00FCI07FFI01F803F,N0FC01F8I0FDFJ0FC01F8,N0F803F8I0F9F8I07E00F8,M01F007FJ0F8F8I03F00F8,M01F00FEI01F0F8I03FC07C,M03F03FCI01F07CI01FE07C,M03E0FF8I01F07CJ0FF87C,M03E3FFJ03E07CJ03FF3E,M03IFCJ03E07CJ01IFE,M03IF8J03E03CK07FFE,M03FFEK03E03CK03FFE,M03FF8K03E03EL07FE,M03FCL03E03EL01FE,V03E03E,V03E03C,:V03E07C,:V01F07C,:V01F0F8,W0F8F8,W0F9F8,W0FDF,W07FF,W07FE,W03FE,W01FC,W01F8,X0F8,X07,,:::::::::^FS";
                printScript.AppendLine(imgstr); //logo

                printScript.AppendLine("^XZ");

                printResult = Commons.ZPLPrint.SendStringToPrinter(printScript.ToString());
            }
            catch (Exception ex)
            {
            }
            */
        }


        private void CreateLabelDesigner(XRDesignPanel designPanel)
        {
            if (grdLabelDefList.View.FocusedRowHandle < 0)
                return;

            DataRow row = grdLabelDefList.View.GetFocusedDataRow();

            int pageHeight = Format.GetInteger(row["PAGEHEIGHT"]);
            int pageWidth = Format.GetInteger(row["PAGEWIDTH"]);

            //    XtraReport currentReport = designPanel.Report;//new XtraReport();
            designPanel.Report.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            //designPanel.Report.PageHeight = 600;
            //designPanel.Report.PageWidth = 800;
            designPanel.Report.PageHeight = pageHeight;
            designPanel.Report.PageWidth = pageWidth;
            designPanel.Report.ShowPrintMarginsWarning = false;
            designPanel.Report.ShowPreviewMarginLines = false;
            designPanel.Report.Margins.Left = 0;
            designPanel.Report.Margins.Right = 0;
            designPanel.Report.Margins.Top = 0;
            designPanel.Report.Margins.Bottom = 0;


            Band topBand = designPanel.Report.Bands.GetBandByType(typeof(TopMarginBand));
            if (topBand == null)
                designPanel.Report.Bands.Add(new TopMarginBand());
            topBand = designPanel.Report.Bands.GetBandByType(typeof(TopMarginBand));
            topBand.HeightF = 0;

            Band bottomBand = designPanel.Report.Bands.GetBandByType(typeof(BottomMarginBand));
            if (bottomBand == null)
                designPanel.Report.Bands.Add(new BottomMarginBand());
            bottomBand = designPanel.Report.Bands.GetBandByType(typeof(BottomMarginBand));
            bottomBand.HeightF = 0;

            Band detailBand = designPanel.Report.Bands.GetBandByType(typeof(DetailBand));
            if (detailBand == null)
                designPanel.Report.Bands.Add(new DetailBand());
            detailBand = designPanel.Report.Bands.GetBandByType(typeof(DetailBand));
            //detailBand.HeightF = 600;
            detailBand.HeightF = pageHeight;

            designPanel.Report.Bands.Remove(topBand);
            designPanel.Report.Bands.Remove(bottomBand);

            designPanel.Report.PrintingSystem.Document.AutoFitToPagesWidth = 1;



            designPanel.ExecCommand(ReportCommand.Zoom);
            ZoomService zoom = (ZoomService)designPanel.GetService(typeof(ZoomService));
            zoom.ZoomFactor = 0.5f;

            designPanel.Refresh();




        }
        #endregion




    }
}
