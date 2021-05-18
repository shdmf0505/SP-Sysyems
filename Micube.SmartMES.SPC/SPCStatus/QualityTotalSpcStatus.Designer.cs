namespace Micube.SmartMES.SPC
{
    partial class QualityTotalSpcStatus
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabMain = new Micube.Framework.SmartControls.SmartTabControl();
            this.tabTrend = new DevExpress.XtraTab.XtraTabPage();
            this.splMain = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grpChart = new Micube.Framework.SmartControls.SmartGroupBox();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.ucXBar = new Micube.SmartMES.SPC.UserControl.ucXBar();
            this.grdRawData = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.lblReduction1 = new Micube.Framework.SmartControls.SmartLabel();
            this.lblExtension1 = new Micube.Framework.SmartControls.SmartLabel();
            this.splMainSub01 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.gridTotal = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.splMainSub0101 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.cboDefect = new Micube.Framework.SmartControls.SmartComboBox();
            this.gridDefect = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.cboProduct = new Micube.Framework.SmartControls.SmartComboBox();
            this.gridProduct = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tabControlChart = new DevExpress.XtraTab.XtraTabPage();
            this.tabProcess = new DevExpress.XtraTab.XtraTabPage();
            this.tabRowData = new DevExpress.XtraTab.XtraTabPage();
            this.grdRowData = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tabOverRules = new DevExpress.XtraTab.XtraTabPage();
            this.grdOverRules = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tabTrend.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).BeginInit();
            this.splMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpChart)).BeginInit();
            this.grpChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splMainSub01)).BeginInit();
            this.splMainSub01.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splMainSub0101)).BeginInit();
            this.splMainSub0101.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboDefect.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProduct.Properties)).BeginInit();
            this.tabRowData.SuspendLayout();
            this.tabOverRules.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 508);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(659, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabMain);
            this.pnlContent.Size = new System.Drawing.Size(659, 512);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(964, 541);
            // 
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedTabPage = this.tabTrend;
            this.tabMain.Size = new System.Drawing.Size(659, 512);
            this.tabMain.TabIndex = 0;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabTrend,
            this.tabControlChart,
            this.tabProcess,
            this.tabRowData,
            this.tabOverRules});
            // 
            // tabTrend
            // 
            this.tabTrend.Controls.Add(this.splMain);
            this.tabTrend.Name = "tabTrend";
            this.tabTrend.Padding = new System.Windows.Forms.Padding(3);
            this.tabTrend.Size = new System.Drawing.Size(653, 483);
            this.tabTrend.Text = "Trend";
            // 
            // splMain
            // 
            this.splMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splMain.Horizontal = false;
            this.splMain.Location = new System.Drawing.Point(3, 3);
            this.splMain.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.splMain.Name = "splMain";
            this.splMain.Panel1.Controls.Add(this.grpChart);
            this.splMain.Panel1.Text = "Panel1";
            this.splMain.Panel2.Controls.Add(this.splMainSub01);
            this.splMain.Panel2.Text = "Panel2";
            this.splMain.Size = new System.Drawing.Size(647, 477);
            this.splMain.SplitterPosition = 179;
            this.splMain.TabIndex = 0;
            this.splMain.Text = "smartSpliterContainer1";
            // 
            // grpChart
            // 
            this.grpChart.Controls.Add(this.smartSpliterContainer1);
            this.grpChart.Controls.Add(this.lblReduction1);
            this.grpChart.Controls.Add(this.lblExtension1);
            this.grpChart.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.grpChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpChart.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.grpChart.Location = new System.Drawing.Point(0, 0);
            this.grpChart.Name = "grpChart";
            this.grpChart.ShowBorder = true;
            this.grpChart.Size = new System.Drawing.Size(647, 179);
            this.grpChart.TabIndex = 8;
            this.grpChart.Text = "Chart";
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Horizontal = false;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(2, 31);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.ucXBar);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdRawData);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(643, 146);
            this.smartSpliterContainer1.SplitterPosition = 136;
            this.smartSpliterContainer1.TabIndex = 2;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // ucXBar
            // 
            this.ucXBar.Appearance.BackColor = System.Drawing.Color.White;
            this.ucXBar.Appearance.Options.UseBackColor = true;
            this.ucXBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucXBar.Location = new System.Drawing.Point(0, 0);
            this.ucXBar.Name = "ucXBar";
            this.ucXBar.Size = new System.Drawing.Size(643, 136);
            this.ucXBar.TabIndex = 7;
            // 
            // grdRawData
            // 
            this.grdRawData.Caption = "분석 자료";
            this.grdRawData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRawData.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)(((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete)));
            this.grdRawData.IsUsePaging = false;
            this.grdRawData.LanguageKey = null;
            this.grdRawData.Location = new System.Drawing.Point(0, 0);
            this.grdRawData.Margin = new System.Windows.Forms.Padding(0);
            this.grdRawData.Name = "grdRawData";
            this.grdRawData.ShowBorder = false;
            this.grdRawData.ShowStatusBar = false;
            this.grdRawData.Size = new System.Drawing.Size(643, 5);
            this.grdRawData.TabIndex = 3;
            this.grdRawData.UseAutoBestFitColumns = false;
            // 
            // lblReduction1
            // 
            this.lblReduction1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReduction1.ImageOptions.Image = global::Micube.SmartMES.SPC.Properties.Resources.reduction;
            this.lblReduction1.Location = new System.Drawing.Point(620, 6);
            this.lblReduction1.Name = "lblReduction1";
            this.lblReduction1.Size = new System.Drawing.Size(22, 21);
            this.lblReduction1.TabIndex = 1;
            // 
            // lblExtension1
            // 
            this.lblExtension1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExtension1.ImageOptions.Image = global::Micube.SmartMES.SPC.Properties.Resources.extension;
            this.lblExtension1.Location = new System.Drawing.Point(592, 5);
            this.lblExtension1.Name = "lblExtension1";
            this.lblExtension1.Size = new System.Drawing.Size(27, 23);
            this.lblExtension1.TabIndex = 0;
            // 
            // splMainSub01
            // 
            this.splMainSub01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splMainSub01.Horizontal = false;
            this.splMainSub01.Location = new System.Drawing.Point(0, 0);
            this.splMainSub01.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.splMainSub01.Name = "splMainSub01";
            this.splMainSub01.Panel1.Controls.Add(this.gridTotal);
            this.splMainSub01.Panel1.Text = "Panel1";
            this.splMainSub01.Panel2.Controls.Add(this.splMainSub0101);
            this.splMainSub01.Panel2.Text = "Panel2";
            this.splMainSub01.Size = new System.Drawing.Size(647, 293);
            this.splMainSub01.SplitterPosition = 84;
            this.splMainSub01.TabIndex = 0;
            this.splMainSub01.Text = "smartSpliterContainer2";
            // 
            // gridTotal
            // 
            this.gridTotal.Caption = "";
            this.gridTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTotal.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.gridTotal.IsUsePaging = false;
            this.gridTotal.LanguageKey = "DefectTotal";
            this.gridTotal.Location = new System.Drawing.Point(0, 0);
            this.gridTotal.Margin = new System.Windows.Forms.Padding(0);
            this.gridTotal.Name = "gridTotal";
            this.gridTotal.ShowBorder = true;
            this.gridTotal.ShowStatusBar = false;
            this.gridTotal.Size = new System.Drawing.Size(647, 84);
            this.gridTotal.TabIndex = 1;
            this.gridTotal.UseAutoBestFitColumns = false;
            // 
            // splMainSub0101
            // 
            this.splMainSub0101.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splMainSub0101.Horizontal = false;
            this.splMainSub0101.Location = new System.Drawing.Point(0, 0);
            this.splMainSub0101.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.splMainSub0101.Name = "splMainSub0101";
            this.splMainSub0101.Panel1.Controls.Add(this.cboDefect);
            this.splMainSub0101.Panel1.Controls.Add(this.gridDefect);
            this.splMainSub0101.Panel1.Text = "Panel1";
            this.splMainSub0101.Panel2.Controls.Add(this.cboProduct);
            this.splMainSub0101.Panel2.Controls.Add(this.gridProduct);
            this.splMainSub0101.Panel2.Text = "Panel2";
            this.splMainSub0101.Size = new System.Drawing.Size(647, 204);
            this.splMainSub0101.SplitterPosition = 167;
            this.splMainSub0101.TabIndex = 0;
            this.splMainSub0101.Text = "smartSpliterContainer3";
            // 
            // cboDefect
            // 
            this.cboDefect.LabelText = null;
            this.cboDefect.LanguageKey = null;
            this.cboDefect.Location = new System.Drawing.Point(163, 3);
            this.cboDefect.Name = "cboDefect";
            this.cboDefect.PopupWidth = 0;
            this.cboDefect.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDefect.Properties.Appearance.Options.UseFont = true;
            this.cboDefect.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDefect.Properties.NullText = "";
            this.cboDefect.ShowHeader = true;
            this.cboDefect.Size = new System.Drawing.Size(151, 22);
            this.cboDefect.TabIndex = 2;
            this.cboDefect.VisibleColumns = null;
            this.cboDefect.VisibleColumnsWidth = null;
            this.cboDefect.TextChanged += new System.EventHandler(this.cboDefect_TextChanged);
            // 
            // gridDefect
            // 
            this.gridDefect.Caption = "";
            this.gridDefect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDefect.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.gridDefect.IsUsePaging = false;
            this.gridDefect.LanguageKey = "DEFECTCLASSIFICATION";
            this.gridDefect.Location = new System.Drawing.Point(0, 0);
            this.gridDefect.Margin = new System.Windows.Forms.Padding(0);
            this.gridDefect.Name = "gridDefect";
            this.gridDefect.ShowBorder = true;
            this.gridDefect.ShowStatusBar = false;
            this.gridDefect.Size = new System.Drawing.Size(647, 167);
            this.gridDefect.TabIndex = 1;
            this.gridDefect.UseAutoBestFitColumns = false;
            // 
            // cboProduct
            // 
            this.cboProduct.LabelText = null;
            this.cboProduct.LanguageKey = null;
            this.cboProduct.Location = new System.Drawing.Point(163, 3);
            this.cboProduct.Name = "cboProduct";
            this.cboProduct.PopupWidth = 0;
            this.cboProduct.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboProduct.Properties.Appearance.Options.UseFont = true;
            this.cboProduct.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboProduct.Properties.NullText = "";
            this.cboProduct.ShowHeader = true;
            this.cboProduct.Size = new System.Drawing.Size(151, 22);
            this.cboProduct.TabIndex = 3;
            this.cboProduct.VisibleColumns = null;
            this.cboProduct.VisibleColumnsWidth = null;
            this.cboProduct.TextChanged += new System.EventHandler(this.cboProduct_TextChanged);
            // 
            // gridProduct
            // 
            this.gridProduct.Caption = "";
            this.gridProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridProduct.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.gridProduct.IsUsePaging = false;
            this.gridProduct.LanguageKey = "DEFECTCLASSIFICATION";
            this.gridProduct.Location = new System.Drawing.Point(0, 0);
            this.gridProduct.Margin = new System.Windows.Forms.Padding(0);
            this.gridProduct.Name = "gridProduct";
            this.gridProduct.ShowBorder = true;
            this.gridProduct.ShowStatusBar = false;
            this.gridProduct.Size = new System.Drawing.Size(647, 32);
            this.gridProduct.TabIndex = 1;
            this.gridProduct.UseAutoBestFitColumns = false;
            // 
            // tabControlChart
            // 
            this.tabControlChart.Name = "tabControlChart";
            this.tabControlChart.Padding = new System.Windows.Forms.Padding(3);
            this.tabControlChart.PageVisible = false;
            this.tabControlChart.Size = new System.Drawing.Size(653, 483);
            this.tabControlChart.Text = "ControlChart";
            // 
            // tabProcess
            // 
            this.tabProcess.Name = "tabProcess";
            this.tabProcess.Padding = new System.Windows.Forms.Padding(3);
            this.tabProcess.PageVisible = false;
            this.tabProcess.Size = new System.Drawing.Size(653, 483);
            this.tabProcess.Text = "Process";
            // 
            // tabRowData
            // 
            this.tabRowData.Controls.Add(this.grdRowData);
            this.tabRowData.Name = "tabRowData";
            this.tabRowData.Padding = new System.Windows.Forms.Padding(3);
            this.tabRowData.Size = new System.Drawing.Size(653, 483);
            this.tabRowData.Text = "Row Data";
            // 
            // grdRowData
            // 
            this.grdRowData.Caption = "";
            this.grdRowData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRowData.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdRowData.IsUsePaging = false;
            this.grdRowData.LanguageKey = "SPCRAWDATA";
            this.grdRowData.Location = new System.Drawing.Point(3, 3);
            this.grdRowData.Margin = new System.Windows.Forms.Padding(0);
            this.grdRowData.Name = "grdRowData";
            this.grdRowData.ShowBorder = true;
            this.grdRowData.ShowStatusBar = false;
            this.grdRowData.Size = new System.Drawing.Size(647, 477);
            this.grdRowData.TabIndex = 0;
            this.grdRowData.UseAutoBestFitColumns = false;
            // 
            // tabOverRules
            // 
            this.tabOverRules.Controls.Add(this.grdOverRules);
            this.tabOverRules.Name = "tabOverRules";
            this.tabOverRules.Padding = new System.Windows.Forms.Padding(3);
            this.tabOverRules.Size = new System.Drawing.Size(653, 483);
            this.tabOverRules.Text = "Over Rules";
            // 
            // grdOverRules
            // 
            this.grdOverRules.Caption = "";
            this.grdOverRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOverRules.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdOverRules.IsUsePaging = false;
            this.grdOverRules.LanguageKey = "SPCOVERRAWDATA";
            this.grdOverRules.Location = new System.Drawing.Point(3, 3);
            this.grdOverRules.Margin = new System.Windows.Forms.Padding(0);
            this.grdOverRules.Name = "grdOverRules";
            this.grdOverRules.ShowBorder = true;
            this.grdOverRules.ShowStatusBar = false;
            this.grdOverRules.Size = new System.Drawing.Size(647, 477);
            this.grdOverRules.TabIndex = 0;
            this.grdOverRules.UseAutoBestFitColumns = false;
            // 
            // QualityTotalSpcStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Name = "QualityTotalSpcStatus";
            this.Text = "SmartConditionBaseForm";
            this.Load += new System.EventHandler(this.QualityAnalysisSpcStatus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tabTrend.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).EndInit();
            this.splMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpChart)).EndInit();
            this.grpChart.ResumeLayout(false);
            this.grpChart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splMainSub01)).EndInit();
            this.splMainSub01.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splMainSub0101)).EndInit();
            this.splMainSub0101.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboDefect.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProduct.Properties)).EndInit();
            this.tabRowData.ResumeLayout(false);
            this.tabOverRules.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage tabControlChart;
        private DevExpress.XtraTab.XtraTabPage tabProcess;
        private DevExpress.XtraTab.XtraTabPage tabRowData;
        private DevExpress.XtraTab.XtraTabPage tabOverRules;
        private Framework.SmartControls.SmartBandedGrid grdRowData;
        private Framework.SmartControls.SmartBandedGrid grdOverRules;
        private DevExpress.XtraTab.XtraTabPage tabTrend;
        private UserControl.ucXBar ucXBar;
        private Framework.SmartControls.SmartSpliterContainer splMain;
        private Framework.SmartControls.SmartSpliterContainer splMainSub01;
        private Framework.SmartControls.SmartBandedGrid gridTotal;
        private Framework.SmartControls.SmartSpliterContainer splMainSub0101;
        private Framework.SmartControls.SmartBandedGrid gridDefect;
        private Framework.SmartControls.SmartBandedGrid gridProduct;
        private Framework.SmartControls.SmartComboBox cboDefect;
        private Framework.SmartControls.SmartComboBox cboProduct;
        private Framework.SmartControls.SmartGroupBox grpChart;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdRawData;
        private Framework.SmartControls.SmartLabel lblReduction1;
        private Framework.SmartControls.SmartLabel lblExtension1;
    }
}