namespace Micube.SmartMES.Test
{
    partial class MasterDetailGridTest
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
            this.grdMasterDetail = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdMasterDetail);
            // 
            // grdMasterDetail
            // 
            this.grdMasterDetail.Caption = "";
            this.grdMasterDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMasterDetail.IsUsePaging = false;
            this.grdMasterDetail.LanguageKey = null;
            this.grdMasterDetail.Location = new System.Drawing.Point(0, 0);
            this.grdMasterDetail.Margin = new System.Windows.Forms.Padding(0);
            this.grdMasterDetail.Name = "grdMasterDetail";
            this.grdMasterDetail.ShowBorder = true;
            this.grdMasterDetail.Size = new System.Drawing.Size(470, 396);
            this.grdMasterDetail.TabIndex = 0;
            // 
            // MasterDetailGridTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "MasterDetailGridTest";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdMasterDetail;
    }
}