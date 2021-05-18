namespace Micube.SmartMES.ProcessManagement
{
	partial class PackingHistory
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
			this.grdLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
			this.grdPackingList = new Micube.Framework.SmartControls.SmartBandedGrid();
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
			this.pnlContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlCondition
			// 
			this.pnlCondition.Location = new System.Drawing.Point(2, 31);
			this.pnlCondition.Size = new System.Drawing.Size(296, 412);
			// 
			// pnlToolbar
			// 
			this.pnlToolbar.Size = new System.Drawing.Size(537, 24);
			// 
			// pnlContent
			// 
			this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
			this.pnlContent.Appearance.Options.UseBackColor = true;
			this.pnlContent.Controls.Add(this.grdPackingList);
			this.pnlContent.Controls.Add(this.smartSpliterControl1);
			this.pnlContent.Controls.Add(this.grdLotList);
			this.pnlContent.Size = new System.Drawing.Size(537, 416);
			// 
			// pnlMain
			// 
			this.pnlMain.Size = new System.Drawing.Size(842, 445);
			// 
			// grdLotList
			// 
			this.grdLotList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdLotList.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.grdLotList.IsUsePaging = false;
			this.grdLotList.LanguageKey = "GRIDLOTLIST";
			this.grdLotList.Location = new System.Drawing.Point(0, 91);
			this.grdLotList.Margin = new System.Windows.Forms.Padding(0);
			this.grdLotList.Name = "grdLotList";
			this.grdLotList.ShowBorder = true;
			this.grdLotList.Size = new System.Drawing.Size(537, 325);
			this.grdLotList.TabIndex = 0;
			// 
			// smartSpliterControl1
			// 
			this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.smartSpliterControl1.Location = new System.Drawing.Point(0, 86);
			this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSpliterControl1.Name = "smartSpliterControl1";
			this.smartSpliterControl1.Size = new System.Drawing.Size(537, 5);
			this.smartSpliterControl1.TabIndex = 1;
			this.smartSpliterControl1.TabStop = false;
			// 
			// grdPackingList
			// 
			this.grdPackingList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdPackingList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdPackingList.IsUsePaging = false;
			this.grdPackingList.LanguageKey = "PACKINGLOTLIST";
			this.grdPackingList.Location = new System.Drawing.Point(0, 0);
			this.grdPackingList.Margin = new System.Windows.Forms.Padding(0);
			this.grdPackingList.Name = "grdPackingList";
			this.grdPackingList.ShowBorder = true;
			this.grdPackingList.Size = new System.Drawing.Size(537, 86);
			this.grdPackingList.TabIndex = 2;
			// 
			// PackingHistory
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(862, 465);
			this.Name = "PackingHistory";
			this.Text = "SmartConditionBaseForm";
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
			this.pnlContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Framework.SmartControls.SmartBandedGrid grdLotList;
		private Framework.SmartControls.SmartBandedGrid grdPackingList;
		private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
	}
}