namespace Micube.SmartMES.QualityAnalysis
{
    partial class MeasureFormManagement
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grdFileList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.pnlFileUploadButton = new Micube.Framework.SmartControls.SmartPanel();
            this.btnFileDownload = new Micube.Framework.SmartControls.SmartButton();
            this.btnFileDelete = new Micube.Framework.SmartControls.SmartButton();
            this.btnFileAdd = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFileUploadButton)).BeginInit();
            this.pnlFileUploadButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 397);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tableLayoutPanel1);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.grdFileList, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnlFileUploadButton, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(475, 401);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // grdFileList
            // 
            this.grdFileList.Caption = "대책서 양식 리스트";
            this.grdFileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFileList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdFileList.IsUsePaging = false;
            this.grdFileList.LanguageKey = "MEASUREFORMLIST";
            this.grdFileList.Location = new System.Drawing.Point(0, 33);
            this.grdFileList.Margin = new System.Windows.Forms.Padding(0);
            this.grdFileList.Name = "grdFileList";
            this.grdFileList.ShowBorder = true;
            this.grdFileList.Size = new System.Drawing.Size(475, 368);
            this.grdFileList.TabIndex = 2;
            // 
            // pnlFileUploadButton
            // 
            this.pnlFileUploadButton.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlFileUploadButton.Controls.Add(this.btnFileDownload);
            this.pnlFileUploadButton.Controls.Add(this.btnFileDelete);
            this.pnlFileUploadButton.Controls.Add(this.btnFileAdd);
            this.pnlFileUploadButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFileUploadButton.Location = new System.Drawing.Point(0, 0);
            this.pnlFileUploadButton.Margin = new System.Windows.Forms.Padding(0);
            this.pnlFileUploadButton.Name = "pnlFileUploadButton";
            this.pnlFileUploadButton.Size = new System.Drawing.Size(475, 30);
            this.pnlFileUploadButton.TabIndex = 1;
            // 
            // btnFileDownload
            // 
            this.btnFileDownload.AllowFocus = false;
            this.btnFileDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFileDownload.IsBusy = false;
            this.btnFileDownload.IsWrite = false;
            this.btnFileDownload.LanguageKey = "FILEDOWNLOAD";
            this.btnFileDownload.Location = new System.Drawing.Point(400, 0);
            this.btnFileDownload.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnFileDownload.Name = "btnFileDownload";
            this.btnFileDownload.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnFileDownload.Size = new System.Drawing.Size(75, 25);
            this.btnFileDownload.TabIndex = 2;
            this.btnFileDownload.Text = "다운로드";
            this.btnFileDownload.TooltipLanguageKey = "";
            // 
            // btnFileDelete
            // 
            this.btnFileDelete.AllowFocus = false;
            this.btnFileDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFileDelete.IsBusy = false;
            this.btnFileDelete.IsWrite = false;
            this.btnFileDelete.LanguageKey = "FILEDELETE";
            this.btnFileDelete.Location = new System.Drawing.Point(315, 0);
            this.btnFileDelete.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnFileDelete.Name = "btnFileDelete";
            this.btnFileDelete.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnFileDelete.Size = new System.Drawing.Size(75, 25);
            this.btnFileDelete.TabIndex = 1;
            this.btnFileDelete.Text = "파일삭제";
            this.btnFileDelete.TooltipLanguageKey = "";
            // 
            // btnFileAdd
            // 
            this.btnFileAdd.AllowFocus = false;
            this.btnFileAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFileAdd.IsBusy = false;
            this.btnFileAdd.IsWrite = false;
            this.btnFileAdd.LanguageKey = "FILEADD";
            this.btnFileAdd.Location = new System.Drawing.Point(230, 0);
            this.btnFileAdd.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnFileAdd.Name = "btnFileAdd";
            this.btnFileAdd.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnFileAdd.Size = new System.Drawing.Size(75, 25);
            this.btnFileAdd.TabIndex = 0;
            this.btnFileAdd.Text = "파일추가";
            this.btnFileAdd.TooltipLanguageKey = "";
            // 
            // MeasureFormManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "MeasureFormManagement";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlFileUploadButton)).EndInit();
            this.pnlFileUploadButton.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartPanel pnlFileUploadButton;
        public Framework.SmartControls.SmartButton btnFileDownload;
        public Framework.SmartControls.SmartButton btnFileDelete;
        public Framework.SmartControls.SmartButton btnFileAdd;
        public Framework.SmartControls.SmartBandedGrid grdFileList;
    }
}