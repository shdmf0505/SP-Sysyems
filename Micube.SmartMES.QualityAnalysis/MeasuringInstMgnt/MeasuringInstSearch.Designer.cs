namespace Micube.SmartMES.QualityAnalysis
{
    partial class MeasuringInstSearch
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MeasuringInstSearch));
            this.cboQuarterType = new Micube.Framework.SmartControls.SmartComboBox();
            this.btnMailSend = new Micube.Framework.SmartControls.SmartButton();
            this.grdMain = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.fpcRNR = new Micube.SmartMES.Commons.Controls.SmartFileProcessingControl();
            this.fpcMiddle = new Micube.SmartMES.Commons.Controls.SmartFileProcessingControl();
            this.fpcCalibration = new Micube.SmartMES.Commons.Controls.SmartFileProcessingControl();
            this.smartLayoutControl1 = new Micube.Framework.SmartControls.SmartLayoutControl();
            this.smartLayoutControlGroup1 = new Micube.Framework.SmartControls.SmartLayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboQuarterType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControl1)).BeginInit();
            this.smartLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 508);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(659, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartLayoutControl1);
            this.pnlContent.Size = new System.Drawing.Size(659, 512);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(964, 541);
            // 
            // cboQuarterType
            // 
            this.cboQuarterType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboQuarterType.LabelText = null;
            this.cboQuarterType.LanguageKey = null;
            this.cboQuarterType.Location = new System.Drawing.Point(480, 2);
            this.cboQuarterType.Name = "cboQuarterType";
            this.cboQuarterType.PopupWidth = 0;
            this.cboQuarterType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboQuarterType.Properties.NullText = "";
            this.cboQuarterType.ShowHeader = true;
            this.cboQuarterType.Size = new System.Drawing.Size(67, 20);
            this.cboQuarterType.StyleController = this.smartLayoutControl1;
            this.cboQuarterType.TabIndex = 76;
            this.cboQuarterType.VisibleColumns = null;
            this.cboQuarterType.VisibleColumnsWidth = null;
            // 
            // btnMailSend
            // 
            this.btnMailSend.AllowFocus = false;
            this.btnMailSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMailSend.IsBusy = false;
            this.btnMailSend.IsWrite = false;
            this.btnMailSend.LanguageKey = "SENDMAIL";
            this.btnMailSend.Location = new System.Drawing.Point(551, 2);
            this.btnMailSend.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnMailSend.Name = "btnMailSend";
            this.btnMailSend.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnMailSend.Size = new System.Drawing.Size(106, 22);
            this.btnMailSend.StyleController = this.smartLayoutControl1;
            this.btnMailSend.TabIndex = 3;
            this.btnMailSend.Text = "메일발송";
            this.btnMailSend.TooltipLanguageKey = "";
            // 
            // grdMain
            // 
            this.grdMain.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdMain.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdMain.IsUsePaging = false;
            this.grdMain.LanguageKey = "MEASUREMENTLIST";
            this.grdMain.Location = new System.Drawing.Point(2, 28);
            this.grdMain.Margin = new System.Windows.Forms.Padding(0);
            this.grdMain.Name = "grdMain";
            this.grdMain.ShowBorder = true;
            this.grdMain.Size = new System.Drawing.Size(655, 247);
            this.grdMain.TabIndex = 1;
            this.grdMain.UseAutoBestFitColumns = false;
            // 
            // fpcRNR
            // 
            this.fpcRNR.countRows = false;
            this.fpcRNR.executeFileAfterDown = false;
            this.fpcRNR.LanguageKey = "MEASURINGRNRINFOFILE";
            this.fpcRNR.Location = new System.Drawing.Point(441, 279);
            this.fpcRNR.Margin = new System.Windows.Forms.Padding(0);
            this.fpcRNR.Name = "fpcRNR";
            this.fpcRNR.showImage = false;
            this.fpcRNR.Size = new System.Drawing.Size(216, 231);
            this.fpcRNR.TabIndex = 96;
            this.fpcRNR.UploadPath = "";
            this.fpcRNR.UseCommentsColumn = true;
            // 
            // fpcMiddle
            // 
            this.fpcMiddle.countRows = false;
            this.fpcMiddle.executeFileAfterDown = false;
            this.fpcMiddle.LanguageKey = "MEASURINGMIDDLEINFOFILE";
            this.fpcMiddle.Location = new System.Drawing.Point(222, 279);
            this.fpcMiddle.Margin = new System.Windows.Forms.Padding(0);
            this.fpcMiddle.Name = "fpcMiddle";
            this.fpcMiddle.showImage = false;
            this.fpcMiddle.Size = new System.Drawing.Size(215, 231);
            this.fpcMiddle.TabIndex = 92;
            this.fpcMiddle.UploadPath = "";
            this.fpcMiddle.UseCommentsColumn = true;
            // 
            // fpcCalibration
            // 
            this.fpcCalibration.countRows = false;
            this.fpcCalibration.executeFileAfterDown = false;
            this.fpcCalibration.LanguageKey = "MEASURINGCALIBRATIONFILE";
            this.fpcCalibration.Location = new System.Drawing.Point(2, 279);
            this.fpcCalibration.Margin = new System.Windows.Forms.Padding(0);
            this.fpcCalibration.Name = "fpcCalibration";
            this.fpcCalibration.showImage = false;
            this.fpcCalibration.Size = new System.Drawing.Size(216, 231);
            this.fpcCalibration.TabIndex = 86;
            this.fpcCalibration.UploadPath = "";
            this.fpcCalibration.UseCommentsColumn = true;
            // 
            // smartLayoutControl1
            // 
            this.smartLayoutControl1.Controls.Add(this.cboQuarterType);
            this.smartLayoutControl1.Controls.Add(this.fpcRNR);
            this.smartLayoutControl1.Controls.Add(this.btnMailSend);
            this.smartLayoutControl1.Controls.Add(this.fpcMiddle);
            this.smartLayoutControl1.Controls.Add(this.grdMain);
            this.smartLayoutControl1.Controls.Add(this.fpcCalibration);
            this.smartLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.smartLayoutControl1.Name = "smartLayoutControl1";
            this.smartLayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(878, 267, 650, 400);
            this.smartLayoutControl1.Root = this.smartLayoutControlGroup1;
            this.smartLayoutControl1.Size = new System.Drawing.Size(659, 512);
            this.smartLayoutControl1.TabIndex = 3;
            this.smartLayoutControl1.Text = "smartLayoutControl1";
            // 
            // smartLayoutControlGroup1
            // 
            this.smartLayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.smartLayoutControlGroup1.GroupBordersVisible = false;
            this.smartLayoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.emptySpaceItem1});
            this.smartLayoutControlGroup1.Name = "Root";
            this.smartLayoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.smartLayoutControlGroup1.Size = new System.Drawing.Size(659, 512);
            this.smartLayoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdMain;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(659, 251);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.fpcCalibration;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 277);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(220, 235);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.fpcMiddle;
            this.layoutControlItem3.Location = new System.Drawing.Point(220, 277);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(219, 235);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.fpcRNR;
            this.layoutControlItem4.Location = new System.Drawing.Point(439, 277);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(220, 235);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnMailSend;
            this.layoutControlItem5.Location = new System.Drawing.Point(549, 0);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(110, 26);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(110, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(110, 26);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.cboQuarterType;
            this.smartLayoutControl1.SetLanguageKey(this.layoutControlItem6, "MODE");
            this.layoutControlItem6.Location = new System.Drawing.Point(369, 0);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(180, 24);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(180, 24);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(180, 26);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(105, 14);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(369, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // MeasuringInstSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Name = "MeasuringInstSearch";
            this.Text = "MeasuringInstSearch";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboQuarterType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControl1)).EndInit();
            this.smartLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Framework.SmartControls.SmartButton btnMailSend;
        private Framework.SmartControls.SmartComboBox cboQuarterType;
        private Framework.SmartControls.SmartBandedGrid grdMain;
        private Commons.Controls.SmartFileProcessingControl fpcCalibration;
        private Commons.Controls.SmartFileProcessingControl fpcMiddle;
        private Commons.Controls.SmartFileProcessingControl fpcRNR;
        private Framework.SmartControls.SmartLayoutControl smartLayoutControl1;
        private Framework.SmartControls.SmartLayoutControlGroup smartLayoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}