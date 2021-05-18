namespace Micube.SmartMES.OutsideOrderMgnt
{
    partial class OutsourcingAmountStatusBrowse
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
            this.smartSpliterContainer2 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdOSAmount = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdOSAmountDetail = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).BeginInit();
            this.smartSpliterContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 556);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(860, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer2);
            this.pnlContent.Size = new System.Drawing.Size(860, 560);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1165, 589);
            // 
            // smartSpliterContainer2
            // 
            this.smartSpliterContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer2.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.smartSpliterContainer2.Horizontal = false;
            this.smartSpliterContainer2.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer2.Name = "smartSpliterContainer2";
            this.smartSpliterContainer2.Panel1.Controls.Add(this.grdOSAmount);
            this.smartSpliterContainer2.Panel1.Text = "Panel1";
            this.smartSpliterContainer2.Panel2.Controls.Add(this.grdOSAmountDetail);
            this.smartSpliterContainer2.Panel2.Text = "Panel2";
            this.smartSpliterContainer2.Size = new System.Drawing.Size(860, 560);
            this.smartSpliterContainer2.SplitterPosition = 244;
            this.smartSpliterContainer2.TabIndex = 4;
            this.smartSpliterContainer2.Text = "smartSpliterContainer2";
            // 
            // grdOSAmount
            // 
            this.grdOSAmount.Caption = "OSPAmountStatusInformation";
            this.grdOSAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOSAmount.IsUsePaging = false;
            this.grdOSAmount.LanguageKey = "OSPAmountStatusInformation";
            this.grdOSAmount.Location = new System.Drawing.Point(0, 0);
            this.grdOSAmount.Margin = new System.Windows.Forms.Padding(0);
            this.grdOSAmount.Name = "grdOSAmount";
            this.grdOSAmount.ShowBorder = true;
            this.grdOSAmount.ShowStatusBar = false;
            this.grdOSAmount.Size = new System.Drawing.Size(860, 311);
            this.grdOSAmount.TabIndex = 115;
            // 
            // grdOSAmountDetail
            // 
            this.grdOSAmountDetail.Caption = "OSPAmountDetailInformation";
            this.grdOSAmountDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOSAmountDetail.IsUsePaging = false;
            this.grdOSAmountDetail.LanguageKey = "OSPAmountDetailInformation";
            this.grdOSAmountDetail.Location = new System.Drawing.Point(0, 0);
            this.grdOSAmountDetail.Margin = new System.Windows.Forms.Padding(0);
            this.grdOSAmountDetail.Name = "grdOSAmountDetail";
            this.grdOSAmountDetail.ShowBorder = true;
            this.grdOSAmountDetail.ShowStatusBar = false;
            this.grdOSAmountDetail.Size = new System.Drawing.Size(860, 244);
            this.grdOSAmountDetail.TabIndex = 115;
            // 
            // OutsourcingAmountStatusBrowse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 627);
            this.Name = "OutsourcingAmountStatusBrowse";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).EndInit();
            this.smartSpliterContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer2;
        private Framework.SmartControls.SmartBandedGrid grdOSAmount;
        private Framework.SmartControls.SmartBandedGrid grdOSAmountDetail;
    }
}