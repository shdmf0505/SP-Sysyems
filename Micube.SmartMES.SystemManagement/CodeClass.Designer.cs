namespace Micube.SmartMES.SystemManagement
{
    partial class CodeClass
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
            this.components = new System.ComponentModel.Container();
            this.grdCodeClass = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdCodeClass);
            // 
            // grdCodeClass
            // 
            this.grdCodeClass.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdCodeClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCodeClass.IsUsePaging = false;
            this.grdCodeClass.LanguageKey = "GRIDCODECLASSLIST";
            this.grdCodeClass.Location = new System.Drawing.Point(0, 0);
            this.grdCodeClass.Margin = new System.Windows.Forms.Padding(0);
            this.grdCodeClass.Name = "grdCodeClass";
            this.grdCodeClass.ShowBorder = true;
            this.grdCodeClass.Size = new System.Drawing.Size(470, 396);
            this.grdCodeClass.TabIndex = 1;
            // 
            // CodeClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "CodeClass";
            this.Text = "CodeClassManagement";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdCodeClass;
    }
}