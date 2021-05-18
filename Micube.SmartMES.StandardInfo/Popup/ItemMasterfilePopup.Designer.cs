namespace Micube.SmartMES.StandardInfo
{
    partial class ItemMasterfilePopup
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemMasterfilePopup));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.btnSave = new Micube.Framework.SmartControls.SmartButton();
			this.fileInspectionPaper = new Micube.SmartMES.Commons.Controls.SmartFileProcessingControl();
			this.pnlButton = new System.Windows.Forms.FlowLayoutPanel();
			this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			this.pnlMain.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.pnlButton.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.tableLayoutPanel1);
			this.pnlMain.Size = new System.Drawing.Size(784, 471);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.fileInspectionPaper, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.pnlButton, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 471);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// btnSave
			// 
			this.btnSave.AllowFocus = false;
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.IsBusy = false;
			this.btnSave.IsWrite = false;
			this.btnSave.LanguageKey = "SAVE";
			this.btnSave.Location = new System.Drawing.Point(619, 0);
			this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.btnSave.Name = "btnSave";
			this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 110;
			this.btnSave.Text = "smartButton1";
			this.btnSave.TooltipLanguageKey = "";
			// 
			// fileInspectionPaper
			// 
			this.fileInspectionPaper.countRows = false;
			this.fileInspectionPaper.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fileInspectionPaper.executeFileAfterDown = false;
			this.fileInspectionPaper.LanguageKey = "";
			this.fileInspectionPaper.Location = new System.Drawing.Point(0, 0);
			this.fileInspectionPaper.Margin = new System.Windows.Forms.Padding(0);
			this.fileInspectionPaper.Name = "fileInspectionPaper";
			this.fileInspectionPaper.showImage = false;
			this.fileInspectionPaper.Size = new System.Drawing.Size(784, 441);
			this.fileInspectionPaper.TabIndex = 3;
			this.fileInspectionPaper.UploadPath = "";
			this.fileInspectionPaper.UseCommentsColumn = true;
			// 
			// pnlButton
			// 
			this.pnlButton.Controls.Add(this.btnCancel);
			this.pnlButton.Controls.Add(this.btnSave);
			this.pnlButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlButton.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.pnlButton.Location = new System.Drawing.Point(3, 444);
			this.pnlButton.Name = "pnlButton";
			this.pnlButton.Size = new System.Drawing.Size(778, 24);
			this.pnlButton.TabIndex = 111;
			// 
			// btnCancel
			// 
			this.btnCancel.AllowFocus = false;
			this.btnCancel.IsBusy = false;
			this.btnCancel.IsWrite = false;
			this.btnCancel.LanguageKey = "CANCEL";
			this.btnCancel.Location = new System.Drawing.Point(700, 0);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 111;
			this.btnCancel.Text = "smartButton1";
			this.btnCancel.TooltipLanguageKey = "";
			// 
			// ItemMasterfilePopup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(804, 491);
			this.Name = "ItemMasterfilePopup";
			this.Text = "DefectCodeClassSelectPopup";
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			this.pnlMain.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.pnlButton.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Commons.Controls.SmartFileProcessingControl fileInspectionPaper;
        private Framework.SmartControls.SmartButton btnSave;
		private System.Windows.Forms.FlowLayoutPanel pnlButton;
		private Framework.SmartControls.SmartButton btnCancel;
	}
}