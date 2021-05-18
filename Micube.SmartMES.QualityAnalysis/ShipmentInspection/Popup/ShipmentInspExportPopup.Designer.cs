namespace Micube.SmartMES.QualityAnalysis
{
    partial class ShipmentInspExportPopup
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.shtExport = new Micube.Framework.SmartControls.SmartSpreadSheet();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtLotNo = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtInspectionDate = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtCustomer = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtProductModel = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lblRevision = new Micube.Framework.SmartControls.SmartLabel();
            this.cboExportRev = new Micube.Framework.SmartControls.SmartComboBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            this.btnExport = new Micube.Framework.SmartControls.SmartButton();
            this.btnReload = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInspectionDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductModel.Properties)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboExportRev.Properties)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(1422, 641);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.shtExport, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1422, 641);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // shtExport
            // 
            this.shtExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shtExport.Location = new System.Drawing.Point(3, 73);
            this.shtExport.Name = "shtExport";
            this.shtExport.Options.Import.Csv.Encoding = null;
            this.shtExport.Options.Import.Txt.Encoding = null;
            this.shtExport.Size = new System.Drawing.Size(1416, 565);
            this.shtExport.TabIndex = 104;
            this.shtExport.Text = "smartSpreadSheet1";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.txtLotNo, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtInspectionDate, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtCustomer, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtProductModel, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 4, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 41);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1416, 26);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // txtLotNo
            // 
            this.txtLotNo.AutoHeight = false;
            this.txtLotNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLotNo.EditorWidth = "80%";
            this.txtLotNo.LanguageKey = "LOTID";
            this.txtLotNo.Location = new System.Drawing.Point(571, 0);
            this.txtLotNo.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.txtLotNo.Name = "txtLotNo";
            this.txtLotNo.Properties.AutoHeight = false;
            this.txtLotNo.Properties.ReadOnly = true;
            this.txtLotNo.Properties.UseReadOnlyAppearance = false;
            this.txtLotNo.Size = new System.Drawing.Size(278, 26);
            this.txtLotNo.TabIndex = 6;
            // 
            // txtInspectionDate
            // 
            this.txtInspectionDate.AutoHeight = false;
            this.txtInspectionDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInspectionDate.EditorWidth = "80%";
            this.txtInspectionDate.LanguageKey = "INSPECTDATE";
            this.txtInspectionDate.Location = new System.Drawing.Point(854, 0);
            this.txtInspectionDate.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.txtInspectionDate.Name = "txtInspectionDate";
            this.txtInspectionDate.Properties.AutoHeight = false;
            this.txtInspectionDate.Properties.ReadOnly = true;
            this.txtInspectionDate.Properties.UseReadOnlyAppearance = false;
            this.txtInspectionDate.Size = new System.Drawing.Size(278, 26);
            this.txtInspectionDate.TabIndex = 4;
            // 
            // txtCustomer
            // 
            this.txtCustomer.AutoHeight = false;
            this.txtCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCustomer.EditorWidth = "80%";
            this.txtCustomer.LanguageKey = "CUSTOMERNAME";
            this.txtCustomer.Location = new System.Drawing.Point(5, 0);
            this.txtCustomer.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Properties.AutoHeight = false;
            this.txtCustomer.Properties.ReadOnly = true;
            this.txtCustomer.Properties.UseReadOnlyAppearance = false;
            this.txtCustomer.Size = new System.Drawing.Size(278, 26);
            this.txtCustomer.TabIndex = 3;
            // 
            // txtProductModel
            // 
            this.txtProductModel.AutoHeight = false;
            this.txtProductModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductModel.EditorWidth = "80%";
            this.txtProductModel.LanguageKey = "PRODUCTMODEL";
            this.txtProductModel.Location = new System.Drawing.Point(288, 0);
            this.txtProductModel.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.txtProductModel.Name = "txtProductModel";
            this.txtProductModel.Properties.AutoHeight = false;
            this.txtProductModel.Properties.ReadOnly = true;
            this.txtProductModel.Properties.UseReadOnlyAppearance = false;
            this.txtProductModel.Size = new System.Drawing.Size(278, 26);
            this.txtProductModel.TabIndex = 2;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.lblRevision, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.cboExportRev, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(1132, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(284, 26);
            this.tableLayoutPanel4.TabIndex = 7;
            // 
            // lblRevision
            // 
            this.lblRevision.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRevision.LanguageKey = "REVISION";
            this.lblRevision.Location = new System.Drawing.Point(0, 0);
            this.lblRevision.Margin = new System.Windows.Forms.Padding(0);
            this.lblRevision.Name = "lblRevision";
            this.lblRevision.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblRevision.Size = new System.Drawing.Size(142, 26);
            this.lblRevision.TabIndex = 7;
            this.lblRevision.Text = "Label";
            // 
            // cboExportRev
            // 
            this.cboExportRev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboExportRev.LabelText = null;
            this.cboExportRev.LanguageKey = null;
            this.cboExportRev.Location = new System.Drawing.Point(142, 6);
            this.cboExportRev.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.cboExportRev.Name = "cboExportRev";
            this.cboExportRev.PopupWidth = 0;
            this.cboExportRev.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboExportRev.Properties.NullText = "";
            this.cboExportRev.ShowHeader = true;
            this.cboExportRev.Size = new System.Drawing.Size(142, 24);
            this.cboExportRev.TabIndex = 6;
            this.cboExportRev.VisibleColumns = null;
            this.cboExportRev.VisibleColumnsWidth = null;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.Controls.Add(this.btnExport, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnSave, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnReload, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1416, 32);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(1219, 3);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(94, 29);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "저장";
            this.btnSave.TooltipLanguageKey = "";
            // 
            // btnExport
            // 
            this.btnExport.AllowFocus = false;
            this.btnExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExport.IsBusy = false;
            this.btnExport.IsWrite = false;
            this.btnExport.Location = new System.Drawing.Point(1319, 3);
            this.btnExport.Margin = new System.Windows.Forms.Padding(3, 3, 15, 0);
            this.btnExport.Name = "btnExport";
            this.btnExport.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnExport.Size = new System.Drawing.Size(82, 29);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "내보내기";
            this.btnExport.TooltipLanguageKey = "";
            // 
            // btnReload
            // 
            this.btnReload.AllowFocus = false;
            this.btnReload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReload.IsBusy = false;
            this.btnReload.IsWrite = false;
            this.btnReload.LanguageKey = "RELOAD";
            this.btnReload.Location = new System.Drawing.Point(1119, 3);
            this.btnReload.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.btnReload.Name = "btnReload";
            this.btnReload.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnReload.Size = new System.Drawing.Size(94, 29);
            this.btnReload.TabIndex = 6;
            this.btnReload.Text = "새로고침";
            this.btnReload.TooltipLanguageKey = "";
            // 
            // ShipmentInspExportPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1442, 661);
            this.Name = "ShipmentInspExportPopup";
            this.Text = "출하검사 성적서 내보내기";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtLotNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInspectionDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductModel.Properties)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboExportRev.Properties)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Framework.SmartControls.SmartLabelTextBox txtInspectionDate;
        private Framework.SmartControls.SmartLabelTextBox txtCustomer;
        private Framework.SmartControls.SmartLabelTextBox txtProductModel;
        private Framework.SmartControls.SmartLabelTextBox txtLotNo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private Framework.SmartControls.SmartComboBox cboExportRev;
        private Framework.SmartControls.SmartLabel lblRevision;
        private Framework.SmartControls.SmartSpreadSheet shtExport;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartButton btnExport;
        private Framework.SmartControls.SmartButton btnReload;
    }
}