namespace Micube.SmartMES.StandardInfo
{
    partial class LeadTime
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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.grdlayer = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdproduct = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 522);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(1011, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.splitContainerControl1);
            this.pnlContent.Size = new System.Drawing.Size(1011, 526);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1316, 555);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.grdlayer);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.grdproduct);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1011, 526);
            this.splitContainerControl1.SplitterPosition = 304;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // grdlayer
            // 
            this.grdlayer.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdlayer.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdlayer.IsUsePaging = false;
            this.grdlayer.LanguageKey = "LAYERLIST";
            this.grdlayer.Location = new System.Drawing.Point(0, 0);
            this.grdlayer.Margin = new System.Windows.Forms.Padding(0);
            this.grdlayer.Name = "grdlayer";
            this.grdlayer.ShowBorder = true;
            this.grdlayer.Size = new System.Drawing.Size(304, 526);
            this.grdlayer.TabIndex = 0;
            this.grdlayer.UseAutoBestFitColumns = false;
            // 
            // grdproduct
            // 
            this.grdproduct.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdproduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdproduct.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdproduct.IsUsePaging = false;
            this.grdproduct.LanguageKey = "GRIDPRODUCTLIST";
            this.grdproduct.Location = new System.Drawing.Point(0, 0);
            this.grdproduct.Margin = new System.Windows.Forms.Padding(0);
            this.grdproduct.Name = "grdproduct";
            this.grdproduct.ShowBorder = true;
            this.grdproduct.Size = new System.Drawing.Size(702, 526);
            this.grdproduct.TabIndex = 0;
            this.grdproduct.UseAutoBestFitColumns = false;
            // 
            // LeadTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1354, 593);
            this.Name = "LeadTime";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "MasterDataClass";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private Framework.SmartControls.SmartBandedGrid grdlayer;
        private Framework.SmartControls.SmartBandedGrid grdproduct;
    }
}