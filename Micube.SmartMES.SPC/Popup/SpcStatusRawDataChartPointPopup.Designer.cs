namespace Micube.SmartMES.SPC
{
    partial class SpcStatusRawDataChartPointPopup
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
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.smartLayoutControl1 = new Micube.Framework.SmartControls.SmartLayoutControl();
            this.btnDataSummary = new Micube.Framework.SmartControls.SmartButton();
            this.grdRawData = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartLayoutControlGroup1 = new Micube.Framework.SmartControls.SmartLayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layCtrItemDataSummary = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControl1)).BeginInit();
            this.smartLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layCtrItemDataSummary)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartLayoutControl1);
            this.pnlMain.Location = new System.Drawing.Point(3, 3);
            this.pnlMain.Size = new System.Drawing.Size(833, 524);
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(714, 499);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 6, 0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(117, 23);
            this.btnClose.StyleController = this.smartLayoutControl1;
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "닫기";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // smartLayoutControl1
            // 
            this.smartLayoutControl1.Controls.Add(this.btnDataSummary);
            this.smartLayoutControl1.Controls.Add(this.btnClose);
            this.smartLayoutControl1.Controls.Add(this.grdRawData);
            this.smartLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.smartLayoutControl1.Margin = new System.Windows.Forms.Padding(0);
            this.smartLayoutControl1.Name = "smartLayoutControl1";
            this.smartLayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(610, 311, 650, 400);
            this.smartLayoutControl1.Root = this.smartLayoutControlGroup1;
            this.smartLayoutControl1.Size = new System.Drawing.Size(833, 524);
            this.smartLayoutControl1.TabIndex = 3;
            this.smartLayoutControl1.Text = "smartLayoutControl1";
            // 
            // btnDataSummary
            // 
            this.btnDataSummary.AllowFocus = false;
            this.btnDataSummary.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDataSummary.Appearance.Options.UseFont = true;
            this.btnDataSummary.IsBusy = false;
            this.btnDataSummary.IsWrite = false;
            this.btnDataSummary.LanguageKey = "SPCRAWIMRSUMMARYFALSE";
            this.btnDataSummary.Location = new System.Drawing.Point(528, 499);
            this.btnDataSummary.Margin = new System.Windows.Forms.Padding(3, 6, 0, 0);
            this.btnDataSummary.Name = "btnDataSummary";
            this.btnDataSummary.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnDataSummary.Size = new System.Drawing.Size(182, 23);
            this.btnDataSummary.StyleController = this.smartLayoutControl1;
            this.btnDataSummary.TabIndex = 4;
            this.btnDataSummary.Text = "I-MR 자료로 변환";
            this.btnDataSummary.TooltipLanguageKey = "";
            this.btnDataSummary.Visible = false;
            this.btnDataSummary.Click += new System.EventHandler(this.btnDataSummary_Click);
            // 
            // grdRawData
            // 
            this.grdRawData.Caption = "Raw Data";
            this.grdRawData.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)(((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete)));
            this.grdRawData.IsUsePaging = false;
            this.grdRawData.LanguageKey = "RAWDATA";
            this.grdRawData.Location = new System.Drawing.Point(2, 2);
            this.grdRawData.Margin = new System.Windows.Forms.Padding(0);
            this.grdRawData.Name = "grdRawData";
            this.grdRawData.ShowBorder = true;
            this.grdRawData.ShowStatusBar = false;
            this.grdRawData.Size = new System.Drawing.Size(829, 493);
            this.grdRawData.TabIndex = 2;
            this.grdRawData.UseAutoBestFitColumns = false;
            // 
            // smartLayoutControlGroup1
            // 
            this.smartLayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.smartLayoutControlGroup1.GroupBordersVisible = false;
            this.smartLayoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.layCtrItemDataSummary});
            this.smartLayoutControlGroup1.Name = "Root";
            this.smartLayoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.smartLayoutControlGroup1.Size = new System.Drawing.Size(833, 524);
            this.smartLayoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdRawData;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(833, 497);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnClose;
            this.layoutControlItem2.Location = new System.Drawing.Point(712, 497);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(121, 27);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(121, 27);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(121, 27);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 497);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(432, 27);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(432, 497);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(94, 27);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layCtrItemDataSummary
            // 
            this.layCtrItemDataSummary.Control = this.btnDataSummary;
            this.layCtrItemDataSummary.Location = new System.Drawing.Point(526, 497);
            this.layCtrItemDataSummary.MaxSize = new System.Drawing.Size(186, 27);
            this.layCtrItemDataSummary.MinSize = new System.Drawing.Size(186, 27);
            this.layCtrItemDataSummary.Name = "layCtrItemDataSummary";
            this.layCtrItemDataSummary.Size = new System.Drawing.Size(186, 27);
            this.layCtrItemDataSummary.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layCtrItemDataSummary.TextSize = new System.Drawing.Size(0, 0);
            this.layCtrItemDataSummary.TextVisible = false;
            this.layCtrItemDataSummary.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // SpcStatusRawDataChartPointPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 530);
            this.LanguageKey = "SPCROWDATAANALYSIS";
            this.Name = "SpcStatusRawDataChartPointPopup";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "RawDataChartPointPopup";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControl1)).EndInit();
            this.smartLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layCtrItemDataSummary)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Framework.SmartControls.SmartBandedGrid grdRawData;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartLayoutControl smartLayoutControl1;
        private Framework.SmartControls.SmartLayoutControlGroup smartLayoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private Framework.SmartControls.SmartButton btnDataSummary;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layCtrItemDataSummary;
    }
}