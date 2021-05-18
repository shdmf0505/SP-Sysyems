namespace Micube.SmartMES.SystemManagement
{
    partial class UserClassCopy
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
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.grdUser = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdCopyUser = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdUserClass = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(371, 586);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(1063, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer1);
            this.pnlContent.Size = new System.Drawing.Size(1063, 589);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1444, 625);
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.splitContainerControl1);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdUserClass);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(1063, 589);
            this.smartSpliterContainer1.SplitterPosition = 516;
            this.smartSpliterContainer1.TabIndex = 0;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.grdUser);
            this.splitContainerControl1.Panel2.Controls.Add(this.grdCopyUser);
            this.splitContainerControl1.Size = new System.Drawing.Size(516, 589);
            this.splitContainerControl1.SplitterPosition = 327;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // grdUser
            // 
            this.grdUser.Caption = "";
            this.grdUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdUser.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdUser.IsUsePaging = false;
            this.grdUser.LanguageKey = "사용자";
            this.grdUser.Location = new System.Drawing.Point(0, 0);
            this.grdUser.Margin = new System.Windows.Forms.Padding(0);
            this.grdUser.Name = "grdUser";
            this.grdUser.ShowBorder = true;
            this.grdUser.Size = new System.Drawing.Size(516, 327);
            this.grdUser.TabIndex = 0;
            this.grdUser.UseAutoBestFitColumns = false;
            // 
            // grdCopyUser
            // 
            this.grdCopyUser.Caption = "";
            this.grdCopyUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCopyUser.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdCopyUser.IsUsePaging = false;
            this.grdCopyUser.LanguageKey = "복제 대상 사용자";
            this.grdCopyUser.Location = new System.Drawing.Point(0, 0);
            this.grdCopyUser.Margin = new System.Windows.Forms.Padding(0);
            this.grdCopyUser.Name = "grdCopyUser";
            this.grdCopyUser.ShowBorder = true;
            this.grdCopyUser.Size = new System.Drawing.Size(516, 256);
            this.grdCopyUser.TabIndex = 0;
            this.grdCopyUser.UseAutoBestFitColumns = false;
            // 
            // grdUserClass
            // 
            this.grdUserClass.Caption = "";
            this.grdUserClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdUserClass.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdUserClass.IsUsePaging = false;
            this.grdUserClass.LanguageKey = "사용자 그룹";
            this.grdUserClass.Location = new System.Drawing.Point(0, 0);
            this.grdUserClass.Margin = new System.Windows.Forms.Padding(0);
            this.grdUserClass.Name = "grdUserClass";
            this.grdUserClass.ShowBorder = true;
            this.grdUserClass.Size = new System.Drawing.Size(541, 589);
            this.grdUserClass.TabIndex = 0;
            this.grdUserClass.UseAutoBestFitColumns = false;
            // 
            // UserClassCopy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1482, 663);
            this.Name = "UserClassCopy";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdUserClass;
        private Framework.SmartControls.SmartBandedGrid grdUser;
        private Framework.SmartControls.SmartBandedGrid grdCopyUser;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
    }
}