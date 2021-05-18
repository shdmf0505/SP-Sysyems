namespace Micube.SmartMES.StandardInfo
{
    partial class RegistActionManagement
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
            this.grdAction = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 704);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnSave);
            this.pnlToolbar.Size = new System.Drawing.Size(898, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.btnSave, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdAction);
            this.pnlContent.Size = new System.Drawing.Size(898, 708);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1203, 737);
            // 
            // grdAction
            // 
            this.grdAction.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAction.IsUsePaging = false;
            this.grdAction.LanguageKey = "Action";
            this.grdAction.Location = new System.Drawing.Point(0, 0);
            this.grdAction.Margin = new System.Windows.Forms.Padding(0);
            this.grdAction.Name = "grdAction";
            this.grdAction.ShowBorder = true;
            this.grdAction.Size = new System.Drawing.Size(898, 708);
            this.grdAction.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.LanguageKey = "Save";
            this.btnSave.Location = new System.Drawing.Point(818, 0);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(80, 24);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "smartButton2";
            this.btnSave.TooltipLanguageKey = "";
            // 
            // RegistActionManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1223, 757);
            this.Name = "RegistActionManagement";
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
        private Framework.SmartControls.SmartBandedGrid grdAction;
        private Framework.SmartControls.SmartButton btnSave;
    }
}