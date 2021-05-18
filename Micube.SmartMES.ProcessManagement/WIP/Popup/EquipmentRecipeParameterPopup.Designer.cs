namespace Micube.SmartMES.ProcessManagement
{
    partial class EquipmentRecipeParameterPopup
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
            this.grdEquipmentRecipe = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.grdEquipmentRecipe);
            this.pnlMain.Size = new System.Drawing.Size(1340, 750);
            // 
            // grdEquipmentRecipe
            // 
            this.grdEquipmentRecipe.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdEquipmentRecipe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdEquipmentRecipe.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdEquipmentRecipe.IsUsePaging = false;
            this.grdEquipmentRecipe.LanguageKey = null;
            this.grdEquipmentRecipe.Location = new System.Drawing.Point(0, 0);
            this.grdEquipmentRecipe.Margin = new System.Windows.Forms.Padding(0);
            this.grdEquipmentRecipe.Name = "grdEquipmentRecipe";
            this.grdEquipmentRecipe.ShowBorder = true;
            this.grdEquipmentRecipe.Size = new System.Drawing.Size(1340, 750);
            this.grdEquipmentRecipe.TabIndex = 0;
            // 
            // EquipmentRecipeParameterPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1360, 770);
            this.LanguageKey = "EQUIPMENTRECIPEPARAMETER";
            this.Name = "EquipmentRecipeParameterPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "설비 Recipe 파라미터";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdEquipmentRecipe;
    }
}