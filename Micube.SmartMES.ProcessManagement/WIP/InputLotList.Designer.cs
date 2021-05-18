namespace Micube.SmartMES.ProcessManagement
{
    partial class InputLotList
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
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdInputList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.chkPrintLotCard = new Micube.Framework.SmartControls.SmartCheckBox();
            this.xtraTabPageInputResult = new DevExpress.XtraTab.XtraTabPage();
            this.smartSpliterContainer2 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdInputResult = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdLotListResult = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.lblComment = new Micube.Framework.SmartControls.SmartLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.xtraTabPageInputStandby.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkPrintLotCard.Properties)).BeginInit();
            this.xtraTabPageInputResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).BeginInit();
            this.smartSpliterContainer2.SuspendLayout();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 517);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.smartSplitTableLayoutPanel2);
            this.pnlToolbar.Size = new System.Drawing.Size(873, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.smartSplitTableLayoutPanel2, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabMain);
            this.pnlContent.Size = new System.Drawing.Size(873, 521);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1178, 550);
            // 
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedTabPage = this.xtraTabPageInputStandby;
            this.tabMain.Size = new System.Drawing.Size(873, 521);
            this.tabMain.TabIndex = 0;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageInputStandby,
            this.xtraTabPageInputResult});
            // 
            // xtraTabPageInputStandby
            // 
            this.xtraTabPageInputStandby.Controls.Add(this.smartSpliterContainer1);
            this.tabMain.SetLanguageKey(this.xtraTabPageInputStandby, "INPUTREADY");
            this.xtraTabPageInputStandby.Name = "xtraTabPageInputStandby";
            this.xtraTabPageInputStandby.Size = new System.Drawing.Size(867, 492);
            this.xtraTabPageInputStandby.Text = "xtraTabPage1";
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Horizontal = false;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdInputList);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(867, 492);
            this.smartSpliterContainer1.SplitterPosition = 238;
            this.smartSpliterContainer1.TabIndex = 1;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdInputList
            // 
            this.grdInputList.Caption = "";
            this.grdInputList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInputList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInputList.IsUsePaging = false;
            this.grdInputList.LanguageKey = "INPUTREADY";
            this.grdInputList.Location = new System.Drawing.Point(0, 0);
            this.grdInputList.Margin = new System.Windows.Forms.Padding(0);
            this.grdInputList.Name = "grdInputList";
            this.grdInputList.ShowBorder = true;
            this.grdInputList.ShowButtonBar = false;
            this.grdInputList.ShowStatusBar = false;
            this.grdInputList.Size = new System.Drawing.Size(867, 238);
            this.grdInputList.TabIndex = 0;
            this.grdInputList.UseAutoBestFitColumns = false;
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 3;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 142F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdLotList, 0, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.chkPrintLotCard, 1, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 3;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(867, 249);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // grdLotList
            // 
            this.grdLotList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.smartSplitTableLayoutPanel1.SetColumnSpan(this.grdLotList, 3);
            this.grdLotList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLotList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLotList.IsUsePaging = false;
            this.grdLotList.LanguageKey = "GRIDLOTLIST";
            this.grdLotList.Location = new System.Drawing.Point(0, 47);
            this.grdLotList.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotList.Name = "grdLotList";
            this.grdLotList.ShowBorder = true;
            this.grdLotList.ShowStatusBar = false;
            this.grdLotList.Size = new System.Drawing.Size(867, 202);
            this.grdLotList.TabIndex = 1;
            this.grdLotList.UseAutoBestFitColumns = false;
            // 
            // chkPrintLotCard
            // 
            this.chkPrintLotCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkPrintLotCard.LanguageKey = "ISPRINTLOTCARD";
            this.chkPrintLotCard.Location = new System.Drawing.Point(8, 3);
            this.chkPrintLotCard.Name = "chkPrintLotCard";
            this.chkPrintLotCard.Properties.Caption = "smartCheckBox1";
            this.chkPrintLotCard.Size = new System.Drawing.Size(136, 33);
            this.chkPrintLotCard.TabIndex = 2;
            // 
            // xtraTabPageInputResult
            // 
            this.xtraTabPageInputResult.Controls.Add(this.smartSpliterContainer2);
            this.tabMain.SetLanguageKey(this.xtraTabPageInputResult, "INPUTRESULT");
            this.xtraTabPageInputResult.Name = "xtraTabPageInputResult";
            this.xtraTabPageInputResult.Size = new System.Drawing.Size(867, 492);
            this.xtraTabPageInputResult.Text = "xtraTabPage2";
            // 
            // smartSpliterContainer2
            // 
            this.smartSpliterContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer2.Horizontal = false;
            this.smartSpliterContainer2.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer2.Name = "smartSpliterContainer2";
            this.smartSpliterContainer2.Panel1.Controls.Add(this.grdInputResult);
            this.smartSpliterContainer2.Panel1.Text = "Panel1";
            this.smartSpliterContainer2.Panel2.Controls.Add(this.grdLotListResult);
            this.smartSpliterContainer2.Panel2.Text = "Panel2";
            this.smartSpliterContainer2.Size = new System.Drawing.Size(867, 492);
            this.smartSpliterContainer2.SplitterPosition = 244;
            this.smartSpliterContainer2.TabIndex = 0;
            this.smartSpliterContainer2.Text = "smartSpliterContainer2";
            // 
            // grdInputResult
            // 
            this.grdInputResult.Caption = "";
            this.grdInputResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInputResult.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInputResult.IsUsePaging = false;
            this.grdInputResult.LanguageKey = "INPUTRESULT";
            this.grdInputResult.Location = new System.Drawing.Point(0, 0);
            this.grdInputResult.Margin = new System.Windows.Forms.Padding(0);
            this.grdInputResult.Name = "grdInputResult";
            this.grdInputResult.ShowBorder = true;
            this.grdInputResult.ShowStatusBar = false;
            this.grdInputResult.Size = new System.Drawing.Size(867, 244);
            this.grdInputResult.TabIndex = 4;
            this.grdInputResult.UseAutoBestFitColumns = false;
            // 
            // grdLotListResult
            // 
            this.grdLotListResult.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLotListResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLotListResult.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLotListResult.IsUsePaging = false;
            this.grdLotListResult.LanguageKey = "GRIDLOTLIST";
            this.grdLotListResult.Location = new System.Drawing.Point(0, 0);
            this.grdLotListResult.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotListResult.Name = "grdLotListResult";
            this.grdLotListResult.ShowBorder = true;
            this.grdLotListResult.ShowStatusBar = false;
            this.grdLotListResult.Size = new System.Drawing.Size(867, 243);
            this.grdLotListResult.TabIndex = 0;
            this.grdLotListResult.UseAutoBestFitColumns = false;
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.ColumnCount = 3;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.lblComment, 2, 0);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(47, 0);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 1;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(826, 24);
            this.smartSplitTableLayoutPanel2.TabIndex = 6;
            // 
            // lblComment
            // 
            this.lblComment.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblComment.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblComment.Appearance.Options.UseFont = true;
            this.lblComment.Appearance.Options.UseForeColor = true;
            this.lblComment.Appearance.Options.UseTextOptions = true;
            this.lblComment.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblComment.Location = new System.Drawing.Point(529, 3);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(294, 18);
            this.lblComment.TabIndex = 0;
            // 
            // InputLotList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 588);
            this.Name = "InputLotList";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.xtraTabPageInputStandby.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkPrintLotCard.Properties)).EndInit();
            this.xtraTabPageInputResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).EndInit();
            this.smartSpliterContainer2.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageInputStandby;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageInputResult;
        private Framework.SmartControls.SmartBandedGrid grdInputList;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdLotList;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer2;
        private Framework.SmartControls.SmartBandedGrid grdInputResult;
        private Framework.SmartControls.SmartBandedGrid grdLotListResult;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartCheckBox chkPrintLotCard;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
        private Framework.SmartControls.SmartLabel lblComment;
    }
}