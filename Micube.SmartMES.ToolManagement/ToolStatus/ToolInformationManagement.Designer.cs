namespace Micube.SmartMES.ToolManagement
{
    partial class ToolInformationManagement
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
            this.grdToolInformationList = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 582);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(860, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdToolInformationList);
            this.pnlContent.Size = new System.Drawing.Size(860, 586);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1165, 615);
            // 
            // grdToolInformationList
            // 
            this.grdToolInformationList.Caption = "치공구현황:";
            this.grdToolInformationList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdToolInformationList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdToolInformationList.IsUsePaging = false;
            this.grdToolInformationList.LanguageKey = "TOOLINFORMATIONLIST";
            this.grdToolInformationList.Location = new System.Drawing.Point(0, 0);
            this.grdToolInformationList.Margin = new System.Windows.Forms.Padding(0);
            this.grdToolInformationList.Name = "grdToolInformationList";
            this.grdToolInformationList.ShowBorder = true;
            this.grdToolInformationList.ShowStatusBar = false;
            this.grdToolInformationList.Size = new System.Drawing.Size(860, 586);
            this.grdToolInformationList.TabIndex = 104;
            this.grdToolInformationList.UseAutoBestFitColumns = false;
            // 
            // ToolInformationManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1185, 635);
            this.Name = "ToolInformationManagement";
            this.Text = "ToolInformationManagement";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Framework.SmartControls.SmartBandedGrid grdToolInformationList;
    }
}