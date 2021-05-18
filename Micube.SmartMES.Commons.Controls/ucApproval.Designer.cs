namespace Micube.SmartMES.Commons.Controls
{
    partial class ucApproval
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnADDAPPROVAL = new Micube.Framework.SmartControls.SmartButton();
            this.grdApproval = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.flowLayoutPanel3, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdApproval, 0, 1);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 2;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(862, 248);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.flowLayoutPanel4);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(862, 33);
            this.flowLayoutPanel3.TabIndex = 8;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.AutoSize = true;
            this.flowLayoutPanel4.Controls.Add(this.btnADDAPPROVAL);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(759, 4);
            this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(103, 26);
            this.flowLayoutPanel4.TabIndex = 5;
            // 
            // btnADDAPPROVAL
            // 
            this.btnADDAPPROVAL.AllowFocus = false;
            this.btnADDAPPROVAL.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.btnADDAPPROVAL.Appearance.Options.UseBackColor = true;
            this.btnADDAPPROVAL.IsBusy = false;
            this.btnADDAPPROVAL.IsWrite = false;
            this.btnADDAPPROVAL.LanguageKey = "ADDAPPROVAL";
            this.btnADDAPPROVAL.Location = new System.Drawing.Point(3, 3);
            this.btnADDAPPROVAL.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.btnADDAPPROVAL.Name = "btnADDAPPROVAL";
            this.btnADDAPPROVAL.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnADDAPPROVAL.Size = new System.Drawing.Size(100, 23);
            this.btnADDAPPROVAL.TabIndex = 0;
            this.btnADDAPPROVAL.TabStop = false;
            this.btnADDAPPROVAL.Text = "결재자추가";
            this.btnADDAPPROVAL.TooltipLanguageKey = "";
            // 
            // grdApproval
            // 
            this.grdApproval.Caption = "결재정보";
            this.grdApproval.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdApproval.IsUsePaging = false;
            this.grdApproval.LanguageKey = "APPROVALINFO";
            this.grdApproval.Location = new System.Drawing.Point(0, 33);
            this.grdApproval.Margin = new System.Windows.Forms.Padding(0);
            this.grdApproval.Name = "grdApproval";
            this.grdApproval.ShowBorder = true;
            this.grdApproval.ShowStatusBar = false;
            this.grdApproval.Size = new System.Drawing.Size(862, 215);
            this.grdApproval.TabIndex = 1;
            this.grdApproval.TabStop = false;
            // 
            // ucApproval
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.Name = "ucApproval";
            this.Size = new System.Drawing.Size(862, 248);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        public Framework.SmartControls.SmartBandedGrid grdApproval;
        public Framework.SmartControls.SmartButton btnADDAPPROVAL;
    }
}
