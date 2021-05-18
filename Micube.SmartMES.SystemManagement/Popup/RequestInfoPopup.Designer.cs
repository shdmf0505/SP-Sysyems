namespace Micube.SmartMES.SystemManagement
{
    partial class RequestInfoPopup
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
            this.grdmenuinfo = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.btn_close = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(0, 36);
            this.pnlCondition.Size = new System.Drawing.Size(0, 0);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btn_close);
            this.pnlToolbar.Size = new System.Drawing.Size(762, 30);
            this.pnlToolbar.Controls.SetChildIndex(this.btn_close, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdmenuinfo);
            this.pnlContent.Size = new System.Drawing.Size(762, 376);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(762, 412);
            // 
            // grdmenuinfo
            // 
            this.grdmenuinfo.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdmenuinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdmenuinfo.IsUsePaging = false;
            this.grdmenuinfo.LanguageKey = "GRIDMENULIST";
            this.grdmenuinfo.Location = new System.Drawing.Point(0, 0);
            this.grdmenuinfo.Margin = new System.Windows.Forms.Padding(0);
            this.grdmenuinfo.Name = "grdmenuinfo";
            this.grdmenuinfo.ShowBorder = true;
            this.grdmenuinfo.Size = new System.Drawing.Size(762, 376);
            this.grdmenuinfo.TabIndex = 0;
            // 
            // btn_close
            // 
            this.btn_close.AllowFocus = false;
            this.btn_close.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_close.IsBusy = false;
            this.btn_close.Location = new System.Drawing.Point(606, 0);
            this.btn_close.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btn_close.Name = "btn_close";
            this.btn_close.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btn_close.Size = new System.Drawing.Size(156, 30);
            this.btn_close.TabIndex = 5;
            this.btn_close.Text = "닫기 버튼";
            this.btn_close.TooltipLanguageKey = "";
            // 
            // RequestInfoPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ConditionsVisible = false;
            this.Name = "RequestInfoPopup";
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

        private Framework.SmartControls.SmartBandedGrid grdmenuinfo;
        private Framework.SmartControls.SmartButton btn_close;
    }
}