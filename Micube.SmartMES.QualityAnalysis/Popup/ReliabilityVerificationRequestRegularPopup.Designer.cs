namespace Micube.SmartMES.QualityAnalysis
{
    partial class ReliabilityVerificationRequestRegularPopup
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
            this.tlpMainTable = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.manufacturingHistoryControl1 = new Micube.SmartMES.QualityAnalysis.ManufacturingHistoryControl();
            this.grdProductnformation = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            this.grdInspection = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdMeasuredValue = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.tlpMainTable.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tlpMainTable);
            this.pnlMain.Size = new System.Drawing.Size(1244, 705);
            // 
            // tlpMainTable
            // 
            this.tlpMainTable.ColumnCount = 2;
            this.tlpMainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMainTable.Controls.Add(this.manufacturingHistoryControl1, 0, 1);
            this.tlpMainTable.Controls.Add(this.grdProductnformation, 0, 0);
            this.tlpMainTable.Controls.Add(this.flowLayoutPanel1, 1, 3);
            this.tlpMainTable.Controls.Add(this.grdInspection, 0, 2);
            this.tlpMainTable.Controls.Add(this.grdMeasuredValue, 1, 2);
            this.tlpMainTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainTable.Location = new System.Drawing.Point(0, 0);
            this.tlpMainTable.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tlpMainTable.Name = "tlpMainTable";
            this.tlpMainTable.RowCount = 4;
            this.tlpMainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.33203F));
            this.tlpMainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44.33443F));
            this.tlpMainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.33353F));
            this.tlpMainTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMainTable.Size = new System.Drawing.Size(1244, 705);
            this.tlpMainTable.TabIndex = 0;
            // 
            // manufacturingHistoryControl1
            // 
            this.tlpMainTable.SetColumnSpan(this.manufacturingHistoryControl1, 2);
            this.manufacturingHistoryControl1.CurrentDataRow = null;
            this.manufacturingHistoryControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.manufacturingHistoryControl1.Location = new System.Drawing.Point(3, 139);
            this.manufacturingHistoryControl1.Name = "manufacturingHistoryControl1";
            this.manufacturingHistoryControl1.Size = new System.Drawing.Size(1238, 291);
            this.manufacturingHistoryControl1.TabIndex = 7;
            // 
            // grdProductnformation
            // 
            this.grdProductnformation.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.tlpMainTable.SetColumnSpan(this.grdProductnformation, 2);
            this.grdProductnformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProductnformation.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdProductnformation.IsUsePaging = false;
            this.grdProductnformation.LanguageKey = "PRODUCTINFORMATION";
            this.grdProductnformation.Location = new System.Drawing.Point(0, 0);
            this.grdProductnformation.Margin = new System.Windows.Forms.Padding(0);
            this.grdProductnformation.Name = "grdProductnformation";
            this.grdProductnformation.ShowBorder = true;
            this.grdProductnformation.Size = new System.Drawing.Size(1244, 136);
            this.grdProductnformation.TabIndex = 3;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.btnClose);
            this.flowLayoutPanel1.Controls.Add(this.btnSave);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1080, 679);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(164, 26);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.btnClose.Appearance.Options.UseBackColor = true;
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(89, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "smartButton1";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.btnSave.Appearance.Options.UseBackColor = true;
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = true;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(3, 0);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "smartButton2";
            this.btnSave.TooltipLanguageKey = "";
            // 
            // grdInspection
            // 
            this.grdInspection.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdInspection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInspection.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInspection.IsUsePaging = false;
            this.grdInspection.LanguageKey = null;
            this.grdInspection.Location = new System.Drawing.Point(0, 433);
            this.grdInspection.Margin = new System.Windows.Forms.Padding(0);
            this.grdInspection.Name = "grdInspection";
            this.grdInspection.ShowBorder = true;
            this.grdInspection.Size = new System.Drawing.Size(622, 236);
            this.grdInspection.TabIndex = 8;
            // 
            // grdMeasuredValue
            // 
            this.grdMeasuredValue.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdMeasuredValue.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdMeasuredValue.IsUsePaging = false;
            this.grdMeasuredValue.LanguageKey = null;
            this.grdMeasuredValue.Location = new System.Drawing.Point(622, 433);
            this.grdMeasuredValue.Margin = new System.Windows.Forms.Padding(0);
            this.grdMeasuredValue.Name = "grdMeasuredValue";
            this.grdMeasuredValue.ShowBorder = true;
            this.grdMeasuredValue.Size = new System.Drawing.Size(622, 236);
            this.grdMeasuredValue.TabIndex = 9;
            // 
            // ReliabilityVerificationRequestRegularPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 725);
            this.LanguageKey = "REGISTMODIFYRELIABVERIFIREQREG";
            this.Name = "ReliabilityVerificationRequestRegularPopup";
            this.Text = "신뢰성 검증 의뢰 등록/수정(정기)";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.tlpMainTable.ResumeLayout(false);
            this.tlpMainTable.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel tlpMainTable;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private ManufacturingHistoryControl manufacturingHistoryControl1;
        private Framework.SmartControls.SmartBandedGrid grdProductnformation;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartBandedGrid grdInspection;
        private Framework.SmartControls.SmartBandedGrid grdMeasuredValue;
    }
}