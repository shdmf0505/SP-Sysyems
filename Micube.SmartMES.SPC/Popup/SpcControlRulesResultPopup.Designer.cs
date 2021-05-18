namespace Micube.SmartMES.SPC
{
    partial class SpcControlRulesResultPopup
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
            this.pnlAction = new Micube.Framework.SmartControls.SmartPanel();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.spltMain = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.pnlGrid = new Micube.Framework.SmartControls.SmartPanel();
            this.grdRuleResult = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.pnlInfo = new Micube.Framework.SmartControls.SmartPanel();
            this.lblCheckOverCountValue = new Micube.Framework.SmartControls.SmartLabel();
            this.lblTotalPointCountValue = new Micube.Framework.SmartControls.SmartLabel();
            this.lblXBarValue = new Micube.Framework.SmartControls.SmartLabel();
            this.lblVerificationCountValue = new Micube.Framework.SmartControls.SmartLabel();
            this.lblCheckOverCount = new Micube.Framework.SmartControls.SmartLabel();
            this.lblTotalPointCount = new Micube.Framework.SmartControls.SmartLabel();
            this.lblXBar = new Micube.Framework.SmartControls.SmartLabel();
            this.grdRuleInfo = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.lblVerificationResultsValue = new Micube.Framework.SmartControls.SmartLabel();
            this.lblVerificationCount = new Micube.Framework.SmartControls.SmartLabel();
            this.lblVerificationResults = new Micube.Framework.SmartControls.SmartLabel();
            this.grdRuleDetail = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlAction)).BeginInit();
            this.pnlAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltMain)).BeginInit();
            this.spltMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGrid)).BeginInit();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlInfo)).BeginInit();
            this.pnlInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartPanel1);
            this.pnlMain.Controls.Add(this.pnlAction);
            this.pnlMain.Location = new System.Drawing.Point(3, 3);
            this.pnlMain.Size = new System.Drawing.Size(1096, 664);
            // 
            // pnlAction
            // 
            this.pnlAction.Controls.Add(this.btnClose);
            this.pnlAction.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAction.Location = new System.Drawing.Point(0, 627);
            this.pnlAction.Name = "pnlAction";
            this.pnlAction.Size = new System.Drawing.Size(1096, 37);
            this.pnlAction.TabIndex = 7;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(969, 4);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 6, 0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(114, 28);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "닫기";
            this.btnClose.TooltipLanguageKey = "";
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // smartPanel1
            // 
            this.smartPanel1.Controls.Add(this.spltMain);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(1096, 627);
            this.smartPanel1.TabIndex = 8;
            // 
            // spltMain
            // 
            this.spltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltMain.Horizontal = false;
            this.spltMain.Location = new System.Drawing.Point(2, 2);
            this.spltMain.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spltMain.Name = "spltMain";
            this.spltMain.Panel1.Controls.Add(this.pnlGrid);
            this.spltMain.Panel1.Controls.Add(this.pnlInfo);
            this.spltMain.Panel1.Text = "Panel1";
            this.spltMain.Panel2.Controls.Add(this.grdRuleDetail);
            this.spltMain.Panel2.Text = "Panel2";
            this.spltMain.Size = new System.Drawing.Size(1092, 623);
            this.spltMain.SplitterPosition = 400;
            this.spltMain.TabIndex = 0;
            this.spltMain.Text = "smartSpliterContainer1";
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.grdRuleResult);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 140);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(1092, 260);
            this.pnlGrid.TabIndex = 1;
            // 
            // grdRuleResult
            // 
            this.grdRuleResult.Caption = "RuleOverData";
            this.grdRuleResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRuleResult.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)(((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete)));
            this.grdRuleResult.IsUsePaging = false;
            this.grdRuleResult.LanguageKey = "SPCRULESOVERLIST";
            this.grdRuleResult.Location = new System.Drawing.Point(2, 2);
            this.grdRuleResult.Margin = new System.Windows.Forms.Padding(0);
            this.grdRuleResult.Name = "grdRuleResult";
            this.grdRuleResult.ShowBorder = false;
            this.grdRuleResult.ShowStatusBar = false;
            this.grdRuleResult.Size = new System.Drawing.Size(1088, 256);
            this.grdRuleResult.TabIndex = 4;
            // 
            // pnlInfo
            // 
            this.pnlInfo.Controls.Add(this.lblCheckOverCountValue);
            this.pnlInfo.Controls.Add(this.lblTotalPointCountValue);
            this.pnlInfo.Controls.Add(this.lblXBarValue);
            this.pnlInfo.Controls.Add(this.lblVerificationCountValue);
            this.pnlInfo.Controls.Add(this.lblCheckOverCount);
            this.pnlInfo.Controls.Add(this.lblTotalPointCount);
            this.pnlInfo.Controls.Add(this.lblXBar);
            this.pnlInfo.Controls.Add(this.grdRuleInfo);
            this.pnlInfo.Controls.Add(this.lblVerificationResultsValue);
            this.pnlInfo.Controls.Add(this.lblVerificationCount);
            this.pnlInfo.Controls.Add(this.lblVerificationResults);
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(1092, 140);
            this.pnlInfo.TabIndex = 0;
            // 
            // lblCheckOverCountValue
            // 
            this.lblCheckOverCountValue.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckOverCountValue.Appearance.Options.UseFont = true;
            this.lblCheckOverCountValue.Location = new System.Drawing.Point(477, 41);
            this.lblCheckOverCountValue.Name = "lblCheckOverCountValue";
            this.lblCheckOverCountValue.Size = new System.Drawing.Size(13, 25);
            this.lblCheckOverCountValue.TabIndex = 11;
            this.lblCheckOverCountValue.Text = "3";
            // 
            // lblTotalPointCountValue
            // 
            this.lblTotalPointCountValue.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPointCountValue.Appearance.Options.UseFont = true;
            this.lblTotalPointCountValue.Location = new System.Drawing.Point(157, 101);
            this.lblTotalPointCountValue.Name = "lblTotalPointCountValue";
            this.lblTotalPointCountValue.Size = new System.Drawing.Size(13, 25);
            this.lblTotalPointCountValue.TabIndex = 9;
            this.lblTotalPointCountValue.Text = "3";
            // 
            // lblXBarValue
            // 
            this.lblXBarValue.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblXBarValue.Appearance.Options.UseFont = true;
            this.lblXBarValue.Location = new System.Drawing.Point(157, 70);
            this.lblXBarValue.Name = "lblXBarValue";
            this.lblXBarValue.Size = new System.Drawing.Size(13, 25);
            this.lblXBarValue.TabIndex = 8;
            this.lblXBarValue.Text = "3";
            // 
            // lblVerificationCountValue
            // 
            this.lblVerificationCountValue.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVerificationCountValue.Appearance.Options.UseFont = true;
            this.lblVerificationCountValue.Location = new System.Drawing.Point(157, 39);
            this.lblVerificationCountValue.Name = "lblVerificationCountValue";
            this.lblVerificationCountValue.Size = new System.Drawing.Size(13, 25);
            this.lblVerificationCountValue.TabIndex = 3;
            this.lblVerificationCountValue.Text = "3";
            // 
            // lblCheckOverCount
            // 
            this.lblCheckOverCount.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckOverCount.Appearance.Options.UseFont = true;
            this.lblCheckOverCount.LanguageKey = "SPCRULESCHECKOVERCOUNT";
            this.lblCheckOverCount.Location = new System.Drawing.Point(319, 41);
            this.lblCheckOverCount.Name = "lblCheckOverCount";
            this.lblCheckOverCount.Size = new System.Drawing.Size(126, 23);
            this.lblCheckOverCount.TabIndex = 10;
            this.lblCheckOverCount.Text = "전체 Over 개수:";
            // 
            // lblTotalPointCount
            // 
            this.lblTotalPointCount.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPointCount.Appearance.Options.UseFont = true;
            this.lblTotalPointCount.LanguageKey = "SPCRULESTOTALPOINTCOUNT";
            this.lblTotalPointCount.Location = new System.Drawing.Point(9, 100);
            this.lblTotalPointCount.Name = "lblTotalPointCount";
            this.lblTotalPointCount.Size = new System.Drawing.Size(111, 23);
            this.lblTotalPointCount.TabIndex = 7;
            this.lblTotalPointCount.Text = "전체 Point 수:";
            // 
            // lblXBar
            // 
            this.lblXBar.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblXBar.Appearance.Options.UseFont = true;
            this.lblXBar.LanguageKey = "SPCRULESCENTERVALUE";
            this.lblXBar.Location = new System.Drawing.Point(10, 70);
            this.lblXBar.Name = "lblXBar";
            this.lblXBar.Size = new System.Drawing.Size(76, 23);
            this.lblXBar.TabIndex = 6;
            this.lblXBar.Text = "중 앙 값 :";
            // 
            // grdRuleInfo
            // 
            this.grdRuleInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdRuleInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.grdRuleInfo.Caption = "RuleInfo";
            this.grdRuleInfo.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)(((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete)));
            this.grdRuleInfo.IsUsePaging = false;
            this.grdRuleInfo.LanguageKey = "SPCRULESINFO";
            this.grdRuleInfo.Location = new System.Drawing.Point(571, 2);
            this.grdRuleInfo.Margin = new System.Windows.Forms.Padding(0);
            this.grdRuleInfo.Name = "grdRuleInfo";
            this.grdRuleInfo.ShowBorder = false;
            this.grdRuleInfo.ShowStatusBar = false;
            this.grdRuleInfo.Size = new System.Drawing.Size(519, 135);
            this.grdRuleInfo.TabIndex = 5;
            // 
            // lblVerificationResultsValue
            // 
            this.lblVerificationResultsValue.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVerificationResultsValue.Appearance.Options.UseFont = true;
            this.lblVerificationResultsValue.Location = new System.Drawing.Point(157, 8);
            this.lblVerificationResultsValue.Name = "lblVerificationResultsValue";
            this.lblVerificationResultsValue.Size = new System.Drawing.Size(296, 25);
            this.lblVerificationResultsValue.TabIndex = 0;
            this.lblVerificationResultsValue.Text = "있음 - Rules Over 자료가 있습니다.";
            // 
            // lblVerificationCount
            // 
            this.lblVerificationCount.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVerificationCount.Appearance.Options.UseFont = true;
            this.lblVerificationCount.Location = new System.Drawing.Point(10, 40);
            this.lblVerificationCount.Name = "lblVerificationCount";
            this.lblVerificationCount.Size = new System.Drawing.Size(75, 23);
            this.lblVerificationCount.TabIndex = 2;
            this.lblVerificationCount.Text = "검증개수:";
            // 
            // lblVerificationResults
            // 
            this.lblVerificationResults.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVerificationResults.Appearance.Options.UseFont = true;
            this.lblVerificationResults.LanguageKey = "SPCRULESVERIFICATIONRESULTS";
            this.lblVerificationResults.Location = new System.Drawing.Point(10, 10);
            this.lblVerificationResults.Name = "lblVerificationResults";
            this.lblVerificationResults.Size = new System.Drawing.Size(75, 23);
            this.lblVerificationResults.TabIndex = 1;
            this.lblVerificationResults.Text = "검증결과:";
            // 
            // grdRuleDetail
            // 
            this.grdRuleDetail.Caption = "Detail";
            this.grdRuleDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRuleDetail.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)(((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete)));
            this.grdRuleDetail.IsUsePaging = false;
            this.grdRuleDetail.LanguageKey = "SPCRULESOVERDATALIST";
            this.grdRuleDetail.Location = new System.Drawing.Point(0, 0);
            this.grdRuleDetail.Margin = new System.Windows.Forms.Padding(0);
            this.grdRuleDetail.Name = "grdRuleDetail";
            this.grdRuleDetail.ShowBorder = false;
            this.grdRuleDetail.ShowStatusBar = false;
            this.grdRuleDetail.Size = new System.Drawing.Size(1092, 218);
            this.grdRuleDetail.TabIndex = 5;
            // 
            // SpcControlRulesResultPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 670);
            this.LanguageKey = "";
            this.Name = "SpcControlRulesResultPopup";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Raw Data";
            this.Load += new System.EventHandler(this.SpcControlRulesResultPopup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlAction)).EndInit();
            this.pnlAction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spltMain)).EndInit();
            this.spltMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlGrid)).EndInit();
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlInfo)).EndInit();
            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartPanel pnlAction;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartSpliterContainer spltMain;
        private Framework.SmartControls.SmartPanel pnlGrid;
        private Framework.SmartControls.SmartPanel pnlInfo;
        private Framework.SmartControls.SmartBandedGrid grdRuleResult;
        private Framework.SmartControls.SmartBandedGrid grdRuleDetail;
        private Framework.SmartControls.SmartLabel lblVerificationResults;
        private Framework.SmartControls.SmartLabel lblVerificationResultsValue;
        private Framework.SmartControls.SmartBandedGrid grdRuleInfo;
        private Framework.SmartControls.SmartLabel lblVerificationCountValue;
        private Framework.SmartControls.SmartLabel lblVerificationCount;
        private Framework.SmartControls.SmartLabel lblTotalPointCountValue;
        private Framework.SmartControls.SmartLabel lblXBarValue;
        private Framework.SmartControls.SmartLabel lblTotalPointCount;
        private Framework.SmartControls.SmartLabel lblXBar;
        private Framework.SmartControls.SmartLabel lblCheckOverCountValue;
        private Framework.SmartControls.SmartLabel lblCheckOverCount;
    }
}