namespace Micube.SmartMES.ProcessManagement
{
    partial class LotWindowTime
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
			this.toolTipController = new DevExpress.Utils.ToolTipController(this.components);
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
			this.pnlContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlCondition
			// 
			this.pnlCondition.Location = new System.Drawing.Point(2, 31);
			this.pnlCondition.Size = new System.Drawing.Size(296, 379);
			// 
			// pnlToolbar
			// 
			this.pnlToolbar.Size = new System.Drawing.Size(457, 24);
			// 
			// pnlContent
			// 
			this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
			this.pnlContent.Appearance.Options.UseBackColor = true;
			this.pnlContent.Controls.Add(this.grdMain);
			this.pnlContent.Size = new System.Drawing.Size(457, 383);
			// 
			// pnlMain
			// 
			this.pnlMain.Location = new System.Drawing.Point(19, 19);
			this.pnlMain.Size = new System.Drawing.Size(762, 412);
			// 
			// grdTimeLotList
			// 
			this.grdMain.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdMain.IsUsePaging = false;
			this.grdMain.LanguageKey = "WINDOWTIMELOTSEARCH";
			this.grdMain.Location = new System.Drawing.Point(0, 0);
			this.grdMain.Margin = new System.Windows.Forms.Padding(0);
			this.grdMain.Name = "grdTimeLotList";
			this.grdMain.ShowBorder = true;
			this.grdMain.Size = new System.Drawing.Size(457, 383);
			this.grdMain.TabIndex = 0;
			// 
			// LotWindowTime
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Name = "LotWindowTime";
			this.Padding = new System.Windows.Forms.Padding(19);
			this.Text = "SmartConditionBaseForm";
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
			this.pnlContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdMain;
		private DevExpress.Utils.ToolTipController toolTipController;
	}
}