namespace Micube.SmartMES.StandardInfo
{
    partial class SelectWIPPopup
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
            this.grdWip = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.ucDataLeftRightBtn1 = new Micube.SmartMES.Commons.Controls.ucDataLeftRightBtn();
            this.grdSelectedWIP = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.txtSearch = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
            this.btnOK = new Micube.Framework.SmartControls.SmartButton();
            this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.btnOK);
            this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(885, 548);
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 3;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdWip, 0, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.ucDataLeftRightBtn1, 1, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdSelectedWIP, 2, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartPanel1, 0, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 2;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(885, 504);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // grdWip
            // 
            this.grdWip.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdWip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdWip.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdWip.IsUsePaging = false;
            this.grdWip.LanguageKey = null;
            this.grdWip.Location = new System.Drawing.Point(0, 30);
            this.grdWip.Margin = new System.Windows.Forms.Padding(0);
            this.grdWip.Name = "grdWip";
            this.grdWip.ShowBorder = true;
            this.grdWip.ShowButtonBar = false;
            this.grdWip.Size = new System.Drawing.Size(400, 474);
            this.grdWip.TabIndex = 0;
            this.grdWip.UseAutoBestFitColumns = false;
            // 
            // ucDataLeftRightBtn1
            // 
            this.ucDataLeftRightBtn1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDataLeftRightBtn1.Location = new System.Drawing.Point(403, 33);
            this.ucDataLeftRightBtn1.Name = "ucDataLeftRightBtn1";
            this.ucDataLeftRightBtn1.Size = new System.Drawing.Size(54, 468);
            this.ucDataLeftRightBtn1.SourceGrid = null;
            this.ucDataLeftRightBtn1.TabIndex = 1;
            this.ucDataLeftRightBtn1.TargetGrid = null;
            // 
            // grdSelectedWIP
            // 
            this.grdSelectedWIP.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdSelectedWIP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSelectedWIP.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdSelectedWIP.IsUsePaging = false;
            this.grdSelectedWIP.LanguageKey = null;
            this.grdSelectedWIP.Location = new System.Drawing.Point(460, 30);
            this.grdSelectedWIP.Margin = new System.Windows.Forms.Padding(0);
            this.grdSelectedWIP.Name = "grdSelectedWIP";
            this.grdSelectedWIP.ShowBorder = true;
            this.grdSelectedWIP.ShowButtonBar = false;
            this.grdSelectedWIP.Size = new System.Drawing.Size(425, 474);
            this.grdSelectedWIP.TabIndex = 1;
            this.grdSelectedWIP.UseAutoBestFitColumns = false;
            // 
            // smartPanel1
            // 
            this.smartPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel1.Controls.Add(this.txtSearch);
            this.smartPanel1.Controls.Add(this.btnSearch);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(400, 30);
            this.smartPanel1.TabIndex = 6;
            // 
            // txtSearch
            // 
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtSearch.LanguageKey = "SEARCH";
            this.txtSearch.Location = new System.Drawing.Point(0, 0);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(0);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(220, 20);
            this.txtSearch.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSearch.IsBusy = false;
            this.btnSearch.IsWrite = false;
            this.btnSearch.LanguageKey = "SEARCH";
            this.btnSearch.Location = new System.Drawing.Point(320, 0);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(0);
            this.btnSearch.MaximumSize = new System.Drawing.Size(80, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSearch.Size = new System.Drawing.Size(80, 20);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "검색";
            this.btnSearch.TooltipLanguageKey = "";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnOK
            // 
            this.btnOK.AllowFocus = false;
            this.btnOK.IsBusy = false;
            this.btnOK.IsWrite = false;
            this.btnOK.LanguageKey = "OK";
            this.btnOK.Location = new System.Drawing.Point(331, 516);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnOK.Size = new System.Drawing.Size(80, 25);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "smartButton1";
            this.btnOK.TooltipLanguageKey = "";
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.IsBusy = false;
            this.btnCancel.IsWrite = false;
            this.btnCancel.LanguageKey = "CANCEL";
            this.btnCancel.Location = new System.Drawing.Point(460, 516);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "smartButton1";
            this.btnCancel.TooltipLanguageKey = "";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SelectWIPPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 568);
            this.Name = "SelectWIPPopup";
            this.Text = "SelectWIPPopup";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartBandedGrid grdWip;
        private Framework.SmartControls.SmartBandedGrid grdSelectedWIP;
        private Micube.SmartMES.Commons.Controls.ucDataLeftRightBtn ucDataLeftRightBtn1;
        private Framework.SmartControls.SmartButton btnCancel;
        private Framework.SmartControls.SmartButton btnOK;
        private Framework.SmartControls.SmartLabelTextBox txtSearch;
        private Framework.SmartControls.SmartButton btnSearch;
        private Framework.SmartControls.SmartPanel smartPanel1;
    }
}