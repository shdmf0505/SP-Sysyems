namespace Micube.SmartMES.QualityAnalysis
{
    partial class StatusByItemPopup
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
            this.grdStatusByItem = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.grdStatusByItem);
            this.pnlMain.Size = new System.Drawing.Size(817, 346);
            // 
            // grdStatusByItem
            // 
            this.grdStatusByItem.Caption = "";
            this.grdStatusByItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdStatusByItem.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdStatusByItem.IsUsePaging = false;
            this.grdStatusByItem.LanguageKey = null;
            this.grdStatusByItem.Location = new System.Drawing.Point(0, 0);
            this.grdStatusByItem.Margin = new System.Windows.Forms.Padding(0);
            this.grdStatusByItem.Name = "grdStatusByItem";
            this.grdStatusByItem.ShowBorder = true;
            this.grdStatusByItem.ShowButtonBar = false;
            this.grdStatusByItem.Size = new System.Drawing.Size(817, 346);
            this.grdStatusByItem.TabIndex = 0;
            this.grdStatusByItem.UseAutoBestFitColumns = false;
            // 
            // StatusByItemPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 366);
            this.LanguageKey = "TABCURRENTBYITEM";
            this.Name = "StatusByItemPopup";
            this.Text = "품목별 현황";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdStatusByItem;
    }
}