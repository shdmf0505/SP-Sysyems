namespace Micube.SmartMES.Test
{
    partial class TwoGrid
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
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdCodeClass = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdCode = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 397);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer1);
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdCodeClass);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdCode);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(475, 401);
            this.smartSpliterContainer1.SplitterPosition = 276;
            this.smartSpliterContainer1.TabIndex = 0;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdCodeClass
            // 
            this.grdCodeClass.Caption = "CODECLASSLIST";
            this.grdCodeClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCodeClass.IsUsePaging = false;
            this.grdCodeClass.LanguageKey = null;
            this.grdCodeClass.Location = new System.Drawing.Point(0, 0);
            this.grdCodeClass.Margin = new System.Windows.Forms.Padding(0);
            this.grdCodeClass.Name = "grdCodeClass";
            this.grdCodeClass.ShowBorder = true;
            this.grdCodeClass.Size = new System.Drawing.Size(276, 401);
            this.grdCodeClass.TabIndex = 0;
            // 
            // grdCode
            // 
            this.grdCode.Caption = "CODELIST";
            this.grdCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCode.IsUsePaging = false;
            this.grdCode.LanguageKey = null;
            this.grdCode.Location = new System.Drawing.Point(0, 0);
            this.grdCode.Margin = new System.Windows.Forms.Padding(0);
            this.grdCode.Name = "grdCode";
            this.grdCode.ShowBorder = true;
            this.grdCode.Size = new System.Drawing.Size(194, 401);
            this.grdCode.TabIndex = 0;
            // 
            // TwoGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "TwoGrid";
            this.Text = "CodeClassManagement";
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