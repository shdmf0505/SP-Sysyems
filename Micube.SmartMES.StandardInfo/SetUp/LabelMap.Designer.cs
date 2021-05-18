﻿namespace Micube.SmartMES.StandardInfo
{
	partial class LabelMap
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
			this.grdLabelMapList = new Micube.Framework.SmartControls.SmartBandedGrid();
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
			this.pnlContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlCondition
			// 
			this.pnlCondition.Location = new System.Drawing.Point(2, 31);
			this.pnlCondition.Size = new System.Drawing.Size(296, 397);
			// 
			// pnlContent
			// 
			this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
			this.pnlContent.Appearance.Options.UseBackColor = true;
			this.pnlContent.Controls.Add(this.grdLabelMapList);
			// 
			// grdLabelMapList
			// 
			this.grdLabelMapList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdLabelMapList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdLabelMapList.IsUsePaging = false;
			this.grdLabelMapList.LanguageKey = "LABELMAPLIST";
			this.grdLabelMapList.Location = new System.Drawing.Point(0, 0);
			this.grdLabelMapList.Margin = new System.Windows.Forms.Padding(0);
			this.grdLabelMapList.Name = "grdLabelMapList";
			this.grdLabelMapList.ShowBorder = true;
			this.grdLabelMapList.Size = new System.Drawing.Size(475, 401);
			this.grdLabelMapList.TabIndex = 0;
			// 
			// LabelMap
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Name = "LabelMap";
			this.Text = "SmartConditionBaseForm";
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
			this.pnlContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Framework.SmartControls.SmartBandedGrid grdLabelMapList;
	}
}