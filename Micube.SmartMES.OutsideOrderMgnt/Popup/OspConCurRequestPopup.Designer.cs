namespace Micube.SmartMES.OutsideOrderMgnt.Popup
{
    partial class OspConCurRequestPopup
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
            Micube.Framework.SmartControls.ConditionCollection conditionCollection3 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection4 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdSearch = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.cboStatus = new Micube.Framework.SmartControls.SmartComboBox();
            this.cboFunctionid = new Micube.Framework.SmartControls.SmartComboBox();
            this.txtRequestno = new Micube.Framework.SmartControls.SmartTextBox();
            this.dtpEndDate = new Micube.Framework.SmartControls.SmartDateEdit();
            this.dtpStartDate = new Micube.Framework.SmartControls.SmartDateEdit();
            this.lbldate = new Micube.Framework.SmartControls.SmartLabel();
            this.lblStatus = new Micube.Framework.SmartControls.SmartLabel();
            this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
            this.lblFunctionid = new Micube.Framework.SmartControls.SmartLabel();
            this.smartLabel3 = new Micube.Framework.SmartControls.SmartLabel();
            this.lblRequesttime = new Micube.Framework.SmartControls.SmartLabel();
            this.lblRequestno = new Micube.Framework.SmartControls.SmartLabel();
            this.popupRequestuser = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFunctionid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRequestno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupRequestuser.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(911, 426);
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdSearch, 0, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartPanel1, 0, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 2;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(911, 426);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // grdSearch
            // 
            this.grdSearch.Caption = "";
            this.grdSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSearch.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdSearch.IsUsePaging = false;
            this.grdSearch.LanguageKey = "";
            this.grdSearch.Location = new System.Drawing.Point(0, 80);
            this.grdSearch.Margin = new System.Windows.Forms.Padding(0);
            this.grdSearch.Name = "grdSearch";
            this.grdSearch.ShowBorder = true;
            this.grdSearch.Size = new System.Drawing.Size(911, 346);
            this.grdSearch.TabIndex = 112;
            this.grdSearch.UseAutoBestFitColumns = false;
            // 
            // smartPanel1
            // 
            this.smartPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel1.Controls.Add(this.popupRequestuser);
            this.smartPanel1.Controls.Add(this.cboStatus);
            this.smartPanel1.Controls.Add(this.cboFunctionid);
            this.smartPanel1.Controls.Add(this.txtRequestno);
            this.smartPanel1.Controls.Add(this.dtpEndDate);
            this.smartPanel1.Controls.Add(this.dtpStartDate);
            this.smartPanel1.Controls.Add(this.lbldate);
            this.smartPanel1.Controls.Add(this.lblStatus);
            this.smartPanel1.Controls.Add(this.btnSearch);
            this.smartPanel1.Controls.Add(this.lblFunctionid);
            this.smartPanel1.Controls.Add(this.smartLabel3);
            this.smartPanel1.Controls.Add(this.lblRequesttime);
            this.smartPanel1.Controls.Add(this.lblRequestno);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(911, 80);
            this.smartPanel1.TabIndex = 0;
            // 
            // cboStatus
            // 
            this.cboStatus.LabelText = null;
            this.cboStatus.LanguageKey = null;
            this.cboStatus.Location = new System.Drawing.Point(725, 11);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.PopupWidth = 0;
            this.cboStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatus.Properties.NullText = "";
            this.cboStatus.ShowHeader = true;
            this.cboStatus.Size = new System.Drawing.Size(138, 24);
            this.cboStatus.TabIndex = 122;
            this.cboStatus.VisibleColumns = null;
            this.cboStatus.VisibleColumnsWidth = null;
            // 
            // cboFunctionid
            // 
            this.cboFunctionid.LabelText = null;
            this.cboFunctionid.LanguageKey = null;
            this.cboFunctionid.Location = new System.Drawing.Point(400, 11);
            this.cboFunctionid.Name = "cboFunctionid";
            this.cboFunctionid.PopupWidth = 0;
            this.cboFunctionid.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboFunctionid.Properties.NullText = "";
            this.cboFunctionid.ShowHeader = true;
            this.cboFunctionid.Size = new System.Drawing.Size(214, 24);
            this.cboFunctionid.TabIndex = 122;
            this.cboFunctionid.VisibleColumns = null;
            this.cboFunctionid.VisibleColumnsWidth = null;
            // 
            // txtRequestno
            // 
            this.txtRequestno.LabelText = null;
            this.txtRequestno.LanguageKey = null;
            this.txtRequestno.Location = new System.Drawing.Point(93, 11);
            this.txtRequestno.Name = "txtRequestno";
            this.txtRequestno.Size = new System.Drawing.Size(161, 24);
            this.txtRequestno.TabIndex = 121;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.EditValue = null;
            this.dtpEndDate.LabelText = null;
            this.dtpEndDate.LanguageKey = null;
            this.dtpEndDate.Location = new System.Drawing.Point(273, 45);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpEndDate.Size = new System.Drawing.Size(125, 24);
            this.dtpEndDate.TabIndex = 118;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.EditValue = null;
            this.dtpStartDate.LabelText = null;
            this.dtpStartDate.LanguageKey = null;
            this.dtpStartDate.Location = new System.Drawing.Point(92, 45);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpStartDate.Size = new System.Drawing.Size(125, 24);
            this.dtpStartDate.TabIndex = 119;
            // 
            // lbldate
            // 
            this.lbldate.LanguageKey = "";
            this.lbldate.Location = new System.Drawing.Point(233, 48);
            this.lbldate.Name = "lbldate";
            this.lbldate.Size = new System.Drawing.Size(11, 18);
            this.lbldate.TabIndex = 120;
            this.lbldate.Text = "~";
            // 
            // lblStatus
            // 
            this.lblStatus.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.lblStatus.Appearance.Options.UseForeColor = true;
            this.lblStatus.LanguageKey = "STATUS";
            this.lblStatus.Location = new System.Drawing.Point(641, 14);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(55, 18);
            this.lblStatus.TabIndex = 113;
            this.lblStatus.Text = "STATUS";
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.IsBusy = false;
            this.btnSearch.IsWrite = false;
            this.btnSearch.LanguageKey = "SEARCH";
            this.btnSearch.Location = new System.Drawing.Point(814, 43);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSearch.Size = new System.Drawing.Size(80, 24);
            this.btnSearch.TabIndex = 116;
            this.btnSearch.Text = "조회";
            this.btnSearch.TooltipLanguageKey = "";
            // 
            // lblFunctionid
            // 
            this.lblFunctionid.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.lblFunctionid.Appearance.Options.UseForeColor = true;
            this.lblFunctionid.LanguageKey = "FUNCTIONID";
            this.lblFunctionid.Location = new System.Drawing.Point(273, 14);
            this.lblFunctionid.Name = "lblFunctionid";
            this.lblFunctionid.Size = new System.Drawing.Size(52, 18);
            this.lblFunctionid.TabIndex = 113;
            this.lblFunctionid.Text = "작업구분";
            // 
            // smartLabel3
            // 
            this.smartLabel3.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.smartLabel3.Appearance.Options.UseForeColor = true;
            this.smartLabel3.LanguageKey = "REQUESTUSER";
            this.smartLabel3.Location = new System.Drawing.Point(476, 43);
            this.smartLabel3.Name = "smartLabel3";
            this.smartLabel3.Size = new System.Drawing.Size(39, 18);
            this.smartLabel3.TabIndex = 113;
            this.smartLabel3.Text = "요청자";
            // 
            // lblRequesttime
            // 
            this.lblRequesttime.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.lblRequesttime.Appearance.Options.UseForeColor = true;
            this.lblRequesttime.LanguageKey = "REQDATE";
            this.lblRequesttime.Location = new System.Drawing.Point(23, 48);
            this.lblRequesttime.Name = "lblRequesttime";
            this.lblRequesttime.Size = new System.Drawing.Size(52, 18);
            this.lblRequesttime.TabIndex = 113;
            this.lblRequesttime.Text = "요청일자";
            // 
            // lblRequestno
            // 
            this.lblRequestno.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.lblRequestno.Appearance.Options.UseForeColor = true;
            this.lblRequestno.LanguageKey = "CSMREQUESTNO";
            this.lblRequestno.Location = new System.Drawing.Point(23, 14);
            this.lblRequestno.Name = "lblRequestno";
            this.lblRequestno.Size = new System.Drawing.Size(52, 18);
            this.lblRequestno.TabIndex = 113;
            this.lblRequestno.Text = "요청번호";
            // 
            // popupRequestuser
            // 
            this.popupRequestuser.LabelText = null;
            this.popupRequestuser.LanguageKey = null;
            this.popupRequestuser.Location = new System.Drawing.Point(568, 42);
            this.popupRequestuser.Name = "popupRequestuser";
            conditionItemSelectPopup2.ApplySelection = null;
            conditionItemSelectPopup2.AutoFillColumnNames = null;
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
            conditionItemSelectPopup2.RelationIds = null;
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
            this.popupRequestuser.SelectPopupCondition = conditionItemSelectPopup2;
            this.popupRequestuser.Size = new System.Drawing.Size(158, 24);
            this.popupRequestuser.TabIndex = 123;
            // 
            // OspConCurRequestPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 446);
            this.LanguageKey = "OSPCONCURREQUESTPOPUP";
            this.Name = "OspConCurRequestPopup";
            this.Text = "Claim마감기간";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.smartPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFunctionid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRequestno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupRequestuser.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartBandedGrid grdSearch;
        private Framework.SmartControls.SmartLabel lblRequestno;
        private Framework.SmartControls.SmartButton btnSearch;
        private Framework.SmartControls.SmartDateEdit dtpEndDate;
        private Framework.SmartControls.SmartDateEdit dtpStartDate;
        private Framework.SmartControls.SmartLabel lbldate;
        private Framework.SmartControls.SmartLabel lblRequesttime;
        private Framework.SmartControls.SmartTextBox txtRequestno;
        private Framework.SmartControls.SmartComboBox cboFunctionid;
        private Framework.SmartControls.SmartLabel lblFunctionid;
        private Framework.SmartControls.SmartComboBox cboStatus;
        private Framework.SmartControls.SmartLabel lblStatus;
        private Framework.SmartControls.SmartLabel smartLabel3;
        private Framework.SmartControls.SmartSelectPopupEdit popupRequestuser;
    }
}