namespace Micube.SmartMES.StandardInfo
{
    partial class SubMaterialRoutingBom
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
            this.grdMain = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.grbRight = new Micube.Framework.SmartControls.SmartGroupBox();
            this.pnlRight = new Micube.Framework.SmartControls.SmartPanel();
            this.btnImport = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grbRight)).BeginInit();
            this.grbRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlRight)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 397);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnImport);
            this.pnlToolbar.Size = new System.Drawing.Size(475, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.btnImport, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlContent.Size = new System.Drawing.Size(475, 401);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(780, 430);
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 3;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdMain, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartSpliterControl1, 1, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grbRight, 2, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 1;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(475, 401);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // grdMain
            // 
            this.grdMain.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdMain.IsUsePaging = false;
            this.grdMain.LanguageKey = null;
            this.grdMain.Location = new System.Drawing.Point(0, 0);
            this.grdMain.Margin = new System.Windows.Forms.Padding(0);
            this.grdMain.Name = "grdMain";
            this.grdMain.ShowBorder = true;
            this.grdMain.Size = new System.Drawing.Size(176, 401);
            this.grdMain.TabIndex = 0;
            this.grdMain.UseAutoBestFitColumns = false;
            // 
            // smartSpliterControl1
            // 
            this.smartSpliterControl1.Location = new System.Drawing.Point(176, 0);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(5, 401);
            this.smartSpliterControl1.TabIndex = 1;
            this.smartSpliterControl1.TabStop = false;
            // 
            // grbRight
            // 
            this.grbRight.Controls.Add(this.pnlRight);
            this.grbRight.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.grbRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbRight.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.grbRight.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.grbRight.Location = new System.Drawing.Point(186, 0);
            this.grbRight.Margin = new System.Windows.Forms.Padding(0);
            this.grbRight.Name = "grbRight";
            this.grbRight.ShowBorder = true;
            this.grbRight.Size = new System.Drawing.Size(289, 401);
            this.grbRight.TabIndex = 2;
            this.grbRight.Text = "smartGroupBox1";
            // 
            // pnlRight
            // 
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(2, 31);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(3);
            this.pnlRight.Size = new System.Drawing.Size(285, 368);
            this.pnlRight.TabIndex = 0;
            // 
            // btnImport
            // 
            this.btnImport.AllowFocus = false;
            this.btnImport.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnImport.IsBusy = false;
            this.btnImport.IsWrite = false;
            this.btnImport.Location = new System.Drawing.Point(395, 0);
            this.btnImport.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnImport.Name = "btnImport";
            this.btnImport.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnImport.Size = new System.Drawing.Size(80, 24);
            this.btnImport.TabIndex = 5;
            this.btnImport.Text = "smartButton1";
            this.btnImport.TooltipLanguageKey = "";
            // 
            // SubMaterialRoutingBom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "SubMaterialRoutingBom";
            this.Text = "SubMaterialRoutingBom";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grbRight)).EndInit();
            this.grbRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlRight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartBandedGrid grdMain;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
        private Framework.SmartControls.SmartGroupBox grbRight;
        private Framework.SmartControls.SmartPanel pnlRight;
        private Framework.SmartControls.SmartButton btnImport;
    }
}