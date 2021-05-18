namespace Micube.SmartMES.QualityAnalysis
{
    partial class ClaimLotManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClaimLotManagement));
            this.tpgHistory = new DevExpress.XtraTab.XtraTabPage();
            this.grdClaimHistory = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgClaimLot = new DevExpress.XtraTab.XtraTabPage();
            this.smartSpliterContainer2 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdDefectCode = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdDefectCodeCnt = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdClaimLotRouting = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.txtProductId = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtProductName = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtProcessSegmentName = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.flowLayoutPanel10 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClaimLot = new Micube.Framework.SmartControls.SmartButton();
            this.txtPnlCnt = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtPcsCnt = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDataUp = new Micube.Framework.SmartControls.SmartButton();
            this.btnDataDown = new Micube.Framework.SmartControls.SmartButton();
            this.tabClaimLot = new Micube.Framework.SmartControls.SmartTabControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.tpgHistory.SuspendLayout();
            this.tpgClaimLot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).BeginInit();
            this.smartSpliterContainer2.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProcessSegmentName.Properties)).BeginInit();
            this.flowLayoutPanel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPnlCnt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPcsCnt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabClaimLot)).BeginInit();
            this.tabClaimLot.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 770);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(1613, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabClaimLot);
            this.pnlContent.Size = new System.Drawing.Size(1613, 774);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1918, 803);
            // 
            // tpgHistory
            // 
            this.tpgHistory.Controls.Add(this.grdClaimHistory);
            this.tabClaimLot.SetLanguageKey(this.tpgHistory, "ISSUEDLOTHISTORY");
            this.tpgHistory.Name = "tpgHistory";
            this.tpgHistory.Size = new System.Drawing.Size(1607, 745);
            this.tpgHistory.Text = "내역조회";
            // 
            // grdClaimHistory
            // 
            this.grdClaimHistory.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdClaimHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdClaimHistory.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdClaimHistory.IsUsePaging = false;
            this.grdClaimHistory.LanguageKey = "ISSUEDLOTHISTORY";
            this.grdClaimHistory.Location = new System.Drawing.Point(0, 0);
            this.grdClaimHistory.Margin = new System.Windows.Forms.Padding(0);
            this.grdClaimHistory.Name = "grdClaimHistory";
            this.grdClaimHistory.ShowBorder = false;
            this.grdClaimHistory.Size = new System.Drawing.Size(1607, 745);
            this.grdClaimHistory.TabIndex = 0;
            this.grdClaimHistory.UseAutoBestFitColumns = false;
            // 
            // tpgClaimLot
            // 
            this.tpgClaimLot.Controls.Add(this.smartSpliterContainer2);
            this.tabClaimLot.SetLanguageKey(this.tpgClaimLot, "CLAIMPROCESS");
            this.tpgClaimLot.Name = "tpgClaimLot";
            this.tpgClaimLot.Size = new System.Drawing.Size(1607, 745);
            this.tpgClaimLot.Text = "Claim처리";
            // 
            // smartSpliterContainer2
            // 
            this.smartSpliterContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer2.Horizontal = false;
            this.smartSpliterContainer2.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer2.Name = "smartSpliterContainer2";
            this.smartSpliterContainer2.Panel1.Controls.Add(this.grdDefectCode);
            this.smartSpliterContainer2.Panel1.Text = "Panel1";
            this.smartSpliterContainer2.Panel2.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.smartSpliterContainer2.Panel2.Controls.Add(this.smartPanel1);
            this.smartSpliterContainer2.Panel2.Text = "Panel2";
            this.smartSpliterContainer2.Size = new System.Drawing.Size(1607, 745);
            this.smartSpliterContainer2.SplitterPosition = 360;
            this.smartSpliterContainer2.TabIndex = 0;
            this.smartSpliterContainer2.Text = "smartSpliterContainer2";
            // 
            // grdDefectCode
            // 
            this.grdDefectCode.Caption = "불량 Lot";
            this.grdDefectCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDefectCode.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdDefectCode.IsUsePaging = false;
            this.grdDefectCode.LanguageKey = "DEFECTLOT";
            this.grdDefectCode.Location = new System.Drawing.Point(0, 0);
            this.grdDefectCode.Margin = new System.Windows.Forms.Padding(0);
            this.grdDefectCode.Name = "grdDefectCode";
            this.grdDefectCode.ShowBorder = false;
            this.grdDefectCode.Size = new System.Drawing.Size(1607, 360);
            this.grdDefectCode.TabIndex = 0;
            this.grdDefectCode.UseAutoBestFitColumns = false;
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 3;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdDefectCodeCnt, 0, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdClaimLotRouting, 2, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 40);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 2;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(1607, 340);
            this.smartSplitTableLayoutPanel1.TabIndex = 1;
            // 
            // grdDefectCodeCnt
            // 
            this.grdDefectCodeCnt.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdDefectCodeCnt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDefectCodeCnt.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdDefectCodeCnt.IsUsePaging = false;
            this.grdDefectCodeCnt.LanguageKey = null;
            this.grdDefectCodeCnt.Location = new System.Drawing.Point(0, 28);
            this.grdDefectCodeCnt.Margin = new System.Windows.Forms.Padding(0);
            this.grdDefectCodeCnt.Name = "grdDefectCodeCnt";
            this.grdDefectCodeCnt.ShowBorder = false;
            this.grdDefectCodeCnt.ShowButtonBar = false;
            this.grdDefectCodeCnt.ShowStatusBar = false;
            this.grdDefectCodeCnt.Size = new System.Drawing.Size(958, 312);
            this.grdDefectCodeCnt.TabIndex = 2;
            this.grdDefectCodeCnt.UseAutoBestFitColumns = false;
            // 
            // grdClaimLotRouting
            // 
            this.grdClaimLotRouting.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdClaimLotRouting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdClaimLotRouting.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdClaimLotRouting.IsUsePaging = false;
            this.grdClaimLotRouting.LanguageKey = null;
            this.grdClaimLotRouting.Location = new System.Drawing.Point(968, 28);
            this.grdClaimLotRouting.Margin = new System.Windows.Forms.Padding(0);
            this.grdClaimLotRouting.Name = "grdClaimLotRouting";
            this.grdClaimLotRouting.ShowBorder = false;
            this.grdClaimLotRouting.ShowButtonBar = false;
            this.grdClaimLotRouting.ShowStatusBar = false;
            this.grdClaimLotRouting.Size = new System.Drawing.Size(639, 312);
            this.grdClaimLotRouting.TabIndex = 3;
            this.grdClaimLotRouting.UseAutoBestFitColumns = false;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.smartSplitTableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel3, 3);
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.6229F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.3771F));
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel3, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel10, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1607, 28);
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.txtProductId);
            this.flowLayoutPanel3.Controls.Add(this.txtProductName);
            this.flowLayoutPanel3.Controls.Add(this.txtProcessSegmentName);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(925, 28);
            this.flowLayoutPanel3.TabIndex = 0;
            // 
            // txtProductId
            // 
            this.txtProductId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductId.LabelText = "품목코드";
            this.txtProductId.LabelWidth = "30%";
            this.txtProductId.LanguageKey = null;
            this.txtProductId.Location = new System.Drawing.Point(3, 3);
            this.txtProductId.Name = "txtProductId";
            this.txtProductId.Properties.ReadOnly = true;
            this.txtProductId.Size = new System.Drawing.Size(180, 20);
            this.txtProductId.TabIndex = 0;
            // 
            // txtProductName
            // 
            this.txtProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductName.LabelText = "품목명";
            this.txtProductName.LabelWidth = "15%";
            this.txtProductName.LanguageKey = null;
            this.txtProductName.Location = new System.Drawing.Point(189, 3);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Properties.ReadOnly = true;
            this.txtProductName.Size = new System.Drawing.Size(340, 20);
            this.txtProductName.TabIndex = 1;
            // 
            // txtProcessSegmentName
            // 
            this.txtProcessSegmentName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProcessSegmentName.LabelText = "공정명";
            this.txtProcessSegmentName.LabelWidth = "20%";
            this.txtProcessSegmentName.LanguageKey = "PROCESSSEGMENTNAME";
            this.txtProcessSegmentName.Location = new System.Drawing.Point(535, 3);
            this.txtProcessSegmentName.Name = "txtProcessSegmentName";
            this.txtProcessSegmentName.Properties.ReadOnly = true;
            this.txtProcessSegmentName.Size = new System.Drawing.Size(310, 20);
            this.txtProcessSegmentName.TabIndex = 3;
            // 
            // flowLayoutPanel10
            // 
            this.flowLayoutPanel10.Controls.Add(this.btnClaimLot);
            this.flowLayoutPanel10.Controls.Add(this.txtPnlCnt);
            this.flowLayoutPanel10.Controls.Add(this.txtPcsCnt);
            this.flowLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel10.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel10.Location = new System.Drawing.Point(925, 0);
            this.flowLayoutPanel10.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel10.Name = "flowLayoutPanel10";
            this.flowLayoutPanel10.Size = new System.Drawing.Size(682, 28);
            this.flowLayoutPanel10.TabIndex = 1;
            // 
            // btnClaimLot
            // 
            this.btnClaimLot.AllowFocus = false;
            this.btnClaimLot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnClaimLot.IsBusy = false;
            this.btnClaimLot.IsWrite = true;
            this.btnClaimLot.LanguageKey = "CLAIMPROCESS";
            this.btnClaimLot.Location = new System.Drawing.Point(559, 1);
            this.btnClaimLot.Margin = new System.Windows.Forms.Padding(3, 1, 3, 0);
            this.btnClaimLot.Name = "btnClaimLot";
            this.btnClaimLot.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClaimLot.Size = new System.Drawing.Size(120, 25);
            this.btnClaimLot.TabIndex = 0;
            this.btnClaimLot.Text = "Claim처리";
            this.btnClaimLot.ToolTipIconType = DevExpress.Utils.ToolTipIconType.WindLogo;
            this.btnClaimLot.TooltipLanguageKey = "";
            // 
            // txtPnlCnt
            // 
            this.txtPnlCnt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPnlCnt.LabelText = "PNL";
            this.txtPnlCnt.LanguageKey = "PNL";
            this.txtPnlCnt.Location = new System.Drawing.Point(403, 3);
            this.txtPnlCnt.Name = "txtPnlCnt";
            this.txtPnlCnt.Properties.DisplayFormat.FormatString = "#,###,###,###,###,##0";
            this.txtPnlCnt.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPnlCnt.Properties.EditFormat.FormatString = "#,###,###,###,###,##0";
            this.txtPnlCnt.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPnlCnt.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtPnlCnt.Properties.ReadOnly = true;
            this.txtPnlCnt.Size = new System.Drawing.Size(150, 20);
            this.txtPnlCnt.TabIndex = 1;
            // 
            // txtPcsCnt
            // 
            this.txtPcsCnt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPcsCnt.LabelText = "PCS";
            this.txtPcsCnt.LanguageKey = "PCS";
            this.txtPcsCnt.Location = new System.Drawing.Point(247, 3);
            this.txtPcsCnt.Name = "txtPcsCnt";
            this.txtPcsCnt.Properties.DisplayFormat.FormatString = "#,###,###,###,###,##0";
            this.txtPcsCnt.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPcsCnt.Properties.EditFormat.FormatString = "#,###,###,###,###,##0";
            this.txtPcsCnt.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPcsCnt.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtPcsCnt.Properties.ReadOnly = true;
            this.txtPcsCnt.Size = new System.Drawing.Size(150, 20);
            this.txtPcsCnt.TabIndex = 2;
            // 
            // smartPanel1
            // 
            this.smartPanel1.Controls.Add(this.tableLayoutPanel1);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(1607, 40);
            this.smartPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnDataUp, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDataDown, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1603, 36);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnDataUp
            // 
            this.btnDataUp.AllowFocus = false;
            this.btnDataUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDataUp.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDataUp.ImageOptions.Image")));
            this.btnDataUp.IsBusy = false;
            this.btnDataUp.IsWrite = false;
            this.btnDataUp.Location = new System.Drawing.Point(804, 0);
            this.btnDataUp.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnDataUp.Name = "btnDataUp";
            this.btnDataUp.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnDataUp.Size = new System.Drawing.Size(54, 36);
            this.btnDataUp.TabIndex = 2;
            this.btnDataUp.TooltipLanguageKey = "";
            // 
            // btnDataDown
            // 
            this.btnDataDown.AllowFocus = false;
            this.btnDataDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDataDown.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDataDown.ImageOptions.Image")));
            this.btnDataDown.IsBusy = false;
            this.btnDataDown.IsWrite = false;
            this.btnDataDown.Location = new System.Drawing.Point(744, 0);
            this.btnDataDown.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnDataDown.Name = "btnDataDown";
            this.btnDataDown.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnDataDown.Size = new System.Drawing.Size(54, 36);
            this.btnDataDown.TabIndex = 1;
            this.btnDataDown.TooltipLanguageKey = "";
            // 
            // tabClaimLot
            // 
            this.tabClaimLot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabClaimLot.Location = new System.Drawing.Point(0, 0);
            this.tabClaimLot.Name = "tabClaimLot";
            this.tabClaimLot.SelectedTabPage = this.tpgClaimLot;
            this.tabClaimLot.Size = new System.Drawing.Size(1613, 774);
            this.tabClaimLot.TabIndex = 0;
            this.tabClaimLot.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgClaimLot,
            this.tpgHistory});
            // 
            // ClaimLotManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1938, 823);
            this.Name = "ClaimLotManagement";
            this.Text = "불량폐기취소";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.tpgHistory.ResumeLayout(false);
            this.tpgClaimLot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).EndInit();
            this.smartSpliterContainer2.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtProductId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProcessSegmentName.Properties)).EndInit();
            this.flowLayoutPanel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtPnlCnt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPcsCnt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabClaimLot)).EndInit();
            this.tabClaimLot.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabClaimLot;
        private DevExpress.XtraTab.XtraTabPage tpgClaimLot;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer2;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartBandedGrid grdDefectCodeCnt;
        private Framework.SmartControls.SmartBandedGrid grdClaimLotRouting;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private Framework.SmartControls.SmartLabelTextBox txtProductId;
        private Framework.SmartControls.SmartLabelTextBox txtProductName;
        private Framework.SmartControls.SmartLabelTextBox txtProcessSegmentName;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel10;
        private Framework.SmartControls.SmartButton btnClaimLot;
        private Framework.SmartControls.SmartLabelTextBox txtPnlCnt;
        private Framework.SmartControls.SmartLabelTextBox txtPcsCnt;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartButton btnDataUp;
        private Framework.SmartControls.SmartButton btnDataDown;
        private DevExpress.XtraTab.XtraTabPage tpgHistory;
        private Framework.SmartControls.SmartBandedGrid grdClaimHistory;
        private Framework.SmartControls.SmartBandedGrid grdDefectCode;
    }
}