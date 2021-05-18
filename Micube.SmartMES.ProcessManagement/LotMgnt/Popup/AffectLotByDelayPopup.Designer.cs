namespace Micube.SmartMES.ProcessManagement
{
	partial class AffectLotByDelayPopup
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
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
            this.cboDelayCode = new Micube.Framework.SmartControls.SmartComboBox();
            this.txtLotId = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblLotId = new Micube.Framework.SmartControls.SmartLabel();
            this.lblDelayCode = new Micube.Framework.SmartControls.SmartLabel();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.lblComment = new Micube.Framework.SmartControls.SmartLabel();
            this.txtComment = new Micube.Framework.SmartControls.SmartMemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboDelayCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).BeginInit();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(470, 188);
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(390, 168);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(75, 20);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "저장";
            this.btnSave.TooltipLanguageKey = "";
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.IsBusy = false;
            this.btnCancel.IsWrite = false;
            this.btnCancel.LanguageKey = "CANCEL";
            this.btnCancel.Location = new System.Drawing.Point(290, 168);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancel.Size = new System.Drawing.Size(75, 20);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "취소";
            this.btnCancel.TooltipLanguageKey = "";
            // 
            // cboDelayCode
            // 
            this.smartSplitTableLayoutPanel1.SetColumnSpan(this.cboDelayCode, 3);
            this.cboDelayCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboDelayCode.LabelText = null;
            this.cboDelayCode.LanguageKey = null;
            this.cboDelayCode.Location = new System.Drawing.Point(100, 25);
            this.cboDelayCode.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.cboDelayCode.Name = "cboDelayCode";
            this.cboDelayCode.PopupWidth = 0;
            this.cboDelayCode.Properties.AutoHeight = false;
            this.cboDelayCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDelayCode.Properties.NullText = "";
            this.cboDelayCode.ShowHeader = true;
            this.cboDelayCode.Size = new System.Drawing.Size(355, 20);
            this.cboDelayCode.TabIndex = 7;
            this.cboDelayCode.VisibleColumns = null;
            this.cboDelayCode.VisibleColumnsWidth = null;
            // 
            // txtLotId
            // 
            this.smartSplitTableLayoutPanel1.SetColumnSpan(this.txtLotId, 3);
            this.txtLotId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLotId.LabelText = null;
            this.txtLotId.LanguageKey = null;
            this.txtLotId.Location = new System.Drawing.Point(100, 0);
            this.txtLotId.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.txtLotId.Name = "txtLotId";
            this.txtLotId.Properties.AutoHeight = false;
            this.txtLotId.Properties.ReadOnly = true;
            this.txtLotId.Size = new System.Drawing.Size(355, 20);
            this.txtLotId.TabIndex = 1;
            // 
            // lblLotId
            // 
            this.lblLotId.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblLotId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLotId.LanguageKey = "LOTID";
            this.lblLotId.Location = new System.Drawing.Point(0, 0);
            this.lblLotId.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lblLotId.Name = "lblLotId";
            this.lblLotId.Size = new System.Drawing.Size(90, 20);
            this.lblLotId.TabIndex = 0;
            this.lblLotId.Text = "LOT ID";
            // 
            // lblDelayCode
            // 
            this.lblDelayCode.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDelayCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDelayCode.LanguageKey = "DELAYREASON";
            this.lblDelayCode.Location = new System.Drawing.Point(0, 25);
            this.lblDelayCode.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lblDelayCode.Name = "lblDelayCode";
            this.lblDelayCode.Size = new System.Drawing.Size(90, 20);
            this.lblDelayCode.TabIndex = 6;
            this.lblDelayCode.Text = "체공사유";
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 5;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.lblDelayCode, 0, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.lblLotId, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.txtLotId, 1, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.cboDelayCode, 1, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.lblComment, 0, 4);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.txtComment, 0, 6);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.btnCancel, 2, 9);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.btnSave, 3, 9);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 10;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(470, 188);
            this.smartSplitTableLayoutPanel1.TabIndex = 1;
            // 
            // lblComment
            // 
            this.lblComment.LanguageKey = "COMMENT";
            this.lblComment.Location = new System.Drawing.Point(3, 53);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(40, 14);
            this.lblComment.TabIndex = 11;
            this.lblComment.Text = "특이사항";
            // 
            // txtComment
            // 
            this.smartSplitTableLayoutPanel1.SetColumnSpan(this.txtComment, 4);
            this.txtComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtComment.Location = new System.Drawing.Point(3, 78);
            this.txtComment.Name = "txtComment";
            this.smartSplitTableLayoutPanel1.SetRowSpan(this.txtComment, 2);
            this.txtComment.Size = new System.Drawing.Size(459, 77);
            this.txtComment.TabIndex = 12;
            // 
            // AffectLotByDelayPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 208);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.LanguageKey = "INPUTWAITINGCODE";
            this.Name = "AffectLotByDelayPopup";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboDelayCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).EndInit();
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
		private Framework.SmartControls.SmartLabel lblDelayCode;
		private Framework.SmartControls.SmartLabel lblLotId;
		private Framework.SmartControls.SmartTextBox txtLotId;
		private Framework.SmartControls.SmartComboBox cboDelayCode;
		private Framework.SmartControls.SmartButton btnCancel;
		private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartLabel lblComment;
        private Framework.SmartControls.SmartMemoEdit txtComment;
    }
}
