namespace Micube.SmartMES.QualityAnalysis
{
    partial class SendMailPopup
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
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.gbxUserList = new Micube.Framework.SmartControls.SmartGroupBox();
            this.grdUserList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnDelete = new Micube.Framework.SmartControls.SmartButton();
            this.btnAdd = new Micube.Framework.SmartControls.SmartButton();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.btnSend = new Micube.Framework.SmartControls.SmartButton();
            this.txtTitle = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.tabSend = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgEmail = new DevExpress.XtraTab.XtraTabPage();
            this.memoEmail = new Micube.Framework.SmartControls.SmartMemoEdit();
            this.tpgSMS = new DevExpress.XtraTab.XtraTabPage();
            this.memoSMS = new Micube.Framework.SmartControls.SmartMemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbxUserList)).BeginInit();
            this.gbxUserList.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabSend)).BeginInit();
            this.tabSend.SuspendLayout();
            this.tpgEmail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEmail.Properties)).BeginInit();
            this.tpgSMS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoSMS.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(627, 545);
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 2;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.gbxUserList, 0, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartSpliterControl1, 0, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 1, 5);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.txtTitle, 0, 3);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.tabSend, 0, 4);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 6;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(627, 545);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // gbxUserList
            // 
            this.smartSplitTableLayoutPanel1.SetColumnSpan(this.gbxUserList, 2);
            this.gbxUserList.Controls.Add(this.grdUserList);
            this.gbxUserList.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.gbxUserList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxUserList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.gbxUserList.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.gbxUserList.LanguageKey = "RECEIVERLIST";
            this.gbxUserList.Location = new System.Drawing.Point(0, 30);
            this.gbxUserList.Margin = new System.Windows.Forms.Padding(0);
            this.gbxUserList.Name = "gbxUserList";
            this.gbxUserList.ShowBorder = true;
            this.gbxUserList.Size = new System.Drawing.Size(627, 178);
            this.gbxUserList.TabIndex = 0;
            this.gbxUserList.Text = "수신자 리스트";
            // 
            // grdUserList
            // 
            this.grdUserList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdUserList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdUserList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdUserList.IsUsePaging = false;
            this.grdUserList.LanguageKey = null;
            this.grdUserList.Location = new System.Drawing.Point(2, 31);
            this.grdUserList.Margin = new System.Windows.Forms.Padding(0);
            this.grdUserList.Name = "grdUserList";
            this.grdUserList.ShowBorder = false;
            this.grdUserList.ShowButtonBar = false;
            this.grdUserList.Size = new System.Drawing.Size(623, 145);
            this.grdUserList.TabIndex = 0;
            this.grdUserList.UseAutoBestFitColumns = false;
            // 
            // smartSpliterControl1
            // 
            this.smartSplitTableLayoutPanel1.SetColumnSpan(this.smartSpliterControl1, 2);
            this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartSpliterControl1.Location = new System.Drawing.Point(0, 208);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(627, 5);
            this.smartSpliterControl1.TabIndex = 2;
            this.smartSpliterControl1.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnDelete);
            this.flowLayoutPanel1.Controls.Add(this.btnAdd);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(313, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(314, 30);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // btnDelete
            // 
            this.btnDelete.AllowFocus = false;
            this.btnDelete.IsBusy = false;
            this.btnDelete.IsWrite = false;
            this.btnDelete.Location = new System.Drawing.Point(234, 0);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnDelete.Size = new System.Drawing.Size(80, 25);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.Text = "삭제";
            this.btnDelete.TooltipLanguageKey = "";
            // 
            // btnAdd
            // 
            this.btnAdd.AllowFocus = false;
            this.btnAdd.IsBusy = false;
            this.btnAdd.IsWrite = false;
            this.btnAdd.Location = new System.Drawing.Point(148, 0);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnAdd.Size = new System.Drawing.Size(80, 25);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "추가";
            this.btnAdd.TooltipLanguageKey = "";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btnClose);
            this.flowLayoutPanel2.Controls.Add(this.btnSend);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(313, 515);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(314, 30);
            this.flowLayoutPanel2.TabIndex = 4;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.Location = new System.Drawing.Point(234, 5);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 5, 0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(80, 25);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "닫기";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // btnSend
            // 
            this.btnSend.AllowFocus = false;
            this.btnSend.IsBusy = false;
            this.btnSend.IsWrite = false;
            this.btnSend.Location = new System.Drawing.Point(148, 5);
            this.btnSend.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.btnSend.Name = "btnSend";
            this.btnSend.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSend.Size = new System.Drawing.Size(80, 25);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "보내기";
            this.btnSend.TooltipLanguageKey = "";
            // 
            // txtTitle
            // 
            this.txtTitle.AutoHeight = false;
            this.smartSplitTableLayoutPanel1.SetColumnSpan(this.txtTitle, 2);
            this.txtTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTitle.LabelText = "제목 :";
            this.txtTitle.LabelWidth = "10%";
            this.txtTitle.LanguageKey = "TITLE";
            this.txtTitle.Location = new System.Drawing.Point(3, 221);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Properties.AutoHeight = false;
            this.txtTitle.Size = new System.Drawing.Size(621, 24);
            this.txtTitle.TabIndex = 5;
            // 
            // tabSend
            // 
            this.smartSplitTableLayoutPanel1.SetColumnSpan(this.tabSend, 2);
            this.tabSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSend.Location = new System.Drawing.Point(0, 248);
            this.tabSend.Margin = new System.Windows.Forms.Padding(0);
            this.tabSend.Name = "tabSend";
            this.tabSend.SelectedTabPage = this.tpgEmail;
            this.tabSend.Size = new System.Drawing.Size(627, 267);
            this.tabSend.TabIndex = 6;
            this.tabSend.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgEmail,
            this.tpgSMS});
            // 
            // tpgEmail
            // 
            this.tpgEmail.Controls.Add(this.memoEmail);
            this.tpgEmail.Name = "tpgEmail";
            this.tpgEmail.Size = new System.Drawing.Size(621, 238);
            this.tpgEmail.Text = "Email";
            // 
            // memoEmail
            // 
            this.memoEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoEmail.Location = new System.Drawing.Point(0, 0);
            this.memoEmail.Margin = new System.Windows.Forms.Padding(0);
            this.memoEmail.Name = "memoEmail";
            this.memoEmail.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.memoEmail.Size = new System.Drawing.Size(621, 238);
            this.memoEmail.TabIndex = 0;
            // 
            // tpgSMS
            // 
            this.tpgSMS.Controls.Add(this.memoSMS);
            this.tpgSMS.Name = "tpgSMS";
            this.tpgSMS.Size = new System.Drawing.Size(774, 169);
            this.tpgSMS.Text = "SMS";
            // 
            // memoSMS
            // 
            this.memoSMS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoSMS.Location = new System.Drawing.Point(0, 0);
            this.memoSMS.Name = "memoSMS";
            this.memoSMS.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.memoSMS.Size = new System.Drawing.Size(774, 169);
            this.memoSMS.TabIndex = 0;
            // 
            // SendMailPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 565);
            this.Name = "SendMailPopup";
            this.Text = "MailPopup";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbxUserList)).EndInit();
            this.gbxUserList.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabSend)).EndInit();
            this.tabSend.ResumeLayout(false);
            this.tpgEmail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoEmail.Properties)).EndInit();
            this.tpgSMS.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoSMS.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartGroupBox gbxUserList;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
        private Framework.SmartControls.SmartBandedGrid grdUserList;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Framework.SmartControls.SmartButton btnDelete;
        private Framework.SmartControls.SmartButton btnAdd;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartButton btnSend;
        private Framework.SmartControls.SmartLabelTextBox txtTitle;
        private Framework.SmartControls.SmartTabControl tabSend;
        private DevExpress.XtraTab.XtraTabPage tpgEmail;
        private DevExpress.XtraTab.XtraTabPage tpgSMS;
        private Framework.SmartControls.SmartMemoEdit memoEmail;
        private Framework.SmartControls.SmartMemoEdit memoSMS;
    }
}