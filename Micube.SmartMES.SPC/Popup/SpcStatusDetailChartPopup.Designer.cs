namespace Micube.SmartMES.SPC
{
    partial class SpcStatusDetailChartPopup
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
            this.ucChartDetail1 = new Micube.SmartMES.SPC.UserControl.ucChartDetail();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.ucChartDetail1);
            this.pnlMain.Size = new System.Drawing.Size(1244, 841);
            // 
            // ucChartDetail1
            // 
            this.ucChartDetail1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucChartDetail1.Location = new System.Drawing.Point(0, 0);
            this.ucChartDetail1.Name = "ucChartDetail1";
            this.ucChartDetail1.Size = new System.Drawing.Size(1244, 841);
            this.ucChartDetail1.TabIndex = 1;
            // 
            // SpcStatusDetailChartPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 861);
            this.Name = "SpcStatusDetailChartPopup";
            this.Text = "상세정보 ";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public UserControl.ucChartDetail ucChartDetail1;
    }
}