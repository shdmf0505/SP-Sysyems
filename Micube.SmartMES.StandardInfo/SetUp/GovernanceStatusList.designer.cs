namespace Micube.SmartMES.StandardInfo
{
    partial class GovernanceStatusList
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
            this.grdProcessdefinition = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.grdProcessPath = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdBillofMaterial = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdGovernanceStatu = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdApprovaltransaction = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.btnDetail = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Size = new System.Drawing.Size(371, 516);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnDetail);
            this.pnlToolbar.Size = new System.Drawing.Size(790, 30);
            this.pnlToolbar.Controls.SetChildIndex(this.btnDetail, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer1);
            this.pnlContent.Size = new System.Drawing.Size(790, 519);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1171, 555);
            // 
            // grdProcessdefinition
            // 
            this.grdProcessdefinition.Caption = "";
            this.grdProcessdefinition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProcessdefinition.IsUsePaging = false;
            this.grdProcessdefinition.LanguageKey = "ReworkRouting";
            this.grdProcessdefinition.Location = new System.Drawing.Point(0, 0);
            this.grdProcessdefinition.Margin = new System.Windows.Forms.Padding(0);
            this.grdProcessdefinition.Name = "grdProcessdefinition";
            this.grdProcessdefinition.ShowBorder = true;
            this.grdProcessdefinition.ShowStatusBar = false;
            this.grdProcessdefinition.Size = new System.Drawing.Size(790, 229);
            this.grdProcessdefinition.TabIndex = 1;
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.grdProcessdefinition);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.grdProcessPath);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(790, 257);
            this.splitContainerControl2.SplitterPosition = 229;
            this.splitContainerControl2.TabIndex = 1;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // grdProcessPath
            // 
            this.grdProcessPath.Caption = "";
            this.grdProcessPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProcessPath.IsUsePaging = false;
            this.grdProcessPath.LanguageKey = "OPERATION";
            this.grdProcessPath.Location = new System.Drawing.Point(0, 0);
            this.grdProcessPath.Margin = new System.Windows.Forms.Padding(0);
            this.grdProcessPath.Name = "grdProcessPath";
            this.grdProcessPath.ShowBorder = true;
            this.grdProcessPath.ShowStatusBar = false;
            this.grdProcessPath.Size = new System.Drawing.Size(790, 22);
            this.grdProcessPath.TabIndex = 1;
            // 
            // grdBillofMaterial
            // 
            this.grdBillofMaterial.Caption = "";
            this.grdBillofMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdBillofMaterial.IsUsePaging = false;
            this.grdBillofMaterial.LanguageKey = "CONSUMABLEDEF";
            this.grdBillofMaterial.Location = new System.Drawing.Point(0, 0);
            this.grdBillofMaterial.Margin = new System.Windows.Forms.Padding(0);
            this.grdBillofMaterial.Name = "grdBillofMaterial";
            this.grdBillofMaterial.ShowBorder = true;
            this.grdBillofMaterial.ShowStatusBar = false;
            this.grdBillofMaterial.Size = new System.Drawing.Size(790, 56);
            this.grdBillofMaterial.TabIndex = 2;
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Horizontal = false;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdGovernanceStatu);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdApprovaltransaction);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(790, 519);
            this.smartSpliterContainer1.SplitterPosition = 349;
            this.smartSpliterContainer1.TabIndex = 1;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdGovernanceStatu
            // 
            this.grdGovernanceStatu.Caption = "";
            this.grdGovernanceStatu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdGovernanceStatu.IsUsePaging = false;
            this.grdGovernanceStatu.LanguageKey = "Approbal";
            this.grdGovernanceStatu.Location = new System.Drawing.Point(0, 0);
            this.grdGovernanceStatu.Margin = new System.Windows.Forms.Padding(0);
            this.grdGovernanceStatu.Name = "grdGovernanceStatu";
            this.grdGovernanceStatu.ShowBorder = true;
            this.grdGovernanceStatu.Size = new System.Drawing.Size(790, 349);
            this.grdGovernanceStatu.TabIndex = 0;
            // 
            // grdApprovaltransaction
            // 
            this.grdApprovaltransaction.Caption = "";
            this.grdApprovaltransaction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdApprovaltransaction.IsUsePaging = false;
            this.grdApprovaltransaction.LanguageKey = "PROCESSSTATUS";
            this.grdApprovaltransaction.Location = new System.Drawing.Point(0, 0);
            this.grdApprovaltransaction.Margin = new System.Windows.Forms.Padding(0);
            this.grdApprovaltransaction.Name = "grdApprovaltransaction";
            this.grdApprovaltransaction.ShowBorder = true;
            this.grdApprovaltransaction.Size = new System.Drawing.Size(790, 164);
            this.grdApprovaltransaction.TabIndex = 0;
            // 
            // btnDetail
            // 
            this.btnDetail.AllowFocus = false;
            this.btnDetail.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDetail.IsBusy = false;
            this.btnDetail.Location = new System.Drawing.Point(693, 0);
            this.btnDetail.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnDetail.Size = new System.Drawing.Size(97, 30);
            this.btnDetail.TabIndex = 6;
            this.btnDetail.Text = "상세";
            this.btnDetail.TooltipLanguageKey = "";
            // 
            // GovernanceStatusList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 593);
            this.Name = "GovernanceStatusList";
            this.Padding = new System.Windows.Forms.Padding(19, 19, 19, 19);
            this.Text = "MasterDataClass";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Framework.SmartControls.SmartBandedGrid grdProcessdefinition;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private Framework.SmartControls.SmartBandedGrid grdProcessPath;
        private Framework.SmartControls.SmartBandedGrid grdBillofMaterial;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdGovernanceStatu;
        private Framework.SmartControls.SmartBandedGrid grdApprovaltransaction;
        private Framework.SmartControls.SmartButton btnDetail;
    }
}