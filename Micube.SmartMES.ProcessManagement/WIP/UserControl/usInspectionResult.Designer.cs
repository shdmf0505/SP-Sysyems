namespace Micube.SmartMES.ProcessManagement
{
    partial class usInspectionResult
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
            this.pnlDefectFile = new System.Windows.Forms.Panel();
            this.picBox = new Micube.Framework.SmartControls.SmartPictureEdit();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.grdFile = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdInspect = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.defectSpliterContainer = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdInspDefect = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterControl2 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.pnlDefectFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.defectSpliterContainer)).BeginInit();
            this.defectSpliterContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDefectFile
            // 
            this.pnlDefectFile.Controls.Add(this.picBox);
            this.pnlDefectFile.Controls.Add(this.smartSpliterControl1);
            this.pnlDefectFile.Controls.Add(this.grdFile);
            this.pnlDefectFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDefectFile.Location = new System.Drawing.Point(0, 0);
            this.pnlDefectFile.Name = "pnlDefectFile";
            this.pnlDefectFile.Size = new System.Drawing.Size(300, 266);
            this.pnlDefectFile.TabIndex = 6;
            // 
            // picBox
            // 
            this.picBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBox.Location = new System.Drawing.Point(0, 148);
            this.picBox.Margin = new System.Windows.Forms.Padding(0);
            this.picBox.Name = "picBox";
            this.picBox.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picBox.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.picBox.Size = new System.Drawing.Size(300, 118);
            this.picBox.TabIndex = 3;
            // 
            // smartSpliterControl1
            // 
            this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartSpliterControl1.Location = new System.Drawing.Point(0, 143);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(300, 5);
            this.smartSpliterControl1.TabIndex = 16;
            this.smartSpliterControl1.TabStop = false;
            // 
            // grdFile
            // 
            this.grdFile.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdFile.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdFile.IsUsePaging = false;
            this.grdFile.LanguageKey = null;
            this.grdFile.Location = new System.Drawing.Point(0, 0);
            this.grdFile.Margin = new System.Windows.Forms.Padding(0);
            this.grdFile.Name = "grdFile";
            this.grdFile.ShowBorder = true;
            this.grdFile.ShowButtonBar = false;
            this.grdFile.Size = new System.Drawing.Size(300, 143);
            this.grdFile.TabIndex = 2;
            this.grdFile.UseAutoBestFitColumns = false;
            // 
            // grdInspect
            // 
            this.grdInspect.Caption = "";
            this.grdInspect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInspect.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInspect.IsUsePaging = false;
            this.grdInspect.LanguageKey = null;
            this.grdInspect.Location = new System.Drawing.Point(0, 0);
            this.grdInspect.Margin = new System.Windows.Forms.Padding(0);
            this.grdInspect.Name = "grdInspect";
            this.grdInspect.ShowBorder = true;
            this.grdInspect.Size = new System.Drawing.Size(1071, 261);
            this.grdInspect.TabIndex = 7;
            this.grdInspect.UseAutoBestFitColumns = false;
            // 
            // defectSpliterContainer
            // 
            this.defectSpliterContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.defectSpliterContainer.Location = new System.Drawing.Point(0, 266);
            this.defectSpliterContainer.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.defectSpliterContainer.Name = "defectSpliterContainer";
            this.defectSpliterContainer.Panel1.Controls.Add(this.grdInspDefect);
            this.defectSpliterContainer.Panel1.Text = "Panel1";
            this.defectSpliterContainer.Panel2.Controls.Add(this.pnlDefectFile);
            this.defectSpliterContainer.Panel2.Text = "Panel2";
            this.defectSpliterContainer.Size = new System.Drawing.Size(1071, 266);
            this.defectSpliterContainer.SplitterPosition = 766;
            this.defectSpliterContainer.TabIndex = 8;
            this.defectSpliterContainer.Text = "smartSpliterContainer1";
            // 
            // grdInspDefect
            // 
            this.grdInspDefect.Caption = "";
            this.grdInspDefect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInspDefect.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInspDefect.IsUsePaging = false;
            this.grdInspDefect.LanguageKey = null;
            this.grdInspDefect.Location = new System.Drawing.Point(0, 0);
            this.grdInspDefect.Margin = new System.Windows.Forms.Padding(0);
            this.grdInspDefect.Name = "grdInspDefect";
            this.grdInspDefect.ShowBorder = true;
            this.grdInspDefect.Size = new System.Drawing.Size(766, 266);
            this.grdInspDefect.TabIndex = 5;
            this.grdInspDefect.UseAutoBestFitColumns = false;
            // 
            // smartSpliterControl2
            // 
            this.smartSpliterControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.smartSpliterControl2.Location = new System.Drawing.Point(0, 261);
            this.smartSpliterControl2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl2.Name = "smartSpliterControl2";
            this.smartSpliterControl2.Size = new System.Drawing.Size(1071, 5);
            this.smartSpliterControl2.TabIndex = 15;
            this.smartSpliterControl2.TabStop = false;
            // 
            // usInspectionResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdInspect);
            this.Controls.Add(this.smartSpliterControl2);
            this.Controls.Add(this.defectSpliterContainer);
            this.Name = "usInspectionResult";
            this.Size = new System.Drawing.Size(1071, 532);
            this.pnlDefectFile.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.defectSpliterContainer)).EndInit();
            this.defectSpliterContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlDefectFile;
        private Framework.SmartControls.SmartPictureEdit picBox;
        private Framework.SmartControls.SmartBandedGrid grdFile;
        private Framework.SmartControls.SmartBandedGrid grdInspect;
        private Framework.SmartControls.SmartSpliterContainer defectSpliterContainer;
        private Framework.SmartControls.SmartBandedGrid grdInspDefect;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl2;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
    }
}
