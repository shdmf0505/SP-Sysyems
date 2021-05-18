namespace Micube.SmartMES.Commons.Controls
{
    partial class ucLabelViewer2
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
            this.smartPropertyGrid1 = new Micube.Framework.SmartControls.SmartPropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.smartPropertyGrid1)).BeginInit();
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
            // smartPropertyGrid1
            // 
            this.smartPropertyGrid1.Appearance.Category.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.smartPropertyGrid1.Appearance.Category.Options.UseFont = true;
            this.smartPropertyGrid1.Appearance.VertLine.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.smartPropertyGrid1.Appearance.VertLine.Options.UseFont = true;
            this.smartPropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPropertyGrid1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.smartPropertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.smartPropertyGrid1.Name = "smartPropertyGrid1";
            this.smartPropertyGrid1.OptionsBehavior.SmartExpand = false;
            this.smartPropertyGrid1.OptionsView.ShowButtons = false;
            this.smartPropertyGrid1.OptionsView.ShowRootCategories = false;
            this.smartPropertyGrid1.Size = new System.Drawing.Size(655, 446);
            this.smartPropertyGrid1.TabIndex = 4;
            // 
            // ucLabelViewer2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.smartPropertyGrid1);
            this.Name = "ucLabelViewer2";
            this.Size = new System.Drawing.Size(655, 446);
            ((System.ComponentModel.ISupportInitialize)(this.smartPropertyGrid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraReports.UserDesigner.DesignBar designBar3;
        private DevExpress.XtraReports.UserDesigner.DesignBar designBar1;
        private Framework.SmartControls.SmartPropertyGrid smartPropertyGrid1;
    }
}
