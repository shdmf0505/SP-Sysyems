namespace Micube.SmartMES.QualityAnalysis
{
    partial class ProcessInspectionRequestLotPopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessInspectionRequestLotPopup));
            Micube.Framework.SmartControls.ConditionCollection conditionCollection1 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection2 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            Micube.Framework.SmartControls.ConditionItemSelectPopup conditionItemSelectPopup2 = new Micube.Framework.SmartControls.ConditionItemSelectPopup();
            Micube.Framework.SmartControls.ConditionCollection conditionCollection3 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection4 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grdLot = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.popProcessSegmentId = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            this.lblProcessSegmentId = new Micube.Framework.SmartControls.SmartLabel();
            this.lblLotId = new Micube.Framework.SmartControls.SmartLabel();
            this.lblProductDefId = new Micube.Framework.SmartControls.SmartLabel();
            this.popProductDefId = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
            this.txtLotId = new Micube.Framework.SmartControls.SmartTextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.btnApply = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popProcessSegmentId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popProductDefId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(777, 513);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.grdLot, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(777, 513);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // grdLot
            // 
            this.grdLot.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLot.IsUsePaging = false;
            this.grdLot.LanguageKey = "LOTLIST";
            this.grdLot.Location = new System.Drawing.Point(0, 62);
            this.grdLot.Margin = new System.Windows.Forms.Padding(0);
            this.grdLot.Name = "grdLot";
            this.grdLot.ShowBorder = true;
            this.grdLot.Size = new System.Drawing.Size(777, 415);
            this.grdLot.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 7;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.83333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.83333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.83333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.83333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.Controls.Add(this.popProcessSegmentId, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblProcessSegmentId, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblLotId, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblProductDefId, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.popProductDefId, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSearch, 6, 2);
            this.tableLayoutPanel2.Controls.Add(this.txtLotId, 2, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(777, 62);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // popProcessSegmentId
            // 
            this.popProcessSegmentId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.popProcessSegmentId.LabelText = null;
            this.popProcessSegmentId.LanguageKey = null;
            this.popProcessSegmentId.Location = new System.Drawing.Point(487, 0);
            this.popProcessSegmentId.Margin = new System.Windows.Forms.Padding(0);
            this.popProcessSegmentId.Name = "popProcessSegmentId";
            this.popProcessSegmentId.Properties.AutoHeight = false;
            conditionItemSelectPopup1.ApplySelection = null;
            conditionItemSelectPopup1.AutoFillColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("conditionItemSelectPopup1.AutoFillColumnNames")));
            conditionItemSelectPopup1.CanOkNoSelection = true;
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
            conditionItemSelectPopup1.IsEnabled = true;
            conditionItemSelectPopup1.IsHidden = false;
            conditionItemSelectPopup1.IsImmediatlyUpdate = true;
            conditionItemSelectPopup1.IsKeyColumn = false;
            conditionItemSelectPopup1.IsMultiGrid = false;
            conditionItemSelectPopup1.IsReadOnly = false;
            conditionItemSelectPopup1.IsRequired = false;
            conditionItemSelectPopup1.IsSearchOnLoading = true;
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
            conditionItemSelectPopup1.SearchQuery = null;
            conditionItemSelectPopup1.SelectionQuery = null;
            conditionItemSelectPopup1.ShowSearchButton = true;
            conditionItemSelectPopup1.TextAlignment = Micube.Framework.SmartControls.TextAlignment.Default;
            conditionItemSelectPopup1.Title = null;
            conditionItemSelectPopup1.ToolTip = null;
            conditionItemSelectPopup1.ToolTipLanguageKey = null;
            conditionItemSelectPopup1.ValueFieldName = "";
            conditionItemSelectPopup1.WindowSize = new System.Drawing.Size(800, 500);
            this.popProcessSegmentId.SelectPopupCondition = conditionItemSelectPopup1;
            this.popProcessSegmentId.Size = new System.Drawing.Size(159, 26);
            this.popProcessSegmentId.TabIndex = 5;
            // 
            // lblProcessSegmentId
            // 
            this.lblProcessSegmentId.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblProcessSegmentId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProcessSegmentId.LanguageKey = "PROCESSSEGMENT";
            this.lblProcessSegmentId.Location = new System.Drawing.Point(328, 0);
            this.lblProcessSegmentId.Margin = new System.Windows.Forms.Padding(0);
            this.lblProcessSegmentId.Name = "lblProcessSegmentId";
            this.lblProcessSegmentId.Size = new System.Drawing.Size(159, 26);
            this.lblProcessSegmentId.TabIndex = 2;
            this.lblProcessSegmentId.Text = "smartLabel3";
            // 
            // lblLotId
            // 
            this.lblLotId.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblLotId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLotId.LanguageKey = "LOTID";
            this.lblLotId.Location = new System.Drawing.Point(5, 31);
            this.lblLotId.Margin = new System.Windows.Forms.Padding(0);
            this.lblLotId.Name = "lblLotId";
            this.lblLotId.Size = new System.Drawing.Size(159, 26);
            this.lblLotId.TabIndex = 1;
            this.lblLotId.Text = "smartLabel2";
            // 
            // lblProductDefId
            // 
            this.lblProductDefId.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblProductDefId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductDefId.LanguageKey = "PRODUCTDEF";
            this.lblProductDefId.Location = new System.Drawing.Point(5, 0);
            this.lblProductDefId.Margin = new System.Windows.Forms.Padding(0);
            this.lblProductDefId.Name = "lblProductDefId";
            this.lblProductDefId.Size = new System.Drawing.Size(159, 26);
            this.lblProductDefId.TabIndex = 0;
            this.lblProductDefId.Text = "smartLabel1";
            // 
            // popProductDefId
            // 
            this.popProductDefId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.popProductDefId.LabelText = null;
            this.popProductDefId.LanguageKey = null;
            this.popProductDefId.Location = new System.Drawing.Point(164, 0);
            this.popProductDefId.Margin = new System.Windows.Forms.Padding(0);
            this.popProductDefId.Name = "popProductDefId";
            this.popProductDefId.Properties.AutoHeight = false;
            conditionItemSelectPopup2.ApplySelection = null;
            conditionItemSelectPopup2.AutoFillColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("conditionItemSelectPopup2.AutoFillColumnNames")));
            conditionItemSelectPopup2.CanOkNoSelection = true;
            conditionItemSelectPopup2.ConditionDefaultId = null;
            conditionItemSelectPopup2.ConditionLayoutType = DevExpress.XtraLayout.Utils.LayoutType.Horizontal;
            conditionItemSelectPopup2.ConditionRequireId = "";
            conditionItemSelectPopup2.Conditions = conditionCollection3;
            conditionItemSelectPopup2.CustomPopup = null;
            conditionItemSelectPopup2.CustomValidate = null;
            conditionItemSelectPopup2.DefaultDisplayValue = null;
            conditionItemSelectPopup2.DefaultValue = null;
            conditionItemSelectPopup2.DisplayFieldName = "";
            conditionItemSelectPopup2.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            conditionItemSelectPopup2.GreatThenEqual = false;
            conditionItemSelectPopup2.GreatThenId = "";
            conditionItemSelectPopup2.GridColumns = conditionCollection4;
            conditionItemSelectPopup2.Id = null;
            conditionItemSelectPopup2.InitAction = null;
            conditionItemSelectPopup2.IsEnabled = true;
            conditionItemSelectPopup2.IsHidden = false;
            conditionItemSelectPopup2.IsImmediatlyUpdate = true;
            conditionItemSelectPopup2.IsKeyColumn = false;
            conditionItemSelectPopup2.IsMultiGrid = false;
            conditionItemSelectPopup2.IsReadOnly = false;
            conditionItemSelectPopup2.IsRequired = false;
            conditionItemSelectPopup2.IsSearchOnLoading = true;
            conditionItemSelectPopup2.LabelText = null;
            conditionItemSelectPopup2.LanguageKey = null;
            conditionItemSelectPopup2.LessThenEqual = false;
            conditionItemSelectPopup2.LessThenId = "";
            conditionItemSelectPopup2.NoSelectionMessageId = "";
            conditionItemSelectPopup2.PopupButtonStyle = Micube.Framework.SmartControls.PopupButtonStyles.Ok_Cancel;
            conditionItemSelectPopup2.PopupCustomValidation = null;
            conditionItemSelectPopup2.Position = 0D;
            conditionItemSelectPopup2.QueryPopup = null;
            conditionItemSelectPopup2.RelationIds = ((System.Collections.Generic.List<string>)(resources.GetObject("conditionItemSelectPopup2.RelationIds")));
            conditionItemSelectPopup2.ResultAction = null;
            conditionItemSelectPopup2.ResultCount = 1;
            conditionItemSelectPopup2.SearchQuery = null;
            conditionItemSelectPopup2.SelectionQuery = null;
            conditionItemSelectPopup2.ShowSearchButton = true;
            conditionItemSelectPopup2.TextAlignment = Micube.Framework.SmartControls.TextAlignment.Default;
            conditionItemSelectPopup2.Title = null;
            conditionItemSelectPopup2.ToolTip = null;
            conditionItemSelectPopup2.ToolTipLanguageKey = null;
            conditionItemSelectPopup2.ValueFieldName = "";
            conditionItemSelectPopup2.WindowSize = new System.Drawing.Size(800, 500);
            this.popProductDefId.SelectPopupCondition = conditionItemSelectPopup2;
            this.popProductDefId.Size = new System.Drawing.Size(159, 26);
            this.popProductDefId.TabIndex = 4;
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSearch.IsBusy = false;
            this.btnSearch.LanguageKey = "SEARCH";
            this.btnSearch.Location = new System.Drawing.Point(697, 31);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSearch.Size = new System.Drawing.Size(80, 25);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "smartButton1";
            this.btnSearch.TooltipLanguageKey = "";
            // 
            // txtLotId
            // 
            this.txtLotId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLotId.LabelText = null;
            this.txtLotId.LanguageKey = null;
            this.txtLotId.Location = new System.Drawing.Point(164, 31);
            this.txtLotId.Margin = new System.Windows.Forms.Padding(0);
            this.txtLotId.Name = "txtLotId";
            this.txtLotId.Properties.AutoHeight = false;
            this.txtLotId.Size = new System.Drawing.Size(159, 26);
            this.txtLotId.TabIndex = 7;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.btnClose);
            this.flowLayoutPanel1.Controls.Add(this.btnApply);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 487);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(777, 26);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.IsBusy = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(697, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(80, 25);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "smartButton1";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // btnApply
            // 
            this.btnApply.AllowFocus = false;
            this.btnApply.IsBusy = false;
            this.btnApply.LanguageKey = "APPLY";
            this.btnApply.Location = new System.Drawing.Point(611, 0);
            this.btnApply.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnApply.Name = "btnApply";
            this.btnApply.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnApply.Size = new System.Drawing.Size(80, 25);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "smartButton2";
            this.btnApply.TooltipLanguageKey = "";
            // 
            // ProcessInspectionRequestLotPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 533);
            this.Name = "ProcessInspectionRequestLotPopup";
            this.Text = "ProcessInspectionRequestLotPopup";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popProcessSegmentId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popProductDefId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartBandedGrid grdLot;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Framework.SmartControls.SmartSelectPopupEdit popProcessSegmentId;
        private Framework.SmartControls.SmartLabel lblProcessSegmentId;
        private Framework.SmartControls.SmartLabel lblLotId;
        private Framework.SmartControls.SmartLabel lblProductDefId;
        private Framework.SmartControls.SmartSelectPopupEdit popProductDefId;
        private Framework.SmartControls.SmartButton btnSearch;
        private Framework.SmartControls.SmartTextBox txtLotId;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartButton btnApply;
    }
}