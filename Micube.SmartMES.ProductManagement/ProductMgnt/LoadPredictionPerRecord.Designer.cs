namespace Micube.SmartMES.ProductManagement
{
    partial class LoadPredictionPerRecord
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
            this.tabControl = new Micube.Framework.SmartControls.SmartTabControl();
            this.pagMain = new DevExpress.XtraTab.XtraTabPage();
            this.pagDetail = new DevExpress.XtraTab.XtraTabPage();
            this.grdLoadDetail = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdLoadTotal = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.tabControl.SuspendLayout();
            this.pagMain.SuspendLayout();
            this.pagDetail.SuspendLayout();
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
            this.pnlContent.Controls.Add(this.tabControl);
            this.pnlContent.Size = new System.Drawing.Size(457, 383);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(762, 412);
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedTabPage = this.pagMain;
            this.tabControl.Size = new System.Drawing.Size(457, 383);
            this.tabControl.TabIndex = 1;
            this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.pagMain,
            this.pagDetail});
            // 
            // pagMain
            // 
            this.pagMain.Controls.Add(this.grdLoadTotal);
            this.tabControl.SetLanguageKey(this.pagMain, "MAIN");
            this.pagMain.Name = "pagMain";
            this.pagMain.Size = new System.Drawing.Size(451, 354);
            this.pagMain.Text = "xtraTabPage1";
            // 
            // pagDetail
            // 
            this.pagDetail.Controls.Add(this.grdLoadDetail);
            this.tabControl.SetLanguageKey(this.pagDetail, "DETAIL");
            this.pagDetail.Name = "pagDetail";
            this.pagDetail.Size = new System.Drawing.Size(451, 354);
            this.pagDetail.Text = "xtraTabPage2";
            // 
            // grdLoadDetail
            // 
            this.grdLoadDetail.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLoadDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLoadDetail.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLoadDetail.IsUsePaging = false;
            this.grdLoadDetail.LanguageKey = "LOADRESULTVIEW";
            this.grdLoadDetail.Location = new System.Drawing.Point(0, 0);
            this.grdLoadDetail.Margin = new System.Windows.Forms.Padding(0);
            this.grdLoadDetail.Name = "grdLoadDetail";
            this.grdLoadDetail.ShowBorder = true;
            this.grdLoadDetail.ShowStatusBar = false;
            this.grdLoadDetail.Size = new System.Drawing.Size(451, 354);
            this.grdLoadDetail.TabIndex = 2;
            // 
            // grdLoadTotal
            // 
            this.grdLoadTotal.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLoadTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLoadTotal.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLoadTotal.IsUsePaging = false;
            this.grdLoadTotal.LanguageKey = "LOADRESULTVIEW";
            this.grdLoadTotal.Location = new System.Drawing.Point(0, 0);
            this.grdLoadTotal.Margin = new System.Windows.Forms.Padding(0);
            this.grdLoadTotal.Name = "grdLoadTotal";
            this.grdLoadTotal.ShowBorder = true;
            this.grdLoadTotal.Size = new System.Drawing.Size(451, 354);
            this.grdLoadTotal.TabIndex = 3;
            // 
            // LoadPredictionPerRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "LoadPredictionPerRecord";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.pagMain.ResumeLayout(false);
            this.pagDetail.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabControl;
        private DevExpress.XtraTab.XtraTabPage pagMain;
        private Framework.SmartControls.SmartBandedGrid grdLoadTotal;
        private DevExpress.XtraTab.XtraTabPage pagDetail;
        private Framework.SmartControls.SmartBandedGrid grdLoadDetail;
    }
}