namespace Micube.SmartMES.ProcessManagement
{
    partial class DelayLot
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
            this.tabDelayLot = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgDelayLotList = new DevExpress.XtraTab.XtraTabPage();
            this.grdDelayLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgDelayList = new DevExpress.XtraTab.XtraTabPage();
            this.grdDelayList = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabDelayLot)).BeginInit();
            this.tabDelayLot.SuspendLayout();
            this.tpgDelayLotList.SuspendLayout();
            this.tpgDelayList.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 383);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(452, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabDelayLot);
            this.pnlContent.Size = new System.Drawing.Size(452, 378);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(762, 412);
            // 
            // tabDelayLot
            // 
            this.tabDelayLot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabDelayLot.Location = new System.Drawing.Point(0, 0);
            this.tabDelayLot.Name = "tabDelayLot";
            this.tabDelayLot.SelectedTabPage = this.tpgDelayLotList;
            this.tabDelayLot.Size = new System.Drawing.Size(452, 378);
            this.tabDelayLot.TabIndex = 1;
            this.tabDelayLot.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgDelayLotList,
            this.tpgDelayList});
            // 
            // tpgDelayLotList
            // 
            this.tpgDelayLotList.Controls.Add(this.grdDelayLotList);
            this.tabDelayLot.SetLanguageKey(this.tpgDelayLotList, "DELAYLOTLIST");
            this.tpgDelayLotList.Name = "tpgDelayLotList";
            this.tpgDelayLotList.Size = new System.Drawing.Size(450, 348);
            this.tpgDelayLotList.Text = "체공 LOT 이력";
            // 
            // grdDelayLotList
            // 
            this.grdDelayLotList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdDelayLotList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDelayLotList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdDelayLotList.IsUsePaging = false;
            this.grdDelayLotList.LanguageKey = "DELAYLOTLIST";
            this.grdDelayLotList.Location = new System.Drawing.Point(0, 0);
            this.grdDelayLotList.Margin = new System.Windows.Forms.Padding(0);
            this.grdDelayLotList.Name = "grdDelayLotList";
            this.grdDelayLotList.ShowBorder = true;
            this.grdDelayLotList.Size = new System.Drawing.Size(450, 348);
            this.grdDelayLotList.TabIndex = 1;
            this.grdDelayLotList.UseAutoBestFitColumns = false;
            // 
            // tpgDelayList
            // 
            this.tpgDelayList.Controls.Add(this.grdDelayList);
            this.tabDelayLot.SetLanguageKey(this.tpgDelayList, "DELAYLIST");
            this.tpgDelayList.Name = "tpgDelayList";
            this.tpgDelayList.Size = new System.Drawing.Size(450, 348);
            this.tpgDelayList.Text = "체공 이력";
            // 
            // grdDelayList
            // 
            this.grdDelayList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdDelayList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDelayList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdDelayList.IsUsePaging = false;
            this.grdDelayList.LanguageKey = "DELAYLIST";
            this.grdDelayList.Location = new System.Drawing.Point(0, 0);
            this.grdDelayList.Margin = new System.Windows.Forms.Padding(0);
            this.grdDelayList.Name = "grdDelayList";
            this.grdDelayList.ShowBorder = true;
            this.grdDelayList.ShowStatusBar = false;
            this.grdDelayList.Size = new System.Drawing.Size(450, 348);
            this.grdDelayList.TabIndex = 0;
            this.grdDelayList.UseAutoBestFitColumns = false;
            // 
            // DelayLot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "DelayLot";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabDelayLot)).EndInit();
            this.tabDelayLot.ResumeLayout(false);
            this.tpgDelayLotList.ResumeLayout(false);
            this.tpgDelayList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabDelayLot;
        private DevExpress.XtraTab.XtraTabPage tpgDelayLotList;
        private Framework.SmartControls.SmartBandedGrid grdDelayLotList;
        private DevExpress.XtraTab.XtraTabPage tpgDelayList;
        private Framework.SmartControls.SmartBandedGrid grdDelayList;
    }
}