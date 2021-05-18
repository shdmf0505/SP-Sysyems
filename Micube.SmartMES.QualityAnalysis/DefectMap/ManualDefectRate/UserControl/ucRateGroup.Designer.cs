namespace Micube.SmartMES.QualityAnalysis
{
    partial class ucRateGroup
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
            this.layoutSheet = new Micube.Framework.SmartControls.SmartLayoutControl();
            this.flowList = new System.Windows.Forms.FlowLayoutPanel();
            this.cboDefectGroup = new Micube.Framework.SmartControls.SmartCheckedComboBox();
            this.cboDefectSub = new Micube.Framework.SmartControls.SmartCheckedComboBox();
            this.btnFilter = new Micube.Framework.SmartControls.SmartButton();
            this.smartLayoutControlGroup1 = new Micube.Framework.SmartControls.SmartLayoutControlGroup();
            this.layoutFilter = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutDefectSub = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutDefectGroup = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutSheet)).BeginInit();
            this.layoutSheet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboDefectGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDefectSub.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDefectSub)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDefectGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutSheet
            // 
            this.layoutSheet.Controls.Add(this.flowList);
            this.layoutSheet.Controls.Add(this.cboDefectGroup);
            this.layoutSheet.Controls.Add(this.cboDefectSub);
            this.layoutSheet.Controls.Add(this.btnFilter);
            this.layoutSheet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutSheet.Location = new System.Drawing.Point(0, 0);
            this.layoutSheet.Name = "layoutSheet";
            this.layoutSheet.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1168, 521, 650, 400);
            this.layoutSheet.Root = this.smartLayoutControlGroup1;
            this.layoutSheet.Size = new System.Drawing.Size(800, 800);
            this.layoutSheet.TabIndex = 0;
            this.layoutSheet.Text = "smartLayoutControl1";
            // 
            // flowList
            // 
            this.flowList.Location = new System.Drawing.Point(2, 33);
            this.flowList.Name = "flowList";
            this.flowList.Size = new System.Drawing.Size(796, 765);
            this.flowList.TabIndex = 7;
            // 
            // cboDefectGroup
            // 
            this.cboDefectGroup.DisplayMember = null;
            this.cboDefectGroup.LabelText = null;
            this.cboDefectGroup.LanguageKey = null;
            this.cboDefectGroup.Location = new System.Drawing.Point(72, 7);
            this.cboDefectGroup.Name = "cboDefectGroup";
            this.cboDefectGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDefectGroup.Properties.DisplayMember = null;
            this.cboDefectGroup.Properties.ShowHeader = true;
            this.cboDefectGroup.Properties.ValueMember = null;
            this.cboDefectGroup.ShowHeader = true;
            this.cboDefectGroup.Size = new System.Drawing.Size(113, 20);
            this.cboDefectGroup.StyleController = this.layoutSheet;
            this.cboDefectGroup.TabIndex = 6;
            this.cboDefectGroup.ValueMember = null;
            // 
            // cboDefectSub
            // 
            this.cboDefectSub.DisplayMember = null;
            this.cboDefectSub.LabelText = null;
            this.cboDefectSub.LanguageKey = null;
            this.cboDefectSub.Location = new System.Drawing.Point(254, 7);
            this.cboDefectSub.Name = "cboDefectSub";
            this.cboDefectSub.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDefectSub.Properties.DisplayMember = null;
            this.cboDefectSub.Properties.ShowHeader = true;
            this.cboDefectSub.Properties.ValueMember = null;
            this.cboDefectSub.ShowHeader = true;
            this.cboDefectSub.Size = new System.Drawing.Size(113, 20);
            this.cboDefectSub.StyleController = this.layoutSheet;
            this.cboDefectSub.TabIndex = 5;
            this.cboDefectSub.ValueMember = null;
            // 
            // btnFilter
            // 
            this.btnFilter.AllowFocus = false;
            this.btnFilter.IsBusy = false;
            this.btnFilter.IsWrite = false;
            this.btnFilter.Location = new System.Drawing.Point(371, 7);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnFilter.Size = new System.Drawing.Size(75, 22);
            this.btnFilter.StyleController = this.layoutSheet;
            this.btnFilter.TabIndex = 4;
            this.btnFilter.Text = "Filter";
            this.btnFilter.TooltipLanguageKey = "";
            // 
            // smartLayoutControlGroup1
            // 
            this.smartLayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.smartLayoutControlGroup1.GroupBordersVisible = false;
            this.smartLayoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutFilter,
            this.layoutDefectSub,
            this.layoutDefectGroup,
            this.layoutControlItem1,
            this.emptySpaceItem2,
            this.emptySpaceItem1,
            this.emptySpaceItem3});
            this.smartLayoutControlGroup1.Name = "Root";
            this.smartLayoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.smartLayoutControlGroup1.Size = new System.Drawing.Size(800, 800);
            this.smartLayoutControlGroup1.TextVisible = false;
            // 
            // layoutFilter
            // 
            this.layoutFilter.Control = this.btnFilter;
            this.layoutFilter.Location = new System.Drawing.Point(369, 5);
            this.layoutFilter.MaxSize = new System.Drawing.Size(79, 26);
            this.layoutFilter.MinSize = new System.Drawing.Size(79, 26);
            this.layoutFilter.Name = "layoutFilter";
            this.layoutFilter.Size = new System.Drawing.Size(79, 26);
            this.layoutFilter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutFilter.TextSize = new System.Drawing.Size(0, 0);
            this.layoutFilter.TextVisible = false;
            // 
            // layoutDefectSub
            // 
            this.layoutDefectSub.Control = this.cboDefectSub;
            this.layoutDefectSub.Location = new System.Drawing.Point(187, 5);
            this.layoutDefectSub.MaxSize = new System.Drawing.Size(182, 26);
            this.layoutDefectSub.MinSize = new System.Drawing.Size(182, 26);
            this.layoutDefectSub.Name = "layoutDefectSub";
            this.layoutDefectSub.Size = new System.Drawing.Size(182, 26);
            this.layoutDefectSub.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutDefectSub.Text = "서브";
            this.layoutDefectSub.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutDefectSub.TextSize = new System.Drawing.Size(60, 14);
            this.layoutDefectSub.TextToControlDistance = 5;
            // 
            // layoutDefectGroup
            // 
            this.layoutDefectGroup.Control = this.cboDefectGroup;
            this.layoutDefectGroup.Location = new System.Drawing.Point(5, 5);
            this.layoutDefectGroup.MaxSize = new System.Drawing.Size(182, 26);
            this.layoutDefectGroup.MinSize = new System.Drawing.Size(182, 26);
            this.layoutDefectGroup.Name = "layoutDefectGroup";
            this.layoutDefectGroup.Size = new System.Drawing.Size(182, 26);
            this.layoutDefectGroup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutDefectGroup.Text = "그룹";
            this.layoutDefectGroup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutDefectGroup.TextSize = new System.Drawing.Size(60, 14);
            this.layoutDefectGroup.TextToControlDistance = 5;
            this.layoutDefectGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.flowList;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 31);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(800, 769);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(453, 10);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(347, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 5);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(5, 10);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(5, 10);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(5, 26);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(10, 5);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(10, 5);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(800, 5);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // ucRateGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutSheet);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucRateGroup";
            this.Size = new System.Drawing.Size(800, 800);
            ((System.ComponentModel.ISupportInitialize)(this.layoutSheet)).EndInit();
            this.layoutSheet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboDefectGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDefectSub.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDefectSub)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDefectGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartLayoutControl layoutSheet;
        private Framework.SmartControls.SmartCheckedComboBox cboDefectGroup;
        private Framework.SmartControls.SmartCheckedComboBox cboDefectSub;
        private Framework.SmartControls.SmartButton btnFilter;
        private Framework.SmartControls.SmartLayoutControlGroup smartLayoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutFilter;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutDefectSub;
        private DevExpress.XtraLayout.LayoutControlItem layoutDefectGroup;
        private System.Windows.Forms.FlowLayoutPanel flowList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
    }
}
