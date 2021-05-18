namespace Micube.SmartMES.SPC.UserControl
{
    partial class ucCpkFrame
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
            this.splMain = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.palLeftMain = new Micube.Framework.SmartControls.SmartPanel();
            this.chkLeftEstimate = new Micube.Framework.SmartControls.SmartCheckBox();
            this.smartGroupBox1 = new Micube.Framework.SmartControls.SmartGroupBox();
            this.btnReExecution = new Micube.Framework.SmartControls.SmartButton();
            this.lblAllRLcl = new Micube.Framework.SmartControls.SmartLabel();
            this.txtAllRLclValue = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblAllRCcl = new Micube.Framework.SmartControls.SmartLabel();
            this.txtAllRCclValue = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblAllRUcl = new Micube.Framework.SmartControls.SmartLabel();
            this.txtAllRUclValue = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblAllLcl = new Micube.Framework.SmartControls.SmartLabel();
            this.txtAllLclValue = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblAllCcl = new Micube.Framework.SmartControls.SmartLabel();
            this.txtAllCclValue = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblAllUcl = new Micube.Framework.SmartControls.SmartLabel();
            this.txtAllUclValue = new Micube.Framework.SmartControls.SmartTextBox();
            this.rdoLeftGroup = new Micube.Framework.SmartControls.SmartRadioGroup();
            this.cboLeftChartType = new Micube.Framework.SmartControls.SmartComboBox();
            this.chkAllUslLsl = new Micube.Framework.SmartControls.SmartCheckBox();
            this.splMainSub = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.ucCpkGrid01 = new Micube.SmartMES.SPC.UserControl.ucCpkGrid();
            this.grdSelectChartRawData = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).BeginInit();
            this.splMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.palLeftMain)).BeginInit();
            this.palLeftMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkLeftEstimate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).BeginInit();
            this.smartGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllRLclValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllRCclValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllRUclValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllLclValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllCclValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllUclValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoLeftGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLeftChartType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllUslLsl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splMainSub)).BeginInit();
            this.splMainSub.SuspendLayout();
            this.SuspendLayout();
            // 
            // splMain
            // 
            this.splMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splMain.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splMain.IsSplitterFixed = true;
            this.splMain.Location = new System.Drawing.Point(0, 0);
            this.splMain.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.splMain.Name = "splMain";
            this.splMain.Panel1.Controls.Add(this.palLeftMain);
            this.splMain.Panel1.Text = "Panel1";
            this.splMain.Panel2.Controls.Add(this.splMainSub);
            this.splMain.Panel2.Text = "Panel2";
            this.splMain.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            this.splMain.Size = new System.Drawing.Size(1005, 606);
            this.splMain.SplitterPosition = 150;
            this.splMain.TabIndex = 1;
            this.splMain.Text = "splMain";
            // 
            // palLeftMain
            // 
            this.palLeftMain.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.palLeftMain.Appearance.Options.UseBackColor = true;
            this.palLeftMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.palLeftMain.Controls.Add(this.chkLeftEstimate);
            this.palLeftMain.Controls.Add(this.smartGroupBox1);
            this.palLeftMain.Controls.Add(this.cboLeftChartType);
            this.palLeftMain.Controls.Add(this.chkAllUslLsl);
            this.palLeftMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.palLeftMain.Location = new System.Drawing.Point(0, 0);
            this.palLeftMain.Name = "palLeftMain";
            this.palLeftMain.Size = new System.Drawing.Size(0, 0);
            this.palLeftMain.TabIndex = 0;
            // 
            // chkLeftEstimate
            // 
            this.chkLeftEstimate.EditValue = true;
            this.chkLeftEstimate.Location = new System.Drawing.Point(14, 403);
            this.chkLeftEstimate.Name = "chkLeftEstimate";
            this.chkLeftEstimate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLeftEstimate.Properties.Appearance.Options.UseFont = true;
            this.chkLeftEstimate.Properties.Caption = "추정치";
            this.chkLeftEstimate.Size = new System.Drawing.Size(117, 23);
            this.chkLeftEstimate.TabIndex = 4;
            // 
            // smartGroupBox1
            // 
            this.smartGroupBox1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.smartGroupBox1.Appearance.Options.UseBackColor = true;
            this.smartGroupBox1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartGroupBox1.CaptionLocation = DevExpress.Utils.Locations.Top;
            this.smartGroupBox1.Controls.Add(this.btnReExecution);
            this.smartGroupBox1.Controls.Add(this.lblAllRLcl);
            this.smartGroupBox1.Controls.Add(this.txtAllRLclValue);
            this.smartGroupBox1.Controls.Add(this.lblAllRCcl);
            this.smartGroupBox1.Controls.Add(this.txtAllRCclValue);
            this.smartGroupBox1.Controls.Add(this.lblAllRUcl);
            this.smartGroupBox1.Controls.Add(this.txtAllRUclValue);
            this.smartGroupBox1.Controls.Add(this.lblAllLcl);
            this.smartGroupBox1.Controls.Add(this.txtAllLclValue);
            this.smartGroupBox1.Controls.Add(this.lblAllCcl);
            this.smartGroupBox1.Controls.Add(this.txtAllCclValue);
            this.smartGroupBox1.Controls.Add(this.lblAllUcl);
            this.smartGroupBox1.Controls.Add(this.txtAllUclValue);
            this.smartGroupBox1.Controls.Add(this.rdoLeftGroup);
            this.smartGroupBox1.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.smartGroupBox1.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.smartGroupBox1.Location = new System.Drawing.Point(8, 85);
            this.smartGroupBox1.Name = "smartGroupBox1";
            this.smartGroupBox1.ShowBorder = true;
            this.smartGroupBox1.Size = new System.Drawing.Size(135, 312);
            this.smartGroupBox1.TabIndex = 3;
            this.smartGroupBox1.Text = "Control Limit Option";
            // 
            // btnReExecution
            // 
            this.btnReExecution.AllowFocus = false;
            this.btnReExecution.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReExecution.Appearance.Options.UseFont = true;
            this.btnReExecution.IsBusy = false;
            this.btnReExecution.Location = new System.Drawing.Point(6, 274);
            this.btnReExecution.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnReExecution.Name = "btnReExecution";
            this.btnReExecution.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnReExecution.Size = new System.Drawing.Size(123, 25);
            this.btnReExecution.TabIndex = 17;
            this.btnReExecution.Text = "재분석";
            this.btnReExecution.TooltipLanguageKey = "";
            this.btnReExecution.Click += new System.EventHandler(this.btnReExecution_Click);
            // 
            // lblAllRLcl
            // 
            this.lblAllRLcl.Location = new System.Drawing.Point(6, 244);
            this.lblAllRLcl.Name = "lblAllRLcl";
            this.lblAllRLcl.Size = new System.Drawing.Size(26, 14);
            this.lblAllRLcl.TabIndex = 16;
            this.lblAllRLcl.Text = "RLCL";
            // 
            // txtAllRLclValue
            // 
            this.txtAllRLclValue.EditValue = "999,999.0123";
            this.txtAllRLclValue.LabelText = null;
            this.txtAllRLclValue.LanguageKey = null;
            this.txtAllRLclValue.Location = new System.Drawing.Point(39, 241);
            this.txtAllRLclValue.Name = "txtAllRLclValue";
            this.txtAllRLclValue.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtAllRLclValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAllRLclValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAllRLclValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtAllRLclValue.Size = new System.Drawing.Size(90, 20);
            this.txtAllRLclValue.TabIndex = 15;
            // 
            // lblAllRCcl
            // 
            this.lblAllRCcl.Location = new System.Drawing.Point(6, 221);
            this.lblAllRCcl.Name = "lblAllRCcl";
            this.lblAllRCcl.Size = new System.Drawing.Size(27, 14);
            this.lblAllRCcl.TabIndex = 14;
            this.lblAllRCcl.Text = "RCCL";
            // 
            // txtAllRCclValue
            // 
            this.txtAllRCclValue.EditValue = "999,999.0123";
            this.txtAllRCclValue.LabelText = null;
            this.txtAllRCclValue.LanguageKey = null;
            this.txtAllRCclValue.Location = new System.Drawing.Point(39, 218);
            this.txtAllRCclValue.Name = "txtAllRCclValue";
            this.txtAllRCclValue.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtAllRCclValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAllRCclValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAllRCclValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtAllRCclValue.Size = new System.Drawing.Size(90, 20);
            this.txtAllRCclValue.TabIndex = 13;
            // 
            // lblAllRUcl
            // 
            this.lblAllRUcl.Location = new System.Drawing.Point(6, 198);
            this.lblAllRUcl.Name = "lblAllRUcl";
            this.lblAllRUcl.Size = new System.Drawing.Size(28, 14);
            this.lblAllRUcl.TabIndex = 12;
            this.lblAllRUcl.Text = "RUCL";
            // 
            // txtAllRUclValue
            // 
            this.txtAllRUclValue.EditValue = "999,999.0123";
            this.txtAllRUclValue.LabelText = null;
            this.txtAllRUclValue.LanguageKey = null;
            this.txtAllRUclValue.Location = new System.Drawing.Point(39, 195);
            this.txtAllRUclValue.Name = "txtAllRUclValue";
            this.txtAllRUclValue.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtAllRUclValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAllRUclValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAllRUclValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtAllRUclValue.Size = new System.Drawing.Size(90, 20);
            this.txtAllRUclValue.TabIndex = 11;
            // 
            // lblAllLcl
            // 
            this.lblAllLcl.Location = new System.Drawing.Point(6, 152);
            this.lblAllLcl.Name = "lblAllLcl";
            this.lblAllLcl.Size = new System.Drawing.Size(19, 14);
            this.lblAllLcl.TabIndex = 8;
            this.lblAllLcl.Text = "LCL";
            // 
            // txtAllLclValue
            // 
            this.txtAllLclValue.EditValue = "999,999.0123";
            this.txtAllLclValue.LabelText = null;
            this.txtAllLclValue.LanguageKey = null;
            this.txtAllLclValue.Location = new System.Drawing.Point(39, 149);
            this.txtAllLclValue.Name = "txtAllLclValue";
            this.txtAllLclValue.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtAllLclValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAllLclValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAllLclValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtAllLclValue.Size = new System.Drawing.Size(90, 20);
            this.txtAllLclValue.TabIndex = 7;
            // 
            // lblAllCcl
            // 
            this.lblAllCcl.Location = new System.Drawing.Point(6, 129);
            this.lblAllCcl.Name = "lblAllCcl";
            this.lblAllCcl.Size = new System.Drawing.Size(20, 14);
            this.lblAllCcl.TabIndex = 6;
            this.lblAllCcl.Text = "CCL";
            // 
            // txtAllCclValue
            // 
            this.txtAllCclValue.EditValue = "999,999.0123";
            this.txtAllCclValue.LabelText = null;
            this.txtAllCclValue.LanguageKey = null;
            this.txtAllCclValue.Location = new System.Drawing.Point(39, 126);
            this.txtAllCclValue.Name = "txtAllCclValue";
            this.txtAllCclValue.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtAllCclValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAllCclValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAllCclValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtAllCclValue.Size = new System.Drawing.Size(90, 20);
            this.txtAllCclValue.TabIndex = 5;
            // 
            // lblAllUcl
            // 
            this.lblAllUcl.Location = new System.Drawing.Point(6, 106);
            this.lblAllUcl.Name = "lblAllUcl";
            this.lblAllUcl.Size = new System.Drawing.Size(21, 14);
            this.lblAllUcl.TabIndex = 4;
            this.lblAllUcl.Text = "UCL";
            // 
            // txtAllUclValue
            // 
            this.txtAllUclValue.EditValue = "999,999.0123";
            this.txtAllUclValue.LabelText = null;
            this.txtAllUclValue.LanguageKey = null;
            this.txtAllUclValue.Location = new System.Drawing.Point(39, 103);
            this.txtAllUclValue.Name = "txtAllUclValue";
            this.txtAllUclValue.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtAllUclValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAllUclValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAllUclValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtAllUclValue.Size = new System.Drawing.Size(90, 20);
            this.txtAllUclValue.TabIndex = 3;
            // 
            // rdoLeftGroup
            // 
            this.rdoLeftGroup.EditValue = 2;
            this.rdoLeftGroup.Location = new System.Drawing.Point(6, 19);
            this.rdoLeftGroup.Name = "rdoLeftGroup";
            this.rdoLeftGroup.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoLeftGroup.Properties.Appearance.Options.UseFont = true;
            this.rdoLeftGroup.Properties.Columns = 1;
            this.rdoLeftGroup.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.rdoLeftGroup.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "해석용"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "관리용"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "직접입력")});
            this.rdoLeftGroup.Size = new System.Drawing.Size(123, 73);
            this.rdoLeftGroup.TabIndex = 2;
            // 
            // cboLeftChartType
            // 
            this.cboLeftChartType.LabelText = null;
            this.cboLeftChartType.LanguageKey = null;
            this.cboLeftChartType.Location = new System.Drawing.Point(14, 24);
            this.cboLeftChartType.Name = "cboLeftChartType";
            this.cboLeftChartType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboLeftChartType.Properties.NullText = "";
            this.cboLeftChartType.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.OnlyInPopup;
            this.cboLeftChartType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cboLeftChartType.ShowHeader = true;
            this.cboLeftChartType.Size = new System.Drawing.Size(123, 20);
            this.cboLeftChartType.TabIndex = 1;
            // 
            // chkAllUslLsl
            // 
            this.chkAllUslLsl.EditValue = true;
            this.chkAllUslLsl.Location = new System.Drawing.Point(14, 50);
            this.chkAllUslLsl.Name = "chkAllUslLsl";
            this.chkAllUslLsl.Properties.Caption = "USL / LSL";
            this.chkAllUslLsl.Size = new System.Drawing.Size(117, 19);
            this.chkAllUslLsl.TabIndex = 2;
            // 
            // splMainSub
            // 
            this.splMainSub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splMainSub.Horizontal = false;
            this.splMainSub.Location = new System.Drawing.Point(0, 0);
            this.splMainSub.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.splMainSub.Name = "splMainSub";
            this.splMainSub.Panel1.Controls.Add(this.ucCpkGrid01);
            this.splMainSub.Panel1.Text = "Panel1";
            this.splMainSub.Panel2.Controls.Add(this.grdSelectChartRawData);
            this.splMainSub.Panel2.Text = "Panel2";
            this.splMainSub.Size = new System.Drawing.Size(1005, 606);
            this.splMainSub.SplitterPosition = 452;
            this.splMainSub.TabIndex = 0;
            this.splMainSub.Text = "smartSpliterContainer2";
            // 
            // ucCpkGrid01
            // 
            this.ucCpkGrid01.Appearance.BackColor = System.Drawing.Color.White;
            this.ucCpkGrid01.Appearance.Options.UseBackColor = true;
            this.ucCpkGrid01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCpkGrid01.Location = new System.Drawing.Point(0, 0);
            this.ucCpkGrid01.Name = "ucCpkGrid01";
            this.ucCpkGrid01.Size = new System.Drawing.Size(1005, 452);
            this.ucCpkGrid01.TabIndex = 0;
            // 
            // grdSelectChartRawData
            // 
            this.grdSelectChartRawData.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdSelectChartRawData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSelectChartRawData.IsUsePaging = false;
            this.grdSelectChartRawData.LanguageKey = "SPCCHARTRAWDATA";
            this.grdSelectChartRawData.Location = new System.Drawing.Point(0, 0);
            this.grdSelectChartRawData.Margin = new System.Windows.Forms.Padding(0);
            this.grdSelectChartRawData.Name = "grdSelectChartRawData";
            this.grdSelectChartRawData.ShowBorder = true;
            this.grdSelectChartRawData.ShowStatusBar = false;
            this.grdSelectChartRawData.Size = new System.Drawing.Size(1005, 149);
            this.grdSelectChartRawData.TabIndex = 1;
            // 
            // ucCpkFrame
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splMain);
            this.Name = "ucCpkFrame";
            this.Size = new System.Drawing.Size(1005, 606);
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).EndInit();
            this.splMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.palLeftMain)).EndInit();
            this.palLeftMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkLeftEstimate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).EndInit();
            this.smartGroupBox1.ResumeLayout(false);
            this.smartGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllRLclValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllRCclValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllRUclValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllLclValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllCclValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllUclValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoLeftGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLeftChartType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllUslLsl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splMainSub)).EndInit();
            this.splMainSub.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Framework.SmartControls.SmartPanel palLeftMain;
        private Framework.SmartControls.SmartGroupBox smartGroupBox1;
        private Framework.SmartControls.SmartRadioGroup rdoLeftGroup;
        private Framework.SmartControls.SmartComboBox cboLeftChartType;
        private Framework.SmartControls.SmartCheckBox chkAllUslLsl;
        private Framework.SmartControls.SmartCheckBox chkLeftEstimate;
        private Framework.SmartControls.SmartLabel lblAllRLcl;
        private Framework.SmartControls.SmartTextBox txtAllRLclValue;
        private Framework.SmartControls.SmartLabel lblAllRCcl;
        private Framework.SmartControls.SmartTextBox txtAllRCclValue;
        private Framework.SmartControls.SmartLabel lblAllRUcl;
        private Framework.SmartControls.SmartTextBox txtAllRUclValue;
        private Framework.SmartControls.SmartLabel lblAllLcl;
        private Framework.SmartControls.SmartTextBox txtAllLclValue;
        private Framework.SmartControls.SmartLabel lblAllCcl;
        private Framework.SmartControls.SmartTextBox txtAllCclValue;
        private Framework.SmartControls.SmartLabel lblAllUcl;
        private Framework.SmartControls.SmartTextBox txtAllUclValue;
        private Framework.SmartControls.SmartButton btnReExecution;
        private Framework.SmartControls.SmartBandedGrid grdSelectChartRawData;
        public Framework.SmartControls.SmartSpliterContainer splMain;
        public Framework.SmartControls.SmartSpliterContainer splMainSub;
        public ucCpkGrid ucCpkGrid01;
    }
}
