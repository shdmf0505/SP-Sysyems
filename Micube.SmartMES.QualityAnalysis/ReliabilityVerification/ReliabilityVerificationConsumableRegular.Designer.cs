namespace Micube.SmartMES.QualityAnalysis
{
    partial class ReliabilityVerificationConsumableRegular
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
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRecieve1 = new Micube.Framework.SmartControls.SmartButton();
            this.btnNew = new Micube.Framework.SmartControls.SmartButton();
            this.btnFlag = new Micube.Framework.SmartControls.SmartButton();
            this.grdReliabiVerifiReqRgistRegular = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.flowLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 397);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnFlag);
            this.pnlToolbar.Controls.Add(this.flowLayoutPanel3);
            this.pnlToolbar.Controls.SetChildIndex(this.flowLayoutPanel3, 0);
            this.pnlToolbar.Controls.SetChildIndex(this.btnFlag, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdReliabiVerifiReqRgistRegular);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.btnRecieve1);
            this.flowLayoutPanel3.Controls.Add(this.btnNew);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(475, 24);
            this.flowLayoutPanel3.TabIndex = 3;
            this.flowLayoutPanel3.Visible = false;
            // 
            // btnRecieve1
            // 
            this.btnRecieve1.AllowFocus = false;
            this.btnRecieve1.IsBusy = false;
            this.btnRecieve1.IsWrite = false;
            this.btnRecieve1.LanguageKey = "SAVE";
            this.btnRecieve1.Location = new System.Drawing.Point(392, 0);
            this.btnRecieve1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnRecieve1.Name = "btnRecieve1";
            this.btnRecieve1.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnRecieve1.Size = new System.Drawing.Size(80, 25);
            this.btnRecieve1.TabIndex = 6;
            this.btnRecieve1.Text = "저장";
            this.btnRecieve1.TooltipLanguageKey = "";
            // 
            // btnNew
            // 
            this.btnNew.AllowFocus = false;
            this.btnNew.IsBusy = false;
            this.btnNew.IsWrite = false;
            this.btnNew.LanguageKey = "REGISTRATION";
            this.btnNew.Location = new System.Drawing.Point(306, 0);
            this.btnNew.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnNew.Name = "btnNew";
            this.btnNew.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnNew.Size = new System.Drawing.Size(80, 25);
            this.btnNew.TabIndex = 7;
            this.btnNew.Text = "등록";
            this.btnNew.TooltipLanguageKey = "";
            // 
            // btnFlag
            // 
            this.btnFlag.AllowFocus = false;
            this.btnFlag.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnFlag.IsBusy = false;
            this.btnFlag.IsWrite = true;
            this.btnFlag.Location = new System.Drawing.Point(0, 0);
            this.btnFlag.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnFlag.Name = "btnFlag";
            this.btnFlag.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnFlag.Size = new System.Drawing.Size(65, 24);
            this.btnFlag.TabIndex = 11;
            this.btnFlag.Text = "Flag";
            this.btnFlag.TooltipLanguageKey = "";
            this.btnFlag.Visible = false;
            // 
            // grdReliabiVerifiReqRgistRegular
            // 
            this.grdReliabiVerifiReqRgistRegular.Caption = "신뢰성검증(자재) 접수현황";
            this.grdReliabiVerifiReqRgistRegular.CausesValidation = false;
            this.grdReliabiVerifiReqRgistRegular.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReliabiVerifiReqRgistRegular.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdReliabiVerifiReqRgistRegular.IsUsePaging = false;
            this.grdReliabiVerifiReqRgistRegular.LanguageKey = "RELIABVERIFIREREQCONSUMABLESTATUS";
            this.grdReliabiVerifiReqRgistRegular.Location = new System.Drawing.Point(0, 0);
            this.grdReliabiVerifiReqRgistRegular.Margin = new System.Windows.Forms.Padding(0);
            this.grdReliabiVerifiReqRgistRegular.Name = "grdReliabiVerifiReqRgistRegular";
            this.grdReliabiVerifiReqRgistRegular.ShowBorder = true;
            this.grdReliabiVerifiReqRgistRegular.ShowStatusBar = false;
            this.grdReliabiVerifiReqRgistRegular.Size = new System.Drawing.Size(475, 401);
            this.grdReliabiVerifiReqRgistRegular.TabIndex = 2;
            // 
            // ReliabilityVerificationConsumableRegular
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "ReliabilityVerificationConsumableRegular";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private Framework.SmartControls.SmartButton btnRecieve1;
        private Framework.SmartControls.SmartButton btnNew;
        public Framework.SmartControls.SmartButton btnFlag;
        private Framework.SmartControls.SmartBandedGrid grdReliabiVerifiReqRgistRegular;
    }
}