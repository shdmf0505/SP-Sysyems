namespace Micube.SmartMES.Test
{
    partial class TestGrid
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
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdUserlist = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdAreaList = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Size = new System.Drawing.Size(371, 471);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(1040, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer1);
            this.pnlContent.Size = new System.Drawing.Size(1040, 474);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1421, 510);
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdUserlist);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdAreaList);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(1040, 474);
            this.smartSpliterContainer1.SplitterPosition = 792;
            this.smartSpliterContainer1.TabIndex = 0;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdUserlist
            // 
            this.grdUserlist.Caption = "grdUserlist";
            this.grdUserlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdUserlist.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy)
            | Micube.Framework.SmartControls.GridButtonItem.Delete)
            | Micube.Framework.SmartControls.GridButtonItem.Preview)
            | Micube.Framework.SmartControls.GridButtonItem.Import)
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdUserlist.IsUsePaging = false;
            this.grdUserlist.LanguageKey = "GRIDUSERLIST";
            this.grdUserlist.Location = new System.Drawing.Point(0, 0);
            this.grdUserlist.Margin = new System.Windows.Forms.Padding(0);
            this.grdUserlist.Name = "grdUserlist";
            this.grdUserlist.ShowBorder = true;
            this.grdUserlist.Size = new System.Drawing.Size(792, 474);
            this.grdUserlist.TabIndex = 0;
            this.grdUserlist.UseAutoBestFitColumns = false;
            // 
            // grdAreaList
            // 
            this.grdAreaList.Caption = "grdAreaList";
            this.grdAreaList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAreaList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy)
            | Micube.Framework.SmartControls.GridButtonItem.Delete)
            | Micube.Framework.SmartControls.GridButtonItem.Preview)
            | Micube.Framework.SmartControls.GridButtonItem.Import)
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdAreaList.IsUsePaging = false;
            this.grdAreaList.LanguageKey = "AREAINFOLIST";
            this.grdAreaList.Location = new System.Drawing.Point(0, 0);
            this.grdAreaList.Margin = new System.Windows.Forms.Padding(0);
            this.grdAreaList.Name = "grdAreaList";
            this.grdAreaList.ShowBorder = true;
            this.grdAreaList.Size = new System.Drawing.Size(242, 474);
            this.grdAreaList.TabIndex = 0;
            this.grdAreaList.UseAutoBestFitColumns = false;
            // 
            // UserAreaStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1459, 548);
            this.Name = "UserAreaStatus";
            this.Padding = new System.Windows.Forms.Padding(19, 19, 19, 19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdUserlist;
        private Framework.SmartControls.SmartBandedGrid grdAreaList;
    }
}