namespace Micube.SmartMES.ProcessManagement
{
    partial class InOutLotRecord
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InOutLotRecord));
            this.toolTipWipInfo = new DevExpress.Utils.ToolTipController(this.components);
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.grdShipping = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tabControl = new Micube.Framework.SmartControls.SmartTabControl();
            this.tbInput = new DevExpress.XtraTab.XtraTabPage();
            this.grdInput = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tbShipping = new DevExpress.XtraTab.XtraTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tbInput.SuspendLayout();
            this.tbShipping.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabControl);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "copy_16x16.png");
            // 
            // grdShipping
            // 
            this.grdShipping.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdShipping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdShipping.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.grdShipping.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.Export;
            this.grdShipping.IsUsePaging = false;
            this.grdShipping.LanguageKey = "SHIPRESULT";
            this.grdShipping.Location = new System.Drawing.Point(0, 0);
            this.grdShipping.Margin = new System.Windows.Forms.Padding(0);
            this.grdShipping.Name = "grdShipping";
            this.grdShipping.ShowBorder = true;
            this.grdShipping.ShowStatusBar = false;
            this.grdShipping.Size = new System.Drawing.Size(750, 460);
            this.grdShipping.TabIndex = 6;
            this.grdShipping.UseAutoBestFitColumns = false;
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedTabPage = this.tbInput;
            this.tabControl.Size = new System.Drawing.Size(756, 489);
            this.tabControl.TabIndex = 7;
            this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tbInput,
            this.tbShipping});
            // 
            // tbInput
            // 
            this.tbInput.Controls.Add(this.grdInput);
            this.tabControl.SetLanguageKey(this.tbInput, "INPUTRESULT");
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(750, 460);
            this.tbInput.Text = "투입실적";
            // 
            // grdInput
            // 
            this.grdInput.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInput.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.grdInput.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.Export;
            this.grdInput.IsUsePaging = false;
            this.grdInput.LanguageKey = "INPUTRESULT";
            this.grdInput.Location = new System.Drawing.Point(0, 0);
            this.grdInput.Margin = new System.Windows.Forms.Padding(0);
            this.grdInput.Name = "grdInput";
            this.grdInput.ShowBorder = true;
            this.grdInput.ShowStatusBar = false;
            this.grdInput.Size = new System.Drawing.Size(750, 460);
            this.grdInput.TabIndex = 7;
            this.grdInput.UseAutoBestFitColumns = false;
            // 
            // tbShipping
            // 
            this.tbShipping.Controls.Add(this.grdShipping);
            this.tabControl.SetLanguageKey(this.tbShipping, "SHIPRESULT");
            this.tbShipping.Name = "tbShipping";
            this.tbShipping.Size = new System.Drawing.Size(750, 460);
            this.tbShipping.Text = "출고실적";
            // 
            // InOutLotRecord
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1081, 538);
            this.Name = "InOutLotRecord";
            this.Text = "LotLocking";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tbInput.ResumeLayout(false);
            this.tbShipping.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.Utils.ToolTipController toolTipWipInfo;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private Framework.SmartControls.SmartBandedGrid grdShipping;
        private Framework.SmartControls.SmartTabControl tabControl;
        private DevExpress.XtraTab.XtraTabPage tbInput;
        private Framework.SmartControls.SmartBandedGrid grdInput;
        private DevExpress.XtraTab.XtraTabPage tbShipping;
    }
}