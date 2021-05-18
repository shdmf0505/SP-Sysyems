namespace Micube.SmartMES.ProcessManagement
{
    partial class SampleRouting
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
            this.smartPanel2 = new System.Windows.Forms.Panel();
            this.smartTabControl1 = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgResource = new DevExpress.XtraTab.XtraTabPage();
            this.grdResource = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgBOM = new DevExpress.XtraTab.XtraTabPage();
            this.grdMaterial = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgDurable = new DevExpress.XtraTab.XtraTabPage();
            this.grdDurable = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.smartPanel3 = new Micube.Framework.SmartControls.SmartPanel();
            this.grdRouting = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel4 = new Micube.Framework.SmartControls.SmartPanel();
            this.selProcessSegment = new Micube.Framework.SmartControls.SmartLabelSelectPopupEdit();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.grdLotInfo = new Micube.SmartMES.Commons.Controls.SmartLotInfoGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.smartPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartTabControl1)).BeginInit();
            this.smartTabControl1.SuspendLayout();
            this.tpgResource.SuspendLayout();
            this.tpgBOM.SuspendLayout();
            this.tpgDurable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel3)).BeginInit();
            this.smartPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel4)).BeginInit();
            this.smartPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.selProcessSegment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 546);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(926, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartPanel2);
            this.pnlContent.Controls.Add(this.smartPanel1);
            this.pnlContent.Size = new System.Drawing.Size(926, 550);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1231, 579);
            // 
            // smartPanel2
            // 
            this.smartPanel2.Controls.Add(this.smartTabControl1);
            this.smartPanel2.Controls.Add(this.smartSpliterControl1);
            this.smartPanel2.Controls.Add(this.smartPanel3);
            this.smartPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel2.Location = new System.Drawing.Point(0, 105);
            this.smartPanel2.Name = "smartPanel2";
            this.smartPanel2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.smartPanel2.Size = new System.Drawing.Size(926, 445);
            this.smartPanel2.TabIndex = 4;
            // 
            // smartTabControl1
            // 
            this.smartTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartTabControl1.Location = new System.Drawing.Point(654, 5);
            this.smartTabControl1.Name = "smartTabControl1";
            this.smartTabControl1.SelectedTabPage = this.tpgResource;
            this.smartTabControl1.Size = new System.Drawing.Size(272, 440);
            this.smartTabControl1.TabIndex = 14;
            this.smartTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgResource,
            this.tpgBOM,
            this.tpgDurable});
            // 
            // tpgResource
            // 
            this.tpgResource.Controls.Add(this.grdResource);
            this.smartTabControl1.SetLanguageKey(this.tpgResource, "RESOURCE");
            this.tpgResource.Name = "tpgResource";
            this.tpgResource.Padding = new System.Windows.Forms.Padding(3);
            this.tpgResource.Size = new System.Drawing.Size(266, 411);
            this.tpgResource.Text = "Resource";
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
            this.grdResource.LanguageKey = "RESOURCE";
            this.grdResource.Location = new System.Drawing.Point(3, 3);
            this.grdResource.Margin = new System.Windows.Forms.Padding(0);
            this.grdResource.Name = "grdResource";
            this.grdResource.ShowBorder = true;
            this.grdResource.ShowStatusBar = false;
            this.grdResource.Size = new System.Drawing.Size(260, 405);
            this.grdResource.TabIndex = 3;
            this.grdResource.UseAutoBestFitColumns = false;
            // 
            // tpgBOM
            // 
            this.tpgBOM.Controls.Add(this.grdMaterial);
            this.smartTabControl1.SetLanguageKey(this.tpgBOM, "CONSUMABLE");
            this.tpgBOM.Name = "tpgBOM";
            this.tpgBOM.Padding = new System.Windows.Forms.Padding(3);
            this.tpgBOM.Size = new System.Drawing.Size(145, 350);
            this.tpgBOM.Text = "자재";
            // 
            // grdMaterial
            // 
            this.grdMaterial.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMaterial.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdMaterial.IsUsePaging = false;
            this.grdMaterial.LanguageKey = "CONSUMABLE";
            this.grdMaterial.Location = new System.Drawing.Point(3, 3);
            this.grdMaterial.Margin = new System.Windows.Forms.Padding(0);
            this.grdMaterial.Name = "grdMaterial";
            this.grdMaterial.ShowBorder = true;
            this.grdMaterial.ShowStatusBar = false;
            this.grdMaterial.Size = new System.Drawing.Size(139, 344);
            this.grdMaterial.TabIndex = 4;
            this.grdMaterial.UseAutoBestFitColumns = false;
            // 
            // tpgDurable
            // 
            this.tpgDurable.Controls.Add(this.grdDurable);
            this.smartTabControl1.SetLanguageKey(this.tpgDurable, "DURABLE");
            this.tpgDurable.Name = "tpgDurable";
            this.tpgDurable.Padding = new System.Windows.Forms.Padding(3);
            this.tpgDurable.Size = new System.Drawing.Size(145, 350);
            this.tpgDurable.Text = "치공구";
            // 
            // grdDurable
            // 
            this.grdDurable.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdDurable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDurable.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdDurable.IsUsePaging = false;
            this.grdDurable.LanguageKey = "DURABLE";
            this.grdDurable.Location = new System.Drawing.Point(3, 3);
            this.grdDurable.Margin = new System.Windows.Forms.Padding(0);
            this.grdDurable.Name = "grdDurable";
            this.grdDurable.ShowBorder = true;
            this.grdDurable.ShowStatusBar = false;
            this.grdDurable.Size = new System.Drawing.Size(139, 344);
            this.grdDurable.TabIndex = 4;
            this.grdDurable.UseAutoBestFitColumns = false;
            // 
            // smartSpliterControl1
            // 
            this.smartSpliterControl1.Location = new System.Drawing.Point(649, 5);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(5, 440);
            this.smartSpliterControl1.TabIndex = 13;
            this.smartSpliterControl1.TabStop = false;
            // 
            // smartPanel3
            // 
            this.smartPanel3.Controls.Add(this.grdRouting);
            this.smartPanel3.Controls.Add(this.smartPanel4);
            this.smartPanel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.smartPanel3.Location = new System.Drawing.Point(0, 5);
            this.smartPanel3.Name = "smartPanel3";
            this.smartPanel3.Size = new System.Drawing.Size(649, 440);
            this.smartPanel3.TabIndex = 15;
            // 
            // grdRouting
            // 
            this.grdRouting.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdRouting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRouting.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdRouting.IsUsePaging = false;
            this.grdRouting.LanguageKey = "ROUTING";
            this.grdRouting.Location = new System.Drawing.Point(2, 32);
            this.grdRouting.Margin = new System.Windows.Forms.Padding(0);
            this.grdRouting.Name = "grdRouting";
            this.grdRouting.ShowBorder = true;
            this.grdRouting.Size = new System.Drawing.Size(645, 406);
            this.grdRouting.TabIndex = 3;
            this.grdRouting.UseAutoBestFitColumns = false;
            // 
            // smartPanel4
            // 
            this.smartPanel4.Controls.Add(this.selProcessSegment);
            this.smartPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartPanel4.Location = new System.Drawing.Point(2, 2);
            this.smartPanel4.Name = "smartPanel4";
            this.smartPanel4.Size = new System.Drawing.Size(645, 30);
            this.smartPanel4.TabIndex = 4;
            // 
            // selProcessSegment
            // 
            this.selProcessSegment.EditorWidth = "80%";
            this.selProcessSegment.LabelWidth = "20%";
            this.selProcessSegment.LanguageKey = "PROCESSSEGMENT";
            this.selProcessSegment.Location = new System.Drawing.Point(5, 5);
            this.selProcessSegment.Name = "selProcessSegment";
            this.selProcessSegment.Size = new System.Drawing.Size(356, 20);
            this.selProcessSegment.TabIndex = 0;
            // 
            // smartPanel1
            // 
            this.smartPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel1.Controls.Add(this.grdLotInfo);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(926, 105);
            this.smartPanel1.TabIndex = 3;
            // 
            // grdLotInfo
            // 
            this.grdLotInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLotInfo.Location = new System.Drawing.Point(0, 0);
            this.grdLotInfo.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotInfo.Name = "grdLotInfo";
            this.grdLotInfo.Size = new System.Drawing.Size(926, 105);
            this.grdLotInfo.TabIndex = 3;
            // 
            // SampleRouting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1251, 599);
            this.Name = "SampleRouting";
            this.Text = "Sample Routing";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.smartPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartTabControl1)).EndInit();
            this.smartTabControl1.ResumeLayout(false);
            this.tpgResource.ResumeLayout(false);
            this.tpgBOM.ResumeLayout(false);
            this.tpgDurable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel3)).EndInit();
            this.smartPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel4)).EndInit();
            this.smartPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.selProcessSegment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel smartPanel2;
        private Framework.SmartControls.SmartTabControl smartTabControl1;
        private DevExpress.XtraTab.XtraTabPage tpgResource;
        private DevExpress.XtraTab.XtraTabPage tpgBOM;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
        private Framework.SmartControls.SmartBandedGrid grdMaterial;
        private Framework.SmartControls.SmartBandedGrid grdResource;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Commons.Controls.SmartLotInfoGrid grdLotInfo;
        private Framework.SmartControls.SmartPanel smartPanel3;
        private Framework.SmartControls.SmartBandedGrid grdRouting;
        private Framework.SmartControls.SmartPanel smartPanel4;
        private Framework.SmartControls.SmartLabelSelectPopupEdit selProcessSegment;
        private DevExpress.XtraTab.XtraTabPage tpgDurable;
        private Framework.SmartControls.SmartBandedGrid grdDurable;
    }
}
