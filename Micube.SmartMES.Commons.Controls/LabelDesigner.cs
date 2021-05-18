using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UserDesigner;
using DevExpress.XtraReports.UI;
using System.ComponentModel.Design;
using DevExpress.XtraReports.Design;
using DevExpress.XtraReports.Design.Commands;
using DevExpress.XtraTabbedMdi;
using DevExpress.XtraBars.Docking2010.Views.Tabbed;
using DevExpress.XtraReports.UserDesigner.Native;
using DevExpress.XtraVerticalGrid;

namespace Micube.SmartMES.Commons.Controls
{
	public partial class LabelDesigner : UserControl
	{


        public delegate void OnOpenLabelEventHandler(object sender);
        public event OnOpenLabelEventHandler OnOpenLabel;

        private DataTable labelInfoTable;
        private int prevFocusedRowHandle;
        public LabelDesigner()
        {
            InitializeComponent();


            InitializeControl();
            InitializeEvent();
        }

        private void InitializeEvent()
        {
            reportDesigner1.AddService(typeof(IMenuCreationService), new CustomMenuCreationService(reportDesigner1));
            XtraReport.FilterComponentProperties += XtraReport_FilterComponentProperties;
        }

        private void InitializeControl()
        {
            this.barOpen.Enabled = false;

        }
        private void XtraReport_FilterComponentProperties(object sender, FilterComponentPropertiesEventArgs e)
        {
            try
            {
                e.Properties.Remove("PaperKind");
                e.Properties.Remove("PageHeight");
                e.Properties.Remove("PageWidth");
                //e.Properties.Remove("FormattingRules");
                //e.Properties.Remove("FormattingRuleSheet");
                //e.Properties.Remove("StylePriority");
                //e.Properties.Remove("Styles");
                //e.Properties.Remove("StyleSheet");
                //e.Properties.Remove("StyleSheetPath");
                //e.Properties.Remove("Watermark");
                //e.Properties.Remove("DrillDownControl");
                //e.Properties.Remove("DrillDownExpanded");
                //e.Properties.Remove("EditOptions");
                //e.Properties.Remove("ExportOptions");
                //e.Properties.Remove("HorizontalContentSplitting");
                //e.Properties.Remove("InteractiveSorting");
                //e.Properties.Remove("KeepTogether");
                //e.Properties.Remove("KeepTogetherWithDetailReports");
                //e.Properties.Remove("LockedInUserDesigner");
                //e.Properties.Remove("PageBreak");
                //e.Properties.Remove("ProcessDuplicates");
                //e.Properties.Remove("ProcessDuplicatesMode");
                //e.Properties.Remove("ProcessDuplicatesTarget");
                //e.Properties.Remove("ProcessHiddenCellMode");
                //e.Properties.Remove("ProcessNullValues");
                //e.Properties.Remove("ReportUnit");
                //e.Properties.Remove("ScriptLanguage");
                //e.Properties.Remove("ScriptReferences");
                //e.Properties.Remove("ScriptReferencesString");
                //e.Properties.Remove("Scripts");
                //e.Properties.Remove("ScriptSecurityPermissions");
                //e.Properties.Remove("ShowPreviewMarginLines");
                //e.Properties.Remove("VerticalContentSplitting");
                //e.Properties.Remove("Visible");
                //e.Properties.Remove("CalculatedFields");
                //e.Properties.Remove("DataAdapter");
                //e.Properties.Remove("DataBindings");
                //e.Properties.Remove("DataMember");
                //e.Properties.Remove("DataSource");
                //e.Properties.Remove("FilterString");
                //e.Properties.Remove("Lines");
                //e.Properties.Remove("NullValueText");
                //e.Properties.Remove("SortFields");
                //e.Properties.Remove("Summary");
                //e.Properties.Remove("TextFormatString");
                //e.Properties.Remove("XlsxFormatString");
                //e.Properties.Remove("XmlDataPath");
                //e.Properties.Remove("DataSourceSchema");
                //e.Properties.Remove("DesignerOptions");
                //e.Properties.Remove("DrawGrid");
                //e.Properties.Remove("DrawWatermark");
                //e.Properties.Remove("PreviewRowCount");
                //e.Properties.Remove("SnapGridSize");
                //e.Properties.Remove("SnapGridStepCount");
                //e.Properties.Remove("SnappingMode");
                //e.Properties.Remove("SnapLineMargin");
                //e.Properties.Remove("SnapLinePadding");
                //e.Properties.Remove("Bookmark");
                //e.Properties.Remove("BookmarkDuplicateSuppress");
                //e.Properties.Remove("BookmarkParent");
                //e.Properties.Remove("NavigateUrl");
                //e.Properties.Remove("Target");
                //e.Properties.Remove("PrinterName");
                //e.Properties.Remove("RollPaper");
                //e.Properties.Remove("Parameters");
                //e.Properties.Remove("RequestParameters");
                //e.Properties.Remove("ReportPrintOptions");
                //e.Properties.Remove("RightToLeft");
                //e.Properties.Remove("RightToLeftLayout");
                //e.Properties.Remove("ShowPrintMarginsWarning");
                //e.Properties.Remove("ShowPrintStatusDialog");
                //e.Properties.Remove("Bands");
                //e.Properties.Remove("SubBands");
                ////e.Properties.Remove("Parent");
                //e.Properties.Remove("Band");
                //e.Properties.Remove("CanHaveChildren");
                //e.Properties.Remove("ComponentStorage");
                //e.Properties.Remove("Container");
                //e.Properties.Remove("Controls");
                //e.Properties.Remove("ControlType");
                //e.Properties.Remove("CrossBandControls");
                //e.Properties.Remove("CurrentRowIndex");
                //e.Properties.Remove("DetailPrintCount");
                //e.Properties.Remove("DetailPrintCountOnEmptyDataSource");
                //e.Properties.Remove("DocumentExportMode");
                //e.Properties.Remove("Dpi");
                //e.Properties.Remove("DrillDownKey");
                //e.Properties.Remove("EvenStyleName");
                //e.Properties.Remove("EventsInfo");
                //e.Properties.Remove("EventsScriptManager");
                //e.Properties.Remove("Expanded");
                //e.Properties.Remove("ExpressionBindings");
                //e.Properties.Remove("Extensions");
                //e.Properties.Remove("FormattingRuleLinks");
                //e.Properties.Remove("GridSize");
                //e.Properties.Remove("HasChildren");
                //e.Properties.Remove("Index");
                //e.Properties.Remove("IsDisposed");
                //e.Properties.Remove("IsSingleChild");
                //e.Properties.Remove("MasterReport");
                //e.Properties.Remove("NextCell");
                //e.Properties.Remove("ObjectStorage");
                //e.Properties.Remove("OddStyleName");
                //e.Properties.Remove("Parent");
                //e.Properties.Remove("ParentStyleUsing");
                //e.Properties.Remove("PreviousCell");
                //e.Properties.Remove("PrintingSystem");
                //e.Properties.Remove("PrintOnEmptyDataSource");
                //e.Properties.Remove("RepeatCountOnEmptyDataSource");
                //e.Properties.Remove("RootReport");
                //e.Properties.Remove("ScriptsSource");
                //e.Properties.Remove("ShowDesignerHints");
                //e.Properties.Remove("ShowExportWarnings");
                //e.Properties.Remove("ShowPrintingWarnings");
                //e.Properties.Remove("ShrinkSubReportIconArea");
                //e.Properties.Remove("Site");
                //e.Properties.Remove("SnapToGrid");
                //e.Properties.Remove("SourceUrl");
                //e.Properties.Remove("StyleName");
                //e.Properties.Remove("Version");
                ////e.Properties.Remove("Name");
                //e.Properties.Remove("TextTrimming");


            }
            catch (Exception ex)
            {

            }
        }

        public DataTable LabelInfoTable { get => labelInfoTable; set => labelInfoTable = value; }
        public int PrevFocusedRowHandle { get => prevFocusedRowHandle; set => prevFocusedRowHandle = value; }
        public bool IsOpenButtonEnable { get => this.barOpen.Enabled; set => this.barOpen.Enabled = value; }

        public DevExpress.XtraReports.UserDesigner.XRDesignMdiController GetDesignControl()
        {
             return reportDesigner1;
        }

        private void barOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(this.OnOpenLabel != null)
                this.OnOpenLabel(this);
        }
    }

    public class ClosingReportCommandHandler : ICommandHandler
    {
        XRDesignMdiController controller;
        public delegate void OnClosingEventHadnler(ReportCommand command, object[] args);
        public event OnClosingEventHadnler OnClosing;
        public ClosingReportCommandHandler(XRDesignMdiController controller)
        {
            this.controller = controller;
        }
        public bool CanHandleCommand(ReportCommand command, ref bool useNextHandler)
        {
            useNextHandler = !(command == ReportCommand.Closing);
            return !useNextHandler;
        }

        public void HandleCommand(ReportCommand command, object[] args)
        {
            if (OnClosing != null)
                OnClosing(command, args);
        }
    }


    class CustomMenuCreationService : IMenuCreationService
    {
        XRDesignMdiController controller;
        XtraReport ActiveReport
        {
            get
            {
                return controller.ActiveDesignPanel != null ? controller.ActiveDesignPanel.Report : null;
            }
        }
        CommandID showGridCommandID;
        CommandID hideGridCommandID;
        public CustomMenuCreationService(XRDesignMdiController controller)
        {
            this.controller = controller;
            Guid guid = Guid.NewGuid();
            showGridCommandID = new CommandID(guid, 0);
            hideGridCommandID = new CommandID(guid, 1);
        }
        public void ProcessMenuItems(MenuKind menuKind, MenuItemDescriptionCollection items)
        {
            if (menuKind == MenuKind.Selection)
            {

                List<MenuItemDescription> itemList = items.Where(s => s.Text != null).ToList();


                //items.Remove(itemList.Where(s => s.Text.Equals("Edit and Reorder Bands...")).FirstOrDefault());
                //items.Remove(itemList.Where(s => s.Text.Equals("View &Code")).FirstOrDefault());
                //items.Remove(itemList.Where(s => s.Text.Equals("Add &Sub-Band")).FirstOrDefault());
                //items.Remove(itemList.Where(s => s.Text.Equals("Insert Detail Report")).FirstOrDefault());
                //items.Remove(itemList.Where(s => s.Text.Equals("Add To Gallery")).FirstOrDefault());
                //items.Remove(itemList.Where(s => s.Text.Equals("P&roperties")).FirstOrDefault());
                //items.Remove(itemList.Where(s => s.Text.Equals("Design in Report Wizard...")).FirstOrDefault());
                //items.Remove(itemList.Where(s => s.Text.Equals("Edit Bindings...")).FirstOrDefault());
                //items.Remove(itemList.Where(s => s.Text.Equals("Add &Sub-Band")).FirstOrDefault());
                //items.Remove(itemList.Where(s => s.Text.Equals("Insert &Band")).FirstOrDefault());

            }
        }
        public MenuCommandDescription[] GetCustomMenuCommands()
        {
            return new MenuCommandDescription[0];
        }

    
    }
}
