namespace Micube.SmartMES.StandardInfo
{
    partial class DefectCodeManagement
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
            this.tabDefect = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgDefectClass = new DevExpress.XtraTab.XtraTabPage();
            this.grdDefectClass = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgQcSegmentDefect = new DevExpress.XtraTab.XtraTabPage();
            this.spcDefect = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grdQCSegment = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSaveQcSegment = new Micube.Framework.SmartControls.SmartButton();
            this.spcDefect2 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.grdDefectCode = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSaveDefectCode = new Micube.Framework.SmartControls.SmartButton();
            this.btnSelectDefect = new Micube.Framework.SmartControls.SmartButton();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.grdProcessSegment = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSaveProcessSegmentClass = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabDefect)).BeginInit();
            this.tabDefect.SuspendLayout();
            this.tpgDefectClass.SuspendLayout();
            this.tpgQcSegmentDefect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcDefect)).BeginInit();
            this.spcDefect.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcDefect2)).BeginInit();
            this.spcDefect2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 509);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(932, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabDefect);
            this.pnlContent.Size = new System.Drawing.Size(932, 513);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1237, 542);
            // 
            // tabDefect
            // 
            this.tabDefect.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tabDefect.BorderStylePage = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tabDefect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabDefect.Location = new System.Drawing.Point(0, 0);
            this.tabDefect.Margin = new System.Windows.Forms.Padding(0);
            this.tabDefect.Name = "tabDefect";
            this.tabDefect.SelectedTabPage = this.tpgDefectClass;
            this.tabDefect.Size = new System.Drawing.Size(932, 513);
            this.tabDefect.TabIndex = 0;
            this.tabDefect.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgDefectClass,
            this.tpgQcSegmentDefect});
            // 
            // tpgDefectClass
            // 
            this.tpgDefectClass.Controls.Add(this.grdDefectClass);
            this.tabDefect.SetLanguageKey(this.tpgDefectClass, "DEFECTCODECLASS");
            this.tpgDefectClass.Margin = new System.Windows.Forms.Padding(0);
            this.tpgDefectClass.Name = "tpgDefectClass";
            this.tpgDefectClass.Padding = new System.Windows.Forms.Padding(10);
            this.tpgDefectClass.Size = new System.Drawing.Size(926, 484);
            this.tpgDefectClass.Text = "xtraTabPage1";
            // 
            // grdDefectClass
            // 
            this.grdDefectClass.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdDefectClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDefectClass.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdDefectClass.IsUsePaging = false;
            this.grdDefectClass.LanguageKey = "DEFECTCODECLASSLIST";
            this.grdDefectClass.Location = new System.Drawing.Point(10, 10);
            this.grdDefectClass.Margin = new System.Windows.Forms.Padding(0);
            this.grdDefectClass.Name = "grdDefectClass";
            this.grdDefectClass.ShowBorder = true;
            this.grdDefectClass.ShowStatusBar = false;
            this.grdDefectClass.Size = new System.Drawing.Size(906, 464);
            this.grdDefectClass.TabIndex = 0;
            this.grdDefectClass.UseAutoBestFitColumns = false;
            // 
            // tpgQcSegmentDefect
            // 
            this.tpgQcSegmentDefect.Controls.Add(this.spcDefect);
            this.tabDefect.SetLanguageKey(this.tpgQcSegmentDefect, "DEFECTCODE");
            this.tpgQcSegmentDefect.Margin = new System.Windows.Forms.Padding(0);
            this.tpgQcSegmentDefect.Name = "tpgQcSegmentDefect";
            this.tpgQcSegmentDefect.Padding = new System.Windows.Forms.Padding(10);
            this.tpgQcSegmentDefect.Size = new System.Drawing.Size(926, 484);
            this.tpgQcSegmentDefect.Text = "xtraTabPage2";
            // 
            // spcDefect
            // 
            this.spcDefect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcDefect.Location = new System.Drawing.Point(10, 10);
            this.spcDefect.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcDefect.Name = "spcDefect";
            this.spcDefect.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.spcDefect.Panel2.Controls.Add(this.spcDefect2);
            this.spcDefect.Size = new System.Drawing.Size(906, 464);
            this.spcDefect.SplitterPosition = 600;
            this.spcDefect.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.grdQCSegment, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(600, 464);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // grdQCSegment
            // 
            this.grdQCSegment.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdQCSegment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdQCSegment.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdQCSegment.IsUsePaging = false;
            this.grdQCSegment.LanguageKey = "QCSEGMENTLIST";
            this.grdQCSegment.Location = new System.Drawing.Point(0, 35);
            this.grdQCSegment.Margin = new System.Windows.Forms.Padding(0);
            this.grdQCSegment.Name = "grdQCSegment";
            this.grdQCSegment.ShowBorder = true;
            this.grdQCSegment.Size = new System.Drawing.Size(600, 429);
            this.grdQCSegment.TabIndex = 0;
            this.grdQCSegment.UseAutoBestFitColumns = false;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.btnSaveQcSegment);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel2.Size = new System.Drawing.Size(600, 25);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // btnSaveQcSegment
            // 
            this.btnSaveQcSegment.AllowFocus = false;
            this.btnSaveQcSegment.IsBusy = false;
            this.btnSaveQcSegment.IsWrite = false;
            this.btnSaveQcSegment.LanguageKey = "SAVE";
            this.btnSaveQcSegment.Location = new System.Drawing.Point(520, 0);
            this.btnSaveQcSegment.Margin = new System.Windows.Forms.Padding(0);
            this.btnSaveQcSegment.Name = "btnSaveQcSegment";
            this.btnSaveQcSegment.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSaveQcSegment.Size = new System.Drawing.Size(80, 25);
            this.btnSaveQcSegment.TabIndex = 0;
            this.btnSaveQcSegment.Text = "SAVE";
            this.btnSaveQcSegment.TooltipLanguageKey = "";
            // 
            // spcDefect2
            // 
            this.spcDefect2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcDefect2.Horizontal = false;
            this.spcDefect2.Location = new System.Drawing.Point(0, 0);
            this.spcDefect2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcDefect2.Name = "spcDefect2";
            this.spcDefect2.Panel1.Controls.Add(this.tableLayoutPanel2);
            this.spcDefect2.Panel1.Text = "Panel1";
            this.spcDefect2.Panel2.Controls.Add(this.tableLayoutPanel3);
            this.spcDefect2.Panel2.Text = "Panel2";
            this.spcDefect2.Size = new System.Drawing.Size(301, 464);
            this.spcDefect2.SplitterPosition = 350;
            this.spcDefect2.TabIndex = 0;
            this.spcDefect2.Text = "smartSpliterContainer2";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.grdDefectCode, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(301, 350);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // grdDefectCode
            // 
            this.grdDefectCode.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdDefectCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDefectCode.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdDefectCode.IsUsePaging = false;
            this.grdDefectCode.LanguageKey = "DEFECTCODELIST";
            this.grdDefectCode.Location = new System.Drawing.Point(0, 35);
            this.grdDefectCode.Margin = new System.Windows.Forms.Padding(0);
            this.grdDefectCode.Name = "grdDefectCode";
            this.grdDefectCode.ShowBorder = true;
            this.grdDefectCode.Size = new System.Drawing.Size(301, 315);
            this.grdDefectCode.TabIndex = 0;
            this.grdDefectCode.UseAutoBestFitColumns = false;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.Controls.Add(this.btnSaveDefectCode);
            this.flowLayoutPanel3.Controls.Add(this.btnSelectDefect);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel3.Size = new System.Drawing.Size(301, 25);
            this.flowLayoutPanel3.TabIndex = 1;
            // 
            // btnSaveDefectCode
            // 
            this.btnSaveDefectCode.AllowFocus = false;
            this.btnSaveDefectCode.IsBusy = false;
            this.btnSaveDefectCode.IsWrite = false;
            this.btnSaveDefectCode.LanguageKey = "SAVE";
            this.btnSaveDefectCode.Location = new System.Drawing.Point(221, 0);
            this.btnSaveDefectCode.Margin = new System.Windows.Forms.Padding(0);
            this.btnSaveDefectCode.Name = "btnSaveDefectCode";
            this.btnSaveDefectCode.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSaveDefectCode.Size = new System.Drawing.Size(80, 25);
            this.btnSaveDefectCode.TabIndex = 0;
            this.btnSaveDefectCode.Text = "SAVE";
            this.btnSaveDefectCode.TooltipLanguageKey = "";
            // 
            // btnSelectDefect
            // 
            this.btnSelectDefect.AllowFocus = false;
            this.btnSelectDefect.IsBusy = false;
            this.btnSelectDefect.IsWrite = false;
            this.btnSelectDefect.LanguageKey = "DEFECTCODECLASSSELECT";
            this.btnSelectDefect.Location = new System.Drawing.Point(135, 0);
            this.btnSelectDefect.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.btnSelectDefect.Name = "btnSelectDefect";
            this.btnSelectDefect.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSelectDefect.Size = new System.Drawing.Size(80, 25);
            this.btnSelectDefect.TabIndex = 1;
            this.btnSelectDefect.Text = "smartButton1";
            this.btnSelectDefect.TooltipLanguageKey = "";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.grdProcessSegment, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(301, 109);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // grdProcessSegment
            // 
            this.grdProcessSegment.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdProcessSegment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProcessSegment.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdProcessSegment.IsUsePaging = false;
            this.grdProcessSegment.LanguageKey = "MIDDLEPROCESSSEGMENTCLASSLIST";
            this.grdProcessSegment.Location = new System.Drawing.Point(0, 35);
            this.grdProcessSegment.Margin = new System.Windows.Forms.Padding(0);
            this.grdProcessSegment.Name = "grdProcessSegment";
            this.grdProcessSegment.ShowBorder = true;
            this.grdProcessSegment.Size = new System.Drawing.Size(301, 74);
            this.grdProcessSegment.TabIndex = 0;
            this.grdProcessSegment.UseAutoBestFitColumns = false;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.AutoSize = true;
            this.flowLayoutPanel4.Controls.Add(this.btnSaveProcessSegmentClass);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel4.Size = new System.Drawing.Size(301, 25);
            this.flowLayoutPanel4.TabIndex = 1;
            // 
            // btnSaveProcessSegmentClass
            // 
            this.btnSaveProcessSegmentClass.AllowFocus = false;
            this.btnSaveProcessSegmentClass.IsBusy = false;
            this.btnSaveProcessSegmentClass.IsWrite = false;
            this.btnSaveProcessSegmentClass.LanguageKey = "SAVE";
            this.btnSaveProcessSegmentClass.Location = new System.Drawing.Point(221, 0);
            this.btnSaveProcessSegmentClass.Margin = new System.Windows.Forms.Padding(0);
            this.btnSaveProcessSegmentClass.Name = "btnSaveProcessSegmentClass";
            this.btnSaveProcessSegmentClass.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSaveProcessSegmentClass.Size = new System.Drawing.Size(80, 25);
            this.btnSaveProcessSegmentClass.TabIndex = 0;
            this.btnSaveProcessSegmentClass.Text = "SAVE";
            this.btnSaveProcessSegmentClass.TooltipLanguageKey = "";
            // 
            // DefectCodeManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1275, 580);
            this.Name = "DefectCodeManagement";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabDefect)).EndInit();
            this.tabDefect.ResumeLayout(false);
            this.tpgDefectClass.ResumeLayout(false);
            this.tpgQcSegmentDefect.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcDefect)).EndInit();
            this.spcDefect.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcDefect2)).EndInit();
            this.spcDefect2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabDefect;
        private DevExpress.XtraTab.XtraTabPage tpgDefectClass;
        private Framework.SmartControls.SmartBandedGrid grdDefectClass;
        private DevExpress.XtraTab.XtraTabPage tpgQcSegmentDefect;
        private Framework.SmartControls.SmartSpliterContainer spcDefect;
        private Framework.SmartControls.SmartSpliterContainer spcDefect2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartBandedGrid grdQCSegment;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Framework.SmartControls.SmartButton btnSaveQcSegment;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private Framework.SmartControls.SmartButton btnSaveDefectCode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Framework.SmartControls.SmartBandedGrid grdProcessSegment;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private Framework.SmartControls.SmartButton btnSaveProcessSegmentClass;
        private Framework.SmartControls.SmartButton btnSelectDefect;
        private Framework.SmartControls.SmartBandedGrid grdDefectCode;
    }
}