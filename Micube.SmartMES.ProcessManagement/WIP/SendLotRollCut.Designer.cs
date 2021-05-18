namespace Micube.SmartMES.ProcessManagement
{
    partial class SendLotRollCut
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendLotRollCut));
            Micube.Framework.SmartControls.ConditionCollection conditionCollection3 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection4 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            this.grdWIP = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.spcSpliter = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.ucDataUpDownBtnCtrl = new Micube.SmartMES.Commons.Controls.ucDataUpDownBtn();
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.lblArea = new Micube.Framework.SmartControls.SmartLabel();
            this.lblComment = new Micube.Framework.SmartControls.SmartLabel();
            this.txtComment = new Micube.Framework.SmartControls.SmartTextBox();
            this.cboArea = new Micube.Framework.SmartControls.SmartComboBox();
            this.txtWorker = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            this.lblWorker = new Micube.Framework.SmartControls.SmartLabel();
            this.txtWorkerID = new Micube.Framework.SmartControls.SmartTextBox();
            this.smartSplitTableLayoutPanel3 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdCreateLot = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSplitTableLayoutPanel4 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.btnUp = new Micube.Framework.SmartControls.SmartButton();
            this.btnDown = new Micube.Framework.SmartControls.SmartButton();
            this.lblLotSize = new Micube.Framework.SmartControls.SmartLabel();
            this.txtLotSize = new Micube.Framework.SmartControls.SmartTextBox();
            this.btnCreate = new Micube.Framework.SmartControls.SmartButton();
            this.btnPrint = new Micube.Framework.SmartControls.SmartButton();
            this.chkPrintLotcard = new Micube.Framework.SmartControls.SmartCheckBox();
            this.chkMerge = new Micube.Framework.SmartControls.SmartCheckBox();
            this.smartButton1 = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorker.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkerID.Properties)).BeginInit();
            this.smartSplitTableLayoutPanel3.SuspendLayout();
            this.smartSplitTableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotSize.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPrintLotcard.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkMerge.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.smartButton1);
            this.pnlToolbar.Controls.SetChildIndex(this.smartButton1, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlContent.Controls.Add(this.spcSpliter);
            // 
            // grdWIP
            // 
            this.grdWIP.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdWIP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdWIP.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdWIP.IsUsePaging = false;
            this.grdWIP.LanguageKey = "WIPLIST";
            this.grdWIP.Location = new System.Drawing.Point(0, 0);
            this.grdWIP.Margin = new System.Windows.Forms.Padding(0);
            this.grdWIP.Name = "grdWIP";
            this.grdWIP.ShowBorder = true;
            this.grdWIP.Size = new System.Drawing.Size(756, 168);
            this.grdWIP.TabIndex = 1;
            this.grdWIP.UseAutoBestFitColumns = false;
            // 
            // grdLotList
            // 
            this.grdLotList.Caption = "Lot List";
            this.grdLotList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLotList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLotList.IsUsePaging = false;
            this.grdLotList.LanguageKey = "";
            this.grdLotList.Location = new System.Drawing.Point(0, 30);
            this.grdLotList.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotList.Name = "grdLotList";
            this.grdLotList.ShowBorder = true;
            this.grdLotList.Size = new System.Drawing.Size(410, 176);
            this.grdLotList.TabIndex = 2;
            this.grdLotList.UseAutoBestFitColumns = false;
            // 
            // spcSpliter
            // 
            this.spcSpliter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.spcSpliter.Location = new System.Drawing.Point(0, 484);
            this.spcSpliter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcSpliter.Name = "spcSpliter";
            this.spcSpliter.Size = new System.Drawing.Size(756, 5);
            this.spcSpliter.TabIndex = 5;
            this.spcSpliter.TabStop = false;
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdWIP, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.ucDataUpDownBtnCtrl, 0, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartSplitTableLayoutPanel2, 0, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartSplitTableLayoutPanel3, 0, 3);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 4;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.1282F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.8718F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(756, 484);
            this.smartSplitTableLayoutPanel1.TabIndex = 6;
            // 
            // ucDataUpDownBtnCtrl
            // 
            this.ucDataUpDownBtnCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDataUpDownBtnCtrl.Location = new System.Drawing.Point(3, 171);
            this.ucDataUpDownBtnCtrl.Name = "ucDataUpDownBtnCtrl";
            this.ucDataUpDownBtnCtrl.Size = new System.Drawing.Size(750, 44);
            this.ucDataUpDownBtnCtrl.SourceGrid = null;
            this.ucDataUpDownBtnCtrl.TabIndex = 2;
            this.ucDataUpDownBtnCtrl.TargetGrid = null;
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.ColumnCount = 12;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.60371F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.39629F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.lblArea, 0, 0);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.lblComment, 0, 1);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.txtComment, 2, 1);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.cboArea, 2, 0);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.txtWorker, 8, 0);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.lblWorker, 10, 0);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.txtWorkerID, 4, 0);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(0, 218);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 3;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(756, 60);
            this.smartSplitTableLayoutPanel2.TabIndex = 3;
            // 
            // lblArea
            // 
            this.lblArea.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblArea.Appearance.Options.UseForeColor = true;
            this.lblArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblArea.LanguageKey = "SENDRESOURCEID";
            this.lblArea.Location = new System.Drawing.Point(3, 3);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(74, 24);
            this.lblArea.TabIndex = 0;
            this.lblArea.Text = "smartLabel1";
            // 
            // lblComment
            // 
            this.lblComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblComment.LanguageKey = "COMMENT";
            this.lblComment.Location = new System.Drawing.Point(3, 33);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(74, 24);
            this.lblComment.TabIndex = 4;
            this.lblComment.Text = "smartLabel3";
            // 
            // txtComment
            // 
            this.smartSplitTableLayoutPanel2.SetColumnSpan(this.txtComment, 7);
            this.txtComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtComment.LabelText = null;
            this.txtComment.LanguageKey = null;
            this.txtComment.Location = new System.Drawing.Point(88, 33);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(523, 20);
            this.txtComment.TabIndex = 5;
            // 
            // cboArea
            // 
            this.cboArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboArea.LabelText = null;
            this.cboArea.LanguageKey = null;
            this.cboArea.Location = new System.Drawing.Point(88, 3);
            this.cboArea.Name = "cboArea";
            this.cboArea.PopupWidth = 0;
            this.cboArea.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboArea.Properties.NullText = "";
            this.cboArea.ShowHeader = true;
            this.cboArea.Size = new System.Drawing.Size(144, 20);
            this.cboArea.TabIndex = 6;
            this.cboArea.VisibleColumns = null;
            this.cboArea.VisibleColumnsWidth = null;
            // 
            // txtWorker
            // 
            this.txtWorker.LabelText = null;
            this.txtWorker.LanguageKey = null;
            this.txtWorker.Location = new System.Drawing.Point(553, 3);
            this.txtWorker.Name = "txtWorker";
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
            this.txtWorker.SelectPopupCondition = conditionItemSelectPopup2;
            this.txtWorker.Size = new System.Drawing.Size(58, 20);
            this.txtWorker.TabIndex = 3;
            this.txtWorker.Visible = false;
            // 
            // lblWorker
            // 
            this.lblWorker.LanguageKey = "WORKER";
            this.lblWorker.Location = new System.Drawing.Point(622, 3);
            this.lblWorker.Name = "lblWorker";
            this.lblWorker.Size = new System.Drawing.Size(65, 14);
            this.lblWorker.TabIndex = 1;
            this.lblWorker.Text = "smartLabel2";
            this.lblWorker.Visible = false;
            // 
            // txtWorkerID
            // 
            this.txtWorkerID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWorkerID.LabelText = null;
            this.txtWorkerID.LanguageKey = null;
            this.txtWorkerID.Location = new System.Drawing.Point(243, 3);
            this.txtWorkerID.Name = "txtWorkerID";
            this.txtWorkerID.Properties.ReadOnly = true;
            this.txtWorkerID.Size = new System.Drawing.Size(144, 20);
            this.txtWorkerID.TabIndex = 7;
            // 
            // smartSplitTableLayoutPanel3
            // 
            this.smartSplitTableLayoutPanel3.ColumnCount = 2;
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.34939F));
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.65061F));
            this.smartSplitTableLayoutPanel3.Controls.Add(this.grdCreateLot, 1, 2);
            this.smartSplitTableLayoutPanel3.Controls.Add(this.smartSplitTableLayoutPanel4, 0, 0);
            this.smartSplitTableLayoutPanel3.Controls.Add(this.grdLotList, 0, 2);
            this.smartSplitTableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel3.Location = new System.Drawing.Point(0, 278);
            this.smartSplitTableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel3.Name = "smartSplitTableLayoutPanel3";
            this.smartSplitTableLayoutPanel3.RowCount = 3;
            this.smartSplitTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.smartSplitTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.smartSplitTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel3.Size = new System.Drawing.Size(756, 206);
            this.smartSplitTableLayoutPanel3.TabIndex = 4;
            // 
            // grdCreateLot
            // 
            this.grdCreateLot.Caption = "Lot List";
            this.grdCreateLot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCreateLot.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdCreateLot.IsUsePaging = false;
            this.grdCreateLot.LanguageKey = "";
            this.grdCreateLot.Location = new System.Drawing.Point(410, 30);
            this.grdCreateLot.Margin = new System.Windows.Forms.Padding(0);
            this.grdCreateLot.Name = "grdCreateLot";
            this.grdCreateLot.ShowBorder = true;
            this.grdCreateLot.Size = new System.Drawing.Size(346, 176);
            this.grdCreateLot.TabIndex = 3;
            this.grdCreateLot.UseAutoBestFitColumns = false;
            // 
            // smartSplitTableLayoutPanel4
            // 
            this.smartSplitTableLayoutPanel4.ColumnCount = 13;
            this.smartSplitTableLayoutPanel3.SetColumnSpan(this.smartSplitTableLayoutPanel4, 2);
            this.smartSplitTableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.smartSplitTableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.smartSplitTableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.smartSplitTableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.smartSplitTableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.smartSplitTableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.smartSplitTableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.smartSplitTableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.smartSplitTableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.smartSplitTableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.smartSplitTableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel4.Controls.Add(this.btnUp, 0, 0);
            this.smartSplitTableLayoutPanel4.Controls.Add(this.btnDown, 1, 0);
            this.smartSplitTableLayoutPanel4.Controls.Add(this.lblLotSize, 2, 0);
            this.smartSplitTableLayoutPanel4.Controls.Add(this.txtLotSize, 4, 0);
            this.smartSplitTableLayoutPanel4.Controls.Add(this.btnCreate, 6, 0);
            this.smartSplitTableLayoutPanel4.Controls.Add(this.btnPrint, 11, 0);
            this.smartSplitTableLayoutPanel4.Controls.Add(this.chkPrintLotcard, 8, 0);
            this.smartSplitTableLayoutPanel4.Controls.Add(this.chkMerge, 10, 0);
            this.smartSplitTableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel4.Name = "smartSplitTableLayoutPanel4";
            this.smartSplitTableLayoutPanel4.RowCount = 1;
            this.smartSplitTableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel4.Size = new System.Drawing.Size(756, 25);
            this.smartSplitTableLayoutPanel4.TabIndex = 0;
            // 
            // btnUp
            // 
            this.btnUp.AllowFocus = false;
            this.btnUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUp.IsBusy = false;
            this.btnUp.IsWrite = false;
            this.btnUp.Location = new System.Drawing.Point(3, 0);
            this.btnUp.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnUp.Name = "btnUp";
            this.btnUp.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnUp.Size = new System.Drawing.Size(34, 25);
            this.btnUp.TabIndex = 0;
            this.btnUp.Text = "▲";
            this.btnUp.TooltipLanguageKey = "";
            // 
            // btnDown
            // 
            this.btnDown.AllowFocus = false;
            this.btnDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDown.IsBusy = false;
            this.btnDown.IsWrite = false;
            this.btnDown.Location = new System.Drawing.Point(43, 0);
            this.btnDown.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnDown.Name = "btnDown";
            this.btnDown.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnDown.Size = new System.Drawing.Size(34, 25);
            this.btnDown.TabIndex = 1;
            this.btnDown.Text = "▼";
            this.btnDown.TooltipLanguageKey = "";
            // 
            // lblLotSize
            // 
            this.lblLotSize.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblLotSize.Appearance.Options.UseForeColor = true;
            this.lblLotSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLotSize.LanguageKey = "LOTSIZE";
            this.lblLotSize.Location = new System.Drawing.Point(83, 3);
            this.lblLotSize.Name = "lblLotSize";
            this.lblLotSize.Size = new System.Drawing.Size(48, 19);
            this.lblLotSize.TabIndex = 2;
            this.lblLotSize.Text = "smartLabel1";
            // 
            // txtLotSize
            // 
            this.txtLotSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLotSize.LabelText = null;
            this.txtLotSize.LanguageKey = null;
            this.txtLotSize.Location = new System.Drawing.Point(142, 3);
            this.txtLotSize.Name = "txtLotSize";
            this.txtLotSize.Size = new System.Drawing.Size(94, 20);
            this.txtLotSize.TabIndex = 3;
            // 
            // btnCreate
            // 
            this.btnCreate.AllowFocus = false;
            this.btnCreate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCreate.IsBusy = false;
            this.btnCreate.IsWrite = false;
            this.btnCreate.LanguageKey = "CREATELOT";
            this.btnCreate.Location = new System.Drawing.Point(247, 0);
            this.btnCreate.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCreate.Size = new System.Drawing.Size(64, 25);
            this.btnCreate.TabIndex = 4;
            this.btnCreate.Text = "smartButton3";
            this.btnCreate.TooltipLanguageKey = "";
            // 
            // btnPrint
            // 
            this.btnPrint.AllowFocus = false;
            this.btnPrint.IsBusy = false;
            this.btnPrint.IsWrite = false;
            this.btnPrint.Location = new System.Drawing.Point(577, 0);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPrint.Size = new System.Drawing.Size(44, 25);
            this.btnPrint.TabIndex = 7;
            this.btnPrint.Text = "smartButton1";
            this.btnPrint.TooltipLanguageKey = "";
            this.btnPrint.Visible = false;
            // 
            // chkPrintLotcard
            // 
            this.chkPrintLotcard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkPrintLotcard.LanguageKey = "LOTCARDPRINT";
            this.chkPrintLotcard.Location = new System.Drawing.Point(327, 3);
            this.chkPrintLotcard.Name = "chkPrintLotcard";
            this.chkPrintLotcard.Properties.Caption = "smartCheckBox1";
            this.chkPrintLotcard.Size = new System.Drawing.Size(114, 19);
            this.chkPrintLotcard.TabIndex = 6;
            // 
            // chkMerge
            // 
            this.chkMerge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkMerge.LanguageKey = "MERGE";
            this.chkMerge.Location = new System.Drawing.Point(457, 3);
            this.chkMerge.Name = "chkMerge";
            this.chkMerge.Properties.Caption = "smartCheckBox1";
            this.chkMerge.Properties.ReadOnly = true;
            this.chkMerge.Size = new System.Drawing.Size(114, 19);
            this.chkMerge.TabIndex = 5;
            // 
            // smartButton1
            // 
            this.smartButton1.AllowFocus = false;
            this.smartButton1.IsBusy = false;
            this.smartButton1.IsWrite = false;
            this.smartButton1.Location = new System.Drawing.Point(311, 5);
            this.smartButton1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.smartButton1.Name = "smartButton1";
            this.smartButton1.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.smartButton1.Size = new System.Drawing.Size(87, 14);
            this.smartButton1.TabIndex = 5;
            this.smartButton1.Text = "smartButton1";
            this.smartButton1.TooltipLanguageKey = "";
            this.smartButton1.Visible = false;
            this.smartButton1.Click += new System.EventHandler(this.smartButton1_Click);
            // 
            // SendLotRollCut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 538);
            this.Name = "SendLotRollCut";
            this.Text = "LotLocking";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorker.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkerID.Properties)).EndInit();
            this.smartSplitTableLayoutPanel3.ResumeLayout(false);
            this.smartSplitTableLayoutPanel4.ResumeLayout(false);
            this.smartSplitTableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotSize.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPrintLotcard.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkMerge.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdWIP;
        private Framework.SmartControls.SmartSpliterControl spcSpliter;
        private Framework.SmartControls.SmartBandedGrid grdLotList;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Commons.Controls.ucDataUpDownBtn ucDataUpDownBtnCtrl;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
        private Framework.SmartControls.SmartLabel lblArea;
        private Framework.SmartControls.SmartLabel lblWorker;
        private Framework.SmartControls.SmartSelectPopupEdit txtWorker;
        private Framework.SmartControls.SmartLabel lblComment;
        private Framework.SmartControls.SmartTextBox txtComment;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel3;
        private Framework.SmartControls.SmartBandedGrid grdCreateLot;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel4;
        private Framework.SmartControls.SmartButton btnUp;
        private Framework.SmartControls.SmartButton btnDown;
        private Framework.SmartControls.SmartLabel lblLotSize;
        private Framework.SmartControls.SmartTextBox txtLotSize;
        private Framework.SmartControls.SmartButton btnCreate;
        private Framework.SmartControls.SmartCheckBox chkMerge;
        private Framework.SmartControls.SmartComboBox cboArea;
        private Framework.SmartControls.SmartCheckBox chkPrintLotcard;
        private Framework.SmartControls.SmartButton btnPrint;
        private Framework.SmartControls.SmartButton smartButton1;
        private Framework.SmartControls.SmartTextBox txtWorkerID;
    }
}