namespace Micube.SmartMES.StandardInfo
{
    partial class MasterDataClass
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
            this.tabIdManagement = new Micube.Framework.SmartControls.SmartTabControl();
            this.tnpMDC = new DevExpress.XtraTab.XtraTabPage();
            this.grdMDCList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tnpAAG = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grdMDCList1 = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdAAGList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdAttribGList = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabIdManagement)).BeginInit();
            this.tabIdManagement.SuspendLayout();
            this.tnpMDC.SuspendLayout();
            this.tnpAAG.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Size = new System.Drawing.Size(371, 516);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(790, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabIdManagement);
            this.pnlContent.Size = new System.Drawing.Size(790, 519);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1171, 555);
            // 
            // tabIdManagement
            // 
            this.tabIdManagement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabIdManagement.Location = new System.Drawing.Point(0, 0);
            this.tabIdManagement.Name = "tabIdManagement";
            this.tabIdManagement.SelectedTabPage = this.tnpMDC;
            this.tabIdManagement.Size = new System.Drawing.Size(790, 519);
            this.tabIdManagement.TabIndex = 1;
            this.tabIdManagement.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tnpMDC,
            this.tnpAAG});
            // 
            // tnpMDC
            // 
            this.tnpMDC.Controls.Add(this.grdMDCList);
            this.tabIdManagement.SetLanguageKey(this.tnpMDC, "MASTERDATACLASS");
            this.tnpMDC.Name = "tnpMDC";
            this.tnpMDC.Size = new System.Drawing.Size(783, 483);
            this.tnpMDC.Text = "품목유형";
            // 
            // grdMDCList
            // 
            this.grdMDCList.Caption = "";
            this.grdMDCList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMDCList.IsUsePaging = false;
            this.grdMDCList.LanguageKey = null;
            this.grdMDCList.Location = new System.Drawing.Point(0, 0);
            this.grdMDCList.Margin = new System.Windows.Forms.Padding(0);
            this.grdMDCList.Name = "grdMDCList";
            this.grdMDCList.ShowBorder = true;
            this.grdMDCList.ShowStatusBar = false;
            this.grdMDCList.Size = new System.Drawing.Size(783, 483);
            this.grdMDCList.TabIndex = 2;
            // 
            // tnpAAG
            // 
            this.tnpAAG.Controls.Add(this.tableLayoutPanel1);
            this.tabIdManagement.SetLanguageKey(this.tnpAAG, "ATTRIBUTEGROUP");
            this.tnpAAG.Name = "tnpAAG";
            this.tnpAAG.Size = new System.Drawing.Size(783, 483);
            this.tnpAAG.Text = "속성그룹";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.96935F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.03065F));
            this.tableLayoutPanel1.Controls.Add(this.grdMDCList1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.smartSpliterContainer1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(783, 483);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // grdMDCList1
            // 
            this.grdMDCList1.Caption = "";
            this.grdMDCList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMDCList1.IsUsePaging = false;
            this.grdMDCList1.LanguageKey = null;
            this.grdMDCList1.Location = new System.Drawing.Point(0, 0);
            this.grdMDCList1.Margin = new System.Windows.Forms.Padding(0);
            this.grdMDCList1.Name = "grdMDCList1";
            this.grdMDCList1.ShowBorder = true;
            this.grdMDCList1.ShowStatusBar = false;
            this.grdMDCList1.Size = new System.Drawing.Size(219, 483);
            this.grdMDCList1.TabIndex = 2;
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(219, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdAAGList);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdAttribGList);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(564, 483);
            this.smartSpliterContainer1.SplitterPosition = 276;
            this.smartSpliterContainer1.TabIndex = 1;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdAAGList
            // 
            this.grdAAGList.Caption = "";
            this.grdAAGList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAAGList.IsUsePaging = false;
            this.grdAAGList.LanguageKey = null;
            this.grdAAGList.Location = new System.Drawing.Point(0, 0);
            this.grdAAGList.Margin = new System.Windows.Forms.Padding(0);
            this.grdAAGList.Name = "grdAAGList";
            this.grdAAGList.ShowBorder = true;
            this.grdAAGList.ShowStatusBar = false;
            this.grdAAGList.Size = new System.Drawing.Size(276, 483);
            this.grdAAGList.TabIndex = 0;
            // 
            // grdAttribGList
            // 
            this.grdAttribGList.Caption = "";
            this.grdAttribGList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAttribGList.IsUsePaging = false;
            this.grdAttribGList.LanguageKey = null;
            this.grdAttribGList.Location = new System.Drawing.Point(0, 0);
            this.grdAttribGList.Margin = new System.Windows.Forms.Padding(0);
            this.grdAttribGList.Name = "grdAttribGList";
            this.grdAttribGList.ShowBorder = true;
            this.grdAttribGList.ShowStatusBar = false;
            this.grdAttribGList.Size = new System.Drawing.Size(282, 483);
            this.grdAttribGList.TabIndex = 0;
            // 
            // MasterDataClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 593);
            this.Name = "MasterDataClass";
            this.Padding = new System.Windows.Forms.Padding(19, 19, 19, 19);
            this.Text = "MasterDataClass";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabIdManagement)).EndInit();
            this.tabIdManagement.ResumeLayout(false);
            this.tnpMDC.ResumeLayout(false);
            this.tnpAAG.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabIdManagement;
        private DevExpress.XtraTab.XtraTabPage tnpMDC;
        private DevExpress.XtraTab.XtraTabPage tnpAAG;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartBandedGrid grdMDCList1;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdAAGList;
        private Framework.SmartControls.SmartBandedGrid grdAttribGList;
        private Framework.SmartControls.SmartBandedGrid grdMDCList;
    }
}