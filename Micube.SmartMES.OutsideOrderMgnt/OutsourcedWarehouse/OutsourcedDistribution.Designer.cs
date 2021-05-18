namespace Micube.SmartMES.OutsideOrderMgnt
{
    partial class OutsourcedDistribution
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
            this.btnDistVen = new Micube.Framework.SmartControls.SmartButton();
            this.grdOutsourcedDistribution = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
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
            this.pnlToolbar.Controls.Add(this.btnDistVen);
            this.pnlToolbar.Size = new System.Drawing.Size(845, 30);
            this.pnlToolbar.Controls.SetChildIndex(this.btnDistVen, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdOutsourcedDistribution);
            this.pnlContent.Size = new System.Drawing.Size(845, 911);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1226, 947);
            // 
            // btnDistVen
            // 
            this.btnDistVen.AllowFocus = false;
            this.btnDistVen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDistVen.IsBusy = false;
            this.btnDistVen.IsWrite = true;
            this.btnDistVen.LanguageKey = "OSPAREAIDCHANGE";
            this.btnDistVen.Location = new System.Drawing.Point(711, 0);
            this.btnDistVen.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnDistVen.Name = "btnDistVen";
            this.btnDistVen.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnDistVen.Size = new System.Drawing.Size(113, 25);
            this.btnDistVen.TabIndex = 5;
            this.btnDistVen.Text = "외주처변경";
            this.btnDistVen.TooltipLanguageKey = "";
            this.btnDistVen.Visible = false;
            // 
            // grdOutsourcedDistribution
            // 
            this.grdOutsourcedDistribution.Caption = "외주 창고 입고 목록";
            this.grdOutsourcedDistribution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOutsourcedDistribution.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdOutsourcedDistribution.IsUsePaging = false;
            this.grdOutsourcedDistribution.LanguageKey = "OUTSOURCEDDISTRIBUTIONLIST";
            this.grdOutsourcedDistribution.Location = new System.Drawing.Point(0, 0);
            this.grdOutsourcedDistribution.Margin = new System.Windows.Forms.Padding(0);
            this.grdOutsourcedDistribution.Name = "grdOutsourcedDistribution";
            this.grdOutsourcedDistribution.ShowBorder = true;
            this.grdOutsourcedDistribution.Size = new System.Drawing.Size(845, 911);
            this.grdOutsourcedDistribution.TabIndex = 2;
            // 
            // OutsourcedDistribution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 985);
            this.Name = "OutsourcedDistribution";
            this.Padding = new System.Windows.Forms.Padding(19, 19, 19, 19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartButton btnDistVen;
        private Framework.SmartControls.SmartBandedGrid grdOutsourcedDistribution;
    }
}