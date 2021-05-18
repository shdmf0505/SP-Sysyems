namespace Micube.SmartMES.QualityAnalysis
{
    partial class ucDayYieldRate
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
            this.ucDayMainSpliter = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.ucDayUpperSpliter = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.ucDayUpper2Spliter = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.chartYiedRateWeek = new Micube.Framework.SmartControls.SmartChart();
            this.chartYiedRateDay = new Micube.Framework.SmartControls.SmartChart();
            this.grdDayYieldRate = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.topInfoPanel = new Micube.Framework.SmartControls.SmartPanel();
            this.periodSmartLabelTextBox = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.itemSmartLabelTextBox = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.gbMonthYield = new Micube.Framework.SmartControls.SmartGroupBox();
            this.chartYiedRateMonth = new Micube.Framework.SmartControls.SmartChart();
            this.gbWeekYield = new Micube.Framework.SmartControls.SmartGroupBox();
            this.gbDayYield = new Micube.Framework.SmartControls.SmartGroupBox();
            this.gbGridPeriodYield = new Micube.Framework.SmartControls.SmartGroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.ucMainPanel)).BeginInit();
            this.ucMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ucChartGridPanel)).BeginInit();
            this.ucChartGridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ucDayMainSpliter)).BeginInit();
            this.ucDayMainSpliter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ucDayUpperSpliter)).BeginInit();
            this.ucDayUpperSpliter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ucDayUpper2Spliter)).BeginInit();
            this.ucDayUpper2Spliter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartYiedRateWeek)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartYiedRateDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topInfoPanel)).BeginInit();
            this.topInfoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.periodSmartLabelTextBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemSmartLabelTextBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbMonthYield)).BeginInit();
            this.gbMonthYield.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartYiedRateMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbWeekYield)).BeginInit();
            this.gbWeekYield.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbDayYield)).BeginInit();
            this.gbDayYield.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbGridPeriodYield)).BeginInit();
            this.gbGridPeriodYield.SuspendLayout();
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
            this.ucChartGridPanel.Controls.Add(this.ucDayMainSpliter);
            this.ucChartGridPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucChartGridPanel.Location = new System.Drawing.Point(2, 42);
            this.ucChartGridPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ucChartGridPanel.Name = "ucChartGridPanel";
            this.ucChartGridPanel.Size = new System.Drawing.Size(844, 484);
            this.ucChartGridPanel.TabIndex = 0;
            // 
            // ucDayMainSpliter
            // 
            this.ucDayMainSpliter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDayMainSpliter.Horizontal = false;
            this.ucDayMainSpliter.Location = new System.Drawing.Point(2, 2);
            this.ucDayMainSpliter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.ucDayMainSpliter.Name = "ucDayMainSpliter";
            this.ucDayMainSpliter.Panel1.Controls.Add(this.ucDayUpperSpliter);
            this.ucDayMainSpliter.Panel1.Text = "Panel1";
            this.ucDayMainSpliter.Panel2.Controls.Add(this.gbGridPeriodYield);
            this.ucDayMainSpliter.Panel2.Text = "Panel2";
            this.ucDayMainSpliter.Size = new System.Drawing.Size(840, 480);
            this.ucDayMainSpliter.SplitterPosition = 250;
            this.ucDayMainSpliter.TabIndex = 0;
            // 
            // ucDayUpperSpliter
            // 
            this.ucDayUpperSpliter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDayUpperSpliter.Location = new System.Drawing.Point(0, 0);
            this.ucDayUpperSpliter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.ucDayUpperSpliter.Name = "ucDayUpperSpliter";
            this.ucDayUpperSpliter.Panel1.Controls.Add(this.gbMonthYield);
            this.ucDayUpperSpliter.Panel1.Text = "Panel1";
            this.ucDayUpperSpliter.Panel2.Controls.Add(this.ucDayUpper2Spliter);
            this.ucDayUpperSpliter.Panel2.Text = "Panel2";
            this.ucDayUpperSpliter.Size = new System.Drawing.Size(840, 250);
            this.ucDayUpperSpliter.SplitterPosition = 250;
            this.ucDayUpperSpliter.TabIndex = 0;
            this.ucDayUpperSpliter.Text = "smartSpliterContainer2";
            // 
            // ucDayUpper2Spliter
            // 
            this.ucDayUpper2Spliter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDayUpper2Spliter.Location = new System.Drawing.Point(0, 0);
            this.ucDayUpper2Spliter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.ucDayUpper2Spliter.Name = "ucDayUpper2Spliter";
            this.ucDayUpper2Spliter.Panel1.Controls.Add(this.gbWeekYield);
            this.ucDayUpper2Spliter.Panel1.Text = "Panel1";
            this.ucDayUpper2Spliter.Panel2.Controls.Add(this.gbDayYield);
            this.ucDayUpper2Spliter.Panel2.Text = "Panel2";
            this.ucDayUpper2Spliter.Size = new System.Drawing.Size(585, 250);
            this.ucDayUpper2Spliter.SplitterPosition = 300;
            this.ucDayUpper2Spliter.TabIndex = 0;
            this.ucDayUpper2Spliter.Text = "smartSpliterContainer3";
            // 
            // chartYiedRateWeek
            // 
            this.chartYiedRateWeek.AutoLayout = false;
            this.chartYiedRateWeek.CacheToMemory = true;
            this.chartYiedRateWeek.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartYiedRateWeek.Legend.Name = "Default Legend";
            this.chartYiedRateWeek.Location = new System.Drawing.Point(2, 31);
            this.chartYiedRateWeek.Name = "chartYiedRateWeek";
            this.chartYiedRateWeek.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartYiedRateWeek.Size = new System.Drawing.Size(296, 217);
            this.chartYiedRateWeek.TabIndex = 0;
            // 
            // chartYiedRateDay
            // 
            this.chartYiedRateDay.AutoLayout = false;
            this.chartYiedRateDay.CacheToMemory = true;
            this.chartYiedRateDay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartYiedRateDay.Legend.Name = "Default Legend";
            this.chartYiedRateDay.Location = new System.Drawing.Point(2, 31);
            this.chartYiedRateDay.Name = "chartYiedRateDay";
            this.chartYiedRateDay.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartYiedRateDay.Size = new System.Drawing.Size(276, 217);
            this.chartYiedRateDay.TabIndex = 0;
            // 
            // grdDayYieldRate
            // 
            this.grdDayYieldRate.Caption = "";
            this.grdDayYieldRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDayYieldRate.IsUsePaging = false;
            this.grdDayYieldRate.LanguageKey = null;
            this.grdDayYieldRate.Location = new System.Drawing.Point(2, 31);
            this.grdDayYieldRate.Margin = new System.Windows.Forms.Padding(0);
            this.grdDayYieldRate.Name = "grdDayYieldRate";
            this.grdDayYieldRate.ShowBorder = true;
            this.grdDayYieldRate.ShowButtonBar = false;
            this.grdDayYieldRate.Size = new System.Drawing.Size(836, 192);
            this.grdDayYieldRate.TabIndex = 0;
            this.grdDayYieldRate.UseAutoBestFitColumns = false;
            // 
            // topInfoPanel
            // 
            this.topInfoPanel.Controls.Add(this.periodSmartLabelTextBox);
            this.topInfoPanel.Controls.Add(this.itemSmartLabelTextBox);
            this.topInfoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topInfoPanel.Location = new System.Drawing.Point(2, 2);
            this.topInfoPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.topInfoPanel.Name = "topInfoPanel";
            this.topInfoPanel.Size = new System.Drawing.Size(844, 40);
            this.topInfoPanel.TabIndex = 0;
            // 
            // periodSmartLabelTextBox
            // 
            this.periodSmartLabelTextBox.EditorWidth = "200%";
            this.periodSmartLabelTextBox.LabelText = "기간";
            this.periodSmartLabelTextBox.LanguageKey = "PERIOD";
            this.periodSmartLabelTextBox.Location = new System.Drawing.Point(320, 10);
            this.periodSmartLabelTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.periodSmartLabelTextBox.Name = "periodSmartLabelTextBox";
            this.periodSmartLabelTextBox.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.periodSmartLabelTextBox.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.periodSmartLabelTextBox.Properties.ReadOnly = true;
            this.periodSmartLabelTextBox.Size = new System.Drawing.Size(292, 20);
            this.periodSmartLabelTextBox.TabIndex = 5;
            // 
            // itemSmartLabelTextBox
            // 
            this.itemSmartLabelTextBox.EditorWidth = "200%";
            this.itemSmartLabelTextBox.LabelText = "품목명";
            this.itemSmartLabelTextBox.LanguageKey = "PRODUCTDEFNAME";
            this.itemSmartLabelTextBox.Location = new System.Drawing.Point(5, 10);
            this.itemSmartLabelTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.itemSmartLabelTextBox.Name = "itemSmartLabelTextBox";
            this.itemSmartLabelTextBox.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.itemSmartLabelTextBox.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.itemSmartLabelTextBox.Properties.ReadOnly = true;
            this.itemSmartLabelTextBox.Size = new System.Drawing.Size(292, 20);
            this.itemSmartLabelTextBox.TabIndex = 4;
            // 
            // gbMonthYield
            // 
            this.gbMonthYield.Controls.Add(this.chartYiedRateMonth);
            this.gbMonthYield.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.gbMonthYield.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMonthYield.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.gbMonthYield.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.gbMonthYield.LanguageKey = "YIELDRATEMONTHLY";
            this.gbMonthYield.Location = new System.Drawing.Point(0, 0);
            this.gbMonthYield.Name = "gbMonthYield";
            this.gbMonthYield.ShowBorder = true;
            this.gbMonthYield.Size = new System.Drawing.Size(250, 250);
            this.gbMonthYield.TabIndex = 1;
            // 
            // chartYiedRateMonth
            // 
            this.chartYiedRateMonth.AutoLayout = false;
            this.chartYiedRateMonth.CacheToMemory = true;
            this.chartYiedRateMonth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartYiedRateMonth.Legend.Name = "Default Legend";
            this.chartYiedRateMonth.Location = new System.Drawing.Point(2, 31);
            this.chartYiedRateMonth.Name = "chartYiedRateMonth";
            this.chartYiedRateMonth.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartYiedRateMonth.Size = new System.Drawing.Size(246, 217);
            this.chartYiedRateMonth.TabIndex = 1;
            // 
            // gbWeekYield
            // 
            this.gbWeekYield.Controls.Add(this.chartYiedRateWeek);
            this.gbWeekYield.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.gbWeekYield.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbWeekYield.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.gbWeekYield.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.gbWeekYield.LanguageKey = "YIELDRATEWEEKLY";
            this.gbWeekYield.Location = new System.Drawing.Point(0, 0);
            this.gbWeekYield.Name = "gbWeekYield";
            this.gbWeekYield.ShowBorder = true;
            this.gbWeekYield.Size = new System.Drawing.Size(300, 250);
            this.gbWeekYield.TabIndex = 1;
            // 
            // gbDayYield
            // 
            this.gbDayYield.Controls.Add(this.chartYiedRateDay);
            this.gbDayYield.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.gbDayYield.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDayYield.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.gbDayYield.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.gbDayYield.LanguageKey = "YIELDRATEDAILY";
            this.gbDayYield.Location = new System.Drawing.Point(0, 0);
            this.gbDayYield.Name = "gbDayYield";
            this.gbDayYield.ShowBorder = true;
            this.gbDayYield.Size = new System.Drawing.Size(280, 250);
            this.gbDayYield.TabIndex = 1;
            // 
            // gbGridPeriodYield
            // 
            this.gbGridPeriodYield.Controls.Add(this.grdDayYieldRate);
            this.gbGridPeriodYield.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.gbGridPeriodYield.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbGridPeriodYield.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.gbGridPeriodYield.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.gbGridPeriodYield.Location = new System.Drawing.Point(0, 0);
            this.gbGridPeriodYield.Name = "gbGridPeriodYield";
            this.gbGridPeriodYield.ShowBorder = true;
            this.gbGridPeriodYield.Size = new System.Drawing.Size(840, 225);
            this.gbGridPeriodYield.TabIndex = 1;
            // 
            // ucDayYieldRate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucMainPanel);
            this.Name = "ucDayYieldRate";
            this.Size = new System.Drawing.Size(848, 528);
            this.Load += new System.EventHandler(this.ucDayYieldRate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ucMainPanel)).EndInit();
            this.ucMainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ucChartGridPanel)).EndInit();
            this.ucChartGridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ucDayMainSpliter)).EndInit();
            this.ucDayMainSpliter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ucDayUpperSpliter)).EndInit();
            this.ucDayUpperSpliter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ucDayUpper2Spliter)).EndInit();
            this.ucDayUpper2Spliter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartYiedRateWeek)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartYiedRateDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topInfoPanel)).EndInit();
            this.topInfoPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.periodSmartLabelTextBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemSmartLabelTextBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbMonthYield)).EndInit();
            this.gbMonthYield.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartYiedRateMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbWeekYield)).EndInit();
            this.gbWeekYield.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbDayYield)).EndInit();
            this.gbDayYield.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbGridPeriodYield)).EndInit();
            this.gbGridPeriodYield.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartPanel ucMainPanel;
        private Framework.SmartControls.SmartPanel ucChartGridPanel;
        private Framework.SmartControls.SmartPanel topInfoPanel;
        private Framework.SmartControls.SmartSpliterContainer ucDayMainSpliter;
        private Framework.SmartControls.SmartBandedGrid grdDayYieldRate;
        private Framework.SmartControls.SmartLabelTextBox itemSmartLabelTextBox;
        private Framework.SmartControls.SmartLabelTextBox periodSmartLabelTextBox;
        private Framework.SmartControls.SmartSpliterContainer ucDayUpperSpliter;
        private Framework.SmartControls.SmartSpliterContainer ucDayUpper2Spliter;
        private Framework.SmartControls.SmartChart chartYiedRateWeek;
        private Framework.SmartControls.SmartChart chartYiedRateDay;
        private Framework.SmartControls.SmartGroupBox gbMonthYield;
        private Framework.SmartControls.SmartChart chartYiedRateMonth;
        private Framework.SmartControls.SmartGroupBox gbWeekYield;
        private Framework.SmartControls.SmartGroupBox gbDayYield;
        private Framework.SmartControls.SmartGroupBox gbGridPeriodYield;
    }
}
