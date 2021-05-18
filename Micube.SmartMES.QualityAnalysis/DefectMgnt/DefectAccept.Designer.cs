namespace Micube.SmartMES.QualityAnalysis
{
    partial class DefectAccept
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
            Micube.Framework.SmartControls.ConditionItemSelectPopup conditionItemSelectPopup2 = new Micube.Framework.SmartControls.ConditionItemSelectPopup();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DefectAccept));
            Micube.Framework.SmartControls.ConditionCollection conditionCollection3 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection4 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            this.tabDefectAccept = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgTakeover = new DevExpress.XtraTab.XtraTabPage();
            this.grdTakeover = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgHistory = new DevExpress.XtraTab.XtraTabPage();
            this.grdHistory = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancelTakeover = new Micube.Framework.SmartControls.SmartButton();
            this.btnTakeover = new Micube.Framework.SmartControls.SmartButton();
            this.popupInboundArea = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            this.lblInboundArea = new Micube.Framework.SmartControls.SmartLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabDefectAccept)).BeginInit();
            this.tabDefectAccept.SuspendLayout();
            this.tpgTakeover.SuspendLayout();
            this.tpgHistory.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupInboundArea.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 397);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.flowLayoutPanel2);
            this.pnlToolbar.Size = new System.Drawing.Size(542, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.flowLayoutPanel2, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabDefectAccept);
            this.pnlContent.Size = new System.Drawing.Size(542, 401);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(847, 430);
            // 
            // tabDefectAccept
            // 
            this.tabDefectAccept.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tabDefectAccept.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabDefectAccept.Location = new System.Drawing.Point(0, 0);
            this.tabDefectAccept.Name = "tabDefectAccept";
            this.tabDefectAccept.SelectedTabPage = this.tpgTakeover;
            this.tabDefectAccept.Size = new System.Drawing.Size(542, 401);
            this.tabDefectAccept.TabIndex = 0;
            this.tabDefectAccept.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgTakeover,
            this.tpgHistory});
            // 
            // tpgTakeover
            // 
            this.tpgTakeover.Controls.Add(this.grdTakeover);
            this.tpgTakeover.Name = "tpgTakeover";
            this.tpgTakeover.Size = new System.Drawing.Size(536, 372);
            this.tpgTakeover.Text = "인수처리";
            // 
            // grdTakeover
            // 
            this.grdTakeover.Caption = "인수처리 리스트";
            this.grdTakeover.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTakeover.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdTakeover.IsUsePaging = false;
            this.grdTakeover.LanguageKey = null;
            this.grdTakeover.Location = new System.Drawing.Point(0, 0);
            this.grdTakeover.Margin = new System.Windows.Forms.Padding(0);
            this.grdTakeover.Name = "grdTakeover";
            this.grdTakeover.ShowBorder = false;
            this.grdTakeover.Size = new System.Drawing.Size(536, 372);
            this.grdTakeover.TabIndex = 0;
            this.grdTakeover.UseAutoBestFitColumns = false;
            // 
            // tpgHistory
            // 
            this.tpgHistory.Controls.Add(this.grdHistory);
            this.tpgHistory.Name = "tpgHistory";
            this.tpgHistory.Size = new System.Drawing.Size(536, 372);
            this.tpgHistory.Text = "내역조회";
            // 
            // grdHistory
            // 
            this.grdHistory.Caption = "내역조회 리스트";
            this.grdHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdHistory.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdHistory.IsUsePaging = false;
            this.grdHistory.LanguageKey = null;
            this.grdHistory.Location = new System.Drawing.Point(0, 0);
            this.grdHistory.Margin = new System.Windows.Forms.Padding(0);
            this.grdHistory.Name = "grdHistory";
            this.grdHistory.ShowBorder = false;
            this.grdHistory.ShowStatusBar = false;
            this.grdHistory.Size = new System.Drawing.Size(536, 372);
            this.grdHistory.TabIndex = 0;
            this.grdHistory.UseAutoBestFitColumns = false;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btnCancelTakeover);
            this.flowLayoutPanel2.Controls.Add(this.btnTakeover);
            this.flowLayoutPanel2.Controls.Add(this.popupInboundArea);
            this.flowLayoutPanel2.Controls.Add(this.lblInboundArea);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(47, 0);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(495, 24);
            this.flowLayoutPanel2.TabIndex = 5;
            // 
            // btnCancelTakeover
            // 
            this.btnCancelTakeover.AllowFocus = false;
            this.btnCancelTakeover.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCancelTakeover.IsBusy = false;
            this.btnCancelTakeover.IsWrite = false;
            this.btnCancelTakeover.LanguageKey = "CANCLETAKEOVER";
            this.btnCancelTakeover.Location = new System.Drawing.Point(412, 0);
            this.btnCancelTakeover.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnCancelTakeover.Name = "btnCancelTakeover";
            this.btnCancelTakeover.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancelTakeover.Size = new System.Drawing.Size(80, 24);
            this.btnCancelTakeover.TabIndex = 0;
            this.btnCancelTakeover.Text = "인수취소";
            this.btnCancelTakeover.TooltipLanguageKey = "";
            this.btnCancelTakeover.Visible = false;
            // 
            // btnTakeover
            // 
            this.btnTakeover.AllowFocus = false;
            this.btnTakeover.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTakeover.IsBusy = false;
            this.btnTakeover.IsWrite = false;
            this.btnTakeover.LanguageKey = "TAKEOVER";
            this.btnTakeover.Location = new System.Drawing.Point(326, 0);
            this.btnTakeover.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnTakeover.Name = "btnTakeover";
            this.btnTakeover.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnTakeover.Size = new System.Drawing.Size(80, 24);
            this.btnTakeover.TabIndex = 1;
            this.btnTakeover.Text = "인수등록";
            this.btnTakeover.TooltipLanguageKey = "";
            this.btnTakeover.Visible = false;
            // 
            // popupInboundArea
            // 
            this.popupInboundArea.Dock = System.Windows.Forms.DockStyle.Top;
            this.popupInboundArea.LabelText = null;
            this.popupInboundArea.LanguageKey = null;
            this.popupInboundArea.Location = new System.Drawing.Point(88, 2);
            this.popupInboundArea.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
            this.popupInboundArea.Name = "popupInboundArea";
            this.popupInboundArea.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            conditionItemSelectPopup2.ApplySelection = null;
            conditionItemSelectPopup2.AutoFillColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("conditionItemSelectPopup2.AutoFillColumnNames")));
            conditionItemSelectPopup2.CanOkNoSelection = true;
            conditionItemSelectPopup2.ClearButtonRealOnly = false;
            conditionItemSelectPopup2.ClearButtonVisible = true;
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
            conditionItemSelectPopup2.IsCaptionWordWrap = false;
            conditionItemSelectPopup2.IsEnabled = true;
            conditionItemSelectPopup2.IsHidden = false;
            conditionItemSelectPopup2.IsImmediatlyUpdate = true;
            conditionItemSelectPopup2.IsKeyColumn = false;
            conditionItemSelectPopup2.IsMultiGrid = false;
            conditionItemSelectPopup2.IsReadOnly = false;
            conditionItemSelectPopup2.IsRequired = false;
            conditionItemSelectPopup2.IsSearchOnLoading = true;
            conditionItemSelectPopup2.IsUseMultiColumnPaste = true;
            conditionItemSelectPopup2.IsUseRowCheckByMouseDrag = false;
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
            conditionItemSelectPopup2.SearchButtonReadOnly = false;
            conditionItemSelectPopup2.SearchQuery = null;
            conditionItemSelectPopup2.SearchText = null;
            conditionItemSelectPopup2.SearchTextControlId = null;
            conditionItemSelectPopup2.SelectionQuery = null;
            conditionItemSelectPopup2.ShowSearchButton = true;
            conditionItemSelectPopup2.TextAlignment = Micube.Framework.SmartControls.TextAlignment.Default;
            conditionItemSelectPopup2.Title = null;
            conditionItemSelectPopup2.ToolTip = null;
            conditionItemSelectPopup2.ToolTipLanguageKey = null;
            conditionItemSelectPopup2.ValueFieldName = "";
            conditionItemSelectPopup2.WindowSize = new System.Drawing.Size(800, 500);
            this.popupInboundArea.SelectPopupCondition = conditionItemSelectPopup2;
            this.popupInboundArea.Size = new System.Drawing.Size(232, 20);
            this.popupInboundArea.TabIndex = 2;
            // 
            // lblInboundArea
            // 
            this.lblInboundArea.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblInboundArea.LanguageKey = "RECEIVEAREA";
            this.lblInboundArea.Location = new System.Drawing.Point(32, 5);
            this.lblInboundArea.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.lblInboundArea.Name = "lblInboundArea";
            this.lblInboundArea.Size = new System.Drawing.Size(50, 14);
            this.lblInboundArea.TabIndex = 3;
            this.lblInboundArea.Text = "인수작업장";
            // 
            // DefectAccept
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 450);
            this.Name = "DefectAccept";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabDefectAccept)).EndInit();
            this.tabDefectAccept.ResumeLayout(false);
            this.tpgTakeover.ResumeLayout(false);
            this.tpgHistory.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupInboundArea.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabDefectAccept;
        private DevExpress.XtraTab.XtraTabPage tpgTakeover;
        private DevExpress.XtraTab.XtraTabPage tpgHistory;
        private Framework.SmartControls.SmartBandedGrid grdTakeover;
        private Framework.SmartControls.SmartBandedGrid grdHistory;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Framework.SmartControls.SmartButton btnCancelTakeover;
        private Framework.SmartControls.SmartButton btnTakeover;
        private Framework.SmartControls.SmartSelectPopupEdit popupInboundArea;
        private Framework.SmartControls.SmartLabel lblInboundArea;
    }
}