namespace Micube.SmartMES.StandardInfo
{
    partial class VendorManagement
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
            this.grdVendor = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.btnImport = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Size = new System.Drawing.Size(371, 373);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnImport);
            this.pnlToolbar.Size = new System.Drawing.Size(381, 30);
            this.pnlToolbar.Controls.SetChildIndex(this.btnImport, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdVendor);
            this.pnlContent.Size = new System.Drawing.Size(381, 376);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(762, 412);
            // 
            // grdVendor
            // 
            this.grdVendor.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdVendor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdVendor.IsUsePaging = false;
            this.grdVendor.LanguageKey = "VENDORLIST";
            this.grdVendor.Location = new System.Drawing.Point(0, 0);
            this.grdVendor.Margin = new System.Windows.Forms.Padding(0);
            this.grdVendor.Name = "grdVendor";
            this.grdVendor.ShowBorder = true;
            this.grdVendor.Size = new System.Drawing.Size(381, 376);
            this.grdVendor.TabIndex = 0;
            // 
            // btnImport
            // 
            this.btnImport.AllowFocus = false;
            this.btnImport.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnImport.IsBusy = false;
            this.btnImport.LanguageKey = "IMPORT";
            this.btnImport.Location = new System.Drawing.Point(301, 0);
            this.btnImport.Margin = new System.Windows.Forms.Padding(0);
            this.btnImport.Name = "btnImport";
            this.btnImport.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnImport.Size = new System.Drawing.Size(80, 30);
            this.btnImport.TabIndex = 8;
            this.btnImport.Text = "smartButton1";
            this.btnImport.TooltipLanguageKey = "";
            this.btnImport.Visible = false;
            // 
            // VendorManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "VendorManagement";
            this.Padding = new System.Windows.Forms.Padding(19, 19, 19, 19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdVendor;
        private Framework.SmartControls.SmartButton btnImport;
    }
}