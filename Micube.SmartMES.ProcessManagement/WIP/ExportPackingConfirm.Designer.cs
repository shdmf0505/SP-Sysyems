namespace Micube.SmartMES.ProcessManagement
{
    partial class ExportPackingConfirm
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
            this.grdPackingConfirm = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 379);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(457, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdPackingConfirm);
            this.pnlContent.Size = new System.Drawing.Size(457, 383);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(762, 412);
            // 
            // grdPackingConfirm
            // 
            this.grdPackingConfirm.Caption = "";
            this.grdPackingConfirm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPackingConfirm.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.Export;
            this.grdPackingConfirm.IsUsePaging = false;
            this.grdPackingConfirm.LanguageKey = "";
            this.grdPackingConfirm.Location = new System.Drawing.Point(0, 0);
            this.grdPackingConfirm.Margin = new System.Windows.Forms.Padding(0);
            this.grdPackingConfirm.Name = "grdPackingConfirm";
            this.grdPackingConfirm.ShowBorder = true;
            this.grdPackingConfirm.Size = new System.Drawing.Size(457, 383);
            this.grdPackingConfirm.TabIndex = 2;
            this.grdPackingConfirm.UseAutoBestFitColumns = false;
            // 
            // ExportPackingConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "ExportPackingConfirm";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdPackingConfirm;
    }
}