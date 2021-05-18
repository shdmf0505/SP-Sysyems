namespace Micube.SmartMES.ProcessManagement
{
	partial class LotWorkAreaChange
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
			this.grdWIP = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.spcSpliter = new Micube.Framework.SmartControls.SmartSpliterControl();
			this.panel1 = new System.Windows.Forms.Panel();
			this.grdTransArea = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
			this.smartGroupBox1 = new Micube.Framework.SmartControls.SmartGroupBox();
			this.cboTargetArea = new Micube.Framework.SmartControls.SmartLabelComboBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.ucDataUpDownBtn = new Micube.SmartMES.Commons.Controls.ucDataUpDownBtn();
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
			this.pnlContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).BeginInit();
			this.smartGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cboTargetArea.Properties)).BeginInit();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlCondition
			// 
			this.pnlCondition.Location = new System.Drawing.Point(2, 31);
			this.pnlCondition.Size = new System.Drawing.Size(296, 583);
			// 
			// pnlToolbar
			// 
			this.pnlToolbar.Size = new System.Drawing.Size(652, 24);
			// 
			// pnlContent
			// 
			this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
			this.pnlContent.Appearance.Options.UseBackColor = true;
			this.pnlContent.Controls.Add(this.grdWIP);
			this.pnlContent.Controls.Add(this.spcSpliter);
			this.pnlContent.Controls.Add(this.panel1);
			this.pnlContent.Size = new System.Drawing.Size(652, 587);
			// 
			// pnlMain
			// 
			this.pnlMain.Size = new System.Drawing.Size(957, 616);
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
			this.grdWIP.Size = new System.Drawing.Size(652, 254);
			this.grdWIP.TabIndex = 2;
			// 
			// spcSpliter
			// 
			this.spcSpliter.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.spcSpliter.Location = new System.Drawing.Point(0, 254);
			this.spcSpliter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.spcSpliter.Name = "spcSpliter";
			this.spcSpliter.Size = new System.Drawing.Size(652, 5);
			this.spcSpliter.TabIndex = 6;
			this.spcSpliter.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.grdTransArea);
			this.panel1.Controls.Add(this.smartSpliterControl1);
			this.panel1.Controls.Add(this.smartGroupBox1);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 259);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(652, 328);
			this.panel1.TabIndex = 7;
			// 
			// grdTransArea
			// 
			this.grdTransArea.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdTransArea.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdTransArea.IsUsePaging = false;
			this.grdTransArea.LanguageKey = "TARGETWIP";
			this.grdTransArea.Location = new System.Drawing.Point(0, 45);
			this.grdTransArea.Margin = new System.Windows.Forms.Padding(0);
			this.grdTransArea.Name = "grdTransArea";
			this.grdTransArea.ShowBorder = true;
			this.grdTransArea.Size = new System.Drawing.Size(330, 283);
			this.grdTransArea.TabIndex = 2;
			// 
			// smartSpliterControl1
			// 
			this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Right;
			this.smartSpliterControl1.Location = new System.Drawing.Point(330, 45);
			this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSpliterControl1.Name = "smartSpliterControl1";
			this.smartSpliterControl1.Size = new System.Drawing.Size(5, 283);
			this.smartSpliterControl1.TabIndex = 6;
			this.smartSpliterControl1.TabStop = false;
			// 
			// smartGroupBox1
			// 
			this.smartGroupBox1.Controls.Add(this.cboTargetArea);
			this.smartGroupBox1.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
			this.smartGroupBox1.Dock = System.Windows.Forms.DockStyle.Right;
			this.smartGroupBox1.GroupStyle = DevExpress.Utils.GroupStyle.Card;
			this.smartGroupBox1.LanguageKey = "TRANSAREA";
			this.smartGroupBox1.Location = new System.Drawing.Point(335, 45);
			this.smartGroupBox1.Name = "smartGroupBox1";
			this.smartGroupBox1.ShowBorder = true;
			this.smartGroupBox1.Size = new System.Drawing.Size(317, 283);
			this.smartGroupBox1.TabIndex = 0;
			this.smartGroupBox1.Text = "작업장 변경";
			// 
			// cboTargetArea
			// 
			this.cboTargetArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboTargetArea.LabelText = "대상자원";
			this.cboTargetArea.LabelWidth = "20%";
			this.cboTargetArea.LanguageKey = "TARGETRESOURCE";
			this.cboTargetArea.Location = new System.Drawing.Point(5, 34);
			this.cboTargetArea.Name = "cboTargetArea";
			this.cboTargetArea.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.cboTargetArea.Properties.NullText = "";
			this.cboTargetArea.Size = new System.Drawing.Size(307, 20);
			this.cboTargetArea.TabIndex = 1;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.ucDataUpDownBtn);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(652, 45);
			this.panel2.TabIndex = 0;
			// 
			// ucDataUpDownBtn
			// 
			this.ucDataUpDownBtn.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ucDataUpDownBtn.Location = new System.Drawing.Point(0, 0);
			this.ucDataUpDownBtn.Name = "ucDataUpDownBtn";
			this.ucDataUpDownBtn.Size = new System.Drawing.Size(652, 45);
			this.ucDataUpDownBtn.SourceGrid = null;
			this.ucDataUpDownBtn.TabIndex = 0;
			this.ucDataUpDownBtn.TargetGrid = null;
			// 
			// LotWorkAreaChange
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(977, 636);
			this.Name = "LotWorkAreaChange";
			this.Text = "SmartConditionBaseForm";
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
			this.pnlContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).EndInit();
			this.smartGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.cboTargetArea.Properties)).EndInit();
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Framework.SmartControls.SmartBandedGrid grdWIP;
		private Framework.SmartControls.SmartSpliterControl spcSpliter;
		private System.Windows.Forms.Panel panel1;
		private Framework.SmartControls.SmartBandedGrid grdTransArea;
		private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
		private Framework.SmartControls.SmartGroupBox smartGroupBox1;
		private Framework.SmartControls.SmartLabelComboBox cboTargetArea;
		private System.Windows.Forms.Panel panel2;
		private Commons.Controls.ucDataUpDownBtn ucDataUpDownBtn;
	}
}