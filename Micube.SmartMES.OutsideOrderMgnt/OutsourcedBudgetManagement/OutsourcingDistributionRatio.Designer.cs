namespace Micube.SmartMES.OutsideOrderMgnt
{
    partial class OutsourcingDistributionRatio
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
            this.dtpToYm = new Micube.Framework.SmartControls.SmartDateEdit();
            this.smartLayoutControl1 = new Micube.Framework.SmartControls.SmartLayoutControl();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdLeft = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdRight = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.dtpFromYm = new Micube.Framework.SmartControls.SmartDateEdit();
            this.btnOk = new Micube.Framework.SmartControls.SmartButton();
            this.smartLayoutControlGroup1 = new Micube.Framework.SmartControls.SmartLayoutControlGroup();
            this.layoutOk = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutTo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutFrom = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutEmpty = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnSaveTot = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToYm.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToYm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControl1)).BeginInit();
            this.smartLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromYm.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromYm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutOk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutEmpty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 906);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnSaveTot);
            this.pnlToolbar.Size = new System.Drawing.Size(919, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.btnSaveTot, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartLayoutControl1);
            this.pnlContent.Size = new System.Drawing.Size(919, 910);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1224, 939);
            // 
            // dtpToYm
            // 
            this.dtpToYm.EditValue = null;
            this.dtpToYm.LabelText = null;
            this.dtpToYm.LanguageKey = null;
            this.dtpToYm.Location = new System.Drawing.Point(680, 2);
            this.dtpToYm.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.dtpToYm.Name = "dtpToYm";
            this.dtpToYm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpToYm.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpToYm.Properties.DisplayFormat.FormatString = "yyyy-MM";
            this.dtpToYm.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpToYm.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dtpToYm.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpToYm.Properties.Mask.EditMask = "yyyy-MM";
            this.dtpToYm.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dtpToYm.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
            this.dtpToYm.Size = new System.Drawing.Size(133, 20);
            this.dtpToYm.StyleController = this.smartLayoutControl1;
            this.dtpToYm.TabIndex = 18;
            // 
            // smartLayoutControl1
            // 
            this.smartLayoutControl1.Controls.Add(this.smartSpliterContainer1);
            this.smartLayoutControl1.Controls.Add(this.dtpFromYm);
            this.smartLayoutControl1.Controls.Add(this.dtpToYm);
            this.smartLayoutControl1.Controls.Add(this.btnOk);
            this.smartLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.smartLayoutControl1.Name = "smartLayoutControl1";
            this.smartLayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1128, 78, 650, 400);
            this.smartLayoutControl1.Root = this.smartLayoutControlGroup1;
            this.smartLayoutControl1.Size = new System.Drawing.Size(919, 910);
            this.smartLayoutControl1.TabIndex = 1;
            this.smartLayoutControl1.Text = "smartLayoutControl1";
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Location = new System.Drawing.Point(2, 28);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdLeft);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdRight);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(915, 880);
            this.smartSpliterContainer1.SplitterPosition = 800;
            this.smartSpliterContainer1.TabIndex = 117;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdLeft
            // 
            this.grdLeft.Caption = "";
            this.grdLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLeft.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLeft.IsUsePaging = false;
            this.grdLeft.LanguageKey = "LISTOFPROCESSESAPPLYINGRATIO";
            this.grdLeft.Location = new System.Drawing.Point(0, 0);
            this.grdLeft.Margin = new System.Windows.Forms.Padding(0);
            this.grdLeft.Name = "grdLeft";
            this.grdLeft.ShowBorder = true;
            this.grdLeft.Size = new System.Drawing.Size(800, 880);
            this.grdLeft.TabIndex = 115;
            this.grdLeft.UseAutoBestFitColumns = false;
            // 
            // grdRight
            // 
            this.grdRight.Caption = "";
            this.grdRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRight.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdRight.IsUsePaging = false;
            this.grdRight.LanguageKey = "DISTRIBUTIONRATIOBYPARTNER";
            this.grdRight.Location = new System.Drawing.Point(0, 0);
            this.grdRight.Margin = new System.Windows.Forms.Padding(0);
            this.grdRight.Name = "grdRight";
            this.grdRight.ShowBorder = true;
            this.grdRight.Size = new System.Drawing.Size(110, 880);
            this.grdRight.TabIndex = 116;
            this.grdRight.UseAutoBestFitColumns = false;
            // 
            // dtpFromYm
            // 
            this.dtpFromYm.EditValue = null;
            this.dtpFromYm.LabelText = null;
            this.dtpFromYm.LanguageKey = null;
            this.dtpFromYm.Location = new System.Drawing.Point(480, 2);
            this.dtpFromYm.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.dtpFromYm.Name = "dtpFromYm";
            this.dtpFromYm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFromYm.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFromYm.Properties.DisplayFormat.FormatString = "yyyy-MM";
            this.dtpFromYm.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpFromYm.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dtpFromYm.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpFromYm.Properties.Mask.EditMask = "yyyy-MM";
            this.dtpFromYm.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dtpFromYm.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
            this.dtpFromYm.Size = new System.Drawing.Size(133, 20);
            this.dtpFromYm.StyleController = this.smartLayoutControl1;
            this.dtpFromYm.TabIndex = 17;
            // 
            // btnOk
            // 
            this.btnOk.AllowFocus = false;
            this.btnOk.IsBusy = false;
            this.btnOk.IsWrite = false;
            this.btnOk.LanguageKey = "OK";
            this.btnOk.Location = new System.Drawing.Point(817, 2);
            this.btnOk.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnOk.Name = "btnOk";
            this.btnOk.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnOk.Size = new System.Drawing.Size(100, 22);
            this.btnOk.StyleController = this.smartLayoutControl1;
            this.btnOk.TabIndex = 116;
            this.btnOk.Text = "smartButton1";
            this.btnOk.TooltipLanguageKey = "";
            // 
            // smartLayoutControlGroup1
            // 
            this.smartLayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.smartLayoutControlGroup1.GroupBordersVisible = false;
            this.smartLayoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutOk,
            this.layoutTo,
            this.layoutFrom,
            this.layoutEmpty,
            this.layoutControlItem3});
            this.smartLayoutControlGroup1.Name = "Root";
            this.smartLayoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.smartLayoutControlGroup1.Size = new System.Drawing.Size(919, 910);
            this.smartLayoutControlGroup1.TextVisible = false;
            // 
            // layoutOk
            // 
            this.layoutOk.Control = this.btnOk;
            this.layoutOk.Location = new System.Drawing.Point(815, 0);
            this.layoutOk.MaxSize = new System.Drawing.Size(104, 26);
            this.layoutOk.MinSize = new System.Drawing.Size(104, 26);
            this.layoutOk.Name = "layoutOk";
            this.layoutOk.Size = new System.Drawing.Size(104, 26);
            this.layoutOk.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutOk.TextSize = new System.Drawing.Size(0, 0);
            this.layoutOk.TextVisible = false;
            this.layoutOk.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutTo
            // 
            this.layoutTo.Control = this.dtpToYm;
            this.smartLayoutControl1.SetLanguageKey(this.layoutTo, "TOYEARMONTH");
            this.layoutTo.Location = new System.Drawing.Point(615, 0);
            this.layoutTo.MaxSize = new System.Drawing.Size(200, 24);
            this.layoutTo.MinSize = new System.Drawing.Size(200, 24);
            this.layoutTo.Name = "layoutTo";
            this.layoutTo.Size = new System.Drawing.Size(200, 26);
            this.layoutTo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutTo.TextSize = new System.Drawing.Size(60, 14);
            this.layoutTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutFrom
            // 
            this.layoutFrom.Control = this.dtpFromYm;
            this.smartLayoutControl1.SetLanguageKey(this.layoutFrom, "FROMYEARMONTH");
            this.layoutFrom.Location = new System.Drawing.Point(415, 0);
            this.layoutFrom.MaxSize = new System.Drawing.Size(200, 24);
            this.layoutFrom.MinSize = new System.Drawing.Size(200, 24);
            this.layoutFrom.Name = "layoutFrom";
            this.layoutFrom.Size = new System.Drawing.Size(200, 26);
            this.layoutFrom.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutFrom.TextSize = new System.Drawing.Size(60, 14);
            this.layoutFrom.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutEmpty
            // 
            this.layoutEmpty.AllowHotTrack = false;
            this.layoutEmpty.Location = new System.Drawing.Point(0, 0);
            this.layoutEmpty.Name = "layoutEmpty";
            this.layoutEmpty.Size = new System.Drawing.Size(415, 26);
            this.layoutEmpty.TextSize = new System.Drawing.Size(0, 0);
            this.layoutEmpty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.smartSpliterContainer1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(919, 884);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // btnSaveTot
            // 
            this.btnSaveTot.AllowFocus = false;
            this.btnSaveTot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveTot.IsBusy = false;
            this.btnSaveTot.IsWrite = true;
            this.btnSaveTot.LanguageKey = "SAVE";
            this.btnSaveTot.Location = new System.Drawing.Point(2235, 1);
            this.btnSaveTot.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.btnSaveTot.Name = "btnSaveTot";
            this.btnSaveTot.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSaveTot.Size = new System.Drawing.Size(80, 25);
            this.btnSaveTot.TabIndex = 111;
            this.btnSaveTot.Text = "저장";
            this.btnSaveTot.TooltipLanguageKey = "";
            this.btnSaveTot.Visible = false;
            // 
            // OutsourcingDistributionRatio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 977);
            this.Name = "OutsourcingDistributionRatio";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToYm.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToYm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControl1)).EndInit();
            this.smartLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromYm.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromYm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutOk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutEmpty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Framework.SmartControls.SmartDateEdit dtpToYm;
        private Framework.SmartControls.SmartDateEdit dtpFromYm;
        private Framework.SmartControls.SmartButton btnSaveTot;
        private Framework.SmartControls.SmartLayoutControl smartLayoutControl1;
        private Framework.SmartControls.SmartButton btnOk;
        private Framework.SmartControls.SmartLayoutControlGroup smartLayoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutOk;
        private DevExpress.XtraLayout.LayoutControlItem layoutTo;
        private DevExpress.XtraLayout.LayoutControlItem layoutFrom;
        private DevExpress.XtraLayout.EmptySpaceItem layoutEmpty;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdLeft;
        private Framework.SmartControls.SmartBandedGrid grdRight;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}