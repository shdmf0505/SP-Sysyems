namespace Micube.SmartMES.ProcessManagement
{
	partial class ucMessageInfo
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
			this.pnlOptions = new Micube.Framework.SmartControls.SmartPanel();
			this.chkPopupView = new Micube.Framework.SmartControls.SmartCheckBox();
			this.pnlTitle = new Micube.Framework.SmartControls.SmartPanel();
			this.txtTitle = new Micube.Framework.SmartControls.SmartTextBox();
			this.lblTitle = new Micube.Framework.SmartControls.SmartLabel();
			this.txtComment = new Micube.Framework.SmartControls.SmartRichTextBox();
			this.smartSplitTableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlOptions)).BeginInit();
			this.pnlOptions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.chkPopupView.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlTitle)).BeginInit();
			this.pnlTitle.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// smartSplitTableLayoutPanel1
			// 
			this.smartSplitTableLayoutPanel1.ColumnCount = 1;
			this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.smartSplitTableLayoutPanel1.Controls.Add(this.pnlOptions, 0, 0);
			this.smartSplitTableLayoutPanel1.Controls.Add(this.pnlTitle, 0, 1);
			this.smartSplitTableLayoutPanel1.Controls.Add(this.txtComment, 0, 2);
			this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
			this.smartSplitTableLayoutPanel1.RowCount = 3;
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(441, 413);
			this.smartSplitTableLayoutPanel1.TabIndex = 1;
			// 
			// pnlOptions
			// 
			this.pnlOptions.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pnlOptions.Controls.Add(this.chkPopupView);
			this.pnlOptions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlOptions.Location = new System.Drawing.Point(0, 0);
			this.pnlOptions.Margin = new System.Windows.Forms.Padding(0);
			this.pnlOptions.Name = "pnlOptions";
			this.pnlOptions.Size = new System.Drawing.Size(441, 26);
			this.pnlOptions.TabIndex = 0;
			// 
			// chkPopupView
			// 
			this.chkPopupView.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.chkPopupView.LanguageKey = "CHKPOPUPVIEW";
			this.chkPopupView.Location = new System.Drawing.Point(3, 3);
			this.chkPopupView.Name = "chkPopupView";
			this.chkPopupView.Properties.Caption = "팝업으로 보여주기";
			this.chkPopupView.Size = new System.Drawing.Size(130, 19);
			this.chkPopupView.TabIndex = 0;
			// 
			// pnlTitle
			// 
			this.pnlTitle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pnlTitle.Controls.Add(this.txtTitle);
			this.pnlTitle.Controls.Add(this.lblTitle);
			this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlTitle.Location = new System.Drawing.Point(0, 26);
			this.pnlTitle.Margin = new System.Windows.Forms.Padding(0);
			this.pnlTitle.Name = "pnlTitle";
			this.pnlTitle.Size = new System.Drawing.Size(441, 26);
			this.pnlTitle.TabIndex = 1;
			// 
			// txtTitle
			// 
			this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.txtTitle.LabelText = null;
			this.txtTitle.LanguageKey = null;
			this.txtTitle.Location = new System.Drawing.Point(53, 3);
			this.txtTitle.Name = "txtTitle";
			this.txtTitle.Size = new System.Drawing.Size(385, 20);
			this.txtTitle.TabIndex = 1;
			// 
			// lblTitle
			// 
			this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTitle.LanguageKey = "TITLE";
			this.lblTitle.Location = new System.Drawing.Point(3, 5);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(32, 14);
			this.lblTitle.TabIndex = 0;
			this.lblTitle.Text = "제목 : ";
			// 
			// txtComment
			// 
			this.txtComment.AlignCenterVisible = true;
			this.txtComment.AlignLeftVisible = true;
			this.txtComment.AlignRightVisible = true;
			this.txtComment.BoldVisible = true;
			this.txtComment.BulletsVisible = true;
			this.txtComment.ChooseFontVisible = true;
			this.txtComment.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtComment.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.txtComment.FontColorVisible = true;
			this.txtComment.FontFamilyVisible = true;
			this.txtComment.FontSizeVisible = true;
			this.txtComment.INDENT = 10;
			this.txtComment.ItalicVisible = true;
			this.txtComment.Location = new System.Drawing.Point(3, 56);
			this.txtComment.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txtComment.Name = "txtComment";
			this.txtComment.Rtf = "{\\rtf1\\ansi\\deff0{\\fonttbl{\\f0\\fnil\\fcharset204 Microsoft Sans Serif;}}\r\n\\viewkin" +
    "d4\\uc1\\pard\\lang1042\\f0\\fs18 smartRichTextBox1\\par\r\n}\r\n";
			this.txtComment.SeparatorAlignVisible = true;
			this.txtComment.SeparatorBoldUnderlineItalicVisible = true;
			this.txtComment.SeparatorFontColorVisible = true;
			this.txtComment.SeparatorFontVisible = true;
			this.txtComment.Size = new System.Drawing.Size(435, 353);
			this.txtComment.TabIndex = 2;
			this.txtComment.ToolStripVisible = true;
			this.txtComment.UnderlineVisible = true;
			this.txtComment.WordWrapVisible = false;
			// 
			// ucMessageInfo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.smartSplitTableLayoutPanel1);
			this.Name = "ucMessageInfo";
			this.Size = new System.Drawing.Size(441, 413);
			this.smartSplitTableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlOptions)).EndInit();
			this.pnlOptions.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.chkPopupView.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlTitle)).EndInit();
			this.pnlTitle.ResumeLayout(false);
			this.pnlTitle.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
		private Framework.SmartControls.SmartPanel pnlOptions;
		private Framework.SmartControls.SmartCheckBox chkPopupView;
		private Framework.SmartControls.SmartPanel pnlTitle;
		private Framework.SmartControls.SmartTextBox txtTitle;
		private Framework.SmartControls.SmartLabel lblTitle;
		private Framework.SmartControls.SmartRichTextBox txtComment;
	}
}
