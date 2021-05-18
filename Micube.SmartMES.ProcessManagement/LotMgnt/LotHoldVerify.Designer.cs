namespace Micube.SmartMES.ProcessManagement
{
    partial class LotHoldVerify
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdHold = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.txtDefectCode = new Micube.Framework.SmartControls.SmartLabelSelectPopupEdit();
            this.cboState = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.smartGroupBox1 = new Micube.Framework.SmartControls.SmartGroupBox();
            this.txtComment = new DevExpress.XtraEditors.MemoEdit();
            this.lblComment = new Micube.Framework.SmartControls.SmartLabel();
            this.cboReason = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.cboTopClass = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnUpDown = new Micube.SmartMES.Commons.Controls.ucDataUpDownBtn();
            this.spcSpliter = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.tabHold = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpHoldVerify = new DevExpress.XtraTab.XtraTabPage();
            this.grdWIP = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpScrap = new DevExpress.XtraTab.XtraTabPage();
            this.grdScrap = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.panel1.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDefectCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).BeginInit();
            this.smartGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboReason.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTopClass.Properties)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabHold)).BeginInit();
            this.tabHold.SuspendLayout();
            this.tpHoldVerify.SuspendLayout();
            this.tpScrap.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabHold);
            this.pnlContent.Controls.Add(this.spcSpliter);
            this.pnlContent.Controls.Add(this.panel1);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.panel1.Controls.Add(this.smartSpliterControl1);
            this.panel1.Controls.Add(this.smartGroupBox1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 161);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(756, 328);
            this.panel1.TabIndex = 4;
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdHold, 0, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartSplitTableLayoutPanel2, 0, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 45);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 2;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(434, 283);
            this.smartSplitTableLayoutPanel1.TabIndex = 7;
            // 
            // grdHold
            // 
            this.grdHold.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdHold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdHold.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdHold.IsUsePaging = false;
            this.grdHold.LanguageKey = "TARGETHOLD";
            this.grdHold.Location = new System.Drawing.Point(0, 29);
            this.grdHold.Margin = new System.Windows.Forms.Padding(0);
            this.grdHold.Name = "grdHold";
            this.grdHold.ShowBorder = true;
            this.grdHold.Size = new System.Drawing.Size(434, 254);
            this.grdHold.TabIndex = 2;
            this.grdHold.UseAutoBestFitColumns = false;
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.ColumnCount = 4;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 310F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 310F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.txtDefectCode, 2, 0);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.cboState, 0, 0);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 1;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(434, 29);
            this.smartSplitTableLayoutPanel2.TabIndex = 3;
            // 
            // txtDefectCode
            // 
            this.txtDefectCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDefectCode.EditorWidth = "80%";
            this.txtDefectCode.LabelText = " 불량 코드";
            this.txtDefectCode.LabelWidth = "25%";
            this.txtDefectCode.LanguageKey = "DEFECTCODE";
            this.txtDefectCode.Location = new System.Drawing.Point(315, 0);
            this.txtDefectCode.Margin = new System.Windows.Forms.Padding(0);
            this.txtDefectCode.Name = "txtDefectCode";
            this.txtDefectCode.Size = new System.Drawing.Size(310, 20);
            this.txtDefectCode.TabIndex = 2;
            // 
            // cboState
            // 
            this.cboState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboState.EditorWidth = "100%";
            this.cboState.LanguageKey = "HOLDSTAE";
            this.cboState.Location = new System.Drawing.Point(3, 3);
            this.cboState.Name = "cboState";
            this.cboState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboState.Properties.NullText = "";
            this.cboState.Size = new System.Drawing.Size(304, 20);
            this.cboState.TabIndex = 0;
            // 
            // smartSpliterControl1
            // 
            this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.smartSpliterControl1.Location = new System.Drawing.Point(434, 45);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(5, 283);
            this.smartSpliterControl1.TabIndex = 6;
            this.smartSpliterControl1.TabStop = false;
            // 
            // smartGroupBox1
            // 
            this.smartGroupBox1.Controls.Add(this.txtComment);
            this.smartGroupBox1.Controls.Add(this.lblComment);
            this.smartGroupBox1.Controls.Add(this.cboReason);
            this.smartGroupBox1.Controls.Add(this.cboTopClass);
            this.smartGroupBox1.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.smartGroupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.smartGroupBox1.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.smartGroupBox1.LanguageKey = "HOLDREASON";
            this.smartGroupBox1.Location = new System.Drawing.Point(439, 45);
            this.smartGroupBox1.Name = "smartGroupBox1";
            this.smartGroupBox1.ShowBorder = true;
            this.smartGroupBox1.Size = new System.Drawing.Size(317, 283);
            this.smartGroupBox1.TabIndex = 0;
            this.smartGroupBox1.Text = "보류 사유";
            // 
            // txtComment
            // 
            this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComment.Location = new System.Drawing.Point(5, 106);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(306, 171);
            this.txtComment.TabIndex = 5;
            // 
            // lblComment
            // 
            this.lblComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblComment.LanguageKey = "DETAILNOTE";
            this.lblComment.Location = new System.Drawing.Point(5, 86);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(40, 14);
            this.lblComment.TabIndex = 3;
            this.lblComment.Text = "상세사항";
            // 
            // cboReason
            // 
            this.cboReason.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboReason.LabelText = "중분류";
            this.cboReason.LabelWidth = "20%";
            this.cboReason.LanguageKey = "MIDDLECLASS";
            this.cboReason.Location = new System.Drawing.Point(5, 60);
            this.cboReason.Name = "cboReason";
            this.cboReason.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboReason.Properties.NullText = "";
            this.cboReason.Size = new System.Drawing.Size(307, 20);
            this.cboReason.TabIndex = 2;
            // 
            // cboTopClass
            // 
            this.cboTopClass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTopClass.LabelText = "대분류";
            this.cboTopClass.LabelWidth = "20%";
            this.cboTopClass.LanguageKey = "LARGECLASS";
            this.cboTopClass.Location = new System.Drawing.Point(5, 34);
            this.cboTopClass.Name = "cboTopClass";
            this.cboTopClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTopClass.Properties.NullText = "";
            this.cboTopClass.Size = new System.Drawing.Size(307, 20);
            this.cboTopClass.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnUpDown);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(756, 45);
            this.panel2.TabIndex = 0;
            // 
            // btnUpDown
            // 
            this.btnUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUpDown.Location = new System.Drawing.Point(0, 0);
            this.btnUpDown.Name = "btnUpDown";
            this.btnUpDown.Size = new System.Drawing.Size(756, 45);
            this.btnUpDown.SourceGrid = null;
            this.btnUpDown.TabIndex = 0;
            this.btnUpDown.TargetGrid = null;
            // 
            // spcSpliter
            // 
            this.spcSpliter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.spcSpliter.Location = new System.Drawing.Point(0, 156);
            this.spcSpliter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcSpliter.Name = "spcSpliter";
            this.spcSpliter.Size = new System.Drawing.Size(756, 5);
            this.spcSpliter.TabIndex = 5;
            this.spcSpliter.TabStop = false;
            // 
            // tabHold
            // 
            this.tabHold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabHold.Location = new System.Drawing.Point(0, 0);
            this.tabHold.Name = "tabHold";
            this.tabHold.SelectedTabPage = this.tpHoldVerify;
            this.tabHold.Size = new System.Drawing.Size(756, 156);
            this.tabHold.TabIndex = 6;
            this.tabHold.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpHoldVerify,
            this.tpScrap});
            // 
            // tpHoldVerify
            // 
            this.tpHoldVerify.Controls.Add(this.grdWIP);
            this.tabHold.SetLanguageKey(this.tpHoldVerify, "HOLDVERIFY");
            this.tpHoldVerify.Name = "tpHoldVerify";
            this.tpHoldVerify.Size = new System.Drawing.Size(750, 127);
            this.tpHoldVerify.Text = "xtraTabPage1";
            // 
            // grdWIP
            // 
            this.grdWIP.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdWIP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdWIP.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdWIP.IsUsePaging = false;
            this.grdWIP.LanguageKey = "WIPLIST";
            this.grdWIP.Location = new System.Drawing.Point(0, 0);
            this.grdWIP.Margin = new System.Windows.Forms.Padding(0);
            this.grdWIP.Name = "grdWIP";
            this.grdWIP.ShowBorder = true;
            this.grdWIP.ShowStatusBar = false;
            this.grdWIP.Size = new System.Drawing.Size(750, 127);
            this.grdWIP.TabIndex = 2;
            this.grdWIP.UseAutoBestFitColumns = false;
            // 
            // tpScrap
            // 
            this.tpScrap.Controls.Add(this.grdScrap);
            this.tabHold.SetLanguageKey(this.tpScrap, "SCRAP");
            this.tpScrap.Name = "tpScrap";
            this.tpScrap.Size = new System.Drawing.Size(750, 127);
            this.tpScrap.Text = "xtraTabPage2";
            // 
            // grdScrap
            // 
            this.grdScrap.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdScrap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdScrap.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdScrap.IsUsePaging = false;
            this.grdScrap.LanguageKey = "WIPLIST";
            this.grdScrap.Location = new System.Drawing.Point(0, 0);
            this.grdScrap.Margin = new System.Windows.Forms.Padding(0);
            this.grdScrap.Name = "grdScrap";
            this.grdScrap.ShowBorder = true;
            this.grdScrap.ShowStatusBar = false;
            this.grdScrap.Size = new System.Drawing.Size(750, 127);
            this.grdScrap.TabIndex = 3;
            this.grdScrap.UseAutoBestFitColumns = false;
            // 
            // LotHoldVerify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 538);
            this.Name = "LotHoldVerify";
            this.Text = "Lot Hold";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.panel1.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDefectCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).EndInit();
            this.smartGroupBox1.ResumeLayout(false);
            this.smartGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboReason.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTopClass.Properties)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabHold)).EndInit();
            this.tabHold.ResumeLayout(false);
            this.tpHoldVerify.ResumeLayout(false);
            this.tpScrap.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private Framework.SmartControls.SmartSpliterControl spcSpliter;
        private System.Windows.Forms.Panel panel2;
        private Framework.SmartControls.SmartBandedGrid grdHold;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
        private Framework.SmartControls.SmartGroupBox smartGroupBox1;
        private Framework.SmartControls.SmartLabel lblComment;
        private Framework.SmartControls.SmartLabelComboBox cboReason;
        private Framework.SmartControls.SmartLabelComboBox cboTopClass;
        private DevExpress.XtraEditors.MemoEdit txtComment;
        private Micube.SmartMES.Commons.Controls.ucDataUpDownBtn btnUpDown;
        private Framework.SmartControls.SmartTabControl tabHold;
        private DevExpress.XtraTab.XtraTabPage tpHoldVerify;
        private Framework.SmartControls.SmartBandedGrid grdWIP;
        private DevExpress.XtraTab.XtraTabPage tpScrap;
        private Framework.SmartControls.SmartBandedGrid grdScrap;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
        private Framework.SmartControls.SmartLabelComboBox cboState;
        private Framework.SmartControls.SmartLabelSelectPopupEdit txtDefectCode;
    }
}