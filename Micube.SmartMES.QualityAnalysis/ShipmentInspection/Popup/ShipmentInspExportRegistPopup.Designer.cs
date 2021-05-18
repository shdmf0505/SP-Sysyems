namespace Micube.SmartMES.QualityAnalysis
{
    partial class ShipmentInspExportRegistPopup
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
            this.grdLot = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.txtLotNo = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblLotNo = new Micube.Framework.SmartControls.SmartLabel();
            this.lblProductDefID = new Micube.Framework.SmartControls.SmartLabel();
            this.txtProductDefID = new Micube.Framework.SmartControls.SmartTextBox();
            this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductDefID.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(764, 541);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.grdLot, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(764, 541);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // grdLot
            // 
            this.grdLot.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLot.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLot.IsUsePaging = false;
            this.grdLot.LanguageKey = null;
            this.grdLot.Location = new System.Drawing.Point(0, 32);
            this.grdLot.Margin = new System.Windows.Forms.Padding(0);
            this.grdLot.Name = "grdLot";
            this.grdLot.ShowBorder = true;
            this.grdLot.Size = new System.Drawing.Size(764, 476);
            this.grdLot.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.btnClose, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSave, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 508);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(764, 33);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.LanguageKey = "CLEAR";
            this.btnClose.Location = new System.Drawing.Point(659, 5);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(100, 28);
            this.btnClose.TabIndex = 4;
            this.btnClose.TooltipLanguageKey = "";
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.LanguageKey = "CLEAR";
            this.btnSave.Location = new System.Drawing.Point(510, 5);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(100, 28);
            this.btnSave.TabIndex = 3;
            this.btnSave.TooltipLanguageKey = "";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tableLayoutPanel3.Controls.Add(this.txtLotNo, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblLotNo, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblProductDefID, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtProductDefID, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnSearch, 4, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(764, 27);
            this.tableLayoutPanel3.TabIndex = 6;
            // 
            // txtLotNo
            // 
            this.txtLotNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLotNo.LabelText = null;
            this.txtLotNo.LanguageKey = null;
            this.txtLotNo.Location = new System.Drawing.Point(114, 0);
            this.txtLotNo.Margin = new System.Windows.Forms.Padding(0);
            this.txtLotNo.Name = "txtLotNo";
            this.txtLotNo.Properties.AutoHeight = false;
            this.txtLotNo.Size = new System.Drawing.Size(198, 27);
            this.txtLotNo.TabIndex = 8;
            // 
            // lblLotNo
            // 
            this.lblLotNo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblLotNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLotNo.LanguageKey = "PROCESSSEGMENT";
            this.lblLotNo.Location = new System.Drawing.Point(5, 0);
            this.lblLotNo.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblLotNo.Name = "lblLotNo";
            this.lblLotNo.Size = new System.Drawing.Size(109, 27);
            this.lblLotNo.TabIndex = 3;
            this.lblLotNo.Text = "smartLabel3";
            // 
            // lblProductDefID
            // 
            this.lblProductDefID.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblProductDefID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductDefID.LanguageKey = "PROCESSSEGMENT";
            this.lblProductDefID.Location = new System.Drawing.Point(317, 0);
            this.lblProductDefID.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblProductDefID.Name = "lblProductDefID";
            this.lblProductDefID.Size = new System.Drawing.Size(109, 27);
            this.lblProductDefID.TabIndex = 4;
            this.lblProductDefID.Text = "smartLabel3";
            // 
            // txtProductDefID
            // 
            this.txtProductDefID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductDefID.LabelText = null;
            this.txtProductDefID.LanguageKey = null;
            this.txtProductDefID.Location = new System.Drawing.Point(426, 0);
            this.txtProductDefID.Margin = new System.Windows.Forms.Padding(0);
            this.txtProductDefID.Name = "txtProductDefID";
            this.txtProductDefID.Properties.AutoHeight = false;
            this.txtProductDefID.Size = new System.Drawing.Size(198, 27);
            this.txtProductDefID.TabIndex = 9;
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSearch.IsBusy = false;
            this.btnSearch.IsWrite = false;
            this.btnSearch.LanguageKey = "SEARCH";
            this.btnSearch.Location = new System.Drawing.Point(659, 0);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSearch.Size = new System.Drawing.Size(100, 27);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "검색";
            this.btnSearch.TooltipLanguageKey = "";
            // 
            // ShipmentInspExportRegistPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Name = "ShipmentInspExportRegistPopup";
            this.Text = "출하검사 Lot 등록";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductDefID.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartBandedGrid grdLot;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Framework.SmartControls.SmartLabel lblLotNo;
        private Framework.SmartControls.SmartLabel lblProductDefID;
        private Framework.SmartControls.SmartTextBox txtLotNo;
        private Framework.SmartControls.SmartTextBox txtProductDefID;
        private Framework.SmartControls.SmartButton btnSearch;
    }
}