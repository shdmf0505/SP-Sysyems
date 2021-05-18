namespace Micube.SmartMES.QualityAnalysis
{
    partial class LOTAnalysisPivot
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
            this.components = new System.ComponentModel.Container();
            this.pnlGrids = new Micube.Framework.SmartControls.SmartPanel();
            this.pnlChartGrid = new Micube.Framework.SmartControls.SmartPanel();
            this.scChartGrid = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.chartResourceTime = new Micube.Framework.SmartControls.SmartChart();
            this.scGridGroup = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.gridConsumable = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.gridDurable = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.btnLotSelect = new Micube.Framework.SmartControls.SmartButton();
            this.cbLOT = new Micube.Framework.SmartControls.SmartComboBox();
            this.tbLotCount = new Micube.Framework.SmartControls.SmartTextBox();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            this.periodSmartLabelTextBox = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.itemSmartLabelTextBox = new Micube.Framework.SmartControls.SmartLabelTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGrids)).BeginInit();
            this.pnlGrids.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlChartGrid)).BeginInit();
            this.pnlChartGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scChartGrid)).BeginInit();
            this.scChartGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartResourceTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scGridGroup)).BeginInit();
            this.scGridGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbLOT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLotCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodSmartLabelTextBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemSmartLabelTextBox.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 680);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(1067, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.pnlGrids);
            this.pnlContent.Size = new System.Drawing.Size(1067, 684);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1372, 713);
            // 
            // pnlGrids
            // 
            this.pnlGrids.Controls.Add(this.pnlChartGrid);
            this.pnlGrids.Controls.Add(this.smartPanel1);
            this.pnlGrids.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrids.Location = new System.Drawing.Point(0, 0);
            this.pnlGrids.Name = "pnlGrids";
            this.pnlGrids.Size = new System.Drawing.Size(1067, 684);
            this.pnlGrids.TabIndex = 0;
            // 
            // pnlChartGrid
            // 
            this.pnlChartGrid.Controls.Add(this.scChartGrid);
            this.pnlChartGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChartGrid.Location = new System.Drawing.Point(2, 50);
            this.pnlChartGrid.Name = "pnlChartGrid";
            this.pnlChartGrid.Size = new System.Drawing.Size(1063, 632);
            this.pnlChartGrid.TabIndex = 14;
            // 
            // scChartGrid
            // 
            this.scChartGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scChartGrid.Location = new System.Drawing.Point(2, 2);
            this.scChartGrid.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.scChartGrid.Name = "scChartGrid";
            this.scChartGrid.Panel1.Controls.Add(this.chartResourceTime);
            this.scChartGrid.Panel1.Text = "Panel1";
            this.scChartGrid.Panel2.Controls.Add(this.scGridGroup);
            this.scChartGrid.Panel2.Text = "Panel2";
            this.scChartGrid.Size = new System.Drawing.Size(1059, 628);
            this.scChartGrid.SplitterPosition = 450;
            this.scChartGrid.TabIndex = 0;
            this.scChartGrid.Text = "smartSpliterContainer1";
            // 
            // chartResourceTime
            // 
            this.chartResourceTime.AutoLayout = false;
            this.chartResourceTime.CacheToMemory = true;
            this.chartResourceTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartResourceTime.Legend.Name = "Default Legend";
            this.chartResourceTime.Location = new System.Drawing.Point(0, 0);
            this.chartResourceTime.Name = "chartResourceTime";
            this.chartResourceTime.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartResourceTime.Size = new System.Drawing.Size(450, 628);
            this.chartResourceTime.TabIndex = 0;
            // 
            // scGridGroup
            // 
            this.scGridGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scGridGroup.Horizontal = false;
            this.scGridGroup.Location = new System.Drawing.Point(0, 0);
            this.scGridGroup.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.scGridGroup.Name = "scGridGroup";
            this.scGridGroup.Panel1.Controls.Add(this.gridConsumable);
            this.scGridGroup.Panel1.Text = "Panel1";
            this.scGridGroup.Panel2.Controls.Add(this.gridDurable);
            this.scGridGroup.Panel2.Text = "Panel2";
            this.scGridGroup.Size = new System.Drawing.Size(604, 628);
            this.scGridGroup.SplitterPosition = 396;
            this.scGridGroup.TabIndex = 0;
            this.scGridGroup.Text = "smartSpliterContainer1";
            // 
            // gridBOMConsumable
            // 
            this.gridConsumable.Caption = "";
            this.gridConsumable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridConsumable.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.Export;
            this.gridConsumable.IsUsePaging = false;
            this.gridConsumable.LanguageKey = null;
            this.gridConsumable.Location = new System.Drawing.Point(0, 0);
            this.gridConsumable.Margin = new System.Windows.Forms.Padding(0);
            this.gridConsumable.Name = "gridBOMConsumable";
            this.gridConsumable.ShowBorder = true;
            this.gridConsumable.Size = new System.Drawing.Size(604, 396);
            this.gridConsumable.TabIndex = 0;
            // 
            // gridDurable
            // 
            this.gridDurable.Caption = "";
            this.gridDurable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDurable.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.Export;
            this.gridDurable.IsUsePaging = false;
            this.gridDurable.LanguageKey = null;
            this.gridDurable.Location = new System.Drawing.Point(0, 0);
            this.gridDurable.Margin = new System.Windows.Forms.Padding(0);
            this.gridDurable.Name = "gridDurable";
            this.gridDurable.ShowBorder = true;
            this.gridDurable.Size = new System.Drawing.Size(604, 227);
            this.gridDurable.TabIndex = 1;
            // 
            // smartPanel1
            // 
            this.smartPanel1.Controls.Add(this.btnLotSelect);
            this.smartPanel1.Controls.Add(this.cbLOT);
            this.smartPanel1.Controls.Add(this.tbLotCount);
            this.smartPanel1.Controls.Add(this.smartLabel1);
            this.smartPanel1.Controls.Add(this.periodSmartLabelTextBox);
            this.smartPanel1.Controls.Add(this.itemSmartLabelTextBox);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartPanel1.Location = new System.Drawing.Point(2, 2);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(1063, 48);
            this.smartPanel1.TabIndex = 1;
            // 
            // btnLotSelect
            // 
            this.btnLotSelect.AllowFocus = false;
            this.btnLotSelect.IsBusy = false;
            this.btnLotSelect.IsWrite = false;
            this.btnLotSelect.LanguageKey = "LOTNOSEARCH";
            this.btnLotSelect.Location = new System.Drawing.Point(931, 10);
            this.btnLotSelect.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnLotSelect.Name = "btnLotSelect";
            this.btnLotSelect.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnLotSelect.Size = new System.Drawing.Size(101, 25);
            this.btnLotSelect.TabIndex = 13;
            this.btnLotSelect.Text = "LOT선택";
            this.btnLotSelect.TooltipLanguageKey = "";
            // 
            // cbLOT
            // 
            this.cbLOT.LabelText = null;
            this.cbLOT.LanguageKey = null;
            this.cbLOT.Location = new System.Drawing.Point(734, 13);
            this.cbLOT.Name = "cbLOT";
            this.cbLOT.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbLOT.Properties.NullText = "";
            this.cbLOT.ShowHeader = true;
            this.cbLOT.Size = new System.Drawing.Size(183, 20);
            this.cbLOT.TabIndex = 12;
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
            this.tbLotCount.Size = new System.Drawing.Size(25, 20);
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
            // LOTAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1392, 733);
            this.Name = "LOTAnalysis";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGrids)).EndInit();
            this.pnlGrids.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlChartGrid)).EndInit();
            this.pnlChartGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scChartGrid)).EndInit();
            this.scChartGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartResourceTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scGridGroup)).EndInit();
            this.scGridGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.smartPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbLOT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLotCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodSmartLabelTextBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemSmartLabelTextBox.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartPanel pnlGrids;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartLabelTextBox periodSmartLabelTextBox;
        private Framework.SmartControls.SmartLabelTextBox itemSmartLabelTextBox;
        private Framework.SmartControls.SmartTextBox tbLotCount;
        private Framework.SmartControls.SmartLabel smartLabel1;
        private Framework.SmartControls.SmartButton btnLotSelect;
        private Framework.SmartControls.SmartComboBox cbLOT;
        private Framework.SmartControls.SmartPanel pnlChartGrid;
        private Framework.SmartControls.SmartSpliterContainer scChartGrid;
        private Framework.SmartControls.SmartChart chartResourceTime;
        private Framework.SmartControls.SmartSpliterContainer scGridGroup;
        private Framework.SmartControls.SmartBandedGrid gridConsumable;
        private Framework.SmartControls.SmartBandedGrid gridDurable;
    }
}