namespace Micube.SmartMES.QualityAnalysis
{
    partial class ucAOIDiagram
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
            this.components = new System.ComponentModel.Container();
            this.layoutMain = new DevExpress.XtraLayout.LayoutControl();
            this.diagramControlMap = new Micube.Framework.SmartControls.SmartDiagramControl();
            this.chkMain = new Micube.Framework.SmartControls.SmartCheckBox();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemCheckBox = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemMain = new DevExpress.XtraLayout.LayoutControlItem();
            this.colorPickMain = new DevExpress.XtraEditors.ColorPickEdit();
            this.layoutControlItemColorBox = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutMain)).BeginInit();
            this.layoutMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.diagramControlMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkMain.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCheckBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorPickMain.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemColorBox)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutMain
            // 
            this.layoutMain.Controls.Add(this.colorPickMain);
            this.layoutMain.Controls.Add(this.diagramControlMap);
            this.layoutMain.Controls.Add(this.chkMain);
            this.layoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutMain.Location = new System.Drawing.Point(0, 0);
            this.layoutMain.Margin = new System.Windows.Forms.Padding(0);
            this.layoutMain.Name = "layoutMain";
            this.layoutMain.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(569, 167, 650, 400);
            this.layoutMain.Root = this.layoutControlGroup1;
            this.layoutMain.Size = new System.Drawing.Size(300, 300);
            this.layoutMain.TabIndex = 1;
            this.layoutMain.Text = "layoutControl1";
            // 
            // diagramControlMap
            // 
            this.diagramControlMap.IsReadOnly = false;
            this.diagramControlMap.Location = new System.Drawing.Point(2, 22);
            this.diagramControlMap.Margin = new System.Windows.Forms.Padding(0);
            this.diagramControlMap.Name = "diagramControlMap";
            this.diagramControlMap.OptionsBehavior.SelectedStencils = new DevExpress.Diagram.Core.StencilCollection(new string[] {
            "BasicShapes",
            "BasicFlowchartShapes"});
            this.diagramControlMap.OptionsView.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.diagramControlMap.Size = new System.Drawing.Size(296, 276);
            this.diagramControlMap.StyleController = this.layoutMain;
            this.diagramControlMap.TabIndex = 5;
            // 
            // chkMain
            // 
            this.chkMain.Location = new System.Drawing.Point(2, 0);
            this.chkMain.Margin = new System.Windows.Forms.Padding(0);
            this.chkMain.Name = "chkMain";
            this.chkMain.Properties.Appearance.Options.UseTextOptions = true;
            this.chkMain.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.chkMain.Properties.Caption = "Main";
            this.chkMain.Size = new System.Drawing.Size(248, 19);
            this.chkMain.StyleController = this.layoutMain;
            this.chkMain.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemCheckBox,
            this.layoutControlItemMain,
            this.layoutControlItemColorBox});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(300, 300);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItemCheckBox
            // 
            this.layoutControlItemCheckBox.Control = this.chkMain;
            this.layoutControlItemCheckBox.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemCheckBox.Name = "layoutControlItemCheckBox";
            this.layoutControlItemCheckBox.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 0, 0);
            this.layoutControlItemCheckBox.Size = new System.Drawing.Size(250, 20);
            this.layoutControlItemCheckBox.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemCheckBox.TextVisible = false;
            // 
            // layoutControlItemMain
            // 
            this.layoutControlItemMain.Control = this.diagramControlMap;
            this.layoutControlItemMain.Location = new System.Drawing.Point(0, 20);
            this.layoutControlItemMain.Name = "layoutControlItemMain";
            this.layoutControlItemMain.Size = new System.Drawing.Size(300, 280);
            this.layoutControlItemMain.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemMain.TextVisible = false;
            // 
            // colorPickMain
            // 
            this.colorPickMain.EditValue = System.Drawing.Color.Empty;
            this.colorPickMain.Location = new System.Drawing.Point(250, 0);
            this.colorPickMain.Margin = new System.Windows.Forms.Padding(0);
            this.colorPickMain.Name = "colorPickMain";
            this.colorPickMain.Properties.AutomaticColor = System.Drawing.Color.Black;
            this.colorPickMain.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.colorPickMain.Size = new System.Drawing.Size(50, 20);
            this.colorPickMain.StyleController = this.layoutMain;
            this.colorPickMain.TabIndex = 6;
            // 
            // layoutControlItemColorBox
            // 
            this.layoutControlItemColorBox.Control = this.colorPickMain;
            this.layoutControlItemColorBox.Location = new System.Drawing.Point(250, 0);
            this.layoutControlItemColorBox.MaxSize = new System.Drawing.Size(50, 20);
            this.layoutControlItemColorBox.MinSize = new System.Drawing.Size(50, 20);
            this.layoutControlItemColorBox.Name = "layoutControlItemColorBox";
            this.layoutControlItemColorBox.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItemColorBox.Size = new System.Drawing.Size(50, 20);
            this.layoutControlItemColorBox.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemColorBox.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemColorBox.TextVisible = false;
            // 
            // ucAOIDiagram
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.layoutMain);
            this.Name = "ucAOIDiagram";
            this.Size = new System.Drawing.Size(300, 300);
            ((System.ComponentModel.ISupportInitialize)(this.layoutMain)).EndInit();
            this.layoutMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.diagramControlMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkMain.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCheckBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorPickMain.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemColorBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutMain;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCheckBox;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemMain;
        private Framework.SmartControls.SmartCheckBox chkMain;
        private Framework.SmartControls.SmartDiagramControl diagramControlMap;
        private DevExpress.XtraEditors.ColorPickEdit colorPickMain;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemColorBox;
    }
}
