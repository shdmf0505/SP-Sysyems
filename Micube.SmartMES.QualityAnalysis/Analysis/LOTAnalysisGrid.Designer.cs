namespace Micube.SmartMES.QualityAnalysis
{
    partial class LOTAnalysisGrid
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
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.btnDefectAnalysis = new Micube.Framework.SmartControls.SmartButton();
            this.btnLotSelect = new Micube.Framework.SmartControls.SmartButton();
            this.cbLOT = new Micube.Framework.SmartControls.SmartComboBox();
            this.tbLotCount = new Micube.Framework.SmartControls.SmartTextBox();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            this.periodSmartLabelTextBox = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.itemSmartLabelTextBox = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.pnlChartGrid = new Micube.Framework.SmartControls.SmartPanel();
            this.scMainGrid = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.gbPivotGrid = new Micube.Framework.SmartControls.SmartGroupBox();
            this.tb1Match = new System.Windows.Forms.TextBox();
            this.tb20Match = new System.Windows.Forms.TextBox();
            this.tb50Match = new System.Windows.Forms.TextBox();
            this.tb70Match = new System.Windows.Forms.TextBox();
            this.tb100Match = new System.Windows.Forms.TextBox();
            this.pvGridWorkTime = new Micube.Framework.SmartControls.SmartPivotGridControl();
            this.scGridResConsumable = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.gbConsumableGrid = new Micube.Framework.SmartControls.SmartGroupBox();
            this.grdProcessConsumable = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.gbDurableGrid = new Micube.Framework.SmartControls.SmartGroupBox();
            this.grdProcessDurable = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.pnlGrids = new Micube.Framework.SmartControls.SmartPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbLOT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLotCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodSmartLabelTextBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemSmartLabelTextBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlChartGrid)).BeginInit();
            this.pnlChartGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMainGrid)).BeginInit();
            this.scMainGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbPivotGrid)).BeginInit();
            this.gbPivotGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pvGridWorkTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scGridResConsumable)).BeginInit();
            this.scGridResConsumable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbConsumableGrid)).BeginInit();
            this.gbConsumableGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbDurableGrid)).BeginInit();
            this.gbDurableGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGrids)).BeginInit();
            this.pnlGrids.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 680);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(1238, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.pnlGrids);
            this.pnlContent.Size = new System.Drawing.Size(1238, 684);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1543, 713);
            // 
            // smartPanel1
            // 
            this.smartPanel1.Controls.Add(this.btnDefectAnalysis);
            this.smartPanel1.Controls.Add(this.btnLotSelect);
            this.smartPanel1.Controls.Add(this.cbLOT);
            this.smartPanel1.Controls.Add(this.tbLotCount);
            this.smartPanel1.Controls.Add(this.smartLabel1);
            this.smartPanel1.Controls.Add(this.periodSmartLabelTextBox);
            this.smartPanel1.Controls.Add(this.itemSmartLabelTextBox);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartPanel1.Location = new System.Drawing.Point(2, 2);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(1234, 48);
            this.smartPanel1.TabIndex = 1;
            // 
            // btnDefectAnalysis
            // 
            this.btnDefectAnalysis.AllowFocus = false;
            this.btnDefectAnalysis.IsBusy = false;
            this.btnDefectAnalysis.IsWrite = false;
            this.btnDefectAnalysis.LanguageKey = "DEFECTANALYSIS";
            this.btnDefectAnalysis.Location = new System.Drawing.Point(1062, 10);
            this.btnDefectAnalysis.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnDefectAnalysis.Name = "btnDefectAnalysis";
            this.btnDefectAnalysis.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnDefectAnalysis.Size = new System.Drawing.Size(146, 25);
            this.btnDefectAnalysis.TabIndex = 14;
            this.btnDefectAnalysis.Text = "불량 분석(공정 시계열)";
            this.btnDefectAnalysis.TooltipLanguageKey = "";
            // 
            // btnLotSelect
            // 
            this.btnLotSelect.AllowFocus = false;
            this.btnLotSelect.IsBusy = false;
            this.btnLotSelect.IsWrite = false;
            this.btnLotSelect.LanguageKey = "LOTNOSEARCH";
            this.btnLotSelect.Location = new System.Drawing.Point(955, 10);
            this.btnLotSelect.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnLotSelect.Name = "btnLotSelect";
            this.btnLotSelect.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnLotSelect.Size = new System.Drawing.Size(101, 25);
            this.btnLotSelect.TabIndex = 13;
            this.btnLotSelect.Text = "LOT선택";
            this.btnLotSelect.TooltipLanguageKey = "";
            this.btnLotSelect.Click += new System.EventHandler(this.btnLotSelect_Click);
            // 
            // cbLOT
            // 
            this.cbLOT.AllowDrop = true;
            this.cbLOT.DisplayMember = "CODENAME";
            this.cbLOT.LabelText = null;
            this.cbLOT.LanguageKey = null;
            this.cbLOT.Location = new System.Drawing.Point(758, 13);
            this.cbLOT.Name = "cbLOT";
            this.cbLOT.PopupWidth = 0;
            this.cbLOT.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbLOT.Properties.DisplayMember = "CODENAME";
            this.cbLOT.Properties.NullText = "";
            this.cbLOT.Properties.ValueMember = "CODEID";
            this.cbLOT.ShowHeader = true;
            this.cbLOT.Size = new System.Drawing.Size(183, 20);
            this.cbLOT.TabIndex = 12;
            this.cbLOT.UseEmptyItem = true;
            this.cbLOT.ValueMember = "CODEID";
            this.cbLOT.VisibleColumns = null;
            this.cbLOT.VisibleColumnsWidth = null;
            // 
            // tbLotCount
            // 
            this.tbLotCount.Enabled = false;
            this.tbLotCount.LabelText = null;
            this.tbLotCount.LanguageKey = null;
            this.tbLotCount.Location = new System.Drawing.Point(689, 12);
            this.tbLotCount.Name = "tbLotCount";
            this.tbLotCount.Properties.Appearance.BackColor = System.Drawing.Color.Silver;
            this.tbLotCount.Properties.Appearance.Options.UseBackColor = true;
            this.tbLotCount.Size = new System.Drawing.Size(52, 20);
            this.tbLotCount.TabIndex = 11;
            // 
            // smartLabel1
            // 
            this.smartLabel1.Location = new System.Drawing.Point(649, 14);
            this.smartLabel1.Name = "smartLabel1";
            this.smartLabel1.Size = new System.Drawing.Size(31, 14);
            this.smartLabel1.TabIndex = 10;
            this.smartLabel1.Text = "LOT :";
            // 
            // periodSmartLabelTextBox
            // 
            this.periodSmartLabelTextBox.EditorWidth = "200%";
            this.periodSmartLabelTextBox.LabelText = "기간";
            this.periodSmartLabelTextBox.LanguageKey = "PERIOD";
            this.periodSmartLabelTextBox.Location = new System.Drawing.Point(329, 13);
            this.periodSmartLabelTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.periodSmartLabelTextBox.Name = "periodSmartLabelTextBox";
            this.periodSmartLabelTextBox.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.periodSmartLabelTextBox.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.periodSmartLabelTextBox.Properties.ReadOnly = true;
            this.periodSmartLabelTextBox.Size = new System.Drawing.Size(292, 20);
            this.periodSmartLabelTextBox.TabIndex = 9;
            // 
            // itemSmartLabelTextBox
            // 
            this.itemSmartLabelTextBox.EditorWidth = "200%";
            this.itemSmartLabelTextBox.LabelText = "품목명";
            this.itemSmartLabelTextBox.LanguageKey = "PRODUCTDEFNAME";
            this.itemSmartLabelTextBox.Location = new System.Drawing.Point(7, 13);
            this.itemSmartLabelTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.itemSmartLabelTextBox.Name = "itemSmartLabelTextBox";
            this.itemSmartLabelTextBox.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.itemSmartLabelTextBox.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.itemSmartLabelTextBox.Properties.ReadOnly = true;
            this.itemSmartLabelTextBox.Size = new System.Drawing.Size(292, 20);
            this.itemSmartLabelTextBox.TabIndex = 8;
            // 
            // pnlChartGrid
            // 
            this.pnlChartGrid.Controls.Add(this.scMainGrid);
            this.pnlChartGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChartGrid.Location = new System.Drawing.Point(2, 50);
            this.pnlChartGrid.Name = "pnlChartGrid";
            this.pnlChartGrid.Size = new System.Drawing.Size(1234, 632);
            this.pnlChartGrid.TabIndex = 14;
            // 
            // scMainGrid
            // 
            this.scMainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMainGrid.Horizontal = false;
            this.scMainGrid.Location = new System.Drawing.Point(2, 2);
            this.scMainGrid.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.scMainGrid.Name = "scMainGrid";
            this.scMainGrid.Panel1.Controls.Add(this.gbPivotGrid);
            this.scMainGrid.Panel1.Text = "Panel1";
            this.scMainGrid.Panel2.Controls.Add(this.scGridResConsumable);
            this.scMainGrid.Panel2.Text = "Panel2";
            this.scMainGrid.Size = new System.Drawing.Size(1230, 628);
            this.scMainGrid.SplitterPosition = 399;
            this.scMainGrid.TabIndex = 0;
            this.scMainGrid.Text = "smartSpliterContainer1";
            // 
            // gbPivotGrid
            // 
            this.gbPivotGrid.Controls.Add(this.tb1Match);
            this.gbPivotGrid.Controls.Add(this.tb20Match);
            this.gbPivotGrid.Controls.Add(this.tb50Match);
            this.gbPivotGrid.Controls.Add(this.tb70Match);
            this.gbPivotGrid.Controls.Add(this.tb100Match);
            this.gbPivotGrid.Controls.Add(this.pvGridWorkTime);
            this.gbPivotGrid.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.gbPivotGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPivotGrid.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.gbPivotGrid.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.gbPivotGrid.Location = new System.Drawing.Point(0, 0);
            this.gbPivotGrid.Name = "gbPivotGrid";
            this.gbPivotGrid.ShowBorder = true;
            this.gbPivotGrid.Size = new System.Drawing.Size(1230, 399);
            this.gbPivotGrid.TabIndex = 1;
            // 
            // tb1Match
            // 
            this.tb1Match.Location = new System.Drawing.Point(329, 4);
            this.tb1Match.Name = "tb1Match";
            this.tb1Match.Size = new System.Drawing.Size(61, 22);
            this.tb1Match.TabIndex = 5;
            this.tb1Match.Text = "> 0% ";
            // 
            // tb20Match
            // 
            this.tb20Match.Location = new System.Drawing.Point(248, 4);
            this.tb20Match.Name = "tb20Match";
            this.tb20Match.Size = new System.Drawing.Size(61, 22);
            this.tb20Match.TabIndex = 4;
            this.tb20Match.Text = "~20% ";
            // 
            // tb50Match
            // 
            this.tb50Match.Location = new System.Drawing.Point(167, 4);
            this.tb50Match.Name = "tb50Match";
            this.tb50Match.Size = new System.Drawing.Size(61, 22);
            this.tb50Match.TabIndex = 3;
            this.tb50Match.Text = "~50% ";
            // 
            // tb70Match
            // 
            this.tb70Match.Location = new System.Drawing.Point(86, 4);
            this.tb70Match.Name = "tb70Match";
            this.tb70Match.Size = new System.Drawing.Size(61, 22);
            this.tb70Match.TabIndex = 2;
            this.tb70Match.Text = "~70% ";
            // 
            // tb100Match
            // 
            this.tb100Match.Location = new System.Drawing.Point(5, 4);
            this.tb100Match.Name = "tb100Match";
            this.tb100Match.Size = new System.Drawing.Size(61, 22);
            this.tb100Match.TabIndex = 1;
            this.tb100Match.Text = "100%";
            // 
            // pvGridWorkTime
            // 
            this.pvGridWorkTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pvGridWorkTime.GrandTotalCaptionText = null;
            this.pvGridWorkTime.Location = new System.Drawing.Point(2, 31);
            this.pvGridWorkTime.Name = "pvGridWorkTime";
            this.pvGridWorkTime.OptionsView.ShowGrandTotalsForSingleValues = true;
            this.pvGridWorkTime.OptionsView.ShowTotalsForSingleValues = true;
            this.pvGridWorkTime.Size = new System.Drawing.Size(1226, 366);
            this.pvGridWorkTime.TabIndex = 0;
            this.pvGridWorkTime.TotalFieldNames = null;
            this.pvGridWorkTime.UseCheckBoxField = false;
            this.pvGridWorkTime.UseGrandTotalCaption = false;
            // 
            // scGridResConsumable
            // 
            this.scGridResConsumable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scGridResConsumable.Horizontal = false;
            this.scGridResConsumable.Location = new System.Drawing.Point(0, 0);
            this.scGridResConsumable.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.scGridResConsumable.Name = "scGridResConsumable";
            this.scGridResConsumable.Panel1.Controls.Add(this.gbConsumableGrid);
            this.scGridResConsumable.Panel1.Text = "Panel1";
            this.scGridResConsumable.Panel2.Controls.Add(this.gbDurableGrid);
            this.scGridResConsumable.Panel2.Text = "Panel2";
            this.scGridResConsumable.Size = new System.Drawing.Size(1230, 224);
            this.scGridResConsumable.SplitterPosition = 150;
            this.scGridResConsumable.TabIndex = 0;
            this.scGridResConsumable.Text = "smartSpliterContainer1";
            // 
            // gbConsumableGrid
            // 
            this.gbConsumableGrid.Controls.Add(this.grdProcessConsumable);
            this.gbConsumableGrid.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.gbConsumableGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbConsumableGrid.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.gbConsumableGrid.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.gbConsumableGrid.LanguageKey = "MATERIALINFO";
            this.gbConsumableGrid.Location = new System.Drawing.Point(0, 0);
            this.gbConsumableGrid.Name = "gbConsumableGrid";
            this.gbConsumableGrid.ShowBorder = true;
            this.gbConsumableGrid.Size = new System.Drawing.Size(1230, 150);
            this.gbConsumableGrid.TabIndex = 2;
            // 
            // grdProcessConsumable
            // 
            this.grdProcessConsumable.Caption = "";
            this.grdProcessConsumable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProcessConsumable.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdProcessConsumable.IsUsePaging = false;
            this.grdProcessConsumable.LanguageKey = "MATERIALINFO";
            this.grdProcessConsumable.Location = new System.Drawing.Point(2, 31);
            this.grdProcessConsumable.Margin = new System.Windows.Forms.Padding(0);
            this.grdProcessConsumable.Name = "grdProcessConsumable";
            this.grdProcessConsumable.ShowBorder = true;
            this.grdProcessConsumable.ShowButtonBar = false;
            this.grdProcessConsumable.Size = new System.Drawing.Size(1226, 117);
            this.grdProcessConsumable.TabIndex = 1;
            this.grdProcessConsumable.UseAutoBestFitColumns = false;
            // 
            // gbDurableGrid
            // 
            this.gbDurableGrid.Controls.Add(this.grdProcessDurable);
            this.gbDurableGrid.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.gbDurableGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDurableGrid.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.gbDurableGrid.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.gbDurableGrid.LanguageKey = "DURABLEINFO";
            this.gbDurableGrid.Location = new System.Drawing.Point(0, 0);
            this.gbDurableGrid.Name = "gbDurableGrid";
            this.gbDurableGrid.ShowBorder = true;
            this.gbDurableGrid.Size = new System.Drawing.Size(1230, 69);
            this.gbDurableGrid.TabIndex = 3;
            // 
            // grdProcessDurable
            // 
            this.grdProcessDurable.Caption = "";
            this.grdProcessDurable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProcessDurable.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdProcessDurable.IsUsePaging = false;
            this.grdProcessDurable.LanguageKey = "DURABLEINFO";
            this.grdProcessDurable.Location = new System.Drawing.Point(2, 31);
            this.grdProcessDurable.Margin = new System.Windows.Forms.Padding(0);
            this.grdProcessDurable.Name = "grdProcessDurable";
            this.grdProcessDurable.ShowBorder = true;
            this.grdProcessDurable.ShowButtonBar = false;
            this.grdProcessDurable.Size = new System.Drawing.Size(1226, 36);
            this.grdProcessDurable.TabIndex = 2;
            this.grdProcessDurable.UseAutoBestFitColumns = false;
            // 
            // pnlGrids
            // 
            this.pnlGrids.Controls.Add(this.pnlChartGrid);
            this.pnlGrids.Controls.Add(this.smartPanel1);
            this.pnlGrids.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrids.Location = new System.Drawing.Point(0, 0);
            this.pnlGrids.Name = "pnlGrids";
            this.pnlGrids.Size = new System.Drawing.Size(1238, 684);
            this.pnlGrids.TabIndex = 1;
            // 
            // LOTAnalysisGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1563, 733);
            this.Name = "LOTAnalysisGrid";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.smartPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbLOT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLotCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodSmartLabelTextBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemSmartLabelTextBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlChartGrid)).EndInit();
            this.pnlChartGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMainGrid)).EndInit();
            this.scMainGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbPivotGrid)).EndInit();
            this.gbPivotGrid.ResumeLayout(false);
            this.gbPivotGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pvGridWorkTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scGridResConsumable)).EndInit();
            this.scGridResConsumable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbConsumableGrid)).EndInit();
            this.gbConsumableGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbDurableGrid)).EndInit();
            this.gbDurableGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlGrids)).EndInit();
            this.pnlGrids.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartPanel pnlGrids;
        private Framework.SmartControls.SmartPanel pnlChartGrid;
        private Framework.SmartControls.SmartSpliterContainer scMainGrid;
        private Framework.SmartControls.SmartPivotGridControl pvGridWorkTime;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartButton btnLotSelect;
        private Framework.SmartControls.SmartComboBox cbLOT;
        private Framework.SmartControls.SmartTextBox tbLotCount;
        private Framework.SmartControls.SmartLabel smartLabel1;
        private Framework.SmartControls.SmartLabelTextBox periodSmartLabelTextBox;
        private Framework.SmartControls.SmartLabelTextBox itemSmartLabelTextBox;
        private Framework.SmartControls.SmartSpliterContainer scGridResConsumable;
        private Framework.SmartControls.SmartGroupBox gbPivotGrid;
        private Framework.SmartControls.SmartBandedGrid grdProcessConsumable;
        private Framework.SmartControls.SmartBandedGrid grdProcessDurable;
        private Framework.SmartControls.SmartGroupBox gbConsumableGrid;
        private Framework.SmartControls.SmartGroupBox gbDurableGrid;
        private System.Windows.Forms.TextBox tb100Match;
        private System.Windows.Forms.TextBox tb70Match;
        private System.Windows.Forms.TextBox tb50Match;
        private System.Windows.Forms.TextBox tb20Match;
        private System.Windows.Forms.TextBox tb1Match;
        private Framework.SmartControls.SmartButton btnDefectAnalysis;
    }
}