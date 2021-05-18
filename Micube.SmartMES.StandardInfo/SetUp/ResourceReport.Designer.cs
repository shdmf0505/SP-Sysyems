namespace Micube.SmartMES.StandardInfo
{
    partial class ResourceReport
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
			this.grdResource = new Micube.Framework.SmartControls.SmartBandedGrid();
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
			this.pnlContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlCondition
			// 
			this.pnlCondition.Location = new System.Drawing.Point(2, 31);
			this.pnlCondition.Size = new System.Drawing.Size(296, 512);
			// 
			// pnlToolbar
			// 
			this.pnlToolbar.Size = new System.Drawing.Size(742, 24);
			// 
			// pnlContent
			// 
			this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
			this.pnlContent.Appearance.Options.UseBackColor = true;
			this.pnlContent.Controls.Add(this.grdResource);
			this.pnlContent.Size = new System.Drawing.Size(742, 516);
			// 
			// pnlMain
			// 
			this.pnlMain.Size = new System.Drawing.Size(1047, 545);
			// 
			// grdResource
			// 
			this.grdResource.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdResource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdResource.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
			this.grdResource.IsUsePaging = false;
			this.grdResource.LanguageKey = "RESOURCEINFOLIST";
			this.grdResource.Location = new System.Drawing.Point(0, 0);
			this.grdResource.Margin = new System.Windows.Forms.Padding(0);
			this.grdResource.Name = "grdResource";
			this.grdResource.ShowBorder = true;
			this.grdResource.Size = new System.Drawing.Size(742, 516);
			this.grdResource.TabIndex = 0;
			// 
			// ResourceReport
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1067, 565);
			this.Name = "ResourceReport";
			this.Text = "SmartConditionBaseForm";
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
			this.pnlContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdResource;
    }
}