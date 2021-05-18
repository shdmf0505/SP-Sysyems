namespace Micube.SmartMES.StandardInfo
{
	partial class LabelMaster
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
            this.tabPartition = new Micube.Framework.SmartControls.SmartTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.grdLabelClassList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.labelDesigner1 = new Micube.SmartMES.Commons.Controls.LabelDesigner();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.grdLabelDefList = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPartition)).BeginInit();
            this.tabPartition.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Size = new System.Drawing.Size(371, 685);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(742, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabPartition);
            this.pnlContent.Size = new System.Drawing.Size(742, 688);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1123, 724);
            // 
            // tabPartition
            // 
            this.tabPartition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPartition.Location = new System.Drawing.Point(0, 0);
            this.tabPartition.Name = "tabPartition";
            this.tabPartition.SelectedTabPage = this.xtraTabPage1;
            this.tabPartition.Size = new System.Drawing.Size(742, 688);
            this.tabPartition.TabIndex = 0;
            this.tabPartition.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.grdLabelClassList);
            this.tabPartition.SetLanguageKey(this.xtraTabPage1, "LABELGROUP");
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(735, 652);
            this.xtraTabPage1.Text = "라벨 그룹";
            // 
            // grdLabelClassList
            // 
            this.grdLabelClassList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLabelClassList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLabelClassList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLabelClassList.IsUsePaging = false;
            this.grdLabelClassList.LanguageKey = "LABELCLASSLIST";
            this.grdLabelClassList.Location = new System.Drawing.Point(0, 0);
            this.grdLabelClassList.Margin = new System.Windows.Forms.Padding(0);
            this.grdLabelClassList.Name = "grdLabelClassList";
            this.grdLabelClassList.ShowBorder = true;
            this.grdLabelClassList.ShowStatusBar = false;
            this.grdLabelClassList.Size = new System.Drawing.Size(735, 652);
            this.grdLabelClassList.TabIndex = 0;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.labelDesigner1);
            this.xtraTabPage2.Controls.Add(this.splitter1);
            this.xtraTabPage2.Controls.Add(this.grdLabelDefList);
            this.tabPartition.SetLanguageKey(this.xtraTabPage2, "LABELFORM");
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(824, 592);
            this.xtraTabPage2.Text = "라벨 양식";
            // 
            // labelDesigner1
            // 
            this.labelDesigner1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelDesigner1.IsOpenButtonEnable = false;
            this.labelDesigner1.LabelInfoTable = null;
            this.labelDesigner1.Location = new System.Drawing.Point(0, 253);
            this.labelDesigner1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelDesigner1.Name = "labelDesigner1";
            this.labelDesigner1.PrevFocusedRowHandle = 0;
            this.labelDesigner1.Size = new System.Drawing.Size(824, 339);
            this.labelDesigner1.TabIndex = 2;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 250);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(824, 3);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // grdLabelDefList
            // 
            this.grdLabelDefList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLabelDefList.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdLabelDefList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLabelDefList.IsUsePaging = false;
            this.grdLabelDefList.LanguageKey = "LABELDEFINITIONLIST";
            this.grdLabelDefList.Location = new System.Drawing.Point(0, 0);
            this.grdLabelDefList.Margin = new System.Windows.Forms.Padding(0);
            this.grdLabelDefList.Name = "grdLabelDefList";
            this.grdLabelDefList.ShowBorder = true;
            this.grdLabelDefList.ShowStatusBar = false;
            this.grdLabelDefList.Size = new System.Drawing.Size(824, 250);
            this.grdLabelDefList.TabIndex = 0;
            // 
            // LabelMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 762);
            this.Name = "LabelMaster";
            this.Padding = new System.Windows.Forms.Padding(19, 19, 19, 19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPartition)).EndInit();
            this.tabPartition.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private Framework.SmartControls.SmartTabControl tabPartition;
		private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
		private Framework.SmartControls.SmartBandedGrid grdLabelClassList;
		private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
		private Commons.Controls.LabelDesigner labelDesigner1;
		private System.Windows.Forms.Splitter splitter1;
		private Framework.SmartControls.SmartBandedGrid grdLabelDefList;
    }
}