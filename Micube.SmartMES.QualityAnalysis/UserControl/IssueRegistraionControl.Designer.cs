namespace Micube.SmartMES.QualityAnalysis
{
    partial class IssueRegistrationControl
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            Micube.Framework.SmartControls.ConditionItemSelectPopup conditionItemSelectPopup1 = new Micube.Framework.SmartControls.ConditionItemSelectPopup();
            Micube.Framework.SmartControls.ConditionCollection conditionCollection1 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection2 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            Micube.Framework.SmartControls.ConditionItemSelectPopup conditionItemSelectPopup2 = new Micube.Framework.SmartControls.ConditionItemSelectPopup();
            Micube.Framework.SmartControls.ConditionCollection conditionCollection3 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection4 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IssueRegistrationControl));
            Micube.Framework.SmartControls.ConditionItemSelectPopup conditionItemSelectPopup3 = new Micube.Framework.SmartControls.ConditionItemSelectPopup();
            Micube.Framework.SmartControls.ConditionCollection conditionCollection5 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection6 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            this.tabCarProgress = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgCarReqeust = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.lblReqestDescription = new Micube.Framework.SmartControls.SmartLabel();
            this.memoRequestDescription = new Micube.Framework.SmartControls.SmartMemoEdit();
            this.lblManager = new Micube.Framework.SmartControls.SmartLabel();
            this.popupManager = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            this.cboRequestNumber = new Micube.Framework.SmartControls.SmartComboBox();
            this.txtRequestDate = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblRequestNumber = new Micube.Framework.SmartControls.SmartLabel();
            this.lblRequestDate = new Micube.Framework.SmartControls.SmartLabel();
            this.chkRequestMeasure = new Micube.Framework.SmartControls.SmartCheckBox();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnDownload = new Micube.Framework.SmartControls.SmartButton();
            this.btnRequestReset = new Micube.Framework.SmartControls.SmartButton();
            this.btnReqestSave = new Micube.Framework.SmartControls.SmartButton();
            this.tblReason = new System.Windows.Forms.TableLayoutPanel();
            this.cboReasonSegmentId = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.cboReasonLotId = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.cboReasonIdVersion = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.txtReasonArea = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.tpgCarReceipt = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblReceiptDescription = new Micube.Framework.SmartControls.SmartLabel();
            this.memoReceiptDescription = new Micube.Framework.SmartControls.SmartMemoEdit();
            this.lblReceiptor = new Micube.Framework.SmartControls.SmartLabel();
            this.popupReceiptor = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            this.cboReceiptNumber = new Micube.Framework.SmartControls.SmartComboBox();
            this.txtReceiptDate = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblReceiptNumber = new Micube.Framework.SmartControls.SmartLabel();
            this.lblReceiptDate = new Micube.Framework.SmartControls.SmartLabel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnReceiptReset = new Micube.Framework.SmartControls.SmartButton();
            this.btnReceiptSave = new Micube.Framework.SmartControls.SmartButton();
            this.fileReceiptControl = new Micube.SmartMES.Commons.Controls.SmartFileProcessingControl();
            this.tpgAccept = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.lblAcceptNumber = new Micube.Framework.SmartControls.SmartLabel();
            this.cboAcceptNumber = new Micube.Framework.SmartControls.SmartComboBox();
            this.flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAcceptReset = new Micube.Framework.SmartControls.SmartButton();
            this.btnAcceptSave = new Micube.Framework.SmartControls.SmartButton();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdCARAccept = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdValidationHistory = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgValidation = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.smartGroupBox4 = new Micube.Framework.SmartControls.SmartGroupBox();
            this.tableLayoutPanel15 = new System.Windows.Forms.TableLayoutPanel();
            this.txtConcurrence = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblIsclose = new Micube.Framework.SmartControls.SmartLabel();
            this.cboIsclose = new Micube.Framework.SmartControls.SmartComboBox();
            this.lblConcurrence = new Micube.Framework.SmartControls.SmartLabel();
            this.dtrSearchDate = new Micube.Framework.SmartControls.SmartDateRangeEdit();
            this.lblSearchDate = new Micube.Framework.SmartControls.SmartLabel();
            this.smartGroupBox1 = new Micube.Framework.SmartControls.SmartGroupBox();
            this.tableLayoutPanel14 = new System.Windows.Forms.TableLayoutPanel();
            this.lblCheckDescription = new Micube.Framework.SmartControls.SmartLabel();
            this.memoCheckDescription = new Micube.Framework.SmartControls.SmartMemoEdit();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            this.popupInspector = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            this.lblCheckResult = new Micube.Framework.SmartControls.SmartLabel();
            this.cboCheckResult = new Micube.Framework.SmartControls.SmartComboBox();
            this.lblCheckDate = new Micube.Framework.SmartControls.SmartLabel();
            this.txtCheckDate = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblValidationNumber = new Micube.Framework.SmartControls.SmartLabel();
            this.cboValidationNumber = new Micube.Framework.SmartControls.SmartComboBox();
            this.flowLayoutPanel8 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnValidationReset = new Micube.Framework.SmartControls.SmartButton();
            this.btnValidationSave = new Micube.Framework.SmartControls.SmartButton();
            this.fileValidationControl = new Micube.SmartMES.Commons.Controls.SmartFileProcessingControl();
            this.grdFileList2 = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.tabCarProgress)).BeginInit();
            this.tabCarProgress.SuspendLayout();
            this.tpgCarReqeust.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoRequestDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupManager.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboRequestNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRequestDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRequestMeasure.Properties)).BeginInit();
            this.flowLayoutPanel5.SuspendLayout();
            this.tblReason.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboReasonSegmentId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboReasonLotId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboReasonIdVersion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReasonArea.Properties)).BeginInit();
            this.tpgCarReceipt.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoReceiptDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupReceiptor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboReceiptNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiptDate.Properties)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.tpgAccept.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboAcceptNumber.Properties)).BeginInit();
            this.flowLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            this.tpgValidation.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox4)).BeginInit();
            this.smartGroupBox4.SuspendLayout();
            this.tableLayoutPanel15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtConcurrence.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboIsclose.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtrSearchDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).BeginInit();
            this.smartGroupBox1.SuspendLayout();
            this.tableLayoutPanel14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoCheckDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupInspector.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCheckResult.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCheckDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboValidationNumber.Properties)).BeginInit();
            this.flowLayoutPanel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCarProgress
            // 
            this.tabCarProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCarProgress.Location = new System.Drawing.Point(0, 0);
            this.tabCarProgress.Margin = new System.Windows.Forms.Padding(0);
            this.tabCarProgress.Name = "tabCarProgress";
            this.tabCarProgress.SelectedTabPage = this.tpgCarReqeust;
            this.tabCarProgress.Size = new System.Drawing.Size(821, 436);
            this.tabCarProgress.TabIndex = 5;
            this.tabCarProgress.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgCarReqeust,
            this.tpgCarReceipt,
            this.tpgAccept,
            this.tpgValidation});
            // 
            // tpgCarReqeust
            // 
            this.tpgCarReqeust.Controls.Add(this.tableLayoutPanel9);
            this.tabCarProgress.SetLanguageKey(this.tpgCarReqeust, "CARREQUEST");
            this.tpgCarReqeust.Name = "tpgCarReqeust";
            this.tpgCarReqeust.Size = new System.Drawing.Size(815, 407);
            this.tpgCarReqeust.Text = "CAR 요청";
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 6;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.37357F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.28791F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.89098F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.74381F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.70374F));
            this.tableLayoutPanel9.Controls.Add(this.lblReqestDescription, 1, 7);
            this.tableLayoutPanel9.Controls.Add(this.memoRequestDescription, 2, 7);
            this.tableLayoutPanel9.Controls.Add(this.lblManager, 4, 3);
            this.tableLayoutPanel9.Controls.Add(this.popupManager, 5, 3);
            this.tableLayoutPanel9.Controls.Add(this.cboRequestNumber, 2, 1);
            this.tableLayoutPanel9.Controls.Add(this.txtRequestDate, 2, 3);
            this.tableLayoutPanel9.Controls.Add(this.lblRequestNumber, 1, 1);
            this.tableLayoutPanel9.Controls.Add(this.lblRequestDate, 1, 3);
            this.tableLayoutPanel9.Controls.Add(this.chkRequestMeasure, 3, 3);
            this.tableLayoutPanel9.Controls.Add(this.flowLayoutPanel5, 4, 1);
            this.tableLayoutPanel9.Controls.Add(this.tblReason, 1, 5);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel9.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 8;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(815, 407);
            this.tableLayoutPanel9.TabIndex = 5;
            // 
            // lblReqestDescription
            // 
            this.lblReqestDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReqestDescription.LanguageKey = "REMARK";
            this.lblReqestDescription.Location = new System.Drawing.Point(5, 117);
            this.lblReqestDescription.Margin = new System.Windows.Forms.Padding(0);
            this.lblReqestDescription.Name = "lblReqestDescription";
            this.lblReqestDescription.Size = new System.Drawing.Size(116, 290);
            this.lblReqestDescription.TabIndex = 3;
            this.lblReqestDescription.Text = "비고 :";
            // 
            // memoRequestDescription
            // 
            this.tableLayoutPanel9.SetColumnSpan(this.memoRequestDescription, 4);
            this.memoRequestDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoRequestDescription.EditValue = " ";
            this.memoRequestDescription.Location = new System.Drawing.Point(121, 117);
            this.memoRequestDescription.Margin = new System.Windows.Forms.Padding(0);
            this.memoRequestDescription.Name = "memoRequestDescription";
            this.memoRequestDescription.Size = new System.Drawing.Size(694, 290);
            this.memoRequestDescription.TabIndex = 4;
            // 
            // lblManager
            // 
            this.lblManager.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblManager.LanguageKey = "MANAGER";
            this.lblManager.Location = new System.Drawing.Point(446, 33);
            this.lblManager.Margin = new System.Windows.Forms.Padding(0);
            this.lblManager.Name = "lblManager";
            this.lblManager.Size = new System.Drawing.Size(87, 23);
            this.lblManager.TabIndex = 5;
            this.lblManager.Text = "담당자";
            // 
            // popupManager
            // 
            this.popupManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.popupManager.EditValue = "";
            this.popupManager.LabelText = "";
            this.popupManager.LanguageKey = null;
            this.popupManager.Location = new System.Drawing.Point(533, 33);
            this.popupManager.Margin = new System.Windows.Forms.Padding(0);
            this.popupManager.Name = "popupManager";
            this.popupManager.Properties.AutoHeight = false;
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
            this.popupManager.SelectPopupCondition = conditionItemSelectPopup1;
            this.popupManager.Size = new System.Drawing.Size(282, 23);
            this.popupManager.TabIndex = 6;
            // 
            // cboRequestNumber
            // 
            this.cboRequestNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboRequestNumber.LabelText = null;
            this.cboRequestNumber.LanguageKey = null;
            this.cboRequestNumber.Location = new System.Drawing.Point(121, 5);
            this.cboRequestNumber.Margin = new System.Windows.Forms.Padding(0);
            this.cboRequestNumber.Name = "cboRequestNumber";
            this.cboRequestNumber.PopupWidth = 0;
            this.cboRequestNumber.Properties.AutoHeight = false;
            this.cboRequestNumber.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboRequestNumber.Properties.NullText = "";
            this.cboRequestNumber.ShowHeader = true;
            this.cboRequestNumber.Size = new System.Drawing.Size(237, 23);
            this.cboRequestNumber.TabIndex = 7;
            this.cboRequestNumber.VisibleColumns = null;
            this.cboRequestNumber.VisibleColumnsWidth = null;
            // 
            // txtRequestDate
            // 
            this.txtRequestDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRequestDate.LabelText = null;
            this.txtRequestDate.LanguageKey = null;
            this.txtRequestDate.Location = new System.Drawing.Point(121, 33);
            this.txtRequestDate.Margin = new System.Windows.Forms.Padding(0);
            this.txtRequestDate.Name = "txtRequestDate";
            this.txtRequestDate.Properties.AutoHeight = false;
            this.txtRequestDate.Properties.ReadOnly = true;
            this.txtRequestDate.Size = new System.Drawing.Size(237, 23);
            this.txtRequestDate.TabIndex = 8;
            // 
            // lblRequestNumber
            // 
            this.lblRequestNumber.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblRequestNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRequestNumber.LanguageKey = "CARREQUESTSEQUENCE";
            this.lblRequestNumber.Location = new System.Drawing.Point(5, 5);
            this.lblRequestNumber.Margin = new System.Windows.Forms.Padding(0);
            this.lblRequestNumber.Name = "lblRequestNumber";
            this.lblRequestNumber.Size = new System.Drawing.Size(116, 23);
            this.lblRequestNumber.TabIndex = 9;
            this.lblRequestNumber.Text = "CAR 요청순번";
            // 
            // lblRequestDate
            // 
            this.lblRequestDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblRequestDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRequestDate.LanguageKey = "CARREQUESTDATE";
            this.lblRequestDate.Location = new System.Drawing.Point(5, 33);
            this.lblRequestDate.Margin = new System.Windows.Forms.Padding(0);
            this.lblRequestDate.Name = "lblRequestDate";
            this.lblRequestDate.Size = new System.Drawing.Size(116, 23);
            this.lblRequestDate.TabIndex = 10;
            this.lblRequestDate.Text = "CAR 요청날짜";
            // 
            // chkRequestMeasure
            // 
            this.chkRequestMeasure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkRequestMeasure.LanguageKey = "REQUESTFORACTION";
            this.chkRequestMeasure.Location = new System.Drawing.Point(363, 33);
            this.chkRequestMeasure.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.chkRequestMeasure.Name = "chkRequestMeasure";
            this.chkRequestMeasure.Properties.AutoHeight = false;
            this.chkRequestMeasure.Properties.Caption = "대책요청";
            this.chkRequestMeasure.Size = new System.Drawing.Size(83, 23);
            this.chkRequestMeasure.TabIndex = 11;
            // 
            // flowLayoutPanel5
            // 
            this.tableLayoutPanel9.SetColumnSpan(this.flowLayoutPanel5, 2);
            this.flowLayoutPanel5.Controls.Add(this.btnDownload);
            this.flowLayoutPanel5.Controls.Add(this.btnRequestReset);
            this.flowLayoutPanel5.Controls.Add(this.btnReqestSave);
            this.flowLayoutPanel5.Controls.Add(this.grdFileList2);
            this.flowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel5.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(446, 5);
            this.flowLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(369, 23);
            this.flowLayoutPanel5.TabIndex = 12;
            // 
            // btnDownload
            // 
            this.btnDownload.AllowFocus = false;
            this.btnDownload.IsBusy = false;
            this.btnDownload.IsWrite = false;
            this.btnDownload.LanguageKey = "DOWNLOADMEASURESFORM";
            this.btnDownload.Location = new System.Drawing.Point(237, 0);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnDownload.Size = new System.Drawing.Size(132, 23);
            this.btnDownload.TabIndex = 0;
            this.btnDownload.Text = "대책서 양식 다운로드";
            this.btnDownload.TooltipLanguageKey = "";
            // 
            // btnRequestReset
            // 
            this.btnRequestReset.AllowFocus = false;
            this.btnRequestReset.IsBusy = false;
            this.btnRequestReset.IsWrite = false;
            this.btnRequestReset.LanguageKey = "CLEAR";
            this.btnRequestReset.Location = new System.Drawing.Point(156, 0);
            this.btnRequestReset.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnRequestReset.Name = "btnRequestReset";
            this.btnRequestReset.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnRequestReset.Size = new System.Drawing.Size(75, 23);
            this.btnRequestReset.TabIndex = 1;
            this.btnRequestReset.Text = "초기화";
            this.btnRequestReset.TooltipLanguageKey = "";
            // 
            // btnReqestSave
            // 
            this.btnReqestSave.AllowFocus = false;
            this.btnReqestSave.IsBusy = false;
            this.btnReqestSave.IsWrite = false;
            this.btnReqestSave.LanguageKey = "TEMPSAVE";
            this.btnReqestSave.Location = new System.Drawing.Point(75, 0);
            this.btnReqestSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnReqestSave.Name = "btnReqestSave";
            this.btnReqestSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnReqestSave.Size = new System.Drawing.Size(75, 23);
            this.btnReqestSave.TabIndex = 2;
            this.btnReqestSave.Text = "임시저장";
            this.btnReqestSave.TooltipLanguageKey = "";
            // 
            // tblReason
            // 
            this.tblReason.ColumnCount = 2;
            this.tableLayoutPanel9.SetColumnSpan(this.tblReason, 5);
            this.tblReason.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblReason.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblReason.Controls.Add(this.cboReasonSegmentId, 0, 2);
            this.tblReason.Controls.Add(this.cboReasonLotId, 1, 0);
            this.tblReason.Controls.Add(this.cboReasonIdVersion, 0, 0);
            this.tblReason.Controls.Add(this.txtReasonArea, 1, 2);
            this.tblReason.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblReason.Location = new System.Drawing.Point(5, 61);
            this.tblReason.Margin = new System.Windows.Forms.Padding(0);
            this.tblReason.Name = "tblReason";
            this.tblReason.RowCount = 3;
            this.tblReason.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblReason.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tblReason.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblReason.Size = new System.Drawing.Size(810, 51);
            this.tblReason.TabIndex = 13;
            // 
            // cboReasonSegmentId
            // 
            this.cboReasonSegmentId.AutoHeight = false;
            this.cboReasonSegmentId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboReasonSegmentId.LabelText = "원인공정";
            this.cboReasonSegmentId.LabelWidth = "21.5%";
            this.cboReasonSegmentId.LanguageKey = "REASONPROCESSSEGMENTID";
            this.cboReasonSegmentId.Location = new System.Drawing.Point(0, 28);
            this.cboReasonSegmentId.Margin = new System.Windows.Forms.Padding(0);
            this.cboReasonSegmentId.Name = "cboReasonSegmentId";
            this.cboReasonSegmentId.Properties.AutoHeight = false;
            this.cboReasonSegmentId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboReasonSegmentId.Properties.NullText = "";
            this.cboReasonSegmentId.Size = new System.Drawing.Size(405, 23);
            this.cboReasonSegmentId.TabIndex = 2;
            // 
            // cboReasonLotId
            // 
            this.cboReasonLotId.AutoHeight = false;
            this.cboReasonLotId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboReasonLotId.LabelText = "원인lotid";
            this.cboReasonLotId.LabelWidth = "22.5%";
            this.cboReasonLotId.LanguageKey = "REASONCONSUMABLELOTID";
            this.cboReasonLotId.Location = new System.Drawing.Point(408, 0);
            this.cboReasonLotId.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.cboReasonLotId.Name = "cboReasonLotId";
            this.cboReasonLotId.Properties.AutoHeight = false;
            this.cboReasonLotId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboReasonLotId.Properties.NullText = "";
            this.cboReasonLotId.Size = new System.Drawing.Size(402, 23);
            this.cboReasonLotId.TabIndex = 1;
            // 
            // cboReasonIdVersion
            // 
            this.cboReasonIdVersion.AutoHeight = false;
            this.cboReasonIdVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboReasonIdVersion.LabelText = "원인품목|버전";
            this.cboReasonIdVersion.LabelWidth = "21.5%";
            this.cboReasonIdVersion.LanguageKey = "REASONCONSUMABLEDEFID";
            this.cboReasonIdVersion.Location = new System.Drawing.Point(0, 0);
            this.cboReasonIdVersion.Margin = new System.Windows.Forms.Padding(0);
            this.cboReasonIdVersion.Name = "cboReasonIdVersion";
            this.cboReasonIdVersion.Properties.AutoHeight = false;
            this.cboReasonIdVersion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboReasonIdVersion.Properties.NullText = "";
            this.cboReasonIdVersion.Size = new System.Drawing.Size(405, 23);
            this.cboReasonIdVersion.TabIndex = 0;
            // 
            // txtReasonArea
            // 
            this.txtReasonArea.AutoHeight = false;
            this.txtReasonArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReasonArea.LabelText = "원인작업장";
            this.txtReasonArea.LabelWidth = "22.5%";
            this.txtReasonArea.LanguageKey = "REASONAREA";
            this.txtReasonArea.Location = new System.Drawing.Point(408, 28);
            this.txtReasonArea.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.txtReasonArea.Name = "txtReasonArea";
            this.txtReasonArea.Properties.AutoHeight = false;
            this.txtReasonArea.Properties.ReadOnly = true;
            this.txtReasonArea.Size = new System.Drawing.Size(402, 23);
            this.txtReasonArea.TabIndex = 3;
            // 
            // tpgCarReceipt
            // 
            this.tpgCarReceipt.Controls.Add(this.tableLayoutPanel10);
            this.tabCarProgress.SetLanguageKey(this.tpgCarReceipt, "CARRECEIPT");
            this.tpgCarReceipt.Name = "tpgCarReceipt";
            this.tpgCarReceipt.Size = new System.Drawing.Size(815, 407);
            this.tpgCarReceipt.Text = "CAR 접수";
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 1;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel10.Controls.Add(this.fileReceiptControl, 0, 3);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 4;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(815, 407);
            this.tableLayoutPanel10.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.37357F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.28791F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.89098F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.74381F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.70374F));
            this.tableLayoutPanel2.Controls.Add(this.lblReceiptDescription, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.memoReceiptDescription, 2, 4);
            this.tableLayoutPanel2.Controls.Add(this.lblReceiptor, 4, 2);
            this.tableLayoutPanel2.Controls.Add(this.popupReceiptor, 5, 2);
            this.tableLayoutPanel2.Controls.Add(this.cboReceiptNumber, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtReceiptDate, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblReceiptNumber, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblReceiptDate, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel2, 4, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 5);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(815, 158);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // lblReceiptDescription
            // 
            this.lblReceiptDescription.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblReceiptDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReceiptDescription.LanguageKey = "REMARK";
            this.lblReceiptDescription.Location = new System.Drawing.Point(5, 56);
            this.lblReceiptDescription.Margin = new System.Windows.Forms.Padding(0);
            this.lblReceiptDescription.Name = "lblReceiptDescription";
            this.lblReceiptDescription.Size = new System.Drawing.Size(116, 102);
            this.lblReceiptDescription.TabIndex = 3;
            this.lblReceiptDescription.Text = "비고 :";
            // 
            // memoReceiptDescription
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.memoReceiptDescription, 4);
            this.memoReceiptDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoReceiptDescription.EditValue = "";
            this.memoReceiptDescription.Location = new System.Drawing.Point(121, 56);
            this.memoReceiptDescription.Margin = new System.Windows.Forms.Padding(0);
            this.memoReceiptDescription.Name = "memoReceiptDescription";
            this.memoReceiptDescription.Size = new System.Drawing.Size(694, 102);
            this.memoReceiptDescription.TabIndex = 4;
            // 
            // lblReceiptor
            // 
            this.lblReceiptor.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblReceiptor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReceiptor.LanguageKey = "RECEIPTOR";
            this.lblReceiptor.Location = new System.Drawing.Point(446, 28);
            this.lblReceiptor.Margin = new System.Windows.Forms.Padding(0);
            this.lblReceiptor.Name = "lblReceiptor";
            this.lblReceiptor.Size = new System.Drawing.Size(87, 23);
            this.lblReceiptor.TabIndex = 5;
            this.lblReceiptor.Text = "접수자";
            // 
            // popupReceiptor
            // 
            this.popupReceiptor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.popupReceiptor.EditValue = "";
            this.popupReceiptor.LabelText = "";
            this.popupReceiptor.LanguageKey = null;
            this.popupReceiptor.Location = new System.Drawing.Point(533, 28);
            this.popupReceiptor.Margin = new System.Windows.Forms.Padding(0);
            this.popupReceiptor.Name = "popupReceiptor";
            this.popupReceiptor.Properties.AutoHeight = false;
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
            this.popupReceiptor.SelectPopupCondition = conditionItemSelectPopup2;
            this.popupReceiptor.Size = new System.Drawing.Size(282, 23);
            this.popupReceiptor.TabIndex = 6;
            // 
            // cboReceiptNumber
            // 
            this.cboReceiptNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboReceiptNumber.LabelText = null;
            this.cboReceiptNumber.LanguageKey = null;
            this.cboReceiptNumber.Location = new System.Drawing.Point(121, 0);
            this.cboReceiptNumber.Margin = new System.Windows.Forms.Padding(0);
            this.cboReceiptNumber.Name = "cboReceiptNumber";
            this.cboReceiptNumber.PopupWidth = 0;
            this.cboReceiptNumber.Properties.AutoHeight = false;
            this.cboReceiptNumber.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboReceiptNumber.Properties.NullText = "";
            this.cboReceiptNumber.ShowHeader = true;
            this.cboReceiptNumber.Size = new System.Drawing.Size(237, 23);
            this.cboReceiptNumber.TabIndex = 7;
            this.cboReceiptNumber.VisibleColumns = null;
            this.cboReceiptNumber.VisibleColumnsWidth = null;
            // 
            // txtReceiptDate
            // 
            this.txtReceiptDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReceiptDate.LabelText = null;
            this.txtReceiptDate.LanguageKey = null;
            this.txtReceiptDate.Location = new System.Drawing.Point(121, 28);
            this.txtReceiptDate.Margin = new System.Windows.Forms.Padding(0);
            this.txtReceiptDate.Name = "txtReceiptDate";
            this.txtReceiptDate.Properties.AutoHeight = false;
            this.txtReceiptDate.Properties.ReadOnly = true;
            this.txtReceiptDate.Size = new System.Drawing.Size(237, 23);
            this.txtReceiptDate.TabIndex = 8;
            // 
            // lblReceiptNumber
            // 
            this.lblReceiptNumber.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblReceiptNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReceiptNumber.LanguageKey = "CARRECEIPTSEQUENCE";
            this.lblReceiptNumber.Location = new System.Drawing.Point(5, 0);
            this.lblReceiptNumber.Margin = new System.Windows.Forms.Padding(0);
            this.lblReceiptNumber.Name = "lblReceiptNumber";
            this.lblReceiptNumber.Size = new System.Drawing.Size(116, 23);
            this.lblReceiptNumber.TabIndex = 9;
            this.lblReceiptNumber.Text = "CAR 접수순번";
            // 
            // lblReceiptDate
            // 
            this.lblReceiptDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblReceiptDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReceiptDate.LanguageKey = "CARRECEIPTDATE";
            this.lblReceiptDate.Location = new System.Drawing.Point(5, 28);
            this.lblReceiptDate.Margin = new System.Windows.Forms.Padding(0);
            this.lblReceiptDate.Name = "lblReceiptDate";
            this.lblReceiptDate.Size = new System.Drawing.Size(116, 23);
            this.lblReceiptDate.TabIndex = 10;
            this.lblReceiptDate.Text = "CAR 접수날짜";
            // 
            // flowLayoutPanel2
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.flowLayoutPanel2, 2);
            this.flowLayoutPanel2.Controls.Add(this.btnReceiptReset);
            this.flowLayoutPanel2.Controls.Add(this.btnReceiptSave);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(446, 0);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(369, 23);
            this.flowLayoutPanel2.TabIndex = 12;
            // 
            // btnReceiptReset
            // 
            this.btnReceiptReset.AllowFocus = false;
            this.btnReceiptReset.IsBusy = false;
            this.btnReceiptReset.IsWrite = false;
            this.btnReceiptReset.LanguageKey = "CLEAR";
            this.btnReceiptReset.Location = new System.Drawing.Point(294, 0);
            this.btnReceiptReset.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnReceiptReset.Name = "btnReceiptReset";
            this.btnReceiptReset.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnReceiptReset.Size = new System.Drawing.Size(75, 23);
            this.btnReceiptReset.TabIndex = 1;
            this.btnReceiptReset.Text = "초기화";
            this.btnReceiptReset.TooltipLanguageKey = "";
            // 
            // btnReceiptSave
            // 
            this.btnReceiptSave.AllowFocus = false;
            this.btnReceiptSave.IsBusy = false;
            this.btnReceiptSave.IsWrite = false;
            this.btnReceiptSave.LanguageKey = "TEMPSAVE";
            this.btnReceiptSave.Location = new System.Drawing.Point(213, 0);
            this.btnReceiptSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnReceiptSave.Name = "btnReceiptSave";
            this.btnReceiptSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnReceiptSave.Size = new System.Drawing.Size(75, 23);
            this.btnReceiptSave.TabIndex = 2;
            this.btnReceiptSave.Text = "임시저장";
            this.btnReceiptSave.TooltipLanguageKey = "";
            // 
            // fileReceiptControl
            // 
            this.fileReceiptControl.ButtonVisible = false;
            this.fileReceiptControl.countRows = false;
            this.fileReceiptControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileReceiptControl.executeFileAfterDown = false;
            this.fileReceiptControl.LanguageKey = "ATTACHEDFILE";
            this.fileReceiptControl.Location = new System.Drawing.Point(0, 168);
            this.fileReceiptControl.Margin = new System.Windows.Forms.Padding(0);
            this.fileReceiptControl.Name = "fileReceiptControl";
            this.fileReceiptControl.showImage = false;
            this.fileReceiptControl.Size = new System.Drawing.Size(815, 239);
            this.fileReceiptControl.TabIndex = 7;
            this.fileReceiptControl.UploadPath = "";
            this.fileReceiptControl.UseCommentsColumn = true;
            // 
            // tpgAccept
            // 
            this.tpgAccept.Controls.Add(this.tableLayoutPanel12);
            this.tabCarProgress.SetLanguageKey(this.tpgAccept, "CARAPPVOVAL");
            this.tpgAccept.Name = "tpgAccept";
            this.tpgAccept.Size = new System.Drawing.Size(815, 407);
            this.tpgAccept.Text = "CAR 승인";
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.ColumnCount = 3;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.22288F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.77712F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.Controls.Add(this.lblAcceptNumber, 0, 1);
            this.tableLayoutPanel12.Controls.Add(this.cboAcceptNumber, 1, 1);
            this.tableLayoutPanel12.Controls.Add(this.flowLayoutPanel7, 2, 1);
            this.tableLayoutPanel12.Controls.Add(this.smartSpliterContainer1, 0, 3);
            this.tableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel12.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 4;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(815, 407);
            this.tableLayoutPanel12.TabIndex = 0;
            // 
            // lblAcceptNumber
            // 
            this.lblAcceptNumber.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblAcceptNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAcceptNumber.LanguageKey = "CARAPPVOVALSEQUENCE";
            this.lblAcceptNumber.Location = new System.Drawing.Point(5, 5);
            this.lblAcceptNumber.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblAcceptNumber.Name = "lblAcceptNumber";
            this.lblAcceptNumber.Size = new System.Drawing.Size(110, 23);
            this.lblAcceptNumber.TabIndex = 1;
            this.lblAcceptNumber.Text = "CAR 승인순번";
            // 
            // cboAcceptNumber
            // 
            this.cboAcceptNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboAcceptNumber.LabelText = null;
            this.cboAcceptNumber.LanguageKey = null;
            this.cboAcceptNumber.Location = new System.Drawing.Point(115, 5);
            this.cboAcceptNumber.Margin = new System.Windows.Forms.Padding(0);
            this.cboAcceptNumber.Name = "cboAcceptNumber";
            this.cboAcceptNumber.PopupWidth = 0;
            this.cboAcceptNumber.Properties.AutoHeight = false;
            this.cboAcceptNumber.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboAcceptNumber.Properties.NullText = "";
            this.cboAcceptNumber.ShowHeader = true;
            this.cboAcceptNumber.Size = new System.Drawing.Size(291, 23);
            this.cboAcceptNumber.TabIndex = 2;
            this.cboAcceptNumber.VisibleColumns = null;
            this.cboAcceptNumber.VisibleColumnsWidth = null;
            // 
            // flowLayoutPanel7
            // 
            this.flowLayoutPanel7.Controls.Add(this.btnAcceptReset);
            this.flowLayoutPanel7.Controls.Add(this.btnAcceptSave);
            this.flowLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel7.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel7.Location = new System.Drawing.Point(406, 5);
            this.flowLayoutPanel7.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel7.Name = "flowLayoutPanel7";
            this.flowLayoutPanel7.Size = new System.Drawing.Size(409, 23);
            this.flowLayoutPanel7.TabIndex = 3;
            // 
            // btnAcceptReset
            // 
            this.btnAcceptReset.AllowFocus = false;
            this.btnAcceptReset.IsBusy = false;
            this.btnAcceptReset.IsWrite = false;
            this.btnAcceptReset.LanguageKey = "CLEAR";
            this.btnAcceptReset.Location = new System.Drawing.Point(334, 0);
            this.btnAcceptReset.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnAcceptReset.Name = "btnAcceptReset";
            this.btnAcceptReset.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnAcceptReset.Size = new System.Drawing.Size(75, 23);
            this.btnAcceptReset.TabIndex = 0;
            this.btnAcceptReset.Text = "초기화";
            this.btnAcceptReset.TooltipLanguageKey = "";
            // 
            // btnAcceptSave
            // 
            this.btnAcceptSave.AllowFocus = false;
            this.btnAcceptSave.IsBusy = false;
            this.btnAcceptSave.IsWrite = false;
            this.btnAcceptSave.LanguageKey = "TEMPSAVE";
            this.btnAcceptSave.Location = new System.Drawing.Point(253, 0);
            this.btnAcceptSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnAcceptSave.Name = "btnAcceptSave";
            this.btnAcceptSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnAcceptSave.Size = new System.Drawing.Size(75, 23);
            this.btnAcceptSave.TabIndex = 1;
            this.btnAcceptSave.Text = "임시저장";
            this.btnAcceptSave.TooltipLanguageKey = "";
            // 
            // smartSpliterContainer1
            // 
            this.tableLayoutPanel12.SetColumnSpan(this.smartSpliterContainer1, 3);
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 33);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdCARAccept);
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdValidationHistory);
            this.smartSpliterContainer1.Size = new System.Drawing.Size(815, 374);
            this.smartSpliterContainer1.SplitterPosition = 440;
            this.smartSpliterContainer1.TabIndex = 0;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdCARAccept
            // 
            this.grdCARAccept.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdCARAccept.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCARAccept.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdCARAccept.IsUsePaging = false;
            this.grdCARAccept.LanguageKey = "CARACCEPTHISTORY";
            this.grdCARAccept.Location = new System.Drawing.Point(0, 0);
            this.grdCARAccept.Margin = new System.Windows.Forms.Padding(0);
            this.grdCARAccept.Name = "grdCARAccept";
            this.grdCARAccept.ShowBorder = true;
            this.grdCARAccept.ShowStatusBar = false;
            this.grdCARAccept.Size = new System.Drawing.Size(440, 374);
            this.grdCARAccept.TabIndex = 1;
            this.grdCARAccept.UseAutoBestFitColumns = false;
            // 
            // grdValidationHistory
            // 
            this.grdValidationHistory.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdValidationHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdValidationHistory.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdValidationHistory.IsUsePaging = false;
            this.grdValidationHistory.LanguageKey = "CARVALDATIONHISTORY";
            this.grdValidationHistory.Location = new System.Drawing.Point(0, 0);
            this.grdValidationHistory.Margin = new System.Windows.Forms.Padding(0);
            this.grdValidationHistory.Name = "grdValidationHistory";
            this.grdValidationHistory.ShowBorder = true;
            this.grdValidationHistory.ShowStatusBar = false;
            this.grdValidationHistory.Size = new System.Drawing.Size(370, 374);
            this.grdValidationHistory.TabIndex = 2;
            this.grdValidationHistory.UseAutoBestFitColumns = false;
            // 
            // tpgValidation
            // 
            this.tpgValidation.Controls.Add(this.tableLayoutPanel13);
            this.tabCarProgress.SetLanguageKey(this.tpgValidation, "CAREVALUATIONS");
            this.tpgValidation.Name = "tpgValidation";
            this.tpgValidation.Size = new System.Drawing.Size(815, 407);
            this.tpgValidation.Text = "유효성 평가";
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.ColumnCount = 4;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52F));
            this.tableLayoutPanel13.Controls.Add(this.smartGroupBox4, 0, 5);
            this.tableLayoutPanel13.Controls.Add(this.smartGroupBox1, 0, 3);
            this.tableLayoutPanel13.Controls.Add(this.lblValidationNumber, 0, 1);
            this.tableLayoutPanel13.Controls.Add(this.cboValidationNumber, 1, 1);
            this.tableLayoutPanel13.Controls.Add(this.flowLayoutPanel8, 3, 1);
            this.tableLayoutPanel13.Controls.Add(this.fileValidationControl, 3, 3);
            this.tableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel13.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel13.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 6;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(815, 407);
            this.tableLayoutPanel13.TabIndex = 0;
            // 
            // smartGroupBox4
            // 
            this.tableLayoutPanel13.SetColumnSpan(this.smartGroupBox4, 2);
            this.smartGroupBox4.Controls.Add(this.tableLayoutPanel15);
            this.smartGroupBox4.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.smartGroupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartGroupBox4.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.smartGroupBox4.LanguageKey = "DATAEVALUATION";
            this.smartGroupBox4.Location = new System.Drawing.Point(0, 259);
            this.smartGroupBox4.Margin = new System.Windows.Forms.Padding(0);
            this.smartGroupBox4.Name = "smartGroupBox4";
            this.smartGroupBox4.ShowBorder = true;
            this.smartGroupBox4.Size = new System.Drawing.Size(388, 148);
            this.smartGroupBox4.TabIndex = 7;
            this.smartGroupBox4.Text = "Data평가";
            // 
            // tableLayoutPanel15
            // 
            this.tableLayoutPanel15.ColumnCount = 5;
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.26347F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.13772F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.86827F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.73054F));
            this.tableLayoutPanel15.Controls.Add(this.txtConcurrence, 2, 3);
            this.tableLayoutPanel15.Controls.Add(this.lblIsclose, 3, 3);
            this.tableLayoutPanel15.Controls.Add(this.cboIsclose, 4, 3);
            this.tableLayoutPanel15.Controls.Add(this.lblConcurrence, 1, 3);
            this.tableLayoutPanel15.Controls.Add(this.dtrSearchDate, 2, 1);
            this.tableLayoutPanel15.Controls.Add(this.lblSearchDate, 1, 1);
            this.tableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel15.Location = new System.Drawing.Point(2, 31);
            this.tableLayoutPanel15.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.tableLayoutPanel15.Name = "tableLayoutPanel15";
            this.tableLayoutPanel15.RowCount = 5;
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel15.Size = new System.Drawing.Size(384, 115);
            this.tableLayoutPanel15.TabIndex = 0;
            // 
            // txtConcurrence
            // 
            this.txtConcurrence.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConcurrence.EditValue = "";
            this.txtConcurrence.LabelText = null;
            this.txtConcurrence.LanguageKey = null;
            this.txtConcurrence.Location = new System.Drawing.Point(74, 60);
            this.txtConcurrence.Margin = new System.Windows.Forms.Padding(0);
            this.txtConcurrence.Name = "txtConcurrence";
            this.txtConcurrence.Properties.AutoHeight = false;
            this.txtConcurrence.Properties.ReadOnly = true;
            this.txtConcurrence.Size = new System.Drawing.Size(118, 50);
            this.txtConcurrence.TabIndex = 12;
            // 
            // lblIsclose
            // 
            this.lblIsclose.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblIsclose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIsclose.LanguageKey = "ISCLOSE";
            this.lblIsclose.Location = new System.Drawing.Point(197, 60);
            this.lblIsclose.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblIsclose.Name = "lblIsclose";
            this.lblIsclose.Size = new System.Drawing.Size(55, 50);
            this.lblIsclose.TabIndex = 2;
            this.lblIsclose.Text = "마감여부";
            // 
            // cboIsclose
            // 
            this.cboIsclose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboIsclose.LabelText = null;
            this.cboIsclose.LanguageKey = null;
            this.cboIsclose.Location = new System.Drawing.Point(252, 60);
            this.cboIsclose.Margin = new System.Windows.Forms.Padding(0);
            this.cboIsclose.Name = "cboIsclose";
            this.cboIsclose.PopupWidth = 0;
            this.cboIsclose.Properties.AutoHeight = false;
            this.cboIsclose.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboIsclose.Properties.NullText = "";
            this.cboIsclose.Properties.ShowHeader = false;
            this.cboIsclose.ShowHeader = false;
            this.cboIsclose.Size = new System.Drawing.Size(132, 50);
            this.cboIsclose.TabIndex = 3;
            this.cboIsclose.UseEmptyItem = true;
            this.cboIsclose.VisibleColumns = null;
            this.cboIsclose.VisibleColumnsWidth = null;
            // 
            // lblConcurrence
            // 
            this.lblConcurrence.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblConcurrence.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblConcurrence.LanguageKey = "SAMEOCCURRENCE";
            this.lblConcurrence.Location = new System.Drawing.Point(5, 60);
            this.lblConcurrence.Margin = new System.Windows.Forms.Padding(0);
            this.lblConcurrence.Name = "lblConcurrence";
            this.lblConcurrence.Size = new System.Drawing.Size(69, 50);
            this.lblConcurrence.TabIndex = 4;
            this.lblConcurrence.Text = "동시발생건";
            // 
            // dtrSearchDate
            // 
            this.tableLayoutPanel15.SetColumnSpan(this.dtrSearchDate, 3);
            this.dtrSearchDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtrSearchDate.EditValue = "2021-01-25,2021-01-25";
            this.dtrSearchDate.LabelText = null;
            this.dtrSearchDate.LanguageKey = null;
            this.dtrSearchDate.Location = new System.Drawing.Point(74, 5);
            this.dtrSearchDate.Margin = new System.Windows.Forms.Padding(0);
            this.dtrSearchDate.Name = "dtrSearchDate";
            this.dtrSearchDate.Properties.AutoHeight = false;
            this.dtrSearchDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtrSearchDate.Size = new System.Drawing.Size(310, 50);
            this.dtrSearchDate.TabIndex = 13;
            // 
            // lblSearchDate
            // 
            this.lblSearchDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblSearchDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSearchDate.Location = new System.Drawing.Point(5, 5);
            this.lblSearchDate.Margin = new System.Windows.Forms.Padding(0);
            this.lblSearchDate.Name = "lblSearchDate";
            this.lblSearchDate.Size = new System.Drawing.Size(69, 50);
            this.lblSearchDate.TabIndex = 14;
            this.lblSearchDate.Text = "조회기간";
            // 
            // smartGroupBox1
            // 
            this.tableLayoutPanel13.SetColumnSpan(this.smartGroupBox1, 2);
            this.smartGroupBox1.Controls.Add(this.tableLayoutPanel14);
            this.smartGroupBox1.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.smartGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartGroupBox1.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.smartGroupBox1.LanguageKey = "FIELDEVALUATION";
            this.smartGroupBox1.Location = new System.Drawing.Point(0, 33);
            this.smartGroupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.smartGroupBox1.Name = "smartGroupBox1";
            this.smartGroupBox1.ShowBorder = true;
            this.smartGroupBox1.Size = new System.Drawing.Size(388, 221);
            this.smartGroupBox1.TabIndex = 0;
            this.smartGroupBox1.Text = "현장평가";
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.ColumnCount = 9;
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33333F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33333F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33333F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel14.Controls.Add(this.lblCheckDescription, 1, 3);
            this.tableLayoutPanel14.Controls.Add(this.memoCheckDescription, 2, 3);
            this.tableLayoutPanel14.Controls.Add(this.smartLabel1, 4, 1);
            this.tableLayoutPanel14.Controls.Add(this.popupInspector, 5, 1);
            this.tableLayoutPanel14.Controls.Add(this.lblCheckResult, 7, 1);
            this.tableLayoutPanel14.Controls.Add(this.cboCheckResult, 8, 1);
            this.tableLayoutPanel14.Controls.Add(this.lblCheckDate, 1, 1);
            this.tableLayoutPanel14.Controls.Add(this.txtCheckDate, 2, 1);
            this.tableLayoutPanel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel14.Location = new System.Drawing.Point(2, 31);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.RowCount = 5;
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.Size = new System.Drawing.Size(384, 188);
            this.tableLayoutPanel14.TabIndex = 0;
            // 
            // lblCheckDescription
            // 
            this.lblCheckDescription.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCheckDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCheckDescription.Location = new System.Drawing.Point(5, 33);
            this.lblCheckDescription.Margin = new System.Windows.Forms.Padding(0);
            this.lblCheckDescription.Name = "lblCheckDescription";
            this.lblCheckDescription.Size = new System.Drawing.Size(49, 23);
            this.lblCheckDescription.TabIndex = 4;
            this.lblCheckDescription.Text = "점검내용";
            // 
            // memoCheckDescription
            // 
            this.tableLayoutPanel14.SetColumnSpan(this.memoCheckDescription, 7);
            this.memoCheckDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoCheckDescription.Location = new System.Drawing.Point(54, 33);
            this.memoCheckDescription.Margin = new System.Windows.Forms.Padding(0);
            this.memoCheckDescription.Name = "memoCheckDescription";
            this.tableLayoutPanel14.SetRowSpan(this.memoCheckDescription, 3);
            this.memoCheckDescription.Size = new System.Drawing.Size(330, 155);
            this.memoCheckDescription.TabIndex = 5;
            // 
            // smartLabel1
            // 
            this.smartLabel1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.smartLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLabel1.LanguageKey = "CHECKER";
            this.smartLabel1.Location = new System.Drawing.Point(137, 5);
            this.smartLabel1.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.smartLabel1.Name = "smartLabel1";
            this.smartLabel1.Size = new System.Drawing.Size(44, 23);
            this.smartLabel1.TabIndex = 6;
            this.smartLabel1.Text = "점검자";
            // 
            // popupInspector
            // 
            this.popupInspector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.popupInspector.LabelText = null;
            this.popupInspector.LanguageKey = null;
            this.popupInspector.Location = new System.Drawing.Point(181, 5);
            this.popupInspector.Margin = new System.Windows.Forms.Padding(0);
            this.popupInspector.Name = "popupInspector";
            this.popupInspector.Properties.AutoHeight = false;
            conditionItemSelectPopup3.ApplySelection = null;
            conditionItemSelectPopup3.AutoFillColumnNames = null;
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
            conditionItemSelectPopup3.RelationIds = null;
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
            this.popupInspector.SelectPopupCondition = conditionItemSelectPopup3;
            this.popupInspector.Size = new System.Drawing.Size(73, 23);
            this.popupInspector.TabIndex = 7;
            // 
            // lblCheckResult
            // 
            this.lblCheckResult.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCheckResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCheckResult.LanguageKey = "CHECKRESULT";
            this.lblCheckResult.Location = new System.Drawing.Point(259, 5);
            this.lblCheckResult.Margin = new System.Windows.Forms.Padding(0);
            this.lblCheckResult.Name = "lblCheckResult";
            this.lblCheckResult.Size = new System.Drawing.Size(49, 23);
            this.lblCheckResult.TabIndex = 8;
            this.lblCheckResult.Text = "점검결과";
            // 
            // cboCheckResult
            // 
            this.cboCheckResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboCheckResult.LabelText = null;
            this.cboCheckResult.LanguageKey = null;
            this.cboCheckResult.Location = new System.Drawing.Point(308, 5);
            this.cboCheckResult.Margin = new System.Windows.Forms.Padding(0);
            this.cboCheckResult.Name = "cboCheckResult";
            this.cboCheckResult.PopupWidth = 0;
            this.cboCheckResult.Properties.AutoHeight = false;
            this.cboCheckResult.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCheckResult.Properties.NullText = "";
            this.cboCheckResult.ShowHeader = true;
            this.cboCheckResult.Size = new System.Drawing.Size(76, 23);
            this.cboCheckResult.TabIndex = 9;
            this.cboCheckResult.UseEmptyItem = true;
            this.cboCheckResult.VisibleColumns = null;
            this.cboCheckResult.VisibleColumnsWidth = null;
            // 
            // lblCheckDate
            // 
            this.lblCheckDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCheckDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCheckDate.LanguageKey = "CHECKDATE";
            this.lblCheckDate.Location = new System.Drawing.Point(5, 5);
            this.lblCheckDate.Margin = new System.Windows.Forms.Padding(0);
            this.lblCheckDate.Name = "lblCheckDate";
            this.lblCheckDate.Size = new System.Drawing.Size(49, 23);
            this.lblCheckDate.TabIndex = 10;
            this.lblCheckDate.Text = "점검일시";
            // 
            // txtCheckDate
            // 
            this.txtCheckDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCheckDate.LabelText = null;
            this.txtCheckDate.LanguageKey = null;
            this.txtCheckDate.Location = new System.Drawing.Point(54, 5);
            this.txtCheckDate.Margin = new System.Windows.Forms.Padding(0);
            this.txtCheckDate.Name = "txtCheckDate";
            this.txtCheckDate.Properties.AutoHeight = false;
            this.txtCheckDate.Properties.ReadOnly = true;
            this.txtCheckDate.Size = new System.Drawing.Size(73, 23);
            this.txtCheckDate.TabIndex = 11;
            // 
            // lblValidationNumber
            // 
            this.lblValidationNumber.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblValidationNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValidationNumber.LanguageKey = "CAREVALUATIONSSEQUENCE";
            this.lblValidationNumber.Location = new System.Drawing.Point(5, 5);
            this.lblValidationNumber.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblValidationNumber.Name = "lblValidationNumber";
            this.lblValidationNumber.Size = new System.Drawing.Size(92, 23);
            this.lblValidationNumber.TabIndex = 3;
            this.lblValidationNumber.Text = "유효성순번";
            // 
            // cboValidationNumber
            // 
            this.cboValidationNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboValidationNumber.LabelText = null;
            this.cboValidationNumber.LanguageKey = null;
            this.cboValidationNumber.Location = new System.Drawing.Point(97, 5);
            this.cboValidationNumber.Margin = new System.Windows.Forms.Padding(0);
            this.cboValidationNumber.Name = "cboValidationNumber";
            this.cboValidationNumber.PopupWidth = 0;
            this.cboValidationNumber.Properties.AutoHeight = false;
            this.cboValidationNumber.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboValidationNumber.Properties.NullText = "";
            this.cboValidationNumber.ShowHeader = true;
            this.cboValidationNumber.Size = new System.Drawing.Size(291, 23);
            this.cboValidationNumber.TabIndex = 4;
            this.cboValidationNumber.VisibleColumns = null;
            this.cboValidationNumber.VisibleColumnsWidth = null;
            // 
            // flowLayoutPanel8
            // 
            this.flowLayoutPanel8.Controls.Add(this.btnValidationReset);
            this.flowLayoutPanel8.Controls.Add(this.btnValidationSave);
            this.flowLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel8.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel8.Location = new System.Drawing.Point(393, 5);
            this.flowLayoutPanel8.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel8.Name = "flowLayoutPanel8";
            this.flowLayoutPanel8.Size = new System.Drawing.Size(422, 23);
            this.flowLayoutPanel8.TabIndex = 5;
            // 
            // btnValidationReset
            // 
            this.btnValidationReset.AllowFocus = false;
            this.btnValidationReset.IsBusy = false;
            this.btnValidationReset.IsWrite = false;
            this.btnValidationReset.LanguageKey = "CLEAR";
            this.btnValidationReset.Location = new System.Drawing.Point(347, 0);
            this.btnValidationReset.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnValidationReset.Name = "btnValidationReset";
            this.btnValidationReset.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnValidationReset.Size = new System.Drawing.Size(75, 23);
            this.btnValidationReset.TabIndex = 0;
            this.btnValidationReset.Text = "초기화";
            this.btnValidationReset.TooltipLanguageKey = "";
            // 
            // btnValidationSave
            // 
            this.btnValidationSave.AllowFocus = false;
            this.btnValidationSave.IsBusy = false;
            this.btnValidationSave.IsWrite = false;
            this.btnValidationSave.LanguageKey = "TEMPSAVE";
            this.btnValidationSave.Location = new System.Drawing.Point(266, 0);
            this.btnValidationSave.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnValidationSave.Name = "btnValidationSave";
            this.btnValidationSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnValidationSave.Size = new System.Drawing.Size(75, 23);
            this.btnValidationSave.TabIndex = 1;
            this.btnValidationSave.Text = "임시저장";
            this.btnValidationSave.TooltipLanguageKey = "";
            // 
            // fileValidationControl
            // 
            this.fileValidationControl.ButtonVisible = false;
            this.fileValidationControl.countRows = false;
            this.fileValidationControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileValidationControl.executeFileAfterDown = false;
            this.fileValidationControl.LanguageKey = "ATTACHEDFILE";
            this.fileValidationControl.Location = new System.Drawing.Point(393, 33);
            this.fileValidationControl.Margin = new System.Windows.Forms.Padding(0);
            this.fileValidationControl.Name = "fileValidationControl";
            this.tableLayoutPanel13.SetRowSpan(this.fileValidationControl, 3);
            this.fileValidationControl.showImage = false;
            this.fileValidationControl.Size = new System.Drawing.Size(422, 374);
            this.fileValidationControl.TabIndex = 6;
            this.fileValidationControl.UploadPath = "";
            this.fileValidationControl.UseCommentsColumn = true;
            // 
            // grdFileList2
            // 
            this.grdFileList2.Caption = "대책서 양식 리스트";
            this.grdFileList2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFileList2.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdFileList2.IsUsePaging = false;
            this.grdFileList2.LanguageKey = "MEASUREFORMLIST";
            this.grdFileList2.Location = new System.Drawing.Point(72, 0);
            this.grdFileList2.Margin = new System.Windows.Forms.Padding(0);
            this.grdFileList2.Name = "grdFileList2";
            this.grdFileList2.ShowBorder = true;
            this.grdFileList2.Size = new System.Drawing.Size(0, 23);
            this.grdFileList2.TabIndex = 4;
            this.grdFileList2.UseAutoBestFitColumns = false;
            this.grdFileList2.Visible = false;
            // 
            // IssueRegistrationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabCarProgress);
            this.Name = "IssueRegistrationControl";
            this.Size = new System.Drawing.Size(821, 436);
            ((System.ComponentModel.ISupportInitialize)(this.tabCarProgress)).EndInit();
            this.tabCarProgress.ResumeLayout(false);
            this.tpgCarReqeust.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoRequestDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupManager.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboRequestNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRequestDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRequestMeasure.Properties)).EndInit();
            this.flowLayoutPanel5.ResumeLayout(false);
            this.tblReason.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboReasonSegmentId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboReasonLotId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboReasonIdVersion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReasonArea.Properties)).EndInit();
            this.tpgCarReceipt.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoReceiptDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupReceiptor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboReceiptNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiptDate.Properties)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.tpgAccept.ResumeLayout(false);
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboAcceptNumber.Properties)).EndInit();
            this.flowLayoutPanel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            this.tpgValidation.ResumeLayout(false);
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel13.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox4)).EndInit();
            this.smartGroupBox4.ResumeLayout(false);
            this.tableLayoutPanel15.ResumeLayout(false);
            this.tableLayoutPanel15.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtConcurrence.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboIsclose.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtrSearchDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).EndInit();
            this.smartGroupBox1.ResumeLayout(false);
            this.tableLayoutPanel14.ResumeLayout(false);
            this.tableLayoutPanel14.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoCheckDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupInspector.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCheckResult.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCheckDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboValidationNumber.Properties)).EndInit();
            this.flowLayoutPanel8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraTab.XtraTabPage tpgCarReqeust;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private Framework.SmartControls.SmartLabel lblReqestDescription;
        private Framework.SmartControls.SmartMemoEdit memoRequestDescription;
        private Framework.SmartControls.SmartLabel lblManager;
        private Framework.SmartControls.SmartTextBox txtRequestDate;
        private Framework.SmartControls.SmartLabel lblRequestNumber;
        private Framework.SmartControls.SmartLabel lblRequestDate;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private Framework.SmartControls.SmartButton btnDownload;
        private Framework.SmartControls.SmartButton btnRequestReset;
        private Framework.SmartControls.SmartButton btnReqestSave;
        private DevExpress.XtraTab.XtraTabPage tpgCarReceipt;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Framework.SmartControls.SmartLabel lblReceiptDescription;
        private Framework.SmartControls.SmartMemoEdit memoReceiptDescription;
        private Framework.SmartControls.SmartLabel lblReceiptor;
        private Framework.SmartControls.SmartTextBox txtReceiptDate;
        private Framework.SmartControls.SmartLabel lblReceiptNumber;
        private Framework.SmartControls.SmartLabel lblReceiptDate;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Framework.SmartControls.SmartButton btnReceiptReset;
        private Framework.SmartControls.SmartButton btnReceiptSave;
        private DevExpress.XtraTab.XtraTabPage tpgAccept;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
        private Framework.SmartControls.SmartLabel lblAcceptNumber;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7;
        private Framework.SmartControls.SmartButton btnAcceptReset;
        private Framework.SmartControls.SmartButton btnAcceptSave;
        private DevExpress.XtraTab.XtraTabPage tpgValidation;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel13;
        private Framework.SmartControls.SmartGroupBox smartGroupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel14;
        private Framework.SmartControls.SmartLabel lblCheckDescription;
        private Framework.SmartControls.SmartMemoEdit memoCheckDescription;
        private Framework.SmartControls.SmartLabel lblValidationNumber;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel8;
        private Framework.SmartControls.SmartButton btnValidationReset;
        private Framework.SmartControls.SmartButton btnValidationSave;
        private Framework.SmartControls.SmartLabel smartLabel1;
        private Framework.SmartControls.SmartSelectPopupEdit popupInspector;
        private Framework.SmartControls.SmartLabel lblCheckResult;
        private Framework.SmartControls.SmartComboBox cboCheckResult;
        private Framework.SmartControls.SmartLabel lblCheckDate;
        private Framework.SmartControls.SmartTextBox txtCheckDate;
        private Commons.Controls.SmartFileProcessingControl fileReceiptControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private Commons.Controls.SmartFileProcessingControl fileValidationControl;
        private Framework.SmartControls.SmartCheckBox chkRequestMeasure;
        private Framework.SmartControls.SmartGroupBox smartGroupBox4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel15;
        private Framework.SmartControls.SmartTextBox txtConcurrence;
        private Framework.SmartControls.SmartLabel lblIsclose;
        private Framework.SmartControls.SmartComboBox cboIsclose;
        private Framework.SmartControls.SmartLabel lblConcurrence;
        private Framework.SmartControls.SmartDateRangeEdit dtrSearchDate;
        private Framework.SmartControls.SmartLabel lblSearchDate;
        public Framework.SmartControls.SmartSelectPopupEdit popupManager;
        public Framework.SmartControls.SmartSelectPopupEdit popupReceiptor;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdCARAccept;
        private Framework.SmartControls.SmartBandedGrid grdValidationHistory;
        private System.Windows.Forms.TableLayoutPanel tblReason;
        private Framework.SmartControls.SmartLabelComboBox cboReasonIdVersion;
        private Framework.SmartControls.SmartLabelComboBox cboReasonSegmentId;
        private Framework.SmartControls.SmartLabelComboBox cboReasonLotId;
        public Framework.SmartControls.SmartComboBox cboRequestNumber;
        public Framework.SmartControls.SmartComboBox cboReceiptNumber;
        public Framework.SmartControls.SmartComboBox cboAcceptNumber;
        public Framework.SmartControls.SmartTabControl tabCarProgress;
        public Framework.SmartControls.SmartComboBox cboValidationNumber;
        private Framework.SmartControls.SmartLabelTextBox txtReasonArea;
        public Framework.SmartControls.SmartBandedGrid grdFileList2;
    }
}
