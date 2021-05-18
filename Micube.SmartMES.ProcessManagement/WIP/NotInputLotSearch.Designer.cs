namespace Micube.SmartMES.ProcessManagement
{
	partial class NotInputLotSearch
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
            this.tabNotInputList = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgNotInputList = new DevExpress.XtraTab.XtraTabPage();
            this.grdNotInputList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgNotInputMaterialRequirement = new DevExpress.XtraTab.XtraTabPage();
            this.grdNotInputMaterialRequirement = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgNotInputSalesOrder = new DevExpress.XtraTab.XtraTabPage();
            this.grdNotInputSalesOrder = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.btnPrint = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabNotInputList)).BeginInit();
            this.tabNotInputList.SuspendLayout();
            this.tpgNotInputList.SuspendLayout();
            this.tpgNotInputMaterialRequirement.SuspendLayout();
            this.tpgNotInputSalesOrder.SuspendLayout();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 659);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.smartSplitTableLayoutPanel2);
            this.pnlToolbar.Size = new System.Drawing.Size(727, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.smartSplitTableLayoutPanel2, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabNotInputList);
            this.pnlContent.Size = new System.Drawing.Size(727, 663);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1032, 692);
            // 
            // tabNotInputList
            // 
            this.tabNotInputList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabNotInputList.Location = new System.Drawing.Point(0, 0);
            this.tabNotInputList.Name = "tabNotInputList";
            this.tabNotInputList.SelectedTabPage = this.tpgNotInputList;
            this.tabNotInputList.Size = new System.Drawing.Size(727, 663);
            this.tabNotInputList.TabIndex = 0;
            this.tabNotInputList.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgNotInputList,
            this.tpgNotInputMaterialRequirement,
            this.tpgNotInputSalesOrder});
            // 
            // tpgNotInputList
            // 
            this.tpgNotInputList.Controls.Add(this.grdNotInputList);
            this.tabNotInputList.SetLanguageKey(this.tpgNotInputList, "NOTINPUTLIST");
            this.tpgNotInputList.Name = "tpgNotInputList";
            this.tpgNotInputList.Size = new System.Drawing.Size(721, 634);
            this.tpgNotInputList.Text = "미투입 List";
            // 
            // grdNotInputList
            // 
            this.grdNotInputList.Caption = "";
            this.grdNotInputList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdNotInputList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdNotInputList.IsUsePaging = false;
            this.grdNotInputList.LanguageKey = "";
            this.grdNotInputList.Location = new System.Drawing.Point(0, 0);
            this.grdNotInputList.Margin = new System.Windows.Forms.Padding(0);
            this.grdNotInputList.Name = "grdNotInputList";
            this.grdNotInputList.ShowBorder = true;
            this.grdNotInputList.ShowStatusBar = false;
            this.grdNotInputList.Size = new System.Drawing.Size(721, 634);
            this.grdNotInputList.TabIndex = 2;
            // 
            // tpgNotInputMaterialRequirement
            // 
            this.tpgNotInputMaterialRequirement.Controls.Add(this.grdNotInputMaterialRequirement);
            this.tabNotInputList.SetLanguageKey(this.tpgNotInputMaterialRequirement, "NOTINPUTMATERIALREQUIREMENT");
            this.tpgNotInputMaterialRequirement.Name = "tpgNotInputMaterialRequirement";
            this.tpgNotInputMaterialRequirement.Size = new System.Drawing.Size(469, 372);
            this.tpgNotInputMaterialRequirement.Text = "미투입 자재 소요";
            // 
            // grdNotInputMaterialRequirement
            // 
            this.grdNotInputMaterialRequirement.Caption = "";
            this.grdNotInputMaterialRequirement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdNotInputMaterialRequirement.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdNotInputMaterialRequirement.IsUsePaging = false;
            this.grdNotInputMaterialRequirement.LanguageKey = "";
            this.grdNotInputMaterialRequirement.Location = new System.Drawing.Point(0, 0);
            this.grdNotInputMaterialRequirement.Margin = new System.Windows.Forms.Padding(0);
            this.grdNotInputMaterialRequirement.Name = "grdNotInputMaterialRequirement";
            this.grdNotInputMaterialRequirement.ShowBorder = true;
            this.grdNotInputMaterialRequirement.ShowStatusBar = false;
            this.grdNotInputMaterialRequirement.Size = new System.Drawing.Size(469, 372);
            this.grdNotInputMaterialRequirement.TabIndex = 3;
            // 
            // tpgNotInputSalesOrder
            // 
            this.tpgNotInputSalesOrder.Controls.Add(this.grdNotInputSalesOrder);
            this.tabNotInputList.SetLanguageKey(this.tpgNotInputSalesOrder, "NOTINPUTLISTSO");
            this.tpgNotInputSalesOrder.Name = "tpgNotInputSalesOrder";
            this.tpgNotInputSalesOrder.Size = new System.Drawing.Size(721, 634);
            this.tpgNotInputSalesOrder.Text = "미투입List(수주+품목)";
            // 
            // grdNotInputSalesOrder
            // 
            this.grdNotInputSalesOrder.Caption = "";
            this.grdNotInputSalesOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdNotInputSalesOrder.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdNotInputSalesOrder.IsUsePaging = false;
            this.grdNotInputSalesOrder.LanguageKey = "";
            this.grdNotInputSalesOrder.Location = new System.Drawing.Point(0, 0);
            this.grdNotInputSalesOrder.Margin = new System.Windows.Forms.Padding(0);
            this.grdNotInputSalesOrder.Name = "grdNotInputSalesOrder";
            this.grdNotInputSalesOrder.ShowBorder = true;
            this.grdNotInputSalesOrder.ShowStatusBar = false;
            this.grdNotInputSalesOrder.Size = new System.Drawing.Size(721, 634);
            this.grdNotInputSalesOrder.TabIndex = 3;
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.ColumnCount = 2;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.btnPrint, 3, 0);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(47, 0);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 1;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(680, 24);
            this.smartSplitTableLayoutPanel2.TabIndex = 7;
            // 
            // btnPrint
            // 
            this.btnPrint.AllowFocus = false;
            this.btnPrint.IsBusy = false;
            this.btnPrint.IsWrite = false;
            this.btnPrint.LanguageKey = "PRINT";
            this.btnPrint.Location = new System.Drawing.Point(598, 0);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "출력";
            this.btnPrint.TooltipLanguageKey = "";
            // 
            // NotInputLotSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 712);
            this.Name = "NotInputLotSearch";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabNotInputList)).EndInit();
            this.tabNotInputList.ResumeLayout(false);
            this.tpgNotInputList.ResumeLayout(false);
            this.tpgNotInputMaterialRequirement.ResumeLayout(false);
            this.tpgNotInputSalesOrder.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private Framework.SmartControls.SmartTabControl tabNotInputList;
		private DevExpress.XtraTab.XtraTabPage tpgNotInputList;
		private DevExpress.XtraTab.XtraTabPage tpgNotInputMaterialRequirement;
		private Framework.SmartControls.SmartBandedGrid grdNotInputList;
		private Framework.SmartControls.SmartBandedGrid grdNotInputMaterialRequirement;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
        private Framework.SmartControls.SmartButton btnPrint;
        private DevExpress.XtraTab.XtraTabPage tpgNotInputSalesOrder;
        private Framework.SmartControls.SmartBandedGrid grdNotInputSalesOrder;
    }
}
