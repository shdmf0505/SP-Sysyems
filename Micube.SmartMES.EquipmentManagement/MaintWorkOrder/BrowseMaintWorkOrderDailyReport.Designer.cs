namespace Micube.SmartMES.EquipmentManagement
{
    partial class BrowseMaintWorkOrderDailyReport
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
            this.grdMaintWorkOrderStatus = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 397);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdMaintWorkOrderStatus);
            // 
            // grdMaintWorkOrderStatus
            // 
            this.grdMaintWorkOrderStatus.Caption = "";
            this.grdMaintWorkOrderStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMaintWorkOrderStatus.IsUsePaging = false;
            this.grdMaintWorkOrderStatus.LanguageKey = "MAINTWORKORDERDAILYREPORT";
            this.grdMaintWorkOrderStatus.Location = new System.Drawing.Point(0, 0);
            this.grdMaintWorkOrderStatus.Margin = new System.Windows.Forms.Padding(0);
            this.grdMaintWorkOrderStatus.Name = "grdMaintWorkOrderStatus";
            this.grdMaintWorkOrderStatus.ShowBorder = true;
            this.grdMaintWorkOrderStatus.ShowStatusBar = false;
            this.grdMaintWorkOrderStatus.Size = new System.Drawing.Size(475, 401);
            this.grdMaintWorkOrderStatus.TabIndex = 116;
            // 
            // BrowseMaintWorkOrderDailyReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "BrowseMaintWorkOrderDailyReport";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdMaintWorkOrderStatus;
    }
}