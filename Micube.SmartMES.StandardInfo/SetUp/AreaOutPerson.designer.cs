namespace Micube.SmartMES.StandardInfo
{
    partial class AreaOutPerson
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
            this.grdAreaPerson = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.smartGroupBox1 = new Micube.Framework.SmartControls.SmartGroupBox();
            this.treeParentArea = new Micube.Framework.SmartControls.SmartTreeList();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).BeginInit();
            this.smartGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeParentArea)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Size = new System.Drawing.Size(371, 516);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(790, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer1);
            this.pnlContent.Size = new System.Drawing.Size(790, 519);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1171, 555);
            // 
            // grdAreaPerson
            // 
            this.grdAreaPerson.Caption = "";
            this.grdAreaPerson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAreaPerson.IsUsePaging = false;
            this.grdAreaPerson.LanguageKey = null;
            this.grdAreaPerson.Location = new System.Drawing.Point(0, 0);
            this.grdAreaPerson.Margin = new System.Windows.Forms.Padding(0);
            this.grdAreaPerson.Name = "grdAreaPerson";
            this.grdAreaPerson.ShowBorder = true;
            this.grdAreaPerson.ShowStatusBar = false;
            this.grdAreaPerson.Size = new System.Drawing.Size(429, 519);
            this.grdAreaPerson.TabIndex = 0;
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.smartGroupBox1);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdAreaPerson);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(790, 519);
            this.smartSpliterContainer1.SplitterPosition = 355;
            this.smartSpliterContainer1.TabIndex = 2;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // smartGroupBox1
            // 
            this.smartGroupBox1.Controls.Add(this.treeParentArea);
            this.smartGroupBox1.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.smartGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartGroupBox1.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.smartGroupBox1.LanguageKey = "TREEAREALIST";
            this.smartGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.smartGroupBox1.Name = "smartGroupBox1";
            this.smartGroupBox1.ShowBorder = true;
            this.smartGroupBox1.Size = new System.Drawing.Size(355, 519);
            this.smartGroupBox1.TabIndex = 1;
            this.smartGroupBox1.Text = "smartGroupBox1";
            // 
            // treeParentArea
            // 
            this.treeParentArea.DisplayMember = null;
            this.treeParentArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeParentArea.LabelText = null;
            this.treeParentArea.LanguageKey = null;
            this.treeParentArea.Location = new System.Drawing.Point(2, 37);
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
            this.treeParentArea.Size = new System.Drawing.Size(351, 480);
            this.treeParentArea.TabIndex = 0;
            this.treeParentArea.ValueMember = "ID";
            this.treeParentArea.ValueNodeTypeFieldName = "Equipment";
            // 
            // AreaPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 593);
            this.Name = "AreaPerson";
            this.Padding = new System.Windows.Forms.Padding(19, 19, 19, 19);
            this.Text = "MasterDataClass";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).EndInit();
            this.smartGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeParentArea)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdAreaPerson;
        private Framework.SmartControls.SmartGroupBox smartGroupBox1;
        private Framework.SmartControls.SmartTreeList treeParentArea;
    }
}