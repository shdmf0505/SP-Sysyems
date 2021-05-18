namespace Micube.SmartMES.ProcessManagement
{
    partial class PackingTransit
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
            this.grdPackingList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tabPacking = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgProduct = new DevExpress.XtraTab.XtraTabPage();
            this.tpgExport = new DevExpress.XtraTab.XtraTabPage();
            this.spcSpliter = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.grdExportLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.grdExportPackingList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboTransitArea = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.smartSpliterControl2 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.ucDataUpDownBtnCtrl = new Micube.SmartMES.Commons.Controls.ucDataUpDownBtn();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPacking)).BeginInit();
            this.tabPacking.SuspendLayout();
            this.tpgProduct.SuspendLayout();
            this.tpgExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboTransitArea.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(371, 700);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(785, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabPacking);
            this.pnlContent.Size = new System.Drawing.Size(785, 703);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1166, 739);
            // 
            // grdPackingList
            // 
            this.grdPackingList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdPackingList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPackingList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdPackingList.IsUsePaging = false;
            this.grdPackingList.LanguageKey = "WIPLIST";
            this.grdPackingList.Location = new System.Drawing.Point(3, 3);
            this.grdPackingList.Margin = new System.Windows.Forms.Padding(0);
            this.grdPackingList.Name = "grdPackingList";
            this.grdPackingList.ShowBorder = true;
            this.grdPackingList.ShowStatusBar = false;
            this.grdPackingList.Size = new System.Drawing.Size(772, 299);
            this.grdPackingList.TabIndex = 1;
            this.grdPackingList.UseAutoBestFitColumns = false;
            // 
            // grdLotList
            // 
            this.grdLotList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLotList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grdLotList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLotList.IsUsePaging = false;
            this.grdLotList.LanguageKey = "LOTINFO";
            this.grdLotList.Location = new System.Drawing.Point(3, 308);
            this.grdLotList.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotList.Name = "grdLotList";
            this.grdLotList.ShowBorder = true;
            this.grdLotList.ShowStatusBar = false;
            this.grdLotList.Size = new System.Drawing.Size(772, 356);
            this.grdLotList.TabIndex = 8;
            this.grdLotList.UseAutoBestFitColumns = false;
            // 
            // tabPacking
            // 
            this.tabPacking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPacking.Location = new System.Drawing.Point(0, 0);
            this.tabPacking.Name = "tabPacking";
            this.tabPacking.SelectedTabPage = this.tpgProduct;
            this.tabPacking.Size = new System.Drawing.Size(785, 703);
            this.tabPacking.TabIndex = 9;
            this.tabPacking.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgProduct,
            this.tpgExport});
            // 
            // tpgProduct
            // 
            this.tpgProduct.Controls.Add(this.grdPackingList);
            this.tpgProduct.Controls.Add(this.spcSpliter);
            this.tpgProduct.Controls.Add(this.grdLotList);
            this.tabPacking.SetLanguageKey(this.tpgProduct, "PACKINGPRODUCT");
            this.tpgProduct.Name = "tpgProduct";
            this.tpgProduct.Padding = new System.Windows.Forms.Padding(3);
            this.tpgProduct.Size = new System.Drawing.Size(778, 667);
            this.tpgProduct.Text = "제품 포장";
            // 
            // tpgExport
            // 
            this.tpgExport.Controls.Add(this.grdExportPackingList);
            this.tpgExport.Controls.Add(this.ucDataUpDownBtnCtrl);
            this.tpgExport.Controls.Add(this.smartSpliterControl2);
            this.tpgExport.Controls.Add(this.smartPanel1);
            this.tpgExport.Controls.Add(this.smartSpliterControl1);
            this.tabPacking.SetLanguageKey(this.tpgExport, "PACKINGEXPORT");
            this.tpgExport.Name = "tpgExport";
            this.tpgExport.Padding = new System.Windows.Forms.Padding(3);
            this.tpgExport.Size = new System.Drawing.Size(778, 667);
            this.tpgExport.Text = "수출 포장";
            // 
            // spcSpliter
            // 
            this.spcSpliter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.spcSpliter.Location = new System.Drawing.Point(3, 302);
            this.spcSpliter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcSpliter.Name = "spcSpliter";
            this.spcSpliter.Size = new System.Drawing.Size(772, 6);
            this.spcSpliter.TabIndex = 9;
            this.spcSpliter.TabStop = false;
            // 
            // grdExportLotList
            // 
            this.grdExportLotList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdExportLotList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdExportLotList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdExportLotList.IsUsePaging = false;
            this.grdExportLotList.LanguageKey = "LOTINFO";
            this.grdExportLotList.Location = new System.Drawing.Point(2, 44);
            this.grdExportLotList.Margin = new System.Windows.Forms.Padding(0);
            this.grdExportLotList.Name = "grdExportLotList";
            this.grdExportLotList.ShowBorder = true;
            this.grdExportLotList.Size = new System.Drawing.Size(768, 356);
            this.grdExportLotList.TabIndex = 9;
            this.grdExportLotList.UseAutoBestFitColumns = false;
            // 
            // smartSpliterControl1
            // 
            this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartSpliterControl1.Location = new System.Drawing.Point(3, 3);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(772, 6);
            this.smartSpliterControl1.TabIndex = 10;
            this.smartSpliterControl1.TabStop = false;
            // 
            // grdExportPackingList
            // 
            this.grdExportPackingList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdExportPackingList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdExportPackingList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdExportPackingList.IsUsePaging = false;
            this.grdExportPackingList.LanguageKey = "WIPLIST";
            this.grdExportPackingList.Location = new System.Drawing.Point(3, 9);
            this.grdExportPackingList.Margin = new System.Windows.Forms.Padding(0);
            this.grdExportPackingList.Name = "grdExportPackingList";
            this.grdExportPackingList.ShowBorder = true;
            this.grdExportPackingList.Size = new System.Drawing.Size(772, 193);
            this.grdExportPackingList.TabIndex = 11;
            this.grdExportPackingList.UseAutoBestFitColumns = false;
            // 
            // smartPanel1
            // 
            this.smartPanel1.Controls.Add(this.grdExportLotList);
            this.smartPanel1.Controls.Add(this.panel1);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.smartPanel1.Location = new System.Drawing.Point(3, 262);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(772, 402);
            this.smartPanel1.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cboTransitArea);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(768, 42);
            this.panel1.TabIndex = 10;
            // 
            // cboTransitArea
            // 
            this.cboTransitArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTransitArea.LabelText = "인계작업장";
            this.cboTransitArea.LabelWidth = "20%";
            this.cboTransitArea.LanguageKey = "TRANSITAREA";
            this.cboTransitArea.Location = new System.Drawing.Point(13, 9);
            this.cboTransitArea.Name = "cboTransitArea";
            this.cboTransitArea.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTransitArea.Properties.NullText = "";
            this.cboTransitArea.Size = new System.Drawing.Size(435, 24);
            this.cboTransitArea.TabIndex = 8;
            // 
            // smartSpliterControl2
            // 
            this.smartSpliterControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.smartSpliterControl2.Location = new System.Drawing.Point(3, 256);
            this.smartSpliterControl2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl2.Name = "smartSpliterControl2";
            this.smartSpliterControl2.Size = new System.Drawing.Size(772, 6);
            this.smartSpliterControl2.TabIndex = 13;
            this.smartSpliterControl2.TabStop = false;
            // 
            // ucDataUpDownBtnCtrl
            // 
            this.ucDataUpDownBtnCtrl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucDataUpDownBtnCtrl.Location = new System.Drawing.Point(3, 202);
            this.ucDataUpDownBtnCtrl.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.ucDataUpDownBtnCtrl.Name = "ucDataUpDownBtnCtrl";
            this.ucDataUpDownBtnCtrl.Size = new System.Drawing.Size(772, 54);
            this.ucDataUpDownBtnCtrl.SourceGrid = null;
            this.ucDataUpDownBtnCtrl.TabIndex = 14;
            this.ucDataUpDownBtnCtrl.TargetGrid = null;
            // 
            // PackingTransit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1196, 769);
            this.LanguageKey = "PACKINGENDLIST";
            this.Name = "PackingTransit";
            this.Text = "포장 인계";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPacking)).EndInit();
            this.tabPacking.ResumeLayout(false);
            this.tpgProduct.ResumeLayout(false);
            this.tpgExport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboTransitArea.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdPackingList;
        private Framework.SmartControls.SmartBandedGrid grdLotList;
        private Framework.SmartControls.SmartTabControl tabPacking;
        private DevExpress.XtraTab.XtraTabPage tpgProduct;
        private Framework.SmartControls.SmartSpliterControl spcSpliter;
        private DevExpress.XtraTab.XtraTabPage tpgExport;
        private Framework.SmartControls.SmartBandedGrid grdExportPackingList;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
        private Framework.SmartControls.SmartBandedGrid grdExportLotList;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private System.Windows.Forms.Panel panel1;
        private Framework.SmartControls.SmartLabelComboBox cboTransitArea;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl2;
        private Commons.Controls.ucDataUpDownBtn ucDataUpDownBtnCtrl;
    }
}