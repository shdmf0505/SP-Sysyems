namespace Micube.SmartMES.ProcessManagement
{
	partial class ProcessResultCreate
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
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            this.lblToProcess = new Micube.Framework.SmartControls.SmartLabel();
            this.cboResource = new Micube.Framework.SmartControls.SmartComboBox();
            this.lblFromProcess = new Micube.Framework.SmartControls.SmartLabel();
            this.cboToProcess = new Micube.Framework.SmartControls.SmartComboBox();
            this.cboFromProcess = new Micube.Framework.SmartControls.SmartComboBox();
            this.smartLabel2 = new Micube.Framework.SmartControls.SmartLabel();
            this.cboTargetCompany = new Micube.Framework.SmartControls.SmartComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboResource.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboToProcess.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFromProcess.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTargetCompany.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 397);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlToolbar.Size = new System.Drawing.Size(916, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.smartSplitTableLayoutPanel1, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdLotList);
            this.pnlContent.Controls.Add(this.smartPanel1);
            this.pnlContent.Size = new System.Drawing.Size(916, 401);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1221, 430);
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 2;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(47, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 1;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(869, 24);
            this.smartSplitTableLayoutPanel1.TabIndex = 5;
            // 
            // grdLotList
            // 
            this.grdLotList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLotList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLotList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLotList.IsUsePaging = false;
            this.grdLotList.LanguageKey = "GRIDLOTLIST";
            this.grdLotList.Location = new System.Drawing.Point(0, 29);
            this.grdLotList.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotList.Name = "grdLotList";
            this.grdLotList.ShowBorder = true;
            this.grdLotList.Size = new System.Drawing.Size(916, 372);
            this.grdLotList.TabIndex = 0;
            this.grdLotList.UseAutoBestFitColumns = false;
            // 
            // smartPanel1
            // 
            this.smartPanel1.Controls.Add(this.smartLabel2);
            this.smartPanel1.Controls.Add(this.cboTargetCompany);
            this.smartPanel1.Controls.Add(this.smartLabel1);
            this.smartPanel1.Controls.Add(this.lblToProcess);
            this.smartPanel1.Controls.Add(this.cboResource);
            this.smartPanel1.Controls.Add(this.lblFromProcess);
            this.smartPanel1.Controls.Add(this.cboToProcess);
            this.smartPanel1.Controls.Add(this.cboFromProcess);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(916, 29);
            this.smartPanel1.TabIndex = 1;
            // 
            // smartLabel1
            // 
            this.smartLabel1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.smartLabel1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.smartLabel1.Appearance.Options.UseBackColor = true;
            this.smartLabel1.Appearance.Options.UseForeColor = true;
            this.smartLabel1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.smartLabel1.LanguageKey = "TRANSITRESOURCE";
            this.smartLabel1.Location = new System.Drawing.Point(470, 4);
            this.smartLabel1.Margin = new System.Windows.Forms.Padding(5, 2, 5, 0);
            this.smartLabel1.Name = "smartLabel1";
            this.smartLabel1.Size = new System.Drawing.Size(61, 20);
            this.smartLabel1.TabIndex = 2;
            this.smartLabel1.Text = "인계자원";
            // 
            // lblToProcess
            // 
            this.lblToProcess.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lblToProcess.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblToProcess.Appearance.Options.UseBackColor = true;
            this.lblToProcess.Appearance.Options.UseForeColor = true;
            this.lblToProcess.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblToProcess.LanguageKey = "TOPROCESSSEGMENT";
            this.lblToProcess.Location = new System.Drawing.Point(232, 4);
            this.lblToProcess.Margin = new System.Windows.Forms.Padding(5, 2, 5, 0);
            this.lblToProcess.Name = "lblToProcess";
            this.lblToProcess.Size = new System.Drawing.Size(60, 20);
            this.lblToProcess.TabIndex = 2;
            this.lblToProcess.Text = "To 공정";
            // 
            // cboResource
            // 
            this.cboResource.LabelText = null;
            this.cboResource.LanguageKey = null;
            this.cboResource.Location = new System.Drawing.Point(536, 4);
            this.cboResource.Margin = new System.Windows.Forms.Padding(0, 2, 15, 0);
            this.cboResource.Name = "cboResource";
            this.cboResource.PopupWidth = 0;
            this.cboResource.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboResource.Properties.NullText = "";
            this.cboResource.ShowHeader = true;
            this.cboResource.Size = new System.Drawing.Size(156, 20);
            this.cboResource.TabIndex = 3;
            this.cboResource.VisibleColumns = null;
            this.cboResource.VisibleColumnsWidth = null;
            // 
            // lblFromProcess
            // 
            this.lblFromProcess.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblFromProcess.Appearance.Options.UseForeColor = true;
            this.lblFromProcess.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblFromProcess.LanguageKey = "FROMPROCESSSEGMENT";
            this.lblFromProcess.Location = new System.Drawing.Point(7, 4);
            this.lblFromProcess.Margin = new System.Windows.Forms.Padding(5, 2, 5, 0);
            this.lblFromProcess.Name = "lblFromProcess";
            this.lblFromProcess.Size = new System.Drawing.Size(69, 20);
            this.lblFromProcess.TabIndex = 2;
            this.lblFromProcess.Text = "From 공정";
            // 
            // cboToProcess
            // 
            this.cboToProcess.LabelText = null;
            this.cboToProcess.LanguageKey = null;
            this.cboToProcess.Location = new System.Drawing.Point(297, 4);
            this.cboToProcess.Margin = new System.Windows.Forms.Padding(0, 2, 15, 0);
            this.cboToProcess.Name = "cboToProcess";
            this.cboToProcess.PopupWidth = 0;
            this.cboToProcess.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboToProcess.Properties.NullText = "";
            this.cboToProcess.ShowHeader = true;
            this.cboToProcess.Size = new System.Drawing.Size(156, 20);
            this.cboToProcess.TabIndex = 3;
            this.cboToProcess.VisibleColumns = null;
            this.cboToProcess.VisibleColumnsWidth = null;
            // 
            // cboFromProcess
            // 
            this.cboFromProcess.LabelText = null;
            this.cboFromProcess.LanguageKey = null;
            this.cboFromProcess.Location = new System.Drawing.Point(81, 4);
            this.cboFromProcess.Margin = new System.Windows.Forms.Padding(0, 2, 15, 0);
            this.cboFromProcess.Name = "cboFromProcess";
            this.cboFromProcess.PopupWidth = 0;
            this.cboFromProcess.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboFromProcess.Properties.NullText = "";
            this.cboFromProcess.ShowHeader = true;
            this.cboFromProcess.Size = new System.Drawing.Size(144, 20);
            this.cboFromProcess.TabIndex = 3;
            this.cboFromProcess.VisibleColumns = null;
            this.cboFromProcess.VisibleColumnsWidth = null;
            // 
            // smartLabel2
            // 
            this.smartLabel2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.smartLabel2.Appearance.ForeColor = System.Drawing.Color.Red;
            this.smartLabel2.Appearance.Options.UseBackColor = true;
            this.smartLabel2.Appearance.Options.UseForeColor = true;
            this.smartLabel2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.smartLabel2.LanguageKey = "TARGETCOMPANY";
            this.smartLabel2.Location = new System.Drawing.Point(698, 4);
            this.smartLabel2.Margin = new System.Windows.Forms.Padding(5, 2, 5, 0);
            this.smartLabel2.Name = "smartLabel2";
            this.smartLabel2.Size = new System.Drawing.Size(69, 20);
            this.smartLabel2.TabIndex = 4;
            this.smartLabel2.Text = "대상법인";
            // 
            // cboTargetCompany
            // 
            this.cboTargetCompany.LabelText = null;
            this.cboTargetCompany.LanguageKey = null;
            this.cboTargetCompany.Location = new System.Drawing.Point(772, 3);
            this.cboTargetCompany.Margin = new System.Windows.Forms.Padding(0, 2, 15, 0);
            this.cboTargetCompany.Name = "cboTargetCompany";
            this.cboTargetCompany.PopupWidth = 0;
            this.cboTargetCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTargetCompany.Properties.NullText = "";
            this.cboTargetCompany.ShowHeader = true;
            this.cboTargetCompany.Size = new System.Drawing.Size(156, 20);
            this.cboTargetCompany.TabIndex = 5;
            this.cboTargetCompany.VisibleColumns = null;
            this.cboTargetCompany.VisibleColumnsWidth = null;
            // 
            // ProcessResultCreate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1241, 450);
            this.Name = "ProcessResultCreate";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboResource.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboToProcess.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFromProcess.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTargetCompany.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
		private Framework.SmartControls.SmartBandedGrid grdLotList;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartLabel lblToProcess;
        private Framework.SmartControls.SmartLabel lblFromProcess;
        private Framework.SmartControls.SmartComboBox cboToProcess;
        private Framework.SmartControls.SmartComboBox cboFromProcess;
        private Framework.SmartControls.SmartLabel smartLabel1;
        private Framework.SmartControls.SmartComboBox cboResource;
        private Framework.SmartControls.SmartLabel smartLabel2;
        private Framework.SmartControls.SmartComboBox cboTargetCompany;
    }
}
