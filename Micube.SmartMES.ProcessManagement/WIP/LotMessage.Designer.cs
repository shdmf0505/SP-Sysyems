namespace Micube.SmartMES.ProcessManagement
{
	partial class LotMessage
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
			this.tabControl = new Micube.Framework.SmartControls.SmartTabControl();
			this.tabPageProductDef = new DevExpress.XtraTab.XtraTabPage();
			this.grdProductDefInfo = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.smartSpliterControl2 = new Micube.Framework.SmartControls.SmartSpliterControl();
			this.grpProductDef = new Micube.Framework.SmartControls.SmartGroupBox();
			this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
			this.ucMessageInfoProductDef = new Micube.SmartMES.ProcessManagement.ucMessageInfo();
			this.pnlOptions = new Micube.Framework.SmartControls.SmartPanel();
			this.popSendSegmentFrPd = new Micube.Framework.SmartControls.SmartLabelSelectPopupEdit();
			this.tabPageSegment = new DevExpress.XtraTab.XtraTabPage();
			this.grdSegmentInfo = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.smartSpliterControl3 = new Micube.Framework.SmartControls.SmartSpliterControl();
			this.grpSegment = new Micube.Framework.SmartControls.SmartGroupBox();
			this.ucMessageInfoSegment = new Micube.SmartMES.ProcessManagement.ucMessageInfo();
			this.tabPageLot = new DevExpress.XtraTab.XtraTabPage();
			this.grdWIP = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.spcSpliter = new Micube.Framework.SmartControls.SmartSpliterControl();
			this.panel1 = new System.Windows.Forms.Panel();
			this.grdTargetWip = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
			this.grpLot = new Micube.Framework.SmartControls.SmartGroupBox();
			this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
			this.ucMessageInfoLot = new Micube.SmartMES.ProcessManagement.ucMessageInfo();
			this.popSendSegmentFrLot = new Micube.Framework.SmartControls.SmartLabelSelectPopupEdit();
			this.pnlUpDown = new System.Windows.Forms.Panel();
			this.ucDataUpDownBtn = new Micube.SmartMES.Commons.Controls.ucDataUpDownBtn();
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
			this.pnlContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
			this.tabControl.SuspendLayout();
			this.tabPageProductDef.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grpProductDef)).BeginInit();
			this.grpProductDef.SuspendLayout();
			this.smartSplitTableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlOptions)).BeginInit();
			this.pnlOptions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.popSendSegmentFrPd.Properties)).BeginInit();
			this.tabPageSegment.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grpSegment)).BeginInit();
			this.grpSegment.SuspendLayout();
			this.tabPageLot.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grpLot)).BeginInit();
			this.grpLot.SuspendLayout();
			this.smartSplitTableLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.popSendSegmentFrLot.Properties)).BeginInit();
			this.pnlUpDown.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlCondition
			// 
			this.pnlCondition.Location = new System.Drawing.Point(2, 31);
			this.pnlCondition.Size = new System.Drawing.Size(296, 659);
			// 
			// pnlToolbar
			// 
			this.pnlToolbar.Size = new System.Drawing.Size(727, 24);
			// 
			// pnlContent
			// 
			this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
			this.pnlContent.Appearance.Options.UseBackColor = true;
			this.pnlContent.Controls.Add(this.tabControl);
			this.pnlContent.Size = new System.Drawing.Size(727, 663);
			// 
			// pnlMain
			// 
			this.pnlMain.Size = new System.Drawing.Size(1032, 692);
			// 
			// tabControl
			// 
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedTabPage = this.tabPageProductDef;
			this.tabControl.Size = new System.Drawing.Size(727, 663);
			this.tabControl.TabIndex = 0;
			this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageProductDef,
            this.tabPageSegment,
            this.tabPageLot});
			// 
			// tabPageProductDef
			// 
			this.tabPageProductDef.Controls.Add(this.grdProductDefInfo);
			this.tabPageProductDef.Controls.Add(this.smartSpliterControl2);
			this.tabPageProductDef.Controls.Add(this.grpProductDef);
			this.tabControl.SetLanguageKey(this.tabPageProductDef, "PRODUCTDEF");
			this.tabPageProductDef.Name = "tabPageProductDef";
			this.tabPageProductDef.Size = new System.Drawing.Size(721, 634);
			this.tabPageProductDef.Text = "품목";
			// 
			// grdProductDefInfo
			// 
			this.grdProductDefInfo.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdProductDefInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdProductDefInfo.IsUsePaging = false;
			this.grdProductDefInfo.LanguageKey = "GRIDPRODUCTLIST";
			this.grdProductDefInfo.Location = new System.Drawing.Point(0, 0);
			this.grdProductDefInfo.Margin = new System.Windows.Forms.Padding(0);
			this.grdProductDefInfo.Name = "grdProductDefInfo";
			this.grdProductDefInfo.ShowBorder = true;
			this.grdProductDefInfo.ShowStatusBar = false;
			this.grdProductDefInfo.Size = new System.Drawing.Size(721, 301);
			this.grdProductDefInfo.TabIndex = 2;
			// 
			// smartSpliterControl2
			// 
			this.smartSpliterControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.smartSpliterControl2.Location = new System.Drawing.Point(0, 301);
			this.smartSpliterControl2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSpliterControl2.Name = "smartSpliterControl2";
			this.smartSpliterControl2.Size = new System.Drawing.Size(721, 5);
			this.smartSpliterControl2.TabIndex = 1;
			this.smartSpliterControl2.TabStop = false;
			// 
			// grpProductDef
			// 
			this.grpProductDef.Controls.Add(this.smartSplitTableLayoutPanel1);
			this.grpProductDef.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
			this.grpProductDef.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.grpProductDef.GroupStyle = DevExpress.Utils.GroupStyle.Card;
			this.grpProductDef.LanguageKey = "MESSAGEINFO";
			this.grpProductDef.Location = new System.Drawing.Point(0, 306);
			this.grpProductDef.Name = "grpProductDef";
			this.grpProductDef.ShowBorder = true;
			this.grpProductDef.Size = new System.Drawing.Size(721, 328);
			this.grpProductDef.TabIndex = 0;
			this.grpProductDef.Text = "smartGroupBox1";
			// 
			// smartSplitTableLayoutPanel1
			// 
			this.smartSplitTableLayoutPanel1.ColumnCount = 1;
			this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.smartSplitTableLayoutPanel1.Controls.Add(this.ucMessageInfoProductDef, 0, 1);
			this.smartSplitTableLayoutPanel1.Controls.Add(this.pnlOptions, 0, 0);
			this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(2, 31);
			this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
			this.smartSplitTableLayoutPanel1.RowCount = 2;
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(717, 295);
			this.smartSplitTableLayoutPanel1.TabIndex = 0;
			// 
			// ucMessageInfoProductDef
			// 
			this.ucMessageInfoProductDef.CheckedShowType = false;
			this.ucMessageInfoProductDef.CommentText = "{\\rtf1\\ansi\\ansicpg949\\deff0\\deflang1033\\deflangfe1042{\\fonttbl{\\f0\\fnil\\fcharset" +
    "204 Microsoft Sans Serif;}}\r\n\\viewkind4\\uc1\\pard\\lang1042\\f0\\fs18\\par\r\n}\r\n";
			this.ucMessageInfoProductDef.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ucMessageInfoProductDef.Location = new System.Drawing.Point(3, 29);
			this.ucMessageInfoProductDef.Name = "ucMessageInfoProductDef";
			this.ucMessageInfoProductDef.Size = new System.Drawing.Size(711, 263);
			this.ucMessageInfoProductDef.TabIndex = 0;
			this.ucMessageInfoProductDef.TitleText = "";
			// 
			// pnlOptions
			// 
			this.pnlOptions.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pnlOptions.Controls.Add(this.popSendSegmentFrPd);
			this.pnlOptions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlOptions.Location = new System.Drawing.Point(0, 0);
			this.pnlOptions.Margin = new System.Windows.Forms.Padding(0);
			this.pnlOptions.Name = "pnlOptions";
			this.pnlOptions.Size = new System.Drawing.Size(717, 26);
			this.pnlOptions.TabIndex = 1;
			// 
			// popSendSegmentFrPd
			// 
			this.popSendSegmentFrPd.Appearance.ForeColor = System.Drawing.Color.Red;
			this.popSendSegmentFrPd.Appearance.Options.UseForeColor = true;
			this.popSendSegmentFrPd.EditorWidth = "70%";
			this.popSendSegmentFrPd.LabelWidth = "30%";
			this.popSendSegmentFrPd.LanguageKey = "SENDSEGMENT";
			this.popSendSegmentFrPd.Location = new System.Drawing.Point(3, 3);
			this.popSendSegmentFrPd.Name = "popSendSegmentFrPd";
			this.popSendSegmentFrPd.Size = new System.Drawing.Size(220, 20);
			this.popSendSegmentFrPd.TabIndex = 0;
			// 
			// tabPageSegment
			// 
			this.tabPageSegment.Controls.Add(this.grdSegmentInfo);
			this.tabPageSegment.Controls.Add(this.smartSpliterControl3);
			this.tabPageSegment.Controls.Add(this.grpSegment);
			this.tabControl.SetLanguageKey(this.tabPageSegment, "PROCESSSEGMENT");
			this.tabPageSegment.Name = "tabPageSegment";
			this.tabPageSegment.Size = new System.Drawing.Size(469, 372);
			this.tabPageSegment.Text = "공정";
			// 
			// grdSegmentInfo
			// 
			this.grdSegmentInfo.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdSegmentInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdSegmentInfo.IsUsePaging = false;
			this.grdSegmentInfo.LanguageKey = "PROCESSSEGMENTLIST";
			this.grdSegmentInfo.Location = new System.Drawing.Point(0, 0);
			this.grdSegmentInfo.Margin = new System.Windows.Forms.Padding(0);
			this.grdSegmentInfo.Name = "grdSegmentInfo";
			this.grdSegmentInfo.ShowBorder = true;
			this.grdSegmentInfo.ShowStatusBar = false;
			this.grdSegmentInfo.Size = new System.Drawing.Size(469, 39);
			this.grdSegmentInfo.TabIndex = 3;
			// 
			// smartSpliterControl3
			// 
			this.smartSpliterControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.smartSpliterControl3.Location = new System.Drawing.Point(0, 39);
			this.smartSpliterControl3.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSpliterControl3.Name = "smartSpliterControl3";
			this.smartSpliterControl3.Size = new System.Drawing.Size(469, 5);
			this.smartSpliterControl3.TabIndex = 4;
			this.smartSpliterControl3.TabStop = false;
			// 
			// grpSegment
			// 
			this.grpSegment.Controls.Add(this.ucMessageInfoSegment);
			this.grpSegment.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
			this.grpSegment.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.grpSegment.GroupStyle = DevExpress.Utils.GroupStyle.Card;
			this.grpSegment.LanguageKey = "MESSAGEINFO";
			this.grpSegment.Location = new System.Drawing.Point(0, 44);
			this.grpSegment.Name = "grpSegment";
			this.grpSegment.ShowBorder = true;
			this.grpSegment.Size = new System.Drawing.Size(469, 328);
			this.grpSegment.TabIndex = 5;
			this.grpSegment.Text = "smartGroupBox3";
			// 
			// ucMessageInfoSegment
			// 
			this.ucMessageInfoSegment.CheckedShowType = false;
			this.ucMessageInfoSegment.CommentText = "{\\rtf1\\ansi\\ansicpg949\\deff0\\deflang1033\\deflangfe1042{\\fonttbl{\\f0\\fnil\\fcharset" +
    "204 Microsoft Sans Serif;}}\r\n\\viewkind4\\uc1\\pard\\lang1042\\f0\\fs18\\par\r\n}\r\n";
			this.ucMessageInfoSegment.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ucMessageInfoSegment.Location = new System.Drawing.Point(2, 31);
			this.ucMessageInfoSegment.Name = "ucMessageInfoSegment";
			this.ucMessageInfoSegment.Size = new System.Drawing.Size(465, 295);
			this.ucMessageInfoSegment.TabIndex = 0;
			this.ucMessageInfoSegment.TitleText = "";
			// 
			// tabPageLot
			// 
			this.tabPageLot.Controls.Add(this.grdWIP);
			this.tabPageLot.Controls.Add(this.spcSpliter);
			this.tabPageLot.Controls.Add(this.panel1);
			this.tabControl.SetLanguageKey(this.tabPageLot, "LOT");
			this.tabPageLot.Name = "tabPageLot";
			this.tabPageLot.Size = new System.Drawing.Size(721, 634);
			this.tabPageLot.Text = "LOT";
			// 
			// grdWIP
			// 
			this.grdWIP.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdWIP.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdWIP.IsUsePaging = false;
			this.grdWIP.LanguageKey = "WIPLIST";
			this.grdWIP.Location = new System.Drawing.Point(0, 0);
			this.grdWIP.Margin = new System.Windows.Forms.Padding(0);
			this.grdWIP.Name = "grdWIP";
			this.grdWIP.ShowBorder = true;
			this.grdWIP.ShowStatusBar = false;
			this.grdWIP.Size = new System.Drawing.Size(721, 301);
			this.grdWIP.TabIndex = 3;
			// 
			// spcSpliter
			// 
			this.spcSpliter.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.spcSpliter.Location = new System.Drawing.Point(0, 301);
			this.spcSpliter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.spcSpliter.Name = "spcSpliter";
			this.spcSpliter.Size = new System.Drawing.Size(721, 5);
			this.spcSpliter.TabIndex = 7;
			this.spcSpliter.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.grdTargetWip);
			this.panel1.Controls.Add(this.smartSpliterControl1);
			this.panel1.Controls.Add(this.grpLot);
			this.panel1.Controls.Add(this.pnlUpDown);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 306);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(721, 328);
			this.panel1.TabIndex = 8;
			// 
			// grdTargetWip
			// 
			this.grdTargetWip.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdTargetWip.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdTargetWip.IsUsePaging = false;
			this.grdTargetWip.LanguageKey = "TARGETWIP";
			this.grdTargetWip.Location = new System.Drawing.Point(0, 45);
			this.grdTargetWip.Margin = new System.Windows.Forms.Padding(0);
			this.grdTargetWip.Name = "grdTargetWip";
			this.grdTargetWip.ShowBorder = true;
			this.grdTargetWip.ShowStatusBar = false;
			this.grdTargetWip.Size = new System.Drawing.Size(216, 283);
			this.grdTargetWip.TabIndex = 8;
			// 
			// smartSpliterControl1
			// 
			this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Right;
			this.smartSpliterControl1.Location = new System.Drawing.Point(216, 45);
			this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSpliterControl1.Name = "smartSpliterControl1";
			this.smartSpliterControl1.Size = new System.Drawing.Size(5, 283);
			this.smartSpliterControl1.TabIndex = 6;
			this.smartSpliterControl1.TabStop = false;
			// 
			// grpLot
			// 
			this.grpLot.Controls.Add(this.smartSplitTableLayoutPanel2);
			this.grpLot.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
			this.grpLot.Dock = System.Windows.Forms.DockStyle.Right;
			this.grpLot.GroupStyle = DevExpress.Utils.GroupStyle.Card;
			this.grpLot.LanguageKey = "MESSAGEINFO";
			this.grpLot.Location = new System.Drawing.Point(221, 45);
			this.grpLot.Name = "grpLot";
			this.grpLot.ShowBorder = true;
			this.grpLot.Size = new System.Drawing.Size(500, 283);
			this.grpLot.TabIndex = 7;
			this.grpLot.Text = "smartGroupBox2";
			// 
			// smartSplitTableLayoutPanel2
			// 
			this.smartSplitTableLayoutPanel2.ColumnCount = 1;
			this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.smartSplitTableLayoutPanel2.Controls.Add(this.ucMessageInfoLot, 0, 1);
			this.smartSplitTableLayoutPanel2.Controls.Add(this.popSendSegmentFrLot, 0, 0);
			this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(2, 31);
			this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
			this.smartSplitTableLayoutPanel2.RowCount = 2;
			this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(496, 250);
			this.smartSplitTableLayoutPanel2.TabIndex = 0;
			// 
			// ucMessageInfoLot
			// 
			this.ucMessageInfoLot.CheckedShowType = false;
			this.ucMessageInfoLot.CommentText = "{\\rtf1\\ansi\\ansicpg949\\deff0\\deflang1033\\deflangfe1042{\\fonttbl{\\f0\\fnil\\fcharset" +
    "204 Microsoft Sans Serif;}}\r\n\\viewkind4\\uc1\\pard\\lang1042\\f0\\fs18\\par\r\n}\r\n";
			this.ucMessageInfoLot.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ucMessageInfoLot.Location = new System.Drawing.Point(3, 29);
			this.ucMessageInfoLot.Name = "ucMessageInfoLot";
			this.ucMessageInfoLot.Size = new System.Drawing.Size(490, 218);
			this.ucMessageInfoLot.TabIndex = 0;
			this.ucMessageInfoLot.TitleText = "";
			// 
			// popSendSegmentFrLot
			// 
			this.popSendSegmentFrLot.Appearance.ForeColor = System.Drawing.Color.Red;
			this.popSendSegmentFrLot.Appearance.Options.UseForeColor = true;
			this.popSendSegmentFrLot.Dock = System.Windows.Forms.DockStyle.Fill;
			this.popSendSegmentFrLot.EditorWidth = "70%";
			this.popSendSegmentFrLot.LabelWidth = "30%";
			this.popSendSegmentFrLot.LanguageKey = "SENDSEGMENT";
			this.popSendSegmentFrLot.Location = new System.Drawing.Point(3, 3);
			this.popSendSegmentFrLot.Name = "popSendSegmentFrLot";
			this.popSendSegmentFrLot.Size = new System.Drawing.Size(490, 20);
			this.popSendSegmentFrLot.TabIndex = 2;
			// 
			// pnlUpDown
			// 
			this.pnlUpDown.Controls.Add(this.ucDataUpDownBtn);
			this.pnlUpDown.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlUpDown.Location = new System.Drawing.Point(0, 0);
			this.pnlUpDown.Name = "pnlUpDown";
			this.pnlUpDown.Size = new System.Drawing.Size(721, 45);
			this.pnlUpDown.TabIndex = 0;
			// 
			// ucDataUpDownBtn
			// 
			this.ucDataUpDownBtn.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ucDataUpDownBtn.Location = new System.Drawing.Point(0, 0);
			this.ucDataUpDownBtn.Name = "ucDataUpDownBtn";
			this.ucDataUpDownBtn.Size = new System.Drawing.Size(721, 45);
			this.ucDataUpDownBtn.SourceGrid = null;
			this.ucDataUpDownBtn.TabIndex = 0;
			this.ucDataUpDownBtn.TargetGrid = null;
			// 
			// LotMessage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1052, 712);
			this.Name = "LotMessage";
			this.Text = "SmartConditionBaseForm";
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
			this.pnlContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
			this.tabControl.ResumeLayout(false);
			this.tabPageProductDef.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grpProductDef)).EndInit();
			this.grpProductDef.ResumeLayout(false);
			this.smartSplitTableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlOptions)).EndInit();
			this.pnlOptions.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.popSendSegmentFrPd.Properties)).EndInit();
			this.tabPageSegment.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grpSegment)).EndInit();
			this.grpSegment.ResumeLayout(false);
			this.tabPageLot.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grpLot)).EndInit();
			this.grpLot.ResumeLayout(false);
			this.smartSplitTableLayoutPanel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.popSendSegmentFrLot.Properties)).EndInit();
			this.pnlUpDown.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Framework.SmartControls.SmartTabControl tabControl;
		private DevExpress.XtraTab.XtraTabPage tabPageProductDef;
		private DevExpress.XtraTab.XtraTabPage tabPageSegment;
		private DevExpress.XtraTab.XtraTabPage tabPageLot;
		private Framework.SmartControls.SmartBandedGrid grdWIP;
		private Framework.SmartControls.SmartSpliterControl spcSpliter;
		private System.Windows.Forms.Panel panel1;
		private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
		private System.Windows.Forms.Panel pnlUpDown;
		private Framework.SmartControls.SmartGroupBox grpLot;
		private Framework.SmartControls.SmartBandedGrid grdProductDefInfo;
		private Framework.SmartControls.SmartSpliterControl smartSpliterControl2;
		private Framework.SmartControls.SmartGroupBox grpProductDef;
		private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
		private ucMessageInfo ucMessageInfoProductDef;
		private Framework.SmartControls.SmartPanel pnlOptions;
		private Framework.SmartControls.SmartBandedGrid grdSegmentInfo;
		private Framework.SmartControls.SmartSpliterControl smartSpliterControl3;
		private Framework.SmartControls.SmartGroupBox grpSegment;
		private ucMessageInfo ucMessageInfoSegment;
		private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
		private ucMessageInfo ucMessageInfoLot;
		private Framework.SmartControls.SmartBandedGrid grdTargetWip;
		private Commons.Controls.ucDataUpDownBtn ucDataUpDownBtn;
		private Framework.SmartControls.SmartLabelSelectPopupEdit popSendSegmentFrPd;
		private Framework.SmartControls.SmartLabelSelectPopupEdit popSendSegmentFrLot;
	}
}