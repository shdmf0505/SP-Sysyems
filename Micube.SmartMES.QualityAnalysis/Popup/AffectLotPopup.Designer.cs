namespace Micube.SmartMES.QualityAnalysis
{
    partial class AffectLotPopup
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
            Micube.Framework.SmartControls.ConditionItemSelectPopup conditionItemSelectPopup2 = new Micube.Framework.SmartControls.ConditionItemSelectPopup();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AffectLotPopup));
            Micube.Framework.SmartControls.ConditionCollection conditionCollection3 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection4 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblProductId = new Micube.Framework.SmartControls.SmartLabel();
            this.lblLotId = new Micube.Framework.SmartControls.SmartLabel();
            this.popupProductId = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            this.txtLotId = new Micube.Framework.SmartControls.SmartTextBox();
            this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
            this.btnAffectLotCalc = new Micube.Framework.SmartControls.SmartButton();
            this.gbxAffectLot = new Micube.Framework.SmartControls.SmartGroupBox();
            this.grdAffectLot = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.btnApply = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupProductId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbxAffectLot)).BeginInit();
            this.gbxAffectLot.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(760, 378);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.267455F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.15965F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.795997F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.8383F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.8421F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.89474F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.02632F));
            this.tableLayoutPanel1.Controls.Add(this.lblProductId, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblLotId, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.popupProductId, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtLotId, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSearch, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAffectLotCalc, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.gbxAffectLot, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.625239F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.37476F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(760, 378);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblProductId
            // 
            this.lblProductId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductId.Location = new System.Drawing.Point(0, 0);
            this.lblProductId.Margin = new System.Windows.Forms.Padding(0);
            this.lblProductId.Name = "lblProductId";
            this.lblProductId.Size = new System.Drawing.Size(55, 25);
            this.lblProductId.TabIndex = 0;
            this.lblProductId.Text = "품목코드";
            // 
            // lblLotId
            // 
            this.lblLotId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLotId.Location = new System.Drawing.Point(220, 0);
            this.lblLotId.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.lblLotId.Name = "lblLotId";
            this.lblLotId.Size = new System.Drawing.Size(39, 25);
            this.lblLotId.TabIndex = 1;
            this.lblLotId.Text = "LOT#";
            // 
            // popupProductId
            // 
            this.popupProductId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.popupProductId.EditValue = "";
            this.popupProductId.LabelText = null;
            this.popupProductId.LanguageKey = null;
            this.popupProductId.Location = new System.Drawing.Point(55, 0);
            this.popupProductId.Margin = new System.Windows.Forms.Padding(0);
            this.popupProductId.Name = "popupProductId";
            this.popupProductId.Properties.AutoHeight = false;
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
            this.popupProductId.SelectPopupCondition = conditionItemSelectPopup2;
            this.popupProductId.Size = new System.Drawing.Size(145, 25);
            this.popupProductId.TabIndex = 2;
            // 
            // txtLotId
            // 
            this.txtLotId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLotId.EditValue = "";
            this.txtLotId.LabelText = null;
            this.txtLotId.LanguageKey = null;
            this.txtLotId.Location = new System.Drawing.Point(259, 0);
            this.txtLotId.Margin = new System.Windows.Forms.Padding(0);
            this.txtLotId.Name = "txtLotId";
            this.txtLotId.Properties.AutoHeight = false;
            this.txtLotId.Size = new System.Drawing.Size(135, 25);
            this.txtLotId.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.IsBusy = false;
            this.btnSearch.LanguageKey = "SEARCH";
            this.btnSearch.Location = new System.Drawing.Point(404, 0);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSearch.Size = new System.Drawing.Size(80, 25);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "조회";
            this.btnSearch.TooltipLanguageKey = "";
            // 
            // btnAffectLotCalc
            // 
            this.btnAffectLotCalc.AllowFocus = false;
            this.btnAffectLotCalc.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAffectLotCalc.IsBusy = false;
            this.btnAffectLotCalc.Location = new System.Drawing.Point(588, 0);
            this.btnAffectLotCalc.Margin = new System.Windows.Forms.Padding(0);
            this.btnAffectLotCalc.Name = "btnAffectLotCalc";
            this.btnAffectLotCalc.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnAffectLotCalc.Size = new System.Drawing.Size(172, 25);
            this.btnAffectLotCalc.TabIndex = 5;
            this.btnAffectLotCalc.Text = "Affect Lot 산정(출) 연계";
            this.btnAffectLotCalc.TooltipLanguageKey = "";
            // 
            // gbxAffectLot
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.gbxAffectLot, 7);
            this.gbxAffectLot.Controls.Add(this.grdAffectLot);
            this.gbxAffectLot.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.gbxAffectLot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxAffectLot.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.gbxAffectLot.Location = new System.Drawing.Point(0, 34);
            this.gbxAffectLot.Margin = new System.Windows.Forms.Padding(0);
            this.gbxAffectLot.Name = "gbxAffectLot";
            this.gbxAffectLot.ShowBorder = true;
            this.gbxAffectLot.Size = new System.Drawing.Size(760, 306);
            this.gbxAffectLot.TabIndex = 6;
            this.gbxAffectLot.Text = "Affect Lot 리스트";
            // 
            // grdAffectLot
            // 
            this.grdAffectLot.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdAffectLot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAffectLot.IsUsePaging = false;
            this.grdAffectLot.LanguageKey = null;
            this.grdAffectLot.Location = new System.Drawing.Point(2, 31);
            this.grdAffectLot.Margin = new System.Windows.Forms.Padding(0);
            this.grdAffectLot.Name = "grdAffectLot";
            this.grdAffectLot.ShowBorder = false;
            this.grdAffectLot.ShowButtonBar = false;
            this.grdAffectLot.Size = new System.Drawing.Size(756, 273);
            this.grdAffectLot.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 7);
            this.flowLayoutPanel1.Controls.Add(this.btnClose);
            this.flowLayoutPanel1.Controls.Add(this.btnApply);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 350);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(760, 28);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.IsBusy = false;
            this.btnClose.Location = new System.Drawing.Point(680, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(80, 25);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "닫기";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // btnApply
            // 
            this.btnApply.AllowFocus = false;
            this.btnApply.IsBusy = false;
            this.btnApply.Location = new System.Drawing.Point(594, 0);
            this.btnApply.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnApply.Name = "btnApply";
            this.btnApply.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnApply.Size = new System.Drawing.Size(80, 25);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "적용";
            this.btnApply.TooltipLanguageKey = "";
            // 
            // AffectLotPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 398);
            this.Name = "AffectLotPopup";
            this.Text = "Affect Lot 추가 ";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupProductId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbxAffectLot)).EndInit();
            this.gbxAffectLot.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartLabel lblProductId;
        private Framework.SmartControls.SmartLabel lblLotId;
        private Framework.SmartControls.SmartSelectPopupEdit popupProductId;
        private Framework.SmartControls.SmartTextBox txtLotId;
        private Framework.SmartControls.SmartButton btnSearch;
        private Framework.SmartControls.SmartButton btnAffectLotCalc;
        private Framework.SmartControls.SmartGroupBox gbxAffectLot;
        private Framework.SmartControls.SmartBandedGrid grdAffectLot;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartButton btnApply;
    }
}