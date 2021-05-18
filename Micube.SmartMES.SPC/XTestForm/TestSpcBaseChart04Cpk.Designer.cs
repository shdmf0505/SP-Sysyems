namespace Micube.SmartMES.SPC
{
    partial class TestSpcBaseChart04Cpk
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
            this.ucCpkGrid1 = new Micube.SmartMES.SPC.UserControl.ucCpkGrid();
            this.SuspendLayout();
            // 
            // ucCpkGrid1
            // 
            this.ucCpkGrid1.Appearance.BackColor = System.Drawing.Color.White;
            this.ucCpkGrid1.Appearance.Options.UseBackColor = true;
            this.ucCpkGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCpkGrid1.Location = new System.Drawing.Point(0, 0);
            this.ucCpkGrid1.Name = "ucCpkGrid1";
            this.ucCpkGrid1.Size = new System.Drawing.Size(895, 699);
            this.ucCpkGrid1.TabIndex = 0;
            // 
            // TestSpcBaseChart04Cpk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 699);
            this.Controls.Add(this.ucCpkGrid1);
            this.Name = "TestSpcBaseChart04Cpk";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TestSpcBaseChart04Cpk";
            this.Load += new System.EventHandler(this.TestSpcBaseChart04Cpk_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.ucCpkGrid ucCpkGrid1;
    }
}