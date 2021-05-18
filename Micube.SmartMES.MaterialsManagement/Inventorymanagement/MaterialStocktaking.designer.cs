namespace Micube.SmartMES.MaterialsManagement
{
    partial class MaterialStocktaking
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
            this.smartSpliterContainer2 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdMaterialStatistics = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.pnlButton = new System.Windows.Forms.FlowLayoutPanel();
            this.btnWipPhysicalList = new Micube.Framework.SmartControls.SmartButton();
            this.btnTakeOverStop = new Micube.Framework.SmartControls.SmartButton();
            this.btnTakeOverStart = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).BeginInit();
            this.smartSpliterContainer2.SuspendLayout();
            this.pnlButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 518);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.pnlButton);
            this.pnlToolbar.Size = new System.Drawing.Size(732, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.pnlButton, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer2);
            this.pnlContent.Size = new System.Drawing.Size(732, 522);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1037, 551);
            // 
            // smartSpliterContainer2
            // 
            this.smartSpliterContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer2.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.smartSpliterContainer2.Horizontal = false;
            this.smartSpliterContainer2.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer2.Name = "smartSpliterContainer2";
            this.smartSpliterContainer2.Panel1.Controls.Add(this.grdMaterialStatistics);
            this.smartSpliterContainer2.Panel1.Text = "Panel1";
            this.smartSpliterContainer2.Size = new System.Drawing.Size(732, 522);
            this.smartSpliterContainer2.SplitterPosition = 11;
            this.smartSpliterContainer2.TabIndex = 4;
            this.smartSpliterContainer2.Text = "smartSpliterContainer2";
            // 
            // grdMaterialStatistics
            // 
            this.grdMaterialStatistics.Caption = "";
            this.grdMaterialStatistics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMaterialStatistics.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdMaterialStatistics.IsUsePaging = false;
            this.grdMaterialStatistics.LanguageKey = "MATERIALSTATUSSTATISTICSLIST";
            this.grdMaterialStatistics.Location = new System.Drawing.Point(0, 0);
            this.grdMaterialStatistics.Margin = new System.Windows.Forms.Padding(0);
            this.grdMaterialStatistics.Name = "grdMaterialStatistics";
            this.grdMaterialStatistics.ShowBorder = true;
            this.grdMaterialStatistics.ShowStatusBar = false;
            this.grdMaterialStatistics.Size = new System.Drawing.Size(732, 506);
            this.grdMaterialStatistics.TabIndex = 115;
            this.grdMaterialStatistics.UseAutoBestFitColumns = false;
            // 
            // pnlButton
            // 
            this.pnlButton.Controls.Add(this.btnWipPhysicalList);
            this.pnlButton.Controls.Add(this.btnTakeOverStop);
            this.pnlButton.Controls.Add(this.btnTakeOverStart);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButton.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlButton.Location = new System.Drawing.Point(47, 0);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(685, 24);
            this.pnlButton.TabIndex = 6;
            // 
            // btnWipPhysicalList
            // 
            this.btnWipPhysicalList.AllowFocus = false;
            this.btnWipPhysicalList.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnWipPhysicalList.Appearance.Options.UseFont = true;
            this.btnWipPhysicalList.IsBusy = false;
            this.btnWipPhysicalList.IsWrite = false;
            this.btnWipPhysicalList.LanguageKey = "WIPCHECKLIST";
            this.btnWipPhysicalList.Location = new System.Drawing.Point(562, 0);
            this.btnWipPhysicalList.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnWipPhysicalList.Name = "btnWipPhysicalList";
            this.btnWipPhysicalList.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnWipPhysicalList.Size = new System.Drawing.Size(120, 23);
            this.btnWipPhysicalList.TabIndex = 0;
            this.btnWipPhysicalList.Text = "실사리스트";
            this.btnWipPhysicalList.TooltipLanguageKey = "";
            this.btnWipPhysicalList.Click += new System.EventHandler(this.btnWipPhysicalList_Click);
            // 
            // btnTakeOverStop
            // 
            this.btnTakeOverStop.AllowFocus = false;
            this.btnTakeOverStop.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnTakeOverStop.Appearance.Options.UseFont = true;
            this.btnTakeOverStop.IsBusy = false;
            this.btnTakeOverStop.IsWrite = false;
            this.btnTakeOverStop.LanguageKey = "WIPCHECKFINISH";
            this.btnTakeOverStop.Location = new System.Drawing.Point(436, 0);
            this.btnTakeOverStop.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnTakeOverStop.Name = "btnTakeOverStop";
            this.btnTakeOverStop.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnTakeOverStop.Size = new System.Drawing.Size(120, 23);
            this.btnTakeOverStop.TabIndex = 2;
            this.btnTakeOverStop.Text = "재공실사 완료";
            this.btnTakeOverStop.TooltipLanguageKey = "";
            this.btnTakeOverStop.Visible = false;
            // 
            // btnTakeOverStart
            // 
            this.btnTakeOverStart.AllowFocus = false;
            this.btnTakeOverStart.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnTakeOverStart.Appearance.Options.UseFont = true;
            this.btnTakeOverStart.IsBusy = false;
            this.btnTakeOverStart.IsWrite = false;
            this.btnTakeOverStart.LanguageKey = "WIPCHECKSTART";
            this.btnTakeOverStart.Location = new System.Drawing.Point(310, 0);
            this.btnTakeOverStart.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnTakeOverStart.Name = "btnTakeOverStart";
            this.btnTakeOverStart.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnTakeOverStart.Size = new System.Drawing.Size(120, 23);
            this.btnTakeOverStart.TabIndex = 3;
            this.btnTakeOverStart.Text = "재공실사 시작";
            this.btnTakeOverStart.TooltipLanguageKey = "";
            this.btnTakeOverStart.Visible = false;
            // 
            // MaterialStocktaking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 571);
            this.Name = "MaterialStocktaking";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).EndInit();
            this.smartSpliterContainer2.ResumeLayout(false);
            this.pnlButton.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer2;
        private Framework.SmartControls.SmartBandedGrid grdMaterialStatistics;
        private System.Windows.Forms.FlowLayoutPanel pnlButton;
        private Framework.SmartControls.SmartButton btnWipPhysicalList;
        private Framework.SmartControls.SmartButton btnTakeOverStop;
        private Framework.SmartControls.SmartButton btnTakeOverStart;
    }
}