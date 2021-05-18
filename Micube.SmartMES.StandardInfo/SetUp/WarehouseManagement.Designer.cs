namespace Micube.SmartMES.StandardInfo
{
    partial class WarehouseManagement
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
            this.btnImport = new Micube.Framework.SmartControls.SmartButton();
            this.spcWarehouse = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdWarehouse = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdLocator = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spcWarehouse)).BeginInit();
            this.spcWarehouse.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Size = new System.Drawing.Size(371, 512);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnImport);
            this.pnlToolbar.Size = new System.Drawing.Size(461, 30);
            this.pnlToolbar.Controls.SetChildIndex(this.btnImport, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.spcWarehouse);
            this.pnlContent.Size = new System.Drawing.Size(461, 515);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(842, 551);
            // 
            // btnImport
            // 
            this.btnImport.AllowFocus = false;
            this.btnImport.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnImport.IsBusy = false;
            this.btnImport.LanguageKey = "IMPORT";
            this.btnImport.Location = new System.Drawing.Point(381, 0);
            this.btnImport.Margin = new System.Windows.Forms.Padding(0);
            this.btnImport.Name = "btnImport";
            this.btnImport.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnImport.Size = new System.Drawing.Size(80, 30);
            this.btnImport.TabIndex = 8;
            this.btnImport.Text = "smartButton1";
            this.btnImport.TooltipLanguageKey = "";
            this.btnImport.Visible = false;
            // 
            // spcWarehouse
            // 
            this.spcWarehouse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcWarehouse.Horizontal = false;
            this.spcWarehouse.Location = new System.Drawing.Point(0, 0);
            this.spcWarehouse.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcWarehouse.Name = "spcWarehouse";
            this.spcWarehouse.Panel1.Controls.Add(this.grdWarehouse);
            this.spcWarehouse.Panel1.Text = "Panel1";
            this.spcWarehouse.Panel2.Controls.Add(this.grdLocator);
            this.spcWarehouse.Panel2.Text = "Panel2";
            this.spcWarehouse.Size = new System.Drawing.Size(461, 515);
            this.spcWarehouse.SplitterPosition = 400;
            this.spcWarehouse.TabIndex = 0;
            this.spcWarehouse.Text = "smartSpliterContainer1";
            // 
            // grdWarehouse
            // 
            this.grdWarehouse.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdWarehouse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdWarehouse.IsUsePaging = false;
            this.grdWarehouse.LanguageKey = "WAREHOUSELIST";
            this.grdWarehouse.Location = new System.Drawing.Point(0, 0);
            this.grdWarehouse.Margin = new System.Windows.Forms.Padding(0);
            this.grdWarehouse.Name = "grdWarehouse";
            this.grdWarehouse.ShowBorder = true;
            this.grdWarehouse.Size = new System.Drawing.Size(461, 400);
            this.grdWarehouse.TabIndex = 0;
            // 
            // grdLocator
            // 
            this.grdLocator.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLocator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLocator.IsUsePaging = false;
            this.grdLocator.LanguageKey = "LOCATORLIST";
            this.grdLocator.Location = new System.Drawing.Point(0, 0);
            this.grdLocator.Margin = new System.Windows.Forms.Padding(0);
            this.grdLocator.Name = "grdLocator";
            this.grdLocator.ShowBorder = true;
            this.grdLocator.Size = new System.Drawing.Size(461, 109);
            this.grdLocator.TabIndex = 0;
            // 
            // WarehouseManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 589);
            this.Name = "WarehouseManagement";
            this.Padding = new System.Windows.Forms.Padding(19, 19, 19, 19);
            this.Text = "CustomerManagement";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spcWarehouse)).EndInit();
            this.spcWarehouse.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Framework.SmartControls.SmartButton btnImport;
        private Framework.SmartControls.SmartSpliterContainer spcWarehouse;
        private Framework.SmartControls.SmartBandedGrid grdWarehouse;
        private Framework.SmartControls.SmartBandedGrid grdLocator;
    }
}