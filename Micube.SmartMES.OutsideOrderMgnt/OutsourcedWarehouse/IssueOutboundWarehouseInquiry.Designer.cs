namespace Micube.SmartMES.OutsideOrderMgnt
{
    partial class IssueOutboundWarehouseInquiry
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
            this.grdIssueOutboundWarehouseInquiry = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.btnOutputslip = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Size = new System.Drawing.Size(371, 908);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnOutputslip);
            this.pnlToolbar.Size = new System.Drawing.Size(845, 30);
            this.pnlToolbar.Controls.SetChildIndex(this.btnOutputslip, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdIssueOutboundWarehouseInquiry);
            this.pnlContent.Size = new System.Drawing.Size(845, 911);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1226, 947);
            // 
            // grdIssueOutboundWarehouseInquiry
            // 
            this.grdIssueOutboundWarehouseInquiry.Caption = "외주창고 출고 목록";
            this.grdIssueOutboundWarehouseInquiry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdIssueOutboundWarehouseInquiry.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdIssueOutboundWarehouseInquiry.IsUsePaging = false;
            this.grdIssueOutboundWarehouseInquiry.LanguageKey = "ISSUEOUTBOUNDWAREHOUSEINQUIRYLIST";
            this.grdIssueOutboundWarehouseInquiry.Location = new System.Drawing.Point(0, 0);
            this.grdIssueOutboundWarehouseInquiry.Margin = new System.Windows.Forms.Padding(0);
            this.grdIssueOutboundWarehouseInquiry.Name = "grdIssueOutboundWarehouseInquiry";
            this.grdIssueOutboundWarehouseInquiry.ShowBorder = true;
            this.grdIssueOutboundWarehouseInquiry.Size = new System.Drawing.Size(845, 911);
            this.grdIssueOutboundWarehouseInquiry.TabIndex = 8;
            // 
            // btnOutputslip
            // 
            this.btnOutputslip.AllowFocus = false;
            this.btnOutputslip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOutputslip.IsBusy = false;
            this.btnOutputslip.IsWrite = true;
            this.btnOutputslip.LanguageKey = "OUTPUTSLIP";
            this.btnOutputslip.Location = new System.Drawing.Point(718, 3);
            this.btnOutputslip.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnOutputslip.Name = "btnOutputslip";
            this.btnOutputslip.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnOutputslip.Size = new System.Drawing.Size(105, 23);
            this.btnOutputslip.TabIndex = 5;
            this.btnOutputslip.Text = "출고전표";
            this.btnOutputslip.TooltipLanguageKey = "";
            this.btnOutputslip.Visible = false;
            this.btnOutputslip.Click += new System.EventHandler(this.btnOutputslip_Click_1);
            // 
            // IssueOutboundWarehouseInquiry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 985);
            this.Name = "IssueOutboundWarehouseInquiry";
            this.Padding = new System.Windows.Forms.Padding(19, 19, 19, 19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdIssueOutboundWarehouseInquiry;
        private Framework.SmartControls.SmartButton btnOutputslip;
    }
}