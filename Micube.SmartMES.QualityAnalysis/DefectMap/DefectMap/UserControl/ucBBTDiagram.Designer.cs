namespace Micube.SmartMES.QualityAnalysis
{
    partial class ucBBTDiagram
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.grdControlMain = new DevExpress.XtraGrid.GridControl();
            this.grdViewMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.chkMain = new Micube.Framework.SmartControls.SmartCheckBox();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemCheckBox = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdControlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdViewMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkMain.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCheckBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.grdControlMain);
            this.layoutControl1.Controls.Add(this.chkMain);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(569, 167, 650, 400);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(298, 298);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // grdControlMain
            // 
            this.grdControlMain.Location = new System.Drawing.Point(2, 21);
            this.grdControlMain.MainView = this.grdViewMain;
            this.grdControlMain.Name = "grdControlMain";
            this.grdControlMain.Size = new System.Drawing.Size(294, 275);
            this.grdControlMain.TabIndex = 10;
            this.grdControlMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdViewMain});
            // 
            // grdViewMain
            // 
            this.grdViewMain.GridControl = this.grdControlMain;
            this.grdViewMain.Name = "grdViewMain";
            // 
            // chkMain
            // 
            this.chkMain.Location = new System.Drawing.Point(2, 0);
            this.chkMain.Margin = new System.Windows.Forms.Padding(0);
            this.chkMain.Name = "chkMain";
            this.chkMain.Properties.Appearance.Options.UseTextOptions = true;
            this.chkMain.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.chkMain.Properties.Caption = "Main";
            this.chkMain.Size = new System.Drawing.Size(296, 19);
            this.chkMain.StyleController = this.layoutControl1;
            this.chkMain.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemCheckBox,
            this.layoutControlItem1});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(298, 298);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItemCheckBox
            // 
            this.layoutControlItemCheckBox.Control = this.chkMain;
            this.layoutControlItemCheckBox.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemCheckBox.Name = "layoutControlItemCheckBox";
            this.layoutControlItemCheckBox.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 0, 0);
            this.layoutControlItemCheckBox.Size = new System.Drawing.Size(298, 19);
            this.layoutControlItemCheckBox.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemCheckBox.TextVisible = false;
            this.layoutControlItemCheckBox.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdControlMain;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 19);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(298, 279);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // ucBBTDiagram
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ucBBTDiagram";
            this.Size = new System.Drawing.Size(298, 298);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdControlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdViewMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkMain.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCheckBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;        
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCheckBox;
        private Framework.SmartControls.SmartCheckBox chkMain;
        private DevExpress.XtraGrid.GridControl grdControlMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdViewMain;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}
