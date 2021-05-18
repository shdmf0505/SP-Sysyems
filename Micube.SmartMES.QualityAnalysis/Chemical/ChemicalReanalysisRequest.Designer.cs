namespace Micube.SmartMES.QualityAnalysis
{
    partial class ChemicalReanalysisRequest
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
            this.btnDeadline = new Micube.Framework.SmartControls.SmartButton();
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdGenerationHistory = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdReanalysisHistory = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 397);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlToolbar.Controls.SetChildIndex(this.smartSplitTableLayoutPanel1, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSplitTableLayoutPanel2);
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 2;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.btnDeadline, 1, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(47, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 1;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(428, 24);
            this.smartSplitTableLayoutPanel1.TabIndex = 7;
            // 
            // btnDeadline
            // 
            this.btnDeadline.AllowFocus = false;
            this.btnDeadline.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDeadline.IsBusy = false;
            this.btnDeadline.IsWrite = false;
            this.btnDeadline.LanguageKey = "DEADLINE";
            this.btnDeadline.Location = new System.Drawing.Point(350, 0);
            this.btnDeadline.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnDeadline.Name = "btnDeadline";
            this.btnDeadline.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnDeadline.Size = new System.Drawing.Size(75, 24);
            this.btnDeadline.TabIndex = 5;
            this.btnDeadline.Text = "마감";
            this.btnDeadline.TooltipLanguageKey = "";
            this.btnDeadline.Visible = false;
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.ColumnCount = 1;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdGenerationHistory, 0, 0);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdReanalysisHistory, 0, 2);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.smartSpliterControl1, 0, 1);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 3;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(475, 401);
            this.smartSplitTableLayoutPanel2.TabIndex = 0;
            // 
            // grdGenerationHistory
            // 
            this.grdGenerationHistory.Caption = "발생이력";
            this.grdGenerationHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdGenerationHistory.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdGenerationHistory.IsUsePaging = false;
            this.grdGenerationHistory.LanguageKey = "GENERATIONHISTORYLIST";
            this.grdGenerationHistory.Location = new System.Drawing.Point(0, 0);
            this.grdGenerationHistory.Margin = new System.Windows.Forms.Padding(0);
            this.grdGenerationHistory.Name = "grdGenerationHistory";
            this.grdGenerationHistory.ShowBorder = true;
            this.grdGenerationHistory.Size = new System.Drawing.Size(475, 195);
            this.grdGenerationHistory.TabIndex = 0;
            // 
            // grdReanalysisHistory
            // 
            this.grdReanalysisHistory.Caption = "재분석이력";
            this.grdReanalysisHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReanalysisHistory.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdReanalysisHistory.IsUsePaging = false;
            this.grdReanalysisHistory.LanguageKey = "REANALYSISHISTORYLIST";
            this.grdReanalysisHistory.Location = new System.Drawing.Point(0, 205);
            this.grdReanalysisHistory.Margin = new System.Windows.Forms.Padding(0);
            this.grdReanalysisHistory.Name = "grdReanalysisHistory";
            this.grdReanalysisHistory.ShowBorder = true;
            this.grdReanalysisHistory.Size = new System.Drawing.Size(475, 196);
            this.grdReanalysisHistory.TabIndex = 1;
            // 
            // smartSpliterControl1
            // 
            this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartSpliterControl1.Location = new System.Drawing.Point(0, 195);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(475, 5);
            this.smartSpliterControl1.TabIndex = 2;
            this.smartSpliterControl1.TabStop = false;
            // 
            // ChemicalReanalysisRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "ChemicalReanalysisRequest";
            this.Text = "ChemicalReanalysisRequest";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartButton btnDeadline;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
        private Framework.SmartControls.SmartBandedGrid grdGenerationHistory;
        private Framework.SmartControls.SmartBandedGrid grdReanalysisHistory;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
    }
}