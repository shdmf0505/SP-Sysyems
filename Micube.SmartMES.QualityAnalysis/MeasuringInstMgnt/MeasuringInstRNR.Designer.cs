namespace Micube.SmartMES.QualityAnalysis
{
    partial class MeasuringInstRNR
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
            this.grdMain = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.sptMain = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.btnSaveSubData = new Micube.Framework.SmartControls.SmartButton();
            this.grdSub = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.sptMain.SuspendLayout();
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
            this.pnlContent.Controls.Add(this.sptMain);
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
            this.grdMain.LanguageKey = "SPECDEFINITIONLIST";
            this.grdMain.Location = new System.Drawing.Point(0, 0);
            this.grdMain.Margin = new System.Windows.Forms.Padding(0);
            this.grdMain.Name = "grdMain";
            this.grdMain.ShowBorder = true;
            this.grdMain.Size = new System.Drawing.Size(475, 216);
            this.grdMain.TabIndex = 0;
            this.grdMain.UseAutoBestFitColumns = false;
            // 
            // sptMain
            // 
            this.sptMain.ColumnCount = 1;
            this.sptMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.sptMain.Controls.Add(this.grdMain, 0, 0);
            this.sptMain.Controls.Add(this.btnSaveSubData, 0, 1);
            this.sptMain.Controls.Add(this.grdSub, 0, 3);
            this.sptMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sptMain.Location = new System.Drawing.Point(0, 0);
            this.sptMain.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.sptMain.Name = "sptMain";
            this.sptMain.RowCount = 4;
            this.sptMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 58.33333F));
            this.sptMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sptMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.sptMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 41.66667F));
            this.sptMain.Size = new System.Drawing.Size(475, 401);
            this.sptMain.TabIndex = 1;
            // 
            // btnSaveSubData
            // 
            this.btnSaveSubData.AllowFocus = false;
            this.btnSaveSubData.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSaveSubData.IsBusy = false;
            this.btnSaveSubData.IsWrite = false;
            this.btnSaveSubData.LanguageKey = "SAVE";
            this.btnSaveSubData.Location = new System.Drawing.Point(395, 216);
            this.btnSaveSubData.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnSaveSubData.Name = "btnSaveSubData";
            this.btnSaveSubData.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSaveSubData.Size = new System.Drawing.Size(80, 25);
            this.btnSaveSubData.TabIndex = 1;
            this.btnSaveSubData.Text = "smartButton1";
            this.btnSaveSubData.TooltipLanguageKey = "";
            // 
            // grdSub
            // 
            this.grdSub.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdSub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSub.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdSub.IsUsePaging = false;
            this.grdSub.LanguageKey = "MEASURINGISRNRTESTLIST";
            this.grdSub.Location = new System.Drawing.Point(0, 246);
            this.grdSub.Margin = new System.Windows.Forms.Padding(0);
            this.grdSub.Name = "grdSub";
            this.grdSub.ShowBorder = true;
            this.grdSub.Size = new System.Drawing.Size(475, 155);
            this.grdSub.TabIndex = 2;
            this.grdSub.UseAutoBestFitColumns = false;
            // 
            // MeasuringInstRNR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "MeasuringInstRNR";
            this.Text = "MeasuringInstRNR";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.sptMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdMain;
        private Framework.SmartControls.SmartSplitTableLayoutPanel sptMain;
        private Framework.SmartControls.SmartButton btnSaveSubData;
        private Framework.SmartControls.SmartBandedGrid grdSub;
    }
}