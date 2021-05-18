namespace Micube.SmartMES.QualityAnalysis
{
    partial class AuditManagementledger
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
            this.grdAudit = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.btnFlag = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(363, 190, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 397);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnFlag);
            this.pnlToolbar.Controls.SetChildIndex(this.btnFlag, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdAudit);
            // 
            // grdAudit
            // 
            this.grdAudit.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdAudit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAudit.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdAudit.IsUsePaging = false;
            this.grdAudit.LanguageKey = "AUDITMANAGEREGIST";
            this.grdAudit.Location = new System.Drawing.Point(0, 0);
            this.grdAudit.Margin = new System.Windows.Forms.Padding(0);
            this.grdAudit.Name = "grdAudit";
            this.grdAudit.ShowBorder = true;
            this.grdAudit.ShowStatusBar = false;
            this.grdAudit.Size = new System.Drawing.Size(475, 401);
            this.grdAudit.TabIndex = 1;
            // 
            // btnFlag
            // 
            this.btnFlag.AllowFocus = false;
            this.btnFlag.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnFlag.IsBusy = false;
            this.btnFlag.IsWrite = true;
            this.btnFlag.Location = new System.Drawing.Point(47, 0);
            this.btnFlag.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnFlag.Name = "btnFlag";
            this.btnFlag.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnFlag.Size = new System.Drawing.Size(428, 24);
            this.btnFlag.TabIndex = 5;
            this.btnFlag.Text = "Flag";
            this.btnFlag.TooltipLanguageKey = "";
            this.btnFlag.Visible = false;
            // 
            // AuditManagementledger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "AuditManagementledger";
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

        private Framework.SmartControls.SmartBandedGrid grdAudit;
        private Framework.SmartControls.SmartButton btnFlag;
    }
}