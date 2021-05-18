namespace Micube.SmartMES.StandardInfo
{
    partial class EquipmentClassManagement
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
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grpBox = new Micube.Framework.SmartControls.SmartGroupBox();
            this.treeEquipmentClass = new Micube.Framework.SmartControls.SmartTreeList();
            this.grdEquipmentClass = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpBox)).BeginInit();
            this.grpBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeEquipmentClass)).BeginInit();
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
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grpBox);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdEquipmentClass);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(475, 401);
            this.smartSpliterContainer1.SplitterPosition = 300;
            this.smartSpliterContainer1.TabIndex = 2;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grpBox
            // 
            this.grpBox.Controls.Add(this.treeEquipmentClass);
            this.grpBox.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.grpBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBox.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.grpBox.LanguageKey = "EQUIPMENTCLASSTREE";
            this.grpBox.Location = new System.Drawing.Point(0, 0);
            this.grpBox.Name = "grpBox";
            this.grpBox.ShowBorder = true;
            this.grpBox.Size = new System.Drawing.Size(300, 401);
            this.grpBox.TabIndex = 0;
            this.grpBox.Text = "smartGroupBox1";
            // 
            // treeEquipmentClass
            // 
            this.treeEquipmentClass.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.treeEquipmentClass.DisplayMember = null;
            this.treeEquipmentClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeEquipmentClass.LabelText = null;
            this.treeEquipmentClass.LanguageKey = "EQUIPMENTCLASS";
            this.treeEquipmentClass.Location = new System.Drawing.Point(2, 31);
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
            this.treeEquipmentClass.Size = new System.Drawing.Size(296, 368);
            this.treeEquipmentClass.TabIndex = 0;
            this.treeEquipmentClass.ValueMember = "ID";
            this.treeEquipmentClass.ValueNodeTypeFieldName = "Equipment";
            // 
            // grdEquipmentClass
            // 
            this.grdEquipmentClass.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdEquipmentClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdEquipmentClass.IsUsePaging = false;
            this.grdEquipmentClass.LanguageKey = "EQUIPMENTCLASSINFOLIST";
            this.grdEquipmentClass.Location = new System.Drawing.Point(0, 0);
            this.grdEquipmentClass.Margin = new System.Windows.Forms.Padding(0);
            this.grdEquipmentClass.Name = "grdEquipmentClass";
            this.grdEquipmentClass.ShowBorder = true;
            this.grdEquipmentClass.ShowStatusBar = false;
            this.grdEquipmentClass.Size = new System.Drawing.Size(170, 401);
            this.grdEquipmentClass.TabIndex = 0;
            // 
            // EquipmentClassManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "EquipmentClassManagement";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpBox)).EndInit();
            this.grpBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeEquipmentClass)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartGroupBox grpBox;
        private Framework.SmartControls.SmartTreeList treeEquipmentClass;
        private Framework.SmartControls.SmartBandedGrid grdEquipmentClass;
    }
}