namespace Micube.SmartMES.SPC
{
    partial class TestSpcBaseChart0302DB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestSpcBaseChart0302DB));
            this.tabMain = new Micube.Framework.SmartControls.SmartTabControl();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.smartSpreadSheet1 = new Micube.Framework.SmartControls.SmartSpreadSheet();
            this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
            this.grdOverRules = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.btnSearchData = new Micube.Framework.SmartControls.SmartButton();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnExcelImageInsert = new Micube.Framework.SmartControls.SmartButton();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.btnExport = new Micube.Framework.SmartControls.SmartButton();
            this.txtLocation = new Micube.Framework.SmartControls.SmartTextBox();
            this.txtChangeData = new Micube.Framework.SmartControls.SmartTextBox();
            this.txtImageLocation = new Micube.Framework.SmartControls.SmartTextBox();
            this.smartPanel2 = new Micube.Framework.SmartControls.SmartPanel();
            this.btnPrint = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.xtraTabPage3.SuspendLayout();
            this.xtraTabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLocation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChangeData.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtImageLocation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).BeginInit();
            this.smartPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 633);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(850, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartPanel2);
            this.pnlContent.Controls.Add(this.smartPanel1);
            this.pnlContent.Size = new System.Drawing.Size(850, 637);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1155, 666);
            // 
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(2, 2);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabMain.SelectedTabPage = this.xtraTabPage3;
            this.tabMain.Size = new System.Drawing.Size(846, 549);
            this.tabMain.TabIndex = 3;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage3,
            this.xtraTabPage4});
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.smartSpreadSheet1);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(840, 520);
            this.xtraTabPage3.Text = "Raw Data";
            // 
            // smartSpreadSheet1
            // 
            this.smartSpreadSheet1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpreadSheet1.Location = new System.Drawing.Point(0, 0);
            this.smartSpreadSheet1.Name = "smartSpreadSheet1";
            this.smartSpreadSheet1.Options.Import.Csv.Encoding = ((System.Text.Encoding)(resources.GetObject("smartSpreadSheet1.Options.Import.Csv.Encoding")));
            this.smartSpreadSheet1.Options.Import.Txt.Encoding = ((System.Text.Encoding)(resources.GetObject("smartSpreadSheet1.Options.Import.Txt.Encoding")));
            this.smartSpreadSheet1.Size = new System.Drawing.Size(840, 520);
            this.smartSpreadSheet1.TabIndex = 0;
            this.smartSpreadSheet1.Text = "smartSpreadSheet1";
            // 
            // xtraTabPage4
            // 
            this.xtraTabPage4.Controls.Add(this.grdOverRules);
            this.xtraTabPage4.Name = "xtraTabPage4";
            this.xtraTabPage4.Size = new System.Drawing.Size(465, 284);
            this.xtraTabPage4.Text = "Over Rules";
            // 
            // grdOverRules
            // 
            this.grdOverRules.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdOverRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOverRules.IsUsePaging = false;
            this.grdOverRules.LanguageKey = "SPCOVERRAWDATA";
            this.grdOverRules.Location = new System.Drawing.Point(0, 0);
            this.grdOverRules.Margin = new System.Windows.Forms.Padding(0);
            this.grdOverRules.Name = "grdOverRules";
            this.grdOverRules.Padding = new System.Windows.Forms.Padding(3);
            this.grdOverRules.ShowBorder = true;
            this.grdOverRules.ShowStatusBar = false;
            this.grdOverRules.Size = new System.Drawing.Size(465, 284);
            this.grdOverRules.TabIndex = 1;
            // 
            // btnSearchData
            // 
            this.btnSearchData.AllowFocus = false;
            this.btnSearchData.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchData.Appearance.Options.UseFont = true;
            this.btnSearchData.IsBusy = false;
            this.btnSearchData.IsWrite = false;
            this.btnSearchData.Location = new System.Drawing.Point(15, 4);
            this.btnSearchData.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSearchData.Name = "btnSearchData";
            this.btnSearchData.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSearchData.Size = new System.Drawing.Size(151, 34);
            this.btnSearchData.TabIndex = 5;
            this.btnSearchData.Text = "메모 위치에 자료 입력";
            this.btnSearchData.TooltipLanguageKey = "";
            this.btnSearchData.Click += new System.EventHandler(this.btnSearchData_Click);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnSearchData;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(276, 36);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // btnExcelImageInsert
            // 
            this.btnExcelImageInsert.AllowFocus = false;
            this.btnExcelImageInsert.IsBusy = false;
            this.btnExcelImageInsert.IsWrite = false;
            this.btnExcelImageInsert.Location = new System.Drawing.Point(15, 47);
            this.btnExcelImageInsert.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnExcelImageInsert.Name = "btnExcelImageInsert";
            this.btnExcelImageInsert.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnExcelImageInsert.Size = new System.Drawing.Size(151, 25);
            this.btnExcelImageInsert.TabIndex = 5;
            this.btnExcelImageInsert.Text = "Image 입력";
            this.btnExcelImageInsert.TooltipLanguageKey = "";
            this.btnExcelImageInsert.Click += new System.EventHandler(this.btnExcelImageInsert_Click);
            // 
            // smartPanel1
            // 
            this.smartPanel1.Controls.Add(this.btnPrint);
            this.smartPanel1.Controls.Add(this.btnExport);
            this.smartPanel1.Controls.Add(this.txtLocation);
            this.smartPanel1.Controls.Add(this.btnSearchData);
            this.smartPanel1.Controls.Add(this.txtChangeData);
            this.smartPanel1.Controls.Add(this.txtImageLocation);
            this.smartPanel1.Controls.Add(this.btnExcelImageInsert);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(850, 84);
            this.smartPanel1.TabIndex = 4;
            // 
            // btnExport
            // 
            this.btnExport.AllowFocus = false;
            this.btnExport.IsBusy = false;
            this.btnExport.IsWrite = false;
            this.btnExport.Location = new System.Drawing.Point(509, 9);
            this.btnExport.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnExport.Name = "btnExport";
            this.btnExport.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnExport.Size = new System.Drawing.Size(151, 56);
            this.btnExport.TabIndex = 9;
            this.btnExport.Text = "엑셀 Export";
            this.btnExport.TooltipLanguageKey = "";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // txtLocation
            // 
            this.txtLocation.LabelText = null;
            this.txtLocation.LanguageKey = null;
            this.txtLocation.Location = new System.Drawing.Point(303, 11);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(200, 20);
            this.txtLocation.TabIndex = 8;
            // 
            // txtChangeData
            // 
            this.txtChangeData.LabelText = null;
            this.txtChangeData.LanguageKey = null;
            this.txtChangeData.Location = new System.Drawing.Point(181, 11);
            this.txtChangeData.Name = "txtChangeData";
            this.txtChangeData.Size = new System.Drawing.Size(100, 20);
            this.txtChangeData.TabIndex = 7;
            // 
            // txtImageLocation
            // 
            this.txtImageLocation.LabelText = "좌표";
            this.txtImageLocation.LanguageKey = null;
            this.txtImageLocation.Location = new System.Drawing.Point(181, 52);
            this.txtImageLocation.Name = "txtImageLocation";
            this.txtImageLocation.Size = new System.Drawing.Size(100, 20);
            this.txtImageLocation.TabIndex = 6;
            // 
            // smartPanel2
            // 
            this.smartPanel2.Controls.Add(this.tabMain);
            this.smartPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel2.Location = new System.Drawing.Point(0, 84);
            this.smartPanel2.Name = "smartPanel2";
            this.smartPanel2.Size = new System.Drawing.Size(850, 553);
            this.smartPanel2.TabIndex = 5;
            // 
            // btnPrint
            // 
            this.btnPrint.AllowFocus = false;
            this.btnPrint.IsBusy = false;
            this.btnPrint.IsWrite = false;
            this.btnPrint.Location = new System.Drawing.Point(666, 9);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPrint.Size = new System.Drawing.Size(151, 56);
            this.btnPrint.TabIndex = 10;
            this.btnPrint.Text = "Print";
            this.btnPrint.TooltipLanguageKey = "";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // TestSpcBaseChart0302DB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1175, 686);
            this.Name = "TestSpcBaseChart0302DB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TestSpcBaseChart0302DB";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TestSpcBaseChart0302DB_Load);
            this.Resize += new System.EventHandler(this.TestSpcBaseChart0302DB_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.xtraTabPage3.ResumeLayout(false);
            this.xtraTabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtLocation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChangeData.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtImageLocation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).EndInit();
            this.smartPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private Framework.SmartControls.SmartSpreadSheet smartSpreadSheet1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage4;
        private Framework.SmartControls.SmartBandedGrid grdOverRules;
        private Framework.SmartControls.SmartButton btnSearchData;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private Framework.SmartControls.SmartButton btnExcelImageInsert;
        private Framework.SmartControls.SmartPanel smartPanel2;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartTextBox txtChangeData;
        private Framework.SmartControls.SmartTextBox txtImageLocation;
        private Framework.SmartControls.SmartTextBox txtLocation;
        private Framework.SmartControls.SmartButton btnExport;
        private Framework.SmartControls.SmartButton btnPrint;
    }
}