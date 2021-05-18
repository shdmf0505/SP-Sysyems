namespace Micube.SmartMES.QualityAnalysis
{
    partial class Inspection
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
            this.grdInspection = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnDelete = new Micube.Framework.SmartControls.SmartButton();
            this.btnRegistartion = new Micube.Framework.SmartControls.SmartButton();
            this.btnPrintLabel = new Micube.Framework.SmartControls.SmartButton();
            this.btnFlag = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 397);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.flowLayoutPanel2);
            this.pnlToolbar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.pnlToolbar.Controls.SetChildIndex(this.flowLayoutPanel2, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdInspection);
            // 
            // grdInspection
            // 
            this.grdInspection.Caption = "검사원 관리현황";
            this.grdInspection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInspection.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInspection.IsUsePaging = false;
            this.grdInspection.LanguageKey = "INSPECTIONMANAGEMENT";
            this.grdInspection.Location = new System.Drawing.Point(0, 0);
            this.grdInspection.Margin = new System.Windows.Forms.Padding(0);
            this.grdInspection.Name = "grdInspection";
            this.grdInspection.ShowBorder = true;
            this.grdInspection.Size = new System.Drawing.Size(475, 401);
            this.grdInspection.TabIndex = 0;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btnDelete);
            this.flowLayoutPanel2.Controls.Add(this.btnRegistartion);
            this.flowLayoutPanel2.Controls.Add(this.btnPrintLabel);
            this.flowLayoutPanel2.Controls.Add(this.btnFlag);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(47, 0);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(428, 24);
            this.flowLayoutPanel2.TabIndex = 5;
            // 
            // btnDelete
            // 
            this.btnDelete.AllowFocus = false;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDelete.IsBusy = false;
            this.btnDelete.IsWrite = false;
            this.btnDelete.LanguageKey = "DELETE";
            this.btnDelete.Location = new System.Drawing.Point(348, 0);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnDelete.Size = new System.Drawing.Size(80, 24);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.Text = "삭제";
            this.btnDelete.TooltipLanguageKey = "";
            this.btnDelete.Visible = false;
            // 
            // btnRegistartion
            // 
            this.btnRegistartion.AllowFocus = false;
            this.btnRegistartion.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRegistartion.IsBusy = false;
            this.btnRegistartion.IsWrite = false;
            this.btnRegistartion.LanguageKey = "REGISTRATION";
            this.btnRegistartion.Location = new System.Drawing.Point(262, 0);
            this.btnRegistartion.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnRegistartion.Name = "btnRegistartion";
            this.btnRegistartion.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnRegistartion.Size = new System.Drawing.Size(80, 24);
            this.btnRegistartion.TabIndex = 1;
            this.btnRegistartion.Text = "등록";
            this.btnRegistartion.TooltipLanguageKey = "";
            this.btnRegistartion.Visible = false;
            // 
            // btnPrintLabel
            // 
            this.btnPrintLabel.AllowFocus = false;
            this.btnPrintLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPrintLabel.IsBusy = false;
            this.btnPrintLabel.IsWrite = false;
            this.btnPrintLabel.LanguageKey = "PRINTLABEL";
            this.btnPrintLabel.Location = new System.Drawing.Point(176, 0);
            this.btnPrintLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPrintLabel.Name = "btnPrintLabel";
            this.btnPrintLabel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPrintLabel.Size = new System.Drawing.Size(80, 24);
            this.btnPrintLabel.TabIndex = 2;
            this.btnPrintLabel.Text = "라벨출력";
            this.btnPrintLabel.TooltipLanguageKey = "";
            this.btnPrintLabel.Visible = false;
            // 
            // btnFlag
            // 
            this.btnFlag.AllowFocus = false;
            this.btnFlag.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnFlag.IsBusy = false;
            this.btnFlag.IsWrite = true;
            this.btnFlag.Location = new System.Drawing.Point(90, 0);
            this.btnFlag.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnFlag.Name = "btnFlag";
            this.btnFlag.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnFlag.Size = new System.Drawing.Size(80, 24);
            this.btnFlag.TabIndex = 3;
            this.btnFlag.Text = "Flag";
            this.btnFlag.TooltipLanguageKey = "";
            this.btnFlag.Visible = false;
            // 
            // Inspection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "Inspection";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdInspection;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Framework.SmartControls.SmartButton btnDelete;
        private Framework.SmartControls.SmartButton btnRegistartion;
        private Framework.SmartControls.SmartButton btnPrintLabel;
        private Framework.SmartControls.SmartButton btnFlag;
    }
}