namespace Micube.SmartMES.QualityAnalysis
{ 
    partial class ucRateEquipmentType
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grbMain = new Micube.Framework.SmartControls.SmartGroupBox();
            this.chartMain = new Micube.Framework.SmartControls.SmartChart();
            this.layoutSheet = new Micube.Framework.SmartControls.SmartLayoutControl();
            this.grdMain = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.cboDefectSub = new Micube.Framework.SmartControls.SmartCheckedComboBox();
            this.cboDefectGroup = new Micube.Framework.SmartControls.SmartCheckedComboBox();
            this.btnFilter = new Micube.Framework.SmartControls.SmartButton();
            this.smartLayoutControlGroup1 = new Micube.Framework.SmartControls.SmartLayoutControlGroup();
            this.layoutFilter = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutDefectGroup = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutDefectSub = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutEmpty = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grbMain)).BeginInit();
            this.grbMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutSheet)).BeginInit();
            this.layoutSheet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboDefectSub.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDefectGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDefectGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDefectSub)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutEmpty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 2;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grbMain, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.layoutSheet, 1, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 1;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(998, 218);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // grbMain
            // 
            this.grbMain.Controls.Add(this.chartMain);
            this.grbMain.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.grbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbMain.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.grbMain.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.grbMain.LanguageKey = "";
            this.grbMain.Location = new System.Drawing.Point(3, 3);
            this.grbMain.Name = "grbMain";
            this.grbMain.ShowBorder = true;
            this.grbMain.Size = new System.Drawing.Size(343, 212);
            this.grbMain.TabIndex = 1;
            // 
            // chartMain
            // 
            this.chartMain.AutoLayout = false;
            this.chartMain.CacheToMemory = true;
            this.chartMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartMain.Legend.Name = "Default Legend";
            this.chartMain.Location = new System.Drawing.Point(2, 31);
            this.chartMain.Name = "chartMain";
            this.chartMain.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartMain.Size = new System.Drawing.Size(339, 179);
            this.chartMain.TabIndex = 0;
            // 
            // layoutSheet
            // 
            this.layoutSheet.Controls.Add(this.grdMain);
            this.layoutSheet.Controls.Add(this.cboDefectSub);
            this.layoutSheet.Controls.Add(this.cboDefectGroup);
            this.layoutSheet.Controls.Add(this.btnFilter);
            this.layoutSheet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutSheet.Location = new System.Drawing.Point(352, 3);
            this.layoutSheet.Name = "layoutSheet";
            this.layoutSheet.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1270, 0, 650, 400);
            this.layoutSheet.Root = this.smartLayoutControlGroup1;
            this.layoutSheet.Size = new System.Drawing.Size(643, 212);
            this.layoutSheet.TabIndex = 2;
            this.layoutSheet.Text = "smartLayoutControl1";
            // 
            // grdMain
            // 
            this.grdMain.Caption = "";
            this.grdMain.IsUsePaging = false;
            this.grdMain.LanguageKey = "";
            this.grdMain.Location = new System.Drawing.Point(0, 28);
            this.grdMain.Margin = new System.Windows.Forms.Padding(0);
            this.grdMain.Name = "grdMain";
            this.grdMain.ShowBorder = true;
            this.grdMain.Size = new System.Drawing.Size(643, 184);
            this.grdMain.TabIndex = 13;
            // 
            // cboDefectSub
            // 
            this.cboDefectSub.DisplayMember = null;
            this.cboDefectSub.LabelText = "불량 그룹";
            this.cboDefectSub.LanguageKey = null;
            this.cboDefectSub.Location = new System.Drawing.Point(449, 2);
            this.cboDefectSub.Margin = new System.Windows.Forms.Padding(0);
            this.cboDefectSub.Name = "cboDefectSub";
            this.cboDefectSub.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDefectSub.Properties.DisplayMember = null;
            this.cboDefectSub.Properties.ShowHeader = true;
            this.cboDefectSub.Properties.ValueMember = null;
            this.cboDefectSub.ShowHeader = true;
            this.cboDefectSub.Size = new System.Drawing.Size(113, 20);
            this.cboDefectSub.StyleController = this.layoutSheet;
            this.cboDefectSub.TabIndex = 12;
            this.cboDefectSub.ValueMember = null;
            // 
            // cboDefectGroup
            // 
            this.cboDefectGroup.DisplayMember = null;
            this.cboDefectGroup.LabelText = "불량 그룹";
            this.cboDefectGroup.LanguageKey = null;
            this.cboDefectGroup.Location = new System.Drawing.Point(267, 2);
            this.cboDefectGroup.Margin = new System.Windows.Forms.Padding(0);
            this.cboDefectGroup.Name = "cboDefectGroup";
            this.cboDefectGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDefectGroup.Properties.DisplayMember = null;
            this.cboDefectGroup.Properties.ShowHeader = true;
            this.cboDefectGroup.Properties.ValueMember = null;
            this.cboDefectGroup.ShowHeader = true;
            this.cboDefectGroup.Size = new System.Drawing.Size(113, 20);
            this.cboDefectGroup.StyleController = this.layoutSheet;
            this.cboDefectGroup.TabIndex = 11;
            this.cboDefectGroup.ValueMember = null;
            // 
            // btnFilter
            // 
            this.btnFilter.AllowFocus = false;
            this.btnFilter.IsBusy = false;
            this.btnFilter.IsWrite = false;
            this.btnFilter.Location = new System.Drawing.Point(566, 2);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnFilter.Size = new System.Drawing.Size(75, 22);
            this.btnFilter.StyleController = this.layoutSheet;
            this.btnFilter.TabIndex = 4;
            this.btnFilter.Text = "Filter";
            this.btnFilter.TooltipLanguageKey = "";
            // 
            // smartLayoutControlGroup1
            // 
            this.smartLayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.smartLayoutControlGroup1.GroupBordersVisible = false;
            this.smartLayoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutFilter,
            this.layoutDefectGroup,
            this.layoutDefectSub,
            this.layoutEmpty,
            this.layoutControlItem2});
            this.smartLayoutControlGroup1.Name = "Root";
            this.smartLayoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.smartLayoutControlGroup1.Size = new System.Drawing.Size(643, 212);
            this.smartLayoutControlGroup1.TextVisible = false;
            // 
            // layoutFilter
            // 
            this.layoutFilter.Control = this.btnFilter;
            this.layoutFilter.Location = new System.Drawing.Point(564, 0);
            this.layoutFilter.MaxSize = new System.Drawing.Size(79, 26);
            this.layoutFilter.MinSize = new System.Drawing.Size(79, 26);
            this.layoutFilter.Name = "layoutFilter";
            this.layoutFilter.Size = new System.Drawing.Size(79, 26);
            this.layoutFilter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutFilter.TextSize = new System.Drawing.Size(0, 0);
            this.layoutFilter.TextVisible = false;
            // 
            // layoutDefectGroup
            // 
            this.layoutDefectGroup.Control = this.cboDefectGroup;
            this.layoutDefectGroup.Location = new System.Drawing.Point(200, 0);
            this.layoutDefectGroup.MaxSize = new System.Drawing.Size(182, 26);
            this.layoutDefectGroup.MinSize = new System.Drawing.Size(182, 26);
            this.layoutDefectGroup.Name = "layoutDefectGroup";
            this.layoutDefectGroup.Size = new System.Drawing.Size(182, 26);
            this.layoutDefectGroup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutDefectGroup.Text = "group";
            this.layoutDefectGroup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutDefectGroup.TextSize = new System.Drawing.Size(60, 14);
            this.layoutDefectGroup.TextToControlDistance = 5;
            this.layoutDefectGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutDefectSub
            // 
            this.layoutDefectSub.Control = this.cboDefectSub;
            this.layoutDefectSub.Location = new System.Drawing.Point(382, 0);
            this.layoutDefectSub.MaxSize = new System.Drawing.Size(182, 26);
            this.layoutDefectSub.MinSize = new System.Drawing.Size(182, 26);
            this.layoutDefectSub.Name = "layoutDefectSub";
            this.layoutDefectSub.Size = new System.Drawing.Size(182, 26);
            this.layoutDefectSub.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutDefectSub.Text = "sub";
            this.layoutDefectSub.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutDefectSub.TextSize = new System.Drawing.Size(60, 14);
            this.layoutDefectSub.TextToControlDistance = 5;
            // 
            // layoutEmpty
            // 
            this.layoutEmpty.AllowHotTrack = false;
            this.layoutEmpty.Location = new System.Drawing.Point(0, 0);
            this.layoutEmpty.Name = "layoutEmpty";
            this.layoutEmpty.Size = new System.Drawing.Size(200, 26);
            this.layoutEmpty.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.grdMain;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 2, 0);
            this.layoutControlItem2.Size = new System.Drawing.Size(643, 186);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // ucRateEquipmentType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.Name = "ucRateEquipmentType";
            this.Size = new System.Drawing.Size(998, 218);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grbMain)).EndInit();
            this.grbMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutSheet)).EndInit();
            this.layoutSheet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboDefectSub.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDefectGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDefectGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDefectSub)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutEmpty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartGroupBox grbMain;
        private Framework.SmartControls.SmartChart chartMain;
        private Framework.SmartControls.SmartLayoutControl layoutSheet;
        private Framework.SmartControls.SmartLayoutControlGroup smartLayoutControlGroup1;
        private Framework.SmartControls.SmartButton btnFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutFilter;
        private DevExpress.XtraLayout.EmptySpaceItem layoutEmpty;
        private Framework.SmartControls.SmartCheckedComboBox cboDefectSub;
        private Framework.SmartControls.SmartCheckedComboBox cboDefectGroup;
        private DevExpress.XtraLayout.LayoutControlItem layoutDefectGroup;
        private DevExpress.XtraLayout.LayoutControlItem layoutDefectSub;
        private Framework.SmartControls.SmartBandedGrid grdMain;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}
