namespace Micube.SmartMES.QualityAnalysis
{
	partial class QualityAddFilesPopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QualityAddFilesPopup));
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.fpcImage = new Micube.SmartMES.Commons.Controls.SmartFileProcessingControl();
            this.smartPanel2 = new Micube.Framework.SmartControls.SmartPanel();
            this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
            this.btnApply = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).BeginInit();
            this.smartPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(888, 409);
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartPanel1, 0, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 2;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(888, 409);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // smartPanel1
            // 
            this.smartPanel1.Controls.Add(this.fpcImage);
            this.smartPanel1.Controls.Add(this.smartPanel2);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(3, 3);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(882, 403);
            this.smartPanel1.TabIndex = 0;
            // 
            // fpcImage
            // 
            this.fpcImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpcImage.LanguageKey = "";
            this.fpcImage.Location = new System.Drawing.Point(2, 2);
            this.fpcImage.Margin = new System.Windows.Forms.Padding(0, 12, 5, 12);
            this.fpcImage.Name = "fpcImage";
            this.fpcImage.Size = new System.Drawing.Size(878, 360);
            this.fpcImage.TabIndex = 3;
            this.fpcImage.UploadPath = "";
            this.fpcImage.UseCommentsColumn = true;
            // 
            // smartPanel2
            // 
            this.smartPanel2.Controls.Add(this.btnCancel);
            this.smartPanel2.Controls.Add(this.btnApply);
            this.smartPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.smartPanel2.Location = new System.Drawing.Point(2, 362);
            this.smartPanel2.Name = "smartPanel2";
            this.smartPanel2.Size = new System.Drawing.Size(878, 39);
            this.smartPanel2.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.IsBusy = false;
            this.btnCancel.IsWrite = false;
            this.btnCancel.LanguageKey = "CANCEL";
            this.btnCancel.Location = new System.Drawing.Point(791, 7);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TooltipLanguageKey = "";
            // 
            // btnApply
            // 
            this.btnApply.AllowFocus = false;
            this.btnApply.IsBusy = false;
            this.btnApply.IsWrite = false;
            this.btnApply.LanguageKey = "APPLY";
            this.btnApply.Location = new System.Drawing.Point(710, 7);
            this.btnApply.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnApply.Name = "btnApply";
            this.btnApply.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 3;
            this.btnApply.Text = "Apply";
            this.btnApply.TooltipLanguageKey = "";
            // 
            // QualityAddFilesPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 429);
            this.Name = "QualityAddFilesPopup";
            this.Text = "QualityAddFilesPopup";
            this.Load += new System.EventHandler(this.QualityAddFilesPopup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).EndInit();
            this.smartPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartPanel smartPanel2;
        private Commons.Controls.SmartFileProcessingControl fpcImage;
        private Framework.SmartControls.SmartButton btnCancel;
        private Framework.SmartControls.SmartButton btnApply;
    }
}