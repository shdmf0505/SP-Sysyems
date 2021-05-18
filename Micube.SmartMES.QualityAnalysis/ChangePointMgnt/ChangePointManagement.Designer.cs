namespace Micube.SmartMES.QualityAnalysis
{
    partial class ChangePointManagement
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
            this.grdChangePointStatus = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRegistration = new Micube.Framework.SmartControls.SmartButton();
            this.btnPrint = new Micube.Framework.SmartControls.SmartButton();
            this.btnFlag = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
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
            this.pnlContent.Controls.Add(this.grdChangePointStatus);
            // 
            // grdChangePointStatus
            // 
            this.grdChangePointStatus.Caption = "변경점 이력현황";
            this.grdChangePointStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdChangePointStatus.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdChangePointStatus.IsUsePaging = false;
            this.grdChangePointStatus.LanguageKey = "CHANGEPOINTSTATUS";
            this.grdChangePointStatus.Location = new System.Drawing.Point(0, 0);
            this.grdChangePointStatus.Margin = new System.Windows.Forms.Padding(0);
            this.grdChangePointStatus.Name = "grdChangePointStatus";
            this.grdChangePointStatus.ShowBorder = true;
            this.grdChangePointStatus.Size = new System.Drawing.Size(475, 401);
            this.grdChangePointStatus.TabIndex = 0;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btnRegistration);
            this.flowLayoutPanel2.Controls.Add(this.btnPrint);
            this.flowLayoutPanel2.Controls.Add(this.btnFlag);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(47, 0);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(428, 24);
            this.flowLayoutPanel2.TabIndex = 5;
            // 
            // btnRegistration
            // 
            this.btnRegistration.AllowFocus = false;
            this.btnRegistration.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRegistration.IsBusy = false;
            this.btnRegistration.IsWrite = false;
            this.btnRegistration.LanguageKey = "REGISTRATION";
            this.btnRegistration.Location = new System.Drawing.Point(345, 0);
            this.btnRegistration.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnRegistration.Name = "btnRegistration";
            this.btnRegistration.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnRegistration.Size = new System.Drawing.Size(80, 24);
            this.btnRegistration.TabIndex = 0;
            this.btnRegistration.Text = "등록";
            this.btnRegistration.TooltipLanguageKey = "";
            this.btnRegistration.Visible = false;
            // 
            // btnPrint
            // 
            this.btnPrint.AllowFocus = false;
            this.btnPrint.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPrint.IsBusy = false;
            this.btnPrint.IsWrite = false;
            this.btnPrint.LanguageKey = "PRINT";
            this.btnPrint.Location = new System.Drawing.Point(259, 0);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPrint.Size = new System.Drawing.Size(80, 24);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "신청서 출력";
            this.btnPrint.TooltipLanguageKey = "";
            this.btnPrint.Visible = false;
            // 
            // btnFlag
            // 
            this.btnFlag.AllowFocus = false;
            this.btnFlag.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnFlag.IsBusy = false;
            this.btnFlag.IsWrite = true;
            this.btnFlag.Location = new System.Drawing.Point(173, 0);
            this.btnFlag.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnFlag.Name = "btnFlag";
            this.btnFlag.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnFlag.Size = new System.Drawing.Size(80, 24);
            this.btnFlag.TabIndex = 2;
            this.btnFlag.Text = "Flag";
            this.btnFlag.TooltipLanguageKey = "";
            this.btnFlag.Visible = false;
            // 
            // ChangePointManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "ChangePointManagement";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdChangePointStatus;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Framework.SmartControls.SmartButton btnRegistration;
        private Framework.SmartControls.SmartButton btnPrint;
        private Framework.SmartControls.SmartButton btnFlag;
    }
}