namespace Micube.SmartMES.QualityAnalysis
{
    partial class ucLotMessageGrd
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpMessage = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdMessage = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.rtbMessage = new Micube.Framework.SmartControls.SmartRichTextBox();
            this.btnWrite = new Micube.Framework.SmartControls.SmartButton();
            this.tlpMessage.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMessage
            // 
            this.tlpMessage.ColumnCount = 3;
            this.tlpMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tlpMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMessage.Controls.Add(this.grdMessage, 0, 0);
            this.tlpMessage.Controls.Add(this.tableLayoutPanel1, 2, 0);
            this.tlpMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMessage.Location = new System.Drawing.Point(0, 0);
            this.tlpMessage.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tlpMessage.Name = "tlpMessage";
            this.tlpMessage.RowCount = 1;
            this.tlpMessage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMessage.Size = new System.Drawing.Size(671, 469);
            this.tlpMessage.TabIndex = 2;
            // 
            // grdMessage
            // 
            this.grdMessage.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMessage.IsUsePaging = false;
            this.grdMessage.LanguageKey = null;
            this.grdMessage.Location = new System.Drawing.Point(0, 0);
            this.grdMessage.Margin = new System.Windows.Forms.Padding(0);
            this.grdMessage.Name = "grdMessage";
            this.grdMessage.ShowBorder = true;
            this.grdMessage.ShowButtonBar = false;
            this.grdMessage.ShowStatusBar = false;
            this.grdMessage.Size = new System.Drawing.Size(300, 469);
            this.grdMessage.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.rtbMessage, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnWrite, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(310, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(361, 469);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // rtbMessage
            // 
            this.rtbMessage.AlignCenterVisible = false;
            this.rtbMessage.AlignLeftVisible = false;
            this.rtbMessage.AlignRightVisible = false;
            this.rtbMessage.BoldVisible = false;
            this.rtbMessage.BulletsVisible = false;
            this.rtbMessage.ChooseFontVisible = false;
            this.rtbMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbMessage.FontColorVisible = false;
            this.rtbMessage.FontFamilyVisible = false;
            this.rtbMessage.FontSizeVisible = false;
            this.rtbMessage.INDENT = 10;
            this.rtbMessage.ItalicVisible = false;
            this.rtbMessage.Location = new System.Drawing.Point(0, 30);
            this.rtbMessage.Margin = new System.Windows.Forms.Padding(0);
            this.rtbMessage.Name = "rtbMessage";
            this.rtbMessage.ReadOnly = true;
            this.rtbMessage.Rtf = "{\\rtf1\\ansi\\ansicpg949\\deff0\\deflang1033\\deflangfe1042{\\fonttbl{\\f0\\fnil\\fcharset" +
    "204 Microsoft Sans Serif;}}\r\n\\viewkind4\\uc1\\pard\\lang1042\\f0\\fs18\\par\r\n}\r\n";
            this.rtbMessage.SeparatorAlignVisible = false;
            this.rtbMessage.SeparatorBoldUnderlineItalicVisible = false;
            this.rtbMessage.SeparatorFontColorVisible = false;
            this.rtbMessage.SeparatorFontVisible = false;
            this.rtbMessage.Size = new System.Drawing.Size(361, 439);
            this.rtbMessage.TabIndex = 2;
            this.rtbMessage.ToolStripVisible = false;
            this.rtbMessage.UnderlineVisible = false;
            this.rtbMessage.WordWrapVisible = false;
            // 
            // btnWrite
            // 
            this.btnWrite.AllowFocus = false;
            this.btnWrite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWrite.IsBusy = false;
            this.btnWrite.LanguageKey = "REGISTRATION";
            this.btnWrite.Location = new System.Drawing.Point(281, 0);
            this.btnWrite.Margin = new System.Windows.Forms.Padding(0);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnWrite.Size = new System.Drawing.Size(80, 25);
            this.btnWrite.TabIndex = 3;
            this.btnWrite.Text = "smartButton1";
            this.btnWrite.TooltipLanguageKey = "";
            // 
            // ucLotMessageGrd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMessage);
            this.Name = "ucLotMessageGrd";
            this.Size = new System.Drawing.Size(671, 469);
            this.tlpMessage.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel tlpMessage;
        private Framework.SmartControls.SmartBandedGrid grdMessage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartRichTextBox rtbMessage;
        private Framework.SmartControls.SmartButton btnWrite;
    }
}
