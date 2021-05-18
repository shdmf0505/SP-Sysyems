namespace Micube.SmartMES.QualityAnalysis
{
    partial class ucLotYieldRate
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.ucMainPanel = new Micube.Framework.SmartControls.SmartPanel();
            this.ucChartGridPanel = new Micube.Framework.SmartControls.SmartPanel();
            this.ucLOTMainSpliter = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.gbLotYieldRate = new Micube.Framework.SmartControls.SmartGroupBox();
            this.chartLotYieldRate = new Micube.Framework.SmartControls.SmartChart();
            this.gbDefectCodeRate = new Micube.Framework.SmartControls.SmartGroupBox();
            this.chartDefectCodeRate = new Micube.Framework.SmartControls.SmartChart();
            this.topInfoPanel = new Micube.Framework.SmartControls.SmartPanel();
            this.btnLotAnalysis = new Micube.Framework.SmartControls.SmartButton();
            this.periodSmartLabelTextBox = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.itemSmartLabelTextBox = new Micube.Framework.SmartControls.SmartLabelTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ucMainPanel)).BeginInit();
            this.ucMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ucChartGridPanel)).BeginInit();
            this.ucChartGridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ucLOTMainSpliter)).BeginInit();
            this.ucLOTMainSpliter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbLotYieldRate)).BeginInit();
            this.gbLotYieldRate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartLotYieldRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbDefectCodeRate)).BeginInit();
            this.gbDefectCodeRate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDefectCodeRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topInfoPanel)).BeginInit();
            this.topInfoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.periodSmartLabelTextBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemSmartLabelTextBox.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ucMainPanel
            // 
            this.ucMainPanel.Controls.Add(this.ucChartGridPanel);
            this.ucMainPanel.Controls.Add(this.topInfoPanel);
            this.ucMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMainPanel.Location = new System.Drawing.Point(0, 0);
            this.ucMainPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ucMainPanel.Name = "ucMainPanel";
            this.ucMainPanel.Size = new System.Drawing.Size(848, 528);
            this.ucMainPanel.TabIndex = 0;
            // 
            // ucChartGridPanel
            // 
            this.ucChartGridPanel.Controls.Add(this.ucLOTMainSpliter);
            this.ucChartGridPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucChartGridPanel.Location = new System.Drawing.Point(2, 42);
            this.ucChartGridPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ucChartGridPanel.Name = "ucChartGridPanel";
            this.ucChartGridPanel.Size = new System.Drawing.Size(844, 484);
            this.ucChartGridPanel.TabIndex = 0;
            // 
            // ucLOTMainSpliter
            // 
            this.ucLOTMainSpliter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucLOTMainSpliter.Horizontal = false;
            this.ucLOTMainSpliter.Location = new System.Drawing.Point(2, 2);
            this.ucLOTMainSpliter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.ucLOTMainSpliter.Name = "ucLOTMainSpliter";
            this.ucLOTMainSpliter.Panel1.Controls.Add(this.gbLotYieldRate);
            this.ucLOTMainSpliter.Panel1.Text = "Panel1";
            this.ucLOTMainSpliter.Panel2.Controls.Add(this.gbDefectCodeRate);
            this.ucLOTMainSpliter.Panel2.Text = "Panel2";
            this.ucLOTMainSpliter.Size = new System.Drawing.Size(840, 480);
            this.ucLOTMainSpliter.SplitterPosition = 250;
            this.ucLOTMainSpliter.TabIndex = 0;
            // 
            // gbLotYieldRate
            // 
            this.gbLotYieldRate.Controls.Add(this.chartLotYieldRate);
            this.gbLotYieldRate.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.gbLotYieldRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbLotYieldRate.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.gbLotYieldRate.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.gbLotYieldRate.LanguageKey = "LOTYIELD";
            this.gbLotYieldRate.Location = new System.Drawing.Point(0, 0);
            this.gbLotYieldRate.Name = "gbLotYieldRate";
            this.gbLotYieldRate.ShowBorder = true;
            this.gbLotYieldRate.Size = new System.Drawing.Size(840, 250);
            this.gbLotYieldRate.TabIndex = 2;
            // 
            // chartLotYieldRate
            // 
            this.chartLotYieldRate.AutoLayout = false;
            this.chartLotYieldRate.CacheToMemory = true;
            this.chartLotYieldRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartLotYieldRate.Legend.Name = "Default Legend";
            this.chartLotYieldRate.Location = new System.Drawing.Point(2, 31);
            this.chartLotYieldRate.Name = "chartLotYieldRate";
            this.chartLotYieldRate.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartLotYieldRate.Size = new System.Drawing.Size(836, 217);
            this.chartLotYieldRate.TabIndex = 2;
            // 
            // gbDefectCodeRate
            // 
            this.gbDefectCodeRate.Controls.Add(this.chartDefectCodeRate);
            this.gbDefectCodeRate.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.gbDefectCodeRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDefectCodeRate.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.gbDefectCodeRate.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.gbDefectCodeRate.Location = new System.Drawing.Point(0, 0);
            this.gbDefectCodeRate.Name = "gbDefectCodeRate";
            this.gbDefectCodeRate.ShowBorder = true;
            this.gbDefectCodeRate.Size = new System.Drawing.Size(840, 225);
            this.gbDefectCodeRate.TabIndex = 1;
            // 
            // chartDefectCodeRate
            // 
            this.chartDefectCodeRate.AutoLayout = false;
            this.chartDefectCodeRate.CacheToMemory = true;
            this.chartDefectCodeRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartDefectCodeRate.Legend.Name = "Default Legend";
            this.chartDefectCodeRate.Location = new System.Drawing.Point(2, 31);
            this.chartDefectCodeRate.Name = "chartDefectCodeRate";
            this.chartDefectCodeRate.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartDefectCodeRate.Size = new System.Drawing.Size(836, 192);
            this.chartDefectCodeRate.TabIndex = 0;
            // 
            // topInfoPanel
            // 
            this.topInfoPanel.Controls.Add(this.btnLotAnalysis);
            this.topInfoPanel.Controls.Add(this.periodSmartLabelTextBox);
            this.topInfoPanel.Controls.Add(this.itemSmartLabelTextBox);
            this.topInfoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topInfoPanel.Location = new System.Drawing.Point(2, 2);
            this.topInfoPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.topInfoPanel.Name = "topInfoPanel";
            this.topInfoPanel.Size = new System.Drawing.Size(844, 40);
            this.topInfoPanel.TabIndex = 0;
            // 
            // btnLotAnalysis
            // 
            this.btnLotAnalysis.AllowFocus = false;
            this.btnLotAnalysis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLotAnalysis.IsBusy = false;
            this.btnLotAnalysis.IsWrite = false;
            this.btnLotAnalysis.LanguageKey = "LOTANALYSIS";
            this.btnLotAnalysis.Location = new System.Drawing.Point(714, 7);
            this.btnLotAnalysis.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnLotAnalysis.Name = "btnLotAnalysis";
            this.btnLotAnalysis.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnLotAnalysis.Size = new System.Drawing.Size(125, 25);
            this.btnLotAnalysis.TabIndex = 8;
            this.btnLotAnalysis.Text = "LOT분석";
            this.btnLotAnalysis.TooltipLanguageKey = "";
            // 
            // periodSmartLabelTextBox
            // 
            this.periodSmartLabelTextBox.EditorWidth = "200%";
            this.periodSmartLabelTextBox.LabelText = "기간";
            this.periodSmartLabelTextBox.LanguageKey = "PERIOD";
            this.periodSmartLabelTextBox.Location = new System.Drawing.Point(350, 12);
            this.periodSmartLabelTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.periodSmartLabelTextBox.Name = "periodSmartLabelTextBox";
            this.periodSmartLabelTextBox.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.periodSmartLabelTextBox.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.periodSmartLabelTextBox.Properties.ReadOnly = true;
            this.periodSmartLabelTextBox.Size = new System.Drawing.Size(292, 20);
            this.periodSmartLabelTextBox.TabIndex = 7;
            // 
            // itemSmartLabelTextBox
            // 
            this.itemSmartLabelTextBox.EditorWidth = "200%";
            this.itemSmartLabelTextBox.LabelText = "품목명";
            this.itemSmartLabelTextBox.LanguageKey = "PRODUCTDEFNAME";
            this.itemSmartLabelTextBox.Location = new System.Drawing.Point(26, 12);
            this.itemSmartLabelTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.itemSmartLabelTextBox.Name = "itemSmartLabelTextBox";
            this.itemSmartLabelTextBox.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.itemSmartLabelTextBox.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.itemSmartLabelTextBox.Properties.ReadOnly = true;
            this.itemSmartLabelTextBox.Size = new System.Drawing.Size(292, 20);
            this.itemSmartLabelTextBox.TabIndex = 6;
            // 
            // ucLotYieldRate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucMainPanel);
            this.Name = "ucLotYieldRate";
            this.Size = new System.Drawing.Size(848, 528);
            this.Load += new System.EventHandler(this.ucLotYieldRate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ucMainPanel)).EndInit();
            this.ucMainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ucChartGridPanel)).EndInit();
            this.ucChartGridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ucLOTMainSpliter)).EndInit();
            this.ucLOTMainSpliter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbLotYieldRate)).EndInit();
            this.gbLotYieldRate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartLotYieldRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbDefectCodeRate)).EndInit();
            this.gbDefectCodeRate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartDefectCodeRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topInfoPanel)).EndInit();
            this.topInfoPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.periodSmartLabelTextBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemSmartLabelTextBox.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartPanel ucMainPanel;
        private Framework.SmartControls.SmartPanel ucChartGridPanel;
        private Framework.SmartControls.SmartPanel topInfoPanel;
        private Framework.SmartControls.SmartSpliterContainer ucLOTMainSpliter;
        private Framework.SmartControls.SmartLabelTextBox periodSmartLabelTextBox;
        private Framework.SmartControls.SmartLabelTextBox itemSmartLabelTextBox;
        private Framework.SmartControls.SmartChart chartDefectCodeRate;
        private Framework.SmartControls.SmartButton btnLotAnalysis;
        private Framework.SmartControls.SmartGroupBox gbLotYieldRate;
        private Framework.SmartControls.SmartChart chartLotYieldRate;
        private Framework.SmartControls.SmartGroupBox gbDefectCodeRate;
    }
}
