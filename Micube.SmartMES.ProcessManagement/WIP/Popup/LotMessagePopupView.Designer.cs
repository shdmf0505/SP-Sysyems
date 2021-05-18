namespace Micube.SmartMES.ProcessManagement
{
	partial class LotMessagePopupView
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
			this.pnlTitle = new Micube.Framework.SmartControls.SmartPanel();
			this.txtTitle = new Micube.Framework.SmartControls.SmartTextBox();
			this.lblTitle = new Micube.Framework.SmartControls.SmartLabel();
			this.grdLotInfo = new Micube.SmartMES.Commons.Controls.SmartLotInfoGrid();
			this.grdMessageList = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.rtbMessage = new Micube.Framework.SmartControls.SmartRichTextBox();
			this.pnlButton = new Micube.Framework.SmartControls.SmartPanel();
			this.btnClose = new Micube.Framework.SmartControls.SmartButton();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			this.pnlMain.SuspendLayout();
			this.smartSplitTableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlTitle)).BeginInit();
			this.pnlTitle.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlButton)).BeginInit();
			this.pnlButton.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel1);
			this.pnlMain.Size = new System.Drawing.Size(483, 654);
			// 
			// smartSplitTableLayoutPanel1
			// 
			this.smartSplitTableLayoutPanel1.ColumnCount = 1;
			this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.smartSplitTableLayoutPanel1.Controls.Add(this.pnlTitle, 0, 4);
			this.smartSplitTableLayoutPanel1.Controls.Add(this.grdLotInfo, 0, 0);
			this.smartSplitTableLayoutPanel1.Controls.Add(this.grdMessageList, 0, 2);
			this.smartSplitTableLayoutPanel1.Controls.Add(this.rtbMessage, 0, 6);
			this.smartSplitTableLayoutPanel1.Controls.Add(this.pnlButton, 0, 8);
			this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
			this.smartSplitTableLayoutPanel1.RowCount = 9;
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 151F));
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 250F));
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3F));
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3F));
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(483, 654);
			this.smartSplitTableLayoutPanel1.TabIndex = 0;
			// 
			// pnlTitle
			// 
			this.pnlTitle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pnlTitle.Controls.Add(this.txtTitle);
			this.pnlTitle.Controls.Add(this.lblTitle);
			this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlTitle.Location = new System.Drawing.Point(0, 421);
			this.pnlTitle.Margin = new System.Windows.Forms.Padding(0);
			this.pnlTitle.Name = "pnlTitle";
			this.pnlTitle.Size = new System.Drawing.Size(483, 26);
			this.pnlTitle.TabIndex = 2;
			// 
			// txtTitle
			// 
			this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.txtTitle.LabelText = null;
			this.txtTitle.LanguageKey = null;
			this.txtTitle.Location = new System.Drawing.Point(53, 3);
			this.txtTitle.Name = "txtTitle";
			this.txtTitle.Size = new System.Drawing.Size(427, 20);
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
			// grdLotInfo
			// 
			this.grdLotInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdLotInfo.Location = new System.Drawing.Point(0, 0);
			this.grdLotInfo.Margin = new System.Windows.Forms.Padding(0);
			this.grdLotInfo.Name = "grdLotInfo";
			this.grdLotInfo.Size = new System.Drawing.Size(483, 151);
			this.grdLotInfo.TabIndex = 0;
			// 
			// grdMessageList
			// 
			this.grdMessageList.Caption = "";
			this.grdMessageList.IsUsePaging = false;
			this.grdMessageList.LanguageKey = null;
			this.grdMessageList.Location = new System.Drawing.Point(0, 161);
			this.grdMessageList.Margin = new System.Windows.Forms.Padding(0);
			this.grdMessageList.Name = "grdMessageList";
			this.grdMessageList.ShowBorder = true;
			this.grdMessageList.Size = new System.Drawing.Size(483, 250);
			this.grdMessageList.TabIndex = 1;
			// 
			// rtbMessage
			// 
			this.rtbMessage.AlignCenterVisible = false;
			this.rtbMessage.AlignLeftVisible = false;
			this.rtbMessage.AlignRightVisible = false;
			this.rtbMessage.BoldVisible = false;
			this.rtbMessage.BulletsVisible = false;
			this.rtbMessage.ChooseFontVisible = false;
			this.rtbMessage.FontColorVisible = false;
			this.rtbMessage.FontFamilyVisible = false;
			this.rtbMessage.FontSizeVisible = false;
			this.rtbMessage.INDENT = 10;
			this.rtbMessage.ItalicVisible = false;
			this.rtbMessage.Location = new System.Drawing.Point(3, 453);
			this.rtbMessage.Name = "rtbMessage";
			this.rtbMessage.ReadOnly = true;
			this.rtbMessage.Rtf = "{\\rtf1\\ansi\\ansicpg949\\deff0\\deflang1033\\deflangfe1042{\\fonttbl{\\f0\\fnil\\fcharset" +
    "204 Microsoft Sans Serif;}}\r\n\\viewkind4\\uc1\\pard\\lang1042\\f0\\fs18\\par\r\n}\r\n";
			this.rtbMessage.SeparatorAlignVisible = false;
			this.rtbMessage.SeparatorBoldUnderlineItalicVisible = false;
			this.rtbMessage.SeparatorFontColorVisible = false;
			this.rtbMessage.SeparatorFontVisible = false;
			this.rtbMessage.Size = new System.Drawing.Size(477, 169);
			this.rtbMessage.TabIndex = 3;
			this.rtbMessage.ToolStripVisible = false;
			this.rtbMessage.UnderlineVisible = false;
			this.rtbMessage.WordWrapVisible = false;
			// 
			// pnlButton
			// 
			this.pnlButton.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pnlButton.Controls.Add(this.btnClose);
			this.pnlButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlButton.Location = new System.Drawing.Point(0, 628);
			this.pnlButton.Margin = new System.Windows.Forms.Padding(0);
			this.pnlButton.Name = "pnlButton";
			this.pnlButton.Size = new System.Drawing.Size(483, 26);
			this.pnlButton.TabIndex = 4;
			// 
			// btnClose
			// 
			this.btnClose.AllowFocus = false;
			this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.btnClose.IsBusy = false;
			this.btnClose.LanguageKey = "CLOSE";
			this.btnClose.Location = new System.Drawing.Point(406, 1);
			this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.btnClose.Name = "btnClose";
			this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 0;
			this.btnClose.Text = "닫기";
			this.btnClose.TooltipLanguageKey = "";
			// 
			// LotMessagePopupView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(503, 674);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.LanguageKey = "LOTMESSAGEPOPUP";
			this.Name = "LotMessagePopupView";
			this.Text = "SmartConditionBaseForm";
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			this.pnlMain.ResumeLayout(false);
			this.smartSplitTableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlTitle)).EndInit();
			this.pnlTitle.ResumeLayout(false);
			this.pnlTitle.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlButton)).EndInit();
			this.pnlButton.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
		private Commons.Controls.SmartLotInfoGrid grdLotInfo;
		private Framework.SmartControls.SmartBandedGrid grdMessageList;
		private Framework.SmartControls.SmartPanel pnlTitle;
		private Framework.SmartControls.SmartTextBox txtTitle;
		private Framework.SmartControls.SmartLabel lblTitle;
		private Framework.SmartControls.SmartRichTextBox rtbMessage;
		private Framework.SmartControls.SmartPanel pnlButton;
		private Framework.SmartControls.SmartButton btnClose;
	}
}