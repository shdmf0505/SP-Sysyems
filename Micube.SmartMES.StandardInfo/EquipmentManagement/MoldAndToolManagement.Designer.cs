namespace Micube.SmartMES.StandardInfo
{
    partial class MoldAndToolManagement
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
            this.grdMoldAndTool = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tabMoldAndToolManager = new Micube.Framework.SmartControls.SmartTabControl();
            this.pageMold = new DevExpress.XtraTab.XtraTabPage();
            this.grdMold = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.pageBBT = new DevExpress.XtraTab.XtraTabPage();
            this.grdBBT = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.pageFilm = new DevExpress.XtraTab.XtraTabPage();
            this.grdFilm = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMoldAndToolManager)).BeginInit();
            this.tabMoldAndToolManager.SuspendLayout();
            this.pageMold.SuspendLayout();
            this.pageBBT.SuspendLayout();
            this.pageFilm.SuspendLayout();
            this.xtraTabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 516);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(835, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabMoldAndToolManager);
            this.pnlContent.Size = new System.Drawing.Size(835, 520);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1140, 549);
            // 
            // grdMoldAndTool
            // 
            this.grdMoldAndTool.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdMoldAndTool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMoldAndTool.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdMoldAndTool.IsUsePaging = false;
            this.grdMoldAndTool.LanguageKey = "MOLDANDTOOLLIST";
            this.grdMoldAndTool.Location = new System.Drawing.Point(0, 0);
            this.grdMoldAndTool.Margin = new System.Windows.Forms.Padding(0);
            this.grdMoldAndTool.Name = "grdMoldAndTool";
            this.grdMoldAndTool.ShowBorder = true;
            this.grdMoldAndTool.ShowStatusBar = false;
            this.grdMoldAndTool.Size = new System.Drawing.Size(829, 491);
            this.grdMoldAndTool.TabIndex = 0;
            this.grdMoldAndTool.UseAutoBestFitColumns = false;
            // 
            // tabMoldAndToolManager
            // 
            this.tabMoldAndToolManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMoldAndToolManager.Location = new System.Drawing.Point(0, 0);
            this.tabMoldAndToolManager.Name = "tabMoldAndToolManager";
            this.tabMoldAndToolManager.SelectedTabPage = this.pageMold;
            this.tabMoldAndToolManager.Size = new System.Drawing.Size(835, 520);
            this.tabMoldAndToolManager.TabIndex = 1;
            this.tabMoldAndToolManager.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.pageMold,
            this.pageBBT,
            this.pageFilm,
            this.xtraTabPage4});
            // 
            // pageMold
            // 
            this.pageMold.AccessibleName = "MOLD";
            this.pageMold.Controls.Add(this.grdMold);
            this.tabMoldAndToolManager.SetLanguageKey(this.pageMold, "MOLDANDWOODEN");
            this.pageMold.Name = "pageMold";
            this.pageMold.Size = new System.Drawing.Size(829, 491);
            this.pageMold.Text = "xtraTabPage1";
            // 
            // grdMold
            // 
            this.grdMold.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdMold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMold.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdMold.IsUsePaging = false;
            this.grdMold.LanguageKey = "MOLDANDWOODEN";
            this.grdMold.Location = new System.Drawing.Point(0, 0);
            this.grdMold.Margin = new System.Windows.Forms.Padding(0);
            this.grdMold.Name = "grdMold";
            this.grdMold.ShowBorder = true;
            this.grdMold.ShowStatusBar = false;
            this.grdMold.Size = new System.Drawing.Size(829, 491);
            this.grdMold.TabIndex = 0;
            this.grdMold.UseAutoBestFitColumns = false;
            // 
            // pageBBT
            // 
            this.pageBBT.AccessibleName = "BBT";
            this.pageBBT.Controls.Add(this.grdBBT);
            this.tabMoldAndToolManager.SetLanguageKey(this.pageBBT, "BBTANDJIG");
            this.pageBBT.Name = "pageBBT";
            this.pageBBT.Size = new System.Drawing.Size(750, 460);
            this.pageBBT.Text = "xtraTabPage2";
            // 
            // grdBBT
            // 
            this.grdBBT.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdBBT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdBBT.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdBBT.IsUsePaging = false;
            this.grdBBT.LanguageKey = "BBTANDJIG";
            this.grdBBT.Location = new System.Drawing.Point(0, 0);
            this.grdBBT.Margin = new System.Windows.Forms.Padding(0);
            this.grdBBT.Name = "grdBBT";
            this.grdBBT.ShowBorder = true;
            this.grdBBT.ShowStatusBar = false;
            this.grdBBT.Size = new System.Drawing.Size(750, 460);
            this.grdBBT.TabIndex = 1;
            this.grdBBT.UseAutoBestFitColumns = false;
            // 
            // pageFilm
            // 
            this.pageFilm.AccessibleName = "FILM";
            this.pageFilm.Controls.Add(this.grdFilm);
            this.tabMoldAndToolManager.SetLanguageKey(this.pageFilm, "FILM");
            this.pageFilm.Name = "pageFilm";
            this.pageFilm.Size = new System.Drawing.Size(829, 491);
            this.pageFilm.Text = "xtraTabPage3";
            // 
            // grdFilm
            // 
            this.grdFilm.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdFilm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFilm.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdFilm.IsUsePaging = false;
            this.grdFilm.LanguageKey = "FILM";
            this.grdFilm.Location = new System.Drawing.Point(0, 0);
            this.grdFilm.Margin = new System.Windows.Forms.Padding(0);
            this.grdFilm.Name = "grdFilm";
            this.grdFilm.ShowBorder = true;
            this.grdFilm.ShowStatusBar = false;
            this.grdFilm.Size = new System.Drawing.Size(829, 491);
            this.grdFilm.TabIndex = 1;
            this.grdFilm.UseAutoBestFitColumns = false;
            // 
            // xtraTabPage4
            // 
            this.xtraTabPage4.Controls.Add(this.grdMoldAndTool);
            this.xtraTabPage4.Name = "xtraTabPage4";
            this.xtraTabPage4.PageVisible = false;
            this.xtraTabPage4.Size = new System.Drawing.Size(829, 491);
            this.xtraTabPage4.Text = "xtraTabPage4";
            // 
            // MoldAndToolManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 569);
            this.Name = "MoldAndToolManagement";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMoldAndToolManager)).EndInit();
            this.tabMoldAndToolManager.ResumeLayout(false);
            this.pageMold.ResumeLayout(false);
            this.pageBBT.ResumeLayout(false);
            this.pageFilm.ResumeLayout(false);
            this.xtraTabPage4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdMoldAndTool;
        private Framework.SmartControls.SmartTabControl tabMoldAndToolManager;
        private DevExpress.XtraTab.XtraTabPage pageMold;
        private Framework.SmartControls.SmartBandedGrid grdMold;
        private DevExpress.XtraTab.XtraTabPage pageBBT;
        private Framework.SmartControls.SmartBandedGrid grdBBT;
        private DevExpress.XtraTab.XtraTabPage pageFilm;
        private Framework.SmartControls.SmartBandedGrid grdFilm;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage4;
    }
}