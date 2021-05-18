namespace Micube.SmartMES.StandardInfo
{
    partial class InspectionManagement
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
            this.tabInspection = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgInspectionDef = new DevExpress.XtraTab.XtraTabPage();
            this.grdInspectionDef = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgInspectionControl = new DevExpress.XtraTab.XtraTabPage();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.smartPanel3 = new Micube.Framework.SmartControls.SmartPanel();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdstdProcessSegment = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdstdInspectionControl = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel2 = new Micube.Framework.SmartControls.SmartPanel();
            this.smartSpliterContainer2 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdmidProcessSegment = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdmidInspectionControl = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.smartSpliterContainer3 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdtopProcessSegment = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdtopInspectionControl = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabInspection)).BeginInit();
            this.tabInspection.SuspendLayout();
            this.tpgInspectionDef.SuspendLayout();
            this.tpgInspectionControl.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel3)).BeginInit();
            this.smartPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).BeginInit();
            this.smartPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).BeginInit();
            this.smartSpliterContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer3)).BeginInit();
            this.smartSpliterContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Size = new System.Drawing.Size(371, 900);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnSave);
            this.pnlToolbar.Size = new System.Drawing.Size(843, 30);
            this.pnlToolbar.Controls.SetChildIndex(this.btnSave, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabInspection);
            this.pnlContent.Size = new System.Drawing.Size(843, 903);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1224, 939);
            // 
            // tabInspection
            // 
            this.tabInspection.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tabInspection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabInspection.Location = new System.Drawing.Point(0, 0);
            this.tabInspection.Margin = new System.Windows.Forms.Padding(0);
            this.tabInspection.Name = "tabInspection";
            this.tabInspection.SelectedTabPage = this.tpgInspectionDef;
            this.tabInspection.Size = new System.Drawing.Size(843, 903);
            this.tabInspection.TabIndex = 0;
            this.tabInspection.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgInspectionDef,
            this.tpgInspectionControl});
            // 
            // tpgInspectionDef
            // 
            this.tpgInspectionDef.Controls.Add(this.grdInspectionDef);
            this.tabInspection.SetLanguageKey(this.tpgInspectionDef, "INSPECTIONDEFINITION");
            this.tpgInspectionDef.Margin = new System.Windows.Forms.Padding(0);
            this.tpgInspectionDef.Name = "tpgInspectionDef";
            this.tpgInspectionDef.Size = new System.Drawing.Size(836, 867);
            this.tpgInspectionDef.Text = "InspectionDef";
            // 
            // grdInspectionDef
            // 
            this.grdInspectionDef.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdInspectionDef.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInspectionDef.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInspectionDef.IsUsePaging = false;
            this.grdInspectionDef.LanguageKey = "INSPECTIONDEFINITIONLIST";
            this.grdInspectionDef.Location = new System.Drawing.Point(0, 0);
            this.grdInspectionDef.Margin = new System.Windows.Forms.Padding(0);
            this.grdInspectionDef.Name = "grdInspectionDef";
            this.grdInspectionDef.ShowBorder = false;
            this.grdInspectionDef.ShowStatusBar = false;
            this.grdInspectionDef.Size = new System.Drawing.Size(836, 867);
            this.grdInspectionDef.TabIndex = 0;
            // 
            // tpgInspectionControl
            // 
            this.tpgInspectionControl.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.tabInspection.SetLanguageKey(this.tpgInspectionControl, "INSPECTIONCONTROL");
            this.tpgInspectionControl.Margin = new System.Windows.Forms.Padding(0);
            this.tpgInspectionControl.Name = "tpgInspectionControl";
            this.tpgInspectionControl.Padding = new System.Windows.Forms.Padding(10);
            this.tpgInspectionControl.Size = new System.Drawing.Size(824, 592);
            this.tpgInspectionControl.Text = "InspectionControl";
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 3;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.4F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartPanel3, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartPanel2, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartPanel1, 0, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 1;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 847F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(804, 572);
            this.smartSplitTableLayoutPanel1.TabIndex = 1;
            // 
            // smartPanel3
            // 
            this.smartPanel3.Controls.Add(this.smartSpliterContainer1);
            this.smartPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel3.Location = new System.Drawing.Point(537, 3);
            this.smartPanel3.Name = "smartPanel3";
            this.smartPanel3.Size = new System.Drawing.Size(264, 566);
            this.smartPanel3.TabIndex = 4;
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Horizontal = false;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(2, 2);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdstdProcessSegment);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdstdInspectionControl);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(260, 562);
            this.smartSpliterContainer1.SplitterPosition = 227;
            this.smartSpliterContainer1.TabIndex = 1;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdstdProcessSegment
            // 
            this.grdstdProcessSegment.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdstdProcessSegment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdstdProcessSegment.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdstdProcessSegment.IsUsePaging = false;
            this.grdstdProcessSegment.LanguageKey = "PROCESSSEGMENT";
            this.grdstdProcessSegment.Location = new System.Drawing.Point(0, 0);
            this.grdstdProcessSegment.Margin = new System.Windows.Forms.Padding(0);
            this.grdstdProcessSegment.Name = "grdstdProcessSegment";
            this.grdstdProcessSegment.ShowBorder = true;
            this.grdstdProcessSegment.ShowStatusBar = false;
            this.grdstdProcessSegment.Size = new System.Drawing.Size(260, 227);
            this.grdstdProcessSegment.TabIndex = 0;
            // 
            // grdstdInspectionControl
            // 
            this.grdstdInspectionControl.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdstdInspectionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdstdInspectionControl.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdstdInspectionControl.IsUsePaging = false;
            this.grdstdInspectionControl.LanguageKey = "INSPECTIONCLASS";
            this.grdstdInspectionControl.Location = new System.Drawing.Point(0, 0);
            this.grdstdInspectionControl.Margin = new System.Windows.Forms.Padding(0);
            this.grdstdInspectionControl.Name = "grdstdInspectionControl";
            this.grdstdInspectionControl.ShowBorder = true;
            this.grdstdInspectionControl.ShowStatusBar = false;
            this.grdstdInspectionControl.Size = new System.Drawing.Size(260, 329);
            this.grdstdInspectionControl.TabIndex = 0;
            // 
            // smartPanel2
            // 
            this.smartPanel2.Controls.Add(this.smartSpliterContainer2);
            this.smartPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel2.Location = new System.Drawing.Point(270, 3);
            this.smartPanel2.Name = "smartPanel2";
            this.smartPanel2.Size = new System.Drawing.Size(261, 566);
            this.smartPanel2.TabIndex = 3;
            // 
            // smartSpliterContainer2
            // 
            this.smartSpliterContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer2.Horizontal = false;
            this.smartSpliterContainer2.Location = new System.Drawing.Point(2, 2);
            this.smartSpliterContainer2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer2.Name = "smartSpliterContainer2";
            this.smartSpliterContainer2.Panel1.Controls.Add(this.grdmidProcessSegment);
            this.smartSpliterContainer2.Panel1.Text = "Panel1";
            this.smartSpliterContainer2.Panel2.Controls.Add(this.grdmidInspectionControl);
            this.smartSpliterContainer2.Panel2.Text = "Panel2";
            this.smartSpliterContainer2.Size = new System.Drawing.Size(257, 562);
            this.smartSpliterContainer2.SplitterPosition = 227;
            this.smartSpliterContainer2.TabIndex = 1;
            this.smartSpliterContainer2.Text = "smartSpliterContainer2";
            // 
            // grdmidProcessSegment
            // 
            this.grdmidProcessSegment.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdmidProcessSegment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdmidProcessSegment.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdmidProcessSegment.IsUsePaging = false;
            this.grdmidProcessSegment.LanguageKey = "PROCESSSEGMENT";
            this.grdmidProcessSegment.Location = new System.Drawing.Point(0, 0);
            this.grdmidProcessSegment.Margin = new System.Windows.Forms.Padding(0);
            this.grdmidProcessSegment.Name = "grdmidProcessSegment";
            this.grdmidProcessSegment.ShowBorder = true;
            this.grdmidProcessSegment.ShowStatusBar = false;
            this.grdmidProcessSegment.Size = new System.Drawing.Size(257, 227);
            this.grdmidProcessSegment.TabIndex = 0;
            // 
            // grdmidInspectionControl
            // 
            this.grdmidInspectionControl.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdmidInspectionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdmidInspectionControl.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdmidInspectionControl.IsUsePaging = false;
            this.grdmidInspectionControl.LanguageKey = "INSPECTIONCLASS";
            this.grdmidInspectionControl.Location = new System.Drawing.Point(0, 0);
            this.grdmidInspectionControl.Margin = new System.Windows.Forms.Padding(0);
            this.grdmidInspectionControl.Name = "grdmidInspectionControl";
            this.grdmidInspectionControl.ShowBorder = true;
            this.grdmidInspectionControl.ShowStatusBar = false;
            this.grdmidInspectionControl.Size = new System.Drawing.Size(257, 329);
            this.grdmidInspectionControl.TabIndex = 0;
            // 
            // smartPanel1
            // 
            this.smartPanel1.Controls.Add(this.smartSpliterContainer3);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(3, 3);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(261, 566);
            this.smartPanel1.TabIndex = 1;
            // 
            // smartSpliterContainer3
            // 
            this.smartSpliterContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer3.Horizontal = false;
            this.smartSpliterContainer3.Location = new System.Drawing.Point(2, 2);
            this.smartSpliterContainer3.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer3.Name = "smartSpliterContainer3";
            this.smartSpliterContainer3.Panel1.Controls.Add(this.grdtopProcessSegment);
            this.smartSpliterContainer3.Panel1.Text = "Panel1";
            this.smartSpliterContainer3.Panel2.Controls.Add(this.grdtopInspectionControl);
            this.smartSpliterContainer3.Panel2.Text = "Panel2";
            this.smartSpliterContainer3.Size = new System.Drawing.Size(257, 562);
            this.smartSpliterContainer3.SplitterPosition = 227;
            this.smartSpliterContainer3.TabIndex = 1;
            this.smartSpliterContainer3.Text = "smartSpliterContainer3";
            // 
            // grdtopProcessSegment
            // 
            this.grdtopProcessSegment.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdtopProcessSegment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdtopProcessSegment.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdtopProcessSegment.IsUsePaging = false;
            this.grdtopProcessSegment.LanguageKey = "PROCESSSEGMENT";
            this.grdtopProcessSegment.Location = new System.Drawing.Point(0, 0);
            this.grdtopProcessSegment.Margin = new System.Windows.Forms.Padding(0);
            this.grdtopProcessSegment.Name = "grdtopProcessSegment";
            this.grdtopProcessSegment.ShowBorder = true;
            this.grdtopProcessSegment.ShowStatusBar = false;
            this.grdtopProcessSegment.Size = new System.Drawing.Size(257, 227);
            this.grdtopProcessSegment.TabIndex = 0;
            // 
            // grdtopInspectionControl
            // 
            this.grdtopInspectionControl.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdtopInspectionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdtopInspectionControl.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdtopInspectionControl.IsUsePaging = false;
            this.grdtopInspectionControl.LanguageKey = "INSPECTIONCLASS";
            this.grdtopInspectionControl.Location = new System.Drawing.Point(0, 0);
            this.grdtopInspectionControl.Margin = new System.Windows.Forms.Padding(0);
            this.grdtopInspectionControl.Name = "grdtopInspectionControl";
            this.grdtopInspectionControl.ShowBorder = true;
            this.grdtopInspectionControl.ShowStatusBar = false;
            this.grdtopInspectionControl.Size = new System.Drawing.Size(257, 329);
            this.grdtopInspectionControl.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(768, 2);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(80, 24);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "smartButton2";
            this.btnSave.TooltipLanguageKey = "";
            this.btnSave.Visible = false;
            // 
            // InspectionManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 977);
            this.LanguageKey = "INSPECTIONCLASS";
            this.Name = "InspectionManagement";
            this.Padding = new System.Windows.Forms.Padding(19, 19, 19, 19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabInspection)).EndInit();
            this.tabInspection.ResumeLayout(false);
            this.tpgInspectionDef.ResumeLayout(false);
            this.tpgInspectionControl.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel3)).EndInit();
            this.smartPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).EndInit();
            this.smartPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).EndInit();
            this.smartSpliterContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer3)).EndInit();
            this.smartSpliterContainer3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabInspection;
        private DevExpress.XtraTab.XtraTabPage tpgInspectionDef;
        private DevExpress.XtraTab.XtraTabPage tpgInspectionControl;
        private Framework.SmartControls.SmartBandedGrid grdInspectionDef;
        private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartPanel smartPanel3;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdstdProcessSegment;
        private Framework.SmartControls.SmartBandedGrid grdstdInspectionControl;
        private Framework.SmartControls.SmartPanel smartPanel2;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer2;
        private Framework.SmartControls.SmartBandedGrid grdmidProcessSegment;
        private Framework.SmartControls.SmartBandedGrid grdmidInspectionControl;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer3;
        private Framework.SmartControls.SmartBandedGrid grdtopProcessSegment;
        private Framework.SmartControls.SmartBandedGrid grdtopInspectionControl;
    }
}