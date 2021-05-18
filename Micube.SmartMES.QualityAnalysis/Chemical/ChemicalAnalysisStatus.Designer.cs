namespace Micube.SmartMES.QualityAnalysis
{
    partial class ChemicalAnalysisStatus
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
            this.grdChemicalAnalysisStatus = new Micube.Framework.SmartControls.SmartBandedGrid();
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
            this.pnlContent.Controls.Add(this.grdChemicalAnalysisStatus);
            // 
            // grdChemicalAnalysisStatus
            // 
            this.grdChemicalAnalysisStatus.Caption = "약품분석현황";
            this.grdChemicalAnalysisStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdChemicalAnalysisStatus.IsUsePaging = false;
            this.grdChemicalAnalysisStatus.LanguageKey = "CHEMICALANALYSISSTATUSLIST";
            this.grdChemicalAnalysisStatus.Location = new System.Drawing.Point(0, 0);
            this.grdChemicalAnalysisStatus.Margin = new System.Windows.Forms.Padding(0);
            this.grdChemicalAnalysisStatus.Name = "grdChemicalAnalysisStatus";
            this.grdChemicalAnalysisStatus.ShowBorder = true;
            this.grdChemicalAnalysisStatus.Size = new System.Drawing.Size(475, 401);
            this.grdChemicalAnalysisStatus.TabIndex = 0;
            // 
            // ChemicalAnalysisStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "ChemicalAnalysisStatus";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdChemicalAnalysisStatus;
    }
}