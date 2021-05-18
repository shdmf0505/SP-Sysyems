namespace Micube.SmartMES.ProcessManagement
{
    partial class ProgressPerProduct
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
            this.toolTipWipInfo = new DevExpress.Utils.ToolTipController(this.components);
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdSummary = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tabControl = new Micube.Framework.SmartControls.SmartTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.grdWip = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.grdResult = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.tabControl.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer1);
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Horizontal = false;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdSummary);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.tabControl);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(756, 489);
            this.smartSpliterContainer1.SplitterPosition = 302;
            this.smartSpliterContainer1.TabIndex = 0;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdSummary
            // 
            this.grdSummary.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSummary.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdSummary.IsUsePaging = false;
            this.grdSummary.LanguageKey = "WIPLIST";
            this.grdSummary.Location = new System.Drawing.Point(0, 0);
            this.grdSummary.Margin = new System.Windows.Forms.Padding(0);
            this.grdSummary.Name = "grdSummary";
            this.grdSummary.ShowBorder = true;
            this.grdSummary.Size = new System.Drawing.Size(756, 302);
            this.grdSummary.TabIndex = 4;
            this.grdSummary.UseAutoBestFitColumns = false;
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedTabPage = this.xtraTabPage1;
            this.tabControl.Size = new System.Drawing.Size(756, 182);
            this.tabControl.TabIndex = 4;
            this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.grdWip);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(750, 153);
            this.xtraTabPage1.Text = "재공 현황";
            // 
            // grdWip
            // 
            this.grdWip.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdWip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdWip.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdWip.IsUsePaging = false;
            this.grdWip.LanguageKey = "WIPLIST";
            this.grdWip.Location = new System.Drawing.Point(0, 0);
            this.grdWip.Margin = new System.Windows.Forms.Padding(0);
            this.grdWip.Name = "grdWip";
            this.grdWip.ShowBorder = true;
            this.grdWip.ShowStatusBar = false;
            this.grdWip.Size = new System.Drawing.Size(750, 153);
            this.grdWip.TabIndex = 2;
            this.grdWip.UseAutoBestFitColumns = false;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.grdResult);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(750, 153);
            this.xtraTabPage2.Text = "실적 현황";
            // 
            // grdResult
            // 
            this.grdResult.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdResult.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdResult.IsUsePaging = false;
            this.grdResult.LanguageKey = "WORKRESULT";
            this.grdResult.Location = new System.Drawing.Point(0, 0);
            this.grdResult.Margin = new System.Windows.Forms.Padding(0);
            this.grdResult.Name = "grdResult";
            this.grdResult.ShowBorder = true;
            this.grdResult.ShowStatusBar = false;
            this.grdResult.Size = new System.Drawing.Size(750, 153);
            this.grdResult.TabIndex = 2;
            this.grdResult.UseAutoBestFitColumns = false;
            // 
            // ProgressPerProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 538);
            this.Name = "ProgressPerProduct";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.Utils.ToolTipController toolTipWipInfo;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdSummary;
        private Framework.SmartControls.SmartTabControl tabControl;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private Framework.SmartControls.SmartBandedGrid grdWip;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private Framework.SmartControls.SmartBandedGrid grdResult;
    }
}
