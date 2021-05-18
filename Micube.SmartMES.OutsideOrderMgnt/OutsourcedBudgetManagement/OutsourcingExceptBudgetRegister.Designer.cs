namespace Micube.SmartMES.OutsideOrderMgnt
{
    partial class OutsourcingExceptBudgetRegister
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
            this.grdOutsourcingExceptBudgetRegister = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Size = new System.Drawing.Size(371, 908);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(845, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdOutsourcingExceptBudgetRegister);
            this.pnlContent.Size = new System.Drawing.Size(845, 911);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1226, 947);
            // 
            // grdOutsourcingExceptBudgetRegister
            // 
            this.grdOutsourcingExceptBudgetRegister.Caption = "예산 통제 제외 등록";
            this.grdOutsourcingExceptBudgetRegister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOutsourcingExceptBudgetRegister.IsUsePaging = false;
            this.grdOutsourcingExceptBudgetRegister.LanguageKey = "OUTSOURCINGEXCEPTBUDGETREGISTER";
            this.grdOutsourcingExceptBudgetRegister.Location = new System.Drawing.Point(0, 0);
            this.grdOutsourcingExceptBudgetRegister.Margin = new System.Windows.Forms.Padding(0);
            this.grdOutsourcingExceptBudgetRegister.Name = "grdOutsourcingExceptBudgetRegister";
            this.grdOutsourcingExceptBudgetRegister.ShowBorder = true;
            this.grdOutsourcingExceptBudgetRegister.Size = new System.Drawing.Size(845, 911);
            this.grdOutsourcingExceptBudgetRegister.TabIndex = 2;
            // 
            // OutsourcingExceptBudgetRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 985);
            this.Name = "OutsourcingExceptBudgetRegister";
            this.Padding = new System.Windows.Forms.Padding(19, 19, 19, 19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdOutsourcingExceptBudgetRegister;
    }
}