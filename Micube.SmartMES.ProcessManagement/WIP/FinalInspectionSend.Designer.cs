namespace Micube.SmartMES.ProcessManagement
{
    partial class FinalInspectionSend
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
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.pnlTopCondition = new Micube.Framework.SmartControls.SmartPanel();
            this.tlpTopCondition = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.txtArea = new Micube.Framework.SmartControls.SmartLabelSelectPopupEdit();
            this.txtLotId = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.cboTransitArea = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.grdLotInfo = new Micube.SmartMES.Commons.Controls.SmartLotInfoGrid();
            this.pfsInfo = new Micube.Framework.SmartControls.SmartPanel();
            this.txtDefectQty = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.lblComment = new Micube.Framework.SmartControls.SmartLabel();
            this.txtGoodQty = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtComment = new Micube.Framework.SmartControls.SmartTextBox();
            this.tabInfo = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgSplit = new DevExpress.XtraTab.XtraTabPage();
            this.grdLotSplit = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSplitQty = new Micube.Framework.SmartControls.SmartLabel();
            this.txtSplitQty = new Micube.Framework.SmartControls.SmartSpinEdit();
            this.txtParentLotQty = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.cboUOM = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.btnSplit = new Micube.Framework.SmartControls.SmartButton();
            this.tpgDefect = new DevExpress.XtraTab.XtraTabPage();
            this.grdDefect = new Micube.SmartMES.ProcessManagement.usDefectGrid();
            this.tpgMessage = new DevExpress.XtraTab.XtraTabPage();
            this.ucMessage = new Micube.SmartMES.ProcessManagement.usLotMessage();
            this.tpgComment = new DevExpress.XtraTab.XtraTabPage();
            this.grdComment = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgProcessSpec = new DevExpress.XtraTab.XtraTabPage();
            this.grdProcessSpec = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.btnInit = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTopCondition)).BeginInit();
            this.pnlTopCondition.SuspendLayout();
            this.tlpTopCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTransitArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pfsInfo)).BeginInit();
            this.pfsInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDefectQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGoodQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabInfo)).BeginInit();
            this.tabInfo.SuspendLayout();
            this.tpgSplit.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSplitQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParentLotQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUOM.Properties)).BeginInit();
            this.tpgDefect.SuspendLayout();
            this.tpgMessage.SuspendLayout();
            this.tpgComment.SuspendLayout();
            this.tpgProcessSpec.SuspendLayout();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(0, 30);
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.OptionsView.UseDefaultDragAndDropRendering = false;
            this.pnlCondition.Size = new System.Drawing.Size(0, 0);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.smartSplitTableLayoutPanel2);
            this.pnlToolbar.Size = new System.Drawing.Size(957, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.smartSplitTableLayoutPanel2, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlContent.Size = new System.Drawing.Size(957, 664);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(957, 693);
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdLotList, 0, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.pnlTopCondition, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdLotInfo, 0, 4);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.pfsInfo, 0, 6);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.tabInfo, 0, 8);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 9;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 11F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(957, 664);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // grdLotList
            // 
            this.grdLotList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLotList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLotList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLotList.IsUsePaging = false;
            this.grdLotList.LanguageKey = "LOTINFO";
            this.grdLotList.Location = new System.Drawing.Point(0, 44);
            this.grdLotList.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotList.Name = "grdLotList";
            this.grdLotList.ShowBorder = true;
            this.grdLotList.ShowStatusBar = false;
            this.grdLotList.Size = new System.Drawing.Size(957, 200);
            this.grdLotList.TabIndex = 3;
            this.grdLotList.UseAutoBestFitColumns = false;
            // 
            // pnlTopCondition
            // 
            this.pnlTopCondition.Controls.Add(this.tlpTopCondition);
            this.pnlTopCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTopCondition.Location = new System.Drawing.Point(0, 0);
            this.pnlTopCondition.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTopCondition.Name = "pnlTopCondition";
            this.pnlTopCondition.Size = new System.Drawing.Size(957, 34);
            this.pnlTopCondition.TabIndex = 0;
            // 
            // tlpTopCondition
            // 
            this.tlpTopCondition.ColumnCount = 9;
            this.tlpTopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpTopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.41176F));
            this.tlpTopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpTopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.41176F));
            this.tlpTopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpTopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.41176F));
            this.tlpTopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpTopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.76471F));
            this.tlpTopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tlpTopCondition.Controls.Add(this.txtArea, 1, 1);
            this.tlpTopCondition.Controls.Add(this.txtLotId, 3, 1);
            this.tlpTopCondition.Controls.Add(this.cboTransitArea, 5, 1);
            this.tlpTopCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTopCondition.Location = new System.Drawing.Point(2, 2);
            this.tlpTopCondition.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tlpTopCondition.Name = "tlpTopCondition";
            this.tlpTopCondition.RowCount = 3;
            this.tlpTopCondition.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4F));
            this.tlpTopCondition.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTopCondition.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tlpTopCondition.Size = new System.Drawing.Size(953, 30);
            this.tlpTopCondition.TabIndex = 0;
            // 
            // txtArea
            // 
            this.txtArea.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtArea.Appearance.Options.UseForeColor = true;
            this.txtArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtArea.LabelText = "작업장";
            this.txtArea.LanguageKey = "AREA";
            this.txtArea.Location = new System.Drawing.Point(10, 4);
            this.txtArea.Margin = new System.Windows.Forms.Padding(0);
            this.txtArea.Name = "txtArea";
            this.txtArea.Size = new System.Drawing.Size(252, 20);
            this.txtArea.TabIndex = 0;
            // 
            // txtLotId
            // 
            this.txtLotId.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtLotId.Appearance.Options.UseForeColor = true;
            this.txtLotId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLotId.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.txtLotId.LabelText = "LOT NO";
            this.txtLotId.LanguageKey = "LOTID";
            this.txtLotId.Location = new System.Drawing.Point(272, 4);
            this.txtLotId.Margin = new System.Windows.Forms.Padding(0);
            this.txtLotId.Name = "txtLotId";
            this.txtLotId.Size = new System.Drawing.Size(252, 20);
            this.txtLotId.TabIndex = 1;
            // 
            // cboTransitArea
            // 
            this.cboTransitArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboTransitArea.LabelText = "인계작업장";
            this.cboTransitArea.LabelWidth = "24%";
            this.cboTransitArea.LanguageKey = "TRANSITAREA";
            this.cboTransitArea.Location = new System.Drawing.Point(534, 4);
            this.cboTransitArea.Margin = new System.Windows.Forms.Padding(0);
            this.cboTransitArea.Name = "cboTransitArea";
            this.cboTransitArea.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTransitArea.Properties.NullText = "";
            this.cboTransitArea.Size = new System.Drawing.Size(252, 20);
            this.cboTransitArea.TabIndex = 9;
            // 
            // grdLotInfo
            // 
            this.grdLotInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLotInfo.Location = new System.Drawing.Point(0, 254);
            this.grdLotInfo.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotInfo.Name = "grdLotInfo";
            this.grdLotInfo.Size = new System.Drawing.Size(957, 103);
            this.grdLotInfo.TabIndex = 1;
            // 
            // pfsInfo
            // 
            this.pfsInfo.Controls.Add(this.txtDefectQty);
            this.pfsInfo.Controls.Add(this.lblComment);
            this.pfsInfo.Controls.Add(this.txtGoodQty);
            this.pfsInfo.Controls.Add(this.txtComment);
            this.pfsInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pfsInfo.Location = new System.Drawing.Point(3, 368);
            this.pfsInfo.Name = "pfsInfo";
            this.pfsInfo.Size = new System.Drawing.Size(951, 66);
            this.pfsInfo.TabIndex = 5;
            // 
            // txtDefectQty
            // 
            this.txtDefectQty.Enabled = false;
            this.txtDefectQty.LabelText = "불량 수량";
            this.txtDefectQty.LabelWidth = "30%";
            this.txtDefectQty.LanguageKey = "DEFECTQTY";
            this.txtDefectQty.Location = new System.Drawing.Point(235, 7);
            this.txtDefectQty.Name = "txtDefectQty";
            this.txtDefectQty.Size = new System.Drawing.Size(220, 20);
            this.txtDefectQty.TabIndex = 11;
            // 
            // lblComment
            // 
            this.lblComment.LanguageKey = "COMMENT";
            this.lblComment.Location = new System.Drawing.Point(11, 37);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(40, 14);
            this.lblComment.TabIndex = 7;
            this.lblComment.Text = "특이사항";
            // 
            // txtGoodQty
            // 
            this.txtGoodQty.Enabled = false;
            this.txtGoodQty.LabelText = "양품 수량";
            this.txtGoodQty.LabelWidth = "30%";
            this.txtGoodQty.LanguageKey = "GOODQTY";
            this.txtGoodQty.Location = new System.Drawing.Point(8, 7);
            this.txtGoodQty.Name = "txtGoodQty";
            this.txtGoodQty.Size = new System.Drawing.Size(220, 20);
            this.txtGoodQty.TabIndex = 0;
            // 
            // txtComment
            // 
            this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComment.LabelText = null;
            this.txtComment.LanguageKey = null;
            this.txtComment.Location = new System.Drawing.Point(88, 34);
            this.txtComment.Margin = new System.Windows.Forms.Padding(0);
            this.txtComment.Name = "txtComment";
            this.txtComment.Properties.AutoHeight = false;
            this.txtComment.Size = new System.Drawing.Size(855, 20);
            this.txtComment.TabIndex = 6;
            // 
            // tabInfo
            // 
            this.tabInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabInfo.Location = new System.Drawing.Point(3, 451);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.SelectedTabPage = this.tpgSplit;
            this.tabInfo.Size = new System.Drawing.Size(951, 210);
            this.tabInfo.TabIndex = 3;
            this.tabInfo.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgSplit,
            this.tpgDefect,
            this.tpgMessage,
            this.tpgComment,
            this.tpgProcessSpec});
            // 
            // tpgSplit
            // 
            this.tpgSplit.Controls.Add(this.grdLotSplit);
            this.tpgSplit.Controls.Add(this.panel1);
            this.tabInfo.SetLanguageKey(this.tpgSplit, "LOTSPLIT");
            this.tpgSplit.Name = "tpgSplit";
            this.tpgSplit.Padding = new System.Windows.Forms.Padding(3);
            this.tpgSplit.Size = new System.Drawing.Size(945, 181);
            this.tpgSplit.Text = "Lot 분할";
            // 
            // grdLotSplit
            // 
            this.grdLotSplit.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLotSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLotSplit.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLotSplit.IsUsePaging = false;
            this.grdLotSplit.LanguageKey = "LOTSPLIT";
            this.grdLotSplit.Location = new System.Drawing.Point(3, 39);
            this.grdLotSplit.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotSplit.Name = "grdLotSplit";
            this.grdLotSplit.ShowBorder = true;
            this.grdLotSplit.ShowStatusBar = false;
            this.grdLotSplit.Size = new System.Drawing.Size(939, 139);
            this.grdLotSplit.TabIndex = 4;
            this.grdLotSplit.UseAutoBestFitColumns = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblSplitQty);
            this.panel1.Controls.Add(this.txtSplitQty);
            this.panel1.Controls.Add(this.txtParentLotQty);
            this.panel1.Controls.Add(this.cboUOM);
            this.panel1.Controls.Add(this.btnSplit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(939, 36);
            this.panel1.TabIndex = 5;
            // 
            // lblSplitQty
            // 
            this.lblSplitQty.LanguageKey = "SPLITQTY";
            this.lblSplitQty.Location = new System.Drawing.Point(202, 10);
            this.lblSplitQty.Name = "lblSplitQty";
            this.lblSplitQty.Size = new System.Drawing.Size(44, 14);
            this.lblSplitQty.TabIndex = 7;
            this.lblSplitQty.Text = "분할 수량";
            // 
            // txtSplitQty
            // 
            this.txtSplitQty.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSplitQty.LabelText = null;
            this.txtSplitQty.LanguageKey = null;
            this.txtSplitQty.Location = new System.Drawing.Point(308, 8);
            this.txtSplitQty.Name = "txtSplitQty";
            this.txtSplitQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSplitQty.Size = new System.Drawing.Size(100, 20);
            this.txtSplitQty.TabIndex = 6;
            // 
            // txtParentLotQty
            // 
            this.txtParentLotQty.LabelText = "모 LOT 수량";
            this.txtParentLotQty.LanguageKey = null;
            this.txtParentLotQty.Location = new System.Drawing.Point(516, 8);
            this.txtParentLotQty.Name = "txtParentLotQty";
            this.txtParentLotQty.Properties.ReadOnly = true;
            this.txtParentLotQty.Size = new System.Drawing.Size(220, 20);
            this.txtParentLotQty.TabIndex = 3;
            this.txtParentLotQty.Visible = false;
            // 
            // cboUOM
            // 
            this.cboUOM.LabelText = "UOM";
            this.cboUOM.LabelWidth = "20%";
            this.cboUOM.LanguageKey = null;
            this.cboUOM.Location = new System.Drawing.Point(5, 8);
            this.cboUOM.Name = "cboUOM";
            this.cboUOM.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboUOM.Properties.NullText = "";
            this.cboUOM.Size = new System.Drawing.Size(177, 20);
            this.cboUOM.TabIndex = 2;
            // 
            // btnSplit
            // 
            this.btnSplit.AllowFocus = false;
            this.btnSplit.IsBusy = false;
            this.btnSplit.IsWrite = false;
            this.btnSplit.LanguageKey = "SPLIT";
            this.btnSplit.Location = new System.Drawing.Point(418, 5);
            this.btnSplit.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSplit.Size = new System.Drawing.Size(83, 25);
            this.btnSplit.TabIndex = 1;
            this.btnSplit.Text = "분할";
            this.btnSplit.TooltipLanguageKey = "";
            // 
            // tpgDefect
            // 
            this.tpgDefect.Controls.Add(this.grdDefect);
            this.tabInfo.SetLanguageKey(this.tpgDefect, "DEFECTINFO");
            this.tpgDefect.Name = "tpgDefect";
            this.tpgDefect.Padding = new System.Windows.Forms.Padding(3);
            this.tpgDefect.Size = new System.Drawing.Size(744, 0);
            this.tpgDefect.Text = "불량 정보";
            // 
            // grdDefect
            // 
            this.grdDefect.DataSource = null;
            this.grdDefect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDefect.Location = new System.Drawing.Point(3, 3);
            this.grdDefect.LotID = null;
            this.grdDefect.Name = "grdDefect";
            this.grdDefect.Size = new System.Drawing.Size(738, 0);
            this.grdDefect.TabIndex = 0;
            this.grdDefect.VisibleTopDefectCode = false;
            // 
            // tpgMessage
            // 
            this.tpgMessage.Controls.Add(this.ucMessage);
            this.tabInfo.SetLanguageKey(this.tpgMessage, "MESSAGE");
            this.tpgMessage.Name = "tpgMessage";
            this.tpgMessage.Padding = new System.Windows.Forms.Padding(3);
            this.tpgMessage.Size = new System.Drawing.Size(744, 0);
            this.tpgMessage.Text = "Message";
            // 
            // ucMessage
            // 
            this.ucMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMessage.Location = new System.Drawing.Point(3, 3);
            this.ucMessage.MessageDataSource = null;
            this.ucMessage.Name = "ucMessage";
            this.ucMessage.Size = new System.Drawing.Size(738, 0);
            this.ucMessage.TabIndex = 3;
            // 
            // tpgComment
            // 
            this.tpgComment.Controls.Add(this.grdComment);
            this.tabInfo.SetLanguageKey(this.tpgComment, "REMARKS");
            this.tpgComment.Name = "tpgComment";
            this.tpgComment.Padding = new System.Windows.Forms.Padding(3);
            this.tpgComment.Size = new System.Drawing.Size(744, 0);
            this.tpgComment.Text = "특기사항";
            // 
            // grdComment
            // 
            this.grdComment.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdComment.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdComment.IsUsePaging = false;
            this.grdComment.LanguageKey = null;
            this.grdComment.Location = new System.Drawing.Point(3, 3);
            this.grdComment.Margin = new System.Windows.Forms.Padding(0);
            this.grdComment.Name = "grdComment";
            this.grdComment.ShowBorder = true;
            this.grdComment.ShowStatusBar = false;
            this.grdComment.Size = new System.Drawing.Size(738, 0);
            this.grdComment.TabIndex = 4;
            this.grdComment.UseAutoBestFitColumns = false;
            // 
            // tpgProcessSpec
            // 
            this.tpgProcessSpec.Controls.Add(this.grdProcessSpec);
            this.tabInfo.SetLanguageKey(this.tpgProcessSpec, "PROCESSSPEC");
            this.tpgProcessSpec.Name = "tpgProcessSpec";
            this.tpgProcessSpec.Padding = new System.Windows.Forms.Padding(3);
            this.tpgProcessSpec.Size = new System.Drawing.Size(744, 0);
            this.tpgProcessSpec.Text = "공정 SPEC";
            // 
            // grdProcessSpec
            // 
            this.grdProcessSpec.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdProcessSpec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProcessSpec.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdProcessSpec.IsUsePaging = false;
            this.grdProcessSpec.LanguageKey = null;
            this.grdProcessSpec.Location = new System.Drawing.Point(3, 3);
            this.grdProcessSpec.Margin = new System.Windows.Forms.Padding(0);
            this.grdProcessSpec.Name = "grdProcessSpec";
            this.grdProcessSpec.ShowBorder = true;
            this.grdProcessSpec.ShowStatusBar = false;
            this.grdProcessSpec.Size = new System.Drawing.Size(738, 0);
            this.grdProcessSpec.TabIndex = 4;
            this.grdProcessSpec.UseAutoBestFitColumns = false;
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.ColumnCount = 2;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.btnInit, 3, 0);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(47, 0);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 1;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(910, 24);
            this.smartSplitTableLayoutPanel2.TabIndex = 6;
            // 
            // btnInit
            // 
            this.btnInit.AllowFocus = false;
            this.btnInit.IsBusy = false;
            this.btnInit.IsWrite = false;
            this.btnInit.LanguageKey = "INITIALIZE";
            this.btnInit.Location = new System.Drawing.Point(828, 0);
            this.btnInit.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnInit.Name = "btnInit";
            this.btnInit.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnInit.Size = new System.Drawing.Size(75, 23);
            this.btnInit.TabIndex = 0;
            this.btnInit.Text = "초기화";
            this.btnInit.TooltipLanguageKey = "";
            // 
            // FinalInspectionSend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 713);
            this.ConditionsVisible = false;
            this.LanguageKey = "SEG0560";
            this.Name = "FinalInspectionSend";
            this.ShowSaveCompleteMessage = false;
            this.Text = "최종검사 작업 완료";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTopCondition)).EndInit();
            this.pnlTopCondition.ResumeLayout(false);
            this.tlpTopCondition.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTransitArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pfsInfo)).EndInit();
            this.pfsInfo.ResumeLayout(false);
            this.pfsInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDefectQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGoodQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabInfo)).EndInit();
            this.tabInfo.ResumeLayout(false);
            this.tpgSplit.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSplitQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParentLotQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUOM.Properties)).EndInit();
            this.tpgDefect.ResumeLayout(false);
            this.tpgMessage.ResumeLayout(false);
            this.tpgComment.ResumeLayout(false);
            this.tpgProcessSpec.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
        private Framework.SmartControls.SmartButton btnInit;
        private Framework.SmartControls.SmartPanel pnlTopCondition;
        private Framework.SmartControls.SmartSplitTableLayoutPanel tlpTopCondition;
        private Framework.SmartControls.SmartLabelSelectPopupEdit txtArea;
        private Framework.SmartControls.SmartLabelTextBox txtLotId;
        private Commons.Controls.SmartLotInfoGrid grdLotInfo;
        private Framework.SmartControls.SmartTabControl tabInfo;
        private DevExpress.XtraTab.XtraTabPage tpgDefect;
        private DevExpress.XtraTab.XtraTabPage tpgMessage;
        private Framework.SmartControls.SmartPanel pfsInfo;
        private Framework.SmartControls.SmartLabel lblComment;
        private Framework.SmartControls.SmartTextBox txtComment;
        private Framework.SmartControls.SmartBandedGrid grdLotList;
        private Framework.SmartControls.SmartLabelComboBox cboTransitArea;
        private DevExpress.XtraTab.XtraTabPage tpgSplit;
        private Framework.SmartControls.SmartBandedGrid grdLotSplit;
        private System.Windows.Forms.Panel panel1;
        private Framework.SmartControls.SmartLabelTextBox txtParentLotQty;
        private Framework.SmartControls.SmartLabelComboBox cboUOM;
        private Framework.SmartControls.SmartButton btnSplit;
        private Framework.SmartControls.SmartLabelTextBox txtDefectQty;
        private Framework.SmartControls.SmartLabelTextBox txtGoodQty;
        private usLotMessage ucMessage;
        private DevExpress.XtraTab.XtraTabPage tpgComment;
        private DevExpress.XtraTab.XtraTabPage tpgProcessSpec;
        private Framework.SmartControls.SmartBandedGrid grdComment;
        private Framework.SmartControls.SmartBandedGrid grdProcessSpec;
        private Framework.SmartControls.SmartLabel lblSplitQty;
        private Framework.SmartControls.SmartSpinEdit txtSplitQty;
        private usDefectGrid grdDefect;
    }
}