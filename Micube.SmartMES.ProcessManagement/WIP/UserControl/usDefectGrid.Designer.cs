namespace Micube.SmartMES.ProcessManagement
{
    partial class usDefectGrid
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
            this.defectList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.pnlDefect = new System.Windows.Forms.Panel();
            this.lblImageYN = new Micube.Framework.SmartControls.SmartLabel();
            this.chkImageYN = new Micube.Framework.SmartControls.SmartCheckEdit();
            this.lblPCSQty = new Micube.Framework.SmartControls.SmartLabel();
            this.lblPNLQty = new Micube.Framework.SmartControls.SmartLabel();
            this.txtPCSQty = new Micube.Framework.SmartControls.SmartSpinEdit();
            this.txtPNLQty = new Micube.Framework.SmartControls.SmartSpinEdit();
            this.btnDefectSearch = new Micube.Framework.SmartControls.SmartButton();
            this.txtQCSegment = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtDefectCode = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlDefect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkImageYN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPCSQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPNLQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQCSegment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDefectCode.Properties)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // defectList
            // 
            this.defectList.Caption = "";
            this.defectList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.defectList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.defectList.IsUsePaging = false;
            this.defectList.LanguageKey = "DEFECT";
            this.defectList.Location = new System.Drawing.Point(0, 30);
            this.defectList.Margin = new System.Windows.Forms.Padding(0);
            this.defectList.Name = "defectList";
            this.defectList.ShowBorder = true;
            this.defectList.ShowStatusBar = false;
            this.defectList.Size = new System.Drawing.Size(883, 111);
            this.defectList.TabIndex = 6;
            this.defectList.UseAutoBestFitColumns = false;
            // 
            // pnlDefect
            // 
            this.pnlDefect.Controls.Add(this.tableLayoutPanel1);
            this.pnlDefect.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDefect.Location = new System.Drawing.Point(0, 0);
            this.pnlDefect.Name = "pnlDefect";
            this.pnlDefect.Size = new System.Drawing.Size(883, 30);
            this.pnlDefect.TabIndex = 6;
            // 
            // lblImageYN
            // 
            this.lblImageYN.LanguageKey = "PHOTOREGISTRATIONYN";
            this.lblImageYN.Location = new System.Drawing.Point(743, 3);
            this.lblImageYN.Name = "lblImageYN";
            this.lblImageYN.Size = new System.Drawing.Size(0, 14);
            this.lblImageYN.TabIndex = 7;
            this.lblImageYN.Visible = false;
            // 
            // chkImageYN
            // 
            this.chkImageYN.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkImageYN.EditValue = true;
            this.chkImageYN.LabelText = null;
            this.chkImageYN.LanguageKey = "PHOTOREGISTRATIONYN";
            this.chkImageYN.Location = new System.Drawing.Point(763, 5);
            this.chkImageYN.Name = "chkImageYN";
            this.chkImageYN.Properties.Caption = "";
            this.chkImageYN.Size = new System.Drawing.Size(117, 19);
            this.chkImageYN.TabIndex = 6;
            // 
            // lblPCSQty
            // 
            this.lblPCSQty.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPCSQty.Location = new System.Drawing.Point(553, 8);
            this.lblPCSQty.Name = "lblPCSQty";
            this.lblPCSQty.Size = new System.Drawing.Size(21, 14);
            this.lblPCSQty.TabIndex = 0;
            this.lblPCSQty.Text = "PCS";
            // 
            // lblPNLQty
            // 
            this.lblPNLQty.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPNLQty.Location = new System.Drawing.Point(443, 8);
            this.lblPNLQty.Name = "lblPNLQty";
            this.lblPNLQty.Size = new System.Drawing.Size(21, 14);
            this.lblPNLQty.TabIndex = 0;
            this.lblPNLQty.Text = "PNL";
            // 
            // txtPCSQty
            // 
            this.txtPCSQty.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPCSQty.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPCSQty.LabelText = null;
            this.txtPCSQty.LanguageKey = null;
            this.txtPCSQty.Location = new System.Drawing.Point(583, 5);
            this.txtPCSQty.Name = "txtPCSQty";
            this.txtPCSQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtPCSQty.Size = new System.Drawing.Size(74, 20);
            this.txtPCSQty.TabIndex = 4;
            // 
            // txtPNLQty
            // 
            this.txtPNLQty.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPNLQty.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPNLQty.LabelText = null;
            this.txtPNLQty.LanguageKey = null;
            this.txtPNLQty.Location = new System.Drawing.Point(473, 5);
            this.txtPNLQty.Name = "txtPNLQty";
            this.txtPNLQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtPNLQty.Size = new System.Drawing.Size(74, 20);
            this.txtPNLQty.TabIndex = 3;
            // 
            // btnDefectSearch
            // 
            this.btnDefectSearch.AllowFocus = false;
            this.btnDefectSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnDefectSearch.IsBusy = false;
            this.btnDefectSearch.IsWrite = false;
            this.btnDefectSearch.LanguageKey = "ADD";
            this.btnDefectSearch.Location = new System.Drawing.Point(663, 6);
            this.btnDefectSearch.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnDefectSearch.Name = "btnDefectSearch";
            this.btnDefectSearch.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnDefectSearch.Size = new System.Drawing.Size(74, 18);
            this.btnDefectSearch.TabIndex = 5;
            this.btnDefectSearch.Text = "추가";
            this.btnDefectSearch.TooltipLanguageKey = "";
            // 
            // txtQCSegment
            // 
            this.txtQCSegment.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtQCSegment.LabelText = "품질공정";
            this.txtQCSegment.LanguageKey = "QCSEGMENTID";
            this.txtQCSegment.Location = new System.Drawing.Point(223, 5);
            this.txtQCSegment.Name = "txtQCSegment";
            this.txtQCSegment.Size = new System.Drawing.Size(214, 20);
            this.txtQCSegment.TabIndex = 2;
            // 
            // txtDefectCode
            // 
            this.txtDefectCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtDefectCode.LabelText = "불량코드";
            this.txtDefectCode.LanguageKey = "DEFECTCODEID";
            this.txtDefectCode.Location = new System.Drawing.Point(3, 5);
            this.txtDefectCode.Name = "txtDefectCode";
            this.txtDefectCode.Size = new System.Drawing.Size(214, 20);
            this.txtDefectCode.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 9;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.txtDefectCode, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtQCSegment, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDefectSearch, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtPCSQty, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblPCSQty, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblPNLQty, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtPNLQty, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblImageYN, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkImageYN, 8, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(883, 30);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // usDefectGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.defectList);
            this.Controls.Add(this.pnlDefect);
            this.Name = "usDefectGrid";
            this.Size = new System.Drawing.Size(883, 141);
            this.pnlDefect.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkImageYN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPCSQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPNLQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQCSegment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDefectCode.Properties)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid defectList;
        private System.Windows.Forms.Panel pnlDefect;
        private Framework.SmartControls.SmartLabelTextBox txtQCSegment;
        private Framework.SmartControls.SmartLabelTextBox txtDefectCode;
        private Framework.SmartControls.SmartButton btnDefectSearch;
        private Framework.SmartControls.SmartLabel lblPCSQty;
        private Framework.SmartControls.SmartLabel lblPNLQty;
        private Framework.SmartControls.SmartSpinEdit txtPCSQty;
        private Framework.SmartControls.SmartSpinEdit txtPNLQty;
        private Framework.SmartControls.SmartCheckEdit chkImageYN;
        private Framework.SmartControls.SmartLabel lblImageYN;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
