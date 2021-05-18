namespace Micube.SmartMES.ProcessManagement
{
	partial class WIPPerDaily
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
            this.tabPageByProcessSegment = new DevExpress.XtraTab.XtraTabPage();
            this.grdByProcess = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tabPageByProductDef = new DevExpress.XtraTab.XtraTabPage();
            this.grdByProduct = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPageByProcessSegment.SuspendLayout();
            this.tabPageByProductDef.SuspendLayout();
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
            this.tabControl.SelectedTabPage = this.tabPageByProcessSegment;
            this.tabControl.Size = new System.Drawing.Size(727, 663);
            this.tabControl.TabIndex = 0;
            this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageByProcessSegment,
            this.tabPageByProductDef});
            // 
            // tabPageByProcessSegment
            // 
            this.tabPageByProcessSegment.Controls.Add(this.grdByProcess);
            this.tabControl.SetLanguageKey(this.tabPageByProcessSegment, "PROCESSSEGMENT");
            this.tabPageByProcessSegment.Name = "tabPageByProcessSegment";
            this.tabPageByProcessSegment.Size = new System.Drawing.Size(721, 634);
            this.tabPageByProcessSegment.Text = "공정별";
            // 
            // grdByProcess
            // 
            this.grdByProcess.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdByProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdByProcess.IsUsePaging = false;
            this.grdByProcess.LanguageKey = "RESULTANDSTOCKBYPROCESS";
            this.grdByProcess.Location = new System.Drawing.Point(0, 0);
            this.grdByProcess.Margin = new System.Windows.Forms.Padding(0);
            this.grdByProcess.Name = "grdByProcess";
            this.grdByProcess.ShowBorder = true;
            this.grdByProcess.ShowStatusBar = false;
            this.grdByProcess.Size = new System.Drawing.Size(721, 634);
            this.grdByProcess.TabIndex = 2;
            // 
            // tabPageByProductDef
            // 
            this.tabPageByProductDef.Controls.Add(this.grdByProduct);
            this.tabControl.SetLanguageKey(this.tabPageByProductDef, "PRODUCTDEF");
            this.tabPageByProductDef.Name = "tabPageByProductDef";
            this.tabPageByProductDef.Size = new System.Drawing.Size(721, 634);
            this.tabPageByProductDef.Text = "품목별";
            // 
            // grdByProduct
            // 
            this.grdByProduct.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdByProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdByProduct.IsUsePaging = false;
            this.grdByProduct.LanguageKey = "RESULTANDSTOCKBYPRODUCT";
            this.grdByProduct.Location = new System.Drawing.Point(0, 0);
            this.grdByProduct.Margin = new System.Windows.Forms.Padding(0);
            this.grdByProduct.Name = "grdByProduct";
            this.grdByProduct.ShowBorder = true;
            this.grdByProduct.ShowStatusBar = false;
            this.grdByProduct.Size = new System.Drawing.Size(721, 634);
            this.grdByProduct.TabIndex = 3;
            // 
            // WIPPerDaily
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 712);
            this.Name = "WIPPerDaily";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPageByProcessSegment.ResumeLayout(false);
            this.tabPageByProductDef.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private Framework.SmartControls.SmartTabControl tabControl;
		private DevExpress.XtraTab.XtraTabPage tabPageByProcessSegment;
		private DevExpress.XtraTab.XtraTabPage tabPageByProductDef;
		private Framework.SmartControls.SmartBandedGrid grdByProcess;
		private Framework.SmartControls.SmartBandedGrid grdByProduct;
	}
}
