namespace Micube.SmartMES.SystemManagement
{
    partial class DongDoGold
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
            this.smartUserControl1 = new Micube.Framework.SmartControls.Forms.SmartUserControl();
            this.grdmeasure = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 397);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdmeasure);
            this.pnlContent.Controls.Add(this.smartUserControl1);
            // 
            // smartUserControl1
            // 
            this.smartUserControl1.Location = new System.Drawing.Point(291, 147);
            this.smartUserControl1.Name = "smartUserControl1";
            this.smartUserControl1.Size = new System.Drawing.Size(150, 150);
            this.smartUserControl1.TabIndex = 0;
            // 
            // grdmeasure
            // 
            this.grdmeasure.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdmeasure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdmeasure.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdmeasure.IsUsePaging = false;
            this.grdmeasure.LanguageKey = "INTERFACELIST";
            this.grdmeasure.Location = new System.Drawing.Point(0, 0);
            this.grdmeasure.Margin = new System.Windows.Forms.Padding(0);
            this.grdmeasure.Name = "grdmeasure";
            this.grdmeasure.ShowBorder = true;
            this.grdmeasure.Size = new System.Drawing.Size(475, 401);
            this.grdmeasure.TabIndex = 1;
            this.grdmeasure.UseAutoBestFitColumns = false;
            // 
            // DongDoGold
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "DongDoGold";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.Forms.SmartUserControl smartUserControl1;
        private Framework.SmartControls.SmartBandedGrid grdmeasure;
    }
}