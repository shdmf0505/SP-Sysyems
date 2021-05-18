namespace Micube.SmartMES.ProcessManagement
{
	partial class SampleRoutingResourcePopup
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
            this.btnOk = new Micube.Framework.SmartControls.SmartButton();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.txtEquipmentClass = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblEquipmentClass = new Micube.Framework.SmartControls.SmartLabel();
            this.txtArea = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblArea = new Micube.Framework.SmartControls.SmartLabel();
            this.txtResource = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblResource = new Micube.Framework.SmartControls.SmartLabel();
            this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdResource = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEquipmentClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResource.Properties)).BeginInit();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(791, 442);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Controls.Add(this.btnOk);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 418);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(791, 24);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.IsBusy = false;
            this.btnCancel.IsWrite = false;
            this.btnCancel.LanguageKey = "CANCEL";
            this.btnCancel.Location = new System.Drawing.Point(716, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "취소";
            this.btnCancel.TooltipLanguageKey = "";
            // 
            // btnOk
            // 
            this.btnOk.AllowFocus = false;
            this.btnOk.IsBusy = false;
            this.btnOk.IsWrite = false;
            this.btnOk.LanguageKey = "OK";
            this.btnOk.Location = new System.Drawing.Point(638, 0);
            this.btnOk.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnOk.Name = "btnOk";
            this.btnOk.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "확인";
            this.btnOk.TooltipLanguageKey = "";
            // 
            // smartPanel1
            // 
            this.smartPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel1.Controls.Add(this.txtEquipmentClass);
            this.smartPanel1.Controls.Add(this.lblEquipmentClass);
            this.smartPanel1.Controls.Add(this.txtArea);
            this.smartPanel1.Controls.Add(this.lblArea);
            this.smartPanel1.Controls.Add(this.txtResource);
            this.smartPanel1.Controls.Add(this.lblResource);
            this.smartPanel1.Controls.Add(this.btnSearch);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(791, 24);
            this.smartPanel1.TabIndex = 1;
            // 
            // txtEquipmentClass
            // 
            this.txtEquipmentClass.LabelText = null;
            this.txtEquipmentClass.LanguageKey = null;
            this.txtEquipmentClass.Location = new System.Drawing.Point(491, 2);
            this.txtEquipmentClass.Name = "txtEquipmentClass";
            this.txtEquipmentClass.Size = new System.Drawing.Size(114, 20);
            this.txtEquipmentClass.TabIndex = 7;
            // 
            // lblEquipmentClass
            // 
            this.lblEquipmentClass.LanguageKey = "EQUIPMENTGROUP";
            this.lblEquipmentClass.Location = new System.Drawing.Point(429, 5);
            this.lblEquipmentClass.Margin = new System.Windows.Forms.Padding(0);
            this.lblEquipmentClass.Name = "lblEquipmentClass";
            this.lblEquipmentClass.Size = new System.Drawing.Size(40, 14);
            this.lblEquipmentClass.TabIndex = 6;
            this.lblEquipmentClass.Text = "설비그룹";
            // 
            // txtArea
            // 
            this.txtArea.LabelText = null;
            this.txtArea.LanguageKey = null;
            this.txtArea.Location = new System.Drawing.Point(282, 2);
            this.txtArea.Name = "txtArea";
            this.txtArea.Size = new System.Drawing.Size(114, 20);
            this.txtArea.TabIndex = 7;
            // 
            // lblArea
            // 
            this.lblArea.LanguageKey = "AREA";
            this.lblArea.Location = new System.Drawing.Point(220, 5);
            this.lblArea.Margin = new System.Windows.Forms.Padding(0);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(30, 14);
            this.lblArea.TabIndex = 6;
            this.lblArea.Text = "작업장";
            // 
            // txtResource
            // 
            this.txtResource.LabelText = null;
            this.txtResource.LanguageKey = null;
            this.txtResource.Location = new System.Drawing.Point(83, 2);
            this.txtResource.Name = "txtResource";
            this.txtResource.Size = new System.Drawing.Size(112, 20);
            this.txtResource.TabIndex = 7;
            // 
            // lblResource
            // 
            this.lblResource.LanguageKey = "RESOURCE";
            this.lblResource.Location = new System.Drawing.Point(6, 5);
            this.lblResource.Margin = new System.Windows.Forms.Padding(0);
            this.lblResource.Name = "lblResource";
            this.lblResource.Size = new System.Drawing.Size(20, 14);
            this.lblResource.TabIndex = 6;
            this.lblResource.Text = "자원";
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.IsBusy = false;
            this.btnSearch.IsWrite = false;
            this.btnSearch.LanguageKey = "SEARCH";
            this.btnSearch.Location = new System.Drawing.Point(716, 1);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "검색";
            this.btnSearch.TooltipLanguageKey = "";
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdResource, 0, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartPanel1, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 4);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 5;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(791, 442);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // grdResource
            // 
            this.grdResource.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdResource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdResource.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdResource.IsUsePaging = false;
            this.grdResource.LanguageKey = "RESOURCE";
            this.grdResource.Location = new System.Drawing.Point(0, 35);
            this.grdResource.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.grdResource.Name = "grdResource";
            this.grdResource.ShowBorder = true;
            this.grdResource.Size = new System.Drawing.Size(791, 378);
            this.grdResource.TabIndex = 4;
            // 
            // SampleRoutingResourcePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 462);
            this.LanguageKey = "RESOURCE";
            this.Name = "SampleRoutingResourcePopup";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.smartPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEquipmentClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResource.Properties)).EndInit();
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartButton btnSearch;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Framework.SmartControls.SmartButton btnCancel;
        private Framework.SmartControls.SmartButton btnOk;
        private Framework.SmartControls.SmartTextBox txtArea;
        private Framework.SmartControls.SmartLabel lblArea;
        private Framework.SmartControls.SmartTextBox txtResource;
        private Framework.SmartControls.SmartLabel lblResource;
        private Framework.SmartControls.SmartBandedGrid grdResource;
        private Framework.SmartControls.SmartTextBox txtEquipmentClass;
        private Framework.SmartControls.SmartLabel lblEquipmentClass;
    }
}
