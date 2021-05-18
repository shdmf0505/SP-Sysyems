namespace Micube.SmartMES.QualityAnalysis
{
    partial class DaySelectPopup
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
            this.grdDays = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.btnLOTView = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.grdDays);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.None;
            this.pnlMain.Size = new System.Drawing.Size(460, 300);
            // 
            // grdDays
            // 
            this.grdDays.Caption = "";
            this.grdDays.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDays.IsUsePaging = false;
            this.grdDays.LanguageKey = null;
            this.grdDays.Location = new System.Drawing.Point(0, 0);
            this.grdDays.Margin = new System.Windows.Forms.Padding(0);
            this.grdDays.Name = "grdDays";
            this.grdDays.ShowBorder = true;
            this.grdDays.ShowButtonBar = false;
            this.grdDays.Size = new System.Drawing.Size(460, 300);
            this.grdDays.TabIndex = 0;
            this.grdDays.UseAutoBestFitColumns = false;
            // 
            // btnLOTView
            // 
            this.btnLOTView.AllowFocus = false;
            this.btnLOTView.IsBusy = false;
            this.btnLOTView.IsWrite = false;
            this.btnLOTView.LanguageKey = "LOTVIEW";
            this.btnLOTView.Location = new System.Drawing.Point(390, 320);
            this.btnLOTView.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnLOTView.Name = "btnLOTView";
            this.btnLOTView.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnLOTView.Size = new System.Drawing.Size(80, 25);
            this.btnLOTView.TabIndex = 1;
            this.btnLOTView.Text = "LOT보기";
            this.btnLOTView.TooltipLanguageKey = "";
            // 
            // DaySelectPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 356);
            this.Controls.Add(this.btnLOTView);
            this.Name = "DaySelectPopup";
            this.Text = "날짜 선택";
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.Controls.SetChildIndex(this.btnLOTView, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdDays;
        private Framework.SmartControls.SmartButton btnLOTView;
    }
}