namespace Micube.SmartMES.EquipmentManagement
{
    partial class SparePartEquipmentBrowse
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
            this.grdEquipmentSparePart = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 619);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(969, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdEquipmentSparePart);
            this.pnlContent.Size = new System.Drawing.Size(969, 623);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1274, 652);
            // 
            // grdEquipmentSparePart
            // 
            this.grdEquipmentSparePart.Caption = "EquipmentSPStatusRevision";
            this.grdEquipmentSparePart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdEquipmentSparePart.IsUsePaging = false;
            this.grdEquipmentSparePart.LanguageKey = "EquipmentSPStatusRevision";
            this.grdEquipmentSparePart.Location = new System.Drawing.Point(0, 0);
            this.grdEquipmentSparePart.Margin = new System.Windows.Forms.Padding(0);
            this.grdEquipmentSparePart.Name = "grdEquipmentSparePart";
            this.grdEquipmentSparePart.ShowBorder = true;
            this.grdEquipmentSparePart.ShowStatusBar = false;
            this.grdEquipmentSparePart.Size = new System.Drawing.Size(969, 623);
            this.grdEquipmentSparePart.TabIndex = 116;
            // 
            // SparePartEquipmentBrowse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1294, 672);
            this.Name = "SparePartEquipmentBrowse";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdEquipmentSparePart;
    }
}