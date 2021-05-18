namespace Micube.SmartMES.QualityAnalysis
{
    partial class StatusByTypePopup
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
            this.grdStatusByType = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.grdStatusByType);
            this.pnlMain.Size = new System.Drawing.Size(828, 348);
            // 
            // grdStatusByType
            // 
            this.grdStatusByType.Caption = "";
            this.grdStatusByType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdStatusByType.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdStatusByType.IsUsePaging = false;
            this.grdStatusByType.LanguageKey = null;
            this.grdStatusByType.Location = new System.Drawing.Point(0, 0);
            this.grdStatusByType.Margin = new System.Windows.Forms.Padding(0);
            this.grdStatusByType.Name = "grdStatusByType";
            this.grdStatusByType.ShowBorder = true;
            this.grdStatusByType.ShowButtonBar = false;
            this.grdStatusByType.Size = new System.Drawing.Size(828, 348);
            this.grdStatusByType.TabIndex = 0;
            this.grdStatusByType.UseAutoBestFitColumns = false;
            // 
            // StatusByTypePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 368);
            this.LanguageKey = "STATUSBYTYPE";
            this.Name = "StatusByTypePopup";
            this.Text = "타입별 현황";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdStatusByType;
    }
}