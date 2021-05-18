namespace Micube.SmartMES.SystemManagement
{
    partial class DictionaryClass
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
            this.grdDictionaryClass = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Size = new System.Drawing.Size(296, 530);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(584, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdDictionaryClass);
            this.pnlContent.Size = new System.Drawing.Size(584, 525);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(894, 559);
            // 
            // grdDictionaryClass
            // 
            this.grdDictionaryClass.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdDictionaryClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDictionaryClass.IsUsePaging = false;
            this.grdDictionaryClass.LanguageKey = "GRIDDICTIONARYCLASSLIST";
            this.grdDictionaryClass.Location = new System.Drawing.Point(0, 0);
            this.grdDictionaryClass.Margin = new System.Windows.Forms.Padding(0);
            this.grdDictionaryClass.Name = "grdDictionaryClass";
            this.grdDictionaryClass.ShowBorder = true;
            this.grdDictionaryClass.Size = new System.Drawing.Size(584, 525);
            this.grdDictionaryClass.TabIndex = 1;
            // 
            // DictionaryClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 579);
            this.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.Name = "DictionaryClass";
            this.Text = "DictionaryClassManagement";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdDictionaryClass;
    }
}