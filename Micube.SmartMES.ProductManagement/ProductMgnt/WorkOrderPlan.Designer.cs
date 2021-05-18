namespace Micube.SmartMES.ProductManagement
{
    partial class WorkOrderPlan
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
            this.components = new System.ComponentModel.Container();
            this.grdFcst = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.spcSpliter = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.toolTipWipInfo = new DevExpress.Utils.ToolTipController(this.components);
            this.tabMain = new Micube.Framework.SmartControls.SmartTabControl();
            this.tbProductPlan = new DevExpress.XtraTab.XtraTabPage();
            this.tbPivot = new DevExpress.XtraTab.XtraTabPage();
            this.gbPivot = new Micube.Framework.SmartControls.SmartGroupBox();
            this.pvProductPlan = new Micube.Framework.SmartControls.SmartPivotGridControl();
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tbProductPlan.SuspendLayout();
            this.tbPivot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbPivot)).BeginInit();
            this.gbPivot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pvProductPlan)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.smartSplitTableLayoutPanel2);
            this.pnlToolbar.Controls.SetChildIndex(this.smartSplitTableLayoutPanel2, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabMain);
            this.pnlContent.Controls.Add(this.spcSpliter);
            // 
            // grdFcst
            // 
            this.grdFcst.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdFcst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFcst.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdFcst.IsUsePaging = false;
            this.grdFcst.LanguageKey = "INPUTRESULT";
            this.grdFcst.Location = new System.Drawing.Point(0, 0);
            this.grdFcst.Margin = new System.Windows.Forms.Padding(0);
            this.grdFcst.Name = "grdFcst";
            this.grdFcst.ShowBorder = true;
            this.grdFcst.ShowStatusBar = false;
            this.grdFcst.Size = new System.Drawing.Size(750, 455);
            this.grdFcst.TabIndex = 1;
            this.grdFcst.UseAutoBestFitColumns = false;
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
            this.tabMain.SelectedTabPage = this.tbProductPlan;
            this.tabMain.Size = new System.Drawing.Size(756, 484);
            this.tabMain.TabIndex = 6;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tbProductPlan,
            this.tbPivot});
            // 
            // tbProductPlan
            // 
            this.tbProductPlan.Controls.Add(this.grdFcst);
            this.tabMain.SetLanguageKey(this.tbProductPlan, "PRODUCTPLAN");
            this.tbProductPlan.Name = "tbProductPlan";
            this.tbProductPlan.Size = new System.Drawing.Size(750, 455);
            this.tbProductPlan.Text = "xtraTabPage1";
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
            this.gbPivot.Controls.Add(this.pvProductPlan);
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
            // pvProductPlan
            // 
            this.pvProductPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pvProductPlan.GrandTotalCaptionText = null;
            this.pvProductPlan.Location = new System.Drawing.Point(2, 31);
            this.pvProductPlan.Name = "pvProductPlan";
            this.pvProductPlan.OptionsView.ShowGrandTotalsForSingleValues = true;
            this.pvProductPlan.OptionsView.ShowTotalsForSingleValues = true;
            this.pvProductPlan.Size = new System.Drawing.Size(746, 422);
            this.pvProductPlan.TabIndex = 0;
            this.pvProductPlan.TotalFieldNames = null;
            this.pvProductPlan.UseCheckBoxField = false;
            this.pvProductPlan.UseGrandTotalCaption = false;
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.ColumnCount = 1;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(47, 0);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 1;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(709, 24);
            this.smartSplitTableLayoutPanel2.TabIndex = 7;
            // 
            // WorkOrderPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 538);
            this.Name = "WorkOrderPlan";
            this.Text = "LotLocking";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tbProductPlan.ResumeLayout(false);
            this.tbPivot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbPivot)).EndInit();
            this.gbPivot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pvProductPlan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdFcst;
        private Framework.SmartControls.SmartSpliterControl spcSpliter;
        private DevExpress.Utils.ToolTipController toolTipWipInfo;
        private Framework.SmartControls.SmartTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage tbProductPlan;
        private DevExpress.XtraTab.XtraTabPage tbPivot;
        private Framework.SmartControls.SmartPivotGridControl pvProductPlan;
        private Framework.SmartControls.SmartGroupBox gbPivot;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
    }
}