namespace Micube.SmartMES.ToolManagement.Popup
{
    partial class FilmCodePopup
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
            this.grdFilmCodeList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.btnConfirm = new Micube.Framework.SmartControls.SmartButton();
            this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtProductCode = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblItemCode = new Micube.Framework.SmartControls.SmartLabel();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.txtProductDefName = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblItemVer = new Micube.Framework.SmartControls.SmartLabel();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.cboProcessSegment = new Micube.Framework.SmartControls.SmartComboBox();
            this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
            this.lblItemNm = new Micube.Framework.SmartControls.SmartLabel();
            this.txtFilmCode = new Micube.Framework.SmartControls.SmartTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.smartSplitTableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductDefName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboProcessSegment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilmCode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel4);
            this.pnlMain.Size = new System.Drawing.Size(846, 406);
            // 
            // smartSplitTableLayoutPanel4
            // 
            this.smartSplitTableLayoutPanel4.ColumnCount = 3;
            this.smartSplitTableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 270F));
            this.smartSplitTableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 242F));
            this.smartSplitTableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 312F));
            this.smartSplitTableLayoutPanel4.Controls.Add(this.grdFilmCodeList, 0, 1);
            this.smartSplitTableLayoutPanel4.Controls.Add(this.panelControl5, 0, 2);
            this.smartSplitTableLayoutPanel4.Controls.Add(this.panelControl1, 0, 0);
            this.smartSplitTableLayoutPanel4.Controls.Add(this.panelControl2, 1, 0);
            this.smartSplitTableLayoutPanel4.Controls.Add(this.panelControl3, 2, 0);
            this.smartSplitTableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel4.Name = "smartSplitTableLayoutPanel4";
            this.smartSplitTableLayoutPanel4.RowCount = 3;
            this.smartSplitTableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.smartSplitTableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 341F));
            this.smartSplitTableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.smartSplitTableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel4.Size = new System.Drawing.Size(846, 406);
            this.smartSplitTableLayoutPanel4.TabIndex = 1;
            // 
            // grdFilmCodeList
            // 
            this.grdFilmCodeList.Caption = "필름목록:";
            this.smartSplitTableLayoutPanel4.SetColumnSpan(this.grdFilmCodeList, 3);
            this.grdFilmCodeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFilmCodeList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdFilmCodeList.IsUsePaging = false;
            this.grdFilmCodeList.LanguageKey = "FILMCODEBROWSE";
            this.grdFilmCodeList.Location = new System.Drawing.Point(0, 33);
            this.grdFilmCodeList.Margin = new System.Windows.Forms.Padding(0);
            this.grdFilmCodeList.Name = "grdFilmCodeList";
            this.grdFilmCodeList.ShowBorder = true;
            this.grdFilmCodeList.ShowStatusBar = false;
            this.grdFilmCodeList.Size = new System.Drawing.Size(846, 341);
            this.grdFilmCodeList.TabIndex = 111;
            // 
            // panelControl5
            // 
            this.panelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartSplitTableLayoutPanel4.SetColumnSpan(this.panelControl5, 3);
            this.panelControl5.Controls.Add(this.btnConfirm);
            this.panelControl5.Controls.Add(this.btnCancel);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl5.Location = new System.Drawing.Point(0, 374);
            this.panelControl5.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(846, 32);
            this.panelControl5.TabIndex = 113;
            // 
            // btnConfirm
            // 
            this.btnConfirm.AllowFocus = false;
            this.btnConfirm.IsBusy = false;
            this.btnConfirm.IsWrite = false;
            this.btnConfirm.LanguageKey = "CONFIRM";
            this.btnConfirm.Location = new System.Drawing.Point(337, 3);
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
            this.btnCancel.IsWrite = false;
            this.btnCancel.LanguageKey = "CANCEL";
            this.btnCancel.Location = new System.Drawing.Point(418, 3);
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
            this.panelControl1.Controls.Add(this.txtProductCode);
            this.panelControl1.Controls.Add(this.lblItemCode);
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(270, 33);
            this.panelControl1.TabIndex = 86;
            // 
            // txtProductCode
            // 
            this.txtProductCode.EditValue = "";
            this.txtProductCode.LabelText = null;
            this.txtProductCode.LanguageKey = "";
            this.txtProductCode.Location = new System.Drawing.Point(110, 5);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtProductCode.Properties.Appearance.Options.UseFont = true;
            this.txtProductCode.Properties.AutoHeight = false;
            this.txtProductCode.Size = new System.Drawing.Size(149, 22);
            this.txtProductCode.TabIndex = 6;
            this.txtProductCode.Tag = "MASTERDATACLASS";
            // 
            // lblItemCode
            // 
            this.lblItemCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblItemCode.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblItemCode.Appearance.Options.UseFont = true;
            this.lblItemCode.Appearance.Options.UseTextOptions = true;
            this.lblItemCode.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblItemCode.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblItemCode.LanguageKey = "PRODUCTDEFID";
            this.lblItemCode.Location = new System.Drawing.Point(5, 5);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(100, 22);
            this.lblItemCode.TabIndex = 0;
            this.lblItemCode.Text = "품목코드:";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.txtProductDefName);
            this.panelControl2.Controls.Add(this.lblItemVer);
            this.panelControl2.Location = new System.Drawing.Point(270, 0);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(242, 33);
            this.panelControl2.TabIndex = 87;
            // 
            // txtProductDefName
            // 
            this.txtProductDefName.EditValue = "";
            this.txtProductDefName.LabelText = null;
            this.txtProductDefName.LanguageKey = "";
            this.txtProductDefName.Location = new System.Drawing.Point(83, 5);
            this.txtProductDefName.Name = "txtProductDefName";
            this.txtProductDefName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtProductDefName.Properties.Appearance.Options.UseFont = true;
            this.txtProductDefName.Properties.AutoHeight = false;
            this.txtProductDefName.Size = new System.Drawing.Size(156, 22);
            this.txtProductDefName.TabIndex = 5;
            this.txtProductDefName.Tag = "MASTERDATACLASS";
            // 
            // lblItemVer
            // 
            this.lblItemVer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblItemVer.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblItemVer.Appearance.Options.UseFont = true;
            this.lblItemVer.Appearance.Options.UseTextOptions = true;
            this.lblItemVer.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblItemVer.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblItemVer.LanguageKey = "PRODUCTDEFNAME";
            this.lblItemVer.Location = new System.Drawing.Point(5, 5);
            this.lblItemVer.Name = "lblItemVer";
            this.lblItemVer.Size = new System.Drawing.Size(72, 22);
            this.lblItemVer.TabIndex = 0;
            this.lblItemVer.Text = "품목명:";
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.cboProcessSegment);
            this.panelControl3.Controls.Add(this.btnSearch);
            this.panelControl3.Controls.Add(this.lblItemNm);
            this.panelControl3.Location = new System.Drawing.Point(512, 0);
            this.panelControl3.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(331, 33);
            this.panelControl3.TabIndex = 88;
            // 
            // cboProcessSegment
            // 
            this.cboProcessSegment.LabelText = null;
            this.cboProcessSegment.LanguageKey = null;
            this.cboProcessSegment.Location = new System.Drawing.Point(119, 6);
            this.cboProcessSegment.Name = "cboProcessSegment";
            this.cboProcessSegment.PopupWidth = 0;
            this.cboProcessSegment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboProcessSegment.Properties.NullText = "";
            this.cboProcessSegment.ShowHeader = true;
            this.cboProcessSegment.Size = new System.Drawing.Size(86, 20);
            this.cboProcessSegment.TabIndex = 117;
            this.cboProcessSegment.VisibleColumns = null;
            this.cboProcessSegment.VisibleColumnsWidth = null;
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.IsBusy = false;
            this.btnSearch.IsWrite = false;
            this.btnSearch.LanguageKey = "SEARCH";
            this.btnSearch.Location = new System.Drawing.Point(248, 4);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSearch.Size = new System.Drawing.Size(80, 25);
            this.btnSearch.TabIndex = 109;
            this.btnSearch.Text = "smartButton1";
            this.btnSearch.TooltipLanguageKey = "";
            // 
            // lblItemNm
            // 
            this.lblItemNm.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblItemNm.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblItemNm.Appearance.Options.UseFont = true;
            this.lblItemNm.Appearance.Options.UseTextOptions = true;
            this.lblItemNm.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblItemNm.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblItemNm.LanguageKey = "USEPROCESSSEGMENT";
            this.lblItemNm.Location = new System.Drawing.Point(5, 5);
            this.lblItemNm.Name = "lblItemNm";
            this.lblItemNm.Size = new System.Drawing.Size(100, 22);
            this.lblItemNm.TabIndex = 0;
            this.lblItemNm.Text = "사용공정:";
            // 
            // txtFilmCode
            // 
            this.txtFilmCode.EditValue = "";
            this.txtFilmCode.LabelText = null;
            this.txtFilmCode.LanguageKey = "";
            this.txtFilmCode.Location = new System.Drawing.Point(747, 419);
            this.txtFilmCode.Name = "txtFilmCode";
            this.txtFilmCode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtFilmCode.Properties.Appearance.Options.UseFont = true;
            this.txtFilmCode.Properties.AutoHeight = false;
            this.txtFilmCode.Size = new System.Drawing.Size(149, 22);
            this.txtFilmCode.TabIndex = 7;
            this.txtFilmCode.Tag = "MASTERDATACLASS";
            this.txtFilmCode.Visible = false;
            // 
            // FilmCodePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 426);
            this.Controls.Add(this.txtFilmCode);
            this.LanguageKey = "FILMCODEBROWSE";
            this.Name = "FilmCodePopup";
            this.Text = "FilmCodePopup";
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.Controls.SetChildIndex(this.txtFilmCode, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.smartSplitTableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtProductCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtProductDefName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboProcessSegment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilmCode.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel4;
        private Framework.SmartControls.SmartBandedGrid grdFilmCodeList;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private Framework.SmartControls.SmartButton btnConfirm;
        private Framework.SmartControls.SmartButton btnCancel;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Framework.SmartControls.SmartLabel lblItemCode;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private Framework.SmartControls.SmartTextBox txtProductDefName;
        private Framework.SmartControls.SmartLabel lblItemVer;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private Framework.SmartControls.SmartButton btnSearch;
        private Framework.SmartControls.SmartLabel lblItemNm;
        private Framework.SmartControls.SmartComboBox cboProcessSegment;
        private Framework.SmartControls.SmartTextBox txtProductCode;
        private Framework.SmartControls.SmartTextBox txtFilmCode;
    }
}