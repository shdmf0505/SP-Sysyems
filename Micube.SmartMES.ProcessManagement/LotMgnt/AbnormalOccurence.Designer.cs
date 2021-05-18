namespace Micube.SmartMES.ProcessManagement
{
    partial class AbnormalOccurence
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
			this.spcSpliter = new Micube.Framework.SmartControls.SmartSpliterControl();
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
			this.pnlContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlCondition
			// 
			this.pnlCondition.Location = new System.Drawing.Point(2, 31);
			this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
			this.pnlCondition.Size = new System.Drawing.Size(296, 485);
			// 
			// pnlToolbar
			// 
			this.pnlToolbar.Size = new System.Drawing.Size(756, 24);
			// 
			// pnlContent
			// 
			this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
			this.pnlContent.Appearance.Options.UseBackColor = true;
			this.pnlContent.Controls.Add(this.grdMain);
			this.pnlContent.Controls.Add(this.spcSpliter);
			this.pnlContent.Size = new System.Drawing.Size(756, 489);
			// 
			// pnlMain
			// 
			this.pnlMain.Size = new System.Drawing.Size(1061, 518);
			// 
			// grdMain
			// 
			this.grdMain.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdMain.IsUsePaging = false;
			this.grdMain.LanguageKey = "ABNORMALOCCURRENCESEARCH";
			this.grdMain.Location = new System.Drawing.Point(0, 0);
			this.grdMain.Margin = new System.Windows.Forms.Padding(0);
			this.grdMain.Name = "grdMain";
			this.grdMain.ShowBorder = true;
			this.grdMain.Size = new System.Drawing.Size(756, 484);
			this.grdMain.TabIndex = 1;
			// 
			// spcSpliter
			// 
			this.spcSpliter.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.spcSpliter.Location = new System.Drawing.Point(0, 484);
			this.spcSpliter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.spcSpliter.Name = "spcSpliter";
			this.spcSpliter.Size = new System.Drawing.Size(756, 5);
			this.spcSpliter.TabIndex = 5;
			this.spcSpliter.TabStop = false;
			// 
			// AbnormalOccurence
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1081, 538);
			this.LanguageKey = "ABNORMALOCCURRENCESEARCH";
			this.Name = "AbnormalOccurence";
			this.Text = "이상발생 현황";
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
			this.pnlContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdMain;
        private Framework.SmartControls.SmartSpliterControl spcSpliter;
    }
}