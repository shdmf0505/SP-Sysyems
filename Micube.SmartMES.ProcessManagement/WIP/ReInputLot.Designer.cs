namespace Micube.SmartMES.ProcessManagement
{
    partial class ReInputLot
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
            this.tlpReInputLot = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdProductList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdProductionOrderList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.pnlProductionOrderInfo = new Micube.Framework.SmartControls.SmartPanel();
            this.tlpProductionOrderInfo = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.smartLabelSpinEdit1 = new Micube.Framework.SmartControls.SmartLabelSpinEdit();
            this.numPureInput = new Micube.Framework.SmartControls.SmartLabelSpinEdit();
            this.numRealInput = new Micube.Framework.SmartControls.SmartLabelSpinEdit();
            this.numYield = new Micube.Framework.SmartControls.SmartLabelSpinEdit();
            this.numSurplusApply = new Micube.Framework.SmartControls.SmartLabelSpinEdit();
            this.numLotSizePanel = new Micube.Framework.SmartControls.SmartLabelSpinEdit();
            this.numRealInputPanel = new Micube.Framework.SmartControls.SmartLabelSpinEdit();
            this.numReInputSequence = new Micube.Framework.SmartControls.SmartLabelSpinEdit();
            this.txtReason = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.smartSpliterControl3 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.tbLot = new Micube.Framework.SmartControls.SmartTabControl();
            this.tabpageLotist = new DevExpress.XtraTab.XtraTabPage();
            this.grdLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tabPageReinputReason = new DevExpress.XtraTab.XtraTabPage();
            this.grdReinputReason = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.btnReInputSplit = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.tlpReInputLot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlProductionOrderInfo)).BeginInit();
            this.pnlProductionOrderInfo.SuspendLayout();
            this.tlpProductionOrderInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartLabelSpinEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPureInput.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRealInput.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYield.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSurplusApply.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLotSizePanel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRealInputPanel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReInputSequence.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReason.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLot)).BeginInit();
            this.tbLot.SuspendLayout();
            this.tabpageLotist.SuspendLayout();
            this.tabPageReinputReason.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 463);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlToolbar.Size = new System.Drawing.Size(845, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.smartSplitTableLayoutPanel1, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tlpReInputLot);
            this.pnlContent.Size = new System.Drawing.Size(845, 467);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1150, 496);
            // 
            // tlpReInputLot
            // 
            this.tlpReInputLot.ColumnCount = 1;
            this.tlpReInputLot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpReInputLot.Controls.Add(this.grdProductList, 0, 4);
            this.tlpReInputLot.Controls.Add(this.grdProductionOrderList, 0, 0);
            this.tlpReInputLot.Controls.Add(this.smartSpliterControl1, 0, 1);
            this.tlpReInputLot.Controls.Add(this.pnlProductionOrderInfo, 0, 2);
            this.tlpReInputLot.Controls.Add(this.smartSpliterControl3, 0, 5);
            this.tlpReInputLot.Controls.Add(this.tbLot, 0, 6);
            this.tlpReInputLot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpReInputLot.Location = new System.Drawing.Point(0, 0);
            this.tlpReInputLot.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tlpReInputLot.Name = "tlpReInputLot";
            this.tlpReInputLot.RowCount = 7;
            this.tlpReInputLot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpReInputLot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpReInputLot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpReInputLot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.tlpReInputLot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpReInputLot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpReInputLot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tlpReInputLot.Size = new System.Drawing.Size(845, 467);
            this.tlpReInputLot.TabIndex = 0;
            // 
            // grdProductList
            // 
            this.grdProductList.Caption = "품목 List";
            this.grdProductList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProductList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdProductList.IsUsePaging = false;
            this.grdProductList.LanguageKey = null;
            this.grdProductList.Location = new System.Drawing.Point(0, 224);
            this.grdProductList.Margin = new System.Windows.Forms.Padding(0);
            this.grdProductList.Name = "grdProductList";
            this.grdProductList.ShowBorder = true;
            this.grdProductList.Size = new System.Drawing.Size(845, 115);
            this.grdProductList.TabIndex = 10;
            this.grdProductList.UseAutoBestFitColumns = false;
            // 
            // grdProductionOrderList
            // 
            this.grdProductionOrderList.Caption = "수주 List";
            this.grdProductionOrderList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProductionOrderList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdProductionOrderList.IsUsePaging = false;
            this.grdProductionOrderList.LanguageKey = null;
            this.grdProductionOrderList.Location = new System.Drawing.Point(0, 0);
            this.grdProductionOrderList.Margin = new System.Windows.Forms.Padding(0);
            this.grdProductionOrderList.Name = "grdProductionOrderList";
            this.grdProductionOrderList.ShowBorder = true;
            this.grdProductionOrderList.Size = new System.Drawing.Size(845, 115);
            this.grdProductionOrderList.TabIndex = 0;
            this.grdProductionOrderList.UseAutoBestFitColumns = false;
            // 
            // smartSpliterControl1
            // 
            this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartSpliterControl1.Location = new System.Drawing.Point(0, 115);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(845, 5);
            this.smartSpliterControl1.TabIndex = 1;
            this.smartSpliterControl1.TabStop = false;
            // 
            // pnlProductionOrderInfo
            // 
            this.pnlProductionOrderInfo.Controls.Add(this.tlpProductionOrderInfo);
            this.pnlProductionOrderInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProductionOrderInfo.Location = new System.Drawing.Point(0, 125);
            this.pnlProductionOrderInfo.Margin = new System.Windows.Forms.Padding(0);
            this.pnlProductionOrderInfo.Name = "pnlProductionOrderInfo";
            this.pnlProductionOrderInfo.Size = new System.Drawing.Size(845, 90);
            this.pnlProductionOrderInfo.TabIndex = 9;
            // 
            // tlpProductionOrderInfo
            // 
            this.tlpProductionOrderInfo.ColumnCount = 9;
            this.tlpProductionOrderInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpProductionOrderInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpProductionOrderInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpProductionOrderInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpProductionOrderInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpProductionOrderInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpProductionOrderInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpProductionOrderInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpProductionOrderInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tlpProductionOrderInfo.Controls.Add(this.smartLabelSpinEdit1, 0, 6);
            this.tlpProductionOrderInfo.Controls.Add(this.numPureInput, 1, 1);
            this.tlpProductionOrderInfo.Controls.Add(this.numRealInput, 3, 1);
            this.tlpProductionOrderInfo.Controls.Add(this.numYield, 5, 1);
            this.tlpProductionOrderInfo.Controls.Add(this.numSurplusApply, 1, 3);
            this.tlpProductionOrderInfo.Controls.Add(this.numLotSizePanel, 5, 3);
            this.tlpProductionOrderInfo.Controls.Add(this.numRealInputPanel, 3, 3);
            this.tlpProductionOrderInfo.Controls.Add(this.numReInputSequence, 7, 3);
            this.tlpProductionOrderInfo.Controls.Add(this.txtReason, 1, 5);
            this.tlpProductionOrderInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpProductionOrderInfo.Location = new System.Drawing.Point(2, 2);
            this.tlpProductionOrderInfo.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tlpProductionOrderInfo.Name = "tlpProductionOrderInfo";
            this.tlpProductionOrderInfo.RowCount = 7;
            this.tlpProductionOrderInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tlpProductionOrderInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpProductionOrderInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tlpProductionOrderInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpProductionOrderInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tlpProductionOrderInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpProductionOrderInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tlpProductionOrderInfo.Size = new System.Drawing.Size(841, 86);
            this.tlpProductionOrderInfo.TabIndex = 2;
            // 
            // smartLabelSpinEdit1
            // 
            this.smartLabelSpinEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLabelSpinEdit1.LabelText = "잉여 적용량";
            this.smartLabelSpinEdit1.LanguageKey = "SURPLUSAPPLY";
            this.smartLabelSpinEdit1.Location = new System.Drawing.Point(0, 81);
            this.smartLabelSpinEdit1.Margin = new System.Windows.Forms.Padding(0);
            this.smartLabelSpinEdit1.Name = "smartLabelSpinEdit1";
            this.smartLabelSpinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.smartLabelSpinEdit1.Properties.Mask.EditMask = "n0";
            this.smartLabelSpinEdit1.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.smartLabelSpinEdit1.Properties.ReadOnly = true;
            this.smartLabelSpinEdit1.Size = new System.Drawing.Size(10, 20);
            this.smartLabelSpinEdit1.TabIndex = 9;
            // 
            // numPureInput
            // 
            this.numPureInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numPureInput.LabelText = "순투입대상량";
            this.numPureInput.LanguageKey = "PUREINPUTQTY";
            this.numPureInput.Location = new System.Drawing.Point(10, 5);
            this.numPureInput.Margin = new System.Windows.Forms.Padding(0);
            this.numPureInput.Name = "numPureInput";
            this.numPureInput.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numPureInput.Properties.Mask.EditMask = "n0";
            this.numPureInput.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.numPureInput.Properties.ReadOnly = true;
            this.numPureInput.Size = new System.Drawing.Size(197, 20);
            this.numPureInput.TabIndex = 0;
            // 
            // numRealInput
            // 
            this.numRealInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numRealInput.LabelText = "실 투입량";
            this.numRealInput.LanguageKey = "REALINPUT";
            this.numRealInput.Location = new System.Drawing.Point(217, 5);
            this.numRealInput.Margin = new System.Windows.Forms.Padding(0);
            this.numRealInput.Name = "numRealInput";
            this.numRealInput.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numRealInput.Properties.Mask.EditMask = "n0";
            this.numRealInput.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.numRealInput.Properties.ReadOnly = true;
            this.numRealInput.Size = new System.Drawing.Size(197, 20);
            this.numRealInput.TabIndex = 1;
            // 
            // numYield
            // 
            this.numYield.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numYield.LabelText = "수율(%)";
            this.numYield.LanguageKey = "YEILD";
            this.numYield.Location = new System.Drawing.Point(424, 5);
            this.numYield.Margin = new System.Windows.Forms.Padding(0);
            this.numYield.Name = "numYield";
            this.numYield.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numYield.Properties.Mask.EditMask = "n0";
            this.numYield.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.numYield.Properties.ReadOnly = true;
            this.numYield.Size = new System.Drawing.Size(197, 20);
            this.numYield.TabIndex = 2;
            // 
            // numSurplusApply
            // 
            this.numSurplusApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numSurplusApply.LabelText = "잉여 적용량";
            this.numSurplusApply.LanguageKey = "SURPLUSAPPLY";
            this.numSurplusApply.Location = new System.Drawing.Point(10, 32);
            this.numSurplusApply.Margin = new System.Windows.Forms.Padding(0);
            this.numSurplusApply.Name = "numSurplusApply";
            this.numSurplusApply.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numSurplusApply.Properties.Mask.EditMask = "n0";
            this.numSurplusApply.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.numSurplusApply.Properties.ReadOnly = true;
            this.numSurplusApply.Size = new System.Drawing.Size(197, 20);
            this.numSurplusApply.TabIndex = 5;
            // 
            // numLotSizePanel
            // 
            this.numLotSizePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numLotSizePanel.LabelText = "LOT SIZE";
            this.numLotSizePanel.LanguageKey = "LOTSIZE";
            this.numLotSizePanel.Location = new System.Drawing.Point(424, 32);
            this.numLotSizePanel.Margin = new System.Windows.Forms.Padding(0);
            this.numLotSizePanel.Name = "numLotSizePanel";
            this.numLotSizePanel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numLotSizePanel.Properties.Mask.EditMask = "n0";
            this.numLotSizePanel.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.numLotSizePanel.Size = new System.Drawing.Size(197, 20);
            this.numLotSizePanel.TabIndex = 6;
            // 
            // numRealInputPanel
            // 
            this.numRealInputPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numRealInputPanel.LabelText = "기준투입(PNL)";
            this.numRealInputPanel.LanguageKey = "STANDARDINPUTPNL";
            this.numRealInputPanel.Location = new System.Drawing.Point(217, 32);
            this.numRealInputPanel.Margin = new System.Windows.Forms.Padding(0);
            this.numRealInputPanel.Name = "numRealInputPanel";
            this.numRealInputPanel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numRealInputPanel.Properties.Mask.EditMask = "n0";
            this.numRealInputPanel.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.numRealInputPanel.Size = new System.Drawing.Size(197, 20);
            this.numRealInputPanel.TabIndex = 7;
            // 
            // numReInputSequence
            // 
            this.numReInputSequence.Appearance.ForeColor = System.Drawing.Color.Black;
            this.numReInputSequence.Appearance.Options.UseForeColor = true;
            this.numReInputSequence.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numReInputSequence.LabelText = "재투입 차수";
            this.numReInputSequence.LanguageKey = "REINPUTSEQUENCE";
            this.numReInputSequence.Location = new System.Drawing.Point(631, 32);
            this.numReInputSequence.Margin = new System.Windows.Forms.Padding(0);
            this.numReInputSequence.Name = "numReInputSequence";
            this.numReInputSequence.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numReInputSequence.Properties.Mask.EditMask = "n0";
            this.numReInputSequence.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.numReInputSequence.Size = new System.Drawing.Size(197, 20);
            this.numReInputSequence.TabIndex = 8;
            // 
            // txtReason
            // 
            this.tlpProductionOrderInfo.SetColumnSpan(this.txtReason, 4);
            this.txtReason.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReason.LabelWidth = "14%";
            this.txtReason.LanguageKey = "REINPUTREASON";
            this.txtReason.Location = new System.Drawing.Point(13, 62);
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(408, 20);
            this.txtReason.TabIndex = 10;
            // 
            // smartSpliterControl3
            // 
            this.smartSpliterControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartSpliterControl3.Location = new System.Drawing.Point(0, 339);
            this.smartSpliterControl3.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl3.Name = "smartSpliterControl3";
            this.smartSpliterControl3.Size = new System.Drawing.Size(845, 5);
            this.smartSpliterControl3.TabIndex = 5;
            this.smartSpliterControl3.TabStop = false;
            // 
            // tbLot
            // 
            this.tbLot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLot.Location = new System.Drawing.Point(3, 352);
            this.tbLot.Name = "tbLot";
            this.tbLot.SelectedTabPage = this.tabpageLotist;
            this.tbLot.Size = new System.Drawing.Size(839, 112);
            this.tbLot.TabIndex = 11;
            this.tbLot.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabpageLotist,
            this.tabPageReinputReason});
            // 
            // tabpageLotist
            // 
            this.tabpageLotist.Controls.Add(this.grdLotList);
            this.tbLot.SetLanguageKey(this.tabpageLotist, "Lot List");
            this.tabpageLotist.Name = "tabpageLotist";
            this.tabpageLotist.Size = new System.Drawing.Size(833, 83);
            this.tabpageLotist.Text = "xtraTabPage1";
            // 
            // grdLotList
            // 
            this.grdLotList.Caption = "LOT List";
            this.grdLotList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLotList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLotList.IsUsePaging = false;
            this.grdLotList.LanguageKey = "GRIDLOTLIST";
            this.grdLotList.Location = new System.Drawing.Point(0, 0);
            this.grdLotList.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotList.Name = "grdLotList";
            this.grdLotList.ShowBorder = true;
            this.grdLotList.ShowStatusBar = false;
            this.grdLotList.Size = new System.Drawing.Size(833, 83);
            this.grdLotList.TabIndex = 12;
            this.grdLotList.UseAutoBestFitColumns = false;
            // 
            // tabPageReinputReason
            // 
            this.tabPageReinputReason.Controls.Add(this.grdReinputReason);
            this.tbLot.SetLanguageKey(this.tabPageReinputReason, "REINPUTREASON");
            this.tabPageReinputReason.Name = "tabPageReinputReason";
            this.tabPageReinputReason.Size = new System.Drawing.Size(833, 83);
            this.tabPageReinputReason.Text = "xtraTabPage2";
            // 
            // grdReinputReason
            // 
            this.grdReinputReason.Caption = "LOT List";
            this.grdReinputReason.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReinputReason.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdReinputReason.IsUsePaging = false;
            this.grdReinputReason.LanguageKey = "GRIDLOTLIST";
            this.grdReinputReason.Location = new System.Drawing.Point(0, 0);
            this.grdReinputReason.Margin = new System.Windows.Forms.Padding(0);
            this.grdReinputReason.Name = "grdReinputReason";
            this.grdReinputReason.ShowBorder = true;
            this.grdReinputReason.ShowStatusBar = false;
            this.grdReinputReason.Size = new System.Drawing.Size(833, 83);
            this.grdReinputReason.TabIndex = 13;
            this.grdReinputReason.UseAutoBestFitColumns = false;
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 4;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.btnReInputSplit, 3, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(47, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 1;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(798, 24);
            this.smartSplitTableLayoutPanel1.TabIndex = 5;
            // 
            // btnReInputSplit
            // 
            this.btnReInputSplit.AllowFocus = false;
            this.btnReInputSplit.IsBusy = false;
            this.btnReInputSplit.IsWrite = false;
            this.btnReInputSplit.LanguageKey = "REINPUTSPLIT";
            this.btnReInputSplit.Location = new System.Drawing.Point(716, 0);
            this.btnReInputSplit.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnReInputSplit.Name = "btnReInputSplit";
            this.btnReInputSplit.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnReInputSplit.Size = new System.Drawing.Size(75, 23);
            this.btnReInputSplit.TabIndex = 0;
            this.btnReInputSplit.Text = "재투입 분할";
            this.btnReInputSplit.TooltipLanguageKey = "";
            // 
            // ReInputLot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 516);
            this.Name = "ReInputLot";
            this.Text = "ReInputLot";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.tlpReInputLot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlProductionOrderInfo)).EndInit();
            this.pnlProductionOrderInfo.ResumeLayout(false);
            this.tlpProductionOrderInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartLabelSpinEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPureInput.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRealInput.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYield.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSurplusApply.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLotSizePanel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRealInputPanel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReInputSequence.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReason.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLot)).EndInit();
            this.tbLot.ResumeLayout(false);
            this.tabpageLotist.ResumeLayout(false);
            this.tabPageReinputReason.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel tlpReInputLot;
        private Framework.SmartControls.SmartBandedGrid grdProductionOrderList;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
        private Framework.SmartControls.SmartSplitTableLayoutPanel tlpProductionOrderInfo;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl3;
        private Framework.SmartControls.SmartLabelSpinEdit numPureInput;
        private Framework.SmartControls.SmartLabelSpinEdit numRealInput;
        private Framework.SmartControls.SmartLabelSpinEdit numYield;
        private Framework.SmartControls.SmartLabelSpinEdit numSurplusApply;
        private Framework.SmartControls.SmartLabelSpinEdit numLotSizePanel;
        private Framework.SmartControls.SmartLabelSpinEdit numRealInputPanel;
        private Framework.SmartControls.SmartLabelSpinEdit numReInputSequence;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartButton btnReInputSplit;
        private Framework.SmartControls.SmartPanel pnlProductionOrderInfo;
        private Framework.SmartControls.SmartBandedGrid grdProductList;
        private Framework.SmartControls.SmartLabelSpinEdit smartLabelSpinEdit1;
        private Framework.SmartControls.SmartLabelTextBox txtReason;
        private Framework.SmartControls.SmartTabControl tbLot;
        private DevExpress.XtraTab.XtraTabPage tabpageLotist;
        private Framework.SmartControls.SmartBandedGrid grdLotList;
        private DevExpress.XtraTab.XtraTabPage tabPageReinputReason;
        private Framework.SmartControls.SmartBandedGrid grdReinputReason;
    }
}
