namespace Micube.SmartMES.StandardInfo
{
	partial class AreaManagement
	{
		/// <summary>
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 디자이너에서 생성한 코드

		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grpArea = new Micube.Framework.SmartControls.SmartGroupBox();
            this.treeParentArea = new Micube.Framework.SmartControls.SmartTreeList();
            this.smartSpliterContainer2 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdAreaList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdResourceList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tabArea = new Micube.Framework.SmartControls.SmartTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.grdAreaClass = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.grdResource = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpArea)).BeginInit();
            this.grpArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeParentArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).BeginInit();
            this.smartSpliterContainer2.SuspendLayout();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabArea)).BeginInit();
            this.tabArea.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            this.xtraTabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 508);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlToolbar.Size = new System.Drawing.Size(659, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.smartSplitTableLayoutPanel1, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabArea);
            this.pnlContent.Size = new System.Drawing.Size(659, 512);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(964, 541);
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 2;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(47, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 1;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(612, 24);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(5, 5);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grpArea);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.smartSpliterContainer2);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(643, 473);
            this.smartSpliterContainer1.SplitterPosition = 300;
            this.smartSpliterContainer1.TabIndex = 2;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grpArea
            // 
            this.grpArea.Controls.Add(this.treeParentArea);
            this.grpArea.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.grpArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpArea.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.grpArea.LanguageKey = "";
            this.grpArea.Location = new System.Drawing.Point(0, 0);
            this.grpArea.Name = "grpArea";
            this.grpArea.ShowBorder = true;
            this.grpArea.Size = new System.Drawing.Size(300, 473);
            this.grpArea.TabIndex = 0;
            this.grpArea.Text = "smartGroupBox1";
            // 
            // treeParentArea
            // 
            this.treeParentArea.DisplayMember = null;
            this.treeParentArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeParentArea.LabelText = null;
            this.treeParentArea.LanguageKey = null;
            this.treeParentArea.Location = new System.Drawing.Point(2, 31);
            this.treeParentArea.MaxHeight = 0;
            this.treeParentArea.Name = "treeParentArea";
            this.treeParentArea.NodeTypeFieldName = "NODETYPE";
            this.treeParentArea.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.treeParentArea.OptionsBehavior.AutoPopulateColumns = false;
            this.treeParentArea.OptionsBehavior.Editable = false;
            this.treeParentArea.OptionsFilter.AllowAutoFilterConditionChange = DevExpress.Utils.DefaultBoolean.False;
            this.treeParentArea.OptionsFilter.AllowColumnMRUFilterList = false;
            this.treeParentArea.OptionsFilter.AllowMRUFilterList = false;
            this.treeParentArea.OptionsFind.AlwaysVisible = true;
            this.treeParentArea.OptionsFind.ClearFindOnClose = false;
            this.treeParentArea.OptionsFind.FindMode = DevExpress.XtraTreeList.FindMode.FindClick;
            this.treeParentArea.OptionsFind.FindNullPrompt = "";
            this.treeParentArea.OptionsFind.ShowClearButton = false;
            this.treeParentArea.OptionsFind.ShowCloseButton = false;
            this.treeParentArea.OptionsFind.ShowFindButton = false;
            this.treeParentArea.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.treeParentArea.OptionsView.ShowColumns = false;
            this.treeParentArea.OptionsView.ShowHierarchyIndentationLines = DevExpress.Utils.DefaultBoolean.True;
            this.treeParentArea.OptionsView.ShowHorzLines = false;
            this.treeParentArea.OptionsView.ShowIndentAsRowStyle = true;
            this.treeParentArea.OptionsView.ShowIndicator = false;
            this.treeParentArea.OptionsView.ShowTreeLines = DevExpress.Utils.DefaultBoolean.True;
            this.treeParentArea.OptionsView.ShowVertLines = false;
            this.treeParentArea.ParentMember = "ParentID";
            this.treeParentArea.ResultIsLeafLevel = true;
            this.treeParentArea.Size = new System.Drawing.Size(296, 440);
            this.treeParentArea.TabIndex = 0;
            this.treeParentArea.ValueMember = "ID";
            this.treeParentArea.ValueNodeTypeFieldName = "Equipment";
            // 
            // smartSpliterContainer2
            // 
            this.smartSpliterContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer2.Horizontal = false;
            this.smartSpliterContainer2.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer2.Name = "smartSpliterContainer2";
            this.smartSpliterContainer2.Panel1.Controls.Add(this.grdAreaList);
            this.smartSpliterContainer2.Panel1.Text = "Panel1";
            this.smartSpliterContainer2.Panel2.Controls.Add(this.smartSplitTableLayoutPanel2);
            this.smartSpliterContainer2.Panel2.Text = "Panel2";
            this.smartSpliterContainer2.Size = new System.Drawing.Size(338, 473);
            this.smartSpliterContainer2.SplitterPosition = 325;
            this.smartSpliterContainer2.TabIndex = 0;
            this.smartSpliterContainer2.Text = "smartSpliterContainer2";
            // 
            // grdAreaList
            // 
            this.grdAreaList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdAreaList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAreaList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdAreaList.IsUsePaging = false;
            this.grdAreaList.LanguageKey = "";
            this.grdAreaList.Location = new System.Drawing.Point(0, 0);
            this.grdAreaList.Margin = new System.Windows.Forms.Padding(0);
            this.grdAreaList.Name = "grdAreaList";
            this.grdAreaList.ShowBorder = true;
            this.grdAreaList.ShowStatusBar = false;
            this.grdAreaList.Size = new System.Drawing.Size(338, 325);
            this.grdAreaList.TabIndex = 1;
            this.grdAreaList.UseAutoBestFitColumns = false;
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.ColumnCount = 1;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdResourceList, 0, 1);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 2;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(338, 143);
            this.smartSplitTableLayoutPanel2.TabIndex = 1;
            // 
            // grdResourceList
            // 
            this.grdResourceList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdResourceList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdResourceList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdResourceList.IsUsePaging = false;
            this.grdResourceList.LanguageKey = "RESOURCEINFOLIST";
            this.grdResourceList.Location = new System.Drawing.Point(0, 0);
            this.grdResourceList.Margin = new System.Windows.Forms.Padding(0);
            this.grdResourceList.Name = "grdResourceList";
            this.grdResourceList.ShowBorder = true;
            this.grdResourceList.ShowStatusBar = false;
            this.grdResourceList.Size = new System.Drawing.Size(338, 143);
            this.grdResourceList.TabIndex = 0;
            this.grdResourceList.UseAutoBestFitColumns = false;
            // 
            // tabArea
            // 
            this.tabArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabArea.Location = new System.Drawing.Point(0, 0);
            this.tabArea.Name = "tabArea";
            this.tabArea.SelectedTabPage = this.xtraTabPage1;
            this.tabArea.Size = new System.Drawing.Size(659, 512);
            this.tabArea.TabIndex = 3;
            this.tabArea.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.smartSpliterContainer1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Padding = new System.Windows.Forms.Padding(5);
            this.xtraTabPage1.Size = new System.Drawing.Size(653, 483);
            this.xtraTabPage1.Text = "xtraTabPage1";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.grdAreaClass);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Padding = new System.Windows.Forms.Padding(5);
            this.xtraTabPage2.Size = new System.Drawing.Size(653, 483);
            this.xtraTabPage2.Text = "xtraTabPage2";
            // 
            // grdAreaClass
            // 
            this.grdAreaClass.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdAreaClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAreaClass.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdAreaClass.IsUsePaging = false;
            this.grdAreaClass.LanguageKey = "";
            this.grdAreaClass.Location = new System.Drawing.Point(5, 5);
            this.grdAreaClass.Margin = new System.Windows.Forms.Padding(0);
            this.grdAreaClass.Name = "grdAreaClass";
            this.grdAreaClass.ShowBorder = true;
            this.grdAreaClass.ShowStatusBar = false;
            this.grdAreaClass.Size = new System.Drawing.Size(643, 473);
            this.grdAreaClass.TabIndex = 1;
            this.grdAreaClass.UseAutoBestFitColumns = false;
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.grdResource);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Padding = new System.Windows.Forms.Padding(5);
            this.xtraTabPage3.Size = new System.Drawing.Size(653, 483);
            this.xtraTabPage3.Text = "xtraTabPage3";
            // 
            // grdResource
            // 
            this.grdResource.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdResource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdResource.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdResource.IsUsePaging = false;
            this.grdResource.LanguageKey = "";
            this.grdResource.Location = new System.Drawing.Point(5, 5);
            this.grdResource.Margin = new System.Windows.Forms.Padding(0);
            this.grdResource.Name = "grdResource";
            this.grdResource.ShowBorder = true;
            this.grdResource.ShowStatusBar = false;
            this.grdResource.Size = new System.Drawing.Size(643, 473);
            this.grdResource.TabIndex = 0;
            this.grdResource.UseAutoBestFitColumns = false;
            // 
            // AreaManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Name = "AreaManagement";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpArea)).EndInit();
            this.grpArea.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeParentArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).EndInit();
            this.smartSpliterContainer2.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabArea)).EndInit();
            this.tabArea.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartGroupBox grpArea;
        private Framework.SmartControls.SmartTreeList treeParentArea;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer2;
        private Framework.SmartControls.SmartBandedGrid grdAreaList;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
        private Framework.SmartControls.SmartBandedGrid grdResourceList;
        private Framework.SmartControls.SmartTabControl tabArea;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private Framework.SmartControls.SmartBandedGrid grdAreaClass;
        private Framework.SmartControls.SmartBandedGrid grdResource;
    }
}

