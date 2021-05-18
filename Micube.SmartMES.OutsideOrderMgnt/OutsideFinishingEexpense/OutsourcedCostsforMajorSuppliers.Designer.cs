namespace Micube.SmartMES.OutsideOrderMgnt
{
    partial class OutsourcedCostsforMajorSuppliers
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
            this.tplMain = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdClaimConfirm = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.txtperiod = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblPeriod = new Micube.Framework.SmartControls.SmartLabel();
            this.btnSettlemantProcess = new Micube.Framework.SmartControls.SmartButton();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            this.btnSettlemantList = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.tplMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtperiod.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Size = new System.Drawing.Size(371, 900);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(843, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tplMain);
            this.pnlContent.Size = new System.Drawing.Size(843, 903);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1224, 939);
            // 
            // tplMain
            // 
            this.tplMain.ColumnCount = 1;
            this.tplMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplMain.Controls.Add(this.grdClaimConfirm, 0, 1);
            this.tplMain.Controls.Add(this.smartPanel1, 0, 0);
            this.tplMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplMain.Location = new System.Drawing.Point(0, 0);
            this.tplMain.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tplMain.Name = "tplMain";
            this.tplMain.RowCount = 2;
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tplMain.Size = new System.Drawing.Size(843, 903);
            this.tplMain.TabIndex = 0;
            // 
            // grdClaimConfirm
            // 
            this.grdClaimConfirm.Caption = "";
            this.grdClaimConfirm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdClaimConfirm.IsUsePaging = false;
            this.grdClaimConfirm.LanguageKey = "OUTSOURCEDCOSTSFORMAJORSUPPLIERSLIST";
            this.grdClaimConfirm.Location = new System.Drawing.Point(0, 40);
            this.grdClaimConfirm.Margin = new System.Windows.Forms.Padding(0);
            this.grdClaimConfirm.Name = "grdClaimConfirm";
            this.grdClaimConfirm.ShowBorder = true;
            this.grdClaimConfirm.ShowStatusBar = false;
            this.grdClaimConfirm.Size = new System.Drawing.Size(843, 863);
            this.grdClaimConfirm.TabIndex = 6;
            // 
            // smartPanel1
            // 
            this.smartPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel1.Controls.Add(this.txtperiod);
            this.smartPanel1.Controls.Add(this.lblPeriod);
            this.smartPanel1.Controls.Add(this.btnSettlemantProcess);
            this.smartPanel1.Controls.Add(this.btnSave);
            this.smartPanel1.Controls.Add(this.btnSettlemantList);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(843, 40);
            this.smartPanel1.TabIndex = 7;
            // 
            // txtperiod
            // 
            this.txtperiod.EditValue = "";
            this.txtperiod.LabelText = null;
            this.txtperiod.LanguageKey = null;
            this.txtperiod.Location = new System.Drawing.Point(133, 7);
            this.txtperiod.Name = "txtperiod";
            this.txtperiod.Properties.ReadOnly = true;
            this.txtperiod.Size = new System.Drawing.Size(226, 24);
            this.txtperiod.TabIndex = 10;
            // 
            // lblPeriod
            // 
            this.lblPeriod.LanguageKey = "PERIODDESCRIPTION";
            this.lblPeriod.Location = new System.Drawing.Point(5, 11);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(26, 18);
            this.lblPeriod.TabIndex = 9;
            this.lblPeriod.Text = "기간";
            // 
            // btnSettlemantProcess
            // 
            this.btnSettlemantProcess.AllowFocus = false;
            this.btnSettlemantProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettlemantProcess.IsBusy = false;
            this.btnSettlemantProcess.LanguageKey = "SETTLEMENTPROCESS";
            this.btnSettlemantProcess.Location = new System.Drawing.Point(664, 9);
            this.btnSettlemantProcess.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSettlemantProcess.Name = "btnSettlemantProcess";
            this.btnSettlemantProcess.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSettlemantProcess.Size = new System.Drawing.Size(80, 25);
            this.btnSettlemantProcess.TabIndex = 3;
            this.btnSettlemantProcess.Text = "정산처리";
            this.btnSettlemantProcess.TooltipLanguageKey = "";
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.IsBusy = false;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(578, 9);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "저장";
            this.btnSave.TooltipLanguageKey = "";
            // 
            // btnSettlemantList
            // 
            this.btnSettlemantList.AllowFocus = false;
            this.btnSettlemantList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettlemantList.IsBusy = false;
            this.btnSettlemantList.LanguageKey = "SETTLEMENTLIST";
            this.btnSettlemantList.Location = new System.Drawing.Point(756, 9);
            this.btnSettlemantList.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSettlemantList.Name = "btnSettlemantList";
            this.btnSettlemantList.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSettlemantList.Size = new System.Drawing.Size(80, 25);
            this.btnSettlemantList.TabIndex = 4;
            this.btnSettlemantList.Text = "정산내역";
            this.btnSettlemantList.TooltipLanguageKey = "";
            // 
            // OutsourcedCostsforMajorSuppliers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 977);
            this.Name = "OutsourcedCostsforMajorSuppliers";
            this.Padding = new System.Windows.Forms.Padding(19, 19, 19, 19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.tplMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.smartPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtperiod.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel tplMain;
        private Framework.SmartControls.SmartBandedGrid grdClaimConfirm;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartButton btnSettlemantProcess;
        private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartButton btnSettlemantList;
        private Framework.SmartControls.SmartTextBox txtperiod;
        private Framework.SmartControls.SmartLabel lblPeriod;
    }
}