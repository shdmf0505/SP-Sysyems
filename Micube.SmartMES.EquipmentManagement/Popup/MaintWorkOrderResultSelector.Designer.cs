namespace Micube.SmartMES.EquipmentManagement.Popup
{
    partial class MaintWorkOrderResultSelector
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
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.grdSelectList = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.grdSelectList);
            this.pnlMain.Controls.Add(this.smartPanel1);
            this.pnlMain.Size = new System.Drawing.Size(514, 490);
            // 
            // smartPanel1
            // 
            this.smartPanel1.Controls.Add(this.btnClose);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.smartPanel1.Location = new System.Drawing.Point(0, 454);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(514, 36);
            this.smartPanel1.TabIndex = 323;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.IsBusy = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(202, 5);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(106, 25);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "닫기:";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // grdSelectList
            // 
            this.grdSelectList.Caption = "2019-09-16 PM목록";
            this.grdSelectList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSelectList.IsUsePaging = false;
            this.grdSelectList.LanguageKey = "";
            this.grdSelectList.Location = new System.Drawing.Point(0, 0);
            this.grdSelectList.Margin = new System.Windows.Forms.Padding(0);
            this.grdSelectList.Name = "grdSelectList";
            this.grdSelectList.ShowBorder = true;
            this.grdSelectList.Size = new System.Drawing.Size(514, 454);
            this.grdSelectList.TabIndex = 324;
            // 
            // MaintWorkOrderResultSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 510);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MaintWorkOrderResultSelector";
            this.Text = "";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdSelectList;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartButton btnClose;
    }
}