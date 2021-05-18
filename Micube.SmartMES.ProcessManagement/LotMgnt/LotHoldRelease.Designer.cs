namespace Micube.SmartMES.ProcessManagement
{
    partial class LotHoldRelease
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
            this.grdHold = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdRelease = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.smartGroupBox1 = new Micube.Framework.SmartControls.SmartGroupBox();
            this.txtComment = new DevExpress.XtraEditors.MemoEdit();
            this.lblComment = new Micube.Framework.SmartControls.SmartLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ucDataUpDownBtnCtrl = new Micube.SmartMES.Commons.Controls.ucDataUpDownBtn();
            this.spcSpliter = new Micube.Framework.SmartControls.SmartSpliterControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).BeginInit();
            this.smartGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 485);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(756, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdHold);
            this.pnlContent.Controls.Add(this.spcSpliter);
            this.pnlContent.Controls.Add(this.panel1);
            this.pnlContent.Size = new System.Drawing.Size(756, 489);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1061, 518);
            // 
            // grdHold
            // 
            this.grdHold.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdHold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdHold.IsUsePaging = false;
            this.grdHold.LanguageKey = "HOLDLIST";
            this.grdHold.Location = new System.Drawing.Point(0, 0);
            this.grdHold.Margin = new System.Windows.Forms.Padding(0);
            this.grdHold.Name = "grdHold";
            this.grdHold.ShowBorder = true;
            this.grdHold.Size = new System.Drawing.Size(756, 156);
            this.grdHold.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdRelease);
            this.panel1.Controls.Add(this.smartSpliterControl1);
            this.panel1.Controls.Add(this.smartGroupBox1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 161);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(756, 328);
            this.panel1.TabIndex = 4;
            // 
            // grdRelease
            // 
            this.grdRelease.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdRelease.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRelease.IsUsePaging = false;
            this.grdRelease.LanguageKey = "RELEASEHOLD";
            this.grdRelease.Location = new System.Drawing.Point(0, 45);
            this.grdRelease.Margin = new System.Windows.Forms.Padding(0);
            this.grdRelease.Name = "grdRelease";
            this.grdRelease.ShowBorder = true;
            this.grdRelease.Size = new System.Drawing.Size(434, 283);
            this.grdRelease.TabIndex = 2;
            // 
            // smartSpliterControl1
            // 
            this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.smartSpliterControl1.Location = new System.Drawing.Point(434, 45);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(5, 283);
            this.smartSpliterControl1.TabIndex = 6;
            this.smartSpliterControl1.TabStop = false;
            // 
            // smartGroupBox1
            // 
            this.smartGroupBox1.Controls.Add(this.txtComment);
            this.smartGroupBox1.Controls.Add(this.lblComment);
            this.smartGroupBox1.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.smartGroupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.smartGroupBox1.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.smartGroupBox1.LanguageKey = "RELEASEREASON";
            this.smartGroupBox1.Location = new System.Drawing.Point(439, 45);
            this.smartGroupBox1.Name = "smartGroupBox1";
            this.smartGroupBox1.ShowBorder = true;
            this.smartGroupBox1.Size = new System.Drawing.Size(317, 283);
            this.smartGroupBox1.TabIndex = 0;
            this.smartGroupBox1.Text = "해제 사유";
            // 
            // txtComment
            // 
            this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComment.Location = new System.Drawing.Point(5, 54);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(306, 223);
            this.txtComment.TabIndex = 5;
            // 
            // lblComment
            // 
            this.lblComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblComment.LanguageKey = "SPECIALNOTE";
            this.lblComment.Location = new System.Drawing.Point(5, 34);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(40, 14);
            this.lblComment.TabIndex = 3;
            this.lblComment.Text = "특이사항";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ucDataUpDownBtnCtrl);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(756, 45);
            this.panel2.TabIndex = 0;
            // 
            // ucDataUpDownBtnCtrl
            // 
            this.ucDataUpDownBtnCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDataUpDownBtnCtrl.Location = new System.Drawing.Point(0, 0);
            this.ucDataUpDownBtnCtrl.Name = "ucDataUpDownBtnCtrl";
            this.ucDataUpDownBtnCtrl.Size = new System.Drawing.Size(756, 45);
            this.ucDataUpDownBtnCtrl.SourceGrid = null;
            this.ucDataUpDownBtnCtrl.TabIndex = 1;
            this.ucDataUpDownBtnCtrl.TargetGrid = null;
            // 
            // spcSpliter
            // 
            this.spcSpliter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.spcSpliter.Location = new System.Drawing.Point(0, 156);
            this.spcSpliter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcSpliter.Name = "spcSpliter";
            this.spcSpliter.Size = new System.Drawing.Size(756, 5);
            this.spcSpliter.TabIndex = 5;
            this.spcSpliter.TabStop = false;
            // 
            // LotHoldRelease
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 538);
            this.Name = "LotHoldRelease";
            this.Text = "Lot Hold Release";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).EndInit();
            this.smartGroupBox1.ResumeLayout(false);
            this.smartGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdHold;
        private System.Windows.Forms.Panel panel1;
        private Framework.SmartControls.SmartSpliterControl spcSpliter;
        private System.Windows.Forms.Panel panel2;
        private Framework.SmartControls.SmartBandedGrid grdRelease;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
        private Framework.SmartControls.SmartGroupBox smartGroupBox1;
        private Framework.SmartControls.SmartLabel lblComment;
        private DevExpress.XtraEditors.MemoEdit txtComment;
        private Micube.SmartMES.Commons.Controls.ucDataUpDownBtn ucDataUpDownBtnCtrl;
    }
}