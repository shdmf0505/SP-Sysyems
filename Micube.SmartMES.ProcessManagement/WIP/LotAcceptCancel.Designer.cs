namespace Micube.SmartMES.ProcessManagement
{
    partial class LotAcceptCancel
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
            this.tlpLotAcceptCancel = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.pnlInfo = new Micube.Framework.SmartControls.SmartPanel();
            this.tlpInfo = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.txtWorker = new Micube.Framework.SmartControls.SmartLabelSelectPopupEdit();
            this.tlpComment = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.lblComment = new Micube.Framework.SmartControls.SmartLabel();
            this.txtComment = new Micube.Framework.SmartControls.SmartTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.tlpLotAcceptCancel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlInfo)).BeginInit();
            this.pnlInfo.SuspendLayout();
            this.tlpInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorker.Properties)).BeginInit();
            this.tlpComment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tlpLotAcceptCancel);
            // 
            // tlpLotAcceptCancel
            // 
            this.tlpLotAcceptCancel.ColumnCount = 1;
            this.tlpLotAcceptCancel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLotAcceptCancel.Controls.Add(this.grdLotList, 0, 0);
            this.tlpLotAcceptCancel.Controls.Add(this.pnlInfo, 0, 2);
            this.tlpLotAcceptCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLotAcceptCancel.Location = new System.Drawing.Point(0, 0);
            this.tlpLotAcceptCancel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tlpLotAcceptCancel.Name = "tlpLotAcceptCancel";
            this.tlpLotAcceptCancel.RowCount = 3;
            this.tlpLotAcceptCancel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLotAcceptCancel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpLotAcceptCancel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpLotAcceptCancel.Size = new System.Drawing.Size(470, 396);
            this.tlpLotAcceptCancel.TabIndex = 0;
            // 
            // grdLotList
            // 
            this.grdLotList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLotList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLotList.IsUsePaging = false;
            this.grdLotList.LanguageKey = null;
            this.grdLotList.Location = new System.Drawing.Point(0, 0);
            this.grdLotList.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotList.Name = "grdLotList";
            this.grdLotList.ShowBorder = true;
            this.grdLotList.Size = new System.Drawing.Size(470, 286);
            this.grdLotList.TabIndex = 0;
            // 
            // pnlInfo
            // 
            this.pnlInfo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlInfo.Controls.Add(this.tlpInfo);
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInfo.Location = new System.Drawing.Point(0, 296);
            this.pnlInfo.Margin = new System.Windows.Forms.Padding(0);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(470, 100);
            this.pnlInfo.TabIndex = 1;
            // 
            // tlpInfo
            // 
            this.tlpInfo.ColumnCount = 11;
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpInfo.Controls.Add(this.txtWorker, 1, 1);
            this.tlpInfo.Controls.Add(this.tlpComment, 3, 1);
            this.tlpInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpInfo.Location = new System.Drawing.Point(0, 0);
            this.tlpInfo.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tlpInfo.Name = "tlpInfo";
            this.tlpInfo.RowCount = 5;
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpInfo.Size = new System.Drawing.Size(470, 100);
            this.tlpInfo.TabIndex = 0;
            // 
            // txtWorker
            // 
            this.txtWorker.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtWorker.Appearance.Options.UseForeColor = true;
            this.txtWorker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWorker.LabelText = "등록자";
            this.txtWorker.LanguageKey = "WRITER";
            this.txtWorker.Location = new System.Drawing.Point(10, 10);
            this.txtWorker.Margin = new System.Windows.Forms.Padding(0);
            this.txtWorker.Name = "txtWorker";
            this.txtWorker.Size = new System.Drawing.Size(82, 20);
            this.txtWorker.TabIndex = 0;
            // 
            // tlpComment
            // 
            this.tlpComment.ColumnCount = 2;
            this.tlpInfo.SetColumnSpan(this.tlpComment, 7);
            this.tlpComment.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpComment.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpComment.Controls.Add(this.lblComment, 0, 0);
            this.tlpComment.Controls.Add(this.txtComment, 1, 0);
            this.tlpComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpComment.Location = new System.Drawing.Point(102, 10);
            this.tlpComment.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tlpComment.Name = "tlpComment";
            this.tlpComment.RowCount = 1;
            this.tlpComment.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpComment.Size = new System.Drawing.Size(358, 20);
            this.tlpComment.TabIndex = 1;
            // 
            // lblComment
            // 
            this.lblComment.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblComment.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lblComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblComment.LanguageKey = "COMMENT";
            this.lblComment.Location = new System.Drawing.Point(0, 0);
            this.lblComment.Margin = new System.Windows.Forms.Padding(0);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(30, 20);
            this.lblComment.TabIndex = 0;
            this.lblComment.Text = "특이사항";
            // 
            // txtComment
            // 
            this.txtComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtComment.LabelText = null;
            this.txtComment.LanguageKey = null;
            this.txtComment.Location = new System.Drawing.Point(40, 0);
            this.txtComment.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.txtComment.Name = "txtComment";
            this.txtComment.Properties.AutoHeight = false;
            this.txtComment.Size = new System.Drawing.Size(318, 20);
            this.txtComment.TabIndex = 1;
            // 
            // LotAcceptCancel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "LotAcceptCancel";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.tlpLotAcceptCancel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlInfo)).EndInit();
            this.pnlInfo.ResumeLayout(false);
            this.tlpInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtWorker.Properties)).EndInit();
            this.tlpComment.ResumeLayout(false);
            this.tlpComment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel tlpLotAcceptCancel;
        private Framework.SmartControls.SmartBandedGrid grdLotList;
        private Framework.SmartControls.SmartPanel pnlInfo;
        private Framework.SmartControls.SmartSplitTableLayoutPanel tlpInfo;
        private Framework.SmartControls.SmartLabelSelectPopupEdit txtWorker;
        private Framework.SmartControls.SmartSplitTableLayoutPanel tlpComment;
        private Framework.SmartControls.SmartLabel lblComment;
        private Framework.SmartControls.SmartTextBox txtComment;
    }
}