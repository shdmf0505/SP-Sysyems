namespace Micube.SmartMES.SystemManagement
{
    partial class ProcedureMng
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
            this.btn_excute = new Micube.Framework.SmartControls.SmartButton();
            this.gridList2 = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 397);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btn_excute);
            this.pnlToolbar.Controls.SetChildIndex(this.btn_excute, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.gridList2);
            // 
            // btn_excute
            // 
            this.btn_excute.AllowFocus = false;
            this.btn_excute.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_excute.IsBusy = false;
            this.btn_excute.IsWrite = false;
            this.btn_excute.LanguageKey = "EXECUTE";
            this.btn_excute.Location = new System.Drawing.Point(395, 0);
            this.btn_excute.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btn_excute.Name = "btn_excute";
            this.btn_excute.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btn_excute.Size = new System.Drawing.Size(80, 24);
            this.btn_excute.TabIndex = 5;
            this.btn_excute.Text = "실행";
            this.btn_excute.TooltipLanguageKey = "";
            // 
            // gridList2
            // 
            this.gridList2.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.gridList2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridList2.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.gridList2.IsUsePaging = false;
            this.gridList2.LanguageKey = null;
            this.gridList2.Location = new System.Drawing.Point(0, 0);
            this.gridList2.Margin = new System.Windows.Forms.Padding(0);
            this.gridList2.Name = "gridList2";
            this.gridList2.ShowBorder = true;
            this.gridList2.Size = new System.Drawing.Size(475, 401);
            this.gridList2.TabIndex = 0;
            this.gridList2.UseAutoBestFitColumns = false;
            // 
            // ProcedureMng
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "ProcedureMng";
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
        private Framework.SmartControls.SmartButton btn_excute;
        private Framework.SmartControls.SmartBandedGrid gridList2;
    }
}