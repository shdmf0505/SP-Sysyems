namespace Micube.SmartMES.ProcessManagement
{
    partial class WorkResultPerManageSegment
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
			this.grdWorkResultPerPNL = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.tabWorkResultPerSegment = new Micube.Framework.SmartControls.SmartTabControl();
			this.perPNLPage = new DevExpress.XtraTab.XtraTabPage();
			this.perMMPage = new DevExpress.XtraTab.XtraTabPage();
			this.grdWorkResultPerMM = new Micube.Framework.SmartControls.SmartBandedGrid();
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
			this.pnlContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tabWorkResultPerSegment)).BeginInit();
			this.tabWorkResultPerSegment.SuspendLayout();
			this.perPNLPage.SuspendLayout();
			this.perMMPage.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlCondition
			// 
			this.pnlCondition.Location = new System.Drawing.Point(2, 31);
			this.pnlCondition.Size = new System.Drawing.Size(296, 379);
			// 
			// pnlToolbar
			// 
			this.pnlToolbar.Size = new System.Drawing.Size(457, 24);
			// 
			// pnlContent
			// 
			this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
			this.pnlContent.Appearance.Options.UseBackColor = true;
			this.pnlContent.Controls.Add(this.tabWorkResultPerSegment);
			this.pnlContent.Size = new System.Drawing.Size(457, 383);
			// 
			// pnlMain
			// 
			this.pnlMain.Location = new System.Drawing.Point(19, 19);
			this.pnlMain.Size = new System.Drawing.Size(762, 412);
			// 
			// grdWorkResultPerPNL
			// 
			this.grdWorkResultPerPNL.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdWorkResultPerPNL.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdWorkResultPerPNL.IsUsePaging = false;
			this.grdWorkResultPerPNL.LanguageKey = "WORKRESULTPROCESSSEGMENTMNG";
			this.grdWorkResultPerPNL.Location = new System.Drawing.Point(0, 0);
			this.grdWorkResultPerPNL.Margin = new System.Windows.Forms.Padding(0);
			this.grdWorkResultPerPNL.Name = "grdWorkResultPerPNL";
			this.grdWorkResultPerPNL.ShowBorder = true;
			this.grdWorkResultPerPNL.ShowStatusBar = false;
			this.grdWorkResultPerPNL.Size = new System.Drawing.Size(451, 354);
			this.grdWorkResultPerPNL.TabIndex = 0;
			// 
			// tabWorkResultPerSegment
			// 
			this.tabWorkResultPerSegment.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabWorkResultPerSegment.Location = new System.Drawing.Point(0, 0);
			this.tabWorkResultPerSegment.Name = "tabWorkResultPerSegment";
			this.tabWorkResultPerSegment.SelectedTabPage = this.perPNLPage;
			this.tabWorkResultPerSegment.Size = new System.Drawing.Size(457, 383);
			this.tabWorkResultPerSegment.TabIndex = 1;
			this.tabWorkResultPerSegment.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.perPNLPage,
            this.perMMPage});
			// 
			// perPNLPage
			// 
			this.perPNLPage.Controls.Add(this.grdWorkResultPerPNL);
			this.tabWorkResultPerSegment.SetLanguageKey(this.perPNLPage, "PNL");
			this.perPNLPage.Name = "perPNLPage";
			this.perPNLPage.Size = new System.Drawing.Size(451, 354);
			this.perPNLPage.Text = "PNL";
			// 
			// perMMPage
			// 
			this.perMMPage.Controls.Add(this.grdWorkResultPerMM);
			this.tabWorkResultPerSegment.SetLanguageKey(this.perMMPage, "MM");
			this.perMMPage.Name = "perMMPage";
			this.perMMPage.Size = new System.Drawing.Size(451, 354);
			this.perMMPage.Text = "MM";
			// 
			// grdWorkResultPerMM
			// 
			this.grdWorkResultPerMM.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdWorkResultPerMM.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdWorkResultPerMM.IsUsePaging = false;
			this.grdWorkResultPerMM.LanguageKey = "WORKRESULTPROCESSSEGMENTMNG";
			this.grdWorkResultPerMM.Location = new System.Drawing.Point(0, 0);
			this.grdWorkResultPerMM.Margin = new System.Windows.Forms.Padding(0);
			this.grdWorkResultPerMM.Name = "grdWorkResultPerMM";
			this.grdWorkResultPerMM.ShowBorder = true;
			this.grdWorkResultPerMM.Size = new System.Drawing.Size(451, 354);
			this.grdWorkResultPerMM.TabIndex = 0;
			// 
			// WorkResultPerManageSegment
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Name = "WorkResultPerManageSegment";
			this.Padding = new System.Windows.Forms.Padding(19);
			this.Text = "SmartConditionBaseForm";
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
			this.pnlContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tabWorkResultPerSegment)).EndInit();
			this.tabWorkResultPerSegment.ResumeLayout(false);
			this.perPNLPage.ResumeLayout(false);
			this.perMMPage.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdWorkResultPerPNL;
        private Framework.SmartControls.SmartTabControl tabWorkResultPerSegment;
        private DevExpress.XtraTab.XtraTabPage perPNLPage;
        private DevExpress.XtraTab.XtraTabPage perMMPage;
        private Framework.SmartControls.SmartBandedGrid grdWorkResultPerMM;
    }
}