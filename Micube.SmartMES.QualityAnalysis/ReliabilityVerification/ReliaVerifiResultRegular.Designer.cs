namespace Micube.SmartMES.QualityAnalysis
{
    partial class ReliaVerifiResultRegular
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
            this.tabReliaVerifiResultRegular = new Micube.Framework.SmartControls.SmartTabControl();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.grdReliaVerifiResultRegular = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.grdReReliaVerifiResultRegular = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPopupFlag = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabReliaVerifiResultRegular)).BeginInit();
            this.tabReliaVerifiResultRegular.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.xtraTabPage3.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 397);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.flowLayoutPanel2);
            this.pnlToolbar.Controls.SetChildIndex(this.flowLayoutPanel2, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabReliaVerifiResultRegular);
            // 
            // tabReliaVerifiResultRegular
            // 
            this.tabReliaVerifiResultRegular.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabReliaVerifiResultRegular.Location = new System.Drawing.Point(0, 0);
            this.tabReliaVerifiResultRegular.Name = "tabReliaVerifiResultRegular";
            this.tabReliaVerifiResultRegular.SelectedTabPage = this.xtraTabPage2;
            this.tabReliaVerifiResultRegular.Size = new System.Drawing.Size(475, 401);
            this.tabReliaVerifiResultRegular.TabIndex = 0;
            this.tabReliaVerifiResultRegular.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage2,
            this.xtraTabPage3});
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.tableLayoutPanel2);
            this.tabReliaVerifiResultRegular.SetLanguageKey(this.xtraTabPage2, "REQUEST");
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(469, 372);
            this.xtraTabPage2.Text = "의뢰";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.grdReliaVerifiResultRegular, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 372F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(469, 372);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // grdReliaVerifiResultRegular
            // 
            this.grdReliaVerifiResultRegular.Caption = "신뢰성 검증 의뢰 결과 현황";
            this.grdReliaVerifiResultRegular.CausesValidation = false;
            this.tableLayoutPanel2.SetColumnSpan(this.grdReliaVerifiResultRegular, 2);
            this.grdReliaVerifiResultRegular.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReliaVerifiResultRegular.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdReliaVerifiResultRegular.IsUsePaging = false;
            this.grdReliaVerifiResultRegular.LanguageKey = "RELIABVERIFIRERERESSTATUS";
            this.grdReliaVerifiResultRegular.Location = new System.Drawing.Point(0, 0);
            this.grdReliaVerifiResultRegular.Margin = new System.Windows.Forms.Padding(0);
            this.grdReliaVerifiResultRegular.Name = "grdReliaVerifiResultRegular";
            this.grdReliaVerifiResultRegular.ShowBorder = true;
            this.grdReliaVerifiResultRegular.ShowStatusBar = false;
            this.grdReliaVerifiResultRegular.Size = new System.Drawing.Size(469, 372);
            this.grdReliaVerifiResultRegular.TabIndex = 2;
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.tableLayoutPanel3);
            this.tabReliaVerifiResultRegular.SetLanguageKey(this.xtraTabPage3, "REQUESTSECONDOPINION");
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(469, 372);
            this.xtraTabPage3.Text = "재의뢰";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.grdReReliaVerifiResultRegular, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 372F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(469, 372);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // grdReReliaVerifiResultRegular
            // 
            this.grdReReliaVerifiResultRegular.Caption = "신뢰성 검증 재의뢰 결과 현황";
            this.grdReReliaVerifiResultRegular.CausesValidation = false;
            this.tableLayoutPanel3.SetColumnSpan(this.grdReReliaVerifiResultRegular, 2);
            this.grdReReliaVerifiResultRegular.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReReliaVerifiResultRegular.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdReReliaVerifiResultRegular.IsUsePaging = false;
            this.grdReReliaVerifiResultRegular.LanguageKey = "RERELIABVERIFIRERERESSTATUS";
            this.grdReReliaVerifiResultRegular.Location = new System.Drawing.Point(0, 0);
            this.grdReReliaVerifiResultRegular.Margin = new System.Windows.Forms.Padding(0);
            this.grdReReliaVerifiResultRegular.Name = "grdReReliaVerifiResultRegular";
            this.grdReReliaVerifiResultRegular.ShowBorder = true;
            this.grdReReliaVerifiResultRegular.ShowStatusBar = false;
            this.grdReReliaVerifiResultRegular.Size = new System.Drawing.Size(469, 372);
            this.grdReReliaVerifiResultRegular.TabIndex = 1;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btnPopupFlag);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(47, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(428, 24);
            this.flowLayoutPanel2.TabIndex = 5;
            // 
            // btnPopupFlag
            // 
            this.btnPopupFlag.AllowFocus = false;
            this.btnPopupFlag.IsBusy = false;
            this.btnPopupFlag.IsWrite = true;
            this.btnPopupFlag.Location = new System.Drawing.Point(345, 0);
            this.btnPopupFlag.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPopupFlag.Name = "btnPopupFlag";
            this.btnPopupFlag.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPopupFlag.Size = new System.Drawing.Size(80, 25);
            this.btnPopupFlag.TabIndex = 0;
            this.btnPopupFlag.Text = "smartButton1";
            this.btnPopupFlag.TooltipLanguageKey = "";
            this.btnPopupFlag.Visible = false;
            // 
            // ReliaVerifiResultRegular
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "ReliaVerifiResultRegular";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabReliaVerifiResultRegular)).EndInit();
            this.tabReliaVerifiResultRegular.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.xtraTabPage3.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabReliaVerifiResultRegular;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Framework.SmartControls.SmartBandedGrid grdReliaVerifiResultRegular;
        private Framework.SmartControls.SmartBandedGrid grdReReliaVerifiResultRegular;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Framework.SmartControls.SmartButton btnPopupFlag;
    }
}