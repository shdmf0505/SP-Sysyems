namespace Micube.SmartMES.QualityAnalysis
{
    partial class ReliabilityVerificationAbnormalOccurrenceRegular
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
            this.grdReliabiVerifiAbnormalOccurenceRegular = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.btnPopupFlag = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 397);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnPopupFlag);
            this.pnlToolbar.Controls.SetChildIndex(this.btnPopupFlag, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdReliabiVerifiAbnormalOccurenceRegular);
            // 
            // grdReliabiVerifiAbnormalOccurenceRegular
            // 
            this.grdReliabiVerifiAbnormalOccurenceRegular.Caption = "신뢰성 검증 이상 발생 현황";
            this.grdReliabiVerifiAbnormalOccurenceRegular.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReliabiVerifiAbnormalOccurenceRegular.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdReliabiVerifiAbnormalOccurenceRegular.IsUsePaging = false;
            this.grdReliabiVerifiAbnormalOccurenceRegular.LanguageKey = "RELIABILITYVERIFICATIONABNORMALOCCUREGULARSTATUS";
            this.grdReliabiVerifiAbnormalOccurenceRegular.Location = new System.Drawing.Point(0, 0);
            this.grdReliabiVerifiAbnormalOccurenceRegular.Margin = new System.Windows.Forms.Padding(0);
            this.grdReliabiVerifiAbnormalOccurenceRegular.Name = "grdReliabiVerifiAbnormalOccurenceRegular";
            this.grdReliabiVerifiAbnormalOccurenceRegular.ShowBorder = true;
            this.grdReliabiVerifiAbnormalOccurenceRegular.ShowStatusBar = false;
            this.grdReliabiVerifiAbnormalOccurenceRegular.Size = new System.Drawing.Size(475, 401);
            this.grdReliabiVerifiAbnormalOccurenceRegular.TabIndex = 7;
            this.grdReliabiVerifiAbnormalOccurenceRegular.UseAutoBestFitColumns = false;
            // 
            // btnPopupFlag
            // 
            this.btnPopupFlag.AllowFocus = false;
            this.btnPopupFlag.IsBusy = false;
            this.btnPopupFlag.IsWrite = true;
            this.btnPopupFlag.Location = new System.Drawing.Point(400, -4);
            this.btnPopupFlag.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPopupFlag.Name = "btnPopupFlag";
            this.btnPopupFlag.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPopupFlag.Size = new System.Drawing.Size(80, 25);
            this.btnPopupFlag.TabIndex = 5;
            this.btnPopupFlag.Text = "smartButton1";
            this.btnPopupFlag.TooltipLanguageKey = "";
            this.btnPopupFlag.Visible = false;
            // 
            // ReliabilityVerificationAbnormalOccurrenceRegular
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.LanguageKey = "RELIABILITYVERIFICATIONABNORMALOCCURRENCEREGULAR";
            this.Name = "ReliabilityVerificationAbnormalOccurrenceRegular";
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

        private Framework.SmartControls.SmartBandedGrid grdReliabiVerifiAbnormalOccurenceRegular;
        private Framework.SmartControls.SmartButton btnPopupFlag;
    }
}