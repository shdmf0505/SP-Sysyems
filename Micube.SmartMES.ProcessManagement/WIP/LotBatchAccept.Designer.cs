namespace Micube.SmartMES.ProcessManagement
{
    partial class LotBatchAccept
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
            Micube.Framework.SmartControls.ConditionItemSelectPopup conditionItemSelectPopup1 = new Micube.Framework.SmartControls.ConditionItemSelectPopup();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LotBatchAccept));
            Micube.Framework.SmartControls.ConditionCollection conditionCollection1 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection2 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            this.grdMain = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.smartLayoutControl1 = new Micube.Framework.SmartControls.SmartLayoutControl();
            this.txtComment = new Micube.Framework.SmartControls.SmartTextBox();
            this.txtWorker = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            this.smartLayoutControlGroup1 = new Micube.Framework.SmartControls.SmartLayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControl1)).BeginInit();
            this.smartLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorker.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
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
            // grdMain
            // 
            this.grdMain.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdMain.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdMain.IsUsePaging = false;
            this.grdMain.LanguageKey = null;
            this.grdMain.Location = new System.Drawing.Point(2, 26);
            this.grdMain.Margin = new System.Windows.Forms.Padding(0);
            this.grdMain.Name = "grdMain";
            this.grdMain.ShowBorder = true;
            this.grdMain.ShowStatusBar = false;
            this.grdMain.Size = new System.Drawing.Size(746, 497);
            this.grdMain.TabIndex = 0;
            this.grdMain.UseAutoBestFitColumns = false;
            // 
            // smartPanel1
            // 
            this.smartPanel1.Controls.Add(this.smartLayoutControl1);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(754, 529);
            this.smartPanel1.TabIndex = 1;
            // 
            // smartLayoutControl1
            // 
            this.smartLayoutControl1.Controls.Add(this.txtComment);
            this.smartLayoutControl1.Controls.Add(this.grdMain);
            this.smartLayoutControl1.Controls.Add(this.txtWorker);
            this.smartLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLayoutControl1.Location = new System.Drawing.Point(2, 2);
            this.smartLayoutControl1.Name = "smartLayoutControl1";
            this.smartLayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1131, 327, 650, 400);
            this.smartLayoutControl1.Root = this.smartLayoutControlGroup1;
            this.smartLayoutControl1.Size = new System.Drawing.Size(750, 525);
            this.smartLayoutControl1.TabIndex = 7;
            this.smartLayoutControl1.Text = "smartLayoutControl1";
            // 
            // txtComment
            // 
            this.txtComment.LabelText = null;
            this.txtComment.LanguageKey = null;
            this.txtComment.Location = new System.Drawing.Point(355, 2);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(393, 20);
            this.txtComment.StyleController = this.smartLayoutControl1;
            this.txtComment.TabIndex = 8;
            // 
            // txtWorker
            // 
            this.txtWorker.LabelText = null;
            this.txtWorker.LanguageKey = null;
            this.txtWorker.Location = new System.Drawing.Point(112, 2);
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
            this.txtWorker.Size = new System.Drawing.Size(116, 20);
            this.txtWorker.StyleController = this.smartLayoutControl1;
            this.txtWorker.TabIndex = 5;
            // 
            // smartLayoutControlGroup1
            // 
            this.smartLayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.smartLayoutControlGroup1.GroupBordersVisible = false;
            this.smartLayoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1});
            this.smartLayoutControlGroup1.Name = "Root";
            this.smartLayoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.smartLayoutControlGroup1.Size = new System.Drawing.Size(750, 525);
            this.smartLayoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdMain;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(750, 501);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtWorker;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(230, 24);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(230, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 2, 2, 2);
            this.layoutControlItem2.Size = new System.Drawing.Size(230, 24);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(105, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtComment;
            this.layoutControlItem3.Location = new System.Drawing.Point(245, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(505, 24);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(105, 14);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(230, 0);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(15, 10);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(15, 10);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(15, 24);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // LotBatchAccept
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1097, 596);
            this.Name = "LotBatchAccept";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.ShowSaveCompleteMessage = false;
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControl1)).EndInit();
            this.smartLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorker.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdMain;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartSelectPopupEdit txtWorker;
        private Framework.SmartControls.SmartTextBox txtComment;
        private Framework.SmartControls.SmartLayoutControl smartLayoutControl1;
        private Framework.SmartControls.SmartLayoutControlGroup smartLayoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}