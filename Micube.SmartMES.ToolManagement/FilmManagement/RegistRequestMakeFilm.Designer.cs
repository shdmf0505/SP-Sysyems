namespace Micube.SmartMES.ToolManagement
{
    partial class RegistRequestMakeFilm
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
            this.smartPanel4 = new Micube.Framework.SmartControls.SmartPanel();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            this.lblInputTitle = new Micube.Framework.SmartControls.SmartLabel();
            this.btnRegist = new Micube.Framework.SmartControls.SmartButton();
            this.btnReport = new Micube.Framework.SmartControls.SmartButton();
            this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
            this.grdMain = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel4)).BeginInit();
            this.smartPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 640);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(932, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdMain);
            this.pnlContent.Controls.Add(this.smartPanel4);
            this.pnlContent.Size = new System.Drawing.Size(932, 644);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1237, 673);
            // 
            // smartPanel4
            // 
            this.smartPanel4.Controls.Add(this.btnSave);
            this.smartPanel4.Controls.Add(this.lblInputTitle);
            this.smartPanel4.Controls.Add(this.btnRegist);
            this.smartPanel4.Controls.Add(this.btnReport);
            this.smartPanel4.Controls.Add(this.btnCancel);
            this.smartPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartPanel4.Location = new System.Drawing.Point(0, 0);
            this.smartPanel4.Name = "smartPanel4";
            this.smartPanel4.Size = new System.Drawing.Size(932, 34);
            this.smartPanel4.TabIndex = 5;
            this.smartPanel4.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(589, 5);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 1131;
            this.btnSave.Text = "저장:";
            this.btnSave.TooltipLanguageKey = "";
            this.btnSave.Visible = false;
            // 
            // lblInputTitle
            // 
            this.lblInputTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblInputTitle.Appearance.Options.UseFont = true;
            this.lblInputTitle.LanguageKey = "FILMREQUESTACCEPT";
            this.lblInputTitle.Location = new System.Drawing.Point(5, 5);
            this.lblInputTitle.Name = "lblInputTitle";
            this.lblInputTitle.Size = new System.Drawing.Size(118, 19);
            this.lblInputTitle.TabIndex = 1130;
            this.lblInputTitle.Text = "필름제작요청접수:";
            // 
            // btnRegist
            // 
            this.btnRegist.AllowFocus = false;
            this.btnRegist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegist.IsBusy = false;
            this.btnRegist.IsWrite = false;
            this.btnRegist.LanguageKey = "RECEIVE";
            this.btnRegist.Location = new System.Drawing.Point(675, 5);
            this.btnRegist.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnRegist.Name = "btnRegist";
            this.btnRegist.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnRegist.Size = new System.Drawing.Size(80, 25);
            this.btnRegist.TabIndex = 125;
            this.btnRegist.Text = "접수:";
            this.btnRegist.TooltipLanguageKey = "";
            this.btnRegist.Visible = false;
            // 
            // btnReport
            // 
            this.btnReport.AllowFocus = false;
            this.btnReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReport.IsBusy = false;
            this.btnReport.IsWrite = false;
            this.btnReport.LanguageKey = "REQUESTDOCUMENT";
            this.btnReport.Location = new System.Drawing.Point(847, 5);
            this.btnReport.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnReport.Name = "btnReport";
            this.btnReport.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnReport.Size = new System.Drawing.Size(80, 25);
            this.btnReport.TabIndex = 127;
            this.btnReport.Text = "의뢰서:";
            this.btnReport.TooltipLanguageKey = "";
            this.btnReport.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.IsBusy = false;
            this.btnCancel.IsWrite = false;
            this.btnCancel.LanguageKey = "CANCEL";
            this.btnCancel.Location = new System.Drawing.Point(761, 5);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 126;
            this.btnCancel.Text = "취소:";
            this.btnCancel.TooltipLanguageKey = "";
            this.btnCancel.Visible = false;
            // 
            // grdFilmRequest
            // 
            this.grdMain.Caption = "필름제작요청목록:";
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdMain.IsUsePaging = false;
            this.grdMain.LanguageKey = "FILMREQUESTACCEPTLIST";
            this.grdMain.Location = new System.Drawing.Point(0, 34);
            this.grdMain.Margin = new System.Windows.Forms.Padding(0);
            this.grdMain.Name = "grdFilmRequest";
            this.grdMain.ShowBorder = true;
            this.grdMain.Size = new System.Drawing.Size(932, 610);
            this.grdMain.TabIndex = 1291;
            this.grdMain.UseAutoBestFitColumns = false;
            // 
            // RegistRequestMakeFilm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 693);
            this.Name = "RegistRequestMakeFilm";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel4)).EndInit();
            this.smartPanel4.ResumeLayout(false);
            this.smartPanel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartPanel smartPanel4;
        private Framework.SmartControls.SmartLabel lblInputTitle;
        private Framework.SmartControls.SmartButton btnRegist;
        private Framework.SmartControls.SmartButton btnReport;
        private Framework.SmartControls.SmartButton btnCancel;
        private Framework.SmartControls.SmartBandedGrid grdMain;
        private Framework.SmartControls.SmartButton btnSave;
    }
}