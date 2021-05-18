namespace Micube.SmartMES.OutsideOrderMgnt
{
    partial class OutsourcedClaimConfirmation
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
            this.smartPanel2 = new Micube.Framework.SmartControls.SmartPanel();
            this.btnClosePeriod = new Micube.Framework.SmartControls.SmartButton();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            this.txtReducerate = new Micube.Framework.SmartControls.SmartTextBox();
            this.txtReduceqty = new Micube.Framework.SmartControls.SmartTextBox();
            this.txtDescription = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblDescription = new Micube.Framework.SmartControls.SmartLabel();
            this.lblReducerate = new Micube.Framework.SmartControls.SmartLabel();
            this.lblReduceqty = new Micube.Framework.SmartControls.SmartLabel();
            this.btnReduction = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.tplMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).BeginInit();
            this.smartPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtReducerate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReduceqty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(371, 900);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnClosePeriod);
            this.pnlToolbar.Controls.Add(this.btnSave);
            this.pnlToolbar.Controls.Add(this.btnReduction);
            this.pnlToolbar.Size = new System.Drawing.Size(843, 30);
            this.pnlToolbar.Controls.SetChildIndex(this.btnReduction, 0);
            this.pnlToolbar.Controls.SetChildIndex(this.btnSave, 0);
            this.pnlToolbar.Controls.SetChildIndex(this.btnClosePeriod, 0);
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
            this.tplMain.Controls.Add(this.smartPanel2, 0, 0);
            this.tplMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplMain.Location = new System.Drawing.Point(0, 0);
            this.tplMain.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tplMain.Name = "tplMain";
            this.tplMain.RowCount = 2;
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplMain.Size = new System.Drawing.Size(843, 903);
            this.tplMain.TabIndex = 0;
            // 
            // grdClaimConfirm
            // 
            this.grdClaimConfirm.Caption = "";
            this.grdClaimConfirm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdClaimConfirm.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdClaimConfirm.IsUsePaging = false;
            this.grdClaimConfirm.LanguageKey = "OUTSOURCEDCLAIMCONFIRMATIONLIST";
            this.grdClaimConfirm.Location = new System.Drawing.Point(0, 40);
            this.grdClaimConfirm.Margin = new System.Windows.Forms.Padding(0);
            this.grdClaimConfirm.Name = "grdClaimConfirm";
            this.grdClaimConfirm.ShowBorder = true;
            this.grdClaimConfirm.ShowStatusBar = false;
            this.grdClaimConfirm.Size = new System.Drawing.Size(843, 863);
            this.grdClaimConfirm.TabIndex = 6;
            this.grdClaimConfirm.UseAutoBestFitColumns = false;
            // 
            // smartPanel2
            // 
            this.smartPanel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel2.Controls.Add(this.txtReducerate);
            this.smartPanel2.Controls.Add(this.txtReduceqty);
            this.smartPanel2.Controls.Add(this.txtDescription);
            this.smartPanel2.Controls.Add(this.lblDescription);
            this.smartPanel2.Controls.Add(this.lblReducerate);
            this.smartPanel2.Controls.Add(this.lblReduceqty);
            this.smartPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel2.Location = new System.Drawing.Point(0, 0);
            this.smartPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel2.Name = "smartPanel2";
            this.smartPanel2.Size = new System.Drawing.Size(843, 40);
            this.smartPanel2.TabIndex = 7;
            // 
            // btnClosePeriod
            // 
            this.btnClosePeriod.AllowFocus = false;
            this.btnClosePeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClosePeriod.IsBusy = false;
            this.btnClosePeriod.IsWrite = true;
            this.btnClosePeriod.LanguageKey = "CLOSEPERIOD";
            this.btnClosePeriod.Location = new System.Drawing.Point(674, 5);
            this.btnClosePeriod.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnClosePeriod.Name = "btnClosePeriod";
            this.btnClosePeriod.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClosePeriod.Size = new System.Drawing.Size(80, 25);
            this.btnClosePeriod.TabIndex = 3;
            this.btnClosePeriod.Text = "마감기간";
            this.btnClosePeriod.TooltipLanguageKey = "";
            this.btnClosePeriod.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = true;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(588, 4);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "저장";
            this.btnSave.TooltipLanguageKey = "";
            this.btnSave.Visible = false;
            // 
            // txtReducerate
            // 
            this.txtReducerate.LabelText = null;
            this.txtReducerate.LanguageKey = null;
            this.txtReducerate.Location = new System.Drawing.Point(366, 9);
            this.txtReducerate.Name = "txtReducerate";
            this.txtReducerate.Properties.Appearance.Options.UseTextOptions = true;
            this.txtReducerate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtReducerate.Properties.DisplayFormat.FormatString = "#0.##";
            this.txtReducerate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtReducerate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtReducerate.Properties.Mask.EditMask = "#0.##";
            this.txtReducerate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtReducerate.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtReducerate.Size = new System.Drawing.Size(70, 24);
            this.txtReducerate.TabIndex = 10;
            // 
            // txtReduceqty
            // 
            this.txtReduceqty.LabelText = null;
            this.txtReduceqty.LanguageKey = null;
            this.txtReduceqty.Location = new System.Drawing.Point(136, 9);
            this.txtReduceqty.Name = "txtReduceqty";
            this.txtReduceqty.Properties.Appearance.Options.UseTextOptions = true;
            this.txtReduceqty.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtReduceqty.Properties.DisplayFormat.FormatString = "#,###,###,##0";
            this.txtReduceqty.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtReduceqty.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtReduceqty.Properties.Mask.EditMask = "#,###,###,##0";
            this.txtReduceqty.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtReduceqty.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtReduceqty.Size = new System.Drawing.Size(70, 24);
            this.txtReduceqty.TabIndex = 10;
            // 
            // txtDescription
            // 
            this.txtDescription.LabelText = null;
            this.txtDescription.LanguageKey = null;
            this.txtDescription.Location = new System.Drawing.Point(588, 9);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(248, 24);
            this.txtDescription.TabIndex = 9;
            // 
            // lblDescription
            // 
            this.lblDescription.LanguageKey = "DESCRIPTION";
            this.lblDescription.Location = new System.Drawing.Point(453, 9);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(26, 18);
            this.lblDescription.TabIndex = 7;
            this.lblDescription.Text = "비고";
            // 
            // lblReducerate
            // 
            this.lblReducerate.LanguageKey = "REDUCERATE";
            this.lblReducerate.Location = new System.Drawing.Point(228, 9);
            this.lblReducerate.Name = "lblReducerate";
            this.lblReducerate.Size = new System.Drawing.Size(39, 18);
            this.lblReducerate.TabIndex = 7;
            this.lblReducerate.Text = "감면율";
            // 
            // lblReduceqty
            // 
            this.lblReduceqty.LanguageKey = "OSPREDUCEQTY";
            this.lblReduceqty.Location = new System.Drawing.Point(3, 9);
            this.lblReduceqty.Name = "lblReduceqty";
            this.lblReduceqty.Size = new System.Drawing.Size(39, 18);
            this.lblReduceqty.TabIndex = 7;
            this.lblReduceqty.Text = "감면수";
            // 
            // btnReduction
            // 
            this.btnReduction.AllowFocus = false;
            this.btnReduction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReduction.IsBusy = false;
            this.btnReduction.IsWrite = true;
            this.btnReduction.LanguageKey = "REDUCTION";
            this.btnReduction.Location = new System.Drawing.Point(756, 1);
            this.btnReduction.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnReduction.Name = "btnReduction";
            this.btnReduction.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnReduction.Size = new System.Drawing.Size(80, 25);
            this.btnReduction.TabIndex = 4;
            this.btnReduction.Text = "감면처리";
            this.btnReduction.TooltipLanguageKey = "";
            this.btnReduction.Visible = false;
            // 
            // OutsourcedClaimConfirmation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 977);
            this.Name = "OutsourcedClaimConfirmation";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.tplMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).EndInit();
            this.smartPanel2.ResumeLayout(false);
            this.smartPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtReducerate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReduceqty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel tplMain;
        private Framework.SmartControls.SmartBandedGrid grdClaimConfirm;
        private Framework.SmartControls.SmartPanel smartPanel2;
        private Framework.SmartControls.SmartButton btnClosePeriod;
        private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartButton btnReduction;
        private Framework.SmartControls.SmartTextBox txtDescription;
        private Framework.SmartControls.SmartLabel lblDescription;
        private Framework.SmartControls.SmartLabel lblReducerate;
        private Framework.SmartControls.SmartLabel lblReduceqty;
        private Framework.SmartControls.SmartTextBox txtReducerate;
        private Framework.SmartControls.SmartTextBox txtReduceqty;
    }
}