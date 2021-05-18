namespace Micube.SmartMES.QualityAnalysis
{
    partial class ucRateDefault
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
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grbMain = new Micube.Framework.SmartControls.SmartGroupBox();
            this.chartMain = new Micube.Framework.SmartControls.SmartChart();
            this.grdMain = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grbMain)).BeginInit();
            this.grbMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).BeginInit();
            this.SuspendLayout();
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 2;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grbMain, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdMain, 1, 0);
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
            this.grbMain.Location = new System.Drawing.Point(3, 3);
            this.grbMain.Name = "grbMain";
            this.grbMain.ShowBorder = true;
            this.grbMain.Size = new System.Drawing.Size(343, 212);
            this.grbMain.TabIndex = 2;
            // 
            // chartMain
            // 
            this.chartMain.AutoLayout = false;
            this.chartMain.CacheToMemory = true;
            this.chartMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartMain.Legend.Name = "Default Legend";
            this.chartMain.Location = new System.Drawing.Point(2, 31);
            this.chartMain.Margin = new System.Windows.Forms.Padding(0);
            this.chartMain.Name = "chartMain";
            this.chartMain.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartMain.Size = new System.Drawing.Size(339, 179);
            this.chartMain.TabIndex = 0;
            // 
            // grdMain
            // 
            this.grdMain.Caption = "";
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.IsUsePaging = false;
            this.grdMain.LanguageKey = null;
            this.grdMain.Location = new System.Drawing.Point(349, 0);
            this.grdMain.Margin = new System.Windows.Forms.Padding(0);
            this.grdMain.Name = "grdMain";
            this.grdMain.Padding = new System.Windows.Forms.Padding(3);
            this.grdMain.ShowBorder = true;
            this.grdMain.Size = new System.Drawing.Size(649, 218);
            this.grdMain.TabIndex = 3;
            // 
            // ucRateDefault
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.Name = "ucRateDefault";
            this.Size = new System.Drawing.Size(998, 218);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grbMain)).EndInit();
            this.grbMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartGroupBox grbMain;
        private Framework.SmartControls.SmartChart chartMain;
        private Framework.SmartControls.SmartBandedGrid grdMain;
    }
}
