namespace Micube.SmartMES.OutsideOrderMgnt
{
    partial class OutsourcingExclusionProcessing
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
            this.tapIsExcept = new Micube.Framework.SmartControls.SmartTabControl();
            this.tapOspActual = new DevExpress.XtraTab.XtraTabPage();
            this.grdOspActual = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tapOspEtcWork = new DevExpress.XtraTab.XtraTabPage();
            this.grdOspEtcWork = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tapOspEtcAmount = new DevExpress.XtraTab.XtraTabPage();
            this.grdOspEtcAmount = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.btnExclusion = new Micube.Framework.SmartControls.SmartButton();
            this.btnExclusionCancel = new Micube.Framework.SmartControls.SmartButton();
            this.txtperiod = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblPeriod = new Micube.Framework.SmartControls.SmartLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tapIsExcept)).BeginInit();
            this.tapIsExcept.SuspendLayout();
            this.tapOspActual.SuspendLayout();
            this.tapOspEtcWork.SuspendLayout();
            this.tapOspEtcAmount.SuspendLayout();
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
            this.pnlContent.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlContent.Size = new System.Drawing.Size(843, 903);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1224, 939);
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.tapIsExcept, 0, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartPanel1, 0, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 2;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(843, 903);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // tapIsExcept
            // 
            this.tapIsExcept.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tapIsExcept.Location = new System.Drawing.Point(3, 43);
            this.tapIsExcept.Name = "tapIsExcept";
            this.tapIsExcept.SelectedTabPage = this.tapOspActual;
            this.tapIsExcept.Size = new System.Drawing.Size(837, 857);
            this.tapIsExcept.TabIndex = 0;
            this.tapIsExcept.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tapOspActual,
            this.tapOspEtcWork,
            this.tapOspEtcAmount});
            // 
            // tapOspActual
            // 
            this.tapOspActual.Controls.Add(this.grdOspActual);
            this.tapIsExcept.SetLanguageKey(this.tapOspActual, "OSPACTUALISEXCEPT");
            this.tapOspActual.Name = "tapOspActual";
            this.tapOspActual.Size = new System.Drawing.Size(830, 821);
            this.tapOspActual.Text = "OSPACTUALISEXCEPT";
            // 
            // grdOspActual
            // 
            this.grdOspActual.Caption = "";
            this.grdOspActual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOspActual.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdOspActual.IsUsePaging = false;
            this.grdOspActual.LanguageKey = "OUTSOURCINGEXCLUSIONOSPACTUAL";
            this.grdOspActual.Location = new System.Drawing.Point(0, 0);
            this.grdOspActual.Margin = new System.Windows.Forms.Padding(0);
            this.grdOspActual.Name = "grdOspActual";
            this.grdOspActual.ShowBorder = true;
            this.grdOspActual.ShowStatusBar = false;
            this.grdOspActual.Size = new System.Drawing.Size(830, 821);
            this.grdOspActual.TabIndex = 8;
            // 
            // tapOspEtcWork
            // 
            this.tapOspEtcWork.Controls.Add(this.grdOspEtcWork);
            this.tapIsExcept.SetLanguageKey(this.tapOspEtcWork, "OSPETCWORKISEXCEPT");
            this.tapOspEtcWork.Name = "tapOspEtcWork";
            this.tapOspEtcWork.Size = new System.Drawing.Size(818, 546);
            this.tapOspEtcWork.Text = "OSPETCWORKISEXCEPT";
            // 
            // grdOspEtcWork
            // 
            this.grdOspEtcWork.Caption = "";
            this.grdOspEtcWork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOspEtcWork.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdOspEtcWork.IsUsePaging = false;
            this.grdOspEtcWork.LanguageKey = "OUTSOURCINGEXCLUSIONOSPETCWORK";
            this.grdOspEtcWork.Location = new System.Drawing.Point(0, 0);
            this.grdOspEtcWork.Margin = new System.Windows.Forms.Padding(0);
            this.grdOspEtcWork.Name = "grdOspEtcWork";
            this.grdOspEtcWork.ShowBorder = true;
            this.grdOspEtcWork.ShowStatusBar = false;
            this.grdOspEtcWork.Size = new System.Drawing.Size(818, 546);
            this.grdOspEtcWork.TabIndex = 8;
            // 
            // tapOspEtcAmount
            // 
            this.tapOspEtcAmount.Controls.Add(this.grdOspEtcAmount);
            this.tapIsExcept.SetLanguageKey(this.tapOspEtcAmount, "OSPETCAMOUNTISEXCEPT");
            this.tapOspEtcAmount.Name = "tapOspEtcAmount";
            this.tapOspEtcAmount.Size = new System.Drawing.Size(818, 546);
            this.tapOspEtcAmount.Text = "OSPETCAMOUNTISEXCEPT";
            // 
            // grdOspEtcAmount
            // 
            this.grdOspEtcAmount.Caption = "";
            this.grdOspEtcAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOspEtcAmount.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdOspEtcAmount.IsUsePaging = false;
            this.grdOspEtcAmount.LanguageKey = "OUTSOURCINGEXCLUSIONOSPETCAMOUNT";
            this.grdOspEtcAmount.Location = new System.Drawing.Point(0, 0);
            this.grdOspEtcAmount.Margin = new System.Windows.Forms.Padding(0);
            this.grdOspEtcAmount.Name = "grdOspEtcAmount";
            this.grdOspEtcAmount.ShowBorder = true;
            this.grdOspEtcAmount.ShowStatusBar = false;
            this.grdOspEtcAmount.Size = new System.Drawing.Size(818, 546);
            this.grdOspEtcAmount.TabIndex = 8;
            // 
            // smartPanel1
            // 
            this.smartPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel1.Controls.Add(this.btnExclusion);
            this.smartPanel1.Controls.Add(this.btnExclusionCancel);
            this.smartPanel1.Controls.Add(this.txtperiod);
            this.smartPanel1.Controls.Add(this.lblPeriod);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(843, 40);
            this.smartPanel1.TabIndex = 1;
            // 
            // btnExclusion
            // 
            this.btnExclusion.AllowFocus = false;
            this.btnExclusion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExclusion.IsBusy = false;
            this.btnExclusion.IsWrite = true;
            this.btnExclusion.LanguageKey = "OSPEXCLUSION";
            this.btnExclusion.Location = new System.Drawing.Point(664, 6);
            this.btnExclusion.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnExclusion.Name = "btnExclusion";
            this.btnExclusion.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnExclusion.Size = new System.Drawing.Size(80, 25);
            this.btnExclusion.TabIndex = 11;
            this.btnExclusion.Text = "제외처리";
            this.btnExclusion.TooltipLanguageKey = "";
            this.btnExclusion.Visible = false;
            // 
            // btnExclusionCancel
            // 
            this.btnExclusionCancel.AllowFocus = false;
            this.btnExclusionCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExclusionCancel.IsBusy = false;
            this.btnExclusionCancel.IsWrite = true;
            this.btnExclusionCancel.LanguageKey = "OSPEXCLUSIONCANCEL";
            this.btnExclusionCancel.Location = new System.Drawing.Point(754, 6);
            this.btnExclusionCancel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnExclusionCancel.Name = "btnExclusionCancel";
            this.btnExclusionCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnExclusionCancel.Size = new System.Drawing.Size(80, 25);
            this.btnExclusionCancel.TabIndex = 12;
            this.btnExclusionCancel.Text = "제외취소";
            this.btnExclusionCancel.TooltipLanguageKey = "";
            this.btnExclusionCancel.Visible = false;
            // 
            // txtperiod
            // 
            this.txtperiod.LabelText = null;
            this.txtperiod.LanguageKey = null;
            this.txtperiod.Location = new System.Drawing.Point(108, 7);
            this.txtperiod.Name = "txtperiod";
            this.txtperiod.Size = new System.Drawing.Size(226, 24);
            this.txtperiod.TabIndex = 10;
            // 
            // lblPeriod
            // 
            this.lblPeriod.LanguageKey = "PERIODDESCRIPTION";
            this.lblPeriod.Location = new System.Drawing.Point(8, 9);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(79, 18);
            this.lblPeriod.TabIndex = 8;
            this.lblPeriod.Text = "smartLabel1";
            // 
            // OutsourcingExclusionProcessing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 977);
            this.Name = "OutsourcingExclusionProcessing";
            this.Padding = new System.Windows.Forms.Padding(19, 19, 19, 19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tapIsExcept)).EndInit();
            this.tapIsExcept.ResumeLayout(false);
            this.tapOspActual.ResumeLayout(false);
            this.tapOspEtcWork.ResumeLayout(false);
            this.tapOspEtcAmount.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.smartPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtperiod.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartTabControl tapIsExcept;
        private DevExpress.XtraTab.XtraTabPage tapOspActual;
        private DevExpress.XtraTab.XtraTabPage tapOspEtcWork;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartTextBox txtperiod;
        private Framework.SmartControls.SmartLabel lblPeriod;
        private DevExpress.XtraTab.XtraTabPage tapOspEtcAmount;
        private Framework.SmartControls.SmartButton btnExclusion;
        private Framework.SmartControls.SmartButton btnExclusionCancel;
        private Framework.SmartControls.SmartBandedGrid grdOspActual;
        private Framework.SmartControls.SmartBandedGrid grdOspEtcWork;
        private Framework.SmartControls.SmartBandedGrid grdOspEtcAmount;
    }
}