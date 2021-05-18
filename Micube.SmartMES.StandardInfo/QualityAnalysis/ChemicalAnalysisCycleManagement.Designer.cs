namespace Micube.SmartMES.StandardInfo
{
    partial class ChemicalAnalysisCycleManagement
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
            this.tabCycle = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgCycle = new DevExpress.XtraTab.XtraTabPage();
            this.grdCycle = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgCycleApply = new DevExpress.XtraTab.XtraTabPage();
            this.spcCycle1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.spcCycle3 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdProcessSegmentClass = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdEquipment = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.spcCycle2 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdInspItem = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdCycleApply = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabCycle)).BeginInit();
            this.tabCycle.SuspendLayout();
            this.tpgCycle.SuspendLayout();
            this.tpgCycleApply.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcCycle1)).BeginInit();
            this.spcCycle1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcCycle3)).BeginInit();
            this.spcCycle3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcCycle2)).BeginInit();
            this.spcCycle2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Size = new System.Drawing.Size(371, 667);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(1112, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabCycle);
            this.pnlContent.Size = new System.Drawing.Size(1112, 670);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1493, 706);
            // 
            // tabCycle
            // 
            this.tabCycle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCycle.Location = new System.Drawing.Point(0, 0);
            this.tabCycle.Name = "tabCycle";
            this.tabCycle.SelectedTabPage = this.tpgCycle;
            this.tabCycle.Size = new System.Drawing.Size(1112, 670);
            this.tabCycle.TabIndex = 0;
            this.tabCycle.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgCycle,
            this.tpgCycleApply});
            // 
            // tpgCycle
            // 
            this.tpgCycle.Controls.Add(this.grdCycle);
            this.tabCycle.SetLanguageKey(this.tpgCycle, "CYCLEDEFINITION");
            this.tpgCycle.Name = "tpgCycle";
            this.tpgCycle.Padding = new System.Windows.Forms.Padding(10);
            this.tpgCycle.Size = new System.Drawing.Size(1105, 634);
            this.tpgCycle.Text = "회차정의";
            // 
            // grdCycle
            // 
            this.grdCycle.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdCycle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCycle.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdCycle.IsUsePaging = false;
            this.grdCycle.LanguageKey = "CYCLELIST";
            this.grdCycle.Location = new System.Drawing.Point(10, 10);
            this.grdCycle.Margin = new System.Windows.Forms.Padding(0);
            this.grdCycle.Name = "grdCycle";
            this.grdCycle.ShowBorder = true;
            this.grdCycle.ShowStatusBar = false;
            this.grdCycle.Size = new System.Drawing.Size(1085, 614);
            this.grdCycle.TabIndex = 0;
            // 
            // tpgCycleApply
            // 
            this.tpgCycleApply.Controls.Add(this.spcCycle1);
            this.tabCycle.SetLanguageKey(this.tpgCycleApply, "CYCLEAPPLY");
            this.tpgCycleApply.Name = "tpgCycleApply";
            this.tpgCycleApply.Padding = new System.Windows.Forms.Padding(10);
            this.tpgCycleApply.Size = new System.Drawing.Size(1105, 634);
            this.tpgCycleApply.Text = "회차적용";
            // 
            // spcCycle1
            // 
            this.spcCycle1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcCycle1.Location = new System.Drawing.Point(10, 10);
            this.spcCycle1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcCycle1.Name = "spcCycle1";
            this.spcCycle1.Panel1.Controls.Add(this.spcCycle3);
            this.spcCycle1.Panel1.Text = "Panel1";
            this.spcCycle1.Panel2.Controls.Add(this.spcCycle2);
            this.spcCycle1.Panel2.Text = "Panel2";
            this.spcCycle1.Size = new System.Drawing.Size(1085, 614);
            this.spcCycle1.SplitterPosition = 650;
            this.spcCycle1.TabIndex = 0;
            this.spcCycle1.Text = "smartSpliterContainer1";
            // 
            // spcCycle3
            // 
            this.spcCycle3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcCycle3.Horizontal = false;
            this.spcCycle3.Location = new System.Drawing.Point(0, 0);
            this.spcCycle3.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcCycle3.Name = "spcCycle3";
            this.spcCycle3.Panel1.Controls.Add(this.grdProcessSegmentClass);
            this.spcCycle3.Panel1.Text = "Panel1";
            this.spcCycle3.Panel2.Controls.Add(this.grdEquipment);
            this.spcCycle3.Panel2.Text = "Panel2";
            this.spcCycle3.Size = new System.Drawing.Size(650, 614);
            this.spcCycle3.SplitterPosition = 400;
            this.spcCycle3.TabIndex = 0;
            this.spcCycle3.Text = "smartSpliterContainer1";
            // 
            // grdProcessSegmentClass
            // 
            this.grdProcessSegmentClass.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdProcessSegmentClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProcessSegmentClass.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdProcessSegmentClass.IsUsePaging = false;
            this.grdProcessSegmentClass.LanguageKey = "TOPPROCESSSEGMENTCLASSLIST";
            this.grdProcessSegmentClass.Location = new System.Drawing.Point(0, 0);
            this.grdProcessSegmentClass.Margin = new System.Windows.Forms.Padding(0);
            this.grdProcessSegmentClass.Name = "grdProcessSegmentClass";
            this.grdProcessSegmentClass.ShowBorder = true;
            this.grdProcessSegmentClass.ShowStatusBar = false;
            this.grdProcessSegmentClass.Size = new System.Drawing.Size(650, 400);
            this.grdProcessSegmentClass.TabIndex = 0;
            // 
            // grdEquipment
            // 
            this.grdEquipment.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdEquipment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdEquipment.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdEquipment.IsUsePaging = false;
            this.grdEquipment.LanguageKey = "MAINEQUIPMENTLIST";
            this.grdEquipment.Location = new System.Drawing.Point(0, 0);
            this.grdEquipment.Margin = new System.Windows.Forms.Padding(0);
            this.grdEquipment.Name = "grdEquipment";
            this.grdEquipment.ShowBorder = true;
            this.grdEquipment.ShowStatusBar = false;
            this.grdEquipment.Size = new System.Drawing.Size(650, 208);
            this.grdEquipment.TabIndex = 0;
            // 
            // spcCycle2
            // 
            this.spcCycle2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcCycle2.Location = new System.Drawing.Point(0, 0);
            this.spcCycle2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcCycle2.Name = "spcCycle2";
            this.spcCycle2.Panel1.Controls.Add(this.grdInspItem);
            this.spcCycle2.Panel1.Text = "Panel1";
            this.spcCycle2.Panel2.Controls.Add(this.grdCycleApply);
            this.spcCycle2.Panel2.Text = "Panel2";
            this.spcCycle2.Size = new System.Drawing.Size(429, 614);
            this.spcCycle2.SplitterPosition = 300;
            this.spcCycle2.TabIndex = 0;
            this.spcCycle2.Text = "smartSpliterContainer1";
            // 
            // grdInspItem
            // 
            this.grdInspItem.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdInspItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInspItem.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInspItem.IsUsePaging = false;
            this.grdInspItem.LanguageKey = "INSPITEMLIST";
            this.grdInspItem.Location = new System.Drawing.Point(0, 0);
            this.grdInspItem.Margin = new System.Windows.Forms.Padding(0);
            this.grdInspItem.Name = "grdInspItem";
            this.grdInspItem.ShowBorder = true;
            this.grdInspItem.ShowStatusBar = false;
            this.grdInspItem.Size = new System.Drawing.Size(300, 614);
            this.grdInspItem.TabIndex = 0;
            // 
            // grdCycleApply
            // 
            this.grdCycleApply.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdCycleApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCycleApply.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdCycleApply.IsUsePaging = false;
            this.grdCycleApply.LanguageKey = "CYCLEAPPLYLIST";
            this.grdCycleApply.Location = new System.Drawing.Point(0, 0);
            this.grdCycleApply.Margin = new System.Windows.Forms.Padding(0);
            this.grdCycleApply.Name = "grdCycleApply";
            this.grdCycleApply.ShowBorder = true;
            this.grdCycleApply.ShowStatusBar = false;
            this.grdCycleApply.Size = new System.Drawing.Size(123, 614);
            this.grdCycleApply.TabIndex = 0;
            // 
            // ChemicalAnalysisCycleManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1531, 744);
            this.Name = "ChemicalAnalysisCycleManagement";
            this.Padding = new System.Windows.Forms.Padding(19, 19, 19, 19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabCycle)).EndInit();
            this.tabCycle.ResumeLayout(false);
            this.tpgCycle.ResumeLayout(false);
            this.tpgCycleApply.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcCycle1)).EndInit();
            this.spcCycle1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcCycle3)).EndInit();
            this.spcCycle3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcCycle2)).EndInit();
            this.spcCycle2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabCycle;
        private DevExpress.XtraTab.XtraTabPage tpgCycle;
        private DevExpress.XtraTab.XtraTabPage tpgCycleApply;
        private Framework.SmartControls.SmartBandedGrid grdCycle;
        private Framework.SmartControls.SmartSpliterContainer spcCycle1;
        private Framework.SmartControls.SmartSpliterContainer spcCycle2;
        private Framework.SmartControls.SmartBandedGrid grdCycleApply;
        private Framework.SmartControls.SmartSpliterContainer spcCycle3;
        private Framework.SmartControls.SmartBandedGrid grdProcessSegmentClass;
        private Framework.SmartControls.SmartBandedGrid grdEquipment;
        private Framework.SmartControls.SmartBandedGrid grdInspItem;
    }
}