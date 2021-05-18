namespace Micube.SmartMES.OutsideOrderMgnt
{
    partial class OutsourcingYoungPongPriceClass
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
            this.grdSub = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdMain = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 906);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(919, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer1);
            this.pnlContent.Size = new System.Drawing.Size(919, 910);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1224, 939);
            // 
            // grdSub
            // 
            this.grdSub.Caption = "";
            this.grdSub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSub.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdSub.IsUsePaging = false;
            this.grdSub.LanguageKey = "";
            this.grdSub.Location = new System.Drawing.Point(0, 0);
            this.grdSub.Margin = new System.Windows.Forms.Padding(0);
            this.grdSub.Name = "grdSub";
            this.grdSub.ShowBorder = true;
            this.grdSub.Size = new System.Drawing.Size(244, 910);
            this.grdSub.TabIndex = 10;
            this.grdSub.UseAutoBestFitColumns = false;
            // 
            // grdMain
            // 
            this.grdMain.Caption = "";
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdMain.IsUsePaging = false;
            this.grdMain.LanguageKey = "";
            this.grdMain.Location = new System.Drawing.Point(0, 0);
            this.grdMain.Margin = new System.Windows.Forms.Padding(0);
            this.grdMain.Name = "grdMain";
            this.grdMain.ShowBorder = true;
            this.grdMain.Size = new System.Drawing.Size(670, 910);
            this.grdMain.TabIndex = 9;
            this.grdMain.UseAutoBestFitColumns = false;
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdMain);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdSub);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(919, 910);
            this.smartSpliterContainer1.SplitterPosition = 670;
            this.smartSpliterContainer1.TabIndex = 1;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // OutsourcingYoungPongPriceClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 977);
            this.Name = "OutsourcingYoungPongPriceClass";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Framework.SmartControls.SmartBandedGrid grdSub;
        private Framework.SmartControls.SmartBandedGrid grdMain;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
    }
}