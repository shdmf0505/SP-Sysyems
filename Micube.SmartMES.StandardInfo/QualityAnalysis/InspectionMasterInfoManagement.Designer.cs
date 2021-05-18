namespace Micube.SmartMES.StandardInfo
{
    partial class InspectionMasterInfoManagement
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
            this.grdInspection = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tabInspectionItemRel = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgResource = new DevExpress.XtraTab.XtraTabPage();
            this.grdResource = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgInspItemClass = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.smartSpliterContainer2 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdInspItemClass = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdInspItemRel = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.btnToSpec = new Micube.Framework.SmartControls.SmartButton();
            this.tpgInspectionPoint = new DevExpress.XtraTab.XtraTabPage();
            this.grdInspectionPoint = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabInspectionItemRel)).BeginInit();
            this.tabInspectionItemRel.SuspendLayout();
            this.tpgResource.SuspendLayout();
            this.tpgInspItemClass.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).BeginInit();
            this.smartSpliterContainer2.SuspendLayout();
            this.tpgInspectionPoint.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Size = new System.Drawing.Size(371, 513);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(625, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer1);
            this.pnlContent.Size = new System.Drawing.Size(625, 516);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1006, 552);
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Horizontal = false;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdInspection);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.tabInspectionItemRel);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(625, 516);
            this.smartSpliterContainer1.SplitterPosition = 250;
            this.smartSpliterContainer1.TabIndex = 0;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdInspection
            // 
            this.grdInspection.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdInspection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInspection.IsUsePaging = false;
            this.grdInspection.LanguageKey = "INSPECTIONCLASSLIST";
            this.grdInspection.Location = new System.Drawing.Point(0, 0);
            this.grdInspection.Margin = new System.Windows.Forms.Padding(0);
            this.grdInspection.Name = "grdInspection";
            this.grdInspection.ShowBorder = true;
            this.grdInspection.Size = new System.Drawing.Size(625, 250);
            this.grdInspection.TabIndex = 0;
            // 
            // tabInspectionItemRel
            // 
            this.tabInspectionItemRel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabInspectionItemRel.Location = new System.Drawing.Point(0, 0);
            this.tabInspectionItemRel.Name = "tabInspectionItemRel";
            this.tabInspectionItemRel.SelectedTabPage = this.tpgResource;
            this.tabInspectionItemRel.Size = new System.Drawing.Size(625, 260);
            this.tabInspectionItemRel.TabIndex = 0;
            this.tabInspectionItemRel.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgResource,
            this.tpgInspItemClass,
            this.tpgInspectionPoint});
            // 
            // tpgResource
            // 
            this.tpgResource.Controls.Add(this.grdResource);
            this.tabInspectionItemRel.SetLanguageKey(this.tpgResource, "RESOURCE");
            this.tpgResource.Name = "tpgResource";
            this.tpgResource.Padding = new System.Windows.Forms.Padding(10);
            this.tpgResource.Size = new System.Drawing.Size(618, 224);
            this.tpgResource.Text = "RESOURCE";
            // 
            // grdResource
            // 
            this.grdResource.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdResource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdResource.IsUsePaging = false;
            this.grdResource.LanguageKey = "RESOURCELIST";
            this.grdResource.Location = new System.Drawing.Point(10, 10);
            this.grdResource.Margin = new System.Windows.Forms.Padding(0);
            this.grdResource.Name = "grdResource";
            this.grdResource.ShowBorder = true;
            this.grdResource.ShowStatusBar = false;
            this.grdResource.Size = new System.Drawing.Size(598, 204);
            this.grdResource.TabIndex = 0;
            // 
            // tpgInspItemClass
            // 
            this.tpgInspItemClass.Controls.Add(this.tableLayoutPanel1);
            this.tabInspectionItemRel.SetLanguageKey(this.tpgInspItemClass, "INSPITEMCLASS/INSPITEM");
            this.tpgInspItemClass.Name = "tpgInspItemClass";
            this.tpgInspItemClass.Padding = new System.Windows.Forms.Padding(10);
            this.tpgInspItemClass.Size = new System.Drawing.Size(618, 224);
            this.tpgInspItemClass.Text = "INSPITEMCLASS";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.smartSpliterContainer2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnToSpec, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(598, 204);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // smartSpliterContainer2
            // 
            this.smartSpliterContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer2.Location = new System.Drawing.Point(0, 30);
            this.smartSpliterContainer2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer2.Name = "smartSpliterContainer2";
            this.smartSpliterContainer2.Panel1.Controls.Add(this.grdInspItemClass);
            this.smartSpliterContainer2.Panel1.Text = "Panel1";
            this.smartSpliterContainer2.Panel2.Controls.Add(this.grdInspItemRel);
            this.smartSpliterContainer2.Panel2.Text = "Panel2";
            this.smartSpliterContainer2.Size = new System.Drawing.Size(598, 174);
            this.smartSpliterContainer2.SplitterPosition = 300;
            this.smartSpliterContainer2.TabIndex = 1;
            this.smartSpliterContainer2.Text = "smartSpliterContainer2";
            // 
            // grdInspItemClass
            // 
            this.grdInspItemClass.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdInspItemClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInspItemClass.IsUsePaging = false;
            this.grdInspItemClass.LanguageKey = "INSPITEMCLASSLIST";
            this.grdInspItemClass.Location = new System.Drawing.Point(0, 0);
            this.grdInspItemClass.Margin = new System.Windows.Forms.Padding(0);
            this.grdInspItemClass.Name = "grdInspItemClass";
            this.grdInspItemClass.ShowBorder = true;
            this.grdInspItemClass.ShowStatusBar = false;
            this.grdInspItemClass.Size = new System.Drawing.Size(300, 174);
            this.grdInspItemClass.TabIndex = 0;
            // 
            // grdInspItemRel
            // 
            this.grdInspItemRel.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdInspItemRel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInspItemRel.IsUsePaging = false;
            this.grdInspItemRel.LanguageKey = "INSPECTIONITEMRELLIST";
            this.grdInspItemRel.Location = new System.Drawing.Point(0, 0);
            this.grdInspItemRel.Margin = new System.Windows.Forms.Padding(0);
            this.grdInspItemRel.Name = "grdInspItemRel";
            this.grdInspItemRel.ShowBorder = true;
            this.grdInspItemRel.ShowStatusBar = false;
            this.grdInspItemRel.Size = new System.Drawing.Size(292, 174);
            this.grdInspItemRel.TabIndex = 0;
            // 
            // btnToSpec
            // 
            this.btnToSpec.AllowFocus = false;
            this.btnToSpec.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnToSpec.IsBusy = false;
            this.btnToSpec.LanguageKey = "SPEC";
            this.btnToSpec.Location = new System.Drawing.Point(518, 0);
            this.btnToSpec.Margin = new System.Windows.Forms.Padding(0);
            this.btnToSpec.Name = "btnToSpec";
            this.btnToSpec.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnToSpec.Size = new System.Drawing.Size(80, 25);
            this.btnToSpec.TabIndex = 2;
            this.btnToSpec.Text = "smartButton1";
            this.btnToSpec.TooltipLanguageKey = "";
            // 
            // tpgInspectionPoint
            // 
            this.tpgInspectionPoint.Controls.Add(this.grdInspectionPoint);
            this.tabInspectionItemRel.SetLanguageKey(this.tpgInspectionPoint, "INSPECTIONPOINT");
            this.tpgInspectionPoint.Name = "tpgInspectionPoint";
            this.tpgInspectionPoint.Padding = new System.Windows.Forms.Padding(10);
            this.tpgInspectionPoint.Size = new System.Drawing.Size(824, 336);
            this.tpgInspectionPoint.Text = "INSPECTIONPOINT";
            // 
            // grdInspectionPoint
            // 
            this.grdInspectionPoint.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdInspectionPoint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInspectionPoint.IsUsePaging = false;
            this.grdInspectionPoint.LanguageKey = "INSPECTIONPOINTLIST";
            this.grdInspectionPoint.Location = new System.Drawing.Point(10, 10);
            this.grdInspectionPoint.Margin = new System.Windows.Forms.Padding(0);
            this.grdInspectionPoint.Name = "grdInspectionPoint";
            this.grdInspectionPoint.ShowBorder = true;
            this.grdInspectionPoint.ShowStatusBar = false;
            this.grdInspectionPoint.Size = new System.Drawing.Size(804, 316);
            this.grdInspectionPoint.TabIndex = 0;
            // 
            // InspectionMasterInfoManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 590);
            this.Name = "InspectionMasterInfoManagement";
            this.Padding = new System.Windows.Forms.Padding(19, 19, 19, 19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabInspectionItemRel)).EndInit();
            this.tabInspectionItemRel.ResumeLayout(false);
            this.tpgResource.ResumeLayout(false);
            this.tpgInspItemClass.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).EndInit();
            this.smartSpliterContainer2.ResumeLayout(false);
            this.tpgInspectionPoint.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdInspection;
        private Framework.SmartControls.SmartTabControl tabInspectionItemRel;
        private DevExpress.XtraTab.XtraTabPage tpgResource;
        private DevExpress.XtraTab.XtraTabPage tpgInspItemClass;
        private DevExpress.XtraTab.XtraTabPage tpgInspectionPoint;
        private Framework.SmartControls.SmartBandedGrid grdResource;
        private Framework.SmartControls.SmartBandedGrid grdInspectionPoint;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer2;
        private Framework.SmartControls.SmartBandedGrid grdInspItemClass;
        private Framework.SmartControls.SmartBandedGrid grdInspItemRel;
        private Framework.SmartControls.SmartButton btnToSpec;
    }
}