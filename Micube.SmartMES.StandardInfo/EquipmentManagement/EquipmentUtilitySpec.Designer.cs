namespace Micube.SmartMES.StandardInfo
{
	partial class EquipmentUtilitySpec
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
            this.grdEquipmentSpec = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 527);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(649, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdEquipmentSpec);
            this.pnlContent.Size = new System.Drawing.Size(649, 531);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(954, 560);
            // 
            // grdEquipmentSpec
            // 
            this.grdEquipmentSpec.Caption = "유틸리티 사양 등록";
            this.grdEquipmentSpec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdEquipmentSpec.IsUsePaging = false;
            this.grdEquipmentSpec.LanguageKey = "EQUIPMENTUTILITYSPECLIST";
            this.grdEquipmentSpec.Location = new System.Drawing.Point(0, 0);
            this.grdEquipmentSpec.Margin = new System.Windows.Forms.Padding(0);
            this.grdEquipmentSpec.Name = "grdEquipmentUtilitySpec";
            this.grdEquipmentSpec.ShowBorder = true;
            this.grdEquipmentSpec.Size = new System.Drawing.Size(649, 531);
            this.grdEquipmentSpec.TabIndex = 1;
            // 
            // EquipmentSpec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 580);
            this.Name = "EquipmentUtilitySpec";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdEquipmentSpec;
    }
}