namespace Micube.SmartMES.QualityAnalysis
{
    partial class FileProgressDialogPopup
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
            this.tlpFileProgressDialog = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.pnlFileProgressWait = new Micube.Framework.SmartControls.SmartProgressPanel();
            this.barFileProgress = new Micube.Framework.SmartControls.SmartProgressBarControl();
            this.grdFileList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.pnlStartButton = new Micube.Framework.SmartControls.SmartPanel();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.btnStart = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.tlpFileProgressDialog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barFileProgress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlStartButton)).BeginInit();
            this.pnlStartButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tlpFileProgressDialog);
            this.pnlMain.Size = new System.Drawing.Size(580, 652);
            // 
            // tlpFileProgressDialog
            // 
            this.tlpFileProgressDialog.ColumnCount = 1;
            this.tlpFileProgressDialog.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFileProgressDialog.Controls.Add(this.pnlFileProgressWait, 0, 0);
            this.tlpFileProgressDialog.Controls.Add(this.barFileProgress, 0, 1);
            this.tlpFileProgressDialog.Controls.Add(this.grdFileList, 0, 2);
            this.tlpFileProgressDialog.Controls.Add(this.pnlStartButton, 0, 3);
            this.tlpFileProgressDialog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFileProgressDialog.Location = new System.Drawing.Point(0, 0);
            this.tlpFileProgressDialog.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tlpFileProgressDialog.Name = "tlpFileProgressDialog";
            this.tlpFileProgressDialog.RowCount = 4;
            this.tlpFileProgressDialog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpFileProgressDialog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpFileProgressDialog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFileProgressDialog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpFileProgressDialog.Size = new System.Drawing.Size(580, 652);
            this.tlpFileProgressDialog.TabIndex = 0;
            // 
            // pnlFileProgressWait
            // 
            this.pnlFileProgressWait.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pnlFileProgressWait.Appearance.Options.UseBackColor = true;
            this.pnlFileProgressWait.BarAnimationElementThickness = 2;
            this.pnlFileProgressWait.ContentAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.pnlFileProgressWait.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFileProgressWait.Location = new System.Drawing.Point(0, 0);
            this.pnlFileProgressWait.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.pnlFileProgressWait.Name = "pnlFileProgressWait";
            this.pnlFileProgressWait.ShowCaption = false;
            this.pnlFileProgressWait.ShowDescription = false;
            this.pnlFileProgressWait.Size = new System.Drawing.Size(580, 40);
            this.pnlFileProgressWait.TabIndex = 0;
            this.pnlFileProgressWait.WaitAnimationType = DevExpress.Utils.Animation.WaitingAnimatorType.Ring;
            // 
            // barFileProgress
            // 
            this.barFileProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.barFileProgress.Location = new System.Drawing.Point(0, 50);
            this.barFileProgress.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.barFileProgress.Name = "barFileProgress";
            this.barFileProgress.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.barFileProgress.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.barFileProgress.Properties.ShowTitle = true;
            this.barFileProgress.ShowProgressInTaskBar = true;
            this.barFileProgress.Size = new System.Drawing.Size(580, 20);
            this.barFileProgress.TabIndex = 1;
            // 
            // grdFileList
            // 
            this.grdFileList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdFileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFileList.IsUsePaging = false;
            this.grdFileList.LanguageKey = null;
            this.grdFileList.Location = new System.Drawing.Point(0, 80);
            this.grdFileList.Margin = new System.Windows.Forms.Padding(0);
            this.grdFileList.Name = "grdFileList";
            this.grdFileList.ShowBorder = true;
            this.grdFileList.Size = new System.Drawing.Size(580, 537);
            this.grdFileList.TabIndex = 2;
            // 
            // pnlStartButton
            // 
            this.pnlStartButton.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlStartButton.Controls.Add(this.btnClose);
            this.pnlStartButton.Controls.Add(this.btnStart);
            this.pnlStartButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStartButton.Location = new System.Drawing.Point(0, 617);
            this.pnlStartButton.Margin = new System.Windows.Forms.Padding(0);
            this.pnlStartButton.Name = "pnlStartButton";
            this.pnlStartButton.Size = new System.Drawing.Size(580, 35);
            this.pnlStartButton.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.IsBusy = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(500, 10);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(80, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "닫기";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // btnStart
            // 
            this.btnStart.AllowFocus = false;
            this.btnStart.IsBusy = false;
            this.btnStart.Location = new System.Drawing.Point(500, 10);
            this.btnStart.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnStart.Name = "btnStart";
            this.btnStart.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnStart.Size = new System.Drawing.Size(80, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "시작";
            this.btnStart.TooltipLanguageKey = "";
            // 
            // FileProgressDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 670);
            this.ControlBox = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileProgressDialog";
            this.Padding = new System.Windows.Forms.Padding(10, 10, 10, 8);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FileProgressDialog";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.tlpFileProgressDialog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barFileProgress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlStartButton)).EndInit();
            this.pnlStartButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel tlpFileProgressDialog;
        private Framework.SmartControls.SmartProgressPanel pnlFileProgressWait;
        private Framework.SmartControls.SmartProgressBarControl barFileProgress;
        private Framework.SmartControls.SmartBandedGrid grdFileList;
        private Framework.SmartControls.SmartPanel pnlStartButton;
        private Framework.SmartControls.SmartButton btnStart;
        private Framework.SmartControls.SmartButton btnClose;
    }
}