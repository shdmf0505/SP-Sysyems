namespace Micube.SmartMES.MaterialsManagement
{
    partial class MaterialNeedQtyInquiry
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
            this.smartSpliterContainer2 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdMaster = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdDetail = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).BeginInit();
            this.smartSpliterContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(371, 908);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(851, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer2);
            this.pnlContent.Size = new System.Drawing.Size(851, 911);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1232, 947);
            // 
            // smartSpliterContainer2
            // 
            this.smartSpliterContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer2.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.smartSpliterContainer2.Horizontal = false;
            this.smartSpliterContainer2.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer2.Name = "smartSpliterContainer2";
            this.smartSpliterContainer2.Panel1.Controls.Add(this.grdMaster);
            this.smartSpliterContainer2.Panel1.Text = "Panel1";
            this.smartSpliterContainer2.Panel2.Controls.Add(this.grdDetail);
            this.smartSpliterContainer2.Panel2.Text = "Panel2";
            this.smartSpliterContainer2.Size = new System.Drawing.Size(851, 911);
            this.smartSpliterContainer2.SplitterPosition = 363;
            this.smartSpliterContainer2.TabIndex = 4;
            this.smartSpliterContainer2.Text = "smartSpliterContainer2";
            // 
            // grdMaster
            // 
            this.grdMaster.Caption = "";
            this.grdMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMaster.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdMaster.IsUsePaging = false;
            this.grdMaster.LanguageKey = "MATERIALSTATUSSTATISTICSLIST";
            this.grdMaster.Location = new System.Drawing.Point(0, 0);
            this.grdMaster.Margin = new System.Windows.Forms.Padding(0);
            this.grdMaster.Name = "grdMaster";
            this.grdMaster.ShowBorder = true;
            this.grdMaster.ShowStatusBar = false;
            this.grdMaster.Size = new System.Drawing.Size(851, 542);
            this.grdMaster.TabIndex = 116;
            this.grdMaster.UseAutoBestFitColumns = false;
            // 
            // grdDetail
            // 
            this.grdDetail.Caption = "";
            this.grdDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDetail.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdDetail.IsUsePaging = false;
            this.grdDetail.LanguageKey = "MATERIALNEEDQTYINQUIRYLIST";
            this.grdDetail.Location = new System.Drawing.Point(0, 0);
            this.grdDetail.Margin = new System.Windows.Forms.Padding(0);
            this.grdDetail.Name = "grdDetail";
            this.grdDetail.ShowBorder = true;
            this.grdDetail.ShowStatusBar = false;
            this.grdDetail.Size = new System.Drawing.Size(851, 363);
            this.grdDetail.TabIndex = 117;
            this.grdDetail.UseAutoBestFitColumns = false;
            // 
            // MaterialNeedQtyInquiry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 977);
            this.Name = "MaterialNeedQtyInquiry";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).EndInit();
            this.smartSpliterContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer2;
        private Framework.SmartControls.SmartBandedGrid grdMaster;
        private Framework.SmartControls.SmartBandedGrid grdDetail;
    }
}