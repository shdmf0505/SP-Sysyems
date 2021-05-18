namespace Micube.SmartMES.OutsideOrderMgnt
{
    partial class SubcontractPriceRegistration
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
            this.tblMain = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.tblSubsub = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdCombination = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdOspPrice = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            this.smartPanel2 = new Micube.Framework.SmartControls.SmartPanel();
            this.btnPriceSave = new Micube.Framework.SmartControls.SmartButton();
            this.smartLabel2 = new Micube.Framework.SmartControls.SmartLabel();
            this.tblMainSub = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdClassPirceCode = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdProSClass = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.tblMain.SuspendLayout();
            this.tblSubsub.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).BeginInit();
            this.smartPanel2.SuspendLayout();
            this.tblMainSub.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Size = new System.Drawing.Size(371, 908);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(845, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tblMain);
            this.pnlContent.Size = new System.Drawing.Size(845, 911);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1226, 947);
            // 
            // tblMain
            // 
            this.tblMain.ColumnCount = 1;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.Controls.Add(this.tblSubsub, 0, 2);
            this.tblMain.Controls.Add(this.tblMainSub, 0, 0);
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.Location = new System.Drawing.Point(0, 0);
            this.tblMain.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 3;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tblMain.Size = new System.Drawing.Size(845, 911);
            this.tblMain.TabIndex = 0;
            // 
            // tblSubsub
            // 
            this.tblSubsub.ColumnCount = 3;
            this.tblSubsub.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tblSubsub.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblSubsub.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblSubsub.Controls.Add(this.grdCombination, 0, 1);
            this.tblSubsub.Controls.Add(this.grdOspPrice, 2, 1);
            this.tblSubsub.Controls.Add(this.smartPanel1, 0, 0);
            this.tblSubsub.Controls.Add(this.smartPanel2, 2, 0);
            this.tblSubsub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblSubsub.Location = new System.Drawing.Point(0, 550);
            this.tblSubsub.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tblSubsub.Name = "tblSubsub";
            this.tblSubsub.RowCount = 2;
            this.tblSubsub.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblSubsub.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblSubsub.Size = new System.Drawing.Size(845, 361);
            this.tblSubsub.TabIndex = 1;
            // 
            // grdCombination
            // 
            this.grdCombination.Caption = "";
            this.grdCombination.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCombination.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdCombination.IsUsePaging = false;
            this.grdCombination.LanguageKey = "BREAKDOWNBYOUTSOURCINGUNITPRICE";
            this.grdCombination.Location = new System.Drawing.Point(0, 40);
            this.grdCombination.Margin = new System.Windows.Forms.Padding(0);
            this.grdCombination.Name = "grdCombination";
            this.grdCombination.ShowBorder = true;
            this.grdCombination.Size = new System.Drawing.Size(584, 321);
            this.grdCombination.TabIndex = 4;
            // 
            // grdOspPrice
            // 
            this.grdOspPrice.Caption = "";
            this.grdOspPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOspPrice.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdOspPrice.IsUsePaging = false;
            this.grdOspPrice.LanguageKey = "OUTSOURCEDUNITPRICEREGISTRATIONDETAILS";
            this.grdOspPrice.Location = new System.Drawing.Point(594, 40);
            this.grdOspPrice.Margin = new System.Windows.Forms.Padding(0);
            this.grdOspPrice.Name = "grdOspPrice";
            this.grdOspPrice.ShowBorder = true;
            this.grdOspPrice.Size = new System.Drawing.Size(251, 321);
            this.grdOspPrice.TabIndex = 3;
            // 
            // smartPanel1
            // 
            this.smartPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel1.Controls.Add(this.btnSave);
            this.smartPanel1.Controls.Add(this.smartLabel1);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(584, 40);
            this.smartPanel1.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = true;
            this.btnSave.Location = new System.Drawing.Point(501, 6);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "저장";
            this.btnSave.TooltipLanguageKey = "";
            // 
            // smartLabel1
            // 
            this.smartLabel1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.smartLabel1.Appearance.Options.UseFont = true;
            this.smartLabel1.LanguageKey = "BASEDONOUTSOURCINGUNITPRICE";
            this.smartLabel1.Location = new System.Drawing.Point(14, 8);
            this.smartLabel1.Name = "smartLabel1";
            this.smartLabel1.Size = new System.Drawing.Size(110, 24);
            this.smartLabel1.TabIndex = 0;
            this.smartLabel1.Text = "smartLabel1";
            // 
            // smartPanel2
            // 
            this.smartPanel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel2.Controls.Add(this.btnPriceSave);
            this.smartPanel2.Controls.Add(this.smartLabel2);
            this.smartPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel2.Location = new System.Drawing.Point(594, 0);
            this.smartPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel2.Name = "smartPanel2";
            this.smartPanel2.Size = new System.Drawing.Size(251, 40);
            this.smartPanel2.TabIndex = 5;
            // 
            // btnPriceSave
            // 
            this.btnPriceSave.AllowFocus = false;
            this.btnPriceSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPriceSave.IsBusy = false;
            this.btnPriceSave.IsWrite = true;
            this.btnPriceSave.Location = new System.Drawing.Point(164, 6);
            this.btnPriceSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPriceSave.Name = "btnPriceSave";
            this.btnPriceSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPriceSave.Size = new System.Drawing.Size(80, 25);
            this.btnPriceSave.TabIndex = 1;
            this.btnPriceSave.Text = "저장";
            this.btnPriceSave.TooltipLanguageKey = "";
            // 
            // smartLabel2
            // 
            this.smartLabel2.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.smartLabel2.Appearance.Options.UseFont = true;
            this.smartLabel2.LanguageKey = "OUTSOURCINGUNITPRICE";
            this.smartLabel2.Location = new System.Drawing.Point(5, 8);
            this.smartLabel2.Name = "smartLabel2";
            this.smartLabel2.Size = new System.Drawing.Size(110, 24);
            this.smartLabel2.TabIndex = 0;
            this.smartLabel2.Text = "smartLabel1";
            // 
            // tblMainSub
            // 
            this.tblMainSub.ColumnCount = 3;
            this.tblMainSub.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tblMainSub.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblMainSub.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tblMainSub.Controls.Add(this.grdClassPirceCode, 2, 0);
            this.tblMainSub.Controls.Add(this.grdProSClass, 0, 0);
            this.tblMainSub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMainSub.Location = new System.Drawing.Point(0, 0);
            this.tblMainSub.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tblMainSub.Name = "tblMainSub";
            this.tblMainSub.RowCount = 1;
            this.tblMainSub.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainSub.Size = new System.Drawing.Size(845, 540);
            this.tblMainSub.TabIndex = 0;
            // 
            // grdClassPirceCode
            // 
            this.grdClassPirceCode.Caption = "";
            this.grdClassPirceCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdClassPirceCode.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdClassPirceCode.IsUsePaging = false;
            this.grdClassPirceCode.LanguageKey = "OUTSOURCINGUNITPRICEMANAGEMENTLIST";
            this.grdClassPirceCode.Location = new System.Drawing.Point(344, 0);
            this.grdClassPirceCode.Margin = new System.Windows.Forms.Padding(0);
            this.grdClassPirceCode.Name = "grdClassPirceCode";
            this.grdClassPirceCode.ShowBorder = true;
            this.grdClassPirceCode.Size = new System.Drawing.Size(501, 540);
            this.grdClassPirceCode.TabIndex = 4;
            // 
            // grdProSClass
            // 
            this.grdProSClass.Caption = "";
            this.grdProSClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProSClass.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdProSClass.IsUsePaging = false;
            this.grdProSClass.LanguageKey = "PROCESSSEGMENTCLASSLIST";
            this.grdProSClass.Location = new System.Drawing.Point(0, 0);
            this.grdProSClass.Margin = new System.Windows.Forms.Padding(0);
            this.grdProSClass.Name = "grdProSClass";
            this.grdProSClass.ShowBorder = true;
            this.grdProSClass.Size = new System.Drawing.Size(334, 540);
            this.grdProSClass.TabIndex = 3;
            // 
            // SubcontractPriceRegistration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 985);
            this.Name = "SubcontractPriceRegistration";
            this.Padding = new System.Windows.Forms.Padding(19, 19, 19, 19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.tblMain.ResumeLayout(false);
            this.tblSubsub.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.smartPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).EndInit();
            this.smartPanel2.ResumeLayout(false);
            this.smartPanel2.PerformLayout();
            this.tblMainSub.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel tblMain;
        private Framework.SmartControls.SmartSplitTableLayoutPanel tblSubsub;
        private Framework.SmartControls.SmartSplitTableLayoutPanel tblMainSub;
        private Framework.SmartControls.SmartBandedGrid grdCombination;
        private Framework.SmartControls.SmartBandedGrid grdOspPrice;
        private Framework.SmartControls.SmartBandedGrid grdClassPirceCode;
        private Framework.SmartControls.SmartBandedGrid grdProSClass;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartPanel smartPanel2;
        private Framework.SmartControls.SmartLabel smartLabel1;
        private Framework.SmartControls.SmartLabel smartLabel2;
        private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartButton btnPriceSave;
    }
}