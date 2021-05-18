namespace Micube.SmartMES.ProcessManagement
{
	partial class LotMessagePopup
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
			this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
			this.btnClose = new Micube.Framework.SmartControls.SmartButton();
			this.smartPanel2 = new Micube.Framework.SmartControls.SmartPanel();
			this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
			this.grdMessageList = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.rtpComment = new Micube.Framework.SmartControls.SmartRichTextBox();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			this.pnlMain.SuspendLayout();
			this.smartSplitTableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
			this.smartPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).BeginInit();
			this.smartPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel1);
			this.pnlMain.Size = new System.Drawing.Size(1094, 527);
			// 
			// smartSplitTableLayoutPanel1
			// 
			this.smartSplitTableLayoutPanel1.ColumnCount = 1;
			this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.smartSplitTableLayoutPanel1.Controls.Add(this.smartPanel1, 0, 2);
			this.smartSplitTableLayoutPanel1.Controls.Add(this.smartPanel2, 0, 0);
			this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
			this.smartSplitTableLayoutPanel1.RowCount = 3;
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3F));
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(1094, 527);
			this.smartSplitTableLayoutPanel1.TabIndex = 0;
			// 
			// smartPanel1
			// 
			this.smartPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smartPanel1.Controls.Add(this.btnClose);
			this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smartPanel1.Location = new System.Drawing.Point(0, 501);
			this.smartPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.smartPanel1.Name = "smartPanel1";
			this.smartPanel1.Size = new System.Drawing.Size(1094, 26);
			this.smartPanel1.TabIndex = 0;
			// 
			// btnClose
			// 
			this.btnClose.AllowFocus = false;
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.IsBusy = false;
			this.btnClose.LanguageKey = "CLOSE";
			this.btnClose.Location = new System.Drawing.Point(1016, 2);
			this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.btnClose.Name = "btnClose";
			this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 0;
			this.btnClose.Text = "닫기";
			this.btnClose.TooltipLanguageKey = "";
			// 
			// smartPanel2
			// 
			this.smartPanel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smartPanel2.Controls.Add(this.grdMessageList);
			this.smartPanel2.Controls.Add(this.smartSpliterControl1);
			this.smartPanel2.Controls.Add(this.rtpComment);
			this.smartPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smartPanel2.Location = new System.Drawing.Point(0, 0);
			this.smartPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.smartPanel2.Name = "smartPanel2";
			this.smartPanel2.Size = new System.Drawing.Size(1094, 498);
			this.smartPanel2.TabIndex = 1;
			// 
			// smartSpliterControl1
			// 
			this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Right;
			this.smartSpliterControl1.Location = new System.Drawing.Point(538, 0);
			this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSpliterControl1.Name = "smartSpliterControl1";
			this.smartSpliterControl1.Size = new System.Drawing.Size(5, 498);
			this.smartSpliterControl1.TabIndex = 1;
			this.smartSpliterControl1.TabStop = false;
			// 
			// grdMessageList
			// 
			this.grdMessageList.Caption = "";
			this.grdMessageList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdMessageList.IsUsePaging = false;
			this.grdMessageList.LanguageKey = null;
			this.grdMessageList.Location = new System.Drawing.Point(0, 0);
			this.grdMessageList.Margin = new System.Windows.Forms.Padding(0);
			this.grdMessageList.Name = "grdMessageList";
			this.grdMessageList.ShowBorder = true;
			this.grdMessageList.Size = new System.Drawing.Size(538, 498);
			this.grdMessageList.TabIndex = 2;
			// 
			// rtpComment
			// 
			this.rtpComment.AlignCenterVisible = false;
			this.rtpComment.AlignLeftVisible = false;
			this.rtpComment.AlignRightVisible = false;
			this.rtpComment.BackColor = System.Drawing.Color.White;
			this.rtpComment.BoldVisible = false;
			this.rtpComment.BulletsVisible = false;
			this.rtpComment.ChooseFontVisible = false;
			this.rtpComment.Dock = System.Windows.Forms.DockStyle.Right;
			this.rtpComment.FontColorVisible = false;
			this.rtpComment.FontFamilyVisible = false;
			this.rtpComment.FontSizeVisible = false;
			this.rtpComment.INDENT = 10;
			this.rtpComment.ItalicVisible = false;
			this.rtpComment.Location = new System.Drawing.Point(543, 0);
			this.rtpComment.Name = "rtpComment";
			this.rtpComment.ReadOnly = true;
			this.rtpComment.Rtf = "{\\rtf1\\ansi\\ansicpg949\\deff0\\deflang1033\\deflangfe1042{\\fonttbl{\\f0\\fnil\\fcharset" +
    "204 Microsoft Sans Serif;}}\r\n\\viewkind4\\uc1\\pard\\lang1042\\f0\\fs18\\par\r\n}\r\n";
			this.rtpComment.SeparatorAlignVisible = false;
			this.rtpComment.SeparatorBoldUnderlineItalicVisible = false;
			this.rtpComment.SeparatorFontColorVisible = false;
			this.rtpComment.SeparatorFontVisible = false;
			this.rtpComment.Size = new System.Drawing.Size(551, 498);
			this.rtpComment.TabIndex = 3;
			this.rtpComment.ToolStripVisible = false;
			this.rtpComment.UnderlineVisible = false;
			this.rtpComment.WordWrapVisible = false;
			// 
			// LotMessagePopup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1114, 547);
			this.Name = "LotMessagePopup";
			this.Text = "SmartConditionBaseForm";
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			this.pnlMain.ResumeLayout(false);
			this.smartSplitTableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
			this.smartPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).EndInit();
			this.smartPanel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
		private Framework.SmartControls.SmartPanel smartPanel1;
		private Framework.SmartControls.SmartButton btnClose;
		private Framework.SmartControls.SmartPanel smartPanel2;
		private Framework.SmartControls.SmartBandedGrid grdMessageList;
		private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
		private Framework.SmartControls.SmartRichTextBox rtpComment;
	}
}