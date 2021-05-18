namespace Micube.SmartMES.ProcessManagement
{
    partial class CaculationInputQty
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
            this.tabMain = new Micube.Framework.SmartControls.SmartTabControl();
            this.xtraTabPageInputStandby = new DevExpress.XtraTab.XtraTabPage();
            this.grdProductList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.xtraTabPageInputResult = new DevExpress.XtraTab.XtraTabPage();
            this.grdCalList = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.xtraTabPageInputStandby.SuspendLayout();
            this.xtraTabPageInputResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 467);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(738, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabMain);
            this.pnlContent.Size = new System.Drawing.Size(738, 471);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1043, 500);
            // 
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedTabPage = this.xtraTabPageInputStandby;
            this.tabMain.Size = new System.Drawing.Size(738, 471);
            this.tabMain.TabIndex = 6;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageInputStandby,
            this.xtraTabPageInputResult});
            // 
            // xtraTabPageInputStandby
            // 
            this.xtraTabPageInputStandby.Controls.Add(this.grdProductList);
            this.tabMain.SetLanguageKey(this.xtraTabPageInputStandby, "CACULATIONINPUT");
            this.xtraTabPageInputStandby.Name = "xtraTabPageInputStandby";
            this.xtraTabPageInputStandby.Size = new System.Drawing.Size(732, 442);
            this.xtraTabPageInputStandby.Text = "투입량 산출";
            // 
            // grdProductList
            // 
            this.grdProductList.Caption = "";
            this.grdProductList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProductList.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.Export;
            this.grdProductList.IsUsePaging = false;
            this.grdProductList.LanguageKey = "CACULATIONINPUT";
            this.grdProductList.Location = new System.Drawing.Point(0, 0);
            this.grdProductList.Margin = new System.Windows.Forms.Padding(0);
            this.grdProductList.Name = "grdProductList";
            this.grdProductList.ShowBorder = true;
            this.grdProductList.Size = new System.Drawing.Size(732, 442);
            this.grdProductList.TabIndex = 2;
            this.grdProductList.UseAutoBestFitColumns = false;
            // 
            // xtraTabPageInputResult
            // 
            this.xtraTabPageInputResult.Controls.Add(this.grdCalList);
            this.tabMain.SetLanguageKey(this.xtraTabPageInputResult, "CACULATIONINPUTLIST");
            this.xtraTabPageInputResult.Name = "xtraTabPageInputResult";
            this.xtraTabPageInputResult.Size = new System.Drawing.Size(732, 442);
            this.xtraTabPageInputResult.Text = "투입량 산출 리스트";
            // 
            // grdCalList
            // 
            this.grdCalList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdCalList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCalList.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.Export;
            this.grdCalList.IsUsePaging = false;
            this.grdCalList.LanguageKey = "CACULATIONINPUTLIST";
            this.grdCalList.Location = new System.Drawing.Point(0, 0);
            this.grdCalList.Margin = new System.Windows.Forms.Padding(0);
            this.grdCalList.Name = "grdCalList";
            this.grdCalList.ShowBorder = true;
            this.grdCalList.ShowStatusBar = false;
            this.grdCalList.Size = new System.Drawing.Size(732, 442);
            this.grdCalList.TabIndex = 8;
            this.grdCalList.UseAutoBestFitColumns = false;
            // 
            // CaculationInputQty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 538);
            this.LanguageKey = "PRINTLABEL";
            this.Name = "CaculationInputQty";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "포장라벨 출력";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.xtraTabPageInputStandby.ResumeLayout(false);
            this.xtraTabPageInputResult.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageInputStandby;
        private Framework.SmartControls.SmartBandedGrid grdProductList;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageInputResult;
        private Framework.SmartControls.SmartBandedGrid grdCalList;
    }
}