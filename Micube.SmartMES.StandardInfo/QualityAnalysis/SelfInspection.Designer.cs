namespace Micube.SmartMES.StandardInfo
{
    partial class SelfInspection
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
            this.grdSelfInspection = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.btnPoint = new Micube.Framework.SmartControls.SmartButton();
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
            this.pnlToolbar.Controls.Add(this.btnPoint);
            this.pnlToolbar.Size = new System.Drawing.Size(381, 30);
            this.pnlToolbar.Controls.SetChildIndex(this.btnPoint, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdSelfInspection);
            this.pnlContent.Size = new System.Drawing.Size(381, 376);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(762, 412);
            // 
            // grdSelfInspection
            // 
            this.grdSelfInspection.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdSelfInspection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSelfInspection.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdSelfInspection.IsUsePaging = false;
            this.grdSelfInspection.LanguageKey = "SELFINSPECTION";
            this.grdSelfInspection.Location = new System.Drawing.Point(0, 0);
            this.grdSelfInspection.Margin = new System.Windows.Forms.Padding(0);
            this.grdSelfInspection.Name = "grdSelfInspection";
            this.grdSelfInspection.ShowBorder = true;
            this.grdSelfInspection.Size = new System.Drawing.Size(381, 376);
            this.grdSelfInspection.TabIndex = 0;
            // 
            // btnPoint
            // 
            this.btnPoint.AllowFocus = false;
            this.btnPoint.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPoint.IsBusy = false;
            this.btnPoint.IsWrite = false;
            this.btnPoint.LanguageKey = "QCPOINT";
            this.btnPoint.Location = new System.Drawing.Point(284, 0);
            this.btnPoint.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPoint.Name = "btnPoint";
            this.btnPoint.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPoint.Size = new System.Drawing.Size(97, 30);
            this.btnPoint.TabIndex = 7;
            this.btnPoint.Text = "검사Point";
            this.btnPoint.TooltipLanguageKey = "";
            this.btnPoint.Visible = false;
            // 
            // SelfInspection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "SelfInspection";
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

        private Framework.SmartControls.SmartBandedGrid grdSelfInspection;
        private Framework.SmartControls.SmartButton btnPoint;
    }
}