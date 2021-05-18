namespace Micube.SmartMES.Commons.Controls
{
    partial class ucLabelViewer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.designBar3 = new DevExpress.XtraReports.UserDesigner.DesignBar();
            this.designBar1 = new DevExpress.XtraReports.UserDesigner.DesignBar();
            this.documentViewer1 = new DevExpress.XtraPrinting.Preview.DocumentViewer();
            this.smartPropertyGrid1 = new Micube.Framework.SmartControls.SmartPropertyGrid();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            ((System.ComponentModel.ISupportInitialize)(this.smartPropertyGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // designBar3
            // 
            this.designBar3.BarName = "Formatting Toolbar";
            this.designBar3.DockCol = 1;
            this.designBar3.DockRow = 1;
            this.designBar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.designBar3.Text = "Formatting Toolbar";
            // 
            // designBar1
            // 
            this.designBar1.BarName = "Main Menu";
            this.designBar1.DockCol = 0;
            this.designBar1.DockRow = 0;
            this.designBar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.designBar1.OptionsBar.MultiLine = true;
            this.designBar1.OptionsBar.UseWholeRow = true;
            this.designBar1.Text = "Main Menu";
            // 
            // documentViewer1
            // 
            this.documentViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentViewer1.IsMetric = true;
            this.documentViewer1.Location = new System.Drawing.Point(0, 0);
            this.documentViewer1.Name = "documentViewer1";
            this.documentViewer1.Size = new System.Drawing.Size(389, 446);
            this.documentViewer1.TabIndex = 2;
            // 
            // smartPropertyGrid1
            // 
            this.smartPropertyGrid1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smartPropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPropertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.smartPropertyGrid1.Name = "smartPropertyGrid1";
            this.smartPropertyGrid1.OptionsBehavior.SmartExpand = false;
            this.smartPropertyGrid1.OptionsView.ShowButtons = false;
            this.smartPropertyGrid1.OptionsView.ShowRootCategories = false;
            this.smartPropertyGrid1.Size = new System.Drawing.Size(261, 446);
            this.smartPropertyGrid1.TabIndex = 3;
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.documentViewer1);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.smartPropertyGrid1);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(655, 446);
            this.smartSpliterContainer1.SplitterPosition = 389;
            this.smartSpliterContainer1.TabIndex = 1;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // ucLabelViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.smartSpliterContainer1);
            this.Name = "ucLabelViewer";
            this.Size = new System.Drawing.Size(655, 446);
            ((System.ComponentModel.ISupportInitialize)(this.smartPropertyGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraReports.UserDesigner.DesignBar designBar3;
        private DevExpress.XtraReports.UserDesigner.DesignBar designBar1;
        private DevExpress.XtraPrinting.Preview.DocumentViewer documentViewer1;
        private Framework.SmartControls.SmartPropertyGrid smartPropertyGrid1;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
    }
}
