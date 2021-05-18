namespace Micube.SmartMES.ProcessManagement
{
    partial class usLotMessage
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
            this.grdMessage = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.rtbMessage = new Micube.Framework.SmartControls.SmartRichTextBox();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtTitle = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblMessageTitle = new Micube.Framework.SmartControls.SmartLabel();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grdMessage
            // 
            this.grdMessage.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdMessage.Dock = System.Windows.Forms.DockStyle.Left;
            this.grdMessage.IsUsePaging = false;
            this.grdMessage.LanguageKey = null;
            this.grdMessage.Location = new System.Drawing.Point(0, 0);
            this.grdMessage.Margin = new System.Windows.Forms.Padding(0);
            this.grdMessage.Name = "grdMessage";
            this.grdMessage.ShowBorder = true;
            this.grdMessage.ShowStatusBar = false;
            this.grdMessage.Size = new System.Drawing.Size(766, 389);
            this.grdMessage.TabIndex = 0;
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
            this.rtbMessage.Location = new System.Drawing.Point(771, 34);
            this.rtbMessage.Margin = new System.Windows.Forms.Padding(0);
            this.rtbMessage.Name = "rtbMessage";
            this.rtbMessage.Rtf = "{\\rtf1\\ansi\\ansicpg949\\deff0\\deflang1033\\deflangfe1042{\\fonttbl{\\f0\\fnil\\fcharset" +
    "204 Microsoft Sans Serif;}}\r\n\\viewkind4\\uc1\\pard\\lang1042\\f0\\fs18\\par\r\n}\r\n";
            this.rtbMessage.SeparatorAlignVisible = false;
            this.rtbMessage.SeparatorBoldUnderlineItalicVisible = false;
            this.rtbMessage.SeparatorFontColorVisible = false;
            this.rtbMessage.SeparatorFontVisible = false;
            this.rtbMessage.Size = new System.Drawing.Size(289, 355);
            this.rtbMessage.TabIndex = 1;
            this.rtbMessage.ToolStripVisible = false;
            this.rtbMessage.UnderlineVisible = false;
            this.rtbMessage.WordWrapVisible = false;
            // 
            // smartSpliterControl1
            // 
            this.smartSpliterControl1.Location = new System.Drawing.Point(766, 0);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(5, 389);
            this.smartSpliterControl1.TabIndex = 14;
            this.smartSpliterControl1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtTitle);
            this.panel3.Controls.Add(this.lblMessageTitle);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(771, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(289, 34);
            this.panel3.TabIndex = 15;
            // 
            // txtTitle
            // 
            this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitle.LabelText = null;
            this.txtTitle.LanguageKey = null;
            this.txtTitle.Location = new System.Drawing.Point(58, 7);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(215, 20);
            this.txtTitle.TabIndex = 3;
            // 
            // lblMessageTitle
            // 
            this.lblMessageTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMessageTitle.LanguageKey = "TITLE";
            this.lblMessageTitle.Location = new System.Drawing.Point(6, 10);
            this.lblMessageTitle.Name = "lblMessageTitle";
            this.lblMessageTitle.Size = new System.Drawing.Size(32, 14);
            this.lblMessageTitle.TabIndex = 2;
            this.lblMessageTitle.Text = "제목 : ";
            // 
            // usLotMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rtbMessage);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.smartSpliterControl1);
            this.Controls.Add(this.grdMessage);
            this.Name = "usLotMessage";
            this.Size = new System.Drawing.Size(1060, 389);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Framework.SmartControls.SmartBandedGrid grdMessage;
        private Framework.SmartControls.SmartRichTextBox rtbMessage;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
        private System.Windows.Forms.Panel panel3;
        private Framework.SmartControls.SmartTextBox txtTitle;
        private Framework.SmartControls.SmartLabel lblMessageTitle;
    }
}
