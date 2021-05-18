namespace Micube.SmartMES.SystemManagement
{
    partial class Manual
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
            this.grdManual = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.smartGroupBox1 = new Micube.Framework.SmartControls.SmartGroupBox();
            this.treeManual = new Micube.Framework.SmartControls.SmartTreeList();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).BeginInit();
            this.smartGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeManual)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 397);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(600, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer1);
            this.pnlContent.Size = new System.Drawing.Size(600, 401);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(905, 430);
            // 
            // grdManual
            // 
            this.grdManual.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdManual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdManual.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdManual.IsUsePaging = false;
            this.grdManual.LanguageKey = "GRIDMENULIST";
            this.grdManual.Location = new System.Drawing.Point(0, 0);
            this.grdManual.Margin = new System.Windows.Forms.Padding(0);
            this.grdManual.Name = "grdManual";
            this.grdManual.ShowBorder = true;
            this.grdManual.Size = new System.Drawing.Size(295, 401);
            this.grdManual.TabIndex = 1;
            this.grdManual.UseAutoBestFitColumns = false;
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.smartGroupBox1);
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdManual);
            this.smartSpliterContainer1.Size = new System.Drawing.Size(600, 401);
            this.smartSpliterContainer1.SplitterPosition = 300;
            this.smartSpliterContainer1.TabIndex = 1;
            // 
            // smartGroupBox1
            // 
            this.smartGroupBox1.Controls.Add(this.treeManual);
            this.smartGroupBox1.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.smartGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartGroupBox1.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.smartGroupBox1.LanguageKey = "TREEMENULIST";
            this.smartGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.smartGroupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.smartGroupBox1.Name = "smartGroupBox1";
            this.smartGroupBox1.ShowBorder = true;
            this.smartGroupBox1.Size = new System.Drawing.Size(300, 401);
            this.smartGroupBox1.TabIndex = 2;
            this.smartGroupBox1.Text = "smartGroupBox1";
            // 
            // treeManual
            // 
            this.treeManual.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.treeManual.DisplayMember = null;
            this.treeManual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeManual.LabelText = null;
            this.treeManual.LanguageKey = null;
            this.treeManual.Location = new System.Drawing.Point(2, 31);
            this.treeManual.Margin = new System.Windows.Forms.Padding(0);
            this.treeManual.MaxHeight = 0;
            this.treeManual.Name = "treeManual";
            this.treeManual.NodeTypeFieldName = "NODETYPE";
            this.treeManual.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.treeManual.OptionsBehavior.AutoPopulateColumns = false;
            this.treeManual.OptionsBehavior.Editable = false;
            this.treeManual.OptionsFilter.AllowAutoFilterConditionChange = DevExpress.Utils.DefaultBoolean.False;
            this.treeManual.OptionsFilter.AllowColumnMRUFilterList = false;
            this.treeManual.OptionsFilter.AllowMRUFilterList = false;
            this.treeManual.OptionsFind.AlwaysVisible = true;
            this.treeManual.OptionsFind.ClearFindOnClose = false;
            this.treeManual.OptionsFind.FindMode = DevExpress.XtraTreeList.FindMode.FindClick;
            this.treeManual.OptionsFind.FindNullPrompt = "";
            this.treeManual.OptionsFind.ShowClearButton = false;
            this.treeManual.OptionsFind.ShowCloseButton = false;
            this.treeManual.OptionsFind.ShowFindButton = false;
            this.treeManual.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.treeManual.OptionsView.ShowColumns = false;
            this.treeManual.OptionsView.ShowHierarchyIndentationLines = DevExpress.Utils.DefaultBoolean.True;
            this.treeManual.OptionsView.ShowHorzLines = false;
            this.treeManual.OptionsView.ShowIndentAsRowStyle = true;
            this.treeManual.OptionsView.ShowIndicator = false;
            this.treeManual.OptionsView.ShowTreeLines = DevExpress.Utils.DefaultBoolean.True;
            this.treeManual.OptionsView.ShowVertLines = false;
            this.treeManual.ParentMember = "ParentID";
            this.treeManual.ResultIsLeafLevel = true;
            this.treeManual.Size = new System.Drawing.Size(296, 368);
            this.treeManual.TabIndex = 0;
            this.treeManual.ValueMember = "ID";
            this.treeManual.ValueNodeTypeFieldName = "Equipment";
            // 
            // Manual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 450);
            this.Name = "Manual";
            this.Text = "Manual";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).EndInit();
            this.smartGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeManual)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Framework.SmartControls.SmartBandedGrid grdManual;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartGroupBox smartGroupBox1;
        private Framework.SmartControls.SmartTreeList treeManual;
    }
}
