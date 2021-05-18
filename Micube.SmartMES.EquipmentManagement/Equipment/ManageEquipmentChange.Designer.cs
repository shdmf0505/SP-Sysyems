namespace Micube.SmartMES.EquipmentManagement
{
    partial class ManageEquipmentChange
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageEquipmentChange));
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdEqpHistory = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdAttachment = new Micube.SmartMES.Commons.Controls.SmartFileProcessingControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 621);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(1081, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer1);
            this.pnlContent.Size = new System.Drawing.Size(1081, 625);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1386, 654);
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.smartSpliterContainer1.Horizontal = false;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdEqpHistory);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdAttachment);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(1081, 625);
            this.smartSpliterContainer1.SplitterPosition = 249;
            this.smartSpliterContainer1.TabIndex = 0;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdEqpHistory
            // 
            this.grdEqpHistory.Caption = "설비수리접수목록:";
            this.grdEqpHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdEqpHistory.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdEqpHistory.IsUsePaging = false;
            this.grdEqpHistory.LanguageKey = "EQUIPMENTHISTORYLIST";
            this.grdEqpHistory.Location = new System.Drawing.Point(0, 0);
            this.grdEqpHistory.Margin = new System.Windows.Forms.Padding(0);
            this.grdEqpHistory.Name = "grdEqpHistory";
            this.grdEqpHistory.ShowBorder = true;
            this.grdEqpHistory.Size = new System.Drawing.Size(1081, 371);
            this.grdEqpHistory.TabIndex = 1291;
            // 
            // grdAttachment
            // 
            this.grdAttachment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAttachment.LanguageKey = "RELATIONFILE";
            this.grdAttachment.Location = new System.Drawing.Point(0, 0);
            this.grdAttachment.Margin = new System.Windows.Forms.Padding(0);
            this.grdAttachment.Name = "grdAttachment";
            this.grdAttachment.Size = new System.Drawing.Size(1081, 249);
            this.grdAttachment.TabIndex = 1298;
            this.grdAttachment.UploadPath = "";
            this.grdAttachment.UseCommentsColumn = true;
            // 
            // ManageEquipmentChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1406, 674);
            this.Name = "ManageEquipmentChange";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdEqpHistory;
        private Commons.Controls.SmartFileProcessingControl grdAttachment;
    }
}