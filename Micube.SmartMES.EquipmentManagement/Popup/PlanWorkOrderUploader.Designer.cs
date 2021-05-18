namespace Micube.SmartMES.EquipmentManagement.Popup
{
    partial class PlanWorkOrderUploader
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
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            this.btnValidate = new Micube.Framework.SmartControls.SmartButton();
            this.btnLoadExcel = new Micube.Framework.SmartControls.SmartButton();
            this.deEndDate = new Micube.Framework.SmartControls.SmartDateEdit();
            this.deStartDate = new Micube.Framework.SmartControls.SmartDateEdit();
            this.lblPlanDate = new Micube.Framework.SmartControls.SmartLabel();
            this.btnInitialize = new Micube.Framework.SmartControls.SmartButton();
            this.grdPlanList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.fileDiag = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.grdPlanList);
            this.pnlMain.Controls.Add(this.flowLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(1134, 602);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnClose);
            this.flowLayoutPanel1.Controls.Add(this.btnSave);
            this.flowLayoutPanel1.Controls.Add(this.btnValidate);
            this.flowLayoutPanel1.Controls.Add(this.btnLoadExcel);
            this.flowLayoutPanel1.Controls.Add(this.deEndDate);
            this.flowLayoutPanel1.Controls.Add(this.deStartDate);
            this.flowLayoutPanel1.Controls.Add(this.lblPlanDate);
            this.flowLayoutPanel1.Controls.Add(this.btnInitialize);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1134, 26);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(1030, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(101, 22);
            this.btnClose.TabIndex = 325;
            this.btnClose.Text = "닫기:";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(923, 0);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(101, 22);
            this.btnSave.TabIndex = 326;
            this.btnSave.Text = "저장:";
            this.btnSave.TooltipLanguageKey = "";
            // 
            // btnValidate
            // 
            this.btnValidate.AllowFocus = false;
            this.btnValidate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValidate.IsBusy = false;
            this.btnValidate.IsWrite = false;
            this.btnValidate.LanguageKey = "PMAUTOVALIDATE";
            this.btnValidate.Location = new System.Drawing.Point(816, 0);
            this.btnValidate.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnValidate.Size = new System.Drawing.Size(101, 22);
            this.btnValidate.TabIndex = 334;
            this.btnValidate.Text = "유효성검사:";
            this.btnValidate.TooltipLanguageKey = "";
            // 
            // btnLoadExcel
            // 
            this.btnLoadExcel.AllowFocus = false;
            this.btnLoadExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadExcel.IsBusy = false;
            this.btnLoadExcel.IsWrite = false;
            this.btnLoadExcel.LanguageKey = "UPLOADEXCEL";
            this.btnLoadExcel.Location = new System.Drawing.Point(709, 0);
            this.btnLoadExcel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnLoadExcel.Name = "btnLoadExcel";
            this.btnLoadExcel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnLoadExcel.Size = new System.Drawing.Size(101, 22);
            this.btnLoadExcel.TabIndex = 336;
            this.btnLoadExcel.Text = "엑셀불러오기:";
            this.btnLoadExcel.TooltipLanguageKey = "";
            // 
            // deEndDate
            // 
            this.deEndDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deEndDate.EditValue = null;
            this.deEndDate.LabelText = null;
            this.deEndDate.LanguageKey = null;
            this.deEndDate.Location = new System.Drawing.Point(559, 3);
            this.deEndDate.Name = "deEndDate";
            this.deEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEndDate.Size = new System.Drawing.Size(144, 20);
            this.deEndDate.TabIndex = 332;
            // 
            // deStartDate
            // 
            this.deStartDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deStartDate.EditValue = null;
            this.deStartDate.LabelText = null;
            this.deStartDate.LanguageKey = null;
            this.deStartDate.Location = new System.Drawing.Point(409, 3);
            this.deStartDate.Name = "deStartDate";
            this.deStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deStartDate.Size = new System.Drawing.Size(144, 20);
            this.deStartDate.TabIndex = 333;
            // 
            // lblPlanDate
            // 
            this.lblPlanDate.Appearance.Options.UseTextOptions = true;
            this.lblPlanDate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblPlanDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblPlanDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPlanDate.LanguageKey = "PLANDATE";
            this.lblPlanDate.Location = new System.Drawing.Point(259, 3);
            this.lblPlanDate.Name = "lblPlanDate";
            this.lblPlanDate.Size = new System.Drawing.Size(144, 16);
            this.lblPlanDate.TabIndex = 330;
            this.lblPlanDate.Text = "계획일:";
            // 
            // btnInitialize
            // 
            this.btnInitialize.AllowFocus = false;
            this.btnInitialize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInitialize.IsBusy = false;
            this.btnInitialize.IsWrite = false;
            this.btnInitialize.LanguageKey = "INITIALIZE";
            this.btnInitialize.Location = new System.Drawing.Point(152, 0);
            this.btnInitialize.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnInitialize.Name = "btnInitialize";
            this.btnInitialize.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnInitialize.Size = new System.Drawing.Size(101, 22);
            this.btnInitialize.TabIndex = 335;
            this.btnInitialize.Text = "유효성검사:";
            this.btnInitialize.TooltipLanguageKey = "";
            this.btnInitialize.Visible = false;
            // 
            // grdPlanList
            // 
            this.grdPlanList.Caption = "PMAUTOCREATELIST";
            this.grdPlanList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPlanList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdPlanList.IsUsePaging = false;
            this.grdPlanList.LanguageKey = "PMAUTOCREATELIST";
            this.grdPlanList.Location = new System.Drawing.Point(0, 26);
            this.grdPlanList.Margin = new System.Windows.Forms.Padding(0);
            this.grdPlanList.Name = "grdPlanList";
            this.grdPlanList.ShowBorder = true;
            this.grdPlanList.ShowStatusBar = false;
            this.grdPlanList.Size = new System.Drawing.Size(1134, 576);
            this.grdPlanList.TabIndex = 112;
            // 
            // fileDiag
            // 
            this.fileDiag.FileName = "openFileDialog1";
            // 
            // PlanWorkOrderUploader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1154, 622);
            this.LanguageKey = "PMAUTOCREATE";
            this.Name = "PlanWorkOrderUploader";
            this.Text = "PMAUTOCREATE";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartDate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Framework.SmartControls.SmartBandedGrid grdPlanList;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartButton btnValidate;
        private Framework.SmartControls.SmartDateEdit deEndDate;
        private Framework.SmartControls.SmartDateEdit deStartDate;
        private Framework.SmartControls.SmartLabel lblPlanDate;
        private Framework.SmartControls.SmartButton btnInitialize;
        private Framework.SmartControls.SmartButton btnLoadExcel;
        private System.Windows.Forms.OpenFileDialog fileDiag;
    }
}