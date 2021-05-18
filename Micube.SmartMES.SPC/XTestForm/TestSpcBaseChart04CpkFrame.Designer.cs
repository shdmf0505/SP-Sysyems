namespace Micube.SmartMES.SPC
{
    partial class TestSpcBaseChart04CpkFrame
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
            this.ucCpkFrame01 = new Micube.SmartMES.SPC.UserControl.ucCpkFrame();
            this.SuspendLayout();
            // 
            // ucCpkFrame01
            // 
            this.ucCpkFrame01.Appearance.BackColor = System.Drawing.Color.White;
            this.ucCpkFrame01.Appearance.Options.UseBackColor = true;
            this.ucCpkFrame01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCpkFrame01.Location = new System.Drawing.Point(0, 0);
            this.ucCpkFrame01.Name = "ucCpkFrame01";
            this.ucCpkFrame01.Size = new System.Drawing.Size(1038, 587);
            this.ucCpkFrame01.TabIndex = 0;
            // 
            // TestSpcBaseChart04CpkFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 587);
            this.Controls.Add(this.ucCpkFrame01);
            this.Name = "TestSpcBaseChart04CpkFrame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TestSpcBaseChart04CpkFrame";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TestSpcBaseChart04CpkFrame_Load);
            this.Resize += new System.EventHandler(this.TestSpcBaseChart04CpkFrame_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.ucCpkFrame ucCpkFrame01;
    }
}