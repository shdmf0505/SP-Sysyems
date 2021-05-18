namespace Micube.SmartMES.SPC
{
    partial class TestSpcBaseChart05XBarFrame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestSpcBaseChart05XBarFrame));
            this.tabMain = new Micube.Framework.SmartControls.SmartTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.ucXBarFrame1 = new Micube.SmartMES.SPC.UserControl.ucXBarFrame();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.ucCpkFrame1 = new Micube.SmartMES.SPC.UserControl.ucCpkFrame();
            this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
            this.grdOverRules = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.smartSpreadSheet1 = new Micube.Framework.SmartControls.SmartSpreadSheet();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            this.xtraTabPage4.SuspendLayout();
            this.xtraTabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabMain.SelectedTabPage = this.xtraTabPage1;
            this.tabMain.Size = new System.Drawing.Size(1079, 574);
            this.tabMain.TabIndex = 4;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage4,
            this.xtraTabPage3});
            this.tabMain.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tabMain_SelectedPageChanged);
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.ucXBarFrame1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1073, 545);
            this.xtraTabPage1.Text = "관리도";
            // 
            // ucXBarFrame1
            // 
            this.ucXBarFrame1.Appearance.BackColor = System.Drawing.Color.White;
            this.ucXBarFrame1.Appearance.Options.UseBackColor = true;
            this.ucXBarFrame1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucXBarFrame1.Location = new System.Drawing.Point(0, 0);
            this.ucXBarFrame1.Name = "ucXBarFrame1";
            this.ucXBarFrame1.Padding = new System.Windows.Forms.Padding(3);
            this.ucXBarFrame1.Size = new System.Drawing.Size(1073, 545);
            this.ucXBarFrame1.TabIndex = 0;
            this.ucXBarFrame1.Load += new System.EventHandler(this.ucXBarFrame1_Load);
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.ucCpkFrame1);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1073, 545);
            this.xtraTabPage2.Text = "공정능력";
            // 
            // ucCpkFrame1
            // 
            this.ucCpkFrame1.Appearance.BackColor = System.Drawing.Color.White;
            this.ucCpkFrame1.Appearance.Options.UseBackColor = true;
            this.ucCpkFrame1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCpkFrame1.Location = new System.Drawing.Point(0, 0);
            this.ucCpkFrame1.Name = "ucCpkFrame1";
            this.ucCpkFrame1.Padding = new System.Windows.Forms.Padding(3);
            this.ucCpkFrame1.Size = new System.Drawing.Size(1073, 545);
            this.ucCpkFrame1.TabIndex = 0;
            // 
            // xtraTabPage4
            // 
            this.xtraTabPage4.Controls.Add(this.grdOverRules);
            this.xtraTabPage4.Name = "xtraTabPage4";
            this.xtraTabPage4.Size = new System.Drawing.Size(1073, 545);
            this.xtraTabPage4.Text = "Over Rules";
            // 
            // grdOverRules
            // 
            this.grdOverRules.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdOverRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOverRules.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdOverRules.IsUsePaging = false;
            this.grdOverRules.LanguageKey = "SPCOVERRAWDATA";
            this.grdOverRules.Location = new System.Drawing.Point(0, 0);
            this.grdOverRules.Margin = new System.Windows.Forms.Padding(0);
            this.grdOverRules.Name = "grdOverRules";
            this.grdOverRules.Padding = new System.Windows.Forms.Padding(3);
            this.grdOverRules.ShowBorder = true;
            this.grdOverRules.ShowStatusBar = false;
            this.grdOverRules.Size = new System.Drawing.Size(1073, 545);
            this.grdOverRules.TabIndex = 1;
            this.grdOverRules.UseAutoBestFitColumns = false;
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.smartSpreadSheet1);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(1073, 545);
            this.xtraTabPage3.Text = "Raw Data";
            // 
            // smartSpreadSheet1
            // 
            this.smartSpreadSheet1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpreadSheet1.Location = new System.Drawing.Point(0, 0);
            this.smartSpreadSheet1.Name = "smartSpreadSheet1";
            this.smartSpreadSheet1.Options.Import.Csv.Encoding = ((System.Text.Encoding)(resources.GetObject("smartSpreadSheet1.Options.Import.Csv.Encoding")));
            this.smartSpreadSheet1.Options.Import.Txt.Encoding = ((System.Text.Encoding)(resources.GetObject("smartSpreadSheet1.Options.Import.Txt.Encoding")));
            this.smartSpreadSheet1.Size = new System.Drawing.Size(1073, 545);
            this.smartSpreadSheet1.TabIndex = 0;
            this.smartSpreadSheet1.Text = "smartSpreadSheet1";
            // 
            // TestSpcBaseChart05XBarFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 574);
            this.Controls.Add(this.tabMain);
            this.Name = "TestSpcBaseChart05XBarFrame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TestSpcBaseChart05XBarFrame";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TestSpcBaseChart05XBarFrame_Load);
            this.Resize += new System.EventHandler(this.TestSpcBaseChart05XBarFrame_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage4.ResumeLayout(false);
            this.xtraTabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private UserControl.ucXBarFrame ucXBarFrame1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private UserControl.ucCpkFrame ucCpkFrame1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage4;
        private Framework.SmartControls.SmartBandedGrid grdOverRules;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private Framework.SmartControls.SmartSpreadSheet smartSpreadSheet1;
    }
}