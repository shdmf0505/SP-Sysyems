namespace Micube.SmartMES.ToolManagement
{
    partial class RegistScrapFilm
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
            this.lblInputTitle = new Micube.Framework.SmartControls.SmartLabel();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            this.grdFilmRequest = new Micube.Framework.SmartControls.SmartBandedGrid();
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
            this.pnlCondition.Size = new System.Drawing.Size(296, 582);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(856, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdFilmRequest);
            this.pnlContent.Controls.Add(this.smartPanel4);
            this.pnlContent.Size = new System.Drawing.Size(856, 586);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1161, 615);
            // 
            // smartPanel4
            // 
            this.smartPanel4.Controls.Add(this.lblInputTitle);
            this.smartPanel4.Controls.Add(this.btnSave);
            this.smartPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartPanel4.Location = new System.Drawing.Point(0, 0);
            this.smartPanel4.Name = "smartPanel4";
            this.smartPanel4.Size = new System.Drawing.Size(856, 34);
            this.smartPanel4.TabIndex = 6;
            this.smartPanel4.Visible = false;
            // 
            // lblInputTitle
            // 
            this.lblInputTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblInputTitle.Appearance.Options.UseFont = true;
            this.lblInputTitle.LanguageKey = "FILMREGISTSCRAP";
            this.lblInputTitle.Location = new System.Drawing.Point(5, 5);
            this.lblInputTitle.Name = "lblInputTitle";
            this.lblInputTitle.Size = new System.Drawing.Size(90, 19);
            this.lblInputTitle.TabIndex = 1130;
            this.lblInputTitle.Text = "필름폐기등록:";
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.LanguageKey = "SCRAP";
            this.btnSave.Location = new System.Drawing.Point(771, 5);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 127;
            this.btnSave.Text = "폐기:";
            this.btnSave.TooltipLanguageKey = "";
            this.btnSave.Visible = false;
            // 
            // grdFilmRequest
            // 
            this.grdFilmRequest.Caption = "필름LOT목록:";
            this.grdFilmRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFilmRequest.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdFilmRequest.IsUsePaging = false;
            this.grdFilmRequest.LanguageKey = "FILMLOTLIST";
            this.grdFilmRequest.Location = new System.Drawing.Point(0, 34);
            this.grdFilmRequest.Margin = new System.Windows.Forms.Padding(0);
            this.grdFilmRequest.Name = "grdFilmRequest";
            this.grdFilmRequest.ShowBorder = true;
            this.grdFilmRequest.Size = new System.Drawing.Size(856, 552);
            this.grdFilmRequest.TabIndex = 1292;
            this.grdFilmRequest.UseAutoBestFitColumns = false;
            // 
            // RegistScrapFilm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1181, 635);
            this.Name = "RegistScrapFilm";
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
        private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartBandedGrid grdFilmRequest;
    }
}