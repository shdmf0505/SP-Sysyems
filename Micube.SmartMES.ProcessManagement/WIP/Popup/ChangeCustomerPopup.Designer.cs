namespace Micube.SmartMES.ProcessManagement
{
	partial class ChangeCustomerPopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeCustomerPopup));
            this.panel1 = new System.Windows.Forms.Panel();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
            this.schCusotmerID = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            this.grdPackingList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.cboCustomer = new Micube.Framework.SmartControls.SmartComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.schCusotmerID.Properties)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboCustomer.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.grdPackingList);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.panel3);
            this.pnlMain.Size = new System.Drawing.Size(603, 388);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.smartPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(603, 37);
            this.panel1.TabIndex = 0;
            // 
            // smartPanel1
            // 
            this.smartPanel1.Controls.Add(this.cboCustomer);
            this.smartPanel1.Controls.Add(this.btnSearch);
            this.smartPanel1.Controls.Add(this.schCusotmerID);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(3, 3);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.smartPanel1.Size = new System.Drawing.Size(597, 31);
            this.smartPanel1.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.ImageOptions.Image")));
            this.btnSearch.IsBusy = false;
            this.btnSearch.IsWrite = false;
            this.btnSearch.Location = new System.Drawing.Point(483, 5);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSearch.Size = new System.Drawing.Size(100, 20);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "조회";
            this.btnSearch.TooltipLanguageKey = "";
            // 
            // schCusotmerID
            // 
            this.schCusotmerID.LabelText = "고객코드";
            this.schCusotmerID.LanguageKey = null;
            this.schCusotmerID.Location = new System.Drawing.Point(5, 5);
            this.schCusotmerID.Name = "schCusotmerID";
            this.schCusotmerID.Size = new System.Drawing.Size(219, 20);
            this.schCusotmerID.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnClose);
            this.panel3.Controls.Add(this.btnSave);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 335);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(603, 53);
            this.panel3.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.ImageOptions.Image")));
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(508, 3);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(92, 46);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "닫기";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.LanguageKey = "Save";
            this.btnSave.Location = new System.Drawing.Point(405, 3);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(97, 46);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "저장";
            this.btnSave.TooltipLanguageKey = "";
            // 
            // grdPackingList
            // 
            this.grdPackingList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdPackingList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPackingList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdPackingList.IsUsePaging = false;
            this.grdPackingList.LanguageKey = "WIPLIST";
            this.grdPackingList.Location = new System.Drawing.Point(0, 37);
            this.grdPackingList.Margin = new System.Windows.Forms.Padding(0);
            this.grdPackingList.Name = "grdPackingList";
            this.grdPackingList.ShowBorder = true;
            this.grdPackingList.Size = new System.Drawing.Size(603, 298);
            this.grdPackingList.TabIndex = 3;
            this.grdPackingList.UseAutoBestFitColumns = false;
            // 
            // cboCustomer
            // 
            this.cboCustomer.LabelText = null;
            this.cboCustomer.LanguageKey = null;
            this.cboCustomer.Location = new System.Drawing.Point(230, 5);
            this.cboCustomer.Name = "cboCustomer";
            this.cboCustomer.PopupWidth = 0;
            this.cboCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCustomer.Properties.NullText = "";
            this.cboCustomer.ShowHeader = true;
            this.cboCustomer.Size = new System.Drawing.Size(247, 20);
            this.cboCustomer.TabIndex = 7;
            this.cboCustomer.VisibleColumns = null;
            this.cboCustomer.VisibleColumnsWidth = null;
            // 
            // ChangeCustomerPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 408);
            this.LanguageKey = "LABELVIEW";
            this.Name = "ChangeCustomerPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "고객변경";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.schCusotmerID.Properties)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboCustomer.Properties)).EndInit();
            this.ResumeLayout(false);

		}

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartButton btnSearch;
        private Framework.SmartControls.SmartLabelTextBox schCusotmerID;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartBandedGrid grdPackingList;
        private Framework.SmartControls.SmartComboBox cboCustomer;
    }
}