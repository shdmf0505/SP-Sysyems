namespace Micube.SmartMES.ProcessManagement
{
	partial class ReturnConsumable
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
			this.grdConsumableList = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
			this.pnlPanel = new Micube.Framework.SmartControls.SmartPanel();
			this.ucDataUpDownBtnCtrl = new Micube.SmartMES.Commons.Controls.ucDataUpDownBtn();
			this.grdReturnConsumList = new Micube.Framework.SmartControls.SmartBandedGrid();
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
			this.pnlContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlPanel)).BeginInit();
			this.pnlPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlCondition
			// 
			this.pnlCondition.Location = new System.Drawing.Point(2, 31);
			this.pnlCondition.Size = new System.Drawing.Size(296, 556);
			// 
			// pnlToolbar
			// 
			this.pnlToolbar.Size = new System.Drawing.Size(658, 24);
			// 
			// pnlContent
			// 
			this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
			this.pnlContent.Appearance.Options.UseBackColor = true;
			this.pnlContent.Controls.Add(this.pnlPanel);
			this.pnlContent.Controls.Add(this.smartSpliterControl1);
			this.pnlContent.Controls.Add(this.grdConsumableList);
			this.pnlContent.Size = new System.Drawing.Size(658, 560);
			// 
			// pnlMain
			// 
			this.pnlMain.Size = new System.Drawing.Size(963, 589);
			// 
			// grdConsumableList
			// 
			this.grdConsumableList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdConsumableList.Dock = System.Windows.Forms.DockStyle.Top;
			this.grdConsumableList.IsUsePaging = false;
			this.grdConsumableList.LanguageKey = null;
			this.grdConsumableList.Location = new System.Drawing.Point(0, 0);
			this.grdConsumableList.Margin = new System.Windows.Forms.Padding(0);
			this.grdConsumableList.Name = "grdConsumableList";
			this.grdConsumableList.ShowBorder = true;
			this.grdConsumableList.Size = new System.Drawing.Size(658, 400);
			this.grdConsumableList.TabIndex = 0;
			// 
			// smartSpliterControl1
			// 
			this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smartSpliterControl1.Location = new System.Drawing.Point(0, 400);
			this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSpliterControl1.Name = "smartSpliterControl1";
			this.smartSpliterControl1.Size = new System.Drawing.Size(658, 5);
			this.smartSpliterControl1.TabIndex = 1;
			this.smartSpliterControl1.TabStop = false;
			// 
			// pnlPanel
			// 
			this.pnlPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pnlPanel.Controls.Add(this.grdReturnConsumList);
			this.pnlPanel.Controls.Add(this.ucDataUpDownBtnCtrl);
			this.pnlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlPanel.Location = new System.Drawing.Point(0, 405);
			this.pnlPanel.Name = "pnlPanel";
			this.pnlPanel.Size = new System.Drawing.Size(658, 155);
			this.pnlPanel.TabIndex = 2;
			// 
			// ucDataUpDownBtnCtrl
			// 
			this.ucDataUpDownBtnCtrl.Dock = System.Windows.Forms.DockStyle.Top;
			this.ucDataUpDownBtnCtrl.Location = new System.Drawing.Point(2, 2);
			this.ucDataUpDownBtnCtrl.Name = "ucDataUpDownBtnCtrl";
			this.ucDataUpDownBtnCtrl.Size = new System.Drawing.Size(654, 52);
			this.ucDataUpDownBtnCtrl.SourceGrid = null;
			this.ucDataUpDownBtnCtrl.TabIndex = 2;
			this.ucDataUpDownBtnCtrl.TargetGrid = null;
			// 
			// grdReturnConsumList
			// 
			this.grdReturnConsumList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdReturnConsumList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdReturnConsumList.IsUsePaging = false;
			this.grdReturnConsumList.LanguageKey = null;
			this.grdReturnConsumList.Location = new System.Drawing.Point(2, 54);
			this.grdReturnConsumList.Margin = new System.Windows.Forms.Padding(0);
			this.grdReturnConsumList.Name = "grdReturnConsumList";
			this.grdReturnConsumList.ShowBorder = true;
			this.grdReturnConsumList.Size = new System.Drawing.Size(654, 99);
			this.grdReturnConsumList.TabIndex = 3;
			// 
			// ReturnConsumable
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(983, 609);
			this.Name = "ReturnConsumable";
			this.Text = "SmartConditionBaseForm";
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
			this.pnlContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlPanel)).EndInit();
			this.pnlPanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Framework.SmartControls.SmartBandedGrid grdConsumableList;
		private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
		private Framework.SmartControls.SmartPanel pnlPanel;
		private Framework.SmartControls.SmartBandedGrid grdReturnConsumList;
		private Commons.Controls.ucDataUpDownBtn ucDataUpDownBtnCtrl;
	}
}