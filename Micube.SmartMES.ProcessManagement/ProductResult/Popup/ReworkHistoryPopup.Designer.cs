namespace Micube.SmartMES.ProcessManagement
{
	partial class ReworkHistoryPopup
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
            this.grdProcessPath = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.grdProcessPath);
            this.pnlMain.Size = new System.Drawing.Size(791, 563);
            // 
            // grdProcessPath
            // 
            this.grdProcessPath.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdProcessPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProcessPath.IsUsePaging = false;
            this.grdProcessPath.LanguageKey = "PROCESSSEGMENT";
            this.grdProcessPath.Location = new System.Drawing.Point(0, 0);
            this.grdProcessPath.Margin = new System.Windows.Forms.Padding(0);
            this.grdProcessPath.Name = "grdProcessPath";
            this.grdProcessPath.ShowBorder = true;
            this.grdProcessPath.Size = new System.Drawing.Size(791, 563);
            this.grdProcessPath.TabIndex = 5;
            // 
            // ReworkHistoryPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 583);
            this.LanguageKey = "REWORKROUTING";
            this.Name = "ReworkHistoryPopup";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

		}

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdProcessPath;
    }
}
