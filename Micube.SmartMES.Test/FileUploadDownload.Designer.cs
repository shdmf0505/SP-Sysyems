namespace Micube.SmartMES.Test
{
    partial class FileUploadDownload
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileUploadDownload));
            this.fpcFile = new Micube.SmartMES.Commons.Controls.SmartFileProcessingControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.fpcFile);
            // 
            // fpcFile
            // 
            this.fpcFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpcFile.LanguageKey = "TEST";
            this.fpcFile.Location = new System.Drawing.Point(0, 0);
            this.fpcFile.Margin = new System.Windows.Forms.Padding(0);
            this.fpcFile.Name = "fpcFile";
            this.fpcFile.Size = new System.Drawing.Size(470, 396);
            this.fpcFile.TabIndex = 0;
            this.fpcFile.UploadPath = "";
            this.fpcFile.UseCommentsColumn = true;
            // 
            // FileUploadDownload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "FileUploadDownload";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Commons.Controls.SmartFileProcessingControl fpcFile;
    }
}