namespace Micube.SmartMES.OutsideOrderMgnt
{
    partial class OutsourcingCostsByDate
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
            this.tapCostDate = new Micube.Framework.SmartControls.SmartTabControl();
            this.tapOspDayAmount = new DevExpress.XtraTab.XtraTabPage();
            this.grdOspDateAmount = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tapOspDayQty = new DevExpress.XtraTab.XtraTabPage();
            this.grdOspDateQty = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tapCostDate)).BeginInit();
            this.tapCostDate.SuspendLayout();
            this.tapOspDayAmount.SuspendLayout();
            this.tapOspDayQty.SuspendLayout();
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
            this.smartSplitTableLayoutPanel1.Controls.Add(this.tapCostDate, 0, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 1;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 628F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(843, 903);
            this.smartSplitTableLayoutPanel1.TabIndex = 1;
            // 
            // tapCostDate
            // 
            this.tapCostDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tapCostDate.Location = new System.Drawing.Point(3, 3);
            this.tapCostDate.Name = "tapCostDate";
            this.tapCostDate.SelectedTabPage = this.tapOspDayQty;
            this.tapCostDate.Size = new System.Drawing.Size(837, 897);
            this.tapCostDate.TabIndex = 0;
            this.tapCostDate.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tapOspDayQty,
            this.tapOspDayAmount});
            // 
            // tapOspDayAmount
            // 
            this.tapOspDayAmount.Controls.Add(this.grdOspDateAmount);
            this.tapCostDate.SetLanguageKey(this.tapOspDayAmount, "OSPCOSTBYDATEAMOUNT");
            this.tapOspDayAmount.Name = "tapOspDayAmount";
            this.tapOspDayAmount.Size = new System.Drawing.Size(830, 861);
            this.tapOspDayAmount.Text = "일별 외주비집계(수량/금액)";
            // 
            // grdOspDateAmount
            // 
            this.grdOspDateAmount.Caption = "";
            this.grdOspDateAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOspDateAmount.IsUsePaging = false;
            this.grdOspDateAmount.LanguageKey = "OUTSOURCINGCOSTSBYDATEAMOUNT";
            this.grdOspDateAmount.Location = new System.Drawing.Point(0, 0);
            this.grdOspDateAmount.Margin = new System.Windows.Forms.Padding(0);
            this.grdOspDateAmount.Name = "grdOspDateAmount";
            this.grdOspDateAmount.ShowBorder = true;
            this.grdOspDateAmount.ShowStatusBar = false;
            this.grdOspDateAmount.Size = new System.Drawing.Size(830, 861);
            this.grdOspDateAmount.TabIndex = 8;
            // 
            // tapOspDayQty
            // 
            this.tapOspDayQty.Controls.Add(this.grdOspDateQty);
            this.tapCostDate.SetLanguageKey(this.tapOspDayQty, "OSPCOSTBYDATEQTY");
            this.tapOspDayQty.Name = "tapOspDayQty";
            this.tapOspDayQty.Size = new System.Drawing.Size(830, 861);
            this.tapOspDayQty.Text = "일별외주비집계";
            // 
            // grdOspDateQty
            // 
            this.grdOspDateQty.Caption = "";
            this.grdOspDateQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOspDateQty.IsUsePaging = false;
            this.grdOspDateQty.LanguageKey = "OUTSOURCINGCOSTSBYDATEQTY";
            this.grdOspDateQty.Location = new System.Drawing.Point(0, 0);
            this.grdOspDateQty.Margin = new System.Windows.Forms.Padding(0);
            this.grdOspDateQty.Name = "grdOspDateQty";
            this.grdOspDateQty.ShowBorder = true;
            this.grdOspDateQty.ShowStatusBar = false;
            this.grdOspDateQty.Size = new System.Drawing.Size(830, 861);
            this.grdOspDateQty.TabIndex = 8;
            // 
            // OutsourcingCostsByDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 977);
            this.Name = "OutsourcingCostsByDate";
            this.Padding = new System.Windows.Forms.Padding(19, 19, 19, 19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tapCostDate)).EndInit();
            this.tapCostDate.ResumeLayout(false);
            this.tapOspDayAmount.ResumeLayout(false);
            this.tapOspDayQty.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartTabControl tapCostDate;
        private DevExpress.XtraTab.XtraTabPage tapOspDayQty;
        private Framework.SmartControls.SmartBandedGrid grdOspDateQty;
        private DevExpress.XtraTab.XtraTabPage tapOspDayAmount;
        private Framework.SmartControls.SmartBandedGrid grdOspDateAmount;
    }
}