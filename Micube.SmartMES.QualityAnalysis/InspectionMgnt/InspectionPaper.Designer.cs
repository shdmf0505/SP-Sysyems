namespace Micube.SmartMES.QualityAnalysis
{
    partial class InspectionPaper
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InspectionPaper));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grdInspectionPaper = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.fileInspectionPaper = new Micube.SmartMES.Commons.Controls.SmartFileProcessingControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 397);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tableLayoutPanel1);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.grdInspectionPaper, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.fileInspectionPaper, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(475, 401);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // grdInspectionPaper
            // 
            this.grdInspectionPaper.Caption = "검사원 평가문제지 현황";
            this.grdInspectionPaper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInspectionPaper.IsUsePaging = false;
            this.grdInspectionPaper.LanguageKey = "INSPECTIONPAPERMANAGEMENT";
            this.grdInspectionPaper.Location = new System.Drawing.Point(0, 0);
            this.grdInspectionPaper.Margin = new System.Windows.Forms.Padding(0);
            this.grdInspectionPaper.Name = "grdInspectionPaper";
            this.grdInspectionPaper.ShowBorder = true;
            this.grdInspectionPaper.Size = new System.Drawing.Size(475, 195);
            this.grdInspectionPaper.TabIndex = 0;
            // 
            // fileInspectionPaper
            // 
            this.fileInspectionPaper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileInspectionPaper.LanguageKey = "";
            this.fileInspectionPaper.Location = new System.Drawing.Point(0, 205);
            this.fileInspectionPaper.Margin = new System.Windows.Forms.Padding(0);
            this.fileInspectionPaper.Name = "fileInspectionPaper";
            this.fileInspectionPaper.Size = new System.Drawing.Size(475, 196);
            this.fileInspectionPaper.TabIndex = 1;
            this.fileInspectionPaper.UploadPath = "";
            this.fileInspectionPaper.UseCommentsColumn = true;
            // 
            // InspectionPaper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "InspectionPaper";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartBandedGrid grdInspectionPaper;
        private Commons.Controls.SmartFileProcessingControl fileInspectionPaper;
    }
}