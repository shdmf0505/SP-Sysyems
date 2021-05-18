namespace Micube.SmartMES.ProcessManagement
{
    partial class ProductDetail
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
            this.grdRouting = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cboProductDef = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.cboProductVersion = new Micube.Framework.SmartControls.SmartComboBox();
            this.txtCustomer = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtProductType = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtProductDefName = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.tabHistory = new Micube.Framework.SmartControls.SmartTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.grdConsume = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.grdDurable = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.grdEquipment = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
            this.grdDefect = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdRelated = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel2 = new System.Windows.Forms.Panel();
            this.tabMain = new Micube.Framework.SmartControls.SmartTabControl();
            this.xtraTabPage6 = new DevExpress.XtraTab.XtraTabPage();
            this.smartSpliterControl2 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.xtraTabPage7 = new DevExpress.XtraTab.XtraTabPage();
            this.grdWIP = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterControl3 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.grdLotHist = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.xtraTabPage5 = new DevExpress.XtraTab.XtraTabPage();
            this.grdResult = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboProductDef.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProductVersion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductDefName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabHistory)).BeginInit();
            this.tabHistory.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            this.xtraTabPage3.SuspendLayout();
            this.xtraTabPage4.SuspendLayout();
            this.smartPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.xtraTabPage6.SuspendLayout();
            this.xtraTabPage7.SuspendLayout();
            this.xtraTabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 485);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(931, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartPanel2);
            this.pnlContent.Controls.Add(this.smartPanel1);
            this.pnlContent.Size = new System.Drawing.Size(931, 489);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1236, 518);
            // 
            // grdRouting
            // 
            this.grdRouting.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdRouting.Dock = System.Windows.Forms.DockStyle.Left;
            this.grdRouting.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdRouting.IsUsePaging = false;
            this.grdRouting.LanguageKey = "PRODUCTROUTING";
            this.grdRouting.Location = new System.Drawing.Point(0, 5);
            this.grdRouting.Margin = new System.Windows.Forms.Padding(0);
            this.grdRouting.Name = "grdRouting";
            this.grdRouting.ShowBorder = true;
            this.grdRouting.Size = new System.Drawing.Size(335, 453);
            this.grdRouting.TabIndex = 1;
            this.grdRouting.UseAutoBestFitColumns = false;
            // 
            // smartPanel1
            // 
            this.smartPanel1.Controls.Add(this.tableLayoutPanel1);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(931, 31);
            this.smartPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.18182F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.45454F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.18182F));
            this.tableLayoutPanel1.Controls.Add(this.cboProductDef, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cboProductVersion, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtCustomer, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtProductType, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtProductDefName, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(927, 27);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // cboProductDef
            // 
            this.cboProductDef.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboProductDef.LabelText = "품목코드";
            this.cboProductDef.LabelWidth = "30%";
            this.cboProductDef.LanguageKey = "PRODUCTDEFID";
            this.cboProductDef.Location = new System.Drawing.Point(3, 3);
            this.cboProductDef.Name = "cboProductDef";
            this.cboProductDef.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboProductDef.Properties.NullText = "";
            this.cboProductDef.Size = new System.Drawing.Size(162, 20);
            this.cboProductDef.TabIndex = 4;
            // 
            // cboProductVersion
            // 
            this.cboProductVersion.LabelText = null;
            this.cboProductVersion.LanguageKey = null;
            this.cboProductVersion.Location = new System.Drawing.Point(171, 3);
            this.cboProductVersion.Name = "cboProductVersion";
            this.cboProductVersion.PopupWidth = 0;
            this.cboProductVersion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboProductVersion.Properties.NullText = "";
            this.cboProductVersion.ShowHeader = true;
            this.cboProductVersion.Size = new System.Drawing.Size(64, 20);
            this.cboProductVersion.TabIndex = 2;
            this.cboProductVersion.UseEmptyItem = true;
            this.cboProductVersion.VisibleColumns = null;
            this.cboProductVersion.VisibleColumnsWidth = null;
            // 
            // txtCustomer
            // 
            this.txtCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCustomer.LabelText = "고객사";
            this.txtCustomer.LabelWidth = "30%";
            this.txtCustomer.LanguageKey = "COMPANYCLIENT";
            this.txtCustomer.Location = new System.Drawing.Point(760, 3);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Size = new System.Drawing.Size(164, 20);
            this.txtCustomer.TabIndex = 0;
            // 
            // txtProductType
            // 
            this.txtProductType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductType.LabelText = "양산구분";
            this.txtProductType.LabelWidth = "60%";
            this.txtProductType.LanguageKey = "LOTPRODUCTTYPE";
            this.txtProductType.Location = new System.Drawing.Point(676, 3);
            this.txtProductType.Name = "txtProductType";
            this.txtProductType.Size = new System.Drawing.Size(78, 20);
            this.txtProductType.TabIndex = 0;
            // 
            // txtProductDefName
            // 
            this.txtProductDefName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductDefName.EditorWidth = "90%";
            this.txtProductDefName.LabelText = "품목명";
            this.txtProductDefName.LabelWidth = "10%";
            this.txtProductDefName.LanguageKey = "PRODUCTDEFNAME";
            this.txtProductDefName.Location = new System.Drawing.Point(255, 3);
            this.txtProductDefName.Name = "txtProductDefName";
            this.txtProductDefName.Size = new System.Drawing.Size(415, 20);
            this.txtProductDefName.TabIndex = 3;
            // 
            // tabHistory
            // 
            this.tabHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabHistory.Location = new System.Drawing.Point(3, 3);
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.SelectedTabPage = this.xtraTabPage1;
            this.tabHistory.Size = new System.Drawing.Size(579, 208);
            this.tabHistory.TabIndex = 8;
            this.tabHistory.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3,
            this.xtraTabPage4});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.grdConsume);
            this.tabHistory.SetLanguageKey(this.xtraTabPage1, "CONSUMABLEDEF");
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.xtraTabPage1.Size = new System.Drawing.Size(573, 179);
            this.xtraTabPage1.Text = "자재";
            // 
            // grdConsume
            // 
            this.grdConsume.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdConsume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdConsume.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdConsume.IsUsePaging = false;
            this.grdConsume.LanguageKey = "CONSUMABLE";
            this.grdConsume.Location = new System.Drawing.Point(3, 3);
            this.grdConsume.Margin = new System.Windows.Forms.Padding(0);
            this.grdConsume.Name = "grdConsume";
            this.grdConsume.ShowBorder = true;
            this.grdConsume.ShowStatusBar = false;
            this.grdConsume.Size = new System.Drawing.Size(567, 173);
            this.grdConsume.TabIndex = 2;
            this.grdConsume.UseAutoBestFitColumns = false;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.grdDurable);
            this.tabHistory.SetLanguageKey(this.xtraTabPage2, "DURABLE");
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.xtraTabPage2.Size = new System.Drawing.Size(117, 91);
            this.xtraTabPage2.Text = "치공구";
            // 
            // grdDurable
            // 
            this.grdDurable.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdDurable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDurable.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdDurable.IsUsePaging = false;
            this.grdDurable.LanguageKey = "DURABLE";
            this.grdDurable.Location = new System.Drawing.Point(3, 3);
            this.grdDurable.Margin = new System.Windows.Forms.Padding(0);
            this.grdDurable.Name = "grdDurable";
            this.grdDurable.ShowBorder = true;
            this.grdDurable.ShowStatusBar = false;
            this.grdDurable.Size = new System.Drawing.Size(111, 85);
            this.grdDurable.TabIndex = 3;
            this.grdDurable.UseAutoBestFitColumns = false;
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.grdEquipment);
            this.tabHistory.SetLanguageKey(this.xtraTabPage3, "EQUIPMENT");
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.xtraTabPage3.Size = new System.Drawing.Size(117, 91);
            this.xtraTabPage3.Text = "설비";
            // 
            // grdEquipment
            // 
            this.grdEquipment.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdEquipment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdEquipment.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdEquipment.IsUsePaging = false;
            this.grdEquipment.LanguageKey = "EQUIPMENT";
            this.grdEquipment.Location = new System.Drawing.Point(3, 3);
            this.grdEquipment.Margin = new System.Windows.Forms.Padding(0);
            this.grdEquipment.Name = "grdEquipment";
            this.grdEquipment.ShowBorder = true;
            this.grdEquipment.ShowStatusBar = false;
            this.grdEquipment.Size = new System.Drawing.Size(111, 85);
            this.grdEquipment.TabIndex = 3;
            this.grdEquipment.UseAutoBestFitColumns = false;
            // 
            // xtraTabPage4
            // 
            this.xtraTabPage4.Controls.Add(this.grdDefect);
            this.tabHistory.SetLanguageKey(this.xtraTabPage4, "DEFECTSTATUS");
            this.xtraTabPage4.Name = "xtraTabPage4";
            this.xtraTabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.xtraTabPage4.Size = new System.Drawing.Size(117, 91);
            this.xtraTabPage4.Text = "불량현황";
            // 
            // grdDefect
            // 
            this.grdDefect.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdDefect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDefect.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdDefect.IsUsePaging = false;
            this.grdDefect.LanguageKey = "DEFECT";
            this.grdDefect.Location = new System.Drawing.Point(3, 3);
            this.grdDefect.Margin = new System.Windows.Forms.Padding(0);
            this.grdDefect.Name = "grdDefect";
            this.grdDefect.ShowBorder = true;
            this.grdDefect.ShowStatusBar = false;
            this.grdDefect.Size = new System.Drawing.Size(111, 85);
            this.grdDefect.TabIndex = 3;
            this.grdDefect.UseAutoBestFitColumns = false;
            // 
            // grdRelated
            // 
            this.grdRelated.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdRelated.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grdRelated.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdRelated.IsUsePaging = false;
            this.grdRelated.LanguageKey = "SITEWIPLIST";
            this.grdRelated.Location = new System.Drawing.Point(3, 216);
            this.grdRelated.Margin = new System.Windows.Forms.Padding(0);
            this.grdRelated.Name = "grdRelated";
            this.grdRelated.ShowBorder = true;
            this.grdRelated.ShowStatusBar = false;
            this.grdRelated.Size = new System.Drawing.Size(579, 205);
            this.grdRelated.TabIndex = 10;
            this.grdRelated.UseAutoBestFitColumns = false;
            // 
            // smartPanel2
            // 
            this.smartPanel2.Controls.Add(this.tabMain);
            this.smartPanel2.Controls.Add(this.smartSpliterControl1);
            this.smartPanel2.Controls.Add(this.grdRouting);
            this.smartPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel2.Location = new System.Drawing.Point(0, 31);
            this.smartPanel2.Name = "smartPanel2";
            this.smartPanel2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.smartPanel2.Size = new System.Drawing.Size(931, 458);
            this.smartPanel2.TabIndex = 11;
            // 
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(340, 5);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedTabPage = this.xtraTabPage6;
            this.tabMain.Size = new System.Drawing.Size(591, 453);
            this.tabMain.TabIndex = 13;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage6,
            this.xtraTabPage7,
            this.xtraTabPage5});
            // 
            // xtraTabPage6
            // 
            this.xtraTabPage6.Controls.Add(this.tabHistory);
            this.xtraTabPage6.Controls.Add(this.smartSpliterControl2);
            this.xtraTabPage6.Controls.Add(this.grdRelated);
            this.tabMain.SetLanguageKey(this.xtraTabPage6, "PRODUCTDETAIL");
            this.xtraTabPage6.Name = "xtraTabPage6";
            this.xtraTabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.xtraTabPage6.Size = new System.Drawing.Size(585, 424);
            this.xtraTabPage6.Text = "품목현황";
            // 
            // smartSpliterControl2
            // 
            this.smartSpliterControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.smartSpliterControl2.Location = new System.Drawing.Point(3, 211);
            this.smartSpliterControl2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl2.Name = "smartSpliterControl2";
            this.smartSpliterControl2.Size = new System.Drawing.Size(579, 5);
            this.smartSpliterControl2.TabIndex = 12;
            this.smartSpliterControl2.TabStop = false;
            // 
            // xtraTabPage7
            // 
            this.xtraTabPage7.Controls.Add(this.grdWIP);
            this.xtraTabPage7.Controls.Add(this.smartSpliterControl3);
            this.xtraTabPage7.Controls.Add(this.grdLotHist);
            this.tabMain.SetLanguageKey(this.xtraTabPage7, "WIPLIST");
            this.xtraTabPage7.Name = "xtraTabPage7";
            this.xtraTabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.xtraTabPage7.Size = new System.Drawing.Size(129, 336);
            this.xtraTabPage7.Text = "재공현황";
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
            this.grdWIP.Location = new System.Drawing.Point(3, 3);
            this.grdWIP.Margin = new System.Windows.Forms.Padding(0);
            this.grdWIP.Name = "grdWIP";
            this.grdWIP.ShowBorder = true;
            this.grdWIP.ShowStatusBar = false;
            this.grdWIP.Size = new System.Drawing.Size(123, 94);
            this.grdWIP.TabIndex = 2;
            this.grdWIP.UseAutoBestFitColumns = false;
            // 
            // smartSpliterControl3
            // 
            this.smartSpliterControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.smartSpliterControl3.Location = new System.Drawing.Point(3, 97);
            this.smartSpliterControl3.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl3.Name = "smartSpliterControl3";
            this.smartSpliterControl3.Size = new System.Drawing.Size(123, 5);
            this.smartSpliterControl3.TabIndex = 13;
            this.smartSpliterControl3.TabStop = false;
            // 
            // grdLotHist
            // 
            this.grdLotHist.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLotHist.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grdLotHist.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLotHist.IsUsePaging = false;
            this.grdLotHist.LanguageKey = "LOTHISTORY";
            this.grdLotHist.Location = new System.Drawing.Point(3, 102);
            this.grdLotHist.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotHist.Name = "grdLotHist";
            this.grdLotHist.ShowBorder = true;
            this.grdLotHist.ShowStatusBar = false;
            this.grdLotHist.Size = new System.Drawing.Size(123, 231);
            this.grdLotHist.TabIndex = 3;
            this.grdLotHist.UseAutoBestFitColumns = false;
            // 
            // xtraTabPage5
            // 
            this.xtraTabPage5.Controls.Add(this.grdResult);
            this.tabMain.SetLanguageKey(this.xtraTabPage5, "WIPRESULT");
            this.xtraTabPage5.Name = "xtraTabPage5";
            this.xtraTabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.xtraTabPage5.Size = new System.Drawing.Size(129, 336);
            this.xtraTabPage5.Text = "공정실적";
            // 
            // grdResult
            // 
            this.grdResult.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdResult.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdResult.IsUsePaging = false;
            this.grdResult.LanguageKey = "WIPRESULT";
            this.grdResult.Location = new System.Drawing.Point(3, 3);
            this.grdResult.Margin = new System.Windows.Forms.Padding(0);
            this.grdResult.Name = "grdResult";
            this.grdResult.ShowBorder = true;
            this.grdResult.ShowStatusBar = false;
            this.grdResult.Size = new System.Drawing.Size(123, 330);
            this.grdResult.TabIndex = 3;
            this.grdResult.UseAutoBestFitColumns = false;
            // 
            // smartSpliterControl1
            // 
            this.smartSpliterControl1.Location = new System.Drawing.Point(335, 5);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(5, 453);
            this.smartSpliterControl1.TabIndex = 12;
            this.smartSpliterControl1.TabStop = false;
            // 
            // ProductDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1256, 538);
            this.LanguageKey = "PRODUCTDETAIL";
            this.Name = "ProductDetail";
            this.Text = "품목 상세 정보";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboProductDef.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProductVersion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductDefName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabHistory)).EndInit();
            this.tabHistory.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage3.ResumeLayout(false);
            this.xtraTabPage4.ResumeLayout(false);
            this.smartPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.xtraTabPage6.ResumeLayout(false);
            this.xtraTabPage7.ResumeLayout(false);
            this.xtraTabPage5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdRouting;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartTabControl tabHistory;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage4;
        private Framework.SmartControls.SmartLabelTextBox txtCustomer;
        private Framework.SmartControls.SmartLabelTextBox txtProductType;
        private Framework.SmartControls.SmartBandedGrid grdConsume;
        private Framework.SmartControls.SmartBandedGrid grdDurable;
        private Framework.SmartControls.SmartBandedGrid grdEquipment;
        private Framework.SmartControls.SmartBandedGrid grdDefect;
        private Framework.SmartControls.SmartBandedGrid grdRelated;
        private System.Windows.Forms.Panel smartPanel2;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
        private Framework.SmartControls.SmartComboBox cboProductVersion;
        private Framework.SmartControls.SmartLabelTextBox txtProductDefName;
        private Framework.SmartControls.SmartTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage6;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage7;
        private Framework.SmartControls.SmartBandedGrid grdWIP;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl3;
        private Framework.SmartControls.SmartBandedGrid grdLotHist;
        private Framework.SmartControls.SmartLabelComboBox cboProductDef;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage5;
        private Framework.SmartControls.SmartBandedGrid grdResult;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}