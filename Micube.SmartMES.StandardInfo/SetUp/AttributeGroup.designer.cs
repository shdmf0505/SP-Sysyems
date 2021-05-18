namespace Micube.SmartMES.StandardInfo
{
    partial class AttributeGroup
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
            this.grdAttribGList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdAAGList = new Micube.Framework.SmartControls.SmartBandedGrid();
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
            this.pnlCondition.Size = new System.Drawing.Size(371, 516);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(790, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer1);
            this.pnlContent.Size = new System.Drawing.Size(790, 519);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1171, 555);
            // 
            // grdAttribGList
            // 
            this.grdAttribGList.Caption = "";
            this.grdAttribGList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAttribGList.IsUsePaging = false;
            this.grdAttribGList.LanguageKey = null;
            this.grdAttribGList.Location = new System.Drawing.Point(0, 0);
            this.grdAttribGList.Margin = new System.Windows.Forms.Padding(0);
            this.grdAttribGList.Name = "grdAttribGList";
            this.grdAttribGList.ShowBorder = true;
            this.grdAttribGList.ShowStatusBar = false;
            this.grdAttribGList.Size = new System.Drawing.Size(429, 519);
            this.grdAttribGList.TabIndex = 0;
            // 
            // grdAAGList
            // 
            this.grdAAGList.Caption = "";
            this.grdAAGList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAAGList.IsUsePaging = false;
            this.grdAAGList.LanguageKey = null;
            this.grdAAGList.Location = new System.Drawing.Point(0, 0);
            this.grdAAGList.Margin = new System.Windows.Forms.Padding(0);
            this.grdAAGList.Name = "grdAAGList";
            this.grdAAGList.ShowBorder = true;
            this.grdAAGList.ShowStatusBar = false;
            this.grdAAGList.Size = new System.Drawing.Size(355, 519);
            this.grdAAGList.TabIndex = 0;
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdAAGList);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdAttribGList);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(790, 519);
            this.smartSpliterContainer1.SplitterPosition = 355;
            this.smartSpliterContainer1.TabIndex = 2;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // AttributeGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 593);
            this.Name = "AttributeGroup";
            this.Padding = new System.Windows.Forms.Padding(19, 19, 19, 19);
            this.Text = "MasterDataClass";
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
        private Framework.SmartControls.SmartBandedGrid grdAAGList;
        private Framework.SmartControls.SmartBandedGrid grdAttribGList;
    }
}