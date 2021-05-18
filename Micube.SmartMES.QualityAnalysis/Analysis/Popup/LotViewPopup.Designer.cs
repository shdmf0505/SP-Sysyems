namespace Micube.SmartMES.QualityAnalysis
{
    partial class LotViewPopup
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
            this.grdLOTView = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.pnlTopInfo = new Micube.Framework.SmartControls.SmartPanel();
            this.pnlBodyGrid = new Micube.Framework.SmartControls.SmartPanel();
            this.btnLOTAnalysis = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTopInfo)).BeginInit();
            this.pnlTopInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBodyGrid)).BeginInit();
            this.pnlBodyGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlBodyGrid);
            this.pnlMain.Controls.Add(this.pnlTopInfo);
            this.pnlMain.Size = new System.Drawing.Size(852, 360);
            // 
            // grdLOTView
            // 
            this.grdLOTView.Caption = "";
            this.grdLOTView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLOTView.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLOTView.IsUsePaging = false;
            this.grdLOTView.LanguageKey = null;
            this.grdLOTView.Location = new System.Drawing.Point(2, 2);
            this.grdLOTView.Margin = new System.Windows.Forms.Padding(0);
            this.grdLOTView.Name = "grdLOTView";
            this.grdLOTView.ShowBorder = true;
            this.grdLOTView.ShowButtonBar = false;
            this.grdLOTView.Size = new System.Drawing.Size(848, 324);
            this.grdLOTView.TabIndex = 0;
            this.grdLOTView.UseAutoBestFitColumns = false;
            // 
            // pnlTopInfo
            // 
            this.pnlTopInfo.Controls.Add(this.btnLOTAnalysis);
            this.pnlTopInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlTopInfo.Name = "pnlTopInfo";
            this.pnlTopInfo.Size = new System.Drawing.Size(852, 32);
            this.pnlTopInfo.TabIndex = 1;
            // 
            // pnlBodyGrid
            // 
            this.pnlBodyGrid.Controls.Add(this.grdLOTView);
            this.pnlBodyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBodyGrid.Location = new System.Drawing.Point(0, 32);
            this.pnlBodyGrid.Name = "pnlBodyGrid";
            this.pnlBodyGrid.Size = new System.Drawing.Size(852, 328);
            this.pnlBodyGrid.TabIndex = 2;
            // 
            // btnLOTAnalysis
            // 
            this.btnLOTAnalysis.AllowFocus = false;
            this.btnLOTAnalysis.IsBusy = false;
            this.btnLOTAnalysis.IsWrite = false;
            this.btnLOTAnalysis.LanguageKey = "LOTANALYSIS";
            this.btnLOTAnalysis.Location = new System.Drawing.Point(5, 4);
            this.btnLOTAnalysis.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnLOTAnalysis.Name = "btnLOTAnalysis";
            this.btnLOTAnalysis.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnLOTAnalysis.Size = new System.Drawing.Size(106, 25);
            this.btnLOTAnalysis.TabIndex = 1;
            this.btnLOTAnalysis.Text = "LOT 분석";
            this.btnLOTAnalysis.TooltipLanguageKey = "";
            // 
            // LotViewPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 380);
            this.LanguageKey = "LOTVIEW";
            this.Name = "LotViewPopup";
            this.Text = "LOT 보기";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTopInfo)).EndInit();
            this.pnlTopInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlBodyGrid)).EndInit();
            this.pnlBodyGrid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdLOTView;
        private Framework.SmartControls.SmartPanel pnlBodyGrid;
        private Framework.SmartControls.SmartPanel pnlTopInfo;
        private Framework.SmartControls.SmartButton btnLOTAnalysis;
    }
}