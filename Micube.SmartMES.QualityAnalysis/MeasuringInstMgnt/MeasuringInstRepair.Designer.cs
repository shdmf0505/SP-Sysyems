namespace Micube.SmartMES.QualityAnalysis
{
    partial class MeasuringInstRepair
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
            this.pnlGroup = new Micube.Framework.SmartControls.SmartPanel();
            this.smartButton1 = new Micube.Framework.SmartControls.SmartButton();
            this.btnMailSend = new Micube.Framework.SmartControls.SmartButton();
            this.tblContainer = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.smartSpliterControl2 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.smartSpliterControl3 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.grdMain = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdOccurrence = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdSymptom = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdQATeamAction = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGroup)).BeginInit();
            this.pnlGroup.SuspendLayout();
            this.tblContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 397);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(742, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tblContainer);
            this.pnlContent.Controls.Add(this.pnlGroup);
            this.pnlContent.Size = new System.Drawing.Size(742, 401);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1047, 430);
            // 
            // pnlGroup
            // 
            this.pnlGroup.Controls.Add(this.smartButton1);
            this.pnlGroup.Controls.Add(this.btnMailSend);
            this.pnlGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGroup.Location = new System.Drawing.Point(0, 0);
            this.pnlGroup.Name = "pnlGroup";
            this.pnlGroup.Size = new System.Drawing.Size(742, 35);
            this.pnlGroup.TabIndex = 2;
            // 
            // smartButton1
            // 
            this.smartButton1.AllowFocus = false;
            this.smartButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.smartButton1.IsBusy = false;
            this.smartButton1.IsWrite = false;
            this.smartButton1.LanguageKey = "MEASURINGRIGHTSDOCUMENT";
            this.smartButton1.Location = new System.Drawing.Point(594, 6);
            this.smartButton1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.smartButton1.Name = "smartButton1";
            this.smartButton1.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.smartButton1.Size = new System.Drawing.Size(143, 23);
            this.smartButton1.TabIndex = 77;
            this.smartButton1.Text = "경위서";
            this.smartButton1.TooltipLanguageKey = "";
            // 
            // btnMailSend
            // 
            this.btnMailSend.AllowFocus = false;
            this.btnMailSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMailSend.IsBusy = false;
            this.btnMailSend.IsWrite = false;
            this.btnMailSend.LanguageKey = "SENDMAIL";
            this.btnMailSend.Location = new System.Drawing.Point(449, 6);
            this.btnMailSend.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnMailSend.Name = "btnMailSend";
            this.btnMailSend.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnMailSend.Size = new System.Drawing.Size(143, 23);
            this.btnMailSend.TabIndex = 3;
            this.btnMailSend.Text = "메일발송";
            this.btnMailSend.TooltipLanguageKey = "";
            // 
            // tblContainer
            // 
            this.tblContainer.ColumnCount = 5;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblContainer.Controls.Add(this.smartSpliterControl1, 0, 1);
            this.tblContainer.Controls.Add(this.smartSpliterControl2, 1, 2);
            this.tblContainer.Controls.Add(this.smartSpliterControl3, 3, 2);
            this.tblContainer.Controls.Add(this.grdMain, 0, 0);
            this.tblContainer.Controls.Add(this.grdOccurrence, 0, 2);
            this.tblContainer.Controls.Add(this.grdSymptom, 2, 2);
            this.tblContainer.Controls.Add(this.grdQATeamAction, 4, 2);
            this.tblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContainer.Location = new System.Drawing.Point(0, 35);
            this.tblContainer.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 3;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblContainer.Size = new System.Drawing.Size(742, 366);
            this.tblContainer.TabIndex = 3;
            // 
            // smartSpliterControl1
            // 
            this.tblContainer.SetColumnSpan(this.smartSpliterControl1, 5);
            this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartSpliterControl1.Location = new System.Drawing.Point(0, 178);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(742, 5);
            this.smartSpliterControl1.TabIndex = 0;
            this.smartSpliterControl1.TabStop = false;
            // 
            // smartSpliterControl2
            // 
            this.smartSpliterControl2.Location = new System.Drawing.Point(240, 188);
            this.smartSpliterControl2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl2.Name = "smartSpliterControl2";
            this.smartSpliterControl2.Size = new System.Drawing.Size(5, 178);
            this.smartSpliterControl2.TabIndex = 1;
            this.smartSpliterControl2.TabStop = false;
            // 
            // smartSpliterControl3
            // 
            this.smartSpliterControl3.Location = new System.Drawing.Point(490, 188);
            this.smartSpliterControl3.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl3.Name = "smartSpliterControl3";
            this.smartSpliterControl3.Size = new System.Drawing.Size(5, 178);
            this.smartSpliterControl3.TabIndex = 2;
            this.smartSpliterControl3.TabStop = false;
            // 
            // grdMain
            // 
            this.grdMain.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.tblContainer.SetColumnSpan(this.grdMain, 5);
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdMain.IsUsePaging = false;
            this.grdMain.LanguageKey = "MEASURINGREPAIRDISPOSALINFO";
            this.grdMain.Location = new System.Drawing.Point(0, 0);
            this.grdMain.Margin = new System.Windows.Forms.Padding(0);
            this.grdMain.Name = "grdMain";
            this.grdMain.ShowBorder = true;
            this.grdMain.Size = new System.Drawing.Size(742, 178);
            this.grdMain.TabIndex = 3;
            this.grdMain.UseAutoBestFitColumns = false;
            // 
            // grdOccurrence
            // 
            this.grdOccurrence.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdOccurrence.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOccurrence.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdOccurrence.IsUsePaging = false;
            this.grdOccurrence.LanguageKey = "MEASURINGOCCURRENCECONTENT";
            this.grdOccurrence.Location = new System.Drawing.Point(0, 188);
            this.grdOccurrence.Margin = new System.Windows.Forms.Padding(0);
            this.grdOccurrence.Name = "grdOccurrence";
            this.grdOccurrence.ShowBorder = true;
            this.grdOccurrence.Size = new System.Drawing.Size(240, 178);
            this.grdOccurrence.TabIndex = 4;
            this.grdOccurrence.UseAutoBestFitColumns = false;
            // 
            // grdSymptom
            // 
            this.grdSymptom.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdSymptom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSymptom.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdSymptom.IsUsePaging = false;
            this.grdSymptom.LanguageKey = "MEASURINGSYMPTOM";
            this.grdSymptom.Location = new System.Drawing.Point(250, 188);
            this.grdSymptom.Margin = new System.Windows.Forms.Padding(0);
            this.grdSymptom.Name = "grdSymptom";
            this.grdSymptom.ShowBorder = true;
            this.grdSymptom.Size = new System.Drawing.Size(240, 178);
            this.grdSymptom.TabIndex = 5;
            this.grdSymptom.UseAutoBestFitColumns = false;
            // 
            // grdQATeamAction
            // 
            this.grdQATeamAction.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdQATeamAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdQATeamAction.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdQATeamAction.IsUsePaging = false;
            this.grdQATeamAction.LanguageKey = "MEASURINGACTIONCONTENT";
            this.grdQATeamAction.Location = new System.Drawing.Point(500, 188);
            this.grdQATeamAction.Margin = new System.Windows.Forms.Padding(0);
            this.grdQATeamAction.Name = "grdQATeamAction";
            this.grdQATeamAction.ShowBorder = true;
            this.grdQATeamAction.Size = new System.Drawing.Size(242, 178);
            this.grdQATeamAction.TabIndex = 6;
            this.grdQATeamAction.UseAutoBestFitColumns = false;
            // 
            // MeasuringInstRepair
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 450);
            this.Name = "MeasuringInstRepair";
            this.Text = "MeasuringInstRepair";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGroup)).EndInit();
            this.pnlGroup.ResumeLayout(false);
            this.tblContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Framework.SmartControls.SmartPanel pnlGroup;
        private Framework.SmartControls.SmartButton btnMailSend;
        private Framework.SmartControls.SmartButton smartButton1;
        private Framework.SmartControls.SmartSplitTableLayoutPanel tblContainer;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl2;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl3;
        private Framework.SmartControls.SmartBandedGrid grdMain;
        private Framework.SmartControls.SmartBandedGrid grdOccurrence;
        private Framework.SmartControls.SmartBandedGrid grdSymptom;
        private Framework.SmartControls.SmartBandedGrid grdQATeamAction;
    }
}