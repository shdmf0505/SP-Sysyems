namespace Micube.SmartMES.QualityAnalysis
{
    partial class MeasuringInstCalibrationPlan
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
            this.grdCalibration = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.sptMain = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdRNR = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdMiddle = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.pnlGrid = new Micube.Framework.SmartControls.SmartPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.sptMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGrid)).BeginInit();
            this.pnlGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 397);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(475, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.pnlGrid);
            this.pnlContent.Size = new System.Drawing.Size(475, 401);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(780, 430);
            // 
            // grdCalibration
            // 
            this.grdCalibration.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdCalibration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCalibration.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdCalibration.IsUsePaging = false;
            this.grdCalibration.LanguageKey = "MEASURINGCALIBRATION";
            this.grdCalibration.Location = new System.Drawing.Point(0, 0);
            this.grdCalibration.Margin = new System.Windows.Forms.Padding(0);
            this.grdCalibration.Name = "grdCalibration";
            this.grdCalibration.Padding = new System.Windows.Forms.Padding(3);
            this.grdCalibration.ShowBorder = true;
            this.grdCalibration.Size = new System.Drawing.Size(471, 132);
            this.grdCalibration.TabIndex = 0;
            this.grdCalibration.UseAutoBestFitColumns = false;
            // 
            // sptMain
            // 
            this.sptMain.ColumnCount = 1;
            this.sptMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.sptMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.sptMain.Controls.Add(this.grdRNR, 0, 2);
            this.sptMain.Controls.Add(this.grdCalibration, 0, 0);
            this.sptMain.Controls.Add(this.grdMiddle, 0, 1);
            this.sptMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sptMain.Location = new System.Drawing.Point(2, 2);
            this.sptMain.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.sptMain.Name = "sptMain";
            this.sptMain.RowCount = 3;
            this.sptMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.sptMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.sptMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.sptMain.Size = new System.Drawing.Size(471, 397);
            this.sptMain.TabIndex = 1;
            // 
            // grdRNR
            // 
            this.grdRNR.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdRNR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRNR.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdRNR.IsUsePaging = false;
            this.grdRNR.LanguageKey = "MEASURINGRNR";
            this.grdRNR.Location = new System.Drawing.Point(0, 264);
            this.grdRNR.Margin = new System.Windows.Forms.Padding(0);
            this.grdRNR.Name = "grdRNR";
            this.grdRNR.Padding = new System.Windows.Forms.Padding(3);
            this.grdRNR.ShowBorder = true;
            this.grdRNR.Size = new System.Drawing.Size(471, 133);
            this.grdRNR.TabIndex = 3;
            this.grdRNR.UseAutoBestFitColumns = false;
            // 
            // grdMiddle
            // 
            this.grdMiddle.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMiddle.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdMiddle.IsUsePaging = false;
            this.grdMiddle.LanguageKey = "MEASURINGINTERMEDIATE INSPECTION";
            this.grdMiddle.Location = new System.Drawing.Point(0, 132);
            this.grdMiddle.Margin = new System.Windows.Forms.Padding(0);
            this.grdMiddle.Name = "grdMiddle";
            this.grdMiddle.Padding = new System.Windows.Forms.Padding(3);
            this.grdMiddle.ShowBorder = true;
            this.grdMiddle.Size = new System.Drawing.Size(471, 132);
            this.grdMiddle.TabIndex = 2;
            this.grdMiddle.UseAutoBestFitColumns = false;
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.sptMain);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 0);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(475, 401);
            this.pnlGrid.TabIndex = 3;
            // 
            // MeasuringInstCalibrationPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "MeasuringInstCalibrationPlan";
            this.Text = "MeasuringInstCalibrationPlan";
            this.Load += new System.EventHandler(this.MeasuringInstCalibrationPlan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.sptMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlGrid)).EndInit();
            this.pnlGrid.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdCalibration;
        private Framework.SmartControls.SmartSplitTableLayoutPanel sptMain;
        private Framework.SmartControls.SmartBandedGrid grdRNR;
        private Framework.SmartControls.SmartBandedGrid grdMiddle;
        private Framework.SmartControls.SmartPanel pnlGrid;
    }
}