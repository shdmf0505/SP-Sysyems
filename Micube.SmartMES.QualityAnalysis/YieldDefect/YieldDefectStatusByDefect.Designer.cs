namespace Micube.SmartMES.QualityAnalysis
{
    partial class YieldDefectStatusByDefect
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
            this.gridYieldDefectStatus = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.pnlTop = new Micube.Framework.SmartControls.SmartPanel();
            this.btnByItem = new Micube.Framework.SmartControls.SmartButton();
            this.btnByType = new Micube.Framework.SmartControls.SmartButton();
            this.pnlBody = new Micube.Framework.SmartControls.SmartPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBody)).BeginInit();
            this.pnlBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 609);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(814, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.pnlBody);
            this.pnlContent.Controls.Add(this.pnlTop);
            this.pnlContent.Size = new System.Drawing.Size(814, 613);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1119, 642);
            // 
            // gridYieldDefectStatus
            // 
            this.gridYieldDefectStatus.Caption = "";
            this.gridYieldDefectStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridYieldDefectStatus.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.Export;
            this.gridYieldDefectStatus.IsUsePaging = false;
            this.gridYieldDefectStatus.LanguageKey = "DEFECTSTATUSBYDEFECT";
            this.gridYieldDefectStatus.Location = new System.Drawing.Point(2, 2);
            this.gridYieldDefectStatus.Margin = new System.Windows.Forms.Padding(0);
            this.gridYieldDefectStatus.Name = "gridYieldDefectStatus";
            this.gridYieldDefectStatus.ShowBorder = true;
            this.gridYieldDefectStatus.ShowStatusBar = false;
            this.gridYieldDefectStatus.Size = new System.Drawing.Size(810, 574);
            this.gridYieldDefectStatus.TabIndex = 1;
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.btnByItem);
            this.pnlTop.Controls.Add(this.btnByType);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(814, 35);
            this.pnlTop.TabIndex = 2;
            // 
            // btnByItem
            // 
            this.btnByItem.AllowFocus = false;
            this.btnByItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnByItem.IsBusy = false;
            this.btnByItem.IsWrite = false;
            this.btnByItem.LanguageKey = "TABCURRENTBYITEM";
            this.btnByItem.Location = new System.Drawing.Point(698, 5);
            this.btnByItem.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnByItem.Name = "btnByItem";
            this.btnByItem.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnByItem.Size = new System.Drawing.Size(105, 25);
            this.btnByItem.TabIndex = 1;
            this.btnByItem.Text = "품목별 현황";
            this.btnByItem.TooltipLanguageKey = "";
            this.btnByItem.Click += new System.EventHandler(this.btnByItem_Click);
            // 
            // btnByType
            // 
            this.btnByType.AllowFocus = false;
            this.btnByType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnByType.IsBusy = false;
            this.btnByType.IsWrite = false;
            this.btnByType.LanguageKey = "STATUSBYTYPE";
            this.btnByType.Location = new System.Drawing.Point(576, 5);
            this.btnByType.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnByType.Name = "btnByType";
            this.btnByType.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnByType.Size = new System.Drawing.Size(96, 25);
            this.btnByType.TabIndex = 0;
            this.btnByType.Text = "타입별 현황";
            this.btnByType.TooltipLanguageKey = "";
            this.btnByType.Click += new System.EventHandler(this.btnByType_Click);
            // 
            // pnlBody
            // 
            this.pnlBody.Controls.Add(this.gridYieldDefectStatus);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 35);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(814, 578);
            this.pnlBody.TabIndex = 3;
            // 
            // YieldDefectStatusByDefect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 662);
            this.Name = "YieldDefectStatusByDefect";
            this.Text = "Yield & Defect Status by Product Item";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlBody)).EndInit();
            this.pnlBody.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid gridYieldDefectStatus;
        private Framework.SmartControls.SmartPanel pnlTop;
        private Framework.SmartControls.SmartButton btnByItem;
        private Framework.SmartControls.SmartButton btnByType;
        private Framework.SmartControls.SmartPanel pnlBody;
    }
}