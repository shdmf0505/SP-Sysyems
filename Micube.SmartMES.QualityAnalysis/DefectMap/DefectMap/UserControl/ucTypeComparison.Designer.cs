namespace Micube.SmartMES.QualityAnalysis
{
    partial class ucTypeComparison
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
            this.smartLayoutControl1 = new Micube.Framework.SmartControls.SmartLayoutControl();
            this.layoutMain = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grpLeft = new Micube.Framework.SmartControls.SmartGroupBox();
            this.grpRight = new Micube.Framework.SmartControls.SmartGroupBox();
            this.smartLayoutControl2 = new Micube.Framework.SmartControls.SmartLayoutControl();
            this.spcSheet = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdLeft = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdRight = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.chartMain = new Micube.Framework.SmartControls.SmartChart();
            this.smartLayoutControlGroup2 = new Micube.Framework.SmartControls.SmartLayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.smartSpliterControl2 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.btnRun = new Micube.Framework.SmartControls.SmartButton();
            this.cboRightType = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.cboLeftType = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.smartLayoutControlGroup1 = new Micube.Framework.SmartControls.SmartLayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControl1)).BeginInit();
            this.smartLayoutControl1.SuspendLayout();
            this.layoutMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControl2)).BeginInit();
            this.smartLayoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcSheet)).BeginInit();
            this.spcSheet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboRightType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLeftType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // smartLayoutControl1
            // 
            this.smartLayoutControl1.Controls.Add(this.layoutMain);
            this.smartLayoutControl1.Controls.Add(this.btnRun);
            this.smartLayoutControl1.Controls.Add(this.cboRightType);
            this.smartLayoutControl1.Controls.Add(this.cboLeftType);
            this.smartLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.smartLayoutControl1.Margin = new System.Windows.Forms.Padding(0);
            this.smartLayoutControl1.Name = "smartLayoutControl1";
            this.smartLayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1104, 325, 650, 400);
            this.smartLayoutControl1.Root = this.smartLayoutControlGroup1;
            this.smartLayoutControl1.Size = new System.Drawing.Size(1000, 600);
            this.smartLayoutControl1.TabIndex = 0;
            this.smartLayoutControl1.Text = "smartLayoutControl1";
            // 
            // layoutMain
            // 
            this.layoutMain.ColumnCount = 3;
            this.layoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.layoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutMain.Controls.Add(this.grpLeft, 0, 2);
            this.layoutMain.Controls.Add(this.grpRight, 2, 2);
            this.layoutMain.Controls.Add(this.smartLayoutControl2, 0, 0);
            this.layoutMain.Controls.Add(this.smartSpliterControl1, 1, 2);
            this.layoutMain.Controls.Add(this.smartSpliterControl2, 0, 1);
            this.layoutMain.Location = new System.Drawing.Point(2, 28);
            this.layoutMain.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.layoutMain.Name = "layoutMain";
            this.layoutMain.RowCount = 3;
            this.layoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.layoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.layoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.layoutMain.Size = new System.Drawing.Size(996, 570);
            this.layoutMain.TabIndex = 7;
            // 
            // grpLeft
            // 
            this.grpLeft.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.grpLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpLeft.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.grpLeft.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.grpLeft.Location = new System.Drawing.Point(0, 234);
            this.grpLeft.Margin = new System.Windows.Forms.Padding(0);
            this.grpLeft.Name = "grpLeft";
            this.grpLeft.ShowBorder = true;
            this.grpLeft.Size = new System.Drawing.Size(493, 336);
            this.grpLeft.TabIndex = 0;
            this.grpLeft.Text = "smartGroupBox1";
            // 
            // grpRight
            // 
            this.grpRight.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.grpRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpRight.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.grpRight.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.grpRight.Location = new System.Drawing.Point(503, 234);
            this.grpRight.Margin = new System.Windows.Forms.Padding(0);
            this.grpRight.Name = "grpRight";
            this.grpRight.ShowBorder = true;
            this.grpRight.Size = new System.Drawing.Size(493, 336);
            this.grpRight.TabIndex = 1;
            this.grpRight.Text = "smartGroupBox2";
            // 
            // smartLayoutControl2
            // 
            this.layoutMain.SetColumnSpan(this.smartLayoutControl2, 3);
            this.smartLayoutControl2.Controls.Add(this.spcSheet);
            this.smartLayoutControl2.Controls.Add(this.chartMain);
            this.smartLayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLayoutControl2.Location = new System.Drawing.Point(0, 0);
            this.smartLayoutControl2.Margin = new System.Windows.Forms.Padding(0);
            this.smartLayoutControl2.Name = "smartLayoutControl2";
            this.smartLayoutControl2.Root = this.smartLayoutControlGroup2;
            this.smartLayoutControl2.Size = new System.Drawing.Size(996, 224);
            this.smartLayoutControl2.TabIndex = 2;
            this.smartLayoutControl2.Text = "smartLayoutControl2";
            // 
            // spcSheet
            // 
            this.spcSheet.Location = new System.Drawing.Point(219, 0);
            this.spcSheet.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.spcSheet.Name = "spcSheet";
            this.spcSheet.Panel1.Controls.Add(this.grdLeft);
            this.spcSheet.Panel1.Text = "Panel1";
            this.spcSheet.Panel2.Controls.Add(this.grdRight);
            this.spcSheet.Panel2.Text = "Panel2";
            this.spcSheet.Size = new System.Drawing.Size(777, 224);
            this.spcSheet.SplitterPosition = 670;
            this.spcSheet.TabIndex = 7;
            this.spcSheet.Text = "smartSpliterContainer";
            // 
            // grdLeft
            // 
            this.grdLeft.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLeft.IsUsePaging = false;
            this.grdLeft.LanguageKey = null;
            this.grdLeft.Location = new System.Drawing.Point(0, 0);
            this.grdLeft.Margin = new System.Windows.Forms.Padding(0);
            this.grdLeft.Name = "grdLeft";
            this.grdLeft.ShowBorder = true;
            this.grdLeft.Size = new System.Drawing.Size(670, 224);
            this.grdLeft.TabIndex = 6;
            // 
            // grdRight
            // 
            this.grdRight.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRight.IsUsePaging = false;
            this.grdRight.LanguageKey = null;
            this.grdRight.Location = new System.Drawing.Point(0, 0);
            this.grdRight.Margin = new System.Windows.Forms.Padding(0);
            this.grdRight.Name = "grdRight";
            this.grdRight.ShowBorder = true;
            this.grdRight.Size = new System.Drawing.Size(102, 224);
            this.grdRight.TabIndex = 7;
            // 
            // chartMain
            // 
            this.chartMain.AutoLayout = false;
            this.chartMain.CacheToMemory = true;
            this.chartMain.Legend.Name = "Default Legend";
            this.chartMain.Location = new System.Drawing.Point(0, 0);
            this.chartMain.Name = "chartMain";
            this.chartMain.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartMain.Size = new System.Drawing.Size(215, 224);
            this.chartMain.TabIndex = 4;
            // 
            // smartLayoutControlGroup2
            // 
            this.smartLayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.smartLayoutControlGroup2.GroupBordersVisible = false;
            this.smartLayoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem8});
            this.smartLayoutControlGroup2.Name = "smartLayoutControlGroup2";
            this.smartLayoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.smartLayoutControlGroup2.Size = new System.Drawing.Size(996, 224);
            this.smartLayoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.chartMain;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(219, 0);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(219, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 4, 0, 0);
            this.layoutControlItem5.Size = new System.Drawing.Size(219, 224);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.spcSheet;
            this.layoutControlItem8.Location = new System.Drawing.Point(219, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem8.Size = new System.Drawing.Size(777, 224);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // smartSpliterControl2
            // 
            this.layoutMain.SetColumnSpan(this.smartSpliterControl2, 3);
            this.smartSpliterControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartSpliterControl2.Location = new System.Drawing.Point(0, 224);
            this.smartSpliterControl2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl2.Name = "smartSpliterControl2";
            this.smartSpliterControl2.Size = new System.Drawing.Size(996, 5);
            this.smartSpliterControl2.TabIndex = 4;
            this.smartSpliterControl2.TabStop = false;
            // 
            // btnRun
            // 
            this.btnRun.AllowFocus = false;
            this.btnRun.IsBusy = false;
            this.btnRun.Location = new System.Drawing.Point(923, 2);
            this.btnRun.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnRun.Name = "btnRun";
            this.btnRun.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnRun.Size = new System.Drawing.Size(75, 22);
            this.btnRun.StyleController = this.smartLayoutControl1;
            this.btnRun.TabIndex = 6;
            this.btnRun.Text = "Run";
            this.btnRun.TooltipLanguageKey = "";
            // 
            // cboRightType
            // 
            this.cboRightType.LanguageKey = null;
            this.cboRightType.Location = new System.Drawing.Point(184, 2);
            this.cboRightType.Name = "cboRightType";
            this.cboRightType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboRightType.Properties.NullText = "";
            this.cboRightType.Size = new System.Drawing.Size(178, 20);
            this.cboRightType.TabIndex = 5;
            // 
            // cboLeftType
            // 
            this.cboLeftType.LanguageKey = null;
            this.cboLeftType.Location = new System.Drawing.Point(2, 2);
            this.cboLeftType.Name = "cboLeftType";
            this.cboLeftType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboLeftType.Properties.NullText = "";
            this.cboLeftType.Size = new System.Drawing.Size(178, 20);
            this.cboLeftType.TabIndex = 4;
            // 
            // smartLayoutControlGroup1
            // 
            this.smartLayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.smartLayoutControlGroup1.GroupBordersVisible = false;
            this.smartLayoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.smartLayoutControlGroup1.Name = "Root";
            this.smartLayoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.smartLayoutControlGroup1.Size = new System.Drawing.Size(1000, 600);
            this.smartLayoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cboLeftType;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(182, 23);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(182, 23);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(182, 26);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cboRightType;
            this.layoutControlItem2.Location = new System.Drawing.Point(182, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(182, 23);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(182, 23);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(182, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(364, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(557, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnRun;
            this.layoutControlItem3.Location = new System.Drawing.Point(921, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(79, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(79, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(79, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.layoutMain;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1000, 574);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // smartSpliterControl1
            // 
            this.smartSpliterControl1.Location = new System.Drawing.Point(493, 234);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(5, 336);
            this.smartSpliterControl1.TabIndex = 3;
            this.smartSpliterControl1.TabStop = false;
            // 
            // ucTypeComparison
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.smartLayoutControl1);
            this.Name = "ucTypeComparison";
            this.Size = new System.Drawing.Size(1000, 600);
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControl1)).EndInit();
            this.smartLayoutControl1.ResumeLayout(false);
            this.layoutMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControl2)).EndInit();
            this.smartLayoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcSheet)).EndInit();
            this.spcSheet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboRightType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLeftType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartLayoutControl smartLayoutControl1;
        private Framework.SmartControls.SmartLayoutControlGroup smartLayoutControlGroup1;
        private Framework.SmartControls.SmartLabelComboBox cboRightType;
        private Framework.SmartControls.SmartLabelComboBox cboLeftType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private Framework.SmartControls.SmartButton btnRun;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private Framework.SmartControls.SmartSplitTableLayoutPanel layoutMain;
        private Framework.SmartControls.SmartGroupBox grpLeft;
        private Framework.SmartControls.SmartGroupBox grpRight;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private Framework.SmartControls.SmartLayoutControl smartLayoutControl2;
        private Framework.SmartControls.SmartChart chartMain;
        private Framework.SmartControls.SmartLayoutControlGroup smartLayoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl2;
        private Framework.SmartControls.SmartSpliterContainer spcSheet;
        private Framework.SmartControls.SmartBandedGrid grdLeft;
        private Framework.SmartControls.SmartBandedGrid grdRight;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
    }
}
