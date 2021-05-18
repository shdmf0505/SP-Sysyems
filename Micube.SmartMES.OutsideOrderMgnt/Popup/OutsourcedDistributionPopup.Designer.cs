namespace Micube.SmartMES.OutsideOrderMgnt.Popup
{
    partial class OutsourcedDistributionPopup
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
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdSegmentList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdProcessStatus = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
            this.smartPanel2 = new Micube.Framework.SmartControls.SmartPanel();
            this.smartLabel2 = new Micube.Framework.SmartControls.SmartLabel();
            this.smartPanel3 = new Micube.Framework.SmartControls.SmartPanel();
            this.smartLabel3 = new Micube.Framework.SmartControls.SmartLabel();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            this.btnSaveOK = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).BeginInit();
            this.smartPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel3)).BeginInit();
            this.smartPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(1044, 754);
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdSegmentList, 0, 5);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdLotList, 0, 3);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdProcessStatus, 0, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartPanel1, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartPanel2, 0, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartPanel3, 0, 4);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 6;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(1044, 754);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // grdSegmentList
            // 
            this.grdSegmentList.Caption = "";
            this.grdSegmentList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSegmentList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdSegmentList.IsUsePaging = false;
            this.grdSegmentList.LanguageKey = "OUTSOURCINGALLOCATIONSEGMENTLIST";
            this.grdSegmentList.Location = new System.Drawing.Point(0, 531);
            this.grdSegmentList.Margin = new System.Windows.Forms.Padding(0);
            this.grdSegmentList.Name = "grdSegmentList";
            this.grdSegmentList.ShowBorder = true;
            this.grdSegmentList.Size = new System.Drawing.Size(1044, 223);
            this.grdSegmentList.TabIndex = 115;
            // 
            // grdLotList
            // 
            this.grdLotList.Caption = "";
            this.grdLotList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLotList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLotList.IsUsePaging = false;
            this.grdLotList.LanguageKey = "OUTSOURCINGALLOCATIONLOTNOLIST";
            this.grdLotList.Location = new System.Drawing.Point(0, 270);
            this.grdLotList.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotList.Name = "grdLotList";
            this.grdLotList.ShowBorder = true;
            this.grdLotList.Size = new System.Drawing.Size(1044, 221);
            this.grdLotList.TabIndex = 113;
            // 
            // grdProcessStatus
            // 
            this.grdProcessStatus.Caption = "";
            this.grdProcessStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProcessStatus.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdProcessStatus.IsUsePaging = false;
            this.grdProcessStatus.LanguageKey = "PROCESSSTATUSBYSUBCONTRACTOR";
            this.grdProcessStatus.Location = new System.Drawing.Point(0, 40);
            this.grdProcessStatus.Margin = new System.Windows.Forms.Padding(0);
            this.grdProcessStatus.Name = "grdProcessStatus";
            this.grdProcessStatus.ShowBorder = true;
            this.grdProcessStatus.Size = new System.Drawing.Size(1044, 190);
            this.grdProcessStatus.TabIndex = 112;
            // 
            // smartPanel1
            // 
            this.smartPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel1.Controls.Add(this.smartLabel1);
            this.smartPanel1.Controls.Add(this.btnSearch);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(1044, 40);
            this.smartPanel1.TabIndex = 0;
            // 
            // smartLabel1
            // 
            this.smartLabel1.Appearance.Font = new System.Drawing.Font("Tahoma", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.smartLabel1.Appearance.Options.UseFont = true;
            this.smartLabel1.LanguageKey = "PROCESSSTATUSBYSUBCONTRACTOR";
            this.smartLabel1.Location = new System.Drawing.Point(14, 6);
            this.smartLabel1.Name = "smartLabel1";
            this.smartLabel1.Size = new System.Drawing.Size(68, 27);
            this.smartLabel1.TabIndex = 112;
            this.smartLabel1.Text = "공정현황";
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.IsBusy = false;
            this.btnSearch.IsWrite = false;
            this.btnSearch.LanguageKey = "SEARCH";
            this.btnSearch.Location = new System.Drawing.Point(949, 8);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSearch.Size = new System.Drawing.Size(80, 24);
            this.btnSearch.TabIndex = 111;
            this.btnSearch.Text = "조회";
            this.btnSearch.TooltipLanguageKey = "";
            // 
            // smartPanel2
            // 
            this.smartPanel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel2.Controls.Add(this.smartLabel2);
            this.smartPanel2.Controls.Add(this.btnSaveOK);
            this.smartPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel2.Location = new System.Drawing.Point(0, 230);
            this.smartPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel2.Name = "smartPanel2";
            this.smartPanel2.Size = new System.Drawing.Size(1044, 40);
            this.smartPanel2.TabIndex = 1;
            // 
            // smartLabel2
            // 
            this.smartLabel2.Appearance.Font = new System.Drawing.Font("Tahoma", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.smartLabel2.Appearance.Options.UseFont = true;
            this.smartLabel2.LanguageKey = "OUTSOURCINGALLOCATIONLOTNOLIST";
            this.smartLabel2.Location = new System.Drawing.Point(14, 7);
            this.smartLabel2.Name = "smartLabel2";
            this.smartLabel2.Size = new System.Drawing.Size(119, 27);
            this.smartLabel2.TabIndex = 112;
            this.smartLabel2.Text = "LOT NO 목록";
            // 
            // smartPanel3
            // 
            this.smartPanel3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel3.Controls.Add(this.smartLabel3);
            this.smartPanel3.Controls.Add(this.btnClose);
            this.smartPanel3.Controls.Add(this.btnSave);
            this.smartPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel3.Location = new System.Drawing.Point(0, 491);
            this.smartPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel3.Name = "smartPanel3";
            this.smartPanel3.Size = new System.Drawing.Size(1044, 40);
            this.smartPanel3.TabIndex = 114;
            // 
            // smartLabel3
            // 
            this.smartLabel3.Appearance.Font = new System.Drawing.Font("Tahoma", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.smartLabel3.Appearance.Options.UseFont = true;
            this.smartLabel3.LanguageKey = "OUTSOURCINGALLOCATIONSEGMENTLIST";
            this.smartLabel3.Location = new System.Drawing.Point(14, 10);
            this.smartLabel3.Name = "smartLabel3";
            this.smartLabel3.Size = new System.Drawing.Size(0, 27);
            this.smartLabel3.TabIndex = 112;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(951, 9);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(80, 24);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "smartButton1";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(865, 9);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(80, 24);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "smartButton2";
            this.btnSave.TooltipLanguageKey = "";
            // 
            // btnSaveOK
            // 
            this.btnSaveOK.AllowFocus = false;
            this.btnSaveOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveOK.IsBusy = false;
            this.btnSaveOK.IsWrite = false;
            this.btnSaveOK.LanguageKey = "OK";
            this.btnSaveOK.Location = new System.Drawing.Point(951, 7);
            this.btnSaveOK.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSaveOK.Name = "btnSaveOK";
            this.btnSaveOK.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSaveOK.Size = new System.Drawing.Size(80, 24);
            this.btnSaveOK.TabIndex = 8;
            this.btnSaveOK.Text = "smartButton2";
            this.btnSaveOK.TooltipLanguageKey = "";
            // 
            // OutsourcedDistributionPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 774);
            this.Name = "OutsourcedDistributionPopup";
            this.Text = "외주처변경";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.smartPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).EndInit();
            this.smartPanel2.ResumeLayout(false);
            this.smartPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel3)).EndInit();
            this.smartPanel3.ResumeLayout(false);
            this.smartPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartPanel smartPanel2;
        private Framework.SmartControls.SmartBandedGrid grdLotList;
        private Framework.SmartControls.SmartBandedGrid grdProcessStatus;
        private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartButton btnSearch;
        private Framework.SmartControls.SmartLabel smartLabel1;
        private Framework.SmartControls.SmartLabel smartLabel2;
        private Framework.SmartControls.SmartBandedGrid grdSegmentList;
        private Framework.SmartControls.SmartPanel smartPanel3;
        private Framework.SmartControls.SmartLabel smartLabel3;
        private Framework.SmartControls.SmartButton btnSaveOK;
    }
}