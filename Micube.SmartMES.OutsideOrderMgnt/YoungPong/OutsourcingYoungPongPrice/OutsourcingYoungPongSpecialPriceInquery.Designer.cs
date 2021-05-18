namespace Micube.SmartMES.OutsideOrderMgnt
{
    partial class OutsourcingYoungPongSpecialPriceInquiry
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
            this.grdSpecialAmount = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 517);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(764, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdSpecialAmount);
            this.pnlContent.Size = new System.Drawing.Size(764, 521);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1069, 550);
            // 
            // grdSpecialAmount
            // 
            this.grdSpecialAmount.Caption = "";
            this.grdSpecialAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSpecialAmount.IsUsePaging = false;
            this.grdSpecialAmount.LanguageKey = "OSPSpecialAmountHistoryBrowse";
            this.grdSpecialAmount.Location = new System.Drawing.Point(0, 0);
            this.grdSpecialAmount.Margin = new System.Windows.Forms.Padding(0);
            this.grdSpecialAmount.Name = "grdSpecialAmount";
            this.grdSpecialAmount.ShowBorder = true;
            this.grdSpecialAmount.ShowStatusBar = false;
            this.grdSpecialAmount.Size = new System.Drawing.Size(764, 521);
            this.grdSpecialAmount.TabIndex = 119;
            // 
            // OutsourcingSpecialAmountModifiedBrowse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1089, 570);
            this.Name = "OutsourcingSpecialAmountModifiedBrowse";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdSpecialAmount;
    }
}