namespace Micube.SmartMES.StandardInfo
{
	partial class Category
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
			this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
			this.grbCategory = new Micube.Framework.SmartControls.SmartGroupBox();
			this.treeCategory = new Micube.Framework.SmartControls.SmartTreeList();
			this.grdCategoryList = new Micube.Framework.SmartControls.SmartBandedGrid();
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
			this.pnlContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
			this.smartSpliterContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grbCategory)).BeginInit();
			this.grbCategory.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.treeCategory)).BeginInit();
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
			this.pnlContent.Controls.Add(this.smartSpliterContainer1);
			// 
			// smartSpliterContainer1
			// 
			this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
			this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSpliterContainer1.Name = "smartSpliterContainer1";
			this.smartSpliterContainer1.Panel1.Controls.Add(this.grbCategory);
			this.smartSpliterContainer1.Panel1.Text = "Panel1";
			this.smartSpliterContainer1.Panel2.Controls.Add(this.grdCategoryList);
			this.smartSpliterContainer1.Panel2.Text = "Panel2";
			this.smartSpliterContainer1.Size = new System.Drawing.Size(475, 401);
			this.smartSpliterContainer1.SplitterPosition = 300;
			this.smartSpliterContainer1.TabIndex = 0;
			this.smartSpliterContainer1.Text = "smartSpliterContainer1";
			// 
			// grbCategory
			// 
			this.grbCategory.Controls.Add(this.treeCategory);
			this.grbCategory.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
			this.grbCategory.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grbCategory.GroupStyle = DevExpress.Utils.GroupStyle.Card;
			this.grbCategory.LanguageKey = "CATEGORYTYPE";
			this.grbCategory.Location = new System.Drawing.Point(0, 0);
			this.grbCategory.Name = "grbCategory";
			this.grbCategory.ShowBorder = true;
			this.grbCategory.Size = new System.Drawing.Size(300, 401);
			this.grbCategory.TabIndex = 0;
			// 
			// treeCategory
			// 
			this.treeCategory.DisplayMember = null;
			this.treeCategory.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeCategory.LabelText = null;
			this.treeCategory.LanguageKey = null;
			this.treeCategory.Location = new System.Drawing.Point(2, 31);
			this.treeCategory.MaxHeight = 0;
			this.treeCategory.Name = "treeCategory";
			this.treeCategory.NodeTypeFieldName = "NODETYPE";
			this.treeCategory.OptionsBehavior.AllowRecursiveNodeChecking = true;
			this.treeCategory.OptionsBehavior.AutoPopulateColumns = false;
			this.treeCategory.OptionsBehavior.Editable = false;
			this.treeCategory.OptionsFilter.AllowAutoFilterConditionChange = DevExpress.Utils.DefaultBoolean.False;
			this.treeCategory.OptionsFilter.AllowColumnMRUFilterList = false;
			this.treeCategory.OptionsFilter.AllowMRUFilterList = false;
			this.treeCategory.OptionsFind.AlwaysVisible = true;
			this.treeCategory.OptionsFind.ClearFindOnClose = false;
			this.treeCategory.OptionsFind.FindMode = DevExpress.XtraTreeList.FindMode.FindClick;
			this.treeCategory.OptionsFind.FindNullPrompt = "";
			this.treeCategory.OptionsFind.ShowClearButton = false;
			this.treeCategory.OptionsFind.ShowCloseButton = false;
			this.treeCategory.OptionsFind.ShowFindButton = false;
			this.treeCategory.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.treeCategory.OptionsView.ShowColumns = false;
			this.treeCategory.OptionsView.ShowHierarchyIndentationLines = DevExpress.Utils.DefaultBoolean.True;
			this.treeCategory.OptionsView.ShowHorzLines = false;
			this.treeCategory.OptionsView.ShowIndentAsRowStyle = true;
			this.treeCategory.OptionsView.ShowIndicator = false;
			this.treeCategory.OptionsView.ShowTreeLines = DevExpress.Utils.DefaultBoolean.True;
			this.treeCategory.OptionsView.ShowVertLines = false;
			this.treeCategory.ParentMember = "ParentID";
			this.treeCategory.ResultIsLeafLevel = true;
			this.treeCategory.Size = new System.Drawing.Size(296, 368);
			this.treeCategory.TabIndex = 0;
			this.treeCategory.ValueMember = "ID";
			this.treeCategory.ValueNodeTypeFieldName = "Equipment";
			// 
			// grdCategoryList
			// 
			this.grdCategoryList.Caption = "";
			this.grdCategoryList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdCategoryList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
			this.grdCategoryList.IsUsePaging = false;
			this.grdCategoryList.LanguageKey = "CATEGORYLIST";
			this.grdCategoryList.Location = new System.Drawing.Point(0, 0);
			this.grdCategoryList.Margin = new System.Windows.Forms.Padding(0);
			this.grdCategoryList.Name = "grdCategoryList";
			this.grdCategoryList.ShowBorder = true;
			this.grdCategoryList.Size = new System.Drawing.Size(170, 401);
			this.grdCategoryList.TabIndex = 0;
			// 
			// Category
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Name = "Category";
			this.Text = "SmartConditionBaseForm";
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
			this.pnlContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
			this.smartSpliterContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grbCategory)).EndInit();
			this.grbCategory.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.treeCategory)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
		private Framework.SmartControls.SmartGroupBox grbCategory;
		private Framework.SmartControls.SmartTreeList treeCategory;
		private Framework.SmartControls.SmartBandedGrid grdCategoryList;
	}
}