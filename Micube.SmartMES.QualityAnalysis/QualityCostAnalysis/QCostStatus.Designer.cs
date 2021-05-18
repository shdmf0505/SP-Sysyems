namespace Micube.SmartMES.QualityAnalysis
{
    partial class QCostStatus
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
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdQCostStatus = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPrice = new Micube.Framework.SmartControls.SmartButton();
            this.spinSalesPrice = new Micube.Framework.SmartControls.SmartLabelSpinEdit();
            this.spinProductionPrice = new Micube.Framework.SmartControls.SmartLabelSpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinSalesPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinProductionPrice.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 582);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(858, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSplitTableLayoutPanel2);
            this.pnlContent.Size = new System.Drawing.Size(858, 586);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1163, 615);
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.ColumnCount = 2;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.76112F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.23888F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdQCostStatus, 0, 1);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.flowLayoutPanel3, 1, 0);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 2;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(858, 586);
            this.smartSplitTableLayoutPanel2.TabIndex = 1;
            // 
            // grdQCostStatus
            // 
            this.grdQCostStatus.Caption = "Q-Cost 현황";
            this.smartSplitTableLayoutPanel2.SetColumnSpan(this.grdQCostStatus, 2);
            this.grdQCostStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdQCostStatus.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdQCostStatus.IsUsePaging = false;
            this.grdQCostStatus.LanguageKey = "QCOSTSTATUS";
            this.grdQCostStatus.Location = new System.Drawing.Point(0, 40);
            this.grdQCostStatus.Margin = new System.Windows.Forms.Padding(0);
            this.grdQCostStatus.Name = "grdQCostStatus";
            this.grdQCostStatus.ShowBorder = true;
            this.grdQCostStatus.Size = new System.Drawing.Size(858, 546);
            this.grdQCostStatus.TabIndex = 0;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.btnPrice);
            this.flowLayoutPanel3.Controls.Add(this.spinSalesPrice);
            this.flowLayoutPanel3.Controls.Add(this.spinProductionPrice);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(224, 3);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(631, 34);
            this.flowLayoutPanel3.TabIndex = 1;
            // 
            // btnPrice
            // 
            this.btnPrice.AllowFocus = false;
            this.btnPrice.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPrice.IsBusy = false;
            this.btnPrice.IsWrite = false;
            this.btnPrice.LanguageKey = "PRODUCTIONSALESPRICE";
            this.btnPrice.Location = new System.Drawing.Point(523, 0);
            this.btnPrice.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPrice.Name = "btnPrice";
            this.btnPrice.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPrice.Size = new System.Drawing.Size(105, 25);
            this.btnPrice.TabIndex = 0;
            this.btnPrice.Text = "생산/매출금액";
            this.btnPrice.TooltipLanguageKey = "";
            this.btnPrice.Visible = false;
            // 
            // spinSalesPrice
            // 
            this.spinSalesPrice.Dock = System.Windows.Forms.DockStyle.Top;
            this.spinSalesPrice.LabelText = "매출금액";
            this.spinSalesPrice.LabelWidth = "30%";
            this.spinSalesPrice.LanguageKey = "SALESAMOUNT";
            this.spinSalesPrice.Location = new System.Drawing.Point(267, 3);
            this.spinSalesPrice.Name = "spinSalesPrice";
            this.spinSalesPrice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinSalesPrice.Properties.DisplayFormat.FormatString = "#,###,###,###,###,##0";
            this.spinSalesPrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinSalesPrice.Properties.EditFormat.FormatString = "#,###,###,###,###,##0";
            this.spinSalesPrice.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinSalesPrice.Properties.Mask.EditMask = "#,###,###,###,###,##0";
            this.spinSalesPrice.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.spinSalesPrice.Properties.ReadOnly = true;
            this.spinSalesPrice.Size = new System.Drawing.Size(250, 20);
            this.spinSalesPrice.TabIndex = 1;
            // 
            // spinProductionPrice
            // 
            this.spinProductionPrice.Dock = System.Windows.Forms.DockStyle.Top;
            this.spinProductionPrice.LabelText = "생산입고금액";
            this.spinProductionPrice.LanguageKey = "PRODUCTIONPRICE";
            this.spinProductionPrice.Location = new System.Drawing.Point(11, 3);
            this.spinProductionPrice.Name = "spinProductionPrice";
            this.spinProductionPrice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinProductionPrice.Properties.DisplayFormat.FormatString = "#,###,###,###,###,##0";
            this.spinProductionPrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinProductionPrice.Properties.Mask.EditMask = "#,###,###,###,###,##0";
            this.spinProductionPrice.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.spinProductionPrice.Properties.ReadOnly = true;
            this.spinProductionPrice.Size = new System.Drawing.Size(250, 20);
            this.spinProductionPrice.TabIndex = 2;
            // 
            // QCostStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 635);
            this.Name = "QCostStatus";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spinSalesPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinProductionPrice.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
        private Framework.SmartControls.SmartBandedGrid grdQCostStatus;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private Framework.SmartControls.SmartButton btnPrice;
        private Framework.SmartControls.SmartLabelSpinEdit spinSalesPrice;
        private Framework.SmartControls.SmartLabelSpinEdit spinProductionPrice;
    }
}