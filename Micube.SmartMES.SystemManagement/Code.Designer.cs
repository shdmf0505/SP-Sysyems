namespace Micube.SmartMES.SystemManagement
{
    partial class Code
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
            this.grdCode = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdCodeClass = new Micube.Framework.SmartControls.SmartBandedGrid();
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
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer1);
            // 
            // grdCode
            // 
            this.grdCode.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCode.IsUsePaging = false;
            this.grdCode.LanguageKey = "GRIDCODELIST";
            this.grdCode.Location = new System.Drawing.Point(0, 0);
            this.grdCode.Margin = new System.Windows.Forms.Padding(0);
            this.grdCode.Name = "grdCode";
            this.grdCode.ShowBorder = true;
            this.grdCode.Size = new System.Drawing.Size(60, 396);
            this.grdCode.TabIndex = 28;
            // 
            // grdCodeClass
            // 
            this.grdCodeClass.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdCodeClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCodeClass.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.Refresh;
            this.grdCodeClass.IsUsePaging = false;
            this.grdCodeClass.LanguageKey = "GRIDCODECLASSLIST";
            this.grdCodeClass.Location = new System.Drawing.Point(0, 0);
            this.grdCodeClass.Margin = new System.Windows.Forms.Padding(0);
            this.grdCodeClass.Name = "grdCodeClass";
            this.grdCodeClass.ShowBorder = true;
            this.grdCodeClass.Size = new System.Drawing.Size(400, 396);
            this.grdCodeClass.TabIndex = 27;
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdCodeClass);
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdCode);
            this.smartSpliterContainer1.Size = new System.Drawing.Size(470, 396);
            this.smartSpliterContainer1.SplitterPosition = 400;
            this.smartSpliterContainer1.TabIndex = 0;
            // 
            // Code
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "Code";
            this.Text = "Code";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdCodeClass;
        private Framework.SmartControls.SmartBandedGrid grdCode;
    }
}