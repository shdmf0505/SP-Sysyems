namespace Micube.SmartMES.QualityAnalysis
{
    partial class ChemicalRegistration
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
            this.grdChemicalRegistration = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.btnSupplementConfirmation = new Micube.Framework.SmartControls.SmartButton();
            this.btnSupplementRegistartion = new Micube.Framework.SmartControls.SmartButton();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 397);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlToolbar.Size = new System.Drawing.Size(475, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.smartSplitTableLayoutPanel1, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdChemicalRegistration);
            this.pnlContent.Size = new System.Drawing.Size(475, 401);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(780, 430);
            // 
            // grdChemicalRegistration
            // 
            this.grdChemicalRegistration.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdChemicalRegistration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdChemicalRegistration.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdChemicalRegistration.IsUsePaging = false;
            this.grdChemicalRegistration.LanguageKey = "CHEMICALREGISTARTION";
            this.grdChemicalRegistration.Location = new System.Drawing.Point(0, 0);
            this.grdChemicalRegistration.Margin = new System.Windows.Forms.Padding(0);
            this.grdChemicalRegistration.Name = "grdChemicalRegistration";
            this.grdChemicalRegistration.ShowBorder = true;
            this.grdChemicalRegistration.Size = new System.Drawing.Size(475, 401);
            this.grdChemicalRegistration.TabIndex = 0;
            this.grdChemicalRegistration.UseAutoBestFitColumns = false;
            // 
            // btnSupplementConfirmation
            // 
            this.btnSupplementConfirmation.AllowFocus = false;
            this.btnSupplementConfirmation.IsBusy = false;
            this.btnSupplementConfirmation.IsWrite = false;
            this.btnSupplementConfirmation.LanguageKey = "SUPPLEMENTCONFIRMATION";
            this.btnSupplementConfirmation.Location = new System.Drawing.Point(329, 0);
            this.btnSupplementConfirmation.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSupplementConfirmation.Name = "btnSupplementConfirmation";
            this.btnSupplementConfirmation.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSupplementConfirmation.Size = new System.Drawing.Size(96, 23);
            this.btnSupplementConfirmation.TabIndex = 5;
            this.btnSupplementConfirmation.Text = "보충량 확정";
            this.btnSupplementConfirmation.TooltipLanguageKey = "";
            this.btnSupplementConfirmation.Visible = false;
            // 
            // btnSupplementRegistartion
            // 
            this.btnSupplementRegistartion.AllowFocus = false;
            this.btnSupplementRegistartion.IsBusy = false;
            this.btnSupplementRegistartion.IsWrite = false;
            this.btnSupplementRegistartion.LanguageKey = "SUPPLEMENTREGISTARTION";
            this.btnSupplementRegistartion.Location = new System.Drawing.Point(228, 0);
            this.btnSupplementRegistartion.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSupplementRegistartion.Name = "btnSupplementRegistartion";
            this.btnSupplementRegistartion.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSupplementRegistartion.Size = new System.Drawing.Size(95, 23);
            this.btnSupplementRegistartion.TabIndex = 6;
            this.btnSupplementRegistartion.Text = "보충량 저장";
            this.btnSupplementRegistartion.TooltipLanguageKey = "";
            this.btnSupplementRegistartion.Visible = false;
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 3;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.btnSupplementRegistartion, 1, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.btnSupplementConfirmation, 2, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(47, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 1;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(428, 24);
            this.smartSplitTableLayoutPanel1.TabIndex = 7;
            // 
            // ChemicalRegistration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "ChemicalRegistration";
            this.Text = "ChemicalRegistration";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Framework.SmartControls.SmartBandedGrid grdChemicalRegistration;
        private Framework.SmartControls.SmartButton btnSupplementRegistartion;
        private Framework.SmartControls.SmartButton btnSupplementConfirmation;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
    }
}