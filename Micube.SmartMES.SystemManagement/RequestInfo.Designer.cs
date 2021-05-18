namespace Micube.SmartMES.SystemManagement
{
    partial class RequestInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RequestInfo));
            this.treeMenu = new Micube.Framework.SmartControls.SmartTreeList();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.smartSpliterContainer2 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdrequest = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterContainer3 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.smartPanel3 = new Micube.Framework.SmartControls.SmartPanel();
            this.ucMessageInfo = new Micube.SmartMES.SystemManagement.ucMessageInfoNoPopup();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.fileSr = new Micube.SmartMES.Commons.Controls.SmartFileProcessingControl();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.grdcomment = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel2 = new Micube.Framework.SmartControls.SmartPanel();
            this.btn_commentsave = new Micube.Framework.SmartControls.SmartButton();
            this.txtcomment = new Micube.Framework.SmartControls.SmartTextBox();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            this.btn_add = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).BeginInit();
            this.smartSpliterContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer3)).BeginInit();
            this.smartSpliterContainer3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel3)).BeginInit();
            this.smartPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).BeginInit();
            this.smartPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtcomment.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(0, 36);
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(3152, 522, 812, 500);
            this.pnlCondition.Size = new System.Drawing.Size(0, 0);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btn_add);
            this.pnlToolbar.Size = new System.Drawing.Size(1565, 30);
            this.pnlToolbar.Controls.SetChildIndex(this.btn_add, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer1);
            this.pnlContent.Size = new System.Drawing.Size(1565, 789);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1565, 825);
            // 
            // treeMenu
            // 
            this.treeMenu.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeMenu.DisplayMember = null;
            this.treeMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeMenu.LabelText = null;
            this.treeMenu.LanguageKey = null;
            this.treeMenu.Location = new System.Drawing.Point(0, 0);
            this.treeMenu.MaxHeight = 0;
            this.treeMenu.Name = "treeMenu";
            this.treeMenu.NodeTypeFieldName = "NODETYPE";
            this.treeMenu.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.treeMenu.OptionsBehavior.AutoPopulateColumns = false;
            this.treeMenu.OptionsBehavior.Editable = false;
            this.treeMenu.OptionsFilter.AllowAutoFilterConditionChange = DevExpress.Utils.DefaultBoolean.False;
            this.treeMenu.OptionsFilter.AllowColumnMRUFilterList = false;
            this.treeMenu.OptionsFilter.AllowMRUFilterList = false;
            this.treeMenu.OptionsFind.AlwaysVisible = true;
            this.treeMenu.OptionsFind.ClearFindOnClose = false;
            this.treeMenu.OptionsFind.FindMode = DevExpress.XtraTreeList.FindMode.FindClick;
            this.treeMenu.OptionsFind.FindNullPrompt = "";
            this.treeMenu.OptionsFind.ShowClearButton = false;
            this.treeMenu.OptionsFind.ShowCloseButton = false;
            this.treeMenu.OptionsFind.ShowFindButton = false;
            this.treeMenu.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.treeMenu.OptionsView.ShowColumns = false;
            this.treeMenu.OptionsView.ShowHierarchyIndentationLines = DevExpress.Utils.DefaultBoolean.True;
            this.treeMenu.OptionsView.ShowHorzLines = false;
            this.treeMenu.OptionsView.ShowIndentAsRowStyle = true;
            this.treeMenu.OptionsView.ShowIndicator = false;
            this.treeMenu.OptionsView.ShowTreeLines = DevExpress.Utils.DefaultBoolean.True;
            this.treeMenu.OptionsView.ShowVertLines = false;
            this.treeMenu.ParentMember = "ParentID";
            this.treeMenu.ResultIsLeafLevel = true;
            this.treeMenu.Size = new System.Drawing.Size(303, 789);
            this.treeMenu.TabIndex = 0;
            this.treeMenu.ValueMember = "ID";
            this.treeMenu.ValueNodeTypeFieldName = "Equipment";
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.treeMenu);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.smartSpliterContainer2);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(1565, 789);
            this.smartSpliterContainer1.SplitterPosition = 303;
            this.smartSpliterContainer1.TabIndex = 1;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // smartSpliterContainer2
            // 
            this.smartSpliterContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer2.Horizontal = false;
            this.smartSpliterContainer2.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer2.Name = "smartSpliterContainer2";
            this.smartSpliterContainer2.Panel1.Controls.Add(this.grdrequest);
            this.smartSpliterContainer2.Panel1.Text = "Panel1";
            this.smartSpliterContainer2.Panel2.Controls.Add(this.smartSpliterContainer3);
            this.smartSpliterContainer2.Panel2.Text = "Panel2";
            this.smartSpliterContainer2.Size = new System.Drawing.Size(1256, 789);
            this.smartSpliterContainer2.SplitterPosition = 376;
            this.smartSpliterContainer2.TabIndex = 1;
            this.smartSpliterContainer2.Text = "smartSpliterContainer2";
            // 
            // grdrequest
            // 
            this.grdrequest.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdrequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdrequest.IsUsePaging = false;
            this.grdrequest.LanguageKey = "REQUESTINFO";
            this.grdrequest.Location = new System.Drawing.Point(0, 0);
            this.grdrequest.Margin = new System.Windows.Forms.Padding(0);
            this.grdrequest.Name = "grdrequest";
            this.grdrequest.ShowBorder = true;
            this.grdrequest.Size = new System.Drawing.Size(1256, 376);
            this.grdrequest.TabIndex = 0;
            // 
            // smartSpliterContainer3
            // 
            this.smartSpliterContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer3.Horizontal = false;
            this.smartSpliterContainer3.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer3.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer3.Name = "smartSpliterContainer3";
            this.smartSpliterContainer3.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.smartSpliterContainer3.Panel1.Text = "Panel1";
            this.smartSpliterContainer3.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.smartSpliterContainer3.Panel2.Text = "Panel2";
            this.smartSpliterContainer3.Size = new System.Drawing.Size(1256, 407);
            this.smartSpliterContainer3.SplitterPosition = 296;
            this.smartSpliterContainer3.TabIndex = 0;
            this.smartSpliterContainer3.Text = "smartSpliterContainer3";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.smartPanel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 249F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1256, 296);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // smartPanel3
            // 
            this.smartPanel3.Controls.Add(this.ucMessageInfo);
            this.smartPanel3.Controls.Add(this.smartSpliterControl1);
            this.smartPanel3.Controls.Add(this.fileSr);
            this.smartPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel3.Location = new System.Drawing.Point(3, 3);
            this.smartPanel3.Name = "smartPanel3";
            this.smartPanel3.Size = new System.Drawing.Size(1250, 290);
            this.smartPanel3.TabIndex = 1;
            // 
            // ucMessageInfo
            // 
            this.ucMessageInfo.CommentText = "{\\rtf1\\ansi\\ansicpg1251\\deff0{\\fonttbl{\\f0\\fnil\\fcharset204 Microsoft Sans Serif;" +
    "}}\r\n\\viewkind4\\uc1\\pard\\lang1042\\f0\\fs18}\r\n";
            this.ucMessageInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMessageInfo.Location = new System.Drawing.Point(2, 2);
            this.ucMessageInfo.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.ucMessageInfo.Name = "ucMessageInfo";
            this.ucMessageInfo.Size = new System.Drawing.Size(586, 286);
            this.ucMessageInfo.TabIndex = 0;
            this.ucMessageInfo.TitleText = "";
            this.ucMessageInfo.Load += new System.EventHandler(this.ucMessageInfo_Load);
            // 
            // smartSpliterControl1
            // 
            this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.smartSpliterControl1.Location = new System.Drawing.Point(588, 2);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(6, 286);
            this.smartSpliterControl1.TabIndex = 3;
            this.smartSpliterControl1.TabStop = false;
            // 
            // fileSr
            // 
            this.fileSr.Dock = System.Windows.Forms.DockStyle.Right;
            this.fileSr.LanguageKey = "FILE";
            this.fileSr.Location = new System.Drawing.Point(594, 2);
            this.fileSr.Margin = new System.Windows.Forms.Padding(0);
            this.fileSr.Name = "fileSr";
            this.fileSr.Size = new System.Drawing.Size(654, 286);
            this.fileSr.TabIndex = 1;
            this.fileSr.UploadPath = "";
            this.fileSr.UseCommentsColumn = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1256, 105);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.grdcomment, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.smartPanel2, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1250, 99);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // grdcomment
            // 
            this.grdcomment.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdcomment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdcomment.IsUsePaging = false;
            this.grdcomment.LanguageKey = "COMMENTS";
            this.grdcomment.Location = new System.Drawing.Point(0, 0);
            this.grdcomment.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.grdcomment.Name = "grdcomment";
            this.grdcomment.ShowBorder = true;
            this.grdcomment.Size = new System.Drawing.Size(1250, 59);
            this.grdcomment.TabIndex = 1;
            // 
            // smartPanel2
            // 
            this.smartPanel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel2.Controls.Add(this.btn_commentsave);
            this.smartPanel2.Controls.Add(this.txtcomment);
            this.smartPanel2.Controls.Add(this.smartLabel1);
            this.smartPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel2.Location = new System.Drawing.Point(3, 69);
            this.smartPanel2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.smartPanel2.Name = "smartPanel2";
            this.smartPanel2.Size = new System.Drawing.Size(1244, 27);
            this.smartPanel2.TabIndex = 3;
            // 
            // btn_commentsave
            // 
            this.btn_commentsave.AllowFocus = false;
            this.btn_commentsave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_commentsave.IsBusy = false;
            this.btn_commentsave.IsWrite = false;
            this.btn_commentsave.Location = new System.Drawing.Point(1135, 0);
            this.btn_commentsave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.btn_commentsave.Name = "btn_commentsave";
            this.btn_commentsave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btn_commentsave.Size = new System.Drawing.Size(105, 22);
            this.btn_commentsave.TabIndex = 2;
            this.btn_commentsave.Text = "저장";
            this.btn_commentsave.TooltipLanguageKey = "";
            // 
            // txtcomment
            // 
            this.txtcomment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtcomment.LabelText = null;
            this.txtcomment.LanguageKey = null;
            this.txtcomment.Location = new System.Drawing.Point(50, 0);
            this.txtcomment.Name = "txtcomment";
            this.txtcomment.Size = new System.Drawing.Size(1079, 24);
            this.txtcomment.TabIndex = 1;
            // 
            // smartLabel1
            // 
            this.smartLabel1.Location = new System.Drawing.Point(0, 0);
            this.smartLabel1.Name = "smartLabel1";
            this.smartLabel1.Size = new System.Drawing.Size(31, 18);
            this.smartLabel1.TabIndex = 0;
            this.smartLabel1.Text = "내용 ";
            // 
            // btn_add
            // 
            this.btn_add.AllowFocus = false;
            this.btn_add.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_add.IsBusy = false;
            this.btn_add.IsWrite = false;
            this.btn_add.LanguageKey = "NEW";
            this.btn_add.Location = new System.Drawing.Point(1357, 0);
            this.btn_add.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.btn_add.Name = "btn_add";
            this.btn_add.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btn_add.Size = new System.Drawing.Size(103, 29);
            this.btn_add.TabIndex = 5;
            this.btn_add.Text = "신규";
            this.btn_add.TooltipLanguageKey = "";
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // RequestInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1603, 863);
            this.ConditionsVisible = false;
            this.Name = "RequestInfo";
            this.Padding = new System.Windows.Forms.Padding(19, 19, 19, 19);
            this.Text = "69c ";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).EndInit();
            this.smartSpliterContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer3)).EndInit();
            this.smartSpliterContainer3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel3)).EndInit();
            this.smartPanel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).EndInit();
            this.smartPanel2.ResumeLayout(false);
            this.smartPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtcomment.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartTreeList treeMenu;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer2;
        private Framework.SmartControls.SmartBandedGrid grdrequest;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Commons.Controls.SmartFileProcessingControl fileSr;
        private Framework.SmartControls.SmartBandedGrid grdcomment;
        private ucMessageInfoNoPopup ucMessageInfo;
        private Framework.SmartControls.SmartPanel smartPanel3;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Framework.SmartControls.SmartPanel smartPanel2;
        private Framework.SmartControls.SmartButton btn_commentsave;
        private Framework.SmartControls.SmartTextBox txtcomment;
        private Framework.SmartControls.SmartLabel smartLabel1;
        private Framework.SmartControls.SmartButton btn_add;
    }
}