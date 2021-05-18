namespace Micube.SmartMES.OutsideOrderMgnt
{
    partial class OutsourcingEtcWorkStatusBrowse
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
            this.grdWorkStatus = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 485);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(601, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdWorkStatus);
            this.pnlContent.Size = new System.Drawing.Size(601, 489);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(906, 518);
            // 
            // grdWorkStatus
            // 
            this.grdWorkStatus.Caption = "OSPEtcWorkStatusInformation";
            this.grdWorkStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdWorkStatus.IsUsePaging = false;
            this.grdWorkStatus.LanguageKey = "OSPEtcWorkStatusInformation";
            this.grdWorkStatus.Location = new System.Drawing.Point(0, 0);
            this.grdWorkStatus.Margin = new System.Windows.Forms.Padding(0);
            this.grdWorkStatus.Name = "grdWorkStatus";
            this.grdWorkStatus.ShowBorder = true;
            this.grdWorkStatus.ShowStatusBar = false;
            this.grdWorkStatus.Size = new System.Drawing.Size(601, 489);
            this.grdWorkStatus.TabIndex = 117;
            // 
            // OutsourcingEtcWorkStatusBrowse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 556);
            this.Name = "OutsourcingEtcWorkStatusBrowse";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdWorkStatus;
    }
}