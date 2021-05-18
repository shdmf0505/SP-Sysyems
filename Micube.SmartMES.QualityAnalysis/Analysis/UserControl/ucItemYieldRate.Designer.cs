namespace Micube.SmartMES.QualityAnalysis
{
    partial class ucItemYieldRate
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
            this.pnlMain = new Micube.Framework.SmartControls.SmartPanel();
            this.pnlBody = new Micube.Framework.SmartControls.SmartPanel();
            this.ucItemMainSpliter = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.gbItemYieldRate = new Micube.Framework.SmartControls.SmartGroupBox();
            this.chartItemYield = new Micube.Framework.SmartControls.SmartChart();
            this.ucItemLowerSpliter = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.chartDefectCode = new Micube.Framework.SmartControls.SmartChart();
            this.chartLotYield = new Micube.Framework.SmartControls.SmartChart();
            this.pnlTopInfo = new Micube.Framework.SmartControls.SmartPanel();
            this.cboItemYieldChartViewType = new Micube.Framework.SmartControls.SmartComboBox();
            this.periodSmartLabelTextBox = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.itemSmartLabelTextBox = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.gbItemDefectCodeChart = new Micube.Framework.SmartControls.SmartGroupBox();
            this.gbItemLOTDefectChart = new Micube.Framework.SmartControls.SmartGroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBody)).BeginInit();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ucItemMainSpliter)).BeginInit();
            this.ucItemMainSpliter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbItemYieldRate)).BeginInit();
            this.gbItemYieldRate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartItemYield)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ucItemLowerSpliter)).BeginInit();
            this.ucItemLowerSpliter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDefectCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartLotYield)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTopInfo)).BeginInit();
            this.pnlTopInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboItemYieldChartViewType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodSmartLabelTextBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemSmartLabelTextBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbItemDefectCodeChart)).BeginInit();
            this.gbItemDefectCodeChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbItemLOTDefectChart)).BeginInit();
            this.gbItemLOTDefectChart.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlBody);
            this.pnlMain.Controls.Add(this.pnlTopInfo);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(850, 583);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlBody
            // 
            this.pnlBody.Controls.Add(this.ucItemMainSpliter);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(2, 49);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(846, 532);
            this.pnlBody.TabIndex = 2;
            // 
            // ucItemMainSpliter
            // 
            this.ucItemMainSpliter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucItemMainSpliter.Horizontal = false;
            this.ucItemMainSpliter.Location = new System.Drawing.Point(2, 2);
            this.ucItemMainSpliter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.ucItemMainSpliter.Name = "ucItemMainSpliter";
            this.ucItemMainSpliter.Panel1.Controls.Add(this.gbItemYieldRate);
            this.ucItemMainSpliter.Panel1.Text = "Panel1";
            this.ucItemMainSpliter.Panel2.Controls.Add(this.ucItemLowerSpliter);
            this.ucItemMainSpliter.Panel2.Text = "Panel2";
            this.ucItemMainSpliter.Size = new System.Drawing.Size(842, 528);
            this.ucItemMainSpliter.SplitterPosition = 292;
            this.ucItemMainSpliter.TabIndex = 0;
            this.ucItemMainSpliter.Text = "smartSpliterContainer1";
            // 
            // gbItemYieldRate
            // 
            this.gbItemYieldRate.Controls.Add(this.chartItemYield);
            this.gbItemYieldRate.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.gbItemYieldRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbItemYieldRate.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.gbItemYieldRate.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.gbItemYieldRate.LanguageKey = "ITEMYIELDRATE";
            this.gbItemYieldRate.Location = new System.Drawing.Point(0, 0);
            this.gbItemYieldRate.Name = "gbItemYieldRate";
            this.gbItemYieldRate.ShowBorder = true;
            this.gbItemYieldRate.Size = new System.Drawing.Size(842, 292);
            this.gbItemYieldRate.TabIndex = 1;
            // 
            // chartItemYield
            // 
            this.chartItemYield.AutoLayout = false;
            this.chartItemYield.CacheToMemory = true;
            this.chartItemYield.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartItemYield.Legend.Name = "Default Legend";
            this.chartItemYield.Location = new System.Drawing.Point(2, 31);
            this.chartItemYield.Name = "chartItemYield";
            this.chartItemYield.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartItemYield.Size = new System.Drawing.Size(838, 259);
            this.chartItemYield.TabIndex = 1;
            // 
            // ucItemLowerSpliter
            // 
            this.ucItemLowerSpliter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucItemLowerSpliter.Location = new System.Drawing.Point(0, 0);
            this.ucItemLowerSpliter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.ucItemLowerSpliter.Name = "ucItemLowerSpliter";
            this.ucItemLowerSpliter.Panel1.Controls.Add(this.gbItemDefectCodeChart);
            this.ucItemLowerSpliter.Panel1.Text = "Panel1";
            this.ucItemLowerSpliter.Panel2.Controls.Add(this.gbItemLOTDefectChart);
            this.ucItemLowerSpliter.Panel2.Text = "Panel2";
            this.ucItemLowerSpliter.Size = new System.Drawing.Size(842, 231);
            this.ucItemLowerSpliter.SplitterPosition = 450;
            this.ucItemLowerSpliter.TabIndex = 0;
            this.ucItemLowerSpliter.Text = "smartSpliterContainer2";
            // 
            // chartDefectCode
            // 
            this.chartDefectCode.AutoLayout = false;
            this.chartDefectCode.CacheToMemory = true;
            this.chartDefectCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartDefectCode.Legend.Name = "Default Legend";
            this.chartDefectCode.Location = new System.Drawing.Point(2, 31);
            this.chartDefectCode.Name = "chartDefectCode";
            this.chartDefectCode.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartDefectCode.Size = new System.Drawing.Size(446, 198);
            this.chartDefectCode.TabIndex = 1;
            // 
            // chartLotYield
            // 
            this.chartLotYield.AutoLayout = false;
            this.chartLotYield.CacheToMemory = true;
            this.chartLotYield.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartLotYield.Legend.Name = "Default Legend";
            this.chartLotYield.Location = new System.Drawing.Point(2, 31);
            this.chartLotYield.Name = "chartLotYield";
            this.chartLotYield.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartLotYield.Size = new System.Drawing.Size(383, 198);
            this.chartLotYield.TabIndex = 0;
            // 
            // pnlTopInfo
            // 
            this.pnlTopInfo.Controls.Add(this.cboItemYieldChartViewType);
            this.pnlTopInfo.Controls.Add(this.periodSmartLabelTextBox);
            this.pnlTopInfo.Controls.Add(this.itemSmartLabelTextBox);
            this.pnlTopInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopInfo.Location = new System.Drawing.Point(2, 2);
            this.pnlTopInfo.Name = "pnlTopInfo";
            this.pnlTopInfo.Size = new System.Drawing.Size(846, 47);
            this.pnlTopInfo.TabIndex = 1;
            // 
            // cboItemYieldChartViewType
            // 
            this.cboItemYieldChartViewType.LabelText = null;
            this.cboItemYieldChartViewType.LanguageKey = null;
            this.cboItemYieldChartViewType.Location = new System.Drawing.Point(650, 12);
            this.cboItemYieldChartViewType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboItemYieldChartViewType.Name = "cboItemYieldChartViewType";
            this.cboItemYieldChartViewType.PopupWidth = 0;
            this.cboItemYieldChartViewType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboItemYieldChartViewType.Properties.DropDownRows = 2;
            this.cboItemYieldChartViewType.Properties.NullText = "";
            this.cboItemYieldChartViewType.ShowHeader = true;
            this.cboItemYieldChartViewType.Size = new System.Drawing.Size(109, 20);
            this.cboItemYieldChartViewType.TabIndex = 11;
            this.cboItemYieldChartViewType.VisibleColumns = null;
            this.cboItemYieldChartViewType.VisibleColumnsWidth = null;
            // 
            // periodSmartLabelTextBox
            // 
            this.periodSmartLabelTextBox.EditorWidth = "200%";
            this.periodSmartLabelTextBox.LabelText = "기간";
            this.periodSmartLabelTextBox.LanguageKey = "PERIOD";
            this.periodSmartLabelTextBox.Location = new System.Drawing.Point(320, 12);
            this.periodSmartLabelTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.periodSmartLabelTextBox.Name = "periodSmartLabelTextBox";
            this.periodSmartLabelTextBox.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.periodSmartLabelTextBox.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.periodSmartLabelTextBox.Properties.ReadOnly = true;
            this.periodSmartLabelTextBox.Size = new System.Drawing.Size(292, 20);
            this.periodSmartLabelTextBox.TabIndex = 10;
            // 
            // itemSmartLabelTextBox
            // 
            this.itemSmartLabelTextBox.EditorWidth = "200%";
            this.itemSmartLabelTextBox.LabelText = "품목명";
            this.itemSmartLabelTextBox.LanguageKey = "PRODUCTDEFNAME";
            this.itemSmartLabelTextBox.Location = new System.Drawing.Point(5, 12);
            this.itemSmartLabelTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.itemSmartLabelTextBox.Name = "itemSmartLabelTextBox";
            this.itemSmartLabelTextBox.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.itemSmartLabelTextBox.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.itemSmartLabelTextBox.Properties.ReadOnly = true;
            this.itemSmartLabelTextBox.Size = new System.Drawing.Size(292, 20);
            this.itemSmartLabelTextBox.TabIndex = 9;
            // 
            // gbItemDefectCodeChart
            // 
            this.gbItemDefectCodeChart.Controls.Add(this.chartDefectCode);
            this.gbItemDefectCodeChart.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.gbItemDefectCodeChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbItemDefectCodeChart.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.gbItemDefectCodeChart.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.gbItemDefectCodeChart.Location = new System.Drawing.Point(0, 0);
            this.gbItemDefectCodeChart.Name = "gbItemDefectCodeChart";
            this.gbItemDefectCodeChart.ShowBorder = true;
            this.gbItemDefectCodeChart.Size = new System.Drawing.Size(450, 231);
            this.gbItemDefectCodeChart.TabIndex = 2;
            // 
            // gbItemLOTDefectChart
            // 
            this.gbItemLOTDefectChart.Controls.Add(this.chartLotYield);
            this.gbItemLOTDefectChart.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.gbItemLOTDefectChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbItemLOTDefectChart.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.gbItemLOTDefectChart.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.gbItemLOTDefectChart.Location = new System.Drawing.Point(0, 0);
            this.gbItemLOTDefectChart.Name = "gbItemLOTDefectChart";
            this.gbItemLOTDefectChart.ShowBorder = true;
            this.gbItemLOTDefectChart.Size = new System.Drawing.Size(387, 231);
            this.gbItemLOTDefectChart.TabIndex = 1;
            // 
            // ucItemYieldRate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Name = "ucItemYieldRate";
            this.Size = new System.Drawing.Size(850, 583);
            this.Load += new System.EventHandler(this.ucItemYieldRate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlBody)).EndInit();
            this.pnlBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ucItemMainSpliter)).EndInit();
            this.ucItemMainSpliter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbItemYieldRate)).EndInit();
            this.gbItemYieldRate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartItemYield)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ucItemLowerSpliter)).EndInit();
            this.ucItemLowerSpliter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartDefectCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartLotYield)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTopInfo)).EndInit();
            this.pnlTopInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboItemYieldChartViewType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodSmartLabelTextBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemSmartLabelTextBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbItemDefectCodeChart)).EndInit();
            this.gbItemDefectCodeChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbItemLOTDefectChart)).EndInit();
            this.gbItemLOTDefectChart.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartPanel pnlMain;
        private Framework.SmartControls.SmartPanel pnlTopInfo;
        private Framework.SmartControls.SmartComboBox cboItemYieldChartViewType;
        private Framework.SmartControls.SmartLabelTextBox periodSmartLabelTextBox;
        private Framework.SmartControls.SmartLabelTextBox itemSmartLabelTextBox;
        private Framework.SmartControls.SmartPanel pnlBody;
        private Framework.SmartControls.SmartSpliterContainer ucItemMainSpliter;
        private Framework.SmartControls.SmartSpliterContainer ucItemLowerSpliter;
        private Framework.SmartControls.SmartChart chartDefectCode;
        private Framework.SmartControls.SmartChart chartLotYield;
        private Framework.SmartControls.SmartGroupBox gbItemYieldRate;
        private Framework.SmartControls.SmartChart chartItemYield;
        private Framework.SmartControls.SmartGroupBox gbItemDefectCodeChart;
        private Framework.SmartControls.SmartGroupBox gbItemLOTDefectChart;
    }
}
