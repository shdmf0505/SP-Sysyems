namespace Micube.SmartMES.ProcessManagement
{
    partial class InputLotRecord
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputLotRecord));
            this.grdInputResultByDay = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.spcSpliter = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.toolTipWipInfo = new DevExpress.Utils.ToolTipController(this.components);
            this.tabMain = new Micube.Framework.SmartControls.SmartTabControl();
            this.tbInputResult = new DevExpress.XtraTab.XtraTabPage();
            this.tbProduct = new DevExpress.XtraTab.XtraTabPage();
            this.grdProduct = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tbPivot = new DevExpress.XtraTab.XtraTabPage();
            this.gbPivot = new Micube.Framework.SmartControls.SmartGroupBox();
            this.pvtInputResult = new Micube.Framework.SmartControls.SmartPivotGridControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.tbLot = new DevExpress.XtraTab.XtraTabPage();
            this.grdLot = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tbInputResult.SuspendLayout();
            this.tbProduct.SuspendLayout();
            this.tbPivot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbPivot)).BeginInit();
            this.gbPivot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pvtInputResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.tbLot.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabMain);
            this.pnlContent.Controls.Add(this.spcSpliter);
            // 
            // grdInputResultByDay
            // 
            this.grdInputResultByDay.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdInputResultByDay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInputResultByDay.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.Export;
            this.grdInputResultByDay.IsUsePaging = false;
            this.grdInputResultByDay.LanguageKey = "INPUTRESULT";
            this.grdInputResultByDay.Location = new System.Drawing.Point(0, 0);
            this.grdInputResultByDay.Margin = new System.Windows.Forms.Padding(0);
            this.grdInputResultByDay.Name = "grdInputResultByDay";
            this.grdInputResultByDay.ShowBorder = true;
            this.grdInputResultByDay.ShowStatusBar = false;
            this.grdInputResultByDay.Size = new System.Drawing.Size(750, 455);
            this.grdInputResultByDay.TabIndex = 1;
            this.grdInputResultByDay.UseAutoBestFitColumns = false;
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
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedTabPage = this.tbProduct;
            this.tabMain.Size = new System.Drawing.Size(756, 484);
            this.tabMain.TabIndex = 6;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tbInputResult,
            this.tbProduct,
            this.tbLot,
            this.tbPivot});
            // 
            // tbInputResult
            // 
            this.tbInputResult.Controls.Add(this.grdInputResultByDay);
            this.tabMain.SetLanguageKey(this.tbInputResult, "INPUTRESULT");
            this.tbInputResult.Name = "tbInputResult";
            this.tbInputResult.Size = new System.Drawing.Size(750, 455);
            this.tbInputResult.Text = "xtraTabPage1";
            // 
            // tbProduct
            // 
            this.tbProduct.Controls.Add(this.grdProduct);
            this.tbProduct.Name = "tbProduct";
            this.tbProduct.Size = new System.Drawing.Size(750, 455);
            this.tbProduct.Text = "품목별 투입실적";
            // 
            // grdProduct
            // 
            this.grdProduct.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProduct.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.Export;
            this.grdProduct.IsUsePaging = false;
            this.grdProduct.LanguageKey = "INPUTRESULT";
            this.grdProduct.Location = new System.Drawing.Point(0, 0);
            this.grdProduct.Margin = new System.Windows.Forms.Padding(0);
            this.grdProduct.Name = "grdProduct";
            this.grdProduct.ShowBorder = true;
            this.grdProduct.ShowStatusBar = false;
            this.grdProduct.Size = new System.Drawing.Size(750, 455);
            this.grdProduct.TabIndex = 2;
            this.grdProduct.UseAutoBestFitColumns = false;
            // 
            // tbPivot
            // 
            this.tbPivot.Controls.Add(this.gbPivot);
            this.tbPivot.Name = "tbPivot";
            this.tbPivot.Size = new System.Drawing.Size(750, 455);
            this.tbPivot.Text = "PIVOT";
            // 
            // gbPivot
            // 
            this.gbPivot.Controls.Add(this.pvtInputResult);
            this.gbPivot.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.gbPivot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPivot.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.Export;
            this.gbPivot.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.gbPivot.Location = new System.Drawing.Point(0, 0);
            this.gbPivot.Name = "gbPivot";
            this.gbPivot.ShowBorder = true;
            this.gbPivot.Size = new System.Drawing.Size(750, 455);
            this.gbPivot.TabIndex = 1;
            // 
            // pvtInputResult
            // 
            this.pvtInputResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pvtInputResult.GrandTotalCaptionText = null;
            this.pvtInputResult.Location = new System.Drawing.Point(2, 31);
            this.pvtInputResult.Name = "pvtInputResult";
            this.pvtInputResult.OptionsView.ShowGrandTotalsForSingleValues = true;
            this.pvtInputResult.OptionsView.ShowTotalsForSingleValues = true;
            this.pvtInputResult.Size = new System.Drawing.Size(746, 422);
            this.pvtInputResult.TabIndex = 0;
            this.pvtInputResult.TotalFieldNames = null;
            this.pvtInputResult.UseCheckBoxField = false;
            this.pvtInputResult.UseGrandTotalCaption = false;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "copy_16x16.png");
            // 
            // tbLot
            // 
            this.tbLot.Controls.Add(this.grdLot);
            this.tbLot.Name = "tbLot";
            this.tbLot.Size = new System.Drawing.Size(750, 455);
            this.tbLot.Text = "LOT별 투입실적";
            // 
            // grdLot
            // 
            this.grdLot.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLot.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLot.IsUsePaging = false;
            this.grdLot.LanguageKey = "LOTLIST";
            this.grdLot.Location = new System.Drawing.Point(0, 0);
            this.grdLot.Margin = new System.Windows.Forms.Padding(0);
            this.grdLot.Name = "grdLot";
            this.grdLot.ShowBorder = true;
            this.grdLot.Size = new System.Drawing.Size(750, 455);
            this.grdLot.TabIndex = 0;
            this.grdLot.UseAutoBestFitColumns = false;
            // 
            // InputLotRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 538);
            this.Name = "InputLotRecord";
            this.Text = "LotLocking";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tbInputResult.ResumeLayout(false);
            this.tbProduct.ResumeLayout(false);
            this.tbPivot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbPivot)).EndInit();
            this.gbPivot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pvtInputResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.tbLot.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdInputResultByDay;
        private Framework.SmartControls.SmartSpliterControl spcSpliter;
        private DevExpress.Utils.ToolTipController toolTipWipInfo;
        private Framework.SmartControls.SmartTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage tbInputResult;
        private DevExpress.XtraTab.XtraTabPage tbPivot;
        private Framework.SmartControls.SmartPivotGridControl pvtInputResult;
        private Framework.SmartControls.SmartGroupBox gbPivot;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraTab.XtraTabPage tbProduct;
        private Framework.SmartControls.SmartBandedGrid grdProduct;
        private DevExpress.XtraTab.XtraTabPage tbLot;
        private Framework.SmartControls.SmartBandedGrid grdLot;
    }
}