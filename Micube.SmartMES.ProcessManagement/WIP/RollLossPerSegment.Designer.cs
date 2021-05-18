namespace Micube.SmartMES.ProcessManagement
{
    partial class RollLossPerSegment
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
			this.itemtab = new Micube.Framework.SmartControls.SmartTabControl();
			this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
			this.grdRollLossByProduct = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
			this.grdRollLossBySegment = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.lblProductStd = new Micube.Framework.SmartControls.SmartLabel();
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
			this.pnlContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.itemtab)).BeginInit();
			this.itemtab.SuspendLayout();
			this.xtraTabPage1.SuspendLayout();
			this.xtraTabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlCondition
			// 
			this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
			this.pnlCondition.Size = new System.Drawing.Size(296, 379);
			// 
			// pnlToolbar
			// 
			this.pnlToolbar.Size = new System.Drawing.Size(457, 24);
			// 
			// pnlContent
			// 
			this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
			this.pnlContent.Appearance.Options.UseBackColor = true;
			this.pnlContent.Controls.Add(this.itemtab);
			this.pnlContent.Size = new System.Drawing.Size(457, 383);
			// 
			// pnlMain
			// 
			this.pnlMain.Location = new System.Drawing.Point(19, 19);
			this.pnlMain.Size = new System.Drawing.Size(762, 412);
			// 
			// itemtab
			// 
			this.itemtab.Dock = System.Windows.Forms.DockStyle.Fill;
			this.itemtab.Location = new System.Drawing.Point(0, 0);
			this.itemtab.Name = "itemtab";
			this.itemtab.SelectedTabPage = this.xtraTabPage1;
			this.itemtab.Size = new System.Drawing.Size(457, 383);
			this.itemtab.TabIndex = 1;
			this.itemtab.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
			// 
			// xtraTabPage1
			// 
			this.xtraTabPage1.Controls.Add(this.lblProductStd);
			this.xtraTabPage1.Controls.Add(this.grdRollLossByProduct);
			this.itemtab.SetLanguageKey(this.xtraTabPage1, "ITEM");
			this.xtraTabPage1.Name = "xtraTabPage1";
			this.xtraTabPage1.Size = new System.Drawing.Size(451, 354);
			this.xtraTabPage1.Text = "xtraTabPage1";
			// 
			// grdRollLossByProduct
			// 
			this.grdRollLossByProduct.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdRollLossByProduct.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdRollLossByProduct.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
			this.grdRollLossByProduct.IsUsePaging = false;
			this.grdRollLossByProduct.LanguageKey = "ROLLLOSSPERPRODUCT";
			this.grdRollLossByProduct.Location = new System.Drawing.Point(0, 0);
			this.grdRollLossByProduct.Margin = new System.Windows.Forms.Padding(0);
			this.grdRollLossByProduct.Name = "grdRollLossByProduct";
			this.grdRollLossByProduct.ShowBorder = true;
			this.grdRollLossByProduct.ShowStatusBar = false;
			this.grdRollLossByProduct.Size = new System.Drawing.Size(451, 354);
			this.grdRollLossByProduct.TabIndex = 2;
			this.grdRollLossByProduct.UseAutoBestFitColumns = false;
			// 
			// xtraTabPage2
			// 
			this.xtraTabPage2.Controls.Add(this.grdRollLossBySegment);
			this.itemtab.SetLanguageKey(this.xtraTabPage2, "PROCESSCHANGETYPE");
			this.xtraTabPage2.Name = "xtraTabPage2";
			this.xtraTabPage2.Size = new System.Drawing.Size(750, 460);
			this.xtraTabPage2.Text = "xtraTabPage2";
			// 
			// grdRollLossBySegment
			// 
			this.grdRollLossBySegment.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdRollLossBySegment.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdRollLossBySegment.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
			this.grdRollLossBySegment.IsUsePaging = false;
			this.grdRollLossBySegment.LanguageKey = "ROLLLOSSPERSEGMENT";
			this.grdRollLossBySegment.Location = new System.Drawing.Point(0, 0);
			this.grdRollLossBySegment.Margin = new System.Windows.Forms.Padding(0);
			this.grdRollLossBySegment.Name = "grdRollLossBySegment";
			this.grdRollLossBySegment.ShowBorder = true;
			this.grdRollLossBySegment.ShowStatusBar = false;
			this.grdRollLossBySegment.Size = new System.Drawing.Size(750, 460);
			this.grdRollLossBySegment.TabIndex = 1;
			this.grdRollLossBySegment.UseAutoBestFitColumns = false;
			// 
			// lblProductStd
			// 
			this.lblProductStd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblProductStd.Appearance.BackColor = System.Drawing.Color.Ivory;
			this.lblProductStd.Appearance.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblProductStd.Appearance.ForeColor = System.Drawing.Color.Red;
			this.lblProductStd.Appearance.Options.UseBackColor = true;
			this.lblProductStd.Appearance.Options.UseFont = true;
			this.lblProductStd.Appearance.Options.UseForeColor = true;
			this.lblProductStd.Appearance.Options.UseTextOptions = true;
			this.lblProductStd.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.lblProductStd.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.lblProductStd.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
			this.lblProductStd.LanguageKey = "FINISHROLLCUT";
			this.lblProductStd.Location = new System.Drawing.Point(302, 4);
			this.lblProductStd.Name = "lblProductStd";
			this.lblProductStd.Size = new System.Drawing.Size(110, 20);
			this.lblProductStd.TabIndex = 2;
			this.lblProductStd.Text = "투입 완료 기준";
			// 
			// RollLossPerSegment
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Name = "RollLossPerSegment";
			this.Padding = new System.Windows.Forms.Padding(19);
			this.Text = "SmartConditionBaseForm";
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
			this.pnlContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.itemtab)).EndInit();
			this.itemtab.ResumeLayout(false);
			this.xtraTabPage1.ResumeLayout(false);
			this.xtraTabPage2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private Framework.SmartControls.SmartTabControl itemtab;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
		private Framework.SmartControls.SmartBandedGrid grdRollLossByProduct;
		private Framework.SmartControls.SmartBandedGrid grdRollLossBySegment;
		private Framework.SmartControls.SmartLabel lblProductStd;
	}
}