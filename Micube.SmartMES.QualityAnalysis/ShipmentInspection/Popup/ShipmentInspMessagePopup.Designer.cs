namespace Micube.SmartMES.QualityAnalysis
{
    partial class ShipmentInspMessagePopup
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            this.memoContents = new Micube.Framework.SmartControls.SmartRichTextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtUser = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtProcessSegment = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtTile = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.grdLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProcessSegment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTile.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(952, 291);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.06441F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.93559F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.memoContents, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.grdLotList, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.18182F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81.81818F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(952, 291);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.btnClose);
            this.flowLayoutPanel1.Controls.Add(this.btnSave);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(356, 265);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(596, 26);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.IsBusy = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(516, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(80, 25);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "smartButton1";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.IsBusy = false;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(430, 0);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "smartButton2";
            this.btnSave.TooltipLanguageKey = "";
            // 
            // memoContents
            // 
            this.memoContents.AlignCenterVisible = true;
            this.memoContents.AlignLeftVisible = true;
            this.memoContents.AlignRightVisible = true;
            this.memoContents.BoldVisible = true;
            this.memoContents.BulletsVisible = true;
            this.memoContents.ChooseFontVisible = true;
            this.memoContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoContents.FontColorVisible = true;
            this.memoContents.FontFamilyVisible = true;
            this.memoContents.FontSizeVisible = true;
            this.memoContents.INDENT = 10;
            this.memoContents.ItalicVisible = true;
            this.memoContents.Location = new System.Drawing.Point(359, 53);
            this.memoContents.Name = "memoContents";
            this.memoContents.Rtf = "{\\rtf1\\ansi\\ansicpg949\\deff0\\deflang1033\\deflangfe1042{\\fonttbl{\\f0\\fnil\\fcharset" +
    "204 Microsoft Sans Serif;}}\r\n\\viewkind4\\uc1\\pard\\lang1042\\f0\\fs18\\par\r\n}\r\n";
            this.memoContents.SeparatorAlignVisible = true;
            this.memoContents.SeparatorBoldUnderlineItalicVisible = true;
            this.memoContents.SeparatorFontColorVisible = true;
            this.memoContents.SeparatorFontVisible = true;
            this.memoContents.Size = new System.Drawing.Size(590, 199);
            this.memoContents.TabIndex = 5;
            this.memoContents.ToolStripVisible = true;
            this.memoContents.UnderlineVisible = true;
            this.memoContents.WordWrapVisible = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.txtUser, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtProcessSegment, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtTile, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(356, 5);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(596, 45);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // txtUser
            // 
            this.txtUser.AutoHeight = false;
            this.txtUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUser.LanguageKey = "CREATEUSER";
            this.txtUser.Location = new System.Drawing.Point(298, 0);
            this.txtUser.Margin = new System.Windows.Forms.Padding(0);
            this.txtUser.Name = "txtUser";
            this.txtUser.Properties.AutoHeight = false;
            this.txtUser.Properties.ReadOnly = true;
            this.txtUser.Size = new System.Drawing.Size(298, 17);
            this.txtUser.TabIndex = 1;
            // 
            // txtProcessSegment
            // 
            this.txtProcessSegment.AutoHeight = false;
            this.txtProcessSegment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProcessSegment.LanguageKey = "PROCESSSEGMENTNAME";
            this.txtProcessSegment.Location = new System.Drawing.Point(0, 0);
            this.txtProcessSegment.Margin = new System.Windows.Forms.Padding(0);
            this.txtProcessSegment.Name = "txtProcessSegment";
            this.txtProcessSegment.Properties.AutoHeight = false;
            this.txtProcessSegment.Properties.ReadOnly = true;
            this.txtProcessSegment.Size = new System.Drawing.Size(298, 17);
            this.txtProcessSegment.TabIndex = 0;
            // 
            // txtTile
            // 
            this.txtTile.AutoHeight = false;
            this.tableLayoutPanel2.SetColumnSpan(this.txtTile, 2);
            this.txtTile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTile.LabelWidth = "14.5%";
            this.txtTile.LanguageKey = "TITLE";
            this.txtTile.Location = new System.Drawing.Point(0, 22);
            this.txtTile.Margin = new System.Windows.Forms.Padding(0);
            this.txtTile.Name = "txtTile";
            this.txtTile.Properties.AutoHeight = false;
            this.txtTile.Size = new System.Drawing.Size(596, 17);
            this.txtTile.TabIndex = 2;
            // 
            // grdLotList
            // 
            this.grdLotList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLotList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLotList.IsUsePaging = false;
            this.grdLotList.LanguageKey = null;
            this.grdLotList.Location = new System.Drawing.Point(0, 5);
            this.grdLotList.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotList.Name = "grdLotList";
            this.tableLayoutPanel1.SetRowSpan(this.grdLotList, 2);
            this.grdLotList.ShowBorder = true;
            this.grdLotList.ShowButtonBar = false;
            this.grdLotList.ShowStatusBar = false;
            this.grdLotList.Size = new System.Drawing.Size(351, 250);
            this.grdLotList.TabIndex = 7;
            // 
            // ShipmentInspMessagePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 311);
            this.Name = "ShipmentInspMessagePopup";
            this.Text = "ShipmentInspMessagePopup";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProcessSegment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTile.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartLabelTextBox txtProcessSegment;
        private Framework.SmartControls.SmartLabelTextBox txtUser;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartRichTextBox memoContents;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Framework.SmartControls.SmartBandedGrid grdLotList;
        private Framework.SmartControls.SmartLabelTextBox txtTile;
    }
}