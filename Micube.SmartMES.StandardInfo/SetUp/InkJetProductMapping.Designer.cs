namespace Micube.SmartMES.StandardInfo
{
    partial class InkJetProductMapping
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
            this.grdInkJetMapping = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 551);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(1007, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdInkJetMapping);
            this.pnlContent.Size = new System.Drawing.Size(1007, 555);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1312, 584);
            // 
            // grdInkJetMapping
            // 
            this.grdInkJetMapping.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdInkJetMapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInkJetMapping.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInkJetMapping.IsUsePaging = false;
            this.grdInkJetMapping.LanguageKey = "INKJETPRODUCTMAP";
            this.grdInkJetMapping.Location = new System.Drawing.Point(0, 0);
            this.grdInkJetMapping.Margin = new System.Windows.Forms.Padding(0);
            this.grdInkJetMapping.Name = "grdInkJetMapping";
            this.grdInkJetMapping.ShowBorder = true;
            this.grdInkJetMapping.Size = new System.Drawing.Size(1007, 555);
            this.grdInkJetMapping.TabIndex = 0;
            // 
            // InkJetProductMapping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1332, 604);
            this.Name = "InkJetProductMapping";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdInkJetMapping;
    }
}