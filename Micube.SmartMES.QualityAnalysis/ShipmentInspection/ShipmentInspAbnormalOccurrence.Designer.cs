namespace Micube.SmartMES.QualityAnalysis
{
    partial class ShipmentInspAbnormalOccurrence
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
            this.tabAbnormal = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgOutflow = new DevExpress.XtraTab.XtraTabPage();
            this.grdOutFlow = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgCause = new DevExpress.XtraTab.XtraTabPage();
            this.grdCause = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.btnPopupFlag = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabAbnormal)).BeginInit();
            this.tabAbnormal.SuspendLayout();
            this.tpgOutflow.SuspendLayout();
            this.tpgCause.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 397);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnPopupFlag);
            this.pnlToolbar.Controls.SetChildIndex(this.btnPopupFlag, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabAbnormal);
            // 
            // tabAbnormal
            // 
            this.tabAbnormal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabAbnormal.Location = new System.Drawing.Point(0, 0);
            this.tabAbnormal.Margin = new System.Windows.Forms.Padding(0);
            this.tabAbnormal.Name = "tabAbnormal";
            this.tabAbnormal.SelectedTabPage = this.tpgOutflow;
            this.tabAbnormal.Size = new System.Drawing.Size(475, 401);
            this.tabAbnormal.TabIndex = 0;
            this.tabAbnormal.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgOutflow,
            this.tpgCause});
            // 
            // tpgOutflow
            // 
            this.tpgOutflow.Controls.Add(this.grdOutFlow);
            this.tabAbnormal.SetLanguageKey(this.tpgOutflow, "OUTFLOWPROCESS");
            this.tpgOutflow.Name = "tpgOutflow";
            this.tpgOutflow.Size = new System.Drawing.Size(469, 372);
            this.tpgOutflow.Text = "xtraTabPage1";
            // 
            // grdOutFlow
            // 
            this.grdOutFlow.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdOutFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOutFlow.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdOutFlow.IsUsePaging = false;
            this.grdOutFlow.LanguageKey = "OUTFLOWPROCESS";
            this.grdOutFlow.Location = new System.Drawing.Point(0, 0);
            this.grdOutFlow.Margin = new System.Windows.Forms.Padding(0);
            this.grdOutFlow.Name = "grdOutFlow";
            this.grdOutFlow.ShowBorder = true;
            this.grdOutFlow.ShowStatusBar = false;
            this.grdOutFlow.Size = new System.Drawing.Size(469, 372);
            this.grdOutFlow.TabIndex = 2;
            this.grdOutFlow.UseAutoBestFitColumns = false;
            // 
            // tpgCause
            // 
            this.tpgCause.Controls.Add(this.grdCause);
            this.tabAbnormal.SetLanguageKey(this.tpgCause, "CAUSEPROCESS");
            this.tpgCause.Name = "tpgCause";
            this.tpgCause.Size = new System.Drawing.Size(469, 372);
            this.tpgCause.Text = "xtraTabPage2";
            // 
            // grdCause
            // 
            this.grdCause.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdCause.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCause.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdCause.IsUsePaging = false;
            this.grdCause.LanguageKey = "CAUSEPROCESS";
            this.grdCause.Location = new System.Drawing.Point(0, 0);
            this.grdCause.Margin = new System.Windows.Forms.Padding(0);
            this.grdCause.Name = "grdCause";
            this.grdCause.ShowBorder = true;
            this.grdCause.Size = new System.Drawing.Size(469, 372);
            this.grdCause.TabIndex = 0;
            this.grdCause.UseAutoBestFitColumns = false;
            // 
            // btnPopupFlag
            // 
            this.btnPopupFlag.AllowFocus = false;
            this.btnPopupFlag.IsBusy = false;
            this.btnPopupFlag.IsWrite = true;
            this.btnPopupFlag.Location = new System.Drawing.Point(395, -2);
            this.btnPopupFlag.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPopupFlag.Name = "btnPopupFlag";
            this.btnPopupFlag.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPopupFlag.Size = new System.Drawing.Size(80, 25);
            this.btnPopupFlag.TabIndex = 10;
            this.btnPopupFlag.Text = "smartButton1";
            this.btnPopupFlag.TooltipLanguageKey = "";
            this.btnPopupFlag.Visible = false;
            // 
            // ShipmentInspAbnormalOccurrence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "ShipmentInspAbnormalOccurrence";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabAbnormal)).EndInit();
            this.tabAbnormal.ResumeLayout(false);
            this.tpgOutflow.ResumeLayout(false);
            this.tpgCause.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabAbnormal;
        private DevExpress.XtraTab.XtraTabPage tpgOutflow;
        private DevExpress.XtraTab.XtraTabPage tpgCause;
        private Framework.SmartControls.SmartBandedGrid grdOutFlow;
        private Framework.SmartControls.SmartBandedGrid grdCause;
        private Framework.SmartControls.SmartButton btnPopupFlag;
    }
}