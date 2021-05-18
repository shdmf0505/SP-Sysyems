namespace Micube.SmartMES.ProcessManagement
{
    partial class LotBatchTrackIn
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
            Micube.Framework.SmartControls.ConditionItemSelectPopup conditionItemSelectPopup1 = new Micube.Framework.SmartControls.ConditionItemSelectPopup();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LotBatchTrackIn));
            Micube.Framework.SmartControls.ConditionCollection conditionCollection1 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection2 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            this.grdLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.txtComment = new Micube.Framework.SmartControls.SmartTextBox();
            this.txtwork = new Micube.Framework.SmartControls.SmartLabel();
            this.txt2 = new Micube.Framework.SmartControls.SmartLabel();
            this.txtWorker = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            this.tabUnderList = new Micube.Framework.SmartControls.SmartTabControl();
            this.tabpageEquipment = new DevExpress.XtraTab.XtraTabPage();
            this.grdEquipmentList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tabpageComment = new DevExpress.XtraTab.XtraTabPage();
            this.grdComment = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tabpageProcessSpec = new DevExpress.XtraTab.XtraTabPage();
            this.grdProcessSpec = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorker.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabUnderList)).BeginInit();
            this.tabUnderList.SuspendLayout();
            this.tabpageEquipment.SuspendLayout();
            this.tabpageComment.SuspendLayout();
            this.tabpageProcessSpec.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 525);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(754, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartPanel1);
            this.pnlContent.Size = new System.Drawing.Size(754, 529);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1059, 558);
            // 
            // grdLotList
            // 
            this.grdLotList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLotList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLotList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLotList.IsUsePaging = false;
            this.grdLotList.LanguageKey = null;
            this.grdLotList.Location = new System.Drawing.Point(0, 0);
            this.grdLotList.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotList.Name = "grdLotList";
            this.grdLotList.ShowBorder = true;
            this.grdLotList.Size = new System.Drawing.Size(750, 276);
            this.grdLotList.TabIndex = 0;
            this.grdLotList.UseAutoBestFitColumns = false;
            // 
            // smartPanel1
            // 
            this.smartPanel1.Controls.Add(this.smartSpliterContainer1);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(754, 529);
            this.smartPanel1.TabIndex = 1;
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Horizontal = false;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(2, 2);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdLotList);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(750, 525);
            this.smartSpliterContainer1.SplitterPosition = 276;
            this.smartSpliterContainer1.TabIndex = 6;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartSplitTableLayoutPanel2, 0, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.tabUnderList, 0, 2);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 3;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(750, 244);
            this.smartSplitTableLayoutPanel1.TabIndex = 5;
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.ColumnCount = 7;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.33334F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.txtComment, 5, 0);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.txtwork, 0, 0);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.txt2, 4, 0);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.txtWorker, 2, 0);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(0, 5);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 2;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(750, 35);
            this.smartSplitTableLayoutPanel2.TabIndex = 5;
            // 
            // txtComment
            // 
            this.txtComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtComment.LabelText = null;
            this.txtComment.LanguageKey = null;
            this.txtComment.Location = new System.Drawing.Point(413, 3);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(277, 20);
            this.txtComment.TabIndex = 8;
            // 
            // txtwork
            // 
            this.txtwork.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtwork.Appearance.Options.UseForeColor = true;
            this.txtwork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtwork.LanguageKey = "WORKMAN";
            this.txtwork.Location = new System.Drawing.Point(3, 3);
            this.txtwork.Name = "txtwork";
            this.txtwork.Size = new System.Drawing.Size(94, 24);
            this.txtwork.TabIndex = 6;
            this.txtwork.Text = "작업자";
            // 
            // txt2
            // 
            this.txt2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt2.LanguageKey = "COMMENT";
            this.txt2.Location = new System.Drawing.Point(313, 3);
            this.txt2.Name = "txt2";
            this.txt2.Size = new System.Drawing.Size(94, 24);
            this.txt2.TabIndex = 7;
            this.txt2.Text = "특이사항";
            // 
            // txtWorker
            // 
            this.txtWorker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWorker.LabelText = null;
            this.txtWorker.LanguageKey = null;
            this.txtWorker.Location = new System.Drawing.Point(108, 3);
            this.txtWorker.Name = "txtWorker";
            conditionItemSelectPopup1.ApplySelection = null;
            conditionItemSelectPopup1.AutoFillColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("conditionItemSelectPopup1.AutoFillColumnNames")));
            conditionItemSelectPopup1.CanOkNoSelection = true;
            conditionItemSelectPopup1.ClearButtonRealOnly = false;
            conditionItemSelectPopup1.ClearButtonVisible = true;
            conditionItemSelectPopup1.ConditionDefaultId = null;
            conditionItemSelectPopup1.ConditionLayoutType = DevExpress.XtraLayout.Utils.LayoutType.Horizontal;
            conditionItemSelectPopup1.ConditionRequireId = "";
            conditionItemSelectPopup1.Conditions = conditionCollection1;
            conditionItemSelectPopup1.CustomPopup = null;
            conditionItemSelectPopup1.CustomValidate = null;
            conditionItemSelectPopup1.DefaultDisplayValue = null;
            conditionItemSelectPopup1.DefaultValue = null;
            conditionItemSelectPopup1.DisplayFieldName = "";
            conditionItemSelectPopup1.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            conditionItemSelectPopup1.GreatThenEqual = false;
            conditionItemSelectPopup1.GreatThenId = "";
            conditionItemSelectPopup1.GridColumns = conditionCollection2;
            conditionItemSelectPopup1.Id = null;
            conditionItemSelectPopup1.InitAction = null;
            conditionItemSelectPopup1.IsCaptionWordWrap = false;
            conditionItemSelectPopup1.IsEnabled = true;
            conditionItemSelectPopup1.IsHidden = false;
            conditionItemSelectPopup1.IsImmediatlyUpdate = true;
            conditionItemSelectPopup1.IsKeyColumn = false;
            conditionItemSelectPopup1.IsMultiGrid = false;
            conditionItemSelectPopup1.IsReadOnly = false;
            conditionItemSelectPopup1.IsRequired = false;
            conditionItemSelectPopup1.IsSearchOnLoading = true;
            conditionItemSelectPopup1.IsUseMultiColumnPaste = true;
            conditionItemSelectPopup1.IsUseRowCheckByMouseDrag = false;
            conditionItemSelectPopup1.LabelText = null;
            conditionItemSelectPopup1.LanguageKey = null;
            conditionItemSelectPopup1.LessThenEqual = false;
            conditionItemSelectPopup1.LessThenId = "";
            conditionItemSelectPopup1.NoSelectionMessageId = "";
            conditionItemSelectPopup1.PopupButtonStyle = Micube.Framework.SmartControls.PopupButtonStyles.Ok_Cancel;
            conditionItemSelectPopup1.PopupCustomValidation = null;
            conditionItemSelectPopup1.Position = 0D;
            conditionItemSelectPopup1.QueryPopup = null;
            conditionItemSelectPopup1.RelationIds = ((System.Collections.Generic.List<string>)(resources.GetObject("conditionItemSelectPopup1.RelationIds")));
            conditionItemSelectPopup1.ResultAction = null;
            conditionItemSelectPopup1.ResultCount = 1;
            conditionItemSelectPopup1.SearchButtonReadOnly = false;
            conditionItemSelectPopup1.SearchQuery = null;
            conditionItemSelectPopup1.SearchText = null;
            conditionItemSelectPopup1.SearchTextControlId = null;
            conditionItemSelectPopup1.SelectionQuery = null;
            conditionItemSelectPopup1.ShowSearchButton = true;
            conditionItemSelectPopup1.TextAlignment = Micube.Framework.SmartControls.TextAlignment.Default;
            conditionItemSelectPopup1.Title = null;
            conditionItemSelectPopup1.ToolTip = null;
            conditionItemSelectPopup1.ToolTipLanguageKey = null;
            conditionItemSelectPopup1.ValueFieldName = "";
            conditionItemSelectPopup1.WindowSize = new System.Drawing.Size(800, 500);
            this.txtWorker.SelectPopupCondition = conditionItemSelectPopup1;
            this.txtWorker.Size = new System.Drawing.Size(194, 20);
            this.txtWorker.TabIndex = 5;
            // 
            // tabUnderList
            // 
            this.tabUnderList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabUnderList.Location = new System.Drawing.Point(3, 43);
            this.tabUnderList.Name = "tabUnderList";
            this.tabUnderList.SelectedTabPage = this.tabpageEquipment;
            this.tabUnderList.Size = new System.Drawing.Size(744, 198);
            this.tabUnderList.TabIndex = 6;
            this.tabUnderList.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabpageEquipment,
            this.tabpageComment,
            this.tabpageProcessSpec});
            // 
            // tabpageEquipment
            // 
            this.tabpageEquipment.Controls.Add(this.grdEquipmentList);
            this.tabUnderList.SetLanguageKey(this.tabpageEquipment, "EQUIPMENT");
            this.tabpageEquipment.Name = "tabpageEquipment";
            this.tabpageEquipment.Size = new System.Drawing.Size(738, 169);
            this.tabpageEquipment.Text = "xtraTabPage1";
            // 
            // grdEquipmentList
            // 
            this.grdEquipmentList.Caption = "";
            this.grdEquipmentList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdEquipmentList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdEquipmentList.IsUsePaging = false;
            this.grdEquipmentList.LanguageKey = "";
            this.grdEquipmentList.Location = new System.Drawing.Point(0, 0);
            this.grdEquipmentList.Margin = new System.Windows.Forms.Padding(0);
            this.grdEquipmentList.Name = "grdEquipmentList";
            this.grdEquipmentList.ShowBorder = true;
            this.grdEquipmentList.ShowStatusBar = false;
            this.grdEquipmentList.Size = new System.Drawing.Size(738, 169);
            this.grdEquipmentList.TabIndex = 1;
            this.grdEquipmentList.UseAutoBestFitColumns = false;
            // 
            // tabpageComment
            // 
            this.tabpageComment.Controls.Add(this.grdComment);
            this.tabUnderList.SetLanguageKey(this.tabpageComment, "DFFREMARKS");
            this.tabpageComment.Name = "tabpageComment";
            this.tabpageComment.Size = new System.Drawing.Size(740, 129);
            this.tabpageComment.Text = "xtraTabPage2";
            // 
            // grdComment
            // 
            this.grdComment.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdComment.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdComment.IsUsePaging = false;
            this.grdComment.LanguageKey = null;
            this.grdComment.Location = new System.Drawing.Point(0, 0);
            this.grdComment.Margin = new System.Windows.Forms.Padding(0);
            this.grdComment.Name = "grdComment";
            this.grdComment.ShowBorder = true;
            this.grdComment.ShowStatusBar = false;
            this.grdComment.Size = new System.Drawing.Size(740, 129);
            this.grdComment.TabIndex = 1;
            this.grdComment.UseAutoBestFitColumns = false;
            // 
            // tabpageProcessSpec
            // 
            this.tabpageProcessSpec.Controls.Add(this.grdProcessSpec);
            this.tabUnderList.SetLanguageKey(this.tabpageProcessSpec, "PROCESSSPEC");
            this.tabpageProcessSpec.Name = "tabpageProcessSpec";
            this.tabpageProcessSpec.Size = new System.Drawing.Size(740, 129);
            this.tabpageProcessSpec.Text = "xtraTabPage3";
            // 
            // grdProcessSpec
            // 
            this.grdProcessSpec.Caption = "";
            this.grdProcessSpec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProcessSpec.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdProcessSpec.IsUsePaging = false;
            this.grdProcessSpec.LanguageKey = null;
            this.grdProcessSpec.Location = new System.Drawing.Point(0, 0);
            this.grdProcessSpec.Margin = new System.Windows.Forms.Padding(0);
            this.grdProcessSpec.Name = "grdProcessSpec";
            this.grdProcessSpec.ShowBorder = true;
            this.grdProcessSpec.ShowStatusBar = false;
            this.grdProcessSpec.Size = new System.Drawing.Size(740, 129);
            this.grdProcessSpec.TabIndex = 1;
            this.grdProcessSpec.UseAutoBestFitColumns = false;
            // 
            // LotBatchTrackIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1097, 596);
            this.Name = "LotBatchTrackIn";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.ShowSaveCompleteMessage = false;
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorker.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabUnderList)).EndInit();
            this.tabUnderList.ResumeLayout(false);
            this.tabpageEquipment.ResumeLayout(false);
            this.tabpageComment.ResumeLayout(false);
            this.tabpageProcessSpec.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdLotList;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartLabel txt2;
        private Framework.SmartControls.SmartLabel txtwork;
        private Framework.SmartControls.SmartSelectPopupEdit txtWorker;
        private Framework.SmartControls.SmartTextBox txtComment;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
        private Framework.SmartControls.SmartTabControl tabUnderList;
        private DevExpress.XtraTab.XtraTabPage tabpageEquipment;
        private Framework.SmartControls.SmartBandedGrid grdEquipmentList;
        private DevExpress.XtraTab.XtraTabPage tabpageComment;
        private DevExpress.XtraTab.XtraTabPage tabpageProcessSpec;
        private Framework.SmartControls.SmartBandedGrid grdComment;
        private Framework.SmartControls.SmartBandedGrid grdProcessSpec;
    }
}
