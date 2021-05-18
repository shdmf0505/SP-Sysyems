namespace Micube.SmartMES.QualityAnalysis
{
    partial class DefectRoutingPathPopup
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.gbxProcessPath = new Micube.Framework.SmartControls.SmartGroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.gbxProductRouting = new Micube.Framework.SmartControls.SmartGroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.grdProductRouting = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.txtProductResource = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.gbxReworkRouting = new Micube.Framework.SmartControls.SmartGroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.txtReworkResource = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.grdReworkRouting = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.txtReworkRoutingId = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtReworkRoutingName = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.txtRepairLotNo = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtReasonCancel = new Micube.Framework.SmartControls.SmartLabelTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbxProcessPath)).BeginInit();
            this.gbxProcessPath.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbxProductRouting)).BeginInit();
            this.gbxProductRouting.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductResource.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbxReworkRouting)).BeginInit();
            this.gbxReworkRouting.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtReworkResource.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReworkRoutingId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReworkRoutingName.Properties)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRepairLotNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReasonCancel.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(1064, 474);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.gbxProcessPath, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1064, 474);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this.btnClose);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 444);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1061, 30);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.Location = new System.Drawing.Point(981, 3);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(80, 25);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "닫기";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // gbxProcessPath
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.gbxProcessPath, 2);
            this.gbxProcessPath.Controls.Add(this.tableLayoutPanel3);
            this.gbxProcessPath.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.gbxProcessPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxProcessPath.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.gbxProcessPath.LanguageKey = "PROCESSSEQUENCE";
            this.gbxProcessPath.Location = new System.Drawing.Point(3, 3);
            this.gbxProcessPath.Name = "gbxProcessPath";
            this.gbxProcessPath.ShowBorder = true;
            this.gbxProcessPath.Size = new System.Drawing.Size(1058, 438);
            this.gbxProcessPath.TabIndex = 2;
            this.gbxProcessPath.Text = "공정순서";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.smartSpliterContainer1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel2, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(2, 31);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1054, 405);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 30);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.gbxProductRouting);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.gbxReworkRouting);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(1054, 375);
            this.smartSpliterContainer1.SplitterPosition = 515;
            this.smartSpliterContainer1.TabIndex = 1;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // gbxProductRouting
            // 
            this.gbxProductRouting.Controls.Add(this.tableLayoutPanel2);
            this.gbxProductRouting.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.gbxProductRouting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxProductRouting.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.gbxProductRouting.LanguageKey = "PRODUCTROUTING";
            this.gbxProductRouting.Location = new System.Drawing.Point(0, 0);
            this.gbxProductRouting.Name = "gbxProductRouting";
            this.gbxProductRouting.ShowBorder = true;
            this.gbxProductRouting.Size = new System.Drawing.Size(515, 375);
            this.gbxProductRouting.TabIndex = 1;
            this.gbxProductRouting.Text = "품목 라우팅";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.grdProductRouting, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtProductResource, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 31);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(511, 342);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // grdProductRouting
            // 
            this.grdProductRouting.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.tableLayoutPanel2.SetColumnSpan(this.grdProductRouting, 2);
            this.grdProductRouting.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.None;
            this.grdProductRouting.IsUsePaging = false;
            this.grdProductRouting.LanguageKey = null;
            this.grdProductRouting.Location = new System.Drawing.Point(0, 27);
            this.grdProductRouting.Margin = new System.Windows.Forms.Padding(0);
            this.grdProductRouting.Name = "grdProductRouting";
            this.grdProductRouting.ShowBorder = false;
            this.grdProductRouting.ShowButtonBar = false;
            this.grdProductRouting.ShowStatusBar = false;
            this.grdProductRouting.Size = new System.Drawing.Size(511, 315);
            this.grdProductRouting.TabIndex = 3;
            // 
            // txtProductResource
            // 
            this.txtProductResource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductResource.LabelText = "대상 자원";
            this.txtProductResource.LanguageKey = "RESOURCENAME";
            this.txtProductResource.Location = new System.Drawing.Point(3, 3);
            this.txtProductResource.Name = "txtProductResource";
            this.txtProductResource.Properties.ReadOnly = true;
            this.txtProductResource.Size = new System.Drawing.Size(249, 20);
            this.txtProductResource.TabIndex = 4;
            // 
            // gbxReworkRouting
            // 
            this.gbxReworkRouting.Controls.Add(this.tableLayoutPanel4);
            this.gbxReworkRouting.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.gbxReworkRouting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxReworkRouting.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.gbxReworkRouting.LanguageKey = "REWORKROUTING";
            this.gbxReworkRouting.Location = new System.Drawing.Point(0, 0);
            this.gbxReworkRouting.Name = "gbxReworkRouting";
            this.gbxReworkRouting.ShowBorder = true;
            this.gbxReworkRouting.Size = new System.Drawing.Size(534, 375);
            this.gbxReworkRouting.TabIndex = 2;
            this.gbxReworkRouting.Text = "재작업 라우팅";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.txtReworkResource, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.grdReworkRouting, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.txtReworkRoutingId, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.txtReworkRoutingName, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(2, 31);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(530, 342);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // txtReworkResource
            // 
            this.txtReworkResource.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtReworkResource.LabelText = "대상 자원";
            this.txtReworkResource.LanguageKey = "RESOURCENAME";
            this.txtReworkResource.Location = new System.Drawing.Point(3, 30);
            this.txtReworkResource.Name = "txtReworkResource";
            this.txtReworkResource.Properties.ReadOnly = true;
            this.txtReworkResource.Size = new System.Drawing.Size(259, 20);
            this.txtReworkResource.TabIndex = 4;
            // 
            // grdReworkRouting
            // 
            this.grdReworkRouting.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.tableLayoutPanel4.SetColumnSpan(this.grdReworkRouting, 2);
            this.grdReworkRouting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReworkRouting.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.None;
            this.grdReworkRouting.IsUsePaging = false;
            this.grdReworkRouting.LanguageKey = null;
            this.grdReworkRouting.Location = new System.Drawing.Point(0, 54);
            this.grdReworkRouting.Margin = new System.Windows.Forms.Padding(0);
            this.grdReworkRouting.Name = "grdReworkRouting";
            this.grdReworkRouting.ShowBorder = false;
            this.grdReworkRouting.ShowButtonBar = false;
            this.grdReworkRouting.ShowStatusBar = false;
            this.grdReworkRouting.Size = new System.Drawing.Size(530, 288);
            this.grdReworkRouting.TabIndex = 3;
            // 
            // txtReworkRoutingId
            // 
            this.txtReworkRoutingId.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtReworkRoutingId.LabelText = "재작업 라우팅 ID";
            this.txtReworkRoutingId.LanguageKey = "REWORKROUTINGID";
            this.txtReworkRoutingId.Location = new System.Drawing.Point(3, 3);
            this.txtReworkRoutingId.Name = "txtReworkRoutingId";
            this.txtReworkRoutingId.Properties.ReadOnly = true;
            this.txtReworkRoutingId.Size = new System.Drawing.Size(259, 20);
            this.txtReworkRoutingId.TabIndex = 1;
            // 
            // txtReworkRoutingName
            // 
            this.txtReworkRoutingName.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtReworkRoutingName.LabelText = "재작업 라우팅명";
            this.txtReworkRoutingName.LabelWidth = "30%";
            this.txtReworkRoutingName.LanguageKey = "REWORKROUTINGNAME";
            this.txtReworkRoutingName.Location = new System.Drawing.Point(268, 3);
            this.txtReworkRoutingName.Name = "txtReworkRoutingName";
            this.txtReworkRoutingName.Properties.ReadOnly = true;
            this.txtReworkRoutingName.Size = new System.Drawing.Size(259, 20);
            this.txtReworkRoutingName.TabIndex = 2;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.txtRepairLotNo);
            this.flowLayoutPanel2.Controls.Add(this.txtReasonCancel);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(1054, 30);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // txtRepairLotNo
            // 
            this.txtRepairLotNo.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtRepairLotNo.LabelText = "Repair Lot No";
            this.txtRepairLotNo.LanguageKey = "Repair Lot No";
            this.txtRepairLotNo.Location = new System.Drawing.Point(3, 3);
            this.txtRepairLotNo.Name = "txtRepairLotNo";
            this.txtRepairLotNo.Properties.ReadOnly = true;
            this.txtRepairLotNo.Size = new System.Drawing.Size(281, 20);
            this.txtRepairLotNo.TabIndex = 0;
            // 
            // txtReasonCancel
            // 
            this.txtReasonCancel.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtReasonCancel.LabelText = "취소사유";
            this.txtReasonCancel.LanguageKey = "REASONCANCEL";
            this.txtReasonCancel.Location = new System.Drawing.Point(290, 3);
            this.txtReasonCancel.Name = "txtReasonCancel";
            this.txtReasonCancel.Properties.ReadOnly = true;
            this.txtReasonCancel.Size = new System.Drawing.Size(220, 20);
            this.txtReasonCancel.TabIndex = 1;
            // 
            // DefectRoutingPathPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 494);
            this.Name = "DefectRoutingPathPopup";
            this.Text = "공정순서";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbxProcessPath)).EndInit();
            this.gbxProcessPath.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbxProductRouting)).EndInit();
            this.gbxProductRouting.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtProductResource.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbxReworkRouting)).EndInit();
            this.gbxReworkRouting.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtReworkResource.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReworkRoutingId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReworkRoutingName.Properties)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtRepairLotNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReasonCancel.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartGroupBox gbxProcessPath;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Framework.SmartControls.SmartBandedGrid grdProductRouting;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private Framework.SmartControls.SmartBandedGrid grdReworkRouting;
        private Framework.SmartControls.SmartLabelTextBox txtReworkRoutingId;
        private Framework.SmartControls.SmartLabelTextBox txtReworkRoutingName;
        private Framework.SmartControls.SmartLabelTextBox txtReworkResource;
        private Framework.SmartControls.SmartGroupBox gbxProductRouting;
        private Framework.SmartControls.SmartGroupBox gbxReworkRouting;
        private Framework.SmartControls.SmartLabelTextBox txtProductResource;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Framework.SmartControls.SmartLabelTextBox txtRepairLotNo;
        private Framework.SmartControls.SmartLabelTextBox txtReasonCancel;
    }
}