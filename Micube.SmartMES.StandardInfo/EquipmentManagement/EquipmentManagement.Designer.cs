namespace Micube.SmartMES.StandardInfo
{
	partial class EquipmentManagement
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
            this.grdEquipmentList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartTabControl1 = new Micube.Framework.SmartControls.SmartTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.treeEquipmentClass = new Micube.Framework.SmartControls.SmartTreeList();
            this.smartSpliterContainer2 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdEquipmentClass = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterContainer3 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdEquipment = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.picbox = new Micube.Framework.SmartControls.SmartPictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartTabControl1)).BeginInit();
            this.smartTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeEquipmentClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).BeginInit();
            this.smartSpliterContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer3)).BeginInit();
            this.smartSpliterContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbox.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 638);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(772, 24);
            this.pnlToolbar.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlToolbar_Paint);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartTabControl1);
            this.pnlContent.Size = new System.Drawing.Size(772, 642);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1077, 671);
            // 
            // grdEquipmentList
            // 
            this.grdEquipmentList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdEquipmentList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdEquipmentList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdEquipmentList.IsUsePaging = false;
            this.grdEquipmentList.LanguageKey = "EQUIPMENTINFOLIST";
            this.grdEquipmentList.Location = new System.Drawing.Point(0, 0);
            this.grdEquipmentList.Margin = new System.Windows.Forms.Padding(0);
            this.grdEquipmentList.Name = "grdEquipmentList";
            this.grdEquipmentList.ShowBorder = true;
            this.grdEquipmentList.ShowStatusBar = false;
            this.grdEquipmentList.Size = new System.Drawing.Size(766, 613);
            this.grdEquipmentList.TabIndex = 0;
            this.grdEquipmentList.UseAutoBestFitColumns = false;
            // 
            // smartTabControl1
            // 
            this.smartTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartTabControl1.Location = new System.Drawing.Point(0, 0);
            this.smartTabControl1.Name = "smartTabControl1";
            this.smartTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.smartTabControl1.Size = new System.Drawing.Size(772, 642);
            this.smartTabControl1.TabIndex = 1;
            this.smartTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.grdEquipmentList);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(766, 613);
            this.xtraTabPage1.Text = "xtraTabPage1";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.smartSpliterContainer1);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(766, 613);
            this.xtraTabPage2.Text = "xtraTabPage2";
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.treeEquipmentClass);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.smartSpliterContainer2);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(766, 613);
            this.smartSpliterContainer1.SplitterPosition = 289;
            this.smartSpliterContainer1.TabIndex = 0;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // treeEquipmentClass
            // 
            this.treeEquipmentClass.DisplayMember = null;
            this.treeEquipmentClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeEquipmentClass.LabelText = null;
            this.treeEquipmentClass.LanguageKey = null;
            this.treeEquipmentClass.Location = new System.Drawing.Point(0, 0);
            this.treeEquipmentClass.MaxHeight = 0;
            this.treeEquipmentClass.Name = "treeEquipmentClass";
            this.treeEquipmentClass.NodeTypeFieldName = "NODETYPE";
            this.treeEquipmentClass.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.treeEquipmentClass.OptionsBehavior.AutoPopulateColumns = false;
            this.treeEquipmentClass.OptionsBehavior.Editable = false;
            this.treeEquipmentClass.OptionsFilter.AllowAutoFilterConditionChange = DevExpress.Utils.DefaultBoolean.False;
            this.treeEquipmentClass.OptionsFilter.AllowColumnMRUFilterList = false;
            this.treeEquipmentClass.OptionsFilter.AllowMRUFilterList = false;
            this.treeEquipmentClass.OptionsFind.AlwaysVisible = true;
            this.treeEquipmentClass.OptionsFind.ClearFindOnClose = false;
            this.treeEquipmentClass.OptionsFind.FindMode = DevExpress.XtraTreeList.FindMode.FindClick;
            this.treeEquipmentClass.OptionsFind.FindNullPrompt = "";
            this.treeEquipmentClass.OptionsFind.ShowClearButton = false;
            this.treeEquipmentClass.OptionsFind.ShowCloseButton = false;
            this.treeEquipmentClass.OptionsFind.ShowFindButton = false;
            this.treeEquipmentClass.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.treeEquipmentClass.OptionsView.ShowColumns = false;
            this.treeEquipmentClass.OptionsView.ShowHierarchyIndentationLines = DevExpress.Utils.DefaultBoolean.True;
            this.treeEquipmentClass.OptionsView.ShowHorzLines = false;
            this.treeEquipmentClass.OptionsView.ShowIndentAsRowStyle = true;
            this.treeEquipmentClass.OptionsView.ShowIndicator = false;
            this.treeEquipmentClass.OptionsView.ShowTreeLines = DevExpress.Utils.DefaultBoolean.True;
            this.treeEquipmentClass.OptionsView.ShowVertLines = false;
            this.treeEquipmentClass.ParentMember = "ParentID";
            this.treeEquipmentClass.ResultIsLeafLevel = true;
            this.treeEquipmentClass.Size = new System.Drawing.Size(289, 613);
            this.treeEquipmentClass.TabIndex = 0;
            this.treeEquipmentClass.ValueMember = "ID";
            this.treeEquipmentClass.ValueNodeTypeFieldName = "Equipment";
            // 
            // smartSpliterContainer2
            // 
            this.smartSpliterContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer2.Horizontal = false;
            this.smartSpliterContainer2.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer2.Name = "smartSpliterContainer2";
            this.smartSpliterContainer2.Panel1.Controls.Add(this.grdEquipmentClass);
            this.smartSpliterContainer2.Panel1.Text = "Panel1";
            this.smartSpliterContainer2.Panel2.Controls.Add(this.smartSpliterContainer3);
            this.smartSpliterContainer2.Panel2.Text = "Panel2";
            this.smartSpliterContainer2.Size = new System.Drawing.Size(472, 613);
            this.smartSpliterContainer2.SplitterPosition = 380;
            this.smartSpliterContainer2.TabIndex = 0;
            this.smartSpliterContainer2.Text = "smartSpliterContainer2";
            // 
            // grdEquipmentClass
            // 
            this.grdEquipmentClass.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdEquipmentClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdEquipmentClass.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdEquipmentClass.IsUsePaging = false;
            this.grdEquipmentClass.LanguageKey = "EQUIPMENT";
            this.grdEquipmentClass.Location = new System.Drawing.Point(0, 0);
            this.grdEquipmentClass.Margin = new System.Windows.Forms.Padding(0);
            this.grdEquipmentClass.Name = "grdEquipmentClass";
            this.grdEquipmentClass.ShowBorder = true;
            this.grdEquipmentClass.ShowStatusBar = false;
            this.grdEquipmentClass.Size = new System.Drawing.Size(472, 380);
            this.grdEquipmentClass.TabIndex = 0;
            this.grdEquipmentClass.UseAutoBestFitColumns = false;
            // 
            // smartSpliterContainer3
            // 
            this.smartSpliterContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer3.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer3.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer3.Name = "smartSpliterContainer3";
            this.smartSpliterContainer3.Panel1.Controls.Add(this.grdEquipment);
            this.smartSpliterContainer3.Panel1.Text = "Panel1";
            this.smartSpliterContainer3.Panel2.Controls.Add(this.picbox);
            this.smartSpliterContainer3.Panel2.Text = "Panel2";
            this.smartSpliterContainer3.Size = new System.Drawing.Size(472, 228);
            this.smartSpliterContainer3.SplitterPosition = 857;
            this.smartSpliterContainer3.TabIndex = 0;
            this.smartSpliterContainer3.Text = "smartSpliterContainer3";
            // 
            // grdEquipment
            // 
            this.grdEquipment.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdEquipment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdEquipment.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdEquipment.IsUsePaging = false;
            this.grdEquipment.LanguageKey = "CHILDEQUIPMENT";
            this.grdEquipment.Location = new System.Drawing.Point(0, 0);
            this.grdEquipment.Margin = new System.Windows.Forms.Padding(0);
            this.grdEquipment.Name = "grdEquipment";
            this.grdEquipment.ShowBorder = true;
            this.grdEquipment.ShowStatusBar = false;
            this.grdEquipment.Size = new System.Drawing.Size(467, 228);
            this.grdEquipment.TabIndex = 0;
            this.grdEquipment.UseAutoBestFitColumns = false;
            // 
            // picbox
            // 
            this.picbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picbox.Location = new System.Drawing.Point(0, 0);
            this.picbox.Name = "picbox";
            this.picbox.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picbox.Size = new System.Drawing.Size(0, 0);
            this.picbox.TabIndex = 0;
            // 
            // EquipmentManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 709);
            this.Name = "EquipmentManagement";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "EquipmentManagement";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartTabControl1)).EndInit();
            this.smartTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeEquipmentClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).EndInit();
            this.smartSpliterContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer3)).EndInit();
            this.smartSpliterContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picbox.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private Framework.SmartControls.SmartBandedGrid grdEquipmentList;
        private Framework.SmartControls.SmartTabControl smartTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartTreeList treeEquipmentClass;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer2;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer3;
        private Framework.SmartControls.SmartBandedGrid grdEquipment;
        private Framework.SmartControls.SmartPictureEdit picbox;
        private Framework.SmartControls.SmartBandedGrid grdEquipmentClass;
    }
}