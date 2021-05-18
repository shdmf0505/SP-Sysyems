﻿namespace Micube.SmartMES.ProcessManagement
{
    partial class LotHoldResult
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
            this.grdLotHoldResult = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
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
            this.pnlContent.Controls.Add(this.grdLotHoldResult);
            // 
            // grdLotHoldResult
            // 
            this.grdLotHoldResult.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLotHoldResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLotHoldResult.IsUsePaging = false;
            this.grdLotHoldResult.LanguageKey = null;
            this.grdLotHoldResult.Location = new System.Drawing.Point(0, 0);
            this.grdLotHoldResult.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotHoldResult.Name = "grdLotHoldResult";
            this.grdLotHoldResult.ShowBorder = true;
            this.grdLotHoldResult.Size = new System.Drawing.Size(475, 401);
            this.grdLotHoldResult.TabIndex = 0;
            this.grdLotHoldResult.LanguageKey = "LOTHOLDRESULT";
            // 
            // LotHoldResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "LotHoldResult";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdLotHoldResult;
    }
}