namespace Micube.SmartMES.ProcessManagement
{
	partial class LotIncommingInspection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LotIncommingInspection));
            Micube.Framework.SmartControls.ConditionCollection conditionCollection1 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection2 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            this.grdLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.smartTabControl1 = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgDefectResult = new DevExpress.XtraTab.XtraTabPage();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.smartSpliterContainer2 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.popupInspectionUser = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            this.lblInspector = new Micube.Framework.SmartControls.SmartLabel();
            this.txtAreaName = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblArea = new Micube.Framework.SmartControls.SmartLabel();
            this.txtReinspectReason = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblReInspect = new Micube.Framework.SmartControls.SmartLabel();
            this.txtLotId = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblLot = new Micube.Framework.SmartControls.SmartLabel();
            this.grdLotQty = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdNcrList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.defectProcessListControl = new Micube.SmartMES.ProcessManagement.DefectProcessListControl();
            this.tpgInspectionHistory = new DevExpress.XtraTab.XtraTabPage();
            this.usInspectionResult1 = new Micube.SmartMES.ProcessManagement.usInspectionResult();
            this.pnlButton = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartTabControl1)).BeginInit();
            this.smartTabControl1.SuspendLayout();
            this.tpgDefectResult.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).BeginInit();
            this.smartSpliterContainer2.SuspendLayout();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupInspectionUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAreaName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReinspectReason.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).BeginInit();
            this.tpgInspectionHistory.SuspendLayout();
            this.pnlButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 651);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.pnlButton);
            this.pnlToolbar.Size = new System.Drawing.Size(949, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.pnlButton, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer1);
            this.pnlContent.Size = new System.Drawing.Size(949, 646);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1259, 680);
            // 
            // grdLotList
            // 
            this.grdLotList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.grdLotList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLotList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLotList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLotList.IsUsePaging = false;
            this.grdLotList.LanguageKey = null;
            this.grdLotList.Location = new System.Drawing.Point(0, 0);
            this.grdLotList.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotList.Name = "grdLotList";
            this.grdLotList.ShowBorder = true;
            this.grdLotList.Size = new System.Drawing.Size(949, 293);
            this.grdLotList.TabIndex = 0;
            this.grdLotList.UseAutoBestFitColumns = false;
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Horizontal = false;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.AutoScroll = true;
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdLotList);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.AutoScroll = true;
            this.smartSpliterContainer1.Panel2.Controls.Add(this.smartTabControl1);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(949, 646);
            this.smartSpliterContainer1.SplitterPosition = 293;
            this.smartSpliterContainer1.TabIndex = 2;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // smartTabControl1
            // 
            this.smartTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartTabControl1.Location = new System.Drawing.Point(0, 0);
            this.smartTabControl1.Name = "smartTabControl1";
            this.smartTabControl1.SelectedTabPage = this.tpgDefectResult;
            this.smartTabControl1.Size = new System.Drawing.Size(949, 343);
            this.smartTabControl1.TabIndex = 4;
            this.smartTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgDefectResult,
            this.tpgInspectionHistory});
            // 
            // tpgDefectResult
            // 
            this.tpgDefectResult.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.smartTabControl1.SetLanguageKey(this.tpgDefectResult, "DEFECTRESULT");
            this.tpgDefectResult.Name = "tpgDefectResult";
            this.tpgDefectResult.Padding = new System.Windows.Forms.Padding(3);
            this.tpgDefectResult.Size = new System.Drawing.Size(947, 313);
            this.tpgDefectResult.Text = "검사결과등록";
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartSpliterContainer2, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.defectProcessListControl, 0, 1);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 2;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 165F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(941, 307);
            this.smartSplitTableLayoutPanel1.TabIndex = 3;
            // 
            // smartSpliterContainer2
            // 
            this.smartSpliterContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer2.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer2.Name = "smartSpliterContainer2";
            this.smartSpliterContainer2.Panel1.Controls.Add(this.smartSplitTableLayoutPanel2);
            this.smartSpliterContainer2.Panel1.Text = "Panel1";
            this.smartSpliterContainer2.Panel2.Controls.Add(this.grdNcrList);
            this.smartSpliterContainer2.Panel2.Text = "Panel2";
            this.smartSpliterContainer2.Size = new System.Drawing.Size(941, 165);
            this.smartSpliterContainer2.SplitterPosition = 552;
            this.smartSpliterContainer2.TabIndex = 3;
            this.smartSpliterContainer2.Text = "smartSpliterContainer2";
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.ColumnCount = 1;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.smartPanel1, 0, 0);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdLotQty, 0, 1);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 2;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(552, 165);
            this.smartSplitTableLayoutPanel2.TabIndex = 0;
            // 
            // smartPanel1
            // 
            this.smartPanel1.AutoSize = true;
            this.smartPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel1.Controls.Add(this.popupInspectionUser);
            this.smartPanel1.Controls.Add(this.lblInspector);
            this.smartPanel1.Controls.Add(this.txtAreaName);
            this.smartPanel1.Controls.Add(this.lblArea);
            this.smartPanel1.Controls.Add(this.txtReinspectReason);
            this.smartPanel1.Controls.Add(this.lblReInspect);
            this.smartPanel1.Controls.Add(this.txtLotId);
            this.smartPanel1.Controls.Add(this.lblLot);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(552, 75);
            this.smartPanel1.TabIndex = 0;
            // 
            // popupInspectionUser
            // 
            this.popupInspectionUser.LabelText = null;
            this.popupInspectionUser.LanguageKey = null;
            this.popupInspectionUser.Location = new System.Drawing.Point(65, 36);
            this.popupInspectionUser.Name = "popupInspectionUser";
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
            conditionItemSelectPopup1.RelationIds = ((System.Collections.Generic.List<string>)(resources.GetObject("conditionItemSelectPopup1.RelationIds")));
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
            this.popupInspectionUser.SelectPopupCondition = conditionItemSelectPopup1;
            this.popupInspectionUser.Size = new System.Drawing.Size(162, 20);
            this.popupInspectionUser.TabIndex = 17;
            // 
            // lblInspector
            // 
            this.lblInspector.LanguageKey = "";
            this.lblInspector.Location = new System.Drawing.Point(10, 42);
            this.lblInspector.Name = "lblInspector";
            this.lblInspector.Size = new System.Drawing.Size(30, 14);
            this.lblInspector.TabIndex = 16;
            this.lblInspector.Text = "검사자";
            // 
            // txtAreaName
            // 
            this.txtAreaName.LabelText = null;
            this.txtAreaName.LanguageKey = null;
            this.txtAreaName.Location = new System.Drawing.Point(65, 3);
            this.txtAreaName.Name = "txtAreaName";
            this.txtAreaName.Properties.ReadOnly = true;
            this.txtAreaName.Size = new System.Drawing.Size(162, 20);
            this.txtAreaName.TabIndex = 15;
            // 
            // lblArea
            // 
            this.lblArea.LanguageKey = "AREA";
            this.lblArea.Location = new System.Drawing.Point(10, 6);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(30, 14);
            this.lblArea.TabIndex = 14;
            this.lblArea.Text = "작업장";
            // 
            // txtReinspectReason
            // 
            this.txtReinspectReason.LabelText = null;
            this.txtReinspectReason.LanguageKey = null;
            this.txtReinspectReason.Location = new System.Drawing.Point(300, 36);
            this.txtReinspectReason.Name = "txtReinspectReason";
            this.txtReinspectReason.Properties.ReadOnly = true;
            this.txtReinspectReason.Size = new System.Drawing.Size(249, 20);
            this.txtReinspectReason.TabIndex = 12;
            // 
            // lblReInspect
            // 
            this.lblReInspect.LanguageKey = "REINSPECTREASION";
            this.lblReInspect.Location = new System.Drawing.Point(247, 38);
            this.lblReInspect.Name = "lblReInspect";
            this.lblReInspect.Size = new System.Drawing.Size(39, 14);
            this.lblReInspect.TabIndex = 11;
            this.lblReInspect.Text = "LOT ID";
            // 
            // txtLotId
            // 
            this.txtLotId.LabelText = null;
            this.txtLotId.LanguageKey = null;
            this.txtLotId.Location = new System.Drawing.Point(300, 3);
            this.txtLotId.Name = "txtLotId";
            this.txtLotId.Properties.ReadOnly = true;
            this.txtLotId.Size = new System.Drawing.Size(249, 20);
            this.txtLotId.TabIndex = 12;
            // 
            // lblLot
            // 
            this.lblLot.LanguageKey = "LOTID";
            this.lblLot.Location = new System.Drawing.Point(247, 5);
            this.lblLot.Name = "lblLot";
            this.lblLot.Size = new System.Drawing.Size(39, 14);
            this.lblLot.TabIndex = 11;
            this.lblLot.Text = "LOT ID";
            // 
            // grdLotQty
            // 
            this.grdLotQty.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLotQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLotQty.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLotQty.IsUsePaging = false;
            this.grdLotQty.LanguageKey = null;
            this.grdLotQty.Location = new System.Drawing.Point(0, 75);
            this.grdLotQty.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotQty.Name = "grdLotQty";
            this.grdLotQty.ShowBorder = true;
            this.grdLotQty.ShowStatusBar = false;
            this.grdLotQty.Size = new System.Drawing.Size(552, 90);
            this.grdLotQty.TabIndex = 1;
            this.grdLotQty.UseAutoBestFitColumns = false;
            // 
            // grdNcrList
            // 
            this.grdNcrList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdNcrList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdNcrList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdNcrList.IsUsePaging = false;
            this.grdNcrList.LanguageKey = "NCRSTANDARD";
            this.grdNcrList.Location = new System.Drawing.Point(0, 0);
            this.grdNcrList.Margin = new System.Windows.Forms.Padding(0);
            this.grdNcrList.Name = "grdNcrList";
            this.grdNcrList.ShowBorder = true;
            this.grdNcrList.ShowStatusBar = false;
            this.grdNcrList.Size = new System.Drawing.Size(379, 165);
            this.grdNcrList.TabIndex = 0;
            this.grdNcrList.UseAutoBestFitColumns = false;
            // 
            // defectProcessListControl
            // 
            this.defectProcessListControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.defectProcessListControl.IsSaved = true;
            this.defectProcessListControl.Location = new System.Drawing.Point(0, 165);
            this.defectProcessListControl.Margin = new System.Windows.Forms.Padding(0);
            this.defectProcessListControl.Name = "defectProcessListControl";
            this.defectProcessListControl.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.defectProcessListControl.Size = new System.Drawing.Size(941, 142);
            this.defectProcessListControl.TabIndex = 4;
            // 
            // tpgInspectionHistory
            // 
            this.tpgInspectionHistory.Controls.Add(this.usInspectionResult1);
            this.smartTabControl1.SetLanguageKey(this.tpgInspectionHistory, "SELFINSPECTIONHISTORY");
            this.tpgInspectionHistory.Name = "tpgInspectionHistory";
            this.tpgInspectionHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tpgInspectionHistory.Size = new System.Drawing.Size(947, 313);
            this.tpgInspectionHistory.Text = "자주검사 이력";
            // 
            // usInspectionResult1
            // 
            this.usInspectionResult1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usInspectionResult1.InspectionData = null;
            this.usInspectionResult1.Location = new System.Drawing.Point(3, 3);
            this.usInspectionResult1.LotID = null;
            this.usInspectionResult1.Name = "usInspectionResult1";
            this.usInspectionResult1.Size = new System.Drawing.Size(941, 307);
            this.usInspectionResult1.TabIndex = 0;
            // 
            // pnlButton
            // 
            this.pnlButton.Controls.Add(this.btnSave);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButton.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlButton.Location = new System.Drawing.Point(45, 0);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(904, 24);
            this.pnlButton.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(826, 0);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "저장";
            this.btnSave.TooltipLanguageKey = "";
            // 
            // LotIncommingInspection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1297, 718);
            this.Name = "LotIncommingInspection";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartTabControl1)).EndInit();
            this.smartTabControl1.ResumeLayout(false);
            this.tpgDefectResult.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).EndInit();
            this.smartSpliterContainer2.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.smartPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupInspectionUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAreaName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReinspectReason.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).EndInit();
            this.tpgInspectionHistory.ResumeLayout(false);
            this.pnlButton.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private Framework.SmartControls.SmartBandedGrid grdLotList;
		private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
		private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
		private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer2;
		private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
		private Framework.SmartControls.SmartPanel smartPanel1;
		private Framework.SmartControls.SmartLabel lblInspector;
		private Framework.SmartControls.SmartTextBox txtAreaName;
		private Framework.SmartControls.SmartLabel lblArea;
		private Framework.SmartControls.SmartTextBox txtLotId;
		private Framework.SmartControls.SmartLabel lblLot;
		private Framework.SmartControls.SmartBandedGrid grdNcrList;
		private Framework.SmartControls.SmartBandedGrid grdLotQty;
		private DefectProcessListControl defectProcessListControl;
		private Framework.SmartControls.SmartSelectPopupEdit popupInspectionUser;
		private System.Windows.Forms.FlowLayoutPanel pnlButton;
		private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartTextBox txtReinspectReason;
        private Framework.SmartControls.SmartLabel lblReInspect;
        private Framework.SmartControls.SmartTabControl smartTabControl1;
        private DevExpress.XtraTab.XtraTabPage tpgDefectResult;
        private DevExpress.XtraTab.XtraTabPage tpgInspectionHistory;
        private usInspectionResult usInspectionResult1;
    }
}