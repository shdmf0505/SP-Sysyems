namespace Micube.SmartMES.SPC
{
    partial class TestSpcBaseChart0301Raw
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
            this.SuspendLayout();
            // 
            // ucChartDetail1
            // 
            this.ucChartDetail1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucChartDetail1.Location = new System.Drawing.Point(0, 0);
            this.ucChartDetail1.Name = "ucChartDetail1";
            this.ucChartDetail1.Size = new System.Drawing.Size(1264, 921);
            this.ucChartDetail1.TabIndex = 0;
            // 
            // TestSpcBaseChart0301Raw
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 921);
            this.Controls.Add(this.ucChartDetail1);
            this.Name = "TestSpcBaseChart0301Raw";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TestSpcBaseChart0301Raw";
            this.Load += new System.EventHandler(this.TestSpcBaseChart0301Raw_Load);
            this.Resize += new System.EventHandler(this.TestSpcBaseChart0301Raw_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        public UserControl.ucChartDetail ucChartDetail1;
    }
}