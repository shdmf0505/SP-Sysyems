namespace Micube.SmartMES.OutsideOrderMgnt
{
    partial class OutsourcedClaimPeriodConfirmation
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
            Micube.Framework.SmartControls.ConditionCollection conditionCollection1 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection2 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            this.tplMain = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdClaimConfirm = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel2 = new Micube.Framework.SmartControls.SmartPanel();
            this.txtPeriodstate = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblTransavtionno = new Micube.Framework.SmartControls.SmartLabel();
            this.PopupPeriodid = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            this.lblAreaid = new Micube.Framework.SmartControls.SmartLabel();
            this.btnClosePeriod = new Micube.Framework.SmartControls.SmartButton();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.tplMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).BeginInit();
            this.smartPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPeriodstate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PopupPeriodid.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
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
            this.pnlContent.Controls.Add(this.tplMain);
            this.pnlContent.Size = new System.Drawing.Size(843, 903);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1224, 939);
            // 
            // tplMain
            // 
            this.tplMain.ColumnCount = 1;
            this.tplMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplMain.Controls.Add(this.grdClaimConfirm, 0, 1);
            this.tplMain.Controls.Add(this.smartPanel2, 0, 0);
            this.tplMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplMain.Location = new System.Drawing.Point(0, 0);
            this.tplMain.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tplMain.Name = "tplMain";
            this.tplMain.RowCount = 2;
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplMain.Size = new System.Drawing.Size(843, 903);
            this.tplMain.TabIndex = 0;
            // 
            // grdClaimConfirm
            // 
            this.grdClaimConfirm.Caption = "";
            this.grdClaimConfirm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdClaimConfirm.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdClaimConfirm.IsUsePaging = false;
            this.grdClaimConfirm.LanguageKey = "OUTSOURCEDCLAIMCONFIRMATIONLIST";
            this.grdClaimConfirm.Location = new System.Drawing.Point(0, 40);
            this.grdClaimConfirm.Margin = new System.Windows.Forms.Padding(0);
            this.grdClaimConfirm.Name = "grdClaimConfirm";
            this.grdClaimConfirm.ShowBorder = true;
            this.grdClaimConfirm.ShowStatusBar = false;
            this.grdClaimConfirm.Size = new System.Drawing.Size(843, 863);
            this.grdClaimConfirm.TabIndex = 6;
            this.grdClaimConfirm.UseAutoBestFitColumns = false;
            // 
            // smartPanel2
            // 
            this.smartPanel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel2.Controls.Add(this.txtPeriodstate);
            this.smartPanel2.Controls.Add(this.lblTransavtionno);
            this.smartPanel2.Controls.Add(this.PopupPeriodid);
            this.smartPanel2.Controls.Add(this.lblAreaid);
            this.smartPanel2.Controls.Add(this.btnClosePeriod);
            this.smartPanel2.Controls.Add(this.btnSave);
            this.smartPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel2.Location = new System.Drawing.Point(0, 0);
            this.smartPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel2.Name = "smartPanel2";
            this.smartPanel2.Size = new System.Drawing.Size(843, 40);
            this.smartPanel2.TabIndex = 7;
            // 
            // txtPeriodstate
            // 
            this.txtPeriodstate.LabelText = null;
            this.txtPeriodstate.LanguageKey = null;
            this.txtPeriodstate.Location = new System.Drawing.Point(461, 6);
            this.txtPeriodstate.Name = "txtPeriodstate";
            this.txtPeriodstate.Properties.ReadOnly = true;
            this.txtPeriodstate.Size = new System.Drawing.Size(114, 24);
            this.txtPeriodstate.TabIndex = 20;
            // 
            // lblTransavtionno
            // 
            this.lblTransavtionno.LanguageKey = "PERIODSTATE";
            this.lblTransavtionno.Location = new System.Drawing.Point(357, 6);
            this.lblTransavtionno.Name = "lblTransavtionno";
            this.lblTransavtionno.Size = new System.Drawing.Size(79, 18);
            this.lblTransavtionno.TabIndex = 19;
            this.lblTransavtionno.Text = "smartLabel1";
            // 
            // PopupPeriodid
            // 
            this.PopupPeriodid.LabelText = null;
            this.PopupPeriodid.LanguageKey = null;
            this.PopupPeriodid.Location = new System.Drawing.Point(135, 7);
            this.PopupPeriodid.Name = "PopupPeriodid";
            conditionItemSelectPopup1.ApplySelection = null;
            conditionItemSelectPopup1.AutoFillColumnNames = null;
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
            conditionItemSelectPopup1.RelationIds = null;
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
            this.PopupPeriodid.SelectPopupCondition = conditionItemSelectPopup1;
            this.PopupPeriodid.Size = new System.Drawing.Size(174, 24);
            this.PopupPeriodid.TabIndex = 18;
            // 
            // lblAreaid
            // 
            this.lblAreaid.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblAreaid.Appearance.Options.UseForeColor = true;
            this.lblAreaid.LanguageKey = "CLOSEYM";
            this.lblAreaid.Location = new System.Drawing.Point(5, 10);
            this.lblAreaid.Name = "lblAreaid";
            this.lblAreaid.Size = new System.Drawing.Size(79, 18);
            this.lblAreaid.TabIndex = 17;
            this.lblAreaid.Text = "smartLabel1";
            // 
            // btnClosePeriod
            // 
            this.btnClosePeriod.AllowFocus = false;
            this.btnClosePeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClosePeriod.IsBusy = false;
            this.btnClosePeriod.IsWrite = true;
            this.btnClosePeriod.LanguageKey = "CLOSEPERIOD";
            this.btnClosePeriod.Location = new System.Drawing.Point(674, 6);
            this.btnClosePeriod.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnClosePeriod.Name = "btnClosePeriod";
            this.btnClosePeriod.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClosePeriod.Size = new System.Drawing.Size(80, 25);
            this.btnClosePeriod.TabIndex = 3;
            this.btnClosePeriod.Text = "마감기간";
            this.btnClosePeriod.TooltipLanguageKey = "";
            this.btnClosePeriod.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = true;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(588, 6);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "저장";
            this.btnSave.TooltipLanguageKey = "";
            this.btnSave.Visible = false;
            // 
            // OutsourcedClaimPeriodConfirmation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 977);
            this.Name = "OutsourcedClaimPeriodConfirmation";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.tplMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).EndInit();
            this.smartPanel2.ResumeLayout(false);
            this.smartPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPeriodstate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PopupPeriodid.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel tplMain;
        private Framework.SmartControls.SmartBandedGrid grdClaimConfirm;
        private Framework.SmartControls.SmartPanel smartPanel2;
        private Framework.SmartControls.SmartButton btnClosePeriod;
        private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartSelectPopupEdit PopupPeriodid;
        private Framework.SmartControls.SmartLabel lblAreaid;
        private Framework.SmartControls.SmartLabel lblTransavtionno;
        private Framework.SmartControls.SmartTextBox txtPeriodstate;
    }
}