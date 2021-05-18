namespace Micube.SmartMES.StandardInfo
{
    partial class InspItemManagement
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
            this.tpgInspectionMethod = new DevExpress.XtraTab.XtraTabPage();
            this.grdInspectionMethodCode = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgInspectionItem = new DevExpress.XtraTab.XtraTabPage();
            this.grdInspectionItem = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tabInspItem1 = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgInspectionMethodItem = new DevExpress.XtraTab.XtraTabPage();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.trvInspectionMethod = new Micube.Framework.SmartControls.SmartTreeList();
            this.smartSpliterContainer2 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdInspectionMethod = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdInspectionMethodItem = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.tpgInspectionMethod.SuspendLayout();
            this.tpgInspectionItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabInspItem1)).BeginInit();
            this.tabInspItem1.SuspendLayout();
            this.tpgInspectionMethodItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trvInspectionMethod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).BeginInit();
            this.smartSpliterContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Size = new System.Drawing.Size(371, 585);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(1136, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabInspItem1);
            this.pnlContent.Size = new System.Drawing.Size(1136, 588);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1517, 624);
            // 
            // tpgInspectionMethod
            // 
            this.tpgInspectionMethod.Controls.Add(this.grdInspectionMethodCode);
            this.tabInspItem1.SetLanguageKey(this.tpgInspectionMethod, "REGISTINSPMETHOD");
            this.tpgInspectionMethod.Margin = new System.Windows.Forms.Padding(0);
            this.tpgInspectionMethod.Name = "tpgInspectionMethod";
            this.tpgInspectionMethod.Padding = new System.Windows.Forms.Padding(10);
            this.tpgInspectionMethod.Size = new System.Drawing.Size(824, 592);
            this.tpgInspectionMethod.Text = "InspectionMethod";
            // 
            // grdInspectionMethodCode
            // 
            this.grdInspectionMethodCode.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdInspectionMethodCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInspectionMethodCode.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInspectionMethodCode.IsUsePaging = false;
            this.grdInspectionMethodCode.LanguageKey = "INSPMETHODS";
            this.grdInspectionMethodCode.Location = new System.Drawing.Point(10, 10);
            this.grdInspectionMethodCode.Margin = new System.Windows.Forms.Padding(0);
            this.grdInspectionMethodCode.Name = "grdInspectionMethodCode";
            this.grdInspectionMethodCode.ShowBorder = true;
            this.grdInspectionMethodCode.ShowStatusBar = false;
            this.grdInspectionMethodCode.Size = new System.Drawing.Size(804, 572);
            this.grdInspectionMethodCode.TabIndex = 1;
            // 
            // tpgInspectionItem
            // 
            this.tpgInspectionItem.Controls.Add(this.grdInspectionItem);
            this.tabInspItem1.SetLanguageKey(this.tpgInspectionItem, "REGISTINSPITEM");
            this.tpgInspectionItem.Margin = new System.Windows.Forms.Padding(0);
            this.tpgInspectionItem.Name = "tpgInspectionItem";
            this.tpgInspectionItem.Size = new System.Drawing.Size(1129, 552);
            this.tpgInspectionItem.Text = "InspectionItem";
            // 
            // grdInspectionItem
            // 
            this.grdInspectionItem.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdInspectionItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInspectionItem.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInspectionItem.IsUsePaging = false;
            this.grdInspectionItem.LanguageKey = "INSPITEMS";
            this.grdInspectionItem.Location = new System.Drawing.Point(0, 0);
            this.grdInspectionItem.Margin = new System.Windows.Forms.Padding(0);
            this.grdInspectionItem.Name = "grdInspectionItem";
            this.grdInspectionItem.ShowBorder = false;
            this.grdInspectionItem.ShowStatusBar = false;
            this.grdInspectionItem.Size = new System.Drawing.Size(1129, 552);
            this.grdInspectionItem.TabIndex = 0;
            // 
            // tabInspItem1
            // 
            this.tabInspItem1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tabInspItem1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabInspItem1.Location = new System.Drawing.Point(0, 0);
            this.tabInspItem1.Margin = new System.Windows.Forms.Padding(0);
            this.tabInspItem1.Name = "tabInspItem1";
            this.tabInspItem1.SelectedTabPage = this.tpgInspectionItem;
            this.tabInspItem1.Size = new System.Drawing.Size(1136, 588);
            this.tabInspItem1.TabIndex = 0;
            this.tabInspItem1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgInspectionItem,
            this.tpgInspectionMethod,
            this.tpgInspectionMethodItem});
            // 
            // tpgInspectionMethodItem
            // 
            this.tpgInspectionMethodItem.Controls.Add(this.smartSpliterContainer1);
            this.tabInspItem1.SetLanguageKey(this.tpgInspectionMethodItem, "REGISTINSPMETHODITEM");
            this.tpgInspectionMethodItem.Name = "tpgInspectionMethodItem";
            this.tpgInspectionMethodItem.Size = new System.Drawing.Size(1129, 552);
            this.tpgInspectionMethodItem.Text = "InspectionMethodItem";
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.trvInspectionMethod);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.smartSpliterContainer2);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(1129, 552);
            this.smartSpliterContainer1.SplitterPosition = 301;
            this.smartSpliterContainer1.TabIndex = 0;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // trvInspectionMethod
            // 
            this.trvInspectionMethod.DisplayMember = null;
            this.trvInspectionMethod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvInspectionMethod.LabelText = null;
            this.trvInspectionMethod.LanguageKey = null;
            this.trvInspectionMethod.Location = new System.Drawing.Point(0, 0);
            this.trvInspectionMethod.MaxHeight = 0;
            this.trvInspectionMethod.Name = "trvInspectionMethod";
            this.trvInspectionMethod.NodeTypeFieldName = "NODETYPE";
            this.trvInspectionMethod.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.trvInspectionMethod.OptionsBehavior.AutoPopulateColumns = false;
            this.trvInspectionMethod.OptionsBehavior.Editable = false;
            this.trvInspectionMethod.OptionsFilter.AllowAutoFilterConditionChange = DevExpress.Utils.DefaultBoolean.False;
            this.trvInspectionMethod.OptionsFilter.AllowColumnMRUFilterList = false;
            this.trvInspectionMethod.OptionsFilter.AllowMRUFilterList = false;
            this.trvInspectionMethod.OptionsFind.AlwaysVisible = true;
            this.trvInspectionMethod.OptionsFind.ClearFindOnClose = false;
            this.trvInspectionMethod.OptionsFind.FindMode = DevExpress.XtraTreeList.FindMode.FindClick;
            this.trvInspectionMethod.OptionsFind.FindNullPrompt = "";
            this.trvInspectionMethod.OptionsFind.ShowClearButton = false;
            this.trvInspectionMethod.OptionsFind.ShowCloseButton = false;
            this.trvInspectionMethod.OptionsFind.ShowFindButton = false;
            this.trvInspectionMethod.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.trvInspectionMethod.OptionsView.ShowColumns = false;
            this.trvInspectionMethod.OptionsView.ShowHierarchyIndentationLines = DevExpress.Utils.DefaultBoolean.True;
            this.trvInspectionMethod.OptionsView.ShowHorzLines = false;
            this.trvInspectionMethod.OptionsView.ShowIndentAsRowStyle = true;
            this.trvInspectionMethod.OptionsView.ShowIndicator = false;
            this.trvInspectionMethod.OptionsView.ShowTreeLines = DevExpress.Utils.DefaultBoolean.True;
            this.trvInspectionMethod.OptionsView.ShowVertLines = false;
            this.trvInspectionMethod.ParentMember = "ParentID";
            this.trvInspectionMethod.ResultIsLeafLevel = true;
            this.trvInspectionMethod.Size = new System.Drawing.Size(301, 552);
            this.trvInspectionMethod.TabIndex = 0;
            this.trvInspectionMethod.ValueMember = "ID";
            this.trvInspectionMethod.ValueNodeTypeFieldName = "Equipment";
            // 
            // smartSpliterContainer2
            // 
            this.smartSpliterContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer2.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer2.Name = "smartSpliterContainer2";
            this.smartSpliterContainer2.Panel1.Controls.Add(this.grdInspectionMethod);
            this.smartSpliterContainer2.Panel1.Text = "Panel1";
            this.smartSpliterContainer2.Panel2.Controls.Add(this.grdInspectionMethodItem);
            this.smartSpliterContainer2.Panel2.Text = "Panel2";
            this.smartSpliterContainer2.Size = new System.Drawing.Size(822, 552);
            this.smartSpliterContainer2.SplitterPosition = 476;
            this.smartSpliterContainer2.TabIndex = 0;
            this.smartSpliterContainer2.Text = "smartSpliterContainer2";
            // 
            // grdInspectionMethod
            // 
            this.grdInspectionMethod.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdInspectionMethod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInspectionMethod.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInspectionMethod.IsUsePaging = false;
            this.grdInspectionMethod.LanguageKey = "INSPMETHODS";
            this.grdInspectionMethod.Location = new System.Drawing.Point(0, 0);
            this.grdInspectionMethod.Margin = new System.Windows.Forms.Padding(0);
            this.grdInspectionMethod.Name = "grdInspectionMethod";
            this.grdInspectionMethod.ShowBorder = true;
            this.grdInspectionMethod.ShowStatusBar = false;
            this.grdInspectionMethod.Size = new System.Drawing.Size(476, 552);
            this.grdInspectionMethod.TabIndex = 2;
            // 
            // grdInspectionMethodItem
            // 
            this.grdInspectionMethodItem.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdInspectionMethodItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInspectionMethodItem.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInspectionMethodItem.IsUsePaging = false;
            this.grdInspectionMethodItem.LanguageKey = "INSPITEMS";
            this.grdInspectionMethodItem.Location = new System.Drawing.Point(0, 0);
            this.grdInspectionMethodItem.Margin = new System.Windows.Forms.Padding(0);
            this.grdInspectionMethodItem.Name = "grdInspectionMethodItem";
            this.grdInspectionMethodItem.ShowBorder = true;
            this.grdInspectionMethodItem.ShowStatusBar = false;
            this.grdInspectionMethodItem.Size = new System.Drawing.Size(340, 552);
            this.grdInspectionMethodItem.TabIndex = 2;
            // 
            // InspItemManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1555, 662);
            this.LanguageKey = "INSPECTIONCLASS";
            this.Name = "InspItemManagement";
            this.Padding = new System.Windows.Forms.Padding(19, 19, 19, 19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.tpgInspectionMethod.ResumeLayout(false);
            this.tpgInspectionItem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabInspItem1)).EndInit();
            this.tabInspItem1.ResumeLayout(false);
            this.tpgInspectionMethodItem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trvInspectionMethod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).EndInit();
            this.smartSpliterContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabInspItem1;
        private DevExpress.XtraTab.XtraTabPage tpgInspectionItem;
        private Framework.SmartControls.SmartBandedGrid grdInspectionItem;
        private DevExpress.XtraTab.XtraTabPage tpgInspectionMethod;
        private DevExpress.XtraTab.XtraTabPage tpgInspectionMethodItem;
        private Framework.SmartControls.SmartBandedGrid grdInspectionMethodCode;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartTreeList trvInspectionMethod;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer2;
        private Framework.SmartControls.SmartBandedGrid grdInspectionMethod;
        private Framework.SmartControls.SmartBandedGrid grdInspectionMethodItem;
    }
}