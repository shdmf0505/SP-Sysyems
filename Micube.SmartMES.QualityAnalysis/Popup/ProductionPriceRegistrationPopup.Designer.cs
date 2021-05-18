namespace Micube.SmartMES.QualityAnalysis
{
    partial class ProductionPriceRegistrationPopup
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
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            this.gbxProductionPriceRegistartion = new Micube.Framework.SmartControls.SmartGroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtStandardMonth = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.btnProductionPriceApply = new Micube.Framework.SmartControls.SmartButton();
            this.spinProductionPrice = new Micube.Framework.SmartControls.SmartLabelSpinEdit();
            this.spinSalesPrice = new Micube.Framework.SmartControls.SmartLabelSpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbxProductionPriceRegistartion)).BeginInit();
            this.gbxProductionPriceRegistartion.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStandardMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinProductionPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSalesPrice.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(331, 155);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.gbxProductionPriceRegistartion, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(331, 155);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this.btnClose);
            this.flowLayoutPanel1.Controls.Add(this.btnSave);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 130);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(331, 25);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnClose.IsBusy = false;
            this.btnClose.Location = new System.Drawing.Point(248, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(80, 25);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "닫기";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.IsBusy = false;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(162, 0);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "저장";
            this.btnSave.TooltipLanguageKey = "";
            // 
            // gbxProductionPriceRegistartion
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.gbxProductionPriceRegistartion, 2);
            this.gbxProductionPriceRegistartion.Controls.Add(this.tableLayoutPanel2);
            this.gbxProductionPriceRegistartion.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.gbxProductionPriceRegistartion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxProductionPriceRegistartion.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.gbxProductionPriceRegistartion.Location = new System.Drawing.Point(3, 3);
            this.gbxProductionPriceRegistartion.Name = "gbxProductionPriceRegistartion";
            this.gbxProductionPriceRegistartion.ShowBorder = true;
            this.gbxProductionPriceRegistartion.Size = new System.Drawing.Size(325, 124);
            this.gbxProductionPriceRegistartion.TabIndex = 2;
            this.gbxProductionPriceRegistartion.Text = "생산입고 매출금액 등록";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.84735F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.15265F));
            this.tableLayoutPanel2.Controls.Add(this.txtStandardMonth, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnProductionPriceApply, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.spinProductionPrice, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.spinSalesPrice, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 31);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(321, 91);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // txtStandardMonth
            // 
            this.txtStandardMonth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStandardMonth.LabelText = "기준월";
            this.txtStandardMonth.LanguageKey = "STANDARDYM";
            this.txtStandardMonth.Location = new System.Drawing.Point(3, 3);
            this.txtStandardMonth.Name = "txtStandardMonth";
            this.txtStandardMonth.Properties.ReadOnly = true;
            this.txtStandardMonth.Size = new System.Drawing.Size(214, 20);
            this.txtStandardMonth.TabIndex = 3;
            // 
            // btnProductionPriceApply
            // 
            this.btnProductionPriceApply.AllowFocus = false;
            this.btnProductionPriceApply.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnProductionPriceApply.IsBusy = false;
            this.btnProductionPriceApply.LanguageKey = "PRODUCTIONPRICEAPPLY";
            this.btnProductionPriceApply.Location = new System.Drawing.Point(223, 30);
            this.btnProductionPriceApply.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnProductionPriceApply.Name = "btnProductionPriceApply";
            this.btnProductionPriceApply.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnProductionPriceApply.Size = new System.Drawing.Size(95, 25);
            this.btnProductionPriceApply.TabIndex = 4;
            this.btnProductionPriceApply.Text = "생산금액적용";
            this.btnProductionPriceApply.TooltipLanguageKey = "";
            // 
            // spinProductionPrice
            // 
            this.spinProductionPrice.Dock = System.Windows.Forms.DockStyle.Top;
            this.spinProductionPrice.LabelText = "생산입고금액";
            this.spinProductionPrice.LanguageKey = "PRODUCTIONPRICE";
            this.spinProductionPrice.Location = new System.Drawing.Point(3, 33);
            this.spinProductionPrice.Name = "spinProductionPrice";
            this.spinProductionPrice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinProductionPrice.Properties.DisplayFormat.FormatString = "#,###,###,###,###,##0";
            this.spinProductionPrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinProductionPrice.Properties.Mask.EditMask = "#,###,###,###,###,##0";
            this.spinProductionPrice.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.spinProductionPrice.Size = new System.Drawing.Size(214, 20);
            this.spinProductionPrice.TabIndex = 5;
            // 
            // spinSalesPrice
            // 
            this.spinSalesPrice.Dock = System.Windows.Forms.DockStyle.Top;
            this.spinSalesPrice.LabelText = "매출금액";
            this.spinSalesPrice.LanguageKey = "SALESAMOUNT";
            this.spinSalesPrice.Location = new System.Drawing.Point(3, 63);
            this.spinSalesPrice.Name = "spinSalesPrice";
            this.spinSalesPrice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinSalesPrice.Properties.DisplayFormat.FormatString = "#,###,###,###,###,##0";
            this.spinSalesPrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinSalesPrice.Properties.Mask.EditMask = "#,###,###,###,###,##0";
            this.spinSalesPrice.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.spinSalesPrice.Size = new System.Drawing.Size(214, 20);
            this.spinSalesPrice.TabIndex = 6;
            // 
            // ProductionPriceRegistrationPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 175);
            this.Name = "ProductionPriceRegistrationPopup";
            this.Text = "생산입고 매출금액 등록";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbxProductionPriceRegistartion)).EndInit();
            this.gbxProductionPriceRegistartion.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtStandardMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinProductionPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSalesPrice.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartGroupBox gbxProductionPriceRegistartion;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Framework.SmartControls.SmartLabelTextBox txtStandardMonth;
        private Framework.SmartControls.SmartButton btnProductionPriceApply;
        private Framework.SmartControls.SmartLabelSpinEdit spinProductionPrice;
        private Framework.SmartControls.SmartLabelSpinEdit spinSalesPrice;
    }
}