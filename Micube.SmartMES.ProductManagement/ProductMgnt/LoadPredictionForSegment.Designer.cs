namespace Micube.SmartMES.ProductManagement
{
    partial class LoadPredictionForSegment
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
            this.toolTipWipInfo = new DevExpress.Utils.ToolTipController();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.grdLoad = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartGroupBox1 = new Micube.Framework.SmartControls.SmartGroupBox();
            this.txtConfirmUser = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtConfirmDate = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.smartSplitTableLayoutPanel3 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.btnInit = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).BeginInit();
            this.smartGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtConfirmUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConfirmDate.Properties)).BeginInit();
            this.smartSplitTableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.smartSplitTableLayoutPanel3);
            this.pnlToolbar.Controls.SetChildIndex(this.smartSplitTableLayoutPanel3, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdLoad);
            this.pnlContent.Controls.Add(this.smartGroupBox1);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControl1);
            this.barManager1.DockControls.Add(this.barDockControl2);
            this.barManager1.DockControls.Add(this.barDockControl3);
            this.barManager1.DockControls.Add(this.barDockControl4);
            this.barManager1.Form = this;
            this.barManager1.MaxItemId = 0;
            // 
            // barDockControl1
            // 
            this.barDockControl1.CausesValidation = false;
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(10, 10);
            this.barDockControl1.Manager = this.barManager1;
            this.barDockControl1.Size = new System.Drawing.Size(1061, 0);
            // 
            // barDockControl2
            // 
            this.barDockControl2.CausesValidation = false;
            this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl2.Location = new System.Drawing.Point(10, 528);
            this.barDockControl2.Manager = this.barManager1;
            this.barDockControl2.Size = new System.Drawing.Size(1061, 0);
            // 
            // barDockControl3
            // 
            this.barDockControl3.CausesValidation = false;
            this.barDockControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl3.Location = new System.Drawing.Point(10, 10);
            this.barDockControl3.Manager = this.barManager1;
            this.barDockControl3.Size = new System.Drawing.Size(0, 518);
            // 
            // barDockControl4
            // 
            this.barDockControl4.CausesValidation = false;
            this.barDockControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl4.Location = new System.Drawing.Point(1071, 10);
            this.barDockControl4.Manager = this.barManager1;
            this.barDockControl4.Size = new System.Drawing.Size(0, 518);
            // 
            // grdLoad
            // 
            this.grdLoad.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLoad.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLoad.IsUsePaging = false;
            this.grdLoad.LanguageKey = "RESULTANDSTOCKBYPROCESS";
            this.grdLoad.Location = new System.Drawing.Point(0, 34);
            this.grdLoad.Margin = new System.Windows.Forms.Padding(0);
            this.grdLoad.Name = "grdLoad";
            this.grdLoad.ShowBorder = true;
            this.grdLoad.Size = new System.Drawing.Size(756, 455);
            this.grdLoad.TabIndex = 8;
            this.grdLoad.UseAutoBestFitColumns = false;
            // 
            // smartGroupBox1
            // 
            this.smartGroupBox1.Controls.Add(this.txtConfirmUser);
            this.smartGroupBox1.Controls.Add(this.txtConfirmDate);
            this.smartGroupBox1.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.smartGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartGroupBox1.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.smartGroupBox1.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.smartGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.smartGroupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.smartGroupBox1.Name = "smartGroupBox1";
            this.smartGroupBox1.Padding = new System.Windows.Forms.Padding(3);
            this.smartGroupBox1.ShowBorder = true;
            this.smartGroupBox1.ShowCaption = false;
            this.smartGroupBox1.Size = new System.Drawing.Size(756, 34);
            this.smartGroupBox1.TabIndex = 9;
            this.smartGroupBox1.Text = "smartGroupBox1";
            // 
            // txtConfirmUser
            // 
            this.txtConfirmUser.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtConfirmUser.Appearance.Options.UseForeColor = true;
            this.txtConfirmUser.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtConfirmUser.EditorWidth = "70%";
            this.txtConfirmUser.LabelText = "확정자";
            this.txtConfirmUser.LabelWidth = "30%";
            this.txtConfirmUser.LanguageKey = "SETTLEUSER";
            this.txtConfirmUser.Location = new System.Drawing.Point(344, 5);
            this.txtConfirmUser.Margin = new System.Windows.Forms.Padding(0);
            this.txtConfirmUser.Name = "txtConfirmUser";
            this.txtConfirmUser.Size = new System.Drawing.Size(191, 20);
            this.txtConfirmUser.TabIndex = 5;
            // 
            // txtConfirmDate
            // 
            this.txtConfirmDate.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtConfirmDate.Appearance.Options.UseForeColor = true;
            this.txtConfirmDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtConfirmDate.EditorWidth = "80%";
            this.txtConfirmDate.LabelText = "확정시간";
            this.txtConfirmDate.LabelWidth = "20%";
            this.txtConfirmDate.LanguageKey = "CONFIRMDATE";
            this.txtConfirmDate.Location = new System.Drawing.Point(5, 5);
            this.txtConfirmDate.Margin = new System.Windows.Forms.Padding(0);
            this.txtConfirmDate.Name = "txtConfirmDate";
            this.txtConfirmDate.Size = new System.Drawing.Size(339, 20);
            this.txtConfirmDate.TabIndex = 4;
            // 
            // smartSplitTableLayoutPanel3
            // 
            this.smartSplitTableLayoutPanel3.ColumnCount = 2;
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.smartSplitTableLayoutPanel3.Controls.Add(this.btnInit, 1, 0);
            this.smartSplitTableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel3.Location = new System.Drawing.Point(47, 0);
            this.smartSplitTableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel3.Name = "smartSplitTableLayoutPanel3";
            this.smartSplitTableLayoutPanel3.RowCount = 1;
            this.smartSplitTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel3.Size = new System.Drawing.Size(709, 24);
            this.smartSplitTableLayoutPanel3.TabIndex = 6;
            // 
            // btnInit
            // 
            this.btnInit.AllowFocus = false;
            this.btnInit.IsBusy = false;
            this.btnInit.IsWrite = false;
            this.btnInit.LanguageKey = "CONFIRMATION";
            this.btnInit.Location = new System.Drawing.Point(627, 0);
            this.btnInit.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnInit.Name = "btnInit";
            this.btnInit.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnInit.Size = new System.Drawing.Size(75, 23);
            this.btnInit.TabIndex = 1;
            this.btnInit.Text = "확정";
            this.btnInit.TooltipLanguageKey = "";
            // 
            // LoadPredictionForSegment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 538);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.LanguageKey = "WIPLIST";
            this.Name = "LoadPredictionForSegment";
            this.Text = "재공현황";
            this.Controls.SetChildIndex(this.barDockControl1, 0);
            this.Controls.SetChildIndex(this.barDockControl2, 0);
            this.Controls.SetChildIndex(this.barDockControl4, 0);
            this.Controls.SetChildIndex(this.barDockControl3, 0);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).EndInit();
            this.smartGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtConfirmUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConfirmDate.Properties)).EndInit();
            this.smartSplitTableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.Utils.ToolTipController toolTipWipInfo;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private Framework.SmartControls.SmartBandedGrid grdLoad;
        private Framework.SmartControls.SmartGroupBox smartGroupBox1;
        private Framework.SmartControls.SmartLabelTextBox txtConfirmUser;
        private Framework.SmartControls.SmartLabelTextBox txtConfirmDate;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel3;
        private Framework.SmartControls.SmartButton btnInit;
    }
}