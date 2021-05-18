namespace Micube.SmartMES.OutsideOrderMgnt
{
    partial class OutsourcingPeriodConfirmation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OutsourcingPeriodConfirmation));
            Micube.Framework.SmartControls.ConditionCollection conditionCollection3 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection4 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.tapIsExcept = new Micube.Framework.SmartControls.SmartTabControl();
            this.tapOspActual = new DevExpress.XtraTab.XtraTabPage();
            this.grdOspActual = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tapOspEtcWork = new DevExpress.XtraTab.XtraTabPage();
            this.grdOspEtcWork = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tapOspEtcAmount = new DevExpress.XtraTab.XtraTabPage();
            this.grdOspEtcAmount = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.lblAreaid = new Micube.Framework.SmartControls.SmartLabel();
            this.PopupPeriodid = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            this.lblTransavtionno = new Micube.Framework.SmartControls.SmartLabel();
            this.txtPeriodstate = new Micube.Framework.SmartControls.SmartTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tapIsExcept)).BeginInit();
            this.tapIsExcept.SuspendLayout();
            this.tapOspActual.SuspendLayout();
            this.tapOspEtcWork.SuspendLayout();
            this.tapOspEtcAmount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PopupPeriodid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPeriodstate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Size = new System.Drawing.Size(371, 900);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(843, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlContent.Size = new System.Drawing.Size(843, 903);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1224, 939);
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.tapIsExcept, 0, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartPanel1, 0, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 2;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(843, 903);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // tapIsExcept
            // 
            this.tapIsExcept.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tapIsExcept.Location = new System.Drawing.Point(3, 33);
            this.tapIsExcept.Name = "tapIsExcept";
            this.tapIsExcept.SelectedTabPage = this.tapOspActual;
            this.tapIsExcept.Size = new System.Drawing.Size(837, 867);
            this.tapIsExcept.TabIndex = 2;
            this.tapIsExcept.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tapOspActual,
            this.tapOspEtcWork,
            this.tapOspEtcAmount});
            // 
            // tapOspActual
            // 
            this.tapOspActual.Controls.Add(this.grdOspActual);
            this.tapIsExcept.SetLanguageKey(this.tapOspActual, "OSPACTUALISEXCEPT");
            this.tapOspActual.Name = "tapOspActual";
            this.tapOspActual.Size = new System.Drawing.Size(830, 821);
            this.tapOspActual.Text = "OSPACTUALISEXCEPT";
            // 
            // grdOspActual
            // 
            this.grdOspActual.Caption = "";
            this.grdOspActual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOspActual.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdOspActual.IsUsePaging = false;
            this.grdOspActual.LanguageKey = "OUTSOURCINGEXCLUSIONOSPACTUAL";
            this.grdOspActual.Location = new System.Drawing.Point(0, 0);
            this.grdOspActual.Margin = new System.Windows.Forms.Padding(0);
            this.grdOspActual.Name = "grdOspActual";
            this.grdOspActual.ShowBorder = true;
            this.grdOspActual.ShowStatusBar = false;
            this.grdOspActual.Size = new System.Drawing.Size(830, 821);
            this.grdOspActual.TabIndex = 8;
            this.grdOspActual.UseAutoBestFitColumns = false;
            // 
            // tapOspEtcWork
            // 
            this.tapOspEtcWork.Controls.Add(this.grdOspEtcWork);
            this.tapIsExcept.SetLanguageKey(this.tapOspEtcWork, "OSPETCWORKISEXCEPT");
            this.tapOspEtcWork.Name = "tapOspEtcWork";
            this.tapOspEtcWork.Size = new System.Drawing.Size(830, 821);
            this.tapOspEtcWork.Text = "OSPETCWORKISEXCEPT";
            // 
            // grdOspEtcWork
            // 
            this.grdOspEtcWork.Caption = "";
            this.grdOspEtcWork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOspEtcWork.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdOspEtcWork.IsUsePaging = false;
            this.grdOspEtcWork.LanguageKey = "OUTSOURCINGEXCLUSIONOSPETCWORK";
            this.grdOspEtcWork.Location = new System.Drawing.Point(0, 0);
            this.grdOspEtcWork.Margin = new System.Windows.Forms.Padding(0);
            this.grdOspEtcWork.Name = "grdOspEtcWork";
            this.grdOspEtcWork.ShowBorder = true;
            this.grdOspEtcWork.ShowStatusBar = false;
            this.grdOspEtcWork.Size = new System.Drawing.Size(830, 821);
            this.grdOspEtcWork.TabIndex = 8;
            this.grdOspEtcWork.UseAutoBestFitColumns = false;
            // 
            // tapOspEtcAmount
            // 
            this.tapOspEtcAmount.Controls.Add(this.grdOspEtcAmount);
            this.tapIsExcept.SetLanguageKey(this.tapOspEtcAmount, "OSPETCAMOUNTISEXCEPT");
            this.tapOspEtcAmount.Name = "tapOspEtcAmount";
            this.tapOspEtcAmount.Size = new System.Drawing.Size(830, 831);
            this.tapOspEtcAmount.Text = "OSPETCAMOUNTISEXCEPT";
            // 
            // grdOspEtcAmount
            // 
            this.grdOspEtcAmount.Caption = "";
            this.grdOspEtcAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOspEtcAmount.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdOspEtcAmount.IsUsePaging = false;
            this.grdOspEtcAmount.LanguageKey = "OUTSOURCINGEXCLUSIONOSPETCAMOUNT";
            this.grdOspEtcAmount.Location = new System.Drawing.Point(0, 0);
            this.grdOspEtcAmount.Margin = new System.Windows.Forms.Padding(0);
            this.grdOspEtcAmount.Name = "grdOspEtcAmount";
            this.grdOspEtcAmount.ShowBorder = true;
            this.grdOspEtcAmount.ShowStatusBar = false;
            this.grdOspEtcAmount.Size = new System.Drawing.Size(830, 831);
            this.grdOspEtcAmount.TabIndex = 8;
            this.grdOspEtcAmount.UseAutoBestFitColumns = false;
            // 
            // smartPanel1
            // 
            this.smartPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel1.Controls.Add(this.smartSplitTableLayoutPanel2);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(843, 30);
            this.smartPanel1.TabIndex = 1;
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.ColumnCount = 5;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.txtPeriodstate, 3, 0);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.lblTransavtionno, 2, 0);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.PopupPeriodid, 1, 0);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.lblAreaid, 0, 0);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 1;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(843, 30);
            this.smartSplitTableLayoutPanel2.TabIndex = 0;
            // 
            // lblAreaid
            // 
            this.lblAreaid.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblAreaid.Appearance.Options.UseForeColor = true;
            this.lblAreaid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAreaid.LanguageKey = "CLOSEYM";
            this.lblAreaid.Location = new System.Drawing.Point(3, 3);
            this.lblAreaid.Name = "lblAreaid";
            this.lblAreaid.Size = new System.Drawing.Size(144, 24);
            this.lblAreaid.TabIndex = 2;
            this.lblAreaid.Text = "smartLabel1";
            // 
            // PopupPeriodid
            // 
            this.PopupPeriodid.LabelText = null;
            this.PopupPeriodid.LanguageKey = null;
            this.PopupPeriodid.Location = new System.Drawing.Point(153, 3);
            this.PopupPeriodid.Name = "PopupPeriodid";
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
            this.PopupPeriodid.SelectPopupCondition = conditionItemSelectPopup2;
            this.PopupPeriodid.Size = new System.Drawing.Size(174, 24);
            this.PopupPeriodid.TabIndex = 16;
            // 
            // lblTransavtionno
            // 
            this.lblTransavtionno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTransavtionno.LanguageKey = "PERIODSTATE";
            this.lblTransavtionno.Location = new System.Drawing.Point(353, 3);
            this.lblTransavtionno.Name = "lblTransavtionno";
            this.lblTransavtionno.Size = new System.Drawing.Size(144, 24);
            this.lblTransavtionno.TabIndex = 17;
            this.lblTransavtionno.Text = "smartLabel1";
            // 
            // txtPeriodstate
            // 
            this.txtPeriodstate.LabelText = null;
            this.txtPeriodstate.LanguageKey = null;
            this.txtPeriodstate.Location = new System.Drawing.Point(503, 3);
            this.txtPeriodstate.Name = "txtPeriodstate";
            this.txtPeriodstate.Properties.ReadOnly = true;
            this.txtPeriodstate.Size = new System.Drawing.Size(114, 24);
            this.txtPeriodstate.TabIndex = 19;
            // 
            // OutsourcingPeriodConfirmation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 977);
            this.Name = "OutsourcingPeriodConfirmation";
            this.Padding = new System.Windows.Forms.Padding(19, 19, 19, 19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tapIsExcept)).EndInit();
            this.tapIsExcept.ResumeLayout(false);
            this.tapOspActual.ResumeLayout(false);
            this.tapOspEtcWork.ResumeLayout(false);
            this.tapOspEtcAmount.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PopupPeriodid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPeriodstate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartTabControl tapIsExcept;
        private DevExpress.XtraTab.XtraTabPage tapOspActual;
        private Framework.SmartControls.SmartBandedGrid grdOspActual;
        private DevExpress.XtraTab.XtraTabPage tapOspEtcWork;
        private Framework.SmartControls.SmartBandedGrid grdOspEtcWork;
        private DevExpress.XtraTab.XtraTabPage tapOspEtcAmount;
        private Framework.SmartControls.SmartBandedGrid grdOspEtcAmount;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
        private Framework.SmartControls.SmartLabel lblAreaid;
        private Framework.SmartControls.SmartSelectPopupEdit PopupPeriodid;
        private Framework.SmartControls.SmartLabel lblTransavtionno;
        private Framework.SmartControls.SmartTextBox txtPeriodstate;
    }
}