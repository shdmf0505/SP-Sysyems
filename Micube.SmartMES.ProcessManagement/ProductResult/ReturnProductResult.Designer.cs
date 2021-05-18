namespace Micube.SmartMES.ProcessManagement
{
    partial class ReturnProductResult
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
            this.grdReturn = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 515);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(796, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdReturn);
            this.pnlContent.Size = new System.Drawing.Size(796, 519);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1101, 548);
            // 
            // grdReturn
            // 
            this.grdReturn.Caption = "";
            this.grdReturn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReturn.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.Export;
            this.grdReturn.IsUsePaging = false;
            this.grdReturn.LanguageKey = "  ";
            this.grdReturn.Location = new System.Drawing.Point(0, 0);
            this.grdReturn.Margin = new System.Windows.Forms.Padding(0);
            this.grdReturn.Name = "grdReturn";
            this.grdReturn.ShowBorder = true;
            this.grdReturn.ShowStatusBar = false;
            this.grdReturn.Size = new System.Drawing.Size(796, 519);
            this.grdReturn.TabIndex = 1;
            this.grdReturn.UseAutoBestFitColumns = false;
            // 
            // ReturnProductResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 568);
            this.Name = "ReturnProductResult";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdReturn;
    }
}