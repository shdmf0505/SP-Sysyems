namespace Micube.SmartMES.ProcessManagement
{
	partial class StayReasonCode
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
            this.lblDelayCode = new Micube.Framework.SmartControls.SmartLabel();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.lblComment = new Micube.Framework.SmartControls.SmartLabel();
            this.txtComment = new Micube.Framework.SmartControls.SmartMemoEdit();
            this.grdStaying = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboDelayCode.Properties)).BeginInit();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).BeginInit();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel2);
            this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(612, 329);
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(532, 171);
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
            this.btnCancel.Location = new System.Drawing.Point(432, 171);
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
            this.cboDelayCode.Location = new System.Drawing.Point(100, 5);
            this.cboDelayCode.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.cboDelayCode.Name = "cboDelayCode";
            this.cboDelayCode.PopupWidth = 0;
            this.cboDelayCode.Properties.AutoHeight = false;
            this.cboDelayCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDelayCode.Properties.NullText = "";
            this.cboDelayCode.ShowHeader = true;
            this.cboDelayCode.Size = new System.Drawing.Size(497, 20);
            this.cboDelayCode.TabIndex = 7;
            this.cboDelayCode.VisibleColumns = null;
            this.cboDelayCode.VisibleColumnsWidth = null;
            // 
            // lblDelayCode
            // 
            this.lblDelayCode.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDelayCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDelayCode.LanguageKey = "DELAYREASON";
            this.lblDelayCode.Location = new System.Drawing.Point(0, 5);
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
            this.smartSplitTableLayoutPanel1.Controls.Add(this.lblDelayCode, 0, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.cboDelayCode, 1, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.lblComment, 0, 3);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.txtComment, 0, 5);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.btnCancel, 2, 8);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.btnSave, 3, 8);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 138);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 9;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(612, 191);
            this.smartSplitTableLayoutPanel1.TabIndex = 1;
            // 
            // lblComment
            // 
            this.lblComment.LanguageKey = "COMMENT";
            this.lblComment.Location = new System.Drawing.Point(3, 33);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(40, 14);
            this.lblComment.TabIndex = 11;
            this.lblComment.Text = "특이사항";
            // 
            // txtComment
            // 
            this.smartSplitTableLayoutPanel1.SetColumnSpan(this.txtComment, 4);
            this.txtComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtComment.Location = new System.Drawing.Point(3, 58);
            this.txtComment.Name = "txtComment";
            this.smartSplitTableLayoutPanel1.SetRowSpan(this.txtComment, 2);
            this.txtComment.Size = new System.Drawing.Size(601, 100);
            this.txtComment.TabIndex = 12;
            // 
            // grdStaying
            // 
            this.grdStaying.Caption = "";
            this.grdStaying.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdStaying.IsUsePaging = false;
            this.grdStaying.LanguageKey = null;
            this.grdStaying.Location = new System.Drawing.Point(0, 30);
            this.grdStaying.Margin = new System.Windows.Forms.Padding(0);
            this.grdStaying.Name = "grdStaying";
            this.grdStaying.ShowBorder = true;
            this.grdStaying.Size = new System.Drawing.Size(612, 108);
            this.grdStaying.TabIndex = 2;
            this.grdStaying.UseAutoBestFitColumns = false;
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.ColumnCount = 1;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdStaying, 0, 1);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.smartLabel1, 0, 0);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 2;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(612, 138);
            this.smartSplitTableLayoutPanel2.TabIndex = 2;
            // 
            // smartLabel1
            // 
            this.smartLabel1.Appearance.BackColor = System.Drawing.Color.Red;
            this.smartLabel1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.smartLabel1.Appearance.ForeColor = System.Drawing.Color.Yellow;
            this.smartLabel1.Appearance.Options.UseBackColor = true;
            this.smartLabel1.Appearance.Options.UseFont = true;
            this.smartLabel1.Appearance.Options.UseForeColor = true;
            this.smartLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLabel1.Location = new System.Drawing.Point(3, 3);
            this.smartLabel1.Name = "smartLabel1";
            this.smartLabel1.Size = new System.Drawing.Size(606, 24);
            this.smartLabel1.TabIndex = 3;
            this.smartLabel1.Text = "공정 체공이 기준 시간을 넘었습니다. 체공 사유를 입력해 주세요";
            // 
            // StayReasonCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 349);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.LanguageKey = "INPUTWAITINGCODE";
            this.Name = "StayReasonCode";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboDelayCode.Properties)).EndInit();
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).EndInit();
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
		private Framework.SmartControls.SmartLabel lblDelayCode;
		private Framework.SmartControls.SmartComboBox cboDelayCode;
		private Framework.SmartControls.SmartButton btnCancel;
		private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartLabel lblComment;
        private Framework.SmartControls.SmartMemoEdit txtComment;
        private Framework.SmartControls.SmartBandedGrid grdStaying;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
        private Framework.SmartControls.SmartLabel smartLabel1;
    }
}
