namespace Micube.SmartMES.EquipmentManagement.Popup
{
    partial class EquipmentCodePopup
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
            this.smartSplitTableLayoutPanel4 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdDurableCodeList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.btnConfirm = new Micube.Framework.SmartControls.SmartButton();
            this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblItemCode = new Micube.Framework.SmartControls.SmartLabel();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.lblItemVer = new Micube.Framework.SmartControls.SmartLabel();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.lblItemNm = new Micube.Framework.SmartControls.SmartLabel();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.cboProcessSegmentClass = new Micube.Framework.SmartControls.SmartComboBox();
            this.smartLabel7 = new Micube.Framework.SmartControls.SmartLabel();
            this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
            this.cboPlant = new Micube.Framework.SmartControls.SmartComboBox();
            this.cboArea = new Micube.Framework.SmartControls.SmartComboBox();
            this.txtEquipmentName = new Micube.Framework.SmartControls.SmartTextBox();
            this.smartSplitTableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboProcessSegmentClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlant.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEquipmentName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // smartSplitTableLayoutPanel4
            // 
            this.smartSplitTableLayoutPanel4.ColumnCount = 4;
            this.smartSplitTableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 212F));
            this.smartSplitTableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 219F));
            this.smartSplitTableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 277F));
            this.smartSplitTableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 260F));
            this.smartSplitTableLayoutPanel4.Controls.Add(this.grdDurableCodeList, 0, 1);
            this.smartSplitTableLayoutPanel4.Controls.Add(this.panelControl5, 0, 2);
            this.smartSplitTableLayoutPanel4.Controls.Add(this.panelControl1, 0, 0);
            this.smartSplitTableLayoutPanel4.Controls.Add(this.panelControl2, 1, 0);
            this.smartSplitTableLayoutPanel4.Controls.Add(this.panelControl3, 2, 0);
            this.smartSplitTableLayoutPanel4.Controls.Add(this.panelControl4, 3, 0);
            this.smartSplitTableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel4.Name = "smartSplitTableLayoutPanel4";
            this.smartSplitTableLayoutPanel4.RowCount = 3;
            this.smartSplitTableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.smartSplitTableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 448F));
            this.smartSplitTableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.smartSplitTableLayoutPanel4.Size = new System.Drawing.Size(1053, 515);
            this.smartSplitTableLayoutPanel4.TabIndex = 1;
            // 
            // grdDurableCodeList
            // 
            this.grdDurableCodeList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.smartSplitTableLayoutPanel4.SetColumnSpan(this.grdDurableCodeList, 4);
            this.grdDurableCodeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDurableCodeList.IsUsePaging = false;
            this.grdDurableCodeList.LanguageKey = "SPAREPARTIDANDNAME";
            this.grdDurableCodeList.Location = new System.Drawing.Point(0, 33);
            this.grdDurableCodeList.Margin = new System.Windows.Forms.Padding(0);
            this.grdDurableCodeList.Name = "grdDurableCodeList";
            this.grdDurableCodeList.ShowBorder = true;
            this.grdDurableCodeList.ShowStatusBar = false;
            this.grdDurableCodeList.Size = new System.Drawing.Size(1053, 448);
            this.grdDurableCodeList.TabIndex = 111;
            // 
            // panelControl5
            // 
            this.panelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartSplitTableLayoutPanel4.SetColumnSpan(this.panelControl5, 3);
            this.panelControl5.Controls.Add(this.btnConfirm);
            this.panelControl5.Controls.Add(this.btnCancel);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl5.Location = new System.Drawing.Point(0, 481);
            this.panelControl5.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(708, 34);
            this.panelControl5.TabIndex = 113;
            // 
            // btnConfirm
            // 
            this.btnConfirm.AllowFocus = false;
            this.btnConfirm.IsBusy = false;
            this.btnConfirm.LanguageKey = "CONFIRM";
            this.btnConfirm.Location = new System.Drawing.Point(450, 3);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "smartButton2";
            this.btnConfirm.TooltipLanguageKey = "";
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.IsBusy = false;
            this.btnCancel.LanguageKey = "CANCEL";
            this.btnCancel.Location = new System.Drawing.Point(531, 3);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "smartButton1";
            this.btnCancel.TooltipLanguageKey = "";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.cboPlant);
            this.panelControl1.Controls.Add(this.lblItemCode);
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(212, 33);
            this.panelControl1.TabIndex = 86;
            // 
            // lblItemCode
            // 
            this.lblItemCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblItemCode.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblItemCode.Appearance.Options.UseFont = true;
            this.lblItemCode.Appearance.Options.UseTextOptions = true;
            this.lblItemCode.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblItemCode.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblItemCode.LanguageKey = "PLANT";
            this.lblItemCode.Location = new System.Drawing.Point(5, 5);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(80, 22);
            this.lblItemCode.TabIndex = 0;
            this.lblItemCode.Text = "SITE:";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.cboArea);
            this.panelControl2.Controls.Add(this.lblItemVer);
            this.panelControl2.Location = new System.Drawing.Point(212, 0);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(219, 33);
            this.panelControl2.TabIndex = 87;
            // 
            // lblItemVer
            // 
            this.lblItemVer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblItemVer.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblItemVer.Appearance.Options.UseFont = true;
            this.lblItemVer.Appearance.Options.UseTextOptions = true;
            this.lblItemVer.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblItemVer.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblItemVer.LanguageKey = "AREA";
            this.lblItemVer.Location = new System.Drawing.Point(5, 5);
            this.lblItemVer.Name = "lblItemVer";
            this.lblItemVer.Size = new System.Drawing.Size(72, 22);
            this.lblItemVer.TabIndex = 0;
            this.lblItemVer.Text = "Area:";
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.cboProcessSegmentClass);
            this.panelControl3.Controls.Add(this.lblItemNm);
            this.panelControl3.Location = new System.Drawing.Point(431, 0);
            this.panelControl3.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(276, 33);
            this.panelControl3.TabIndex = 88;
            // 
            // lblItemNm
            // 
            this.lblItemNm.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblItemNm.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblItemNm.Appearance.Options.UseFont = true;
            this.lblItemNm.Appearance.Options.UseTextOptions = true;
            this.lblItemNm.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblItemNm.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblItemNm.LanguageKey = "PROCESSSEGMENTCLASSNAME";
            this.lblItemNm.Location = new System.Drawing.Point(5, 5);
            this.lblItemNm.Name = "lblItemNm";
            this.lblItemNm.Size = new System.Drawing.Size(100, 22);
            this.lblItemNm.TabIndex = 0;
            this.lblItemNm.Text = "대공정:";
            // 
            // panelControl4
            // 
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.txtEquipmentName);
            this.panelControl4.Controls.Add(this.smartLabel7);
            this.panelControl4.Controls.Add(this.btnSearch);
            this.panelControl4.Location = new System.Drawing.Point(708, 0);
            this.panelControl4.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(334, 33);
            this.panelControl4.TabIndex = 114;
            // 
            // cboProcessSegmentClass
            // 
            this.cboProcessSegmentClass.LabelText = null;
            this.cboProcessSegmentClass.LanguageKey = null;
            this.cboProcessSegmentClass.Location = new System.Drawing.Point(116, 6);
            this.cboProcessSegmentClass.Name = "cboProcessSegmentClass";
            this.cboProcessSegmentClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboProcessSegmentClass.Properties.NullText = "";
            this.cboProcessSegmentClass.ShowHeader = true;
            this.cboProcessSegmentClass.Size = new System.Drawing.Size(157, 20);
            this.cboProcessSegmentClass.TabIndex = 110;
            // 
            // smartLabel7
            // 
            this.smartLabel7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.smartLabel7.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.smartLabel7.Appearance.Options.UseFont = true;
            this.smartLabel7.Appearance.Options.UseTextOptions = true;
            this.smartLabel7.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.smartLabel7.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.smartLabel7.LanguageKey = "EQUIPMENT";
            this.smartLabel7.Location = new System.Drawing.Point(5, 5);
            this.smartLabel7.Name = "smartLabel7";
            this.smartLabel7.Size = new System.Drawing.Size(77, 22);
            this.smartLabel7.TabIndex = 0;
            this.smartLabel7.Text = "설비명:";
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.IsBusy = false;
            this.btnSearch.LanguageKey = "SEARCH";
            this.btnSearch.Location = new System.Drawing.Point(251, 4);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSearch.Size = new System.Drawing.Size(80, 25);
            this.btnSearch.TabIndex = 109;
            this.btnSearch.Text = "smartButton1";
            this.btnSearch.TooltipLanguageKey = "";
            // 
            // cboPlant
            // 
            this.cboPlant.LabelText = null;
            this.cboPlant.LanguageKey = null;
            this.cboPlant.Location = new System.Drawing.Point(91, 7);
            this.cboPlant.Name = "cboPlant";
            this.cboPlant.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPlant.Properties.NullText = "";
            this.cboPlant.ShowHeader = true;
            this.cboPlant.Size = new System.Drawing.Size(118, 20);
            this.cboPlant.TabIndex = 115;
            // 
            // cboArea
            // 
            this.cboArea.LabelText = null;
            this.cboArea.LanguageKey = null;
            this.cboArea.Location = new System.Drawing.Point(83, 7);
            this.cboArea.Name = "cboArea";
            this.cboArea.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboArea.Properties.NullText = "";
            this.cboArea.ShowHeader = true;
            this.cboArea.Size = new System.Drawing.Size(133, 20);
            this.cboArea.TabIndex = 116;
            // 
            // txtEquipmentName
            // 
            this.txtEquipmentName.EditValue = "";
            this.txtEquipmentName.LabelText = null;
            this.txtEquipmentName.LanguageKey = "";
            this.txtEquipmentName.Location = new System.Drawing.Point(88, 5);
            this.txtEquipmentName.Name = "txtEquipmentName";
            this.txtEquipmentName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtEquipmentName.Properties.Appearance.Options.UseFont = true;
            this.txtEquipmentName.Properties.AutoHeight = false;
            this.txtEquipmentName.Size = new System.Drawing.Size(150, 22);
            this.txtEquipmentName.TabIndex = 5;
            this.txtEquipmentName.Tag = "SUBCLASS";
            // 
            // EquipmentCodePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1053, 515);
            this.Controls.Add(this.smartSplitTableLayoutPanel4);
            this.Name = "EquipmentCodePopup";
            this.Text = "EquipmentCodePopup";
            this.smartSplitTableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboProcessSegmentClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlant.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEquipmentName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel4;
        private Framework.SmartControls.SmartBandedGrid grdDurableCodeList;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private Framework.SmartControls.SmartButton btnConfirm;
        private Framework.SmartControls.SmartButton btnCancel;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Framework.SmartControls.SmartLabel lblItemCode;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private Framework.SmartControls.SmartLabel lblItemVer;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private Framework.SmartControls.SmartLabel lblItemNm;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private Framework.SmartControls.SmartComboBox cboProcessSegmentClass;
        private Framework.SmartControls.SmartLabel smartLabel7;
        private Framework.SmartControls.SmartButton btnSearch;
        private Framework.SmartControls.SmartComboBox cboPlant;
        private Framework.SmartControls.SmartComboBox cboArea;
        private Framework.SmartControls.SmartTextBox txtEquipmentName;
    }
}