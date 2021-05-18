namespace Micube.SmartMES.ToolManagement
{
    partial class RepairToolRequestSend
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RepairToolRequestSend));
            Micube.Framework.SmartControls.ConditionCollection conditionCollection3 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection4 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdToolRepiarSend = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            this.grdInputToolRepairSend = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel4 = new Micube.Framework.SmartControls.SmartPanel();
            this.popEditVendor = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            this.cboToolRepairType = new Micube.Framework.SmartControls.SmartComboBox();
            this.lblRepairVendor = new Micube.Framework.SmartControls.SmartLabel();
            this.txtSendor = new Micube.Framework.SmartControls.SmartTextBox();
            this.txtSendSequence = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblSendDate = new Micube.Framework.SmartControls.SmartLabel();
            this.deSendDate = new Micube.Framework.SmartControls.SmartDateEdit();
            this.lblSendUser = new Micube.Framework.SmartControls.SmartLabel();
            this.lblRepairType = new Micube.Framework.SmartControls.SmartLabel();
            this.btnSearchTool = new Micube.Framework.SmartControls.SmartButton();
            this.lblInputTitle = new Micube.Framework.SmartControls.SmartLabel();
            this.btnInitialize = new Micube.Framework.SmartControls.SmartButton();
            this.btnErase = new Micube.Framework.SmartControls.SmartButton();
            this.btnModify = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel4)).BeginInit();
            this.smartPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popEditVendor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboToolRepairType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendSequence.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deSendDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deSendDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 725);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(1134, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer1);
            this.pnlContent.Size = new System.Drawing.Size(1134, 729);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1439, 758);
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Horizontal = false;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdToolRepiarSend);
            this.smartSpliterContainer1.Panel1.Controls.Add(this.smartPanel1);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdInputToolRepairSend);
            this.smartSpliterContainer1.Panel2.Controls.Add(this.smartPanel4);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(1134, 729);
            this.smartSpliterContainer1.SplitterPosition = 347;
            this.smartSpliterContainer1.TabIndex = 2;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdToolRepiarSend
            // 
            this.grdToolRepiarSend.Caption = "치공구 수리출고목록:";
            this.grdToolRepiarSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdToolRepiarSend.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdToolRepiarSend.IsUsePaging = false;
            this.grdToolRepiarSend.LanguageKey = "TOOLREPAIRSENDLIST";
            this.grdToolRepiarSend.Location = new System.Drawing.Point(0, 36);
            this.grdToolRepiarSend.Margin = new System.Windows.Forms.Padding(0);
            this.grdToolRepiarSend.Name = "grdToolRepiarSend";
            this.grdToolRepiarSend.ShowBorder = true;
            this.grdToolRepiarSend.Size = new System.Drawing.Size(1134, 311);
            this.grdToolRepiarSend.TabIndex = 103;
            this.grdToolRepiarSend.UseAutoBestFitColumns = false;
            // 
            // smartPanel1
            // 
            this.smartPanel1.Controls.Add(this.smartLabel1);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(1134, 36);
            this.smartPanel1.TabIndex = 102;
            // 
            // smartLabel1
            // 
            this.smartLabel1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.smartLabel1.Appearance.Options.UseFont = true;
            this.smartLabel1.LanguageKey = "TOOLREPAIRSEND";
            this.smartLabel1.Location = new System.Drawing.Point(5, 5);
            this.smartLabel1.Name = "smartLabel1";
            this.smartLabel1.Size = new System.Drawing.Size(109, 19);
            this.smartLabel1.TabIndex = 113;
            this.smartLabel1.Text = "치공구 수리출고:";
            // 
            // grdInputToolRepairSend
            // 
            this.grdInputToolRepairSend.Caption = "수리출고:";
            this.grdInputToolRepairSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInputToolRepairSend.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInputToolRepairSend.IsUsePaging = false;
            this.grdInputToolRepairSend.LanguageKey = "REPAIRSENDTOOLLIST";
            this.grdInputToolRepairSend.Location = new System.Drawing.Point(0, 37);
            this.grdInputToolRepairSend.Margin = new System.Windows.Forms.Padding(0);
            this.grdInputToolRepairSend.Name = "grdInputToolRepairSend";
            this.grdInputToolRepairSend.ShowBorder = true;
            this.grdInputToolRepairSend.Size = new System.Drawing.Size(1134, 340);
            this.grdInputToolRepairSend.TabIndex = 102;
            this.grdInputToolRepairSend.UseAutoBestFitColumns = false;
            // 
            // smartPanel4
            // 
            this.smartPanel4.Controls.Add(this.popEditVendor);
            this.smartPanel4.Controls.Add(this.cboToolRepairType);
            this.smartPanel4.Controls.Add(this.lblRepairVendor);
            this.smartPanel4.Controls.Add(this.txtSendor);
            this.smartPanel4.Controls.Add(this.txtSendSequence);
            this.smartPanel4.Controls.Add(this.lblSendDate);
            this.smartPanel4.Controls.Add(this.deSendDate);
            this.smartPanel4.Controls.Add(this.lblSendUser);
            this.smartPanel4.Controls.Add(this.lblRepairType);
            this.smartPanel4.Controls.Add(this.btnSearchTool);
            this.smartPanel4.Controls.Add(this.lblInputTitle);
            this.smartPanel4.Controls.Add(this.btnInitialize);
            this.smartPanel4.Controls.Add(this.btnErase);
            this.smartPanel4.Controls.Add(this.btnModify);
            this.smartPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartPanel4.Location = new System.Drawing.Point(0, 0);
            this.smartPanel4.Name = "smartPanel4";
            this.smartPanel4.Size = new System.Drawing.Size(1134, 37);
            this.smartPanel4.TabIndex = 2;
            // 
            // popEditVendor
            // 
            this.popEditVendor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.popEditVendor.LabelText = null;
            this.popEditVendor.LanguageKey = null;
            this.popEditVendor.Location = new System.Drawing.Point(148, 3);
            this.popEditVendor.Name = "popEditVendor";
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
            this.popEditVendor.SelectPopupCondition = conditionItemSelectPopup2;
            this.popEditVendor.Size = new System.Drawing.Size(42, 20);
            this.popEditVendor.TabIndex = 121;
            this.popEditVendor.Visible = false;
            // 
            // cboToolRepairType
            // 
            this.cboToolRepairType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboToolRepairType.LabelText = null;
            this.cboToolRepairType.LanguageKey = null;
            this.cboToolRepairType.Location = new System.Drawing.Point(507, 5);
            this.cboToolRepairType.Name = "cboToolRepairType";
            this.cboToolRepairType.PopupWidth = 0;
            this.cboToolRepairType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboToolRepairType.Properties.NullText = "";
            this.cboToolRepairType.ShowHeader = true;
            this.cboToolRepairType.Size = new System.Drawing.Size(27, 20);
            this.cboToolRepairType.TabIndex = 117;
            this.cboToolRepairType.Visible = false;
            this.cboToolRepairType.VisibleColumns = null;
            this.cboToolRepairType.VisibleColumnsWidth = null;
            // 
            // lblRepairVendor
            // 
            this.lblRepairVendor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRepairVendor.Appearance.Options.UseTextOptions = true;
            this.lblRepairVendor.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblRepairVendor.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblRepairVendor.LanguageKey = "REPAIRVENDOR";
            this.lblRepairVendor.Location = new System.Drawing.Point(73, 0);
            this.lblRepairVendor.Name = "lblRepairVendor";
            this.lblRepairVendor.Size = new System.Drawing.Size(69, 24);
            this.lblRepairVendor.TabIndex = 119;
            this.lblRepairVendor.Text = "수리업체:";
            this.lblRepairVendor.Visible = false;
            // 
            // txtSendor
            // 
            this.txtSendor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSendor.LabelText = null;
            this.txtSendor.LanguageKey = null;
            this.txtSendor.Location = new System.Drawing.Point(415, 7);
            this.txtSendor.Name = "txtSendor";
            this.txtSendor.Size = new System.Drawing.Size(26, 20);
            this.txtSendor.TabIndex = 118;
            this.txtSendor.Visible = false;
            // 
            // txtSendSequence
            // 
            this.txtSendSequence.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSendSequence.LabelText = null;
            this.txtSendSequence.LanguageKey = null;
            this.txtSendSequence.Location = new System.Drawing.Point(332, 5);
            this.txtSendSequence.Name = "txtSendSequence";
            this.txtSendSequence.Size = new System.Drawing.Size(32, 20);
            this.txtSendSequence.TabIndex = 117;
            this.txtSendSequence.Visible = false;
            // 
            // lblSendDate
            // 
            this.lblSendDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSendDate.Appearance.Options.UseTextOptions = true;
            this.lblSendDate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblSendDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblSendDate.LanguageKey = "SENDDATE";
            this.lblSendDate.Location = new System.Drawing.Point(196, 1);
            this.lblSendDate.Name = "lblSendDate";
            this.lblSendDate.Size = new System.Drawing.Size(84, 24);
            this.lblSendDate.TabIndex = 115;
            this.lblSendDate.Text = "출고일자:";
            this.lblSendDate.Visible = false;
            // 
            // deSendDate
            // 
            this.deSendDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deSendDate.EditValue = null;
            this.deSendDate.LabelText = null;
            this.deSendDate.LanguageKey = null;
            this.deSendDate.Location = new System.Drawing.Point(286, 5);
            this.deSendDate.Name = "deSendDate";
            this.deSendDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deSendDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deSendDate.Size = new System.Drawing.Size(40, 20);
            this.deSendDate.TabIndex = 116;
            this.deSendDate.Visible = false;
            // 
            // lblSendUser
            // 
            this.lblSendUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSendUser.Appearance.Options.UseTextOptions = true;
            this.lblSendUser.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblSendUser.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblSendUser.LanguageKey = "SENDOR";
            this.lblSendUser.Location = new System.Drawing.Point(370, 5);
            this.lblSendUser.Name = "lblSendUser";
            this.lblSendUser.Size = new System.Drawing.Size(39, 24);
            this.lblSendUser.TabIndex = 115;
            this.lblSendUser.Text = "출고자:";
            this.lblSendUser.Visible = false;
            // 
            // lblRepairType
            // 
            this.lblRepairType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRepairType.Appearance.Options.UseTextOptions = true;
            this.lblRepairType.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblRepairType.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblRepairType.LanguageKey = "TOOLREPAIRTYPE";
            this.lblRepairType.Location = new System.Drawing.Point(447, 4);
            this.lblRepairType.Name = "lblRepairType";
            this.lblRepairType.Size = new System.Drawing.Size(54, 24);
            this.lblRepairType.TabIndex = 5;
            this.lblRepairType.Text = "수리구분:";
            this.lblRepairType.Visible = false;
            // 
            // btnSearchTool
            // 
            this.btnSearchTool.AllowFocus = false;
            this.btnSearchTool.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchTool.IsBusy = false;
            this.btnSearchTool.IsWrite = false;
            this.btnSearchTool.LanguageKey = "CHOICETOOL";
            this.btnSearchTool.Location = new System.Drawing.Point(960, 7);
            this.btnSearchTool.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSearchTool.Name = "btnSearchTool";
            this.btnSearchTool.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSearchTool.Size = new System.Drawing.Size(80, 25);
            this.btnSearchTool.TabIndex = 111;
            this.btnSearchTool.Text = "Tool선택:";
            this.btnSearchTool.TooltipLanguageKey = "";
            this.btnSearchTool.Visible = false;
            // 
            // lblInputTitle
            // 
            this.lblInputTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblInputTitle.Appearance.Options.UseFont = true;
            this.lblInputTitle.LanguageKey = "REPAIRSENDTOOLLIST";
            this.lblInputTitle.Location = new System.Drawing.Point(5, 5);
            this.lblInputTitle.Name = "lblInputTitle";
            this.lblInputTitle.Size = new System.Drawing.Size(62, 19);
            this.lblInputTitle.TabIndex = 113;
            this.lblInputTitle.Text = "수리출고:";
            // 
            // btnInitialize
            // 
            this.btnInitialize.AllowFocus = false;
            this.btnInitialize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInitialize.IsBusy = false;
            this.btnInitialize.IsWrite = false;
            this.btnInitialize.LanguageKey = "CLEAR";
            this.btnInitialize.Location = new System.Drawing.Point(788, 7);
            this.btnInitialize.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnInitialize.Name = "btnInitialize";
            this.btnInitialize.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnInitialize.Size = new System.Drawing.Size(80, 25);
            this.btnInitialize.TabIndex = 112;
            this.btnInitialize.Text = "초기화:";
            this.btnInitialize.TooltipLanguageKey = "";
            this.btnInitialize.Visible = false;
            // 
            // btnErase
            // 
            this.btnErase.AllowFocus = false;
            this.btnErase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnErase.IsBusy = false;
            this.btnErase.IsWrite = false;
            this.btnErase.LanguageKey = "ERASE";
            this.btnErase.Location = new System.Drawing.Point(1046, 7);
            this.btnErase.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnErase.Name = "btnErase";
            this.btnErase.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnErase.Size = new System.Drawing.Size(80, 25);
            this.btnErase.TabIndex = 110;
            this.btnErase.Text = "삭제:";
            this.btnErase.TooltipLanguageKey = "";
            this.btnErase.Visible = false;
            // 
            // btnModify
            // 
            this.btnModify.AllowFocus = false;
            this.btnModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModify.IsBusy = false;
            this.btnModify.IsWrite = false;
            this.btnModify.LanguageKey = "SAVE";
            this.btnModify.Location = new System.Drawing.Point(874, 7);
            this.btnModify.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnModify.Name = "btnModify";
            this.btnModify.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnModify.Size = new System.Drawing.Size(80, 25);
            this.btnModify.TabIndex = 111;
            this.btnModify.Text = "저장:";
            this.btnModify.TooltipLanguageKey = "";
            this.btnModify.Visible = false;
            // 
            // RepairToolRequestSend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1459, 778);
            this.Name = "RepairToolRequestSend";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.smartPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel4)).EndInit();
            this.smartPanel4.ResumeLayout(false);
            this.smartPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popEditVendor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboToolRepairType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendSequence.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deSendDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deSendDate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdToolRepiarSend;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartLabel smartLabel1;
        private Framework.SmartControls.SmartBandedGrid grdInputToolRepairSend;
        private Framework.SmartControls.SmartPanel smartPanel4;
        private Framework.SmartControls.SmartLabel lblRepairType;
        private Framework.SmartControls.SmartButton btnSearchTool;
        private Framework.SmartControls.SmartLabel lblInputTitle;
        private Framework.SmartControls.SmartButton btnInitialize;
        private Framework.SmartControls.SmartButton btnErase;
        private Framework.SmartControls.SmartButton btnModify;
        private Framework.SmartControls.SmartLabel lblSendDate;
        private Framework.SmartControls.SmartDateEdit deSendDate;
        private Framework.SmartControls.SmartLabel lblSendUser;
        private Framework.SmartControls.SmartLabel lblRepairVendor;
        private Framework.SmartControls.SmartTextBox txtSendor;
        private Framework.SmartControls.SmartTextBox txtSendSequence;
        private Framework.SmartControls.SmartComboBox cboToolRepairType;
        private Framework.SmartControls.SmartSelectPopupEdit popEditVendor;
    }
}