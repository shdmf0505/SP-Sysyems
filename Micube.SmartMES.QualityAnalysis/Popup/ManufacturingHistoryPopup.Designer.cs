namespace Micube.SmartMES.QualityAnalysis
{
    partial class ManufacturingHistoryPopup
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
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.treeMeasureHistory = new Micube.Framework.SmartControls.SmartTreeList();
            this.grdMeasureHistory = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.btnApply = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeMeasureHistory)).BeginInit();
            this.flowLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel2);
            this.pnlMain.Size = new System.Drawing.Size(964, 441);
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.ColumnCount = 2;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.treeMeasureHistory, 0, 0);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdMeasureHistory, 1, 0);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.flowLayoutPanel3, 1, 1);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 2;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(964, 441);
            this.smartSplitTableLayoutPanel2.TabIndex = 1;
            // 
            // treeMeasureHistory
            // 
            this.treeMeasureHistory.DisplayMember = null;
            this.treeMeasureHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeMeasureHistory.LabelText = null;
            this.treeMeasureHistory.LanguageKey = null;
            this.treeMeasureHistory.Location = new System.Drawing.Point(3, 3);
            this.treeMeasureHistory.MaxHeight = 0;
            this.treeMeasureHistory.Name = "treeMeasureHistory";
            this.treeMeasureHistory.NodeTypeFieldName = "NODETYPE";
            this.treeMeasureHistory.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.treeMeasureHistory.OptionsBehavior.AutoPopulateColumns = false;
            this.treeMeasureHistory.OptionsBehavior.Editable = false;
            this.treeMeasureHistory.OptionsFilter.AllowAutoFilterConditionChange = DevExpress.Utils.DefaultBoolean.False;
            this.treeMeasureHistory.OptionsFilter.AllowColumnMRUFilterList = false;
            this.treeMeasureHistory.OptionsFilter.AllowMRUFilterList = false;
            this.treeMeasureHistory.OptionsFind.AlwaysVisible = true;
            this.treeMeasureHistory.OptionsFind.ClearFindOnClose = false;
            this.treeMeasureHistory.OptionsFind.FindMode = DevExpress.XtraTreeList.FindMode.FindClick;
            this.treeMeasureHistory.OptionsFind.FindNullPrompt = "";
            this.treeMeasureHistory.OptionsFind.ShowClearButton = false;
            this.treeMeasureHistory.OptionsFind.ShowCloseButton = false;
            this.treeMeasureHistory.OptionsFind.ShowFindButton = false;
            this.treeMeasureHistory.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.treeMeasureHistory.OptionsView.ShowColumns = false;
            this.treeMeasureHistory.OptionsView.ShowHierarchyIndentationLines = DevExpress.Utils.DefaultBoolean.True;
            this.treeMeasureHistory.OptionsView.ShowHorzLines = false;
            this.treeMeasureHistory.OptionsView.ShowIndentAsRowStyle = true;
            this.treeMeasureHistory.OptionsView.ShowIndicator = false;
            this.treeMeasureHistory.OptionsView.ShowTreeLines = DevExpress.Utils.DefaultBoolean.True;
            this.treeMeasureHistory.OptionsView.ShowVertLines = false;
            this.treeMeasureHistory.ParentMember = "ParentID";
            this.treeMeasureHistory.ResultIsLeafLevel = true;
            this.treeMeasureHistory.Size = new System.Drawing.Size(283, 400);
            this.treeMeasureHistory.TabIndex = 0;
            this.treeMeasureHistory.ValueMember = "ID";
            this.treeMeasureHistory.ValueNodeTypeFieldName = "Equipment";
            // 
            // grdMeasureHistory
            // 
            this.grdMeasureHistory.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdMeasureHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMeasureHistory.IsUsePaging = false;
            this.grdMeasureHistory.LanguageKey = "MANUFACTURINGHISTORYCHECK";
            this.grdMeasureHistory.Location = new System.Drawing.Point(289, 0);
            this.grdMeasureHistory.Margin = new System.Windows.Forms.Padding(0);
            this.grdMeasureHistory.Name = "grdMeasureHistory";
            this.grdMeasureHistory.ShowBorder = true;
            this.grdMeasureHistory.Size = new System.Drawing.Size(675, 406);
            this.grdMeasureHistory.TabIndex = 1;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.btnClose);
            this.flowLayoutPanel3.Controls.Add(this.btnApply);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(292, 409);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(669, 29);
            this.flowLayoutPanel3.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.IsBusy = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(586, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(80, 25);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "닫기";
            this.btnClose.TooltipLanguageKey = "CLOSE";
            // 
            // btnApply
            // 
            this.btnApply.AllowFocus = false;
            this.btnApply.IsBusy = false;
            this.btnApply.LanguageKey = "APPLY";
            this.btnApply.Location = new System.Drawing.Point(500, 0);
            this.btnApply.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnApply.Name = "btnApply";
            this.btnApply.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnApply.Size = new System.Drawing.Size(80, 25);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "적용";
            this.btnApply.TooltipLanguageKey = "APPLY";
            // 
            // ManufacturingHistoryPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.LanguageKey = "MANUFACTURINGHISTORYCHECK";
            this.Name = "ManufacturingHistoryPopup";
            this.Text = "MeasureHistoryPopup1";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeMeasureHistory)).EndInit();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
        private Framework.SmartControls.SmartTreeList treeMeasureHistory;
        private Framework.SmartControls.SmartBandedGrid grdMeasureHistory;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartButton btnApply;
    }
}