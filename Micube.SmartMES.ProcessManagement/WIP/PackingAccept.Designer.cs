namespace Micube.SmartMES.ProcessManagement
{
    partial class PackingAccept
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
			this.grdPacking = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.pnlLot = new Micube.Framework.SmartControls.SmartPanel();
			this.txtLotId = new Micube.Framework.SmartControls.SmartTextBox();
			this.lblLotId = new Micube.Framework.SmartControls.SmartLabel();
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
			this.pnlContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlLot)).BeginInit();
			this.pnlLot.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlCondition
			// 
			this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
			// 
			// pnlContent
			// 
			this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
			this.pnlContent.Appearance.Options.UseBackColor = true;
			this.pnlContent.Controls.Add(this.grdPacking);
			this.pnlContent.Controls.Add(this.pnlLot);
			// 
			// grdPacking
			// 
			this.grdPacking.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdPacking.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdPacking.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
			this.grdPacking.IsUsePaging = false;
			this.grdPacking.LanguageKey = "WIPLIST";
			this.grdPacking.Location = new System.Drawing.Point(0, 30);
			this.grdPacking.Margin = new System.Windows.Forms.Padding(0);
			this.grdPacking.Name = "grdPacking";
			this.grdPacking.ShowBorder = true;
			this.grdPacking.Size = new System.Drawing.Size(756, 459);
			this.grdPacking.TabIndex = 1;
			this.grdPacking.UseAutoBestFitColumns = false;
			// 
			// pnlLot
			// 
			this.pnlLot.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pnlLot.Controls.Add(this.lblLotId);
			this.pnlLot.Controls.Add(this.txtLotId);
			this.pnlLot.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlLot.Location = new System.Drawing.Point(0, 0);
			this.pnlLot.Name = "pnlLot";
			this.pnlLot.Size = new System.Drawing.Size(756, 30);
			this.pnlLot.TabIndex = 2;
			// 
			// txtLotId
			// 
			this.txtLotId.LabelText = null;
			this.txtLotId.LanguageKey = null;
			this.txtLotId.Location = new System.Drawing.Point(70, 5);
			this.txtLotId.Name = "txtLotId";
			this.txtLotId.Size = new System.Drawing.Size(249, 20);
			this.txtLotId.TabIndex = 0;
			// 
			// lblLotId
			// 
			this.lblLotId.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.lblLotId.LanguageKey = "LOTID";
			this.lblLotId.Location = new System.Drawing.Point(5, 5);
			this.lblLotId.Name = "lblLotId";
			this.lblLotId.Size = new System.Drawing.Size(65, 20);
			this.lblLotId.TabIndex = 1;
			this.lblLotId.Text = "LOT ID : ";
			// 
			// PackingAccept
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1081, 538);
			this.Name = "PackingAccept";
			this.Text = "Lot Hold";
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
			this.pnlContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlLot)).EndInit();
			this.pnlLot.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdPacking;
		private Framework.SmartControls.SmartPanel pnlLot;
		private Framework.SmartControls.SmartLabel lblLotId;
		private Framework.SmartControls.SmartTextBox txtLotId;
	}
}