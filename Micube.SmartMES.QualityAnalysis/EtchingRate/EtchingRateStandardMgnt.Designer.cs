namespace Micube.SmartMES.QualityAnalysis
{
    partial class EtchingRateStandardMgnt
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
            this.tabEtchingRate = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgMeasure = new DevExpress.XtraTab.XtraTabPage();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.grdSpec = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdMeasure = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgReMeasure = new DevExpress.XtraTab.XtraTabPage();
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.smartSpliterControl2 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.grdAbnormal = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdReMeasure = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.btnPopupFlag = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabEtchingRate)).BeginInit();
            this.tabEtchingRate.SuspendLayout();
            this.tpgMeasure.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            this.tpgReMeasure.SuspendLayout();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 495);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnPopupFlag);
            this.pnlToolbar.Size = new System.Drawing.Size(566, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.btnPopupFlag, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabEtchingRate);
            this.pnlContent.Size = new System.Drawing.Size(566, 499);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(871, 528);
            // 
            // tabEtchingRate
            // 
            this.tabEtchingRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabEtchingRate.Location = new System.Drawing.Point(0, 0);
            this.tabEtchingRate.Name = "tabEtchingRate";
            this.tabEtchingRate.SelectedTabPage = this.tpgMeasure;
            this.tabEtchingRate.Size = new System.Drawing.Size(566, 499);
            this.tabEtchingRate.TabIndex = 0;
            this.tabEtchingRate.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgMeasure,
            this.tpgReMeasure});
            // 
            // tpgMeasure
            // 
            this.tpgMeasure.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.tabEtchingRate.SetLanguageKey(this.tpgMeasure, "REGISTER/SEARCH");
            this.tpgMeasure.Name = "tpgMeasure";
            this.tpgMeasure.Padding = new System.Windows.Forms.Padding(10);
            this.tpgMeasure.Size = new System.Drawing.Size(560, 470);
            this.tpgMeasure.Text = "xtraTabPage1";
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartSpliterControl1, 0, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdSpec, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdMeasure, 0, 2);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 3;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(540, 450);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // smartSpliterControl1
            // 
            this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartSpliterControl1.Location = new System.Drawing.Point(0, 220);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(540, 5);
            this.smartSpliterControl1.TabIndex = 0;
            this.smartSpliterControl1.TabStop = false;
            // 
            // grdSpec
            // 
            this.grdSpec.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdSpec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSpec.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdSpec.IsUsePaging = false;
            this.grdSpec.LanguageKey = "SPECDEFINITIONLIST";
            this.grdSpec.Location = new System.Drawing.Point(0, 0);
            this.grdSpec.Margin = new System.Windows.Forms.Padding(0);
            this.grdSpec.Name = "grdSpec";
            this.grdSpec.ShowBorder = true;
            this.grdSpec.ShowStatusBar = false;
            this.grdSpec.Size = new System.Drawing.Size(540, 220);
            this.grdSpec.TabIndex = 1;
            // 
            // grdMeasure
            // 
            this.grdMeasure.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdMeasure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMeasure.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdMeasure.IsUsePaging = false;
            this.grdMeasure.LanguageKey = "MEASUREDVALUE";
            this.grdMeasure.Location = new System.Drawing.Point(0, 230);
            this.grdMeasure.Margin = new System.Windows.Forms.Padding(0);
            this.grdMeasure.Name = "grdMeasure";
            this.grdMeasure.ShowBorder = true;
            this.grdMeasure.ShowStatusBar = false;
            this.grdMeasure.Size = new System.Drawing.Size(540, 220);
            this.grdMeasure.TabIndex = 2;
            // 
            // tpgReMeasure
            // 
            this.tpgReMeasure.Controls.Add(this.smartSplitTableLayoutPanel2);
            this.tabEtchingRate.SetLanguageKey(this.tpgReMeasure, "ABNORMALOCCURRENCESEARCH");
            this.tpgReMeasure.Name = "tpgReMeasure";
            this.tpgReMeasure.Padding = new System.Windows.Forms.Padding(10);
            this.tpgReMeasure.Size = new System.Drawing.Size(469, 372);
            this.tpgReMeasure.Text = "xtraTabPage2";
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.ColumnCount = 1;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.smartSpliterControl2, 0, 1);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdAbnormal, 0, 0);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdReMeasure, 0, 2);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(10, 10);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 3;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(449, 352);
            this.smartSplitTableLayoutPanel2.TabIndex = 1;
            // 
            // smartSpliterControl2
            // 
            this.smartSpliterControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartSpliterControl2.Location = new System.Drawing.Point(0, 171);
            this.smartSpliterControl2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl2.Name = "smartSpliterControl2";
            this.smartSpliterControl2.Size = new System.Drawing.Size(449, 5);
            this.smartSpliterControl2.TabIndex = 0;
            this.smartSpliterControl2.TabStop = false;
            // 
            // grdAbnormal
            // 
            this.grdAbnormal.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdAbnormal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAbnormal.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdAbnormal.IsUsePaging = false;
            this.grdAbnormal.LanguageKey = "ABNORMALOCCURRENCELIST";
            this.grdAbnormal.Location = new System.Drawing.Point(0, 0);
            this.grdAbnormal.Margin = new System.Windows.Forms.Padding(0);
            this.grdAbnormal.Name = "grdAbnormal";
            this.grdAbnormal.ShowBorder = true;
            this.grdAbnormal.ShowStatusBar = false;
            this.grdAbnormal.Size = new System.Drawing.Size(449, 171);
            this.grdAbnormal.TabIndex = 1;
            // 
            // grdReMeasure
            // 
            this.grdReMeasure.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdReMeasure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReMeasure.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdReMeasure.IsUsePaging = false;
            this.grdReMeasure.LanguageKey = "REMEASURELIST";
            this.grdReMeasure.Location = new System.Drawing.Point(0, 181);
            this.grdReMeasure.Margin = new System.Windows.Forms.Padding(0);
            this.grdReMeasure.Name = "grdReMeasure";
            this.grdReMeasure.ShowBorder = true;
            this.grdReMeasure.ShowStatusBar = false;
            this.grdReMeasure.Size = new System.Drawing.Size(449, 171);
            this.grdReMeasure.TabIndex = 2;
            // 
            // btnPopupFlag
            // 
            this.btnPopupFlag.AllowFocus = false;
            this.btnPopupFlag.IsBusy = false;
            this.btnPopupFlag.IsWrite = true;
            this.btnPopupFlag.Location = new System.Drawing.Point(493, 0);
            this.btnPopupFlag.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPopupFlag.Name = "btnPopupFlag";
            this.btnPopupFlag.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPopupFlag.Size = new System.Drawing.Size(73, 23);
            this.btnPopupFlag.TabIndex = 5;
            this.btnPopupFlag.Text = "smartButton1";
            this.btnPopupFlag.TooltipLanguageKey = "";
            this.btnPopupFlag.Visible = false;
            // 
            // EtchingRateStandardMgnt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 548);
            this.Name = "EtchingRateStandardMgnt";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabEtchingRate)).EndInit();
            this.tabEtchingRate.ResumeLayout(false);
            this.tpgMeasure.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.tpgReMeasure.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabEtchingRate;
        private DevExpress.XtraTab.XtraTabPage tpgMeasure;
        private DevExpress.XtraTab.XtraTabPage tpgReMeasure;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
        private Framework.SmartControls.SmartBandedGrid grdSpec;
        private Framework.SmartControls.SmartBandedGrid grdMeasure;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl2;
        private Framework.SmartControls.SmartBandedGrid grdAbnormal;
        private Framework.SmartControls.SmartBandedGrid grdReMeasure;
        private Framework.SmartControls.SmartButton btnPopupFlag;
    }
}