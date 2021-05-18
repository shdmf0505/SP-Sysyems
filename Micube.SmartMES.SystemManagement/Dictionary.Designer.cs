namespace Micube.SmartMES.SystemManagement
{
    partial class Dictionary
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
            this.grdDictionary = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdDictionaryClass = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer1);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            // 
            // grdDictionary
            // 
            this.grdDictionary.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdDictionary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDictionary.IsUsePaging = false;
            this.grdDictionary.LanguageKey = "GRIDDICTIONARYLIST";
            this.grdDictionary.Location = new System.Drawing.Point(0, 0);
            this.grdDictionary.Margin = new System.Windows.Forms.Padding(0);
            this.grdDictionary.Name = "grdDictionary";
            this.grdDictionary.ShowBorder = true;
            this.grdDictionary.Size = new System.Drawing.Size(160, 396);
            this.grdDictionary.TabIndex = 2;
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
            this.grdDictionaryClass.Size = new System.Drawing.Size(300, 396);
            this.grdDictionaryClass.TabIndex = 0;
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdDictionaryClass);
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdDictionary);
            this.smartSpliterContainer1.Size = new System.Drawing.Size(470, 396);
            this.smartSpliterContainer1.SplitterPosition = 300;
            this.smartSpliterContainer1.TabIndex = 3;
            // 
            // Dictionary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "Dictionary";
            this.Text = "DictionaryManagement";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Framework.SmartControls.SmartBandedGrid grdDictionaryClass;
        private Framework.SmartControls.SmartBandedGrid grdDictionary;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
    }
}