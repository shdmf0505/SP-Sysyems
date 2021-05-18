namespace Micube.SmartMES.OutsideOrderMgnt.Popup
{
    partial class productdefidPopup
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
            this.tplMain = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdProductItem = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.btnConfirm = new Micube.Framework.SmartControls.SmartButton();
            this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtItemCode = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblItemCode = new Micube.Framework.SmartControls.SmartLabel();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.txtItemNm = new Micube.Framework.SmartControls.SmartTextBox();
            this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
            this.lblItemNm = new Micube.Framework.SmartControls.SmartLabel();
            this.smartPanel6 = new Micube.Framework.SmartControls.SmartPanel();
            this.lblproductdeftype = new Micube.Framework.SmartControls.SmartLabel();
            this.cboproductdeftype = new Micube.Framework.SmartControls.SmartComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.tplMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemNm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel6)).BeginInit();
            this.smartPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboproductdeftype.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tplMain);
            this.pnlMain.Size = new System.Drawing.Size(948, 426);
            // 
            // tplMain
            // 
            this.tplMain.ColumnCount = 2;
            this.tplMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 310F));
            this.tplMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 506F));
            this.tplMain.Controls.Add(this.grdProductItem, 0, 2);
            this.tplMain.Controls.Add(this.panelControl5, 0, 3);
            this.tplMain.Controls.Add(this.panelControl1, 0, 1);
            this.tplMain.Controls.Add(this.panelControl3, 1, 1);
            this.tplMain.Controls.Add(this.smartPanel6, 0, 0);
            this.tplMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplMain.Location = new System.Drawing.Point(0, 0);
            this.tplMain.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tplMain.Name = "tplMain";
            this.tplMain.RowCount = 4;
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tplMain.Size = new System.Drawing.Size(948, 426);
            this.tplMain.TabIndex = 2;
            // 
            // grdProductItem
            // 
            this.grdProductItem.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.tplMain.SetColumnSpan(this.grdProductItem, 2);
            this.grdProductItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProductItem.IsUsePaging = false;
            this.grdProductItem.LanguageKey = "PRODUCTDEFIDNAME";
            this.grdProductItem.Location = new System.Drawing.Point(0, 64);
            this.grdProductItem.Margin = new System.Windows.Forms.Padding(0);
            this.grdProductItem.Name = "grdProductItem";
            this.grdProductItem.ShowBorder = true;
            this.grdProductItem.ShowStatusBar = false;
            this.grdProductItem.Size = new System.Drawing.Size(948, 322);
            this.grdProductItem.TabIndex = 111;
            // 
            // panelControl5
            // 
            this.panelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tplMain.SetColumnSpan(this.panelControl5, 2);
            this.panelControl5.Controls.Add(this.btnConfirm);
            this.panelControl5.Controls.Add(this.btnCancel);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl5.Location = new System.Drawing.Point(0, 386);
            this.panelControl5.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(948, 40);
            this.panelControl5.TabIndex = 113;
            // 
            // btnConfirm
            // 
            this.btnConfirm.AllowFocus = false;
            this.btnConfirm.IsBusy = false;
            this.btnConfirm.LanguageKey = "CONFIRM";
            this.btnConfirm.Location = new System.Drawing.Point(395, 8);
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
            this.btnCancel.Location = new System.Drawing.Point(476, 8);
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
            this.panelControl1.Controls.Add(this.txtItemCode);
            this.panelControl1.Controls.Add(this.lblItemCode);
            this.panelControl1.Location = new System.Drawing.Point(0, 30);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(310, 33);
            this.panelControl1.TabIndex = 86;
            // 
            // txtItemCode
            // 
            this.txtItemCode.EditValue = "";
            this.txtItemCode.LabelText = null;
            this.txtItemCode.LanguageKey = "";
            this.txtItemCode.Location = new System.Drawing.Point(111, 3);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtItemCode.Properties.Appearance.Options.UseFont = true;
            this.txtItemCode.Properties.AutoHeight = false;
            this.txtItemCode.Size = new System.Drawing.Size(187, 22);
            this.txtItemCode.TabIndex = 5;
            this.txtItemCode.Tag = "MATERIALTYPE";
            // 
            // lblItemCode
            // 
            this.lblItemCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblItemCode.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblItemCode.Appearance.Options.UseFont = true;
            this.lblItemCode.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblItemCode.LanguageKey = "PRODUCTDEFID";
            this.lblItemCode.Location = new System.Drawing.Point(5, 4);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(100, 22);
            this.lblItemCode.TabIndex = 0;
            this.lblItemCode.Text = "품목코드";
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.txtItemNm);
            this.panelControl3.Controls.Add(this.btnSearch);
            this.panelControl3.Controls.Add(this.lblItemNm);
            this.panelControl3.Location = new System.Drawing.Point(310, 30);
            this.panelControl3.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(638, 33);
            this.panelControl3.TabIndex = 88;
            // 
            // txtItemNm
            // 
            this.txtItemNm.EditValue = "";
            this.txtItemNm.LabelText = null;
            this.txtItemNm.LanguageKey = "";
            this.txtItemNm.Location = new System.Drawing.Point(156, 4);
            this.txtItemNm.Name = "txtItemNm";
            this.txtItemNm.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtItemNm.Properties.Appearance.Options.UseFont = true;
            this.txtItemNm.Properties.AutoHeight = false;
            this.txtItemNm.Size = new System.Drawing.Size(327, 22);
            this.txtItemNm.TabIndex = 5;
            this.txtItemNm.Tag = "SUBCLASS";
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.IsBusy = false;
            this.btnSearch.LanguageKey = "SEARCH";
            this.btnSearch.Location = new System.Drawing.Point(555, 4);
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
            this.lblItemNm.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblItemNm.LanguageKey = "PRODUCTDEFNAME";
            this.lblItemNm.Location = new System.Drawing.Point(5, 5);
            this.lblItemNm.Name = "lblItemNm";
            this.lblItemNm.Size = new System.Drawing.Size(100, 22);
            this.lblItemNm.TabIndex = 0;
            this.lblItemNm.Text = "품목명";
            // 
            // smartPanel6
            // 
            this.smartPanel6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel6.Controls.Add(this.lblproductdeftype);
            this.smartPanel6.Controls.Add(this.cboproductdeftype);
            this.smartPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel6.Location = new System.Drawing.Point(0, 0);
            this.smartPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel6.Name = "smartPanel6";
            this.smartPanel6.Size = new System.Drawing.Size(310, 30);
            this.smartPanel6.TabIndex = 114;
            // 
            // lblproductdeftype
            // 
            this.lblproductdeftype.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblproductdeftype.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblproductdeftype.Appearance.Options.UseFont = true;
            this.lblproductdeftype.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblproductdeftype.LanguageKey = "PRODUCTDEFTYPE";
            this.lblproductdeftype.Location = new System.Drawing.Point(5, 3);
            this.lblproductdeftype.Name = "lblproductdeftype";
            this.lblproductdeftype.Size = new System.Drawing.Size(100, 22);
            this.lblproductdeftype.TabIndex = 0;
            this.lblproductdeftype.Text = "품목코드";
            // 
            // cboproductdeftype
            // 
            this.cboproductdeftype.LabelText = null;
            this.cboproductdeftype.LanguageKey = null;
            this.cboproductdeftype.Location = new System.Drawing.Point(111, 1);
            this.cboproductdeftype.Name = "cboproductdeftype";
            this.cboproductdeftype.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboproductdeftype.Properties.NullText = "";
            this.cboproductdeftype.ShowHeader = true;
            this.cboproductdeftype.Size = new System.Drawing.Size(187, 24);
            this.cboproductdeftype.TabIndex = 115;
            // 
            // productdefidPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 446);
            this.Name = "productdefidPopup";
            this.Text = "제품조회";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.tplMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtItemCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtItemNm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel6)).EndInit();
            this.smartPanel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboproductdeftype.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel tplMain;
        private Framework.SmartControls.SmartBandedGrid grdProductItem;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private Framework.SmartControls.SmartButton btnConfirm;
        private Framework.SmartControls.SmartButton btnCancel;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Framework.SmartControls.SmartTextBox txtItemCode;
        private Framework.SmartControls.SmartLabel lblItemCode;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private Framework.SmartControls.SmartTextBox txtItemNm;
        private Framework.SmartControls.SmartButton btnSearch;
        private Framework.SmartControls.SmartLabel lblItemNm;
        private Framework.SmartControls.SmartPanel smartPanel6;
        private Framework.SmartControls.SmartLabel lblproductdeftype;
        private Framework.SmartControls.SmartComboBox cboproductdeftype;
    }
}