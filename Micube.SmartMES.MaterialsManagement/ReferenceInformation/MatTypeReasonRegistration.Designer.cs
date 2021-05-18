namespace Micube.SmartMES.MaterialsManagement
{
    partial class MatTypeReasonRegistration
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
            this.tapTransactioncode = new DevExpress.XtraTab.XtraTabPage();
            this.grdMatType = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tapTransactionreasoncode = new DevExpress.XtraTab.XtraTabPage();
            this.grdReason = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tapCostcentercode = new DevExpress.XtraTab.XtraTabPage();
            this.grdCostcentercode = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tapTransactioncode.SuspendLayout();
            this.tapTransactionreasoncode.SuspendLayout();
            this.tapCostcentercode.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Size = new System.Drawing.Size(371, 908);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(845, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabMain);
            this.pnlContent.Size = new System.Drawing.Size(845, 911);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1226, 947);
            // 
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedTabPage = this.tapTransactioncode;
            this.tabMain.Size = new System.Drawing.Size(845, 911);
            this.tabMain.TabIndex = 0;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tapTransactioncode,
            this.tapTransactionreasoncode,
            this.tapCostcentercode});
            // 
            // tapTransactioncode
            // 
            this.tapTransactioncode.Controls.Add(this.grdMatType);
            this.tabMain.SetLanguageKey(this.tapTransactioncode, "TRANSACTIONCODE");
            this.tapTransactioncode.Name = "tapTransactioncode";
            this.tapTransactioncode.Size = new System.Drawing.Size(838, 875);
            this.tapTransactioncode.Text = "거래유형코드";
            // 
            // grdMatType
            // 
            this.grdMatType.Caption = "수입검사의뢰 내역";
            this.grdMatType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMatType.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdMatType.IsUsePaging = false;
            this.grdMatType.LanguageKey = "MATTRANSACTIONCODELIST";
            this.grdMatType.Location = new System.Drawing.Point(0, 0);
            this.grdMatType.Margin = new System.Windows.Forms.Padding(0);
            this.grdMatType.Name = "grdMatType";
            this.grdMatType.ShowBorder = true;
            this.grdMatType.ShowStatusBar = false;
            this.grdMatType.Size = new System.Drawing.Size(838, 875);
            this.grdMatType.TabIndex = 4;
            // 
            // tapTransactionreasoncode
            // 
            this.tapTransactionreasoncode.Controls.Add(this.grdReason);
            this.tabMain.SetLanguageKey(this.tapTransactionreasoncode, "TRANSACTIONREASONCODE");
            this.tapTransactionreasoncode.Name = "tapTransactionreasoncode";
            this.tapTransactionreasoncode.Size = new System.Drawing.Size(838, 875);
            this.tapTransactionreasoncode.Text = "거래사유코드";
            // 
            // grdReason
            // 
            this.grdReason.Caption = "수입검사의뢰 내역";
            this.grdReason.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReason.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdReason.IsUsePaging = false;
            this.grdReason.LanguageKey = "MATTRANSACTIONREASONCODELIST";
            this.grdReason.Location = new System.Drawing.Point(0, 0);
            this.grdReason.Margin = new System.Windows.Forms.Padding(0);
            this.grdReason.Name = "grdReason";
            this.grdReason.ShowBorder = true;
            this.grdReason.ShowStatusBar = false;
            this.grdReason.Size = new System.Drawing.Size(838, 875);
            this.grdReason.TabIndex = 4;
            // 
            // tapCostcentercode
            // 
            this.tapCostcentercode.Controls.Add(this.grdCostcentercode);
            this.tabMain.SetLanguageKey(this.tapCostcentercode, "COSTCENTERCODELIST");
            this.tapCostcentercode.Name = "tapCostcentercode";
            this.tapCostcentercode.Size = new System.Drawing.Size(838, 875);
            this.tapCostcentercode.Text = "xtraTabPage1";
            // 
            // grdCostcentercode
            // 
            this.grdCostcentercode.Caption = "";
            this.grdCostcentercode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCostcentercode.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdCostcentercode.IsUsePaging = false;
            this.grdCostcentercode.LanguageKey = "GRPCOSTCENTERCODELIST";
            this.grdCostcentercode.Location = new System.Drawing.Point(0, 0);
            this.grdCostcentercode.Margin = new System.Windows.Forms.Padding(0);
            this.grdCostcentercode.Name = "grdCostcentercode";
            this.grdCostcentercode.ShowBorder = true;
            this.grdCostcentercode.ShowStatusBar = false;
            this.grdCostcentercode.Size = new System.Drawing.Size(838, 875);
            this.grdCostcentercode.TabIndex = 5;
            // 
            // MatTypeReasonRegistration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 985);
            this.Name = "MatTypeReasonRegistration";
            this.Padding = new System.Windows.Forms.Padding(19, 19, 19, 19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tapTransactioncode.ResumeLayout(false);
            this.tapTransactionreasoncode.ResumeLayout(false);
            this.tapCostcentercode.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage tapTransactioncode;
        private DevExpress.XtraTab.XtraTabPage tapTransactionreasoncode;
        private Framework.SmartControls.SmartBandedGrid grdMatType;
        private Framework.SmartControls.SmartBandedGrid grdReason;
        private DevExpress.XtraTab.XtraTabPage tapCostcentercode;
        private Framework.SmartControls.SmartBandedGrid grdCostcentercode;
    }
}