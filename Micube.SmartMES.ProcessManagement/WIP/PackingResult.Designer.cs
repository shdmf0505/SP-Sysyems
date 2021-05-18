namespace Micube.SmartMES.ProcessManagement
{
	partial class PackingResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PackingResult));
            Micube.Framework.SmartControls.ConditionCollection conditionCollection1 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection2 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdLotInfo = new Micube.SmartMES.Commons.Controls.SmartLotInfoGrid();
            this.smartPanel2 = new Micube.Framework.SmartControls.SmartPanel();
            this.btnSaveLot = new Micube.Framework.SmartControls.SmartButton();
            this.numDefectQty = new Micube.Framework.SmartControls.SmartSpinEdit();
            this.numGoodQty = new Micube.Framework.SmartControls.SmartSpinEdit();
            this.txtUnit = new Micube.Framework.SmartControls.SmartTextBox();
            this.txtComment = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblDefectQty = new Micube.Framework.SmartControls.SmartLabel();
            this.lblGoodQty = new Micube.Framework.SmartControls.SmartLabel();
            this.lblComment = new Micube.Framework.SmartControls.SmartLabel();
            this.lblUom = new Micube.Framework.SmartControls.SmartLabel();
            this.tabPartition = new Micube.Framework.SmartControls.SmartTabControl();
            this.PackingProcess = new DevExpress.XtraTab.XtraTabPage();
            this.grdWaitPackingLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.btnCreateBoxNo = new Micube.Framework.SmartControls.SmartButton();
            this.txtBoxNo = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblBoxNo = new Micube.Framework.SmartControls.SmartLabel();
            this.DefectList = new DevExpress.XtraTab.XtraTabPage();
            this.defectListForPacking = new Micube.SmartMES.ProcessManagement.DefectListForPacking();
            this.PackingSpec = new DevExpress.XtraTab.XtraTabPage();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblArea = new Micube.Framework.SmartControls.SmartLabel();
            this.popArea = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            this.lblWorker = new Micube.Framework.SmartControls.SmartLabel();
            this.cboUser = new Micube.Framework.SmartControls.SmartComboBox();
            this.lblLotId = new Micube.Framework.SmartControls.SmartLabel();
            this.txtLotId = new Micube.Framework.SmartControls.SmartTextBox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnStartPacking = new Micube.Framework.SmartControls.SmartButton();
            this.btnPrintLabel = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).BeginInit();
            this.smartPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDefectQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGoodQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPartition)).BeginInit();
            this.tabPartition.SuspendLayout();
            this.PackingProcess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBoxNo.Properties)).BeginInit();
            this.DefectList.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).BeginInit();
            this.flowLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(0, 30);
            this.pnlCondition.Size = new System.Drawing.Size(0, 0);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.flowLayoutPanel3);
            this.pnlToolbar.Size = new System.Drawing.Size(905, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.flowLayoutPanel3, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlContent.Size = new System.Drawing.Size(905, 600);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(905, 629);
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdLotInfo, 0, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartPanel2, 0, 4);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.tabPartition, 0, 6);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 7;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(905, 600);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // grdLotInfo
            // 
            this.grdLotInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLotInfo.Location = new System.Drawing.Point(0, 37);
            this.grdLotInfo.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotInfo.Name = "grdLotInfo";
            this.grdLotInfo.Size = new System.Drawing.Size(905, 101);
            this.grdLotInfo.TabIndex = 2;
            // 
            // smartPanel2
            // 
            this.smartPanel2.Controls.Add(this.btnSaveLot);
            this.smartPanel2.Controls.Add(this.numDefectQty);
            this.smartPanel2.Controls.Add(this.numGoodQty);
            this.smartPanel2.Controls.Add(this.txtUnit);
            this.smartPanel2.Controls.Add(this.txtComment);
            this.smartPanel2.Controls.Add(this.lblDefectQty);
            this.smartPanel2.Controls.Add(this.lblGoodQty);
            this.smartPanel2.Controls.Add(this.lblComment);
            this.smartPanel2.Controls.Add(this.lblUom);
            this.smartPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel2.Location = new System.Drawing.Point(0, 148);
            this.smartPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel2.Name = "smartPanel2";
            this.smartPanel2.Size = new System.Drawing.Size(905, 75);
            this.smartPanel2.TabIndex = 3;
            // 
            // btnSaveLot
            // 
            this.btnSaveLot.AllowFocus = false;
            this.btnSaveLot.Enabled = false;
            this.btnSaveLot.IsBusy = false;
            this.btnSaveLot.LanguageKey = "SAVELOT";
            this.btnSaveLot.Location = new System.Drawing.Point(563, 44);
            this.btnSaveLot.Margin = new System.Windows.Forms.Padding(0);
            this.btnSaveLot.Name = "btnSaveLot";
            this.btnSaveLot.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSaveLot.Size = new System.Drawing.Size(75, 20);
            this.btnSaveLot.TabIndex = 14;
            this.btnSaveLot.Text = "LOT 저장";
            this.btnSaveLot.TooltipLanguageKey = "";
            // 
            // numDefectQty
            // 
            this.numDefectQty.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numDefectQty.LabelText = null;
            this.numDefectQty.LanguageKey = null;
            this.numDefectQty.Location = new System.Drawing.Point(448, 10);
            this.numDefectQty.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.numDefectQty.Name = "numDefectQty";
            this.numDefectQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numDefectQty.Properties.ReadOnly = true;
            this.numDefectQty.Size = new System.Drawing.Size(100, 20);
            this.numDefectQty.TabIndex = 12;
            // 
            // numGoodQty
            // 
            this.numGoodQty.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numGoodQty.LabelText = null;
            this.numGoodQty.LanguageKey = null;
            this.numGoodQty.Location = new System.Drawing.Point(263, 10);
            this.numGoodQty.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.numGoodQty.Name = "numGoodQty";
            this.numGoodQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numGoodQty.Properties.Mask.EditMask = "n";
            this.numGoodQty.Properties.ReadOnly = true;
            this.numGoodQty.Size = new System.Drawing.Size(100, 20);
            this.numGoodQty.TabIndex = 11;
            // 
            // txtUnit
            // 
            this.txtUnit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtUnit.LabelText = null;
            this.txtUnit.LanguageKey = null;
            this.txtUnit.Location = new System.Drawing.Point(73, 10);
            this.txtUnit.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Properties.ReadOnly = true;
            this.txtUnit.Size = new System.Drawing.Size(100, 20);
            this.txtUnit.TabIndex = 10;
            // 
            // txtComment
            // 
            this.txtComment.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtComment.LabelText = null;
            this.txtComment.LanguageKey = null;
            this.txtComment.Location = new System.Drawing.Point(73, 44);
            this.txtComment.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(475, 20);
            this.txtComment.TabIndex = 8;
            // 
            // lblDefectQty
            // 
            this.lblDefectQty.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDefectQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDefectQty.LanguageKey = "DEFECTQTY";
            this.lblDefectQty.Location = new System.Drawing.Point(378, 10);
            this.lblDefectQty.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.lblDefectQty.Name = "lblDefectQty";
            this.lblDefectQty.Size = new System.Drawing.Size(65, 20);
            this.lblDefectQty.TabIndex = 6;
            this.lblDefectQty.Text = "불량수량";
            // 
            // lblGoodQty
            // 
            this.lblGoodQty.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblGoodQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblGoodQty.LanguageKey = "GOODQTY";
            this.lblGoodQty.Location = new System.Drawing.Point(193, 10);
            this.lblGoodQty.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.lblGoodQty.Name = "lblGoodQty";
            this.lblGoodQty.Size = new System.Drawing.Size(65, 20);
            this.lblGoodQty.TabIndex = 4;
            this.lblGoodQty.Text = "양품수량";
            // 
            // lblComment
            // 
            this.lblComment.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblComment.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblComment.LanguageKey = "COMMENT";
            this.lblComment.Location = new System.Drawing.Point(5, 44);
            this.lblComment.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(65, 20);
            this.lblComment.TabIndex = 2;
            this.lblComment.Text = "특이사항";
            // 
            // lblUom
            // 
            this.lblUom.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUom.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblUom.LanguageKey = "UOM";
            this.lblUom.Location = new System.Drawing.Point(5, 10);
            this.lblUom.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblUom.Name = "lblUom";
            this.lblUom.Size = new System.Drawing.Size(65, 20);
            this.lblUom.TabIndex = 1;
            this.lblUom.Text = "UOM";
            // 
            // tabPartition
            // 
            this.tabPartition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPartition.Enabled = false;
            this.tabPartition.Location = new System.Drawing.Point(3, 236);
            this.tabPartition.Name = "tabPartition";
            this.tabPartition.SelectedTabPage = this.PackingProcess;
            this.tabPartition.Size = new System.Drawing.Size(899, 361);
            this.tabPartition.TabIndex = 4;
            this.tabPartition.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.PackingProcess,
            this.DefectList,
            this.PackingSpec});
            // 
            // PackingProcess
            // 
            this.PackingProcess.Controls.Add(this.grdWaitPackingLotList);
            this.PackingProcess.Controls.Add(this.smartPanel1);
            this.tabPartition.SetLanguageKey(this.PackingProcess, "PACKINGPROCESS");
            this.PackingProcess.Name = "PackingProcess";
            this.PackingProcess.Size = new System.Drawing.Size(893, 332);
            this.PackingProcess.Text = "포장작업";
            // 
            // grdWaitPackingLotList
            // 
            this.grdWaitPackingLotList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdWaitPackingLotList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdWaitPackingLotList.IsUsePaging = false;
            this.grdWaitPackingLotList.LanguageKey = "PACKINGLOTLIST";
            this.grdWaitPackingLotList.Location = new System.Drawing.Point(0, 33);
            this.grdWaitPackingLotList.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.grdWaitPackingLotList.Name = "grdWaitPackingLotList";
            this.grdWaitPackingLotList.ShowBorder = true;
            this.grdWaitPackingLotList.ShowStatusBar = false;
            this.grdWaitPackingLotList.Size = new System.Drawing.Size(893, 299);
            this.grdWaitPackingLotList.TabIndex = 1;
            // 
            // smartPanel1
            // 
            this.smartPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel1.Controls.Add(this.btnCreateBoxNo);
            this.smartPanel1.Controls.Add(this.txtBoxNo);
            this.smartPanel1.Controls.Add(this.lblBoxNo);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(893, 33);
            this.smartPanel1.TabIndex = 0;
            // 
            // btnCreateBoxNo
            // 
            this.btnCreateBoxNo.AllowFocus = false;
            this.btnCreateBoxNo.Enabled = false;
            this.btnCreateBoxNo.IsBusy = false;
            this.btnCreateBoxNo.LanguageKey = "CREATEBOXNO";
            this.btnCreateBoxNo.Location = new System.Drawing.Point(374, 5);
            this.btnCreateBoxNo.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnCreateBoxNo.Name = "btnCreateBoxNo";
            this.btnCreateBoxNo.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCreateBoxNo.Size = new System.Drawing.Size(90, 23);
            this.btnCreateBoxNo.TabIndex = 2;
            this.btnCreateBoxNo.Text = "Box 번호생성";
            this.btnCreateBoxNo.TooltipLanguageKey = "";
            // 
            // txtBoxNo
            // 
            this.txtBoxNo.LabelText = null;
            this.txtBoxNo.LanguageKey = null;
            this.txtBoxNo.Location = new System.Drawing.Point(73, 6);
            this.txtBoxNo.Margin = new System.Windows.Forms.Padding(0, 2, 15, 0);
            this.txtBoxNo.Name = "txtBoxNo";
            this.txtBoxNo.Size = new System.Drawing.Size(285, 20);
            this.txtBoxNo.TabIndex = 1;
            // 
            // lblBoxNo
            // 
            this.lblBoxNo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblBoxNo.LanguageKey = "BOXNO";
            this.lblBoxNo.Location = new System.Drawing.Point(5, 6);
            this.lblBoxNo.Margin = new System.Windows.Forms.Padding(5, 2, 2, 0);
            this.lblBoxNo.Name = "lblBoxNo";
            this.lblBoxNo.Size = new System.Drawing.Size(45, 20);
            this.lblBoxNo.TabIndex = 0;
            this.lblBoxNo.Text = "Box No";
            // 
            // DefectList
            // 
            this.DefectList.Controls.Add(this.defectListForPacking);
            this.tabPartition.SetLanguageKey(this.DefectList, "DEFECT");
            this.DefectList.Name = "DefectList";
            this.DefectList.Size = new System.Drawing.Size(893, 332);
            this.DefectList.Text = "불량";
            // 
            // defectListForPacking
            // 
            this.defectListForPacking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.defectListForPacking.Location = new System.Drawing.Point(0, 0);
            this.defectListForPacking.Name = "defectListForPacking";
            this.defectListForPacking.Size = new System.Drawing.Size(893, 332);
            this.defectListForPacking.TabIndex = 0;
            // 
            // PackingSpec
            // 
            this.tabPartition.SetLanguageKey(this.PackingSpec, "PACKINGSPEC");
            this.PackingSpec.Name = "PackingSpec";
            this.PackingSpec.Size = new System.Drawing.Size(893, 332);
            this.PackingSpec.Text = "포장사양";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.lblArea);
            this.flowLayoutPanel2.Controls.Add(this.popArea);
            this.flowLayoutPanel2.Controls.Add(this.lblWorker);
            this.flowLayoutPanel2.Controls.Add(this.cboUser);
            this.flowLayoutPanel2.Controls.Add(this.lblLotId);
            this.flowLayoutPanel2.Controls.Add(this.txtLotId);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(905, 27);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // lblArea
            // 
            this.lblArea.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblArea.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblArea.LanguageKey = "AREA";
            this.lblArea.Location = new System.Drawing.Point(5, 4);
            this.lblArea.Margin = new System.Windows.Forms.Padding(5, 2, 5, 0);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(55, 20);
            this.lblArea.TabIndex = 4;
            this.lblArea.Text = "작업장";
            // 
            // popArea
            // 
            this.popArea.LabelText = null;
            this.popArea.LanguageKey = null;
            this.popArea.Location = new System.Drawing.Point(68, 3);
            this.popArea.Name = "popArea";
            conditionItemSelectPopup1.ApplySelection = null;
            conditionItemSelectPopup1.AutoFillColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("conditionItemSelectPopup1.AutoFillColumnNames")));
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
            conditionItemSelectPopup1.IsEnabled = true;
            conditionItemSelectPopup1.IsHidden = false;
            conditionItemSelectPopup1.IsImmediatlyUpdate = true;
            conditionItemSelectPopup1.IsKeyColumn = false;
            conditionItemSelectPopup1.IsMultiGrid = false;
            conditionItemSelectPopup1.IsReadOnly = false;
            conditionItemSelectPopup1.IsRequired = false;
            conditionItemSelectPopup1.IsSearchOnLoading = true;
            conditionItemSelectPopup1.LabelText = null;
            conditionItemSelectPopup1.LanguageKey = null;
            conditionItemSelectPopup1.LessThenEqual = false;
            conditionItemSelectPopup1.LessThenId = "";
            conditionItemSelectPopup1.NoSelectionMessageId = "";
            conditionItemSelectPopup1.PopupButtonStyle = Micube.Framework.SmartControls.PopupButtonStyles.Ok_Cancel;
            conditionItemSelectPopup1.PopupCustomValidation = null;
            conditionItemSelectPopup1.Position = 0D;
            conditionItemSelectPopup1.QueryPopup = null;
            conditionItemSelectPopup1.RelationIds = ((System.Collections.Generic.List<string>)(resources.GetObject("conditionItemSelectPopup1.RelationIds")));
            conditionItemSelectPopup1.ResultAction = null;
            conditionItemSelectPopup1.ResultCount = 1;
            //conditionItemSelectPopup1.SearchButtonReadOnly = false;
            conditionItemSelectPopup1.SearchQuery = null;
            //conditionItemSelectPopup1.SearchText = null;
            conditionItemSelectPopup1.SelectionQuery = null;
            conditionItemSelectPopup1.ShowSearchButton = true;
            conditionItemSelectPopup1.TextAlignment = Micube.Framework.SmartControls.TextAlignment.Default;
            conditionItemSelectPopup1.Title = null;
            conditionItemSelectPopup1.ToolTip = null;
            conditionItemSelectPopup1.ToolTipLanguageKey = null;
            conditionItemSelectPopup1.ValueFieldName = "";
            conditionItemSelectPopup1.WindowSize = new System.Drawing.Size(800, 500);
            this.popArea.SelectPopupCondition = conditionItemSelectPopup1;
            this.popArea.Size = new System.Drawing.Size(190, 20);
            this.popArea.TabIndex = 6;
            // 
            // lblWorker
            // 
            this.lblWorker.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblWorker.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblWorker.LanguageKey = "ACTUALUSER";
            this.lblWorker.Location = new System.Drawing.Point(266, 4);
            this.lblWorker.Margin = new System.Windows.Forms.Padding(5, 2, 5, 0);
            this.lblWorker.Name = "lblWorker";
            this.lblWorker.Size = new System.Drawing.Size(55, 20);
            this.lblWorker.TabIndex = 0;
            this.lblWorker.Text = "작업자";
            // 
            // cboUser
            // 
            this.cboUser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboUser.LabelText = null;
            this.cboUser.LanguageKey = null;
            this.cboUser.Location = new System.Drawing.Point(326, 4);
            this.cboUser.Margin = new System.Windows.Forms.Padding(0, 2, 15, 0);
            this.cboUser.Name = "cboUser";
            this.cboUser.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboUser.Properties.NullText = "";
            this.cboUser.ShowHeader = true;
            this.cboUser.Size = new System.Drawing.Size(100, 20);
            this.cboUser.TabIndex = 1;
            // 
            // lblLotId
            // 
            this.lblLotId.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLotId.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblLotId.LanguageKey = "LOTID";
            this.lblLotId.Location = new System.Drawing.Point(441, 4);
            this.lblLotId.Margin = new System.Windows.Forms.Padding(0, 2, 5, 0);
            this.lblLotId.Name = "lblLotId";
            this.lblLotId.Size = new System.Drawing.Size(45, 20);
            this.lblLotId.TabIndex = 2;
            this.lblLotId.Text = "LOT ID";
            // 
            // txtLotId
            // 
            this.txtLotId.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtLotId.LabelText = null;
            this.txtLotId.LanguageKey = null;
            this.txtLotId.Location = new System.Drawing.Point(491, 4);
            this.txtLotId.Margin = new System.Windows.Forms.Padding(0, 2, 15, 0);
            this.txtLotId.Name = "txtLotId";
            this.txtLotId.Size = new System.Drawing.Size(285, 20);
            this.txtLotId.TabIndex = 3;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.btnStartPacking);
            this.flowLayoutPanel3.Controls.Add(this.btnPrintLabel);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(47, 0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(858, 24);
            this.flowLayoutPanel3.TabIndex = 5;
            // 
            // btnStartPacking
            // 
            this.btnStartPacking.AllowFocus = false;
            this.btnStartPacking.IsBusy = false;
            this.btnStartPacking.LanguageKey = "PACKINGSTART";
            this.btnStartPacking.Location = new System.Drawing.Point(778, 0);
            this.btnStartPacking.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnStartPacking.Name = "btnStartPacking";
            this.btnStartPacking.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnStartPacking.Size = new System.Drawing.Size(80, 23);
            this.btnStartPacking.TabIndex = 0;
            this.btnStartPacking.Text = "포장작업완료";
            this.btnStartPacking.TooltipLanguageKey = "";
            // 
            // btnPrintLabel
            // 
            this.btnPrintLabel.AllowFocus = false;
            this.btnPrintLabel.IsBusy = false;
            this.btnPrintLabel.LanguageKey = "PRINTLABEL";
            this.btnPrintLabel.Location = new System.Drawing.Point(695, 0);
            this.btnPrintLabel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnPrintLabel.Name = "btnPrintLabel";
            this.btnPrintLabel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPrintLabel.Size = new System.Drawing.Size(80, 23);
            this.btnPrintLabel.TabIndex = 1;
            this.btnPrintLabel.Text = "라벨출력";
            this.btnPrintLabel.TooltipLanguageKey = "";
            // 
            // PackingResult_bak
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 649);
            this.ConditionsVisible = false;
            this.Name = "PackingResult_bak";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).EndInit();
            this.smartPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numDefectQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGoodQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPartition)).EndInit();
            this.tabPartition.ResumeLayout(false);
            this.PackingProcess.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtBoxNo.Properties)).EndInit();
            this.DefectList.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.popArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).EndInit();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
		private Commons.Controls.SmartLotInfoGrid grdLotInfo;
		private Framework.SmartControls.SmartPanel smartPanel2;
		private Framework.SmartControls.SmartTabControl tabPartition;
		private DevExpress.XtraTab.XtraTabPage PackingProcess;
		private DevExpress.XtraTab.XtraTabPage DefectList;
		private DevExpress.XtraTab.XtraTabPage PackingSpec;
		private Framework.SmartControls.SmartTextBox txtComment;
		private Framework.SmartControls.SmartLabel lblDefectQty;
		private Framework.SmartControls.SmartLabel lblGoodQty;
		private Framework.SmartControls.SmartLabel lblComment;
		private Framework.SmartControls.SmartLabel lblUom;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private Framework.SmartControls.SmartLabel lblLotId;
		private Framework.SmartControls.SmartTextBox txtLotId;
		private DefectListForPacking defectListForPacking;
		private Framework.SmartControls.SmartTextBox txtUnit;
		private Framework.SmartControls.SmartBandedGrid grdWaitPackingLotList;
		private Framework.SmartControls.SmartPanel smartPanel1;
		private Framework.SmartControls.SmartButton btnCreateBoxNo;
		private Framework.SmartControls.SmartTextBox txtBoxNo;
		private Framework.SmartControls.SmartLabel lblBoxNo;
		private Framework.SmartControls.SmartSpinEdit numDefectQty;
		private Framework.SmartControls.SmartSpinEdit numGoodQty;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
		private Framework.SmartControls.SmartButton btnStartPacking;
		private Framework.SmartControls.SmartButton btnPrintLabel;
		private Framework.SmartControls.SmartButton btnSaveLot;
		private Framework.SmartControls.SmartLabel lblArea;
		private Framework.SmartControls.SmartLabel lblWorker;
		private Framework.SmartControls.SmartComboBox cboUser;
		private Framework.SmartControls.SmartSelectPopupEdit popArea;
	}
}