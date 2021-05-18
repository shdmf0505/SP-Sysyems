namespace Micube.SmartMES.MaterialsManagement
{
    partial class MaterialOutflowRequest
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
            Micube.Framework.SmartControls.ConditionItemSelectPopup conditionItemSelectPopup3 = new Micube.Framework.SmartControls.ConditionItemSelectPopup();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaterialOutflowRequest));
            Micube.Framework.SmartControls.ConditionCollection conditionCollection5 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection6 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            Micube.Framework.SmartControls.ConditionItemSelectPopup conditionItemSelectPopup2 = new Micube.Framework.SmartControls.ConditionItemSelectPopup();
            Micube.Framework.SmartControls.ConditionCollection conditionCollection3 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection4 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            this.tplMain = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdSearch = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdMaterial = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpldetail = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.txtwarehouseid = new Micube.Framework.SmartControls.SmartTextBox();
            this.txtwarehousename = new Micube.Framework.SmartControls.SmartTextBox();
            this.popupOspAreaid = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            this.lblwarehousename = new Micube.Framework.SmartControls.SmartLabel();
            this.lblProcesssegmentid = new Micube.Framework.SmartControls.SmartLabel();
            this.lblRequestno = new Micube.Framework.SmartControls.SmartLabel();
            this.lblAreaid = new Micube.Framework.SmartControls.SmartLabel();
            this.lblRequestuser = new Micube.Framework.SmartControls.SmartLabel();
            this.lblDescription = new Micube.Framework.SmartControls.SmartLabel();
            this.lblRequesttype = new Micube.Framework.SmartControls.SmartLabel();
            this.cboRequesttype = new Micube.Framework.SmartControls.SmartComboBox();
            this.txtRequestno = new Micube.Framework.SmartControls.SmartTextBox();
            this.txtRequestusername = new Micube.Framework.SmartControls.SmartTextBox();
            this.txtRequestdepartment = new Micube.Framework.SmartControls.SmartTextBox();
            this.txtDescription = new Micube.Framework.SmartControls.SmartTextBox();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.btnClear = new Micube.Framework.SmartControls.SmartButton();
            this.btnDelete = new Micube.Framework.SmartControls.SmartButton();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            this.lblDetail = new Micube.Framework.SmartControls.SmartLabel();
            this.btnSlip = new Micube.Framework.SmartControls.SmartButton();
            this.popupProcesssegmentid = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.tplMain.SuspendLayout();
            this.tpldetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtwarehouseid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtwarehousename.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupOspAreaid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboRequesttype.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRequestno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRequestusername.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRequestdepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupProcesssegmentid.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(371, 908);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnSlip);
            this.pnlToolbar.Size = new System.Drawing.Size(845, 30);
            this.pnlToolbar.Controls.SetChildIndex(this.btnSlip, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tplMain);
            this.pnlContent.Size = new System.Drawing.Size(845, 911);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1226, 947);
            // 
            // tplMain
            // 
            this.tplMain.ColumnCount = 1;
            this.tplMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplMain.Controls.Add(this.grdSearch, 0, 0);
            this.tplMain.Controls.Add(this.grdMaterial, 0, 3);
            this.tplMain.Controls.Add(this.tpldetail, 0, 2);
            this.tplMain.Controls.Add(this.smartPanel1, 0, 1);
            this.tplMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplMain.Location = new System.Drawing.Point(0, 0);
            this.tplMain.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tplMain.Name = "tplMain";
            this.tplMain.RowCount = 4;
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tplMain.Size = new System.Drawing.Size(845, 911);
            this.tplMain.TabIndex = 0;
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
            this.grdSearch.LanguageKey = "MATERIALDISCHARGEREQUESTLIST";
            this.grdSearch.Location = new System.Drawing.Point(0, 0);
            this.grdSearch.Margin = new System.Windows.Forms.Padding(0);
            this.grdSearch.Name = "grdSearch";
            this.grdSearch.ShowBorder = true;
            this.grdSearch.Size = new System.Drawing.Size(845, 468);
            this.grdSearch.TabIndex = 9;
            this.grdSearch.UseAutoBestFitColumns = false;
            // 
            // grdMaterial
            // 
            this.grdMaterial.Caption = "";
            this.grdMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMaterial.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdMaterial.IsUsePaging = false;
            this.grdMaterial.LanguageKey = "MATERIALDISBURSEMENTREQUEST";
            this.grdMaterial.Location = new System.Drawing.Point(0, 598);
            this.grdMaterial.Margin = new System.Windows.Forms.Padding(0);
            this.grdMaterial.Name = "grdMaterial";
            this.grdMaterial.ShowBorder = true;
            this.grdMaterial.Size = new System.Drawing.Size(845, 313);
            this.grdMaterial.TabIndex = 7;
            this.grdMaterial.UseAutoBestFitColumns = false;
            // 
            // tpldetail
            // 
            this.tpldetail.ColumnCount = 7;
            this.tpldetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tpldetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tpldetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tpldetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tpldetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tpldetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tpldetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tpldetail.Controls.Add(this.popupProcesssegmentid, 3, 1);
            this.tpldetail.Controls.Add(this.txtwarehouseid, 6, 0);
            this.tpldetail.Controls.Add(this.txtwarehousename, 5, 0);
            this.tpldetail.Controls.Add(this.popupOspAreaid, 3, 0);
            this.tpldetail.Controls.Add(this.lblwarehousename, 4, 0);
            this.tpldetail.Controls.Add(this.lblProcesssegmentid, 2, 1);
            this.tpldetail.Controls.Add(this.lblRequestno, 0, 0);
            this.tpldetail.Controls.Add(this.lblAreaid, 2, 0);
            this.tpldetail.Controls.Add(this.lblRequestuser, 4, 1);
            this.tpldetail.Controls.Add(this.lblDescription, 0, 2);
            this.tpldetail.Controls.Add(this.lblRequesttype, 0, 1);
            this.tpldetail.Controls.Add(this.cboRequesttype, 1, 1);
            this.tpldetail.Controls.Add(this.txtRequestno, 1, 0);
            this.tpldetail.Controls.Add(this.txtRequestusername, 5, 1);
            this.tpldetail.Controls.Add(this.txtRequestdepartment, 6, 1);
            this.tpldetail.Controls.Add(this.txtDescription, 1, 2);
            this.tpldetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpldetail.Location = new System.Drawing.Point(0, 508);
            this.tpldetail.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tpldetail.Name = "tpldetail";
            this.tpldetail.RowCount = 3;
            this.tpldetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tpldetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tpldetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tpldetail.Size = new System.Drawing.Size(845, 90);
            this.tpldetail.TabIndex = 0;
            // 
            // txtwarehouseid
            // 
            this.txtwarehouseid.LabelText = null;
            this.txtwarehouseid.LanguageKey = null;
            this.txtwarehouseid.Location = new System.Drawing.Point(793, 3);
            this.txtwarehouseid.Name = "txtwarehouseid";
            this.txtwarehouseid.Properties.ReadOnly = true;
            this.txtwarehouseid.Size = new System.Drawing.Size(49, 24);
            this.txtwarehouseid.TabIndex = 18;
            this.txtwarehouseid.Visible = false;
            // 
            // txtwarehousename
            // 
            this.txtwarehousename.LabelText = null;
            this.txtwarehousename.LanguageKey = null;
            this.txtwarehousename.Location = new System.Drawing.Point(643, 3);
            this.txtwarehousename.Name = "txtwarehousename";
            this.txtwarehousename.Properties.ReadOnly = true;
            this.txtwarehousename.Size = new System.Drawing.Size(144, 24);
            this.txtwarehousename.TabIndex = 17;
            // 
            // popupOspAreaid
            // 
            this.popupOspAreaid.LabelText = null;
            this.popupOspAreaid.LanguageKey = null;
            this.popupOspAreaid.Location = new System.Drawing.Point(373, 3);
            this.popupOspAreaid.Name = "popupOspAreaid";
            conditionItemSelectPopup3.ApplySelection = null;
            conditionItemSelectPopup3.AutoFillColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("conditionItemSelectPopup3.AutoFillColumnNames")));
            conditionItemSelectPopup3.CanOkNoSelection = true;
            conditionItemSelectPopup3.ClearButtonRealOnly = false;
            conditionItemSelectPopup3.ClearButtonVisible = true;
            conditionItemSelectPopup3.ConditionDefaultId = null;
            conditionItemSelectPopup3.ConditionLayoutType = DevExpress.XtraLayout.Utils.LayoutType.Horizontal;
            conditionItemSelectPopup3.ConditionRequireId = "";
            conditionItemSelectPopup3.Conditions = conditionCollection5;
            conditionItemSelectPopup3.CustomPopup = null;
            conditionItemSelectPopup3.CustomValidate = null;
            conditionItemSelectPopup3.DefaultDisplayValue = null;
            conditionItemSelectPopup3.DefaultValue = null;
            conditionItemSelectPopup3.DisplayFieldName = "";
            conditionItemSelectPopup3.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            conditionItemSelectPopup3.GreatThenEqual = false;
            conditionItemSelectPopup3.GreatThenId = "";
            conditionItemSelectPopup3.GridColumns = conditionCollection6;
            conditionItemSelectPopup3.Id = null;
            conditionItemSelectPopup3.InitAction = null;
            conditionItemSelectPopup3.IsCaptionWordWrap = false;
            conditionItemSelectPopup3.IsEnabled = true;
            conditionItemSelectPopup3.IsHidden = false;
            conditionItemSelectPopup3.IsImmediatlyUpdate = true;
            conditionItemSelectPopup3.IsKeyColumn = false;
            conditionItemSelectPopup3.IsMultiGrid = false;
            conditionItemSelectPopup3.IsReadOnly = false;
            conditionItemSelectPopup3.IsRequired = false;
            conditionItemSelectPopup3.IsSearchOnLoading = true;
            conditionItemSelectPopup3.IsUseMultiColumnPaste = true;
            conditionItemSelectPopup3.IsUseRowCheckByMouseDrag = false;
            conditionItemSelectPopup3.LabelText = null;
            conditionItemSelectPopup3.LanguageKey = null;
            conditionItemSelectPopup3.LessThenEqual = false;
            conditionItemSelectPopup3.LessThenId = "";
            conditionItemSelectPopup3.NoSelectionMessageId = "";
            conditionItemSelectPopup3.PopupButtonStyle = Micube.Framework.SmartControls.PopupButtonStyles.Ok_Cancel;
            conditionItemSelectPopup3.PopupCustomValidation = null;
            conditionItemSelectPopup3.Position = 0D;
            conditionItemSelectPopup3.QueryPopup = null;
            conditionItemSelectPopup3.RelationIds = ((System.Collections.Generic.List<string>)(resources.GetObject("conditionItemSelectPopup3.RelationIds")));
            conditionItemSelectPopup3.ResultAction = null;
            conditionItemSelectPopup3.ResultCount = 1;
            conditionItemSelectPopup3.SearchButtonReadOnly = false;
            conditionItemSelectPopup3.SearchQuery = null;
            conditionItemSelectPopup3.SearchText = null;
            conditionItemSelectPopup3.SearchTextControlId = null;
            conditionItemSelectPopup3.SelectionQuery = null;
            conditionItemSelectPopup3.ShowSearchButton = true;
            conditionItemSelectPopup3.TextAlignment = Micube.Framework.SmartControls.TextAlignment.Default;
            conditionItemSelectPopup3.Title = null;
            conditionItemSelectPopup3.ToolTip = null;
            conditionItemSelectPopup3.ToolTipLanguageKey = null;
            conditionItemSelectPopup3.ValueFieldName = "";
            conditionItemSelectPopup3.WindowSize = new System.Drawing.Size(800, 500);
            this.popupOspAreaid.SelectPopupCondition = conditionItemSelectPopup3;
            this.popupOspAreaid.Size = new System.Drawing.Size(144, 24);
            this.popupOspAreaid.TabIndex = 16;
            // 
            // lblwarehousename
            // 
            this.lblwarehousename.LanguageKey = "WAREHOUSENAME";
            this.lblwarehousename.Location = new System.Drawing.Point(523, 3);
            this.lblwarehousename.Name = "lblwarehousename";
            this.lblwarehousename.Size = new System.Drawing.Size(26, 18);
            this.lblwarehousename.TabIndex = 0;
            this.lblwarehousename.Text = "창고";
            // 
            // lblProcesssegmentid
            // 
            this.lblProcesssegmentid.LanguageKey = "PROCESSSEGMENTNAME";
            this.lblProcesssegmentid.Location = new System.Drawing.Point(263, 33);
            this.lblProcesssegmentid.Name = "lblProcesssegmentid";
            this.lblProcesssegmentid.Size = new System.Drawing.Size(52, 18);
            this.lblProcesssegmentid.TabIndex = 0;
            this.lblProcesssegmentid.Text = "요청부서";
            // 
            // lblRequestno
            // 
            this.lblRequestno.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblRequestno.Appearance.Options.UseForeColor = true;
            this.lblRequestno.LanguageKey = "CSMREQUESTNO";
            this.lblRequestno.Location = new System.Drawing.Point(3, 3);
            this.lblRequestno.Name = "lblRequestno";
            this.lblRequestno.Size = new System.Drawing.Size(52, 18);
            this.lblRequestno.TabIndex = 0;
            this.lblRequestno.Text = "요청번호";
            // 
            // lblAreaid
            // 
            this.lblAreaid.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblAreaid.Appearance.Options.UseForeColor = true;
            this.lblAreaid.LanguageKey = "AREANAME";
            this.lblAreaid.Location = new System.Drawing.Point(263, 3);
            this.lblAreaid.Name = "lblAreaid";
            this.lblAreaid.Size = new System.Drawing.Size(39, 18);
            this.lblAreaid.TabIndex = 0;
            this.lblAreaid.Text = "작업장";
            // 
            // lblRequestuser
            // 
            this.lblRequestuser.LanguageKey = "REQUESTUSER";
            this.lblRequestuser.Location = new System.Drawing.Point(523, 33);
            this.lblRequestuser.Name = "lblRequestuser";
            this.lblRequestuser.Size = new System.Drawing.Size(39, 18);
            this.lblRequestuser.TabIndex = 0;
            this.lblRequestuser.Text = "요청자";
            // 
            // lblDescription
            // 
            this.lblDescription.LanguageKey = "MATREMARK";
            this.lblDescription.Location = new System.Drawing.Point(3, 63);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(79, 18);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "smartLabel1";
            // 
            // lblRequesttype
            // 
            this.lblRequesttype.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblRequesttype.Appearance.Options.UseForeColor = true;
            this.lblRequesttype.LanguageKey = "CSMREQUESTTYPE";
            this.lblRequesttype.Location = new System.Drawing.Point(3, 33);
            this.lblRequesttype.Name = "lblRequesttype";
            this.lblRequesttype.Size = new System.Drawing.Size(52, 18);
            this.lblRequesttype.TabIndex = 0;
            this.lblRequesttype.Text = "요청유형";
            // 
            // cboRequesttype
            // 
            this.cboRequesttype.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboRequesttype.LabelText = null;
            this.cboRequesttype.LanguageKey = null;
            this.cboRequesttype.Location = new System.Drawing.Point(113, 33);
            this.cboRequesttype.Name = "cboRequesttype";
            this.cboRequesttype.PopupWidth = 0;
            this.cboRequesttype.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboRequesttype.Properties.NullText = "";
            this.cboRequesttype.ShowHeader = true;
            this.cboRequesttype.Size = new System.Drawing.Size(144, 24);
            this.cboRequesttype.TabIndex = 1;
            this.cboRequesttype.VisibleColumns = null;
            this.cboRequesttype.VisibleColumnsWidth = null;
            // 
            // txtRequestno
            // 
            this.txtRequestno.LabelText = null;
            this.txtRequestno.LanguageKey = null;
            this.txtRequestno.Location = new System.Drawing.Point(113, 3);
            this.txtRequestno.Name = "txtRequestno";
            this.txtRequestno.Properties.ReadOnly = true;
            this.txtRequestno.Size = new System.Drawing.Size(144, 24);
            this.txtRequestno.TabIndex = 3;
            // 
            // txtRequestusername
            // 
            this.txtRequestusername.LabelText = null;
            this.txtRequestusername.LanguageKey = null;
            this.txtRequestusername.Location = new System.Drawing.Point(643, 33);
            this.txtRequestusername.Name = "txtRequestusername";
            this.txtRequestusername.Properties.ReadOnly = true;
            this.txtRequestusername.Size = new System.Drawing.Size(144, 24);
            this.txtRequestusername.TabIndex = 4;
            // 
            // txtRequestdepartment
            // 
            this.txtRequestdepartment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRequestdepartment.LabelText = null;
            this.txtRequestdepartment.LanguageKey = null;
            this.txtRequestdepartment.Location = new System.Drawing.Point(793, 33);
            this.txtRequestdepartment.Name = "txtRequestdepartment";
            this.txtRequestdepartment.Properties.ReadOnly = true;
            this.txtRequestdepartment.Size = new System.Drawing.Size(49, 24);
            this.txtRequestdepartment.TabIndex = 4;
            this.txtRequestdepartment.Visible = false;
            // 
            // txtDescription
            // 
            this.tpldetail.SetColumnSpan(this.txtDescription, 5);
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.LabelText = null;
            this.txtDescription.LanguageKey = null;
            this.txtDescription.Location = new System.Drawing.Point(113, 63);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(674, 24);
            this.txtDescription.TabIndex = 5;
            // 
            // smartPanel1
            // 
            this.smartPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel1.Controls.Add(this.btnClear);
            this.smartPanel1.Controls.Add(this.btnDelete);
            this.smartPanel1.Controls.Add(this.btnSave);
            this.smartPanel1.Controls.Add(this.lblDetail);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(0, 468);
            this.smartPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(845, 40);
            this.smartPanel1.TabIndex = 1;
            // 
            // btnClear
            // 
            this.btnClear.AllowFocus = false;
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.IsBusy = false;
            this.btnClear.IsWrite = true;
            this.btnClear.LanguageKey = "CLEAR";
            this.btnClear.Location = new System.Drawing.Point(584, 5);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClear.Size = new System.Drawing.Size(80, 25);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "초기화";
            this.btnClear.TooltipLanguageKey = "";
            this.btnClear.Visible = false;
            // 
            // btnDelete
            // 
            this.btnDelete.AllowFocus = false;
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.IsBusy = false;
            this.btnDelete.IsWrite = true;
            this.btnDelete.LanguageKey = "DELETE";
            this.btnDelete.Location = new System.Drawing.Point(756, 5);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnDelete.Size = new System.Drawing.Size(80, 25);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "삭제";
            this.btnDelete.TooltipLanguageKey = "";
            this.btnDelete.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = true;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(670, 5);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "저장";
            this.btnSave.TooltipLanguageKey = "";
            this.btnSave.Visible = false;
            // 
            // lblDetail
            // 
            this.lblDetail.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetail.Appearance.Options.UseFont = true;
            this.lblDetail.LanguageKey = "MATERIALDISCHARGEREQUEST";
            this.lblDetail.Location = new System.Drawing.Point(8, 9);
            this.lblDetail.Name = "lblDetail";
            this.lblDetail.Size = new System.Drawing.Size(110, 24);
            this.lblDetail.TabIndex = 0;
            this.lblDetail.Text = "smartLabel7";
            // 
            // btnSlip
            // 
            this.btnSlip.AllowFocus = false;
            this.btnSlip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSlip.IsBusy = false;
            this.btnSlip.IsWrite = true;
            this.btnSlip.Location = new System.Drawing.Point(1007, 4);
            this.btnSlip.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSlip.Name = "btnSlip";
            this.btnSlip.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSlip.Size = new System.Drawing.Size(80, 25);
            this.btnSlip.TabIndex = 2;
            this.btnSlip.Text = "청구전표";
            this.btnSlip.TooltipLanguageKey = "";
            this.btnSlip.Visible = false;
            // 
            // popupProcesssegmentid
            // 
            this.popupProcesssegmentid.LabelText = null;
            this.popupProcesssegmentid.LanguageKey = null;
            this.popupProcesssegmentid.Location = new System.Drawing.Point(373, 33);
            this.popupProcesssegmentid.Name = "popupProcesssegmentid";
            this.popupProcesssegmentid.Properties.ReadOnly = true;
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
            this.popupProcesssegmentid.SelectPopupCondition = conditionItemSelectPopup2;
            this.popupProcesssegmentid.Size = new System.Drawing.Size(144, 24);
            this.popupProcesssegmentid.TabIndex = 19;
            // 
            // MaterialOutflowRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 985);
            this.Name = "MaterialOutflowRequest";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.tplMain.ResumeLayout(false);
            this.tpldetail.ResumeLayout(false);
            this.tpldetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtwarehouseid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtwarehousename.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupOspAreaid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboRequesttype.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRequestno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRequestusername.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRequestdepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.smartPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupProcesssegmentid.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel tplMain;
        private Framework.SmartControls.SmartSplitTableLayoutPanel tpldetail;
        private Framework.SmartControls.SmartLabel lblProcesssegmentid;
        private Framework.SmartControls.SmartLabel lblAreaid;
        private Framework.SmartControls.SmartLabel lblRequestuser;
        private Framework.SmartControls.SmartLabel lblDescription;
        private Framework.SmartControls.SmartLabel lblRequesttype;
        private Framework.SmartControls.SmartTextBox txtRequestno;
        private Framework.SmartControls.SmartTextBox txtRequestdepartment;
        private Framework.SmartControls.SmartTextBox txtDescription;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartLabel lblDetail;
        private Framework.SmartControls.SmartButton btnClear;
        private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartBandedGrid grdMaterial;
        private Framework.SmartControls.SmartButton btnSlip;
        private Framework.SmartControls.SmartBandedGrid grdSearch;
        private Framework.SmartControls.SmartButton btnDelete;
        private Framework.SmartControls.SmartLabel lblwarehousename;
        private Framework.SmartControls.SmartLabel lblRequestno;
        private Framework.SmartControls.SmartComboBox cboRequesttype;
        private Framework.SmartControls.SmartTextBox txtRequestusername;
        private Framework.SmartControls.SmartSelectPopupEdit popupOspAreaid;
        private Framework.SmartControls.SmartTextBox txtwarehousename;
        private Framework.SmartControls.SmartTextBox txtwarehouseid;
        private Framework.SmartControls.SmartSelectPopupEdit popupProcesssegmentid;
    }
}