namespace Micube.SmartMES.ToolManagement
{
    partial class RegistSendFilm
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
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdFilmRequest = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel4 = new Micube.Framework.SmartControls.SmartPanel();
            this.lblInputTitle = new Micube.Framework.SmartControls.SmartLabel();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            this.btnPrintLabel = new Micube.Framework.SmartControls.SmartButton();
            this.grdLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel4)).BeginInit();
            this.smartPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 653);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(940, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer1);
            this.pnlContent.Size = new System.Drawing.Size(940, 657);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1245, 686);
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.smartSpliterContainer1.Horizontal = false;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdFilmRequest);
            this.smartSpliterContainer1.Panel1.Controls.Add(this.smartPanel4);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdLotList);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(940, 657);
            this.smartSpliterContainer1.SplitterPosition = 283;
            this.smartSpliterContainer1.TabIndex = 2;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdFilmRequest
            // 
            this.grdFilmRequest.Caption = "필름발행출고목록:";
            this.grdFilmRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFilmRequest.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdFilmRequest.IsUsePaging = false;
            this.grdFilmRequest.LanguageKey = "FILMREGISTSENDLIST";
            this.grdFilmRequest.Location = new System.Drawing.Point(0, 34);
            this.grdFilmRequest.Margin = new System.Windows.Forms.Padding(0);
            this.grdFilmRequest.Name = "grdFilmRequest";
            this.grdFilmRequest.ShowBorder = true;
            this.grdFilmRequest.Size = new System.Drawing.Size(940, 335);
            this.grdFilmRequest.TabIndex = 1291;
            this.grdFilmRequest.UseAutoBestFitColumns = false;
            // 
            // smartPanel4
            // 
            this.smartPanel4.Controls.Add(this.lblInputTitle);
            this.smartPanel4.Controls.Add(this.btnSave);
            this.smartPanel4.Controls.Add(this.btnPrintLabel);
            this.smartPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartPanel4.Location = new System.Drawing.Point(0, 0);
            this.smartPanel4.Name = "smartPanel4";
            this.smartPanel4.Size = new System.Drawing.Size(940, 34);
            this.smartPanel4.TabIndex = 4;
            this.smartPanel4.Visible = false;
            // 
            // lblInputTitle
            // 
            this.lblInputTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblInputTitle.Appearance.Options.UseFont = true;
            this.lblInputTitle.LanguageKey = "FILMREGISTSEND";
            this.lblInputTitle.Location = new System.Drawing.Point(5, 5);
            this.lblInputTitle.Name = "lblInputTitle";
            this.lblInputTitle.Size = new System.Drawing.Size(118, 19);
            this.lblInputTitle.TabIndex = 1130;
            this.lblInputTitle.Text = "필름발행출고등록:";
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.LanguageKey = "TOOLPUBLISHBTN";
            this.btnSave.Location = new System.Drawing.Point(855, 5);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 127;
            this.btnSave.Text = "발행/출고:";
            this.btnSave.TooltipLanguageKey = "";
            this.btnSave.Visible = false;
            // 
            // btnPrintLabel
            // 
            this.btnPrintLabel.AllowFocus = false;
            this.btnPrintLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintLabel.IsBusy = false;
            this.btnPrintLabel.IsWrite = false;
            this.btnPrintLabel.LanguageKey = "PRINTLABEL";
            this.btnPrintLabel.Location = new System.Drawing.Point(769, 5);
            this.btnPrintLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPrintLabel.Name = "btnPrintLabel";
            this.btnPrintLabel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPrintLabel.Size = new System.Drawing.Size(80, 25);
            this.btnPrintLabel.TabIndex = 126;
            this.btnPrintLabel.Text = "라벨인쇄:";
            this.btnPrintLabel.TooltipLanguageKey = "";
            this.btnPrintLabel.Visible = false;
            // 
            // grdLotList
            // 
            this.grdLotList.Caption = "필름LOT내역:";
            this.grdLotList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLotList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLotList.IsUsePaging = false;
            this.grdLotList.LanguageKey = "FILMLOTSTATUS";
            this.grdLotList.Location = new System.Drawing.Point(0, 0);
            this.grdLotList.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotList.Name = "grdLotList";
            this.grdLotList.ShowBorder = true;
            this.grdLotList.Size = new System.Drawing.Size(940, 283);
            this.grdLotList.TabIndex = 1291;
            this.grdLotList.UseAutoBestFitColumns = false;
            // 
            // RegistSendFilm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1265, 706);
            this.Name = "RegistSendFilm";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel4)).EndInit();
            this.smartPanel4.ResumeLayout(false);
            this.smartPanel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdFilmRequest;
        private Framework.SmartControls.SmartPanel smartPanel4;
        private Framework.SmartControls.SmartLabel lblInputTitle;
        private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartButton btnPrintLabel;
        private Framework.SmartControls.SmartBandedGrid grdLotList;
    }
}