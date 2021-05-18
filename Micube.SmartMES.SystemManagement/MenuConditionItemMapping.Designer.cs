namespace Micube.SmartMES.SystemManagement
{
    partial class MenuConditionItemMapping
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuConditionItemMapping));
            this.treeMenuList = new Micube.Framework.SmartControls.SmartTreeList();
            this.grdMappingList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel2 = new Micube.Framework.SmartControls.SmartPanel();
            this.btnSelectItemAdd = new Micube.Framework.SmartControls.SmartButton();
            this.btnSelectItemDelete = new Micube.Framework.SmartControls.SmartButton();
            this.grdNotMappingList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.smartGroupBox1 = new Micube.Framework.SmartControls.SmartGroupBox();
            this.smartSpliterContainer2 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeMenuList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).BeginInit();
            this.smartPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).BeginInit();
            this.smartGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).BeginInit();
            this.smartSpliterContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Size = new System.Drawing.Size(296, 992);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(1574, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer1);
            this.pnlContent.Size = new System.Drawing.Size(1574, 987);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1884, 1021);
            // 
            // treeMenuList
            // 
            this.treeMenuList.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.treeMenuList.DisplayMember = null;
            this.treeMenuList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeMenuList.KeyFieldName = "MENUID";
            this.treeMenuList.LabelText = null;
            this.treeMenuList.LanguageKey = null;
            this.treeMenuList.Location = new System.Drawing.Point(2, 27);
            this.treeMenuList.Margin = new System.Windows.Forms.Padding(0);
            this.treeMenuList.MaxHeight = 0;
            this.treeMenuList.Name = "treeMenuList";
            this.treeMenuList.NodeTypeFieldName = "NODETYPE";
            this.treeMenuList.OptionsFind.AlwaysVisible = true;
            this.treeMenuList.OptionsFind.ClearFindOnClose = false;
            this.treeMenuList.OptionsFind.FindMode = DevExpress.XtraTreeList.FindMode.FindClick;
            this.treeMenuList.OptionsFind.FindNullPrompt = "";
            this.treeMenuList.OptionsFind.ShowClearButton = false;
            this.treeMenuList.OptionsFind.ShowCloseButton = false;
            this.treeMenuList.OptionsFind.ShowFindButton = false;
            this.treeMenuList.ParentFieldName = "PARENTMENUID";
            this.treeMenuList.ParentMember = "PARENTMENUID";
            this.treeMenuList.ResultIsLeafLevel = false;
            this.treeMenuList.Size = new System.Drawing.Size(346, 958);
            this.treeMenuList.TabIndex = 0;
            this.treeMenuList.ValueMember = "MENUID";
            this.treeMenuList.ValueNodeTypeFieldName = "Equipment";
            // 
            // grdMappingList
            // 
            this.grdMappingList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdMappingList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMappingList.IsUsePaging = false;
            this.grdMappingList.LanguageKey = "GRIDMENUCONDITIONITEMMAPPINGLIST";
            this.grdMappingList.Location = new System.Drawing.Point(0, 40);
            this.grdMappingList.Margin = new System.Windows.Forms.Padding(0);
            this.grdMappingList.Name = "grdMappingList";
            this.grdMappingList.ShowBorder = true;
            this.grdMappingList.Size = new System.Drawing.Size(1214, 537);
            this.grdMappingList.TabIndex = 3;
            // 
            // smartPanel2
            // 
            this.smartPanel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel2.Controls.Add(this.btnSelectItemAdd);
            this.smartPanel2.Controls.Add(this.btnSelectItemDelete);
            this.smartPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartPanel2.Location = new System.Drawing.Point(0, 0);
            this.smartPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel2.Name = "smartPanel2";
            this.smartPanel2.Size = new System.Drawing.Size(1214, 40);
            this.smartPanel2.TabIndex = 2;
            // 
            // btnSelectItemAdd
            // 
            this.btnSelectItemAdd.AllowFocus = false;
            this.btnSelectItemAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectItemAdd.ImageOptions.Image")));
            this.btnSelectItemAdd.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnSelectItemAdd.IsBusy = false;
            this.btnSelectItemAdd.Location = new System.Drawing.Point(10, 5);
            this.btnSelectItemAdd.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSelectItemAdd.Name = "btnSelectItemAdd";
            this.btnSelectItemAdd.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSelectItemAdd.Size = new System.Drawing.Size(30, 30);
            this.btnSelectItemAdd.TabIndex = 0;
            this.btnSelectItemAdd.TooltipLanguageKey = "";
            // 
            // btnSelectItemDelete
            // 
            this.btnSelectItemDelete.AllowFocus = false;
            this.btnSelectItemDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectItemDelete.ImageOptions.Image")));
            this.btnSelectItemDelete.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnSelectItemDelete.IsBusy = false;
            this.btnSelectItemDelete.Location = new System.Drawing.Point(50, 5);
            this.btnSelectItemDelete.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSelectItemDelete.Name = "btnSelectItemDelete";
            this.btnSelectItemDelete.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSelectItemDelete.Size = new System.Drawing.Size(30, 30);
            this.btnSelectItemDelete.TabIndex = 1;
            this.btnSelectItemDelete.TooltipLanguageKey = "";
            // 
            // grdNotMappingList
            // 
            this.grdNotMappingList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdNotMappingList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdNotMappingList.IsUsePaging = false;
            this.grdNotMappingList.LanguageKey = "GRIDMENUCONDITIONITEMNOTMAPPINGLIST";
            this.grdNotMappingList.Location = new System.Drawing.Point(0, 0);
            this.grdNotMappingList.Margin = new System.Windows.Forms.Padding(0);
            this.grdNotMappingList.Name = "grdNotMappingList";
            this.grdNotMappingList.ShowBorder = true;
            this.grdNotMappingList.Size = new System.Drawing.Size(1214, 400);
            this.grdNotMappingList.TabIndex = 0;
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.smartGroupBox1);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.smartSpliterContainer2);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(1574, 987);
            this.smartSpliterContainer1.SplitterPosition = 350;
            this.smartSpliterContainer1.TabIndex = 1;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // smartGroupBox1
            // 
            this.smartGroupBox1.Controls.Add(this.treeMenuList);
            this.smartGroupBox1.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.smartGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartGroupBox1.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.smartGroupBox1.LanguageKey = "TREEMENULIST";
            this.smartGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.smartGroupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.smartGroupBox1.Name = "smartGroupBox1";
            this.smartGroupBox1.ShowBorder = true;
            this.smartGroupBox1.Size = new System.Drawing.Size(350, 987);
            this.smartGroupBox1.TabIndex = 1;
            this.smartGroupBox1.Text = "smartGroupBox1";
            // 
            // smartSpliterContainer2
            // 
            this.smartSpliterContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer2.Horizontal = false;
            this.smartSpliterContainer2.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer2.Name = "smartSpliterContainer2";
            this.smartSpliterContainer2.Panel1.Controls.Add(this.grdNotMappingList);
            this.smartSpliterContainer2.Panel1.Text = "Panel1";
            this.smartSpliterContainer2.Panel2.Controls.Add(this.grdMappingList);
            this.smartSpliterContainer2.Panel2.Controls.Add(this.smartPanel2);
            this.smartSpliterContainer2.Panel2.Text = "Panel2";
            this.smartSpliterContainer2.Size = new System.Drawing.Size(1214, 987);
            this.smartSpliterContainer2.SplitterPosition = 400;
            this.smartSpliterContainer2.TabIndex = 0;
            this.smartSpliterContainer2.Text = "smartSpliterContainer2";
            // 
            // MenuConditionItemMapping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Name = "MenuConditionItemMapping";
            this.Text = "메뉴 - 조회조건 항목 맵핑";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeMenuList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).EndInit();
            this.smartPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).EndInit();
            this.smartGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).EndInit();
            this.smartSpliterContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Framework.SmartControls.SmartTreeList treeMenuList;
        private Framework.SmartControls.SmartPanel smartPanel2;
        private Framework.SmartControls.SmartBandedGrid grdNotMappingList;
        private Framework.SmartControls.SmartBandedGrid grdMappingList;
        private Framework.SmartControls.SmartButton btnSelectItemAdd;
        private Framework.SmartControls.SmartButton btnSelectItemDelete;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartGroupBox smartGroupBox1;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer2;
    }
}