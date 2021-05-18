namespace Micube.SmartMES.QualityAnalysis
{
    partial class DefectStatusByProcessEquipment
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
            this.pnlGridMain = new Micube.Framework.SmartControls.SmartPanel();
            this.splitGridMain = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdTopInfo = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.splitGridBody = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdProcessInfo = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdAreaEqpInfo = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridMain)).BeginInit();
            this.pnlGridMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGridMain)).BeginInit();
            this.splitGridMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGridBody)).BeginInit();
            this.splitGridBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(371, 593);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(728, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.pnlGridMain);
            this.pnlContent.Size = new System.Drawing.Size(728, 596);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1109, 632);
            // 
            // pnlGridMain
            // 
            this.pnlGridMain.Controls.Add(this.splitGridMain);
            this.pnlGridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridMain.Location = new System.Drawing.Point(0, 0);
            this.pnlGridMain.Name = "pnlGridMain";
            this.pnlGridMain.Size = new System.Drawing.Size(728, 596);
            this.pnlGridMain.TabIndex = 5;
            // 
            // splitGridMain
            // 
            this.splitGridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitGridMain.Horizontal = false;
            this.splitGridMain.Location = new System.Drawing.Point(2, 2);
            this.splitGridMain.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.splitGridMain.Name = "splitGridMain";
            this.splitGridMain.Panel1.Controls.Add(this.grdTopInfo);
            this.splitGridMain.Panel1.Text = "Panel1";
            this.splitGridMain.Panel2.Controls.Add(this.splitGridBody);
            this.splitGridMain.Panel2.Text = "Panel2";
            this.splitGridMain.Size = new System.Drawing.Size(724, 592);
            this.splitGridMain.SplitterPosition = 80;
            this.splitGridMain.TabIndex = 0;
            // 
            // grdTopInfo
            // 
            this.grdTopInfo.Caption = "";
            this.grdTopInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTopInfo.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdTopInfo.IsUsePaging = false;
            this.grdTopInfo.LanguageKey = null;
            this.grdTopInfo.Location = new System.Drawing.Point(0, 0);
            this.grdTopInfo.Margin = new System.Windows.Forms.Padding(0);
            this.grdTopInfo.Name = "grdTopInfo";
            this.grdTopInfo.ShowBorder = true;
            this.grdTopInfo.ShowButtonBar = false;
            this.grdTopInfo.ShowStatusBar = false;
            this.grdTopInfo.Size = new System.Drawing.Size(724, 80);
            this.grdTopInfo.TabIndex = 0;
            this.grdTopInfo.UseAutoBestFitColumns = false;
            // 
            // splitGridBody
            // 
            this.splitGridBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitGridBody.Location = new System.Drawing.Point(0, 0);
            this.splitGridBody.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.splitGridBody.Name = "splitGridBody";
            this.splitGridBody.Panel1.Controls.Add(this.grdProcessInfo);
            this.splitGridBody.Panel1.Text = "Panel1";
            this.splitGridBody.Panel2.Controls.Add(this.grdAreaEqpInfo);
            this.splitGridBody.Panel2.Text = "Panel2";
            this.splitGridBody.Size = new System.Drawing.Size(724, 506);
            this.splitGridBody.SplitterPosition = 557;
            this.splitGridBody.TabIndex = 0;
            this.splitGridBody.Text = "smartSpliterContainer1";
            // 
            // grdProcessInfo
            // 
            this.grdProcessInfo.Caption = "";
            this.grdProcessInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProcessInfo.IsUsePaging = false;
            this.grdProcessInfo.LanguageKey = null;
            this.grdProcessInfo.Location = new System.Drawing.Point(0, 0);
            this.grdProcessInfo.Margin = new System.Windows.Forms.Padding(0);
            this.grdProcessInfo.Name = "grdProcessInfo";
            this.grdProcessInfo.ShowBorder = true;
            this.grdProcessInfo.Size = new System.Drawing.Size(557, 506);
            this.grdProcessInfo.TabIndex = 0;
            this.grdProcessInfo.UseAutoBestFitColumns = false;
            // 
            // grdAreaEqpInfo
            // 
            this.grdAreaEqpInfo.Caption = "";
            this.grdAreaEqpInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAreaEqpInfo.IsUsePaging = false;
            this.grdAreaEqpInfo.LanguageKey = null;
            this.grdAreaEqpInfo.Location = new System.Drawing.Point(0, 0);
            this.grdAreaEqpInfo.Margin = new System.Windows.Forms.Padding(0);
            this.grdAreaEqpInfo.Name = "grdAreaEqpInfo";
            this.grdAreaEqpInfo.ShowBorder = true;
            this.grdAreaEqpInfo.Size = new System.Drawing.Size(161, 506);
            this.grdAreaEqpInfo.TabIndex = 1;
            this.grdAreaEqpInfo.UseAutoBestFitColumns = false;
            // 
            // DefectStatusByProcessEquipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 662);
            this.Name = "DefectStatusByProcessEquipment";
            this.Text = "Yield & Defect Status by Product Item";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridMain)).EndInit();
            this.pnlGridMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitGridMain)).EndInit();
            this.splitGridMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitGridBody)).EndInit();
            this.splitGridBody.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartPanel pnlGridMain;
        private Framework.SmartControls.SmartSpliterContainer splitGridMain;
        private Framework.SmartControls.SmartBandedGrid grdTopInfo;
        private Framework.SmartControls.SmartSpliterContainer splitGridBody;
        private Framework.SmartControls.SmartBandedGrid grdProcessInfo;
        private Framework.SmartControls.SmartBandedGrid grdAreaEqpInfo;
    }
}